using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave7Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            public static void Wave7Update()
            {
                if (waveTime == 5)
                {
                    float time = 40;
                    Fortimes(32, () =>
                    {
                        if (!IsFoolMode)
                        {
                            CreateArrow(time, "R", Rand(9, 14) / 2f + 2f, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(time, "D", Rand(9, 14) / 2f + 2f, 1, 0, ArrowAttribute.SpeedUp);
                        }
                        else
                        {
                            CreateArrow(time, "R", 5f, 0, 0);
                            CreateArrow(time, "D", 5f, 1, 0);
                        }
                        time += 18f;
                    });
                }
                if (waveTime == 32 * 18 + 50) ChangeRound();
            }
        }
    }
}