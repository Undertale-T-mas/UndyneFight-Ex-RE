using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    internal partial class Transcendence : IChampionShip
    {
        public IWaveSet GameContent => new Game();
        public partial class Game : WaveConstructor, IWaveSet
        {
            public void Noob()
            {
                if (InBeat(64 - 8))
                {
                    RegisterFunctionOnce("Move", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;
                        heart1.InstantSplit(new(new(460, -80), new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.GreenSoulAlpha = 0.3f;

                        Vector2 delta = new(140, -320), centre = new(320, 240);

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutElastic(centre - delta, centre, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            SetPlayerBoxMission(heart1);
                            BoxStates.CurrentBox.GreenSoulAlpha = 0.5f;
                            InstantTP(BoxStates.Centre);
                            InstantSetBox(s, 84, 84);
                        });
                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(8), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(8)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantTP(BoxStates.Centre);
                            InstantSetBox(s, 84, 84);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        DelayBeat(5, () =>
                        {
                            builder3.Insert(BeatTime(3), ValueEasing.EaseOutCirc(0.3f, 0, BeatTime(4)));
                            builder3.Run((s) => box2.GreenSoulAlpha = s);
                        });
                        DelayBeat(8.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("LineUp2", () =>
                    {
                        Line line1 = new(new(90, 0), BoxStates.CurrentBox.CollidingBox.TopLeft);
                        Line line2 = new(new(550, 0), BoxStates.CurrentBox.CollidingBox.TopRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.5f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineDown2", () =>
                    {
                        Line line1 = new(new(90, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft);
                        Line line2 = new(new(550, 480), BoxStates.CurrentBox.CollidingBox.BottomRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.5f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineUp", () =>
                    {
                        Line line1 = new(new(120, 0), BoxStates.CurrentBox.CollidingBox.TopLeft);
                        Line line2 = new(new(520, 0), BoxStates.CurrentBox.CollidingBox.TopRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.7f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineDown", () =>
                    {
                        Line line1 = new(new(120, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft);
                        Line line2 = new(new(520, 480), BoxStates.CurrentBox.CollidingBox.BottomRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.7f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("Cross", () =>
                    {
                        CentreEasing.EaseBuilder centre = new();
                        ValueEasing.EaseBuilder rotation = new();
                        centre.Insert(BeatTime(8), CentreEasing.EaseOutCirc(new(320, 240), new(320, 150), BeatTime(8)));
                        rotation.Insert(BeatTime(8), ValueEasing.Linear(0, 45, BeatTime(8)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutQuint(new(320, 150), new(240, 150), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, -180, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutQuint(new(240, 150), new(400, 150), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 180, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutExpo(new(400, 150), new(400, 330), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 90, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutExpo(new(400, 330), new(240, 330), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 90, BeatTime(32)));

                        centre.Insert(BeatTime(4), CentreEasing.EaseOutCirc(new(240, 330), new(320, 240), BeatTime(4)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(45, -45, BeatTime(8)));

                        centre.Insert(BeatTime(96 - 4), CentreEasing.Stable(320, 240));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -5, BeatTime(2)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseInQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseOutQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(7.5f), ValueEasing.Stable(0));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(45, 90, BeatTime(6)));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(90, 135, BeatTime(6)));

                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(45, -45, BeatTime(8)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -5, BeatTime(2)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseInQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseOutQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(7.5f), ValueEasing.Stable(0));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(45, 90, BeatTime(6)));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(90, 135, BeatTime(6)));

                        rotation.Insert(BeatTime(5.85f), ValueEasing.EaseOutBack(45, -45, BeatTime(6)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -4, BeatTime(2)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, 8, BeatTime(2)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -4, BeatTime(1.85f)));

                        rotation.Insert(BeatTime(4), ValueEasing.EaseOutExpo(0, 60, BeatTime(4)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(0, 60, BeatTime(8)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(0, 60, BeatTime(8)));
                        rotation.Insert(BeatTime(0.15f), ValueEasing.Stable(0));

                        centre.Insert(BeatTime(16), CentreEasing.EaseOutQuad(new(320, 240), new(320, 80), BeatTime(16)));
                        rotation.Insert(BeatTime(16), ValueEasing.EaseOutBack(-45, 225, BeatTime(16)));
                        centre.Insert(BeatTime(16), CentreEasing.EaseOutQuad(new(320, 240), new(320, 80), BeatTime(16)));
                        rotation.Insert(BeatTime(16), ValueEasing.EaseOutBack(225, -45, BeatTime(16)));

                        Line line1 = new(centre.GetResult(), rotation.GetResult());
                        Line line2 = new(centre.GetResult(), ValueEasing.Combine(rotation.GetResult(), 90));
                        Line[] lines = { line1, line2 };

                        DelayBeat(256 + 8 + 16, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });
                        DelayBeat(240 + 8, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(14), 0.419f);
                            }
                        });
                        DelayBeat(240 + 8 + 14, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(6));
                            }
                        });

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutCubic(0, 0.42f, BeatTime(16)));

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.Alpha = 0.00f;
                            builder.Run(s => line.Alpha = s);

                            for (int i = 0; i < 6; i++)
                            {
                                int t = i;
                                DelayBeat(i * 16 + 8, () =>
                                {
                                    line.InsertRetention(new(t * 3 + 3, 0.32f - t * 0.03f));
                                });
                            }
                        }
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(0f, -4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "Move", "", "", "", "", "", "", "(Blur)",

                    //cur
                    "(R)(LineUp)(RotateL)", "", "", "", "(+1)(LineDown)", "", "", "",
                    "(+1)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "(Blur)",
                    "(+1)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "",
                    "(+1)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "(Blur)",
                    "(+1)(LineUp)(RotateR)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                    "(+1)(LineUp)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "(Blur)",
                    "(+1)(LineUp)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                    "(+1)(LineUp)(Cross)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(128 - 16))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.06f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(12.1f), ValueEasing.EaseOutQuad(0.06f, 0.02f, BeatTime(12.1f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("Blur2", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(12.1f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(12.1f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, -4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 0f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("Up", () =>
                    {
                        ScreenDrawing.BoundColor = Color.Gray;
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutElastic(0, 140f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(4f), ValueEasing.EaseOutSine(0, 1f, BeatTime(4f)));
                        DelayBeat(4, () =>
                        {
                            builder2.Run((s) =>
                            {
                                ScreenDrawing.BoundColor = Color.Lerp(Color.Gray, Color.Silver, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("MovePosition", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(16, 16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(16, 16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(-16, 16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(-16, 16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(-16, -16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(-16, -16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(16, -16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(16, -16), new(0, 0), BeatTime(16)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        });
                    });
                    RegisterFunctionOnce("Black", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black, BeatTime(3));
                        DelayBeat(0.9f, () =>
                        {
                            rainer.Speed = 5f;
                            rainer.Intensity = 0.5f;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "(Blur)",

                    //cur
                    "D(RotateL)(MovePosition)", "", "+1", "", "+1", "", "+1", "",
                    "+1", "", "+1", "", "+1", "", "+1", "(Blur2)",
                    "(+1)", "", "+1", "", "+1", "", "+1", "",
                    "(+1)", "", "+1", "", "+1", "", "+1", "(Blur)",
                    "+1(RotateR)", "", "+1", "", "+1", "", "+1", "",
                    "+1", "", "+1", "", "+1", "", "+1", "(Blur2)",
                 //   "D1", "", "D1", "", "D1", "(Up)", "D1", "",
                  //  "(RotateMid)(Black)", "", "", "",  
                    "(D)", "", "+1", "", "+1", "", "+1", "",
                    "(D)", "", "+1", "", "+1", "", "+1", "(Blur)",
                    "D(RotateL)", "", "+1", "", "+1", "", "+1", "",
                    "D", "", "+1", "", "+1", "", "+1", "(Blur2)",
                    "(D)", "", "+1", "", "+1", "", "+1", "",
                    "(D)", "", "+1", "", "+1", "", "+1", "(Blur)",
                    "D(RotateR)", "", "+1", "", "+1", "", "+1", "",
                    "D", "", "+1", "", "+1", "", "+1", "(Blur2)",
                    "D", "", "D", "", "D", "(Up)", "D", "",
                    "(RotateMid)(Black)", "", "", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(256 - 5))
                {
                    RegisterFunctionOnce("BlurLong", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.26f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(14), ValueEasing.EaseOutQuad(0.26f, 0.02f, BeatTime(14)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.09f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.09f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), true);
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(15, BeatTime(16), false);
                        ScreenDrawing.CameraEffect.SizeShrink(7, BeatTime(16));
                    });
                    RegisterFunctionOnce("ConvulseR2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(15, BeatTime(16), true);
                        ScreenDrawing.CameraEffect.SizeShrink(7, BeatTime(16));
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "(BlurLong)",

                    //cur 
                    "(#16#$00)(#16#$21)(ConvulseL)(ConvulseL2)", ">^$00", "<^$21", "<^$00", ">^$21", ">^$00", "<^$21", "<^$00",
                    ">^$21", ">^$00", "<^$21", "<^$00", ">^$21", ">^$00", "<^$21", "<^$00",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#16#$01)(#16#$20)(ConvulseR)(ConvulseR2)", ">^$01", "<^$2", "<^$01", ">^$2", ">^$01", "<^$2", "<^$01",
                    ">^$2", ">^$01", "<^$2", "<^$01", ">^$2", ">^$01", "<^$2", "<^$01",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#10#$00)(#10#$21)(ConvulseL)(ConvulseL2)", ">^$00", "<^$21", "<^$00", ">^$21", ">^$00", "<^$21", "<^$00",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "Blur",
                    "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "", "", "Blur",
                    "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "", "", "", "", "(BlurLong)",
                    "(#13#$31)(#13#$10)(ConvulseR)(ConvulseR2)", ">^$31", "<^$10", "<^$31", ">^$10", ">^$31", "<^$10", "<^$31",
                    ">^$10", ">^$31", "<^$10", "<^$31", ">^$10", ">^$31", "", "(BlurLong)",
                    "(#13#$11)(#13#$30)(ConvulseL)(ConvulseL2)", ">^$11", "<^$30", "<^$11", ">^$30", ">^$11", "<^$30", "<^$11",
                    ">^$30", ">^$11", "<^$30", "<^$11", ">^$30", ">^$11", "", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(384 - 8))
                {
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });

                    RegisterFunctionOnce("Down", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(32), ValueEasing.EaseInElastic(140f, 110f, BeatTime(32)));
                        builder.Insert(BeatTime(32), ValueEasing.EaseOutElastic(110f, 80f, BeatTime(32)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                    });

                    float delta = 0;
                    RegisterFunctionOnce("Up", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        ValueEasing.EaseBuilder builder2 = new();
                        float v = ScreenDrawing.UpBoundDistance;
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCubic(0 + v, 36f + v, BeatTime(2)));
                        builder.Insert(BeatTime(1.6f), ValueEasing.EaseInCubic(36f, 0.0f, BeatTime(1.6f)));

                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                        builder2.Insert(BeatTime(1), ValueEasing.EaseOutCirc(0 + delta, 0.04f + delta, BeatTime(1)));
                        builder2.Insert(BeatTime(2.6f), ValueEasing.EaseInCirc(0.04f, 0.011f, BeatTime(2.6f)));
                        delta += 0.011f;

                        builder2.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.Silver, Color.Red, s);
                        });
                    });

                    RegisterFunctionOnce("LineLRev", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(640, 240), new(320, 240), BeatTime(4)));
                        builder.Insert(BeatTime(2.5f), CentreEasing.EaseOutQuad(new(320, 240), new(640, 240), BeatTime(2.5f)));
                        Line line = new(builder.GetResult(), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3 + 2.5f, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5 + 2.5f, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineRRev", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(0, 240), new(320, 240), BeatTime(4)));
                        builder.Insert(BeatTime(2.5f), CentreEasing.EaseOutQuad(new(320, 240), new(0, 240), BeatTime(2.5f)));
                        Line line = new(builder.GetResult(), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3 + 2.5f, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5 + 2.5f, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.06f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.06f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 30 + 0.35f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(2), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(2), true);
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "Blur", 
                    //cur
                    "ConvulseL(R)(LineL)(Down)", "", "R", "", "ConvulseL(R)(LineL)", "", "R", "Blur",
                    "ConvulseL(R)(LineL)", "", "R", "", "ConvulseL(R)(LineL)", "", "R", "Blur",
                    "ConvulseL(R)(LineL)", "", "R", "", "ConvulseL(R)(LineL)", "", "R", "Blur",
                    "ConvulseL(R)(LineL)", "", "R", "", "ConvulseL(R)(LineLRev)", "", "R", "Blur",
                    "ConvulseR(R)(LineR)", "", "R", "", "ConvulseR(R)(LineR)", "", "R", "Blur",
                    "ConvulseR(R)(LineR)", "", "R", "", "ConvulseR(R)(LineR)", "", "R", "Blur",
                    "ConvulseR(R)(LineR)", "", "R", "", "ConvulseR(R)(LineR)", "", "R", "Blur",
                    "ConvulseR(R)(LineR)", "", "R", "", "ConvulseR(R)(LineRRev)", "", "R", "Blur",
                    "ConvulseL(R1)(LineL)(Up)", "", "R1", "", "ConvulseL(R1)(LineL)(Up)", "", "R1", "Blur",
                    "ConvulseL(R1)(LineL)(Up)", "", "R1", "", "ConvulseL(R1)(LineL)(Up)", "", "R1", "Blur",
                    "ConvulseL(R1)(LineL)(Up)", "", "R1", "", "ConvulseL(R1)(LineL)(Up)", "", "R1", "Blur",
                    "ConvulseL(R1)(LineL)(Up)", "", "R1", "", "ConvulseL(R1)(LineLRev)(Up)", "", "R1", "Blur",
                    "ConvulseR(R1)(LineR)(Up)", "", "R1", "", "ConvulseR(R1)(LineR)(Up)", "", "R1", "Blur",
                    "ConvulseR(R1)(LineR)(Up)", "", "R1", "", "ConvulseR(R1)(LineR)(Up)", "", "R1", "Blur",
                    "ConvulseR(R1)(LineR)(Up)", "", "R1", "", "ConvulseR(R1)(LineR)(Up)", "", "R1", "Blur",
                    "ConvulseR(R1)(LineR)(Up)", "", "R1", "", "ConvulseR(R1)(LineRRev)(Up)", "", "R1", "Blur",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }

                }
                if (InBeat(512 - 8 - 4))
                {
                    RegisterFunctionOnce("LineL2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });

                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("BlurLong", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.26f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(14), ValueEasing.EaseOutQuad(0.26f, 0.02f, BeatTime(14)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("BlurLong2", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.36f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(24), ValueEasing.EaseOutQuad(0.36f, 0.02f, BeatTime(24)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), true);
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(8), false);
                        ScreenDrawing.CameraEffect.SizeShrink(5, BeatTime(8));
                    });

                    RegisterFunctionOnce("SceneOut", () =>
                    {
                        DelayBeat(8, () =>
                        {
                            ScreenDrawing.SceneOut(Color.Black * 0.98f, BeatTime(8f - 1));
                        });
                        for (int i = 0; i < 7; i++)
                        {
                            int t = i;
                            DelayBeat(t, () =>
                            {
                                ScreenDrawing.MakeFlicker(Color.Black * (t * 0.06f + 0.4f));
                            });
                            if (i >= 4)
                            {
                                DelayBeat(t + 0.5f, () =>
                                {
                                    ScreenDrawing.MakeFlicker(Color.Black * (t * 0.06f + 0.4f));
                                });
                            }
                        }

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseInElastic(0.0f, 0.5f, BeatTime(8)));
                        builder.Insert(BeatTime(24), ValueEasing.EaseOutCubic(0.5f, 1.0f, BeatTime(24)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.Silver, Color.Gray * 0.5f, s);
                        });
                    });

                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, -3f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 3f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 0f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });

                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur  
                    "(D)(+01)RotateL", "", "(D)(+01)", "", "(D)(+01)RotateR", "", "(D)(+01)", "",
                    "RotateMid", "", "", "", "", "", "", "(Blur)",
                    "LineL(ConvulseR)", "", "", "(Blur)", "LineL(ConvulseR)", "", "", "(Blur)",
                    "LineL(ConvulseR)", "", "", "(Blur)", "LineL(ConvulseR)", "", "", "",

                    "(D)(+01)RotateR", "", "(D)(+01)", "", "(D)(+01)RotateL", "", "(D)(+01)", "",
                    "RotateMid", "", "", "", "", "", "", "(Blur)",
                    "LineR(ConvulseL)", "", "", "(Blur)", "LineR(ConvulseL)", "", "", "(Blur)",
                    "LineR(ConvulseL)", "", "", "(Blur)", "LineR(ConvulseL)", "", "", "(Blur)",

                    "(R)(+01)(ConvulseL)LineL", "", "(R)(+01)", "(Blur)", "(R)(+01)(ConvulseR)LineR", "", "(R)(+01)", "(Blur)",
                    "(R)(+01)(ConvulseL)LineL", "", "(R)(+01)", "(Blur)", "(R)(+01)(ConvulseR)LineR", "", "(R)(+01)", "(Blur)",
                    "(R)(+01)(ConvulseL)LineL", "", "(R)(+01)LineR", "(Blur)", "(R)(+01)(ConvulseR)LineL", "", "(R)(+01)LineR", "(Blur)",
                    "(R)(+01)(ConvulseL)LineL", "", "(R)(+01)LineR", "(Blur)", "(R)(+01)(ConvulseR)LineL", "", "(R)(+01)LineR", "(Blur)",

                    "R(LineL2)(ConvulseL)", "+0(LineL2)(Blur)", "R(LineR2)", "+0(LineR2)(Blur)", "R(LineL2)(ConvulseL)", "+0(LineL2)(Blur)","R(LineR2)", "+0(LineR2)(Blur)",
                    "R(LineL2)(ConvulseR)", "+0(LineL2)(Blur)", "R(LineR2)", "+0(LineR2)(Blur)", "R(LineL2)(ConvulseR)", "+0(LineL2)(Blur)","+0(LineR2)", "(LineR2)(BlurLong2)",
                    "SceneOut(#8#$00)(#8#$21)(ConvulseL2)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    float time = BeatTime(4);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(640 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("MovePosition", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(16, 16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(16, 16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(-16, 16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(-16, 16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(-16, -16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(-16, -16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(16, -16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(16, -16), new(0, 0), BeatTime(32)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        });
                    });
                    RegisterFunctionOnce("BlurGauss", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(12), ValueEasing.EaseInCubic(0, 1.5f, BeatTime(12)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(1.5f, 0, BeatTime(8)));
                        DelayBeat(1.4f, () =>
                        {
                            builder.Run((s) => Blur.Sigma = s);
                        });
                    });
                    RegisterFunctionOnce("Pre", () =>
                    {
                        InstantSetBox(600, 60, 60);
                        rainer.Intensity = 0.18f;
                        rainer.Speed = 3f;
                        Blur.Sigma = 0.0f;
                        InstantTP(BoxStates.Centre);

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), CentreEasing.EaseOutCirc(new(450, 600), new(320, 240), BeatTime(6)));
                        builder.Run((s) =>
                        {
                            InstantTP(s);
                            InstantSetBox(s, 84, 84);
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("FakeBox2", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(-160, -120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        box2.InstantSetAlpha(0);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("FakeBox1", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(160, 120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.InstantSetAlpha(0.0f);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });

                    Action<Vector2, float> MakeLine = (delta, angle) =>
                    {
                        Vector2 centre = new(320, 240);
                        Line line = new(CentreEasing.EaseOutQuad(centre + delta, centre + delta + GetVector2(360, angle), BeatTime(16)), (s) => angle + 90);
                        line.Alpha = 0;
                        line.AlphaIncrease(BeatTime(4), 0.4f);
                        DelayBeat(8, () => line.AlphaDecrease(BeatTime(8)));
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++) line.InsertRetention(new(i * 2 + 2, 0.6f - i * 0.15f));
                    };
                    RegisterFunctionOnce("Line1", () => MakeLine(new(180, 120), -120));
                    RegisterFunctionOnce("Line2", () => MakeLine(new(-180, 120), -60));

                    string[] rhythm = { 
                    //pre
                    "", "", "Pre", "", "", "", "", "",

                    //cur
                    "RotateL(MovePosition)(FakeBox1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "#40#R1(RotateR2)(Line1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "#52#D0(RotateL2)(FakeBox2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "#40#+11", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateR2)(Line2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(768 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("BlurGauss", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(12), ValueEasing.EaseInCubic(0, 1.5f, BeatTime(12)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(1.5f, 0, BeatTime(8)));
                        DelayBeat(1.4f, () =>
                        {
                            builder.Run((s) => Blur.Sigma = s);
                        });
                    });
                    RegisterFunctionOnce("FakeBox3", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(160, -120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        box2.InstantSetAlpha(0);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("FakeBox4", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(-160, 120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.InstantSetAlpha(0.0f);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    Action<Vector2, float> MakeLine = (delta, angle) =>
                    {
                        Vector2 centre = new(320, 240);
                        Line line = new(CentreEasing.EaseOutQuad(centre + delta, centre + delta + GetVector2(360, angle), BeatTime(16)), (s) => angle + 90);
                        line.Alpha = 0;
                        line.AlphaIncrease(BeatTime(4), 0.4f);
                        DelayBeat(8, () => line.AlphaDecrease(BeatTime(8)));
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++) line.InsertRetention(new(i * 2 + 2, 0.6f - i * 0.15f));
                    };
                    RegisterFunctionOnce("Line2", () => MakeLine(new(180, -120), 120));
                    RegisterFunctionOnce("Line1", () => MakeLine(new(-180, -120), 60));
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "#40#D(RotateL)(FakeBox3)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D1(RotateL)", "", "", "", "", "", "", "",
                    "D1", "", "", "", "", "", "", "",
                    "#40#R1(RotateR)(Line1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D(RotateR)", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "#52#D0(RotateL2)(FakeBox4)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "#40#+11", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateR2)(Line2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    ">-10", "<+00", ">+11", "<+01", ">-10", "<+00", ">+11", "<+01",
                    "<-10", ">+00", "<+11", ">+01", "<-10", ">+00", "<+11", ">+01",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(896 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "(RotateL)R", "", "", "", "R", "", "", "D",
                    "+0", "", "R", "", "R", "", "", "",
                    "(RotateL)R1", "", "", "R", "+0", "", "R", "+0",
                    "", "R", "+0", "", "R", "", "", "",
                    "(RotateR)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "(RotateR)R1", "", "", "R", "+0", "", "R", "+0",
                    "", "R", "+0", "R", "+0", "", "R", "",
                    "(RotateL2)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "R", "", "", "R", "+0", "", "R", "+0",
                    "", "R", "+0", "", "R", "", "", "",
                    "(RotateR2)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "R", "", "", "R", "+0", "", "R", "+0",
                    "R", "+0", "R", "+0", "+0", "", "R", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(1024 - 8))
                {
                    SetPlayerBoxMission(0);
                    float time = BeatTime(0);
                    ScreenDrawing.ScreenAngle = 3;
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.12f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.12f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s * 0.7f);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 7f;
                                splitter.Intensity = MathF.Max(1f, s * 40 + 0.19f);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutQuad(3, 0, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutQuad(0, 3f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(14, BeatTime(3.1f), false);
                    });
                    RegisterFunctionOnce("ConvulseR2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(14, BeatTime(3.1f), true);
                    });
                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1), ValueEasing.Linear(1, 0.5f, BeatTime(1)));
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.5f, 1f, BeatTime(1.5f)));
                        builder.Insert(BeatTime(0.5f), ValueEasing.Linear(1f, 0.6f, BeatTime(0.5f)));
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.5f, 1f, BeatTime(1.5f)));
                        builder.Insert(BeatTime(0.5f), ValueEasing.Linear(1f, 0.65f, BeatTime(0.5f)));
                        builder.Insert(BeatTime(4), ValueEasing.Linear(0.65f, 0, BeatTime(4)));

                        builder.Run(s =>
                        {
                            Color col = Color.Lerp(Color.Transparent, Color.White, s);
                            ScreenDrawing.ThemeColor = ScreenDrawing.UIColor = col;
                        });

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(1), ValueEasing.Linear(-2, 0, BeatTime(1)));
                        builder2.Insert(BeatTime(4), ValueEasing.Linear(4, 0, BeatTime(4)));
                        builder2.Adjust = false;
                        builder2.Run(s =>
                        {
                            ScreenDrawing.UISettings.HPShowerPos += new Vector2(0, s);
                            ScreenDrawing.UISettings.NameShowerPos += new Vector2(0, s);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.2f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.2f), true);
                    });
                    RegisterFunctionOnce("Line1", () =>
                    {
                        int count = 5;
                        Line line = new(CentreEasing.EaseOutQuart(new(0, 0), new(640, 480), BeatTime(count)), ValueEasing.Stable(-45));
                        line.Alpha = 0.5f;
                        line.Width = 3.4f;
                        line.InsertRetention(new(1, 0.5f));
                        line.InsertRetention(new(2, 0.4f));
                        line.InsertRetention(new(3, 0.3f));
                        CreateEntity(line);
                        DelayBeat(count, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        int count = 5;
                        Line line = new(CentreEasing.EaseOutQuart(new(640, 0), new(0, 480), BeatTime(count)), ValueEasing.Stable(45));
                        line.Alpha = 0.5f;
                        line.Width = 3.4f;
                        line.InsertRetention(new(1, 0.5f));
                        line.InsertRetention(new(2, 0.4f));
                        line.InsertRetention(new(3, 0.3f));
                        CreateEntity(line);
                        DelayBeat(count, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Line3", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            float count = 5.5f;
                            Func<ICustomMotion, Vector2> p1 = CentreEasing.EaseOutCubic(new(320, 480), new(320, -260), BeatTime(count));
                            Line line1 = new(CentreEasing.Combine(p1, new Vector2(-320, 240)), p1);
                            Line line2 = new(CentreEasing.Combine(p1, new Vector2(320, 240)), p1);

                            Line[] lines = { line1, line2 };
                            foreach (Line line in lines)
                            {
                                line.Alpha = 0.5f;
                                line.Width = 3.4f;
                                line.InsertRetention(new(0.5f, 0.5f));
                                line.InsertRetention(new(1, 0.4f));
                                line.InsertRetention(new(1.5f, 0.3f));
                                CreateEntity(line);
                                DelayBeat(count, () => line.Dispose());
                            }
                        });
                    });
                    RegisterFunctionOnce("Rise", () =>
                    {
                        SetBoxMission(0);
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(8), ValueEasing.EaseOutCubic(BoxStates.Centre.Y, BoxStates.Centre.Y - 15, BeatTime(8)));
                        ease.Run(s => { InstantSetBox(s, 84, 84); InstantTP(320, s); });
                    });
                    RegisterFunctionOnce("Step1", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                        ScreenDrawing.UISettings.UISurfaceDrawing production = ScreenDrawing.UISettings.CreateUISurface();
                        rainer.controlLayer = production.UISurface;

                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);

                        ValueEasing.EaseBuilder ease1 = new();
                        ease1.Insert(BeatTime(8), ValueEasing.EaseOutCirc(210, 280, BeatTime(8)));
                        ease1.Insert(BeatTime(8), ValueEasing.EaseOutElastic(280, 410, BeatTime(8)));

                        ValueEasing.EaseBuilder easeL = new();
                        easeL.Insert(BeatTime(8), ValueEasing.EaseOutCubic(320, 260, BeatTime(8)));
                        easeL.Insert(BeatTime(8), ValueEasing.EaseOutQuint(260, 140, BeatTime(8)));
                        ValueEasing.EaseBuilder easeR = new();
                        easeR.Insert(BeatTime(8), ValueEasing.EaseOutCubic(320, 380, BeatTime(8)));
                        easeR.Insert(BeatTime(8), ValueEasing.EaseOutQuint(380, 260, BeatTime(8)));

                        CentreEasing.EaseBuilder builder1 = new();
                        builder1.Insert(BeatTime(16), CentreEasing.Combine(easeL.GetResult(), ease1.GetResult()));
                        builder1.Run(s =>
                        {
                            SetPlayerBoxMission(0);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.Combine(easeR.GetResult(), ease1.GetResult()));
                        builder2.Run(s =>
                        {
                            SetPlayerBoxMission(1);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                    });
                    RegisterFunctionOnce("Step2", () =>
                    {
                        float time = BeatTime(8);

                        ScreenDrawing.MakeFlicker(Color.Black * 0.5f);
                        ValueEasing.EaseBuilder shadowOut = new();
                        shadowOut.Insert(BeatTime(4), ValueEasing.EaseOutQuad(ScreenDrawing.DownBoundDistance, 0, BeatTime(4)));
                        shadowOut.Run(s =>
                        {
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s;
                        });
                        CentreEasing.EaseBuilder ease1 = new();
                        ease1.Insert(BeatTime(8), CentreEasing.Alternate(
                            1,
                            CentreEasing.EaseOutCirc(new(260, 280), new(380, 410), time),
                            CentreEasing.Combine(ValueEasing.EaseOutCirc(260, 380, time), ValueEasing.EaseOutElastic(280, 410, time))
                        ));
                        CentreEasing.EaseBuilder ease2 = new();
                        ease2.Insert(BeatTime(8), CentreEasing.Alternate(
                            1,
                            CentreEasing.EaseOutCirc(new(380, 280), new(500, 410), time),
                            CentreEasing.Combine(ValueEasing.EaseOutCirc(380, 500, time), ValueEasing.EaseOutElastic(280, 410, time))
                        ));
                        SetPlayerBoxMission(0);
                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);
                        ease1.Run(s =>
                        {
                            SetPlayerBoxMission(2);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        SetPlayerBoxMission(1);
                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);
                        ease2.Run(s =>
                        {
                            SetPlayerBoxMission(3);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        DelayBeat(6, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(2), ValueEasing.Linear(0.5f, 0.0f, BeatTime(2)));
                            builder.Run(s =>
                            {
                                for (int i = 0; i <= 3; i++)
                                {
                                    SetPlayerBoxMission(i);
                                    BoxStates.CurrentBox.GreenSoulAlpha = s;
                                    Heart.Alpha = s * 2;
                                }
                            });
                        });
                    });
                    RegisterFunctionOnce("LineS", () =>
                    {
                        float count = 5.5f;
                        Line line1 = new(CentreEasing.EaseOutSine(new(360, 240), new(200, 240), BeatTime(7)), ValueEasing.Stable(90));
                        Line line2 = new(CentreEasing.EaseOutSine(new(280, 240), new(440, 240), BeatTime(7)), ValueEasing.Stable(90));

                        Line[] lines = { line1, line2 };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0;
                            line.Width = 3.4f;
                            line.InsertRetention(new(1, 0.4f));
                            line.AlphaIncrease(BeatTime(1f), 0.7f);
                            DelayBeat(0.51f + 2, () => line.AlphaDecrease(BeatTime(4.3f)));
                            CreateEntity(line);
                            DelayBeat(count, () => line.Dispose());
                        }
                    });
                    RegisterFunctionOnce("Line4k", () =>
                    {
                        float height = 410 - 42 - 2;
                        Line line1 = new(CentreEasing.Stable(new(0, height)), (s) =>
                        {
                            float k = s.AppearTime / BeatTime(7);
                            float scale = (1 - k) * (1 - k);
                            float angle = scale * MathF.PI / 2;
                            return new Vector2(MathF.Cos(angle) * 640f * 1.6f, -MathF.Sin(angle) * height * 1.6f) + new Vector2(0, height);
                        });
                        line1.InsertRetention(new(1, 0.5f));
                        line1.Alpha = 0.7f;
                        DelayBeat(4, () =>
                        {
                            line1.AlphaDecrease(BeatTime(3));
                        });
                        line1.TransverseMirror = true;

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 0), new(320, height), BeatTime(8)));
                        builder.Insert(BeatTime(32), CentreEasing.Stable(new(320, height)));

                        Line line2 = new(builder.GetResult(), (s) => 0);
                        line2.Alpha = 0.5f;
                        DelayBeat(4, () => line2.AlphaIncrease(BeatTime(4), 0.5f));

                        CreateEntity(line1);
                        CreateEntity(line2);

                        DelayBeat(4, () =>
                        {
                            for (int i = -2; i <= 2; i++)
                            {
                                float xPos = 320 + i * 120;
                                CentreEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(xPos, -80), new(xPos, height), BeatTime(8)));
                                Line line = new(CentreEasing.Stable(xPos, -690), builder.GetResult());
                                CreateEntity(line);
                                line.DrawingColor = Color.Silver;
                                line.Width = 3.5f + MathF.Abs(i) * 0.6f;
                            }
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "(RotateL)R", "", "", "", "R", "", "", "R",
                    "+2", "", "+2", "", "R", "", "", "",
                    "(RotateL)R", "", "", "$0", "+2", "", "+2", "+2",
                    "", "+2", "+2", "", "R", "", "", "",
                    "(RotateR)R", "", "", "", "R", "", "", "R",
                    "+2", "", "+2", "", "R", "", "", "",
                    "(RotateR2)R", "", "", "$2", "+2", "", "+2", "+2",
                    "(RotateL2)", "+2", "+2", "+2", "+2", "", "+0", "Blur",
                    "(ConvulseL)(Line1)(R)(+01)", "", "(+0)(+01)", "", "(R)(+011)", "", "(+0)(+01)", "",
                    "(R)(+01)", "", "(+0)(+01)", "", "(R)(^+01'2)", "", "(+0)(+01)", "Blur",
                    "(ConvulseL)(Line2)(R)(+01)", "", "(+0)(+01)", "", "(R)(+011)", "", "(+0)(+01)", "",
                    "(R)(+01)", "", "(+0)(+01)", "", "(R)(^+01'2)", "", "(+0)(+01)", "Blur(Line3)",
                    "(ConvulseR)(Rise)(R)(+01)", "", "(+0)(+01)", "+011", "(R)(^+01'2)", "", "(+0)(+01)", "Blur(Line3)",
                    "(ConvulseR)(Rise)(R)(+01)", "", "(+0)(+01)", "+011", "(R)(^+01'2)", "", "(+0)(+01)", "Blur(Line3)(LineS)",
                    "(ConvulseR2)(Step1)", "", "", "", "", "", "", "Flicker",
                    "(ConvulseL2)(Step2)(Line4k)", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(1152 - 8))
                {
                    DelayBeat(10, Regenerate);
                    float camRotation = 48;
                    #region event
                    RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(0, 0), new(0, 60), BeatTime(8)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDelta = s);

                        cameraProduction = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);

                        ValueEasing.EaseBuilder camera = new();
                        Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending));
                        Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y);
                        Effect3D.ProjectPoint = new(0, 0, 200);

                        ValueEasing.EaseBuilder posMove = new();
                        posMove.Insert(BeatTime(8), ValueEasing.EaseOutSine(200, 170, BeatTime(8)));
                        posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                        camera.Insert(BeatTime(8), ValueEasing.EaseOutSine(0, camRotation, BeatTime(8)));
                        camera.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        SetPlayerMission(0);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                        SetPlayerMission(1);
                        Heart.RotateTo(-90);
                        Heart.FixArrow = false;
                        SetPlayerMission(2);
                        Heart.RotateTo(90);
                        Heart.FixArrow = false;
                        SetPlayerMission(3);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.02f, 0.01f, BeatTime(2.5f)));
                        builder.Run((s) =>
                        {
                            splitter.Intensity = s * 30 + 0.39f;
                        });
                    });
                    RegisterFunctionOnce("CameraZL", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, 0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, -0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation - 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation - 1.2f, camRotation, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation + 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation + 1.2f, camRotation, BeatTime(1.5f)));
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

                    float speed = 26.5f;
                    float scale = 2.5f;
                    float lateWaitingScale = 0.1f;
                    RegisterFunctionOnce("T0V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(0);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 0, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 2, speed, 0, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 0, 0);
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
                    RegisterFunctionOnce("Tag", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            //PlaySound(Sounds.pierce);
                        }));
                    });
                    DelayEnabled = false;
                    #endregion
                    CreateChart(0, BeatTime(1) * 8, 8.0f, new[] { 
                    //pre
                    "Move(Rotate)", "", "", "", "", "", "", "",
                    //cur
                    "T0(T3)(Tag)(CameraZL)", "T0V(T3V)", "", "",     //3
                    "!!12/3", "(T0)(Tag)", "", "",    "", "!!12/3", "(T3)", "", "",    "",  //4
                    "T0(Tag)(CameraUp)(CameraZL)", "", "", "T1(CameraUp)(CameraZR)",  //5
                    "(Tag)", "", "(T2)", "",  //6
                    "!!3", "(T1)(Tag)(CameraUp)", "(T2)(CameraZL)", "(T1)(CameraZR)",  //7
                    "(T2)(Tag)", "(T2V)", "(T2V)", "(T2V)", //8
                    "T1(T2V)(Tag)(CameraZL)", "", "!!8/4", "(T2)", "", "", "", //9
                    "(T1)(Tag)(CameraUp)", "(T1V)", "(T1V)(T2)(CameraZL)", "T2V", //10
                    "!!8/4", "(T1)(T2V)(Tag)(CameraDown)", "", "", "",    "", "T1(T2)(CameraZL)", //11
                    "(Tag)", "T1(T2)(CameraZL)", "", "", //12
                    "(T2)(Tag)(CameraZL)", "(T2V)", "(T2V)", "T2V", //13
                    "T1(Tag)", "T2", "T1", "T2", //14
                    "!!12/3", "T1(Tag)(CameraUp)", "", "T2",    "", "", "", //15
                    "T2(Tag)", "T1", "T2", "T1", //16
                    "!!12/3", "(T2)(Tag)(CameraUp)", "", "T1",    "", "", "", //17
                    "!!8/4", "T1(Tag)", "", "T1V", "",     "!!8/4", "T2(Tag)", "", "T2V", "", //18
                    "(T3)(Tag)(CameraZL)(CameraUp)", "(T3V)", "(T3V)", "(T3V)",  //19
                    "!!12/3", "(T3V)(T0)(Tag)(CameraUp)", "", "",   "(T3V)", "!!12/3", "(T0)(T3V)(CameraDown)", "", "",   "(T3V)", //20
                    "(T3V)(T0)(Tag)(CameraZL)", "", "", "T1(T2)", //21
                    "(Tag)", "", "(T1)(T2)", "", //22
                    "!!3", "(Tag)(T1)(CameraUp)(CameraZL)", "(T2)(CameraZR)", "T1(CameraZL)", //23
                    "(T2)(Tag)(CameraZL)", "(T2V)", "(T2V)", "(T2V)", //24
                    "T1(T2V)(Tag)", "", "!!16/4", "(T2)(CameraZL)(CameraUp)", "", "", "",    "", //25
                    "T1(Tag)", "", "(T2)", "", //26
                    "!!6/3", "T0(T3)(Tag)(CameraZL)(CameraDown)", "", "",    "", "T0(T3)", //27
                    "(Tag)", "T0(T3)", "", "", //28
                    "!!8", "T1(T2)(Tag)(CameraZR)(CameraUp)", "", "T1V(T2V)", "", "T1V(T2V)", "", "T1V(T2V)", "", //29
                    "T1V(T2V)(Tag)", "T1V(T2V)", "T1V(T2V)", "T1V(T2V)",  //30
                    "(T0)(T3)(T1V)(T2V)(Tag)(CameraZL)(CameraUp)", "T0V(T3V)", "T0V(T3V)", "",  //31
                    "T2(Tag)", "T1", "T2", "T1",  //32
                    "!!8/2", "T2(Tag)(CameraZL)(CameraUp)", "",    "", "T1", "", //33
                    "!!8/2", "T2(Tag)(CameraZL)(CameraUp)", "", "",     "!!8/2", "T1", "", "" //34

                });
                }
                if (InBeat(1280 - 8))
                {
                    float speed = 26.5f;
                    float scale = 2.5f;
                    float lateWaitingScale = 0.1f;
                    float camRotation = 48;
                    #region event
                    RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(0, 0), new(0, 60), BeatTime(8)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDelta = s);

                        RenderProduction production4 = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(production4);

                        ValueEasing.EaseBuilder camera = new();
                        Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending));
                        Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y);
                        Effect3D.ProjectPoint = new(0, 0, 200);
                        camera.Insert(BeatTime(8), ValueEasing.EaseOutSine(0, camRotation, BeatTime(8)));
                        camera.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        SetPlayerMission(0);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                        SetPlayerMission(1);
                        Heart.RotateTo(-90);
                        Heart.FixArrow = false;
                        SetPlayerMission(2);
                        Heart.RotateTo(90);
                        Heart.FixArrow = false;
                        SetPlayerMission(3);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                    });
                    RegisterFunctionOnce("CameraZL", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, 0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, -0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation - 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation - 1.2f, camRotation, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation + 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation + 1.2f, camRotation, BeatTime(1.5f)));
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
                    RegisterFunctionOnce("T0V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(0);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 0, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 2, speed, 0, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 0, 0);
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
                    RegisterFunctionOnce("Tag", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            //PlaySound(Sounds.pierce);
                        }));
                    });
                    DelayEnabled = false;
                    #endregion
                    CreateChart(0, BeatTime(1) * 8, 8.0f, new[] {
                    //pre
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "T0(T3)(CameraDown)(CameraZL)", "T0V(T3V)", "", "",    //35
                    "!!8/2", "(CameraZL)(T0)", "",     "", "!!8/2", "(T3)", "",    "",    //36
                    "T0", "!!8/4", "", "", "T2", "",    "!!12/3", "T1(CameraZR)", "", "",   //37
                    "", "!!12/3", "(CameraZR)T1", "", "",     "", "",    //38
                    "!!3", "(T2)(CameraZL)(CameraUp)", "T1(CameraZR)", "(T2)(CameraZL)",    //39
                    "T1(CameraDown)", "T1V", "T1V", "T1V",    //40
                    "T1V(T2)(CameraZL)", "", "!!16/4", "CameraZL(CameraDown)(T1)", "", "", "",    "",    //41
                    "(T2)(CameraZR)", "", "T1", "",    //42
                    "(T1)(T2)(CameraUp)", "", "", "T2(T1)",    //43
                    "", "T0(CameraZL)(T3)", "", "",    //44
                    "!!8/2", "T1(T2)(CameraZL)", "",   "", "", "",    //45
                    "T2", "T1", "T2", "T1",    //46
                    "(CameraDown)(T2)", "T2V", "T2V", "T2V",    //47
                    "T1", "T2", "T1", "T2",    //48
                    "!!6", "(CameraZR)T1", "", "", "T2", "", "",    //49
                    "!!16/4", "CameraUp(T1)", "", "", "",     "",   "!!16/4", "CameraUp(T2)", "", "", "",     "",    //50
                    "CameraZL(CameraDown)(T0)", "(T0V)", "(T0V)", "(T0V)",    //51
                    "!!8/2", "(T0V)(T3)", "",   "(T0V)",   "!!8/2", "(T0V)(T3)", "",   "(T0V)",    //52
                    "(T3)(T0V)(CameraZL)", "", "", "T0(T3)(CameraZR)",    //53
                    "", "T0(T3)", "", "",    //54
                    "!!3", "(CameraZL)(CameraUp)(T1)", "(CameraZR)(T2)", "(CameraZL)(T1)",    //55
                    "CameraZL(CameraDown)(T2)", "(T2V)", "(T2V)", "(T2V)",    //56
                    "(T2V)(T1)(CameraZL)", "", "!!16/4", "(T2)(CameraZL)", "", "", "",     "",    //57
                    "(CameraZL)T1", "", "(T2)", "",    //58
                    "(CameraZL)(CameraDown)(T1)", "", "", "(CameraZR)(T2)",    //59
                    "", "(CameraZL)", "", "",    //60 
                    "T0(T1)(CameraZL)(CameraDown)", "", "", "",    //61
                    "(T2)(T3)(CameraZL)(CameraDown)", "", "", "",    //62 
                    "T0(T2)(T1)(T3)(CameraZR)(CameraUp)", "", "", "",    //63 
                });

                }
                if (InBeat(1408 - 16 - 1))
                {
                    DelayEnabled = true;

                    DelayBeat(12, Regenerate);

                    RegisterFunctionOnce("Shatter", () =>
                    {
                        DelayBeat(0.875f, () =>
                        {
                            // sinwave shake
                            ScreenDrawing.Shaders.Filter filter;
                            ScreenDrawing.SceneRendering.InsertProduction(filter = new ScreenDrawing.Shaders.Filter(SinWave, 0.6755f));
                            DelayBeat(15, () =>
                            {
                                filter.Dispose();
                                production1.Dispose(); production2.Dispose();
                                ScreenDrawing.UpExtending = 0;
                                cameraProduction.Dispose();
                                Blur.Dispose();
                                ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                                Line[] lines = GetAll<Line>();
                                foreach (Line line in lines)
                                {
                                    line.Dispose();
                                }
                            });
                            float a = 0;
                            ForBeat(15, () =>
                            {
                                SinWave.Parameters["time"].SetValue(a); a += 0.25f;
                                SinWave.Parameters["time2"].SetValue(a * 1.5f);
                            });

                            ValueEasing.EaseBuilder rgbShake = new();
                            rgbShake.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0, 4, BeatTime(1.5f)));
                            rgbShake.Insert(BeatTime(14.5f), ValueEasing.EaseOutCubic(4, 0, BeatTime(14.5f)));
                            rgbShake.Run(s =>
                            {
                                SinWave.Parameters["frequency"].SetValue(s * 10f + 140f);
                                SinWave.Parameters["frequency2"].SetValue(s * 25f + 200f);
                                SinWave.Parameters["range2"].SetValue(s);
                                SinWave.Parameters["range"].SetValue(s * 1.25f);
                            });

                            // blur effect
                            ValueEasing.EaseBuilder blur = new();
                            blur.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0.0f, 0.2f, BeatTime(16)));
                            blur.Run(s =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                Blur.Sigma = s * 5;
                            });

                            // camera effect
                            ValueEasing.EaseBuilder camera = new();
                            camera.Insert(BeatTime(14), ValueEasing.EaseOutCirc(0.0f, 0.2f, BeatTime(14)));
                            camera.Run(s =>
                            {
                                ScreenDrawing.ScreenAngle = s * 52;
                            });

                            // flicker 
                            ScreenDrawing.MakeFlicker(Color.White * 0.55f);
                            ScreenDrawing.SceneOutScale = 1.6f;
                            DelayBeat(3, () => ScreenDrawing.SceneOut(Color.Black, BeatTime(11.5f)));

                            // set chart
                            DelayBeat(15.1f, () =>
                            {
                                for (int i = 3; i > 0; i--)
                                {
                                    SetPlayerMission(i);
                                    Heart.Dispose();
                                }
                                SetPlayerMission(0);
                                InstantTP(new(320, 240));
                                InstantSetGreenBox();
                                ScreenDrawing.ScreenAngle = 0;
                                Heart.InstantSetRotation(0);
                                Heart.Alpha = 1.0f;
                                BoxStates.CurrentBox.GreenSoulAlpha = 0.5f;
                                rainer.Intensity = 0.15f;
                                ScreenDrawing.BoundColor = Color.White;
                                ScreenDrawing.ThemeColor = Color.White;
                                ScreenDrawing.UIColor = Color.White;
                                ScreenDrawing.UISettings.RemoveUISurface();
                                ScreenDrawing.UISettings.NameShowerPos = new Vector2(20, 457);
                                ScreenDrawing.UISettings.HPShowerPos = new(320, 443);
                            });

                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 4, BeatTime(16)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseInQuad(4, 2, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(2, 0, BeatTime(8)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 4, BeatTime(16)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseInQuad(4, 2, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(2, 0, BeatTime(8)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = -s;
                        });
                    });
                    string[] rhythm = { 
                    //pre 
                    "Shatter", "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    //cur 
                    "RotateL", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateR", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateL", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateR", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    string[] rhythm2 = { 
                    //pre 
                    "",
                    //cur
                    "$0", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#5#D1(D)", "", "", "", "", "", "", "",
                    "#5#D(D1)", "", "", "", "", "", "", "",
                    "#3#D1(D)", "", "", "", "", "", "#4#D(D1)", "",
                    "", "", "", "", "#17#D1(D)(D)", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1(D0)", "", "", "",
                    "#13#D0(D1)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#5#D1(D)", "", "", "", "", "", "", "",
                    "#5#D(D1)", "", "", "", "", "", "", "",
                    "#3#D1(D)", "", "", "", "", "", "#23#D(D1)(D1)", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1(D0)", "", "", "",
                };
                    CreateChart(0, BeatTime(1) * 8, 6.0f, rhythm);
                    DelayBeat(9 + 7, () =>
                    {
                        CreateChart(0, BeatTime(1) * 8, 6.0f, rhythm2);
                    });
                }
            }
            /// <summary>
            /// IT'S EXTREME BARRAGE!!!
            /// </summary>
            public void Hard()
            {
                if (InBeat(64 - 8))
                {
                    RegisterFunctionOnce("Move", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;
                        heart1.InstantSplit(new(new(460, -80), new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.GreenSoulAlpha = 0.3f;

                        Vector2 delta = new(140, -320), centre = new(320, 240);

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutElastic(centre - delta, centre, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            SetPlayerBoxMission(heart1);
                            BoxStates.CurrentBox.GreenSoulAlpha = 0.5f;
                            InstantTP(BoxStates.Centre);
                            InstantSetBox(s, 84, 84);
                        });
                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(8), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(8)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantTP(BoxStates.Centre);
                            InstantSetBox(s, 84, 84);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        DelayBeat(5, () =>
                        {
                            builder3.Insert(BeatTime(3), ValueEasing.EaseOutCirc(0.3f, 0, BeatTime(4)));
                            builder3.Run((s) => box2.GreenSoulAlpha = s);
                        });
                        DelayBeat(8.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("LineUp2", () =>
                    {
                        Line line1 = new(new(90, 0), BoxStates.CurrentBox.CollidingBox.TopLeft);
                        Line line2 = new(new(550, 0), BoxStates.CurrentBox.CollidingBox.TopRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.5f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineDown2", () =>
                    {
                        Line line1 = new(new(90, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft);
                        Line line2 = new(new(550, 480), BoxStates.CurrentBox.CollidingBox.BottomRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.5f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineUp", () =>
                    {
                        Line line1 = new(new(120, 0), BoxStates.CurrentBox.CollidingBox.TopLeft);
                        Line line2 = new(new(520, 0), BoxStates.CurrentBox.CollidingBox.TopRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.7f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineDown", () =>
                    {
                        Line line1 = new(new(120, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft);
                        Line line2 = new(new(520, 480), BoxStates.CurrentBox.CollidingBox.BottomRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.7f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("Cross", () =>
                    {
                        CentreEasing.EaseBuilder centre = new();
                        ValueEasing.EaseBuilder rotation = new();
                        centre.Insert(BeatTime(8), CentreEasing.EaseOutCirc(new(320, 240), new(320, 150), BeatTime(8)));
                        rotation.Insert(BeatTime(8), ValueEasing.Linear(0, 45, BeatTime(8)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutQuint(new(320, 150), new(240, 150), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, -180, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutQuint(new(240, 150), new(400, 150), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 180, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutExpo(new(400, 150), new(400, 330), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 90, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutExpo(new(400, 330), new(240, 330), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 90, BeatTime(32)));

                        centre.Insert(BeatTime(4), CentreEasing.EaseOutCirc(new(240, 330), new(320, 240), BeatTime(4)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(45, -45, BeatTime(8)));

                        centre.Insert(BeatTime(96 - 4), CentreEasing.Stable(320, 240));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -5, BeatTime(2)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseInQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseOutQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(7.5f), ValueEasing.Stable(0));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(45, 90, BeatTime(6)));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(90, 135, BeatTime(6)));

                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(45, -45, BeatTime(8)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -5, BeatTime(2)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseInQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseOutQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(7.5f), ValueEasing.Stable(0));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(45, 90, BeatTime(6)));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(90, 135, BeatTime(6)));

                        rotation.Insert(BeatTime(5.85f), ValueEasing.EaseOutBack(45, -45, BeatTime(6)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -4, BeatTime(2)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, 8, BeatTime(2)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -4, BeatTime(1.85f)));

                        rotation.Insert(BeatTime(4), ValueEasing.EaseOutExpo(0, 60, BeatTime(4)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(0, 60, BeatTime(8)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(0, 60, BeatTime(8)));
                        rotation.Insert(BeatTime(0.15f), ValueEasing.Stable(0));

                        centre.Insert(BeatTime(16), CentreEasing.EaseOutQuad(new(320, 240), new(320, 80), BeatTime(16)));
                        rotation.Insert(BeatTime(16), ValueEasing.EaseOutBack(-45, 225, BeatTime(16)));
                        centre.Insert(BeatTime(16), CentreEasing.EaseOutQuad(new(320, 240), new(320, 80), BeatTime(16)));
                        rotation.Insert(BeatTime(16), ValueEasing.EaseOutBack(225, -45, BeatTime(16)));

                        Line line1 = new(centre.GetResult(), rotation.GetResult());
                        Line line2 = new(centre.GetResult(), ValueEasing.Combine(rotation.GetResult(), 90));
                        Line[] lines = { line1, line2 };

                        DelayBeat(256 + 8 + 16, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });
                        DelayBeat(240 + 8, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(14), 0.419f);
                            }
                        });
                        DelayBeat(240 + 8 + 14, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(6));
                            }
                        });

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutCubic(0, 0.42f, BeatTime(16)));

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.Alpha = 0.00f;
                            builder.Run(s => line.Alpha = s);

                            for (int i = 0; i < 6; i++)
                            {
                                int t = i;
                                DelayBeat(i * 16 + 8, () =>
                                {
                                    line.InsertRetention(new(t * 3 + 3, 0.32f - t * 0.03f));
                                });
                            }
                        }
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(0f, -4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "Move", "", "", "", "", "", "", "(Blur)",

                    //cur
                    "(R)(LineUp)(RotateL)", "", "", "", "(+1)(LineDown)", "", "", "",
                    "(+1)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "(Blur)",
                    "(D)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "",
                    "(+1)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "(Blur)",
                    "(D)(LineUp)(RotateR)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                    "(+1)(LineUp)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "(Blur)",
                    "(D)(LineUp)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                    "(+1)(LineUp)(Cross)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.2f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(128 - 16))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.06f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(12.1f), ValueEasing.EaseOutQuad(0.06f, 0.02f, BeatTime(12.1f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("Blur2", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(12.1f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(12.1f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, -4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 0f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("Up", () =>
                    {
                        ScreenDrawing.BoundColor = Color.Gray;
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutElastic(0, 140f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(4f), ValueEasing.EaseOutSine(0, 1f, BeatTime(4f)));
                        DelayBeat(4, () =>
                        {
                            builder2.Run((s) =>
                            {
                                ScreenDrawing.BoundColor = Color.Lerp(Color.Gray, Color.Silver, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("MovePosition", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(16, 16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(16, 16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(-16, 16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(-16, 16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(-16, -16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(-16, -16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(16, -16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(16, -16), new(0, 0), BeatTime(16)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        });
                    });
                    RegisterFunctionOnce("Black", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black, BeatTime(3));
                        DelayBeat(0.9f, () =>
                        {
                            rainer.Speed = 5f;
                            rainer.Intensity = 0.5f;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "(Blur)",

                    //cur
                    "D1(RotateL)(MovePosition)", "", "+11", "", "+11", "", "+11", "",
                    "D1", "", "+11", "", "+11", "", "+11", "(Blur2)",
                    "(D1)(+0)", "", "+11", "", "+11", "", "+11", "",
                    "(D1)(+0)", "", "+11", "", "+11", "", "+11", "(Blur)",
                    "D(RotateR)", "", "+1", "", "+1", "", "+1", "",
                    "D", "", "+1", "", "+1", "", "+1", "(Blur2)",
                 //   "D1", "", "D1", "", "D1", "(Up)", "D1", "",
                  //  "(RotateMid)(Black)", "", "", "",  
                    "(D)(+01)", "", "+1", "", "+1", "", "+1", "",
                    "(D)(+01)", "", "+1", "", "+1", "", "+1", "(Blur)",
                    "D1(RotateL)", "", "+11", "", "+11", "", "+11", "",
                    "D1", "", "+11", "", "+11", "", "+11", "(Blur2)",
                    "(D1)(+0)", "", "+11", "", "+11", "", "+11", "",
                    "(D1)(+0)", "", "+11", "", "+11", "", "+11", "(Blur)",
                    "D(RotateR)", "", "+1", "", "+1", "", "+1", "",
                    "D", "", "+1", "", "+1", "", "+1", "(Blur2)",
                    "D", "", "D", "", "D", "(Up)", "D", "",
                    "(RotateMid)(Black)", "", "", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.2f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(256 - 5))
                {
                    RegisterFunctionOnce("BlurLong", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.26f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(14), ValueEasing.EaseOutQuad(0.26f, 0.02f, BeatTime(14)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.09f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.09f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), true);
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(15, BeatTime(16), false);
                        ScreenDrawing.CameraEffect.SizeShrink(7, BeatTime(16));
                    });
                    RegisterFunctionOnce("ConvulseR2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(15, BeatTime(16), true);
                        ScreenDrawing.CameraEffect.SizeShrink(7, BeatTime(16));
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "(BlurLong)",

                    //cur 
                    "(#16#$00)(#16#$21)(ConvulseL)(ConvulseL2)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>^$21)",
                    "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>^$21)",
                    "($3'1.5)($3'1.5)", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#16#$01)(#16#$20)(ConvulseR)(ConvulseR2)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>^$20)",
                    "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>^$20)",
                    "($3'1.5)($3'1.5)", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#10#$00)(#10#$21)(ConvulseL)(ConvulseL2)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>^$21)",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "Blur",
                    "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "", "", "Blur",
                    "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "", "", "", "", "(BlurLong)",
                    "(#13#$31)(#13#$10)(ConvulseR)(ConvulseR2)", ">^$31(>$10)", "<$31(<^$10)", ">^$31(>$10)", "<$31(<^$10)", ">^$31(>$10)", "<$31(<^$10)", ">^$31(>^$10)",
                    "<$31(<^$10)", ">^$31(>$10)", "<$31(<^$10)", ">^$31(>$10)", "<$31(<^$10)", ">^$31(>^$10)", "", "(BlurLong)",
                    "(#13#$11)(#13#$30)(ConvulseL)(ConvulseL2)", ">^$11(>$30)", "<$11(<^$30)", ">^$11(>$30)", "<$11(<^$30)", ">^$11(>$30)", "<$11(<^$30)", ">^$11(>^$30)",
                    "<$11(<^$30)", ">^$11(>$30)", "<$11(<^$30)", ">^$11(>$30)", "<$11(<^$30)", ">^$11(>^$30)", "", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(384 - 8))
                {
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });

                    RegisterFunctionOnce("Down", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(32), ValueEasing.EaseInElastic(140f, 110f, BeatTime(32)));
                        builder.Insert(BeatTime(32), ValueEasing.EaseOutElastic(110f, 80f, BeatTime(32)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                    });

                    float delta = 0;
                    RegisterFunctionOnce("Up", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        ValueEasing.EaseBuilder builder2 = new();
                        float v = ScreenDrawing.UpBoundDistance;
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCubic(0 + v, 36f + v, BeatTime(2)));
                        builder.Insert(BeatTime(1.6f), ValueEasing.EaseInCubic(36f, 0.0f, BeatTime(1.6f)));

                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                        builder2.Insert(BeatTime(1), ValueEasing.EaseOutCirc(0 + delta, 0.04f + delta, BeatTime(1)));
                        builder2.Insert(BeatTime(2.6f), ValueEasing.EaseInCirc(0.04f, 0.011f, BeatTime(2.6f)));
                        delta += 0.011f;

                        builder2.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.Silver, Color.Red, s);
                        });
                    });

                    RegisterFunctionOnce("LineLRev", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(640, 240), new(320, 240), BeatTime(4)));
                        builder.Insert(BeatTime(2.5f), CentreEasing.EaseOutQuad(new(320, 240), new(640, 240), BeatTime(2.5f)));
                        Line line = new(builder.GetResult(), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3 + 2.5f, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5 + 2.5f, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineRRev", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(0, 240), new(320, 240), BeatTime(4)));
                        builder.Insert(BeatTime(2.5f), CentreEasing.EaseOutQuad(new(320, 240), new(0, 240), BeatTime(2.5f)));
                        Line line = new(builder.GetResult(), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3 + 2.5f, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5 + 2.5f, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.06f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.06f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 30 + 0.35f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(2), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(2), true);
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "Blur", 
                    //cur
                    "ConvulseL(D)(LineL)(Down)", "", "D", "", "ConvulseL(D)(LineL)", "", "D", "Blur",
                    "ConvulseL(D)(LineL)", "", "D", "", "ConvulseL(D)(LineL)", "", "D", "Blur",
                    "ConvulseL(D)(LineL)", "", "D", "", "ConvulseL(D)(LineL)", "", "D", "Blur",
                    "ConvulseL(D)(LineL)", "", "D", "", "ConvulseL(D)(LineLRev)", "", "D", "Blur",
                    "ConvulseR(D)(LineR)", "", "D", "", "ConvulseR(D)(LineR)", "", "D", "Blur",
                    "ConvulseR(D)(LineR)", "", "D", "", "ConvulseR(D)(LineR)", "", "D", "Blur",
                    "ConvulseR(D)(LineR)", "", "D", "", "ConvulseR(D)(LineR)", "", "D", "Blur",
                    "ConvulseR(D)(LineR)", "", "D", "", "ConvulseR(D)(LineRRev)", "", "D", "Blur",
                    "ConvulseL(D1)(LineL)(Up)", "", "D1", "", "ConvulseL(D1)(LineL)(Up)", "", "D1", "Blur",
                    "ConvulseL(D1)(LineL)(Up)", "", "D1", "", "ConvulseL(D1)(LineL)(Up)", "", "D1", "Blur",
                    "ConvulseL(D1)(LineL)(Up)", "", "D1", "", "ConvulseL(D1)(LineL)(Up)", "", "D1", "Blur",
                    "ConvulseL(D1)(LineL)(Up)", "", "D1", "", "ConvulseL(D1)(LineLRev)(Up)", "", "D1", "Blur",
                    "ConvulseR(D1)(LineR)(Up)", "", "D1", "", "ConvulseR(D1)(LineR)(Up)", "", "D1", "Blur",
                    "ConvulseR(D1)(LineR)(Up)", "", "D1", "", "ConvulseR(D1)(LineR)(Up)", "", "D1", "Blur",
                    "ConvulseR(D1)(LineR)(Up)", "", "D1", "", "ConvulseR(D1)(LineR)(Up)", "", "D1", "Blur",
                    "ConvulseR(D1)(LineR)(Up)", "", "D1", "", "ConvulseR(D1)(LineRRev)(Up)", "", "D1", "Blur",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                    CreateChart(BeatTime(8), BeatTime(8), 6.4f, new string[]
                    {
                    "$01","","","",   "$21","","","",
                    "$01","","","",   "$21","","","",
                    "$01","","","",   "$21","","","",
                    "$01","","","",   "$21","","","",

                    "$01","","","",   "$21","","","",
                    "$01","","","",   "$21","","","",
                    "$01","","","",   "$21","","","",
                    "$01","","","",   "$21","","","",

                    "$0","","","",   "$2","","","",
                    "$0","","","",   "$2","","","",
                    "$0","","","",   "$2","","","",
                    "$0","","","",   "$2","","","",

                    "$0","","","",   "$2","","","",
                    "$0","","","",   "$2","","","",
                    "$0","","","",   "$2","","","",
                    "$0","","","",   "$2","","","",
                    });
                }
                if (InBeat(512 - 8 - 4))
                {
                    RegisterFunctionOnce("LineL2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });

                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("BlurLong", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.26f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(14), ValueEasing.EaseOutQuad(0.26f, 0.02f, BeatTime(14)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("BlurLong2", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.36f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(24), ValueEasing.EaseOutQuad(0.36f, 0.02f, BeatTime(24)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), true);
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(8), false);
                        ScreenDrawing.CameraEffect.SizeShrink(5, BeatTime(8));
                    });

                    RegisterFunctionOnce("SceneOut", () =>
                    {
                        DelayBeat(8, () =>
                        {
                            ScreenDrawing.SceneOut(Color.Black * 0.98f, BeatTime(8f - 1));
                        });
                        for (int i = 0; i < 7; i++)
                        {
                            int t = i;
                            DelayBeat(t, () =>
                            {
                                ScreenDrawing.MakeFlicker(Color.Black * (t * 0.06f + 0.4f));
                            });
                            if (i >= 4)
                            {
                                DelayBeat(t + 0.5f, () =>
                                {
                                    ScreenDrawing.MakeFlicker(Color.Black * (t * 0.06f + 0.4f));
                                });
                            }
                        }

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseInElastic(0.0f, 0.5f, BeatTime(8)));
                        builder.Insert(BeatTime(24), ValueEasing.EaseOutCubic(0.5f, 1.0f, BeatTime(24)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.Silver, Color.Gray * 0.5f, s);
                        });
                    });

                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, -3f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 3f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 0f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });

                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur  
                    "(D)(+01)RotateL", "", "(D)(+01)", "", "(D)(+01)RotateR", "", "(D)(+01)", "",
                    "RotateMid", "", "", "", "", "", "", "(Blur)",
                    "LineL(ConvulseR)", "", "", "(Blur)", "LineL(ConvulseR)", "", "", "(Blur)",
                    "LineL(ConvulseR)", "", "", "(Blur)", "LineL(ConvulseR)", "", "", "",

                    "(D)(+01)RotateR", "", "(D)(+01)", "", "(D)(+01)RotateL", "", "(D)(+01)", "",
                    "RotateMid", "", "", "", "", "", "", "(Blur)",
                    "LineR(ConvulseL)", "", "", "(Blur)", "LineR(ConvulseL)", "", "", "(Blur)",
                    "LineR(ConvulseL)", "", "", "(Blur)", "LineR(ConvulseL)", "", "", "(Blur)",

                    "(D)(+01)(ConvulseL)LineL", "", "(D)(+01)", "(Blur)", "(D)(+01)(ConvulseR)LineR", "", "(D)(+01)", "(Blur)",
                    "(D)(+01)(ConvulseL)LineL", "", "(D)(+01)", "(Blur)", "(D)(+01)(ConvulseR)LineR", "", "(D)(+01)", "(Blur)",
                    "(D)(+01)(ConvulseL)LineL", "", "(D)(+01)LineR", "(Blur)", "(D)(+01)(ConvulseR)LineL", "", "(D)(+01)LineR", "(Blur)",
                    "(D)(+01)(ConvulseL)LineL", "", "(D)(+01)LineR", "(Blur)", "(D)(+01)(ConvulseR)LineL", "", "(D)(+01)LineR", "(Blur)",

                    "D(LineL2)(ConvulseL)", "+0(LineL2)(Blur)", "D1(LineR2)", "+01(LineR2)(Blur)", "D(LineL2)(ConvulseL)", "+0(LineL2)(Blur)","D1(LineR2)", "+01(LineR2)(Blur)",
                    "D(LineL2)(ConvulseR)", "+0(LineL2)(Blur)", "D1(LineR2)", "+01(LineR2)(Blur)", "D(LineL2)(ConvulseR)", "+0(LineL2)(Blur)","+0(LineR2)", "(LineR2)(BlurLong2)",
                    "SceneOut(#8#$00)(#8#$21)(ConvulseL2)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    float time = BeatTime(4);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(640 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("MovePosition", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(16, 16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(16, 16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(-16, 16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(-16, 16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(-16, -16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(-16, -16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(16, -16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(16, -16), new(0, 0), BeatTime(32)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        });
                    });
                    RegisterFunctionOnce("BlurGauss", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(12), ValueEasing.EaseInCubic(0, 1.5f, BeatTime(12)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(1.5f, 0, BeatTime(8)));
                        DelayBeat(1.4f, () =>
                        {
                            builder.Run((s) => Blur.Sigma = s);
                        });
                    });
                    RegisterFunctionOnce("Pre", () =>
                    {
                        InstantSetBox(600, 60, 60);
                        rainer.Intensity = 0.18f;
                        rainer.Speed = 3f;
                        Blur.Sigma = 0.0f;
                        InstantTP(BoxStates.Centre);

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), CentreEasing.EaseOutCirc(new(450, 600), new(320, 240), BeatTime(6)));
                        builder.Run((s) =>
                        {
                            InstantTP(s);
                            InstantSetBox(s, 84, 84);
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("FakeBox2", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(-160, -120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        box2.InstantSetAlpha(0);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("FakeBox1", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(160, 120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.InstantSetAlpha(0.0f);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });

                    Action<Vector2, float> MakeLine = (delta, angle) =>
                    {
                        Vector2 centre = new(320, 240);
                        Line line = new(CentreEasing.EaseOutQuad(centre + delta, centre + delta + GetVector2(360, angle), BeatTime(16)), (s) => angle + 90);
                        line.Alpha = 0;
                        line.AlphaIncrease(BeatTime(4), 0.4f);
                        DelayBeat(8, () => line.AlphaDecrease(BeatTime(8)));
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++) line.InsertRetention(new(i * 2 + 2, 0.6f - i * 0.15f));
                    };
                    RegisterFunctionOnce("Line1", () => MakeLine(new(180, 120), -120));
                    RegisterFunctionOnce("Line2", () => MakeLine(new(-180, 120), -60));

                    string[] rhythm = { 
                    //pre
                    "", "", "Pre", "", "", "", "", "",

                    //cur
                    "RotateL(MovePosition)(FakeBox1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "#40#R1(RotateR2)(Line1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D1", "", "", "", "", "", "", "",
                    "D1", "", "", "", "", "", "", "",
                    "#52#D0(RotateL2)(FakeBox2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "#40#+11", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateR2)(Line2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(768 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("BlurGauss", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(12), ValueEasing.EaseInCubic(0, 1.5f, BeatTime(12)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(1.5f, 0, BeatTime(8)));
                        DelayBeat(1.4f, () =>
                        {
                            builder.Run((s) => Blur.Sigma = s);
                        });
                    });
                    RegisterFunctionOnce("FakeBox3", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(160, -120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        box2.InstantSetAlpha(0);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("FakeBox4", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(-160, 120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.InstantSetAlpha(0.0f);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    Action<Vector2, float> MakeLine = (delta, angle) =>
                    {
                        Vector2 centre = new(320, 240);
                        Line line = new(CentreEasing.EaseOutQuad(centre + delta, centre + delta + GetVector2(360, angle), BeatTime(16)), (s) => angle + 90);
                        line.Alpha = 0;
                        line.AlphaIncrease(BeatTime(4), 0.4f);
                        DelayBeat(8, () => line.AlphaDecrease(BeatTime(8)));
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++) line.InsertRetention(new(i * 2 + 2, 0.6f - i * 0.15f));
                    };
                    RegisterFunctionOnce("Line2", () => MakeLine(new(180, -120), 120));
                    RegisterFunctionOnce("Line1", () => MakeLine(new(-180, -120), 60));
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "#60#D0(RotateL)(FakeBox3)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D(RotateL)", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "#40#R1(RotateR)(Line1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D1(RotateR)", "", "", "", "", "", "", "",
                    "D1", "", "", "", "", "", "", "",
                    "#52#D0(RotateL2)(FakeBox4)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "#40#+11", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateR2)(Line2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    ">-10", "<+00", ">+11", "<+01", ">-10", "<+00", ">+11", "<+01",
                    "<-10", ">+00", "<+11", ">+01", "<-10", ">+00", "<+11", ">+01",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(896 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "(RotateL)R", "", "", "", "R", "", "", "R1",
                    "R", "", "R", "", "R", "", "", "",
                    "(RotateL)R", "", "", "R", "+0", "", "R1", "+01",
                    "", "R", "+0", "", "R", "", "", "",
                    "(RotateR)R1", "", "", "", "R1", "", "", "R",
                    "R1", "", "R1", "", "R1", "", "", "",
                    "(RotateR)R1", "", "", "R1", "+01", "", "R", "+0",
                    "", "R1", "+01", "R", "+0", "", "R1", "",
                    "(RotateL2)R", "", "", "", "R", "", "", "R1",
                    "R", "", "R", "", "R", "", "", "",
                    "R", "", "", "R", "+0", "", "R1", "+01",
                    "", "R", "+0", "", "R", "", "", "",
                    "(RotateR2)R1", "", "", "", "R1", "", "", "R",
                    "R1", "", "R1", "", "R1", "", "", "",
                    "R1", "", "", "R1", "+01", "", "R", "+0",
                    "R1", "+01", "R", "+0", "+0", "", "R1", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(1024 - 8))
                {
                    SetPlayerBoxMission(0);
                    float time = BeatTime(0);
                    ScreenDrawing.ScreenAngle = 3;
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.12f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.12f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s * 0.7f);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 7f;
                                splitter.Intensity = MathF.Max(1f, s * 40 + 0.19f);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutQuad(3, 0, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutQuad(0, 3f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(14, BeatTime(3.1f), false);
                    });
                    RegisterFunctionOnce("ConvulseR2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(14, BeatTime(3.1f), true);
                    });
                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1), ValueEasing.Linear(1, 0.5f, BeatTime(1)));
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.5f, 1f, BeatTime(1.5f)));
                        builder.Insert(BeatTime(0.5f), ValueEasing.Linear(1f, 0.6f, BeatTime(0.5f)));
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.5f, 1f, BeatTime(1.5f)));
                        builder.Insert(BeatTime(0.5f), ValueEasing.Linear(1f, 0.65f, BeatTime(0.5f)));
                        builder.Insert(BeatTime(4), ValueEasing.Linear(0.65f, 0, BeatTime(4)));

                        builder.Run(s =>
                        {
                            Color col = Color.Lerp(Color.Transparent, Color.White, s);
                            ScreenDrawing.ThemeColor = ScreenDrawing.UIColor = col;
                        });

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(1), ValueEasing.Linear(-2, 0, BeatTime(1)));
                        builder2.Insert(BeatTime(4), ValueEasing.Linear(4, 0, BeatTime(4)));
                        builder2.Adjust = false;
                        builder2.Run(s =>
                        {
                            ScreenDrawing.UISettings.HPShowerPos += new Vector2(0, s);
                            ScreenDrawing.UISettings.NameShowerPos += new Vector2(0, s);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.2f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.2f), true);
                    });
                    RegisterFunctionOnce("Line1", () =>
                    {
                        int count = 5;
                        Line line = new(CentreEasing.EaseOutQuart(new(0, 0), new(640, 480), BeatTime(count)), ValueEasing.Stable(-45));
                        line.Alpha = 0.5f;
                        line.Width = 3.4f;
                        line.InsertRetention(new(1, 0.5f));
                        line.InsertRetention(new(2, 0.4f));
                        line.InsertRetention(new(3, 0.3f));
                        CreateEntity(line);
                        DelayBeat(count, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        int count = 5;
                        Line line = new(CentreEasing.EaseOutQuart(new(640, 0), new(0, 480), BeatTime(count)), ValueEasing.Stable(45));
                        line.Alpha = 0.5f;
                        line.Width = 3.4f;
                        line.InsertRetention(new(1, 0.5f));
                        line.InsertRetention(new(2, 0.4f));
                        line.InsertRetention(new(3, 0.3f));
                        CreateEntity(line);
                        DelayBeat(count, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Line3", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            float count = 5.5f;
                            Func<ICustomMotion, Vector2> p1 = CentreEasing.EaseOutCubic(new(320, 480), new(320, -260), BeatTime(count));
                            Line line1 = new(CentreEasing.Combine(p1, new Vector2(-320, 240)), p1);
                            Line line2 = new(CentreEasing.Combine(p1, new Vector2(320, 240)), p1);

                            Line[] lines = { line1, line2 };
                            foreach (Line line in lines)
                            {
                                line.Alpha = 0.5f;
                                line.Width = 3.4f;
                                line.InsertRetention(new(0.5f, 0.5f));
                                line.InsertRetention(new(1, 0.4f));
                                line.InsertRetention(new(1.5f, 0.3f));
                                CreateEntity(line);
                                DelayBeat(count, () => line.Dispose());
                            }
                        });
                    });
                    RegisterFunctionOnce("Rise", () =>
                    {
                        SetBoxMission(0);
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(8), ValueEasing.EaseOutCubic(BoxStates.Centre.Y, BoxStates.Centre.Y - 15, BeatTime(8)));
                        ease.Run(s => { InstantSetBox(s, 84, 84); InstantTP(320, s); });
                    });
                    RegisterFunctionOnce("Step1", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                        ScreenDrawing.UISettings.UISurfaceDrawing production = ScreenDrawing.UISettings.CreateUISurface();
                        rainer.controlLayer = production.UISurface;

                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);

                        ValueEasing.EaseBuilder ease1 = new();
                        ease1.Insert(BeatTime(8), ValueEasing.EaseOutCirc(210, 280, BeatTime(8)));
                        ease1.Insert(BeatTime(8), ValueEasing.EaseOutElastic(280, 410, BeatTime(8)));

                        ValueEasing.EaseBuilder easeL = new();
                        easeL.Insert(BeatTime(8), ValueEasing.EaseOutCubic(320, 260, BeatTime(8)));
                        easeL.Insert(BeatTime(8), ValueEasing.EaseOutQuint(260, 140, BeatTime(8)));
                        ValueEasing.EaseBuilder easeR = new();
                        easeR.Insert(BeatTime(8), ValueEasing.EaseOutCubic(320, 380, BeatTime(8)));
                        easeR.Insert(BeatTime(8), ValueEasing.EaseOutQuint(380, 260, BeatTime(8)));

                        CentreEasing.EaseBuilder builder1 = new();
                        builder1.Insert(BeatTime(16), CentreEasing.Combine(easeL.GetResult(), ease1.GetResult()));
                        builder1.Run(s =>
                        {
                            SetPlayerBoxMission(0);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.Combine(easeR.GetResult(), ease1.GetResult()));
                        builder2.Run(s =>
                        {
                            SetPlayerBoxMission(1);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                    });
                    RegisterFunctionOnce("Step2", () =>
                    {
                        float time = BeatTime(8);

                        ScreenDrawing.MakeFlicker(Color.Black * 0.5f);
                        ValueEasing.EaseBuilder shadowOut = new();
                        shadowOut.Insert(BeatTime(4), ValueEasing.EaseOutQuad(ScreenDrawing.DownBoundDistance, 0, BeatTime(4)));
                        shadowOut.Run(s =>
                        {
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s;
                        });
                        CentreEasing.EaseBuilder ease1 = new();
                        ease1.Insert(BeatTime(8), CentreEasing.Alternate(
                            1,
                            CentreEasing.EaseOutCirc(new(260, 280), new(380, 410), time),
                            CentreEasing.Combine(ValueEasing.EaseOutCirc(260, 380, time), ValueEasing.EaseOutElastic(280, 410, time))
                        ));
                        CentreEasing.EaseBuilder ease2 = new();
                        ease2.Insert(BeatTime(8), CentreEasing.Alternate(
                            1,
                            CentreEasing.EaseOutCirc(new(380, 280), new(500, 410), time),
                            CentreEasing.Combine(ValueEasing.EaseOutCirc(380, 500, time), ValueEasing.EaseOutElastic(280, 410, time))
                        ));
                        SetPlayerBoxMission(0);
                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);
                        ease1.Run(s =>
                        {
                            SetPlayerBoxMission(2);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        SetPlayerBoxMission(1);
                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);
                        ease2.Run(s =>
                        {
                            SetPlayerBoxMission(3);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        DelayBeat(6, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(2), ValueEasing.Linear(0.5f, 0.0f, BeatTime(2)));
                            builder.Run(s =>
                            {
                                for (int i = 0; i <= 3; i++)
                                {
                                    SetPlayerBoxMission(i);
                                    BoxStates.CurrentBox.GreenSoulAlpha = s;
                                    Heart.Alpha = s * 2;
                                }
                            });
                        });
                    });
                    RegisterFunctionOnce("LineS", () =>
                    {
                        float count = 5.5f;
                        Line line1 = new(CentreEasing.EaseOutSine(new(360, 240), new(200, 240), BeatTime(7)), ValueEasing.Stable(90));
                        Line line2 = new(CentreEasing.EaseOutSine(new(280, 240), new(440, 240), BeatTime(7)), ValueEasing.Stable(90));

                        Line[] lines = { line1, line2 };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0;
                            line.Width = 3.4f;
                            line.InsertRetention(new(1, 0.4f));
                            line.AlphaIncrease(BeatTime(1f), 0.7f);
                            DelayBeat(0.51f + 2, () => line.AlphaDecrease(BeatTime(4.3f)));
                            CreateEntity(line);
                            DelayBeat(count, () => line.Dispose());
                        }
                    });
                    RegisterFunctionOnce("Line4k", () =>
                    {
                        float height = 410 - 42 - 2;
                        Line line1 = new(CentreEasing.Stable(new(0, height)), (s) =>
                        {
                            float k = s.AppearTime / BeatTime(7);
                            float scale = (1 - k) * (1 - k);
                            float angle = scale * MathF.PI / 2;
                            return new Vector2(MathF.Cos(angle) * 640f * 1.6f, -MathF.Sin(angle) * height * 1.6f) + new Vector2(0, height);
                        });
                        line1.InsertRetention(new(1, 0.5f));
                        line1.Alpha = 0.7f;
                        DelayBeat(4, () =>
                        {
                            line1.AlphaDecrease(BeatTime(3));
                        });
                        line1.TransverseMirror = true;

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 0), new(320, height), BeatTime(8)));
                        builder.Insert(BeatTime(32), CentreEasing.Stable(new(320, height)));

                        Line line2 = new(builder.GetResult(), (s) => 0);
                        line2.Alpha = 0.5f;
                        DelayBeat(4, () => line2.AlphaIncrease(BeatTime(4), 0.5f));

                        CreateEntity(line1);
                        CreateEntity(line2);

                        DelayBeat(4, () =>
                        {
                            for (int i = -2; i <= 2; i++)
                            {
                                float xPos = 320 + i * 120;
                                CentreEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(xPos, -80), new(xPos, height), BeatTime(8)));
                                Line line = new(CentreEasing.Stable(xPos, -690), builder.GetResult());
                                CreateEntity(line);
                                line.DrawingColor = Color.Silver;
                                line.Width = 3.5f + MathF.Abs(i) * 0.6f;
                            }
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "(RotateL)R", "", "", "", "R", "", "", "R1",
                    "R", "", "R1", "", "R1", "", "", "",
                    "(RotateL)R", "", "", "R", "+0", "", "R1", "+01",
                    "", "R", "+0", "", "R", "", "", "",
                    "(RotateR)R", "", "", "", "R", "", "", "R1",
                    "R", "", "R", "", "R", "", "", "",
                    "(RotateR2)R1", "", "", "R", "R1", "", "R1", "R",
                    "(RotateL2)", "R", "R1", "R", "R1", "", "R1", "Blur",
                    "(ConvulseL)(Line1)(R)(+01)", "", "(R)(+01)", "", "(R)(+011)", "", "(R)(+01)", "",
                    "(R)(+01)", "", "(R)(+01)", "", "(R)(^+01'2)", "", "(R)(+01)", "Blur",
                    "(ConvulseL)(Line2)(R)(+01)", "", "(R)(+01)", "", "(R)(+011)", "", "(R)(+01)", "",
                    "(R)(+01)", "", "(R)(+01)", "", "(R)(^+01'2)", "", "(R)(+01)", "Blur(Line3)",
                    "(ConvulseR)(Rise)(R)(+01)", "", "(R)(+01)", "+011", "(R)(^+01'2)", "", "(R)(+01)", "Blur(Line3)",
                    "(ConvulseR)(Rise)(R)(+01)", "", "(R)(+01)", "+011", "(R)(^+01'2)", "", "(R)(+01)", "Blur(Line3)(LineS)",
                    "(ConvulseR2)(Step1)", "", "", "", "", "", "", "Flicker",
                    "(ConvulseL2)(Step2)(Line4k)", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(1152 - 8))
                {
                    DelayBeat(10, Regenerate);
                    float camRotation = 48;
                    #region event
                    RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(0, 0), new(0, 60), BeatTime(8)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDelta = s);

                        cameraProduction = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);

                        ValueEasing.EaseBuilder camera = new();
                        Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending));
                        Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y);
                        Effect3D.ProjectPoint = new(0, 0, 200);

                        ValueEasing.EaseBuilder posMove = new();
                        posMove.Insert(BeatTime(8), ValueEasing.EaseOutSine(200, 170, BeatTime(8)));
                        posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                        camera.Insert(BeatTime(8), ValueEasing.EaseOutSine(0, camRotation, BeatTime(8)));
                        camera.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        SetPlayerMission(0);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                        SetPlayerMission(1);
                        Heart.RotateTo(-90);
                        Heart.FixArrow = false;
                        SetPlayerMission(2);
                        Heart.RotateTo(90);
                        Heart.FixArrow = false;
                        SetPlayerMission(3);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.02f, 0.01f, BeatTime(2.5f)));
                        builder.Run((s) =>
                        {
                            splitter.Intensity = s * 30 + 0.39f;
                        });
                    });
                    RegisterFunctionOnce("CameraZL", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, 0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, -0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation - 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation - 1.2f, camRotation, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation + 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation + 1.2f, camRotation, BeatTime(1.5f)));
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

                    float speed = 30f;
                    float scale = 2.5f;
                    float lateWaitingScale = 0.1f;
                    RegisterFunctionOnce("T0V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(0);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 0, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 2, speed, 0, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 0, 0);
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
                    RegisterFunctionOnce("Tag", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            //PlaySound(Sounds.pierce);
                        }));
                    });
                    DelayEnabled = false;
                    #endregion
                    CreateChart(0, BeatTime(1) * 8, 8.0f, new[] { 
                    //pre
                    "Move(Rotate)", "", "", "", "", "", "", "",
                    //cur
                    "T0(T1)(T2)(T3)(Tag)(CameraZL)", "T0V(T1V)(T2V)(T3V)", "T0V(T1V)(T2V)(T3V)", "T0V(T3V)",     //3
                    "!!12/3", "T0V(T3V)(T1)(T2)(Tag)", "", "",    "", "!!12/3", "(T1)(T2)", "", "",    "",  //4
                    "T1(T2)(Tag)(CameraUp)(CameraZL)", "", "", "T0(T1)(T2)(CameraUp)(CameraZR)",  //5
                    "(Tag)", "", "T1(T2)(T3)", "",  //6
                    "!!3", "(T0)(T1)(Tag)(CameraUp)", "(T2)(T3)(CameraZL)", "(T0)(T1)(CameraZR)",  //7
                    "(T2)(T3)(Tag)", "(T2V)(T3V)", "(T2V)(T3V)", "(T2V)(T3V)", //8
                    "T0(T1)(T2V)(T3V)(Tag)(CameraZL)", "", "!!8/4", "(T1)(T2)", "", "", "", //9
                    "T0(T1)(Tag)(CameraUp)", "T0V(T1V)", "T0V(T1V)(T2)(T3)(CameraZL)", "T3V(T2V)", //10
                    "!!8/4", "(T0)(T1)(T3V)(T2V)(Tag)(CameraDown)", "", "", "",    "", "T1(T2)(CameraZL)", //11
                    "(Tag)", "T2(T3)(CameraZL)", "", "", //12
                    "T2(T3)(Tag)(CameraZL)", "(T2V)(T3V)", "(T2V)(T3V)", "(T2V)(T3V)", //13
                    "T0(Tag)(T2V)(T3V)", "T1", "T2", "T3", //14
                    "!!12/3", "T0(Tag)(CameraUp)", "T1", "T2",    "T3", "", "", //15
                    "T3(Tag)", "T2", "T1", "T0", //16
                    "!!12/3", "(T3)(Tag)(CameraUp)", "T2", "T1",    "T0", "", "", //17
                    "!!8/4", "T0(Tag)", "T1", "T2", "T3",     "!!6/3", "T2", "T1", "T0", //18
                    "(T2)(T3)(Tag)(CameraZL)(CameraUp)", "T2V(T3V)", "T2V(T3V)", "T2V(T3V)",  //19
                    "!!12/3", "T2V(T3V)(T0)(Tag)(CameraUp)", "", "",   "T2V(T3V)", "!!12/3", "(T1)(T2V)(T3V)(CameraDown)", "", "",   "T2V(T3V)", //20
                    "T2V(T3V)(T0)(T1)(Tag)(CameraZL)", "", "", "T1(T3)", //21
                    "(Tag)", "", "(T2)(T3)", "", //22
                    "!!3", "T2(Tag)(T0)(CameraUp)(CameraZL)", "T2(T0)(CameraZR)", "T2(T0)(CameraZL)", //23
                    "(T1)(T3)(Tag)(CameraZL)", "(T1V)(T3V)", "(T1V)(T3V)", "T1V(T3V)", //24
                    "T0(T2)(T1V)(T3V)(Tag)", "", "!!16/4", "(T1)(T2)(CameraZL)(CameraUp)", "", "", "",    "", //25
                    "T0(T1)(Tag)", "", "(T1)(T3)", "", //26
                    "!!6/3", "T0(T3)(Tag)(CameraZL)(CameraDown)", "", "",    "", "T2(T3)", //27
                    "(Tag)", "T1(T2)", "", "", //28
                    "!!8", "T0(T1)(Tag)(CameraZR)(CameraUp)", "T2", "T3", "T0", "T1", "T2", "T3", "T1", //29
                    "T2(Tag)", "T3", "T0", "T1",  //30
                    "(T0)(T3)(Tag)(CameraZL)(CameraUp)", "T0V(T3V)", "T0V(T3V)", "",  //31
                    "T2(Tag)", "T1", "T2", "T1",  //32
                    "!!8/2", "T0(T3)(Tag)(CameraZL)(CameraUp)", "",    "", "T2(T3)", "", //33
                    "!!8/2", "T2(T0)(Tag)(CameraZL)(CameraUp)", "", "",     "!!8/2", "T0(T1)", "", "" //34

                });
                }
                if (InBeat(1280 - 8))
                {
                    float speed = 30f;
                    float scale = 2.5f;
                    float lateWaitingScale = 0.1f;
                    float camRotation = 48;
                    #region event
                    RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(0, 0), new(0, 60), BeatTime(8)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDelta = s);

                        RenderProduction production4 = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(production4);

                        ValueEasing.EaseBuilder camera = new();
                        Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending));
                        Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y);
                        Effect3D.ProjectPoint = new(0, 0, 200);
                        camera.Insert(BeatTime(8), ValueEasing.EaseOutSine(0, camRotation, BeatTime(8)));
                        camera.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        SetPlayerMission(0);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                        SetPlayerMission(1);
                        Heart.RotateTo(-90);
                        Heart.FixArrow = false;
                        SetPlayerMission(2);
                        Heart.RotateTo(90);
                        Heart.FixArrow = false;
                        SetPlayerMission(3);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                    });
                    RegisterFunctionOnce("CameraZL", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, 0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, -0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation - 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation - 1.2f, camRotation, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation + 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation + 1.2f, camRotation, BeatTime(1.5f)));
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
                    RegisterFunctionOnce("T0V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(0);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 0, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 2, speed, 0, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 0, 0);
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
                    RegisterFunctionOnce("Tag", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            //PlaySound(Sounds.pierce);
                        }));
                    });
                    DelayEnabled = false;
                    #endregion
                    CreateChart(0, BeatTime(1) * 8, 8.0f, new[] {
                    //pre
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "T0(T1)(T2)(T3)(CameraDown)(CameraZL)", "T0V(T1V)(T2V)(T3V)", "T0V(T1V)(T2V)(T3V)", "T0V(T3V)",    //35
                    "!!8/2", "T0V(T3V)(CameraZL)(T1)(T2)", "",     "", "!!8/2", "(T1)(T2)", "",    "",    //36
                    "T0(T3)", "!!8/4", "", "T1", "T2", "T3",    "!!12/3", "T2(CameraZR)", "T1", "T0",   //37
                    "", "!!12/3", "T1(CameraZR)", "T2", "T3",     "", "",    //38
                    "!!3", "T1(T2)(CameraZL)(CameraUp)", "T1(T2)(CameraZR)", "T1(T2)(CameraZL)",    //39
                    "T0(T3)(CameraDown)", "T0V(T3V)", "T0V(T3V)", "T0V(T3V)",    //40
                    "T0V(T3V)(T1)(T2)(CameraZL)", "", "!!16/4", "CameraZL(CameraDown)(T3)(T1)", "", "", "",    "",    //41
                    "T0(T2)(CameraZR)", "", "T1(T2)", "",    //42
                    "T0(T1)(T2)(T3)(CameraUp)", "T0V(T1V)(T2V)(T3V)", "", "T2(T1)",    //43
                    "", "T0(CameraZL)(T3)", "", "",    //44
                    "!!8/2", "T0(T1)(CameraZL)", "",   "", "", "",    //45
                    "T3", "T0", "T3", "T0",    //46
                    "(CameraDown)(T0)T3", "T0V(T3V)", "T0V(T3V)", "T0V(T3V)",    //47
                    "T1(T3V)(T0V)", "T2", "T1", "T2",    //48
                    "!!6", "(CameraZR)T0(T3)", "", "", "T2(T1)", "", "",    //49
                    "!!16/4", "CameraUp(T1)", "T2", "T3", "",     "",   "!!16/4", "CameraUp(T2)", "T1", "T0", "",     "",    //50
                    "CameraZL(CameraDown)(T2)(T3)(T1)(T0)", "(T2V)T3V", "(T2V)T3V", "(T2V)T3V",    //51
                    "!!8/2", "(T2V)T3V(T0)", "",   "(T2V)T3V",   "!!8/2", "(T2V)T3V(T1)", "",   "(T2V)T3V",    //52
                    "T0(T1)(T2V)(T3V)(CameraZL)", "", "", "T0(T3)(CameraZR)",    //53
                    "T1", "T0(T3)", "T2", "",    //54
                    "!!3", "(CameraZL)(CameraUp)(T1)(T3)", "(CameraZR)(T1)(T3)", "(CameraZL)(T1)(T3)",    //55
                    "CameraZL(CameraDown)(T0)(T2)", "T2V(T0V)", "T2V(T0V)", "T2V(T0V)",    //56
                    "T2V(T0V)(T1)(T3)(CameraZL)", "", "!!16/4", "(T0)(T2)(CameraZL)", "", "", "",     "",    //57
                    "(CameraZL)T1(T3)", "", "T1(T3)", "",    //58
                    "(CameraZL)(CameraDown)(T0)(T1)(T3)", "", "", "(CameraZR)(T1)(T2)(T3)",    //59
                    "", "T1(T2)(T0)(CameraZL)", "", "",    //60 
                    "T0(T1)(T3)(CameraZL)(CameraDown)", "", "T1", "",    //61
                    "T0(T2)(T3)(CameraZL)(CameraDown)", "", "T2", "T1",    //62 
                    "T0(T3)(CameraZR)(CameraUp)", "", "", "",    //63 
                });

                }
                if (InBeat(1408 - 16 - 1))
                {
                    DelayEnabled = true;

                    DelayBeat(12, Regenerate);

                    RegisterFunctionOnce("Shatter", () =>
                    {
                        DelayBeat(0.875f, () =>
                        {
                            // sinwave shake
                            ScreenDrawing.Shaders.Filter filter;
                            ScreenDrawing.SceneRendering.InsertProduction(filter = new ScreenDrawing.Shaders.Filter(SinWave, 0.6755f));
                            DelayBeat(15, () =>
                            {
                                filter.Dispose();
                                production1.Dispose(); production2.Dispose();
                                ScreenDrawing.UpExtending = 0;
                                cameraProduction.Dispose();
                                Blur.Dispose();
                                ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                                Line[] lines = GetAll<Line>();
                                foreach (Line line in lines)
                                {
                                    line.Dispose();
                                }
                            });
                            float a = 0;
                            ForBeat(15, () =>
                            {
                                SinWave.Parameters["time"].SetValue(a); a += 0.25f;
                                SinWave.Parameters["time2"].SetValue(a * 1.5f);
                            });

                            ValueEasing.EaseBuilder rgbShake = new();
                            rgbShake.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0, 4, BeatTime(1.5f)));
                            rgbShake.Insert(BeatTime(14.5f), ValueEasing.EaseOutCubic(4, 0, BeatTime(14.5f)));
                            rgbShake.Run(s =>
                            {
                                SinWave.Parameters["frequency"].SetValue(s * 10f + 140f);
                                SinWave.Parameters["frequency2"].SetValue(s * 25f + 200f);
                                SinWave.Parameters["range2"].SetValue(s);
                                SinWave.Parameters["range"].SetValue(s * 1.25f);
                            });

                            // blur effect
                            ValueEasing.EaseBuilder blur = new();
                            blur.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0.0f, 0.2f, BeatTime(16)));
                            blur.Run(s =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                Blur.Sigma = s * 5;
                            });

                            // camera effect
                            ValueEasing.EaseBuilder camera = new();
                            camera.Insert(BeatTime(14), ValueEasing.EaseOutCirc(0.0f, 0.2f, BeatTime(14)));
                            camera.Run(s =>
                            {
                                ScreenDrawing.ScreenAngle = s * 52;
                            });

                            // flicker 
                            ScreenDrawing.MakeFlicker(Color.White * 0.55f);
                            ScreenDrawing.SceneOutScale = 1.6f;
                            DelayBeat(3, () => ScreenDrawing.SceneOut(Color.Black, BeatTime(11.5f)));

                            // set chart
                            DelayBeat(15.1f, () =>
                            {
                                for (int i = 3; i > 0; i--)
                                {
                                    SetPlayerMission(i);
                                    Heart.Dispose();
                                }
                                SetPlayerMission(0);
                                InstantTP(new(320, 240));
                                InstantSetGreenBox();
                                ScreenDrawing.ScreenAngle = 0;
                                Heart.InstantSetRotation(0);
                                Heart.Alpha = 1.0f;
                                BoxStates.CurrentBox.GreenSoulAlpha = 0.5f;
                                rainer.Intensity = 0.15f;
                                ScreenDrawing.BoundColor = Color.White;
                                ScreenDrawing.ThemeColor = Color.White;
                                ScreenDrawing.UIColor = Color.White;
                                ScreenDrawing.UISettings.RemoveUISurface();
                                ScreenDrawing.UISettings.NameShowerPos = new Vector2(20, 457);
                                ScreenDrawing.UISettings.HPShowerPos = new(320, 443);
                            });

                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 4, BeatTime(16)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseInQuad(4, 2, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(2, 0, BeatTime(8)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 4, BeatTime(16)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseInQuad(4, 2, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(2, 0, BeatTime(8)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = -s;
                        });
                    });
                    string[] rhythm = { 
                    //pre 
                    "Shatter", "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    //cur 
                    "RotateL", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateR", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateL", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateR", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    string[] rhythm2 = { 
                    //pre 
                    "",
                    //cur
                    "$0", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#5#D1(D)", "", "", "", "", "", "", "",
                    "#5#D(D1)", "", "", "", "", "", "", "",
                    "#3#D1(D)", "", "", "", "", "", "#4#D(D1)", "",
                    "", "", "", "", "#17#D1(D)(D)", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1(D0)", "", "", "",
                    "#13#D0(D1)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#5#D1(D)", "", "", "", "", "", "", "",
                    "#5#D(D1)", "", "", "", "", "", "", "",
                    "#3#D1(D)", "", "", "", "", "", "#23#D(D1)(D1)", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1(D0)", "", "", "",
                };
                    CreateChart(0, BeatTime(1) * 8, 6.0f, rhythm);
                    DelayBeat(9 + 7, () =>
                    {
                        CreateChart(0, BeatTime(1) * 8, 6.0f, rhythm2);
                    });
                }
            }
            /// <summary>
            /// IT'S NORMAL BARRAGE!!!
            /// </summary>
            public void Easy()
            {
                if (InBeat(64 - 8))
                {
                    RegisterFunctionOnce("Move", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;
                        heart1.InstantSplit(new(new(460, -80), new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.GreenSoulAlpha = 0.3f;

                        Vector2 delta = new(140, -320), centre = new(320, 240);

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutElastic(centre - delta, centre, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            SetPlayerBoxMission(heart1);
                            BoxStates.CurrentBox.GreenSoulAlpha = 0.5f;
                            InstantTP(BoxStates.Centre);
                            InstantSetBox(s, 84, 84);
                        });
                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(8), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(8)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantTP(BoxStates.Centre);
                            InstantSetBox(s, 84, 84);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        DelayBeat(5, () =>
                        {
                            builder3.Insert(BeatTime(3), ValueEasing.EaseOutCirc(0.3f, 0, BeatTime(4)));
                            builder3.Run((s) => box2.GreenSoulAlpha = s);
                        });
                        DelayBeat(8.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("LineUp2", () =>
                    {
                        Line line1 = new(new(90, 0), BoxStates.CurrentBox.CollidingBox.TopLeft);
                        Line line2 = new(new(550, 0), BoxStates.CurrentBox.CollidingBox.TopRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.5f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineDown2", () =>
                    {
                        Line line1 = new(new(90, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft);
                        Line line2 = new(new(550, 480), BoxStates.CurrentBox.CollidingBox.BottomRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.5f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineUp", () =>
                    {
                        Line line1 = new(new(120, 0), BoxStates.CurrentBox.CollidingBox.TopLeft);
                        Line line2 = new(new(520, 0), BoxStates.CurrentBox.CollidingBox.TopRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.7f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("LineDown", () =>
                    {
                        Line line1 = new(new(120, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft);
                        Line line2 = new(new(520, 480), BoxStates.CurrentBox.CollidingBox.BottomRight);
                        Line[] lines = { line1, line2 };
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(4), ValueEasing.Linear(0.7f, 0.0f, BeatTime(4)));
                        ease.Run((s) =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = s;
                            }
                        });
                        DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("Cross", () =>
                    {
                        CentreEasing.EaseBuilder centre = new();
                        ValueEasing.EaseBuilder rotation = new();
                        centre.Insert(BeatTime(8), CentreEasing.EaseOutCirc(new(320, 240), new(320, 150), BeatTime(8)));
                        rotation.Insert(BeatTime(8), ValueEasing.Linear(0, 45, BeatTime(8)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutQuint(new(320, 150), new(240, 150), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, -180, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutQuint(new(240, 150), new(400, 150), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 180, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutExpo(new(400, 150), new(400, 330), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 90, BeatTime(32)));

                        centre.Insert(BeatTime(32), CentreEasing.EaseOutExpo(new(400, 330), new(240, 330), BeatTime(32)));
                        rotation.Insert(BeatTime(32), ValueEasing.EaseOutBack(0, 90, BeatTime(32)));

                        centre.Insert(BeatTime(4), CentreEasing.EaseOutCirc(new(240, 330), new(320, 240), BeatTime(4)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(45, -45, BeatTime(8)));

                        centre.Insert(BeatTime(96 - 4), CentreEasing.Stable(320, 240));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -5, BeatTime(2)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseInQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseOutQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(7.5f), ValueEasing.Stable(0));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(45, 90, BeatTime(6)));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(90, 135, BeatTime(6)));

                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(45, -45, BeatTime(8)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -5, BeatTime(2)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseInQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(1.25f), ValueEasing.EaseOutQuad(0, 2.5f, BeatTime(1.25f)));
                        rotation.Insert(BeatTime(7.5f), ValueEasing.Stable(0));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(45, 90, BeatTime(6)));
                        rotation.Insert(BeatTime(6), ValueEasing.EaseOutBack(90, 135, BeatTime(6)));

                        rotation.Insert(BeatTime(5.85f), ValueEasing.EaseOutBack(45, -45, BeatTime(6)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -4, BeatTime(2)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, 8, BeatTime(2)));
                        rotation.Insert(BeatTime(2), ValueEasing.EaseOutQuad(0, -4, BeatTime(1.85f)));

                        rotation.Insert(BeatTime(4), ValueEasing.EaseOutExpo(0, 60, BeatTime(4)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(0, 60, BeatTime(8)));
                        rotation.Insert(BeatTime(8), ValueEasing.EaseOutBack(0, 60, BeatTime(8)));
                        rotation.Insert(BeatTime(0.15f), ValueEasing.Stable(0));

                        centre.Insert(BeatTime(16), CentreEasing.EaseOutQuad(new(320, 240), new(320, 80), BeatTime(16)));
                        rotation.Insert(BeatTime(16), ValueEasing.EaseOutBack(-45, 225, BeatTime(16)));
                        centre.Insert(BeatTime(16), CentreEasing.EaseOutQuad(new(320, 240), new(320, 80), BeatTime(16)));
                        rotation.Insert(BeatTime(16), ValueEasing.EaseOutBack(225, -45, BeatTime(16)));

                        Line line1 = new(centre.GetResult(), rotation.GetResult());
                        Line line2 = new(centre.GetResult(), ValueEasing.Combine(rotation.GetResult(), 90));
                        Line[] lines = { line1, line2 };

                        DelayBeat(256 + 8 + 16, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Dispose();
                            }
                        });
                        DelayBeat(240 + 8, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(14), 0.419f);
                            }
                        });
                        DelayBeat(240 + 8 + 14, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(6));
                            }
                        });

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutCubic(0, 0.42f, BeatTime(16)));

                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.Alpha = 0.00f;
                            builder.Run(s => line.Alpha = s);

                            for (int i = 0; i < 6; i++)
                            {
                                int t = i;
                                DelayBeat(i * 16 + 8, () =>
                                {
                                    line.InsertRetention(new(t * 3 + 3, 0.32f - t * 0.03f));
                                });
                            }
                        }
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(0f, -4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "Move", "", "", "", "", "", "", "(Blur)",

                    //cur
                    "(R)(LineUp)(RotateL)", "", "", "", "(+1)(LineDown)", "", "", "",
                    "(+1)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "(Blur)",
                    "(+1)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "",
                    "(+1)(LineUp)", "", "", "", "(+1)(LineDown)", "", "", "(Blur)",
                    "(+1)(LineUp)(RotateR)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                    "(+1)(LineUp)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "(Blur)",
                    "(+1)(LineUp)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                    "(+1)(LineUp)(Cross)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(128 - 16))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.06f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(12.1f), ValueEasing.EaseOutQuad(0.06f, 0.02f, BeatTime(12.1f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("Blur2", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(12.1f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(12.1f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, -4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 4f, BeatTime(16f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutExpo(ScreenDrawing.ScreenAngle, 0f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("Up", () =>
                    {
                        ScreenDrawing.BoundColor = Color.Gray;
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutElastic(0, 140f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(4f), ValueEasing.EaseOutSine(0, 1f, BeatTime(4f)));
                        DelayBeat(4, () =>
                        {
                            builder2.Run((s) =>
                            {
                                ScreenDrawing.BoundColor = Color.Lerp(Color.Gray, Color.Silver, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("MovePosition", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(16, 16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(16, 16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(-16, 16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(-16, 16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(-16, -16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(-16, -16), new(0, 0), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(16, -16), BeatTime(16)));
                        builder.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(16, -16), new(0, 0), BeatTime(16)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        });
                    });
                    RegisterFunctionOnce("Black", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black, BeatTime(3));
                        DelayBeat(0.9f, () =>
                        {
                            rainer.Speed = 5f;
                            rainer.Intensity = 0.5f;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "(Blur)",

                    //cur
                    "D(RotateL)(MovePosition)", "", "+1", "", "+1", "", "+1", "",
                    "+1", "", "+1", "", "+1", "", "+1", "(Blur2)",
                    "(+1)", "", "+1", "", "+1", "", "+1", "",
                    "(+1)", "", "+1", "", "+1", "", "+1", "(Blur)",
                    "+1(RotateR)", "", "+1", "", "+1", "", "+1", "",
                    "+1", "", "+1", "", "+1", "", "+1", "(Blur2)",
                 //   "D1", "", "D1", "", "D1", "(Up)", "D1", "",
                  //  "(RotateMid)(Black)", "", "", "",  
                    "(D)", "", "+1", "", "+1", "", "+1", "",
                    "(D)", "", "+1", "", "+1", "", "+1", "(Blur)",
                    "D(RotateL)", "", "+1", "", "+1", "", "+1", "",
                    "D", "", "+1", "", "+1", "", "+1", "(Blur2)",
                    "(D)", "", "+1", "", "+1", "", "+1", "",
                    "(D)", "", "+1", "", "+1", "", "+1", "(Blur)",
                    "D(RotateR)", "", "+1", "", "+1", "", "+1", "",
                    "D", "", "+1", "", "+1", "", "+1", "(Blur2)",
                    "D", "", "D", "", "D", "(Up)", "D", "",
                    "(RotateMid)(Black)", "", "", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(256 - 5))
                {
                    RegisterFunctionOnce("BlurLong", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.26f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(14), ValueEasing.EaseOutQuad(0.26f, 0.02f, BeatTime(14)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.09f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.09f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), true);
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(15, BeatTime(16), false);
                        ScreenDrawing.CameraEffect.SizeShrink(7, BeatTime(16));
                    });
                    RegisterFunctionOnce("ConvulseR2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(15, BeatTime(16), true);
                        ScreenDrawing.CameraEffect.SizeShrink(7, BeatTime(16));
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "(BlurLong)",

                    //cur 
                    "(#16#$00)(#16#$21)(ConvulseL)(ConvulseL2)", ">^$00", "<^$21", "<^$00", ">^$21", ">^$00", "<^$21", "<^$00",
                    ">^$21", ">^$00", "<^$21", "<^$00", ">^$21", ">^$00", "<^$21", "<^$00",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#16#$01)(#16#$20)(ConvulseR)(ConvulseR2)", ">^$01", "<^$2", "<^$01", ">^$2", ">^$01", "<^$2", "<^$01",
                    ">^$2", ">^$01", "<^$2", "<^$01", ">^$2", ">^$01", "<^$2", "<^$01",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#10#$00)(#10#$21)(ConvulseL)(ConvulseL2)", ">^$00", "<^$21", "<^$00", ">^$21", ">^$00", "<^$21", "<^$00",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "Blur",
                    "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "", "", "Blur",
                    "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "", "", "", "", "(BlurLong)",
                    "(#13#$31)(#13#$10)(ConvulseR)(ConvulseR2)", ">^$31", "<^$10", "<^$31", ">^$10", ">^$31", "<^$10", "<^$31",
                    ">^$10", ">^$31", "<^$10", "<^$31", ">^$10", ">^$31", "", "(BlurLong)",
                    "(#13#$11)(#13#$30)(ConvulseL)(ConvulseL2)", ">^$11", "<^$30", "<^$11", ">^$30", ">^$11", "<^$30", "<^$11",
                    ">^$30", ">^$11", "<^$30", "<^$11", ">^$30", ">^$11", "", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(384 - 8))
                {
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });

                    RegisterFunctionOnce("Down", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(32), ValueEasing.EaseInElastic(140f, 110f, BeatTime(32)));
                        builder.Insert(BeatTime(32), ValueEasing.EaseOutElastic(110f, 80f, BeatTime(32)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                    });

                    float delta = 0;
                    RegisterFunctionOnce("Up", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        ValueEasing.EaseBuilder builder2 = new();
                        float v = ScreenDrawing.UpBoundDistance;
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCubic(0 + v, 36f + v, BeatTime(2)));
                        builder.Insert(BeatTime(1.6f), ValueEasing.EaseInCubic(36f, 0.0f, BeatTime(1.6f)));

                        builder.Run((s) =>
                        {
                            ScreenDrawing.UpBoundDistance = s;
                            ScreenDrawing.DownBoundDistance = s;
                        });
                        builder2.Insert(BeatTime(1), ValueEasing.EaseOutCirc(0 + delta, 0.04f + delta, BeatTime(1)));
                        builder2.Insert(BeatTime(2.6f), ValueEasing.EaseInCirc(0.04f, 0.011f, BeatTime(2.6f)));
                        delta += 0.011f;

                        builder2.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.Silver, Color.Red, s);
                        });
                    });

                    RegisterFunctionOnce("LineLRev", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(640, 240), new(320, 240), BeatTime(4)));
                        builder.Insert(BeatTime(2.5f), CentreEasing.EaseOutQuad(new(320, 240), new(640, 240), BeatTime(2.5f)));
                        Line line = new(builder.GetResult(), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3 + 2.5f, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5 + 2.5f, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineRRev", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(0, 240), new(320, 240), BeatTime(4)));
                        builder.Insert(BeatTime(2.5f), CentreEasing.EaseOutQuad(new(320, 240), new(0, 240), BeatTime(2.5f)));
                        Line line = new(builder.GetResult(), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(1, 0.4f));
                        line.InsertRetention(new(2, 0.3f));
                        line.InsertRetention(new(3, 0.2f));
                        CreateEntity(line);
                        DelayBeat(3 + 2.5f, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5 + 2.5f, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.06f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.06f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 30 + 0.35f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(2), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(2), true);
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "Blur", 
                    //cur
                    "ConvulseL(R)(LineL)(Down)", "", "R", "", "ConvulseL(R)(LineL)", "", "R", "Blur",
                    "ConvulseL(R)(LineL)", "", "R", "", "ConvulseL(R)(LineL)", "", "R", "Blur",
                    "ConvulseL(R)(LineL)", "", "R", "", "ConvulseL(R)(LineL)", "", "R", "Blur",
                    "ConvulseL(R)(LineL)", "", "R", "", "ConvulseL(R)(LineLRev)", "", "R", "Blur",
                    "ConvulseR(R)(LineR)", "", "R", "", "ConvulseR(R)(LineR)", "", "R", "Blur",
                    "ConvulseR(R)(LineR)", "", "R", "", "ConvulseR(R)(LineR)", "", "R", "Blur",
                    "ConvulseR(R)(LineR)", "", "R", "", "ConvulseR(R)(LineR)", "", "R", "Blur",
                    "ConvulseR(R)(LineR)", "", "R", "", "ConvulseR(R)(LineRRev)", "", "R", "Blur",
                    "ConvulseL(R1)(LineL)(Up)", "", "R1", "", "ConvulseL(R1)(LineL)(Up)", "", "R1", "Blur",
                    "ConvulseL(R1)(LineL)(Up)", "", "R1", "", "ConvulseL(R1)(LineL)(Up)", "", "R1", "Blur",
                    "ConvulseL(R1)(LineL)(Up)", "", "R1", "", "ConvulseL(R1)(LineL)(Up)", "", "R1", "Blur",
                    "ConvulseL(R1)(LineL)(Up)", "", "R1", "", "ConvulseL(R1)(LineLRev)(Up)", "", "R1", "Blur",
                    "ConvulseR(R1)(LineR)(Up)", "", "R1", "", "ConvulseR(R1)(LineR)(Up)", "", "R1", "Blur",
                    "ConvulseR(R1)(LineR)(Up)", "", "R1", "", "ConvulseR(R1)(LineR)(Up)", "", "R1", "Blur",
                    "ConvulseR(R1)(LineR)(Up)", "", "R1", "", "ConvulseR(R1)(LineR)(Up)", "", "R1", "Blur",
                    "ConvulseR(R1)(LineR)(Up)", "", "R1", "", "ConvulseR(R1)(LineRRev)(Up)", "", "R1", "Blur",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }

                }
                if (InBeat(512 - 8 - 4))
                {
                    RegisterFunctionOnce("LineL2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.42f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.419f));
                        DelayBeat(5, () => line.Dispose());
                    });

                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(1.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("BlurLong", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.26f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(14), ValueEasing.EaseOutQuad(0.26f, 0.02f, BeatTime(14)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("BlurLong2", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.36f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(24), ValueEasing.EaseOutQuad(0.36f, 0.02f, BeatTime(24)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                splitter.Intensity = MathF.Max(1f, s * 20 + 0.5f);
                            });
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.6f), true);
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(8), false);
                        ScreenDrawing.CameraEffect.SizeShrink(5, BeatTime(8));
                    });

                    RegisterFunctionOnce("SceneOut", () =>
                    {
                        DelayBeat(8, () =>
                        {
                            ScreenDrawing.SceneOut(Color.Black * 0.98f, BeatTime(8f - 1));
                        });
                        for (int i = 0; i < 7; i++)
                        {
                            int t = i;
                            DelayBeat(t, () =>
                            {
                                ScreenDrawing.MakeFlicker(Color.Black * (t * 0.06f + 0.4f));
                            });
                            if (i >= 4)
                            {
                                DelayBeat(t + 0.5f, () =>
                                {
                                    ScreenDrawing.MakeFlicker(Color.Black * (t * 0.06f + 0.4f));
                                });
                            }
                        }

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseInElastic(0.0f, 0.5f, BeatTime(8)));
                        builder.Insert(BeatTime(24), ValueEasing.EaseOutCubic(0.5f, 1.0f, BeatTime(24)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.Silver, Color.Gray * 0.5f, s);
                        });
                    });

                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, -3f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 3f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 0f, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });

                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur  
                    "(D)(+01)RotateL", "", "(D)(+01)", "", "(D)(+01)RotateR", "", "(D)(+01)", "",
                    "RotateMid", "", "", "", "", "", "", "(Blur)",
                    "LineL(ConvulseR)", "", "", "(Blur)", "LineL(ConvulseR)", "", "", "(Blur)",
                    "LineL(ConvulseR)", "", "", "(Blur)", "LineL(ConvulseR)", "", "", "",

                    "(D)(+01)RotateR", "", "(D)(+01)", "", "(D)(+01)RotateL", "", "(D)(+01)", "",
                    "RotateMid", "", "", "", "", "", "", "(Blur)",
                    "LineR(ConvulseL)", "", "", "(Blur)", "LineR(ConvulseL)", "", "", "(Blur)",
                    "LineR(ConvulseL)", "", "", "(Blur)", "LineR(ConvulseL)", "", "", "(Blur)",

                    "(R)(+01)(ConvulseL)LineL", "", "(R)(+01)", "(Blur)", "(R)(+01)(ConvulseR)LineR", "", "(R)(+01)", "(Blur)",
                    "(R)(+01)(ConvulseL)LineL", "", "(R)(+01)", "(Blur)", "(R)(+01)(ConvulseR)LineR", "", "(R)(+01)", "(Blur)",
                    "(R)(+01)(ConvulseL)LineL", "", "(R)(+01)LineR", "(Blur)", "(R)(+01)(ConvulseR)LineL", "", "(R)(+01)LineR", "(Blur)",
                    "(R)(+01)(ConvulseL)LineL", "", "(R)(+01)LineR", "(Blur)", "(R)(+01)(ConvulseR)LineL", "", "(R)(+01)LineR", "(Blur)",

                    "R(LineL2)(ConvulseL)", "+0(LineL2)(Blur)", "R(LineR2)", "+0(LineR2)(Blur)", "R(LineL2)(ConvulseL)", "+0(LineL2)(Blur)","R(LineR2)", "+0(LineR2)(Blur)",
                    "R(LineL2)(ConvulseR)", "+0(LineL2)(Blur)", "R(LineR2)", "+0(LineR2)(Blur)", "R(LineL2)(ConvulseR)", "+0(LineL2)(Blur)","+0(LineR2)", "(LineR2)(BlurLong2)",
                    "SceneOut(#8#$00)(#8#$21)(ConvulseL2)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    float time = BeatTime(4);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(640 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("MovePosition", () =>
                    {
                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(16, 16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(16, 16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(-16, 16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(-16, 16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(-16, -16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(-16, -16), new(0, 0), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutBack(new(0, 0), new(16, -16), BeatTime(32)));
                        builder.Insert(BeatTime(32), CentreEasing.EaseOutSine(new(16, -16), new(0, 0), BeatTime(32)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDelta = s;
                        });
                    });
                    RegisterFunctionOnce("BlurGauss", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(12), ValueEasing.EaseInCubic(0, 1.5f, BeatTime(12)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(1.5f, 0, BeatTime(8)));
                        DelayBeat(1.4f, () =>
                        {
                            builder.Run((s) => Blur.Sigma = s);
                        });
                    });
                    RegisterFunctionOnce("Pre", () =>
                    {
                        InstantSetBox(600, 60, 60);
                        rainer.Intensity = 0.18f;
                        rainer.Speed = 3f;
                        Blur.Sigma = 0.0f;
                        InstantTP(BoxStates.Centre);

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), CentreEasing.EaseOutCirc(new(450, 600), new(320, 240), BeatTime(6)));
                        builder.Run((s) =>
                        {
                            InstantTP(s);
                            InstantSetBox(s, 84, 84);
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("FakeBox2", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(-160, -120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        box2.InstantSetAlpha(0);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("FakeBox1", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(160, 120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.InstantSetAlpha(0.0f);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });

                    Action<Vector2, float> MakeLine = (delta, angle) =>
                    {
                        Vector2 centre = new(320, 240);
                        Line line = new(CentreEasing.EaseOutQuad(centre + delta, centre + delta + GetVector2(360, angle), BeatTime(16)), (s) => angle + 90);
                        line.Alpha = 0;
                        line.AlphaIncrease(BeatTime(4), 0.4f);
                        DelayBeat(8, () => line.AlphaDecrease(BeatTime(8)));
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++) line.InsertRetention(new(i * 2 + 2, 0.6f - i * 0.15f));
                    };
                    RegisterFunctionOnce("Line1", () => MakeLine(new(180, 120), -120));
                    RegisterFunctionOnce("Line2", () => MakeLine(new(-180, 120), -60));

                    string[] rhythm = { 
                    //pre
                    "", "", "Pre", "", "", "", "", "",

                    //cur
                    "RotateL(MovePosition)(FakeBox1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "#40#R1(RotateR2)(Line1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "#52#D0(RotateL2)(FakeBox2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "#40#+11", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateR2)(Line2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(768 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("BlurGauss", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(12), ValueEasing.EaseInCubic(0, 1.5f, BeatTime(12)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(1.5f, 0, BeatTime(8)));
                        DelayBeat(1.4f, () =>
                        {
                            builder.Run((s) => Blur.Sigma = s);
                        });
                    });
                    RegisterFunctionOnce("FakeBox3", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(160, -120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        box2.InstantSetAlpha(0);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("FakeBox4", () =>
                    {
                        Player.Heart heart1 = Heart;
                        FightBox box1 = heart1.controlingBox;

                        Vector2 centre = new(320, 240), delta = new(-160, 120);
                        heart1.InstantSplit(new(centre + delta, new(84, 84)));
                        Player.Heart heart2 = Heart;
                        FightBox box2 = heart2.controlingBox;
                        BoxStates.CurrentBox.InstantSetAlpha(0.0f);
                        heart2.Alpha = 0;

                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.EaseOutCirc(centre + delta, centre, BeatTime(16)));
                        builder2.Run((s) =>
                        {
                            SetPlayerBoxMission(heart2);
                            InstantSetBox(s, 84, 84);
                            InstantTP(BoxStates.Centre);
                        });
                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(10), ValueEasing.EaseOutQuad(0f, 0.3f, BeatTime(10)));
                        builder3.Insert(BeatTime(6), ValueEasing.Linear(0.3f, 0, BeatTime(6)));
                        builder3.Run((s) =>
                        {
                            heart2.Alpha = s * 2;
                            box2.InstantSetAlpha(s);
                        });

                        DelayBeat(16.5f, () => heart2.Dispose());
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    Action<Vector2, float> MakeLine = (delta, angle) =>
                    {
                        Vector2 centre = new(320, 240);
                        Line line = new(CentreEasing.EaseOutQuad(centre + delta, centre + delta + GetVector2(360, angle), BeatTime(16)), (s) => angle + 90);
                        line.Alpha = 0;
                        line.AlphaIncrease(BeatTime(4), 0.4f);
                        DelayBeat(8, () => line.AlphaDecrease(BeatTime(8)));
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++) line.InsertRetention(new(i * 2 + 2, 0.6f - i * 0.15f));
                    };
                    RegisterFunctionOnce("Line2", () => MakeLine(new(180, -120), 120));
                    RegisterFunctionOnce("Line1", () => MakeLine(new(-180, -120), 60));
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "#40#D(RotateL)(FakeBox3)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D1(RotateL)", "", "", "", "", "", "", "",
                    "D1", "", "", "", "", "", "", "",
                    "#40#R1(RotateR)(Line1)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    "D(RotateR)", "", "", "", "", "", "", "",
                    "D", "", "", "", "", "", "", "",
                    "#52#D0(RotateL2)(FakeBox4)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "#40#+11", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateR2)(Line2)", "", "", "", "", "", "", "",
                    "BlurGauss", "", "", "", "", "", "", "",
                    ">-10", "<+00", ">+11", "<+01", ">-10", "<+00", ">+11", "<+01",
                    "<-10", ">+00", "<+11", ">+01", "<-10", ">+00", "<+11", ">+01",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(896 - 8))
                {
                    float time = BeatTime(0);

                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.05f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.05f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(-1f, -6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(6), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 1f + ScreenDrawing.ScreenAngle, BeatTime(6)));
                        builder.Insert(BeatTime(26), ValueEasing.EaseOutQuad(1f, 6f, BeatTime(26)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "(RotateL)R", "", "", "", "R", "", "", "D",
                    "+0", "", "R", "", "R", "", "", "",
                    "(RotateL)R1", "", "", "R", "+0", "", "R", "+0",
                    "", "R", "+0", "", "R", "", "", "",
                    "(RotateR)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "(RotateR)R1", "", "", "R", "+0", "", "R", "+0",
                    "", "R", "+0", "R", "+0", "", "R", "",
                    "(RotateL2)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "R", "", "", "R", "+0", "", "R", "+0",
                    "", "R", "+0", "", "R", "", "", "",
                    "(RotateR2)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "R", "", "", "R", "+0", "", "R", "+0",
                    "R", "+0", "R", "+0", "+0", "", "R", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(1024 - 8))
                {
                    SetPlayerBoxMission(0);
                    float time = BeatTime(0);
                    ScreenDrawing.ScreenAngle = 3;
                    RegisterFunctionOnce("Blur", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(0.02f, 0.12f, BeatTime(0.5f)));
                            builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.12f, 0.02f, BeatTime(2.5f)));
                            builder.Run((s) =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s * 0.7f);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 7f;
                                splitter.Intensity = MathF.Max(1f, s * 40 + 0.19f);
                            });
                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, -0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(-0.5f, -3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(3f), ValueEasing.EaseInQuad(0f + ScreenDrawing.ScreenAngle, 0.5f + ScreenDrawing.ScreenAngle, BeatTime(3f)));
                        builder.Insert(BeatTime(13f), ValueEasing.EaseOutQuad(0.5f, 3f, BeatTime(13f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutQuad(3, 0, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8f), ValueEasing.EaseOutQuad(0, 3f, BeatTime(8f)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("ConvulseL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(14, BeatTime(3.1f), false);
                    });
                    RegisterFunctionOnce("ConvulseR2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(14, BeatTime(3.1f), true);
                    });
                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1), ValueEasing.Linear(1, 0.5f, BeatTime(1)));
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.5f, 1f, BeatTime(1.5f)));
                        builder.Insert(BeatTime(0.5f), ValueEasing.Linear(1f, 0.6f, BeatTime(0.5f)));
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.5f, 1f, BeatTime(1.5f)));
                        builder.Insert(BeatTime(0.5f), ValueEasing.Linear(1f, 0.65f, BeatTime(0.5f)));
                        builder.Insert(BeatTime(4), ValueEasing.Linear(0.65f, 0, BeatTime(4)));

                        builder.Run(s =>
                        {
                            Color col = Color.Lerp(Color.Transparent, Color.White, s);
                            ScreenDrawing.ThemeColor = ScreenDrawing.UIColor = col;
                        });

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(1), ValueEasing.Linear(-2, 0, BeatTime(1)));
                        builder2.Insert(BeatTime(4), ValueEasing.Linear(4, 0, BeatTime(4)));
                        builder2.Adjust = false;
                        builder2.Run(s =>
                        {
                            ScreenDrawing.UISettings.HPShowerPos += new Vector2(0, s);
                            ScreenDrawing.UISettings.NameShowerPos += new Vector2(0, s);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.2f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(4, BeatTime(1.2f), true);
                    });
                    RegisterFunctionOnce("Line1", () =>
                    {
                        int count = 5;
                        Line line = new(CentreEasing.EaseOutQuart(new(0, 0), new(640, 480), BeatTime(count)), ValueEasing.Stable(-45));
                        line.Alpha = 0.5f;
                        line.Width = 3.4f;
                        line.InsertRetention(new(1, 0.5f));
                        line.InsertRetention(new(2, 0.4f));
                        line.InsertRetention(new(3, 0.3f));
                        CreateEntity(line);
                        DelayBeat(count, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        int count = 5;
                        Line line = new(CentreEasing.EaseOutQuart(new(640, 0), new(0, 480), BeatTime(count)), ValueEasing.Stable(45));
                        line.Alpha = 0.5f;
                        line.Width = 3.4f;
                        line.InsertRetention(new(1, 0.5f));
                        line.InsertRetention(new(2, 0.4f));
                        line.InsertRetention(new(3, 0.3f));
                        CreateEntity(line);
                        DelayBeat(count, () => line.Dispose());
                    });
                    RegisterFunctionOnce("Line3", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            float count = 5.5f;
                            Func<ICustomMotion, Vector2> p1 = CentreEasing.EaseOutCubic(new(320, 480), new(320, -260), BeatTime(count));
                            Line line1 = new(CentreEasing.Combine(p1, new Vector2(-320, 240)), p1);
                            Line line2 = new(CentreEasing.Combine(p1, new Vector2(320, 240)), p1);

                            Line[] lines = { line1, line2 };
                            foreach (Line line in lines)
                            {
                                line.Alpha = 0.5f;
                                line.Width = 3.4f;
                                line.InsertRetention(new(0.5f, 0.5f));
                                line.InsertRetention(new(1, 0.4f));
                                line.InsertRetention(new(1.5f, 0.3f));
                                CreateEntity(line);
                                DelayBeat(count, () => line.Dispose());
                            }
                        });
                    });
                    RegisterFunctionOnce("Rise", () =>
                    {
                        SetBoxMission(0);
                        ValueEasing.EaseBuilder ease = new();
                        ease.Insert(BeatTime(8), ValueEasing.EaseOutCubic(BoxStates.Centre.Y, BoxStates.Centre.Y - 15, BeatTime(8)));
                        ease.Run(s => { InstantSetBox(s, 84, 84); InstantTP(320, s); });
                    });
                    RegisterFunctionOnce("Step1", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                        ScreenDrawing.UISettings.UISurfaceDrawing production = ScreenDrawing.UISettings.CreateUISurface();
                        rainer.controlLayer = production.UISurface;

                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);

                        ValueEasing.EaseBuilder ease1 = new();
                        ease1.Insert(BeatTime(8), ValueEasing.EaseOutCirc(210, 280, BeatTime(8)));
                        ease1.Insert(BeatTime(8), ValueEasing.EaseOutElastic(280, 410, BeatTime(8)));

                        ValueEasing.EaseBuilder easeL = new();
                        easeL.Insert(BeatTime(8), ValueEasing.EaseOutCubic(320, 260, BeatTime(8)));
                        easeL.Insert(BeatTime(8), ValueEasing.EaseOutQuint(260, 140, BeatTime(8)));
                        ValueEasing.EaseBuilder easeR = new();
                        easeR.Insert(BeatTime(8), ValueEasing.EaseOutCubic(320, 380, BeatTime(8)));
                        easeR.Insert(BeatTime(8), ValueEasing.EaseOutQuint(380, 260, BeatTime(8)));

                        CentreEasing.EaseBuilder builder1 = new();
                        builder1.Insert(BeatTime(16), CentreEasing.Combine(easeL.GetResult(), ease1.GetResult()));
                        builder1.Run(s =>
                        {
                            SetPlayerBoxMission(0);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        CentreEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), CentreEasing.Combine(easeR.GetResult(), ease1.GetResult()));
                        builder2.Run(s =>
                        {
                            SetPlayerBoxMission(1);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                    });
                    RegisterFunctionOnce("Step2", () =>
                    {
                        float time = BeatTime(8);

                        ScreenDrawing.MakeFlicker(Color.Black * 0.5f);
                        ValueEasing.EaseBuilder shadowOut = new();
                        shadowOut.Insert(BeatTime(4), ValueEasing.EaseOutQuad(ScreenDrawing.DownBoundDistance, 0, BeatTime(4)));
                        shadowOut.Run(s =>
                        {
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s;
                        });
                        CentreEasing.EaseBuilder ease1 = new();
                        ease1.Insert(BeatTime(8), CentreEasing.Alternate(
                            1,
                            CentreEasing.EaseOutCirc(new(260, 280), new(380, 410), time),
                            CentreEasing.Combine(ValueEasing.EaseOutCirc(260, 380, time), ValueEasing.EaseOutElastic(280, 410, time))
                        ));
                        CentreEasing.EaseBuilder ease2 = new();
                        ease2.Insert(BeatTime(8), CentreEasing.Alternate(
                            1,
                            CentreEasing.EaseOutCirc(new(380, 280), new(500, 410), time),
                            CentreEasing.Combine(ValueEasing.EaseOutCirc(380, 500, time), ValueEasing.EaseOutElastic(280, 410, time))
                        ));
                        SetPlayerBoxMission(0);
                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);
                        ease1.Run(s =>
                        {
                            SetPlayerBoxMission(2);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        SetPlayerBoxMission(1);
                        Heart.InstantSplit(Heart.controlingBox.CollidingBox);
                        ease2.Run(s =>
                        {
                            SetPlayerBoxMission(3);
                            InstantTP(s);
                            BoxStates.Centre = s;
                        });
                        DelayBeat(6, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(2), ValueEasing.Linear(0.5f, 0.0f, BeatTime(2)));
                            builder.Run(s =>
                            {
                                for (int i = 0; i <= 3; i++)
                                {
                                    SetPlayerBoxMission(i);
                                    BoxStates.CurrentBox.GreenSoulAlpha = s;
                                    Heart.Alpha = s * 2;
                                }
                            });
                        });
                    });
                    RegisterFunctionOnce("LineS", () =>
                    {
                        float count = 5.5f;
                        Line line1 = new(CentreEasing.EaseOutSine(new(360, 240), new(200, 240), BeatTime(7)), ValueEasing.Stable(90));
                        Line line2 = new(CentreEasing.EaseOutSine(new(280, 240), new(440, 240), BeatTime(7)), ValueEasing.Stable(90));

                        Line[] lines = { line1, line2 };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0;
                            line.Width = 3.4f;
                            line.InsertRetention(new(1, 0.4f));
                            line.AlphaIncrease(BeatTime(1f), 0.7f);
                            DelayBeat(0.51f + 2, () => line.AlphaDecrease(BeatTime(4.3f)));
                            CreateEntity(line);
                            DelayBeat(count, () => line.Dispose());
                        }
                    });
                    RegisterFunctionOnce("Line4k", () =>
                    {
                        float height = 410 - 42 - 2;
                        Line line1 = new(CentreEasing.Stable(new(0, height)), (s) =>
                        {
                            float k = s.AppearTime / BeatTime(7);
                            float scale = (1 - k) * (1 - k);
                            float angle = scale * MathF.PI / 2;
                            return new Vector2(MathF.Cos(angle) * 640f * 1.6f, -MathF.Sin(angle) * height * 1.6f) + new Vector2(0, height);
                        });
                        line1.InsertRetention(new(1, 0.5f));
                        line1.Alpha = 0.7f;
                        DelayBeat(4, () =>
                        {
                            line1.AlphaDecrease(BeatTime(3));
                        });
                        line1.TransverseMirror = true;

                        CentreEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(320, 0), new(320, height), BeatTime(8)));
                        builder.Insert(BeatTime(32), CentreEasing.Stable(new(320, height)));

                        Line line2 = new(builder.GetResult(), (s) => 0);
                        line2.Alpha = 0.5f;
                        DelayBeat(4, () => line2.AlphaIncrease(BeatTime(4), 0.5f));

                        CreateEntity(line1);
                        CreateEntity(line2);

                        DelayBeat(4, () =>
                        {
                            for (int i = -2; i <= 2; i++)
                            {
                                float xPos = 320 + i * 120;
                                CentreEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(xPos, -80), new(xPos, height), BeatTime(8)));
                                Line line = new(CentreEasing.Stable(xPos, -690), builder.GetResult());
                                CreateEntity(line);
                                line.DrawingColor = Color.Silver;
                                line.Width = 3.5f + MathF.Abs(i) * 0.6f;
                            }
                        });
                    });
                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "(RotateL)R", "", "", "", "R", "", "", "R",
                    "+2", "", "+2", "", "R", "", "", "",
                    "(RotateL)R", "", "", "$0", "+2", "", "+2", "+2",
                    "", "+2", "+2", "", "R", "", "", "",
                    "(RotateR)R", "", "", "", "R", "", "", "R",
                    "+2", "", "+2", "", "R", "", "", "",
                    "(RotateR2)R", "", "", "$2", "+2", "", "+2", "+2",
                    "(RotateL2)", "+2", "+2", "+2", "+2", "", "+0", "Blur",
                    "(ConvulseL)(Line1)(R)(+01)", "", "(+0)(+01)", "", "(R)(+011)", "", "(+0)(+01)", "",
                    "(R)(+01)", "", "(+0)(+01)", "", "(R)(^+01'2)", "", "(+0)(+01)", "Blur",
                    "(ConvulseL)(Line2)(R)(+01)", "", "(+0)(+01)", "", "(R)(+011)", "", "(+0)(+01)", "",
                    "(R)(+01)", "", "(+0)(+01)", "", "(R)(^+01'2)", "", "(+0)(+01)", "Blur(Line3)",
                    "(ConvulseR)(Rise)(R)(+01)", "", "(+0)(+01)", "+011", "(R)(^+01'2)", "", "(+0)(+01)", "Blur(Line3)",
                    "(ConvulseR)(Rise)(R)(+01)", "", "(+0)(+01)", "+011", "(R)(^+01'2)", "", "(+0)(+01)", "Blur(Line3)(LineS)",
                    "(ConvulseR2)(Step1)", "", "", "", "", "", "", "Flicker",
                    "(ConvulseL2)(Step2)(Line4k)", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(1152 - 8))
                {
                    DelayBeat(10, Regenerate);
                    float camRotation = 48;
                    #region event
                    RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(0, 0), new(0, 60), BeatTime(8)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDelta = s);

                        cameraProduction = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);

                        ValueEasing.EaseBuilder camera = new();
                        Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending));
                        Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y);
                        Effect3D.ProjectPoint = new(0, 0, 200);

                        ValueEasing.EaseBuilder posMove = new();
                        posMove.Insert(BeatTime(8), ValueEasing.EaseOutSine(200, 170, BeatTime(8)));
                        posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                        camera.Insert(BeatTime(8), ValueEasing.EaseOutSine(0, camRotation, BeatTime(8)));
                        camera.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        SetPlayerMission(0);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                        SetPlayerMission(1);
                        Heart.RotateTo(-90);
                        Heart.FixArrow = false;
                        SetPlayerMission(2);
                        Heart.RotateTo(90);
                        Heart.FixArrow = false;
                        SetPlayerMission(3);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2.5f), ValueEasing.EaseOutQuad(0.02f, 0.01f, BeatTime(2.5f)));
                        builder.Run((s) =>
                        {
                            splitter.Intensity = s * 30 + 0.39f;
                        });
                    });
                    RegisterFunctionOnce("CameraZL", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, 0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, -0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation - 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation - 1.2f, camRotation, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation + 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation + 1.2f, camRotation, BeatTime(1.5f)));
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

                    float speed = 26.5f;
                    float scale = 2.5f;
                    float lateWaitingScale = 0.1f;
                    RegisterFunctionOnce("T0V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(0);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 0, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 2, speed, 0, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 0, 0);
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
                    RegisterFunctionOnce("Tag", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            //PlaySound(Sounds.pierce);
                        }));
                    });
                    DelayEnabled = false;
                    #endregion
                    CreateChart(0, BeatTime(1) * 8, 8.0f, new[] { 
                    //pre
                    "Move(Rotate)", "", "", "", "", "", "", "",
                    //cur
                    "T0(T3)(Tag)(CameraZL)", "T0V(T3V)", "", "",     //3
                    "!!12/3", "(T0)(Tag)", "", "",    "", "!!12/3", "(T3)", "", "",    "",  //4
                    "T0(Tag)(CameraUp)(CameraZL)", "", "", "T1(CameraUp)(CameraZR)",  //5
                    "(Tag)", "", "(T2)", "",  //6
                    "!!3", "(T1)(Tag)(CameraUp)", "(T2)(CameraZL)", "(T1)(CameraZR)",  //7
                    "(T2)(Tag)", "(T2V)", "(T2V)", "(T2V)", //8
                    "T1(T2V)(Tag)(CameraZL)", "", "!!8/4", "(T2)", "", "", "", //9
                    "(T1)(Tag)(CameraUp)", "(T1V)", "(T1V)(T2)(CameraZL)", "T2V", //10
                    "!!8/4", "(T1)(T2V)(Tag)(CameraDown)", "", "", "",    "", "T1(T2)(CameraZL)", //11
                    "(Tag)", "T1(T2)(CameraZL)", "", "", //12
                    "(T2)(Tag)(CameraZL)", "(T2V)", "(T2V)", "T2V", //13
                    "T1(Tag)", "T2", "T1", "T2", //14
                    "!!12/3", "T1(Tag)(CameraUp)", "", "T2",    "", "", "", //15
                    "T2(Tag)", "T1", "T2", "T1", //16
                    "!!12/3", "(T2)(Tag)(CameraUp)", "", "T1",    "", "", "", //17
                    "!!8/4", "T1(Tag)", "", "T1V", "",     "!!8/4", "T2(Tag)", "", "T2V", "", //18
                    "(T3)(Tag)(CameraZL)(CameraUp)", "(T3V)", "(T3V)", "(T3V)",  //19
                    "!!12/3", "(T3V)(T0)(Tag)(CameraUp)", "", "",   "(T3V)", "!!12/3", "(T0)(T3V)(CameraDown)", "", "",   "(T3V)", //20
                    "(T3V)(T0)(Tag)(CameraZL)", "", "", "T1(T2)", //21
                    "(Tag)", "", "(T1)(T2)", "", //22
                    "!!3", "(Tag)(T1)(CameraUp)(CameraZL)", "(T2)(CameraZR)", "T1(CameraZL)", //23
                    "(T2)(Tag)(CameraZL)", "(T2V)", "(T2V)", "(T2V)", //24
                    "T1(T2V)(Tag)", "", "!!16/4", "(T2)(CameraZL)(CameraUp)", "", "", "",    "", //25
                    "T1(Tag)", "", "(T2)", "", //26
                    "!!6/3", "T0(T3)(Tag)(CameraZL)(CameraDown)", "", "",    "", "T0(T3)", //27
                    "(Tag)", "T0(T3)", "", "", //28
                    "!!8", "T1(T2)(Tag)(CameraZR)(CameraUp)", "", "T1V(T2V)", "", "T1V(T2V)", "", "T1V(T2V)", "", //29
                    "T1V(T2V)(Tag)", "T1V(T2V)", "T1V(T2V)", "T1V(T2V)",  //30
                    "(T0)(T3)(T1V)(T2V)(Tag)(CameraZL)(CameraUp)", "T0V(T3V)", "T0V(T3V)", "",  //31
                    "T2(Tag)", "T1", "T2", "T1",  //32
                    "!!8/2", "T2(Tag)(CameraZL)(CameraUp)", "",    "", "T1", "", //33
                    "!!8/2", "T2(Tag)(CameraZL)(CameraUp)", "", "",     "!!8/2", "T1", "", "" //34

                });
                }
                if (InBeat(1280 - 8))
                {
                    float speed = 26.5f;
                    float scale = 2.5f;
                    float lateWaitingScale = 0.1f;
                    float camRotation = 48;
                    #region event
                    RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(0, 0), new(0, 60), BeatTime(8)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDelta = s);

                        RenderProduction production4 = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(production4);

                        ValueEasing.EaseBuilder camera = new();
                        Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending));
                        Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y);
                        Effect3D.ProjectPoint = new(0, 0, 200);
                        camera.Insert(BeatTime(8), ValueEasing.EaseOutSine(0, camRotation, BeatTime(8)));
                        camera.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        SetPlayerMission(0);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                        SetPlayerMission(1);
                        Heart.RotateTo(-90);
                        Heart.FixArrow = false;
                        SetPlayerMission(2);
                        Heart.RotateTo(90);
                        Heart.FixArrow = false;
                        SetPlayerMission(3);
                        Heart.RotateTo(180);
                        Heart.FixArrow = false;
                    });
                    RegisterFunctionOnce("CameraZL", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, 0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(0, -0.012f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-0.012f, 0.0f, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation - 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation - 1.2f, camRotation, BeatTime(1.5f)));
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
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation, camRotation + 1.2f, BeatTime(1.5f)));
                            builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(camRotation + 1.2f, camRotation, BeatTime(1.5f)));
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
                    RegisterFunctionOnce("T0V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(0);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 0, speed, 1, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 2, speed, 0, 0);
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
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 0, 0);
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
                    RegisterFunctionOnce("Tag", () =>
                    {
                        AddInstance(new InstantEvent(CurrentTime, () =>
                        {
                            //PlaySound(Sounds.pierce);
                        }));
                    });
                    DelayEnabled = false;
                    #endregion
                    CreateChart(0, BeatTime(1) * 8, 8.0f, new[] {
                    //pre
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "T0(T3)(CameraDown)(CameraZL)", "T0V(T3V)", "", "",    //35
                    "!!8/2", "(CameraZL)(T0)", "",     "", "!!8/2", "(T3)", "",    "",    //36
                    "T0", "!!8/4", "", "", "T2", "",    "!!12/3", "T1(CameraZR)", "", "",   //37
                    "", "!!12/3", "(CameraZR)T1", "", "",     "", "",    //38
                    "!!3", "(T2)(CameraZL)(CameraUp)", "T1(CameraZR)", "(T2)(CameraZL)",    //39
                    "T1(CameraDown)", "T1V", "T1V", "T1V",    //40
                    "T1V(T2)(CameraZL)", "", "!!16/4", "CameraZL(CameraDown)(T1)", "", "", "",    "",    //41
                    "(T2)(CameraZR)", "", "T1", "",    //42
                    "(T1)(T2)(CameraUp)", "", "", "T2(T1)",    //43
                    "", "T0(CameraZL)(T3)", "", "",    //44
                    "!!8/2", "T1(T2)(CameraZL)", "",   "", "", "",    //45
                    "T2", "T1", "T2", "T1",    //46
                    "(CameraDown)(T2)", "T2V", "T2V", "T2V",    //47
                    "T1", "T2", "T1", "T2",    //48
                    "!!6", "(CameraZR)T1", "", "", "T2", "", "",    //49
                    "!!16/4", "CameraUp(T1)", "", "", "",     "",   "!!16/4", "CameraUp(T2)", "", "", "",     "",    //50
                    "CameraZL(CameraDown)(T0)", "(T0V)", "(T0V)", "(T0V)",    //51
                    "!!8/2", "(T0V)(T3)", "",   "(T0V)",   "!!8/2", "(T0V)(T3)", "",   "(T0V)",    //52
                    "(T3)(T0V)(CameraZL)", "", "", "T0(T3)(CameraZR)",    //53
                    "", "T0(T3)", "", "",    //54
                    "!!3", "(CameraZL)(CameraUp)(T1)", "(CameraZR)(T2)", "(CameraZL)(T1)",    //55
                    "CameraZL(CameraDown)(T2)", "(T2V)", "(T2V)", "(T2V)",    //56
                    "(T2V)(T1)(CameraZL)", "", "!!16/4", "(T2)(CameraZL)", "", "", "",     "",    //57
                    "(CameraZL)T1", "", "(T2)", "",    //58
                    "(CameraZL)(CameraDown)(T1)", "", "", "(CameraZR)(T2)",    //59
                    "", "(CameraZL)", "", "",    //60 
                    "T0(T1)(CameraZL)(CameraDown)", "", "", "",    //61
                    "(T2)(T3)(CameraZL)(CameraDown)", "", "", "",    //62 
                    "T0(T2)(T1)(T3)(CameraZR)(CameraUp)", "", "", "",    //63 
                });

                }
                if (InBeat(1408 - 16 - 1))
                {
                    DelayEnabled = true;

                    DelayBeat(12, Regenerate);

                    RegisterFunctionOnce("Shatter", () =>
                    {
                        DelayBeat(0.875f, () =>
                        {
                            // sinwave shake
                            ScreenDrawing.Shaders.Filter filter;
                            ScreenDrawing.SceneRendering.InsertProduction(filter = new ScreenDrawing.Shaders.Filter(SinWave, 0.6755f));
                            DelayBeat(15, () =>
                            {
                                filter.Dispose();
                                production1.Dispose(); production2.Dispose();
                                ScreenDrawing.UpExtending = 0;
                                cameraProduction.Dispose();
                                Blur.Dispose();
                                ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                                Line[] lines = GetAll<Line>();
                                foreach (Line line in lines)
                                {
                                    line.Dispose();
                                }
                            });
                            float a = 0;
                            ForBeat(15, () =>
                            {
                                SinWave.Parameters["time"].SetValue(a); a += 0.25f;
                                SinWave.Parameters["time2"].SetValue(a * 1.5f);
                            });

                            ValueEasing.EaseBuilder rgbShake = new();
                            rgbShake.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(0, 4, BeatTime(1.5f)));
                            rgbShake.Insert(BeatTime(14.5f), ValueEasing.EaseOutCubic(4, 0, BeatTime(14.5f)));
                            rgbShake.Run(s =>
                            {
                                SinWave.Parameters["frequency"].SetValue(s * 10f + 140f);
                                SinWave.Parameters["frequency2"].SetValue(s * 25f + 200f);
                                SinWave.Parameters["range2"].SetValue(s);
                                SinWave.Parameters["range"].SetValue(s * 1.25f);
                            });

                            // blur effect
                            ValueEasing.EaseBuilder blur = new();
                            blur.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0.0f, 0.2f, BeatTime(16)));
                            blur.Run(s =>
                            {
                                StepSample.Intensity = MathF.Max(0.01f, s);
                                ShadersScale.Intensity = MathF.Max(0.01f, s) / 3f;
                                Blur.Sigma = s * 5;
                            });

                            // camera effect
                            ValueEasing.EaseBuilder camera = new();
                            camera.Insert(BeatTime(14), ValueEasing.EaseOutCirc(0.0f, 0.2f, BeatTime(14)));
                            camera.Run(s =>
                            {
                                ScreenDrawing.ScreenAngle = s * 52;
                            });

                            // flicker 
                            ScreenDrawing.MakeFlicker(Color.White * 0.55f);
                            ScreenDrawing.SceneOutScale = 1.6f;
                            DelayBeat(3, () => ScreenDrawing.SceneOut(Color.Black, BeatTime(11.5f)));

                            // set chart
                            DelayBeat(15.1f, () =>
                            {
                                for (int i = 3; i > 0; i--)
                                {
                                    SetPlayerMission(i);
                                    Heart.Dispose();
                                }
                                SetPlayerMission(0);
                                InstantTP(new(320, 240));
                                InstantSetGreenBox();
                                ScreenDrawing.ScreenAngle = 0;
                                Heart.InstantSetRotation(0);
                                Heart.Alpha = 1.0f;
                                BoxStates.CurrentBox.GreenSoulAlpha = 0.5f;
                                rainer.Intensity = 0.15f;
                                ScreenDrawing.BoundColor = Color.White;
                                ScreenDrawing.ThemeColor = Color.White;
                                ScreenDrawing.UIColor = Color.White;
                                ScreenDrawing.UISettings.RemoveUISurface();
                                ScreenDrawing.UISettings.NameShowerPos = new Vector2(20, 457);
                                ScreenDrawing.UISettings.HPShowerPos = new(320, 443);
                            });

                        });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 4, BeatTime(16)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseInQuad(4, 2, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(2, 0, BeatTime(8)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 4, BeatTime(16)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseInQuad(4, 2, BeatTime(8)));
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutQuad(2, 0, BeatTime(8)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = -s;
                        });
                    });
                    string[] rhythm = { 
                    //pre 
                    "Shatter", "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    //cur 
                    "RotateL", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateR", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateL", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "RotateR", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    string[] rhythm2 = { 
                    //pre 
                    "",
                    //cur
                    "$0", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#5#D1(D)", "", "", "", "", "", "", "",
                    "#5#D(D1)", "", "", "", "", "", "", "",
                    "#3#D1(D)", "", "", "", "", "", "#4#D(D1)", "",
                    "", "", "", "", "#17#D1(D)(D)", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1(D0)", "", "", "",
                    "#13#D0(D1)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#5#D1(D)", "", "", "", "", "", "", "",
                    "#5#D(D1)", "", "", "", "", "", "", "",
                    "#3#D1(D)", "", "", "", "", "", "#23#D(D1)(D1)", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1(D0)", "", "", "",
                };
                    CreateChart(0, BeatTime(1) * 8, 6.0f, rhythm);
                    DelayBeat(9 + 7, () =>
                    {
                        CreateChart(0, BeatTime(1) * 8, 6.0f, rhythm2);
                    });
                }
            }
        }
    }
}