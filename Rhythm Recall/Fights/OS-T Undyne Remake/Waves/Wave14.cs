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
            public static void Wave14Initialize()
            {
                SetBox(250, 580, 275);
                TP();
                SetSoul(0);
                if (!IsFoolMode)
                    Heart.RotateTo(180);
                waitingTime = 50;
            }
            public static void Wave14Update()
            {
                waitingTime -= 1;
                if (waveTime <= 720)
                    if (waitingTime <= 0)
                    {
                        waitingTime = 45 - Sqrt(waveTime) * 1.1f;
                        float rot = Rand(0, 44);
                        for (int i = 0; i < 8; i++)
                        {
                            CreateSpear(new SwarmSpear(Heart.Centre, 7.6f, 165, rot += 45, 52));
                            CreateSpear(new SwarmSpear(Heart.Centre, 7.6f, 165, rot + 6, 52));
                        }
                    }
                if (waveTime >= 770)
                {
                    Heart.RotateTo(0);
                    ChangeRound();
                }
            }
        }
    }
}