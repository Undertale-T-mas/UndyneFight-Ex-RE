using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave12Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            public static void Wave12Update()
            {
                if (waveTime == 3)
                {
                    float time = 50;
                    if (IsFoolMode)
                    {
                        Fortimes(46, () =>
                        {
                            CreateArrow(time, "R", 7.2f, 0, 0);
                            time += 9;
                        });
                    }
                    else
                    {
                        Fortimes(60, () =>
                        {
                            CreateArrow(time, "R", 7.2f, 0, 0);
                            time += 7;
                        });
                    }
                }
                if (waveTime == 440)
                {
                    float time = 40;
                    if (IsFoolMode)
                    {
                        Fortimes(27, () =>
                        {
                            CreateArrow(time, 2, 8.0f, 0, 2);
                            CreateArrow(time, 3, 8.0f, 1, 2);
                            time += 4;
                        });
                    }
                    else
                    {
                        Fortimes(27, () =>
                        {
                            CreateArrow(time, 2, 11.0f, 0, 2);
                            CreateArrow(time, 3, 11.0f, 1, 2);
                            time += 4;
                        });
                    }
                }
                if (waveTime == 614)
                    ChangeRound();
            }
        }
    }
}