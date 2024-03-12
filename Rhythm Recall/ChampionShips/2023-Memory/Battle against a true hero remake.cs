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
    public class ClassicFightRemake : IChampionShip
    {
        public ClassicFightRemake()
        {
            Game.game = new Game();
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
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 2f),
                            new(Difficulty.Normal, 10f),
                            new(Difficulty.Extreme, 17.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 2f),
                            new(Difficulty.Normal, 10f),
                            new(Difficulty.Extreme, 17.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 8.5f),
                            new(Difficulty.Normal, 13.5f),
                            new(Difficulty.Extreme, 20.7f),
                        }
                    );
                public override string BarrageAuthor => "T-mas";
                public override string AttributeAuthor => "T-mas & Walar & IceAgeDOT";
                public override string PaintAuthor => "OtokP";
                public override string SongAuthor => "TobyFox";
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(6.2485f) { }

            public static Game game;

            public string Music => "undyne theme";
            //  public string Music => "Brain Power";
            public string FightName => "Undertale Undyne (RE)";

            private static class NormalBarrage { }

            public void Noob()
            {
                if (Gametime < -2) return;

                // intro 
                // var v = GetAll<Entity>();
                if (notRegistered)
                {
                    notRegistered = false;
                    RegisterFunction("ToRed", () =>
                    {
                        Player.Heart origin = Heart;
                        Player.Heart heart = CreateHeart(522, 177, 177);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(300, 177, 177);
                        SetSoul(0);

                        SetPlayerBoxMission(origin);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(-300, 84, 84);

                        ForBeat(4, () => origin.InstantTP(origin.controlingBox.Centre));
                        DelayBeat(4, () => origin.Dispose());
                        DelayBeat(4.2f, () => SetPlayerBoxMission(heart));
                    });
                    RegisterFunction("Spear", () =>
                    {
                        CreateSpear(new NormalSpear(Heart.Centre + GetVector2(130, Rand(0, 359))) { WaitingTime = BeatTime(6) });
                    });
                }

                if (InBeat(2))
                {
                    RegisterFunctionOnce("UShine", () =>
                    {
                        Line line = new(CentreEasing.Stable(new(320, 0)), (s) => new(320, BoxStates.Up));
                        line.DrawingColor = Color.Gold;
                        line.Alpha = 0.5f;
                        line.AlphaDecrease(BeatTime(4));
                        CreateEntity(line);

                        CentreEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(320, 270), BeatTime(4)));
                        easeBuilder.Run((x) => { BoxStates.Centre = x; Heart.InstantTP(x); });
                    });
                    RegisterFunctionOnce("UShine2", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            int t = i;
                            Line line = new(CentreEasing.Stable(new(320 + t * 10, 0)), (s) => new(320 + t * 5, BoxStates.Up));
                            line.DrawingColor = Color.Gold;
                            line.Alpha = 0.5f;
                            line.AlphaDecrease(BeatTime(4));
                            CreateEntity(line);
                        }

                        CentreEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(4), CentreEasing.EaseOutQuad(new(320, BoxStates.Centre.Y), new(320, BoxStates.Centre.Y - 30 / 3f), BeatTime(4)));
                        easeBuilder.Run((x) => { BoxStates.Centre = x; Heart.InstantTP(x); });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-5) * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + 5.2f * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-0.3f) * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateLSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateRSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateMidSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(5));
                    });

                    RegisterFunctionOnce("SideLine", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float xPos = sign * 320 + 320;
                            for (int i = 1; i <= 3; i++)
                            {
                                Line line = new(new Vector2(xPos - i * sign * 12, 0), new Vector2(xPos - i * sign * 12, 480));
                                line.DrawingColor = Color.Yellow;
                                line.Alpha = i / 12f + 0.16f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(3 + i));
                            }
                        }
                    });
                    RegisterFunctionOnce("SpearLine", () =>
                    {
                        Vector2[] origins = {
                        new(120, 100),
                        new(132, 100),
                        new(132, 340),
                        new(148, 334),
                        new(126, 398),
                        new(104, 334),
                        new(120, 340)
                        };
                        Vector2 centre = new(126, 240);
                        Vector2[] deltas = new Vector2[origins.Length];
                        for (int i = 0; i < origins.Length; i++)
                        {
                            deltas[i] = origins[i] - centre;
                        }

                        float rotation = 0, time = 0;
                        Vector2[] positions = new Vector2[origins.Length];
                        ForBeat120(4, () =>
                        {
                            time += 0.5f;
                            rotation -= time * time * 0.003f;
                            for (int i = 0; i < origins.Length; i++)
                            {
                                positions[i] = centre + Rotate(deltas[i], rotation);
                            }
                        });
                        for (int i = 0; i < origins.Length; i++)
                        {
                            int t = i;
                            Line line = new((s) => positions[t], (s) => positions[(t + 1) % positions.Length]);
                            line.DrawingColor = Color.Silver;
                            line.Alpha = 0.3f;
                            CreateEntity(line);
                            DelayBeat(1, () =>
                            {
                                line.AlphaDecrease(BeatTime(3));
                            });
                        }
                    });
                    RegisterFunctionOnce("MirroredLine", () =>
                    {
                        Vector2[] origins = {
                        new(120, 100),
                        new(132, 100),
                        new(132, 340),
                        new(148, 334),
                        new(126, 398),
                        new(104, 334),
                        new(120, 340)
                        };
                        Vector2 centre = new(126, 240);
                        Vector2[] deltas = new Vector2[origins.Length];
                        for (int i = 0; i < origins.Length; i++)
                        {
                            deltas[i] = origins[i] - centre;
                        }

                        float rotation = 0, time = 0;
                        Vector2[] positions = new Vector2[origins.Length];
                        ForBeat120(4, () =>
                        {
                            time += 0.5f;
                            rotation -= time * time * 0.003f;
                            for (int i = 0; i < origins.Length; i++)
                            {
                                positions[i] = centre + Rotate(deltas[i], rotation);
                                positions[i].X = 640 - positions[i].X;
                            }
                        });
                        for (int i = 0; i < origins.Length; i++)
                        {
                            int t = i;
                            Line line = new((s) => positions[t], (s) => positions[(t + 1) % positions.Length]);
                            line.DrawingColor = Color.Silver;
                            line.Alpha = 0.3f;
                            CreateEntity(line);
                            DelayBeat(1, () =>
                            {
                                line.AlphaDecrease(BeatTime(3));
                            });
                        }
                    });
                    RegisterFunctionOnce("SideLine2", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float xPos = sign * 320 + 320;
                            for (int i = 1; i <= 2; i++)
                            {
                                Line line = new(new Vector2(xPos - i * sign * 12, 0), new Vector2(xPos - i * sign * 12, 480));
                                line.DrawingColor = Color.Yellow;
                                line.Alpha = i / 10f + 0.24f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(2 + i * 0.5f));
                            }
                        }
                    });
                    RegisterFunctionOnce("SideLine3", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float yPos = sign * 240 + 240;
                            for (int i = 1; i <= 2; i++)
                            {
                                Line line = new(new Vector2(0, yPos - (i + 1) * sign * 10), new Vector2(640, yPos - (i + 1) * sign * 12));
                                line.DrawingColor = Color.Silver * 0.5f;
                                line.Alpha = i / 10f + 0.24f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(2 + i * 0.5f));
                            }
                        }
                    });
                    RegisterFunctionOnce("FollowLine", () =>
                    {
                        Line up = new((s) => new(320, BoxStates.Up - 12), (s) => ScreenDrawing.ScreenAngle * 0.9f);
                        Line down = new((s) => new(320, BoxStates.Down + 12), (s) => ScreenDrawing.ScreenAngle * 0.9f);
                        Line[] all = { up, down };
                        foreach (Line line in all)
                        {
                            CreateEntity(line);

                            for (int i = 0; i <= 1; i++)
                                DelayBeat(i * 8, () =>
                                {
                                    line.Alpha = 0.2f;
                                    line.AlphaDecrease(BeatTime(3), 0.12f);
                                });
                            DelayBeat(16, () => { line.Alpha = 0.2f; line.AlphaDecrease(BeatTime(4)); });

                            line.DrawingColor = Color.Aqua;
                            line.Alpha = 0.2f;
                        }
                    });

                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(1.8f), false);
                        ScreenDrawing.CameraEffect.SizeShrink(1.41f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(1.8f), true);
                        ScreenDrawing.CameraEffect.SizeShrink(1.4f, BeatTime(3));
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(2f, BeatTime(3.4f));
                    });
                    RegisterFunctionOnce("Expand2", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3f, BeatTime(4.4f));
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(2f, BeatTime(3.4f));
                    });

                    float height = 6;
                    float alpha = 0.1f;
                    RegisterFunctionOnce("Rise", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Black * 0.27f);
                        height += 32;
                        alpha += 0.1f;
                        ForBeat(2, () =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.Red * alpha, 0.2f);
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.DownBoundDistance * 0.8f + height * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("Fade", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.8f, BeatTime(2.7f));
                        DelayBeat(2.5f, () =>
                        {
                            rainer.Intensity = 0.2f;
                            rainer.Speed = 3.0f;
                        });
                    });

                    RegisterFunctionOnce("FallArrow1", () =>
                    {
                        FakeArrow arrow = new(RandIn(0, 1), 0);
                        CreateEntity(arrow);
                        ValueEasing.EaseBuilder moveY = new();
                        moveY.Insert(game.BeatTime(1), ValueEasing.EaseOutQuad(-10, 75, game.BeatTime(1)));
                        moveY.Insert(game.BeatTime(1), ValueEasing.EaseInQuad(75, -10, game.BeatTime(1)));
                        moveY.Run((s) =>
                        {
                            Vector2 t = arrow.Centre;
                            t.X = 100;
                            t.Y = s;
                            arrow.Centre = t;
                        });
                        ValueEasing.EaseBuilder rotate = new();
                        rotate.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(Rand(0, 360), 100 + Rand(0, 360), game.BeatTime(2)));
                        rotate.Run((s) =>
                        {
                            arrow.Rotation = s;
                        });
                        ValueEasing.EaseBuilder alpha = new();
                        alpha.Insert(game.BeatTime(1), ValueEasing.Linear(0, 0.6f, game.BeatTime(1)));
                        alpha.Insert(game.BeatTime(1), ValueEasing.Linear(0.6f, 0, game.BeatTime(1)));
                        alpha.Run((s) =>
                        {
                            arrow.Alpha = s;
                        });
                    });
                    string[] rhythm = {
                    "$3'0.6(UShine)(FallArrow1)", "", "", "",    "", "", "", "",
                    "/", "", "", "",    "", "", "", "RotateL",
                    "$3'0.6(FollowLine)(UShine2)", "", "", "",    "", "", "", "RotateR",
                    "$3'0.6(UShine2)", "", "", "",    "", "", "", "RotateMid",
                    "$3'0.6(UShine2)", "", "", "",    "", "", "", "",
                    "/", "", "", "",    "", "", "RotateRSoft", "",

                    "R(SideLine)", "", "", "+0",    "", "", "+0", "",
                    "$3", "+0", "", "",    "+0", "", "+0(RotateLSoft)", "",
                    "R(SideLine)", "", "", "+0",    "", "", "+0", "",
                    "$3", "+0", "", "",    "+0", "", "+0(RotateRSoft)", "",
                    "R(SideLine)", "", "", "+0",    "", "", "+0", "",
                    "$3", "+0", "", "",    "+0", "", "+0(RotateMidSoft)", "",
                    "(^R'1.5)(SideLine2)(ShrinkL)", "", "", "(^+0'1.5)(ShrinkR)",    "", "", "(^R'1.5)(ShrinkL)", "",
                    "/", "(^+0'1.5)(ShrinkR)", "", "",    "(^R'1.5)(SideLine2)(ShrinkL)", "", "+0(RotateLSoft)", "",
                    "R(SpearLine)(SideLine)", "", "", "+0",    "", "", "D", "",
                    "/", "+0", "", "",    "D", "", "+0(RotateRSoft)", "",
                    "R(MirroredLine)(SideLine)", "", "", "+0",    "", "", "D", "",
                    "/", "+0", "", "",    "D", "", "+0(RotateLSoft)", "",
                    "R(SideLine)(SideLine3)", "", "", "+0",    "", "", "D", "",
                    "/", "+0", "", "",    "D", "", "+0(RotateMidSoft)", "",
                    "(^R'1.5)(Rise)(Shrink)", "", "", "(^+0'1.5)(Rise)(Shrink)",    "", "", "(^R'1.5)(Rise)(Shrink)", "",
                    "/", "(^+0'1.5)(Rise)(Expand)", "", "",    "(^R'1.5)(Expand2)", "(Fade)", "ToRed", "",
                };
                    float time = game.BeatTime(14);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(176))
                {
                    string[] rhythm = {
                    "", "", "", "",    "Spear", "", "", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                };
                    float time = game.BeatTime(16 + 1.1f);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(312))
                {
                    RegisterFunctionOnce("DownCross", () =>
                    {
                        float time = BeatTime(32);
                        for (int i = 0; i <= 30; i++)
                        {
                            DelayBeat(i * 8, () =>
                            {
                                Line line = new(
                                    CentreEasing.EaseInQuad(new(0, 0 - 30), new(0, 480 + 90), time),
                                    CentreEasing.EaseInQuad(new(640, 0 - 90 - 30), new(640, 480), time)
                                );
                                line.Alpha = 0.34f;
                                line.DrawingColor = Color.LightBlue;
                                for (int k = 2; k <= 4; k++) line.InsertRetention(new(k * 2 - 2, 1.3f / k));
                                DelayBeat(32, () => line.Dispose());
                                CreateEntity(line);
                            });
                            DelayBeat(i * 8 + 4, () =>
                            {
                                Line line = new(
                                    CentreEasing.EaseInQuad(new(0, 0 - 90 - 30), new(0, 480), time),
                                    CentreEasing.EaseInQuad(new(640, 0 - 30), new(640, 480 + 90), time)
                                );
                                line.Alpha = 0.34f;
                                line.DrawingColor = Color.LightBlue;
                                for (int k = 2; k <= 4; k++) line.InsertRetention(new(k * 2 - 2, 1.3f / k));
                                DelayBeat(32, () => line.Dispose());
                                CreateEntity(line);
                            });
                        }

                        GlobalResources.Effects.NeonLineShader shader;
                        RenderProduction production;
                        ScreenDrawing.BackGroundRendering.InsertProduction(production = new ScreenDrawing.Shaders.Filter(shader = Shaders.NeonLine, 0.5f));
                        DelayBeat(960 - (440 - 4), () =>
                        {
                            production.Dispose();
                        });

                        Color mission = Color.Blue * 0.22f, cur = Color.Transparent;
                        shader.Speed = 1.0f;
                        shader.DrawingColor = cur;
                        ForBeat(8, () =>
                        {
                            cur = Color.Lerp(cur, mission, 0.2f);
                            shader.DrawingColor = cur;
                        });
                    });
                    RegisterFunctionOnce("RotateLSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateRSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateMidSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(5));
                    });

                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.4f, BeatTime(2), false);
                        ScreenDrawing.CameraEffect.SizeShrink(2.7f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.4f, BeatTime(2), true);
                        ScreenDrawing.CameraEffect.SizeShrink(2.7f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate+", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 7 * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("LRotate", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 5 * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-5) * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1 * 0.2f;
                        });
                        DelayBeat(3.1f, () => { ScreenDrawing.ScreenAngle = 0; });
                    });

                    string[] rhythm = {
                                        "RotateLSoft", "", "", "",
                    "R", "", "", "",    "", "", "+0", "",
                    "", "", "", "",    "R", "", "", "",
                    "", "", "+0", "",    "", "", "", "",
                    "(R)", "", "", "",    "(+0'1.2)(RotateRSoft)", "", "", "",
                    "R", "", "", "",    "", "", "+0", "",
                    "", "", "", "",    "R", "", "", "",
                    "", "", "+0", "",    "", "", "", "",
                    "(R)", "", "", "",    "(+0'1.2)(RotateLSoft)", "", "", "",
                    "R", "", "", "",    "", "", "+0", "",
                    "", "", "", "",    "R", "", "", "",
                    "", "", "+0", "",    "", "", "", "",
                    "(R)", "", "", "",    "(+0'1.2)(RotateMidSoft)", "", "", "",
                    "R", "", "", "",    "", "", "+0", "",
                    "", "", "", "",    "R", "", "", "",
                    "", "", "+0", "",    "", "", "", "(ShrinkR)",
                    "(R)", "", "", "(ShrinkL)",    "(+0)(RotateRSoft)", "", "", "",
                    "R1", "", "", "",    "", "", "+01", "",
                    "", "", "", "",    "R1", "", "", "",
                    "", "", "+01", "",    "", "", "", "",
                    "(R1)", "", "", "",    "(+01'1.2)(RotateLSoft)", "", "", "",
                    "R1", "", "", "",    "", "", "+01", "",
                    "", "", "", "",    "R1", "", "", "",
                    "", "", "+01", "",    "", "", "", "",
                    "(R1)", "", "", "",    "(+01'1.2)(RotateMidSoft)", "", "", "",
                    "R1", "", "", "",    "", "", "+01", "",
                    "", "", "", "",    "R1", "", "", "",
                    "", "", "+01", "",    "", "", "", "",
                    "R1", "", "", "",    "+01", "", "", "",
                    "(R1)(LRotate)", "", "", "",    "", "", "(+01)(RRotate)", "",
                    "", "", "", "",    "(R1)(LRotate)", "", "", "",
                    "", "", "(R1)(LRotate+)", "",    "DownCross", "", "", "",
                    "(R1)(MidRotate)", "", "", "",    "", "", "", "",
                };
                    float time = game.BeatTime(8 - 2);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                #region not made
                if (InBeat(440 - 8))
                {
                    RegisterFunctionOnce("MoveLine", () =>
                    {
                        Line left = new(CentreEasing.Combine(CentreEasing.SinWave(20, BeatTime(16), 0, 0), new Vector2(50, 0)), (s) => 90);
                        Line right = new(CentreEasing.Combine(CentreEasing.SinWave(20, BeatTime(16), 0, -180), new Vector2(640 - 50, 0)), (s) => 90);
                        Line[] lines = { left, right };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0.2f;
                            line.Depth = 0.2f;
                            line.DrawingColor = Color.Gold;
                            CreateEntity(line);
                            DelayBeat(128, () => line.AlphaDecrease(BeatTime(2)));
                        }
                    });
                    RegisterFunctionOnce("Black", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.75f, BeatTime(2));
                    });

                    string[] rhythm = {
                    "Black", "/", "/", "/",    "/", "/", "^$1", "/",
                    "/", "/", "/", "/",    "+0", "/", "/", "/",
                    "#17#+0", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "+0", "/", "/", "/",
                    "#17#+0", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "+0", "/", "/", "/",
                    "R", "/", "/", "/",    "+0", "/", "/", "/",
                    "#15#+0", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "#6#R", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "/", "/",
                    "#17#+0", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",

                    "MoveLine", "Black", "/", "/",    "/", "/", "^$1", "/",
                    "/", "/", "/", "/",    "+0", "/", "/", "/",
                    "#17#+0", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "+0", "/", "/", "/",
                    "#17#+0", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "+0", "/", "/", "/",
                    "R", "/", "/", "/",    "+0", "/", "/", "/",
                    "#17#+0", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "/", "/",
                    "#20#R", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",

                };
                    float time = game.BeatTime(16);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(696 - 0.4f))
                {
                    //create lines
                    for (int i = 4; i < 60; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            Line up = new(CentreEasing.EaseOutQuad(new(320, 0), new(320, BoxStates.Up), BeatTime(8)), (s) => 0);
                            Line down = new(CentreEasing.EaseOutQuad(new(320, 480), new(320, BoxStates.Down), BeatTime(8)), (s) => 0);
                            Line[] lines = { up, down };
                            foreach (Line line in lines)
                            {
                                line.Alpha = 0.4f;
                                line.DrawingColor = Color.LightBlue;
                                line.InsertRetention(new(2, 0.56f));
                                line.InsertRetention(new(4, 0.3f));
                                DelayBeat(4, () =>
                                {
                                    line.AlphaDecrease(BeatTime(4));
                                });
                                CreateEntity(line);
                            }
                        });
                    }
                    RegisterFunctionOnce("MoveBox", () =>
                    {
                        ValueEasing.EaseBuilder height = new();
                        height.Insert(BeatTime(114), ValueEasing.Linear(160, 88, BeatTime(114)));
                        height.Run(s => SetBox(240, 150, s));
                        SetSoul(0);
                    });
                    RegisterFunction("LPike", () =>
                    {
                        SetBoxMission(1);
                        float x = Rand(-3, 3) * -20 + 320;
                        float y = BoxStates.Down + 33;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), -90, game.BeatTime(8)) { IsHidden = true });
                    });
                    RegisterFunction("RPike", () =>
                    {
                        SetBoxMission(0);
                        float x = Rand(-3, 3) * 20 + 320;
                        float y = BoxStates.Up - 33;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), 90, game.BeatTime(8)) { IsHidden = true });
                    });

                    string[] rhythm = {
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "MoveBox",
                    "LPike", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "", "/", "/", "/",   "", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                };
                    float time = game.BeatTime(0);
                    SetPlayerMission(1);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(832 - 0.26f))
                {
                    float curAlpha = 0.05f;
                    float alphaMax = 0;
                    RegisterFunctionOnce("HPike", () =>
                    {
                        curAlpha = 0.2f;
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + Rand(1, 5) * 16 - 3;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, game.BeatTime(8)) { IsHidden = true });
                        Line line = new(new Vector2(BoxStates.Left, y), new Vector2(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(BeatTime(2));
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("CreateLine", () =>
                    {
                        float rotation = MathUtil.Direction(new(320, 240));
                        Line bottomLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(160, 360)), (s) => rotation);
                        Line bottomRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(480, 360)), (s) => -rotation);
                        Line topLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(160, 120)), (s) => -rotation);
                        Line topRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(480, 120)), (s) => rotation);
                        Line[] lines = { bottomLeft, bottomRight, topLeft, topRight };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.Alpha = curAlpha;
                            line.DrawingColor = Color.Gold;
                        }
                        ForBeat(125, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = curAlpha;
                            }
                            curAlpha = curAlpha * 0.8f + 0.05f * 0.2f;
                            alphaMax += 0.002f;
                            curAlpha = MathF.Min(alphaMax, curAlpha);
                        });
                        DelayBeat(126, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(2));
                            }
                        });
                    });
                    RegisterFunctionOnce("LRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            float limit = 0.06f;
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 5 * 0.2f;
                                ScreenDrawing.ScreenScale += limit * 0.2f;
                                limit *= 0.8f;
                            });
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            float limit = 0.06f;
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-5) * 0.2f;
                                ScreenDrawing.ScreenScale += limit * 0.2f;
                                limit *= 0.8f;
                            });
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            DelayBeat(1, () =>
                            {
                                ScreenDrawing.SceneOut(Color.Silver, BeatTime(3.5f));
                            });
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle *= 0.8f;
                            });
                            DelayBeat(3.4f, () =>
                            {
                                ScreenDrawing.ScreenAngle = 0;
                                ScreenDrawing.ScreenScale = 1;
                                rainer.Intensity = 0;
                                rainer.Rotation = 12;
                                rainer.Speed = 7;
                                ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = 0;

                                InstantSetBox(600, 84, 84);
                                SetSoul(1);
                                InstantTP(BoxStates.Centre);

                                ScreenDrawing.UIColor = Color.Transparent;
                                ScreenDrawing.ThemeColor = Color.Transparent;
                                ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                                ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                                Heart.Shields.AddShield(Heart.Shields.BShield);
                            });
                        });
                    });
                    RegisterFunctionOnce("HPikeM", () =>
                    {
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + 3 * 16 - 3;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                        Line line = new(new Vector2(BoxStates.Left, y), new Vector2(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(BeatTime(2));
                        CreateEntity(line);
                    });
                    float time = game.BeatTime(0);
                    string[] rhythm = {
                    "HPike(CreateLine)", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPikeM(LRotate)", "/", "/", "/",   "/", "/", "HPikeM(RRotate)" , "/",
                    "/", "/", "/", "/",   "HPikeM(LRotate)", "/", "/" , "/",
                    "/", "/", "HPikeM(RRotate)", "/",   "/", "/", "/" , "/",
                    "HPikeM(MidRotate)", "/", "/", "/",   "/", "/", "/" , "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                    CentreEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(128), CentreEasing.YSinWave(6, BeatTime(32), 0));
                    builder.Run((s) => { ScreenDrawing.ScreenPositionDelta = s; });
                }
                #endregion
                if (InBeat(960))
                {
                    float time = game.BeatTime(0);
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    RegisterFunctionOnce("Texture1", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash0, BeatTime(3 * 4)) { FadeSpeed = 0.32f });
                    });
                    RegisterFunctionOnce("Texture2", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash1, BeatTime(3 * 4)));
                    });
                    RegisterFunctionOnce("Texture3", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash2, BeatTime(9 * 4)));

                        List<TextAttribute> attributes = new();
                        attributes.Add(new TextSpeedAttribute(55));
                        attributes.Add(new TextSizeAttribute(0.75f));
                        attributes.Add(new TextFadeoutAttribute(BeatTime(36), BeatTime(4)));
                        for (int i = 1; i <= 5; i++)
                            attributes.Add(new TextTimeThreshold(BeatTime(i * 4)));
                        TextPrinter printer;
                        CreateEntity(printer = new TextPrinter("$$$Is $something $still $nostalgic $in $memory?", new Vector2(60, 340), attributes.ToArray()));
                        DelayBeat(41, () => printer.Dispose());
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                    RegisterFunctionOnce("MoveCentre", () =>
                    {
                        BoxStates.BoxMovingScale = 0.04f;
                        SetGreenBox();

                        ScreenDrawing.UIColor = Color.White;
                        ScreenDrawing.ThemeColor = Color.White;
                        ScreenDrawing.HPBar.HPExistColor = Color.White;
                        ScreenDrawing.HPBar.HPLoseColor = Color.White;

                        ForBeat120(32, () => { TP(BoxStates.Centre); ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.87f + 0.995f * 0.13f; });
                    });
                    RegisterFunctionOnce("Easing", () =>
                    {
                        ValueEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(73), ValueEasing.LerpTo(0, 0.05f, ValueEasing.SinWave(5, BeatTime(48), 0)));
                        easeBuilder.Run((x) => ScreenDrawing.ScreenAngle = x);
                        DelayBeat(74, () => ScreenDrawing.ScreenAngle = 0);
                    });

                    RegisterFunctionOnce("ScaleL", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ForBeat(3f, () =>
                            {
                                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.15f * 0.1f;
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                            });
                        });
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White * 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ScaleR", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ForBeat(3f, () =>
                            {
                                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.3f * 0.1f;
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + (-5) * 0.1f;
                            });
                        });
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.SceneOut(Color.Silver * 0.99f, BeatTime(4.5f));
                        });
                    });
                    RegisterFunctionOnce("Restore", () =>
                    {
                        PlaySound(Sounds.heal);
                        Regenerate(1);
                        ScreenDrawing.BoundColor = Color.Red * 0.56f;
                        ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = 110;
                        Color currentLose = Color.White, currentExist = Color.White;
                        ForBeat(3f, () =>
                        {
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.03f;

                            currentLose = Color.Lerp(currentLose, Color.Red, 0.1f);
                            currentExist = Color.Lerp(currentExist, Color.Yellow, 0.1f);

                            ScreenDrawing.HPBar.HPLoseColor = currentLose;
                            ScreenDrawing.HPBar.HPExistColor = currentExist;
                        });
                        rainer.Intensity = 0.8f;
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
                    string[] rhythm = {
                    "Texture1(RGBSplit)(Fade2)(VoidArrow)", "", "", "",   "", "", "", "",   "", "", "", "Fade0",
                    "", "", "", "",   "", "", "", "",   "", "", "", "",
                    "Texture2(Fade2)", "", "", "",   "", "", "", "",   "", "", "", "Fade",
                    "", "", "", "",   "", "", "", "",   "", "", "", "",
                    "Texture3(Fade2)", "", "", "",   "", "", "", "",   "", "", "", "Fade2",
                    "", "", "", "",   "", "", "", "",   "", "", "", "Fade2",
                    "", "", "", "",   "", "", "", "",   "", "", "", "Fade",
                    "", "", "", "(MoveCentre)",   "", "", "", "",   "", "", "", "Fade3",

                    "Easing", "", "", "",   "D", "", "", "",   "+0", "", "", "Fade3",
                    "D1", "", "", "",   "+01", "", "", "",   "+01", "", "", "Fade3",
                    "D", "", "", "",   "+0", "", "", "",   "+0", "", "", "Fade3",
                    "D1", "", "", "",   "+01", "", "", "",   "+01", "", "", "Fade3",
                    "#44#$3", "", "", "",   "$3", "", "", "",   "$3", "", "", "Fade3",
                    "#30#$31", "", "", "",   "$31", "", "", "",   "$31", "", "", "Fade3",
                    "$3", "", "", "",   "$3", "", "", "",   "$3", "", "$3", "$3(Fade3)",
                    "$3", "", "", "ScaleL",    "", "", "", "ScaleR",    "", "", "", "Restore", ""
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1144))
                {
                    ForBeat(4, 500, () =>
                    {
                        CreateEntity(new Particle(Color.White, new Vector2(Rand(4f, 6f), Rand(-0.5f, 0.5f)) * 2, Rand(19f, 25f), new(-121, Rand(0, 480)), Sprites.arrow[0, 1, 0]) { DarkingSpeed = 9f, Rotation = GetRadian(180f) });
                        CreateEntity(new Particle(Color.White, new Vector2(-Rand(4f, 6f), Rand(-0.5f, 0.5f)) * 2, Rand(19f, 25f), new(640 + 121, Rand(0, 480)), Sprites.arrow[1, 1, 0]) { DarkingSpeed = 9f });
                    });

                    sampleIntensity = 0.05f;
                    splitIntensity = 1f;
                    RegisterFunctionOnce("Shader", () =>
                    {
                        var v1 = Shaders.StepSample;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        v1.Intensity = sampleIntensity;
                        ForBeat(144, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.9f + 0.02f * 0.1f;
                            splitIntensity = splitIntensity * 0.9f + 1.00f * 0.1f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                        });
                        ForBeat(144, 48, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.95f + 0.04f * 0.05f;
                            splitIntensity = splitIntensity * 0.95f + 1.50f * 0.05f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.18f;
                        splitIntensity = 3f;
                        ScreenDrawing.CameraEffect.SizeExpand(1.4f, BeatTime(2));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    float time = BeatTime(8);

                    int randVal = 0;
                    string arr10 = $"(<${randVal}0)",
                           arr11 = $"(>${randVal}0)",
                           arr1s = $"(^${randVal}0'1.5)";
                    string arr20 = $"(<${randVal + 1}0)",
                           arr21 = $"(>${randVal + 1}0)",
                           arr2s = $"(^${randVal + 1}0'1.5)";
                    string arr30 = $"(<${randVal + 2}0)",
                           arr31 = $"(>${randVal + 2}0)",
                           arr3s = $"(^${randVal + 2}0'1.5)";
                    string arr40 = $"(<${randVal + 3}0)",
                           arr41 = $"(>${randVal + 3}0)",
                           arr4s = $"(^${randVal + 3}0'1.5)";

                    string[] rhythm = {
                    //1
                    "(Shader)(Blur)(LRotate)", "", "", "",   "(Blur)(RRotate)", "", "", "",   "(R)(Blur)(LRotate)", "", "+0", "",
                    //2
                    "(R)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)ConvulseL", "",
                    $"(${randVal + 2}00)ConvulseR", "",
                    $"(Blur)(${randVal}00)ConvulseL", "",
                    $"(${randVal + 2}00)ConvulseR", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                    $"(BlurDeep)" + arr30 + arr3s, arr31, arr30 + arr3s, arr31,   arr30 + arr3s, "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1192))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(17, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(34, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.28f;
                        splitIntensity = 4f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ArrowDelay", () =>
                    {
                        int randVal = Rand(0, 1) * 2 + 1;
                        string arr10 = $"(<${randVal}0)",
                               arr11 = $"(>${randVal}0)",
                               arr1s = $"(^${randVal}0'2)";
                        string[] rhythm = {
                        arr1s + arr10, arr11, arr1s + arr10, arr11, arr1s + arr10
                        };
                        string[] rhythm2 = {
                        "(R)", "", "+0", "",   "(R)", "", "+0", "",    "(R)", "", "+0", ""
                        };
                        List<Arrow> alphaControls = new();
                        float time = BeatTime(12);
                        for (int i = 0; i < rhythm.Length; i++)
                        {
                            GameObject[] arrows = NormalizedObjects(time, 5.6f, rhythm[i]);
                            foreach (Arrow arrow in arrows)
                            {
                                DelayBeat(11.2f, () => { arrow.Delay(BeatTime(48)); });
                                CreateEntity(arrow);
                                alphaControls.Add(arrow);
                            }
                            time += game.BeatTime(1);
                        }
                        time = BeatTime(12);
                        for (int i = 0; i < rhythm2.Length; i++)
                        {
                            GameObject[] arrows = NormalizedObjects(time, 6f, rhythm2[i]);
                            foreach (Arrow arrow in arrows)
                            {
                                DelayBeat(11.2f, () => { arrow.Delay(BeatTime(12)); });
                                CreateEntity(arrow);
                                alphaControls.Add(arrow);
                            }
                            time += game.BeatTime(1);
                        }
                        foreach (Arrow arrow in alphaControls)
                        {
                            arrow.Alpha = 0.56f;
                        }
                        float alphaVal = 0.56f;
                        ForBeat(12, 12, () =>
                        {
                            alphaVal = alphaVal * 0.85f + 1 * 0.15f;
                            foreach (Arrow arrow in alphaControls) arrow.Alpha = alphaVal;
                        });
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string[] rhythm = {
                    //1
                    "(Blur)(R)(LRotate)", "", "R", "",   "(Blur)+0(RRotate)", "", "+0", "",   "(R)(Blur)(LRotate)", "", "+0", "",
                    //2
                    "(R)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)", "",
                    $"(${randVal + 2}00)(ConvulseR)", "",
                    $"(Blur)(${randVal}00)(ConvulseL)", "",
                    $"(${randVal + 2}00)(ConvulseR)", "",
                    //3 
                    $"(Blur)(${randVal}00)(ArrowDelay)(ConvulseL)", "", "", "",    "(Blur)+0", "", "", "",    "(Blur)+0", "", "D", "+0",
                    //4 
                    "(+0)Shrink(BlurDeep)",    "", "", "", "",    "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1240))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.28f;
                        splitIntensity = 4f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string arr10 = $"(<${randVal}0)",
                           arr11 = $"(>${randVal}0)",
                           arr1s = $"(^${randVal}0'1.5)";
                    string arr20 = $"(<${randVal + 1}0)",
                           arr21 = $"(>${randVal + 1}0)",
                           arr2s = $"(^${randVal + 1}0'1.5)";
                    string arr40 = $"(<${randVal + 1}0)",
                           arr41 = $"(>${randVal + 1}0)",
                           arr4s = $"(^${randVal + 1}0'1.5)";

                    string[] rhythm = {
                    //1
                    "(Blur)(LRotate)", "", "", "",   "(Blur)(RRotate)", "", "", "",   "(Blur)(LRotate)", "", "", "",
                    //2
                    "R(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(ConvulseL)", "",
                    $"(${randVal + 2}00)(ConvulseR)", "",
                    $"(Blur)(${randVal}00)(ConvulseL)", "",
                    $"(${randVal + 2}00)(ConvulseR)", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                     "(BlurDeep)", "", "", "",   "", "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1288))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.32f;
                        splitIntensity = 5f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    RegisterFunctionOnce("ExpandL", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("ExpandR", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(12.2f), true);
                    });
                    RegisterFunctionOnce("FinalRed", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(240, 600, 84);
                        Regenerate(1);
                        PlaySound(Sounds.heal);
                    });

                    float time = BeatTime(8);

                    int randVal = Rand(0, 3);
                    string arr10 = $"(<${randVal}0)",
                           arr11 = $"(>${randVal}0)",
                           arr1s = $"(^${randVal}0'1.5)";
                    string arr20 = $"(<${randVal + 2}0)",
                           arr21 = $"(>${randVal + 2}0)",
                           arr2s = $"(^${randVal + 2}0'1.5)";

                    string[] rhythm = {
                    //1
                    "(Blur)(R)(ConvulseL)", "", "", "",   "(Blur)(ConvulseL)", "", "R", "",   "+0(Blur)(ConvulseL)", "", "+0", "",
                    //2 
                    "(ConvulseR)(Blur)(R)", "", "", "",    "(ConvulseR)(Blur)(R)", "", "", "",    "(ConvulseR)(Blur)(R)", "", "", "",
                    //3  
                    //4 
                    "(BlurDeep)(ExpandL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, "", "",
                    "(BlurDeep)(ExpandR)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, "FinalRed", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1336))
                {
                    RegisterFunctionOnce("Box1", () =>
                    {
                        SetBox(270, 600, 320);
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Circle", () =>
                    {
                        int count = 7;
                        float way = Rand(0, 1) * 2 - 1;
                        float rot = Rand(0, 359);
                        PlaySound(Sounds.spearAppear);
                        Fortimes(count, () =>
                        {
                            CreateSpear(new CircleSpear(Heart.Centre, 3.7f * way, 1.88f, 191, rot += 360f / count, 0.025f));
                        });
                    });
                    RegisterFunctionOnce("Swarm", () =>
                    {
                        int count = 6;
                        float rotation = Rand(0, 359);
                        PlaySound(Sounds.spearAppear);
                        Fortimes(count, () =>
                        {
                            rotation += 360f / count;
                            CreateSpear(new SwarmSpear(Heart.Centre, 5.0f, 177, rotation, BeatTime(6)));
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(15, rotation), 5.0f, 177, rotation + 45, BeatTime(6)));
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(15, rotation), 5.0f, 177, rotation - 45, BeatTime(6)));
                        });
                        DelayBeat(6, () =>
                        {
                            PlaySound(Sounds.spearShoot);
                        });
                    });
                    RegisterFunctionOnce("Align", () =>
                    {
                        bool way = true;
                        float width = 56;
                        float delta = Rand(-width / 2, width / 2);
                        float timeDel = 0.0f;
                        BoxStates.BoxMovingScale = 0.05f;
                        SetBox(270, 600, 280);
                        float y = BoxStates.Up;
                        for (float i = 20; i <= 620; i += width)
                        {
                            way = !way;
                            float x = delta + i;
                            float v = timeDel;
                            DelayBeat(v, () =>
                            {
                                CreateSpear(new Pike(new(x, y), 90, BeatTime(12 - v)) { IsShootMute = true, IsSpawnMute = true });
                            });
                            timeDel += 0.25f;
                        }
                        ForBeat120(12, () =>
                        {
                            Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                        });
                        DelayBeat(8, () =>
                        {
                            PlaySound(Sounds.spearShoot);
                        });
                        ScreenDrawing.CameraEffect.Rotate(45, BeatTime(8));
                    });
                    RegisterFunctionOnce("Restore", () =>
                    {
                        sampleIntensity = 0.5f;
                        splitIntensity = 7;
                        Heart.InstantSetRotation(0);
                        InstantSetBox(Heart.Centre, 16, 16);
                        var v1 = Shaders.StepSample;
                        PlaySound(Sounds.switchScene);
                        PlaySound(Sounds.switchScene);
                        ForBeat(120, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.96f + 0.06f * 0.04f;
                            splitIntensity = splitIntensity * 0.96f + 1.80f * 0.04f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                            ScreenDrawing.ScreenAngle *= 0.7f;
                        });
                    });
                    RegisterFunctionOnce("MoveCentre", () =>
                    {
                        BoxStates.BoxMovingScale = 0.11f;
                        SetBox(240, 84, 84);
                        ForBeat(12, () => InstantTP(BoxStates.Centre));
                    });
                    RegisterFunctionOnce("Enlarge", () =>
                    {
                        SetGreenBox();
                        SetSoul(1);
                    });

                    float time = BeatTime(8);
                    string[] rhythm = {
                    "Box1(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Swarm", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Swarm", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Align", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Restore", "", "", "",    "MoveCentre", "", "", "",    "Enlarge", "", "", "",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1432))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.12f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.31f;
                        splitIntensity = 5f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string arr10 = $"(<${randVal}0)",
                           arr11 = $"(>${randVal}0)",
                           arr1s = $"(^${randVal}0'1.5)";
                    string arr20 = $"(<${randVal + 1}0)",
                           arr21 = $"(>${randVal + 1}0)",
                           arr2s = $"(^${randVal + 1}0'1.5)";
                    string arr30 = $"(<${randVal + 2}0)",
                           arr31 = $"(>${randVal + 2}0)",
                           arr3s = $"(^${randVal + 2}0'1.5)";
                    string arr40 = $"(<${randVal + 3}0)",
                           arr41 = $"(>${randVal + 3}0)",
                           arr4s = $"(^${randVal + 3}0'1.5)";

                    string[] rhythm = {
                    //1
                    "(Blur)(LRotate)(R)", "", "R", "+0",   "(Blur)(RRotate)(R)", "", "R", "+0",   "(Blur)(LRotate)(R)", "", "R", "+0",
                    //2
                    "(R)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(ConvulseL)", "",
                    $"(${randVal + 2}00)(ConvulseR)", "",
                    $"(Blur)(${randVal}00)(ConvulseL)", "",
                    $"(${randVal + 2}00)(ConvulseR)", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                    $"(BlurDeep)" + arr30 + arr3s, arr31, arr30 + arr3s, arr31,   arr30 + arr3s, "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1480))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.14f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.35f;
                        splitIntensity = 6f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.5f), true);
                    });

                    RegisterFunctionOnce("ExpandL", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(26, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("ExpandR", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(26, BeatTime(12.2f), true);
                    });
                    RegisterFunctionOnce("FinalRed", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(240, 600, 84);
                        Regenerate(1);
                    });

                    float time = BeatTime(8);

                    int randVal = 0;
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'3)(^${randVal + 2}1'3)";
                    string arr20 = $"(<${randVal}1)(>${randVal + 2}0)",
                           arr21 = $"(>${randVal}1)(<${randVal + 2}0)",
                           arr2s = $"(^${randVal}1'3)(^${randVal + 2}0'3)";

                    string[] rhythm = {
                    //1
                    "(Blur)(R)(ConvulseL)", "", "", "",   "(Blur)(ConvulseL)", "", "(R)", "",   "+0(Blur)(ConvulseL)", "", "R", "",
                    //2 
                    "(ConvulseR)(Blur)(+0)", "", "", "",    "(ConvulseR)(Blur)(R)", "", "", "",    "(ConvulseR)(Blur)(R)", "", "", "",
                    //3  
                    //4 
                    "(BlurDeep)(ExpandL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, "", "",
                    "(BlurDeep)(ExpandR)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, "FinalRed", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
            }
            public void Hard()
            {
                if (Gametime < -2) return;

                // intro 
                // var v = GetAll<Entity>();
                if (notRegistered)
                {
                    notRegistered = false;
                    RegisterFunction("ToRed", () =>
                    {
                        Player.Heart origin = Heart;
                        Player.Heart heart = CreateHeart(500, 160, 160);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(300, 160, 160);
                        SetSoul(0);

                        SetPlayerBoxMission(origin);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(-300, 84, 84);

                        ForBeat(4, () => origin.InstantTP(origin.controlingBox.Centre));
                        DelayBeat(4, () => origin.Dispose());
                        DelayBeat(4.2f, () => SetPlayerBoxMission(heart));
                    });
                    RegisterFunction("Spear", () =>
                    {
                        CreateSpear(new NormalSpear(Heart.Centre + GetVector2(130, Rand(0, 359))) { WaitingTime = BeatTime(6) });
                    });
                    RegisterFunction("Split", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.8f, BeatTime(2.4f));
                        DelayBeat(1.5f, () =>
                        {
                            rainer.Speed = 6f;
                        });
                        DelayBeat(2.4f, () =>
                        {
                            rainer.Intensity = 0.5f;
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = 140;
                        });

                        Player.Heart origin = Heart;
                        Player.Heart heart = CreateHeart(320, 84, 84);

                        Vector2 centre1 = new(320, 240), centre2 = new(320, 240);
                        Vector2 mission1 = new(260, 190), mission2 = new(380, 290);
                        bool type1 = false, type2 = false;

                        SetPlayerBoxMission(origin);
                        SetBox(mission1, 84, 84);
                        BoxStates.BoxMovingScale = 0;
                        InstantTP(mission1);
                        SetPlayerBoxMission(heart);
                        SetBox(mission2, 84, 84);
                        BoxStates.BoxMovingScale = 0;
                        InstantTP(mission2);

                        Vector2 start = new(320, 240);
                        float time1 = 0;

                        float theta = GetAngle(MathF.Atan2(60, 50));

                        float GetPercent(float v)
                        {
                            return MathF.Pow(v, 0.66f);
                        }
                        ForBeat(3, () =>
                        {
                            time1 += 1 / game.BeatTime(3);
                            SetPlayerBoxMission(origin);
                            type1 = !type1;
                            BoxStates.Centre = type1 ? centre1 : mission1;
                            centre1 = Vector2.Lerp(start, mission1, GetPercent(time1));

                            Line line1, line2;
                            CreateEntity(line1 = new Line(centre1 + new Vector2(33, 33), (s) => { return -theta; }) { DrawingColor = new Color(128, 255, 128, 128) * 0.1f });
                            CreateEntity(line2 = new Line(centre2 + new Vector2(44, -44) * (1.7f - 0.7f * GetPercent(time1)), (s) => { return 90 - theta; }) { DrawingColor = new Color(128, 255, 128, 128) * 0.2f });
                            line1.AlphaDecrease(game.BeatTime(1.8f));
                            line2.AlphaDecrease(game.BeatTime(1.8f));
                        });
                        ForBeat(3, () =>
                        {
                            SetPlayerBoxMission(heart);
                            type2 = !type2;
                            BoxStates.Centre = type2 ? centre2 : mission2;
                            centre2 = Vector2.Lerp(start, mission2, GetPercent(time1));
                            Line line1, line2;
                            CreateEntity(line1 = new Line(centre2 - new Vector2(33, 33), (s) => { return -theta; }) { DrawingColor = new Color(128, 255, 128, 128) * 0.1f });
                            CreateEntity(line2 = new Line(centre2 + new Vector2(-44, 44) * (1.7f - 0.7f * GetPercent(time1)), (s) => { return 90 - theta; }) { DrawingColor = new Color(128, 255, 128, 128) * 0.2f });
                            line1.AlphaDecrease(game.BeatTime(1.8f));
                            line2.AlphaDecrease(game.BeatTime(1.8f));
                        });
                        DelayBeat(3.1f, () =>
                        {
                            SetPlayerBoxMission(origin);
                            BoxStates.Centre = mission1;
                            InstantTP(BoxStates.Centre);
                            Heart.Shields.RemoveShield(Heart.Shields.RShield);

                            SetPlayerBoxMission(heart);
                            BoxStates.Centre = mission2;
                            InstantTP(BoxStates.Centre);
                            Heart.Shields.RemoveShield(Heart.Shields.BShield);

                            float time = 0;
                            ForBeat120(126, () =>
                            {
                                time += 0.5f;
                                SetPlayerBoxMission(origin);
                                BoxStates.Centre = new Vector2(0, 7 * MathF.Sin(time / game.BeatTime(16))) + mission1;
                                TP(BoxStates.Centre);
                                SetPlayerBoxMission(heart);
                                BoxStates.Centre = new Vector2(0, -7 * MathF.Sin(time / game.BeatTime(16))) + mission2;
                                TP(BoxStates.Centre);
                            });
                            ForBeat120(136, 114, () =>
                            {
                                time += 0.5f;
                                SetPlayerBoxMission(origin);
                                BoxStates.Centre = GetVector2(8 * MathF.Sin(time / game.BeatTime(16)), 45) + mission1;
                                InstantTP(BoxStates.Centre);
                                SetPlayerBoxMission(heart);
                                BoxStates.Centre = GetVector2(-8 * MathF.Sin(time / game.BeatTime(16)), 45) + mission2;
                                InstantTP(BoxStates.Centre);
                            });
                            DelayBeat(127, () =>
                            {
                                PlaySound(Sounds.switchScene);
                                PlaySound(Sounds.switchScene);
                                float theta = GetAngle(MathF.Atan2(60, 50));
                                float distance = (BoxStates.Centre - new Vector2(320, 240)).Length();
                                float k = 0;
                                ForBeat(8, () =>
                                {
                                    time = 0;
                                    k = k * 0.9f + 170 * 0.1f;
                                    SetPlayerBoxMission(origin);
                                    mission1 = BoxStates.Centre = new Vector2(320, 240) + GetVector2(distance, theta + 180 + k);
                                    InstantTP(BoxStates.Centre);
                                    SetPlayerBoxMission(heart);
                                    mission2 = BoxStates.Centre = new Vector2(320, 240) + GetVector2(distance, theta + k);
                                    InstantTP(BoxStates.Centre);
                                    rainer.Rotation = rainer.Rotation * 0.9f + (-10f) * 0.1f;
                                });
                            });
                        });
                    });
                    RegisterFunction("DelayArrow", () =>
                    {
                        Arrow arr = MakeArrow(BeatTime(4), 1, 7, 1, 0);
                        DelayBeat(3f, () => { arr.Delay(BeatTime(4)); });
                        CreateEntity(arr);
                    });
                    RegisterFunction("FixRed", () =>
                    {
                        float y1, y2, x1, x2;
                        SetBoxMission(0);
                        x2 = BoxStates.Right;
                        y2 = BoxStates.Down;
                        SetBoxMission(1);
                        x1 = BoxStates.Left;
                        y1 = BoxStates.Up;
                        SetBoxMission(0);
                        BoxStates.BoxMovingScale = 0.25f;
                        SetBox(BoxStates.Left, x2, y1, y2);
                        SetBoxMission(1);
                        BoxStates.BoxMovingScale = 0.25f;
                        SetBox(x1, BoxStates.Right, y1, y2);

                        DelayBeat(2, () =>
                        {
                            SetBoxMission(0);
                            BoxStates.BoxMovingScale = 0.25f;
                            SetBox(320 + 1, x2 + 15, y1, y2);
                            SetBoxMission(1);
                            BoxStates.BoxMovingScale = 0.25f;
                            SetBox(x1 - 15, 320 - 1, y1, y2);
                            SetPlayerMission(0);
                            Heart.RotateTo(180);
                            DelayBeat(2, () =>
                            {
                                SetPlayerMission(0);
                                Heart.InstantSetRotation(180);
                                SetSoul(0);
                                SetPlayerMission(1);
                                SetSoul(0);
                                PlaySound(Sounds.switchScene);
                                PlaySound(Sounds.switchScene);
                            });
                        });
                    });

                    RegisterFunction("LPike", () =>
                    {
                        SetBoxMission(1);
                        float x = Rand(0, 5) * -20 + 320 - 8;
                        float y = BoxStates.Down + 33;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), -90, game.BeatTime(8)) { IsHidden = true });
                        CreateSpear(new Pike(new(x - 7, y), -90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                    });
                    RegisterFunction("RPike", () =>
                    {
                        SetBoxMission(0);
                        float x = Rand(0, 5) * 20 + 320 + 8;
                        float y = BoxStates.Up - 33;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), 90, game.BeatTime(8)) { IsHidden = true });
                        CreateSpear(new Pike(new(x + 7, y), 90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                    });
                    RegisterFunction("HPikeM", () =>
                    {
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + Rand(1, 5) * 16 - 3;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                        Line line = new(new Vector2(BoxStates.Left, y), new Vector2(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(BeatTime(2));
                        CreateEntity(line);
                    });

                    RegisterFunction("MoveBox", () =>
                    {
                        ForBeat120(8, 29 * 4, () =>
                        {
                            SetBoxMission(0);
                            BoxStates.Up += 0.034f;
                            BoxStates.Down -= 0.034f;
                            SetBoxMission(1);
                            BoxStates.Up += 0.034f;
                            BoxStates.Down -= 0.034f;
                        });
                    });
                    RegisterFunction("Merge", () =>
                    {
                        float y1, y2, x1, x2;
                        SetBoxMission(0);
                        x2 = BoxStates.Right;
                        y2 = BoxStates.Down;
                        SetBoxMission(1);
                        x1 = BoxStates.Left;
                        y1 = BoxStates.Up;

                        float yCentre = (y1 + y2) / 2f;
                        float width = x2 - x1, height = y2 - y1;
                        Vector2 centre = new(320, yCentre);
                        CreateLine(yCentre, height);

                        SetBoxMission(0);
                        InstantSetBox(centre, width, height);
                        SetBoxMission(1);
                        InstantSetBox(centre, width, height);

                        PlaySound(Sounds.pierce);
                        DelayBeat(8, () =>
                        {
                            PlaySound(Sounds.damaged);

                            SetPlayerMission(1);
                            Player.Heart mission = Heart;
                            SetPlayerMission(0);
                            Heart.RotateTo(0);
                            Heart.Merge(mission);

                            CentreEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(3), CentreEasing.Alternate(1,
                                CentreEasing.Linear(new(-9, 0), new(0, 0), BeatTime(3)),
                                CentreEasing.Linear(new(9, 0), new(0, 0), BeatTime(3))
                            ));
                            builder.Run((s) =>
                            {
                                ScreenDrawing.ScreenPositionDelta = s;
                            });
                            DelayBeat(3.2f, () =>
                            {
                                ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            });
                        });
                    });
                }

                if (InBeat(2))
                {
                    RegisterFunctionOnce("UShine", () =>
                    {
                        Line line = new(CentreEasing.Stable(new(320, 0)), (s) => new(320, BoxStates.Up));
                        line.DrawingColor = Color.Gold;
                        line.Alpha = 0.5f;
                        line.AlphaDecrease(BeatTime(4));
                        CreateEntity(line);

                        CentreEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(320, 270), BeatTime(4)));
                        easeBuilder.Run((x) => { BoxStates.Centre = x; Heart.InstantTP(x); });
                    });
                    RegisterFunctionOnce("UShine2", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            int t = i;
                            Line line = new(CentreEasing.Stable(new(320 + t * 10, 0)), (s) => new(320 + t * 5, BoxStates.Up));
                            line.DrawingColor = Color.Gold;
                            line.Alpha = 0.5f;
                            line.AlphaDecrease(BeatTime(4));
                            CreateEntity(line);
                        }

                        CentreEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(4), CentreEasing.EaseOutQuad(new(320, BoxStates.Centre.Y), new(320, BoxStates.Centre.Y - 30 / 3f), BeatTime(4)));
                        easeBuilder.Run((x) => { BoxStates.Centre = x; Heart.InstantTP(x); });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-5) * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + 5.2f * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-0.3f) * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateLSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateRSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateMidSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(5));
                    });

                    RegisterFunctionOnce("SideLine", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float xPos = sign * 320 + 320;
                            for (int i = 1; i <= 3; i++)
                            {
                                Line line = new(new Vector2(xPos - i * sign * 12, 0), new Vector2(xPos - i * sign * 12, 480));
                                line.DrawingColor = Color.Yellow;
                                line.Alpha = i / 12f + 0.16f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(3 + i));
                            }
                        }
                    });
                    RegisterFunctionOnce("SpearLine", () =>
                    {
                        Vector2[] origins = {
                        new(120, 100),
                        new(132, 100),
                        new(132, 340),
                        new(148, 334),
                        new(126, 398),
                        new(104, 334),
                        new(120, 340)
                        };
                        Vector2 centre = new(126, 240);
                        Vector2[] deltas = new Vector2[origins.Length];
                        for (int i = 0; i < origins.Length; i++)
                        {
                            deltas[i] = origins[i] - centre;
                        }

                        float rotation = 0, time = 0;
                        Vector2[] positions = new Vector2[origins.Length];
                        ForBeat120(4, () =>
                        {
                            time += 0.5f;
                            rotation -= time * time * 0.003f;
                            for (int i = 0; i < origins.Length; i++)
                            {
                                positions[i] = centre + Rotate(deltas[i], rotation);
                            }
                        });
                        for (int i = 0; i < origins.Length; i++)
                        {
                            int t = i;
                            Line line = new((s) => positions[t], (s) => positions[(t + 1) % positions.Length]);
                            line.DrawingColor = Color.Silver;
                            line.Alpha = 0.3f;
                            CreateEntity(line);
                            DelayBeat(1, () =>
                            {
                                line.AlphaDecrease(BeatTime(3));
                            });
                        }
                    });
                    RegisterFunctionOnce("MirroredLine", () =>
                    {
                        Vector2[] origins = {
                        new(120, 100),
                        new(132, 100),
                        new(132, 340),
                        new(148, 334),
                        new(126, 398),
                        new(104, 334),
                        new(120, 340)
                        };
                        Vector2 centre = new(126, 240);
                        Vector2[] deltas = new Vector2[origins.Length];
                        for (int i = 0; i < origins.Length; i++)
                        {
                            deltas[i] = origins[i] - centre;
                        }

                        float rotation = 0, time = 0;
                        Vector2[] positions = new Vector2[origins.Length];
                        ForBeat120(4, () =>
                        {
                            time += 0.5f;
                            rotation -= time * time * 0.003f;
                            for (int i = 0; i < origins.Length; i++)
                            {
                                positions[i] = centre + Rotate(deltas[i], rotation);
                                positions[i].X = 640 - positions[i].X;
                            }
                        });
                        for (int i = 0; i < origins.Length; i++)
                        {
                            int t = i;
                            Line line = new((s) => positions[t], (s) => positions[(t + 1) % positions.Length]);
                            line.DrawingColor = Color.Silver;
                            line.Alpha = 0.3f;
                            CreateEntity(line);
                            DelayBeat(1, () =>
                            {
                                line.AlphaDecrease(BeatTime(3));
                            });
                        }
                    });
                    RegisterFunctionOnce("SideLine2", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float xPos = sign * 320 + 320;
                            for (int i = 1; i <= 2; i++)
                            {
                                Line line = new(new Vector2(xPos - i * sign * 12, 0), new Vector2(xPos - i * sign * 12, 480));
                                line.DrawingColor = Color.Yellow;
                                line.Alpha = i / 10f + 0.24f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(2 + i * 0.5f));
                            }
                        }
                    });
                    RegisterFunctionOnce("SideLine3", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float yPos = sign * 240 + 240;
                            for (int i = 1; i <= 2; i++)
                            {
                                Line line = new(new Vector2(0, yPos - (i + 1) * sign * 10), new Vector2(640, yPos - (i + 1) * sign * 12));
                                line.DrawingColor = Color.Silver * 0.5f;
                                line.Alpha = i / 10f + 0.24f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(2 + i * 0.5f));
                            }
                        }
                    });
                    RegisterFunctionOnce("FollowLine", () =>
                    {
                        Line up = new((s) => new(320, BoxStates.Up - 12), (s) => ScreenDrawing.ScreenAngle * 0.9f);
                        Line down = new((s) => new(320, BoxStates.Down + 12), (s) => ScreenDrawing.ScreenAngle * 0.9f);
                        Line[] all = { up, down };
                        foreach (Line line in all)
                        {
                            CreateEntity(line);

                            for (int i = 0; i <= 1; i++)
                                DelayBeat(i * 8, () =>
                                {
                                    line.Alpha = 0.2f;
                                    line.AlphaDecrease(BeatTime(3), 0.12f);
                                });
                            DelayBeat(16, () => { line.Alpha = 0.2f; line.AlphaDecrease(BeatTime(4)); });

                            line.DrawingColor = Color.Aqua;
                            line.Alpha = 0.2f;
                        }
                    });

                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(1.8f), false);
                        ScreenDrawing.CameraEffect.SizeShrink(1.41f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(1.8f), true);
                        ScreenDrawing.CameraEffect.SizeShrink(1.4f, BeatTime(3));
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(2f, BeatTime(3.4f));
                    });
                    RegisterFunctionOnce("Expand2", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3f, BeatTime(4.4f));
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(2f, BeatTime(3.4f));
                    });

                    float height = 6;
                    float alpha = 0.1f;
                    RegisterFunctionOnce("Rise", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Black * 0.27f);
                        height += 32;
                        alpha += 0.1f;
                        ForBeat(2, () =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.Red * alpha, 0.2f);
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.DownBoundDistance * 0.8f + height * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("Fade", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.8f, BeatTime(2.7f));
                        DelayBeat(2.5f, () =>
                        {
                            rainer.Intensity = 0.2f;
                            rainer.Speed = 3.0f;
                        });
                    });

                    RegisterFunctionOnce("FallArrow1", () =>
                    {
                        FakeArrow arrow = new(RandIn(0, 1), 0);
                        CreateEntity(arrow);
                        ValueEasing.EaseBuilder moveY = new();
                        moveY.Insert(game.BeatTime(1), ValueEasing.EaseOutQuad(-10, 75, game.BeatTime(1)));
                        moveY.Insert(game.BeatTime(1), ValueEasing.EaseInQuad(75, -10, game.BeatTime(1)));
                        moveY.Run((s) =>
                        {
                            Vector2 t = arrow.Centre;
                            t.X = 100;
                            t.Y = s;
                            arrow.Centre = t;
                        });
                        ValueEasing.EaseBuilder rotate = new();
                        rotate.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(Rand(0, 360), 100 + Rand(0, 360), game.BeatTime(2)));
                        rotate.Run((s) =>
                        {
                            arrow.Rotation = s;
                        });
                        ValueEasing.EaseBuilder alpha = new();
                        alpha.Insert(game.BeatTime(1), ValueEasing.Linear(0, 0.6f, game.BeatTime(1)));
                        alpha.Insert(game.BeatTime(1), ValueEasing.Linear(0.6f, 0, game.BeatTime(1)));
                        alpha.Run((s) =>
                        {
                            arrow.Alpha = s;
                        });
                    });
                    string[] rhythm = {
                    "$3'0.6(UShine)(FallArrow1)", "", "", "",    "", "", "", "",
                    "/", "", "", "",    "", "", "", "RotateL",
                    "$3'0.6(FollowLine)(UShine2)", "", "", "",    "", "", "", "RotateR",
                    "$3'0.6(UShine2)", "", "", "",    "", "", "", "RotateMid",
                    "$3'0.6(UShine2)", "", "", "",    "", "", "", "",
                    "/", "", "", "",    "", "", "RotateRSoft", "",

                    "N3($31)(SideLine)", "", "", "N3",    "", "", "N3", "",
                    "$3", "+0", "", "",    "N3", "", "N3(RotateLSoft)", "",
                    "N31($3)(SideLine)", "", "", "N3",    "", "", "N3", "",
                    "$3", "+0", "", "",    "N3", "", "N3(RotateRSoft)", "",
                    "N3($31)(SideLine)", "", "", "N3",    "", "", "N3", "",
                    "$3", "+0", "", "",    "N3", "", "N3(RotateMidSoft)", "",
                    "(^R'1.5)(^+21'1.5)(SideLine2)(ShrinkL)", "", "", "(^R'1.5)(^+21'1.5)(ShrinkR)",    "", "", "(^R'1.5)(^+21'1.5)(ShrinkL)", "",
                    "/", "(^R'1.5)(^+21'1.5)(ShrinkR)", "", "",    "(^R'1.5)(^+21'1.5)(SideLine2)(ShrinkL)", "", "D(RotateLSoft)", "",
                    "R(SpearLine)(SideLine)", "", "", "D1",    "", "", "D", "",
                    "/", "D1", "", "",    "D", "", "+0(RotateRSoft)", "",
                    "R1(MirroredLine)(SideLine)", "", "", "D",    "", "", "D1", "",
                    "/", "D", "", "",    "D1", "", "+01(RotateLSoft)", "",
                    "R(SideLine)(SideLine3)", "", "", "D1",    "", "", "D", "",
                    "/", "D1", "", "",    "D", "", "+0(RotateMidSoft)", "",
                    "(^R'1.5)(^+21'1.5)(Rise)(Shrink)", "", "", "(^R'1.5)(^+21'1.5)(Rise)(Shrink)",    "", "", "(^R'1.5)(^+21'1.5)(Rise)(Shrink)", "",
                    "/", "(^R'1.5)(^+21'1.5)(Rise)(Expand)", "", "",    "(^R'1.5)(^+21'1.5)(Expand2)", "(Fade)", "ToRed", "",
                };
                    float time = game.BeatTime(14);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(176))
                {
                    string[] rhythm = {
                    "", "", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                };
                    float time = game.BeatTime(16 + 1.1f);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(312))
                {
                    RegisterFunctionOnce("DownCross", () =>
                    {
                        float time = BeatTime(32);
                        for (int i = 0; i <= 30; i++)
                        {
                            DelayBeat(i * 8, () =>
                            {
                                Line line = new(
                                    CentreEasing.EaseInQuad(new(0, 0 - 30), new(0, 480 + 90), time),
                                    CentreEasing.EaseInQuad(new(640, 0 - 90 - 30), new(640, 480), time)
                                );
                                line.Alpha = 0.34f;
                                line.DrawingColor = Color.LightBlue;
                                for (int k = 2; k <= 4; k++) line.InsertRetention(new(k * 2 - 2, 1.3f / k));
                                DelayBeat(32, () => line.Dispose());
                                CreateEntity(line);
                            });
                            DelayBeat(i * 8 + 4, () =>
                            {
                                Line line = new(
                                    CentreEasing.EaseInQuad(new(0, 0 - 90 - 30), new(0, 480), time),
                                    CentreEasing.EaseInQuad(new(640, 0 - 30), new(640, 480 + 90), time)
                                );
                                line.Alpha = 0.34f;
                                line.DrawingColor = Color.LightBlue;
                                for (int k = 2; k <= 4; k++) line.InsertRetention(new(k * 2 - 2, 1.3f / k));
                                DelayBeat(32, () => line.Dispose());
                                CreateEntity(line);
                            });
                        }

                        GlobalResources.Effects.NeonLineShader shader;
                        RenderProduction production;
                        ScreenDrawing.BackGroundRendering.InsertProduction(production = new ScreenDrawing.Shaders.Filter(shader = Shaders.NeonLine, 0.5f));
                        DelayBeat(960 - (440 - 4), () =>
                        {
                            production.Dispose();
                        });

                        Color mission = Color.Blue * 0.22f, cur = Color.Transparent;
                        shader.Speed = 1.0f;
                        shader.DrawingColor = cur;
                        ForBeat(8, () =>
                        {
                            cur = Color.Lerp(cur, mission, 0.2f);
                            shader.DrawingColor = cur;
                        });
                    });
                    RegisterFunctionOnce("RotateLSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateRSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateMidSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(5));
                    });

                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.4f, BeatTime(2), false);
                        ScreenDrawing.CameraEffect.SizeShrink(2.7f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.4f, BeatTime(2), true);
                        ScreenDrawing.CameraEffect.SizeShrink(2.7f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate+", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 7 * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("LRotate", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 5 * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-5) * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1 * 0.2f;
                        });
                        DelayBeat(3.1f, () => { ScreenDrawing.ScreenAngle = 0; });
                    });

                    string[] rhythm = {
                                        "RotateLSoft", "", "", "",
                    "R", "", "", "",    "R", "", "+0", "",
                    "+0", "", "", "",    "R", "", "", "",
                    "R", "", "+0", "",    "D", "", "+0", "",
                    "(R)(+21'1.2)", "", "", "",    "(+2'1.2)(+21)(RotateRSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "+0", "",
                    "+0", "", "", "",    "R", "", "", "",
                    "R", "", "+0", "",    "D", "", "+0", "",
                    "(R)(+21'1.2)", "", "", "",    "(+2'1.2)(+21)(RotateLSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "+0", "",
                    "+0", "", "", "",    "R", "", "", "",
                    "R", "", "+0", "",    "D", "", "+0", "",
                    "(R)(+21'1.2)", "", "", "",    "(+2'1.2)(+21)(RotateMidSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "+1", "",
                    "+1", "", "", "",    "R", "", "+1", "",
                    "+1", "", "", "(ShrinkL)",    "(R)(+21)", "", "", "(ShrinkR)",
                    "(R)(+21)", "", "", "(ShrinkL)",    "(R)(+21)(RotateRSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "R1", "",
                    "R", "", "", "",    "R", "", "", "",
                    "R", "", "R1", "",    "R", "", "", "",
                    "(R)(+21'1.2)", "", "", "",    "(+2'1.2)(+21)(RotateLSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "R1", "",
                    "R", "", "", "",    "R", "", "", "",
                    "R", "", "R1", "",    "R", "", "", "",
                    "(R)(+21'1.2)", "", "", "",    "(+2'1.2)(+21)(RotateMidSoft)", "", "", "",
                    "R", "", "", "(ShrinkL)",    "($0'1.2)", "", "$2'1.2", "",
                    "R", "", "", "(ShrinkR)",    "($2'1.2)", "", "$0'1.2", "",
                    "R", "", "", "",    "R", "", "", "",
                    "R", "", "", "",    "", "", "", "",
                    "R(R1)(LRotate)", "", "", "",    "", "", "R(R1)(RRotate)", "",
                    "", "", "", "",    "R(R1)(LRotate)", "", "", "",
                    "", "", "R(R1)(LRotate+)", "",    "DownCross", "", "", "",
                    "R(R1)(MidRotate)", "", "", "Split",    "", "", "", "",
                };
                    float time = game.BeatTime(8 - 2);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                #region not made
                if (InBeat(440 - 8))
                {
                    RegisterFunctionOnce("MoveLine", () =>
                    {
                        Line left = new(CentreEasing.Combine(CentreEasing.SinWave(20, BeatTime(16), 0, 0), new Vector2(50, 0)), (s) => 90);
                        Line right = new(CentreEasing.Combine(CentreEasing.SinWave(20, BeatTime(16), 0, -180), new Vector2(640 - 50, 0)), (s) => 90);
                        Line[] lines = { left, right };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0.2f;
                            line.Depth = 0.2f;
                            line.DrawingColor = Color.Gold;
                            CreateEntity(line);
                            DelayBeat(128, () => line.AlphaDecrease(BeatTime(2)));
                        }
                    });

                    string[] rhythm = {
                    "", "/", "/", "/",    "/", "/", "^$1", "/",
                    "/", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",

                    "MoveLine", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "FixRed",
                    "R", "/", "/", "/",    "", "/", "/", "/",

                };
                    float time = game.BeatTime(16);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(440 + 8))
                {
                    string[] rhythm = {
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "/", "/",

                    "DelayArrow", "/", "/", "/",    "/", "/", "", "/",
                    "/", "/", "/", "/",    "", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                };
                    float time = game.BeatTime(0);
                    SetPlayerMission(1);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(696 - 0.4f))
                {
                    //create lines
                    for (int i = 4; i < 60; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            Line up = new(CentreEasing.EaseOutQuad(new(320, 0), new(320, BoxStates.Up), BeatTime(8)), (s) => 0);
                            Line down = new(CentreEasing.EaseOutQuad(new(320, 480), new(320, BoxStates.Down), BeatTime(8)), (s) => 0);
                            Line[] lines = { up, down };
                            foreach (Line line in lines)
                            {
                                line.Alpha = 0.4f;
                                line.DrawingColor = Color.LightBlue;
                                line.InsertRetention(new(2, 0.56f));
                                line.InsertRetention(new(4, 0.3f));
                                DelayBeat(4, () =>
                                {
                                    line.AlphaDecrease(BeatTime(4));
                                });
                                CreateEntity(line);
                            }
                        });
                    }

                    string[] rhythm = {
                    "MoveBox", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "LPike", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "Merge", "/", "/", "/",   "", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                };
                    float time = game.BeatTime(0);
                    SetPlayerMission(1);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(832 - 0.26f))
                {
                    float curAlpha = 0.05f;
                    float alphaMax = 0;
                    RegisterFunctionOnce("HPike", () =>
                    {
                        curAlpha = 0.2f;
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + Rand(1, 5) * 16 - 3;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, game.BeatTime(8)) { IsHidden = true });
                        Line line = new(new Vector2(BoxStates.Left, y), new Vector2(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(BeatTime(2));
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("CreateLine", () =>
                    {
                        float rotation = MathUtil.Direction(new(320, 240));
                        Line bottomLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(160, 360)), (s) => rotation);
                        Line bottomRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(480, 360)), (s) => -rotation);
                        Line topLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(160, 120)), (s) => -rotation);
                        Line topRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(480, 120)), (s) => rotation);
                        Line[] lines = { bottomLeft, bottomRight, topLeft, topRight };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.Alpha = curAlpha;
                            line.DrawingColor = Color.Gold;
                        }
                        ForBeat(125, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = curAlpha;
                            }
                            curAlpha = curAlpha * 0.8f + 0.05f * 0.2f;
                            alphaMax += 0.002f;
                            curAlpha = MathF.Min(alphaMax, curAlpha);
                        });
                        DelayBeat(126, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(2));
                            }
                        });
                    });
                    RegisterFunctionOnce("LRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            float limit = 0.06f;
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 5 * 0.2f;
                                ScreenDrawing.ScreenScale += limit * 0.2f;
                                limit *= 0.8f;
                            });
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            float limit = 0.06f;
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-5) * 0.2f;
                                ScreenDrawing.ScreenScale += limit * 0.2f;
                                limit *= 0.8f;
                            });
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            DelayBeat(1, () =>
                            {
                                ScreenDrawing.SceneOut(Color.Silver, BeatTime(3.5f));
                            });
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle *= 0.8f;
                            });
                            DelayBeat(3.4f, () =>
                            {
                                ScreenDrawing.ScreenAngle = 0;
                                ScreenDrawing.ScreenScale = 1;
                                rainer.Intensity = 0;
                                rainer.Rotation = 12;
                                rainer.Speed = 7;
                                ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = 0;

                                InstantSetBox(600, 84, 84);
                                SetSoul(1);
                                InstantTP(BoxStates.Centre);

                                ScreenDrawing.UIColor = Color.Transparent;
                                ScreenDrawing.ThemeColor = Color.Transparent;
                                ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                                ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                                Heart.Shields.AddShield(Heart.Shields.BShield);
                            });
                        });
                    });
                    float time = game.BeatTime(0);
                    string[] rhythm = {
                    "HPike(CreateLine)", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPikeM(LRotate)", "/", "/", "/",   "/", "/", "HPikeM(RRotate)" , "/",
                    "/", "/", "/", "/",   "HPikeM(LRotate)", "/", "/" , "/",
                    "/", "/", "HPikeM(RRotate)", "/",   "/", "/", "/" , "/",
                    "HPikeM(MidRotate)", "/", "/", "/",   "/", "/", "/" , "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                    CentreEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(128), CentreEasing.YSinWave(6, BeatTime(32), 0));
                    builder.Run((s) => { ScreenDrawing.ScreenPositionDelta = s; });
                }
                #endregion
                if (InBeat(960))
                {
                    float time = game.BeatTime(0);
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    RegisterFunctionOnce("Texture1", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash0, BeatTime(3 * 4)) { FadeSpeed = 0.32f });
                    });
                    RegisterFunctionOnce("Texture2", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash1, BeatTime(3 * 4)));
                    });
                    RegisterFunctionOnce("Texture3", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash2, BeatTime(9 * 4)));

                        List<TextAttribute> attributes = new();
                        attributes.Add(new TextSpeedAttribute(55));
                        attributes.Add(new TextSizeAttribute(0.75f));
                        attributes.Add(new TextFadeoutAttribute(BeatTime(36), BeatTime(4)));
                        for (int i = 1; i <= 5; i++)
                            attributes.Add(new TextTimeThreshold(BeatTime(i * 4)));
                        TextPrinter printer;
                        CreateEntity(printer = new TextPrinter("$$$Is $something $still $nostalgic $in $memory?", new Vector2(60, 340), attributes.ToArray()));
                        DelayBeat(41, () => printer.Dispose());
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                    RegisterFunctionOnce("MoveCentre", () =>
                    {
                        BoxStates.BoxMovingScale = 0.04f;
                        SetGreenBox();

                        ScreenDrawing.UIColor = Color.White;
                        ScreenDrawing.ThemeColor = Color.White;
                        ScreenDrawing.HPBar.HPExistColor = Color.White;
                        ScreenDrawing.HPBar.HPLoseColor = Color.White;

                        ForBeat120(32, () => { TP(BoxStates.Centre); ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.87f + 0.995f * 0.13f; });
                    });
                    RegisterFunctionOnce("Easing", () =>
                    {
                        ValueEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(73), ValueEasing.LerpTo(0, 0.05f, ValueEasing.SinWave(5, BeatTime(48), 0)));
                        easeBuilder.Run((x) => ScreenDrawing.ScreenAngle = x);
                        DelayBeat(74, () => ScreenDrawing.ScreenAngle = 0);
                    });

                    RegisterFunctionOnce("ScaleL", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ForBeat(3f, () =>
                            {
                                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.15f * 0.1f;
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                            });
                        });
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White * 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ScaleR", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ForBeat(3f, () =>
                            {
                                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.3f * 0.1f;
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + (-5) * 0.1f;
                            });
                        });
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.SceneOut(Color.Silver * 0.99f, BeatTime(4.5f));
                        });
                    });
                    RegisterFunctionOnce("Restore", () =>
                    {
                        PlaySound(Sounds.heal);
                        Regenerate(1);
                        ScreenDrawing.BoundColor = Color.Red * 0.56f;
                        ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = 110;
                        Color currentLose = Color.White, currentExist = Color.White;
                        ForBeat(3f, () =>
                        {
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.03f;

                            currentLose = Color.Lerp(currentLose, Color.Red, 0.1f);
                            currentExist = Color.Lerp(currentExist, Color.Yellow, 0.1f);

                            ScreenDrawing.HPBar.HPLoseColor = currentLose;
                            ScreenDrawing.HPBar.HPExistColor = currentExist;
                        });
                        rainer.Intensity = 0.8f;
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
                    string[] rhythm = {
                    "Texture1(RGBSplit)(Fade2)(VoidArrow)", "", "", "",   "", "", "", "",   "", "", "", "Fade0",
                    "", "", "", "",   "", "", "", "",   "", "", "", "",
                    "Texture2(Fade2)", "", "", "",   "", "", "", "",   "", "", "", "Fade",
                    "", "", "", "",   "", "", "", "",   "", "", "", "",
                    "Texture3(Fade2)", "", "", "",   "", "", "", "",   "", "", "", "Fade2",
                    "", "", "", "",   "", "", "", "",   "", "", "", "Fade2",
                    "", "", "", "",   "", "", "", "",   "", "", "", "Fade",
                    "", "", "", "(MoveCentre)",   "", "", "", "",   "", "", "", "Fade3",

                    "Easing", "", "", "",   "D", "", "", "",   "D", "", "", "Fade3",
                    "D1", "", "", "",   "D1", "", "", "",   "D1", "", "", "Fade3",
                    "D", "", "", "",   "D", "", "", "",   "N3", "", "", "Fade3",
                    "D1", "", "", "",   "D1", "", "", "",   "N31", "", "", "Fade3",
                    "#44#$3", "", "", "",   "$3", "", "", "",   "$3", "", "", "Fade3",
                    "#30#$31", "", "", "",   "$31", "", "", "",   "$31", "", "", "Fade3",
                    "N3", "", "", "",   "N3", "", "", "",   "N3", "", "$01", "+2(Fade3)",
                    "+21", "", "", "ScaleL",    "", "", "", "ScaleR",    "", "", "", "Restore", ""
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1144))
                {
                    ForBeat(4, 500, () =>
                    {
                        CreateEntity(new Particle(Color.White, new Vector2(Rand(4f, 6f), Rand(-0.5f, 0.5f)) * 2, Rand(19f, 25f), new(-121, Rand(0, 480)), Sprites.arrow[0, 1, 0]) { DarkingSpeed = 9f, Rotation = GetRadian(180f) });
                        CreateEntity(new Particle(Color.White, new Vector2(-Rand(4f, 6f), Rand(-0.5f, 0.5f)) * 2, Rand(19f, 25f), new(640 + 121, Rand(0, 480)), Sprites.arrow[1, 1, 0]) { DarkingSpeed = 9f });
                    });

                    sampleIntensity = 0.05f;
                    splitIntensity = 1f;
                    RegisterFunctionOnce("Shader", () =>
                    {
                        var v1 = Shaders.StepSample;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        v1.Intensity = sampleIntensity;
                        ForBeat(144, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.9f + 0.02f * 0.1f;
                            splitIntensity = splitIntensity * 0.9f + 1.00f * 0.1f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                        });
                        ForBeat(144, 48, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.95f + 0.04f * 0.05f;
                            splitIntensity = splitIntensity * 0.95f + 1.50f * 0.05f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.18f;
                        splitIntensity = 3f;
                        ScreenDrawing.CameraEffect.SizeExpand(1.4f, BeatTime(2));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    float time = BeatTime(8);

                    int randVal = 0;
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'1.5)(^${randVal + 2}1'1.5)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'1.5)(^${randVal + 3}1'1.5)";
                    string arr30 = $"(<${randVal + 2}0)(>${randVal + 0}1)",
                           arr31 = $"(>${randVal + 2}0)(<${randVal + 0}1)",
                           arr3s = $"(^${randVal + 2}0'1.5)(^${randVal + 0}1'1.5)";
                    string arr40 = $"(<${randVal + 3}0)(>${randVal + 1}1)",
                           arr41 = $"(>${randVal + 3}0)(<${randVal + 1}1)",
                           arr4s = $"(^${randVal + 3}0'1.5)(^${randVal + 1}1'1.5)";

                    string[] rhythm = {
                    //1
                    "(Shader)(Blur)(LRotate)", "", "", "",   "(Blur)(RRotate)", "", "", "",   "(R)(+01)(Blur)(LRotate)", "", "R", "",
                    //2
                    "(R)(+01)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)ConvulseL", "",
                    $"(${randVal}11)(${randVal + 2}01)ConvulseR", "",
                    $"(Blur)(${randVal}01)(${randVal + 2}11)ConvulseL", "",
                    $"(${randVal}11)(${randVal + 2}01)ConvulseR", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                    $"(BlurDeep)" + arr30 + arr3s, arr31, arr30 + arr3s, arr31,   arr30 + arr3s, "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1192))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(17, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(34, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.28f;
                        splitIntensity = 4f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ArrowDelay", () =>
                    {
                        int randVal = Rand(0, 1) * 2 + 1;
                        string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                               arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                               arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                        string[] rhythm = {
                        arr1s + arr10, arr11, arr1s + arr10, arr11, arr1s + arr10
                        };
                        string[] rhythm2 = {
                        "R(R1)", "", "R", "",   "R1(R)", "", "R1", "",    "R(R1)", "", "R", ""
                        };
                        List<Arrow> alphaControls = new();
                        float time = BeatTime(12);
                        for (int i = 0; i < rhythm.Length; i++)
                        {
                            GameObject[] arrows = NormalizedObjects(time, 5.6f, rhythm[i]);
                            foreach (Arrow arrow in arrows)
                            {
                                DelayBeat(11.2f, () => { arrow.Delay(BeatTime(48)); });
                                CreateEntity(arrow);
                                alphaControls.Add(arrow);
                            }
                            time += game.BeatTime(1);
                        }
                        time = BeatTime(12);
                        for (int i = 0; i < rhythm2.Length; i++)
                        {
                            GameObject[] arrows = NormalizedObjects(time, 6f, rhythm2[i]);
                            foreach (Arrow arrow in arrows)
                            {
                                DelayBeat(11.2f, () => { arrow.Delay(BeatTime(12)); });
                                CreateEntity(arrow);
                                alphaControls.Add(arrow);
                            }
                            time += game.BeatTime(1);
                        }
                        foreach (Arrow arrow in alphaControls)
                        {
                            arrow.Alpha = 0.56f;
                        }
                        float alphaVal = 0.56f;
                        ForBeat(12, 12, () =>
                        {
                            alphaVal = alphaVal * 0.85f + 1 * 0.15f;
                            foreach (Arrow arrow in alphaControls) arrow.Alpha = alphaVal;
                        });
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string[] rhythm = {
                    //1
                    "(Blur)R(R1)(LRotate)", "", "R", "",   "(Blur)R1(R)(RRotate)", "", "R1", "",   "R(R1)(Blur)(LRotate)", "", "R", "",
                    //2
                    "R(R1)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(ConvulseL)(${randVal}00)(${randVal + 2}10)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    $"(Blur)(${randVal}01)(${randVal + 2}11)(ConvulseL)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    //3 
                    $"(Blur)(${randVal}01)(${randVal + 2}11)(ArrowDelay)(ConvulseL)", "", "", "",    "(Blur)+0(R1)", "", "", "",    "(Blur)+0(R1)", "", "$0($01)", "$1($11)",
                    //4 
                    "($2)($21)Shrink(BlurDeep)",    "", "", "", "",    "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1240))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.28f;
                        splitIntensity = 4f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'2)(^${randVal + 3}1'2)";
                    string arr40 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr41 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr4s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";

                    string[] rhythm = {
                    //1
                    "(Blur)(LRotate)", "", "", "",   "(Blur)(RRotate)", "", "", "",   "(Blur)(LRotate)", "", "", "",
                    //2
                    "R(R1)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    $"(Blur)(${randVal}01)(${randVal + 2}11)(ConvulseL)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                     "(BlurDeep)", "", "", "",   "", "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1288))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.32f;
                        splitIntensity = 5f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    RegisterFunctionOnce("ExpandL", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("ExpandR", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(12.2f), true);
                    });
                    RegisterFunctionOnce("FinalRed", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(240, 600, 84);
                        Regenerate(1);
                        PlaySound(Sounds.heal);
                    });

                    float time = BeatTime(8);

                    int randVal = Rand(0, 3);
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                    string arr20 = $"(<${randVal}1)(>${randVal + 2}0)",
                           arr21 = $"(>${randVal}1)(<${randVal + 2}0)",
                           arr2s = $"(^${randVal}1'2)(^${randVal + 2}0'2)";

                    string[] rhythm = {
                    //1
                    "(Blur)R(R1)(ConvulseL)", "", "", "",   "(Blur)(ConvulseL)", "", "R1", "",   "R1(Blur)(ConvulseL)", "", "R1", "",
                    //2 
                    "(ConvulseR)(Blur)R(R1)", "", "", "",    "(ConvulseR)(Blur)R(R1)", "", "", "",    "(ConvulseR)(Blur)R(R1)", "", "", "",
                    //3  
                    //4 
                    "(BlurDeep)(ExpandL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, "", "",
                    "(BlurDeep)(ExpandR)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, "FinalRed", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1336))
                {
                    RegisterFunctionOnce("Box1", () =>
                    {
                        SetBox(270, 600, 320);
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Circle", () =>
                    {
                        int count = 9;
                        float way = Rand(0, 1) * 2 - 1;
                        float rot = Rand(0, 359);
                        PlaySound(Sounds.spearAppear);
                        Fortimes(count, () =>
                        {
                            CreateSpear(new CircleSpear(Heart.Centre, 3.7f * way, 1.88f, 187f, rot += 360f / count, 0.025f));
                        });
                    });
                    RegisterFunctionOnce("Swarm", () =>
                    {
                        int count = 8;
                        float rotation = Rand(0, 359);
                        PlaySound(Sounds.spearAppear);
                        Fortimes(count, () =>
                        {
                            rotation += 360f / count;
                            CreateSpear(new SwarmSpear(Heart.Centre, 5.0f, 164, rotation, BeatTime(6)));
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(15, rotation), 5.0f, 164, rotation + 45, BeatTime(6)));
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(15, rotation), 5.0f, 164, rotation - 45, BeatTime(6)));
                        });
                        DelayBeat(6, () =>
                        {
                            PlaySound(Sounds.spearShoot);
                        });
                    });
                    RegisterFunctionOnce("Align", () =>
                    {
                        bool way = true;
                        float width = 36;
                        float delta = Rand(-width / 2, width / 2);
                        float timeDel = 0.0f;
                        BoxStates.BoxMovingScale = 0.05f;
                        SetBox(270, 600, 280);
                        float y = BoxStates.Up;
                        for (float i = 20; i <= 620; i += width)
                        {
                            way = !way;
                            float x = delta + i;
                            float v = timeDel;
                            DelayBeat(v, () =>
                            {
                                CreateSpear(new Pike(new(x, y), 90, BeatTime(12 - v)) { IsShootMute = true, IsSpawnMute = true });
                            });
                            timeDel += 0.25f;
                        }
                        ForBeat120(12, () =>
                        {
                            Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                        });
                        DelayBeat(8, () =>
                        {
                            PlaySound(Sounds.spearShoot);
                        });
                        ScreenDrawing.CameraEffect.Rotate(45, BeatTime(8));
                    });
                    RegisterFunctionOnce("Restore", () =>
                    {
                        sampleIntensity = 0.5f;
                        splitIntensity = 7;
                        Heart.InstantSetRotation(0);
                        InstantSetBox(Heart.Centre, 16, 16);
                        var v1 = Shaders.StepSample;
                        PlaySound(Sounds.switchScene);
                        PlaySound(Sounds.switchScene);
                        ForBeat(120, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.96f + 0.06f * 0.04f;
                            splitIntensity = splitIntensity * 0.96f + 1.80f * 0.04f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                            ScreenDrawing.ScreenAngle *= 0.7f;
                        });
                    });
                    RegisterFunctionOnce("MoveCentre", () =>
                    {
                        BoxStates.BoxMovingScale = 0.11f;
                        SetBox(240, 84, 84);
                        ForBeat(12, () => InstantTP(BoxStates.Centre));
                    });
                    RegisterFunctionOnce("Enlarge", () =>
                    {
                        SetGreenBox();
                        SetSoul(1);
                    });

                    float time = BeatTime(8);
                    string[] rhythm = {
                    "Box1(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Swarm", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Swarm", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Align", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Restore", "", "", "",    "MoveCentre", "", "", "",    "Enlarge", "", "", "",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1432))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.12f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.31f;
                        splitIntensity = 5f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2.5)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'2.5)(^${randVal + 3}1'2.5)";
                    string arr30 = $"(<${randVal + 2}0)(>${randVal + 0}1)",
                           arr31 = $"(>${randVal + 2}0)(<${randVal + 0}1)",
                           arr3s = $"(^${randVal + 2}0'2.5)(^${randVal + 0}1'2.5)";
                    string arr40 = $"(<${randVal + 3}0)(>${randVal + 1}1)",
                           arr41 = $"(>${randVal + 3}0)(<${randVal + 1}1)",
                           arr4s = $"(^${randVal + 3}0'2.5)(^${randVal + 1}1'2.5)";

                    string[] rhythm = {
                    //1
                    "(Blur)(LRotate)R(R1)", "", "R", "+0",   "(Blur)(RRotate)R(R1)", "", "R1", "+01",   "(Blur)(LRotate)R(R1)", "", "R", "+0",
                    //2
                    "R(R1)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}10)(${randVal + 2}00)(ConvulseR)", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                    $"(BlurDeep)" + arr30 + arr3s, arr31, arr30 + arr3s, arr31,   arr30 + arr3s, "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1480))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.14f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.35f;
                        splitIntensity = 6f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.5f), true);
                    });

                    RegisterFunctionOnce("ExpandL", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(26, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("ExpandR", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(26, BeatTime(12.2f), true);
                    });
                    RegisterFunctionOnce("FinalRed", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(240, 600, 84);
                        Regenerate(1);
                    });

                    float time = BeatTime(8);

                    int randVal = Rand(0, 3);
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'3)(^${randVal + 2}1'3)";
                    string arr20 = $"(<${randVal}1)(>${randVal + 2}0)",
                           arr21 = $"(>${randVal}1)(<${randVal + 2}0)",
                           arr2s = $"(^${randVal}1'3)(^${randVal + 2}0'3)";

                    string[] rhythm = {
                    //1
                    "(Blur)(R)(+01)(ConvulseL)", "", "", "",   "(Blur)(ConvulseL)", "", "(R)", "",   "R1(Blur)(ConvulseL)", "", "R", "",
                    //2 
                    "(ConvulseR)(Blur)(R)(+01)", "", "", "",    "(ConvulseR)(Blur)(R)(+01)", "", "", "",    "(ConvulseR)(Blur)(R)(+01)", "", "", "",
                    //3  
                    //4 
                    "(BlurDeep)(ExpandL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, "", "",
                    "(BlurDeep)(ExpandR)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, "FinalRed", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
            }
            public void Easy()
            {
                if (Gametime < -2) return;

                // intro 
                // var v = GetAll<Entity>();
                if (notRegistered)
                {
                    notRegistered = false;
                    RegisterFunction("ToRed", () =>
                    {
                        Player.Heart origin = Heart;
                        Player.Heart heart = CreateHeart(500, 160, 160);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(300, 160, 160);
                        SetSoul(0);

                        SetPlayerBoxMission(origin);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(-300, 84, 84);

                        ForBeat(4, () => origin.InstantTP(origin.controlingBox.Centre));
                        DelayBeat(4, () => origin.Dispose());
                        DelayBeat(4.2f, () => SetPlayerBoxMission(heart));
                    });
                    RegisterFunction("Spear", () =>
                    {
                        CreateSpear(new NormalSpear(Heart.Centre + GetVector2(130, Rand(0, 359))) { WaitingTime = BeatTime(6) });
                    });
                }

                if (InBeat(2))
                {
                    RegisterFunctionOnce("UShine", () =>
                    {
                        Line line = new(CentreEasing.Stable(new(320, 0)), (s) => new(320, BoxStates.Up));
                        line.DrawingColor = Color.Gold;
                        line.Alpha = 0.5f;
                        line.AlphaDecrease(BeatTime(4));
                        CreateEntity(line);

                        CentreEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(320, 270), BeatTime(4)));
                        easeBuilder.Run((x) => { BoxStates.Centre = x; Heart.InstantTP(x); });
                    });
                    RegisterFunctionOnce("UShine2", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            int t = i;
                            Line line = new(CentreEasing.Stable(new(320 + t * 10, 0)), (s) => new(320 + t * 5, BoxStates.Up));
                            line.DrawingColor = Color.Gold;
                            line.Alpha = 0.5f;
                            line.AlphaDecrease(BeatTime(4));
                            CreateEntity(line);
                        }

                        CentreEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(4), CentreEasing.EaseOutQuad(new(320, BoxStates.Centre.Y), new(320, BoxStates.Centre.Y - 30 / 3f), BeatTime(4)));
                        easeBuilder.Run((x) => { BoxStates.Centre = x; Heart.InstantTP(x); });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-5) * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + 5.2f * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-0.3f) * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateLSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateRSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateMidSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(5));
                    });

                    RegisterFunctionOnce("SideLine", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float xPos = sign * 320 + 320;
                            for (int i = 1; i <= 3; i++)
                            {
                                Line line = new(new Vector2(xPos - i * sign * 12, 0), new Vector2(xPos - i * sign * 12, 480));
                                line.DrawingColor = Color.Yellow;
                                line.Alpha = i / 12f + 0.16f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(3 + i));
                            }
                        }
                    });
                    RegisterFunctionOnce("SpearLine", () =>
                    {
                        Vector2[] origins = {
                        new(120, 100),
                        new(132, 100),
                        new(132, 340),
                        new(148, 334),
                        new(126, 398),
                        new(104, 334),
                        new(120, 340)
                        };
                        Vector2 centre = new(126, 240);
                        Vector2[] deltas = new Vector2[origins.Length];
                        for (int i = 0; i < origins.Length; i++)
                        {
                            deltas[i] = origins[i] - centre;
                        }

                        float rotation = 0, time = 0;
                        Vector2[] positions = new Vector2[origins.Length];
                        ForBeat120(4, () =>
                        {
                            time += 0.5f;
                            rotation -= time * time * 0.003f;
                            for (int i = 0; i < origins.Length; i++)
                            {
                                positions[i] = centre + Rotate(deltas[i], rotation);
                            }
                        });
                        for (int i = 0; i < origins.Length; i++)
                        {
                            int t = i;
                            Line line = new((s) => positions[t], (s) => positions[(t + 1) % positions.Length]);
                            line.DrawingColor = Color.Silver;
                            line.Alpha = 0.3f;
                            CreateEntity(line);
                            DelayBeat(1, () =>
                            {
                                line.AlphaDecrease(BeatTime(3));
                            });
                        }
                    });
                    RegisterFunctionOnce("MirroredLine", () =>
                    {
                        Vector2[] origins = {
                        new(120, 100),
                        new(132, 100),
                        new(132, 340),
                        new(148, 334),
                        new(126, 398),
                        new(104, 334),
                        new(120, 340)
                        };
                        Vector2 centre = new(126, 240);
                        Vector2[] deltas = new Vector2[origins.Length];
                        for (int i = 0; i < origins.Length; i++)
                        {
                            deltas[i] = origins[i] - centre;
                        }

                        float rotation = 0, time = 0;
                        Vector2[] positions = new Vector2[origins.Length];
                        ForBeat120(4, () =>
                        {
                            time += 0.5f;
                            rotation -= time * time * 0.003f;
                            for (int i = 0; i < origins.Length; i++)
                            {
                                positions[i] = centre + Rotate(deltas[i], rotation);
                                positions[i].X = 640 - positions[i].X;
                            }
                        });
                        for (int i = 0; i < origins.Length; i++)
                        {
                            int t = i;
                            Line line = new((s) => positions[t], (s) => positions[(t + 1) % positions.Length]);
                            line.DrawingColor = Color.Silver;
                            line.Alpha = 0.3f;
                            CreateEntity(line);
                            DelayBeat(1, () =>
                            {
                                line.AlphaDecrease(BeatTime(3));
                            });
                        }
                    });
                    RegisterFunctionOnce("SideLine2", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float xPos = sign * 320 + 320;
                            for (int i = 1; i <= 2; i++)
                            {
                                Line line = new(new Vector2(xPos - i * sign * 12, 0), new Vector2(xPos - i * sign * 12, 480));
                                line.DrawingColor = Color.Yellow;
                                line.Alpha = i / 10f + 0.24f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(2 + i * 0.5f));
                            }
                        }
                    });
                    RegisterFunctionOnce("SideLine3", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float yPos = sign * 240 + 240;
                            for (int i = 1; i <= 2; i++)
                            {
                                Line line = new(new Vector2(0, yPos - (i + 1) * sign * 10), new Vector2(640, yPos - (i + 1) * sign * 12));
                                line.DrawingColor = Color.Silver * 0.5f;
                                line.Alpha = i / 10f + 0.24f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(2 + i * 0.5f));
                            }
                        }
                    });
                    RegisterFunctionOnce("FollowLine", () =>
                    {
                        Line up = new((s) => new(320, BoxStates.Up - 12), (s) => ScreenDrawing.ScreenAngle * 0.9f);
                        Line down = new((s) => new(320, BoxStates.Down + 12), (s) => ScreenDrawing.ScreenAngle * 0.9f);
                        Line[] all = { up, down };
                        foreach (Line line in all)
                        {
                            CreateEntity(line);

                            for (int i = 0; i <= 1; i++)
                                DelayBeat(i * 8, () =>
                                {
                                    line.Alpha = 0.2f;
                                    line.AlphaDecrease(BeatTime(3), 0.12f);
                                });
                            DelayBeat(16, () => { line.Alpha = 0.2f; line.AlphaDecrease(BeatTime(4)); });

                            line.DrawingColor = Color.Aqua;
                            line.Alpha = 0.2f;
                        }
                    });

                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(1.8f), false);
                        ScreenDrawing.CameraEffect.SizeShrink(1.41f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(1.8f), true);
                        ScreenDrawing.CameraEffect.SizeShrink(1.4f, BeatTime(3));
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(2f, BeatTime(3.4f));
                    });
                    RegisterFunctionOnce("Expand2", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3f, BeatTime(4.4f));
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(2f, BeatTime(3.4f));
                    });

                    float height = 6;
                    float alpha = 0.1f;
                    RegisterFunctionOnce("Rise", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Black * 0.27f);
                        height += 32;
                        alpha += 0.1f;
                        ForBeat(2, () =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.Red * alpha, 0.2f);
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.DownBoundDistance * 0.8f + height * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("Fade", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.8f, BeatTime(2.7f));
                        DelayBeat(2.5f, () =>
                        {
                            rainer.Intensity = 0.2f;
                            rainer.Speed = 3.0f;
                        });
                    });

                    RegisterFunctionOnce("FallArrow1", () =>
                    {
                        FakeArrow arrow = new(RandIn(0, 1), 0);
                        CreateEntity(arrow);
                        ValueEasing.EaseBuilder moveY = new();
                        moveY.Insert(game.BeatTime(1), ValueEasing.EaseOutQuad(-10, 75, game.BeatTime(1)));
                        moveY.Insert(game.BeatTime(1), ValueEasing.EaseInQuad(75, -10, game.BeatTime(1)));
                        moveY.Run((s) =>
                        {
                            Vector2 t = arrow.Centre;
                            t.X = 100;
                            t.Y = s;
                            arrow.Centre = t;
                        });
                        ValueEasing.EaseBuilder rotate = new();
                        rotate.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(Rand(0, 360), 100 + Rand(0, 360), game.BeatTime(2)));
                        rotate.Run((s) =>
                        {
                            arrow.Rotation = s;
                        });
                        ValueEasing.EaseBuilder alpha = new();
                        alpha.Insert(game.BeatTime(1), ValueEasing.Linear(0, 0.6f, game.BeatTime(1)));
                        alpha.Insert(game.BeatTime(1), ValueEasing.Linear(0.6f, 0, game.BeatTime(1)));
                        alpha.Run((s) =>
                        {
                            arrow.Alpha = s;
                        });
                    });
                    string[] rhythm = {
                    "$3'0.6(UShine)(FallArrow1)", "", "", "",    "", "", "", "",
                    "/", "", "", "",    "", "", "", "RotateL",
                    "$3'0.6(FollowLine)(UShine2)", "", "", "",    "", "", "", "RotateR",
                    "$3'0.6(UShine2)", "", "", "",    "", "", "", "RotateMid",
                    "$3'0.6(UShine2)", "", "", "",    "", "", "", "",
                    "/", "", "", "",    "", "", "RotateRSoft", "",

                    "N3($31)(SideLine)", "", "", "N3",    "", "", "N3", "",
                    "$3", "+0", "", "",    "N3", "", "N3(RotateLSoft)", "",
                    "N31($3)(SideLine)", "", "", "N3",    "", "", "N3", "",
                    "$3", "+0", "", "",    "N3", "", "N3(RotateRSoft)", "",
                    "N3($31)(SideLine)", "", "", "N3",    "", "", "N3", "",
                    "$3", "+0", "", "",    "N3", "", "N3(RotateMidSoft)", "",
                    "(^R'1.5)(^+01'1.5)(SideLine2)(ShrinkL)", "", "", "(^R'1.5)(^+01'1.5)(ShrinkR)",    "", "", "(^R'1.5)(^+01'1.5)(ShrinkL)", "",
                    "/", "(^R'1.5)(^+01'1.5)(ShrinkR)", "", "",    "(^R'1.5)(^+01'1.5)(SideLine2)(ShrinkL)", "", "+0(RotateLSoft)", "",
                    "R(SpearLine)(SideLine)", "", "", "D",    "", "", "D", "",
                    "/", "D", "", "",    "D", "", "+0(RotateRSoft)", "",
                    "R1(MirroredLine)(SideLine)", "", "", "D1",    "", "", "D1", "",
                    "/", "D1", "", "",    "D1", "", "+01(RotateLSoft)", "",
                    "R(SideLine)(SideLine3)", "", "", "D",    "", "", "D", "",
                    "/", "D", "", "",    "D", "", "+0(RotateMidSoft)", "",
                    "(^R'1.5)(^+01'1.5)(Rise)(Shrink)", "", "", "(^R'1.5)(^+01'1.5)(Rise)(Shrink)",    "", "", "(^R'1.5)(^+01'1.5)(Rise)(Shrink)", "",
                    "/", "(^R'1.5)(^+01'1.5)(Rise)(Expand)", "", "",    "(^R'1.5)(^+01'1.5)(Expand2)", "(Fade)", "ToRed", "",
                };
                    float time = game.BeatTime(14);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(176))
                {
                    string[] rhythm = {
                    "", "", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                };
                    float time = game.BeatTime(16 + 1.1f);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(312))
                {
                    RegisterFunctionOnce("DownCross", () =>
                    {
                        float time = BeatTime(32);
                        for (int i = 0; i <= 30; i++)
                        {
                            DelayBeat(i * 8, () =>
                            {
                                Line line = new(
                                    CentreEasing.EaseInQuad(new(0, 0 - 30), new(0, 480 + 90), time),
                                    CentreEasing.EaseInQuad(new(640, 0 - 90 - 30), new(640, 480), time)
                                );
                                line.Alpha = 0.34f;
                                line.DrawingColor = Color.LightBlue;
                                for (int k = 2; k <= 4; k++) line.InsertRetention(new(k * 2 - 2, 1.3f / k));
                                DelayBeat(32, () => line.Dispose());
                                CreateEntity(line);
                            });
                            DelayBeat(i * 8 + 4, () =>
                            {
                                Line line = new(
                                    CentreEasing.EaseInQuad(new(0, 0 - 90 - 30), new(0, 480), time),
                                    CentreEasing.EaseInQuad(new(640, 0 - 30), new(640, 480 + 90), time)
                                );
                                line.Alpha = 0.34f;
                                line.DrawingColor = Color.LightBlue;
                                for (int k = 2; k <= 4; k++) line.InsertRetention(new(k * 2 - 2, 1.3f / k));
                                DelayBeat(32, () => line.Dispose());
                                CreateEntity(line);
                            });
                        }

                        GlobalResources.Effects.NeonLineShader shader;
                        RenderProduction production;
                        ScreenDrawing.BackGroundRendering.InsertProduction(production = new ScreenDrawing.Shaders.Filter(shader = Shaders.NeonLine, 0.5f));
                        DelayBeat(960 - (440 - 4), () =>
                        {
                            production.Dispose();
                        });

                        Color mission = Color.Blue * 0.22f, cur = Color.Transparent;
                        shader.Speed = 1.0f;
                        shader.DrawingColor = cur;
                        ForBeat(8, () =>
                        {
                            cur = Color.Lerp(cur, mission, 0.2f);
                            shader.DrawingColor = cur;
                        });
                    });
                    RegisterFunctionOnce("RotateLSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateRSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateMidSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(5));
                    });

                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.4f, BeatTime(2), false);
                        ScreenDrawing.CameraEffect.SizeShrink(2.7f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.4f, BeatTime(2), true);
                        ScreenDrawing.CameraEffect.SizeShrink(2.7f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate+", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 7 * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("LRotate", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 5 * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-5) * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1 * 0.2f;
                        });
                        DelayBeat(3.1f, () => { ScreenDrawing.ScreenAngle = 0; });
                    });

                    string[] rhythm = {
                                        "RotateLSoft", "", "", "",
                    "R", "", "+0", "",    "", "", "R", "",
                    "+0", "", "", "",    "R", "", "+0", "",
                    "", "", "R", "",    "+0", "", "", "",
                    "(R)(+01'1.2)", "", "", "",    "(+2'1.2)(+01)(RotateRSoft)", "", "", "",
                    "R", "", "+0", "",    "", "", "R", "",
                    "+0", "", "", "",    "R", "", "+0", "",
                    "", "", "R", "",    "+0", "", "", "",
                    "(R)(+01'1.2)", "", "", "",    "(+2'1.2)(+01)(RotateLSoft)", "", "", "",
                    "R", "", "+0", "",    "", "", "R", "",
                    "+0", "", "", "",    "R", "", "+0", "",
                    "", "", "R", "",    "+0", "", "", "",
                    "(R)(+01'1.2)", "", "", "",    "(+2'1.2)(+01)(RotateMidSoft)", "", "", "",
                    "R", "", "+0", "",    "", "", "R", "",
                    "+0", "", "", "",    "R", "", "+0", "",
                    "", "", "R", "",    "+0", "", "", "(ShrinkR)",
                    "(R)(+01)", "", "", "(ShrinkL)",    "(R)(+01)(RotateRSoft)", "", "", "",
                    "R1", "", "+01", "",    "", "", "R1", "",
                    "+01", "", "", "",    "R1", "", "+01", "",
                    "", "", "R1", "",    "+01", "", "", "",
                    "(R)(+01'1.2)", "", "", "",    "(+2'1.2)(+01)(RotateLSoft)", "", "", "",
                    "R1", "", "+01", "",    "", "", "R1", "",
                    "+01", "", "", "",    "R1", "", "+01", "",
                    "", "", "R1", "",    "+01", "", "", "",
                    "(R)(+01'1.2)", "", "", "",    "(+2'1.2)(+01)(RotateMidSoft)", "", "", "",
                    "R1", "", "+01", "",    "", "", "R1", "",
                    "+01", "", "", "",    "R1", "", "+01", "",
                    "", "", "R1", "",    "+01", "", "", "",
                    "R1", "", "", "",    "R1", "", "", "",
                    "+0(R1)(LRotate)", "", "", "",    "", "", "+0(R1)(RRotate)", "",
                    "", "", "", "",    "+0(R1)(LRotate)", "", "", "",
                    "", "", "+0(R1)(LRotate+)", "",    "DownCross", "", "", "",
                    "+0(R1)(MidRotate)", "", "", "",    "", "", "", "",
                };
                    float time = game.BeatTime(8 - 2);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                #region not made
                if (InBeat(440 - 8))
                {
                    RegisterFunctionOnce("MoveLine", () =>
                    {
                        Line left = new(CentreEasing.Combine(CentreEasing.SinWave(20, BeatTime(16), 0, 0), new Vector2(50, 0)), (s) => 90);
                        Line right = new(CentreEasing.Combine(CentreEasing.SinWave(20, BeatTime(16), 0, -180), new Vector2(640 - 50, 0)), (s) => 90);
                        Line[] lines = { left, right };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0.2f;
                            line.Depth = 0.2f;
                            line.DrawingColor = Color.Gold;
                            CreateEntity(line);
                            DelayBeat(128, () => line.AlphaDecrease(BeatTime(2)));
                        }
                    });
                    RegisterFunctionOnce("Black", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.75f, BeatTime(2));
                    });

                    string[] rhythm = {
                    "Black", "/", "/", "/",    "/", "/", "^$1", "/",
                    "/", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",

                    "MoveLine", "Black", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "",
                    "R", "/", "/", "/",    "", "/", "/", "/",

                };
                    float time = game.BeatTime(16);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(440 + 8))
                {
                    string[] rhythm = {
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "/", "/",

                    "", "/", "/", "/",    "/", "/", "", "/",
                    "/", "/", "/", "/",    "", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                };
                    float time = game.BeatTime(0);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(696 - 0.4f))
                {
                    //create lines
                    for (int i = 4; i < 60; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            Line up = new(CentreEasing.EaseOutQuad(new(320, 0), new(320, BoxStates.Up), BeatTime(8)), (s) => 0);
                            Line down = new(CentreEasing.EaseOutQuad(new(320, 480), new(320, BoxStates.Down), BeatTime(8)), (s) => 0);
                            Line[] lines = { up, down };
                            foreach (Line line in lines)
                            {
                                line.Alpha = 0.4f;
                                line.DrawingColor = Color.LightBlue;
                                line.InsertRetention(new(2, 0.56f));
                                line.InsertRetention(new(4, 0.3f));
                                DelayBeat(4, () =>
                                {
                                    line.AlphaDecrease(BeatTime(4));
                                });
                                CreateEntity(line);
                            }
                        });
                    }
                    RegisterFunctionOnce("MoveBox", () =>
                    {
                        ValueEasing.EaseBuilder height = new();
                        height.Insert(BeatTime(114), ValueEasing.Linear(160, 88, BeatTime(114)));
                        height.Run(s => SetBox(240, 150, s));
                        SetSoul(0);
                    });
                    RegisterFunction("LPike", () =>
                    {
                        SetBoxMission(1);
                        float x = Rand(-3, 3) * -20 + 320;
                        float y = BoxStates.Down + 33;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x - 4, y), -90, game.BeatTime(8)) { IsHidden = true });
                        CreateSpear(new Pike(new(x + 4, y), -90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                    });
                    RegisterFunction("RPike", () =>
                    {
                        SetBoxMission(0);
                        float x = Rand(-3, 3) * 20 + 320;
                        float y = BoxStates.Up - 33;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x - 4, y), 90, game.BeatTime(8)) { IsHidden = true });
                        CreateSpear(new Pike(new(x + 4, y), 90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                    });

                    string[] rhythm = {
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "MoveBox",
                    "LPike", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "", "/", "/", "/",   "", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                };
                    float time = game.BeatTime(0);
                    SetPlayerMission(1);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(832 - 0.26f))
                {
                    float curAlpha = 0.05f;
                    float alphaMax = 0;
                    RegisterFunctionOnce("HPike", () =>
                    {
                        curAlpha = 0.2f;
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + Rand(1, 5) * 16 - 3;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, game.BeatTime(8)) { IsHidden = true });
                        Line line = new(new Vector2(BoxStates.Left, y), new Vector2(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(BeatTime(2));
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("CreateLine", () =>
                    {
                        float rotation = MathUtil.Direction(new(320, 240));
                        Line bottomLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(160, 360)), (s) => rotation);
                        Line bottomRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(480, 360)), (s) => -rotation);
                        Line topLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(160, 120)), (s) => -rotation);
                        Line topRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(480, 120)), (s) => rotation);
                        Line[] lines = { bottomLeft, bottomRight, topLeft, topRight };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.Alpha = curAlpha;
                            line.DrawingColor = Color.Gold;
                        }
                        ForBeat(125, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = curAlpha;
                            }
                            curAlpha = curAlpha * 0.8f + 0.05f * 0.2f;
                            alphaMax += 0.002f;
                            curAlpha = MathF.Min(alphaMax, curAlpha);
                        });
                        DelayBeat(126, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(2));
                            }
                        });
                    });
                    RegisterFunctionOnce("LRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            float limit = 0.06f;
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 5 * 0.2f;
                                ScreenDrawing.ScreenScale += limit * 0.2f;
                                limit *= 0.8f;
                            });
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            float limit = 0.06f;
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-5) * 0.2f;
                                ScreenDrawing.ScreenScale += limit * 0.2f;
                                limit *= 0.8f;
                            });
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            DelayBeat(1, () =>
                            {
                                ScreenDrawing.SceneOut(Color.Silver, BeatTime(3.5f));
                            });
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle *= 0.8f;
                            });
                            DelayBeat(3.4f, () =>
                            {
                                ScreenDrawing.ScreenAngle = 0;
                                ScreenDrawing.ScreenScale = 1;
                                rainer.Intensity = 0;
                                rainer.Rotation = 12;
                                rainer.Speed = 7;
                                ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = 0;

                                InstantSetBox(600, 84, 84);
                                SetSoul(1);
                                InstantTP(BoxStates.Centre);

                                ScreenDrawing.UIColor = Color.Transparent;
                                ScreenDrawing.ThemeColor = Color.Transparent;
                                ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                                ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                                Heart.Shields.AddShield(Heart.Shields.BShield);
                            });
                        });
                    });
                    RegisterFunctionOnce("HPikeM", () =>
                    {
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + Rand(1, 5) * 16 - 3;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                        Line line = new(new Vector2(BoxStates.Left, y), new Vector2(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(BeatTime(2));
                        CreateEntity(line);
                    });
                    float time = game.BeatTime(0);
                    string[] rhythm = {
                    "HPike(CreateLine)", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPikeM(LRotate)", "/", "/", "/",   "/", "/", "HPikeM(RRotate)" , "/",
                    "/", "/", "/", "/",   "HPikeM(LRotate)", "/", "/" , "/",
                    "/", "/", "HPikeM(RRotate)", "/",   "/", "/", "/" , "/",
                    "HPikeM(MidRotate)", "/", "/", "/",   "/", "/", "/" , "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                    CentreEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(128), CentreEasing.YSinWave(6, BeatTime(32), 0));
                    builder.Run((s) => { ScreenDrawing.ScreenPositionDelta = s; });
                }
                #endregion
                if (InBeat(960))
                {
                    float time = game.BeatTime(0);
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    RegisterFunctionOnce("Texture1", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash0, BeatTime(3 * 4)) { FadeSpeed = 0.32f });
                    });
                    RegisterFunctionOnce("Texture2", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash1, BeatTime(3 * 4)));
                    });
                    RegisterFunctionOnce("Texture3", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash2, BeatTime(9 * 4)));

                        List<TextAttribute> attributes = new();
                        attributes.Add(new TextSpeedAttribute(55));
                        attributes.Add(new TextSizeAttribute(0.75f));
                        attributes.Add(new TextFadeoutAttribute(BeatTime(36), BeatTime(4)));
                        for (int i = 1; i <= 5; i++)
                            attributes.Add(new TextTimeThreshold(BeatTime(i * 4)));
                        TextPrinter printer;
                        CreateEntity(printer = new TextPrinter("$$$Is $something $still $nostalgic $in $memory?", new Vector2(60, 340), attributes.ToArray()));
                        DelayBeat(41, () => printer.Dispose());
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                    RegisterFunctionOnce("MoveCentre", () =>
                    {
                        BoxStates.BoxMovingScale = 0.04f;
                        SetGreenBox();

                        ScreenDrawing.UIColor = Color.White;
                        ScreenDrawing.ThemeColor = Color.White;
                        ScreenDrawing.HPBar.HPExistColor = Color.White;
                        ScreenDrawing.HPBar.HPLoseColor = Color.White;

                        ForBeat120(32, () => { TP(BoxStates.Centre); ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.87f + 0.995f * 0.13f; });
                    });
                    RegisterFunctionOnce("Easing", () =>
                    {
                        ValueEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(73), ValueEasing.LerpTo(0, 0.05f, ValueEasing.SinWave(5, BeatTime(48), 0)));
                        easeBuilder.Run((x) => ScreenDrawing.ScreenAngle = x);
                        DelayBeat(74, () => ScreenDrawing.ScreenAngle = 0);
                    });

                    RegisterFunctionOnce("ScaleL", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ForBeat(3f, () =>
                            {
                                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.15f * 0.1f;
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                            });
                        });
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White * 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ScaleR", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ForBeat(3f, () =>
                            {
                                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.3f * 0.1f;
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + (-5) * 0.1f;
                            });
                        });
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.SceneOut(Color.Silver * 0.99f, BeatTime(4.5f));
                        });
                    });
                    RegisterFunctionOnce("Restore", () =>
                    {
                        PlaySound(Sounds.heal);
                        Regenerate(1);
                        ScreenDrawing.BoundColor = Color.Red * 0.56f;
                        ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = 110;
                        Color currentLose = Color.White, currentExist = Color.White;
                        ForBeat(3f, () =>
                        {
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.03f;

                            currentLose = Color.Lerp(currentLose, Color.Red, 0.1f);
                            currentExist = Color.Lerp(currentExist, Color.Yellow, 0.1f);

                            ScreenDrawing.HPBar.HPLoseColor = currentLose;
                            ScreenDrawing.HPBar.HPExistColor = currentExist;
                        });
                        rainer.Intensity = 0.8f;
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
                    string[] rhythm = {
                    "Texture1(RGBSplit)(Fade2)(VoidArrow)", "", "", "",   "", "", "", "",   "", "", "", "Fade0",
                    "", "", "", "",   "", "", "", "",   "", "", "", "",
                    "Texture2(Fade2)", "", "", "",   "", "", "", "",   "", "", "", "Fade",
                    "", "", "", "",   "", "", "", "",   "", "", "", "",
                    "Texture3(Fade2)", "", "", "",   "", "", "", "",   "", "", "", "Fade2",
                    "", "", "", "",   "", "", "", "",   "", "", "", "Fade2",
                    "", "", "", "",   "", "", "", "",   "", "", "", "Fade",
                    "", "", "", "(MoveCentre)",   "", "", "", "",   "", "", "", "Fade3",

                    "Easing", "", "", "",   "D", "", "", "",   "D", "", "", "Fade3",
                    "D1", "", "", "",   "D1", "", "", "",   "D1", "", "", "Fade3",
                    "D", "", "", "",   "D", "", "", "",   "N3", "", "", "Fade3",
                    "D1", "", "", "",   "D1", "", "", "",   "N31", "", "", "Fade3",
                    "#44#$3", "", "", "",   "$3", "", "", "",   "$3", "", "", "Fade3",
                    "#30#$31", "", "", "",   "$31", "", "", "",   "$31", "", "", "Fade3",
                    "$3", "", "", "",   "$3", "", "", "",   "$3", "", "$31", "+0(Fade3)",
                    "+01", "", "", "ScaleL",    "", "", "", "ScaleR",    "", "", "", "Restore", ""
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1144))
                {
                    ForBeat(4, 500, () =>
                    {
                        CreateEntity(new Particle(Color.White, new Vector2(Rand(4f, 6f), Rand(-0.5f, 0.5f)) * 2, Rand(19f, 25f), new(-121, Rand(0, 480)), Sprites.arrow[0, 1, 0]) { DarkingSpeed = 9f, Rotation = GetRadian(180f) });
                        CreateEntity(new Particle(Color.White, new Vector2(-Rand(4f, 6f), Rand(-0.5f, 0.5f)) * 2, Rand(19f, 25f), new(640 + 121, Rand(0, 480)), Sprites.arrow[1, 1, 0]) { DarkingSpeed = 9f });
                    });

                    sampleIntensity = 0.05f;
                    splitIntensity = 1f;
                    RegisterFunctionOnce("Shader", () =>
                    {
                        var v1 = Shaders.StepSample;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        v1.Intensity = sampleIntensity;
                        ForBeat(144, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.9f + 0.02f * 0.1f;
                            splitIntensity = splitIntensity * 0.9f + 1.00f * 0.1f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                        });
                        ForBeat(144, 48, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.95f + 0.04f * 0.05f;
                            splitIntensity = splitIntensity * 0.95f + 1.50f * 0.05f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.18f;
                        splitIntensity = 3f;
                        ScreenDrawing.CameraEffect.SizeExpand(1.4f, BeatTime(2));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    float time = BeatTime(8);

                    int randVal = 0;
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'1.5)(^${randVal + 2}1'1.5)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'1.5)(^${randVal + 3}1'1.5)";
                    string arr30 = $"(<${randVal + 2}0)(>${randVal + 0}1)",
                           arr31 = $"(>${randVal + 2}0)(<${randVal + 0}1)",
                           arr3s = $"(^${randVal + 2}0'1.5)(^${randVal + 0}1'1.5)";
                    string arr40 = $"(<${randVal + 3}0)(>${randVal + 1}1)",
                           arr41 = $"(>${randVal + 3}0)(<${randVal + 1}1)",
                           arr4s = $"(^${randVal + 3}0'1.5)(^${randVal + 1}1'1.5)";

                    string[] rhythm = {
                    //1
                    "(Shader)(Blur)(LRotate)", "", "", "",   "(Blur)(RRotate)", "", "", "",   "(R)(+01)(Blur)(LRotate)", "", "+0", "",
                    //2
                    "(R)(+01)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)ConvulseL", "",
                    $"(${randVal}10)(${randVal + 2}00)ConvulseR", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)ConvulseL", "",
                    $"(${randVal}10)(${randVal + 2}00)ConvulseR", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                    $"(BlurDeep)" + arr30 + arr3s, arr31, arr30 + arr3s, arr31,   arr30 + arr3s, "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1192))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(17, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(34, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.28f;
                        splitIntensity = 4f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ArrowDelay", () =>
                    {
                        int randVal = Rand(0, 1) * 2 + 1;
                        string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                               arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                               arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                        string[] rhythm = {
                        arr1s + arr10, arr11, arr1s + arr10, arr11, arr1s + arr10
                        };
                        string[] rhythm2 = {
                        "(R)(+01)", "", "+0", "",   "(R)(+01)", "", "+01", "",    "(R)(+01)", "", "+0", ""
                        };
                        List<Arrow> alphaControls = new();
                        float time = BeatTime(12);
                        for (int i = 0; i < rhythm.Length; i++)
                        {
                            GameObject[] arrows = NormalizedObjects(time, 5.6f, rhythm[i]);
                            foreach (Arrow arrow in arrows)
                            {
                                DelayBeat(11.2f, () => { arrow.Delay(BeatTime(48)); });
                                CreateEntity(arrow);
                                alphaControls.Add(arrow);
                            }
                            time += game.BeatTime(1);
                        }
                        time = BeatTime(12);
                        for (int i = 0; i < rhythm2.Length; i++)
                        {
                            GameObject[] arrows = NormalizedObjects(time, 6f, rhythm2[i]);
                            foreach (Arrow arrow in arrows)
                            {
                                DelayBeat(11.2f, () => { arrow.Delay(BeatTime(12)); });
                                CreateEntity(arrow);
                                alphaControls.Add(arrow);
                            }
                            time += game.BeatTime(1);
                        }
                        foreach (Arrow arrow in alphaControls)
                        {
                            arrow.Alpha = 0.56f;
                        }
                        float alphaVal = 0.56f;
                        ForBeat(12, 12, () =>
                        {
                            alphaVal = alphaVal * 0.85f + 1 * 0.15f;
                            foreach (Arrow arrow in alphaControls) arrow.Alpha = alphaVal;
                        });
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string[] rhythm = {
                    //1
                    "(Blur)(R)(+01)(LRotate)", "", "+0", "",   "(Blur)(R)(+01)(RRotate)", "", "+01", "",   "(R)(+01)(Blur)(LRotate)", "", "+0", "",
                    //2
                    "(R)(+01)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(ConvulseL)(${randVal}00)(${randVal + 2}10)", "",
                    $"(${randVal}10)(${randVal + 2}00)(ConvulseR)", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}10)(${randVal + 2}00)(ConvulseR)", "",
                    //3 
                    $"(Blur)(${randVal}01)(${randVal + 2}11)(ArrowDelay)(ConvulseL)", "", "", "",    "(Blur)+0(R1)", "", "", "",    "(Blur)+0(R1)", "", "$0", "$1",
                    //4 
                    "($2)Shrink(BlurDeep)",    "", "", "", "",    "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1240))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.28f;
                        splitIntensity = 4f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'2)(^${randVal + 3}1'2)";
                    string arr40 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr41 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr4s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";

                    string[] rhythm = {
                    //1
                    "(Blur)(LRotate)", "", "", "",   "(Blur)(RRotate)", "", "", "",   "(Blur)(LRotate)", "", "", "",
                    //2
                    "R(+01)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}10)(${randVal + 2}00)(ConvulseR)", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}10)(${randVal + 2}00)(ConvulseR)", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                     "(BlurDeep)", "", "", "",   "", "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1288))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.32f;
                        splitIntensity = 5f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    RegisterFunctionOnce("ExpandL", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("ExpandR", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(12.2f), true);
                    });
                    RegisterFunctionOnce("FinalRed", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(240, 600, 84);
                        Regenerate(1);
                        PlaySound(Sounds.heal);
                    });

                    float time = BeatTime(8);

                    int randVal = Rand(0, 3);
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                    string arr20 = $"(<${randVal}1)(>${randVal + 2}0)",
                           arr21 = $"(>${randVal}1)(<${randVal + 2}0)",
                           arr2s = $"(^${randVal}1'2)(^${randVal + 2}0'2)";

                    string[] rhythm = {
                    //1
                    "(Blur)(R)(+01)(ConvulseL)", "", "", "",   "(Blur)(ConvulseL)", "", "+01", "",   "+01(Blur)(ConvulseL)", "", "+01", "",
                    //2 
                    "(ConvulseR)(Blur)(R)(+01)", "", "", "",    "(ConvulseR)(Blur)(R)(+01)", "", "", "",    "(ConvulseR)(Blur)(R)(+01)", "", "", "",
                    //3  
                    //4 
                    "(BlurDeep)(ExpandL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, "", "",
                    "(BlurDeep)(ExpandR)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, "FinalRed", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1336))
                {
                    RegisterFunctionOnce("Box1", () =>
                    {
                        SetBox(270, 600, 320);
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Circle", () =>
                    {
                        int count = 9;
                        float way = Rand(0, 1) * 2 - 1;
                        float rot = Rand(0, 359);
                        PlaySound(Sounds.spearAppear);
                        Fortimes(count, () =>
                        {
                            CreateSpear(new CircleSpear(Heart.Centre, 3.7f * way, 1.88f, 187f, rot += 360f / count, 0.025f));
                        });
                    });
                    RegisterFunctionOnce("Swarm", () =>
                    {
                        int count = 8;
                        float rotation = Rand(0, 359);
                        PlaySound(Sounds.spearAppear);
                        Fortimes(count, () =>
                        {
                            rotation += 360f / count;
                            CreateSpear(new SwarmSpear(Heart.Centre, 5.0f, 164, rotation, BeatTime(6)));
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(15, rotation), 5.0f, 164, rotation + 45, BeatTime(6)));
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(15, rotation), 5.0f, 164, rotation - 45, BeatTime(6)));
                        });
                        DelayBeat(6, () =>
                        {
                            PlaySound(Sounds.spearShoot);
                        });
                    });
                    RegisterFunctionOnce("Align", () =>
                    {
                        bool way = true;
                        float width = 48;
                        float delta = Rand(-width / 2, width / 2);
                        float timeDel = 0.0f;
                        BoxStates.BoxMovingScale = 0.05f;
                        SetBox(270, 600, 280);
                        float y = BoxStates.Up;
                        for (float i = 20; i <= 620; i += width)
                        {
                            way = !way;
                            float x = delta + i;
                            float v = timeDel;
                            DelayBeat(v, () =>
                            {
                                CreateSpear(new Pike(new(x, y), 90, BeatTime(12 - v)) { IsShootMute = true, IsSpawnMute = true });
                            });
                            timeDel += 0.25f;
                        }
                        ForBeat120(12, () =>
                        {
                            Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                        });
                        DelayBeat(8, () =>
                        {
                            PlaySound(Sounds.spearShoot);
                        });
                        ScreenDrawing.CameraEffect.Rotate(45, BeatTime(8));
                    });
                    RegisterFunctionOnce("Restore", () =>
                    {
                        sampleIntensity = 0.5f;
                        splitIntensity = 7;
                        Heart.InstantSetRotation(0);
                        InstantSetBox(Heart.Centre, 16, 16);
                        var v1 = Shaders.StepSample;
                        PlaySound(Sounds.switchScene);
                        PlaySound(Sounds.switchScene);
                        ForBeat(120, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.96f + 0.06f * 0.04f;
                            splitIntensity = splitIntensity * 0.96f + 1.80f * 0.04f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                            ScreenDrawing.ScreenAngle *= 0.7f;
                        });
                    });
                    RegisterFunctionOnce("MoveCentre", () =>
                    {
                        BoxStates.BoxMovingScale = 0.11f;
                        SetBox(240, 84, 84);
                        ForBeat(12, () => InstantTP(BoxStates.Centre));
                    });
                    RegisterFunctionOnce("Enlarge", () =>
                    {
                        SetGreenBox();
                        SetSoul(1);
                    });

                    float time = BeatTime(8);
                    string[] rhythm = {
                    "Box1(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Swarm", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Swarm", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Align", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Restore", "", "", "",    "MoveCentre", "", "", "",    "Enlarge", "", "", "",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1432))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.12f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.31f;
                        splitIntensity = 5f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2.5)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'2.5)(^${randVal + 3}1'2.5)";
                    string arr30 = $"(<${randVal + 2}0)(>${randVal + 0}1)",
                           arr31 = $"(>${randVal + 2}0)(<${randVal + 0}1)",
                           arr3s = $"(^${randVal + 2}0'2.5)(^${randVal + 0}1'2.5)";
                    string arr40 = $"(<${randVal + 3}0)(>${randVal + 1}1)",
                           arr41 = $"(>${randVal + 3}0)(<${randVal + 1}1)",
                           arr4s = $"(^${randVal + 3}0'2.5)(^${randVal + 1}1'2.5)";

                    string[] rhythm = {
                    //1
                    "(Blur)(LRotate)(D)(+01)", "", "+0", "+0",   "(Blur)(RRotate)(D)(+01)", "", "+01", "+01",   "(Blur)(LRotate)(D)(+01)", "", "+0", "+0",
                    //2
                    "(D)(+01)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}10)(${randVal + 2}00)(ConvulseR)", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}10)(${randVal + 2}00)(ConvulseR)", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                    $"(BlurDeep)" + arr30 + arr3s, arr31, arr30 + arr3s, arr31,   arr30 + arr3s, "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1480))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.14f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.35f;
                        splitIntensity = 6f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.5f), true);
                    });

                    RegisterFunctionOnce("ExpandL", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(26, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("ExpandR", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(26, BeatTime(12.2f), true);
                    });
                    RegisterFunctionOnce("FinalRed", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(240, 600, 84);
                        Regenerate(1);
                    });

                    float time = BeatTime(8);

                    int randVal = Rand(0, 3);
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'3)(^${randVal + 2}1'3)";
                    string arr20 = $"(<${randVal}1)(>${randVal + 2}0)",
                           arr21 = $"(>${randVal}1)(<${randVal + 2}0)",
                           arr2s = $"(^${randVal}1'3)(^${randVal + 2}0'3)";

                    string[] rhythm = {
                    //1
                    "(Blur)(R)(+01)(ConvulseL)", "", "", "",   "(Blur)(ConvulseL)", "", "(+0)", "",   "+0(Blur)(ConvulseL)", "", "+0", "",
                    //2 
                    "(ConvulseR)(Blur)(R)(+01)", "", "", "",    "(ConvulseR)(Blur)(R)(+01)", "", "", "",    "(ConvulseR)(Blur)(R)(+01)", "", "", "",
                    //3  
                    //4 
                    "(BlurDeep)(ExpandL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, "", "",
                    "(BlurDeep)(ExpandR)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, "FinalRed", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
            }

            public void Normal()
            {
                Easy();
            }

            private bool notRegistered = true;

            private void CreateLine(float yCentre, float length)
            {
                CentreEasing.EaseBuilder builder = new();
                float time = BeatTime(8);
                builder.Insert(time, CentreEasing.Alternate(
                        3f,
                        CentreEasing.LerpTo(new(320, yCentre), 0.12f, CentreEasing.EaseOutQuad(new(320 - 31, yCentre), new(320, yCentre), time)),
                        CentreEasing.LerpTo(new(320, yCentre), 0.12f, CentreEasing.EaseOutQuad(new(320 + 31, yCentre), new(320, yCentre), time))
                    ));
                builder.Insert(time, CentreEasing.Accelerating(new(-5, -7), new(0, 0.7f)));
                Line follow, mid, line = new(builder.GetResult(), (s) => s.AppearTime > time ? (90 + (s.AppearTime - time) * 3.3f) : 90, (s) => length) { Depth = 0.5f };
                line.Width = 4;
                line.DrawingColor = Color.Red;
                follow = new((s) => line.Centre, (s) => 90);
                DelayBeat(8, () =>
                {
                    line.AlphaDecrease(time);
                    line.DrawingColor = Color.Red;

                    Line mid2 = new((s) => line.Centre, (s) => line.Rotation, (s) => 1000);
                    CreateEntity(mid2);
                    mid2.AlphaDecrease(time / 3);
                    mid2.DrawingColor = Color.Orange;

                    follow.Alpha += 0.2f;
                    follow.AlphaDecrease(time / 3, 0.67f);
                });
                ForBeat120(0, 16, () =>
                    line.DrawingColor = Color.Lerp(line.DrawingColor, Color.White, 0.12f));

                mid = new(new Vector2(320, 0), new Vector2(320, 480));
                CreateEntity(line);
                CreateEntity(mid);
                CreateEntity(follow);
                mid.AlphaDecrease(time / 3);
                follow.AlphaDecrease(time / 3, 0.5f);
                follow.DrawingColor = Color.Orange;
                follow.Depth = 0.48f;
                mid.DrawingColor = Color.Orange;

                DelayBeat(16, () =>
                {
                    follow.Dispose();
                    mid.Dispose();
                    line.Dispose();
                });
            }

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

            private ScreenDrawing.Shaders.RGBSplitting splitter = null;
            private float sampleIntensity = 0.05f;
            private float splitIntensity = 1f;

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
            public void Extreme()
            {
                if (Gametime < -2) return;

                // intro 
                // var v = GetAll<Entity>();
                if (notRegistered)
                {
                    notRegistered = false;
                    RegisterFunction("ToRed", () =>
                    {
                        Player.Heart origin = Heart;
                        Player.Heart heart = CreateHeart(500, 160, 160);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(300, 160, 160);
                        SetSoul(0);

                        SetPlayerBoxMission(origin);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(-300, 84, 84);

                        ForBeat(4, () => origin.InstantTP(origin.controlingBox.Centre));
                        DelayBeat(4, () => origin.Dispose());
                        DelayBeat(4.2f, () => SetPlayerBoxMission(heart));
                    });
                    RegisterFunction("Spear", () =>
                    {
                        CreateSpear(new NormalSpear(Heart.Centre + GetVector2(130, Rand(0, 359))) { WaitingTime = BeatTime(6) });
                    });
                    RegisterFunction("Split", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.8f, BeatTime(2.4f));
                        DelayBeat(1.5f, () =>
                        {
                            rainer.Speed = 6f;
                        });
                        DelayBeat(2.4f, () =>
                        {
                            rainer.Intensity = 0.5f;
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = 140;
                        });

                        Player.Heart origin = Heart;
                        Player.Heart heart = CreateHeart(320, 84, 84);

                        Vector2 centre1 = new(320, 240), centre2 = new(320, 240);
                        Vector2 mission1 = new(260, 190), mission2 = new(380, 290);
                        bool type1 = false, type2 = false;

                        SetPlayerBoxMission(origin);
                        SetBox(mission1, 84, 84);
                        BoxStates.BoxMovingScale = 0;
                        InstantTP(mission1);
                        SetPlayerBoxMission(heart);
                        SetBox(mission2, 84, 84);
                        BoxStates.BoxMovingScale = 0;
                        InstantTP(mission2);

                        Vector2 start = new(320, 240);
                        float time1 = 0;

                        float theta = GetAngle(MathF.Atan2(60, 50));

                        float GetPercent(float v)
                        {
                            return MathF.Pow(v, 0.66f);
                        }
                        ForBeat(3, () =>
                        {
                            time1 += 1 / game.BeatTime(3);
                            SetPlayerBoxMission(origin);
                            type1 = !type1;
                            BoxStates.Centre = type1 ? centre1 : mission1;
                            centre1 = Vector2.Lerp(start, mission1, GetPercent(time1));

                            Line line1, line2;
                            CreateEntity(line1 = new Line(centre1 + new Vector2(33, 33), (s) => { return -theta; }) { DrawingColor = new Color(128, 255, 128, 128) * 0.1f });
                            CreateEntity(line2 = new Line(centre2 + new Vector2(44, -44) * (1.7f - 0.7f * GetPercent(time1)), (s) => { return 90 - theta; }) { DrawingColor = new Color(128, 255, 128, 128) * 0.2f });
                            line1.AlphaDecrease(game.BeatTime(1.8f));
                            line2.AlphaDecrease(game.BeatTime(1.8f));
                        });
                        ForBeat(3, () =>
                        {
                            SetPlayerBoxMission(heart);
                            type2 = !type2;
                            BoxStates.Centre = type2 ? centre2 : mission2;
                            centre2 = Vector2.Lerp(start, mission2, GetPercent(time1));
                            Line line1, line2;
                            CreateEntity(line1 = new Line(centre2 - new Vector2(33, 33), (s) => { return -theta; }) { DrawingColor = new Color(128, 255, 128, 128) * 0.1f });
                            CreateEntity(line2 = new Line(centre2 + new Vector2(-44, 44) * (1.7f - 0.7f * GetPercent(time1)), (s) => { return 90 - theta; }) { DrawingColor = new Color(128, 255, 128, 128) * 0.2f });
                            line1.AlphaDecrease(game.BeatTime(1.8f));
                            line2.AlphaDecrease(game.BeatTime(1.8f));
                        });
                        DelayBeat(3.1f, () =>
                        {
                            SetPlayerBoxMission(origin);
                            BoxStates.Centre = mission1;
                            InstantTP(BoxStates.Centre);
                            Heart.Shields.RemoveShield(Heart.Shields.RShield);

                            SetPlayerBoxMission(heart);
                            BoxStates.Centre = mission2;
                            InstantTP(BoxStates.Centre);
                            Heart.Shields.RemoveShield(Heart.Shields.BShield);

                            float time = 0;
                            ForBeat120(126, () =>
                            {
                                time += 0.5f;
                                SetPlayerBoxMission(origin);
                                BoxStates.Centre = new Vector2(0, 7 * MathF.Sin(time / game.BeatTime(16))) + mission1;
                                TP(BoxStates.Centre);
                                SetPlayerBoxMission(heart);
                                BoxStates.Centre = new Vector2(0, -7 * MathF.Sin(time / game.BeatTime(16))) + mission2;
                                TP(BoxStates.Centre);
                            });
                            ForBeat120(136, 114, () =>
                            {
                                time += 0.5f;
                                SetPlayerBoxMission(origin);
                                BoxStates.Centre = GetVector2(8 * MathF.Sin(time / game.BeatTime(16)), 45) + mission1;
                                InstantTP(BoxStates.Centre);
                                SetPlayerBoxMission(heart);
                                BoxStates.Centre = GetVector2(-8 * MathF.Sin(time / game.BeatTime(16)), 45) + mission2;
                                InstantTP(BoxStates.Centre);
                            });
                            DelayBeat(127, () =>
                            {
                                PlaySound(Sounds.switchScene);
                                PlaySound(Sounds.switchScene);
                                float theta = GetAngle(MathF.Atan2(60, 50));
                                float distance = (BoxStates.Centre - new Vector2(320, 240)).Length();
                                float k = 0;
                                ForBeat(8, () =>
                                {
                                    time = 0;
                                    k = k * 0.9f + 170 * 0.1f;
                                    SetPlayerBoxMission(origin);
                                    mission1 = BoxStates.Centre = new Vector2(320, 240) + GetVector2(distance, theta + 180 + k);
                                    InstantTP(BoxStates.Centre);
                                    SetPlayerBoxMission(heart);
                                    mission2 = BoxStates.Centre = new Vector2(320, 240) + GetVector2(distance, theta + k);
                                    InstantTP(BoxStates.Centre);
                                    rainer.Rotation = rainer.Rotation * 0.9f + (-10f) * 0.1f;
                                });
                            });
                        });
                    });
                    RegisterFunction("DelayArrow", () =>
                    {
                        Arrow arr = MakeArrow(BeatTime(4), 1, 7, 1, 0);
                        DelayBeat(3f, () => { arr.Delay(BeatTime(4)); });
                        CreateEntity(arr);
                    });
                    RegisterFunction("FixRed", () =>
                    {
                        float y1, y2, x1, x2;
                        SetBoxMission(0);
                        x2 = BoxStates.Right;
                        y2 = BoxStates.Down;
                        SetBoxMission(1);
                        x1 = BoxStates.Left;
                        y1 = BoxStates.Up;
                        SetBoxMission(0);
                        BoxStates.BoxMovingScale = 0.25f;
                        SetBox(BoxStates.Left, x2, y1, y2);
                        SetBoxMission(1);
                        BoxStates.BoxMovingScale = 0.25f;
                        SetBox(x1, BoxStates.Right, y1, y2);

                        DelayBeat(2, () =>
                        {
                            SetBoxMission(0);
                            BoxStates.BoxMovingScale = 0.25f;
                            SetBox(320 + 1, x2 + 15, y1, y2);
                            SetBoxMission(1);
                            BoxStates.BoxMovingScale = 0.25f;
                            SetBox(x1 - 15, 320 - 1, y1, y2);
                            SetPlayerMission(0);
                            Heart.RotateTo(180);
                            DelayBeat(2, () =>
                            {
                                SetPlayerMission(0);
                                Heart.InstantSetRotation(180);
                                SetSoul(0);
                                SetPlayerMission(1);
                                SetSoul(0);
                                PlaySound(Sounds.switchScene);
                                PlaySound(Sounds.switchScene);
                            });
                        });
                    });

                    RegisterFunction("LPike", () =>
                    {
                        SetBoxMission(1);
                        float x = Rand(0, 5) * -20 + 320 - 8;
                        float y = BoxStates.Down + 33;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), -90, game.BeatTime(8)) { IsHidden = true });
                        CreateSpear(new Pike(new(x - 7, y), -90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                    });
                    RegisterFunction("RPike", () =>
                    {
                        SetBoxMission(0);
                        float x = Rand(0, 5) * 20 + 320 + 8;
                        float y = BoxStates.Up - 33;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), 90, game.BeatTime(8)) { IsHidden = true });
                        CreateSpear(new Pike(new(x + 7, y), 90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                    });
                    RegisterFunction("HPikeM", () =>
                    {
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + Rand(1, 5) * 16 - 3;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, game.BeatTime(8)) { IsHidden = true, IsSpawnMute = true });
                        Line line = new(new Vector2(BoxStates.Left, y), new Vector2(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(BeatTime(2));
                        CreateEntity(line);
                    });

                    RegisterFunction("MoveBox", () =>
                    {
                        ForBeat120(8, 29 * 4, () =>
                        {
                            SetBoxMission(0);
                            BoxStates.Up += 0.034f;
                            BoxStates.Down -= 0.034f;
                            SetBoxMission(1);
                            BoxStates.Up += 0.034f;
                            BoxStates.Down -= 0.034f;
                        });
                    });
                    RegisterFunction("Merge", () =>
                    {
                        float y1, y2, x1, x2;
                        SetBoxMission(0);
                        x2 = BoxStates.Right;
                        y2 = BoxStates.Down;
                        SetBoxMission(1);
                        x1 = BoxStates.Left;
                        y1 = BoxStates.Up;

                        float yCentre = (y1 + y2) / 2f;
                        float width = x2 - x1, height = y2 - y1;
                        Vector2 centre = new(320, yCentre);
                        CreateLine(yCentre, height);

                        SetBoxMission(0);
                        InstantSetBox(centre, width, height);
                        SetBoxMission(1);
                        InstantSetBox(centre, width, height);

                        PlaySound(Sounds.pierce);
                        DelayBeat(8, () =>
                        {
                            PlaySound(Sounds.damaged);

                            SetPlayerMission(1);
                            Player.Heart mission = Heart;
                            SetPlayerMission(0);
                            Heart.RotateTo(0);
                            Heart.Merge(mission);

                            CentreEasing.EaseBuilder builder = new();
                            builder.Insert(BeatTime(3), CentreEasing.Alternate(1,
                                CentreEasing.Linear(new(-9, 0), new(0, 0), BeatTime(3)),
                                CentreEasing.Linear(new(9, 0), new(0, 0), BeatTime(3))
                            ));
                            builder.Run((s) =>
                            {
                                ScreenDrawing.ScreenPositionDelta = s;
                            });
                            DelayBeat(3.2f, () =>
                            {
                                ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            });
                        });
                    });
                }

                if (InBeat(2))
                {
                    RegisterFunctionOnce("UShine", () =>
                    {
                        Line line = new(CentreEasing.Stable(new(320, 0)), (s) => new(320, BoxStates.Up));
                        line.DrawingColor = Color.Gold;
                        line.Alpha = 0.5f;
                        line.AlphaDecrease(BeatTime(4));
                        CreateEntity(line);

                        CentreEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(320, 270), BeatTime(4)));
                        easeBuilder.Run((x) => { BoxStates.Centre = x; Heart.InstantTP(x); });
                    });
                    RegisterFunctionOnce("UShine2", () =>
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            int t = i;
                            Line line = new(CentreEasing.Stable(new(320 + t * 10, 0)), (s) => new(320 + t * 5, BoxStates.Up));
                            line.DrawingColor = Color.Gold;
                            line.Alpha = 0.5f;
                            line.AlphaDecrease(BeatTime(4));
                            CreateEntity(line);
                        }

                        CentreEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(4), CentreEasing.EaseOutQuad(new(320, BoxStates.Centre.Y), new(320, BoxStates.Centre.Y - 30 / 3f), BeatTime(4)));
                        easeBuilder.Run((x) => { BoxStates.Centre = x; Heart.InstantTP(x); });
                    });
                    RegisterFunctionOnce("RotateL", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-5) * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateR", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + 5.2f * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateMid", () =>
                    {
                        ForBeat(0.1f, 4, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + (-0.3f) * 0.15f;
                        });
                    });
                    RegisterFunctionOnce("RotateLSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateRSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateMidSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(5));
                    });

                    RegisterFunctionOnce("SideLine", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float xPos = sign * 320 + 320;
                            for (int i = 1; i <= 3; i++)
                            {
                                Line line = new(new Vector2(xPos - i * sign * 12, 0), new Vector2(xPos - i * sign * 12, 480));
                                line.DrawingColor = Color.Yellow;
                                line.Alpha = i / 12f + 0.16f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(3 + i));
                            }
                        }
                    });
                    RegisterFunctionOnce("SpearLine", () =>
                    {
                        Vector2[] origins = {
                        new(120, 100),
                        new(132, 100),
                        new(132, 340),
                        new(148, 334),
                        new(126, 398),
                        new(104, 334),
                        new(120, 340)
                        };
                        Vector2 centre = new(126, 240);
                        Vector2[] deltas = new Vector2[origins.Length];
                        for (int i = 0; i < origins.Length; i++)
                        {
                            deltas[i] = origins[i] - centre;
                        }

                        float rotation = 0, time = 0;
                        Vector2[] positions = new Vector2[origins.Length];
                        ForBeat120(4, () =>
                        {
                            time += 0.5f;
                            rotation -= time * time * 0.003f;
                            for (int i = 0; i < origins.Length; i++)
                            {
                                positions[i] = centre + Rotate(deltas[i], rotation);
                            }
                        });
                        for (int i = 0; i < origins.Length; i++)
                        {
                            int t = i;
                            Line line = new((s) => positions[t], (s) => positions[(t + 1) % positions.Length]);
                            line.DrawingColor = Color.Silver;
                            line.Alpha = 0.3f;
                            CreateEntity(line);
                            DelayBeat(1, () =>
                            {
                                line.AlphaDecrease(BeatTime(3));
                            });
                        }
                    });
                    RegisterFunctionOnce("MirroredLine", () =>
                    {
                        Vector2[] origins = {
                        new(120, 100),
                        new(132, 100),
                        new(132, 340),
                        new(148, 334),
                        new(126, 398),
                        new(104, 334),
                        new(120, 340)
                        };
                        Vector2 centre = new(126, 240);
                        Vector2[] deltas = new Vector2[origins.Length];
                        for (int i = 0; i < origins.Length; i++)
                        {
                            deltas[i] = origins[i] - centre;
                        }

                        float rotation = 0, time = 0;
                        Vector2[] positions = new Vector2[origins.Length];
                        ForBeat120(4, () =>
                        {
                            time += 0.5f;
                            rotation -= time * time * 0.003f;
                            for (int i = 0; i < origins.Length; i++)
                            {
                                positions[i] = centre + Rotate(deltas[i], rotation);
                                positions[i].X = 640 - positions[i].X;
                            }
                        });
                        for (int i = 0; i < origins.Length; i++)
                        {
                            int t = i;
                            Line line = new((s) => positions[t], (s) => positions[(t + 1) % positions.Length]);
                            line.DrawingColor = Color.Silver;
                            line.Alpha = 0.3f;
                            CreateEntity(line);
                            DelayBeat(1, () =>
                            {
                                line.AlphaDecrease(BeatTime(3));
                            });
                        }
                    });
                    RegisterFunctionOnce("SideLine2", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float xPos = sign * 320 + 320;
                            for (int i = 1; i <= 2; i++)
                            {
                                Line line = new(new Vector2(xPos - i * sign * 12, 0), new Vector2(xPos - i * sign * 12, 480));
                                line.DrawingColor = Color.Yellow;
                                line.Alpha = i / 10f + 0.24f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(2 + i * 0.5f));
                            }
                        }
                    });
                    RegisterFunctionOnce("SideLine3", () =>
                    {
                        for (int sign = -1; sign <= 1; sign += 2)
                        {
                            float yPos = sign * 240 + 240;
                            for (int i = 1; i <= 2; i++)
                            {
                                Line line = new(new Vector2(0, yPos - (i + 1) * sign * 10), new Vector2(640, yPos - (i + 1) * sign * 12));
                                line.DrawingColor = Color.Silver * 0.5f;
                                line.Alpha = i / 10f + 0.24f;
                                CreateEntity(line);
                                line.AlphaDecrease(BeatTime(2 + i * 0.5f));
                            }
                        }
                    });
                    RegisterFunctionOnce("FollowLine", () =>
                    {
                        Line up = new((s) => new(320, BoxStates.Up - 12), (s) => ScreenDrawing.ScreenAngle * 0.9f);
                        Line down = new((s) => new(320, BoxStates.Down + 12), (s) => ScreenDrawing.ScreenAngle * 0.9f);
                        Line[] all = { up, down };
                        foreach (Line line in all)
                        {
                            CreateEntity(line);

                            for (int i = 0; i <= 1; i++)
                                DelayBeat(i * 8, () =>
                                {
                                    line.Alpha = 0.2f;
                                    line.AlphaDecrease(BeatTime(3), 0.12f);
                                });
                            DelayBeat(16, () => { line.Alpha = 0.2f; line.AlphaDecrease(BeatTime(4)); });

                            line.DrawingColor = Color.Aqua;
                            line.Alpha = 0.2f;
                        }
                    });

                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(1.8f), false);
                        ScreenDrawing.CameraEffect.SizeShrink(1.41f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(0.3f, BeatTime(1.8f), true);
                        ScreenDrawing.CameraEffect.SizeShrink(1.4f, BeatTime(3));
                    });
                    RegisterFunctionOnce("Expand", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(2f, BeatTime(3.4f));
                    });
                    RegisterFunctionOnce("Expand2", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3f, BeatTime(4.4f));
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(2f, BeatTime(3.4f));
                    });

                    float height = 6;
                    float alpha = 0.1f;
                    RegisterFunctionOnce("Rise", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.Black * 0.27f);
                        height += 32;
                        alpha += 0.1f;
                        ForBeat(2, () =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.Red * alpha, 0.2f);
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.DownBoundDistance * 0.8f + height * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("Fade", () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.8f, BeatTime(2.7f));
                        DelayBeat(2.5f, () =>
                        {
                            rainer.Intensity = 0.2f;
                            rainer.Speed = 3.0f;
                        });
                    });

                    RegisterFunctionOnce("FallArrow1", () =>
                    {
                        FakeArrow arrow = new(RandIn(0, 1), 0);
                        CreateEntity(arrow);
                        ValueEasing.EaseBuilder moveY = new();
                        moveY.Insert(game.BeatTime(1), ValueEasing.EaseOutQuad(-10, 75, game.BeatTime(1)));
                        moveY.Insert(game.BeatTime(1), ValueEasing.EaseInQuad(75, -10, game.BeatTime(1)));
                        moveY.Run((s) =>
                        {
                            Vector2 t = arrow.Centre;
                            t.X = 100;
                            t.Y = s;
                            arrow.Centre = t;
                        });
                        ValueEasing.EaseBuilder rotate = new();
                        rotate.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(Rand(0, 360), 100 + Rand(0, 360), game.BeatTime(2)));
                        rotate.Run((s) =>
                        {
                            arrow.Rotation = s;
                        });
                        ValueEasing.EaseBuilder alpha = new();
                        alpha.Insert(game.BeatTime(1), ValueEasing.Linear(0, 0.6f, game.BeatTime(1)));
                        alpha.Insert(game.BeatTime(1), ValueEasing.Linear(0.6f, 0, game.BeatTime(1)));
                        alpha.Run((s) =>
                        {
                            arrow.Alpha = s;
                        });
                    });
                    string[] rhythm = {
                    "$3'0.6(UShine)(FallArrow1)", "", "", "",    "", "", "", "",
                    "/", "", "", "",    "", "", "", "RotateL",
                    "$3'0.6(FollowLine)(UShine2)", "", "", "",    "", "", "", "RotateR",
                    "$3'0.6(UShine2)", "", "", "",    "", "", "", "RotateMid",
                    "$3'0.6(UShine2)", "", "", "",    "", "", "", "",
                    "/", "", "", "",    "", "", "RotateRSoft", "",

                    "N3($31)(SideLine)", "", "", "N3",    "", "", "N3", "",
                    "$31", "N3", "", "",    "N3", "", "N3(RotateLSoft)", "",
                    "N31($3)(SideLine)", "", "", "N31",    "", "", "N31", "",
                    "$3", "N31", "", "",    "N31", "", "N31(RotateRSoft)", "",
                    "N3($31)(SideLine)", "", "", "N3",    "", "", "N3", "",
                    "$31", "N3", "", "",    "N3", "", "N3(RotateMidSoft)", "",
                    "^R'1.5(^D1'1.5)(SideLine2)(ShrinkL)", "", "", "^R'1.5(^D1'1.5)(ShrinkR)",    "", "", "^R'1.5(^D1'1.5)(ShrinkL)", "",
                    "/", "^R'1.5(^D1'1.5)(ShrinkR)", "", "",    "^R'1.5(^D1'1.5)(SideLine2)(ShrinkL)", "", "D(RotateLSoft)", "",
                    "R(SpearLine)(SideLine)", "", "", "D1",    "", "", "D", "",
                    "/", "D1", "", "",    "D", "", "+001(RotateRSoft)", "",
                    "R1(MirroredLine)(SideLine)", "", "", "D",    "", "", "D1", "",
                    "/", "D", "", "",    "D1", "", "+011(RotateLSoft)", "",
                    "R(SideLine)(SideLine3)", "", "", "D1",    "", "", "D", "",
                    "/", "D1", "", "",    "D", "", "+0(RotateMidSoft)", "",
                    "^R'1.5(^D1'1.5)(Rise)(Shrink)", "", "", "^R'1.5(^D1'1.5)(Rise)(Shrink)",    "", "", "^R'1.5(^D1'1.5)(Rise)(Shrink)", "",
                    "/", "^R'1.5(^D1'1.5)(Rise)(Expand)", "", "",    "^R'1.5(^D1'1.5)(Expand2)", "(Fade)", "ToRed", "",
                };
                    float time = game.BeatTime(14);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(176))
                {
                    string[] rhythm = {
                    "/", "", "/", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "Spear", "",    "Spear", "", "Spear", "",
                    "Spear", "", "", "Spear",    "", "", "Spear", "",
                    "", "Spear", "", "",    "Spear", "", "", "",
                };
                    float time = game.BeatTime(16 + 1.1f);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(312))
                {
                    RegisterFunctionOnce("DownCross", () =>
                    {
                        float time = BeatTime(32);
                        for (int i = 0; i <= 30; i++)
                        {
                            DelayBeat(i * 8, () =>
                            {
                                Line line = new(
                                    CentreEasing.EaseInQuad(new(0, 0 - 30), new(0, 480 + 90), time),
                                    CentreEasing.EaseInQuad(new(640, 0 - 90 - 30), new(640, 480), time)
                                );
                                line.Alpha = 0.34f;
                                line.DrawingColor = Color.LightBlue;
                                for (int k = 2; k <= 4; k++) line.InsertRetention(new(k * 2 - 2, 1.3f / k));
                                DelayBeat(32, () => line.Dispose());
                                CreateEntity(line);
                            });
                            DelayBeat(i * 8 + 4, () =>
                            {
                                Line line = new(
                                    CentreEasing.EaseInQuad(new(0, 0 - 90 - 30), new(0, 480), time),
                                    CentreEasing.EaseInQuad(new(640, 0 - 30), new(640, 480 + 90), time)
                                );
                                line.Alpha = 0.34f;
                                line.DrawingColor = Color.LightBlue;
                                for (int k = 2; k <= 4; k++) line.InsertRetention(new(k * 2 - 2, 1.3f / k));
                                DelayBeat(32, () => line.Dispose());
                                CreateEntity(line);
                            });
                        }

                        GlobalResources.Effects.NeonLineShader shader;
                        RenderProduction production;
                        ScreenDrawing.BackGroundRendering.InsertProduction(production = new ScreenDrawing.Shaders.Filter(shader = Shaders.NeonLine, 0.5f));
                        DelayBeat(960 - (440 - 4), () =>
                        {
                            production.Dispose();
                        });

                        Color mission = Color.Blue * 0.22f, cur = Color.Transparent;
                        shader.Speed = 1.0f;
                        shader.DrawingColor = cur;
                        ForBeat(8, () =>
                        {
                            cur = Color.Lerp(cur, mission, 0.2f);
                            shader.DrawingColor = cur;
                        });
                    });
                    RegisterFunctionOnce("RotateLSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateRSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(6));
                    });
                    RegisterFunctionOnce("RotateMidSoft", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(5));
                    });

                    RegisterFunctionOnce("ShrinkL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.4f, BeatTime(2), false);
                        ScreenDrawing.CameraEffect.SizeShrink(2.7f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ShrinkR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3.4f, BeatTime(2), true);
                        ScreenDrawing.CameraEffect.SizeShrink(2.7f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate+", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 7 * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("LRotate", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 5 * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        float limit = 0.04f;
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-5) * 0.2f;
                            ScreenDrawing.ScreenScale += limit * 0.2f;
                            limit *= 0.8f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1 * 0.2f;
                        });
                        DelayBeat(3.1f, () => { ScreenDrawing.ScreenAngle = 0; });
                    });

                    string[] rhythm = {
                                        "RotateLSoft", "", "", "",
                    "R", "", "", "",    "R", "", "+0", "",
                    "+0", "", "", "",    "R", "", "", "",
                    "R", "", "+0", "",    "D", "", "+0", "",
                    "R(D1'1.2)", "", "", "",    "R'1.2(D1)(RotateRSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "+0", "",
                    "+0", "", "", "",    "R", "", "", "",
                    "R", "", "+0", "",    "D", "", "+0", "",
                    "R(D1'1.2)", "", "", "",    "R'1.2(D1)(RotateLSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "+0", "",
                    "+0", "", "", "",    "R", "", "", "",
                    "R", "", "+0", "",    "D", "", "+0", "",
                    "R(D1'1.2)", "", "", "",    "R'1.2(D1)(RotateMidSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "+1", "",
                    "+1", "", "", "",    "R", "", "+1", "",
                    "+1", "", "", "(ShrinkL)",    "R(D1)", "", "", "(ShrinkR)",
                    "R(D1)", "", "", "(ShrinkL)",    "R(D1)(RotateRSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "R1", "",
                    "R", "", "", "",    "R", "", "", "",
                    "R", "", "R1", "",    "R", "", "", "",
                    "R(D1)", "", "", "",    "R(D1)(RotateLSoft)", "", "", "",
                    "R", "", "", "",    "R", "", "R1", "",
                    "R", "", "", "",    "R", "", "", "",
                    "R", "", "R1", "",    "R", "", "", "",
                    "R(D1)", "", "", "",    "R(D1)(RotateMidSoft)", "", "", "",
                    "R1", "", "", "(ShrinkL)",    "(^$0'1.5)", "", "^$2'1.5", "",
                    "R1", "", "", "(ShrinkR)",    "(^$2'1.5)", "", "^$0'1.5", "",
                    "R1", "", "", "",    "R", "", "", "",
                    "R", "", "", "",    "", "", "", "",
                    "R(R1)(LRotate)", "", "", "",    "", "", "R(R1)(RRotate)", "",
                    "", "", "", "",    "R(R1)(LRotate)", "", "", "",
                    "", "", "R(R1)(LRotate+)", "",    "DownCross", "", "", "",
                    "R(R1)(MidRotate)", "", "", "Split",    "", "", "", "",
                };
                    float time = game.BeatTime(8 - 2);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(440 - 8))
                {
                    RegisterFunctionOnce("MoveLine", () =>
                    {
                        Line left = new(CentreEasing.Combine(CentreEasing.SinWave(20, BeatTime(16), 0, 0), new Vector2(50, 0)), (s) => 90);
                        Line right = new(CentreEasing.Combine(CentreEasing.SinWave(20, BeatTime(16), 0, -180), new Vector2(640 - 50, 0)), (s) => 90);
                        Line[] lines = { left, right };
                        foreach (Line line in lines)
                        {
                            line.Alpha = 0.2f;
                            line.Depth = 0.2f;
                            line.DrawingColor = Color.Gold;
                            CreateEntity(line);
                            DelayBeat(128, () => line.AlphaDecrease(BeatTime(2)));
                        }
                    });

                    string[] rhythm = {
                    "", "/", "/", "/",    "/", "/", "^$1", "/",
                    "/", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "N3", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "N3", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",

                    "MoveLine", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "R", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "R", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R", "/",
                    "/", "/", "/", "/",    "R", "/", "/", "/",
                    "/", "/", "R", "/",    "/", "/", "/", "FixRed",
                    "R", "/", "/", "/",    "", "/", "/", "/",

                };
                    float time = game.BeatTime(16);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(440 + 8))
                {
                    string[] rhythm = {
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "R1", "/",
                    "/", "/", "/", "/",    "R1", "/", "/", "/",
                    "/", "/", "R1", "/",    "/", "/", "/", "/",
                    "R1", "/", "/", "/",    "/", "/", "/", "/",

                    "DelayArrow", "/", "/", "/",    "/", "/", "", "/",
                    "/", "/", "/", "/",    "", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "N31", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "N31", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                    "/", "/", "/", "/",    "/", "/", "/", "/",
                };
                    float time = game.BeatTime(0);
                    SetPlayerMission(1);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(696 - 0.4f))
                {
                    //create lines
                    for (int i = 4; i < 60; i++)
                    {
                        DelayBeat(i * 2, () =>
                        {
                            Line up = new(CentreEasing.EaseOutQuad(new(320, 0), new(320, BoxStates.Up), BeatTime(8)), (s) => 0);
                            Line down = new(CentreEasing.EaseOutQuad(new(320, 480), new(320, BoxStates.Down), BeatTime(8)), (s) => 0);
                            Line[] lines = { up, down };
                            foreach (Line line in lines)
                            {
                                line.Alpha = 0.4f;
                                line.DrawingColor = Color.LightBlue;
                                line.InsertRetention(new(2, 0.56f));
                                line.InsertRetention(new(4, 0.3f));
                                DelayBeat(4, () =>
                                {
                                    line.AlphaDecrease(BeatTime(4));
                                });
                                CreateEntity(line);
                            }
                        });
                    }

                    string[] rhythm = {
                    "MoveBox", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "LPike", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "RPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "RPike", "/", "/", "/",   "RPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "RPike", "/",
                    "/", "/", "/", "/",   "RPike", "/", "/", "/",
                    "/", "/", "RPike", "/",   "/", "/", "/", "/",
                    "LPike", "/", "/", "/",   "LPike", "/", "/", "/",
                    "LPike", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                    "Merge", "/", "/", "/",   "", "/", "/", "/",
                    "/", "/", "/", "/",   "/", "/", "/", "/",
                };
                    float time = game.BeatTime(0);
                    SetPlayerMission(1);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                }
                if (InBeat(832 - 0.26f))
                {
                    float curAlpha = 0.05f;
                    float alphaMax = 0;
                    RegisterFunctionOnce("HPike", () =>
                    {
                        curAlpha = 0.2f;
                        SetBoxMission(0);
                        int dir = RandSignal();
                        float x = 320 - dir * (BoxStates.Width / 2f + 24);
                        float y = BoxStates.Up + Rand(1, 5) * 16 - 3;
                        DelayBeat(8, () => PlaySound(Sounds.spearShoot));
                        CreateSpear(new Pike(new(x, y), dir * 90 - 90, game.BeatTime(8)) { IsHidden = true });
                        Line line = new(new Vector2(BoxStates.Left, y), new Vector2(BoxStates.Right, y));
                        line.Width = 2.2f;
                        line.DrawingColor = Color.Red;
                        line.Depth = 0.5f;
                        line.Alpha = 0.7f;
                        line.AlphaDecrease(BeatTime(2));
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("CreateLine", () =>
                    {
                        float rotation = MathUtil.Direction(new(320, 240));
                        Line bottomLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(160, 360)), (s) => rotation);
                        Line bottomRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(480, 360)), (s) => -rotation);
                        Line topLeft = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(160, 120)), (s) => -rotation);
                        Line topRight = new(CentreEasing.Combine(CentreEasing.YSinWave(14, BeatTime(32), 0), new Vector2(480, 120)), (s) => rotation);
                        Line[] lines = { bottomLeft, bottomRight, topLeft, topRight };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            line.Alpha = curAlpha;
                            line.DrawingColor = Color.Gold;
                        }
                        ForBeat(125, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.Alpha = curAlpha;
                            }
                            curAlpha = curAlpha * 0.8f + 0.05f * 0.2f;
                            alphaMax += 0.002f;
                            curAlpha = MathF.Min(alphaMax, curAlpha);
                        });
                        DelayBeat(126, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(BeatTime(2));
                            }
                        });
                    });
                    RegisterFunctionOnce("LRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            float limit = 0.06f;
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 5 * 0.2f;
                                ScreenDrawing.ScreenScale += limit * 0.2f;
                                limit *= 0.8f;
                            });
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            float limit = 0.06f;
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-5) * 0.2f;
                                ScreenDrawing.ScreenScale += limit * 0.2f;
                                limit *= 0.8f;
                            });
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        DelayBeat(7, () =>
                        {
                            DelayBeat(1, () =>
                            {
                                ScreenDrawing.SceneOut(Color.Silver, BeatTime(3.5f));
                            });
                            ForBeat(3, () =>
                            {
                                ScreenDrawing.ScreenAngle *= 0.8f;
                            });
                            DelayBeat(3.4f, () =>
                            {
                                ScreenDrawing.ScreenAngle = 0;
                                ScreenDrawing.ScreenScale = 1;
                                rainer.Intensity = 0;
                                rainer.Rotation = 12;
                                rainer.Speed = 7;
                                ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = 0;

                                InstantSetBox(600, 84, 84);
                                SetSoul(1);
                                InstantTP(BoxStates.Centre);

                                ScreenDrawing.UIColor = Color.Transparent;
                                ScreenDrawing.ThemeColor = Color.Transparent;
                                ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                                ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                                Heart.Shields.AddShield(Heart.Shields.BShield);
                            });
                        });
                    });
                    float time = game.BeatTime(0);
                    string[] rhythm = {
                    "HPike(CreateLine)", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "HPike", "/", "/" , "/",
                    "HPike", "/", "/", "/",   "/", "/", "/" , "/",
                    "HPikeM(LRotate)", "/", "/", "/",   "/", "/", "HPikeM(RRotate)" , "/",
                    "/", "/", "/", "/",   "HPikeM(LRotate)", "/", "/" , "/",
                    "/", "/", "HPikeM(RRotate)", "/",   "/", "/", "/" , "/",
                    "HPikeM(MidRotate)", "/", "/", "/",   "/", "/", "/" , "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(0.5f);
                    }
                    CentreEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(128), CentreEasing.YSinWave(6, BeatTime(32), 0));
                    builder.Run((s) => { ScreenDrawing.ScreenPositionDelta = s; });
                }

                if (InBeat(960))
                {
                    float time = game.BeatTime(0);
                    splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                    RegisterFunctionOnce("Texture1", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash0, BeatTime(3 * 4)) { FadeSpeed = 0.32f });
                    });
                    RegisterFunctionOnce("Texture2", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash1, BeatTime(3 * 4)));
                    });
                    RegisterFunctionOnce("Texture3", () =>
                    {
                        CreateEntity(new Image(Resources.Championships.Memory2022.Flash2, BeatTime(9 * 4)));

                        List<TextAttribute> attributes = new();
                        attributes.Add(new TextSpeedAttribute(55));
                        attributes.Add(new TextSizeAttribute(0.75f));
                        attributes.Add(new TextFadeoutAttribute(BeatTime(36), BeatTime(4)));
                        for (int i = 1; i <= 5; i++)
                            attributes.Add(new TextTimeThreshold(BeatTime(i * 4)));
                        TextPrinter printer;
                        CreateEntity(printer = new TextPrinter("$$$Is $something $still $nostalgic $in $memory?", new Vector2(60, 340), attributes.ToArray()));
                        DelayBeat(41, () => printer.Dispose());
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                        DelayBeat(0.7f, () =>
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

                            float beatCount = 7;
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
                    RegisterFunctionOnce("MoveCentre", () =>
                    {
                        BoxStates.BoxMovingScale = 0.04f;
                        SetGreenBox();

                        ScreenDrawing.UIColor = Color.White;
                        ScreenDrawing.ThemeColor = Color.White;
                        ScreenDrawing.HPBar.HPExistColor = Color.White;
                        ScreenDrawing.HPBar.HPLoseColor = Color.White;

                        ForBeat120(32, () => { TP(BoxStates.Centre); ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.87f + 0.995f * 0.13f; });
                    });
                    RegisterFunctionOnce("Easing", () =>
                    {
                        ValueEasing.EaseBuilder easeBuilder = new();
                        easeBuilder.Insert(BeatTime(73), ValueEasing.LerpTo(0, 0.05f, ValueEasing.SinWave(5, BeatTime(48), 0)));
                        easeBuilder.Run((x) => ScreenDrawing.ScreenAngle = x);
                        DelayBeat(74, () => ScreenDrawing.ScreenAngle = 0);
                    });

                    RegisterFunctionOnce("ScaleL", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ForBeat(3f, () =>
                            {
                                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.15f * 0.1f;
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                            });
                        });
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.White * 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ScaleR", () =>
                    {
                        DelayBeat(0.5f, () =>
                        {
                            ForBeat(3f, () =>
                            {
                                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.3f * 0.1f;
                                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + (-5) * 0.1f;
                            });
                        });
                        DelayBeat(1, () =>
                        {
                            ScreenDrawing.SceneOut(Color.Silver * 0.99f, BeatTime(4.5f));
                        });
                    });
                    RegisterFunctionOnce("Restore", () =>
                    {
                        PlaySound(Sounds.heal);
                        Regenerate(1);
                        ScreenDrawing.BoundColor = Color.Red * 0.56f;
                        ScreenDrawing.UpBoundDistance = ScreenDrawing.DownBoundDistance = 110;
                        Color currentLose = Color.White, currentExist = Color.White;
                        ForBeat(3f, () =>
                        {
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.03f;

                            currentLose = Color.Lerp(currentLose, Color.Red, 0.1f);
                            currentExist = Color.Lerp(currentExist, Color.Yellow, 0.1f);

                            ScreenDrawing.HPBar.HPLoseColor = currentLose;
                            ScreenDrawing.HPBar.HPExistColor = currentExist;
                        });
                        rainer.Intensity = 0.8f;
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
                    string[] rhythm = {
                    "Texture1(RGBSplit)(Fade2)(VoidArrow)", "", "", "",   "", "", "", "",   "", "", "", "Fade0",
                    "", "", "", "",   "", "", "", "",   "", "", "", "",
                    "Texture2(Fade2)", "", "", "",   "", "", "", "",   "", "", "", "Fade",
                    "", "", "", "",   "", "", "", "",   "", "", "", "",
                    "Texture3(Fade2)", "", "", "",   "", "", "", "",   "", "", "", "Fade2",
                    "", "", "", "",   "", "", "", "",   "", "", "", "Fade2",
                    "", "", "", "",   "", "", "", "",   "", "", "", "Fade",
                    "", "", "", "(MoveCentre)",   "", "", "", "",   "", "", "", "Fade3",

                    "Easing", "", "", "",   "D", "", "", "",   "D", "", "", "Fade3",
                    "D1", "", "", "",   "D1", "", "", "",   "D1", "", "", "Fade3",
                    "D", "", "", "",   "D", "", "", "",   "N3", "", "", "Fade3",
                    "D1", "", "", "",   "D1", "", "", "",   "N31", "", "", "Fade3",
                    "#44#$3", "", "", "",   "$3", "", "", "",   "$3", "", "", "Fade3",
                    "#30#$31", "", "", "",   "$31", "", "", "",   "$31", "", "", "Fade3",
                    "N3(N31)", "", "", "",   "N3(N31)", "", "", "",   "N3", "", "N31", "+0(Fade3)",
                    "+01", "", "", "ScaleL",    "", "", "", "ScaleR",    "", "", "", "Restore", ""
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1144))
                {
                    ForBeat(4, 500, () =>
                    {
                        CreateEntity(new Particle(Color.White, new Vector2(Rand(4f, 6f), Rand(-0.5f, 0.5f)) * 2, Rand(19f, 25f), new(-121, Rand(0, 480)), Sprites.arrow[0, 1, 0]) { DarkingSpeed = 9f, Rotation = GetRadian(180f) });
                        CreateEntity(new Particle(Color.White, new Vector2(-Rand(4f, 6f), Rand(-0.5f, 0.5f)) * 2, Rand(19f, 25f), new(640 + 121, Rand(0, 480)), Sprites.arrow[1, 1, 0]) { DarkingSpeed = 9f });
                    });

                    sampleIntensity = 0.05f;
                    splitIntensity = 1f;
                    RegisterFunctionOnce("Shader", () =>
                    {
                        var v1 = Shaders.StepSample;
                        RenderProduction production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.4f);
                        ScreenDrawing.SceneRendering.InsertProduction(production1);
                        v1.Intensity = sampleIntensity;
                        ForBeat(144, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.9f + 0.02f * 0.1f;
                            splitIntensity = splitIntensity * 0.9f + 1.00f * 0.1f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                        });
                        ForBeat(144, 48, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.95f + 0.04f * 0.05f;
                            splitIntensity = splitIntensity * 0.95f + 1.50f * 0.05f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                        });
                    });
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.18f;
                        splitIntensity = 3f;
                        ScreenDrawing.CameraEffect.SizeExpand(1.4f, BeatTime(2));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    float time = BeatTime(8);

                    int randVal = Rand(0, 3);
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'2)(^${randVal + 3}1'2)";
                    string arr30 = $"(<${randVal + 2}0)(>${randVal + 0}1)",
                           arr31 = $"(>${randVal + 2}0)(<${randVal + 0}1)",
                           arr3s = $"(^${randVal + 2}0'2)(^${randVal + 0}1'2)";
                    string arr40 = $"(<${randVal + 3}0)(>${randVal + 1}1)",
                           arr41 = $"(>${randVal + 3}0)(<${randVal + 1}1)",
                           arr4s = $"(^${randVal + 3}0'2)(^${randVal + 1}1'2)";

                    string[] rhythm = {
                    //1
                    "(Shader)(Blur)(LRotate)", "", "", "",   "(Blur)(RRotate)", "", "", "",   "R(R1)(Blur)(LRotate)", "", "R", "",
                    //2
                    "R(R1)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)ConvulseL", "",
                    $"(${randVal}11)(${randVal + 2}01)ConvulseR", "",
                    $"(Blur)(${randVal}01)(${randVal + 2}11)ConvulseL", "",
                    $"(${randVal}11)(${randVal + 2}01)ConvulseR", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                    $"(BlurDeep)" + arr30 + arr3s, arr31, arr30 + arr3s, arr31,   arr30 + arr3s, "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1192))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("Shrink", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(17, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(34, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.28f;
                        splitIntensity = 4f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });
                    RegisterFunctionOnce("ArrowDelay", () =>
                    {
                        int randVal = Rand(0, 1) * 2 + 1;
                        string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                               arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                               arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                        string[] rhythm = {
                        arr1s + arr10, arr11, arr1s + arr10, arr11, arr1s + arr10
                        };
                        string[] rhythm2 = {
                        "R(R1)", "", "R", "",   "R1(R)", "", "R1", "",    "R(R1)", "", "R", ""
                        };
                        List<Arrow> alphaControls = new();
                        float time = BeatTime(12);
                        for (int i = 0; i < rhythm.Length; i++)
                        {
                            GameObject[] arrows = NormalizedObjects(time, 5.6f, rhythm[i]);
                            foreach (Arrow arrow in arrows)
                            {
                                DelayBeat(11.2f, () => { arrow.Delay(BeatTime(48)); });
                                CreateEntity(arrow);
                                alphaControls.Add(arrow);
                            }
                            time += game.BeatTime(1);
                        }
                        time = BeatTime(12);
                        for (int i = 0; i < rhythm2.Length; i++)
                        {
                            GameObject[] arrows = NormalizedObjects(time, 6f, rhythm2[i]);
                            foreach (Arrow arrow in arrows)
                            {
                                DelayBeat(11.2f, () => { arrow.Delay(BeatTime(12)); });
                                CreateEntity(arrow);
                                alphaControls.Add(arrow);
                            }
                            time += game.BeatTime(1);
                        }
                        foreach (Arrow arrow in alphaControls)
                        {
                            arrow.Alpha = 0.56f;
                        }
                        float alphaVal = 0.56f;
                        ForBeat(12, 12, () =>
                        {
                            alphaVal = alphaVal * 0.85f + 1 * 0.15f;
                            foreach (Arrow arrow in alphaControls) arrow.Alpha = alphaVal;
                        });
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string[] rhythm = {
                    //1
                    "(Blur)R(R1)(LRotate)", "", "R", "",   "(Blur)R1(R)(RRotate)", "", "R1", "",   "R(R1)(Blur)(LRotate)", "", "R", "",
                    //2
                    "R(R1)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(ConvulseL)(${randVal}00)(${randVal + 2}10)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    $"(Blur)(${randVal}01)(${randVal + 2}11)(ConvulseL)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    //3 
                    $"(Blur)(${randVal}01)(${randVal + 2}11)(ArrowDelay)(ConvulseL)", "", "", "",    "(Blur)+0(R1)", "", "", "",    "(Blur)+0(R1)", "", "$0($01)", "$1($11)",
                    //4 
                    "($2)($21)Shrink(BlurDeep)",    "", "", "", "",    "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1240))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.28f;
                        splitIntensity = 4f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'2)(^${randVal + 3}1'2)";
                    string arr40 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr41 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr4s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";

                    string[] rhythm = {
                    //1
                    "(Blur)(LRotate)", "", "", "",   "(Blur)(RRotate)", "", "", "",   "(Blur)(LRotate)", "", "", "",
                    //2
                    "R(R1)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    $"(Blur)(${randVal}01)(${randVal + 2}11)(ConvulseL)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                     "(BlurDeep)", "", "", "",   "", "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1288))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.08f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.32f;
                        splitIntensity = 5f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    RegisterFunctionOnce("ExpandL", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("ExpandR", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(12, BeatTime(12.2f), true);
                    });
                    RegisterFunctionOnce("FinalRed", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(240, 600, 84);
                        Regenerate(1);
                        PlaySound(Sounds.heal);
                    });

                    float time = BeatTime(8);

                    int randVal = Rand(0, 3);
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2)";
                    string arr20 = $"(<${randVal}1)(>${randVal + 2}0)",
                           arr21 = $"(>${randVal}1)(<${randVal + 2}0)",
                           arr2s = $"(^${randVal}1'2)(^${randVal + 2}0'2)";

                    string[] rhythm = {
                    //1
                    "(Blur)R(R1)(ConvulseL)", "", "", "",   "(Blur)(ConvulseL)", "", "R1", "",   "R1(Blur)(ConvulseL)", "", "R1", "",
                    //2 
                    "(ConvulseR)(Blur)R(R1)", "", "", "",    "(ConvulseR)(Blur)R(R1)", "", "", "",    "(ConvulseR)(Blur)R(R1)", "", "", "",
                    //3  
                    //4 
                    "(BlurDeep)(ExpandL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, "", "",
                    "(BlurDeep)(ExpandR)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, "FinalRed", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1336))
                {
                    RegisterFunctionOnce("Box1", () =>
                    {
                        SetBox(270, 600, 320);
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Circle", () =>
                    {
                        int count = 9;
                        float way = Rand(0, 1) * 2 - 1;
                        float rot = Rand(0, 359);
                        PlaySound(Sounds.spearAppear);
                        Fortimes(count, () =>
                        {
                            CreateSpear(new CircleSpear(Heart.Centre, 3.7f * way, 1.88f, 187f, rot += 360f / count, 0.025f));
                        });
                    });
                    RegisterFunctionOnce("Swarm", () =>
                    {
                        int count = 8;
                        float rotation = Rand(0, 359);
                        PlaySound(Sounds.spearAppear);
                        Fortimes(count, () =>
                        {
                            rotation += 360f / count;
                            CreateSpear(new SwarmSpear(Heart.Centre, 5.0f, 164, rotation, BeatTime(6)));
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(15, rotation), 5.0f, 164, rotation + 45, BeatTime(6)));
                            CreateSpear(new SwarmSpear(Heart.Centre + GetVector2(15, rotation), 5.0f, 164, rotation - 45, BeatTime(6)));
                        });
                        DelayBeat(6, () =>
                        {
                            PlaySound(Sounds.spearShoot);
                        });
                    });
                    RegisterFunctionOnce("Align", () =>
                    {
                        bool way = true;
                        float width = 36;
                        float delta = Rand(-width / 2, width / 2);
                        float timeDel = 0.0f;
                        BoxStates.BoxMovingScale = 0.05f;
                        SetBox(270, 600, 280);
                        float y = BoxStates.Up;
                        for (float i = 20; i <= 620; i += width)
                        {
                            way = !way;
                            float x = delta + i;
                            float v = timeDel;
                            DelayBeat(v, () =>
                            {
                                CreateSpear(new Pike(new(x, y), 90, BeatTime(12 - v)) { IsShootMute = true, IsSpawnMute = true });
                            });
                            timeDel += 0.25f;
                        }
                        ForBeat120(12, () =>
                        {
                            Heart.InstantSetRotation(ScreenDrawing.ScreenAngle);
                        });
                        DelayBeat(8, () =>
                        {
                            PlaySound(Sounds.spearShoot);
                        });
                        ScreenDrawing.CameraEffect.Rotate(45, BeatTime(8));
                    });
                    RegisterFunctionOnce("Restore", () =>
                    {
                        sampleIntensity = 0.5f;
                        splitIntensity = 7;
                        Heart.InstantSetRotation(0);
                        InstantSetBox(Heart.Centre, 16, 16);
                        var v1 = Shaders.StepSample;
                        PlaySound(Sounds.switchScene);
                        PlaySound(Sounds.switchScene);
                        ForBeat(120, () =>
                        {
                            sampleIntensity = sampleIntensity * 0.96f + 0.06f * 0.04f;
                            splitIntensity = splitIntensity * 0.96f + 1.80f * 0.04f;
                            v1.Intensity = sampleIntensity;
                            splitter.Intensity = splitIntensity;
                            ScreenDrawing.ScreenAngle *= 0.7f;
                        });
                    });
                    RegisterFunctionOnce("MoveCentre", () =>
                    {
                        BoxStates.BoxMovingScale = 0.11f;
                        SetBox(240, 84, 84);
                        ForBeat(12, () => InstantTP(BoxStates.Centre));
                    });
                    RegisterFunctionOnce("Enlarge", () =>
                    {
                        SetGreenBox();
                        SetSoul(1);
                    });

                    float time = BeatTime(8);
                    string[] rhythm = {
                    "Box1(Circle)", "", "", "",    "(Circle)", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "(Circle)", "", "", "",    "", "", "", "",
                    "Swarm", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Swarm", "", "", "",    "", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "(Circle)", "", "", "",    "", "", "", "",
                    "(Circle)", "", "", "",    "(Circle)", "", "", "",    "", "", "", "",
                    "Align", "", "", "",    "", "", "", "",    "", "", "", "",
                    "Restore", "", "", "",    "MoveCentre", "", "", "",    "Enlarge", "", "", "",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1432))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.12f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.31f;
                        splitIntensity = 5f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(2, BeatTime(1.5f), true);
                    });

                    int randVal = Rand(0, 3);
                    float time = BeatTime(8);

                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'2)(^${randVal + 2}1'2.5)";
                    string arr20 = $"(<${randVal + 1}0)(>${randVal + 3}1)",
                           arr21 = $"(>${randVal + 1}0)(<${randVal + 3}1)",
                           arr2s = $"(^${randVal + 1}0'2.5)(^${randVal + 3}1'2.5)";
                    string arr30 = $"(<${randVal + 2}0)(>${randVal + 0}1)",
                           arr31 = $"(>${randVal + 2}0)(<${randVal + 0}1)",
                           arr3s = $"(^${randVal + 2}0'2.5)(^${randVal + 0}1'2.5)";
                    string arr40 = $"(<${randVal + 3}0)(>${randVal + 1}1)",
                           arr41 = $"(>${randVal + 3}0)(<${randVal + 1}1)",
                           arr4s = $"(^${randVal + 3}0'2.5)(^${randVal + 1}1'2.5)";

                    string[] rhythm = {
                    //1
                    "(Blur)(LRotate)R(R1)", "", "R", "+0",   "(Blur)(RRotate)R(R1)", "", "R1", "+01",   "(Blur)(LRotate)R(R1)", "", "R", "+0",
                    //2
                    "R(R1)(Blur)(MidRotate)", "", "", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}10)(${randVal + 2}00)(ConvulseR)", "",
                    $"(Blur)(${randVal}00)(${randVal + 2}10)(ConvulseL)", "",
                    $"(${randVal}11)(${randVal + 2}01)(ConvulseR)", "",
                    //3
                    $"(BlurDeep)(ConvulseL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,   arr10 + arr1s, "",
                    $"(BlurDeep)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,   arr20 + arr2s, "", 
                    //4
                    $"(BlurDeep)" + arr30 + arr3s, arr31, arr30 + arr3s, arr31,   arr30 + arr3s, "",
                    $"(BlurDeep)" + arr40 + arr4s, arr41, arr40 + arr4s, arr41,   arr40 + arr4s, "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                if (InBeat(1480))
                {
                    RegisterFunctionOnce("Blur", () =>
                    {
                        sampleIntensity = 0.14f;
                    });
                    RegisterFunctionOnce("BlurDeep", () =>
                    {
                        sampleIntensity = 0.35f;
                        splitIntensity = 6f;
                        ScreenDrawing.CameraEffect.SizeExpand(2.4f, BeatTime(3));
                    });

                    RegisterFunctionOnce("LRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + 3 * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("RRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.8f + (-3) * 0.2f;
                        });
                    });
                    RegisterFunctionOnce("MidRotate", () =>
                    {
                        ForBeat(3, () =>
                        {
                            ScreenDrawing.ScreenAngle *= 0.8f;
                            if (ScreenDrawing.ScreenAngle > 0f) ScreenDrawing.ScreenAngle -= MathF.Min(ScreenDrawing.ScreenAngle, 0.2f);
                            if (ScreenDrawing.ScreenAngle < 0f) ScreenDrawing.ScreenAngle += MathF.Min(-ScreenDrawing.ScreenAngle, 0.2f);
                        });
                    });
                    RegisterFunctionOnce("ConvulseL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.5f), false);
                    });
                    RegisterFunctionOnce("ConvulseR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(3, BeatTime(1.5f), true);
                    });

                    RegisterFunctionOnce("ExpandL", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(26, BeatTime(12.2f), false);
                    });
                    RegisterFunctionOnce("ExpandR", () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(12.2f));
                        ScreenDrawing.CameraEffect.Convulse(26, BeatTime(12.2f), true);
                    });
                    RegisterFunctionOnce("FinalRed", () =>
                    {
                        SetSoul(0);
                        BoxStates.BoxMovingScale = 0.1f;
                        SetBox(240, 600, 84);
                        Regenerate(1);
                    });

                    float time = BeatTime(8);

                    int randVal = Rand(0, 3);
                    string arr10 = $"(<${randVal}0)(>${randVal + 2}1)",
                           arr11 = $"(>${randVal}0)(<${randVal + 2}1)",
                           arr1s = $"(^${randVal}0'3)(^${randVal + 2}1'3)";
                    string arr20 = $"(<${randVal}1)(>${randVal + 2}0)",
                           arr21 = $"(>${randVal}1)(<${randVal + 2}0)",
                           arr2s = $"(^${randVal}1'3)(^${randVal + 2}0'3)";

                    string[] rhythm = {
                    //1
                    "(Blur)R(R1)(ConvulseL)", "", "", "",   "(Blur)(ConvulseL)", "", "R1", "",   "R1(Blur)(ConvulseL)", "", "R1", "",
                    //2 
                    "(ConvulseR)(Blur)R(R1)", "", "", "",    "(ConvulseR)(Blur)R(R1)", "", "", "",    "(ConvulseR)(Blur)R(R1)", "", "", "",
                    //3  
                    //4 
                    "(BlurDeep)(ExpandL)" + arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, arr10 + arr1s, arr11,    arr10 + arr1s, arr11, "", "",
                    "(BlurDeep)(ExpandR)" + arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, arr20 + arr2s, arr21,    arr20 + arr2s, arr21, "FinalRed", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 5.6f, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
            }

            public new void Start()
            {
                CreateEntity(rainer = new Rainer());

                //   CustomSpear spear = new(new(320, 240), CentreEasing.EaseInBack(new(0, 0), new(0, 120), BeatTime(6)), ValueEasing.Stable(90)); 
                //   spear.AlphaIncrease(BeatTime(2));
                //   CreateEntity(spear);
                //  DelayBeat(6, () => spear.AlphaDecrease(BeatTime(2)));

                game = this;
                HeartAttribute.MaxHP = 5;
                HeartAttribute.Speed = 3.26f;
                HeartAttribute.SoftFalling = true;
                TP();
                SetGreenBox();
                SetSoul(1);
                HeartAttribute.ArrowFixed = true;

                //      CreateLine(150, 100);
                //   GametimeDelta = 4300;
                GametimeDelta = -2.5f;// + game.BeatTime(311);
                ScreenDrawing.UISettings.CreateUISurface();

                if (AnomalyAvailable() ) TryGenerateAnomaly();
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
                CreateChart(0, BeatTime(8), 0, rhythm);
            }
            void TryGenerateAnomaly()
            {
                if (((GameStates.CurrentScene as SongFightingScene).Mode & GameMode.Buffed) == GameMode.Buffed)
                    DelayBeat(210, () =>
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