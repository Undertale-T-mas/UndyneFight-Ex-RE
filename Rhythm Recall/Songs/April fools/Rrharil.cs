using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
namespace AprilExtends
{
    public class Rrharil : IChampionShip
    {
        // Token: 0x06000070 RID: 112 RVA: 0x0000438C File Offset: 0x0000258C
        public Rrharil()
        {
            Rrharil.Game.instance = new Rrharil.Game();
            divisionImformation = new SaveInfo("imf{");
            divisionImformation.PushNext(new SaveInfo("dif:4"));
            difficulties = new Dictionary<string, Difficulty>();
            difficulties.Add("HD 12", Difficulty.Normal);
            difficulties.Add("IN 15", Difficulty.Extreme);
            difficulties.Add("AT 16", Difficulty.ExtremePlus);
        }
        public Dictionary<string, Difficulty> DifficultyPanel
        {
            get
            {
                return difficulties;
            }
        }
        public SaveInfo DivisionImformation
        {
            get
            {
                return divisionImformation;
            }
        }
        public IWaveSet GameContent
        {
            get
            {
                return new Rrharil.Game();
            }
        }
        private readonly Dictionary<string, Difficulty> difficulties = new Dictionary<string, Difficulty>();
        public SaveInfo divisionImformation;

        private class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(4.93421f) { }
            public class DrawLine
            {
                public class NormalLine : Entity
                {
                    public float duration = 0;
                    public float x1 = 0;
                    public float y1 = 0;
                    public float x2 = 0;
                    public float y2 = 0;
                    public Color color = Color.White;
                    public NormalLine(float x1, float y1, float x2, float y2, float duration, float alpha, Color color)
                    {
                        this.x1 = x1;
                        this.y1 = y1;
                        this.x2 = x2;
                        this.y2 = y2;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new(x1, y1), new(x2, y2), 4, color * alpha, 0.99f);
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
                    public Linerotate(float xCenter, float yCenter, float rotate, float duration, float alpha, Color color)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.rotate = rotate;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.color = color;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        DrawingLab.DrawLine(new(xCenter - Tan(rotate) * yCenter, 0), new(xCenter + Tan(rotate) * (480 - yCenter), 480), 4, color * alpha, 0.99f);
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
                public class Linerotatelong : Entity
                {
                    public float duration = 0;
                    public float xCenter = 0;
                    public float yCenter = 0;
                    public float rotate = 0;
                    public float length = 0;
                    public float Anotherlength = 0;
                    public Color color = Color.White;
                    public Linerotatelong(float xCenter, float yCenter, float rotate, float duration, float alpha, float length, float Anotherlength, Color color)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.rotate = rotate;
                        this.duration = duration;
                        this.alpha = alpha;
                        this.length = length;
                        this.color = color;
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
                            4,
                            color * alpha,
                            0.99f);
                        Depth = 0.99f;

                        DrawingLab.DrawLine(
                            new Vector2(xCenter, yCenter),
                            new Vector2(
                                xCenter + Cos(rotate + 180) * Anotherlength,
                                yCenter + Sin(rotate + 180) * Anotherlength),
                            4,
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
                public class Photo : Entity
                {
                    public int accuracy = 0;
                    public float xCenter = 0;
                    public float yCenter = 0;
                    public float radiusA = 0;
                    public float radiusB = 0;
                    public float rotateA = 0;
                    public float rotateB = 0;
                    public float rotateC = 0;
                    public Photo(float xCenter, float yCenter, float radiusA, float radiusB, float rotateA, float rotateB, float rotateC, int accuracy)
                    {
                        this.xCenter = xCenter;
                        this.yCenter = yCenter;
                        this.accuracy = accuracy;
                        this.radiusA = radiusA;
                        this.radiusB = radiusB;
                        this.rotateA = rotateA;
                        this.rotateB = rotateB;
                        this.rotateC = rotateC;
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
                                new Vector2(xCenter + Cos(rotateA / accuracy * i) * radiusA + Cos(rotateA) * radiusA,
                                            yCenter + Sin(rotateA / accuracy * i) * radiusA + Sin(rotateA) * radiusA),
                                new Vector2(xCenter + Cos(rotateA / accuracy * i) * radiusB + Cos(rotateA) * radiusB,
                                            yCenter + Sin(rotateA / accuracy * i) * radiusB + Sin(rotateA) * radiusB),
                                4,
                                Color.White,
                                0.99f);
                            Depth = 0.99f;
                        }
                    }

                    public override void Update()
                    {
                        time++;
                        if (time == 11000)
                        {
                            Dispose();
                        }

                    }
                }
            }
            public SongImformation Attributes => new ThisImformation();
            private class ThisImformation : SongImformation
            {
                public override string BarrageAuthor
                {
                    get
                    {
                        return "Tlottgodinf";
                    }
                }
                public override string SongAuthor
                {
                    get
                    {
                        return "Team Grimoire";
                    }
                }
                public override string PaintAuthor
                {
                    get
                    {
                        return "knife";
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
                        return Color.Cyan;
                    }
                }

                public override Dictionary<Difficulty, float> CompleteDifficulty
                {
                    get
                    {
                        return new Dictionary<Difficulty, float>(new KeyValuePair<Difficulty, float>[]
                        {
                            new KeyValuePair<Difficulty, float>(Difficulty.Normal, 12.5f),
                            new KeyValuePair<Difficulty, float>(Difficulty.Extreme, 15.5f),
                            new KeyValuePair<Difficulty, float>(Difficulty.ExtremePlus, 21.1f)
                        });
                    }
                }
                public override Dictionary<Difficulty, float> ComplexDifficulty
                {
                    get
                    {
                        return new Dictionary<Difficulty, float>(new KeyValuePair<Difficulty, float>[]
                        {
                            new KeyValuePair<Difficulty, float>(Difficulty.Normal, 15.5f),
                            new KeyValuePair<Difficulty, float>(Difficulty.Extreme, 21f),
                            new KeyValuePair<Difficulty, float>(Difficulty.ExtremePlus, 24.9f)
                        });
                    }
                }
                public override Dictionary<Difficulty, float> APDifficulty
                {
                    get
                    {
                        return new Dictionary<Difficulty, float>(new KeyValuePair<Difficulty, float>[]
                        {
                            new KeyValuePair<Difficulty, float>(Difficulty.Normal, 20.2f),
                            new KeyValuePair<Difficulty, float>(Difficulty.Extreme, 24.2f),
                            new KeyValuePair<Difficulty, float>(Difficulty.ExtremePlus, 26.9f)
                        });
                    }
                }

