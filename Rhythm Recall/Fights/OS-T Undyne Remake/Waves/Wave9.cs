using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave9Initialize()
            {
                SetBox(250, 580, 275);
                TP();
                SetSoul(0);
            }
            public static void Wave9Update()
            {
                if (waveTime % (45 + (IsFoolMode ? 10 : 0)) == 0)
                {
                    float v = Rand(0, 45);
                    int sign = Rand(0, 1) * 2 - 1;
                    for (int i = 0; i < 8; i++)
                        CreateSpear(new CircleSpear(Heart.Centre, 4.3f * sign, 1.67f, 196, v + i * 45, 0.018f));
                }
                if (waveTime >= 750)
                {
                    ChangeRound();
                }
            }
        }
    }
}