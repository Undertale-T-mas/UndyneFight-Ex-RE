using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave6Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            public static void Wave6Update()
            {
                if (waveTime % (24 * 8 + 17 * 5) == 5 && waveTime <= 900)
                {
                    float time = 44;
                    Fortimes(24 - (IsFoolMode ? 1 : 0), (x) =>
                    {
                        CreateArrow(time += 8, "R", 6.6f - (IsFoolMode ? 1 : 0), x % 2, 0);
                    });
                    if (IsFoolMode) time += 4;
                    time += 5;
                    Fortimes(8, () =>
                    {
                        CreateArrow(time, 0, 10, 0, 0, ArrowAttribute.RotateR);
                        CreateArrow(time += 5, 2, 10, 1, 0, ArrowAttribute.RotateR);
                    });
                    Fortimes(8, () =>
                    {
                        CreateArrow(time, 1, 10, 0, 0, ArrowAttribute.RotateR);
                        CreateArrow(time += 5, 3, 10, 1, 0, ArrowAttribute.RotateR);
                    });
                }
                if (waveTime == 1155)
                    ChangeRound();
            }
        }
    }
}