using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    internal partial class Clb1e86f2 : IChampionShip
    {
        public Clb1e86f2()
        {
            Game.game = new Game();
            difficulties = new()
            {
                { "div2", Difficulty.Normal },
                { "div1", Difficulty.Extreme },
                { "div0", Difficulty.ExtremePlus }
            };
            //    this.difficulties.Add("Anomaly Test", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public partial class Game : WaveConstructor, IWaveSet
        {
            public void ExtremePlus()
            {
            }

            public void Noob()
            {
            }

            public void Easy()
            {
            }

            public void Normal()
            {
            }

            public void Hard()
            {
                Line line = new(LinkEase(
                        EaseOut(BeatTime(2), new Vector2(320, 240), new Vector2(640, 240), EaseState.Linear),
                        EaseOut(BeatTime(2), new Vector2(640, 240), new Vector2(320, 240), EaseState.Back),
                        EaseOut(BeatTime(2), new Vector2(320, 240), new Vector2(0, 240), EaseState.Circ)
                    ).Easing,
                    Stable(9999, 45).Easing);
            }

            private static class EXBarrage
            {
                static float BeatTime(float x) => game.BeatTime(x);
                static void BarrageCreate(float x, float y, float z, string[] w) => game.BarrageCreate(x, y, z, w);
                static void DelayBeat(float x, Action y) => game.DelayBeat(x, y);
                static void RegFunc1(string n, Action a) => game.RegisterFunctionOnce(n, a);

                static Arrow.UnitEasing easeA = null, easeB = null, easeC = null;
                static Arrow.EnsembleEasing easeX = null, easeY = null, easeZ = null;

                static RGBSplitting rgbSplit;
                static GlobalResources.Effects.ScatterShader scatter;
                static RenderProduction scatterP;

                static Lighting lighting;

                public static void Intro0()
                {
                    rgbSplit = new();
                    rgbSplit.Disturbance = false;

                    ScreenDrawing.SceneRendering.InsertProduction(rgbSplit);
                    ScreenDrawing.SceneRendering.InsertProduction(scatterP = new Filter(scatter = Shaders.Scatter, 0.6f));
                    scatter.Ratio = 0.04f;
                    rgbSplit.Intensity = 0.5f;
                    scatter.Intensity = 1.5f;

                    AddInstance(easeA = new Arrow.UnitEasing()
                    {
                        ApplyTime = BeatTime(2.25f),
                        RotationEase = SimplifiedEasing.Copy(SimplifiedEasing.LinkEase(
                            SimplifiedEasing.EaseOut(BeatTime(0.25f), 15, SimplifiedEasing.EaseState.Sine),
                            SimplifiedEasing.EaseIn(BeatTime(0.25f), -15, SimplifiedEasing.EaseState.Sine),
                            SimplifiedEasing.EaseOut(BeatTime(0.25f), -15, SimplifiedEasing.EaseState.Sine),
                            SimplifiedEasing.EaseIn(BeatTime(0.25f), 15, SimplifiedEasing.EaseState.Sine)
                        ), 2)
                    });
                    for (int i = 0; i < 4; i++)
                        game.DelayBeat(2 + i * 8, () =>
                        {
                            SimplifiedEasing.RunEase((s) => easeA.Intensity = s,
                                    SimplifiedEasing.EaseIn(BeatTime(5f), 1.0f, SimplifiedEasing.EaseState.Sine),
                                    SimplifiedEasing.EaseIn(BeatTime(3f), -0.9f, SimplifiedEasing.EaseState.Sine)
                                );
                        });
                    game.DelayBeat(0, () =>
                    {
                        easeA.TagApply("A");
                    });
                    game.DelayBeat(8, () =>
                    {
                        easeA.TagApply("B");
                    });
                    game.DelayBeat(16, () =>
                    {
                        easeA.TagApply("A");
                    });
                    game.DelayBeat(24, () =>
                    {
                        easeA.TagApply("B");
                    });
                    game.RegisterFunctionOnce("Eff0", () =>
                    {
                        SimplifiedEasing.RunEase((s) =>
                        {
                            scatter.Ratio = MathHelper.Lerp(0.04f, 0.55f, s);
                            scatter.Intensity = MathHelper.Lerp(1.5f, 10f, s);
                            rgbSplit.Intensity = MathHelper.Lerp(0.5f, 5f, s);
                            ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = MathHelper.LerpPrecise(54f, 190f, s);
                            ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.Magenta, s * 0.3f + 0.7f) * 0.4f;
                        },
                                SimplifiedEasing.EaseIn(BeatTime(4f), 1.0f, SimplifiedEasing.EaseState.Sine),
                                SimplifiedEasing.EaseIn(BeatTime(4f), -1.0f, SimplifiedEasing.EaseState.Sine)
                            );


                        //shake:
                        SimplifiedEasing.RunEase((s) =>
                        {
                            BoxStates.Centre = new Vector2(320 + s, 240);
                            Heart.Centre = BoxStates.Centre;
                        },
                            SimplifiedEasing.Scale(
                                SimplifiedEasing.LinkEase(
                                    SimplifiedEasing.EaseOut(BeatTime(4f), 2, SimplifiedEasing.EaseState.Cubic),
                                    SimplifiedEasing.EaseIn(BeatTime(3f), -2, SimplifiedEasing.EaseState.Cubic))
                                ,
                                SimplifiedEasing.Alternate(2f, SimplifiedEasing.Stable(1f, 1f), SimplifiedEasing.Stable(1, -1)))

                            );
                    });
                    BarrageCreate(BeatTime(2), BeatTime(2), 6.5f, new string[] {
                        "", "", "", "",       "", "", "", "",

                        "*^$0'1.6(*^$2'1.6)(#6#$31)(Eff0)", "", "~_!$31@A", "",       "~_!$31@A", "", "~_!$31@A", "",
                        "~_!$31@A", "", "~_!$31@A", "",       "~_!$31@A", "", "~_!$31@A", "",
                        "~_!$31@A", "", "~_!$31@A", "",       "~_!$31@A", "", "~_!$31@A", "",
                        "", "", "", "",       "", "", "", "",

                        "(#6#$11)(Eff0)", "", "~_!$11@B", "",       "~_!$11@B", "", "~_!$11@B", "",
                        "~_!$11@B", "", "~_!$11@B", "",       "~_!$11@B", "", "~_!$11@B", "",
                        "~_!$11@B", "", "~_!$11@B", "",       "~_!$11@B", "", "~_!$11@B", "",
                        "", "", "", "",       "", "", "", "",

                        "(#6#$31)(Eff0)", "", "~_!$31@A", "",       "~_!$31@A", "", "~_!$31@A", "",
                        "~_!$31@A", "", "~_!$31@A", "",       "~_!$31@A", "", "~_!$31@A", "",
                        "~_!$31@A", "", "~_!$31@A", "",       "~_!$31@A", "", "~_!$31@A", "",
                        "", "", "", "",       "", "", "", "",

                        "(#5#$11)(Eff0)", "", "~_!$11@B", "",       "~_!$11@B", "", "~_!$11@B", "",
                        "~_!$11@B", "", "~_!$11@B", "",       "~_!$11@B", "", "~_!$11@B", "",
                        "~_!$11@B($3)", "", "~_!$11@B", "($3)",       "", "", "($3)", "",
                        "$0", "$1", "$2", "$3",       "<$301", ">$301", "<$301", ">$301",
                    });
                }
                public static void Intro1()
                {
                    game.RegisterFunctionOnce("Eff0", () =>
                    {
                        SimplifiedEasing.RunEase((s) =>
                        {
                            scatter.Ratio = MathHelper.Lerp(0.04f, 0.55f, s);
                            scatter.Intensity = MathHelper.Lerp(1.5f, 10f, s);
                            rgbSplit.Intensity = MathHelper.Lerp(0.5f, 5f, s);

                            ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = MathHelper.LerpPrecise(54f, 190f, s);
                            ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.Magenta, s * 0.3f + 0.7f) * 0.4f;
                        },
                                SimplifiedEasing.EaseIn(BeatTime(4f), 1.0f, SimplifiedEasing.EaseState.Sine),
                                SimplifiedEasing.EaseIn(BeatTime(3.9f), -1.0f, SimplifiedEasing.EaseState.Sine),
                                SimplifiedEasing.Stable(0)
                            );

                        //shake:
                        SimplifiedEasing.RunEase((s) =>
                        {
                            BoxStates.Centre = new Vector2(320 + s, 240);
                        },
                            SimplifiedEasing.Scale(
                                SimplifiedEasing.LinkEase(
                                    SimplifiedEasing.EaseOut(BeatTime(4f), 2, SimplifiedEasing.EaseState.Cubic),
                                    SimplifiedEasing.EaseIn(BeatTime(3f), -2, SimplifiedEasing.EaseState.Cubic))
                                ,
                                SimplifiedEasing.Alternate(2f, SimplifiedEasing.Stable(1f, 1f), SimplifiedEasing.Stable(1, -1)))

                            );
                    });
                    game.RegisterFunctionOnce("End", () =>
                    {
                        SimplifiedEasing.RunEase((s) =>
                        {
                            scatter.Ratio = MathHelper.Lerp(0.14f, 0, s);
                            scatter.Intensity = MathHelper.Lerp(3.5f, 0, s);
                            rgbSplit.Intensity = MathHelper.Lerp(1.5f, 0, s);
                        }, SimplifiedEasing.EaseOut(BeatTime(2), 1, SimplifiedEasing.EaseState.Linear), SimplifiedEasing.Stable(1));
                        game.DelayBeat(0.5f, () => { scatterP.Dispose(); rgbSplit.Dispose(); });
                    });
                    game.RegisterFunctionOnce("RiseStart", () => {
                        RunEase((s) => {
                            ScreenDrawing.BackGroundColor = Color.BlueViolet * s;
                        }, EaseOut(BeatTime(5), 0.16f, 0.26f, EaseState.Linear), EaseOut(BeatTime(3), 0.26f, 0.10f, EaseState.Linear));
                    });
                    game.RegisterFunctionOnce("LightStart", () =>
                    {
                        lighting = new(0.74f);
                        ScreenDrawing.SceneRendering.InsertProduction(lighting);

                        lighting.LightingMode = Lighting.LightMode.Additive;
                        SimplifiedEasing.RunEase((s) =>
                        {
                            lighting.AmbientColor = Color.Lerp(Color.Transparent,
                            Color.Lerp(Color.Magenta * 0.72f, Color.White * 0.72f, 0.5f), s);
                        },
                            SimplifiedEasing.EaseIn(BeatTime(2.5f), 0.7f, SimplifiedEasing.EaseState.Quad),
                            SimplifiedEasing.EaseOut(BeatTime(1.1f), 0.7f, 1.0f, SimplifiedEasing.EaseState.Quad),
                            SimplifiedEasing.EaseOut(BeatTime(1.2f), 1.0f, 0.0f, SimplifiedEasing.EaseState.Linear));

                        Lighting.Light lightA = new() { position = new(320, 240), color = Color.Transparent, size = 80 };
                        lighting.Lights.Add(lightA);

                        lightA.scale = new(1.15f, 1.0f);
                        SimplifiedEasing.RunEase((s) =>
                        {
                            lightA.size = MathHelper.Lerp(100, 670, s);
                            lightA.color = Color.Lerp(Color.Transparent, Color.White, s);
                        },
                            SimplifiedEasing.EaseIn(BeatTime(2.5f), 0.7f, SimplifiedEasing.EaseState.Quad),
                            SimplifiedEasing.EaseOut(BeatTime(1.1f), 0.7f, 1.0f, SimplifiedEasing.EaseState.Quad),
                            SimplifiedEasing.EaseOut(BeatTime(0.85f), 1.0f, 0.0f, SimplifiedEasing.EaseState.Linear));


                        /*
                        lighting.AmbientColor = Color.White;
                        lighting.Lights.Add(new() { position = new(320, 240), color = Color.White, size = 500 });*/
                        /*
                           lighting.Lights.Add(new() { position = new(60, 40), color = Color.White, size = 400});
                           lighting.Lights.Add(new() { position = new(580, 40), color = Color.White, size = 400 });
                           lighting.Lights.Add(new() { position = new(60, 440), color = Color.White, size = 400 });
                           lighting.Lights.Add(new() { position = new(580, 440), color = Color.White, size = 400 });*/
                        /*
                        SimplifiedEasing.RunEase((s) => { lighting.AmbientColor = Color.Lerp(Color.White, 
                            Color.Lerp(Color.Magenta * 0.52f, Color.White * 0.52f, 0.5f), s); },
                            SimplifiedEasing.EaseIn(BeatTime(0.6f), 0.7f, SimplifiedEasing.EaseState.Quad),
                            SimplifiedEasing.EaseOut(BeatTime(0.4f), 0.7f, 1.0f, SimplifiedEasing.EaseState.Quad)
                            
                            ); 
                        */
                    });
                    BarrageCreate(BeatTime(2), BeatTime(2), 6.5f, new string[] {
                        "", "", "", "",       "", "", "", "",

                        "($00)(+01)(Eff0)", "", "", "",       "(d0)(+01)", "", "(d0)(+01)", "",
                        "", "", "(d0)(+01)", "",       "(d0)(+01)", "", "", "",
                        "(d0)(+01)", "", "(d0)(+01)", "",       "", "", "(d0)(+01)", "",
                        "(d0)(+01)", "", "(d0)(+01)", "",       "(d0)(+01)", "", "(d0)(+01)", "",

                        "(d0)(+01)(Eff0)", "", "", "",       "(d0)(+01)", "", "(d0)(+01)", "",
                        "", "", "(d0)(+01)", "",       "(d0)(+01)", "", "(d0)(+01)", "",
                        "(d0)(+01)", "", "", "(d0)(+01)",       "", "", "(d0)(+01)", "",
                        "(d0)(+01)", "", "", "",       "(d0)(+01)", "", "", "",

                        "(d0)(+01)(Eff0)", "", "", "",       "(d0)(+01)", "", "(d0)(+01)", "",
                        "", "", "(d0)(+01)", "",       "(d0)(+01)", "", "", "",
                        "(d0)(+01)", "", "(d0)(+01)", "",       "", "", "(d0)(+01)", "",
                        "(d0)(+01)", "", "(d0)(+01)", "",       "(d0)(+01)", "", "(d0)(+01)", "",

                        "(d0)(+01)", "", "", "",       "(d0)(+01)(RiseStart)", "", "(d0)(+01)", "",
                        "", "", "(d0)(+01)", "",       "(d0)(+01)", "", "(d0)(+01)", "",
                        "(d0)(+01)", "", "", "(n3)(+01)(LightStart)",       "", "", "($3)(+01)", "",
                        "$0", "$1", "$2", "$3",       ">$301(End)", "<$301", ">$301", "<$301",
                    });
                }
                public static void Area01()
                {
                    AddInstance(easeX = new Arrow.EnsembleEasing() { });
                    easeX.TagApply("X");

                    easeA?.Dispose();

                    AddInstance(easeA = new Arrow.UnitEasing()
                    {
                        ApplyTime = BeatTime(3.0f),
                        RotationEase = SimplifiedEasing.Copy(SimplifiedEasing.LinkEase(
                            SimplifiedEasing.EaseOut(BeatTime(0.25f), 7, SimplifiedEasing.EaseState.Circ),
                            SimplifiedEasing.EaseIn(BeatTime(0.25f), -7, SimplifiedEasing.EaseState.Circ),
                            SimplifiedEasing.EaseOut(BeatTime(0.25f), -7, SimplifiedEasing.EaseState.Circ),
                            SimplifiedEasing.EaseIn(BeatTime(0.15f), 7, SimplifiedEasing.EaseState.Circ),
                            SimplifiedEasing.Stable(BeatTime(0.1f))
                        ), 2)
                    });
                    easeA.TagApply("A");
                    easeA.Intensity = 0.7f;

                    AddInstance(easeB = new Arrow.UnitEasing()
                    {
                        ApplyTime = BeatTime(3.0f),
                        RotationEase =
                        SimplifiedEasing.LinkEase(false,
                            SimplifiedEasing.EaseOut(BeatTime(0.35f), 45, 45, SimplifiedEasing.EaseState.Linear),
                            SimplifiedEasing.EaseOut(BeatTime(0.06f), 45, 46, SimplifiedEasing.EaseState.Circ),
                            SimplifiedEasing.EaseOut(BeatTime(0.06f), 46, 44, SimplifiedEasing.EaseState.Circ),
                            SimplifiedEasing.EaseOut(BeatTime(0.06f), 44, 45, SimplifiedEasing.EaseState.Circ),
                            SimplifiedEasing.EaseOut(BeatTime(1f), 45, 45, SimplifiedEasing.EaseState.Linear),
                            SimplifiedEasing.EaseOut(BeatTime(1f), 45, 0, SimplifiedEasing.EaseState.Circ)
                        )
                    });
                    easeB.TagApply("B");

                    void XEase(float pos)
                    {
                        easeX.RevolutionEase(
                                SimplifiedEasing.EaseOut(2.5f, pos, -pos, SimplifiedEasing.EaseState.Circ),
                                SimplifiedEasing.EaseOut(2.2f, -pos, 0, SimplifiedEasing.EaseState.Circ)
                            );
                    }
                    void YEase(float pos)
                    {
                        easeX.SelfRotationEase(
                            SimplifiedEasing.EaseOut(3f, pos, -pos, SimplifiedEasing.EaseState.Circ),
                            SimplifiedEasing.EaseOut(2.5f, -pos, 0, SimplifiedEasing.EaseState.Circ)
                            );
                    }
                    game.RegisterFunctionOnce("XShakeA", () =>
                    {
                        XEase(1.7f);
                    });
                    game.RegisterFunctionOnce("XShakeB", () =>
                    {
                        XEase(-1.7f);
                    });
                    game.RegisterFunctionOnce("YShakeA", () =>
                    {
                        YEase(46);
                    });
                    game.RegisterFunctionOnce("YShakeB", () =>
                    {
                        YEase(-46);
                    });
                    game.RegisterFunctionOnce("YShakeC", () =>
                    {
                        YEase(17);
                    });
                    game.RegisterFunctionOnce("YShakeD", () =>
                    {
                        YEase(-17);
                    });
                    Lighting.Light B = null;
                    game.RegisterFunctionOnce("Beat", () => {
                        RunEase(s =>
                        {
                            ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = s;
                        },
                            EaseOut(BeatTime(0.12f), 54, 68, EaseState.Quint),
                            EaseOut(BeatTime(1.88f), 68, 54, EaseState.Cubic));
                        ScreenDrawing.MakeFlicker(Color.BlueViolet * 0.33f);
                        RunEase(s => {
                            ScreenDrawing.ScreenScale = s;
                            B.color = Color.BlueViolet * (s - 0.8f) * 3.5f;
                            B.size = MathHelper.Lerp(560f, 340f, (s - 1) * 15);
                        }, EaseOut(BeatTime(1.1f), 1.05f, 1.0f, EaseState.Cubic)); 
                    }); 
                    game.RegisterFunctionOnce("Pre", () => {
                        B = new() { size = 340f, color = Color.BlueViolet * 0.63f, position = new(320, 240) };
                        RunEase(s => { B.color = Color.BlueViolet * s; },
                            EaseOut(BeatTime(2.4f), 0f, 0.63f, EaseState.Quad));
                        lighting.Lights.Add(B);
                    });
                    game.RegisterFunctionOnce("Rotate", () => {
                        RunEase(s => { ScreenDrawing.ScreenAngle = s; },
                            EaseOut(BeatTime(0.81f), 0f, 2f, EaseState.Cubic),
                            EaseIn(BeatTime(0.55f), 2f, 0f, EaseState.Cubic),
                            EaseOut(BeatTime(0.65f), 0f, -2f, EaseState.Cubic),
                            EaseOut(BeatTime(1.1f), -2f, 0f, EaseState.Cubic) 
                            
                            );
                        RunEase(s => { ScreenDrawing.MasterAlpha = s; }, false,
                            Copy(EaseIn(BeatTime(0.25f), 0.37f, 1.0f, EaseState.Linear), 8), Stable(1, 1));
                    });
                    game.RegisterFunctionOnce("RL", () => {
                        RunEase(s => { ScreenDrawing.ScreenAngle = s; }, EaseOut(BeatTime(0.5f), 3f, 0f, EaseState.Cubic));
                    });
                    game.RegisterFunctionOnce("RR", () => {
                        RunEase(s => { ScreenDrawing.ScreenAngle = s; }, EaseOut(BeatTime(0.5f), -3f, 0f, EaseState.Cubic));
                    });
                    game.RegisterFunction("WAVE", () => {
                        Shaders.Seismic.Radius = 400;
                        var filter = ScreenDrawing.ActivateShader(Shaders.Seismic, 0.6f);
                        RunEase(s => { Shaders.Seismic.Progress = s; }, EaseOut(BeatTime(1.5f), 1.0f, EaseState.Linear));
                        DelayBeat(1.5f, filter.Dispose);
                    });
                    BarrageCreate(BeatTime(2), BeatTime(2), 6.5f, new string[] {
                        "", "", "", "",       "", "", "", "Pre",

                        "(+0@X)", "", "", "",       "(+0)(+01'0.8@X,B)(XShakeA)", "", "(d0@X)", "",
                        "", "", "(d0@X)", "",       "(d0)(+01'0.8@Y,B)(YShakeA)(Beat)", "", "", "",
                        "(d0@X)", "", "(d0@X)", "",       "(+01'0.8@X,B)(XShakeB)(Beat)", "", "(d0@X)", "",
                        "(d0@X)", "", "(d0@X)", "",       "(d0)(+01'0.8@Y,B)(YShakeB)(Beat)", "", "(d0@X)", "",

                        "(d0@X)", "", "", "",       "(d0)(+01'0.8@X,B)(XShakeA)(Beat)", "", "(d0@X)", "",
                        "", "", "(d0@X)", "",       "(d0)(+01'0.8@Y,B)(YShakeA)(Beat)", "", "(d0@X)", "",
                        "(d0@X)", "", "", "(d0@X)",       "(+01'0.8@X,B)(XShakeB)(Beat)", "", "(d0@X)", "(YShakeB)",
                        "*^$0'1.6(*^$2'1.6)(RL)", "", "", "(YShakeB)",       "*^$01'1.6(*^$21'1.6)(Beat)(RR)", "", "", "",

                        "(d0@X)", "", "", "",       "(d0)(+01'0.8@X,B)(XShakeA)(Beat)", "", "(d0@X)", "",
                        "", "", "(d0@X)", "",       "(d0)(+01'0.8@Y,B)(YShakeA)(Beat)", "", "", "",
                        "(d0@X)", "", "(d0@X)", "",       "(+01'0.8@X,B)(XShakeB)(Beat)", "", "(d0@X)", "",
                        "(d0@X)", "", "(d0@X)", "",       "(d0)(+01'0.8@Y,B)(YShakeB)(Beat)", "", "(d0@X)", "",

                        "(d0@X)", "", "", "",       "(d0)(+01'0.8@X,B)(XShakeA)(Beat)", "", "(d0@X)", "",
                        "", "", "(d0@X)", "",       "(d0)(+01'0.8@Y,B)(YShakeA)(Beat)", "", "(d0@X)", "",
                        "(d0@X)", "", "", "(d0@X)",       "(+01'0.8@X,B)(XShakeB)(Beat)", "", "(n00@X)", "(YShakeD)",
                        "$0@X,A(Rotate)", "(YShakeC)$2@X,A", "$0@X,A", "(YShakeD)$2@X,A",       "$0@X,A(WAVE)", "(YShakeC)$2@X,A", "$0@X,A", "$2@X,A",
                    });
                }
                public static void Area02()
                {
                    game.RegisterFunctionOnce("ColorChange", () => {
                        ScreenDrawing.BoundColor = Color.Aqua;
                    });
                    BarrageCreate(BeatTime(2), BeatTime(2), 6.5f, new string[] {
                        "", "", "", "",       "", "", "", "(ColorChange)",

                        "R1", "", "", "",       "", "", "", "",
                    });
                }
            }
            public void Extreme()
            {
                if (InBeat(4f)) EXBarrage.Intro0();
                if (InBeat(4f + 32f)) EXBarrage.Intro1();
                if (InBeat(4f + 64f)) EXBarrage.Area01();
                if (InBeat(4f + 96f)) EXBarrage.Area02();
            }

            public override void Start()
            {
                InstantSetGreenBox();
                SetSoul(1);
                InstantTP(320, 240);

                base.Start();

                Filter filter;
                ScreenDrawing.SceneRendering.InsertProduction(filter = new Filter(Shaders.Blur, 0.99f));
                Shaders.Blur.Factor = new(3, 0);
                Shaders.Blur.Sigma = 3.0f;  

                RunEase((s) => {
                    ScreenDrawing.BackGroundColor = Color.BlueViolet * s;
                }, EaseOut(BeatTime(6), 0.16f, EaseState.Linear));
                RunEase((s) =>
                {
                    Shaders.Blur.Sigma = s;
                }, false, EaseOut(BeatTime(9), 3.0f, 0.0f, EaseState.Quad), Stable(2, 0));

                ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = 54f;
                ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.Magenta, 0.3f + 0.7f) * 0.4f;

                bool delayEnable = false ;
                if (delayEnable)
                {
                    float delay = BeatTime(32 - 1);
                    //  delay = BeatTime(10 + 366);
                    PlayOffset = delay + 3.1f;
                    GametimeDelta += delay;
                    InstantSetBox(new Vector2(320, 240), 84, 84);
                    InstantTP(new Vector2(320, 240));
                }
                else
                {
                    PlayOffset = 0;
                }
            }

            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 14.0f),
                            new(Difficulty.Extreme, 17.2f),
                            new(Difficulty.ExtremePlus, 18.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 14.0f),
                            new(Difficulty.Extreme, 17.2f),
                            new(Difficulty.ExtremePlus, 18.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 16.5f),
                            new(Difficulty.Extreme, 20.7f),
                            new(Difficulty.ExtremePlus, 21.9f),
                        }
                    );
                public override string BarrageAuthor => "T-mas";
                public override string AttributeAuthor => "T-mas & Walar";
                public override string PaintAuthor => "?";
                public override string SongAuthor => "?";
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(62.5f / (200f / 60) / 1) { }

            public static Game game;

            public string Music => "#1e86f2";
            //  public string Music => "Brain Power";
            public string FightName => "The Endless Journey";
        }
    }
}