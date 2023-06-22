using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.FightResources.Sprites;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Achievements
{
    internal class AchievementBlock : Entity, ISelectAble
    {
        public static Vector2 positionDetla = Vector2.Zero;

        private bool isSelected = false;
        private int posX, posY;
        private Achievement achievementPresenting;

        public AchievementBlock(Achievement achievement, int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
            achievementPresenting = achievement;
            collidingBox.Width = 108;
            collidingBox.Height = 120;
            Centre = new Vector2(320 + (posX - 2) * 119, 84 + posY * 125);
            LocateCard();
        }
        public AchievementBlock CopyBlock()
        {
            return new AchievementBlock(achievementPresenting, posX, posY);
        }

        public override void Update()
        {
            Depth = 0.2f;
            controlLayer = Surface.Normal;

            if (isSelected)
            {
                bodyColor = Color.Lerp(bodyColor, Color.Gold, 0.25f);
                infoDelta = Vector2.Lerp(infoDelta, Vector2.Zero, 0.1f);
                infoAlpha = MathHelper.Lerp(infoAlpha, 1, 0.12f);
            }
            else
            {
                bodyColor = Color.Lerp(bodyColor, Color.White, 0.25f);
                infoDelta = Vector2.Lerp(infoDelta, new(0, -95), 0.1f);
                infoAlpha = infoAlpha <= 0 ? 0 : MathHelper.Lerp(infoAlpha, -0.1f, 0.1f);
            }
        }
        Color bodyColor = Color.White;
        float infoAlpha = 0;
        Vector2 infoPos;
        Vector2 infoSize = new(180, 240);
        Vector2 infoDelta;
        Vector2 infoDeltaStart;

        private void LocateCard()
        {
            Vector2 pos = collidingBox.TopRight;
            pos.X += 15;
            infoDeltaStart = new(60, 0);
            if (pos.X + infoSize.X + 15 > 640)
            {
                pos.X = collidingBox.Left - infoSize.X - 15;
                infoDeltaStart = new(-60, 0);
            }
            pos.Y += 30;
            if (pos.Y + infoSize.Y + 10 > 480) pos.Y = collidingBox.Up - infoSize.Y - 10;
            infoPos = pos;
        }

        public override void Draw()
        {
            Depth = 0.27f;
            DrawingLab.DrawRectangle(collidingBox, bodyColor, 3, Depth);
            if (infoAlpha > 0)
            {
                Color whiteAlpha = Color.White * infoAlpha;
                CollideRect box = new(infoPos + infoDelta, infoSize);
                DrawingLab.DrawRectangle(box, Color.LightBlue * infoAlpha, 3, 0.98f);
                Depth = 0.91f;
                FormalDraw(pixiv, box.ToRectangle(), Color.DarkSlateBlue * infoAlpha);

                GLFont font = FightResources.Font.NormalFont;
                Vector2 boxCentre = box.GetCentre();
                float boxUp = box.Up;
                float boxLeft = box.Left;
                font.CentreDraw(achievementPresenting.Title, new Vector2(boxCentre.X, boxUp + 24), whiteAlpha, 0.7f, 0.96f);
                font.LimitDraw(achievementPresenting.AchievementIntroduction, new Vector2(boxLeft + 8, boxUp + 48), whiteAlpha, box.Width - 14, 22, 0.65f, 0.96f);

                CollideRect rect = new(boxLeft + 14, box.Down - 21, box.Right - 28 - boxLeft, 8);
                CollideRect occp = new(rect.Left, rect.Up,
                    rect.Width * MathF.Min(achievementPresenting.CurrentProgress * 1.0f / achievementPresenting.FullProgress, 1f),
                    rect.Height);

                Depth = 0.963f;
                FormalDraw(pixiv, rect.ToRectangle(), Color.Red * infoAlpha);
                Depth = 0.975f;
                FormalDraw(pixiv, occp.ToRectangle(), Color.Lime * infoAlpha);

                font.CentreDraw(achievementPresenting.CurrentProgress + "/" + achievementPresenting.FullProgress, rect.GetCentre() - new Vector2(0, 21),
                    (!achievementPresenting.Achieved ? Color.White : (achievementPresenting.OnlineAchieved ? Color.Gold : Color.Yellow)) * infoAlpha, 1.0f, 0.99f);
            }
        }

        public void DeSelected() => isSelected = false;
        public void Selected()
        {
            infoAlpha = 0.2f;
            infoDelta = infoDeltaStart;
            isSelected = true;
        }
        public void SelectionEvent()
        {

        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
    internal class AchievementShower : Selector
    {
        private class MenuSelections
        {
            public class BackButton : TextSelection
            {
                public BackButton() : base("Back", new Vector2(320, 430))
                {
                    Size = 0.9f;
                }

                public override void SelectionEvent()
                {
                    Back();
                    instance.Dispose();
                }
            }
            public static List<ISelectAble> GetMenuSelections()
            {
                return new List<ISelectAble>() { new BackButton() };
            }
        }

        private static Achievement[,] achievementLists;
        private static List<AchievementBlock[]> achievementBlocksList;

        public Vector2 PositionDelta { get; private set; } = Vector2.Zero;

        public static void Initialze()
        {
            int pageCount = 1 + (AchievementManager.achievements.Count - 1) / 18;
            achievementLists = new Achievement[pageCount, 15];

            SortedSet<Achievement> achievementSort = new();

            foreach (var v in AchievementManager.achievements.Values)
                if (!v.Hidden) achievementSort.Add(v);

            Achievement[] resultAchievements = achievementSort.ToArray();

            for (int i = 0; i < AchievementManager.achievements.Count; i++)
                achievementLists[i / 15, i % 15] = resultAchievements[^(i + 1)];

            achievementBlocksList = new List<AchievementBlock[]>();
            for (int i = 0; i < pageCount; i++)
            {
                AchievementBlock[] achievementBlocks;
                var val = (i != pageCount - 1) ? 15 : AchievementManager.achievements.Count % 15;
                achievementBlocks = new AchievementBlock[val];
                for (int j = 0; j < achievementBlocks.Length; j++)
                {
                    achievementBlocks[j] = achievementLists[i, j].GetBlock(j % 5, j / 5);
                }
                achievementBlocksList.Add(achievementBlocks);
            }
        }

        private void SelectPage(int page)
        {
            Selections.Clear();
            foreach (var v in achievementBlocksList[page])
            {
                PushSelection(v.CopyBlock());
            }
            var menu = MenuSelections.GetMenuSelections();
            foreach (var v in menu)
            {
                PushSelection(v);
            }
            currentSelect = Selections.Count - 2;
        }

        private static AchievementShower instance;
        public AchievementShower()
        {
            instance = this;

            IsCancelAvailable = false;
            AutoDispose = false;

            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp))
                {
                    currentSelect -= 5;
                }
                else if (IsKeyPressed120f(InputIdentity.MainDown))
                {
                    currentSelect += 5;
                }
                if (IsKeyPressed120f(InputIdentity.MainRight))
                {
                    currentSelect += 1;
                }
                else if (IsKeyPressed120f(InputIdentity.MainLeft))
                {
                    currentSelect -= 1;
                }
                if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                else if (currentSelect < 0) currentSelect = 0;
            };
            SelectChanged += () => { changeSelection.CreateInstance().Play(); };
            Selected += () =>
            {
                select.CreateInstance().Play();
            };
        }
        public override void Start()
        {
            SelectPage(0);
        }
    }
}