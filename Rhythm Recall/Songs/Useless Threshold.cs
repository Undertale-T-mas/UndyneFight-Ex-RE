using Extends;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static AprilExtends.Stasis;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    internal class UselessThreshold : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public UselessThreshold()
        {
            dif.Add("Div.3", Difficulty.Easy);
            dif.Add("Div.2", Difficulty.Normal);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (220f / 60f)) { }
            public string Music => "Useless Threshold";
            public string FightName => "Useless Threshold";
            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "";
                public override string BarrageAuthor => "Tlott.God.Inf * 2.Kvon.0";
                public override string AttributeAuthor => "fn.I.do.Gt.to.lT _ On.or.Kz";
                public Information() { this.MusicOptimized = true; }
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                );
            }
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            private bool notRegistered = true;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production, production1, production2;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;
            public void Easy()
            {
                
            }

            public void Extreme()
            {
                if (InBeat(0))
                {
                    RegisterFunctionOnce("Line1", () =>
                    {
                        Line l = new(Rand(120, 640 - 120), Rand(85, 95)) { Alpha = 0.0f };
                        CreateEntity(l);
                        l.AlphaIncreaseAndDecrease(BeatTime(4), 0.6f);
                        DelayBeat(4, () => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        Line l1=new(EaseOut(BeatTime(1),new Vector2(320-42,240-42),new Vector2(320-360,240-360),EaseState.Sine),
                            EaseOut(BeatTime(1), new Vector2(320 + 42, 240 + 42), new Vector2(320 + 360, 240 + 360), EaseState.Sine))
                        { Alpha = 0.8f };
                        Line l2 = new(EaseOut(BeatTime(1), new Vector2(320 - 42, 240 + 42), new Vector2(320 - 360, 240 + 360), EaseState.Sine),
                            EaseOut(BeatTime(1), new Vector2(320 + 42, 240 - 42), new Vector2(320 + 360, 240 - 360), EaseState.Sine))
                        { Alpha = 0.8f };
                        Line[] lines = { l1, l2 };
                        foreach(Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(1), 0.85f);
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Line3", () =>
                    {
                        Line l1 = new(EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(0, 240), EaseState.Quad), Stable(0, 90)) { Alpha = 0.55f };
                        Line l2 = new(EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 0), EaseState.Quad), Stable(0, 0)) { Alpha = 0.55f };
                        CreateEntity(l1);
                        CreateEntity(l2);
                        l1.TransverseMirror = true;
                        l2.VerticalMirror = true;
                        for (int i = 0; i < 3; i++)
                        {
                            l1.InsertRetention(new(BeatTime(0.1f * i), 0.55f - i * 0.55f / 3f));
                            l2.InsertRetention(new(BeatTime(0.1f * i), 0.55f - i * 0.55f / 3f));
                        }
                        DelayBeat(1, () => { l1.Dispose();l2.Dispose(); });
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                    {
                        "d(Line1)","","","",    "","","","",    "","","","",    "","","","",
                        "Line1","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "Line1","","","",    "","","","",    "","","","",    "","","","",
                        "Line1","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "Line1","","","",    "","","","",    "","","","",    "","","","",
                        "Line1","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "Line1","","","",    "","","","",    "","","","",    "","","","",
                        "Line1","","","",    "","","","",    "","","","",    "","","","",
                        ////16
                        "d(Line1)","","","",    "Line1","","","",    "Line1","","","",    "Line1","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "Line1","","","",    "Line1","","","",    "Line1","","","",    "Line1","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "Line1","","","",    "Line1","","","",    "Line1","","","",    "Line1","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "(Line1)(Line2)(Line3)","","","",    "Line1","","","",    "Line1","","","",    "","","","",
                        "Line1","","","",    "Line1","","","",    "Line1","","","",    "","","","",
                        ////32
                        "$2","","","",    "","","","",    "$3","","","",    "","","","",
                        "$0","","","",    "","","","",    "+2","","","",    "+2","","","",
                        //
                        "","","","",    "","","","",    "*d1","","","",    "*+01","","","",
                        "*+01","","","",    "*+01","","","",    "*+01","","","",    "","","","",
                        //--
                        "d","","","",    "d","","","",    "!!3","d","+0","",    "","","","",
                        "","","","",    "","","","",    "d1","","","",    "","","","",
                        //
                        "d1","","","",    "","","","",    "","","","",    "","","","",
                        "(d1)(+2)","","","",    "","","","",    "","","","",    "","","","",
                        ////48
                        "d","","","",    "+01","","","",    "d","","","",    "+01","","","",
                        "d","","","",    "+01","","","",    "d","","","",    "+01","","","",
                        //
                        "!!3","d","+0","",    "","","","",    "","","","",    "","","","",
                        "$0","","","",    "","","","",    "$2","","","",    "","","","",
                        //--
                        "($3)($31)","","","",    "","","","",    "","","","",    "","","","",
                        "$21","","","",    "","","","",    "$01","","","",    "","","","",
                        //
                        "($1)($11)","","","",    "","","","",    "","","","",    "","","","",
                        "($3)($31)","","","",    "","","","",    "","","","",    "","","","",
                        ////64
                    });
                }
                if (InBeat(4 * 16))
                {
                    Arrow.UnitEasing eL = new();
                    Arrow.UnitEasing eR = new();
                    AddInstance(eL);
                    AddInstance(eR);
                    eL.ApplyTime = BeatTime(4);
                    eR.ApplyTime = BeatTime(4);
                    eL.RotationEase = LinkEase(EaseOut(BeatTime(2), 30, 0, EaseState.Quad),
                        EaseIn(BeatTime(2), 0, 30, EaseState.Quad));
                    eR.RotationEase = LinkEase(EaseOut(BeatTime(2), -30, 0, EaseState.Quad),
                        EaseIn(BeatTime(2), 0, -30, EaseState.Quad));
                    eL.TagApply("L");
                    eR.TagApply("R");
                    CreateChart(BeatTime(4), BeatTime(1), 6.7f, new string[]
                    {
                        "d","","","",    "+01","","~_+01","",    "d","","","",    "+01","","~_+01","",
                        "d","","","",    "+01","","~_+01","",    "d","","","",    "+01","","~_+01","",
                        //
                        "($2)($2'1.6@L)","","~_$2'1.6@L","",    "~_$2'1.6@L","","~_$2'1.6@L","",    "~_$2'1.6@L","","~_$2'1.6@L","",    "~_$2'1.6@L","","~_$2'1.6@L","",
                        "~_$2'1.6@L","","~_$2'1.6@L","",    "~_$2'1.6@L","","~_$2'1.6@L","",    "$2'1.6@L","","","",    "","","","",
                        //--
                        "d","","","",    "+01","","~_+01","",    "d","","","",    "+01","","~_+01","",
                        "d","","","",    "+01","","~_+01","",    "d","","~_+0","",    "","","","",
                        //
                        "($01)($01'1.6@L)","","~_$01'1.6@L","",    "~_$01'1.6@L","","~_$01'1.6@L","",    "~_$01'1.6@L","","~_$01'1.6@L","",    "~_$01'1.6@L","","~_$01'1.6@L","",
                        "~_$01'1.6@L","","~_$01'1.6@L","",    "~_$01'1.6@L","","~_$01'1.6@L","",    "$01'1.6@L","","","",    "","","","",
                        ////16
                        "*d","","","",    "*+0","","","",    "*d1","","","",    "*+01","","","",
                        "*d","","","",    "*+0","","","",    "*$31","","*$3","",    "*$31","","*$3","",
                        //
                        "*d1","","","",    "*+01","","","",    "*d","","","",    "*+0","","","",
                        "*d1","","","",    "*+01","","","",    "(d)(+0'1.8@R)","","","",    "(d1)(+01'1.8@R)","","","",
                        //--
                        "*d","","","",    "*+0","","","",    "*d1","","","",    "*+01","","","",
                        "(*d)(>+0'2.1)","","","",    "*+0","","","",    "*d1","","","",    "*+01","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////32
                        "(d)(+01)","","","",    "","","","",    "(d)(+01)","","","",    "","","","",
                        "(d)(+01)","","","",    "","","","",    "*d","","","",    "","","","",
                        //
                        "(d)(+01)","","","",    "","","","",    "(d)(+01)","","","",    "","","","",
                        "(d)(+01)","","","",    "","","","",    "*d1","","","",    "","","","",
                        //--
                        "($0)($21)","","","",    "","","","",    "(+0)(+21)","","","",    "","","","",
                        "(+0)(+21)","","","",    "","","","",    "*d","","*d1","",    "","","","",
                        //
                        "($2)($01)","","","",    "","","","",    "(+0)(+21)","","","",    "","","","",
                        "(+0)(+21)","","","",    "","","","",    "*d1","","*d","",    "","","","",
                        ////48
                        "($0)($21)","","","",    "*d1","","","",    "($2)($01)","","","",    "*d1","","","",
                        "($0)($21)","","","",    "","","","",    "(d)(+01)","","","",    "","","","",
                        //
                        "($2)($01)","","","",    "*d","","","",    "($0)($21)","","","",    "*d","","","",
                        "($2)($01)","","","",    "","","","",    "(d)(+01)","","","",    "","","","",
                        //--
                        "(d)(+11)","","","",    "+1","","","",    "(d1)(+1)","","","",    "+11","","","",
                        "(d)(+11)","","","",    "","","","",    "*$3","","*$31","",    "*$3","","*$31","",
                        //
                        "(d)(-11)","","","",    "-1","","","",    "(d1)(-1)","","","",    "-11","","","",
                        "($2)($01)","","","",    "(*$2)(*$01)","","","",    "$0","","$11","",    "$2","","$31","",
                        ////64
                    });
                }
                if (InBeat(4 * 126))//喜欢我的大b纵吗.jpg
                {
                    Arrow.UnitEasing eL = new();
                    Arrow.UnitEasing eR = new();
                    Arrow.UnitEasing K = new();
                    Arrow.UnitEasing L = new();
                    Arrow.UnitEasing R = new();
                    AddInstance(eL);
                    AddInstance(eR);
                    AddInstance(K);
                    AddInstance(L);
                    AddInstance(R);
                    eL.ApplyTime = BeatTime(4);
                    eR.ApplyTime = BeatTime(4);
                    L.ApplyTime = BeatTime(4);
                    R.ApplyTime = BeatTime(4);
                    K.ApplyTime = BeatTime(4);
                    eL.PositionEase = Stable(BeatTime(4), new Vector2(-4, 0));
                    eR.PositionEase=Stable(BeatTime(4), new Vector2(4, 0));
                    L.PositionEase = Stable(BeatTime(4), new Vector2(-12, 0));
                    R.PositionEase = Stable(BeatTime(4), new Vector2(12, 0));
                    K.AlphaEase = LinkEase(Stable(BeatTime(3.75f), 0.1f), Linear(BeatTime(0.1f), 0.1f, 0.99f));
                    eL.TagApply("L");
                    eR.TagApply("R");
                    K.TagApply("K");
                    L.TagApply("tL");
                    R.TagApply("tR");
                    RegisterFunctionOnce("Down", () =>
                    {
                        RunEase(s => InstantSetBox(s, 84, 84),
                            EaseOut(BeatTime(1), BoxStates.Centre.Y, 300, EaseState.Quad),
                            Stable(0,300));
                        ForBeat(1, () => { InstantTP(new Vector2(320, BoxStates.Centre.Y)); });
                    });
                    RegisterFunctionOnce("DownUp", () =>
                    {
                        RunEase(s => InstantSetBox(s, 84, 84),
                            EaseOut(BeatTime(Arguments[0]), BoxStates.Centre.Y, Arguments[1], EaseState.Quad),
                            Stable(BeatTime(Arguments[2]), Arguments[1]),
                            EaseIn(BeatTime(Arguments[3]), Arguments[1], Arguments[4], EaseState.Quad));
                        ForBeat(Arguments[0] + Arguments[2] + Arguments[3], () => { InstantTP(new Vector2(320, BoxStates.Centre.Y)); });
                    });
                    RegisterFunctionOnce("Up", () =>
                    {
                        RunEase(s => InstantSetBox(s, 84, 84),
                            EaseIn(BeatTime(0.5f) - 1, BoxStates.Centre.Y, -45, EaseState.Quart));
                        DelayBeat(0.5f, () =>
                        {
                            InstantSetBox(480 + 45, 84, 84);
                            RunEase(s => InstantSetBox(s, 84, 84),EaseOut(BeatTime(0.5f), 480 + 45, 180, EaseState.Quart));
                        });
                        ForBeat(1, () => { InstantTP(new Vector2(320, BoxStates.Centre.Y)); });
                    });
                    RegisterFunctionOnce("III", () =>
                    {
                        Line l1 = new(new Vector2(320, 60), 0) { Alpha = 0.7f, Width = 4 };
                        Line l2 = new(new Vector2(320, 480 - 60), 0) { Alpha = 0.7f, Width = 4 };
                        Line l3 = new(320, 90) { Alpha = 0.7f, Width = 4 };
                        Line l4 = new(320 - 160, 90) { Alpha = 0.7f, Width = 4 };
                        Line l5 = new(320 + 160, 90) { Alpha = 0.7f, Width = 4 };
                        Line l3a = new(320, 95) { Alpha = 0.7f, Width = 4 };
                        Line l4a = new(320 - 160, 95) { Alpha = 0.7f, Width = 4 };
                        Line l5a = new(320 + 160, 95) { Alpha = 0.7f, Width = 4 };
                        Line l3b = new(320, 85) { Alpha = 0.7f, Width = 4 };
                        Line l4b = new(320 - 160, 85) { Alpha = 0.7f, Width = 4 };
                        Line l5b = new(320 + 160, 85) { Alpha = 0.7f, Width = 4 };
                        Line[] lines = { l1, l2, l3, l4, l5, l4a, l4b, l5a, l5b, l3a, l3b };
                        foreach(Line line in lines)
                        {
                            CreateEntity(line);
                            line.AlphaDecrease(BeatTime(3), 0.7f);
                            DelayBeat(3, () => { line.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("II", () =>
                    {
                        Line l1 = new(new Vector2(320, 60), 0) { Alpha = 0.7f, Width = 4 };
                        Line l2 = new(new Vector2(320, 480 - 60), 0) { Alpha = 0.7f, Width = 4 };
                        Line l4 = new(320 - 80, 90) { Alpha = 0.7f, Width = 4 };
                        Line l5 = new(320 + 80, 90) { Alpha = 0.7f, Width = 4 };
                        Line l4a = new(320 - 80, 95) { Alpha = 0.7f, Width = 4 };
                        Line l5a = new(320 + 80, 95) { Alpha = 0.7f, Width = 4 };
                        Line l4b = new(320 - 80, 85) { Alpha = 0.7f, Width = 4 };
                        Line l5b = new(320 + 80, 85) { Alpha = 0.7f, Width = 4 };
                        Line[] lines = { l1, l2, l4, l5, l4a, l4b, l5a, l5b };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.AlphaDecrease(BeatTime(3), 0.7f);
                            DelayBeat(3, () => { line.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("I", () =>
                    {
                        Line l1 = new(new Vector2(320, 60), 0) { Alpha = 0.7f, Width = 4 };
                        Line l2 = new(new Vector2(320, 480 - 60), 0) { Alpha = 0.7f, Width = 4 };
                        Line l3 = new(320, 90) { Alpha = 0.7f, Width = 4 };
                        Line l3a = new(320, 95) { Alpha = 0.7f, Width = 4 };
                        Line l3b = new(320, 85) { Alpha = 0.7f, Width = 4 };
                        Line[] lines = { l1, l2, l3, l3a, l3b };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.AlphaDecrease(BeatTime(3), 0.7f);
                            DelayBeat(3, () => { line.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("DownEnd", () =>
                    {
                        RunEase(s => InstantSetBox(s, 84, 84),
                            EaseIn(BeatTime(2), BoxStates.Centre.Y, 640, EaseState.Back),
                            Stable(0, 640));
                        ForBeat(2, () => { InstantTP(new Vector2(320, BoxStates.Centre.Y)); });
                        Extends.DrawingUtil.BetterBlackScreen(BeatTime(2), BeatTime(2), BeatTime(6), Color.Black * 0.85f);
                    });
                    RegisterFunctionOnce("splitter", () =>
                    {
                        RunEase((s) => { splitter.Intensity = s; Blur.Sigma = 0.2f * s; },
                            Stable(0, 8), EaseOut(BeatTime(2), 8, 0, EaseState.Sine));
                    });
                    RegisterFunctionOnce("Dots", () =>
                    {
                        ForBeat(Arguments[0], () =>
                        {
                            CreateEntity(new Particle(Color.Red * Rand(0.4f, 0.9f), new Vector2(Rand(-1, 1), Rand(Arguments[1], Arguments[2])), 8, new Vector2(Rand(40, 600), Arguments[3])) { Depth = 0.99f });
                        });
                    });
                    RegisterFunctionOnce("DownBound", () =>
                    {
                        ScreenDrawing.BoundColor = Color.Red * 0.4f;
                        RunEase(s => ScreenDrawing.DownBoundDistance = s,
                            EaseOut(BeatTime(6), 0, 120, EaseState.Quad), Stable(BeatTime(37), 120),
                            EaseOut(BeatTime(1.5f), 120, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("UpBound", () =>
                    {
                        RunEase(s => ScreenDrawing.UpBoundDistance = s,
                            EaseOut(BeatTime(1.5f), 0, 120, EaseState.Quad), Stable(BeatTime(29), 120),
                            EaseOut(BeatTime(1.5f), 120, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("Line1", () =>
                    {
                        for (int i = 0; i < 256; i++)
                        {
                            DelayBeat(i*0.25f, () =>
                            {
                                Line a = new(320 - 120, 90) { Alpha = 0.6f };
                                Line b = new(320 + 120, 90) { Alpha = 0.6f };
                                Line[] l = { a, b };
                                foreach(Line c in l)
                                {
                                    CreateEntity(c);
                                    c.AlphaDecrease(BeatTime(0.3f), 0.5f);
                                    DelayBeat(0.3f, () => { c.Dispose(); });
                                }
                            });
                        }
                    });
                    RegisterFunctionOnce("Line0A", () =>
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(i * 0.5f, () =>
                            {
                                Line c = new(320, 0) { Alpha = 0.6f };
                                CreateEntity(c);
                                c.AlphaDecrease(BeatTime(0.5f), 0.5f);
                                DelayBeat(0.5f, () => { c.Dispose(); });
                            });
                        }
                    });
                    RegisterFunctionOnce("Line0B", () =>
                    {
                        Line a = new(new Vector2(320, 240), EaseOut(BeatTime(1), 0, 90, EaseState.Quad)) { Alpha = 0.6f };
                        Line b = new(new Vector2(320, 240), EaseOut(BeatTime(1), 0, 45, EaseState.Quad)) { Alpha = 0.6f };
                        Line c = new(new Vector2(320, 240), EaseOut(BeatTime(1), 0, 135, EaseState.Quad)) { Alpha = 0.6f };
                        Line[] d = { a, b, c };
                        foreach(Line l in d)
                        {
                            CreateEntity(l);
                            DelayBeat(1,()=> { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Line1s", () =>
                    {
                        Line a = new(EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320 - 120, 240), EaseState.Quad), Stable(0, 90)) { Alpha = 0.6f };
                        Line b = new(EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320 + 120, 240), EaseState.Quad), Stable(0, 90)) { Alpha = 0.6f };
                        Line[] d = { a, b };
                        foreach (Line l in d)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(1), 0.6f);
                            DelayBeat(1, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("CrossA", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 180), EaseState.Quad), Stable(BeatTime(31), new Vector2(320, 200)),
                            EaseInOut(BeatTime(2), new Vector2(320, 180), new Vector2(320, 300), 0.5f, EaseState.Quad), Stable(BeatTime(31), new Vector2(320, 300))),
                            LinkEase(EaseInOut(BeatTime(1), 45, 155, 0.5f, EaseState.Quad), Stable(BeatTime(2), 115), Linear(0.1f, 155, 170), EaseOut(BeatTime(4), 170, 155, EaseState.Quart), Linear(0.1f, 155, 170), EaseOut(BeatTime(4), 170, 155, EaseState.Quart),
                            Linear(0.1f, 155, 170), EaseOut(BeatTime(4), 170, 155, EaseState.Quart), Linear(0.1f, 155, 170), EaseOut(BeatTime(4), 170, 155, EaseState.Quart), Linear(0.1f, 155, 170), EaseOut(BeatTime(4), 170, 155, EaseState.Quart), Linear(0.1f, 155, 170), EaseOut(BeatTime(4), 170, 155, EaseState.Quart),
                            Linear(0.1f, 155, 170), EaseOut(BeatTime(4), 170, 155, EaseState.Quart), Linear(0.1f, 155, 160), EaseOut(BeatTime(1), 160, 155, EaseState.Quart),
                            EaseInOut(BeatTime(2), 155, 205, 0.5f, EaseState.Quad), Stable(BeatTime(1), 205), Linear(0.1f, 205, 190), EaseOut(BeatTime(4), 190, 205, EaseState.Quart), Linear(0.1f, 205, 190), EaseOut(BeatTime(4), 190, 205, EaseState.Quart),
                             Linear(0.1f, 205, 190), EaseOut(BeatTime(4), 190, 205, EaseState.Quart), Linear(0.1f, 205, 190), EaseOut(BeatTime(4), 190, 205, EaseState.Quart), Linear(0.1f, 205, 190), EaseOut(BeatTime(4), 190, 205, EaseState.Quart), Linear(0.1f, 205, 190), EaseOut(BeatTime(4), 190, 205, EaseState.Quart),
                              Linear(0.1f, 205, 190), EaseOut(BeatTime(4), 190, 205, EaseState.Quart), Linear(0.1f, 205, 190), EaseOut(BeatTime(4), 190, 205, EaseState.Quart)))
                        { Alpha = 0.5f };
                        Line b = new(LinkEase(EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 180), EaseState.Quad), Stable(BeatTime(31), new Vector2(320, 200)),
                            EaseInOut(BeatTime(2), new Vector2(320, 180), new Vector2(320, 300), 0.5f, EaseState.Quad), Stable(BeatTime(31), new Vector2(320, 300))),
                            LinkEase(EaseInOut(BeatTime(1), 135, 25, 0.5f, EaseState.Quad), Stable(BeatTime(2), 25), Linear(0.1f, 25, 10), EaseOut(BeatTime(4), 10, 25, EaseState.Quart), Linear(0.1f, 25, 10), EaseOut(BeatTime(4), 10, 25, EaseState.Quart),
                             Linear(0.1f, 25, 10), EaseOut(BeatTime(4), 10, 25, EaseState.Quart), Linear(0.1f, 25, 10), EaseOut(BeatTime(4), 10, 25, EaseState.Quart), Linear(0.1f, 25, 10), EaseOut(BeatTime(4), 10, 25, EaseState.Quart), Linear(0.1f, 25, 10), EaseOut(BeatTime(4), 10, 25, EaseState.Quart),
                              Linear(0.1f, 25, 10), EaseOut(BeatTime(4), 10, 25, EaseState.Quart), Linear(0.1f, 25, 20), EaseOut(BeatTime(1), 20, 25, EaseState.Quart),
                            EaseInOut(BeatTime(2), 25, -25, 0.5f, EaseState.Quad), Stable(BeatTime(1), -25), Linear(0.1f, -25, -10), EaseOut(BeatTime(4), -10, -25, EaseState.Quart), Linear(0.1f, -25, -10), EaseOut(BeatTime(4), -10, -25, EaseState.Quart),
                            Linear(0.1f, -25, -10), EaseOut(BeatTime(4), -10, -25, EaseState.Quart), Linear(0.1f, -25, -10), EaseOut(BeatTime(4), -10, -25, EaseState.Quart), Linear(0.1f, -25, -10), EaseOut(BeatTime(4), -10, -25, EaseState.Quart), Linear(0.1f, -25, -10), EaseOut(BeatTime(4), -10, -25, EaseState.Quart),
                            Linear(0.1f, -25, -10), EaseOut(BeatTime(4), -10, -25, EaseState.Quart), Linear(0.1f, -25, -10), EaseOut(BeatTime(4), -10, -25, EaseState.Quart)))
                        { Alpha = 0.5f };
                        Line[] d = { a, b };
                        foreach (Line l in d)
                        {
                            CreateEntity(l);
                            DelayBeat(65, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(0, 0), new Vector2(120, 90), EaseState.Quad), Stable(BeatTime(0.5f), new Vector2(120, 90)),
                           Linear(BeatTime(1), new Vector2(120, 90), new Vector2(640, 480))), Stable(BeatTime(3), -25))
                        { Alpha = 0.5f };
                        Line b = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(0, 480), new Vector2(120, 390), EaseState.Quad), Stable(BeatTime(0.5f), new Vector2(120, 390)),
                           Linear(BeatTime(1), new Vector2(120, 390), new Vector2(640, 0))), Stable(BeatTime(3), 25))
                        { Alpha = 0.5f };
                        Line[] d = { a, b };
                        foreach (Line l in d)
                        {
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            DelayBeat(3, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Line3", () =>
                    {
                        Line l = new(EaseOut(BeatTime(2), new Vector2(320 - 120, 240), new Vector2(-80, 240), EaseState.Quart), Stable(0, 90)) { Alpha = 0.6f };
                        CreateEntity(l);
                        l.TransverseMirror = true;
                        for (int i = 0; i < 4; i++)
                        {
                            l.InsertRetention(new(BeatTime(0.125f * i), l.Alpha - l.Alpha / 4f * i));
                        }
                        l.AlphaDecrease(BeatTime(1.5f), 0.6f);
                        DelayBeat(1.5f, () => { l.Dispose(); });
                    });
                    RegisterFunctionOnce("Ar1", () =>
                    {
                        RunEase((s) => { CreateEntity(new FakeArrow(1, 0, 0, s, 1, 0.8f, -90)); },
                            EaseOut(BeatTime(2), new Vector2(0, 240 - 80), new Vector2(320 - 12, 240 - 80), EaseState.Quart),
                            EaseIn(BeatTime(2), new Vector2(320 - 12, 240 - 80), new Vector2(320 - 12, Heart.Centre.Y - 38), EaseState.Quad));
                    });
                    RegisterFunctionOnce("Ar2", () =>
                    {
                        RunEase((s) => { CreateEntity(new FakeArrow(0, 0, 0, s, 1, 0.8f, -90)); },
                            EaseOut(BeatTime(2), new Vector2(640, 240 - 80), new Vector2(320 + 12, 240 - 80), EaseState.Quart),
                            EaseIn(BeatTime(2), new Vector2(320 + 12, 240 - 80), new Vector2(320 + 12, Heart.Centre.Y - 38), EaseState.Quad));
                    });
                    RegisterFunctionOnce("Ar3", () =>
                    {
                        RunEase((s) => { CreateEntity(new FakeArrow(0, 0, 0, s, 1, 0.8f, 90)); },
                            EaseOut(BeatTime(2), new Vector2(640, 240 + 80), new Vector2(320 + 12, 240 + 80), EaseState.Quart),
                            EaseIn(BeatTime(2), new Vector2(320 + 12, 240 + 80), new Vector2(320 + 12, Heart.Centre.Y + 38), EaseState.Quad));
                    });
                    RegisterFunctionOnce("Ar4", () =>
                    {
                        RunEase((s) => { CreateEntity(new FakeArrow(1, 0, 0, s, 1, 0.8f, 90)); },
                            EaseOut(BeatTime(2), new Vector2(0, 240 + 80), new Vector2(320 - 12, 240 + 80), EaseState.Quart),
                            EaseIn(BeatTime(2), new Vector2(320 - 12, 240 + 80), new Vector2(320 - 12, Heart.Centre.Y + 38), EaseState.Quad));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 6.7f, new string[]
                    {
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "$0(III)(DownBound)","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //--
                        "$2(II)(<40,-5,-2,490>Dots)","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "$3(I)(Line0A)","","","",    "+0'1.2","","","",    "+0'1.2","","","",    "+0'1.2","","","",
                        "+0'1.5(Line0B)","","+0'1.5","",    "+0'1.5","","+0'1.5","",    "+0'1.5(Down)(CrossA)(Line1s)","","","",    "","","","",
                        ////16
                        "($3'1.6)(+0'1.6)(Line1)(Line2)","","","",    "","","*+01'1.6@L(<0.9,306,0.4,0.4,300>DownUp)","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        "($3'1.6)(+0'1.6)(splitter)(Line3)","","","",    "*+0'1.6@R(<0.1,302,0,0.2,300>DownUp)","","","",    "*+01'1.6@L(<0.6,306,0.2,0.2,304>DownUp)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        //
                        "+0'1.6(<1,312,0.3,0.7,300>DownUp)(Line2)","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","","","",
                        "($3'1.6)(+0'1.6)(splitter)(Line3)","","","",    "*+0'1.6(<0.1,302,0,0.2,300>DownUp)","","","",    "*+0'1.6(<0.1,302,0,0.2,300>DownUp)","","","",    "*+0'1.6(<0.1,302,0,0.2,300>DownUp)","","","",
                        //--
                        "*+0'1.6(<0.1,302,0,0.2,300>DownUp)(Line2)","","","",    "*+01'1.6@L(<0.8,306,0.2,0.4,300>DownUp)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","","",
                        "($3'1.6)(+0'1.6)(<0.1,302,0,0.2,300>DownUp)(splitter)(Line3)(Ar1)","","","",    "*+01'1.6@L(<2.5,315,0.8,1.2,310>DownUp)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","","",
                        //
                        "(*+01'1.6@L)(Line2)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        "(*+01'1.6@L)(+01'1.6@K)(splitter)(Line3)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "+01'1.6(<0.5,325,0.2,0.4,300>DownUp)","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",
                        ////32
                        "($31'1.6)(+01'1.6)(Line2)","","","",    "","","*+0'1.6@R(<0.9,306,0.4,0.4,300>DownUp)","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        "($31'1.6)(+01'1.6)(splitter)(Line3)","","","",    "*+01'1.6@L(<0.1,302,0,0.2,300>DownUp)","","","",    "*+0'1.6@R(<0.6,306,0.2,0.2,304>DownUp)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        //
                        "+01'1.6(<1,312,0.3,0.7,300>DownUp)(Line2)","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","","","",
                        "($31'1.6)(+01'1.6)(splitter)(Line3)","","","",    "*+01'1.6(<0.1,302,0,0.2,300>DownUp)","","","",    "*+01'1.6(<0.1,302,0,0.2,300>DownUp)","","","",    "*+01'1.6(<0.1,302,0,0.2,300>DownUp)","","","",
                        //--
                        "*+01'1.6(<0.1,302,0,0.2,300>DownUp)(Line2)","","","",    "*+0'1.6@R(<0.8,310,0.2,0.4,300>DownUp)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","","",
                        "($31'1.6)(+01'1.6)(<0.1,302,0,0.2,300>DownUp)(splitter)(Line3)(Ar2)","","","",    "*+0'1.6@R(<2.5,315,0.8,1.2,310>DownUp)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","","",
                        //
                        "(*+0'1.6@R)(Line2)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        "(*+0'1.6@R)(+0'1.6@K@tR)(splitter)(Line3)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "+0'1.6(Up)","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",
                        ////turn into another side - 180
                        "(<32,5,2,-10>Dots)(UpBound)(Line2)","","","",    "","","*$11'1.6@L(<0.9,174,0.4,0.4,180>DownUp)","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        "($1'1.6)(+0'1.6)(splitter)(Line3)","","","",    "*+0'1.6@R(<0.1,178,0,0.2,180>DownUp)","","","",    "*+01'1.6@L(<0.6,174,0.2,0.2,176>DownUp)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        //
                        "+0'1.6(<1,168,0.3,0.7,180>DownUp)(Line2)","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","","","",
                        "($1'1.6)(+0'1.6)(splitter)(Line3)","","","",    "*+0'1.6(<0.1,178,0,0.2,180>DownUp)","","","",    "*+0'1.6(<0.1,178,0,0.2,180>DownUp)","","","",    "*+0'1.6(<0.1,178,0,0.2,180>DownUp)","","","",
                        //--
                        "*+0'1.6(<0.1,178,0,0.2,180>DownUp)(Line2)","","","",    "*+01'1.6@L(<0.8,174,0.2,0.4,180>DownUp)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","","",
                        "($1'1.6)(+0'1.6)(<0.1,178,0,0.2,180>DownUp)(splitter)(Line3)(Ar3)","","","",    "*+01'1.6@L(<2.5,165,0.8,1.2,170>DownUp)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","","",
                        //
                        "(*+01'1.6@L)(Line2)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        "(*+0'1.6@L)(+0'1.6@K@tR)(splitter)(Line3)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "+01'1.6(<0.5,155,0.2,0.4,180>DownUp)","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",
                        ////64
                        "($11'1.6)(+01'1.6)(Line2)","","","",    "","","*+0'1.6@R(<0.9,174,0.4,0.4,180>DownUp)","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        "($11'1.6)(+01'1.6)(splitter)(Line3)","","","",    "*+01'1.6@L(<0.1,178,0,0.2,180>DownUp)","","","",    "*+0'1.6@R(<0.6,174,0.2,0.2,176>DownUp)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        //
                        "+01'1.6(<1,168,0.3,0.7,180>DownUp)(Line2)","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","","","",
                        "($11'1.6)(+01'1.6)(splitter)(Line3)","","","",    "*+01'1.6(<0.1,178,0,0.2,180>DownUp)","","","",    "*+01'1.6(<0.1,178,0,0.2,180>DownUp)","","","",    "*+01'1.6(<0.1,178,0,0.2,180>DownUp)","","","",
                        //--
                        "*+01'1.6(<0.1,178,0,0.2,180>DownUp)(Line2)","","","",    "*+0'1.6@R(<0.8,170,0.2,0.4,180>DownUp)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","","",
                        "($11'1.6)(+01'1.6)(<0.1,178,0,0.2,180>DownUp)(splitter)(Line3)(Ar4)","","","",    "*+0'1.6@R(<2.5,165,0.8,1.2,170>DownUp)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","","",
                        //
                        "(*+0'1.6@R)(Line2)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        "(*+01'1.6@R)(+01'1.6@K@tL)(splitter)(Line3)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "+0'1.6(DownEnd)","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","","","",
                        ////80
                    });
                }
            }

            public void ExtremePlus()
            {
                
            }

            public void Hard()
            {
                
            }

            public void Noob()
            {
                
            }

            public void Normal()
            {

            }
            public void Start()
            {
                production = Blur = new Blur(0.505f);
                production1 = new Filter(FightResources.Shaders.StepSample, 0.51f);
                splitter = new RGBSplitting(0.9f) { Disturbance = false };
                StepSample = FightResources.Shaders.StepSample;
                Blur.Sigma = 0f;
                StepSample.Intensity = 0.0f;
                StepSample.CentreX = 320f;
                StepSample.CentreY = 240f;
                splitter.Intensity = 0.0f;
                ScreenDrawing.SceneRendering.InsertProduction(production);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(splitter);

                SetSoul(1);
                InstantTP(new(320, 240));
                SetGreenBox();
                HeartAttribute.MaxHP = 16;
                ScreenDrawing.HPBar.HPLoseColor = Color.DarkRed;
                ScreenDrawing.HPBar.HPExistColor = Color.Blue * 0.7f;
                Settings.GreenTap = true;
                HeartAttribute.ArrowFixed = true;

                GametimeDelta = BeatTime(-4);
                bool jump = true;
                if (jump)
                {
                    int beat = 4*126;
                    //int beat = 4 * 15;
                    GametimeDelta = BeatTime( - 4 + beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}
