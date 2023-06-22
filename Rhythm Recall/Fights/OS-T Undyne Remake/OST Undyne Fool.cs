using Microsoft.Xna.Framework;
using UndyneFight_Ex;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyneFool : OSTUndyne, IClassicFight
    {
        public override string FightName => base.FightName + "(fool)";
        public override void Start()
        {
            base.Start();
            slowDownTime = 5;
            IsFoolMode = true;
            HeartAttribute.DamageTaken = 9;
            GameStates.InstanceCreate(new FoolShower());
        }
        private class FoolShower : Entity
        {
            private int appearTime = 0;

            public FoolShower()
            {
                Centre = new(564, 412);
            }

            public override void Draw()
            {
                FightResources.Font.FightFont.CentreDraw("u fool!", Centre, new Color(DrawingLab.HsvToRgb(appearTime * 1.6f, 255, 255, 127)), 1.3f, 0.5f);
                for (int i = 0; i < 4; i++)
                {
                    FightResources.Font.FightFont.CentreDraw("u fool!", Centre + MathUtil.GetVector2(Sin(appearTime * 2) * 30, appearTime * 1.3f + i * 90), new Color(DrawingLab.HsvToRgb(i * 90 + appearTime * 0.5f, 255, 255, 127)), 1.3f, 0.5f + i * 0.01f);
                }
            }

            public override void Update()
            {
                appearTime++;
            }
        }
    }
}