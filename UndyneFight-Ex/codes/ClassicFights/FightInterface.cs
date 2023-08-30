using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Fight
{
    public abstract class FightSelection : Entity
    {
        public FightSelection() { }
        public FightSelection(Texture2D image)
        {
            Image = image;
            collidingBox = new CollideRect(0, 0, image.Width, image.Height);
        }

        public virtual Vector2 HeartStayPosition => Centre;

        protected bool GetCollide()
        {
            return Player.heartInstance != null && GetCollide(Player.heartInstance);
        }
        protected bool GetCollide(Player.Heart player)
        {
            return collidingBox.Contain(player.Centre);
        }

        public override void Update()
        {
            if (GetCollide())
            {
                if (IsKeyPressed(InputIdentity.Confirm))
                {
                    Functions.PlaySound(Sounds.select);
                    ZPressed();
                }
            }
        }

        public abstract void ZPressed();
    }
    public abstract class FightTextSelection : FightSelection
    {
        public override Vector2 HeartStayPosition => new(collidingBox.TopLeft.X + 10, Centre.Y);
        private bool isCollide;
        protected bool IsCollide => isCollide;
        private readonly string text;
        protected string Text => text;
        private Color drawingColor = Color.White;

        protected Vector2 Delta { set; private get; }

        private Vector2 expectCentre;
        protected Vector2 ExpectCentre => expectCentre;

        public FightTextSelection(string text, Vector2 location)
        {
            collidingBox.Size = Font.NormalFont.SFX.MeasureString(text);
            collidingBox.TopLeft = location;
            expectCentre = Centre;
            this.text = text;
        }

        public Color DrawingColor { set => drawingColor = value; }

        public override void Draw()
        {
            Font.NormalFont.CentreDraw(text, Centre, isCollide ? Color.Lerp(drawingColor, Color.Gold, 0.5f) : drawingColor, 1.0f, 0.0f);
        }
        public override void Update()
        {
            Centre = expectCentre + Delta;
            isCollide = GetCollide();
            base.Update();
        }
    }
    public class FightSelectionCollection : Entity
    {
        internal static Stack<FightSelectionCollection> decisionTree;
        internal static FightSelectionCollection MainMenu { get; private set; }
        private bool isCirclulateSelection;
        private int currentSelection = 0;
        private FightSelection[] selections;

        public FightSelectionCollection(FightSelection[] selections)
        {
            controlLayer = Surface.Hidden;
            if (decisionTree.Count > 0)
            {
                decisionTree.Peek().Dispose();
                if (selections.Length == 0)
                {
                    ClassicFight.EndSelecting();
                    ClassicFight.ChangeRound();
                    return;
                }
                Player.heartInstance.Teleport(selections[0].HeartStayPosition);
            }
            else
            {
                MainMenu = this;
                Player.heartInstance.Teleport(selections[FightStates.firstDecision].HeartStayPosition);
            }

            decisionTree.Push(this);
            this.selections = selections;
        }

        public bool IsCirclulateSelection { set => isCirclulateSelection = value; }
        public bool Enabled { get; internal set; } = true;
        public bool UpdateEnabled { get; internal set; } = true;

        public override void Draw()
        {
            if (this != MainMenu) MainMenu.Draw();
            foreach (var v in selections) v.Draw();
        }

        public override void Update()
        {
            selections = (from x in selections where !x.Disposed select x).ToArray();
            if (selections == null || selections.Length == 0)
            {
                Dispose();
                return;
            }
            if (UpdateEnabled)
                foreach (var v in selections)
                    v.Update();
            if (!Enabled) return;

            int last = currentSelection;
            if (IsKeyPressed(InputIdentity.MainUp) || IsKeyPressed(InputIdentity.MainLeft))
            {
                currentSelection--;
                if (currentSelection < 0)
                    currentSelection = isCirclulateSelection ? selections.Length - 1 : 0;
            }
            if (IsKeyPressed(InputIdentity.MainDown) || IsKeyPressed(InputIdentity.MainRight))
            {
                currentSelection++;
                if (currentSelection >= selections.Length)
                    currentSelection = isCirclulateSelection ? 0 : selections.Length - 1;
            }
            if (last != currentSelection) //selection is changed
                Player.heartInstance.Teleport(selections[currentSelection].HeartStayPosition);

            if (IsKeyPressed(InputIdentity.Cancel))
            {
                if (IsKeyPressed(InputIdentity.Confirm)) return;
                if (this == MainMenu) return;
                Dispose();
                decisionTree.Pop();
                FightSelectionCollection v = decisionTree.Peek();
                v.Reverse();
                v.Enabled = true;
                Player.heartInstance.Teleport(v.selections[v.currentSelection].HeartStayPosition);
                if (v != MainMenu)
                    InstanceCreate(v);
            }
        }

        public void Teleport()
        {
            Player.heartInstance.Teleport(selections[currentSelection].HeartStayPosition);
        }

        public override void Dispose()
        {
            if (this != MainMenu)
                base.Dispose();
            if (this == MainMenu)
            {
                FightStates.boxMessage?.Dispose();
                Enabled = false;
            }
        }

        public override void Reverse()
        {
            if (this == MainMenu && FightStates.roundType)
                ClassicFight.ResetMessage();
            base.Reverse();
        }
    }

    public interface IExtraOption
    {
        public string OptionName { get; }
        public Type IntroScene { get; }
    }
    public interface IClassicFight
    {
        public string FightName { get; }

        public void Start();
        public void RoundChanged();
        public void Update();
        public void GameEnd();
    }

    internal class FightVirtualObject : GameObject
    {
        private readonly IClassicFight classicFight;
        public FightVirtualObject(IClassicFight classicFight)
        {
            classicFight.Start();
            this.classicFight = classicFight;
        }
        public override void Update()
        {
            classicFight.Update();
            if (isInBattle)
                FightStates.enemys.RemoveAll(s => s.Disposed);
        }
        public override void Dispose()
        {
            classicFight.GameEnd();
            base.Dispose();
        }
    }
    internal static class ClassicFightEnterance
    {
        public static IClassicFight currentFight;
        public static void CreateClassicFight(IClassicFight classicFight)
        {
            FightStates.UIBoxPosition = new CollideRect(320 - 589 / 2f, 312 - 140 / 2f, 589, 140);
            ClassicFight.InterActive.Reset();
            FightStates.enemys = new List<Enemy>();
            FightStates.items = new List<Item>();
            FightStates.actions = new List<GameAction>();
            FightSelectionCollection.decisionTree = new Stack<FightSelectionCollection>();
            FightStates.roundType = true;
            FightStates.finishSelecting = true;
            StateShower.DisposeInstance();
            GravityLine.GravityLines.Clear();
            currentFight = classicFight;
            InstanceCreate(new FightVirtualObject(classicFight));
        }
    }
}