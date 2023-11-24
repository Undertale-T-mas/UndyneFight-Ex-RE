using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
                public override string AttributeAuthor => "Tlott.God.Inf _ Onor.Kz";
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
                    CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                    {
                        "d","","","",    "","","","",    "","","","",    "","","","",
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
                        ////16
                        "d","","","",    "","","","",    "","","","",    "","","","",
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
                        "d","","","",    "+01","","","",    "d","","","",    "+01","","","",
                        "d","","","",    "+01","","","",    "d","","","",    "+01","","","",
                        //
                        "($2)($2'1.6@L)","","~_$2'1.6@L'","",    "~_$2'1.6@L","","~_$2'1.6@L","",    "~_$2'1.6@L","","~_$2'1.6@L","",    "~_$2'1.6@L","","~_$2'1.6@L","",
                        "~_$2'1.6@L","","~_$2'1.6@L","",    "~_$2'1.6@L","","~_$2'1.6@L","",    "$2'1.6@L","","","",    "","","","",
                        //--
                        "d","","","",    "+01","","","",    "d","","","",    "+01","","","",
                        "d","","","",    "+01","","","",    "d","","","",    "","","","",
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
                bool jump = false;
                if (jump)
                {
                    int beat = 4*16;
                    //int beat = 4 * 15;
                    GametimeDelta = BeatTime( - 4 + beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}
