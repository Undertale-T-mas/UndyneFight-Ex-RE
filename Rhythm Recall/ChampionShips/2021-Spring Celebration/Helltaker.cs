using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static Extends.LineMoveLibrary;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class HellTaker : IChampionShip
    {
        public HellTaker()
        {
            Game.instance = new Game();
            divisionImformation = new SaveInfo("imf{");
            divisionImformation.PushNext(new SaveInfo("time:9.5,12"));
            divisionImformation.PushNext(new SaveInfo("date:2,9"));
            divisionImformation.PushNext(new SaveInfo("dif:0,3"));

            difficulties = new()
            {
                { "div.2", Difficulty.Noob },
                { "div.1", Difficulty.Hard }
            };
        }

        private Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionImformation => divisionImformation;
        public SaveInfo divisionImformation;

        IWaveSet IChampionShip.GameContent => Game.instance;

        public class Game : IWaveSet
        {
            public const float SingleBeat = 6.25f;
            public static float BeatTime(float x) => x * SingleBeat;

            public static bool InBeat(float beat) => Gametime >= BeatTime(beat) && Gametime < BeatTime(beat) + 1;
            public static bool At0thBeat(float beatCount) => (int)(Gametime % BeatTime(beatCount)) == 0;
            public static bool AtKthBeat(float beatCount, float K) => (int)(Gametime % BeatTime(beatCount)) == (int)K;
            public static bool InBeat(float leftBeat, float rightBeat) => Gametime >= BeatTime(leftBeat) && Gametime <= BeatTime(rightBeat) + 1;

            public static Game instance;

            public string FightName => "Helltaker";

            public string Music => "Helltaker";

            class ThisImformation : SongImformation
            {
                public override string BarrageAuthor => "T-mas";
                public override string SongAuthor => "ROCK_2027";
                public override string Extra => "The song is unauthorized, and \nif that infringe a copyright, \ncall T-mas by email:480642510@qq.com, \nwe will delete this song immediately";
                public override Color ExtraColor => Color.Red;
                public override string PaintAuthor => "SRI GAMES";

                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 2.5f),
                            new(Difficulty.Hard, 14.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 2.5f),
                            new(Difficulty.Hard, 14.6f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 9.0f),
                            new(Difficulty.Hard, 20.6f),
                        }
                    );
            }
            public SongImformation Attributes => new ThisImformation();

            public void Easy()
            {
                throw new System.NotImplementedException();
            }

            public void Extreme()
            {
                throw new System.NotImplementedException();
            }

            public void Normal()
            {
                throw new System.NotImplementedException();
            }

            public void Noob()
            {
                if ((int)(GametimeF * 2) != (int)(GametimeF) * 2) return;
                if (InBeat(0))
                {
                    float time = BeatTime(4f);
                    Fortimes(47, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.4f, 0, 0);
                        time += BeatTime(4);
                    });
                }
                if (InBeat(192))
                {
                    SetBox(290, 140, 140);
                    SetSoul(0);
                }
                if (InBeat(193))
                {
                    CreateBone(new CustomBone(new Vector2(250, 290), Motions.PositionRoute.YAxisSin, 0, 133, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 76, BeatTime(16), 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(390, 290), Motions.PositionRoute.YAxisSin, 0, 133, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 76, BeatTime(16), 0 }
                    });
                }
                if (InBeat(320, 448 - 16) && At0thBeat(2))
                {
                    CreateGB(new NormalGB(GetVector2(150, Gametime * 2) + RectangleBox.instance.Centre, RectangleBox.instance.Centre, new Vector2(1.0f, 0.5f), (Gametime * 2 + 180), 40, 20));
                }
                if (InBeat(446))
                {
                    SetSoul(2);
                }
                if (InBeat(448, 514 - 6) && At0thBeat(4))
                {
                    CreateBone(new DownBone(false, 3, 25));
                    CreateBone(new UpBone(false, 3, 25));
                }
                if (InBeat(510))
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                if (InBeat(502))
                {
                    float time = BeatTime(10f);
                    float[,] rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1}
                    };
                    Fortimes(2, () =>
                    {
                        Fortimes(4, (i) =>
                        {
                            Fortimes(16, (j) =>
                            {
                                if (rhythms[i, j] == 1)
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                time += BeatTime(2.0f);
                            });
                        });
                    });
                    rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                    };
                    time += BeatTime(0.24f);
                    Fortimes(8, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms[0, i] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                            time += BeatTime(2.0f);
                        });
                    });
                }
                if (InBeat(1026))
                {
                    SetBox(290, 120, 120);
                    SetSoul(2);
                }
                if (InBeat(1024 + 8, 1024 + 256 - 8) && AtKthBeat(16, BeatTime(4)))
                {
                    int way = Rand(0, 4);
                    float angle = way * 90;
                    Heart.GiveForce(angle, 5f);
                    CreateGB(new NormalGB(new Vector2(260, 170), Heart.Centre, new Vector2(1.0f, 0.6f), 90, BeatTime(8), BeatTime(1)));
                    CreateGB(new NormalGB(new Vector2(380, 410), Heart.Centre, new Vector2(1.0f, 0.6f), 270, BeatTime(8), BeatTime(1)));
                    CreateGB(new NormalGB(new Vector2(200, 350), Heart.Centre, new Vector2(1.0f, 0.6f), 0, BeatTime(8), BeatTime(1)));
                    CreateGB(new NormalGB(new Vector2(440, 230), Heart.Centre, new Vector2(1.0f, 0.6f), 180, BeatTime(8), BeatTime(1)));
                }

                if (InBeat(1280))
                {
                    SetSoul(0);
                    Heart.RotateTo(0);
                    SetBox(290, 160, 160);
                }
                if (InBeat(1280 + 8, 1280 + 256 - 8) && AtKthBeat(8, 0))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 5.0f, 80));
                }
                if (InBeat(1280 + 8, 1280 + 256 - 8) && AtKthBeat(8, BeatTime(4)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 4.4f, 76));
                }

                if (InBeat(1536, 1536 + 64 - 8) && AtKthBeat(8, 0))
                {
                    CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, Vector2.One, BeatTime(8), 9));
                }

                if (InBeat(1598))
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                if (InBeat(1593f))
                {
                    float time = BeatTime(11 - 4f);

                    float[,] rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1}
                        };
                    Fortimes(2, () =>
                    {
                        Fortimes(4, (i) =>
                        {
                            Fortimes(16, (j) =>
                            {
                                if (rhythms[i, j] == 1)
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                time += BeatTime(2);
                            });
                        });
                    });
                    rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                        };
                    time += BeatTime(0.2f);
                    Fortimes(8, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms[0, i] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0);
                            time += BeatTime(2);
                        });
                    });
                }

                if (InBeat(2100f))
                {
                    float time = BeatTime(12);

                    float[,] rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1}
                        };
                    Fortimes(2, () =>
                    {
                        Fortimes(4, (i) =>
                        {
                            Fortimes(16, (j) =>
                            {
                                if (rhythms[i, j] == 1)
                                    CreateArrow(time, Rand(0, 3), 5, 0, 0);
                                time += BeatTime(2.0f);
                            });
                        });
                    });
                }
            }
            public Color ReturnColor()
            {
                return Rand(0, 2) == 0 ? Color.White : Color.Red;
            }
            public void Hard()
            {
                if ((int)(GametimeF * 2) != (int)(GametimeF) * 2) return;
                if (InBeat(0))
                {
                    float time = BeatTime(4f);
                    Fortimes(48 / 2 - 1, (i) =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.4f, 0, 0);
                        CreateArrow(time + BeatTime(2), LastRand, 6.4f, 0, 0);
                        AddInstance(new TimeRangedEvent(time, 1, () =>
                        {
                            Linerotate line = new(Rand(40, 600), 240, 270, 160, 0, ReturnColor());
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(BeatTime(1), 1, () =>
                            {
                                RotLerp(line, BeatTime(16), 270 + Extends.Someway.Rand0or1() * 16 + Rand(-1.0f, 4.0f), 0.1f);
                            }));
                            AlphaSin(line, BeatTime(11));
                        }));
                        CreateArrow(time + BeatTime(4), LastRand, 6.4f, 0, 0);
                        CreateArrow(time + BeatTime(4), Rand(0, 3), 6.4f, 1, 0);
                        CreateArrow(time + BeatTime(6), LastRand, 6.4f, 1, 0);
                        AddInstance(new TimeRangedEvent(time + BeatTime(4), 1, () =>
                        {
                            Linerotate line = new(Rand(40, 600), 240, 270, 160, 0, ReturnColor());
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(BeatTime(1), 1, () =>
                            {
                                RotLerp(line, BeatTime(16), 270 + Extends.Someway.Rand0or1() * 16 + Rand(-1.0f, 4.0f), 0.1f);
                            }));
                            AlphaSin(line, BeatTime(11));
                        }));
                        CreateArrow(time + BeatTime(8), LastRand, 6.4f, 1, 0);
                        time += BeatTime(8);

                    });
                }
                if (InBeat(23 * 8 - 16))
                {
                    CreateArrow(BeatTime(18), "R", 6.4f, 0, 0);
                    CreateArrow(BeatTime(20), "R", 6.4f, 0, 0);
                }
                if (InBeat(192))
                {
                    SetBox(290, 140, 140);
                    SetSoul(0);
                }
                if (InBeat(193))
                {
                    CreateBone(new CustomBone(new Vector2(250, 290), Motions.PositionRoute.YAxisSin, 0, 133, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 76, BeatTime(16), 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(390, 290), Motions.PositionRoute.YAxisSin, 0, 133, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 76, BeatTime(16), 0 }
                    });
                }
                if (InBeat(192 + 4, 320 - 4) && At0thBeat(2))
                {
                    float val = Sin(Gametime * 1.5f) * 40;
                    CreateBone(new DownBone(false, 2.7f, 44 + val));
                    CreateBone(new UpBone(false, 2.7f, 44 - val));
                }
                if (InBeat(320, 448 - 16) && At0thBeat(1))
                {
                    CreateGB(new NormalGB(GetVector2(150, Gametime * 4) + RectangleBox.instance.Centre, RectangleBox.instance.Centre, new Vector2(1.0f, 0.5f), (Gametime * 4 + 180), 40, 20));
                }
                if (InBeat(446))
                {
                    SetSoul(2);
                }
                if (InBeat(448, 514 - 6) && At0thBeat(4))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 3, 25));
                    CreateBone(new UpBone(false, 3, 25));
                }
                if (InBeat(448, 514 - 6) && At0thBeat(8))
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(0, 160), Heart.Centre, new Vector2(1.0f, 0.45f), 270, BeatTime(4), 14));

                if (InBeat(510))
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                if (InBeat(502f))
                {
                    float time = BeatTime(10);
                    float[,] rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 2, 1, 1, 1}
                    };
                    Fortimes(2, () =>
                    {
                        Fortimes(4, (i) =>
                        {
                            Fortimes(16, (j) =>
                            {
                                if (rhythms[i, j] >= 1)
                                    CreateArrow(time, Rand(0, 3), 6.4f, 0, 0);
                                if (rhythms[i, j] == 2)
                                    CreateArrow(time + BeatTime(1), Rand(0, 3), 6.4f, 0, 0);
                                if ((j & 1) == 1)
                                    CreateArrow(time, Rand(0, 3), 6.4f, 1, 0);
                                time += BeatTime(2);
                            });
                        });
                    });
                    rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                    };
                    Fortimes(8, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms[0, i] == 1)
                                CreateArrow(time, Rand(0, 3), 6.4f, 0, 0);
                            if ((i & 1) == 1)
                                CreateArrow(time, Rand(0, 3), 6.4f, 1, 0);
                            time += BeatTime(2);
                        });
                    });
                }
                if (InBeat(1026))
                {
                    SetBox(290, 120, 120);
                    SetSoul(2);
                }
                if (InBeat(1024 + 8, 1024 + 256 - 8) && AtKthBeat(8, BeatTime(4)))
                {
                    int way = Rand(0, 4);
                    float angle = way * 90;
                    Heart.GiveForce(angle, 5f);
                    CreateGB(new NormalGB(new Vector2(260, 170), Heart.Centre, new Vector2(1.0f, 1f), 90, BeatTime(6), BeatTime(1)));
                    CreateGB(new NormalGB(new Vector2(380, 410), Heart.Centre, new Vector2(1.0f, 1f), 270, BeatTime(6), BeatTime(1)));
                    CreateGB(new NormalGB(new Vector2(200, 350), Heart.Centre, new Vector2(1.0f, 1f), 0, BeatTime(6), BeatTime(1)));
                    CreateGB(new NormalGB(new Vector2(440, 230), Heart.Centre, new Vector2(1.0f, 1f), 180, BeatTime(6), BeatTime(1)));
                }

                if (InBeat(1280))
                {
                    SetSoul(0);
                    Heart.RotateTo(0);
                    SetBox(290, 160, 160);
                }
                if (InBeat(1280 + 8, 1280 + 256 - 8) && AtKthBeat(8, 0))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new DownBone(false, 6.0f, 40));
                    CreateBone(new UpBone(false, 6.0f, 80));
                }
                if (InBeat(1280 + 8, 1280 + 256 - 8) && AtKthBeat(8, BeatTime(4)))
                {
                    PlaySound(FightResources.Sounds.pierce);
                    CreateBone(new UpBone(false, 3.0f, 40));
                    CreateBone(new DownBone(false, 3.0f, 80));
                }

                if (InBeat(1536, 1536 + 64 - 8) && AtKthBeat(8, 0))
                {
                    CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, Vector2.One, BeatTime(8), 9));
                    CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, Vector2.One, BeatTime(8), 9));
                }

                if (InBeat(1598))
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                if (InBeat(1593f))
                {
                    float time = BeatTime(11f - 4);
                    float[,] rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 2, 1, 1, 1}
                    };
                    Fortimes(2, () =>
                    {
                        Fortimes(4, (i) =>
                        {
                            Fortimes(16, (j) =>
                            {
                                if (rhythms[i, j] >= 1)
                                    CreateArrow(time, Rand(0, 3), 6.4f, 0, 0);
                                if (rhythms[i, j] == 2)
                                    CreateArrow(time + BeatTime(1), Rand(0, 3), 6.4f, 0, 0);
                                if ((j & 1) == 1)
                                    CreateArrow(time, Rand(0, 3), 6.4f, 1, 0);
                                time += BeatTime(2);
                            });
                        });
                    });
                    rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                    };
                    Fortimes(8, () =>
                    {
                        Fortimes(16, (i) =>
                        {
                            if (rhythms[0, i] == 1)
                                CreateArrow(time, Rand(0, 3), 6.4f, 0, 0);
                            if ((i & 1) == 1)
                                CreateArrow(time, Rand(0, 3), 6.4f, 1, 0);
                            time += BeatTime(2);
                        });
                    });
                }

                if (InBeat(2100f))
                {
                    float time = BeatTime(12);
                    float[,] rhythms = new float[,] {
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 2, 1, 1, 1}
                    };
                    Fortimes(3, () =>
                    {
                        Fortimes(4, (i) =>
                        {
                            Fortimes(16, (j) =>
                            {
                                if (rhythms[i, j] >= 1)
                                    CreateArrow(time, Rand(0, 3), 6.4f, 0, 0);
                                if (rhythms[i, j] == 2)
                                    CreateArrow(time + BeatTime(1), Rand(0, 3), 6.4f, 0, 0);
                                if ((j & 1) == 1)
                                    CreateArrow(time, Rand(0, 3), 6.4f, 1, 0);
                                time += BeatTime(2);
                            });
                        });
                    });
                    CreateArrow(time, Rand(0, 3), 6.4f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 6.4f, 0, 0);
                }
            }

            public void Start()
            {
                GametimeDelta = -3f;
                Heart.Speed = 3.0f;
                SetGreenBox();
                SetSoul(1);
                HeartAttribute.MaxHP = (9);
                TP();
            }

            public void ExtremePlus()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}