using Microsoft.Xna.Framework;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    public class CashIndicater : Entity
    {
        float x = -444;
        public CashIndicater()
        {
            ValueEasing.EaseBuilder builder = new();
            builder.Insert(45, ValueEasing.EaseOutBack(-220, 0, 45));
            builder.Run(s => { x = s; });
        }

        public override void Draw()
        {
            var cash = PlayerManager.CurrentUser.ShopData.CashManager;
            Vector2 anchor = IntroUI.RatingShowing.CollidingBox.TopLeft + new Vector2(8 + x, -27);

            GlobalResources.Font.NormalFont.Draw("Energy:" + cash.Energy, anchor - new Vector2(0, 37), Color.Aqua, 0.72f, 0.4f);
            GlobalResources.Font.NormalFont.Draw("Resonance:" + cash.Resonance, anchor - new Vector2(0, 16), Color.LightCoral, 0.72f, 0.4f);
        }

        public override void Update()
        {
        }
        public override void Dispose()
        {
            ValueEasing.EaseBuilder builder = new();
            builder.Insert(44, ValueEasing.EaseInBack(x, -220, 44));
            builder.Run(s => { x = s; });
            AddChild(new InstantEvent(45, () =>
            {
                base.Dispose();
            }));
        }
    }

    internal partial class StoreFront : Selector
    {
        internal abstract class StoreArea : Entity, ISelectAble
        {
            private CollideRect area;
            public StoreArea(CollideRect area)
            {
                this.area = area;
            }

            protected abstract Selector AreaEntity { get; }

            bool isSelected = false;
            public void DeSelected()
            {
                isSelected = false;
            }

            public void Selected()
            {
                isSelected = true;
            }

            public override void Draw()
            {
                Color drawingColor = isSelected ? Color.Gold : Color.White;
                DrawingLab.DrawRectangle(area, drawingColor, 3.5f, 0.5f);
            }

            public void SelectionEvent()
            {
                InstanceCreate(AreaEntity);
                InstanceCreate(cashIndicater = new CashIndicater());
            }
        }
        private static CashIndicater cashIndicater;
        public StoreFront()
        {
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp))
                    currentSelect -= 2;
                else if (IsKeyPressed120f(InputIdentity.MainDown))
                    currentSelect += 2;
                if (IsKeyPressed120f(InputIdentity.MainRight))
                    currentSelect--;
                else if (IsKeyPressed120f(InputIdentity.MainLeft))
                    currentSelect--;
                if (currentSelect >= SelectionCount) currentSelect -= SelectionCount;
                else if (currentSelect < 0) currentSelect = SelectionCount - 1;
            };
            ResetSelect();
            Fight.Functions.PlaySound(FightResources.Sounds.select, 0.45f);
            Selected += () => { Fight.Functions.PlaySound(FightResources.Sounds.select); };
            SelectChanged += () =>
            {
                Fight.Functions.PlaySound(FightResources.Sounds.changeSelection, 0.45f);
            };
            PushSelection(new CashChanger(new(30, 30, 283, 110)));
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Reverse()
        {
            cashIndicater.Dispose();
            base.Reverse();
        }
    }
}