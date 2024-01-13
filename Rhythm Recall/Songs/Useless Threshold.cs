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
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        //--
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        ////48
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //--
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////64
                    });
                }
                if (InBeat(4 * 127))
                {
                    Arrow.UnitEasing eL = new();
                    Arrow.UnitEasing eR = new();
                    AddInstance(eL);
                    AddInstance(eR);
                    eL.ApplyTime = BeatTime(4);
                    eR.ApplyTime = BeatTime(4);
                    eL.PositionEase = Stable(BeatTime(4), new Vector2(-4, 0));
                    eR.PositionEase=Stable(BeatTime(4), new Vector2(4, 0));
                    eL.TagApply("L");
                    eR.TagApply("R");
                    CreateChart(BeatTime(4), BeatTime(1), 6.7f, new string[]
                    {
                        "$0","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "$2","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //--
                        "$3","","","",    "+0'1.2","","","",    "+0'1.2","","","",    "+0'1.2","","","",
                        "+0'1.5","","+0'1.5","",    "+0'1.5","","+0'1.5","",    "","","","",    "","","","",
                        //
                        "($3'1.6)(+0'1.6)","","","",    "","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        "($3'1.6)(+0'1.6)","","","",    "*+0'1.6@R","","","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        ////16
                        "+0'1.6","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","","","",
                        "($3'1.6)(+0'1.6)","","","",    "*+0'1.6","","","",    "*+0'1.6","","","",    "*+0'1.6","","","",
                        //
                        "*+0'1.6","","","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","","",
                        "($3'1.6)(+0'1.6)","","","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","","",
                        //--
                        "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        "(*+01'1.6@L)(+01'1.6)","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",
                        //
                        "($31'1.6)(+01'1.6)","","","",    "","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",    "*+01'1.6@L","","*+0'1.6@R","",
                        "($31'1.6)(+01'1.6)","","","",    "*+01'1.6@L","","","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        ////32
                        "+01'1.6","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","+01'1.6","+01'1.6","+01'1.6",    "+01'1.6","","","",
                        "($31'1.6)(+01'1.6)","","","",    "*+01'1.6","","","",    "*+01'1.6","","","",    "*+01'1.6","","","",
                        //
                        "*+01'1.6","","","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","","",
                        "($31'1.6)(+01'1.6)","","","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","","",
                        //--
                        "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",
                        "(*+0'1.6@R)(+0'1.6)","","*+01'1.6@L","",    "*+0'1.6@R","","*+01'1.6@L","",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",    "+0'1.6","+0'1.6","+0'1.6","+0'1.6",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////48
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //--
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////64
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
                    int beat = 4*127;
                    //int beat = 4 * 15;
                    GametimeDelta = BeatTime( - 4 + beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}
