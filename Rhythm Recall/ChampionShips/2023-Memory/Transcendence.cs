using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    internal partial class Transcendence : IChampionShip
    {
        public Transcendence()
        {
            Game.game = new Game();
            divisionImformation = new SaveInfo("imf{");
            divisionImformation.PushNext(new SaveInfo("dif:4"));
            difficulties = new();
            difficulties.Add("div2", Difficulty.Normal);
            difficulties.Add("div1", Difficulty.Extreme);
            //    this.difficulties.Add("Anomaly Test", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionImformation => divisionImformation;
        public SaveInfo divisionImformation;

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
                            new(Difficulty.Extreme, 19.7f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 14.0f),
                            new(Difficulty.Extreme, 19.9f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 16.5f),
                            new(Difficulty.Extreme, 21.7f),
                        }
                    );
                public override string BarrageAuthor => "T-mas";
                public override string AttributeAuthor => "T-mas & Walar & IceAgeDOT";
                public override string PaintAuthor => "Normist & OtokP";
                public override string SongAuthor => "SK_kent";
                public override bool Hidden
                {
                    get
                    {
#if DEBUG
                        return false;
#endif
                        return PlayerManager.CurrentUser == null || !PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("reTranscendence");
                    }
                }
                public override HashSet<Difficulty> UnlockedDifficulties
                {
                    get
                    {
#if DEBUG
                        return base.UnlockedDifficulties;
#endif
                        HashSet<Difficulty> difficulties = new();
                        int v = UndyneFight_Ex.PlayerManager.CurrentUser.Custom.Nexts["reTranscendence"].Nexts["info"].IntValue;
                        for (int i = 0; i <= v; i++) difficulties.Add((Difficulty)i);
                        return difficulties;
                    }
                }
            }
            public SongImformation Attributes => new ThisImformation();

            public Game() : base(62.5f / (190f / 60) / 4) { }

            public static Game game;

            public string Music => "Transcendence";
            //  public string Music => "Brain Power";
            public string FightName => "Transcendence";

            private static class NormalBarrage { }

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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] { 
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
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] {
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
                                ScreenDrawing.ScreenPositionDetla = Vector2.Zero;
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
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    DelayBeat(9 + 7, () =>
                    {
                        BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm2);
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                    BarrageCreate(BeatTime(8), BeatTime(8), 6.4f, new string[]
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] { 
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
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] {
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
                                ScreenDrawing.ScreenPositionDetla = Vector2.Zero;
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
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    DelayBeat(9 + 7, () =>
                    {
                        BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm2);
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] { 
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
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] {
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
                                ScreenDrawing.ScreenPositionDetla = Vector2.Zero;
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
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    DelayBeat(9 + 7, () =>
                    {
                        BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm2);
                    });
                }
            }
            /// <summary>
            /// IT'S HARD BARRAGE!!!
            /// </summary>
            public void Normal()
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
                    "(D)(LineUp)(RotateR)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
                    "(+1)(LineUp)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "(Blur)",
                    "(+1)(LineUp)", "", "(+1)(LineUp2)", "", "(+1)(LineDown)", "", "(+1)(LineDown2)", "",
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                    "(D1)", "", "+11", "", "+11", "", "+11", "",
                    "(D1)", "", "+11", "", "+11", "", "+11", "(Blur)",
                    "D(RotateR)", "", "+1", "", "+1", "", "+1", "",
                    "D", "", "+1", "", "+1", "", "+1", "(Blur2)",
                 //   "D1", "", "D1", "", "D1", "(Up)", "D1", "",
                  //  "(RotateMid)(Black)", "", "", "",  
                    "(D)", "", "+1", "", "+1", "", "+1", "",
                    "(D)", "", "+1", "", "+1", "", "+1", "(Blur)",
                    "D1(RotateL)", "", "+11", "", "+11", "", "+11", "",
                    "D1", "", "+11", "", "+11", "", "+11", "(Blur2)",
                    "(D1)", "", "+11", "", "+11", "", "+11", "",
                    "(D1)", "", "+11", "", "+11", "", "+11", "(Blur)",
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
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#16#$01)(#16#$20)(ConvulseR)(ConvulseR2)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>^$20)",
                    "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>^$20)",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
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
                        // sinwave shake
                        ScreenDrawing.Shaders.Filter filter;
                        ScreenDrawing.SceneRendering.InsertProduction(filter = new ScreenDrawing.Shaders.Filter(SinWave, 0.6755f));
                        DelayBeat(16, () =>
                        {
                            filter.Dispose();
                        });
                        float a = 0;
                        ForBeat(16, () =>
                        {
                            SinWave.Parameters["time"].SetValue(a); a += 0.26f;
                            SinWave.Parameters["time2"].SetValue(a * 1.5f);
                        });

                        ValueEasing.EaseBuilder rgbShake = new();
                        rgbShake.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(0, 1.8f, BeatTime(1)));
                        rgbShake.Insert(BeatTime(7), ValueEasing.EaseOutCubic(1.8f, 0.6f, BeatTime(7)));
                        rgbShake.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1, 8, BeatTime(1)));
                        rgbShake.Insert(BeatTime(7), ValueEasing.EaseOutCubic(8, 0, BeatTime(7)));
                        rgbShake.Run(s =>
                        {
                            SinWave.Parameters["frequency"].SetValue(s * 15f + 150f);
                            SinWave.Parameters["frequency2"].SetValue(s * 35f + 220f);
                            SinWave.Parameters["range2"].SetValue(s);
                            SinWave.Parameters["range"].SetValue(s * 1.25f);
                        });

                        //background color
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
                    "R1(LineL2)(ConvulseR)", "+01(LineL2)(Blur)", "R1(LineR2)", "+01(LineR2)(Blur)", "R1(LineL2)(ConvulseR)", "+01(LineL2)(Blur)","+01(LineR2)", "(LineR2)(BlurLong2)",
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                    "#60#D(RotateL)(FakeBox3)", "", "", "", "", "", "", "",
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
                    "(RotateL)R", "", "", "", "R", "", "", "D",
                    "+0", "", "R", "", "R", "", "", "",
                    "(RotateL)R1", "", "", "R1", "+01", "", "R1", "+01",
                    "", "R1", "+01", "", "R1", "", "", "",
                    "(RotateR)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "(RotateR)R1", "", "", "R1", "+01", "", "R1", "+01",
                    "", "R1", "+01", "R1", "+01", "", "R1", "",
                    "(RotateL2)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "R1", "", "", "R1", "+01", "", "R1", "+01",
                    "", "R1", "+01", "", "R1", "", "", "",
                    "(RotateR2)R", "", "", "", "R", "", "", "R",
                    "+0", "", "R", "", "R", "", "", "",
                    "R1", "", "", "R1", "+01", "", "R1", "+01",
                    "R1", "+01", "R1", "+01", "+01", "", "R1", "",
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
                    "(ConvulseL)(Line1)(R)(+01)", "", "(+0)(+01)", "", "(R)(+011)", "", "(+0)(+01)", "",
                    "(R)(+01)", "", "(+0)(+01)", "", "(R)(^+01'2)", "", "(+0)(+01)", "Blur",
                    "(ConvulseL)(Line2)(R)(+01)", "", "(+0)(+01)", "", "(R)(+011)", "", "(+0)(+01)", "",
                    "(R)(+01)", "", "(+0)(+01)", "", "(R)(^+01'2)", "", "(+0)(+01)", "Blur(Line3)",
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
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] { 
                    //pre
                    "Move(Rotate)", "", "", "", "", "", "", "",
                    //cur
                    "T0(T3)(Tag)(CameraZL)", "T0V(T3V)", "T0V(T3V)", "T0V(T3V)",     //3
                    "!!12/3", "T0V(T3V)(T1)(T2)(Tag)", "", "",    "", "!!12/3", "(T0)(T3)", "", "",    "",  //4
                    "T1(T2)(Tag)(CameraUp)(CameraZL)", "", "", "T0(T1)(CameraUp)(CameraZR)",  //5
                    "(Tag)", "", "(T2)(T3)", "",  //6
                    "!!3", "(T0)(Tag)(CameraUp)", "(T2)(CameraZL)", "(T0)(CameraZR)",  //7
                    "(T2)(T3)(Tag)", "(T2V)(T3V)", "(T2V)(T3V)", "(T2V)(T3V)", //8
                    "T0(T1)(T2V)(T3V)(Tag)(CameraZL)", "", "!!8/4", "(T2)(T3)", "", "", "", //9
                    "T0(T1)(Tag)(CameraUp)", "T0V(T1V)", "T0V(T1V)(T2)(T3)(CameraZL)", "T3V(T2V)", //10
                    "!!8/4", "(T0)(T1)(T3V)(T2V)(Tag)(CameraDown)", "", "", "",    "", "T0(T1)(CameraZL)", //11
                    "(Tag)", "T0(T1)(CameraZL)", "", "", //12
                    "T2(T3)(Tag)(CameraZL)", "(T2V)(T3V)", "(T2V)(T3V)", "(T2V)(T3V)", //13
                    "T1(Tag)(T2V)(T3V)", "T2", "T1", "T2", //14
                    "!!12/3", "T1(Tag)(CameraUp)", "", "T2",    "", "", "", //15
                    "T2(Tag)", "T1", "T2", "T1", //16
                    "!!12/3", "(T2)(Tag)(CameraUp)", "", "T1",    "", "", "", //17
                    "!!8/4", "T0(Tag)", "", "T0V", "",     "!!8/4", "T1(Tag)", "", "T1V", "", //18
                    "(T2)(T3)(Tag)(CameraZL)(CameraUp)", "T2V(T3V)", "T2V(T3V)", "T2V(T3V)",  //19
                    "!!12/3", "T2V(T3V)(T0)(Tag)(CameraUp)", "", "",   "T2V(T3V)", "!!12/3", "(T0)(T2V)(T3V)(CameraDown)", "", "",   "T2V(T3V)", //20
                    "T2V(T3V)(T0)(T1)(Tag)(CameraZL)", "", "", "T1(T2)", //21
                    "(Tag)", "", "(T1)(T2)", "", //22
                    "!!3", "T3(Tag)(CameraUp)(CameraZL)", "T0(CameraZR)", "T3(CameraZL)", //23
                    "(T1)(T2)(Tag)(CameraZL)", "(T1V)(T2V)", "(T1V)(T2V)", "T1V(T2V)", //24
                    "T0(T3)(T1V)(T2V)(Tag)", "", "!!16/4", "(T0)(T3)(CameraZL)(CameraUp)", "", "", "",    "", //25
                    "T1(T2)(Tag)", "", "(T1)(T2)", "", //26
                    "!!6/3", "T0(T3)(Tag)(CameraZL)(CameraDown)", "", "",    "", "T2(T3)", //27
                    "(Tag)", "T1(T2)", "", "", //28
                    "!!8", "T1(T2)(Tag)(CameraZR)(CameraUp)", "", "T1V(T2V)", "", "T1V(T2V)", "", "T1V(T2V)", "", //29
                    "T1V(T2V)(Tag)", "T1V(T2V)", "T1V(T2V)", "T1V(T2V)",  //30
                    "(T0)(T3)(T1V)(T2V)(Tag)(CameraZL)(CameraUp)", "T0V(T3V)", "T0V(T3V)", "",  //31
                    "T2(Tag)", "T1", "T2", "T1",  //32
                    "!!8/2", "T0(T3)(Tag)(CameraZL)(CameraUp)", "",    "", "T0(T3)", "", //33
                    "!!8/2", "T3(T0)(Tag)(CameraZL)(CameraUp)", "", "",     "!!8/2", "T0(T3)", "", "" //34

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
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] {
                    //pre
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "T0(T3)(CameraDown)(CameraZL)", "T0V(T3V)", "T0V(T3V)", "T0V(T3V)",    //35
                    "!!8/2", "T0V(T3V)(CameraZL)(T1)(T2)", "",     "", "!!8/2", "(T1)(T2)", "",    "",    //36
                    "T0(T3)", "!!8/4", "", "T1", "T2", "",    "!!12/3", "T1(CameraZR)", "T2", "",   //37
                    "", "!!12/3", "T1(CameraZR)", "T2", "",     "", "",    //38
                    "!!3", "T1(CameraZL)(CameraUp)", "(T3)(CameraZR)", "T1(CameraZL)",    //39
                    "T0(T3)(CameraDown)", "T0V(T3V)", "T0V(T3V)", "T0V(T3V)",    //40
                    "T0V(T3V)(T1)(T2)(CameraZL)", "", "!!16/4", "CameraZL(CameraDown)(T0)(T3)", "", "", "",    "",    //41
                    "T1(T2)(CameraZR)", "", "T1(T2)", "",    //42
                    "T0(T1)(T2)(T3)(CameraUp)", "", "", "T2(T1)",    //43
                    "", "T0(CameraZL)(T3)", "", "",    //44
                    "!!8/2", "T1(T2)(CameraZL)", "",   "", "", "",    //45
                    "T2", "T1", "T2", "T1",    //46
                    "(CameraDown)(T0)T3", "T0V(T3V)", "T0V(T3V)", "T0V(T3V)",    //47
                    "T1", "T2", "T1", "T2",    //48
                    "!!6", "(CameraZR)T0(T3)", "", "", "T2(T3)", "", "",    //49
                    "!!16/4", "CameraUp(T1)(T2)", "", "", "",     "",   "!!16/4", "CameraUp(T0)(T1)", "", "", "",     "",    //50
                    "CameraZL(CameraDown)(T2)(T3)(T1)(T0)", "(T2V)T3V", "(T2V)T3V", "(T2V)T3V",    //51
                    "!!8/2", "(T2V)T3V(T0)", "",   "(T2V)T3V",   "!!8/2", "(T2V)T3V(T0)", "",   "(T2V)T3V",    //52
                    "T0(T1)(T2V)(T3V)(CameraZL)", "", "", "T0(T3)(CameraZR)",    //53
                    "", "T0(T3)", "", "",    //54
                    "!!3", "(CameraZL)(CameraUp)(T1)", "(CameraZR)(T2)", "(CameraZL)(T1)",    //55
                    "CameraZL(CameraDown)(T1)(T2)", "T1V(T2V)", "T1V(T2V)", "T1V(T2V)",    //56
                    "T1V(T2V)(T0)(T3)(CameraZL)", "", "!!16/4", "(T0)(T2)(CameraZL)", "", "", "",     "",    //57
                    "(CameraZL)T1(T3)", "", "T0(T2)", "",    //58
                    "(CameraZL)(CameraDown)(T0)(T1)", "", "", "(CameraZR)(T2)(T3)",    //59
                    "", "(CameraZL)", "(T3)", "",    //60 
                    "T0(T1)(CameraZL)(CameraDown)", "", "T0", "",    //61
                    "(T2)(T3)(CameraZL)(CameraDown)", "", "(T1)", "(T2)",    //62 
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
                            HeartAttribute.InvincibleToPhysics = false;
                            // sinwave shake
                            ScreenDrawing.Shaders.Filter filter;
                            ScreenDrawing.SceneRendering.InsertProduction(filter = new ScreenDrawing.Shaders.Filter(SinWave, 0.6755f));
                            DelayBeat(15, () =>
                            {
                                rainer.controlLayer = Surface.Normal;
                                filter.Dispose();
                                production1.Dispose(); production2.Dispose();
                                ScreenDrawing.UpExtending = 0;
                                cameraProduction.Dispose();
                                Blur.Dispose();
                                ScreenDrawing.ScreenPositionDetla = Vector2.Zero;
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
                            DelayBeat(15.01f, () =>
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
                    "#4#D1(D)", "", "", "", "", "", "", "",
                    "#4#D(D1)", "", "", "", "", "", "", "",
                    "#2#D1(D)", "", "", "", "", "", "#3#D", "",
                    "", "", "", "", "#12#D1(D)", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1", "", "", "",
                    "#12#D0", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#4#D1", "", "", "", "", "", "", "",
                    "#4#D", "", "", "", "", "", "", "",
                    "#2#D1", "", "", "", "", "", "#16#D(D1)", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    DelayBeat(9 + 7, () =>
                    {
                        BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm2);
                    });
                }
                if (InBeat(1536 - 8))
                {
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(680, 240), new(240, 240), BeatTime(24)), (s) => 90);
                        CreateEntity(line);
                        line.Alpha = 0.65f;
                        line.AlphaDecrease(BeatTime(24));
                        line.InsertRetention(new(1.5f, 0.5f));
                        line.InsertRetention(new(3f, 0.3f));
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(-40, 240), new(400, 240), BeatTime(24)), (s) => 90);
                        CreateEntity(line);
                        line.Alpha = 0.65f;
                        line.AlphaDecrease(BeatTime(24));
                        line.InsertRetention(new(1.5f, 0.5f));
                        line.InsertRetention(new(3f, 0.3f));
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
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutCubic(0, 4, BeatTime(16)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                            ScreenDrawing.ScreenScale = 1 + s * 0.01f;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutCubic(4, -4, BeatTime(16)));
                        builder.Insert(BeatTime(7), ValueEasing.EaseOutCubic(-4, 0, BeatTime(7)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), ValueEasing.EaseOutCubic(1.04f, 1.12f, BeatTime(16)));
                        builder2.Insert(BeatTime(7), ValueEasing.EaseOutCubic(1.12f, 1.0f, BeatTime(7)));
                        builder2.Run(s =>
                        {
                            ScreenDrawing.ScreenScale = s;
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
                    RegisterFunctionOnce("Flicker1", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Black * 0.64f);
                    });
                    RegisterFunctionOnce("Flicker2", () =>
                    {
                        ScreenDrawing.SceneOutScale = 2.5f;
                        ScreenDrawing.SceneOut(Color.Black, BeatTime(3.2f));
                    });

                    string[] rhythm = { 
                    //pre 
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "LineL(RotateL)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "LineR(RotateR)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateL2)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateR2)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "Flicker1", "", "Flicker1", "Flicker1",
                    "Flicker1", "Flicker1", "Flicker1", "", "Flicker2", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    string[] rhythm2 = { 
                    //pre 
                    "", "", "", "", "", "", "", "",  
                    //cur
                    "#12#D0", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#4#D1", "", "", "", "", "", "", "",
                    "#4#D", "", "", "", "", "", "", "",
                    "#2#D1", "", "", "", "", "", "#3#D", "",
                    "", "", "", "", "#14#D1(D)", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "D1", "", "", "",
                    "#13#D0", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#4#D1", "", "", "", "", "", "", "",
                    "#4#D", "", "", "", "", "", "", "",
                    "#12#D1(D)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#7#D0(D1)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm2);
                }
                if (InBeat(1664 - 8))
                {
                    float line1Alpha = 0.18f;
                    RegisterFunctionOnce("Lit", () =>
                    {
                        rainer.Speed = 5f;
                        rainer.Intensity = 0.65f;
                        DelayBeat(0.2f, () =>
                        {
                            ValueEasing.EaseBuilder boundDistance = new();
                            boundDistance.Insert(BeatTime(16), ValueEasing.EaseOutCubic(0, 112f, BeatTime(16)));
                            boundDistance.Run(s =>
                                    ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s
                                );
                            ValueEasing.EaseBuilder backColor = new();
                            backColor.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 0.5f, BeatTime(16)));
                            backColor.Insert(BeatTime(16), ValueEasing.Stable(0.5f));
                            backColor.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0.5f, 1f, BeatTime(16)));
                            backColor.Run(s =>
                                ScreenDrawing.BackGroundColor = Color.Lerp(Color.Transparent, Color.DimGray * 0.35f, s)
                            );

                            ScreenDrawing.BoundColor = Color.Silver * 0.55f;
                        });
                    });
                    RegisterFunctionOnce("LRotateLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(120, 480), new(-20, 200), BeatTime(8)), ValueEasing.EaseOutCubic(0, -90, BeatTime(8)));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("RRotateLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(520, 480), new(660, 200), BeatTime(8)), ValueEasing.EaseOutCubic(0, 90, BeatTime(8)));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("LRotateLine2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(-10, 280), new(120, -15), BeatTime(8)), ValueEasing.EaseOutCubic(90, 0, BeatTime(8)));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("RRotateLine2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(650, 280), new(520, -15), BeatTime(8)), ValueEasing.EaseOutCubic(-90, 0, BeatTime(8)));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });

                    RegisterFunctionOnce("Line2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuart(new(320, 500), new(320, -20), BeatTime(8)), ValueEasing.Stable(0));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(0.5f, 0.5f));
                        line.InsertRetention(new(1f, 0.3f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("LLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutCubic(new(320, 550), new(320, -80), BeatTime(8)), ValueEasing.Stable(11));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("RLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutCubic(new(320, 550), new(320, -80), BeatTime(8)), ValueEasing.Stable(-11));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });

                    RegisterFunctionOnce("Split", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutQuad(4.0f, 0.6f, BeatTime(4)));
                        builder.Run(s => splitter.Intensity = s);
                    });

                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.9f, BeatTime(1.1f), false);

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(6, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.9f, BeatTime(1.1f), true);

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-6, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });

                    RegisterFunctionOnce("BlueLine", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Line line = new(new Vector2(0, 240 + i * 7), new Vector2(640, 240 + i * 7));
                            CreateEntity(line);
                            line.Alpha = 0.3f;
                            line.DrawingColor = Color.LightSkyBlue;
                            line.AlphaDecrease(BeatTime(3));
                        }
                    });
                    RegisterFunctionOnce("RedLine", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Line line = new(new Vector2(0, 240 + i * 7), new Vector2(640, 240 + i * 7));
                            CreateEntity(line);
                            line.Alpha = 0.3f;
                            line.DrawingColor = Color.LightCoral;
                            line.AlphaDecrease(BeatTime(3));
                        }
                    });

                    float aRotation = 0;
                    float bRotation = 0;
                    RegisterFunctionOnce("ARun", () =>
                    {
                        ValueEasing.EaseBuilder aBuilder = new();
                        aBuilder.Adjust = false;
                        for (int j = 0; j < 4; j++)
                        {
                            int count = (j == 3) ? 2 : 3;
                            for (int i = 0; i < count; i++)
                            {
                                aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                                aBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                                aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                                aBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                            }
                            aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                            aBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                            aBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));

                            if (j == 2)
                            {
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-4, 4, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(4, -4, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-4, 4, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutCirc(4, 0, BeatTime(1)));
                            }
                            else if (j == 3)
                            {
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-3, 3, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(3, -3, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-3, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                            }
                            else
                            {
                                aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                                aBuilder.Insert(BeatTime(1.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                                aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                                aBuilder.Insert(BeatTime(1.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                            }
                        }
                        aBuilder.Run(s => aRotation = s);
                    });
                    RegisterFunctionOnce("BRun", () =>
                    {
                        ValueEasing.EaseBuilder bBuilder = new();
                        bBuilder.Adjust = false;
                        for (int j = 0; j < 4; j++)
                        {
                            int count = (j == 3) ? 2 : 3;
                            for (int i = 0; i < count; i++)
                            {
                                bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                                bBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                                bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                                bBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                            }
                            bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                            bBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                            bBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));

                            if (j == 2)
                            {
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(4, -4, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-4, 4, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(4, -4, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-4, 0, BeatTime(1)));
                            }
                            else if (j == 3)
                            {
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(3, -3, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-3, 3, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(3, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                            }
                            else
                            {
                                bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                                bBuilder.Insert(BeatTime(1.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                                bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                                bBuilder.Insert(BeatTime(1.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                            }
                        }
                        bBuilder.Run(s => bRotation = s);
                    });

                    string[] rhythm = { 
                    //pre 
                    "", "", "", "", "", "", "", "Lit", 
                    //cur 
                    "LRotateLine(ARun)(BRun)(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine",
                    "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine",
                    "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine",
                    "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine",
                    "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2",
                    "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2",
                    "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2",
                    "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2",
                    "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine", "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine",
                    "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine", "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine",
                    "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine", "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine",
                    "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine", "LLine(Split)(ConvulseL)", "RLine(Split)(ConvulseL)", "LLine(Split)(ConvulseL)", "RLine",
                    "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2", "LRotateLine", "(RRotateLine)Line2", "LRotateLine2(Split)(ConvulseR)", "Line2(RRotateLine2)", "LRotateLine2", "Line2(RRotateLine2)",
                    "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2", "LRotateLine", "(RRotateLine)Line2", "LRotateLine2(Split)(ConvulseR)", "Line2(RRotateLine2)", "LRotateLine2", "Line2(RRotateLine2)",
                    "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2", "LRotateLine", "(RRotateLine)Line2", "LRotateLine2(Split)(ConvulseR)", "Line2(RRotateLine2)(Split)(ConvulseL)", "LRotateLine2(Split)(ConvulseR)", "Line2(RRotateLine2)",
                    "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2(Split)(ConvulseL)", "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2(Split)(ConvulseL)", "(Split(ConvulseR))",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    string[] rhythm2 = { 
                    //pre 
                    "", "", "", "", "", "", "", "",  
                    //cur 
                    "($01{A})($21{A})(RedLine)", "", "", "", "($01{B})($21{B})(RedLine)", "", "", "",
                    "($01{A})($21{A})(RedLine)", "", "", "", "($01{B})($21{B})(RedLine)", "", "", "",
                    "($01{A})($21{A})(RedLine)", "", "", "", "($01{B})($21{B})(RedLine)", "", "", "",
                    "($01{A})($21{A})(RedLine)", "", "", "", "($01{B})(RedLine)", "", "($21{B})(RedLine)", "",
                    "($00{A})($20{B})(BlueLine)", "", "", "", "($00{B})($20{A})(BlueLine)", "", "", "",
                    "($00{A})($20{B})(BlueLine)", "", "", "", "($00{B})($20{A})(BlueLine)", "", "", "",
                    "($00{A})($20{B})(BlueLine)", "", "", "", "($00{B})($20{A})(BlueLine)", "", "", "",
                    "($00{A})($20{B})(BlueLine)", "", "", "", "($00{B})(BlueLine)", "", "($20{A})(BlueLine)", "",
                    "($01{B})($21{B})(RedLine)", "", "", "", "($01{A})($21{A})(RedLine)", "", "", "",
                    "($01{B})($21{B})(RedLine)", "", "", "", "($01{A})($21{A})(RedLine)", "", "", "",
                    "($01{B})($21{B})(RedLine)", "", "", "", "($01{A})($21{A})(RedLine)", "", "", "",
                    "($01{B})($21{B})(RedLine)", "", "", "", "($01{A})(RedLine)", "($20{B})(BlueLine)", "($01{A})(RedLine)", "",
                    "($00{B})($20{A})(BlueLine)", "", "", "", "($00{A})($20{B})(BlueLine)", "", "", "",
                    "($00{B})($20{A})(BlueLine)", "", "", "", "($00{A})($20{B})(BlueLine)", "", "", "",
                    "($00{B})($20{A})(BlueLine)", "", "", "", "($00{A})(BlueLine)", "($01{B})(RedLine)", "($00{A})(BlueLine)", "",
                    "(^$20'1{A})(BlueLine)", "($21{B})(RedLine)", "(^$20'1{A})(BlueLine)", "($21{B})(RedLine)", "(^$20'1{A})(BlueLine)", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm2);

                    ForBeat120(1, 144, () =>
                    {
                        Arrow[] tagA = GetAll<Arrow>("A");
                        foreach (Arrow a in tagA)
                        {
                            a.CentreRotationOffset = aRotation;
                        }
                        Arrow[] tagB = GetAll<Arrow>("B");
                        foreach (Arrow b in tagB)
                        {
                            b.CentreRotationOffset = bRotation;
                        }
                    });
                }
                if (InBeat(1792 - 16))
                {
                    Vector2 XDelta, YDelta, ZDelta, WDelta;
                    XDelta = YDelta = ZDelta = WDelta = Vector2.Zero;

                    RegisterFunctionOnce("PreArrow", () =>
                    {
                        float delay = BeatTime(11.8f);
                        for (int time = 0; time <= 7; time++)
                        {
                            int count = (time % 2 == 0) ? 0 : 1;
                            int dir = count * 2 - 1;

                            Arrow arr = MakeArrow(delay, "D", 7.0f, count, 0);
                            DelayBeat(11, () =>
                            {
                                arr.Delay(BeatTime(4));

                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(5), ValueEasing.EaseOutQuart(90, 0, BeatTime(5)));
                                builder.Run(s =>
                                {
                                    arr.CentreRotationOffset = s * dir;
                                });
                            });
                            arr.CentreRotationOffset = 90 * dir;

                            CreateEntity(arr);
                            delay += BeatTime(2);
                        }
                    });

                    RegisterFunctionOnce("CrossLine", () =>
                    {
                        for (int dir = -1; dir <= 1; dir += 2)
                        {
                            for (int x = 0; x <= 1; x++)
                            {
                                float rotation = x * 90;

                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(1.3f), ValueEasing.Stable(rotation));
                                builder.Insert(BeatTime(5), ValueEasing.EaseOutQuart(0, 90 * dir, BeatTime(3.6f)));
                                builder.Insert(BeatTime(3), ValueEasing.Linear(0, 20 * dir, BeatTime(3)));

                                Line line = new(CentreEasing.Stable(320, 240), builder.GetResult());
                                line.Alpha = 0.0f;
                                line.DrawingColor = Color.White;
                                line.AlphaIncrease(BeatTime(1.3f), 0.2f);
                                DelayBeat(3, () => line.AlphaDecrease(BeatTime(6)));

                                CreateEntity(line);
                            }
                        }
                    });

                    float delta = 0;
                    RegisterFunctionOnce("LConvulse", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.5f, BeatTime(0.75f), false);
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-12, 0, BeatTime(0.75f)));
                        builder.Run(s => delta = s);
                    });
                    RegisterFunctionOnce("RConvulse", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.5f, BeatTime(0.75f), true);
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(12, 0, BeatTime(0.75f)));
                        builder.Run(s => delta = s);
                    });

                    RegisterFunctionOnce("BlueLine", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Line line = new(CentreEasing.EaseOutQuart(new Vector2(320, 240 + i * 22), new(320, 240 + i * 8), BeatTime(2)), ValueEasing.Stable(0));
                            CreateEntity(line);
                            line.Alpha = 0.0f;
                            line.DrawingColor = Color.LightSkyBlue;
                            line.AlphaIncreaseAndDecrease(BeatTime(2.1f), 0.25f);
                        }
                    });
                    RegisterFunctionOnce("RedLine", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Line line = new(CentreEasing.EaseOutQuart(new Vector2(320, 240 + i * 22), new(320, 240 + i * 8), BeatTime(2f)), ValueEasing.Stable(0));
                            CreateEntity(line);
                            line.Alpha = 0.0f;
                            line.DrawingColor = Color.LightCoral;
                            line.AlphaIncreaseAndDecrease(BeatTime(2.1f), 0.25f);
                        }
                    });

                    RegisterFunctionOnce("LConvulse2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.9f, BeatTime(1.0f), false);

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(7, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunctionOnce("RConvulse2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.9f, BeatTime(1.0f), true);

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-7, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });

                    RegisterFunctionOnce("LineL2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.32f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.319f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.32f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.319f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.32f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.319f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.32f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.319f));
                        DelayBeat(5, () => line.Dispose());
                    });

                    string[] rhythm2 = { 
                    //pre 
                    "PreArrow", "", "", "", "", "", "", "",
                    "", "", "CrossLine", "", "", "", "", "",  
                    //cur  
                    "(LConvulse)LineL", "", "", "", "(LConvulse)LineL", "", "", "",
                    "(LConvulse)LineL", "", "", "", "(LConvulse)LineL", "", "", "",
                    "D(LConvulse)(LineL)", "", "D1", "", "D(LConvulse)(LineL)", "", "D1", "",
                    "D(LConvulse)(LineL)", "", "D1", "", "D(LConvulse)(LineL2)", "", "D1", "",
                    "D(RConvulse)(LineR)", "", "D1", "", "D(RConvulse)(LineR)", "", "D1", "",
                    "D(RConvulse)(LineR)", "", "D1", "", "D(RConvulse)(LineR)", "", "D1", "",
                    "D(RConvulse)(LineR)", "", "D1", "", "D(RConvulse)(LineR)", "", "D1", "",
                    "D(RConvulse)(LineR)", "", "D1", "", "D(RConvulse)(LineR2)", "", "D1", "",
                    "($01{X,T})($21{Y,T})(LConvulse)(RedLine)(LineL)", "", "($01{X,T})($21{Y,T})(LConvulse)(RedLine)(LineL)", "", "($01{X,T})($21{Y,T})(LConvulse)(RedLine)(LineL)", "", "($01{X,T})($21{Y,T})(LConvulse)(RedLine)(LineL)", "",
                    "($01{X,T})($21{Y,T})(LConvulse)(RedLine)(LineL)", "", "($01{X,T})($21{Y,T})(LConvulse)(RedLine)(LineL)", "", "($01{X,T})($21{Y,T})(LConvulse)(RedLine)(LineL)", "", "($01{X,T})($21{Y,T})(LConvulse)(RedLine)(LineL)", "",
                    "($00{Y,T})($20{X,T})(RConvulse)(BlueLine)(LineR)", "", "($00{Y,T})($20{X,T})(RConvulse)(BlueLine)(LineR)", "", "($00{Y,T})($20{X,T})(RConvulse)(BlueLine)(LineR)", "", "($00{Y,T})($20{X,T})(RConvulse)(BlueLine)(LineR)", "",
                    "($00{Y,T})($20{X,T})(RConvulse)(BlueLine)(LineR)", "", "($00{Y,T})($20{X,T})(RConvulse)(BlueLine)(LineR)", "", "($00{Y,T})($20{X,T})(RConvulse)(BlueLine)(LineR)", "", "($00{Y,T})($20{X,T})(RConvulse)(BlueLine)(LineR)", "",
                    "($01{X,T})(LConvulse)(RedLine)(LineL)", "($21{Y,T})(RConvulse)(LineL)", "($01{X,T})(LConvulse)(RedLine)(LineL)", "($21{Y,T})(RConvulse)(LineL)", "($01{X,T})(LConvulse)(RedLine)(LineL)", "($21{Y,T})(RConvulse)(LineL)", "($01{X,T})(LConvulse)(RedLine)(LineL)", "($21{Y,T})(RConvulse)(LineL)",
                    "($00{Y,T})(LConvulse)(BlueLine)(LineR)", "($20{X,T})(RConvulse)(LineR)", "($00{Y,T})(LConvulse)(BlueLine)(LineR)", "($20{X,T})(RConvulse)(LineR)", "($00{Y,T})(LConvulse)(BlueLine)(LineR)", "($20{X,T})(RConvulse)(LineR)", "($00{Y,T})(LConvulse)(BlueLine)(LineR)", "($20{X,T})(RConvulse)(LineR)",
                    "($01{X,T})($21{Y,T})(RConvulse2)(LineL)", "(LineR)", "(<$11)(LConvulse2)(LineL)", "(>$10)(LConvulse2)(LineR)", "(<$11)(LConvulse2)(LineL)", "(LineR)", "(>$31)(RConvulse2)(LineL)", "(<$30)(RConvulse2)(LineR)",
                    "(LineL)", "(>$10)(LConvulse2)(LineR)", "(<$11)(LConvulse2)(LineL)", "(>$10)(LConvulse2)(LineR)", "(<$11)(LConvulse2)(LineL)", "(>$10)(LConvulse2)(LineR)", "(<$11)(LConvulse2)(LineL)", "(>$10)(LConvulse2)(LineR)",
                };

                    ForBeat120(1, 144, () =>
                    {
                        Arrow[] tagA = GetAll<Arrow>("X");
                        foreach (Arrow a in tagA)
                        {
                            float time = a.BlockTime - GametimeF;
                            time /= 15f;
                            a.Offset = new(delta * 0.4f, time * time * time * time + delta);
                            a.Alpha = MathUtil.Clamp(0, 4.1f - time * 1.5f, 0.85f);
                        }
                        Arrow[] tagB = GetAll<Arrow>("Y");
                        foreach (Arrow b in tagB)
                        {
                            float time = b.BlockTime - GametimeF;
                            time /= 15f;
                            b.Offset = new(delta * 0.4f, -time * time * time * time + delta);
                            b.Alpha = MathUtil.Clamp(0, 4.1f - time * 1.5f, 0.85f);
                        }
                    });
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm2);
                }
                NormalFinal();
            }
            private void NormalFinal()
            {
                if (InBeat(1920 - 8))
                {
                    ScreenDrawing.BackGroundColor = Color.DimGray * 0.35f;
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(ScreenDrawing.ScreenAngle, -3f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(ScreenDrawing.ScreenAngle, 3f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 0f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });

                    RegisterFunctionOnce("MoveL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(9), ValueEasing.EaseOutBack(ScreenDrawing.ScreenPositionDetla.X, -11f, BeatTime(9)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDetla = new(s, 0);
                        });
                        ValueEasing.EaseBuilder rotate = new();
                        rotate.Insert(BeatTime(3), ValueEasing.EaseOutQuart(0, 2, BeatTime(3)));
                        rotate.Insert(BeatTime(1.5f), ValueEasing.EaseInQuad(2, 1, BeatTime(1.5f)));
                        rotate.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(1, 0.0f, BeatTime(1.5f)));
                        rotate.Insert(BeatTime(1), ValueEasing.Stable(0));
                        rotate.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("MoveR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(9), ValueEasing.EaseOutBack(ScreenDrawing.ScreenPositionDetla.X, 11f, BeatTime(9)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDetla = new(s, 0);
                        });
                        ValueEasing.EaseBuilder rotate = new();
                        rotate.Insert(BeatTime(3), ValueEasing.EaseOutQuart(0, -2, BeatTime(3)));
                        rotate.Insert(BeatTime(1.5f), ValueEasing.EaseInQuad(-2, -1, BeatTime(1.5f)));
                        rotate.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(-1, 0.0f, BeatTime(1.5f)));
                        rotate.Insert(BeatTime(1), ValueEasing.Stable(0));
                        rotate.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("MoveMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenPositionDetla.X, 0, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDetla = new(s, 0);
                        });
                    });

                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.15f);
                        ScreenDrawing.CameraEffect.Convulse(0.1f, BeatTime(1.4f), false);
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2.2f, BeatTime(1.2f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2.2f, BeatTime(1.2f), true);
                    });

                    string[] rhythm = { 
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "(D)(+01)(RotateL)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)(Flicker)", "", "(D)(+01)", "", "", "", "(D)", "",
                    "(D)(+01)(RotateR)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)(Flicker)", "", "(D)(+01)", "", "", "", "D", "",
                    "(D)(+01)(RotateL)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)(Flicker)", "", "(D)(+01)", "", "", "", "(D)(+01)", "",
                    "(D)(+01)(RotateMid)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "D(Flicker)", "+0", "+0", "", "(D)(+01)", "", "(D)(+01)", "",

                    "(D)(+01)(MoveL)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)(Flicker)", "", "(D)(+01)", "", "", "", "D", "",
                    "(D)(+01)(MoveR)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)(Flicker)", "", "(D)(+01)", "", "", "", "D", "",
                    "(D)(+01)(MoveL)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)(Flicker)", "", "(D)(+01)", "", "", "", "(D)(+01)", "",
                    "(D)(+01)(MoveMid)(ConvulseL)", "", "", "", "(^D'1.6)(^+01'1.6)(ConvulseL)", "", "", "",
                    "(^D'1.6)(^+01'1.6)(ConvulseR)", "", "(^D'1.6)(^+01'1.6)(ConvulseR)", "", "", "", "(D)", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                }
                if (InBeat(2048 - 8))
                {
                    ScreenDrawing.BackGroundColor = Color.DimGray * 0.35f;
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(ScreenDrawing.ScreenAngle, -3f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(ScreenDrawing.ScreenAngle, 3f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 0f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });

                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.15f);
                        ScreenDrawing.CameraEffect.Convulse(0.1f, BeatTime(1.4f), false);
                    });
                    RegisterFunctionOnce("In", () =>
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            DelayBeat(i, () =>
                            {
                                ScreenDrawing.MakeFlicker(Color.White * 0.38f);
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(0, 0.02f, BeatTime(0.5f)));
                                builder.Insert(BeatTime(0.5f), ValueEasing.Stable(0));
                                builder.OffsetPosition = ScreenDrawing.ScreenScale;
                                builder.Run(s =>
                                    ScreenDrawing.ScreenScale = s
                                );
                            });
                        }
                        DelayBeat(8, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(3), ValueEasing.EaseOutQuint(ScreenDrawing.ScreenScale, 1f, BeatTime(3)));
                            builder.Run(s =>
                                ScreenDrawing.ScreenScale = s
                            );
                        });
                    });

                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2.7f, BeatTime(1f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2.7f, BeatTime(1f), true);
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1f), ValueEasing.EaseOutCubic(0.06f, 0.01f, BeatTime(1)));
                        builder.OffsetPosition = ScreenDrawing.ScreenScale;
                        builder.Run(s => ScreenDrawing.ScreenScale = s);
                    });
                    RegisterFunctionOnce("Expand2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1f), ValueEasing.EaseOutCubic(0.07f, 0.017f, BeatTime(1)));
                        builder.OffsetPosition = ScreenDrawing.ScreenScale;
                        builder.Run(s => ScreenDrawing.ScreenScale = s);
                    });
                    RegisterFunctionOnce("Reverse", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4f), ValueEasing.EaseOutQuad(ScreenDrawing.ScreenScale + 0.17f, 1f, BeatTime(4)));
                        builder.Run(s => ScreenDrawing.ScreenScale = s);
                        ScreenDrawing.MakeFlicker(Color.White * 0.45f);
                    });
                    RegisterFunctionOnce("Split", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutQuad(5.0f, 0.6f, BeatTime(4)));
                        builder.Run(s => splitter.Intensity = s);
                    });

                    RegisterFunctionOnce("Down", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-7, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunctionOnce("Up", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-7, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });

                    string[] rhythm = { 
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "(D)(+01)(RotateL)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)Flicker", "", "(D)(+01)", "", "", "", "(D)", "",
                    "(D)(+01)(RotateR)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)(Flicker)", "", "(D)(+01)", "", "", "", "D", "",
                    "(D)(+01)(RotateL)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)(Flicker)", "", "(D)(+01)", "", "", "", "(D)(+01)", "",
                    "(D)(+01)(RotateMid)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "D(In)", "+0", "+2", "+0", "+2", "+0", "+2", "",

                    "($11)($31)ConvulseL(Split)", "", "($11)($31)ConvulseL(Split)", "", "", "", "", "",
                    "($10)($30)ConvulseR(Split)(Expand)", "", "($10)($30)ConvulseR(Split)(Expand)", "", "", "", "($0)($21)ConvulseR(Split)", "",
                    "(<$30)(>$31)ConvulseL(Split)(Down)", "", "", "(<$10)(>$11)ConvulseL(Split)(Up)", "", "", "(<$30)(>$31)ConvulseL(Split)(Down)", "",
                    "(>$30)(<$31)ConvulseR(Split)(Down)", "", "(>$10)(<$11)ConvulseR(Split)(Up)", "", "", "", "(>$30)(<$31)ConvulseR(Split)(Down)", "",
                    "($00)($20)(ConvulseL)(Expand2)(Split)", "", "", "($00)($20)(ConvulseL)(Expand2)(Split)", "", "", "($00)($20)(ConvulseL)(Expand2)(Split)", "",
                    "($01)($21)(ConvulseR)(Expand2)(Split)", "", "", "($01)($21)(ConvulseR)(Expand2)(Split)", "", "", "($00)($21)(ConvulseR)(Expand2)(Split)", "",
                    "($00)($20)(ConvulseL)(Expand2)(Split)", "", "", "($00)($20)(ConvulseL)(Expand2)(Split)", "", "", "($00)($20)(ConvulseL)(Expand2)(Split)", "",
                    "(<$00)(<$20)(>$01)(>$21)(ConvulseR)(Reverse)", "", "", "", "", "", "", "",

                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                }
                if (InBeat(2176 - 8))
                {
                    ScreenDrawing.BackGroundColor = Color.DimGray * 0.35f;
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(360, BeatTime(18));
                    });
                    RegisterFunctionOnce("RotateN", () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-360, BeatTime(16));
                    });
                    RegisterFunctionOnce("Spread", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation + 21 + t * 15, BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 + 21 + t * 15, BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }
                    });
                    RegisterFunctionOnce("SpreadN", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation - (21 + t * 15), BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 - (21 + t * 15), BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }
                    });
                    RegisterFunctionOnce("ShakeA", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(6.0f, BeatTime(1), false);
                        ScreenDrawing.CameraEffect.SizeExpand(12.0f, BeatTime(1));
                    });
                    RegisterFunctionOnce("ShakeB", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.0f, BeatTime(1), true);
                        ScreenDrawing.CameraEffect.SizeExpand(2.0f, BeatTime(1));
                    });
                    RegisterFunctionOnce("Shake", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCirc(2.4f, 0, BeatTime(2.6f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s);
                        builder.Run(s => ScreenDrawing.ScreenScale = s * 0.012f + 1);
                    });
                    RegisterFunctionOnce("ShakeN", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCirc(-2.4f, 0, BeatTime(2.6f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s);
                        builder.Run(s => ScreenDrawing.ScreenScale = -s * 0.012f + 1);
                    });

                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.18f);
                        ScreenDrawing.CameraEffect.Convulse(0.1f, BeatTime(1.4f), false);
                    });
                    string[] rhythm = { 
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur 
                    "(D)(+01)", "", "", "(D)(+01)", "(Spread)(Shake)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "(SpreadN)(ShakeN)", "", "(D)", "",
                    "(D)(+01)", "", "", "(D)(+01)", "(Spread)(Shake)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "(SpreadN)(ShakeN)", "", "(#15#$0)(#15#$21)(Rotate)", "",
                    "", "", "<$001", "<$211", ">$001(Spread)(ShakeA)", ">$211", "<$001", "<$211",
                    ">$001", ">$211", "<$001", "<$211", ">$001(SpreadN)(ShakeB)", ">$211", "<$001", "<$211",
                    "(D)(+01)", "", "D", "", "(D)(+01)(Spread)(Shake)", "", "D", "",
                    "(D)(+01)", "", "D", "+0", "+2(SpreadN)(ShakeN)", "", "D", "",
                    "(D)(+01)", "", "", "(D)(+01)", "(Spread)(Shake)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "(SpreadN)(ShakeN)", "", "(D)", "",
                    "(D)(+01)", "", "", "(D)(+01)", "(Spread)(Shake)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "(SpreadN)(ShakeN)", "", "($30)($11)", "",
                    "(#13#$2)(#13#$01)(RotateN)", "", "<$011", "<$201", ">$011(Spread)(ShakeA)", ">$201", ">$011", ">$201",
                    ">$011", ">$201", ">$011", ">$201", ">$011(SpreadN)(ShakeB)", ">$201", ">$011", ">$201",
                    "(D)(+01)(Spread)(Shake)(Flicker)", "", "D1", "", "(D)(+01)(Spread)(Shake)(Flicker)", "", "D1", "",
                    "(D)(+01)(SpreadN)(ShakeN)(Flicker)", "", "D1", "+01", "+21(SpreadN)(ShakeN)(Flicker)", "", "D1", "",

                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                }
                if (InBeat(2304 - 8))
                {
                    ScreenDrawing.BackGroundColor = Color.DimGray * 0.35f;
                    RegisterFunctionOnce("Spread", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation + 21 + t * 15, BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 + 21 + t * 15, BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }
                    });
                    RegisterFunctionOnce("SpreadN", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation - (21 + t * 15), BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 - (21 + t * 15), BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }
                    });

                    RegisterFunction("ConvulseX", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(2.5f, 0, BeatTime(1.5f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s);

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(4), ValueEasing.EaseOutQuad(5.0f, 0.6f, BeatTime(4)));
                        builder2.Run(s => splitter.Intensity = s);

                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-12, 0, BeatTime(1.4f)));
                        builder3.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunction("ConvulseY", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-2.5f, 0, BeatTime(1.5f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s);

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(4), ValueEasing.EaseOutQuad(5.0f, 0.6f, BeatTime(4)));
                        builder2.Run(s => splitter.Intensity = s);

                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(12, 0, BeatTime(1.4f)));
                        builder3.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });

                    float AlphaCalc(float delta)
                    {
                        delta = MathF.Abs(delta);
                        if (delta <= 50) return 1;
                        delta -= 50;
                        delta = 1 - delta / 150f;
                        return delta * delta;
                    }
                    RegisterFunctionOnce("Shake", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCirc(2.4f, 0, BeatTime(2.6f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s * 1.2f);
                        builder.Run(s => ScreenDrawing.ScreenScale = s * 0.012f + 1);
                    });
                    RegisterFunctionOnce("ShakeN", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCirc(-2.4f, 0, BeatTime(2.6f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s * 1.2f);
                        builder.Run(s => ScreenDrawing.ScreenScale = -s * 0.012f + 1);
                    });
                    float yDelta1 = 0;
                    float yDelta2 = 0;
                    RegisterFunctionOnce("Run", () =>
                    {
                        ValueEasing.EaseBuilder builderX = new();
                        builderX.Adjust = false;
                        builderX.Insert(BeatTime(1), ValueEasing.Stable(990));
                        for (int i = 0; i < 4; i++)
                        {
                            builderX.Insert(BeatTime(9), ValueEasing.EaseOutElastic(220, 0, BeatTime(9)));
                            builderX.Insert(BeatTime(7), ValueEasing.Stable(990));
                        }
                        builderX.Run(s => yDelta1 = s);
                        ValueEasing.EaseBuilder builderY = new();
                        builderY.Adjust = false;
                        builderY.Insert(BeatTime(1), ValueEasing.Stable(990));
                        builderY.Insert(BeatTime(6f), ValueEasing.Stable(990));
                        for (int i = 0; i < 4; i++)
                        {
                            builderY.Insert(BeatTime(9), ValueEasing.EaseOutElastic(-220, 0, BeatTime(9)));
                            builderY.Insert(BeatTime(7), ValueEasing.Stable(990));
                        }
                        builderY.Run(s => yDelta2 = s);
                    }
                    );
                    string[] rhythm2 = {
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "", "", "", "", "(SpreadN)(Shake)", "", "", "",
                    "", "", "", "", "Spread(ShakeN)", "", "", "",
                    "", "", "", "", "(SpreadN)(Shake)", "", "", "",
                    "", "", "", "", "Spread(ShakeN)", "", "", "",
                    "", "", "", "", "(SpreadN)(Shake)", "", "", "",
                    "", "", "", "", "Spread(ShakeN)", "", "", "",
                    "", "", "", "", "(SpreadN)(Shake)", "", "", "",
                    "", "", "", "", "Spread(ShakeN)", "", "", "",

                    "ConvulseX", "", "", "", "", "", "ConvulseX", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseY", "", "", "", "", "", "ConvulseY", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseX", "", "", "", "", "", "ConvulseX", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseY", "", "", "", "", "", "ConvulseY", "",
                    "", "", "", "", "", "", "", "",
                };
                    string[] rhythm = { 
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur 
                    "(D)(+01)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "", "", "(D)", "",
                    "(D)(+01)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "", "", "(D)", "",
                    "(D)(+01)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "", "", "(D)", "",
                    "(D)(+01)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                    "$0(Run)", "+01", "+20", "+01", "+20", "+01", "+20", "+01",

                    "($00{X})($20{X})", "", "", "D1", "", "", "($00{Y})($20{Y})", "",
                    "", "D1", "", "", "D", "", "D", "",
                    "($01{X})($21{X})", "", "", "D", "", "", "($01{Y})($21{Y})", "",
                    "", "D", "", "", "D1", "", "(D1)", "",
                    "($00{X})($20{X})", "", "", "D1", "", "", "($00{Y})($20{Y})", "",
                    "", "D1", "", "", "D", "", "(D)", "",
                    "($01{X})($21{X})", "", "", "D", "", "", "($01{Y})($21{Y})", "",
                    "", "D", "", "", "D1", "", "(D1)", "",

                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm2);

                    Arrow[] allX = new Arrow[0];
                    Arrow[] allY = new Arrow[0];
                    DelayBeat(1, () => { allX = GetAll<Arrow>("X"); allY = GetAll<Arrow>("Y"); });
                    ForBeat120(64, 80, () =>
                    {
                        foreach (Arrow arr in allX)
                        {
                            arr.Offset = new(0, yDelta1);
                            arr.Alpha = AlphaCalc(yDelta1);
                        }
                        foreach (Arrow arr in allY)
                        {
                            arr.Offset = new(0, yDelta2);
                            arr.Alpha = AlphaCalc(yDelta2);
                        }
                    });
                }
                if (InBeat(2432 - 8))
                {
                    float AlphaCalc(float delta)
                    {
                        delta = MathF.Abs(delta);
                        if (delta <= 50) return 1;
                        delta -= 50;
                        delta = 1 - delta / 150f;
                        return delta * delta;
                    }
                    float yDelta1 = 0, yDelta2 = 0;
                    RegisterFunctionOnce("Run", () =>
                    {
                        ValueEasing.EaseBuilder builderX = new();
                        builderX.Adjust = false;
                        builderX.Insert(BeatTime(1), ValueEasing.Stable(990));
                        for (int i = 0; i < 4; i++)
                        {
                            builderX.Insert(BeatTime(9), ValueEasing.EaseOutElastic(220, 0, BeatTime(9)));
                            builderX.Insert(BeatTime(7), ValueEasing.Stable(990));
                        }
                        builderX.Run(s => yDelta1 = s);
                        ValueEasing.EaseBuilder builderY = new();
                        builderY.Adjust = false;
                        builderY.Insert(BeatTime(1), ValueEasing.Stable(990));
                        builderY.Insert(BeatTime(6f), ValueEasing.Stable(990));
                        for (int i = 0; i < 4; i++)
                        {
                            builderY.Insert(BeatTime(9), ValueEasing.EaseOutElastic(-220, 0, BeatTime(9)));
                            builderY.Insert(BeatTime(7), ValueEasing.Stable(990));
                        }
                        builderY.Run(s => yDelta2 = s);
                    });
                    RegisterFunctionOnce("Spread", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation + 21 + t * 15, BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 + 21 + t * 15, BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }

                        ValueEasing.EaseBuilder builderz = new();
                        builderz.Insert(BeatTime(2), ValueEasing.EaseOutCirc(2.4f, 0, BeatTime(2)));
                        builderz.Run(s => ScreenDrawing.ScreenAngle = s * 1.6f);
                        builderz.Run(s => ScreenDrawing.ScreenScale = s * 0.012f + 1.1f);
                    });
                    RegisterFunctionOnce("SpreadN", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation - (21 + t * 15), BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 - (21 + t * 15), BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }

                        ValueEasing.EaseBuilder builderz = new();
                        builderz.Insert(BeatTime(2), ValueEasing.EaseOutCirc(-2.4f, 0, BeatTime(2)));
                        builderz.Run(s => ScreenDrawing.ScreenAngle = s * 1.6f);
                        builderz.Run(s => ScreenDrawing.ScreenScale = -s * 0.012f + 1.05f);
                    });
                    RegisterFunctionOnce("Zoom", () =>
                    {
                        ValueEasing.EaseBuilder zoom = new();
                        zoom.Insert(BeatTime(64), ValueEasing.EaseOutQuart(1.2f, 1.00f, BeatTime(64)));
                        zoom.Run(s => ScreenDrawing.ScreenScale = s);
                    });
                    string[] rhythm = { 
                    //pre  
                    "Run", "", "", "", "", "", "", "", 
                    //cur  
                    "($00{X})($20{X})", "", "", "D1", "", "", "($00{Y})($20{Y})", "",
                    "", "D1", "", "", "D", "", "(D)", "",
                    "($01{X})($21{X})", "", "", "D", "", "", "($01{Y})($21{Y})", "",
                    "", "D", "", "", "D1", "", "(D1)", "",
                    "($00{X})($20{X})", "", "", "D1", "", "", "($00{Y})($20{Y})", "",
                    "", "D1", "", "", "D", "", "(D)", "",
                    "($01{X})($21{X})", "", "", "D", "", "", "($01{Y})($21{Y})", "",
                    "", "D", "", "", "^D1'1.5", "", "(^$21'1.5)", "",

                    "#31#$1(#31#$31)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "", "",
                    "$00($21)",
                };
                    string[] rhythm2 = {
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur 
                    "ConvulseX", "", "", "", "", "", "ConvulseX", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseY", "", "", "", "", "", "ConvulseY", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseX", "", "", "", "", "", "ConvulseX", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseY", "", "", "", "", "", "ConvulseY", "",
                    "", "", "", "", "SpreadN", "", "Spread", "",
                    "Zoom", "", "", "", "", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm2);
                    Arrow[] allX = new Arrow[0];
                    Arrow[] allY = new Arrow[0];
                    DelayBeat(1, () => { allX = GetAll<Arrow>("X"); allY = GetAll<Arrow>("Y"); });
                    ForBeat120(80, () =>
                    {
                        foreach (Arrow arr in allX)
                        {
                            arr.Offset = new(0, yDelta1);
                            arr.Alpha = AlphaCalc(yDelta1);
                        }
                        foreach (Arrow arr in allY)
                        {
                            arr.Offset = new(0, yDelta2);
                            arr.Alpha = AlphaCalc(yDelta2);
                        }
                    });
                }
            }

            private bool notRegistered = true;

            private class Rainer : Entity
            {
                public float Intensity { set; private get; } = 0;
                public float Speed { set; private get; } = 3f;

                public Rainer()
                {
                    Rotation = 10f;
                    UpdateIn120 = true;
                }

                private class WindParticle : Entity
                {
                    private readonly float randVal = 0;
                    private readonly Rainer rainer;
                    private readonly float alpha = 0;
                    private readonly float speed = 0;

                    public WindParticle(Rainer rainer)
                    {
                        UpdateIn120 = true;
                        speed = Rand(0f, 1f) + rainer.Speed;
                        Rotation = Rand(-2f, 2f);
                        alpha = Rand(0.3f, 0.5f);
                        this.rainer = rainer;
                        Centre = new(Rand(-50, 690), -15);
                        randVal = Rand(0f, 1f);
                    }

                    public override void Draw()
                    {
                        if (rainer.Intensity <= randVal) return;
                    }

                    public override void Update()
                    {
                        Vector2 del = GetVector2(speed / 1.5f, rainer.Rotation + Rotation + 90);
                        Centre += del;
                        if (Centre.Y > 499) Dispose();
                    }
                }
                private class RainDrop : Entity
                {
                    private readonly float length;
                    private readonly float randVal = 0;
                    private readonly Rainer rainer;
                    private readonly float alpha = 0;
                    private readonly float speed = 0;

                    public RainDrop(Rainer rainer)
                    {
                        controlLayer = rainer.controlLayer;
                        UpdateIn120 = true;
                        speed = Rand(0f, 1f) + rainer.Speed;
                        Rotation = Rand(-2f, 2f);
                        alpha = Rand(0.3f, 0.5f);
                        this.rainer = rainer;
                        Centre = new(Rand(-50, 690), -15);
                        length = Rand(6, 11) + rainer.Speed * 1f;
                        randVal = Rand(0f, 1f);
                    }

                    public override void Draw()
                    {
                        if (rainer.Intensity <= randVal) return;
                        Vector2 del = GetVector2(length / 2, rainer.Rotation + Rotation + 90);
                        DrawingLab.DrawLine(Centre + del, Centre - del, 2, Color.LightBlue * alpha, 0.0f);
                    }

                    public override void Update()
                    {
                        Vector2 del = GetVector2(speed / 1.5f, rainer.Rotation + Rotation + 90);
                        Centre += del;
                        if (Centre.Y > 499) Dispose();
                    }
                }

                public override void Draw()
                {
                }

                public override void Update()
                {
                    var drop = new RainDrop(this);
                    AddChild(drop);
                }
            }

            private Rainer rainer;
            private ScreenDrawing.Shaders.RGBSplitting splitter = new();

            class FakeArrow : AutoEntity
            {
                public FakeArrow(int color, int rotateType)
                {
                    Image = FightResources.Sprites.arrow[color, rotateType, 0];
                }
                public override void Update()
                {

                }
            }

            public void Extreme()
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
                    "(D)(+01)(LineUp)(RotateL)", "", "", "", "(D)(+01)(LineDown)", "", "", "",
                    "(D)(+01)(LineUp)", "", "", "", "(D)(+01)(LineDown)", "", "", "(Blur)",
                    "(D)(+01)(LineUp)", "", "", "", "(D)(+01)(LineDown)", "", "", "",
                    "(D)(+01)(LineUp)", "", "", "", "(D)(+01)(LineDown)", "", "", "(Blur)",
                    "(D)(+01)(LineUp)(RotateR)", "", "(D)(+01)(LineUp2)", "", "(D)(+01)(LineDown)", "", "(D)(+01)(LineDown2)", "",
                    "(D)(+01)(LineUp)", "", "(D)(+01)(LineUp2)", "", "(D)(+01)(LineDown)", "", "(D)(+01)(LineDown2)", "(Blur)",
                    "(D)(+01)(LineUp)", "", "(D)(+01)(LineUp2)", "", "(D)(+01)(LineDown)", "", "(D)(+01)(LineDown2)", "",
                    "(D)(+01)(LineUp)(Cross)", "", "(D)(+01)(LineUp2)", "", "(D)(+01)(LineDown)", "", "(D)(+01)(LineDown2)", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.0f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                if (InBeat(128 - 8))
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                    "", "", "", "", "", "", "", "(Blur)",

                    //cur
                    "D0(#12#D1)(RotateL)(MovePosition)", "", "D0", "", "D0", "", "D0", "",
                    "D0", "", "D0", "", "D0", "", "D0", "(Blur2)",
                    "D0(D1)", "", "D0", "", "D0", "", "D0", "",
                    "D0(D1)", "", "D0", "", "D0", "", "D0", "(Blur)",
                    "D1(#12#D0)(RotateR)", "", "D1", "", "D1", "", "D1", "",
                    "D1", "", "D1", "", "D1", "", "D1", "(Blur2)",
                 //   "D1", "", "D1", "", "D1", "(Up)", "D1", "",
                  //  "(RotateMid)(Black)", "", "", "",  
                    "D1(D0)", "", "D1", "", "D1", "", "D1", "",
                    "D1(D0)", "", "D1", "", "D1", "", "D1", "(Blur)",
                    "D0(#12#D1)(RotateL)", "", "D0", "", "D0", "", "D0", "",
                    "D0", "", "D0", "", "D0", "", "D0", "(Blur2)",
                    "D0(D1)", "", "D0", "", "D0", "", "D0", "",
                    "D0(D1)", "", "D0", "", "D0", "", "D0", "(Blur)",
                    "D1(#12#D0)(RotateR)", "", "D1", "", "D1", "", "D1", "",
                    "D1", "", "D1", "", "D1", "", "D1", "(Blur2)",
                    "D1", "", "D1", "", "D1", "(Up)", "D1", "",
                    "(RotateMid)(Black)", "", "", "",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.0f, rhythm[i]);
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
                    "$10'1.5", "($11'1.5)", "$10'1.5", "(^$11'1.5)", "$30'1.5", "($31'1.5)", "$30'1.5", "(^$31'1.5)",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#16#$01)(#16#$20)(ConvulseR)(ConvulseR2)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>$20)", "<$01(<^$20)", ">^$01(>^$20)",
                    "$10'1.5", "($11'1.5)", "$10'1.5", "(^$11'1.5)", "$30'1.5", "($31'1.5)", "$30'1.5", "(^$31'1.5)",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "",
                    "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "(BlurLong)",
                    "(#10#$00)(#10#$21)(ConvulseL)(ConvulseL2)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>$21)", "<$00(<^$21)", ">^$00(>^$21)",
                    "", "", "", "Blur", "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "Blur",
                    "(D0)(^+0'2)(D1)(^+01'2)(ConvulseR)", "", "", "", "", "", "", "Blur",
                    "(D0)(^+0'2)(D1)(^+01'2)(ConvulseL)", "", "", "", "($0)($21)", "($2)($01)", "($0)($21)", "(BlurLong)",
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
                    "ConvulseL(D)(D1)(LineL)(Down)", "", "D1", "", "ConvulseL(D)(D1)(LineL)", "", "D1", "Blur",
                    "ConvulseL(D)(D1)(LineL)", "", "D1", "", "ConvulseL(D)(D1)(LineL)", "", "D1", "Blur",
                    "ConvulseL(D)(D1)(LineL)", "", "D1", "", "ConvulseL(D)(D1)(LineL)", "", "D1", "Blur",
                    "ConvulseL(D)(D1)(LineL)", "", "D1", "", "ConvulseL(D)(D1)(LineLRev)", "", "D1", "Blur",
                    "ConvulseR(D1)(D)(LineR)", "", "D", "", "ConvulseR(D1)(D)(LineR)", "", "D", "Blur",
                    "ConvulseR(D1)(D)(LineR)", "", "D", "", "ConvulseR(D1)(D)(LineR)", "", "D", "Blur",
                    "ConvulseR(D1)(D)(LineR)", "", "D", "", "ConvulseR(D1)(D)(LineR)", "", "D", "Blur",
                    "ConvulseR(D1)(D)(LineR)", "", "D", "", "ConvulseR(D1)(D)(LineRRev)", "", "D", "Blur",
                    "ConvulseL(D)(D1)(LineL)(Up)", "", "D1", "", "ConvulseL(D)(D1)(LineL)(Up)", "", "D1", "Blur",
                    "ConvulseL(D)(D1)(LineL)(Up)", "", "D1", "", "ConvulseL(D)(D1)(LineL)(Up)", "", "D1", "Blur",
                    "ConvulseL(D)(D1)(LineL)(Up)", "", "D1", "", "ConvulseL(D)(D1)(LineL)(Up)", "", "D1", "Blur",
                    "ConvulseL(D)(D1)(LineL)(Up)", "", "D1", "", "ConvulseL(D)(D1)(LineLRev)(Up)", "", "D1", "Blur",
                    "ConvulseR(D1)(D)(LineR)(Up)", "", "D", "", "ConvulseR(D1)(D)(LineR)(Up)", "", "D", "Blur",
                    "ConvulseR(D1)(D)(LineR)(Up)", "", "D", "", "ConvulseR(D1)(D)(LineR)(Up)", "", "D", "Blur",
                    "ConvulseR(D1)(D)(LineR)(Up)", "", "D", "", "ConvulseR(D1)(D)(LineR)(Up)", "", "D", "Blur",
                    "ConvulseR(D1)(D)(LineR)(Up)", "", "D", "", "ConvulseR(D1)(D)(LineRRev)(Up)", "", "D", "Blur",
                };
                    float time = 0;
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
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
                        // sinwave shake
                        ScreenDrawing.Shaders.Filter filter;
                        ScreenDrawing.SceneRendering.InsertProduction(filter = new ScreenDrawing.Shaders.Filter(SinWave, 0.6755f));
                        DelayBeat(16, () =>
                        {
                            filter.Dispose();
                        });
                        float a = 0;
                        ForBeat(16, () =>
                        {
                            SinWave.Parameters["time"].SetValue(a); a += 0.26f;
                            SinWave.Parameters["time2"].SetValue(a * 1.5f);
                        });

                        ValueEasing.EaseBuilder rgbShake = new();
                        rgbShake.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(0, 1.8f, BeatTime(1)));
                        rgbShake.Insert(BeatTime(7), ValueEasing.EaseOutCubic(1.8f, 0.6f, BeatTime(7)));
                        rgbShake.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1, 8, BeatTime(1)));
                        rgbShake.Insert(BeatTime(7), ValueEasing.EaseOutCubic(8, 0, BeatTime(7)));
                        rgbShake.Run(s =>
                        {
                            SinWave.Parameters["frequency"].SetValue(s * 15f + 150f);
                            SinWave.Parameters["frequency2"].SetValue(s * 35f + 220f);
                            SinWave.Parameters["range2"].SetValue(s);
                            SinWave.Parameters["range"].SetValue(s * 1.25f);
                        });

                        //background color
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

                    "D(LineL2)(ConvulseL)", "+01(LineL2)(Blur)", "D(LineR2)", "+01(LineR2)(Blur)", "D(LineL2)(ConvulseL)", "+01(LineL2)(Blur)","D(LineR2)", "+01(LineR2)(Blur)",
                    "D(LineL2)(ConvulseR)", "+01(LineL2)(Blur)", "D(LineR2)", "+01(LineR2)(Blur)", "D(LineL2)(ConvulseR)", "+01(LineL2)(Blur)","D(LineR2)", "+01(LineR2)(BlurLong2)",
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
                            ScreenDrawing.ScreenPositionDetla = s;
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
                    "(RotateL)D", "", "", "", "D(D1)", "", "", "D1",
                    "+00", "", "D1", "", "D1", "", "", "",
                    "(RotateL)D", "", "", "D", "(+0)(D1)", "", "D1", "+01",
                    "", "D0", "+00", "", "D1", "", "", "",
                    "(RotateR)D1", "", "", "", "D1(D0)", "", "", "D0",
                    "+01", "", "D0", "", "D0", "", "", "",
                    "(RotateR)D1", "", "", "D1", "(+01)(D0)", "", "D1", "+01",
                    "", "D0", "+01", "+20", "+01", "", "D0", "",
                    "(RotateL2)D", "", "", "", "D(D1)", "", "", "D1",
                    "+00", "", "D1", "", "D1", "", "", "",
                    "D", "", "", "D", "(+0)(D1)", "", "D1", "+01",
                    "", "D0", "+00", "", "D1", "", "", "",
                    "(RotateR2)D1", "", "", "", "D1(D0)", "", "", "D0",
                    "+01", "", "D0", "", "D0", "", "", "",
                    "D1", "", "", "D1", "(+01)(D0)", "", "D1", "+01",
                    "D0", "D0", "+01", "+20", "+01", "", "D0", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
                ExtremeBefore4K();
                Extreme4K();
                if (InBeat(1408 - 16 - 1))
                {
                    DelayEnabled = true;

                    DelayBeat(12, Regenerate);

                    RegisterFunctionOnce("Shatter", () =>
                    {
                        DelayBeat(0.875f, () =>
                        {
                            HeartAttribute.InvincibleToPhysics = false;
                            // sinwave shake
                            ScreenDrawing.Shaders.Filter filter;
                            ScreenDrawing.SceneRendering.InsertProduction(filter = new ScreenDrawing.Shaders.Filter(SinWave, 0.6755f));
                            DelayBeat(15, () =>
                            {
                                rainer.controlLayer = Surface.Normal;
                                filter.Dispose();
                                production1.Dispose();
                                ScreenDrawing.UpExtending = 0;
                                cameraProduction.Dispose();
                                ScreenDrawing.ScreenPositionDetla = Vector2.Zero;
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
                            DelayBeat(15.01f, () =>
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
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    DelayBeat(9 + 7, () =>
                    {
                        BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm2);
                    });
                }
                if (InBeat(1536 - 8))
                {
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(680, 240), new(240, 240), BeatTime(24)), (s) => 90);
                        CreateEntity(line);
                        line.Alpha = 0.65f;
                        line.AlphaDecrease(BeatTime(24));
                        line.InsertRetention(new(1.5f, 0.5f));
                        line.InsertRetention(new(3f, 0.3f));
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(-40, 240), new(400, 240), BeatTime(24)), (s) => 90);
                        CreateEntity(line);
                        line.Alpha = 0.65f;
                        line.AlphaDecrease(BeatTime(24));
                        line.InsertRetention(new(1.5f, 0.5f));
                        line.InsertRetention(new(3f, 0.3f));
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
                    RegisterFunctionOnce("RotateL2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutCubic(0, 4, BeatTime(16)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                            ScreenDrawing.ScreenScale = 1 + s * 0.01f;
                        });
                    });
                    RegisterFunctionOnce("RotateR2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(16), ValueEasing.EaseOutCubic(4, -4, BeatTime(16)));
                        builder.Insert(BeatTime(7), ValueEasing.EaseOutCubic(-4, 0, BeatTime(7)));
                        builder.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(16), ValueEasing.EaseOutCubic(1.04f, 1.12f, BeatTime(16)));
                        builder2.Insert(BeatTime(7), ValueEasing.EaseOutCubic(1.12f, 1.0f, BeatTime(7)));
                        builder2.Run(s =>
                        {
                            ScreenDrawing.ScreenScale = s;
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
                    RegisterFunctionOnce("Flicker1", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Black * 0.64f);
                    });
                    RegisterFunctionOnce("Flicker2", () =>
                    {
                        ScreenDrawing.SceneOutScale = 2.5f;
                        ScreenDrawing.SceneOut(Color.Black, BeatTime(3.2f));
                    });

                    string[] rhythm = { 
                    //pre 
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "LineL(RotateL)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "LineR(RotateR)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateL2)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "(RotateR2)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "Flicker1", "", "Flicker1", "Flicker1",
                    "Flicker1", "Flicker1", "Flicker1", "", "Flicker2", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    string[] rhythm2 = { 
                    //pre 
                    "", "", "", "", "", "", "", "",  
                    //cur
                    "#13#D0(D1)", "", "", "", "", "", "", "",
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
                    "#13#D1(D)(D)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "#8#D0(D1)(D1)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm2);
                }
                if (InBeat(1664 - 8))
                {
                    float line1Alpha = 0.18f;
                    RegisterFunctionOnce("Lit", () =>
                    {
                        rainer.Speed = 5f;
                        rainer.Intensity = 0.65f;
                        DelayBeat(0.2f, () =>
                        {
                            ValueEasing.EaseBuilder boundDistance = new();
                            boundDistance.Insert(BeatTime(16), ValueEasing.EaseOutCubic(0, 112f, BeatTime(16)));
                            boundDistance.Run(s =>
                                    ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s
                                );
                            ValueEasing.EaseBuilder backColor = new();
                            backColor.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 0.5f, BeatTime(16)));
                            backColor.Insert(BeatTime(16), ValueEasing.Stable(0.5f));
                            backColor.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0.5f, 1f, BeatTime(16)));
                            backColor.Run(s =>
                                ScreenDrawing.BackGroundColor = Color.Lerp(Color.Transparent, Color.DimGray * 0.35f, s)
                            );

                            ScreenDrawing.BoundColor = Color.Silver * 0.55f;
                        });
                    });
                    RegisterFunctionOnce("LRotateLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(120, 480), new(-20, 200), BeatTime(8)), ValueEasing.EaseOutCubic(0, -90, BeatTime(8)));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("RRotateLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(520, 480), new(660, 200), BeatTime(8)), ValueEasing.EaseOutCubic(0, 90, BeatTime(8)));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("LRotateLine2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(-10, 280), new(120, -15), BeatTime(8)), ValueEasing.EaseOutCubic(90, 0, BeatTime(8)));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("RRotateLine2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(650, 280), new(520, -15), BeatTime(8)), ValueEasing.EaseOutCubic(-90, 0, BeatTime(8)));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });

                    RegisterFunctionOnce("Line2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuart(new(320, 500), new(320, -20), BeatTime(8)), ValueEasing.Stable(0));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(0.5f, 0.5f));
                        line.InsertRetention(new(1f, 0.3f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("LLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutCubic(new(320, 550), new(320, -80), BeatTime(8)), ValueEasing.Stable(11));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });
                    RegisterFunctionOnce("RLine", () =>
                    {
                        Line line = new(CentreEasing.EaseOutCubic(new(320, 550), new(320, -80), BeatTime(8)), ValueEasing.Stable(-11));
                        CreateEntity(line);
                        line.Alpha = line1Alpha;
                        line.InsertRetention(new(1f, 0.5f));
                        DelayBeat(8, line.Dispose);
                    });

                    RegisterFunctionOnce("Split", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutQuad(4.0f, 0.6f, BeatTime(4)));
                        builder.Run(s => splitter.Intensity = s);
                    });

                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.9f, BeatTime(1.1f), false);

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(6, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.9f, BeatTime(1.1f), true);

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-6, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });

                    RegisterFunctionOnce("BlueLine", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Line line = new(new Vector2(0, 240 + i * 7), new Vector2(640, 240 + i * 7));
                            CreateEntity(line);
                            line.Alpha = 0.3f;
                            line.DrawingColor = Color.LightSkyBlue;
                            line.AlphaDecrease(BeatTime(3));
                        }
                    });
                    RegisterFunctionOnce("RedLine", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Line line = new(new Vector2(0, 240 + i * 7), new Vector2(640, 240 + i * 7));
                            CreateEntity(line);
                            line.Alpha = 0.3f;
                            line.DrawingColor = Color.LightCoral;
                            line.AlphaDecrease(BeatTime(3));
                        }
                    });

                    float aRotation = 0;
                    float bRotation = 0;
                    RegisterFunctionOnce("ARun", () =>
                    {
                        ValueEasing.EaseBuilder aBuilder = new();
                        aBuilder.Adjust = false;
                        for (int j = 0; j < 4; j++)
                        {
                            int count = (j == 3) ? 2 : 3;
                            for (int i = 0; i < count; i++)
                            {
                                aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                                aBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                                aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                                aBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                            }
                            aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                            aBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                            aBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));

                            if (j == 2)
                            {
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-4, 4, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(4, -4, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-4, 4, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutCirc(4, 0, BeatTime(1)));
                            }
                            else if (j == 3)
                            {
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-3, 3, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(3, -3, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-3, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                                aBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                            }
                            else
                            {
                                aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                                aBuilder.Insert(BeatTime(1.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                                aBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                                aBuilder.Insert(BeatTime(1.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                            }
                        }
                        aBuilder.Run(s => aRotation = s);
                    });
                    RegisterFunctionOnce("BRun", () =>
                    {
                        ValueEasing.EaseBuilder bBuilder = new();
                        bBuilder.Adjust = false;
                        for (int j = 0; j < 4; j++)
                        {
                            int count = (j == 3) ? 2 : 3;
                            for (int i = 0; i < count; i++)
                            {
                                bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                                bBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                                bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                                bBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                            }
                            bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                            bBuilder.Insert(BeatTime(2.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                            bBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));

                            if (j == 2)
                            {
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(4, -4, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-4, 4, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(4, -4, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-4, 0, BeatTime(1)));
                            }
                            else if (j == 3)
                            {
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(3, -3, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-3, 3, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(3, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.Stable(0));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(9, 0, BeatTime(1)));
                                bBuilder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-9, 0, BeatTime(1)));
                            }
                            else
                            {
                                bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(4, -3, BeatTime(0.7f)));
                                bBuilder.Insert(BeatTime(1.3f), ValueEasing.EaseOutCirc(-3, 0, BeatTime(2.3f)));
                                bBuilder.Insert(BeatTime(0.7f), ValueEasing.Linear(-4, 3, BeatTime(0.7f)));
                                bBuilder.Insert(BeatTime(1.3f), ValueEasing.EaseOutCirc(3, 0, BeatTime(2.3f)));
                            }
                        }
                        bBuilder.Run(s => bRotation = s);
                    });

                    string[] rhythm = { 
                    //pre 
                    "", "", "", "", "", "", "", "Lit", 
                    //cur 
                    "LRotateLine(ARun)(BRun)(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine",
                    "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine",
                    "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine",
                    "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine", "LRotateLine(Split)(ConvulseL)", "RRotateLine",
                    "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2",
                    "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2",
                    "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2",
                    "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2", "LRotateLine2(Split)(ConvulseR)", "RRotateLine2",
                    "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine", "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine",
                    "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine", "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine",
                    "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine", "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine",
                    "LLine(Split)(ConvulseL)", "RLine", "LLine", "RLine", "LLine(Split)(ConvulseL)", "RLine(Split)(ConvulseL)", "LLine(Split)(ConvulseL)", "RLine",
                    "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2", "LRotateLine", "(RRotateLine)Line2", "LRotateLine2(Split)(ConvulseR)", "Line2(RRotateLine2)", "LRotateLine2", "Line2(RRotateLine2)",
                    "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2", "LRotateLine", "(RRotateLine)Line2", "LRotateLine2(Split)(ConvulseR)", "Line2(RRotateLine2)", "LRotateLine2", "Line2(RRotateLine2)",
                    "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2", "LRotateLine", "(RRotateLine)Line2", "LRotateLine2(Split)(ConvulseR)", "Line2(RRotateLine2)(Split)(ConvulseL)", "LRotateLine2(Split)(ConvulseR)", "Line2(RRotateLine2)",
                    "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2(Split)(ConvulseL)", "LRotateLine(Split)(ConvulseR)", "(RRotateLine)Line2(Split)(ConvulseL)", "(Split(ConvulseR))",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 6.0f, rhythm);
                    string[] rhythm2 = { 
                    //pre 
                    "", "", "", "", "", "", "", "",  
                    //cur 
                    "($01{A})($21{A})(#16#$30)(RedLine)", "", "", "", "($01{B})($21{B})(RedLine)", "", "", "",
                    "($01{A})($21{A})(RedLine)", "", "", "", "($01{B})($21{B})(RedLine)", "", "", "",
                    "($01{A})($21{A})(RedLine)", "", "", "", "($01{B})($21{B})(#3#$10)(RedLine)", "", "", "",
                    "($01{A})($21{A})(RedLine)", "", "(#3#$30)", "", "($01{B})($21{B})(RedLine)", "", "($01{B})($21{B})(RedLine)", "",
                    "($00{A})($20{B})(#16#$31)(BlueLine)", "", "", "", "($00{B})($20{A})(BlueLine)", "", "", "",
                    "($00{A})($20{B})(BlueLine)", "", "", "", "($00{B})($20{A})(BlueLine)", "", "", "",
                    "($00{A})($20{B})(BlueLine)", "", "", "", "($00{B})($20{A})($11)(BlueLine)", "", "", "",
                    "($00{A})($20{B})($31)(BlueLine)", "", "(#3#$11)", "", "($00{B})($20{A})(BlueLine)", "", "($00)($20{A})(BlueLine)", "",
                    "($01{B})($21{B})(#8#$30)(RedLine)", "", "", "", "($01{A})($21{A})(RedLine)", "", "", "",
                    "($01{B})($21{B})(RedLine)", "", "", "", "($01{A})($21{A})($10)(RedLine)", "", "", "",
                    "($01{B})($21{B})(#4#$30)(RedLine)", "", "", "", "($01{A})($21{A})(RedLine)", "", "", "",
                    "($01{B})($21{B})(#4#$10)(RedLine)", "", "", "", "(*$01{A})(*$21{B})(RedLine)", "(*$00{B})(*$20{A})(BlueLine)", "(*$01{A})(*$21{B})(RedLine)", "",
                    "($00{B})($20{A})(#6#$31)(BlueLine)", "", "", "", "($00{A})($20{B})(BlueLine)", "", "", "",
                    "($00{B})($20{A})(BlueLine)", "", "", "", "($00{A})($20{B})(BlueLine)", "", "", "",
                    "($00{B})($20{A})(BlueLine)", "", "", "", "(*$00{A})(*$20{B})(BlueLine)", "(*$01{B})(*$21{A})(RedLine)", "(*$00{A})(*$20{B})(BlueLine)", "",
                    "(*$00{B})(*^$20'1{A})(BlueLine)", "(*^$01'1{A})(*$21{B})(RedLine)", "(*$00{B})(*^$20'1{A})(BlueLine)", "(*^$01'1{A})(*$21{B})(RedLine)", "(*$00{B})(*^$20'1{A})(BlueLine)", "", "", "",
                    "", "", "", "", "", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm2);

                    ForBeat120(1, 144, () =>
                    {
                        Arrow[] tagA = GetAll<Arrow>("A");
                        foreach (Arrow a in tagA)
                        {
                            a.CentreRotationOffset = aRotation;
                        }
                        Arrow[] tagB = GetAll<Arrow>("B");
                        foreach (Arrow b in tagB)
                        {
                            b.CentreRotationOffset = bRotation;
                        }
                    });
                }
                if (InBeat(1792 - 16))
                {
                    Vector2 XDelta, YDelta, ZDelta, WDelta;
                    XDelta = YDelta = ZDelta = WDelta = Vector2.Zero;

                    RegisterFunctionOnce("PreArrow", () =>
                    {
                        float delay = BeatTime(11.8f);
                        for (int time = 0; time <= 7; time++)
                        {
                            int count = (time % 2 == 0) ? 1 : 0;
                            int dir = count * 2 - 1;
                            for (int ctype = 0; ctype <= count; ctype++)
                            {
                                Arrow arr = MakeArrow(delay, "D", 7.0f, ctype, 0);
                                DelayBeat(11, () =>
                                {
                                    arr.Delay(BeatTime(4));

                                    ValueEasing.EaseBuilder builder = new();
                                    builder.Insert(BeatTime(5), ValueEasing.EaseOutQuart(90, 0, BeatTime(5)));
                                    builder.Run(s =>
                                    {
                                        arr.CentreRotationOffset = s * dir;
                                    });
                                });
                                arr.CentreRotationOffset = 90 * dir;

                                CreateEntity(arr);
                            }
                            delay += BeatTime(2);
                        }
                    });

                    RegisterFunctionOnce("CrossLine", () =>
                    {
                        for (int dir = -1; dir <= 1; dir += 2)
                        {
                            for (int x = 0; x <= 1; x++)
                            {
                                float rotation = x * 90;

                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(1.3f), ValueEasing.Stable(rotation));
                                builder.Insert(BeatTime(5), ValueEasing.EaseOutQuart(0, 90 * dir, BeatTime(3.6f)));
                                builder.Insert(BeatTime(3), ValueEasing.Linear(0, 20 * dir, BeatTime(3)));

                                Line line = new(CentreEasing.Stable(320, 240), builder.GetResult());
                                line.Alpha = 0.0f;
                                line.DrawingColor = Color.White;
                                line.AlphaIncrease(BeatTime(1.3f), 0.2f);
                                DelayBeat(3, () => line.AlphaDecrease(BeatTime(6)));

                                CreateEntity(line);
                            }
                        }
                    });

                    float delta = 0;
                    RegisterFunctionOnce("LConvulse", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.5f, BeatTime(0.75f), false);
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(-12, 0, BeatTime(0.75f)));
                        builder.Run(s => delta = s);
                    });
                    RegisterFunctionOnce("RConvulse", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.5f, BeatTime(0.75f), true);
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1), ValueEasing.EaseOutQuart(12, 0, BeatTime(0.75f)));
                        builder.Run(s => delta = s);
                    });

                    RegisterFunctionOnce("BlueLine", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Line line = new(CentreEasing.EaseOutQuart(new Vector2(320, 240 + i * 22), new(320, 240 + i * 8), BeatTime(2)), ValueEasing.Stable(0));
                            CreateEntity(line);
                            line.Alpha = 0.0f;
                            line.DrawingColor = Color.LightSkyBlue;
                            line.AlphaIncreaseAndDecrease(BeatTime(2.1f), 0.25f);
                        }
                    });
                    RegisterFunctionOnce("RedLine", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            Line line = new(CentreEasing.EaseOutQuart(new Vector2(320, 240 + i * 22), new(320, 240 + i * 8), BeatTime(2f)), ValueEasing.Stable(0));
                            CreateEntity(line);
                            line.Alpha = 0.0f;
                            line.DrawingColor = Color.LightCoral;
                            line.AlphaIncreaseAndDecrease(BeatTime(2.1f), 0.25f);
                        }
                    });

                    RegisterFunctionOnce("LConvulse2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.9f, BeatTime(1.0f), false);

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(7, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunctionOnce("RConvulse2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(1.9f, BeatTime(1.0f), true);

                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-7, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });

                    RegisterFunctionOnce("LineL2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.32f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.319f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR2", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.32f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.319f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineL", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(640, 240), new(0, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.32f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.319f));
                        DelayBeat(5, () => line.Dispose());
                    });
                    RegisterFunctionOnce("LineR", () =>
                    {
                        Line line = new(CentreEasing.EaseOutQuad(new(0, 240), new(640, 240), BeatTime(4)), (s) => { return 90.1f; });
                        line.Alpha = 0.32f;
                        line.InsertRetention(new(0.5f, 0.4f));
                        line.InsertRetention(new(1f, 0.3f));
                        CreateEntity(line);
                        DelayBeat(3, () => line.AlphaDecrease(BeatTime(1), 0.319f));
                        DelayBeat(5, () => line.Dispose());
                    });

                    string[] rhythm2 = { 
                    //pre 
                    "PreArrow", "", "", "", "", "", "", "",
                    "", "", "CrossLine", "", "", "", "", "",  
                    //cur  
                    "(LConvulse)LineL", "", "", "", "(LConvulse)LineL", "", "", "",
                    "(LConvulse)LineL", "", "", "", "(LConvulse)LineL", "", "", "",
                    "D(D1)(LConvulse)(LineL)", "", "D", "", "D(D1)(LConvulse)(LineL)", "", "D", "",
                    "D(D1)(LConvulse)(LineL)", "", "D", "", "D(D1)(LConvulse)(LineL2)", "", "D", "",
                    "D(D1)(RConvulse)(LineR)", "", "D1", "", "D(D1)(RConvulse)(LineR)", "", "D1", "",
                    "D(D1)(RConvulse)(LineR)", "", "D1", "", "D(D1)(RConvulse)(LineR)", "", "D1", "",
                    "D(D1)(RConvulse)(LineR)", "", "D1", "", "D(D1)(RConvulse)(LineR)", "", "D1", "",
                    "D(D1)(RConvulse)(LineR)", "", "D1", "", "D(D1)(RConvulse)(LineR2)", "", "D1", "",
                    "(*$01{X,T})(*$21{Y,T})R(LConvulse)(RedLine)(LineL)", "", "(*$01{X,T})(*$21{Y,T})R(LConvulse)(RedLine)(LineL)", "", "(*$01{X,T})(*$21{Y,T})R(LConvulse)(RedLine)(LineL)", "", "(*$01{X,T})(*$21{Y,T})R(LConvulse)(RedLine)(LineL)", "",
                    "(*$01{X,T})(*$21{Y,T})R(LConvulse)(RedLine)(LineL)", "", "(*$01{X,T})(*$21{Y,T})R(LConvulse)(RedLine)(LineL)", "", "(*$01{X,T})(*$21{Y,T})$30(LConvulse)(RedLine)(LineL)", "", "(*$01{X,T})(*$21{Y,T})R(LConvulse)(RedLine)(LineL)", "",
                    "(*$00{Y,T})(*$20{X,T})R1(RConvulse)(BlueLine)(LineR)", "", "(*$00{Y,T})(*$20{X,T})R1(RConvulse)(BlueLine)(LineR)", "", "(*$00{Y,T})(*$20{X,T})R1(RConvulse)(BlueLine)(LineR)", "", "(*$00{Y,T})(*$20{X,T})R1(RConvulse)(BlueLine)(LineR)", "",
                    "(*$00{Y,T})(*$20{X,T})R1(RConvulse)(BlueLine)(LineR)", "", "(*$00{Y,T})(*$20{X,T})R1(RConvulse)(BlueLine)(LineR)", "", "(*$00{Y,T})(*$20{X,T})$31(RConvulse)(BlueLine)(LineR)", "", "(*$00{Y,T})(*$20{X,T})$11(RConvulse)(BlueLine)(LineR)", "",
                    "(*$01{X,T})(*$21{Y,T})(LConvulse)(RedLine)(LineL)", "$1(RConvulse)(LineL)", "(*$01{X,T})(*$21{Y,T})(LConvulse)(RedLine)(LineL)", "$1(RConvulse)(LineL)", "(*$01{X,T})(*$21{Y,T})(LConvulse)(RedLine)(LineL)", "$1(RConvulse)(LineL)", "(*$01{X,T})(*$21{Y,T})(LConvulse)(RedLine)(LineL)", "$10(RConvulse)(LineL)",
                    "(*$20{X,T})(*$00{Y,T})(LConvulse)(BlueLine)(LineR)", "$31(RConvulse)(LineR)", "(*$20{X,T})(*$00{Y,T})(LConvulse)(BlueLine)(LineR)", "$31(RConvulse)(LineR)", "(*$20{X,T})(*$00{Y,T})(LConvulse)(BlueLine)(LineR)", "$31(RConvulse)(LineR)", "(*$20{X,T})(*$00{Y,T})(LConvulse)(BlueLine)(LineR)", "$31(RConvulse)(LineR)",
                    "(*$01{X,T})(*$21{Y,T})(RConvulse2)(LineL)", "(LineR)", "(*<$11)(*>$31)(LConvulse2)(LineL)", "(*>$10)(*<$30)(LConvulse2)(LineR)", "(*<$11)(*>$31)(LConvulse2)(LineL)", "(LineR)", "(*<$11)(*>$31)(RConvulse2)(LineL)", "(*>$10)(*<$30)(RConvulse2)(LineR)",
                    "(LineL)", "(*>$10)(*<$30)(LConvulse2)(LineR)", "(*<$11)(*>$31)(LConvulse2)(LineL)", "(*>$10)(*<$30)(LConvulse2)(LineR)", "(*<$11)(*>$31)(LConvulse2)(LineL)", "(*>$10)(*<$30)(LConvulse2)(LineR)", "(*<$11)(*>$31)(LConvulse2)(LineL)", "(*>$10)(*<$30)(LConvulse2)(LineR)",
                };

                    ForBeat120(1, 144, () =>
                    {
                        Arrow[] tagA = GetAll<Arrow>("X");
                        foreach (Arrow a in tagA)
                        {
                            float time = a.BlockTime - GametimeF;
                            time /= 15f;
                            a.Offset = new(delta * 0.4f, time * time * time * time + delta);
                            a.Alpha = MathUtil.Clamp(0, 4.1f - time * 1.5f, 0.85f);
                        }
                        Arrow[] tagB = GetAll<Arrow>("Y");
                        foreach (Arrow b in tagB)
                        {
                            float time = b.BlockTime - GametimeF;
                            time /= 15f;
                            b.Offset = new(delta * 0.4f, -time * time * time * time + delta);
                            b.Alpha = MathUtil.Clamp(0, 4.1f - time * 1.5f, 0.85f);
                        }
                    });
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm2);
                }
                ExtremeFinal();
            }

            private void ExtremeBefore4K()
            {
                if (InBeat(1024 - 8))
                {
                    SetPlayerBoxMission(0);
                    float time = BeatTime(0);
                    ScreenDrawing.UISettings.UISurfaceDrawing uiSurface = null;
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
                        ValueEasing.EaseBuilder remove = new();
                        remove.Insert(BeatTime(8), ValueEasing.EaseOutCubic(ScreenDrawing.DownBoundDistance, 0, BeatTime(8)));
                        remove.Run(s => ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = s);

                        ScreenDrawing.MakeFlicker(Color.Silver * 0.5f);
                        uiSurface = ScreenDrawing.UISettings.CreateUISurface();
                        rainer.controlLayer = uiSurface.UISurface;

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
                        production2.Dispose();
                        Blur.Dispose();
                        float time = BeatTime(8);

                        ScreenDrawing.MakeFlicker(Color.Black * 0.45f);
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
                                builder.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(xPos, -120), new(xPos, height), BeatTime(8)));
                                Line line = new(CentreEasing.Stable(xPos, -890), builder.GetResult());
                                CreateEntity(line);
                                line.DrawingColor = Color.Silver;
                                line.Width = 3.5f + MathF.Abs(i) * 0.6f;
                            }
                        });
                    });
                    RegisterFunctionOnce("Light", () =>
                    {
                        ScreenDrawing.SceneRendering.InsertProduction(lightProduction = new Lighting(0.821f));
                        lightProduction.AmbientColor = Color.White;
                        SimplifiedEasing.RunEase((s) =>
                        {
                            lightProduction.AmbientColor = Color.White * s;
                        }, false,
                            SimplifiedEasing.EaseOut(BeatTime(1), 1.0f, 0.5f, SimplifiedEasing.EaseState.Linear),
                            SimplifiedEasing.EaseOut(BeatTime(1), 0.9f, 0.5f, SimplifiedEasing.EaseState.Linear),
                            SimplifiedEasing.EaseOut(BeatTime(1), 0.85f, 0.5f, SimplifiedEasing.EaseState.Linear),
                            SimplifiedEasing.EaseOut(BeatTime(1), 0.8f, 0.5f, SimplifiedEasing.EaseState.Linear)
                        );
                        Lighting.Light lightWhite = new() { size = 700f, color = Color.White, position = new(320, 500) },
                                       lightRed1 = new() { size = 470f, color = Color.Lerp(Color.White, Color.Magenta, 0.8f), position = new(100, -20) },
                                       lightRed2 = new() { size = 470f, color = Color.Lerp(Color.White, Color.Magenta, 0.8f), position = new(540, -20) };
                        lightProduction.Lights.Add(lightWhite);
                        lightProduction.Lights.Add(lightRed1);
                        lightProduction.Lights.Add(lightRed2);
                    });
                    RegisterFunctionOnce("Image", () =>
                    {
                        ImageEntity ent;
                        CreateEntity(ent = new ImageEntity(SongIllustration) { Centre = new(320, 240), Depth = 0.667f, controlLayer = uiSurface.UISurface });
                        SimplifiedEasing.RunEase((s) =>
                        {
                            ent.Alpha = s;
                        }, false,
                            SimplifiedEasing.EaseIn(BeatTime(8), 0f, 0.2f, SimplifiedEasing.EaseState.Sine)
                        );
                        ent.BlendColor = Color.White;
                    });

                    string[] rhythm = { 
                    //pre
                    "", "", "", "", "", "", "", "",

                    //cur
                    "(RotateL)D", "", "", "", "D(D1)", "", "", "D1",
                    "+00", "", "D1", "", "D1", "", "", "",
                    "(RotateL)D", "", "", "D", "(+0)(D1)", "", "D1", "+01",
                    "", "D0", "+00", "", "D1", "", "", "",
                    "(RotateR)D1", "", "", "", "D1(D0)", "", "", "D0",
                    "+01", "", "D0", "", "D0", "", "", "",
                    "(RotateR2)D1", "", "", "D1", "(+01)(D0)", "", "D1", "+01",
                    "(RotateL2)", "D0", "+01", "+20", "+01", "", "D0", "Blur",
                    "(ConvulseL)(Line1)(D)(+01)", "", "(D)(+01)", "", "(D)(+011)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "(D)(^+01'2)", "", "(D)(+01)", "Blur",
                    "(ConvulseL)(Line2)(D)(+01)", "", "(D)(+01)", "", "(D)(+011)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "", "(D)(^+01'2)", "", "(D)(+01)", "Blur(Line3)",
                    "(ConvulseR)(Rise)(D)(+01)", "", "(D)(+01)", "+011", "(D)(^+01'2)", "", "(D)(+01)", "Blur(Line3)",
                    "(ConvulseR)(Rise)(D)(+01)", "", "(D)(+01)", "+011", "(D)(^+01'2)", "", "(D)(+01)", "Blur(Line3)(LineS)",
                    "(ConvulseR2)(Step1)", "", "", "", "", "", "", "Flicker",
                    "(ConvulseL2)(Step2)(Line4k)", "(Image)(Light)", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.4f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
            }
            private void Extreme4K()
            {
                if (InBeat(1152 - 8))
                {
                    DelayBeat(10, Regenerate);
                    float camRotation = 48;
                    RegisterFunctionOnce("Move", () =>
                    {
                        HeartAttribute.InvincibleToPhysics = true;

                        ScreenDrawing.UpExtending = 1.85f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(0, 0), new(0, 60), BeatTime(8)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                            builder.Insert(BeatTime(0.6f), ValueEasing.EaseOutCubic(0, 0.017f, BeatTime(0.6f)));
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(0.017f, 0.0f, BeatTime(1.1f)));
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
                            builder.Insert(BeatTime(0.6f), ValueEasing.EaseOutCubic(0, -0.017f, BeatTime(0.6f)));
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(-0.017f, 0.0f, BeatTime(1.1f)));
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
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(camRotation, camRotation - 2.2f, BeatTime(1.1f)));
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(camRotation - 2.2f, camRotation, BeatTime(1.1f)));
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
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(camRotation, camRotation + 2.2f, BeatTime(1.1f)));
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(camRotation + 2.2f, camRotation, BeatTime(1.1f)));
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

                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] { 
                    //pre
                    "Move(Rotate)", "", "", "", "", "", "", "",
                    //cur
                    "T0(T2)(T3)(Tag)(CameraZL)", "T0V", "T0V", "T0V",     //3
                    "!!12/3", "T1(Tag)", "T2", "T3",    "", "!!12/3", "T0", "T1", "T2",    "T3",  //4
                    "T0(T1)(Tag)(CameraUp)(CameraZL)", "", "", "T0(T1)(T2)(CameraUp)(CameraZR)",  //5
                    "T2V(Tag)", "T2V", "T1(T2)(T3)", "T1V",  //6
                    "!!3", "T1V(T0)(T3)(Tag)(CameraUp)", "(T0)(T3)(CameraZL)", "(T0)(T3)(CameraZR)",  //7
                    "T1(Tag)", "T1V", "T1V", "T1V", //8
                    "T2(T3)(T1V)(Tag)(CameraZL)", "", "!!8/4", "T0", "T1", "T2", "T3", //9
                    "T0(T1)(Tag)(CameraUp)", "T0V", "T0V(T2)(T3)(CameraZL)", "T3V", //10
                    "!!8/4", "T0(T3V)(Tag)(CameraDown)", "T1", "T2", "T0",    "T1", "T2(T3)(CameraZL)", //11
                    "T3V(T0)(Tag)", "T1(T2)(CameraZL)", "T0(T2V)", "T1", //12
                    "T2(T3)(Tag)(CameraZL)", "", "", "", //13
                    "T0(Tag)", "T1", "T3", "T2", //14
                    "!!12/3", "T0(Tag)(CameraUp)", "T1", "T2",    "T3", "", "", //15
                    "T3(Tag)", "T2", "T0", "T1", //16
                    "!!12/3", "T0(T3)(Tag)(CameraUp)", "T2", "T1",    "", "", "", //17
                    "!!8/4", "T0(Tag)", "T1", "T2", "T3",     "!!6/3", "T2", "T1", "T0", //18
                    "T1(T2)(T3)(Tag)(CameraZL)(CameraUp)", "T2V(T3V)", "T2V(T3V)", "T3V",  //19
                    "!!12/3", "T3V(T0)(Tag)(CameraUp)", "T1", "T2",   "T3V", "!!12/3", "(T0)(T3V)(CameraDown)", "T1", "T2",   "T3V", //20
                    "T3V(T0)(T1)(Tag)(CameraZL)", "T2", "T0", "T1(T2)", //21
                    "T2(Tag)", "T3", "T0", "T1", //22
                    "!!3", "T2(Tag)(T0)(CameraUp)(CameraZL)", "T2(CameraZR)", "T2(CameraZL)", //23
                    "T0(T1)(T3)(Tag)(CameraZL)", "T0V(T1V)(T3V)", "T0V(T1V)", "T0V", //24
                    "T2(T3)(Tag)", "", "!!16/4", "T0(CameraZL)(CameraUp)", "T1", "T2", "T3",    "", //25
                    "T0(T1)(Tag)", "T2", "(T1)(T3)", "", //26
                    "!!6/3", "T0(T3)(Tag)(CameraZL)(CameraDown)", "T2", "T1",    "", "T2(T3)", //27
                    "(Tag)", "T1(T2)", "", "", //28
                    "!!8", "T0(T1)(Tag)(CameraZR)(CameraUp)", "T2", "T3", "T0", "T1", "T2", "T3", "T0", //29
                    "T2(Tag)", "T3", "T0", "T1",  //30
                    "T2(T0)(T3)(Tag)(CameraZL)(CameraUp)", "T0V(T3V)", "T0V(T3V)", "",  //31
                    "T2(Tag)", "T1", "T2", "T1",  //32
                    "!!8/2", "T0(T3)(Tag)(CameraZL)(CameraUp)", "T2",    "T1", "T3", "", //33
                    "!!8/2", "T2(T3)(Tag)(CameraZL)(CameraUp)", "T1", "",     "!!8/2", "T0(T1)", "T2", "" //34

                });
                }
                if (InBeat(1280 - 8))
                {
                    float speed = 30f;
                    float scale = 2.5f;
                    float lateWaitingScale = 0.1f;
                    float camRotation = 48;
                    RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(8), CentreEasing.EaseOutQuad(new(0, 0), new(0, 60), BeatTime(8)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

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
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(0, 0.012f, BeatTime(1.1f)));
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(0.012f, 0.0f, BeatTime(1.1f)));
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
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(0, -0.012f, BeatTime(1.1f)));
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(-0.012f, 0.0f, BeatTime(1.1f)));
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
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(camRotation, camRotation - 1.2f, BeatTime(1.1f)));
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(camRotation - 1.2f, camRotation, BeatTime(1.1f)));
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
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(camRotation, camRotation + 1.2f, BeatTime(1.1f)));
                            builder.Insert(BeatTime(1.1f), ValueEasing.EaseOutCubic(camRotation + 1.2f, camRotation, BeatTime(1.1f)));
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

                    BarrageCreate(0, BeatTime(1) * 8, 8.0f, new[] {
                    //pre
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "T0(T1)(T3)(CameraDown)(CameraZL)", "T0V(T1V)(T3V)", "T0V(T1V)(T3V)", "T0V(T1V)(T3V)",    //35
                    "!!8/2", "T0V(T3)(CameraZL)", "T2",     "", "!!8/2", "T3", "T2",    "",    //36
                    "T2(T3)", "!!8/4", "", "T0", "T1", "T3",    "!!12/3", "T2(CameraZR)", "T1", "T0",   //37
                    "", "!!12/3", "T1(CameraZR)", "T2", "T3",     "", "T0",    //38
                    "!!3", "T1(T2)(T3)(CameraZL)(CameraUp)", "T1(T2)(CameraZR)", "T1(T2)(CameraZL)",    //39
                    "T0(T3)(CameraDown)", "T0V(T3V)", "T2(T0V)", "T0V(T1)",    //40
                    "T0V(T2)(T3)(CameraZL)", "", "!!16/4", "CameraZL(CameraDown)(T3)", "T2", "T1", "T0",    "",    //41
                    "T2(T3)(CameraZR)", "T0", "T1(T2)", "",    //42
                    "T0(T1)(T3)(CameraUp)", "T0V(T1V)", "", "T2(T1)",    //43
                    "", "T0(CameraZL)(T3)", "T1", "T2",    //44
                    "!!8/2", "T0(T1)(CameraZL)", "T2",   "", "", "T3",    //45
                    "T3", "T0", "T2", "T1",    //46
                    "(CameraDown)(T0)T3", "T0V(T3V)", "T0V(T3V)", "T0V(T3V)",    //47
                    "T0(T3V)", "T3", "T1", "T2",    //48
                    "!!6", "(CameraZR)T0(T1)", "T2", "T3", "T0", "T2", "T3",    //49
                    "!!16/4", "CameraUp(T1)", "T2", "T3", "",     "",   "!!16/4", "CameraUp(T2)", "T1", "T0", "",     "",    //50
                    "CameraZL(CameraDown)(T1)(T2)(T3)", "T3V", "T3V", "T3V",    //51
                    "!!8/2", "T3V(T0)", "T2",   "T3V",   "!!8/2", "T3V(T2)", "T0",   "T3V",    //52
                    "T0(T1)(CameraZL)", "", "", "T0(T3)(CameraZR)",    //53
                    "T1", "T0(T3)", "T2", "",    //54
                    "!!3", "(CameraZL)(CameraUp)T1", "(CameraZR)T1", "(CameraZL)T1",    //55
                    "CameraZL(CameraDown)(T0)(T2)(T3)", "T2V(T3V)(T0V)", "T0V", "T0V",    //56
                    "T0V(T2)(T3)(CameraZL)", "", "!!16/4", "T3(CameraZL)", "T2", "T1", "T0",     "",    //57
                    "(CameraZL)T2(T3)", "T0", "T1(T3)", "",    //58
                    "(CameraZL)(CameraDown)(T0)(T1)T2", "", "", "(CameraZR)T2(T3)",    //59
                    "T0", "T1(T2)(CameraZL)", "", "",    //60 
                    "T0(T1)(T3)(CameraZL)(CameraDown)", "", "T1", "",    //61
                    "T0(T2)(T3)(CameraZL)(CameraDown)", "", "T2", "T1",    //62 
                    "T0(T3)(CameraZR)(CameraUp)", "", "", "",    //63 
                });
                }
            }
            private void ExtremeFinal()
            {
                if (InBeat(1920 - 8))
                {
                    ScreenDrawing.BackGroundColor = Color.DimGray * 0.35f;
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(ScreenDrawing.ScreenAngle, -3f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(ScreenDrawing.ScreenAngle, 3f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 0f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });

                    RegisterFunctionOnce("MoveL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(9), ValueEasing.EaseOutBack(ScreenDrawing.ScreenPositionDetla.X, -11f, BeatTime(9)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDetla = new(s, 0);
                        });
                        ValueEasing.EaseBuilder rotate = new();
                        rotate.Insert(BeatTime(3), ValueEasing.EaseOutQuart(0, 2, BeatTime(3)));
                        rotate.Insert(BeatTime(1.5f), ValueEasing.EaseInQuad(2, 1, BeatTime(1.5f)));
                        rotate.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(1, 0.0f, BeatTime(1.5f)));
                        rotate.Insert(BeatTime(1), ValueEasing.Stable(0));
                        rotate.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("MoveR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(9), ValueEasing.EaseOutBack(ScreenDrawing.ScreenPositionDetla.X, 11f, BeatTime(9)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDetla = new(s, 0);
                        });
                        ValueEasing.EaseBuilder rotate = new();
                        rotate.Insert(BeatTime(3), ValueEasing.EaseOutQuart(0, -2, BeatTime(3)));
                        rotate.Insert(BeatTime(1.5f), ValueEasing.EaseInQuad(-2, -1, BeatTime(1.5f)));
                        rotate.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(-1, 0.0f, BeatTime(1.5f)));
                        rotate.Insert(BeatTime(1), ValueEasing.Stable(0));
                        rotate.Run(s =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("MoveMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenPositionDetla.X, 0, BeatTime(4)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenPositionDetla = new(s, 0);
                        });
                    });

                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.15f);
                        ScreenDrawing.CameraEffect.Convulse(0.1f, BeatTime(1.4f), false);
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2.2f, BeatTime(1.2f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2.2f, BeatTime(1.2f), true);
                    });
                    RegisterFunction("KickAL", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(BeatTime(2), ValueEasing.EaseInCubic(0.75f * 0.5f, 0, BeatTime(2)));
                        Line la = new(80, 90);
                        Line lb = new(80 + 60, 90);
                        Line[] l = { la, lb };
                        foreach (Line a in l)
                        {
                            CreateEntity(a);
                            alp.Run((s) => { a.Alpha = s; });
                            DelayBeat(2, () => { a.Dispose(); });
                        }
                    });
                    RegisterFunction("KickAR", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(BeatTime(2), ValueEasing.EaseInCubic(0.75f * 0.5f, 0, BeatTime(2)));
                        Line la = new(640 - 80, 90);
                        Line lb = new(640 - 80 - 60, 90);
                        Line[] l = { la, lb };
                        foreach (Line a in l)
                        {
                            CreateEntity(a);
                            alp.Run((s) => { a.Alpha = s; });
                            DelayBeat(2, () => { a.Dispose(); });
                        }
                    });
                    RegisterFunctionOnce("KickAp", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(BeatTime(2), ValueEasing.EaseInSine(0.45f * 0.5f, 0, BeatTime(2)));
                        Line la = new(new Vector2(320, 240 - 160), 0);
                        Line lb = new(new Vector2(320, 240 + 160), 0);
                        Line[] l = { la, lb };
                        foreach (Line a in l)
                        {
                            CreateEntity(a);
                            alp.Run((s) => { a.Alpha = s; });
                            DelayBeat(2, () => { a.Dispose(); });
                        }
                    });
                    RegisterFunction("KickBL", () =>
                    {
                        CentreEasing.EaseBuilder c1 = new();
                        c1.Insert(BeatTime(1.55f), CentreEasing.Linear(new Vector2(640, 240), new Vector2(0 - 160, 240), BeatTime(1.55f)));
                        CentreEasing.EaseBuilder c2 = new();
                        c2.Insert(BeatTime(1.55f), CentreEasing.Linear(new Vector2(640 + 160, 0), new Vector2(0, 0), BeatTime(1.55f)));
                        Line line = new(c1.GetResult(), c2.GetResult()) { Alpha = 0.85f * 0.65f };
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++)
                        {
                            int t = i;
                            line.InsertRetention(new(i * 4, 0.85f - 0.85f / 3 * t));
                        }
                        line.AlphaDecrease(BeatTime(3));
                        line.VerticalMirror = true;
                    });
                    RegisterFunction("KickBR", () =>
                    {
                        CentreEasing.EaseBuilder c1 = new();
                        c1.Insert(BeatTime(1.55f), CentreEasing.Linear(new Vector2(0, 240), new Vector2(640 + 160, 240), BeatTime(1.55f)));
                        CentreEasing.EaseBuilder c2 = new();
                        c2.Insert(BeatTime(1.55f), CentreEasing.Linear(new Vector2(0 - 160, 0), new Vector2(640, 0), BeatTime(1.55f)));
                        Line line = new(c1.GetResult(), c2.GetResult()) { Alpha = 0.85f * 0.65f };
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++)
                        {
                            int t = i;
                            line.InsertRetention(new(i * 4, 0.85f - 0.85f / 3 * t));
                        }
                        line.AlphaDecrease(BeatTime(3));
                        line.VerticalMirror = true;
                    });
                    RegisterFunctionOnce("KickBm", () =>
                    {
                        ValueEasing.EaseBuilder alp2 = new();
                        alp2.Insert(BeatTime(0.25f) - 2, ValueEasing.Stable(0.1f * 0.25f));
                        alp2.Insert(2, ValueEasing.Linear(0.1f * 0.25f, 0.65f * 0.25f, 2));
                        alp2.Insert(BeatTime(1.55f), ValueEasing.EaseInCubic(0.65f * 0.25f, 0, BeatTime(1.55f)));
                        ValueEasing.EaseBuilder alp3 = new();
                        alp3.Insert(BeatTime(0.5f) - 2, ValueEasing.Stable(0.1f * 0.25f));
                        alp3.Insert(2, ValueEasing.Linear(0.1f * 0.25f, 0.65f * 0.25f, 2));
                        alp3.Insert(BeatTime(1.55f), ValueEasing.EaseInCubic(0.65f * 0.25f, 0, BeatTime(1.55f)));
                        Line lb = new(new Vector2(320, 240), 45);
                        Line lc = new(new Vector2(320, 240), -45);
                        CreateEntity(lb);
                        CreateEntity(lc);
                        //CreateEntity(la1);
                        //CreateEntity(lb1);
                        //CreateEntity(lc1);
                        //CreateEntity(ld1); 
                        alp2.Run((s) => { lb.Alpha = s; });
                        alp3.Run((s) => { lc.Alpha = s; });
                        DelayBeat(2.3f, () =>
                        {
                            lb.Dispose();
                            lc.Dispose();
                            //la1.Dispose();
                            //lb1.Dispose();
                            //lc1.Dispose();
                            //ld1.Dispose(); 
                        });
                    });
                    string[] rhythm = { 
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "D(D1)(RotateL)", "", "", "(D)(D1)", "", "", "D(D1)", "",
                    "(D)(D1)(Flicker)(KickAR)", "", "(D)(D1)(KickAR)", "", "", "", "(D)", "",
                    "(D)(D1)(RotateR)", "", "", "D(D1)", "", "", "(D)(D1)", "",
                    "D(D1)(Flicker)(KickAL)", "", "(D1)(D)(KickAL)", "+2", "+2", "", "D", "",
                    "(D)(D1)(RotateL)", "", "", "D(D1)", "", "", "(D)(D1)", "",
                    "D(D1)(Flicker)(KickAR)", "", "(D)(D1)(KickAR)", "", "", "", "D(D1)", "",
                    "(D)(D1)(RotateMid)", "", "", "D(D1)", "", "", "(D)(D1)", "",
                    "D(Flicker)(KickAp)(KickAR)(KickAL)", "+2(KickAp)", "(+2)(KickAp)(KickAR)(KickAL)", "", "(D)(D1)", "", "D(D1)", "",

                    "(D)(D1)(MoveL)(KickBL)", "", "", "D(D1)", "", "", "(D)(D1)", "",
                    "D(D1)(Flicker)", "", "(D)(D1)", "", "", "", "D", "",
                    "(D)(D1)(MoveR)(KickBR)", "", "", "D(D1)", "", "", "(D)(D1)", "",
                    "D(D1)(Flicker)", "", "(D)(D1)", "+21", "+21", "", "D", "",
                    "(D)(D1)(MoveL)(KickBL)", "", "", "D(D1)", "", "", "(D)(D1)", "",
                    "D(D1)(Flicker)", "", "(D)(D1)", "", "", "", "D(D1)", "",
                    "(D)(D1)(MoveMid)(ConvulseL)(KickBR)", "", "", "", "^D'1.6(^D1'1.6)(ConvulseL)", "", "", "",
                    "(^D'1.6)(^D1'1.6)(ConvulseR)(KickBm)(KickAR)", "", "^D'1.6(^D1'1.6)(ConvulseR)(KickBm)(KickAL)", "", "", "", "(D)", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                }
                if (InBeat(2048 - 8))
                {
                    ScreenDrawing.BackGroundColor = Color.DimGray * 0.35f;
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(ScreenDrawing.ScreenAngle, -3f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutBack(ScreenDrawing.ScreenAngle, 3f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(8), ValueEasing.EaseOutCubic(ScreenDrawing.ScreenAngle, 0f, BeatTime(8)));
                        builder.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
                    });

                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.15f);
                        ScreenDrawing.CameraEffect.Convulse(0.1f, BeatTime(1.4f), false);
                    });
                    RegisterFunctionOnce("In", () =>
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            DelayBeat(i, () =>
                            {
                                ScreenDrawing.MakeFlicker(Color.White * 0.38f);
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(0.5f), ValueEasing.EaseOutCirc(0, 0.02f, BeatTime(0.5f)));
                                builder.Insert(BeatTime(0.5f), ValueEasing.Stable(0));
                                builder.OffsetPosition = ScreenDrawing.ScreenScale;
                                builder.Run(s =>
                                    ScreenDrawing.ScreenScale = s
                                );
                            });
                        }
                        DelayBeat(8, () =>
                        {
                            ValueEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(3), ValueEasing.EaseOutQuint(ScreenDrawing.ScreenScale, 1f, BeatTime(3)));
                            builder.Run(s =>
                                ScreenDrawing.ScreenScale = s
                            );
                        });
                    });

                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2.7f, BeatTime(1f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2.7f, BeatTime(1f), true);
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1f), ValueEasing.EaseOutCubic(0.06f, 0.01f, BeatTime(1)));
                        builder.OffsetPosition = ScreenDrawing.ScreenScale;
                        builder.Run(s => ScreenDrawing.ScreenScale = s);
                    });
                    RegisterFunctionOnce("Expand2", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1f), ValueEasing.EaseOutCubic(0.07f, 0.017f, BeatTime(1)));
                        builder.OffsetPosition = ScreenDrawing.ScreenScale;
                        builder.Run(s => ScreenDrawing.ScreenScale = s);
                    });
                    RegisterFunctionOnce("Reverse", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4f), ValueEasing.EaseOutQuad(ScreenDrawing.ScreenScale + 0.17f, 1f, BeatTime(4)));
                        builder.Run(s => ScreenDrawing.ScreenScale = s);
                        ScreenDrawing.MakeFlicker(Color.White * 0.45f);
                    });
                    RegisterFunctionOnce("Split", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(4), ValueEasing.EaseOutQuad(5.0f, 0.6f, BeatTime(4)));
                        builder.Run(s => splitter.Intensity = s);
                    });

                    RegisterFunctionOnce("Down", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-7, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunctionOnce("Up", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-7, 0, BeatTime(1.4f)));
                        builder.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunctionOnce("KickCR", () =>
                    {
                        DelayBeat(1, () =>
                        {
                            ValueEasing.EaseBuilder alp = new();
                            alp.Insert(BeatTime(0.5f) - 2, ValueEasing.EaseInCubic(0.75f * 0.5f, 0.3f * 0.5f, BeatTime(0.5f)));
                            alp.Insert(2, ValueEasing.Linear(0.3f * 0.5f, 0.75f * 0.5f, 2));
                            alp.Insert(BeatTime(2), ValueEasing.EaseInCubic(0.75f * 0.5f, 0, BeatTime(2)));
                            Line la = new(640 - 80, 90);
                            Line lb = new(640 - 80 - 60, 90);
                            Line[] l = { la, lb };
                            foreach (Line a in l)
                            {
                                CreateEntity(a);
                                alp.Run((s) => { a.Alpha = s; });
                                DelayBeat(2, () => { a.Dispose(); });
                            }
                        });
                        CentreEasing.EaseBuilder c1 = new();
                        c1.Insert(BeatTime(1.55f), CentreEasing.Linear(new Vector2(0, 240), new Vector2(640 + 160, 240), BeatTime(1.55f)));
                        CentreEasing.EaseBuilder c2 = new();
                        c2.Insert(BeatTime(1.55f), CentreEasing.Linear(new Vector2(0 - 160, 0), new Vector2(640, 0), BeatTime(1.55f)));
                        Line line = new(c1.GetResult(), c2.GetResult()) { Alpha = 0.85f };
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++)
                        {
                            int t = i;
                            line.InsertRetention(new(i * 4, 0.85f - 0.85f / 3 * t));
                        }
                        line.AlphaDecrease(BeatTime(1.55f), 0.5f);
                        line.VerticalMirror = true;
                        DelayBeat(1.55f, () => { line.Dispose(); });
                    });
                    RegisterFunctionOnce("KickCL", () =>
                    {
                        DelayBeat(1, () =>
                        {
                            ValueEasing.EaseBuilder alp = new();
                            alp.Insert(BeatTime(0.5f) - 2, ValueEasing.EaseInCubic(0.75f * 0.5f, 0.3f * 0.5f, BeatTime(0.5f)));
                            alp.Insert(2, ValueEasing.Linear(0.3f * 0.5f, 0.75f * 0.5f, 2));
                            alp.Insert(BeatTime(2), ValueEasing.EaseInCubic(0.75f * 0.5f, 0, BeatTime(2)));
                            Line la = new(80, 90);
                            Line lb = new(80 + 60, 90);
                            Line[] l = { la, lb };
                            foreach (Line a in l)
                            {
                                CreateEntity(a);
                                alp.Run((s) => { a.Alpha = s; });
                                DelayBeat(2, () => { a.Dispose(); });
                            }
                        });
                        CentreEasing.EaseBuilder c1 = new();
                        c1.Insert(BeatTime(1.55f), CentreEasing.Linear(new Vector2(640, 240), new Vector2(0 - 160, 240), BeatTime(1.55f)));
                        CentreEasing.EaseBuilder c2 = new();
                        c2.Insert(BeatTime(1.55f), CentreEasing.Linear(new Vector2(640 + 160, 0), new Vector2(0, 0), BeatTime(1.55f)));
                        Line line = new(c1.GetResult(), c2.GetResult()) { Alpha = 0.85f };
                        CreateEntity(line);
                        for (int i = 0; i < 3; i++)
                        {
                            int t = i;
                            line.InsertRetention(new(i * 4, 0.85f - 0.85f / 3 * t));
                        }
                        line.AlphaDecrease(BeatTime(1.55f), 0.5f);
                        line.VerticalMirror = true;
                        DelayBeat(1.55f, () => { line.Dispose(); });
                    });

                    string[] rhythm = { 
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "D(D1)(RotateL)(KickBR)", "", "", "(D)(D1)", "", "", "(D)(+21)", "(+21)",
                    "(+0)(+21)(Flicker)(KickAR)", "", "(D)(D1)(KickAR)", "", "", "", "(D)", "",
                    "(D)(D1)(RotateR)(KickBL)", "", "", "D(D1)", "", "", "(D)(D1)", "",
                    "D(D1)(Flicker)(KickAL)", "", "(D)(D1)(KickAL)", "", "", "", "D", "",
                    "(D)(D1)(RotateL)(KickBR)", "", "", "D(D1)", "", "", "(D1)(+2)", "(+2)",
                    "(+01)(+2)(Flicker)(KickAR)", "", "(D)(D1)(KickAR)", "", "", "", "D(D1)", "",
                    "(D)(D1)(RotateMid)(KickBL)", "", "", "D(D1)", "", "", "(D)(D1)", "",
                    "D(In)", "+2", "+2", "+2", "+2", "+2", "+2", "",

                    "(*$11)(*$31)ConvulseL(Split)", "", "(*$11)(*$31)ConvulseL(Split)", "", "", "", "", "",
                    "(*$10)(*$30)ConvulseR(Split)(Expand)", "", "(*$10)(*$30)ConvulseR(Split)(Expand)", "", "", "", "($0)($21)ConvulseR(Split)", "",
                    "(*$01)(*$20)(*<$30)(_>$31)ConvulseL(Split)(Down)", "", "", "(*$01)(*$20)(*<$10)(_>$11)ConvulseL(Split)(Up)", "", "", "(*$01)(*$20)(*<$30)(_>$31)ConvulseL(Split)(Down)", "",
                    "(*$21)(*$00)(*>$30)(_<$31)ConvulseR(Split)(Down)", "", "(*$21)(*$00)(*>$10)(_<$11)ConvulseR(Split)(Up)", "", "", "", "(*$21)(*$00)(*>$30)(_<$31)ConvulseR(Split)(Down)", "",
                    "(*$00)(*$20)R1(ConvulseL)(Expand2)(Split)", "", "", "(*$00)(*$20)R1(ConvulseL)(Expand2)(Split)", "", "", "($00)($20)R1(ConvulseL)(Expand2)(Split)", "",
                    "(*$01)(*$21)R(ConvulseR)(Expand2)(Split)", "", "", "(*$01)(*$21)R(ConvulseR)(Expand2)(Split)", "", "", "($00)($20)R1(ConvulseR)(Expand2)(Split)", "",
                    "(*$00)(*$20)R1(ConvulseL)(Expand2)(Split)", "", "", "(*$00)(*$20)R1(ConvulseL)(Expand2)(Split)", "", "", "($00)($20)R1(ConvulseL)(Expand2)(Split)", "",
                    "(*<$00)(*<$20)(*>$01)(*>$21)(ConvulseR)(Reverse)", "", "", "", "", "", "", "",

                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                }
                if (InBeat(2176 - 8))
                {
                    ScreenDrawing.BackGroundColor = Color.DimGray * 0.35f;
                    RegisterFunctionOnce("Rotate", () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(360, BeatTime(18));
                    });
                    RegisterFunctionOnce("RotateN", () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(-360, BeatTime(16));
                    });
                    RegisterFunctionOnce("Spread", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation + 21 + t * 15, BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 + 21 + t * 15, BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }
                    });
                    RegisterFunctionOnce("SpreadN", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation - (21 + t * 15), BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 - (21 + t * 15), BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }
                    });
                    RegisterFunctionOnce("ShakeA", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(6.0f, BeatTime(1), false);
                        ScreenDrawing.CameraEffect.SizeExpand(12.0f, BeatTime(1));
                    });
                    RegisterFunctionOnce("ShakeB", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.0f, BeatTime(1), true);
                        ScreenDrawing.CameraEffect.SizeExpand(2.0f, BeatTime(1));
                    });
                    RegisterFunctionOnce("Shake", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCirc(2.4f, 0, BeatTime(2.6f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s);
                        builder.Run(s => ScreenDrawing.ScreenScale = s * 0.012f + 1);
                    });
                    RegisterFunctionOnce("ShakeN", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCirc(-2.4f, 0, BeatTime(2.6f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s);
                        builder.Run(s => ScreenDrawing.ScreenScale = -s * 0.012f + 1);
                    });

                    RegisterFunctionOnce("Flicker", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.18f);
                        ScreenDrawing.CameraEffect.Convulse(0.1f, BeatTime(1.4f), false);
                    });
                    string[] rhythm = { 
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur 
                    "D(D1)", "", "D", "(D)(D1)", "(Spread)(Shake)", "", "D(D1)", "",
                    "(D)(D1)", "", "D(D1)", "", "(SpreadN)(ShakeN)D", "", "D", "",
                    "(D)(D1)", "", "D", "D(D1)", "(Spread)(Shake)", "", "(D)(D1)", "",
                    "D(D1)", "", "(D)(D1)", "", "(SpreadN)(ShakeN)D", "", "(#15#$0)(#15#$21)(Rotate)", "",
                    "", "", "<$001", "<$211", ">$001(Spread)(ShakeA)", ">$211", "<$001", "<$211",
                    ">$001", ">$211", "<$001", "<$211", ">$001(SpreadN)(ShakeB)", ">$211", "<$001", "<$211",
                    "D(D1)", "", "D", "", "D(D1)(Spread)(Shake)", "", "D", "",
                    "D(D1)", "", "D", "D1", "D(SpreadN)(ShakeN)", "", "D", "",
                    "D(D1)", "", "D", "(D)(D1)", "(Spread)(Shake)", "", "D(D1)", "",
                    "(D)(D1)", "", "D(D1)", "", "(SpreadN)(ShakeN)D", "", "(D)", "",
                    "(D)(D1)", "", "D", "D(D1)", "(Spread)(Shake)", "", "(D)(D1)", "",
                    "D(D1)", "", "(D)(D1)", "", "(SpreadN)(ShakeN)", "", "($30)($11)", "",
                    "(#13#$2)(#13#$01)(RotateN)", "", "<$011", "<$201", ">$011(Spread)(ShakeA)", ">$201", ">$011", ">$201",
                    ">$011", ">$201", ">$011", ">$201", ">$011(SpreadN)(ShakeB)", ">$201", ">$011", ">$201",
                    "D1(D)(Spread)(Shake)(Flicker)", "", "D1", "", "D1(D)(Spread)(Shake)(Flicker)", "", "D1", "",
                    "D1(D)(SpreadN)(ShakeN)(Flicker)", "", "D1", "D", "D1(SpreadN)(ShakeN)(Flicker)", "", "D1", "",

                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                }
                if (InBeat(2304 - 8))
                {
                    ScreenDrawing.BackGroundColor = Color.DimGray * 0.35f;
                    RegisterFunctionOnce("Spread", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation + 21 + t * 15, BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 + 21 + t * 15, BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }
                    });
                    RegisterFunctionOnce("SpreadN", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation - (21 + t * 15), BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 - (21 + t * 15), BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }
                    });

                    RegisterFunction("ConvulseX", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(2.5f, 0, BeatTime(1.5f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s);

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(4), ValueEasing.EaseOutQuad(5.0f, 0.6f, BeatTime(4)));
                        builder2.Run(s => splitter.Intensity = s);

                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(-12, 0, BeatTime(1.4f)));
                        builder3.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });
                    RegisterFunction("ConvulseY", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(-2.5f, 0, BeatTime(1.5f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s);

                        ValueEasing.EaseBuilder builder2 = new();
                        builder2.Insert(BeatTime(4), ValueEasing.EaseOutQuad(5.0f, 0.6f, BeatTime(4)));
                        builder2.Run(s => splitter.Intensity = s);

                        ValueEasing.EaseBuilder builder3 = new();
                        builder3.Insert(BeatTime(1.4f), ValueEasing.EaseOutCubic(12, 0, BeatTime(1.4f)));
                        builder3.Run(s => ScreenDrawing.ScreenPositionDetla = new(0, s));
                    });

                    float AlphaCalc(float delta)
                    {
                        delta = MathF.Abs(delta);
                        if (delta <= 50) return 1;
                        delta -= 50;
                        delta = 1 - delta / 150f;
                        return delta * delta;
                    }
                    RegisterFunctionOnce("Shake", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCirc(2.4f, 0, BeatTime(2.6f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s * 1.2f);
                        builder.Run(s => ScreenDrawing.ScreenScale = s * 0.012f + 1);
                    });
                    RegisterFunctionOnce("ShakeN", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(BeatTime(2), ValueEasing.EaseOutCirc(-2.4f, 0, BeatTime(2.6f)));
                        builder.Run(s => ScreenDrawing.ScreenAngle = s * 1.2f);
                        builder.Run(s => ScreenDrawing.ScreenScale = -s * 0.012f + 1);
                    });
                    float yDelta1 = 0;
                    float yDelta2 = 0;
                    RegisterFunctionOnce("Run", () =>
                    {
                        ValueEasing.EaseBuilder builderX = new();
                        builderX.Adjust = false;
                        builderX.Insert(BeatTime(1), ValueEasing.Stable(990));
                        for (int i = 0; i < 4; i++)
                        {
                            builderX.Insert(BeatTime(9), ValueEasing.EaseOutElastic(220, 0, BeatTime(9)));
                            builderX.Insert(BeatTime(7), ValueEasing.Stable(990));
                        }
                        builderX.Run(s => yDelta1 = s);
                        ValueEasing.EaseBuilder builderY = new();
                        builderY.Adjust = false;
                        builderY.Insert(BeatTime(1), ValueEasing.Stable(990));
                        builderY.Insert(BeatTime(6f), ValueEasing.Stable(990));
                        for (int i = 0; i < 4; i++)
                        {
                            builderY.Insert(BeatTime(9), ValueEasing.EaseOutElastic(-220, 0, BeatTime(9)));
                            builderY.Insert(BeatTime(7), ValueEasing.Stable(990));
                        }
                        builderY.Run(s => yDelta2 = s);
                    }
                    );
                    string[] rhythm2 = {
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur
                    "", "", "", "", "(SpreadN)(Shake)", "", "", "",
                    "", "", "", "", "Spread(ShakeN)", "", "", "",
                    "", "", "", "", "(SpreadN)(Shake)", "", "", "",
                    "", "", "", "", "Spread(ShakeN)", "", "", "",
                    "", "", "", "", "(SpreadN)(Shake)", "", "", "",
                    "", "", "", "", "Spread(ShakeN)", "", "", "",
                    "", "", "", "", "(SpreadN)(Shake)", "", "", "",
                    "", "", "", "", "Spread(ShakeN)", "", "", "",

                    "ConvulseX", "", "", "", "", "", "ConvulseX", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseY", "", "", "", "", "", "ConvulseY", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseX", "", "", "", "", "", "ConvulseX", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseY", "", "", "", "", "", "ConvulseY", "",
                    "", "", "", "", "", "", "", "",
                };
                    string[] rhythm = { 
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur 
                    "D(D1)", "", "D", "(D)(D1)", "", "", "D(D1)", "",
                    "(D)(D1)", "", "D(D1)", "", "D", "", "(D)", "",
                    "D(D1)", "", "D", "(D)(D1)", "", "", "D(D1)", "",
                    "(D)(D1)", "", "D(D1)", "", "D", "", "(D)", "",
                    "D(D1)", "", "D", "(D)(D1)", "", "", "D(D1)", "",
                    "(D)(D1)", "", "D(D1)", "", "D", "", "(D)", "",
                    "D(D1)", "", "D", "(D)(D1)", "", "", "D(D1)", "",
                    "<R'1.15(Run)", ">+11'1.15", "<+00'1.15", ">+11'1.15", "<+00'1.15", ">+11'1.15", "<+00'1.15", ">+11'1.15",

                    "($00{X})($20{X})(R1)", "", "", "D1", "", "", "($00{Y})($20{Y})(R1)", "",
                    "", "D1", "", "", "(D1)D", "", "D1(D)", "",
                    "($01{X})($21{X})(R)", "", "", "D", "", "", "($01{Y})($21{Y})(R)", "",
                    "", "D", "", "", "(D)D1", "", "D(D1)", "",
                    "($00{X})($20{X})(R1)", "", "", "D1", "", "", "($00{Y})($20{Y})(R1)", "",
                    "", "D1", "", "", "(D1)D", "", "D1(D)", "",
                    "($01{X})($21{X})(R)", "", "", "D", "", "", "($01{Y})($21{Y})(R)", "",
                    "", "D", "", "", "(D)D1", "", "D(D1)", "",

                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm2);

                    Arrow[] allX = new Arrow[0];
                    Arrow[] allY = new Arrow[0];
                    DelayBeat(1, () => { allX = GetAll<Arrow>("X"); allY = GetAll<Arrow>("Y"); });
                    ForBeat120(64, 80, () =>
                    {
                        foreach (Arrow arr in allX)
                        {
                            arr.Offset = new(0, yDelta1);
                            arr.Alpha = AlphaCalc(yDelta1);
                        }
                        foreach (Arrow arr in allY)
                        {
                            arr.Offset = new(0, yDelta2);
                            arr.Alpha = AlphaCalc(yDelta2);
                        }
                    });
                }
                if (InBeat(2432 - 8))
                {
                    float AlphaCalc(float delta)
                    {
                        delta = MathF.Abs(delta);
                        if (delta <= 50) return 1;
                        delta -= 50;
                        delta = 1 - delta / 150f;
                        return delta * delta;
                    }
                    float yDelta1 = 0, yDelta2 = 0;
                    RegisterFunctionOnce("Run", () =>
                    {
                        ValueEasing.EaseBuilder builderX = new();
                        builderX.Adjust = false;
                        builderX.Insert(BeatTime(1), ValueEasing.Stable(990));
                        for (int i = 0; i < 4; i++)
                        {
                            builderX.Insert(BeatTime(9), ValueEasing.EaseOutElastic(220, 0, BeatTime(9)));
                            builderX.Insert(BeatTime(7), ValueEasing.Stable(990));
                        }
                        builderX.Run(s => yDelta1 = s);
                        ValueEasing.EaseBuilder builderY = new();
                        builderY.Adjust = false;
                        builderY.Insert(BeatTime(1), ValueEasing.Stable(990));
                        builderY.Insert(BeatTime(6f), ValueEasing.Stable(990));
                        for (int i = 0; i < 4; i++)
                        {
                            builderY.Insert(BeatTime(9), ValueEasing.EaseOutElastic(-220, 0, BeatTime(9)));
                            builderY.Insert(BeatTime(7), ValueEasing.Stable(990));
                        }
                        builderY.Run(s => yDelta2 = s);
                    });
                    RegisterFunctionOnce("Spread", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation + 21 + t * 15, BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 + 21 + t * 15, BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }

                        ValueEasing.EaseBuilder builderz = new();
                        builderz.Insert(BeatTime(2), ValueEasing.EaseOutCirc(2.4f, 0, BeatTime(2)));
                        builderz.Run(s => ScreenDrawing.ScreenAngle = s * 1.6f);
                        builderz.Run(s => ScreenDrawing.ScreenScale = s * 0.012f + 1.1f);
                    });
                    RegisterFunctionOnce("SpreadN", () =>
                    {
                        for (int x = 0; x < 3; x += 2)
                        {
                            int t = x;
                            for (int i = 0; i < 4; i++)
                            {
                                float rotation = i * 90;
                                float rotation2 = i * 90f + 90;
                                var rot1 = ValueEasing.EaseInCirc(rotation, rotation - (21 + t * 15), BeatTime(3.2f));
                                var rot2 = ValueEasing.EaseInCirc(rotation2, rotation2 - (21 + t * 15), BeatTime(3.2f));
                                var ease1 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot1);
                                var ease2 = CentreEasing.PolarCombine(CentreEasing.Linear(new Vector2(42, 42), new Vector2(88 + t * 44, 88 + t * 44), BeatTime(3.2f)), rot2);
                                Line line = new Line(CentreEasing.Combine(new Vector2(320, 240), ease1), CentreEasing.Combine(new Vector2(320, 240), ease2));
                                line.DrawingColor = Color.White;
                                CreateEntity(line);
                                float p = line.Alpha = 0.4f - 0.1f * t;
                                ValueEasing.EaseBuilder builder = new();
                                builder.Insert(BeatTime(3.2f), ValueEasing.EaseOutQuad(p, 0, BeatTime(3.2f)));
                                DelayBeat(3.2f, () => line.Dispose());
                                builder.Run(s => line.Alpha = s * 1.4f);
                            }
                        }

                        ValueEasing.EaseBuilder builderz = new();
                        builderz.Insert(BeatTime(2), ValueEasing.EaseOutCirc(-2.4f, 0, BeatTime(2)));
                        builderz.Run(s => ScreenDrawing.ScreenAngle = s * 1.6f);
                        builderz.Run(s => ScreenDrawing.ScreenScale = -s * 0.012f + 1.05f);
                    });
                    RegisterFunctionOnce("Zoom", () =>
                    {
                        ValueEasing.EaseBuilder zoom = new();
                        zoom.Insert(BeatTime(64), ValueEasing.EaseOutQuart(1.2f, 1.00f, BeatTime(64)));
                        zoom.Run(s => ScreenDrawing.ScreenScale = s);
                    });
                    string[] rhythm = { 
                    //pre  
                    "Run", "", "", "", "", "", "", "", 
                    //cur  
                    "($00{X})($20{X})(R1)", "", "", "D1", "", "", "($00{Y})($20{Y})(R1)", "",
                    "", "D1", "", "", "(D1)D", "", "D1(D)", "",
                    "($01{X})($21{X})(R)", "", "", "D", "", "", "($01{Y})($21{Y})(R)", "",
                    "", "D", "", "", "(D)D1", "", "D(D1)", "",
                    "($00{X})($20{X})(R1)", "", "", "D1", "", "", "($00{Y})($20{Y})(R1)", "",
                    "", "D1", "", "", "(D1)D", "", "D1(D)", "",
                    "($01{X})($21{X})(R)", "", "", "D", "", "", "($01{Y})($21{Y})(R)", "",
                    "", "D", "", "", "(^D'1.5)^D1'1.5", "", "^$0'1.5(^$21'1.5)", "",

                    "#62#$1(#62#$31)", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "", "",
                    "$00($21)",
                };
                    string[] rhythm2 = {
                    //pre  
                    "", "", "", "", "", "", "", "", 
                    //cur 
                    "ConvulseX", "", "", "", "", "", "ConvulseX", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseY", "", "", "", "", "", "ConvulseY", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseX", "", "", "", "", "", "ConvulseX", "",
                    "", "", "", "", "", "", "", "",
                    "ConvulseY", "", "", "", "", "", "ConvulseY", "",
                    "", "", "", "", "SpreadN", "", "Spread", "",
                    "Zoom", "", "", "", "", "", "", "",
                };
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm);
                    BarrageCreate(0, BeatTime(1) * 8, 7.0f, rhythm2);
                    Arrow[] allX = new Arrow[0];
                    Arrow[] allY = new Arrow[0];
                    DelayBeat(1, () => { allX = GetAll<Arrow>("X"); allY = GetAll<Arrow>("Y"); });
                    ForBeat120(80, () =>
                    {
                        foreach (Arrow arr in allX)
                        {
                            arr.Offset = new(0, yDelta1);
                            arr.Alpha = AlphaCalc(yDelta1);
                        }
                        foreach (Arrow arr in allY)
                        {
                            arr.Offset = new(0, yDelta2);
                            arr.Alpha = AlphaCalc(yDelta2);
                        }
                    });
                }
            }

            GlobalResources.Effects.StepSampleShader StepSample;
            GlobalResources.Effects.ScaleShader ShadersScale;
            GlobalResources.Effects.CameraShader Effect3D;
            ScreenDrawing.Shaders.Blur Blur;
            Shader SinWave;
            GlobalResources.Effects.GrayShader Gray;

            RenderProduction cameraProduction, production1, production2, production3, grayProduction;
            Lighting lightProduction;

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

                production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.51f);
                production2 = new ScreenDrawing.Shaders.Filter(Shaders.Scale, 0.501f);
                production3 = Blur = new ScreenDrawing.Shaders.Blur(0.505f);
                grayProduction = new ScreenDrawing.Shaders.Filter(Shaders.Gray, 0.998f);

                Blur.Sigma = 0.0f;

                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(production2);
                ScreenDrawing.SceneRendering.InsertProduction(production3);

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
                ScreenDrawing.SceneRendering.InsertProduction(splitter);

                CreateEntity(rainer = new Rainer());
                rainer.Intensity = 0.18f;
                rainer.Speed = 3f;

                game = this;

                HeartAttribute.BuffedLevel = 3;
                HeartAttribute.MaxHP = 10;
                HeartAttribute.Speed = 3.26f;
                HeartAttribute.SoftFalling = true;

                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.HPExistColor = Color.Silver;

                InstantTP(-200, -200);
                InstantSetBox(new Vector2(180, 560), 84, 84);
                SetSoul(1);
                HeartAttribute.ArrowFixed = true;

                //  ScreenDrawing.UISettings.CreateUISurface();

                //      CreateLine(150, 100);
                //   GametimeDetla = 4300;
                GametimeDelta = -1.1f; //+ game.BeatTime(311);
                bool delayEnable = false;
                if (delayEnable)
                {
                    float delay = BeatTime(1000 - 10);
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
        }
    }
}