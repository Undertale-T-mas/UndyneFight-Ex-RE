using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave1Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
                #region Arrows
                float time = 50;
                Fortimes(12, () =>
                {
                    int s;
                    CreateArrow(time, s = Rand(0, 1) * 2, 6.0f, 0, 0);
                    CreateArrow(time + Rand(8, 12), s, 6.0f, 0, 0);
                    CreateArrow(time, s = Rand(0, 1) * 2 + 1, 6.0f, 1, 0);
                    CreateArrow(time + Rand(8, 12), s, 6.0f, 1, 0);
                    time += Rand(25, 30);
                });
                #endregion
            }
            public static void Wave1Update()
            {
                if (waveTime >= 290 && GetAll<Arrow>().Length == 0)
                {
                    if (waveTime >= 400)
                        ChangeRound();
                    else
                    {
                        float time = 55;
                        Fortimes(14, () =>
                        {
                            CreateArrow(time, Rand(0, 1) * 2 + 1, 5.5f, 0, IsFoolMode ? 0 : 1);
                            time += Rand(18, 28);
                        });
                        time = 56;
                        Fortimes(10, () =>
                        {
                            CreateArrow(time, Rand(0, 3), 4.6f, 1, 2);
                            time += Rand(18, 28) * 14 / 10f;
                        });
                    }
                }
            }
        }
    }
}