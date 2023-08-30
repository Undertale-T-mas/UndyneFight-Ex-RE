using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class NeverGonnaGiveYouUp : IChampionShip
    {
        public NeverGonnaGiveYouUp()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("time:14,17"));
            divisionInformation.PushNext(new SaveInfo("date:2,9"));
            divisionInformation.PushNext(new SaveInfo("dif:2,4"));

            difficulties = new()
            {
                { "div.2", Difficulty.Normal },
                { "div.1", Difficulty.Extreme }
            };
        }

        private Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        IWaveSet IChampionShip.GameContent => Game.instance;

        public class Game : IWaveSet
        {
            public const float SingleBeat = 8.27f;
            public static int BeatTime(float x) => (int)(x * SingleBeat);

            public static Game instance;

            public string FightName => "Never gonna give you up";

            class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => "T-mas";
                public override string SongAuthor => "Rick Astley";
                public override string PaintAuthor => "OtokP";

                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 9.5f),
                            new(Difficulty.Extreme, 16.9f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 9.0f),
                            new(Difficulty.Extreme, 16.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 16.0f),
                            new(Difficulty.Extreme, 19.8f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            public string Music => "Never Gonna Give You Up";

            public void Easy()
            {
                throw new System.NotImplementedException();
            }

            public void Noob()
            {
                throw new System.NotImplementedException();
            }

            public void Hard()
            {
                throw new System.NotImplementedException();
            }

            public void Normal()
            {
                if ((int)(GametimeF * 2) != (int)(GametimeF) * 2) return;
                if (Gametime == 2)
                {
                    float time = SingleBeat * 1.1f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time - SingleBeat, 0, 19.0f, 0, 0);
                        time += SingleBeat;
                    });
                    time += SingleBeat;
                    Rand(0, 3);
                    Fortimes(6, () =>
                    {
                        CreateArrow(time, LastRand, 6.0f, 0, 0);
                        time += SingleBeat;
                    });
                    time = 10.6f * SingleBeat;

                    int[] rhythm = { 1, 0, 0, 1, 0, 0, 0, 0 };
                    Fortimes(8, () =>
                    {
                        int i = 0;
                        Fortimes(8, () =>
                        {
                            if (rhythm[i] == 1)
                                CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                            else
                                CreateArrow(time, LastRand, 6.0f, 0, 0);
                            time += SingleBeat * 2;
                            i++;
                        });
                    });
                }
                if (Gametime == BeatTime(140))
                {
                    SetSoul(2);
                }
                if (Gametime >= (int)(140 * SingleBeat) && Gametime < BeatTime(260))
                {
                    float detlax = Sin(Gametime * 2) * 60;
                    SetBox(220 + detlax, 420 + detlax, 170f, 370);
                }
                if (Gametime >= BeatTime(140) && Gametime <= BeatTime(260) && Gametime % BeatTime(8) == BeatTime(0))
                {
                    Heart.GiveForce(Rand(0, 1) * 180, 5f);
                }
                if (Gametime >= BeatTime(140) && Gametime <= BeatTime(252) && Gametime % BeatTime(8) == BeatTime(4.5f))
                {
                    for (int i = 1; i <= 24; i++)
                        CreateBone(new SideCircleBone(i * 15, 2, 55f, 20f));
                }
                if (Gametime == BeatTime(264))
                {
                    Heart.GiveForce(0, 6f);
                    SetBox(270, 290, 164);
                }
                if (Gametime >= BeatTime(264) && Gametime <= BeatTime(318) && Gametime % BeatTime(4) == BeatTime(3))
                {





                    CreateBone(new DownBone(false, 5.0f, 20));
                    CreateBone(new DownBone(true, 5.0f, 20));
                    CreateBone(new UpBone(true, 5.0f, 100));
                    CreateBone(new UpBone(false, 5.0f, 100));
                }
                if (BeatTime(323) == Gametime)
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                if (BeatTime(311) == Gametime)
                {
                    float time = SingleBeat * 16f;
                    int[] rhythm = { 1, 1, 2, 2, 1, 0, 0, 0 };
                    int[] rhythm2 = { 1, 1, 2, 2, 1, 0, 1, 0, 1, 0, 0, 0 };

                    Fortimes(2, () =>
                    {
                        Fortimes(2, () =>
                        {
                            Fortimes(7, (i) =>
                            {
                                if (rhythm[i] >= 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                }
                                if (rhythm[i] == 2) time += SingleBeat;
                                time += SingleBeat * 2;
                                i++;
                            });
                        });

                        Fortimes(12, (i) =>
                        {
                            if (rhythm2[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                            }

                            time += SingleBeat * 2;

                            if (rhythm2[i] == 2)
                            {
                                time += SingleBeat * 2;
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                time += SingleBeat * 2;
                            }
                        });
                    });
                }
                if (Gametime == BeatTime(327 + 128))
                {
                    Heart.Speed = 3.1f;
                    SetSoul(2);
                    SetBox(310, 320, 160);
                }
                if (Gametime == BeatTime(330 + 128))
                {
                    CreatePlatform(new Platform(1, new Vector2(324, 340), Motions.PositionRoute.YAxisSin, 0, 72, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 102, BeatTime(32), 0 },
                        createWithScaling = true
                    });
                }
                if (Gametime >= BeatTime(460) && Gametime <= BeatTime(458 + 112) && Gametime % 6 == 0)
                {
                    CreateBone(new DownBone(false, 3.5f, 20) { MarkScore = false });
                    CreateBone(new UpBone(true, 3.5f, 20) { MarkScore = false });
                }
                if (Gametime >= BeatTime(460) && Gametime <= BeatTime(458 + 112) && Gametime % BeatTime(8) == 0)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    float base1 = Rand(0, 100);
                    CreateBone(new CustomBone(new Vector2(160, 390), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                    {
                        ColorType = 1,
                        PositionRouteParam = new float[] { 3.0f, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 70, 100, 0 + base1, 64 * 2 }
                    });
                    CreateBone(new CustomBone(new Vector2(160, 230), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                    {
                        ColorType = 1,
                        PositionRouteParam = new float[] { 3.0f, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 70, 100, 50 + base1, 42 * 2 }
                    });
                }
                if (Gametime == BeatTime(460 + 114))
                {
                    SetSoul(0);
                    SetBox(290, 200, 200);
                }
                if (Gametime >= BeatTime(582) && Gametime <= BeatTime(574 + 72) && Gametime % BeatTime(4) == 1)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 3.6f, 91));
                    CreateBone(new DownBone(true, 3.6f, 91));
                }
                if (BeatTime(643) == Gametime)
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                if (BeatTime(631) == Gametime)
                {
                    float time = SingleBeat * 16f;
                    int[] rhythm = { 1, 1, 2, 2, 1, 0, 0, 0 };
                    int[] rhythm2 = { 1, 1, 2, 2, 1, 0, 1, 0, 1, 0, 0, 0 };
                    Fortimes(4, (x) =>
                    {
                        Fortimes(2, (y) =>
                        {
                            Fortimes(7, (i) =>
                            {
                                if (rhythm[i] >= 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                    CreateArrow(time, LastRand, 5, 1, 0);
                                }
                                if (rhythm[i] == 2)
                                {
                                    time += SingleBeat;
                                    if (x % 2 == 1 && y == 1)
                                    {
                                        if (i == 2) time += SingleBeat;
                                        else time -= SingleBeat * 0.7f;
                                    }
                                }
                                if (x % 2 == 1 && y == 1 && i == 4) time -= SingleBeat * 0.3f;
                                time += SingleBeat * 2;
                                i++;
                            });
                        });

                        Fortimes(12, (i) =>
                        {
                            if (rhythm2[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                CreateArrow(time, LastRand, 5, 1, 0);
                            }

                            time += SingleBeat * 2;

                            if (rhythm2[i] == 2)
                            {
                                time += SingleBeat * 2;
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                CreateArrow(time, LastRand, 5, 1, 0);
                                time += SingleBeat * 2;
                            }
                        });
                    });
                }

                if (BeatTime(904) == Gametime)
                {
                    Heart.Speed = 3.1f;
                    SetSoul(0);
                    SetBox(310, 240, 200);
                }
                if (BeatTime(906) <= Gametime && Gametime <= BeatTime(906 + 126) && Gametime % BeatTime(1) == 0)
                {
                    float height = Sin(Gametime * 2) * 44 + 60;

                    CreateBone(new UpBone(false, 4.0f, height)
                    {
                        MarkScore = Gametime % BeatTime(2) == 0
                    });
                    CreateBone(new DownBone(false, 4.0f, 120 - height)
                    {
                        MarkScore = Gametime % BeatTime(2) == 0
                    });

                    if (Gametime % BeatTime(4) == BeatTime(3))
                    {
                        PlaySound(FightResources.Sounds.pierce);
                        CreateBone(new UpBone(false, 6.0f, 237)
                        {
                            ColorType = Rand(1, 2)
                        });
                    }
                }

                if (BeatTime(1032) == Gametime)
                {
                    Heart.Speed = 3.1f;
                    SetSoul(0);
                    SetBox(320, 180, 180);
                }
                if (Gametime >= BeatTime(1032) && Gametime <= BeatTime(1040 + 116) && Gametime % BeatTime(8) == 0)
                {
                    switch (Rand(0, 2))
                    {
                        case 0:
                            Fortimes(4, (x) =>
                            {
                                CreateGB(new NormalGB(FightBox.instance.Centre + GetVector2(160, 45 + x * 90), Heart.Centre, Vector2.One, x * 90 + 225, 48, 11));
                            });
                            break;
                        case 1:
                            Fortimes(4, (x) =>
                            {
                                CreateGB(new NormalGB(FightBox.instance.Centre + GetVector2(140, x * 90), Heart.Centre, Vector2.One, x * 90 + 180, 48, 11));
                            });
                            break;
                        case 2:
                            CreateGB(new NormalGB(new Vector2(240, 240), Heart.Centre, Vector2.One, 0, 48, 11));
                            CreateGB(new NormalGB(new Vector2(400, 400), Heart.Centre, Vector2.One, 180, 48, 11));
                            CreateGB(new NormalGB(new Vector2(400, 240), Heart.Centre, Vector2.One, 90, 48, 11));
                            CreateGB(new NormalGB(new Vector2(240, 400), Heart.Centre, Vector2.One, 270, 48, 11));
                            break;
                    }
                }
                if (Gametime == BeatTime(1162))
                {
                    SetSoul(2);
                    SetBox(310, 320, 160);
                }

                if (Gametime == BeatTime(1165))
                {
                    CreateBone(new CustomBone(new Vector2(160, 310), Motions.PositionRoute.YAxisSin, 0, 156, 410)
                    {
                        PositionRouteParam = new float[] { 0, 180, 200, 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(480, 310), Motions.PositionRoute.YAxisSin, 0, 156, 410)
                    {
                        PositionRouteParam = new float[] { 0, 180, 200, 0 }
                    });
                }

                if (Gametime == BeatTime(1167))
                {
                    for (int i = -2; i <= 2; i++)
                        CreateBone(new CustomBone(new Vector2(320 + i * 54, 390), (s) =>
                        {
                            float time = s.AppearTime;
                            return new Vector2(0, (float)System.Math.Pow(2, time - 410));
                        }, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                        {
                            LengthRouteParam = new float[] { 40, 120, 90, 45 },
                            RotationRouteParam = new float[] { 0 },
                        });
                }

                if (Gametime == BeatTime(1219))
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }

                if (BeatTime(1208) == Gametime)
                {
                    float time = SingleBeat * 15.2f;
                    int[] rhythm = { 1, 1, 2, 2, 1, 0, 0, 0 };
                    int[] rhythm2 = { 1, 1, 2, 2, 1, 0, 1, 0, 1, 0, 0, 0 };
                    Fortimes(6, (x) =>
                    {
                        Fortimes(2, (y) =>
                        {
                            Fortimes(7, (i) =>
                            {
                                if (rhythm[i] >= 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                    CreateArrow(time, LastRand, 5, 1, 0);
                                }
                                if (rhythm[i] == 2)
                                {
                                    time += SingleBeat;
                                    if (x % 2 == 1 && y == 1)
                                    {
                                        if (i == 2) time += SingleBeat;
                                        else time -= SingleBeat * 0.7f;
                                    }
                                }
                                if (x % 2 == 1 && y == 1 && i == 4) time -= SingleBeat * 0.3f;
                                time += SingleBeat * 2;
                                i++;
                            });
                        });

                        Fortimes(12, (i) =>
                        {
                            if (rhythm2[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                CreateArrow(time, LastRand, 5, 1, 0);
                            }

                            time += SingleBeat * 2;

                            if (rhythm2[i] == 2)
                            {
                                time += SingleBeat * 2;
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                CreateArrow(time, LastRand, 5, 1, 0);
                                time += SingleBeat * 2;
                            }
                        });
                    });
                }
            }

            public void Extreme()
            {
                if ((int)(GametimeF * 2) != (int)(GametimeF) * 2) return;
                if (Gametime == 2)
                {
                    float time = SingleBeat * 1.1f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time - SingleBeat, 0, 19.0f, 0, 0);
                        time += SingleBeat;
                    });
                    time += SingleBeat;
                    Rand(0, 3);
                    Fortimes(6, () =>
                    {
                        CreateArrow(time, LastRand, 6.0f, 0, 0);
                        time += SingleBeat;
                    });
                    time = 10.6f * SingleBeat;

                    int[] rhythm = { 1, 0, 0, 1, 0, 0, 0, 0 };
                    int[] rhythm1 = { 1, 0, 0, 1, 0, 0, 1, 0 };
                    int[] rhythm2 = { 1, 0, 0, 1, 0, 0, 2, 2 };
                    int[] rhythm3 = { 1, 0, 0, 1, 0, 2, 1, 2 };

                    List<int[]> rhythms = new() { rhythm1, rhythm2, rhythm1, rhythm3, rhythm1, rhythm2, rhythm1, rhythm3 };

                    Fortimes(8, (x) =>
                    {
                        int i = 0;
                        float tmp = time;
                        Fortimes(8, () =>
                        {
                            CreateArrow(time, (rhythm[i] == 1) ? Rand(0, 3) : LastRand, 6.0f, 0, 0);
                            time += SingleBeat * 2;
                            i++;
                        });
                        time = tmp;
                        i = 0;
                        Fortimes(8, () =>
                        {
                            if (rhythms[x][i] == 1)
                                CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                            if (rhythms[x][i] == 2)
                            {
                                CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                                CreateArrow(time + SingleBeat, Rand(0, 3), 6.0f, 1, 0);
                            }
                            time += SingleBeat * 2;
                            i++;
                        });
                    });
                }
                if (Gametime == BeatTime(140))
                {
                    Heart.Gravity = 9.3f;
                    SetSoul(2);
                }
                if (Gametime >= (int)(140 * SingleBeat) && Gametime < BeatTime(260))
                {
                    float detlax = Sin(Gametime * 2) * 70;
                    SetBox(220 + detlax, 420 + detlax, 170f, 370);
                }
                if (Gametime >= BeatTime(140) && Gametime <= BeatTime(260) && Gametime % BeatTime(8) == BeatTime(0))
                {
                    Heart.GiveForce(Rand(0, 1) * 180, 5f);
                }
                if (Gametime >= BeatTime(144) && Gametime <= BeatTime(252) && Gametime % BeatTime(8) == BeatTime(4.5f))
                {
                    for (int i = 1; i <= 20; i++)
                        CreateBone(new SideCircleBone(i * 18, -3, 165f, 20f));
                }
                if (Gametime == BeatTime(264))
                {
                    Heart.GiveForce(0, 6f);
                    SetBox(270, 290, 164);
                }
                if (Gametime >= BeatTime(264) && Gametime <= BeatTime(318) && Gametime % BeatTime(4) == BeatTime(3))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 5.0f, 20));
                    CreateBone(new DownBone(true, 5.0f, 20));
                    CreateBone(new UpBone(true, 5.0f, 100));
                    CreateBone(new UpBone(false, 5.0f, 100));
                }
                if (BeatTime(323) == Gametime)
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                if (BeatTime(311) == Gametime)
                {
                    float time = SingleBeat * 16f;
                    int[] rhythm = { 3, 3, 2, 2, 1, 0, 0, 0 };
                    int[] rhythm2 = { 3, 3, 2, 2, 1, 0, 1, 0, 1, 0, 0, 0 };

                    Fortimes(2, () =>
                    {
                        Fortimes(2, () =>
                        {
                            Fortimes(7, (i) =>
                            {
                                if (rhythm[i] >= 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                }
                                if
                                    (rhythm[i] == 3) CreateArrow(time + SingleBeat, Rand(0, 3), 5, 0, 0);
                                else
                                    CreateArrow(time, Rand(0, 3), 5, 1, 0);
                                if (rhythm[i] == 2) time += SingleBeat;
                                time += SingleBeat * 2;
                                i++;
                            });
                        });

                        Fortimes(12, (i) =>
                        {
                            if (rhythm2[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                            }
                            if (rhythm2[i] == 3) CreateArrow(time + SingleBeat, Rand(0, 3), 5, 0, 0);
                            else
                                CreateArrow(time, Rand(0, 3), 5, 1, 0);

                            time += SingleBeat * 2;

                            if (rhythm2[i] == 2)
                            {
                                time += SingleBeat * 2;
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                time += SingleBeat * 2;
                            }
                        });
                    });
                }
                if (Gametime == BeatTime(327 + 128))
                {
                    Heart.Speed = 3.1f;
                    SetSoul(2);
                    SetBox(310, 320, 160);
                }
                if (Gametime == BeatTime(330 + 128))
                {
                    CreatePlatform(new Platform(1, new Vector2(324, 340), Motions.PositionRoute.YAxisSin, 0, 72, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 102, BeatTime(32), 0 },
                        createWithScaling = true
                    });
                }
                if (Gametime >= BeatTime(460) && Gametime <= BeatTime(458 + 112) && Gametime % 6 == 0)
                {
                    CreateBone(new DownBone(false, 3.5f, 20) { MarkScore = false });
                    CreateBone(new UpBone(true, 3.5f, 20) { MarkScore = false });
                }
                if (Gametime >= BeatTime(460) && Gametime <= BeatTime(458 + 112) && Gametime % BeatTime(8) == 0)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    float base1 = Rand(0, 100);
                    CreateBone(new CustomBone(new Vector2(480, 230), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                    {
                        ColorType = 2,
                        PositionRouteParam = new float[] { -4.0f, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 90, 100, Rand(0, 99), 100 }
                    });
                    CreateBone(new CustomBone(new Vector2(160, 390), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                    {
                        ColorType = 1,
                        PositionRouteParam = new float[] { 3.0f, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 70, 100, 0 + base1, 68 * 2 }
                    });
                    CreateBone(new CustomBone(new Vector2(160, 230), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                    {
                        ColorType = 1,
                        PositionRouteParam = new float[] { 3.0f, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 70, 100, 50 + base1, 48 * 2 }
                    });
                }
                if (Gametime == BeatTime(460 + 114))
                {
                    SetSoul(0);
                    SetBox(290, 200, 200);
                }
                if (Gametime >= BeatTime(582) && Gametime <= BeatTime(574 + 72) && Gametime % BeatTime(4) == 1)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 5.6f, 97));
                    CreateBone(new DownBone(true, 5.6f, 97));
                }
                if (BeatTime(643) == Gametime)
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                if (BeatTime(631) == Gametime)
                {
                    float time = SingleBeat * 16f;
                    int[] rhythm = { 3, 3, 2, 2, 1, 0, 0, 0 };
                    int[] rhythm2 = { 3, 3, 2, 2, 1, 0, 1, 0, 1, 0, 0, 0 };
                    Fortimes(4, (x) =>
                    {
                        Fortimes(2, (y) =>
                        {
                            Fortimes(7, (i) =>
                            {
                                if (rhythm[i] >= 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                }
                                if
                                   (rhythm[i] == 3) CreateArrow(time + SingleBeat, Rand(0, 3), 5, 0, 0);
                                else
                                    CreateArrow(time, Rand(0, 3), 5, 1, 0);
                                if (rhythm[i] == 2)
                                {
                                    time += SingleBeat;
                                    if (x % 2 == 1 && y == 1)
                                    {
                                        if (i == 2) time += SingleBeat;
                                        else time -= SingleBeat * 0.7f;
                                    }
                                }
                                if (x % 2 == 1 && y == 1 && i == 4) time -= SingleBeat * 0.3f;
                                time += SingleBeat * 2;
                                i++;
                            });
                        });

                        Fortimes(12, (i) =>
                        {
                            if (rhythm2[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                            }
                            if (rhythm2[i] == 3) CreateArrow(time + SingleBeat, Rand(0, 3), 5, 0, 0);
                            else
                                CreateArrow(time, Rand(0, 3), 5, 1, 0);

                            time += SingleBeat * 2;

                            if (rhythm2[i] == 2)
                            {
                                time += SingleBeat * 2;
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                time += SingleBeat * 2;
                            }
                        });
                    });
                }

                if (BeatTime(904) == Gametime)
                {
                    Heart.Speed = 3.1f;
                    SetSoul(0);
                    SetBox(310, 240, 200);
                }
                if (BeatTime(906) <= Gametime && Gametime <= BeatTime(906 + 126) && Gametime % BeatTime(1) == 0)
                {
                    float height = Sin(Gametime * 2) * 44 + 65;

                    CreateBone(new UpBone(false, 4.0f, height)
                    {
                        MarkScore = Gametime % BeatTime(2) == 0
                    });
                    CreateBone(new DownBone(false, 4.0f, 130 - height)
                    {
                        MarkScore = Gametime % BeatTime(2) == 0
                    });

                    if (Gametime % BeatTime(4) == BeatTime(3))
                    {
                        PlaySound(FightResources.Sounds.pierce);
                        CreateBone(new UpBone(false, 7.0f, 237)
                        {
                            ColorType = Rand(1, 2)
                        });
                    }
                }

                if (BeatTime(1032) == Gametime)
                {
                    Heart.Speed = 3.1f;
                    SetSoul(0);
                    SetBox(320, 180, 180);
                }
                if (Gametime >= BeatTime(1032) && Gametime <= BeatTime(1040 + 116) && Gametime % BeatTime(8) == 0)
                {
                    switch (Rand(0, 2))
                    {
                        case 0:
                            Fortimes(4, (x) =>
                            {
                                CreateGB(new NormalGB(FightBox.instance.Centre + GetVector2(160, 45 + x * 90), Heart.Centre, Vector2.One, x * 90 + 225, 40, 11));
                            });
                            break;
                        case 1:
                            Fortimes(4, (x) =>
                            {
                                CreateGB(new NormalGB(FightBox.instance.Centre + GetVector2(140, x * 90), Heart.Centre, Vector2.One, x * 90 + 180, 40, 11));
                            });
                            break;
                        case 2:
                            CreateGB(new NormalGB(new Vector2(240, 240), Heart.Centre, Vector2.One, 0, 40, 11));
                            CreateGB(new NormalGB(new Vector2(400, 400), Heart.Centre, Vector2.One, 180, 40, 11));
                            CreateGB(new NormalGB(new Vector2(400, 240), Heart.Centre, Vector2.One, 90, 40, 11));
                            CreateGB(new NormalGB(new Vector2(240, 400), Heart.Centre, Vector2.One, 270, 40, 11));
                            break;
                    }
                }
                if (Gametime == BeatTime(1162))
                {
                    SetSoul(2);
                    SetBox(310, 320, 160);
                }

                if (Gametime == BeatTime(1165))
                {
                    CreateBone(new CustomBone(new Vector2(160, 310), Motions.PositionRoute.YAxisSin, 0, 156, 410)
                    {
                        screenC = new CollideRect(-300, -200, 1500, 1500),
                        PositionRouteParam = new float[] { 0, 230, 200, 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(480, 310), Motions.PositionRoute.YAxisSin, 0, 156, 410)
                    {
                        screenC = new CollideRect(-300, -200, 1500, 1500),
                        PositionRouteParam = new float[] { 0, 230, 200, 0 }
                    });
                }

                if (Gametime == BeatTime(1167))
                {
                    for (int i = -2; i <= 2; i++)
                        CreateBone(new CustomBone(new Vector2(320 + i * 57, 390), (s) =>
                        {
                            float time = s.AppearTime;
                            return new Vector2(0, (float)System.Math.Pow(2, time - 410));
                        }, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                        {
                            LengthRouteParam = new float[] { 40, 80, 60, 45 },
                            RotationRouteParam = new float[] { 0 },
                            screenC = new CollideRect(-550, -550, 1740, 1580)
                        });
                }

                if (Gametime == BeatTime(1219))
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }

                if (BeatTime(1208) == Gametime)
                {
                    float time = SingleBeat * 15.2f;
                    int[] rhythm = { 3, 3, 2, 2, 1, 0, 0, 0 };
                    int[] rhythm2 = { 3, 3, 2, 2, 1, 0, 1, 0, 1, 0, 0, 0 };
                    Fortimes(6, (x) =>
                    {
                        Fortimes(2, (y) =>
                        {
                            Fortimes(7, (i) =>
                            {
                                if (rhythm[i] >= 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                }
                                if
                                   (rhythm[i] == 3) CreateArrow(time + SingleBeat, Rand(0, 3), 5, 0, 0);
                                else
                                    CreateArrow(time, Rand(0, 3), 5, 1, 0);
                                if (rhythm[i] == 2)
                                {
                                    time += SingleBeat;
                                    if (x % 2 == 1 && y == 1)
                                    {
                                        if (i == 2) time += SingleBeat;
                                        else time -= SingleBeat * 0.7f;
                                    }
                                }
                                if (x % 2 == 1 && y == 1 && i == 4) time -= SingleBeat * 0.3f;
                                time += SingleBeat * 2;
                                i++;
                            });
                        });

                        Fortimes(12, (i) =>
                        {
                            if (rhythm2[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                            }
                            if (rhythm2[i] == 3) CreateArrow(time + SingleBeat, Rand(0, 3), 5, 0, 0);
                            else
                                CreateArrow(time, Rand(0, 3), 5, 1, 0);

                            time += SingleBeat * 2;

                            if (rhythm2[i] == 2)
                            {
                                time += SingleBeat * 2;
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                CreateArrow(time, LastRand, 5, 1, 0);
                                time += SingleBeat * 2;
                            }
                        });
                    });
                }
            }

            public void Start()
            {
                HeartAttribute.MaxHP = (6);
                SetGreenBox();
                SetSoul(1);
                TP();
            }

            public void ExtremePlus()
            {
                throw new System.NotImplementedException();
            }
        };
    }
}