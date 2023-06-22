using Microsoft.Xna.Framework;
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
            public static void Wave3Initialize()
            {
                SetBox(295, 160, 160);
                TP();
                SetSoul(0);
            }
            private static void CreateSplitSpears(Vector2 position, int count)
            {
                float rot = Rand(0, 360) * 1.0f / count;
                for (int i = 0; i < count; i++)
                {
                    CreateSpear(new Pike(position, rot += 360 / count, 55) { Speed = 1.2f, Acceleration = 0.15f });
                }
            }
            private static void CreateSplitSpears2(Vector2 position, int count)
            {
                float rot = Rand(0, 360) * 1.0f / count;
                for (int i = 0; i < count; i++)
                {
                    float cur_rot = rot += 360 / count;
                    CreateSpear(new Pike(position + GetVector2(10, cur_rot + 90), cur_rot, 55) { Speed = 0.9f, Acceleration = 0.06f });
                }
            }
            private static void CreateSplitSpears2(Vector2 position, int count, int waitingTime)
            {
                float rot = Rand(0, 360) * 1.0f / count;
                for (int i = 0; i < count; i++)
                {
                    float cur_rot = rot += 360 / count;
                    CreateSpear(new Pike(position + GetVector2(10, cur_rot + 90), cur_rot, waitingTime) { Speed = 1.1f, Acceleration = 0.06f, DrawingColor = new Color(214, 194, 214) });
                }
            }
            public static void Wave3Update()
            {
                if (waveTime <= 510)
                {
                    if (waveTime >= 120 && waveTime <= 195 && waveTime % (IsFoolMode ? 20 : 15) == 0)
                    {
                        CreateSplitSpears(new Vector2(100, -30 + waveTime * 2.2f), 13);
                        CreateSplitSpears(new Vector2(540, -30 + waveTime * 2.2f), 13);
                    }
                    if (waveTime >= 295 && waveTime <= 395 && waveTime % (IsFoolMode ? 25 : 20) == 0)
                    {
                        CreateSplitSpears2(new Vector2(320 + (waveTime - 395) * 3f, 64), 15);
                        CreateSplitSpears2(new Vector2(320 - (waveTime - 395) * 3f, 64), 15);
                    }
                    if (waveTime % 25 == 0)
                    {
                        CreateSpear(new NormalSpear(Heart.Centre + GetVector2(156, Rand(0, 359))));
                    }
                }
                if (waveTime == 550) ChangeRound();
            }
        }
    }
}