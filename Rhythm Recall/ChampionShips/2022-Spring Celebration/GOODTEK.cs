using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class Goodtek : IChampionShip
    {
        public Goodtek()
        {
            difficulties = new();
            difficulties.Add("div.2", Difficulty.Noob);
            difficulties.Add("div.1", Difficulty.Extreme);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new GOODTEK();

        public class GOODTEK : IWaveSet
        {
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.5f),
                            new(Difficulty.Extreme, 18.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.5f),
                            new(Difficulty.Extreme, 18.9f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 7.5f),
                            new(Difficulty.Extreme, 21.0f),
                        }
                    );
                public override string BarrageAuthor => "Tlottgodinf";
                public override string PaintAuthor => "OtokP";
                public override string SongAuthor => "EBIMAYO";
            }
            private class LineR : Entity
            {
                public int duration = 0;
                public float xCentre = 0;
                public float yCentre = 0;
                public int rotate = 0;
                public LineR(float xCentre, float yCentre, int rotate, int duration, float alpha)
                {
                    this.xCentre = xCentre;
                    this.yCentre = yCentre;
                    this.rotate = rotate;
                    this.duration = duration;
                    this.alpha = alpha;
                }
                public float alpha = 1;
                public int time = 0;
                public float speed = 1;
                public override void Draw()
                {
                    DrawingLab.DrawLine(new(xCentre - Tan(rotate) * yCentre, 0), new(xCentre + Tan(rotate) * (480 - yCentre), 480), 4, Color.White * alpha, 0.99f);
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

            private class LT : Entity
            {
                public int duration = 0;
                public Vector2 point1 = new Vector2(0, 0);
                public Vector2 point2 = new Vector2(0, 0);
                public LT(Vector2 point1, Vector2 point2, int duration, float alpha)
                {
                    this.point1 = point1;
                    this.point2 = point2;
                    this.duration = duration;
                    this.alpha = alpha;
                }
                public float alpha = 1;
                public int time = 0;
                public float speed = 1;
                public override void Draw()
                {
                    DrawingLab.DrawLine(point1, point2, 4, Color.White * alpha, 0.99f);
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
            public string Music => "GOODTEK";
            public string FightName => "GOODTEK";
            public SongInformation Attributes => new ThisInformation();
            public void Start()
            {
                //static Shader shader = new Shader(Loader.Load<Effect>("Musics\\Grievous Lady\\Effects"));

                SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                SetSoul(1);
                TP();
                GametimeDelta = -3;
                HeartAttribute.DamageTaken = 1;
                HeartAttribute.MaxHP = 9;
                Heart.JumpTimeLimit = 2;
                //GametimeDelta = (int)(bpm * 16 * 73 - bpm - 85);
                //PlayOffset = (int)(bpm * 16 * 73 - bpm - 78);
            }

            public static float bpm = 4.9342f;
            public void Effect()
            {

                for (int a = 0; a < 32; a++) if (GametimeF == (int)(bpm * 16 + bpm * a * 2)) ScreenDrawing.CameraEffect.SizeShrink(1, 3);
                #region 线段
                NormalLine line1 = new(71.11f * 1, 0, 71.11f * 1, 480, (int)(bpm * 8), 1);
                NormalLine line2 = new(71.11f * 2, 0, 71.11f * 2, 480, (int)(bpm * 8), 1);
                NormalLine line3 = new(71.11f * 3, 0, 71.11f * 3, 480, (int)(bpm * 8), 1);
                NormalLine line4 = new(71.11f * 4, 0, 71.11f * 4, 480, (int)(bpm * 8), 1);
                NormalLine line5 = new(71.11f * 5, 0, 71.11f * 5, 480, (int)(bpm * 8), 1);
                NormalLine line6 = new(71.11f * 6, 0, 71.11f * 6, 480, (int)(bpm * 8), 1);
                NormalLine line7 = new(71.11f * 7, 0, 71.11f * 7, 480, (int)(bpm * 8), 1);
                NormalLine line8 = new(71.11f * 8, 0, 71.11f * 8, 480, (int)(bpm * 8), 1);
                //开头八种线
                NormalLine lineRand = new(71.11f * Rand(1, 8), 0, 71.11f * Rand(1, 8), 480, (int)(bpm * 8), 1);
                //十六个端点随机链接线
                NormalLine Y1 = new(320 - 84, 240 - 128, 320, 240, (int)(bpm * 32), 0.85f);
                NormalLine Y2 = new(320 + 84, 240 - 128, 320, 240, (int)(bpm * 32), 0.85f);
                NormalLine Y3 = new(320, 240 + 128, 320, 240, (int)(bpm * 32), 0.85f);
                //组成Y的三条线
                LineR lineR1 = new(320, 240, (int)(0 + (GametimeF * 1.1f)), (int)(bpm * 8), 0.85f);
                LineR lineR2 = new(320, 240, (int)(90 + (GametimeF * 1.1f)), (int)(bpm * 8), 0.85f);
                //旋转的两条线
                #endregion
                #region 正着的线来一遍       
                if (GametimeF == (int)(bpm * 2 + bpm * 16)) CreateEntity(line1);
                if (GametimeF == (int)(bpm * 4 + bpm * 16)) CreateEntity(line2);
                if (GametimeF == (int)(bpm * 6 + bpm * 16)) CreateEntity(line3);
                if (GametimeF == (int)(bpm * 8 + bpm * 16)) CreateEntity(line4);
                if (GametimeF == (int)(bpm * 10 + bpm * 16)) CreateEntity(line5);
                if (GametimeF == (int)(bpm * 12 + bpm * 16)) CreateEntity(line6);
                if (GametimeF == (int)(bpm * 14 + bpm * 16)) CreateEntity(line7);
                if (GametimeF == (int)(bpm * 16 + bpm * 16)) CreateEntity(line8);
                #endregion
                #region 倒着来一遍  
                if (GametimeF == (int)(bpm * 18 + bpm * 16)) CreateEntity(line8);
                if (GametimeF == (int)(bpm * 20 + bpm * 16)) CreateEntity(line7);
                if (GametimeF == (int)(bpm * 22 + bpm * 16)) CreateEntity(line6);
                if (GametimeF == (int)(bpm * 24 + bpm * 16)) CreateEntity(line5);
                if (GametimeF == (int)(bpm * 26 + bpm * 16)) CreateEntity(line4);
                if (GametimeF == (int)(bpm * 28 + bpm * 16)) CreateEntity(line3);
                if (GametimeF == (int)(bpm * 30 + bpm * 16)) CreateEntity(line2);
                if (GametimeF == (int)(bpm * 32 + bpm * 16)) CreateEntity(line1);
                #endregion
                for (int a = 0; a < 16; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 3 + a * bpm * 2)) CreateEntity(lineRand);
                }//随机线条来16下
                AddInstance(new TimeRangedEvent(10, 60, () =>
                {
                    line1.alpha -= 0.045f;
                    line2.alpha -= 0.045f;
                    line3.alpha -= 0.045f;
                    line4.alpha -= 0.045f;
                    line5.alpha -= 0.045f;
                    line6.alpha -= 0.045f;
                    line7.alpha -= 0.045f;
                    line8.alpha -= 0.045f;
                    lineRand.alpha -= 0.045f;
                }));//递减alpha
                if (GametimeF == (int)(bpm * 16 * 5))
                {
                    line1.Dispose();
                    line2.Dispose();
                    line3.Dispose();
                    line4.Dispose();
                    line5.Dispose();
                    line6.Dispose();
                    line7.Dispose();
                    line8.Dispose();
                    lineRand.Dispose();
                }//Dispose线
                if (GametimeF == (int)(bpm * 16 * 20))
                {
                    CreateEntity(Y1);
                    CreateEntity(Y2);
                    CreateEntity(Y3);
                }//创建Y字
                AddInstance(new TimeRangedEvent(bpm * 16, 60, () =>
                {
                    Y1.alpha -= 0.025f;
                    Y2.alpha -= 0.025f;
                    Y3.alpha -= 0.025f;
                }));//递减alpha
                if (GametimeF == (int)(bpm * 16 * 23))
                {
                    Y1.Dispose(); Y2.Dispose(); Y3.Dispose();
                }//DisopseY字线
                if (GametimeF == (int)(bpm * 16 * 32))
                {
                    float box = 0;
                    float a = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 12, () =>
                    {
                        InstantSetBox(BoxStates.Centre.Y - a * a, 84, 84);
                        InstantTP(Heart.Centre - new Vector2(0, a * a));
                        a += 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, bpm * 8, () =>
                    {
                        InstantSetBox(600 - box, 226, 138);
                        InstantTP(Heart.Centre - new Vector2(0, box));
                        box += 8;
                    }));
                }
                if (GametimeF == (int)(bpm * 16 * 56))
                {
                    ScreenDrawing.CameraEffect.SizeShrink(30, bpm * 16);
                }
            }
            public void Extreme()
            {

                /*if (GametimeF >= 0 && GametimeF <= 114514)
                {

                    CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1,
                        GametimeF.ToString(),
                        new Vector2(0, 100),
                        new UndyneFight_Ex.Fight.TextColorAttribute(new Color(Rand(0, 255), Rand(0, 255), Rand(0, 255)))));
                }*/
                Effect();
                for (int a = 0; a < 10; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 5 + a * 16 * bpm) - 80)
                    {
                        ExtremeBarrage.Intro6();
                    }
                }
                if (GametimeF == (int)(bpm * 16 * 15) - 80) ExtremeBarrage.Intro16();
                if (GametimeF == (int)(bpm * 16 * 16) - 80) ExtremeBarrage.Intro17();
                if (GametimeF == (int)(bpm * 16 * 17) - 80) ExtremeBarrage.Intro18();
                if (GametimeF == (int)(bpm * 16 * 18) - 80) ExtremeBarrage.Intro19();
                if (GametimeF == (int)(bpm * 16 * 19) - 80) ExtremeBarrage.Intro18();
                if (GametimeF == (int)(bpm * 16 * 20) - 80) ExtremeBarrage.Intro18();
                if (GametimeF == (int)(bpm * 16 * 21) - 80) ExtremeBarrage.Intro18();
                if (GametimeF == (int)(bpm * 16 * 22) - 80) ExtremeBarrage.Intro23();
                if (GametimeF == (int)(bpm * 16 * 23) - 80) ExtremeBarrage.Intro24();
                if (GametimeF == (int)(bpm * 16 * 24) - 80) ExtremeBarrage.Intro25();
                if (GametimeF == (int)(bpm * 16 * 25) - 80) ExtremeBarrage.Intro26();
                if (GametimeF == (int)(bpm * 16 * 26) - 80) ExtremeBarrage.Intro26();
                if (GametimeF == (int)(bpm * 16 * 27) - 80) ExtremeBarrage.Intro28();
                if (GametimeF == (int)(bpm * 16 * 28) - 80) ExtremeBarrage.Intro28();
                if (GametimeF == (int)(bpm * 16 * 29) - 80) ExtremeBarrage.Intro30();
                if (GametimeF == (int)(bpm * 16 * 30) - 80) ExtremeBarrage.Intro30();
                if (GametimeF == (int)(bpm * 16 * 31) - 80)
                {
                    ExtremeBarrage.Intro32();
                    ExtremeBarrage.AnotherIntro32();
                }
                if (GametimeF == (int)(bpm * 16 * 32) - 80)

                    ExtremeBarrage.Intro33();

                if (GametimeF == (int)(bpm * 16 * 33 + bpm * 8)) SetSoul(2);
                for (int a = 0; a < 7; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 34 + a * 16 * bpm))
                    {
                        ExtremeBarrage.Intro34();

                    }
                }//放骨头
                if (GametimeF == (int)(bpm * 16 * 41 + bpm * 8)) Heart.GiveForce(180, 4);
                for (int a = 0; a < 6; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 42 + a * 16 * bpm))
                    {
                        ExtremeBarrage.Intro34();
                    }

                }//放骨头
                if (GametimeF == (int)(bpm * 16 * 48 + bpm * 8))
                {
                    Heart.GiveForce(0, 8);
                    TP();
                    SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                    SetSoul(1);
                }//设置绿心,方向
                if (GametimeF == (int)(bpm * 16 * 49) - 80) ExtremeBarrage.Intro51and52();
                if (GametimeF == (int)(bpm * 16 * 51) - 80) ExtremeBarrage.Intro53and54();
                if (GametimeF == (int)(bpm * 16 * 53) - 80) ExtremeBarrage.Intro55();
                if (GametimeF == (int)(bpm * 16 * 54) - 80) ExtremeBarrage.Intro55();
                if (GametimeF == (int)(bpm * 16 * 55) - 80) ExtremeBarrage.Intro57();
                if (GametimeF == (int)(bpm * 16 * 57))
                {
                    ExtremeBarrage.Intro59to61();
                    SetSoul(3);
                    SetBox(320 - 90, 320 + 90, 240 - 90, 240 + 90);
                }//设置橙心，放攻击
                if (GametimeF == (int)(bpm * 16 * 57 + 25))
                {
                    CreateBone(new CentreCircleBone(0, 3, 240, bpm * 6 * 16));
                }
                if (GametimeF == (int)(bpm * 16 * 59)) ExtremeBarrage.Intro62and63();
                if (GametimeF == (int)(bpm * 16 * 61)) ExtremeBarrage.Intro64to67();
                if (GametimeF == (int)(bpm * 16 * 65))
                {
                    SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                    TP();
                    SetSoul(1);
                }//设置绿心
                if (GametimeF == (int)(bpm * 16 * 65 - 80))
                {
                    for (int a = 0; a < 30; a++)
                    {
                        CreateArrow(80 + bpm * a * 2, "R", 6, 0, 0);
                        CreateArrow(80 + bpm * a * 2 + bpm, "+2", 6, 1, 0);
                    }
                }//蓝对红
                if (GametimeF == (int)(bpm * 16 * 69 - 80))
                {
                    for (int a = 0; a < 16; a++)
                    {
                        CreateArrow(80 + bpm * a * 2, "R", 6, 0, 0);
                        CreateArrow(80 + bpm * a * 2 + bpm, "+2", 6, 1, 0);
                    }
                    AddInstance(new InstantEvent(bpm * 32, () =>
                    {
                        for (int a = 0; a < 15; a++)
                        {
                            CreateArrow(80 + bpm * a * 2, "R", 6, 0, 0);
                            CreateArrow(80 + bpm * a * 2 + bpm, "R", 6, 1, 0);
                        }
                    }));
                }//蓝对红，蓝红
                if (GametimeF == (int)(bpm * 16 * 73 - bpm - 80)) ExtremeBarrage.Intro76();
                if (GametimeF == (int)(bpm * 16 * 74 - bpm - 80)) ExtremeBarrage.Intro77();
                if (GametimeF == (int)(bpm * 16 * 75 - bpm - 80)) ExtremeBarrage.Intro78();
                if (GametimeF == (int)(bpm * 16 * 76 - bpm - 80)) ExtremeBarrage.Intro79and80();
                if (GametimeF == (int)(bpm * 16 * 78 - bpm - 80)) ExtremeBarrage.Intro81();
                if (GametimeF == (int)(bpm * 16 * 79 - bpm - 80)) ExtremeBarrage.Intro82();
                if (GametimeF == (int)(bpm * 16 * 80 - bpm - 80)) ExtremeBarrage.Intro83and84();
                if (GametimeF == (int)(bpm * 16 * 81 - 80)) ExtremeBarrage.Intro85();
                if (GametimeF == (int)(bpm * 16 * 82 - 80)) ExtremeBarrage.Intro86();
                if (GametimeF == (int)(bpm * 16 * 83 - 80)) ExtremeBarrage.Intro85();
                if (GametimeF == (int)(bpm * 16 * 84 - 80)) ExtremeBarrage.Intro86();
                if (GametimeF == (int)(bpm * 16 * 85 - 80)) ExtremeBarrage.Intro85();
                if (GametimeF == (int)(bpm * 16 * 86 - 80)) ExtremeBarrage.Intro86();
                if (GametimeF == (int)(bpm * 16 * 87 - 80)) ExtremeBarrage.Intro85();
                if (GametimeF == (int)(bpm * 16 * 88 - 80)) ExtremeBarrage.Intro87();
                if (GametimeF == (int)(bpm * 16 * 89 - 80)) ExtremeBarrage.Intro51and52();
                if (GametimeF == (int)(bpm * 16 * 91 - 80)) ExtremeBarrage.Intro53and54();
                if (GametimeF == (int)(bpm * 16 * 93 - 80)) ExtremeBarrage.Intro55();
                if (GametimeF == (int)(bpm * 16 * 94 - 80)) ExtremeBarrage.Intro55();
                if (GametimeF == (int)(bpm * 16 * 95 - 80)) ExtremeBarrage.Intro57();
                if (GametimeF == (int)(bpm * 16 * 97 - 80)) CreateArrow(80, 0, 13, 1, 0, ArrowAttribute.SpeedUp);
                if (GametimeF == (int)(bpm * 16 * 97 - 80)) CreateArrow(80, 2, 13, 0, 0, ArrowAttribute.SpeedUp);
            }
            public void Noob()
            {

                Effect();
                for (int a = 0; a < 10; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 5 + a * 16 * bpm) - 80)
                    {
                        NoobBarrage.Intro6();
                    }
                }
                if (GametimeF == (int)(bpm * 16 * 15) - 80) NoobBarrage.Intro16();
                if (GametimeF == (int)(bpm * 16 * 16) - 80) NoobBarrage.Intro17();
                if (GametimeF == (int)(bpm * 16 * 17) - 80) NoobBarrage.Intro18();
                if (GametimeF == (int)(bpm * 16 * 18) - 80) NoobBarrage.Intro19();
                if (GametimeF == (int)(bpm * 16 * 19) - 80) NoobBarrage.Intro18();
                if (GametimeF == (int)(bpm * 16 * 20) - 80) NoobBarrage.Intro18();
                if (GametimeF == (int)(bpm * 16 * 21) - 80) NoobBarrage.Intro18();
                if (GametimeF == (int)(bpm * 16 * 22) - 80) NoobBarrage.Intro23();
                if (GametimeF == (int)(bpm * 16 * 23) - 80) NoobBarrage.Intro24();
                if (GametimeF == (int)(bpm * 16 * 24) - 80) NoobBarrage.Intro25();
                if (GametimeF == (int)(bpm * 16 * 25) - 80) NoobBarrage.Intro26();
                if (GametimeF == (int)(bpm * 16 * 26) - 80) NoobBarrage.Intro26();
                if (GametimeF == (int)(bpm * 16 * 27) - 80) NoobBarrage.Intro28();
                if (GametimeF == (int)(bpm * 16 * 28) - 80) NoobBarrage.Intro28();
                if (GametimeF == (int)(bpm * 16 * 29) - 80) NoobBarrage.Intro30();
                if (GametimeF == (int)(bpm * 16 * 30) - 80) NoobBarrage.Intro30();
                if (GametimeF == (int)(bpm * 16 * 31) - 80)
                {
                    NoobBarrage.Intro32();
                    NoobBarrage.AnotherIntro32();
                }
                if (GametimeF == (int)(bpm * 16 * 32) - 80)

                    NoobBarrage.Intro33();

                if (GametimeF == (int)(bpm * 16 * 33 + bpm * 8)) SetSoul(2);
                for (int a = 0; a < 7; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 34 + a * 16 * bpm))
                    {
                        NoobBarrage.Intro34();

                    }
                }//放骨头
                if (GametimeF == (int)(bpm * 16 * 41 + bpm * 8)) Heart.GiveForce(180, 4);
                for (int a = 0; a < 6; a++)
                {
                    if (GametimeF == (int)(bpm * 16 * 42 + a * 16 * bpm))
                    {
                        NoobBarrage.Intro34();
                    }

                }//放骨头
                if (GametimeF == (int)(bpm * 16 * 48 + bpm * 8))
                {
                    Heart.GiveForce(0, 8);
                    TP();
                    SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                    SetSoul(1);
                }//设置绿心,方向
                if (GametimeF == (int)(bpm * 16 * 49) - 80) NoobBarrage.Intro51and52();
                if (GametimeF == (int)(bpm * 16 * 51) - 80) NoobBarrage.Intro53and54();
                if (GametimeF == (int)(bpm * 16 * 53) - 80) NoobBarrage.Intro55();
                if (GametimeF == (int)(bpm * 16 * 54) - 80) NoobBarrage.Intro55();
                if (GametimeF == (int)(bpm * 16 * 55) - 80) NoobBarrage.Intro57();
                if (GametimeF == (int)(bpm * 16 * 57))
                {
                    NoobBarrage.Intro59to61();
                    SetSoul(0);
                    SetBox(320 - 90, 320 + 90, 240 - 90, 240 + 90);
                }//设置红心，放攻击
                if (GametimeF == (int)(bpm * 16 * 57 + 25))
                {
                    CreateBone(new CentreCircleBone(0, 3, 240, bpm * 7 * 16));
                }
                if (GametimeF == (int)(bpm * 16 * 59)) NoobBarrage.Intro62and63();
                if (GametimeF == (int)(bpm * 16 * 61)) NoobBarrage.Intro64to67();
                if (GametimeF == (int)(bpm * 16 * 65))
                {
                    SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
                    TP();
                    SetSoul(1);
                }//设置绿心
                if (GametimeF == (int)(bpm * 16 * 65 + bpm * 4 - 80))
                {
                    for (int a = 0; a < 14; a++)
                    {
                        CreateArrow(80 + bpm * a * 4, "R", 6, 0, 0);
                        CreateArrow(80 + bpm * a * 4 + bpm, "+0", 6, 0, 0);
                        CreateArrow(80 + bpm * a * 4 + bpm * 2, "+0", 6, 0, 0);
                        CreateArrow(80 + bpm * a * 4 + bpm * 3, "+0", 6, 0, 0);
                    }
                }//蓝对红
                if (GametimeF == (int)(bpm * 16 * 69 - 80))
                {

                    for (int a = 0; a < 8; a++)
                    {
                        CreateArrow(80 + bpm * a * 4, "R", 6, 0, 0);
                        CreateArrow(80 + bpm * a * 4 + bpm, "+0", 6, 0, 0);
                        CreateArrow(80 + bpm * a * 4 + bpm * 2, "+0", 6, 0, 0);
                        CreateArrow(80 + bpm * a * 4 + bpm * 3, "+0", 6, 0, 0);
                    }
                    AddInstance(new InstantEvent(bpm * 32, () =>
                    {
                        for (int a = 0; a < 7; a++)
                        {
                            CreateArrow(80 + bpm * a * 4, "R", 6, 0, 0);
                            CreateArrow(80 + bpm * a * 4 + bpm, "+0", 6, 0, 0);
                            CreateArrow(80 + bpm * a * 4 + bpm * 2, "+0", 6, 0, 0);
                            CreateArrow(80 + bpm * a * 4 + bpm * 3, "+0", 6, 0, 0);
                        }
                    }));
                }//蓝对红，蓝红
                if (GametimeF == (int)(bpm * 16 * 73 - bpm - 80)) NoobBarrage.Intro76();
                if (GametimeF == (int)(bpm * 16 * 74 - bpm - 80)) NoobBarrage.Intro77();
                if (GametimeF == (int)(bpm * 16 * 75 - bpm - 80)) NoobBarrage.Intro78();
                if (GametimeF == (int)(bpm * 16 * 76 - bpm - 80)) NoobBarrage.Intro79and80();
                if (GametimeF == (int)(bpm * 16 * 78 - bpm - 80)) NoobBarrage.Intro81();
                if (GametimeF == (int)(bpm * 16 * 79 - bpm - 80)) NoobBarrage.Intro82();
                if (GametimeF == (int)(bpm * 16 * 80 - bpm - 80)) NoobBarrage.Intro83and84();
                if (GametimeF == (int)(bpm * 16 * 81 - 80)) NoobBarrage.Intro85();
                if (GametimeF == (int)(bpm * 16 * 82 - 80)) NoobBarrage.Intro86();
                if (GametimeF == (int)(bpm * 16 * 83 - 80)) NoobBarrage.Intro85();
                if (GametimeF == (int)(bpm * 16 * 84 - 80)) NoobBarrage.Intro86();
                if (GametimeF == (int)(bpm * 16 * 85 - 80)) NoobBarrage.Intro85();
                if (GametimeF == (int)(bpm * 16 * 86 - 80)) NoobBarrage.Intro86();
                if (GametimeF == (int)(bpm * 16 * 87 - 80)) NoobBarrage.Intro85();
                if (GametimeF == (int)(bpm * 16 * 88 - 80)) NoobBarrage.Intro87();
                if (GametimeF == (int)(bpm * 16 * 89 - 80)) NoobBarrage.Intro51and52();
                if (GametimeF == (int)(bpm * 16 * 91 - 80)) NoobBarrage.Intro53and54();
                if (GametimeF == (int)(bpm * 16 * 93 - 80)) NoobBarrage.Intro55();
                if (GametimeF == (int)(bpm * 16 * 94 - 80)) NoobBarrage.Intro55();
                if (GametimeF == (int)(bpm * 16 * 95 - 80)) NoobBarrage.Intro57();
                if (GametimeF == (int)(bpm * 16 * 97 - 80)) CreateArrow(80, 3, 13, 1, 0, ArrowAttribute.SpeedUp);
            }

            private static class ExtremeBarrage
            {
                public static void Intro5()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","/","/",
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "R","+0","+0","+0",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro6()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","+0","+0","+0",
                "/","/","/","/",
                "R","+0","+0","+0",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro16()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","+0","+0","+0",
                "/","/","/","/",
                "R","+0","+0","+0",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro17()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","R","/","R",
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
                "R","/","R","/",
                "R","/","R","/",
                "R","R","/","R",
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
                public static void Intro18()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
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
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro19()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","/","/",
                "$0","$2","$0","$2",


                };
                    int[] Bluearrowtype =
                    {
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
                1,1,1,1,
                };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, Bluearrowtype[a]);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "$0","$2","$0","$2",

                };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, Bluearrowtype[a]);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro23()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","R","+0",
                "R","+0","R","+0",
                "R","+0","/","/",
                "$0","$2","$0","$2",
            };
                    int[] Bluearrowtype =
                   {
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
                1,1,1,1,
                };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, Bluearrowtype[a]);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","/","/",
                "$0","$2","$0","$2",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, Bluearrowtype[a]);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro24()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
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
                };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro25()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
                "R","/","R","/",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro26()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro28()
                {
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro30()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
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
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }

                }
                public static void Intro32()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }

                }
                public static void AnotherIntro32()
                {
                    float Beattime = 0;
                    string[] Arrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "R","/","R","/",
                "R","/","R","/",
            };
                    for (int a = 0; a < Arrowway.Length; a++)
                    {
                        if (Arrowway[a] == "/")
                        {
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] != "/")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 6.2f, 0, 0);
                            CreateArrow(80 + Beattime + bpm, "+2", 6.2f, 1, 0);
                            Beattime += bpm;
                        }
                    }
                }
                public static void Intro33()
                {
                    float Beattime = 0;
                    string[] Arrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "/","/","/","/",
                "/","/","/","/",
            };
                    for (int a = 0; a < Arrowway.Length; a++)
                    {
                        if (Arrowway[a] == "/")
                        {
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] != "/")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 6.2f, 0, 0);
                            CreateArrow(80 + Beattime + bpm, "+2", 6.2f, 1, 0);
                            Beattime += bpm;
                        }
                    }


                }
                public static void Intro34()
                {

                    if (Rand(0, 1) == 0)
                    {
                        float Aspeed = 0;
                        float R = Rand(0, 360);
                        UpBone bone1 = new(true, 0.9f, Rand(24, 64));
                        DownBone bone2 = new(true, 0.9f, 128 - LastRand - 40);
                        for (int a = 0; a < 3; a++)
                        {
                            AddInstance(new TimeRangedEvent(a * 3, 1, () =>
                            {
                                CreateBone(bone1);
                                CreateBone(bone2);
                            }));
                        }
                        AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                        {
                            bone1.MissionLength = Sin(R + Aspeed) * 28 + 40;
                            bone2.MissionLength = Sin(-R - Aspeed) * 28 + 40;
                            Aspeed += 4.5f;
                        }));
                        AddInstance(new InstantEvent(bpm * 8, () => { CreateBone(new UpBone(true, 3.2f, 131) { ColorType = 1 }); }));
                        PlaySound(Sounds.pierce);
                    }
                    else
                    {
                        float Aspeed = 0;
                        float R = Rand(0, 360);
                        UpBone bone1 = new(false, 0.9f, Rand(24, 64));
                        DownBone bone2 = new(false, 0.9f, 128 - LastRand - 40);
                        for (int a = 0; a < 3; a++)
                        {
                            AddInstance(new TimeRangedEvent(a * 3, 1, () =>
                            {
                                CreateBone(bone1);
                                CreateBone(bone2);
                            }));
                        }
                        AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                        {
                            bone1.MissionLength = Sin(R + Aspeed) * 28 + 40;
                            bone2.MissionLength = Sin(-R - Aspeed) * 28 + 40;
                            Aspeed += 4.5f;
                        }));
                        AddInstance(new InstantEvent(bpm * 8, () => { CreateBone(new UpBone(false, 3.2f, 131) { ColorType = 1 }); }));
                        PlaySound(Sounds.pierce);
                    }
                }//蓝心骨
                public static void Intro51and52()
                {
                    float Beattime = 0;
                    string[] Arrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "RT","/","RT","/",
                "RT","/","RT","/",
                "RR","RB","RR","RB",
                "RB","/","RB","/",
            };
                    for (int a = 0; a < Arrowway.Length; a++)
                    {
                        if (Arrowway[a] == "/")
                        {
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "R")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 3, 0, 0);
                            CreateArrow(80 + Beattime + bpm, "+0", 15f, 1, 0);
                            CreateArrow(80 + Beattime + bpm * 2, "+0", 15f, 0, 0);
                            CreateArrow(80 + Beattime + bpm * 3, "+0", 15f, 0, 0);
                            CreateArrow(80 + Beattime + bpm * 3, "+0", 15f, 1, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "RT")
                        {
                            CreateArrow(80 + Beattime, "R", 6.2f, 0, 0);
                            CreateArrow(80 + Beattime, "R", 6.2f, 1, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "RB")
                        {
                            CreateArrow(80 + Beattime, "R", 6.2f, 0, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "RR")
                        {
                            CreateArrow(80 + Beattime, "R", 6.2f, 1, 0);
                            Beattime += bpm;
                        }
                    }
                }
                public static void Intro53and54()
                {
                    float Beattime = 0;
                    string[] Arrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "RT","/","RT","/",
                "RT","/","RT","/",
                "RB","RR","RB","RR",
                "RB","RR","/","/",
            };
                    for (int a = 0; a < Arrowway.Length; a++)
                    {
                        if (Arrowway[a] == "/")
                        {
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "R")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 3, 0, 0);
                            CreateArrow(80 + Beattime + bpm, "+0", 15f, 1, 0);
                            CreateArrow(80 + Beattime + bpm * 2, "+0", 15f, 0, 0);
                            CreateArrow(80 + Beattime + bpm * 3, "+0", 15f, 0, 0);
                            CreateArrow(80 + Beattime + bpm * 3, "+0", 15f, 1, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "RT")
                        {
                            CreateArrow(80 + Beattime, "R", 6.2f, 0, 0);
                            CreateArrow(80 + Beattime, "R", 6.2f, 1, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "RB")
                        {
                            CreateArrow(80 + Beattime, "R", 6.2f, 0, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "RR")
                        {
                            CreateArrow(80 + Beattime, "R", 6.2f, 1, 0);
                            Beattime += bpm;
                        }
                    }
                }
                public static void Intro55()
                {
                    {
                        float BeattimeB = 0;
                        string[] Bluearrowway =
                        {
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
                                CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
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
            };
                        for (int a = 0; a < Redarrowway.Length; a++)
                        {
                            if (Redarrowway[a] == "/")
                            {
                                BeattimeR += bpm;
                            }
                            else if (Redarrowway[a] != "/")
                            {
                                CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                                BeattimeR += bpm;
                            }
                        }

                    }
                }
                public static void Intro57()
                {
                    {
                        float BeattimeB = 0;
                        string[] Bluearrowway =
                        {
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
                                CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
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
            };
                        for (int a = 0; a < Redarrowway.Length; a++)
                        {
                            if (Redarrowway[a] == "/")
                            {
                                BeattimeR += bpm;
                            }
                            else if (Redarrowway[a] != "/")
                            {
                                CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                                BeattimeR += bpm;
                            }
                        }

                    }
                }
                public static void Intro58()
                {
                    {
                        float BeattimeB = 0;
                        string[] Bluearrowway =
                        {
                "R","/","R","R",
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
                                CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                                BeattimeB += bpm;
                            }
                        }
                        float BeattimeR = 0;
                        string[] Redarrowway =
                        {
                "R","+0","/","+0",
                "/","+0","/","+0",
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
                                CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 1, 0);
                                BeattimeR += bpm;
                            }
                        }

                    }
                }
                public static void Intro59to61()
                {
                    CreateEntity(new Boneslab(0, 7, 0, (int)(bpm * 8 + bpm * 6 * 16)));
                    CreateEntity(new Boneslab(90, 7, 0, (int)(bpm * 6 * 16)));
                    CreateEntity(new Boneslab(180, 7, 0, (int)(bpm * 8 + bpm * 6 * 16)));
                    CreateEntity(new Boneslab(270, 7, 0, (int)(bpm * 8 + bpm * 6 * 16)));
                    CreateGB(new NormalGB(
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(1, 0.4f),
                        bpm * 24,
                        bpm));
                    AddInstance(new InstantEvent(bpm * 6, () =>
                    {
                        CreateGB(new NormalGB(
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(1, 0.4f),
                        bpm * 24 - bpm * 6,
                        bpm));
                    }));
                    AddInstance(new InstantEvent(bpm * 10, () =>
                    {
                        CreateGB(new NormalGB(
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(1, 0.4f),
                        bpm * 24 - bpm * 10,
                        bpm));
                    }));
                }
                public static void Intro62and63()
                {
                    CreateGB(new NormalGB(
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(1, 0.4f),
                        bpm * 24,
                        bpm));
                    AddInstance(new InstantEvent(bpm * 6, () =>
                    {
                        CreateGB(new NormalGB(
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(1, 0.4f),
                        bpm * 24 - bpm * 6,
                        bpm));
                    }));
                    AddInstance(new InstantEvent(bpm * 16, () =>
                    {
                        CreateGB(new NormalGB(
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(1, 0.4f),
                        bpm * 24 - bpm * 16,
                        bpm));
                    }));
                    AddInstance(new InstantEvent(bpm * 20, () =>
                    {
                        CreateBone(new UpBone(false, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new RightBone(false, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new LeftBone(true, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new DownBone(true, 6.5f, 90) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    }));
                }
                public static void Intro64to67()
                {
                    CreateGB(new NormalGB(
                       new Vector2(Rand(160, 480), Rand(120, 360)),
                       new Vector2(Rand(160, 480), Rand(120, 360)),
                       new Vector2(1, 0.4f),
                       bpm * 24,
                       bpm));
                    AddInstance(new InstantEvent(bpm * 8, () =>
                    {
                        CreateGB(new NormalGB(
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(1, 0.4f),
                        bpm * 24 - bpm * 8,
                        bpm));
                    }));
                    AddInstance(new InstantEvent(bpm * 20, () =>
                    {
                        CreateGB(new NormalGB(
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(Rand(160, 480), Rand(120, 360)),
                        new Vector2(1, 0.4f),
                        bpm * 24 - bpm * 20,
                        bpm));
                    }));
                    AddInstance(new InstantEvent(bpm * 22, () =>
                    {
                        CreateBone(new UpBone(false, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new RightBone(false, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new LeftBone(true, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new DownBone(true, 6.5f, 90) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    }));
                    for (int a = 0; a < 6; a += 2)
                    {
                        AddInstance(new InstantEvent(bpm * 28 + bpm * 4 * a, () =>
                        {
                            CreateBone(new UpBone(true, 4f, 176) { ColorType = Rand(1, 2) });
                        }));
                    }

                }

                public static void Intro76()
                {
                    CreateGB(new GreenSoulGB(80, Rand(0, 3), 1, bpm * 10));
                    CreateArrow(80, LastRand, 6.7f, 1, 0);
                    CreateGB(new GreenSoulGB(80+bpm*14, Rand(0, 3), 1, bpm * 4));
                    CreateArrow(80 + bpm * 14, LastRand, 6.7f, 1, 0);
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.7f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }


                }
                public static void Intro77()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 14, Rand(0, 3), 1, bpm * 8));
                    CreateArrow(80 + bpm * 14, LastRand, 6.7f, 1, 0);
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.7f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }


                }
                public static void Intro78()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 14, Rand(0, 3), 1, bpm * 8));
                    CreateArrow(80 + bpm * 14, LastRand, 6.7f, 1, 0);
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "/","/","R","/",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.7f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }


                }
                public static void Intro79and80()
                {
                    CreateGB(new GreenSoulGB(80+bpm*16, Rand(0, 3), 1, bpm * 10));
                    CreateArrow(80 + bpm * 16, LastRand, 6.7f, 1, 0);
                    CreateGB(new GreenSoulGB(80 + bpm * 30, Rand(0, 3), 1, bpm * 4));
                    CreateArrow(80 + bpm * 30, LastRand, 6.7f, 1, 0);
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
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

                "/","/","/","/",
                "/","/","/","/",
                "/","/","/","/",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.7f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }


                }
                public static void Intro81()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 14, Rand(0, 3), 1, bpm * 10));
                    CreateArrow(80 + bpm * 14, LastRand, 6.7f, 1, 0);
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.7f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro82()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 14, Rand(0, 3), 1, bpm * 8));
                    CreateArrow(80 + bpm * 14, LastRand, 6.7f, 1, 0);
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "/","/","R","/",
                "R","/","/","/",

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
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.7f, 1, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro83and84()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "/","/","/","/",
                "/","/","/","/",
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro85()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","SB","/",
                "R","/","SB","/",
                "R","/","SB","/",
                "R","/","SB","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "SB")
                        {
                            CreateArrow(80 + BeattimeB, "R", 9, 0, 0, ArrowAttribute.SpeedUp);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "/","/","SR","/",
                "/","/","SR","/",
                "/","/","SR","/",
                "/","/","SR","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.7f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "SR")
                        {
                            CreateArrow(80 + BeattimeR, "R", 9, 1, 0, ArrowAttribute.SpeedUp);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro86()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","/","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 5.3f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "SB")
                        {
                            CreateArrow(80 + BeattimeB, "R", 9, 0, 0, ArrowAttribute.SpeedUp);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "SR","/","/","/",
                "SR","/","/","/",
                "SR","/","/","/",
                "SR","/","/","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 5.3f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "SR")
                        {
                            CreateArrow(80 + BeattimeR, "R", 9, 1, 0, ArrowAttribute.SpeedUp);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro87()
                {

                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","/","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 5.3f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "SB")
                        {
                            CreateArrow(80 + BeattimeB, "R", 9, 0, 0, ArrowAttribute.SpeedUp);
                            BeattimeB += bpm;
                        }
                    }
                    float BeattimeR = 0;
                    string[] Redarrowway =
                    {
                "SR","/","","/",
                "SR","/","","/",
                "SR","/","","/",
                "SR","/","/","/",
            };
                    for (int a = 0; a < Redarrowway.Length; a++)
                    {
                        if (Redarrowway[a] == "/")
                        {
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 5.3f, 1, 0);
                            BeattimeR += bpm;
                        }
                        else if (Redarrowway[a] == "SR")
                        {
                            CreateArrow(80 + BeattimeR, "R", 9, 1, 0, ArrowAttribute.SpeedUp);
                            BeattimeR += bpm;
                        }
                    }
                }
            }

            private static class NoobBarrage
            {
                public static void Intro5()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "/","/","/","/",
                "R","/","+0","/",
                "+0","/","+0","/",
                "R","/","+0","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, a % 5 % 2);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro6()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","+0","/",
                "+0","/","+0","/",
                "+0","/","+0","/",
                "+0","/","+0","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, a % 5 % 2);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro16()
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
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro17()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","R","/",
                "R","/","+0","/",
                "R","+0","/","R",
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
                            CreateArrow(80 + BeattimeB, "R", 6.5f, 1, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro18()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro19()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","+0","+0",
                "+0","+0","+0","+0",
                "+2","+0","/","/",
                "$3","$3","$3","$3",


                };
                    int[] Bluearrowtype =
                    {
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, Bluearrowtype[a]);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro23()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","+0","+0","+0",
                "+0","+0","+0","+0",
                "+2","+0","/","/",
                "$3","$3","$3","$3",
            };
                    int[] Bluearrowtype =
                   {
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
                1,1,0,0,
                };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, Bluearrowtype[a]);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro24()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","+0","/",
                "R","/","+0","/",
                "R","/","+0","/",
                "R","/","+0","/",
                };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro25()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "R","/","/","/",
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
                            CreateArrow(80 + BeattimeB, "R", 6.2f, 1, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro26()
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
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro28()
                {
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
                        else if (Redarrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeR, Redarrowway[a], 6.2f, 0, 0);
                            BeattimeR += bpm;
                        }
                    }
                }
                public static void Intro30()
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
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }

                }
                public static void Intro32()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","+0","/",
                "R","/","+0","/",
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
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }

                }
                public static void AnotherIntro32()
                {
                    float Beattime = 0;
                    string[] Arrowway =
                    {
                "/","/","/","/",
                "/","/","/","/",
                "R","/","+0","/",
                "R","/","+0","/",
            };
                    for (int a = 0; a < Arrowway.Length; a++)
                    {
                        if (Arrowway[a] == "/")
                        {
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] != "/")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 6.2f, 0, 0);
                            CreateArrow(80 + Beattime + bpm, "+0", 6.2f, 0, 0);
                            Beattime += bpm;
                        }
                    }
                }
                public static void Intro33()
                {
                    float Beattime = 0;
                    string[] Arrowway =
                    {
                "R","/","R","/",
                "R","/","R","/",
                "/","/","/","/",
                "/","/","/","/",
            };
                    for (int a = 0; a < Arrowway.Length; a++)
                    {
                        if (Arrowway[a] == "/")
                        {
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] != "/")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 6.2f, 0, 0);
                            Beattime += bpm;
                        }
                    }


                }
                public static void Intro34()
                {

                    if (Rand(0, 1) == 0)
                    {
                        UpBone bone1 = new(true, 0.8f, Rand(24, 64));
                        DownBone bone2 = new(true, 0.8f, 128 - LastRand - 40);
                        for (int a = 0; a < 3; a++)
                        {
                            AddInstance(new TimeRangedEvent(a * 3, 1, () =>
                            {
                                CreateBone(bone1);
                                CreateBone(bone2);
                            }));
                        }
                        PlaySound(Sounds.pierce);
                    }
                    else
                    {
                        UpBone bone1 = new(false, 0.8f, Rand(24, 64));
                        DownBone bone2 = new(false, 0.8f, 128 - LastRand - 40);
                        for (int a = 0; a < 3; a++)
                        {
                            AddInstance(new TimeRangedEvent(a * 3, 1, () =>
                            {
                                CreateBone(bone1);
                                CreateBone(bone2);
                            }));
                        }
                        PlaySound(Sounds.pierce);
                    }
                }//蓝心骨
                public static void Intro51and52()
                {
                    float Beattime = 0;
                    string[] Arrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "RB","/","+0","/",
                "RB","/","+0","/",
                "+0","+0","+0","+0",
                "RB","/","RB","/",
            };
                    for (int a = 0; a < Arrowway.Length; a++)
                    {
                        if (Arrowway[a] == "/")
                        {
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "R")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 3, 0, 0);
                            CreateArrow(80 + Beattime + bpm, "+0", 15f, 0, 0);
                            CreateArrow(80 + Beattime + bpm * 2, "+0", 15f, 0, 0);
                            CreateArrow(80 + Beattime + bpm * 3, "+0", 15f, 0, 0);
                            CreateArrow(80 + Beattime + bpm * 3, "+0", 15f, 0, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] != "/" && Arrowway[a] != "R")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 6.2f, 0, 0);
                            Beattime += bpm;
                        }
                    }
                }
                public static void Intro53and54()
                {
                    float Beattime = 0;
                    string[] Arrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "RR","/","+0","/",
                "RR","/","+0","/",
                "+0","+0","+0","+0",
                "RR","RR","/","/",
            };
                    for (int a = 0; a < Arrowway.Length; a++)
                    {
                        if (Arrowway[a] == "/")
                        {
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "R")
                        {
                            CreateArrow(80 + Beattime, Arrowway[a], 3, 1, 0);
                            CreateArrow(80 + Beattime + bpm, "+0", 15f, 1, 0);
                            CreateArrow(80 + Beattime + bpm * 2, "+0", 15f, 1, 0);
                            CreateArrow(80 + Beattime + bpm * 3, "+0", 15f, 1, 0);
                            CreateArrow(80 + Beattime + bpm * 3, "+0", 15f, 1, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "RR")
                        {
                            CreateArrow(80 + Beattime, "R", 6.2f, 0, 0);
                            Beattime += bpm;
                        }
                        else if (Arrowway[a] == "+0")
                        {
                            CreateArrow(80 + Beattime, "+0", 6.2f, 0, 0);
                            Beattime += bpm;
                        }
                    }
                }
                public static void Intro55()
                {

                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","+0","/",
                "R","/","+0","/",
                "R","/","+0","/",
                "R","/","+0","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] != "/")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                            BeattimeB += bpm;
                        }
                    }


                }
                public static void Intro57()
                {
                    {
                        float BeattimeB = 0;
                        string[] Bluearrowway =
                        {
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
                                CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                                BeattimeB += bpm;
                            }
                        }

                    }
                }
                public static void Intro58()
                {
                    {
                        float BeattimeB = 0;
                        string[] Bluearrowway =
                        {
                "R","+0","+2","+0",
                "R","+0","+2","+0",
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
                                CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.2f, 0, 0);
                                BeattimeB += bpm;
                            }
                        }

                    }
                }
                public static void Intro59to61()
                {
                    CreateEntity(new Boneslab(90, 7, 0, (int)(bpm * 6 * 16)));
                    CreateEntity(new Boneslab(270, 7, 0, (int)(bpm * 8 + bpm * 6 * 16)));
                    AddInstance(new InstantEvent(bpm * 24, () =>
                    {
                        CreateBone(new UpBone(false, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new DownBone(true, 6.5f, 90) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    }));
                }
                public static void Intro62and63()
                {
                    AddInstance(new InstantEvent(bpm * 24, () =>
                    {
                        CreateBone(new UpBone(false, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new DownBone(true, 6.5f, 90) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    }));
                }
                public static void Intro64to67()
                {
                    AddInstance(new InstantEvent(bpm * 24, () =>
                    {
                        CreateBone(new UpBone(false, 6.5f, 90) { ColorType = 2 });
                        CreateBone(new DownBone(true, 6.5f, 90) { ColorType = 2 });
                        PlaySound(Sounds.pierce);
                    }));
                    for (int a = 0; a < 3; a++)
                    {
                        AddInstance(new InstantEvent(bpm * 28 + bpm * 8 * a, () =>
                        {
                            CreateBone(new UpBone(true, 4f, 176) { ColorType = 2 });
                        }));
                    }

                }


                public static void Intro76()
                {
                    CreateGB(new GreenSoulGB(80, 0, 0, bpm * 10));
                    CreateArrow(80, 0, 6.7f, 0, 0);
                    CreateGB(new GreenSoulGB(80 + bpm * 14, 0, 0, bpm * 4));
                    CreateArrow(80 + bpm * 14, 0, 6.7f, 0, 0);

                }
                public static void Intro77()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 14, 0, 0, bpm * 8));
                    CreateArrow(80 + bpm * 14, 0, 6.7f, 0, 0);
                }
                public static void Intro78()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 14, 2, 0, bpm * 8));
                    CreateArrow(80 + bpm * 14, 2, 6.7f, 0, 0);

                }
                public static void Intro79and80()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 16, 2, 0, bpm * 10));
                    CreateArrow(80 + bpm * 16, 2, 6.7f, 0, 0);
                    CreateGB(new GreenSoulGB(80 + bpm * 30, 2, 0, bpm * 4));
                    CreateArrow(80 + bpm * 30, 2, 6.7f, 0, 0);


                }
                public static void Intro81()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 14, 0, 0, bpm * 10));
                    CreateArrow(80 + bpm * 14, 0, 6.7f, 0, 0);
                }
                public static void Intro82()
                {
                    CreateGB(new GreenSoulGB(80 + bpm * 14, 0, 0, bpm * 8));
                    CreateArrow(80 + bpm * 14, 0, 6.7f, 0, 0);
                }
                public static void Intro83and84()
                {


                }
                public static void Intro85()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","/","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 6.7f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "SB")
                        {
                            CreateArrow(80 + BeattimeB, "+0", 9, 0, 0, ArrowAttribute.SpeedUp);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro86()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","/","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 5.3f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "SB")
                        {
                            CreateArrow(80 + BeattimeB, "+0", 9, 0, 0, ArrowAttribute.SpeedUp);
                            BeattimeB += bpm;
                        }
                    }
                }
                public static void Intro87()
                {

                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","R","/",
                "SB","/","/","/",
            };
                    for (int a = 0; a < Bluearrowway.Length; a++)
                    {
                        if (Bluearrowway[a] == "/")
                        {
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "R")
                        {
                            CreateArrow(80 + BeattimeB, Bluearrowway[a], 5.3f, 0, 0);
                            BeattimeB += bpm;
                        }
                        else if (Bluearrowway[a] == "SB")
                        {
                            CreateArrow(80 + BeattimeB, "+0", 9, 0, 0, ArrowAttribute.SpeedUp);
                            BeattimeB += bpm;
                        }
                    }
                }
            }
            #region Non
            public void Easy()
            {


            }
            public void Hard()
            {
                LT line1 = new(new(320, 0), new(320, 640), 1111, 1);
                if (GametimeF == 120) CreateEntity(line1);
                if (GametimeF == 180)
                {



                }
            }
            public void Normal()
            {
                if (GametimeF == 1)
                {
                    SetSoul(3);
                }
                if (GametimeF == (int)(bpm * 8))
                {
                    ExtremeBarrage.Intro59to61();
                }
                if (GametimeF >= (int)(bpm * 8) && GametimeF <= (int)(bpm * 16 + bpm * 8))
                {
                    SetBox(
                        320 - 90 + (GametimeF - bpm * 8) * (GametimeF - bpm * 8) * 0.03f,
                        320 + 90 + (GametimeF - bpm * 8) * (GametimeF - bpm * 8) * 0.03f,
                        240 - 90,
                        240 + 90
                        );
                }
                AddInstance(new InstantEvent(bpm * 16 + bpm * 8, () =>
                {
                    float BoxU = BoxStates.Up;
                    float BoxR = BoxStates.Right;
                    if (GametimeF >= (int)(bpm * 16 + bpm * 8) && GametimeF <= (int)(bpm * 16 * 2 + bpm * 8))
                    {
                        SetBox(
                            BoxU - (GametimeF - bpm * 16 - bpm * 8) * (GametimeF - bpm * 16 - bpm * 8) * 0.03f,
                            BoxU + 180 - (GametimeF - bpm * 16 - bpm * 8) * (GametimeF - bpm * 16 - bpm * 8) * 0.03f,
                            240 - 90,
                            240 + 90
                            );
                    }
                }));
                if (GametimeF == (int)(bpm * 48))
                {
                    CreateBone(new UpBone(false, 6.5f, 90) { ColorType = 2 });
                    CreateBone(new RightBone(false, 6.5f, 90) { ColorType = 2 });
                    CreateBone(new LeftBone(true, 6.5f, 90) { ColorType = 2 });
                    CreateBone(new DownBone(true, 6.5f, 90) { ColorType = 2 });
                    PlaySound(Sounds.pierce);
                }
            }
            public void ExtremePlus()
            {

            }
            #endregion
        }
    }
}