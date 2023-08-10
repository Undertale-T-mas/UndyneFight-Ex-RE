using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class HopesAndDreams : IChampionShip
    {
        public HopesAndDreams()
        {
            Game.game = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));
            difficulties = new();
            difficulties.Add("div.2", Difficulty.Noob);
            difficulties.Add("div.1", Difficulty.Normal);
            difficulties.Add("div.0", Difficulty.Extreme);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public void ExtremePlus()
            {
                throw new System.NotImplementedException();
            }

            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 4f),
                            new(Difficulty.Normal, 12f),
                            new(Difficulty.Extreme, 16.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 4f),
                            new(Difficulty.Normal, 12f),
                            new(Difficulty.Extreme, 16.9f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 9f),
                            new(Difficulty.Normal, 15.5f),
                            new(Difficulty.Extreme, 20.9f),
                        }
                    );
                public override string BarrageAuthor => "Tlottgodinf";
                public override string AttributeAuthor => "Tlottgodinf";
                public override string PaintAuthor => "Undertale - Toby Fox";
                public override string SongAuthor => "Toby Fox";
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(62.5f / (180f / 60f)) { }

            public static Game game;

            public string Music => "SAVE(Hopes and Dreams)";
            //  public string Music => "Brain Power";
            public string FightName => "SAVE(Hopes and Dreams)";

            private static class NormalBarrage { }
            Texture2D image;
            private class Image : Entity
            {
                private readonly float fadeTime;
                public Image(Texture2D tex, float fadeTime)
                {
                    this.fadeTime = fadeTime;
                    Image = tex;
                    UpdateIn120 = true;
                }

                public override void Draw()
                {
                    Depth = 0.99f;
                    FormalDraw(Image, new(320, 240), Color.White * alpha, 0.9f, 0, new(320, 240));
                }

                private float alpha = 0;
                public float FadeSpeed { private get; set; } = 0.34f;

                private float time = 0;
                public override void Update()
                {
                    time += 0.5f;
                    if (time > fadeTime)
                    {
                        alpha -= FadeSpeed * 0.12f;
                        if (alpha < 0) Dispose();
                    }
                    else if (alpha < 0.9f)
                    {
                        alpha += 0.06f;
                    }
                }
            }
            public void Noob()
            {
                if (InBeat(0)) nb1();
                if (InBeat(64)) nb2();
                if (InBeat(128)) nb3();
                if (InBeat(192)) nb4();
                if (InBeat(320 - 4)) nb5();
                if (InBeat(384)) nb6();
                if (InBeat(444)) nb7();
            }
            public void Hard()
            {
                if (Gametime < 0) return;
            }
            public void Easy()
            {
                if (Gametime < 0) return;
            }

            public void Normal()
            {
                if (InBeat(0)) nr1();
                if (InBeat(64)) nr2();
                if (InBeat(128)) nr3();
                if (InBeat(192)) nr4();
                if (InBeat(320 - 4)) nr5();
                if (InBeat(384)) nr6();
                if (InBeat(444)) nr7();
            }
            private static class ExtremeBarrage
            {

            }
            /// <summary>
            /// 便携的谱面创建
            /// </summary>
            /// <param name="Delay">延迟时间，一般用来让箭头不闪现入场</param>
            /// <param name="Beat">拍号，如果写BeatTime(1)即为每个字符串占一个32分的长度</param>
            /// <param name="arrowspeed">普遍的箭头速度</param>
            /// <param name="Barrage">谱面内容，即字符串数组</param>
            public void LineShadow(int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * 0.5f, 0.24f - 0.24f / times * t));

                }
            }
            public void LineShadow(float deep, int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * 0.5f, deep - deep / times * t));

                }
            }
            public void LineShadow(float delay, float deep, int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * delay, deep - deep / times * t));

                }
            }
            public void Extreme()
            {
                //    CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { sound = false });
                //      CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Stars:" + "$" + (GetAll<Extends.Star>().Length).ToString(), new(0, 240 - 20), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { sound = false });
                if (InBeat(0)) ex1();
                if (InBeat(64)) ex2();
                if (InBeat(128)) ex3();
                if (InBeat(192)) ex4();
                if (InBeat(320 - 4)) ex5();
                if (InBeat(384)) ex6();
                if (InBeat(444)) ex7();

            }
            void ex1()
            {

                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(0, 120, BeatTime(4)), ValueEasing.EaseOutSine(0, 180, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(120, 280, BeatTime(4)), ValueEasing.EaseOutQuad(180, 160, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(280, 260, BeatTime(4)), ValueEasing.EaseOutSine(160, 300, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(260, 320, BeatTime(4)), ValueEasing.EaseOutQuad(300, 360, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(320, 420, BeatTime(4)), ValueEasing.EaseOutQuad(360, 160, BeatTime(4))));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0.5f), new(0, 0.15f)));
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(0, ValueEasing.Stable(90));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(90, 90 + 60, BeatTime(4)));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(90 + 60, 40, (BeatTime(4))));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(40, 130, BeatTime(4)));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(130, 160, (BeatTime(4))));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(160, 180 + 45, (BeatTime(4))));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(0, 60, BeatTime(4)));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(60, 40 - 90, (BeatTime(4))));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(40 - 90, 130 - 90, BeatTime(4)));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(130 - 90, 160 - 90, (BeatTime(4))));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(160 - 90, 90 + 45, (BeatTime(4))));
                    Line l1 = new(ce.GetResult(), ve1.GetResult()) { Alpha = 0.65f };
                    Line l2 = new(ce.GetResult(), ve2.GetResult()) { Alpha = 0.65f };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    LineShadow(1.5f, 0.4f, 8, l1);
                    LineShadow(1.5f, 0.4f, 8, l2);

                    //l1.ObliqueMirror = true;
                    //l2.ObliqueMirror = true;
                });
                RegisterFunctionOnce("star1", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(4), CentreEasing.LerpTo(new(320, -50), 0.0295f, new Vector2(320, 100)));
                    Extends.Star s = new(new Vector2(320, -50), 0.8f) { rotatespeed = 0.5f };
                    e.Run((m) => { s.Centre = new Vector2(0, 0) + m; });
                    s.rotatespeed = 1.8f;
                    CreateEntity(s);
                    DelayBeat(4, () => { s.Dispose(); });
                    ForBeat(4, () => { s.rotatespeed -= 0.005f; });
                });
                RegisterFunctionOnce("star2", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.40f, 0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s = new(e.GetResult(), 0.6f) { rotate = false };
                    CreateEntity(s);
                    e.Run((m) => { s.Centre = new Vector2(320, 100) + m; });
                    s.rotatespeed = 0;
                    ForBeat(16, () =>
                    {
                        s.rotatespeed += 0.1f;
                    });

                });
                RegisterFunctionOnce("star3", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.10f, 0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s = new(new Vector2(320, 100), 0.6f) { rotate = false };
                    CreateEntity(s);
                    e.Run((m) => { s.Centre = new Vector2(320, 100) + m; });
                    CentreEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.10f, -0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s1 = new(new Vector2(320, 100), 0.6f) { rotate = false };
                    CreateEntity(s1);
                    e1.Run((m) => { s1.Centre = new Vector2(320, 100) + m; });
                    float add = 0;
                    s.rotatespeed = 0;
                    s1.rotatespeed = 0;
                    ForBeat(16, () =>
                    {
                        add += 0.1f;
                        s.rotatespeed -= 0.1f;
                        s1.rotatespeed += 0.1f;
                    });
                });
                RegisterFunctionOnce("fireball1", () =>
                {
                    if (Rand(0, 1) == 0)
                    {

                        CentreEasing.EaseBuilder e = new();
                        e.Insert(0, CentreEasing.Stable(Rand(0, BoxStates.Left - 5), -10));
                        e.Insert(BeatTime(16), CentreEasing.Linear(new Vector2(0, 4.5f)));
                        Fireball f = new(e.GetResult(), 1f);
                        CreateEntity(f);
                        e.Run((m) => { f.Centre = m; });
                        ForBeat(4, () =>
                        {
                            f.Alpha -= 0.02f;
                        });
                    }
                    else if (LastRand == 1)
                    {
                        CentreEasing.EaseBuilder e = new();
                        e.Insert(0, CentreEasing.Stable(Rand(BoxStates.Right + 5, 480), -10));
                        e.Insert(BeatTime(16), CentreEasing.Linear(new Vector2(0, 4.5f)));
                        Fireball f = new(e.GetResult(), 1f);
                        CreateEntity(f);
                        e.Run((m) => { f.Centre = m; });
                        ForBeat(4, () => { f.Alpha -= 0.02f; });
                    }
                });
                RegisterFunctionOnce("line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 500));
                    ce.Insert(BeatTime(8), CentreEasing.Linear(new Vector2(0, -1f)));
                    ValueEasing.EaseBuilder re = new();
                    re.Insert(BeatTime(6), ValueEasing.EaseOutSine(0, 30, BeatTime(6)));
                    ValueEasing.EaseBuilder re2 = new();
                    re2.Insert(BeatTime(6), ValueEasing.EaseOutSine(0, -30, BeatTime(6)));
                    Line l = new(ce.GetResult(), re.GetResult()) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(ce.GetResult(), re2.GetResult()) { DrawingColor = Color.CornflowerBlue };
                    l.AlphaDecrease(BeatTime(5.7f));
                    l2.AlphaDecrease(BeatTime(5.7f));
                    CreateEntity(l);
                    CreateEntity(l2);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                        l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                    });
                });
                RegisterFunctionOnce("line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 0), new(320, BoxStates.Up), BeatTime(8)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 480));
                    ce2.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 480), new(320, BoxStates.Down), BeatTime(8)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(0)) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(0)) { DrawingColor = Color.CornflowerBlue };
                    l.AlphaDecrease(BeatTime(4f));
                    l2.AlphaDecrease(BeatTime(4f));
                    CreateEntity(l);
                    CreateEntity(l2);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                        l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                    });

                });
                RegisterFunctionOnce("line3", () =>
                {
                    ValueEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(2), ValueEasing.EaseOutQuad(30, 160, BeatTime(2)));
                    e.Insert(BeatTime(4), ValueEasing.EaseOutQuart(160, 180 + 45, BeatTime(4)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.Stable(320, 240));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, 0), new(0, 0.12f)));
                    Line l = new(ce.GetResult(), e.GetResult()) { Alpha = 0.75f };
                    l.InsertRetention(new Line.RetentionEffect(0.5f, 0.5f));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(4f)); });
                    CreateEntity(l);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);

                    });
                    for (int i = 0; i < 12; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.02f));

                    }
                });
                RegisterFunctionOnce("line4", () =>
                {
                    ValueEasing.EaseBuilder e = new();

                    e.Insert(BeatTime(4), ValueEasing.EaseOutQuart(160 + 90, 270 + 45, BeatTime(4)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 240));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, 0), new(0, 0.12f)));
                    Line l = new(ce.GetResult(), e.GetResult()) { Alpha = 0.75f };
                    for (int i = 0; i < 12; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.02f));

                    }
                    l.AlphaDecrease(BeatTime(4f));
                    CreateEntity(l);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);

                    });

                });
                RegisterFunctionOnce("line5", () =>
                {
                    Color[] c =
                    {
                    Color.Red,Color.Orange,Color.Yellow,Color.Green,Color.Cyan,Color.Blue,Color.Purple,Color.White
                    };
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        DelayBeat(i * 0.5f, () =>
                        {

                            ValueEasing.EaseBuilder e = new();
                            e.Insert(BeatTime(0), ValueEasing.Stable(90));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(BeatTime(0), CentreEasing.Stable(200 + 600 / 9 * (x), 240));
                            ce.Insert(BeatTime(6), CentreEasing.Linear(-6f));
                            Line l = new(ce.GetResult(), e.GetResult()) { DrawingColor = c[x], Alpha = 0.5f };
                            l.InsertRetention(new Line.RetentionEffect(0.5f, 0.5f));
                            l.AlphaDecrease(BeatTime(5f));
                            CreateEntity(l);
                            ForBeat(8, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.05f);

                            });
                            for (int i = 0; i < 24; i++)
                            {
                                int t = i;
                                l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.01f));

                            }
                        });
                    }


                });
                RegisterFunctionOnce("star4", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.10f, 0.41f), -10), new(0, 0.17f)));
                    Extends.Star s = new(new Vector2(0, 0), 0.6f) { rotate = false };
                    CreateEntity(s);
                    int a = Rand(-60, -20);
                    int b = Rand(20, 60);
                    e.Run((m) => { s.Centre = new Vector2(320 + a, 510) + m; });
                    CentreEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.41f, -0.10f), -10), new(0, 0.17f)));
                    Extends.Star s1 = new(new Vector2(0, 0), 0.6f) { rotate = false };
                    CreateEntity(s1);
                    e1.Run((m) => { s1.Centre = new Vector2(320 + b, 510) + m; });

                    s.rotatespeed = 0;
                    s1.rotatespeed = 0;
                    ForBeat(16, () =>
                    {

                        s.rotatespeed -= 0.07f;
                        s1.rotatespeed += 0.07f;
                    });

                });
                RegisterFunctionOnce("return", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });

                RegisterFunctionOnce("RotateL", () =>
                {
                    ScreenDrawing.CameraEffect.RotateTo(-2.3f, BeatTime(4f));
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    ScreenDrawing.CameraEffect.RotateTo(2.3f, BeatTime(4f));
                });
                RegisterFunctionOnce("RotateReturn", () =>
                     {
                         ScreenDrawing.CameraEffect.RotateTo(0f, BeatTime(4f));
                     });
                RegisterFunctionOnce("BoxSin", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(240));
                    ve.Insert(BeatTime(2f), ValueEasing.EaseOutQuad(240, 210, BeatTime(2f)));
                    ve.Insert(BeatTime(22), ValueEasing.SinWave(23, BeatTime(8), 0.5f));
                    ve.Insert(BeatTime(2f), ValueEasing.EaseOutQuad(210, 260, BeatTime(2f)));
                    float b = 0;
                    ve.Run((m) => { b = m; });
                    ForBeat(28, () => { InstantSetBox(b, 84, 84); InstantTP(320, b); });

                });
                RegisterFunctionOnce("WhiteShake", () =>
                 {
                     for (int i = 0; i < 8; i++)
                     {
                         int x = i;
                         DelayBeat(i * 0.5f, () =>
                         {
                             Color[] c =
                             {
                            Color.Red,
                            Color.Orange,
                            Color.Yellow,
                            Color.Green,
                            Color.Cyan,
                            Color.Blue,
                            Color.Purple,
                            Color.White
                             };
                             ScreenDrawing.MakeFlicker(c[x] * 0.35f);
                         });
                     }
                 });
                RegisterFunctionOnce("Effect1", () =>
                {
                    Color[] c =
                    {
                    Color.Red,Color.Orange,Color.Yellow,Color.Green,Color.Cyan,Color.Blue,Color.Purple,Color.White
                    };
                    ValueEasing.EaseBuilder e = new();
                    ValueEasing.EaseBuilder e2 = new();
                    ValueEasing.EaseBuilder e3 = new();
                    e.Insert(BeatTime(1), ValueEasing.LerpTo(1, 1.1f, 0.04f));
                    e.Run((m) => { ScreenDrawing.ScreenScale = m; });
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        DelayBeat(i * 0.5f, () =>
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(90));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(0, 0));
                            ce.Insert(BeatTime(1.5f), CentreEasing.LerpTo(new Vector2(0, 0), 0.06f, new Vector2(760, 0)));
                            Line l = new(ce.GetResult(), ve.GetResult()) { DrawingColor = c[x] };
                            CreateEntity(l);
                            ScreenDrawing.CameraEffect.Convulse(10, true);
                            for (int i = 0; i < 3; i++)
                            {
                                int t = i;
                                l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.06f));

                            }


                        });
                    }
                });
                BarrageCreate(0, BeatTime(1), 6, new string[]
                {
                //
                "star1(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star2(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star3","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "star1(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star3(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star2","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "star4(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star4(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star4","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "(star4)","","","",   "","","","",

                "line2(return)(star4)","","","",   "","","","",
                "","","","",   "","","","",
                "$31(line3)","","","",   "","","","",
                "","","","",   "","","","",

                "$31($0)(line4)(line5)","","","",   "$1","","","",
                "$2","","","",   "$1","","","",
                "$2","","","",   "$2","","","",
                "$3","","","",   "$3","","","",
                //
                "(^$31'1.5)N3(RotateL)(BoxSin)(Line1)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(^$31'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$11'1.5)N3(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "(^$11'1.5)N3(RotateL)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(^$11'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$31'1.5)N3(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(^$01'1.5)N3(RotateL)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(+0)(^$01'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$21'1.5)N3(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "+0","","","",   "","","","",
                "(^N31'1.5)(^N3'1.5)","","","",   "","","","",

                "(^N31'1.5)(^N3'1.5)(RotateReturn)","","","",   "","","","",
                "","","","",   "","","","",
                "(^N31'1.5)(^N3'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$1'1.5)(WhiteShake)(Effect1)","","","",  "(^$21'1.5)(^+2'1.5)","","","",
                "(^$31'1.5)(^+2'1.5)","","","",   "(^$31'1.5)(^+2'1.5)","","","",
                "(^$11'1.5)(^+2'1.5)","","","",   "(^$11'1.5)(^+2'1.5)","","","",
                "(^$01'1.5)(^+2'1.5)","","","",   "(^$01'1.5)(^+2'1.5)","","","",
                    //
                });
            }
            void ex2()
            {
                RegisterFunctionOnce("SinLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(70, 0));
                    ce.Insert(BeatTime(64), CentreEasing.XSinWave(30, BeatTime(6), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(64), ValueEasing.Linear(0, 360, BeatTime(8)));
                    ve.Run((m) => { l.DrawingColor = new Color(DrawingLab.HsvToRgb(m, 255, 255, 0)); });
                    l.TransverseMirror = true;
                    for (int i = 0; i < 48; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.005f));

                    }

                    DelayBeat(60, () => { l.AlphaDecrease(BeatTime(4)); });
                    ValueEasing.EaseBuilder box = new();
                    box.Insert(0, ValueEasing.Stable(200));
                    box.Insert(BeatTime(60), ValueEasing.SinWave(8, BeatTime(4), 0.5f));
                    box.Run((m) => { InstantSetBox(240, m, 488); });
                    ValueEasing.EaseBuilder down = new();
                    down.Insert(0, ValueEasing.Stable(100));
                    down.Insert(BeatTime(57) - 4, ValueEasing.SinWave(70, BeatTime(8), 0f));
                    down.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                    ScreenDrawing.BoundColor = Color.White * 0.23f;
                    ForBeat(60, () =>
                    {

                        if ((int)GametimeF % 60 == 0)
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(8));
                            ve.Insert(BeatTime(4), ValueEasing.EaseInSine(8, 12, BeatTime(4)));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(320, 560));
                            ce.Insert(BeatTime(16), CentreEasing.Accerlating(new(0, 0f), new(0, -0.175f)));
                            Line l2 = new(ce.GetResult(), ve.GetResult());
                            CreateEntity(l2);
                            l2.DrawingColor = Color.CornflowerBlue;
                            l2.Width = 2.7f;
                            ForBeat(6, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                                l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                            });
                            LineShadow(2.5f, 0.75f, 6, l2);
                            l2.Alpha = 0.5f;
                        }
                        if ((int)GametimeF % 60 == 30)
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(-8));
                            ve.Insert(BeatTime(4), ValueEasing.EaseInSine(-8, -12, BeatTime(4)));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(320, 560));
                            ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0f), new(0, -0.175f)));
                            Line l2 = new(ce.GetResult(), ve.GetResult());
                            CreateEntity(l2);
                            l2.DrawingColor = Color.CornflowerBlue;
                            l2.Width = 2.7f;
                            ForBeat(6, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                                l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                            });
                            LineShadow(2.5f, 0.75f, 4, l2);
                            l2.Alpha = 0.5f;
                        }
                    });
                });
                RegisterFunctionOnce("Light", () =>
                {
                    ScreenDrawing.WhiteOut(BeatTime(2));
                    SetBox(240, 200, 480);
                    SetSoul(0);
                    TP();
                    Extends.DrawingUtil.LerpScreenScale(60, 1, 0.09f);
                    CentreEasing.EaseBuilder ce = new();
                    Vector2 Value = ScreenDrawing.HPBar.AreaOccupied.GetCentre();
                    ce.Insert(0, CentreEasing.Stable(ScreenDrawing.HPBar.AreaOccupied.GetCentre()));
                    ce.Insert(0, CentreEasing.EaseOutQuad(Value, new(20, 480 - 60), BeatTime(2)));
                    ce.Run((m) => { ScreenDrawing.HPBar.AreaOccupied = new(m, ScreenDrawing.HPBar.AreaOccupied.Size); });
                });
                RegisterFunction("BHStar", () =>
                {
                    int max = 8;
                    int RotateRand = Rand(0, 359);
                    for (int a = 0; a < max; a++)
                    {
                        int value = a;
                        Star s = new(new Vector2(0, 0), 0.5f) { ColorType = a % 2 + 1 };
                        CreateEntity(s);
                        CentreEasing.EaseBuilder ce = new();
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(0, ValueEasing.Stable(360));
                        ve1.Insert(BeatTime(12), ValueEasing.Linear(360, -360, BeatTime(6)));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(0, ValueEasing.Stable(RotateRand + 360 / max * value));
                        ve2.Insert(BeatTime(16), ValueEasing.Linear(0, 1080 / 2, BeatTime(16)));
                        float valueN = 0;
                        ve1.Run((m) =>
                        {

                            s.Centre = new Vector2(320, 240) + new Vector2(Cos(valueN) * m, Sin(valueN) * m);

                        });
                        ve2.Run((n) =>
                        {
                            valueN = n;
                        });
                    }
                });
                int k = 0;
                RegisterFunctionOnce("Fireball1", () =>
                {
                    int val = Rand(-1, 1);
                    while (val == k)
                    {
                        val = Rand(-1, 1);
                    }
                    k = val;
                    float ct = k * 66 + 320;
                    float delta = Rand(0, 1f);
                    for (int a = 0; a < 16; a++)
                    {
                        int value = a;
                        Fireball f = new(new Vector2(0, 0), 1);
                        CreateEntity(f);
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(BeatTime(32), ValueEasing.SinWave(35, -BeatTime(4), 1f / 12f * value + delta));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(BeatTime(32), ValueEasing.Linear(-3.6f));
                        float valueN = 0;
                        ve1.Run((m) =>
                        {
                            f.Centre = new(ct + m, 488 + 13 * value + valueN);
                        });
                        ve2.Run((n) =>
                        {
                            valueN = n;
                        });
                        Fireball f2 = new(new Vector2(0, 0), 1);
                        CreateEntity(f2);
                        ValueEasing.EaseBuilder ve3 = new();
                        ve3.Insert(BeatTime(32), ValueEasing.SinWave(35, -BeatTime(4), 1f / 12f * value + delta));
                        ValueEasing.EaseBuilder ve4 = new();
                        ve4.Insert(BeatTime(32), ValueEasing.Linear(-3.6f));
                        float valueN2 = 0;
                        ve3.Run((m) =>
                        {
                            f2.Centre = new(ct - m, 488 + 13 * value + valueN2);
                        });
                        ve4.Run((n) =>
                        {
                            valueN2 = n;
                        });
                    }
                });


                RegisterFunctionOnce("Over", () =>
                {
                    SetSoul(1);
                    SetBox(240, 84, 84);
                    ScreenDrawing.WhiteOut(BeatTime(4));
                    TP();
                    ValueEasing.EaseBuilder down = new();
                    down.Insert(BeatTime(1), ValueEasing.LerpTo(100 + 35, 0, 0.1f));
                    down.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                });
                BarrageCreate(0, BeatTime(1), 6, new string[]
                {
                "(Light)","","","",  "","","","",
                "","","","",  "","","","",
                "(SinLine)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Over)","","","",  "","","","",
                "","","","",  "","","","",
                "","","","",  "","","","",
                "","","","",  "","","","",
                    //
                });
            }
            void ex3()
            {
                Player.Heart heart1 = Heart;
                Player.Heart heart2 = CreateHeart(new CollideRect(380, 180, 84, 84));
                RegisterFunctionOnce("Start", () =>
                {


                    SetPlayerBoxMission(heart1);
                    SetSoul(1);

                    TP(320 - 60, 240 - 180 + 240);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(30));
                    for (int a = 0; a < 8; a++)
                        ve.Insert(BeatTime(4), ValueEasing.EaseOutElastic(30 + a * 30, 30 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve.Insert(BeatTime(8), ValueEasing.EaseOutBack(30 + a * 30, 30 + (a + 1) * 30, BeatTime(8)));
                    ve.Run((m) => { SetPlayerBoxMission(heart1); InstantTP(new Vector2(320, 240) + GetVector2(85, m)); InstantSetBox(new Vector2(320, 240) + GetVector2(85, m), 84, 84); });
                    SetPlayerBoxMission(heart2);
                    SetSoul(1);
                    TP(320 - 60, 240 - 180 + 240);
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(0, ValueEasing.Stable(30));
                    for (int a = 0; a < 8; a++)
                        ve2.Insert(BeatTime(4), ValueEasing.EaseOutElastic(30 + a * 30, 30 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve2.Insert(BeatTime(8), ValueEasing.EaseOutBack(30 + a * 30, 30 + (a + 1) * 30, BeatTime(8)));
                    ve2.Run((m) => { SetPlayerBoxMission(heart2); InstantTP(new Vector2(320, 240) + GetVector2(85, 180 + m)); InstantSetBox(new Vector2(320, 240) + GetVector2(85, 180 + m), 84, 84); });
                    DelayBeat(0, () =>
                     {
                         heart1.Shields.RemoveShield(heart1.Shields.BShield);
                         heart2.Shields.RemoveShield(heart2.Shields.RShield);
                     });
                });
                RegisterFunctionOnce("CentreLine", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(75));
                    for (int a = 0; a < 8; a++)
                        ve.Insert(BeatTime(4), ValueEasing.EaseOutElastic(75 + 30 * a, 75 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve.Insert(BeatTime(8), ValueEasing.EaseOutBack(75 + 30 * a, 75 + (a + 1) * 30, BeatTime(8)));
                    Line l = new(CentreEasing.Stable(320, 240), ve.GetResult());
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(128), ValueEasing.Linear(0, 360, BeatTime(8)));
                    ve2.Run((m) => { l.DrawingColor = new Color(DrawingLab.HsvToRgb(m, 255, 255, 0)); });
                    LineShadow(0.5f, 8, l);
                    CreateEntity(l);
                    DelayBeat(60, () => { l.AlphaDecrease(BeatTime(4)); });
                });
                RegisterFunctionOnce("Screen1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(0, 0), new(-18, 4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(-18, 4), new(18, 4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(18, 4), new(18, -4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen4", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(18, -4), new(-18, -4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("ScreenOver", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(-14, -4), new(0, 0), BeatTime(2)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Return", () =>
                {

                    SetPlayerBoxMission(heart2);
                    Vector2 boxc2 = BoxStates.Centre;
                    SetPlayerBoxMission(heart1);
                    Vector2 boxc = BoxStates.Centre;
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1) - 2, CentreEasing.Alternate(2, new Func<ICustomMotion, Vector2>[]
                    {
                    CentreEasing.Linear(boxc,boxc2,2),
                    CentreEasing.Linear(boxc2,boxc,2)
                    }));
                    ce.Run((m) => { SetPlayerBoxMission(heart1); InstantSetBox(m, 84, 84); InstantTP(m); });
                    DelayBeat(1, () => { ScreenDrawing.MakeFlicker(Color.White); });
                    heart2.Merge(heart1);
                    SetPlayerBoxMission(heart1);
                    heart1.Shields.AddShield(heart1.Shields.BShield);
                    heart1.Shields.AddShield(heart1.Shields.RShield);
                    SetSoul(1);
                    ForBeat(1.125f, 5, () =>
                    {
                        SetPlayerBoxMission(heart1);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        InstantTP(320, 240);
                    });

                });
                RegisterFunctionOnce("Merge", () =>
                {
                    heart2.Merge(heart1);
                    SetPlayerBoxMission(heart1);
                    heart1.Shields.AddShield(heart1.Shields.BShield);
                    heart1.Shields.AddShield(heart1.Shields.RShield);
                    SetSoul(1);
                    DelayBeat(1f, () =>
                    {
                        SetBox(new Vector2(320, 240), 84, 84);
                        TP(320, 240);
                    });

                });
                RegisterFunctionOnce("WhiteBound1", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.DownBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 130, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(130, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                });
                RegisterFunctionOnce("WhiteBound2", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.RightBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 200, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(200, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.RightBoundDistance = m; });
                });
                RegisterFunctionOnce("WhiteBound3", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.LeftBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 200, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(200, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.LeftBoundDistance = m; });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(0, 240));
                    ce1.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(0, 240), new(320 + 120, 240), BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1.75f));
                    LineShadow(2.3f, 0.75f, 4, l1);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 240));
                    ce1.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(640, 240), new(320 - 120, 240), BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1.75f));
                    LineShadow(2.3f, 0.75f, 4, l1);
                });
                SetPlayerBoxMission(heart1);
                BarrageCreate(0, BeatTime(1), 5, new string[]
                {
                "Start(CentreLine)",""
                });
                SetPlayerBoxMission(heart2);
                BarrageCreate(0, BeatTime(1), 4.5f, new string[]
                {
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "N1","","","",   "","","","",
                "N1","","","",   "","","","",
                "","","","",   "","","","",
                "N1","","","",   "","","","",

                "N1","","","",   "","","","",
                "N1","","","",   "N1","","","",
                "","","","",   "","","","",
                "N1","","","",   "","","","",

                "N1","","","",   "","","","",
                "N1","","","",   "","","","",
                "","","","",   "N1","","","",
                "N1","","","",   "","","","",
                //
                "","","","",   "","","","",
                "N2","","","",   "","","","",
                "","","","",   "","","","",
                "N2","","","",   "","","","",

                "","","","",   "","","","",
                "N2","","","",   "","","","",
                "","","","",   "","","","",
                "N2","","","",   "","","","",

                "","","","",   "","","","",
                "N2","","","",   "N2","","","",
                "","","","",   "","","","",
                "N2","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "N3","","","",
                "","","","",   "","","","",
                //
                "#7#$0(Screen1)(RRotate)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "(Screen2)(LRotate)","","","",   "","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "","","","",

                "N0","","","",   "N0","","","",
                "N0","","","",   "","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "","","","",
                //
                "#7#$1(Screen3)(LRotate)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "(Screen4)(RRotate)","","","",   "","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "","","","",

                "N0","","","",   "","","","",
                "N0(BHStar)","","","",   "","","","",
                "N0(ScreenOver)(MidRotate)","","","",   "N0","","","",
                "N0(Return)","","","",   "","","","",

                ""
                });
                SetPlayerBoxMission(heart1);
                BarrageCreate(0, BeatTime(1), 4.5f, new string[]
                {
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "N31(LRotate)(Line1)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "N31(RRotate)(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "N31(LRotate)(Line1)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "N01","","","",   "","","","",
                "","","","",   "","","","",
                "N01(RRotate)(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "N01","","","",   "","","","",
                "","","","",   "","","","",
                "N01(LRotate)(Line1)","","","",   "","","","",
                "","","","",   "","","","",

                "N01","","","",   "","","","",
                "","","","",   "","","","",
                "N01(RRotate)(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "N11","","","",   "","","","",
                "N11","","","",   "","","","",
                "N11(MidRotate)(Line1)","","","",   "","","","",
                "N11","","","",   "","","","",
                //
                "(WhiteBound1)","","","",   "","","","",
                "N11","","","",   "","","","",
                "N11(Line2)","","","",   "","","","",
                "N11","","","",   "","","","",

                "N11","","","",   "","","","",
                "N11","","","",   "","","","",
                "N11(Line1)","","","",   "","","","",
                "N11","","","",   "","","","",

                "#7#$11(WhiteBound2)","","","",   "","","","",
                "","","","",   "","","","",
                "(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(Line1)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(WhiteBound3)","","","",   "","","","",
                "N21","","","",   "","","","",
                "N21(Line2)","","","",   "","","","",
                "N21","","","",   "","","","",

                "N21","","","",   "","","","",
                "N21","","","",   "","","","",
                "N21(Line1)","","","",   "","","","",
                "N21","","","",   "","","","",

                "#7#$31(WhiteBound2)","","","",   "","","","",
                "","","","",   "","","","",
                "(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    //
                });
            }
            void ex4()
            {
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(0, 240));
                    ce1.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new Vector2(0, 240), new Vector2(640, 240), BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.7f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1f));
                    LineShadow(0.7f, 5, l1);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 240));
                    ce1.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new Vector2(640, 240), new Vector2(0, 240), BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.7f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1f));
                    LineShadow(0.7f, 5, l1);
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320 + 20, 480));
                    ce.Insert(BeatTime(4), CentreEasing.LerpTo(new Vector2(320 + 20, 480), 0.07f, new Vector2(320 + 20 + 90, 480)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 240 - 15));
                    ce2.Insert(BeatTime(4), CentreEasing.LerpTo(new Vector2(0, 240 - 15), 0.07f, new Vector2(0, 240 - 15 - 90f / 4f * 3f)));
                    Line l1 = new(ce.GetResult(), ce2.GetResult()) { Alpha = 0.8f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(0.78f));
                    l1.ObliqueMirror = true;
                    l1.TransverseMirror = true;
                    l1.VerticalMirror = true;
                });
                RegisterFunctionOnce("Line4", () =>
                {
                    Line l1 = new(CentreEasing.Stable(Rand(0, 640), 240), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(2));
                });
                RegisterFunctionOnce("Camera1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6.9f, BeatTime(0.78f), true);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.SinWave(0.01f, BeatTime(1f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6.9f, BeatTime(0.78f), false);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.SinWave(0.01f, BeatTime(1f), 0.5f));
                    // ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera3", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(10, BeatTime(2f), false);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.8f), ValueEasing.SinWave(0.03f, BeatTime(1.8f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera4", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(10, BeatTime(2f), true);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.8f), ValueEasing.SinWave(0.03f, BeatTime(1.8f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                SetPlayerBoxMission(0);
                BarrageCreate(BeatTime(0), BeatTime(1), 5.4f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                "($0)($21)","","","",   "","","","",
                "($2)($01)","","","",   "","","","",

                "(R)(+21)","","","",   "(R)(+21)","","","",
                "","","","",   "(R)(+21)","","","",
                "","","","",   "(^R'2)(+01)(Line4)","","(^+1'2)(+01)(Line4)","",
                "(^+1'2)(+01)(Line4)","","","",   "","","","",

                "(R)(+11)","","","",   "","","","",
                "(D)(-11)","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "(^R'2)(+01)(Line4)","","(^+1'2)(+01)(Line4)","",
                "(^+1'2)(+01)(Line4)","","","",   "","","","",
                "(R)(^+0'1.5)(D1)(^+01'1.5)(Line3)","","","",   "","","","",
                "(R)(^+0'1.5)(D1)(^+01'1.5)(Line3)","","","",   "","","","",
                //
                "(R)(D1)","","","",   "","","","",
                "(R)(D1)","","","",   "","","","",
                "($01)($2)","","","",   "","","","",
                "($21)($0)","","","",   "","","","",

                "(R)(+21)","","","",   "(R)(+21)","","","",
                "","","","",   "(R)(+21)","","","",
                "","","","",   "(^R'2)(+01)(Line4)","","(^+1'2)(+01)(Line4)","",
                "(^+1'2)(+01)(Line4)","","","",   "","","","",

                "(R)(+11)","","","",   "","","","",
                "(D)(-11)","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "(^R'2)(+01)(Line4)","","(^+1'2)(+01)(Line4)","",
                "(^+1'2)(+01)(Line4)","","","",   "","","","",
                "(R)(^+0'1.5)(D1)(^+01'1.5)(Line3)","","","",   "","","","",
                "(R)(^+0'1.5)(D1)(^+01'1.5)(Line3)","","","",   "","","","",
                //
                "(R)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(-11)(+2)","","","",
                "(+0)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(+1)","","","",

                "(R)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(-11)(+2)","","","",
                "(+0)(+11)","","","",   "(-1)(-11)(Line4)","","(+01)(Line4)","",
                "(+1)(-11)(Line4)","","","",   "(+1)","","","",

                "(R)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(-11)(+2)","","","",
                "(+0)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(+1)","","","",

                "(#1.67#R)(#1.67#D1)(Camera3)","","","",   "","","","",
                "","","","",   "","","","",
                "(#1.67#R)(#1.67#D1)(Camera4)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(R)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(-11)(+2)","","","",
                "(+0)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(+1)","","","",

                "(R)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(-11)(+2)","","","",
                "(+0)(+11)","","","",   "(-1)(-11)(Line4)","","(+01)(Line4)","",
                "(+1)(-11)(Line4)","","","",   "(+1)","","","",

                "(R)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(-11)(+2)","","","",
                "(+0)(+11)","","","",   "(-1)","","","",
                "(+0)(-11)","","","",   "(+1)","","","",

                "(#1.67#R)(#1.67#D1)(Camera3)","","","",   "","","","",
                "","","","",   "","","","",
                "(#1.67#R)(#1.67#D1)(Camera4)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(D1)(R)(Line1)","","","",   "+0(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "(+0)(D1)(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "+0(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "+0(Line1)","","","",

                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",
                "(D1)(R)(Line2)","","","",   "(+0)(D1)(Line2)","","","",
                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",
                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",

                "(D1)(R)(Line1)","","","",   "+0(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "(+0)(D1)(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "+0(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "+0(Line1)","","","",

                "(#1.67#R)(Camera4)","","","",   "","","","",
                "D(Line3)","","","",   "","","","",
                "(#1.67#R)(Camera3)","","","",   "","","","",
                "D(Line3)","","","",   "","","","",
                //
                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",
                "(D)(R1)(Line2)","","","",   "(+01)(D)(Line2)","","","",
                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",
                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",

                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D)(R1)(Line1)","","","",   "(+01)(D)(Line1)","","","",
                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",

                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",
                "(D)(R1)(Line2)","","","",   "(+01)(D)(Line2)","","","",
                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",
                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",

                "(#1.67#R1)","","","",   "","","","",
                "D1(Line3)","","","",   "","","","",
                "(#1.67#R1)","","","",   "","","","",
                "D1(Line3)","","","",   "","","","",
                //
                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "(+0)(D1)(Line1)","","","",
                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",
                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "(+0)(D1)(Line1)","","","",
                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",
                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line1)","","","",   "(+0)(D1)(Line1)","","","",
                "(D)(R1)(Line2)","","","",   "+01(Line2)","","","",
                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",

                "(#1.67#R1)(Camera3)","","","",   "","","","",
                "D(Line3)","","","",   "","","","",
                "(#1.67#R)(Camera4)","","","",   "","","","",
                "D1(Line3)","","","",   "","","","",
                //
                 "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line2)","","","",   "(+0)(D1)(Line2)","","","",
                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line2)","","","",   "(+0)(D1)(Line2)","","","",
                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line2)","","","",   "(+0)(D1)(Line2)","","","",
                "(D)(R1)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(R)(Line2)","","","",   "+0(Line2)","","","",

                "(#1.67#R1)(Camera4)","","","",   "","","","",
                "D1(Line3)","","","",   "","","","",
                "(#1.67#R)(Camera3)","","","",   "","","","",
                "D(Line3)","","","",   "","","","",
                //
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    });

            }
            void ex5()
            {
                RegisterFunctionOnce("LConvulse", () =>
                {
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(-3, 0, BeatTime(0.5f)));
                    builder.Run(s => ScreenDrawing.ScreenAngle = s);
                });
                RegisterFunctionOnce("RConvulse", () =>
                {
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(3, 0, BeatTime(0.5f)));
                    builder.Run(s => ScreenDrawing.ScreenAngle = s);
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 5.8f, new string[]
                {
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",

                "D(D1)","","","",   "D(D1)","","","",
                "","","","",   "D(D1)","","","",
                "","","","",    "(D1)(D)(+0)","","+2","",
                "+2","","","",   "","","","",

                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "D(D1)","","","",
                "","","","",   "D(D1)","","","",

                "","","","",    "(D1)(D)(+0)","","+2","",
                "+2","","","",   "","","","",
                "(*^$20'2)(*^$00'2)(LConvulse)","","","",   "","","","",
                "(*^$21'2)(*^$01'2)(RConvulse)","","","",   "","","","",
                //
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",

                "D(D1)","","","",   "D(D1)","","","",
                "","","","",   "D(D1)","","","",
                "","","","",    "(D1)(D)(+0)","","+2","",
                "+2","","","",   "","","","",

                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "D(D1)","","","",
                "","","","",   "D(D1)","","","",

                "","","","",   "D(D1)","","","",
                "D(D1)","","","",   "D(D1)","","","",
                "D(D1)","","","",   "D(D1)","","","",
                "D(D1)","","","",   "D(D1)","","","", 
                //
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",

                "D(D1)","","","",   "D(D1)","","","",
                "","","","",   "D(D1)","","","",
                "","","","",    "(D)(D1)(+01)","","+21","",
                "+21","","","",   "","","","",

                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "D(D1)","","","",
                "","","","",   "D(D1)","","","",

                "","","","",    "(D)(D1)(+01)","","+21","",
                "+21","","","",   "","","","",
                "(*^$21'2)(*^$01'2)(LConvulse)","","","",   "","","","",
                "(*^$20'2)(*^$00'2)(RConvulse)","","","",   "","","","",
                //
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",

                "D(D1)","","","",   "D(D1)","","","",
                "","","","",   "D(D1)","","","",
                "","","","",    "(D)(D1)(+01)","","+21","",
                "+21","","","",   "","","","",

                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "","","","",
                "D(D1)","","","",   "D(D1)","","","",
                "","","","",   "D(D1)","","","",

                "","","","",   "D(D1)","","","",
                "D(D1)","","","",   "D(D1)","","","",
                "D(D1)","","","",   "D(D1)","","","",
                "D(D1)","","","",   "D(D1)","","","", 
                    //
                });
            }
            private ScreenDrawing.Shaders.RGBSplitting splitter = null;
            void ex6()
            {

                splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                RegisterFunctionOnce("Texture1", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash3, BeatTime(8.4f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("Texture2", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash4, BeatTime(7.8f - 0.5f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("Texture3", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash5, BeatTime(7.8f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("RGBSplit", () =>
                {
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    //  var production2 = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    splitter.Intensity = 1.0f;
                    //  production2.RandomDisturb = 3.0f;
                    splitter.RandomDisturb = 0;
                    ScreenDrawing.SceneRendering.InsertProduction(splitter);
                    ScreenDrawing.ScreenScale = 1.2f;
                    // ScreenDrawing.SceneRendering.InsertProduction(production2);
                });
                RegisterFunctionOnce("Fade0", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                  {
                      var v1 = Shaders.StepSample;
                      var v2 = Shaders.Scale;
                      RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                      RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                      ScreenDrawing.SceneRendering.InsertProduction(production1);
                      ScreenDrawing.SceneRendering.InsertProduction(production2);
                      v1.Intensity = 0.0f;
                      v2.Intensity = 0.0f;
                      float val = 1.4f;
                      float cur = 0;

                      float beatCount = 7 / 4;
                      float time = BeatTime(beatCount);
                      ForBeat120(beatCount, () =>
                      {
                          val -= 0.5f * (1 + val * 10) / time;
                          cur = cur * 0.7f + val * 0.3f;
                          if (cur < 0) cur = 0;
                          v1.Intensity = cur * 1.4f;
                          v2.Intensity = cur * 0.5f;
                          splitter.Intensity = cur * 40 + 1;
                      });
                      DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                  });
                });
                RegisterFunctionOnce("Fade", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                  {
                      var v1 = Shaders.StepSample;
                      var v2 = Shaders.Scale;
                      RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                      RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                      ScreenDrawing.SceneRendering.InsertProduction(production1);
                      ScreenDrawing.SceneRendering.InsertProduction(production2);
                      v1.Intensity = 0.0f;
                      v2.Intensity = 0.0f;
                      float val = 1.2f;
                      float cur = 0;

                      float beatCount = 7 / 4;
                      float time = BeatTime(beatCount);
                      ForBeat120(beatCount, () =>
                      {
                          val -= 0.5f * (1 + val * 10) / time;
                          cur = cur * 0.7f + val * 0.3f;
                          if (cur < 0) cur = 0;
                          v1.Intensity = cur;
                          v2.Intensity = cur * 0.5f;
                          splitter.Intensity = cur * 40 + 1;
                      });
                      DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                  });
                });
                RegisterFunctionOnce("Fade3", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                      var v1 = Shaders.StepSample;
                      var v2 = Shaders.Scale;
                      RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                      RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                      ScreenDrawing.SceneRendering.InsertProduction(production1);
                      ScreenDrawing.SceneRendering.InsertProduction(production2);
                      v1.Intensity = 0.0f;
                      v2.Intensity = 0.0f;
                      float val = 0.2f;
                      float cur = 0;

                      float beatCount = 7 / 4;
                      float time = BeatTime(beatCount);
                      ForBeat(beatCount, () =>
                      {
                          val -= 0.75f * (1 + val * 10) / time;
                          cur = cur * 0.6f + val * 0.4f;
                          if (cur < 0) cur = 0;
                          v1.Intensity = cur * 0.3f;
                          v2.Intensity = cur * 0.05f;
                          splitter.Intensity = cur * 30 + 1;
                      });
                      DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                  });
                });
                RegisterFunctionOnce("Fade2", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                  {
                      var v1 = Shaders.StepSample;
                      var v2 = Shaders.Scale;
                      RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                      RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                      ScreenDrawing.SceneRendering.InsertProduction(production1);
                      ScreenDrawing.SceneRendering.InsertProduction(production2);
                      v1.Intensity = 0.0f;
                      v2.Intensity = 0.0f;
                      float val = 0.3f;
                      float cur = 0;

                      float beatCount = 8;
                      float time = BeatTime(beatCount);
                      ForBeat(beatCount, () =>
                      {
                          val -= 0.75f * (1 + val * 10) / time;
                          cur = cur * 0.6f + val * 0.4f;
                          if (cur < 0) cur = 0;
                          v1.Intensity = cur;
                          v2.Intensity = cur * 0.5f;
                          splitter.Intensity = cur * 30 + 1;
                      });
                      DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                  });
                });
                RegisterFunctionOnce("VoidArrow", () =>
                {
                    Arrow[] a = GetAll<Arrow>();
                    for (int i = 0; i < a.Length; i++)
                    {
                        int x = i;
                        a[x].VoidMode = true;
                    }

                });
                RegisterFunctionOnce("MoveCentre", () =>
                {
                    BoxStates.BoxMovingScale = 0.04f;
                    SetGreenBox();

                    ScreenDrawing.UIColor = Color.White;
                    ScreenDrawing.ThemeColor = Color.White;
                    ScreenDrawing.HPBar.HPExistColor = Color.Yellow;
                    ScreenDrawing.HPBar.HPLoseColor = Color.White * 0;

                    ForBeat120(32, () => { TP(BoxStates.Centre); ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.87f + 0.9995f * 0.13f; });
                });
                RegisterFunctionOnce("WhiteOut", () =>
                {
                    ScreenDrawing.WhiteOut(BeatTime(2f));
                });
                RegisterFunctionOnce("SinScreen", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(32), ValueEasing.SinWave(5, BeatTime(16), 0f));
                    ve.Run((m) => { ScreenDrawing.ScreenAngle = m; });
                    DelayBeat(32, () => { ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(4)); });
                });
                BarrageCreate(0, BeatTime(1), 5.8f, new string[]
                {
                "Texture1(RGBSplit)(Fade2)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "Fade","","Texture2","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","Fade","","",
                //
                "Texture3","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","Fade",

                "(VoidArrow)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "MoveCentre(SinScreen)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R1","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R1","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R1","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R1","","","",
                "","","","",   "","","","",
                //
                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R1","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R1","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R1","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","WhiteOut","","",
                "","","","",   "","","","",
                    //
                });
            }
            void ex7()
            {
                BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                {

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(R1)","","","",   "(R)(R1)","","","",
                "","","","",   "(R)(R1)","","","",

                "","","","",   "(R)(R1)","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",

                "(R)(R1)","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "","","","",   "(R)(R1)","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                //
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "(R)(R1)","","","",
                "","","","",   "(R)(R1)","","","",

                "","","","",   "(R)(R1)","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",

                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(R1)","","","",   "(R)(R1)","","","",
                //
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "(R)(R1)","","","",
                "","","","",   "(R)(R1)","","","",

                "","","","",   "(R)(R1)","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",

                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "(R)(R1)","","","",
                "","","","",   "(R)(R1)","","","",

                "","","","",   "(R)(R1)","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                //
                "(R)(R1)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",
                "(R)(R1)","","","",   "","","","",

                "(R)(R1)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //"","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    //

                });
            }

            void nb1()
            {

                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(0, 120, BeatTime(4)), ValueEasing.EaseOutSine(0, 180, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(120, 280, BeatTime(4)), ValueEasing.EaseOutQuad(180, 160, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(280, 260, BeatTime(4)), ValueEasing.EaseOutSine(160, 300, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(260, 320, BeatTime(4)), ValueEasing.EaseOutQuad(300, 360, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(320, 420, BeatTime(4)), ValueEasing.EaseOutQuad(360, 160, BeatTime(4))));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0.5f), new(0, 0.15f)));
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(0, ValueEasing.Stable(90));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(90, 90 + 60, BeatTime(4)));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(90 + 60, 40, (BeatTime(4))));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(40, 130, BeatTime(4)));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(130, 160, (BeatTime(4))));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(160, 180 + 45, (BeatTime(4))));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(0, 60, BeatTime(4)));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(60, 40 - 90, (BeatTime(4))));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(40 - 90, 130 - 90, BeatTime(4)));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(130 - 90, 160 - 90, (BeatTime(4))));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(160 - 90, 90 + 45, (BeatTime(4))));
                    Line l1 = new(ce.GetResult(), ve1.GetResult()) { Alpha = 0.65f };
                    Line l2 = new(ce.GetResult(), ve2.GetResult()) { Alpha = 0.65f };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    LineShadow(1.5f, 0.4f, 8, l1);
                    LineShadow(1.5f, 0.4f, 8, l2);

                    //l1.ObliqueMirror = true;
                    //l2.ObliqueMirror = true;
                });
                RegisterFunctionOnce("star1", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(4), CentreEasing.LerpTo(new(320, -50), 0.0295f, new Vector2(320, 100)));
                    Extends.Star s = new(new Vector2(320, -50), 0.8f) { rotatespeed = 0.5f, ColorType = 1 };
                    e.Run((m) => { s.Centre = new Vector2(0, 0) + m; });
                    s.rotatespeed = 1.8f;
                    CreateEntity(s);
                    DelayBeat(4, () => { s.Dispose(); });
                    ForBeat(4, () => { s.rotatespeed -= 0.005f; });
                });
                RegisterFunctionOnce("star2", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.40f, 0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s = new(e.GetResult(), 0.6f) { rotate = false, ColorType = 1 };
                    CreateEntity(s);
                    e.Run((m) => { s.Centre = new Vector2(320, 100) + m; });
                    s.rotatespeed = 0;
                    ForBeat(16, () =>
                    {
                        s.rotatespeed += 0.1f;
                    });

                });
                RegisterFunctionOnce("star3", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.10f, 0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s = new(new Vector2(320, 100), 0.6f) { rotate = false, ColorType = 1 };
                    CreateEntity(s);
                    e.Run((m) => { s.Centre = new Vector2(320, 100) + m; });
                    CentreEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.10f, -0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s1 = new(new Vector2(320, 100), 0.6f) { rotate = false, ColorType = 1 };
                    CreateEntity(s1);
                    e1.Run((m) => { s1.Centre = new Vector2(320, 100) + m; });
                    float add = 0;
                    s.rotatespeed = 0;
                    s1.rotatespeed = 0;
                    ForBeat(16, () =>
                    {
                        add += 0.1f;
                        s.rotatespeed -= 0.1f;
                        s1.rotatespeed += 0.1f;
                    });
                });
                RegisterFunctionOnce("fireball1", () =>
                {
                    if (Rand(0, 1) == 0)
                    {

                        CentreEasing.EaseBuilder e = new();
                        e.Insert(0, CentreEasing.Stable(Rand(0, BoxStates.Left - 5), -10));
                        e.Insert(BeatTime(16), CentreEasing.Linear(new Vector2(0, 4.5f)));
                        Fireball f = new(e.GetResult(), 1f);
                        CreateEntity(f);
                        e.Run((m) => { f.Centre = m; });
                        ForBeat(4, () =>
                        {
                            f.Alpha -= 0.02f;
                        });
                    }
                    else if (LastRand == 1)
                    {
                        CentreEasing.EaseBuilder e = new();
                        e.Insert(0, CentreEasing.Stable(Rand(BoxStates.Right + 5, 480), -10));
                        e.Insert(BeatTime(16), CentreEasing.Linear(new Vector2(0, 4.5f)));
                        Fireball f = new(e.GetResult(), 1f);
                        CreateEntity(f);
                        e.Run((m) => { f.Centre = m; });
                        ForBeat(4, () => { f.Alpha -= 0.02f; });
                    }
                });
                RegisterFunctionOnce("line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 500));
                    ce.Insert(BeatTime(8), CentreEasing.Linear(new Vector2(0, -1f)));
                    ValueEasing.EaseBuilder re = new();
                    re.Insert(BeatTime(6), ValueEasing.EaseOutSine(0, 30, BeatTime(6)));
                    ValueEasing.EaseBuilder re2 = new();
                    re2.Insert(BeatTime(6), ValueEasing.EaseOutSine(0, -30, BeatTime(6)));
                    Line l = new(ce.GetResult(), re.GetResult()) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(ce.GetResult(), re2.GetResult()) { DrawingColor = Color.CornflowerBlue };
                    l.AlphaDecrease(BeatTime(5.7f));
                    l2.AlphaDecrease(BeatTime(5.7f));
                    CreateEntity(l);
                    CreateEntity(l2);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                        l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                    });
                });
                RegisterFunctionOnce("line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 0), new(320, BoxStates.Up), BeatTime(8)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 480));
                    ce2.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 480), new(320, BoxStates.Down), BeatTime(8)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(0)) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(0)) { DrawingColor = Color.CornflowerBlue };
                    l.AlphaDecrease(BeatTime(4f));
                    l2.AlphaDecrease(BeatTime(4f));
                    CreateEntity(l);
                    CreateEntity(l2);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                        l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                    });

                });
                RegisterFunctionOnce("line3", () =>
                {
                    ValueEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(2), ValueEasing.EaseOutQuad(30, 160, BeatTime(2)));
                    e.Insert(BeatTime(4), ValueEasing.EaseOutQuart(160, 180 + 45, BeatTime(4)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.Stable(320, 240));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, 0), new(0, 0.12f)));
                    Line l = new(ce.GetResult(), e.GetResult()) { Alpha = 0.75f };
                    l.InsertRetention(new Line.RetentionEffect(0.5f, 0.5f));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(4f)); });
                    CreateEntity(l);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);

                    });
                    for (int i = 0; i < 12; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.02f));

                    }
                });
                RegisterFunctionOnce("line4", () =>
                {
                    ValueEasing.EaseBuilder e = new();

                    e.Insert(BeatTime(4), ValueEasing.EaseOutQuart(160 + 90, 270 + 45, BeatTime(4)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 240));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, 0), new(0, 0.12f)));
                    Line l = new(ce.GetResult(), e.GetResult()) { Alpha = 0.75f };
                    for (int i = 0; i < 12; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.02f));

                    }
                    l.AlphaDecrease(BeatTime(4f));
                    CreateEntity(l);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);

                    });

                });
                RegisterFunctionOnce("line5", () =>
                {
                    Color[] c =
                    {
                    Color.Red,Color.Orange,Color.Yellow,Color.Green,Color.Cyan,Color.Blue,Color.Purple,Color.White
                    };
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        DelayBeat(i * 0.5f, () =>
                        {

                            ValueEasing.EaseBuilder e = new();
                            e.Insert(BeatTime(0), ValueEasing.Stable(90));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(BeatTime(0), CentreEasing.Stable(200 + 600 / 9 * (x), 240));
                            ce.Insert(BeatTime(6), CentreEasing.Linear(-6f));
                            Line l = new(ce.GetResult(), e.GetResult()) { DrawingColor = c[x], Alpha = 0.5f };
                            l.InsertRetention(new Line.RetentionEffect(0.5f, 0.5f));
                            l.AlphaDecrease(BeatTime(5f));
                            CreateEntity(l);
                            ForBeat(8, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.05f);

                            });
                            for (int i = 0; i < 24; i++)
                            {
                                int t = i;
                                l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.01f));

                            }
                        });
                    }


                });
                RegisterFunctionOnce("star4", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.10f, 0.41f), -10), new(0, 0.17f)));
                    Extends.Star s = new(new Vector2(0, 0), 0.6f) { rotate = false };
                    CreateEntity(s);
                    int a = Rand(-60, -20);
                    int b = Rand(20, 60);
                    e.Run((m) => { s.Centre = new Vector2(320 + a, 510) + m; });
                    CentreEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.41f, -0.10f), -10), new(0, 0.17f)));
                    Extends.Star s1 = new(new Vector2(0, 0), 0.6f) { rotate = false };
                    CreateEntity(s1);
                    e1.Run((m) => { s1.Centre = new Vector2(320 + b, 510) + m; });

                    s.rotatespeed = 0;
                    s1.rotatespeed = 0;
                    ForBeat(16, () =>
                    {

                        s.rotatespeed -= 0.07f;
                        s1.rotatespeed += 0.07f;
                    });

                });
                RegisterFunctionOnce("return", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });

                RegisterFunctionOnce("RotateL", () =>
                {
                    ScreenDrawing.CameraEffect.RotateTo(-2.3f, BeatTime(4f));
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    ScreenDrawing.CameraEffect.RotateTo(2.3f, BeatTime(4f));
                });
                RegisterFunctionOnce("RotateReturn", () =>
                {
                    ScreenDrawing.CameraEffect.RotateTo(0f, BeatTime(4f));
                });
                RegisterFunctionOnce("BoxSin", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(240));
                    ve.Insert(BeatTime(2f), ValueEasing.EaseOutQuad(240, 210, BeatTime(2f)));
                    ve.Insert(BeatTime(22), ValueEasing.SinWave(23, BeatTime(8), 0.5f));
                    ve.Insert(BeatTime(2f), ValueEasing.EaseOutQuad(210, 260, BeatTime(2f)));
                    float b = 0;
                    ve.Run((m) => { b = m; });
                    ForBeat(28, () => { InstantSetBox(b, 84, 84); InstantTP(320, b); });

                });
                RegisterFunctionOnce("WhiteShake", () =>
                {
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        DelayBeat(i * 0.5f, () =>
                        {
                            Color[] c =
                            {
                            Color.Red,
                            Color.Orange,
                            Color.Yellow,
                            Color.Green,
                            Color.Cyan,
                            Color.Blue,
                            Color.Purple,
                            Color.White
                             };
                            ScreenDrawing.MakeFlicker(c[x] * 0.35f);
                        });
                    }
                });
                RegisterFunctionOnce("Effect1", () =>
                {
                    Color[] c =
                    {
                    Color.Red,Color.Orange,Color.Yellow,Color.Green,Color.Cyan,Color.Blue,Color.Purple,Color.White
                    };
                    ValueEasing.EaseBuilder e = new();
                    ValueEasing.EaseBuilder e2 = new();
                    ValueEasing.EaseBuilder e3 = new();
                    e.Insert(BeatTime(1), ValueEasing.LerpTo(1, 1.1f, 0.04f));
                    e.Run((m) => { ScreenDrawing.ScreenScale = m; });
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        DelayBeat(i * 0.5f, () =>
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(90));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(0, 0));
                            ce.Insert(BeatTime(1.5f), CentreEasing.LerpTo(new Vector2(0, 0), 0.06f, new Vector2(760, 0)));
                            Line l = new(ce.GetResult(), ve.GetResult()) { DrawingColor = c[x] };
                            CreateEntity(l);
                            ScreenDrawing.CameraEffect.Convulse(10, true);
                            for (int i = 0; i < 3; i++)
                            {
                                int t = i;
                                l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.06f));

                            }


                        });
                    }
                });
                BarrageCreate(0, BeatTime(1), 6, new string[]
                {
                //
                "star1(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star2(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star3","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "star1(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star3(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star2","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "star4(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star4(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star4","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "(star4)","","","",   "","","","",

                "line2(return)(star4)","","","",   "","","","",
                "","","","",   "","","","",
                "$3(line3)","","","",   "","","","",
                "","","","",   "","","","",

                "$3(line4)(line5)","","","",   "$1","","","",
                "$2","","","",   "$1","","","",
                "$2","","","",   "$2","","","",
                "$3","","","",   "$3","","","",
                //
                "D(RotateL)(BoxSin)(Line1)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(^+0'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "D(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "D(RotateL)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(^+0'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "D(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "D(RotateL)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(^+0'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "D(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "+0","","","",   "","","","",
                "(^D'1.5)","","","",   "","","","",

                "(^D'1.5)(RotateReturn)","","","",   "","","","",
                "","","","",   "","","","",
                "(^D'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$2'1.5)(WhiteShake)(Effect1)","","","",  "(^$21'1.5)","","","",
                "(^$21'1.5)","","","",   "(^$21'1.5)","","","",
                "(^$01'1.5)","","","",   "(^$01'1.5)","","","",
                "(^$01'1.5)","","","",   "(^$01'1.5)","","","",
                    //
                });
            }
            void nb2()
            {
                RegisterFunctionOnce("SinLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(70, 0));
                    ce.Insert(BeatTime(64), CentreEasing.XSinWave(30, BeatTime(6), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(64), ValueEasing.Linear(0, 360, BeatTime(8)));
                    ve.Run((m) => { l.DrawingColor = new Color(DrawingLab.HsvToRgb(m, 255, 255, 0)); });
                    l.TransverseMirror = true;
                    for (int i = 0; i < 48; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.005f));

                    }

                    DelayBeat(60, () => { l.AlphaDecrease(BeatTime(4)); });
                    ValueEasing.EaseBuilder box = new();
                    box.Insert(0, ValueEasing.Stable(200));
                    box.Insert(BeatTime(60), ValueEasing.SinWave(8, BeatTime(4), 0.5f));
                    box.Run((m) => { InstantSetBox(240, m, 488); });
                    ValueEasing.EaseBuilder down = new();
                    down.Insert(0, ValueEasing.Stable(100));
                    down.Insert(BeatTime(57) - 4, ValueEasing.SinWave(70, BeatTime(8), 0f));
                    down.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                    ScreenDrawing.BoundColor = Color.White * 0.23f;
                    ForBeat(60, () =>
                    {

                        if ((int)GametimeF % 60 == 0)
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(8));
                            ve.Insert(BeatTime(4), ValueEasing.EaseInSine(8, 12, BeatTime(4)));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(320, 560));
                            ce.Insert(BeatTime(16), CentreEasing.Accerlating(new(0, 0f), new(0, -0.175f)));
                            Line l2 = new(ce.GetResult(), ve.GetResult());
                            CreateEntity(l2);
                            l2.DrawingColor = Color.CornflowerBlue;
                            l2.Width = 2.7f;
                            ForBeat(6, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                                l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                            });
                            LineShadow(2.5f, 0.75f, 6, l2);
                            l2.Alpha = 0.5f;
                        }
                        if ((int)GametimeF % 60 == 30)
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(-8));
                            ve.Insert(BeatTime(4), ValueEasing.EaseInSine(-8, -12, BeatTime(4)));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(320, 560));
                            ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0f), new(0, -0.175f)));
                            Line l2 = new(ce.GetResult(), ve.GetResult());
                            CreateEntity(l2);
                            l2.DrawingColor = Color.CornflowerBlue;
                            l2.Width = 2.7f;
                            ForBeat(6, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                                l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                            });
                            LineShadow(2.5f, 0.75f, 4, l2);
                            l2.Alpha = 0.5f;
                        }
                    });
                });
                RegisterFunctionOnce("Light", () =>
                {
                    ScreenDrawing.WhiteOut(BeatTime(2));
                    SetBox(240, 200, 480);
                    SetSoul(0);
                    TP();
                    Extends.DrawingUtil.LerpScreenScale(60, 1, 0.09f);
                    CentreEasing.EaseBuilder ce = new();
                    Vector2 Value = ScreenDrawing.HPBar.AreaOccupied.GetCentre();
                    ce.Insert(0, CentreEasing.Stable(ScreenDrawing.HPBar.AreaOccupied.GetCentre()));
                    ce.Insert(0, CentreEasing.EaseOutQuad(Value, new(20, 480 - 60), BeatTime(2)));
                    ce.Run((m) => { ScreenDrawing.HPBar.AreaOccupied = new(m, ScreenDrawing.HPBar.AreaOccupied.Size); });
                });
                RegisterFunction("BHStar", () =>
                {
                    int max = 8;
                    int RotateRand = Rand(0, 359);
                    for (int a = 0; a < max; a++)
                    {
                        int value = a;
                        Star s = new(new Vector2(0, 0), 0.5f) { ColorType = 1 };
                        CreateEntity(s);
                        CentreEasing.EaseBuilder ce = new();
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(0, ValueEasing.Stable(360));
                        ve1.Insert(BeatTime(12), ValueEasing.Linear(360, -360, BeatTime(6)));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(0, ValueEasing.Stable(RotateRand + 360 / max * value));
                        ve2.Insert(BeatTime(16), ValueEasing.Linear(0, 1080 / 2, BeatTime(16)));
                        float valueN = 0;
                        ve1.Run((m) =>
                        {

                            s.Centre = new Vector2(320, 240) + new Vector2(Cos(valueN) * m, Sin(valueN) * m);

                        });
                        ve2.Run((n) =>
                        {
                            valueN = n;
                        });
                    }
                });
                int k = 0;
                RegisterFunctionOnce("Fireball1", () =>
                {
                    int val = Rand(-1, 1);
                    while (val == k)
                    {
                        val = Rand(-1, 1);
                    }
                    k = val;
                    float ct = k * 66 + 320;
                    float delta = Rand(0, 1f);
                    for (int a = 0; a < 16; a++)
                    {
                        int value = a;
                        Fireball f = new(new Vector2(0, 0), 1);
                        CreateEntity(f);
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(BeatTime(32), ValueEasing.SinWave(35, -BeatTime(4), 1f / 12f * value + delta));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(BeatTime(32), ValueEasing.Linear(-3.6f));
                        float valueN = 0;
                        ve1.Run((m) =>
                        {
                            f.Centre = new(ct + m, 488 + 13 * value + valueN);
                        });
                        ve2.Run((n) =>
                        {
                            valueN = n;
                        });
                        Fireball f2 = new(new Vector2(0, 0), 1);
                        CreateEntity(f2);
                        ValueEasing.EaseBuilder ve3 = new();
                        ve3.Insert(BeatTime(32), ValueEasing.SinWave(35, -BeatTime(4), 1f / 12f * value + delta));
                        ValueEasing.EaseBuilder ve4 = new();
                        ve4.Insert(BeatTime(32), ValueEasing.Linear(-3.6f));
                        float valueN2 = 0;
                        ve3.Run((m) =>
                        {
                            f2.Centre = new(ct - m, 488 + 13 * value + valueN2);
                        });
                        ve4.Run((n) =>
                        {
                            valueN2 = n;
                        });
                    }
                });

                RegisterFunctionOnce("Over", () =>
                {
                    SetSoul(1);
                    SetBox(240, 84, 84);
                    ScreenDrawing.WhiteOut(BeatTime(4));
                    TP();
                    ValueEasing.EaseBuilder down = new();
                    down.Insert(BeatTime(1), ValueEasing.LerpTo(100 + 35, 0, 0.1f));
                    down.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                });
                BarrageCreate(0, BeatTime(1), 6, new string[]
                {
                "(Light)","","","",  "","","","",
                "","","","",  "","","","",
                "(SinLine)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Over)","","","",  "","","","",
                "","","","",  "","","","",
                "","","","",  "","","","",
                "","","","",  "","","","",
                    //
                });
            }
            void nb3()
            {
                Player.Heart heart1 = Heart;
                Player.Heart heart2 = CreateHeart(new CollideRect(380, 180, 84, 84));
                RegisterFunctionOnce("Start", () =>
                {
                    SetPlayerBoxMission(heart1);
                    SetSoul(1);

                    TP(320 - 60, 240 - 180 + 240);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(30));
                    for (int a = 0; a < 8; a++)
                        ve.Insert(BeatTime(4), ValueEasing.EaseOutElastic(30 + a * 30, 30 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve.Insert(BeatTime(8), ValueEasing.EaseOutBack(30 + a * 30, 30 + (a + 1) * 30, BeatTime(8)));
                    ve.Run((m) => { SetPlayerBoxMission(heart1); InstantTP(new Vector2(320, 240) + GetVector2(85, m)); InstantSetBox(new Vector2(320, 240) + GetVector2(85, m), 84, 84); });
                    SetPlayerBoxMission(heart2);
                    SetSoul(1);
                    TP(320 - 60, 240 - 180 + 240);
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(0, ValueEasing.Stable(30));
                    for (int a = 0; a < 8; a++)
                        ve2.Insert(BeatTime(4), ValueEasing.EaseOutElastic(30 + a * 30, 30 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve2.Insert(BeatTime(8), ValueEasing.EaseOutBack(30 + a * 30, 30 + (a + 1) * 30, BeatTime(8)));
                    ve2.Run((m) => { SetPlayerBoxMission(heart2); InstantTP(new Vector2(320, 240) + GetVector2(85, 180 + m)); InstantSetBox(new Vector2(320, 240) + GetVector2(85, 180 + m), 84, 84); });
                    DelayBeat(0, () =>
                    {
                        heart1.Shields.RemoveShield(heart1.Shields.BShield);
                        heart2.Shields.RemoveShield(heart2.Shields.RShield);
                    });
                });
                RegisterFunctionOnce("CentreLine", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(75));
                    for (int a = 0; a < 8; a++)
                        ve.Insert(BeatTime(4), ValueEasing.EaseOutElastic(75 + 30 * a, 75 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve.Insert(BeatTime(8), ValueEasing.EaseOutBack(75 + 30 * a, 75 + (a + 1) * 30, BeatTime(8)));
                    Line l = new(CentreEasing.Stable(320, 240), ve.GetResult());
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(128), ValueEasing.Linear(0, 360, BeatTime(8)));
                    ve2.Run((m) => { l.DrawingColor = new Color(DrawingLab.HsvToRgb(m, 255, 255, 0)); });
                    LineShadow(0.5f, 8, l);
                    CreateEntity(l);
                    DelayBeat(60, () => { l.AlphaDecrease(BeatTime(4)); });
                });
                RegisterFunctionOnce("Screen1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(0, 0), new(-18, 4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(-18, 4), new(18, 4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(18, 4), new(18, -4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen4", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(18, -4), new(-18, -4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("ScreenOver", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(-14, -4), new(0, 0), BeatTime(2)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Return", () =>
                {
                    SetPlayerBoxMission(heart2);
                    Vector2 boxc2 = BoxStates.Centre;
                    SetPlayerBoxMission(heart1);
                    Vector2 boxc = BoxStates.Centre;
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1) - 2, CentreEasing.Alternate(2, new Func<ICustomMotion, Vector2>[]
                    {
                    CentreEasing.Linear(boxc,boxc2,2),
                    CentreEasing.Linear(boxc2,boxc,2)
                    }));
                    ce.Run((m) => { SetPlayerBoxMission(heart1); InstantSetBox(m, 84, 84); InstantTP(m); });
                    DelayBeat(1, () => { ScreenDrawing.MakeFlicker(Color.White); });
                    heart2.Merge(heart1);
                    SetPlayerBoxMission(heart1);
                    heart1.Shields.AddShield(heart1.Shields.BShield);
                    heart1.Shields.AddShield(heart1.Shields.RShield);
                    SetSoul(1);
                    ForBeat(1.125f, 5, () =>
                    {
                        SetPlayerBoxMission(heart1);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        InstantTP(320, 240);
                    });

                });
                RegisterFunctionOnce("Merge", () =>
                {
                    heart2.Merge(heart1);
                    SetPlayerBoxMission(heart1);
                    heart1.Shields.AddShield(heart1.Shields.BShield);
                    heart1.Shields.AddShield(heart1.Shields.RShield);
                    SetSoul(1);
                    DelayBeat(1f, () =>
                    {
                        SetBox(new Vector2(320, 240), 84, 84);
                        TP(320, 240);
                    });

                });
                RegisterFunctionOnce("WhiteBound1", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.DownBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 130, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(130, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                });
                RegisterFunctionOnce("WhiteBound2", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.RightBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 200, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(200, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.RightBoundDistance = m; });
                });
                RegisterFunctionOnce("WhiteBound3", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.LeftBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 200, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(200, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.LeftBoundDistance = m; });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(0, 240));
                    ce1.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(0, 240), new(320 + 120, 240), BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1.75f));
                    LineShadow(2.3f, 0.75f, 4, l1);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 240));
                    ce1.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(640, 240), new(320 - 120, 240), BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1.75f));
                    LineShadow(2.3f, 0.75f, 4, l1);
                });
                SetPlayerBoxMission(heart1);
                BarrageCreate(0, BeatTime(1), 5, new string[]
                {
                "Start(CentreLine)",""
                });
                SetPlayerBoxMission(heart2);
                BarrageCreate(0, BeatTime(1), 4.5f, new string[]
                {
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "N1","","","",   "","","","",
                "+0","","","",   "","","","",
                "N1","","","",   "","","","",
                "+0","","","",   "","","","",

                "N1","","","",   "","","","",
                "+0","","","",   "+0","","","",
                "N1","","","",   "","","","",
                "+0","","","",   "","","","",

                "N1","","","",   "","","","",
                "+0","","","",   "","","","",
                "N1","","","",   "+0","","","",
                "+0","","","",   "","","","",
                //
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "#7#$0(Screen1)(RRotate)","","","",   "","","","",
                "+0","","","",   "","","","",
                "+0","","","",   "","","","",
                "+0","","","",   "","","","",

                "+0","","","",   "","","","",
                "+0","","","",   "","","","",
                "+0","","","",   "","","","",
                "+0","","","",   "","","","",

                "(Screen2)(LRotate)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "#7#$1(Screen3)(LRotate)","","","",   "","","","",
                "+0","","","",   "","","","",
                "+0","","","",   "","","","",
                "+0","","","",   "","","","",

                "+0","","","",   "","","","",
                "+0","","","",   "","","","",
                "+0","","","",   "","","","",
                "+0","","","",   "","","","",

                "(Screen4)(RRotate)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "(BHStar)","","","",   "","","","",
                "(ScreenOver)(MidRotate)","","","",   "","","","",
                "(Return)","","","",   "","","","",

                ""
                });
                SetPlayerBoxMission(heart1);
                BarrageCreate(0, BeatTime(1), 4.5f, new string[]
                {
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(LRotate)(Line1)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(RRotate)(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(LRotate)(Line1)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "N01","","","",   "","","","",
                "+01","","","",   "","","","",
                "N01(RRotate)(Line2)","","","",   "","","","",
                "+01","","","",   "","","","",

                "N01","","","",   "","","","",
                "+01","","","",   "","","","",
                "N01(LRotate)(Line1)","","","",   "","","","",
                "+01","","","",   "","","","",

                "N01","","","",   "","","","",
                "+01","","","",   "+01","","","",
                "N01(RRotate)(Line2)","","","",   "","","","",
                "+01","","","",   "","","","",

                "N11","","","",   "","","","",
                "+01","","","",   "","","","",
                "N11(MidRotate)(Line1)","","","",   "+01","","","",
                "+01","","","",   "","","","",
                //
                "(WhiteBound1)","","","",   "","","","",
                "","","","",   "","","","",
                "(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(Line1)","","","",   "","","","",
                "","","","",   "","","","",

                "#7#$11(WhiteBound2)","","","",   "","","","",
                "+01","","","",   "+01","","","",
                "(Line2)+01","","","",   "+01","","","",
                "+01","","","",   "","","","",

                "+01","","","",   "+01","","","",
                "+01","","","",   "","","","",
                "(Line1)+01","","","",   "+01","","","",
                "+01","","","",   "","","","",
                //
                "(WhiteBound3)","","","",   "","","","",
                "","","","",   "","","","",
                "(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(Line1)","","","",   "","","","",
                "","","","",   "","","","",

                "#7#$31(WhiteBound2)","","","",   "","","","",
                "+01","","","",   "+01","","","",
                "(Line2)+01","","","",   "+01","","","",
                "+01","","","",   "","","","",

                "+01","","","",   "","","","",
                "+01","","","",   "","","","",
                "+01","","","",   "+01","","","",
                "+01","","","",   "","","","",
                    //
                });
            }
            void nb4()
            {
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(0, 240));
                    ce1.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new Vector2(0, 240), new Vector2(640, 240), BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.7f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1f));
                    LineShadow(0.7f, 5, l1);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 240));
                    ce1.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new Vector2(640, 240), new Vector2(0, 240), BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.7f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1f));
                    LineShadow(0.7f, 5, l1);
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320 + 20, 480));
                    ce.Insert(BeatTime(4), CentreEasing.LerpTo(new Vector2(320 + 20, 480), 0.07f, new Vector2(320 + 20 + 90, 480)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 240 - 15));
                    ce2.Insert(BeatTime(4), CentreEasing.LerpTo(new Vector2(0, 240 - 15), 0.07f, new Vector2(0, 240 - 15 - 90f / 4f * 3f)));
                    Line l1 = new(ce.GetResult(), ce2.GetResult()) { Alpha = 0.8f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(0.78f));
                    l1.ObliqueMirror = true;
                    l1.TransverseMirror = true;
                    l1.VerticalMirror = true;
                });
                RegisterFunctionOnce("Line4", () =>
                {
                    Line l1 = new(CentreEasing.Stable(Rand(0, 640), 240), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(2));
                });
                RegisterFunctionOnce("Camera1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6.9f, BeatTime(0.78f), true);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.SinWave(0.01f, BeatTime(1f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6.9f, BeatTime(0.78f), false);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.SinWave(0.01f, BeatTime(1f), 0.5f));
                    // ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera3", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(10, BeatTime(2f), false);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.8f), ValueEasing.SinWave(0.03f, BeatTime(1.8f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera4", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(10, BeatTime(2f), true);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.8f), ValueEasing.SinWave(0.03f, BeatTime(1.8f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                SetPlayerBoxMission(0);
                BarrageCreate(BeatTime(0), BeatTime(1), 5.4f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                "($0)","","","",   "","","","",
                "($2)","","","",   "","","","",

                "(R)","","","",   "(R)","","","",
                "","","","",   "(R)","","","",
                "","","","",   "(R)(Line4)","","(+0)(Line4)","",
                "(+0)(Line4)","","","",   "","","","",

                "(R)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(R)","","","",   "(R)","","","",
                "","","","",   "(R)","","","",

                "","","","",   "(R)(Line4)","","(+0)(Line4)","",
                "(+0)(Line4)","","","",   "","","","",
                "($1)(^+0'1.5)(Line3)","","","",   "","","","",
                "($3)(^+0'1.5)(Line3)","","","",   "","","","",
                //
                "(R)","","","",   "","","","",
                "(R)","","","",   "","","","",
                "($01)","","","",   "","","","",
                "($21)","","","",   "","","","",

                "(R)","","","",   "(R)","","","",
                "","","","",   "(R)","","","",
                "","","","",   "(R1)","","(+01)(Line4)","",
                "(+01)(Line4)","","","",   "","","","",

                "(R)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(R)","","","",   "(R)","","","",
                "","","","",   "(R)","","","",

                "","","","",   "(R1)(Line4)","","(+01)(Line4)","",
                "(+01)(Line4)","","","",   "","","","",
                "($0)(^+0'1.5)(Line3)","","","",   "","","","",
                "($2)(^+0'1.5)(Line3)","","","",   "","","","",
                //
                "(R)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",
                "(+1)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",

                "(R)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",
                "(+1)","","","",   "(-0)(Line4)","","(Line4)","",
                "(+1)(Line4)","","","",   "(+0)","","","",

                "(R)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",
                "(+1)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",

                "(#1.37#D)(Camera3)","","","",   "","","","",
                "","","","",   "","","","",
                "(#1.37#D1)(Camera4)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(R)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",
                "(+1)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",

                "(R)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",
                "(+1)","","","",   "(-0)(Line4)","","(Line4)","",
                "(+1)(Line4)","","","",   "(+0)","","","",

                "(R)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",
                "(+1)","","","",   "(-0)","","","",
                "(+1)","","","",   "(+0)","","","",

                "(#1.37#D)(Camera3)","","","",   "","","","",
                "","","","",   "","","","",
                "(#1.37#D1)(Camera4)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(D0)(Line1)","","","",   "+0(Line1)","","","",
                "(D0)(Line1)","","","",   "(+0)(+0)(Line1)","","","",
                "(D0)(Line1)","","","",   "+0(Line1)","","","",
                "(D0)(Line1)","","","",   "+0(Line1)","","","",

                "(D0)(Line2)","","","",   "+0(Line2)","","","",
                "(D0)(Line2)","","","",   "(+0)(Line2)","","","",
                "(D0)(Line2)","","","",   "+0(Line2)","","","",
                "(D0)(Line2)","","","",   "+0(Line2)","","","",

                "(D0)(Line1)","","","",   "+0(Line1)","","","",
                "(D0)(Line1)","","","",   "(+0)(Line1)","","","",
                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D0)(Line1)","","","",   "+0(Line1)","","","",

                "(#1.37#D)(Camera4)","","","",   "","","","",
                "+0(Line3)","","","",   "","","","",
                "(#1.37#D)(Camera3)","","","",   "","","","",
                "+0(Line3)","","","",   "","","","",
                //
                "(D)(Line2)","","","",   "+0(Line2)","","","",
                "(D)(Line2)","","","",   "(+0)(Line2)","","","",
                "(D)(Line2)","","","",   "+0(Line2)","","","",
                "(D)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(Line1)","","","",   "(+0)(Line1)","","","",
                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(Line1)","","","",   "+0(Line1)","","","",

                "(D)(Line2)","","","",   "+0(Line2)","","","",
                "(D)(Line2)","","","",   "(+0)(Line2)","","","",
                "(D)(Line2)","","","",   "+0(Line2)","","","",
                "(D)(Line2)","","","",   "+0(Line2)","","","",

                "(#1.37#R1)","","","",   "","","","",
                "+01(Line3)","","","",   "","","","",
                "(#1.37#R1)","","","",   "","","","",
                "+01(Line3)","","","",   "","","","",
                //
                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D0)(+0)(Line1)","","","",   "(+0)(Line1)","","","",
                "(D)(Line2)","","","",   "+0(Line2)","","","",
                "(D0)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D0)(+0)(Line1)","","","",   "(+0)(Line1)","","","",
                "(D)(Line2)","","","",   "+0(Line2)","","","",
                "(D0)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(+0)(Line1)","","","",   "(+0)(Line1)","","","",
                "(D)(Line2)","","","",   "+0(Line2)","","","",
                "(D)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(#1.37#D)(Camera3)","","","",   "","","","",
                "+0(Line3)","","","",   "","","","",
                "(#1.37#D)(Camera4)","","","",   "","","","",
                "+0(Line3)","","","",   "","","","",
                //
                 "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(+0)(Line2)","","","",   "(+0)(Line2)","","","",
                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(+0)(Line2)","","","",   "(+0)(Line2)","","","",
                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(+0)(Line2)","","","",   "(+0)(Line2)","","","",
                "(D)(Line1)","","","",   "+0(Line1)","","","",
                "(D)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(#1.37#D1)(Camera4)","","","",   "","","","",
                "+01(Line3)","","","",   "","","","",
                "(#1.37#D1)(Camera3)","","","",   "","","","",
                "+01(Line3)","","","",   "","","","",
                //
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    });

            }
            void nb5()
            {
                RegisterFunctionOnce("LConvulse", () =>
                {
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(-3, 0, BeatTime(0.5f)));
                    builder.Run(s => ScreenDrawing.ScreenAngle = s);
                });
                RegisterFunctionOnce("RConvulse", () =>
                {
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(3, 0, BeatTime(0.5f)));
                    builder.Run(s => ScreenDrawing.ScreenAngle = s);
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 5.8f, new string[]
                {
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",
                "","","","",    "(D)(+0)","","+0","",
                "+0","","","",   "","","","",

                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",

                "","","","",    "(D)(+0)","","+0","",
                "+0","","","",   "","","","",
                "(^$01'2)(LConvulse)","","","",   "","","","",
                "(^$21'2)(RConvulse)","","","",   "","","","",
                //
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",
                "","","","",    "(D)(+0)","","+0","",
                "+0","","","",   "","","","",

                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",

                "","","","",   "(D)","","","",
                "(D)","","","",   "(D)","","","",
                "(D)","","","",   "(D)","","","",
                "(D)","","","",   "(D)","","","", 
                //
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",
                "","","","",    "(D1)(+01)","","+01","",
                "+01","","","",   "","","","",

                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",

                "","","","",    "(D1)(+01)","","+01","",
                "+01","","","",   "","","","",
                "(<$0)(_^$0'2)(LConvulse)","","","",   "","","","",
                "(>$2)(_^$2'2)(RConvulse)","","","",   "","","","",
                //
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",
                "","","","",    "(D1)(+01)","","+01","",
                "+01","","","",   "","","","",

                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",

                "","","","",   "(D)","","","",
                "(D)","","","",   "(D)","","","",
                "(D)","","","",   "(D)","","","",
                "(D)","","","",   "(D)","","","", 
                    //
                });
            }
            void nb6()
            {

                splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                RegisterFunctionOnce("Texture1", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash3, BeatTime(8.4f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("Texture2", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash4, BeatTime(7.8f - 0.5f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("Texture3", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash5, BeatTime(7.8f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("RGBSplit", () =>
                {
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    //  var production2 = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    splitter.Intensity = 1.0f;
                    //  production2.RandomDisturb = 3.0f;
                    splitter.RandomDisturb = 0;
                    ScreenDrawing.SceneRendering.InsertProduction(splitter);
                    ScreenDrawing.ScreenScale = 1.2f;
                    // ScreenDrawing.SceneRendering.InsertProduction(production2);
                });
                RegisterFunctionOnce("Fade0", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                        var v1 = Shaders.StepSample;
                        var v2 = Shaders.Scale;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        ScreenDrawing.SceneRendering.InsertProduction(production2);
                        v1.Intensity = 0.0f;
                        v2.Intensity = 0.0f;
                        float val = 1.4f;
                        float cur = 0;

                        float beatCount = 7 / 4;
                        float time = BeatTime(beatCount);
                        ForBeat120(beatCount, () =>
                        {
                            val -= 0.5f * (1 + val * 10) / time;
                            cur = cur * 0.7f + val * 0.3f;
                            if (cur < 0) cur = 0;
                            v1.Intensity = cur * 1.4f;
                            v2.Intensity = cur * 0.5f;
                            splitter.Intensity = cur * 40 + 1;
                        });
                        DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                    });
                });
                RegisterFunctionOnce("Fade", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                        var v1 = Shaders.StepSample;
                        var v2 = Shaders.Scale;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        ScreenDrawing.SceneRendering.InsertProduction(production2);
                        v1.Intensity = 0.0f;
                        v2.Intensity = 0.0f;
                        float val = 1.2f;
                        float cur = 0;

                        float beatCount = 7 / 4;
                        float time = BeatTime(beatCount);
                        ForBeat120(beatCount, () =>
                        {
                            val -= 0.5f * (1 + val * 10) / time;
                            cur = cur * 0.7f + val * 0.3f;
                            if (cur < 0) cur = 0;
                            v1.Intensity = cur;
                            v2.Intensity = cur * 0.5f;
                            splitter.Intensity = cur * 40 + 1;
                        });
                        DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                    });
                });
                RegisterFunctionOnce("Fade3", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                        var v1 = Shaders.StepSample;
                        var v2 = Shaders.Scale;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        ScreenDrawing.SceneRendering.InsertProduction(production2);
                        v1.Intensity = 0.0f;
                        v2.Intensity = 0.0f;
                        float val = 0.2f;
                        float cur = 0;

                        float beatCount = 7 / 4;
                        float time = BeatTime(beatCount);
                        ForBeat(beatCount, () =>
                        {
                            val -= 0.75f * (1 + val * 10) / time;
                            cur = cur * 0.6f + val * 0.4f;
                            if (cur < 0) cur = 0;
                            v1.Intensity = cur * 0.3f;
                            v2.Intensity = cur * 0.05f;
                            splitter.Intensity = cur * 30 + 1;
                        });
                        DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                    });
                });
                RegisterFunctionOnce("Fade2", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                        var v1 = Shaders.StepSample;
                        var v2 = Shaders.Scale;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        ScreenDrawing.SceneRendering.InsertProduction(production2);
                        v1.Intensity = 0.0f;
                        v2.Intensity = 0.0f;
                        float val = 0.3f;
                        float cur = 0;

                        float beatCount = 8;
                        float time = BeatTime(beatCount);
                        ForBeat(beatCount, () =>
                        {
                            val -= 0.75f * (1 + val * 10) / time;
                            cur = cur * 0.6f + val * 0.4f;
                            if (cur < 0) cur = 0;
                            v1.Intensity = cur;
                            v2.Intensity = cur * 0.5f;
                            splitter.Intensity = cur * 30 + 1;
                        });
                        DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                    });
                });
                RegisterFunctionOnce("VoidArrow", () =>
                {
                    Arrow[] a = GetAll<Arrow>();
                    for (int i = 0; i < a.Length; i++)
                    {
                        int x = i;
                        a[x].VoidMode = true;
                    }

                });
                RegisterFunctionOnce("MoveCentre", () =>
                {
                    BoxStates.BoxMovingScale = 0.04f;
                    SetGreenBox();

                    ScreenDrawing.UIColor = Color.White;
                    ScreenDrawing.ThemeColor = Color.White;
                    ScreenDrawing.HPBar.HPExistColor = Color.Yellow;
                    ScreenDrawing.HPBar.HPLoseColor = Color.White * 0;

                    ForBeat120(32, () => { TP(BoxStates.Centre); ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.87f + 0.9995f * 0.13f; });
                });
                RegisterFunctionOnce("WhiteOut", () =>
                {
                    ScreenDrawing.WhiteOut(BeatTime(2f));
                });
                RegisterFunctionOnce("SinScreen", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(32), ValueEasing.SinWave(5, BeatTime(16), 0f));
                    ve.Run((m) => { ScreenDrawing.ScreenAngle = m; });
                    DelayBeat(32, () => { ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(4)); });
                });
                BarrageCreate(0, BeatTime(1), 5.8f, new string[]
                {
                "Texture1(RGBSplit)(Fade2)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "Fade","","Texture2","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","Fade","","",
                //
                "Texture3","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","Fade",

                "(VoidArrow)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "MoveCentre(SinScreen)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",
                //
                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","WhiteOut","","",
                "","","","",   "","","","",
                    //
                });
            }
            void nb7()
            {
                BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                {

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",

                "","","","",   "(D)","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "(D)","","","",   "","","","",
                "","","","",   "","","","",
                "(D)","","","",   "","","","",
                "","","","",   "(D)","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                //
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",

                "","","","",   "(D)","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                //
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",

                "","","","",   "(D)","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "(D)","","","",
                "","","","",   "(D)","","","",

                "","","","",   "(D)","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",
                //
                "(D)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(D)","","","",   "","","","",
                "(D)","","","",   "","","","",

                "(D)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //"","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    //

                });
            }

            void nr1()
            {

                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(0, 120, BeatTime(4)), ValueEasing.EaseOutSine(0, 180, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(120, 280, BeatTime(4)), ValueEasing.EaseOutQuad(180, 160, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(280, 260, BeatTime(4)), ValueEasing.EaseOutSine(160, 300, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(260, 320, BeatTime(4)), ValueEasing.EaseOutQuad(300, 360, BeatTime(4))));
                    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(320, 420, BeatTime(4)), ValueEasing.EaseOutQuad(360, 160, BeatTime(4))));
                    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0.5f), new(0, 0.15f)));
                    ValueEasing.EaseBuilder ve1 = new();
                    ve1.Insert(0, ValueEasing.Stable(90));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(90, 90 + 60, BeatTime(4)));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(90 + 60, 40, (BeatTime(4))));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(40, 130, BeatTime(4)));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(130, 160, (BeatTime(4))));
                    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(160, 180 + 45, (BeatTime(4))));
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(0, 60, BeatTime(4)));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(60, 40 - 90, (BeatTime(4))));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(40 - 90, 130 - 90, BeatTime(4)));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(130 - 90, 160 - 90, (BeatTime(4))));
                    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(160 - 90, 90 + 45, (BeatTime(4))));
                    Line l1 = new(ce.GetResult(), ve1.GetResult()) { Alpha = 0.65f };
                    Line l2 = new(ce.GetResult(), ve2.GetResult()) { Alpha = 0.65f };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    LineShadow(1.5f, 0.4f, 8, l1);
                    LineShadow(1.5f, 0.4f, 8, l2);

                    //l1.ObliqueMirror = true;
                    //l2.ObliqueMirror = true;
                });
                RegisterFunctionOnce("star1", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(4), CentreEasing.LerpTo(new(320, -50), 0.0295f, new Vector2(320, 100)));
                    Extends.Star s = new(new Vector2(320, -50), 0.8f) { rotatespeed = 0.5f };
                    e.Run((m) => { s.Centre = new Vector2(0, 0) + m; });
                    s.rotatespeed = 1.8f;
                    CreateEntity(s);
                    DelayBeat(4, () => { s.Dispose(); });
                    ForBeat(4, () => { s.rotatespeed -= 0.005f; });
                });
                RegisterFunctionOnce("star2", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.40f, 0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s = new(e.GetResult(), 0.6f) { rotate = false };
                    CreateEntity(s);
                    e.Run((m) => { s.Centre = new Vector2(320, 100) + m; });
                    s.rotatespeed = 0;
                    ForBeat(16, () =>
                    {
                        s.rotatespeed += 0.1f;
                    });

                });
                RegisterFunctionOnce("star3", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.10f, 0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s = new(new Vector2(320, 100), 0.6f) { rotate = false };
                    CreateEntity(s);
                    e.Run((m) => { s.Centre = new Vector2(320, 100) + m; });
                    CentreEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.10f, -0.61f), Rand(-3.00f, -5.00f)), new(0, 0.15f)));
                    Extends.Star s1 = new(new Vector2(320, 100), 0.6f) { rotate = false };
                    CreateEntity(s1);
                    e1.Run((m) => { s1.Centre = new Vector2(320, 100) + m; });
                    float add = 0;
                    s.rotatespeed = 0;
                    s1.rotatespeed = 0;
                    ForBeat(16, () =>
                    {
                        add += 0.1f;
                        s.rotatespeed -= 0.1f;
                        s1.rotatespeed += 0.1f;
                    });
                });
                RegisterFunctionOnce("fireball1", () =>
                {
                    if (Rand(0, 1) == 0)
                    {

                        CentreEasing.EaseBuilder e = new();
                        e.Insert(0, CentreEasing.Stable(Rand(0, BoxStates.Left - 5), -10));
                        e.Insert(BeatTime(16), CentreEasing.Linear(new Vector2(0, 4.5f)));
                        Fireball f = new(e.GetResult(), 1f);
                        CreateEntity(f);
                        e.Run((m) => { f.Centre = m; });
                        ForBeat(4, () =>
                        {
                            f.Alpha -= 0.02f;
                        });
                    }
                    else if (LastRand == 1)
                    {
                        CentreEasing.EaseBuilder e = new();
                        e.Insert(0, CentreEasing.Stable(Rand(BoxStates.Right + 5, 480), -10));
                        e.Insert(BeatTime(16), CentreEasing.Linear(new Vector2(0, 4.5f)));
                        Fireball f = new(e.GetResult(), 1f);
                        CreateEntity(f);
                        e.Run((m) => { f.Centre = m; });
                        ForBeat(4, () => { f.Alpha -= 0.02f; });
                    }
                });
                RegisterFunctionOnce("line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 500));
                    ce.Insert(BeatTime(8), CentreEasing.Linear(new Vector2(0, -1f)));
                    ValueEasing.EaseBuilder re = new();
                    re.Insert(BeatTime(6), ValueEasing.EaseOutSine(0, 30, BeatTime(6)));
                    ValueEasing.EaseBuilder re2 = new();
                    re2.Insert(BeatTime(6), ValueEasing.EaseOutSine(0, -30, BeatTime(6)));
                    Line l = new(ce.GetResult(), re.GetResult()) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(ce.GetResult(), re2.GetResult()) { DrawingColor = Color.CornflowerBlue };
                    l.AlphaDecrease(BeatTime(5.7f));
                    l2.AlphaDecrease(BeatTime(5.7f));
                    CreateEntity(l);
                    CreateEntity(l2);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                        l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                    });
                });
                RegisterFunctionOnce("line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 0), new(320, BoxStates.Up), BeatTime(8)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 480));
                    ce2.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 480), new(320, BoxStates.Down), BeatTime(8)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(0)) { DrawingColor = Color.CornflowerBlue };
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(0)) { DrawingColor = Color.CornflowerBlue };
                    l.AlphaDecrease(BeatTime(4f));
                    l2.AlphaDecrease(BeatTime(4f));
                    CreateEntity(l);
                    CreateEntity(l2);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                        l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                    });

                });
                RegisterFunctionOnce("line3", () =>
                {
                    ValueEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(2), ValueEasing.EaseOutQuad(30, 160, BeatTime(2)));
                    e.Insert(BeatTime(4), ValueEasing.EaseOutQuart(160, 180 + 45, BeatTime(4)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.Stable(320, 240));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, 0), new(0, 0.12f)));
                    Line l = new(ce.GetResult(), e.GetResult()) { Alpha = 0.75f };
                    l.InsertRetention(new Line.RetentionEffect(0.5f, 0.5f));
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(4f)); });
                    CreateEntity(l);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);

                    });
                    for (int i = 0; i < 12; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.02f));

                    }
                });
                RegisterFunctionOnce("line4", () =>
                {
                    ValueEasing.EaseBuilder e = new();

                    e.Insert(BeatTime(4), ValueEasing.EaseOutQuart(160 + 90, 270 + 45, BeatTime(4)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320, 240));
                    ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0, 0), new(0, 0.12f)));
                    Line l = new(ce.GetResult(), e.GetResult()) { Alpha = 0.75f };
                    for (int i = 0; i < 12; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.02f));

                    }
                    l.AlphaDecrease(BeatTime(4f));
                    CreateEntity(l);
                    ForBeat(6, () =>
                    {
                        l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);

                    });

                });
                RegisterFunctionOnce("line5", () =>
                {
                    Color[] c =
                    {
                    Color.Red,Color.Orange,Color.Yellow,Color.Green,Color.Cyan,Color.Blue,Color.Purple,Color.White
                    };
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        DelayBeat(i * 0.5f, () =>
                        {

                            ValueEasing.EaseBuilder e = new();
                            e.Insert(BeatTime(0), ValueEasing.Stable(90));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(BeatTime(0), CentreEasing.Stable(200 + 600 / 9 * (x), 240));
                            ce.Insert(BeatTime(6), CentreEasing.Linear(-6f));
                            Line l = new(ce.GetResult(), e.GetResult()) { DrawingColor = c[x], Alpha = 0.5f };
                            l.InsertRetention(new Line.RetentionEffect(0.5f, 0.5f));
                            l.AlphaDecrease(BeatTime(5f));
                            CreateEntity(l);
                            ForBeat(8, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.05f);

                            });
                            for (int i = 0; i < 24; i++)
                            {
                                int t = i;
                                l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.01f));

                            }
                        });
                    }


                });
                RegisterFunctionOnce("star4", () =>
                {
                    CentreEasing.EaseBuilder e = new();
                    e.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(0.10f, 0.41f), -10), new(0, 0.17f)));
                    Extends.Star s = new(new Vector2(0, 0), 0.6f) { rotate = false };
                    CreateEntity(s);
                    int a = Rand(-60, -20);
                    int b = Rand(20, 60);
                    e.Run((m) => { s.Centre = new Vector2(320 + a, 510) + m; });
                    CentreEasing.EaseBuilder e1 = new();
                    e1.Insert(BeatTime(8), CentreEasing.Accerlating(new(Rand(-0.41f, -0.10f), -10), new(0, 0.17f)));
                    Extends.Star s1 = new(new Vector2(0, 0), 0.6f) { rotate = false };
                    CreateEntity(s1);
                    e1.Run((m) => { s1.Centre = new Vector2(320 + b, 510) + m; });

                    s.rotatespeed = 0;
                    s1.rotatespeed = 0;
                    ForBeat(16, () =>
                    {

                        s.rotatespeed -= 0.07f;
                        s1.rotatespeed += 0.07f;
                    });

                });
                RegisterFunctionOnce("return", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });

                RegisterFunctionOnce("RotateL", () =>
                {
                    ScreenDrawing.CameraEffect.RotateTo(-2.3f, BeatTime(4f));
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    ScreenDrawing.CameraEffect.RotateTo(2.3f, BeatTime(4f));
                });
                RegisterFunctionOnce("RotateReturn", () =>
                {
                    ScreenDrawing.CameraEffect.RotateTo(0f, BeatTime(4f));
                });
                RegisterFunctionOnce("BoxSin", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(240));
                    ve.Insert(BeatTime(2f), ValueEasing.EaseOutQuad(240, 210, BeatTime(2f)));
                    ve.Insert(BeatTime(22), ValueEasing.SinWave(23, BeatTime(8), 0.5f));
                    ve.Insert(BeatTime(2f), ValueEasing.EaseOutQuad(210, 260, BeatTime(2f)));
                    float b = 0;
                    ve.Run((m) => { b = m; });
                    ForBeat(28, () => { InstantSetBox(b, 84, 84); InstantTP(320, b); });

                });
                RegisterFunctionOnce("WhiteShake", () =>
                {
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        DelayBeat(i * 0.5f, () =>
                        {
                            Color[] c =
                            {
                            Color.Red,
                            Color.Orange,
                            Color.Yellow,
                            Color.Green,
                            Color.Cyan,
                            Color.Blue,
                            Color.Purple,
                            Color.White
                             };
                            ScreenDrawing.MakeFlicker(c[x] * 0.35f);
                        });
                    }
                });
                RegisterFunctionOnce("Effect1", () =>
                {
                    Color[] c =
                    {
                    Color.Red,Color.Orange,Color.Yellow,Color.Green,Color.Cyan,Color.Blue,Color.Purple,Color.White
                    };
                    ValueEasing.EaseBuilder e = new();
                    ValueEasing.EaseBuilder e2 = new();
                    ValueEasing.EaseBuilder e3 = new();
                    e.Insert(BeatTime(1), ValueEasing.LerpTo(1, 1.1f, 0.04f));
                    e.Run((m) => { ScreenDrawing.ScreenScale = m; });
                    for (int i = 0; i < 8; i++)
                    {
                        int x = i;
                        DelayBeat(i * 0.5f, () =>
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(90));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(0, 0));
                            ce.Insert(BeatTime(1.5f), CentreEasing.LerpTo(new Vector2(0, 0), 0.06f, new Vector2(760, 0)));
                            Line l = new(ce.GetResult(), ve.GetResult()) { DrawingColor = c[x] };
                            CreateEntity(l);
                            ScreenDrawing.CameraEffect.Convulse(10, true);
                            for (int i = 0; i < 3; i++)
                            {
                                int t = i;
                                l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.06f));

                            }


                        });
                    }
                });
                BarrageCreate(0, BeatTime(1), 6, new string[]
                {
                //
                "star1(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star2(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star3","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "star1(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star3(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star2","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "star4(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",
                "star4(fireball1)(line1)","","","",   "","","","",
                "(fireball1)(line1)","","","",   "","","","",

                "star4","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "(star4)","","","",   "","","","",

                "line2(return)(star4)","","","",   "","","","",
                "","","","",   "","","","",
                "$31(line3)","","","",   "","","","",
                "","","","",   "","","","",

                "$31($0)(line4)(line5)","","","",   "$1","","","",
                "$2","","","",   "$1","","","",
                "$2","","","",   "$2","","","",
                "$3","","","",   "$3","","","",
                //
                "(^$31'1.5)N3(RotateL)(BoxSin)(Line1)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(^$31'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$11'1.5)N3(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "(^$11'1.5)N3(RotateL)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(^$11'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$31'1.5)N3(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(^$01'1.5)N3(RotateL)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "(+0)(^$01'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$21'1.5)N3(RotateR)","","","",   "+0","","","",
                "+0","","","",   "+0","","","",
                "+0","","","",   "","","","",
                "(^N31'1.5)(^N3'1.5)","","","",   "","","","",

                "(^N31'1.5)(^N3'1.5)(RotateReturn)","","","",   "","","","",
                "","","","",   "","","","",
                "(^N31'1.5)(^N3'1.5)","","","",   "","","","",
                "","","","",   "","","","",

                "(^$2'1.5)(WhiteShake)(Effect1)","","","",  "(^$21'1.5)(^+2'1.5)","","","",
                "(^$21'1.5)(^+2'1.5)","","","",   "(^$21'1.5)(^+2'1.5)","","","",
                "(^$01'1.5)(^+2'1.5)","","","",   "(^$01'1.5)(^+2'1.5)","","","",
                "(^$01'1.5)(^+2'1.5)","","","",   "(^$01'1.5)(^+2'1.5)","","","",
                    //
                });
            }
            void nr2()
            {
                RegisterFunctionOnce("SinLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(70, 0));
                    ce.Insert(BeatTime(64), CentreEasing.XSinWave(30, BeatTime(6), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(64), ValueEasing.Linear(0, 360, BeatTime(8)));
                    ve.Run((m) => { l.DrawingColor = new Color(DrawingLab.HsvToRgb(m, 255, 255, 0)); });
                    l.TransverseMirror = true;
                    for (int i = 0; i < 48; i++)
                    {
                        int t = i;
                        l.InsertRetention(new(t * 0.5f, 0.24f - t * 0.005f));

                    }

                    DelayBeat(60, () => { l.AlphaDecrease(BeatTime(4)); });
                    ValueEasing.EaseBuilder box = new();
                    box.Insert(0, ValueEasing.Stable(200));
                    box.Insert(BeatTime(60), ValueEasing.SinWave(8, BeatTime(4), 0.5f));
                    box.Run((m) => { InstantSetBox(240, m, 488); });
                    ValueEasing.EaseBuilder down = new();
                    down.Insert(0, ValueEasing.Stable(100));
                    down.Insert(BeatTime(57) - 4, ValueEasing.SinWave(70, BeatTime(8), 0f));
                    down.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                    ScreenDrawing.BoundColor = Color.White * 0.23f;
                    ForBeat(60, () =>
                    {

                        if ((int)GametimeF % 60 == 0)
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(8));
                            ve.Insert(BeatTime(4), ValueEasing.EaseInSine(8, 12, BeatTime(4)));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(320, 560));
                            ce.Insert(BeatTime(16), CentreEasing.Accerlating(new(0, 0f), new(0, -0.175f)));
                            Line l2 = new(ce.GetResult(), ve.GetResult());
                            CreateEntity(l2);
                            l2.DrawingColor = Color.CornflowerBlue;
                            l2.Width = 2.7f;
                            ForBeat(6, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                                l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                            });
                            LineShadow(2.5f, 0.75f, 6, l2);
                            l2.Alpha = 0.5f;
                        }
                        if ((int)GametimeF % 60 == 30)
                        {
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(0, ValueEasing.Stable(-8));
                            ve.Insert(BeatTime(4), ValueEasing.EaseInSine(-8, -12, BeatTime(4)));
                            CentreEasing.EaseBuilder ce = new();
                            ce.Insert(0, CentreEasing.Stable(320, 560));
                            ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0f), new(0, -0.175f)));
                            Line l2 = new(ce.GetResult(), ve.GetResult());
                            CreateEntity(l2);
                            l2.DrawingColor = Color.CornflowerBlue;
                            l2.Width = 2.7f;
                            ForBeat(6, () =>
                            {
                                l.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                                l2.DrawingColor = Color.Lerp(l.DrawingColor, Color.White, 0.025f);
                            });
                            LineShadow(2.5f, 0.75f, 4, l2);
                            l2.Alpha = 0.5f;
                        }
                    });
                });
                RegisterFunctionOnce("Light", () =>
                {
                    ScreenDrawing.WhiteOut(BeatTime(2));
                    SetBox(240, 200, 480);
                    SetSoul(0);
                    TP();
                    Extends.DrawingUtil.LerpScreenScale(60, 1, 0.09f);
                    CentreEasing.EaseBuilder ce = new();
                    Vector2 Value = ScreenDrawing.HPBar.AreaOccupied.GetCentre();
                    ce.Insert(0, CentreEasing.Stable(ScreenDrawing.HPBar.AreaOccupied.GetCentre()));
                    ce.Insert(0, CentreEasing.EaseOutQuad(Value, new(20, 480 - 60), BeatTime(2)));
                    ce.Run((m) => { ScreenDrawing.HPBar.AreaOccupied = new(m, ScreenDrawing.HPBar.AreaOccupied.Size); });
                });
                RegisterFunction("BHStar", () =>
                {
                    int max = 8;
                    int RotateRand = Rand(0, 359);
                    for (int a = 0; a < max; a++)
                    {
                        int value = a;
                        Star s = new(new Vector2(0, 0), 0.5f) { ColorType = 1 };
                        CreateEntity(s);
                        CentreEasing.EaseBuilder ce = new();
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(0, ValueEasing.Stable(360));
                        ve1.Insert(BeatTime(12), ValueEasing.Linear(360, -360, BeatTime(6)));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(0, ValueEasing.Stable(RotateRand + 360 / max * value));
                        ve2.Insert(BeatTime(16), ValueEasing.Linear(0, 1080 / 2, BeatTime(16)));
                        float valueN = 0;
                        ve1.Run((m) =>
                        {

                            s.Centre = new Vector2(320, 240) + new Vector2(Cos(valueN) * m, Sin(valueN) * m);

                        });
                        ve2.Run((n) =>
                        {
                            valueN = n;
                        });
                    }
                });
                int k = 0;
                RegisterFunctionOnce("Fireball1", () =>
                {
                    int val = Rand(-1, 1);
                    while (val == k)
                    {
                        val = Rand(-1, 1);
                    }
                    k = val;
                    float ct = k * 66 + 320;
                    float delta = Rand(0, 1f);
                    for (int a = 0; a < 16; a++)
                    {
                        int value = a;
                        Fireball f = new(new Vector2(0, 0), 1);
                        CreateEntity(f);
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(BeatTime(32), ValueEasing.SinWave(35, -BeatTime(4), 1f / 12f * value + delta));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(BeatTime(32), ValueEasing.Linear(-3.6f));
                        float valueN = 0;
                        ve1.Run((m) =>
                        {
                            f.Centre = new(ct + m, 488 + 13 * value + valueN);
                        });
                        ve2.Run((n) =>
                        {
                            valueN = n;
                        });
                        Fireball f2 = new(new Vector2(0, 0), 1);
                        CreateEntity(f2);
                        ValueEasing.EaseBuilder ve3 = new();
                        ve3.Insert(BeatTime(32), ValueEasing.SinWave(35, -BeatTime(4), 1f / 12f * value + delta));
                        ValueEasing.EaseBuilder ve4 = new();
                        ve4.Insert(BeatTime(32), ValueEasing.Linear(-3.6f));
                        float valueN2 = 0;
                        ve3.Run((m) =>
                        {
                            f2.Centre = new(ct - m, 488 + 13 * value + valueN2);
                        });
                        ve4.Run((n) =>
                        {
                            valueN2 = n;
                        });
                    }
                });

                RegisterFunctionOnce("Over", () =>
                {
                    SetSoul(1);
                    SetBox(240, 84, 84);
                    ScreenDrawing.WhiteOut(BeatTime(4));
                    TP();
                    ValueEasing.EaseBuilder down = new();
                    down.Insert(BeatTime(1), ValueEasing.LerpTo(100 + 35, 0, 0.1f));
                    down.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                });
                BarrageCreate(0, BeatTime(1), 6, new string[]
                {
                "(Light)","","","",  "","","","",
                "","","","",  "","","","",
                "(SinLine)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                //
                "(Fireball1)(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseR)","","","",  "","","","",
                "","","","",  "","","","",

                "(Fireball1)(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",
                "(ConvulseL)","","","",  "","","","",
                "","","","",  "","","","",

                "(BHStar)(Over)","","","",  "","","","",
                "","","","",  "","","","",
                "","","","",  "","","","",
                "","","","",  "","","","",
                    //
                });
            }
            void nr3()
            {
                Player.Heart heart1 = Heart;
                Player.Heart heart2 = CreateHeart(new CollideRect(380, 180, 84, 84));
                RegisterFunctionOnce("Start", () =>
                {
                    SetPlayerBoxMission(heart1);
                    SetSoul(1);

                    TP(320 - 60, 240 - 180 + 240);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(30));
                    for (int a = 0; a < 8; a++)
                        ve.Insert(BeatTime(4), ValueEasing.EaseOutElastic(30 + a * 30, 30 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve.Insert(BeatTime(8), ValueEasing.EaseOutBack(30 + a * 30, 30 + (a + 1) * 30, BeatTime(8)));
                    ve.Run((m) => { SetPlayerBoxMission(heart1); InstantTP(new Vector2(320, 240) + GetVector2(85, m)); InstantSetBox(new Vector2(320, 240) + GetVector2(85, m), 84, 84); });
                    SetPlayerBoxMission(heart2);
                    SetSoul(1);
                    TP(320 - 60, 240 - 180 + 240);
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(0, ValueEasing.Stable(30));
                    for (int a = 0; a < 8; a++)
                        ve2.Insert(BeatTime(4), ValueEasing.EaseOutElastic(30 + a * 30, 30 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve2.Insert(BeatTime(8), ValueEasing.EaseOutBack(30 + a * 30, 30 + (a + 1) * 30, BeatTime(8)));
                    ve2.Run((m) => { SetPlayerBoxMission(heart2); InstantTP(new Vector2(320, 240) + GetVector2(85, 180 + m)); InstantSetBox(new Vector2(320, 240) + GetVector2(85, 180 + m), 84, 84); });
                    DelayBeat(0, () =>
                    {
                        heart1.Shields.RemoveShield(heart1.Shields.BShield);
                        heart2.Shields.RemoveShield(heart2.Shields.RShield);
                    });
                });
                RegisterFunctionOnce("CentreLine", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(75));
                    for (int a = 0; a < 8; a++)
                        ve.Insert(BeatTime(4), ValueEasing.EaseOutElastic(75 + 30 * a, 75 + (a + 1) * 30, BeatTime(6)));
                    for (int a = 0; a < 4; a++)
                        ve.Insert(BeatTime(8), ValueEasing.EaseOutBack(75 + 30 * a, 75 + (a + 1) * 30, BeatTime(8)));
                    Line l = new(CentreEasing.Stable(320, 240), ve.GetResult());
                    ValueEasing.EaseBuilder ve2 = new();
                    ve2.Insert(BeatTime(128), ValueEasing.Linear(0, 360, BeatTime(8)));
                    ve2.Run((m) => { l.DrawingColor = new Color(DrawingLab.HsvToRgb(m, 255, 255, 0)); });
                    LineShadow(0.5f, 8, l);
                    CreateEntity(l);
                    DelayBeat(60, () => { l.AlphaDecrease(BeatTime(4)); });
                });
                RegisterFunctionOnce("Screen1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(0, 0), new(-18, 4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(-18, 4), new(18, 4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(18, 4), new(18, -4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Screen4", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(6), CentreEasing.EaseOutQuad(new(18, -4), new(-18, -4), BeatTime(6)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("ScreenOver", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(-14, -4), new(0, 0), BeatTime(2)));
                    ce.Run((m) => { ScreenDrawing.ScreenPositionDetla = m; });
                });
                RegisterFunctionOnce("Return", () =>
                {
                    SetPlayerBoxMission(heart2);
                    Vector2 boxc2 = BoxStates.Centre;
                    SetPlayerBoxMission(heart1);
                    Vector2 boxc = BoxStates.Centre;
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1) - 2, CentreEasing.Alternate(2, new Func<ICustomMotion, Vector2>[]
                    {
                    CentreEasing.Linear(boxc,boxc2,2),
                    CentreEasing.Linear(boxc2,boxc,2)
                    }));
                    ce.Run((m) => { SetPlayerBoxMission(heart1); InstantSetBox(m, 84, 84); InstantTP(m); });
                    DelayBeat(1, () => { ScreenDrawing.MakeFlicker(Color.White); });
                    heart2.Merge(heart1);
                    SetPlayerBoxMission(heart1);
                    heart1.Shields.AddShield(heart1.Shields.BShield);
                    heart1.Shields.AddShield(heart1.Shields.RShield);
                    SetSoul(1);
                    ForBeat(1.125f, 5, () =>
                    {
                        SetPlayerBoxMission(heart1);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        InstantTP(320, 240);
                    });

                });
                RegisterFunctionOnce("Merge", () =>
                {
                    heart2.Merge(heart1);
                    SetPlayerBoxMission(heart1);
                    heart1.Shields.AddShield(heart1.Shields.BShield);
                    heart1.Shields.AddShield(heart1.Shields.RShield);
                    SetSoul(1);
                    DelayBeat(1f, () =>
                    {
                        SetBox(new Vector2(320, 240), 84, 84);
                        TP(320, 240);
                    });

                });
                RegisterFunctionOnce("WhiteBound1", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.DownBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 130, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(130, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.DownBoundDistance = m; });
                });
                RegisterFunctionOnce("WhiteBound2", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.RightBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 200, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(200, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.RightBoundDistance = m; });
                });
                RegisterFunctionOnce("WhiteBound3", () =>
                {
                    ScreenDrawing.BoundColor = Color.White * 0.5f;
                    ValueEasing.EaseBuilder ve = new();
                    ScreenDrawing.LeftBoundDistance = 0;
                    ve.Insert(BeatTime(7f), ValueEasing.EaseOutSine(0, 200, BeatTime(7f)));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(200, 0, BeatTime(1f)));
                    ve.Run((m) => { ScreenDrawing.LeftBoundDistance = m; });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(0, 240));
                    ce1.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(0, 240), new(320 + 120, 240), BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1.75f));
                    LineShadow(2.3f, 0.75f, 4, l1);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 240));
                    ce1.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(640, 240), new(320 - 120, 240), BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1.75f));
                    LineShadow(2.3f, 0.75f, 4, l1);
                });
                SetPlayerBoxMission(heart1);
                BarrageCreate(0, BeatTime(1), 5, new string[]
                {
                "Start(CentreLine)",""
                });
                SetPlayerBoxMission(heart2);
                BarrageCreate(0, BeatTime(1), 4.5f, new string[]
                {
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "N1","","","",   "","","","",
                "N1","","","",   "","","","",
                "N1","","","",   "","","","",
                "N1","","","",   "","","","",

                "N1","","","",   "","","","",
                "N1","","","",   "N1","","","",
                "N1","","","",   "","","","",
                "N1","","","",   "","","","",

                "N1","","","",   "","","","",
                "N1","","","",   "","","","",
                "N1","","","",   "N1","","","",
                "N1","","","",   "","","","",
                //
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "#7#$0(Screen1)(RRotate)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "(Screen2)(LRotate)","","","",   "","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "","","","",

                "N0","","","",   "N0","","","",
                "N0","","","",   "","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "","","","",
                //
                "#7#$1(Screen3)(LRotate)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "(Screen4)(RRotate)","","","",   "","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "N0","","","",
                "N0","","","",   "","","","",

                "N0","","","",   "","","","",
                "N0(BHStar)","","","",   "","","","",
                "N0(ScreenOver)(MidRotate)","","","",   "N0","","","",
                "N0(Return)","","","",   "","","","",

                ""
                });
                SetPlayerBoxMission(heart1);
                BarrageCreate(0, BeatTime(1), 4.5f, new string[]
                {
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(LRotate)(Line1)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(RRotate)(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(LRotate)(Line1)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "N01","","","",   "","","","",
                "N01","","","",   "","","","",
                "N01(RRotate)(Line2)","","","",   "","","","",
                "N01","","","",   "","","","",

                "N01","","","",   "","","","",
                "N01","","","",   "","","","",
                "N01(LRotate)(Line1)","","","",   "","","","",
                "N01","","","",   "","","","",

                "N01","","","",   "","","","",
                "N01","","","",   "N01","","","",
                "N01(RRotate)(Line2)","","","",   "","","","",
                "N01","","","",   "","","","",

                "N11","","","",   "","","","",
                "N11","","","",   "","","","",
                "N11(MidRotate)(Line1)","","","",   "N11","","","",
                "N11","","","",   "","","","",
                //
                "(WhiteBound1)","","","",   "","","","",
                "N11","","","",   "","","","",
                "N11(Line2)","","","",   "","","","",
                "N11","","","",   "","","","",

                "N11","","","",   "","","","",
                "N11","","","",   "","","","",
                "N11(Line1)","","","",   "","","","",
                "N11","","","",   "","","","",

                "#7#$11(WhiteBound2)","","","",   "","","","",
                "","","","",   "","","","",
                "(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(Line1)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(WhiteBound3)","","","",   "","","","",
                "N21","","","",   "","","","",
                "N21(Line2)","","","",   "","","","",
                "N21","","","",   "","","","",

                "N21","","","",   "","","","",
                "N21","","","",   "","","","",
                "N21(Line1)","","","",   "","","","",
                "N21","","","",   "","","","",

                "#7#$31(WhiteBound2)","","","",   "","","","",
                "","","","",   "","","","",
                "(Line2)","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    //
                });
            }
            void nr4()
            {
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(0, 240));
                    ce1.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new Vector2(0, 240), new Vector2(640, 240), BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.7f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1f));
                    LineShadow(0.7f, 5, l1);
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 240));
                    ce1.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new Vector2(640, 240), new Vector2(0, 240), BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.7f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1f));
                    LineShadow(0.7f, 5, l1);
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(320 + 20, 480));
                    ce.Insert(BeatTime(4), CentreEasing.LerpTo(new Vector2(320 + 20, 480), 0.07f, new Vector2(320 + 20 + 90, 480)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 240 - 15));
                    ce2.Insert(BeatTime(4), CentreEasing.LerpTo(new Vector2(0, 240 - 15), 0.07f, new Vector2(0, 240 - 15 - 90f / 4f * 3f)));
                    Line l1 = new(ce.GetResult(), ce2.GetResult()) { Alpha = 0.8f };
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(0.78f));
                    l1.ObliqueMirror = true;
                    l1.TransverseMirror = true;
                    l1.VerticalMirror = true;
                });
                RegisterFunctionOnce("Line4", () =>
                {
                    Line l1 = new(CentreEasing.Stable(Rand(0, 640), 240), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(2));
                });
                RegisterFunctionOnce("Camera1", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6.9f, BeatTime(0.78f), true);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.SinWave(0.01f, BeatTime(1f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera2", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6.9f, BeatTime(0.78f), false);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.SinWave(0.01f, BeatTime(1f), 0.5f));
                    // ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera3", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(10, BeatTime(2f), false);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.8f), ValueEasing.SinWave(0.03f, BeatTime(1.8f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                RegisterFunctionOnce("Camera4", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(10, BeatTime(2f), true);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.8f), ValueEasing.SinWave(0.03f, BeatTime(1.8f), 0.5f));
                    //ve.Run((m) => { ScreenDrawing.ScreenScale = m; });
                });
                SetPlayerBoxMission(0);
                BarrageCreate(BeatTime(0), BeatTime(1), 5.4f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                "($0)($01)","","","",   "","","","",
                "($2)($21)","","","",   "","","","",

                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(Line4)","","(+1)(Line4)","",
                "(+1)(Line4)","","","",   "","","","",

                "(R)(+01)","","","",   "","","","",
                "(D)(-01)","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "(R)(Line4)","","(+1)(Line4)","",
                "(+1)(Line4)","","","",   "","","","",
                "($1)(^+0'1.5)(+21)(^+01'1.5)(Line3)","","","",   "","","","",
                "($3)(^+0'1.5)(+21)(^+01'1.5)(Line3)","","","",   "","","","",
                //
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "($01)($0)","","","",   "","","","",
                "($21)($2)","","","",   "","","","",

                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",
                "","","","",   "(R1)","","(+11)(Line4)","",
                "(+11)(Line4)","","","",   "","","","",

                "(R)(+01)","","","",   "","","","",
                "(D)(-01)","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "(R1)(Line4)","","(+11)(Line4)","",
                "(+11)(Line4)","","","",   "","","","",
                "($0)(^+0'1.5)($0)(^+01'1.5)(Line3)","","","",   "","","","",
                "($2)(^+0'1.5)($2)(^+01'1.5)(Line3)","","","",   "","","","",
                //
                "(R)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(-01)(+0)","","","",
                "(+1)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(+0)","","","",

                "(R)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(-01)(+0)","","","",
                "(+1)(+01)","","","",   "(-0)(-01)(Line4)","","(+01)(Line4)","",
                "(+1)(-01)(Line4)","","","",   "(+0)","","","",

                "(R)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(-01)(+0)","","","",
                "(+1)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(+0)","","","",

                "(#1.37#R)(#1.37#+01)(Camera3)","","","",   "","","","",
                "","","","",   "","","","",
                "(#1.37#R)(#1.37#+01)(Camera4)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(R)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(-01)(+0)","","","",
                "(+1)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(+0)","","","",

                "(R)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(-01)(+0)","","","",
                "(+1)(+01)","","","",   "(-0)(-01)(Line4)","","(+01)(Line4)","",
                "(+1)(-01)(Line4)","","","",   "(+0)","","","",

                "(R)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(-01)(+0)","","","",
                "(+1)(+01)","","","",   "(-0)","","","",
                "(+1)(-01)","","","",   "(+0)","","","",

                "(#1.37#R)(#1.37#+01)(Camera3)","","","",   "","","","",
                "","","","",   "","","","",
                "(#1.37#R)(#1.37#+01)(Camera4)","","","",   "","","","",
                "","","","",   "","","","",
                //
                "(D1)(+0)(Line1)","","","",   "+0(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "(+0)(+0)(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "+0(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "+0(Line1)","","","",

                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",
                "(D1)(+0)(Line2)","","","",   "(+0)(+0)(Line2)","","","",
                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",
                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D1)(+0)(Line1)","","","",   "+0(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "(+0)(+0)(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "+0(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "+0(Line1)","","","",

                "(#1.37#R)(Camera4)","","","",   "","","","",
                "+0(Line3)","","","",   "","","","",
                "(#1.37#R)(Camera3)","","","",   "","","","",
                "+0(Line3)","","","",   "","","","",
                //
                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",
                "(D)(+01)(Line2)","","","",   "(+01)(+0)(Line2)","","","",
                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",
                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",

                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D)(+01)(Line1)","","","",   "(+01)(+0)(Line1)","","","",
                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",

                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",
                "(D)(+01)(Line2)","","","",   "(+01)(+0)(Line2)","","","",
                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",
                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",

                "(#1.37#R1)","","","",   "","","","",
                "+01(Line3)","","","",   "","","","",
                "(#1.37#R1)","","","",   "","","","",
                "+01(Line3)","","","",   "","","","",
                //
                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "(+0)(+01)(Line1)","","","",
                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",
                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "(+0)(+01)(Line1)","","","",
                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",
                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line1)","","","",   "(+0)(+01)(Line1)","","","",
                "(D)(+01)(Line2)","","","",   "+01(Line2)","","","",
                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(#1.67#R1)(Camera3)","","","",   "","","","",
                "D(Line3)","","","",   "","","","",
                "(#1.67#R)(Camera4)","","","",   "","","","",
                "D1(Line3)","","","",   "","","","",
                //
                 "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line2)","","","",   "(+0)(+01)(Line2)","","","",
                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line2)","","","",   "(+0)(+01)(Line2)","","","",
                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line2)","","","",   "(+0)(+01)(Line2)","","","",
                "(D)(+01)(Line1)","","","",   "+01(Line1)","","","",
                "(D1)(+0)(Line2)","","","",   "+0(Line2)","","","",

                "(#1.37#R1)(Camera4)","","","",   "","","","",
                "+01(Line3)","","","",   "","","","",
                "(#1.37#R)(Camera3)","","","",   "","","","",
                "+0(Line3)","","","",   "","","","",
                //
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    });

            }
            void nr5()
            {
                RegisterFunctionOnce("LConvulse", () =>
                {
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(-3, 0, BeatTime(0.5f)));
                    builder.Run(s => ScreenDrawing.ScreenAngle = s);
                });
                RegisterFunctionOnce("RConvulse", () =>
                {
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutCubic(3, 0, BeatTime(0.5f)));
                    builder.Run(s => ScreenDrawing.ScreenAngle = s);
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 5.8f, new string[]
                {
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",

                "(D)(+01)","","","",   "(D)(+01)","","","",
                "","","","",   "(D)(+01)","","","",
                "","","","",    "(D)(+0)","","+1","",
                "+1","","","",   "","","","",

                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","",
                "","","","",   "(D)(+01)","","","",

                "","","","",    "(D)(+0)","","+1","",
                "+1","","","",   "","","","",
                "(*^$20'2)(*^$00'2)(LConvulse)","","","",   "","","","",
                "(*^$21'2)(*^$01'2)(RConvulse)","","","",   "","","","",
                //
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",

                "(D)(+01)","","","",   "(D)(+01)","","","",
                "","","","",   "(D)(+01)","","","",
                "","","","",    "(D)(+0)","","+1","",
                "+1","","","",   "","","","",

                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","",
                "","","","",   "(D)(+01)","","","",

                "","","","",   "(D)(+01)","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","", 
                //
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",

                "(D)(+01)","","","",   "(D)(+01)","","","",
                "","","","",   "(D)(+01)","","","",
                "","","","",    "(D1)(+01)","","+11","",
                "+11","","","",   "","","","",

                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","",
                "","","","",   "(D)(+01)","","","",

                "","","","",    "(D1)(+01)","","+11","",
                "+11","","","",   "","","","",
                "(*^$21'2)(*^$01'2)(LConvulse)","","","",   "","","","",
                "(*^$20'2)(*^$00'2)(RConvulse)","","","",   "","","","",
                //
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",

                "(D)(+01)","","","",   "(D)(+01)","","","",
                "","","","",   "(D)(+01)","","","",
                "","","","",    "(D1)(+01)","","+11","",
                "+11","","","",   "","","","",

                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","",
                "","","","",   "(D)(+01)","","","",

                "","","","",   "(D)(+01)","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","",
                "(D)(+01)","","","",   "(D)(+01)","","","", 
                    //
                });
            }
            void nr6()
            {

                splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                RegisterFunctionOnce("Texture1", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash3, BeatTime(8.4f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("Texture2", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash4, BeatTime(7.8f - 0.5f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("Texture3", () =>
                {
                    CreateEntity(new Image(Resources.Championships.Memory2022.Flash5, BeatTime(7.8f)) { FadeSpeed = 0.32f });
                });
                RegisterFunctionOnce("RGBSplit", () =>
                {
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    //  var production2 = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    splitter.Intensity = 1.0f;
                    //  production2.RandomDisturb = 3.0f;
                    splitter.RandomDisturb = 0;
                    ScreenDrawing.SceneRendering.InsertProduction(splitter);
                    ScreenDrawing.ScreenScale = 1.2f;
                    // ScreenDrawing.SceneRendering.InsertProduction(production2);
                });
                RegisterFunctionOnce("Fade0", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                        var v1 = Shaders.StepSample;
                        var v2 = Shaders.Scale;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        ScreenDrawing.SceneRendering.InsertProduction(production2);
                        v1.Intensity = 0.0f;
                        v2.Intensity = 0.0f;
                        float val = 1.4f;
                        float cur = 0;

                        float beatCount = 7 / 4;
                        float time = BeatTime(beatCount);
                        ForBeat120(beatCount, () =>
                        {
                            val -= 0.5f * (1 + val * 10) / time;
                            cur = cur * 0.7f + val * 0.3f;
                            if (cur < 0) cur = 0;
                            v1.Intensity = cur * 1.4f;
                            v2.Intensity = cur * 0.5f;
                            splitter.Intensity = cur * 40 + 1;
                        });
                        DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                    });
                });
                RegisterFunctionOnce("Fade", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                        var v1 = Shaders.StepSample;
                        var v2 = Shaders.Scale;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        ScreenDrawing.SceneRendering.InsertProduction(production2);
                        v1.Intensity = 0.0f;
                        v2.Intensity = 0.0f;
                        float val = 1.2f;
                        float cur = 0;

                        float beatCount = 7 / 4;
                        float time = BeatTime(beatCount);
                        ForBeat120(beatCount, () =>
                        {
                            val -= 0.5f * (1 + val * 10) / time;
                            cur = cur * 0.7f + val * 0.3f;
                            if (cur < 0) cur = 0;
                            v1.Intensity = cur;
                            v2.Intensity = cur * 0.5f;
                            splitter.Intensity = cur * 40 + 1;
                        });
                        DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                    });
                });
                RegisterFunctionOnce("Fade3", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                        var v1 = Shaders.StepSample;
                        var v2 = Shaders.Scale;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        ScreenDrawing.SceneRendering.InsertProduction(production2);
                        v1.Intensity = 0.0f;
                        v2.Intensity = 0.0f;
                        float val = 0.2f;
                        float cur = 0;

                        float beatCount = 7 / 4;
                        float time = BeatTime(beatCount);
                        ForBeat(beatCount, () =>
                        {
                            val -= 0.75f * (1 + val * 10) / time;
                            cur = cur * 0.6f + val * 0.4f;
                            if (cur < 0) cur = 0;
                            v1.Intensity = cur * 0.3f;
                            v2.Intensity = cur * 0.05f;
                            splitter.Intensity = cur * 30 + 1;
                        });
                        DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                    });
                });
                RegisterFunctionOnce("Fade2", () =>
                {
                    DelayBeat(0.7f / 4, () =>
                    {
                        var v1 = Shaders.StepSample;
                        var v2 = Shaders.Scale;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        RenderProduction production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.45f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        ScreenDrawing.SceneRendering.InsertProduction(production2);
                        v1.Intensity = 0.0f;
                        v2.Intensity = 0.0f;
                        float val = 0.3f;
                        float cur = 0;

                        float beatCount = 8;
                        float time = BeatTime(beatCount);
                        ForBeat(beatCount, () =>
                        {
                            val -= 0.75f * (1 + val * 10) / time;
                            cur = cur * 0.6f + val * 0.4f;
                            if (cur < 0) cur = 0;
                            v1.Intensity = cur;
                            v2.Intensity = cur * 0.5f;
                            splitter.Intensity = cur * 30 + 1;
                        });
                        DelayBeat(beatCount, () => { production1.Dispose(); production2.Dispose(); });
                    });
                });
                RegisterFunctionOnce("VoidArrow", () =>
                {
                    Arrow[] a = GetAll<Arrow>();
                    for (int i = 0; i < a.Length; i++)
                    {
                        int x = i;
                        a[x].VoidMode = true;
                    }

                });
                RegisterFunctionOnce("MoveCentre", () =>
                {
                    BoxStates.BoxMovingScale = 0.04f;
                    SetGreenBox();

                    ScreenDrawing.UIColor = Color.White;
                    ScreenDrawing.ThemeColor = Color.White;
                    ScreenDrawing.HPBar.HPExistColor = Color.Yellow;
                    ScreenDrawing.HPBar.HPLoseColor = Color.White * 0;

                    ForBeat120(32, () => { TP(BoxStates.Centre); ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.87f + 0.9995f * 0.13f; });
                });
                RegisterFunctionOnce("WhiteOut", () =>
                {
                    ScreenDrawing.WhiteOut(BeatTime(2f));
                });
                RegisterFunctionOnce("SinScreen", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(32), ValueEasing.SinWave(5, BeatTime(16), 0f));
                    ve.Run((m) => { ScreenDrawing.ScreenAngle = m; });
                    DelayBeat(32, () => { ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(4)); });
                });
                BarrageCreate(0, BeatTime(1), 5.8f, new string[]
                {
                "Texture1(RGBSplit)(Fade2)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "Fade","","Texture2","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","Fade","","",
                //
                "Texture3","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","Fade",

                "(VoidArrow)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "MoveCentre(SinScreen)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //
                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",
                //
                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "R","","","",
                "","","","",   "","","","",

                "","","R","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","WhiteOut","","",
                "","","","",   "","","","",
                    //
                });
            }
            void nr7()
            {
                BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                {

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "(R)(+01)","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",

                "(R)(+01)","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                //
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "(R)(+01)","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",

                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                //
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "(R)(+01)","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",

                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "(R)(+01)","","","",
                "","","","",   "(R)(+01)","","","",

                "","","","",   "(R)(+01)","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                //
                "(R)(+01)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",
                "(R)(+01)","","","",   "","","","",

                "(R)(+01)","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                //"","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",

                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                "","","","",   "","","","",
                    //

                });
            }

            public void Start()
            {
                RegisterFunction("LRotate", () =>
                {
                    ForBeat(3 / 4f, () =>
                  {
                      ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + 3.5f * 0.15f;
                  });
                });
                RegisterFunction("RRotate", () =>
                {
                    ForBeat(3 / 4f, () =>
                  {
                      ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-3.5f) * 0.15f;
                  });
                });
                RegisterFunction("MidRotate", () =>
                {
                    ForBeat(3 / 4f, () =>
                    {
                        ScreenDrawing.ScreenAngle *= 0.8f;
                        if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                        if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                    });
                });
                RegisterFunction("ConvulseL", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6, BeatTime(1.5f / 2f), false);
                });
                RegisterFunction("ConvulseR", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(6, BeatTime(1.5f / 2f), true);
                });
                SetBox(240, 140, 140);
                InstantTP(320, 240);
                game = this;
                HeartAttribute.MaxHP = 6;
                HeartAttribute.Speed = 3.3f;
                HeartAttribute.SoftFalling = true;

                GametimeDelta = -11f; //+4000;

                if (false)
                {
                    SetSoul(1);
                    SetGreenBox();
                    PlayOffset = BeatTime(319 - 4);
                    GametimeDelta = -11f + BeatTime(319 - 4) - 3;
                }
                //InstantSetBox(240, 84, 84);
                //SetSoul(1);
                ScreenDrawing.UISettings.CreateUISurface();
                if (AnomalyAvailable()) TryGenerateAnomaly();
            }

            bool AnomalyAvailable()
            {
                return PlayerManager.CurrentUser != null
&& DateTime.Now.Day == 18 && !PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("reTranscendence");
            }
            void AnomalyIntro()
            {
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Black * 0.6f);
                });
                RegisterFunctionOnce("Fade", () =>
                {
                    ScreenDrawing.SceneOut(Color.Black, BeatTime(11));
                    ResetBarrage();
                });
                RegisterFunctionOnce("Reset", () =>
                {
                    GameStates.ResetScene(new FacingCharactor());
                });
                string[] rhythm = {
                            "Flicker", "", "Flicker", "", "Flicker", "", "Flicker", "Flicker",
                            "Fade", "", "", "", "", "", "", "",
                            "Reset", "", "", "", "", "", "", "",
                        };
                BarrageCreate(0, BeatTime(1), 0, rhythm);
            }
            void TryGenerateAnomaly()
            {
                if (((GameStates.CurrentScene as SongFightingScene).Mode & GameMode.Buffed) == GameMode.Buffed)
                    DelayBeat(44, () =>
                    {
                        if (HeartAttribute.HP <= 1f)
                        {
                            AnomalyIntro();
                        }
                    });
                if (CurrentDifficulty == Difficulty.Extreme)
                {
                    AdvanceFunctions.Interactive.AddEndEvent(AnomalyIntro);
                    AutoEnd = false;
                }
            }
        }
    }
}