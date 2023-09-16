using Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class AFleetingOath : IChampionShip
    {
        public AFleetingOath()
        {
            difficulties = new();
            //this.difficulties.Add("Pa5t Lv.&", Difficulty.Easy);
            //this.difficulties.Add("&rese^6 [u.1l", Difficulty.Normal);
            difficulties.Add("?", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (190f / 60f)) { }
            public string Music => "A Fleeting Oath";

            public string FightName => "A Fleeting Oath";
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            //new(Difficulty.Easy, 0),
                            //new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            //new(Difficulty.Easy, 0),
                            //new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            //new(Difficulty.Easy, 0),
                            //new(Difficulty.Normal, 0),
                            new(Difficulty.ExtremePlus, 0)
                    });
                public override string BarrageAuthor => "ParaDOXXX";
                public override string AttributeAuthor => "Broken zKronO";
                public override string PaintAuthor => "from TONIKAKUKAWAII";
                public override string SongAuthor => "Neko Hacker feat. Tsukasa";
            }
            public SongInformation Attributes => new ThisInformation();
            private bool notRegistered = true;
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production, production1, production2;
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
            void Part01()
            {
                DelayBeat(0, () =>
                {
                    DrawingUtil.BlackScreen(0, BeatTime(3.75f), BeatTime(1.25f));
                });
                RegisterFunctionOnce("LineA", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(1.5f), CentreEasing.EaseInQuad(new(0, 240), new(320, 240), BeatTime(1.5f)));
                    c2.Insert(BeatTime(1.5f), CentreEasing.EaseInQuad(new(320, 0), new(320, 240), BeatTime(1.5f)));
                    Line a = new(c1.GetResult(), (s) => { return 90; }) { Alpha = 0.01f };
                    Line b = new(c2.GetResult(), (s) => { return 0; }) { Alpha = 0.01f };
                    CreateEntity(a); CreateEntity(b);
                    a.AlphaIncrease(BeatTime(1.5f), 0.74f);
                    b.AlphaIncrease(BeatTime(1.5f), 0.74f);
                    a.TransverseMirror = true; b.VerticalMirror = true;
                    DelayBeat(1.5f, () => { a.Dispose(); b.Dispose(); });
                });
                RegisterFunctionOnce("LineB", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(0, 0), BeatTime(2)));
                    c2.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(320, 240), new(640, 480), BeatTime(2)));
                    Line a = new(c1.GetResult(), (s) => { return 135; }) { Alpha = 0.75f };
                    Line b = new(c2.GetResult(), (s) => { return 135; }) { Alpha = 0.75f };
                    Line[] line = { a, b };
                    foreach (Line l in line)
                    {
                        CreateEntity(l);
                        for (int i = 0; i < 4; i++)
                        {
                            l.InsertRetention(new(5 * i, 0.75f - 0.1f * i));
                        }
                        l.TransverseMirror = true;
                        DelayBeat(2, () => { l.Dispose(); });
                    }
                });
                RegisterFunctionOnce("LineC", () =>
                {
                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(0.75f), CentreEasing.EaseOutSine(new(320, 240), new(160, 240), BeatTime(0.75f)));
                    c2.Insert(BeatTime(0.75f), CentreEasing.EaseOutSine(new(320, 240), new(320, 80), BeatTime(0.75f)));
                    Line a = new(c1.GetResult(), (s) => { return 90; }) { Alpha = 0.55f };
                    Line b = new(c2.GetResult(), (s) => { return 0; }) { Alpha = 0.55f };
                    Line[] line = { a, b };
                    foreach (Line l in line)
                    {
                        CreateEntity(l);
                        l.ObliqueMirror = true;
                        DelayBeat(0.75f, () => { l.Dispose(); });
                    }

                });
                RegisterFunctionOnce("LineD1", () =>
                {
                    int r = Rand(80, 100);
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(1.75f), CentreEasing.EaseInCubic(new(-40, 240), new(400, 240), BeatTime(1.75f)));
                    Line a = new(c.GetResult(), (s) => { return r; }) { Alpha = 0.8f };
                    CreateEntity(a);
                    for (int i = 0; i < 8; i++)
                    {
                        a.InsertRetention(new(5 * i, 0.8f - 0.02f * i));
                    }
                    a.AlphaDecrease(BeatTime(1.75f), 0.8f);
                    DelayBeat(1.75f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("LineD2", () =>
                {
                    int r = Rand(80, 100);
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(1.75f), CentreEasing.EaseInCubic(new(680, 240), new(240, 240), BeatTime(1.75f)));
                    Line a = new(c.GetResult(), (s) => { return r; }) { Alpha = 0.8f };
                    CreateEntity(a);
                    for (int i = 0; i < 8; i++)
                    {
                        a.InsertRetention(new(5 * i, 0.8f - 0.02f * i));
                    }
                    a.AlphaDecrease(BeatTime(1.75f), 0.8f);
                    DelayBeat(1.75f, () => { a.Dispose(); });
                });
                RegisterFunctionOnce("Blur", () =>
                {
                    ValueEasing.EaseBuilder v = new();
                    v.Insert(BeatTime(4), ValueEasing.EaseInQuad(3.6f, 0f, BeatTime(4)));
                    DelayBeat(0, () =>
                    {
                        v.Run((s) => { Blur.Sigma = s; });
                    });
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
                    "Blur","","","",    "","","","",    "LineA","","LineA","",    "LineA","","LineA","",
                    "LineA","","LineA","",    "LineA","","LineA","",    "","","","",    "","","","",
                    //
                    "($21)(D)(LineB)(LineC)","","","",    "","","","",    "","","","",    "","","","",
                    "$01(LineD2)","","","",    "","","","",    "R","","","",    "","","","",
                    //
                    "$21(LineD1)","","","",    "","","","",    "R","","","",    "","","","",
                    "$01(LineD2)","","","",    "R","","","",    "","","","",    "","","","",
                    //
                    "($21)(D)(LineD1)","","","",    "","","","",    "","","","",    "","","","",
                    "$01(LineD2)","","","",    "","","","",    "R","","","",    "","","","",
                    //
                    "$21(LineD1)","","","",    "","","","",    "R","","","",    "","","","",
                    "$01(LineD2)","","","",    "R","","","",    "","","","",    "","","","",
                    //
                    "($21)(D)(LineD1)","","","",    "","","","",    "","","","",    "","","","",
                    "$01(LineD2)","","","",    "","","","",    "R","","","",    "","","","",
                    //
                    "$21(LineD1)","","","",    "","","","",    "R","","","",    "","","","",
                    "$01(LineD2)","","","",    "R","","","",    "","","","",    "","","","",
                    //
                    "($21)(D)(LineD1)","","","",    "","","","",    "","","","",    "","","","",
                    "$01(LineD2)","","","",    "","","","",    "R","","","",    "R","","","",
                    //
                    "$21(LineD1)","","","",    "","","","",    "N1","","","",    "R","","","",
                    "$0(LineD2)","","","",    "R","","","",    "R","","","",    "R","","","",
                    ////
                    "($2)(D1)(LineD1)","","","",    "","","","",    "","","","",    "","","","",
                    "$0(LineD2)","","","",    "","","","",    "R1","","","",    "","","","",
                    //
                    "$2(LineD1)","","","",    "","","","",    "R1","","","",    "","","","",
                    "$0(LineD2)","","","",    "R1","","","",    "","","","",    "","","","",
                    //
                    "($2)(D1)(LineD1)","","","",    "","","","",    "","","","",    "","","","",
                    "$0(LineD2)","","","",    "","","","",    "R1","","","",    "","","","",
                    //
                    "$2(LineD1)","","","",    "","","","",    "R1","","","",    "","","","",
                    "$0(LineD2)","","","",    "R1","","","",    "","","","",    "","","","",
                    //
                    "($2)(D1)(LineD1)","","","",    "","","","",    "","","","",    "","","","",
                    "$0(LineD2)","","","",    "","","","",    "R1","","","",    "","","","",
                    //
                    "$2(LineD1)","","","",    "","","","",    "R1","","","",    "","","","",
                    "$0(LineD2)","","","",    "R1","","","",    "","","","",    "","","","",
                    //
                    "($2)(D1'1.2)(LineD1)","","+01'1.2","",    "","","+11'1.2","",    "+012'1.2","","","",    "+112'1.2","","+11'1.2","",
                    "(+312'1.2)($0)(LineD2)","","","",    "+11'1.2","","","",    "+112'1.2","","+11'1.2","",    "+312'1.2","","","",
                    //
                    "($2)(D1'1.2)(LineD1)","","","",    "","","D1'1.2","",    "","","","",    "+11'1.2","","+012'1.2","",
                    "(+01'1.2)($0)(LineD2)","","","",    "$31'1.2","","+01'1.2","",    "+011'1.2","","","",    "+311'1.2","","+011'1.2","",
                    //
                });
            }
            void Part02()
            {
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
                    "(#0.9#$21)(#0.9#$0)","","","",    "","","","",    "+0","","","",    "","","","",
                    "R","","","",    "D","","","",    "(R)(+21)","","","",    "","","","",
                    //
                    "R","","","",    "D","","","",    "(R)(+21)","","","",    "","","","",
                    "R","","","",    "D","","","",    "(R)(+21)","","","",    "","","","",
                    //
                    "R","","","",    "D","","","",    "(R)(+21)","","","",    "","","","",
                    "R","","","",    "D","","","",    "(R)(+21)","","","",    "","","","",
                    //
                    "R","","","",    "D","","","",    "(R)(+21)","","","",    "","","","",
                    "R","","","",    "D","","","",    "(#0.75#$3)(#0.75#$11)","","","",    "","","","",
                    ////
                    "(#0.75#$3)(#0.75#$11)","","","",    "","","","",    "+01","","","",    "","","","",
                    "R1","","","",    "D1","","","",    "(R)(+21)","","","",    "","","","",
                    //
                    "R1","","","",    "D1","","","",    "(R)(+21)","","","",    "","","","",
                    "R1","","","",    "D1","","","",    "(R)(+21)","","","",    "","","","",
                    //
                    "R1","","","",    "D1","","","",    "(R)(+21)","","","",    "","","","",
                    "R1","","","",    "D1","","","",    "(R)(+21)","","","",    "(+1)(+21)","","","",
                    //
                    "","","","",    "","","","",    "(R)(+21)","","","",    "+2","","","",
                    "(D)(+21)","","","",    "+01","","","",    "(D)(+21)","","","",    "+2","","","",
                    ////
                    "(#0.9#$21)(#0.9#$0)","","","",    "","","","",    "(+0)(+21)","","","",    "D","","+1","",
                    "+3","","","",    "D1","","","",    "(+01)(+2)","","","",    "+0","","","",
                    //
                    "D","","+3","",    "+1","","","",    "(+0)(+21)","","","",    "+01","","","",
                    "D1","","","",    "D1","","","",    "(R)(+21)","","","",    "+01","","","",
                    //
                    "(#0.9#$21)(#0.9#$0)","","","",    "","","","",    "(+0)(+21)","","","",    "D","","+1","",
                    "+3","","","",    "D1","","","",    "(+01)(+2)","","","",    "+0","","","",
                    //
                    "D","","+3","",    "+1","","","",    "(+0)(+21)","","","",    "+01","","","",
                    "D1","","","",    "D1","","","",    "(#0.75#$3)(#0.75#$11)","","","",    "","","","",
                    ////
                    "(#0.9#$3)(#0.9#$11)","","","",    "","","","",    "(+01)(+2)","","","",    "D1","","+11","",
                    "+31","","","",    "D","","","",    "(+0)(+21)","","","",    "+01","","","",
                    //
                    "D1","","+31","",    "+11","","","",    "(+01)(+2)","","","",    "+0","","","",
                    "D","","","",    "D","","","",    "(R1)(+2)","","","",    "+0","","","",
                    //
                    "(#0.9#$21)(#0.9#$0)","","","",    "","","","",    "(+0)(+21)","","","",    "D","","+1","",
                    "#0.75#+31","","","",    "+21","","","",    "(+01)(+2)","","","",    "+0","","","",
                    //
                    "#0.75#R","","","",    "+2","","","",      "#0.75#+01","","","",    "+21","","","",
                    "(+0)(+21)","","","",    "(R)(+21)","","","",    "D11","","+311","",    "+111","","+311","",
                    ////
                    "(#0.9#$0)(#0.9#$31)","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "(#0.75#$1)(#0.75#$21)","","","",    "","","","",
                    //
                    "(#0.9#$1)(#0.9#$21)","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "(#0.9#$01)(#0.9#$3)","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "(#0.75#$11)(#0.75#$2)","","","",    "","","","",
                    //
                    "(#0.9#$11)(#0.9#$2)","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    ////
                });
            }
            void Part03()
            {
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
                    "(R)(+21)","","","",    "+01","","","",    "(R1)(+2)","","","",    "+0","","","",
                    "(R)(+21)","","","",    "","","","",    "(R1)(+2)","","","",    "+2","","","",
                    //
                    "(R)(+21)","","","",    "+21","","","",    "(R1)(+2)","","","",    "","","","",
                    "(R)(+21)","","","",    "+01","","","",    "(R1)(+2)","","","",    "+0","","","",
                    //
                    "(R)(+21)","","","",    "+21","","","",    "(R1)(+2)","","","",    "+2","","","",
                    "(R)(+21)","","","",    "","","","",    "(R1)(+2)","","","",    "+0","","","",
                    //
                    "(R)(+21)","","","",    "+01","","","",    "(R1)(+2)","","","",    "","","","",
                    "(R)(+21)","","","",    "+01","","","",    "D01","","+001","",    "+211","","+011","",
                    ////
                    "($21)(+01)","","","",    "","","","",    "(+21)(+01)","","","",    "","","","",
                    "($11)(+01)","","","",    "(+0)(+0)","","","",    "$001'1.2","","$201'1.2","",    "$001'1.2","","$201'1.2","",
                    //
                    "(D)(+21)","","","",    "(D)(+21)","","","",    "(D)(+21)","","","",    "(D)(+21)","","","",
                    "(D)(+21)","","","",    "(D)(+21)","","","",    "$211'1.2","","$011'1.2","",    "$211'1.2","","$011'1.2","",
                    //
                    "D1'1.2","","+11'1.2","",    "!!5","+31'1.2","(~_+01'1.6)(>+01'2.4)","(~_+01'1.6)(>+01'2.4)","(~_+01'1.6)(>+01'2.4)","(~_+01'1.6)(>+01'2.4)",
                    "D'1.2","","+3'1.2","",    "!!5","+1'1.2","(~_+0'1.6)(>+0'2.4)","(~_+0'1.6)(>+0'2.4)","(~_+0'1.6)(>+0'2.4)","(~_+0'1.6)(>+0'2.4)",
                    "(D'1.2)(+21'1.2)","","+2'1.2","",    "(D'1.2)(+21'1.2)","","+01'1.2","",
                    "(D'1.2)(+21'1.2)","","+2'1.2","",    "!!5","($3'1.2)($11'1.2)","(~_$11'1.6)(<$3'2.4)","(~_$3'1.6)(<$11'2.4)","(~_$11'1.6)(<$3'2.4)","(~_$3'1.6)(<$11'2.4)",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    ////
                });
            }
            void Part04()
            {
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                RegisterFunctionOnce("LineD2", () =>
                {

                });
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    ////
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    //
                    "","","","",    "","","","",    "","","","",    "","","","",
                    "","","","",    "","","","",    "","","","",    "","","","",
                    ////
                });
            }
            public void ExtremePlus()
            {

            }
            public void Start()
            {
                game = this;
                production = Blur = new Blur(0.505f);
                production1 = new Filter(Shaders.StepSample, 0.51f);
                splitter = new RGBSplitting(0.9f) { Disturbance = false };
                StepSample = Shaders.StepSample;
                Blur.Sigma = 0f;
                StepSample.Intensity = 0.0f;
                StepSample.CentreX = 320f;
                StepSample.CentreY = 240f;
                splitter.Intensity = 0.0f;
                ScreenDrawing.SceneRendering.InsertProduction(production);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(splitter);

                GametimeDelta = -1;
                PlayOffset = BeatTime(0);
                //GametimeDelta = -4 + BeatTime(41 * 4);
                //PlayOffset = BeatTime(41 * 4);

                SetSoul(1);
                InstantTP(new(320, 240));
                SetGreenBox();
                HeartAttribute.MaxHP = 16;
                ScreenDrawing.HPBar.HPLoseColor = Color.DarkRed;
                ScreenDrawing.HPBar.HPExistColor = Color.Blue * 0.7f;
                bool jump = false;
                if (jump)
                {
                    float beat = 4f * 40;
                    GametimeDelta = -3.5f + BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}