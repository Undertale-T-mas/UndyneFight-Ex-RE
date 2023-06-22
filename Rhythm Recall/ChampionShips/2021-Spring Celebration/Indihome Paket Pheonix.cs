using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public class IndihomePaketPhoenix : IChampionShip
    {
        public IndihomePaketPhoenix()
        {
            Game.instance = new Game();
            divisionImformation = new SaveInfo("imf{");
            divisionImformation.PushNext(new SaveInfo("time:14,17"));
            divisionImformation.PushNext(new SaveInfo("date:2,9"));
            divisionImformation.PushNext(new SaveInfo("dif:2,5"));

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Normal);
            difficulties.Add("div.1", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionImformation => divisionImformation;
        public SaveInfo divisionImformation;

        IWaveSet IChampionShip.GameContent => Game.instance;

        public class Game : IWaveSet
        {
            public const float SingleBeat = 7.4f;
            public static int BeatTime(float x) => (int)(x * SingleBeat);

            public static Game instance;

            public string FightName => "Indihome Paket Phoenix";

            private class ThisImformation : SongImformation
            {
                public override string BarrageAuthor => "T-mas";

                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 10.5f),
                            new(Difficulty.ExtremePlus, 19.2f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 10.5f),
                            new(Difficulty.ExtremePlus, 18.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 16.9f),
                            new(Difficulty.ExtremePlus, 21.1f),
                        }
                    );
            }
            public SongImformation Attributes => new ThisImformation();

            public string Music => "Indihome";

            public void Easy()
            {
                throw new System.NotImplementedException();
            }

            public void Extreme()
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
                if (Gametime >= BeatTime(11) && Gametime <= BeatTime(134) && Gametime % BeatTime(4) == BeatTime(3.3f))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 3f, 15));
                    CreateBone(new UpBone(true, 3f, 120));
                }
                if (Gametime == BeatTime(140))
                {
                    SetSoul(0);
                    SetBox(290, 180, 180);
                }
                if (Gametime >= BeatTime(140) && Gametime <= BeatTime(256) && Gametime % BeatTime(8) == 0)
                {
                    CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(160, Rand(0, 359)), Heart.Centre, Vector2.One, 58, 16));
                }
                if (Gametime == BeatTime(256))
                {
                    SetBox(290, 320, 160);
                    SetSoul(2);
                }
                if (Gametime >= BeatTime(256) && Gametime <= BeatTime(384))
                {
                    if (Gametime % BeatTime(1) == 0 && Gametime >= BeatTime(260))
                    {
                        CreateBone(new DownBone(true, 3f, 3) { MarkScore = false });
                        CreateBone(new UpBone(false, 3f, 3) { MarkScore = false });
                    }
                    if (Gametime % BeatTime(8) == 0)
                    {
                        PlaySound(FightResources.Sounds.pierce);
                        if (Gametime >= BeatTime(264))
                            CreateBone(new DownBone(false, 4.6f, 152) { ColorType = 1 });
                        CreatePlatform(new Platform(1, new Vector2(115, 310), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { 2.5f, 0 }
                        });
                    }
                }
                if (Gametime >= BeatTime(384) && Gametime <= BeatTime(500))
                {
                    if (Gametime % BeatTime(1) == 0)
                    {
                        CreateBone(new DownBone(false, 3f, 3) { MarkScore = false });
                        CreateBone(new UpBone(true, 3f, 3) { MarkScore = false });
                    }
                    if (Gametime % BeatTime(8) == 0)
                    {
                        CreatePlatform(new Platform(0, new Vector2(525, 310), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { -2.5f, 0 }
                        });
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
                    }
                }
                if (Gametime == BeatTime(506))
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.Speed = 3.2f;
                    TP();
                }
                if (Gametime == BeatTime(502))
                {
                    float time = 12f * 7.18f;
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                    time += 4 * 7.18f;

                    int[] rhythms = { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                    int[] rhythms2 = { 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                    Fortimes(4, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms[i] == 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                            }
                            time += 2 * 7.18f;
                        });
                    });
                    Fortimes(4, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms2[i] == 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                            }
                            time += 2 * 7.18f;
                        });
                    });
                }
                if (Gametime == BeatTime(766))
                {
                    Heart.GiveForce(90, 6);
                    SetSoul(2);
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 5.0f, 73) { ColorType = 2 });
                    CreateBone(new UpBone(true, 5.0f, 73) { ColorType = 2 });
                }
                if (Gametime == BeatTime(780))
                {
                    SetBox(290, 180, 180);
                }
                if (Gametime >= BeatTime(780) && Gametime <= BeatTime(900) && Gametime % BeatTime(8) == BeatTime(7.7f))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    for (int i = 0; i < 2; i++)
                        CreateBone(new CustomBone(new Vector2(230, 230 + i * 120), Motions.PositionRoute.linear, 0, 56)
                        {
                            PositionRouteParam = new float[] { 4, 0 }
                        });
                    CreateBone(new CustomBone(new Vector2(410, 290), Motions.PositionRoute.linear, 0, 56)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                if (Gametime == BeatTime(902))
                {
                    SetBox(290, 260, 160);
                    Heart.GiveForce(0, 5);
                }
                if (Gametime >= BeatTime(907) && Gametime <= BeatTime(1020) && Gametime % BeatTime(4) == 0)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(true, 4.7f, 65));
                    CreateBone(new DownBone(true, 4.7f, 38));
                    CreateBone(new UpBone(true, 3.7f, 45));
                    CreateBone(new DownBone(true, 3.7f, 18));
                }

                if (Gametime == BeatTime(1021))
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.Speed = 3.2f;
                    TP();
                }
                if (Gametime == BeatTime(1016))
                {
                    float time = 12f * 7.18f;
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                    time += 4 * 7.18f;

                    int[] rhythms = { 1, 1, 1, 1, 1, 0, 1, 1 };

                    Fortimes(4, () =>
                    {
                        Fortimes(8, (i) =>
                        {
                            if (rhythms[i] == 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                            }
                            time += 2 * 7.18f;
                        });
                    });
                    Fortimes(4, () =>
                    {
                        Fortimes(8, (i) =>
                        {
                            if (rhythms[i] == 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                            }
                            time += 2 * 7.18f;
                        });
                    });

                }
                if (Gametime == BeatTime(1168))
                {
                    SetBox(260, 100, 280);
                    SetSoul(2);
                }
                if (Gametime == BeatTime(1172))
                {
                    CreateBone(new CustomBone(new Vector2(270, 260), Motions.PositionRoute.YAxisSin, 0, 276, BeatTime(120))
                    {
                        PositionRouteParam = new float[] { 0, 54, BeatTime(16), 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(370, 260), Motions.PositionRoute.YAxisSin, 0, 276, BeatTime(120))
                    {
                        PositionRouteParam = new float[] { 0, 54, BeatTime(16), 0 }
                    });
                }
                if (Gametime >= BeatTime(1172) && Gametime <= BeatTime(1284) && Gametime % BeatTime(5) == 0)
                {
                    CreatePlatform(new Platform(0, new Vector2(295, 120), Motions.PositionRoute.linear, 0, 50)
                    {
                        PositionRouteParam = new float[] { 0, 2.2f }
                    });
                }
                if (Gametime >= BeatTime(1180) && Gametime <= BeatTime(1284) && Gametime % BeatTime(8) == BeatTime(7.6f))
                {
                    CreateGB(new NormalGB(new Vector2(150, 400), Heart.Centre, Vector2.One, 0, 30, 50));
                }
                if (Gametime == BeatTime(1294))
                {
                    SetBox(290, 230, 160);
                }
                if (Gametime >= BeatTime(1300) && Gametime <= BeatTime(1410) && Gametime % BeatTime(8f / 3 * 2) == 0)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 4f, 65));
                    CreateBone(new DownBone(false, 4f, 38));
                    CreateBone(new UpBone(true, 3.4f, 45));
                    CreateBone(new DownBone(true, 3.4f, 18));
                }
                if (Gametime == BeatTime(1412))
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.Speed = 3.2f;
                    TP();
                }
                if (Gametime == BeatTime(1406))
                {
                    float time = 12f * 7.18f;
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                    time += 4 * 7.18f;

                    int[] rhythms = { 1, 1, 1, 1, 1, 1, 1, 1 };

                    Fortimes(2, () =>
                    {
                        Fortimes(4, () =>
                        {
                            Fortimes(8, (i) =>
                            {
                                if (rhythms[i] == 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                }
                                time += 2 * 7.18f;
                            });
                        });
                        Fortimes(4, () =>
                        {
                            Fortimes(8, (i) =>
                            {
                                if (rhythms[i] == 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                }
                                time += 2 * 7.18f;
                            });
                        });
                    });
                }
                float detlax = 1418.75f;
                if (Gametime == BeatTime(256 + detlax))
                {
                    SetBox(290, 320, 160);
                    SetSoul(2);
                }
                if (Gametime >= BeatTime(256 + detlax) && Gametime <= BeatTime(384 + detlax))
                {
                    if (Gametime % BeatTime(1) == 0 && Gametime >= BeatTime(260 + detlax))
                    {
                        CreateBone(new DownBone(true, 3f, 3) { MarkScore = false });
                        CreateBone(new UpBone(false, 3f, 3) { MarkScore = false });
                    }
                    if (Gametime % BeatTime(8) == 0)
                    {
                        PlaySound(FightResources.Sounds.pierce);
                        if (Gametime >= BeatTime(264))
                            CreateBone(new DownBone(false, 4.6f, 152) { ColorType = 1 });
                        CreatePlatform(new Platform(1, new Vector2(115, 310), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { 2.5f, 0 }
                        });
                    }
                }
                if (Gametime >= BeatTime(384 + detlax) && Gametime <= BeatTime(500 + detlax))
                {
                    if (Gametime % BeatTime(1) == 0)
                    {
                        CreateBone(new DownBone(false, 3f, 3) { MarkScore = false });
                        CreateBone(new UpBone(true, 3f, 3) { MarkScore = false });
                    }
                    if (Gametime % BeatTime(8) == 0)
                    {
                        CreatePlatform(new Platform(0, new Vector2(525, 310), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { -2.5f, 0 }
                        });
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
                    }
                }
                if (Gametime == BeatTime(506 + detlax))
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.Speed = 3.2f;
                    TP();
                }
                if (Gametime == BeatTime(502 + detlax))
                {
                    float time = 12f * 7.18f;
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                    time += 4 * 7.18f;

                    int[] rhythms = { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                    int[] rhythms2 = { 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                    Fortimes(4, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms[i] == 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                            }
                            time += 2 * 7.18f;
                        });
                    });
                    Fortimes(4, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms2[i] == 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                            }
                            time += 2 * 7.18f;
                        });
                    });

                    CreateArrow(time - 1, Rand(0, 3), 5.0f, 0, 0);
                    CreateArrow(time - 1, Rand(0, 3), 5.0f, 0, 0);
                }
            }

            public void ExtremePlus()
            {
                if ((int)(GametimeF * 2) != (int)(GametimeF) * 2) return;
                if (Gametime >= BeatTime(11) && Gametime <= BeatTime(134) && Gametime % BeatTime(4) == BeatTime(3.3f))
                {
                    //SetBox(290, 320, 160);
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 3f, 15));
                    CreateBone(new CustomBone(new Vector2(480, 210), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                    {
                        PositionRouteParam = new float[] { -3, 0 },
                        LengthRouteParam = new float[] { 20 * 2, BeatTime(8), Gametime * 2, 130 * 2 },
                        RotationRouteParam = new float[] { 0 }
                    });
                }
                if (Gametime == BeatTime(140))
                {
                    SetSoul(0);
                    SetBox(290, 180, 180);
                }
                if (Gametime >= BeatTime(140) && Gametime <= BeatTime(256) && Gametime % BeatTime(4) == 0)
                {
                    if (Gametime % (BeatTime(4) * 4) == 0)
                    {
                        CreateBone(new CustomBone(new Vector2(410, 200), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                        {
                            PositionRouteParam = new float[] { -3.6f, 0 },
                            LengthRouteParam = new float[] { 50 * 2, 60, Rand(0, 49), 88 * 2 },
                            RotationRouteParam = new float[] { 0 },
                            ColorType = 1
                        });
                        CreateBone(new CustomBone(new Vector2(410, 380), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                        {
                            PositionRouteParam = new float[] { -3.6f, 0 },
                            LengthRouteParam = new float[] { 50 * 2, 60, LastRand + 30, 88 * 2 },
                            RotationRouteParam = new float[] { 0 },
                            ColorType = 2
                        });
                        PlaySound(FightResources.Sounds.pierce);
                    }

                    if (Gametime % (BeatTime(4) * 4) == (BeatTime(4) * 2))
                    {
                        CreateBone(new CustomBone(new Vector2(230, 200), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                        {
                            PositionRouteParam = new float[] { 3.6f, 0 },
                            LengthRouteParam = new float[] { 50 * 2, 60, Rand(0, 49), 88 * 2 },
                            RotationRouteParam = new float[] { 0 },
                            ColorType = 2
                        });
                        CreateBone(new CustomBone(new Vector2(230, 380), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                        {
                            PositionRouteParam = new float[] { 3.6f, 0 },
                            LengthRouteParam = new float[] { 50 * 2, 60, LastRand + 30, 88 * 2 },
                            RotationRouteParam = new float[] { 0 },
                            ColorType = 1
                        });
                        PlaySound(FightResources.Sounds.pierce);
                    }
                    CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 6));
                }
                if (Gametime == BeatTime(256))
                {
                    SetBox(290, 320, 160);
                    SetSoul(2);
                }
                if (Gametime == BeatTime(262))
                {
                    CreateBone(new SwarmBone(70, 160, 0, 920 + BeatTime(79)) { ColorType = 2 });
                    CreateBone(new SwarmBone(70, 160, 40, 920 + BeatTime(79)) { ColorType = 2 });
                    CreateBone(new SwarmBone(70, 160, 80, 920 + BeatTime(79)) { ColorType = 2 });
                    CreateBone(new SwarmBone(70, 160, 120, 920 + BeatTime(79)) { ColorType = 2 });
                }
                if (Gametime >= BeatTime(256) && Gametime <= BeatTime(384))
                {
                    if (Gametime % BeatTime(1) == 0 && Gametime >= BeatTime(260))
                    {
                        CreateBone(new DownBone(true, 3f, 3) { MarkScore = false });
                        CreateBone(new UpBone(false, 3f, 3) { MarkScore = false });
                    }
                    if (Gametime % BeatTime(8) == 0)
                    {
                        PlaySound(FightResources.Sounds.pierce);
                        if (Gametime >= BeatTime(264))
                            CreateBone(new DownBone(false, 4.6f, 157) { ColorType = 1 });
                        CreatePlatform(new Platform(1, new Vector2(115, 310), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { 2.5f, 0 }
                        });
                    }
                }
                if (Gametime >= BeatTime(384) && Gametime <= BeatTime(500))
                {
                    if (Gametime % BeatTime(1) == 0)
                    {
                        CreateBone(new DownBone(false, 3f, 3) { MarkScore = false });
                        CreateBone(new UpBone(true, 3f, 3) { MarkScore = false });
                    }
                    if (Gametime % BeatTime(8) == 0)
                    {
                        CreatePlatform(new Platform(0, new Vector2(525, 310), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { -2.5f, 0 }
                        });
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
                    }
                }
                if (Gametime == BeatTime(506))
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.Speed = 3.2f;
                    TP();
                }
                if (Gametime == BeatTime(502))
                {
                    float time = 12f * SingleBeat;
                    CreateArrow(time - 4, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time - 4, Rand(0, 3), 6.0f, 1, 0);
                    CreateArrow(time - 4, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time - 4, Rand(0, 3), 6.0f, 1, 0);
                    time += 3.4f * SingleBeat;

                    int[] rhythms = { 1, 0, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                    int[] rhythms2 = { 1, 0, 2, 1, 1, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1 };
                    Fortimes(4, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                CreateArrow(time + ((rhythms[i] == 2) ? BeatTime(1) : 0), Rand(0, 3), 5.0f, 1, 0);
                            }
                            time += 1.95f * SingleBeat;
                        });
                    });
                    Fortimes(4, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms2[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                CreateArrow(time + ((rhythms2[i] == 2) ? BeatTime(1) : 0), Rand(0, 3), 5.0f, 1, 0);
                            }
                            time += 1.95f * SingleBeat;
                        });
                    });
                }
                if (Gametime == BeatTime(766))
                {
                    Heart.GiveForce(90, 6);
                    SetSoul(2);
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 5.0f, 73) { ColorType = 2 });
                    CreateBone(new UpBone(true, 5.0f, 73) { ColorType = 2 });
                }
                if (Gametime == BeatTime(780))
                {
                    SetBox(290, 180, 180);
                }
                if (Gametime >= BeatTime(780) && Gametime <= BeatTime(900) && Gametime % BeatTime(8) == BeatTime(7.7f))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    for (int i = 0; i < 2; i++)
                        CreateBone(new CustomBone(new Vector2(230, 230 + i * 120), Motions.PositionRoute.linear, 0, 56)
                        {
                            PositionRouteParam = new float[] { 4, 0 }
                        });
                    CreateBone(new CustomBone(new Vector2(410, 290), Motions.PositionRoute.linear, 0, 56)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                if (Gametime >= BeatTime(780) && Gametime <= BeatTime(900) && Gametime % BeatTime(16) == BeatTime(7.7f))
                {
                    for (int i = -2; i <= 2; i++)
                        CreateGB(new NormalGB(new Vector2(140, 290 + 60 * i), Heart.Centre, new Vector2(1, 0.4f), 0, BeatTime(8), 6));
                    for (int i = -2; i <= 3; i++)
                        CreateGB(new NormalGB(new Vector2(500, 260 + 60 * i), Heart.Centre, new Vector2(1, 0.4f), 180, BeatTime(16), 6));
                }
                if (Gametime == BeatTime(902))
                {
                    SetBox(290, 260, 160);
                    Heart.GiveForce(0, 5);
                }
                if (Gametime >= BeatTime(907) && Gametime <= BeatTime(1020) && Gametime % BeatTime(4) == 0)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(true, 5.7f, 69));
                    CreateBone(new DownBone(true, 5.7f, 42));
                    CreateBone(new UpBone(true, 4.7f, 49));
                    CreateBone(new DownBone(true, 4.7f, 22));
                }

                if (Gametime == BeatTime(1021))
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.Speed = 3.2f;
                    TP();
                }
                if (Gametime == BeatTime(1016))
                {
                    float time = 12.0f * SingleBeat;
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                    time += 3.5f * SingleBeat;

                    int[] rhythms = { 1, 1, 1, 1, 1, 0, 1, 1 };

                    Fortimes(4, () =>
                    {
                        Fortimes(8, (i) =>
                        {
                            if (rhythms[i] == 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                            }
                            time += 1.95f * SingleBeat;
                        });
                    });
                    Fortimes(4, () =>
                    {
                        Fortimes(8, (i) =>
                        {
                            if (rhythms[i] == 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                            }
                            time += 1.95f * SingleBeat;
                        });
                    });

                }
                if (Gametime == BeatTime(1168))
                {
                    SetBox(260, 100, 280);
                    SetSoul(2);
                }
                if (Gametime == BeatTime(1172))
                {
                    CreateBone(new CustomBone(new Vector2(270, 260), Motions.PositionRoute.YAxisSin, 0, 276, BeatTime(120))
                    {
                        PositionRouteParam = new float[] { 0, 58, BeatTime(16), 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(370, 260), Motions.PositionRoute.YAxisSin, 0, 276, BeatTime(120))
                    {
                        PositionRouteParam = new float[] { 0, 58, BeatTime(16), 0 }
                    });
                }
                if (Gametime >= BeatTime(1172) && Gametime <= BeatTime(1284) && Gametime % BeatTime(5) == 0)
                {
                    CreatePlatform(new Platform(0, new Vector2(320, 120), Motions.PositionRoute.YAxisSin, 0, 33)
                    {
                        PositionRouteParam = new float[] { 2.2f, 54, 92, Rand(0, 79) }
                    });
                }
                if (Gametime >= BeatTime(1180) && Gametime <= BeatTime(1284) && Gametime % BeatTime(8) == BeatTime(7.6f))
                {
                    CreateGB(new NormalGB(new Vector2(150, 400), Heart.Centre, Vector2.One, 0, 30, 50));
                    CreateGB(new NormalGB(new Vector2(150, 120), Heart.Centre, Vector2.One, 0, 30, 50));
                }
                if (Gametime == BeatTime(1294))
                {
                    SetBox(290, 230, 160);
                }
                if (Gametime >= BeatTime(1300) && Gametime <= BeatTime(1410) && Gametime % BeatTime(4f) == 0)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 4f, 45));
                    CreateBone(new DownBone(false, 4f, 33));
                    CreateBone(new UpBone(true, 3.4f, 35));
                    CreateBone(new DownBone(true, 3.4f, 13));
                }
                if (Gametime == BeatTime(1412))
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.Speed = 3.2f;
                    TP();
                }
                if (Gametime == BeatTime(1406))
                {
                    float time = 12.0f * SingleBeat;
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                    time += 3.5f * SingleBeat;

                    int[] rhythms = { 1, 1, 1, 1, 1, 1, 1, 1 };

                    Fortimes(2, () =>
                    {
                        Fortimes(4, () =>
                        {
                            Fortimes(8, (i) =>
                            {
                                if (rhythms[i] == 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                                }
                                time += 1.95f * SingleBeat;
                            });
                        });
                        Fortimes(4, () =>
                        {
                            Fortimes(8, (i) =>
                            {
                                if (rhythms[i] == 1)
                                {
                                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                                }
                                time += 1.95f * SingleBeat;
                            });
                        });
                    });
                }
                float detlax = 1418.75f;
                if (Gametime == BeatTime(256 + detlax))
                {
                    SetBox(290, 320, 160);
                    SetSoul(2);
                }
                if (Gametime == BeatTime(265 + detlax))
                {
                    CreateBone(new SwarmBone(70, 160, 0, 920 + BeatTime(75)) { ColorType = 2 });
                    CreateBone(new SwarmBone(70, 160, 40, 920 + BeatTime(75)) { ColorType = 2 });
                    CreateBone(new SwarmBone(70, 160, 80, 920 + BeatTime(75)) { ColorType = 2 });
                    CreateBone(new SwarmBone(70, 160, 120, 920 + BeatTime(75)) { ColorType = 2 });
                }
                if (Gametime >= BeatTime(259 + detlax) && Gametime <= BeatTime(384 + detlax))
                {
                    if (Gametime % BeatTime(1) == 0 && Gametime >= BeatTime(260 + detlax))
                    {
                        CreateBone(new DownBone(true, 3f, 3) { MarkScore = false });
                        CreateBone(new UpBone(false, 3f, 3) { MarkScore = false });
                    }
                    if (Gametime % BeatTime(8) == 0)
                    {
                        PlaySound(FightResources.Sounds.pierce);
                        if (Gametime >= BeatTime(264))
                            CreateBone(new DownBone(false, 4.6f, 152) { ColorType = 1 });
                        CreatePlatform(new Platform(1, new Vector2(115, 310), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { 2.5f, 0 }
                        });
                    }
                }
                if (Gametime >= BeatTime(384 + detlax) && Gametime <= BeatTime(500 + detlax))
                {
                    if (Gametime % BeatTime(1) == 0)
                    {
                        CreateBone(new DownBone(false, 3f, 3) { MarkScore = false });
                        CreateBone(new UpBone(true, 3f, 3) { MarkScore = false });
                    }
                    if (Gametime % BeatTime(8) == 0)
                    {
                        CreatePlatform(new Platform(0, new Vector2(525, 310), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { -2.5f, 0 }
                        });
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
                    }
                }
                if (Gametime == BeatTime(506 + detlax))
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.Speed = 3.2f;
                    TP();
                }
                if (Gametime == BeatTime(502 + detlax))
                {
                    float time = 12f * SingleBeat;
                    CreateArrow(time - 2, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time - 2, Rand(0, 3), 6.0f, 1, 0);
                    CreateArrow(time - 2, Rand(0, 3), 6.0f, 0, 0);
                    CreateArrow(time - 2, Rand(0, 3), 6.0f, 1, 0);
                    time += 3.5f * SingleBeat;

                    int[] rhythms = { 1, 0, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                    int[] rhythms2 = { 1, 0, 2, 1, 1, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1 };
                    Fortimes(4, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                CreateArrow(time + ((rhythms[i] == 2) ? BeatTime(1) : 0), Rand(0, 3), 5.0f, 1, 0);
                            }
                            time += 1.95f * SingleBeat;
                        });
                    });
                    Fortimes(4, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms2[i] >= 1)
                            {
                                CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                                CreateArrow(time + ((rhythms2[i] == 2) ? BeatTime(1) : 0), Rand(0, 3), 5.0f, 1, 0);
                            }
                            time += 1.95f * SingleBeat;
                        });
                    });
                }
            }

            public void Start()
            {
                HeartAttribute.MaxHP = (10);
                SetBox(290, 320, 160);
                SetSoul(2);
                Heart.Speed = 3.2f;
                Heart.Gravity = 7f;
                Heart.JumpSpeed = 5f;
                TP();
            }

            public void Noob()
            {
                throw new System.NotImplementedException();
            }
        };
    }
}