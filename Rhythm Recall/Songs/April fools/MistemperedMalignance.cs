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
    public class MistemperedMalignance : IChampionShip
    {
        public MistemperedMalignance()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new();
            difficulties.Add("? ? ?", Difficulty.ExtremePlus);
            difficulties.Add("TEST", Difficulty.Extreme);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public static Game instance;
            private static Shader shader;
            public Game() : base(3750 / 155) { }
            public static float bpm = 3750 / 155 / 4;

            public string Music => "Mistempered Malignance";

            public string FightName => "Mistempered Malignance";

            public SongInformation Attributes => new ThisInformation();
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 1)
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 1)
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 1),
                        }
                    );
                public override string BarrageAuthor => "zKronO";
                public override string AttributeAuthor => "zKronO";
                public override string PaintAuthor => "from Arcaea(lowiro)";
                public override string SongAuthor => "s-don vs nitro (lowiro)";
            }
            public static void PlusGreenSoulRotate(float rot, float duration)
            {
                float start = Heart.Rotation;
                float end = rot;
                float del = start - end;
                float t = 0;
                AddInstance(new TimeRangedEvent(0, duration, () =>
                {
                    float x = t / (duration - 1);
                    float f = 2 * x - x * x;
                    Heart.InstantSetRotation(start - del * f);
                    t++;
                }));
            }
            public static void ArrowDrop(int way, int color, int rotatingType, float num, float duration_of_beat)
            {
                for (int i = 0; i < num; i++)
                {
                    CreateArrow(80 + i * (duration_of_beat * bpm / num), way, 16, color, rotatingType);
                }
            }
            public static void RGB()
            {
                ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(shader));
                float a = 0;
                AddInstance(new TimeRangedEvent(0, 2, () =>
                {
                    shader.Parameters["distance"].SetValue(new Vector2(a, a)); a += 0.015f / 2;
                }));
                AddInstance(new TimeRangedEvent(3, 8, () =>
                {
                    shader.Parameters["distance"].SetValue(new Vector2(a, a)); a -= 0.015f / 8;
                }));
                AddInstance(new TimeRangedEvent(12, 1, () =>
                {
                    shader.Parameters["distance"].SetValue(new Vector2(0, 0));
                }));
            }
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
            private static class ExBarrage
            {
                public static Game game;
                public static void pt0()
                {
                    Heart.Shields.AddShield(new Player.Heart.Shield(2, Heart)
                    {
                        UpdateKeys = new InputIdentity[]
                        {
                            InputIdentity.ThirdRight, InputIdentity.ThirdDown, InputIdentity.ThirdLeft, InputIdentity.ThirdUp
                        }
                    });
                    Heart.Shields.AddShield(new Player.Heart.Shield(3, Heart)
                    {
                        UpdateKeys = new InputIdentity[]
                        {
                            InputIdentity.FourthRight, InputIdentity.FourthDown, InputIdentity.FourthLeft, InputIdentity.FourthUp
                        }
                    });
                }
                public static void pt1()
                {
                    shader.Parameters["frequency"].SetValue(0f);
                    shader.Parameters["distance"].SetValue(new Vector2(0f, 0f));
                    shader.Parameters["range"].SetValue(0f);
                    shader.Parameters["frequency2"].SetValue(0f);
                    shader.Parameters["range2"].SetValue(0f);
                    shader.Parameters["time"].SetValue(0f);
                    shader.Parameters["time2"].SetValue(0f);
                    DrawingUtil.SetScreenScale(1, (int)(bpm * 16));
                    DrawingUtil.PlusRotate(7.5f, (int)(bpm * 16));
                    CreateEntity(new GreenSoulGB((int)(bpm * 16), 0, 0, (int)(bpm * 4)));
                    CreateEntity(new GreenSoulGB((int)(bpm * 16), 2, 1, (int)(bpm * 4)));
                    CreateEntity(new GreenSoulGB((int)(bpm * 16), 1, 2, (int)(bpm * 4)));
                    CreateEntity(new GreenSoulGB((int)(bpm * 16), 3, 3, (int)(bpm * 4)));
                    float t = 0;
                    string[] rhythm =
                    {
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R0","/","/","/",    "D(+01)","/","/","/",    "/","/","D(+01)","/",    "/","/","/","/",

                        "$0","/","/","/",    "/","/","/","/",    "+1","/","/","/",    "D0","/","/","/",
                        "/","/","/","/",    "+1","/","/","/",    "D0","/","/","/",    "/","/","/","/",

                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "D1","/","/","/",    "/","/","D(+01)","/",    "/","/","/","/",    "D(+01)","/","/","/",

                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R0","/","/","/",    "/","/","/","/",

                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += 0.5f * bpm;
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(80 + t, 8.5f, rhythm[i]);
                            t += 0.5f * bpm;
                        }
                    }
                    AddInstance(new InstantEvent((int)(bpm * 16), () =>
                    {
                        DrawingUtil.PlusScreenScale(-0.25f, (int)(bpm * 6));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 2 * 16) - 80, () =>
                    {
                        ArrowDrop(0, 0, 1, 7, 2);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 16 + bpm * 12) - 80, () =>
                    {
                        ArrowDrop(2, 1, 0, 14, 2);
                    }));
                }
                public static void pt2()
                {
                    AddInstance(new InstantEvent(0, () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 4 * 2), 2, 3, (int)(bpm * 3)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 6), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 4 * 2), 0, 2, (int)(bpm * 3)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 8), () =>
                    {
                        DrawingUtil.PlusRotate(10, (int)(bpm * 4));
                        DrawingUtil.PlusScreenScale(0.2f, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 6 + bpm * 8), () =>
                    {
                        DrawingUtil.PlusRotate(-10, (int)(bpm * 4));
                        DrawingUtil.PlusScreenScale(0.2f, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 3), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 4 * 2), 3, 1, (int)(bpm * 3)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 4), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 4 * 2), 1, 0, (int)(bpm * 6)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 5), () =>
                    {
                        PlusGreenSoulRotate(180, (int)(bpm * 4));
                        DrawingUtil.SetScreenScale(2, (int)(bpm * 4));
                        DrawingUtil.ScreenAngle(180, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 6), () =>
                    {
                        PlusGreenSoulRotate(360, (int)(bpm * 4));
                        DrawingUtil.SetScreenScale(1, (int)(bpm * 4));
                        DrawingUtil.ScreenAngle(360, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 5), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 4 * 2), 3, 3, (int)(bpm * 3)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 8), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 4 * 2), 2, 2, (int)(bpm * 3)));
                        CreateEntity(new GreenSoulGB((int)(bpm * 4 * 2), 0, 3, (int)(bpm * 7)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 10), () =>
                    {
                        DrawingUtil.PlusScreenScale(0.25f, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 9), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 4 * 2), "R", 0, (int)(bpm * 3)));
                    }));
                }
                public static void pt3()
                {
                    float Beat2 = 3750f / 145f;
                    float Beat3 = 3750f / 135f;
                    float Beat4 = 3750f / 125f;
                    float Beat5 = 3750f / 115f;
                    float Beat6 = 3750f / 110f;
                    float Beat7 = 3750f / 105.5f;
                    float Beat8 = 3750f / 97.5f;
                    float Beat9 = 3750f / 92.5f;
                    float BeatA = 3750f / 87.5f;
                    float BeatB = 3750f / 85.5f;
                    float BeatC = 3750f / 85f;
                    float start = BoxStates.Centre.Y;
                    float del = start - 360;
                    float t = 0;
                    AddInstance(new TimeRangedEvent((int)(bpm * 32), (int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7 + Beat8 + Beat9 + BeatA + BeatB + BeatC), () =>
                     {
                         float x = t / ((int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7 + Beat8 + Beat9 + BeatA + BeatB + BeatC) - 1);
                         float f = 2 * x - x * x;
                         InstantSetBox(start - del * f, 84, 84);
                         t++;
                         InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                         ScreenDrawing.ScreenPositionDetla = new Vector2(ScreenDrawing.ScreenPositionDetla.X, (240 - BoxStates.Centre.Y) * 2f);
                     }));
                    AddInstance(new InstantEvent((int)(bpm * 32), () =>
                    {
                        DrawingUtil.SetScreenScale(2.25f, (int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7 + Beat8 + Beat9 + BeatA + BeatB + BeatC));
                    }));
                    AddInstance(new InstantEvent(0, () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 1, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 1, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 1, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 1, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 1, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 1, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 2, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 2, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 2, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 2, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 2, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 2, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 3, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 3, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 3, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 3, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 3, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 3, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4 + Beat5), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 4, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 4, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 4, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 4, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 4, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 4, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 5, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 5, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 5, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 5, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 5, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 5, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 6, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 6, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 6, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 6, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 6, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 6, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7 + Beat8), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 7, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 7, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 7, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 7, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 7, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 7, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7 + Beat8 + Beat9), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 8, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 8, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 8, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 8, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 8, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 8, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7 + Beat8 + Beat9 + BeatA), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 9, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 9, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 9, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 9, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 9, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 9, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7 + Beat8 + Beat9 + BeatA + BeatB), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 10, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 10, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 10, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 10, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 10, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 10, 1, 0);
                    }));
                    AddInstance(new InstantEvent((int)(Beat2 + Beat3 + Beat4 + Beat5 + Beat6 + Beat7 + Beat8 + Beat9 + BeatA + BeatB + BeatC), () =>
                    {
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 11, 0, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 11, 0, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 11, 0, 0);
                        CreateArrow((int)(bpm * 32), 0, 8 - 0.5f * 11, 1, 0);
                        CreateArrow((int)(bpm * 32), 2, 8 - 0.5f * 11, 1, 0);
                        CreateArrow((int)(bpm * 32), 3, 8 - 0.5f * 11, 1, 0);
                    }));
                }
                public static void pt4()
                {
                    float t = 0;
                    string[] rhythm =
                    {
                        "($011)($211)","/","/","/",    "/","/","/","/",    "(R)(D1)","/","/","/",    "(R)(D1)","/","/","/",    "R","/","/","/",    "/","/","/","/",

                        "(R)(D1)","/","/","/",    "R","/","/","/",    "D1","/","/","/",    "/","/","/","/",
                        "(R1)(D)","/","/","/",    "R1","/","/","/",    "D","/","/","/",    "/","/","/","/",

                        "$311","/","/","/",    "/","/","/","/",    "($001)($201)","/","/","/",    "/","/","/","/",
                        "(R1)(D)","/","/","/",    "(R1)(D)","/","/","/",    "R1","/","/","/",    "/","/","/","/",

                        "(R1)(D)","/","/","/",    "R1","/","/","/",    "D","/","/","/",    "/","/","/","/",
                        "(R)(D1)","/","/","/",    "R","/","/","/",    "D1","/","/","/",    "/","/","/","/",

                        "$301","/","/","/",    "D","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$311","/","/","/",    "D1","/","/","/",    "/","/","/","/",    "/","/","/","/",

                        "$301","/","/","/",    "D","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$311","/","/","/",    "D1","/","/","/",    "/","/","/","/",    "/","/","/","/",

                        "(R)(+2)","/","/","/",    "D1","/","/","/",    "(D1)(+21)","/","/","/",    "D","/","/","/",
                        "(D)(+2)","/","/","/",    "D1","/","/","/",    "(D1)(+21)","/","/","/",    "D","/","/","/",
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += 0.5f * bpm;
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(80 + t, 8.5f, rhythm[i]);
                            t += 0.5f * bpm;
                        }
                    }
                    CreateEntity(new GreenSoulGB((int)(bpm * 2 * 4), 3, 0, (int)(bpm * 4)));
                    CreateArrow((int)(bpm * 2 * 4), 3, 6.2f, 0, 1);
                    AddInstance(new InstantEvent((int)(bpm * 2 * 4), () =>
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                        DrawingUtil.SetScreenScale(1, (int)(bpm * 2));
                        DrawingUtil.Shock(2, 12.5f, 10);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 18 * 2), () =>
                    {
                        DrawingUtil.SetScreenScale(2.5f, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 2 * 16), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 2 * 4), 3, 1, (int)(bpm * 4)));
                        AddInstance(new InstantEvent((int)(bpm * 2 * 4), () =>
                        {
                            DrawingUtil.SetScreenScale(0.875f, (int)(bpm * 2));
                            DrawingUtil.Shock(2, 12.5f, 10);
                        }));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 5 * 16 - 2 * bpm) - 80, () =>
                    {
                        CreateArrow(80, 0, 9, 1, 1, ArrowAttribute.RotateR);
                        CreateArrow(80, 2, 9, 1, 1, ArrowAttribute.RotateL);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 16 + bpm * 4), () =>
                    {
                        DrawingUtil.SetScreenScale(2.5f, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 16), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 2 * 4), 3, 0, (int)(bpm * 4)));
                        AddInstance(new InstantEvent((int)(bpm * 2 * 4), () =>
                        {
                            DrawingUtil.SetScreenScale(0.75f, (int)(bpm * 2));
                            DrawingUtil.Shock(2, 12.5f, 10);
                        }));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 5.5f * 16 - 2 * bpm) - 80, () =>
                    {
                        CreateArrow(80, 0, 9, 0, 1, ArrowAttribute.RotateR);
                        CreateArrow(80, 2, 9, 0, 1, ArrowAttribute.RotateL);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4.5f * 16 + bpm * 4), () =>
                    {
                        DrawingUtil.SetScreenScale(2.5f, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4.5f * 16), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 2 * 4), 3, 1, (int)(bpm * 4)));
                        AddInstance(new InstantEvent((int)(bpm * 2 * 4), () =>
                        {
                            DrawingUtil.SetScreenScale(0.715f, (int)(bpm * 2));
                            DrawingUtil.Shock(2, 12.5f, 10);
                        }));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 6 * 16 - 2 * bpm) - 80, () =>
                    {
                        CreateArrow(80, 0, 9, 1, 1, ArrowAttribute.RotateR);
                        CreateArrow(80, 2, 9, 1, 1, ArrowAttribute.RotateL);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 5 * 16 + bpm * 4), () =>
                    {
                        DrawingUtil.SetScreenScale(2.5f, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 5 * 16), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 2 * 4), 3, 0, (int)(bpm * 4)));
                        AddInstance(new InstantEvent((int)(bpm * 2 * 4), () =>
                        {
                            DrawingUtil.SetScreenScale(0.685f, (int)(bpm * 2));
                            DrawingUtil.Shock(2, 12.5f, 10);
                        }));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 6.5f * 16 - 2 * bpm) - 80, () =>
                    {
                        CreateArrow(80, 0, 9, 0, 1, ArrowAttribute.RotateR);
                        CreateArrow(80, 2, 9, 0, 1, ArrowAttribute.RotateL);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 5.5f * 16 + bpm * 4), () =>
                    {
                        DrawingUtil.SetScreenScale(2.5f, (int)(bpm * 4));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 5.5f * 16), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 2 * 4), 3, 1, (int)(bpm * 4)));
                        AddInstance(new InstantEvent((int)(bpm * 2 * 4), () =>
                        {
                            DrawingUtil.SetScreenScale(0.625f, (int)(bpm * 2));
                            DrawingUtil.Shock(2, 12.5f, 10);
                        }));
                    }));
                }
                public static void pt5()
                {
                    CreateEntity(new GreenSoulGB((int)(bpm * 16), 1, 2, (int)(bpm * 4)));
                    CreateEntity(new GreenSoulGB((int)(bpm * 16), 3, 3, (int)(bpm * 4)));
                    // 目害玩意
                    Arrow ar1 = MakeArrow((int)(bpm * 16.5f), "R", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar2 = MakeArrow((int)(bpm * 16.75f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar3 = MakeArrow((int)(bpm * 17), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar4 = MakeArrow((int)(bpm * 17.25f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar5 = MakeArrow((int)(bpm * 17.5f), "R", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar6 = MakeArrow((int)(bpm * 18), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar7 = MakeArrow((int)(bpm * 18.25f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar8 = MakeArrow((int)(bpm * 19), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar9 = MakeArrow((int)(bpm * 19.25f), "R", 8, Rand(0, 1), Rand(0, 2));
                    Arrow ar0 = MakeArrow((int)(bpm * 19.75f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arA = MakeArrow((int)(bpm * 19.5f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arB = MakeArrow((int)(bpm * 20), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arC = MakeArrow((int)(bpm * 20.25f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arD = MakeArrow((int)(bpm * 20.5f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arE = MakeArrow((int)(bpm * 21f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arF = MakeArrow((int)(bpm * 21.25f), "R", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arG = MakeArrow((int)(bpm * 21.75f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arH = MakeArrow((int)(bpm * 21.5f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arI = MakeArrow((int)(bpm * 22), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arJ = MakeArrow((int)(bpm * 22.25f), "D", 8, Rand(0, 1), Rand(0, 2));
                    Arrow arK = MakeArrow((int)(bpm * 22.5f), "D", 8, Rand(0, 1), Rand(0, 2));
                    CreateEntity(ar1);
                    CreateEntity(ar2);
                    CreateEntity(ar3);
                    CreateEntity(ar4);
                    CreateEntity(ar5);
                    CreateEntity(ar6);
                    CreateEntity(ar7);
                    CreateEntity(ar8);
                    CreateEntity(ar9);
                    CreateEntity(ar0);
                    CreateEntity(arA);
                    CreateEntity(arB);
                    CreateEntity(arC);
                    CreateEntity(arD);
                    CreateEntity(arE);
                    CreateEntity(arF);
                    CreateEntity(arG);
                    CreateEntity(arH);
                    CreateEntity(arI);
                    CreateEntity(arJ);
                    CreateEntity(arK);
                    AddInstance(new InstantEvent((int)(bpm * 16), () =>
                    {
                        DrawingUtil.Shock(2, 12.5f, 12);
                        DrawingUtil.SetScreenScale(1, (int)(bpm * 4));
                        ar1.Stop(256);
                        ar2.Stop(256);
                        ar3.Stop(256);
                        ar4.Stop(256);
                        ar5.Stop(256);
                        ar6.Stop(256);
                        ar7.Stop(256);
                        ar8.Stop(256);
                        ar9.Stop(256);
                        ar0.Stop(256);
                        arA.Stop(256);
                        arB.Stop(256);
                        arC.Stop(256);
                        arD.Stop(256);
                        arE.Stop(256);
                        arF.Stop(256);
                        arG.Stop(256);
                        arH.Stop(256);
                        arI.Stop(256);
                        arJ.Stop(256);
                        arK.Stop(256);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 32), () =>
                    {
                        ar1.Dispose();
                        ar2.Dispose();
                        ar3.Dispose();
                        ar4.Dispose();
                        ar5.Dispose();
                        ar6.Dispose();
                        ar7.Dispose();
                        ar8.Dispose();
                        ar9.Dispose();
                        ar0.Dispose();
                        arA.Dispose();
                        arB.Dispose();
                        arC.Dispose();
                        arD.Dispose();
                        arE.Dispose();
                        arF.Dispose();
                        arG.Dispose();
                        arH.Dispose();
                        arI.Dispose();
                        arJ.Dispose();
                        arK.Dispose();
                    }));
                    for (int i = 0; i < 6; i++)
                    {
                        AddInstance(new InstantEvent((int)(bpm * 28 + i * bpm * 0.65f), () =>
                        {
                            DrawingUtil.BlackScreen(0, (int)(bpm * 0.5f), 1);
                        }));
                    }
                }
                public static void pt6()
                {
                    #region Barrage
                    Arrow ar1 = MakeArrow((int)(bpm * 10), 3, 12, 0, 0);
                    CreateEntity(ar1);
                    for (int i = 0; i < 6; i++)
                    {
                        AddInstance(new InstantEvent((int)(i * bpm * 4 + bpm * 9.5f), () =>
                        {
                            CreateArrow((int)(bpm * 0.5f), 3, 8, 0, 0);
                        }));
                        AddInstance(new InstantEvent((int)(bpm * 4 * i + bpm * 9.625f), () =>
                        {
                            ar1.Delay((int)(bpm * 4));
                        }));
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        AddInstance(new InstantEvent((int)(bpm * 24 + bpm * 9.75f) + i, () =>
                        {
                            ar1.Delay(1.5f);
                        }));
                    }
                    AddInstance(new InstantEvent((int)(bpm * 24 + bpm * 9.75f), () =>
                    {
                        ar1.Delay(1.5f);
                        DrawingUtil.Shock(1, 12, 0, (int)(bpm * 2));
                        Shadershake(3, 125, (int)(bpm * 2));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 24 + bpm * 9.75f) + 100, () =>
                    {
                        ar1.Dispose();
                    }));
                    #endregion
                    #region Effect
                    float t = 0;
                    AddInstance(new TimeRangedEvent((int)(bpm * 8), (int)(bpm * 2), () =>
                    {
                        float x = t / ((int)(bpm * 2) - 1);
                        float f = 2 * x - x * x;
                        ScreenDrawing.ScreenScale += 0.05f * f;
                        t++;
                    }));
                    for (int i = 0; i < 6; i++)
                    {
                        AddInstance(new InstantEvent((int)(i * bpm * 4 + bpm * 10), () =>
                        {
                            DrawingUtil.MinusScreenScale(0.25f, (int)(bpm * 4));
                        }));
                    }
                    #endregion
                }
                public static void pt7()
                {
                    for (int i = 0; i < 3; i++)
                    {
                        AddInstance(new InstantEvent((int)(i * bpm * 4 + bpm * 9.5f), () =>
                        {
                            CreateArrow((int)(bpm * 0.5f), 3, 8, 1, 0);
                        }));
                    }
                    Arrow ar2 = MakeArrow((int)(bpm * 10), 3, 12, 1, 0);
                    CreateEntity(ar2);
                    for (int i = 0; i < 3; i++)
                    {
                        AddInstance(new InstantEvent((int)(bpm * 4 * i + bpm * 9.625f), () =>
                        {
                            ar2.Delay((int)(bpm * 4));
                        }));
                        AddInstance(new InstantEvent((int)(i * bpm * 4 + bpm * 10), () =>
                        {
                            DrawingUtil.MinusScreenScale(0.25f, (int)(bpm * 4));
                        }));
                    }
                    AddInstance(new InstantEvent((int)(bpm * 4 * 4 + bpm * 10) - 80, () =>
                    {
                        CreateArrow(80, 2, 15, 0, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(80, 2, 15, 1, 0, ArrowAttribute.SpeedUp);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 4 + bpm * 12) - 80, () =>
                    {
                        CreateArrow(80, 3, 15, 0, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(80, 3, 15, 1, 0, ArrowAttribute.SpeedUp);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 5 + bpm * 10) - 80, () =>
                    {
                        CreateArrow(80, 0, 15, 0, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(80, 0, 15, 1, 0, ArrowAttribute.SpeedUp);
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 5 + bpm * 10), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 4), 0, 0, (int)(bpm * 3)));
                        CreateEntity(new GreenSoulGB((int)(bpm * 4), 3, 3, (int)(bpm * 4)));
                        CreateEntity(new GreenSoulGB((int)(bpm * 4), 2, 1, (int)(bpm * 3)));
                        for (int i = 0; i < (int)(bpm * 8); i++)
                        {
                            CreateArrow((int)(bpm * 4 + (i / 2)), 0, 15, 0, 0);
                            CreateArrow((int)(bpm * 4 + (i / 2)), 2, 15, 1, 0);
                        }
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4 * 6 + bpm * 10), () =>
                    {
                        DrawingUtil.SetScreenScale(1, (int)(bpm * 4));
                    }));
                }
                public static void pt8()
                {
                    float t = 0;
                    string[] rhythm =
                    {
                        "$210","/","/","/",    "$310","/","/","/",    "$0","/","/","/",    "$3","/","/","/",
                        "$01","/","/","/",    "$31","/","/","/",    "$2","/","/","/",    "$3","/","/","/",

                        "R","/","+3","/",    "+3","/","+3","/",    "D12","/","+012","/",    "+012","/","+012","/",
                        "R1","/","+31","/",    "+31","/","+3","/",    "D12","/","+012","/",    "+012","/","+012","/",

                        "(R)(+21)","(+0)(+01)","(+0)(+01)","(+0)(+01)",    "(R02)(+212)","(+002)(+012)","(+002)(+012)","(+002)(+012)",
                        "(R)(+21)","(+0)(+01)","(+0)(+01)","(+0)(+01)",    "(R02)(+212)","(+002)(+012)","(+002)(+012)","(+002)(+012)",
                        "(R)(+21)","(+0)(+01)","(+0)(+01)","(+0)(+01)",    "(R02)(+212)","(+002)(+012)","(+002)(+012)","(+002)(+012)",
                        "(R)(+21)","(+0)(+01)","(+0)(+01)","(+0)(+01)",    "(R02)(+212)","(+002)(+012)","(+002)(+012)","(+002)(+012)",
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += 0.5f * bpm;
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows((int)(bpm * 8) + t, 8.5f, rhythm[i]);
                            t += 0.5f * bpm;
                        }
                    }
                    AddInstance(new InstantEvent(0, () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 8), "R", 0, (int)(bpm * 3)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 4), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 8), "D", 1, (int)(bpm * 3)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 8), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 8), "D", 0, (int)(bpm * 3)));
                    }));
                    AddInstance(new InstantEvent((int)(bpm * 12), () =>
                    {
                        CreateEntity(new GreenSoulGB((int)(bpm * 8), "D", 1, (int)(bpm * 3)));
                    }));
                    Arrow ar1 = MakeArrow((int)(bpm * 56), 0, 7.5f, 1, 0);
                    CreateEntity(ar1);
                    Arrow ar2 = MakeArrow((int)(bpm * 56), 1, 7.5f, 0, 0);
                    CreateEntity(ar2);
                    Arrow ar3 = MakeArrow((int)(bpm * 56), 2, 7.5f, 1, 0);
                    CreateEntity(ar3);
                    Arrow ar4 = MakeArrow((int)(bpm * 56), 3, 7.5f, 0, 0);
                    CreateEntity(ar4);
                    Arrow ar5 = MakeArrow((int)(bpm * 56), 0, 7.5f, 1, 2);
                    CreateEntity(ar5);
                    Arrow ar6 = MakeArrow((int)(bpm * 56), 1, 7.5f, 0, 1);
                    CreateEntity(ar6);
                    Arrow ar7 = MakeArrow((int)(bpm * 56), 2, 7.5f, 1, 2);
                    CreateEntity(ar7);
                    Arrow ar8 = MakeArrow((int)(bpm * 56), 3, 7.5f, 0, 1);
                    CreateEntity(ar8);
                    for (int i = 0; i < 256; i++)
                    {
                        AddInstance(new InstantEvent((int)(bpm * 55) + i, () =>
                        {
                            ar1.Delay(2);
                            ar2.Delay(2);
                            ar3.Delay(2);
                            ar4.Delay(2);
                            ar5.Delay(2);
                            ar6.Delay(2);
                            ar7.Delay(2);
                            ar8.Delay(2);
                        }));
                    }
                    AddInstance(new InstantEvent((int)(bpm * 55) + 45, () =>
                    {
                        ar1.Dispose();
                        ar2.Dispose();
                        ar3.Dispose();
                        ar4.Dispose();
                        ar5.Dispose();
                        ar6.Dispose();
                        ar7.Dispose();
                        ar8.Dispose();
                    }));
                }
                public static void pt9()
                {

                }
                public static void pt10()
                {

                }
                public static void pt11()
                {

                }
                public static void pt12()
                {

                }
                public static void pt13()
                {

                }
                public static void pt14()
                {

                }
                public static void pt15()
                {

                }
                public static void pt16()
                {

                }
                public static void pt17()
                {

                }
                public static void pt18()
                {

                }
                public static void pt19()
                {

                }
            }
            public void ExtremePlus()
            {
                //if (InBeat(2.2f + 4 * 21 - 2)) ExBarrage.pt0();
                //---------------------------//
                if (Gametime < 0) return;
                if (InBeat(0)) ExBarrage.pt0();
                if (InBeat(0.125f)) ExBarrage.pt1();
                if (InBeat(8.125f - 2)) ExBarrage.pt2();
                if (InBeat(3 * 4)) ExBarrage.pt3();
                if (InBeat(2.25f + 4 * 9 - 2)) ExBarrage.pt4();
                if (InBeat(2.25f + 4 * 16 - 4)) ExBarrage.pt5();
                if (InBeat(2.25f + 4 * 17 - 2f)) ExBarrage.pt6();
                if (InBeat(2.25f + 4 * 19 - 2f)) ExBarrage.pt7();
                if (InBeat(2.75f + 4 * 21 - 2f)) ExBarrage.pt8();
            }

            public void Extreme()
            {
                //
            }
            #region Non-
            public void Easy()
            {
                //
            }

            public void Hard()
            {
                //
            }

            public void Noob()
            {
                //
            }

            public void Normal()
            {
                //
            }
            #endregion
            public void Start()
            {
                ScreenDrawing.ScreenScale = 0.01f;
                GametimeDelta = BeatTime(-4);
                //GametimeDelta = BeatTime(2.2f + 4 * 21 - 3); 
                SetGreenBox();
                InstantTP(320, 240);
                SetSoul(1);
                HeartAttribute.MaxHP = 1000;
                ScreenDrawing.HPBar.Vertical = true;
                HeartAttribute.DamageTaken = 10;
                ScreenDrawing.HPBar.HPExistColor = Color.Red;
                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.AreaOccupied = new CollideRect(20, 140, 20, 180);
                shader = new Shader(Loader.Load<Effect>("Musics\\Mistempered Malignance\\Shaking"));
            }
        }

    }
}