using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.Remake;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class Traveler_at_Sunset : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Traveler_at_Sunset()
        {
            dif.Add("Div.2", Difficulty.Normal);
            dif.Add("Div.1", Difficulty.ExtremePlus);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor,IWaveSet
        {
            public Project() : base(62.5f/(226f/60f)) { }
            
            
            public string Music => "Traveler at Sunset";

            public string FightName => "Traveler at Sunset";

            public SongImformation Attributes => new Information();
            class Information : SongImformation
            {
                public Information() { this.MusicOptimized = true; }
                public override string SongAuthor => "SK_kent";
                public override string BarrageAuthor => "zKronO vs Tlottgodinf vs T-mas";
                public override string AttributeAuthor => "Woem feat. ParaDOXXX";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Normal,12.0f),
                    new(Difficulty.ExtremePlus,19.4f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Normal,12.0f),
                    new(Difficulty.ExtremePlus,19.4f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Normal,16.0f),
                    new(Difficulty.ExtremePlus,20.9f)
                    }
                    );
            }
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production, production1;
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            #region disused
            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Hard()
            {
                throw new NotImplementedException();
            }
            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }
            #endregion
            private class Winder : Entity
            {
                public float Intensity { set; get; } = 10;
                private float Speed { set; get; } = 40f;
                public float Length { set; get; } = 300f;
                public float BasicSpeed { set; get; } = 1f;
                public Color DrawingColor { set; get; } = Color.White;
                public bool Direction { set; get; } = false;
                public float Width { get; set; } = 1.5f;

                public Winder()
                {
                    UpdateIn120 = true;
                }
                public float timer = 0;
                public override void Draw()
                {
                    
                    
                }

                public override void Update()
                {
                    timer++;
                    if (timer % Intensity < 1)
                    {
                        Speed = 40f * BasicSpeed;
                        Length = 150f * (BasicSpeed + 1);
                        CreateEntity(new Wind(Speed * Rand(0.8f, 1.4f), Length, DrawingColor, Width, Direction));
                    }
                }
                class Wind : Entity
                {
                    float Speed;
                    float Width = 1.5f;
                    Vector2 point1;
                    Vector2 point2;
                    Color color;
                    public Wind(float Speed, float length, Color color, float width, bool dir = false)
                    {
                        this.Width = width;
                        this.color = color;
                        this.Speed = Speed;
                        if (dir)
                        {
                            this.Speed = -Speed;
                            point1 = new(-20, Rand(10, 470));
                            point2 = new(-20 - length, LastRand);
                        }
                        else
                        {
                            point1 = new(660, Rand(10, 470));
                            point2 = new(660 + length, LastRand);
                        }
                    }
                    float timer = 0;
                    public float Colordepth = Rand(0.300f, 0.500f);
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(point1,point2,Width, color * Colordepth,0.1f);
                    }

                    public override void Update()
                    {
                        timer++;
                        point1 += new Vector2(-Speed, 0);
                        point2 += new Vector2(-Speed, 0);
                        if (timer >= 900 / Speed + 30) this.Dispose();
                    }
                }
            }
            Winder r = new(), s = null;
            static Arrow.UnitEasing easeA = null, easeB = null, easeC = null, easeD, easeE, easeF, easeG;
            static Arrow.EnsembleEasing easeX = null, easeY = null, easeZ = null, easeU, easeV, easeW;
            public void ExtremePlus()
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(114), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { PlaySound = false });
                if (InBeat(0))
                {
                    RegisterFunction("FadeOut", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.MasterAlpha = s;
                        },
                        Stable(BeatTime(8),0),
                        EaseIn(BeatTime(64), 1, EaseState.Sine)
                        );
                        RunEase((q) =>
                        {
                            ScreenDrawing.ScreenScale = q;
                        },
                        Stable(BeatTime(8),5f),
                        EaseOut(BeatTime(72), -4f, EaseState.Sine)
                        );
                        RunEase((f) =>
                        {
                            r.BasicSpeed = f;
                        },
                        Stable(BeatTime(8), 0.1f),
                        EaseIn(BeatTime(78), 0.9f, EaseState.Quad));
                    });
                    RegisterFunctionOnce("GaussBlur", () =>
                    {
                        RunEase((s) =>
                        {
                            Blur.Sigma = s;
                        }, Stable(BeatTime(0), 2.2f),
                        EaseOut(BeatTime(16), -2.2f, EaseState.Quad));
                    });
                    RegisterFunctionOnce("Soul1", () =>
                    {
                        SetSoul(1);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {   //0
                        "FadeOut","","","",    "","","","",
                        "","","","",    "","","","",
                        //1
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //2
                        "GaussBlur(Soul1)","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","(Soul1)",    "","","","",
                        "","","","",    "","","","",
                        //3
                        "GaussBlur","","","",    "","","(Soul1)","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","(Soul1)","","",    "","","","",
                        //4
                        "GaussBlur","","","",    "","","","",
                        "","","","",    "(Soul1)","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //5
                        "GaussBlur(Soul1)","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","(Soul1)",    "","","","",
                        "","","","",    "","","","",
                        //6
                        "GaussBlur","","","",    "","","(Soul1)","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","(Soul1)","","",    "","","","",
                        //7
                        "GaussBlur","","","",    "","","","",
                        "","","","",    "(Soul1)","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //8
                        "GaussBlur(Soul1)","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","(Soul1)",    "","","","",
                        "","","","",    "","","","",
                        //9
                        "GaussBlur","","","",    "","","(Soul1)","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //注意每4个字符串为1beat
                    });
                }
                if (InBeat(72))
                {
                    RegisterFunctionOnce("LoadA", () =>
                    {
                        Line a = new(EaseOut(BeatTime(2), new Vector2(0, 0), new Vector2(320, 240), EaseState.Quad).Easing, Stable(BeatTime(2), -40).Easing) { Alpha = 0.7f };
                        Line b = new(EaseOut(BeatTime(2), new Vector2(640, 480), new Vector2(320, 240), EaseState.Quad).Easing, Stable(BeatTime(2), -40).Easing) { Alpha = 0.7f };
                        Line[]line = { a, b };
                        foreach(Line l in line)
                        {
                            CreateEntity(l);
                            for (int i = 0; i < 6; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.7f - i * 0.07f));
                            }
                            DelayBeat(4f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LoadB", () =>
                    {
                        Line a = new(EaseOut(BeatTime(2), new Vector2(640, 0), new Vector2(320, 240), EaseState.Quad).Easing, Stable(BeatTime(2), 40).Easing) { Alpha = 0.7f };
                        Line b = new(EaseOut(BeatTime(2), new Vector2(0, 480), new Vector2(320, 240), EaseState.Quad).Easing, Stable(BeatTime(2), 40).Easing) { Alpha = 0.7f };
                        Line[] line = { a, b };
                        foreach (Line l in line)
                        {
                            CreateEntity(l);
                            for (int i = 0; i < 6; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.7f - i * 0.07f));
                            }
                            DelayBeat(4f, () => { l.Dispose(); });
                        }
                        DelayBeat(2, () =>
                        {
                            float k = -20f;
                            AddInstance(new TimeRangedEvent(20f, () =>
                            {
                                Line c = new(new Vector2(320 + (-20 + k) * (-20 + k) * 0.0625f - 100, 240), 130) { Alpha = 0.75f };
                                Line d = new(new Vector2(320 - (-20 + k) * (-20 + k) * 0.0625f + 100, 240), 130) { Alpha = 0.75f };
                                CreateEntity(c);
                                CreateEntity(d);
                                AddInstance(new TimeRangedEvent(10, () =>
                                {
                                    c.Alpha -= 0.075f;
                                    d.Alpha -= 0.075f;
                                }));
                                k++;
                                DelayBeat(3, () => { c.Dispose(); d.Dispose(); });
                            }));
                        });//你要不要看看你写了什么史
                    });
                    RegisterFunctionOnce("KickA", () =>
                    {
                        Line a=new(LinkEase(EaseOut(BeatTime(0.8f), new Vector2(0, 0), new Vector2(120, 90), EaseState.Quart),
                            Stable(BeatTime(0.2f),new Vector2(320,240))).Easing, 
                            Stable(BeatTime(1), -37.5f).Easing)
                        { Alpha = 0.75f };
                        Line b = new(LinkEase(EaseOut(BeatTime(0.8f), new Vector2(0, 480), new Vector2(120, 480 - 90), EaseState.Quart),
                            Stable(BeatTime(0.2f), new Vector2(160, 120))).Easing,
                            Stable(BeatTime(1), 37.5f).Easing)
                        { Alpha = 0.75f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.75f - i * 0.05f));
                            }
                            DelayBeat(0, () => { l.AlphaDecrease(BeatTime(1), 0.75f); });
                            DelayBeat(1, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("KickB1", () => 
                    {
                        Line a = new(LinkEase(Stable(BeatTime(1), new Vector2(160, 240)),
                            EaseIn(BeatTime(3), new Vector2(160, 240), new Vector2(580, 240), EaseState.Quart),
                            Stable(BeatTime(4), new Vector2(580, 240))).Easing, Stable(BeatTime(8), 90).Easing)
                        { Alpha = 0.75f };
                        Line b = new(EaseOut(BeatTime(1.5f), new Vector2(160, 240), new Vector2(160 + 60, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line c = new(EaseOut(BeatTime(1.5f), new Vector2(160, 240), new Vector2(160 - 60, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line[] linesA = { b, c };
                        foreach (Line lA in linesA)
                        {
                            CreateEntity(lA);
                            lA.AlphaDecrease(BeatTime(1.5f), 0.55f);
                            DelayBeat(1.5f, () => { lA.Dispose(); });
                        }
                        CreateEntity(a);
                        DelayBeat(5, () => { a.AlphaDecrease(BeatTime(1), 0.75f); });//Group1
                        Line d = new(LinkEase(EaseOut(BeatTime(1), new Vector2(580, 240), new Vector2(500, 240), EaseState.Quart),
                            Stable(BeatTime(1), new Vector2(500, 240))).Easing,
                            Stable(BeatTime(2), 90).Easing)
                        { Alpha = 0.55f };
                        Line e = new(Stable(BeatTime(2),new Vector2(580,420)).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f),90,70,EaseState.Quad),
                            Stable(BeatTime(0.5f),70)).Easing)
                        { Alpha = 0.75f };
                        Line f = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(580, 60), new Vector2(480 - 50, 60), EaseState.Quart),
                            Stable(BeatTime(0.5f), new Vector2(480 - 50, 60))).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 120, EaseState.Quad),
                            Stable(BeatTime(0.5f), 120)).Easing)
                        { Alpha = 0.55f };
                        Line g = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(580, 420), new Vector2(380 - 100, 420), EaseState.Quart),
                            Stable(BeatTime(0.5f), new Vector2(380 - 100, 420))).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 50, EaseState.Quad),
                            Stable(BeatTime(0.5f), 50)).Easing)
                        { Alpha = 0.35f };
                        Line[] linesB = { d, e, f, g };
                        foreach(Line lB in linesB)
                        {
                            DelayBeat(4, () => { CreateEntity(lB); });
                            DelayBeat(5, () => { lB.AlphaDecrease(BeatTime(1)); });
                            DelayBeat(6, () => { lB.Dispose(); });
                        }
                        DelayBeat(6, () => { a.Dispose(); });
                    });//你要不要看看你写了什么史啊？？
                    RegisterFunctionOnce("KickB2", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(1), new Vector2(480, 240)),
                            EaseIn(BeatTime(3), new Vector2(480, 240), new Vector2(60, 240), EaseState.Quart),
                            Stable(BeatTime(4), new Vector2(60, 240))).Easing, Stable(BeatTime(8), 90).Easing)
                        { Alpha = 0.75f };
                        Line b = new(EaseOut(BeatTime(1.5f), new Vector2(480, 240), new Vector2(480 + 60, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line c = new(EaseOut(BeatTime(1.5f), new Vector2(480, 240), new Vector2(480 - 60, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line[] linesA = { b, c };
                        foreach (Line lA in linesA)
                        {
                            CreateEntity(lA);
                            lA.AlphaDecrease(BeatTime(1.5f), 0.55f);
                            DelayBeat(1.5f, () => { lA.Dispose(); });
                        }
                        CreateEntity(a);
                        DelayBeat(5, () => { a.AlphaDecrease(BeatTime(1), 0.75f); });
                        Line a1 = new(LinkEase(Stable(BeatTime(0), new Vector2(480 - 240, 240)),
                            EaseIn(BeatTime(2), new Vector2(480 - 240, 240), new Vector2(60, 240), EaseState.Quart)).Easing, 
                            Stable(BeatTime(2), 90).Easing)
                        { Alpha = 0.75f };
                        Line b1 = new(EaseOut(BeatTime(1.5f), new Vector2(480 - 240, 240), new Vector2(480 + 60 - 240, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line c1 = new(EaseOut(BeatTime(1.5f), new Vector2(480 - 240, 240), new Vector2(480 - 60 - 240, 240), EaseState.Sine).Easing,
                            Stable(BeatTime(1.5f), 90).Easing)
                        { Alpha = 0.55f };
                        Line[] linesA1 = { b1, c1 };
                        foreach (Line lA1 in linesA1)
                        {
                            DelayBeat(2, () =>
                            {
                                CreateEntity(lA1);
                                lA1.AlphaDecrease(BeatTime(1.5f), 0.55f);
                                DelayBeat(1.5f, () => { lA1.Dispose(); });
                            });
                        }
                        DelayBeat(2, () =>
                        {
                            CreateEntity(a1);
                            DelayBeat(2, () => { a1.Dispose(); });
                        });//Group1
                        Line d = new(LinkEase(EaseOut(BeatTime(1), new Vector2(60, 240), new Vector2(140, 240), EaseState.Quart),
                            Stable(BeatTime(1), new Vector2(140, 240))).Easing,
                            Stable(BeatTime(2), 90).Easing)
                        { Alpha = 0.55f };
                        Line e = new(Stable(BeatTime(2), new Vector2(60, 60)).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 70, EaseState.Quad),
                            Stable(BeatTime(0.5f), 70)).Easing)
                        { Alpha = 0.75f };
                        Line f = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(60, 420), new Vector2(160 + 50, 420), EaseState.Quart),
                            Stable(BeatTime(0.5f), new Vector2(160 + 50, 420))).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 120, EaseState.Quad),
                            Stable(BeatTime(0.5f), 120)).Easing)
                        { Alpha = 0.55f };
                        Line g = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(60, 60), new Vector2(260 + 100, 60), EaseState.Quart),
                            Stable(BeatTime(0.5f), new Vector2(260 + 100, 60))).Easing,
                            LinkEase(EaseOut(BeatTime(1.5f), 90, 50, EaseState.Quad),
                            Stable(BeatTime(0.5f), 50)).Easing)
                        { Alpha = 0.35f };
                        Line[] linesB = { d, e, f, g };
                        foreach (Line lB in linesB)
                        {
                            DelayBeat(4, () => { CreateEntity(lB); });
                            DelayBeat(5, () => { lB.AlphaDecrease(BeatTime(1)); });
                            DelayBeat(6, () => { lB.Dispose(); });
                        }
                        DelayBeat(6, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("Step", () => 
                    {
                        RunEase((s) =>
                        {
                            StepSample.Intensity = s;
                            splitter.Intensity = s * 15;
                        },
                        Stable(0, 0.15f),
                        EaseOut(BeatTime(2), 0.15f, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("SplitterR", () =>
                    {
                        RunEase((s) =>
                        {
                            splitter.Intensity = s;
                        },
                        Stable(0, 9),
                        EaseOut(BeatTime(3), 9, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("SplitterL", () =>
                    {
                        RunEase((s) =>
                        {
                            splitter.Intensity = -s;
                        },
                        Stable(0, 9),
                        EaseOut(BeatTime(3), 9, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("Bound", () => 
                    {
                        ScreenDrawing.BoundColor = Color.White * 0.6f;
                        RunEase((s) => { ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = s; },
                            EaseOut(BeatTime(2), 0, 160, EaseState.Quad),
                            SineWave(15, BeatTime(10), 3, 0),
                            Stable(0, 160));
                    });
                    RegisterFunctionOnce("ConvL", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            Stable(0, 2.5f), EaseOut(BeatTime(3.5f), 2.5f, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("ConvR", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenAngle = -s,
                            Stable(0, 2.5f), EaseOut(BeatTime(3.5f), 2.5f, 0, EaseState.Quad));
                    });
                    RegisterFunctionOnce("SoulL", () =>
                    {
                        RunEase(s => Heart.Rotation = s * 2,
                            EaseOut(BeatTime(0.5f), 0, 1.2f, EaseState.Quart),
                            EaseOut(BeatTime(3.25f), 1.2f, 0, EaseState.Linear),
                            Stable(0, 0));
                    });
                    RegisterFunctionOnce("SoulR", () =>
                    {
                        RunEase(s => Heart.Rotation = -s * 2,
                            EaseOut(BeatTime(0.5f), 0, 1.2f, EaseState.Quart),
                            EaseOut(BeatTime(3.25f), 1.2f, 0, EaseState.Linear),
                            Stable(0, 0));
                    });
                    RegisterFunctionOnce("a", () => { });
                    RegisterFunctionOnce("winder", () => { 
                        r.Intensity = 6.0f;
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {   //10
                        "$0(LoadA)","+1","+1","+1",    "+1","+1","+1","+1",
                        "+11(LoadB)","+11","+11","+11",   "+11","+11","+11","+11",
                        //11
                        "(*$0'2)(*$2'2)(KickA)","","","",    "","","","",
                        "(d)(+01)","","","",    "","","","",
                        "(d)(+01)","","","",    "","","","",
                        "d","","","",    "d","","","",
                        //12
                        "d","","","",    "","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        //13
                        "(d)(+21)","","","",    "","","","",
                        "d","","","",    "d","","","",
                        "d","","","",    "d","","","",
                        "d","","","",    "d","","","",
                        //14
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "","","","",
                        //15
                        "(Step)(KickB1)(Bound)(d)(+0)(+21)(winder)","","","",    "","","","",
                        "(d)(+0)","","","",    "","","","",
                        "(KickA)(ConvR)(SplitterR)(d)(+0)(+01)","","","",    "","","","",
                        "d","","","",    "d","","","",
                        //16
                        "(Step)(KickB2)(d)(+21)(+01)","","","",    "","","","",
                        "(Step)(d1)(+01)","","","",    "d1","","","",
                        "(KickA)(ConvL)(SplitterL)(d)(+01)(+01)","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        //17
                        "(Step)(KickB1)(d)(+21)","","","",    "","","","",
                        "","","","",    "d","","","",
                        "(KickA)(ConvR)(SplitterR)(d)(+0)(+01)","","","",    "d","","","",
                        "d","","","",    "d","","","",
                        //18
                        "(Step)(KickB2)(#7.75#$0)(*+2)(SoulR)","","","",    "","","","",
                        "(Step)","","","",    "","","","",
                        "(KickA)(ConvL)(SplitterL)(#3.75#$21)(*+21)(SoulL)","","","",    "","","","",
                        "","","","",    "","","","",
                    });
                }
                if (InBeat(72 + 64))

                {
                    RegisterFunctionOnce("UaD", () =>
                    {
                        RunEase((s) =>
                        {
                            InstantSetBox(s, 84, 84);
                            InstantTP(new Vector2(320, s));
                        }, EaseIn(BeatTime(3), 240, -86, EaseState.Quad),
                        EaseOut(BeatTime(5), -86, 240, EaseState.Back),
                        Stable(0, 240));
                    });
                    RegisterFunctionOnce("DownLine", () =>
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            Line a = new(LinkEase(Stable(0, new Vector2(0, 0)),
                                EaseOut(BeatTime(2), new Vector2(320, 0), new Vector2(320, i * 40 + i * 12), EaseState.Quad),
                                Stable(BeatTime(2), new Vector2(320, i * 40 + i * 12))).Easing, Stable(0, 0).Easing)
                            { Alpha = 0.75f - i * 0.12f };
                            CreateEntity(a);
                            DelayBeat(2, () =>
                            {
                                a.AlphaDecrease(BeatTime(2));
                            });
                            DelayBeat(4, () => { a.Dispose(); });
                        }
                    });
                    AddInstance(easeA = new Arrow.UnitEasing()
                    {
                        ApplyTime = BeatTime(1.6f),
                        RotationEase = SimplifiedEasing.EaseOut(BeatTime(1.5f), 0, -45, SimplifiedEasing.EaseState.Sine) 
                        
                    }); 
                    RegisterFunctionOnce("pre", () =>
                    {
                        s = new();
                        s.BasicSpeed = 2.4f;
                        s.Width = 3.5f;
                        s.DrawingColor = Color.Lerp(Color.Aqua, Color.White, 0.7f);
                        s.Direction = true;
                        GameStates.InstanceCreate(s);
                        easeA.TagApply("A");
                    });
                    RegisterFunctionOnce("Wave", () =>
                    {

                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "", "", "", "(UaD)",    "", "", "", "",    
                        "", "", "", "",    "(pre)", "", "", "",    
                        //1
                        "(DownLine)", "", "", "",    "", "", "", "",    
                        "", "", "", "",    "", "", "", "",    
                        "", "", "", "",    "", "", "", "",    
                        "", "", "", "",    "", "", "", "",    
                        //2
                        "", "", "", "",    "", "", "", "",    
                        "", "", "", "",    "", "", "", "",    
                        "", "", "", "",    "", "", "", "",    
                        "", "", "", "",    "", "", "", "",     
                        //3
                        "d", "", "", "",    "d", "", "d", "",    
                        "", "", "d1", "",    "d1", "", "", "",    
                        "d", "", "d", "",    "", "", "d1", "",    
                        "d1", "", "d1", "",    "d", "", "d", "",    
                        //4
                        "d", "", "", "",    "d", "", "d", "",    
                        "", "", "d1", "",    "d1", "", "", "",    
                        "d", "", "d", "",    "d1", "", "d1", "",    
                        "d1(d)", "", "d", "",    "d", "", "d", "",    
                        //5
                        "d(d1)", "", "", "",    "d", "", "", "",
                        "d(d1)", "", "", "",    "d", "", "", "",
                        "d(d1)", "", "", "",    "d", "", "", "",
                        "d(d1)", "", "", "",    "d", "", "", "",
                        //6
                        "d(d1)", "", "", "",    "d1", "", "", "",
                        "d(d1)", "", "", "",    "d", "", "", "",
                        "d(d1)", "", "", "",    "d1", "", "", "",
                        "d(d1)", "", "", "",    "d", "", "", "",
                        //7
                        "(d1)(d)", "", "+1", "",    "+1", "", "+1", "",
                        "-1", "", "-1", "",    "-1", "", "-1", "",
                        "(d)(n01)", "", "+11", "",    "+11", "", "+11", "",
                        "-11", "", "-11", "",    "-11", "", "-11", "",
                        //8 
                        "-11", "+0", "-11", "+0",    "-11", "+0", "-11", "+0", 
                        "*$312@A", "*$302@A", "*$312@A", "*$302@A",    "*$112@A", "*$102@A", "*$112@A", "*$102@A",
                        "(*^$00'1.8)(*^$20'1.8)", "", "", "",    "", "", "$10", "",
                        "$30", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(72 + 128))
                {  
                    RegisterFunctionOnce("pre", () =>
                    {
                        BoxUtils.Vertexify();
                        var box = BoxUtils.VertexBoxInstance;

                        //     320,200
                        //240,280    400,280
                        //     320,360
                        box.SetPosition(box.Split(3, 0.5f), new(400, 280));
                        box.SetPosition(box.Split(2, 0.5f), new(320, 200));
                        box.SetPosition(box.Split(1, 0.5f), new(240, 280));
                        box.SetPosition(box.Split(0, 0.5f), new(320, 360));

                        box.SetPosition(0, new Vector2(360, 320));
                        box.SetPosition(2, new Vector2(280, 320));
                        box.SetPosition(4, new Vector2(280, 240));
                        box.SetPosition(6, new Vector2(360, 240));
                        
                        BoxUtils.Move(new(0, -40));
                        ScreenDrawing.BoxBackColor = Color.Transparent;

                        SetSoul(Souls.RedSoul);
                    });
                    RegisterFunctionOnce("Green", () =>
                    {
                        BoxUtils.DeVertexify(new(320 - 42, 240 - 42, 84, 84));
                        SetSoul(1);
                        TP();
                    });
                    
                    BarrageCreate(BeatTime(4), BeatTime(2), 7.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",    
                        "", "", "", "",    "(pre)", "", "", "",    
                        //1
                        "", "", "", "",    "", "", "", "",    
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
                        "Green", "", "", "",    "", "", "", "",      
                        //5
                        "(d1)(+00)", "", "", "",    "(d1)(+00)", "", "d1", "",
                        "d1", "", "d1", "",    "(d1)(+00)", "", "d1", "",
                        "(d1)(+00)", "", "d1", "",    "(d1)(+00)", "", "d1", "",
                        "(d1)(+00)", "", "d1", "",    "(d1)(+00)", "", "d1", "",    
                        //6
                        "d(d1)", "", "", "",    "d(d1)", "", "d", "",    
                        "d", "", "d", "",    "d(d1)", "", "", "",
                        "d(d1)", "", "d", "",    "d(d1)", "", "d", "",
                        "d(d1)", "", "d", "",    "d(d1)", "", "+0", "",      
                        //7
                        "*^$01'1.6(*^$21'1.6)", "", "", "",    "d(d1)", "", "d", "",    
                        "d", "", "d", "",    "d(d1)", "", "d", "",    
                        "d", "", "d", "",    "d(d1)", "", "d", "",    
                        "d", "", "d", "",    "d(d1)", "", "n3", "",    
                        //8
                        "$3(n31)", "", "$0", "$1",    "$2", "", "$3", "",    
                        "$31(d)", "", "$21", "$11",    "$01", "", "$31", "",    
                        "(D)(+01)", "", "(D)(+01)", "",    "(D)(+01)", "", "(D)(+01)", "",
                        "(D)(+01)", "", "", "",    "", "", "", "",      
                    });
                }
                if(InBeat(72 + 192))
                {
                    easeA.TagApply("B");
                    easeB.TagApply("A");
                    easeX.RevolutionEase(Stable(2, -45));
                    RegisterFunctionOnce("pre", () => {
                        easeX.TagApply("X");
                        Arrow[] all = GetAll<Arrow>();
                        foreach(Arrow arr in  all)
                        {
                            arr.LateWaitingScale = 0.3f;
                        }
                    });
                    RegisterFunctionOnce("changeEase", () => { 
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "(pre)", "", "", "",    
                        //1
                        "$21", "$01", "$20", "$00",     "$21", "$01", "$20", "$00",   
                        "*$212@A", "", "*$002@A", "*$212@A",    "*$002@B", "", "*$212@B", "",
                        "*$002@B", "", "*$302@A", "*$312@A",    "*$302@B", "", "*$002@A", "*$012@A",
                        "*$002@B", "", "*$102@A", "*$112@A",    "*$102@A", "", "", "",     
                        //2
                        "$31", "", "$11", "",    "$10", "$20", "$11", "$01",
                        "$0", "$1", "$2", "",    "$31", "", "$11", "",
                        "$31", "", "$0", "$1",    "$2", "", "$11", "",
                        "$31", "", "$0", "$1",    "$2", "", "$11", "",      
                        //3
                        "*$212@X'1.2(*$012@X'1.2)", "", "$20", "$00",     "$21", "$01", "$20", "$00",
                        "*$012@A", "", "*$202@A", "*$012@A",    "*$202@B", "", "*$012@B", "",
                        "*$202@B", "", "*$302@A", "*$312@A",    "*$302@B", "", "*$002@A", "*$012@A",
                        "*$002@B", "", "*$102@A", "*$112@A",    "*$102@A", "", "", "",     
                        //4      
                        "$20", "", "$00", "$01",    "$10", "$11", "$20", "$21",
                        "$30", "$31", "$00", "$01",    "$10", "$11", "$20", "$21",
                        "$30", "", "$31", "$00",    "$01", "", "$01", "",
                        "(*$00'1.2)(*$20'1.2)", "", "(*$01'1.2)(*$21'1.2)", "",    "(*$00'1.2)(*$20'1.2)", "", "(*$01'1.2)(*$21'1.2)", "",   
                        //5
                        "", "", "", "",  
                        "$1", "$2", "$1", "",    "$31", "", "$1", "$0",
                        "$1", "", "$11", "",    "$31", "", "$1", "$2",
                        "$1", "", "$11", "$01",    "$11", "", "$1", "$0",
                        "$1", "", "", "",    
                        //6
                        "$31", "", "$11", "",    "$2", "$1", "$01", "$11",
                        "$2", "$1", "$0", "",    "$31", "", "$11", "",
                        "$31", "", "$2", "$1",    "$0", "", "$3", "",
                        "$1", "", "$21", "$11(changeEase)",    "$01", "", "$3", "",      
                        //7
                        "*$212@X'1.2(*$012@X'1.2)", "", "$1", "",    "$01", "$11", "$2", "$1",
                        "$0", "", "$21", "$11",    "$01", "", "$3", "",
                        "$31", "", "$0", "$1",    "$2", "", "$21", "$11",
                        "$01", "", "$0", "$1",    "$2", "", "", "",    
                        //8
                        "*$212@A'1.2(*$012@A'1.2)", "", "*$202@B'1.2(*$002@B'1.2)", "*$212@A'1.2(*$012@A'1.2)",    "*$202@B'1.2(*$002@B'1.2)", "", "*$212@A'1.2(*$012@A'1.2)", "",
                        "*$202@B'1.2(*$002@B'1.2)", "", "*$212@A'1.2(*$012@A'1.2)", "*$202@B'1.2(*$002@B'1.2)",    "*$212@A'1.2(*$012@A'1.2)", "", "(*$202@B'1.2)(*$002@B'1.2) ", "",
                        "(*$212@A'1.2)(*$012@A'1.2) ", "", "*$302@A", "*$312@B",    "*$302@A", "*$312@B", "*$302@A", "",
                        "", "", "", "",    "", "", "", "",   
                    });
                }
                if (InBeat(328))
                {
                    DelayBeat(6.5f, () => {
                        easeA.Dispose(); easeA = new(); AddInstance(easeA);
                        easeB.Dispose(); easeB = new(); AddInstance(easeB);
                        easeX.RevolutionEase(Stable(1, 13));
                        easeY.RevolutionEase(Stable(1, 26));
                        easeZ.RevolutionEase(Stable(1, 39));
                        easeX.TagApply("X");
                        easeY.TagApply("Y");
                        easeZ.TagApply("Z");
                        easeU.RevolutionEase(Stable(1, 52));
                        easeV.RevolutionEase(Stable(1, 65));
                        easeW.RevolutionEase(Stable(1, 78));
                        easeU.TagApply("U");
                        easeV.TagApply("V");
                        easeW.TagApply("W");

                        float selfRota = 30f;
                        easeX.SelfRotationEase(Stable(1, selfRota));
                        easeY.SelfRotationEase(Stable(1, selfRota));
                        easeZ.SelfRotationEase(Stable(1, selfRota));
                        easeU.SelfRotationEase(Stable(1, selfRota));
                        easeV.SelfRotationEase(Stable(1, selfRota));
                        easeW.SelfRotationEase(Stable(1, selfRota));

                        float time = BeatTime(1.5f);
                        easeA.ApplyTime = time;
                        easeB.ApplyTime = time;
                        easeC.ApplyTime = time;
                        easeD.ApplyTime = time;
                        easeE.ApplyTime = time;
                        easeF.ApplyTime = time;
                        easeA.RotationEase = Linear(3f, -13);
                        easeB.RotationEase = Linear(4f, -26);
                        easeC.RotationEase = Linear(5f, -39);
                        easeD.RotationEase = Linear(6f, -52);
                        easeE.RotationEase = Linear(7f, -65);
                        easeF.RotationEase = Linear(8f, -78);
                        easeA.SelfRotation = -selfRota;
                        easeB.SelfRotation = -selfRota;
                        easeC.SelfRotation = -selfRota;
                        easeD.SelfRotation = -selfRota;
                        easeE.SelfRotation = -selfRota;
                        easeF.SelfRotation = -selfRota;

                        easeA.TagApply("A");
                        easeB.TagApply("B");
                        easeC.TagApply("C");
                        easeD.TagApply("D");
                        easeE.TagApply("E");
                        easeF.TagApply("F");
                    });

                    (CurrentScene as SongFightingScene).Accuracy.SpecifyTime = 0.71f;

                    RegisterFunctionOnce("pre", () => {
                        Heart.RotateTo(-45); 
                    });

                    // Generate the Snakes
                    BarrageCreate(BeatTime(4 + 2), BeatTime(1), 8, new string[] {  
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "", 

                        //1
                        "$0", "~_!$0", "~_!$0", "~_!$0",    "~_!$0", "~_!$0", "~_!$0", "!!24/6",  
                        "~_!$0@X", "~_!$0@Y", "~_!$0@Z", "~_!$0@U", "~_!$0@V", "~_$0@W",

                        "$1", "~_!$1", "~_!$1", "~_!$1",    "~_!$1", "~_!$1", "~_!$1", "!!24/6",
                        "~_!$1@A", "~_!$1@B", "~_!$1@C", "~_!$1@D", "~_!$1@E", "~_$1@F",

                        "$0", "~_!$0", "~_!$0", "~_!$0",    "~_!$0", "~_!$0", "~_!$0", "!!24/6",
                        "~_!$0@X", "~_!$0@Y", "~_!$0@Z", "~_!$0@U", "~_!$0@V", "~_$0@W",

                        "$1", "~_!$1", "~_!$1", "~_!$1",    "~_!$1", "~_!$1", "~_!$1", "!!24/6",
                        "~_!$1@A", "~_!$1@B", "~_!$1@C", "~_!$1@D", "~_!$1@E", "~_$1@F",

                        "$0", "~_!$0", "~_!$0", "~_!$0",    "~_!$0", "~_!$0", "~_!$0", "!!24/6",
                        "~_!$0@X", "~_!$0@Y", "~_!$0@Z", "~_!$0@U", "~_!$0@V", "~_$0@W",

                        "$1", "~_!$1", "~_!$1", "~_!$1",    "~_!$1", "~_!$1", "~_!$1", "!!24/6",
                        "~_!$1@A", "~_!$1@B", "~_!$1@C", "~_!$1@D", "~_!$1@E", "~_$1@F",

                        "$0", "~_!$0", "~_!$0", "~_!$0",    "~_!$0", "~_!$0", "~_!$0", "!!24/6",
                        "~_!$0@X", "~_!$0@Y", "~_!$0@Z", "~_!$0@U", "~_!$0@V", "~_$0@W",
                       
                        "$1", "~_!$1", "~_!$1", "!!24/6",
                        "~_!$1@X", "~_!$1@Y", "~_!$1@Z", "~_!$1@U", "~_!$1@V", "~_$1@W", 
                        "$2", "~_!$2", "~_!$2", "",

                        //2
                        "$21", "~_!$21", "~_!$21", "~_!$21",    "~_!$21", "~_!$21", "~_!$21", "!!24/6",
                        "~_!$21@X", "~_!$21@Y", "~_!$21@Z", "~_!$21@U", "~_!$21@V", "~_$21@W",

                        "$31", "~_!$31", "~_!$31", "~_!$31",    "~_!$31", "~_!$31", "~_!$31", "!!24/6",
                        "~_!$31@A", "~_!$31@B", "~_!$31@C", "~_!$31@D", "~_!$31@E", "~_$31@F",

                        "$21", "~_!$21", "~_!$21", "~_!$21",    "~_!$21", "~_!$21", "~_!$21", "!!24/6",
                        "~_!$21@X", "~_!$21@Y", "~_!$21@Z", "~_!$21@U", "~_!$21@V", "~_$21@W",

                        "$31", "~_!$31", "~_!$31", "~_!$31",    "~_!$31", "~_!$31", "~_!$31", "!!24/6",
                        "~_!$31@A", "~_!$31@B", "~_!$31@C", "~_!$31@D", "~_!$31@E", "~_$31@F",

                        "$21", "~_!$21", "~_!$21", "~_!$21",    "~_!$21", "~_!$21", "~_!$21", "!!24/6",
                        "~_!$21@X", "~_!$21@Y", "~_!$21@Z", "~_!$21@U", "~_!$21@V", "~_$21@W",

                        "$31", "~_!$31", "~_!$31", "~_!$31",    "~_!$31", "~_!$31", "~_!$31", "!!24/6",
                        "~_!$31@A", "~_!$31@B", "~_!$31@C", "~_!$31@D", "~_!$31@E", "~_$31@F",

                        "$21", "~_!$21", "~_!$21", "~_!$21",    "~_!$21", "~_!$21", "~_!$21", "!!24/6",
                        "~_!$21@X", "~_!$21@Y", "~_!$21@Z", "~_!$21@U", "~_!$21@V", "~_$21@W",

                        "$31", "~_!$31", "~_!$31", "!!24/6",
                        "~_!$31@X", "~_!$31@Y", "~_!$31@Z", "~_!$31@U", "~_!$31@V", "~_$31@W",
                        "$01", "~_!$01", "~_!$01", "",
                    });
                    easeG.PositionEase = LinkEase(
                        Stable(BeatTime(1.4f), new Vector2(0, -245)) ,
                        EaseOut(BeatTime(1.1f), new Vector2(0, -245), new Vector2(0, 0), EaseState.Elastic)
                        );
                    easeG.ApplyTime = BeatTime(2.5f);
                    easeG.TagApply("G");
                    
                    BarrageCreate(BeatTime(4), BeatTime(2), 12, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "", 

                        //1
                        "(*$01@G)(*$21@G)", "", "", "",    "(*$01@G)(*$21@G)", "", "", "",
                        "(*$01@G)(*$21@G)", "", "", "",    "(*$01@G)(*$21@G)", "", "", "",
                        "(*$01@G)(*$21@G)", "", "", "",    "(*$01@G)(*$21@G)", "", "", "",
                        "(*$01@G)(*$21@G)", "", "", "",    "", "", "", "",     

                        //2
                        "(*$00@G)(*$20@G)", "", "", "",    "(*$00@G)(*$20@G)", "", "", "",
                        "(*$00@G)(*$20@G)", "", "", "",    "(*$00@G)(*$20@G)", "", "", "",
                        "(*$00@G)(*$20@G)", "", "", "",    "(*$00@G)(*$20@G)", "", "", "",
                        "(*$00@G)(*$20@G)", "", "", "(*$00)(*$20)",    "", "", "", "",     
                        //3
                        "''7",
                        "n01", "~_!+01", "~_!+01", "~_!+01",    "~_+01", "~_!+01", "~_+01", "",
                        "d", "~_!+0", "~_+0", "",    "d1", "~_!+01", "~_+01", "",
                        "d", "~_!+0", "~_+0", "",    "d1", "~_!+01", "~_+01", "",
                        "n2", "~_!+0", "~_+0", "",    "+0", "~_!+0", "~_+0", "", 
 
                        //4
                        "!!3", "$2", "$1", "$3",    "!!3", "$0", "$1", "$3",  
                        "!!3", "$2", "$1", "$3",    "!!3", "$0", "$1", "$3",  
                        "!!3", "$2", "$1", "$0",    "!!3", "$2", "$3", "$0", 
                        "!!3", "$2", "$1", "$0",     
                    });
                }
                if (InBeat(360))
                {
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "", 

                        //1
                        "", "", "", "",    "", "", "", "",
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
            }
            public void Normal()
            {

            }
            public void Start()
            {
                AddInstance(easeA = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.75f),
                    RotationEase = LinkEase(
                        Stable(BeatTime(0.5f), 0),
                        EaseOut(BeatTime(2.2f), 0, -45, EaseState.Sine))
                });
                AddInstance(easeB = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.75f),
                    RotationEase = LinkEase(
                        Stable(BeatTime(0.5f), -90),
                        EaseOut(BeatTime(2.2f), -90, -45, EaseState.Sine))

                });
                AddInstance(easeC = new Arrow.UnitEasing());
                AddInstance(easeD = new Arrow.UnitEasing());
                AddInstance(easeE = new Arrow.UnitEasing());
                AddInstance(easeF = new Arrow.UnitEasing()); 
                AddInstance(easeG = new Arrow.UnitEasing()); 

                easeX = new();
                AddInstance(easeX);
                easeY = new();
                AddInstance(easeY);
                easeZ = new();
                AddInstance(easeZ);
                easeU = new();
                AddInstance(easeU);
                easeV = new();
                AddInstance(easeV);
                easeW = new();
                AddInstance(easeW);

                production = Blur = new ScreenDrawing.Shaders.Blur(0.505f);
                production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.51f);
                splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                StepSample = Shaders.StepSample;
                Blur.Sigma = 0f;
                StepSample.Intensity = 0.0f;
                StepSample.CentreX = 320f;
                StepSample.CentreY = 240f;
                splitter.Intensity = 0.0f;
                ScreenDrawing.SceneRendering.InsertProduction(production);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(splitter);

                CreateEntity(r);
                GametimeDelta = -1.5f;
                SetSoul(1);
                InstantSetBox(new Vector2(320,240), 84, 84);
                InstantTP(320, 240);
                ScreenDrawing.MasterAlpha = 0f;
                ScreenDrawing.ScreenScale = 2f;
                bool jump = true;
                if (jump)
                {
                    int beat = 328;
                    GametimeDelta = -1.5f + BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}