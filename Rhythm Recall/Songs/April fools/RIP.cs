using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class RIP : IChampionShip
    {
        public RIP()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new();
            difficulties.Add("Rest In Peace  Lv.19.7", Difficulty.ExtremePlus);
            difficulties.Add("Rest In Peace  Lv.14.0", Difficulty.Hard);
            difficulties.Add("Rest In Peace  Lv.5.8", Difficulty.Easy);
        }
        public static Color BarColor = new(139, 0, 0, 255);
        public static Color LoseColor = Color.Gray;
        public static float HeartRot = 0;
        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();
        public static class mainEffects
        {
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
            public class MaskSquare : Entity
            {
                public int duration = 0;
                public float LeftUpX = 0;
                public float LeftUpY = 0;
                public float width = 0;
                public float height = 0;
                public Color color = Color.White;
                public MaskSquare(float LeftUpX, float LeftUpY, float width, float height, int duration, Color color, float alpha)
                {
                    this.LeftUpX = LeftUpX;
                    this.LeftUpY = LeftUpY;
                    this.width = width;
                    this.height = height;
                    this.duration = duration;
                    this.color = color;
                    this.alpha = alpha;
                }
                public float alpha = 1;
                public int time = 0;
                public float speed = 1;
                public override void Draw()
                {
                    FormalDraw(Sprites.pixUnit, new CollideRect(LeftUpX, LeftUpY, width, height).ToRectangle(), color * alpha);
                    Depth = 0.98f;
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
            public static void ScreenAngleNeo(float angle, float time)
            {
                float start = ScreenDrawing.ScreenAngle;
                float end = angle;
                float del = start - end;
                float t = 0;
                AddInstance(new TimeRangedEvent(0, time, () =>
                {
                    float x = t / (time - 1);
                    float f = 2 * x - x * x;
                    ScreenDrawing.ScreenAngle = start - del * f;
                    t++;
                }));
            }
            public static void PlusRotate(float intensity, float time)
            {
                float start = ScreenDrawing.ScreenAngle;
                float end = start + intensity;
                float del = start - end;
                float t = 0;
                AddInstance(new TimeRangedEvent(0, time / 2f, () =>
                {
                    float x = t / (time / 2f - 1);
                    float f = 2 * x - x * x;
                    ScreenDrawing.ScreenAngle = start - del * f;
                    t++;
                }));
            }
            public static void BGColor()
            {
                if (Rand(1, 12) == 1)
                {
                    AddInstance(new TimeRangedEvent(8, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, Color.Black, 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 2)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(150, 10, 20), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 3)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(160, 20, 10), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 4)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(76, 25, 29), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 5)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(150, 28, 27), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 6)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(10, 10, 48), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 7)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, Color.DarkRed, 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 8)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(65, 0, 0), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 9)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(10, 10, 10), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 10)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(125, 10, 10), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 11)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(30, 30, 30), 0.1f);
                    }));
                }
                else if (Rand(1, 12) == 12)
                {
                    AddInstance(new TimeRangedEvent(4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(220, 5, 30), 0.1f);
                    }));
                }
            }
            public static void ChangebarColor()
            {
                AddInstance(new TimeRangedEvent(13, () =>
                {
                    BarColor = Color.Lerp(BarColor, new Color(255, 0, 0, 255), 0.2f);
                    LoseColor = Color.Lerp(LoseColor, Color.Gray, 0.2f);
                    ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, Color.White, 0.2f);
                }));
                AddInstance(new TimeRangedEvent(14, 30, () =>
                {
                    BarColor = Color.Lerp(BarColor, new Color(80, 0, 0, 255), 0.07f);
                    LoseColor = Color.Lerp(LoseColor, Color.Gray * 0.3f, 0.07f);
                    ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, Color.White * 0.3f, 0.07f);
                }));
            }
            public static void ChangebarColor2()
            {
                AddInstance(new TimeRangedEvent(300, () =>
                {
                    BarColor = Color.Lerp(BarColor, new Color(0, 0, 0, 0), 0.0015f);
                    LoseColor = Color.Lerp(LoseColor, new Color(0, 0, 0, 0), 0.0015f);
                    ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, new Color(0, 0, 0, 0), 0.0015f);
                    ScreenDrawing.HPBar.AreaOccupied = new CollideRect(240 * 0.04f + ScreenDrawing.HPBar.AreaOccupied.X * 0.96f, 140, 20, 180);
                }));
            }
            public static void HPlerp()
            {
                float lerp = Rand(3.1f * 9 / 8, 4.2f);
                float time = 1;
                AddInstance(new TimeRangedEvent(160, () =>
                {
                    HeartAttribute.HP -= (int)(lerp * lerp * time);
                    time -= 1f / 160f;
                }));

            }
            public static Color BeautifulColor()
            {
                Color color = Color.White;
                if (Rand(0, 5) == 0) color = new(188, 255, 255);
                else if (LastRand == 1) color = new(115, 136, 217);
                else if (LastRand == 2) color = new(115, 106, 221);
                else if (LastRand == 3) color = new(255, 255, 255);
                else if (LastRand == 4) color = new(185, 162, 199);
                else if (LastRand == 5) color = new(141, 255, 231);
                return color;
            }
        }
        static class Tips
        {
            public static string Tip01()
            {
                return "Hello, This is a Tip...";
            }
            public static string Tip02()
            {
                return "Welcome to Rhythm Recall";
            }
            public static string Tip03()
            {
                return "Did you pass the song?";
            }
            public static string Tip04()
            {
                return "R.I.P.";
            }
            public static string Tip05()
            {
                return "Never Gonna Let You Down--";
            }
            public static string Tip06()
            {
                return "Will anyone read this?";
            }
            public static string Tip07()
            {
                return "[UndyneFight-Ex] Engine by T-mas";
            }
            public static string Tip08()
            {
                return "Rest In Peace";
            }
            public static string Tip09()
            {
                return "Never Gonna Give You Up--";
            }
            public static string Tip10()
            {
                return "This is a very longggggggggggggg\nggggggggggggggggggggggggggggggggggg\nggggggggggggg Tip!!";
            }
            public static string Tip11()
            {
                return "Oh, one of the charters is a mem\nber of karatoin the past...";
            }
            public static string Tip12()
            {
                return "I need hands that can draw verrr\nryyyy wellllll!";
            }
            public static string Tip13()
            {
                return "I dreamt that I can compose...\nBut the reality is...";
            }
            public static string Tip14()
            {
                return "I am fool";
            }
            public static string Tip15()
            {
                return "Let's BaiLan! Ah yeah yeah yeah!";
            }
            public static string Tip16()
            {
                return "WJdqfahkufgakfuasfkawfawfasjfkau\nefhkasrkghaskdfghakurkghg";
            }
            public static string Tip17()
            {
                return "How long will it take you to see\nthis Tip again?";
            }
            public static string Tip18()
            {
                return "The song is unauthorized, and if\nthat infringe a copyright, call T-mas\nby email:480642510@qq.com, we will \ndelete this song immediately";
            }
        }
        public class aaaa : RenderingManager
        {

        }
        public class Game : WaveConstructor, IWaveSet
        {

            private class ThisInformation : SongInformation
            {
                public override string BarrageAuthor
                {
                    get
                    {
                        return "Tlottgodinf & zKronO";
                    }
                }
                public override string SongAuthor
                {
                    get
                    {
                        return "The Great Eicateve(for his friend)";
                    }
                }
                public override string PaintAuthor
                {
                    get
                    {
                        return "yukkering";
                    }
                }
                public override string AttributeAuthor => "Tlottgodinf & zKronO";
                public int TipsRoute = Rand(0, 18);
                public override string Extra
                {
                    get
                    {
                        /*if (TipsRoute == 1) return Tips.Tip01();
                        else if (TipsRoute == 2) return Tips.Tip02();
                        else if (TipsRoute == 3) return Tips.Tip03();
                        else if (TipsRoute == 4) return Tips.Tip04();
                        else if (TipsRoute == 5) return Tips.Tip05();
                        else if (TipsRoute == 6) return Tips.Tip06();
                        else if (TipsRoute == 7) return Tips.Tip07();
                        else if (TipsRoute == 8) return Tips.Tip08();
                        else if (TipsRoute == 9) return Tips.Tip09();
                        else if (TipsRoute == 10) return Tips.Tip10();
                        else if (TipsRoute == 11) return Tips.Tip11();
                        else if (TipsRoute == 12) return Tips.Tip12();
                        else if (TipsRoute == 13) return Tips.Tip13();
                        else if (TipsRoute == 14) return Tips.Tip14();
                        else if (TipsRoute == 15) return Tips.Tip15();
                        else if (TipsRoute == 16) return Tips.Tip16();
                        else if (TipsRoute == 17) return Tips.Tip17();
                        else return Tips.Tip18();*/
                        return "Warning of Photosensitive Epilepsy\nThis game contains fast flashing graphics." +
                            "\nIf you experience any discomfort or symptoms during the game," +
                            "\nplease stop playing immediately and consult your \ndoctor before continuing";
                    }
                }
                public override Color ExtraColor
                {
                    get
                    {
                        return Color.Red;
                    }
                }

                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 19.7f),
                            new(Difficulty.Hard, 14.0f),
                            new(Difficulty.Easy, 5.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 19.8f),
                            new(Difficulty.Hard, 14.1f),
                            new(Difficulty.Easy, 5.9f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 24.2f),
                            new(Difficulty.Hard, 19.8f),
                            new(Difficulty.Easy, 14f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(62.5f / (192 / 60f)) { }
            public static float bpm = 4.8828125f;

            public static Game instance;

            public string Music => "RIP";

            public string FightName => "R.I.P.";
            public float SongDuration => 144 * 62.5f; //填写歌曲的时长（毫秒）
            static Shader muhai;

            public static void Effect()
            {
                if (GametimeF > 28 * 62.5f + 50 && GametimeF < 30 * 62.5f + 10)
                {
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(muhai));
                    AddInstance(new TimeRangedEvent(0f, 1f, delegate ()
                    {
                        muhai.Parameters["distance1"].SetValue(new Vector2(Rand(-0.35f, -0.05f), Rand(-0.15f, 0.15f)));
                        muhai.Parameters["distance2"].SetValue(new Vector2(Rand(0.05f, 0.35f), Rand(-0.15f, 0.15f)));
                    }));
                    AddInstance(new TimeRangedEvent(85f, 1f, delegate ()
                    {
                        muhai.Parameters["distance1"].SetValue(new Vector2(0f, 0f));
                        muhai.Parameters["distance2"].SetValue(new Vector2(0f, 0f));
                    }));
                }
                if (GametimeF > 39 * 62.5f && GametimeF < 40 * 62.5f)
                {
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(muhai));
                    AddInstance(new TimeRangedEvent(0f, 1f, delegate ()
                    {
                        muhai.Parameters["distance1"].SetValue(new Vector2(Rand(-0.35f, -0.05f), Rand(-0.15f, 0.15f)));
                        muhai.Parameters["distance2"].SetValue(new Vector2(Rand(0.05f, 0.35f), Rand(-0.15f, 0.15f)));
                    }));
                    AddInstance(new TimeRangedEvent(85f, 1f, delegate ()
                    {
                        muhai.Parameters["distance1"].SetValue(new Vector2(0f, 0f));
                        muhai.Parameters["distance2"].SetValue(new Vector2(0f, 0f));
                    }));
                    ScreenDrawing.ScreenPositionDetla = Rand(0, 1) == 0 ? new(-7, 0) : new(7, 0);
                }
                if (GametimeF > 48 * 62.5f + 50 && GametimeF < 50 * 62.5f + 10)
                {
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(muhai));
                    AddInstance(new TimeRangedEvent(0f, 1f, delegate ()
                    {
                        muhai.Parameters["distance2"].SetValue(new Vector2(Rand(-0.35f, 0.15f), Rand(-0.25f, 0.25f)));
                        muhai.Parameters["distance3"].SetValue(new Vector2(Rand(-0.15f, 0.35f), Rand(-0.25f, 0.25f)));
                    }));
                    AddInstance(new TimeRangedEvent(85f, 1f, delegate ()
                    {
                        muhai.Parameters["distance2"].SetValue(new Vector2(0f, 0f));
                        muhai.Parameters["distance3"].SetValue(new Vector2(0f, 0f));
                    }));
                }
                if (GametimeF > 53 * 62.5f + 50 && GametimeF < 55 * 62.5f + 10)
                {
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(muhai));
                    AddInstance(new TimeRangedEvent(0f, 1f, delegate ()
                    {
                        muhai.Parameters["distance2"].SetValue(new Vector2(Rand(-0.35f, 0.15f), Rand(-0.25f, 0.25f)));
                        muhai.Parameters["distance3"].SetValue(new Vector2(Rand(-0.15f, 0.35f), Rand(-0.25f, 0.25f)));
                    }));
                    AddInstance(new TimeRangedEvent(85f, 1f, delegate ()
                    {
                        muhai.Parameters["distance2"].SetValue(new Vector2(0f, 0f));
                        muhai.Parameters["distance3"].SetValue(new Vector2(0f, 0f));
                    }));
                }
                if (GametimeF > 43 * 62.5f + 50 && GametimeF < 45 * 62.5f + 10)
                {
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(muhai));
                    AddInstance(new TimeRangedEvent(0f, 1f, delegate ()
                    {
                        muhai.Parameters["distance2"].SetValue(new Vector2(Rand(-0.35f, 0.15f), Rand(-0.25f, 0.25f)));
                        muhai.Parameters["distance1"].SetValue(new Vector2(Rand(-0.15f, 0.35f), Rand(-0.25f, 0.25f)));
                    }));
                    AddInstance(new TimeRangedEvent(85f, 1f, delegate ()
                    {
                        muhai.Parameters["distance2"].SetValue(new Vector2(0f, 0f));
                        muhai.Parameters["distance1"].SetValue(new Vector2(0f, 0f));
                    }));
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.1f;
                    ScreenDrawing.ScreenAngle *= 0.9f;
                }
                if (GametimeF == 5)
                {
                    SetBoxMission(0);
                    InstantSetBox(new Vector2(320, 240), 84, 84);
                    InstantTP(320, 240);
                }
                if (GametimeF == 1)
                {
                    mainEffects.MaskSquare m = new(320 - 50, 240 - 50, 100, 100, (int)(8 * 16 * bpm), Color.Black, 1);
                    CreateEntity(m);
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 25, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 24, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 23, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 22, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 21, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 20, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 19, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 18, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 17, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 16, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 15, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 14, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 13, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 12, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 11, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 10, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 9, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 8, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 7, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 6, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 5, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 4, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 3, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f * 2, 1, () =>
                    {
                        m.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) - 2.5f, 1, () =>
                    {
                        m.alpha = 0;
                    }));
                    mainEffects.DrawLine.NormalLine l1 = new(320, 0, 320, 480, (int)(8 * 16 * bpm), 0.25f, Color.DarkRed, 0.99f);
                    mainEffects.DrawLine.NormalLine l2 = new(320, 0, 320, 480, (int)(8 * 16 * bpm), 0.25f, Color.DarkRed, 0.99f);
                    mainEffects.DrawLine.NormalLine l3 = new(320, 0, 320, 480, (int)(8 * 16 * bpm), 0.25f, Color.DarkRed, 0.99f);
                    mainEffects.DrawLine.NormalLine l4 = new(320, 0, 320, 480, (int)(8 * 16 * bpm), 0.25f, Color.DarkRed, 0.99f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    CreateEntity(l3);
                    CreateEntity(l4);
                    float t = 0;
                    AddInstance(new TimeRangedEvent(0, (int)(8 * 16 * bpm), () =>
                    {
                        l1.x1 = Sin(t * -0.45f) * (int)(4 * 16 * bpm * 0.45f) + 320;
                        l2.x1 = Sin(t * -0.75f) * (int)(4 * 16 * bpm * 0.75f) + 320;
                        l3.x1 = Sin(t * 0.45f) * (int)(4 * 16 * bpm * 0.45f) + 320;
                        l4.x1 = Sin(t * 0.75f) * (int)(4 * 16 * bpm * 0.75f) + 320;
                        l1.x2 = Sin(t * -0.45f) * (int)(4 * 16 * bpm * 0.45f) + 320;
                        l2.x2 = Sin(t * -0.75f) * (int)(4 * 16 * bpm * 0.75f) + 320;
                        l3.x2 = Sin(t * 0.45f) * (int)(4 * 16 * bpm * 0.45f) + 320;
                        l4.x2 = Sin(t * 0.75f) * (int)(4 * 16 * bpm * 0.75f) + 320;
                        t += 1;
                    }));
                    AddInstance(new TimeRangedEvent(0, (int)(7 * 16 * bpm), () =>
                    {
                        l1.alpha = Sin(t * (int)(0.05f * bpm)) * (int)bpm * 0.15f + 0.5f;
                        l2.alpha = Sin(t * (int)(0.05f * bpm)) * (int)bpm * 0.15f + 0.5f;
                        l3.alpha = Sin(t * (int)(0.05f * bpm)) * (int)bpm * 0.15f + 0.5f;
                        l4.alpha = Sin(t * (int)(0.05f * bpm)) * (int)bpm * 0.15f + 0.5f;
                    })); AddInstance(new TimeRangedEvent((int)(7 * 16 * bpm), (int)(16 * bpm), () =>
                    {
                        l1.alpha -= 0.02f;
                        l2.alpha -= 0.02f;
                        l3.alpha -= 0.02f;
                        l4.alpha -= 0.02f;
                    }));
                }//开头
                for (int i = 0; i < 6; i++)
                {
                    if (GametimeF == (int)(8 * bpm * 16) + i * (int)(16 * bpm))
                    {
                        mainEffects.DrawLine.Linerotate l = new(320, 620, 75, 120, 0.5f, Color.White, 0.1f);
                        CreateEntity(l);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, 120, () =>
                        {
                            l.yCenter -= 10;
                            l.alpha = Sin(t * 0.05f) * 0.25f + 0.5f;
                            t += 1;
                        }));
                    }
                    if (GametimeF == (int)(8 * bpm * 16 + bpm * 8) + i * (int)(16 * bpm))
                    {
                        mainEffects.DrawLine.Linerotate l = new(320, 620, -75, 120, 0.5f, Color.White, 0.1f);
                        CreateEntity(l);
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, 120, () =>
                        {
                            l.yCenter -= 10;
                            l.alpha = Sin(t * 0.05f) * 0.25f + 0.5f;
                            t += 1;
                        }));
                    }
                }
                if (GametimeF == (int)(16 * 16 * bpm - 4 * bpm))
                {
                    ScreenDrawing.WhiteOut((int)(4 * bpm));
                }
                if (GametimeF > 20 * 62.5f && GametimeF < 60 * 62.5f && GametimeF % 5f == 1f)
                {
                    mainEffects.BGColor();
                }
                if (GametimeF == 0)
                {
                    mainEffects.DrawLine.NormalLine l = new(0, 240, 640, 240, 300, 0.75f, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(0, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(24 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(24 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(28 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(28 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                }
                if (GametimeF == (int)(2 * 16 * bpm))
                {
                    mainEffects.DrawLine.NormalLine l = new(0, 120, 640, 120, 300, 0.75f, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(0, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(24 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(24 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(28 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(28 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                }
                if (GametimeF == (int)(4 * 16 * bpm))
                {
                    mainEffects.DrawLine.NormalLine l = new(0, 360, 640, 360, 300, 0.75f, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(0, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(24 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(24 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(28 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(28 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                }
                if (GametimeF == (int)(6 * 16 * bpm))
                {
                    mainEffects.DrawLine.NormalLine l = new(0, 280, 640, 280, 300, 0.75f, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(0, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(24 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(24 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(28 * bpm), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(28 * bpm) + 1, 15, () =>
                    {
                        l.alpha -= 0.05f;
                    }));
                }
                if (GametimeF == (int)(12 * 16 * bpm))
                {
                    mainEffects.DrawLine.Linerotate l1 = new(320, 240, 0, 300, 0.1f, Color.White, 0.99f);
                    mainEffects.DrawLine.Linerotate l2 = new(320, 240, 0, 300, 0.1f, Color.White, 0.99f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    float i = 0;
                    AddInstance(new TimeRangedEvent(0, 300, () =>
                    {
                        l1.xCenter -= i * i * 0.001f;
                        l2.xCenter += i * i * 0.001f;
                        l1.alpha += 0.005f;
                        l2.alpha += 0.005f;
                        i += 1;
                    }));
                }
                if (GametimeF == (int)(14 * 16 * bpm))
                {
                    mainEffects.DrawLine.Linerotate l1 = new(0, 240, 0, 300, 0, Color.White, 0.99f);
                    mainEffects.DrawLine.Linerotate l2 = new(640, 240, 0, 300, 0, Color.White, 0.99f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    AddInstance(new TimeRangedEvent(0, 300, () =>
                    {
                        l2.xCenter -= 640 / ((int)(16 * bpm));
                        l1.xCenter += 640 / ((int)(16 * bpm));
                    }));
                    AddInstance(new TimeRangedEvent(0, 1, () =>
                    {
                        l1.alpha = 1;
                        l2.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent(1, 1, () =>
                    {
                        l1.alpha = 0;
                        l2.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 1 * bpm), 1, () =>
                    {
                        l1.alpha = 1;
                        l2.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 1 * bpm) + 1, 1, () =>
                    {
                        l1.alpha = 0;
                        l2.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 2 * bpm), 1, () =>
                    {
                        l1.alpha = 1;
                        l2.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 2 * bpm) + 1, 1, () =>
                    {
                        l1.alpha = 0;
                        l2.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 3 * bpm), 1, () =>
                    {
                        l1.alpha = 1;
                        l2.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 3 * bpm) + 1, 1, () =>
                    {
                        l1.alpha = 0;
                        l2.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 4 * bpm), 1, () =>
                    {
                        l1.alpha = 1;
                        l2.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 4 * bpm) + 1, 1, () =>
                    {
                        l1.alpha = 0;
                        l2.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 5 * bpm), 1, () =>
                    {
                        l1.alpha = 1;
                        l2.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 5 * bpm) + 1, 1, () =>
                    {
                        l1.alpha = 0;
                        l2.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 6 * bpm), 1, () =>
                    {
                        l1.alpha = 1;
                        l2.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 6 * bpm) + 1, 1, () =>
                    {
                        l1.alpha = 0;
                        l2.alpha = 0;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 7 * bpm), 1, () =>
                    {
                        l1.alpha = 1;
                        l2.alpha = 1;
                    }));
                    AddInstance(new TimeRangedEvent((int)(2 * 7 * bpm) + 1, 1, () =>
                    {
                        l1.alpha = 0;
                        l2.alpha = 0;
                    }));
                }
                for (int i = 0; i < 6; i++)
                {
                    if (Gametime == (int)(23 * 16 * bpm) + i * (int)(2 * bpm))
                    {
                        mainEffects.DrawLine.NormalLine l1 = new(0, Rand(240 - 160, 240 + 160), 640, LastRand, (int)(2 * bpm), 0.75f, Color.White, 0.99f);
                        CreateEntity(l1);
                        mainEffects.DrawLine.NormalLine l2 = new(0, Rand(240 - 160, 240 + 160), 640, LastRand, (int)(2 * bpm), 0.75f, Color.White, 0.99f);
                        CreateEntity(l2);
                        mainEffects.DrawLine.NormalLine l3 = new(Rand(320 - 240, 240 + 320), 0, LastRand, 480, (int)(2 * bpm), 0.75f, Color.White, 0.99f);
                        CreateEntity(l3);
                        mainEffects.DrawLine.NormalLine l4 = new(Rand(320 - 240, 240 + 320), 0, LastRand, 480, (int)(2 * bpm), 0.75f, Color.White, 0.99f);
                        CreateEntity(l4);
                        AddInstance(new TimeRangedEvent(0, (int)(2 * bpm), () =>
                        {
                            l1.alpha -= 0.075f;
                            l2.alpha -= 0.075f;
                            l3.alpha -= 0.075f;
                            l4.alpha -= 0.075f;
                        }));
                    }
                }
                if (GametimeF == (int)(23 * 16 * bpm + 8 * bpm))
                {
                    mainEffects.DrawLine.Linerotate l1_1 = new(320, 240, 0, 600, 0.75f, Color.White, 0.99f);
                    CreateEntity(l1_1);
                    mainEffects.DrawLine.Linerotate l1 = new(320, 240, 0, 600, 0.75f, Color.White, 0.99f);
                    CreateEntity(l1);
                    mainEffects.DrawLine.NormalLine l2 = new(0, 240, 640, 240, 600, 0, Color.White, 0.99f);
                    CreateEntity(l2);
                    mainEffects.DrawLine.Linerotate l3 = new(320, 240, 45, 600, 0, Color.White, 0.99f);
                    CreateEntity(l3);
                    mainEffects.DrawLine.Linerotate l4 = new(320, 240, 135, 600, 0, Color.White, 0.99f);
                    CreateEntity(l4);
                    AddInstance(new TimeRangedEvent((int)(2 * bpm), 1, () =>
                    {
                        l2.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(4 * bpm), 1, () =>
                    {
                        l3.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(6 * bpm), 1, () =>
                    {
                        l4.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * bpm), 120, () =>
                    {
                        l1.alpha -= 0.025f;
                        l1_1.alpha -= 0.025f;
                        l2.alpha -= 0.025f;
                        l3.alpha -= 0.025f;
                        l4.alpha -= 0.025f;
                        l1.xCenter -= 16;
                        l1_1.xCenter += 16;
                        l2.y1 -= 16;
                        l2.y2 -= 16;
                        l3.yCenter -= 16;
                        l4.yCenter -= 16;
                    }));
                }
                if (GametimeF > (int)(32 * 16 * bpm) && GametimeF < (int)(32 * 16 * bpm + bpm * 6))
                {
                    ScreenDrawing.ScreenAngle = Rand(0, 1) == 0 ? 0 : 180;
                }
                if (GametimeF > (int)(33 * 16 * bpm + 8 * bpm) && GametimeF < (int)(35 * 16 * bpm))
                {
                    InstantSetBox(380 - (GametimeF - (int)(33 * 16 * bpm + 8 * bpm)) * 2.25f, 84, 84);
                    TP(BoxStates.Centre.X, BoxStates.Centre.Y);
                    ScreenDrawing.ScreenPositionDetla = new(Heart.Centre.X - 320, Heart.Centre.Y - 240);
                }
                if (GametimeF == (int)(33 * 16 * bpm + 8 * bpm))
                {
                    mainEffects.MaskSquare m = new(-640, -480, 640 * 3, 480 * 3, 600, Color.Black, 1);
                    CreateEntity(m);
                    AddInstance(new TimeRangedEvent((int)(4 * bpm), (int)(20 * bpm), () =>
                    {
                        ScreenDrawing.ScreenAngle -= 0.8f;
                        ScreenDrawing.ScreenScale -= 0.0025f;
                    }));
                    for (int i = 0; i < 13; i++)
                    {
                        AddInstance(new TimeRangedEvent(i * 2, 1, () =>
                        {
                            m.alpha = 1;
                        }));
                        AddInstance(new TimeRangedEvent(i * 2 + 1, 1, () =>
                        {
                            m.alpha = 0;
                        }));
                    }
                    AddInstance(new TimeRangedEvent(0, (int)(4 * bpm), () =>
                    {
                        ScreenDrawing.ScreenScale += 0.005f;
                    }));
                }
                if (GametimeF > 43 * 62.5f + 50 && GametimeF < 44 * 62.5f + 30)
                {
                    ScreenDrawing.ScreenPositionDetla = ScreenDrawing.ScreenPositionDetla * 0.9f + new Vector2(0, ScreenDrawing.ScreenPositionDetla.Y / 2) * 0.1f;
                }
                if (GametimeF > 44 * 62.5f + 30 && GametimeF < 45 * 62.5f + 10)
                {
                    ScreenDrawing.ScreenPositionDetla = ScreenDrawing.ScreenPositionDetla * 0.9f + new Vector2(0, 0) * 0.1f;
                }
                if (GametimeF == 44 * 62.5f + 30)
                {
                    SetBoxMission(0);
                    SetBox((240 - BoxStates.Centre.Y) / 2 + BoxStates.Centre.Y, 84, 84);
                    TP();
                }
                if (GametimeF == 43 * 62.5f + 50)
                {
                    SetBoxMission(0);
                    SetGreenBox();
                    TP();
                }
                if (GametimeF == (int)(37 * 16 * bpm + 12 * bpm))
                {
                    mainEffects.DrawLine.Linerotate l1 = new(0, 240, 0, 300, 0, Color.White, 0.99f);
                    CreateEntity(l1);
                    AddInstance(new TimeRangedEvent(0, 300, () =>
                    {
                        l1.xCenter += 15;
                    }));
                    for (int i = 0; i < 10; i++)
                    {
                        AddInstance(new TimeRangedEvent(i * 5, 1, () =>
                        {
                            l1.alpha = 1;
                        }));
                        AddInstance(new TimeRangedEvent(i * 5 + 1, 1, () =>
                        {
                            l1.alpha = 0;
                        }));
                    }
                }
                if (GametimeF == 60 * 62.5f - 40)
                {
                    ScreenDrawing.WhiteOut(40);
                }
                if (GametimeF == 60 * 62.5f)
                {
                    ScreenDrawing.BackGroundColor = Color.Black;
                }
                if (GametimeF == 45 * 62.5f + 30)
                {
                    InstantSetBox(new Vector2(280, 200), 84, 84);
                    InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                    Heart.GiveForce(180, 0);
                    AddInstance(new TimeRangedEvent(0, 62.5f, () =>
                    {
                        ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.25f * 0.1f;
                        ScreenDrawing.ScreenPositionDetla = ScreenDrawing.ScreenPositionDetla * 0.9f + new Vector2(Heart.Centre.X - 320, Heart.Centre.Y - 240) * 0.1f;
                    }));
                }
                if (GametimeF == 45 * 62.5f + 30)
                {
                    InstantSetBox(new Vector2(280, 200), 84, 84);
                    InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                    AddInstance(new TimeRangedEvent(0, 62.5f - 30, () =>
                    {
                        ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.25f * 0.1f;
                        ScreenDrawing.ScreenPositionDetla = ScreenDrawing.ScreenPositionDetla * 0.9f + new Vector2(Heart.Centre.X - 320, Heart.Centre.Y - 240) * 0.1f;
                    }));
                    mainEffects.MaskSquare m = new(-640, -480, 640 * 3, 480 * 3, 600, Color.Black, 0);
                    CreateEntity(m);
                    for (int i = 0; i < 16; i++)
                    {
                        AddInstance(new TimeRangedEvent(i * 2, 1, () =>
                        {
                            m.alpha = 1;
                        }));
                        AddInstance(new TimeRangedEvent(i * 2 + 1, 1, () =>
                        {
                            m.alpha = 0;
                        }));
                    }
                }
                if (GametimeF == 46 * 62.5f)
                {
                    ScreenDrawing.ScreenAngle = 180;
                    InstantSetBox(new Vector2(360, 280), 84, 84);
                    InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                    Heart.GiveForce(180, 0);
                    AddInstance(new TimeRangedEvent(0, 62.5f - 30, () =>
                    {
                        ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.25f * 0.1f;
                        ScreenDrawing.ScreenPositionDetla = ScreenDrawing.ScreenPositionDetla * 0.9f + new Vector2(Heart.Centre.X - 320, Heart.Centre.Y - 240) * 0.1f;
                    }));
                    mainEffects.MaskSquare m = new(-640, -480, 640 * 3, 480 * 3, 600, Color.Black, 0);
                    CreateEntity(m);
                    for (int i = 0; i < 16; i++)
                    {
                        AddInstance(new TimeRangedEvent(i * 2, 1, () =>
                        {
                            m.alpha = 1;
                        }));
                        AddInstance(new TimeRangedEvent(i * 2 + 1, 1, () =>
                        {
                            m.alpha = 0;
                        }));
                    }
                }
                if (GametimeF == 47 * 62.5f)
                {
                    ScreenDrawing.ScreenAngle = 0;
                    SetGreenBox();
                    TP();
                    Heart.GiveForce(0, 0);
                    AddInstance(new TimeRangedEvent(0, 62.5f, () =>
                    {
                        ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1 * 0.1f;
                        ScreenDrawing.ScreenPositionDetla = ScreenDrawing.ScreenPositionDetla * 0.9f + new Vector2(0, 0) * 0.1f;
                    }));
                }
                if (GametimeF > 49 * 62.5f + 30 && GametimeF < 50 * 62.5f)
                {
                    if (Rand(0, 1) == 0)
                    {
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 - 126, 240), 84f, 84f);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                    }
                    else
                    {
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 + 126, 240), 84f, 84f);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                    }
                }
                if (GametimeF == (int)(42 * 16 * bpm))
                {
                    mainEffects.MaskSquare m = new(0, 0, 0, 480, 30, Color.DarkRed, 1);
                    CreateEntity(m);
                    AddInstance(new TimeRangedEvent(0, 15, () =>
                    {
                        m.width += 42.7f;
                    }));
                    AddInstance(new TimeRangedEvent(15, 15, () =>
                    {
                        m.LeftUpX += 42.7f;
                        m.width -= 42.7f;
                    }));
                }
                if (GametimeF == (int)(43 * 16 * bpm + 12 * bpm))
                {
                    mainEffects.MaskSquare m = new(0, 0, 640, 0, 20, Color.DarkRed, 1);
                    CreateEntity(m);
                    AddInstance(new TimeRangedEvent(0, 10, () =>
                    {
                        m.height += 48;
                    }));
                    AddInstance(new TimeRangedEvent(10, 10, () =>
                    {
                        m.LeftUpY += 48;
                        m.height -= 48;
                    }));
                }
                if (GametimeF == (int)(bpm * 16 * 48))
                {
                    ScreenDrawing.ScreenScale = 1.175f;
                    SetSoul(0);
                    int r = 0;
                    int t = (int)(bpm * 16);
                    mainEffects.DrawLine.Linerotate l1 = new(320, 520, 75, (int)(bpm * 32), 0.75f, Color.White, 0.1f);
                    mainEffects.DrawLine.Linerotate l2 = new(320, 580, 105, (int)(bpm * 32), 0.75f, Color.White, 0.1f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    AddInstance(new TimeRangedEvent(0, (int)(bpm * 16 * 8), () =>
                    {
                        ScreenDrawing.ScreenScale = Sin(r * 0.05f) * 0.135f + 1.175f;
                        r++;
                    }));
                    AddInstance(new TimeRangedEvent(0, (int)(bpm * 16), () =>
                    {
                        SetBoxMission(0);
                        t--;
                        InstantSetBox(new Vector2(320, 240 - t * t * 0.075f), 42, 42);
                        SetPlayerMission(0);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        l1.yCenter -= 8;
                        l1.alpha -= 0.00625f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(bpm * 16), 1, () =>
                    {
                        l1.alpha = 0.75f;
                        l2.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(bpm * 16), (int)(bpm * 16), () =>
                    {
                        SetBoxMission(0);
                        t++;
                        InstantSetBox(new Vector2(320, 240 + t * t * 0.075f), 42, 42);
                        SetPlayerMission(0);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        l1.yCenter -= 8;
                        l1.alpha -= 0.00625f;
                        l2.yCenter -= 8;
                        l2.alpha -= 0.00625f;
                    }));
                    AddInstance(new TimeRangedEvent(0, 1, () =>
                    {
                        ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                        ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                        ScreenDrawing.UIColor = Color.Transparent;
                    }));
                    for (int i = 0; i < 6; i++)
                    {
                        AddInstance(new TimeRangedEvent(9 * 62.5f + 30 + i * 5, 1, () =>
                        {
                            ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                            ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                            ScreenDrawing.UIColor = Color.Transparent;
                        }));
                        AddInstance(new TimeRangedEvent(9 * 62.5f + 31 + i * 5, 1, () =>
                        {
                            ScreenDrawing.HPBar.HPExistColor = Color.DarkRed;
                            ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                            ScreenDrawing.UIColor = Color.White;
                        }));
                    }
                }
                if (GametimeF == (int)(bpm * 16 * 50) - 60)
                {
                    int t = (int)(bpm * 16);
                    mainEffects.DrawLine.Linerotate l1 = new(320 - 40, 240, 45 + 69, (int)(bpm * 32) + 160, 0, Color.White, 0.1f);
                    mainEffects.DrawLine.Linerotate l2 = new(320 + 40, 240, 45 + 69, (int)(bpm * 32) + 160, 0, Color.White, 0.1f);
                    mainEffects.DrawLine.Linerotate l3 = new(320, 240 - 40, 135 + 69, (int)(bpm * 32) + 160, 0, Color.White, 0.1f);
                    mainEffects.DrawLine.Linerotate l4 = new(320, 240 + 40, 135 + 69, (int)(bpm * 32) + 160, 0, Color.White, 0.1f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    CreateEntity(l3);
                    CreateEntity(l4);
                    AddInstance(new TimeRangedEvent(0, 60, () =>
                    {
                        l1.alpha += 0.0125f;
                        l2.alpha += 0.0125f;
                        l3.alpha += 0.0125f;
                        l4.alpha += 0.0125f;
                        l1.rotate -= 1.15f;
                        l2.rotate -= 1.15f;
                        l3.rotate -= 1.15f;
                        l4.rotate -= 1.15f;
                    }));
                    AddInstance(new TimeRangedEvent(60, (int)(bpm * 16), () =>
                    {
                        SetBoxMission(0);
                        t--;
                        InstantSetBox(new Vector2(320 - t * t * 0.075f, 240), 42, 42);
                        SetPlayerMission(0);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        l1.alpha -= 0.00625f;
                        l2.alpha -= 0.00625f;
                        l3.alpha -= 0.00625f;
                        l4.alpha -= 0.00625f;
                        l1.rotate -= 1.15f;
                        l2.rotate -= 1.15f;
                        l3.rotate -= 1.15f;
                        l4.rotate -= 1.15f;
                        l1.xCenter += 0.25f;
                        l2.xCenter -= 0.25f;
                        l3.yCenter += 0.25f;
                        l4.yCenter -= 0.25f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(bpm * 16) + 60, 1, () =>
                    {
                        l1.alpha = 0.75f;
                        l2.alpha = 0.75f;
                        l3.alpha = 0.75f;
                        l4.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(bpm * 16) + 60, (int)(bpm * 16) + 100, () =>
                    {
                        SetBoxMission(0);
                        t++;
                        InstantSetBox(new Vector2(320 + t * t * 0.075f, 240), 42, 42);
                        SetPlayerMission(0);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        l1.alpha -= 0.00625f;
                        l2.alpha -= 0.00625f;
                        l3.alpha -= 0.00625f;
                        l4.alpha -= 0.00625f;
                        l1.rotate -= 1.15f;
                        l2.rotate -= 1.15f;
                        l3.rotate -= 1.15f;
                        l4.rotate -= 1.15f;
                        l1.xCenter += 0.25f;
                        l2.xCenter -= 0.25f;
                        l3.yCenter += 0.25f;
                        l4.yCenter -= 0.25f;
                    }));
                }
                if (GametimeF == (int)(bpm * 16 * 52))
                {
                    int t = (int)(bpm * 16);
                    mainEffects.DrawLine.Linerotate l1 = new(320 - 20, 240, 45, (int)(bpm * 32) + 100, 0.75f, Color.White, 0.99f);
                    mainEffects.DrawLine.Linerotate l2 = new(320 + 20, 240, 45, (int)(bpm * 32) + 100, 0.75f, Color.White, 0.99f);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    AddInstance(new TimeRangedEvent(0, (int)(bpm * 16), () =>
                    {
                        t--;
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 - t * t * 0.075f - 120, 240 - t * t * 0.075f), 42, 42);
                        SetPlayerMission(0);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320 + t * t * 0.075f + 120, 240 + t * t * 0.075f), 42, 42);
                        SetPlayerMission(1);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        l1.alpha -= 0.00625f;
                        l2.alpha -= 0.00625f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(bpm * 16), 1, () =>
                    {
                        l1.alpha = 0.75f;
                        l2.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(bpm * 16), (int)(bpm * 16) + 100, () =>
                    {
                        t++;
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 + t * t * 0.075f - 120, 240 + t * t * 0.075f), 42, 42);
                        SetPlayerMission(0);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320 - t * t * 0.075f + 120, 240 - t * t * 0.075f), 42, 42);
                        SetPlayerMission(1);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        l1.alpha -= 0.00625f;
                        l2.alpha -= 0.00625f;
                    }));
                    Heart.Split();
                    SetPlayerMission(0);
                    SetSoul(0);
                    SetPlayerMission(1);
                    SetSoul(2);
                }
                if (GametimeF == (int)(bpm * 16 * 54))
                {
                    int t = (int)(bpm * 16);
                    mainEffects.DrawLine.Linerotate l = new(320, 240, 0, (int)(bpm * 32), 0.75f, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(0, (int)(bpm * 16), () =>
                    {
                        t--;
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 - t * t * 0.075f - 80, 240), 42, 42);
                        SetPlayerMission(0);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320 + t * t * 0.075f + 80, 240), 42, 42);
                        SetPlayerMission(1);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        l.alpha -= 0.00625f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(bpm * 16), 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(bpm * 16), (int)(bpm * 16), () =>
                    {
                        t++;
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 - t * t * 0.075f - 80, 240), 42, 42);
                        SetPlayerMission(0);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320 + t * t * 0.075f + 80, 240), 42, 42);
                        SetPlayerMission(1);
                        InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                        l.alpha -= 0.00625f;
                    }));
                }
                if (GametimeF > (int)(bpm * 16 * 54) && GametimeF < (int)(bpm * 16 * 58))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.05f * 0.1f;
                }
                if (GametimeF == (int)(bpm * 16 * 56 - 20))
                {
                    SetPlayerMission(0);
                    SetSoul(1);
                    SetBoxMission(0);
                    InstantSetBox(new Vector2(320, 500), 84f, 84f);
                    SetBoxMission(1);
                    InstantSetBox(new Vector2(320, 500), 84f, 84f);
                    SetPlayerMission(0);
                    Player.hearts[1].Teleport(new(320, 500));
                    Player.hearts[1].Merge(Player.hearts[0]);
                    SetPlayerMission(0);
                    InstantTP(320, 500);
                    SetSoul(1);
                    SetPlayerMission(0);
                }
                if (GametimeF == (int)(bpm * 16 * 56))
                {
                    SetBoxMission(0);
                    SetGreenBox();
                    SetPlayerMission(0);
                    SetSoul(1);
                    TP(320, 240);
                }
                if (GametimeF == (int)(76 * 16 * bpm))
                {
                    mainEffects.DrawLine.NormalLine l = new(0, 240, 640, 240, (int)(2 * 16 * bpm), 0, Color.Wheat, 0.1f);
                    CreateEntity(l);
                    AddInstance(new InstantEvent((int)(12 * bpm), () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(12 * bpm), 5, () =>
                    {
                        l.alpha -= 0.15f;
                    }));
                    AddInstance(new InstantEvent((int)(14 * bpm), () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(14 * bpm), 5, () =>
                    {
                        l.alpha -= 0.15f;
                    }));
                    AddInstance(new InstantEvent((int)(16 * bpm), () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(16 * bpm), 5, () =>
                    {
                        l.alpha -= 0.15f;
                    }));
                    AddInstance(new InstantEvent((int)(20 * bpm), () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(20 * bpm), 5, () =>
                    {
                        l.alpha -= 0.15f;
                    }));
                    AddInstance(new InstantEvent((int)(22 * bpm), () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(22 * bpm), 5, () =>
                    {
                        l.alpha -= 0.15f;
                    }));
                    AddInstance(new InstantEvent((int)(23.5f * bpm), () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(23.5f * bpm), 5, () =>
                    {
                        l.alpha -= 0.15f;
                    }));
                    AddInstance(new InstantEvent((int)(25.5f * bpm), () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent((int)(25.5f * bpm), 5, () =>
                    {
                        l.alpha -= 0.15f;
                    }));
                }
                if (GametimeF > (int)(80f * 16 * bpm) && GametimeF < (int)(96f * 16 * bpm) && GametimeF % 10 == 0)
                {
                    rain3();
                }
                if (GametimeF == (int)(80f * 16 * bpm))
                {
                    float r = 0;
                    float g = 0;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(30, () =>
                    {
                        r++;
                        g++;
                        b++;
                    }));
                }
                if (GametimeF >= (int)(bpm * 16 * 80) && GametimeF < (int)(bpm * 16 * 80) + 12)
                {
                    ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(130, 206, 255), 0.08f);
                }
                if (GametimeF >= (int)(bpm * 16 * 80) + 12 && GametimeF < (int)(bpm * 16 * 80) + 24)
                {
                    ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(97, 131, 212), 0.06f);
                }
                if (GametimeF >= (int)(bpm * 16 * 80) + 24 && GametimeF < (int)(bpm * 16 * 80) + 60)
                {
                    ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(0, 0, 0), 0.01f);
                }
                if (GametimeF == (int)(bpm * 16 * 80))
                {
                    AddInstance(new TimeRangedEvent(bpm * 32, bpm * 16 - 1, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(109, 134, 204), 0.005f);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32 + bpm * 16, bpm * 16, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(30, 77, 99), 0.003f);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 48 + bpm * 16, bpm * 16, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(0, 0, 0), 0.003f);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 112 + bpm * (32 - 6), 12, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(228, 221, 248), 0.08f);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 112 + bpm * (32 - 6), 60, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(0, 0, 0), 0.05f);
                    }));
                }
                if (GametimeF == (int)(bpm * 16 * 96))
                {
                    ScreenDrawing.BackGroundColor = new(255, 255, 255);
                    AddInstance(new TimeRangedEvent(bpm * 4, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(69, 140, 188), 0.1f);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4 + 1, bpm * 100, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, new(0, 0, 0), 0.0075f);
                    }));
                }
            }
            public static void rain3()
            {
                float rot = -Rand(90 - 8, 90 + 8);
                float speed = Rand(3.7f, 4.2f);
                CreateEntity(new Particle(
                    mainEffects.BeautifulColor(),
                   new(
                       Cos(rot) * speed,
                       Sin(rot) * speed),
                   Rand(20, 26),
                   new(Rand(0, 170), 480), Sprites.square)
                {
                    AutoRotate = true,
                    SlowLerp = Rand(0.008f, 0.013f),
                    DarkingSpeed = Rand(1.5f, 2.0f),

                });
                float rot1 = -Rand(90 - 8, 90 + 8);
                float speed1 = Rand(3.7f, 4.2f);
                CreateEntity(new Particle(
                    mainEffects.BeautifulColor(),
                   new(
                       Cos(rot1) * speed1,
                       Sin(rot1) * speed1),
                   Rand(20, 26),
                   new(640 - Rand(0, 170), 480), Sprites.square)
                {
                    AutoRotate = true,
                    SlowLerp = Rand(0.008f, 0.013f),
                    DarkingSpeed = Rand(1.5f, 2.0f)
                });
            }
            private static class ExBarrage
            {

                public static void ArrowAtk1()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //1
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //2
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //3
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //4
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //5
                        "R","/","/","/",    "R0","/","/","/",    "R","/","/","/",    "R0","/","/","/",
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //6
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //7
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //8
                        "(R)(+01)","/","(+0)(+01)","/",    "(R)(+01)","/","(+0)(+01)","/",    "(R)(+01)","/","(+0)(+01)","/",    "(R)(+01)","/","(+0)(+01)","/",

                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                    float Beatt = 0;
                    string[] Base =
                        {
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                       "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                       //2
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //3
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //4
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",   
                        //5
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //6
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                    };
                    for (int i = 0; i < Base.Length; i++)
                    {
                        if (Base[i] == "/")
                        {
                            Beatt += 0.5f * bpm;
                        }
                        else if (Base[i] != "/")
                        {
                            float rand = Rand(-11.0f, 11.0f);
                            instance.CreateArrows(80 + Beatt, 6f, Base[i]);
                            Beatt += 0.5f * bpm;
                            AddInstance(new TimeRangedEvent(80 + Beatt, bpm * 4 - 1, () => { HeartRot = rand * 0.06f + HeartRot * 0.94f; }));
                        }
                    }
                }
                public static void ArrowAtk2()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //1
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //2
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //3
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //4
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //5
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //6
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //7
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",    "/","/","/","/",
                        //8
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "($0)($0)","($01)($01)","($2)($2)","($21)($21)", "($0)($0)","($01)($01)","($2)($2)","($21)($21)",
                        "($0)($0)","($01)($01)","($2)($2)","($21)($21)", "($0)($0)","($01)($01)","($2)($2)","($21)($21)",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] == "X")
                        {
                            int way = Rand(0, 3);
                            int way2 = Rand(0, 3);
                            CreateArrow(80 + Beat, way, 7.5f, 1, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat, way2, 7.5f, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat + bpm * 2, way, 7.5f, 1, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat + bpm * 2, way2, 7.5f, 0, 0, ArrowAttribute.SpeedUp);
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 10f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk3()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "$0(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        "R(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        //
                        "R(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        "R(+212)","/","/","/",    "+000","/","/","/",    "$001($001)","-101","+101","-101",    "+101","-101","+001","-001",
                        //
                        "$0(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        "R(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        //
                        "R(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        "R(+212)","/","/","/",    "+000","/","/","/",    "$201($201)","+101","+101","+101",    "+001","+001","+001","+001",
                        //
                        "$0(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        "R(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        //
                        "R(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        "R(+212)","/","/","/",    "+000","/","/","/",    "$001($001)","+001","+101","+001",    "+101","+001","+101","+001",
                        //
                        "$0(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                        "R(+212)","/","/","/",    "+000","/","/","/",    "R(+212)(+012)","/","/","/",    "+000","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                    //float Beatt = 0;
                    /* string[] Bass =
                     {
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                     };
                     for (int i = 0; i < Bass.Length; i++)
                     {
                         if (Bass[i] == "/")
                         {
                             Beatt += 0.5f * bpm;
                         }
                         else if (Bass[i] != "/")
                         {
                             UndyneFight_Ex.Fight.AdvanceFunctions.CreateArrows(80 + Beatt, 6.85f, Bass[i]);
                             Beatt += 0.5f * bpm;
                         }
                     }*/
                }
                public static void ArrowAtk4()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "(R)(+210)","/","(+2)(+210)","/",    "(+2)(+210)","/","(+2)(+210)","/"
                        };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk5()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            int way = Rand(0, 3);
                            int way2 = Rand(0, 3);
                            CreateArrow(80 + Beat, way, 7.5f, 1, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat, way2, 7.5f, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat + bpm * 2, way, 7.5f, 1, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat + bpm * 2, way2, 7.5f, 0, 0, ArrowAttribute.SpeedUp);
                            Beat += 0.5f * bpm;
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk6()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "$0","/","/","/",    "$0","/","/","/",    "$2","/","/","/",    "$2","/","/","/",
                        "$0","/","/","/",    "$0","/","/","/",    "$2","/","/","/",    "$2","/","/","/",
                        //
                        "(D)(+0)","/","/","+001(+001)",    "/","/","+001(+001)","/",     "/","(D)(+0)","/","/",    "+001(+001)","/","/","+001(+001)",
                        "/","/","(D)(+0)","/",    "/","+001(+001)","/","/",    "(+001)(+001)($011)","+111","+111","+011","($011)","+111","+111","+011",
                        };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk7()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "$0($21)","/","/","/",
                        "$1($31)","/","/","/",    "/","/","/","/",    "$2($01)","/","/","/",    "G302","($11)","/","/","/",
                        //
                        "$11","/","/","/",    "$11($11)","/","$11($11)","/",     "$11($11)","/","/","/",    "$11","/","/","/",
                        "$2($21)","/","/","/",    "$2($21)","/","/","/",    "$0($01)","/","/","/",    "$0($01)","/","/","/",
                        //
                        "$10","/","/","/",    "+00","/","/","/",    "+00","/","/","/",    "$31","/","/","/",
                        "+01","/","/","/",    "/","/","/","/",    "+01","/","/","/",    "G002","/","/","/","/",
                        //
                        "$0","/","/","/",    "$01($01)","/","$01($01)","/",    "$01($01)","/","/","/",    "$01($01)","/","/","/",
                        "R(R1)","/","/","/",    "R(R1)","/","/","/",    "R(R1)","/","/","/",    "R(R1)","/","/","/",
                        //
                        "$11","/","/","/", "$0","/","/","/", "/","/","/","/", "$31","/","/","/",
                        "$2","/","/","/", "/","/","/","/", "$0","/", "/","/", "$2","/","/","/",
                        //
                        "G202","/","/","/","/",    "$21($21)","/","$21($21)","/",   "$21($21)","/","/","/",   "$21","/","/","/",
                        "$21","/","/","/",    "/","/","/","/",    "$111($111)","/","/","/",    "(R)(+0)","/","/","/",
                        //
                        "(R)(+0)","/","/","/",    "(R)(+0)","/","/","/",    "(R)(+0)","/","/","/",    "(R)(+0)","/","/","/",
                        "(R)(+0)","/","/","/",    "(R)(+0)","/","/","/",    "(R)(+0)","/","/","/",    "(R)(+0)","/","/","/",
                        //split
                        "(R)(+0)","/","/","/",    "(R)(+0)","/","/","/",    "(R)(+0)","/","/","/",    "/","/","/","/",

                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/", "$3","/","/","/",    "+001","/","/","/",
                        //
                        "+001","/","/","/",    "$20","/","/","/",    "+001","/","/","/",    "+001","/","/","/",
                        "$30","/","/","/",    "+001","/","/","/",    "+000","/","/","/",    "$30","/","(+001)(+001)","/",
                        //turn
                        "+0","/","/","/",    "($3)","/","/","/",    "($2)","/","/","/",    "$30","/","/","/",
                        "$30","/","/","/",    "$30","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$20","/","/","/",    "+0","/","/","/",    "$30","/","/","/",    "$30","/","+001","/",

                        //split
                        "+0","/","/","/",    "$2(+0)","/","/","/",    "$2(+2)","/","/","/",    "$20","/","+0","/",
                        "$20","/","/","/",    "$30","/","/","/",    "$301","/","/","/",    "+001","/","/","/",
                        //
                        "+01","/","/","/",    "(D)(+01)","/","/","/",    "(D)(+01)","/","/","/",    "(D)(+01)","/","/","/",
                         "(D)(+01)","/","/","/",    "(D)(+01)","/","/","/",    "(D)(+01)","/","/","/",    "D0","/","+01","/",
                        //
                        "+01","/","/","/",    "D(+01)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","$100","/",    "/","/","/","/",    "/","/","/","/",   "(R1)(+01)","/",   "/","/","(R1)(+01)","/",
                        //
                        "/","/","(R1)(+01)","/",    "/","/","(R1)(+01)","/",    "/","/","(R1)(+01)","/","/","/",    "(R1)(+01)","/","/","/",
                        "G112","($11)(+01)","/","/","/",    "/","/","/","/",    "/","/","/","/",    
                        //
                        "/","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R(R1)","/","/","/",    "/","/","/","/",   "/","/","/","/",    "/","/","/","/",
                        };
                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(Beat + 80, Arrow[i][1] - '0', Arrow[i][2] - '0', (Arrow[i][3] - '0') * greencount));
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk7_1()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //split
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",

                        "/","/","/","/",    "$011","/","/","/",    "$01","/","/","/",    "$11","/","$011","/",
                        //turn
                        "+01","/","/","/",    "($11)(+01)","/","/","/",    "($11)(+01)","/","/","/",    "$11","/","/","/",
                        "$01","/","/","/",    "$01","/","/","/",    "$011","/","/","/",    "+011","/","/","/",
                        //
                        "+01","/","/","/",    "$11","/","/","/",    "+01","/","/","/",    "+011","/","/","/",
                        "$01","/","/","/",    "+01","/","/","/",    "+01","/","/","/",    "$11","/","+011","/",
                        //split
                        "+01","/","/","/",    "$11(+01)","/","/","/",    "$01(+01)","/","/","/",    "$01","/","+01","/",
                        "$01","/","/","/",    "$01","/","/","/",    "/","/","/","/",    "/","/","/","/",

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
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk8()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "EF1","$0","/","/","/",    "+1","/","/","/",    "+1","/","/","/",    "+1","/","/","/",
                        "+1","/","/","/",    "+1","/","/","/",    "+1","/","/","/",    "+1","/","/","/",
                        //
                        "+1","/","/","/",    "+1","/","/","/",    "+1","/","/","/",    "+1","/","/","/",
                        "+101","/","/","/",    "+1","/","/","/",    "+1","/","/","/",    "+1","/","/","/",
                        //
                        "EF2","+1","/","/","/",    "+1","/","/","/",    "+101","/","/","/",    "+1","/","/","/",
                        "+10","/","/","/",    "+101","/","/","/",    "+1","/","/","/",    "+1","/","/","/",
                        //
                        "+10","/","/","/",    "+101","/","/","/",    "+1","/","/","/",    "+1","/","/","/",
                        "+1","/","/","/",    "+1","/","/","/",    "+101","/","/","/",    "+1","/","/","/",
                        //
                        "EF3","+3","/","/","/",    "+3","/","/","/",    "+3","/","/","/",    "+301","/","/","/",
                        "+3","/","/","/",    "+3","/","/","/",    "+3","/","/","/",    "+301","/","/","/",
                        //
                        "+301","/","/","/",    "+301","/","/","/",    "+301","/","/","/",    "+301","/","/","/",
                        "+301","/","/","/",    "+301","/","/","/",    "+301","/","/","/",    "+3","/","/","/",
                        //
                        "EF4","+3","/","/","/",    "+3","/","/","/",    "+3","/","/","/",    "+3","/","/","/",
                        "+3","/","/","/",    "+301","/","/","/",    "+3","/","/","/",    "+3","/","/","/",
                        //
                        "+3","/","/","/",    "+3","/","/","/",    "+3","/","/","/",    "+3","/","/","/",
                        "+3","/","/","/",    "+3","/","/","/",    "+3","/","/","/",    "+301","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] == "EF1")
                        {
                            AddInstance(new TimeRangedEvent(Beat, bpm * 16 - 2, () => { HeartRot = HeartRot * 0.97f + 130 * 0.03f; }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, bpm * 16 - 2, () => { HeartRot = HeartRot * 0.97f + 320 * 0.03f; }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.Linerotate line1 = new(320, 240, 15, bpm * 26, 1) { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.Linerotate line2 = new(320, 240, 11, bpm * 26, 0.8f) { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.Linerotate line3 = new(320, 240, 19, bpm * 26, 0.8f) { Tags = new string[] { "a" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                CreateEntity(line3);
                                AddInstance(new InstantEvent(1, () =>
                                {
                                    Extends.DrawingUtil.Linerotate[] lines = GetAll<Extends.DrawingUtil.Linerotate>("a");
                                    for (int a = 0; a < lines.Length; a++)
                                    {
                                        int x = a;
                                        AddInstance(new TimeRangedEvent(bpm * 64, () => { lines[x].alpha = lines[x].alpha * 0.975f + 0 * 0.025f; lines[x].rotate = lines[x].rotate * 0.95f - 8 * 0.05f; }));
                                    }
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () =>
                            {
                                Extends.DrawingUtil.Linerotate line1 = new(320, 240, 80, 800, 1) { Tags = new string[] { "b" } };
                                Extends.DrawingUtil.Linerotate line2 = new(320, 240, 75, 800, 0.8f) { Tags = new string[] { "b" } };
                                Extends.DrawingUtil.Linerotate line3 = new(320, 240, 85, 800, 0.8f) { Tags = new string[] { "b" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                CreateEntity(line3);
                                AddInstance(new InstantEvent(1, () =>
                                {
                                    Extends.DrawingUtil.Linerotate[] lines = GetAll<Extends.DrawingUtil.Linerotate>("b");
                                    for (int a = 0; a < lines.Length; a++)
                                    {
                                        int x = a;
                                        AddInstance(new TimeRangedEvent(bpm * 17, () => { lines[x].rotate = lines[x].rotate * 0.95f + 103 * 0.05f; }));
                                        AddInstance(new TimeRangedEvent(bpm * 32, () => { lines[x].alpha = lines[x].alpha * 0.95f + 0 * 0.05f; }));
                                    }
                                }));
                            }));
                            for (int a = 0; a < 16; a++)
                            {
                                AddInstance(new TimeRangedEvent(Beat + a * 5, 1, () =>
                                {
                                    for (int b = 0; b < 3; b++)
                                    {
                                        float rot = -Rand(90 - 8, 90 + 8);
                                        float speed = Rand(2f, 3.1f);
                                        CreateEntity(new Particle(
                                            new Color(241, 214, 91),
                                           new(
                                               Cos(rot) * speed,
                                               Sin(rot) * speed),
                                           Rand(16, 24),
                                           new(Rand(320 - 140, 320 + 140), 480), Sprites.square)
                                        {
                                            AutoRotate = true,
                                            SlowLerp = Rand(0.006f, 0.008f),
                                            DarkingSpeed = Rand(2.0f, 2.5f)
                                        });
                                    }
                                }));
                            }
                        }
                        else if (Arrow[i] == "EF2")
                        {
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(Beat, (int)bpm * 35f, () =>
                            {
                                sin += 180 / (int)bpm / 35f;
                                ScreenDrawing.ScreenScale = 1 + Sin(sin) * 5f;
                            }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () => { SetSoul(1); HeartRot = 180; }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () => { SetSoul(1); HeartRot = 0; }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.NormalLine line1 = new(
                                    0, 480,
                                    640,
                                    640,
                                    500, 1, Color.Red * 0.8f
                                    )
                                { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.NormalLine line2 = new(
                                   640,
                                   800,
                                   0,
                                   640,
                                    500, 1, Color.Red * 0.8f
                                    )
                                { Tags = new string[] { "a" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                float sin = 0;
                                AddInstance(new TimeRangedEvent(1, bpm * 32, () =>
                                {
                                    sin += 360 / bpm / 128;
                                    line1.y1 = 480 - Sin(sin) * 820;
                                    line1.y2 = 640 - Sin(sin) * 820;
                                    line2.y1 = 640 - Sin(sin) * 820;
                                    line2.y2 = 800 - Sin(sin) * 820;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () =>
                            {
                                AddInstance(new InstantEvent(2, () =>
                                {
                                    Extends.DrawingUtil.Linerotatelong[] Lastlines = GetAll<Extends.DrawingUtil.Linerotatelong>("b");
                                    for (int a = 0; a < Lastlines.Length; a++)
                                    {
                                        int x = a;
                                        Lastlines[x].Dispose();
                                    }

                                }));
                            }));
                            for (int a = 0; a < 10; a++)
                            {

                                AddInstance(new TimeRangedEvent(Beat + a * 5, 1, () =>
                                {
                                    for (int b = 0; b < 3; b++)
                                    {
                                        float rot = Rand(0, 360);
                                        float speed = Rand(0.8f, 1.1f);
                                        CreateEntity(new Particle(
                                            new Color(241, 214, 91),
                                           new(
                                               Cos(rot) * speed,
                                               Sin(rot) * speed),
                                           Rand(12, 18),
                                           Heart.Centre, Sprites.square)
                                        {
                                            AutoRotate = true,
                                            SlowLerp = Rand(0.006f, 0.008f),
                                            DarkingSpeed = Rand(2.0f, 2.5f)
                                        });
                                    }
                                }));
                            }
                        }
                        else if (Arrow[i] == "EF3")
                        {
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(Beat, bpm * 16, () =>
                            {
                                sin += 90 / bpm / 16;
                                ScreenDrawing.ScreenPositionDetla = new(-Sin(sin) * 750f, 0);
                            }));
                            float sin2 = 180;
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, bpm * 16, () =>
                            {
                                sin2 += 90 / bpm / 16;
                                ScreenDrawing.ScreenPositionDetla = new(640 + Sin(sin2) * 640f, 0);
                            }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.NormalLine line = new(320 - 80, 120, 320 + 80, 120, (int)(bpm * 48), 1, new(255, 255, 255, 125));
                                line.width = 0;
                                Extends.DrawingUtil.NormalLine line1 = new(320, 40, 320, 240 + 40, (int)(bpm * 48), 1, new(255, 255, 255, 125));
                                line1.width = 0;
                                CreateEntity(line);
                                CreateEntity(line1);
                                AddInstance(new TimeRangedEvent(bpm * 16, () =>
                                {
                                    line.width = line.width * 0.96f + 36 * 0.04f;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 16, bpm * 32, () =>
                                {
                                    line1.width = line1.width * 0.97f + 36 * 0.03f;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 28, bpm * 16, () =>
                                {
                                    line.color = Color.Lerp(line.color, new(0, 0, 0, 0), 0.05f);
                                    line1.color = Color.Lerp(line1.color, new(0, 0, 0, 0), 0.05f);
                                }));
                            }));
                        }
                        else if (Arrow[i] == "EF4")
                        {
                            AddInstance(new TimeRangedEvent(Beat, 1, () => { ScreenDrawing.CameraEffect.SizeExpand(100, bpm * 30); }));

                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);

                            instance.CreateArrows(Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void Phase2Arrow1()
                {
                    float beat = 0;
                    string[] arrow =
                        {

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        //"R","/","/","/",   "+0","/","/","/",

                       // "+1","/","/","/",   "/","/","/","/",
                        //"/","/","/","/",   "R1","/","/","/",
                        //"R1","/","/","R", "/","/","/","+0",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "S00","/","/","/",   "/","/","/","/",
                        "S10","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "M","EF1","(R1)(+01)(R)","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "M","(R1)(+01)","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "M","(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","(R1)(+01)","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "(R1)(+01)","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "(R1)(+01)","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","(R1)(+01)","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "(R1)(+01)","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "(R1)(+01)","/","/","/",   "D","/","/","/",

                        "M","(R1)(+01)","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "(R1)(+01)","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "(R1)(+01)","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "M","(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","EF2","(R1)(+01)(R)","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "EF3","(R1)(+01)","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "M","(R1)(+01)","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "(R1)(+01)","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","(R1)(+01)","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "EF4","(R1)(+01)","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "(R1)(+01)","/","/","/",
                        "D","/","/","/",   "(R1)(+01)","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","(R1)(+01)","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "(R1)(+01)","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "EF5","(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "(R1)(+01)","/","/","/",   "(R1)(+01)","/","/","/",

                        "Shake","B","(R1)(+01)","/","/","/",   "R(R1)","/","/","/",
                        "Shake","R(R1)","/","/","/",   "R(R1)","/","/","/",
                        "Shake","R(R1)","/","/","/",   "R(R1)","/","/","/",
                        "Shake","R(R1)","/","/","/",   "R(R1)","/","/","/",

                        "Shake","(R)(+01)","/","(+0)(+01)","/",   "(R)(+01)","/","(+0)(+01)","/",
                        "Shake","(R)(+01)","/","(+0)(+01)","/",   "(R)(+01)","/","(+0)(+01)","/",

                        "Shake","($0)($0)","($01)($01)","($2)($2)","($21)($21)", "($0)($0)","($01)($01)","($2)($2)","($21)($21)",
                        "Shake","($0)($0)","($01)($01)","($2)($2)","($21)($21)", "($0)($0)","($01)($01)","($2)($2)","($21)($21)",
            };

                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "EF1")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                for (int a = 0; a < 32; a++)
                                {
                                    float rot = Rand(0, 359);
                                    float speed = Rand(9f, 14.5f);
                                    CreateEntity(new Particle(
                                        mainEffects.BeautifulColor(),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(22, 26),
                                       new(320, 240), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(4.2f, 5.7f)
                                    });
                                }
                            }));
                        }
                        else if (arrow[i] == "EF2")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                for (int a = 0; a < 64; a++)
                                {
                                    float rot = Rand(270 - 30, 270 + 30);
                                    float speed = Rand(1f, 16f);
                                    CreateEntity(new Particle(
                                        new(129, 27, 50),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(18, 24),
                                       new(Rand(0, 640), 480), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(3f, 4.3f)
                                    });
                                }
                                for (int a = 0; a < 64; a++)
                                {
                                    float rot = Rand(90 - 30, 90 + 30);
                                    float speed = Rand(1f, 16f);
                                    CreateEntity(new Particle(
                                        new(62, 218, 228),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(18, 24),
                                       new(Rand(0, 640), 0), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(3f, 4.3f)
                                    });
                                }
                            }));
                        }
                        else if (arrow[i] == "EF3")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                for (int a = 0; a < 94; a++)
                                {
                                    float rot = Rand(90 - 30, 90 + 30);
                                    float speed = Rand(2f, 20f);
                                    CreateEntity(new Particle(
                                        new(0, 0, 0),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(22, 26),
                                       new(Rand(0, 640), 0), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.03f, 0.04f),
                                        DarkingSpeed = Rand(2f, 2.5f)
                                    });
                                }
                            }));
                        }
                        else if (arrow[i] == "EF4")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {

                                AddInstance(new TimeRangedEvent(60, () =>
                                {
                                    float rot = Rand(-10, 10);
                                    float speed = Rand(2f, 4f);
                                    CreateEntity(new Particle(
                                        new(255, 255, 255, 0),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(22, 26),
                                       new(0, Rand(0, 480)), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = -Rand(0.015f, 0.020f),
                                        DarkingSpeed = Rand(6.0f, 7.0f)
                                    });
                                }));
                                AddInstance(new TimeRangedEvent(60, () =>
                                {
                                    float rot = 180 + Rand(-10, 10);
                                    float speed = Rand(2f, 4f);
                                    CreateEntity(new Particle(
                                        new(255, 255, 255, 0),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(22, 26),
                                       new(640, Rand(0, 480)), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = -Rand(0.015f, 0.020f),
                                        DarkingSpeed = Rand(6.0f, 7.0f)
                                    });
                                }));
                            }));
                        }

                        else if (arrow[i] == "EF5")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 15 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.Linerotate line = new(320, 240, 97, bpm * 16, 0);
                                CreateEntity(line);
                                AddInstance(new TimeRangedEvent(bpm * 1, () =>
                                {
                                    line.alpha += 1 / bpm;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 1, bpm * 3, () =>
                                {
                                    line.alpha -= 1 / bpm / 3;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 4, bpm * 1, () =>
                                {
                                    line.rotate = 90 - 7;
                                    line.alpha += 1 / bpm;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 5, bpm * 6, () =>
                                {
                                    line.alpha -= 1 / bpm / 6;
                                }));
                            }));
                        }
                        else if (arrow[i] == "Shake")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 5, delegate ()
                            {
                                ScreenDrawing.ScreenPositionDetla = new(Rand(-3, 3), 0);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + 8 + beat, 1, delegate ()
                            {
                                ScreenDrawing.ScreenPositionDetla = new(0, 0);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 2 + beat, 5, delegate ()
                            {
                                ScreenDrawing.ScreenPositionDetla = new(Rand(-3, 3), 0);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 2 + 8 + beat, 1, delegate ()
                            {
                                ScreenDrawing.ScreenPositionDetla = new(0, 0);
                            }));
                        }
                        else if (arrow[i] == "M")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpGreenBox(bpm * 8, new(320 + Rand(-26, 26), 240 + Rand(-24, 24)), 0.01f);
                            }));
                        }
                        else if (arrow[i] == "B")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpGreenBox(bpm * 8, new(320, 240), 0.01f);
                            }));
                        }
                        else if (arrow[i] == "($0)($0)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($0)($0)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($01)($01)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($01)($01)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($2)($2)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($2)($2)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($21)($21)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($21)($21)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            beat += bpm * 0.5f;
                            CreateArrow(bpm * 16 + beat, "R", 10f, arrow[i][1] - '0', arrow[i][2] - '0');
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            beat += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "R", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 6f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void FinalAttack()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                        "Line","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "Line","(R1)(+01)","/","+01(+01)","/",   "+01(+01)","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",

                       "Line", "(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",
                       "Line","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",

                       "Line", "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "Line", "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                       "Line", "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "Line","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "Heart","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",
                        "Heart","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",

                        "Heart","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",
                       "Heart","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",

                        "Line","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "Line","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "Line","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "Line","/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "EF1","(R1)(+01)","/","(+01)(+01)","/",   "(+01)(+01)","/","(+11)","(+11)",
                        "+11","/","/","/",   "/","/","/","/",
                        "EF2","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",

                        "EF3","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",
                       "(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",

                        "EF4","(R1)(+01)","/","(+01)(+01)","/",   "(+01)(+01)","/","(+11)(+01)","(+11)(+01)",
                        "(+11)(+01)","/","/","/",   "/","/","/","/",
                        "EF5","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",

                        "EF6","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",
                       "EF7","(R1)(+01)","/","+01","/",   "+01","/","+11","+11",
                        "+11","/","/","/",   "/","/","/","/",

                        "Shake","(R)(+21)","/","/","/",   "(+2)(+21)","/","/","/",
                        "Shake","(R)(+21)","/","/","/",   "(+2)(+21)","/","/","/",
                        "Shake","(R)(+21)","/","/","/",   "(+2)(+21)","/","/","/",
                        "Shake","(R)(+21)","/","/","/",   "(+2)(+21)","/","/","/",

                        "Shake","(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "Shake","(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "Shake","(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",
                        "Shake","(R)(R1)","/","/","/",   "(R)(R1)","/","/","/",

                        "Shake","($1)($3)($01)","/","/","/",   "($1)($3)($21)","/","/","/",
                        "Shake","($1)($3)($01)","/","/","/",   "($1)($3)($21)","/","/","/",
                        "Shake","($1)($3)($01)","/","/","/",   "($1)($3)($21)","/","/","/",
                        "Shake","($1)($3)($01)","/","/","/",   "($1)($3)($21)","/","/","/",

                        "Shake","(R)(+01)","/","(+0)(+01)","/",   "(R)(+01)","/","(+0)(+01)","/",
                        "Shake","(R)(+01)","/","(+0)(+01)","/",   "(R)(+01)","/","(+0)(+01)","/",
                        "EF1","Heart","Shake","($0)($0)","($01)($01)","($2)($2)","($21)($21)", "($0)($0)","($01)($01)","($2)($2)","($21)($21)",
                        "Shake","($0)($0)","($01)($01)","($2)($2)","($21)($21)", "($0)($0)","($01)($01)","/","/",
                        "G08","S1"
            };

                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "EF1")
                        {

                            for (int a = 0; a < 90; a++)
                            {
                                AddInstance(new TimeRangedEvent(bpm * 16 + a * 0.3f + beat, 1, () =>
                                {
                                    float rot = -Rand(80, 105);
                                    float speed = Rand(10f, 13f);
                                    CreateEntity(new Particle(
                                        new(Rand(150, 222), LastRand, LastRand),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(6, 54),
                                       new(Rand(0, 640), 500), Sprites.lightBall)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = -Rand(0.070f, 0.080f)
                                    });
                                }));

                            }
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, bpm * 8, () =>
                            {
                                sin += 360 / bpm / 16;
                                ScreenDrawing.BackGroundColor = new((int)(Sin(sin) * 77), (int)(Sin(sin) * 106), (int)(Sin(sin) * 150));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpScreenPos(bpm * 6, new(0, -30), 0.075f);
                            }));
                        }
                        else if (arrow[i] == "EF2")
                        {

                            for (int a = 0; a < 90; a++)
                            {
                                AddInstance(new TimeRangedEvent(bpm * 16 + a * 0.3f + beat, 1, () =>
                                {
                                    float rot = Rand(80, 100);
                                    float speed = Rand(10f, 13f);
                                    CreateEntity(new Particle(
                                        new(Rand(150, 222), LastRand, LastRand),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(6, 73),
                                       new(Rand(0, 640), -20), Sprites.lightBall)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = -Rand(0.070f, 0.080f)
                                    });
                                }));

                            }
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, bpm * 8, () =>
                            {
                                sin += 360 / bpm / 16;
                                ScreenDrawing.BackGroundColor = new((int)(Sin(sin) * 42), (int)(Sin(sin) * 77), (int)(Sin(sin) * 170));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpScreenPos(bpm * 6, new(0, 30), 0.075f);
                            }));
                        }
                        else if (arrow[i] == "EF3")
                        {

                            for (int a = 0; a < 64; a++)
                            {
                                AddInstance(new TimeRangedEvent(bpm * 16 + a * 0.5f + beat, 1, () =>
                                {
                                    float rot = Rand(88, 92);
                                    float speed = Rand(15f, 26f);
                                    CreateEntity(new Particle(
                                        new(Rand(150, 222), LastRand, LastRand),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(6, 18),
                                       new(Rand(60, 290), -20), Sprites.lightBall)
                                    {
                                        AutoRotate = true,
                                        DarkingSpeed = Rand(3.2f, 4.8f)
                                    });
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 16 + a * 0.5f + beat, 1, () =>
                                {
                                    float rot = Rand(88, 92);
                                    float speed = -Rand(15f, 26f);
                                    CreateEntity(new Particle(
                                        new(Rand(150, 222), LastRand, LastRand),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(6, 18),
                                       new(Rand(60, 290), 500), Sprites.lightBall)
                                    {
                                        AutoRotate = true,
                                        DarkingSpeed = Rand(3.2f, 4.8f)
                                    });
                                }));
                            }
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpScreenPos(bpm * 6, new(14, 14), 0.075f);
                                Extends.DrawingUtil.RotateWithBack(bpm * 16, -20);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 32 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpScreenPos(bpm * 6, new(0, 0), 0.075f);
                                Extends.DrawingUtil.RotateWithBack(bpm * 16, 20);
                            }));
                        }
                        else if (arrow[i] == "EF4")
                        {

                            for (int a = 0; a < 40; a++)
                            {
                                AddInstance(new TimeRangedEvent(bpm * 16 + a * 0.8f + beat, 1, () =>
                                {
                                    float rot = -Rand(70, 110);
                                    float speed = Rand(5f, 7f);
                                    CreateEntity(new Particle(
                                        new(255, 255, 255),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       24,
                                       new(Rand(0, 640), 500), Sprites.brokenHeart)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = -Rand(0.08f, 0.1f),
                                        DarkingSpeed = 13f
                                    });
                                }));

                            }
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, bpm * 8, () =>
                            {
                                sin += 360 / bpm / 16;
                                ScreenDrawing.BackGroundColor = new((int)(Sin(sin) * 80), (int)(Sin(sin) * 0), (int)(Sin(sin) * 0));
                            }));
                        }
                        else if (arrow[i] == "EF5")
                        {

                            for (int a = 0; a < 40; a++)
                            {
                                AddInstance(new TimeRangedEvent(bpm * 16 + a * 0.8f + beat, 1, () =>
                                {
                                    float rot = Rand(70, 110);
                                    float speed = Rand(5f, 7f);
                                    CreateEntity(new Particle(
                                        new(255, 255, 255),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       24,
                                       new(Rand(0, 640), -20), Sprites.arrow[Rand(0, 1), Rand(0, 1), 2])
                                    {
                                        AutoRotate = true,
                                        SlowLerp = -Rand(0.08f, 0.1f),
                                        DarkingSpeed = 13f,
                                        Rotation = Rand(0, 359)
                                    });
                                }));

                            }
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, bpm * 8, () =>
                            {
                                sin += 360 / bpm / 16;
                                ScreenDrawing.BackGroundColor = new((int)(Sin(sin) * 80), (int)(Sin(sin) * 0), (int)(Sin(sin) * 0));
                            }));
                        }
                        else if (arrow[i] == "EF6")
                        {
                            ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(muhai));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, bpm * 6, delegate ()
                            {
                                muhai.Parameters["distance2"].SetValue(new Vector2(Rand(-0.35f, 0.15f), Rand(-0.25f, 0.25f)));
                                muhai.Parameters["distance3"].SetValue(new Vector2(Rand(-0.15f, 0.35f), Rand(-0.25f, 0.25f)));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 6 + 1 + bpm * 16 + beat, 1, delegate ()
                            {
                                muhai.Parameters["distance2"].SetValue(new Vector2(0f, 0f));
                                muhai.Parameters["distance3"].SetValue(new Vector2(0f, 0f));
                            }));
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, bpm * 8, () =>
                            {
                                sin += 360 / bpm / 16;
                                ScreenDrawing.BackGroundColor = new((int)(Sin(sin) * 20), (int)(Sin(sin) * 80), (int)(Sin(sin) * 80));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                ScreenDrawing.CameraEffect.SizeShrink(8, bpm * 8);
                            }));
                        }
                        else if (arrow[i] == "EF7")
                        {
                            ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(muhai));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, bpm * 6, delegate ()
                            {
                                muhai.Parameters["distance2"].SetValue(new Vector2(Rand(-0.35f, 0.15f), Rand(-0.25f, 0.25f)));
                                muhai.Parameters["distance3"].SetValue(new Vector2(Rand(-0.15f, 0.35f), Rand(-0.25f, 0.25f)));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 6 + 1 + bpm * 16 + beat, 1, delegate ()
                            {
                                muhai.Parameters["distance2"].SetValue(new Vector2(0f, 0f));
                                muhai.Parameters["distance3"].SetValue(new Vector2(0f, 0f));
                            }));
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, bpm * 8, () =>
                            {
                                sin += 360 / bpm / 16;
                                ScreenDrawing.BackGroundColor = new((int)(Sin(sin) * 120), (int)(Sin(sin) * 20), (int)(Sin(sin) * 110));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                ScreenDrawing.CameraEffect.SizeExpand(8, bpm * 8);
                            }));
                        }
                        else if (arrow[i] == "Heart")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, delegate ()
                            {
                                SetSoul(1);
                                ScreenDrawing.CameraEffect.SizeExpand(12, bpm * 12);
                                CreateEntity(new Particle(
                                        new(0, 255, 0),
                                       new(
                                           Cos(90) * 6,
                                           Sin(90) * 6),
                                       24,
                                       new(Rand(0, 640), -20), Sprites.soulCollide)
                                {
                                    AutoRotate = true,
                                    DarkingSpeed = Rand(4f, 6f),
                                    SlowLerp = -0.008f
                                });
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat + bpm, 1, delegate ()
                            {
                                CreateEntity(new Particle(
                                        new(0, 255, 0),
                                       new(
                                           Cos(90) * 6,
                                           Sin(90) * 6),
                                       24,
                                       new(Rand(0, 640), -20), Sprites.soulCollide)
                                {
                                    AutoRotate = true,
                                    DarkingSpeed = Rand(4f, 6f),
                                    SlowLerp = -0.008f
                                });
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat + bpm * 2, 1, delegate ()
                            {
                                CreateEntity(new Particle(
                                        new(0, 255, 0),
                                       new(
                                           Cos(90) * 6,
                                           Sin(90) * 6),
                                      24,
                                       new(Rand(0, 640), -20), Sprites.soulCollide)
                                {
                                    AutoRotate = true,
                                    DarkingSpeed = Rand(4f, 6f),
                                    SlowLerp = -0.008f
                                });
                            }));
                        }
                        else if (arrow[i] == "Line")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, delegate ()
                            {
                                int way = Rand(0, 1) == 0 ? 1 : -1;
                                Extends.DrawingUtil.Linerotate line = new(Rand(0, 640), 480, 270 + Rand(-15, 15), bpm * 16, 0, new Color(71, 140, 147) * 0.3f);
                                CreateEntity(line);
                                float speed = 0;
                                float realspeed = Rand(5.6f, 7.5f);
                                AddInstance(new TimeRangedEvent(bpm * 16, () =>
                                {
                                    speed += 1 / bpm / 16;
                                    line.xCenter += realspeed * realspeed * way * speed;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 2, () =>
                                {
                                    line.alpha += 1 / bpm / 2;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat + bpm, 1, delegate ()
                            {
                                int way = Rand(0, 1) == 0 ? 1 : -1;
                                Extends.DrawingUtil.Linerotate line = new(Rand(0, 640), 480, 270 + Rand(-15, 15), bpm * 16, 0, new Color(71, 140, 147) * 0.3f);
                                CreateEntity(line);
                                float speed = 0;
                                float realspeed = Rand(5.6f, 7.5f);
                                AddInstance(new TimeRangedEvent(bpm * 16, () =>
                                {
                                    speed += 1 / bpm / 16;
                                    line.xCenter += realspeed * realspeed * way * speed;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 2, () =>
                                {
                                    line.alpha += 1 / bpm / 2;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat + bpm * 2, 1, delegate ()
                            {
                                int way = Rand(0, 1) == 0 ? 1 : -1;
                                Extends.DrawingUtil.Linerotate line = new(Rand(0, 640), 480, 270 + Rand(-15, 15), bpm * 16, 0, new Color(71, 140, 147) * 0.3f);
                                CreateEntity(line);
                                float speed = 0;
                                float realspeed = Rand(5.6f, 7.5f);
                                AddInstance(new TimeRangedEvent(bpm * 16, () =>
                                {
                                    speed += 1 / bpm / 16;
                                    line.xCenter += realspeed * realspeed * way * speed;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 2, () =>
                                {
                                    line.alpha += 1 / bpm / 2;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat + bpm * 3, 1, delegate ()
                            {
                                int way = Rand(0, 1) == 0 ? 1 : -1;
                                Extends.DrawingUtil.Linerotate line = new(Rand(0, 640), 480, 270 + Rand(-15, 15), bpm * 16, 0, new Color(71, 140, 147) * 0.3f);
                                CreateEntity(line);
                                float speed = 0;
                                float realspeed = Rand(5.6f, 7.5f);
                                AddInstance(new TimeRangedEvent(bpm * 16, () =>
                                {
                                    speed += 1 / bpm / 16;
                                    line.xCenter += realspeed * realspeed * way * speed;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 2, () =>
                                {
                                    line.alpha += 1 / bpm / 2;
                                }));
                            }));
                        }
                        else if (arrow[i] == "Shake")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 5, delegate ()
                            {
                                ScreenDrawing.ScreenPositionDetla = new(Rand(-2, 2), 0);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + 8 + beat, 1, delegate ()
                            {
                                ScreenDrawing.ScreenPositionDetla = new(0, 0);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 2 + beat, 5, delegate ()
                            {
                                ScreenDrawing.ScreenPositionDetla = new(Rand(-2, 2), 0);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 2 + 8 + beat, 1, delegate ()
                            {
                                ScreenDrawing.ScreenPositionDetla = new(0, 0);
                            }));
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "1", arrow[i][1] - '0', 13 * greencount));
                            AddInstance(new InstantEvent(bpm * 16 + beat, () =>
                            {
                                AddInstance(new TimeRangedEvent(bpm * 16, () =>
                                {
                                    ScreenDrawing.BackGroundColor = Color.Lerp(ScreenDrawing.BackGroundColor, Color.White, 0.08f);
                                    BarColor = Color.Lerp(BarColor, Color.White, 0.08f);
                                    ScreenDrawing.HPBar.HPExistColor = Color.Lerp(BarColor, Color.White, 0.08f);
                                    ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, Color.White, 0.08f);
                                }));
                                CreateEntity(new TextPrinter((int)(bpm * 180), "$$$R.I.P.", new(320 - 215, 40), new TextAttribute[]
                                                     {
                                      new TextSpeedAttribute(1444),
                                      new TextSizeAttribute(5),
                                      new TextColorAttribute(Color.Black)
                                                     }));

                                GreenSoulGB[] gb = GetAll<GreenSoulGB>();
                                for (int a = 0; a < gb.Length; a++)
                                {
                                    int x = a;
                                    gb[x].Dispose();
                                }
                            }));
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "3", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] == "($0)($0)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($0)($0)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($01)($01)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($01)($01)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($2)($2)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($2)($2)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($21)($21)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($21)($21)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 6f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                    float beatt = 0;
                    string[] Base =
                        {
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "R(R1)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",
                        "R(R1)","/","/","/",   "R(R1)","/","/","/",

                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",

                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",

                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",
                        "R","/","/","/",   "+0","/","/","/",

                       "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",

                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","/","/",   "/","/","/","/",

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
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
            };
                    for (int i = 0; i < Base.Length; i++)
                    {
                        if (Base[i] == "/")
                        {
                            beatt += bpm * 0.5f;
                        }
                        else if (Base[i][0] == 'S')
                        {
                            beatt += bpm * 0.5f;
                            CreateArrow(bpm * 16 + beatt, "R", 8f, Base[i][1] - '0', Base[i][2] - '0');
                        }
                        else if (Base[i][0] == 'G')
                        {
                            beatt += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beatt + bpm * 16, "R", Base[i][1] - '0', (Base[i][2] - '0') * greencount));
                        }
                        else if (Base[i] != "/")
                        {
                            if (!string.IsNullOrWhiteSpace(Base[i]))
                            {
                                GameObject[] arrows = instance.MakeArrows(bpm * 16 + beatt, 6f, Base[i]);
                                foreach (GameObject arr in arrows)
                                {
                                    AddInstance(arr);
                                }
                            }
                            beatt += bpm * 0.5f;
                        }
                    }
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, 114514, () =>
                    {
                        HeartRot = Sin(speed) * 10;
                        speed += 360 / bpm / 64;
                    }));
                    float beattt = 0;
                    string[] piano =
                        {
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R02)(+002)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "Line","(R02)(+002)","/","/","/",   "Line","(+002)(+002)","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R12)(+012)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(+012)(+012)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(+012)(+012)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R02)(+002)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(+002)(+002)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(+002)(+002)","/","/","/",   "/","/","/","/",

                        "(R12)(+012)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(+012)(+012)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(+012)(+012)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "Line","(R12)(+012)","/","/","/",   "Line","(+012)(+012)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R02)(+002)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R02)(+002)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "(R02)(+002)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(+002)(+002)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(+002)(+002)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

            };
                    for (int i = 0; i < piano.Length; i++)
                    {
                        if (piano[i] == "/")
                        {
                            beattt += bpm * 0.5f;
                        }
                        else if (piano[i][0] == 'S')
                        {
                            beattt += bpm * 0.5f;
                            CreateArrow(bpm * 16 + beattt, "R", 8f, piano[i][1] - '0', piano[i][2] - '0');
                        }
                        else if (piano[i][0] == 'G')
                        {
                            beattt += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beattt + bpm * 16, "R", piano[i][1] - '0', (piano[i][2] - '0') * greencount));
                        }
                        else if (piano[i] == "Line")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beattt, 1, () =>
                            {
                                int rot = Rand(-30, 30);
                                Extends.DrawingUtil.Linerotate line1 = new(320, 240, rot, bpm * 16, 0);
                                Extends.DrawingUtil.Linerotate line2 = new(320, 240, rot + 90, bpm * 16, 0);
                                Extends.DrawingUtil.Linerotate line3 = new(320, 240, rot + 180, bpm * 16, 0);
                                Extends.DrawingUtil.Linerotate line4 = new(320, 240, rot + 270, bpm * 16, 0);
                                CreateEntity(line1);
                                CreateEntity(line2);
                                CreateEntity(line3);
                                CreateEntity(line4);
                                float rotRand = Rand(-15, 15);
                                AddInstance(new TimeRangedEvent(bpm * 2, () =>
                                {
                                    line1.alpha += 1 / bpm / 2;
                                    line2.alpha += 1 / bpm / 2;
                                    line3.alpha += 1 / bpm / 2;
                                    line4.alpha += 1 / bpm / 2;
                                }));
                                float speed2 = 0;
                                AddInstance(new TimeRangedEvent(bpm * 32, () =>
                                {
                                    speed2 += 1 / bpm / 4;
                                    line1.xCenter += speed2 * 8 * 8;
                                    line2.yCenter -= speed2 * 8 * 8;
                                    line3.xCenter -= speed2 * 8 * 8;
                                    line4.yCenter += speed2 * 8 * 8;
                                }));
                            }));
                        }
                        else if (piano[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beattt, 6f, piano[i]);
                            beattt += bpm * 0.5f;
                        }
                    }
                }
            }
            private static class HardBarrage
            {
                public static void ArrowAtk1()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //1
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //2
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //3
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //4
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //5
                        "R","/","/","/",    "R0","/","/","/",    "R","/","/","/",    "R0","/","/","/",
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //6
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //7
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //8
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",

                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk2()
                {

                }
                public static void ArrowAtk3()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "$01","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        //
                        "R1","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R","+0","+0","+0",    "+201","+001","+001","+001",
                        //
                        "$01","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        //
                        "R1","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R","+0","+0","+0",    "+001","+001","+001","+001",
                        //
                        "$01","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        //
                        "R1","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R","+0","+0","+0",    "+201","+001","+001","+001",
                        //
                        "$01","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                    //float Beatt = 0;
                    /* string[] Bass =
                     {
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                     };
                     for (int i = 0; i < Bass.Length; i++)
                     {
                         if (Bass[i] == "/")
                         {
                             Beatt += 0.5f * bpm;
                         }
                         else if (Bass[i] != "/")
                         {
                             UndyneFight_Ex.Fight.AdvanceFunctions.CreateArrows(80 + Beatt, 6.85f, Bass[i]);
                             Beatt += 0.5f * bpm;
                         }
                     }*/
                }
                public static void ArrowAtk4()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($0)(+210)","/","(+2)(+210)","/",    "(+2)(+210)","/","(+2)(+210)","/"
                        };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk5()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "X","/","/","/",    "/","/","/","/",    "X","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            int way = Rand(0, 3);
                            int way2 = Rand(0, 3);
                            CreateArrow(80 + Beat, way, 7.5f, Rand(0, 1), 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat, way, 7.5f, LastRand, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat + bpm * 2, way, 7.5f, LastRand, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat + bpm * 2, way, 7.5f, LastRand, 0, ArrowAttribute.SpeedUp);
                            Beat += 0.5f * bpm;
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk6()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //
                        "(D)(+0)","/","/","+00(+00)",    "/","/","+00(+00)","/",     "/","(D)(+0)","/","/",    "+00(+00)","/","/","+00(+00)",
                        "/","/","(D)(+0)","/",    "/","+00(+00)","/","/",    "(+00)(+00)",
                        };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk7()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "$1($1)","/","/","/",
                        "$1($1)","/","/","/",    "/","/","/","/",    "$3($3)","/","/","/",    "G302","($3)","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",     "/","/","/","/",    "/","/","/","/",
                        "$21","/","/","/",    "$21","/","/","/",    "$21","/","/","/",    "$21","/","/","/",
                        //
                        "$01","/","/","/",    "+01","/","/","/",    "+01","/","/","/",    "$01","/","/","/",
                        "+01","/","/","/",    "+01","/","/","/",    "+01","/","/","/",    "G002","/","/","/","/",
                        //
                        "$0","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "(R)(+11)","/","/","/",    "(-1)(+11)","/","/","/",     "(R)(+11)","/","/","/",    "(-1)(+11)","/","/","/",
                        //
                        "$11","/","/","/", "$0","/","/","/", "/","/","/","/", "$31","/","/","/",
                        "$2","/","/","/", "/","/","/","/", "$0","/", "/","/", "$2","/","/","/",
                        //
                        "G202","/","/","/","/",    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "$111($111)","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/","/",    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/","/",    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",
                        //split
                        "/","/","/","/","/",    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/", "$301","/","/","/",    "+0","/","/","/",
                        //
                        "/","/","/","/",    "$20","/","/","/",    "+0","/","/","/",    "/","/","/","/",
                        "$30","/","/","/",    "+0","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //turn
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$20","/","/","/",    "+0","/","/","/",    "$3","/","/","/",    "$30","/","+0","/",

                        //split
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "$20","/","+0","/",
                        "D0","/","/","/",    "D0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        //
                        "+0","/","/","/",    "D","/","/","/",    "D","/","/","/",    "D","/","/","/",
                         "D","/","/","/",    "D","/","/","/",    "D","/","/","/",    "D0","/","+0","/",
                        //
                        "+0","/","/","/",    "(+0)","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","$100","/",    "/","/","/","/",    "/","/","/","/",   "(R1)","/",   "/","/","(R1)","/",
                        //
                        "/","/","(R1)","/",    "/","/","(R1)","/",    "/","/","(R1)","/","/","/",    "(R1)","/","/","/",
                        "G112","($11)","/","/","/",    "/","/","/","/",    "/","/","/","/",    
                        //
                        "/","/","/","/",    "/","/","/","/",    "R(R1)","/","/","/",    "/","/","/","/",
                        "R(R1)","/","/","/",    "/","/","/","/",   "/","/","/","/",    "/","/","/","/",
                        };
                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(Beat + 80, Arrow[i][1] - '0', Arrow[i][2] - '0', (Arrow[i][3] - '0') * greencount));
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk7_1()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //split
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",

                        "/","/","/","/",    "/","/","/","/",    "($01)($01)","/","/","/",    "($11)($11)","/","($01)($01)","/",
                        //turn
                        "($11)($11)","/","/","/",    "($11)(+01)","/","/","/",    "($11)(+01)","/","/","/",    "$01","/","/","/",
                        "$01","/","/","/",    "$01","/","/","/",    "$01","/","/","/",    "+01","/","/","/",
                        //
                        "+01","/","/","/",    "+01","/","/","/",    "$11","/","/","/",    "+01","/","/","/",
                        "$01","/","/","/",    "+01","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //split
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",

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
                            instance.CreateArrows(80 + Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk8()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "EF1","($1)($1)","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                        "($3)($3)","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                        "EF2","($1)($1)","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                        "($3)($3)","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                        "EF3","($1)($1)","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                        "($3)($3)","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                        "EF4","($1)($1)","/","/","/",   "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                        "($3)($3)","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] == "EF1")
                        {
                            AddInstance(new TimeRangedEvent(Beat, bpm * 16 - 2, () => { HeartRot = HeartRot * 0.97f + 130 * 0.03f; }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, bpm * 16 - 2, () => { HeartRot = HeartRot * 0.97f + 320 * 0.03f; }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.Linerotate line1 = new(320, 240, 15, bpm * 26, 1) { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.Linerotate line2 = new(320, 240, 11, bpm * 26, 0.8f) { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.Linerotate line3 = new(320, 240, 19, bpm * 26, 0.8f) { Tags = new string[] { "a" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                CreateEntity(line3);
                                AddInstance(new InstantEvent(1, () =>
                                {
                                    Extends.DrawingUtil.Linerotate[] lines = GetAll<Extends.DrawingUtil.Linerotate>("a");
                                    for (int a = 0; a < lines.Length; a++)
                                    {
                                        int x = a;
                                        AddInstance(new TimeRangedEvent(bpm * 64, () => { lines[x].alpha = lines[x].alpha * 0.975f + 0 * 0.025f; lines[x].rotate = lines[x].rotate * 0.95f - 8 * 0.05f; }));
                                    }
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () =>
                            {
                                Extends.DrawingUtil.Linerotate line1 = new(320, 240, 80, 800, 1) { Tags = new string[] { "b" } };
                                Extends.DrawingUtil.Linerotate line2 = new(320, 240, 75, 800, 0.8f) { Tags = new string[] { "b" } };
                                Extends.DrawingUtil.Linerotate line3 = new(320, 240, 85, 800, 0.8f) { Tags = new string[] { "b" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                CreateEntity(line3);
                                AddInstance(new InstantEvent(1, () =>
                                {
                                    Extends.DrawingUtil.Linerotate[] lines = GetAll<Extends.DrawingUtil.Linerotate>("b");
                                    for (int a = 0; a < lines.Length; a++)
                                    {
                                        int x = a;
                                        AddInstance(new TimeRangedEvent(bpm * 17, () => { lines[x].rotate = lines[x].rotate * 0.95f + 103 * 0.05f; }));
                                        AddInstance(new TimeRangedEvent(bpm * 32, () => { lines[x].alpha = lines[x].alpha * 0.95f + 0 * 0.05f; }));
                                    }
                                }));
                            }));
                            for (int a = 0; a < 16; a++)
                            {
                                AddInstance(new TimeRangedEvent(Beat + a * 5, 1, () =>
                                {
                                    for (int b = 0; b < 3; b++)
                                    {
                                        float rot = -Rand(90 - 8, 90 + 8);
                                        float speed = Rand(2f, 3.1f);
                                        CreateEntity(new Particle(
                                            new Color(241, 214, 91),
                                           new(
                                               Cos(rot) * speed,
                                               Sin(rot) * speed),
                                           Rand(16, 24),
                                           new(Rand(320 - 140, 320 + 140), 480), Sprites.square)
                                        {
                                            AutoRotate = true,
                                            SlowLerp = Rand(0.006f, 0.008f),
                                            DarkingSpeed = Rand(2.0f, 2.5f)
                                        });
                                    }
                                }));
                            }
                        }
                        else if (Arrow[i] == "EF2")
                        {
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(Beat, (int)bpm * 35f, () =>
                            {
                                sin += 180 / (int)bpm / 35f;
                                ScreenDrawing.ScreenScale = 1 + Sin(sin) * 5f;
                            }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () => { SetSoul(1); HeartRot = 180; }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () => { SetSoul(1); HeartRot = 0; }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.NormalLine line1 = new(
                                    0, 480,
                                    640,
                                    640,
                                    500, 1, Color.Red * 0.8f
                                    )
                                { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.NormalLine line2 = new(
                                   640,
                                   800,
                                   0,
                                   640,
                                    500, 1, Color.Red * 0.8f
                                    )
                                { Tags = new string[] { "a" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                float sin = 0;
                                AddInstance(new TimeRangedEvent(1, bpm * 32, () =>
                                {
                                    sin += 360 / bpm / 128;
                                    line1.y1 = 480 - Sin(sin) * 820;
                                    line1.y2 = 640 - Sin(sin) * 820;
                                    line2.y1 = 640 - Sin(sin) * 820;
                                    line2.y2 = 800 - Sin(sin) * 820;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () =>
                            {
                                AddInstance(new InstantEvent(2, () =>
                                {
                                    Extends.DrawingUtil.Linerotatelong[] Lastlines = GetAll<Extends.DrawingUtil.Linerotatelong>("b");
                                    for (int a = 0; a < Lastlines.Length; a++)
                                    {
                                        int x = a;
                                        Lastlines[x].Dispose();
                                    }

                                }));
                            }));
                            for (int a = 0; a < 10; a++)
                            {

                                AddInstance(new TimeRangedEvent(Beat + a * 5, 1, () =>
                                {
                                    for (int b = 0; b < 3; b++)
                                    {
                                        float rot = Rand(0, 360);
                                        float speed = Rand(0.8f, 1.1f);
                                        CreateEntity(new Particle(
                                            new Color(241, 214, 91),
                                           new(
                                               Cos(rot) * speed,
                                               Sin(rot) * speed),
                                           Rand(12, 18),
                                           Heart.Centre, Sprites.square)
                                        {
                                            AutoRotate = true,
                                            SlowLerp = Rand(0.006f, 0.008f),
                                            DarkingSpeed = Rand(2.0f, 2.5f)
                                        });
                                    }
                                }));
                            }
                        }
                        else if (Arrow[i] == "EF3")
                        {
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(Beat, bpm * 16, () =>
                            {
                                sin += 90 / bpm / 16;
                                ScreenDrawing.ScreenPositionDetla = new(-Sin(sin) * 750f, 0);
                            }));
                            float sin2 = 180;
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, bpm * 16, () =>
                            {
                                sin2 += 90 / bpm / 16;
                                ScreenDrawing.ScreenPositionDetla = new(640 + Sin(sin2) * 640f, 0);
                            }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.NormalLine line = new(320 - 80, 120, 320 + 80, 120, (int)(bpm * 48), 1, new(255, 255, 255, 125));
                                line.width = 0;
                                Extends.DrawingUtil.NormalLine line1 = new(320, 40, 320, 240 + 40, (int)(bpm * 48), 1, new(255, 255, 255, 125));
                                line1.width = 0;
                                CreateEntity(line);
                                CreateEntity(line1);
                                AddInstance(new TimeRangedEvent(bpm * 16, () =>
                                {
                                    line.width = line.width * 0.96f + 36 * 0.04f;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 16, bpm * 32, () =>
                                {
                                    line1.width = line1.width * 0.97f + 36 * 0.03f;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 28, bpm * 16, () =>
                                {
                                    line.color = Color.Lerp(line.color, new(0, 0, 0, 0), 0.05f);
                                    line1.color = Color.Lerp(line1.color, new(0, 0, 0, 0), 0.05f);
                                }));
                            }));
                        }
                        else if (Arrow[i] == "EF4")
                        {
                            AddInstance(new TimeRangedEvent(Beat, 1, () => { ScreenDrawing.CameraEffect.SizeExpand(100, bpm * 30); }));

                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);

                            instance.CreateArrows(Beat, 6f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void Phase2Arrow1()
                {
                    float beat = 0;
                    string[] arrow =
                        {

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        //"R","/","/","/",   "+0","/","/","/",

                       // "+1","/","/","/",   "/","/","/","/",
                        //"/","/","/","/",   "R1","/","/","/",
                        //"R1","/","/","R", "/","/","/","+0",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "S00","/","/","/",   "/","/","/","/",
                        "S10","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "M","EF1","(R1)(+01)(R)","/","/","/",
                        "R","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "M","D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "M","D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "M","D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","EF2","(R1)(+01)(R)","/","/","/",   "R","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "M","D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "M","D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",
                        "D","/","/","/",   "D","/","/","/",

                        "B","(R1)(+01)","/","/","/",   "(R)(+21)","/","/","/",
                        "(+2)(+21)","/","/","/",   "(+2)(+21)","/","/","/",
                        "(+2)(+21)","/","/","/",   "(+2)(+21)","/","/","/",
                        "(+2)(+21)","/","/","/",   "(+2)(+21)","/","/","/",

                        "(R)(+01)","/","(+0)(+01)","/",   "(R)(+01)","/","(+0)(+01)","/",
                        "(R)(+01)","/","(+0)(+01)","/",   "/","/","/","/",

                        "G303","G113"

            };

                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "EF1")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                for (int a = 0; a < 32; a++)
                                {
                                    float rot = Rand(0, 359);
                                    float speed = Rand(9f, 14.5f);
                                    CreateEntity(new Particle(
                                        mainEffects.BeautifulColor(),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(22, 26),
                                       new(320, 240), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(4.2f, 5.7f)
                                    });
                                }
                            }));
                        }
                        else if (arrow[i] == "EF2")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                for (int a = 0; a < 64; a++)
                                {
                                    float rot = Rand(270 - 30, 270 + 30);
                                    float speed = Rand(1f, 16f);
                                    CreateEntity(new Particle(
                                        new(129, 27, 50),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(18, 24),
                                       new(Rand(0, 640), 480), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(3f, 4.3f)
                                    });
                                }
                                for (int a = 0; a < 64; a++)
                                {
                                    float rot = Rand(90 - 30, 90 + 30);
                                    float speed = Rand(1f, 16f);
                                    CreateEntity(new Particle(
                                        new(62, 218, 228),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(18, 24),
                                       new(Rand(0, 640), 0), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(3f, 4.3f)
                                    });
                                }
                            }));
                        }
                        else if (arrow[i] == "M")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpGreenBox(bpm * 8, new(320 + Rand(-26, 26), 240 + Rand(-24, 24)), 0.01f);
                            }));
                        }
                        else if (arrow[i] == "B")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpGreenBox(bpm * 8, new(320, 240), 0.01f);
                            }));
                        }
                        else if (arrow[i] == "($0)($0)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($0)($0)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($01)($01)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($01)($01)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($2)($2)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($2)($2)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($21)($21)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($21)($21)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            beat += bpm * 0.5f;
                            CreateArrow(bpm * 16 + beat, "R", 8f, arrow[i][1] - '0', arrow[i][2] - '0');
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            beat += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beat + bpm * 16, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * greencount));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 6f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void FinalAttack()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "(R1)(+01)","/","+01","/",   "+01","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "(R1)(+01)","/","+01","/",   "+01","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",
                       "(R1)(+01)","/","+01","/",   "+01","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R1)(+01)","/","+01","/",   "+01","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",
                       "(R1)(+01)","/","+01","/",   "+01","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "(R1)(+01)","/","+01","/",   "+01","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",
                       "(R1)(+01)","/","+01","/",   "+01","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R1)(+01)","/","(+01)(+01)","/",   "(+01)(+01)","/","(R)(+01)","/",
                        "+0","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","(+01)(+01)","/",   "(+01)(+01)","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "(R1)(+01)","/","(+01)(+01)","/",   "(+01)","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","(+01)","/",   "(+01)","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "(R1)(+01)","/","(+01)(+01)","/",   "(+01)(+01)","/","(R)(+0)","/",
                        "(+0)(+0)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","(+01)(+01)","/",   "(+01)(+01)","/","(R)(+0)","/",
                        "(+0)(+0)","/","/","/",   "/","/","/","/",

                        "(R1)(+01)","/","(+01)(+01)","/",   "(+01)(+01)","/","(R)","/",
                        "(+0)","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","(+01)(+01)","/",   "(+01)","/","(R)","/",
                        "(+0)","/","/","/",   "/","/","/","/",

                        "(R)","/","/","/",   "(+21)","/","/","/",
                        "(R)","/","/","/",   "(+21)","/","/","/",
                        "(R)","/","/","/",   "(+21)","/","/","/",
                        "(R)","/","/","/",   "(+21)","/","/","/",

                        "(R)","/","/","/",   "(+21)","/","/","/",
                        "(R)","/","/","/",   "(+21)","/","/","/",
                        "(R)","/","/","/",   "(+21)","/","/","/",
                        "(R)","/","/","/",   "(+21)","/","/","/",

                        "($1)($01)($01)","/","/","/",   "($3)($21)($21)","/","/","/",
                        "($1)($01)($01)","/","/","/",   "($3)($21)($21)","/","/","/",
                        "($1)($01)($01)","/","/","/",   "($3)($21)($21)","/","/","/",
                        "($1)($01)($01)","/","/","/",   "($3)($21)($21)","/","/","/",

                        "(R)(+01)","/","(+0)(+01)","/",   "(R)(+01)","/","(+0)(+01)","/",
                        "(R)(+01)","/","(+0)(+01)","/",   "(R)(+01)","/","(+0)(+01)","/",
                        "($3)($3)","($11)($11)","($3)($3)","($11)($11)", "($3)($3)","($11)($11)","($3)($3)","($11)($11)",
                        "($3)($3)","($11)($11)","($3)($3)","($11)($11)", "($3)($3)","($11)($11)","($3)($3)","/",
                        "G08","S1"
            };

                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "1", arrow[i][1] - '0', 13 * greencount));
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "3", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] == "($3)($3)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($3)($3)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($11)($11)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 8f, "($11)($11)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($2)($2)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($2)($2)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($21)($21)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($21)($21)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 6f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                    float beatt = 0;
                    string[] Base =
                        {
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "R","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",
                        "R","/","/","/",   "/","/","/","/",
                        "R(R1)","/","/","/",   "/","/","/","/",

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

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
            };
                    for (int i = 0; i < Base.Length; i++)
                    {
                        if (Base[i] == "/")
                        {
                            beatt += bpm * 0.5f;
                        }
                        else if (Base[i][0] == 'S')
                        {
                            beatt += bpm * 0.5f;
                            CreateArrow(bpm * 16 + beatt, "R", 10f, Base[i][1] - '0', Base[i][2] - '0');
                        }
                        else if (Base[i][0] == 'G')
                        {
                            beatt += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beatt + bpm * 16, "R", Base[i][1] - '0', (Base[i][2] - '0') * greencount));
                        }
                        else if (Base[i] != "/")
                        {
                            if (!string.IsNullOrWhiteSpace(Base[i]))
                            {
                                GameObject[] arrows = instance.MakeArrows(bpm * 16 + beatt, 6f, Base[i]);
                                foreach (GameObject arr in arrows)
                                {
                                    AddInstance(arr);
                                }
                            }
                            beatt += bpm * 0.5f;
                        }
                    }
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, 114514, () =>
                    {
                        HeartRot = Sin(speed) * 10;
                        speed += 360 / bpm / 64;
                    }));
                    float beattt = 0;
                    string[] piano =
                        {
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)(+01)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R0)(+00)","/","/","/",   "(+00)(+00)","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R1)(+01)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(+00)(+01)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R0)(+01)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R0)(+01)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R0)(+01)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R0)(+01)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R0)(+01)","/","/","/",   "(R0)(+01)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R0)(+0)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R0)(+00)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "(R0)(+00)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(+00)(+00)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(+00)(+00)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

            };
                    for (int i = 0; i < piano.Length; i++)
                    {
                        if (piano[i] == "/")
                        {
                            beattt += bpm * 0.5f;
                        }
                        else if (piano[i][0] == 'S')
                        {
                            beattt += bpm * 0.5f;
                            CreateArrow(bpm * 16 + beattt, "R", 10f, piano[i][1] - '0', piano[i][2] - '0');
                        }
                        else if (piano[i][0] == 'G')
                        {
                            beattt += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beattt + bpm * 16, "R", piano[i][1] - '0', (piano[i][2] - '0') * greencount));
                        }
                        else if (piano[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beattt, 6f, piano[i]);
                            beattt += bpm * 0.5f;
                        }
                    }
                }
                /*
                 * for(int a=0;a<8;a++)CreateLine(...){Tags=new string[]{"a"}};
                 * Line lines = GetAll<Line>("a");
                 * float speedVec = Rand(1f,2.5f);
                 * float rot = Rand(0,359);
                 * for(int a=0;a<lines.Length;a++)
                 * {
                 * int x=a;
                 * game.DelayBeat(8, () => lines[x].Centre += GetVector2(speedVec,rot);});
                 * }
                 * game.DelayBeat(8, () => speedVec=speedVec*0.93f+0*0.07f);});
                 */
            }
            private static class EasyBarrage
            {
                public static void ArrowAtk1()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //1
                        "R","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                       "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                       //2
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //3
                        "R","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //4
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "/","/","/","/",    "/","/","/","/",   
                        //5
                        "R","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //6
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //7
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        //8
                        "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",
                        "R","/","/","/",    "R0","/","/","/",    "R0","/","/","/",    "R0","/","/","/",

                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 7.05f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk2()
                {

                }
                public static void ArrowAtk3()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "$0","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        //
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","+0","/",    "+001","/","+001","/",
                        //
                        "$0","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        //
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","+0","/",    "+001","/","+001","/",
                        //
                        "$0","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        //
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","+0","/",    "+001","/","+001","/",
                        //
                        "$0","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 6.85f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                    //float Beatt = 0;
                    /* string[] Bass =
                     {
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         //
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "$001","+001","+101","+001",    "+101","+001","+101","+001",
                         //
                         "($0)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                         "(R)(+212)","/","/","/",    "+000","/","/","/",    "(R)(+212)(+012)","/","/","/",    "+000","/","/","/",
                     };
                     for (int i = 0; i < Bass.Length; i++)
                     {
                         if (Bass[i] == "/")
                         {
                             Beatt += 0.5f * bpm;
                         }
                         else if (Bass[i] != "/")
                         {
                             UndyneFight_Ex.Fight.AdvanceFunctions.CreateArrows(80 + Beatt, 6.85f, Bass[i]);
                             Beatt += 0.5f * bpm;
                         }
                     }*/
                }
                public static void ArrowAtk4()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            instance.CreateArrows(80 + Beat, 7.55f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk5()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "X","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            int way = Rand(0, 3);
                            int way2 = Rand(0, 3);
                            CreateArrow(80 + Beat, way, 8, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat, way, 8, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat + bpm * 2, way, 8, 0, 0, ArrowAttribute.SpeedUp);
                            CreateArrow(80 + Beat + bpm * 2, way, 8, 0, 0, ArrowAttribute.SpeedUp);
                            Beat += 0.5f * bpm;
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk6()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/","/","/",
                        //
                        "(R)","/","/","+00",    "/","/","+00","/",     "/","(R)","/","/",    "+00","/","/","+00",
                        "/","/","(R)","/",    "/","+00","/","/",    "(+00)",
                        };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);
                            instance.CreateArrows(80 + Beat, 7.25f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk7()
                {
                    float Beat = 0;
                    string[] Arrow =
                        {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "$1","/","/","/",
                        "$1","/","/","/",    "/","/","/","/",    "$3","/","/","/",    "G302","/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",     "/","/","/","/",    "/","/","/","/",
                        "$2","/","/","/",    "/","/","/","/",    "$2","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "$2","/","/","/",    "$2","/","/","/",    "$2","/","/","/",
                        "/","/","/","/",    "$0","/","/","/",    "$0","/","/","/",    "G002","$0","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "(R)(+01)","/","/","/",    "(+0)(+01)","/","/","/",     "(R)(+01)","/","/","/",    "(+0)(+01)","/","/","/", 
                        //
                        "$0","/","/","/", "$0","/","/","/", "/","/","/","/", "$1","/","/","/",
                        "$3","/","/","/", "/","/","/","/", "$2","/", "/","/", "$2","/","/","/",
                        //
                        "G202","/","/","/","/",    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/","/",    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/","/",    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",
                        //split
                        "/","/","/","/","/",    "/","/","/","/",   "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/", "$3","/","/","/",    "+0","/","/","/",
                        //
                        "/","/","/","/",    "$20","/","/","/",    "+0","/","/","/",    "/","/","/","/",
                        "$30","/","/","/",    "+0","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //turn
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",

                        //split
                        "$2","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "$20","/","+0","/",
                        "$2","/","/","/",    "$3","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        //
                        "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "$2","/","/","/",
                         "D","/","/","/",    "D","/","/","/",    "D","/","/","/",    "D0","/","+0","/",
                        //
                        "+0","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",   "/","/",   "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/","/","/",    "/","/","/","/",
                        "G112","/","/","/","/",    "/","/","/","/",    "/","/","/","/",    
                        //
                        "/","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",   "/","/","/","/",    "/","/","/","/",
                        };
                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i][0] == 'G')
                        {
                            SetPlayerMission(0);
                            CreateGB(new GreenSoulGB(Beat + 80, Arrow[i][1] - '0', Arrow[i][2] - '0', (Arrow[i][3] - '0') * greencount));
                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);
                            instance.CreateArrows(80 + Beat, 7.15f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk7_1()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //split
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",

                        "/","/","/","/",    "/","/","/","/",    "($01)","/","/","/",    "($01)","/","($01)","/",
                        //turn
                        "($11)","/","/","/",    "($11)","/","/","/",    "($11)","/","/","/",    "$01","/","/","/",
                        "$01","/","/","/",    "$01","/","/","/",    "$01","/","/","/",    "+01","/","/","/",
                        //
                        "+01","/","/","/",    "+01","/","/","/",    "$11","/","/","/",    "+01","/","/","/",
                        "$01","/","/","/",    "+01","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //split
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",

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
                            instance.CreateArrows(80 + Beat, 7.15f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void ArrowAtk8()
                {
                    float Beat = 0;
                    string[] Arrow =
                    {
                        //
                        "EF1","($1)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($3)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "EF2","($1)","/","/","/",     "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($3)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "EF3","($1)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($3)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "EF4","($1)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",

                        "($3)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                    };
                    for (int i = 0; i < Arrow.Length; i++)
                    {
                        if (Arrow[i] == "/")
                        {
                            Beat += 0.5f * bpm;
                        }
                        else if (Arrow[i] == "EF1")
                        {
                            AddInstance(new TimeRangedEvent(Beat, bpm * 16 - 2, () => { HeartRot = HeartRot * 0.97f + 130 * 0.03f; }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, bpm * 16 - 2, () => { HeartRot = HeartRot * 0.97f + 320 * 0.03f; }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.Linerotate line1 = new(320, 240, 15, bpm * 26, 1) { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.Linerotate line2 = new(320, 240, 11, bpm * 26, 0.8f) { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.Linerotate line3 = new(320, 240, 19, bpm * 26, 0.8f) { Tags = new string[] { "a" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                CreateEntity(line3);
                                AddInstance(new InstantEvent(1, () =>
                                {
                                    Extends.DrawingUtil.Linerotate[] lines = GetAll<Extends.DrawingUtil.Linerotate>("a");
                                    for (int a = 0; a < lines.Length; a++)
                                    {
                                        int x = a;
                                        AddInstance(new TimeRangedEvent(bpm * 64, () => { lines[x].alpha = lines[x].alpha * 0.975f + 0 * 0.025f; lines[x].rotate = lines[x].rotate * 0.95f - 8 * 0.05f; }));
                                    }
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () =>
                            {
                                Extends.DrawingUtil.Linerotate line1 = new(320, 240, 80, 800, 1) { Tags = new string[] { "b" } };
                                Extends.DrawingUtil.Linerotate line2 = new(320, 240, 75, 800, 0.8f) { Tags = new string[] { "b" } };
                                Extends.DrawingUtil.Linerotate line3 = new(320, 240, 85, 800, 0.8f) { Tags = new string[] { "b" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                CreateEntity(line3);
                                AddInstance(new InstantEvent(1, () =>
                                {
                                    Extends.DrawingUtil.Linerotate[] lines = GetAll<Extends.DrawingUtil.Linerotate>("b");
                                    for (int a = 0; a < lines.Length; a++)
                                    {
                                        int x = a;
                                        AddInstance(new TimeRangedEvent(bpm * 17, () => { lines[x].rotate = lines[x].rotate * 0.95f + 103 * 0.05f; }));
                                        AddInstance(new TimeRangedEvent(bpm * 32, () => { lines[x].alpha = lines[x].alpha * 0.95f + 0 * 0.05f; }));
                                    }
                                }));
                            }));
                            for (int a = 0; a < 16; a++)
                            {
                                AddInstance(new TimeRangedEvent(Beat + a * 5, 1, () =>
                                {
                                    for (int b = 0; b < 3; b++)
                                    {
                                        float rot = -Rand(90 - 8, 90 + 8);
                                        float speed = Rand(2f, 3.1f);
                                        CreateEntity(new Particle(
                                            new Color(241, 214, 91),
                                           new(
                                               Cos(rot) * speed,
                                               Sin(rot) * speed),
                                           Rand(16, 24),
                                           new(Rand(320 - 140, 320 + 140), 480), Sprites.square)
                                        {
                                            AutoRotate = true,
                                            SlowLerp = Rand(0.006f, 0.008f),
                                            DarkingSpeed = Rand(2.0f, 2.5f)
                                        });
                                    }
                                }));
                            }
                        }
                        else if (Arrow[i] == "EF2")
                        {
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(Beat, (int)bpm * 35f, () =>
                            {
                                sin += 180 / (int)bpm / 35f;
                                ScreenDrawing.ScreenScale = 1 + Sin(sin) * 5f;
                            }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () => { SetSoul(1); HeartRot = 180; }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () => { SetSoul(1); HeartRot = 0; }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.NormalLine line1 = new(
                                    0, 480,
                                    640,
                                    640,
                                    500, 1, Color.Red * 0.8f
                                    )
                                { Tags = new string[] { "a" } };
                                Extends.DrawingUtil.NormalLine line2 = new(
                                   640,
                                   800,
                                   0,
                                   640,
                                    500, 1, Color.Red * 0.8f
                                    )
                                { Tags = new string[] { "a" } };
                                CreateEntity(line1);
                                CreateEntity(line2);
                                float sin = 0;
                                AddInstance(new TimeRangedEvent(1, bpm * 32, () =>
                                {
                                    sin += 360 / bpm / 128;
                                    line1.y1 = 480 - Sin(sin) * 820;
                                    line1.y2 = 640 - Sin(sin) * 820;
                                    line2.y1 = 640 - Sin(sin) * 820;
                                    line2.y2 = 800 - Sin(sin) * 820;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, 1, () =>
                            {
                                AddInstance(new InstantEvent(2, () =>
                                {
                                    Extends.DrawingUtil.Linerotatelong[] Lastlines = GetAll<Extends.DrawingUtil.Linerotatelong>("b");
                                    for (int a = 0; a < Lastlines.Length; a++)
                                    {
                                        int x = a;
                                        Lastlines[x].Dispose();
                                    }

                                }));
                            }));
                            for (int a = 0; a < 10; a++)
                            {

                                AddInstance(new TimeRangedEvent(Beat + a * 5, 1, () =>
                                {
                                    for (int b = 0; b < 3; b++)
                                    {
                                        float rot = Rand(0, 360);
                                        float speed = Rand(0.8f, 1.1f);
                                        CreateEntity(new Particle(
                                            new Color(241, 214, 91),
                                           new(
                                               Cos(rot) * speed,
                                               Sin(rot) * speed),
                                           Rand(12, 18),
                                           Heart.Centre, Sprites.square)
                                        {
                                            AutoRotate = true,
                                            SlowLerp = Rand(0.006f, 0.008f),
                                            DarkingSpeed = Rand(2.0f, 2.5f)
                                        });
                                    }
                                }));
                            }
                        }
                        else if (Arrow[i] == "EF3")
                        {
                            float sin = 0;
                            AddInstance(new TimeRangedEvent(Beat, bpm * 16, () =>
                            {
                                sin += 90 / bpm / 16;
                                ScreenDrawing.ScreenPositionDetla = new(-Sin(sin) * 750f, 0);
                            }));
                            float sin2 = 180;
                            AddInstance(new TimeRangedEvent(bpm * 16 + Beat, bpm * 16, () =>
                            {
                                sin2 += 90 / bpm / 16;
                                ScreenDrawing.ScreenPositionDetla = new(640 + Sin(sin2) * 640f, 0);
                            }));
                            AddInstance(new TimeRangedEvent(Beat, 1, () =>
                            {
                                Extends.DrawingUtil.NormalLine line = new(320 - 80, 120, 320 + 80, 120, (int)(bpm * 48), 1, new(255, 255, 255, 125));
                                line.width = 0;
                                Extends.DrawingUtil.NormalLine line1 = new(320, 40, 320, 240 + 40, (int)(bpm * 48), 1, new(255, 255, 255, 125));
                                line1.width = 0;
                                CreateEntity(line);
                                CreateEntity(line1);
                                AddInstance(new TimeRangedEvent(bpm * 16, () =>
                                {
                                    line.width = line.width * 0.96f + 36 * 0.04f;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 16, bpm * 32, () =>
                                {
                                    line1.width = line1.width * 0.97f + 36 * 0.03f;
                                }));
                                AddInstance(new TimeRangedEvent(bpm * 28, bpm * 16, () =>
                                {
                                    line.color = Color.Lerp(line.color, new(0, 0, 0, 0), 0.05f);
                                    line1.color = Color.Lerp(line1.color, new(0, 0, 0, 0), 0.05f);
                                }));
                            }));
                        }
                        else if (Arrow[i] == "EF4")
                        {
                            AddInstance(new TimeRangedEvent(Beat, 1, () => { ScreenDrawing.CameraEffect.SizeExpand(100, bpm * 30); }));

                        }
                        else if (Arrow[i] != "/")
                        {
                            SetPlayerMission(0);

                            instance.CreateArrows(Beat, 7.25f, Arrow[i]);
                            Beat += 0.5f * bpm;
                        }
                    }
                }
                public static void Phase2Arrow1()
                {
                    float beat = 0;
                    string[] arrow =
                        {

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        //"R","/","/","/",   "+0","/","/","/",

                       // "+1","/","/","/",   "/","/","/","/",
                        //"/","/","/","/",   "R1","/","/","/",
                        //"R1","/","/","R", "/","/","/","+0",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "S00","/","/","/",   "/","/","/","/",
                        "S00","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "M","EF1","(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "M","(R1)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "M","(R1)","/","/","/",   "(R1)","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "M","(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "M","(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",

                        "M","(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "M","(R1)","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "EF2","(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "M","(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "(R1)","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "(R1)","/","/","/",

                        "B","R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",

                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",
                        "R","/","/","/",   "R","/","/","/",

            };

                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "EF1")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                for (int a = 0; a < 32; a++)
                                {
                                    float rot = Rand(0, 359);
                                    float speed = Rand(9f, 14.5f);
                                    CreateEntity(new Particle(
                                        mainEffects.BeautifulColor(),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(22, 26),
                                       new(320, 240), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(4.2f, 5.7f)
                                    });
                                }
                            }));
                        }
                        else if (arrow[i] == "EF2")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                for (int a = 0; a < 64; a++)
                                {
                                    float rot = Rand(270 - 30, 270 + 30);
                                    float speed = Rand(1f, 16f);
                                    CreateEntity(new Particle(
                                        new(129, 27, 50),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(18, 24),
                                       new(Rand(0, 640), 480), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(3f, 4.3f)
                                    });
                                }
                                for (int a = 0; a < 64; a++)
                                {
                                    float rot = Rand(90 - 30, 90 + 30);
                                    float speed = Rand(1f, 16f);
                                    CreateEntity(new Particle(
                                        new(62, 218, 228),
                                       new(
                                           Cos(rot) * speed,
                                           Sin(rot) * speed),
                                       Rand(18, 24),
                                       new(Rand(0, 640), 0), Sprites.square)
                                    {
                                        AutoRotate = true,
                                        SlowLerp = Rand(0.05f, 0.06f),
                                        DarkingSpeed = Rand(3f, 4.3f)
                                    });
                                }
                            }));
                        }
                        else if (arrow[i] == "M")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpGreenBox(bpm * 8, new(320 + Rand(-26, 26), 240 + Rand(-24, 24)), 0.01f);
                            }));
                        }
                        else if (arrow[i] == "B")
                        {
                            AddInstance(new TimeRangedEvent(bpm * 16 + beat, 1, () =>
                            {
                                Extends.DrawingUtil.LerpGreenBox(bpm * 8, new(320, 240), 0.01f);
                            }));
                        }
                        else if (arrow[i] == "($0)($0)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($0)($0)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($01)($01)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($01)($01)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($2)($2)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($2)($2)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($21)($21)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($21)($21)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            beat += bpm * 0.5f;
                            CreateArrow(bpm * 16 + beat, "R", 10f, arrow[i][1] - '0', arrow[i][2] - '0');
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            beat += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beat + bpm * 16, arrow[i][1] - '0', arrow[i][2] - '0', (arrow[i][3] - '0') * greencount));
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 6.7f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void FinalAttack()
                {
                    float beat = 0;
                    string[] arrow =
                        {
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                       "(R)","/","+0","/",   "+0","/","/","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "(R)","/","+0","/",   "+0","/","/","/",
                        "+0","/","/","/",   "/","/","/","/",
                       "(R)","/","+0","/",   "+0","/","/","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R)","/","+0","/",   "+0","/","/","/",
                        "+0","/","/","/",   "/","/","/","/",
                       "(R)","/","+0","/",   "+0","/","/","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "(R)","/","+0","/",   "+0","/","/","/",
                        "+0","/","/","/",   "/","/","/","/",
                       "(R)","/","+0","/",   "+0","/","/","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R)(+0)","/","(+0)(+0)","/",   "(+0)(+0)","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","(+0)(+0)","/",   "(+0)","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "(R)(+0)","/","(+0)","/",   "(+0)","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","(+0)","/",   "(+0)","/","R","/",
                        "+0","/","/","/",   "/","/","/","/",

                        "(R)(+0)","/","(+0)(+0)","/",   "(+0)(+0)","/","(R)(+0)","/",
                        "(+0)(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","(+0)(+0)","/",   "(+0)(+0)","/","(R)(+0)","/",
                        "(+0)","/","/","/",   "/","/","/","/",

                        "(R)(+0)","/","(+0)(+0)","/",   "(+0)(+0)","/","(R)","/",
                        "(+0)","/","/","/",   "/","/","/","/",
                        "(R)(+0)","/","(+0)(+0)","/",   "(+0)","/","(R)","/",
                        "(+0)","/","/","/",   "/","/","/","/",

                        "(R)","/","/","/",   "(+2)","/","/","/",
                        "(R)","/","/","/",   "(+2)","/","/","/",
                        "(R)","/","/","/",   "(+2)","/","/","/",
                        "(R)","/","/","/",   "(+2)","/","/","/",

                        "(R)","/","/","/",   "(+2)","/","/","/",
                        "(R)","/","/","/",   "(+2)","/","/","/",
                        "(R)","/","/","/",   "(+2)","/","/","/",
                        "(R)","/","/","/",   "(+2)","/","/","/",

                        "($1)($1)","/","/","/",   "($3)($3)","/","/","/",
                        "($1)($1)","/","/","/",   "($3)($3)","/","/","/",
                        "($1)($1)","/","/","/",   "($3)($3)","/","/","/",
                        "($1)($1)","/","/","/",   "($3)($3)","/","/","/",

                        "(R)","/","(+0)","/",   "(R)","/","(+0)","/",
                        "(R)","/","(+0)","/",   "(R)","/","(+0)","/",
                        "($3)","/","($3)","/", "($3)","/","($3)","/",
                        "($3)","/","($3)","/", "($3)","/","($3)","/",
                        "G08",
            };

                    float greencount = bpm * 4 - 5;
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/")
                        {
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i][0] == 'S')
                        {
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "1", arrow[i][1] - '0', 13 * greencount));
                        }
                        else if (arrow[i][0] == 'G')
                        {
                            CreateGB(new GreenSoulGB(beat + bpm * 16, "3", arrow[i][1] - '0', (arrow[i][2] - '0') * greencount));
                        }
                        else if (arrow[i] == "($0)($0)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($0)($0)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($01)($01)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($01)($01)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($2)($2)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($2)($2)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] == "($21)($21)")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 12.5f, "($21)($21)");
                            beat += bpm * 0.5f;
                        }
                        else if (arrow[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beat, 7.7f, arrow[i]);
                            beat += bpm * 0.5f;
                        }
                    }
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16, 114514, () =>
                    {
                        HeartRot = Sin(speed) * 10;
                        speed += 360 / bpm / 64;
                    }));
                    float beattt = 0;
                    string[] piano =
                        {
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
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "(R1)","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "(R1)","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "(R1)","/","/","/",   "(R1)","/","/","/",
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
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",
                        "/","/","/","/",   "/","/","/","/",

            };
                    for (int i = 0; i < piano.Length; i++)
                    {
                        if (piano[i] == "/")
                        {
                            beattt += bpm * 0.5f;
                        }
                        else if (piano[i][0] == 'S')
                        {
                            beattt += bpm * 0.5f;
                            CreateArrow(bpm * 16 + beattt, "R", 10f, piano[i][1] - '0', piano[i][2] - '0');
                        }
                        else if (piano[i][0] == 'G')
                        {
                            beattt += bpm * 0.5f;
                            CreateGB(new GreenSoulGB(beattt + bpm * 16, "R", piano[i][1] - '0', (piano[i][2] - '0') * greencount));
                        }
                        else if (piano[i] != "/")
                        {
                            instance.CreateArrows(bpm * 16 + beattt, 6f, piano[i]);
                            beattt += bpm * 0.5f;
                        }
                    }
                }
                /*
                 * for(int a=0;a<8;a++)CreateLine(...){Tags=new string[]{"a"}};
                 * Line lines = GetAll<Line>("a");
                 * float speedVec = Rand(1f,2.5f);
                 * float rot = Rand(0,359);
                 * for(int a=0;a<lines.Length;a++)
                 * {
                 * int x=a;
                 * game.DelayBeat(8, () => lines[x].Centre += GetVector2(speedVec,rot);});
                 * }
                 * game.DelayBeat(8, () => speedVec=speedVec*0.93f+0*0.07f);});
                 */
            }
            #region 没用的
            public void Noob()
            {

            }
            public void Extreme()
            {

            }
            public void Normal()
            {

            }
            #endregion
            public void ExtremePlus()
            {

                Effect();
                #region 开头目害段
                if (GametimeF > (int)(8 * 16 * bpm) && GametimeF < (int)(14 * 16 * bpm))
                {
                    InstantSetBox(Sin(1.25f * (GametimeF - (int)(8 * 16 * bpm + 32 * bpm))) * 25 + 230, 84, 84);
                    InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                }
                if (GametimeF == (int)(8 * 16 * bpm) - 80)
                {
                    ExBarrage.ArrowAtk1();
                    ExBarrage.ArrowAtk2();
                }
                if (GametimeF == (int)(16 * 16 * bpm) - 80)
                {
                    AddInstance(new TimeRangedEvent(bpm * 16, () => { HeartRot = 0 * 0.05f + HeartRot * 0.95f; }));
                    ExBarrage.ArrowAtk3();
                }
                if (GametimeF == (int)(23 * 16 * bpm) - 80)
                {
                    ExBarrage.ArrowAtk4();
                    ExBarrage.ArrowAtk5();
                }
                if (GametimeF == (int)(24 * 16 * bpm) - 80)
                {
                    ExBarrage.ArrowAtk3();
                }
                if (GametimeF == (int)(30 * 16 * bpm) - 80)
                {
                    SetPlayerMission(0);
                    ExBarrage.ArrowAtk6();
                }
                if (GametimeF == (int)(32 * 16 * bpm) - 80)
                {
                    Heart.Split();
                    SetBoxMission(1);
                    InstantSetBox(new Vector2(-1000, -1000), 84f, 84f);
                    SetPlayerMission(1);
                    ExBarrage.ArrowAtk7_1();
                    InstantTP(-10000, -10000);
                    SetPlayerMission(0);
                    ExBarrage.ArrowAtk7();
                    SetBoxMission(0);
                }
                if (GametimeF == (int)(32 * 16 * bpm))
                {
                    AddInstance(new TimeRangedEvent((int)(11 * 16 * bpm + 12 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320, 240), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320, 240), 84f, 84f);
                        SetPlayerMission(0);
                        Player.hearts[1].Teleport(new(320, 240));
                        Player.hearts[1].Merge(Player.hearts[0]);
                        SetPlayerMission(0);
                        TP();
                        SetPlayerMission(0);
                    }));
                    AddInstance(new TimeRangedEvent((int)(10 * 16 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320 + 126, 240 + 84), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320 - 126, 240 - 84), 84f, 84f);
                        SetPlayerMission(0);
                        TP(320 + 126, 240 + 84);
                        SetSoul(1);
                        SetPlayerMission(1);
                        TP(320 - 126, 240 - 84);
                        SetSoul(1);
                        SetBoxMission(0);
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 - 126, 240), 84f, 84f);
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320 + 126, 240), 84f, 84f);
                        SetPlayerMission(0);
                        InstantTP(320 - 126, 240);
                        SetSoul(1);
                        SetPlayerMission(1);
                        InstantTP(320 + 126, 240);
                        SetSoul(1);
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) + 1, 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320 - 126, 240 - 84), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320 + 126, 240 + 84), 84f, 84f);
                        SetPlayerMission(0);
                        TP(320 - 126, 240 - 84);
                        SetSoul(1);
                        SetPlayerMission(1);
                        TP(320 + 126, 240 + 84);
                        SetSoul(1);
                        SetBoxMission(0);
                    }));
                }
                if (GametimeF == (int)(32 * 16 * bpm))
                {
                    SetBoxMission(0);
                    SetBox(380, 84, 84);
                    SetPlayerMission(0);
                    InstantTP(new(320, 380));
                }
                if (GametimeF == (int)(32 * 16 * bpm + bpm * 6))
                {
                    ScreenDrawing.ScreenAngle = 180;
                }

                if (GametimeF == (int)(37 * 16 * bpm + 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$0", 1, (int)(10 * bpm)));
                    mainEffects.DrawLine.NormalLine l = new(0, 120, 640, 120, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(39 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "D", 1, (int)(6 * bpm)));
                    mainEffects.DrawLine.NormalLine l = new(0, 360, 640, 360, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(39 * 16 * bpm + 8 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "D", 0, (int)(6 * bpm)));
                    mainEffects.DrawLine.NormalLine l = new(320, 0, 320, 480, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(40 * 16 * bpm) - 80)
                {
                    SetBoxMission(1);
                    CreateEntity(new GreenSoulGB(80, 0, 1, (int)(10 * bpm)));
                }
                if (GametimeF == (int)(42 * 16 * bpm) - 80)
                {
                    SetBoxMission(0);
                    CreateEntity(new GreenSoulGB(80, 2, 0, (int)(10 * bpm)));
                }
                if (GametimeF == (int)(45 * 16 * bpm + 8 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, 1, 0, (int)(4 * bpm)));
                    CreateEntity(new GreenSoulGB(80, 3, 1, (int)(4 * bpm)));
                }
                if (GametimeF == (int)(46 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$3", 0, (int)(22 * bpm)));
                }
                #endregion
                if (GameStates.IsKeyPressed(InputIdentity.Heal))
                {
                    Regenerate();
                    PlaySound(Sounds.heal);
                }
                if (GametimeF >= (int)(48 * 16 * bpm) && GametimeF <= (int)(56 * 16 * bpm) && GametimeF % 9 == 0)
                {
                    SetPlayerMission(0);
                    float rot = Rand(0, 360);
                    float speed = Rand(1f, 2.1f);
                    CreateEntity(new Particle(
                        new Color(241, 214, 91) * 0.5f,
                       new(
                           Cos(rot) * speed,
                           Sin(rot) * speed),
                       Rand(11, 14.5f),
                       Heart.Centre, Sprites.square)
                    {
                        AutoRotate = true,
                        SlowLerp = Rand(0.03f, 0.04f),
                        DarkingSpeed = Rand(4.3f, 5.5f)
                    });
                    SetPlayerMission(1);
                    float rot1 = Rand(0, 360);
                    float speed1 = Rand(1f, 2.1f);
                    CreateEntity(new Particle(
                        new Color(241, 214, 91) * 0.5f,
                       new(
                           Cos(rot1) * speed1,
                           Sin(rot1) * speed1),
                       Rand(11, 14.5f),
                       Heart.Centre, Sprites.square)
                    {
                        AutoRotate = true,
                        SlowLerp = Rand(0.03f, 0.04f),
                        DarkingSpeed = Rand(4.3f, 5.5f)
                    });
                }
                if (GametimeF == (int)(56 * 16 * bpm))
                {
                    ExBarrage.ArrowAtk8();
                }
                if (GametimeF == (int)(64 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$0", 0, (int)(28 * bpm)));
                    CreateArrow(80, "+0", 8, 1, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + bpm * 18, "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 18, () => { mainEffects.ChangebarColor(); }));
                    float sin = 0;
                    AddInstance(new TimeRangedEvent(80, bpm * 16 * 6 - 5, () =>
                    {
                        sin += 1.05f;
                        ScreenDrawing.ScreenAngle = Sin(sin) * 6.5f;
                    }));
                    float asin = 0;
                    AddInstance(new TimeRangedEvent(80, bpm * 16 * 6, () =>
                    {
                        asin += 360 / bpm / 48;
                        ScreenDrawing.ScreenAngle = Sin(asin) * 6.5f;
                        InstantTP(320 + Sin(asin * 0.5f) * 22f, 240 + Sin(asin * 0.5f) * 17f);
                        InstantSetBox(new Vector2(320 + Sin(asin * 0.5f) * 22f, 240 + Sin(asin * 0.5f) * 17f), 84, 84);
                        HeartRot = Sin(asin * 0.5f) * 13f;
                        ScreenDrawing.BackGroundColor = new((int)(Sin(asin * 3 / 2) * 38), (int)(Sin(asin * 3 / 2) * 15), (int)(Sin(asin * 3 / 2) * 16));
                        //
                        //
                        //217,160,96
                    }));
                    for (int a = 0; a < bpm * 16 * 8 / bpm / 2; a++)
                    {
                        AddInstance(new TimeRangedEvent(80 + a * bpm * 2, 1, () =>
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                float rot1 = Rand(0, 360);
                                float speed1 = Rand(1f, 1.4f);
                                CreateEntity(new Particle(
                                    new Color(241, 214, 91) * 0.5f,
                                   new(
                                       Cos(rot1) * speed1,
                                       Sin(rot1) * speed1),
                                   Rand(18, 22f),
                                   Heart.Centre, Sprites.square)
                                {
                                    AutoRotate = true,
                                    SlowLerp = -Rand(0.040f, 0.050f),
                                    DarkingSpeed = Rand(1.7f, 2.4f)
                                });
                            }
                        }));
                    }
                }
                if (GametimeF == (int)(66 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$2", 0, (int)(28 * bpm)));
                    CreateArrow(80 + bpm * 4, "+0", 8, 1, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + bpm * 20, "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80 + bpm * 4, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 20, () => { mainEffects.ChangebarColor(); }));
                }
                if (GametimeF == (int)(68 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "D", 0, (int)(28 * bpm)));
                    CreateArrow(80 + (int)(bpm * 8), "+0", 8, 1, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + (int)(bpm * 24), "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80 + bpm * 8, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 24, () => { mainEffects.ChangebarColor(); }));
                }
                if (GametimeF == (int)(70 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, 2, 0, (int)(7 * 16 * bpm)));
                    CreateArrow(80 + bpm * 12, 2, 10, 1, 0, ArrowAttribute.SpeedUp);
                    CreateEntity(new GreenSoulGB(80 + bpm * 18, 0, 1, (int)(6 * 16 * bpm)));
                    AddInstance(new InstantEvent(80 + bpm * 12, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 18, () => { mainEffects.ChangebarColor2(); }));
                    for (int a = 0; a < 4; a++)
                    {
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 0);
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 0);
                    }
                    for (int a = 0; a < 4; a++)
                    {
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 2);
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 2);
                    }
                    AddInstance(new InstantEvent(bpm * 18 + 80, () =>
                    {
                        Arrow[] arr = GetAll<Arrow>();
                        for (int a = 0; a < arr.Length; a++)
                        {
                            int x = a;
                            arr[x].Stop(1145);
                        }
                    }));
                }
                if (GametimeF == (int)(70 * 16 * bpm))
                {
                    mainEffects.ScreenAngleNeo(-360, (int)(6 * 16 * bpm));
                }
                if (GametimeF > (int)(70 * 16 * bpm) && GametimeF < (int)(76 * 16 * bpm))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1 * 0.1f;
                }
                for (int i = 0; i < 15; i++)
                {
                    if (GametimeF == (int)(64 * 16 * bpm) + i * (int)(12 * bpm))
                    {
                        int a = 0;
                        mainEffects.DrawLine.Linerotate l1 = new(320, 240, 0, 300, 0, Color.White, 0.1f);
                        CreateEntity(l1);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l1.xCenter = 320 + a * a * 0.01f;
                            a++;
                            l1.alpha += 0.002f;
                            l1.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 3) + i * (int)(12 * bpm))
                    {
                        int b = 0;
                        mainEffects.DrawLine.Linerotate l2 = new(320, 240, 90, 300, 0, Color.White, 0.1f);
                        CreateEntity(l2);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l2.yCenter = 240 - b * b * 0.01f;
                            b++;
                            l2.alpha += 0.002f;
                            l2.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 6) + i * (int)(12 * bpm))
                    {
                        int c = 0;
                        mainEffects.DrawLine.Linerotate l3 = new(320, 240, 0, 300, 0, Color.White, 0.1f);
                        CreateEntity(l3);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l3.xCenter = 320 - c * c * 0.01f;
                            c++;
                            l3.alpha += 0.002f;
                            l3.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 9) + i * (int)(12 * bpm))
                    {
                        int d = 0;
                        mainEffects.DrawLine.Linerotate l4 = new(320, 240, 90, 300, 0, Color.White, 0.1f);
                        CreateEntity(l4);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l4.yCenter = 240 + d * d * 0.01f;
                            d++;
                            l4.alpha += 0.002f;
                            l4.rotate -= 0.5f;
                        }));
                    }
                }
                if (InBeat(76 * 4))
                {
                    mainEffects.DrawLine.Linerotate[] dis = GetAll<mainEffects.DrawLine.Linerotate>();
                    for (int a = 0; a < dis.Length; a++)
                    {
                        int x = a;
                        dis[x].Dispose();
                    }
                    CreateEntity(new mainEffects.MaskSquare(430, 0, 640 - 430, 130, 80, Color.Black, 1));
                    Extends.DrawingUtil.BetterBlackScreen(1, bpm * 32, bpm * 20, Color.Black);
                    Arrow[] arr = GetAll<Arrow>();
                    for (int a = 0; a < arr.Length; a++)
                    {
                        int x = a;
                        arr[x].Dispose();
                    }
                    GreenSoulGB[] gb = GetAll<GreenSoulGB>();
                    for (int a = 0; a < gb.Length; a++)
                    {
                        int x = a;
                        gb[x].Dispose();
                    }

                }
                if (InBeat(76 * 4))
                {
                    ExBarrage.Phase2Arrow1();
                    TP();
                    SetBox(240, 84, 84);
                }
                if (InBeat(76 * 4 + 16f))
                {
                    AddInstance(new TimeRangedEvent(50, () =>
                    {
                        BarColor = Color.Lerp(BarColor, new Color(120, 115, 115), 0.1f);
                        BarColor = Color.Lerp(BarColor, Color.Gray, 0.1f);
                        ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, BarColor, 0.1f);
                        ScreenDrawing.HPBar.AreaOccupied = new CollideRect(25 * 0.08f + ScreenDrawing.HPBar.AreaOccupied.X * 0.92f, 140, 20, 180);
                    }));

                }
                if (GametimeF == (int)BeatTime(76 * 4 + 16) + 51)
                {
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(BeatTime(94 * 4 - 76 * 4 - 16 + 8) - 50, () =>
                    {
                        speed += 360 / bpm / 28;
                        BarColor = new Color(225 - 60 + (int)(Sin(speed) * 60f), 115 + (int)(Sin(speed) * 110f), 115 + (int)(Sin(speed) * 110));
                    }));
                    AddInstance(new TimeRangedEvent(BeatTime(94 * 4 - 76 * 4 - 8) - 49, 3000, () =>
                    {
                        speed += 360 / bpm / 28;
                        BarColor = new Color(135 + (int)(Sin(speed) * 50f), 155 - (int)(Sin(speed) * 10f), 200 + (int)(Sin(speed) * 55f));
                        ScreenDrawing.UIColor = BarColor;
                    }));
                }
                if (InBeat(96 * 4 - 1)) { ScreenDrawing.WhiteOut(bpm * 6); }
                if (InBeat(96 * 4 - 4)) ExBarrage.FinalAttack();
                if (GameStates.IsKeyPressed120f(InputIdentity.Alternate))
                {
                    CreateEntity(new TextPrinter(114514, "$" + (GametimeF / bpm / 16).ToString(), beatVector, new TextSizeAttribute(0.5f)));
                    beatVector += new Vector2(0, 15);
                }
                if (beatVector.Y >= 380)
                {
                    beatVector.X += 75;
                    beatVector.Y = 100;
                }
                //CreateEntity(new TextPrinter(1, (GametimeF / bpm / 4).ToString(), Heart.Centre + new Vector2(-50, -50), new TextSizeAttribute(0.5f)));
                ScreenDrawing.HPBar.HPExistColor = BarColor;
                Heart.InstantSetRotation(HeartRot);
                //Functions.ScreenDrawing.HPBar.HPLoseColor = LoseColor;
                //CreateEntity(new TextPrinter(1, (GametimeF / BeatTime(1)).ToString(), new(320, 240), new TextAttribute[] { }));
                AdvanceFunctions.Interactive.AddMissEvent(() =>
                {
                    if (abool)
                    {
                        abool = false;
                        AddInstance(new TimeRangedEvent(55, () => { hit++; if (hit % 55 == 0) abool = true; }));
                        mainEffects.HPlerp();
                    }
                });
            }
            public void Hard()
            {
                Effect();
                #region 开头目害段
                if (GametimeF > (int)(8 * 16 * bpm) && GametimeF < (int)(14 * 16 * bpm))
                {
                    InstantSetBox(Sin(1.25f * (GametimeF - (int)(8 * 16 * bpm + 32 * bpm))) * 15 + 230, 84, 84);
                    InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                }
                if (GametimeF == (int)(8 * 16 * bpm) - 80)
                {
                    HardBarrage.ArrowAtk1();
                    HardBarrage.ArrowAtk2();
                }
                if (GametimeF == (int)(16 * 16 * bpm) - 80)
                {
                    AddInstance(new TimeRangedEvent(bpm * 16, () => { HeartRot = 0 * 0.05f + HeartRot * 0.95f; }));
                    HardBarrage.ArrowAtk3();
                }
                if (GametimeF == (int)(23 * 16 * bpm) - 80)
                {
                    HardBarrage.ArrowAtk4();
                    HardBarrage.ArrowAtk5();
                }
                if (GametimeF == (int)(24 * 16 * bpm) - 80)
                {
                    HardBarrage.ArrowAtk3();
                }
                if (GametimeF == (int)(30 * 16 * bpm) - 80)
                {
                    SetPlayerMission(0);
                    HardBarrage.ArrowAtk6();
                }
                if (GametimeF == (int)(32 * 16 * bpm) - 80)
                {
                    Heart.Split();
                    SetBoxMission(1);
                    InstantSetBox(new Vector2(-1000, -1000), 84f, 84f);
                    SetPlayerMission(1);
                    HardBarrage.ArrowAtk7_1();
                    InstantTP(-10000, -10000);
                    SetPlayerMission(0);
                    HardBarrage.ArrowAtk7();
                    SetBoxMission(0);
                }
                if (GametimeF == (int)(32 * 16 * bpm))
                {
                    AddInstance(new TimeRangedEvent((int)(11 * 16 * bpm + 12 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320, 240), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320, 240), 84f, 84f);
                        SetPlayerMission(0);
                        Player.hearts[1].Teleport(new(320, 240));
                        Player.hearts[1].Merge(Player.hearts[0]);
                        SetPlayerMission(0);
                        TP();
                        SetPlayerMission(0);
                    }));
                    AddInstance(new TimeRangedEvent((int)(10 * 16 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320 + 126, 240 + 84), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320 - 126, 240 - 84), 84f, 84f);
                        SetPlayerMission(0);
                        TP(320 + 126, 240 + 84);
                        SetSoul(1);
                        SetPlayerMission(1);
                        TP(320 - 126, 240 - 84);
                        SetSoul(1);
                        SetBoxMission(0);
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 - 126, 240), 84f, 84f);
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320 + 126, 240), 84f, 84f);
                        SetPlayerMission(0);
                        InstantTP(320 - 126, 240);
                        SetSoul(1);
                        SetPlayerMission(1);
                        InstantTP(320 + 126, 240);
                        SetSoul(1);
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) + 1, 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320 - 126, 240 - 84), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320 + 126, 240 + 84), 84f, 84f);
                        SetPlayerMission(0);
                        TP(320 - 126, 240 - 84);
                        SetSoul(1);
                        SetPlayerMission(1);
                        TP(320 + 126, 240 + 84);
                        SetSoul(1);
                        SetBoxMission(0);
                    }));
                }
                if (GametimeF == (int)(32 * 16 * bpm))
                {
                    SetBoxMission(0);
                    SetBox(380, 84, 84);
                    SetPlayerMission(0);
                    InstantTP(new(320, 380));
                }
                if (GametimeF == (int)(32 * 16 * bpm + bpm * 6))
                {
                    ScreenDrawing.ScreenAngle = 180;
                }

                if (GametimeF == (int)(37 * 16 * bpm + 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$0", 1, (int)(10 * bpm)));
                    CreateEntity(new GreenSoulGB(80, "$2", 0, (int)(10 * bpm)));
                    mainEffects.DrawLine.NormalLine l = new(0, 120, 640, 120, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(39 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "D", 1, (int)(6 * bpm)));
                    mainEffects.DrawLine.NormalLine l = new(0, 360, 640, 360, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(39 * 16 * bpm + 8 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "D", 0, (int)(6 * bpm)));
                    mainEffects.DrawLine.NormalLine l = new(320, 0, 320, 480, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(40 * 16 * bpm) - 80)
                {
                    SetBoxMission(1);
                    CreateEntity(new GreenSoulGB(80, 0, 1, (int)(10 * bpm)));
                }
                if (GametimeF == (int)(42 * 16 * bpm) - 80)
                {
                    SetBoxMission(0);
                    CreateEntity(new GreenSoulGB(80, 2, 0, (int)(10 * bpm)));
                }
                if (GametimeF == (int)(45 * 16 * bpm + 8 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, 1, 0, (int)(4 * bpm)));
                    CreateEntity(new GreenSoulGB(80, 3, 1, (int)(4 * bpm)));
                }
                if (GametimeF == (int)(46 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$3", 0, (int)(22 * bpm)));
                }
                #endregion
                if (GameStates.IsKeyPressed(InputIdentity.Heal))
                {
                    Regenerate();
                    PlaySound(Sounds.heal);
                }
                if (GametimeF >= (int)(48 * 16 * bpm) && GametimeF <= (int)(56 * 16 * bpm) && GametimeF % 9 == 0)
                {
                    SetPlayerMission(0);
                    float rot = Rand(0, 360);
                    float speed = Rand(1f, 2.1f);
                    CreateEntity(new Particle(
                        new Color(241, 214, 91) * 0.5f,
                       new(
                           Cos(rot) * speed,
                           Sin(rot) * speed),
                       Rand(11, 14.5f),
                       Heart.Centre, Sprites.square)
                    {
                        AutoRotate = true,
                        SlowLerp = Rand(0.03f, 0.04f),
                        DarkingSpeed = Rand(4.3f, 5.5f)
                    });
                    SetPlayerMission(1);
                    float rot1 = Rand(0, 360);
                    float speed1 = Rand(1f, 2.1f);
                    CreateEntity(new Particle(
                        new Color(241, 214, 91) * 0.5f,
                       new(
                           Cos(rot1) * speed1,
                           Sin(rot1) * speed1),
                       Rand(11, 14.5f),
                       Heart.Centre, Sprites.square)
                    {
                        AutoRotate = true,
                        SlowLerp = Rand(0.03f, 0.04f),
                        DarkingSpeed = Rand(4.3f, 5.5f)
                    });
                }
                if (GametimeF == (int)(56 * 16 * bpm))
                {
                    HardBarrage.ArrowAtk8();
                }
                if (GametimeF == (int)(64 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$0", 0, (int)(28 * bpm)));
                    CreateArrow(80, "+0", 8, 1, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + bpm * 18, "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 18, () => { mainEffects.ChangebarColor(); }));
                    float sin = 0;
                    AddInstance(new TimeRangedEvent(80, bpm * 16 * 6 - 5, () =>
                    {
                        sin += 1.05f;
                        ScreenDrawing.ScreenAngle = Sin(sin) * 6.5f;
                    }));
                    float asin = 0;
                    AddInstance(new TimeRangedEvent(80, bpm * 16 * 6, () =>
                    {
                        asin += 360 / bpm / 48;
                        ScreenDrawing.ScreenAngle = Sin(asin) * 6.5f;
                        InstantTP(320 + Sin(asin * 0.5f) * 22f, 240 + Sin(asin * 0.5f) * 17f);
                        InstantSetBox(new Vector2(320 + Sin(asin * 0.5f) * 22f, 240 + Sin(asin * 0.5f) * 17f), 84, 84);
                        HeartRot = Sin(asin * 0.5f) * 13f;
                        ScreenDrawing.BackGroundColor = new((int)(Sin(asin * 3 / 2) * 38), (int)(Sin(asin * 3 / 2) * 15), (int)(Sin(asin * 3 / 2) * 16));
                        //
                        //
                        //217,160,96
                    }));
                    for (int a = 0; a < bpm * 16 * 8 / bpm / 2; a++)
                    {
                        AddInstance(new TimeRangedEvent(80 + a * bpm * 2, 1, () =>
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                float rot1 = Rand(0, 360);
                                float speed1 = Rand(1f, 1.4f);
                                CreateEntity(new Particle(
                                    new Color(241, 214, 91) * 0.5f,
                                   new(
                                       Cos(rot1) * speed1,
                                       Sin(rot1) * speed1),
                                   Rand(18, 22f),
                                   Heart.Centre, Sprites.square)
                                {
                                    AutoRotate = true,
                                    SlowLerp = -Rand(0.040f, 0.050f),
                                    DarkingSpeed = Rand(1.7f, 2.4f)
                                });
                            }
                        }));
                    }
                }
                if (GametimeF == (int)(66 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$2", 0, (int)(28 * bpm)));
                    CreateArrow(80 + bpm * 4, "+0", 8, 1, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + bpm * 20, "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80 + bpm * 4, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 20, () => { mainEffects.ChangebarColor(); }));
                }
                if (GametimeF == (int)(68 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "D", 0, (int)(28 * bpm)));
                    CreateArrow(80 + (int)(bpm * 8), "+0", 8, 1, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + (int)(bpm * 24), "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80 + bpm * 8, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 24, () => { mainEffects.ChangebarColor(); }));
                }
                if (GametimeF == (int)(70 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, 2, 0, (int)(7 * 16 * bpm)));
                    CreateArrow(80 + bpm * 12, 2, 10, 1, 0, ArrowAttribute.SpeedUp);
                    CreateEntity(new GreenSoulGB(80 + bpm * 18, 0, 1, (int)(6 * 16 * bpm)));
                    AddInstance(new InstantEvent(80 + bpm * 12, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 18, () => { mainEffects.ChangebarColor2(); }));
                    for (int a = 0; a < 4; a++)
                    {
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 0);
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 0);
                    }
                    for (int a = 0; a < 4; a++)
                    {
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 2);
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 2);
                    }
                    AddInstance(new InstantEvent(bpm * 18 + 80, () =>
                    {
                        Arrow[] arr = GetAll<Arrow>();
                        for (int a = 0; a < arr.Length; a++)
                        {
                            int x = a;
                            arr[x].Stop(1145);
                        }
                    }));
                }
                if (GametimeF == (int)(70 * 16 * bpm))
                {
                    mainEffects.ScreenAngleNeo(-360, (int)(6 * 16 * bpm));
                }
                if (GametimeF > (int)(70 * 16 * bpm) && GametimeF < (int)(76 * 16 * bpm))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1 * 0.1f;
                }
                for (int i = 0; i < 15; i++)
                {
                    if (GametimeF == (int)(64 * 16 * bpm) + i * (int)(12 * bpm))
                    {
                        int a = 0;
                        mainEffects.DrawLine.Linerotate l1 = new(320, 240, 0, 300, 0, Color.White, 0.1f);
                        CreateEntity(l1);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l1.xCenter = 320 + a * a * 0.01f;
                            a++;
                            l1.alpha += 0.002f;
                            l1.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 3) + i * (int)(12 * bpm))
                    {
                        int b = 0;
                        mainEffects.DrawLine.Linerotate l2 = new(320, 240, 90, 300, 0, Color.White, 0.1f);
                        CreateEntity(l2);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l2.yCenter = 240 - b * b * 0.01f;
                            b++;
                            l2.alpha += 0.002f;
                            l2.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 6) + i * (int)(12 * bpm))
                    {
                        int c = 0;
                        mainEffects.DrawLine.Linerotate l3 = new(320, 240, 0, 300, 0, Color.White, 0.1f);
                        CreateEntity(l3);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l3.xCenter = 320 - c * c * 0.01f;
                            c++;
                            l3.alpha += 0.002f;
                            l3.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 9) + i * (int)(12 * bpm))
                    {
                        int d = 0;
                        mainEffects.DrawLine.Linerotate l4 = new(320, 240, 90, 300, 0, Color.White, 0.1f);
                        CreateEntity(l4);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l4.yCenter = 240 + d * d * 0.01f;
                            d++;
                            l4.alpha += 0.002f;
                            l4.rotate -= 0.5f;
                        }));
                    }
                }
                if (InBeat(76 * 4))
                {
                    mainEffects.DrawLine.Linerotate[] dis = GetAll<mainEffects.DrawLine.Linerotate>();
                    for (int a = 0; a < dis.Length; a++)
                    {
                        int x = a;
                        dis[x].Dispose();
                    }
                    CreateEntity(new mainEffects.MaskSquare(430, 0, 640 - 430, 130, 80, Color.Black, 1));
                    Extends.DrawingUtil.BetterBlackScreen(1, bpm * 32, bpm * 20, Color.Black);
                    Arrow[] arr = GetAll<Arrow>();
                    for (int a = 0; a < arr.Length; a++)
                    {
                        int x = a;
                        arr[x].Dispose();
                    }
                    GreenSoulGB[] gb = GetAll<GreenSoulGB>();
                    for (int a = 0; a < gb.Length; a++)
                    {
                        int x = a;
                        gb[x].Dispose();
                    }

                }
                if (InBeat(76 * 4))
                {
                    HardBarrage.Phase2Arrow1();
                    TP();
                    SetBox(240, 84, 84);
                }
                if (InBeat(76 * 4 + 16f))
                {
                    AddInstance(new TimeRangedEvent(50, () =>
                    {
                        BarColor = Color.Lerp(BarColor, new Color(120, 115, 115), 0.1f);
                        BarColor = Color.Lerp(BarColor, Color.Gray, 0.1f);
                        ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, BarColor, 0.1f);
                        ScreenDrawing.HPBar.AreaOccupied = new CollideRect(25 * 0.08f + ScreenDrawing.HPBar.AreaOccupied.X * 0.92f, 140, 20, 180);
                    }));

                }
                if (GametimeF == (int)BeatTime(76 * 4 + 16) + 51)
                {
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(BeatTime(94 * 4 - 76 * 4 - 16 + 8) - 50, () =>
                    {
                        speed += 360 / bpm / 28;
                        BarColor = new Color(225 - 60 + (int)(Sin(speed) * 60f), 115 + (int)(Sin(speed) * 110f), 115 + (int)(Sin(speed) * 110));
                    }));
                    AddInstance(new TimeRangedEvent(BeatTime(94 * 4 - 76 * 4 - 8) - 49, 3000, () =>
                    {
                        speed += 360 / bpm / 28;
                        BarColor = new Color(135 + (int)(Sin(speed) * 50f), 155 - (int)(Sin(speed) * 10f), 200 + (int)(Sin(speed) * 55f));
                        ScreenDrawing.UIColor = BarColor;
                    }));
                }
                if (InBeat(96 * 4 - 1)) { ScreenDrawing.WhiteOut(bpm * 6); }
                if (InBeat(96 * 4 - 4)) HardBarrage.FinalAttack();
                if (GameStates.IsKeyPressed120f(InputIdentity.Alternate))
                {
                    CreateEntity(new TextPrinter(114514, "$" + (GametimeF / bpm / 16).ToString(), beatVector, new TextSizeAttribute(0.5f)));
                    beatVector += new Vector2(0, 15);
                }
                if (beatVector.Y >= 380)
                {
                    beatVector.X += 75;
                    beatVector.Y = 100;
                }
                //CreateEntity(new TextPrinter(1, (GametimeF / bpm / 4).ToString(), Heart.Centre + new Vector2(-50, -50), new TextSizeAttribute(0.5f)));
                ScreenDrawing.HPBar.HPExistColor = BarColor;
                Heart.InstantSetRotation(HeartRot);
                //Functions.ScreenDrawing.HPBar.HPLoseColor = LoseColor;

                AdvanceFunctions.Interactive.AddMissEvent(() =>
                {
                    if (abool)
                    {
                        abool = false;
                        AddInstance(new TimeRangedEvent(55, () => { hit++; if (hit % 55 == 0) abool = true; }));
                        mainEffects.HPlerp();
                    }
                });
            }
            public void Easy()
            {
                Effect();
                #region 开头目害段
                if (GametimeF > (int)(8 * 16 * bpm) && GametimeF < (int)(14 * 16 * bpm))
                {
                    InstantSetBox(Sin(1.25f * (GametimeF - (int)(8 * 16 * bpm + 32 * bpm))) * 15 + 230, 84, 84);
                    InstantTP(BoxStates.Centre.X, BoxStates.Centre.Y);
                }
                if (GametimeF == (int)(8 * 16 * bpm) - 80)
                {
                    EasyBarrage.ArrowAtk1();
                    EasyBarrage.ArrowAtk2();
                }
                if (GametimeF == (int)(16 * 16 * bpm) - 80)
                {
                    AddInstance(new TimeRangedEvent(bpm * 16, () => { HeartRot = 0 * 0.05f + HeartRot * 0.95f; }));
                    EasyBarrage.ArrowAtk3();
                }
                if (GametimeF == (int)(23 * 16 * bpm) - 80)
                {
                    EasyBarrage.ArrowAtk4();
                    EasyBarrage.ArrowAtk5();
                }
                if (GametimeF == (int)(24 * 16 * bpm) - 80)
                {
                    EasyBarrage.ArrowAtk3();
                }
                if (GametimeF == (int)(30 * 16 * bpm) - 80)
                {
                    SetPlayerMission(0);
                    EasyBarrage.ArrowAtk6();
                }
                if (GametimeF == (int)(32 * 16 * bpm) - 80)
                {
                    Heart.Split();
                    SetBoxMission(1);
                    InstantSetBox(new Vector2(-1000, -1000), 84f, 84f);
                    SetPlayerMission(1);
                    EasyBarrage.ArrowAtk7_1();
                    InstantTP(-10000, -10000);
                    SetPlayerMission(0);
                    EasyBarrage.ArrowAtk7();
                    SetBoxMission(0);
                }
                if (GametimeF == (int)(32 * 16 * bpm))
                {
                    AddInstance(new TimeRangedEvent((int)(11 * 16 * bpm + 12 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320, 240), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320, 240), 84f, 84f);
                        SetPlayerMission(0);
                        Player.hearts[1].Teleport(new(320, 240));
                        Player.hearts[1].Merge(Player.hearts[0]);
                        SetPlayerMission(0);
                        TP();
                        SetPlayerMission(0);
                    }));
                    AddInstance(new TimeRangedEvent((int)(10 * 16 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320 + 126, 240 + 84), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320 - 126, 240 - 84), 84f, 84f);
                        SetPlayerMission(0);
                        TP(320 + 126, 240 + 84);
                        SetSoul(1);
                        SetPlayerMission(1);
                        TP(320 - 126, 240 - 84);
                        SetSoul(1);
                        SetBoxMission(0);
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm), 1, () =>
                    {
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320 - 126, 240), 84f, 84f);
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320 + 126, 240), 84f, 84f);
                        SetPlayerMission(0);
                        InstantTP(320 - 126, 240);
                        SetSoul(1);
                        SetPlayerMission(1);
                        InstantTP(320 + 126, 240);
                        SetSoul(1);
                    }));
                    AddInstance(new TimeRangedEvent((int)(8 * 16 * bpm) + 1, 1, () =>
                    {
                        SetBoxMission(0);
                        SetBox(new Vector2(320 - 126, 240 - 84), 84f, 84f);
                        SetBoxMission(1);
                        SetBox(new Vector2(320 + 126, 240 + 84), 84f, 84f);
                        SetPlayerMission(0);
                        TP(320 - 126, 240 - 84);
                        SetSoul(1);
                        SetPlayerMission(1);
                        TP(320 + 126, 240 + 84);
                        SetSoul(1);
                        SetBoxMission(0);
                    }));
                }
                if (GametimeF == (int)(32 * 16 * bpm))
                {
                    SetBoxMission(0);
                    SetBox(380, 84, 84);
                    SetPlayerMission(0);
                    InstantTP(new(320, 380));
                }
                if (GametimeF == (int)(32 * 16 * bpm + bpm * 6))
                {
                    ScreenDrawing.ScreenAngle = 180;
                }

                if (GametimeF == (int)(37 * 16 * bpm + 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$0", 1, (int)(10 * bpm)));
                    CreateEntity(new GreenSoulGB(80, "$2", 0, (int)(10 * bpm)));
                    mainEffects.DrawLine.NormalLine l = new(0, 120, 640, 120, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(39 * 16 * bpm) - 80)
                {
                    mainEffects.DrawLine.NormalLine l = new(0, 360, 640, 360, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(39 * 16 * bpm + 8 * bpm) - 80)
                {
                    mainEffects.DrawLine.NormalLine l = new(320, 0, 320, 480, 100, 0, Color.White, 0.99f);
                    CreateEntity(l);
                    AddInstance(new TimeRangedEvent(80, 1, () =>
                    {
                        l.alpha = 0.75f;
                    }));
                    AddInstance(new TimeRangedEvent(80, 20, () =>
                    {
                        l.alpha -= 0.0375f;
                    }));
                }
                if (GametimeF == (int)(45 * 16 * bpm + 8 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, 1, 0, (int)(4 * bpm)));
                }
                if (GametimeF == (int)(46 * 16 * bpm) - 80)
                {
                    CreateEntity(new GreenSoulGB(80, "$3", 0, (int)(22 * bpm)));
                }
                #endregion
                if (GameStates.IsKeyPressed(InputIdentity.Heal))
                {
                    Regenerate();
                    PlaySound(Sounds.heal);
                }
                if (GametimeF == (int)(56 * 16 * bpm))
                {
                    EasyBarrage.ArrowAtk8();
                }
                if (GametimeF == (int)(64 * 16 * bpm) - 80)
                {
                    CreateArrow(80, "$0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + bpm * 18, "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 18, () => { mainEffects.ChangebarColor(); }));
                    float sin = 0;
                    AddInstance(new TimeRangedEvent(80, bpm * 16 * 6 - 5, () =>
                    {
                        sin += 1.05f;
                        ScreenDrawing.ScreenAngle = Sin(sin) * 6.5f;
                    }));
                    float asin = 0;
                    AddInstance(new TimeRangedEvent(80, bpm * 16 * 6, () =>
                    {
                        asin += 360 / bpm / 48;
                        ScreenDrawing.ScreenAngle = Sin(asin) * 6.5f;
                        InstantTP(320 + Sin(asin * 0.5f) * 22f, 240 + Sin(asin * 0.5f) * 17f);
                        InstantSetBox(new Vector2(320 + Sin(asin * 0.5f) * 22f, 240 + Sin(asin * 0.5f) * 17f), 84, 84);
                        HeartRot = Sin(asin * 0.5f) * 13f;
                        ScreenDrawing.BackGroundColor = new((int)(Sin(asin * 3 / 2) * 38), (int)(Sin(asin * 3 / 2) * 15), (int)(Sin(asin * 3 / 2) * 16));
                        //
                        //
                        //217,160,96
                    }));
                }
                if (GametimeF == (int)(66 * 16 * bpm) - 80)
                {
                    CreateArrow(80 + bpm * 4, "$2", 8, 0, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + bpm * 20, "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80 + bpm * 4, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 20, () => { mainEffects.ChangebarColor(); }));
                }
                if (GametimeF == (int)(68 * 16 * bpm) - 80)
                {
                    CreateArrow(80 + (int)(bpm * 8), "$0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    CreateArrow(80 + (int)(bpm * 24), "+0", 8, 0, 0, ArrowAttribute.SpeedUp);
                    AddInstance(new InstantEvent(80 + bpm * 8, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 24, () => { mainEffects.ChangebarColor(); }));
                }
                if (GametimeF == (int)(70 * 16 * bpm) - 80)
                {
                    CreateArrow(80 + bpm * 12, 0, 8, 0, 0, ArrowAttribute.SpeedUp);
                    CreateEntity(new GreenSoulGB(80 + bpm * 18, 3, 1, (int)(6 * 16 * bpm)));
                    AddInstance(new InstantEvent(80 + bpm * 12, () => { mainEffects.ChangebarColor(); }));
                    AddInstance(new InstantEvent(80 + bpm * 18, () => { mainEffects.ChangebarColor2(); }));
                    for (int a = 0; a < 4; a++)
                    {
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 0);
                        CreateArrow(80 + bpm * 18 + 3, a, 5, 1, 0);
                    }
                    AddInstance(new InstantEvent(bpm * 18 + 80, () =>
                    {
                        Arrow[] arr = GetAll<Arrow>();
                        for (int a = 0; a < arr.Length; a++)
                        {
                            int x = a;
                            arr[x].Stop(1145);
                        }
                    }));
                }
                if (GametimeF == (int)(70 * 16 * bpm))
                {
                    mainEffects.ScreenAngleNeo(-360, (int)(6 * 16 * bpm));
                }
                if (GametimeF > (int)(70 * 16 * bpm) && GametimeF < (int)(76 * 16 * bpm))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1 * 0.1f;
                }
                for (int i = 0; i < 15; i++)
                {
                    if (GametimeF == (int)(64 * 16 * bpm) + i * (int)(12 * bpm))
                    {
                        int a = 0;
                        mainEffects.DrawLine.Linerotate l1 = new(320, 240, 0, 300, 0, Color.White, 0.1f);
                        CreateEntity(l1);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l1.xCenter = 320 + a * a * 0.01f;
                            a++;
                            l1.alpha += 0.002f;
                            l1.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 3) + i * (int)(12 * bpm))
                    {
                        int b = 0;
                        mainEffects.DrawLine.Linerotate l2 = new(320, 240, 90, 300, 0, Color.White, 0.1f);
                        CreateEntity(l2);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l2.yCenter = 240 - b * b * 0.01f;
                            b++;
                            l2.alpha += 0.002f;
                            l2.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 6) + i * (int)(12 * bpm))
                    {
                        int c = 0;
                        mainEffects.DrawLine.Linerotate l3 = new(320, 240, 0, 300, 0, Color.White, 0.1f);
                        CreateEntity(l3);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l3.xCenter = 320 - c * c * 0.01f;
                            c++;
                            l3.alpha += 0.002f;
                            l3.rotate -= 0.5f;
                        }));
                    }
                    if (GametimeF == (int)(64 * 16 * bpm + bpm * 9) + i * (int)(12 * bpm))
                    {
                        int d = 0;
                        mainEffects.DrawLine.Linerotate l4 = new(320, 240, 90, 300, 0, Color.White, 0.1f);
                        CreateEntity(l4);
                        AddInstance(new TimeRangedEvent(300, () =>
                        {
                            l4.yCenter = 240 + d * d * 0.01f;
                            d++;
                            l4.alpha += 0.002f;
                            l4.rotate -= 0.5f;
                        }));
                    }
                }
                if (InBeat(76 * 4))
                {
                    mainEffects.DrawLine.Linerotate[] dis = GetAll<mainEffects.DrawLine.Linerotate>();
                    for (int a = 0; a < dis.Length; a++)
                    {
                        int x = a;
                        dis[x].Dispose();
                    }
                    CreateEntity(new mainEffects.MaskSquare(430, 0, 640 - 430, 130, 80, Color.Black, 1));
                    Extends.DrawingUtil.BetterBlackScreen(1, bpm * 32, bpm * 20, Color.Black);
                    Arrow[] arr = GetAll<Arrow>();
                    for (int a = 0; a < arr.Length; a++)
                    {
                        int x = a;
                        arr[x].Dispose();
                    }
                    GreenSoulGB[] gb = GetAll<GreenSoulGB>();
                    for (int a = 0; a < gb.Length; a++)
                    {
                        int x = a;
                        gb[x].Dispose();
                    }

                }
                if (InBeat(76 * 4))
                {
                    EasyBarrage.Phase2Arrow1();
                    TP();
                    SetBox(240, 84, 84);
                }
                if (InBeat(76 * 4 + 16f))
                {
                    AddInstance(new TimeRangedEvent(50, () =>
                    {
                        BarColor = Color.Lerp(BarColor, new Color(120, 115, 115), 0.1f);
                        BarColor = Color.Lerp(BarColor, Color.Gray, 0.1f);
                        ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, BarColor, 0.1f);
                        ScreenDrawing.HPBar.AreaOccupied = new CollideRect(25 * 0.08f + ScreenDrawing.HPBar.AreaOccupied.X * 0.92f, 140, 20, 180);
                    }));

                }
                if (GametimeF == (int)BeatTime(76 * 4 + 16) + 51)
                {
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(BeatTime(94 * 4 - 76 * 4 - 16 + 8) - 50, () =>
                    {
                        speed += 360 / bpm / 28;
                        BarColor = new Color(225 - 60 + (int)(Sin(speed) * 60f), 115 + (int)(Sin(speed) * 110f), 115 + (int)(Sin(speed) * 110));
                    }));
                    AddInstance(new TimeRangedEvent(BeatTime(94 * 4 - 76 * 4 - 8) - 49, 3000, () =>
                    {
                        speed += 360 / bpm / 28;
                        BarColor = new Color(135 + (int)(Sin(speed) * 50f), 155 - (int)(Sin(speed) * 10f), 200 + (int)(Sin(speed) * 55f));
                        ScreenDrawing.UIColor = BarColor;
                    }));
                }
                if (InBeat(96 * 4 - 1)) { ScreenDrawing.WhiteOut(bpm * 6); }
                if (InBeat(96 * 4 - 4)) EasyBarrage.FinalAttack();
                if (GameStates.IsKeyPressed120f(InputIdentity.Alternate))
                {
                    CreateEntity(new TextPrinter(114514, "$" + (GametimeF / bpm / 16).ToString(), beatVector, new TextSizeAttribute(0.5f)));
                    beatVector += new Vector2(0, 15);
                }
                if (beatVector.Y >= 380)
                {
                    beatVector.X += 75;
                    beatVector.Y = 100;
                }
                //CreateEntity(new TextPrinter(1, (GametimeF / bpm / 4).ToString(), Heart.Centre + new Vector2(-50, -50), new TextSizeAttribute(0.5f)));
                ScreenDrawing.HPBar.HPExistColor = BarColor;
                Heart.InstantSetRotation(HeartRot);
                //Functions.ScreenDrawing.HPBar.HPLoseColor = LoseColor;

                AdvanceFunctions.Interactive.AddMissEvent(() =>
                {
                    if (abool)
                    {
                        abool = false;
                        AddInstance(new TimeRangedEvent(55, () => { hit++; if (hit % 55 == 0) abool = true; }));
                        mainEffects.HPlerp();
                    }
                });
            }
            int hit = 0;
            bool abool = true;
            Vector2 beatVector = new(100, 100);
            public void ScreenAngleLerp(float duration, float lerpto, float count)
            {
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * (1 - count) + lerpto * count;
                }));
            }

            public void Start()
            {





                SetSoul(1);
                InstantSetBox(new Vector2(320, -100), 84, 84);
                InstantTP(320, -100);
                Heart.Gravity = 0;
                HeartAttribute.DamageTaken = 0;
                HeartAttribute.MaxHP = 10000;
                ScreenDrawing.HPBar.Vertical = true;
                ScreenDrawing.UIColor = Color.White;
                ScreenDrawing.HPBar.HPExistColor = Color.DarkRed;
                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.AreaOccupied = new CollideRect(25, 140, 20, 180);
                GametimeDelta = (int)(-1f * bpm);
                abool = true;
                hit = 0;
                //GametimeDelta = (int)(96 * 16 * bpm) - 120;
                muhai = new Shader(Loader.Load<Effect>("Musics\\RIP\\muhai"));
                muhai.Parameters["distance2"].SetValue(new Vector2(0f, 0f));
                muhai.Parameters["distance1"].SetValue(new Vector2(0f, 0f));
                muhai.Parameters["distance3"].SetValue(new Vector2(0f, 0f));
                BarColor = new(139, 0, 0, 255);
                LoseColor = Color.Gray;
                HeartRot = 0;
            }
        }
    }
}
