using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;
namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave16Initialize()
            {
                SetBox(250, 580, 275);
                TP();
                SetSoul(0);
            }
            public static void Wave16Update()
            {
                if (waveTime <= 566)
                {
                    if (waveTime % (15 + (IsFoolMode ? 5 : 0)) == 0)
                    {
                        CreateSpear(new Pike(Heart.Centre + GetVector2(999, waveTime * 2.0f), 2) { Speed = 6.4f - (IsFoolMode ? 1 : 0), Acceleration = 0.02f, IsSpawnMute = true, IsShootMute = true });
                    }
                    if (waveTime % (55 + (IsFoolMode ? 30 : 0)) == 0)
                    {
                        float v = Rand(0, 359) / 7f;
                        int sign = Rand(0, 1) * 2 - 1;
                        for (int i = 0; i < 7; i++)
                            CreateSpear(new CircleSpear(Heart.Centre, 3.0f * sign, 1.13f, 186, v + i * (360 / 7f), 0.018f));
                    }
                }
                if (waveTime >= 700)
                {
                    ChangeRound();
                }
            }
        }
    }
}