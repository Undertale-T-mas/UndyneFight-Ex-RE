using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace AprilExtends
{
    public class GrievousLady : IChampionShip
    {
        public GrievousLady()
        {
            Game.instance = new GrievousLady.Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));
            difficulties = new Dictionary<string, Difficulty>();
            difficulties.Add("Past None", Difficulty.Noob);
            difficulties.Add("None", Difficulty.Normal);
            difficulties.Add("Present 9+", Difficulty.Hard);
            difficulties.Add("Future 11", Difficulty.Extreme);
            difficulties.Add("Beyond 12", Difficulty.ExtremePlus);
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
                return new GrievousLady.Game();
            }
        }
        private readonly Dictionary<string, Difficulty> difficulties = new();
        public SaveInfo divisionInformation;

        private class Game : WaveConstructor, IWaveSet
        {
            public class DrawLine
            {
                public class NormalLine : Entity
                {
                    public int duration = 0;
                    public float x1 = 0;
                    public float y1 = 0;
                    public float x2 = 0;
                    public float y2 = 0;
                    public Color color = Color.White;
                    public NormalLine(float x1, float y1, float x2, float y2, int duration, float alpha, Color color)
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
                    public float width = 4;
                    public Color color = Color.White;
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
                                width,
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
                    public int acc = 0;
                    public float x = 0;
                    public float y = 0;
                    public float rA = 0;
                    public float rB = 0;
                    public float roA = 0;
                    public float roB = 0;
                    public float width = 4;
                    public Color color
                        = Color.White;
                    public Photo(float x, float y, float rA, float rB, float roA, float roB, int acc, float alpha)
                    {
                        this.x = x;
                        this.y = y;
                        this.acc = acc;
                        this.rA = rA;
                        this.rB = rB;
                        this.roA = roA;
                        this.roB = roB;
                        this.alpha = alpha;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        for (int i = 0; i < acc; i += 1)
                        {
                            int a = acc * 180 - 360;
                            DrawingLab.DrawLine(
                                new Vector2(x + Cos(a / acc * i + roA) * rA,
                                            y + Sin(a / acc * i + roA) * rA),
                                new Vector2(x + Cos(a / acc * i + roA) * rA + Cos(a / acc * i + roB) * rB,
                                            y + Sin(a / acc * i + roA) * rA + Sin(a / acc * i + roB) * rB),//rotatec是绕点旋转，rotate是绕中心旋转
                                width,
                                color,
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
                public class A3D : Entity
                {
                    public int acc = 0;
                    public float x = 0;
                    public float y = 0;
                    public float x1 = 0;
                    public float y1 = 0;
                    public float rA = 0;
                    public float rB = 0;
                    public float roA = 0;
                    public float roB = 0;
                    public A3D(float x, float y, float rA, float rB, float roA, float roB, int acc, float alpha, float x1, float y1)
                    {
                        this.x = x;
                        this.y = y;
                        this.acc = acc;
                        this.rA = rA;
                        this.rB = rB;
                        this.roA = roA;
                        this.roB = roB;
                        this.alpha = alpha;
                        this.x1 = x1;
                        this.y1 = y1;
                    }
                    public float alpha = 1;
                    public int time = 0;
                    public float speed = 1;
                    public override void Draw()
                    {
                        for (int i = 0; i < acc; i += 1)
                        {
                            int a = acc * 180 - 360;
                            DrawingLab.DrawLine(
                                new Vector2(x + Cos(a / acc * i + roA) * rA,
                                            y + Sin(a / acc * i + roA) * rA),
                                new Vector2(x1,
                                            y1),//rotatec是绕点旋转，rotate是绕中心旋转
                                4f,
                                Color.Cyan * alpha,
                                0.99f);
                            Depth = 0.99f;
                        }
                        for (int i = 0; i < 1000; i += 1)
                        {
                            int rotateb = acc * 180 - 360;
                            DrawingLab.DrawLine(
                                new Vector2(x + Cos(rotateb / 1000 * i) * rA,
                                            y + Sin(rotateb / 1000 * i) * rA),
                                new Vector2(x + Cos(rotateb / 1000 * (i + 1)) * rA,
                                            y + Sin(rotateb / 1000 * (i + 1)) * rA),//rotatec是绕点旋转，rotate是绕中心旋转
                                4,
                                Color.Cyan,
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
            public SongInformation Attributes => new ThisInformation();
            private class ThisInformation : SongInformation
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
                        return "Arcaea";
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
            }
            public static float bpm = 4.46427f;
            public static Player.Heart splitheart;
            public static Extends.DrawingUtil.SpecialBox box = new(114514, 0, Heart);
            public static float heartrot = 0;
            public string Music => "Grievous Lady";
            public string FightName => "Grievous Lady";
            public static GrievousLady.Game instance;

            public Game() : base(4.46427f)
            {
            }

            public void Start()
            {


                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                TP();
                SetSoul(1);
                // FightBox.instance.GreenSoulAlpha = 0f;
                HeartAttribute.DamageTaken = 10;
                HeartAttribute.MaxHP = 100;
                ScreenDrawing.HPBar.Vertical = true;
                ScreenDrawing.HPBar.HPExistColor = Color.DarkRed;
                ScreenDrawing.HPBar.HPLoseColor = Color.Black;
                ScreenDrawing.HPBar.AreaOccupied = new(8, 240 - 190, 20, 190);
                ScreenDrawing.UIColor = Color.DeepSkyBlue;
                ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(new Shader(Loader.Load<Effect>("Musics\\Grievous Lady\\Effects"))));

            }
            public static class mainEffects
            {
                public static void shock()
                {
                    for (int a = 0; a < 12; a++)
                    {
                        AddInstance(new TimeRangedEvent(a * 2.1f, 1, () =>
                            { ScreenDrawing.ScreenPositionDelta = new Vector2(Rand(-7.0f, 7.0f), Rand(-7.0f, 7.0f)); }
                        ));
                        AddInstance(new TimeRangedEvent((a + 1) * 2.1f, 1, () =>
                          { ScreenDrawing.ScreenPositionDelta = new Vector2(0); }
));
                    }
                }
                public static void midshock()
                {
                    for (int a = 0; a < 9; a++)
                    {
                        AddInstance(new TimeRangedEvent(a * 1.8f, 1, () =>
                        { ScreenDrawing.ScreenPositionDelta = new Vector2(Rand(-5.0f, 5.0f), Rand(-5.0f, 5.0f)); }
                        ));
                        AddInstance(new TimeRangedEvent((a + 1) * 1.8f, 1, () =>
                        { ScreenDrawing.ScreenPositionDelta = new Vector2(0); }
));
                    }
                }
                public static void littleshock()
                {
                    for (int a = 0; a < 6; a++)
                    {
                        AddInstance(new TimeRangedEvent(a * 1.7f, 1, () =>
                        { ScreenDrawing.ScreenPositionDelta = new Vector2(Rand(-3.0f, 3.0f), Rand(-3.0f, 3.0f)); }
                        ));
                        AddInstance(new TimeRangedEvent((a + 1) * 1.7f, 1, () =>
                        { ScreenDrawing.ScreenPositionDelta = new Vector2(0); }
));
                    }
                }
                public static void rain()
                {
                    float speed = 9;
                    DrawLine.Linerotatelong rain = new(Rand(0, 860), -45, 290 + Rand(-2.5f, 2.5f), bpm * 32, Rand(0.2f, 0.4f), Rand(9, 55), Color.White);
                    rain.width = Rand(2, 4);
                    if (Rand(1, 3) == 1)
                    {
                        CreateEntity(rain);
                    }
                    else
                    {
                        for (int a = 0; a < 2; a++) CreateEntity(rain);
                    }
                    AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                    {
                        rain.xCenter += Cos(110) * speed;
                        rain.yCenter += Sin(110) * speed;
                    }));
                }
                public static void longshock()
                {
                    for (int a = 0; a < 64; a++)
                    {
                        AddInstance(new TimeRangedEvent(a * bpm * 0.5f, 1, () =>
                          { ScreenDrawing.ScreenPositionDelta = new Vector2(Rand(-2, 2), Rand(-2, 2)); }
                        ));
                        AddInstance(new TimeRangedEvent((a + 1) * bpm * 0.5f, 1, () =>
                          { ScreenDrawing.ScreenPositionDelta = new Vector2(0); }
));
                    }
                }
                public static void rain2()
                {
                    DrawLine.Linerotatelong rain = new(Rand(0, 640), -45, 270, bpm * 32, 0.15f, Rand(13, 55), Color.White);
                    rain.width = 1;
                    DrawLine.Linerotatelong rain1 = new(Rand(0, 640), 480 + 45, 90, bpm * 32, 0.15f, Rand(13, 55), Color.White);
                    rain.width = 1;
                    if (Rand(1, 3) == 1)
                    {
                        CreateEntity(rain);
                    }
                    else
                    {
                        for (int a = 0; a < 2; a++) CreateEntity(rain);
                    }
                    if (Rand(1, 3) == 1)
                    {
                        CreateEntity(rain1);
                    }
                    else
                    {
                        for (int a = 0; a < 2; a++) CreateEntity(rain1);
                    }
                    AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                    {
                        rain.yCenter += 9;
                        rain1.yCenter -= 9;
                    }));
                }
            }
            public static class Attacks
            {
                public static void Centerspear()
                {
                    Spear spear1 = new CircleSpear(Heart.Centre, 4f, 1.3f, 146, Rand(0, 60));
                    Spear spear2 = new CircleSpear(Heart.Centre, 4f, 1.3f, 146, LastRand + 60);
                    Spear spear3 = new CircleSpear(Heart.Centre, 4f, 1.3f, 146, LastRand + 120);
                    Spear spear4 = new CircleSpear(Heart.Centre, 4f, 1.3f, 146, LastRand + 180);
                    Spear spear5 = new CircleSpear(Heart.Centre, 4f, 1.3f, 146, LastRand + 240);
                    Spear spear6 = new CircleSpear(Heart.Centre, 4f, 1.3f, 146, LastRand + 300);
                    PlaySound(Sounds.spearAppear);
                    CreateSpear(spear1);
                    CreateSpear(spear2);
                    CreateSpear(spear3);
                    CreateSpear(spear4);
                    CreateSpear(spear5);
                    CreateSpear(spear6);
                }
                public static void Aspear()
                {
                    int R1 = Rand(0, 360);
                    PlaySound(Sounds.spearAppear);
                    Spear spear1 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216)) { IsMute = true };
                    CreateSpear(spear1);

                }
                public static void Sixspear()
                {
                    for (int a = 0; a < 4; a++)
                        PlaySound(Sounds.spearAppear);
                    int R = Rand(0, 60);
                    int R1 = Rand(0, 360);
                    Spear spear1 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R) { IsMute = true };
                    Spear spear2 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 60) { IsMute = true };
                    Spear spear3 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 120) { IsMute = true };
                    Spear spear4 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 180) { IsMute = true };
                    Spear spear5 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 240) { IsMute = true };
                    Spear spear6 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 300) { IsMute = true };
                    CreateSpear(spear1);
                    CreateSpear(spear2);
                    CreateSpear(spear3);
                    CreateSpear(spear4);
                    CreateSpear(spear5);
                    CreateSpear(spear6);
                }
                public static void Superspear()
                {
                    for (int a = 0; a < 8; a++)
                        PlaySound(Sounds.spearAppear);
                    int R = Rand(0, 60);
                    int R1 = Rand(0, 360);
                    Spear spear1 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R) { IsMute = true };
                    Spear spear2 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 15) { IsMute = true };
                    Spear spear3 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 30) { IsMute = true };
                    Spear spear4 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 45) { IsMute = true };
                    Spear spear5 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 60) { IsMute = true };
                    Spear spear6 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 75) { IsMute = true };

                    Spear spear7 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 90) { IsMute = true };
                    Spear spear8 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 105) { IsMute = true };
                    Spear spear9 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 120) { IsMute = true };
                    Spear spear10 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 135) { IsMute = true };
                    Spear spear11 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 150) { IsMute = true };
                    Spear spear12 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 165) { IsMute = true };

                    Spear spear13 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 180) { IsMute = true };
                    Spear spear14 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 195) { IsMute = true };
                    Spear spear15 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 210) { IsMute = true };
                    Spear spear16 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 225) { IsMute = true };
                    Spear spear17 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 240) { IsMute = true };
                    Spear spear18 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 255) { IsMute = true };

                    Spear spear19 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 270) { IsMute = true };
                    Spear spear20 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 285) { IsMute = true };
                    Spear spear21 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 300) { IsMute = true };
                    Spear spear22 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 315) { IsMute = true };
                    Spear spear23 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 330) { IsMute = true };
                    Spear spear24 = new NormalSpear(Heart.Centre + new Vector2(Cos(R1) * 216, Sin(R1) * 216), R + 345) { IsMute = true };
                    CreateSpear(spear1);
                    CreateSpear(spear2);
                    CreateSpear(spear3);
                    CreateSpear(spear4);
                    CreateSpear(spear5);
                    CreateSpear(spear6);

                    CreateSpear(spear7);
                    CreateSpear(spear8);
                    CreateSpear(spear9);
                    CreateSpear(spear10);
                    CreateSpear(spear11);
                    CreateSpear(spear12);

                    CreateSpear(spear13);
                    CreateSpear(spear14);
                    CreateSpear(spear15);
                    CreateSpear(spear16);
                    CreateSpear(spear17);
                    CreateSpear(spear18);

                    CreateSpear(spear19);
                    CreateSpear(spear20);
                    CreateSpear(spear21);
                    CreateSpear(spear22);
                    CreateSpear(spear23);
                    CreateSpear(spear24);
                }
            }
            public void Effects()
            {
                DrawLine.Linerotate line1 = new(-5, -5, 180, bpm * 16 * 3, 1, Color.Cyan);
                DrawLine.Linerotate line2 = new(645, -5, 270, bpm * 16 * 3, 1, Color.Cyan);
                DrawLine.Linerotate line3 = new(645, 485, 0, bpm * 16 * 3, 1, Color.Cyan);
                DrawLine.Linerotate line4 = new(-5, 485, 90, bpm * 16 * 3, 1, Color.Cyan);
                if (GametimeF >= 0 && GametimeF <= bpm * 16 * 12 && GametimeF % 3 == 1) mainEffects.rain();

                if (GametimeF == (int)(bpm * 16 * 4 - 2)) mainEffects.shock();
                if (GametimeF == (int)(bpm * 16 * 8 - 2)) mainEffects.shock();
                if (GametimeF == (int)(bpm * 16 * 9 - 2)) mainEffects.shock();
                if (GametimeF == (int)(bpm * 16 * 10 - 2)) mainEffects.shock();
                if (GametimeF == (int)(bpm * 16 * 11 + bpm * 8 - 2)) mainEffects.shock();
                if (GametimeF == (int)(bpm * 16 * 12))
                {
                    float move = 0;
                    float a = 0.615f;
                    mainEffects.shock();
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    CreateEntity(line4);
                    AddInstance(new TimeRangedEvent(0, bpm * 16 * 3, () =>
                    {
                        line1.rotate += a;
                        line2.rotate += a;
                        line3.rotate += a;
                        line4.rotate += a;
                    }));
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                    {
                        InstantSetBox(new Vector2(320, BoxStates.Centre.Y - 2.1f), 84, 84);
                        InstantTP(new Vector2(320, Heart.Centre.Y - 2.1f));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, bpm * 13, () =>
                      {
                          InstantSetBox(new Vector2(BoxStates.Centre.X - move * 0.3f, BoxStates.Centre.Y + move * move), 84, 84);
                          InstantTP(new Vector2(Heart.Centre.X - move * 0.3f, Heart.Centre.Y + move * move));
                          move += 0.068f;
                      }));
                    AddInstance(new TimeRangedEvent(bpm * 17, 1, () =>
                    {
                        move = 5.5f;
                        InstantSetBox(new Vector2(320, -90), 84, 84);
                        InstantTP(new Vector2(320, -90));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 17, bpm * 16, () =>
                    {
                        InstantSetBox(new Vector2(320, BoxStates.Centre.Y + move), 84, 84);
                        InstantTP(new Vector2(320, Heart.Centre.Y + move));
                        move -= 0.025f;
                        if (BoxStates.Centre.X >= 320) return;
                    }));
                }//线和心
                AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        line1.Dispose();
                        line2.Dispose();
                        line3.Dispose();
                        line4.Dispose();
                    }));
                if (GametimeF == (int)(bpm * 16 * 31)) ScreenDrawing.MakeFlicker(Color.White * 0.5f);
                if (GametimeF == (int)(bpm * 16 * 31 + bpm * 2)) ScreenDrawing.MakeFlicker(Color.White * 0.7f);
                if (GametimeF == (int)(bpm * 16 * 56))
                {
                    float speed = 2f;
                    AddInstance(new TimeRangedEvent(0, bpm * 24 + 2, () =>
                      {
                          InstantSetBox(BoxStates.Left - speed * 0.75f, BoxStates.Right + speed * 0.75f, BoxStates.Up - speed * 0.5f, BoxStates.Down + speed * 0.5f);
                      }));

                }
                if (GametimeF == (int)(bpm * 16 * 57))
                {
                    Heart.EnabledRedShield = true;
                    SetSoul(0);
                }
                if (GametimeF == (int)(bpm * 16 * 66))
                {
                    SetSoul(2);
                    Heart.GiveForce(180, 15);
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        SetSoul(0);
                        Heart.GiveForce(0, 6);
                    }));
                }
                if (GametimeF == (int)(bpm * 16 * 75 + bpm * 12))
                {
                    SetSoul(1);
                    SetBox(240, 84, 84);
                    TP();
                }
                if (GametimeF == (int)(bpm * 16 * 83))
                {
                    ScreenDrawing.CameraEffect.SizeShrink(15, bpm * 16);
                }
                if (GametimeF == (int)(bpm * 16 * 84))
                {
                    mainEffects.midshock();
                }
                if (GametimeF == (int)(bpm * 16 * 100 - bpm * 4))
                {
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                     {
                         InstantSetBox(BoxStates.Centre.Y - 0.6f, 84, 84);
                         InstantTP(Heart.Centre.X, Heart.Centre.Y - 0.6f);
                     }));
                }
                if (GametimeF == (int)(bpm * 16 * 100))
                {
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 16 * 25, () =>
                    {
                        InstantSetBox(new Vector2(BoxStates.Centre.X + Cos(speed * 0.2f) * 0.3f, BoxStates.Centre.Y + Sin(speed) * 1f), 84, 84);
                        InstantTP(new Vector2(Heart.Centre.X + Cos(speed * 0.2f) * 0.3f, Heart.Centre.Y + Sin(speed) * 1f));
                        speed += 4.3f;
                    }));
                }
                if (GametimeF >= (int)(bpm * 16 * 108) && GametimeF <= (int)(bpm * 16 * 117) && GametimeF % 6 == 1)
                {
                    mainEffects.rain2();
                }
            }
            public static class ExtremeBarrage
            {
                private static readonly Shader shader = new(Loader.Load<Effect>("Musics\\Grievous Lady\\Effects"));
                public static void Intro0t3()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "$0","/","/","/",   "/","/","/","/",
                "$31","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R1","/","/","/",
                "R","/","/","/",   "R1","/","/","/",

                "R","/","/","/",   "R1","/","/","/",
                "R","/","/","/",   "R1","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R1","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(beat, Rand(4.00f, 7.00f), arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro4t7()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "/","/","/","/",   "R1","/","/","/",
                "R","/","/","/",   "R1","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R1","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "(R)(+011)","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "(R)(+011)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "(R)(+011)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro8t12()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "R(R1)[1..2]","/","/","/",   "R","/","/","/",
                "R1","/","/","/",   "R","/","/","/",
                "R ","/","/","/",   "/","/","/","/",
                "R ","/","/","/",   "/","/","/","/",

                "R(R1)[0..1]","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R1","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "R(R1)[1..2]","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R(R1)[0..1]","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",

                "R(R1)[1..2]","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",

            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro16()
                {
                    Arrow arrow1 = MakeArrow(2, Rand(0, 3), 6.8f, 0, 0);
                    Arrow arrow2 = MakeArrow(2, Rand(0, 3), 6.8f, 1, 0);

                    Arrow arrow3 = MakeArrow(bpm * 6, Rand(0, 3), 6.8f, 0, 0);
                    Arrow arrow4 = MakeArrow(bpm * 6, Rand(0, 3), 6.8f, 1, 0);

                    Arrow arrow5 = MakeArrow(bpm * 12, Rand(0, 3), 6.8f, 0, 0);
                    Arrow arrow6 = MakeArrow(bpm * 12, Rand(0, 3), 6.8f, 1, 0);

                    Arrow arrow7 = MakeArrow(bpm * 18, Rand(0, 3), 6.8f, 0, 0);
                    Arrow arrow8 = MakeArrow(bpm * 18, Rand(0, 3), 6.8f, 1, 0);
                    CreateEntity(arrow1);
                    CreateEntity(arrow2);
                    CreateEntity(arrow3);
                    CreateEntity(arrow4);
                    CreateEntity(arrow5);
                    CreateEntity(arrow6);
                    CreateEntity(arrow7);
                    CreateEntity(arrow8);
                    arrow1.Stop(bpm * 16);
                    arrow2.Stop(bpm * 16);
                    arrow3.Stop(bpm * 16);
                    arrow4.Stop(bpm * 16);
                    arrow5.Stop(bpm * 16);
                    arrow6.Stop(bpm * 16);
                    arrow7.Stop(bpm * 16);
                    arrow8.Stop(bpm * 16);
                    AddInstance(new TimeRangedEvent(bpm * 16 - 2, 1, () =>
                          {
                              arrow1.Delay(bpm * 16);
                              arrow2.Delay(bpm * 16);
                              arrow3.Delay(bpm * 16);
                              arrow4.Delay(bpm * 16);
                              arrow5.Delay(bpm * 16);
                              arrow6.Delay(bpm * 16);
                              arrow7.Delay(bpm * 16);
                              arrow8.Delay(bpm * 16);
                          }));
                }
                public static void Intro17()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "/","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",

                "R(R1)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "S","/","/","/",
                "S","/","/","/",   "/","/","/","/",

                "R(R1)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",

                "R(R1)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "S","/","/","/",
                "S","/","/","/",   "/","/","/","/",

            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        if (arrow[i] == "S")
                        {
                            CreateArrow(80 + beat, "R", 11, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + beat, "R", 11, 1, 0, ArrowAttribute.SpeedUp);
                            AddInstance(new TimeRangedEvent(78 + beat, 1, () =>
                              {
                                  mainEffects.littleshock();
                                  ScreenDrawing.MakeFlicker(Color.White * 0.3f);
                              }));
                            beat += bpm * 0.5f;
                            float x = 0.9f;
                            AddInstance(new TimeRangedEvent(78 + beat, 10, () =>
                            {
                                shader.Parameters["distance"].SetValue(new Vector2(0.01f * x, 0.01f * x));
                                x -= 0.1f;
                            }));
                        }
                        if (arrow[i] != "S" && arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro24()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "R(R1)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",

                "R(R1)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "S","/","/","/",
                "S","/","/","/",   "/","/","/","/",

                "R(R1)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",

                "R(R1)","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R(R1)","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "S","/","/","/",   "/","/","/","/",

            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        if (arrow[i] == "S")
                        {
                            CreateArrow(80 + beat, "R", 11, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + beat, "R", 11, 1, 0, ArrowAttribute.SpeedUp);
                            AddInstance(new TimeRangedEvent(78 + beat, 1, () =>
                            {
                                mainEffects.shock();
                                ScreenDrawing.MakeFlicker(Color.White * 0.3f);
                            }));
                            beat += bpm * 0.5f;
                        }
                        if (arrow[i] != "S" && arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                            AddInstance(new TimeRangedEvent(78 + beat, 1, () =>
                            {
                                mainEffects.littleshock();
                            }));
                            float x = 0.9f;
                            AddInstance(new TimeRangedEvent(78 + beat, 10, () =>
                            {
                                shader.Parameters["distance"].SetValue(new Vector2(0.01f * x, 0.01f * x));
                                x -= 0.1f;
                            }));
                        }
                    }
                }
                public static void Intro31()
                {
                    CreateGB(new GreenSoulGB(bpm * 16, "R", 1, bpm * 44));
                    for (int a = 0; a < 7; a++)
                    {
                        CreateArrow(bpm * 16 + a * 4 * bpm, LastRand, 12, 1, 0);
                    }
                    CreateArrow(bpm * 16 + 30 * bpm, LastRand, 12, 1, 0);
                    for (int a = 0; a < 3; a++)
                    {
                        CreateArrow(bpm * 48 + a * 4 * bpm, LastRand, 12, 1, 0);
                    }

                    float beat = 0;
                    string[] arrow =
                        {
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",


            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 - 2 + beat, 1, () =>
                                    {
                                        ScreenDrawing.CameraEffect.RotateTo(-20, 1);
                                    }));
                            AddInstance(new TimeRangedEvent(bpm * 16 - 2 + beat + 1, 1, () =>
                              {
                                  ScreenDrawing.CameraEffect.RotateTo(0, bpm * 1);
                              }));
                            instance.CreateArrows(bpm * 16 + beat, 7f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro35()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "(R02)(+012)","/","+012","/",   "+012","/","+012","/",
                "+012","/","+012","/",   "+012","/","+012","/",
                "(R02)(+012)","/","+012","/",   "+012","/","+012","/",
                "+012","/","+012","/",   "+012","/","/","/",

            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro36()
                {
                    CreateGB(new GreenSoulGB(bpm * 16, "R", 1, bpm * 16 * 7 - bpm * 4));
                    for (int a = 0; a < 5; a++)
                    {
                        CreateArrow(bpm * 16 + a * 4 * bpm, LastRand, 12, 1, 0);
                    }
                    CreateArrow(bpm * 16 + 22 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 + 24 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 + 28 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 + 30 * bpm, LastRand, 12, 1, 0);
                    for (int a = 0; a < 6; a++)
                    {
                        CreateArrow(bpm * 48 + a * 4 * bpm, LastRand, 12, 1, 0);
                    }
                    for (int a = 0; a < 4; a++)
                    {
                        CreateArrow(bpm * 48 + bpm * 24 + a * 2 * bpm, LastRand, 12, 1, 0);
                    }
                    CreateArrow(bpm * 16 * 5 + 2 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 5 + 4 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 5 + 8 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 5 + 12 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6 + bpm * 4, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6 + bpm * 8, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6 + bpm * 12, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6 + bpm * 14, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6 + bpm * 16, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6 + bpm * 20, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6 + bpm * 24, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 * 6 + bpm * 28, LastRand, 12, 1, 0);
                    float beat = 0;
                    string[] arrow =
                        {
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R","/","/","/",
                "R","/","/","/",   "R","/","/","/",

                "/","/","/","/",   "R","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 - 2 + beat, 1, () =>
                            {
                                ScreenDrawing.CameraEffect.RotateTo(-20, 3);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 - 2 + beat + 4, 1, () =>
                            {
                                ScreenDrawing.CameraEffect.RotateTo(0, 4);
                            }));
                            instance.CreateArrows(bpm * 16 + beat, 7.2f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro44()
                {
                    CreateGB(new GreenSoulGB(bpm * 16, "R", 1, bpm * 28));
                    for (int a = 0; a < 4; a++)
                    {
                        CreateArrow(bpm * 20 + a * 4 * bpm, LastRand, 12, 1, 0);
                    }
                    CreateArrow(bpm * 16 + 24 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 + 26 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 + 28 * bpm, LastRand, 12, 1, 0);
                    CreateArrow(bpm * 16 + 30 * bpm, LastRand, 12, 1, 0);

                    float beat = 0;
                    string[] arrow =
                        {
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R","/","/","/",
                "R","/","/","/",   "R","/","/","/",


            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 - 2 + beat, 1, () =>
                            {
                                ScreenDrawing.CameraEffect.RotateTo(20, 3);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 - 2 + beat + 4, 1, () =>
                            {
                                ScreenDrawing.CameraEffect.RotateTo(0, 4);
                            }));
                            instance.CreateArrows(bpm * 16 + beat, 7.2f, arrow[i]);

                        }
                    }
                }

                public static void Intro46()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R(R1)","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "R(R1)","/","/","/",
                "R(R1)","/","/","/",   "R(R1)","/","/","/",

            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            if (arrow[i] == "R(R1)")
                            {
                                AddInstance(new TimeRangedEvent(78 + beat, 1, () =>
                                  {
                                      ScreenDrawing.MakeFlicker(Color.White * 0.3f);
                                      ScreenDrawing.CameraEffect.SizeShrink(10, bpm * 2);
                                  }));
                            }
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro47()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",

                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",

                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",

                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            CreateArrow(80 + beat, arrow[i], 12, 0, 0, ArrowAttribute.RotateR);
                            beat += bpm * 0.5f;
                        }
                    }

                    float beat1 = 0;
                    string[] arrow1 =
                        {
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
            };
                    for (int i = 0; i < arrow1.Length; i++)
                    {
                        if (arrow1[i] == "/") beat1 += bpm * 0.5f;
                        else
                        {
                            CreateArrow(80 + beat1, "R", 10, 1, 0, ArrowAttribute.SpeedUp);
                            AddInstance(new TimeRangedEvent(78 + beat1, 1, () =>
                                {
                                    ScreenDrawing.CameraEffect.SizeShrink(1.8f, 5);
                                }));
                            beat1 += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro52()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",

                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",

                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",

                "$2","$1","$0","/",    "/","/","/","/",
                "$2","$1","$0","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            CreateArrow(80 + beat, arrow[i], 12, 0, 0, ArrowAttribute.RotateR);
                            beat += bpm * 0.5f;
                        }
                    }
                    float beat1 = 0;
                    string[] arrow1 =
                        {
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",

                "R1","/","/","/",   "/","/","/","/",
                "R1","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","/","/",
            };
                    for (int i = 0; i < arrow1.Length; i++)
                    {
                        if (arrow1[i] == "/") beat1 += bpm * 0.5f;
                        else
                        {
                            CreateArrow(80 + beat1, "R", 10, 1, 0, ArrowAttribute.SpeedUp);
                            AddInstance(new TimeRangedEvent(78 + beat1, 1, () =>
                            {
                                ScreenDrawing.CameraEffect.SizeShrink(1.8f, 5);
                            }));
                            beat1 += bpm * 0.5f;
                        }
                    }
                    float beat2 = 0;
                    string[] arrow2 =
                        {
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "R(R1)","/","/","/",    "R(R1)","/","/","/",
                "R","/","(R1)","/",    "R","/","(R1)","/",
            };
                    for (int i = 0; i < arrow2.Length; i++)
                    {
                        if (arrow2[i] == "/") beat2 += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat2, 6.85f, arrow2[i]);
                            beat2 += bpm * 0.5f;
                        }
                    }
                    AddInstance(new TimeRangedEvent(bpm * 64, 1, () =>
                        {
                            CreateGB(new GreenSoulGB(80, Rand(0, 3), 1, bpm * 32));
                        }));
                }

                public static void Intro53()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "C","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "A","/","/","/",
                "A","/","/","/",    "/","/","/","/",

                "C","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "/","/","/","/",

                "A","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "A","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "/","/","/","/",

                "C","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "A","/","/","/",
                "A","/","/","/",    "/","/","/","/",

                "C","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "A","/","/","/",
                "A","/","/","/",    "/","/","/","/",

                "A","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "A","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "/","/","/","/",

                "A","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "A","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "/","/","/","/",

                "A","/","/","/",    "/","/","/","/",
                "A","/","/","/",    "/","/","/","/",
                "C","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else if (arrow[i] == "C")
                        {
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Attacks.Centerspear();
                                PlaySound(Sounds.spearAppear);
                            }));
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "A")
                        {
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Attacks.Aspear();
                                PlaySound(Sounds.spearAppear);
                            }));
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro53A()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "$011","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "$211","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "$011","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "$211","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "$011","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "$211","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "$011","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "$211","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro66()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "S","/","/","/",    "S","/","/","/",
                "S","/","/","/",    "/","/","/","/",

                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "S","/","/","/",    "/","/","/","/",
                "S","/","/","/",    "/","/","/","/",

                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "S","/","/","/",
                "S","/","/","/",    "/","/","/","/",
                "S","/","/","/",    "/","/","/","/",

                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "S","/","/","/",    "S","/","/","/",
                "S","/","/","/",    "/","/","/","/",

                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "S","/","/","/",    "S","/","/","/",
                "S","/","/","/",    "/","/","/","/",

                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "S","/","/","/",    "S","/","/","/",
                "S","/","/","/",    "/","/","/","/",

            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else if (arrow[i] == "S")
                        {
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Attacks.Sixspear();
                                PlaySound(Sounds.spearAppear);
                            }));
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro72()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "S","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",


            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else if (arrow[i] == "S")
                        {
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Attacks.Superspear();


                            }));
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro76()
                {
                    CreateGB(new GreenSoulGB(80, "R", 1, bpm * 28));
                    for (int a = 0; a < 24; a++) CreateArrow(80 + a * bpm, LastRand, 12, 1, 0);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                      {
                          mainEffects.longshock();
                      }));
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        CreateGB(new GreenSoulGB(80, "R", 1, bpm * 28));
                        for (int a = 0; a < 24; a++) CreateArrow(80 + a * bpm, LastRand, 12, 1, 0);

                    }));
                    AddInstance(new TimeRangedEvent(80 + bpm * 32, 1, () =>
                        {
                            mainEffects.longshock();
                        }));
                    float beat = 0;
                    string[] arrow =
                        {
                "R","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R","/","/","/",
                "/ ","/","/","/",   "/","/","/","/",
                "R ","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "R","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "/","/","/","/",

                "R","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "R","/","/","/",
                "/ ","/","/","/",   "/","/","/","/",
                "R ","/","/","/",   "/","/","/","/",

                "/","/","/","/",   "R","/","/","/",
                "/","/","/","/",   "/","/","/","/",
                "R","/","/","/",   "R","/","/","/",
                "R","/","/","/",   "/","/","/","/",


            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro80()
                {
                    AddInstance(new TimeRangedEvent(80 + bpm * 24, 1, () =>
                    {
                        mainEffects.shock();
                    }));
                    float beat = 0;
                    string[] arrow =
                        {
                "R(R1)","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","/","/",

                "R(R1)","/","/","/",   "R(R1)","/","/","/",
                "R(R1)","/","/","/",   "R(R1)","/","/","/",
                "R","+0","R1","+01",   "R","+0","R1","+01",
                "R","+0","R1","+01",   "R","+0","R1","+01",

            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else if (arrow[i] == "R(R1)")
                        {
                            CreateArrow(80 + beat, "R", 11, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + beat, "R", 11, 1, 0, ArrowAttribute.SpeedUp);
                            beat += bpm * 0.5f;
                        }
                        else
                        {
                            instance.CreateArrows(80 + beat, 7.1f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro82()
                {
                    Arrow arrow1 = MakeArrow(3, "R", 6.9f, 1, 0);
                    Arrow arrow2 = MakeArrow(3, 2, 11, 0, 0);
                    Arrow arrow3 = MakeArrow(2 + bpm * 0.5f, 1, 11, 0, 0);
                    Arrow arrow4 = MakeArrow(2 + bpm * 1f, 0, 11, 0, 0);
                    Arrow arrow5 = MakeArrow(bpm * 6, "R", 6.9f, 0, 0);
                    Arrow arrow6 = MakeArrow(bpm * 6, "R", 6.9f, 1, 0);
                    Arrow arrow7 = MakeArrow(bpm * 12, "R", 6.9f, 0, 0);
                    Arrow arrow8 = MakeArrow(bpm * 12, "R", 6.9f, 1, 0);
                    CreateEntity(arrow1);
                    CreateEntity(arrow2);
                    CreateEntity(arrow3);
                    CreateEntity(arrow4);
                    CreateEntity(arrow5);
                    CreateEntity(arrow6);
                    CreateEntity(arrow7);
                    CreateEntity(arrow8);
                    AddInstance(new TimeRangedEvent(0, 1, () =>
                    {
                        arrow1.Stop(bpm * 4);
                        arrow2.Stop(bpm * 4);
                        arrow3.Stop(bpm * 4);
                        arrow4.Stop(bpm * 4);
                        arrow5.Stop(bpm * 4);
                        arrow6.Stop(bpm * 4);
                        arrow7.Stop(bpm * 4);
                        arrow8.Stop(bpm * 4);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4 - 2, 1, () =>
                        {
                            arrow1.Delay(bpm * 4);
                            arrow2.Delay(bpm * 4);
                            arrow3.Delay(bpm * 4);
                            arrow4.Delay(bpm * 4);
                            arrow5.Delay(bpm * 4);
                            arrow6.Delay(bpm * 4);
                            arrow7.Delay(bpm * 4);
                            arrow8.Delay(bpm * 4);
                        }));
                    AddInstance(new TimeRangedEvent(bpm * 8 - 2, 1, () =>
                      {
                          arrow1.Delay(bpm * 2);
                          arrow2.Delay(bpm * 2);
                          arrow3.Delay(bpm * 2);
                          arrow4.Delay(bpm * 2);
                          arrow5.Delay(bpm * 2);
                          arrow6.Delay(bpm * 2);
                          arrow7.Delay(bpm * 2);
                          arrow8.Delay(bpm * 2);
                      }));
                    AddInstance(new TimeRangedEvent(bpm * 10 - 2, 1, () =>
                      {
                          arrow1.Delay(bpm * 6);
                          arrow2.Delay(bpm * 6);
                          arrow3.Delay(bpm * 6);
                          arrow4.Delay(bpm * 6);
                          arrow5.Delay(bpm * 6);
                          arrow6.Delay(bpm * 6);
                          arrow7.Delay(bpm * 6);
                          arrow8.Delay(bpm * 6);
                      }));
                    AddInstance(new TimeRangedEvent(bpm * 16 - 2, 1, () =>
                    {
                        arrow1.Delay(bpm * 16);
                        arrow2.Delay(bpm * 16);
                        arrow3.Delay(bpm * 16);
                        arrow4.Delay(bpm * 16);
                        arrow5.Delay(bpm * 16);
                        arrow6.Delay(bpm * 16);
                        arrow7.Delay(bpm * 16);
                        arrow8.Delay(bpm * 16);
                    }));

                }
                public static void Intro85()
                {
                    CreateArrow(80, 2, 11, 0, 0);
                    CreateArrow(80 + bpm * 0.5f, 1, 11, 0, 0);
                    CreateArrow(80 + bpm, 0, 11, 0, 0);
                    CreateArrow(80 + bpm * 6, "R", 6.9f, 0, 0);
                    CreateArrow(80 + bpm * 6, "R", 6.9f, 1, 0);
                    CreateArrow(80 + bpm * 12, "R", 6.9f, 0, 0);
                    CreateArrow(80 + bpm * 12, "R", 6.9f, 1, 0);
                }
                public static void Intro100()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "R(R1)","/","/","/",   "/","/","R(R1)","/",
                "/","/","/","/",   "R(R1)","/","/","/",
                "/ ","/","/","/",   "R(R1)","/","/","/",
                "R(R1) ","/","/","/",   "R(R1)","/","/","/",



            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro101()
                {
                    CreateArrow(80, 2, 11, 1, 0);
                    CreateArrow(80 + bpm * 0.5f, 1, 11, 1, 0);
                    CreateArrow(80 + bpm, 0, 11, 1, 0);
                    CreateArrow(80 + bpm * 6, "R", 6.9f, 0, 0);
                    CreateArrow(80 + bpm * 6, "R", 6.9f, 1, 0);
                    CreateArrow(80 + bpm * 12, "R", 6.9f, 0, 0);
                    CreateArrow(80 + bpm * 12, "R", 6.9f, 1, 0);
                }
                public static void Intro109()
                {
                    CreateArrow(80, "R", 6.9f, 0, 0);
                    CreateArrow(80, 2, 11, 1, 0);
                    CreateArrow(80 + bpm * 0.5f, 1, 11, 1, 0);
                    CreateArrow(80 + bpm, 0, 11, 1, 0);
                    CreateArrow(80 + bpm * 6, "R", 6.9f, 0, 0);
                    CreateArrow(80 + bpm * 6, "R", 6.9f, 1, 0);
                    CreateArrow(80 + bpm * 12, "R", 6.9f, 0, 0);
                    CreateArrow(80 + bpm * 12, "R", 6.9f, 1, 0);
                }
                public static void Intro115()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                "R(R1)","/","/","/",   "/","/","/","R",
                "+21","+2","+21","/",   "/","/","/","/",
                "R(R1)","/","/","/",   "/","/","R","+21",
                 "+2","+21","/","/",     "/","/","R","+21",

                "+2","+21","+2","+21","+2",

            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            instance.CreateArrows(80 + beat, 6.85f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro116()
                {
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                     {
                         CreateGB(new GreenSoulGB(80, "R", 0, bpm * 64));
                         for (int a = 0; a < 17; a++) CreateArrow(80 + a * bpm * 4, LastRand, 8, 0, 0);
                         CreateGB(new GreenSoulGB(80, "R", 1, bpm * 64));
                         for (int a = 0; a < 17; a++) CreateArrow(80 + a * bpm * 4, LastRand, 8, 1, 0);
                     }));
                    float beat = 0;
                    string[] arrow =
                        {
                "/","/","/","/",   "/","/","/","/",
                "/","/","/","/",   "/","/","R(R1)","/",
                "/","/","/","/",   "/","/","/","/",
                "/","/","R(R1)","/",   "/","/","/","/",



            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            CreateArrow(80 + beat, "R", 11, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + beat, "R", 11, 1, 0, ArrowAttribute.SpeedUp);
                            beat += bpm * 0.5f;
                        }
                    }
                }
            }

            public static class ExPlus
            {
                public static void Intro0()
                {
                    float beat = 0;
                    string[] arrow =
                    {
                        "$0","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",

                        "R(R1)","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "R1","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "R(R1)","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "R","/","/","/",
                        "R","+2","/","/",   "R","/","/","/",

                        "R(R1)","/","/","/",   "R1","/","/","/",
                        "R1","+21","/","/",   "R1","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        instance.NormalizedChart(beat, 6, arrow[i]);
                        beat += instance.BeatTime(0.5f);
                    }
                }
                public static void Intro1()
                {
                    float beat = bpm * 16;
                    string[] arrow =
                    {
                        "Rot1","SR04","R1","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",

                        "R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",

                        "Rot2","SR14","R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",

                        "R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "Rot1","SR12","R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "Rot2","SR02","R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",

                        "Rot3","G028","G238","D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        //

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "($0)($2)($01)($21)",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",

                        "(R1)(R)(+2)","/","/","/",   "(+2)(+2)","/","/","/",
                        "(+2)(+2)","/","/","/",   "(R)(R1)(+21)","/","/","/",
                        "(+21)(+21)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+2)(+2)","/","/","/",

                        "(+2)(+2)","/","/","/",   "(R)(R1)(+21)","/","/","/",
                        "(+21)(+21)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+21)(+21)","/","/","/",

                        "(R1)(R)(+2)","/","/","/",   "(+2)(+2)","/","/","/",
                        "(+2)(+2)","/","/","/",   "(R)(R1)(+21)","/","/","/",
                        "(+21)(+21)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+2)(+2)","/","/","/",

                        "(+2)(+2)","/","/","/",   "(R)(R1)(+21)","/","/","/",
                        "(+21)(+21)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(R1)(R)(+2)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+21)(+21)","/","/","/",
                        //

                        "(R1)(R)(+2)","/","/","/",   "(+2)(+2)","/","/","/",
                        "(+2)(+2)","/","/","/",   "(R)(R1)(+21)","/","/","/",
                        "(+21)(+21)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+2)(+2)","/","/","/",

                        "(+2)(+2)","/","/","/",   "(R)(R1)(+21)","/","/","/",
                        "(+21)(+21)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+21)(+21)","/","/","/",

                        "(R1)(R)(+2)","/","/","/",   "(+2)(+2)","/","/","/",
                        "(+2)(+2)","/","/","/",   "(R)(R1)(+21)","/","/","/",
                        "(+21)(+21)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(+2)(+2)","/","/","/",

                        "(+2)(+2)","/","/","/",   "(R)(R1)(+21)","/","/","/",
                        "(+21)(+21)","/","/","/",   "(+21)(+21)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "(R1)(R)(+2)","/","/","/",
                        "(R1)(R)(+2)","/","/","/",   "/","/","/","/",

                        "WR01","R1","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "WR01","(R1)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "WR11","R","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

                        "WR11","R","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "WR11","(R)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "WR11","R","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        //

                        "WR01","R1","/","/","/",   "/","/","/","/",
                        "(R)","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "WR11","R","/","/","/",   "/","/","/","/",

                        "(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "R(R1)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

                        "(R)($01)($21)","/","/","/",   "(R)","/","/","/",
                        "(R)","/","/","/",   "(R)($01)($21)","/","/","/",
                        "(R)","/","/","/",   "(R)","/","/","/",
                        "G122","G332","/","/","/","/",   "/","/","/","/",

                        "G201","G011","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "($2)($01)","/","/","/",   "/","/","/","/",
                        "($2)($01)","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "Rot1")
                        {
                            AddInstance(new TimeRangedEvent(beat, bpm * 16, () =>
                            {

                                box.rotate = box.rotate * 0.92f - 20f * 0.08f;
                                heartrot = heartrot * 0.92f + 20f * 0.08f;
                            }));
                        }
                        else if (arrow[i] == "Rot2")
                        {
                            AddInstance(new TimeRangedEvent(beat, bpm * 16, () =>
                            {

                                box.rotate = box.rotate * 0.92f + 20f * 0.08f;
                                heartrot = heartrot * 0.92f - 20f * 0.08f;
                            }));
                        }
                        else if (arrow[i] == "Rot3")
                        {
                            float speed = 0;
                            AddInstance(new TimeRangedEvent(beat, bpm * 64, () =>
                            {

                                box.rotate += speed;
                                speed = speed * 0.99f + 9f * 0.01f;
                                heartrot = heartrot * 0.92f + 0 * 0.08f;
                            }));
                            AddInstance(new TimeRangedEvent(beat + bpm * 64, bpm * 80, () =>
                            {

                                box.rotate = box.rotate * 0.95f + (box.rotate + (360 - box.rotate % 360.00f)) * 0.05f;
                                heartrot = heartrot * 0.92f + 0 * 0.08f;
                            }));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, 7f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro2()
                {
                    float beat = bpm * 16;
                    string[] arrow =
                    {
                        "R(R1)","/","/","/",   "WR04","R1","/","/","/",
                        "(R1)(+01)","/","/","/",   "R1","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "R1","/","/","/",

                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "WR11","R","/","/","/",
                        "R(+0)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",

                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",

                        "$1","/","/","/",   "($01)($21)","/","($01)($21)","/",
                        "($01)($21)","/","($01)($21)","/",   "($01)($21)","/","($01)($21)","/",
                        "$3","/","/","/",   "($0)($2)","/","($0)($2)","/",
                        "($0)($2)","/","($0)($2)","/",   "($0)($2)","/","($0)($2)","/",

                        "R(R1)","/","/","/",   "WR04","R1","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "R1","/","/","/",

                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "WR13","R","/","/","/",
                        "(R)(+0)","/","/","/",   "/","(R)","(+0)","(+0)",
                        "(+0)(+0)","/","/","/",   "(R)(+0)","/","/","/",

                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "WR03","(R1)","/","/","/",   "R1","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "R1","/","/","/",

                        "(R1)(+01)","/","/","/",  "/","/","/","/",
                        "WR11","R","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        //
                        "/","/","(R)","/",    "/","/","WR12","(R)","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","WR02","R1","/",

                        "(+01)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",  "R(R1)","/","/","/",

                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",

                        "$1","/","/","/",   "($01)($21)","/","($01)($21)","/",
                        "($01)($21)","/","($01)($21)","/",   "($01)($21)","/","($01)($21)","/",
                        "$3","/","/","/",   "($0)($2)","/","($0)($2)","/",
                        "($0)($2)","/","($0)($2)","/",   "($0)($2)","/","($0)($2)","/",

                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",

                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",

                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",

                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "(R)(R1)(R1)","/","/","/",   "(R)(R)(R1)","/","/","/",
                        "(R)(R1)(R1)","/","/","/",   "(R)(R)(R1)","/","/","/",
                        //
                        
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "Rot3")
                        {
                            float speed = 0;
                            AddInstance(new TimeRangedEvent(beat, bpm * 64, () =>
                            {

                                box.rotate += speed;
                                speed = speed * 0.99f + 9f * 0.01f;
                                heartrot = heartrot * 0.92f + 0 * 0.08f;
                            }));
                            AddInstance(new TimeRangedEvent(beat + bpm * 64, bpm * 80, () =>
                            {

                                box.rotate = box.rotate * 0.95f + (box.rotate + (360 - box.rotate % 360.00f)) * 0.05f;
                                heartrot = heartrot * 0.92f + 0 * 0.08f;
                            }));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, 7f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro3()
                {
                    float beat = bpm * 16;
                    string[] arrow =
                    {
                        "$01($21)","/","/","/",   "$1","/","$0","/",
                        "$01($01)","/","/","/",   "$1","/","$0","/",
                        "$21($21)","/","/","/",   "$1","/","$0","/",
                        "$01($01)","/","/","/",   "$1","/","$0","/",

                        "$21($21)","/","/","/",   "$2","/","$0","/",
                        "$01($01)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",

                        "$11($31)","/","/","/",   "$1","/","$0","/",
                        "$11($31)","/","/","/",   "$1","/","$0","/",
                        "$11($31)","/","/","/",   "$1","/","$0","/",
                        "$11($31)","/","/","/",   "$1","/","$0","/",

                        "$11($31)","/","$0","/",   "$2","/","$0","/",
                        "$11($31)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",

                        "$01($21)","/","/","/",   "$1","/","$2","/",
                        "$21($21)","/","/","/",   "$1","/","$2","/",
                        "$01($01)","/","/","/",   "$1","/","$2","/",
                        "$21($21)","/","/","/",   "$1","/","$2","/",

                        "$01($01)","/","/","/",   "$0","/","$2","/",
                        "$21($21)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",

                        "$01($21)","/","/","/",   "$1","/","$2","/",
                        "$21($21)","/","/","/",   "$1","/","$2","/",
                        "$11($31)","/","/","/",   "$1","/","$2","/",
                        "$11($31)","/","/","/",   "$1","/","$2","/",

                        "$01($01)","/","$0","/",   "$2","/","$0","/",
                        "$21($21)","/","/","/",   "R(R1)","/","/","/",
                        "$0","/","$21","/",   "$1","/","$31","/",
                        "$1","/","$31","/",   "$2","/","$01","/",
                        //
                        "G204","G014","($01)($2)","/","/","/",   "/","/","/","/",
                        "($01)($2)","/","/","/",   "/","/","/","/",
                        "Move1","($01)($2)","/","/","/",   "/","/","/","/",
                        "($01)($2)","/","/","/",   "/","/","/","/",

                        "($01)($2)","/","/","/",   "/","/","/","/",
                        "($01)($2)","/","/","/",   "/","/","/","/",
                        "Move2","($01)($2)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "R(R1)","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "Move1","R(R1)","/","/","/",   "R","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

                        "R(R1)","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "Move2","R(R1)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

                        "(R1)($0)","+1","+1","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "R(R1)","/","/","/",
                        "Move1","R(R1)","/","/","/",   "(R1)","/","/","/",
                        "R(R1)","/","/","/",   "(R1)","/","/","/",

                        "R(R1)","/","/","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "Move2","R(R1)","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "(R1)($0)","+1","+1","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "Move1","R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "(R1)($0)","+1","+1","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "R","/","/","/",
                        "Move2","R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        //
                        "(R1)($0)","+1","+1","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "R(R1)","/","/","/",
                        "Move1","R(R1)","/","/","/",   "(R1)","/","/","/",
                        "R(R1)","/","/","/",   "(R1)","/","/","/",

                        "R(R1)","/","/","/",   "(R1)","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                        "Move3","R(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, 7.5f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "Move1")
                        {
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "a");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 80, 240, 270, bpm * 32, 0, Color.Cyan), "b");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 160, 240, 270, bpm * 32, 0, Color.Cyan), "c");
                                Extends.LineMoveLibrary.VecLerpAdd("a", bpm * 32, new(-8f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("b", bpm * 32, new(-9, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("c", bpm * 32, new(-10f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("a", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("b", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("c", bpm * 16, 0.8f, 0, bpm * 8, 0);
                            }));
                        }
                        else if (arrow[i] == "Move2")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "A");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 80, 240, 270, bpm * 32, 0, Color.Cyan), "B");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 160, 240, 270, bpm * 32, 0, Color.Cyan), "C");
                                Extends.LineMoveLibrary.VecLerpAdd("A", bpm * 32, new(8f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("B", bpm * 32, new(9, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("C", bpm * 32, new(10f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("A", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("B", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("C", bpm * 16, 0.8f, 0, bpm * 8, 0);
                            }));
                        }
                        else if (arrow[i] == "Move3")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "a");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 80, 240, 270, bpm * 32, 0, Color.Cyan), "b");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 160, 240, 270, bpm * 32, 0, Color.Cyan), "c");
                                Extends.LineMoveLibrary.VecLerpAdd("a", bpm * 32, new(-8.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.VecLerpAdd("b", bpm * 32, new(-9.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.VecLerpAdd("c", bpm * 32, new(-10.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.AlphaSin("a", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("b", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("c", bpm * 16, 0.8f, 0, bpm * 8, 0);
                            }));
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "A");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 80, 240, 270, bpm * 32, 0, Color.Cyan), "B");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 160, 240, 270, bpm * 32, 0, Color.Cyan), "C");
                                Extends.LineMoveLibrary.VecLerpAdd("A", bpm * 32, new(8.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.VecLerpAdd("B", bpm * 32, new(9.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.VecLerpAdd("C", bpm * 32, new(10.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.AlphaSin("A", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("B", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("C", bpm * 16, 0.8f, 0, bpm * 8, 0);
                            }));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, 7.5f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro4()
                {
                    float beat = bpm * 16;
                    float arrowspeed = 7.5f;
                    string[] arrow =
                    {
                        "$01($21)","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "($01)($111)","/","/","/",   "($01)($211)","/","/","/",
                        "($01)($311)","/","/","/",   "/","/","/","/",

                         "WR11","/","/","/","/",   "/","/","/","/",
                         "R(R1)","/","/","/",   "/","/","/","/",
                         "WR01","/","/","/","/",   "/","/","/","/",
                         "R(R1)","/","/","/",   "/","/","/","/",

                         "R(R1)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "R(R1)","/","/","/",
                        "WR04","R1","/","/","/",   "/","/","/","/",
                         "R1","/","/","/",   "/","/","/","/",

                         "WR12","/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "R(R1)","/","/","/",   "R(R1)","/","/","/",
                         "R(R1)","/","/","/",   "/","/","/","/",

                        "WR02","(R1)(+011)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)(+011)(R)(+2)","/","/","/",   "(R)(+2)","/","/","/",
                        "(R)(+2)","/","/","/",   "/","/","/","/",

                        "WR02","(R1)(+011)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)(+011)(R)(+2)","/","/","/",   "(R)(+2)","/","/","/",
                        "(R)(+2)","/","/","/",   "/","/","/","/",

                         "G034","$0($0)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",

                         "G216","G124","$21($21)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         //
                         "G004","G334","$0($0)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",

                         "G122","$21($21)($0)($0)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",

                         "WR01","R1","/","/","/",   "/","/","/","/",
                         "R1","/","/","/",   "WR13","R","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "R","/","/","/",   "/","/","/","/",

                         "R","/","/","/",   "WR01","R1","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",

                         "WR01","R1","/","/","/",   "/","/","/","/",
                         "R1","/","/","/",   "WR13","R","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "R","/","/","/",   "/","/","/","/",

                         "R","/","/","/",   "WR01","R1","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "($0)($2)($11)","/","/","/",   "($0)($2)($11)","/","/","/",
                         "($0)($2)($11)","/","/","/",   "/","/","/","/",

                         "R(D1)","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",

                         "R(D1)","/","/","/",   "R(D1)","/","/","/",
                         "R(D1)","/","/","/",   "R(D1)","/","/","/",
                         "speedchange1","$0($1)","/","$11($21)","/",  "$1($2)","/","$01($11)","/",
                         "$2($3)","/","$31($01)","/",  "$3($0)","/","$21($31)","/",

                         "EF","/","/",
                    };
                    string[] effect = { };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, arrowspeed, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, arrowspeed, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "speedchange1")
                        {
                            arrowspeed = 10.85f;
                        }
                        else if (arrow[i] == "Move1")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "a");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 120, 240, 270, bpm * 32, 0, Color.Cyan), "b");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 240, 240, 270, bpm * 32, 0, Color.Cyan), "c");
                                Extends.LineMoveLibrary.VecLerpAdd("a", bpm * 32, new(-7f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("b", bpm * 32, new(-8, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("c", bpm * 32, new(-9f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("a", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("b", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("c", bpm * 16, 0.8f, 0, bpm * 24, 0);
                            }));
                        }
                        else if (arrow[i] == "Move2")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "A");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 120, 240, 270, bpm * 32, 0, Color.Cyan), "B");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 240, 240, 270, bpm * 32, 0, Color.Cyan), "C");
                                Extends.LineMoveLibrary.VecLerpAdd("A", bpm * 32, new(7f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("B", bpm * 32, new(8, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("C", bpm * 32, new(9f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("A", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("B", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("C", bpm * 16, 0.8f, 0, bpm * 24, 0);
                            }));
                        }
                        else if (arrow[i] == "Move3")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "aa");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 120, 240, 270, bpm * 32, 0, Color.Cyan), "bb");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 240, 240, 270, bpm * 32, 0, Color.Cyan), "cc");
                                Extends.LineMoveLibrary.VecLerpAdd("aa", bpm * 32, new(-7f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("bb", bpm * 32, new(-8, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("cc", bpm * 32, new(-9f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("aa", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("bb", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("cc", bpm * 16, 0.8f, 0, bpm * 24, 0);
                            }));
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "aaa");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 120, 240, 270, bpm * 32, 0, Color.Cyan), "bbb");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 240, 240, 270, bpm * 32, 0, Color.Cyan), "ccc");
                                Extends.LineMoveLibrary.VecLerpAdd("aaa", bpm * 32, new(7f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("bbb", bpm * 32, new(8, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("ccc", bpm * 32, new(9f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("aaa", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("bbb", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("ccc", bpm * 16, 0.8f, 0, bpm * 24, 0);
                            }));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, arrowspeed, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro5()
                {
                    float beat = bpm * 16;
                    float arrowspeed = 7.85f;
                    string[] arrow =
                    {
                        "R(R1)","/","/","/",  "(R1)","/","/","/",
                        "R","/","R1","/",   "!(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "@(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                         "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "WR01","!(R1)(+01)","/","/","/",   "(R1)","/","/","/",
                        "(R1)","/","@(R)(+0)","/",   "WR11","R","/","/","/",
                        "R","/","/","/",   "R","/","!(R1)(+01)","/",
                        "R","/","/","/",   "(R1)","/","/","/",

                        "@(R1)(+01)(R)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "!(R1)(+01)(R)","/","/","/",
                        "R","/","R1","/",   "R","/","R1","/",
                        "R","/","R1","/",   "R","/","R1","/",
                        //
                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(R0)","/","/","/",   "/","/","/","/",
                        "!($0)($21)","/","~($1)($31)","/",   "@($2)($01)","/","/","/",
                        "/","/","/","/",   "!($0)($21)","/","~($1)($31)","/",
                        "@($2)($01)","/","/","/",   "R(R1)","/","/","/",

                        "~(R1)(R0)","/","/","/",   "!(R1)(R0)","/","/","/",
                        "/","/","!(R1)(R0)","/",   "/","/","/","/",
                        "/","/","/","/",   "!(R1)(+20)","/","/","/",
                        "@(R1)(+20)","/","/","/",   "!(R1)(+20)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(R)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(R)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(R)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!($0)($2)","/","@($01)($21)","/",   "!($0)($2)","/","@($01)($21)","/",

                        "~($1)($3)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(R)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(R)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(R)","/","/","/",   "@(R1)(+2)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "R","/","R1","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","R1","/",   "R","/","R1","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!($0)($2)($01)($21)","/","/","/",   "T(R)(+2)","T(+21)(+21)","T(+2)(+2)",
                        "(+21)(+21)","/","/","/",   "@($0)($2)($01)($21)","/","/","/",
                        "T(R)(+2)","T(+21)(+21)","T(+2)(+2)",   "(+21)(+21)","/","/","/",
                        "T(R)(+2)","T(+21)(+21)","T(+2)(+2)","T(+21)(+21)","T(+2)(+2)","T(+21)(+21)",

                        "(D)(+2)","/","/","/",   "/","/","/","/",
                        "speedchange1","/","/","/","/",   "~(R1)(R)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "~(R1)(R)","/","/","/",   "/","/","/","/",
                        "G008","G118","G228","G338","~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, arrowspeed, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, arrowspeed, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "speedchange3")
                        {
                            arrowspeed = 7.85f;
                        }
                        else if (arrow[i] == "speedchange1")
                        {
                            arrowspeed = 10.85f;
                        }
                        else if (arrow[i][0] == '!')
                        {
                            instance.CreateArrows(beat, arrowspeed * 1.2f, arrow[i].Replace("!", ""), ArrowAttribute.RotateR);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == '@')
                        {
                            instance.CreateArrows(beat, arrowspeed * 1.2f, arrow[i].Replace("@", ""), ArrowAttribute.RotateL);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == '~')
                        {
                            instance.CreateArrows(beat, arrowspeed * 1.75f, arrow[i].Replace("~", ""), ArrowAttribute.SpeedUp);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'T')
                        {
                            instance.CreateArrows(beat, arrowspeed * 1.5f, arrow[i].Replace("T", ""), ArrowAttribute.SpeedUp);
                            beat += bpm * 0.66666f;
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, arrowspeed, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
            }
            public static class Remake
            {
                public static void Intro0()
                {
                    float beat = 0;
                    string[] arrow =
                    {
                        "$0","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",

                        "R(R1)","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "R1","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "R(R1)","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "R","/","/","/",
                        "R","+0","/","/",   "R","/","/","/",

                        "R(R1)","/","/","/",   "R1","/","/","/",
                        "R1","+01","/","/",   "R1","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        instance.NormalizedChart(beat, 6, arrow[i]);
                        beat += instance.BeatTime(0.5f);
                    }
                }
                public static void Intro1()
                {
                    float beat = bpm * 16;
                    string[] arrow =
                    {
                        "Rot1","SR04","R1","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",

                        "R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",

                        "Rot2","SR14","R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",

                        "R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "Rot1","SR12","R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "Rot2","SR02","R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",   "R1","/","/","/",
                        "R1","/","/","/",   "/","/","/","/",

                        "Rot3","G008","G218","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "($0)($21)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        //

                       "($0)($21)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "($0)($2)($01)($21)",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",

                        "(R1)(R)","/","/","/",   "(+0)","/","/","/",
                        "(+0)","/","/","/",   "(R)(R1)","/","/","/",
                        "(+01)","/","/","/",   "(+01)","/","/","/",
                        "(R1)(R)","/","/","/",   "(+0)","/","/","/",

                        "(+0)","/","/","/",   "(R)(R1)","/","/","/",
                        "(+01)","/","/","/",   "(+01)","/","/","/",
                        "(R1)(+2)","/","/","/",   "(+21)(+2)","/","/","/",
                        "(R1)(+2)","/","/","/",   "(+21)(+2)","/","/","/",

                        "(R1)(R)","/","/","/",   "(+0)","/","/","/",
                        "(+0)","/","/","/",   "(R)(R1)","/","/","/",
                        "(+01)","/","/","/",   "(+01)","/","/","/",
                        "(R1)(R)","/","/","/",   "(+0)","/","/","/",

                        "(+0)","/","/","/",   "(R)(R1)","/","/","/",
                        "(+01)","/","/","/",   "(+01)","/","/","/",
                        "(R1)(+2)","/","/","/",   "(+21)(+2)","/","/","/",
                        "(R1)(+2)","/","/","/",   "(+21)(+2)","/","/","/",
                        //

                       "(R1)(R)","/","/","/",   "(R)","/","/","/",
                        "(R)","/","/","/",   "(R)(R1)","/","/","/",
                        "(R1)","/","/","/",   "(R1)","/","/","/",
                        "(R1)(R)","/","/","/",   "(R)","/","/","/",

                        "(R)","/","/","/",   "(R1)(R)","/","/","/",
                        "(R1)","/","/","/",   "(R1)","/","/","/",
                        "(R1)(R)","/","/","/",   "(R1)(R)","/","/","/",
                        "(R1)(R)","/","/","/",   "(R1)(R)","/","/","/",

                        "(R1)(R)","/","/","/",   "(R)","/","/","/",
                        "(R)","/","/","/",   "(R)(R1)","/","/","/",
                        "(R1)","/","/","/",   "(R1)","/","/","/",
                        "(R1)(R)","/","/","/",   "(R)","/","/","/",

                        "(R)","/","/","/",   "(R)(R1)","/","/","/",
                        "(R1)","/","/","/",   "(R1)","/","/","/",
                        "(R1)(R)","/","/","/",   "(R1)(R)","/","/","/",
                        "(R1)(R)","/","/","/",   "/","/","/","/",

                        "WR01","R1","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "WR01","(R1)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "WR11","R","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

                        "WR11","R","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "WR11","(R)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "WR11","R","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        //

                        "WR01","R1","/","/","/",   "/","/","/","/",
                        "(R)","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "WR11","R","/","/","/",   "/","/","/","/",

                        "(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "R(R1)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

                        "($3)($01)($21)","/","/","/",   "(R)","/","/","/",
                        "(R)","/","/","/",   "($1)($01)($21)","/","/","/",
                        "(R)","/","/","/",   "(R)","/","/","/",
                        "G202","G012","/","/","/","/",   "/","/","/","/",

                        "G201","G011","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "($2)($01)","/","/","/",   "/","/","/","/",
                        "($2)($01)","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "Rot1")
                        {
                            AddInstance(new TimeRangedEvent(beat, bpm * 16, () =>
                            {

                                box.rotate = box.rotate * 0.92f - 20f * 0.08f;

                            }));
                        }
                        else if (arrow[i] == "Rot2")
                        {
                            AddInstance(new TimeRangedEvent(beat, bpm * 16, () =>
                            {

                                box.rotate = box.rotate * 0.92f + 20f * 0.08f;

                            }));
                        }
                        else if (arrow[i] == "Rot3")
                        {
                            float speed = 0;
                            AddInstance(new TimeRangedEvent(beat, bpm * 64, () =>
                            {

                                box.rotate += speed;
                                speed = speed * 0.99f + 9f * 0.01f;

                            }));
                            AddInstance(new TimeRangedEvent(beat + bpm * 64, bpm * 80, () =>
                            {

                                box.rotate = box.rotate * 0.95f + (box.rotate + (360 - box.rotate % 360.00f)) * 0.05f;

                            }));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, 7f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro2()
                {
                    float beat = bpm * 16;
                    string[] arrow =
                    {
                        "R(R1)","/","/","/",   "WR04","R1","/","/","/",
                        "(R1)(+01)","/","/","/",   "R1","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "R1","/","/","/",

                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "WR11","R","/","/","/",
                        "R(+0)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",

                        "WR11","/","/","/","/",   "/","/","/","/",
                        "WR01","/","/","/","/",   "/","/","/","/",
                        "WR11","/","/","/","/",   "/","/","/","/",
                        "WR01","/","/","/","/",   "/","/","/","/",

                        "($1)($1)","/","/","/",   "($01)","/","($21)","/",
                        "($01)","/","($21)","/",   "($01)","/","($21)","/",
                        "$31($31)","/","/","/",   "($2)","/","($0)","/",
                        "($2)","/","($0)","/",   "($2)","/","($0)","/",

                        "R(R1)","/","/","/",   "WR04","R1","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "R1","/","/","/",

                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "WR13","R","/","/","/",
                        "(R)(+0)","/","/","/",   "/","(R)","(+0)","(+0)",
                        "(+0)(+0)","/","/","/",   "(R)(+0)","/","/","/",

                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "WR03","(R1)","/","/","/",   "R1","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "R1","/","/","/",

                        "(R1)(+01)","/","/","/",  "/","/","/","/",
                        "WR11","R","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        //
                        "/","/","(R)","/",    "/","/","WR12","(R)","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","WR02","R1","/",

                        "(+01)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",  "R(R1)","/","/","/",

                        "WR11","/","/","/","/",   "/","/","/","/",
                        "WR01","/","/","/","/",   "/","/","/","/",
                        "WR11","/","/","/","/",   "/","/","/","/",
                        "WR01","/","/","/","/",   "/","/","/","/",

                        "($1)($1)","/","/","/",   "($01)","/","($21)","/",
                        "($01)","/","($21)","/",   "($01)","/","($21)","/",
                        "$31($31)","/","/","/",   "($2)","/","($0)","/",
                        "($2)","/","($0)","/",   "($2)","/","($0)","/",

                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",

                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",

                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "WR11","WR01","/","/","/","/",   "/","/","/","/",

                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "(R)(R1)","/","/","/",   "/","/","/","/",
                        "(R)(+11)","/","/","/",   "(R)(-11)","/","/","/",
                        "(R)(+11)","/","/","/",   "(R)(-11)","/","/","/",
                        //
                        
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "Rot3")
                        {
                            float speed = 0;
                            AddInstance(new TimeRangedEvent(beat, bpm * 64, () =>
                            {

                                box.rotate += speed;
                                speed = speed * 0.99f + 9f * 0.01f;
                                heartrot = heartrot * 0.92f + 0 * 0.08f;
                            }));
                            AddInstance(new TimeRangedEvent(beat + bpm * 64, bpm * 80, () =>
                            {

                                box.rotate = box.rotate * 0.95f + (box.rotate + (360 - box.rotate % 360.00f)) * 0.05f;
                                heartrot = heartrot * 0.92f + 0 * 0.08f;
                            }));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, 7f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro3()
                {
                    float beat = bpm * 16;
                    string[] arrow =
                    {
                        "!$01($01)","/","/","/",   "$1","/","$0","/",
                        "@$01($01)","/","/","/",   "$1","/","$0","/",
                        "!$21($21)","/","/","/",   "$1","/","$0","/",
                        "@$01($01)","/","/","/",   "$1","/","$0","/",

                        "!$21($21)","/","/","/",   "$2","/","$0","/",
                        "@$01($01)","/","/","/",   "R(+11)","/","/","/",
                        "(-1)(+11)","/","/","/",   "R(+11)","/","/","/",
                        "(-1)(+11)","/","/","/",   "R(D1)","/","/","/",

                        "~$11($11)","/","/","/",   "$1","/","$0","/",
                        "~$11($11)","/","/","/",   "$1","/","$0","/",
                        "~$11($11)","/","/","/",   "$1","/","$0","/",
                        "~$11($11)","/","/","/",   "$1","/","$0","/",

                        "~$11($11)","/","$0","/",   "$2","/","$0","/",
                        "~$11($11)","/","/","/",   "R(+11)","/","/","/",
                        "(-1)(+11)","/","/","/",   "R(+11)","/","/","/",
                        "(-1)(+11)","/","/","/",   "R(D1)","/","/","/",

                        "!$21($21)","/","/","/",   "$1","/","$2","/",
                        "@$21($21)","/","/","/",   "$1","/","$2","/",
                        "!$01($01)","/","/","/",   "$1","/","$2","/",
                        "@$21($21)","/","/","/",   "$1","/","$2","/",

                        "!$01($01)","/","/","/",   "$0","/","$2","/",
                        @"$21($21)","/","/","/",   "R(+11)","/","/","/",
                        "(-1)(+11)","/","/","/",   "R(+11)","/","/","/",
                        "(-1)(+11)","/","/","/",   "R(D1)","/","/","/",

                        "!$01($01)","/","/","/",   "$1","/","$2","/",
                        "@$21($21)","/","/","/",   "$1","/","$2","/",
                        "~$11($11)","/","/","/",   "$1","/","$2","/",
                        "~$11($11)","/","/","/",   "$1","/","$2","/",

                        "!$01($01)","/","$0","/",   "$2","/","$0","/",
                        "@$21($21)","/","/","/",   "R(D1)","/","/","/",
                        "$0","/","$21","/",   "$1","/","$31","/",
                        "$1","/","$31","/",   "$2","/","$01","/",
                        //
                        "G204","G014","($01)($2)","/","/","/",   "/","/","/","/",
                        "($01)($2)","/","/","/",   "/","/","/","/",
                        "Move1","($01)($2)","/","/","/",   "/","/","/","/",
                        "($01)($2)","/","/","/",   "/","/","/","/",

                        "($01)($2)","/","/","/",   "/","/","/","/",
                        "($01)($2)","/","/","/",   "/","/","/","/",
                        "Move2","($01)($2)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "R(R1)","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "Move1","R(R1)","/","/","/",   "R","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

                        "R(R1)","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "Move2","R(R1)","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

                        "@(R1)(R)","@+0","@+0","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "R(R1)","/","/","/",
                        "Move1","R(R1)","/","/","/",   "(R1)","/","/","/",
                        "R(R1)","/","/","/",   "(R1)","/","/","/",

                        "R(R1)","/","/","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "Move2","R(R1)","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "!(R1)(R)","!+0","!+0","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "Move1","R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "/","/","/","/",

                        "@(R1)(R)","@+0","@+0","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "R","/","/","/",
                        "Move2","R","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        //
                        "!(R1)(R)","!+0","!+0","/",   "(R1)","/","/","/",
                        "(R1)","/","/","/",   "R(R1)","/","/","/",
                        "Move1","R(R1)","/","/","/",   "(R1)","/","/","/",
                        "R(R1)","/","/","/",   "(R1)","/","/","/",

                        "R(R1)","/","/","/",   "(R1)","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                        "Move3","R(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, 7f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, 7.2f, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == '!')
                        {
                            instance.CreateArrows(beat, 7.2f * 1.08f, arrow[i].Replace("!", ""), ArrowAttribute.RotateR);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == '@')
                        {
                            instance.CreateArrows(beat, 7.2f * 1.08f, arrow[i].Replace("@", ""), ArrowAttribute.RotateL);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == '~')
                        {
                            instance.CreateArrows(beat, 7.2f * 1.2f, arrow[i].Replace("~", ""), ArrowAttribute.SpeedUp);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "Move1")
                        {
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "a");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 80, 240, 270, bpm * 32, 0, Color.Cyan), "b");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 160, 240, 270, bpm * 32, 0, Color.Cyan), "c");
                                Extends.LineMoveLibrary.VecLerpAdd("a", bpm * 32, new(-8f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("b", bpm * 32, new(-9, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("c", bpm * 32, new(-10f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("a", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("b", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("c", bpm * 16, 0.8f, 0, bpm * 8, 0);
                            }));
                        }
                        else if (arrow[i] == "Move2")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "A");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 80, 240, 270, bpm * 32, 0, Color.Cyan), "B");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 160, 240, 270, bpm * 32, 0, Color.Cyan), "C");
                                Extends.LineMoveLibrary.VecLerpAdd("A", bpm * 32, new(8f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("B", bpm * 32, new(9, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("C", bpm * 32, new(10f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("A", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("B", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("C", bpm * 16, 0.8f, 0, bpm * 8, 0);
                            }));
                        }
                        else if (arrow[i] == "Move3")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "a");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 80, 240, 270, bpm * 32, 0, Color.Cyan), "b");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 160, 240, 270, bpm * 32, 0, Color.Cyan), "c");
                                Extends.LineMoveLibrary.VecLerpAdd("a", bpm * 32, new(-8.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.VecLerpAdd("b", bpm * 32, new(-9.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.VecLerpAdd("c", bpm * 32, new(-10.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.AlphaSin("a", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("b", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("c", bpm * 16, 0.8f, 0, bpm * 8, 0);
                            }));
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "A");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 80, 240, 270, bpm * 32, 0, Color.Cyan), "B");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 160, 240, 270, bpm * 32, 0, Color.Cyan), "C");
                                Extends.LineMoveLibrary.VecLerpAdd("A", bpm * 32, new(8.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.VecLerpAdd("B", bpm * 32, new(9.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.VecLerpAdd("C", bpm * 32, new(10.5f, 0), 0.1f);
                                Extends.LineMoveLibrary.AlphaSin("A", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("B", bpm * 16, 0.8f, 0, bpm * 8, 0);
                                Extends.LineMoveLibrary.AlphaSin("C", bpm * 16, 0.8f, 0, bpm * 8, 0);
                            }));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, 7.5f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro4()
                {
                    float beat = bpm * 16;
                    float arrowspeed = 7.2f;
                    string[] arrow =
                    {
                        "$01($21)","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "($01)($111)","/","/","/",   "($01)($211)","/","/","/",
                        "($01)($311)","/","/","/",   "/","/","/","/",

                         "WR01","/","/","/","/",   "/","/","/","/",
                         "R(R1)","/","/","/",   "/","/","/","/",
                         "WR11","/","/","/","/",   "/","/","/","/",
                         "R(R1)","/","/","/",   "/","/","/","/",

                         "R(R1)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "R(R1)","/","/","/",
                        "WR04","R1","/","/","/",   "/","/","/","/",
                         "R1","/","/","/",   "/","/","/","/",

                         "WR12","/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "(R)(R1)(+01)","/","/","/",   "(R)(R1)(+01)","/","/","/",
                         "R(R1)","/","/","/",   "/","/","/","/",

                        "WR02","(R1)(+011)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)(+011)(R)","/","/","/",   "(R)(R1)(+01)","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",

                        "WR02","(R1)(+011)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)(+011)(R)","/","/","/",   "(R)(+0)","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",

                         "G014","$0($0)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",

                         "G202","$2($2)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         //
                         "G114","G202","$2($2)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",

                         "G302","$11($11)($3)($3)","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",

                         "WR01","R1","/","/","/",   "/","/","/","/",
                         "R1","/","/","/",   "WR13","R","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "R","/","/","/",   "/","/","/","/",

                         "R","/","/","/",   "WR01","R1","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",

                         "WR01","R1","/","/","/",   "/","/","/","/",
                         "R1","/","/","/",   "WR13","R","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "R","/","/","/",   "/","/","/","/",

                         "R","/","/","/",   "WR01","R1","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "($0)($2)($11)","/","/","/",   "($0)($2)($11)","/","/","/",
                         "($0)($2)($11)","/","/","/",   "/","/","/","/",

                         "R(D1)","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",
                         "R(D1)","/","/","/",   "/","/","/","/",

                         "R(D1)","/","/","/",   "R(D1)","/","/","/",
                         "R(D1)","/","/","/",   "R(D1)","/","/","/",
                         "speedchange1","$0($2)","/","$01($21)","/",  "$0($2)","/","$01($21)","/",
                         "$0($2)","/","$01($21)","/",  "$0($2)","/","$01($21)","/",

                         "EF","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",

                         "EF1","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                         "/","/","/","/",   "/","/","/","/",
                    };
                    string[] effect = { };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, arrowspeed, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, arrowspeed, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "speedchange1")
                        {
                            arrowspeed = 10.85f;
                        }
                        else if (arrow[i] == "Move1")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "a");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 120, 240, 270, bpm * 32, 0, Color.Cyan), "b");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 240, 240, 270, bpm * 32, 0, Color.Cyan), "c");
                                Extends.LineMoveLibrary.VecLerpAdd("a", bpm * 32, new(-7f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("b", bpm * 32, new(-8, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("c", bpm * 32, new(-9f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("a", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("b", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("c", bpm * 16, 0.8f, 0, bpm * 24, 0);
                            }));
                        }
                        else if (arrow[i] == "EF1")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                ScreenDrawing.CameraEffect.SizeShrink(15, bpm * 16);
                            }));
                        }
                        else if (arrow[i] == "Move2")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "A");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 120, 240, 270, bpm * 32, 0, Color.Cyan), "B");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 240, 240, 270, bpm * 32, 0, Color.Cyan), "C");
                                Extends.LineMoveLibrary.VecLerpAdd("A", bpm * 32, new(7f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("B", bpm * 32, new(8, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("C", bpm * 32, new(9f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("A", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("B", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("C", bpm * 16, 0.8f, 0, bpm * 24, 0);
                            }));
                        }
                        else if (arrow[i] == "Move3")
                        {

                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "aa");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 120, 240, 270, bpm * 32, 0, Color.Cyan), "bb");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 + 240, 240, 270, bpm * 32, 0, Color.Cyan), "cc");
                                Extends.LineMoveLibrary.VecLerpAdd("aa", bpm * 32, new(-7f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("bb", bpm * 32, new(-8, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("cc", bpm * 32, new(-9f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("aa", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("bb", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("cc", bpm * 16, 0.8f, 0, bpm * 24, 0);
                            }));
                            AddInstance(new TimeRangedEvent(beat, 1, () =>
                            {
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320, 240, 270, bpm * 32, 0, Color.Cyan), "aaa");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 120, 240, 270, bpm * 32, 0, Color.Cyan), "bbb");
                                Extends.DrawingUtil.CreateTagLine(new Extends.DrawingUtil.Linerotate(320 - 240, 240, 270, bpm * 32, 0, Color.Cyan), "ccc");
                                Extends.LineMoveLibrary.VecLerpAdd("aaa", bpm * 32, new(7f, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("bbb", bpm * 32, new(8, 0), 0.05f);
                                Extends.LineMoveLibrary.VecLerpAdd("ccc", bpm * 32, new(9f, 0), 0.05f);
                                Extends.LineMoveLibrary.AlphaSin("aaa", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("bbb", bpm * 16, 0.8f, 0, bpm * 24, 0);
                                Extends.LineMoveLibrary.AlphaSin("ccc", bpm * 16, 0.8f, 0, bpm * 24, 0);
                            }));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, arrowspeed, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void Intro5()
                {
                    float beat = bpm * 16;
                    float arrowspeed = 7.6f;
                    string[] arrow =
                    {
                        "R(R1)","/","/","/",  "(R1)","/","/","/",
                        "R","/","+0","/",   "!(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "@(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                         "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "WR01","!(R1)(+01)","/","/","/",   "(R1)","/","/","/",
                        "(R1)","/","@(R)(+0)","/",   "WR11","(+0)(+0)","/","/","/",
                        "R","/","/","/",   "R","/","!(R1)(+01)","/",
                        "R","/","/","/",   "(R1)","/","/","/",

                        "@(R1)(+01)(R)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "!(R1)(+01)(R)","/","/","/",
                        "(R)(+0)","/","(+2)(+0)","/",   "(+2)(+0)","/","(+2)(+0)","/",
                        "(R)(+0)","/","(+2)(+0)","/",   "(+2)(+0)","/","(+2)(+0)","/",
                        //
                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "R","/","+0","/",   "@(R1)(+01)","/","/","/",
                        "R(R1)","/","/","/",   "R","/","+0","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(R0)","/","/","/",   "/","/","/","/",
                        "!($0)($21)","/","~($1)($31)","/",   "@($2)($01)","/","/","/",
                        "/","/","/","/",   "!($0)($21)","/","~($1)($31)","/",
                        "@($2)($01)","/","/","/",   "R(R1)","/","/","/",

                        "~(R1)(R0)","/","/","/",   "!(R1)(R0)","/","/","/",
                        "/","/","!(R1)(R0)","/",   "/","/","/","/",
                        "/","/","/","/",   "!(R1)(+20)","/","/","/",
                        "@(R1)(+20)","/","/","/",   "!(R1)(+20)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                       "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(D)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(D)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(D)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!($0)($2)","/","@($01)($21)","/",   "!($0)($2)","/","@($01)($21)","/",

                        "~($1)($3)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(D)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(D)","/","/","/",   "@(R1)(+2)","/","/","/",
                        "!(R1)(D)","/","/","/",   "@(R1)(+2)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!(R1)(+01)","/","/","/",   "R(R1)","/","R","/",
                        "+0","/","R1","/",   "@(+01)(+01)","/","/","/",
                        "(R1)(R)","/","+0","/",   "R1","/","+01","/",
                        "~(R1)(+01)","/","/","/",   "R(R1)","/","/","/",

                        "!($0)($2)($01)($21)","/","/","/",   "T(R)","T(+21)","T(+2)",
                        "(+2)","/","/","/",   "@($0)($2)($01)($21)","/","/","/",
                        "T(R)","T(+21)","T(+2)",   "(+2)","/","/","/",
                        "T($0)","T(+2)","T(+2)","T(+2)","T(+2)","T(+2)",

                        "(+2)","/","/","/",   "/","/","/","/",
                        "speedchange1","/","/","/","/",   "~(R1)(D)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "~(R1)(D)","/","/","/",   "/","/","/","/",
                        "G008","G118","~($0)($11)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                        }
                        else if (arrow[i][0] == 'Y')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(beat, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 8 - 4));
                            CreateArrow(beat, way, arrowspeed, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i][0] == 'W')
                        {
                            SetPlayerMission(0);
                            int way = Rand(0, 3);
                            CreateGB(new GreenSoulGB(beat, way, arrow[i][2] - '0', (arrow[i][3] - '0') * bpm * 4 - 4));
                            CreateArrow(beat, way, arrowspeed, arrow[i][2] - '0', 0);
                        }
                        else if (arrow[i] == "speedchange3")
                        {
                            arrowspeed = 7.85f;
                        }
                        else if (arrow[i] == "speedchange1")
                        {
                            arrowspeed = 10.85f;
                        }
                        else if (arrow[i][0] == '!')
                        {
                            instance.CreateArrows(beat, arrowspeed * 1.05f, arrow[i].Replace("!", ""), ArrowAttribute.RotateR);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == '@')
                        {
                            instance.CreateArrows(beat, arrowspeed * 1.05f, arrow[i].Replace("@", ""), ArrowAttribute.RotateL);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == '~')
                        {
                            instance.CreateArrows(beat, arrowspeed * 1.2f, arrow[i].Replace("~", ""), ArrowAttribute.SpeedUp);
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'T')
                        {
                            instance.CreateArrows(beat, arrowspeed * 1.5f, arrow[i].Replace("T", ""), ArrowAttribute.SpeedUp);
                            beat += bpm * 0.66666f;
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(beat, arrowspeed, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
            }
            public void Noob()
            {
                if (GametimeF == (int)(bpm * 16))
                {
                    Heart.Shields.BShield.Dispose();
                    Heart.Shields.RShield.Dispose();
                    Player.Heart a = Heart.Split();
                    Player.Heart b = Heart.Split();
                    Player.Heart c = Heart.Split();
                    SetBoxMission(0);
                    InstantSetBox(new Vector2(320, 480 + 40), 640, 84);
                    SetBoxMission(1);
                    InstantSetBox(new Vector2(320, 480 + 40), 640, 84);
                    SetBoxMission(2);
                    InstantSetBox(new Vector2(320, 480 + 40), 640, 84);
                    SetBoxMission(3);
                    InstantSetBox(new Vector2(320, 480 + 40), 640, 84);
                    SetPlayerMission(0);
                    InstantTP(128, 508);
                    Heart.GiveForce(0, 1);
                    SetPlayerMission(1);
                    InstantTP(256, 508);
                    Heart.GiveForce(90, 1);
                    SetPlayerMission(2);
                    InstantTP(128 * 3, 508);
                    Heart.GiveForce(180, 1);
                    SetPlayerMission(3);
                    InstantTP(128 * 4, 508);
                    Heart.GiveForce(270, 1);
                }
            }
            public void ExtremePlus()
            {

                Heart.InstantSetRotation(heartrot);
                if (GametimeF == 1)
                {
                    ExPlus.Intro0();
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
                    box = new(114514, 0, Heart);
                    heartrot = 0;
                    CreateEntity(box);
                }

                if (InBeat(16 * 4 - 16)) ExPlus.Intro1();
                if (InBeat(16 * 32 - 16)) ExPlus.Intro2();
                if (InBeat(16 * 48 - 16)) ExPlus.Intro3();
                if (InBeat(16 * 66 - 16)) ExPlus.Intro4();
                if (InBeat(16 * 84 - 16))
                {
                    ExPlus.Intro5();
                }
                /* if (InBeat(16 * 82))
                 {
                     float beat = 0;
                     string[] str = 
                         {
                         "Stop1","/","/","/","/",   "/","/","Go1","/","/",
                         "Stop1","/","/","/","/",   "/","/","Go1","/","/",
                         "Stop2","/","/","Go1","/","/",   "Stop2","/","/","Go1","/","/",
                         "Stop3","/","/","/","/",   "/","/","Go2","/",


                     };
                     Arrow[] arrows = GetAll<Arrow>();
                     for (int i = 0; i < str.Length; i++)
                     {
                         if (str[i] == "/") beat += 0.5f*bpm;
                         if (str[i] == "Stop1")
                         {
                             AddInstance(new InstantEvent(beat, () =>
                             {
                                 for (int a = 0; a < arrows.Length; a++)
                                 {
                                     int x = a;
                                     arrows[x].Stop(bpm * 3f);
                                     arrows[x].Speed = 24f;
                                 }
                             }));

                         }
                         if (str[i] == "Stop2")
                         {
                             AddInstance(new InstantEvent(beat, () =>
                             {
                                 for (int a = 0; a < arrows.Length; a++)
                             {
                                 int x = a;
                                 arrows[x].Stop(bpm * 1f);

                             }
                             }));
                         }
                         if (str[i] == "Stop3")
                         {
                                 AddInstance(new InstantEvent(beat, () =>
                                 {
                                     for (int a = 0; a < arrows.Length; a++)
                             {
                                 int x = a;
                                 arrows[x].Stop(bpm * 4f);

                             }
                                 }));
                         }
                         if (str[i] == "Go1")
                         {
                                     AddInstance(new InstantEvent(beat, () =>
                                     {
                                         for (int a = 0; a < arrows.Length; a++)
                             {
                                 int x = a;
                                 arrows[x].Delay(bpm);

                             }
                                     }));
                         }
                         if (str[i] == "Go2")
                         {
                                         AddInstance(new InstantEvent(beat, () =>
                                         {
                                             for (int a = 0; a < arrows.Length; a++)
                             {
                                 int x = a;
                                 arrows[x].Delay(bpm*18-4);
                                                 arrows[x].Speed = 8f;
                             }
                                         }));
                         }
                     }
                 };*/
            }
            float bpm2 = 62.5f / (240f / 60);
            public void Normal()
            {
                if (InBeat(1))
                {
                    for (int a = 0; a < 100; a++)
                    {
                        CreateArrow(bpm2 * a * 0.5f + 114, "$0", 7, 0, 0);
                        CreateArrow(bpm2 * a * 0.5f + 114 + bpm2 * 0.25f, "$0", 7, 1, 0);
                    }
                }
                Arrow[] ars = GetAll<Arrow>();
                for (int a = 0; a < ars.Length; a++)
                {
                    int x = a;
                    ars[x].JudgeType = Arrow.JudgementType.Tap;
                }
            }
            public void Extreme()
            {

                Heart.InstantSetRotation(heartrot);
                if (GametimeF == 1)
                {
                    Remake.Intro0();
                    box = new(114514, 0, Heart);
                    heartrot = 0;
                    CreateEntity(box);
                }

                if (InBeat(16 * 4 - 16)) Remake.Intro1();
                if (InBeat(16 * 32 - 16)) Remake.Intro2();
                if (InBeat(16 * 48 - 16)) Remake.Intro3();
                if (InBeat(16 * 66 - 16)) Remake.Intro4();
                if (InBeat(16 * 84 - 16))
                {
                    Remake.Intro5();
                }
            }
            public void Hard()
            {
                Effects();
                if (GametimeF == 1)
                {
                    box = new(114514, 0, Heart);
                    heartrot = 0;
                    CreateEntity(box);
                }
                if (GametimeF == 1) ExtremeBarrage.Intro0t3();
                if (GametimeF == (int)(bpm * 16 * 4 - 80))
                {
                    CreateArrow(80, "R", 13, 0, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80, "+0", 13, 1, 0, ArrowAttribute.SpeedUp);
                    ExtremeBarrage.Intro4t7();
                }
                if (GametimeF == (int)(bpm * 16 * 8 - 80)) ExtremeBarrage.Intro8t12();
                if (GametimeF == (int)(bpm * 16 * 14)) ExtremeBarrage.Intro16();
                if (GametimeF == (int)(bpm * 16 * 17 - 80)) ExtremeBarrage.Intro17();
                if (GametimeF == (int)(bpm * 16 * 24 - 80)) ExtremeBarrage.Intro24();
                if (GametimeF == (int)(bpm * 16 * 31 + bpm * 8 - 80))
                {
                    CreateArrow(80, "R", 10, 0, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80, "+0", 10, 1, 0, ArrowAttribute.SpeedUp);
                }
                if (GametimeF == (int)(bpm * 16 * 31 + bpm * 12 - 80))
                {
                    CreateArrow(80, "R", 10, 0, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80, "+0", 10, 1, 0, ArrowAttribute.SpeedUp);
                }
                if (GametimeF == (int)(bpm * 16 * 32 - bpm * 16))
                {
                    ExtremeBarrage.Intro31();
                }
                if (GametimeF == (int)(bpm * 16 * 35 - 80)) ExtremeBarrage.Intro35();
                if (GametimeF == (int)(bpm * 16 * 36 - bpm * 16)) ExtremeBarrage.Intro36();
                if (GametimeF == (int)(bpm * 16 * 43 - 80)) ExtremeBarrage.Intro35();
                if (GametimeF == (int)(bpm * 16 * 44 - bpm * 16)) ExtremeBarrage.Intro44();
                if (GametimeF == (int)(bpm * 16 * 46 - 80)) ExtremeBarrage.Intro46();
                if (GametimeF == (int)(bpm * 16 * 48 - 80)) ExtremeBarrage.Intro47();
                if (GametimeF == (int)(bpm * 16 * 52 - 80)) ExtremeBarrage.Intro52();
                if (GametimeF == (int)(bpm * 16 * 58)) ExtremeBarrage.Intro53();
                if (GametimeF == (int)(bpm * 16 * 58 - 80)) ExtremeBarrage.Intro53A();
                if (GametimeF == (int)(bpm * 16 * 66)) ExtremeBarrage.Intro66();
                if (GametimeF == (int)(bpm * 16 * 72)) ExtremeBarrage.Intro72();
                if (GametimeF == (int)(bpm * 16 * 76 - 80)) ExtremeBarrage.Intro76();
                if (GametimeF == (int)(bpm * 16 * 80 - 80)) ExtremeBarrage.Intro80();
                if (GametimeF == (int)(bpm * 16 * 82)) ExtremeBarrage.Intro82();
                for (int a = 0; a < 14; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 85 + bpm * 16 * a - 80))
                    {
                        ExtremeBarrage.Intro85();
                    }
                    if (GametimeF == (int)(bpm * 16 * 85 + bpm * 16 * a))
                    {
                        mainEffects.midshock();
                    }
                }
                if (GametimeF == (int)(bpm * 16 * 99 - 80)) ExtremeBarrage.Intro100();
                if (GametimeF == (int)(bpm * 16 * 100))
                {
                    float sin = 0;
                    float cos = 0;
                    float speed = 330;
                    Shader shader = new(Loader.Load<Effect>("Musics\\Grievous Lady\\Effects"));
                    AddInstance(new TimeRangedEvent(0, bpm * 21 * 16, () =>
                        {
                            shader.Parameters["distance"].SetValue(new Vector2(sin, cos));
                            sin = Sin(speed) * 0.005f;
                            cos = Sin(speed * 0.5f) * 0.005f;
                            speed += 1.7f;
                        }));
                }
                for (int a = 0; a < 8; a++)
                {

                    if (GametimeF == (int)(bpm * 16 * 100 - 80 + bpm * a * 16))
                    {
                        ExtremeBarrage.Intro101();
                    }
                    if (GametimeF == (int)(bpm * 16 * 100 + bpm * a * 16))
                    {
                        mainEffects.midshock();
                    }
                }
                for (int a = 0; a < 6; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 108 - 80 + bpm * a * 16))
                    {
                        ExtremeBarrage.Intro109();
                    }
                    if (GametimeF == (int)(bpm * 16 * 108 + bpm * a * 16))
                    {
                        mainEffects.midshock();
                    }
                }
                if (GametimeF == (int)(bpm * 16 * 114 - 80)) ExtremeBarrage.Intro115();
                if (GametimeF == (int)(bpm * 16 * 115 - 80)) ExtremeBarrage.Intro116();
            }
            public void Easy()
            {


            }

        }
    }
}

