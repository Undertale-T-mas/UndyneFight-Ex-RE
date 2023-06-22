using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public partial class EndlessTrail
    {
        private static partial class WaveLib
        {
            public static void Wave0Initialize()
            {
                InstantSetBox(290, 140, 140);
                InstantTP(320, 290);
            }
            public static void Wave0Update()
            {
                float beatLength = 9.35f;
                float cycleLength = beatLength * 16;
                float beat4Length = beatLength * 4;
                if (waveTime <= cycleLength * 4)
                {
                    int cyclePos = (int)(waveTime % cycleLength);
                    if (cyclePos == 5)
                    {
                        SetBox(290, 140, 140);
                        SetSoul(2);
                        float time = 64;
                        float timeDet = beatLength - 0.25f;
                        if (waveTime >= cycleLength * 3)
                        {
                            CreateArrow(time += timeDet, "R", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "R", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "R", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                        }
                        else
                        {
                            CreateArrow(time += timeDet, "R", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                            time += timeDet;
                            CreateArrow(time += timeDet, "R", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                            CreateArrow(time += timeDet, "+0", 4.5f, 0, 0);
                        }
                        PlaySound(Sounds.Ding);
                        Heart.GiveForce(180, 12);
                        CreateEntity(new Boneslab(180, 40, 32, 20));
                    }
                    if (cyclePos == 43)
                    {
                        PlaySound(Sounds.Ding);
                        Heart.GiveForce(0, 12);
                        CreateEntity(new Boneslab(0, 12, 10, 8));
                    }
                    if (cyclePos == 70)
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    }
                }

                if (waveTime > cycleLength * 4 && waveTime < cycleLength * 8)
                {
                    int cyclePos = (int)(waveTime % cycleLength);
                    if (cyclePos == 5 && waveTime <= cycleLength * 5)
                    {
                        SetSoul(0);
                        SetBox(290, 140, 140);
                    }
                    if ((int)(cyclePos % beat4Length) == 6)
                    {
                        PlaySound(Sounds.change);
                        CreateBone(new DownBone(false, 2, 65f));
                        CreateBone(new UpBone(true, 2, 65f));
                    }
                }

                if (waveTime > cycleLength * 8 && waveTime < cycleLength * 16)
                {
                    int cyclePos = (int)(waveTime % cycleLength);
                    if (waveTime % 5 == 0) CreateBone(new DownBone(true, 4f, 12));
                    if (waveTime % 5 == 0) CreateBone(new UpBone(false, 4f, 12));
                    if (cyclePos == 1 && waveTime < cycleLength * 9)
                    {
                        SetBox(290, 280, 160);
                        SetSoul(2);
                        Heart.GiveForce(0, 12);
                    }
                    if (cyclePos == (int)cycleLength - 4 && waveTime > cycleLength * 15)
                        foreach (var v in GetAll<Platform>())
                        {
                            v.RotationRoute = Motions.RotationRoute.linear;
                            v.RotationRouteParam = new float[] { Rand(-3, 3), 0f };
                            v.PositionRoute = Motions.PositionRoute.GravityDown;
                            v.PositionRouteParam = new float[] { 9f, Rand(-4, 4) / 2f, Rand(-6, 0) / 5f };
                            v.ResetTime();
                        }
                    if (cyclePos == 5)
                    {
                        CreatePlatform(new Platform(0, new Vector2(150, 330), Motions.PositionRoute.linear, 0, 42)
                        {
                            PositionRouteParam = new float[] { 2.2f, 0 }
                        });
                    }
                    if (cyclePos == (int)(5 + 2 * beat4Length))
                    {
                        CreatePlatform(new Platform(1, new Vector2(490, 276), Motions.PositionRoute.XAxisSin, 0, 45)
                        {
                            PositionRouteParam = new float[] { -1.5f, 22, cycleLength, 0 }
                        });
                    }
                    if (cyclePos == (int)(5 + 3 * beat4Length))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 6, 77));
                        CreateBone(new UpBone(false, 6, 77) { ColorType = 1 });
                    }
                }

                if (waveTime > cycleLength * 16 && waveTime <= cycleLength * 24)
                {

                }

                if (waveTime == 9500)
                {
                    ChangeRound();
                }
            }
        }
    }
}