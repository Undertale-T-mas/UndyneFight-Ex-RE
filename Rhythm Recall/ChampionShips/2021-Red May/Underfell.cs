using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class UnderFell : IChampionShip
    {
        public UnderFell()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("time:13.75,15.50"));
            divisionInformation.PushNext(new SaveInfo("date:5,2"));
            divisionInformation.PushNext(new SaveInfo("dif:2,5"));

            difficulties = new()
            {
                { "div.2", Difficulty.Normal },
                { "div.1", Difficulty.ExtremePlus }
            };
        }

        private Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {

            public Game() : base(6.245f)
            { }

            public static Game instance;

            public string Music => "Underfell Undyne";

            public string FightName => "Underfell";

            class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => "T-mas";
                public override string SongAuthor => "Joandr861";

                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 12.5f),
                            new(Difficulty.ExtremePlus, 19.4f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 12.6f),
                            new(Difficulty.ExtremePlus, 19.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 18.0f),
                            new(Difficulty.ExtremePlus, 21.3f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            #region Non-ChampionShip
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
            public void Extreme()
            {
                throw new System.NotImplementedException();
            }
            #endregion

            public void Normal()
            {
                if (InBeat(12))
                {
                    float time = BeatTime(20);
                    int[] lis = { 0, 2, 0, 2, 3 };
                    Fortimes(16, (x) =>
                    {
                        CreateArrow(time, lis[x % 5], 5.0f, x % 2, 0);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(12 + 32))
                {
                    float time = BeatTime(20);
                    int[] lis = { 0, 2, 0, 2, 3 };
                    Fortimes(32, (x) =>
                    {
                        CreateArrow(time, lis[(x + 16) % 5], 5.0f, x % 2, 1);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(129))
                {
                    SetSoul(0);
                    SetBox(280, 140, 140);
                }
                if (InBeat(128, 192 - 12) && At0thBeat(8))
                {
                    CreateSpear(new NormalSpear(Heart.Centre + GetVector2(180, Rand(0, 359))));
                    CreateSpear(new NormalSpear(Heart.Centre + GetVector2(180, Rand(0, 359))));
                    CreateSpear(new NormalSpear(Heart.Centre + GetVector2(180, Rand(0, 359))));
                }
                if (InBeat(192))
                {
                    SetBox(290, 84, 84);
                    TP(320, 290);
                    CreateSpear(new Pike(new Vector2(320 - 64, 290), 0, BeatTime(60f)) { IsHidden = true });
                    CreateSpear(new Pike(new Vector2(320 + 64, 290), 180, BeatTime(60f)) { IsHidden = true });
                    CreateSpear(new Pike(new Vector2(320, 290 - 64), 90, BeatTime(60f)) { IsHidden = true });
                    CreateSpear(new Pike(new Vector2(320, 290 + 64), 270, BeatTime(60f)) { IsHidden = true });
                    for (int i = 0; i < 2; i++)
                    {
                        CreateSpear(new Pike(new Vector2(260, 260 + i * 15), 0, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(290 + i * 15, 290 - 60), 90, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(380, 260 + i * 15), 180, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(290 + i * 15, 290 + 60), 270, BeatTime(56 + i)) { IsHidden = true });
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        CreateSpear(new Pike(new Vector2(350 - i * 15, 290 - 60), 90, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(350 - i * 15, 290 + 60), 270, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(260, 320 - i * 15), 0, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(380, 320 - i * 15), 180, BeatTime(56 + i)) { IsHidden = true });
                    }
                }
                if (InBeat(192 + 4, 256 - 20) && At0thBeat(8))
                {
                    switch (Rand(0, 3))
                    {
                        case 0:
                            CreateSpear(new Pike(new Vector2(245, 290), 0, BeatTime(8)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(245, 270), 0, BeatTime(8)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 1:
                            CreateSpear(new Pike(new Vector2(395, 290), 180, BeatTime(8)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(395, 310), 180, BeatTime(8)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 2:
                            CreateSpear(new Pike(new Vector2(320, 215), 90, BeatTime(8)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(340, 215), 90, BeatTime(8)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 3:
                            CreateSpear(new Pike(new Vector2(320, 365), 270, BeatTime(8)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(300, 365), 270, BeatTime(8)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                    }
                }
                if (InBeat(256))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }

                if (InBeat(248))
                {
                    float time = BeatTime(12.3f);
                    Fortimes(30, (x) =>
                    {
                        CreateArrow(time, Rand(0, 3), 5, Rand(0, 1), 0);
                        time += BeatTime(2);
                    });
                }

                if (InBeat(320))
                {
                    SetBox(270, 400, 240);
                    SetSoul(0);
                }
                if (InBeat(320, 320 + 120 - 8) && At0thBeat(4))
                {
                    float rot = Rand(0, 359);
                    for (int i = 0; i < 8; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 5.0f, 155, rot, BeatTime(8)));
                        rot += 45;
                    }
                }
                if (InBeat(440))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(448 - 8))
                {
                    float time = BeatTime(8.5f);
                    int[] t = { 1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 1, 1, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 0, 1, 0, 0, 0,
                                1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 1, 0, 0, 0};
                    Fortimes(64, (x) =>
                    {
                        if (t[x] == 1)
                            CreateArrow(time, Rand(0, 3), 4.5f, 1, 0);
                        if (x % 4 == 0)
                        {
                            CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                            CreateArrow(time + BeatTime(2), LastRand, 6.0f, 0, 0);
                            CreateArrow(time + BeatTime(4), LastRand, 6.0f, 0, 0);
                            CreateArrow(time + BeatTime(6), LastRand, 6.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                }
                if (InBeat(576))
                {
                    SetSoul(0);
                    TP(320, 270);
                    SetBox(270, 100, 78);
                }

                if (InBeat(576, 576 + 128 - 16) && At0thBeat(4))
                {
                    CreateSpear(new Pike(new Vector2(320 + Rand(-2, 2) * 20, 270 - 67), 90, BeatTime(8)) { IsHidden = true });
                    CreateSpear(new Pike(new Vector2(320 + Rand(-2, 2) * 20, 270 + 67), 270, BeatTime(8)) { IsHidden = true });
                }
                if (InBeat(704 - 8))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(704 - 8))
                {
                    float time = BeatTime(8.25f);
                    int[] t = { 1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 1, 1, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 0, 0};
                    Fortimes(64, (x) =>
                    {
                        if (t[x] == 1)
                            CreateArrow(time, Rand(0, 3), 6, 1, 0);
                        if (x % 2 == 1)
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(832))
                {
                    SetSoul(0);
                    SetBox(270, 400, 240);
                }
                if (InBeat(832, 832 + 128 - 17) && At0thBeat(8))
                {
                    float rot = Rand(0, 44);
                    for (int i = 0; i < 8; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 6.6f, 155, rot += 45, BeatTime(8)));
                    }
                    rot -= 13f;
                    for (int i = 0; i < 8; i++)
                    {
                        rot += 22.5f;
                        CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(90, rot + 90), 7.6f, 185, rot, BeatTime(8)));
                        rot += 22.5f;
                        CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(110, rot - 90), 7.6f, 185, rot, BeatTime(8)));
                    }
                }

                if (InBeat(960 - 8))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(960 - 8))
                {
                    float time = BeatTime(8.5f);
                    int[] t = { 1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 1, 1, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 0, 0};
                    Fortimes(64, (x) =>
                    {
                        if (t[x] == 1)
                            CreateArrow(time, Rand(0, 3), 6, 1, 0);
                        if (x % 2 == 1)
                            CreateArrow(time, (x % 4) - 1, 5.0f, 0, 1);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(1088))
                {
                    SetSoul(0);
                    SetBox(265, 560, 280);
                }
                if (InBeat(1088, 1088 + 12 * 8 - 4 - 12) && AtKthBeat(12, BeatTime(8)))
                {
                    float rot = Rand(0, 359);
                    for (int i = 0; i < 9; i++)
                    {
                        CreateSpear(new CircleSpear(Heart.Centre, 4f, 1.36f, 187, rot += 360 / 9f, 0.02f));
                    }
                }
                if (InBeat(1184 - 12))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1184 - 12))
                {
                    float time = BeatTime(12.5f);
                    int[] t = { 1, 1, 1, 1, 1, 1,
                                1, 0, 1, 1, 1, 1,
                                1, 0, 1, 0, 1, 0,
                                1, 0, 1, 0, 1, 0,
                                1, 1, 1, 1, 1, 1,
                                1, 0, 1, 1, 1, 1,
                                1, 0, 0, 1, 0, 1,
                                1, 0, 1, 0, 1, 0};
                    Fortimes(48, (x) =>
                    {
                        int y = Rand(0, 3);
                        CreateArrow(time, y, 6.0f, 0, 0);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(1280))
                {
                    SetBox(270, 400, 240);
                    SetSoul(0);
                }
                if (InBeat(1280, 1280 + 12 * 8 - 18) && At0thBeat(4))
                {
                    float rot = Rand(0, 359);
                    for (int i = 0; i < 6; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 7.6f, 142, rot, BeatTime(6f)));
                        rot += 60;
                    }
                }
                if (InBeat(1376 - 8))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1376 - 18))
                {
                    float time = BeatTime(19f);
                    int[] t = { 1, 1, 1, 1, 1, 1,
                                1, 0, 1, 1, 1, 1,
                                1, 0, 1, 0, 1, 0,
                                1, 0, 1, 0, 1, 0,
                                1, 0, 0, 1, 1, 1,
                                1, 0, 1, 0, 1, 0,
                                1, 0, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0};
                    Fortimes(48, (x) =>
                    {
                        int y = Rand(0, 3);
                        if (t[x] == 1)
                            CreateArrow(time, y, 6.0f, 1, 0);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(1473))
                {
                    SetBox(240, 560, 300);
                    SetSoul(0);
                }
                if (InBeat(1473, 1473 + 8) && At0thBeat(0.5f))
                {
                    for (int i = 1; i <= 4; i++)
                        CreateSpear(new Pike(new Vector2(640 / 5 * i, 50), (Gametime - BeatTime(1743)) / BeatTime(0.5f) * (360 / 16f), BeatTime(8))
                        {
                            IsSpawnMute = i == 4
                        });
                }
                if (InBeat(1473 + 24, 1473 + 8 + 24) && At0thBeat(0.5f))
                {
                    for (int i = 1; i <= 4; i++)
                        CreateSpear(new Pike(new Vector2(640 / 5 * i, 430), (Gametime - BeatTime(1743)) / BeatTime(0.5f) * (360 / 16f), BeatTime(8))
                        {
                            IsSpawnMute = i == 4
                        });
                }
                if (InBeat(1473 + 48, 1473 + 8 + 48) && At0thBeat(0.5f))
                {
                    for (int i = 1; i <= 4; i++)
                        CreateSpear(new Pike(new Vector2(640 / 5 * i, 50), (Gametime - BeatTime(1743)) / BeatTime(0.5f) * (360 / 16f), BeatTime(8))
                        {
                            IsSpawnMute = i == 4
                        });
                }
                if (InBeat(1473 + 72, 1473 + 8 + 72) && At0thBeat(0.5f))
                {
                    for (int i = 1; i <= 4; i++)
                        CreateSpear(new Pike(new Vector2(640 / 5 * i, 430), (Gametime - BeatTime(1743)) / BeatTime(0.5f) * (360 / 16f), BeatTime(8))
                        {
                            IsSpawnMute = i == 4
                        });
                }

                if (InBeat(1569 - 2, 1569 + 48 - 14) && AtKthBeat(4, BeatTime(3)))
                {
                    float rot = Rand(0, 359);
                    for (int i = 0; i < 8; i++)
                    {
                        CreateSpear(new CircleSpear(Heart.Centre, 3f, 1.66f, 187, rot += 45, 0.018f));
                    }
                }
                if (InBeat(1569 + 48 - 13.5f))
                {
                    float rot = LastRand;
                    for (int i = 0; i < 6; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 13.6f, 192, rot, BeatTime(8f)));
                        rot += 60;
                    }
                }
                if (InBeat(1569 + 48 - 14))
                {
                    float rot = Rand(0, 359);
                    for (int dis = 1200; dis <= 1800; dis += 100)
                        for (int i = 0; i < 9; i++)
                        {
                            CreateSpear(new SwarmSpear(Heart.Centre, 23.6f + dis / 100f, dis, rot, BeatTime(8f)));
                            rot += 60;
                        }
                }
                if (InBeat(1569 + 48 - 2, 1569 + 48))
                {
                    SetBox(290, 140, 140);
                    SetSoul(Rand(0, 3));
                }
                if (InBeat(1569 + 48 + 0.2f))
                {
                    SetSoul(0);
                }
                if (InBeat(1569 + 48 - 4, 1569 + 96 - 12) && At0thBeat(1))
                {
                    float time = Gametime * 5f;
                    CreateSpear(new SwarmSpear(new Vector2(320, 290), 7.6f, 140, time, BeatTime(7)));
                    CreateSpear(new SwarmSpear(new Vector2(320, 290), 7.6f, 140, time + 180, BeatTime(7)));
                }
            }

            public void ExtremePlus()
            {
                if (InBeat(12))
                {
                    float time = BeatTime(20);
                    int[] lis = { 0, 2, 0, 2, 3 };
                    Fortimes(32, (x) =>
                    {
                        CreateArrow(time, lis[x % 5], 8.0f, (x % 10 >= 5) ? 1 : 0, 0);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(12 + 32))
                {
                    float time = BeatTime(20);
                    int[] lis = { 0, 2, 0, 2, 3 };
                    Fortimes(64, (x) =>
                    {
                        CreateArrow(time, lis[(x + 32) % 5], 8.0f, ((x + 32) % 10 >= 5) ? 1 : 0, 1);
                        time += BeatTime(1);
                    });
                }
                if (InBeat(129))
                {
                    SetSoul(0);
                    SetBox(280, 140, 140);
                }
                if (InBeat(128, 192 - 24) && At0thBeat(4))
                {
                    CreateSpear(new NormalSpear(Heart.Centre + GetVector2(180, Rand(0, 359))));
                    CreateSpear(new NormalSpear(Heart.Centre + GetVector2(160, Rand(0, 359))));
                    CreateSpear(new NormalSpear(Heart.Centre + GetVector2(200, Rand(0, 359))));
                }
                if (InBeat(192))
                {
                    SetBox(290, 84, 84);
                    TP(320, 290);
                    CreateSpear(new Pike(new Vector2(320 - 64, 290), 0, BeatTime(60f)) { IsHidden = true });
                    CreateSpear(new Pike(new Vector2(320 + 64, 290), 180, BeatTime(60f)) { IsHidden = true });
                    CreateSpear(new Pike(new Vector2(320, 290 - 64), 90, BeatTime(60f)) { IsHidden = true });
                    CreateSpear(new Pike(new Vector2(320, 290 + 64), 270, BeatTime(60f)) { IsHidden = true });
                    for (int i = 0; i < 2; i++)
                    {
                        CreateSpear(new Pike(new Vector2(260, 260 + i * 15), 0, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(290 + i * 15, 290 - 60), 90, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(380, 260 + i * 15), 180, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(290 + i * 15, 290 + 60), 270, BeatTime(56 + i)) { IsHidden = true });
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        CreateSpear(new Pike(new Vector2(350 - i * 15, 290 - 60), 90, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(350 - i * 15, 290 + 60), 270, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(260, 320 - i * 15), 0, BeatTime(56 + i)) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(380, 320 - i * 15), 180, BeatTime(56 + i)) { IsHidden = true });
                    }
                }
                if (InBeat(192 + 4, 256 - 18) && At0thBeat(8))
                {
                    switch (Rand(0, 3))
                    {
                        case 0:
                            CreateSpear(new Pike(new Vector2(245, 290), 0, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(245, 270), 0, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 2:
                            CreateSpear(new Pike(new Vector2(395, 290), 180, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(395, 310), 180, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 1:
                            CreateSpear(new Pike(new Vector2(320, 215), 90, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(340, 215), 90, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 3:
                            CreateSpear(new Pike(new Vector2(320, 365), 270, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(300, 365), 270, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                    }
                    switch ((LastRand + 1) % 4)
                    {
                        case 0:
                            CreateSpear(new Pike(new Vector2(245, 290), 0, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(245, 270), 0, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 2:
                            CreateSpear(new Pike(new Vector2(395, 290), 180, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(395, 310), 180, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 1:
                            CreateSpear(new Pike(new Vector2(320, 215), 90, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(340, 215), 90, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                        case 3:
                            CreateSpear(new Pike(new Vector2(320, 365), 270, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            CreateSpear(new Pike(new Vector2(300, 365), 270, BeatTime(12)) { IsHidden = true, DrawingColor = Color.Red });
                            break;
                    }
                }
                if (InBeat(256))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }

                if (InBeat(248))
                {
                    float time = BeatTime(12.3f);
                    int k = 0;
                    Fortimes(30, (x) =>
                    {
                        CreateArrow(time, Rand(0, 3), 5, k ^= 1, Rand(0, 1));
                        time += BeatTime(2);
                    });
                }

                if (InBeat(320))
                {
                    SetBox(270, 400, 240);
                    SetSoul(0);
                }
                if (InBeat(320, 320 + 120 - 8) && At0thBeat(4))
                {
                    float rot = Rand(0, 359);
                    for (int i = 0; i < 10; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 7.0f, 165, rot, BeatTime(8)));
                        CreateSpear(new SwarmSpear(Heart.Centre, 37.0f, 1165, rot, BeatTime(7)));
                        rot += 36;
                    }
                }
                if (InBeat(440))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(448 - 8))
                {
                    float time = BeatTime(8.5f);
                    int[] t = { 1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 1, 1, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 0, 1, 0, 0, 0,
                                1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 1, 0, 0, 0};
                    Fortimes(64, (x) =>
                    {
                        if (t[x] == 1)
                            CreateArrow(time, Rand(0, 3), 4.5f, 1, 0);
                        if (x % 2 == 0)
                        {
                            CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                            CreateArrow(time + BeatTime(2), LastRand + 2, 6.0f, 0, 1);
                        }
                        time += BeatTime(2);
                    });
                }
                if (InBeat(576))
                {
                    SetSoul(0);
                    TP(320, 270);
                    SetBox(270, 75, 78);
                }

                if (InBeat(576, 576 + 128 - 16) && At0thBeat(4))
                {
                    CreateSpear(new Pike(new Vector2(320 + Rand(-2, 2) * 15, 270 - 67), 90, BeatTime(12)) { IsHidden = true });
                    CreateSpear(new Pike(new Vector2(320 + Rand(-2, 2) * 15, 270 + 67), 270, BeatTime(12)) { IsHidden = true });
                }
                if (InBeat(704 - 8))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(704 - 8))
                {
                    float time = BeatTime(8.25f);
                    int[] t = { 1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 1, 1, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 0, 0};
                    Fortimes(64, (x) =>
                    {
                        if (t[x] == 1)
                            CreateArrow(time, Rand(0, 3), 10.0f, 1, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(832))
                {
                    SetSoul(0);
                    SetBox(270, 400, 240);
                }
                if (InBeat(832, 832 + 128 - 17) && At0thBeat(4))
                {
                    float rot = Rand(0, 44);
                    for (int i = 0; i < 10; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 6.6f, 155, rot += 45, BeatTime(7)));
                    }
                    rot -= 13f;
                    for (int i = 0; i < 10; i++)
                    {
                        rot += 22.5f;
                        CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(90, rot + 90), 7.6f, 185, rot, BeatTime(7)));
                        CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(90, rot + 90), 6.6f, 205, rot, BeatTime(7)));
                        rot += 22.5f;
                        CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(110, rot - 90), 7.6f, 185, rot, BeatTime(7)));
                        CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(110, rot - 90), 6.6f, 205, rot, BeatTime(7)));
                    }
                }

                if (InBeat(960 - 8))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(960 - 8))
                {
                    float time = BeatTime(8.5f);
                    int[] t = { 1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 1, 1, 1, 1,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 0, 0, 0, 0, 0,
                                1, 0, 0, 1, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0, 0, 0};
                    Fortimes(64, (x) =>
                    {
                        if (t[x] == 1)
                            CreateArrow(time, Rand(0, 3), 10.0f, 1, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(time, (x % 4) + 3, 5.0f, 0, 1);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(1088))
                {
                    SetSoul(0);
                    SetBox(265, 560, 280);
                }
                if (InBeat(1088, 1088 + 12 * 8 - 4 - 12) && AtKthBeat(12, BeatTime(8)))
                {
                    float rot = Rand(0, 359);
                    for (int i = 0; i < 9; i++)
                    {
                        CreateSpear(new CircleSpear(Heart.Centre, 4f, 1.36f, 187, rot += 360 / 9f, 0.02f));
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        CreateSpear(new CircleSpear(Heart.Centre, -4f, 1.36f, 187, rot += 360 / 3f, 0.02f));
                    }
                }
                if (InBeat(1184 - 12))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1184 - 12))
                {
                    float time = BeatTime(12.5f);
                    int[] t = { 1, 1, 1, 1, 1, 1,
                                1, 0, 1, 1, 1, 1,
                                1, 0, 1, 0, 1, 0,
                                1, 0, 1, 0, 1, 0,
                                1, 1, 1, 1, 1, 1,
                                1, 0, 1, 1, 1, 1,
                                1, 0, 0, 1, 0, 1,
                                1, 0, 1, 0, 1, 0};
                    Fortimes(48, (x) =>
                    {
                        int y = Rand(0, 3);
                        if (t[x] == 1)
                            CreateArrow(time, y + 2, 6.0f, 1, 1);
                        CreateArrow(time, y, 6.0f, 0, 0);
                        time += BeatTime(2);
                    });
                }
                if (InBeat(1280))
                {
                    SetBox(270, 400, 240);
                    SetSoul(0);
                }
                if (InBeat(1280, 1280 + 12 * 8 - 18) && At0thBeat(4))
                {
                    float rot = Rand(0, 359);
                    for (int i = 0; i < 6; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 7.6f, 142, rot, BeatTime(4.2f)));
                        CreateSpear(new SwarmSpear(Heart.Centre, 7.6f, 142, rot + 5, BeatTime(4.2f)));
                        rot += 60;
                    }
                }
                if (InBeat(1376 - 8))
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1376 - 18))
                {
                    float time = BeatTime(19f);
                    int[] t = { 1, 1, 1, 1, 1, 1,
                                1, 0, 1, 1, 1, 1,
                                1, 0, 1, 0, 1, 0,
                                1, 0, 1, 0, 1, 0,
                                1, 0, 0, 1, 1, 1,
                                1, 0, 1, 0, 1, 0,
                                1, 0, 0, 0, 1, 0,
                                1, 0, 0, 0, 0, 0};
                    Fortimes(48, (x) =>
                    {
                        int y = Rand(0, 3);
                        if (t[x] == 1)
                            CreateArrow(time, y, 6.0f, 1, 0);
                        if (x % 2 == 1)
                        {
                            CreateArrow(time, y + 2, 4.0f, 0, 0);
                            CreateArrow(time, y + 2, 4.0f, 0, 1);
                        }
                        time += BeatTime(2);
                    });
                }
                if (InBeat(1473))
                {
                    SetBox(240, 560, 300);
                    SetSoul(0);
                }
                for (int x = 0; x < 4; x++)
                {
                    if (InBeat(1473 + x * 24, 1473 + 8 + x * 24) && At0thBeat(0.5f))
                    {
                        for (int i = 1; i <= 4; i++)
                            CreateSpear(new Pike(new Vector2(640 / 5 * i, 50), (Gametime - BeatTime(1743)) / BeatTime(0.5f) * (360 / 16f), BeatTime(8))
                            {
                                IsSpawnMute = i == 4
                            });
                    }
                    if (InBeat(1473 + 12 + x * 24, 1473 + 8 + 12 + x * 24) && At0thBeat(0.5f))
                    {
                        for (int i = 1; i <= 4; i++)
                            CreateSpear(new Pike(new Vector2(640 / 5 * i, 430), (Gametime - BeatTime(1743)) / BeatTime(0.5f) * (360 / 16f), BeatTime(8))
                            {
                                IsSpawnMute = i == 4
                            });
                    }
                }

                if (InBeat(1569 - 2, 1569 + 48 - 14) && AtKthBeat(4, BeatTime(3)))
                {
                    float rot = Rand(0, 359);
                    for (int i = 0; i < 8; i++)
                    {
                        CreateSpear(new CircleSpear(Heart.Centre, 3.6f, 1.56f, 187, rot += 45, 0.018f));
                    }
                }
                if (InBeat(1569 + 48 - 13.5f))
                {
                    float rot = LastRand;
                    for (int i = 0; i < 6; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 13.6f, 192, rot, BeatTime(8f)));
                        rot += 60;
                    }
                }
                if (InBeat(1569 + 48 - 14))
                {
                    float rot = Rand(0, 359);
                    for (int dis = 1200; dis <= 1800; dis += 100)
                        for (int i = 0; i < 9; i++)
                        {
                            CreateSpear(new SwarmSpear(Heart.Centre, 23.6f + dis / 100f, dis, rot, BeatTime(8f)));
                            rot += 60;
                        }
                }
                if (InBeat(1569 + 48 - 2, 1569 + 48))
                {
                    SetBox(290, 140, 140);
                    SetSoul(Rand(0, 3));
                }
                if (InBeat(1569 + 48 + 0.2f))
                {
                    SetSoul(0);
                }
                if (InBeat(1569 + 48 - 4, 1569 + 96 - 12) && At0thBeat(1))
                {
                    float time = Gametime * 3f;
                    CreateSpear(new SwarmSpear(new Vector2(320, 290), 7.6f, 140, time, BeatTime(7)));
                    CreateSpear(new SwarmSpear(new Vector2(320, 290), 7.6f, 140, time + 180, BeatTime(7)));
                }
                if (InBeat(1569 + 48 - 4, 1569 + 96 - 12) && At0thBeat(8))
                {
                    ScreenDrawing.BoxBackColor = Color.Transparent;
                    float time = Gametime * 3f;
                    for (int i = 0; i < 6; i++)
                    {
                        CreateSpear(new Pike(new Vector2(210, 227 + i * 28), 0, BeatTime(4)) { IsHidden = true, IsSpawnMute = true });
                        Line line = new(new Vector2(250, 227 + i * 28), new Vector2(390, 227 + i * 28)) { DrawingColor = Color.Red};
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutSine(1f, 0.0f, BeatTime(4)));
                        builder.Run(s => line.Alpha = s);
                        CreateEntity(line);
                        DelayBeat(4, () => { line.Dispose(); });
                    }
                }
                if (InBeat(1569 + 48 - 4, 1569 + 96 - 12) && AtKthBeat(8, BeatTime(4)))
                {
                    float time = Gametime * 3f;
                    for (int i = 0; i < 5; i++)
                    {
                        CreateSpear(new Pike(new Vector2(430, 227 + 14 + i * 28), 180, BeatTime(4)) { IsHidden = true, IsSpawnMute = true });
                        Line line = new(new Vector2(250, 241 + i * 28), new Vector2(390, 241 + i * 28)) { DrawingColor = Color.Red };
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutSine(1f, 0.0f, BeatTime(4)));
                        builder.Run(s => line.Alpha = s);
                        CreateEntity(line);
                        DelayBeat(4, () => { line.Dispose(); });
                    }
                }
                if (InBeat(1569 + 96 - 12 + 4))
                    ScreenDrawing.BoxBackColor = Color.Black;
            }

            public void Start()
            {
                ScreenDrawing.UIColor = Color.Red;
                HeartAttribute.Speed = 2.8f;
                SetGreenBox();
                TP();
                SetSoul(1);
                // SetSoul(0);
                HeartAttribute.MaxHP = 14;
            }
        }
    }
}