using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave13Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            public static void Wave13Update()
            {
                if (waveTime == 3)
                {
                    float time = 40;
                    if (IsFoolMode)
                    {
                        Fortimes(10, () =>
                        {
                            float det = 6;
                            CreateArrow(time += det, 0, 9.7f, 0, 0);
                            CreateArrow(time += det, 0, 9.7f, 1, 0);
                            CreateArrow(time += det, 1, 9.7f, 0, 0);
                            CreateArrow(time += det, 1, 9.7f, 1, 0);
                            CreateArrow(time += det, 2, 9.7f, 0, 0);
                            CreateArrow(time += det, 2, 9.7f, 1, 0);
                            CreateArrow(time += det, 3, 9.7f, 0, 0);
                            CreateArrow(time += det, 3, 9.7f, 1, 0);
                            CreateArrow(time += det, 2, 9.7f, 0, 1);
                            CreateArrow(time += det, 2, 9.7f, 1, 1);
                            CreateArrow(time += det, 1, 9.7f, 0, 1);
                            CreateArrow(time += det, 1, 9.7f, 1, 1);
                        });
                    }
                    else
                    {
                        Fortimes(15, () =>
                        {
                            float det = 4;
                            CreateArrow(time += det, 0, 9.7f, 0, 0);
                            CreateArrow(time += det, 0, 9.7f, 1, 0);
                            CreateArrow(time += det, 1, 9.7f, 0, 0);
                            CreateArrow(time += det, 1, 9.7f, 1, 0);
                            CreateArrow(time += det, 2, 9.7f, 0, 0);
                            CreateArrow(time += det, 2, 9.7f, 1, 0);
                            CreateArrow(time += det, 3, 9.7f, 0, 0);
                            CreateArrow(time += det, 3, 9.7f, 1, 0);
                            CreateArrow(time += det, 2, 9.7f, 0, 1);
                            CreateArrow(time += det, 2, 9.7f, 1, 1);
                            CreateArrow(time += det, 1, 9.7f, 0, 1);
                            CreateArrow(time += det, 1, 9.7f, 1, 1);
                        });
                    }
                }
                if (waveTime == 770)
                    SetSoul(0);
                if (waveTime >= 780)
                    ChangeRound();
            }
        }
    }
}