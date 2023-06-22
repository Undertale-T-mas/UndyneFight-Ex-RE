using Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class mu : IChampionShip
    {
        public mu()
        {
            difficulties = new();
            difficulties.Add("Pa5t Lv.&", Difficulty.Easy);
            difficulties.Add("&rese^6 [u.1l", Difficulty.Normal);
            difficulties.Add("3utv5@ 7v.?%&", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (190f / 60f)) { }
            public string Music => "mu";

            public string FightName => "mu";
            private class ThisImformation : SongImformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 0),
                            new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 0),
                            new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 0),
                            new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override string BarrageAuthor => "Parad & zKronO";
                public override string AttributeAuthor => "2.K.R";
                public override string PaintAuthor => "Frums";
                public override string SongAuthor => "Frums";
            }
            public SongImformation Attributes => new ThisImformation();
            private bool notRegistered = true;
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production1;
            RenderProduction production2;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;
            #region non

            public void Hard()
            {
                if (Gametime < 0) return;
            }

            public void Noob()
            {
                if (Gametime < 0) return;
            }
            public void Extreme()
            {
                if (Gametime < 0) return;
            }
            #endregion
            private static class NormalBarrage { }
            public void Easy()
            {
                if (Gametime < 0) return;
            }
            public void Normal()
            {
                if (Gametime < 0) return;
            }
            #region Ex
            void ExPart1()
            {
                RegisterFunctionOnce("fake1", () =>
                {
                    FakeArrow a = new(0, 0, 0, new Vector2(320 + 80, 240), BeatTime(0.4f), 1.2f, 0);
                    FakeArrow b = new(1, 0, 0, new Vector2(320, 240 - 80), BeatTime(0.4f), 1.2f, 270);
                    FakeArrow c = new(0, 0, 0, new Vector2(320 - 80, 240), BeatTime(0.4f), 1.2f, 180);
                    FakeArrow[] ar = { a, b, c };
                    foreach (FakeArrow ars in ar)
                    {
                        CreateEntity(ars);
                    }
                });
                RegisterFunctionOnce("fake2", () =>
                {
                    FakeArrow b = new(1, 0, 0, new Vector2(320 + 80, 240), BeatTime(0.4f), 1.2f, 0);
                    FakeArrow c = new(1, 0, 0, new Vector2(320 - 80, 240), BeatTime(0.4f), 1.2f, 180);
                    FakeArrow a = new(0, 0, 0, new Vector2(320, 240 - 80), BeatTime(0.4f), 1.2f, 270);
                    FakeArrow[] ar = { a, b, c };
                    foreach (FakeArrow ars in ar)
                    {
                        CreateEntity(ars);
                    }
                });
                RegisterFunctionOnce("specArs", () =>
                {
                    CreateArrow(BeatTime(1.5f), 3, 4.2f, 0, 0);
                    CreateArrow(BeatTime(1.5f), 3, 4.2f, 1, 0);
                });
                RegisterFunctionOnce("line1a", () =>
                {
                    Line a = new(new Vector2(320, 160), 60) { Alpha = 0.45f };
                    CreateEntity(a);
                    a.TransverseMirror = true;
                    DelayBeat(0.4f, () =>
                    {
                        a.Dispose();
                    });
                });
                RegisterFunctionOnce("line1b", () =>
                {
                    Line a = new(new Vector2(320, 380), 30) { Alpha = 0.45f };
                    CreateEntity(a);
                    a.TransverseMirror = true;
                    DelayBeat(0.4f, () =>
                    {
                        a.Dispose();
                    });
                });
                RegisterFunctionOnce("line1c", () =>
                {
                    ValueEasing.EaseBuilder v = new();
                    ValueEasing.EaseBuilder v1 = new();
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(21), CentreEasing.Linear(new(320, 200), new(320, 180), BeatTime(21)));
                    v.Insert(BeatTime(21), ValueEasing.SinWave(0.2f, 6, 0));
                    v1.Insert(BeatTime(21), ValueEasing.Linear(30, 20, BeatTime(21)));
                    Line a = new(c.GetResult(), v1.GetResult());
                    v.Run(s => a.Alpha = 0.25f + s);
                    CreateEntity(a);
                    a.TransverseMirror = true;
                    DelayBeat(21, () =>
                    {
                        a.Dispose();
                    });
                });
                RegisterFunctionOnce("line2a", () =>
                {
                    for (int i = 0; i < 210; i++)
                    {
                        DelayBeat(0.1f * i, () =>
                        {
                            CentreEasing.EaseBuilder c = new();
                            c.Insert(BeatTime(0.5f), CentreEasing.EaseOutSine(new(0, 240), new(319, 240), BeatTime(0.5f)));
                            Line a = new(c.GetResult(), (s) => { return 90; }) { Alpha = 0.2f };
                            CreateEntity(a);
                            a.TransverseMirror = true;
                            DelayBeat(0.5f, () => { a.Dispose(); });
                        });
                    }
                });
                RegisterFunctionOnce("line2b", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(21), CentreEasing.Linear(new(220, 240), new(200, 240), BeatTime(21)));
                    Line a = new(c.GetResult(), (s) => { return 90; }) { Alpha = 0.35f };
                    CreateEntity(a);
                    a.TransverseMirror = true;
                    DelayBeat(21, () =>
                    {
                        a.Dispose();
                    });
                });
                RegisterFunctionOnce("line2c", () =>
                {
                    Line a = new(new Vector2(200, 240), 90) { Alpha = 0.35f };
                    CreateEntity(a);
                    a.TransverseMirror = true;
                    DelayBeat(0.3f, () =>
                    {
                        a.Dispose();
                    });
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 5.5f, new string[]
                {  //load
                    "fake1(line1a)","","","",    "fake2(line1b)","","","",    "($3'0.6{Ro})($31'0.6{Lo})(line1c)(line2a)(line2b)","","~_+01{Lo}","",    "~_+0{Ro}","","~_+01{Lm}","",
                    "~_+0{Rm}","","~_+01{Li}","",    "~_+0{Ri}","","~_+01{Ri}","",    "~_+0{Li}","","~_+01{Rm}","",    "~_+0{Lm}","","~_+01{Ro}","",
                    //
                    "~_+0{Lo}","","~_+01{Ro}","",    "~_+0{Lo}","","~_+01{Rm}","",    "~_+0{Lm}","","~_+01{Ri}","",    "~_+0{Li}","","~_+01{Li}","",
                    "~_+0{Ri}","","~_+01{Lm}","",    "~_+0{Rm}","","~_+01{Lo}","",    "~_+0{Ro}","","~_+01{Lo}","",    "~_+0{Ro}","","~_+01{Lm}","",
                    //
                    "~_+0{Rm}","","~_+01{Li}","",    "~_+0{Ri}","","~_+01{Ri}","",    "~_+0{Li}","","~_+01{Rm}","",    "~_+0{Lm}","","~_+01{Ro}","",
                    "~_+0{Lo}","","~_+01{Ro}","",    "~_+0{Lo}","","~_+01{Rm}","",    "~_+0{Lm}","","~_+01{Ri}","",    "~_+0{Li}","","~_+01{Li}","",
                    //
                    "~_+0{Ri}","","~_+01{Lm}","",    "~_+0{Rm}","","~_+01{Lo}","",    "~_+0{Ro}","","~_+01{Lo}","",    "~_+0{Ro}","","~_+01{Lm}","",
                    "~_+0{Rm}","","~_+01{Li}","",    "~_+0{Ri}","","~_+01{Ri}","",    "~_+0{Li}","","~_+01{Rm}","",    "~_+0{Lm}","","~_+01{Ro}","",
                    ////
                    "~_+0{Lo}","","~_+01{Ro}","",    "~_+0{Lo}","","~_+01{Rm}","",    "~_+0{Lm}","","~_+01{Ri}","",    "~_+0{Li}","","~_+01{Li}","",
                    "~_+0{Ri}","","~_+01{Lm}","",    "~_+0{Rm}","","~_+01{Lo}","",    "~_+0{Ro}","","~_+01{Lo}","",    "~_+0{Ro}","","~_+01{Lm}","",
                    //
                    "~_+0{Rm}","","~_+01{Li}","",    "~_+0{Ri}","","~_+01{Ri}","",    "~_+0{Li}","","~_+01{Rm}","",    "~_+0{Lm}","","(~_+01{Ro})(~_+0{Lo})","",
                    "","","","",    "line2c","","","",    "line2c","","","",    "line2c","","","",
                });
            }
            void ExPart2()
            {
                RegisterFunctionOnce("line1", () =>
                {
                    Line a = new(new Vector2(200, 240), 110) { Alpha = 0.55f };
                    Line b = new(new Vector2(220, 240), 110) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    a.AlphaDecrease(BeatTime(1), 0.55f);
                    b.AlphaDecrease(BeatTime(1), 0.55f);
                    DelayBeat(1, () =>
                    {
                        a.Dispose();
                        b.Dispose();
                    });
                });
                RegisterFunctionOnce("line2", () =>
                {
                    Line a = new(new Vector2(440, 240), 70) { Alpha = 0.55f };
                    Line b = new(new Vector2(420, 240), 70) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    a.AlphaDecrease(BeatTime(1), 0.55f);
                    b.AlphaDecrease(BeatTime(1), 0.55f);
                    DelayBeat(1, () =>
                    {
                        a.Dispose();
                        b.Dispose();
                    });
                });
                RegisterFunctionOnce("line3", () =>
                {
                    Line a = new(new Vector2(200, 240), 70) { Alpha = 0.55f };
                    Line b = new(new Vector2(220, 240), 70) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    a.AlphaDecrease(BeatTime(1), 0.55f);
                    b.AlphaDecrease(BeatTime(1), 0.55f);
                    DelayBeat(1, () =>
                    {
                        a.Dispose();
                        b.Dispose();
                    });
                });
                RegisterFunctionOnce("line4", () =>
                {
                    Line a = new(new Vector2(440, 240), 110) { Alpha = 0.55f };
                    Line b = new(new Vector2(420, 240), 110) { Alpha = 0.55f };
                    CreateEntity(a);
                    CreateEntity(b);
                    a.AlphaDecrease(BeatTime(1), 0.55f);
                    b.AlphaDecrease(BeatTime(1), 0.55f);
                    DelayBeat(1, () =>
                    {
                        a.Dispose();
                        b.Dispose();
                    });
                });
                RegisterFunctionOnce("convL", () => { ScreenDrawing.CameraEffect.Convulse(1.5f, BeatTime(0.5f), false); });
                RegisterFunctionOnce("convR", () => { ScreenDrawing.CameraEffect.Convulse(1.5f, BeatTime(0.5f), true); });
                RegisterFunctionOnce("lineA1", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(200, 120), new(800, 480), BeatTime(2.4f)));
                    Line a = new(c.GetResult(), (s) => { return -35; }) { Alpha = 0.45f };
                    CreateEntity(a);
                    for (int i = 0; i < 4; i++)
                    {
                        a.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                    }
                    DelayBeat(2.4f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("lineA2", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(440, 120), new(440 - 600, 480), BeatTime(2.4f)));
                    Line a = new(c.GetResult(), (s) => { return 35; }) { Alpha = 0.45f };
                    CreateEntity(a);
                    for (int i = 0; i < 4; i++)
                    {
                        a.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                    }
                    DelayBeat(2.4f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("lineAr1", () =>
                {
                    float r = Rand(120, 360);
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(200, r), new(800, r + 360), BeatTime(2.4f)));
                    Line a = new(c.GetResult(), (s) => { return -35; }) { Alpha = Rand(0.3f, 0.6f) };
                    CreateEntity(a);
                    for (int i = 0; i < 4; i++)
                    {
                        a.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                    }
                    DelayBeat(2.4f, () => { a.Dispose(); });
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
                    //start
                    "(R)(+21)(line1)(convL)","","","",    "+0(line2)(convR)","","","",    "","","","",    "D1","","+11","",
                    "+31","","","",    "*+012","","","",    "*+012","","","",    "*+012","","","",
                    //
                    "D","","","",    "+21","","","",    "","","","",    "D","","+2","",
                    "+2","","","",    "+111","","","",    "+111","","","",    "+111","","","",
                    ////
                    "(R)(+21)(line4)(convR)","","","",    "+21(line3)(convL)","","","",    "","","","",    "D","","+1","",
                    "+3","","","",    "*+002","","","",    "*+002","","","",    "*+002","","","",
                    //
                    "D1","","","",    "+2","","","",    "","","","",    "D1","","+21","",
                    "+21","","","",    "+101(line1)(line2)(convL)","","","",    "+101","","+2","",    "+2","","","",
                    //
                    "D(line4)(line3)(convR)","","","",    "+002","","","",    "#1.3#+3(lineA1)(line1)(line3)(convL)","","","",    "R","","","",
                    "R","","","",    "(R)(+2)(lineA1)(line1)","","","",    "R","","","",    "(R)(+01)(line1)(line3)(convL)(lineA1)","","","",
                    //
                    "(D)(+01)(lineA1)(line3)","","","",    "+002","","","",    "#1.3#+3(lineA2)(line4)(line2)(convR)","","","",    "R","","","",
                    "R1","","","",    "(R1)(+21)(lineA2)(line2)","","","",    "R1","","","",    "(R)(+01)(lineA2)(line4)(line2)(convR)","","","",
                    ////
                    "(D)(+01)(lineA2)(line4)","","","",    "+012","","","",    "#1.3#+31(lineA1)(line1)(line3)(line4)(line2)(convL)","","","",    "R1","","","",
                    "R1","","","",    "(R1)(+21)(lineA2)(line1)(line2)(convL)","","","",    "R1","","","",    "(R)(+01)(lineA1)(line1)(line3)(line4)(line2)(convR)","","","",
                    //
                    "(D)(+01)(lineA1)","","","",    "+012","","","",    "#1.3#+31(lineA2)(line4)(line2)(line1)(line3)(convR)","","","",    "R1","","","",
                    "R","","","",    "(R)(+2)(lineA1)(line4)(line3)(convR)","","","",    "R(lineAr1)","","","",    "+01(lineA2)(line4)(line2)(line1)(line3)(convL)(lineAr1)","","","",
                    //
                });
            }
            void ExPart3()
            {
                RegisterFunctionOnce("lineAr1", () =>
                {
                    float r = Rand(60, 420);
                    float alp = Rand(0.3f, 0.6f);
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(200, r), new(800, r + 420), BeatTime(2.4f)));
                    CentreEasing.EaseBuilder c1 = new();
                    c1.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(200, r), new(200 - 600, r - 420), BeatTime(2.4f)));
                    Line a = new(c.GetResult(), (s) => { return -35; }) { Alpha = alp };
                    Line b = new(c1.GetResult(), (s) => { return -35; }) { Alpha = alp };
                    CreateEntity(a);
                    CreateEntity(b);
                    for (int i = 0; i < 4; i++)
                    {
                        a.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                        b.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                    }
                    DelayBeat(2.4f, () => { a.Dispose(); b.Dispose(); });
                });
                RegisterFunctionOnce("lineAr2", () =>
                {
                    float r = Rand(60, 420);
                    float alp = Rand(0.3f, 0.6f);
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(440, r), new(440 - 600, r + 420), BeatTime(2.4f)));
                    CentreEasing.EaseBuilder c1 = new();
                    c1.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(440, r), new(440 + 600, r - 420), BeatTime(2.4f)));
                    Line a = new(c.GetResult(), (s) => { return 35; }) { Alpha = alp };
                    Line b = new(c1.GetResult(), (s) => { return 35; }) { Alpha = alp };
                    CreateEntity(a);
                    CreateEntity(b);
                    for (int i = 0; i < 4; i++)
                    {
                        a.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                        b.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                    }
                    DelayBeat(2.4f, () => { a.Dispose(); b.Dispose(); });
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
                    "#0.8#R1(lineAr1)","","","",    "","","","",    "R1(lineAr1)","","","",    "R1","","","",
                    "R1(lineAr1)","","","",    "(R)(R1)","","","",    "R(lineAr1)","","","",    "R","","","",
                    //
                    "#0.8#D(lineAr2)","","","",    "","","","",    "R(lineAr1)","","","",    "R","","","",
                    "R(lineAr2)","","","",    "(R)(R1)","","","",    "R1(lineAr2)","","","",    "R1","","","",
                    ////
                    "#0.8#D1(lineAr2)","","","",    "","","","",    "R1(lineAr2)","","","",    "R1","","","",
                    "R1(lineAr2)","","","",    "(R)(R1)","","","",    "R(lineAr2)","","","",    "R","","","",
                    //
                    "#0.8#D(lineAr1)","","","",    "","","","",    "R(lineAr2)","","","",    "R","","","",
                    "R(lineAr1)","","","",    "(R)(R1)(lineAr1)","","","",    "R1(lineAr1)","","","",    "R1","","","",
                    //
                });
            }
            void ExPart4()
            {
                RegisterFunctionOnce("lineA1", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(200, 120), new(800, 480), BeatTime(2.4f)));
                    Line a = new(c.GetResult(), (s) => { return -35; }) { Alpha = 0.45f };
                    CreateEntity(a);
                    for (int i = 0; i < 4; i++)
                    {
                        a.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                    }
                    DelayBeat(2.4f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("lineA2", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2.4f), CentreEasing.EaseInQuad(new(440, 120), new(440 - 600, 480), BeatTime(2.4f)));
                    Line a = new(c.GetResult(), (s) => { return 35; }) { Alpha = 0.45f };
                    CreateEntity(a);
                    for (int i = 0; i < 4; i++)
                    {
                        a.InsertRetention(new(3 * i, 0.75f - 0.12f * i));
                    }
                    DelayBeat(2.4f, () => { a.Dispose(); });
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
                    "R1(lineA1)","","+21","",    "+21(lineA1)","","","",    "$201","","","",    "R(lineA2)","","R","",
                    "R(lineA2)","","","",    "$301","","","",    "R(lineA1)(lineA2)","","R","",    "R(lineA1)(lineA2)","","","",
                    //
                    "$001","","","",    "(#1.5#$3)(#1.5#$31)","","","",    "","","","",    "","","","",
                    "(<$10)(>$11)","","","",    "","","","",    "","","","",    "","","","",
                    //;
                });
            }
            void ExPart5()
            {
                RegisterFunctionOnce("ConvL", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(1.7f, BeatTime(0.75f), false);
                });
                RegisterFunctionOnce("ConvR", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(1.7f, BeatTime(0.75f), true);
                });
                RegisterFunctionOnce("Step", () =>
                {
                    ValueEasing.EaseBuilder a = new();
                    a.Insert(BeatTime(0.32f), ValueEasing.Linear(0, 0.08f, BeatTime(0.32f)));
                    a.Insert(BeatTime(0.32f), ValueEasing.EaseOutQuad(0.08f, 0, BeatTime(0.32f)));
                    a.Run(s => StepSample.Intensity = s);
                });
                RegisterFunctionOnce("LineL", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(0.75f), CentreEasing.EaseOutCirc(new(0, 240), new(240, 240), BeatTime(0.75f)));
                    Line a = new(c.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                    Line b = new(c.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                    Line[] lines = { a, b };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.75f));
                    }
                });
                RegisterFunctionOnce("LineR", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(0.75f), CentreEasing.EaseOutCirc(new(640, 240), new(400, 240), BeatTime(0.75f)));
                    Line a = new(c.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                    Line b = new(c.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                    Line[] lines = { a, b };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.75f));
                    }
                });
                RegisterFunctionOnce("LineLb", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(0.75f), CentreEasing.EaseOutCirc(new(240, 240), new(0, 240), BeatTime(0.75f)));
                    Line a = new(c.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                    Line b = new(c.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                    Line[] lines = { a, b };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.75f));
                    }
                });
                RegisterFunctionOnce("LineRb", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(0.75f), CentreEasing.EaseOutCirc(new(400, 240), new(640, 240), BeatTime(0.75f)));
                    Line a = new(c.GetResult(), (s) => { return 45; }) { Alpha = 0.85f };
                    Line b = new(c.GetResult(), (s) => { return -45; }) { Alpha = 0.85f };
                    Line[] lines = { a, b };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(0.75f));
                    }
                });
                RegisterFunctionOnce("LineRand", () =>
                {
                    float r = Rand(30, 590);
                    Line a = new(new Vector2(r, 0), 90) { Alpha = 0.85f };
                    Line b = new(new Vector2(r + 20, 0), 90) { Alpha = 0.85f };
                    Line[] lines = { a, b };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        DelayBeat(0.25f, () => { l.Dispose(); });
                    }
                });
                RegisterFunctionOnce("LineG1", () =>
                {
                    for (int i = 0; i < 64; i++)
                    {
                        DelayBeat(i * 0.125f, () =>
                        {
                            CentreEasing.EaseBuilder c1 = new();
                            ValueEasing.EaseBuilder v1 = new();
                            CentreEasing.EaseBuilder c2 = new();
                            ValueEasing.EaseBuilder v2 = new();
                            c1.Insert(BeatTime(0.5f), CentreEasing.Linear(new(0, 0), new(640, 0), BeatTime(0.5f)));
                            c2.Insert(BeatTime(0.5f), CentreEasing.Linear(new(640, 480), new(0, 480), BeatTime(0.5f)));
                            v1.Insert(BeatTime(0.5f), ValueEasing.Linear(0, 90, BeatTime(0.5f)));
                            v2.Insert(BeatTime(0.5f), ValueEasing.Linear(0, 90, BeatTime(0.5f)));
                            Line a = new(c1.GetResult(), v1.GetResult()) { Alpha = 0.75f };
                            Line b = new(c2.GetResult(), v2.GetResult()) { Alpha = 0.75f };
                            Line[] lines = { a, b };
                            foreach (Line l in lines)
                            {
                                CreateEntity(l);
                                DelayBeat(0.5f, () => { l.Dispose(); });
                            }
                        });
                    }

                });
                RegisterFunctionOnce("LineG2", () =>
                {
                    for (int i = 0; i < 60; i++)
                    {
                        DelayBeat(i * 0.125f, () =>
                        {
                            CentreEasing.EaseBuilder c1 = new();
                            ValueEasing.EaseBuilder v1 = new();
                            CentreEasing.EaseBuilder c2 = new();
                            ValueEasing.EaseBuilder v2 = new();
                            c1.Insert(BeatTime(0.5f), CentreEasing.Linear(new(0, 480), new(640, 480), BeatTime(0.5f)));
                            c2.Insert(BeatTime(0.5f), CentreEasing.Linear(new(640, 0), new(0, 0), BeatTime(0.5f)));
                            v1.Insert(BeatTime(0.5f), ValueEasing.Linear(0, -90, BeatTime(0.5f)));
                            v2.Insert(BeatTime(0.5f), ValueEasing.Linear(0, -90, BeatTime(0.5f)));
                            Line a = new(c1.GetResult(), v1.GetResult()) { Alpha = 0.75f };
                            Line b = new(c2.GetResult(), v2.GetResult()) { Alpha = 0.75f };
                            Line[] lines = { a, b };
                            foreach (Line l in lines)
                            {
                                CreateEntity(l);
                                DelayBeat(0.5f, () => { l.Dispose(); });
                            }
                        });
                    }
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
                    "(R)(#0.7#+2)(ConvL)(Step)(LineL)","","","",    "","","^+2(LineL)","",    "","","","",    "","","","",
                    "(R)(#0.7#+2)","","","",    "","","^+2","",    "","","","",    "","","","",
                    //
                    "(R)(#0.7#+2)(ConvR)(Step)(LineR)","","","",    "","","^+2(LineR)","",    "","","","",    "","","","",
                    "(R)(#0.7#+2)","","","",    "","","^+2","",    "(#0.5#D1)","","","",    "+21","","","",
                    ////
                    "(R1)(#0.7#+21)(ConvR)(Step)(LineR)","","","",    "","","^+21(LineR)","",    "","","","",    "","","","",
                    "(R1)(#0.7#+21)","","","",    "","","^+21","",    "","","","",    "","","","",
                    //
                    "(R1)(#0.7#+21)(ConvL)(Step)(LineL)","","","",    "","","^+21(LineL)","",    "","","","",    "","","","",
                    "(R1)(#0.7#+21)","","","",    "","","^+21","",    "(#0.5#D)","","","",    "+2","","","",
                    //
                    "(R)(#0.7#+2)(ConvL)(Step)(LineL)","","","",    "","","^+2(LineL)","",    "+0(LineR)","","","",    "","","","",
                    "(R)(#0.7#+2)","","","",    "","","^+2","",    "+0","","","",    "","","","",
                    //
                    "(R)(#0.7#+2)(ConvR)(Step)(LineR)","","","",    "","","^+2(LineR)","",    "+0(LineL)","","","",    "","","","",
                    "(R)(#0.7#+2)","","","",    "","","^+2","",    "+01","","+21","",    "+21","","","",
                    ////
                    "(R1)(#0.7#+21)(ConvR)(Step)(LineR)","","","",    "","","^+21(LineR)","",    "+01(LineL)","","","",    "","","","",
                    "(R1)(#0.7#+21)","","","",    "","","^+21","",    "+01","","","",    "","","","",
                    //
                    "(R1)(#0.7#+21)(ConvL)(Step)(LineL)","","","",    "","","^+21(LineL)","",    "+01(LineR)","","","",    "","","","",
                    "(R1)(#0.7#+21)","","","",    "","","^+21","",    "+0","","+2","",    "+2","","","",
                    //
                    "(R)(+2)(ConvL)(LineL)(LineR)","","","",    "","","+1","",    "+1","","+1","",    "+1","","","",
                    "(R1)(+21)(ConvR)(LineL)(LineR)","","","",    "","","+31","",    "+31","","+31","",    "+31","","","",
                    //
                    "(R1)(+21)(ConvL)(LineLb)(LineRb)","","","",    "","","+31","",    "+31","","+31","",    "+31","","","",
                    "(R)(+2)(ConvR)(LineLb)(LineRb)","","","",    "","","+1","",    "+1","","+1","",    "+1","","","",
                    ////
                    "(R1)(+21)(ConvR)(LineL)(LineR)","","","",    "","","+31","",    "+31","","+31","",    "+31","","","",
                    "(R)(+2)(ConvL)(LineLb)(LineRb)","","","",    "","","+1","",    "+1","","+1","",    "+1","","","",
                    //
                    "(R)(+2)(ConvR)(LineL)(LineR)","","","",    "","","+1","",    "+1","","+1","",    "+1","","","",
                    "(R1)(+21)(ConvL)(LineLb)(LineRb)","","","",    "","","+31","",    "+31","","+31","",    "+31","","","",
                    //
                    "(R1)(#0.7#+21)(LineL)(LineR)(LineG1)","","","",    "","","+21(LineL)","",    "+111(LineRb)","","+111(LineLb)","",    "+111(LineR)","","","",
                    "(R1)(#0.7#+21)(LineLb)(LineRb)","","","",    "","","+21(LineR)","",    "+111(LineLb)","","+111(LineL)","",    "+111(LineRb)","","","",
                    //
                    "(R1)(#0.7#+21)(LineL)(LineR)","","","",    "","","+21(LineL)","",    "+111(LineRb)","","+111(LineLb)","",    "+111(LineR)","","","",
                    "R1(LineRand)","","D1(LineRand)","",    "D1(LineRand)","","D1(LineRand)","",    "D1(LineRand)","","D1(LineRand)","",    "D1(LineRand)","","","",
                    ////
                    "(R)(#0.7#+2)(LineLb)(LineRb)(LineG2)","","","",    "","","+2(LineL)","",    "+301(LineRb)","","+301(LineR)","",    "+301(LineLb)","","","",
                    "(R)(#0.7#+2)(LineL)(LineR)","","","",    "","","+2(LineR)","",    "+301(LineLb)","","+301(LineRb)","",    "+301(LineR)","","","",
                    //
                    "(R)(#0.7#+2)(LineLb)(LineRb)","","","",    "","","+2(LineL)","",    "+301(LineRb)","","+301(LineR)","",    "+301(LineLb)","","","",
                    "R(LineRand)","","D(LineRand)","",    "D(LineRand)","","D(LineRand)","",    "D(LineRand)","","D(LineRand)","",    "D(LineRand)","","","",
                    //
                });
            }
            void ExTurn1()
            {
                RegisterFunctionOnce("LineA", () =>
                {
                    int r1 = Rand(84, 96);
                    int r2 = Rand(84, 96);
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(1.5f), CentreEasing.EaseInSine(new(320, 240), new(-20, 240), BeatTime(1.5f)));
                    c2.Insert(BeatTime(1.5f), CentreEasing.EaseInSine(new(320, 240), new(660, 240), BeatTime(1.5f)));
                    Line a = new(c1.GetResult(), (s) => { return r1; }) { Alpha = 0.01f };
                    Line b = new(c2.GetResult(), (s) => { return r2; }) { Alpha = 0.01f };
                    Line[] lines = { a, b };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        l.AlphaIncrease(BeatTime(1.5f), 0.75f);
                        for (int i = 0; i < 4; i++)
                        {
                            l.InsertRetention(new(1.4f * i, 0.8f - 0.12f * i));
                        }
                        DelayBeat(1.55f, () => { l.Dispose(); });
                    }
                });
                CreateEntity(new GreenSoulGB(BeatTime(4), 3, 0, BeatTime(4f * (190f / 185f + 19f / 18f + 190f / 175f + 1f))));
                CreateEntity(new GreenSoulGB(BeatTime(4), 3, 1, BeatTime(4f * (190f / 185f + 19f / 18f + 190f / 175f + 1f))));
                BarrageCreate(BeatTime(4f), BeatTime(1), 6.6f, new string[]
                {
                    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",
                    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",
                });
                BarrageCreate(BeatTime(4f * (2f)), BeatTime(190f / 185f), 6.6f, new string[]
                {
                    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",
                    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",
                });
                BarrageCreate(BeatTime(4f * (2f + 190f / 185f)), BeatTime(19f / 18f), 6.6f, new string[]
                {
                    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",    "LineA","","LineA","",
                    "LineA","","","",    "LineA","","","",    "LineA","","","",    "LineA","","","",
                });
                BarrageCreate(BeatTime(4f * (2f + 190f / 185f + 19f / 18f)), BeatTime(190f / 175f), 6.6f, new string[]
                {
                    "LineA","","","",    "LineA","","","",    "LineA","","","",    "LineA","","","",
                    "LineA","","","",    "LineA","","","",    "LineA","","","",    "LineA","","","",
                });
                BarrageCreate(BeatTime(4f * (2f + 190f / 185f + 19f / 18f + 190f / 175f)), BeatTime(19f / 17f), 6.6f, new string[]
                {
                    "","","","",    "","","","",    "/","","","",    "","","","",
                    "/","","","",    "","","","",    "/","","","",    "","","","",
                });
            }
            void ExPart6()
            {
                RegisterFunctionOnce("LineAd", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(1.25f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(240, 240), BeatTime(1.25f * 19f / 17f)));
                    c1.Insert(BeatTime(0.5f * 19f / 17f), CentreEasing.Stable(new(240, 240)));
                    c2.Insert(BeatTime(1.25f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(400, 240), BeatTime(1.25f * 19f / 17f)));
                    c2.Insert(BeatTime(0.5f * 19f / 17f), CentreEasing.Stable(new(400, 240)));
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    v1.Insert(BeatTime(1.25f * 19f / 17f), ValueEasing.EaseOutQuad(90, 95, BeatTime(1.25f * 19f / 17f)));
                    v1.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(95));
                    v2.Insert(BeatTime(1.25f * 19f / 17f), ValueEasing.EaseOutQuad(90, 85, BeatTime(1.25f * 19f / 17f)));
                    v2.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(85));
                    Line a = new(320, 90) { Alpha = 0.7f };
                    Line b = new(c1.GetResult(), v1.GetResult()) { Alpha = 0.7f };
                    Line c = new(c2.GetResult(), v2.GetResult()) { Alpha = 0.7f };
                    Line[] lines = { a, b, c };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        DelayBeat(1 * 19f / 17f, () => { l.AlphaDecrease(BeatTime(1.5f * 19f / 17f)); });
                        DelayBeat(2.5f * 19f / 17f, () => { l.Dispose(); });
                    }
                });
                RegisterFunctionOnce("LineAu", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(1.25f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(240, 240), BeatTime(1.25f * 19f / 17f)));
                    c1.Insert(BeatTime(0.5f * 19f / 17f), CentreEasing.Stable(new(240, 240)));
                    c2.Insert(BeatTime(1.25f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(400, 240), BeatTime(1.25f * 19f / 17f)));
                    c2.Insert(BeatTime(0.5f * 19f / 17f), CentreEasing.Stable(new(400, 240)));
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    v1.Insert(BeatTime(1.25f * 19f / 17f), ValueEasing.EaseOutQuad(90, 95, BeatTime(1.25f * 19f / 17f)));
                    v1.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(95));
                    v2.Insert(BeatTime(1.25f * 19f / 17f), ValueEasing.EaseOutQuad(90, 85, BeatTime(1.25f * 19f / 17f)));
                    v2.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(85));
                    Line a = new(320, 90) { Alpha = 0.7f };
                    Line b = new(c1.GetResult(), v2.GetResult()) { Alpha = 0.7f };
                    Line c = new(c2.GetResult(), v1.GetResult()) { Alpha = 0.7f };
                    Line[] lines = { a, b, c };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        DelayBeat(1 * 19f / 17f, () => { l.AlphaDecrease(BeatTime(1.5f * 19f / 17f)); });
                        DelayBeat(2.5f * 19f / 17f, () => { l.Dispose(); });
                    }
                });
                RegisterFunctionOnce("LineAl", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(1.25f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(320, 160), BeatTime(1.25f * 19f / 17f)));
                    c1.Insert(BeatTime(0.5f * 19f / 17f), CentreEasing.Stable(new(320, 160)));
                    c2.Insert(BeatTime(1.25f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(320, 320), BeatTime(1.25f * 19f / 17f)));
                    c2.Insert(BeatTime(0.5f * 19f / 17f), CentreEasing.Stable(new(320, 320)));
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    v1.Insert(BeatTime(1.25f * 19f / 17f), ValueEasing.EaseOutQuad(0, 5, BeatTime(1.25f * 19f / 17f)));
                    v1.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(5));
                    v2.Insert(BeatTime(1.25f * 19f / 17f), ValueEasing.EaseOutQuad(0, -5, BeatTime(1.25f * 19f / 17f)));
                    v2.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(-5));
                    Line a = new(320, 0) { Alpha = 0.7f };
                    Line b = new(c1.GetResult(), v2.GetResult()) { Alpha = 0.7f };
                    Line c = new(c2.GetResult(), v1.GetResult()) { Alpha = 0.7f };
                    Line[] lines = { a, b, c };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        DelayBeat(1 * 19f / 17f, () => { l.AlphaDecrease(BeatTime(1.5f * 19f / 17f)); });
                        DelayBeat(2.5f * 19f / 17f, () => { l.Dispose(); });
                    }
                });
                RegisterFunctionOnce("LineAr", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(1.25f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(320, 160), BeatTime(1.25f * 19f / 17f)));
                    c1.Insert(BeatTime(0.5f * 19f / 17f), CentreEasing.Stable(new(320, 160)));
                    c2.Insert(BeatTime(1.25f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(320, 320), BeatTime(1.25f * 19f / 17f)));
                    c2.Insert(BeatTime(0.5f * 19f / 17f), CentreEasing.Stable(new(320, 320)));
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    v1.Insert(BeatTime(1.25f * 19f / 17f), ValueEasing.EaseOutQuad(0, 5, BeatTime(1.25f * 19f / 17f)));
                    v1.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(5));
                    v2.Insert(BeatTime(1.25f * 19f / 17f), ValueEasing.EaseOutQuad(0, -5, BeatTime(1.25f * 19f / 17f)));
                    v2.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(-5));
                    Line a = new(320, 0) { Alpha = 0.7f };
                    Line b = new(c1.GetResult(), v1.GetResult()) { Alpha = 0.7f };
                    Line c = new(c2.GetResult(), v2.GetResult()) { Alpha = 0.7f };
                    Line[] lines = { a, b, c };
                    foreach (Line l in lines)
                    {
                        CreateEntity(l);
                        DelayBeat(1 * 19f / 17f, () => { l.AlphaDecrease(BeatTime(1.5f * 19f / 17f)); });
                        DelayBeat(2.5f * 19f / 17f, () => { l.Dispose(); });
                    }
                });
                RegisterFunctionOnce("LineB", () =>
                {
                    int r = Rand(0, 1);
                    if (r == 0)
                    {
                        CentreEasing.EaseBuilder c = new();
                        ValueEasing.EaseBuilder v = new();
                        c.Insert(BeatTime(4f * 19f / 17f), CentreEasing.Linear(new(320, 480), new(320, 120), BeatTime(4f * 19f / 17f)));
                        v.Insert(BeatTime(4f * 19f / 17f), ValueEasing.EaseOutSine(0, 3, BeatTime(4f * 19f / 17f)));
                        Line a = new(c.GetResult(), v.GetResult()) { Alpha = 0.75f };
                        CreateEntity(a);
                        a.AlphaDecrease(BeatTime(4f * 19f / 17f));
                        DelayBeat(4f * 19f / 17f, () => { a.Dispose(); });
                    }
                    else
                    {
                        CentreEasing.EaseBuilder c = new();
                        ValueEasing.EaseBuilder v = new();
                        c.Insert(BeatTime(4f * 19f / 17f), CentreEasing.Linear(new(320, 480), new(320, 120), BeatTime(4f * 19f / 17f)));
                        v.Insert(BeatTime(4f * 19f / 17f), ValueEasing.EaseOutSine(0, -3, BeatTime(4f * 19f / 17f)));
                        Line a = new(c.GetResult(), v.GetResult()) { Alpha = 0.75f };
                        CreateEntity(a);
                        a.AlphaDecrease(BeatTime(4f * 19f / 17f));
                        DelayBeat(4f * 19f / 17f, () => { a.Dispose(); });
                    }
                });
                RegisterFunctionOnce("LineC1", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    ValueEasing.EaseBuilder v = new();
                    c.Insert(BeatTime(1f * 19f / 17f), CentreEasing.EaseInCirc(new(0, 240), new(320, 240), BeatTime(1f * 19f / 17f)));
                    c.Insert(BeatTime(1f * 19f / 17f), CentreEasing.EaseOutCirc(new(320, 240), new(640, 240), BeatTime(1f * 19f / 17f)));
                    c.Insert(BeatTime(1f * 19f / 17f), CentreEasing.EaseInCirc(new(640, 240), new(320, 240), BeatTime(1f * 19f / 17f)));
                    c.Insert(BeatTime(1f * 19f / 17f), CentreEasing.EaseOutCirc(new(320, 240), new(0, 240), BeatTime(1f * 19f / 17f)));
                    v.Insert(BeatTime(0.75f * 19f / 17f), ValueEasing.EaseOutCubic(90, 97, BeatTime(0.75f * 19f / 17f)));
                    v.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(97));
                    v.Insert(BeatTime(0.75f * 19f / 17f), ValueEasing.EaseInCubic(97, 90, BeatTime(0.75f * 19f / 17f)));
                    v.Insert(BeatTime(0.75f * 19f / 17f), ValueEasing.EaseOutCubic(90, 83, BeatTime(0.75f * 19f / 17f)));
                    v.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(83));
                    v.Insert(BeatTime(0.75f * 19f / 17f), ValueEasing.EaseInCubic(83, 90, BeatTime(0.75f * 19f / 17f)));
                    Line a = new(c.GetResult(), v.GetResult()) { Alpha = 0.4f };
                    CreateEntity(a);
                    a.AlphaIncreaseAndDecrease(BeatTime(2f * 19f / 17f), 0.25f);
                    DelayBeat(2f * 19f / 17f, () => { a.AlphaDecreaseAndIncrease(BeatTime(2f * 19f / 17f), 0.15f); });
                    DelayBeat(4.02f * 19f / 17f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("LineC2", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    ValueEasing.EaseBuilder v = new();
                    c.Insert(BeatTime(1f * 19f / 17f), CentreEasing.EaseInCirc(new(640, 240), new(320, 240), BeatTime(1f * 19f / 17f)));
                    c.Insert(BeatTime(1f * 19f / 17f), CentreEasing.EaseOutCirc(new(320, 240), new(0, 240), BeatTime(1f * 19f / 17f)));
                    c.Insert(BeatTime(1f * 19f / 17f), CentreEasing.EaseInCirc(new(0, 240), new(320, 240), BeatTime(1f * 19f / 17f)));
                    c.Insert(BeatTime(1f * 19f / 17f), CentreEasing.EaseOutCirc(new(320, 240), new(640, 240), BeatTime(1f * 19f / 17f)));
                    v.Insert(BeatTime(0.75f * 19f / 17f), ValueEasing.EaseOutCubic(90, 83, BeatTime(0.75f * 19f / 17f)));
                    v.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(83));
                    v.Insert(BeatTime(0.75f * 19f / 17f), ValueEasing.EaseInCubic(83, 90, BeatTime(0.75f * 19f / 17f)));
                    v.Insert(BeatTime(0.75f * 19f / 17f), ValueEasing.EaseOutCubic(90, 97, BeatTime(0.75f * 19f / 17f)));
                    v.Insert(BeatTime(0.5f * 19f / 17f), ValueEasing.Stable(97));
                    v.Insert(BeatTime(0.75f * 19f / 17f), ValueEasing.EaseInCubic(97, 90, BeatTime(0.75f * 19f / 17f)));
                    Line a = new(c.GetResult(), v.GetResult()) { Alpha = 0.4f };
                    CreateEntity(a);
                    a.AlphaIncreaseAndDecrease(BeatTime(2f * 19f / 17f), 0.25f);
                    DelayBeat(2f * 19f / 17f, () => { a.AlphaDecreaseAndIncrease(BeatTime(2f * 19f / 17f), 0.15f); });
                    DelayBeat(4.02f * 19f / 17f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("LineD1", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    c.Insert(BeatTime(4f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(120, 120), BeatTime(4f * 19f / 17f)));
                    c.Insert(BeatTime(4f * 19f / 17f), CentreEasing.EaseInCirc(new(120, 120), new(640, 640), BeatTime(4f * 19f / 17f)));
                    v1.Insert(BeatTime(4f * 19f / 17f), ValueEasing.EaseOutQuad(0, 315, BeatTime(4f * 19f / 17f)));
                    v2.Insert(BeatTime(4f * 19f / 17f), ValueEasing.EaseOutQuad(-90, 315, BeatTime(4f * 19f / 17f)));
                    v1.Insert(BeatTime(4f * 19f / 17f), ValueEasing.Stable(135));
                    Line a = new(c.GetResult(), v1.GetResult()) { Alpha = 0.65f };
                    Line b = new(c.GetResult(), v2.GetResult()) { Alpha = 0.65f };
                    CreateEntity(a); CreateEntity(b);
                    for (int i = 0; i < 6; i++)
                    {
                        a.InsertRetention(new(1.2f * i, 0.9f - 0.1f * i));
                        b.InsertRetention(new(1.2f * i, 0.9f - 0.1f * i));
                    }
                    DelayBeat(4f * 19f / 17f, () => { b.Dispose(); });
                    DelayBeat(8f * 19f / 17f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("LineD2", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    c.Insert(BeatTime(4f * 19f / 17f), CentreEasing.EaseOutQuad(new(320, 240), new(520, 120), BeatTime(4f * 19f / 17f)));
                    c.Insert(BeatTime(4f * 19f / 17f), CentreEasing.EaseInCirc(new(520, 120), new(0, 640), BeatTime(4f * 19f / 17f)));
                    v1.Insert(BeatTime(4f * 19f / 17f), ValueEasing.EaseOutQuad(0, -315, BeatTime(4f * 19f / 17f)));
                    v2.Insert(BeatTime(4f * 19f / 17f), ValueEasing.EaseOutQuad(90, -315, BeatTime(4f * 19f / 17f)));
                    v1.Insert(BeatTime(4f * 19f / 17f), ValueEasing.Stable(135));
                    Line a = new(c.GetResult(), v1.GetResult()) { Alpha = 0.65f };
                    Line b = new(c.GetResult(), v2.GetResult()) { Alpha = 0.65f };
                    CreateEntity(a); CreateEntity(b);
                    for (int i = 0; i < 6; i++)
                    {
                        a.InsertRetention(new(1.2f * i, 0.9f - 0.1f * i));
                        b.InsertRetention(new(1.2f * i, 0.9f - 0.1f * i));
                    }
                    DelayBeat(4f * 19f / 17f, () => { b.Dispose(); });
                    DelayBeat(8f * 19f / 17f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("LineE", () =>
                {

                });
                BarrageCreate(BeatTime(4f), BeatTime(19f / 17f), 6.6f, new string[]
                {
                    "(R1)(#7.8#R)(LineAd)(LineB)(LineC1)","","","",    "(LineC1)","","","",    "R1(LineB)(LineC1)","","","",    "D1","","","",
                    "","","","",    "R1(LineB)","","","",    "D1","","","",    "","","D1(LineB)","",
                    //
                    "","","R","",    "","","","",    "R(LineB)","","","",    "D","","","",
                    "","","R(LineB)","",    "","","","",    "R","","","",    "","","","",
                    //
                    "(R1)(#7.8#R)(LineAl)(LineB)(LineC2)","","","",    "(LineC2)","","","",    "R1(LineB)(LineC2)","","","",    "D1","","","",
                    "","","","",    "R1(LineB)","","","",    "D1","","","",    "","","D1(LineB)","",
                    ////
                    "","","","",    "R","","","",    "D(LineB)","","","",    "D","","","",
                    "D(LineB)","","","",    "","","","",    "R","","","",    "","","","",
                    //
                    "(R1)(#7.8#R)(LineAr)(LineB)(LineD2)","","","",    "","","","",    "R1(LineB)","","","",    "D1","","","",
                    "","","","",    "R1(LineB)","","","",    "D1","","","",    "","","D1(LineB)","",
                    //
                    "","","R(LineB)","",    "","","","",    "R","","","",    "D(LineB)","","","",
                    "D","","","",    "","","","",    "R(LineB)","","","",    "D","","","",
                    //
                    "D(LineB)(LineC1)(LineC2)","","+2","",    "+2","","","",    "+002(LineB)(LineC1)(LineC2)","","","",    "","","+002","",
                    "(LineB)(LineC1)(LineC2)","","N01","",    "D1","","D1","",    "D1(LineB)","","D1","",    "D1","","","",
                    ////
                    "(R)(#7.8#R1)(LineAu)(LineB)(LineC2)","","","",    "(LineC2)","","","",    "R(LineC2)","","","",    "D(LineB)","","","",
                    "","","","",    "R","","","",    "D(LineB)","","","",    "","","D","",
                    //
                    "","","R1(LineB)","",    "","","","",    "R1","","","",    "D1(LineB)","","","",
                    "","","R1","",    "","","","",    "R1(LineB)","","","",    "","","","",
                    //
                    "(R)(#7.8#R1)(LineAr)(LineB)(LineC1)","","","",    "(LineC1)","","","",    "R(LineC1)","","","",    "D(LineB)","","","",
                    "","","","",    "R","","","",    "D(LineB)","","","",    "","","D","",
                    //
                    "","","","",    "R1(LineB)","","","",    "D1","","","",    "D1(LineB)","","","",
                    "D1","","","",    "","","","",    "R1(LineB)","","","",    "D1","","","",
                    ////
                    "(R)(#7.8#R1)(LineAl)(LineB)(LineD1)","","","",    "","","","",    "R","","","",    "D(LineB)","","","",
                    "","","","",    "R","","","",    "D(LineB)","","","",    "","","D","",
                    //
                    "","","R1(LineB)","",    "","","","",    "R1","","","",    "D1(LineB)","","","",
                    "D1","","","",    "","","","",    "R1(LineB)","","","",    "","","","",
                    //
                    "R1(LineB)(LineC1)(LineC2)","","+21","",    "+21","","","",    "D1(LineB)(LineC1)(LineC2)","","","",    "R1","","+21","",
                    "+21(LineB)(LineC1)(LineC2)","","","",    "R","","D","",    "D(LineB)","","D","",    "D","","D","",
                    //
                });
            }
            void ExPart7()
            {
                BarrageCreate(BeatTime(4), BeatTime(19f / 17f), 6.6f, new string[]
                {
                    "(R1)(+2)","","","",    "D","","","",    "","","","",    "","","","",
                    "R","","","",    "","","","",    "R","","","",    "","","","",
                    ////
                    "R","","","",    "D","","","",    "","","","",    "","","","",
                    "*R12","","*+012","",    "*+012","","*+012","",    "*+212","","*+012","",    "*+012","","*+012","",
                    //
                    "(R)(+01)","","","",    "","","","",    "(D)(+01)","","","",    "","","","",
                    "(D)(+01)","","","",    "","","","",    "(D)(+01)","","","",    "","","","",
                    //
                    "R1","","","",    "D1","","","",    "","","","",    "","","","",
                    "R1","","","",    "","","","",    "R1","","","",    "","","","",
                    //
                    "(R)(+21)","","","",    "D1","","","",    "","","","",    "","","","",
                    "*R02","","*+002","",    "*+002","","*+002","",    "*+202","","*+002","",    "*+002","","*+002","",
                    ////
                    "$3{Lm}","","+01{Li}","",    "+0","","+01{Ri}","",    "+0{Rm}","","","",    "","","","",
                    "R","","","",    "R1","","+21","",    "+21","","+21","",    "+21","","+21","",
                    //
                    "(R)(+21)","","","",    "","","","",    "","","","",    "","","","",
                    "R1","","","",    "D1","","","",    "D1","","","",    "","","","",
                    //
                    "R1","","","",    "D1","","","",    "","","","",    "","","","",
                    "R1","","","",    "","","","",    "R","","","",    "D","","","",
                    //
                    "(R1)(+2)","","","",    "D","","","",    "","","","",    "","","","",
                    "*R02","","*+002","",    "*+002","","*+002","",    "*+202","","*+002","",    "*+002","","*+002","",
                    ////
                    "(R)(+01)","","","",    "","","","",    "(D)(+01)","","","",    "","","","",
                    "(D)(+01)","","","",    "","","","",    "(D)(+01)","","","",    "","","","",
                    //
                    "($31)($1{Ri})","","+0{Li}","",    "+0{Ri}","","+0{Li}","",    "","","","",    "","","","",
                    "R","","","",    "","","","",    "R","","","",    "D","","","",
                    //
                    "R","","","",    "","","","",    "R","","","",    "","","","",
                    "*R12","","*+012","",    "*+012","","*+012","",    "*+212","","*+012","",    "*+012","","*+012","",
                    //
                    "(R1)(+2)","","","",    "","","","",    "","","","",    "","","","",
                    "R1","","","",    "D1","","","",    "","","","",    "R1","","","",
                    ////
                    "R1","","","",    "","","","",    "R1","","","",    "","","","",
                    "R1","","","",    "#15#$01","","","",    "","","","",    "","","","",
                    //
                });
            }
            void ExTurn2()
            {
                RegisterFunctionOnce("fake", () =>
                {
                    float t = 0;
                    CreateEntity(new FakeArrow(0, 0, 0, new(320, 290), BeatTime(2 * 19f / 14f), 1, 90));
                    for (int i = 0; i < 8; i++)
                    {
                        DelayBeat(1 * 19f / 14f + i * 0.125f * 19f / 14f, () =>
                        {
                            t++;
                            CreateEntity(new FakeArrow(0, 0, 0, new(320, 290 + t * 20), BeatTime(1.125f * 19f / 14f - t * 0.125f * 19f / 14f), 1, 90));
                            Line a = new(new Vector2(320, 290 + t * 20), 20) { Alpha = 0.6f - t * 0.05f };
                            Line b = new(new Vector2(320, 290 + t * 20), -20) { Alpha = 0.6f - t * 0.05f };
                            Line[] l = { a, b };
                            foreach (Line lines in l)
                            {
                                CreateEntity(lines);
                                lines.VerticalMirror = true;
                                DelayBeat(1.125f * 19f / 14f - t * 0.125f * 19f / 14f, () =>
                                {
                                    lines.Dispose();
                                });
                            }
                        });
                    }
                });
                BarrageCreate(BeatTime(4), BeatTime(19f / 17f), 6.6f, new string[]
                {
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "$2","","","",    "","","","",    "","","","",    "","","","",
                    //
                });
                BarrageCreate(BeatTime(4f * (1f + 19f / 17f)), BeatTime(19f / 16f), 6.3f, new string[]
                {
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "$3","","","",    "","","","",    "","","","",    "","","","",
                    //
                });
                BarrageCreate(BeatTime(4f * (1f + 19f / 17f + 19f / 16f)), BeatTime(19f / 15f), 6f, new string[]
                {
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "$0","","","",    "","","","",    "","","","",    "","","","",
                    ////
                });
                BarrageCreate(BeatTime(4f * (1f + 19f / 17f + 19f / 16f + 19f / 15f)), BeatTime(19f / 14f), 5.7f, new string[]
                {
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "($1)(fake)","","","",    "","","","",    "","","","",    "","","","",
                    //
                });
            }
            void ExPart8()
            {
                RegisterFunctionOnce("change", () =>
                {
                    ValueEasing.EaseBuilder r1 = new();
                    ValueEasing.EaseBuilder r2 = new();
                    ValueEasing.EaseBuilder r3 = new();
                    ValueEasing.EaseBuilder r4 = new();
                    r1.Insert(BeatTime(6f * 19f / 14f), ValueEasing.EaseOutCubic(0, 270, BeatTime(6f * 19f / 14f)));
                    r1.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.Stable(270));
                    r1.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(270, 270 + 60, BeatTime(4f * 19f / 14f)));
                    r1.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(270 + 60, 270 + 120, BeatTime(4f * 19f / 14f)));
                    r2.Insert(BeatTime(6f * 19f / 14f), ValueEasing.EaseOutCubic(0, 360 * 2 + 60 + 90, BeatTime(6f * 19f / 14f)));
                    r2.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.Stable(360 * 2 + 60 + 90));
                    r2.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 * 2 + 60 + 90, 360 * 2 + 60 + 60 + 90, BeatTime(4f * 19f / 14f)));
                    r2.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 * 2 + 60 + 60 + 90, 360 * 2 + 60 + 120 + 90, BeatTime(4f * 19f / 14f)));
                    r3.Insert(BeatTime(6f * 19f / 14f), ValueEasing.EaseOutCubic(0, 360 + 120 + 90, BeatTime(6f * 19f / 14f)));
                    r3.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.Stable(360 + 120 + 90));
                    r3.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 + 120 + 90, 360 + 120 + 60 + 90, BeatTime(4f * 19f / 14f)));
                    r3.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 + 120 + 60 + 90, 360 + 120 + 120 + 90, BeatTime(4f * 19f / 14f)));
                    r4.Insert(BeatTime(6f * 19f / 14f), ValueEasing.EaseOutCubic(0, 360 * 2 + 60 + 90, BeatTime(6f * 19f / 14f)));
                    r4.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.Stable(360 * 2 + 60 + 90));
                    r4.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 * 2 + 60 + 90, 360 * 2 + 60 + 60 + 90, BeatTime(4f * 19f / 14f)));
                    r4.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 * 2 + 60 + 60 + 90, 360 * 2 + 60 + 120 + 90, BeatTime(4f * 19f / 14f)));
                    r1.Insert(BeatTime(2f * 19f / 14f), ValueEasing.EaseInCirc(390, 540, BeatTime(2f * 19f / 14f)));
                    r2.Insert(BeatTime(2f * 19f / 14f), ValueEasing.EaseInCirc(360 * 2 + 60 + 120 + 90, 360 * 2 + 60 + 120 + 180, BeatTime(2f * 19f / 14f)));
                    r3.Insert(BeatTime(2f * 19f / 14f), ValueEasing.EaseInCirc(690, 540, BeatTime(2f * 19f / 14f)));
                    r4.Insert(BeatTime(2f * 19f / 14f), ValueEasing.EaseInCirc(360 * 2 + 60 + 120 + 90, 360 * 2 + 60 + 120, BeatTime(2f * 19f / 14f)));
                    Line a = new(new Vector2(320, 240), r1.GetResult()) { Alpha = 0.6f };
                    Line b = new(new Vector2(320, 240), r2.GetResult()) { Alpha = 0.3f };
                    Line c = new(new Vector2(320, 240), r3.GetResult()) { Alpha = 0.6f };
                    Line d = new(new Vector2(320, 240), r4.GetResult()) { Alpha = 0.3f };
                    Line[] line = { a, b, c, d };
                    foreach (Line l in line)
                    {
                        CreateEntity(l);
                        for (int i = 0; i < 5; i++)
                        {
                            l.InsertRetention(new(1 * i, 0.5f - 0.3f / 5 * i));
                        }
                        DelayBeat(16f * 19f / 14f, () =>
                        {
                            l.AlphaDecrease(BeatTime(1f), 0.6f);
                            DelayBeat(2f * 19f / 14f, () =>
                            {
                                l.Dispose();
                            });
                        });
                    }
                });
                RegisterFunctionOnce("arrow", () =>
                {
                    for (int i = 1; i < 9; i++)
                    {
                        Arrow Aa1 = MakeArrow(BeatTime(i * 0.5f * 19f / 14f), 3, 5.7f, 0, 0);
                        Arrow Aa2 = MakeArrow(BeatTime(i * 0.5f * 19f / 14f), 1, 5.7f, 0, 0);
                        Arrow[] a = { Aa1, Aa2 };
                        foreach (Arrow ar1 in a)
                        {
                            ValueEasing.EaseBuilder r1 = new();
                            r1.Insert(BeatTime(6f * 19f / 14f), ValueEasing.EaseOutCubic(0, 270, BeatTime(6f * 19f / 14f)));
                            r1.Insert(BeatTime(2f * 19f / 14f), ValueEasing.Stable(270));
                            CreateEntity(ar1);
                            ar1.Stop(BeatTime(4.25f * 19f / 17f));
                            r1.Run(s => ar1.CentreRotationOffset = s - 90);
                        }
                    }
                    for (int i = 1; i < 9; i++)
                    {
                        Arrow Aa1 = MakeArrow(BeatTime(i * 0.5f * 19f / 14f), 3, 5.7f, 0, 0);
                        Arrow Aa2 = MakeArrow(BeatTime(i * 0.5f * 19f / 14f), 1, 5.7f, 0, 0);
                        Arrow[] a = { Aa1, Aa2 };
                        foreach (Arrow ar1 in a)
                        {
                            ValueEasing.EaseBuilder r2 = new();
                            r2.Insert(BeatTime(6f * 19f / 14f), ValueEasing.EaseOutCubic(0, 360 * 2 + 60 + 90, BeatTime(6f * 19f / 14f)));
                            r2.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.Stable(360 * 2 + 60 + 90));
                            r2.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 * 2 + 60 + 90, 360 * 2 + 60 + 60 + 90, BeatTime(4f * 19f / 14f)));
                            r2.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 * 2 + 60 + 60 + 90, 360 * 2 + 60 + 120 + 90, BeatTime(4f * 19f / 14f)));
                            CreateEntity(ar1);
                            ar1.Stop(BeatTime(14f * 19f / 17f));
                            r2.Run(s => ar1.CentreRotationOffset = s - 90);
                        }
                    }
                    for (int i = 1; i < 9; i++)
                    {
                        Arrow Aa1 = MakeArrow(BeatTime(i * 0.5f * 19f / 14f), 3, 5.7f, 1, 0);
                        Arrow Aa2 = MakeArrow(BeatTime(i * 0.5f * 19f / 14f), 1, 5.7f, 1, 0);
                        Arrow[] a = { Aa1, Aa2 };
                        foreach (Arrow ar1 in a)
                        {
                            ValueEasing.EaseBuilder r3 = new();
                            r3.Insert(BeatTime(6f * 19f / 14f), ValueEasing.EaseOutCubic(0, 360 + 120 + 90, BeatTime(6f * 19f / 14f)));
                            r3.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.Stable(360 + 120 + 90));
                            r3.Insert(BeatTime(4f * 19f / 14f), ValueEasing.EaseOutQuart(360 + 120 + 90, 360 + 120 + 60 + 90, BeatTime(4f * 19f / 14f)));
                            CreateEntity(ar1);
                            ar1.Stop(BeatTime(9.125f * 19f / 17f));
                            r3.Run(s => ar1.CentreRotationOffset = s - 90);
                        }
                    }
                });
                RegisterFunctionOnce("lineLA", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    c.Insert(BeatTime(2f * 19f / 14f), CentreEasing.EaseInQuad(new(320, 240), new(320 - 180, 380), BeatTime(2f * 19f / 14f)));
                    c.Insert(BeatTime(3f * 19f / 14f), CentreEasing.EaseInQuad(new(320 - 180, 380), new(320 - 180, -400), BeatTime(3f * 19f / 14f)));
                    v1.Insert(BeatTime(2f * 19f / 14f), ValueEasing.Linear(-270, 90, BeatTime(2f * 19f / 14f)));
                    v1.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.EaseOutCubic(90, 40, BeatTime(1.25f * 19f / 14f)));
                    v1.Insert(BeatTime(8f * 19f / 14f), ValueEasing.Stable(40));
                    v2.Insert(BeatTime(2f * 19f / 14f), ValueEasing.Linear(-270, 90, BeatTime(2f * 19f / 14f)));
                    v2.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.EaseOutCubic(90, 140, BeatTime(1.25f * 19f / 14f)));
                    v2.Insert(BeatTime(8f * 19f / 14f), ValueEasing.Stable(140));
                    Line la = new(c.GetResult(), v1.GetResult()) { Alpha = 0.75f };
                    Line lb = new(c.GetResult(), v2.GetResult()) { Alpha = 0.75f };
                    Line[] a = { la, lb };
                    foreach (Line l in a)
                    {
                        CreateEntity(l);
                        DelayBeat(5f * 19f / 14f, () => { l.Dispose(); });
                        DelayBeat(2f * 19f / 14f, () =>
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                l.InsertRetention(new(3 * 19f / 14f * i, 0.8f - 0.8f / 4 * i));
                            }
                        });
                    }
                });
                RegisterFunctionOnce("lineRA", () =>
                {
                    CentreEasing.EaseBuilder c = new();
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    c.Insert(BeatTime(2f * 19f / 14f), CentreEasing.EaseInQuad(new(320, 240), new(320 + 180, 380), BeatTime(2f * 19f / 14f)));
                    c.Insert(BeatTime(3f * 19f / 14f), CentreEasing.EaseInQuad(new(320 + 180, 380), new(320 + 180, -400), BeatTime(3f * 19f / 14f)));
                    v1.Insert(BeatTime(2f * 19f / 14f), ValueEasing.Linear(270, -90, BeatTime(2f * 19f / 14f)));
                    v1.Insert(BeatTime(1.25f), ValueEasing.EaseOutCubic(-90, -40, BeatTime(1.25f)));
                    v1.Insert(BeatTime(8f * 19f / 14f), ValueEasing.Stable(-40));
                    v2.Insert(BeatTime(2f * 19f / 14f), ValueEasing.Linear(270, -90, BeatTime(2f * 19f / 14f)));
                    v2.Insert(BeatTime(1.25f * 19f / 14f), ValueEasing.EaseOutCubic(-90, -140, BeatTime(1.25f)));
                    v2.Insert(BeatTime(8f * 19f / 14f), ValueEasing.Stable(-140));
                    Line la = new(c.GetResult(), v1.GetResult()) { Alpha = 0.75f };
                    Line lb = new(c.GetResult(), v2.GetResult()) { Alpha = 0.75f };
                    Line[] a = { la, lb };
                    foreach (Line l in a)
                    {
                        CreateEntity(l);
                        DelayBeat(5f * 19f / 14f, () => { l.Dispose(); });
                        DelayBeat(2f * 19f / 14f, () =>
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                l.InsertRetention(new(3 * i, 0.8f - 0.8f / 4 * i));
                            }
                        });
                    }
                });
                RegisterFunctionOnce("lineGA", () =>
                {
                    DelayBeat(0, () =>
                    {
                        Line la = new(new Vector2(128 * 1 - 10, 96 * 1 - 10), 110) { Alpha = 0.72f };
                        Line lb = new(new Vector2(128 * 1 + 10, 96 * 1 + 10), 110) { Alpha = 0.72f };
                        Line[] a = { la, lb };
                        foreach (Line l in a)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(1.2f), 0.72f);
                            DelayBeat(1.2f, () => { l.Dispose(); });
                        }
                    });
                    DelayBeat(0.75f * 19f / 14f, () =>
                    {
                        Line la = new(new Vector2(128 * 2 - 10, 96 * 2 - 10), 110) { Alpha = 0.72f };
                        Line lb = new(new Vector2(128 * 2 + 10, 96 * 2 + 10), 110) { Alpha = 0.72f };
                        Line[] a = { la, lb };
                        foreach (Line l in a)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(1.2f), 0.72f);
                            DelayBeat(1.2f, () => { l.Dispose(); });
                        }
                    });
                    DelayBeat(1.5f * 19f / 14f, () =>
                    {
                        Line la = new(new Vector2(128 * 3 - 10, 96 * 3 - 10), 110) { Alpha = 0.72f };
                        Line lb = new(new Vector2(128 * 3 + 10, 96 * 3 + 10), 110) { Alpha = 0.72f };
                        Line[] a = { la, lb };
                        foreach (Line l in a)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(1.2f), 0.72f);
                            DelayBeat(1.2f, () => { l.Dispose(); });
                        }
                    });
                    DelayBeat(2f * 19f / 14f, () =>
                    {
                        Line la = new(new Vector2(128 * 4 - 10, 96 * 4 - 10), 110) { Alpha = 0.72f };
                        Line lb = new(new Vector2(128 * 4 + 10, 96 * 4 + 10), 110) { Alpha = 0.72f };
                        Line[] a = { la, lb };
                        foreach (Line l in a)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(1.2f), 0.72f);
                            DelayBeat(1.2f, () => { l.Dispose(); });
                        }
                    });
                    DelayBeat(2.75f * 19f / 14f, () =>
                    {
                        Line la = new(new Vector2(128 * 3 - 20, 96 * 2 - 20), 70) { Alpha = 0.72f };
                        Line lb = new(new Vector2(128 * 3 + 20, 96 * 2 + 20), 70) { Alpha = 0.72f };
                        Line[] a = { la, lb };
                        foreach (Line l in a)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(1.2f), 0.72f);
                            DelayBeat(1.2f, () => { l.Dispose(); });
                        }
                    });
                    DelayBeat(3.5f * 19f / 14f, () =>
                    {
                        Line la = new(new Vector2(128 * 2 - 20, 96 * 4 - 20), 70) { Alpha = 0.72f };
                        Line lb = new(new Vector2(128 * 2 + 20, 96 * 4 + 20), 70) { Alpha = 0.72f };
                        Line[] a = { la, lb };
                        foreach (Line l in a)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(1.2f), 0.72f);
                            DelayBeat(1.2f, () => { l.Dispose(); });
                        }
                    });
                });
                RegisterFunctionOnce("lineGB", () =>
                {
                    float r1 = Rand(60, 120);
                    if (r1 <= 90)
                    {
                        float r = Rand(1, 4);
                        CentreEasing.EaseBuilder c1 = new();
                        CentreEasing.EaseBuilder c2 = new();
                        c1.Insert(BeatTime(2f * 19f / 14f), CentreEasing.EaseInCirc(new Vector2(128 * r - 10, 96 * r - 10), new Vector2(128 * r - 10 - 600, 96 * r - 10), BeatTime(2f * 19f / 14f)));
                        c2.Insert(BeatTime(2f * 19f / 14f), CentreEasing.EaseInCirc(new Vector2(128 * r + 10, 96 * r + 10), new Vector2(128 * r + 10 - 600, 96 * r + 10), BeatTime(2f * 19f / 14f)));
                        Line la = new(c1.GetResult(), (s) => { return r1; }) { Alpha = 0.72f };
                        Line lb = new(c2.GetResult(), (s) => { return r1; }) { Alpha = 0.72f };
                        Line[] a = { la, lb };
                        foreach (Line l in a)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2f * 19f / 14f), 0.72f);
                            for (int i = 0; i < 8; i++)
                            {
                                l.InsertRetention(new(3 * 19f / 14f * i, 0.8f - 0.8f / 4 * i));
                            }
                            DelayBeat(2f * 19f / 14f, () => { l.Dispose(); });
                        }
                    }
                    else if (r1 > 90)
                    {
                        float r = Rand(1, 4);
                        CentreEasing.EaseBuilder c1 = new();
                        CentreEasing.EaseBuilder c2 = new();
                        c1.Insert(BeatTime(2f * 19f / 14f), CentreEasing.EaseInCirc(new Vector2(128 * r - 10, 96 * r - 10), new Vector2(128 * r - 10 + 600, 96 * r - 10), BeatTime(2f * 19f / 14f)));
                        c2.Insert(BeatTime(2f * 19f / 14f), CentreEasing.EaseInCirc(new Vector2(128 * r + 10, 96 * r + 10), new Vector2(128 * r + 10 + 600, 96 * r + 10), BeatTime(2f * 19f / 14f)));
                        Line la = new(c1.GetResult(), (s) => { return r1; }) { Alpha = 0.72f };
                        Line lb = new(c2.GetResult(), (s) => { return r1; }) { Alpha = 0.72f };
                        Line[] a = { la, lb };
                        foreach (Line l in a)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2f * 19f / 14f), 0.72f);
                            for (int i = 0; i < 8; i++)
                            {
                                l.InsertRetention(new(3 * 19f / 14f * i, 0.8f - 0.8f / 4 * i));
                            }
                            DelayBeat(2f * 19f / 14f, () => { l.Dispose(); });
                        }
                    }
                });
                RegisterFunctionOnce("coFlicker", () =>
                {
                    float rd = Rand(0, 4);
                    if (rd == 0)
                    {
                        ScreenDrawing.MakeFlicker(new(new(157, 48, 118), 0.42f));
                    }
                    else if (rd == 1)
                    {
                        ScreenDrawing.MakeFlicker(new(new(182, 233, 252), 0.42f));
                    }
                    else if (rd == 2)
                    {
                        ScreenDrawing.MakeFlicker(new(new(252, 250, 157), 0.42f));
                    }
                    else if (rd == 3)
                    {
                        ScreenDrawing.MakeFlicker(new(new(104, 175, 164), 0.42f));
                    }
                    else if (rd == 4)
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.42f);
                    }
                });
                RegisterFunctionOnce("line1", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    ValueEasing.EaseBuilder v1 = new();
                    ValueEasing.EaseBuilder v2 = new();
                    c1.Insert(BeatTime(1.5f * 19f / 14f), CentreEasing.EaseOutCirc(new(220, 0), new(220, 320), BeatTime(1.5f * 19f / 14f)));
                    c2.Insert(BeatTime(1.5f * 19f / 14f), CentreEasing.EaseOutCirc(new(420, 0), new(420, 320), BeatTime(1.5f * 19f / 14f)));
                    c1.Insert(BeatTime(1.5f * 19f / 14f), CentreEasing.EaseInQuart(new(220, 320), new(220, 500), BeatTime(1.5f * 19f / 14f)));
                    c2.Insert(BeatTime(1.5f * 19f / 14f), CentreEasing.EaseInQuart(new(420, 320), new(420, 500), BeatTime(1.5f * 19f / 14f)));
                    v1.Insert(BeatTime(1.5f * 19f / 14f), ValueEasing.Linear(0, 30, BeatTime(1.5f * 19f / 14f)));
                    v1.Insert(BeatTime(1.5f * 19f / 14f), ValueEasing.Linear(30, 0, BeatTime(1.5f * 19f / 14f)));
                    v2.Insert(BeatTime(1.5f * 19f / 14f), ValueEasing.Linear(0, -30, BeatTime(1.5f * 19f / 14f)));
                    v2.Insert(BeatTime(1.5f * 19f / 14f), ValueEasing.Linear(-30, 0, BeatTime(1.5f * 19f / 14f)));
                    Line a = new(c1.GetResult(), v1.GetResult()) { Alpha = 0.85f };
                    Line b = new(c2.GetResult(), v2.GetResult()) { Alpha = 0.85f };
                    Line[] c = { a, b };
                    foreach (Line l in c)
                    {
                        CreateEntity(l);
                        for (int i = 0; i < 16; i++)
                        {
                            l.InsertRetention(new(3 * i, 0.85f - 0.85f / 16 * i));
                        }
                        DelayBeat(4f * 19f / 14f, () => { l.Dispose(); });
                    }
                });
                RegisterFunctionOnce("lineG2", () =>
                {
                    for (int i = 0; i < 32.5f * 4; i++)
                    {
                        DelayBeat(i * 0.25f * 19f / 14f, () =>
                        {
                            CentreEasing.EaseBuilder c = new();
                            ValueEasing.EaseBuilder v = new();
                            c.Insert(BeatTime(1.5f * 19f / 14f), CentreEasing.Linear(new(160, 420), new(160, 40), BeatTime(1.5f * 19f / 14f)));
                            v.Insert(BeatTime(1.5f * 19f / 14f), ValueEasing.Linear(60, 120, BeatTime(1.5f * 19f / 14f)));
                            Line l = new(c.GetResult(), v.GetResult()) { Alpha = 0.1f };
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            l.AlphaIncreaseAndDecrease(BeatTime(1.5f * 19f / 14f), 0.75f);
                            DelayBeat(1.5f * 19f / 14f, () => { l.Dispose(); });
                        });
                    }
                });
                BarrageCreate(BeatTime(4), BeatTime(19f / 14f), 5.7f, new string[]
                {
                    "(change)(arrow)","","","",    "/","","","",    "/","","","",    "/","","","",
                    "/","","","",    "/","","","",    "/","","","",    "/","","","",
                    //
                    "/","","","",    "/","","","",    "/","","","",    "/","","","",
                    "/","","","",    "/","","","",    "/","","","",    "/","","","",
                    //
                    "/","","","",    "/","","","",    "/","","","",    "/","","","",
                    "/","","","",    "/","","","",    "/","","","",    "/","","","",
                    ////
                    "/","","","",    "/","","","",    "/","","","",    "/","","","",
                    "/","","","",    "/","","","",    "/","","","",    "/","","","",
                    //
                    "(N21)(#7.5#$21)(lineLA)(lineGA)","","","",    "","","N21","",    "","","","",    "N21","","","",
                    "#1#N2","","","",    "","","","",    "","","","",    "N21","","","",
                    //
                    "(N21)(lineRA)","","","",    "","","N21","",    "","","","",    "N21","","","",
                    "#1#N2","","","",    "","","","",    "","","","",    "(N0)(#8#$0)(lineGB)","","","",
                    //
                    "N0(lineRA)(lineGB)","","","",    "","","N0","",    "","","","",    "N0","","","",
                    "#1#N01","","","",    "","","","",    "","","","",    "N0","","","",
                    ////
                    "N0(lineLA)","","","",    "","","N0","",    "","","","",    "N0","","","",
                    "#1#N01","","","",    "","","","",    "","","","",    "N0","","","",
                    //
                    "(R)(+21)(lineGB)(coFlicker)","","","",    "D(lineGB)","","+2","",    "+2","","","",    "(D)(+21)(coFlicker)","","","",
                    "(R)(#1#+21)(coFlicker)","","","",    "D","","","",    "D","","","",    "(D)(+21)(coFlicker)","","","",
                    //
                    "(R)(+21)(coFlicker)","","","",    "D1","","+21","",    "+21","","","",    "(D)(+21)(coFlicker)","","","",
                    "(R1)(#1#+2)(coFlicker)","","","",    "D1","","","",    "D1","","","",    "(D)(+21)(lineGB)(coFlicker)","","","",
                    //
                    "($01)(+21)(lineGB)(coFlicker)","","","",    "D1","","+21","",    "+21","","","",    "(R1)(+21)(coFlicker)","","","",
                    "(R1)(+21)(coFlicker)","","","",    "+21","","","",    "(D)(+21)(coFlicker)","","","",    "(D)(+21)(coFlicker)","","","",
                    ////
                    "(D)(+21)(coFlicker)","","","",    "R","","+2","",    "+2","","","",    "!!3","R(coFlicker)(line1)","D(coFlicker)","D(coFlicker)",
                    "D(coFlicker)","","D(coFlicker)","",    "D(coFlicker)","","#0.8#$2(coFlicker)","",    "(#2#D1)($2)(coFlicker)(lineG2)","","$2(coFlicker)","",    "$2(coFlicker)","","$2(coFlicker)","",
                    //
                });
            }
            void ExPart9()
            {
                RegisterFunctionOnce("lineG1", () =>
                {
                    for (int i = 0; i < 32; i++)
                    {
                        DelayBeat(0.5f * i * 19f / 14f, () =>
                        {
                            CentreEasing.EaseBuilder c1 = new();
                            CentreEasing.EaseBuilder c2 = new();
                            c1.Insert(BeatTime(5), CentreEasing.Linear(new(0, 0), new(640, 480), BeatTime(5)));
                            c2.Insert(BeatTime(5), CentreEasing.Linear(new(640, 0), new(0, 480), BeatTime(5)));
                            Line a = new(c1.GetResult(), (s) => { return -28; }) { Alpha = 0.3f };
                            Line b = new(c2.GetResult(), (s) => { return 28; }) { Alpha = 0.3f };
                            CreateEntity(a);
                            CreateEntity(b);
                            DelayBeat(5, () =>
                            {
                                a.Dispose(); b.Dispose();
                            });
                        });
                    }
                });
                BarrageCreate(BeatTime(4), BeatTime(19f / 14f), 5.7f, new string[]
                {
                    "(R1)(lineG1)","","","",    "R1","","","",    "#1.5#R","","","",    "R1","","","",
                    "R1","","","",    "","","#2#R","",    "","","","",    "R","","","",
                    //
                    "R","","","",    "R","","","",    "#1.5#R1","","","",    "R","","","",
                    "R","","","",    "","","#2#R1","",    "","","","",    "R1","","","",
                    //
                    "R1","","","",    "R1","","","",    "#1.5#R","","","",    "R1","","","",
                    "R1","","","",    "","","#2#R","",    "","","","",    "R","","","",
                    ////;
                });
            }
            void ExPart9P()
            {
                BarrageCreate(BeatTime(4), BeatTime(19f / 14f), 5.7f, new string[]
                {
                    "R","","","",    "R","","","",    "#1.5#R1","","","",    "R","","","",
                    "R","","","",    "","","#2#R1","",    "","","","",    "","","","",
                    //1
                });
            }
            void ExPart10()
            {
                RegisterFunctionOnce("coFlicker", () =>
                {
                    float rd = Rand(0, 4);
                    if (rd == 0)
                    {
                        ScreenDrawing.MakeFlicker(new(new(157, 48, 118), 0.42f));
                    }
                    else if (rd == 1)
                    {
                        ScreenDrawing.MakeFlicker(new(new(182, 233, 252), 0.42f));
                    }
                    else if (rd == 2)
                    {
                        ScreenDrawing.MakeFlicker(new(new(252, 250, 157), 0.42f));
                    }
                    else if (rd == 3)
                    {
                        ScreenDrawing.MakeFlicker(new(new(104, 175, 164), 0.42f));
                    }
                    else if (rd == 4)
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.42f);
                    }
                });
                RegisterFunctionOnce("lineG2R", () =>
                {
                    int rd = Rand(0, 7);
                    if (rd == 0)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(0.25f * i * 19f / 14f, () =>
                            {
                                CentreEasing.EaseBuilder c1 = new();
                                c1.Insert(BeatTime(3), CentreEasing.Linear(new(0, 0), new(640, 0), BeatTime(3)));
                                Line a = new(c1.GetResult(), (s) => { return -90; }) { Alpha = 0.4f };
                                CreateEntity(a);
                                DelayBeat(3, () =>
                                {
                                    a.Dispose();
                                });
                            });
                        }
                    }
                    if (rd == 1)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(0.25f * i * 19f / 14f, () =>
                            {
                                CentreEasing.EaseBuilder c1 = new();
                                c1.Insert(BeatTime(3), CentreEasing.Linear(new(0, 0), new(640, 640), BeatTime(3)));
                                Line a = new(c1.GetResult(), (s) => { return -45; }) { Alpha = 0.4f };
                                CreateEntity(a);
                                DelayBeat(3, () =>
                                {
                                    a.Dispose();
                                });
                            });
                        }
                    }
                    if (rd == 2)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(0.25f * i * 19f / 14f, () =>
                            {
                                CentreEasing.EaseBuilder c1 = new();
                                c1.Insert(BeatTime(3), CentreEasing.Linear(new(0, 0), new(0, 640), BeatTime(3)));
                                Line a = new(c1.GetResult(), (s) => { return 0; }) { Alpha = 0.4f };
                                CreateEntity(a);
                                DelayBeat(3, () =>
                                {
                                    a.Dispose();
                                });
                            });
                        }
                    }
                    if (rd == 3)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(0.25f * i * 19f / 14f, () =>
                            {
                                CentreEasing.EaseBuilder c1 = new();
                                c1.Insert(BeatTime(3), CentreEasing.Linear(new(640, 0), new(0, 640), BeatTime(3)));
                                Line a = new(c1.GetResult(), (s) => { return 45; }) { Alpha = 0.4f };
                                CreateEntity(a);
                                DelayBeat(3, () =>
                                {
                                    a.Dispose();
                                });
                            });
                        }
                    }
                    if (rd == 4)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(0.25f * i * 19f / 14f, () =>
                            {
                                CentreEasing.EaseBuilder c1 = new();
                                c1.Insert(BeatTime(3), CentreEasing.Linear(new(640, 0), new(0, 0), BeatTime(3)));
                                Line a = new(c1.GetResult(), (s) => { return 90; }) { Alpha = 0.4f };
                                CreateEntity(a);
                                DelayBeat(3, () =>
                                {
                                    a.Dispose();
                                });
                            });
                        }
                    }
                    if (rd == 5)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(0.25f * i * 19f / 14f, () =>
                            {
                                CentreEasing.EaseBuilder c1 = new();
                                c1.Insert(BeatTime(3), CentreEasing.Linear(new(640, 480), new(0, 480 - 640), BeatTime(3)));
                                Line a = new(c1.GetResult(), (s) => { return -45; }) { Alpha = 0.4f };
                                CreateEntity(a);
                                DelayBeat(3, () =>
                                {
                                    a.Dispose();
                                });
                            });
                        }
                    }
                    if (rd == 6)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(0.25f * i * 19f / 14f, () =>
                            {
                                CentreEasing.EaseBuilder c1 = new();
                                c1.Insert(BeatTime(3), CentreEasing.Linear(new(0, 480), new(0, 480 - 640), BeatTime(3)));
                                Line a = new(c1.GetResult(), (s) => { return 0; }) { Alpha = 0.4f };
                                CreateEntity(a);
                                DelayBeat(3, () =>
                                {
                                    a.Dispose();
                                });
                            });
                        }
                    }
                    if (rd == 7)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            DelayBeat(0.25f * i * 19f / 14f, () =>
                            {
                                CentreEasing.EaseBuilder c1 = new();
                                c1.Insert(BeatTime(3), CentreEasing.Linear(new(0, 480), new(640, 480 - 640), BeatTime(3)));
                                Line a = new(c1.GetResult(), (s) => { return 45; }) { Alpha = 0.4f };
                                CreateEntity(a);
                                DelayBeat(3, () =>
                                {
                                    a.Dispose();
                                });
                            });
                        }
                    }
                });
                RegisterFunctionOnce("end", () =>
                {
                    for (int i = 0; i < 32; i++)
                    {
                        DelayBeat(0.25f * i * 19f / 14f, () =>
                        {
                            ValueEasing.EaseBuilder v1 = new();
                            ValueEasing.EaseBuilder v2 = new();
                            v1.Insert(BeatTime(1 * 19f / 14f), ValueEasing.Linear(-90, 0, BeatTime(1 * 19f / 14f)));
                            v2.Insert(BeatTime(1 * 19f / 14f), ValueEasing.Linear(90, 0, BeatTime(1 * 19f / 14f)));
                            Line a = new((s) => { return new Vector2(0, 480); }, v1.GetResult()) { Alpha = 0.5f };
                            Line b = new((s) => { return new Vector2(640, 480); }, v2.GetResult()) { Alpha = 0.5f };
                            CreateEntity(a);
                            CreateEntity(b);
                            DelayBeat(2 * 19f / 14f, () =>
                            {
                                a.Dispose(); b.Dispose();
                            });
                        });
                    }
                    ValueEasing.EaseBuilder b = new();
                    b.Insert(BeatTime(8 * 19f / 14f), ValueEasing.Linear(0, -1800, BeatTime(8 * 19f / 14f)));
                    b.Run(s => CreateEntity(new DrawingUtil.SpecialBox(1, s, Heart) { alpha = 0.5f }));
                    Heart.controlingBox.GreenSoulAlpha = 0.01f;
                    game.DelayBeat(8 * 19f / 14f, () => { Heart.controlingBox.GreenSoulAlpha = 0.5f; });
                });
                RegisterFunctionOnce("last", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c3 = new();
                    c1.Insert(BeatTime(1.5f * 19f / 14f), CentreEasing.Linear(new(0, 240), new(650, 240), BeatTime(1.5f * 19f / 14f)));
                    c3.Insert(BeatTime(1.5f * 19f / 14f), CentreEasing.Linear(new(320, 0), new(320, 490), BeatTime(1.5f * 19f / 14f)));
                    Line a = new(c1.GetResult(), (s) => { return 90; }) { Alpha = 0.75f };
                    Line c = new(c3.GetResult(), (s) => { return 0; }) { Alpha = 0.75f };
                    CreateEntity(a);
                    a.TransverseMirror = true;
                    for (int i = 0; i < 24; i++)
                    {
                        a.InsertRetention(new(0.75f * i, 0.75f - 0.55f / 24f * i));
                    }
                    CreateEntity(c);
                    c.VerticalMirror = true;
                    for (int i = 0; i < 24; i++)
                    {
                        c.InsertRetention(new(0.75f * i, 0.75f - 0.55f / 24f * i));
                    }
                    DelayBeat(2f * 19f / 14f, () => { a.Dispose(); });
                    DelayBeat(2f * 19f / 14f, () => { c.Dispose(); });
                });
                BarrageCreate(BeatTime(4), BeatTime(19f / 14f), 5.7f, new string[]
                {
                    "R","","","",    "R","","","",    "#1.5#R1","","","",    "R","","","",
                    "R","","","",    "","","#1#R1","",    "","","","",    "R1","","","",
                    //
                    "!!3","D1(lineG2R)","+21","+21",    "R","","D","",    "D","","R1","",    "D1","","D1","",
                    "($31)(+01)(coFlicker)","","","",    "","","($21)(+2)(coFlicker)","",    "","","","",    "($2)(+21)(coFlicker)","","","",
                    //
                    "!!3","D(lineG2R)","+2","+2",    "R1","","D1","",    "D1","","R","",    "D","","D","",
                    "($3)(+0)(coFlicker)","","","",    "","","($2)(+21)(coFlicker)","",    "","","","",    "($21)(+2)(coFlicker)","","","",
                    //
                    "!!3","D(lineG2R)","+2","+2",    "R12","","+012","",    "D1","","R","",    "D","","D","",
                    "($31)(+0)(coFlicker)","","","",    "$2","","($3)(+01)(coFlicker)","",    "$01","","$21","",    "($3)(+01)(coFlicker)","","$0","",
                    ////
                    "!!3","D1(lineG2R)","+21","+21",    "R02","","+002","",    "D","","R1","",    "D1","","D1","",
                    "($31)(+0)(coFlicker)","","","",    "$21","","($3)(+01)(coFlicker)","",    "$0","","$2","",    "($3)(+01)(coFlicker)","","$01","",
                    //
                    "(#10#$0)(#10#$21)(end)","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","last",
                    //
                    ////end
                });
            }
            #endregion
            public void ExtremePlus()
            {
                #region turn a round
                Arrow[] ars3 = GetAll<Arrow>("Lm");
                for (int a = 0; a < ars3.Length; a++)
                {
                    int x = a;
                    ars3[x].Offset = new(-24, 0);
                }
                Arrow[] ars4 = GetAll<Arrow>("Li");
                for (int a = 0; a < ars4.Length; a++)
                {
                    int x = a;
                    ars4[x].Offset = new(-12, 0);
                }
                Arrow[] ars5 = GetAll<Arrow>("Ri");
                for (int a = 0; a < ars5.Length; a++)
                {
                    int x = a;
                    ars5[x].Offset = new(12, 0);
                }
                Arrow[] ars6 = GetAll<Arrow>("Rm");
                for (int a = 0; a < ars6.Length; a++)
                {
                    int x = a;
                    ars6[x].Offset = new(24, 0);
                }
                Arrow[] ars7 = GetAll<Arrow>("Lo");
                for (int a = 0; a < ars7.Length; a++)
                {
                    int x = a;
                    ars7[x].Offset = new(-32, 0);
                }
                Arrow[] ars8 = GetAll<Arrow>("Ro");
                for (int a = 0; a < ars8.Length; a++)
                {
                    int x = a;
                    ars8[x].Offset = new(32, 0);
                }
                #endregion
                float del1 = 4f * (190f / 185f + 19f / 18f + 190f / 175f + 19f / 17f + 41f);
                float times1 = 19f / 17f;
                float del2 = del1 + 4f * (29f * times1 + 19f / 16f + 19f / 15f + 19f / 14f);
                float times2 = 19f / 14f;
                if (InBeat(0)) ExPart1();
                if (InBeat(4 * 6)) ExPart2();
                if (InBeat(4 * 14)) ExPart3();
                if (InBeat(4 * 18)) ExPart3();
                if (InBeat(4 * 22)) ExPart4();
                if (InBeat(4 * 24)) ExPart5();
                if (InBeat(4 * 40)) ExTurn1();
                if (InBeat(del1)) ExPart6();
                if (InBeat(del1 + 4 * 14 * times1)) ExPart7();
                if (InBeat(del1 + 4 * 28 * times1)) ExTurn2();
                if (InBeat(del2)) ExPart8();
                if (InBeat(del2 + 4 * 12 * times2)) ExPart9();
                if (InBeat(del2 + 4 * 15 * times2)) ExPart9P();
                if (InBeat(del2 + 4 * 16 * times2)) ExPart9();
                if (InBeat(del2 + 4 * 19 * times2)) ExPart10();

            }
            public void Start()
            {
                game = this;
                StepSample = Shaders.StepSample;
                production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.51f);
                production2 = new ScreenDrawing.Shaders.Filter(Shaders.Polar, 0.8f);
                RenderProduction production3 = Blur = new ScreenDrawing.Shaders.Blur(0.8f);
                splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                Polar = Shaders.Polar;

                Polar.Intensity = 0f;
                splitter.Intensity = 0.0f;
                Blur.Sigma = 0f;
                StepSample.Intensity = 0.0f;
                StepSample.CentreX = 320f;
                StepSample.CentreY = 240f;
                ScreenDrawing.SceneRendering.InsertProduction(splitter);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(production2);

                //GametimeDelta = -4;
                //'PlayOffset = BeatTime(0);
                GametimeDelta = BeatTime(4 * 40) - 4;
                PlayOffset = BeatTime(4 * 40);
                //GametimeDelta = BeatTime(4f * (190f / 185f + 19f / 18f + 190f / 175f + 19f / 17f + 41f) + 4f * 28f * 19f / 17f) - 4;
                //PlayOffset = BeatTime(4f * (190f / 185f + 19f / 18f + 190f / 175f + 19f / 17f + 41f) + 4f * 28f * 19f / 17f);

                SetSoul(1);
                InstantTP(new(320, 240));
                SetGreenBox();
                HeartAttribute.MaxHP = 15.4321f;
                HeartAttribute.DamageTaken = 1;
                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.HPExistColor = Color.Lerp(Color.Red, Color.DarkRed, 0.4f);
            }
        }
    }
}