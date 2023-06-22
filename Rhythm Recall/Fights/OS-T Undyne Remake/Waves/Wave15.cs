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
            public static void Wave15Initialize()
            {
                SetBox(295, 160, 160);
                TP();
                SetSoul(0);
            }
            public static void Wave15Update()
            {
                if (waveTime <= 640)
                {
                    if (waveTime % (20 + (IsFoolMode ? 6 : 0)) == 0)
                    {
                        CreateSpear(new NormalSpear(Heart.Centre + GetVector2(156, Rand(0, 359))));
                    }
                    if (waveTime % 65 == 0)
                    {
                        Vector2 centre = Heart.Centre + GetVector2(177, Rand(0, 359));
                        ParticleGather v;
                        CreateEntity(v = new ParticleGather(centre, 14, 59, Color.White * 0.6f));

                        v.OnDispose += () =>
                        {
                            for (int i = 0; i < (IsFoolMode ? 6 : 9); i++)
                                CreateSpear(new Pike(centre, 1) { IsSpawnMute = true, Speed = Rand(-16, 30) / 5f + 4, Acceleration = 0.01f });
                        };

                        for (int i = 0; i < 2; i++)
                            CreateSpear(new NormalSpear(centre + GetVector2(5, Rand(0, 359))) { IsMute = true, DelayTargeting = true });
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