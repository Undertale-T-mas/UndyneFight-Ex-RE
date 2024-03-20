using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;

namespace UndyneFight_Ex.Fight
{
    internal static class ItemShowing
    {
        private class ItemShower : FightTextSelection
        {
            public static Vector2 positionDelta;
            public static Vector2 deltaMission;
            private readonly Item item;
            public static int appearTime = 0;

            public ItemShower(string text, float height, Item item) : base(text, new Vector2(50, height))
            {
                controlLayer = Surface.Hidden;
                this.item = item;
            }
            public override void Update()
            {
                if (item == FightStates.items[0])
                {
                    appearTime++;
                    positionDelta = (positionDelta * 0.8f) + (deltaMission * 0.2f);
                }
                if (IsCollide && appearTime >= 10) CalculateDelta();
                Delta = positionDelta;
                base.Update();
            }
            private void CalculateDelta()
            {
                if (Heart.Centre.Y > (FightBox.instance as RectangleBox).Down - 16)
                {
                    Heart.Centre = new Vector2(Heart.Centre.X, (FightBox.instance as RectangleBox).Down - 16);
                    Player.heartInstance.Teleport(new Vector2(HeartStayPosition.X, ExpectCentre.Y + deltaMission.Y));
                    deltaMission.Y -= 40;
                }
                if (Heart.Centre.Y < (FightBox.instance as RectangleBox).Up + 16)
                {
                    Heart.Centre = new Vector2(Heart.Centre.X, (FightBox.instance as RectangleBox).Up + 16);
                    Player.heartInstance.Teleport(new Vector2(HeartStayPosition.X, (FightBox.instance as RectangleBox).Up + 14 + 16));
                    deltaMission.Y += 40;
                }
            }

            public override void ZPressed()
            {
                if (item.IsConsumable)
                {
                    FightStates.items.Remove(item);
                }
                ClassicFight.EndSelecting();
                item.UsingEvent();
            }
        }

        internal static FightSelectionCollection GetItemSelector()
        {
            ItemShower.positionDelta = Vector2.Zero;
            ItemShower.deltaMission = Vector2.Zero;
            ItemShower.appearTime = 0;
            FightSelection[] fightSelections = new FightSelection[FightStates.items.Count];
            float curHeight = (FightBox.instance as RectangleBox).Up + 14;
            int i = 0;
            foreach (var v in FightStates.items)
            {
                fightSelections[i] = new ItemShower("  * " + v.Name, curHeight, v);
                i++;
                curHeight += 40;
            }
            return new FightSelectionCollection(fightSelections);
        }
    }
}