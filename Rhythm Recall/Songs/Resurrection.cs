using Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static Extends.FightUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.CameraEffect;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
namespace Rhythm_Recall.Waves
{
    public class Resurrection : WaveConstructor, IWaveSet
    {
        public static bool linebool = true;
        private class ThisImformation : SongImformation
        {
            public override string BarrageAuthor => "Tlottgodinf";
            public override string SongAuthor => "Cztv-28";
            public override string PaintAuthor => "HZ QVQ";
            public override string AttributeAuthor => "Tlottgodinf";
            public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 17.4f),
                            new(Difficulty.Hard, 14.6f),
                            new(Difficulty.Normal, 10.0f),
                            new(Difficulty.Easy, 6.0f),
                            new(Difficulty.Noob, 3.0f),
                    }
                );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 17.4f),
                            new(Difficulty.Hard, 14.5f),
                            new(Difficulty.Normal, 10.0f),
                            new(Difficulty.Easy, 6.0f),
                            new(Difficulty.Noob, 3.0f),
                    }
                );
            public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 20.9f),
                            new(Difficulty.Hard, 19.0f),
                            new(Difficulty.Normal, 15.0f),
                            new(Difficulty.Easy, 9.0f),
                            new(Difficulty.Noob, 6.0f),
                    }
                );
        }
        public SongImformation Attributes => new ThisImformation();
        public Resurrection() : base(62.5f / (560 / 60f)) { }
        public static Resurrection instance;

        public string Music => "Resurrection";
        public string FightName => "Resurrection.";
        public static float bpm = 62.5f / (560 / 60f);
        public static class ExtremeBarrage
        {
            public class sDownBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                float time = 0;
                public bool marcksore;
                private int appearTime;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public class sUpBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                int appearTime;
                float time = 0;
                public bool marcksore;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public static void SpecialKnife(Vector2 center, float rotate, bool trafic, float trafictime, bool value)
            {
                DrawingUtil.Linerotate head = new(center.X, center.Y, rotate + 180, trafictime, 1, Color.Red);
                head.width = 1.5f;
                CreateEntity(head);
                AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                {
                    head.Dispose();
                }));
                float sin = 90;
                float rot = 0;
                AddInstance(new TimeRangedEvent(0, trafictime - 2, () =>
                {
                    sin += 90 / (trafictime - 2);
                    head.rotate = rot;
                    rot = rotate;
                }));
                if (!trafic)
                {
                    PlaySound(Sounds.Warning);
                }
                AddInstance(new TimeRangedEvent(trafictime + 1, 1, () =>
                {
                    if (!value) PlaySound(Sounds.largeKnife, 0.7f);
                    DrawingUtil.Linerotate Line = new(center.X, center.Y, rot, bpm * 32, 1, Color.Red);
                    Line.width = 0;
                    CreateEntity(Line);
                    Extends.DrawingUtil.Shock(1.2f, 1.3f, 3);
                    AddInstance(new TimeRangedEvent(5, bpm * 9, () =>
                    {
                        Line.width = Line.width * 0.7f + 25 * 0.3f;
                        Line.alpha -= 1 / bpm / 4;
                    }));
                    for (int a = 0; a < 2; a++)
                    {
                        CreateSpear(new Pike(center + new Vector2(Cos(rot + 180) * (128 + a * 66), Sin(rot + 180) * (128 + a * 66)), rot, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                        CreateSpear(new Pike(center + new Vector2(Cos(rot) * (128 + a * 66), Sin(rot) * (128 + a * 66)), rot + 180, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                    }
                }));

            }
            public static void ReturnJumpBone1(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(false, 4.4f, 26));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height - 26 - 48));
                    CreateBone(new UpBone(true, 4.4f, 26));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height - 26 - 48));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(false, 4.4f, 26));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height - 26 - 48));
                    CreateBone(new LeftBone(true, 4.4f, 26));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height - 26 - 48));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(false, 4.4f, 26));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height - 26 - 48));
                    CreateBone(new DownBone(true, 4.4f, 26));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height - 26 - 48));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(false, 4.4f, 26));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height - 26 - 48));
                    CreateBone(new RightBone(true, 4.4f, 26));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height - 26 - 48));
                }
            }
            public static void ReturnJumpBone2(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(true, 4.4f, 26));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height - 26 - 48));
                    CreateBone(new UpBone(false, 4.4f, 26));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height - 26 - 48));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(true, 4.4f, 26));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height - 26 - 48));
                    CreateBone(new LeftBone(false, 4.4f, 26));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height - 26 - 48));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(true, 4.4f, 26));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height - 26 - 48));
                    CreateBone(new DownBone(false, 4.4f, 26));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height - 26 - 48));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(true, 4.4f, 26));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height - 26 - 48));
                    CreateBone(new RightBone(false, 4.4f, 26));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height - 26 - 48));
                }
            }
            public static void SinBone1(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone2(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone3(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone4(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void CrossBone(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void CrossBone2(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void Intro0()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)","/","+0","/",    "+0","/","/","/",
                "+0","/","/","/",    "+2","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",

                "R","/","+1","/",    "-1","/","/","/",
                "R","/","/","/",    "R1","/","/","/",
                "$0","/","$1","/",    "$2","/","$3","/",
                "R1","/","/","/",    "R1","/","/","/",

                "R","/","+1","/",    "-1","/","/","/",
                "R","/","/","/",    "R1","/","/","/",
                "$0","/","$1","/",    "$2","/","$3","/",
                "R1","/","/","/",    "R1","/","/","/",

                "R","/","+1","/",    "-1","/","/","/",
                "R","/","/","/",    "+2","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro1()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$01($01)","/","+11","/",    "-11","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "+2","/","/","/",

                "R1","/","+11","/",    "-11","/","/","/",
                "R1","/","/","/",    "R","/","/","/",
                "$01","/","$11","/",    "$21","/","$31","/",
                "R","/","/","/",    "R","/","/","/",

                "R1","/","+11","/",    "-11","/","/","/",
                "R1","/","/","/",    "R0","/","/","/",
                "$01","/","$11","/",    "$21","/","$31","/",
                "R0","/","/","/",    "R0","/","/","/",

                "R1","/","+11","/",    "-11","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "+2","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro2()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)($21)($21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 6f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro3()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$01($01)($2)($2)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "R(R1)","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro4()
            {
                float beat = 0;
                string[] arrow =
                    {
                "G12","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "G12","/","/","/", "/","/","/","/",

                "G02","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",

                "G12","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",

                "G02","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro5()
            {
                float beat = 0;
                string[] arrow =
                    {
                "G12","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "G12","/","/","/", "/","/","/","/",

                "G02","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",

                "G12","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",

                "G02","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro6()
            {
                int rand1 = Rand(0, 3);
                int rand2 = Rand(0, 3);
                int rand3 = Rand(0, 3);
                int rand4 = Rand(0, 3);
                SetBox(240, 128, 128);
                SetSoul(2);
                AddInstance(new InstantEvent(0.5f, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand1, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 8, () =>
                {
                    CreateEntity(new Boneslab(90 * rand1, 12, 1, bpm) { ColorType = 1 });
                    ReturnJumpBone1(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 12, () =>
                {
                    CreateEntity(new Boneslab(90 * rand1, 24, 1, bpm) { ColorType = 1 });
                    ReturnJumpBone2(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand2, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 24, () =>
                {
                    CreateEntity(new Boneslab(90 * rand2, 12, 1, bpm) { ColorType = 1 });
                    ReturnJumpBone1(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 28, () =>
                {
                    CreateEntity(new Boneslab(90 * rand2, 24, 1, bpm) { ColorType = 1 });
                    ReturnJumpBone2(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand3, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 40, () =>
                {
                    CreateEntity(new Boneslab(90 * rand3, 12, 1, bpm) { ColorType = 1 });
                    ReturnJumpBone1(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 44, () =>
                {
                    CreateEntity(new Boneslab(90 * rand3, 24, 1, bpm) { ColorType = 1 });
                    ReturnJumpBone2(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand4, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 56, () =>
                {
                    CreateEntity(new Boneslab(90 * rand4, 12, 1, bpm) { ColorType = 1 });
                    ReturnJumpBone1(rand4);
                }));
                AddInstance(new InstantEvent(bpm * 60, () =>
                {
                    CreateEntity(new Boneslab(90 * rand4, 24, 1, bpm) { ColorType = 1 });
                    ReturnJumpBone2(rand4);
                }));
            }
            public static void Intro7()
            {

                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                /*for(int a=0;a<8;a++)
                {
                    AddInstance(new InstantEvent(bpm * 16*a, () =>
                    {
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                    }));
                }*/
                CreateEntity(new DownBonesea(58, bpm * 4, 64, false, 2.8f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(58, bpm * 4, 64, true, 2.8f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new sDownBonesea(58, bpm * 4, 64, false, 2.8f, bpm * 64, 2) { tags = new string[] { "a" } });
                CreateEntity(new sUpBonesea(58, bpm * 4, 64, true, 2.8f, bpm * 64, 2) { tags = new string[] { "a" } });
                for (int a = 0; a < 15; a++)
                {
                    AddInstance(new TimeRangedEvent(bpm * 16 * a, 1, () =>
                    {
                        CrossBone(new Vector2(6.5f, 0), 24, 1);
                        PlaySound(Sounds.pierce);
                    }));
                }
                AddInstance(new TimeRangedEvent(1, 1, () =>
                {
                    DownBone[] down = GetAll<DownBone>("a");
                    UpBone[] up = GetAll<UpBone>("a");

                    float speed = 1;
                    AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                    {
                        for (int a = 0; a < down.Length; a++)
                        {
                            int b = a;
                            down[b].MissionLength = 128 / 2 + Sin(speed) * 13 - 14;
                            up[b].MissionLength = 128 - down[b].MissionLength - 14;
                        }
                    }));
                    AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                    {
                        speed += 360 / bpm / 16;
                    }));
                }));
                for (int a = 0; a < 58; a++)
                {
                    AddInstance(new InstantEvent(bpm * 4 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 2, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 3, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro8()
            {
                SetSoul(1);
                TP();
                SetBox(240, 84, 84);
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    SetSoul(1);
                }));
            }
            public static void Intro9()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0","/","/","/", "/","/","/","/",
                "$2","/","/","/", "/","/","/","/",
                "$0","/","/","/", "$2","/","/","/",
                "$0","/","/","/", "$2","/","/","/",

            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 3f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro10()
            {
                float beat = 0;
                string[] arrow =
                    {
                "/","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R1","/","/","/", "+11","/","/","/",
                "+11","/","/","/", "+11","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+11","/", "-1","/","+11","/",
                "R","/","+11","/", "-1","/","+11","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "R1","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",

                "R0","/","/","/", "+1","/","/","/",
                "+1","/","/","/", "+1","/","/","/",
                "R1","/","/","/", "+11","/","/","/",
                "+11","/","/","/", "+11","/","/","/",

                "G03","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "R(R1)","/","/","/", "+01","/","/","/",
                "R1","/","/","/", "+01","/","/","/",

                "R","/","+1","/", "-11","/","+01","/",
                "R(R1)","/","/","/", "R(R1)","/","/","/",
                "R1","/","+11","/", "-1","/","+0","/",
                "R(R1)","/","/","/", "R(R1)","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+01","/", "R","/","+01","/",
                "R","/","+01","/", "R","/","+01","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","+01","/", "R","/","+01","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+01","/", "R","/","+01","/",
                "R","/","+01","/", "R","/","+01","/",

                "R(R1)","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+01","/", "R","/","+01","/",
                "R","/","+01","/", "R","/","+01","/",

                "T01","/","/","/", "/","/","/","/",
                "/","/","/","/", "T01","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",

                "R(+21)"
            };
                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 1, (arrow[i][2] - '0') * greencount));
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 0, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro11()
            {
                CreateBone(new CentreCircleBone(240, -2.8f, 160 - 70, bpm * 16 * 8));
                CreateBone(new CentreCircleBone(240 + 90, 3, 160 - 100, bpm * 16 * 8));
                for (int a = 0; a < 4; a++)
                {
                    CreateBone(new SideCircleBone(240 + 90 * a + 45, 2.8f, 20, bpm * 16 * 8));
                    CreateBone(new SideCircleBone(240 + 90 * a, 2.8f, 50, bpm * 16 * 8));
                }
                AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(240 + 360 / 12 * a, 2.8f, 45, bpm * 16 * 8 - bpm * 8));
                    CreateBone(new CentreCircleBone(30, 2, 80 + 16, bpm * 16 * 8 - bpm * 8));
                    CreateBone(new CentreCircleBone(90, -2, 60 + 15, bpm * 16 * 8 - bpm * 8) { ColorType = 1 });
                    CreateBone(new CentreCircleBone(180, -2, 60 + 15, bpm * 16 * 8 - bpm * 8) { ColorType = 2 });
                }));
                /*AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    SetSoul(0);
                    float beat = 0;
                    string[] arrow =
                        {
            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "$0","/","$0","/",
            "$0","/","/","/", "/","/","/","/",
        };
                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        int x = i;
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'T')
                        {
                            beat += bpm * 4f;
                            instance.CreateArrows(beat + bpm * 16, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 1.333f, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 2.666f, 5.5f, "$0");
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            beat += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] != "/")
                        {
                            AddInstance(new InstantEvent(beat, () =>
                            {
                                PlaySound(Sounds.pierce, 0.75f);
                                CreateEntity(new RotBone(System.MathF.Sqrt(160 * 160 * 2) / 2 - 5, 2.8f, Rand(40, 50), true, Rand(0, 3)));
                                beat += bpm * 0.5f;
                            }));
                        }
                    }
                }));测试卡点*/
            }
            public static void Intro12()
            {
                SetSoul(3);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                /*for(int a=0;a<8;a++)
                {
                    AddInstance(new InstantEvent(bpm * 16*a, () =>
                    {
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                    }));
                }*/
                CreateEntity(new DownBonesea(58, bpm * 4, 64, false, 2.8f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(58, bpm * 4, 64, true, 2.8f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new sDownBonesea(58, bpm * 4, 64, false, 2.8f, bpm * 64, 2) { tags = new string[] { "a" } });
                CreateEntity(new sUpBonesea(58, bpm * 4, 64, true, 2.8f, bpm * 64, 2) { tags = new string[] { "a" } });
                for (int a = 0; a < 15; a++)
                {
                    AddInstance(new TimeRangedEvent(bpm * 16 * a, 1, () =>
                    {
                        int rand = Rand(-25, 25);
                        CreateGB(new NormalGB(Heart.Centre + GetVector2(200, 90 + rand), Heart.Centre + GetVector2(320, 90 + rand), new(1, 0.5f), bpm * 8, bpm * 1.5f));
                        CreateGB(new NormalGB(Heart.Centre + GetVector2(200, 90 - rand), Heart.Centre + GetVector2(320, 90 - rand), new(1, 0.5f), bpm * 8, bpm * 1.5f));
                        CreateGB(new NormalGB(Heart.Centre + GetVector2(200, 270 + rand), Heart.Centre + GetVector2(320, 270 + rand), new(1, 0.5f), bpm * 8, bpm * 1.5f));
                        CreateGB(new NormalGB(Heart.Centre + GetVector2(200, 270 - rand), Heart.Centre + GetVector2(320, 270 - rand), new(1, 0.5f), bpm * 8, bpm * 1.5f));
                        PlaySound(Sounds.pierce);
                    }));
                }
                AddInstance(new TimeRangedEvent(1, 1, () =>
                {
                    DownBone[] down = GetAll<DownBone>("a");
                    UpBone[] up = GetAll<UpBone>("a");

                    float speed = 1;
                    AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                    {
                        for (int a = 0; a < down.Length; a++)
                        {
                            int b = a;
                            down[b].MissionLength = 128 / 2 + Sin(speed) * 13 - 14;
                            up[b].MissionLength = 128 - down[b].MissionLength - 14;
                        }
                    }));
                    AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                    {
                        speed += 360 / bpm / 16;
                    }));
                }));
                for (int a = 0; a < 58; a++)
                {
                    AddInstance(new InstantEvent(bpm * 4 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 2, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 3, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro13()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
            };

                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 1, (arrow[i][2] - '0') * greencount));
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 0, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
                float beat1 = 0;
                string[] arrow1 =
                    {
                "$01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "$0($0)($21)($21)"
            };
                for (int i = 0; i < arrow1.Length; i++)
                {
                    if (arrow1[i] == "/") beat1 += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat1, 6f, arrow1[i]);
                        beat1 += bpm * 0.5f;
                    }
                }
            }
        }
        public static class HardBarrage
        {
            public class sDownBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                float time = 0;
                public bool marcksore;
                private int appearTime;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public class sUpBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                int appearTime;
                float time = 0;
                public bool marcksore;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public static void ReturnJumpBone1(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(false, 4.4f, 26));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new UpBone(true, 4.4f, 26));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(false, 4.4f, 26));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new LeftBone(true, 4.4f, 26));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(false, 4.4f, 26));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new DownBone(true, 4.4f, 26));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(false, 4.4f, 26));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new RightBone(true, 4.4f, 26));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height - 26 - 56));
                }
            }
            public static void ReturnJumpBone2(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(true, 4.4f, 26));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new UpBone(false, 4.4f, 26));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(true, 4.4f, 26));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new LeftBone(false, 4.4f, 26));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(true, 4.4f, 26));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new DownBone(false, 4.4f, 26));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(true, 4.4f, 26));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new RightBone(false, 4.4f, 26));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
            }
            public static void SinBone1(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone2(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone3(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone4(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void CrossBone(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void CrossBone2(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void Intro0()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)","/","+0","/",    "+0","/","/","/",
                "+0","/","/","/",    "+2","/","/","/",
                "R1","/","/","/",    "+01","/","/","/",
                "R1","/","/","/",    "+01","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "$0","/","$1","/",    "$2","/","$3","/",
                "$01","/","/","/",    "$11","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "$0","/","$1","/",    "$2","/","$3","/",
                "$01","/","/","/",    "$11","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "+2","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro1()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$01($01)","/","+01","/",    "+01","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "+2","/","/","/",

                "R1","/","+01","/",    "+01","/","/","/",
                "R1","/","/","/",    "R","/","/","/",
                "$01","/","$11","/",    "$21","/","$31","/",
                "$0","/","/","/",    "$1","/","/","/",

                "R1","/","+01","/",    "+01","/","/","/",
                "R1","/","/","/",    "R0","/","/","/",
                "$01","/","$11","/",    "$21","/","$31","/",
                "$0","/","/","/",    "$1","/","/","/",

                "R1","/","+01","/",    "+01","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "+2","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro2()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)($21)($21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",

                "R(+21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",

                "R(+21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",

                "R(+21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        CreateArrow(beat + 16 * bpm, Rand(0, 3), 6f, 0, 0);
                        CreateArrow(beat + 16 * bpm, LastRand + 2, 6f, 1, 0);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro3()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$01($01)($2)($2)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",

                "R(+21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",

                "R(+21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "R(+21)","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",

                "R(+21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        CreateArrow(beat + 16 * bpm, Rand(0, 3), 6f, 0, 0);
                        CreateArrow(beat + 16 * bpm, LastRand + 2, 6f, 1, 0);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro4()
            {
                float beat = 0;
                string[] arrow =
                    {
                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R0","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R0","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R0","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R0","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R0","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro5()
            {
                float beat = 0;
                string[] arrow =
                    {
                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro6()
            {
                int rand1 = Rand(0, 3);
                int rand2 = Rand(0, 3);
                int rand3 = Rand(0, 3);
                int rand4 = Rand(0, 3);
                SetBox(240, 128, 128);
                SetSoul(2);
                AddInstance(new InstantEvent(0.5f, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand1, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 8, () =>
                {
                    CreateEntity(new Boneslab(90 * rand1, 12, 1, bpm) { ColorType = 2 });
                    ReturnJumpBone1(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 12, () =>
                {
                    CreateEntity(new Boneslab(90 * rand1, 24, 1, bpm) { ColorType = 2 });
                    ReturnJumpBone2(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand2, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 24, () =>
                {
                    CreateEntity(new Boneslab(90 * rand2, 12, 1, bpm) { ColorType = 2 });
                    ReturnJumpBone1(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 28, () =>
                {
                    CreateEntity(new Boneslab(90 * rand2, 24, 1, bpm) { ColorType = 2 });
                    ReturnJumpBone2(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand3, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 40, () =>
                {
                    CreateEntity(new Boneslab(90 * rand3, 12, 1, bpm) { ColorType = 2 });
                    ReturnJumpBone1(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 44, () =>
                {
                    CreateEntity(new Boneslab(90 * rand3, 24, 1, bpm) { ColorType = 2 });
                    ReturnJumpBone2(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand4, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 56, () =>
                {
                    CreateEntity(new Boneslab(90 * rand4, 12, 1, bpm) { ColorType = 2 });
                    ReturnJumpBone1(rand4);
                }));
                AddInstance(new InstantEvent(bpm * 60, () =>
                {
                    CreateEntity(new Boneslab(90 * rand4, 24, 1, bpm) { ColorType = 2 });
                    ReturnJumpBone2(rand4);
                }));
            }
            public static void Intro7()
            {

                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                CreateEntity(new DownBonesea(58, bpm * 4, 64, false, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(58, bpm * 4, 64, true, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new sDownBonesea(58, bpm * 4, 64, false, 2.7f, bpm * 64, 2) { tags = new string[] { "a" } });
                CreateEntity(new sUpBonesea(58, bpm * 4, 64, true, 2.7f, bpm * 64, 2) { tags = new string[] { "a" } });
                AddInstance(new TimeRangedEvent(1, 1, () =>
                {
                    DownBone[] down = GetAll<DownBone>("a");
                    UpBone[] up = GetAll<UpBone>("a");

                    float speed = 1;
                    AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                    {
                        for (int a = 0; a < down.Length; a++)
                        {
                            int b = a;
                            down[b].MissionLength = 128 / 2 + Sin(speed) * 10 - 14;
                            up[b].MissionLength = 128 - down[b].MissionLength - 14;
                        }
                    }));
                    AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                    {
                        speed += 360 / bpm / 16;
                    }));
                }));
                for (int a = 0; a < 58; a++)
                {
                    AddInstance(new InstantEvent(bpm * 4 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 2, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 3, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro8()
            {
                SetSoul(1);
                TP();
                SetBox(240, 84, 84);
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    SetSoul(1);
                }));
            }
            public static void Intro9()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0","/","/","/", "/","/","/","/",
                "$2","/","/","/", "/","/","/","/",
                "$0","/","/","/", "$2","/","/","/",
                "$0","/","/","/", "$2","/","/","/",

            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 3f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro10()
            {
                float beat = 0;
                string[] arrow =
                    {
                "/","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "+11","/","/","/",
                "+11","/","/","/", "+11","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+11","/", "-10","/","+11","/",
                "-1","/","+11","/", "-10","/","+11","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "R1","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",

                "R0","/","/","/", "+1","/","/","/",
                "+1","/","/","/", "+1","/","/","/",
                "+11","/","/","/", "+11","/","/","/",
                "+11","/","/","/", "+11","/","/","/",

                "G03","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",
                "R(R1)","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",

                "R","/","+0","/", "+0","/","+0","/",
                "R(R1)","/","/","/", "R(R1)","/","/","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R(R1)","/","/","/", "R(R1)","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+11","/", "-10","/","+11","/",
                "-1","/","+11","/", "-10","/","+11","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "R1","/","+01","/",
                "R(R1)","/","/","/", "/","/","/","/",
                "R(R1)","/","/","/", "/","/","/","/",

                "R(R1)","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "R1","/","+01","/",
                "R","/","+0","/", "R1","/","+01","/",

                "R(R1)","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "R1","/","+01","/",
                "R","/","+0","/", "R1","/","+01","/",

                "T01","/","/","/", "/","/","/","/",
                "/","/","/","/", "T01","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+21)","/","/","/", "/","/","/","/",

                "R(+21)"
            };
                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, Rand(0, 3), 1, (arrow[i][2] - '0') * greencount));
                        CreateGB(new GreenSoulGB(beat + bpm * 16, LastRand + 2, 0, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro11()
            {
                CreateBone(new CentreCircleBone(240, -3f, 160 - 80, bpm * 16 * 8));
                CreateBone(new CentreCircleBone(240 + 90, -3, 160 - 80, bpm * 16 * 8));
                for (int a = 0; a < 4; a++)
                {
                    CreateBone(new SideCircleBone(240 + 90 * a + 45, 2.8f, 50, bpm * 16 * 8));
                    CreateBone(new SideCircleBone(240 + 90 * a, 2.8f, 50, bpm * 16 * 8));
                }
                AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(240 + 360 / 12 * a, 2.8f, 40, bpm * 16 * 8 - bpm * 8));
                    CreateBone(new CentreCircleBone(30, 2, 80 + 16 + 5, bpm * 16 * 8 - bpm * 8));
                    CreateBone(new CentreCircleBone(90, -2, 60 + 17 + 5, bpm * 16 * 8 - bpm * 8) { ColorType = 1 });
                }));
                /*AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    SetSoul(0);
                    float beat = 0;
                    string[] arrow =
                        {
            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "$0","/","$0","/",
            "$0","/","/","/", "/","/","/","/",
        };
                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        int x = i;
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'T')
                        {
                            beat += bpm * 4f;
                            instance.CreateArrows(beat + bpm * 16, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 1.333f, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 2.666f, 5.5f, "$0");
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            beat += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] != "/")
                        {
                            AddInstance(new InstantEvent(beat, () =>
                            {
                                PlaySound(Sounds.pierce, 0.75f);
                                CreateEntity(new RotBone(System.MathF.Sqrt(160 * 160 * 2) / 2 - 5, 2.8f, Rand(40, 50), true, Rand(0, 3)));
                                beat += bpm * 0.5f;
                            }));
                        }
                    }
                }));测试卡点*/
            }
            public static void Intro12()
            {
                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                /*for(int a=0;a<8;a++)
                {
                    AddInstance(new InstantEvent(bpm * 16*a, () =>
                    {
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                    }));
                }*/
                CreateEntity(new DownBonesea(58, bpm * 4, 64, false, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(58, bpm * 4, 64, true, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new sDownBonesea(58, bpm * 4, 64, false, 2.7f, bpm * 64, 2) { tags = new string[] { "a" } });
                CreateEntity(new sUpBonesea(58, bpm * 4, 64, true, 2.7f, bpm * 64, 2) { tags = new string[] { "a" } });
                AddInstance(new TimeRangedEvent(1, 1, () =>
                {
                    DownBone[] down = GetAll<DownBone>("a");
                    UpBone[] up = GetAll<UpBone>("a");

                    float speed = 1;
                    AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                    {
                        for (int a = 0; a < down.Length; a++)
                        {
                            int b = a;
                            down[b].MissionLength = 128 / 2 + Sin(speed) * 10 - 14;
                            up[b].MissionLength = 128 - down[b].MissionLength - 14;
                        }
                    }));
                    AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                    {
                        speed += 360 / bpm / 16;
                    }));
                }));
                for (int a = 0; a < 58; a++)
                {
                    AddInstance(new InstantEvent(bpm * 4 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 2, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 3, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro13()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0","/","+0","/", "+0","/","+0","/",
                "$0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",

                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",
                "+0","/","+0","/", "+0","/","+0","/",
            };

                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 1, (arrow[i][2] - '0') * greencount));
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 0, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
                float beat1 = 0;
                string[] arrow1 =
                    {
                "$01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "$0($21)"
            };
                for (int i = 0; i < arrow1.Length; i++)
                {
                    if (arrow1[i] == "/") beat1 += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat1, 6f, arrow1[i]);
                        beat1 += bpm * 0.5f;
                    }
                }
            }
        }
        public static class NormalBarrage
        {
            public class sDownBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                float time = 0;
                public bool marcksore;
                private int appearTime;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public class sUpBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                int appearTime;
                float time = 0;
                public bool marcksore;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public static void ReturnJumpBone1(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
            }
            public static void ReturnJumpBone2(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(true, 4.4f, 26));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new UpBone(false, 4.4f, 26));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(true, 4.4f, 26));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new LeftBone(false, 4.4f, 26));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(true, 4.4f, 26));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new DownBone(false, 4.4f, 26));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(true, 4.4f, 26));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new RightBone(false, 4.4f, 26));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
            }
            public static void SinBone1(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone2(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone3(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone4(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void CrossBone(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void CrossBone2(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void Intro0()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)","/","+0","/",    "+0","/","/","/",
                "+0","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "R","/","/","/",    "R","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "$0","/","$1","/",    "$2","/","$3","/",
                "$01","/","/","/",    "$11","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "$0","/","$1","/",    "$2","/","$3","/",
                "$01","/","/","/",    "$11","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "+2","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro1()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$01($01)","/","+01","/",    "+01","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",

                "R1","/","+01","/",    "+01","/","/","/",
                "R1","/","/","/",    "R1","/","/","/",
                "$01","/","$11","/",    "$21","/","$31","/",
                "$0","/","/","/",    "$1","/","/","/",

                "R1","/","+01","/",    "+01","/","/","/",
                "R1","/","/","/",    "R1","/","/","/",
                "$01","/","$11","/",    "$21","/","$31","/",
                "$0","/","/","/",    "$1","/","/","/",

                "R1","/","+01","/",    "+01","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",
                "R1","/","/","/",    "+21","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro2()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)($21)($21)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",

                "R(+01)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",

                "R(+01)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",

                "R(+01)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        CreateArrow(beat + 16 * bpm, Rand(0, 3), 6f, 0, 0);
                        CreateArrow(beat + 16 * bpm, LastRand, 6f, 1, 0);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro3()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$01($01)($2)($2)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",

                "R(+01)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",

                "R(+01)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "R(+01)","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",

                "R(+01)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R(+01)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        CreateArrow(beat + 16 * bpm, Rand(0, 3), 6f, 0, 0);
                        CreateArrow(beat + 16 * bpm, LastRand, 6f, 1, 0);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro4()
            {
                float beat = 0;
                string[] arrow =
                    {
                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro5()
            {
                float beat = 0;
                string[] arrow =
                    {
                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G11","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro6()
            {
                int rand1 = Rand(0, 3);
                int rand2 = Rand(0, 3);
                int rand3 = Rand(0, 3);
                int rand4 = Rand(0, 3);
                SetBox(240, 128, 128);
                SetSoul(2);
                AddInstance(new InstantEvent(0.5f, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand1, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 8, () =>
                {
                    ReturnJumpBone1(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 12, () =>
                {
                    ReturnJumpBone1(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand2, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 24, () =>
                {
                    ReturnJumpBone1(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 28, () =>
                {
                    ReturnJumpBone1(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand3, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 40, () =>
                {
                    ReturnJumpBone1(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 44, () =>
                {
                    ReturnJumpBone1(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand4, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 56, () =>
                {
                    ReturnJumpBone1(rand4);
                }));
                AddInstance(new InstantEvent(bpm * 60, () =>
                {
                    ReturnJumpBone1(rand4);
                }));
            }
            public static void Intro7()
            {

                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                CreateEntity(new DownBonesea(58, bpm * 4, 64 - 6, false, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(58, bpm * 4, 64 - 6, true, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new sDownBonesea(58, bpm * 4, 64 - 18, false, 2.7f, bpm * 64, 2) { tags = new string[] { "a" } });
                CreateEntity(new sUpBonesea(58, bpm * 4, 64 - 18, true, 2.7f, bpm * 64, 2) { tags = new string[] { "a" } });
                AddInstance(new TimeRangedEvent(1, 1, () =>
                {
                    DownBone[] down = GetAll<DownBone>("a");
                    UpBone[] up = GetAll<UpBone>("a");
                }));
                for (int a = 0; a < 58; a++)
                {
                    AddInstance(new InstantEvent(bpm * 4 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 2, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 3, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro8()
            {
                SetSoul(1);
                TP();
                SetBox(240, 84, 84);
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    SetSoul(1);
                }));
            }
            public static void Intro9()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0","/","/","/", "/","/","/","/",
                "$2","/","/","/", "/","/","/","/",
                "$0","/","/","/", "$2","/","/","/",
                "$0","/","/","/", "$2","/","/","/",

            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 3f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro10()
            {
                float beat = 0;
                string[] arrow =
                    {
                "/","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",

                "G11","/","/","/", "/","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "R0","/","/","/",
                "R0","/","/","/", "+10","/","/","/",
                "+10","/","/","/", "+10","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+11","/", "-10","/","+11","/",
                "-1","/","+11","/", "-10","/","+11","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "R0","/","/","/",
                "R0","/","/","/", "R0","/","/","/",
                "R0","/","/","/", "R0","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "R0","/","/","/", "R0","/","/","/",
                "R0","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",

                "R","/","/","/", "+1","/","/","/",
                "+1","/","/","/", "+1","/","/","/",
                "+1","/","/","/", "+1","/","/","/",
                "+1","/","/","/", "+1","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",

                "(R)(+01)","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "(R)(+01)","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",

                "R","/","+0","/", "R","/","+0","/",
                "(R)(+01)","/","/","/", "(R)(+01)","/","/","/",
                "R","/","+0","/", "R","/","+0","/",
                "(R)(+01)","/","/","/", "(R)(+01)","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+11","/", "-10","/","+11","/",
                "-1","/","+11","/", "-10","/","+11","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "R","/","R","/",
                "(R)(+01)","/","/","/", "/","/","/","/",
                "(R)(+01)","/","/","/", "/","/","/","/",

                "(R)(+01)","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "(R)(+01)","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "R1","/","+01","/",
                "R","/","+0","/", "R1","/","+01","/",

                "T01","/","/","/", "/","/","/","/",
                "/","/","/","/", "T01","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "(R)(+01)","/","/","/", "/","/","/","/",

                "(R)(+01)"
            };
                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, Rand(0, 3), 1, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro11()
            {
                CreateBone(new CentreCircleBone(240, -3f, 160 - 60, bpm * 16 * 8));
                CreateBone(new CentreCircleBone(240 + 90, -3, 160 - 60, bpm * 16 * 8));
                for (int a = 0; a < 4; a++)
                {
                    CreateBone(new SideCircleBone(240 + 90 * a + 45, 2.8f, 30, bpm * 16 * 8));
                    CreateBone(new SideCircleBone(240 + 90 * a, 2.8f, 30, bpm * 16 * 8));
                }
                AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(240 + 360 / 12 * a, 2.8f, 30, bpm * 16 * 8 - bpm * 8));
                    CreateBone(new CentreCircleBone(30, 1.8f, 80 + 16 + 5 + 10, bpm * 16 * 8 - bpm * 8));
                    CreateBone(new CentreCircleBone(90, -1.8f, 60 + 17 + 5 + 10, bpm * 16 * 8 - bpm * 8) { ColorType = 1 });
                }));
                /*AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    SetSoul(0);
                    float beat = 0;
                    string[] arrow =
                        {
            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "$0","/","$0","/",
            "$0","/","/","/", "/","/","/","/",
        };
                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        int x = i;
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'T')
                        {
                            beat += bpm * 4f;
                            instance.CreateArrows(beat + bpm * 16, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 1.333f, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 2.666f, 5.5f, "$0");
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            beat += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] != "/")
                        {
                            AddInstance(new InstantEvent(beat, () =>
                            {
                                PlaySound(Sounds.pierce, 0.75f);
                                CreateEntity(new RotBone(System.MathF.Sqrt(160 * 160 * 2) / 2 - 5, 2.8f, Rand(40, 50), true, Rand(0, 3)));
                                beat += bpm * 0.5f;
                            }));
                        }
                    }
                }));测试卡点*/
            }
            public static void Intro12()
            {
                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                /*for(int a=0;a<8;a++)
                {
                    AddInstance(new InstantEvent(bpm * 16*a, () =>
                    {
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                    }));
                }*/
                CreateEntity(new DownBonesea(58, bpm * 4, 64 - 6, false, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(58, bpm * 4, 64 - 6, true, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new sDownBonesea(58, bpm * 4, 64 - 18, false, 2.7f, bpm * 64, 2) { tags = new string[] { "a" } });
                CreateEntity(new sUpBonesea(58, bpm * 4, 64 - 18, true, 2.7f, bpm * 64, 2) { tags = new string[] { "a" } });
                for (int a = 0; a < 58; a++)
                {
                    AddInstance(new InstantEvent(bpm * 4 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 2, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 3, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro13()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0","/","+0","/", "$0","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

               "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "/","/","/","/",
            };

                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 1, (arrow[i][2] - '0') * greencount));
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 0, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
        }
        public static class EasyBarrage
        {
            public class sDownBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                float time = 0;
                public bool marcksore;
                private int appearTime;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public class sUpBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                int appearTime;
                float time = 0;
                public bool marcksore;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public static void ReturnJumpBone1(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
            }
            public static void ReturnJumpBone2(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(true, 4.4f, 26));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new UpBone(false, 4.4f, 26));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(true, 4.4f, 26));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new LeftBone(false, 4.4f, 26));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(true, 4.4f, 26));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new DownBone(false, 4.4f, 26));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(true, 4.4f, 26));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new RightBone(false, 4.4f, 26));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
            }
            public static void SinBone1(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone2(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone3(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone4(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void CrossBone(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void CrossBone2(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void Intro0()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)","/","+0","/",    "+0","/","/","/",
                "+0","/","/","/",    "+2","/","/","/",
                "+2","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "R","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "$0","/","$0","/",    "$2","/","$2","/",
                "$0","/","/","/",    "$2","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "$0","/","$0","/",    "$2","/","$2","/",
                "$0","/","/","/",    "$2","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro1()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)","/","+0","/",    "+0","/","/","/",
                "+0","/","/","/",    "+2","/","/","/",
                "+2","/","/","/",    "+2","/","/","/",
                "R","/","/","/",    "R","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "$0","/","$0","/",    "$2","/","$2","/",
                "$0","/","/","/",    "$2","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "R","/","/","/",
                "$0","/","$0","/",    "$2","/","$2","/",
                "$0","/","/","/",    "$2","/","/","/",

                "R","/","+0","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro2()
            {
                float beat = 0;
                string[] arrow =
                    {
                "($2)($2)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro3()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$01($01)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro4()
            {
                float beat = 0;
                string[] arrow =
                    {
                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro5()
            {
                float beat = 0;
                string[] arrow =
                    {
                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro6()
            {
                int rand1 = Rand(0, 3);
                int rand2 = Rand(0, 3);
                int rand3 = Rand(0, 3);
                int rand4 = Rand(0, 3);
                SetBox(240, 128, 128);
                SetSoul(2);
                AddInstance(new InstantEvent(0.5f, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand1, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 8, () =>
                {
                    ReturnJumpBone1(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 12, () =>
                {
                    ReturnJumpBone1(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand2, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 24, () =>
                {
                    ReturnJumpBone1(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 28, () =>
                {
                    ReturnJumpBone1(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand3, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 40, () =>
                {
                    ReturnJumpBone1(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 44, () =>
                {
                    ReturnJumpBone1(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand4, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 56, () =>
                {
                    ReturnJumpBone1(rand4);
                }));
                AddInstance(new InstantEvent(bpm * 60, () =>
                {
                    ReturnJumpBone1(rand4);
                }));
            }
            public static void Intro7()
            {

                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                CreateEntity(new DownBonesea(58, bpm * 4, 64 - 6, false, 2.6f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(58, bpm * 4, 64 - 6, true, 2.6f, bpm * 64) { tags = new string[] { "a" } });
                AddInstance(new TimeRangedEvent(1, 1, () =>
                {
                    DownBone[] down = GetAll<DownBone>("a");
                    UpBone[] up = GetAll<UpBone>("a");
                }));
                for (int a = 0; a < 58; a++)
                {
                    AddInstance(new InstantEvent(bpm * 4 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 2, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro8()
            {
                SetSoul(1);
                TP();
                SetBox(240, 84, 84);
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    SetSoul(1);
                }));
            }
            public static void Intro9()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0","/","/","/", "/","/","/","/",
                "$2","/","/","/", "/","/","/","/",
                "$0","/","/","/", "$2","/","/","/",
                "$0","/","/","/", "$2","/","/","/",

            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 3f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro10()
            {
                float beat = 0;
                string[] arrow =
                    {
                "/","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R0","/","/","/", "+001","/","/","/",
                "+001","/","/","/", "+001","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "+0","/","/","/",
                "+1","/","/","/", "+0","/","/","/",
                "+1","/","/","/", "+0","/","/","/",
                "+1","/","/","/", "+0","/","/","/",

                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "G01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",

                "(R)(+0)","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "(R)(+0)","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",

                "R","/","+0","/", "+0","/","+0","/",
                "(R)(+0)","/","/","/", "(R)(+0)","/","/","/",
                "R","/","+0","/", "+0","/","+0","/",
                "(R)(+0)","/","/","/", "(R)(+0)","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "+0","/","+0","/",
                "(R)(+0)","/","/","/", "/","/","/","/",
                "(R)(+0)","/","/","/", "/","/","/","/",

                "(R)(+0)","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",

                "(R)(+0)","/","/","/", "R","/","/","/",
                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "R","/","+0","/",
                "R","/","+0","/", "R","/","+0","/",

                "T01","/","/","/", "/","/","/","/",
                "/","/","/","/", "T01","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "(R)(+01)","/","/","/", "/","/","/","/",

                "(R)(+01)"
            };
                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, Rand(0, 3), 1, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro11()
            {
                CreateBone(new CentreCircleBone(240, -3f, 160 - 60, bpm * 16 * 8));
                for (int a = 0; a < 4; a++)
                {
                    CreateBone(new SideCircleBone(240 + 90 * a + 45, 2.8f, 30, bpm * 16 * 8));
                    CreateBone(new SideCircleBone(240 + 90 * a, 2.8f, 30, bpm * 16 * 8));
                }
                AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(240 + 360 / 12 * a, 2.8f, 30, bpm * 16 * 8 - bpm * 8));
                    CreateBone(new CentreCircleBone(30, 2.2f, 80 + 16 + 5 + 10, bpm * 16 * 8 - bpm * 8));
                }));
                /*AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    SetSoul(0);
                    float beat = 0;
                    string[] arrow =
                        {
            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "$0","/","$0","/",
            "$0","/","/","/", "/","/","/","/",
        };
                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        int x = i;
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'T')
                        {
                            beat += bpm * 4f;
                            instance.CreateArrows(beat + bpm * 16, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 1.333f, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 2.666f, 5.5f, "$0");
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            beat += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] != "/")
                        {
                            AddInstance(new InstantEvent(beat, () =>
                            {
                                PlaySound(Sounds.pierce, 0.75f);
                                CreateEntity(new RotBone(System.MathF.Sqrt(160 * 160 * 2) / 2 - 5, 2.8f, Rand(40, 50), true, Rand(0, 3)));
                                beat += bpm * 0.5f;
                            }));
                        }
                    }
                }));测试卡点*/
            }
            public static void Intro12()
            {
                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                /*for(int a=0;a<8;a++)
                {
                    AddInstance(new InstantEvent(bpm * 16*a, () =>
                    {
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                    }));
                }*/
                CreateEntity(new DownBonesea(58, bpm * 4, 64 - 6, false, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(58, bpm * 4, 64 - 6, true, 2.7f, bpm * 64) { tags = new string[] { "a" } });
                for (int a = 0; a < 58; a++)
                {
                    AddInstance(new InstantEvent(bpm * 4 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 4 * a + bpm * 2, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro13()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$01","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "R1","/","/","/",
                "R1","/","/","/", "/","/","/","/",

                "R1","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",
                "R1","/","/","/", "/","/","/","/",


            };

                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 1, (arrow[i][2] - '0') * greencount));
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 0, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
        }
        public static class NoobBarrage
        {
            public class sDownBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sDownBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                float time = 0;
                public bool marcksore;
                private int appearTime;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                DownBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public class sUpBonesea : Entity
            {
                public float duration = 0;
                public float quantity = 0;
                public float distance = 0;
                public float length = 0;
                public float speed = 0;
                public int colortype = 0;
                public bool way = true;
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                }
                public sUpBonesea(float quantity, float distance, float length, bool way, float speed, float duration, int colortype)
                {
                    this.duration = duration;
                    this.quantity = quantity;
                    this.distance = distance;
                    this.length = length;
                    this.speed = speed;
                    this.way = way;
                    this.colortype = colortype;
                }
                public override void Draw()
                {

                }
                int appearTime;
                float time = 0;
                public bool marcksore;
                public string[] tags = { "noany" };
                public override void Update()
                {
                    appearTime += 1;
                    if (appearTime == 1)
                    {
                        if (way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(false, BoxStates.Left - b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);
                            }
                        if (!way)
                            for (int a = 0; a < quantity; a++)
                            {
                                float b = (a + 0.5f) * distance * speed;
                                UpBone bone1 = new(true, BoxStates.Right + b, speed, length) { ColorType = colortype, MarkScore = marcksore, Tags = tags };
                                CreateBone(bone1);

                            }
                    }
                    if (time >= duration)
                    {
                        Dispose();
                    }
                    time++;
                }
            }
            public static void ReturnJumpBone1(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height / 2 - 30));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height / 2 - 30));
                }
            }
            public static void ReturnJumpBone2(int org)
            {
                PlaySound(Sounds.pierce);
                if (org == 0)
                {

                    CreateBone(new DownBone(true, 4.4f, 26));
                    CreateBone(new DownBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new UpBone(false, 4.4f, 26));
                    CreateBone(new UpBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 1)
                {
                    CreateBone(new RightBone(true, 4.4f, 26));
                    CreateBone(new RightBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new LeftBone(false, 4.4f, 26));
                    CreateBone(new LeftBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 2)
                {
                    CreateBone(new UpBone(true, 4.4f, 26));
                    CreateBone(new UpBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new DownBone(false, 4.4f, 26));
                    CreateBone(new DownBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
                if (org == 3)
                {
                    CreateBone(new LeftBone(true, 4.4f, 26));
                    CreateBone(new LeftBone(false, 4.4f, BoxStates.Height - 26 - 56));
                    CreateBone(new RightBone(false, 4.4f, 26));
                    CreateBone(new RightBone(true, 4.4f, BoxStates.Height - 26 - 56));
                }
            }
            public static void SinBone1(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone2(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 1 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.MissionLength = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone3(float start)
            {
                float speed = start;
                DownBone bone1 = new(false, 2.9f, Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 + Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void SinBone4(float start)
            {
                float speed = start;
                UpBone bone1 = new(true, 2.9f, -Sin(speed) * BoxStates.Height / 2) { ColorType = 2 };
                CreateBone(bone1);
                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                {
                    bone1.Length = BoxStates.Height / 2 - Sin(speed) * (BoxStates.Height / 2 - 45);
                    speed += 3.2f;
                }));
            }
            public static void CrossBone(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Left - length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(BoxStates.Right + length / 2, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { -speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void CrossBone2(Vector2 speed, float length, int color)
            {
                int randrot = Rand(0, 360);
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Up - length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot },
                    ColorType = color
                });
                CreateBone(new CustomBone(new(Heart.Centre.X, BoxStates.Down + length / 2), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    PositionRouteParam = new float[] { speed.X, -speed.Y },
                    LengthRouteParam = new float[] { length, bpm * 32 },
                    RotationRouteParam = new float[] { 4, randrot + 90 },
                    ColorType = color
                });
            }
            public static void Intro0()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",

                "$0($0)","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",
                "$0","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$2","/","/","/",

                "R","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",
                "$0","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$2","/","/","/",

                "R","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro1()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",

                "$0($0)","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",
                "$0","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$2","/","/","/",

                "R","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",
                "$0","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$2","/","/","/",

                "R","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",
                "R","/","/","/",    "+0","/","/","/",
                "+0","/","/","/",    "+0","/","/","/",

            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro2()
            {
                float beat = 0;
                string[] arrow =
                    {
                "($2)($2)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro3()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0($0)","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/") beat += bpm * 0.5f;
                    else
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro4()
            {
                float beat = 0;
                string[] arrow =
                    {
                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro5()
            {
                float beat = 0;
                string[] arrow =
                    {
                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro6()
            {
                int rand1 = Rand(0, 3);
                int rand2 = Rand(0, 3);
                int rand3 = Rand(0, 3);
                int rand4 = Rand(0, 3);
                SetBox(240, 128, 128);
                SetSoul(2);
                AddInstance(new InstantEvent(0.5f, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand1, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 8, () =>
                {
                    ReturnJumpBone1(rand1);
                }));
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand2, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 24, () =>
                {
                    ReturnJumpBone1(rand2);
                }));
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand3, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 40, () =>
                {
                    ReturnJumpBone1(rand3);
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    PlaySound(Sounds.Ding);
                    Heart.GiveForce(90 * rand4, 3.5f);
                }));
                AddInstance(new InstantEvent(bpm * 56, () =>
                {
                    ReturnJumpBone1(rand4);
                }));
            }
            public static void Intro7()
            {

                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                CreateEntity(new DownBonesea(29, bpm * 8, 64 - 9, false, 2.5f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(29, bpm * 8, 64 - 9, true, 2.5f, bpm * 64) { tags = new string[] { "a" } });
                AddInstance(new TimeRangedEvent(1, 1, () =>
                {
                    DownBone[] down = GetAll<DownBone>("a");
                    UpBone[] up = GetAll<UpBone>("a");
                }));
                for (int a = 0; a < 29; a++)
                {
                    AddInstance(new InstantEvent(bpm * 8 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro8()
            {
                SetSoul(1);
                TP();
                SetBox(240, 84, 84);
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    SetSoul(1);
                }));
            }
            public static void Intro9()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$1","/","/","/", "/","/","/","/",
                "$1","/","/","/", "/","/","/","/",
                "$1","/","/","/", "$1","/","/","/",
                "$1","/","/","/", "$1","/","/","/",

            };
                float greencount = bpm * 4;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 3f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro10()
            {
                float beat = 0;
                string[] arrow =
                    {
                "/","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "+0","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "+0","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R0","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",

                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "R","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "+0","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",

                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",
                "+1","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",

                "(R)(+0)","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",
                "(R)(+0)","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "+0","/","/","/",
                "(+0)(+0)","/","/","/", "(+0)(+0)","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "(R)(+0)","/","/","/", "(+0)(+0)","/","/","/",

                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",

                "R","/","/","/", "R","/","/","/",
                "R","/","+0","/", "+0","/","+0","/",
                "(R)(+0)","/","/","/", "/","/","/","/",
                "(+0)(+0)","/","/","/", "/","/","/","/",

                "(R)(+0)","/","/","/", "+0","/","/","/",
                "+0","/","/","/", "+0","/","/","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",

                "(R)(+0)","/","/","/", "+0","/","/","/",
                "R","/","/","/", "+0","/","/","/",
                "R","/","+0","/", "+0","/","+0","/",
                "R","/","+0","/", "+0","/","+0","/",

                "T01","/","/","/", "/","/","/","/",
                "/","/","/","/", "T01","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "(R)(+01)","/","/","/", "/","/","/","/",

                "(+0)(+01)"
            };
                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, 3, 0, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
            public static void Intro11()
            {
                CreateBone(new CentreCircleBone(240, -2f, 160 - 60, bpm * 16 * 8));
                for (int a = 0; a < 4; a++)
                {
                    CreateBone(new SideCircleBone(240 + 90 * a + 45, 2.8f, 30, bpm * 16 * 8));
                    CreateBone(new SideCircleBone(240 + 90 * a, 2.8f, 30, bpm * 16 * 8));
                }
                AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(240 + 360 / 12 * a, 2.8f, 30, bpm * 16 * 8 - bpm * 8));
                    AddInstance(new InstantEvent(bpm * 4, () => { CreateBone(new CentreCircleBone(30, 2f, 80 + 16 + 5 + 10, bpm * 16 * 8 - bpm * 12)); }));
                }));
                /*AddInstance(new InstantEvent(bpm * 16 * 8, () =>
                {
                    SetSoul(0);
                    float beat = 0;
                    string[] arrow =
                        {
            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

             "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "/","/","$0","/", "/","/","/","/",
            "/","/","/","/", "$0","/","/","/",
            "/","/","$0","/", "/","/","$0","/",

            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "/","/","/","/",
            "$0","/","/","/", "$0","/","$0","/",
            "$0","/","/","/", "/","/","/","/",
        };
                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        int x = i;
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'T')
                        {
                            beat += bpm * 4f;
                            instance.CreateArrows(beat + bpm * 16, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 1.333f, 5.5f, "$0");
                            instance.CreateArrows(beat + bpm * 16 + bpm * 2.666f, 5.5f, "$0");
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            beat += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] != "/")
                        {
                            AddInstance(new InstantEvent(beat, () =>
                            {
                                PlaySound(Sounds.pierce, 0.75f);
                                CreateEntity(new RotBone(System.MathF.Sqrt(160 * 160 * 2) / 2 - 5, 2.8f, Rand(40, 50), true, Rand(0, 3)));
                                beat += bpm * 0.5f;
                            }));
                        }
                    }
                }));测试卡点*/
            }
            public static void Intro12()
            {
                SetSoul(0);
                SetBox(280, 280, 128);
                Heart.GiveForce(0, 0);
                float x = 0;
                AddInstance(new TimeRangedEvent(bpm * 256, () =>
                {
                    x += 3.2f;
                }));
                /*for(int a=0;a<8;a++)
                {
                    AddInstance(new InstantEvent(bpm * 16*a, () =>
                    {
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                    }));
                }*/
                CreateEntity(new DownBonesea(29, bpm * 8, 64 - 9, false, 2.5f, bpm * 64) { tags = new string[] { "a" } });
                CreateEntity(new UpBonesea(29, bpm * 8, 64 - 9, true, 2.5f, bpm * 64) { tags = new string[] { "a" } });
                for (int a = 0; a < 29; a++)
                {
                    AddInstance(new InstantEvent(bpm * 8 * a, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(bpm * 8 * a + bpm * 4, () =>
                    {
                        PlaySound(Sounds.pierce);
                    }));
                }
            }
            public static void Intro13()
            {
                float beat = 0;
                string[] arrow =
                    {
                "$0","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",

                "R","/","/","/", "/","/","/","/",
                "/","/","/","/", "/","/","/","/",
                "R","/","/","/", "/","/","/","/",
                "+0","/","/","/", "/","/","/","/",


            };

                float greencount = bpm * 4 - 5;
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] == "/")
                    {
                        beat += bpm * 0.5f;
                    }
                    else if (arrow[i][0] == 'T')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 1, (arrow[i][2] - '0') * greencount));
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", 0, (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i][0] == 'G')
                    {
                        beat += bpm * 0.5f;
                        CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                    }
                    else if (arrow[i] != "/")
                    {
                        instance.CreateArrows(bpm * 16 + beat, 5.5f, arrow[i]);
                        beat += bpm * 0.5f;
                    }
                }
            }
        }
        public static class Effects
        {
            public static void Intro0_Ef()
            {
                SizeExpand(10, bpm * 8);
                RotateTo(5, bpm * 2);
                AddInstance(new InstantEvent(bpm * 2 - 1, () =>
                {
                    RotateTo(-5, bpm * 2);
                }));
                AddInstance(new InstantEvent(bpm * 4, () =>
                {
                    RotateTo(0, bpm * 2);
                }));
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    NormalLine line1 = new(Rand(120, 300), 0, LastRand, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line2 = new(LastRand - 6, 0, LastRand - 6, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line3 = new(LastRand + 6, 0, LastRand + 6, 480, 700, 0, Color.Red * 0.8f);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, () =>
                    {
                        line1.alpha = Sin(speed * 360) * 1;
                        line1.x1 += speed * speed * 10;
                        line1.x2 += speed * speed * 10;
                        speed += 1 / bpm / 16;
                        line2.alpha = Sin(speed * 360) * 1f - 0.7f;
                        line2.x1 += speed * speed * 7.5f;
                        line2.x2 += speed * speed * 7.5f;
                        line3.alpha = Sin(speed * 360) * 1f - 0.4f;
                        line3.x1 += speed * speed * 9.5f;
                        line3.x2 += speed * speed * 9.5f;
                        ScreenScale = 1 - Sin(speed * 360 / 2) * 0.06f;
                    }));
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    NormalLine line1 = new(Rand(120 + 300, 300 + 300), 0, LastRand, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line2 = new(LastRand - 6, 0, LastRand - 6, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line3 = new(LastRand + 6, 0, LastRand + 6, 480, 700, 0, Color.Red * 0.8f);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, () =>
                    {
                        speed += 1 / bpm / 16;
                        line1.alpha = Sin(speed * 360) * 1;
                        line1.x1 -= speed * speed * 10;
                        line1.x2 -= speed * speed * 10;
                        line2.alpha = Sin(speed * 360) * 1f - 0.7f;
                        line2.x1 -= speed * speed * 9.5f;
                        line2.x2 -= speed * speed * 9.5f;
                        line3.alpha = Sin(speed * 360) * 1f - 0.4f;
                        line3.x1 -= speed * speed * 7.5f;
                        line3.x2 -= speed * speed * 7.5f;
                        ScreenScale = 1 - Sin(speed * 360) * 0.06f;
                    }));
                }));
            }
            public static void Intro1_Ef()
            {
                Extends.DrawingUtil.Shock();
                SizeExpand(1.5f, 5);
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    NormalLine line1 = new(Rand(120, 300), 0, LastRand, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line2 = new(LastRand - 6, 0, LastRand - 6, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line3 = new(LastRand + 6, 0, LastRand + 6, 480, 700, 0, Color.Red * 0.8f);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, () =>
                    {
                        line1.alpha = Sin(speed * 360) * 1;
                        line1.x1 += speed * speed * 10;
                        line1.x2 += speed * speed * 10;
                        speed += 1 / bpm / 16;
                        line2.alpha = Sin(speed * 360) * 1f - 0.7f;
                        line2.x1 += speed * speed * 7.5f;
                        line2.x2 += speed * speed * 7.5f;
                        line3.alpha = Sin(speed * 360) * 1f - 0.4f;
                        line3.x1 += speed * speed * 9.5f;
                        line3.x2 += speed * speed * 9.5f;
                        ScreenScale = 1 - Sin(speed * 360 / 2) * 0.06f;
                    }));
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    NormalLine line1 = new(Rand(120 + 300, 300 + 300), 0, LastRand, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line2 = new(LastRand - 6, 0, LastRand - 6, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line3 = new(LastRand + 6, 0, LastRand + 6, 480, 700, 0, Color.Red * 0.8f);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, () =>
                    {
                        speed += 1 / bpm / 16;
                        line1.alpha = Sin(speed * 360) * 1;
                        line1.x1 -= speed * speed * 10;
                        line1.x2 -= speed * speed * 10;
                        line2.alpha = Sin(speed * 360) * 1f - 0.7f;
                        line2.x1 -= speed * speed * 9.5f;
                        line2.x2 -= speed * speed * 9.5f;
                        line3.alpha = Sin(speed * 360) * 1f - 0.4f;
                        line3.x1 -= speed * speed * 7.5f;
                        line3.x2 -= speed * speed * 7.5f;
                        ScreenScale = 1 - Sin(speed * 360) * 0.06f;
                    }));
                }));
            }
            public static void Intro2_Ef()
            {
                RotateTo(5, bpm * 8);
                AddInstance(new InstantEvent(bpm * 8, () =>
                {
                    RotateTo(-5, bpm * 4);
                }));
                AddInstance(new InstantEvent(bpm * 12, () =>
                {
                    RotateTo(5, bpm * 4);
                }));
                AddInstance(new InstantEvent(bpm * 16, () =>
                {
                    RotateTo(-5, bpm * 8);
                }));
                AddInstance(new InstantEvent(bpm * 24, () =>
                {
                    RotateTo(5, bpm * 4);
                }));
                AddInstance(new InstantEvent(bpm * 28, () =>
                {
                    RotateTo(-5, bpm * 4);
                }));
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    RotateTo(5, bpm * 8);
                }));
                AddInstance(new InstantEvent(bpm * 40, () =>
                {
                    RotateTo(-5, bpm * 4);
                }));
                AddInstance(new InstantEvent(bpm * 44, () =>
                {
                    RotateTo(5, bpm * 4);
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    RotateTo(-5, bpm * 8);
                }));
                AddInstance(new InstantEvent(bpm * 56, () =>
                {
                    RotateTo(5, bpm * 4);
                }));
                AddInstance(new InstantEvent(bpm * 60, () =>
                {
                    RotateTo(0, bpm * 4);
                }));
                AddInstance(new InstantEvent(bpm * 32, () =>
                {
                    NormalLine line1 = new(Rand(120, 300), 0, LastRand, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line2 = new(LastRand - 6, 0, LastRand - 6, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line3 = new(LastRand + 6, 0, LastRand + 6, 480, 700, 0, Color.Red * 0.8f);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, () =>
                    {
                        line1.alpha = Sin(speed * 360) * 1;
                        line1.x1 += speed * speed * 12;
                        line1.x2 += speed * speed * 12;
                        speed += 1 / bpm / 16;
                        line2.alpha = Sin(speed * 360) * 1f - 0.7f;
                        line2.x1 += speed * speed * 9.5f;
                        line2.x2 += speed * speed * 9.5f;
                        line3.alpha = Sin(speed * 360) * 1f - 0.4f;
                        line3.x1 += speed * speed * 11.5f;
                        line3.x2 += speed * speed * 11.5f;

                    }));
                }));
                AddInstance(new InstantEvent(bpm * 48, () =>
                {
                    NormalLine line1 = new(Rand(120 + 300, 300 + 300), 0, LastRand, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line2 = new(LastRand - 6, 0, LastRand - 6, 480, 700, 0, Color.Red * 0.8f);
                    NormalLine line3 = new(LastRand + 6, 0, LastRand + 6, 480, 700, 0, Color.Red * 0.8f);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, () =>
                    {
                        speed += 1 / bpm / 16;
                        line1.alpha = Sin(speed * 360) * 1;
                        line1.x1 -= speed * speed * 12;
                        line1.x2 -= speed * speed * 12;
                        line2.alpha = Sin(speed * 360) * 1f - 0.7f;
                        line2.x1 -= speed * speed * 11.5f;
                        line2.x2 -= speed * speed * 11.5f;
                        line3.alpha = Sin(speed * 360) * 1f - 0.4f;
                        line3.x1 -= speed * speed * 9.5f;
                        line3.x2 -= speed * speed * 9.5f;

                    }));
                }));

                AddInstance(new InstantEvent(bpm * 64, () =>
                {
                    if (linebool)
                    {
                        NormalLine line1 = new(Rand(120, 300), 0, LastRand, 480, 700, 0, Color.Red * 0.8f);
                        NormalLine line2 = new(LastRand - 6, 0, LastRand - 6, 480, 700, 0, Color.Red * 0.8f);
                        NormalLine line3 = new(LastRand + 6, 0, LastRand + 6, 480, 700, 0, Color.Red * 0.8f);
                        CreateEntity(line1);
                        CreateEntity(line2);
                        CreateEntity(line3);
                        float speed = 0;
                        AddInstance(new TimeRangedEvent(bpm * 16, () =>
                        {
                            line1.alpha = Sin(speed * 360) * 1;
                            line1.x1 += speed * speed * 10;
                            line1.x2 += speed * speed * 10;
                            speed += 1 / bpm / 16;
                            line2.alpha = Sin(speed * 360) * 1f - 0.7f;
                            line2.x1 += speed * speed * 7.5f;
                            line2.x2 += speed * speed * 7.5f;
                            line3.alpha = Sin(speed * 360) * 1f - 0.4f;
                            line3.x1 += speed * speed * 9.5f;
                            line3.x2 += speed * speed * 9.5f;

                        }));
                    }
                }));
                AddInstance(new InstantEvent(bpm * 80, () =>
                {
                    if (linebool)
                    {
                        NormalLine line1 = new(Rand(120 + 300, 300 + 300), 0, LastRand, 480, 700, 0, Color.Red * 0.8f);
                        NormalLine line2 = new(LastRand - 6, 0, LastRand - 6, 480, 700, 0, Color.Red * 0.8f);
                        NormalLine line3 = new(LastRand + 6, 0, LastRand + 6, 480, 700, 0, Color.Red * 0.8f);
                        CreateEntity(line1);
                        CreateEntity(line2);
                        CreateEntity(line3);
                        float speed = 0;
                        AddInstance(new TimeRangedEvent(bpm * 16, () =>
                        {
                            speed += 1 / bpm / 16;
                            line1.alpha = Sin(speed * 360) * 1;
                            line1.x1 -= speed * speed * 10;
                            line1.x2 -= speed * speed * 10;
                            line2.alpha = Sin(speed * 360) * 1f - 0.7f;
                            line2.x1 -= speed * speed * 9.5f;
                            line2.x2 -= speed * speed * 9.5f;
                            line3.alpha = Sin(speed * 360) * 1f - 0.4f;
                            line3.x1 -= speed * speed * 7.5f;
                            line3.x2 -= speed * speed * 7.5f;

                        }));
                    }
                }));

            }
            public static void Intro3_Ef()
            {
                for (int a = 1; a < 14; a++)
                {
                    AddInstance(new TimeRangedEvent(bpm * 16 * a - 1, bpm * 8 - 1, () =>
                    {
                        ScreenPositionDetla = new Vector2(-24 * 0.08f + ScreenPositionDetla.X * 0.92f, 0);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 * a - 1 + bpm * 8, bpm * 8 - 1, () =>
                    {
                        ScreenPositionDetla = new Vector2(24 * 0.08f + ScreenPositionDetla.X * 0.92f, 0);
                    }));
                }
                AddInstance(new TimeRangedEvent(bpm * 16 * 14, 60, () =>
                {
                    ScreenPositionDetla = new Vector2(0 * 0.1f + ScreenPositionDetla.X * 0.9f, 0);
                }));
                for (int a = 0; a < 16 * 14 / 2; a++)
                {
                    AddInstance(new InstantEvent(bpm * 2 * a, () =>
                    {
                        float speed = 0;
                        NormalLine line = new(640, 0, 640, 480, 360, 0, Color.Red * 0.8f);
                        CreateEntity(line);
                        AddInstance(new TimeRangedEvent(bpm * 16, () =>
                        {
                            speed += 1 / bpm / 4;
                            line.x1 -= speed * speed * 3.5f;
                            line.x2 -= speed * speed * 3.5f;
                            line.alpha = Sin(speed * 360 / 4) * 0.8f;
                        }));
                    }));
                    AddInstance(new InstantEvent(bpm * 2 * a + bpm, () =>
                    {
                        float speed = 0;
                        NormalLine line = new(0, 0, 0, 480, 360, 0, Color.Red * 0.8f);
                        CreateEntity(line);
                        AddInstance(new TimeRangedEvent(bpm * 16, () =>
                        {
                            speed += 1 / bpm / 4;
                            line.x1 += speed * speed * 3.5f;
                            line.x2 += speed * speed * 3.5f;
                            line.alpha = Sin(speed * 360 / 4) * 0.8f;
                        }));
                    }));
                }
                AddInstance(new InstantEvent(bpm * 2, () =>
                {
                    NormalLine line = new(BoxStates.Left - 8, 0, BoxStates.Left - 8, 480, (int)(bpm * 16 * 15), 0, Color.Yellow);
                    float count = 0;
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(bpm * 16 * 15, () =>
                    {
                        count += 1 / bpm / 8;
                        line.alpha = Sin(count * 360) * 1;
                    }));
                    NormalLine line1 = new(BoxStates.Right + 8, 0, BoxStates.Right + 8, 480, (int)(bpm * 16 * 15), 0, Color.Yellow);
                    float count1 = 0.5f;
                    CreateEntity(line1);
                    AddInstance(new TimeRangedEvent(bpm * 16 * 15, () =>
                    {
                        count1 += 1 / bpm / 8;
                        line1.alpha = Sin(count1 * 360) * 1;
                    }));
                }));
            }
            public static void Intro4_Ef()
            {
                AddInstance(new TimeRangedEvent(1, bpm * 16 - 12, () =>
                {
                    ScreenScale = 4.5f * 0.04f + ScreenScale * 0.96f;
                }));
                AddInstance(new TimeRangedEvent(bpm * 16 - 8, bpm * 16, () =>
                {
                    ScreenScale = 1 * 0.15f + ScreenScale * 0.85f;
                }));
            }
            public static void Intro5_Ef()
            {
                AddInstance(new TimeRangedEvent(bpm * 16 * 15, 1, () =>
                {
                    Extends.DrawingUtil.BlackScreen(bpm * 4, bpm * 2, bpm * 6);
                }));
                AddInstance(new TimeRangedEvent(bpm * 16 * 15, bpm * 6, () =>
                {
                    InstantSetBox(-90 * 0.06f + BoxStates.Centre.Y * 0.94f, 84, 84);
                    InstantTP(320, -90 * 0.06f + Heart.Centre.Y * 0.94f);
                }));
                AddInstance(new TimeRangedEvent(bpm * 16 * 15 + bpm * 6 + 1, 1, () =>
                {
                    InstantSetBox(480 + 200, 160, 160);
                    InstantTP(320, 480 + 200);
                    SetSoul(0);

                }));
                AddInstance(new TimeRangedEvent(bpm * 16 * 15 + bpm * 6 + 3, bpm * 8, () =>
                {
                    InstantSetBox(240 * 0.06f + BoxStates.Centre.Y * 0.94f, 160, 160);
                    InstantTP(320, 240 * 0.06f + Heart.Centre.Y * 0.94f);
                }));
            }
        }
        public void Extreme()
        {
            if (GametimeF == 0.5f)
            {
                ExtremeBarrage.Intro0();
                Effects.Intro0_Ef();
            }
            if (InBeat(64)) Effects.Intro0_Ef();
            if (InBeat(64 - 16)) ExtremeBarrage.Intro1();
            if (InBeat(128)) Effects.Intro1_Ef();
            if (InBeat(128 - 16)) ExtremeBarrage.Intro2();
            if (InBeat(192)) Effects.Intro1_Ef();
            if (InBeat(192 - 16)) ExtremeBarrage.Intro3();
            if (InBeat(256)) Effects.Intro2_Ef();
            if (InBeat(256 - 16)) ExtremeBarrage.Intro4();
            if (InBeat(320)) Effects.Intro2_Ef();
            if (InBeat(320 - 16)) ExtremeBarrage.Intro5();
            if (InBeat(384)) Effects.Intro2_Ef(); ;
            if (InBeat(384)) ExtremeBarrage.Intro6();
            if (InBeat(448)) Effects.Intro2_Ef(); ;
            if (InBeat(448)) ExtremeBarrage.Intro6();
            if (InBeat(510)) linebool = false;
            if (InBeat(512)) ExtremeBarrage.Intro7();
            if (InBeat(512)) Effects.Intro3_Ef();
            if (InBeat(512 + 16 * 15)) ExtremeBarrage.Intro8();
            if (InBeat(512 + 16 * 15 - 16)) ExtremeBarrage.Intro9();
            if (InBeat(512 + 16 * 15)) Effects.Intro4_Ef();
            if (InBeat(768 - 16)) ExtremeBarrage.Intro10();
            if (InBeat(768)) Effects.Intro5_Ef();
            if (InBeat(768 + 256)) ExtremeBarrage.Intro11();
            if (InBeat(768 + 512)) ExtremeBarrage.Intro12();
            if (InBeat(768 + 512)) Effects.Intro3_Ef();
            if (InBeat(768 + 512 + 256 - 16)) ExtremeBarrage.Intro8();
            if (InBeat(768 + 512 + 256 - 16)) Effects.Intro4_Ef();
            if (InBeat(768 + 512 + 256 - 32)) ExtremeBarrage.Intro9();
            if (InBeat(768 + 512 + 256 - 16)) ExtremeBarrage.Intro13();
            if (InBeat(768 + 512 + 256 + 256 - 16))
            {
                CreateGB(new GreenSoulGB(bpm * 16, 0, 0, bpm * 32));
                CreateGB(new GreenSoulGB(bpm * 16, 2, 1, bpm * 32));
                AddInstance(new InstantEvent(bpm * 16, () => { Rotate(1080, bpm * 32); }));
            };
        }
        public void Hard()
        {
            if (GametimeF == 0.5f)
            {
                HardBarrage.Intro0();
                Effects.Intro0_Ef();
            }
            if (InBeat(64)) Effects.Intro0_Ef();
            if (InBeat(64 - 16)) HardBarrage.Intro1();
            if (InBeat(128)) Effects.Intro1_Ef();
            if (InBeat(128 - 16)) HardBarrage.Intro2();
            if (InBeat(192)) Effects.Intro1_Ef();
            if (InBeat(192 - 16)) HardBarrage.Intro3();
            if (InBeat(256)) Effects.Intro2_Ef();
            if (InBeat(256 - 16)) HardBarrage.Intro4();
            if (InBeat(320)) Effects.Intro2_Ef();
            if (InBeat(320 - 16)) HardBarrage.Intro5();
            if (InBeat(384)) Effects.Intro2_Ef(); ;
            if (InBeat(384)) HardBarrage.Intro6();
            if (InBeat(448)) Effects.Intro2_Ef(); ;
            if (InBeat(448)) HardBarrage.Intro6();
            if (InBeat(510)) linebool = false;
            if (InBeat(512)) HardBarrage.Intro7();
            if (InBeat(512)) Effects.Intro3_Ef();
            if (InBeat(512 + 16 * 15)) HardBarrage.Intro8();
            if (InBeat(512 + 16 * 15 - 16)) HardBarrage.Intro9();
            if (InBeat(512 + 16 * 15)) Effects.Intro4_Ef();
            if (InBeat(768 - 16)) HardBarrage.Intro10();
            if (InBeat(768)) Effects.Intro5_Ef();
            if (InBeat(768 + 256)) HardBarrage.Intro11();
            if (InBeat(768 + 512)) HardBarrage.Intro12();
            if (InBeat(768 + 512)) Effects.Intro3_Ef();
            if (InBeat(768 + 512 + 256 - 16)) HardBarrage.Intro8();
            if (InBeat(768 + 512 + 256 - 16)) Effects.Intro4_Ef();
            if (InBeat(768 + 512 + 256 - 32)) HardBarrage.Intro9();
            if (InBeat(768 + 512 + 256 - 16)) HardBarrage.Intro13();
            if (InBeat(768 + 512 + 256 + 256 - 16))
            {
                CreateGB(new GreenSoulGB(bpm * 16, 0, 0, bpm * 32));
                CreateGB(new GreenSoulGB(bpm * 16, 2, 1, bpm * 32));
                AddInstance(new InstantEvent(bpm * 16, () => { Rotate(1080, bpm * 32); }));
            };
        }
        public void Normal()
        {
            if (GametimeF == 0.5f)
            {
                NormalBarrage.Intro0();
                Effects.Intro0_Ef();
            }
            if (InBeat(64)) Effects.Intro0_Ef();
            if (InBeat(64 - 16)) NormalBarrage.Intro1();
            if (InBeat(128)) Effects.Intro1_Ef();
            if (InBeat(128 - 16)) NormalBarrage.Intro2();
            if (InBeat(192)) Effects.Intro1_Ef();
            if (InBeat(192 - 16)) NormalBarrage.Intro3();
            if (InBeat(256)) Effects.Intro2_Ef();
            if (InBeat(256 - 16)) NormalBarrage.Intro4();
            if (InBeat(320)) Effects.Intro2_Ef();
            if (InBeat(320 - 16)) NormalBarrage.Intro5();
            if (InBeat(384)) Effects.Intro2_Ef(); ;
            if (InBeat(384)) NormalBarrage.Intro6();
            if (InBeat(448)) Effects.Intro2_Ef(); ;
            if (InBeat(448)) NormalBarrage.Intro6();
            if (InBeat(510)) linebool = false;
            if (InBeat(512)) NormalBarrage.Intro7();
            if (InBeat(512)) Effects.Intro3_Ef();
            if (InBeat(512 + 16 * 15)) NormalBarrage.Intro8();
            if (InBeat(512 + 16 * 15 - 16)) NormalBarrage.Intro9();
            if (InBeat(512 + 16 * 15)) Effects.Intro4_Ef();
            if (InBeat(768 - 16)) NormalBarrage.Intro10();
            if (InBeat(768)) Effects.Intro5_Ef();
            if (InBeat(768 + 256)) NormalBarrage.Intro11();
            if (InBeat(768 + 512)) NormalBarrage.Intro12();
            if (InBeat(768 + 512)) Effects.Intro3_Ef();
            if (InBeat(768 + 512 + 256 - 16)) NormalBarrage.Intro8();
            if (InBeat(768 + 512 + 256 - 16)) Effects.Intro4_Ef();
            if (InBeat(768 + 512 + 256 - 32)) NormalBarrage.Intro9();
            if (InBeat(768 + 512 + 256 - 16)) NormalBarrage.Intro13();
            if (InBeat(768 + 512 + 256 + 256 - 16))
            {
                CreateGB(new GreenSoulGB(bpm * 16, 0, 0, bpm * 32));
                CreateGB(new GreenSoulGB(bpm * 16, 2, 1, bpm * 32));
                AddInstance(new InstantEvent(bpm * 16, () => { Rotate(1080, bpm * 32); }));
            };
        }
        public void Easy()
        {
            if (GametimeF == 0.5f)
            {
                EasyBarrage.Intro0();
                Effects.Intro0_Ef();
            }
            if (InBeat(64)) Effects.Intro0_Ef();
            if (InBeat(64 - 16)) EasyBarrage.Intro1();
            if (InBeat(128)) Effects.Intro1_Ef();
            if (InBeat(128 - 16)) EasyBarrage.Intro2();
            if (InBeat(192)) Effects.Intro1_Ef();
            if (InBeat(192 - 16)) EasyBarrage.Intro3();
            if (InBeat(256)) Effects.Intro2_Ef();
            if (InBeat(256 - 16)) EasyBarrage.Intro4();
            if (InBeat(320)) Effects.Intro2_Ef();
            if (InBeat(320 - 16)) EasyBarrage.Intro5();
            if (InBeat(384)) Effects.Intro2_Ef(); ;
            if (InBeat(384)) EasyBarrage.Intro6();
            if (InBeat(448)) Effects.Intro2_Ef(); ;
            if (InBeat(448)) EasyBarrage.Intro6();
            if (InBeat(510)) linebool = false;
            if (InBeat(512)) EasyBarrage.Intro7();
            if (InBeat(512)) Effects.Intro3_Ef();
            if (InBeat(512 + 16 * 15)) EasyBarrage.Intro8();
            if (InBeat(512 + 16 * 15 - 16)) EasyBarrage.Intro9();
            if (InBeat(512 + 16 * 15)) Effects.Intro4_Ef();
            if (InBeat(768 - 16)) EasyBarrage.Intro10();
            if (InBeat(768)) Effects.Intro5_Ef();
            if (InBeat(768 + 256)) EasyBarrage.Intro11();
            if (InBeat(768 + 512)) EasyBarrage.Intro12();
            if (InBeat(768 + 512)) Effects.Intro3_Ef();
            if (InBeat(768 + 512 + 256 - 16)) EasyBarrage.Intro8();
            if (InBeat(768 + 512 + 256 - 16)) Effects.Intro4_Ef();
            if (InBeat(768 + 512 + 256 - 32)) EasyBarrage.Intro9();
            if (InBeat(768 + 512 + 256 - 16)) EasyBarrage.Intro13();
            if (InBeat(768 + 512 + 256 + 256 - 16))
            {
                CreateGB(new GreenSoulGB(bpm * 16, 3, 0, bpm * 32));
                AddInstance(new InstantEvent(bpm * 16, () => { Rotate(1080, bpm * 32); }));
            };
        }
        public void Noob()
        {
            if (GametimeF == 0.5f)
            {
                NoobBarrage.Intro0();
                Effects.Intro0_Ef();
            }
            if (InBeat(64)) Effects.Intro0_Ef();
            if (InBeat(64 - 16)) NoobBarrage.Intro1();
            if (InBeat(128)) Effects.Intro1_Ef();
            if (InBeat(128 - 16)) NoobBarrage.Intro2();
            if (InBeat(192)) Effects.Intro1_Ef();
            if (InBeat(192 - 16)) NoobBarrage.Intro3();
            if (InBeat(256)) Effects.Intro2_Ef();
            if (InBeat(256 - 16)) NoobBarrage.Intro4();
            if (InBeat(320)) Effects.Intro2_Ef();
            if (InBeat(320 - 16)) NoobBarrage.Intro5();
            if (InBeat(384)) Effects.Intro2_Ef(); ;
            if (InBeat(384)) NoobBarrage.Intro6();
            if (InBeat(448)) Effects.Intro2_Ef(); ;
            if (InBeat(448)) NoobBarrage.Intro6();
            if (InBeat(510)) linebool = false;
            if (InBeat(512)) NoobBarrage.Intro7();
            if (InBeat(512)) Effects.Intro3_Ef();
            if (InBeat(512 + 16 * 15)) NoobBarrage.Intro8();
            if (InBeat(512 + 16 * 15 - 16)) NoobBarrage.Intro9();
            if (InBeat(512 + 16 * 15)) Effects.Intro4_Ef();
            if (InBeat(768 - 16)) NoobBarrage.Intro10();
            if (InBeat(768)) Effects.Intro5_Ef();
            if (InBeat(768 + 256)) NoobBarrage.Intro11();
            if (InBeat(768 + 512)) NoobBarrage.Intro12();
            if (InBeat(768 + 512)) Effects.Intro3_Ef();
            if (InBeat(768 + 512 + 256 - 16)) NoobBarrage.Intro8();
            if (InBeat(768 + 512 + 256 - 16)) Effects.Intro4_Ef();
            if (InBeat(768 + 512 + 256 - 32)) NoobBarrage.Intro9();
            if (InBeat(768 + 512 + 256 - 16)) NoobBarrage.Intro13();
            if (InBeat(768 + 512 + 256 + 256 - 16))
            {
                CreateGB(new GreenSoulGB(bpm * 16, 3, 0, bpm * 32));
                AddInstance(new InstantEvent(bpm * 16, () => { Rotate(1080, bpm * 32); }));
            };
        }
        #region Non
        public void ExtremePlus()
        {

        }
        #endregion
        public void Start()
        {
            ScreenDrawing.UISettings.CreateUISurface();
            //TP(160, 280); 
            //GametimeDelta = (int)(this.BeatTime(1200 - 12 + 96 - 3));
            //SetBox(240, 160, 160);
            //TP(320, 240);
            //SetSoul(0);
            //GametimeDelta = (int)(this.BeatTime(768 + 256));
            Heart.Speed = 3.25f;
            Heart.SoftFalling = true;
            SetBox(240, 84, 84);
            SetSoul(1);
            TP();
            instance = this;
            Heart.Gravity = 8.2f;
            HeartAttribute.KRDamage = 5.5f;
            HeartAttribute.MaxHP = 96;
            HeartAttribute.KR = true;

        }
    }
}