                /*public override bool Hidden
                {
                    get
                    {
                        if (PlayerManager.CurrentUser == null) return true;
                        if (!PlayerManager.CurrentUser.SongPlayed("Grievous Lady")) return true;
                        var data = PlayerManager.CurrentUser.GetSongData("Grievous Lady");
                        if (data.CurrentSongStates.ContainsKey(Difficulty.Extreme))
                        {
                            return data.CurrentSongStates[Difficulty.Extreme].Mark == SkillMark.Failed;
                        }
                        return true;
                    }
                }*/
            }
            public static float bpm = 4.93421f;
            public string Music => "Rrhar'il";
            public string FightName => "Rrhar'il";
            public static Rrharil.Game instance;
            public void Start()
            {
                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                TP();
                SetSoul(1);
                HeartAttribute.DamageTaken = 1;
                HeartAttribute.MaxHP = 12;
                /*
                ScreenDrawing.HPBar.Vertical = true;
                ScreenDrawing.HPBar.HPExistColor = Color.Green;
                ScreenDrawing.HPBar.HPLoseColor = Color.Black;
                ScreenDrawing.HPBar.AreaOccupied = new(320-42-5,240-42-5,96-2,96-2);
               */
                ScreenDrawing.UIColor = Color.Gray;
            }
            public void Arrowtype()
            {


            }
            public void DevilsEffects()
            {
                DrawLine.Clock Linerotatelong1 = new(-5, 240, 90, bpm * 100, 1, 108, 12, Color.Cyan) { Depth = 0.99f };
                DrawLine.Clock Linerotatelong2 = new(-5, 240, 110, bpm * 100, 1, 68, 12, Color.Orange) { Depth = 0.98f };
                DrawLine.Clock Linerotatelong3 = new(320, -5, 0, bpm * 100, 1, 108, 12, Color.Cyan) { Depth = 0.99f };
                DrawLine.Clock Linerotatelong4 = new(320, -5, 290, bpm * 100, 1, 68, 12, Color.Orange) { Depth = 0.98f };
                DrawLine.NormalLine normalLine1 = new(320, 0, 320, 480, bpm * 100, 0, Color.Gold) { Depth = 0.8f };
                DrawLine.NormalLine normalLine2 = new(320, 0, 320, 480, bpm * 100, 0, Color.Gold) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach1 = new(320, 0, 320, 480, bpm * 100, 0.35f, Color.Gold) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach2 = new(320, 0, 320, 480, bpm * 100, 0.35f, Color.Gold) { Depth = 0.8f };
                DrawLine.NormalLine normalLine3 = new(0, 240, 640, 240, bpm * 100, 0, Color.Gold) { Depth = 0.8f };
                DrawLine.NormalLine normalLine4 = new(0, 240, 640, 240, bpm * 100, 0, Color.Gold) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach3 = new(0, 240, 640, 240, bpm * 100, 0.35f, Color.Gold) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach4 = new(0, 240, 640, 240, bpm * 100, 0.35f, Color.Gold) { Depth = 0.8f };

                DrawLine.NormalLine normalLine5 = new(160, 0, 160, 480, bpm * 100, 0.8f, Color.Gold);
                DrawLine.NormalLine normalLine6 = new(320, 0, 320, 480, bpm * 100, 0.8f, Color.Gold);
                DrawLine.NormalLine normalLine7 = new(480, 0, 480, 480, bpm * 100, 0.8f, Color.Gold);
                DrawLine.NormalLine normalLine8 = new(320, 0, 320, 480, bpm * 100, 0.3f, Color.Gold);

                DrawLine.Linerotate Linerotate1 = new(-5, 240, 0, bpm * 100, 0.8f, Color.Gold);
                DrawLine.Linerotate Linerotate2 = new(645, 240, 0, bpm * 100, 0.8f, Color.Gold);

                DrawLine.Linerotate Linerotate3 = new(110, 180, 40, bpm * 100, 1, Color.Gold);
                DrawLine.Linerotate Linerotate4 = new(110, 180, 130, bpm * 100, 1, Color.Gold);
                DrawLine.Linerotate Linerotate5 = new(530, 180, 30, bpm * 100, 1, Color.Gold);
                DrawLine.Linerotate Linerotate6 = new(530, 180, 120, bpm * 100, 1, Color.Gold);
                DrawLine.Linerotate Linerotate7 = new(175, 180, 45, bpm * 100, 1, Color.Gold);
                DrawLine.Linerotate Linerotate8 = new(175, 180, 135, bpm * 100, 1, Color.Gold);
                DrawLine.Linerotate Linerotate9 = new(380, 180, 45, bpm * 100, 1, Color.Gold);
                DrawLine.Linerotate Linerotate10 = new(380, 180, 135, bpm * 100, 1, Color.Gold);
                DrawLine.Clock Clock1 = new(320, 240, 0, bpm * 100, 1, 92, 12, Color.Cyan);
                DrawLine.Clock Clock2 = new(320, 240, 100, bpm * 100, 1, 70, 12, Color.Orange);


                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 12 + bpm * 8 * a))
                    {
                        float A = 0;
                        float B = 0;
                        float C = 0;
                        float D = 0;

                        #region 正常线
                        CreateEntity(normalLine1);
                        CreateEntity(normalLine2);
                        #region alpha值
                        AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                        {
                            normalLine1.alpha += 0.045f;
                            normalLine2.alpha += 0.045f;
                        }));
                        #endregion
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                        {
                            normalLine1.x1 -= A * A;
                            normalLine1.x2 -= A * A;
                            A += 0.4f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, bpm * 4, () =>
                        {
                            normalLine2.x1 += B * B;
                            normalLine2.x2 += B * B;
                            B += 0.4f;
                        }));
                        #endregion
                        #endregion

                        #region 附着线
                        CreateEntity(normalLineAttach1);
                        CreateEntity(normalLineAttach2);
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 6, bpm * 4, () =>
                        {
                            normalLineAttach1.x1 -= C * C;
                            normalLineAttach1.x2 -= C * C;
                            C += 0.4f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 10, bpm * 4, () =>
                        {
                            normalLineAttach2.x1 += D * D;
                            normalLineAttach2.x2 += D * D;
                            D += 0.4f;
                        }));
                        #endregion
                        #endregion
                    }
                }//线1        
                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 44 + bpm * 8 * a))
                    {
                        float A = 0;
                        float B = 0;
                        float C = 0;
                        float D = 0;
                        #region 正常线
                        CreateEntity(normalLine3);
                        CreateEntity(normalLine4);
                        #region alpha值
                        AddInstance(new TimeRangedEvent(0, bpm * 5, () =>
                        {
                            normalLine3.alpha += 0.04f;
                            normalLine4.alpha += 0.04f;
                        }));
                        #endregion
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 4, bpm * 5, () =>
                        {
                            normalLine3.y1 -= A * A;
                            normalLine3.y2 -= A * A;
                            A += 0.3f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, bpm * 5, () =>
                        {
                            normalLine4.y1 += B * B;
                            normalLine4.y2 += B * B;
                            B += 0.3f;
                        }));
                        #endregion
                        #endregion

                        #region 附着线
                        CreateEntity(normalLineAttach3);
                        CreateEntity(normalLineAttach4);
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 6, bpm * 5, () =>
                        {
                            normalLineAttach3.y1 -= C * C;
                            normalLineAttach3.y2 -= C * C;
                            C += 0.3f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 10, bpm * 5, () =>
                        {
                            normalLineAttach4.y1 += D * D;
                            normalLineAttach4.y2 += D * D;
                            D += 0.3f;
                        }));
                        #endregion
                        #endregion
                    }
                }//线2
                if (GametimeF == (int)(bpm * 16))
                {
                    CreateEntity(Linerotatelong3);
                    CreateEntity(Linerotatelong4);
                    float a = 0;
                    float d = 0;
                    #region 旋转
                    for (int c = 0; c < 16; c++)
                    {
                        float b = 0;
                        AddInstance(new TimeRangedEvent(c * bpm * 4, bpm * 4, () =>
                        {
                            Linerotatelong3.rotate += b * b;
                            Linerotatelong4.rotate += b * b * 0.1f;
                            b += 0.125f;
                        }));
                    }
                    #endregion
                    #region y轴移动
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        Linerotatelong3.yCenter += a;
                        Linerotatelong4.yCenter += a;
                        a += 0.087f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, bpm * 20, () =>
                    {
                        Linerotatelong3.yCenter += d;
                        Linerotatelong4.yCenter += d;
                        d += 0.087f;
                    }));
                    #endregion
                }//时钟1
                if (GametimeF == (int)(bpm * 48))
                {
                    CreateEntity(Linerotatelong1);
                    CreateEntity(Linerotatelong2);
                    float a = 0;
                    float d = 0;
                    #region 旋转
                    for (int c = 0; c < 16; c++)
                    {
                        float b = 0;
                        AddInstance(new TimeRangedEvent(c * bpm * 4, bpm * 4, () =>
                            {
                                Linerotatelong1.rotate += b * b;
                                Linerotatelong2.rotate += b * b * 0.1f;
                                b += 0.125f;
                            }));
                    }
                    #endregion
                    #region x轴移动
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        Linerotatelong1.xCenter += a;
                        Linerotatelong2.xCenter += a;
                        a += 0.108f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, bpm * 20, () =>
                    {
                        Linerotatelong1.xCenter += d;
                        Linerotatelong2.xCenter += d;
                        d += 0.108f;
                    }));
                    #endregion
                }//时钟2
                if (GametimeF == (int)(bpm * 16 * 5))
                {
                    Linerotatelong1.Dispose();
                    Linerotatelong2.Dispose();
                    Linerotatelong3.Dispose();
                    Linerotatelong4.Dispose();
                    normalLine1.Dispose();
                    normalLine2.Dispose();
                    normalLine3.Dispose();
                    normalLine4.Dispose();
                    normalLineAttach1.Dispose();
                    normalLineAttach2.Dispose();
                    normalLineAttach3.Dispose();
                    normalLineAttach4.Dispose();

                }//Dispose
                if (GametimeF == (int)(bpm * 8 * 16))
                {
                    CreateEntity(normalLine5);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine5.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 8 * 16 + bpm * 0.5f))
                {
                    CreateEntity(normalLine6);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine6.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 8 * 16 + bpm * 1))
                {
                    CreateEntity(normalLine7);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine7.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 9 * 16))
                {
                    CreateEntity(normalLine7);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine7.alpha -= 0.03f;
                    }));

                }
                if (GametimeF == (int)(bpm * 9 * 16 + 0.5f))
                {
                    CreateEntity(normalLine6);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine6.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 9 * 16 + bpm * 1))
                {
                    CreateEntity(normalLine5);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine5.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 10 * 16))
                {
                    CreateEntity(normalLine8);
                    AddInstance(new TimeRangedEvent(0, bpm * 9, () =>
                    {
                        normalLine8.alpha -= 0.007f;
                    }));
                }
                if (GametimeF == (int)(bpm * 11 * 16))
                {
                    normalLine5.Dispose();
                    normalLine6.Dispose();
                    normalLine7.Dispose();
                    normalLine8.Dispose();
                }//Dispose
                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 15 + a * bpm * 4))
                    {
                        BlackScreen(2);
                        AddInstance(new TimeRangedEvent(4, 1, () =>
                        {
                            BlackScreen(2);
                        }));
                    }
                }//黑屏
                if (GametimeF == (int)(bpm * 19 * 16))
                {
                    CreateEntity(Linerotate1);
                    CreateEntity(Linerotate2);
                    float a = 7;
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                    {
                        Linerotate1.xCenter += a * a;
                        a -= 0.044f;
                        if (Linerotate1.xCenter == 650)
                        {
                            a = 7;
                            return;
                        }
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Linerotate2.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, bpm * 5, () =>
                    {
                        CreateEntity(Linerotate2);
                        Linerotate2.xCenter -= a * a;
                        a -= 0.044f;
                        if (Linerotate1.xCenter == -5)
                        {
                            a = 7;
                            return;
                        }
                    }));
                }
                if (GametimeF == (int)(bpm * 23 * 16))
                {
                    Linerotate1.Dispose();
                    Linerotate2.Dispose();
                }

                if (GametimeF == (int)(bpm * 27 * 16 + bpm * 4))
                {
                    CreateEntity(Linerotate3);
                    CreateEntity(Linerotate4);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate3.rotate -= a * a;
                        Linerotate4.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate3.alpha -= b * b;
                        Linerotate4.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 27 * 16 + bpm * 12))
                {
                    Linerotate3.Dispose();
                    Linerotate4.Dispose();
                    CreateEntity(Linerotate5);
                    CreateEntity(Linerotate6);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate5.rotate += a * a;
                        Linerotate6.rotate += a * a;
                        a -= 0.033f;
                        Linerotate5.alpha -= b * b;
                        Linerotate6.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 28 * 16 + bpm * 4))
                {
                    Linerotate5.Dispose();
                    Linerotate6.Dispose();
                    CreateEntity(Linerotate7);
                    CreateEntity(Linerotate8);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate7.rotate -= a * a;
                        Linerotate8.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate7.alpha -= b * b;
                        Linerotate8.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 28 * 16 + bpm * 12))
                {
                    Linerotate7.Dispose();
                    Linerotate8.Dispose();
                    CreateEntity(Linerotate9);
                    CreateEntity(Linerotate10);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate9.rotate += a * a;
                        Linerotate10.rotate += a * a;
                        a -= 0.033f;
                        Linerotate9.alpha -= b * b;
                        Linerotate10.alpha -= b * b;
                        b += 0.012f;
                    }));
                }

                if (GametimeF == (int)(bpm * 31 * 16 + bpm * 4))
                {
                    Linerotate9.Dispose();
                    Linerotate10.Dispose();
                    CreateEntity(Linerotate5);
                    CreateEntity(Linerotate6);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate5.rotate += a * a;
                        Linerotate6.rotate += a * a;
                        a -= 0.033f;
                        Linerotate5.alpha -= b * b;
                        Linerotate6.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 31 * 16 + bpm * 12))
                {
                    Linerotate5.Dispose();
                    Linerotate6.Dispose();
                    CreateEntity(Linerotate3);
                    CreateEntity(Linerotate4);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate3.rotate -= a * a;
                        Linerotate4.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate3.alpha -= b * b;
                        Linerotate4.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 32 * 16 + bpm * 4))
                {
                    Linerotate3.Dispose();
                    Linerotate4.Dispose();
                    CreateEntity(Linerotate9);
                    CreateEntity(Linerotate10);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate9.rotate += a * a;
                        Linerotate10.rotate += a * a;
                        a -= 0.033f;
                        Linerotate9.alpha -= b * b;
                        Linerotate10.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 32 * 16 + bpm * 12))
                {
                    Linerotate9.Dispose();
                    Linerotate10.Dispose();
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                      {
                          Linerotate7.Dispose();
                          Linerotate8.Dispose();
                      }));
                    CreateEntity(Linerotate7);
                    CreateEntity(Linerotate8);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate7.rotate -= a * a;
                        Linerotate8.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate7.alpha -= b * b;
                        Linerotate8.alpha -= b * b;
                        b += 0.012f;
                    }));
                }

                if (GametimeF == (int)(bpm * 50 * 16))
                {
                    Player.Heart a = Heart.Split();
                    TP();
                    AddInstance(new TimeRangedEvent(bpm * 10 * 16, 1, () =>
                        {
                            SetBoxMission(1);
                            InstantSetBox(new Vector2(320, 240), 84, 84);
                            SetBoxMission(0);
                            InstantSetBox(new Vector2(320, 240), 84, 84);
                            Heart.Merge(a);
                        }));
                    AddInstance(new TimeRangedEvent(bpm * 10 * 16 + 1, 1, () =>
                      {
                          SetPlayerMission(0);
                          TP();
                          SetPlayerMission(1);
                          TP();
                      }));
                }
                if (GametimeF == (int)(bpm * 51 * 16 - bpm * 8))
                {

                    SetBoxMission(0);
                    SetBox(240 - 63, 240 + 21, 240 - 42, 240 + 42);
                    SetBoxMission(1);
                    SetBox(240 + 61 + 84 - 21, 240 + 61 + 84 + 63, 240 - 42, 240 + 42);
                    SetPlayerMission(0);
                    TP(240 - 21, 240);
                    SetPlayerMission(1);
                    TP(240 + 61 + 84 + 21, 240);

                }
                if (GametimeF == (int)(bpm * 51 * 16))
                {
                    CreateEntity(Clock1);
                    CreateEntity(Clock2);
                    float c = 0;
                    #region 旋转
                    for (int A = 0; A < 8; A++)
                    {
                        float b = 6f;
                        AddInstance(new TimeRangedEvent(bpm * 6 + A * 16 * bpm, bpm * 2, () =>
                        {
                            Clock1.rotate += b * b * 0.2f;
                            Clock2.rotate += b * b * 0.07f;
                            b += 0.09f;
                        }));
                    }
                    AddInstance(new TimeRangedEvent(8 * 16 * bpm, bpm * 24, () =>
                    {
                        Clock1.xCenter -= c * c;
                        Clock2.xCenter += c * c;
                        c += 0.03f;
                    }));
                    #endregion
                }//双心+钟
                for (int A = 0; A < 8; A++)
                {
                    if (GametimeF == (int)(A * 16 * bpm + bpm * 51 * 16))
                    {
                        float b = 7.2f;
                        CreateEntity(Linerotate1);
                        CreateEntity(Linerotate2);
                        AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                        {
                            Linerotate1.xCenter += b * b;
                            Linerotate2.xCenter -= b * b;
                            b -= 0.35f;
                        }));
                        AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                         {
                             Linerotate1.alpha -= 0.032f;
                             Linerotate2.alpha -= 0.032f;
                         }));
                    }
                }

                if (GametimeF == (int)(bpm * 70 * 16))
                {
                    Linerotate1.Dispose();
                    Linerotate2.Dispose();
                    Clock1.Dispose();
                    Clock2.Dispose();
                }
                if (GametimeF == (int)(bpm * 63 * 16))
                {
                    for (int a = 0; a < 3; a++)
                    {
                        Arrow arrow1 = MakeArrow(80 + bpm * a, 0, 13, 0, 0);
                        Arrow arrow2 = MakeArrow(80 + bpm * a, 1, 13, 0, 0);
                        Arrow arrow3 = MakeArrow(80 + bpm * a, 2, 13, 0, 0);
                        Arrow arrow4 = MakeArrow(80 + bpm * a * 0.5f, 3, 13, 1, 0);
                        Arrow arrow5 = MakeArrow(85 + bpm * a, 1, 13, 1, 1);
                        Arrow arrow6 = MakeArrow(85 + bpm * a, 2, 13, 1, 1);
                        Arrow arrow7 = MakeArrow(90 + bpm * a * 0.5f, 2, 13, 0, 1);
                        Arrow arrow8 = MakeArrow(90 + bpm * a, 0, 13, 1, 0);
                        Arrow arrow9 = MakeArrow(80 + bpm * a * 0.5f, 1, 13, 0, 2);
                        Arrow arrow10 = MakeArrow(80 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow11 = MakeArrow(80 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow12 = MakeArrow(80 + bpm * a, 1, 13, 1, 2);
                        Arrow arrow13 = MakeArrow(85 + bpm * a * 2, 1, 13, 1, 2);
                        Arrow arrow14 = MakeArrow(85 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow15 = MakeArrow(90 + bpm * a, 3, 13, 0, 0);
                        Arrow arrow16 = MakeArrow(95 + bpm * a * 0.5f, 0, 13, 1, 1);
                        Arrow arrow17 = MakeArrow(80 + bpm * a, 0, 13, 1, 2);
                        Arrow arrow18 = MakeArrow(85 + bpm * a * 2.5f, 2, 13, 0, 2);
                        Arrow arrow19 = MakeArrow(85 + bpm * a, 3, 13, 1, 2);
                        Arrow arrow20 = MakeArrow(85 + bpm * a * 0.5f, 0, 13, 1, 2);
                        CreateEntity(arrow1);
                        CreateEntity(arrow2);
                        CreateEntity(arrow3);
                        CreateEntity(arrow4);
                        CreateEntity(arrow5);
                        CreateEntity(arrow6);
                        CreateEntity(arrow7);
                        CreateEntity(arrow8);
                        CreateEntity(arrow9);
                        CreateEntity(arrow10);
                        CreateEntity(arrow11);
                        CreateEntity(arrow12);
                        CreateEntity(arrow13);
                        CreateEntity(arrow14);
                        CreateEntity(arrow15);
                        CreateEntity(arrow16);
                        CreateEntity(arrow17);
                        CreateEntity(arrow18);
                        CreateEntity(arrow19);
                        CreateEntity(arrow20);
                        AddInstance(new TimeRangedEvent(bpm * 16 - 2, bpm * 24 + 2, () =>
                            {
                                arrow1.Stop(1);
                                arrow2.Stop(1);
                                arrow3.Stop(1);
                                arrow4.Stop(1);
                                arrow5.Stop(1);
                                arrow6.Stop(1);
                                arrow7.Stop(1);
                                arrow8.Stop(1);
                                arrow9.Stop(1);
                                arrow10.Stop(1);
                                arrow11.Stop(1);
                                arrow12.Stop(1);
                                arrow13.Stop(1);
                                arrow14.Stop(1);
                                arrow15.Stop(1);
                                arrow16.Stop(1);
                                arrow17.Stop(1);
                                arrow18.Stop(1);
                                arrow19.Stop(1);
                                arrow20.Stop(1);
                            }));
                        AddInstance(new TimeRangedEvent(bpm * 40, bpm * 100, () =>
                         {
                             arrow1.Delay(1.5f);
                             arrow2.Delay(1.5f);
                             arrow3.Delay(1.5f);
                             arrow4.Delay(1.5f);
                             arrow5.Delay(1.5f);
                             arrow6.Delay(1.5f);
                             arrow7.Delay(1.5f);
                             arrow8.Delay(1.5f);
                             arrow9.Delay(1.5f);
                             arrow10.Delay(1.5f);
                             arrow11.Delay(1.5f);
                             arrow12.Delay(1.5f);
                             arrow13.Delay(1.5f);
                             arrow14.Delay(1.5f);
                             arrow15.Delay(1.5f);
                             arrow16.Delay(1.5f);
                             arrow17.Delay(1.5f);
                             arrow18.Delay(1.5f);
                             arrow19.Delay(1.5f);
                             arrow20.Delay(1.4f);
                         }));
                        AddInstance(new TimeRangedEvent(bpm * 80, 1, () =>
                        {
                            arrow1.Dispose();
                            arrow2.Dispose();
                            arrow3.Dispose();
                            arrow4.Dispose();
                            arrow5.Dispose();
                            arrow6.Dispose();
                            arrow7.Dispose();
                            arrow8.Dispose();
                            arrow9.Dispose();
                            arrow10.Dispose();
                            arrow11.Dispose();
                            arrow12.Dispose();
                            arrow13.Dispose();
                            arrow14.Dispose();
                            arrow15.Dispose();
                            arrow16.Dispose();
                            arrow17.Dispose();
                            arrow18.Dispose();
                            arrow19.Dispose();
                            arrow20.Dispose();
                        }));
                    }
                }//大哥换谱面
            }
            public void ExtremeEffects()
            {
                DrawLine.Clock Linerotatelong1 = new(-5, 240, 90, bpm * 100, 1, 108, 12, Color.Cyan) { Depth = 0.99f };
                DrawLine.Clock Linerotatelong2 = new(-5, 240, 110, bpm * 100, 1, 68, 12, Color.Orange) { Depth = 0.98f };
                DrawLine.Clock Linerotatelong3 = new(320, -5, 0, bpm * 100, 1, 108, 12, Color.Cyan) { Depth = 0.99f };
                DrawLine.Clock Linerotatelong4 = new(320, -5, 290, bpm * 100, 1, 68, 12, Color.Orange) { Depth = 0.98f };
                DrawLine.NormalLine normalLine1 = new(320, 0, 320, 480, bpm * 100, 0, Color.SkyBlue) { Depth = 0.8f };
                DrawLine.NormalLine normalLine2 = new(320, 0, 320, 480, bpm * 100, 0, Color.SkyBlue) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach1 = new(320, 0, 320, 480, bpm * 100, 0.35f, Color.SkyBlue) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach2 = new(320, 0, 320, 480, bpm * 100, 0.35f, Color.SkyBlue) { Depth = 0.8f };
                DrawLine.NormalLine normalLine3 = new(0, 240, 640, 240, bpm * 100, 0, Color.SkyBlue) { Depth = 0.8f };
                DrawLine.NormalLine normalLine4 = new(0, 240, 640, 240, bpm * 100, 0, Color.SkyBlue) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach3 = new(0, 240, 640, 240, bpm * 100, 0.35f, Color.SkyBlue) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach4 = new(0, 240, 640, 240, bpm * 100, 0.35f, Color.SkyBlue) { Depth = 0.8f };

                DrawLine.NormalLine normalLine5 = new(160, 0, 160, 480, bpm * 100, 0.8f, Color.SkyBlue);
                DrawLine.NormalLine normalLine6 = new(320, 0, 320, 480, bpm * 100, 0.8f, Color.SkyBlue);
                DrawLine.NormalLine normalLine7 = new(480, 0, 480, 480, bpm * 100, 0.8f, Color.SkyBlue);
                DrawLine.NormalLine normalLine8 = new(320, 0, 320, 480, bpm * 100, 0.3f, Color.SkyBlue);

                DrawLine.Linerotate Linerotate1 = new(-5, 240, 0, bpm * 100, 0.8f, Color.SkyBlue);
                DrawLine.Linerotate Linerotate2 = new(645, 240, 0, bpm * 100, 0.8f, Color.SkyBlue);

                DrawLine.Linerotate Linerotate3 = new(110, 180, 40, bpm * 100, 1, Color.SkyBlue);
                DrawLine.Linerotate Linerotate4 = new(110, 180, 130, bpm * 100, 1, Color.SkyBlue);
                DrawLine.Linerotate Linerotate5 = new(530, 180, 30, bpm * 100, 1, Color.SkyBlue);
                DrawLine.Linerotate Linerotate6 = new(530, 180, 120, bpm * 100, 1, Color.SkyBlue);
                DrawLine.Linerotate Linerotate7 = new(175, 180, 45, bpm * 100, 1, Color.SkyBlue);
                DrawLine.Linerotate Linerotate8 = new(175, 180, 135, bpm * 100, 1, Color.SkyBlue);
                DrawLine.Linerotate Linerotate9 = new(380, 180, 45, bpm * 100, 1, Color.SkyBlue);
                DrawLine.Linerotate Linerotate10 = new(380, 180, 135, bpm * 100, 1, Color.SkyBlue);
                DrawLine.Clock Clock1 = new(320, 240, 0, bpm * 100, 1, 92, 12, Color.Cyan);
                DrawLine.Clock Clock2 = new(320, 240, 100, bpm * 100, 1, 70, 12, Color.Orange);


                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 12 + bpm * 8 * a))
                    {
                        float A = 0;
                        float B = 0;
                        float C = 0;
                        float D = 0;

                        #region 正常线
                        CreateEntity(normalLine1);
                        CreateEntity(normalLine2);
                        #region alpha值
                        AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                        {
                            normalLine1.alpha += 0.045f;
                            normalLine2.alpha += 0.045f;
                        }));
                        #endregion
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                        {
                            normalLine1.x1 -= A * A;
                            normalLine1.x2 -= A * A;
                            A += 0.4f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, bpm * 4, () =>
                        {
                            normalLine2.x1 += B * B;
                            normalLine2.x2 += B * B;
                            B += 0.4f;
                        }));
                        #endregion
                        #endregion

                        #region 附着线
                        CreateEntity(normalLineAttach1);
                        CreateEntity(normalLineAttach2);
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 6, bpm * 4, () =>
                        {
                            normalLineAttach1.x1 -= C * C;
                            normalLineAttach1.x2 -= C * C;
                            C += 0.4f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 10, bpm * 4, () =>
                        {
                            normalLineAttach2.x1 += D * D;
                            normalLineAttach2.x2 += D * D;
                            D += 0.4f;
                        }));
                        #endregion
                        #endregion
                    }
                }//线1        
                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 44 + bpm * 8 * a))
                    {
                        float A = 0;
                        float B = 0;
                        float C = 0;
                        float D = 0;
                        #region 正常线
                        CreateEntity(normalLine3);
                        CreateEntity(normalLine4);
                        #region alpha值
                        AddInstance(new TimeRangedEvent(0, bpm * 5, () =>
                        {
                            normalLine3.alpha += 0.04f;
                            normalLine4.alpha += 0.04f;
                        }));
                        #endregion
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 4, bpm * 5, () =>
                        {
                            normalLine3.y1 -= A * A;
                            normalLine3.y2 -= A * A;
                            A += 0.3f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, bpm * 5, () =>
                        {
                            normalLine4.y1 += B * B;
                            normalLine4.y2 += B * B;
                            B += 0.3f;
                        }));
                        #endregion
                        #endregion

                        #region 附着线
                        CreateEntity(normalLineAttach3);
                        CreateEntity(normalLineAttach4);
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 6, bpm * 5, () =>
                        {
                            normalLineAttach3.y1 -= C * C;
                            normalLineAttach3.y2 -= C * C;
                            C += 0.3f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 10, bpm * 5, () =>
                        {
                            normalLineAttach4.y1 += D * D;
                            normalLineAttach4.y2 += D * D;
                            D += 0.3f;
                        }));
                        #endregion
                        #endregion
                    }
                }//线2
                if (GametimeF == (int)(bpm * 16))
                {
                    CreateEntity(Linerotatelong3);
                    CreateEntity(Linerotatelong4);
                    float a = 0;
                    float d = 0;
                    #region 旋转
                    for (int c = 0; c < 16; c++)
                    {
                        float b = 0;
                        AddInstance(new TimeRangedEvent(c * bpm * 4, bpm * 4, () =>
                        {
                            Linerotatelong3.rotate += b * b;
                            Linerotatelong4.rotate += b * b * 0.1f;
                            b += 0.125f;
                        }));
                    }
                    #endregion
                    #region y轴移动
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        Linerotatelong3.yCenter += a;
                        Linerotatelong4.yCenter += a;
                        a += 0.087f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, bpm * 20, () =>
                    {
                        Linerotatelong3.yCenter += d;
                        Linerotatelong4.yCenter += d;
                        d += 0.087f;
                    }));
                    #endregion
                }//时钟1
                if (GametimeF == (int)(bpm * 48))
                {
                    CreateEntity(Linerotatelong1);
                    CreateEntity(Linerotatelong2);
                    float a = 0;
                    float d = 0;
                    #region 旋转
                    for (int c = 0; c < 16; c++)
                    {
                        float b = 0;
                        AddInstance(new TimeRangedEvent(c * bpm * 4, bpm * 4, () =>
                        {
                            Linerotatelong1.rotate += b * b;
                            Linerotatelong2.rotate += b * b * 0.1f;
                            b += 0.125f;
                        }));
                    }
                    #endregion
                    #region x轴移动
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        Linerotatelong1.xCenter += a;
                        Linerotatelong2.xCenter += a;
                        a += 0.108f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, bpm * 20, () =>
                    {
                        Linerotatelong1.xCenter += d;
                        Linerotatelong2.xCenter += d;
                        d += 0.108f;
                    }));
                    #endregion
                }//时钟2
                if (GametimeF == (int)(bpm * 16 * 5))
                {
                    Linerotatelong1.Dispose();
                    Linerotatelong2.Dispose();
                    Linerotatelong3.Dispose();
                    Linerotatelong4.Dispose();
                    normalLine1.Dispose();
                    normalLine2.Dispose();
                    normalLine3.Dispose();
                    normalLine4.Dispose();
                    normalLineAttach1.Dispose();
                    normalLineAttach2.Dispose();
                    normalLineAttach3.Dispose();
                    normalLineAttach4.Dispose();

                }//Dispose
                if (GametimeF == (int)(bpm * 8 * 16))
                {
                    CreateEntity(normalLine5);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine5.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 8 * 16 + bpm * 0.5f))
                {
                    CreateEntity(normalLine6);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine6.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 8 * 16 + bpm * 1))
                {
                    CreateEntity(normalLine7);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine7.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 9 * 16))
                {
                    CreateEntity(normalLine7);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine7.alpha -= 0.03f;
                    }));

                }
                if (GametimeF == (int)(bpm * 9 * 16 + 0.5f))
                {
                    CreateEntity(normalLine6);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine6.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 9 * 16 + bpm * 1))
                {
                    CreateEntity(normalLine5);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine5.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 10 * 16))
                {
                    CreateEntity(normalLine8);
                    AddInstance(new TimeRangedEvent(0, bpm * 9, () =>
                    {
                        normalLine8.alpha -= 0.007f;
                    }));
                }
                if (GametimeF == (int)(bpm * 11 * 16))
                {
                    normalLine5.Dispose();
                    normalLine6.Dispose();
                    normalLine7.Dispose();
                    normalLine8.Dispose();
                }//Dispose
                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 15 + a * bpm * 4))
                    {
                        BlackScreen(2);
                        AddInstance(new TimeRangedEvent(4, 1, () =>
                        {
                            BlackScreen(2);
                        }));
                    }
                }//黑屏
                if (GametimeF == (int)(bpm * 19 * 16))
                {
                    CreateEntity(Linerotate1);
                    CreateEntity(Linerotate2);
                    float a = 7;
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                    {
                        Linerotate1.xCenter += a * a;
                        a -= 0.044f;
                        if (Linerotate1.xCenter == 650)
                        {
                            a = 7;
                            return;
                        }
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Linerotate2.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, bpm * 5, () =>
                    {
                        CreateEntity(Linerotate2);
                        Linerotate2.xCenter -= a * a;
                        a -= 0.044f;
                        if (Linerotate1.xCenter == -5)
                        {
                            a = 7;
                            return;
                        }
                    }));
                }
                if (GametimeF == (int)(bpm * 23 * 16))
                {
                    Linerotate1.Dispose();
                    Linerotate2.Dispose();
                }

                if (GametimeF == (int)(bpm * 27 * 16 + bpm * 4))
                {
                    CreateEntity(Linerotate3);
                    CreateEntity(Linerotate4);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate3.rotate -= a * a;
                        Linerotate4.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate3.alpha -= b * b;
                        Linerotate4.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 27 * 16 + bpm * 12))
                {
                    Linerotate3.Dispose();
                    Linerotate4.Dispose();
                    CreateEntity(Linerotate5);
                    CreateEntity(Linerotate6);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate5.rotate += a * a;
                        Linerotate6.rotate += a * a;
                        a -= 0.033f;
                        Linerotate5.alpha -= b * b;
                        Linerotate6.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 28 * 16 + bpm * 4))
                {
                    Linerotate5.Dispose();
                    Linerotate6.Dispose();
                    CreateEntity(Linerotate7);
                    CreateEntity(Linerotate8);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate7.rotate -= a * a;
                        Linerotate8.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate7.alpha -= b * b;
                        Linerotate8.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 28 * 16 + bpm * 12))
                {
                    Linerotate7.Dispose();
                    Linerotate8.Dispose();
                    CreateEntity(Linerotate9);
                    CreateEntity(Linerotate10);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate9.rotate += a * a;
                        Linerotate10.rotate += a * a;
                        a -= 0.033f;
                        Linerotate9.alpha -= b * b;
                        Linerotate10.alpha -= b * b;
                        b += 0.012f;
                    }));
                }

                if (GametimeF == (int)(bpm * 31 * 16 + bpm * 4))
                {
                    Linerotate9.Dispose();
                    Linerotate10.Dispose();
                    CreateEntity(Linerotate5);
                    CreateEntity(Linerotate6);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate5.rotate += a * a;
                        Linerotate6.rotate += a * a;
                        a -= 0.033f;
                        Linerotate5.alpha -= b * b;
                        Linerotate6.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 31 * 16 + bpm * 12))
                {
                    Linerotate5.Dispose();
                    Linerotate6.Dispose();
                    CreateEntity(Linerotate3);
                    CreateEntity(Linerotate4);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate3.rotate -= a * a;
                        Linerotate4.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate3.alpha -= b * b;
                        Linerotate4.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 32 * 16 + bpm * 4))
                {
                    Linerotate3.Dispose();
                    Linerotate4.Dispose();
                    CreateEntity(Linerotate9);
                    CreateEntity(Linerotate10);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate9.rotate += a * a;
                        Linerotate10.rotate += a * a;
                        a -= 0.033f;
                        Linerotate9.alpha -= b * b;
                        Linerotate10.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 32 * 16 + bpm * 12))
                {
                    Linerotate9.Dispose();
                    Linerotate10.Dispose();
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Linerotate7.Dispose();
                        Linerotate8.Dispose();
                    }));
                    CreateEntity(Linerotate7);
                    CreateEntity(Linerotate8);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate7.rotate -= a * a;
                        Linerotate8.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate7.alpha -= b * b;
                        Linerotate8.alpha -= b * b;
                        b += 0.012f;
                    }));
                }

                if (GametimeF == (int)(bpm * 50 * 16))
                {
                    Player.Heart a = Heart.Split();
                    TP();
                    AddInstance(new TimeRangedEvent(bpm * 10 * 16, 1, () =>
                    {
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        Heart.Merge(a);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 10 * 16 + 1, 1, () =>
                    {
                        SetPlayerMission(0);
                        TP();
                        SetPlayerMission(1);
                        TP();
                    }));
                }
                if (GametimeF == (int)(bpm * 51 * 16 - bpm * 8))
                {

                    SetBoxMission(0);
                    SetBox(240 - 63, 240 + 21, 240 - 42, 240 + 42);
                    SetBoxMission(1);
                    SetBox(240 + 61 + 84 - 21, 240 + 61 + 84 + 63, 240 - 42, 240 + 42);
                    SetPlayerMission(0);
                    TP(240 - 21, 240);
                    SetPlayerMission(1);
                    TP(240 + 61 + 84 + 21, 240);

                }
                if (GametimeF == (int)(bpm * 51 * 16))
                {
                    CreateEntity(Clock1);
                    CreateEntity(Clock2);
                    float c = 0;
                    #region 旋转
                    for (int A = 0; A < 8; A++)
                    {
                        float b = 6f;
                        AddInstance(new TimeRangedEvent(bpm * 6 + A * 16 * bpm, bpm * 2, () =>
                        {
                            Clock1.rotate += b * b * 0.2f;
                            Clock2.rotate += b * b * 0.07f;
                            b += 0.09f;
                        }));
                    }
                    AddInstance(new TimeRangedEvent(8 * 16 * bpm, bpm * 24, () =>
                    {
                        Clock1.xCenter -= c * c;
                        Clock2.xCenter += c * c;
                        c += 0.03f;
                    }));
                    #endregion
                }//双心+钟
                for (int A = 0; A < 8; A++)
                {
                    if (GametimeF == (int)(A * 16 * bpm + bpm * 51 * 16))
                    {
                        float b = 7.2f;
                        CreateEntity(Linerotate1);
                        CreateEntity(Linerotate2);
                        AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                        {
                            Linerotate1.xCenter += b * b;
                            Linerotate2.xCenter -= b * b;
                            b -= 0.35f;
                        }));
                        AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                        {
                            Linerotate1.alpha -= 0.032f;
                            Linerotate2.alpha -= 0.032f;
                        }));
                    }
                }

                if (GametimeF == (int)(bpm * 70 * 16))
                {
                    Linerotate1.Dispose();
                    Linerotate2.Dispose();
                    Clock1.Dispose();
                    Clock2.Dispose();
                }
                if (GametimeF == (int)(bpm * 63 * 16))
                {
                    for (int a = 0; a < 3; a++)
                    {
                        Arrow arrow1 = MakeArrow(80 + bpm * a, 0, 13, 0, 0);
                        Arrow arrow2 = MakeArrow(80 + bpm * a, 1, 13, 0, 0);
                        Arrow arrow3 = MakeArrow(80 + bpm * a, 2, 13, 0, 0);
                        Arrow arrow4 = MakeArrow(80 + bpm * a * 0.5f, 3, 13, 1, 0);
                        Arrow arrow5 = MakeArrow(85 + bpm * a, 1, 13, 1, 1);
                        Arrow arrow6 = MakeArrow(85 + bpm * a, 2, 13, 1, 1);
                        Arrow arrow7 = MakeArrow(90 + bpm * a * 0.5f, 2, 13, 0, 1);
                        Arrow arrow8 = MakeArrow(90 + bpm * a, 0, 13, 1, 0);
                        Arrow arrow9 = MakeArrow(80 + bpm * a * 0.5f, 1, 13, 0, 2);
                        Arrow arrow10 = MakeArrow(80 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow11 = MakeArrow(80 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow12 = MakeArrow(80 + bpm * a, 1, 13, 1, 2);
                        Arrow arrow13 = MakeArrow(85 + bpm * a * 2, 1, 13, 1, 2);
                        Arrow arrow14 = MakeArrow(85 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow15 = MakeArrow(90 + bpm * a, 3, 13, 0, 0);
                        Arrow arrow16 = MakeArrow(95 + bpm * a * 0.5f, 0, 13, 1, 1);
                        Arrow arrow17 = MakeArrow(80 + bpm * a, 0, 13, 1, 2);
                        Arrow arrow18 = MakeArrow(85 + bpm * a * 2.5f, 2, 13, 0, 2);
                        Arrow arrow19 = MakeArrow(85 + bpm * a, 3, 13, 1, 2);
                        Arrow arrow20 = MakeArrow(85 + bpm * a * 0.5f, 0, 13, 1, 2);
                        CreateEntity(arrow1);
                        CreateEntity(arrow2);
                        CreateEntity(arrow3);
                        CreateEntity(arrow4);
                        CreateEntity(arrow5);
                        CreateEntity(arrow6);
                        CreateEntity(arrow7);
                        CreateEntity(arrow8);
                        CreateEntity(arrow9);
                        CreateEntity(arrow10);
                        CreateEntity(arrow11);
                        CreateEntity(arrow12);
                        CreateEntity(arrow13);
                        CreateEntity(arrow14);
                        CreateEntity(arrow15);
                        CreateEntity(arrow16);
                        CreateEntity(arrow17);
                        CreateEntity(arrow18);
                        CreateEntity(arrow19);
                        CreateEntity(arrow20);
                        AddInstance(new TimeRangedEvent(bpm * 16 - 2, bpm * 24 + 2, () =>
                        {
                            arrow1.Delay(1);
                            arrow2.Delay(1);
                            arrow3.Delay(1);
                            arrow4.Delay(1);
                            arrow5.Delay(1);
                            arrow6.Delay(1);
                            arrow7.Delay(1);
                            arrow8.Delay(1);
                            arrow9.Delay(1);
                            arrow10.Delay(1);
                            arrow11.Delay(1);
                            arrow12.Delay(1);
                            arrow13.Delay(1);
                            arrow14.Delay(1);
                            arrow15.Delay(1);
                            arrow16.Delay(1);
                            arrow17.Delay(1);
                            arrow18.Delay(1);
                            arrow19.Delay(1);
                            arrow20.Delay(1);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 40, bpm * 100, () =>
                         {
                             arrow1.Delay(1.5f);
                             arrow2.Delay(1.5f);
                             arrow3.Delay(1.5f);
                             arrow4.Delay(1.5f);
                             arrow5.Delay(1.5f);
                             arrow6.Delay(1.5f);
                             arrow7.Delay(1.5f);
                             arrow8.Delay(1.5f);
                             arrow9.Delay(1.5f);
                             arrow10.Delay(1.5f);
                             arrow11.Delay(1.5f);
                             arrow12.Delay(1.5f);
                             arrow13.Delay(1.5f);
                             arrow14.Delay(1.5f);
                             arrow15.Delay(1.5f);
                             arrow16.Delay(1.5f);
                             arrow17.Delay(1.5f);
                             arrow18.Delay(1.5f);
                             arrow19.Delay(1.5f);
                             arrow20.Delay(1.4f);
                         }));
                        AddInstance(new TimeRangedEvent(bpm * 80, 1, () =>
                        {
                            arrow1.Dispose();
                            arrow2.Dispose();
                            arrow3.Dispose();
                            arrow4.Dispose();
                            arrow5.Dispose();
                            arrow6.Dispose();
                            arrow7.Dispose();
                            arrow8.Dispose();
                            arrow9.Dispose();
                            arrow10.Dispose();
                            arrow11.Dispose();
                            arrow12.Dispose();
                            arrow13.Dispose();
                            arrow14.Dispose();
                            arrow15.Dispose();
                            arrow16.Dispose();
                            arrow17.Dispose();
                            arrow18.Dispose();
                            arrow19.Dispose();
                            arrow20.Dispose();
                        }));
                    }
                }//大哥换谱面
            }
            public void NormalEffects()
            {
                DrawLine.Clock Linerotatelong1 = new(-5, 240, 90, bpm * 100, 1, 108, 12, Color.Cyan) { Depth = 0.99f };
                DrawLine.Clock Linerotatelong2 = new(-5, 240, 110, bpm * 100, 1, 68, 12, Color.Orange) { Depth = 0.98f };
                DrawLine.Clock Linerotatelong3 = new(320, -5, 0, bpm * 100, 1, 108, 12, Color.Cyan) { Depth = 0.99f };
                DrawLine.Clock Linerotatelong4 = new(320, -5, 290, bpm * 100, 1, 68, 12, Color.Orange) { Depth = 0.98f };
                DrawLine.NormalLine normalLine1 = new(320, 0, 320, 480, bpm * 100, 0, Color.White) { Depth = 0.8f };
                DrawLine.NormalLine normalLine2 = new(320, 0, 320, 480, bpm * 100, 0, Color.White) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach1 = new(320, 0, 320, 480, bpm * 100, 0.35f, Color.White) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach2 = new(320, 0, 320, 480, bpm * 100, 0.35f, Color.White) { Depth = 0.8f };
                DrawLine.NormalLine normalLine3 = new(0, 240, 640, 240, bpm * 100, 0, Color.White) { Depth = 0.8f };
                DrawLine.NormalLine normalLine4 = new(0, 240, 640, 240, bpm * 100, 0, Color.White) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach3 = new(0, 240, 640, 240, bpm * 100, 0.35f, Color.White) { Depth = 0.8f };
                DrawLine.NormalLine normalLineAttach4 = new(0, 240, 640, 240, bpm * 100, 0.35f, Color.White) { Depth = 0.8f };

                DrawLine.NormalLine normalLine5 = new(160, 0, 160, 480, bpm * 100, 0.8f, Color.White);
                DrawLine.NormalLine normalLine6 = new(320, 0, 320, 480, bpm * 100, 0.8f, Color.White);
                DrawLine.NormalLine normalLine7 = new(480, 0, 480, 480, bpm * 100, 0.8f, Color.White);
                DrawLine.NormalLine normalLine8 = new(320, 0, 320, 480, bpm * 100, 0.3f, Color.White);

                DrawLine.Linerotate Linerotate1 = new(-5, 240, 0, bpm * 100, 0.8f, Color.White);
                DrawLine.Linerotate Linerotate2 = new(645, 240, 0, bpm * 100, 0.8f, Color.White);

                DrawLine.Linerotate Linerotate3 = new(110, 180, 40, bpm * 100, 1, Color.White);
                DrawLine.Linerotate Linerotate4 = new(110, 180, 130, bpm * 100, 1, Color.White);
                DrawLine.Linerotate Linerotate5 = new(530, 180, 30, bpm * 100, 1, Color.White);
                DrawLine.Linerotate Linerotate6 = new(530, 180, 120, bpm * 100, 1, Color.White);
                DrawLine.Linerotate Linerotate7 = new(175, 180, 45, bpm * 100, 1, Color.White);
                DrawLine.Linerotate Linerotate8 = new(175, 180, 135, bpm * 100, 1, Color.White);
                DrawLine.Linerotate Linerotate9 = new(380, 180, 45, bpm * 100, 1, Color.White);
                DrawLine.Linerotate Linerotate10 = new(380, 180, 135, bpm * 100, 1, Color.White);
                DrawLine.Clock Clock1 = new(320, 240, 0, bpm * 100, 1, 92, 12, Color.Cyan);
                DrawLine.Clock Clock2 = new(320, 240, 100, bpm * 100, 1, 70, 12, Color.Orange);


                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 12 + bpm * 8 * a))
                    {
                        float A = 0;
                        float B = 0;
                        float C = 0;
                        float D = 0;

                        #region 正常线
                        CreateEntity(normalLine1);
                        CreateEntity(normalLine2);
                        #region alpha值
                        AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                        {
                            normalLine1.alpha += 0.045f;
                            normalLine2.alpha += 0.045f;
                        }));
                        #endregion
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                        {
                            normalLine1.x1 -= A * A;
                            normalLine1.x2 -= A * A;
                            A += 0.4f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, bpm * 4, () =>
                        {
                            normalLine2.x1 += B * B;
                            normalLine2.x2 += B * B;
                            B += 0.4f;
                        }));
                        #endregion
                        #endregion

                        #region 附着线
                        CreateEntity(normalLineAttach1);
                        CreateEntity(normalLineAttach2);
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 6, bpm * 4, () =>
                        {
                            normalLineAttach1.x1 -= C * C;
                            normalLineAttach1.x2 -= C * C;
                            C += 0.4f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 10, bpm * 4, () =>
                        {
                            normalLineAttach2.x1 += D * D;
                            normalLineAttach2.x2 += D * D;
                            D += 0.4f;
                        }));
                        #endregion
                        #endregion
                    }
                }//线1        
                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 44 + bpm * 8 * a))
                    {
                        float A = 0;
                        float B = 0;
                        float C = 0;
                        float D = 0;
                        #region 正常线
                        CreateEntity(normalLine3);
                        CreateEntity(normalLine4);
                        #region alpha值
                        AddInstance(new TimeRangedEvent(0, bpm * 5, () =>
                        {
                            normalLine3.alpha += 0.04f;
                            normalLine4.alpha += 0.04f;
                        }));
                        #endregion
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 4, bpm * 5, () =>
                        {
                            normalLine3.y1 -= A * A;
                            normalLine3.y2 -= A * A;
                            A += 0.3f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, bpm * 5, () =>
                        {
                            normalLine4.y1 += B * B;
                            normalLine4.y2 += B * B;
                            B += 0.3f;
                        }));
                        #endregion
                        #endregion

                        #region 附着线
                        CreateEntity(normalLineAttach3);
                        CreateEntity(normalLineAttach4);
                        #region 移动
                        AddInstance(new TimeRangedEvent(bpm * 6, bpm * 5, () =>
                        {
                            normalLineAttach3.y1 -= C * C;
                            normalLineAttach3.y2 -= C * C;
                            C += 0.3f;
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 10, bpm * 5, () =>
                        {
                            normalLineAttach4.y1 += D * D;
                            normalLineAttach4.y2 += D * D;
                            D += 0.3f;
                        }));
                        #endregion
                        #endregion
                    }
                }//线2
                if (GametimeF == (int)(bpm * 16))
                {
                    CreateEntity(Linerotatelong3);
                    CreateEntity(Linerotatelong4);
                    float a = 0;
                    float d = 0;
                    #region 旋转
                    for (int c = 0; c < 16; c++)
                    {
                        float b = 0;
                        AddInstance(new TimeRangedEvent(c * bpm * 4, bpm * 4, () =>
                        {
                            Linerotatelong3.rotate += b * b;
                            Linerotatelong4.rotate += b * b * 0.1f;
                            b += 0.125f;
                        }));
                    }
                    #endregion
                    #region y轴移动
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        Linerotatelong3.yCenter += a;
                        Linerotatelong4.yCenter += a;
                        a += 0.087f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, bpm * 20, () =>
                    {
                        Linerotatelong3.yCenter += d;
                        Linerotatelong4.yCenter += d;
                        d += 0.087f;
                    }));
                    #endregion
                }//时钟1
                if (GametimeF == (int)(bpm * 48))
                {
                    CreateEntity(Linerotatelong1);
                    CreateEntity(Linerotatelong2);
                    float a = 0;
                    float d = 0;
                    #region 旋转
                    for (int c = 0; c < 16; c++)
                    {
                        float b = 0;
                        AddInstance(new TimeRangedEvent(c * bpm * 4, bpm * 4, () =>
                        {
                            Linerotatelong1.rotate += b * b;
                            Linerotatelong2.rotate += b * b * 0.1f;
                            b += 0.125f;
                        }));
                    }
                    #endregion
                    #region x轴移动
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        Linerotatelong1.xCenter += a;
                        Linerotatelong2.xCenter += a;
                        a += 0.108f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, bpm * 20, () =>
                    {
                        Linerotatelong1.xCenter += d;
                        Linerotatelong2.xCenter += d;
                        d += 0.108f;
                    }));
                    #endregion
                }//时钟2
                if (GametimeF == (int)(bpm * 16 * 5))
                {
                    Linerotatelong1.Dispose();
                    Linerotatelong2.Dispose();
                    Linerotatelong3.Dispose();
                    Linerotatelong4.Dispose();
                    normalLine1.Dispose();
                    normalLine2.Dispose();
                    normalLine3.Dispose();
                    normalLine4.Dispose();
                    normalLineAttach1.Dispose();
                    normalLineAttach2.Dispose();
                    normalLineAttach3.Dispose();
                    normalLineAttach4.Dispose();

                }//Dispose
                if (GametimeF == (int)(bpm * 8 * 16))
                {
                    CreateEntity(normalLine5);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine5.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 8 * 16 + bpm * 0.5f))
                {
                    CreateEntity(normalLine6);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine6.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 8 * 16 + bpm * 1))
                {
                    CreateEntity(normalLine7);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine7.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 9 * 16))
                {
                    CreateEntity(normalLine7);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine7.alpha -= 0.03f;
                    }));

                }
                if (GametimeF == (int)(bpm * 9 * 16 + 0.5f))
                {
                    CreateEntity(normalLine6);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine6.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 9 * 16 + bpm * 1))
                {
                    CreateEntity(normalLine5);
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        normalLine5.alpha -= 0.03f;
                    }));
                }
                if (GametimeF == (int)(bpm * 10 * 16))
                {
                    CreateEntity(normalLine8);
                    AddInstance(new TimeRangedEvent(0, bpm * 9, () =>
                    {
                        normalLine8.alpha -= 0.007f;
                    }));
                }
                if (GametimeF == (int)(bpm * 11 * 16))
                {
                    normalLine5.Dispose();
                    normalLine6.Dispose();
                    normalLine7.Dispose();
                    normalLine8.Dispose();
                }//Dispose
                for (int a = 0; a < 4; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 15 + a * bpm * 4))
                    {
                        BlackScreen(2);
                        AddInstance(new TimeRangedEvent(4, 1, () =>
                        {
                            BlackScreen(2);
                        }));
                    }
                }//黑屏
                if (GametimeF == (int)(bpm * 19 * 16))
                {
                    CreateEntity(Linerotate1);
                    CreateEntity(Linerotate2);
                    float a = 7;
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                    {
                        Linerotate1.xCenter += a * a;
                        a -= 0.044f;
                        if (Linerotate1.xCenter == 650)
                        {
                            a = 7;
                            return;
                        }
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Linerotate2.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, bpm * 5, () =>
                    {
                        CreateEntity(Linerotate2);
                        Linerotate2.xCenter -= a * a;
                        a -= 0.044f;
                        if (Linerotate1.xCenter == -5)
                        {
                            a = 7;
                            return;
                        }
                    }));
                }
                if (GametimeF == (int)(bpm * 23 * 16))
                {
                    Linerotate1.Dispose();
                    Linerotate2.Dispose();
                }

                if (GametimeF == (int)(bpm * 27 * 16 + bpm * 4))
                {
                    CreateEntity(Linerotate3);
                    CreateEntity(Linerotate4);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate3.rotate -= a * a;
                        Linerotate4.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate3.alpha -= b * b;
                        Linerotate4.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 27 * 16 + bpm * 12))
                {
                    Linerotate3.Dispose();
                    Linerotate4.Dispose();
                    CreateEntity(Linerotate5);
                    CreateEntity(Linerotate6);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate5.rotate += a * a;
                        Linerotate6.rotate += a * a;
                        a -= 0.033f;
                        Linerotate5.alpha -= b * b;
                        Linerotate6.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 28 * 16 + bpm * 4))
                {
                    Linerotate5.Dispose();
                    Linerotate6.Dispose();
                    CreateEntity(Linerotate7);
                    CreateEntity(Linerotate8);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate7.rotate -= a * a;
                        Linerotate8.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate7.alpha -= b * b;
                        Linerotate8.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 28 * 16 + bpm * 12))
                {
                    Linerotate7.Dispose();
                    Linerotate8.Dispose();
                    CreateEntity(Linerotate9);
                    CreateEntity(Linerotate10);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate9.rotate += a * a;
                        Linerotate10.rotate += a * a;
                        a -= 0.033f;
                        Linerotate9.alpha -= b * b;
                        Linerotate10.alpha -= b * b;
                        b += 0.012f;
                    }));
                }

                if (GametimeF == (int)(bpm * 31 * 16 + bpm * 4))
                {
                    Linerotate9.Dispose();
                    Linerotate10.Dispose();
                    CreateEntity(Linerotate5);
                    CreateEntity(Linerotate6);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate5.rotate += a * a;
                        Linerotate6.rotate += a * a;
                        a -= 0.033f;
                        Linerotate5.alpha -= b * b;
                        Linerotate6.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 31 * 16 + bpm * 12))
                {
                    Linerotate5.Dispose();
                    Linerotate6.Dispose();
                    CreateEntity(Linerotate3);
                    CreateEntity(Linerotate4);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate3.rotate -= a * a;
                        Linerotate4.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate3.alpha -= b * b;
                        Linerotate4.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 32 * 16 + bpm * 4))
                {
                    Linerotate3.Dispose();
                    Linerotate4.Dispose();
                    CreateEntity(Linerotate9);
                    CreateEntity(Linerotate10);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate9.rotate += a * a;
                        Linerotate10.rotate += a * a;
                        a -= 0.033f;
                        Linerotate9.alpha -= b * b;
                        Linerotate10.alpha -= b * b;
                        b += 0.012f;
                    }));
                }
                if (GametimeF == (int)(bpm * 32 * 16 + bpm * 12))
                {
                    Linerotate9.Dispose();
                    Linerotate10.Dispose();
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Linerotate7.Dispose();
                        Linerotate8.Dispose();
                    }));
                    CreateEntity(Linerotate7);
                    CreateEntity(Linerotate8);
                    float a = 2.2f;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 6, () =>
                    {
                        Linerotate7.rotate -= a * a;
                        Linerotate8.rotate -= a * a;
                        a -= 0.033f;
                        Linerotate7.alpha -= b * b;
                        Linerotate8.alpha -= b * b;
                        b += 0.012f;
                    }));
                }

                if (GametimeF == (int)(bpm * 50 * 16))
                {
                    Player.Heart a = Heart.Split();
                    TP();
                    AddInstance(new TimeRangedEvent(bpm * 10 * 16, 1, () =>
                    {
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        Heart.Merge(a);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 10 * 16 + 1, 1, () =>
                    {
                        SetPlayerMission(0);
                        TP();
                        SetPlayerMission(1);
                        TP();
                    }));
                }
                if (GametimeF == (int)(bpm * 51 * 16 - bpm * 8))
                {

                    SetBoxMission(0);
                    SetBox(240 - 63, 240 + 21, 240 - 42, 240 + 42);
                    SetBoxMission(1);
                    SetBox(240 + 61 + 84 - 21, 240 + 61 + 84 + 63, 240 - 42, 240 + 42);
                    SetPlayerMission(0);
                    TP(240 - 21, 240);
                    SetPlayerMission(1);
                    TP(240 + 61 + 84 + 21, 240);

                }
                if (GametimeF == (int)(bpm * 51 * 16))
                {
                    CreateEntity(Clock1);
                    CreateEntity(Clock2);
                    float c = 0;
                    #region 旋转
                    for (int A = 0; A < 8; A++)
                    {
                        float b = 6f;
                        AddInstance(new TimeRangedEvent(bpm * 6 + A * 16 * bpm, bpm * 2, () =>
                        {
                            Clock1.rotate += b * b * 0.2f;
                            Clock2.rotate += b * b * 0.07f;
                            b += 0.09f;
                        }));
                    }
                    AddInstance(new TimeRangedEvent(8 * 16 * bpm, bpm * 24, () =>
                    {
                        Clock1.xCenter -= c * c;
                        Clock2.xCenter += c * c;
                        c += 0.03f;
                    }));
                    #endregion
                }//双心+钟
                for (int A = 0; A < 8; A++)
                {
                    if (GametimeF == (int)(A * 16 * bpm + bpm * 51 * 16))
                    {
                        float b = 7.2f;
                        CreateEntity(Linerotate1);
                        CreateEntity(Linerotate2);
                        AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                        {
                            Linerotate1.xCenter += b * b;
                            Linerotate2.xCenter -= b * b;
                            b -= 0.35f;
                        }));
                        AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                        {
                            Linerotate1.alpha -= 0.032f;
                            Linerotate2.alpha -= 0.032f;
                        }));
                    }
                }

                if (GametimeF == (int)(bpm * 70 * 16))
                {
                    Linerotate1.Dispose();
                    Linerotate2.Dispose();
                    Clock1.Dispose();
                    Clock2.Dispose();
                }
                if (GametimeF == (int)(bpm * 63 * 16))
                {
                    for (int a = 0; a < 3; a++)
                    {
                        Arrow arrow1 = MakeArrow(80 + bpm * a, 0, 13, 0, 0);
                        Arrow arrow2 = MakeArrow(80 + bpm * a, 1, 13, 0, 0);
                        Arrow arrow3 = MakeArrow(80 + bpm * a, 2, 13, 0, 0);
                        Arrow arrow4 = MakeArrow(80 + bpm * a * 0.5f, 3, 13, 1, 0);
                        Arrow arrow5 = MakeArrow(85 + bpm * a, 1, 13, 1, 1);
                        Arrow arrow6 = MakeArrow(85 + bpm * a, 2, 13, 1, 1);
                        Arrow arrow7 = MakeArrow(90 + bpm * a * 0.5f, 2, 13, 0, 1);
                        Arrow arrow8 = MakeArrow(90 + bpm * a, 0, 13, 1, 0);
                        Arrow arrow9 = MakeArrow(80 + bpm * a * 0.5f, 1, 13, 0, 2);
                        Arrow arrow10 = MakeArrow(80 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow11 = MakeArrow(80 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow12 = MakeArrow(80 + bpm * a, 1, 13, 1, 2);
                        Arrow arrow13 = MakeArrow(85 + bpm * a * 2, 1, 13, 1, 2);
                        Arrow arrow14 = MakeArrow(85 + bpm * a, 1, 13, 0, 2);
                        Arrow arrow15 = MakeArrow(90 + bpm * a, 3, 13, 0, 0);
                        Arrow arrow16 = MakeArrow(95 + bpm * a * 0.5f, 0, 13, 1, 1);
                        Arrow arrow17 = MakeArrow(80 + bpm * a, 0, 13, 1, 2);
                        Arrow arrow18 = MakeArrow(85 + bpm * a * 2.5f, 2, 13, 0, 2);
                        Arrow arrow19 = MakeArrow(85 + bpm * a, 3, 13, 1, 2);
                        Arrow arrow20 = MakeArrow(85 + bpm * a * 0.5f, 0, 13, 1, 2);
                        CreateEntity(arrow1);
                        CreateEntity(arrow2);
                        CreateEntity(arrow3);
                        CreateEntity(arrow4);
                        CreateEntity(arrow5);
                        CreateEntity(arrow6);
                        CreateEntity(arrow7);
                        CreateEntity(arrow8);
                        CreateEntity(arrow9);
                        CreateEntity(arrow10);
                        CreateEntity(arrow11);
                        CreateEntity(arrow12);
                        CreateEntity(arrow13);
                        CreateEntity(arrow14);
                        CreateEntity(arrow15);
                        CreateEntity(arrow16);
                        CreateEntity(arrow17);
                        CreateEntity(arrow18);
                        CreateEntity(arrow19);
                        CreateEntity(arrow20);
                        AddInstance(new TimeRangedEvent(bpm * 16 - 2, bpm * 24 + 2, () =>
                        {
                            arrow1.Delay(1);
                            arrow2.Delay(1);
                            arrow3.Delay(1);
                            arrow4.Delay(1);
                            arrow5.Delay(1);
                            arrow6.Delay(1);
                            arrow7.Delay(1);
                            arrow8.Delay(1);
                            arrow9.Delay(1);
                            arrow10.Delay(1);
                            arrow11.Delay(1);
                            arrow12.Delay(1);
                            arrow13.Delay(1);
                            arrow14.Delay(1);
                            arrow15.Delay(1);
                            arrow16.Delay(1);
                            arrow17.Delay(1);
                            arrow18.Delay(1);
                            arrow19.Delay(1);
                            arrow20.Delay(1);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 40, bpm * 100, () =>
                         {
                             arrow1.Delay(1.5f);
                             arrow2.Delay(1.5f);
                             arrow3.Delay(1.5f);
                             arrow4.Delay(1.5f);
                             arrow5.Delay(1.5f);
                             arrow6.Delay(1.5f);
                             arrow7.Delay(1.5f);
                             arrow8.Delay(1.5f);
                             arrow9.Delay(1.5f);
                             arrow10.Delay(1.5f);
                             arrow11.Delay(1.5f);
                             arrow12.Delay(1.5f);
                             arrow13.Delay(1.5f);
                             arrow14.Delay(1.5f);
                             arrow15.Delay(1.5f);
                             arrow16.Delay(1.5f);
                             arrow17.Delay(1.5f);
                             arrow18.Delay(1.5f);
                             arrow19.Delay(1.5f);
                             arrow20.Delay(1.4f);
                         }));
                        AddInstance(new TimeRangedEvent(bpm * 80, 1, () =>
                        {
                            arrow1.Dispose();
                            arrow2.Dispose();
                            arrow3.Dispose();
                            arrow4.Dispose();
                            arrow5.Dispose();
                            arrow6.Dispose();
                            arrow7.Dispose();
                            arrow8.Dispose();
                            arrow9.Dispose();
                            arrow10.Dispose();
                            arrow11.Dispose();
                            arrow12.Dispose();
                            arrow13.Dispose();
                            arrow14.Dispose();
                            arrow15.Dispose();
                            arrow16.Dispose();
                            arrow17.Dispose();
                            arrow18.Dispose();
                            arrow19.Dispose();
                            arrow20.Dispose();
                        }));
                    }
                }//大哥换谱面
            }

            private static class DevilsBarrage
            {
                public static void Intro6a7()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","R","/",

                "R","/","/","R",
                "/","/","R","/",
                "/","R","/","/",
                "R","/","/","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro8a9()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","/",
                "/","/","R","/",

                "0,32","1,32","2,32","/,32","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "2,32","1,32","0,32",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "0,32")
                        {
                            CreateArrow(80 + BeattimeB, 0, 13, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "1,32")
                        {
                            CreateArrow(80 + BeattimeB, 1, 13, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "2,32")
                        {
                            CreateArrow(80 + BeattimeB, 2, 13, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == " /,32")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","R",
                "/","/","R","/",
                "R","/","/","/",
                "R","/","R","/",

                "R,32","/,32","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "R,32"
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R,32")
                        {
                            CreateArrow(80 + BeattimeR, "R", 7.2f, 1, 0);
                            BeattimeR += 0.5f * bpm;
                        }
                        else if (Redarrowway[a] == " /,32")
                        {
                            BeattimeR += 0.5f * bpm;
                        }
                    }
                }
                public static void Intro11a12()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R","+0","+2","+0",
                "R","+0","+2","+0",
                "R","+0","+2","+0",

                "R","+0","+2","+0",
                "R","+0","+2","+0",
                "R","/","+2","+0",
                "R","+0","+2","+0",

            };
                    int[] Bluearrowtpye =
                {
                 0,0,0,0,
                 0,0,1,1,
                 0,0,1,1,
                 0,0,1,1,

                 0,0,1,1,
                 0,0,1,1,
                 0,0,1,1,
                 0,0,1,1,
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, Bluearrowtpye[a]);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",

                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0, ArrowAttribute.SpeedUp);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro13a14()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","+2","+0",
                "R","+0","+2","+0",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","/","/",
                "R","+1","+1","+1",
                "+1","+1","+1","+1",

            };
                    int[] Bluearrowtpye =
                {
                 0,0,1,1,
                 0,0,1,1,
                 0,0,0,0,
                 0,0,0,0,

                 0,0,0,0,
                 0,0,0,0,
                 0,0,0,0,
                 0,0,0,0,
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, Bluearrowtpye[a]);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "RS","/","/","/",
                "RS","/","/","/",
                "R","/","R","/",
                "R","/","R","/",

                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "RS")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0, ArrowAttribute.SpeedUp);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;

                        }
                    }
                }
                public static void Intro15()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R,32","+1,32","+2,32","/,32","/","/",
                "R,32","+1,32","+2,32","/,32","/","/",
                "R,32","+1,32","+2,32","/,32","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, "R", 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R,32")
                        {
                            CreateArrow(80 + BeattimeB, "R", 7.2f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "+1,32")
                        {
                            CreateArrow(80 + BeattimeB, LastRand + 1, 7.2f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "+2,32")
                        {
                            CreateArrow(80 + BeattimeB, LastRand + 2, 7.2f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "/,32")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, "R", 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro16a17()
                {
                    float BeattimeB = 0;

                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","R",
                "R","/","R","+0",

                "R","/","R","/",
                "R","+0","R","+0",
                "R","+0","R","/",
                "R","/","R","+0",

            };

                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "/","/","/","/",
                "R","+0","/","/",

                "R","+0","R","+0",
                "R","/","R","/",
                "R","/","R","+0",
                "R","+0","R","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro18()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "0","1","2","3",
                "/","/","/","/",
                "0","2","0","2",
                "0","2","0","2",
                "0",
            };
                    int[] Arrowtype =
                    {
                    1,1,1,1,
                    1,1,1,1,
                    0,0,0,0,
                    0,0,0,0,
                    1,
                };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10f, 0, Arrowtype[a]);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "0","3","2","1",
                "2","0","2","0",
                "2","0","2","0",
                "2",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, Arrowtype[a]);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro19()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro20t22()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","+0","R","+0",
                "R","/","R","/",

                "R","R","R","/",
                "/","/","R","/",
                "R","+0","R","+0",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","R",
                "/","R","/","/",
                "R","/","R","/",
                "R","+0","R","+0",

                "/","/","R","/",
                "/","/","R","/",
                "R","/","R","/",
                "R","+0","R","+0",

                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro23a24()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","R","R",
                "R","+0","R","R",
                "+0","R","R","+0",
                "R","R","+0","R",

                "R","+0","R","R",
                "+0","R","R","+0",
                "R","R","+0","R",
                "R","+0","R","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","R",
                "R","/","R","/",

                "R","/","/","/",
                "R","R","R","/",
                "R","/","R","R",
                "R","/","R","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro26()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                    "R","/","/","R",
                    "/","/","R","/",
                    "R","/","/","R",
                    "/","/","R","/",
            };
                    int[] BlueArrowtype =
                    {
                    2,2,2,2,
                    2,2,2,2,
                    0,0,0,0,
                    0,0,0,0,
                };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, BlueArrowtype[a]);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                    "R","/","/","R",
                    "/","/","R","/",
                    "R","/","/","R",
                    "/","/","R","/",

            };
                    int[] RedArrowtype =
                    {
                    0,0,0,0,
                    0,0,0,0,
                    2,2,2,2,
                    2,2,2,2,
                };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, RedArrowtype[a]);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro27()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    4,4,1.333f,1.333f,1.333f,1.333f,1.333f,1.333f,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6, 0, 0);
                        CreateArrow(80 + BeatArrow, "R", 6, 0, 0);
                        CreateArrow(80 + BeatArrow, "R", 6, 0, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro28a29()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R","/","R","/",
                "R","/","/","/",
                "R","/","/","/",

                "R","/","/","/",
                "R","/","R","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "R","/","R","/",
                "/","/","/","/",
                "/","/","/","/",

                "R","/","/","/",
                "R","/","R","/",
                "R","/","/","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro30a31()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",

                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "R","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro33a34()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",

                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro35a36()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","R","R",
                "/","/","R","R",
                "/","/","R","R",
                "/","/","R","R",

                "R","/","/","/",
                "R","R","R","R",
                "R","+0","R","R",
                "R","+0","R","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "R","R","/","/",
                "R","R","/","/",
                "R","R","/","/",

                "R","R","R","R",
                "/","/","/","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro37a38()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","R","R","+0",
                "R","R","R","+0",
                "R","R","R","+0",
                "R","R","R","+0",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",

                "R","/","R","/",
                "R","/","R","/",
                "R","R","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro39a40()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","R",
                "R","/","R","/",
                "R","/","R","R",
                "R","R","R","R",

                "R","+0","R","R",
                "R","R","R","R",
                "R","+0","R","R",
                "R","/","R","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "R","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro41a42()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",

                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",


            };
                    int[] Bluearrowtpye =
    {
                 0,1,0,1,
                 0,1,0,1,
                 0,1,0,1,
                 0,1,0,1,

                 0,1,0,1,
                 0,1,0,1,
                 0,1,0,1,
                 0,1,0,1,
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, Bluearrowtpye[a]);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0, ArrowAttribute.SpeedUp);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro43t50()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    6,2,2,1,1,2,2+2,
                    1,1+2,2+1,1,1,1,2,2,
                    2,2+1,2,1,2,1,1,1,1,1,1,
                    2+1,1,1,1,2,3,3,2+4,
                    2+2,1,2,1,2,2,
                    2,1,1,2,2,2,2,1,1,2,
                    3,3,2,1,1+2,2,1,1,
                    8,8
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 7.2f, 0, 0);
                        CreateArrow(80 + BeatArrow, "+1", 7.2f, 1, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro51t54B()
                {
                    float BeattimeB = 0;
                    int[] Bluearrowway =
                    {
                1,2,1,2,
                1,2,1,2,
                1,2,1,2,
                1,1,1,2,

                1,2,1,2,
                1,1,1,2,
                1,2,1,2,
                1,2,1,2,

                1,2,1,2,
                1,2,1,1,
                1,2,1,2,
                1,2,2,2,

                1,2,1,2,
                1,2,1,1,
                1,2,1,2,
                1,2,1,1,
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == 2)
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != 2)
                        {
                            CreateArrow(60 + BeattimeB, Rand(3, 5), 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro51t54R()
                {
                    float BeattimeR = 0;
                    int[] Redarrowway =
                    {
                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,1,0,

                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,1,0,
                    1,0,0,0,
                    1,0,1,0,
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == 0)
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != 0)
                        {
                            CreateArrow(60 + BeattimeR, Rand(1, 3), 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro56t58B()
                {
                    float BeattimeB = 0;
                    int[] Bluearrowway =
                    {
                1,2,1,2,
                1,2,1,1,
                1,2,1,2,
                1,2,1,1,

                1,2,1,2,
                1,2,1,2,
                1,2,1,2,
                1,2,1,1,

                1,0,1,0,
                1,0,1,0,
                1,0,1,0,
                1,0,1,0,
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == 2)
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == 1)
                        {
                            CreateArrow(60 + BeattimeB, Rand(1, 3), 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == 0)
                        {
                            CreateArrow(60 + BeattimeB, LastRand, 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro56t58R()
                {
                    float BeattimeR = 0;
                    int[] Redarrowway =
                    {
                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == 0)
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != 0)
                        {
                            CreateArrow(60 + BeattimeR, Rand(3, 5), 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro60()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "0","2","0","2",
                "0","2","0","2",
                "0","2","0","2",
                "0","2","0","2",

                "0","2","0","2",
                "0","2","0","2",
                "0","2","0","2",
                "0","2","0","2",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "0")
                        {
                            SetPlayerMission(0);
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "2")
                        {
                            SetPlayerMission(0);
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                }
                public static void Intro62()
                {


                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","R","R","/",
                "R","R","/","R",
                "R","/","R","R",
                "R","R","R","R",

                "R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.5f, 0, 0);
                            CreateArrow(80 + BeattimeB, LastRand + 2, 10, 1, 1);
                            BeattimeB += bpm;
                        }
                    }

                }

                public static void Intro63t66()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "R","/","/","/",
                "/","/","R","R",

                "R","/","/","/",
                "/","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","/","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R0")
                        {
                            CreateArrow(80 + BeattimeB, "R", 7.2f, 0, 0);
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","/","R",
                "R","/","/","R",
                "R","/","R","R",

                "R","/","/","/",
                "/","/","/","R",
                "/","R","/","R",
                "/","R","/","R",

                "/","/","/","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro67a68()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                     4,2,1,1,2,1.333f,1.333f,1.333f,2,2,
                     2,1,1,2,1,1,3,3,2,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 7.2f, Rand(0, 1), 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro69t72()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","R",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","/","/",
                "R","/","R","R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R0")
                        {
                            CreateArrow(80 + BeattimeB, "R", 7.2f, 0, 0);
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

                "R","/","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","R","R",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro77a78()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    0,6,1,1,2,2,0.666f,0.666f,0.666f,1,1,
                    6,1,1,6,1,1,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 7.2f, Rand(0, 1), 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro79a80()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+1","+1",
                "R","+1","+1",
                "R","+1","+1",
                "R","+1","+1",
                "R","R","R","R",

                "R","/","R","R",
                "/","R","/","R",
                "R","/","R","+0",
                "/","R","+0","R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","R",
                "/","/","R","/",
                "/","R","/","/",
                "R","R","R","R",

                "R","/","R","R",
                "/","R","/","R",
                "R","/","R","+0",
                "/","R","+0","R",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro81a82()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","R",
                "/","/","R","/",
                "/","R","/","/",
                "R","R","R","R",

                "R","/","/","R",
                "/","/","R","/",
                "0","2","0","2",
                "0","2","0","2",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "0")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10f, 0, 1);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "2")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10f, 0, 1);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","R",
                "/","/","R","/",
                "/","R","/","/",
                "R","R","R","R",

                "R","/","/","R",
                "/","/","R","/",
                "2","0","2","0",
                "2","0","2","0",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "0")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "2")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro83t86()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","R","/","R",
                "/","R","/","/",
                "R","R","/","R",
                "/","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "R","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "/","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","/","/","R",
                "/","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.5f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","/",
                "/","/","/","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","R",
                "/","/","R","/",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","R","R",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.5f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro87t90()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "/","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "/","R","R","R",
                "/","R","/","R",

                "R","/","/","R",
                "/","/","R","/",
                "R","/","/","R",
                "/","/","R","/",

                "R","/","+1","/",
                "+1","/","+1","/",
                "R","/","+1","/",
                "+1","/","+1","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.5f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","R",
                "/","/","R","/",
                "R","/","/","R",
                "/","/","R","/",

                "R","R","/","+1",
                "/","+1","/","+1",
                "/","+1","/","+1",
                "/","+1","/","+1",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.5f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro91t93()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "/","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "R","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "/","R","/","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.5f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.5f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro94()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    3,3,2,1.333f,1.333f,1.333f,1.333f,1.333f,1.333f
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 7.5f, 0, 0);
                        CreateArrow(80 + BeatArrow, "R", 7.5f, 0, 0);
                        CreateArrow(80 + BeatArrow, "R", 7.5f, 1, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro95a96()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    2,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,2
,                   };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 7.5f, 0, 0);
                        CreateArrow(80 + BeatArrow + bpm, "R", 7.5f, 1, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro97()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    1,1,0.666f,0.666f,0.666f,0.666f,0.666f,0.666f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,
                    0.666f,0.666f,0.666f,0.666f,0.666f,0.666f
,                   };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, 0 + a % 2 * 2, 12.5f, 0, 0, ArrowAttribute.RotateL);
                        CreateArrow(80 + BeatArrow, 1 + a % 2 * 2, 12.5f, 1, 1, ArrowAttribute.SpeedUp);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro98()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","R","R","R",



            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 8, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "R","R","R","R",



            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 8, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
            }

            private static class ExtremeBarrage
            {

                private static string[] stringcpu(string[] origin)
                {
                    for (int a = 0; a < origin.Length; a++)
                    {
                        if (origin[a] != "/")
                        {
                            if (origin[a].Length == 1)
                            {
                                origin[a] += "00";
                            }
                            if (origin[a].Length == 2)
                            {
                                origin[a] += "0";
                            }
                        }
                    }
                    return origin;
                }

                public static void Intro6a7()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","+0","/",

                "+0","/","/","+0",
                "/","/","R","/",
                "/","+0","/","/",
                "+0","/","/","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro8a9()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","/",
                "/","/","R","/",

                "0,32","1,32","2,32","/,32","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "2,32","1,32","0,32",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "0,32")
                        {
                            CreateArrow(80 + BeattimeB, 0, 10, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "1,32")
                        {
                            CreateArrow(80 + BeattimeB, 1, 10, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "2,32")
                        {
                            CreateArrow(80 + BeattimeB, 2, 10, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == " /,32")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","+0",
                "/","/","+0","/",
                "R","/","/","/",
                "+0","/","+0","/",


            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R,32")
                        {
                            CreateArrow(80 + BeattimeR, "R", 6.4f, 1, 0);
                            BeattimeR += 0.5f * bpm;
                        }
                        else if (Redarrowway[a] == " /,32")
                        {
                            BeattimeR += 0.5f * bpm;
                        }
                    }
                }
                public static void Intro11a12()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R","+0","+0","+0",
                "R","+0","+0","+0",
                "R","+0","+0","+0",

                "R","+0","+0","+0",
                "R","+0","+0","+0",
                "R","/","+0","+0",
                "R","+0","+0","+0",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",

                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro13a14()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","+0","+0",
                "R","+0","+0","+0",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","/","/",
                "R","+1","+1","+1",
                "+1","+1","+1","+1",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","+0","/",
                "R","/","+0","/",

                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;

                        }
                    }
                }
                public static void Intro15()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R,32","/,32","/","/","/",
                "R,32","/,32","/","/","/",
                "R,32","/,32","/","/","/"

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, "R", 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R,32")
                        {
                            CreateArrow(80 + BeattimeB, "R", 6.4f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "+1,32")
                        {
                            CreateArrow(80 + BeattimeB, LastRand + 1, 6.4f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "+2,32")
                        {
                            CreateArrow(80 + BeattimeB, LastRand + 2, 6.4f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "/,32")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, "R", 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro16a17()
                {
                    float BeattimeB = 0;

                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","R",
                "R","/","R","+0",

                "R","/","R","/",
                "R","+0","R","+0",
                "R","+0","R","/",
                "R","/","R","+0",

            };

                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "/","/","/","/",
                "R","+0","/","/",


            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro18()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "0","1","2","3",
                "0","1","2","3",
                "0","2","0","2",
                "0","2","0","2",
                "0",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 8f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro19()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro20t22()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","+0",
                "R","+0","R","/",
                "R","+0","R","+0",
                "R","+0","R","+0",

                "R","R","R","/",
                "/","/","R","/",
                "R","+0","R","+0",
                "R","+0","R","+0",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
                "/","R","/","R",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro23a24()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","R","R",
                "R","+0","R","R",
                "+0","R","R","+0",
                "R","R","+0","R",

                "R","+0","R","R",
                "+0","R","R","+0",
                "R","R","+0","R",
                "R","+0","R","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro26()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                    "R","/","/","R",
                    "/","/","R","/",
                    "R","/","/","R",
                    "/","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 2);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                    "R","/","/","R",
                    "/","/","R","/",
                    "R","/","/","R",
                    "/","/","R","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, LastRand, 6.4f, 1, 2);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro27()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    4,4,1.333f,1.333f,1.333f,1.333f,1.333f,1.333f,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6.4f, 0, 0);
                        CreateArrow(80 + BeatArrow, "R", 6.4f, 1, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro28a29()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R","/","R","/",
                "R","/","/","/",
                "R","/","/","/",

                "R","/","/","/",
                "R","/","R","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "R","/","R","/",
                "/","/","/","/",
                "/","/","/","/",

                "R","/","/","/",
                "R","/","R","/",
                "R","/","/","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro30a31()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","/","/","/",

                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "R","/","/","/",

                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "R","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro33a34()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","/","/","/",

                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "R","/","R","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "R","R","/","R",

                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro35a36()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",

                "R","/","/","/",
                "R","+0","+0","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",

                "R","+0","+0","+0",
                "R","/","/","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro37a38()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",

                "R","+0","+0","+0",
                "R","+0","+0","+0",
                "R","+0","+0","+0",
                "R","+0","+0","+0",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",

                "R","/","/","/",
                "R","/","/","/",
                "R","R","R","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro39a40()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","R",
                "R","/","R","/",
                "R","/","R","R",
                "R","+0","R","R",

                "R","+0","+0","+0",
                "R","R","R","+0",
                "R","+0","+0","+0",
                "R","/","R","+0",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "R","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro41a42()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",

                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",


            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0, ArrowAttribute.SpeedUp);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro43t50()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    6,2,2,1,1,2,2+2,
                    1,1+2,2+1,1,1,1,2,2,
                    2,2+1,2,1,2,1,1,1,1,1,1,
                    2+1,1,1,1,2,3,3,2+4,
                    2+2,1,2,1,2,2,
                    2,1,1,2,2,2,2,1,1,2,
                    3,3,2,1,1+2,2,1,1,
                    8,8
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6.4f, 0, 0);
                        CreateArrow(80 + BeatArrow, "+0", 6.4f, 1, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro51t54B()
                {
                    float BeattimeB = 0;
                    int[] Bluearrowway =
                    {
                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,1,0,

                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,1,0,
                    1,0,0,0,
                    1,0,1,0,
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == 0)
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != 0)
                        {
                            SetPlayerMission(1);
                            CreateArrow(60 + BeattimeB, Rand(3, 5), 6.4f, 0, 0);
                            SetPlayerMission(0);
                            CreateArrow(60 + BeattimeB, LastRand + 2, 6.4f, 1, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro51t54R()
                {

                }
                public static void Intro56t58B()
                {
                    float BeattimeB = 0;
                    int[] Bluearrowway =
                    {
                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == 0)
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == 1)
                        {
                            SetPlayerMission(0);
                            CreateArrow(60 + BeattimeB, Rand(1, 3), 6.4f, 0, 0);
                            SetPlayerMission(1);
                            CreateArrow(60 + BeattimeB, LastRand + 2, 6.4f, 1, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro56t58R()
                {

                }
                public static void Intro60()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "0",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "0")
                        {
                            SetPlayerMission(0);
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "2")
                        {
                            SetPlayerMission(0);
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                }
                public static void Intro62()
                {


                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","R","R","/",
                "R","R","/","R",
                "R","/","R","R",
                "R","R","R","R",

                "R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.8f, 0, 0);
                            CreateArrow(80 + BeattimeB, LastRand, 6.8f, 1, 0);
                            BeattimeB += bpm;
                        }
                    }

                }

                public static void Intro63t66()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "R","/","/","/",
                "/","/","R","R",

                "R","/","/","/",
                "/","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","R",
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","/","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","/","R",
                "+0","/","/","R",
                "R","/","R","+0",

                "R","/","/","/",
                "/","/","/","R",
                "/","R","/","R",
                "/","R","/","R",

                "/","/","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro67a68()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                     4,2,1,1,2,1.333f,1.333f,1.333f,2,2,
                     2,1,1,2,1,1,3,3,2,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6.4f, Rand(0, 1), 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro69t72()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","R",

                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","/","/",
                "R","/","/","/",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","/","/",
                "R","/","R","+0",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R0")
                        {
                            CreateArrow(80 + BeattimeB, "R", 6.4f, 0, 0);
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

                "R","/","/","R",
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",

                "R","/","/","/",
                "/","/","/","/",
                "/","/","R","/",
                "/","/","R","/",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","R","+0",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro77a78()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    0,6,1,1,2,2,0.666f,0.666f,0.666f,1,1,
                    6,1,1,6,1,1,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6.4f, Rand(0, 1), 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro79a80()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","+0",
                "R","+0","+0",
                "R","+0","+0",
                "R","+0","+0",
                "R","/","R","/",

                "R","/","R","R",
                "/","R","/","R",
                "R","/","R","+0",
                "/","R","+0","R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","R",
                "/","/","R","/",
                "/","R","/","/",
                "/","R","/","R",

                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro81a82()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","R",
                "/","/","R","/",
                "/","R","/","/",
                "R","R","R","R",

                "R","/","/","R",
                "/","/","R","/",
                "0","2","0","2",
                "0","2","0","2",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.4f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "0")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10f, 0, 1);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "2")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10f, 0, 1);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "R","/","/","R",
                "/","/","R","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.4f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "0")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "2")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro83t86()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","+0",
                "/","/","R","+0",
                "R","/","R","+0",
                "/","/","R","+0",

                "R","/","R","+0",
                "/","/","R","+0",
                "R","/","R","+0",
                "R","/","R","+0",

                "R","/","R","+0",
                "/","/","R","+0",
                "R","/","R","+0",
                "/","/","R","+0",

                "R","R","/","R",
                "/","R","/","R",
                "R","/","/","R",
                "/","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.8f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","+0","/","/",
                "R","+0","/","/",
                "R","/","/","/",
                "/","/","/","/",

                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",

                "R","+0","/","/",
                "R","+0","/","/",
                "R","/","/","+0",
                "/","/","+0","/",

                "R","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","R","+0",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.8f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro87t90()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","+0",
                "/","/","R","+0",
                "R","/","R","+0",
                "/","/","R","+0",

                "R","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",

                "R","/","/","+0",
                "/","/","R","/",
                "R","/","/","+0",
                "/","/","R","/",

                "R","/","+1","/",
                "+1","/","+1","/",
                "R","/","+1","/",
                "+1","/","+1","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.8f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",

                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","R","/",
                "R","+0","/","/",

                "R","/","/","+0",
                "/","/","R","/",
                "R","/","/","+0",
                "/","/","R","/",

                "R","R","/","+1",
                "/","+1","/","+1",
                "/","+1","/","+1",
                "/","+1","/","+1",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.8f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro91t93()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","+0",
                "/","/","R","+0",
                "R","/","R","+0",
                "/","/","R","+0",

                "R","/","R","+0",
                "/","/","R","+0",
                "R","/","R","+0",
                "R","/","R","+0",

                "R","/","R","+0",
                "/","/","R","+0",
                "R","/","R","+0",
                "/","/","R","+0",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.8f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.8f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro94()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    3,3,2,1.333f,1.333f,1.333f,1.333f,1.333f,1.333f
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6.8f, 0, 0);
                        CreateArrow(80 + BeatArrow, "R", 6.8f, 1, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro95a96()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    2,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,2
,                   };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6.8f, 0, 0);
                        CreateArrow(80 + BeatArrow + bpm, "R", 6.8f, 1, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro97()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    1,1,0.666f,0.666f,0.666f,0.666f,0.666f,0.666f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,
                    0.666f,0.666f,0.666f,0.666f,0.666f,0.666f
,                   };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, 0 + a % 2 * 2, 12.5f, 0, 0, ArrowAttribute.RotateL);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro98()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","/","/",
                "R","+0","+0","/",



            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7.3f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","R","+0",
                "/","/","R","+0",
                "/","/","R","+0",
                "R","+0","+0","/",



            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 7.3f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
            }

            private static class NormalBarrage
            {
                public static void Intro6a7()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro8a9()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","/",
                "/","/","R","/",

                "R","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "0,32")
                        {
                            CreateArrow(80 + BeattimeB, 0, 13, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "1,32")
                        {
                            CreateArrow(80 + BeattimeB, 1, 13, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "2,32")
                        {
                            CreateArrow(80 + BeattimeB, 2, 13, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == " /,32")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "+0","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

                "+0"
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R,32")
                        {
                            CreateArrow(80 + BeattimeR, "R", 6f, 1, 0);
                            BeattimeR += 0.5f * bpm;
                        }
                        else if (Redarrowway[a] == " /,32")
                        {
                            BeattimeR += 0.5f * bpm;
                        }
                    }
                }
                public static void Intro11a12()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro13a14()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","/","/",
                "R","/","+1","/",
                "+1","/","+1","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "+0","/","+0","/",
                "+0","/","+0","/",

                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "RS")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 10f, 1, 0, ArrowAttribute.SpeedUp);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6f, 1, 0);
                            BeattimeR += bpm;

                        }
                    }
                }
                public static void Intro15()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, "R", 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R,32")
                        {
                            CreateArrow(80 + BeattimeB, "R", 6f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "+1,32")
                        {
                            CreateArrow(80 + BeattimeB, LastRand + 1, 6f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "+2,32")
                        {
                            CreateArrow(80 + BeattimeB, LastRand + 2, 6f, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "/,32")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "+0","/","/","/",
                "+0","/","/","/",
                "+0","/","/","/",
                "+0","/","/","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, "R", 6f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro16a17()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","/","/",
                "R","+0","R","+0",

                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",

            };

                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro18()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "0","0","1","1",
                "2","2","3","3",
                "0","0","0","2",
                "0","0","0","2",
                "0",
            };

                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 8f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro19()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "R","/","/","/",
                "/","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro20t22()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","+0",
                "R","+0","R","/",
                "R","+2","R","+0",
                "R","+0","R","+0",

                "R","R","R","/",
                "/","/","R","/",
                "R","+0","R","+0",
                "R","+0","R","+0",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro23a24()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","R","/",
                "R","/","R","/",
                "R","/","R","+0",
                "R","/","R","/",

                "R","/","/","/",
                "R","+0","R","/",
                "R","/","R","+0",
                "R","/","R","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro26()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                    "R","/","/","R",
                    "/","/","R","/",
                    "R","/","/","R",
                    "/","/","R","/",
            };
                    int[] BlueArrowtype =
                    {
                    2,2,2,2,
                    2,2,2,2,
                    2,0,0,2,
                    0,0,2,0,
                };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, BlueArrowtype[a]);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro27()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    4,4,1.333f,1.333f,1.333f,1.333f,1.333f,1.333f,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6, 0, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro28a29()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R","/","R","/",
                "R","/","/","/",
                "R","/","/","/",

                "R","/","/","/",
                "R","/","R","/",
                "R","/","/","/",
                "R","/","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro30a31()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","/","/",

                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","/","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro33a34()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","/","/",

                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro35a36()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro37a38()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro39a40()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","+0",
                "R","/","R","/",
                "R","/","R","+0",
                "R","+0","R","+0",

                "R","+0","+0","+0",
                "R","+0","+0","+0",
                "R","+0","+0","+0",
                "R","/","+0","+0",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro41a42()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",

                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",


            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro43t50()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    6,2,2,1,1,2,2+2,
                    1,1+2,2+1,1,1,1,2,2,
                    2,2+1,2,1,2,1,1,1,1,1,1,
                    2+1,1,1,1,2,3,3,2+4,
                    2+2,1,2,1,2,2,
                    2,1,1,2,2,2,2,1,1,2,
                    3,3,2,1,1+2,2,1,1,
                    8,8
                    };
                    string[] Arrowway =
                    {
                        "R","R","R","R","+0","+0","R","R",
                        "+0","R","R","+0","+0","+0","R","R",
                        "R","R","R","+0","R","+0","+0","+0","+0","+0","+0",
                        "R","+0","+0","+0","R","R","R",
                        "R","+0","R","+0","R","R",
                        "R","R","R","R","R","R","R","R","R","R",
                        "R","R","R","R","R","R","R","R",
                        "R","R",
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, Arrowway[a], 6f, 0, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro51t54R()
                {
                    float BeattimeR = 0;
                    int[] Redarrowway =
                    {
                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,1,0,

                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,1,0,
                    1,0,0,0,
                    1,0,1,0,
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == 0)
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != 0)
                        {
                            CreateArrow(60 + BeattimeR, Rand(1, 3), 5f, 0, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro56t58R()
                {
                    float BeattimeR = 0;
                    int[] Redarrowway =
                    {
                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
                    1,0,0,0,

                    1,0,0,0,
                    0,0,0,0,
                    1,0,0,0,
                    1,0,0,0,
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == 0)
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != 0)
                        {
                            CreateArrow(60 + BeattimeR, Rand(3, 5), 5f, 0, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro60()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "0"


            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "0")
                        {
                            SetPlayerMission(0);
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                        else if (Bluearrowway[a] == "2")
                        {
                            SetPlayerMission(0);
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 10, 0, 0);
                            BeattimeB += 0.5f * bpm;
                        }
                    }
                }
                public static void Intro62()
                {


                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","+0","/",
                "R","+0","/","R",
                "+0","/","R","+0",
                "+0","+0","+0","+0",

                "+0",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.8f, 0, 0);
                            CreateArrow(80 + BeattimeB, LastRand, 6.8f, 1, 0);
                            BeattimeB += bpm;
                        }
                    }

                }

                public static void Intro63t66()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "/","/","R","R",
                "R","/","/","R",
                "R","/","R","R",

                "R","/","/","/",
                "/","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","/","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R0")
                        {
                            CreateArrow(80 + BeattimeB, "R", 6f, 0, 0);
                        }
                    }
                }
                public static void Intro67a68()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                     4,2,1,1,2,1.333f,1.333f,1.333f,2,2,
                     2,1,1,2,1,1,3,3,2,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6f, 0, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro69t72()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","+0",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","/","/",
                "/","/","R","R",
                "R","/","/","/",
                "R","/","R","R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R0")
                        {
                            CreateArrow(80 + BeattimeB, "R", 6f, 0, 0);
                        }
                    }
                }
                public static void Intro77a78()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    0,6,1,1,2,2,0.666f,0.666f,0.666f,1,1,
                    6,1,1,6,1,1,
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6f, 0, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro79a80()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+1","+0",
                "R","+1","+0",
                "R","+1","+0",
                "R","+1","+0",
                "R","R","+0","R",

                "R","/","R","+0",
                "/","R","/","R",
                "R","/","R","+0",
                "/","R","+0","R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro81a82()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","R",
                "/","/","R","/",
                "/","R","/","/",
                "R","R","R","R",

                "R","/","/","R",
                "/","/","R","/",
                "2","2","0","2",
                "2","2","0","2",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "0")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 8f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "2")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 8f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro83t86()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","R","/","R",
                "/","R","/","/",
                "R","R","/","R",
                "/","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "R","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "/","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","/","/","R",
                "/","/","R","R",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.3f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro87t90()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "/","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "/","R","R","R",
                "/","R","/","R",

                "R","/","/","R",
                "/","/","R","/",
                "R","/","/","R",
                "/","/","R","/",

                "R","/","+1","/",
                "+1","/","+1","/",
                "+1","/","+1","/",
                "+1","/","+1","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.3f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro91t93()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "/","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "R","R","/","R",

                "R","R","/","R",
                "/","R","/","R",
                "R","R","/","R",
                "/","R","/","R",

            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.3f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro94()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    3,3,2,1.333f,1.333f,1.333f,1.333f,1.333f,1.333f
                    };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6.3f, 0, 0);

                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro95a96()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                    2,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,2
