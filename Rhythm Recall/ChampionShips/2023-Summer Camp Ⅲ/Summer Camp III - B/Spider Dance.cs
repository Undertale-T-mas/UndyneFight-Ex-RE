using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.Remake.Entities;
using UndyneFight_Ex.Remake.Texts;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Remake.TextUtils;
using static UndyneFight_Ex.MathUtil;
using System.Net.Mail;

namespace Rhythm_Recall.Waves
{
    public class Spider_Dance : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Spider_Dance()
        {
            dif.Add("Div.2", Difficulty.Easy);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor,IWaveSet
        {
            public Project() : base(62.5f / (230f / 60f)) { }
            public string Music => "Spider Dance";

            public string FightName => "Spider Dance";

            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "Toby Fox";
                public override string BarrageAuthor => "zKronO";
                public override string AttributeAuthor => "ParaDOXXX";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Easy,7.0f),
                    new(Difficulty.Extreme,17.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,7.0f),
                    new(Difficulty.Extreme,17.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,11.0f),
                    new(Difficulty.Extreme,20.4f)
                    }
                    );
            }
            static Arrow.UnitEasing easeA, easeB, easeC, easeD;
            static Arrow.EnsembleEasing easeX, easeY;
            Blur Blur;
            RenderProduction production, production1, production2, production3, production4;
            GlobalResources.Effects.StepSampleShader StepSample;
            RGBSplitting splitter = new();
            #region disused
            public void ExtremePlus()
            {
                throw new NotImplementedException();
            }

            public void Normal()
            {
                throw new NotImplementedException();
            }
            public void Hard()
            {
                throw new NotImplementedException();
            }

            public void Noob()
            {
                throw new NotImplementedException();
            }
            #endregion
            public void Extreme()
            {
                if (InBeat(0))
                {
                    easeC.TagApply("C");
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            LinkEase(EaseOut(BeatTime(1), 2, 1.75f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.75f, 1.5f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.5f, 1.25f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.25f, 1, EaseState.Back)));
                    });
                    BarrageCreate(BeatTime(0), BeatTime(2), 6.2f, new string[]
                    { 
                        //1
                        "Shrink", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",//pre
                        "", "", "d", "",    "d", "", "", "",
                        "d", "", "+01", "",    "d", "", "+01", "",
                        //2
                        "", "", "d", "",    "d1", "", "+0", "",
                        "d1", "", "+0", "",    "d1", "", "+0", "",
                        "(d)(+0)", "", "(d)(+0)", "",    "(d)(+0)", "", "(d)(+0)", "",
                        "", "", "d", "",    "$2", "+11", "+1", "",
                        //3
                        "(d1)(+0)", "", "", "",    "", "", "", "",
                        "(-1)(+01)", "", "", "",    "", "", "", "",
                        "(-11)(+0)", "", "", "",    "", "", "", "",
                        "(-1)(+01)", "", "", "",    "", "", "", "",
                        //4
                        "*d'1.2", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
                        "*+11'1.2", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",    "*+1'1.2", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",
                        "*+1'1.2", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
                        "(*+1)(*+21)", "", "/", "",    "/", "", "/", "",
                    });
                }
                if (InBeat(24))
                {
                    easeA.TagApply("A");
                    easeB.TagApply("B");
                    easeD.TagApply("D");
                    RegisterFunctionOnce("ScPos", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenPositionDetla = new(s, 0),
                            LinkEase(EaseIn(BeatTime(3), 0, -20, EaseState.Sine),
                            EaseOut(BeatTime(4.5f), -20, 40, EaseState.Quad),
                            EaseIn(BeatTime(2.5f), 40, 0, EaseState.Sine)));
                    });

                    RegisterFunctionOnce("Kick", () =>
                    {
                        Line a = new(60, 90) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line b = new(90, 90) { Alpha = 0.4f, DrawingColor = Color.Purple * 0.9f };
                        Line c = new(640 - 60, 90) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line d = new(640 - 90, 90) { Alpha = 0.4f, DrawingColor = Color.Purple * 0.9f };
                        Line a1 = new(0, 40) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line b1 = new(0, -40) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line c1 = new(640, 40) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line d1 = new(640, -40) { Alpha = 0.7f, DrawingColor = Color.Purple * 0.9f };
                        Line[] lines = { a, b, c, d, a1, b1, c1, d1 };
                        foreach(Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(0.75f));
                            DelayBeat(0.8f, () => { l.Dispose(); });
                        }
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    { 
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "Kick", "",    "Kick", "", "Kick", "",
                        //1
                        "(d)(+21)", "", "+0", "",    "(d)(+21)", "", "+21", "",
                        "(d)(+21)", "", "+0", "",    "(d)(+21)", "", "+21", "",
                        "*$2'1.2(ScPos)", "", "(~_$2'1.2@A)(*N21)", "",    "(*+01)(~_$2'1.2@B)", "", "~_$2'1.2@A", "",
                        "(~_$2'1.2@B)(*N21)", "", "(*+01)(~_$2'1.2@A)", "",    "(~_$2'1.2@B)(*N21)", "", "(*+01)(~_$2'1.2@A)", "",
                        //2
                        "*$01'1.2", "", "(*N0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(*N0)", "", "(*+0)(~_$01'1.2@A)", "",
                        "(~_$01'1.2@B)(*N0)", "", "(*+0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(*N0)", "", "(*+0)(~_$01'1.2@A)", "",
                        "(~_$01'1.2@B)(*N0)", "", "(*+0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(*N0)", "", "(*+0)(~_$01'1.2@A)", "",
                        "($21)(+2)", "", "", "",    "$01", "+1", "+11", "",
                        //3
                        "(#1.75#d)(*+2)", "", "*+0", "",    "*+0", "", "", "",
                        "(#1.75#d1)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        "(#1.75#d)(*+2)", "", "*+0", "",    "*+0", "", "", "",
                        "(#1.75#d1)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        //4
                        "*d'1.2", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",    "*-11'1.2", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",
                        "*-11'1.2", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",    "*-1'1.2", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",
                        "*-1'1.2", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",    "*-11'1.2", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",
                        "(*-1)(*-21)", "", "/", "",    "/", "", "/", "",
                    });
                }
                if (InBeat(56))
                {
                    RegisterFunctionOnce("SetBox", () =>
                    {
                        SetSoul(Souls.RedSoul);
                        BoxUtils.Vertexify();
                        var box = BoxUtils.VertexBoxInstance;

                        //     320,160
                        //240,240    400,240
                        //     320,320

                        box.SetPosition(0, new Vector2(320, 240 - 80));
                        box.SetPosition(1, new Vector2(320, 240 - 80));
                        box.SetPosition(2, new Vector2(320+Sin(60)*80, 240+Cos(60)* 80));
                        box.SetPosition(3, new Vector2(320 - Sin(60) * 80, 240 + Cos(60) * 80)); 
                        BoxUtils.Rotate(new(320, 240), 180);
                        DelayBeat(0.5f, () => { });
                        ScreenDrawing.BoxBackColor = Color.Transparent;
                        DelayBeat(1, () =>
                        {
                            box.SetPosition(3, new Vector2(240, 240 - 80));
                            box.SetPosition(0, new Vector2(320 + 80, 240 - 80));
                            box.SetPosition(1, new Vector2(320 + 80, 240 + 80));
                            box.SetPosition(2, new Vector2(320 - 80, 240 + 80));
                        });
                    });
                    RegisterFunctionOnce("Purple", () =>
                    {
                        SetSoul(4);
                        BoxUtils.DeVertexify(new(240, 240 - 80, 160, 160));
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    { 
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "SetBox", "",    "", "", "", "",
                        //1
                        "", "", "", "",    "Purple", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //2
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //3
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //4
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //5
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //6
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //7
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //8
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(56+ 64))
                {
                    RegisterFunctionOnce("Green", () =>
                    {
                        SetBox(240, 84, 84);
                        SetSoul(1);
                        TP();
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "Green", "",    "", "", "", "",
                        //1
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "", "",    "", "", "", "",
                        //2
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "", "",    "", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "d", "",    "d", "", "d", "",
                        //3
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "", "",    "", "", "", "",
                        //4
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "", "",    "", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "d", "",    "d", "", "d", "",
                        //5
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "", "",    "", "", "", "",
                        //6
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "", "",    "", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "d", "",    "d", "", "d", "",
                        //7
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "d", "",    "", "", "d", "",
                        "", "", "", "",    "d", "", "d", "",
                        //8
                        "d", "", "d", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(184))
                {
                    RegisterFunctionOnce("Green", () =>
                    {
                        SetBox(240, 84, 84);
                        SetSoul(1);
                        TP();
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //1
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        //2
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        //3
                        "d", "", "d", "",    "d", "", "d", "",
                        "d", "", "d", "",    "d", "", "d", "",
                        "d", "", "d", "",    "d", "", "d", "d",
                        "d", "", "d", "",    "d", "", "d", "",
                        //4
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "d", "", "", "",    "", "", "", "",
                        //5
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "", "",    "", "", "", "",
                        //6
                        "d", "", "", "",    "d", "", "d", "",
                        "", "", "", "",    "", "", "", "",
                        "d", "", "", "",    "d", "", "d", "",
                        "d", "", "d", "",    "d", "", "d", "",
                        //7
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "d", "",    "", "", "d", "",
                        "d", "", "", "",    "d", "", "d", "",
                        //8
                        "d", "", "d", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "", "", "", "",
                    });
                }
            }
            public void Easy()
            {

            }
            public void Start()
            {
                Settings.GreenTap = true;
                AddInstance(easeA = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(4.25f),
                    RotationEase = LinkEase(EaseOut(BeatTime(3.75f), 60, 0, EaseState.Quad),
                    Stable(BeatTime(0.25f), 0))
                });
                AddInstance(easeB = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(4.25f),
                    RotationEase = LinkEase(EaseOut(BeatTime(3.75f), -60, 0, EaseState.Quad),
                    Stable(BeatTime(0.25f), 0))
                });
                AddInstance(easeC = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.5f),
                    RotationEase = LinkEase(EaseOut(BeatTime(2.5f), -90, 0, EaseState.Sine))
                });
                AddInstance(easeD = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.5f),
                    RotationEase = LinkEase(EaseOut(BeatTime(2.5f), 90, 0, EaseState.Sine))
                });
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
                GametimeDelta = -3.5f;
                SetSoul(1);
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(320, 240);
                ScreenDrawing.ScreenScale = 2;
                bool jump = true;
                if (jump)
                {
                    int beat = 118;
                    //   int beat = 326;
                    //   int beat = 198 ;
                    GametimeDelta = -3.5f + BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}