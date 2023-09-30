using Extends;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static Extends.Someway;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Entities.EasingUtil;

namespace Rhythm_Recall.Waves
{
    public class Flan : IChampionShip
    {
        public Flan()
        {

            difficulties = new()
            {
                { "Extra", Difficulty.ExtremePlus }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (82f / 60f))
            {

            }
            public string Music => "Flan";

            public string FightName => "Flandre";
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 0f),
                        }
                    );
                public override string BarrageAuthor => "TK";
                public override string PaintAuthor => "";
                public override string SongAuthor => "";
            }
            public SongInformation Attributes => new ThisInformation();
            private bool notRegistered = true;
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production1;
            RenderProduction production2;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;
            public float TempVar;

            public void Hard()
            {
                if (Gametime < 0) return;
            }

            public void Noob()
            {
                if (Gametime < 0) return;
            }
            public void Easy()
            {
                if (Gametime < 0) return;
            }
            public void Normal()
            {
                if (Gametime < 0) return;
            }
            public void Extreme()
            {
                if (Gametime < 0) return;
            }
            public void ExtremePlus()
            {
                //Piano
                if (InBeat(15))
                {
                    CreateChart(60, BeatTime(1), 5, new string[]
                    {
                        "(R)(+0)", "", "","",
                        "R", "", "",
                        "R", "", "","",
                        "R", "", "R","",
                        "R", "", "","R",
                        "", "R", "","R",
                        "", "R", "","R",

                        "","*^R12","","*>+012",
                        "","","","","",

                        "R", "",
                        "R", "",
                        "R", "",
                        "R", "", "R",

                        "","*^R12",
                        "","*<+012","","","","","",

                        "R", "",
                        "R", "",
                        "R", "", "",
                        "R", "", "R","",

                        "","*R12","","*^+012",
                        "","","",

                        "R", "", "","",
                        "R", "", "",
                        "R", "", "","",
                        "R", "", "R","",
                        "R", "", "","R",
                        "", "R", "","R",
                        "", "R", "","R",

                        "","*<R12","","*>+012",
                        "","*^+012","","","",

                        "R", "",
                        "R", "",
                        "R", "",
                        "R", "", "R","",
                        "R","","R","",
                        "R","","R", "",
                        "R", "",
                        "R", "",
                        "R", "", "",
                        "R", "", "R","",
                        "^R1", "", "<+01","",">+01",
                    });
                }
                //Vocals
                if (InBeat(33))
                {
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(14, BeatTime(3.2f), RandBool());
                        ScreenDrawing.CameraEffect.SizeShrink(7, BeatTime(3.2f));
                    });
                    CreateChart(60, BeatTime(2), 6, new string[]
                    {
                        "#4#R0(Shrink)", "","R1",
                        "R1", "",
                        "",
                        "R1", "R1",
                        "R1", "R1",
                        "R1", "R1",
                        "R1", "R1",
                        "R1","","","",

                        "#4#R0(Shrink)",
                        "R1", "R1",
                        "R1", "R1",
                        "R1", "",
                        "","",
                        "R1", "R1",
                        "R1", "R1","R1",
                        "R1", "",
                        "","","",

                        "#4#R1(Shrink)", "",
                        "R", "R",
                        "","R",
                        "R", "R",
                        "R", "R",
                        "R", "R",
                        "R", "R",
                        "","","","",
                    });
                }
                if (InBeat(46.5f))
                {
                    RegisterFunctionOnce("line1", () =>
                    {
                        var ang = Rand(0, 360);
                        Line l1 = new(new Vector2(320, 240), ang) { Alpha = 0.5f };
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1));
                        DelayBeat(1, () =>
                        {
                            l1.Dispose();
                        });
                        l1 = new(new Vector2(320, 240), ang + 90) { Alpha = 0.5f };
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1));
                        DelayBeat(1, () =>
                        {
                            l1.Dispose();
                        });
                    });
                    CreateChart(60, BeatTime(1), 3, new string[]
                    {
                        "", "R", "line1",
                        "R", "line1","",
                        "R", "line1","","",
                        "R", "line1","",
                        "R", "line1","",
                        "R", "line1","",
                        "R", "line1","",
                        "R", "line1",
                    });
                }
                var time = GametimeF;
                if (time == 2121)
                {
                    DrawingUtil.LerpScreenScale(120, 2, 0.08f);
                }
                if (time == 2320)
                {
                    CreateEntity(new Hand());
                }
                if (time == 2350)
                {
                    DrawingUtil.BetterBlackScreen(10, 0, 10, Color.White);
                }
                if (time == 2360)
                {
                    HeartAttribute.MaxHP = 495;
                    HeartAttribute.HP = 495;
                    HeartAttribute.KR = true;
                    HeartAttribute.KRDamage = 5;
                    for (int i = 0; i < 3; i++)
                    {
                        SetSoul(0);
                        Heart.Split();
                        SetSoul(0);
                        SetPlayerBoxMission(i);
                        InstantSetBox(240, 84, 84);
                        InstantTP(320, 240);
                    }
                    ScreenDrawing.ScreenScale = 1;
                }
                if (time > 2360 && time < 2460)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        SetPlayerBoxMission(i);
                        InstantTP(FightBox.boxs[i].Centre);
                    }
                    if (time == 2380)
                    {
                        PlaySound(FightResources.Sounds.pierce);
                        PlaySound(FightResources.Sounds.pierce);
                        SetPlayerBoxMission(0);
                        SetBox(new Vector2(160, 240), 110, 110);
                        SetPlayerBoxMission(1);
                        SetBox(new Vector2(160, 240), 110, 110);
                        SetPlayerBoxMission(2);
                        SetBox(new Vector2(480, 240), 110, 110);
                        SetPlayerBoxMission(3);
                        SetBox(new Vector2(480, 240), 110, 110);
                    }
                    if (time == 2400)
                    {
                        PlaySound(FightResources.Sounds.pierce);
                        PlaySound(FightResources.Sounds.pierce);
                        SetPlayerBoxMission(0);
                        SetBox(new Vector2(160, 120), 140, 140);
                        SetPlayerBoxMission(1);
                        SetBox(new Vector2(160, 360), 140, 140);
                        SetPlayerBoxMission(2);
                        SetBox(new Vector2(480, 120), 140, 140);
                        SetPlayerBoxMission(3);
                        SetBox(new Vector2(480, 360), 140, 140);
                    }
                }
                if (time == 2450)
                {
                    DrawingUtil.ScreenAngle(360, 60);
                    for (int i = 0; i < 4; i++)
                    {
                        SetBoxMission(i);
                        for (int ii = 0; ii < 20; ii++)
                            CreateBone(new SideCircleBone(ii * 30, 6, 75, 60));
                    }
                }
                if (time is 2510 or 2515 or 2520 or 2525)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    bool[] appear = new bool[]
                        {
                            false, true, false
                        };
                    for (int i = 0; i < 3; i++)
                    {
                        appear[i] = (time % 10) == 0;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        SetBoxMission(i);
                        if (appear[0] == true)
                            CreateBone(new DownBone(i < 2, 7, 70));
                        if (appear[1] == true)
                            CreateBone(new LeftBone((i % 2) == 1, 7, 55));
                        if (appear[2] == true)
                            CreateBone(new RightBone((i % 2) == 1, 7, 55));
                    }
                }
                if (time is 2540 or 2550 or 2560 or 2565)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    for (int i = 0; i < 4; i++)
                    {
                        SetBoxMission(i);
                        CreateBone(new UpBone(i < 2, 7, 55));
                    }
                }
                if (time is 2585 or 2605)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    for (int i = 0; i < 4; i++)
                    {
                        SetBoxMission(i);
                        CreateBone(new LeftBone((i % 2) == 0, 7, 65));
                    }
                }
                if (time is 2595 or 2615)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    for (int i = 0; i < 4; i++)
                    {
                        SetBoxMission(i);
                        CreateBone(new RightBone((i % 2) == 0, 7, 65));
                    }
                }
                if (time is 2625 or 2645)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    for (int i = 0; i < 4; i++)
                    {
                        SetBoxMission(i);
                        CreateBone(new UpBone(i < 2, 7, 65));
                    }
                }
                if (time is 2635)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    for (int i = 0; i < 4; i++)
                    {
                        SetBoxMission(i);
                        CreateBone(new DownBone(i < 2, 7, 65));
                    }
                }
                if (time == 2660)
                {
                    PlaySound(FightResources.Sounds.spearAppear);
                    SetPlayerBoxMission(0);
                    SetSoul(1);
                    SetBox(new Vector2(320 - 42, 240 - 42), 84, 84);
                    TP(320 - 42, 240 - 42);
                }
                if (time == 2690)
                {
                    PlaySound(FightResources.Sounds.spearAppear);
                    SetPlayerBoxMission(3);
                    SetSoul(1);
                    SetBox(new Vector2(320 + 42, 240 + 42), 84, 84);
                    TP(320 + 42, 240 + 42);
                }
                if (time == 2720)
                {
                    PlaySound(FightResources.Sounds.spearAppear);
                    SetPlayerBoxMission(1);
                    SetSoul(1);
                    SetBox(new Vector2(320 - 42, 240 + 42), 84, 84);
                    TP(320 - 42, 240 + 42);
                }
                if (time == 2750)
                {
                    PlaySound(FightResources.Sounds.spearAppear);
                    SetPlayerBoxMission(2);
                    SetSoul(1);
                    SetBox(new Vector2(320 + 42, 240 - 42), 84, 84);
                    TP(320 + 42, 240 - 42);
                }
                if (time == 2710)
                {
                    FourSoulArrow(1, Rand(0, 3));
                    FourSoulArrow(3, Rand(0, 3));
                    FourSoulArrow(5, Rand(0, 3));
                    FourSoulArrow(7f, Rand(0, 3));
                    FourSoulArrow(9f, Rand(0, 3));
                    FourSoulArrow(11f, Rand(0, 3));
                    var dir = Rand(0, 3);
                    FourSoulArrow(13, dir);
                    FourSoulArrow(13, dir + 2);
                }
                if (time == 2800)
                {
                    FourSoulArrow(1, Rand(0, 3));
                    FourSoulArrow(2, Rand(0, 3));
                    FourSoulArrow(3, Rand(0, 3));
                    FourSoulArrow(3.5f, Rand(0, 3));
                    FourSoulArrow(4, Rand(0, 3));
                    FourSoulArrow(5, Rand(0, 3));
                    FourSoulArrow(5.5f, Rand(0, 3));
                    FourSoulArrow(6, Rand(0, 3));
                    FourSoulArrow(7, Rand(0, 3));
                    FourSoulArrow(8, Rand(0, 3));
                    FourSoulArrow(9, Rand(0, 3));
                    FourSoulArrow(10, Rand(0, 3));
                    FourSoulArrow(11, Rand(0, 3));
                    FourSoulArrow(12, Rand(0, 3));

                    FourSoulArrow(15, Rand(0, 3));
                    FourSoulArrow(16, Rand(0, 3));
                    FourSoulArrow(17, Rand(0, 3));
                    FourSoulArrow(17.5f, Rand(0, 3));
                    FourSoulArrow(18, Rand(0, 3));
                    FourSoulArrow(19, Rand(0, 3));
                    FourSoulArrow(19.5f, Rand(0, 3));
                    FourSoulArrow(20, Rand(0, 3));
                    FourSoulArrow(21, Rand(0, 3));
                    FourSoulArrow(22, Rand(0, 3));
                    FourSoulArrow(23, Rand(0, 3));

                    FourSoulArrow(26, Rand(0, 3), 8, 1);
                    FourSoulArrow(27, Rand(0, 3), 8, 1);
                    FourSoulArrow(28, Rand(0, 3), 8, 1);
                    FourSoulArrow(29, Rand(0, 3), 8, 1);
                    FourSoulArrow(30, Rand(0, 3), 8, 1);
                    FourSoulArrow(30.5f, Rand(0, 3), 8, 1);
                    FourSoulArrow(31, Rand(0, 3), 8, 1);
                    FourSoulArrow(32, Rand(0, 3), 8, 1);
                    FourSoulArrow(33.5f, Rand(0, 3), 8, 1);
                    FourSoulArrow(34, Rand(0, 3), 8, 1);
                    FourSoulArrow(35, Rand(0, 3), 8, 1);
                    FourSoulArrow(36, Rand(0, 3), 8, 1);
                    FourSoulArrow(37, Rand(0, 3), 8, 1);
                    FourSoulArrow(38, Rand(0, 3), 8, 1);
                    FourSoulArrow(39, Rand(0, 3), 8, 1);
                    FourSoulArrow(41, Rand(0, 3), 8, 1);
                    FourSoulArrow(42, Rand(0, 3), 8, 1);
                    FourSoulArrow(43, Rand(0, 3), 8, 1);

                    for (int i = 0; i < 7; i++)
                    {
                        FourSoulArrow(46 + i * 2f, i);
                        FourSoulArrow(46 + i * 2f, i + 2, 8, 1);
                        if (i == 6)
                        {
                            FourSoulArrow(46 + i * 2f, i + 2);
                            FourSoulArrow(46 + i * 2f, i, 8, 1);
                        }
                    }

                    for (int ii = 0; ii < 8; ii++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if ((ii == 7 && i != 2) || ii != 7)
                            {
                                FourSoulArrow(62 + i * 2 + ii * 6, i);
                                FourSoulArrow(62 + i * 2 + ii * 6, i + 2, 8, 1);
                            }
                        }
                    }
                }
                if (time == 3300)
                {
                    SetPlayerBoxMission(0);
                    CreateGB(new GreenSoulGB(BeatTime(4), 3, 0, 20));
                }
                if (time == 3310)
                {
                    SetPlayerBoxMission(0);
                    CreateGB(new GreenSoulGB(BeatTime(4), 3, 1, 10));
                }
                if (time == 3330)
                {
                    SetPlayerBoxMission(0);
                    CreateGB(new GreenSoulGB(BeatTime(4), 0, 0, BeatTime(1)));
                    CreateGB(new GreenSoulGB(BeatTime(4), 2, 1, BeatTime(1)));
                }
                if (time == 3480)
                {
                    SetPlayerBoxMission(0);
                    SetBox(new Vector2(320 - 21, 240), 84, 84);
                    TP(320 - 21, 240);
                    SetPlayerBoxMission(1);
                    SetBox(new Vector2(320 - 21, 240), 84, 84);
                    TP(320 - 21, 240);
                }
                if (time == 3490)
                {
                    SetPlayerBoxMission(2);
                    SetBox(new Vector2(320 + 21, 240), 84, 84);
                    TP(320 + 21, 240);
                    SetPlayerBoxMission(3);
                    SetBox(new Vector2(320 + 21, 240), 84, 84);
                    TP(320 + 21, 240);
                }
                if (time == 3500)
                {
                    DrawingUtil.ScreenAngle(360, 20);
                    for (int i = 0; i < 4; i++)
                    {
                        SetPlayerBoxMission(i);
                        SetGreenBox();
                        TP();
                    }
                    SetPlayerBoxMission(0);
                    RegisterFunctionOnce("Displace", () =>
                    {
                        Arrow[] arrow = GetAll<Arrow>();
                        var offset = 15;
                        for (int i = 0; i < arrow.Length; i++)
                        {
                            if (arrow[i].ArrowColor == 0)
                                continue;
                            if (arrow[i].Way == 0) arrow[i].Offset = new(0, offset);
                            if (arrow[i].Way == 2) arrow[i].Offset = new(0, -offset);
                        }
                    });
                    RegisterFunctionOnce("camrot", () =>
                    {
                        ScreenDrawing.ScreenAngle = Rand(-25, 25);
                    });
                    RegisterFunctionOnce("camre", () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    CreateChart(60, BeatTime(1), 6, new string[]
                    {
                        "Displace", "", "", "$3($3)",    "", "", "", "$3($3)",
                        "", "", "", "",    "", "", "R", "R",
                        "R", "R", "R", "R",    "R", "R", "R", "R",
                        "R", "R", "R", "R",    "R", "R", "R", "R",
                        "R", "", "", "",        "", "R", "R", "R",
                        "R", "R", "R", "R",    "R", "R", "R", "R",
                        "R", "R", "R", "R",    "R", "R", "R", "R",
                        "R", "R", "R", "",    "", "", "^$1(^$3)", "",
                        "", "", "R", "",    "R", "", "", "",
                        "R", "", "R", "D",    "", "", "", "",
                        "R", "", "R", "D",    "D", "D", "", "D",
                        "", "R", "", "",    "", "", "", "R",
                        "", "", "", "R",    "R", "R", "R", "R",
                        "R", "R", "R", "R",    "R", "R", "", "R(R1)(camrot)",
                        "", "R(R1)(camrot)", "", "R(R1)(camrot)",    "", "R(R1)(camrot)", "", "(camre)",
                    });
                    CreateChart(60, BeatTime(1), 6, new string[]
                   {
                        "$01($21)","", "","",
                        "$01($21)","", "","",
                        "$01($21)","", "","",
                        "$01($21)","", "$01($21)","",
                        "$01($21)", "$01($21)", "$01($21)", "$01($21)",
                        "$01($21)", "", "$01($21)", "",
                        "$01($21)","$01($21)", "","",
                        "$01($21)","","$01($21)","",
                        "$01($21)","", "","",
                        "$01($21)","", "",

                        "$01($21)","", "","",
                        "$01($21)","", "$01($21)","",
                        "$01($21)", "$01($21)", "$01($21)", "$01($21)",
                        "$01($21)", "", "$01($21)", "",
                        "$01($21)","$01($21)",
                        "","",
                        "$01($21)","","$01($21)","",
                        "$01($21)","",

                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                        "$01","+21", "+21","+21",
                   });
                }
                if (time == 3511)
                {
                    Heart.MergeAll();
                }
                if (time >= 3530)
                {
                    //repeat 4 times
                    if (time < 3530 + 190 * 4 - 120)
                    {
                        var dur = 190;
                        var ang = 0;
                        ScreenDrawing.ScreenAngle = TKValueEasing.EaseOutBack((time - 3530) % dur, ang, 360, dur);
                    }
                }
                if (time == 4200)
                {
                    string[] rway = {
                        "R1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                        "D1", "+01", "D1", "+01",
                    };
                    CreateChart(70, BeatTime(1.8f), 7, rway);
                    CreateChart(60, BeatTime(0.5f), 7, new string[]
                    {
                        "", "", "", "R",     "", "", "R", "",
                        "", "R", "", "",     "R", "", "", "R",
                        "", "", "R", "",     "", "R", "", "R",
                        "", "R", "", "",     "R", "", "", "",
                        "", "", "R", "",     "", "R", "", "",
                        "R", "", "", "R",     "", "R", "", "",
                        "R", "", "", "R",     "", "", "", "",
                        "", "", "R", "",     "R", "", "", "",
                        "R", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                        "", "", "", "",     "", "", "", "",
                    });
                }
            }
            public void Start()
            {
                SetSoul(1);
                TP();
                SetGreenBox();
                GametimeDelta = 660;
                //GametimeDelta += BeatTime(17);
                //GametimeDelta = 2120;
                //GametimeDelta = 4190;
                PlayOffset = GametimeDelta;
                HeartAttribute.MaxHP = 3;
                HeartAttribute.HP = 3;
            }

            #region Functions
            private static void FourSoulArrow(float BeatCount, int Direction, int Speed = 8, int Color = 0, int Mode = 0)
            {
                var target = 0;
                Direction %= 4;
                switch (Direction)
                {
                    case 0:
                        target = RandBool() ? 2 : 3;
                        break;
                    case 1:
                        target = RandBool() ? 1 : 3;
                        break;
                    case 2:
                        target = RandBool() ? 0 : 1;
                        break;
                    case 3:
                        target = RandBool() ? 0 : 2;
                        break;
                }
                SetPlayerBoxMission(target);
                CreateArrow(60f + 62.5f / (82f / 60f) * BeatCount / 8f, Direction, Speed, Color, Mode);
            }
            #endregion

            #region Entities
            private class Hand : Entity
            {
                private float alpha = 0;
                private int time = 0;
                private int Y = 480;
                public Hand()
                {

                }

                public override void Draw()
                {
                    FormalDraw(Resources.TKSpecial.AprilFools2023.FlanHand, new(220, Y), Color.White * alpha, 7, 0, new(0, 0));
                }
                public override void Update()
                {
                    if (Y > 140) Y -= 10;
                    time++;
                    alpha += 0.05f;
                    if (time == 30) Dispose();
                }
            }
            #endregion
        }
    }
}