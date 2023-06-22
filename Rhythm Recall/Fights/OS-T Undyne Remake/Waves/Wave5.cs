using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave5Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            public static void Wave5Update()
            {
                if (waveTime % 45 == 0 && waveTime <= 450)
                {
                    float time = 44;
                    Fortimes(9 - (IsFoolMode ? 1 : 0), () =>
                    {
                        CreateArrow(time, 3, 8, 0, 0);
                        CreateArrow(time += 5, 1, 8, 1, 1);
                    });
                    CreateArrow(time, Rand(0, 1) * 2, 5.6f, 0, 1);
                    CreateArrow(time, LastRand * 2 + 2, 5.6f, 1, 1);
                }
                if (waveTime == 560)
                    ChangeRound();
            }
        }
    }
}