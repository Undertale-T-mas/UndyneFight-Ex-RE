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
            public Project() : base(62.5f / (160f / 60f)) { }
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
                Settings.GreenTap = true;
                GametimeDelta = -1.5f - T(3);
                InstantSetGreenBox();
                SetSoul(1);
                InstantTP(320, 240);
                bool delay = true;
                var beat = BeatTime(32);
                if (delay)
                {
                    PlayOffset = beat;
                    GametimeDelta = beat - 1.5f - T(3);
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
                if (InBeat(0))
                {
                    
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
                    CreateChart(BeatTime(4), BeatTime(1), 7f, new string[]
                    {
                        "R(Line1)", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "", "", "", "",         "R(Line2)", "", "", "",

                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
                        "R(Line1)", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "", "", "", "",         "(Line2)", "", "", "",

                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "(L3A)(R)", "+01", "+10", "+01",         "(L3B)(+10)", "+01", "+10", "+01",
                        "+01",
                    });
                }
                if (InBeat(32))
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
                        Line l = new(new Vector2(320, 240), EaseOut(T(2), 90, Arguments[0], EaseState.Quad)){ Alpha = 0.7f };
                        CreateEntity(l);
                        l.AlphaDecrease(T(2), 0.7f);
                        DelayBeat(2, () => { l.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(1.6f, T(1.6f), Arguments[1] == 1);
                    });
                    RegisterFunctionOnce("L2A", () =>
                    {
                        Line l = new(EaseOut(T(1.2f), new Vector2(0, 240), new Vector2(160, 240), EaseState.Sine), Stable(0, 90)) { Alpha = 0.75f };
                        l.AlphaDecrease(T(1.1f), 0.75f);
                        l.TransverseMirror = true;
                        CreateEntity(l);
                        DelayBeat(1.2f,() => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("L2B", () =>
                    {
                        Line l = new(EaseOut(T(1.2f), new Vector2(320, 480), new Vector2(320, 280), EaseState.Sine), Stable(0, 0)) { Alpha = 0.75f };
                        l.AlphaDecrease(T(1.1f), 0.75f);
                        CreateEntity(l);
                        DelayBeat(1.2f, () => { l.Dispose(); });
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
                        Line l = new(LinkEase(EaseOut(T(0.5f), new Vector2(Rand(290, 350), 240), new Vector2(320, 240), EaseState.Elastic),
                            Stable(T(0.5f), new Vector2(320, 240))), Stable(0,90))
                        { Alpha = 0.75f };
                        l.AlphaDecrease(T(1), 0.7f);
                        CreateEntity(l);
                        DelayBeat(1,() => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("L5A", () =>
                    {
                        Line l1 = new(EaseOut(T(0.3f), new Vector2(320, 240), new Vector2(0, 240), EaseState.Linear), Stable(0, 90)) { Alpha = 0.7f };
                        Line l2 = new(EaseOut(T(0.3f), new Vector2(320, 240), new Vector2(320, 0), EaseState.Linear), Stable(0, 0)) { Alpha = 0.05f };
                        l2.AlphaIncrease(T(0.3f), 0.65f);
                        CreateEntity(l1);
                        CreateEntity(l2);
                        DelayBeat(0.4f,() => { l1.Dispose(); l2.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(2, T(0.5f), true);
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
                        Line l = new(new Vector2(Rand(120, 520), 0), 90) { Alpha = 0.4f };
                        l.AlphaDecrease(T(0.5f), 0.4f);
                        CreateEntity(l);
                        DelayBeat(0.5f, () => { l.Dispose(); });
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 7.3f, new string[]
                    {
                        "(<1.6,0,105>UpB)(<1.6,0,105>DownB)", "", "", "",         "", "", "", "",
                        "R(Line)", "", "", "",         "", "", "", "",
                        "#1.8#R1(<75,1>L1)", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "", "", "", "",

                        "R1(<75,1>L1)", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R1(<105,0>L1)", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "R", "", "", "",

                        "R1(<105,0>L1)", "", "", "",         "", "", "", "",
                        "(#1.8#R)(#1.8#D1)(Line)(L2A)", "", "", "",         "(L2A)", "", "", "",
                        "(L2A)", "", "", "",         "(L2A)", "", "", "",
                        "R(L2A)(L2B)", "", "", "",         "R(L2A)(L2B)", "", "", "",

                        "R(L2A)(L2B)", "", "", "",         "R(L2A)(L2B)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "R1(<75,0>L1)", "", "", "",         "R", "", "R", "",
                        "R(<160,1.5>L3A)", "", "", "",         "(R)(<320,1>L3B)", "", "", "",
                        //
                        "R(<105,1>L1)(<480,0.5>L3A)", "", "", "",         "R", "", "", "",
                        "R(L4)", "", "", "",         "*$01(L4)", "", "*+21", "",
                        "*+21", "", "", "",         "*$21(L4)", "", "*+21", "",
                        "*+21", "", "", "",         "*$01(L4)", "", "*+21", "",

                        "*+21", "", "", "",         "*$21(L4)", "", "*+21", "",
                        "(*$0)(*$2)(L5A)", "", "", "",        "(*$0)(*$2)(L5B)", "", "", "",
                        "", "", "", "",         "R", "", "", "",
                        "(R1)", "", "", "",         "R", "", "", "",

                        "R", "", "", "",         "", "", "", "",
                        "(R1)", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "(R)", "", "", "",         "R", "", "", "",

                        "", "", "", "",         "($3)(<2,105,0>UpB)(<2,105,0>DownB)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
                }
                if (InBeat(35))
                {
                    AddInstance(new ScreenShaker(10, 5, BeatTime(0.1f)));
                    DrawingUtil.SetScreenScale(1.3f, BeatTime(0.5f));
                }
                if (InBeat(35.5f, 36) && At0thBeat(0.05f)) ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle >= 0 ? -1.2f : 1.2f;
                if (InBeat(36)) ScreenDrawing.ScreenAngle = 0;
                if (InBeat(36)) DrawingUtil.SetScreenScale(1, BeatTime(1));
                if (InBeat(64.5f)) DrawingUtil.SetScreenScale(1.1f, BeatTime(2));
                if(InBeat(64.5f)) DrawingUtil.BetterBlackScreen(BeatTime(1f), BeatTime(1.5f), BeatTime(0.5f), Color.Black);
                if (InBeat(64))
                {
                    RegisterFunctionOnce("SS", () => { SetSoul(1); });//SoulShine
                    RegisterFunctionOnce("L1", () =>
                    {
                        Line l1 = new(EaseOut(T(0.7f), new Vector2(0, 240), new Vector2(160, 240), EaseState.Quad), Stable(0, 90)) { Alpha = 0.75f };
                        Line l2 = new(EaseOut(T(0.7f), new Vector2(320, 0), new Vector2(320, 120), EaseState.Quad), Stable(0, 0)) { Alpha = 0.75f };
                        l1.AlphaDecrease(T(0.7f), 0.75f);
                        l1.TransverseMirror = true;
                        l2.AlphaDecrease(T(0.7f), 0.75f);
                        l2.VerticalMirror = true;
                        CreateEntity(l1);
                        CreateEntity(l2);
                        DelayBeat(0.7f, () => { l1.Dispose(); l2.Dispose(); });
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 5f, new string[]
                    {
                        "(~_$0)(~_$2)", "", "", "",         "(~_$0)(~_$2)", "", "", "",
                        "(~_$0)(~_$2)(L1)", "", "", "",         "", "", "", "",
                        "(L1)", "", "", "",         "(~_$0)(~_$2)", "", "", "",
                        "(L1)", "", "", "",         "(~_$0)(~_$2)", "", "", "",

                        "(~_$0)(~_$2)(L1)", "", "", "",         "(~_$0)(~_$2)", "", "", "",
                        "(~_$0)(~_$2)(L1)", "", "", "",         "", "", "", "",
                        "(L1)", "", "", "",         "", "", "", "",
                        "(L1)", "", "", "",         "", "", "", "",

                        "(~_$0)(~_$2)(L1)", "", "", "",         "(~_$0)(~_$2)", "", "", "",
                        "(~_$0)(~_$2)(L1)", "", "", "",         "", "", "", "",
                        "(L1)", "", "", "",         "(~_$0)(~_$2)", "", "", "",
                        "(~_$0)(~_$2)(L1)", "", "", "",         "(~_$0)(~_$2)", "", "", "",

                        "(~_$0)(~_$2)(L1)", "", "", "",         "(~_$0)(~_$2)(L1)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "(L1)", "", "", "",         "", "", "", "",
                        "(L1)", "", "", "",         "(L1)", "", "(L1)", "",
                        //
                        "(~_$0)(~_$2)(L1)", "", "", "",         "(~_$0)(~_$2)(L1)", "", "", "",
                        "(~_$0)(~_$2)(L1)", "", "", "",         "", "", "", "",
                        "(~_$0)(~_$2)", "", "", "",         "", "", "", "",
                        "(~_$0)(~_$2)", "", "", "",         "(~_$0)(~_$2)", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "(~_$0)(~_$2)", "", "", "",         "", "", "", "",
                        "(~_$0)(~_$2)", "", "", "",         "(~_$0)(~_$2)", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "(~_$0)(~_$2)", "", "", "",         "(~_$0)(~_$2)", "", "", "",
                        "(~_$0)(~_$2)", "", "", "",         "", "", "", "",
                        "(~_$0)(~_$2)", "", "", "",         "", "", "", "",
                        "(~_$0)(~_$2)", "", "", "",         "(~_$0)(~_$2)", "", "", "",

                        "(~_$0)(~_$2)", "", "", "",         "(~_$0)(~_$2)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",

                        "", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",

                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",

                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "($1'1.2)(<1,1>SSS)(SS)", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "", "", "", "",
                    });
                }
                if (InBeat(96))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "R", "", "", "",         "+0", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "(D)(+01)", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "(D)(+21)", "", "", "",

                        "D1", "", "", "",         "D1", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "(D)(+01)", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "(D)(+21)", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "(D)(+01)", "", "", "",         "D1", "", "", "",
                        "(D)(+21)", "", "", "",         "D1", "", "", "",

                        "", "", "", "",         "D1", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "(D)(+01)", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "(D)(+21)", "", "", "",
                        //
                        "D", "", "", "",         "D", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "(D)(+01)", "", "", "",         "D1", "", "", "",
                        "D1", "", "", "",         "(D)(+21)", "", "", "",

                        "D1", "", "", "",         "D1", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "(D)(+01)", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "(D)(+21)", "", "", "",

                        "D", "", "", "",         "D", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "(D)(+01)", "", "", "",         "D1", "", "", "",
                        "(D)(+21)", "", "", "",         "D1", "", "", "",

                        "", "", "", "",         "D1", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "(D)(+01)", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "(D)(+21)", "", "", "",
                        //
                    });
                }
                if (InBeat(128))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",
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

                        "D", "", "", "",         "+0", "", "", "",
                        "#1#D", "", "", "",         "", "", "", "",
                        "+0", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        //
                        "", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "+0", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "+0", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "+0", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",

                        "R", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "+0", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "+0", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "+0", "", "", "",
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

                        "D1", "", "", "",         "+01", "", "", "",
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
                if (InBeat(164))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "R1", "", "", "",         "R1", "", "", "",
                        "", "", "", "",         "R1", "", "", "",
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

                        "D", "", "", "",         "+0", "", "", "",
                        "#1#D", "", "", "",         "", "", "", "",
                        "+0", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        //
                        "", "", "", "",         "D", "", "", "",
                        "D", "", "", "",         "+0", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "+0", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "+0", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",

                        "R", "", "", "",         "", "", "", "",
                        "D", "", "", "",         "+0", "", "", "",
                        "D", "", "", "",         "D", "", "", "",
                        "+0", "", "", "",         "D", "", "", "",

                        "D", "", "", "",         "+0", "", "", "",
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

                        "D1", "", "", "",         "+01", "", "", "",
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
                if (InBeat(192))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6.3f, new string[]
                    {
                        "D", "", "", "",         "D", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",

                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        //
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
