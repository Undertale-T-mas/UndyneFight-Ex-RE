using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;
namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave10Initialize()
            {
                SetBox(250, 580, 275);
                TP();
                SetSoul(0);
                waitingTime = 50;
            }
            private static float waitingTime = 50;
            public static void Wave10Update()
            {
                waitingTime -= 1;
                if (waveTime <= 720)
                    if (waitingTime <= 0)
                    {
                        waitingTime = IsFoolMode ? 45 : 50 - Sqrt(waveTime) * 1.1f;
                        float rot = Rand(0, 44);
                        for (int i = 0; i < 8; i++)
                        {
                            CreateSpear(new SwarmSpear(Heart.Centre, 6.6f, 155, rot += 45, 52));
                        }
                        rot -= 13f;
                        for (int i = 0; i < 8; i++)
                        {
                            rot += 22.5f;
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(90, rot + 90), 7.6f, 185, rot, 52));
                            rot += 22.5f;
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(110, rot - 90), 7.6f, 185, rot, 52));
                        }
                    }
                if (waveTime == 785) SetSoul(1);
                if (waveTime >= 800)
                {
                    ChangeRound();
                }
            }
        }
    }
}