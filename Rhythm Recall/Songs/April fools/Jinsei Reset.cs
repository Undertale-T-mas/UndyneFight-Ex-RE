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
using static Rhythm_Recall.Waves.Determination.Game;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    internal class JinseiRE : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public JinseiRE()
        {
            dif.Add("Div.3", Difficulty.Easy);
            dif.Add("Div.2", Difficulty.Normal);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (200f / 60f)) { }
            public string Music => "Jinsei Reset Button";
            public string FightName => "Jinsei Reset Button";
            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "KEMU";
                public override string BarrageAuthor => "2.Kvon.0";
                public override string AttributeAuthor => "On.or.Kz";
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
            public class Lyric : Entity
            {
                WaveConstructor wave = new(62.5f / (200f / 60f));
                public Speech[] lyrics;
                public Color color;
                public float scale = 1f;
               float timer = 0;
                int lyriccount = 0;
                float depth = 0.5f;
                public Vector2 location;
                public Lyric(Color color, float scale, Vector2 location, Speech[] lyrics)
                {
                    this.lyrics = lyrics;
                    this.color = color;
                    this.scale = scale;
                    this.location = location;
                }
                public override void Draw()
                {
                    FightResources.Font.Japanese.CentreDraw(lyrics[lyriccount].Lyric, location, color, scale, depth);
                }
                float beatcount = 0;
                public override void Update()
                {
                    timer++;
                    if (lyriccount == lyrics.Length - 1) Dispose();
                    if (timer == 1)
                    {
                        for (int a = 0; a < lyrics.Length; a++)
                        {
                            beatcount += lyrics[a].BeatCount;
                            wave.DelayBeat(beatcount, () => { lyriccount += 1; });
                        }
                    }

                }
            }
            public class Speech
            {
                WaveConstructor wave = new(62.5f / (200f / 60f));
                public string Lyric = "";
                public float BeatCount = 0;
                public Speech(string lyric, float beatcount)
                {
                    Lyric = lyric;
                    BeatCount = beatcount;//*wave.BeatTime(beatcount); 
                }
            }
            public void Easy()
            {
                
            }

            public void Extreme()
            {
                if (InBeat(0))
                {
                    CreateChart(BeatTime(2), BeatTime(1), 6.5f, new string[]
                    {
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "d","","d","",    "d","","","",    "d","","","",
                        //
                        "d","","","",    "d","","","",    "d","","","",    "d","","","",
                        "d","","","",    "d","","","",    "d","","","",    "d","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////16
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
                        ////32
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
                        ////48
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
                        ////64
                    });
                }
                if (InBeat(224))
                {
                    DelayBeat(2, () =>
                    {
                        CreateEntity(new Lyric(Color.White, 1f, new Vector2(320, 400), new Speech[]
                        {
                        new Speech("ダメダメそんなじんせい",8),
                        new Speech("みみなりがじゃまするんだ",7.5f),
                        new Speech("ああ",2),
                        new Speech("だれかしらそうやって",6.5f),
                        new Speech("しか",1),
                        new Speech("って",0.5f),
                        new Speech("く",0.5f),
                        new Speech("れ",1),
                        new Speech("ば",0.5f),
                        new Speech("よ",0.5f),
                        new Speech("か",1),
                        new Speech("った",0.5f),
                        new Speech("ん",0.5f),
                        new Speech("だ",1.5f),
                        new Speech("-",0.1f),
                        }));
                    });
                    CreateChart(BeatTime(2), BeatTime(1), 7.5f, new string[]
                    {
                        "d","","","",    "","","","",    "d","","","",    "d","","","",
                        "","","","",    "d","","","",    "","","","",    "d","","","",
                        //
                        "","","","",    "d","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "d","","","",    "d","","","",    "d","","","",
                        //
                        "d","","","",    "d","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "d","","","",
                        ////16
                        "d","","","",    "","","","",    "","","","",    "d","","","",
                        "d","","","",    "d","","","",    "d","","","",    "d","","","",
                        //
                        "d","","","",    "","","","",    "d","","","",    "","","","",
                        "d","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "","","","",    "","","","",
                        ////32
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

                GametimeDelta = -0.01f;
                bool jump = true;
                if (jump)
                {
                    int beat = 224;
                    //int beat = 4 * 15;
                    GametimeDelta = BeatTime( - 4 + beat);
                    PlayOffset = BeatTime(beat - 4);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}
