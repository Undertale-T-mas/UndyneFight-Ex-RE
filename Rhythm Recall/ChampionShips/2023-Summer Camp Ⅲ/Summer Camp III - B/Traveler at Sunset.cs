using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UndyneFight_Ex;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.Remake;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
using System.Net.Mail;
using System.Dynamic;

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

            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public Information() { this.MusicOptimized = true; }
                public override string SongAuthor => "SK_kent";
                public override string BarrageAuthor => "zKronO vs Tlottgodinf vs T-mas ft. TK";
                public override string AttributeAuthor => "Woem feat. ParaDOXXX and TK";
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
            Blur Blur;
            RenderProduction production, production1, production2, production3, production4;
            GlobalResources.Effects.StepSampleShader StepSample;
            RGBSplitting splitter = new();
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
                        DrawingLab.DrawLine(point1, point2, Width, color * Colordepth, 0.1f);
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
            static Arrow.UnitEasing easeA = null, easeB = null, easeC = null, easeD, easeE, easeF, easeG, easeH, easeI, easeJ, easeK;
            static Arrow.EnsembleEasing easeX = null, easeY = null, easeZ = null, easeU, easeV, easeW, easeS2, easeT2, easeS1, easeT1;
            static Arrow.ClassicApplier easeK1, easeK2, easeK3;
            public void ExtremePlus()
            {
                CreateEntity(new TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(114), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { PlaySound = false });
                if (InBeat(0))
                {
                    RegisterFunction("FadeOut", () =>
                    {
                        RunEase((s) =>
                        {
                            ScreenDrawing.MasterAlpha = s;
                        },
                        Stable(BeatTime(8), 0),
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
                    RegisterFunctionOnce("Line", () =>
                    {
                        float LineX = Rand(200, 440);
                        float Rot = -100;
                        Line line = new(EaseIn(BeatTime(6), new Vector2(LineX, 240), new Vector2(0, 240), EaseState.Cubic).Easing, Stable(BeatTime(2), Rot).Easing);
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("LineCenter", () =>
                    {
                        Line line1 = new(EaseIn(BeatTime(6), new Vector2(320, 240), new Vector2(0, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 90).Easing);
                        CreateEntity(line1);
                        Line line2 = new(EaseIn(BeatTime(6), new Vector2(320, 240), new Vector2(320, -80), EaseState.Sine).Easing, Stable(BeatTime(2), 0).Easing);
                        CreateEntity(line2);
                        Line line3 = new(EaseIn(BeatTime(6), new Vector2(320, 240), new Vector2(640, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 90).Easing);
                        CreateEntity(line3);
                        Line line4 = new(EaseIn(BeatTime(6), new Vector2(320, 240), new Vector2(320, 560), EaseState.Sine).Easing, Stable(BeatTime(2), 0).Easing);
                        CreateEntity(line4);
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0.0f;
                            line.AlphaIncrease(BeatTime(2), 0.8f);
                            DelayBeat(6, () => { line.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineCenter45", () =>
                    {
                        Line line1 = new(EaseIn(BeatTime(4), new Vector2(320, 240), new Vector2(0, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 135).Easing);
                        CreateEntity(line1);
                        Line line2 = new(EaseIn(BeatTime(4), new Vector2(320, 240), new Vector2(320, -80), EaseState.Sine).Easing, Stable(BeatTime(2), 45).Easing);
                        CreateEntity(line2);
                        Line line3 = new(EaseIn(BeatTime(4), new Vector2(320, 240), new Vector2(640, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 135).Easing);
                        CreateEntity(line3);
                        Line line4 = new(EaseIn(BeatTime(4), new Vector2(320, 240), new Vector2(320, 560), EaseState.Sine).Easing, Stable(BeatTime(2), 45).Easing);
                        CreateEntity(line4);
                        Line[] lines = { line1,  line2, line3, line4 };
                        foreach(Line line in lines)
                        {
                            line.Alpha = 0.0f;
                            line.AlphaIncrease(BeatTime(2), 0.8f);
                            DelayBeat(6, () => { line.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineCenter8", () =>
                    {
                        Line line1 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(0, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 90).Easing);
                        Line line2 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(320, -80), EaseState.Sine).Easing, Stable(BeatTime(2), 0).Easing);
                        Line line3 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(640, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 90).Easing);
                        Line line4 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(320, 560), EaseState.Sine).Easing, Stable(BeatTime(2), 0).Easing);
                        Line line5 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(0, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 135).Easing);
                        Line line6 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(320, -80), EaseState.Sine).Easing, Stable(BeatTime(2), 45).Easing);
                        Line line7 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(640, 240), EaseState.Sine).Easing, Stable(BeatTime(2), 135).Easing);
                        Line line8 = new(EaseIn(BeatTime(3), new Vector2(320, 240), new Vector2(320, 560), EaseState.Sine).Easing, Stable(BeatTime(2), 45).Easing);
                        Line[] lines = { line1, line2, line3, line4, line5, line6, line7, line8 };
                        foreach (Line l in lines)
                        {
                            l.Alpha = 0f;
                            CreateEntity(l);
                            l.AlphaIncrease(BeatTime(2), 0.8f);
                            for (int i = 1; i < 2; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.7f - i * 0.07f));
                            }
                            DelayBeat(10.0f, () => { l.AlphaDecrease(BeatTime(1.3f)); });
                            DelayBeat(12f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Test", () =>
                    {
                        float[] args = Arguments;
                        ;
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.4f, new string[]
                    {   //0
                        "FadeOut","","","",    "","","","",
                        "<4,5>Test(<2,3>Test)","","","",    "","","","",
                        //1
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //2
                        "GaussBlur(Soul1)(Line)","","Line","",    "Line","","Line","",
                        "","","","",    "","","","",
                        "","","","(Soul1)",    "","","","",
                        "","","","",    "","","","",
                        //3
                        "GaussBlur(Line)","","Line","",    "Line","","(Soul1)(Line)","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","(Soul1)","","",    "","","","",
                        //4
                        "GaussBlur(Line)","","Line","",    "Line","","Line","",
                        "","","","",    "(Soul1)","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //5
                        "GaussBlur(Soul1)(Line)","","Line","",    "Line","","Line","",
                        "","","","",    "","","","",
                        "","","","(Soul1)",    "","","","",
                        "","","","",    "","","","",
                        //6
                        "GaussBlur(LineCenter)","","LineCenter","",    "LineCenter","","(Soul1)(LineCenter)","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","(Soul1)","","",    "","","","",
                        //7
                        "GaussBlur(LineCenter45)","","LineCenter45","",    "LineCenter45","","LineCenter45","",
                        "","","","",    "(Soul1)","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //8
                        "GaussBlur(Soul1)(LineCenter8)","","","",    "LineCenter8","","","",
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
                        Line a = new(LinkEase(EaseOut(BeatTime(0.8f), new Vector2(0, 0), new Vector2(120, 90), EaseState.Quart),
                            Stable(BeatTime(0),new Vector2(320,240))).Easing, 
                            Stable(BeatTime(1), -37.5f).Easing)
                        { Alpha = 0.6f };
                        Line b = new(LinkEase(EaseOut(BeatTime(0.8f), new Vector2(0, 480), new Vector2(120, 480 - 90), EaseState.Quart),
                            Stable(BeatTime(0), new Vector2(160, 120))).Easing,
                            Stable(BeatTime(1), 37.5f).Easing)
                        { Alpha = 0.6f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(i * 3, 0.75f - i * 0.05f));
                            }
                            DelayBeat(0, () => { l.AlphaDecrease(BeatTime(1.2f), 0.75f); });
                            DelayBeat(1.2f, () => { l.Dispose(); });
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
                        //r.Intensity = 6.0f;
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.4f, new string[]
                    {   //10
                        "$0(LoadA)","+1","+1","+1",    "+1","+1","+1","+1",
                        "+11(LoadB)","+11","+11","+11",   "+11","+11","+11","+11",
                        //11

                        "(*$0'2)(*$2'2)(KickA)","","","",    "","","","",
                        "(d)","","","",    "","","","",
                        "(d)","","","",    "","","","",
                        "d","","","",    "d","","","",
                        //12
                        "d","","","",    "","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        "d1","","","",    "d1","","","",
                        //13
                        "(d)","","","",    "","","","",
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
                        EaseOut(BeatTime(16), -86, 240, EaseState.Elastic),
                        Stable(0, 240));
                    });
                    RegisterFunctionOnce("DownLine", () =>
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            Line a = new(LinkEase(Stable(0, new Vector2(0, 0)),
                                EaseOut(BeatTime(3), new Vector2(320, 0), new Vector2(320, i * 32), EaseState.Quart),
                                Stable(BeatTime(1), new Vector2(320, i * 32))).Easing, Stable(0, 0).Easing)
                            { Alpha = 0.75f - i * 0.06f };
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
                        s = new()
                        {
                            BasicSpeed = 1.2f,
                            Width = 2.2f,
                            DrawingColor = Color.Lerp(Color.Aqua, Color.White, 0.7f),
                            Direction = true
                        };
                        GameStates.InstanceCreate(s);
                        easeA.TagApply("A");
                        DelayBeat(128.5f, () => { s.Dispose(); {                         
} });
                    });
                    RegisterFunctionOnce("Wave", () =>
                    {
                        RunEase(s => Heart.Rotation = s,
                            LinkEase(EaseOut(BeatTime(20), 330, 360, EaseState.Elastic), Stable(0, 0)));
                    });
                    RegisterFunctionOnce("a", () => { });
                    RegisterFunctionOnce("BoundA", () =>
                    {
                        RunEase((s) => { ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = s; },
                            LinkEase(EaseOut(BeatTime(4f), 160, 0, EaseState.Quart),
                            Stable(0, 0)));
                    });
                    RegisterFunctionOnce("BoundB", () =>
                    {
                        int k = 0;
                        ScreenDrawing.BoundColor = Color.Lerp(Color.Black, Color.Red, 0.65f);
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(i * 2, () =>
                            {
                                RunEase((s) => { ScreenDrawing.DownBoundDistance = s; },
                                    EaseOut(BeatTime(1.4f), 24 * k, 24 * k + 48, EaseState.Cubic),
                                    EaseOutIn(BeatTime(0.6f), 24 * k + 48, 24 * k + 24, EaseState.Cubic),
                                    Stable(0, 24 * k + 24));
                                k++;
                            });
                        }
                    });
                    RegisterFunctionOnce("LineGA", () =>
                    {
                        Line a=new(EaseOut(BeatTime(1),new Vector2(320,0),new Vector2(320,80),EaseState.Quad).Easing,
                            Stable(BeatTime(1), 0).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        a.VerticalMirror = true;
                        a.AlphaDecrease(BeatTime(1.3f));
                        for (int i = 0; i < 3; i++)
                        {
                            a.InsertRetention(new(BeatTime(0.2f * i)));
                        }
                        DelayBeat(1.3f, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineGA2", () =>
                    {
                        Line a = new(EaseOut(BeatTime(1), new Vector2(0, 240), new Vector2(60, 240), EaseState.Quad).Easing,
                            Stable(BeatTime(1), 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        a.TransverseMirror = true;
                        a.AlphaDecrease(BeatTime(1.3f));
                        for (int i = 0; i < 3; i++)
                        {
                            a.InsertRetention(new(BeatTime(0.2f * i)));
                        }
                        DelayBeat(1.3f, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineGB1", () =>
                    {
                        int k = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {
                                k++;
                                Line a = new(Stable(BeatTime(1.5f), new Vector2(640f / 9f * k, 240)).Easing,
                                    LinkEase(EaseOut(BeatTime(1.25f), 90, 110, EaseState.Quad), Stable(0, 110)).Easing)
                                { Alpha = 0.7f };
                                CreateEntity(a);
                                a.AlphaDecrease(BeatTime(1.75f));
                                DelayBeat(1.75f, () => { a.Dispose(); });
                            });
                        }
                    });
                    RegisterFunctionOnce("LineGB2", () =>
                    {
                        int k = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(0.5f * i, () =>
                            {
                                k++;
                                Line a = new(Stable(BeatTime(1.75f), new Vector2(640 - 640f / 9f * k, 240)).Easing,
                                    LinkEase(EaseOut(BeatTime(1.25f), 90, 70, EaseState.Quad), Stable(0, 70)).Easing)
                                { Alpha = 0.7f };
                                CreateEntity(a);
                                a.AlphaDecrease(BeatTime(1.75f));
                                DelayBeat(1.75f, () => { a.Dispose(); });
                            });
                        }
                    });
                    RegisterFunctionOnce("LineGC", () =>
                    {
                        int k = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            DelayBeat(0.25f * i, () =>
                            {
                                Line a = new(new Vector2(k * 36 - 64, 240), new Vector2(320, k * 27 - 48)) { Alpha = 0.7f };
                                Line b = new(new Vector2(k * 36 - 64, 240), new Vector2(320, 480 - k * 27 + 48)) { Alpha = 0.7f };
                                Line[] lines = { a, b };
                                foreach(Line l  in lines)
                                {
                                    CreateEntity(l);
                                    l.TransverseMirror = true;
                                    l.AlphaDecrease(BeatTime(1.75f));
                                    DelayBeat(1.75f, () => { l.Dispose(); });
                                }
                                k++;
                            });
                        }
                    });
                    RegisterFunctionOnce("LineGD1", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(2), new Vector2(0, 0), new Vector2(320, 240), EaseState.Back),
                            EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 160), EaseState.Quad),
                            EaseIn(BeatTime(1), new Vector2(320, 160), new Vector2(320, 240), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(-320, 240), EaseState.Sine)).Easing,
                            LinkEase(Stable(BeatTime(2), 90),
                            EaseOut(BeatTime(1.5f), 90, 315, EaseState.Sine),
                            Stable(BeatTime(2), 315)).Easing)
                        { Alpha = 0.7f };
                        Line b = new(LinkEase(EaseOut(BeatTime(2), new Vector2(0, 0), new Vector2(320, 240), EaseState.Back),
                            EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 160), EaseState.Quad),
                            EaseIn(BeatTime(1), new Vector2(320, 160), new Vector2(320, 240), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(-320, 240), EaseState.Sine)).Easing,
                            LinkEase(Stable(BeatTime(2), 0),
                            EaseOut(BeatTime(1.5f), 0, 225, EaseState.Sine),
                            Stable(BeatTime(2), 225)).Easing)
                        { Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach(Line l in lines)
                        {
                            CreateEntity(l);
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(BeatTime(0.125f * i), 0.7f - i * 0.08f));
                            }
                            DelayBeat(5.5f, () => { l.Dispose(); });
                            DelayBeat(2, () => { l.AlphaDecrease(1, 0.35f); });
                            DelayBeat(4, () => { l.AlphaIncrease(1, 0.7F); });
                        }
                    });
                    RegisterFunctionOnce("LineGD2", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(0.5f), new Vector2(640, 480), new Vector2(320, 240), EaseState.Back),
                            EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 160), EaseState.Quad),
                            EaseIn(BeatTime(1), new Vector2(320, 160), new Vector2(320, 240), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(640 + 320, 240), EaseState.Sine)).Easing,
                            LinkEase(Stable(BeatTime(0.5f), 90),
                            EaseOut(BeatTime(1.5f), 90, 315, EaseState.Sine),
                            Stable(BeatTime(2), 315)).Easing)
                        { Alpha = 0.7f };
                        Line b = new(LinkEase(EaseOut(BeatTime(0.5f), new Vector2(640, 480), new Vector2(320, 240), EaseState.Back),
                            EaseOut(BeatTime(1), new Vector2(320, 240), new Vector2(320, 160), EaseState.Quad),
                            EaseIn(BeatTime(1), new Vector2(320, 160), new Vector2(320, 240), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(640 + 320, 240), EaseState.Sine)).Easing,
                            LinkEase(Stable(BeatTime(0.5f), 0),
                            EaseOut(BeatTime(1.5f), 0, 225, EaseState.Sine),
                            Stable(BeatTime(2), 225)).Easing)
                        { Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(BeatTime(0.125f * i), 0.7f - i * 0.08f));
                            }
                            DelayBeat(4, () => { l.Dispose(); });
                            DelayBeat(0.5f, () => { l.AlphaDecrease(1, 0.35f); });
                            DelayBeat(2.5f, () => { l.AlphaIncrease(1, 0.35f); });
                        }
                    });
                    RegisterFunctionOnce("Rotate", () => {
                        RunEase((s) => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(2), 5f, EaseState.Cubic),
                            EaseOut(BeatTime(2), -10f, EaseState.Cubic),
                            EaseOut(BeatTime(2), 5f, EaseState.Cubic)
                            );
                    });
                    RegisterFunctionOnce("Eff1", () => {
                        Filter f1 = ScreenDrawing.ActivateShader(Shaders.Scatter, 0.50125f);
                        Shaders.Scatter.Ratio = 0.05f;
                        Shaders.Scatter.Intensity = 24f;
                        RunEase(s => splitter.Intensity = s,
                            EaseOut(BeatTime(1.5f), 12, 0, EaseState.Quad)
                        );
                        RunEase(s => Shaders.Scatter.Intensity = s,
                            EaseOut(BeatTime(1.5f), 24, 0, EaseState.Quad)
                        );
                        RunEase(s => Shaders.Scatter.Ratio = s,
                            EaseOut(BeatTime(1.5f), 0.6f, 0.05f, EaseState.Quad)
                        );
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            Alternate(1.0f,
                                EaseOut(BeatTime(1.2f), 5, 0, EaseState.Circ),
                                EaseOut(BeatTime(1.2f), -5, 0, EaseState.Circ)
                            )
                        );
                        DelayBeat(1.5f, () => {
                            f1.Dispose();
                        });
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "", "", "", "(UaD)",    "", "", "", "",
                        "", "", "", "",    "(pre)", "", "", "",    
                        //1
                        "(DownLine)(BoundA)(Wave)", "", "", "",    "", "", "", "",
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
                        "d1", "", "d", "",    "d", "", "d", "",    
                        //5
                        "d(d1)(BoundB)(LineGA)(Eff1)", "", "", "",    "d", "", "", "",
                        "d(d1)(LineGA)", "", "", "",    "d", "", "", "",
                        "d(d1)(LineGA)", "", "", "",    "d", "", "", "",
                        "d(d1)(LineGA)", "", "", "",    "d", "", "", "",
                        //6
                        "d(d1)(LineGA)", "", "", "",    "d1", "", "", "",
                        "d(d1)(LineGA)", "", "", "",    "d", "", "", "",
                        "d(d1)(LineGA)", "", "", "",    "d1", "", "", "",
                        "d(d1)(LineGA)", "", "", "",    "d", "", "", "",
                        //7
                        "(d1)(d)(LineGB1)(LineGA2)(Eff1)", "", "+1", "",    "+1", "", "+1", "",
                        "-1", "", "-1", "",    "-1", "", "-1", "",
                        "(d)(LineGB2)", "", "+11", "",    "+11", "", "+11", "",
                        "-11", "", "-11", "",    "-11", "", "-11", "",
                        //8 
                        "n01(LineGC)(Rotate)", "+0", "-11", "+0",    "-11", "+0", "-11", "+0",
                        "*$312@A(LineGC)", "*$302@A", "*$312@A", "*$302@A",    "*$112@A", "*$102@A", "*$112@A", "*$102@A",
                        "(*^$00'1.8)(*^$20'1.8)(LineGD1)", "", "", "",    "", "", "$10(LineGD2)", "",
                        "$30", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(72 + 128))
                {
                    RegisterFunctionOnce("pre", () =>
                    {
                        production1.Dispose();
                        BoxUtils.Vertexify();
                        var box = BoxUtils.VertexBoxInstance;

                        //     320,200
                        //240,280    400,280
                        //     320,360
                        //box.SetPosition(box.Split(3, 0.5f), new(400, 280));
                        //box.SetPosition(box.Split(2, 0.5f), new(320, 200));
                        //box.SetPosition(box.Split(1, 0.5f), new(240, 280));
                        //box.SetPosition(box.Split(0, 0.5f), new(320, 360));
                        for (int i = 0; i < 4; ++i)
                        {
                            box.SetPosition(i, GetVector2((i % 2) == 0 ? 40 : 80, i * 90) + new Vector2(320, 240));
                        }
                        //BoxUtils.Move(new(0, -40));
                        ScreenDrawing.BoxBackColor = Color.Transparent;

                        SetSoul(Souls.RedSoul);
                        ForBeat(28, () =>
                        {
                            var D = (GametimeF - BeatTime(200)) / 4;
                            for (int i = 0; i < 4; ++i)
                            {
                                box.SetPosition(i, GetVector2((i % 2) == 1 ? 10 + D : 320, i * 90) + new Vector2(320, 240));
                            }
                            if (At0thBeat(1))
                            {
                                
                            }
                        });
                        
                    });
                    RegisterFunctionOnce("Green", () =>
                    {
                        var box = BoxUtils.VertexBoxInstance;
                        for (int i = 0; i < 4; ++i)
                        {
                            box.SetPosition(i, GetVector2(80, i * 90) + new Vector2(320, 240));
                        }
                        SetSoul(1);
                        TP();
                        DelayBeat(1, () => {
                            ScreenDrawing.BoxBackColor = Color.Black * 0.8f;
                        });
                    });
                    RegisterFunctionOnce("Box", () =>
                    {
                        BoxUtils.DeVertexify(new(320 - 42, 240 - 42, 84, 84));
                        ScreenDrawing.BoxBackColor = Color.Black;
                    });
                    RegisterFunctionOnce("BounceSpear", () =>
                    {
                        var dir = Rand(0, 360);
                        Vector2 pos = GetVector2(Rand(100, 400), Rand(0, 360)) + new Vector2(320, 240);
                        for (int i = 0; i < 6; i++)
                        {
                            CreateEntity(new NormalSpear(pos, dir + i * 60, 4)
                            {
                                DelayTargeting = false,
                                Rebound = true,
                                Duration = 600,
                                Acceleration = 0.05f,
                                WaitingTime = BeatTime(1)
                            });
                        }
                    });
                    RegisterFunctionOnce("SpearFake", () =>
                    {
                        float rot = 0;
                        Fortimes(15, () =>
                        {
                            CreateSpear(new CircleSpear(Heart.Centre, 8, 5f, 150, rot += 24, 0.005f) { MarkScore = false });
                            CreateSpear(new CircleSpear(Heart.Centre, -8, 5f, 150, rot, 0.005f) { MarkScore = false });
                        });
                    });
                    RegisterFunctionOnce("BoundA", () =>
                    {
                        RunEase((s) => { ScreenDrawing.BoundColor = Color.Lerp(Color.Black, Color.Red, s); },
                            LinkEase(EaseOut(BeatTime(2), 0.65f, 0.35f, EaseState.Quad), Stable(0, 0.45f)));
                        RunEase(s => ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = s,
                            LinkEase(EaseOut(BeatTime(4), 160, EaseState.Quint), Stable(0, 160)));
                        RunEase(s => ScreenDrawing.DownBoundDistance = s,
                            LinkEase(EaseOut(BeatTime(8), 24 * 8, 120, EaseState.Quint), Stable(0, 120)));
                    });
                    RegisterFunctionOnce("BoundB", () =>
                    {
                        RunEase(s => ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = ScreenDrawing.DownBoundDistance = s,
                            LinkEase(EaseOut(BeatTime(2), 160, 0, EaseState.Quint), Stable(0, 0)));
                        RunEase(s => ScreenDrawing.DownBoundDistance = s,
                            LinkEase(EaseOut(BeatTime(2), 120, 0, EaseState.Quint), Stable(0, 0)));
                    });
                    RegisterFunctionOnce("GaussBlur", () =>
                    {
                        RunEase((s) => { Blur.Sigma = s * 1.5f; },
                            LinkEase(EaseOut(BeatTime(1), 0, 1.75f, EaseState.Linear), EaseOut(BeatTime(3), 1.75f, 0.5f, EaseState.Quint), Stable(0, 0)));
                        DelayBeat(30, () => { RunEase(s => Blur.Sigma = s, EaseOut(BeatTime(1), 0.5f, 0, EaseState.Linear)); });
                        DelayBeat(0.5f, () =>
                        {
                            RunEase(s => StepSample.Intensity = s,
                                LinkEase(EaseOut(BeatTime(0.75f), 0, 0.13f, EaseState.Linear), EaseOut(BeatTime(1.2f), 0.13f, 0, EaseState.Sine)));
                        });
                    });
                    RegisterFunctionOnce("SCL", () => {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), 4.0f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCR", () => {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), -4.0f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCS", () => {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.037f, 1.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("Eff1", () => {
                        production4 = ScreenDrawing.ActivateShader(Shaders.Scatter, 0.50125f);
                        Shaders.Scatter.Ratio = 0.05f;
                        Shaders.Scatter.Intensity = 12f;
                        RunEase(s => splitter.Intensity = s,
                            EaseOut(BeatTime(1.5f), 6, 0, EaseState.Quad)
                        );
                        RunEase(s => Shaders.Scatter.Intensity = s,
                            EaseOut(BeatTime(1.5f), 12, 0, EaseState.Quad)
                        );
                        RunEase(s => Shaders.Scatter.Ratio = s,
                            EaseOut(BeatTime(1.5f), 0.4f, 0.05f, EaseState.Quad)
                        );
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            Alternate(1.0f,
                                EaseOut(BeatTime(1.1f), 3.5f, 0, EaseState.Circ),
                                EaseOut(BeatTime(1.1f), -3.5f, 0, EaseState.Circ)
                            )
                        );
                        DelayBeat(1.5f, () => {
                            production4.Dispose();
                        });
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line a = new(EaseOut(BeatTime(0.7f), new Vector2(0, 240), new Vector2(640, 240), EaseState.Quad).Easing,
                            Stable(0, 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(0.7f, () => { a.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(1.0f, 9.0f, false);
                    });
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line a = new(EaseOut(BeatTime(0.7f), new Vector2(640, 240), new Vector2(0, 240), EaseState.Quad).Easing,
                            Stable(0, 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(0.7f, () => { a.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(1.0f, 9.0f, true);
                    });
                    RegisterFunctionOnce("TurnR", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640, 240), new Vector2(120, 240), EaseState.Quart),
                            EaseOut(BeatTime(0.7f), new Vector2(80, 240), new Vector2(640, 240), EaseState.Quad)).Easing,
                            Stable(0, 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(1.7f, () => { a.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(true);
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(false);
                        });
                    });
                    RegisterFunctionOnce("TurnL", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0, 240), new Vector2(640 - 120, 240), EaseState.Quart),
                            EaseOut(BeatTime(0.7f), new Vector2(640 - 120, 240), new Vector2(0, 240), EaseState.Quad)).Easing,
                            Stable(0, 90).Easing)
                        { Alpha = 0.7f };
                        CreateEntity(a);
                        DelayBeat(1.7f, () => { a.Dispose(); });
                        ScreenDrawing.CameraEffect.Convulse(false);
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(true);
                        });
                    });
                    RegisterFunctionOnce("LineG1", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0, 0), new Vector2(160, 120), EaseState.Quart),
                            Stable(0, new Vector2(160, 120))).Easing,
                            Stable(0, -30).Easing){ Alpha = 0.65f };
                        Line b = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0 - 40, 0 - 30), new Vector2(160 - 40, 120 - 30), EaseState.Quart),
                            Stable(0, new Vector2(160 - 40, 120 - 30))).Easing,
                            Stable(0, -30).Easing)
                        { Alpha = 0.4f };
                        Line c = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640 - 0, 480 - 0), new Vector2(640 - 160, 480 - 120), EaseState.Quart),
                            Stable(0, new Vector2(640 - 160, 480 - 120))).Easing,
                            Stable(0, -30).Easing)
                        { Alpha = 0.65f };
                        Line d = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640 - 0 + 40, 480 + 30), new Vector2(640 - 160 + 40, 480 - 120 + 30), EaseState.Quart),
                            Stable(0, new Vector2(640 - 160 + 40, 480 - 120 + 30))).Easing,
                            Stable(0, -30).Easing)
                        { Alpha = 0.4f };
                        Line[] lines = { a, b, c, d };
                        foreach(Line l in lines)
                        {
                            CreateEntity(l);
                            DelayBeat(0.7f, () => { l.AlphaDecrease(BeatTime(1.2f)); });
                            DelayBeat(1.9f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineG2", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640, 0), new Vector2(640 - 160, 120), EaseState.Quart),
                            Stable(0, new Vector2(640 - 160, 120))).Easing,
                            Stable(0, 30).Easing)
                        { Alpha = 0.65f };
                        Line b = new(LinkEase(EaseOut(BeatTime(1), new Vector2(640 + 40, 0 - 30), new Vector2(640 - 160 + 40, 120 - 30), EaseState.Quart),
                            Stable(0, new Vector2(640 - 160 + 40, 120 - 30))).Easing,
                            Stable(0, 30).Easing)
                        { Alpha = 0.4f };
                        Line c = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0, 480), new Vector2(160, 480 - 120), EaseState.Quart),
                            Stable(0, new Vector2(160, 480 - 120))).Easing,
                            Stable(0, 30).Easing)
                        { Alpha = 0.65f };
                        Line d = new(LinkEase(EaseOut(BeatTime(1), new Vector2(0 - 40, 480 + 30), new Vector2(160 - 40, 480 - 120 + 30), EaseState.Quart),
                            Stable(0, new Vector2(160 - 40, 480 - 120 + 30))).Easing,
                            Stable(0, 30).Easing)
                        { Alpha = 0.4f };
                        Line[] lines = { a, b, c, d };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            DelayBeat(0.7f, () => { l.AlphaDecrease(BeatTime(1.2f)); });
                            DelayBeat(1.9f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("SCS-G", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.07f, 1.0f, EaseState.Quint));
                        ScreenDrawing.MakeFlicker(Color.White * 0.6f);
                        Line a = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240 - 320), new Vector2(320 - 160, 240 - 160), EaseState.Quart),
                            Stable(BeatTime(4), new Vector2(320 - 160, 240 - 160)),
                            EaseIn(BeatTime(1.5f), new Vector2(320 - 160, 240 - 160), new Vector2(320 - 320, 240 - 320), EaseState.Cubic)).Easing,
                            Stable(0, -45).Easing)
                        { Alpha = 0.7f };
                        Line b = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240 + 320), new Vector2(320 - 160, 240 + 160), EaseState.Quart),
                            Stable(BeatTime(4), new Vector2(320 - 160, 240 + 160)),
                            EaseIn(BeatTime(1.5f), new Vector2(320 - 160, 240 + 160), new Vector2(320 - 320, 240 + 320), EaseState.Cubic)).Easing,
                            Stable(0, 45).Easing)
                        { Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            for (int i = 0; i < 5; i++)
                            {
                                l.InsertRetention(new(BeatTime(0.25f * i), 0.7f - 0.08f * i));
                            }
                            DelayBeat(6, () => { l.AlphaDecrease(BeatTime(1)); });
                            DelayBeat(7.5f, () => { l.Dispose(); });
                        }
                        DelayBeat(2, () =>
                        {
                            RunEase(s => ScreenDrawing.ScreenScale = s,
                                EaseOut(BeatTime(2), 1.12f, 1.0f, EaseState.Quint));
                            ScreenDrawing.MakeFlicker(Color.White * 0.6f);
                            Line a = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320, 240 - 320), new Vector2(320, 240 - 120), EaseState.Quart),
                                Stable(BeatTime(2), new Vector2(320, 240 - 120)),
                                EaseIn(BeatTime(1.5f), new Vector2(320, 240 - 120), new Vector2(320, 240 - 320), EaseState.Cubic)).Easing,
                                Stable(0, 0).Easing)
                            { Alpha = 0.7f };
                            Line b = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240), new Vector2(320 - 120, 240), EaseState.Quart),
                                Stable(BeatTime(2), new Vector2(320 - 120, 240)),
                                EaseIn(BeatTime(1.5f), new Vector2(320 - 120, 240), new Vector2(320 - 320, 240), EaseState.Cubic)).Easing,
                                Stable(0, 90).Easing)
                            { Alpha = 0.7f };
                            CreateEntity(a, b);
                            a.VerticalMirror = true;
                            b.TransverseMirror = true;
                            for (int i = 0; i < 5; i++)
                            {
                                a.InsertRetention(new(BeatTime(0.25f * i), 0.7f - 0.08f * i));
                                b.InsertRetention(new(BeatTime(0.25f * i), 0.7f - 0.08f * i));
                            }
                            DelayBeat(4, () => { a.AlphaDecrease(BeatTime(1)); b.AlphaDecrease(BeatTime(1)); });
                            DelayBeat(5.5f, () => { a.Dispose(); b.Dispose(); });
                        });
                        DelayBeat(4, () =>
                        {
                            RunEase(s => ScreenDrawing.ScreenScale = s,
                                EaseOut(BeatTime(1), 1.17f, 1.0f, EaseState.Quint));
                            ScreenDrawing.MakeFlicker(Color.White * 0.6f);
                            Line a = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240 - 320), new Vector2(320 - 80, 240 - 80), EaseState.Quart),
                                Stable(BeatTime(0), new Vector2(320 - 80, 240 - 80)),
                                EaseIn(BeatTime(1.5f), new Vector2(320 - 80, 240 - 80), new Vector2(320 - 320, 240 - 320), EaseState.Cubic)).Easing,
                                Stable(0, -45).Easing)
                            { Alpha = 0.7f };
                            Line b = new(LinkEase(EaseOut(BeatTime(2), new Vector2(320 - 320, 240 + 320), new Vector2(320 - 80, 240 + 80), EaseState.Quart),
                                Stable(BeatTime(0), new Vector2(320 - 80, 240 + 80)),
                                EaseIn(BeatTime(1.5f), new Vector2(320 - 80, 240 + 80), new Vector2(320 - 320, 240 + 320), EaseState.Cubic)).Easing,
                                Stable(0, 45).Easing)
                            { Alpha = 0.7f };
                            Line[] lines = { a, b };
                            foreach (Line l in lines)
                            {
                                CreateEntity(l);
                                l.TransverseMirror = true;
                                for (int i = 0; i < 5; i++)
                                {
                                    l.InsertRetention(new(BeatTime(0.25f * i), 0.7f - 0.08f * i));
                                }
                                DelayBeat(2, () => { l.AlphaDecrease(BeatTime(1)); });
                                DelayBeat(3.5f, () => { l.Dispose(); });
                            }
                        });
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.5f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",    
                        "", "", "", "",    "(pre)", "", "", "",
                        //1
                        "(BoundA)(GaussBlur)", "BounceSpear", "BounceSpear", "BounceSpear",    "BounceSpear", "", "", "",
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
                        "(BoundB)", "", "", "SpearFake",    "", "", "", "",
                        "(Green)(Box)", "", "", "",    "", "", "", "",      
                        //5
                        "(d1)(+00)(LineL)", "", "", "",    "(d1)(+00)(SCS)(LineL)", "", "d1", "",
                        "d1(LineL)", "", "d1", "",    "(d1)(+00)(SCS)(LineL)", "", "d1", "",
                        "(d1)(+00)(LineL)", "", "d1", "",    "(d1)(+00)(SCS)(LineL)", "", "d1", "",
                        "(d1)(+00)(LineL)", "", "d1", "",    "(d1)(+00)(SCS)(TurnR)", "", "d1", "",    
                        //6
                        "d(d1)", "", "", "",    "d(d1)(SCS)(LineR)", "", "d", "",
                        "d(LineR)", "", "d", "",    "d(d1)(SCS)(LineR)", "", "", "",
                        "d(d1)(LineR)", "", "d", "",    "d(d1)(SCS)(LineR)", "", "d", "",
                        "d(d1)(LineR)", "", "d", "",    "d(d1)(SCS)(TurnL)", "", "+0", "",      
                        //7
                        "*^$01'1.6(*^$21'1.6)(Eff1)", "", "", "",    "d(d1)(SCS)(SCL)(LineG2)", "", "d", "",
                        "d(LineR)", "", "d", "",    "d(d1)(SCS)(SCR)(LineG1)", "", "d", "",
                        "d(LineL)", "", "d", "",    "d(d1)(SCS)(SCL)(LineG2)", "", "d", "",
                        "d(LineR)", "", "d", "",    "d(d1)(SCS)(SCR)(LineG1)", "", "n3", "",    
                        //8
                        "$3(n31)(SCS-G)", "", "$0", "$1",    "$2", "", "$3", "",
                        "$31", "", "$21", "$11",    "$01", "", "$31", "",
                        "(D)(+01)", "", "(D)(+01)", "",    "(D)(+01)", "", "(D)(+01)", "",
                        "(D)(+01)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(72 + 192))
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
                    RegisterFunctionOnce("ROTL", () => {
                        RunEase((s) => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), 2.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 2.0f, EaseState.Cubic)
                        );
                    });
                    RegisterFunctionOnce("ML", () => {
                        RunEase((s) => ScreenDrawing.ScreenPositionDetla = new Vector2(s, 0),
                            EaseOut(BeatTime(1), 4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), 4.0f, EaseState.Sine)
                        );
                        RunEase((s) => ScreenDrawing.ScreenAngle = s * 0.8f,
                            EaseOut(BeatTime(1), -1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -1.0f, EaseState.Cubic)
                        );
                        RunEase((s) => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.0f, 1.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -0.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 0.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -0.004f, EaseState.Cubic)
                        );
                    });
                    RegisterFunctionOnce("ROTR", () => {
                        RunEase((s) => ScreenDrawing.ScreenAngle = -s,
                            EaseOut(BeatTime(1), 2.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 2.0f, EaseState.Cubic)
                        );
                    });
                    RegisterFunctionOnce("MR", () => {
                        RunEase((s) => ScreenDrawing.ScreenPositionDetla = new Vector2(-s, 0),
                            EaseOut(BeatTime(1), 4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), -4.0f, EaseState.Sine),
                            EaseOut(BeatTime(1), 4.0f, EaseState.Sine)
                        );
                        RunEase((s) => ScreenDrawing.ScreenAngle = -s * 0.8f,
                            EaseOut(BeatTime(1), -1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 1.0f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -1.0f, EaseState.Cubic)
                        );
                        RunEase((s) => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.0f, 1.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -0.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), 0.004f, EaseState.Cubic),
                            EaseOut(BeatTime(1), -0.004f, EaseState.Cubic)
                        );
                    });
                    RegisterFunctionOnce("ShakeA", () => { 
                        RunEase((s) => ScreenDrawing.ScreenAngle = s, false,
                            EaseOut(BeatTime(0.5f), -2.1f, 0, EaseState.Circ),
                            EaseOut(BeatTime(0.5f), -2.1f, 0, EaseState.Circ),
                            EaseOut(BeatTime(0.5f), 2.1f, 0, EaseState.Circ),
                            EaseOut(BeatTime(0.5f), 2.1f, 0, EaseState.Circ)
                        );
                    });
                    RegisterFunctionOnce("LineL", () => {
                        Line l;
                        CreateEntity(l = new Line(
                                EaseOut(BeatTime(1.2f), new Vector2(666, 0), EaseState.Quart).Easing,
                                Stable(BeatTime(2), 90).Easing
                            )
                        { Alpha = 0.5f });
                        l.DelayDispose(BeatTime(1.2f));
                        l.InsertRetention(new(0.5f, 0.6f));
                        l.InsertRetention(new(1.0f, 0.4f));
                    });
                    RegisterFunctionOnce("LineR", () => {
                        Line l;
                        CreateEntity(l = new Line(
                                EaseOut(BeatTime(1.2f), new(640, 0), new Vector2(-26, 0), EaseState.Quart).Easing,
                                Stable(BeatTime(2), 90).Easing
                            )
                        { Alpha = 0.5f });
                        l.DelayDispose(BeatTime(1.2f));
                        l.InsertRetention(new(0.5f, 0.6f));
                        l.InsertRetention(new(1.0f, 0.4f));
                    });
                    RegisterFunctionOnce("SCL", () => {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), 1.3f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCR", () => {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(1), -1.3f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCL2", () => {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(0.5f), 1.1f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCR2", () => {
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            EaseOut(BeatTime(0.5f), -1.1f, 0.0f, EaseState.Quint)
                        );
                    });
                    RegisterFunctionOnce("SCS", () => {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            EaseOut(BeatTime(1), 1.02f, 1.0f, EaseState.Quint)
                        );
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "(pre)", "", "", "",    
                        //1
                        "$21(ROTL)", "$01", "$20", "$00",     "$21", "$01", "$20", "$00",   
                        "*$202@A", "", "*$002@A", "*$202@A",    "*$002@B", "", "*$202@B", "",
                        "*$012@B(ML)", "", "*$212@A", "*$012@A",    "*$212@B", "", "*$002@A", "*$202@A",
                        "*$002@B", "", "*$212@A", "*$012@A",    "*$212@A", "", "", "",     
                        //2
                        "$11(ROTR)", "", "$31", "",    "$00", "$10", "$21", "$11", 
                        "$0", "$1", "$2", "",    "$31", "", "$11", "",
                        "$31(MR)", "", "$0", "$1",    "$2", "", "$11", "",
                        "$31", "", "$0", "$1",    "$2", "", "$11", "",      
                        //3
                        "*$212@X'1.2(*$012@X'1.2)(ROTL)", "", "$20", "$00",     "$21", "$01", "$20", "$00",
                        "*$012@A", "", "*$212@A", "*$012@A",    "*$212@B", "", "*$012@B", "",
                        "*$202@B(ML)", "", "*$002@A", "*$202@A",    "*$002@B", "", "*$212@A", "*$012@A",
                        "*$212@B", "", "*$002@A", "*$202@A",    "*$002@A", "", "", "",      
                        //4      
                        "$20(ROTR)", "", "$00", "$01",    "$10", "$11", "$20", "$21",
                        "$30", "$31", "$00", "$01",    "$10", "$11", "$20", "$21",
                        "$30", "", "$21", "$00",    "$21", "", "$0", "",
                        "(*$00'1.2)(*$20'1.2)", "", "(*$01'1.2)(*$21'1.2)", "",    "(*$00'1.2)(*$20'1.2)", "", "(*$01'1.2)(*$21'1.2)", "",   
                        //5
                        "", "", "", "",
                        "$1(SCS)(LineL)", "$2", "$1", "",    "$31", "", "$1", "$0",
                        "$1(SCS)(LineL)", "", "$11", "",    "$31", "", "$1", "$2",
                        "$1(SCS)(LineL)", "", "$11", "$01",    "$11", "", "$1", "$0",
                        "$1(SCS)(LineL)", "", "", "",    
                        //6
                        "$31(SCL)", "", "$11", "",    "$2(SCS)(LineR)", "$1", "$01", "$11",
                        "$0(SCR)", "$1", "$2", "",    "$31(SCS)(LineR)", "", "$11", "",
                        "$31(SCL)", "", "$0", "$1",    "$2(SCS)(LineR)", "", "$3", "",
                        "$1(SCR)", "", "$21", "$11(changeEase)",    "$01(SCS)(LineR)", "", "$3", "",      
                        //7
                        "*$212@X'1.2(*$012@X'1.2)(SCL)", "", "$1(SCS)(LineL)", "",    "$11", "$01", "$0", "$1",
                        "$2(SCR)", "", "$21", "$11",    "$01(SCS)(LineL)", "", "$3", "",
                        "$31(SCL)", "", "$0", "$1",    "$2(SCS)(LineL)", "", "$21", "$11",
                        "$01(SCR)", "", "$0", "$1",    "$2(SCS)", "", "", "",    
                        //8
                        "*$202@A'1.2(*$002@A'1.2)(SCL2)", "", "*$202@B'1.2(*$002@B'1.2)(SCR2)", "*$212@A'1.2(*$012@A'1.2)(SCR2)",    "*$202@B'1.2(*$002@B'1.2)(SCR2)", "", "*$212@A'1.2(*$012@A'1.2)(SCL2)", "",
                        "*$202@B'1.2(*$002@B'1.2)(SCR2)", "", "*$202@A'1.2(*$002@A'1.2)(SCL2)", "",    "", "", "(*$202@B'1.2)(*$002@B'1.2)(SCR2)", "",
                        "(*$202@A'1.2)(*$002@A'1.2) ", "", "*$102@A", "*$112@B",    "*$102@A", "*$112@B", "*$102@A", "",
                        "*$112@A", "*$102@B",    "*$112@A", "*$102@B", "*$112@A",
                    });
                }
                if (InBeat(328))
                {
                    base.Settings.VoidArrowVolume = 0.0f;
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
                        Regenerate();
                        PlaySound(Sounds.heal);
                    });

                    // Generate the effects
                    DelayBeat(4, () =>
                    {
                        Effect01();
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
                    easeG.TagApply("G");
                    RegisterFunction("WindSlow", () =>
                    {
                        RunEase((speed) =>
                        {
                            r.BasicSpeed = speed;
                        },
                        Stable(0, 1),
                        EaseOut(BeatTime(1), -0.7f, EaseState.Quad)
                        );
                        RunEase((speed) =>
                        {
                            r.Intensity = speed;
                        },
                        Stable(0, 10),
                        EaseOut(BeatTime(1), 10f, EaseState.Quad)
                        );
                    });
                    RegisterFunction("WindFast", () =>
                    {
                        RunEase((speed) =>
                        {
                            r.BasicSpeed = speed;
                        },
                        Stable(0, 0.3f),
                        EaseOut(BeatTime(1), 0.7f, EaseState.Quad)
                        );
                        RunEase((speed) =>
                        {
                            r.Intensity = speed;
                        },
                        Stable(0, 20),
                        EaseOut(BeatTime(1), -10f, EaseState.Quad)
                        );
                    });
                    RegisterFunction("WindFaster", () =>
                    {
                        RunEase((speed) =>
                        {
                            r.BasicSpeed = speed;
                        },
                        Stable(0, 1f),
                        EaseOut(BeatTime(1), 0.25f, EaseState.Quad)
                        );
                        RunEase((speed) =>
                        {
                            r.Intensity = speed;
                        },
                        Stable(0, 10),
                        EaseOut(BeatTime(1), -2f, EaseState.Quad)
                        );
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 12, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "pre", "", "", "",    "", "", "", "", 

                        //1
                        "(*$01@G)(*$21@G)", "", "", "",    "(*$01@G)(*$21@G)", "", "", "",
                        "(*$01@G)(*$21@G)", "", "", "",    "(*$01@G)(*$21@G)", "", "", "",
                        "(*$01@G)(*$21@G)", "", "", "",    "(*$01@G)(*$21@G)", "", "", "",
                        "(*$01@G)(*$21@G)", "", "", "",    "(*$01@G)(*$21@G)", "", "(*$01@G)(*$21@G)", "",     

                        //2
                        "(*$00@G)(*$20@G)", "", "", "",    "(*$00@G)(*$20@G)", "", "", "",
                        "(*$00@G)(*$20@G)", "", "", "",    "(*$00@G)(*$20@G)", "", "", "",
                        "(*$00@G)(*$20@G)", "", "", "",    "(*$00@G)(*$20@G)", "", "", "",
                        "(*$00@G)(*$20@G)(WindSlow)", "", "", "(*$00)(*$20)",    "", "", "", "",     
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
                if (InBeat(358))
                {
                    easeJ.PositionEase = LinkEase(
                        Stable(BeatTime(0), new Vector2(-42, 0)),
                        EaseOut(BeatTime(1.75f), new Vector2(-42, 0), new Vector2(18, 0), EaseState.Quad));
                    easeJ.ApplyTime = BeatTime(1.75f);
                    easeJ.TagApply("K1C");
                    easeK.PositionEase = LinkEase(
                        Stable(BeatTime(0), new Vector2(42, 0)),
                        EaseOut(BeatTime(1.75f), new Vector2(42, 0), new Vector2(-18, 0), EaseState.Quad));
                    easeK.ApplyTime = BeatTime(1.75f);
                    easeK.TagApply("K1D");
                    RegisterFunctionOnce("pre", () =>
                    {
                        easeH.PositionEase = LinkEase(
                            Stable(BeatTime(4), new Vector2(0, 0)),
                            EaseOut(BeatTime(0.75f), new Vector2(0, 0), new Vector2(-18, 0), EaseState.Quad));
                        easeH.ApplyTime = BeatTime(4.75f);
                        easeH.TagApply("K1A");
                        easeI.PositionEase = LinkEase(
                            Stable(BeatTime(4), new Vector2(0, 0)),
                            EaseOut(BeatTime(0.75f), new Vector2(0, 0), new Vector2(18, 0), EaseState.Quad));
                        easeI.ApplyTime = BeatTime(4.75f);
                        easeI.TagApply("K1B");
                        AddInstance(easeK1 = new Arrow.ClassicApplier());
                        DelayBeat(3.75f, () =>
                        {
                            easeK1.ApplyDelay(BeatTime(2));
                        });
                        easeK1.TagApply("K1");
                        easeS1.TagApply("S1");
                        easeT1.TagApply("T1");
                        easeS1.RevolutionEase(Stable(1, 13));
                        easeT1.RevolutionEase(Stable(1, -13));
                        easeS2.TagApply("S2");
                        easeT2.TagApply("T2");
                        easeS2.RevolutionEase(Stable(1, 17));
                        easeT2.RevolutionEase(Stable(1, -17));
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "pre", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(*$3@K1,K1A)(_$3@K1,K1B)(WindFast)", "", "", "",    "(*$3@K1,K1A)(_$3@K1,K1B)", "", "", "", 

                        //1
                        "(#7.6#$1)(*>$01)(*<$21)(~_$01'1.4)(~_$21'2.2)", "", "", "",    "(*<$01)(*>$21)(~_$01'2.2)(~_$21'2.2)", "", "", "",
                        "(*$3@K1C)(_$3@K1D)(*>$01)(*<$21)(~_$01'2.2)(~_$21'2.2)", "", "", "",    "(*<$3@K1C)(_>$3@K1D)(*<$01)(*>$21)(~_$01'2.2)(~_$21'2.2)", "", "", "",
                        "(*$3@K1C)(_$3@K1D)(*>$01)(*<$21)(~_$01'2.2)(~_$21'2.2)", "", "", "",    "(*<$3@K1C)(_>$3@K1D)(*<$01)(*>$21)(~_$01'2.2)(~_$21'2.2)", "", "", "",
                        "(*$3@K1C)(_$3@K1D)(*>$01)(*<$21)(~_$01'2.2)(~_$21'2.2)", "", "", "",    "(*<$3@K1C)(_>$3@K1D)", "", "(*>$3@K1C)(_<$3@K1D)", "",
                        
                        //2
                        "(*<$11@K1C)(_>$11@K1D)(#7.6#$31)(*<$0)(*>$2)(~_$0'2.2)(~_$2'2.2)", "", "", "",    "(*$11@K1C)(_$11@K1D)(*>$0)(*<$2)(~_$0'2.2)(~_$2'2.2)", "", "", "",
                        "(*<$11@K1C)(_>$11@K1D)(*<$0)(*>$2)(~_$0'2.2)(~_$2'2.2)", "", "", "",    "(*$11@K1C)(_$11@K1D)(*>$0)(*<$2)(~_$0'2.2)(~_$2'2.2)", "", "", "",
                        "(*<$11@K1C)(_>$11@K1D)(*<$0)(*>$2)(~_$0'2.2)(~_$2'2.2)", "", "", "",    "(*$11@K1C)(_$11@K1D)(*>$0)(*<$2)(~_$0'2.2)(~_$2'2.2)", "", "(*<$11@K1C)(_>$11@K1D)", "",
                        "(*$11@K1C)(_$11@K1D)(*<$0)(*>$2)(~_$0'2.2)(~_$2'2.2)(WindSlow)", "", "", "(*<$11@K1C)(_>$11@K1D)",    "(*$11@K1C)(_$11@K1D)", "", "(*<$11@K1C)(_>$11@K1D)", "",
                        //3 
                        "n0", "~_!+0", "~_!+0", "~_!+0",    "~_+0", "~_!+0", "~_+0", "",
                        "d1", "~_!+01", "~_+01", "",    "d", "~_!+0", "~_+0", "",
                        "!!3","$21", "$31", "$01",    "!!3","$11", "$21", "$11",
                        "!!3","$01(WindFast)", "$11", "$21","!!3","$11", "$21", "$01",
 
                        //4
                        "!!3", "($31)(+01)", "+11", "+11",    "!!3", "-11", "-11", "-11", 
                        "!!3", "($01)(+01)", "$11", "$21",    "!!3", "$01", "$31", "$21",
                        "(*$01'1.4@S1)(*$21'1.4@S2)(*$00'2.1@T1)(*$20'2.1@T2)", "", "", "",    "", "", "", "",
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
                if (InBeat(392))
                {
                    RegisterFunctionOnce("BugLine", () =>
                    {
                        var cen = LinkEase(false,Stable(0, new Vector2(180, 240)),
                            Linear(BeatTime(2f), new Vector2(180, 240), new Vector2(180, 240)+GetVector2(180,-24)),
                            Stable(0f, new Vector2(520,300)),
                            Linear(BeatTime(2.5f), new Vector2(520, 300), new Vector2(520, 300)+GetVector2(240,-130)),
                            Stable(0f, new Vector2(320, 440)),
                            Linear(BeatTime(2.5f), new Vector2(320, 440), new Vector2(320, 440) + new Vector2(0, -240)),
                            Stable(0f, new Vector2(0, 0)),
                            Linear(BeatTime(5f), new Vector2(0, 0), new Vector2(0, 0) + new Vector2(240,0)),
                            Stable(BeatTime(8f), new Vector2(320, 240))
                            );
                        var rot = LinkEase(false,Stable(0, -24+90),
                            Stable(BeatTime(2), -24 + 90),
                            Stable(BeatTime(2.5f), -130 + 90),
                            Stable(BeatTime(2.5f), 0),
                            Stable(BeatTime(5f), 90),
                            Linear(BeatTime(8),-60,-60+720)
                            );
                        Line l = new(cen.Easing, rot.Easing) { DrawingColor=Color.DarkRed,Width=8};
                        CreateEntity(l);
                        RunEase((sr)=> { l.Alpha = sr; },false,Alternate(2, Stable(BeatTime(16),0f), Stable(BeatTime(16), 0.8f)));
                        for(int a=0;a<4;a++)
                        {
                            int x = a;
                            Line.RetentionEffect r = new(4*x);
                            l.InsertRetention(r);
                        }
                    });
                    RegisterFunctionOnce("BugLine2", () =>
                    {
                        var cen = LinkEase(false, Stable(0, new Vector2(0, 80)),
                            EaseOut(BeatTime(2f), new Vector2(0, 80), new Vector2(0, 160),EaseState.Sine)
                            );
                        var rot = LinkEase(false, Stable(0,0)
                            );
                        Line l = new(cen.Easing, rot.Easing) { DrawingColor = Color.DarkRed, Width = 8,VerticalMirror=true };
                        CreateEntity(l);
                        RunEase((sr) => { l.Alpha = sr; }, false, Alternate(2, Stable(BeatTime(4)+0.5f, 0f), Stable(BeatTime(4), 0.8f)));
                        for (int a = 0; a < 4; a++)
                        {
                            int x = a;
                            Line.RetentionEffect r = new(BeatTime(1) * x);
                            l.InsertRetention(r);
                        }
                    });
                    RegisterFunctionOnce("CrossLine", () =>
                    {
                        Line[] ls = GetAll<Line>();
                        for(int a=0; a<ls.Length; a++)
                        {
                            int x = a;
                            ls[x].Dispose();
                        }

                        var ce = EaseInOut(BeatTime(16), 0, 360 * 3, EaseState.Quad).Easing;
                        Line l = new(Stable(0, new Vector2(320, 240)).Easing, ce) {DrawingColor=Color.Gray };
                        CreateEntity(l);
                        RunEase((s) => { l.Alpha = s; },
                            Stable(0, 0),
                            EaseOut(BeatTime(1), 1, EaseState.Sine),
                            Stable(BeatTime(12),1),
                            EaseIn(BeatTime(1),-1.5f, EaseState.Sine)
                            );
                        for (int a = 0; a < 8; a++)
                        {
                            int x = a;
                            Line.RetentionEffect r = new(8 * x,(8-x)*0.1f);
                            l.InsertRetention(r);
                        }
                    });
                    RegisterFunctionOnce("pre", () => {
                        ScreenDrawing.SceneRendering.ResetProduction();
                        easeA?.Dispose(); easeB?.Dispose();
                        AddInstance(easeA = new Arrow.UnitEasing()
                        {
                            ApplyTime = BeatTime(3.25f),
                            RotationEase = LinkEase(
                                Stable(BeatTime(0.5f), 0),
                                EaseOut(BeatTime(2.7f), 0, -45, EaseState.Sine))
                        });
                        AddInstance(easeB = new Arrow.UnitEasing()
                        {
                            ApplyTime = BeatTime(3.25f),
                            RotationEase = LinkEase(
                                Stable(BeatTime(0.5f), -90),
                                EaseOut(BeatTime(2.7f), -90, -45, EaseState.Sine))
                        });
                        easeA.TagApply("A"); easeB.TagApply("B");
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "pre", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",  
                          
                        //1
                        "!!3", "$1(BugLine)(<0.5,1>Drum)", "$11", "$3",    "!!3", "d1(<0.5,1>Drum)", "d", "d1", 
                        "!!3", "d(<0.5,1>Drum)", "d1", "d",    "!!3", "d1(<0.5,1>Drum)", "d", "d1", 
                        "!!3", "d(<0.5,1>Drum)", "d1", "d",    "!!3", "d1(<0.5,1>Drum)", "d", "d1", 
                        "!!3", "d(<0.5,1>Drum)", "d1", "d",    "!!3", "d1(<0.5,1>Drum)", "+0", "+0", 

                        //2
                        "!!3", "D1(<0.5,-1>Drum)", "*+002@A", "*+002@B",    "!!3", "d1(<0.5,-1>Drum)", "*+002@A", "*+002@B",
                        "!!3", "d1(<0.5,-1>Drum)", "*+002@A", "*+002@B",    "!!3", "d1(<0.5,-1>Drum)", "*+002@A", "*+002@B",
                        "!!3", "d1(<0.5,-1>Drum)(BugLine2)", "*+002@A", "*+002@B",    "!!3", "d1(<0.5,-1>Drum)", "*+002@A", "*+002@B",
                        "!!3", "d1(<0.5,-1>Drum)", "*+002@A", "*+002@B",    "!!3", "d1(<0.5,-1>Drum)", "", "",

                        //3
                        "!!3", "d1(<0.5,1>Drum)(CrossLine)", "d0", "d1",    "!!3", "d0(<0.5,1>Drum)", "d1", "d0",
                        "!!3", "d1(<0.5,1>Drum)", "d0", "d1",    "!!3", "d0(<0.5,1>Drum)", "d1", "d0",
                        "!!3", "d1(<0.5,1>Drum)", "d0", "d1",    "!!3", "d0(<0.5,1>Drum)", "d1", "d0",
                        "!!3", "d1(<0.5,1>Drum)", "d0", "d1",    "!!3", "d0(<0.5,1>Drum)", "d1", "+01",  

                        //4
                        "!!3", "D(<0.5,-1>Drum)", "*+012@A", "*+012@B",    "!!3", "d0(<0.5,-1>Drum)", "*+012@A", "*+012@B",
                        "!!3", "d0(<0.5,-1>Drum)", "*+012@A", "*+012@B",    "!!3", "d0(<0.5,-1>Drum)", "*+012@A", "*+012@B",
                        "!!3", "d0(<0.5,-1>Drum)", "*+012@A", "*+012@B",    "!!3", "d0(<0.5,-1>Drum)", "*+012@A", "*+012@B",
                        "!!3", "d0(<0.5,-1>Drum)", "*+012@A", "*+012@B",    "d0(<0.5,-1>Drum)"
                    });
                }
                if (InBeat(424))
                {
                    float x1 = 0; 
                    float x2 = 0;
                    RegisterFunctionOnce("Value", () =>
                    {                
                        RunEase((s) =>
                        {
                            x1 = s;
                        },
                        EaseOut(BeatTime(32),320,100,EaseState.Back),
                        EaseOut(BeatTime(8), 320, 100, EaseState.Back)
                        );
                        RunEase((s) =>
                        {
                            x2 = s;
                        },
                        EaseOut(BeatTime(32), 320, 540, EaseState.Back)
                        );
                    });
                    RegisterFunctionOnce("Line", () =>
                    {
                        for(int i=0;i<42;i++)
                        {
                            int x = i;
                            DelayBeat(x * 0.66f, () =>
                            {
                                Line l = new(320, 90) { DrawingColor=Color.DarkRed,Width=8};
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                        }
                        for (int i = 0; i < 28; i++)
                        {
                            int x = i;
                            DelayBeat(0.33f+x * 1f, () =>
                            {
                                Line l = new(x2, 90) { DrawingColor = Color.DarkRed, Width = 8 };
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                            DelayBeat(1f + x * 1f, () =>
                            {
                                Line l = new(x1, 90) { DrawingColor = Color.DarkRed, Width = 8 };
                                CreateEntity(l);
                                l.AlphaDecrease(BeatTime(0.66f));
                            });
                        }
                    });
                    RegisterFunctionOnce("KickLine", () =>
                    {
                        Line l = new(LinkEase(Stable(0, new Vector2(320, 240)), EaseOut(BeatTime(2f), new Vector2(20, 0), EaseState.Quart)).Easing, Stable(0, 45).Easing) { Alpha = 0.67f, ObliqueMirror = true, TransverseMirror = true, VerticalMirror = true };
                        Line l2 = new(LinkEase(Stable(0, new Vector2(320, 240)), EaseOut(BeatTime(2f), new Vector2(100, 0), EaseState.Quart)).Easing, Stable(0,45).Easing) { Alpha=0.67f,ObliqueMirror=true,TransverseMirror=true,VerticalMirror=true};
                        CreateEntity(l, l2);
                        l.AlphaDecrease(BeatTime(2));
                        l2.AlphaDecrease(BeatTime(2));
                    });
                    RegisterFunctionOnce("QuickKick", () =>
                    {
                        var ce = LinkEase
                        (Stable(0, new Vector2(320, 240)),
                        EaseOut(BeatTime(1f), new Vector2(20, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(80, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(-20, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(-80, 0), EaseState.Quart),
                        EaseOut(BeatTime(2f), new Vector2(100, 0), EaseState.Quart)
                        ).Easing;
                        var ce2 = LinkEase
                        (Stable(0, new Vector2(320, 240)),
                        EaseOut(BeatTime(1f), new Vector2(80, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(120, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(-80, 0), EaseState.Quart),
                        EaseOut(BeatTime(1f), new Vector2(-120, 0), EaseState.Quart),
                        EaseOut(BeatTime(2f), new Vector2(200, 0), EaseState.Quart)
                        ).Easing;
                        Line l = new(ce, Stable(0, 45).Easing) { Alpha = 0.5f, ObliqueMirror = true, TransverseMirror = true, VerticalMirror = true };
                        Line l2 = new(ce2, Stable(0, 45).Easing) { Alpha = 0.67f, ObliqueMirror = true, TransverseMirror = true, VerticalMirror = true };
                        CreateEntity(l, l2);
                        l.DelayAlphaDecrease(BeatTime(4), BeatTime(2));
                        l2.DelayAlphaDecrease(BeatTime(4), BeatTime(2));
                    });
                    
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",  
                          
                        //1
                        "^d'1.6(^d1'1.6)(Value)(Line)(KickLine)(<1.9,-4>Drum)", "", "", "",    "d", "", "", "",
                        "d(<0>SetScreenAngle)", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",
                        "d", "", "", "",    "d", "", "", "",   

                        //2
                        "^d'1.6(^d1'1.6)(KickLine)(<1.9,4>Drum)", "", "", "",    "d1", "", "", "",
                        "d1(<0>SetScreenAngle)", "", "", "",    "d1", "", "", "",
                        "d1", "", "", "",    "d1", "", "", "",
                        "d1", "", "", "",    "d1", "", "", "",   

                        //3
                        "^d'1.6(^d1'1.6)(KickLine)(<1.9,-4>Drum)", "", "", "",    "d", "", "", "",
                        "d(<0>SetScreenAngle)", "", "", "",    "d", "", "", "",
                        "^d'1.6(^d1'1.6)(KickLine)(<1.9,4>Drum)", "", "", "",    "d1", "", "", "",
                        "d1(<0>SetScreenAngle)", "", "", "",    "d1", "", "", "",

                        //4
                        "^d'1.6(^d1'1.6)(QuickKick)(<0.4,-1>Drum)", "", "", "",    "^d'1.6(^d1'1.6)(<0.4,1>Drum)", "", "", "",
                        "^d'1.6(^d1'1.6)(<0.4,-1>Drum)", "", "", "",    "^d'1.6(^d1'1.6)(<0.4,1>Drum)", "", "", "",
                        "^d'1.6(^d1'1.6)(<1.9,-5>Drum)", "", "", "",    "", "", "", "",
                        "(<0>SetScreenAngle)", "", "", "",    "", "", "", "",
                        
                    });
                }

                if (InBeat(456))
                {
                    RegisterFunctionOnce("KickR", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenPositionDetla = s; },
                            EaseOut(BeatTime(2),new Vector2(-20,20),EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(-10, -40), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(-30, 30), EaseState.Quad),
                            EaseOut(BeatTime(1), new Vector2(-40, 30), EaseState.Quad),
                            EaseOut(BeatTime(1.3f), new Vector2(100, -40), EaseState.Back)
                            );
                    });
                    RegisterFunctionOnce("KickL", () =>
                    {
                        RunEase((s) => { ScreenDrawing.ScreenPositionDetla = s; },
                            EaseOut(BeatTime(2), new Vector2(+20, 20), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(+10, -40), EaseState.Quad),
                            EaseOut(BeatTime(1.5f), new Vector2(+30, 30), EaseState.Quad),
                            EaseOut(BeatTime(1), new Vector2(+40, 30), EaseState.Quad),
                            EaseOut(BeatTime(1.3f), new Vector2(-100, -40), EaseState.Back)
                            );
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",  
                          
                        //1
                        "n21(KickR)", "", "", "",    "", "", "", "",
                        "(n21)", "", "", "",    "", "", "$01", "",
                        "n2", "", "n2", "",    "(n2)($11)", "", "n2", "",
                        "n2($31)", "", "n2", "",    "n2", "", "n2", "",  

                        //2
                        "!!6/3", "($31)($31)(KickL)", "$0", "$1",    "$2", "",    "$0", "", "", "",
                        "#4.6#$0(n01)", "", "", "",     "", "", "$21", "",    
                        "", "", "", "",     "$11", "", "", "",    
                        "(n21)", "", "$2", "",     "", "", "$1", "",

                        //3
                        "(d)(d1)(KickR)", "", "", "",    "n2", "", "n2", "",
                        "n21", "", "n2", "",    "", "", "$1($31)", "",
                        "", "", "n2", "",    "$1($01)", "", "", "",
                        "$3($01)", "", "", "",    "n2", "", "", "",  

                        //4
                        "n0(n01)(KickL)", "", "", "",    "n0(n01)", "", "", "",
                        "n0(n01)", "", "n0", "",    "n0", "", "n0(n01)", "",
                        "", "", "", "",    "n01", "", "", "",
                        "(n01)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(488))
                {
                    RegisterFunctionOnce("ConvL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(Arguments[1], BeatTime(Arguments[0]),true);
                    });
                    RegisterFunctionOnce("ConvR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(Arguments[1], BeatTime(Arguments[0]), false);
                    });
                    RegisterFunctionOnce("KickLine", () =>
                    {
                        var ce = LinkEase(
                            Stable(0,new Vector2(320,240)+GetVector2(40,-30)),
                            EaseOut(BeatTime(2),GetVector2(200,-30),EaseState.Quad)
                            );
                        Line l = new(ce.Easing, Stable(0, -30 + 90).Easing) { ObliqueMirror=true,TransverseMirror=true,VerticalMirror=true};
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(2));
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",  
                          
                        //1
                        "d(d1)(<2.1,6>Drum)(KickLine)", "", "", "",    "d", "", "", "",
                        "d(d1)", "", "d", "",    "", "", "d(d1)", "",
                        "<0>SetScreenAngle", "", "", "",    "d(d1)", "", "", "",
                        "d(d1)", "", "d", "",    "", "", "d", "",  

                        //2
                        "#2.1#d(<6,8>ConvR)", "", "", "",    "", "", "", "",
                        "d1", "", "", "",    "", "", "d1", "",
                        "", "", "", "",    "d1(d)", "", "", "",
                        "d(d1)", "", "d", "",    "", "", "", "",  

                        //3
                        "(#10.5#d)(d1)(<8,8>ConvL)", "", "", "",    "", "", "", "",
                        "d1", "", "", "",    "", "", "d1", "",
                        "", "", "", "",    "d1", "", "", "",
                        "d1", "", "", "",    "", "", "", "",  

                        //4
                        "d1(<8,8>ConvR)", "", "", "",    "", "", "", "",
                        "d1", "", "", "",    "", "", "d1", "",
                        "", "", "d1", "",    "d1", "", "d1", "",
                        "d(d1)", "", "d", "",    "d(d1)", "", "d", "",    "(*^$01'1.7)(*^$21'1.7)"
                    });
                }
            }

            private void Effect01()
            {
                Blur p1 = Blur;
                RegisterFunctionOnce("pre", () => {
                    ScreenDrawing.SceneRendering.InsertProduction(p1);
                    ScreenDrawing.MakeFlicker(Color.Red * 0.75f);

                    float bdis = ScreenDrawing.UpBoundDistance;
                    RunEase(s => {
                        ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance =
                            MathHelper.Lerp(bdis, 210, s);
                        ScreenDrawing.BackGroundColor = Color.Lerp(Color.Red * 0.4f, Color.DarkRed * 0.12f, s);
                        ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.Red * 0.4f, s);
                    }, EaseOut(BeatTime(2), 1, EaseState.Cubic));
                    p1.Sigma = 0.0f;
                    p1.GlitterScale = 0.0f;
                    p1.KawaseMode = true;
                    p1.Glittering = true;
                    ForBeat(72, () => { p1.Sigma *= 0.94f; p1.GlitterScale *= 0.97f; });
                });
                RegisterFunctionOnce("glow", () => {
                    p1.Sigma = 1.0f;
                    p1.GlitterScale = 0.72f;
                    RunEase(s => 
                        ScreenDrawing.MasterAlpha = s, 
                        EaseOut(BeatTime(0.3f), 0.88f, 1.0f, EaseState.Quad)
                    );
                    RunEase(s => splitter.Intensity = s, EaseOut(BeatTime(0.25f), 12f, 0.5f, EaseState.Quad));
                });
                Lighting.Light light = null;
                Lighting lighter = null;
                RegisterFunctionOnce("close", () => {
                    ForBeat(1, () => { p1.Sigma *= 0.9f; p1.GlitterScale *= 0.92f; });
                     
                    RunEase(s => {
                        ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance =
                            MathHelper.Lerp(210, 0, s);
                        ScreenDrawing.BackGroundColor = Color.Lerp(Color.DarkRed * 0.12f, Color.Aqua * 0.04f, s);
                        ScreenDrawing.BoundColor = Color.Lerp(Color.Red * 0.4f, Color.LightPink * 0.8f, s);
                    }, EaseOut(BeatTime(1.5f), 1, EaseState.Cubic));
                    DelayBeat(1, () =>
                        Blur.Sigma = 0.0f);
                    DelayBeat(0.4f, () => {
                        ScreenDrawing.SceneRendering.InsertProduction(lighter = new Lighting(0.943111f));
                        lighter.AmbientColor = Color.White;
                        RunEase(s =>
                        {
                            lighter.AmbientColor = Color.White * s;
                        }, Linear(BeatTime(1.7f), 1.0f, 0.35f));
                        lighter.Lights.Add(light = new()
                        {
                            position = new(320, 240),
                            color = Color.White * 1f,
                            size = 300
                        });
                    });
                });
                RegisterFunctionOnce("returnP", () => {
                    RunEase(s =>
                    {
                        lighter.AmbientColor = Color.White * s;
                    }, Linear(BeatTime(2f), 0.35f, 1.0f));
                    DelayBeat(2, () => {
                        lighter.Dispose();
                    });
                });
                RegisterFunctionOnce("return", () => {
                    RunEase(s => {
                        ScreenDrawing.MasterAlpha = s;
                    }, false, 
                        Linear(BeatTime(0.125f), 0.2f, 1.0f),
                        Linear(BeatTime(0.125f), 0.2f, 1.0f),
                        Linear(BeatTime(0.125f), 0.4f, 1.0f),
                        Linear(BeatTime(0.125f), 0.4f, 1.0f),
                        Linear(BeatTime(0.125f), 0.6f, 1.0f),
                        Linear(BeatTime(0.125f), 0.6f, 1.0f),
                        Linear(BeatTime(0.125f), 0.8f, 1.0f),
                        Linear(BeatTime(0.125f), 0.8f, 1.0f)
                    );
                    RunEase(s => {
                        ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance =
                            MathHelper.Lerp(210, 0, s);
                        ScreenDrawing.BackGroundColor = Color.Lerp(Color.DarkRed * 0.12f, Color.Aqua * 0.04f, s);
                        ScreenDrawing.BoundColor = Color.Lerp(Color.Red * 0.4f, Color.LightPink * 0.8f, s);
                    }, EaseOut(BeatTime(1.5f), 1, 0, EaseState.Cubic));
                });
                RegisterFunctionOnce("shakeL", () => {
                    AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(3, 18, 2f, 180, 180, 0.6f));
                    RunEase(s => ScreenDrawing.ScreenAngle = s, 
                        EaseOut(BeatTime(0.25f), 6.0f, 0.0f, EaseState.Cubic));
                });
                RegisterFunctionOnce("shakeR", () => {
                    AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(3, 18, 2f, 0, 180, 0.6f));
                    RunEase(s => ScreenDrawing.ScreenAngle = s,
                        EaseOut(BeatTime(0.25f), -6.0f, 0.0f, EaseState.Cubic));
                });
                RegisterFunctionOnce("pre2", () => {
                    Shaders.Seismic.Progress = 0;
                    production3 = ScreenDrawing.ActivateShader(Shaders.Seismic, 0.80101f);
                    Blur.Dispose();
                });
                RegisterFunctionOnce("shake", () => {
                    AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(3, 18, 2f, 0, 180, 0.6f));
                    RunEase(s => ScreenDrawing.ScreenAngle = s,
                        EaseOut(BeatTime(0.25f), -6.0f, 0.0f, EaseState.Cubic));
                    RunEase(s => Shaders.Seismic.Progress = s,
                        Linear(BeatTime(1.0f), 0.2f, 0.8f));
                    RunEase(s => Shaders.Seismic.Radius = s,
                        EaseOut(BeatTime(1.0f), 30, 420, EaseState.Circ));

                    RunEase(s => ScreenDrawing.ScreenScale = s, 
                        EaseOut(BeatTime(0.5f), 1.15f, 1.0f, EaseState.Quint));
                });
                RegisterFunctionOnce("shine", () => {
                    RunEase(s => light.size = s,
                        EaseOut(BeatTime(0.6f), 490f, 350f, EaseState.Quad));
                    RunEase(s => lighter.AmbientColor = Color.White * s,
                        EaseOut(BeatTime(0.6f), 1.0f, 0.65f, EaseState.Quad));
                    ScreenDrawing.MakeFlicker(Color.White * 0.41f);
                    RunEase(s => splitter.Intensity = s, EaseOut(BeatTime(0.25f), 7f, 0.5f, EaseState.Quad));
                });
                RegisterFunctionOnce("pre2Dispose", () => {
                    production3.Dispose();
                    production3 = null;
                    lighter.Dispose();
                });
                RegisterFunctionOnce("dim", () => {
                    RunEase(s => ScreenDrawing.MasterAlpha = s, false,
                        Linear(BeatTime(0.25f), 0.4f, 1.0f),
                        Linear(BeatTime(0.25f), 0.4f, 1.0f),
                        Linear(BeatTime(0.25f), 0.3f, 1.0f),
                        Linear(BeatTime(0.25f), 0.3f, 1.0f),
                        Linear(BeatTime(0.25f), 0.2f, 1.0f),
                        Linear(BeatTime(0.25f), 0.2f, 1.0f), 
                        EaseOut(BeatTime(2f), 1.0f, 0.04f, EaseState.Quad),
                        EaseOut(BeatTime(0.21f), 0.04f, 1.0f, EaseState.Quad)
                        );
                });
                BarrageCreate(BeatTime(2), BeatTime(2), 1, new string[] {
                    //pre
                    "", "pre", "", "",    "", "", "", "",   
                    //1
                    "glow(shakeL)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeL)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeL)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeL)", "", "", "",    "glow(shakeR)", "", "glow(shakeL)", "",
                    "glow(shakeR)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeR)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeR)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeR)", "", "", "glow(shakeL)",    "glow(shakeR)", "", "glow(shakeL)", "",
                    //2
                    "close", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "returnP", "", "", "",
                    "", "", "", "",    "return", "", "", "",
                    //3
                    "glow(shakeL)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeL)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeL)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeL)", "", "", "",    "glow(shakeR)", "", "glow(shakeL)", "",
                    "glow(shakeR)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeR)", "", "", "",    "glow(shakeL)", "", "", "",
                    "glow(shakeR)", "", "", "",    "glow(shakeL)", "", "glow(shakeL)", "",
                    "glow(shakeR)", "", "", "glow(shakeL)",    "glow(shakeR)", "", "glow(shakeL)", "",
                    //4
                    "close", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "pre2", "", "", "",    "", "", "", "",
                    "shine(shake)", "", "", "",    "", "", "", "",
                    "shine(shake)", "", "", "",    "", "", "", "",
                    "shine(shake)(dim)", "", "", "",    "", "", "", "",
                    "pre2Dispose", "", "", "",    "", "", "", "",
                });
            }

            public void Normal()
            {

            }
            public void Start()
            {
                RegisterFunction("Drum", () =>
                {
                    ScreenDrawing.ScreenAngle = 0;
                    float time = Arguments[0];
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    },
                    EaseOut(BeatTime(time) / 4f, Arguments[1], EaseState.Quad),
                    EaseIn(BeatTime(time) / 4f * 3, -Arguments[1], EaseState.Quad)
                    );
                    //Delay(time+2, () => { ScreenDrawing.ScreenAngle = 0; });
                });
                RegisterFunction("SetScreenAngle", () =>
                {
                    ScreenDrawing.ScreenAngle = Arguments[0];                   
                });
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
                AddInstance(easeG = new Arrow.UnitEasing()
                {
                    ApplyTime = BeatTime(2.5f),
                    PositionEase = LinkEase(
                        Stable(BeatTime(1.4f), new Vector2(0, -245)),
                        EaseOut(BeatTime(1.1f), new Vector2(0, -245), new Vector2(0, 0), EaseState.Elastic))
                });
                AddInstance(easeH = new Arrow.UnitEasing());
                AddInstance(easeI = new Arrow.UnitEasing());
                AddInstance(easeJ = new Arrow.UnitEasing());
                AddInstance(easeK = new Arrow.UnitEasing());

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
                easeS1 = new();
                AddInstance(easeS1);
                easeT1 = new();
                AddInstance(easeT1);
                easeS2 = new();
                AddInstance(easeS2);
                easeT2 = new();
                AddInstance(easeT2);

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
                CreateEntity(r);
                GametimeDelta = -3.5f;
                SetSoul(1);
                InstantSetBox(new Vector2(320,240), 84, 84);
                InstantTP(320, 240);
                ScreenDrawing.MasterAlpha = 0f;
                ScreenDrawing.ScreenScale = 2f;
                bool jump = true;
                if (jump)
                {
                    //int beat = 192;
                    int beat = 392;
                 //   int beat = 198 ;
                    GametimeDelta = -3.5f + BeatTime(beat);

                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}