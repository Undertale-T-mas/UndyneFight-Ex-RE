using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace AprilExtends
{
    public class LostMemory : IChampionShip
    {
        private class CrossBoneRunner : Entity
        {
            public CrossBoneRunner(Vector2 start, Vector2 speed, int boneCount)
            {
                controlLayer = Surface.Hidden;
                boneCount *= 2;
                rotateSpeed = 2 * (Rand(0, 1) * 2 - 1);
                Rotation = Rand(0, 359);
                Centre = start;
                this.speed = speed;
                bones = new Bone[boneCount];
                for (int i = 0; i < boneCount; i++)
                {
                    bones[i] = new LostMemory.CrossBoneRunner.CrosserBone(2 * (i % 2));
                }
            }
            public override void Draw()
            {
                foreach (Bone v in bones)
                {
                    v.Draw();
                }
            }
            public override void Update()
            {
                Rotation += rotateSpeed;
                Centre += speed;
                appearTime += 1f;
                distSin = Sin(appearTime / instance.BeatTime(24f) * 360f) * 100f;
                float del = 360 / bones.Length;
                for (int i = 0; i < bones.Length; i++)
                {
                    Bone v = bones[i];
                    v.Centre = Centre + MathUtil.GetVector2(distSin, Rotation + del * i);
                    v.Rotation = Rotation * 1.5f + del * i;
                    v.GetCollide(Heart);
                }
            }
            private Vector2 speed;

            private readonly Bone[] bones;

            private float appearTime = 0f;

            private float distSin = 0f;

            private readonly float rotateSpeed = 2f;

            private class CrosserBone : Bone
            {
                public CrosserBone(int type)
                {
                    Length = 20f;
                    ColorType = type;
                    alpha = 1f;
                }
            }
        }
        public LostMemory()
        {
            Game.instance = new LostMemory.Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));
            difficulties = new Dictionary<string, Difficulty>
            {
                { "SP.0", Difficulty.ExtremePlus }
            };
        }
        public Dictionary<string, Difficulty> DifficultyPanel
        {
            get
            {
                return difficulties;
            }
        }
        public SaveInfo DivisionInformation
        {
            get
            {
                return divisionInformation;
            }
        }
        public IWaveSet GameContent
        {
            get
            {
                return new LostMemory.Game();
            }
        }
        private readonly Dictionary<string, Difficulty> difficulties = new();
        public SaveInfo divisionInformation;
        public static Game instance;
        public class Game : WaveConstructor, IWaveSet
        {
            internal static Game instance;
            public Game() : base(4.218125f) { }
            public static float bpm = 9.868421052631579f;
            public int zero = 0;
            public string Music => "LostMemory";

            public string FightName => "Lost Memory";
            static Shader LostSoul;

            public SongInformation Attributes => new ThisInformation();
            private class ThisInformation : SongInformation
            {
                public override string BarrageAuthor
                {
                    get
                    {
                        return "zKronO";
                    }
                }
                public override string SongAuthor
                {
                    get
                    {
                        return "Sakuzyo";
                    }
                }
                public override string PaintAuthor
                {
                    get
                    {
                        return "idk()";
                    }
                }
                public override string Extra
                {
                    get
                    {
                        return "The song is unauthorized, and \nif that infringe a copyright, \ncall T-mas by email:480642510@qq.com, \nwe will delete this song immediately";
                    }
                }
                public override Color ExtraColor
                {
                    get
                    {
                        return Color.Gray;
                    }
                }
                public override Dictionary<Difficulty, float> CompleteDifficulty
                {
                    get
                    {
                        return new Dictionary<Difficulty, float>(new KeyValuePair<Difficulty, float>[]
                        {
                            new KeyValuePair<Difficulty, float>(Difficulty.ExtremePlus, 14.9f),
                        });
                    }
                }
                public override Dictionary<Difficulty, float> ComplexDifficulty
                {
                    get
                    {
                        return new Dictionary<Difficulty, float>(new KeyValuePair<Difficulty, float>[]
                        {
                            new KeyValuePair<Difficulty, float>(Difficulty.ExtremePlus, 14.9f),
                        });
                    }
                }
                public override Dictionary<Difficulty, float> APDifficulty
                {
                    get
                    {
                        return new Dictionary<Difficulty, float>(new KeyValuePair<Difficulty, float>[]
                        {
                            new KeyValuePair<Difficulty, float>(Difficulty.ExtremePlus, 20.9f),
                        });
                    }
                }
            }
            private class KickCounter : Entity
            {
                public override void Draw()
                {
                    Font.NormalFont.CentreDraw((count + 1).ToString() ?? "", new Vector2(320f, 80f), Color.White, GameStates.SpriteBatch);
                    bool flag = time > 0f;
                    if (flag)
                    {
                        Font.NormalFont.CentreDraw("Time = " + (count * 1f / time).ToString(), new Vector2(320f, 120f), Color.White, GameStates.SpriteBatch);
                        Font.NormalFont.CentreDraw("Frame = " + (60f * (count * 1f / time)).ToString(), new Vector2(320f, 160f), Color.White, GameStates.SpriteBatch);
                    }
                }
                public override void Update()
                {
                    bool flag = GameStates.IsKeyPressed(InputIdentity.Alternate) && time == 0f;
                    if (flag)
                    {
                        count = 0;
                        time += 0.001f;
                    }
                    else
                    {
                        bool flag2 = time == 0f;
                        if (!flag2)
                        {
                            time += 1f;
                            bool flag3 = GameStates.IsKeyPressed(InputIdentity.Alternate);
                            if (flag3)
                            {
                                count++;
                                PlaySound(Sounds.pierce);
                            }
                        }
                    }
                }
                private int count = -1;
                private float time = 0f;
            }
            public static class DrawLine
            {
                public class NormalLine : Entity
                {
                    public float duration = 0;
                    public float x1 = 0;
                    public float y1 = 0;
                    public float x2 = 0;
                    public float y2 = 0;
                    public Color color = Color.White;
                    public float depth = 0;
                    public NormalLine(float x1, float y1, float x2, float y2, float duration, float alpha, Color color, float depth)
                    {
                        this.x1 = x1;
                        this.y1 = y1;
                        this.x2 = x2;
                        this.y2 = y2;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                        this.depth = depth;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new(x1, y1), new(x2, y2), 4, color * alpha, depth);
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
                public class Linerotate : Entity
                {
                    public float duration = 0;
                    public float xCenter = 0;
                    public float yCenter = 0;
                    public float rotate = 0;
                    public Color color = Color.White;
                    public float depth = 0;
                    public Linerotate(float xCenter, float yCenter, float rotate, float duration, float alpha, Color color, float depth)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.rotate = rotate;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                        this.depth = depth;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new(xCenter - Tan(rotate) * yCenter, 0), new(xCenter + Tan(rotate) * (480 - yCenter), 480), 4, color * alpha, depth);
                        Depth = depth;
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
                public class Linerotatelong : Entity
                {
                    public Linerotatelong(float xCenter, float yCenter, float rotate, float duration, float alpha, float length, Color color)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.rotate = rotate;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.length = length;
                        this.color = color;
                    }

                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new Vector2(xCenter, yCenter), new Vector2(xCenter + Cos(rotate) * length, yCenter + Sin(rotate) * length), width, color * alpha, 0.99f);
                        Depth = 0.99f;
                    }

                    public override void Update()
                    {
                        time++;
                        bool flag = time == duration;
                        if (flag)
                        {
                            Dispose();
                        }
                    }

                    public float duration = 0f;

                    public float xCenter = 0f;

                    public float yCenter = 0f;

                    public float rotate = 0f;

                    public float length = 0f;

                    public float width = 4f;

                    public Color color = Color.White;

                    public float alpha = 1f;

                    public int time = 0;

                    public float speed = 1f;
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
                        DrawingLab.DrawLine(
                            new Vector2(xCenter, yCenter),
                            new Vector2(
                                xCenter + Cos(rotate) * length,
                                yCenter + Sin(rotate) * length),
                            6,
                            color * alpha,
                            0.99f);
                        Depth = 0.99f;

                        DrawingLab.DrawLine(
                            new Vector2(xCenter, yCenter),
                            new Vector2(
                                xCenter + Cos(rotate + 180) * Anotherlength,
                                yCenter + Sin(rotate + 180) * Anotherlength),
                            6,
                            color * alpha,
                            0.99f);
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
                public class SolidPolygon : Entity
                {
                    public int accuracy = 0;
                    public float duration = 0;
                    public float xCenter = 0;
                    public float yCenter = 0;
                    public float radius = 0;
                    public float rotate = 0;
                    public float rotatec = 0;
                    public Color color = Color.White;
                    public SolidPolygon(float xCenter, float yCenter, float radius, float duration, float alpha, float rotate, float rotatec, Color color, int accuracy)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                        this.accuracy = accuracy;
                        this.radius = radius;
                        this.rotate = rotate;
                        this.rotatec = rotatec;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {

                        for (int i = 0; i < accuracy; i += 1)
                        {
                            int rotateb = accuracy * 180 - 360;
                            DrawingLab.DrawLine(
                                new Vector2(xCenter + Cos(rotateb / accuracy * i + rotatec) * radius + Cos(rotate) * radius,
                                            yCenter + Sin(rotateb / accuracy * i + rotatec) * radius + Sin(rotate) * radius),
                                new Vector2(xCenter + Cos(rotateb / accuracy * (i + 1) + rotatec) * radius + Cos(rotate) * radius,
                                            yCenter + Sin(rotateb / accuracy * (i + 1) + rotatec) * radius + Sin(rotate) * radius),//rotatec是绕点旋转，rotate是绕中心旋转
                                4,
                                color * alpha,
                                0.99f);
                            Depth = 0.99f;
                        }
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
                public class HollowPolygon : Entity
                {
                    public int accuracy = 0;
                    public float duration = 0;
                    public float xCenter = 0;
                    public float yCenter = 0;
                    public float radius = 0;
                    public Color color = Color.White;
                    public HollowPolygon(float xCenter, float yCenter, float radius, float duration, float alpha, Color color, int accuracy)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                        this.accuracy = accuracy;
                        this.radius = radius;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        for (int i = 0; i < accuracy; i += 1)
                        {
                            int rotate = 360;
                            DrawingLab.DrawLine(
                                new Vector2(xCenter + Cos(rotate / accuracy * i) * radius,
                                            yCenter + Sin(rotate / accuracy * i) * radius),
                                new Vector2(xCenter + Cos(rotate / accuracy * (i + 1)) * radius,
                                            yCenter + Sin(rotate / accuracy * (i + 1)) * radius),
                                4,
                                color * alpha,
                                0.99f);
                            Depth = 0.99f;
                        }
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

            public static class mainEffects
            {
                public static void rain()
                {
                    float speed = 9f;
                    Game.DrawLine.Linerotatelong rain = new(Rand(0, 860), -45f, 290f + Rand(-2.5f, 2.5f), bpm * 32f, Rand(0.2f, 0.4f), Rand(9, 55), Color.White);
                    rain.width = Rand(2, 4);
                    bool flag = Rand(1, 3) == 1;
                    if (flag)
                    {
                        CreateEntity(rain);
                    }
                    else
                    {
                        for (int a = 0; a < 2; a++)
                        {
                            CreateEntity(rain);
                        }
                    }
                    AddInstance(new TimeRangedEvent(0f, bpm * 32f, delegate ()
                    {
                        rain.xCenter += Cos(110f) * speed;
                        rain.yCenter += Sin(110f) * speed;
                    }));
                }
                public static void rain2()
                {
                    Game.DrawLine.Linerotatelong rain = new(Rand(0, 640), -45f, 270f, bpm * 32f, 0.15f, Rand(13, 55), Color.White);
                    rain.width = 2f;
                    Game.DrawLine.Linerotatelong rain1 = new(Rand(0, 640), 525f, 90f, bpm * 32f, 0.15f, Rand(13, 55), Color.White);
                    rain.width = 2f;
                    bool flag = Rand(1, 3) == 1;
                    if (flag)
                    {
                        CreateEntity(rain);
                    }
                    else
                    {
                        for (int a = 0; a < 2; a++)
                        {
                            CreateEntity(rain);
                        }
                    }
                    bool flag2 = Rand(1, 3) == 1;
                    if (flag2)
                    {
                        CreateEntity(rain1);
                    }
                    else
                    {
                        for (int a2 = 0; a2 < 2; a2++)
                        {
                            CreateEntity(rain1);
                        }
                    }
                    AddInstance(new TimeRangedEvent(0f, bpm * 32f, delegate ()
                    {
                        rain.yCenter += 9f;
                        rain1.yCenter -= 9f;
                    }));
                }
                public static void rain3()
                {
                    Game.DrawLine.Linerotatelong rain1 = new(Rand(0, 640), 525f, 90f, bpm * 32f, 0.15f, Rand(13, 55), Color.White);
                    rain1.width = 2f;
                    bool flag2 = Rand(1, 3) == 1;
                    if (flag2)
                    {
                        CreateEntity(rain1);
                    }
                    else
                    {
                        for (int a2 = 0; a2 < 2; a2++)
                        {
                            CreateEntity(rain1);
                        }
                    }
                    AddInstance(new TimeRangedEvent(0f, bpm * 32f, delegate ()
                    {
                        rain1.yCenter -= 9f;
                    }));
                }
                public static void rain4()
                {
                    Game.DrawLine.Linerotatelong rain = new(Rand(0, 640), -45f, 270f, bpm * 32f, 0.15f, Rand(13, 55), Color.White);
                    rain.width = 2f;
                    bool flag = Rand(1, 3) == 1;
                    if (flag)
                    {
                        CreateEntity(rain);
                    }
                    else
                    {
                        for (int a = 0; a < 2; a++)
                        {
                            CreateEntity(rain);
                        }
                    }
                    AddInstance(new TimeRangedEvent(0f, bpm * 32f, delegate ()
                    {
                        rain.yCenter += 9f;
                    }));
                }
                public class WhtieSquare : Entity
                {
                    public int duration = 0;
                    public float CentreX = 0;
                    public float CentreY = 0;
                    public float width = 0;
                    public float height = 0;
                    public Color color = Color.White;
                    public WhtieSquare(float CentreX, float CentreY, float width, float height, int duration, Color color)
                    {
                        this.CentreX = CentreX;
                        this.CentreY = CentreY;
                        this.width = width;
                        this.height = height;
                        this.duration = duration;
                        this.color = color;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        FormalDraw(Sprites.pixUnit, new CollideRect(CentreX, CentreY, width, height).ToRectangle(), color);
                        Depth = 0.1f;
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
            public static void EndATK()
            {
                for (int i = 0; i < 40; i++)
                {
                    if (GametimeF == 201 * 62.5f + i * 5)
                    {
                        CreateBone(new CustomBone(new(Rand(320 - 60, 320 + 60), 500), Motions.PositionRoute.linear, 0, 30)
                        {
                            ColorType = Rand(0, 2),
                            PositionRouteParam = new float[] { 0, -8 }
                        });
                    }
                    if (GametimeF == 206 * 62.5f + 50 + i * 14)
                    {
                        CreateBone(new CustomBone(new(Rand(320 - 60, 320 + 60), 500), Motions.PositionRoute.linear, 0, 30)
                        {
                            ColorType = 2,
                            PositionRouteParam = new float[] { 0, -8 }
                        });
                    }
                }
                for (int i = 0; i < 90; i++)
                {
                    if (GametimeF == 216 * 62.5f + 30 + i * 15)
                    {
                        CreateBone(new CustomBone(new(Rand(320 - 60, 320 + 60), -20), Motions.PositionRoute.linear, 0, 30)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { 0, 8 }
                        });
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (GametimeF == (int)(bpm * 8) + 208 * 62.5f + i * (int)(8 * bpm))
                    {
                        LeftBone b1 = new(true, 6, 0);
                        CreateBone(b1);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 11.5f;
                        }));
                    }
                    if (GametimeF == (int)(bpm * 4) + 208 * 62.5f + i * (int)(8 * bpm))
                    {
                        RightBone b1 = new(true, 6, 0);
                        CreateBone(b1);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 11.5f;
                        }));
                    }
                    if (GametimeF == 2 + (int)(bpm * 8) + 208 * 62.5f + i * (int)(8 * bpm))
                    {
                        LeftBone b1 = new(true, 6, 0);
                        CreateBone(b1);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 11.5f;
                        }));
                    }
                    if (GametimeF == 2 + (int)(bpm * 4) + 208 * 62.5f + i * (int)(8 * bpm))
                    {
                        RightBone b1 = new(true, 6, 0);
                        CreateBone(b1);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 11.5f;
                        }));
                    }
                    if (GametimeF == 4 + (int)(bpm * 8) + 208 * 62.5f + i * (int)(8 * bpm))
                    {
                        LeftBone b1 = new(true, 6, 0);
                        CreateBone(b1);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 12;
                        }));
                    }
                    if (GametimeF == 4 + (int)(bpm * 4) + 208 * 62.5f + i * (int)(8 * bpm))
                    {
                        RightBone b1 = new(true, 6, 0);
                        CreateBone(b1);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 12;
                        }));
                    }
                }
                for (int i = 0; i < 12; i++)
                {
                    if (GametimeF == 208 * 62.5f + i * (int)(0.5f * bpm))
                    {
                        LeftBone b1 = new(true, 4, 0);
                        RightBone b2 = new(true, 4, 0);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 2;
                            b2.Length += 2;
                        }));
                    }
                    if (GametimeF == (int)(4.5f * bpm) + 208 * 62.5f + i * (int)(0.5f * bpm))
                    {
                        LeftBone b1 = new(true, 4, 0);
                        RightBone b2 = new(true, 4, 0);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 4;
                            b2.Length += 4;
                        }));
                    }
                    if (GametimeF == (int)(9 * bpm) + 208 * 62.5f + i * (int)(0.5f * bpm))
                    {
                        LeftBone b1 = new(true, 4, 0);
                        RightBone b2 = new(true, 4, 0);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 5;
                            b2.Length += 5;
                        }));
                    }
                    if (GametimeF == (int)(13.5f * bpm) + 208 * 62.5f + i * (int)(0.5f * bpm))
                    {
                        LeftBone b1 = new(true, 4, 0);
                        RightBone b2 = new(true, 4, 0);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 6;
                            b2.Length += 6;
                        }));
                    }
                    if (GametimeF == (int)(18 * bpm) + 208 * 62.5f + i * (int)(0.5f * bpm))
                    {
                        LeftBone b1 = new(true, 4, 0);
                        RightBone b2 = new(true, 4, 0);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 7;
                            b2.Length += 7;
                        }));
                    }
                    if (GametimeF == (int)(22.5f * bpm) + 208 * 62.5f + i * (int)(0.5f * bpm))
                    {
                        LeftBone b1 = new(true, 4, 0);
                        RightBone b2 = new(true, 4, 0);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 8;
                            b2.Length += 8;
                        }));
                    }
                    if (GametimeF == (int)(27 * bpm) + 208 * 62.5f + i * (int)(0.5f * bpm))
                    {
                        LeftBone b1 = new(true, 4, 0);
                        RightBone b2 = new(true, 4, 0);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 9;
                            b2.Length += 9;
                        }));
                    }
                    if (GametimeF == (int)(31.5f * bpm) + 208 * 62.5f + i * (int)(0.5f * bpm))
                    {
                        LeftBone b1 = new(true, 4, 0);
                        RightBone b2 = new(true, 4, 0);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(0, 130, () =>
                        {
                            b1.Length += 10;
                            b2.Length += 10;
                        }));
                    }
                }
                if (GametimeF == 208 * 62.5f)
                {
                    CustomBone cross1;
                    for (int l = 0; l < 2; l++)
                    {
                        cross1 = new CustomBone(new Vector2(320f, 500f), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                        {
                            LengthRouteParam = new float[]
                        {
                                40
                        }
                        };
                        CustomBone customBone1 = cross1;
                        customBone1.PositionRouteParam = new float[] { 0, -8 };
                        CustomBone customBone2 = cross1;
                        float[] a2 = new float[2];
                        a2[0] = 6f;
                        customBone2.RotationRouteParam = a2;
                        cross1.RotationDetla = l * 90;
                        CreateBone(cross1);
                    }
                    CreateBone(new LeftBone(true, 8, 40) { ColorType = 1 });
                    CreateBone(new RightBone(true, 8, 40) { ColorType = 2 });
                    CustomBone cross2;
                    for (int l = 0; l < 8; l++)
                    {
                        cross2 = new CustomBone(new Vector2(320f - 80f, -20f), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                        {
                            LengthRouteParam = new float[]
                        {
                                80
                        }
                        };
                        CustomBone customBone1 = cross2;
                        customBone1.PositionRouteParam = new float[] { 0, 6 };
                        CustomBone customBone2 = cross2;
                        float[] a2 = new float[2];
                        a2[0] = 8f;
                        customBone2.RotationRouteParam = a2;
                        cross2.RotationDetla = l * 45;
                        CreateBone(cross2);
                    }
                    CustomBone cross3;
                    for (int l = 0; l < 8; l++)
                    {
                        cross3 = new CustomBone(new Vector2(320f + 80f, -20f), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                        {
                            LengthRouteParam = new float[]
                        {
                                80
                        }
                        };
                        CustomBone customBone1 = cross3;
                        customBone1.PositionRouteParam = new float[] { 0, 6 };
                        CustomBone customBone2 = cross3;
                        float[] a2 = new float[2];
                        a2[0] = -8f;
                        customBone2.RotationRouteParam = a2;
                        cross3.RotationDetla = l * 45;
                        CreateBone(cross3);
                    }
                }
                if (GametimeF > 14425 && GametimeF < 231 * 62.5f + 10)
                {
                    ScreenDrawing.ScreenAngle *= 0.9f;
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.75f * 0.1f;
                    ScreenDrawing.LeftBoundDistance = 200 - (Gametime - 14425);
                    ScreenDrawing.RightBoundDistance = 200 - (Gametime - 14425);
                }
                if (GametimeF == 14425)
                {
                    ScreenDrawing.WhiteOut(22.5f);
                }
                if (GametimeF == 231 * 62.5f + 10)
                {
                    SetSoul(1);
                    SetGreenBox();
                    Heart.GiveForce(0, 0);
                    ScreenDrawing.ScreenAngle = 0;
                    ScreenDrawing.ScreenScale = 1;
                    ScreenDrawing.BoundColor = new(0, 0, 0);
                    ScreenDrawing.LeftBoundDistance = 0;
                    ScreenDrawing.RightBoundDistance = 0;
                }
                for (int i = 0; i < 480; i++)
                {
                    if (Gametime == 231 * 62.5f + 10 + i * 2.25f)
                    {
                        DrawLine.NormalLine l = new(320, 240, Rand(-20, 660), 0, 6, 0.85f, Color.White, 0.1f);
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 6, () =>
                        {
                            l.alpha -= 0.15f;
                        }));
                    }
                    if (Gametime == 231 * 62.5f + 10 + i * 2.25f)
                    {
                        DrawLine.NormalLine l = new(320, 240, Rand(-20, 660), 480, 6, 0.85f, Color.White, 0.1f);
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 6, () =>
                        {
                            l.alpha -= 0.15f;
                        }));
                    }
                    if (Gametime == 231 * 62.5f + 10 + i * 2.25f)
                    {
                        DrawLine.NormalLine l = new(320, 240, 0, Rand(-20, 500), 6, 0.85f, Color.White, 0.1f);
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 6, () =>
                        {
                            l.alpha -= 0.15f;
                        }));
                    }
                    if (Gametime == 231 * 62.5f + 10 + i * 2.25f)
                    {
                        DrawLine.NormalLine l = new(320, 240, 640, Rand(-20, 500), 6, 0.85f, Color.White, 0.1f);
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 6, () =>
                        {
                            l.alpha -= 0.15f;
                        }));
                    }
                }
                if (GametimeF == 238 * 62.5f + 30 - 80)
                {
                    float Beat1 = 0;
                    string[] Arrow1 =
                    {
                        //0
                        "R0","/","R0","/",
                        //1
                        "R0","/","/","/",    "/","/","/","/",     "R0","/","R0","/",
                        "R0","/","/","/",    "R0","/","/","/",     "/","/","R0","/",
                        //2
                        "R0","/","R0","/",    "R0","/","/","/",     "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",     "R0","/","R0","/",
                        //3
                        "R0","/","/","/",    "/","/","/","/",     "R0","/","R0","/",
                        "R0","/","/","/",    "R0","/","/","/",     "R0","/","R0","/",
                        //4
                        "R0","/","R0","/",    "R0","/","/","/",     "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",     "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow1.Length; i++)
                    {
                        if (Arrow1[i] == "/")
                        {
                            Beat1 += 0.5f * bpm;
                        }
                        else if (Arrow1[i] != "/")
                        {
                            SetPlayerMission(1);
                            instance.CreateArrows(75.5f + Beat1, 7.35f, Arrow1[i]);
                            Beat1 += 0.5f * bpm;
                        }
                    }
                }
                if (GametimeF > 231 * 62.5f + 10 && GametimeF < 248 * 62.5f + 30)
                {
                    InstantSetBox(new Vector2(Sin((Gametime - 231 * 62.5f + 10) * 1) * 40 + 320, Sin((Gametime - (231 * 62.5f) + 10) * 2) * 40 + 240), 84, 84);
                    InstantTP(new(Sin((Gametime - 231 * 62.5f + 10) * 1) * 40 + 320, Sin((Gametime - (231 * 62.5f) + 10) * 2) * 40 + 240));
                }
                if (GametimeF == 248 * 62.5f + 30)
                {
                    float t = 1;
                    AddInstance(new TimeRangedEvent(0, 120, () =>
                    {
                        InstantSetBox(new Vector2(BoxStates.Centre.X + t, BoxStates.Centre.Y + t * t), 84, 84);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        t += 0.2f;
                    }));
                    CreateEntity(new UndyneFight_Ex.Fight.TextPrinter((int)(50 * bpm), "$Congratulations!Kid!\nNow you can have a rest.\nHave a GOOD dream...", new Vector2(160, 180), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gold)));
                }
                if (GametimeF == 15375)
                {

                }
            }
            public static void Effect()
            {
                if (GametimeF > 0 && GametimeF < 936.5f)
                {
                    ScreenDrawing.ScreenAngle = Sin(Gametime * 1.25f) * 5.5f;
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.125f * 0.1f;
                }
                if (GametimeF == 937)
                {
                    ScreenDrawing.ScreenAngle = 0;
                }
                if (Gametime == 0)
                {
                    DrawLine.Linerotate l1 = new(320, 240, 45, 940, 0, Color.White, 0.1f);
                    DrawLine.Linerotate l2 = new(320, 240, 135, 940, 0, Color.White, 0.1f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    AddInstance(new TimeRangedEvent(0, 940, () =>
                    {
                        l1.alpha = Sin((Gametime + 20) * 2.25f) * 0.55f;
                        l2.alpha = Sin((Gametime + 20) * 2.25f) * 0.55f;
                    }));
                }
                for (int i = 0; i < 57; i++)
                {
                    if (GametimeF == i * 15)
                    {
                        float a = 0;
                        DrawLine.Linerotate l3 = new(320, 240 + 60, Rand(86, 94), 940, 0, Color.White, 0.1f);
                        DrawLine.Linerotate l4 = new(320, 240 - 60, Rand(86, 94), 940, 0, Color.White, 0.1f);
                        CreateEntity(l3);
                        CreateEntity(l4);
                        DrawLine.Linerotate l5 = new(320 + 60, 240, Rand(-4, 4), 940, 0, Color.White, 0.1f);
                        DrawLine.Linerotate l6 = new(320 - 60, 240, Rand(-4, 4), 940, 0, Color.White, 0.1f);
                        CreateEntity(l5);
                        CreateEntity(l6);
                        AddInstance(new TimeRangedEvent(40, 900, () =>
                        {
                            l3.alpha += 0.005f;
                            l4.alpha += 0.005f;
                            l5.alpha += 0.005f;
                            l6.alpha += 0.005f;
                            l3.yCenter += a * a * 0.45f;
                            l4.yCenter -= a * a * 0.45f;
                            l5.xCenter += a * a * 0.45f;
                            l6.xCenter -= a * a * 0.45f;
                            a += 0.015f;
                        }));
                        if (GametimeF == 937.5f)
                        {
                            l3.Dispose();
                            l4.Dispose();
                            l5.Dispose();
                            l6.Dispose();
                        }
                    }
                }
                if (GametimeF == 937.5f)
                {
                    float a = 5;
                    float a1 = 10;
                    float a2 = 10;
                    float a3 = 10;
                    float a4 = 10;
                    float a5 = 10;
                    float a6 = 10;
                    float a7 = 10;
                    float a8 = 10;
                    float a9 = 10;
                    float a10 = 10;
                    float a11 = 10;
                    float a12 = 10;
                    float a13 = 10;
                    float a14 = 10;
                    float a15 = 10;
                    float a16 = 10;
                    DrawLine.Linerotate l = new(320, 240, 0, 2799, 0.65f, Color.WhiteSmoke, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(0, 60, () =>
                    {
                        l.xCenter -= a;
                        a -= 0.125f;
                    }));
                    AddInstance(new TimeRangedEvent(60, 60, () =>
                    {
                        l.xCenter += a1;
                        a1 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(120, 60, () =>
                    {
                        l.xCenter -= a2;
                        a2 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(180, 60, () =>
                    {
                        l.xCenter += a3;
                        a3 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(240, 60, () =>
                    {
                        l.xCenter -= a4;
                        a4 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(300, 60, () =>
                    {
                        l.xCenter += a5;
                        a5 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(360, 60, () =>
                    {
                        l.xCenter -= a6;
                        a6 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(420, 60, () =>
                    {
                        l.xCenter += a7;
                        a7 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(480, 60, () =>
                    {
                        l.xCenter -= a8;
                        a8 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(540, 60, () =>
                    {
                        l.xCenter += a9;
                        a9 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(600, 60, () =>
                    {
                        l.xCenter -= a10;
                        a10 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(660, 60, () =>
                    {
                        l.xCenter += a11;
                        a11 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(720, 60, () =>
                    {
                        l.xCenter -= a12;
                        a12 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(780, 60, () =>
                    {
                        l.xCenter += a13;
                        a13 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(840, 60, () =>
                    {
                        l.xCenter -= a14;
                        a14 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(900, 60, () =>
                    {
                        l.xCenter += a15;
                        a15 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(960, 60, () =>
                    {
                        l.alpha -= 0.075f;
                        l.xCenter -= a16;
                        a16 -= 0.25f;
                    }));
                }
                if (GametimeF == 1417.5f)
                {
                    float b = 5;
                    float b1 = 10;
                    float b2 = 10;
                    float b3 = 10;
                    float b4 = 10;
                    float b5 = 10;
                    float b6 = 10;
                    float b7 = 10;
                    float b8 = 10;
                    DrawLine.Linerotate l = new(320, 240, 0, 2799 - 480, 0.65f, Color.WhiteSmoke, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(0, 60, () =>
                    {
                        l.xCenter += b;
                        b -= 0.125f;
                    }));
                    AddInstance(new TimeRangedEvent(60, 60, () =>
                    {
                        l.xCenter -= b1;
                        b1 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(120, 60, () =>
                    {
                        l.xCenter += b2;
                        b2 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(180, 60, () =>
                    {
                        l.xCenter -= b3;
                        b3 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(240, 60, () =>
                    {
                        l.xCenter += b4;
                        b4 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(300, 60, () =>
                    {
                        l.xCenter -= b5;
                        b5 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(360, 60, () =>
                    {
                        l.xCenter += b6;
                        b6 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(420, 60, () =>
                    {
                        l.xCenter -= b7;
                        b7 -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent(480, 60, () =>
                    {
                        l.alpha -= 0.075f;
                        l.xCenter += b8;
                        b8 -= 0.25f;
                    }));
                }
                #region 傻逼转屏
                if (GametimeF > 937.5f + 60 * 0 && GametimeF < 997.5f + 60 * 0)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 1 && GametimeF < 997.5f + 60 * 1)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f - 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 2 && GametimeF < 997.5f + 60 * 2)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 3 && GametimeF < 997.5f + 60 * 3)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f - 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 4 && GametimeF < 997.5f + 60 * 4)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 5 && GametimeF < 997.5f + 60 * 5)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f - 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 6 && GametimeF < 997.5f + 60 * 6)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 7 && GametimeF < 997.5f + 60 * 7)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f - 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 8 && GametimeF < 997.5f + 60 * 8)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 9 && GametimeF < 997.5f + 60 * 9)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f - 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 10 && GametimeF < 997.5f + 60 * 10)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 11 && GametimeF < 997.5f + 60 * 11)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f - 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 12 && GametimeF < 997.5f + 60 * 12)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 13 && GametimeF < 997.5f + 60 * 13)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f - 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 14 && GametimeF < 997.5f + 60 * 14)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 5 * 0.1f;
                }
                if (GametimeF > 937.5f + 60 * 15 && GametimeF < 997.5f + 60 * 15)
                {
                    ScreenDrawing.ScreenAngle *= 0.9f;
                }
                #endregion
                #region 啥笔闪线
                if (GametimeF == 1415)
                {
                    float a = 0;
                    DrawLine.NormalLine l1 = new(-20, 240, 660, 240, 60, 0.55f, Color.White, 0.99f);
                    DrawLine.NormalLine l2 = new(-20, 240, 660, 240, 60, 0.55f, Color.White, 0.99f);
                    DrawLine.Clock c1 = new(320, 240, 0, 1432.5f, 0.99f, 92, 12, Color.Gray);
                    DrawLine.Clock c2 = new(320, 240, 100, 1432.5f, 0.99f, 70, 12, Color.Gray);
                    CreateEntity(c1);
                    CreateEntity(c2);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    AddInstance(new TimeRangedEvent(0, 1432.5f, () =>
                    {
                        l1.alpha -= 0.01f;
                        l2.alpha -= 0.01f;
                        l1.y1 -= 2.75f;
                        l2.y1 += 2.75f;
                        l1.y2 -= 2.75f;
                        l2.y2 += 2.75f;
                        c1.rotate -= a * 0.2f;
                        c2.rotate -= a * 0.07f;
                        a += 0.01f;
                    }));
                    AddInstance(new TimeRangedEvent(1360, 72.5f, () =>
                    {
                        c1.alpha -= 0.0137f;
                        c2.alpha -= 0.0137f;
                    }));
                    if (GametimeF == 2847.5f)
                    {
                        c1.Dispose();
                        c2.Dispose();
                    }
                }
                #endregion
                if (GametimeF > 2812.5f && GametimeF < 2852.5f)
                {
                    DrawLine.NormalLine l1 = new(320, 480, 320 - (2852.5f - Gametime) * (2852.5f - Gametime), 0, 10, 0.75f, Color.YellowGreen, 0.99f);
                    DrawLine.NormalLine l2 = new(320, 480 + 42, 320 + (2852.5f - Gametime) * (2852.5f - Gametime), 0, 10, 0.75f, Color.YellowGreen, 0.99f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    AddInstance(new TimeRangedEvent(0, 10, () =>
                    {
                        l1.alpha -= 0.175f;
                        l2.alpha -= 0.175f;
                    }));
                }
                if (GametimeF == 2812.5f)
                {
                    ScreenDrawing.WhiteOut(40);
                }
                if (GametimeF == 2852.5f)
                {
                    AddInstance(new TimeRangedEvent(0, 4736.5f - 2852.5f, () =>
                    {
                        mainEffects.rain();
                        ScreenDrawing.BoundColor = Color.Gray;
                        ScreenDrawing.UpBoundDistance = 200;
                    }));
                    AddInstance(new TimeRangedEvent(4736.5f - 2852.5f, 1, () =>
                    {
                        ScreenDrawing.BoundColor = new(0, 0, 0);
                        ScreenDrawing.UpBoundDistance = 0;
                    }));
                }
                if (GametimeF > 2852.5f && GametimeF < 4707.5f)
                {
                    InstantSetBox(new Vector2(Sin((Gametime - 10072) * 1) * 20 + 320, 240), 84, 84);
                    InstantTP(Sin((Gametime - 10072) * 1) * 20 + 320, 240);
                }
                if (GametimeF == 4707.5f)
                {
                    ScreenDrawing.WhiteOut(30);
                }
                if (GametimeF == 5217.5f)
                {
                    ScreenDrawing.WhiteOut(42.5f);
                    PlayerInstance.hpControl.GiveProtectTime(85);
                }
                if (GametimeF > 5217.5f && GametimeF < 5260)
                {
                    int x = Rand(120, 520);
                    int y = Rand(80, 400);
                    ScreenDrawing.ScreenPositionDetla = new(Rand(-15, 15), Rand(-10, 10));
                    InstantSetBox(new Vector2(x, y), Rand(30, 160), Rand(30, 160));
                    InstantTP(new(x, y));
                    SetSoul(Rand(0, 4));
                }
                if (GametimeF == 5260)
                {
                    SetGreenBox();
                    SetSoul(1);
                    Heart.GiveForce(0, 7.8f);
                }
                if (GametimeF == 5265)
                {
                    TP();
                }
                if (GametimeF == 5265)
                {
                    ScreenDrawing.BoundColor = new(200, 200, 200);
                    AddInstance(new TimeRangedEvent(0, 30, () =>
                    {
                        ScreenDrawing.UpBoundDistance += 3.75f;
                        ScreenDrawing.DownBoundDistance += 3.75f;
                        ScreenDrawing.LeftBoundDistance += 6.416f;
                        ScreenDrawing.RightBoundDistance += 6.416f;
                    }));
                }
                for (int i = 0; i < 150; i++)
                {
                    if (GametimeF == 5217.5f + i * 0.4f)
                    {
                        DrawLine.NormalLine l1 = new(-160, 500, 800, 500, 20, Rand(0.1f, 0.75f), Color.White, 0.99f);
                        DrawLine.NormalLine l2 = new(-160, 510, 800, 510, 20, Rand(0.1f, 0.75f), Color.White, 0.99f);
                        CreateEntity(l1);
                        CreateEntity(l2);
                        AddInstance(new TimeRangedEvent(0, 20, () =>
                        {
                            l1.y1 -= 55;
                            l2.y1 -= 55.5f;
                            l1.y2 -= 55;
                            l2.y2 -= 55.5f;
                        }));
                        //以下为雪花屏特效半成（暂时没有卵用的屑废物玩意）
                        /*AddInstance(new TimeRangedEvent(0, 20, ()=>
                        {
                            Filter p;
                            ScreenDrawing.SceneRendering.InsertProduction(p = new Filter(Shaders.NeonLine, 0.55f));
                            (p.CurrentShader as GlobalResources.Effects.NeonLineShader).DrawingColor = Color.White * 0.4f;
                            (p.CurrentShader as GlobalResources.Effects.NeonLineShader).Speed = 0.4f;
                            Filter p2;
                            ScreenDrawing.SceneRendering.InsertProduction(p2 = new Filter(Shaders.Cos1Ball, 0.6f));
                            GlobalResources.Effects.BallShapingShader v = p2.CurrentShader as GlobalResources.Effects.BallShapingShader;
                            v.Intensity = 0.2f;
                            v.ScreenScale = 1.1f;
                            ScreenDrawing.ScreenPositionDetla = new Vector2(0f, -4f);
                        }));*/
                    }
                }
                for (int i = 0; i < 480; i++)
                {
                    if (Gametime == 5342.5f + i * 4)
                    {
                        DrawLine.NormalLine l = new(320, 240, Rand(-20, 660), 0, 6, 0.85f, Color.White, 0.1f);
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 6, () =>
                        {
                            l.alpha -= 0.15f;
                        }));
                    }
                    if (Gametime == 5342.5f + i * 3.75f)
                    {
                        DrawLine.NormalLine l = new(320, 240, Rand(-20, 660), 480, 6, 0.85f, Color.White, 0.1f);
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 6, () =>
                        {
                            l.alpha -= 0.15f;
                        }));
                    }
                    if (Gametime == 5342.5f + i * 3.55f)
                    {
                        DrawLine.NormalLine l = new(320, 240, 0, Rand(-20, 500), 6, 0.85f, Color.White, 0.1f);
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 6, () =>
                        {
                            l.alpha -= 0.15f;
                        }));
                    }
                    if (Gametime == 5342.5f + i * 3.35f)
                    {
                        DrawLine.NormalLine l = new(320, 240, 640, Rand(-20, 500), 6, 0.85f, Color.White, 0.1f);
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 6, () =>
                        {
                            l.alpha -= 0.15f;
                        }));
                    }
                }
                if (GametimeF == 7217.5f)
                {
                    ScreenDrawing.WhiteOut(15);
                }
                if (GametimeF == 7227.5f)
                {
                    ScreenDrawing.BoundColor = new(0, 0, 0);
                    ScreenDrawing.UpBoundDistance = 0;
                    ScreenDrawing.DownBoundDistance = 0;
                    ScreenDrawing.LeftBoundDistance = 0;
                    ScreenDrawing.RightBoundDistance = 0;
                }
                if (GametimeF >= 7227.5f && GametimeF <= 8180 && GametimeF % 3f == 1f)
                {
                    mainEffects.rain2();
                }
                if (Gametime > 8180 && GametimeF < 8240)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 2.2f * 0.1f;
                }
                if (GametimeF == 9125)
                {
                    ScreenDrawing.ScreenScale = 4;
                    SetSoul(3);
                    Heart.RotateTo(0);
                    Heart.EnabledRedShield = true;
                    SetBox(240, 120, 120);
                    Heart.GiveForce(0, 4.25f);
                }
                if (GametimeF > 9125 && GametimeF < 9185)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.1f;
                }
                if (GametimeF == 10072.5f)
                {
                    SetSoul(1);
                    Heart.EnabledRedShield = false;
                    SetGreenBox();
                    TP();
                }
                if (GametimeF > 9125 && GametimeF < 9125 + 20)
                {
                    DrawLine.Linerotate l1 = new((GametimeF - 9125) * (GametimeF - 9125) * 0.8f, 240, 0, 10, 1 - (GametimeF - 9125) * 0.05f, Color.White, 0.1f);
                    DrawLine.Linerotate l2 = new(640 - (GametimeF - 9125) * (GametimeF - 9125) * 0.8f, 240, 0, 10, 1 - (GametimeF - 9125) * 0.05f, Color.White, 0.1f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    AddInstance(new TimeRangedEvent(0, 20, () =>
                    {
                        l1.alpha -= 0.05f;
                        l2.alpha -= 0.05f;
                    }));
                }
                for (int i = 0; i < 41; i++)
                {
                    if (GametimeF == 9125 + 20 + i * 20)
                    {
                        DrawLine.NormalLine l1 = new(320, -20, 320, 500, 120, 0, Color.White, 0.1f);
                        DrawLine.NormalLine l2 = new(320, -20, 320, 500, 120, 0, Color.White, 0.1f);
                        CreateEntity(l1);
                        CreateEntity(l2);
                        AddInstance(new TimeRangedEvent(0, 120, () =>
                        {
                            l1.x2 -= 2.5f;
                            l1.x1 -= 2.5f;
                            l2.x2 += 2.5f;
                            l2.x1 += 2.5f;
                        }));
                        AddInstance(new TimeRangedEvent(0, 50, () =>
                        {
                            l1.alpha += 0.0125f;
                            l2.alpha += 0.0125f;
                        }));
                        AddInstance(new TimeRangedEvent(50, 70, () =>
                        {
                            l1.alpha -= 0.00625f;
                            l2.alpha -= 0.00625f;
                        }));
                    }
                }
                for (int i = 0; i < 12; i++)
                {
                    if (GametimeF == 159 * 62.5f + i * 10)
                    {
                        DrawLine.NormalLine l1 = new(320 - 15, -20, 320 + 15, 500, 120, 0, Color.White, 0.1f);
                        DrawLine.NormalLine l2 = new(320 - 15, -20, 320 + 15, 500, 120, 0, Color.White, 0.1f);
                        DrawLine.NormalLine l3 = new(-20, 240 + 11.25f, 660, 240 - 11.25f, 120, 0, Color.White, 0.1f);
                        DrawLine.NormalLine l4 = new(-20, 240 + 11.25f, 660, 240 - 11.25f, 120, 0, Color.White, 0.1f);
                        CreateEntity(l1);
                        CreateEntity(l2);
                        CreateEntity(l3);
                        CreateEntity(l4);
                        AddInstance(new TimeRangedEvent(0, 120, () =>
                        {
                            l1.x1 -= 1.5f;
                            l1.x2 -= 2.5f * 4;
                            l2.x1 += 2.5f * 4;
                            l2.x2 += 1.5f;
                            l3.y2 -= 1.5f;
                            l3.y1 -= 2.5f * 4;
                            l4.y2 += 2.5f * 4;
                            l4.y1 += 1.5f;
                        }));
                        AddInstance(new TimeRangedEvent(0, 50, () =>
                        {
                            l1.alpha += 0.0125f;
                            l2.alpha += 0.0125f;
                            l3.alpha += 0.0125f;
                            l4.alpha += 0.0125f;
                        }));
                        AddInstance(new TimeRangedEvent(50, 70, () =>
                        {
                            l1.alpha -= 0.00625f;
                            l2.alpha -= 0.00625f;
                            l3.alpha -= 0.00625f;
                            l4.alpha -= 0.00625f;
                        }));
                    }
                }
                if (GametimeF == 160 * 62.5f + 50)
                {
                    ScreenDrawing.WhiteOut(20);
                }
                if (GametimeF == 161 * 62.5f + 10)
                {
                    ScreenDrawing.BackGroundColor = new(20, 20, 20);
                    ScreenDrawing.BoundColor = new(195, 195, 205);
                    ScreenDrawing.UpBoundDistance = 200;
                }
                for (int i = 0; i <= 1800; i++)
                {
                    if (Gametime == 10072 + i * 1)
                    {
                        CreateEntity(new Particle(Color.Lerp(Color.WhiteSmoke, Color.White, MathF.Pow(Rand(0, 90) / 15f, 2)) * MathF.Pow(Rand(2, 14) / 4f, 1.5f), new(Rand(-22, 22) / 10f, 2.4f - Rand(-15, 15) / 10f), Rand(12, 19), new(Rand(-100, 740), -15), Sprites.square)
                        {
                            DarkingSpeed = 1.7f,
                            AutoRotate = true
                        });
                    }
                }
                if (GametimeF > 10072 && GametimeF < 11965)
                {
                    InstantSetBox(Sin((Gametime - 10072) * 1) * 20 + 240, 84, 84);
                    InstantTP(320, Sin((Gametime - 10072) * 1) * 20 + 240);
                }
                if (GametimeF == 11967.5f)
                {
                    ScreenDrawing.BackGroundColor = new(0, 0, 0);
                    ScreenDrawing.BoundColor = new(0, 0, 0);
                    ScreenDrawing.UpBoundDistance = 0;
                }
                if (GametimeF == 11967.5f - 30)
                {
                    ScreenDrawing.WhiteOut(30);
                }
                if (GametimeF == 12425)
                {
                    ScreenDrawing.WhiteOut(42.5f);
                    PlayerInstance.hpControl.GiveProtectTime(85);
                }
                if (GametimeF > 12425 && GametimeF < 12467)
                {
                    int x = Rand(120, 520);
                    int y = Rand(80, 400);
                    ScreenDrawing.ScreenPositionDetla = new(Rand(-15, 15), Rand(-10, 10));
                    InstantSetBox(new Vector2(x, y), Rand(30, 160), Rand(30, 160));
                    InstantTP(new(x, y));
                    SetSoul(Rand(0, 4));
                }
                if (GametimeF == 12467.5f)
                {
                    ScreenDrawing.SceneRendering.InsertProduction(new RGBSplitting());
                    SetSoul(2);
                    Heart.GiveForce(0, 8);
                    HeartAttribute.JumpTimeLimit = 0;
                    SetBox(320 - 60, 320 + 60, 0, 480);
                    TP();
                    ScreenDrawing.ScreenPositionDetla = new(0, 0);
                    ScreenDrawing.BoundColor = Color.White;
                    AddInstance(new TimeRangedEvent(0, 20, () =>
                    {
                        ScreenDrawing.LeftBoundDistance += 10;
                        ScreenDrawing.RightBoundDistance += 10;
                    }));
                }
                for (int i = 0; i < 150; i++)
                {
                    if (GametimeF == 12425 + i * 0.4f)
                    {
                        DrawLine.NormalLine l1 = new(-160, 500, 800, 500, 20, Rand(0.1f, 0.75f), Color.White, 0.99f);
                        DrawLine.NormalLine l2 = new(-160, 510, 800, 510, 20, Rand(0.1f, 0.75f), Color.White, 0.99f);
                        CreateEntity(l1);
                        CreateEntity(l2);
                        AddInstance(new TimeRangedEvent(0, 20, () =>
                        {
                            l1.y1 -= 55;
                            l2.y1 -= 55.5f;
                            l1.y2 -= 55;
                            l2.y2 -= 55.5f;
                        }));
                    }
                }
                for (int i = 0; i < 240; i++)
                {
                    if (GametimeF == 12467.5f + i * 8.25f)
                    {
                        DrawLine.Linerotate l = new(320, 520, Rand(60, 120), 120, Rand(0.01f, 0.65f), Color.White, Rand(0.1f, 0.7f));
                        CreateEntity(l);
                        AddInstance(new TimeRangedEvent(0, 120, () =>
                        {
                            l.yCenter -= 5;
                        }));
                    }
                }
                if (GametimeF > 12467.5f && GametimeF < 13509)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.05f * 0.1f;
                    InstantTP(Heart.Centre.X, 30 + (GametimeF - 12467.5f) * 0.175f);
                }
                if (GametimeF == 13509)
                {
                    Heart.GiveForce(0, 100);
                }
                if (GametimeF == 13510 - 30)
                {
                    AddInstance(new TimeRangedEvent(0, 30, () =>
                    {
                        int total = 30;
                        float start = ScreenDrawing.ScreenAngle;
                        float end = ScreenDrawing.ScreenAngle + 180f;
                        float del = start - end;
                        float t = 0f;
                        AddInstance(new TimeRangedEvent(0f, total, delegate ()
                        {
                            ScreenDrawing.ScreenAngle = start - del * (t / (total - 1)) * (t / (total - 1));
                            t = t;
                            t += 1f;
                        }));
                    }));
                }
                #region 小辣鸡紫魂（）
                if (GametimeF == 13510)
                {
                    SetSoul(4);
                    Heart.GiveForce(180, 0);
                    HeartAttribute.PurpleLineCount = 12;
                    CreateEntity(new Boneslab(180, 80, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                    CreateEntity(new Boneslab(0, 40, 0, (int)(bpm * 15 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 6))
                {
                    HeartAttribute.PurpleLineCount = 15 - 2 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 1, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 2 * 6))
                {
                    HeartAttribute.PurpleLineCount = 18 - 3 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 2, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 3 * 6))
                {
                    HeartAttribute.PurpleLineCount = 21 - 4 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 3, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 4 * 6))
                {
                    HeartAttribute.PurpleLineCount = 24 - 5 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 4, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 5 * 6))
                {
                    HeartAttribute.PurpleLineCount = 27 - 6 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 5, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 6 * 6))
                {
                    HeartAttribute.PurpleLineCount = 30 - 7 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 6, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 7 * 6))
                {
                    HeartAttribute.PurpleLineCount = 33 - 8 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 7, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 8 * 6))
                {
                    HeartAttribute.PurpleLineCount = 36 - 9 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 8, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 9 * 6))
                {
                    HeartAttribute.PurpleLineCount = 39 - 10 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 9, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 10 * 6))
                {
                    HeartAttribute.PurpleLineCount = 42 - 11 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 10, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 11 * 6))
                {
                    HeartAttribute.PurpleLineCount = 45 - 12 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 11, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 12 * 6))
                {
                    HeartAttribute.PurpleLineCount = 48 - 13 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 12, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 13 * 6))
                {
                    HeartAttribute.PurpleLineCount = 51 - 14 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 13, (int)(bpm * 6), (int)(bpm * 2 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 14 * 6))
                {
                    HeartAttribute.PurpleLineCount = 54 - 15 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 14, (int)(bpm * 6), (int)(bpm * 1 * 6)));
                }
                if (GametimeF == 13510 + (int)(bpm * 15 * 6))
                {
                    HeartAttribute.PurpleLineCount = 57 - 16 + 3;
                    CreateEntity(new Boneslab(180, 80 + 20 * 15, (int)(bpm * 6), 0));
                }
                #endregion
                if (GametimeF > 13510 && GametimeF < 13510 + 30)
                {

                    DrawLine.Linerotate l1 = new((GametimeF - 13510) * (GametimeF - 13510) * 1.05f, 0, (GametimeF - 13510) * 1.75f, 60, 0.75f, Color.White, 0.9f);
                    DrawLine.Linerotate l2 = new((GametimeF - 13510) * (GametimeF - 13510) * 1.05f, 0, 90 + (GametimeF - 13510) * 1.75f, 60, 0.75f, Color.White, 0.9f);
                    DrawLine.Linerotate l3 = new(640 - (GametimeF - 13510) * (GametimeF - 13510) * 1.05f, 480, -(GametimeF - 13510) * 1.75f, 60, 0.75f, Color.White, 0.9f);
                    DrawLine.Linerotate l4 = new(640 - (GametimeF - 13510) * (GametimeF - 13510) * 1.05f, 480, 90 - (GametimeF - 13510) * 1.75f, 60, 0.75f, Color.White, 0.9f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    CreateEntity(l3);
                    CreateEntity(l4);
                    AddInstance(new TimeRangedEvent(5, 20, () =>
                    {
                        l1.alpha -= 0.0375f;
                        l2.alpha -= 0.0375f;
                        l3.alpha -= 0.0375f;
                        l4.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF > 12467.5f && GametimeF < 14437.5f)
                {
                    if (GametimeF % 3 == 0)
                    {
                        mainEffects.rain3();
                    }
                }
                if (GametimeF > 13510 && GametimeF < 14437.5f)
                {
                    if (GametimeF % 3 == 0)
                    {
                        mainEffects.rain4();
                    }
                }
            }
            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }

            public void ExtremePlus()
            {
                Effect();
                if (GametimeF == 37.5f)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        CreateBone(new SideCircleBone(i * 22.5f, 3, 40, 900));
                    }
                }
                if (GametimeF == 187.5f)
                {
                    CreateBone(new CentreCircleBone(0, 2.35f, 100, 750) { ColorType = 1 });
                    CreateBone(new CentreCircleBone(90, 2.35f, 100, 750) { ColorType = 2 });
                }
                if (GametimeF == 500)
                {
                    CreateBone(new CentreCircleBone(0, -1.75f, 120, 437.5f));
                }
                if (GametimeF == 937.5f)
                {
                    CreateBone(new CustomBone(new(320, 240), (s) => { return new Vector2(Sin(s.AppearTime * 1.125f) * 80, 0); }, 0, 160) { ColorType = 1 });
                    CreateBone(new CustomBone(new(320, 240), (s) => { return new Vector2(Sin(s.AppearTime * -1.125f) * 80, 0); }, 0, 160) { ColorType = 2 });
                }
                for (int i = 0; i < 16; i++)
                {
                    if (GametimeF == 937.5f + i * 60f)
                    {
                        CustomBone cross1;
                        for (int l = 0; l < 2; l++)
                        {
                            cross1 = new CustomBone(new Vector2(260f, 240f), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                            {
                                LengthRouteParam = new float[]
                            {
                                30
                            }
                            };
                            CustomBone customBone1 = cross1;
                            float[] a1 = new float[2];
                            a1[0] = 4f;
                            customBone1.PositionRouteParam = a1;
                            CustomBone customBone2 = cross1;
                            float[] a2 = new float[2];
                            a2[0] = 6f;
                            customBone2.RotationRouteParam = a2;
                            cross1.RotationDetla = l * 90;
                            CreateBone(cross1);
                            PlaySound(Sounds.pierce);
                        }
                        CustomBone cross2;
                        for (int l = 0; l < 2; l++)
                        {
                            cross2 = new CustomBone(new Vector2(380f, 240f), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                            {
                                LengthRouteParam = new float[]
                            {
                                30
                            }
                            };
                            CustomBone customBone1 = cross2;
                            float[] a1 = new float[2];
                            a1[0] = -4f;
                            customBone1.PositionRouteParam = a1;
                            CustomBone customBone2 = cross2;
                            float[] a2 = new float[2];
                            a2[0] = -6f;
                            customBone2.RotationRouteParam = a2;
                            cross2.RotationDetla = l * 90;
                            CreateBone(cross2);
                            PlaySound(Sounds.pierce);
                        }
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    if (GametimeF == 937.5f + i * 120f)
                    {
                        int r = Rand(1, 2);
                        if (r == 1)
                        {
                            CreateEntity(new NormalGB(new(220, 180), new(220, 180), new(1, 1), 0, 60, 20));
                            CreateEntity(new NormalGB(new(420, 180), new(420, 180), new(1, 1), 180, 60, 20));
                            CreateEntity(new NormalGB(new(220, 240), new(220, 240), new(1, 1), 0, 60, 20));
                            CreateEntity(new NormalGB(new(420, 240), new(420, 240), new(1, 1), 180, 60, 20));
                        }
                        if (r == 2)
                        {
                            CreateEntity(new NormalGB(new(220, 300), new(220, 300), new(1, 1), 0, 60, 20));
                            CreateEntity(new NormalGB(new(420, 300), new(420, 300), new(1, 1), 180, 60, 20));
                            CreateEntity(new NormalGB(new(220, 240), new(220, 240), new(1, 1), 0, 60, 20));
                            CreateEntity(new NormalGB(new(420, 240), new(420, 240), new(1, 1), 180, 60, 20));
                        }
                    }
                }
                if (GametimeF == 1875)
                {
                    ScreenDrawing.WhiteOut(30);
                }
                if (GametimeF == 1904)
                {
                    ResetBarrage();
                    ScreenDrawing.ScreenScale = 1;
                    Heart.Split();
                    TP();
                    AddInstance(new TimeRangedEvent(946.5f, 1, () =>
                    {
                        SetBoxMission(1);
                        SetBox(new Vector2(320f - 42, 240f - 42), 84f, 84f);
                        SetBoxMission(0);
                        SetBox(new Vector2(320f - 42, 240f - 42), 84f, 84f);
                        SetPlayerMission(1);
                        SetSoul(1);
                        Player.hearts[0].Teleport(new(320, 240));
                        Player.hearts[0].Merge(Player.hearts[1]);
                        SetPlayerMission(1);
                    }));
                    AddInstance(new TimeRangedEvent(946.5f, 1, () =>
                    {
                        SetPlayerMission(0);
                        TP();
                        SetPlayerMission(1);
                        TP();
                    }));
                }
                if (GametimeF == 1905)
                {
                    SetBoxMission(0);
                    SetBox(194, 278, 198, 282);
                    SetBoxMission(1);
                    SetBox(362, 446, 198, 282);
                    SetPlayerMission(0);
                    SetSoul(1);
                    TP(236, 240);
                    SetPlayerMission(1);
                    TP(404, 240);
                    SetSoul(0);
                    HeartAttribute.Speed = 2.25f;
                }
                if (GametimeF == 1895 - 80)
                {
                    SetBoxMission(0);
                    float Beat1 = 0;
                    string[] Arrow1 =
                    {
                        //1
                        "R1","/","/","/",     "/","/","/","/",    "R1","/","/","/",
                        "R1","/","/","/",     "/","/","/","/",    "/","/","/","/",
                        //2
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //3
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R1","/","R1","/",
                        //4
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //5
                        "R1","/","/","/",     "/","/","/","/",    "R1","/","/","/",
                        "R1","/","/","/",     "/","/","/","/",    "/","/","/","/",
                        //6
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //7
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R1","/","R1","/",
                        //8
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                    };
                    for (int i = 0; i < Arrow1.Length; i++)
                    {
                        if (Arrow1[i] == "/")
                        {
                            Beat1 += 0.5f * bpm;
                        }
                        else if (Arrow1[i] != "/")
                        {
                            SetPlayerMission(0);
                            CreateArrows(76.5f + Beat1, 6.45f, Arrow1[i]);
                            Beat1 += 0.5f * bpm;
                        }
                    }
                }
                for (int i = 0; i < 7; i++)
                {
                    SetBoxMission(1);
                    if (GametimeF == 1905 + i * 120)
                    {
                        int r = Rand(1, 4);
                        if (r == 1)
                        {
                            CreateEntity(new Boneslab(0, 50, 60, 15));
                            CreateEntity(new Boneslab(90, 50, 60, 15));
                        }
                        if (r == 2)
                        {
                            CreateEntity(new Boneslab(90, 50, 60, 15));
                            CreateEntity(new Boneslab(180, 50, 60, 15));
                        }
                        if (r == 3)
                        {
                            CreateEntity(new Boneslab(180, 50, 60, 15));
                            CreateEntity(new Boneslab(270, 50, 60, 15));
                        }
                        if (r == 4)
                        {
                            CreateEntity(new Boneslab(270, 50, 60, 15));
                            CreateEntity(new Boneslab(0, 50, 60, 15));
                        }
                    }
                }
                if (GametimeF == 2847.5f - 80)
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //1
                        "/","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R00","/","/","/",    "R0","/","/","/",
                        //2
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "/","/","/","/",
                        //3
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //4
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //5
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //6
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","R0","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //7
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //8
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.1
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.2
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "/","/","/","/",
                        //1.3
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.4
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.5
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.6
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","R0","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.7
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.8
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(1);
                            CreateArrows(75.5f + Beat, 5.75f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                if (GametimeF == 4730)
                {
                    SetSoul(2);
                    InstantSetBox(240, 180, 180);
                    DrawLine.NormalLine l = new(320, -60, 320, 480 + 60, 487.5f, 0.1f, Color.White, 0.99f);
                    CreateEntity(l);
                    CreateEntity(new Platform(1, new(320 - 30, 240 + 30), Motions.PositionRoute.cameFromDown, 0, 40, 487.5f));
                    CreateEntity(new Platform(1, new(320 + 30, 240 - 30), Motions.PositionRoute.cameFromDown, 0, 40, 487.5f));

                    CreateEntity(new Platform(1, new(320 - 30, 240 - 30), Motions.PositionRoute.cameFromUp, 180, 40, 487.5f));
                    CreateEntity(new Platform(1, new(320 + 30, 240 + 30), Motions.PositionRoute.cameFromUp, 180, 40, 487.5f));

                    CreateEntity(new Platform(1, new(320 - 30, 240 + 30), Motions.PositionRoute.cameFromLeft, 90, 40, 487.5f));
                    CreateEntity(new Platform(1, new(320 + 30, 240 - 30), Motions.PositionRoute.cameFromLeft, 90, 40, 487.5f));

                    CreateEntity(new Platform(1, new(320 - 30, 240 - 30), Motions.PositionRoute.cameFromRight, -90, 40, 487.5f));
                    CreateEntity(new Platform(1, new(320 + 30, 240 + 30), Motions.PositionRoute.cameFromRight, -90, 40, 487.5f));
                    float t = 0;
                    AddInstance(new TimeRangedEvent(0, 487.5f, () =>
                    {
                        t += 1;
                        l.x2 = 320 + Sin(t * 1.225f) * 160;
                    }));
                    #region wdnmd
                    AddInstance(new TimeRangedEvent(12.5f, (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(6 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(9 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(10 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(12 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(18 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(21 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(22 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(6 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(9 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(10 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(12 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(16 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(18 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(21 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(22 * bpm + bpm * 24), 20, () =>
                    {
                        l.alpha -= 0.125f;
                    }));
                    //2
                    AddInstance(new TimeRangedEvent(12.5f, 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(6 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(9 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(10 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(12 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(18 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(21 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(22 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(6 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(9 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(10 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(12 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(16 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha -= 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(18 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha -= 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(21 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(22 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    #endregion 
                }
                for (int i = 0; i < 4; i++)
                {
                    if (GametimeF == 4750 + i * (int)(12 * bpm))
                    {
                        int x = Rand(220, 420);
                        int y = Rand(140, 340);
                        for (int j = 0; j < 3; j++)
                        {
                            CreateEntity(new NormalGB(new(x, y), new(x, y), new(0.75f, 0.5f), 120 * j, (int)(12 * bpm), 15));
                        }
                    }
                }
                if (GametimeF == 4750 + 0 * (int)(12 * bpm))
                {
                    Heart.GiveForce(0, 4.55f);
                    CreateEntity(new Boneslab(0, 120, (int)(10 * bpm), (int)(2 * bpm)));
                }
                if (GametimeF == 4750 + 1 * (int)(12 * bpm))
                {
                    Heart.GiveForce(180, 4.55f);
                    CreateEntity(new Boneslab(180, 120, (int)(10 * bpm), (int)(2 * bpm)));
                }
                if (GametimeF == 4750 + 2 * (int)(12 * bpm))
                {
                    Heart.GiveForce(90, 4.55f);
                    CreateEntity(new Boneslab(90, 120, (int)(10 * bpm), (int)(2 * bpm)));
                }
                if (GametimeF == 4750 + 3 * (int)(12 * bpm))
                {
                    Heart.GiveForce(-90, 4.55f);
                    CreateEntity(new Boneslab(-90, 120, (int)(10 * bpm), (int)(2 * bpm)));
                }
                if (GametimeF == 5342.5f - 80 - (int)bpm)
                {
                    float Beat = 0;
                    string[] bAr =
                    {
                        //1
                        "R00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        //2
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        //3
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        //4
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        //5
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        //6
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        //7
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        //8
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        "D00","/","+000","/",    "+000","/","+000","/",    "+000","/","+000","/",
                        //9
                        "R00","/","/","/",    "/","/","/","/",    "R00","/","/","/",
                        "R00","/","/","/",    "R00","/","/","/",    "R00","/","/","/",
                        //10
                        "R00","/","/","/",    "R00","/","/","/",    "R00","/","R00","/",
                        "R00","/","/","/",    "R00","/","/","/",    "R00","/","/","/",
                        //11
                        "R00","/","/","/",    "/","/","/","/",    "R00","/","/","/",
                        "R00","/","/","/",    "R00","/","/","/",    "R00","/","/","/",
                        //12
                        "R00","/","/","/",    "/","/","/","/",    "R00","/","/","/",
                        "R00","/","/","/",    "/","/","/","/",    "R00","/","/","/",
                        //13
                        "R00","/","/","/",    "/","/","/","/",    "R00","/","/","/",
                        "R00","/","/","/",    "R00","/","/","/",    "R00","/","/","/",
                        //14
                        "R00","/","/","/",    "R00","/","/","/",    "R00","/","R00","/",
                        "R00","/","/","/",    "R00","/","/","/",    "R00","/","/","/",
                        //15
                        "R00","/","/","/",    "/","/","/","/",    "R00","/","/","/",
                        "R00","/","/","/",    "R00","/","/","/",    "R00","/","/","/",
                        //16
                        "R00","/","/","/",    "/","/","/","/",    "R00","/","/","/",
                        "R00","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < bAr.Length; i++)
                    {
                        if (bAr[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (bAr[i] != "/")
                        {
                            SetPlayerMission(1);
                            CreateArrows(75.5f + Beat, 6.45f, bAr[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                if (GametimeF == 5342.5f - 80 - (int)bpm)
                {
                    float Beat = 0;
                    string[] rAr =
                    {
                        //1
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //2
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "X","/","/","/",    "X","/","/","/",
                        //3
                        "X","/","/","/",    "X","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","","/",
                        //4
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //5
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //6
                        "X","/","/","/",    "X","/","/","/",    "X","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //7
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //8
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //9
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //10
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "X","/","/","/",    "X","/","/","/",
                        //11
                        "X","/","/","/",    "X","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //12
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //13
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //14
                        "X","/","/","/",    "X","/","/","/",    "X","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //15
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",
                        //16
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < rAr.Length; i++)
                    {
                        if (rAr[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (rAr[i] != "/")
                        {
                            SetPlayerMission(1);
                            CreateArrow(75 + (int)Beat, "R", 4.75f, 1, 0, ArrowAttribute.SpeedUp);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                if (GametimeF == 7250)
                {
                    SetBox(240, 180, 180);
                    SetSoul(0);
                }
                if (GametimeF == 7457.5f)
                {
                    for (int i = 0; i < 6; i++)
                        CreateBone(new SwarmBone(90, 300, i * 50, 722.5f) { ColorType = 2 });
                }
                for (int i = 0; i < 20; i++)
                {
                    if (GametimeF == 7250 + i * 46)
                    {
                        DownBone b1 = new(true, 1.15f, Rand(40, 80));
                        UpBone b2 = new(true, 1.15f, 120 - LastRand);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(30, 1, () =>
                        {
                            b1.Speed = 3.75f;
                            b2.Speed = 3.75f;
                        }));
                    }
                    if (GametimeF == 7250 + 23 + i * 46)
                    {
                        DownBone b1 = new(false, 1.5f, Rand(40, 80));
                        UpBone b2 = new(false, 1.5f, 120 - LastRand);
                        CreateBone(b1);
                        CreateBone(b2);
                        AddInstance(new TimeRangedEvent(30, 1, () =>
                        {
                            b1.Speed = 3.75f;
                            b2.Speed = 3.75f;
                        }));
                    }
                }
                for (int i = 0; i < 15; i++)
                {
                    if (GametimeF == 8205 + i * 63)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            CreateBone(new SideCircleBone(j * 36, 3.27f, 30, 25));
                        }
                    }
                    if (GametimeF == 8180 + i * 63)
                    {
                        int r = Rand(0, 3);
                        if (r == 0)
                        {
                            Heart.GiveForce(0, 4.25f);
                        }
                        if (r == 1)
                        {
                            Heart.GiveForce(90, 4.25f);
                        }
                        if (r == 2)
                        {
                            Heart.GiveForce(180, 4.25f);
                        }
                        if (r == 3)
                        {
                            Heart.GiveForce(270, 4.25f);
                        }
                        SetSoul(2);
                        CreateEntity(new NormalGB(new(Rand(320 - 120, 320 + 120), Rand(240 - 80, 240 + 80)), new(320, 240), new(0.85f, 0.55f), 50, 4));
                    }
                }
                if (GametimeF == 9125 - 80)
                {
                    float Beat1 = 0;
                    string[] Arrow1 =
                    {
                        //1
                        "R1","/","/","/",     "/","/","/","/",    "R1","/","/","/",
                        "R1","/","/","/",     "/","/","/","/",    "/","/","/","/",
                        //2
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //3
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R1","/","R1","/",
                        //4
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //5
                        "R1","/","/","/",     "/","/","/","/",    "R1","/","/","/",
                        "R1","/","/","/",     "/","/","/","/",    "/","/","/","/",
                        //6
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //7
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R1","/","R1","/",
                        //8
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                    };
                    for (int i = 0; i < Arrow1.Length; i++)
                    {
                        if (Arrow1[i] == "/")
                        {
                            Beat1 += 0.5f * bpm;
                        }
                        else if (Arrow1[i] != "/")
                        {
                            SetPlayerMission(1);
                            instance.CreateArrows(75.5f + Beat1, 6.45f, Arrow1[i]);
                            Beat1 += 0.5f * bpm;
                        }
                    }
                }
                for (int i = 0; i < 15; i++)
                {
                    SetBoxMission(1);
                    if (GametimeF == 9125 + i * 60)
                    {
                        int r = Rand(1, 4);
                        if (r == 1)
                        {
                            CreateEntity(new Boneslab(0, 65, 60, 15) { ColorType = 1 });
                            CreateEntity(new Boneslab(90, 65, 60, 15) { ColorType = 1 });
                        }
                        if (r == 2)
                        {
                            CreateEntity(new Boneslab(90, 65, 60, 15) { ColorType = 1 });
                            CreateEntity(new Boneslab(180, 65, 60, 15) { ColorType = 1 });
                        }
                        if (r == 3)
                        {
                            CreateEntity(new Boneslab(180, 65, 60, 15) { ColorType = 1 });
                            CreateEntity(new Boneslab(270, 65, 60, 15) { ColorType = 1 });
                        }
                        if (r == 4)
                        {
                            CreateEntity(new Boneslab(270, 65, 60, 15) { ColorType = 1 });
                            CreateEntity(new Boneslab(0, 65, 60, 15) { ColorType = 1 });
                        }
                    }
                    if (GametimeF == 9125)
                    {
                        CreateEntity(new Boneslab(270, 5, 0, 900));
                        CreateEntity(new Boneslab(0, 5, 0, 900));
                        CreateEntity(new Boneslab(180, 5, 0, 900));
                        CreateEntity(new Boneslab(90, 5, 0, 900));
                    }
                }
                if (GametimeF == 10072.5f - 80)
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //1
                        "/","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R00","/","/","/",    "R0","/","/","/",
                        //2
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "/","/","/","/",
                        //3
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //4
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //5
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //6
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","R0","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //7
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //8
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.1
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.2
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "/","/","/","/",
                        //1.3
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.4
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.5
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.6
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","R0","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.7
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //1.8
                        "R(+011)","/","/","/",    "/","/","/","/",    "R0","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(1);
                            int r = Rand(0, 1);
                            if (r == 0)
                            {
                                CreateArrow(75 + (int)Beat, 0, 6.85f, 0, 0);
                            }
                            else if (r == 1)
                            {
                                CreateArrow(75 + (int)Beat, 2, 6.85f, 0, 0);
                            }
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                if (GametimeF == 10072.5f - 80)
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //1
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //2
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //3
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //4
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //5
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //6
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //7
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //8
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.1
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.2
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.3
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.4
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.5
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.6
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.7
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //1.8
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "R(+011)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(1);
                            int r = Rand(0, 1);
                            if (r == 0)
                            {
                                CreateArrow(75 + (int)Beat, 0, 6.25f, 1, 1);
                            }
                            else if (r == 1)
                            {
                                CreateArrow(75 + (int)Beat, 2, 6.25f, 1, 1);
                            }
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                if (GametimeF == 11967.5f + 0 * (int)(12 * bpm))
                {
                    Heart.GiveForce(0, 4.55f);
                    CreateEntity(new Boneslab(0, 120, (int)(10 * bpm), (int)(2 * bpm)));
                }
                if (GametimeF == 11967.5f + 1 * (int)(12 * bpm))
                {
                    Heart.GiveForce(180, 4.55f);
                    CreateEntity(new Boneslab(180, 120, (int)(10 * bpm), (int)(2 * bpm)));
                }
                if (GametimeF == 11967.5f + 2 * (int)(12 * bpm))
                {
                    Heart.GiveForce(90, 4.55f);
                    CreateEntity(new Boneslab(90, 120, (int)(10 * bpm), (int)(2 * bpm)));
                }
                if (GametimeF == 11967.5f + 3 * (int)(12 * bpm))
                {
                    Heart.GiveForce(-90, 4.55f);
                    CreateEntity(new Boneslab(-90, 120, (int)(10 * bpm), (int)(2 * bpm)));
                }
                if (GametimeF == 11967.5f)
                {
                    SetSoul(2);
                    InstantSetBox(240, 180, 180);
                    DrawLine.NormalLine l = new(320, -60, 320, 480 + 60, 487.5f, 0.1f, Color.White, 0.99f);
                    CreateEntity(l);
                    CreateEntity(new Platform(1, new(320 - 30, 240 + 30), Motions.PositionRoute.cameFromDown, 0, 40, 487.5f));
                    CreateEntity(new Platform(1, new(320 + 30, 240 - 30), Motions.PositionRoute.cameFromDown, 0, 40, 487.5f));

                    CreateEntity(new Platform(1, new(320 - 30, 240 - 30), Motions.PositionRoute.cameFromUp, 180, 40, 487.5f));
                    CreateEntity(new Platform(1, new(320 + 30, 240 + 30), Motions.PositionRoute.cameFromUp, 180, 40, 487.5f));

                    CreateEntity(new Platform(1, new(320 - 30, 240 + 30), Motions.PositionRoute.cameFromLeft, 90, 40, 487.5f));
                    CreateEntity(new Platform(1, new(320 + 30, 240 - 30), Motions.PositionRoute.cameFromLeft, 90, 40, 487.5f));

                    CreateEntity(new Platform(1, new(320 - 30, 240 - 30), Motions.PositionRoute.cameFromRight, -90, 40, 487.5f));
                    CreateEntity(new Platform(1, new(320 + 30, 240 + 30), Motions.PositionRoute.cameFromRight, -90, 40, 487.5f));
                    float t = 0;
                    AddInstance(new TimeRangedEvent(0, 487.5f, () =>
                    {
                        t += 1;
                        l.x2 = 320 + Sin(t * 1.225f) * 160;
                    }));
                    #region wdnmd
                    AddInstance(new TimeRangedEvent(12.5f, (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(6 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(9 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(10 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(12 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(18 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(21 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(22 * bpm), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(6 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(9 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(10 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(12 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(16 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(18 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(21 * bpm + bpm * 24), (int)bpm, () =>
                    {
                        l.alpha -= 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(22 * bpm + bpm * 24), 20, () =>
                    {
                        l.alpha -= 0.125f;
                    }));
                    //2
                    AddInstance(new TimeRangedEvent(12.5f, 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(6 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(9 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(10 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(12 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(18 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(21 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(22 * bpm), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(6 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(9 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(10 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(12 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(16 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha -= 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(18 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha -= 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(21 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    AddInstance(new TimeRangedEvent(12.5f + (int)(22 * bpm + bpm * 24), 1, () =>
                    {
                        l.alpha = 0.9868421052631579f;
                    }));
                    #endregion 
                }
                for (int i = 0; i < 4; i++)
                {
                    if (GametimeF == 11967.5f + i * (int)(12 * bpm))
                    {
                        int x = Rand(220, 420);
                        int y = Rand(140, 340);
                        for (int j = 0; j < 3; j++)
                        {
                            CreateEntity(new NormalGB(new(x, y), new(x, y), new(0.75f, 0.5f), 120 * j, (int)(12 * bpm), 15));
                        }
                    }
                }
                if (GametimeF == 12737.5f)
                {
                    float sin = 0;
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 64, () =>
                    {
                        sin = Sin(speed) * 24;
                        speed += 4f;
                    }));
                    for (int a = 0; a < (int)(4 * bpm); a++)
                    {
                        AddInstance(new TimeRangedEvent(a * 4, 1, () =>
                        {
                            CreateBone(new LeftBone(true, 3.5f, 120 - 80 + sin));
                            CreateBone(new RightBone(true, 3.5f, 120 - 80 - sin));
                        }));
                    }
                }
                EndATK();
            }

            public void Hard()
            {
                throw new NotImplementedException();
            }

            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Normal()
            {
                //
            }

            public void Start()
            {
                SetSoul(0);
                SetBox(240, 160, 160);
                TP();
                HeartAttribute.MaxHP = 16;
                HeartAttribute.Speed = 3;
                ScreenDrawing.ThemeColor = Color.LightGoldenrodYellow;
                //GametimeDelta = 12425-60;
                //GametimeDelta = 10072.5f - 80-10;
            }
        }
    }
}