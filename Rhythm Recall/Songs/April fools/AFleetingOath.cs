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
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using Color = Microsoft.Xna.Framework.Color;

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
            public Game() : base(62.5f / (162f / 60f)) { }
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
                public override string BarrageAuthor => "Fleeting Toki";
                public override string AttributeAuthor => "zKronO's Oath";
                public override string PaintAuthor => "from TONIKAKUKAWAII";
                public override string SongAuthor => "Neko Hacker feat. Tsukasa";
            }
            public SongInformation Attributes => new ThisInformation();
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            private bool notRegistered = true;
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
            public void ExtremePlus()
            {
                if (InBeat(0f))
                {
                    CreateChart(BeatTime(1), BeatTime(1), 6.2f, new string[]
                    {
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        //
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
                        //
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
                        //
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
                        //
                    });
                }
            }
            public void Start()
            {
                game = this;
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

                GametimeDelta = BeatTime(-0.7f);
                PlayOffset = BeatTime(0);
                bool jump = false;
                if (jump)
                {
                    int beat = 0;
                    //int beat = 4 * 15;
                    GametimeDelta =BeatTime(-0.65f + beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}