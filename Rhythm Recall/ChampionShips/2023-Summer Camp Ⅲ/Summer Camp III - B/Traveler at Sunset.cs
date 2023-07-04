﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
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
                public override string BarrageAuthor => "zKronO vs Tlottgodinf";
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
                public float Speed { set; get; } = 40f;
                public float Length { set; get; } = 300f;
                public float BasicSpeed { set; get; } = 1f;

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
                    if (timer % Intensity < 1) CreateEntity(new Wind(Speed,Length));
                    Speed = 40f* BasicSpeed;
                    Length = 300f*BasicSpeed;
                }
                class Wind : Entity
                {
                    float Speed;
                    float Width = 1.5f;
                    Vector2 point1;
                    Vector2 point2;
                    public Wind(float Speed, float length)
                    {
                        this.Speed = Speed;
                        point1 = new(660, Rand(10, 470));
                        point2 = new(660 + length, LastRand);
                    }
                    float timer = 0;
                    public float Colordepth = Rand(0.300f, 0.500f);
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(point1,point2,Width,Color.White*Colordepth,0.1f);
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
            Winder r = new();
            public void ExtremePlus()
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(114), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { sound = false });
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
                    RegisterFunctionOnce("a", () => { });
                    RegisterFunctionOnce("SoulR", () =>
                    {
                        RunEase(s => Heart.Rotation = -s * 2,
                            EaseOut(BeatTime(0.5f), 0, 1.2f, EaseState.Quart),
                            EaseOut(BeatTime(3.25f), 1.2f, 0, EaseState.Linear),
                            Stable(0, 0));
                    });
                    RegisterFunctionOnce("a", () => { });
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {   //10
                        "$0(LoadA)","+1","+1","+1",    "+1","+1","+1","+1",
                        "+11(LoadB)","+11","+11","+11",   "+11","+11","+11","+11",
                        //11
                        "(*$0'2)(*$2'2)(KickA)","","","",    "","","","",
                        "(D)(+01)","","","",    "","","","",
                        "(D)(+01)","","","",    "","","","",
                        "D","","","",    "D","","","",
                        //12
                        "D","","","",    "","","","",
                        "D1","","","",    "D1","","","",
                        "D1","","","",    "D1","","","",
                        "D1","","","",    "D1","","","",
                        //13
                        "(D)(+21)","","","",    "","","","",
                        "D","","","",    "D","","","",
                        "D","","","",    "D","","","",
                        "D","","","",    "D","","","",
                        //14
                        "D1","","","",    "D1","","","",
                        "D1","","","",    "D1","","","",
                        "D1","","","",    "D1","","","",
                        "D1","","","",    "","","","",
                        //15
                        "(Step)(KickB1)(Bound)(D)(+0)(+21)","","","",    "","","","",
                        "(D)(+0)","","","",    "","","","",
                        "(KickA)(ConvR)(SplitterR)(D)(+0)(+01)","","","",    "","","","",
                        "D","","","",    "D","","","",
                        //16
                        "(Step)(KickB2)(D)(+21)(+01)","","","",    "","","","",
                        "(Step)(D1)(+01)","","","",    "D1","","","",
                        "(KickA)(ConvL)(SplitterL)(D)(+01)(+01)","","","",    "D1","","","",
                        "D1","","","",    "D1","","","",
                        //17
                        "(Step)(KickB1)(D)(+21)","","","",    "","","","",
                        "","","","",    "D","","","",
                        "(KickA)(ConvR)(SplitterR)(D)(+0)(+01)","","","",    "D","","","",
                        "D","","","",    "D","","","",
                        //18
                        "(Step)(KickB2)(#7.75#$0)(+2)(SoulR)","","","",    "","","","",
                        "(Step)","","","",    "","","","",
                        "(KickA)(ConvL)(SplitterL)(#3.75#$21)(+21)(SoulL)","","","",    "","","","",
                        "","","","",    "","","","",
                    });
                }
                if (InBeat(72+68))
                {
                    /*
                    RegisterFunctionOnce("a", () =>
                    {
                    });
                    */
                    BarrageCreate(BeatTime(4), BeatTime(2), 7, new string[]
                    {
                        //19
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //20
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //21
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //22
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //23
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //24
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //25
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        //26
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                        "","","","",    "","","","",
                    });
                }
            }
            public void Normal()
            {

            }
            public void Start()
            {
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
                bool jump = false;
                if (jump)
                {
                    GametimeDelta = -1.5f + BeatTime(72);
                    PlayOffset = BeatTime(72);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}