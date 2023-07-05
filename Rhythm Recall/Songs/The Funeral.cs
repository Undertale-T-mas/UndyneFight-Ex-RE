using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    internal partial class TheFuneral : IChampionShip
    {
        public TheFuneral()
        {
            Game.game = new Game(); 
            difficulties = new();
            difficulties.Add("div2", Difficulty.Normal);
            difficulties.Add("div1", Difficulty.Extreme);
            //    this.difficulties.Add("Anomaly Test", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties; 

        public IWaveSet GameContent => new Game();
        public partial class Game : WaveConstructor, IWaveSet
        {
            public void ExtremePlus()
            {
                GameStates.ResetScene(new TranscendenceAnomaly((int)Difficulty.Extreme));
            }

            private class ThisImformation : SongImformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 14.0f),
                            new(Difficulty.Extreme, 18.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 14.0f),
                            new(Difficulty.Extreme, 18.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 16.5f),
                            new(Difficulty.Extreme, 21.7f),
                        }
                    );
                public override string BarrageAuthor => "T-mas";
                public override string AttributeAuthor => "T-mas & Walar";
                public override string PaintAuthor => "?";
                public override string SongAuthor => "?";
            }
            public SongImformation Attributes => new ThisImformation();

            public Game() : base(62.5f / (220f / 60) / 1) { }

            public static Game game;

            public string Music => "The Funeral";
            //  public string Music => "Brain Power";
            public string FightName => "The Funeral";

            private ScreenDrawing.Shaders.RGBSplitting splitter = new();

            class FakeArrow : AutoEntity
            {
                public FakeArrow(int color, int rotateType)
                {
                    Image = Sprites.arrow[color, rotateType, 0];
                }
                public override void Update()
                {

                }
            }

            GlobalResources.Effects.StepSampleShader StepSample;
            GlobalResources.Effects.ScaleShader ShadersScale;
            GlobalResources.Effects.CameraShader Effect3D;
            ScreenDrawing.Shaders.Blur Blur;
            Shader SinWave;
            GlobalResources.Effects.GrayShader Gray;

            RenderProduction cameraProduction, production1, production2, production3, grayProduction;

            public new void Start()
            {
                // ScreenDrawing.UISettings.CreateUISurface();
                StepSample = Shaders.StepSample;
                ShadersScale = Shaders.Scale;
                Effect3D = Shaders.Camera;
                Gray = Shaders.Gray;

                SinWave = new Shader(Loader.Load<Effect>("Musics\\DustTrust\\shake"));

                SinWave.Parameters["frequency"].SetValue(0f);
                SinWave.Parameters["distance"].SetValue(new Vector2(0f, 0f));
                SinWave.Parameters["range"].SetValue(0f);
                SinWave.Parameters["frequency2"].SetValue(0f);
                SinWave.Parameters["range2"].SetValue(0f);
                SinWave.Parameters["time"].SetValue(0f);
                SinWave.Parameters["time2"].SetValue(0f);

                StepSample.Intensity = 0.01f;
                ShadersScale.Intensity = 0;

                production1 = new Filter(Shaders.StepSample, 0.51f);
                production2 = new Filter(Shaders.Scale, 0.501f);
                production3 = Blur = new Blur(0.505f);
                grayProduction = new Filter(Shaders.Gray, 0.998f);

                Blur.Sigma = 0.0f;

                // ScreenDrawing.SceneRendering.InsertProduction(production1);
                //  ScreenDrawing.SceneRendering.InsertProduction(production2);
                // ScreenDrawing.SceneRendering.InsertProduction(production3); 

                /*  var v = Shaders.Polar;
                  v.Intensity = 0.1f;
                  ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(v));
                */
                Effect3D.CameraRotation = new(0, 0, 0);

                splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                //  var production2 = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                splitter.Intensity = 1.0f;
                //  production2.RandomDisturb = 3.0f;
                splitter.RandomDisturb = 0;
                //       ScreenDrawing.SceneRendering.InsertProduction(splitter); 
                game = this;

                HeartAttribute.MaxHP = 6;
                HeartAttribute.Speed = 3.26f;
                HeartAttribute.SoftFalling = true;

                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.HPExistColor = Color.Silver;

                InstantTP(-200, -200);
                InstantSetBox(new Vector2(180, 560), 84, 84);
                SetSoul(1);
                HeartAttribute.ArrowFixed = true;

                /*     ScreenDrawing.SceneRendering.InsertProduction(new Filter(Shaders.Swirl));
                     Shaders.Swirl.Radius = 100;
                     Shaders.Swirl.RadiusOut = 150;
                     Shaders.Swirl.Intensity = 1.0f;
                     Shaders.Swirl.Rotation = 15f;*/

                DelayBeat(2, () => {
                    ScreenDrawing.SceneRendering.InsertProduction(new Filter(Shaders.Blur, 0.99f));
                    Shaders.Blur.Factor = new(3, 0);
                    Shaders.Blur.Sigma = 3.0f;
                    SimplifiedEasing.RunEase((s) => Shaders.Blur.Sigma = s, 
                        SimplifiedEasing.EaseOut(BeatTime(4), 3, 0,
                        SimplifiedEasing.EaseState.Cubic));
                });
                //  ScreenDrawing.UISettings.CreateUISurface();

                //      CreateLine(150, 100);
                //   GametimeDetla = 4300;
                GametimeDelta = 0.7f; //+ game.BeatTime(311);
                bool delayEnable = false;
                if (delayEnable)
                {
                    float delay = BeatTime(62);
                    //  delay = BeatTime(10 + 366);
                    PlayOffset = delay + 2.1f;
                    GametimeDelta += delay;
                    InstantSetBox(new Vector2(320, 240), 84, 84);
                    InstantTP(new Vector2(320, 240));
                }
                else
                {
                    PlayOffset = 0;
                }
                /*
                LinePlate p = new(128);
                p.Radius = 555;
                p.Centre = new(320, 130);
                AddInstance(p);
                p.Factor = 44;
                p.Alpha = 0.2f;
                for (int i = 2; i < 127; i++) {
                    int t = i;
                    DelayBeat(i * 16, () => p.Factor = t);
                }*/
            }

            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Normal()
            {
                if (InBeat(0.5f))
                {
                    SetSoul(Souls.YellowSoul);
                    InstantSetBox(260, 150, 100);
                    BoxUtils.Vertexify();
                    BoxUtils.VertexBoxInstance.SetPosition(
                        BoxUtils.VertexBoxInstance.Split(1, 0.35f), 
                        new Vector2(340, 350)
                        );
                    ;
                    BoxUtils.VertexBoxInstance.SetPosition(
                        BoxUtils.VertexBoxInstance.Split(2, 0.65f), 
                        new Vector2(300, 350)
                        ); 
                    BoxUtils.VertexBoxInstance.SetPosition(
                        BoxUtils.VertexBoxInstance.Split(5, 0.35f), 
                        new Vector2(300, 250)
                        );
                    ;
                    BoxUtils.VertexBoxInstance.SetPosition(
                        BoxUtils.VertexBoxInstance.Split(6, 0.65f), 
                        new Vector2(340, 250)
                        ); 
                    InstantTP(320, 260);
                } 

            }

            public void Hard()
            {
                
            }

            public void Extreme()
            {
                if (GametimeF < 0) return;
                if (InBeat(1))
                {
                    RegisterFunctionOnce("Rise", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3), CentreEasing.EaseOutQuart(new Vector2(320, 600), new Vector2(320, 240), BeatTime(3)));
                        builder.Run(s => { BoxStates.Centre = s; Heart.InstantTP(s); });

                        ValueEasing.EaseBuilder builder1 = new();
                        builder1.Adjust = false;
                        builder1.Insert(BeatTime(1), ValueEasing.EaseOutCirc(0, 0.5f, BeatTime(1)));
                        for (int i = 0; i < 6; i++)
                            builder1.Insert(BeatTime(0.25f), ValueEasing.EaseOutCirc(0.2f, 0.5f, BeatTime(0.25f)));
                        builder1.Run(s => BoxStates.CurrentBox.InstantSetAlpha(s));
                    });
                    RegisterFunctionOnce("Move", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuart(0, 2.0f, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(2.0f, -2.0f, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(-2.0f, 2.0f, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(2.0f, -2.0f, BeatTime(8)));

                        builder.Run(s => ScreenDrawing.ScreenAngle = s);
                        ValueEasing.EaseBuilder builder1 = new();

                        builder1.Insert(BeatTime(8), ValueEasing.EaseOutQuart(1.0f, 1.02f, BeatTime(8)));
                        builder1.Insert(BeatTime(8), ValueEasing.EaseOutBack(1.02f, 1.04f, BeatTime(8)));
                        builder1.Insert(BeatTime(8), ValueEasing.EaseOutBack(1.04f, 1.06f, BeatTime(8)));
                        builder1.Insert(BeatTime(8), ValueEasing.EaseOutBack(1.06f, 1.08f, BeatTime(8)));

                        builder1.Run(s => ScreenDrawing.ScreenScale = s);
                    });
                    BarrageCreate(0, BeatTime(2), 6.5f, new string[] {
                        "Rise", "", "", "", "", "",
                        "", "", "", "", "", "",
                        "#7#R(Move)", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "#7#R1", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "#7#R", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "#7#R1", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                    });
                }
                if (InBeat(32))
                {
                    RegisterFunctionOnce("Move", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(-2.0f, 2.0f, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(2.0f, -2.0f, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(-2.0f, 2.0f, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuart(2.0f, 0f, BeatTime(8)));

                        builder.Run(s => ScreenDrawing.ScreenAngle = s);
                        ValueEasing.EaseBuilder builder1 = new();

                        builder1.Insert(BeatTime(8), ValueEasing.EaseOutBack(1.08f, 1.06f, BeatTime(8)));
                        builder1.Insert(BeatTime(8), ValueEasing.EaseOutBack(1.06f, 1.04f, BeatTime(8)));
                        builder1.Insert(BeatTime(8), ValueEasing.EaseOutBack(1.04f, 1.02f, BeatTime(8)));
                        builder1.Insert(BeatTime(8), ValueEasing.EaseOutQuart(1.02f, 1.0f, BeatTime(8)));

                        builder1.Run(s => ScreenDrawing.ScreenScale = s);
                    });
                    BarrageCreate(0, BeatTime(2), 6.5f, new string[] {
                        "", "", "", "", "", "",  "", "",
                        "", "", "", "", "", "",  "", "",
                        "#7#R(Move)", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "#7#R1", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "#7#R", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "R1", "", "", "",    "R1", "", "", "",
                        "#7#R1", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                        "R", "", "", "",    "R", "", "", "",
                    });
                }
                if (InBeat(64))
                {
                    RegisterFunctionOnce("Move", () => { });
                    RegisterFunctionOnce("Shatter", () =>
                    {
                        for (int i = 0; i < 3; i++)
                            CreateHeart(2240, 84, 84);

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1), CentreEasing.Alternate(1.0f,
                                CentreEasing.EaseOutElastic(new Vector2(320, 240), new Vector2(140, 280), BeatTime(1)),
                                CentreEasing.EaseOutQuint(new Vector2(320, 240), new Vector2(380, 280), BeatTime(1)))
                            );

                        builder.Run(s =>
                        {
                            SetPlayerBoxMission(0);
                            InstantTP(s);
                            BoxStates.Centre = s;
                            s = new Vector2(640 - s.X, s.Y);
                            SetPlayerBoxMission(1);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });

                        DelayBeat(1, () =>
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                SetPlayerBoxMission(i);
                                Vector2 basis = new Vector2(140 + 120 * i, 300);
                                InstantTP(basis);
                                Heart.FixArrow = false;
                                BoxStates.Centre = basis;

                                int t = i;

                                CentreEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(1), CentreEasing.Alternate(1.0f,
                                        CentreEasing.EaseOutCirc(basis, basis + new Vector2(0, 120), BeatTime(1)),
                                        CentreEasing.Stable(basis + new Vector2(0, 120))
                                    ));
                                builder.Run(s =>
                                {
                                    SetPlayerBoxMission(t);
                                    BoxStates.Centre = s;
                                    InstantTP(s);
                                });
                            }
                            SetPlayerMission(0);
                            Heart.InstantSetRotation(180);
                            SetPlayerMission(1);
                            Heart.InstantSetRotation(-90);
                            SetPlayerMission(2);
                            Heart.InstantSetRotation(90);
                            SetPlayerMission(3);
                            Heart.InstantSetRotation(180);
                        });
                    });
                    RegisterFunctionOnce("Apply3D", () => { Apply3D(); ScreenDrawing.UISettings.CreateUISurface(); });
                    BarrageCreate(0, BeatTime(2), 6.5f, new string[] {
                        "", "", "", "", "", "",  "", "",
                        "", "", "", "", "", "",  "", "",
                        "(Move)(^d'1.7)(^+21'1.7)", "", "", "",    "(^d'1.7)(^+21'1.7)", "", "", "",
                        "(^d'1.7)(^+21'1.7)", "", "", "",     "(^d'1.7)(^+21'1.7)", "", "", "",
                        "(^d'1.7)(^+21'1.7)", "", "", "",     "(^d'1.7)(^+21'1.7)", "", "", "",
                        "(^d'1.7)(^+21'1.7)", "", "", "",     "(^d'1.7)(^+21'1.7)", "", "", "",
                        "(^d'1.7)(^+21'1.7)", "", "^+0'1.7", "",     "(^d'1.7)(^+21'1.7)", "", "", "",
                        "(^d'1.7)(^+21'1.7)", "", "^+0'1.7", "",     "(^d'1.7)(^+21'1.7)", "", "", "",
                        "(^d'1.7)(^+21'1.7)", "", "^+0'1.7", "",     "(^d'1.7)(^+21'1.7)", "", "", "",
                        "(^d'1.7)(^+21'1.7)", "", "^+0'1.7", "",     "(^d'1.7)(^+21'1.7)", "", "", "",
                        "!!3/6",     "#3#R", "D1", "+11",    "+11", "+11", "+11",
                        "!!3/6",     "+11", "+11", "+11",    "+11", "", "",
                        "!!3/6",     "#3#R1", "D", "+1",    "+1", "+1", "+1",
                        "!!3/6",     "+1", "+1", "+1",    "+1", "", "",
                        "!!3/6",     "d", "d1", "d",    "d1", "d", "d1",
                        "!!3/6",     "d", "d1", "d",    "(d1)(+0)", "", "",
                        "$1'1.3", "$2'1.3", "$3'1.3", "",    "$11'1.3", "$01'1.3", "$31'1.3(Apply3D)", "(Shatter)",
                        ">$3", "<$3", ">$3", "<$3",    "", "", "", "",
                    });
                }
                if (InBeat(98.7f))
                {
                    DelayEnabled = false;
                    Give3DFunctions();
                    RegisterFunctionOnce("To5k", () =>
                    {
                        Delay(CurrentTime, () =>
                        {
                            CreateHeart(1000, 84, 84);
                            SetPlayerMission(4);
                            Heart.RotateTo(-90);
                            InstantSetBox(new Vector2(690, 420), 84, 84);
                            CentreEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(1), CentreEasing.EaseOutCubic(
                                new Vector2(800, 420), new Vector2(620, 420), BeatTime(1)));
                            builder.Insert(BeatTime(1), CentreEasing.EaseOutCubic(
                                new Vector2(620, 420), new Vector2(520, 420), BeatTime(1)));
                            builder.Run(s =>
                            {
                                SetPlayerBoxMission(4);
                                InstantTP(s);
                                BoxStates.Centre = s;
                            });
                            DelayBeat(1, () =>
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    int t = i;
                                    CentreEasing.EaseBuilder builder2 = new();
                                    builder2.Insert(BeatTime(1), CentreEasing.EaseOutCubic(
                                        new Vector2(140 + 120 * t, 420), new Vector2(120 + 100 * t, 420), BeatTime(1)));
                                    builder2.Run(s =>
                                    {
                                        SetPlayerBoxMission(t);
                                        InstantTP(s);
                                        BoxStates.Centre = s;
                                    });
                                }
                            });
                        });
                    });
                    BarrageCreate(0, BeatTime(2), 6.5f, new string[] {
                        "", "", "", "",   "",
                        "T0(CameraZL)", "T1", "T2", "T3",    "T0(CameraZL)", "T1", "T2", "T3",
                        "T0(CameraZL)", "T1", "T2", "T3",    "T0(CameraZL)", "T1", "T2", "T3",
                        "T2(CameraA1)", "T1", "T0", "T1",    "T0", "T1", "T0", "T1",
                        "T2", "T3", "T2", "T3",    "T2", "T3", "T2", "T3",

                        "T0", "T1", "T3", "T2",    "T0", "T1", "T3", "T2",
                        "T1", "T0", "T2", "T3",    "T1", "T0", "T2", "T3",
                        "T0", "T1", "T0", "T2",    "T3", "T2", "T0", "T1",
                        "T3", "T2", "T3", "T1",    "T0", "T1", "T3", "",

                        "T1(T2)", "", "(T0)T3", "",    "T0(T2)(T3)", "T0V(T2V)", "T0V(T2V)(T1)", "T0V(T2V)",
                        "T0V(T2V)(T3)", "T0V", "T0V(T2)", "",     "T1(T2)", "", "T2(T3)", "",
                        "T1(T2)", "", "T1(T0)", "",     "T1", "T2", "T1", "T2",
                        "T0(T3)", "(T0V)(T3V)", "(T3V)", "",     "T1(T2)", "", "T1(T3)", "",

                        "T0(T1)", "", "T1(T2)", "",     "T0(T2)", "T0V(T2V)", "T0V(T2V)", "T0V(T2V)",
                        "T2(T3)", "", "T2(T0)", "",     "T0(T1)", "", "T1", "",
                        "T1(T3)", "", "T1(T2)", "",      "T1(T0)", "", "T1(T2)(To5k)", "",
                        "T3(T2)", "", "T1(T2)", "T1V",      "T1V", "T1V", "T1V", "",
                    });
                }
                if (InBeat(98f + 32f))
                {
                    DelayEnabled = false;
                    Give3DFunctions();
                    RegisterFunctionOnce("Remove", () =>
                    {
                        Delay(CurrentTime, () =>
                        {
                            Apply2D();
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(1), ValueEasing.EaseOutCubic(0, 110, BeatTime(1)));
                            float y = BoxStates.Centre.Y;
                            builder.Run(s =>
                            {
                                for (int i = 0; i <= 2; i++)
                                {
                                    SetPlayerBoxMission(i * 2);
                                    InstantTP(BoxStates.Centre.X, s + y);
                                    BoxStates.Centre = Heart.Centre;
                                }
                            });
                            DelayBeat(0.5f, () =>
                            {
                                ValueEasing.EaseBuilder builder2 = new();
                                builder2.Insert(BeatTime(1), ValueEasing.EaseOutCubic(0, 110, BeatTime(1)));
                                builder2.Run(s =>
                                {
                                    for (int i = 0; i <= 1; i++)
                                    {
                                        SetPlayerBoxMission(i * 2 + 1);
                                        InstantTP(BoxStates.Centre.X, s + y);
                                        BoxStates.Centre = Heart.Centre;
                                    }
                                });
                            });
                            DelayBeat(1.5f, () =>
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    SetPlayerMission(i); Heart.Dispose();
                                }
                            });
                            DelayBeat(1, () =>
                            {
                                ValueEasing.EaseBuilder builder3 = new();
                                builder3.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-450, 240, BeatTime(1.5f)));
                                builder3.Run(s =>
                                {
                                    SetPlayerBoxMission(0);
                                    InstantTP(320, s);
                                    BoxStates.Centre = Heart.Centre;
                                });
                            });
                        });
                    });
                    BarrageCreate(0, BeatTime(2), 6.5f, new string[] {
                        "", "", "", "",   "", "", "", "",
                        "T4(T2)(T0)", "T4V(T2V)", "T4V(T2V)", "",     "T3(T2)(T1)", "T3V(T1V)", "T3V(T1V)", "",
                        "T0(T2)", "", "T2(T4)", "T4V",     "T4V", "T4V", "T4V(T2)", "",
                        "T0(T2)", "", "T1(T2)", "",     "T1(T0)", "", "T1(T3)", "",
                        "T3(T4)", "T3V", "T3V", "T3V",     "T3V(T2)", "T3V", "T3V", "",
                        "T1(T3)", "", "T2", "T3",    "T0(T4)", "", "T0(T3)", "",
                        "T3(T1)", "", "T1(T2)", "",       "T2(T3)", "", "T1(T3)", "T1V(T3V)",
                        "T1V(T3V)(T2)", "T1V(T3V)", "T1V(T3V)", "T1V(T3V)",       "T1V(T3V)(T0)", "T1V(T3V)", "T1V(T3V)", "T1V(T3V)",

                        "T1V(T3V)(T4)", "T1V(T3V)", "T1V(T3V)", "T1V(T3V)",       "T1V(T3V)(T0)(T4)", "", "T1(T3)", "",
                        "T0(T1)", "", "T1(T2)", "",       "T3", "T4", "T3", "T2",
                        "T0(T1)", "", "T2(T3)", "",       "T1(T3)", "", "T3(T4)", "",
                        "T0(T2)", "", "T1(T3)", "",        "T2(T4)", "", "T1(T3)", "",
                        "T0(T2)", "", "T0(T1)", "",        "T2(T4)", "", "T3(T4)", "",
                        "T1(T3)(T0)", "T1V(T3V)", "T1V(T3V)(T4)", "",        "T0(T1)", "", "T3(T4)", "T3V",
                        "T3V", "T3V", "T3V(T2)(T1)", "",        "(T1)(T3)", "", "T1(T0)", "T1V",
                        "T1V(T4)", "T1V", "T1V(T2)(T3)", "",        "T1(T2)", "", "T2(T0)", "",
                        "T0(T4)", "", "T4(T2)(Remove)", "",        "T1(T3)", "", "", "",
                    });
                }
            }
            private void Give3DFunctions()
            {
                float camRotation = 48;

                RegisterFunctionOnce("CameraA1", () =>
                {
                    AddInstance(new InstantEvent(CurrentTime, () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1f), ValueEasing.EaseOutCubic(0, 0.012f, BeatTime(1f)));
                        builder.Insert(BeatTime(1f), ValueEasing.EaseOutCubic(0.012f, -0.012f, BeatTime(1f)));
                        builder.Insert(BeatTime(1f), ValueEasing.EaseOutCubic(-0.012f, 0.0f, BeatTime(1f)));
                        builder.Run(s =>
                        {
                            Vector3 t = Effect3D.CameraRotation;
                            t.Z = s;
                            Effect3D.CameraRotation = t;
                        });
                    }));
                });
                RegisterFunctionOnce("CameraZL", () =>
                {
                    AddInstance(new InstantEvent(CurrentTime, () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(2, ValueEasing.EaseOutCubic(0, 0.022f, 2));
                        builder.Insert(11, ValueEasing.EaseOutCubic(0.022f, 0.0f, 11));
                        builder.Run(s =>
                        {
                            Vector3 t = Effect3D.CameraRotation;
                            t.Z = s;
                            Effect3D.CameraRotation = t;
                        });
                    }));
                });
                RegisterFunctionOnce("CameraZR", () =>
                {
                    AddInstance(new InstantEvent(CurrentTime, () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(2, ValueEasing.EaseOutCubic(0, -0.022f, 2));
                        builder.Insert(11, ValueEasing.EaseOutCubic(-0.022f, 0.0f, 11));
                        builder.Run(s =>
                        {
                            Vector3 t = Effect3D.CameraRotation;
                            t.Z = s;
                            Effect3D.CameraRotation = t;
                        });
                    }));
                });
                RegisterFunctionOnce("CameraDown", () =>
                {
                    AddInstance(new InstantEvent(CurrentTime, () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(5, ValueEasing.EaseOutCubic(camRotation, camRotation - 1.2f, 5));
                        builder.Insert(5, ValueEasing.EaseOutCubic(camRotation - 1.2f, camRotation, 5));
                        builder.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, Effect3D.CameraPosition.Z);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(1), ValueEasing.EaseOutCubic(0, -4, BeatTime(1)));
                        builder2.Insert(BeatTime(1), ValueEasing.EaseOutCubic(-4, 0, BeatTime(1)));
                        builder2.Run(s =>
                        {
                            Effect3D.CameraPosition = new(0, Effect3D.CameraPosition.Y, s);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    }));
                });
                RegisterFunctionOnce("CameraUp", () =>
                {
                    AddInstance(new InstantEvent(CurrentTime, () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(5, ValueEasing.EaseOutCubic(camRotation, camRotation + 1.2f, 5));
                        builder.Insert(5, ValueEasing.EaseOutCubic(camRotation + 1.2f, camRotation, 5));
                        builder.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, Effect3D.CameraPosition.Z);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(1), ValueEasing.EaseOutCubic(0, 4, BeatTime(1)));
                        builder2.Insert(BeatTime(1), ValueEasing.EaseOutCubic(4, 0, BeatTime(1)));
                        builder2.Run(s =>
                        {
                            Effect3D.CameraPosition = new(0, Effect3D.CameraPosition.Y, s);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    }));
                });

                float speed = 31;
                float scale = 2.25f;
                float lateWaitingScale = 0.1f;
                RegisterFunctionOnce("T0V", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        SetPlayerBoxMission(0);
                        Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.125f), 1, speed, 1, 0);
                        arr.VoidMode = true;
                        arr.Scale = scale;
                        arr.JudgeType = Arrow.JudgementType.Hold;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    }
                });
                RegisterFunctionOnce("T1V", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        SetPlayerBoxMission(1);
                        Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.125f), 0, speed, 1, 0);
                        arr.VoidMode = true;
                        arr.Scale = scale;
                        arr.JudgeType = Arrow.JudgementType.Hold;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    }
                });
                RegisterFunctionOnce("T2V", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        SetPlayerBoxMission(2);
                        Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.125f), 2, speed, 0, 0);
                        arr.VoidMode = true;
                        arr.Scale = scale;
                        arr.JudgeType = Arrow.JudgementType.Hold;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    }
                });
                RegisterFunctionOnce("T3V", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        SetPlayerBoxMission(3);
                        Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.125f), 1, speed, 0, 0);
                        arr.VoidMode = true;
                        arr.Scale = scale;
                        arr.JudgeType = Arrow.JudgementType.Hold;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    }
                });
                RegisterFunctionOnce("T4V", () =>
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        SetPlayerBoxMission(4);
                        Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.125f), 0, speed, 0, 0);
                        arr.VoidMode = true;
                        arr.Scale = scale;
                        arr.JudgeType = Arrow.JudgementType.Hold;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    }
                });
                RegisterFunctionOnce("T0", () =>
                {
                    SetPlayerBoxMission(0);
                    Arrow arr = MakeArrow(CurrentTime, 1, speed, 1, 0);
                    arr.JudgeType = Arrow.JudgementType.Tap;
                    arr.Scale = scale;
                    arr.LateWaitingScale = lateWaitingScale;
                    CreateEntity(arr);
                });
                RegisterFunctionOnce("T1", () =>
                {
                    SetPlayerBoxMission(1);
                    Arrow arr = MakeArrow(CurrentTime, 0, speed, 1, 0);
                    arr.JudgeType = Arrow.JudgementType.Tap;
                    arr.Scale = scale;
                    arr.LateWaitingScale = lateWaitingScale;
                    CreateEntity(arr);
                });
                RegisterFunctionOnce("T2", () =>
                {
                    SetPlayerBoxMission(2);
                    Arrow arr = MakeArrow(CurrentTime, 2, speed, 0, 0);
                    arr.JudgeType = Arrow.JudgementType.Tap;
                    arr.Scale = scale;
                    arr.LateWaitingScale = lateWaitingScale;
                    CreateEntity(arr);
                });
                RegisterFunctionOnce("T3", () =>
                {
                    SetPlayerBoxMission(3);
                    Arrow arr = MakeArrow(CurrentTime, 1, speed, 0, 0);
                    arr.JudgeType = Arrow.JudgementType.Tap;
                    arr.Scale = scale;
                    arr.LateWaitingScale = lateWaitingScale;
                    CreateEntity(arr);
                });
                RegisterFunctionOnce("T4", () =>
                {
                    SetPlayerBoxMission(4);
                    Arrow arr = MakeArrow(CurrentTime, 0, speed, 0, 0);
                    arr.JudgeType = Arrow.JudgementType.Tap;
                    arr.Scale = scale;
                    arr.LateWaitingScale = lateWaitingScale;
                    CreateEntity(arr);
                });
            }
            private void Apply3D()
            {
                float camRotation = 48;
                DelayBeat(0, () =>
                {
                    ScreenDrawing.UpExtending = 1.75f;
                    ScreenDrawing.DownExtending = 0.25f;

                    CentreEasing.EaseBuilder screen = new();
                    screen.Insert(BeatTime(2), CentreEasing.EaseOutQuint(new(0, 0), new(0, -10), BeatTime(2)));
                    screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

                    cameraProduction = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                    ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);

                    ValueEasing.EaseBuilder camera = new();
                    Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending + ScreenDrawing.DownExtending));
                    Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y * (1 + ScreenDrawing.UpExtending) / (1 + ScreenDrawing.UpExtending + ScreenDrawing.DownExtending));
                    Effect3D.ProjectPoint = new(0, 0, 200);

                    ValueEasing.EaseBuilder posMove = new();

                    int beatCount = 2;

                    posMove.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(200, 154, BeatTime(beatCount)));
                    posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                    camera.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(0, camRotation, BeatTime(beatCount)));
                    camera.Run(rotation =>
                    {
                        Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                        Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                        Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                    });
                });
            }
            private void To6KIn3D()
            {
                float camRotation = 48;
                DelayBeat(0, () =>
                {
                    ValueEasing.EaseBuilder camera = new();

                    int beatCount = 4;

                    camera.Insert(BeatTime(beatCount), ValueEasing.EaseInSine(0, 17, BeatTime(beatCount)));
                    camera.Run(position =>
                    {
                        Effect3D.CameraPosition = new(0, -240 + Sin(camRotation) * -2 * position, -position * 2);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                    });
                });
            }
            private void Apply2D()
            {
                DelayBeat(0, () =>
                {
                    float camRotation = 48;
                    ScreenDrawing.UpExtending = 1.75f;
                    ScreenDrawing.DownExtending = 0.25f;

                    CentreEasing.EaseBuilder screen = new();
                    screen.Insert(BeatTime(2), CentreEasing.EaseOutQuint(new(0, 0), new(0, -10), BeatTime(2)));
                    screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

                    cameraProduction = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                    ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);

                    ValueEasing.EaseBuilder camera = new();
                    Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending + ScreenDrawing.DownExtending));
                    Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y * (1 + ScreenDrawing.UpExtending) / (1 + ScreenDrawing.UpExtending + ScreenDrawing.DownExtending));
                    Effect3D.ProjectPoint = new(0, 0, 200);

                    ValueEasing.EaseBuilder posMove = new();

                    int beatCount = 2;

                    posMove.Insert(BeatTime(beatCount), ValueEasing.Linear(154, 200, BeatTime(beatCount)));
                    posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                    camera.Insert(BeatTime(beatCount), ValueEasing.Linear(camRotation, 0, BeatTime(beatCount)));
                    camera.Run(rotation =>
                    {
                        Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                        Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                        Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                    });
                });
            }
        }
    }
}