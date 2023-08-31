using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public class Chronostasis : IChampionShip
    {
        public Chronostasis()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));
            difficulties = new();
            difficulties.Add("? ? ?", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 1),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 2),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 3),
                        }
                    );
                public override string BarrageAuthor => "2Kr0NO";
                public override string AttributeAuthor => "That is zKr,but not ZkR";
                public override string PaintAuthor => "OtokP";
                public override string SongAuthor => "7eaF";
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(62.5f / (190f / 60f)) { }

            public static Game game;

            public static Game instance;
            private static Shader shader;
            private float curHP = 10000;
            public string Music => "Chronostasis";
            public string FightName => "Chronostasis(7eaF ver.)";

            private static class MainEffect
            {
                /*ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(new Shader(Loader.Load<Effect>("Musics\\Chronostasis\\shader"))
                {
                    StableEvents = (x) => {
                        x.Parameters["hp"].SetValue(game.curHP / 10000f * game.curHP / 10000f);
                    }
                }));*/
                public static void Shadershake(float Wave, float Radian, float duration)
                {
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(shader));
                    float a = 0;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(duration / 2, () =>
                    {
                        a += Wave;
                        b += Radian;
                        shader.Parameters["range"].SetValue(a);
                        shader.Parameters["frequency"].SetValue(b);
                    }));
                    AddInstance(new TimeRangedEvent(duration / 2 + 1, duration / 2, () =>
                    {
                        a -= Wave;
                        b -= Radian;
                        shader.Parameters["range"].SetValue(a);
                        shader.Parameters["frequency"].SetValue(b);
                    }));
                }
                public class Clock : Entity
                {
                    public float duration = 0;
                    public float xCenter = 0;
                    public float yCenter = 0;
                    public float rotate = 0;
                    public float length = 0;
                    public float Anotherlength = 0;
                    public Color color = Color.White;
                    public Clock(float xCenter, float yCenter, float rotate, float duration, float alpha, float length, float Anotherlength, Color color)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.rotate = rotate;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.length = length;
                        this.color = color;
                        this.Anotherlength = Anotherlength;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new Vector2(xCenter, yCenter), new Vector2(xCenter + Cos(rotate) * length, yCenter + Sin(rotate) * length), 6, color * alpha, 0.99f);
                        Depth = 0.99f;

                        DrawingLab.DrawLine(new Vector2(xCenter, yCenter), new Vector2(xCenter + Cos(rotate + 180) * Anotherlength, yCenter + Sin(rotate + 180) * Anotherlength), 6, color * alpha, 0.99f);
                        Depth = 0.99f;
                    }

                    public override void Update()
                    {
                        time++;
                        if (time == duration)
                        {
                            Dispose();
                        }

                    }
                }
            }
            private static class Barrage
            {
                public static void effect00()
                {
                    for (int i = 0; i < 60; i++)
                    {
                        DrawingUtil.Linerotate lL = new(0, 240, 90, game.BeatTime(3), 0.85f);
                        DrawingUtil.Linerotate lR = new(640, 240, 90, game.BeatTime(3), 0.85f);
                        DrawingUtil.Linerotate lU = new(320, -80, 0, game.BeatTime(3), 0.85f);
                        DrawingUtil.Linerotate lD = new(320, 480 + 80, 0, game.BeatTime(3), 0.85f);
                        float a = 0;
                        AddInstance(new InstantEvent(i * 1.2f, () =>
                        {
                            CreateEntity(lL);
                            CreateEntity(lR);
                            CreateEntity(lU);
                            CreateEntity(lD);
                            AddInstance(new TimeRangedEvent(30, () =>
                            {
                                lL.xCenter += 8;
                                lR.xCenter -= 8;
                                lU.yCenter += 8;
                                lD.yCenter -= 8;
                                lL.alpha -= 0.05f;
                                lR.alpha -= 0.05f;
                                lU.alpha -= 0.05f;
                                lD.alpha -= 0.05f;
                                lL.rotate += 5.5f;
                                lR.rotate += 5.5f;
                                lU.rotate += 5.5f;
                                lD.rotate += 5.5f;
                                a++;
                            }));
                        }));
                    }
                }
                public static void effect01()
                {
                    float a = game.BeatTime(2);
                    float b = 0;
                    float i = 0;
                    float x = 514;
                    game.DelayBeat(2, () =>
                    {
                        MainEffect.Clock c1 = new(320, 240, 0, game.BeatTime(26), 0.85f, 85, 20, Color.White);
                        MainEffect.Clock c2 = new(320, 240, 0, game.BeatTime(26), 0.85f, 50, 20, Color.White);
                        CreateEntity(c1);
                        CreateEntity(c2);
                        game.ForBeat(26, () =>
                        {
                            c1.rotate += x * 0.05f;
                            c2.rotate += x * 0.0125f;
                            x--;
                        });
                        game.DelayBeat(26, () =>
                        {
                            c1.Dispose();
                            c2.Dispose();
                        });
                    });
                    game.ForBeat(2, 4, () =>
                    {
                        DrawingUtil.Linerotate lL = new(320 - i * 16, 240, 90, game.BeatTime(3), 0.85f);
                        DrawingUtil.Linerotate lR = new(320 + i * 16, 240, 90, game.BeatTime(3), 0.85f);
                        DrawingUtil.Linerotate lU = new(320, 240 - i * 16, 0, game.BeatTime(3), 0.85f);
                        DrawingUtil.Linerotate lD = new(320, 240 + i * 16, 0, game.BeatTime(3), 0.85f);
                        CreateEntity(lL);
                        CreateEntity(lR);
                        CreateEntity(lU);
                        CreateEntity(lD);
                        game.ForBeat(0, 2, () =>
                        {
                            lL.alpha -= 0.035f;
                            lR.alpha -= 0.035f;
                            lU.alpha -= 0.035f;
                            lD.alpha -= 0.035f;
                        });
                        i++;
                    });
                    game.ForBeat(2, 2, () =>
                    {
                        InstantSetBox(new Vector2(320, BoxStates.Centre.Y + a * 0.04f), 84, 84);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        a--;
                    });
                    game.ForBeat(4, 3, () =>
                    {
                        InstantSetBox(new Vector2(320, BoxStates.Centre.Y + b * 0.02f), 84, 84);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        b--;
                    });
                    game.DelayBeat(7, () =>
                    {
                        float y = BoxStates.Centre.Y;
                        float t = 0;
                        float w = 20;
                        game.ForBeat(0, 21, () =>
                        {
                            InstantSetBox(new Vector2(320, y - Sin(t * 2) * w), 84, 84);
                            InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                            t++;
                            w -= 0.025f;
                        });
                    });
                    float alpha1 = 0.4f;
                    float alpha2 = 0.4f;
                    game.ForBeat(14, 4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.White * alpha1;
                        alpha1 -= 0.01f;
                    });
                    game.DelayBeat(14, () =>
                    {
                        DrawingUtil.ScreenAngle(2, game.BeatTime(2));
                    });
                    game.ForBeat(18, 4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.White * alpha2;
                        alpha2 -= 0.01f;
                    });
                    game.DelayBeat(18, () =>
                    {
                        DrawingUtil.ScreenAngle(-2, game.BeatTime(4));
                    });
                    game.DelayBeat(22, () =>
                    {
                        DrawingUtil.ScreenAngle(0, game.BeatTime(2));
                        DrawingUtil.Linerotate l1 = new(80, 240, 90 + ScreenDrawing.ScreenAngle, game.BeatTime(3), 0.8f) { Tags = new string[] { "l" } };
                        DrawingUtil.Linerotate l2 = new(80 * 2, 240, 90 + ScreenDrawing.ScreenAngle, game.BeatTime(3), 0.8f) { Tags = new string[] { "l" } };
                        DrawingUtil.Linerotate l3 = new(80 * 3, 240, 90 + ScreenDrawing.ScreenAngle, game.BeatTime(3), 0.8f) { Tags = new string[] { "l" } };
                        DrawingUtil.Linerotate l4 = new(80 * 4, 240, 90 + ScreenDrawing.ScreenAngle, game.BeatTime(3), 0.8f) { Tags = new string[] { "l" } };
                        DrawingUtil.Linerotate l5 = new(80 * 5, 240, 90 + ScreenDrawing.ScreenAngle, game.BeatTime(3), 0.8f) { Tags = new string[] { "l" } };
                        DrawingUtil.Linerotate l6 = new(80 * 6, 240, 90 + ScreenDrawing.ScreenAngle, game.BeatTime(3), 0.8f) { Tags = new string[] { "l" } };
                        DrawingUtil.Linerotate l7 = new(80 * 7, 240, 90 + ScreenDrawing.ScreenAngle, game.BeatTime(3), 0.8f) { Tags = new string[] { "l" } };
                        CreateEntity(l1);
                        CreateEntity(l2);
                        CreateEntity(l3);
                        CreateEntity(l4);
                        CreateEntity(l5);
                        CreateEntity(l6);
                        CreateEntity(l7);
                        float start = 90 + ScreenDrawing.ScreenAngle;
                        float end = 90;
                        float del = start - end;
                        float t = 0;
                        AddInstance(new TimeRangedEvent(game.BeatTime(4), () =>
                        {
                            float x = t / (game.BeatTime(4) - 1);
                            float f = 2 * x - x * x;
                            l1.rotate += start - del * f;
                            l2.rotate += start - del * f;
                            l3.rotate += start - del * f;
                            l4.rotate += start - del * f;
                            l5.rotate += start - del * f;
                            l6.rotate += start - del * f;
                            l7.rotate += start - del * f;
                            t++;
                        }));
                        game.ForBeat(4, () =>
                        {
                            l1.alpha -= 0.02f;
                            l2.alpha -= 0.02f;
                            l3.alpha -= 0.02f;
                            l4.alpha -= 0.02f;
                            l5.alpha -= 0.02f;
                            l6.alpha -= 0.02f;
                            l7.alpha -= 0.02f;
                        });
                    });
                    game.DelayBeat(24, () =>
                    {
                        DrawingUtil.MaskSquare m = new(0, 0, 640, 480, game.BeatTime(2), Color.White, 0.01f);
                        CreateEntity(m);
                        game.ForBeat(2, () =>
                        {
                            m.alpha += 1 / game.BeatTime(2);
                        });
                        game.ForBeat(4, 3, () =>
                        {
                            m.alpha -= 1 / game.BeatTime(3);
                        });
                        game.DelayBeat(7, () =>
                        {
                            m.Dispose();
                        });
                    });
                }
                public static void rhythm01()
                {
                    game.DelayBeat(0, () =>
                    {
                        CreateArrow(game.BeatTime(2), 3, 7, 0, 0);
                    });
                    game.DelayBeat(1, () =>
                    {
                        CreateArrow(game.BeatTime(4.5f), "R", 4, 0, 0, ArrowAttribute.RotateL);
                        CreateArrow(game.BeatTime(4.75f), "R", 4, 0, 0, ArrowAttribute.RotateR);
                        CreateArrow(game.BeatTime(5f), "R", 4, 0, 0, ArrowAttribute.RotateL);
                        CreateArrow(game.BeatTime(5.25f), "R", 4, 0, 0, ArrowAttribute.RotateR);
                    });
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","D",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "(R)(+21)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "(D)(+21)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(t, 3.75f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect02()
                {

                }
                public static void rhythm02()
                {
                    game.DelayBeat(16 - 2, () =>
                      {
                          CreateEntity(new GreenSoulGB(game.BeatTime(4), "R", 1, game.BeatTime(6)));
                      });
                    game.DelayBeat(20, () =>
                    {
                        CreateArrow(game.BeatTime(4), "R", 6.25f, 0, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(game.BeatTime(4), "+2", 6.25f, 0, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(game.BeatTime(5f), "D", 6.25f, 0, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(game.BeatTime(5f), "+2", 6.25f, 0, 0, ArrowAttribute.SpeedUp);
                    }); game.DelayBeat(24 - 2, () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(4), "R", 0, game.BeatTime(6)));
                    });
                    game.DelayBeat(29, () =>
                    {
                        Arrow a1 = MakeArrow(game.BeatTime(2), 1, 6.25f, 1, 0);
                        Arrow a2 = MakeArrow(game.BeatTime(2), 3, 6.25f, 1, 0);
                        CreateEntity(a1);
                        CreateEntity(a2);
                        game.DelayBeat(1.5f, () =>
                        {
                            a1.Stop(game.BeatTime(2.6f));
                            a2.Stop(game.BeatTime(2.6f));
                        });
                        game.DelayBeat(3.9f, () =>
                        {
                            a1.Dispose();
                            a2.Dispose();
                            CreateArrow(game.BeatTime(0.1f), 1, 9.25f, 1, 0);
                            CreateArrow(game.BeatTime(0.1f), 3, 9.25f, 1, 0);
                        });
                    });
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "($0)(+21)","/","/","/",    "/","/","/","/",
                        "R","/","+1","/",    "+3","/","/","/",
                        //
                        "($01)(+2)","/","/","/",    "/","/","/","/",
                        "R1","/","+11","/",    "+31","/","/","/",
                        //
                        "($0)(+21)","/","/","/",    "/","/","/","/",
                        "D","/","+21","/",    "+0","/","/","/",
                        //
                        "($01)($3)","/","/","/",    "/","/","/","/",
                        "($0)($31)","/","/","/",    "/","/","/","/",
                        ////
                        "($01)(+2)","/","/","/",    "/","/","/","/",
                        "R1","/","+11","/",    "+31","/","/","/",
                        //
                        "($0)(+21)","/","/","/",    "/","/","/","/",
                        "D","/","+21","/",    "+0","/","/","/",
                        //
                        "($01)(+2)","/","/","/",    "/","/","/","/",
                        "R1","/","+11","/",    "+31","/","/","/",
                        //
                        "($0)($31)","/","/","/",    "/","/","/","/",
                        "($01)($3)","/","/","/",    "/","/","/","/",
                        ////
                        "(R)(+0)","/","/","/",    "/","/","/","/",
                        "D","/","+1","/",    "+3","/","/","/",
                        //
                        "(D)(+0)","/","/","/",    "/","/","/","/",
                        "D","/","+3","/",    "+1","/","/","/",
                        //
                        "(D)(+0)","/","/","/",    "/","/","/","/",
                        "D","/","+1","/",    "+3","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                        "(R1)(+01)","/","/","/",    "/","/","/","/",
                        "D1","/","+31","/",    "+11","/","/","/",
                        //
                        "(D1)(+01)","/","/","/",    "/","/","/","/",
                        "D1","/","+11","/",    "+31","/","/","/",
                        //
                        "(D1)(+01)","/","/","/",    "/","/","/","/",
                        "D1","/","+31","/",    "+11","/","/","/",
                        //
                        "($01)($21)","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(t, 6.25f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect03()
                {
                    game.DelayBeat(4, () =>
                    {
                        MainEffect.Shadershake(1.2f, 60, game.BeatTime(0.75f));
                    });
                    game.DelayBeat(7, () =>
                    {
                        game.ForBeat(0.95f, () =>
                        {
                            float r = Rand(0, 1);
                            if (r == 0)
                            {
                                ScreenDrawing.ScreenAngle = 0;
                            }
                            if (r == 1)
                            {
                                ScreenDrawing.ScreenAngle = 180;
                            }
                        });
                        game.DelayBeat(1, () =>
                        {
                            ScreenDrawing.ScreenAngle = 0;
                        });
                    });
                    game.DelayBeat(12, () =>
                    {
                        DrawingUtil.MaskSquare m = new(0, 0, 640, 480, game.BeatTime(1), Color.Black, 0);
                        CreateEntity(m);
                        for (int i = 0; i < 4; i++)
                        {
                            game.DelayBeat(0.2f * i, () =>
                            {
                                m.alpha = 1;
                            });
                            game.DelayBeat(0.2f * i + 0.1f, () =>
                            {
                                m.alpha = 0;
                            });
                        }
                        for (int i = 0; i < 12; i++)
                        {
                            AddInstance(new InstantEvent(i, () =>
                            {
                                PlaySound(FightResources.Sounds.change);
                            }));
                        }
                    });
                    game.DelayBeat(15, () =>
                    {
                        float a = 0;
                        DrawingUtil.MaskSquare m = new(0, 0, 640, 480, game.BeatTime(1), Color.Black, 0);
                        CreateEntity(m);
                        for (int i = 0; i < 4; i++)
                        {
                            game.DelayBeat(0.2f * i, () =>
                            {
                                m.alpha = 1;
                                PlaySound(FightResources.Sounds.change);
                            });
                            game.DelayBeat(0.2f * i + 0.1f, () =>
                            {
                                m.alpha = 0;
                            });
                        }
                        game.ForBeat(0.95f, () =>
                        {
                            float r = Rand(0, 1);
                            if (r == 0)
                            {
                                ScreenDrawing.ScreenAngle = 0;
                            }
                            if (r == 1)
                            {
                                ScreenDrawing.ScreenAngle = 180;
                            }
                        });
                        game.DelayBeat(1, () =>
                        {
                            ScreenDrawing.ScreenAngle = 0;
                        });
                        game.ForBeat(0.35f, () =>
                        {
                            shader.Parameters["distance"].SetValue(new Vector2(a, a));
                            a += 0.02f / game.BeatTime(0.35f);
                        });
                        game.ForBeat(0.35f, 0.6f, () =>
                        {
                            shader.Parameters["distance"].SetValue(new Vector2(a, a));
                            a -= 0.02f / game.BeatTime(0.6f);
                        });
                        game.DelayBeat(0.95f, () =>
                        {
                            shader.Parameters["distance"].SetValue(new Vector2(0, 0));
                        });
                    });
                }
                public static void rhythm03()
                {
                    game.DelayBeat(4, () =>
                     {
                         GreenSoulGB gb = new(game.BeatTime(2), 3, 0, game.BeatTime(1));
                         CreateEntity(gb);
                         game.DelayBeat(2.5f, () =>
                         {
                             gb.Kill();
                         });
                     });
                    game.DelayBeat(7, () =>
                    {
                        GreenSoulGB gb = new(game.BeatTime(2), 3, 1, game.BeatTime(1));
                        CreateEntity(gb);
                        game.DelayBeat(2.5f, () =>
                        {
                            gb.Kill();
                        });
                    });
                    game.DelayBeat(12, () =>
                    {
                        GreenSoulGB gb = new(game.BeatTime(2), 3, 0, game.BeatTime(1));
                        CreateEntity(gb);
                        game.DelayBeat(2.5f, () =>
                        {
                            gb.Kill();
                        });
                    });
                    game.DelayBeat(15, () =>
                    {
                        GreenSoulGB gb1 = new(game.BeatTime(2), 3, 1, game.BeatTime(1));
                        GreenSoulGB gb2 = new(game.BeatTime(2), 1, 0, game.BeatTime(1));
                        CreateEntity(gb1);
                        CreateEntity(gb2);
                        game.DelayBeat(2.5f, () =>
                        {
                            gb1.Kill();
                            gb2.Kill();
                        });
                    });
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "D","/","+2","/",    "-2","/","+2","/",
                        "-2","/","/","/",    "R1","/","/","/",
                        //
                        "D1","/","/","/",    "D1","/","/","/",
                        "D1","/","/","/",    "D1","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "D","/","/","/",
                        //
                        "D","/","/","/",    "D","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                        "R1","/","/","/",    "D1","/","/","/",
                        "D1","/","/","/",    "D1","/","/","/",
                        //
                        "R","/","/","/",    "D","/","/","/",
                        "D","/","/","/",    "D","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(t, 6.25f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                    float t1 = game.BeatTime(14);
                    string[] rhythm1 =
                    {
                        "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "X","/","/","/",
                        //
                        "X","/","/","/",    "X","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm1.Length; i++)
                    {
                        if (rhythm1[i] == "/")
                        {
                            t1 += game.BeatTime(0.125f);
                        }
                        else if (rhythm1[i] != "/")
                        {
                            CreateArrow(t1, "D", 6.25f, 1, 0);
                            CreateArrow(t1, LastRand, 6.5f, 0, 0);
                            t1 += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void rhythm04()
                {
                    game.DelayBeat(0, () =>
                    {
                        //骨箭（？
                        float t = game.BeatTime(2);
                        string[] rhythm =
                        {
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                    };
                        for (int i = 0; i < rhythm.Length; i++)
                        {
                            if (rhythm[i] == "/")
                            {
                                //CreateSpear(new NormalSpear(new Vector2(320, 240)) { IsMute = true});
                                t += game.BeatTime(0.125f);
                            }
                            else
                            {
                                t += game.BeatTime(0.125f);
                            }
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            CreateBone(new SideCircleBone(90 * i + 45, 2.75f, 160, game.BeatTime(7.5f)));//max
                            CreateBone(new SideCircleBone(90 * i, 2.75f, 40, game.BeatTime(7.5f)));//min
                            CreateBone(new SideCircleBone(90 * i + 15, 2.75f, 70, game.BeatTime(7.5f)));
                            CreateBone(new SideCircleBone(90 * i - 15, 2.75f, 70, game.BeatTime(7.5f)));
                            CreateBone(new SideCircleBone(90 * i + 30, 2.75f, 110, game.BeatTime(7.5f)));
                            CreateBone(new SideCircleBone(90 * i - 30, 2.75f, 110, game.BeatTime(7.5f)));
                        }
                    });
                }
                public static void effect04()
                {
                    SetSoul(0);
                    SetBox(new Vector2(320, 240), 240, 240);
                }
                public static void rhythm05()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(t, 6.25f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect05()
                {

                }
                public static void rhythm06()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(t, 6.25f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect06()
                {

                }
            }
            #region
            public void Noob()
            {
                if (Gametime < 0) return;
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
                if (Gametime < 0) return;
            }

            public void Extreme()
            {
                if (Gametime < 0) return;
            }
            #endregion
            public void ExtremePlus()
            {
                if (Gametime < 0) return;
                if (InBeat(4)) Barrage.effect00();
                if (InBeat(8 - 2)) Barrage.rhythm01();
                if (InBeat(8 - 2)) Barrage.effect01();
                if (InBeat(36 - 2)) Barrage.rhythm02();
            }
            public void Start()
            {
                game = this;
                GametimeDelta = BeatTime(84 - 3);
                HeartAttribute.MaxHP = 100;
                HeartAttribute.Speed = 3.0f;
                HeartAttribute.SoftFalling = true;
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(new(320, 240));
                SetSoul(1);
                ScreenDrawing.HPBar.Vertical = true;
                ScreenDrawing.UIColor = Color.White;
                ScreenDrawing.HPBar.HPExistColor = new(120, 120, 255);
                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.AreaOccupied = new CollideRect(20, 140, 20, 220);
                ScreenDrawing.ThemeColor = new(200, 200, 255);
                shader = new Shader(Loader.Load<Effect>("Musics\\Chronostasis\\shader"));
            }
        }
    }
}