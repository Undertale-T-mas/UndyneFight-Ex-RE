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
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Easy,7.0f),
                    new(Difficulty.Extreme,17.5f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,7.0f),
                    new(Difficulty.Extreme,17.5f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
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
                    easeC.TagApply("C");
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        RunEase(s => ScreenDrawing.ScreenScale = s,
                            LinkEase(EaseOut(BeatTime(1), 2, 1.75f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.75f, 1.5f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.5f, 1.25f, EaseState.Back),
                            EaseOut(BeatTime(1), 1.25f, 1, EaseState.Back)));
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
                    BarrageCreate(BeatTime(0), BeatTime(2), 6.2f, new string[]
                    { 
                        //1
                        "Shrink(LineA)(LineA1)", "", "", "",    "(LineA)(LineA1)(LineA2)", "", "", "",
                        "(LineA)(LineA1)(LineA2)(LineA3)", "", "", "",    "(LineA)(LineA1)(LineA2)(LineA3)(LineA4)", "", "", "",//pre
                        "", "", "d", "",    "d", "", "", "",
                        "d", "", "+01", "",    "d", "", "+01", "",
                        //2
                        "", "", "d", "",    "d1", "", "+0", "",
                        "d1", "", "+0", "",    "d1", "", "+0", "",
                        "(d)(+0)", "", "(d)(+0)", "",    "(d)(+0)", "", "(d)(+0)", "",
                        "", "", "d", "",    "$2", "+11", "+1", "",
                        //3
                        "(d1)(+0)", "", "", "",    "", "", "", "",
                        "(-1)(+01)", "", "", "",    "", "", "", "",
                        "(-11)(+0)", "", "", "",    "", "", "", "",
                        "(-1)(+01)", "", "", "",    "", "", "", "",
                        //4
                        "*d'1.2(LineB1)", "~_!+0'1.2@C", "~_!+0'1.2@C", "~_!+0'1.2@C",    "*+11'1.2(LineB2)", "~_!+01'1.2@C", "~_!+01'1.2@C", "~_!+01'1.2@C",
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
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
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
                        "(~_$01'1.2@B)", "", "$3", "",    "$01", "+1", "+11", "",
                        //3
                        "(#1.75#$0)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        "(#1.75#$1)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        "(#1.75#$2)(*+21)", "", "*+01", "",    "*+01", "", "", "",
                        "(#1.75#$3)(*+21)", "", "*+01", "",    "*+01", "", "", "",
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
                        SetBox(240, 240, 160);
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
                    #endregion
                    RegisterFunctionOnce("SpA", () =>
                    {

                    });
                    RegisterFunctionOnce("Line1s", () =>
                    {
                        float y = 240;
                        float x = 0;
                        for (int i = 0; i < 32; i++)
                        {
                            DelayBeat(i * 0.5f, () => {

                                x += 8;
                                y += Sin(x * 5) * 60;
                                Line d;
                                CreateEntity(d = new Line(EaseOut(BeatTime(2), new Vector2(y, 0), new Vector2(y + 50, 0), EaseState.Quint).Easing,
                                    EaseOut(BeatTime(2), new Vector2(y, 480), new Vector2(y + 50, 480), EaseState.Quint).Easing));
                                Line c;
                                CreateEntity(c = new Line(EaseOut(BeatTime(2), new Vector2(y, 0), new Vector2(y + -50, 0), EaseState.Quint).Easing,
                                    EaseOut(BeatTime(2), new Vector2(y, 480), new Vector2(y + -50, 480), EaseState.Quint).Easing));
                                d.DrawingColor = Color.MediumPurple;
                                d.Depth = 0f;
                                c.Depth = 0f;
                                d.AlphaDecrease(BeatTime(2));
                                c.DrawingColor = Color.MediumPurple;
                                c.AlphaDecrease(BeatTime(2));
                            });
                        }
                        DelayBeat(16, () => {
                            Line d;
                            CreateEntity(d = new Line(EaseOut(BeatTime(4), new Vector2(0, 0), new Vector2(200, 0), EaseState.Cubic).Easing,
                                EaseOut(BeatTime(4), new Vector2(0, 480), new Vector2(200, 480), EaseState.Cubic).Easing));
                            d.DrawingColor = Color.MediumPurple;
                        });
                        DelayBeat(18, () => {
                            Line d;
                            CreateEntity(d = new Line(EaseOut(BeatTime(4), new Vector2(640, 0), new Vector2(440, 0), EaseState.Cubic).Easing,
                                EaseOut(BeatTime(4), new Vector2(640, 480), new Vector2(440, 480), EaseState.Cubic).Easing));
                            d.DrawingColor = Color.MediumPurple;
                        });
                        DelayBeat(20, () => {
                            Line d;
                            CreateEntity(d = new Line(EaseOut(BeatTime(4), new Vector2(0, 0), new Vector2(0, 100), EaseState.Cubic).Easing,
                                EaseOut(BeatTime(4), new Vector2(640, 0), new Vector2(640, 100), EaseState.Cubic).Easing));
                            d.DrawingColor = Color.MediumPurple;
                        });
                        DelayBeat(22, () => {
                            Line d;
                            CreateEntity(d = new Line(EaseOut(BeatTime(4), new Vector2(0, 480), new Vector2(0, 380), EaseState.Quint).Easing,
                                EaseOut(BeatTime(4), new Vector2(640, 480), new Vector2(640, 380), EaseState.Quint).Easing));
                            d.DrawingColor = Color.MediumPurple;
                        });
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    { 
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "SetBox", "",    "", "", "", "",
                        //1
                        "", "", "Purple(Line1s)", "",    "ChangeA", "", "", "",
                        "", "", "", "",    "", "", "", "",
                        "Sp1", "", "Sp2", "",     "Sp3", "", "Sp4","",
                        "Sp5", "", "Sp6", "",     "Sp5", "", "Sp4","",
                        //2
                        "Sp3", "", "Sp2", "",     "Sp1", "", "", "",
                        "tSp6", "", "tSp5", "",     "tSp4", "", "tSp3", "",
                        "tSp2", "", "tSp1", "",     "tSp2", "", "tSp3", "",
                        "tSp4", "", "tSp5", "",     "tSp6", "", "", "",
                        //3
                        "(Sp1)(Sp2)(Sp3)", "", "", "",    "", "", "", "",
                        "(Sp6)(Sp5)(Sp4)", "", "", "",    "", "", "", "",
                        "(Sp1)(Sp2)(Sp3)", "", "", "",    "", "", "", "",
                        "(Sp6)(Sp5)(Sp4)", "", "", "",    "", "", "", "",
                        //4
                        "(tSp1)(tSp6)", "", "(tSp1)(tSp6)", "",     "(tSp2)(tSp5)", "", "(tSp2)(tSp5)", "",
                        "(tSp3)(tSp4)", "", "(tSp3)(tSp4)", "",     "(tSp3)(tSp4)", "", "(tSp3)(tSp4)", "",
                        "(tSp2)(tSp5)", "", "(tSp2)(tSp5)", "",     "(tSp1)(tSp6)", "", "(tSp1)(tSp6)", "",
                        "(Sp1)(Sp6)", "", "", "",    "", "", "", "",
                        //5
                        "(tSp1)(tSp6)", "", "", "",    "(Sp2)(Sp5)", "", "", "",
                        "(tSp3)(tSp4)", "", "", "",    "(Sp1)(Sp6)", "", "", "",
                        "tSp1", "", "tSp2", "",     "tSp3", "", "tSp4","",
                        "tSp5", "", "tSp6", "",     "tSp5", "", "tSp4","",
                        //6
                        "tSp3", "", "tSp2", "",     "tSp1", "", "", "",
                        "Sp6", "", "Sp5", "",     "Sp4", "", "Sp3", "",
                        "Sp2", "", "Sp1", "",     "Sp2", "", "Sp3", "",
                        "Sp4", "", "Sp5", "",     "Sp6", "", "", "",
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
                       AlphaEase=LinkEase(Stable(0, 1),
                        EaseOut(BeatTime(3), 1, -1, EaseState.Linear),
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

                        ApplyTime = (BeatTime(1.5f)),
                        AlphaEase = LinkEase(Stable(0, 0),
                       EaseOut(BeatTime(1.5f), 0, 1, EaseState.Linear),
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
                        foreach(Line l in lines)
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
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
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
                        "(d)(+21)(LineGB2)", "", "", "",    "+0", "", "", "",
                        "(d1)(+2)(LineGA2)", "", "", "",    "+01", "", "", "",
                        "($0)(+2)(LineGA1)", "", "", "",    "(d)(+21)", "", "(d)(+21)", "",
                        "(LineGA2)", "", "", "",    "", "", "", "",
                        //4
                        "($01)(+21)(LineGB1)", "", "", "",     "(d)(+21)", "", "(d)(+21)", "",
                        "(LineGB2)", "", "", "",    "", "", "", "",
                        "($0)(+2)(LineGB1)", "", "", "",     "(d)(+21)", "", "(d)(+21)", "",
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
                        "(^d)(^+21)(LineGA2)", "", "", "",    "+0", "", "", "",
                        "(^d1)(^+2)(LineGB2)", "", "", "",    "+01", "", "", "",
                        "d(LineGA1)", "", "+01", "",    "", "", "(d)(+01)", "",
                        "(LineGB1)", "", "", "",    "d1", "", "+0", "",
                        //8
                        "d1", "", "+0", "",    "+21", "", "", "",
                        "(d1)(+21)(LineGEV)", "", "", "",    "(d11)(+201)(LineGET)", "", "", "",
                        "(d)(+2)(LineXr)", "", "", "",    "(d11)(+201)(LineXl)", "", "", "",
                        "(d01)(+211)(LineGEV)(LineGET)", "", "", "",    "", "", "", "",
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
                    RegisterFunctionOnce("LineG", () =>
                    {
                        for (int i = 0; i < 56 * 4; i++)
                        {
                            DelayBeat(0.25f * i, () =>
                            {
                                Line a = new(EaseOut(BeatTime(4), new Vector2(320 - 240, 0), new Vector2(320 - 240, 480), EaseState.Linear),
                                    EaseOut(BeatTime(4), 0, -180, EaseState.Linear))
                                { Alpha = 0.1f, DrawingColor = Color.MediumPurple };
                                CreateEntity(a);
                                a.AlphaIncreaseAndDecrease(BeatTime(4), 0.55f);
                                DelayBeat(4, () => { a.Dispose(); });
                                Line b = new(EaseOut(BeatTime(4), new Vector2(320 + 240, 480), new Vector2(320 + 240, 0), EaseState.Linear),
                                    EaseOut(BeatTime(4), 0, -180, EaseState.Linear))
                                { Alpha = 0.1f, DrawingColor = Color.MediumPurple };
                                CreateEntity(b);
                                b.AlphaIncreaseAndDecrease(BeatTime(4), 0.55f);
                                DelayBeat(4, () => { b.Dispose(); });
                            });
                        }
                    });
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
                    {
                        //pre
                        "", "", "", "",    "", "", "", "",
                        "", "", "", "",    "LineG", "", "", "",
                        //1
                        "#0.75#$0", "", "", "",    "+2", "", "", "",
                        "#0.75#$21", "", "", "",    "+21", "", "", "",
                        "#0.75#$2", "", "", "",    "+2", "", "", "",
                        "#0.75#$01", "", "", "",    "+21", "", "", "",
                        //2
                        "#0.75#$0", "", "", "",    "+2", "", "", "",
                        "#0.75#$21", "", "", "",    "+21", "", "", "",
                        "(*$1)(+0@C)", "", "", "",    "(*$3)(+0@C)", "", "", "",
                        "(*$0)($01@X)", "", "", "",    "(*$21)($2@X)", "", "", "",
                        //3
                        "d@C", "", "+01@X", "",    "d@C", "", "+01@X", "",
                        "d@C", "", "+01@X", "",    "d@C", "", "+01@X", "",
                        "d@C", "", "+01@X", "",    "d@C", "", "$21@X", "$11",
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
                                { Rotation = -90, MarkScore = false };
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
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL1", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 1f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL2", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 2f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL2", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 2f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL3", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 3f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL3", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 3f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL4", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 4f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL4", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 4f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TL5", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 5f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 - 180, y), new Vector2(320 + 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 + 20, y), new Vector2(320 - 180, y), EaseState.Sine)));
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    RegisterFunctionOnce("TTL5", () =>
                    {
                        float y = BoxStates.Centre.Y - BoxStates.Height / 2f + BoxStates.Height / (Heart.PurpleLineCount + 1) * 5f;
                        Spider sp = new(LinkEase(EaseOut(BeatTime(3), new Vector2(320 + 180, y), new Vector2(320 - 20, y), EaseState.Sine),
                            EaseIn(BeatTime(3), new Vector2(320 - 20, y), new Vector2(320 + 180, y), EaseState.Sine)))
                        { Rotation = 180 };
                        CreateEntity(sp);
                        DelayBeat(6, () => { sp.Dispose(); });
                    });
                    #endregion
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
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
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
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
                        "*$3'0.7", "*$31@X", "*$3'0.85", "*$31@Y",    "*$3", "*$31@X", "*$3'1.15", "*$31@Y",
                        "*$3'1.3", "*$31@X", "*$3'1.45", "*$31@Y",    "*$3'1.6", "*$31@X", "*$3'1.75", "*$31@Y",
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
                    BarrageCreate(BeatTime(4), BeatTime(2), 6.2f, new string[]
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
                HeartAttribute.MaxHP = 8;
                bool jump = true;
                if (jump)
                {

                    //int beat = 54 + 128 + 64 + 64; 
                    int beat = 54 +128;
                    GametimeDelta = -3.5f + BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}