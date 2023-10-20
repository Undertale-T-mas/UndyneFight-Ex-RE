using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.Remake.Entities;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using Extends;

namespace Rhythm_Recall.Waves
{
    public class Weekender : IChampionShip
    {
        public Weekender()
        {

            difficulties = new()
            {
                { "div.2", Difficulty.Normal },
                { "div.1", Difficulty.Extreme }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (123f / 60f)) { }
            public string Music => "Weekender";

            public string FightName => "Weekender";
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {

                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {

                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {

                        }
                    );
                public override string BarrageAuthor => "TK";
                public override string PaintAuthor => "";
                public override string SongAuthor => "Hey! Say! Jump";
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

            public void Extreme()
            {
                if (InBeat(230))
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "#999#R", "", "", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
            }

            public void Noob()
            {
                if (Gametime < 0) return;
            }
            public void Easy()
            {
                if (Gametime < 0) return;
            }
            public void Hard()
            {
                if (Gametime < 0) return;
            }
            public void Normal()
            {
                if (InBeat(3))
                {
                    RunEase(Y =>
                    {
                        InstantSetBox(Y, 84, 84);
                        InstantTP(320, Y);
                    },
                    EaseOut(BeatTime(3), -80, 240, EaseState.Back));
                }
                if (InBeat(2.25f))
                {
                    RegisterFunctionOnce("Zoom", () =>
                    {
                        DrawingUtil.PlusScreenScale(0.5f, BeatTime(1));
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        DrawingUtil.SetScreenScale(1, BeatTime(0.5f));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[] {
                        //Intro
                        "", "", "", "",     "R(Zoom)", "", "", "",
                        "", "", "", "",     "R(Zoom)", "", "", "",
                        "", "", "", "",     "R(Zoom)", "", "", "",
                        "", "", "", "",     "R(Zoom)", "", "", "",
                        "", "", "", "",     "R(Shrink)", "", "", "",
                        "R", "", "", "",     "R", "", "", "",
                        "R", "", "", "",     "R", "", "", "",
                        "R", "", "", "",     "R", "", "", "",
                        "R", "", "R", "",     "R", "", "R", "",
                        "R", "", "R", "",     "R", "", "R", "",
                        "R", "", "R", "",     "R", "", "R", "",
                        "R", "", "R", "",     "R", "",  "R(+01)", "",
                        "R(+01)", "", "R(+01)", "",     "", "", "", "",
                        "R", "", "", "",     "R", "", "", "",
                        "R", "", "R", "",     "", "", "", "",
                        //Repeat
                        "", "", "R", "",     "", "", "R", "",
                        "", "", "", "",     "R", "", "R", "",
                        "R", "", "R", "",     "R", "", "R", "",
                        "R", "", "R", "",     "R", "", "R", "",
                        "", "", "", "",     "", "", "", "",
                        "", "", "R", "",     "", "", "R", "",
                        "", "", "R", "",    "", "", "R", "",
                        "", "", "R", "",    "", "", "R", "",
                        "", "", "R", "",    "", "", "R", "",
                        "R", "", "R", "",   "R", "", "R", "",
                        "R", "", "R", "",   "R", "", "R", "",
                        "R", "", "R", "",   "R", "", "R", "",
                        "R", "", "R", "",   "R(+01)", "", "R(+01)", "",
                        "R(+01)", "", "", "",   "", "", "R", "",
                        "", "", "R", "",    "", "", "R", "",
                        "R", "", "", "",    "", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "", "", "R", "",    "R", "", "R", "",
                        "R", "", "R", "",   "R", "", "R", "",
                        "R", "", "R", "",   "", "", "R(+01)", "",
                    });
                }
                if (InBeat(38))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "R", "", "", "",
                        "R", "", "", "",        "", "", "", "",
                    });
                }
                if (InBeat(69))
                {
                    CreateChart(BeatTime(3.5f), BeatTime(1), 7, new string[] {
                        "R", "", "", "",   "R", "", "", "",
                        "#2#R", "", "", "",   "", "", "", "",
                        "", "", "", "",   "", "", "", "",
                        "", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "", "", "", "",
                        "R", "", "", "",   "", "", "", "",
                        "R", "", "", "",   "", "", "", "",
                        "", "", "", "",   "R", "", "R", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "", "", "", "",
                        "", "", "", "",     "", "",
                        "R1", "", "", "",   "R1", "", "", "",
                        "#2#R1", "", "", "",   "", "", "", "",
                        "", "", "", "",   "", "", "", "",
                        "", "", "", "",   "R1", "", "", "",
                        "R1", "", "", "",   "R1", "", "", "",
                        "R1", "", "", "",   "", "", "", "",
                        "R1", "", "", "",   "", "", "", "",
                        "R1", "", "", "",   "", "", "", "",
                        "", "", "", "",   "R1", "", "R1", "",
                        "R1", "", "", "",   "R1", "", "", "",
                        "R1", "", "", "",   "R1", "", "", "",
                        "", "", "", "",   "R1", "", "", "",
                        "R1", "", "", "",   "R1", "", "", "",
                        "", "", "", "",   "R1", "", "", "",
                        "R1", "", "", "",   "", "", "", "",
                    });
                }
                if (InBeat(101))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[] {
                        "R", "", "", "",   "", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R1", "", "", "",   "R1", "", "", "",
                        "R1", "", "", "",   "", "", "", "",
                        "R", "", "R", "",   "", "", "R", "",
                        "", "", "R", "",   "", "", "R", "",
                        "R", "", "", "",   "", "", "R", "",
                        "", "", "R", "",   "", "", "R", "",
                        "", "", "", "",   "", "", "", "",
                        "R", "", "", "",   "", "", "", "",
                        "(R)(+21)", "", "", "",   "(-1)(+21)", "", "", "",
                        "(-1)(+21)", "", "", "",   "(-1)(+21)", "",

                        "", "", "R", "",   "", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R", "", "", "",   "R", "", "", "",
                        "R1", "", "", "",   "R1", "", "", "",
                        "R1", "", "", "",   "", "", "", "",
                        "R", "", "R", "",   "", "", "R", "",
                        "", "", "R", "",   "", "", "R", "",
                        "R", "", "", "",   "", "", "R", "",
                        "", "", "R", "",   "", "", "R", "",
                        "", "", "", "",   "", "", "", "",
                        "", "", 
                        "R", "", "", "",   "R", "", "", "",
                    });
                }
                if (InBeat(131))
                {
                    RegisterFunctionOnce("Zoom", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(0.5f), ScreenDrawing.ScreenScale, ScreenDrawing.ScreenScale + 0.2f, EaseState.Quad));
                    });
                    RegisterFunctionOnce("OrgScale", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), ScreenDrawing.ScreenScale, 1, EaseState.Quad));
                    });
                    RegisterFunctionOnce("ZmABc", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            LinkEase(false,
                                EaseOut(BeatTime(0.75f), 1, 1.2f, EaseState.Quad),
                                EaseOut(BeatTime(0.75f), 1.2f, 1.4f, EaseState.Quad),
                                EaseOut(BeatTime(0.75f), 1.4f, 1.6f, EaseState.Quad),
                                EaseOut(BeatTime(0.5f), 1.6f, 1, EaseState.Quad)
                            ));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "R(Zoom)", "", "", "",    "R(Zoom)", "", "", "",
                        "R(Zoom)", "", "", "",    "(+0)(OrgScale)", "", "R", "",
                        "R", "", "R", "",         "R", "", "R", "",
                        "", "", "R", "",         "R", "", "R", "",
                        "R", "", "", "",         "R", "", "R", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "(R)(+01)(ZmABc)", "", "", "",
                        "", "", "(R)(+01)", "",         "", "", "", "",
                        "(R)(+01)", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "(R)(+01)(ZmABc)", "", "", "",
                        "", "", "(R)(+01)", "",         "", "", "", "",
                        "(R)(+01)", "", "", "",         "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "(R)(+01)", "", "", "",
                        "(R)(+01)", "", "", "",         "(R)(+01)", "", "", "",
                        "(R)(+01)", "", "", "",         "(R)(+01)", "", "(R)(+01)", "",
                        "", "", "(R)(+01)", "",         "", "", "", "",
                        "R", "", "R", "",         "(R)(+01)", "", "", "",
                        "(R)(+01)", "", "", "",         "(R)(+01)", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "(R)(+01)", "", "", "",         "(R)(+01)", "", "(R)(+01)", "",
                        "(R)(+01)", "", "", "",         "", "", "", "",
                    });
                }
                if (InBeat(156))
                {
                    RegisterFunctionOnce("Zoom", () =>
                    {
                        DrawingUtil.PlusScreenScale(0.5f, BeatTime(1));
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        DrawingUtil.SetScreenScale(1, BeatTime(0.5f));
                    });
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "", "",  "R", "",           "", "", "", "",
                        "", "",  "R", "",           "", "", "", "",
                        "", "",  "R", "",           "", "", "", "",
                        "", "",  "R", "",           "", "", "", "",
                        "", "",  "R", "",           "", "", "", "",
                        "", "",  "R", "",           "", "", "", "",
                        "", "",  "R", "",           "", "", "", "",
                        "", "",  "R", "",           "", "", "", "",

                        "", "",  "R(Zoom)", "",           "", "", "", "",
                        "", "",  "R(Zoom)", "",           "", "", "", "",
                        "", "",  "R(Zoom)", "",           "", "", "", "",
                        "", "",  "R(Zoom)", "",           "", "", "", "",
                        "", "",  "(R)(Shrink)", "",           "", "", "R", "",
                        "", "",  "R", "",           "", "", "", "",
                    });
                }
                if (InBeat(169))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "<1,1.5,4>Scale", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "(<1,360,3>Angle)(<1,1,3>Scale)", "", "+0", "",
                        "", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "#0.5#R", "", "", "",
                        "", "", "R", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "R", "",
                        "", "", "", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "R", "",

                        "", "", "", "",         "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "(<1,360,3>Angle)(<1,1,3>Scale)", "", "+01", "",
                        "", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "R1", "", "", "",
                        "R1", "", "", "",         "", "", "", "",
                        "R1", "", "", "",         "#0.5#R1", "", "", "",
                        "", "", "R1", "",         "", "", "R1", "",
                        "", "", "R1", "",         "", "", "R1", "",
                        "", "", "R1", "",         "", "", "R1", "",
                        "", "", "", "",         "", "", "R1", "",
                        "", "", "R1", "",         "", "", "R1", "",
                        "", "", "R1", "",         "", "", "R1", "",
                        "", "", "R1", "",         "", "", "R1", "",
                    });
                }
                if (InBeat(200))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "", "", "", "",     "R", "" ,"", "",
                        "R", "", "", "",     "#2#R", "" ,"", "",
                        "", "", "", "",     "", "" ,"", "",
                        "", "", "", "",     "", "" ,"", "",
                        "R", "", "", "",     "R", "" ,"", "",
                        "R", "", "", "",     "R", "" ,"", "",
                        "", "", "", "",     "R", "" ,"", "",
                        "", "", "", "",     "R", "" ,"", "",
                        "", "", "", "",     "", "" ,"", "",
                        "R", "", "+0", "",     "R", "" ,"", "",
                        "R", "", "", "",     "R", "" ,"", "",
                        "R", "", "", "",     "R", "" ,"", "",
                        "", "", "", "",     "R", "" ,"", "",
                        "R", "", "", "",     "", "" ,"", "R",
                        "", "", "", "R",     "", "" ,"", "",
                        "", "", "", "", "", "",
                        "", "", "", "",     "R1", "" ,"", "",
                        "R1", "", "", "",     "#2#R1", "" ,"", "",
                        "", "", "", "",     "", "" ,"", "",
                        "", "", "", "",     "", "" ,"", "",
                        "R1", "", "", "",     "R1", "" ,"", "",
                        "R1", "", "", "",     "R1", "" ,"", "",
                        "", "", "", "",     "R1", "" ,"", "",
                        "", "", "", "",     "R1", "" ,"", "",
                        "", "", "", "",     "", "" ,"", "",
                        "R1", "", "+01", "",     "R1", "" ,"", "",
                        "R1", "", "", "",     "R1", "" ,"", "",
                        "R1", "", "", "",     "R1", "" ,"", "",
                        "", "", "", "",     "R1", "" ,"", "",
                        "R1", "", "", "",     "", "" ,"", "R1",
                        "", "", "", "R1",     "", "" ,"", "",
                    });
                }
                if (InBeat(233))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "R", "", "", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
                }
            }
            public void ExtremePlus()
            {

            }
            public void Start()
            {
                #region Functions
                RegisterFunction("Scale", () =>
                {
                    RunEase(s => ScreenDrawing.ScreenScale = s,
                        EaseOut(BeatTime(Arguments[0]), ScreenDrawing.ScreenScale, Arguments[1], (EaseState)Arguments[2]));
                });
                RegisterFunction("Angle", () =>
                {
                    RunEase(s => ScreenDrawing.ScreenAngle = s,
                        EaseOut(BeatTime(Arguments[0]), ScreenDrawing.ScreenAngle, Arguments[1], (EaseState)Arguments[2]));
                });
                #endregion
                SetSoul(1);
                GametimeDelta = BeatTime(229);
                PlayOffset = 240 + GametimeDelta;
                if (GametimeDelta == 0)
                {
                    InstantSetBox(-80, 84, 84);
                    InstantTP(320, -80);
                }
                else
                {
                    SetGreenBox();
                    TP();
                }
            }
        }
    }
}