,                   };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 6.3f, 0, 0);
                        CreateArrow(80 + BeatArrow + bpm, LastRand, 6.3f, 1, 0);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro97()
                {
                    float BeatArrow = 0;
                    float[] Arrow =
                        {
                        3,3,3,3,2,2
,                   };
                    for (int a = 0; a < Arrow.Length; a++)
                    {
                        CreateArrow(80 + BeatArrow, "R", 10f, 0, 0, ArrowAttribute.SpeedUp);
                        CreateArrow(80 + BeatArrow, LastRand, 10f, 1, 1, ArrowAttribute.SpeedUp);
                        BeatArrow += Arrow[a] * bpm;
                    }
                }
                public static void Intro98()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","+0","R",



            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 7, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
            }
            public void Noob()
            {
            }
            public void ExtremePlus()
            {
                DevilsEffects();
                if (GametimeF == (int)(bpm * 5 * 16 - 80)) DevilsBarrage.Intro6a7();
                if (GametimeF == (int)(bpm * 7 * 16 - 80)) DevilsBarrage.Intro8a9();
                if (GametimeF == (int)(bpm * 10 * 16 - 80)) CreateArrow(80, "R", 5, 1, 0);
                if (GametimeF == (int)(bpm * 11 * 16 - 80)) DevilsBarrage.Intro11a12();
                if (GametimeF == (int)(bpm * 13 * 16 - 80)) DevilsBarrage.Intro13a14();
                if (GametimeF == (int)(bpm * 15 * 16 - 80)) DevilsBarrage.Intro15();
                if (GametimeF == (int)(bpm * 16 * 16 - 80)) DevilsBarrage.Intro16a17();
                if (GametimeF == (int)(bpm * 18 * 16 - 80)) DevilsBarrage.Intro18();
                if (GametimeF == (int)(bpm * 19 * 16 - 80)) DevilsBarrage.Intro19();
                if (GametimeF == (int)(bpm * 20 * 16 - 80)) DevilsBarrage.Intro20t22();
                if (GametimeF == (int)(bpm * 23 * 16 - 80)) DevilsBarrage.Intro23a24();
                if (GametimeF == (int)(bpm * 25 * 16 - 80)) DevilsBarrage.Intro26();
                if (GametimeF == (int)(bpm * 26 * 16 - 80)) DevilsBarrage.Intro27();
                if (GametimeF == (int)(bpm * 27 * 16 - 80)) DevilsBarrage.Intro28a29();
                if (GametimeF == (int)(bpm * 29 * 16 - 80)) DevilsBarrage.Intro30a31();
                if (GametimeF == (int)(bpm * 31 * 16 - 80)) DevilsBarrage.Intro28a29();
                if (GametimeF == (int)(bpm * 33 * 16 - 80)) DevilsBarrage.Intro33a34();
                if (GametimeF == (int)(bpm * 35 * 16 - 80)) DevilsBarrage.Intro35a36();
                if (GametimeF == (int)(bpm * 37 * 16 - 80)) DevilsBarrage.Intro37a38();
                if (GametimeF == (int)(bpm * 39 * 16 - 80)) DevilsBarrage.Intro39a40();
                if (GametimeF == (int)(bpm * 41 * 16 - 80)) DevilsBarrage.Intro41a42();
                if (GametimeF == (int)(bpm * 43 * 16 - 80)) DevilsBarrage.Intro43t50();

                if (GametimeF == (int)(bpm * 51 * 16 - 60)) { SetPlayerMission(1); DevilsBarrage.Intro51t54B(); }
                if (GametimeF == (int)(bpm * 51 * 16 - 60)) { SetPlayerMission(0); DevilsBarrage.Intro51t54R(); }
                if (GametimeF == (int)(bpm * 55 * 16 - 60)) { SetPlayerMission(0); DevilsBarrage.Intro56t58B(); }
                if (GametimeF == (int)(bpm * 55 * 16 - 60)) { SetPlayerMission(1); DevilsBarrage.Intro56t58R(); }
                if (GametimeF == (int)(bpm * 58 * 16 - bpm * 16))
                {
                    SetPlayerMission(0);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 1, bpm * 8));
                }
                if (GametimeF == (int)(bpm * 58 * 16 - bpm * 8))
                {
                    SetPlayerMission(1);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 0, bpm * 32));
                }
                if (GametimeF == (int)(bpm * 58 * 16))
                {
                    SetPlayerMission(1);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 1, bpm * 32 + bpm * 16));
                }
                if (GametimeF == (int)(bpm * 61 * 16 - 80)) DevilsBarrage.Intro60();
                if (GametimeF == (int)(bpm * 63 * 16 - 80)) DevilsBarrage.Intro62();
                if (GametimeF == (int)(bpm * 67 * 16 - 80)) DevilsBarrage.Intro63t66();
                if (GametimeF == (int)(bpm * 71 * 16 - 80)) DevilsBarrage.Intro67a68();
                if (GametimeF == (int)(bpm * 73 * 16 - 80)) DevilsBarrage.Intro69t72();
                if (GametimeF == (int)(bpm * 77 * 16 - 80)) DevilsBarrage.Intro77a78();
                if (GametimeF == (int)(bpm * 79 * 16 - 80)) DevilsBarrage.Intro79a80();
                if (GametimeF == (int)(bpm * 81 * 16 - 80)) DevilsBarrage.Intro81a82();
                if (GametimeF == (int)(bpm * 83 * 16 - 80)) DevilsBarrage.Intro83t86();
                if (GametimeF == (int)(bpm * 87 * 16 - 80)) DevilsBarrage.Intro87t90();
                if (GametimeF == (int)(bpm * 91 * 16 - 80)) DevilsBarrage.Intro91t93();
                if (GametimeF == (int)(bpm * 94 * 16 - 80)) DevilsBarrage.Intro94();
                if (GametimeF == (int)(bpm * 95 * 16 - 80)) DevilsBarrage.Intro95a96();
                if (GametimeF == (int)(bpm * 97 * 16 - 80)) DevilsBarrage.Intro97();
                if (GametimeF == (int)(bpm * 98 * 16 - 80)) DevilsBarrage.Intro98();
                if (GametimeF == (int)(bpm * 99 * 16 - bpm * 12))
                {
                    CreateGB(new GreenSoulGB(bpm * 12, Rand(0, 3), 0, bpm * 64));
                    CreateGB(new GreenSoulGB(bpm * 12, Rand(0, 3), 1, bpm * 64));
                }
                if (GametimeF == (int)(bpm * 103 * 16 - 80))
                {
                    CreateArrow(80, 3, 5, 1, 0);
                }
            }
            public void Normal()
            {
                NormalEffects();
                if (GametimeF == (int)(bpm * 5 * 16 - 80)) NormalBarrage.Intro6a7();
                if (GametimeF == (int)(bpm * 7 * 16 - 80)) NormalBarrage.Intro8a9();
                if (GametimeF == (int)(bpm * 10 * 16 - 80)) CreateArrow(80, "R", 5, 1, 0);
                if (GametimeF == (int)(bpm * 11 * 16 - 80)) NormalBarrage.Intro11a12();
                if (GametimeF == (int)(bpm * 13 * 16 - 80)) NormalBarrage.Intro13a14();
                if (GametimeF == (int)(bpm * 15 * 16 - 80)) NormalBarrage.Intro15();
                if (GametimeF == (int)(bpm * 16 * 16 - 80)) NormalBarrage.Intro16a17();
                if (GametimeF == (int)(bpm * 18 * 16 - 80)) NormalBarrage.Intro18();
                if (GametimeF == (int)(bpm * 19 * 16 - 80)) NormalBarrage.Intro19();
                if (GametimeF == (int)(bpm * 20 * 16 - 80)) NormalBarrage.Intro20t22();
                if (GametimeF == (int)(bpm * 23 * 16 - 80)) NormalBarrage.Intro23a24();
                if (GametimeF == (int)(bpm * 25 * 16 - 80)) NormalBarrage.Intro26();
                if (GametimeF == (int)(bpm * 26 * 16 - 80)) NormalBarrage.Intro27();
                if (GametimeF == (int)(bpm * 27 * 16 - 80)) NormalBarrage.Intro28a29();
                if (GametimeF == (int)(bpm * 29 * 16 - 80)) NormalBarrage.Intro30a31();
                if (GametimeF == (int)(bpm * 31 * 16 - 80)) NormalBarrage.Intro28a29();
                if (GametimeF == (int)(bpm * 33 * 16 - 80)) NormalBarrage.Intro33a34();
                if (GametimeF == (int)(bpm * 35 * 16 - 80)) NormalBarrage.Intro35a36();
                if (GametimeF == (int)(bpm * 37 * 16 - 80)) NormalBarrage.Intro37a38();
                if (GametimeF == (int)(bpm * 39 * 16 - 80)) NormalBarrage.Intro39a40();
                if (GametimeF == (int)(bpm * 41 * 16 - 80)) NormalBarrage.Intro41a42();
                if (GametimeF == (int)(bpm * 43 * 16 - 80)) NormalBarrage.Intro43t50();

                if (GametimeF == (int)(bpm * 51 * 16 - 60)) { SetPlayerMission(0); NormalBarrage.Intro51t54R(); }
                if (GametimeF == (int)(bpm * 55 * 16 - 60)) { SetPlayerMission(1); NormalBarrage.Intro56t58R(); }
                if (GametimeF == (int)(bpm * 58 * 16 - bpm * 16))
                {
                    SetPlayerMission(0);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 1, bpm * 8));
                }
                if (GametimeF == (int)(bpm * 58 * 16 - bpm * 8))
                {
                    SetPlayerMission(1);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 0, bpm * 32));
                }
                if (GametimeF == (int)(bpm * 58 * 16))
                {
                    SetPlayerMission(1);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 1, bpm * 32 + bpm * 16));
                }
                if (GametimeF == (int)(bpm * 16 * 60)) NormalBarrage.Intro60();
                if (GametimeF == (int)(bpm * 63 * 16 - 80)) NormalBarrage.Intro62();
                if (GametimeF == (int)(bpm * 67 * 16 - 80)) NormalBarrage.Intro63t66();
                if (GametimeF == (int)(bpm * 71 * 16 - 80)) NormalBarrage.Intro67a68();
                if (GametimeF == (int)(bpm * 73 * 16 - 80)) NormalBarrage.Intro69t72();
                if (GametimeF == (int)(bpm * 77 * 16 - 80)) NormalBarrage.Intro77a78();
                if (GametimeF == (int)(bpm * 79 * 16 - 80)) NormalBarrage.Intro79a80();
                if (GametimeF == (int)(bpm * 81 * 16 - 80)) NormalBarrage.Intro81a82();
                if (GametimeF == (int)(bpm * 83 * 16 - 80)) NormalBarrage.Intro83t86();
                if (GametimeF == (int)(bpm * 87 * 16 - 80)) NormalBarrage.Intro87t90();
                if (GametimeF == (int)(bpm * 91 * 16 - 80)) NormalBarrage.Intro91t93();
                if (GametimeF == (int)(bpm * 94 * 16 - 80)) NormalBarrage.Intro94();
                if (GametimeF == (int)(bpm * 95 * 16 - 80)) NormalBarrage.Intro95a96();
                if (GametimeF == (int)(bpm * 97 * 16 - 80)) NormalBarrage.Intro97();
                if (GametimeF == (int)(bpm * 98 * 16 - 80)) NormalBarrage.Intro98();
                if (GametimeF == (int)(bpm * 99 * 16 - bpm * 12))
                {
                    CreateGB(new GreenSoulGB(bpm * 12, Rand(0, 3), 1, bpm * 60));
                }
                if (GametimeF == (int)(bpm * 103 * 16 - 80))
                {
                    CreateArrow(80, 3, 5, 1, 0);
                }
            }
            public void Hard()
            {
            }
            public void Extreme()
            {
                ExtremeEffects();
                if (GametimeF == (int)(bpm * 5 * 16 - 80)) ExtremeBarrage.Intro6a7();
                if (GametimeF == (int)(bpm * 7 * 16 - 80)) ExtremeBarrage.Intro8a9();
                if (GametimeF == (int)(bpm * 10 * 16 - 80)) CreateArrow(80, "R", 5, 1, 0);
                if (GametimeF == (int)(bpm * 11 * 16 - 80)) ExtremeBarrage.Intro11a12();
                if (GametimeF == (int)(bpm * 13 * 16 - 80)) ExtremeBarrage.Intro13a14();
                if (GametimeF == (int)(bpm * 15 * 16 - 80)) ExtremeBarrage.Intro15();
                if (GametimeF == (int)(bpm * 16 * 16 - 80)) ExtremeBarrage.Intro16a17();
                if (GametimeF == (int)(bpm * 18 * 16 - 80)) ExtremeBarrage.Intro18();
                if (GametimeF == (int)(bpm * 19 * 16 - 80)) ExtremeBarrage.Intro19();
                if (GametimeF == (int)(bpm * 20 * 16 - 80)) ExtremeBarrage.Intro20t22();
                if (GametimeF == (int)(bpm * 23 * 16 - 80)) ExtremeBarrage.Intro23a24();
                if (GametimeF == (int)(bpm * 25 * 16 - 80)) ExtremeBarrage.Intro26();
                if (GametimeF == (int)(bpm * 26 * 16 - 80)) ExtremeBarrage.Intro27();
                if (GametimeF == (int)(bpm * 27 * 16 - 80)) ExtremeBarrage.Intro28a29();
                if (GametimeF == (int)(bpm * 29 * 16 - 80)) ExtremeBarrage.Intro30a31();
                if (GametimeF == (int)(bpm * 31 * 16 - 80)) ExtremeBarrage.Intro28a29();
                if (GametimeF == (int)(bpm * 33 * 16 - 80)) ExtremeBarrage.Intro33a34();
                if (GametimeF == (int)(bpm * 35 * 16 - 80)) ExtremeBarrage.Intro35a36();
                if (GametimeF == (int)(bpm * 37 * 16 - 80)) ExtremeBarrage.Intro37a38();
                if (GametimeF == (int)(bpm * 39 * 16 - 80)) ExtremeBarrage.Intro39a40();
                if (GametimeF == (int)(bpm * 41 * 16 - 80)) ExtremeBarrage.Intro41a42();
                if (GametimeF == (int)(bpm * 43 * 16 - 80)) ExtremeBarrage.Intro43t50();

                if (GametimeF == (int)(bpm * 51 * 16 - 60)) { ExtremeBarrage.Intro51t54B(); }
                if (GametimeF == (int)(bpm * 51 * 16 - 60)) { ExtremeBarrage.Intro51t54R(); }
                if (GametimeF == (int)(bpm * 55 * 16 - 60)) { ExtremeBarrage.Intro56t58B(); }
                if (GametimeF == (int)(bpm * 55 * 16 - 60)) { ExtremeBarrage.Intro56t58R(); }
                if (GametimeF == (int)(bpm * 58 * 16 - bpm * 16))
                {
                    SetPlayerMission(0);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 1, bpm * 8));
                }
                if (GametimeF == (int)(bpm * 58 * 16 - bpm * 8))
                {
                    SetPlayerMission(1);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 0, bpm * 32));
                }
                if (GametimeF == (int)(bpm * 58 * 16))
                {
                    SetPlayerMission(1);
                    CreateGB(new GreenSoulGB(bpm * 16, 3, 1, bpm * 32 + bpm * 16));
                }
                if (GametimeF == (int)(bpm * 61 * 16 - 80)) ExtremeBarrage.Intro60();
                if (GametimeF == (int)(bpm * 63 * 16 - 80)) ExtremeBarrage.Intro62();
                if (GametimeF == (int)(bpm * 67 * 16 - 80)) ExtremeBarrage.Intro63t66();
                if (GametimeF == (int)(bpm * 71 * 16 - 80)) ExtremeBarrage.Intro67a68();
                if (GametimeF == (int)(bpm * 73 * 16 - 80)) ExtremeBarrage.Intro69t72();
                if (GametimeF == (int)(bpm * 77 * 16 - 80)) ExtremeBarrage.Intro77a78();
                if (GametimeF == (int)(bpm * 79 * 16 - 80)) ExtremeBarrage.Intro79a80();
                if (GametimeF == (int)(bpm * 81 * 16 - 80)) ExtremeBarrage.Intro81a82();
                if (GametimeF == (int)(bpm * 83 * 16 - 80)) ExtremeBarrage.Intro83t86();
                if (GametimeF == (int)(bpm * 87 * 16 - 80)) ExtremeBarrage.Intro87t90();
                if (GametimeF == (int)(bpm * 91 * 16 - 80)) ExtremeBarrage.Intro91t93();
                if (GametimeF == (int)(bpm * 94 * 16 - 80)) ExtremeBarrage.Intro94();
                if (GametimeF == (int)(bpm * 95 * 16 - 80)) ExtremeBarrage.Intro95a96();
                if (GametimeF == (int)(bpm * 97 * 16 - 80)) ExtremeBarrage.Intro97();
                if (GametimeF == (int)(bpm * 98 * 16 - 80)) ExtremeBarrage.Intro98();
                if (GametimeF == (int)(bpm * 99 * 16 - bpm * 12))
                {
                    CreateGB(new GreenSoulGB(bpm * 12, Rand(0, 3), 0, bpm * 64));
                    CreateGB(new GreenSoulGB(bpm * 12, Rand(0, 3), 1, bpm * 64));
                }
                if (GametimeF == (int)(bpm * 103 * 16 - 80))
                {
                    CreateArrow(80, 3, 5, 1, 0);
                }
            }
            public void Easy()
            {
                if (GametimeF == (int)(bpm * 16))
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "R(001)(R02)","/","/","/",   "/","/","/","/",
                "(R1)(01)(R12)","/","/","/",   "/","/","/","/",
                "(+202)(+011)(302)","/","/","/",   "/","/","/","/",
                "(+10)(112)(211)","/","/","/",   "/","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat, 6.5f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
            }

        }
    }
}