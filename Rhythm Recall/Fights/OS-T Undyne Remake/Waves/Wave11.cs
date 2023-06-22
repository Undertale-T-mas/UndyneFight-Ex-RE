using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave11Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
                #region Arrows
                if (IsFoolMode) return;
                CreateArrow(35, 0, 8, 0, 0);
                CreateArrow(35 + 7, 2, 8, 0, 1);
                CreateArrow(35 + 14, 0, 8, 0, 0);
                CreateArrow(35 + 21, 3, 4, 0, 0);
                CreateArrow(65, 0, 8, 1, 0);
                CreateArrow(65 + 7, 2, 8, 1, 1);
                CreateArrow(65 + 14, 2, 8, 1, 1);
                CreateArrow(65 + 21, "1", 4, 1, 0);
                #endregion
            }
            public static void Wave11Update()
            {
                if (IsFoolMode)
                {
                    if (waveTime == 10)
                    {
                        float time = 55;
                        Fortimes(11, () =>
                        {
                            CreateArrow(time, "+3", 9, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(time, "+2", 9, 1, 0, ArrowAttribute.SpeedUp);
                            time += 18;
                        });
                        Fortimes(10, () =>
                        {
                            CreateArrow(time, "+1", 7.5f, 0, 1, ArrowAttribute.RotateR);
                            CreateArrow(time, "+2", 7.5f, 1, 1, ArrowAttribute.RotateR);
                            time += 18;
                        });
                    }

                }
                else
                {
                    if (waveTime == 30)
                    {
                        float time = 55;
                        Fortimes(20, () =>
                        {
                            CreateArrow(time, "+3", 12.5f, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(time, "+2", 12.5f, 1, 0, ArrowAttribute.SpeedUp);
                            time += 9;
                        });
                        Fortimes(20, () =>
                        {
                            CreateArrow(time, "+1", 9.5f, 0, 1, ArrowAttribute.RotateR);
                            CreateArrow(time, "+2", 9.5f, 1, 1, ArrowAttribute.RotateR);
                            time += 9;
                        });
                    }
                }
                if (waveTime == 450)
                    ChangeRound();
            }
        }
    }
}