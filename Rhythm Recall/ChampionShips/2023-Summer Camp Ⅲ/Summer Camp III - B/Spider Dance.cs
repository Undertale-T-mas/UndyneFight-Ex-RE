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
using Extends;
using static Extends.DrawingUtil;
using static Extends.LineMoveLibrary;
using static UndyneFight_Ex.Remake.TextUtils;
using static UndyneFight_Ex.MathUtil;
using System.Net.Mail;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Extends;
using GameJolt.Services;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Rhythm_Recall.Waves
{
    internal class Spider_Dance : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Spider_Dance()
        {
            dif.Add("Div.2", Difficulty.Easy);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
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
                    new(Difficulty.Extreme,17.5f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,7.0f),
                    new(Difficulty.Extreme,17.5f)
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
            static Arrow.UnitEasing easeA, easeB, easeC, easeD, easeE;
            static Arrow.EnsembleEasing easeY;
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
                    Line l = new(EaseOut(BeatTime(2), new Vector2(320, 240), new Vector2(640, 240), EaseState.Cubic), EaseOut(BeatTime(2), 0, 90, EaseState.Circ));
                    easeC.TagApply("C");
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            LinkEase(EaseOut(BeatTime(1), 2, 1.75f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.75f, 1.5f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.5f, 1.25f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.25f, 1, EaseState.Back)));
                        RunEase(s => ScreenDrawing.ScreenAngle = s,
                            LinkEase(EaseOut(BeatTime(1), -180, -135, EaseState.Back),
                            EaseOut(BeatTime(1), 0, 45, EaseState.Back),
                            EaseOut(BeatTime(1), 0, 45, EaseState.Back),
                            EaseOut(BeatTime(3), 0, 45, EaseState.Back)));
                    });
                    RegisterFunctionOnce("LineA", () =>
                    {
                        Line a = new(new Vector2(320 - 42, 240 - 42), new Vector2(0, 0)) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        Line b = new(new Vector2(320 + 42, 240 + 42), new Vector2(640, 480)) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.VerticalMirror = true;
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineA1", () =>
                    {
                        Line a = new(new Vector2(320 - 42, 240 - 42), -45) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        Line b = new(new Vector2(320 - 42, 240 + 42), 45) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineA2", () =>
                    {
                        Line a = new(new Vector2(320 - 84, 240), 90) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        Line b = new(new Vector2(320, 240 + 84), 0) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        CreateEntity(a, b);
                        a.TransverseMirror = true;
                        b.VerticalMirror = true;
                        a.AlphaDecrease(BeatTime(2));
                        b.AlphaDecrease(BeatTime(2));
                        DelayBeat(2, () => { a.Dispose(); b.Dispose(); });
                    });
                    RegisterFunctionOnce("LineA3", () =>
                    {
                        Line a = new(new Vector2(320 - 126, 240 - 126), -45) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        Line b = new(new Vector2(320 - 126, 240 + 126), 45) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.TransverseMirror = true;
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineA4", () =>
                    {
                        Line a = new(new Vector2(320 - 168, 240), 90) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        Line b = new(new Vector2(320, 240 + 168), 0) { DrawingColor = Color.MediumPurple * 0.9f, Alpha = 0.7f };
                        CreateEntity(a, b);
                        a.TransverseMirror = true;
                        b.VerticalMirror = true;
                        a.AlphaDecrease(BeatTime(2));
                        b.AlphaDecrease(BeatTime(2));
                        DelayBeat(2, () => { a.Dispose(); b.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB1", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(6), new Vector2(640f / 7f, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f, 490), new Vector2(650, 490), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f, 490), new Vector2(640f / 7f, -10), EaseState.Quad),
                            Stable(BeatTime(5), new Vector2(640f / 7f, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f, -10), new Vector2(650, -10), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(8, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB2", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(5), new Vector2(640f / 7f * 2f, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 2f, -10), new Vector2(650, -10), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * 2f, -10), new Vector2(640f / 7f * 2f, 490), EaseState.Quad),
                            Stable(BeatTime(4), new Vector2(640f / 7f * 2f, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 2f, 490), new Vector2(650, 490), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(7, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB3", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(4), new Vector2(640f / 7f * 3f, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 3f, 490), new Vector2(650, 490), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * 3f, 490), new Vector2(640f / 7f * 3f, -10), EaseState.Quad),
                            Stable(BeatTime(3), new Vector2(640f / 7f * 3f, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 3f, -10), new Vector2(650, -10), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(6, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB4", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(3), new Vector2(640f / 7f * 4f, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 4f, -10), new Vector2(-10, -10), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * 4f, -10), new Vector2(640f / 7f * 4f, 490), EaseState.Quad),
                            Stable(BeatTime(2), new Vector2(640f / 7f * 4f, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 4f, 490), new Vector2(-10, 490), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(5, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB5", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(2), new Vector2(640f / 7f * 5f, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 5f, 490), new Vector2(-10, 490), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * 5f, 490), new Vector2(640f / 7f * 5f, -10), EaseState.Quad),
                            Stable(BeatTime(1), new Vector2(640f / 7f * 5f, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 5f, -10), new Vector2(-10, -10), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(4, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB6", () =>
                    {
                        Line a = new(LinkEase(Stable(BeatTime(1), new Vector2(640f / 7f * 6f, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 6f, -10), new Vector2(-10, -10), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * 6f, -10), new Vector2(640f / 7f * 6f, 490), EaseState.Quad),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * 6f, 490), new Vector2(-10, 490), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(3, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("Line1s", () =>
                    {
                        float e = 405;
                        for (int i = 0; i < 4; i++)
                        {
                            DelayBeat(i * 2, () => {

                                Vector2 a = new(320, 240);
                                Line d;
                                CreateEntity(d = new(LinkEase(EaseOut(BeatTime(2), a + GetVector2(450, e), a + GetVector2(450, e), EaseState.Quint)).Easing,
                                                     LinkEase(EaseOut(BeatTime(4), a + GetVector2(450, e), a + GetVector2(60, e), EaseState.Quint)).Easing)
                                { Alpha = 1f, DrawingColor = Color.MediumPurple * 0.7f });


                                e += 90;
                            });
                        }
                        DelayBeat(8.04f, () => {
                            Line[] Lines = GetAll<Line>();
                            for (int i = 0; i < 4; i++) { Lines[i].Dispose(); }
                        });
                    });
                    RegisterFunctionOnce("Line2s", () =>
                    {
                        Line d;

                        Vector2 a = new(320, 240);
                        float e = 45;
                        float i = -1;
                        Vector2 a2 = a + GetVector2(450, e);
                        Vector2 a3 = a + GetVector2(450, e + (10 * i));
                        Vector2 a4 = a + GetVector2(450, e + (5 * i));
                        Vector2 a5 = a + GetVector2(450, e + (15 * i));

                        int w = 1;
                        int w2 = 1;
                        Vector2 b = new(a.X + (42 * w), a.Y + (42 * w2));
                        Vector2 b2 = new(b.X, b.Y);
                        Vector2 b3 = new(b.X + (15 * w), b.Y + (15 * w2));
                        Vector2 b4 = new(b.X - (5 * w), b.Y - (5 * w2));
                        Vector2 b5 = new(b.X + (25 * w), b.Y + (25 * w2));
                        Vector2 b6 = new(b.X + (240 * w), b.Y + (240 * w2));

                        CreateEntity(d = new(LinkEase(Stable(BeatTime(0), a2), EaseOut(BeatTime(3), a3 - a2, EaseState.Back),
                                                      Stable(BeatTime(0), a3), EaseOut(BeatTime(1), a4 - a3, EaseState.Back),
                                                      Stable(BeatTime(0), a4), EaseOut(BeatTime(5), a5 - a4, EaseState.Back),
                                                      Stable(BeatTime(2), a5), EaseOut(BeatTime(2), Vector2.Zero, EaseState.Back)).Easing,
                                             LinkEase(Stable(BeatTime(0), b2), EaseOut(BeatTime(3), b3 - b2, EaseState.Back),
                                                      Stable(BeatTime(0), b3), EaseOut(BeatTime(1), b4 - b2, EaseState.Back),
                                                      Stable(BeatTime(0), b4), EaseOut(BeatTime(4.5f), b5 - b4, EaseState.Back),
                                                      Stable(BeatTime(0), b5), EaseOut(BeatTime(7), b6 - b5, EaseState.Elastic)).Easing)
                        { DrawingColor = Color.MediumPurple * 0.7f });
                        RunEase((s) => { d.Alpha = s; }, LinkEase(Stable(BeatTime(9), 1), EaseOut(BeatTime(4f), -1, EaseState.Cubic)));
                        DelayBeat(40, () => { d.Dispose(); });
                        a = new(320, 240);
                        e = 135;
                        i = 1;
                        a2 = a + GetVector2(450, e);
                        a3 = a + GetVector2(450, e + (10 * i));
                        a4 = a + GetVector2(450, e + (5 * i));
                        a5 = a + GetVector2(450, e + (15 * i));

                        w = -1;
                        w2 = 1;
                        b = new(a.X + (42 * w), a.Y + (42 * w2));
                        b2 = new(b.X, b.Y);
                        b3 = new(b.X + (15 * w), b.Y + (15 * w2));
                        b4 = new(b.X - (5 * w), b.Y - (5 * w2));
                        b5 = new(b.X + (25 * w), b.Y + (25 * w2));
                        b6 = new(b.X + (240 * w), b.Y + (240 * w2));

                        CreateEntity(d = new(LinkEase(Stable(BeatTime(0), a2), EaseOut(BeatTime(3), a3 - a2, EaseState.Back),
                                                      Stable(BeatTime(0), a3), EaseOut(BeatTime(1), a4 - a3, EaseState.Back),
                                                      Stable(BeatTime(0), a4), EaseOut(BeatTime(5), a5 - a4, EaseState.Back),
                                                      Stable(BeatTime(2), a5), EaseOut(BeatTime(2), Vector2.Zero, EaseState.Back)).Easing,
                                             LinkEase(Stable(BeatTime(0), b2), EaseOut(BeatTime(3), b3 - b2, EaseState.Back),
                                                      Stable(BeatTime(0), b3), EaseOut(BeatTime(1), b4 - b2, EaseState.Back),
                                                      Stable(BeatTime(0), b4), EaseOut(BeatTime(4.5f), b5 - b4, EaseState.Back),
                                                      Stable(BeatTime(0), b5), EaseOut(BeatTime(7), b6 - b5, EaseState.Elastic)).Easing)
                        { DrawingColor = Color.MediumPurple * 0.7f });
                        RunEase((s) => { d.Alpha = s; }, LinkEase(Stable(BeatTime(9), 1), EaseOut(BeatTime(4f), -1, EaseState.Cubic)));
                        DelayBeat(40, () => { d.Dispose(); });
                        a = new(320, 240);
                        e = 225;
                        i = -1;
                        a2 = a + GetVector2(450, e);
                        a3 = a + GetVector2(450, e + (10 * i));
                        a4 = a + GetVector2(450, e + (5 * i));
                        a5 = a + GetVector2(450, e + (15 * i));

                        w = -1;
                        w2 = -1;
                        b = new(a.X + (42 * w), a.Y + (42 * w2));
                        b2 = new(b.X, b.Y);
                        b3 = new(b.X + (15 * w), b.Y + (15 * w2));
                        b4 = new(b.X - (5 * w), b.Y - (5 * w2));
                        b5 = new(b.X + (25 * w), b.Y + (25 * w2));
                        b6 = new(b.X + (240 * w), b.Y + (240 * w2));

                        CreateEntity(d = new(LinkEase(Stable(BeatTime(0), a2), EaseOut(BeatTime(3), a3 - a2, EaseState.Back),
                                                      Stable(BeatTime(0), a3), EaseOut(BeatTime(1), a4 - a3, EaseState.Back),
                                                      Stable(BeatTime(0), a4), EaseOut(BeatTime(5), a5 - a4, EaseState.Back),
                                                      Stable(BeatTime(2), a5), EaseOut(BeatTime(2), Vector2.Zero, EaseState.Back)).Easing,
                                             LinkEase(Stable(BeatTime(0), b2), EaseOut(BeatTime(3), b3 - b2, EaseState.Back),
                                                      Stable(BeatTime(0), b3), EaseOut(BeatTime(1), b4 - b2, EaseState.Back),
                                                      Stable(BeatTime(0), b4), EaseOut(BeatTime(4.5f), b5 - b4, EaseState.Back),
                                                      Stable(BeatTime(0), b5), EaseOut(BeatTime(7), b6 - b5, EaseState.Elastic)).Easing)
                        { DrawingColor = Color.MediumPurple * 0.7f });
                        RunEase((s) => { d.Alpha = s; }, LinkEase(Stable(BeatTime(9), 1), EaseOut(BeatTime(4f), -1, EaseState.Cubic)));
                        DelayBeat(40, () => { d.Dispose(); });
                        a = new(320, 240);
                        e = 315;
                        i = 1;
                        a2 = a + GetVector2(450, e);
                        a3 = a + GetVector2(450, e + (10 * i));
                        a4 = a + GetVector2(450, e + (5 * i));
                        a5 = a + GetVector2(450, e + (15 * i));

                        w = 1;
                        w2 = -1;
                        b = new(a.X + (42 * w), a.Y + (42 * w2));
                        b2 = new(b.X, b.Y);
                        b3 = new(b.X + (15 * w), b.Y + (15 * w2));
                        b4 = new(b.X - (5 * w), b.Y - (5 * w2));
                        b5 = new(b.X + (25 * w), b.Y + (25 * w2));
                        b6 = new(b.X + (240 * w), b.Y + (240 * w2));

                        CreateEntity(d = new(LinkEase(Stable(BeatTime(0), a2), EaseOut(BeatTime(3), a3 - a2, EaseState.Back),
                                                      Stable(BeatTime(0), a3), EaseOut(BeatTime(1), a4 - a3, EaseState.Back),
                                                      Stable(BeatTime(0), a4), EaseOut(BeatTime(5), a5 - a4, EaseState.Back),
                                                      Stable(BeatTime(2), a5), EaseOut(BeatTime(2), Vector2.Zero, EaseState.Back)).Easing,
                                             LinkEase(Stable(BeatTime(0), b2), EaseOut(BeatTime(3), b3 - b2, EaseState.Back),
                                                      Stable(BeatTime(0), b3), EaseOut(BeatTime(1), b4 - b2, EaseState.Back),
                                                      Stable(BeatTime(0), b4), EaseOut(BeatTime(4.5f), b5 - b4, EaseState.Back),
                                                      Stable(BeatTime(0), b5), EaseOut(BeatTime(7), b6 - b5, EaseState.Elastic)).Easing)
                        { DrawingColor = Color.MediumPurple * 0.7f });
                        RunEase((s) => { d.Alpha = s; }, LinkEase(Stable(BeatTime(9), 1), EaseOut(BeatTime(4f), -1, EaseState.Cubic)));
                        DelayBeat(40, () => { d.Dispose(); });
                        SimplifiedEasing.RunEase((s) => { InstantSetBox(240, s.X, s.Y); },
                            LinkEase(Stable(0, new Vector2(84)), EaseOut(BeatTime(3f), new Vector2(32), EaseState.Back)),
                            LinkEase(Stable(0, new Vector2(116)), EaseOut(BeatTime(1f), new Vector2(-10), EaseState.Back)),
                            LinkEase(Stable(0, new Vector2(106)), EaseOut(BeatTime(4.5f), new Vector2(52), EaseState.Back)),
                            LinkEase(Stable(0, new Vector2(158)), EaseOut(BeatTime(8f), new Vector2(-74), EaseState.Elastic)));
                    });

                    CreateChart(BeatTime(0), BeatTime(2), 6.2f, new string[]
                    { 
                        //1
                        "Shrink(LineA)(LineA1)", "", "", "",    "(LineA)(LineA1)(LineA2)", "", "", "",
                        "(LineA)(LineA1)(LineA2)(LineA3)", "", "", "",    "(LineA)(LineA1)(LineA2)(LineA3)(LineA4)", "", "", "",//pre
                        "", "", "d", "",    "d", "", "", "",
                        "d", "", "+01", "",    "d", "", "+01", "",
                        //2
                        "", "", "d", "",    "+01", "", "+0", "",
                        "d", "", "+01", "",    "d", "", "+01", "",
                        "(d)(+0)", "", "(+1)(+0)", "",    "(+1)(+0)", "", "(+1)(+0)", "",
                        "", "", "d", "",    "$2", "+11", "+1", "",
                        //3
                        "(d1)(+0)(Line1s)", "", "", "",    "", "", "", "",
                        "(-1)(+01)", "", "", "",    "", "", "", "",
                        "(-11)(+0)", "", "", "",    "", "", "", "",
                        "(-1)(+01)", "", "", "",    "", "", "", "",
                        //4
                        "*d'1.2(LineB1)(Line2s)", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2(LineB2)", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
                        "*+1'1.2(LineB3)", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2(LineB4)", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
                        "*+1'1.2(LineB5)", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2(LineB6)", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
                        "(*+1)(*+21)", "", "/", "",    "", "", "", "",
                    });
                }

                if (InBeat(24))
                {
                    easeA.TagApply("A");
                    easeB.TagApply("B");
                    easeD.TagApply("D");
                    RegisterFunctionOnce("RotL", () =>
                    {
                        float scale = ScreenDrawing.ScreenScale;
                        ScreenAngle(-1.2f, BeatTime(1));
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            LinkEase(EaseOut(BeatTime(0.75f), scale, scale + 0.02f, EaseState.Linear),
                            Stable(0, scale + 0.02f)));
                    });
                    RegisterFunctionOnce("RotR", () =>
                    {
                        float scale = ScreenDrawing.ScreenScale;
                        ScreenAngle(1.2f, BeatTime(1));
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            LinkEase(EaseOut(BeatTime(0.75f), scale, scale + 0.02f, EaseState.Linear),
                            Stable(0, scale + 0.02f)));
                    });
                    RegisterFunctionOnce("RotBack", () =>
                    {
                        float scale = ScreenDrawing.ScreenScale;
                        ScreenAngle(0, BeatTime(1));
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            LinkEase(EaseOut(BeatTime(2), scale, 1f, EaseState.Quad),
                            Stable(0, 1f)));
                    });
                    RegisterFunctionOnce("Kick", () =>
                    {
                        //这里可以使用mirror，就不用那么麻烦了
                        Line a = new(60, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line b = new(90, 90) { Alpha = 0.4f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line c = new(640 - 60, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line d = new(640 - 90, 90) { Alpha = 0.4f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line a1 = new(0, 40) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line b1 = new(0, -40) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line c1 = new(640, 40) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line d1 = new(640, -40) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line[] lines = { a, b, c, d, a1, b1, c1, d1 };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(0.75f));
                            DelayBeat(0.8f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Kick1", () =>
                    {
                        Line a = new(new Vector2(320, 90), 40) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line b = new(new Vector2(320, 90), -40) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.VerticalMirror = true;
                            l.AlphaDecrease(BeatTime(0.8f));
                            DelayBeat(0.8f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("Kick2", () =>
                    {
                        Line a = new(new Vector2(320, 180), 40) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line b = new(new Vector2(320, 180), -40) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.VerticalMirror = true;
                            l.AlphaDecrease(BeatTime(0.8f));
                            DelayBeat(0.8f, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("ConvR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(0.85f), false);
                    });
                    RegisterFunctionOnce("ConvL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(0.85f), true);
                    });
                    RegisterFunctionOnce("LineB1", () =>
                    {
                        float i = 6f;
                        Line a = new(LinkEase(Stable(BeatTime(i), new Vector2(640f / 7f * i, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, -10), new Vector2(-10, -10), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * i, -10), new Vector2(640f / 7f * i, 490), EaseState.Quad),
                            Stable(BeatTime(i - 1), new Vector2(640f / 7f * i, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, 490), new Vector2(-10, 490), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(i + 2, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB2", () =>
                    {
                        float i = 5f;
                        Line a = new(LinkEase(Stable(BeatTime(i), new Vector2(640f / 7f * i, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, 490), new Vector2(-10, 490), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * i, 490), new Vector2(640f / 7f * i, -10), EaseState.Quad),
                            Stable(BeatTime(i - 1), new Vector2(640f / 7f * i, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, -10), new Vector2(-10, -10), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(i + 2, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB3", () =>
                    {
                        float i = 4f;
                        Line a = new(LinkEase(Stable(BeatTime(i), new Vector2(640f / 7f * i, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, -10), new Vector2(-10, -10), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * i, -10), new Vector2(640f / 7f * i, 490), EaseState.Quad),
                            Stable(BeatTime(i - 1), new Vector2(640f / 7f * i, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, 490), new Vector2(-10, 490), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(i + 2, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB4", () =>
                    {
                        float i = 3f;
                        Line a = new(LinkEase(Stable(BeatTime(i), new Vector2(640f / 7f * i, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, 490), new Vector2(650, 490), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * i, 490), new Vector2(640f / 7f * i, -10), EaseState.Quad),
                            Stable(BeatTime(i - 1), new Vector2(640f / 7f * i, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, -10), new Vector2(650, -10), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(i + 2, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB5", () =>
                    {
                        float i = 2f;
                        Line a = new(LinkEase(Stable(BeatTime(i), new Vector2(640f / 7f * i, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, -10), new Vector2(650, -10), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * i, -10), new Vector2(640f / 7f * i, 490), EaseState.Quad),
                            Stable(BeatTime(i - 1), new Vector2(640f / 7f * i, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, 490), new Vector2(650, 490), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(i + 2, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineB6", () =>
                    {
                        float i = 1f;
                        Line a = new(LinkEase(Stable(BeatTime(i), new Vector2(640f / 7f * i, 490)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, 490), new Vector2(650, 490), EaseState.Back)).Easing,
                            LinkEase(EaseOut(BeatTime(1), new Vector2(640f / 7f * i, 490), new Vector2(640f / 7f * i, -10), EaseState.Quad),
                            Stable(BeatTime(i - 1), new Vector2(640f / 7f * i, -10)),
                            EaseIn(BeatTime(2), new Vector2(640f / 7f * i, -10), new Vector2(650, -10), EaseState.Back)).Easing)
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        CreateEntity(a);
                        DelayBeat(i + 2, () => { a.Dispose(); });
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    { 
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "Kick", "",    "Kick(Kick1)", "", "Kick(Kick1)(Kick2)", "",
                        //1
                        "(d)(+21)", "", "+2", "",    "(d)(+21)", "", "+01", "",
                        "(d)(+21)", "", "+2", "",    "(d)(+21)", "", "+01", "",
                        "(~_$2'1.2@B)(ConvL)(*$01)", "", "(~_$2'1.2@A)(*$21)", "",    "(*$01)(~_$2'1.2@B)(ConvL)", "", "~_$2'1.2@A(*$31)", "",
                        "(~_$2'1.2@B)(*$11)(ConvL)", "", "(*$01)(~_$2'1.2@A)", "",    "(~_$2'1.2@B)(*$31)(ConvL)", "", "(*$21)(~_$2'1.2@A)", "",
                        //2
                        "(~_$01'1.2@B)(~_$2'1.2@B)(ConvR)", "", "(*D0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(*D0)(ConvR)", "", "(*D0)(~_$01'1.2@A)", "",
                        "(~_$01'1.2@B)(*D0)(ConvR)", "", "(*D0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(*D0)(ConvR)", "", "(*D0)(~_$01'1.2@A)", "",
                        "(~_$01'1.2@B)(*D0)(ConvR)", "", "(*D0)(~_$01'1.2@A)", "",    "(~_$01'1.2@B)(*D0)(ConvR)", "", "(*D0)(~_$01'1.2@A)", "",
                        "(~_$01'1.2@B)", "", "$0", "",    "$21", "$0", "$21", "",
                        //3
                        "(#1.5#$0)", "", "(*+21)", "",    "*+01", "", "*+01", "",
                        "(#1.5#$1)", "", "(*+21)", "",    "*+01", "", "*+01", "",
                        "(#1.5#$2)", "", "(*+21)", "",    "*+01", "", "*+01", "",
                        "(#1.5#$3)", "", "(*+21)", "",    "*+01", "", "*+01", "",
                        //4
                        "*d'1.2(LineB1)(RotL)", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",    "*-11'1.2(LineB2)(RotR)", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",
                        "*-1'1.2(LineB3)(RotL)", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",    "*-11'1.2(LineB4)(RotR)", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",
                        "*-1'1.2(LineB5)(RotL)", "~_!+0'1.2@D", "~_!+0'1.2@D", "~_!+0'1.2@D",    "*-11'1.2(LineB6)(RotR)", "~_!+01'1.2@D", "~_!+01'1.2@D", "~_!+01'1.2@D",
                        "(*-1)(*-21)(RotBack)", "", "/", "",    "/", "", "/", "",
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
                        box.SetPosition(2, new Vector2(320 + Sin(60) * 80, 240 + Cos(60) * 80));
                        box.SetPosition(3, new Vector2(320 - Sin(60) * 80, 240 + Cos(60) * 80));
                        BoxUtils.Rotate(new(320, 240), 180);
                        DelayBeat(0.5f, () => { });
                        ScreenDrawing.BoxBackColor = Color.Black;

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
                        BoxUtils.DeVertexify(new(320 - 80, 240 - 80, 160, 160));
                        Heart.PurpleLineCount = 6;
                    });
                    RegisterFunctionOnce("ChangeA", () =>
                    {
                        SetBox(new Vector2(320, 240), 240, 160);
                    });
                    #region Spiders Func
                    RegisterFunctionOnce("Sp1", () =>
                    {
                        CreateEntity(new Spider.LineSpider(1, true, 5));
                    });
                    RegisterFunctionOnce("Sp2", () =>
                    {
                        CreateEntity(new Spider.LineSpider(2, true, 5));
                    });
                    RegisterFunctionOnce("Sp3", () =>
                    {
                        CreateEntity(new Spider.LineSpider(3, true, 5));
                    });
                    RegisterFunctionOnce("Sp4", () =>
                    {
                        CreateEntity(new Spider.LineSpider(4, true, 5));
                    });
                    RegisterFunctionOnce("Sp5", () =>
                    {
                        CreateEntity(new Spider.LineSpider(5, true, 5));
                    });
                    RegisterFunctionOnce("Sp6", () =>
                    {
                        CreateEntity(new Spider.LineSpider(6, true, 5));
                    });
                    RegisterFunctionOnce("tSp1", () =>
                    {
                        CreateEntity(new Spider.LineSpider(1, false, 5));
                    });
                    RegisterFunctionOnce("tSp2", () =>
                    {
                        CreateEntity(new Spider.LineSpider(2, false, 5));
                    });
                    RegisterFunctionOnce("tSp3", () =>
                    {
                        CreateEntity(new Spider.LineSpider(3, false, 5));
                    });
                    RegisterFunctionOnce("tSp4", () =>
                    {
                        CreateEntity(new Spider.LineSpider(4, false, 5));
                    });
                    RegisterFunctionOnce("tSp5", () =>
                    {
                        CreateEntity(new Spider.LineSpider(5, false, 5));
                    });
                    RegisterFunctionOnce("tSp6", () =>
                    {
                        CreateEntity(new Spider.LineSpider(6, false, 5));
                    });
                    RegisterFunctionOnce("DSp1", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210, true, 5));
                    });
                    RegisterFunctionOnce("DSp2", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 1, true, 5));
                    });
                    RegisterFunctionOnce("DSp3", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 2, true, 5));
                    });
                    RegisterFunctionOnce("DSp4", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 3, true, 5));
                    });
                    RegisterFunctionOnce("DSp5", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 4, true, 5));
                    });
                    RegisterFunctionOnce("DSp6", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 5, true, 5));
                    });
                    RegisterFunctionOnce("DSp7", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 6, true, 5f));
                    });
                    RegisterFunctionOnce("DSp8", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 7, true, 5f));
                    });
                    RegisterFunctionOnce("DSp9", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 8, true, 5f));
                    });
                    RegisterFunctionOnce("DSp10", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 9, true, 5f));
                    });
                    RegisterFunctionOnce("USp1", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210, false, 5));
                    });
                    RegisterFunctionOnce("USp2", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 1, false, 5));
                    });
                    RegisterFunctionOnce("USp3", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 2, false, 5));
                    });
                    RegisterFunctionOnce("USp4", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 3, false, 5));
                    });
                    RegisterFunctionOnce("USp5", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 4, false, 5));
                    });
                    RegisterFunctionOnce("USp6", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 5, false, 5));
                    });
                    RegisterFunctionOnce("USp7", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 6, false, 5f));
                    });
                    RegisterFunctionOnce("USp8", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 7, false, 5f));
                    });
                    RegisterFunctionOnce("USp9", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 8, false, 5f));
                    });
                    RegisterFunctionOnce("USp10", () =>
                    {
                        CreateEntity(new Spider.LineSpider2(210f + 24f * 9, false, 5f));
                    });

                    #endregion
                    RegisterFunctionOnce("SpA", () =>
                    {

                    });
                    RegisterFunctionOnce("Line0s", () =>
                    {
                        float Time = 2;
                        float Time2 = 5.5f;
                        for (int i = 0; i < 96; i++)
                        {
                            float X = 0;
                            float Y = 0;
                            float X2 = 0;
                            float Y2 = 0;
                            float L = Rand(0, 1);
                            if (L == 1)
                            {
                                X = Rand(640 - 750, 750 + (640 - 750));
                                X2 = Rand(640 - 750, 750 + (640 - 750));
                                if (Rand(1, 2) == 1)
                                {
                                    Y = 562.5f + (480 - 562.5f);
                                    Y2 = 480 - 562.5f;
                                }
                                else
                                {
                                    Y = 480 - 562.5f;
                                    Y2 = 562.5f + (480 - 562.5f);
                                }

                            }
                            else
                            {
                                Y = Rand(480 - 562.5f, 562.5f + (480 - 562.5f));
                                Y2 = Rand(480 - 562.5f, 562.5f + (480 - 562.5f));

                                if (Rand(1, 2) == 1)
                                {
                                    X = 640 - 750;
                                    X2 = 750 + (640 - 750);
                                }
                                else
                                {
                                    X = 750 + (640 - 750);
                                    X2 = 640 - 750;
                                }
                            }

                            float z = (float)(Math.Atan2(Y - Y2, X - X2) * (180 / Math.PI)) + 90;
                            Time += 0.02f;
                            if (i == 40) { Time2 += 8; }
                            else if (i == 88) { Time2 += 8; }
                            else if (i <= 88) { Time2 += 0.5f; }
                            else if (i >= 88) { Time2 += 0.25f; }
                            Line d;
                            CreateEntity(d = new Line(

                                SimplifiedEasing.LinkEase(SimplifiedEasing.EaseOut(BeatTime(4), new Vector2(X, Y), new Vector2(X, Y), EaseState.Quad),
                                                          SimplifiedEasing.EaseOut(BeatTime(54 + Time), new Vector2(X, Y), new Vector2(X, Y), EaseState.Quad),
                                                          SimplifiedEasing.EaseIn(BeatTime(4), new Vector2(X, Y), new Vector2(X + Cos(z) * 1000, Y + Sin(z) * 1000), EaseState.Quint)).Easing,
                                SimplifiedEasing.LinkEase(SimplifiedEasing.EaseOut(BeatTime(4), new Vector2(X, Y), new Vector2(X2, Y2), EaseState.Quad),
                                                          SimplifiedEasing.EaseOut(BeatTime(54 + Time), new Vector2(X2, Y2), new Vector2(X2, Y2), EaseState.Quad),
                                                          SimplifiedEasing.EaseIn(BeatTime(4), new Vector2(X2, Y2), new Vector2(X2 + Cos(z) * 1000, Y2 + Sin(z) * 1000), EaseState.Quint)).Easing));

                            if (Rand(0, 1) == 1) { d.DrawingColor = Color.MediumPurple; }

                            SimplifiedEasing.RunEase((s) => { d.Alpha = s; },
                                           SimplifiedEasing.EaseOut(BeatTime(4f), 1f, 0.2f, SimplifiedEasing.EaseState.Back));

                            DelayBeat(Time2, () => {
                                SimplifiedEasing.RunEase((s) => { d.Alpha = s; },
                                            SimplifiedEasing.EaseOut(BeatTime(4f), 1f, 0.2f, SimplifiedEasing.EaseState.Back));
                            });
                            d.Width = 1;
                        }
                    });
                    RegisterFunctionOnce("Line1s", () =>
                    {
                        float y = 0;
                        float x = 0;
                        float z = 90;
                        for (int i = 0; i < 12; i++)
                        {
                            DelayBeat(i * 0.5f, () => {

                                x += 3;
                                y += Sin(x * 10) * 100;
                                z -= Sin(x * 10) * 12;

                                Line c = new(EaseOut(BeatTime(2), new Vector2(y, 240), new Vector2(y, 240), EaseState.Cubic), EaseOut(BeatTime(2), 90, z, EaseState.Quint));
                                CreateEntity(c);
                                c.Depth = 0f;
                                c.Alpha = 1;
                                c.DrawingColor = Color.MediumPurple;
                                c.AlphaDecrease(BeatTime(2));
                            });
                        }

                    });
                    RegisterFunctionOnce("Line2s", () =>
                    {
                        float y = 640;
                        float x = 0;
                        float z = 90;
                        for (int i = 0; i < 12; i++)
                        {
                            DelayBeat(i * 0.5f, () => {

                                x += 3;
                                y -= Sin(x * 10) * 100;
                                z += Sin(x * 10) * 12;

                                Line c = new(EaseOut(BeatTime(2), new Vector2(y, 240), new Vector2(y, 240), EaseState.Cubic), EaseOut(BeatTime(2), 90, z, EaseState.Quint));
                                CreateEntity(c);
                                c.Depth = 0f;
                                c.Alpha = 1;
                                c.DrawingColor = Color.MediumPurple;
                                c.AlphaDecrease(BeatTime(2));
                            });
                        }

                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    { 
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "SetBox(Line0s)", "",    "", "", "", "",
                        //1
                        "", "", "Purple", "",    "ChangeA", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "Sp1(DSp1)(Line1s)", "", "Sp2(DSp2)", "",     "Sp3(DSp3)", "", "Sp4(DSp4)","",
                        "Sp5(DSp5)", "", "Sp6(DSp6)", "",     "Sp5(DSp7)", "", "Sp4(DSp8)","",
                        //2
                        "Sp3(DSp9)", "", "Sp2(DSp9)", "",     "Sp1", "", "", "",
                        "tSp6(USp10)(Line2s)", "", "tSp5(USp9)", "",     "tSp4(USp8)", "", "tSp3(USp7)", "",
                        "tSp2(USp6)", "", "tSp1(USp5)", "",     "tSp2(USp4)", "", "tSp3(USp3)", "",
                        "tSp4(USp2)", "", "tSp5", "",     "tSp6", "", "", "",
                        //3
                        "(Sp6)(Sp5)(Sp4)", "", "", "",    "(USp1)(USp2)", "(USp3)", "(USp4)", "(USp5)",
                        "(Sp1)(Sp2)(Sp3)", "", "", "",    "(DSp10)(DSp9)", "(DSp8)", "(DSp7)", "(DSp6)",
                        "(Sp6)(Sp5)(Sp4)", "", "", "",    "(USp1)(USp2)", "(USp3)", "(USp4)", "(USp5)",
                        "(Sp1)(Sp2)(Sp3)", "", "", "",    "(DSp10)(DSp9)", "(DSp8)", "(DSp7)", "(DSp6)",
                        //4
                        "(tSp1)(tSp6)", "", "(tSp1)(tSp6)", "",     "(tSp2)(tSp5)", "", "(tSp2)(tSp5)", "",
                        "(tSp3)(tSp4)", "", "(tSp3)(tSp4)", "",     "(tSp3)(tSp4)", "", "(tSp3)(tSp4)", "",
                        "(tSp2)(tSp5)", "", "(tSp2)(tSp5)", "",     "(tSp1)(tSp6)", "", "(tSp1)(tSp6)", "",
                        "(Sp1)(Sp6)", "", "", "",    "", "", "", "",
                        //5
                        "(tSp1)(tSp6)", "", "", "",    "(Sp2)(Sp5)", "", "", "",
                        "(tSp3)(tSp4)", "", "", "",    "(Sp1)(Sp6)", "", "", "",
                        "tSp1(DSp1)", "(DSp1)", "tSp2(DSp1)", "(DSp1)",     "tSp3(USp3)", "(USp3)", "tSp4(USp3)","(USp3)",
                        "tSp5(DSp5)", "(DSp5)", "tSp6(DSp5)", "(DSp5)",     "tSp5(USp7)", "(USp7)", "tSp4(USp7)","(USp7)",
                        //6
                        "tSp3(USp10)", "(USp10)", "tSp2(USp10)", "(USp10)",     "tSp1(DSp8)", "(DSp8)", "(DSp8)", "(DSp8)",
                        "Sp6(USp6)", "(USp6)", "Sp5(USp6)", "(USp6)",     "Sp4(DSp4)", "(DSp4)", "Sp3(DSp4)", "(DSp4)",
                        "Sp2", "USp1", "Sp1", "DSp10",     "Sp2", "USp2", "Sp3", "DSp9",
                        "Sp4", "USp3", "Sp5", "DSp8",     "Sp6", "USp4", "", "DSp7",
                        //7
                        "(tSp1)(tSp2)(tSp3)", "", "", "",    "", "", "", "",
                        "(tSp6)(tSp5)(tSp4)", "", "", "",    "", "", "", "",
                        "(tSp1)(tSp2)(tSp3)", "", "", "",    "", "", "", "",
                        "(tSp6)(tSp5)(tSp4)", "", "", "",    "", "", "", "",
                        //8
                        "(Sp1)(Sp6)", "", "(Sp1)(Sp6)", "",     "(Sp2)(Sp5)", "", "(Sp2)(Sp5)", "",
                        "(Sp3)(Sp4)", "", "(Sp3)(Sp4)", "",     "(Sp3)(Sp4)", "", "(Sp3)(Sp4)", "",
                        "(Sp2)(Sp5)", "", "(Sp2)(Sp5)", "",     "(Sp1)(Sp6)", "", "(Sp1)(Sp6)", "",
                        "(tSp1)(tSp6)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(56 + 64))
                {
                    DelayBeat(1f, () => {
                        /*   Line i1 = new(EaseOut(BeatTime(2), new Vector2(640, 0), new Vector2(320, 0), EaseState.Cubic), EaseOut(BeatTime(2), 90, 0, EaseState.Cubic));
                           CreateEntity(i1);
                           DelayBeat(2, () => { });
                           i1.AlphaDecrease(BeatTime(2));*/
                        /* Line d;
                         CreateEntity(d = new Line(SimplifiedEasing.EaseOut(BeatTime(4f), new Vector2(320, 0), new Vector2(320, 480), EaseState.Expo).Easing,
                             SimplifiedEasing.EaseOut(BeatTime(4f), new Vector2(320, 0), new Vector2(320, 0), EaseState.Expo).Easing));
                         SimplifiedEasing.RunEase((s) => { d.Alpha = s; }, SimplifiedEasing.EaseOut(BeatTime(4), 1.0f, 0.0f, EaseState.Quad));
                         d.AlphaDecrease(BeatTime(4));*/
                    });

                    Line l = new(LinkEase(Stable(BeatTime(0), new Vector2(320, 240)), EaseOut(BeatTime(2), new Vector2(), EaseState.Expo),
                                          Stable(BeatTime(0), new Vector2(320, 240)), EaseOut(BeatTime(2), new Vector2(), EaseState.Expo)),
                                 LinkEase(Stable(BeatTime(0), 90), EaseOut(BeatTime(2), -90, EaseState.Expo),
                                          Stable(BeatTime(0), 90), EaseOut(BeatTime(2), -90, EaseState.Expo),
                                          Stable(BeatTime(0), 90), EaseOut(BeatTime(2), -90, EaseState.Expo)));

                    ScreenDrawing.WhiteOut(BeatTime(0));
                    RegisterFunctionOnce("Green", () =>
                    {
                        SetBox(240, 84, 84);
                        SetSoul(1);
                        TP();
                    });
                    AddInstance(easeE = new Arrow.UnitEasing()
                    {

                        ApplyTime = (BeatTime(2f)),
                        AlphaEase = LinkEase(Stable(0, 1),
                        EaseOut(BeatTime(4), 1, -1, EaseState.Linear),
                        Stable(BeatTime(0.25f), 0))


                    });
                    easeA.TagApply("A");
                    easeE.TagApply("E");
                    easeB.TagApply("B");
                    /*easeE.TagApply("E");*/
                    Arrow.UnitEasing easeF = new();
                    Arrow.UnitEasing easeE2 = new();
                    AddInstance(easeE2 = new Arrow.UnitEasing()
                    {

                        ApplyTime = (BeatTime(3.9f)),
                        AlphaEase = LinkEase(Stable(0, 1),
                       EaseOut(BeatTime(0), 1, -1, EaseState.Linear),
                       Stable(BeatTime(0.25f), 0))


                    });
                    AddInstance(easeF = new Arrow.UnitEasing()
                    {

                        ApplyTime = (BeatTime(2f)),
                        AlphaEase = LinkEase(Stable(0, 0),
                       EaseOut(BeatTime(2f), 0, 1, EaseState.Linear),
                       Stable(BeatTime(0.25f), 0))


                    });
                    easeE2.TagApply("E2");
                    easeF.TagApply("F");
                    RegisterFunctionOnce("MyLine", () =>
                    { });
                    RegisterFunctionOnce("LineGA1", () =>
                    {
                        Line a = new(60, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line b = new(140, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                        ScreenDrawing.CameraEffect.Convulse(1.7f, BeatTime(2), true);
                    });
                    RegisterFunctionOnce("LineGA2", () =>
                    {
                        Line a = new(120, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line b = new(200, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                        ScreenDrawing.CameraEffect.Convulse(1.7f, BeatTime(2), true);
                    });
                    RegisterFunctionOnce("LineGB1", () =>
                    {
                        Line a = new(640 - 60, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line b = new(640 - 140, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                        ScreenDrawing.CameraEffect.Convulse(1.1f, BeatTime(2), false);
                    });
                    RegisterFunctionOnce("LineGB2", () =>
                    {
                        Line a = new(640 - 120, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line b = new(640 - 200, 90) { Alpha = 0.7f, DrawingColor = Color.MediumPurple * 0.9f };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                        ScreenDrawing.CameraEffect.Convulse(1.1f, BeatTime(2), false);
                    });
                    RegisterFunctionOnce("LineGC1", () =>
                    {
                        Line a = new(new Vector2(160 - 24, 240 + 24), EaseOut(BeatTime(2), 90, 72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line b = new(new Vector2(160 + 24, 240 - 24), EaseOut(BeatTime(2), 90, 72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineGC2", () =>
                    {
                        Line a = new(new Vector2(320 - 24, 240 + 24), EaseOut(BeatTime(2), 90, 72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line b = new(new Vector2(320 + 24, 240 - 24), EaseOut(BeatTime(2), 90, 72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineGC3", () =>
                    {
                        Line a = new(new Vector2(480 - 24, 240 + 24), EaseOut(BeatTime(2), 90, 72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line b = new(new Vector2(480 + 24, 240 - 24), EaseOut(BeatTime(2), 90, 72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineGC4", () =>
                    {
                        Line a = new(new Vector2(160 - 24, 240 - 24), EaseOut(BeatTime(2), -90, -72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line b = new(new Vector2(160 + 24, 240 + 24), EaseOut(BeatTime(2), -90, -72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineGC5", () =>
                    {
                        Line a = new(new Vector2(320 - 24, 240 - 24), EaseOut(BeatTime(2), -90, -72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line b = new(new Vector2(320 + 24, 240 + 24), EaseOut(BeatTime(2), -90, -72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineGC6", () =>
                    {
                        Line a = new(new Vector2(480 - 24, 240 - 24), EaseOut(BeatTime(2), -90, -72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line b = new(new Vector2(480 + 24, 240 + 24), EaseOut(BeatTime(2), -90, -72, EaseState.Quad)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            l.AlphaDecrease(BeatTime(2));
                            DelayBeat(2, () => { l.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("LineGEV", () =>
                    {
                        Line a = new(EaseOut(BeatTime(2), new Vector2(320, 240), new Vector2(-5, 240), EaseState.Quad), Stable(BeatTime(2), 90)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        CreateEntity(a);
                        a.TransverseMirror = true;
                        for (int i = 0; i < 6; i++)
                        {
                            a.InsertRetention(new(BeatTime(0.125f * i), 0.7f - 0.1f * i));
                        }
                        DelayBeat(2, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineGET", () =>
                    {
                        Line a = new(EaseOut(BeatTime(2), new Vector2(320, 240), new Vector2(320, -5), EaseState.Quad), Stable(BeatTime(2), 0)) { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        CreateEntity(a);
                        a.VerticalMirror = true;
                        for (int i = 0; i < 6; i++)
                        {
                            a.InsertRetention(new(BeatTime(0.125f * i), 0.7f - 0.1f * i));
                        }
                        DelayBeat(2, () => { a.Dispose(); });
                    });
                    RegisterFunctionOnce("LineXl", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(180, 240), EaseState.Quad),
                            Stable(BeatTime(1), new Vector2(180, 240))),
                            Stable(BeatTime(2.5f), 50))
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line b = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(180, 240), EaseState.Quad),
                            Stable(BeatTime(1), new Vector2(180, 240))),
                            Stable(BeatTime(2.5f), 130))
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            DelayBeat(2.5f, () => { l.Dispose(); });
                            l.AlphaDecrease(BeatTime(2.5f));
                        }
                        ScreenDrawing.CameraEffect.Convulse(1.7f, BeatTime(2), true);
                    });
                    RegisterFunctionOnce("LineXr", () =>
                    {
                        Line a = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(460, 240), EaseState.Quad),
                            Stable(BeatTime(1), new Vector2(460, 240))),
                            Stable(BeatTime(2.5f), 50))
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line b = new(LinkEase(EaseOut(BeatTime(1.5f), new Vector2(320, 240), new Vector2(460, 240), EaseState.Quad),
                            Stable(BeatTime(1), new Vector2(460, 240))),
                            Stable(BeatTime(2.5f), 130))
                        { Alpha = 0.7f, DrawingColor = Color.MediumPurple };
                        Line[] lines = { a, b };
                        foreach (Line l in lines)
                        {
                            CreateEntity(l);
                            DelayBeat(2.5f, () => { l.Dispose(); });
                            l.AlphaDecrease(BeatTime(2.5f));
                        }
                        ScreenDrawing.CameraEffect.Convulse(1.7f, BeatTime(2), false);
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                       
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "Green", "",    "", "", "", "",
                        //1
                        "(d@E)(*$01)(LineGA2)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0@E)", "", "", "",
                        "(d@E)(*$11)(LineGA2)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0@E)", "", "", "",
                        "(d@E)(*$21)(LineGA1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0@E)", "", "d@E", "",
                        "(*$31)(LineGA1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)", "", "", "",
                        //2
                        "(d@E)(*$01)(LineGA1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0@E)", "", "d@E", "",
                        "(*$11)(LineGA2)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)", "", "", "",
                        "(d@E)(*$21)(LineGA1)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0@E)", "", "d@E", "",
                        "(LineGA2)", "", "d1(LineGC3)", "",    "d0(LineGC2)", "", "d1(LineGC1)", "",
                        //3
                        "(d)(+21)(LineGB2)", "", "", "",    "+01", "", "", "",
                        "(d)(+21)(LineGA2)", "", "", "",    "+01", "", "", "",
                        "($0)(+21)(LineGA1)", "", "", "",    "(d)(+21)", "", "(d)(+21)", "",
                        "(LineGA2)", "", "", "",    "", "", "", "",
                        //4
                        "($0)(+21)(LineGB1)", "", "", "",     "(d)(+21)", "", "(d)(+21)", "",
                        "(LineGB2)", "", "", "",    "", "", "", "",
                        "($0)(+21)(LineGB1)", "", "", "",     "(d)(+21)", "", "(d)(+21)", "",
                        "(LineGB2)", "", "d(LineGC4)", "",    "d1(LineGC5)", "", "d(LineGC6)", "",
                        //5
                        "(d1@E2,F)(*$3)(LineGB2)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01@E2,F)", "", "", "",
                        "(d1@E2,F)(*$2)(LineGB2)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01@E2,F)", "", "", "",
                        "(d1@E2,F)(*$1)(LineGB1)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01@E2,F)", "", "d1@E,F", "",
                        "(*$0)(LineGB1)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)", "", "", "",
                        //6
                        "(d1@E2,F)(*$3)(LineGB1)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01@E2,F)", "", "d1@E2,F", "",
                        "(*$2)(LineGB2)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)", "", "", "",
                        "(d1@E2,F)(*$1)(LineGB1)", "~_!+0@B", "~_!+0@A", "~_!+0@B",    "(~_!+0@A)(+01@E2,F)", "", "d1@E2,F", "",
                        "(LineGB2)", "", "d(LineGC4)", "",    "+11(LineGC5)", "", "+10(LineGC6)", "",
                        //7
                        "(^d)(^+21)(LineGA2)", "", "", "",    "+01", "", "", "",
                        "(^d)(^+21)(LineGB2)", "", "", "",    "+01", "", "", "",
                        "d(LineGA1)", "", "+01", "",    "", "", "(d)(+01)", "",
                        "(LineGB1)", "", "", "",    "d1", "", "+0", "",
                        //8
                        "d1", "", "+0", "",    "+21", "", "", "",
                        "(d)(+21)(LineGEV)", "", "", "",    "(d1)(+2)(LineGET)", "", "", "",
                        "(d)(+21)(LineXr)", "", "", "",    "(d1)(+2)(LineXl)", "", "", "",
                        "(d)(+21)(LineGEV)(LineGET)", "", "", "",    "", "", "", "",
                    });
                }
                if (InBeat(184))
                {
                    easeA.TagApply("A");
                    easeB.TagApply("B");
                    easeC.TagApply("C");

                    Arrow.UnitEasing easeX = new();//创建箭头缓动的实例，其实这里也可以这么写  new(){ApplyTime=...},下面的ApplyTime就可以省略
                    AddInstance(easeX);//添加事件
                    float RunTime = BeatTime(3);//这里创建变量只是为了说明清楚ApplyTime和缓动的Time的关系，即最常见的是相等，相等的时候意味着
                    easeX.ApplyTime = RunTime;//会在标记箭头的x时间之前使用缓动，经过x时间后缓动正好完成，箭头标准地击中盾牌
                    //同样，缓动的使用也有更高级的用法，例如可以实现“偏移入眼，后转正常”的效果
                    //例如我在rotateEase中Stable了2beat，quad了2beat,ApplyTime是4beat（2beat+2beat）
                    //在实现缓动的前4beat中观众会先看到偏移的箭头运作，然后看到箭头变换到正常（不正常也可以，但不推荐）的位置
                    //ApplyTime和缓动Time不等的情况很少，因为这会导致两者时间轴不同，例如缓动没运行完箭头已经dispose了，是否有用这点需要看有什么花招了
                    easeX.PositionEase = LinkEase(Stable(0, new Vector2(0, 400)), EaseOut(BeatTime(3), new Vector2(0, -400), EaseState.Elastic));
                    easeX.TagApply("X");//注意的是，极坐标变换开启后(默认关闭)的，从下到上的0轨箭头的缓动，用在2轨箭头会变成从上到下过来，用在1轨箭头会变成从左向右
                                        //我忘了是啥，也忘了写过没，但我和master提过也讨论过，如果没有这个方法，记得问一下master，让他写一下
                    Arrow.UnitEasing easeW = new();//创建箭头缓动的实例，其实这里也可以这么写  new(){ApplyTime=...},下面的ApplyTime就可以省略
                    AddInstance(easeW);//添加事件

                    easeW.ApplyTime = BeatTime(2);//会在标记箭头的x时间之前使用缓动，经过x时间后缓动正好完成，箭头标准地击中盾牌
                    //同样，缓动的使用也有更高级的用法，例如可以实现“偏移入眼，后转正常”的效果
                    //例如我在rotateEase中Stable了2beat，quad了2beat,ApplyTime是4beat（2beat+2beat）
                    //在实现缓动的前4beat中观众会先看到偏移的箭头运作，然后看到箭头变换到正常（不正常也可以，但不推荐）的位置
                    //ApplyTime和缓动Time不等的情况很少，因为这会导致两者时间轴不同，例如缓动没运行完箭头已经dispose了，是否有用这点需要看有什么花招了
                    easeW.DistanceEase = LinkEase(Stable(0, 50), EaseOut(BeatTime(3), -50, EaseState.Elastic));
                    easeW.TagApply("W");//注意的是，极坐标变换开启后(默认关闭)的，从下到上的0轨箭头的缓动，用在2轨箭头会变成从上到下过来，用在1轨箭头会变成从左向右
                                        //我忘了是啥，也忘了写过没，但我和master提过也讨论过，如果没有这个方法，记得问一下master，让他写一下
                    RegisterFunctionOnce("LineG", () =>
                    {
                        float sin = 0;
                        for (int i = 0; i < 56 * 4; i++)
                        {
                            DelayBeat(0.25f * i, () =>
                            {
                                sin += 5;
                                Line a = new(EaseOut(BeatTime(4), new Vector2(320 - 240 + Sin(sin) * 150, 0), new Vector2(320 - 240 + Sin(sin) * 150, 480), EaseState.Linear),
                                    EaseOut(BeatTime(4), 0, -180, EaseState.Linear))
                                { Alpha = 0.1f, DrawingColor = Color.MediumPurple };
                                CreateEntity(a);
                                a.AlphaIncreaseAndDecrease(BeatTime(4), 0.55f);
                                DelayBeat(4, () => { a.Dispose(); });
                                Line b = new(EaseOut(BeatTime(4), new Vector2(320 + 240 - Sin(sin) * 150, 480), new Vector2(320 + 240 - Sin(sin) * 150, 0), EaseState.Linear),
                                    EaseOut(BeatTime(4), 0, -180, EaseState.Linear))
                                { Alpha = 0.1f, DrawingColor = Color.MediumPurple };
                                CreateEntity(b);
                                b.AlphaIncreaseAndDecrease(BeatTime(4), 0.55f);
                                DelayBeat(4, () => { b.Dispose(); });
                            });
                        }
                    });
                    RegisterFunctionOnce("LineF", () =>
                    {
                        float Y = (Sin(15) * (640 / 5)) * -20;
                        float Y2 = Y;


                        for (int i5 = 0; i5 < 56 * 0.5f; i5++)
                        {

                            DelayBeat(2 * i5, () =>
                            {
                                float g = 0.5f;
                                Y = Y2;
                                for (int i2 = 0; i2 < 7; i2++)
                                {

                                    float G = 0;
                                    for (int i = 0; i < 5; i++)
                                    {



                                        Line a = new(EaseOut(BeatTime(2), new Vector2(G, Y), new Vector2(G, Y + Sin(25) * (128) + 5), EaseState.Cubic),
                                                      EaseOut(BeatTime(2), 25, 25, EaseState.Expo))
                                        { Alpha = 0.25f, DrawingColor = Color.MediumPurple * 1f };
                                        CreateEntity(a);

                                        DelayBeat(2.05f, () => { a.Dispose(); });
                                        Line b = new(EaseOut(BeatTime(2), new Vector2(G, Y), new Vector2(G, Y + Sin(25) * (128) + 5), EaseState.Cubic),
                                                      EaseOut(BeatTime(2), -25, -25, EaseState.Expo))
                                        { Alpha = 0.25f, DrawingColor = Color.MediumPurple * 1f };
                                        CreateEntity(b);

                                        DelayBeat(2.05f, () => { b.Dispose(); });
                                        G = G + 128;

                                    }

                                    Y = Y + Sin(25) * (128) + 5;

                                }
                                Y2 = Y2 + Sin(25) * (128) + 5;
                                if (Y2 >= Sin(25) * (128) + 5) { Y2 = Sin(25) * -128 + 5; }
                            });
                        }
                        DelayBeat(56, () => {

                            Y2 = Sin(25) * -128 + 5;
                            float time = 2f;


                            float g = 0.5f;
                            Y = Y2;
                            for (int i2 = 0; i2 < 7; i2++)
                            {
                                time += 0.5f;
                                float G = 0;
                                for (int i = 0; i < 5; i++)
                                {



                                    Line a = new(EaseOut(BeatTime(2), new Vector2(G, Y), new Vector2(G, Y + Sin(25) * (128) + 5), EaseState.Cubic),
                                                  EaseOut(BeatTime(2), 25, 25, EaseState.Expo))
                                    { Alpha = 0.25f, DrawingColor = Color.MediumPurple * 1f };
                                    CreateEntity(a);

                                    DelayBeat(2.05f, () => { a.Dispose(); });
                                    Line b = new(EaseOut(BeatTime(2), new Vector2(G, Y), new Vector2(G, Y + Sin(25) * (128) + 5), EaseState.Cubic),
                                                 EaseOut(BeatTime(2), -25, -25, EaseState.Expo))
                                    { Alpha = 0.25f, DrawingColor = Color.MediumPurple * 1f };
                                    CreateEntity(b);

                                    DelayBeat(2.05f, () => { b.Dispose(); });
                                    G = G + 128;

                                }

                                Y = Y + Sin(25) * (128) + 5;

                            }




                        });
                        DelayBeat(58, () => {

                            Y2 = Sin(25) * -128 + 5;
                            float time = 2f;


                            float g = 0.5f;
                            Y = Y2;
                            for (int i2 = 0; i2 < 7; i2++)
                            {

                                float G = 0;
                                for (int i = 0; i < 5; i++)
                                {
                                    time += 0.125f;


                                    Line a = new(EaseOut(BeatTime(2), new Vector2(G, Y), new Vector2(G, Y + Sin(25) * (128) + 5), EaseState.Cubic),
                                                  EaseOut(BeatTime(4), 25, Rand(-90, 90), EaseState.Cubic))
                                    { Alpha = 0.25f, DrawingColor = Color.MediumPurple * 1f };
                                    CreateEntity(a);

                                    DelayBeat(7f, () => { a.Dispose(); });
                                    Line b = new(EaseOut(BeatTime(2), new Vector2(G, Y), new Vector2(G, Y + Sin(25) * (128) + 5), EaseState.Cubic),
                                                 EaseOut(BeatTime(4), -25, Rand(-90, 90), EaseState.Cubic))
                                    { Alpha = 0.25f, DrawingColor = Color.MediumPurple * 1f };
                                    CreateEntity(b);

                                    DelayBeat(7f, () => { b.Dispose(); });
                                    G = G + 128;
                                    SimplifiedEasing.RunEase((s) => { a.Alpha = s; b.Alpha = s; },
                                    LinkEase(Stable(BeatTime(time - 2f), 0.25f), EaseOut(BeatTime(2), -0.25f, EaseState.Cubic)));
                                    SimplifiedEasing.RunEase((s) => {
                                        a.DrawingColor = Color.Lerp(Color.MediumPurple, Color.White, s);
                                        b.DrawingColor = Color.Lerp(Color.MediumPurple, Color.White, s);
                                    },
                                    LinkEase(Stable(BeatTime(time - 2f), 0f), EaseOut(BeatTime(0.5f), 1f, EaseState.Cubic)));
                                }

                                Y = Y + Sin(25) * (128) + 5;

                            }




                        });
                    });
                    RegisterFunctionOnce("ScreenMove1", () =>
                    {
                        SimplifiedEasing.RunEase((s) => { ScreenDrawing.ScreenPositionDetla = new(s, 0); },
                            LinkEase(Stable(0, 0), EaseOut(BeatTime(2f), -50, EaseState.Back)),
                            LinkEase(Stable(0, -50), EaseOut(BeatTime(2f), 50, EaseState.Back)),
                            LinkEase(Stable(0, 0), EaseOut(BeatTime(2f), 50, EaseState.Back)),
                            LinkEase(Stable(0, 50), EaseOut(BeatTime(2f), -50, EaseState.Back)),
                            LinkEase(Stable(0, 0), EaseOut(BeatTime(2f), -50, EaseState.Back)),
                            LinkEase(Stable(0, -50), EaseOut(BeatTime(6f), 50, EaseState.Back)));

                        SimplifiedEasing.RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, 0), EaseOut(BeatTime(2f), -15, EaseState.Back)),
                            LinkEase(Stable(0, -15), EaseOut(BeatTime(2f), 15, EaseState.Back)),
                            LinkEase(Stable(0, 0), EaseOut(BeatTime(2f), 15, EaseState.Back)),
                            LinkEase(Stable(0, 15), EaseOut(BeatTime(2f), -15, EaseState.Back)),
                            LinkEase(Stable(0, 0), EaseOut(BeatTime(2f), -15, EaseState.Back)),
                            LinkEase(Stable(0, -15), EaseOut(BeatTime(4f), 15, EaseState.Elastic)));

                    });
                    RegisterFunctionOnce("ScreenMove2", () =>
                    {
                        SimplifiedEasing.RunEase((s) => { ScreenDrawing.ScreenScale = s; },
                            LinkEase(Stable(0, 1), EaseOut(BeatTime(1f), 0.25f, EaseState.Back)),
                            LinkEase(Stable(0, 1.25f), EaseOut(BeatTime(1f), 0.25f, EaseState.Back)),
                            LinkEase(Stable(0, 1.5f), EaseOut(BeatTime(1f), 0.25f, EaseState.Back)),
                            LinkEase(Stable(0, 1.75f), EaseOut(BeatTime(4f), -0.75f, EaseState.Elastic)));
                        SimplifiedEasing.RunEase((s) => { ScreenDrawing.ScreenAngle = s; },
                            LinkEase(Stable(0, 0), EaseOut(BeatTime(1f), 5f, EaseState.Back)),
                            LinkEase(Stable(0, 5f), EaseOut(BeatTime(1f), -10f, EaseState.Back)),
                            LinkEase(Stable(0, -5), EaseOut(BeatTime(1f), 10, EaseState.Back)),
                            LinkEase(Stable(0, 5), EaseOut(BeatTime(4f), -5, EaseState.Elastic)));
                    });
                    CreateChart(BeatTime(4), BeatTime(2), 6.2f, new string[]
                {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "LineG", "", "", "",
                        //1
                        "#0.75#$0(LineF)(ScreenMove1)", "", "", "",    "+2", "", "", "",
                        "#0.75#$21", "", "", "",    "+21", "", "", "",
                        "#0.75#$2", "", "", "",    "+2", "", "", "",
                        "#0.75#$01", "", "", "",    "+21", "", "", "",
                        //2
                        "#0.75#$0", "", "", "",    "+2", "", "", "",
                        "#0.75#$21", "", "", "",    "+21", "", "", "",
                        "(*$1)(+0@C)(ScreenMove2)", "", "", "",    "(*$3)(+0@C)", "", "", "",
                        "(*$0)($01@X)", "", "", "",    "(*$21)($2@X)", "", "", "",
                        //3
                        "d@C", "", "+01@W", "",    "d@C", "", "+01@W", "",
                        "d@C", "", "+01@W", "",    "d@C", "", "+01@W", "",
                        "d@C", "", "+01@W", "",    "d@C", "", "$21@W", "$11",
                        "$01", "", "+0", "",    "d1", "", "+0", "",
                        //4
                        "#1#d1", "", "", "",    "+21", "", "", "",
                        "#1#d", "", "", "",    "+2", "", "", "",
                        "d1", "", "", "",    "(d)(+21)", "", "(+1)(+21)", "",
                        "(+11)(+21)", "", "", "",    "", "", "", "",
                        //5
                        "(d)(*$21)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "", "",
                        "(d)(*$01)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "", "",
                        "(d)(*$31)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "(*$11)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)", "", "", "",
                        //6
                        "(d)(*$31)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "(*$21)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)", "", "", "",
                        "(d)(*$11)", "~_!+01@A", "~_!+01@B", "~_!+01@A",    "(~_!+01@B)(+0)", "", "d", "",
                        "", "", "d", "",    "+01", "", "+0", "",
                        //7
                        "(d)(+21)", "", "", "",    "+01", "", "", "",
                        "(d1)(+2)", "", "", "",    "+0", "", "", "",
                        "d1", "", "+0", "",    "", "", "(d)(+01)", "",
                        "", "", "", "",    "d", "", "+01", "",
                        //8
                         "$1", "", "+0", "",    "+21", "", "", "",
                        "($0)(+21)", "", "", "",    "($01)(+21)", "", "", "",
                        "($2)(+2)", "", "", "",    "($21)(+21)", "", "", "",
                        "($01)(+21)($0)($2)", "", "", "",    "", "", "", "",
                });
                }
                if (InBeat(248))
                {
                    RegisterFunctionOnce("Change0", () =>
                    {
                        SetBox(240, 340, 180);
                        SetSoul(4);
                        Heart.PurpleLineCount = 5;
                    });
                    RegisterFunctionOnce("Change1", () =>
                    {
                        SetBox(320 - 170, 320 + 170, 240 - 90, 490);
                        Heart.PurpleLineCount = 12;
                    });
                    RegisterFunctionOnce("Change2", () =>
                    {
                        SetGreenBox();
                        SetSoul(1);
                        TP();
                    });
                    RegisterFunctionOnce("SpUR", () =>
                    {
                        float r = Rand(320 - 160, 320 + 160);
                        if (r <= Heart.Centre.X + 5 && r >= Heart.Centre.X - 5) { return; }
                        Spider sp = new(LinkEase(EaseIn(BeatTime(2), new Vector2(r, 240 - 100), new Vector2(r, 240 - 10), EaseState.Quad),
                            EaseOut(BeatTime(2), new Vector2(r, 240 - 10), new Vector2(r, 240 + 70), EaseState.Quad),
                            EaseIn(BeatTime(4), new Vector2(r, 240 + 70), new Vector2(r, 240 - 100), EaseState.Quad)))
                        { Rotation = 90 };
                        Line l = new(Stable(BeatTime(8), new Vector2(r, 240 - 90)).Easing,
                            LinkEase(EaseIn(BeatTime(2), new Vector2(r, 240 - 90), new Vector2(r, 240 - 10), EaseState.Quad),
                            EaseOut(BeatTime(2), new Vector2(r, 240 - 10), new Vector2(r, 240 + 70), EaseState.Quad),
                            EaseIn(BeatTime(4), new Vector2(r, 240 + 70), new Vector2(r, 240 - 90), EaseState.Quad)).Easing)
                        { Alpha = 0.8f, Depth = 0.9f };
                        CreateEntity(sp, l);
                        DelayBeat(8, () => { l.Dispose(); sp.Dispose(); });
                    });
                    RegisterFunctionOnce("SpInMess", () =>
                    {
                        for (int i = 0; i < 96; i++)
                        {
                            DelayBeat(i * 0.25f, () =>
                            {
                                float h = Rand(60, 240);
                                float start = Rand(320 - 160, 320 + 160);
                                float end = Rand(320 - 220, 320 + 220);
                                Spider sp = new(
                                    LinkEase(EaseIn(BeatTime(h / 60), start, (start + end) / 2, EaseState.Sine),
                                    EaseOut(BeatTime(h / 60), (start + end) / 2, end, EaseState.Sine)),
                                    LinkEase(EaseOut(BeatTime(h / 60), 490, 490 - h, EaseState.Quad),
                                    EaseIn(BeatTime(h / 60), 490 - h, 490, EaseState.Quad)))
                                { Rotation = -90 };
                                CreateEntity(sp);
                                DelayBeat(h / 30, () => { sp.Dispose(); });
                            });
                        }
                    });
                    RegisterFunctionOnce("Move", () =>
                    {
                        ForBeat(4, () =>
                        {
                            Vector2 cur = ScreenDrawing.UISettings.HPShowerPos;
                            ScreenDrawing.UISettings.HPShowerPos = Vector2.Lerp(cur, new(320, 110), 0.23f);
                        });
                    });
                    RegisterFunctionOnce("Back", () =>
                    {
                        ForBeat(4, () =>
                        {
                            Vector2 cur = ScreenDrawing.UISettings.HPShowerPos;
                            ScreenDrawing.UISettings.HPShowerPos = Vector2.Lerp(cur, new(320, 443), 0.23f);
                        });
                    });
                    #region Transverse Line Spiders ( Return )
                    RegisterFunctionOnce("TL1", () =>
                    {

                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 1f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 0f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL1", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 1f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 180f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL2", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 2f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 0f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL2", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 2f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 180f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL3", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 3f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 0f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL3", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 3f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 180f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL4", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 4f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 0f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL4", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 4f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 180f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL5", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 5f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 0f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL5", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 5f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        sp.Image = Sprites.Croissant;
                        SimplifiedEasing.RunEase((s) => { sp.Rotation = s; },
                        SimplifiedEasing.EaseOut(BeatTime(8f), 180f, -3 * BeatTime(8), SimplifiedEasing.EaseState.Linear));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    #endregion
                    CreateChart(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "Change0", "", "", "",    "", "", "", "",
                        //1
                        "(SpUR)(SpUR)(TL1)(TL2)(TL3)(TL4)(TL5)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(SpUR)(SpUR)(TTL1)(TTL2)(TTL3)(TTL4)(TTL5)", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //2
                        "(SpUR)(SpUR)(TL3)", "(TL2)(TL4)", "(TL1)(TL5)", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(SpUR)(SpUR)(TTL3)", "(TTL2)(TTL4)", "(TTL1)(TTL5)", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //3
                        "(SpUR)(SpUR)(TL3)", "", "(TL2)(TL4)", "",    "(TL1)(TL5)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "(SpUR)(SpUR)(TTL3)", "", "(TTL2)(TTL4)", "",    "(TTL1)(TTL5)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //4
                        "(SpUR)(SpUR)(TL3)", "", "", "(TL2)(TL4)",    "", "", "(TL1)(TL5)", "",
                        "", "", "", "",    "", "", "", "",
                        "(SpUR)(SpUR)(TTL3)", "", "", "(TTL2)(TTL4)",    "", "", "(TTL1)(TTL5)", "",
                        "", "", "", "",    "", "", "Move", "",
                        //5
                        "Change1(SpInMess)(SpInMess)(TL1)", "", "TL4", "",    "TL2", "", "TL3", "",
                        "", "", "", "",    "", "", "", "",
                        "TTL1", "", "TTL4", "",    "TTL2", "", "TTL3", "",
                        "", "", "", "",    "", "", "", "",
                        //6
                        "TL2(TTL3)", "", "", "",    "TL1(TTL4)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "TTL1(TL3)", "", "", "",    "TTL2(TL4)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //7
                        "TTL2(TL3)", "", "", "",    "TTL1(TL4)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "TL1(TTL3)", "", "", "",    "TL2(TTL4)", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //8
                        "TTL1", "", "TTL4", "",    "TL2", "", "TL3", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "", "", "Back", "",    "Change2", "", "", "",
                    });
                }
                if (InBeat(248 + 64))
                {
                    Arrow.UnitEasing easeX = new();
                    AddInstance(easeX);
                    easeX.ApplyTime = BeatTime(4);
                    easeX.RotationEase = LinkEase(Stable(0, -70),
                        EaseOut(BeatTime(3), -70, 0, EaseState.Quad),
                        Stable(BeatTime(1), 0));
                    easeX.PositionEase = LinkEase(Stable(0, new Vector2(0, 0)),
                        EaseOut(BeatTime(3), new Vector2(0, 0), new Vector2(24, 0), EaseState.Linear),
                        Stable(BeatTime(1), new Vector2(24, 0)));
                    easeX.TagApply("X");
                    Arrow.UnitEasing easeY = new();
                    AddInstance(easeY);
                    easeY.ApplyTime = BeatTime(4);
                    easeY.RotationEase = LinkEase(Stable(0, 70),
                        EaseOut(BeatTime(3), 70, 0, EaseState.Quad),
                        Stable(BeatTime(1), 0));
                    easeY.PositionEase = LinkEase(Stable(0, new Vector2(0, 0)),
                        EaseOut(BeatTime(3), new Vector2(0, 0), new Vector2(-24, 0), EaseState.Linear),
                        Stable(BeatTime(1), new Vector2(24, 0)));
                    easeY.TagApply("Y");
                    Arrow.ClassicApplier easeK = new();
                    AddInstance(easeK);
                    DelayBeat(5.8f, () =>
                    {
                        easeK.ApplyDelay(BeatTime(2));
                    });
                    easeK.TagApply("K");
                    CreateChart(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "(d1@K)(+21@K)", "", "", "",    "D@K", "", "+01@K", "",
                        //1
                        "", "", "D@K", "",    "+01@K", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "($0)(+2)", "", "", "",    "D1", "", "+0", "",
                        "", "", "D1", "",    "+0", "", "", "",
                        //2
                        "$0", "$1", "$2", "",    "+01", "", "", "",
                        "($01)(+2)", "", "", "",    "D1", "", "#2#+0", "",
                        "", "", "", "",    "", "", "", "",
                        "+01", "+01", "+11", "+01",    "+11", "+01", "+11", "+01",
                        //3
                        "($0)(+2)", "", "", "",    "D1", "", "+0", "",
                        "", "", "D1", "",    "+0", "", "", "",
                        "($01)(+21)", "", "", "",    "D", "", "+01", "",
                        "", "", "D", "",    "+01", "", "", "",
                        //4
                        "(d)(+01)", "", "", "",    "(D)(+01)", "", "", "",
                        "D", "", "+01", "",    "", "", "D", "",
                        "#3.75#+01", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //5
                        "($1)(+2)", "", "", "",    "D1", "", "+0", "",
                        "", "", "D1", "",    "+0", "", "", "",
                        "($01)(+21)", "", "", "",    "D", "", "+01", "",
                        "", "", "D", "",    "+01", "", "", "",
                        //6
                        "$21", "$11", "$01", "",    "+0", "", "", "",
                        "($0)(+21)", "", "", "",    "D", "", "#2#+01", "",
                        "", "", "", "",    "", "", "", "",
                        "+0", "+3", "+0", "+3",    "+0", "+3", "+0", "+3",
                        //7
                        "($01)(+21)", "", "", "",    "D", "", "+01", "",
                        "", "", "D", "",    "+01", "", "", "",
                        "($2)(+2)", "", "", "",    "D1", "", "+0", "",
                        "", "", "D1", "",    "+0", "", "", "",
                        //8
                        "(*$2)(+0)", "", "", "",    "(*$01)(+01)", "", "", "",
                        "($0)($2)", "", "($01)($21)", "",    "", "", "($001)($201)", "",
                        "*$3'0.7", "*$31@X", "_$3'0.85", "_$31@Y",    "*$3", "*$31@X", "_$3'1.15", "_$31@Y",
                        "*$3'1.3", "*$31@X", "_$3'1.45", "_$31@Y",    "*$3'1.6", "*$31@X", "_$3'1.75", "_$31@Y",
                    });
                }
                if (InBeat(376))
                {
                    easeB.TagApply("C");
                    Arrow.UnitEasing easeI = new();
                    AddInstance(easeI);
                    Arrow.UnitEasing easeJ = new();
                    AddInstance(easeJ);
                    Arrow.UnitEasing easeK = new();
                    AddInstance(easeK);
                    Arrow.UnitEasing easeV = new();
                    AddInstance(easeV);
                    Arrow.UnitEasing easeZ = new();
                    AddInstance(easeZ);
                    easeI.ApplyTime = BeatTime(4);
                    easeJ.ApplyTime = BeatTime(4);
                    easeK.ApplyTime = BeatTime(4);
                    easeV.ApplyTime = BeatTime(4);
                    easeZ.ApplyTime = BeatTime(4);
                    easeI.PositionEase = Stable(BeatTime(4), new Vector2(12, 0));
                    easeJ.PositionEase = Stable(BeatTime(4), new Vector2(-8, 0));
                    easeK.PositionEase = Stable(BeatTime(4), new Vector2(18, 0));
                    easeV.PositionEase = Stable(BeatTime(4), new Vector2(-23, 0));
                    easeZ.PositionEase = Stable(BeatTime(4), new Vector2(26, 0));
                    easeI.TagApply("I");
                    easeJ.TagApply("J");
                    easeK.TagApply("K");
                    easeV.TagApply("V");
                    easeZ.TagApply("Z");
                    CreateChart(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        //1
                        "d@C", "", "", "",    "+11@C", "", "", "",
                        "+1@C", "", "", "",    "+11@C", "", "", "",
                        "D", "", "D", "",    "D", "", "", "",
                        "d1", "", "+0", "",    "D", "", "+01", "",
                        //2
                        "", "", "$1", "",    "~$1@I", "", "~$1@J", "",
                        "~$1@K", "", "~$1@V", "",    "~$1@Z", "", "~$1@V", "",
                        "~$1@K", "", "~$1@Z", "",    "~$1@J", "", "~$1@I", "",
                        "", "", "", "",    "", "", "", "",
                        //end
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
                GametimeDelta = -2.5f;
                SetSoul(1);
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(320, 240);
                ScreenDrawing.ScreenScale = 2;
                HeartAttribute.MaxHP = 10;
                bool jump = false;
                if (jump)
                {

                    //int beat = 54 + 128 + 64 + 64; 
                    //int beat = 54 + 64;
                    //int beat = 22;
                    int beat = 182;
                    GametimeDelta = -3.5f + BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
                else { ScreenDrawing.ScreenAngle = 180; }
            }
        }
    }
}