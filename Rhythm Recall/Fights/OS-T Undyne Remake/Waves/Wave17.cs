using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave17Initialize()
            {
                SetBox(250, 580, 275);
                TP();
                SetSoul(0);
            }
            public static void Wave17Update()
            {
                if (waveTime <= 740)
                    if (waveTime % (48 + (IsFoolMode ? 10 : 0)) == 0)
                    {
                        float v = Rand(1, 360) / 9f;
                        int sign = Rand(0, 1) * 2 - 1;
                        for (int i = 0; i < 9; i++)
                            CreateSpear(new CircleSpear(Heart.Centre, 4.3f * sign, 1.6f, 196, v + i * (360 / 9f), 0.018f));
                    }
                if (waveTime >= 770)
                {
                    ChangeRound();
                }
            }
        }
    }
}