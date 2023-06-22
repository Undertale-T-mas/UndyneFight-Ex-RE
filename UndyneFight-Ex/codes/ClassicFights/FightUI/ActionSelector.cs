using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Fight
{
    internal static class ActionShowing
    {
        internal class ActionSelector : FightTextSelection
        {
            public static Vector2 positionDetla;
            private readonly GameAction action;

            public ActionSelector(string text, float height, GameAction action) : base(text, new Vector2(50, height))
            {
                this.action = action;
                controlLayer = Surface.Hidden;
            }

            public override void ZPressed()
            {
                ClassicFight.EndSelecting();
                action.UsingEvent();
            }
        }

        internal static FightSelectionCollection GetActionSelector()
        {
            FightSelection[] fightSelections = new FightSelection[FightStates.actions.Count];
            float curHeight = 256;
            int i = 0;
            foreach (var v in FightStates.actions)
            {
                fightSelections[i] = new ActionSelector("  * " + v.Name, curHeight, v);
                i++;
                curHeight += 40;
            }
            return new FightSelectionCollection(fightSelections) { };
        }
    }
}