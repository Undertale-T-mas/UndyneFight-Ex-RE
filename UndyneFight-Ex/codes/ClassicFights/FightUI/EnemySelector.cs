using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Fight
{
    internal static class EnemyShowing
    {
        private class EnemyShower : FightTextSelection
        {
            public static Vector2 positionDetla;
            private readonly Enemy connected;
            public EnemyShower(string text, float height, Enemy enemy) : base(text, new Vector2(50, height))
            {
                controlLayer = Surface.Hidden;
                connected = enemy;
            }

            public override void ZPressed()
            {
                FightStates.controllingEnemy = connected;
                switch (FightStates.firstDecision)
                {
                    case 0:
                        ClassicFight.EndSelecting();
                        GameStates.InstanceCreate(new AttackTarget());
                        break;
                    case 1:
                        GameStates.InstanceCreate(ActionShowing.GetActionSelector());
                        break;
                    default:
                        break;
                }

            }
        }

        internal static FightSelectionCollection GetEnemySelector()
        {
            FightSelection[] fightSelections = new FightSelection[FightStates.enemys.Count];
            float curHeight = 256;
            int i = 0;
            foreach (var v in FightStates.enemys)
            {
                fightSelections[i] = new EnemyShower("  * " + v.Name, curHeight, v);
                i++;
                curHeight += 40;
            }
            return new FightSelectionCollection(fightSelections) { };
        }
    }
}