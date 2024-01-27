using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using Extends;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using System.Net.Security;
using System.Net.WebSockets;

namespace Rhythm_Recall.Waves
{
    internal class _1111 : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public _1111()
        {
            dif.Add("Div.4", Difficulty.Noob);
            dif.Add("Div.3", Difficulty.Easy);
            dif.Add("Div.2", Difficulty.Normal);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (158f / 60f)) { }
            public string Music => "1111";
            public string FightName => "_1111";
            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "";
                public override string BarrageAuthor => "114514";
                public override string AttributeAuthor => "1919810";
                public Information() { this.MusicOptimized = true; }
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Noob,0f),
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Noob,0f),
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Noob,0f),
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                    );
            }
            Player.Heart Main;
            Player.Heart Another;
            public void Start()
            {
                #region Bound
                RegisterFunction("UpB", () =>
                {
                    var r = EaseOut(T(Arguments[0]), Arguments[1], Arguments[2], EaseState.Quad);
                    RunEase((s) => { ScreenDrawing.UpBoundDistance = s; }, r);
                });
                RegisterFunction("DownB", () =>
                {
                    var r = EaseOut(T(Arguments[0]), Arguments[1], Arguments[2], EaseState.Quad);
                    RunEase((s) => { ScreenDrawing.DownBoundDistance = s; }, r);
                });
                RegisterFunction("LeftB", () =>
                {
                    var r = EaseOut(T(Arguments[0]), Arguments[1], Arguments[2], EaseState.Quad);
                    RunEase((s) => { ScreenDrawing.LeftBoundDistance = s; }, r);
                });
                RegisterFunction("RightB", () =>
                {
                    var r = EaseOut(T(Arguments[0]), Arguments[1], Arguments[2], EaseState.Quad);
                    RunEase((s) => { ScreenDrawing.RightBoundDistance = s; }, r);
                });
                RegisterFunction("BoundBack", () =>
                {
                    var eas = LinkEase(
                            Stable(0, ScreenDrawing.DownBoundDistance),
                            EaseOut(T(Arguments[0]/2), Arguments[1], EaseState.Quad),
                            EaseIn(T(Arguments[0]/2), -Arguments[1], EaseState.Sine)
                            );
                    RunEase((s) => { ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s; }, eas);
                });
                #endregion
                RegisterFunction("Con", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(Arguments[0], T(Arguments[1]), Arguments[2] == 0);
                });
                RegisterFunction("SSS", () =>
                {
                    var r = LinkEase(Stable(0, ScreenDrawing.ScreenScale), EaseOut(T(Arguments[0]), ScreenDrawing.ScreenScale, Arguments[1], EaseState.Quad));
                    RunEase((s) => { ScreenDrawing.ScreenScale = s; }, r);
                });
                RegisterFunction("SCP", () =>
                {
                    var r = LinkEase(Stable(0, ScreenDrawing.ScreenPositionDelta), EaseOut(T(Arguments[0])-1, ScreenDrawing.ScreenPositionDelta, new(Arguments[1], Arguments[2]), EaseState.Quad));
                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, r);
                });
                RegisterFunction("SA", () =>
                {
                    var r = LinkEase(Stable(0, ScreenDrawing.ScreenAngle), EaseOut(T(Arguments[0]), ScreenDrawing.ScreenAngle, Arguments[1], EaseState.Quad));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, r);
                });
                Settings.GreenTap = true;
                GametimeDelta = 33.75f;
                InstantSetGreenBox();
                SetSoul(1);
                InstantTP(320, 240);
                bool delay = true;
                var beat = BeatTime(128-1);
                if (delay)
                {
                    PlayOffset = beat;
                    GametimeDelta += beat 
                        ;
                }
                DelayBeat(0, () => { Main = Heart; });
                Main = Heart;
               //DrawingUtil.BetterBlackScreen(0, 0, BeatTime(32), Color.Black);
            }
            private float T(float beat)
            {
                return BeatTime(beat);
            }
            public void Easy()
            {

            }

            public void Extreme()
            {
                //Intro 1
                if (InBeat(2f)) 
                {
                    Arrow.UnitEasing ar = new();
                    AddInstance(ar);
                    ar.ApplyTime = BeatTime(2);
                    ar.RotationEase = LinkEase(
                        Stable(0,7),
                        EaseOut(BeatTime(2), 7, 0, EaseState.Quad));
                    ar.TagApply("A");
                    RegisterFunctionOnce("Line1", () =>
                    {
                        Line line = new(EaseIn(BeatTime(6), new Vector2(640, 485), new(0, -5), EaseState.Sine).Easing, EaseIn(BeatTime(6), 0, 90, EaseState.Sine).Easing);
                        line.ObliqueMirror = true;
                        line.Width = 4;
                        line.Alpha = 0.6f;
                        DelayBeat(6, () => line.Dispose());
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        Line line = new(EaseIn(BeatTime(7), new Vector2(0, -5), new(640, 5), EaseState.Sine).Easing, EaseIn(BeatTime(7), 90, 0, EaseState.Sine).Easing);
                        line.ObliqueMirror = true;
                        line.Width = 4;
                        line.Alpha = 0.6f;
                        DelayBeat(12, () => line.Dispose());
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("L3A", () =>
                    {
                        int k = 0;
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(i * 0.0625f, () =>
                            {
                                Line l = new(new Vector2(620 - k * 40, 15 + k * 30), 45) { Alpha = 0.7f };
                                l.AlphaDecrease(BeatTime(0.85f), 0.75f);
                                CreateEntity(l);
                                DelayBeat(BeatTime(1), () => { l.Dispose(); });
                                k++;
                            });
                        }
                    });
                    RegisterFunctionOnce("L3B", () =>
                    {
                        int k = 0;
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(i * 0.0625f, () =>
                            {
                                Line l = new(new Vector2(620 - k * 40, 465 - k * 30), -45) { Alpha = 0.7f };
                                l.AlphaDecrease(BeatTime(0.85f), 0.75f);
                                CreateEntity(l);
                                DelayBeat(BeatTime(1), () => { l.Dispose(); });
                                k++;
                            });
                        }
                    });
                    CreateChart(BeatTime(2), BeatTime(1), 7f, new string[]
                    {
                        "R@A(Line1)", "", "", "",         "R@A", "", "", "",
                        "(R@A)(<4,10,0>SCP)(<4,-3.5>SA)", "", "", "",         "", "", "", "",
                        "R@A", "", "", "",         "R@A", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "R@A", "", "", "",         "R@A", "", "", "",
                        "(<4,-20,0>SCP)(<4,3.5>SA)", "", "", "",         "R@A", "", "", "",
                        "R@A", "", "", "",         "", "", "", "",
                        "", "", "", "",         "R@A", "", "", "",

                        "R@A(Line2)", "", "", "",         "R@A", "", "", "",
                        "R@A(<4,10,0>SCP)(<4,0>SA)", "", "", "",         "", "", "", "",
                        "R@A", "", "", "",         "R@A", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "R@A", "", "",         "", "R@A", "", "",
                        "(<2,1.15>SSS)", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "(<2,1>SSS)", "", "", "",         "", "", "", "",
                        //
                        "", "", "R@A(Line1)", "",         "", "", "R@A", "",
                        "(<4,-10,0>SCP)(<4,3.5>SA)", "", "R@A", "",         "", "", "", "",
                        "", "", "R@A", "",         "", "", "R@A", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "R@A", "",         "", "", "R@A", "",
                        "(<4,20,0>SCP)(<4,-3.5>SA)", "", "", "",         "", "", "R@A", "",
                        "", "", "R@A", "",         "", "", "", "",
                        "", "", "", "",         "", "", "(Line2)", "",

                        "", "", "R@A", "",         "", "", "R@A", "",
                        "(<4,-10,0>SCP)(<4,0>SA)", "", "R@A", "",         "", "", "", "",
                        "", "", "R@A", "",         "", "", "R@A", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "R@A", "",         "", "", "R@A", "",
                        "(<2,1.15>SSS)", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "","(L3A)($2)", "+01", "+10", "+01","(L3B)(+10)", "+01",
                        "+10", "+01",   "!!3/6","$0","+21","-2", "+21","","",
                        "","", "","",         "","","", "",


                    });
                }
                if (InBeat(32.5f))
                {
                    Arrow.UnitEasing eA = new();
                    AddInstance(eA);
                    eA.ApplyTime = BeatTime(4);
                    eA.RotationEase = LinkEase(
                        EaseOut(BeatTime(2), 0, 45, EaseState.Back),
                        EaseIn(BeatTime(2), 45, 0, EaseState.Back));
                    eA.TagApply("A");
                    RegisterFunctionOnce("Line", () =>
                    {
                        bool rnd = RandBool();
                        Vector2 start = new(rnd ? Rand(0, 100) : Rand(540, 640), Rand(0, 480)), end = new(rnd ? Rand(540, 640) : Rand(0, 100), Rand(0, 480));
                        for(int i = 0; i < 3; i++)
                        {
                            Line line = new(EaseInOut(BeatTime(4), start, end, EaseState.Linear).Easing, InfLinear(i * 60f, 4));
                            line.Alpha = 0;
                            line.AlphaIncreaseAndDecrease(BeatTime(4), 1);
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("Cam", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(17.5f, BeatTime(3), Arguments[0] == 0);
                        ScreenDrawing.CameraEffect.SizeShrink(5, BeatTime(3));
                    });
                    ScreenDrawing.BoundColor = Color.White * 0.7f;
                    RegisterFunctionOnce("L1", () =>
                    {
                        Line l = new(new Vector2(320, 0), EaseOut(T(1f), 90, Arguments[0], EaseState.Quad)){ Alpha = 0.7f };
                        CreateEntity(l);
                        l.AlphaDecrease(T(2f), 0.7f);
                        DelayBeat(2f, () => { l.Dispose(); });
                        //ScreenDrawing.CameraEffect.Convulse(4f, T(1.5f), Arguments[1] == 1);
                    });
                    RegisterFunctionOnce("L3A", () =>
                    {
                        Line l = new(LinkEase(Stable(T(Arguments[1]), new Vector2(Arguments[0], 0)),
                            EaseIn(T(0.5f), new Vector2(Arguments[0], 0), new Vector2(320, 0), EaseState.Quad)),
                            LinkEase(EaseOut(T(0.5f), new Vector2(Arguments[0], 0), new Vector2(Arguments[0], 480), EaseState.Quad),
                            Stable(T(Arguments[1] - 0.5f), new Vector2(Arguments[0], 480)),
                            EaseIn(T(0.5f), new Vector2(Arguments[0], 480), new Vector2(320, 480), EaseState.Quad)))
                        { Alpha = 0.75f };
                        CreateEntity(l);
                        DelayBeat(Arguments[1] + 0.5f, () => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("L3B", () =>
                    {
                        Line l = new(LinkEase(Stable(T(Arguments[1]), new Vector2(Arguments[0], 480)),
                            EaseIn(T(0.5f), new Vector2(Arguments[0], 480), new Vector2(320, 480), EaseState.Quad)),
                            LinkEase(EaseOut(T(0.5f), new Vector2(Arguments[0], 480), new Vector2(Arguments[0], 0), EaseState.Quad),
                            Stable(T(Arguments[1] - 0.5f), new Vector2(Arguments[0], 0)),
                            EaseIn(T(0.5f), new Vector2(Arguments[0], 0), new Vector2(320, 0), EaseState.Quad)))
                        { Alpha = 0.75f };
                        CreateEntity(l);
                        DelayBeat(Arguments[1] + 0.5f, () => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("L4", () =>
                    {
                        float dis = 50;
                    var eas = LinkEase(
                        Stable(0, 320, 0),
                        EaseOut(T(0.5f), new Vector2(dis, 0),EaseState.Cubic),
                        EaseIn(T(0.5f), new Vector2(-dis, 0), EaseState.Cubic),
                        EaseOut(T(0.5f), new Vector2(-dis, 0), EaseState.Cubic),
                        EaseIn(T(0.5f), new Vector2(dis, 0), EaseState.Cubic),
                        EaseOut(T(0.5f), new Vector2(dis, 0), EaseState.Cubic),
                        EaseIn(T(0.5f), new Vector2(-dis, 0), EaseState.Cubic),
                        EaseOut(T(0.5f), new Vector2(-dis, 0), EaseState.Cubic),
                        EaseIn(T(0.5f), new Vector2(dis, 0), EaseState.Cubic)
                            );
                        Line l = new(eas, Stable(0, 90))
                        { Alpha = 1 };
                        CreateEntity(l);
                        DelayBeat(4.5f,() => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("L5A", () =>
                    {
                        Line l1 = new(EaseOut(T(0.3f), new Vector2(320, 240), new Vector2(0, 240), EaseState.Linear), Stable(0, 90)) { Alpha = 0.7f };
                        Line l2 = new(EaseOut(T(0.3f), new Vector2(320, 240), new Vector2(320, 0), EaseState.Linear), Stable(0, 0)) { Alpha = 0.05f };
                        l2.AlphaIncrease(T(0.3f), 0.65f);
                        CreateEntity(l1);
                        CreateEntity(l2);
                        DelayBeat(0.4f,() => { l1.Dispose(); l2.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(2, T(0.5f), false);
                    });
                    RegisterFunctionOnce("L5B", () =>
                    {
                        Line l1 = new(EaseOut(T(0.3f), new Vector2(320, 240), new Vector2(640, 240), EaseState.Linear), Stable(0, 90)) { Alpha = 0.7f };
                        Line l2 = new(EaseOut(T(0.3f), new Vector2(320, 240), new Vector2(320, 480), EaseState.Linear), Stable(0, 0)) { Alpha = 0.05f };
                        l2.AlphaIncrease(T(0.3f), 0.65f);
                        CreateEntity(l1);
                        CreateEntity(l2);
                        DelayBeat(0.4f, () => { l1.Dispose(); l2.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(2, T(0.5f), false);
                    });
                    RegisterFunctionOnce("L6", () =>
                    {
                        Line l = new(new Vector2(Rand(120, 520), 0), EaseOut(T(0.5f), 90, Arguments[0],EaseState.Quad)) { Alpha = 0.6f };
                        l.AlphaDecrease(T(0.75f), 0.4f);
                        CreateEntity(l);
                        DelayBeat(0.75f, () => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("L7G", () =>
                    {
                        float random = Rand(0, 359);
                        for (int a = 0; a < 3; a++) {
                            var l = LinkEase(Stable(0, 0), EaseOut(T(6), 480,EaseState.Quad));
                            var r = LinkEase(Stable(0, random + a * 120), EaseOut(T(6), 280, EaseState.Sine));
                            var r2 = LinkEase(Stable(0, random + a * 120+180), EaseOut(T(6), 280, EaseState.Sine));
                            var p1 = Add(Polar(l, r), Stable(0, 320, 240));
                            var p2 = Add(Polar(l, r2), Stable(0, 320, 240));
                            Line L = new(p1,p2);
                            CreateEntity(L);
                            DelayBeat(2, () => { L.AlphaDecrease(T(6)); });
                        }
                    });
                    RegisterFunctionOnce("L8G", () =>
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(i * 0.25f, () =>
                            {
                                Line l1 = new(EaseIn(T(1.5f), new Vector2(320, 240), new Vector2(320, -240), EaseState.Sine),
                                    Stable(T(1.5f), 0))
                                { Alpha = 0 };
                                Line l2=new(EaseIn(T(1.5f),new Vector2(320,240),new Vector2(320 - 480*Cos(30),480),EaseState.Sine),
                                    Stable(T(1.5f), 60))
                                { Alpha=0 };
                                Line l3 = new(EaseIn(T(1.5f), new Vector2(320, 240), new Vector2(320 + 480 * Cos(30), 480), EaseState.Sine),
                                    Stable(T(1.5f), 120))
                                { Alpha = 0 };
                                Line[]lines = { l1, l2, l3 };
                                foreach(Line l  in lines)
                                {
                                    CreateEntity(l);
                                    l.AlphaIncrease(T(1.5f), 0.85f);
                                    DelayBeat(2,() => { l.Dispose(); });
                                }
                            });
                        }
                    });
                    RegisterFunctionOnce("Start", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(5, T(4));
                        ScreenDrawing.CameraEffect.Convulse(10, T(4), true);
                    });
                    RegisterFunctionOnce("Shake", () =>
                    {
                        AddInstance(new ScreenShaker(10, 5, BeatTime(0.1f)));
                    });
                    RegisterFunctionOnce("Shake2", () =>
                    {
                        var eas = LinkEase(Linear(1, new Vector2(0,-12)), Linear(T(1f) - 4, new Vector2(0,12)));
                        RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, eas);
                        var eas2 = LinkEase(Linear(1, Arguments[0]), Linear(T(0.5f) - 4, -Arguments[0]));
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, eas2);
                    });
                    RegisterFunctionOnce("Piano", () =>
                    {
                        var eas = LinkEase(Stable(0, 0, 105), EaseOut(T(3), new Vector2(0, -145), EaseState.Sine));
                        var rot = LinkEase(Linear(T(4), Rand(-15,15)));
                        Line l = new(eas, rot) { Alpha=0.7f};
                        CreateEntity(l);
                        l.AlphaDecrease(T(2));
                        l.VerticalMirror = true;
                    });
                    RegisterFunctionOnce("FinalLine", () =>
                    {
                        var eas = LinkEase(EaseIn(T(6), new Vector2(320, 0), EaseState.Sine));
                        var rot = LinkEase(Stable(0, 90), Linear(T(6), -60));
                        Line l = new(eas, rot);
                        l.AlphaDecrease(T(4));
                        CreateEntity(l);
                        l.TransverseMirror = true;
                        l.VerticalMirror = true;
                        l.ObliqueMirror = true;
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 7.3f, new string[]
                    {
                        "(<1,0,80>UpB)(<1,0,80>DownB)", "", "", "",         "", "", "", "",
                        "R(Line)(Start)(<2,80>BoundBack)(Piano)", "", "", "",         "", "", "", "",
                        "#1.8#R1(<75,1>L1)(Piano)", "", "", "",         "", "", "", "",
                        "D(Piano)", "", "", "",         "", "", "", "",

                        "R1(<75,1>L1)", "", "", "",         "(Piano)", "", "", "",
                        "R", "", "", "",         "(Piano)", "", "", "",
                        "R1(<105,0>L1)(Piano)", "", "", "",         "R(Piano)", "", "", "",
                        "", "", "", "",         "R(Piano)", "", "", "",

                        "R1(<105,0>L1)(Piano)", "", "", "",         "", "", "", "",
                        "(#1.8#R)(#1.8#D1)(Line)(<2,140>BoundBack)(Shake)(<1,1.15>SSS)(Piano)", "", "", "",         "", "", "", "",
                        "(<1,1>SSS)(Piano)", "", "", "",         "", "", "", "",
                        "R(Piano)", "", "", "",         "R", "", "", "",

                        "R", "", "", "",         "R(Piano)", "", "", "",
                        "", "", "", "",         "(Piano)", "", "", "",
                        "R1(<75,0>L1)(Piano)", "", "", "",         "R(Piano)", "", "R", "",
                        "R(<160,1.5>L3A)", "", "", "",         "(R)(<320,1>L3B)(Piano)", "", "", "",
                        //
                        "R(<105,1>L1)(<480,0.5>L3A)(Piano)", "", "", "",         "R", "", "", "",
                        "R(L4)(Piano)", "", "", "",         "*$01(<0.5,40>BoundBack)(<2>Shake2)", "", "*+211", "",
                        "*+21", "", "", "",         "*$21 (<0.5,35>BoundBack)(<-2>Shake2)(Piano)", "", "*+211", "",
                        "*+21", "", "", "",         "*$01(<0.5,30>BoundBack)(<2>Shake2)", "", "*+211", "",

                        "*+21(Piano)", "", "", "",         "*$21(<0.5,25>BoundBack)(<-2>Shake2)", "", "*+211", "",
                        "(*$0)(*$2)(L5A)(Piano)(Piano)", "", "", "",        "(*$0)(*$2)(L5B)(Piano)(Piano)", "", "", "",
                        "", "", "", "",         "R(<75>L6)", "", "", "",
                        "(R1)(<105>L6)", "", "", "",         "R(<75>L6)", "", "", "",

                        "R(<105>L6)", "", "", "",         "", "", "", "",
                        "(R1)(L7G)(<3.5,80>BoundBack)(<3.5,1.15>SSS)(FinalLine)", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "(R)", "", "", "",         "R", "", "", "",

                        "", "", "", "",         "($3)(L8G)(<2,105,0>UpB)(<2,105,0>DownB)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
                }
                if (InBeat(35.25f))
                {
                    AddInstance(new ScreenShaker(10, 5, BeatTime(0.1f)));
                    DrawingUtil.SetScreenScale(1.3f, BeatTime(0.5f));
                }
                if (InBeat(35.75f, 36) && At0thBeat(0.05f)) ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle >= 0 ? -1.2f : 1.2f;
                if (InBeat(36)) ScreenDrawing.ScreenAngle = 0;
                if (InBeat(36)) DrawingUtil.SetScreenScale(1, BeatTime(1));
                
                if(InBeat(65f)) DrawingUtil.BetterBlackScreen(BeatTime(1f), BeatTime(1.5f), BeatTime(0.5f), Color.Black);
                if (InBeat(64.5f))
                {
                    float rotArg = 360 * 4;
                    float rotSta = 30;
                    var len = LinkEase(Stable(0, 80), Linear(T(32), 120));
                    var rota = LinkEase(Stable(0, rotSta), Linear(T(32), rotArg));
                    var rotb = LinkEase(Stable(0, rotSta+90), Linear(T(32), rotArg));
                    var rotc = LinkEase(Stable(0, rotSta + 180), Linear(T(32), rotArg));
                    var rotd = LinkEase(Stable(0, rotSta + 270), Linear(T(32), rotArg));
                    var lseas1 = Add(Polar(len, rota),Stable(0,320,240));
                    var lseas2 = Add(Polar(len, rotb), Stable(0, 320, 240));
                    var lseas3 = Add(Polar(len, rotc), Stable(0, 320, 240));
                    var lseas4 = Add(Polar(len, rotd), Stable(0, 320, 240));
                    Line ls1 = new(lseas1, Stable(0,0,0)) { Alpha=0 };
                    Line ls2 = new(lseas2, Stable(0, 640, 0)) { Alpha = 0 };
                    Line ls3 = new(lseas3, Stable(0, 640, 480)) { Alpha = 0 };
                    Line ls4 = new(lseas4, Stable(0, 0, 480)) { Alpha = 0 };
                    CreateEntity(ls1,ls2,ls3,ls4);
                    RegisterFunctionOnce("SS", () => { SetSoul(1); });//SoulShine
                    RegisterFunctionOnce("L1", () =>
                    {
                        Line l1 = new(EaseOut(T(0.7f), new Vector2(0, 240), new Vector2(80, 240), EaseState.Quad), Stable(0, 90)) { Alpha = 0.75f };
                        Line l2 = new(EaseOut(T(0.7f), new Vector2(320, 0), new Vector2(320, 60), EaseState.Quad), Stable(0, 0)) { Alpha = 0.75f };
                        l1.AlphaDecrease(T(0.7f), 0.75f);
                        l1.TransverseMirror = true;
                        l2.AlphaDecrease(T(0.7f), 0.75f);
                        l2.VerticalMirror = true;
                        CreateEntity(l1);
                        CreateEntity(l2);
                        DelayBeat(0.7f, () => { l1.Dispose(); l2.Dispose(); });
                    });
                    RegisterFunctionOnce("LS", () =>
                    {
                        /*var eas = LinkEase(Stable(0, 0), EaseOut(T(Arguments[0])/2, 1, EaseState.Quad), EaseIn(T(Arguments[0]) / 2, -1, EaseState.Quad));
                        RunEase((s) => { ls1.Alpha = ls2.Alpha = ls3.Alpha = ls4.Alpha = s; },eas);*/
                        var eas = LinkEase(Stable(0, Arguments[0] * (640f / 8f),240));
                        var rot = LinkEase(Stable(0, 90), EaseOut(T(1.5f), Arguments[1], EaseState.Quad));
                        Line l = new(eas,rot);
                        CreateEntity(l);
                        l.AlphaDecrease(T(2));
                    });
                    RegisterFunctionOnce("DepPiano", () =>
                    {
                        Line l1 = new(EaseOut(T(4), new Vector2(240,0), EaseState.Sine),Stable(0,90)) { Alpha = 0.25f,TransverseMirror=true};
                        Line l2 = new(EaseOut(T(4), new Vector2(240/4f*3,0), EaseState.Sine), Stable(0, 90)){ Alpha=0.5f, TransverseMirror = true };
                        Line l3 = new(EaseOut(T(4), new Vector2(240 / 4f * 2,0), EaseState.Sine), Stable(0, 90)) { Alpha = 0.75f, TransverseMirror = true };
                        Line l4 = new(EaseOut(T(4), new Vector2(240 / 4f * 1,0), EaseState.Sine), Stable(0, 90)) { Alpha = 1f, TransverseMirror = true };
                        CreateEntity(l1, l2, l3, l4);
                        l1.AlphaDecrease(T(2.5f));
                        l2.AlphaDecrease(T(2.5f));
                        l3.AlphaDecrease(T(2.5f));
                        l4.AlphaDecrease(T(2.5f));
                        
                    });
                    Arrow.UnitEasing eA = new();
                    AddInstance(eA);
                    eA.ApplyTime = BeatTime(36);
                    eA.TagApply("A");
                    
                    eA.AlphaEase = LinkEase(
                        Stable(T(1+32), 0f),
                        EaseOut(T(1), 1, EaseState.Quad));
                    eA.PositionEase = LinkEase(
                        Stable(T(2+32),0, (380 - 282)),
                        EaseIn(T(2),new Vector2(0,-(380-282)),EaseState.Quad)
                        );
                    CreateChart(BeatTime(4), BeatTime(1), 5f, new string[]
                    {
                        "(R)", "", "", "",         "(+0)", "", "", "",
                        "(+201)", "", "", "",         "", "", "", "",
                        "", "", "", "",         "(R01'0.8)", "", "", "",
                        "", "", "", "",         "(R01'0.8)", "", "", "",

                        "(R01'0.8)", "", "", "",         "(+001'0.8)", "", "", "",
                        "(R01'0.8)", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "(R01'0.8)", "", "", "",         "(R01'0.8)", "", "", "",
                        "(R01'0.8)", "", "", "",         "", "", "", "",
                        "(R01'0.8)", "", "", "",         "(R01'0.8)", "", "", "",
                        "", "", "", "",         "(R01'0.8)", "", "", "",

                        "", "", "", "",         "(R01'0.8)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
                        "(R01'0.8)", "", "", "",         "(R01'0.8)", "", "", "",
                        "(R01'0.8)", "", "", "",         "", "", "", "",
                        "(R01'0.8)", "", "", "",         "(R01'0.8)", "", "", "",
                        "", "", "", "",         "(R01'0.8)", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "(R01'0.8)", "", "", "",         "", "", "", "",
                        "(R01'0.8)", "", "", "",         "(R01'0.8)", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "(R01'0.8)", "", "", "",         "(R01'0.8)", "", "", "",
                        "(R01'0.8)", "", "", "",         "", "", "", "",
                        "(R01'0.8)", "", "", "",         "", "", "", "",
                        "(R01'0.8)", "", "", "",         "(R01'0.8)", "", "", "",

                        "(R01'0.8)", "", "", "",         "(R01'0.8)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "($1'1.2)(<1,1>SSS)(SS)", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 0f, new string[]
                    {
                        "", "", "", "",         "", "", "", "",
                        "~_$11@A(<3,12>LS)(DepPiano)", "", "", "",         "~_$11@A(<4,12>LS)", "", "", "",
                        "", "", "", "",         "~_$11@A(<6,-12>LS)", "", "", "",
                        "", "", "", "",         "~_$11@A(<5,-12>LS)", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "~_$11@A(<7,12>LS)(DepPiano)", "", "", "",         "~_$11@A(<6,12>LS)", "", "", "",
                        "~_$11@A(<5,12>LS)", "", "", "",         "~_$11@A(<4,12>LS)", "", "", "",
                        "", "", "", "",         "~_$11@A(<1,-12>LS)", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "~_$11@A(<6,12>LS)(DepPiano)", "", "", "",         "~_$11@A(<5,12>LS)", "", "", "",
                        "", "", "", "",         "~_$11@A(<3,12>LS)", "", "", "",
                        "", "", "", "",         "~_$11@A(<2,12>LS)", "", "", "",

                        "", "", "", "",         "~_$11@A(<1,12>LS)(DepPiano)", "", "", "",
                        "", "", "", "",         "~_$11@A(<7,12>LS)", "", "", "",
                        "~_$11@A(<6,12>LS)", "", "", "",         "~_$11@A(<5,12>LS)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
                        "~_$11@A(<1,-12>LS)", "", "", "",         "~_$11@A(<2,-12>LS)", "", "", "",
                        "~_$11@A(<3,-12>LS)(DepPiano)", "", "", "",         "~_$11@A(<4,-12>LS)", "", "", "",
                        "~_$11@A(<5,-12>LS)", "", "", "",         "~_$11@A(<6,-12>LS)", "", "", "",
                        "", "", "", "",         "~_$11@A(<1,-12>LS)", "", "", "",

                        "~_$11@A(<2,-12>LS)", "", "", "",         "~_$11@A(<3,-12>LS)", "", "", "",
                        "~_$11@A(<4,-12>LS)(DepPiano)", "", "", "",         "", "", "", "",
                        "~_$11@A(<7,12>LS)", "", "", "",         "~_$11@A(<6,12>LS)", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "~_$11@A(<1,-12>LS)", "", "", "",         "~_$11@A(<2,-12>LS)", "", "", "",
                        "~_$11@A(<3,-12>LS)(DepPiano)", "", "", "",         "", "", "", "",
                        "~_$11@A(<5,12>LS)", "", "", "",         "", "", "", "",
                        "~_$11@A(<3,12>LS)", "", "", "",         "~_$11@A(<4,12>LS)", "", "", "",

                        "~_$11@A(<5,12>LS)", "", "", "",         "~_$11@A(<6,12>LS)(DepPiano)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "~_$11@A(DepPiano)", "", "", "",         "", "", "", "",
                    });
                }
                if (InBeat(96.5f))
                {
                    RegisterFunctionOnce("LS", () =>
                    {
                        /*var eas = LinkEase(Stable(0, 0), EaseOut(T(Arguments[0])/2, 1, EaseState.Quad), EaseIn(T(Arguments[0]) / 2, -1, EaseState.Quad));
                        RunEase((s) => { ls1.Alpha = ls2.Alpha = ls3.Alpha = ls4.Alpha = s; },eas);*/
                        var eas = LinkEase(Stable(0, (8-Arguments[0]) * (640f / 8f), 240));
                        var rot = LinkEase(Stable(0, 90), EaseOut(T(1.5f), -Arguments[1], EaseState.Quad));
                        Line l = new(eas, rot);
                        CreateEntity(l);
                        l.AlphaDecrease(T(2));
                    });
                    RegisterFunctionOnce("jumpLine1", () =>
                    {
                        var eas = LinkEase(
                            Stable(0, 320, 480),
                            EaseOut(T(0.5f), new Vector2(0, -60), EaseState.Sine),
                            EaseOut(T(0.5f), new Vector2(0, -60), EaseState.Sine),
                            EaseOut(T(0.5f),new Vector2(0,80),EaseState.Quad),
                            EaseIn(T(0.5f),new Vector2(0,-80),EaseState.Quad),
                            EaseOut(T(0.5f), new Vector2(0, -80), EaseState.Quad),
                            EaseOut(T(0.5f), new Vector2(0, 80), EaseState.Quad),
                            EaseIn(T(0.5f), new Vector2(0, -80), EaseState.Quad),
                            EaseOut(T(0.5f), new Vector2(0, -80), EaseState.Sine)
                            );
                        var rot = LinkEase(
                            Stable(T(1), 0),
                            EaseOut(T(0.5f),45, EaseState.Quad),
                            EaseIn(T(0.5f),-45,EaseState.Quad),
                            Stable(T(0.5f), 0),
                            EaseOut(T(0.5f), -45, EaseState.Quad),
                            EaseIn(T(0.5f), 45, EaseState.Quad)
                            );
                        Line l = new(eas, rot) { VerticalMirror=true,ObliqueMirror=true,TransverseMirror=true,Alpha=0.5f};
                        CreateEntity(l);
                        DelayBeat(3, () => { l.AlphaDecrease(T(1)); });
                    });
                    RegisterFunctionOnce("jumpLine2", () =>
                    {
                        var eas = LinkEase(
                            Stable(0, 640, 240),
                            EaseOut(T(0.5f), new Vector2(-60,0), EaseState.Sine),
                            EaseOut(T(0.5f), new Vector2(-60,0), EaseState.Sine),
                            EaseOut(T(0.5f), new Vector2(80,0), EaseState.Quad),
                            EaseIn(T(0.5f), new Vector2(-80,0), EaseState.Quad),
                            EaseOut(T(0.5f), new Vector2(-80,0), EaseState.Quad),
                            EaseOut(T(0.5f), new Vector2(80, 0), EaseState.Quad),
                            EaseIn(T(0.5f), new Vector2(-80, 0), EaseState.Quad),
                            EaseOut(T(0.5f), new Vector2(-80, 0), EaseState.Sine)
                            );
                        var rot = LinkEase(
                            Stable(T(1), 90),
                            EaseOut(T(0.5f), 50, EaseState.Quad),
                            EaseIn(T(0.5f), -50, EaseState.Quad),
                            Stable(T(0.5f), 90),
                            EaseOut(T(0.5f), -50, EaseState.Quad),
                            EaseIn(T(0.5f), 50, EaseState.Quad)
                            );
                        Line l = new(eas, rot) { VerticalMirror = true, ObliqueMirror = true, TransverseMirror = true, Alpha = 0.5f };
                        CreateEntity(l);
                        DelayBeat(3, () => { l.AlphaDecrease(T(1)); });
                    });
                    RegisterFunctionOnce("jumpLine3", () =>
                    {
                        var eas = LinkEase(
                            Stable(0, 320, 480),
                            EaseOut(T(0.5f), new Vector2(0, -60), EaseState.Sine),
                            EaseOut(T(0.5f), new Vector2(0, -60), EaseState.Sine),
                            EaseOut(T(0.5f), new Vector2(0, 80), EaseState.Quad),
                            EaseIn(T(0.5f), new Vector2(0, -80), EaseState.Quad),
                            EaseOut(T(0.5f), new Vector2(0, 80), EaseState.Quad),
                            EaseIn(T(0.5f), new Vector2(0, -80), EaseState.Quad),
                            EaseOut(T(0.5f), new Vector2(0, -80), EaseState.Sine)
                            );
                        var rot = LinkEase(
                            Stable(T(1), 0),
                            EaseOut(T(0.5f), 45, EaseState.Quad),
                            EaseIn(T(0.5f), -45, EaseState.Quad),
                            EaseOut(T(0.5f), -45, EaseState.Quad),
                            EaseIn(T(0.5f), 45, EaseState.Quad)
                            );
                        Line l = new(eas, rot) { VerticalMirror = true, ObliqueMirror = true, TransverseMirror = true, Alpha = 0.5f };
                        CreateEntity(l);
                        DelayBeat(3, () => { l.AlphaDecrease(T(1)); });
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "R", "", "", "",         "+0", "", "", "",
                        "D1(jumpLine1)", "", "", "",         "D1", "", "", "",
                        "(D)(+01)", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "(D)(+21)", "", "", "",

                        "D1", "", "", "",         "D1", "", "", "",
                        "D(jumpLine2)", "", "", "",         "D", "", "", "",
                        "(D)(+01)", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "(D)(+21)", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D1(jumpLine3)", "", "", "",         "D1", "", "", "",
                        "(D)(+01)", "", "", "",         "D1", "", "", "",
                        "(D)(+21)", "", "", "",         "D1", "", "", "",

                        "", "", "", "",         "D1", "", "", "",
                        "D(jumpLine1)", "", "", "",         "D", "", "", "",
                        "(D)(+01)", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "(D)(+21)", "", "", "",
                        //
                        "D", "", "", "",         "D", "", "", "",
                        "D1(jumpLine2)", "", "", "",         "D1", "", "", "",
                        "(D)(+01)", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "(D)(+21)", "", "", "",

                        "D1", "", "", "",         "D1", "", "", "",
                        "D(jumpLine1)", "", "", "",         "D", "", "", "",
                        "(D)(+01)", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "(D)(+21)", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D1(jumpLine3)", "", "", "",         "D1", "", "", "",
                        "(D)(+01)", "", "", "",         "D1", "", "", "",
                        "(D)(+21)", "", "", "",         "D1", "", "", "",

                        "", "", "", "",         "D1", "", "", "",
                        "D(jumpLine2)", "", "", "",         "D", "", "", "",
                        "(D)(+01)", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "(D)(+21)", "", "", "",
                        //
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "", "", "", "",         "", "", "", "",
                        "(<3,12>LS)", "", "", "",         "(<4,12>LS)", "", "", "",
                        "", "", "", "",         "(<6,-12>LS)", "", "", "",
                        "", "", "", "",         "(<5,-12>LS)", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "(<7,12>LS)", "", "", "",         "(<6,12>LS)", "", "", "",
                        "(<5,12>LS)", "", "", "",         "(<4,12>LS)", "", "", "",
                        "", "", "", "",         "(<1,-12>LS)", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "(<6,12>LS)", "", "", "",         "(<5,12>LS)", "", "", "",
                        "", "", "", "",         "(<3,12>LS)", "", "", "",
                        "", "", "", "",         "(<2,12>LS)", "", "", "",

                        "", "", "", "",         "(<1,12>LS)", "", "", "",
                        "", "", "", "",         "(<7,12>LS)", "", "", "",
                        "(<6,12>LS)", "", "", "",         "(<5,12>LS)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
                        "(<1,-12>LS)", "", "", "",         "(<2,-12>LS)", "", "", "",
                        "(<3,-12>LS)", "", "", "",         "(<4,-12>LS)", "", "", "",
                        "(<5,-12>LS)", "", "", "",         "(<6,-12>LS)", "", "", "",
                        "", "", "", "",         "(<1,-12>LS)", "", "", "",

                        "(<2,-12>LS)", "", "", "",         "(<3,-12>LS)", "", "", "",
                        "(<4,-12>LS)", "", "", "",         "", "", "", "",
                        "(<7,12>LS)", "", "", "",         "(<6,12>LS)", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "(<1,-12>LS)", "", "", "",         "(<2,-12>LS)", "", "", "",
                        "(<3,-12>LS)", "", "", "",         "", "", "", "",
                        "(<5,12>LS)", "", "", "",         "", "", "", "",
                        "(<3,12>LS)", "", "", "",         "(<4,12>LS)", "", "", "",

                        "(<5,12>LS)", "", "", "",         "(<6,12>LS)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "(<2,12>LS)", "", "", "",         "", "", "", "",
                    });
                }
                if (InBeat(128.5f))
                {
                    RegisterFunctionOnce("RL1", () =>
                    {
                        var x = LinkEase(Stable(0, 0), Linear(T(2), 360));
                        var y = LinkEase(Stable(0, 120), EaseOut(T(2), 360, EaseState.Quad));
                        var eas = Combine(x, y);
                        var rot = LinkEase(Stable(0, 60), Linear(T(2), -70));
                        Line l = new(eas,rot) { Alpha = 0 };
                        CreateEntity(l);
                        var al = LinkEase(Stable(0, 0), Linear(T(1f), 0.8f), Linear(T(1f), -1.2f),Stable(T(2),0));
                        RunEase((s) => { l.Alpha = s; }, al);                    
                    });
                    RegisterFunctionOnce("RL2", () =>
                    {
                        var x = LinkEase(Stable(0, 640), Linear(T(2), -360));
                        var y = LinkEase(Stable(0, 360), EaseOut(T(2), -360, EaseState.Quad));
                        var eas = Combine(x, y);
                        var rot = LinkEase(Stable(0, 60+180), Linear(T(2), -70));
                        Line l = new(eas, rot) { Alpha = 0 };
                        CreateEntity(l);
                        var al = LinkEase(Stable(0, 0), Linear(T(1f), 0.8f), Linear(T(1f), -1.2f), Stable(T(2), 0));
                        RunEase((s) => { l.Alpha = s; }, al);
                    });
                    RegisterFunctionOnce("RL3", () =>
                    {
                        var x = LinkEase(Stable(0, 360), EaseOut(T(2), -360, EaseState.Quad));
                        var y = LinkEase(Stable(0, 120), Linear(T(2), 360));
                        var eas = Combine(x, y);
                        var rot = LinkEase(Stable(0, 60), Linear(T(2), -70));
                        Line l = new(eas, rot) { Alpha = 0 };
                        CreateEntity(l);
                        var al = LinkEase(Stable(0, 0), Linear(T(1f), 0.8f), Linear(T(1f), -1.2f), Stable(T(2), 0));
                        RunEase((s) => { l.Alpha = s; }, al);
                    });
                    RegisterFunctionOnce("RL4", () =>
                    {
                        var x = LinkEase(Stable(0, 640), Linear(T(2), -360));
                        var y = LinkEase(Stable(0, 120), EaseOut(T(2), 360, EaseState.Quad));
                        var eas = Combine(x, y);
                        var rot = LinkEase(Stable(0, 60+90), Linear(T(2), -70));
                        Line l = new(eas, rot) { Alpha = 0 };
                        CreateEntity(l);
                        var al = LinkEase(Stable(0, 0), Linear(T(1f), 0.8f), Linear(T(1f), -1.2f), Stable(T(2), 0));
                        RunEase((s) => { l.Alpha = s; }, al);
                    });
                    RegisterFunctionOnce("SRL1", () =>
                    {
                        var x = LinkEase(Stable(0, 0), Linear(T(2), 360));
                        var y = LinkEase(Stable(0, 120), EaseOut(T(2), 360, EaseState.Quad));
                        var eas = Combine(x, y);
                        var rot = LinkEase(Stable(0, 60), Linear(T(2), -70));
                        Line l = new(eas, rot) { Alpha = 0 };
                        CreateEntity(l);
                        var al = LinkEase(Stable(0, 0), Linear(T(1f), 0.8f), Linear(T(1f), -1.2f), Stable(T(2), 0));
                        RunEase((s) => { l.Alpha = s; }, al);
                    });
                    RegisterFunctionOnce("SRL2", () =>
                    {
                        var x = LinkEase(Stable(0, 640), Linear(T(2), -360));
                        var y = LinkEase(Stable(0, 360), EaseOut(T(2), -360, EaseState.Quad));
                        var eas = Combine(x, y);
                        var rot = LinkEase(Stable(0, 60 ), Linear(T(2), 70));
                        Line l = new(eas, rot) { Alpha = 0 };
                        CreateEntity(l);
                        var al = LinkEase(Stable(0, 0), Linear(T(1f), 0.8f), Linear(T(1f), -1.2f), Stable(T(2), 0));
                        RunEase((s) => { l.Alpha = s; }, al);
                    });
                    RegisterFunctionOnce("SRL3", () =>
                    {
                        var x = LinkEase(Stable(0, 0), Linear(T(2), 360));
                        var y = LinkEase(Stable(0, 360), EaseOut(T(2), -360, EaseState.Quad));
                        var eas = Combine(x, y);
                        var rot = LinkEase(Stable(0, 60 + 270), Linear(T(2), -70));
                        Line l = new(eas, rot) { Alpha = 0 };
                        CreateEntity(l);
                        var al = LinkEase(Stable(0, 0), Linear(T(1f), 0.8f), Linear(T(1f), -1.2f), Stable(T(2), 0));
                        RunEase((s) => { l.Alpha = s; }, al);
                    });
                    RegisterFunctionOnce("SRL4", () =>
                    {
                        var x = LinkEase(Stable(0, 640), Linear(T(2), -360));
                        var y = LinkEase(Stable(0, 120), EaseOut(T(2), 360, EaseState.Quad));
                        var eas = Combine(x, y);
                        var rot = LinkEase(Stable(0, 60 + 90), Linear(T(2), -70));
                        Line l = new(eas, rot) { Alpha = 0 };
                        CreateEntity(l);
                        var al = LinkEase(Stable(0, 0), Linear(T(1f), 0.8f), Linear(T(1f), -1.2f), Stable(T(2), 0));
                        RunEase((s) => { l.Alpha = s; }, al);
                    });
                    RegisterFunctionOnce("CamDelta", () =>
                    {
                        var camEase = LinkEase(
                            Stable(0, 0, 0),
                            Combine(Linear(T(2),20),EaseIn(T(2),-20,EaseState.Quad)),
                            Combine(EaseIn(T(2), -20, EaseState.Quad), Linear(T(2), 20)),
                            Combine(EaseOut(T(2), 20, EaseState.Quad), Linear(T(2), 20)),
                            Combine(Linear(T(2), -20),EaseIn(T(2), -20, EaseState.Quad)),
                            Combine(Linear(T(2), -20), EaseOut(T(2), 20, EaseState.Quad)),
                            Combine(EaseIn(T(2), 10, EaseState.Quad), Linear(T(2), -10)),
                            Combine(EaseOut(T(2), -10, EaseState.Quad), Linear(T(2), -10)),
                            Combine(EaseOut(T(2), 20,EaseState.Quad), EaseIn(T(2), 0, EaseState.Quad))
                            );
                        RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; },camEase);
                    });
                    RegisterFunctionOnce("CamAngle", () =>
                    {
                        var camEase = LinkEase(
                            Stable(0, 0),
                            EaseOut(T(2),7,EaseState.Sine),
                            EaseIn(T(2),-7,EaseState.Sine),
                            EaseOut(T(2),-7,EaseState.Sine),
                            EaseIn(T(2), 7, EaseState.Sine),
                            EaseOut(T(2), 7, EaseState.Sine),
                            EaseIn(T(2), -7, EaseState.Sine),
                            EaseOut(T(2), -7, EaseState.Sine),
                            EaseIn(T(1), 3.5f, EaseState.Sine),
                            EaseOut(T(1), 3.5f, EaseState.Sine)
                            );
                        RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, camEase);
                    });
                    RegisterFunctionOnce("LeftLine", () =>
                    {
                        var eas = LinkEase(Stable(0, 160,0), EaseIn(T(1), new Vector2(160,0), EaseState.Quart));
                        var eas2 = LinkEase(Stable(0, 160, 0), EaseOut(T(0.5f), new Vector2(45, 0), EaseState.Quad));
                        var eas3 = LinkEase(Stable(0, 160, 0), EaseOut(T(0.5f), new Vector2(-45, 0), EaseState.Quad));
                        Line l = new(eas, Stable(0, 90));
                        Line l2 = new(eas2, Stable(0, 90));
                        Line l3 = new(eas3, Stable(0, 90));
                        l.DelayDispose(T(1));
                        l2.AlphaDecrease(T(0.5f));
                        l3.AlphaDecrease(T(0.5f));
                        CreateEntity(l,l2,l3);

                    });
                    RegisterFunctionOnce("RightLine", () =>
                    {
                        var eas = LinkEase(Stable(0, 480, 0), EaseIn(T(0.5f), new Vector2(-160, 0), EaseState.Quart));
                        var eas2 = LinkEase(Stable(0, 480, 0), EaseOut(T(0.25f), new Vector2(45, 0), EaseState.Quad));
                        var eas3 = LinkEase(Stable(0, 480, 0), EaseOut(T(0.25f), new Vector2(-45, 0), EaseState.Quad));
                        Line l = new(eas, Stable(0, 90));
                        Line l2 = new(eas2, Stable(0, 90));
                        Line l3 = new(eas3, Stable(0, 90));
                        l.DelayDispose(T(0.5f));
                        l2.AlphaDecrease(T(0.25f));
                        l3.AlphaDecrease(T(0.25f));
                        CreateEntity(l, l2, l3);

                    });
                    RegisterFunctionOnce("DanceLine", () =>
                    {
                        var eas = LinkEase(
                            Stable(0, 320, 0),
                            EaseOut(T(0.5f),new Vector2(84,0),EaseState.Cubic),
                            EaseIn(T(0.5f), new Vector2(-84, 0), EaseState.Cubic),
                            EaseOut(T(0.5f), new Vector2(-84, 0), EaseState.Cubic),
                            EaseIn(T(0.5f), new Vector2(84, 0), EaseState.Cubic),
                            EaseOut(T(0.5f), new Vector2(84, 0), EaseState.Cubic),
                            EaseIn(T(0.5f), new Vector2(-84, 0), EaseState.Cubic),
                            EaseOut(T(0.5f), new Vector2(-84, 0), EaseState.Cubic),
                            EaseIn(T(0.5f), new Vector2(84, 0), EaseState.Cubic),
                            EaseOut(T(1f), new Vector2(-84, 0), EaseState.Cubic),
                            EaseOut(T(0.5f), new Vector2(20, 0), EaseState.Cubic),
                            EaseOut(T(0.5f), new Vector2(-260, 0), EaseState.Cubic)
                            );
                        Line l = new(eas, Stable(0, 90));
                        CreateEntity(l);
                        DelayBeat(4, () => { l.TransverseMirror = true; });
                        DelayBeat(8, () => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("DoubleLine", () =>
                    {
                        var eas1 = LinkEase(
                            Stable(0, 0,240), 
                            EaseOut(T(1), new Vector2(480, 0), EaseState.Quad),
                            Stable(T(3),480,240),
                            EaseOut(T(1),new Vector2(0,-245), EaseState.Cubic)
                            );
                        var eas2 = LinkEase(
                            Stable(T(0.5f), 645, 240),
                            EaseOut(T(1), new Vector2(-485, 0), EaseState.Quad),
                            Stable(T(2.5f), 160, 240),
                            EaseOut(T(1), new Vector2(0, -245), EaseState.Cubic)
                            );
                        var rot1 = LinkEase(
                            Stable(T(1), 90),
                            EaseOut(T(0.5f), -45, EaseState.Quad),
                            EaseIn(T(0.5f), 45, EaseState.Quad),
                            EaseOut(T(0.5f), 45, EaseState.Quad),
                            EaseIn(T(0.5f), -45, EaseState.Quad),
                            EaseOut(T(0.5f), -90, EaseState.Quad));
                        Line l = new(eas1, rot1);
                        Line l2 = new(eas2, rot1);
                        CreateEntity(l,l2);
                        
                    });
                    RegisterFunctionOnce("UpLine", () =>
                    {
                        Line l = new(LinkEase(Stable(0, 0, 240), EaseOut(T(1), new Vector2(0, -245), EaseState.Cubic)), Stable(0, 0));
                        CreateEntity(l);
                        DelayBeat(2, () => { l.Dispose(); });

                    });
                    RegisterFunctionOnce("DownLine", () =>
                    {
                        Line l = new(LinkEase(Stable(0, 0, 240), EaseOut(T(1), new Vector2(0, 245), EaseState.Cubic)), Stable(0, 0));
                        CreateEntity(l);
                        DelayBeat(2, () => { l.Dispose(); });

                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "#1#$0(RL1)", "(RL1)", "(RL1)", "(RL1)",         "(RL1)", "(RL1)", "(RL1)", "(RL1)",

                        "#1#$21(RL1)(RL2)", "(RL1)(RL2)", "(RL1)(RL2)", "(RL1)(RL2)",         "(RL1)(RL2)", "(RL1)(RL2)", "(RL1)(RL2)", "(RL1)(RL2)",
                        "#3#R(CamDelta)(CamAngle)(RL1)(RL2)(RL3)(RL4)", "(RL1)(RL2)(RL3)(RL4)", "(RL1)(RL2)(RL3)(RL4)", "(RL1)(RL2)(RL3)(RL4)",         "(RL1)(RL2)(RL3)(RL4)", "(RL1)(RL2)(RL3)(RL4)", "(RL1)(RL2)(RL3)(RL4)", "(RL1)(RL2)(RL3)(RL4)",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "+0", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "+0", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "+0", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "+0", "", "", "",
                        "#1#D", "", "", "",         "", "", "", "",
                        "+0", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        //
                        "", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "+0", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "+0", "", "", "",         "D", "", "", "",

                        "D(LeftLine)", "", "", "",         "+0(RightLine)", "", "", "",
                        "R(<1,5>SA)(<1,1.05>SSS)(DanceLine)", "", "", "",         "", "", "", "",
                        "R(<1,-4>SA)(<1,1.1>SSS)", "", "", "",         "", "", "", "",
                        "R(<1,3>SA)(<1,1.15>SSS)", "", "", "",         "", "", "", "",

                        "R(<1,-2>SA)(<1,1.2>SSS)", "", "", "",         "", "", "", "",
                        "D(<1,1>SA)(<1,1.25>SSS)", "", "", "",         "+0", "", "", "",
                        "D(<0.5,-1>SA)(<0.5,1.1>SSS)", "", "", "",         "D(<0.5,0>SA)(<1,1>SSS)", "", "", "",
                        "+0", "", "", "",         "D", "", "", "",

                        "D(DoubleLine)", "", "", "",         "+0", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        //
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "(R)(R1)", "", "", "",         "(R)(R1)", "", "", "",
                        "(R)(R1)", "", "", "",         "(*$0'1.2)(*$2'1.2)", "", "(*$01'1.2)(*$21'1.2)", "",
                        "(*$0'1.2)(*$2'1.2)", "", "", "",         "($0)", "", "", "",
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "D1", "", "", "",         "+01", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "+01", "", "", "",         "D1", "", "", "",

                        "D1(SRL2)", "(SRL2)", "(SRL2)", "(SRL2)",         "+01(SRL2)", "(SRL2)", "(SRL2)", "(SRL2)",
                        "#3#D1", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "+01", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "+01", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        //
                        "", "", "", "",         "#2#D1", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "D1", "", "", "",         "+01", "", "", "",
                        "D1", "", "", "",         "+01", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "+01", "", "", "",         "D1", "", "", "",

                        "D1", "", "", "",         "+01", "", "", "",
                        "D1", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "+01", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "R1", "", "", "",         "+01", "", "", "",
                        "+01", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        //
                    });
                }
                if (InBeat(160.5f))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "#1#$0", "", "", "",         "", "", "", "",

                        "#1#$21", "", "", "",         "", "", "", "",
                        "#3#R", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "+0", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "+0", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "+0", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "", "", "", "",         "D", "", "", "",
                        //
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "", "", "", "",         "D", "", "", "",
                        //
                        "D", "", "", "",         "#2#D", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "$0", "$21", "$3", "$11",         "$2", "$01", "$1", "$31",
                        "$0", "$21", "$3", "$11",         "$2", "$01", "$1", "$31","$31",
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "D1", "", "", "",         "+01", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "+01", "", "", "",         "D1", "", "", "",

                        "D1", "", "", "",         "+01", "", "", "",
                        "#3#D1", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "+01", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        //
                        "D1", "", "", "",         "D1", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",

                        "D1", "", "", "",         "", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "", "", "", "",         "D1", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        //
                    });
                }
                if (InBeat(197.5f))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "", "", "", "",         "", "D", "D", "",

                        "", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "", "", "D", "D",

                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "", "", "", "",         "", "D", "D", "",

                        "", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "!!3/6","D", "", "D", "", "D", "",
                        //
                        "D", "", "", "",         "", "", "", "D",
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "", "", "", "",         "", "", "D", "",

                        "D", "", "", "",         "", "D", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "D", "",

                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "", "", "", "",         "", "D", "D", "",

                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        //
                        "D"
                    });
                }
                if (InBeat(229.5f))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "", "", "D", "",
                        "D", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "D", "", "D", "",
                        "D", "", "", "",         "D", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
                        "D", "", "", "",         "D", "", "D", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "D", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
                }
                if (InBeat(251.5f))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "", "", "", "",
                        "", "", "", "",         "D", "", "", "",

                        "", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
                        "", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "D", "", "", "",

                        "", "", "", "",         "D", "", "", "",
                        "", "", "", "",         "D", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "D", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
                }
            }

                public void Noob()
            {

            }

            public void Normal()
            {

            }

            #region non
            public void ExtremePlus()
            {

            }

            public void Hard()
            {

            }
            #endregion
        }
    }
}
