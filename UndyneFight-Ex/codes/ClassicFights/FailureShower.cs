using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Fight
{
    internal class FailureShower : Selector
    {
        private float alpha = 0;
        private static IClassicFight fight;

        public FailureShower(IClassicFight fight, GameMode mode) : base(false)
        {
            FailureShower.fight = fight;
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp))
                    currentSelect -= 2;
                else if (IsKeyPressed120f(InputIdentity.MainDown))
                    currentSelect += 2;
                else if (IsKeyPressed120f(InputIdentity.MainRight))
                    currentSelect++;
                else if (IsKeyPressed120f(InputIdentity.MainLeft))
                    currentSelect--;
                if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                else if (currentSelect < 0) currentSelect = 0;
            };
            SelectChanged += () => { FightResources.Sounds.changeSelection.CreateInstance().Play(); };

            PushSelection(new ReTry(mode));
            PushSelection(new GiveUp(mode));
        }

        public override void Update()
        {
            if (alpha < 1)
                alpha += 0.025f;
            base.Update();
        }

        public override void Draw()
        {
            GlobalResources.Font.NormalFont.CentreDraw("Game Over", new Vector2(320, 85), Color.White * alpha);
            base.Draw();
        }

        private class ReTry : TextSelection
        {
            GameMode mode;
            public ReTry(GameMode mode) : base("Try again", new Vector2(320, 220)) { Size = 1.0f; this.mode = mode; }
            public override void SelectionEvent()
            {
                SelectBattle(fight, mode);
                base.SelectionEvent();
            }
        }

        private class GiveUp : TextSelection
        {
            GameMode mode;
            public GiveUp(GameMode mode) : base("Quit", new Vector2(320, 270))
            { Size = 1.0f; this.mode = mode; }
            public override void SelectionEvent()
            {
                InstanceCreate(new FightSelector(mode));
                base.SelectionEvent();
            }
        }
    }
}