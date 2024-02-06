using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.FightResources;
using System.Threading;
using System.Runtime.Intrinsics;

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
            private static Shader Reversal;
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
                float depth = 0.9f;
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
            public static void ColorReversal(float duration)
            {
                var kk = new ScreenDrawing.Shaders.Filter(Reversal, 0.5f);
                ScreenDrawing.SceneRendering.InsertProduction(kk);
                AddInstance(new InstantEvent(duration, () =>{ kk.Dispose(); }));
            }
            public void Easy()
            {
                
            }

            public void Extreme()
            {
                if (InBeat(0))
                {
                    RegisterFunctionOnce("Move", () =>
                    {
                        RunEase((s) =>
                        {
                            InstantSetBox(s, 84, 84);
                            InstantTP(s);
                        }, EaseOut(BeatTime(0.8f), new Vector2(320, 240), new Vector2(160, 240), EaseState.Sine), Stable(BeatTime(0.2f), new Vector2(160, 240)),
                        EaseOut(BeatTime(0.8f), new Vector2(160, 240), new Vector2(160, 120), EaseState.Sine), Stable(BeatTime(12.2f), new Vector2(160, 120)),
                        EaseOut(BeatTime(0.8f), new Vector2(160, 120), new Vector2(320, 120), EaseState.Sine), Stable(BeatTime(0.2f), new Vector2(320, 120)),
                        EaseOut(BeatTime(0.8f), new Vector2(320, 120), new Vector2(320, 240), EaseState.Sine), Stable(BeatTime(0.2f), new Vector2()),
                        EaseOut(BeatTime(0.8f), new Vector2(320, 240), new Vector2(480, 240), EaseState.Sine), Stable(BeatTime(0.2f), new Vector2()),
                        EaseOut(BeatTime(0.8f), new Vector2(480, 240), new Vector2(480, 360), EaseState.Sine), Stable(BeatTime(12.2f), new Vector2()),
                        EaseOut(BeatTime(0.8f), new Vector2(480, 360), new Vector2(320, 360), EaseState.Sine), Stable(BeatTime(0.2f), new Vector2()),
                        EaseOut(BeatTime(1.25f), new Vector2(320, 360), new Vector2(320, 240), EaseState.Linear), Stable(BeatTime(0), new Vector2()));
                    });
                    RegisterFunctionOnce("Turn", () =>
                    {
                        ScreenDrawing.ScreenAngle = Arguments[0] * 180;
                        DrawingUtil.BetterBlackScreen(0, 2, 0, Color.Black);
                    });
                    CreateChart(BeatTime(2), BeatTime(1), 6.5f, new string[]
                    {
                        "*$0'1.6(Move)","~_$0'1.6","~_$0'1.6","~_$0'1.6",    "~_$0'1.6","~_$0'1.6","~_$0'1.6","~_$0'1.6",    "($0'1.6)(*$11'1.6)","~_$11'1.6","~_$11'1.6","~_$11'1.6",    "~_$11'1.6","~_$11'1.6","~_$11'1.6","~_$11'1.6",
                        "($11'1.6)(*$0)","","","",    "*$0","","*$11","",    "*$0","","","",    "*$11","","","",
                        //
                        "*$0","","","",    "*$0","","","",    "*$11","","","",    "*$11","","","",
                        "*$1","","","",    "*$11","","","",    "*$0","","","",    "*$01","","","",
                        //
                        "","","","",    "*$1","","","",    "*$1","","~_$1","",    "~_$1","","~_$1","",
                        "($1)(*$01)","","","",    "","","","",    "(*$11)(*$0)","","","",    "(*$11)(*$0)","","","",
                        //
                        "","","","",    "(*$1)(*$01)","","","",    "(*$1)(*$01)","","","",    "","","","",
                        "*$0'1.6","~_$0'1.6","~_$0'1.6","~_$0'1.6",    "~_$0'1.6","~_$0'1.6","~_$0'1.6","~_$0'1.6",    "($0'1.6)(*$11'1.6)","~_$11'1.6","~_$11'1.6","~_$11'1.6",    "~_$11'1.6","~_$11'1.6","~_$11'1.6","_$11'1.6",
                        ////16
                        "*$2'1.6","~_$2'1.6","~_$2'1.6","~_$2'1.6",    "~_$2'1.6","~_$2'1.6","~_$2'1.6","~_$2'1.6",    "($2'1.6)(*$31'1.6)","~_$31'1.6","~_$31'1.6","~_$31'1.6",    "~_$31'1.6","~_$31'1.6","~_$31'1.6","~_$31'1.6",
                        "($31'1.6)(*$2)","","","",    "*$2","","*$31","",    "*$2","","","",    "*$31","","","",
                        //
                        "*$2","","","",    "*$2","","","",    "*$31","","","",    "*$31","","","",
                        "*$3","","","",    "*$31","","","",    "*$2","","","",    "*$21","","","",
                        //
                        "","","","",    "*$21","","","",    "*$21","","~_$21","",    "~_$21","","~_$21","",
                        "($21)(*$31)","","","",    "","","","",    "(*$21)(*$3)","","","",    "(*$21)(*$3)","","","",
                        //
                        "","","","",    "(*$2)(*$31)","","","",    "(*$2)(*$31)","","","",    "","","","",
                        "*$2'1.6","~_$2'1.6","~_$2'1.6","~_$2'1.6",    "~_$2'1.6","~_$2'1.6","~_$2'1.6","_$2'1.6",    "(*$31'1.6)","","(*$31'1.6)(<1>Turn)","",    "(*$31'1.6)(<0>Turn)","","(*$31'1.6)(<1>Turn)","",
                        ////32
                        "(*$31'1.6)($31'1.6)(<0>Turn)","","","",    "","","","",    "","","","",    "","","","",
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
                        RunEase((s) =>
                        {
                            CreateEntity(new Lyric(Color.White * s, 1f, new Vector2(160, 360), new Speech[]
                            {
                                new Speech("ダメダメそんなじんせい",0.1f),
                                new Speech(" ",0.1f),
                            }));
                        }, LinkEase(Stable(BeatTime(2), 1f), EaseIn(BeatTime(5), 1f, 0f, EaseState.Linear)));
                    });
                    DelayBeat(10, () =>
                    {
                        RunEase((s) =>
                        {
                            CreateEntity(new Lyric(Color.White * s, 1f, new Vector2(480, 120), new Speech[]
                            {
                                new Speech("みみなりがじゃまするんだ",0.1f),
                                new Speech(" ",0.1f),
                            }));
                        }, LinkEase(Stable(BeatTime(1.5f), 1f), EaseIn(BeatTime(5), 1f, 0f, EaseState.Linear)));
                    });
                    DelayBeat(17.5f, () =>
                    {
                        RunEase((s) =>
                        {
                            CreateEntity(new Lyric(Color.White * s, 1f, new Vector2(360, 400), new Speech[]
                            {
                                new Speech("ああ だれかしらそうやって",0.1f),
                                new Speech(" ",0.1f),
                            }));
                        }, LinkEase(Stable(BeatTime(2.5f), 1f), EaseIn(BeatTime(5), 1f, 0f, EaseState.Linear)));
                    });
                    DelayBeat(26, () =>
                    {
                        RunEase((s) =>
                        {
                            CreateEntity(new Lyric(new Color(0, 255, 255) * s, 1f, new Vector2(320, 240), new Speech[]
                            {
                                new Speech("しかってくれればよかったんだ",0.1f),
                                new Speech(" ",0.1f),
                            }));
                        }, LinkEase(Stable(BeatTime(2.5f), 1f), EaseIn(BeatTime(5), 1f, 0f, EaseState.Linear)));
                    });
                    RegisterFunctionOnce("StLine", () =>
                    {
                        Line l = new(new Vector2(Arguments[0], 240),
                            LinkEase(EaseOut(BeatTime(0.75f), 105, 90, EaseState.Quad), Stable(BeatTime(1.25f), 90)))
                        { Alpha = 0.4f, Tags = new string[] { "a" } };
                        CreateEntity(l);
                        DelayBeat(0.75f, () => { l.AlphaDecrease(BeatTime(1.25f), 0.4f); });
                    });
                    RegisterFunctionOnce("TrLine", () =>
                    {
                        Line l = new(new Vector2(320, Arguments[0]),
                            LinkEase(EaseOut(BeatTime(0.75f), -15, 0, EaseState.Quad), Stable(BeatTime(1.25f), 0)))
                        { Alpha = 0.4f, Tags = new string[] { "a" } };
                        CreateEntity(l);
                        DelayBeat(0.75f, () => { l.AlphaDecrease(BeatTime(1.25f), 0.4f); });
                    });
                    RegisterFunctionOnce("DiaLine", () =>
                    {
                        Line l1 = new(new Vector2(Arguments[0], 240),
                            LinkEase(EaseOut(BeatTime(0.75f), 45, 30, EaseState.Quad), Stable(BeatTime(1.25f), 30)))
                        { Alpha = 0.4f, Tags = new string[] { "a" } };
                        Line l2 = new(new Vector2(640 - Arguments[0], 240),
                            LinkEase(EaseOut(BeatTime(0.75f), 90 + 45, 90 +60, EaseState.Quad), Stable(BeatTime(1.25f), 90 + 60)))
                        { Alpha = 0.4f, Tags = new string[] { "a" } };
                        Line[]lines = { l1, l2 };
                        foreach (var l in lines)
                        {
                            CreateEntity(l);
                            DelayBeat(0.75f, () => { l.AlphaDecrease(BeatTime(1.25f), 0.4f); });
                        }
                    });
                    RegisterFunctionOnce("Turn", () =>
                    {
                        ColorReversal(BeatTime(8));
                        Line[] l = GetAll<Line>("a");
                        foreach(var line in l) { line.Dispose(); }
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        RunEase(s => Blur.Sigma = s, LinkEase(EaseOut(BeatTime(Arguments[1] / 2f), 0, Arguments[0], EaseState.Linear), EaseOut(BeatTime(Arguments[1] / 2f), Arguments[0], 0, EaseState.Linear)));
                    });
                    RegisterFunctionOnce("Out", () =>
                    {
                        CentreEasing.EaseBuilder e1 =new ();
                        e1.Insert(BeatTime(Arguments[0]),CentreEasing.EaseInQuad(new Vector2(320, 240), new Vector2(0, 240), BeatTime(4)));
                        e1.Insert(BeatTime(8), CentreEasing.Stable(new Vector2()));
                        CentreEasing.EaseBuilder e2 = new();
                        e2.Insert(BeatTime(Arguments[0]), CentreEasing.EaseInQuad(new Vector2(320, 240), new Vector2(640, 240), BeatTime(4)));
                        e2.Insert(BeatTime(8), CentreEasing.Stable(new Vector2()));
                        Line l1 = new(e1.GetResult(), Stable(1, 90)) { Alpha = 0.01f };
                        Line l2 = new(e2.GetResult(), Stable(1, 90)) { Alpha = 0.01f };
                        CreateEntity(l1, l2);
                        l1.AlphaIncrease(BeatTime(Arguments[0]), 0.6f * Arguments[0] / 4f);
                        l1.InsertRetention(new(2f, 0.5f));
                        l2.AlphaIncrease(BeatTime(Arguments[0]), 0.6f * Arguments[0] / 4f);
                        l2.InsertRetention(new(2f, 0.5f));
                        DelayBeat(Arguments[0] + 8f, () => { l1.Dispose(); l2.Dispose(); });
                    });
                    CreateChart(BeatTime(2), BeatTime(1), 7.5f, new string[]
                    {
                        "*$01(<40>StLine)","","","",    "(<120>StLine)","","","",    "*+01(<200>StLine)(<0.4,2.5>Blur)","","","",    "*+01(<280>StLine)","","","",
                        "(<360>StLine)","","","",    "*-11(<440>StLine)","","","",    "(<520>StLine)","","","",    "*-11(<600>StLine)(<2,4>Blur)","","","",
                        //
                        "(<40>StLine)","","","",    "*+21(<120>StLine)","","","",    "(#0.8#+01)(<200>StLine)","","","",    "(<280>StLine)","","","",
                        "*-11(<360>StLine)","","","",    "(<440>StLine)","","","",    "(<520>StLine)","","","",    "(<600>StLine)(<0.8,2>Blur)","","","",
                        //
                        "(#0.8#$2)(<450>TrLine)","","","",    "(<390>TrLine)","","","",    "*+0(<330>TrLine)","","","",    "(<270>TrLine)(<1,6>Blur)","","","",
                        "*-1(<210>TrLine)","","","",    "*+0(<150>TrLine)","","","",    "*-1(<90>TrLine)","","","",    "*+0(<30>TrLine)","","","",
                        //
                        "($301)($111)(<450>TrLine)","","","",    "(<390>TrLine)","","","",    "($001)($211)(<330>TrLine)","","","",    "(<270>TrLine)","","","",
                        "($101)($311)(<210>TrLine)","","","",    "(<150>TrLine)","","","",    "(<90>TrLine)","","","",    "*$21(<30>TrLine)(<2,9>Blur)","","","",
                        ////16
                        "(#1.3#$21)(<768>DiaLine)","","","",    "(<640>DiaLine)","","","",    "(<512>DiaLine)","","","",    "*+1(<384>DiaLine)","","","",
                        "*+11(<256>DiaLine)","","","",    "*+1(<128>DiaLine)","","","",    "*+11(<0>DiaLine)","","","",    "*+1(<-128>DiaLine)","","","",
                        //
                        "(#1.8#$01)(<768>DiaLine)","","","",    "(<640>DiaLine)(<3.5>Out)","","","",    "(#0.8#$2)(<512>DiaLine)(<3>Out)","","","",    "(<384>DiaLine)(<2.5>Out)","","","",
                        "(*$0)(*$21)(<256>DiaLine)(<2>Out)","","","",    "(<128>DiaLine)(<1.5>Out)","","","",    "(<0>DiaLine)(<1>Out)","","","",    "(<-128>DiaLine)(<0.5>Out)","","","",
                        //
                        "Turn","","","",    "(<0.4,1>Blur)","","","",    "","","","",    "","","","",
                        "","","","",    "","","","",    "(<0.4,1>Blur)","","","",    "","","","",
                        //
                        "","","","",    "","","","",    "(<0.6,1.5>Blur)","","","",    "","","","",
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
                Reversal = new Shader(Loader.Load<Effect>("Musics\\Jinsei Reset Button\\Color Reversal"));
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
                bool jump = false;
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
