using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave0Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
                #region Arrows
                CreateArrow(45, "3", 5.6f, 0, 0);
                CreateArrow(65, "3", 5.6f, 0, 0);
                CreateArrow(85, "3", 5.6f, 0, 0);

                int type = IsFoolMode ? 0 : 2;
                CreateArrow(95, "+0", 6.4f, 0, type);
                CreateArrow(105, "+1", 6.4f, 0, type);
                CreateArrow(115, "+1", 6.4f, 0, type);
                CreateArrow(125, "+1", 6.4f, 0, type);
                CreateArrow(135, "+1", 6.4f, 0, type);
                CreateArrow(145, "+1", 6.4f, 0, type);
                CreateArrow(155, "+1", 6.4f, 0, type);
                CreateArrow(165, "+1", 6.4f, 0, type);
                CreateArrow(175, "+1", 6.4f, 0, type);
                CreateArrow(185, "+1", 6.4f, 0, type);
                CreateArrow(195, "+1", 6.4f, 0, type);
                CreateArrow(205, "+1", 6.4f, 0, type);

                CreateArrow(120, "$2", 7.2f, 1, 0);
                CreateArrow(120 + 10, "$2", 7.2f, 1, 1);
                CreateArrow(140, "$0", 7.2f, 1, 0);
                CreateArrow(140 + 10, "$0", 7.2f, 1, 1);
                CreateArrow(160, "$1", 7.2f, 1, 0);
                CreateArrow(160 + 10, "$1", 7.2f, 1, 1);
                CreateArrow(180, "$3", 7.2f, 1, 0);
                CreateArrow(180 + 10, "$3", 7.2f, 1, 1);
                CreateArrow(200, "$3", 7.2f, 1, 0);
                CreateArrow(200 + 10, "$3", 7.2f, 1, 1);

                float time = 213;
                Fortimes(6, () =>
                {
                    CreateArrow(time, 0, 14, 0, 0);
                    CreateArrow(time, 2, 14, 1, 0);
                    time += 3;
                });
                if (IsFoolMode)
                {
                    Fortimes(6, () =>
                    {
                        CreateArrow(time, 0, 8, 0, 1);
                        CreateArrow(time + 3, 2, 8, 1, 1);
                        time += 6;
                    });
                }
                else
                {
                    Fortimes(6, () =>
                    {
                        CreateArrow(time, 2, 8, 0, 1);
                        CreateArrow(time + 3, 0, 8, 1, 1);
                        time += 6;
                    });
                }
                #endregion
            }
            public static void Wave0Update()
            {
                if (waveTime == 340)
                {
                    ChangeRound();
                }
            }
        }
    }
}