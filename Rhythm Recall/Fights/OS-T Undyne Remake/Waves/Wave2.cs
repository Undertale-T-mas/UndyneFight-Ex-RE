using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave2Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
                #region Arrows 
                if (IsFoolMode)
                {
                    float time = 36;
                    CreateArrow(time += 5, "R", 8f, 0, 0);
                    Fortimes(7, () =>
                    {
                        CreateArrow(time += 5, "+0", 8f, 0, 0);
                    });
                    time = 36;
                    CreateArrow(time += 5, "D", 8f, 1, 0);
                    Fortimes(7, () =>
                    {
                        CreateArrow(time += 5, "+0", 8f, 1, 0);
                    });
                }
                else
                {
                    float time = 30;
                    CreateArrow(time += 5, "R", 9f, 0, 0);
                    Fortimes(9, () =>
                    {
                        CreateArrow(time += 5, "+0", 9f, 0, 0);
                    });
                    time = 30;
                    CreateArrow(time += 5, "D", 9f, 1, 0);
                    Fortimes(9, () =>
                    {
                        CreateArrow(time += 5, "+0", 9f, 1, 0);
                    });
                }
                #endregion
            }
            public static void Wave2Update()
            {
                if (waveTime == 20)
                {
                    float time = 56;
                    CreateArrow(time += 6, "$1", 9.7f, 0, 0);
                    Fortimes(20 - (IsFoolMode ? 1 : 0), () =>
                    {
                        CreateArrow(time += 6, "+2", 9.7f, 0, 0);
                    });
                }
                if (waveTime == 175)
                {
                    float time = 56;
                    CreateArrow(time += 7, "$3", 8.7f, 0, 1, ArrowAttribute.RotateR);
                    Fortimes(20 - (IsFoolMode ? 1 : 0), () =>
                    {
                        CreateArrow(time += 7, "+1", 8.7f, 0, 1, ArrowAttribute.RotateR);
                    });
                }
                if (waveTime == 325)
                {
                    float time = 56;
                    CreateArrow(time += 7, "$1", 9.7f, 0, 0);
                    Fortimes(20 - (IsFoolMode ? 1 : 0), () =>
                    {
                        CreateArrow(time += 7, "+2", 9.7f, 0, 0);
                    });
                }
                if (waveTime == 475)
                {
                    float time = 56;
                    CreateArrow(time += 7, "$3", 8.7f, 0, 1, ArrowAttribute.RotateR);
                    Fortimes(20 - (IsFoolMode ? 1 : 0), () =>
                    {
                        CreateArrow(time += 7, "+1", 8.7f, 0, 1, ArrowAttribute.RotateR);
                    });
                }
                if (waveTime == 631 + 56)
                {
                    SetSoul(0);
                }
                if (waveTime == 655 + 56)
                    ChangeRound();
            }
        }
    }
}