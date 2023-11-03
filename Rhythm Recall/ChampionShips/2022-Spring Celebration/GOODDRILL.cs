using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class Gooddrill : IChampionShip
    {
        public Gooddrill()
        {
            difficulties = new();
            difficulties.Add("div.2", Difficulty.Normal);
            difficulties.Add("div.1", Difficulty.Hard);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new GOODDRILL();

        public class GOODDRILL : IWaveSet
        {
            public string Music => "GOODDRILL";

            public string FightName => "GOODDRILL";

            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 9.5f),
                            new(Difficulty.Hard, 14.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 9.0f),
                            new(Difficulty.Hard, 14.2f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 15.5f),
                            new(Difficulty.Hard, 19.5f),
                        }
                    );
                public override string BarrageAuthor => "zkronO";
                public override string AttributeAuthor => "zkronO";
                public override string PaintAuthor => "OtokP";
                public override string SongAuthor => "EBIMAYO";
            }
            public SongInformation Attributes => new ThisInformation();
            public static float bpm = 5.4505f;

            public int zero = 0;

            static class HardBarrage
            {
                public static void A1()
                {
                    SetBox(240, 180, 180);
                    CreateBone(new CentreCircleBone(0, 4, 160, 330) { ColorType = 2 });
                    CreateBone(new CentreCircleBone(0, -2.75f, 160, 330) { ColorType = 1 });
                    for (int i = 0; i <= 8; i++)
                    {
                        CreateBone(new SideCircleBone(0 + i * 360 / 8, 2.25f, 40, 360));
                    }
                }
                public static void A2()
                {
                    PlayerInstance.hpControl.GiveProtectTime(5, true);
                    TP(30, 390);
                    SetSoul(2);
                    SetBox(20, 100, 320, 400);
                    CreateBone(new CustomBone(new(70, 400), Motions.PositionRoute.cameFromDown, 0, 60, 380));
                    CreateBone(new CustomBone(new(70, 80), Motions.PositionRoute.cameFromUp, 0, 510, 380));
                    CreateBone(new CustomBone(new(110, 400), Motions.PositionRoute.cameFromDown, 0, 40, 380));
                    CreateBone(new CustomBone(new(110, 80), Motions.PositionRoute.cameFromUp, 0, 530, 380));
                    CreateBone(new CustomBone(new(206, 405), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(206, 390), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(206, 375), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(206, 360), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(206, 345), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(126, 330), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(206, 330), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(123, 315), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(203, 315), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(120, 300), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(200, 300), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(125, 285), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(205, 285), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(130, 270), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(210, 270), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(127, 255), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(207, 255), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(124, 240), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(204, 240), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(119, 225), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(199, 225), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(116, 210), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(196, 210), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(116, 195), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(116, 180), Motions.PositionRoute.cameFromRight, 90, 55, 380));
                    CreateBone(new CustomBone(new(116, 165), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(116, 150), Motions.PositionRoute.cameFromRight, 90, 55, 380));
                    CreateBone(new CustomBone(new(116, 135), Motions.PositionRoute.cameFromLeft, 90, 55, 380));
                    CreateBone(new CustomBone(new(116, 120), Motions.PositionRoute.cameFromRight, 90, 55, 380));
                }
                public static void A3()
                {
                    CreateBone(new CustomBone(new(Heart.Centre.X, 240 + 120), Motions.PositionRoute.linear, 0, 30)
                    {
                        PositionRouteParam = new float[] { 0, -10 }
                    });
                }
                public static void A4()
                {
                    CreateBone(new SwarmBone(120, 210, 0, 655) { ColorType = 1 });
                    CreateBone(new SwarmBone(120, 210, 70, 655) { ColorType = 2 });
                    CreateBone(new SwarmBone(120, 210, 140, 655) { ColorType = 1 });
                }
                public static void A5()
                {
                    CreateBone(new CustomBone(new(430, Rand(240, 240 + 120)), Motions.PositionRoute.linear, 90, 20)
                    {
                        PositionRouteParam = new float[] { Rand(-4, -3), 0 }
                    });
                    CreateBone(new CustomBone(new(210, Rand(240 - 120, 240)), Motions.PositionRoute.linear, 90, 20)
                    {
                        PositionRouteParam = new float[] { Rand(3, 4), 0 }
                    });
                }
                public static void A6()
                {
                    DownBone bone2 = new(true, 1.25f, Rand(30, 90));
                    DownBone bone3 = new(true, 1.2f, LastRand);
                    UpBone bone4 = new(true, 1.25f, 120 - LastRand);
                    UpBone bone5 = new(true, 1.2f, 120 - LastRand);
                    DownBone bone1 = new(true, 6, 190);
                    CreateBone(bone1);
                    CreateBone(bone2);
                    CreateBone(bone3);
                    CreateBone(bone4);
                    CreateBone(bone5);
                    AddInstance(new TimeRangedEvent(0, 42.5f, () =>
                    {
                        bone1.Speed -= 0.25f;
                        bone2.Speed += 0.125f;
                        bone3.Speed += 0.125f;
                        bone4.Speed += 0.125f;
                        bone5.Speed += 0.125f;
                    }));
                    SetBox(230 + 10, 410 + 10, 240 - 90, 240 + 90);
                }
                public static void A7()
                {
                    DownBone bone2 = new(false, 1.25f, Rand(30, 90));
                    DownBone bone3 = new(false, 1.2f, LastRand);
                    UpBone bone4 = new(false, 1.25f, 120 - LastRand);
                    UpBone bone5 = new(false, 1.2f, 120 - LastRand);
                    DownBone bone1 = new(false, 6, 190);
                    CreateBone(bone1);
                    CreateBone(bone2);
                    CreateBone(bone3);
                    CreateBone(bone4);
                    CreateBone(bone5);
                    AddInstance(new TimeRangedEvent(0, 42.5f, () =>
                    {
                        bone1.Speed -= 0.25f;
                        bone2.Speed += 0.125f;
                        bone3.Speed += 0.125f;
                        bone4.Speed += 0.125f;
                        bone5.Speed += 0.125f;
                    }));
                    SetBox(230 - 10, 410 - 10, 240 - 90, 240 + 90);
                }
                public static void A8()
                {
                    CreateBone(new CustomBone(new(320 - 120, 240 + 90), Motions.PositionRoute.linear, -30, 80)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320 + 120, 240 + 90), Motions.PositionRoute.linear, 30, 80)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                    CreateBone(new CustomBone(new(320 - 240, 240 - 90), Motions.PositionRoute.linear, -30, 260)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320 + 240, 240 - 90), Motions.PositionRoute.linear, 30, 260)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                    LeftBone bone1 = new(false, 6, 220) { ColorType = 1 };
                    CreateBone(bone1);
                    AddInstance(new TimeRangedEvent(0, 85, () =>
                    {
                        bone1.Speed -= 0.175f;
                    }));
                }
                public static void A9()
                {
                    CreateBone(new CustomBone(new(320 - 240, 240 + 90), Motions.PositionRoute.linear, 30, 260)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320 + 240, 240 + 90), Motions.PositionRoute.linear, -30, 260)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                    CreateBone(new CustomBone(new(320 - 120, 240 - 90), Motions.PositionRoute.linear, 30, 80)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320 + 120, 240 - 90), Motions.PositionRoute.linear, -30, 80)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                    LeftBone bone1 = new(true, 6, 220) { ColorType = 1 };
                    CreateBone(bone1);
                    AddInstance(new TimeRangedEvent(0, 85, () =>
                    {
                        bone1.Speed -= 0.175f;
                    }));
                }
                #region 没啥diao用的GB和骨墙（
                //一部分
                public static void A10()
                {
                    CreateEntity(new NormalGB(new(Rand(40, 600), Rand(20, 460)), new(320, 240), new(0.75f, 0.45f), Rand(0, 359), 150, 20));
                    CreateEntity(new Boneslab(270, 55, 0, 20));
                }
                public static void A11()
                {
                    CreateEntity(new Boneslab(270, 110, 0, 10));
                }
                public static void A12()
                {
                    CreateEntity(new Boneslab(270, 165, 0, 10));
                }
                public static void A13()
                {
                }
                public static void A14()
                {
                    CreateEntity(new Boneslab(90, 55, 0, 20));
                }
                public static void A15()
                {
                    CreateEntity(new NormalGB(new(Rand(40, 600), Rand(20, 460)), new(320, 240), new(0.75f, 0.45f), Rand(0, 359), 50, 20));
                    CreateEntity(new Boneslab(90, 110, 0, 10));
                }
                public static void A16()
                {
                    CreateEntity(new Boneslab(90, 165, 0, 10));
                }
                public static void A17()
                {
                }
                //另一部分
                public static void A010()
                {
                    CreateEntity(new NormalGB(new(Rand(40, 600), Rand(20, 460)), new(320, 240), new(0.75f, 0.45f), Rand(0, 359), 70, 20));
                    CreateEntity(new Boneslab(270, 55, 0, 20));
                }
                public static void A011()
                {
                    CreateEntity(new Boneslab(270, 110, 0, 10));
                }
                public static void A012()
                {
                    CreateEntity(new Boneslab(270, 165, 0, 10));
                }
                public static void A013()
                {
                }
                public static void A014()
                {
                    CreateEntity(new Boneslab(90, 55, 0, 20));
                }
                public static void A015()
                {
                    CreateEntity(new NormalGB(new(Rand(40, 600), Rand(20, 460)), new(320, 240), new(0.75f, 0.45f), Rand(0, 359), 130, 20));
                    CreateEntity(new Boneslab(90, 110, 0, 10));
                }
                public static void A016()
                {
                    CreateEntity(new Boneslab(90, 165, 0, 10));
                }
                public static void A017()
                {
                }
                #endregion
                public static void A18()
                {
                    for (int i = 0; i <= 12; i++)
                    {
                        CreateBone(new SideCircleBone(360 / 12 * i, 6, 40, 20));
                    }
                    CreateEntity(new Boneslab(0, 130, 0, 20) { ColorType = 2 });
                }
                public static void A19()
                {
                    int R = Rand(1, 3);
                    if (R == 1)
                    {
                        CreateBone(new CustomBone(new(600, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                    }
                    if (R == 2)
                    {
                        CreateBone(new CustomBone(new(600, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                    }
                    if (R == 3)
                    {
                        CreateBone(new CustomBone(new(600, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                    }
                }
                public static void A20()
                {
                    int R = Rand(1, 3);
                    if (R == 1)
                    {
                        CreateBone(new CustomBone(new(40, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                    }
                    if (R == 2)
                    {
                        CreateBone(new CustomBone(new(40, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                    }
                    if (R == 3)
                    {
                        CreateBone(new CustomBone(new(40, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                    }
                }
                public static void A21()
                {
                    SetSoul(2);
                    CreateEntity(new Boneslab(0, 30, 30, 670));
                    CreateEntity(new Boneslab(180, 30, 30, 1320));
                }
                public static void A22()
                {
                    CreateEntity(new Platform(1, new(320, 280), (s) => { return new Vector2(Sin(s.AppearTime * 1.25f) * 140, 0); }, 0, 50, 640));
                    CreateEntity(new Platform(1, new(320, 280), (s) => { return new Vector2(Sin(s.AppearTime * -1.25f) * 140, 0); }, 0, 50, 640));
                    CreateEntity(new Platform(0, new(320, 200), (s) => { return new Vector2(Sin(s.AppearTime * 1.55f) * 120, 0); }, 0, 50, 640));
                    CreateEntity(new Platform(0, new(320, 200), (s) => { return new Vector2(Sin(s.AppearTime * -1.55f) * 120, 0); }, 0, 50, 640));
                    LeftBone rb1 = new(true, 5, 600) { ColorType = 2 };
                    LeftBone rb2 = new(false, 5, 600) { ColorType = 2 };
                    CreateBone(rb1);
                    CreateBone(rb2);
                    AddInstance(new TimeRangedEvent(30, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(70, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(110, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(150, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(190, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(230, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(270, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(310, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(350, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(390, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(430, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(470, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(510, 1500, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                }
                public static void A23()
                {
                    CreateBone(new CustomBone(new(580, 240), (s) => { return new Vector2(Sin(s.AppearTime * -2.15f) * 160, 0); }, 0, 200));
                    CreateEntity(new NormalGB(new(Rand(40, 600), 120), new(320, -20), new(0.75f, 0.5f), 0, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 120), new(660, -20), new(0.75f, 0.5f), 60, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 120), new(660, 500), new(0.75f, 0.5f), 120, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 120), new(320, 500), new(0.75f, 0.5f), 180, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 120), new(-20, 500), new(0.75f, 0.5f), 240, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 120), new(-20, -20), new(0.75f, 0.5f), 300, 60, 20));
                }
                public static void A24()
                {
                    CreateBone(new CustomBone(new(60, 240), (s) => { return new Vector2(Sin(s.AppearTime * 2.15f) * 160, 0); }, 0, 200));
                    CreateEntity(new NormalGB(new(Rand(40, 600), 360), new(320, -20), new(0.75f, 0.5f), 0, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 360), new(660, -20), new(0.75f, 0.5f), 60, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 360), new(660, 500), new(0.75f, 0.5f), 120, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 360), new(320, 500), new(0.75f, 0.5f), 180, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 360), new(-20, 500), new(0.75f, 0.5f), 240, 60, 20));
                    CreateEntity(new NormalGB(new(LastRand, 360), new(-20, -20), new(0.75f, 0.5f), 300, 60, 20));
                }
                public static void A25()
                {
                    CreateBone(new CustomBone(new(40, 120), Motions.PositionRoute.linear, 40, 240) { PositionRouteParam = new float[] { 8, 0 }, ColorType = 2 });
                    CreateBone(new CustomBone(new(40, 360), Motions.PositionRoute.linear, 130, 240) { PositionRouteParam = new float[] { 8, 0 }, ColorType = 2 });
                    CreateBone(new CustomBone(new(600, 120), Motions.PositionRoute.linear, 130, 240) { PositionRouteParam = new float[] { -8, 0 }, ColorType = 2 });
                    CreateBone(new CustomBone(new(600, 360), Motions.PositionRoute.linear, 40, 240) { PositionRouteParam = new float[] { -8, 0 }, ColorType = 2 });
                }
                public static void A26()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 80)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 80)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A27()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 60)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 60)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A28()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 45)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 45)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A29()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 34)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 34)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A30()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 26)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 26)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A31()
                {

                }
                public static void A32()
                {

                }
                public static void A33()
                {

                }
                public static void A34()
                {

                }
                public static void A35()
                {

                }
                public static void A36()
                {

                }
                public static void A37()
                {

                }
                public static void A38()
                {

                }
                public static void A39()
                {

                }
                public static void A40()
                {

                }
                public static void A41()
                {

                }
            }
            static class NormalBarrage
            {
                public static void A1()
                {
                    SetBox(240, 180, 180);
                    CreateBone(new CentreCircleBone(0, -2.75f, 160, 330));
                    for (int i = 0; i <= 8; i++)
                    {
                        CreateBone(new SideCircleBone(0 + i * 360 / 8, 2.25f, 40, 360));
                    }
                }
                public static void A2()
                {
                    TP(30, 390);
                    SetSoul(2);
                    SetBox(20, 100, 320, 400);
                    PlayerInstance.hpControl.GiveProtectTime(5,true);
                    CreateBone(new CustomBone(new(70, 400), Motions.PositionRoute.cameFromDown, 0, 40, 380));
                    CreateBone(new CustomBone(new(70, 80), Motions.PositionRoute.cameFromUp, 0, 500, 380));
                    CreateBone(new CustomBone(new(110, 400), Motions.PositionRoute.cameFromDown, 0, 30, 380));
                    CreateBone(new CustomBone(new(110, 80), Motions.PositionRoute.cameFromUp, 0, 520, 380));
                    CreateBone(new CustomBone(new(206, 405), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(206, 390), Motions.PositionRoute.cameFromLeft, 90, 35, 380));
                    CreateBone(new CustomBone(new(206, 375), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(206, 360), Motions.PositionRoute.cameFromLeft, 90, 35, 380));
                    CreateBone(new CustomBone(new(206, 345), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(126, 330), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(206, 330), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(123, 315), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(203, 315), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(120, 300), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(200, 300), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(125, 285), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(205, 285), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(130, 270), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(210, 270), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(127, 255), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(207, 255), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(124, 240), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(204, 240), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(119, 225), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(199, 225), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(116, 210), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(196, 210), Motions.PositionRoute.cameFromRight, 90, 35, 380));
                    CreateBone(new CustomBone(new(116, 195), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(116, 180), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(116, 165), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(116, 150), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                    CreateBone(new CustomBone(new(116, 135), Motions.PositionRoute.cameFromLeft, 90, 45, 380));
                    CreateBone(new CustomBone(new(116, 120), Motions.PositionRoute.cameFromRight, 90, 45, 380));
                }
                public static void A3()
                {
                    CreateBone(new CustomBone(new(Heart.Centre.X, 240 + 120), Motions.PositionRoute.linear, 0, 30)
                    {
                        PositionRouteParam = new float[] { 0, -10 }
                    });
                }
                public static void A4()
                {
                    CreateBone(new SwarmBone(120, 210, 0, 655) { ColorType = 2 });
                    CreateBone(new SwarmBone(120, 210, 70, 655) { ColorType = 2 });
                    CreateBone(new SwarmBone(120, 210, 140, 655) { ColorType = 2 });
                }
                public static void A5()
                {
                    CreateBone(new CustomBone(new(210, Rand(240 - 120, 240 + 120)), Motions.PositionRoute.linear, 90, 20)
                    {
                        PositionRouteParam = new float[] { Rand(3, 4), 0 }
                    });
                }
                public static void A6()
                {
                    DownBone bone2 = new(true, 1.25f, Rand(30, 90));
                    DownBone bone3 = new(true, 1.2f, LastRand);
                    UpBone bone4 = new(true, 1.25f, 120 - LastRand);
                    UpBone bone5 = new(true, 1.2f, 120 - LastRand);
                    CreateBone(bone2);
                    CreateBone(bone3);
                    CreateBone(bone4);
                    CreateBone(bone5);
                    AddInstance(new TimeRangedEvent(0, 42.5f, () =>
                    {
                        bone2.Speed += 0.125f;
                        bone3.Speed += 0.125f;
                        bone4.Speed += 0.125f;
                        bone5.Speed += 0.125f;
                    }));
                    SetBox(230 + 10, 410 + 10, 240 - 90, 240 + 90);
                }
                public static void A7()
                {
                    DownBone bone2 = new(false, 1.25f, Rand(30, 90));
                    DownBone bone3 = new(false, 1.2f, LastRand);
                    UpBone bone4 = new(false, 1.25f, 120 - LastRand);
                    UpBone bone5 = new(false, 1.2f, 120 - LastRand);
                    DownBone bone1 = new(false, 6, 190);
                    CreateBone(bone2);
                    CreateBone(bone3);
                    CreateBone(bone4);
                    CreateBone(bone5);
                    AddInstance(new TimeRangedEvent(0, 42.5f, () =>
                    {
                        bone2.Speed += 0.125f;
                        bone3.Speed += 0.125f;
                        bone4.Speed += 0.125f;
                        bone5.Speed += 0.125f;
                    }));
                    SetBox(230 - 10, 410 - 10, 240 - 90, 240 + 90);
                }
                public static void A8()
                {
                    CreateBone(new CustomBone(new(320 - 120, 240 + 90), Motions.PositionRoute.linear, -30, 80)
                    {
                        PositionRouteParam = new float[] { 3.55f, 0 }
                    });
                    CreateBone(new CustomBone(new(320 + 120, 240 + 90), Motions.PositionRoute.linear, 30, 80)
                    {
                        PositionRouteParam = new float[] { -3.55f, 0 }
                    });
                    CreateBone(new CustomBone(new(320 - 240, 240 - 90), Motions.PositionRoute.linear, -30, 260)
                    {
                        PositionRouteParam = new float[] { 3.55f, 0 }
                    });
                    CreateBone(new CustomBone(new(320 + 240, 240 - 90), Motions.PositionRoute.linear, 30, 260)
                    {
                        PositionRouteParam = new float[] { -3.55f, 0 }
                    });
                    LeftBone bone1 = new(false, 6, 220) { ColorType = 2 };
                    CreateBone(bone1);
                    AddInstance(new TimeRangedEvent(0, 85, () =>
                    {
                        bone1.Speed -= 0.125f;
                    }));
                }
                public static void A9()
                {
                    CreateBone(new CustomBone(new(320 - 240, 240 + 90), Motions.PositionRoute.linear, 30, 260)
                    {
                        PositionRouteParam = new float[] { 3.55f, 0 }
                    });
                    CreateBone(new CustomBone(new(320 + 240, 240 + 90), Motions.PositionRoute.linear, -30, 260)
                    {
                        PositionRouteParam = new float[] { -3.55f, 0 }
                    });
                    CreateBone(new CustomBone(new(320 - 120, 240 - 90), Motions.PositionRoute.linear, 30, 80)
                    {
                        PositionRouteParam = new float[] { 3.55f, 0 }
                    });
                    CreateBone(new CustomBone(new(320 + 120, 240 - 90), Motions.PositionRoute.linear, -30, 80)
                    {
                        PositionRouteParam = new float[] { -3.55f, 0 }
                    });
                    LeftBone bone1 = new(true, 6, 220) { ColorType = 2 };
                    CreateBone(bone1);
                    AddInstance(new TimeRangedEvent(0, 85, () =>
                    {
                        bone1.Speed -= 0.125f;
                    }));
                }
                #region 没啥diao用的GB和骨墙（
                //一部分
                public static void A10()
                {
                    CreateEntity(new Boneslab(270, 55, 0, 20));
                }
                public static void A11()
                {
                    CreateEntity(new Boneslab(270, 110, 0, 10));
                }
                public static void A12()
                {
                    CreateEntity(new Boneslab(270, 165, 0, 10));
                }
                public static void A13()
                {
                    CreateEntity(new NormalGB(new(Rand(40, 600), Rand(20, 460)), new(320, 240), new(0.75f, 0.45f), Rand(0, 359), 100, 20));
                }
                public static void A14()
                {
                    CreateEntity(new Boneslab(90, 55, 0, 20));
                }
                public static void A15()
                {
                    CreateEntity(new Boneslab(90, 110, 0, 10));
                }
                public static void A16()
                {
                    CreateEntity(new Boneslab(90, 165, 0, 10));
                }
                public static void A17()
                {
                }
                //另一部分
                public static void A010()
                {
                    CreateEntity(new Boneslab(270, 55, 0, 20));
                }
                public static void A011()
                {
                    CreateEntity(new Boneslab(270, 110, 0, 10));
                }
                public static void A012()
                {
                    CreateEntity(new Boneslab(270, 165, 0, 10));
                }
                public static void A013()
                {
                    CreateEntity(new NormalGB(new(Rand(40, 600), Rand(20, 460)), new(320, 240), new(0.75f, 0.45f), Rand(0, 359), 20, 20));
                }
                public static void A014()
                {
                    CreateEntity(new Boneslab(90, 55, 0, 20));
                }
                public static void A015()
                {
                    CreateEntity(new Boneslab(90, 110, 0, 10));
                }
                public static void A016()
                {
                    CreateEntity(new Boneslab(90, 165, 0, 10));
                }
                public static void A017()
                {
                }
                #endregion
                public static void A18()
                {
                    for (int i = 0; i <= 12; i++)
                    {
                        CreateBone(new SideCircleBone(360 / 12 * i, 6, 40, 20));
                    }
                }
                public static void A19()
                {
                    int R = Rand(1, 3);
                    if (R == 1)
                    {
                        CreateBone(new CustomBone(new(600, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                    }
                    if (R == 2)
                    {
                        CreateBone(new CustomBone(new(600, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                    }
                    if (R == 3)
                    {
                        CreateBone(new CustomBone(new(600, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                        CreateBone(new CustomBone(new(600, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -7, 0 }
                        });
                    }
                }
                public static void A20()
                {
                    int R = Rand(1, 3);
                    if (R == 1)
                    {
                        CreateBone(new CustomBone(new(40, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                    }
                    if (R == 2)
                    {
                        CreateBone(new CustomBone(new(40, 240 - 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                    }
                    if (R == 3)
                    {
                        CreateBone(new CustomBone(new(40, 240 + 40), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                        CreateBone(new CustomBone(new(40, 240), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { 7, 0 }
                        });
                    }
                }
                public static void A21()
                {
                    SetSoul(2);
                    CreateEntity(new Boneslab(0, 10, 30, 670));
                    CreateEntity(new Boneslab(180, 10, 30, 1320));
                    CreateEntity(new Platform(0, new(320, 240), (s) => { return new Vector2(0, Sin(s.AppearTime * 1.75f * 0.75f) * 60); }, 0, 50, 670));
                }
                public static void A22()
                {
                    CreateEntity(new Platform(1, new(320, 280), (s) => { return new Vector2(Sin(s.AppearTime * 1.25f * 0.75f) * 140, 0); }, 0, 50, 640));
                    CreateEntity(new Platform(1, new(320, 280), (s) => { return new Vector2(Sin(s.AppearTime * -1.25f * 0.75f) * 140, 0); }, 0, 50, 640));
                    CreateEntity(new Platform(0, new(320, 200), (s) => { return new Vector2(Sin(s.AppearTime * 1.55f * 0.75f) * 120, 0); }, 0, 50, 640));
                    CreateEntity(new Platform(0, new(320, 200), (s) => { return new Vector2(Sin(s.AppearTime * -1.55f * 0.75f) * 120, 0); }, 0, 50, 640));
                    LeftBone rb1 = new(true, 5, 600) { ColorType = 2 };
                    LeftBone rb2 = new(false, 5, 600) { ColorType = 2 };
                    CreateBone(rb1);
                    CreateBone(rb2);
                    AddInstance(new TimeRangedEvent(30, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(70, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(110, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(150, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(190, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(230, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(270, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(310, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(350, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(390, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(430, 40, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                    AddInstance(new TimeRangedEvent(470, 40, () => { rb1.Speed += 0.25f; rb2.Speed += 0.25f; }));
                    AddInstance(new TimeRangedEvent(510, 1500, () => { rb1.Speed -= 0.25f; rb2.Speed -= 0.25f; }));
                }
                public static void A23()
                {
                    CreateBone(new CustomBone(new(580, 240), (s) => { return new Vector2(Sin(s.AppearTime * -2.15f) * 160, 0); }, 0, 200));
                    CreateEntity(new NormalGB(new(Rand(40, 600), 120), new(320, -20), new(0.75f, 0.5f), 60, 20));
                }
                public static void A24()
                {
                    CreateBone(new CustomBone(new(60, 240), (s) => { return new Vector2(Sin(s.AppearTime * 2.15f) * 160, 0); }, 0, 200));
                    CreateEntity(new NormalGB(new(Rand(40, 600), 320), new(320, -20), new(0.75f, 0.5f), 60, 20));
                }
                public static void A26()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 80)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 80)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A27()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 60)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 60)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A28()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 45)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 45)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A29()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 34)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 34)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A30()
                {
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 26)
                    {
                        PositionRouteParam = new float[] { 4, 0 }
                    });
                    CreateBone(new CustomBone(new(320, 360), Motions.PositionRoute.linear, 0, 26)
                    {
                        PositionRouteParam = new float[] { -4, 0 }
                    });
                }
                public static void A31()
                {

                }
                public static void A32()
                {

                }
                public static void A33()
                {

                }
                public static void A34()
                {

                }
                public static void A35()
                {

                }
                public static void A36()
                {

                }
                public static void A37()
                {

                }
                public static void A38()
                {

                }
                public static void A39()
                {

                }
                public static void A40()
                {

                }
                public static void A41()
                {

                }
            }

            public static void Effect()
            {
                //开头特效
                #region

                if (GametimeF >= 0 && GametimeF < 60)
                {
                    NormalLine line1 = new(0, -80, 300, 220, 200, 1 - (Gametime - 30) * 0.03f * ((Gametime - 30) * 0.03f));
                    NormalLine line2 = new(940, 860, 340, 260, 200, 1 - (Gametime - 30) * 0.03f * ((Gametime - 30) * 0.03f));
                    NormalLine line3 = new(940, -380, 340, 220, 200, 1 - (Gametime - 30) * 0.03f * ((Gametime - 30) * 0.03f));
                    NormalLine line4 = new(-300, 860, 300, 260, 200, 1 - (Gametime - 30) * 0.03f * ((Gametime - 30) * 0.03f));
                    CreateEntity(line4);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    line4.alpha -= 0.5f;
                    line1.alpha -= 0.5f;
                    line2.alpha -= 0.5f;
                    line3.alpha -= 0.5f;
                    AddInstance(new TimeRangedEvent(1, 150, () =>
                    {
                        line4.alpha -= 0.15f;
                        line1.alpha -= 0.15f;
                        line2.alpha -= 0.15f;
                        line3.alpha -= 0.15f;
                    }));
                }
                if (GametimeF >= 0 * 12 && GametimeF < 90 + 12 * 1)
                {
                    NormalLine line = new(0, 220 - (Gametime - 0 * 12) * 0.55f * ((Gametime - 0 * 12) * 0.55f), 640, 220 - (Gametime - 0 * 12) * 0.55f * ((Gametime - 0 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line1 = new(0, 260 + (Gametime - 0 * 12) * 0.55f * ((Gametime - 0 * 12) * 0.55f), 640, 260 + (Gametime - 0 * 12) * 0.55f * ((Gametime - 0 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line2 = new(340 + (Gametime - 0 * 12) * 0.55f * ((Gametime - 0 * 12) * 0.55f), 0, 340 + (Gametime - 0 * 12) * 0.55f * ((Gametime - 0 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line3 = new(300 - (Gametime - 0 * 12) * 0.55f * ((Gametime - 0 * 12) * 0.55f), 0, 300 - (Gametime - 0 * 12) * 0.55f * ((Gametime - 0 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    line.alpha -= 0.5f;
                    line1.alpha -= 0.5f;
                    line2.alpha -= 0.5f;
                    line3.alpha -= 0.5f;
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.15f;
                        line1.alpha -= 0.15f;
                        line2.alpha -= 0.15f;
                        line3.alpha -= 0.15f;
                    }));
                }
                if (GametimeF >= 1 * 12 && GametimeF < 90 + 12 * 2)
                {
                    NormalLine line = new(0, 220 - (Gametime - 1 * 12) * 0.55f * ((Gametime - 1 * 12) * 0.55f), 640, 220 - (Gametime - 1 * 12) * 0.55f * ((Gametime - 1 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line1 = new(0, 260 + (Gametime - 1 * 12) * 0.55f * ((Gametime - 1 * 12) * 0.55f), 640, 260 + (Gametime - 1 * 12) * 0.55f * ((Gametime - 1 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line2 = new(340 + (Gametime - 1 * 12) * 0.55f * ((Gametime - 1 * 12) * 0.55f), 0, 340 + (Gametime - 1 * 12) * 0.55f * ((Gametime - 1 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line3 = new(300 - (Gametime - 1 * 12) * 0.55f * ((Gametime - 1 * 12) * 0.55f), 0, 300 - (Gametime - 1 * 12) * 0.55f * ((Gametime - 1 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.15f;
                        line1.alpha -= 0.15f;
                        line2.alpha -= 0.15f;
                        line3.alpha -= 0.15f;
                    }));
                }
                if (GametimeF >= 2 * 12 && GametimeF < 90 + 12 * 3)
                {
                    NormalLine line = new(0, 220 - (Gametime - 2 * 12) * 0.55f * ((Gametime - 2 * 12) * 0.55f), 640, 220 - (Gametime - 2 * 12) * 0.55f * ((Gametime - 2 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line1 = new(0, 260 + (Gametime - 2 * 12) * 0.55f * ((Gametime - 2 * 12) * 0.55f), 640, 260 + (Gametime - 2 * 12) * 0.55f * ((Gametime - 2 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line2 = new(340 + (Gametime - 2 * 12) * 0.55f * ((Gametime - 2 * 12) * 0.55f), 0, 340 + (Gametime - 2 * 12) * 0.55f * ((Gametime - 2 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line3 = new(300 - (Gametime - 2 * 12) * 0.55f * ((Gametime - 2 * 12) * 0.55f), 0, 300 - (Gametime - 2 * 12) * 0.55f * ((Gametime - 2 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    line.alpha -= 0.5f;
                    line1.alpha -= 0.5f;
                    line2.alpha -= 0.5f;
                    line3.alpha -= 0.5f;
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.15f;
                        line1.alpha -= 0.15f;
                        line2.alpha -= 0.15f;
                        line3.alpha -= 0.15f;
                    }));
                }
                if (GametimeF == 3 * 12 && GametimeF < 90 + 12 * 4)
                {
                    NormalLine line = new(0, 220 - (Gametime - 3 * 12) * 0.55f * ((Gametime - 3 * 12) * 0.55f), 640, 220 - (Gametime - 3 * 12) * 0.55f * ((Gametime - 3 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line1 = new(0, 260 + (Gametime - 3 * 12) * 0.55f * ((Gametime - 3 * 12) * 0.55f), 640, 260 + (Gametime - 3 * 12) * 0.55f * ((Gametime - 3 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line2 = new(340 + (Gametime - 3 * 12) * 0.55f * ((Gametime - 3 * 12) * 0.55f), 0, 340 + (Gametime - 3 * 12) * 0.55f * ((Gametime - 3 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line3 = new(300 - (Gametime - 3 * 12) * 0.55f * ((Gametime - 3 * 12) * 0.55f), 0, 300 - (Gametime - 3 * 12) * 0.55f * ((Gametime - 3 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    line.alpha -= 0.5f;
                    line1.alpha -= 0.5f;
                    line2.alpha -= 0.5f;
                    line3.alpha -= 0.5f;
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.15f;
                        line1.alpha -= 0.15f;
                        line2.alpha -= 0.15f;
                        line3.alpha -= 0.15f;
                    }));
                }
                if (GametimeF >= 4 * 12 && GametimeF < 90 + 12 * 5)
                {
                    NormalLine line = new(0, 220 - (Gametime - 4 * 12) * 0.55f * ((Gametime - 4 * 12) * 0.55f), 640, 220 - (Gametime - 4 * 12) * 0.55f * ((Gametime - 4 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line1 = new(0, 260 + (Gametime - 4 * 12) * 0.55f * ((Gametime - 4 * 12) * 0.55f), 640, 260 + (Gametime - 4 * 12) * 0.55f * ((Gametime - 4 * 12) * 0.55f), 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line2 = new(340 + (Gametime - 4 * 12) * 0.55f * ((Gametime - 4 * 12) * 0.55f), 0, 340 + (Gametime - 4 * 12) * 0.55f * ((Gametime - 4 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    NormalLine line3 = new(300 - (Gametime - 4 * 12) * 0.55f * ((Gametime - 4 * 12) * 0.55f), 0, 300 - (Gametime - 4 * 12) * 0.55f * ((Gametime - 4 * 12) * 0.55f), 480, 150, 0.1f + Gametime * 0.0375f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    line.alpha -= 0.5f;
                    line1.alpha -= 0.5f;
                    line2.alpha -= 0.5f;
                    line3.alpha -= 0.5f;
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.15f;
                        line1.alpha -= 0.15f;
                        line2.alpha -= 0.15f;
                        line3.alpha -= 0.15f;
                    }));
                }
                #endregion
                //啥也不是（
                #region
                if (GametimeF > 755 && GametimeF < 810)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.1f;
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f, ScreenDrawing.ScreenPositionDetla.Y * 0.9f);
                }
                if (GametimeF > 450 && GametimeF < 480)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 2.25f * 0.1f;
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f + 480 * 0.1f, ScreenDrawing.ScreenPositionDetla.Y * 0.9f - 240 * 0.1f);
                }
                #endregion
                //特效1
                #region
                if (GametimeF == 1490 + 60 * 0 && GametimeF < 1490 + 60 * 1)
                {
                    NormalLine line = new(0, 220 - 22 - (Gametime - 0 * 60 - 1490) * 0.55f * ((Gametime - 0 * 60 - 1490) * 0.55f), 640, 220 - 22 - (Gametime - 0 * 60 - 1490) * 0.55f * ((Gametime - 0 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line1 = new(0, 260 + 22 + (Gametime - 0 * 60 - 1490) * 0.55f * ((Gametime - 0 * 60 - 1490) * 0.55f), 640, 260 + 22 + (Gametime - 0 * 60 - 1490) * 0.55f * ((Gametime - 0 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line2 = new(340 + 22 + (Gametime - 0 * 60 - 1490) * 0.55f * ((Gametime - 0 * 60 - 1490) * 0.55f), 0, 340 + 22 + (Gametime - 0 * 60 - 1490) * 0.55f * ((Gametime - 0 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    NormalLine line3 = new(300 - 22 - (Gametime - 0 * 60 - 1490) * 0.55f * ((Gametime - 0 * 60 - 1490) * 0.55f), 0, 300 - 22 - (Gametime - 0 * 60 - 1490) * 0.55f * ((Gametime - 0 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.0125f;
                        line1.alpha -= 0.0125f;
                        line2.alpha -= 0.0125f;
                        line3.alpha -= 0.0125f;
                    }));
                }
                if (GametimeF == 1490 + 60 * 1 && GametimeF < 1490 + 60 * 2)
                {
                    NormalLine line = new(0, 220 - 22 - (Gametime - 1 * 60 - 1490) * 0.55f * ((Gametime - 1 * 60 - 1490) * 0.55f), 640, 220 - 22 - (Gametime - 1 * 60 - 1490) * 0.55f * ((Gametime - 1 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line1 = new(0, 260 + 22 + (Gametime - 1 * 60 - 1490) * 0.55f * ((Gametime - 1 * 60 - 1490) * 0.55f), 640, 260 + 22 + (Gametime - 1 * 60 - 1490) * 0.55f * ((Gametime - 1 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line2 = new(340 + 22 + (Gametime - 1 * 60 - 1490) * 0.55f * ((Gametime - 1 * 60 - 1490) * 0.55f), 0, 340 + 22 + (Gametime - 1 * 60 - 1490) * 0.55f * ((Gametime - 1 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    NormalLine line3 = new(300 - 22 - (Gametime - 1 * 60 - 1490) * 0.55f * ((Gametime - 1 * 60 - 1490) * 0.55f), 0, 300 - 22 - (Gametime - 1 * 60 - 1490) * 0.55f * ((Gametime - 1 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.0125f;
                        line1.alpha -= 0.0125f;
                        line2.alpha -= 0.0125f;
                        line3.alpha -= 0.0125f;
                    }));
                }
                if (GametimeF == 1490 + 60 * 2 && GametimeF < 1490 + 60 * 3)
                {
                    NormalLine line = new(0, 220 - 22 - (Gametime - 2 * 60 - 1490) * 0.55f * ((Gametime - 2 * 60 - 1490) * 0.55f), 640, 220 - 22 - (Gametime - 2 * 60 - 1490) * 0.55f * ((Gametime - 2 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line1 = new(0, 260 + 22 + (Gametime - 2 * 60 - 1490) * 0.55f * ((Gametime - 2 * 60 - 1490) * 0.55f), 640, 260 + 22 + (Gametime - 2 * 60 - 1490) * 0.55f * ((Gametime - 2 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line2 = new(340 + 22 + (Gametime - 2 * 60 - 1490) * 0.55f * ((Gametime - 2 * 60 - 1490) * 0.55f), 0, 340 + 22 + (Gametime - 2 * 60 - 1490) * 0.55f * ((Gametime - 2 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    NormalLine line3 = new(300 - 22 - (Gametime - 2 * 60 - 1490) * 0.55f * ((Gametime - 2 * 60 - 1490) * 0.55f), 0, 300 - 22 - (Gametime - 2 * 60 - 1490) * 0.55f * ((Gametime - 2 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.0125f;
                        line1.alpha -= 0.0125f;
                        line2.alpha -= 0.0125f;
                        line3.alpha -= 0.0125f;
                    }));
                }
                if (GametimeF == 1490 + 60 * 3 && GametimeF < 1490 + 60 * 4)
                {
                    NormalLine line = new(0, 220 - 22 - (Gametime - 3 * 60 - 1490) * 0.55f * ((Gametime - 3 * 60 - 1490) * 0.55f), 640, 220 - 22 - (Gametime - 3 * 60 - 1490) * 0.55f * ((Gametime - 3 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line1 = new(0, 260 + 22 + (Gametime - 3 * 60 - 1490) * 0.55f * ((Gametime - 3 * 60 - 1490) * 0.55f), 640, 260 + 22 + (Gametime - 3 * 60 - 1490) * 0.55f * ((Gametime - 3 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line2 = new(340 + 22 + (Gametime - 3 * 60 - 1490) * 0.55f * ((Gametime - 3 * 60 - 1490) * 0.55f), 0, 340 + 22 + (Gametime - 3 * 60 - 1490) * 0.55f * ((Gametime - 3 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    NormalLine line3 = new(300 - 22 - (Gametime - 3 * 60 - 1490) * 0.55f * ((Gametime - 3 * 60 - 1490) * 0.55f), 0, 300 - 22 - (Gametime - 3 * 60 - 1490) * 0.55f * ((Gametime - 3 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.0125f;
                        line1.alpha -= 0.0125f;
                        line2.alpha -= 0.0125f;
                        line3.alpha -= 0.0125f;
                    }));
                }
                if (GametimeF == 1490 + 60 * 4 && GametimeF < 1490 + 60 * 5)
                {
                    NormalLine line = new(0, 220 - 22 - (Gametime - 4 * 60 - 1490) * 0.55f * ((Gametime - 4 * 60 - 1490) * 0.55f), 640, 220 - 22 - (Gametime - 4 * 60 - 1490) * 0.55f * ((Gametime - 4 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line1 = new(0, 260 + 22 + (Gametime - 4 * 60 - 1490) * 0.55f * ((Gametime - 4 * 60 - 1490) * 0.55f), 640, 260 + 22 + (Gametime - 4 * 60 - 1490) * 0.55f * ((Gametime - 4 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line2 = new(340 + 22 + (Gametime - 4 * 60 - 1490) * 0.55f * ((Gametime - 4 * 60 - 1490) * 0.55f), 0, 340 + 22 + (Gametime - 4 * 60 - 1490) * 0.55f * ((Gametime - 4 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    NormalLine line3 = new(300 - 22 - (Gametime - 4 * 60 - 1490) * 0.55f * ((Gametime - 4 * 60 - 1490) * 0.55f), 0, 300 - 22 - (Gametime - 4 * 60 - 1490) * 0.55f * ((Gametime - 4 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.0125f;
                        line1.alpha -= 0.0125f;
                        line2.alpha -= 0.0125f;
                        line3.alpha -= 0.0125f;
                    }));
                }
                if (GametimeF == 1490 + 60 * 5 && GametimeF < 1490 + 60 * 6)
                {
                    NormalLine line = new(0, 240 - 42 - (Gametime - 5 * 60 - 1490) * 0.55f * ((Gametime - 5 * 60 - 1490) * 0.55f), 640, 240 - 42 - (Gametime - 5 * 60 - 1490) * 0.55f * ((Gametime - 5 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line1 = new(0, 240 + 42 + (Gametime - 5 * 60 - 1490) * 0.55f * ((Gametime - 5 * 60 - 1490) * 0.55f), 640, 240 + 42 + (Gametime - 5 * 60 - 1490) * 0.55f * ((Gametime - 5 * 60 - 1490) * 0.55f), 360, 0.8f);
                    NormalLine line2 = new(320 + 42 + (Gametime - 5 * 60 - 1490) * 0.55f * ((Gametime - 5 * 60 - 1490) * 0.55f), 0, 320 + 42 + (Gametime - 5 * 60 - 1490) * 0.55f * ((Gametime - 5 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    NormalLine line3 = new(320 - 42 - (Gametime - 5 * 60 - 1490) * 0.55f * ((Gametime - 5 * 60 - 1490) * 0.55f), 0, 320 - 42 - (Gametime - 5 * 60 - 1490) * 0.55f * ((Gametime - 5 * 60 - 1490) * 0.55f), 480, 360, 0.8f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.0125f;
                        line1.alpha -= 0.0125f;
                        line2.alpha -= 0.0125f;
                        line3.alpha -= 0.0125f;
                    }));
                }
                #endregion
                //好康的））1
                #region
                if (GametimeF == 2080)
                {
                    SetBox(100, 40, 40);
                    TP(320, 100);
                }
                if (GametimeF == 2100)
                {
                    SetBox(290, 40, 40);
                    SetSoul(0);
                    TP();
                }
                if (GametimeF == 2115)
                {
                    SetBox(240, 40, 40);
                    TP();
                }
                if (GametimeF == 2120)
                {
                    SetBox(240, 180, 180);
                    TP();
                }
                if (GametimeF >= 2121)
                {
                    NormalLine line = new(0, 240 - 90 - (Gametime - 2120) * 0.75f * ((Gametime - 2120) * 0.75f), 640, 240 - 90 - (Gametime - 2120) * 0.75f * ((Gametime - 2120) * 0.75f), 90, 0.65f);
                    NormalLine line1 = new(0, 240 + 90 + (Gametime - 2120) * 0.75f * ((Gametime - 2120) * 0.75f), 640, 240 + 90 + (Gametime - 2120) * 0.75f * ((Gametime - 2120) * 0.75f), 90, 0.65f);
                    NormalLine line2 = new(320 + 90 + (Gametime - 2120) * 0.75f * ((Gametime - 2120) * 0.75f), 0, 320 + 90 + (Gametime - 2120) * 0.75f * ((Gametime - 2120) * 0.75f), 480, 90, 0.65f);
                    NormalLine line3 = new(320 - 90 - (Gametime - 2120) * 0.75f * ((Gametime - 2120) * 0.75f), 0, 320 - 90 - (Gametime - 2120) * 0.75f * ((Gametime - 2120) * 0.75f), 480, 90, 0.65f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.025f;
                        line1.alpha -= 0.025f;
                        line2.alpha -= 0.025f;
                        line3.alpha -= 0.025f;
                    }));
                }
                if (GametimeF >= 2160 && GametimeF < 2190)
                {
                    NormalLine line = new(640 - (Gametime - 2160) * 0.9f * ((Gametime - 2160) * 0.9f), 0, 640 - (Gametime - 2160) * 0.9f * ((Gametime - 2160) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.125f;
                    }));
                }
                if (GametimeF >= 2185 + 0 * 42.5f && GametimeF < 2185 + 1 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f - 50 * 0.1f, 0);
                    NormalLine line = new(640 - (Gametime - 2185 - 0 * 42.5f) * 0.9f * ((Gametime - 2185 - 0 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2185 - 0 * 42.5f) * 0.9f * ((Gametime - 2185 - 0 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2185 + 1 * 42.5f && GametimeF < 2185 + 2 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f + 50 * 0.1f, 0);
                    NormalLine line = new((Gametime - 2185 - 1 * 42.5f) * 0.9f * ((Gametime - 2185 - 1 * 42.5f) * 0.9f), 0, (Gametime - 2185 - 1 * 42.5f) * 0.9f * ((Gametime - 2185 - 1 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2185 + 2 * 42.5f && GametimeF < 2185 + 3 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f - 50 * 0.1f, 0);
                    NormalLine line = new(640 - (Gametime - 2185 - 2 * 42.5f) * 0.9f * ((Gametime - 2185 - 2 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2185 - 2 * 42.5f) * 0.9f * ((Gametime - 2185 - 2 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2185 + 3 * 42.5f && GametimeF < 2185 + 4 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f + 50 * 0.1f, 0);
                    NormalLine line = new((Gametime - 2185 - 3 * 42.5f) * 0.9f * ((Gametime - 2185 - 3 * 42.5f) * 0.9f), 0, (Gametime - 2185 - 3 * 42.5f) * 0.9f * ((Gametime - 2185 - 3 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2185 + 4 * 42.5f && GametimeF < 2185 + 5 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f - 50 * 0.1f, 0);
                    NormalLine line = new(640 - (Gametime - 2185 - 4 * 42.5f) * 0.9f * ((Gametime - 2185 - 4 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2185 - 4 * 42.5f) * 0.9f * ((Gametime - 2185 - 4 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2185 + 5 * 42.5f && GametimeF < 2185 + 6 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f + 50 * 0.1f, 0);
                    NormalLine line = new((Gametime - 2185 - 5 * 42.5f) * 0.9f * ((Gametime - 2185 - 5 * 42.5f) * 0.9f), 0, (Gametime - 2185 - 5 * 42.5f) * 0.9f * ((Gametime - 2185 - 5 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2185 + 6 * 42.5f && GametimeF < 2185 + 7 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f, 0);
                    NormalLine line = new(640 - (Gametime - 2185 - 6 * 42.5f) * 0.9f * ((Gametime - 2185 - 6 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2185 - 6 * 42.5f) * 0.9f * ((Gametime - 2185 - 6 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    NormalLine line1 = new((Gametime - 2185 - 6 * 42.5f) * 0.9f * ((Gametime - 2185 - 6 * 42.5f) * 0.9f), 0, (Gametime - 2185 - 6 * 42.5f) * 0.9f * ((Gametime - 2185 - 6 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line1);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                        line1.alpha -= 0.75f;
                    }));
                }
                #endregion
                //走过场1 F
                #region
                if (GametimeF >= 2450 && GametimeF < 2460)
                {
                    NormalLine line = new(0, (Gametime - 2450) * 1.2f * ((Gametime - 2450) * 1.2f), 640, (Gametime - 2450) * 1.2f * ((Gametime - 2450) * 1.2f), 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2460 && GametimeF < 2470)
                {
                    NormalLine line = new(0, 480 - (Gametime - 2460) * 1.2f * ((Gametime - 2460) * 1.2f), 640, 480 - (Gametime - 2460) * 1.2f * ((Gametime - 2460) * 1.2f), 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2470 && GametimeF < 2480)
                {
                    NormalLine line = new(0, (Gametime - 2470) * 1.2f * ((Gametime - 2470) * 1.2f), 640, (Gametime - 2470) * 1.2f * ((Gametime - 2470) * 1.2f), 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2480 && GametimeF < 2500)
                {
                    Linerotate line = new(320, 240, (Gametime - 2480) * 1.25f * ((Gametime - 2480) * 1.25f), 90, 1 - (Gametime - 2480) * 0.05f * ((Gametime - 2480) * 0.05f));
                    Linerotate line1 = new(320, 240, 90 + (Gametime - 2480) * 1.25f * ((Gametime - 2480) * 1.25f), 90, 1 - (Gametime - 2480) * 0.05f * ((Gametime - 2480) * 0.05f));
                    CreateEntity(line);
                    CreateEntity(line1);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 1;
                        line1.alpha -= 1;
                    }));
                }
                if (GametimeF >= 2480 && GametimeF < 2680)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 180 * 0.1f;
                }
                if (GametimeF >= 2495 && GametimeF < 2525)
                {
                    NormalLine line = new((2525 - Gametime) * 0.9f * ((2525 - Gametime) * 0.9f), 0, (2525 - Gametime) * 0.9f * ((2525 - Gametime) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.125f;
                    }));
                }
                #endregion
                //好康的（并不）2 F
                #region
                if (GametimeF >= 2520 + 0 * 42.5f && GametimeF < 2520 + 1 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f - 50 * 0.1f, 0);
                    NormalLine line = new((Gametime - 2520 - 0 * 42.5f) * 0.9f * ((Gametime - 2520 - 0 * 42.5f) * 0.9f), 0, (Gametime - 2520 - 0 * 42.5f) * 0.9f * ((Gametime - 2520 - 0 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2520 + 1 * 42.5f && GametimeF < 2520 + 2 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f + 50 * 0.1f, 0);
                    NormalLine line = new(640 - (Gametime - 2520 - 1 * 42.5f) * 0.9f * ((Gametime - 2520 - 1 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2520 - 1 * 42.5f) * 0.9f * ((Gametime - 2520 - 1 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2520 + 2 * 42.5f && GametimeF < 2520 + 3 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f - 50 * 0.1f, 0);
                    NormalLine line = new((Gametime - 2520 - 2 * 42.5f) * 0.9f * ((Gametime - 2520 - 2 * 42.5f) * 0.9f), 0, (Gametime - 2520 - 2 * 42.5f) * 0.9f * ((Gametime - 2520 - 2 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2520 + 3 * 42.5f && GametimeF < 2520 + 4 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f + 50 * 0.1f, 0);
                    NormalLine line = new(640 - (Gametime - 2520 - 3 * 42.5f) * 0.9f * ((Gametime - 2520 - 3 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2520 - 3 * 42.5f) * 0.9f * ((Gametime - 2520 - 3 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2520 + 4 * 42.5f && GametimeF < 2520 + 5 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f - 50 * 0.1f, 0);
                    NormalLine line = new((Gametime - 2520 - 4 * 42.5f) * 0.9f * ((Gametime - 2520 - 4 * 42.5f) * 0.9f), 0, (Gametime - 2520 - 4 * 42.5f) * 0.9f * ((Gametime - 2520 - 4 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2520 + 5 * 42.5f && GametimeF < 2520 + 6 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f + 50 * 0.1f, 0);
                    NormalLine line = new(640 - (Gametime - 2520 - 5 * 42.5f) * 0.9f * ((Gametime - 2520 - 5 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2520 - 5 * 42.5f) * 0.9f * ((Gametime - 2520 - 5 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2520 + 6 * 42.5f && GametimeF < 2520 + 7 * 42.5f)
                {
                    ScreenDrawing.ScreenPositionDetla = new(ScreenDrawing.ScreenPositionDetla.X * 0.9f, 0);
                    NormalLine line = new(640 - (Gametime - 2520 - 6 * 42.5f) * 0.9f * ((Gametime - 2520 - 6 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2520 - 6 * 42.5f) * 0.9f * ((Gametime - 2520 - 6 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    NormalLine line1 = new((Gametime - 2520 - 6 * 42.5f) * 0.9f * ((Gametime - 2520 - 6 * 42.5f) * 0.9f), 0, (Gametime - 2520 - 6 * 42.5f) * 0.9f * ((Gametime - 2520 - 6 * 42.5f) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line1);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                        line1.alpha -= 0.75f;
                    }));
                }
                #endregion
                //走过场2 F
                #region
                if (GametimeF > 2850 && GametimeF < 2890)
                {
                    Linerotate line = new(320, 240, (Gametime - 2850) * 1.25f * ((Gametime - 2850) * 1.25f), 90, 1 - (Gametime - 2850) * 0.05f * ((Gametime - 2850) * 0.05f));
                    Linerotate line1 = new(320, 240, 90 + (Gametime - 2850) * 1.25f * ((Gametime - 2850) * 1.25f), 90, 1 - (Gametime - 2850) * 0.05f * ((Gametime - 2850) * 0.05f));
                    Linerotate line2 = new(320, 90, 90, 90, (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f));
                    Linerotate line3 = new(320, 390, 90, 90, (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f));
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 1;
                        line1.alpha -= 1;
                        line2.alpha -= 0.125f;
                        line3.alpha -= 0.125f;
                    }));
                    SetBox(Gametime - 2790 + 240, 180 - (Gametime - 2790) * 3 / 4, 180 - (Gametime - 2790) * 3 / 4);
                }
                if (GametimeF > 2890 && GametimeF < 2920)
                {
                    NormalLine line = new((Gametime - 2890) * 0.9f * ((Gametime - 2890) * 0.9f), 0, (Gametime - 2890) * 0.9f * ((Gametime - 2890) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.125f;
                    }));
                }
                if (GametimeF > 2850 && GametimeF < 2970)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 720 * 0.1f;
                }
                if (GametimeF > 2890 && GametimeF < 2950)
                {
                    NormalLine line = new(0, 240 - 90 - (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f), 640, 240 - 90 - (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f), 90, 0.65f);
                    NormalLine line1 = new(0, 240 + 90 + (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f), 640, 240 + 90 + (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f), 90, 0.65f);
                    NormalLine line2 = new(320 + 90 + (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f), 0, 320 + 90 + (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f), 480, 90, 0.65f);
                    NormalLine line3 = new(320 - 90 - (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f), 0, 320 - 90 - (Gametime - 2890) * 0.75f * ((Gametime - 2890) * 0.75f), 480, 90, 0.65f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    CreateEntity(line3);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.025f;
                        line1.alpha -= 0.025f;
                        line2.alpha -= 0.025f;
                        line3.alpha -= 0.025f;
                    }));
                }
                if (GametimeF > 2830 && GametimeF < 2890)
                {
                    SetBox(240, 180, 180);
                }
                #endregion
                //好康的（并不）3 F
                #region
                if (GametimeF >= 2890 + 1 * 42.5f && GametimeF < 2890 + 2 * 42.5f)
                {
                    NormalLine line = new(640 - (Gametime - 2890 - 1 * 42.5f) * 0.9f * ((Gametime - 2890 - 1 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2890 - 1 * 42.5f) * 0.9f * ((Gametime - 2890 - 1 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 2890 - 1 * 42.5f) * 0.05f * ((Gametime - 2890 - 1 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2890 + 2 * 42.5f && GametimeF < 2890 + 3 * 42.5f)
                {
                    NormalLine line = new((Gametime - 2890 - 2 * 42.5f) * 0.9f * ((Gametime - 2890 - 2 * 42.5f) * 0.9f), 0, (Gametime - 2890 - 2 * 42.5f) * 0.9f * ((Gametime - 2890 - 2 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 2890 - 2 * 42.5f) * 0.05f * ((Gametime - 2890 - 2 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2890 + 3 * 42.5f && GametimeF < 2890 + 4 * 42.5f)
                {
                    NormalLine line = new(640 - (Gametime - 2890 - 3 * 42.5f) * 0.9f * ((Gametime - 2890 - 3 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2890 - 3 * 42.5f) * 0.9f * ((Gametime - 2890 - 3 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 2890 - 3 * 42.5f) * 0.05f * ((Gametime - 2890 - 3 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2890 + 4 * 42.5f && GametimeF < 2890 + 5 * 42.5f)
                {
                    NormalLine line = new((Gametime - 2890 - 4 * 42.5f) * 0.9f * ((Gametime - 2890 - 4 * 42.5f) * 0.9f), 0, (Gametime - 2890 - 4 * 42.5f) * 0.9f * ((Gametime - 2890 - 4 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 2890 - 4 * 42.5f) * 0.05f * ((Gametime - 2890 - 4 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2890 + 5 * 42.5f && GametimeF < 2890 + 6 * 42.5f)
                {
                    NormalLine line = new(640 - (Gametime - 2890 - 5 * 42.5f) * 0.9f * ((Gametime - 2890 - 5 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2890 - 5 * 42.5f) * 0.9f * ((Gametime - 2890 - 5 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 2890 - 5 * 42.5f) * 0.05f * ((Gametime - 2890 - 5 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2890 + 6 * 42.5f && GametimeF < 2890 + 7 * 42.5f)
                {
                    NormalLine line = new((Gametime - 2890 - 6 * 42.5f) * 0.9f * ((Gametime - 2890 - 6 * 42.5f) * 0.9f), 0, (Gametime - 2890 - 6 * 42.5f) * 0.9f * ((Gametime - 2890 - 6 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 2890 - 6 * 42.5f) * 0.05f * ((Gametime - 2890 - 6 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF >= 2890 + 7 * 42.5f && GametimeF < 2890 + 8 * 42.5f)
                {
                    NormalLine line = new(640 - (Gametime - 2890 - 7 * 42.5f) * 0.9f * ((Gametime - 2890 - 7 * 42.5f) * 0.9f), 0, 640 - (Gametime - 2890 - 7 * 42.5f) * 0.9f * ((Gametime - 2890 - 7 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 2890 - 7 * 42.5f) * 0.05f * ((Gametime - 2890 - 7 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                #endregion
                //走过场3
                #region
                if (GametimeF >= 3200 && GametimeF < 3240)
                {
                    Linerotate line = new(320, 240, (Gametime - 3200) * 1.25f * ((Gametime - 3200) * 1.25f), 90, 1 - (Gametime - 3200) * 0.05f * ((Gametime - 3200) * 0.05f));
                    Linerotate line1 = new(320, 240, 90 + (Gametime - 3200) * 1.25f * ((Gametime - 3200) * 1.25f), 90, 1 - (Gametime - 3200) * 0.05f * ((Gametime - 3200) * 0.05f));
                    CreateEntity(line);
                    CreateEntity(line1);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 1;
                        line1.alpha -= 1;
                    }));
                }
                if (GametimeF > 3200 && GametimeF < 3200 + 120)
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 180 * 0.1f;
                }
                if (GametimeF >= 3240 && GametimeF < 3285)
                {
                    NormalLine line = new((3240 - Gametime) * 0.9f * ((3240 - Gametime) * 0.9f), 0, (3240 - Gametime) * 0.9f * ((3240 - Gametime) * 0.9f), 480, 90, 0.75f);
                    NormalLine line1 = new(640 - (3240 - Gametime) * 0.9f * ((3240 - Gametime) * 0.9f), 0, 640 - (3240 - Gametime) * 0.9f * ((3240 - Gametime) * 0.9f), 480, 90, 0.75f);
                    CreateEntity(line);
                    CreateEntity(line1);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.125f;
                        line1.alpha -= 0.125f;
                    }));
                }
                #endregion
                //好康的（并不）4
                #region
                if (GametimeF == 3190 + 1 * 42.5f && GametimeF < 3190 + 2 * 42.5f)
                {
                    NormalLine line = new((Gametime - 3190 - 1 * 42.5f) * 0.9f * ((Gametime - 3190 - 1 * 42.5f) * 0.9f), 0, (Gametime - 3190 - 1 * 42.5f) * 0.9f * ((Gametime - 3190 - 1 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 3190 - 1 * 42.5f) * 0.05f * ((Gametime - 3190 - 1 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF == 3190 + 2 * 42.5f && GametimeF < 3190 + 3 * 42.5f)
                {
                    NormalLine line = new(640 - (Gametime - 3190 - 2 * 42.5f) * 0.9f * ((Gametime - 3190 - 2 * 42.5f) * 0.9f), 0, 640 - (Gametime - 3190 - 2 * 42.5f) * 0.9f * ((Gametime - 3190 - 2 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 3190 - 2 * 42.5f) * 0.05f * ((Gametime - 3190 - 2 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF == 3190 + 3 * 42.5f && GametimeF < 3190 + 4 * 42.5f)
                {
                    NormalLine line = new((Gametime - 3190 - 3 * 42.5f) * 0.9f * ((Gametime - 3190 - 3 * 42.5f) * 0.9f), 0, (Gametime - 3190 - 3 * 42.5f) * 0.9f * ((Gametime - 3190 - 3 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 3190 - 3 * 42.5f) * 0.05f * ((Gametime - 3190 - 3 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF == 3190 + 4 * 42.5f && GametimeF < 3190 + 5 * 42.5f)
                {
                    NormalLine line = new(640 - (Gametime - 3190 - 4 * 42.5f) * 0.9f * ((Gametime - 3190 - 4 * 42.5f) * 0.9f), 0, 640 - (Gametime - 3190 - 4 * 42.5f) * 0.9f * ((Gametime - 3190 - 4 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 3190 - 4 * 42.5f) * 0.05f * ((Gametime - 3190 - 4 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF == 3190 + 5 * 42.5f && GametimeF < 3190 + 6 * 42.5f)
                {
                    NormalLine line = new((Gametime - 3190 - 5 * 42.5f) * 0.9f * ((Gametime - 3190 - 5 * 42.5f) * 0.9f), 0, (Gametime - 3190 - 5 * 42.5f) * 0.9f * ((Gametime - 3190 - 5 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 3190 - 5 * 42.5f) * 0.05f * ((Gametime - 3190 - 5 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF == 3190 + 6 * 42.5f && GametimeF < 3190 + 7 * 42.5f)
                {
                    NormalLine line = new(640 - (Gametime - 3190 - 6 * 42.5f) * 0.9f * ((Gametime - 3190 - 6 * 42.5f) * 0.9f), 0, 640 - (Gametime - 3190 - 6 * 42.5f) * 0.9f * ((Gametime - 3190 - 6 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 3190 - 6 * 42.5f) * 0.05f * ((Gametime - 3190 - 6 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                if (GametimeF == 3190 + 7 * 42.5f && GametimeF < 3190 + 8 * 42.5f)
                {
                    NormalLine line = new((Gametime - 3190 - 7 * 42.5f) * 0.9f * ((Gametime - 3190 - 7 * 42.5f) * 0.9f), 0, (Gametime - 3190 - 7 * 42.5f) * 0.9f * ((Gametime - 3190 - 7 * 42.5f) * 0.9f), 480, 90, 0.75f - (Gametime - 3190 - 7 * 42.5f) * 0.05f * ((Gametime - 3190 - 7 * 42.5f) * 0.05f));
                    CreateEntity(line);
                    AddInstance(new TimeRangedEvent(1, 89, () =>
                    {
                        line.alpha -= 0.75f;
                    }));
                }
                #endregion
                //（？）1
                #region
                if (GametimeF > 3520 && GametimeF < 3560)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.5f * 0.1f;
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 0 * 0.1f;
                }
                if (GametimeF > 3560 && GametimeF < 3580)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1 * 0.1f;
                }
                if (GametimeF == 3520)
                {
                    ScreenDrawing.WhiteOut(40);
                    SetBox(240, 120, 120);
                }
                if (GametimeF == 3480 + 60)
                {
                    SetBox(480, 480 + 120, 240 - 60, 240 + 60);
                    SetSoul(0);
                    Heart.GiveForce(0, 7.5f);
                }
                if (GametimeF == 3500 + 60)
                {
                    SetBox(260, 480 + 120, 240 - 60, 240 + 60);
                    ResetBarrage();
                }
                if (GametimeF == 3500 + 70)
                {
                    TP(BoxStates.Centre.X - 20, BoxStates.Centre.Y);
                }
                if (GametimeF == 3550 + 60)
                {
                    SetBox(240, 120, 120);
                }
                if (GametimeF == 3560 + 60)
                {
                    SetBox(40, 320 + 60, 240 - 60, 240 + 60);
                }
                if (GametimeF == 3560 + 70)
                {
                    TP(BoxStates.Centre.X - 50, BoxStates.Centre.Y);
                }
                if (GametimeF == 3630 + 60)
                {
                    SetBox(240, 120, 120);
                }
                if (GametimeF == 3730)
                {
                    SetBox(40, 320 + 60, 240 - 60, 240 + 60);
                }
                if (GametimeF == 3740)
                {
                    TP(BoxStates.Centre.X - 50, BoxStates.Centre.Y);
                }
                if (GametimeF == 3780)
                {
                    SetBox(240, 120, 120);
                }
                if (GametimeF == 3790)
                {
                    SetBox(260, 480 + 120, 240 - 60, 240 + 60);
                }
                if (GametimeF == 3800)
                {
                    TP(BoxStates.Centre.X + 50, BoxStates.Centre.Y);
                }
                if (GametimeF == 3860)
                {
                    SetBox(240, 120, 120);
                }
                //??
                if (GametimeF > 3630 + 60 && GametimeF < 3630 + 80)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.35f * 0.1f;
                }
                if (GametimeF > 3630 + 80 && GametimeF < 3630 + 100)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.1f;
                }
                if (GametimeF > 3800 + 60 && GametimeF < 3800 + 80)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.35f * 0.1f;
                }
                if (GametimeF > 3800 + 80 && GametimeF < 3800 + 100)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.1f;
                }
                if (GametimeF > 4300 + 60 && GametimeF < 4300 + 80)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.35f * 0.1f;
                }
                if (GametimeF > 4300 + 80 && GametimeF < 4300 + 100)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.1f;
                }
                if (GametimeF > 4490 + 60 && GametimeF < 4490 + 80)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.35f * 0.1f;
                }
                if (GametimeF > 4490 + 80 && GametimeF < 4490 + 100)
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 0.1f;
                }
                #endregion
                //（？）2
                #region
                if (GametimeF == 4220)
                {
                    SetBox(240, 120, 120);
                    SetSoul(0);
                }
                if (GametimeF == 4230)
                {
                    SetBox(260, 480 + 120, 240 - 60, 240 + 60);
                }
                if (GametimeF == 4240)
                {
                    TP(BoxStates.Centre.X + 20, BoxStates.Centre.Y);
                }
                if (GametimeF == 4280)
                {
                    SetBox(240, 120, 120);
                }
                if (GametimeF == 4310)
                {
                    SetBox(40, 320 + 60, 240 - 60, 240 + 60);
                }
                if (GametimeF == 4320)
                {
                    TP(BoxStates.Centre.X - 50, BoxStates.Centre.Y);
                }
                if (GametimeF == 4360)
                {
                    SetBox(240, 120, 120);
                }
                if (GametimeF == 4420)
                {
                    SetBox(40, 320 + 60, 240 - 60, 240 + 60);
                }
                if (GametimeF == 4430)
                {
                    TP(BoxStates.Centre.X - 50, BoxStates.Centre.Y);
                }
                if (GametimeF == 4470)
                {
                    SetBox(240, 120, 120);
                }
                if (GametimeF == 4500)
                {
                    SetBox(260, 480 + 120, 240 - 60, 240 + 60);
                }
                if (GametimeF == 4510)
                {
                    TP(BoxStates.Centre.X + 50, BoxStates.Centre.Y);
                }
                if (GametimeF == 4550)
                {
                    SetBox(240, 120, 120);
                }
                #endregion
                //寄
                if (GametimeF == 4880)
                {
                    SetBox(300, 40, 40);
                }
                if (GametimeF == 4900)
                {
                    SetBox(240, 480, 240);
                }
                //二寄
                if (GametimeF == 5560)
                {
                    SetSoul(0);
                }
                //SP.part
                for (int i = 0; i < 16; i++)
                {
                    int t = 5630 + i * 40;
                    if (GametimeF == t)
                    {
                        Linerotate l = new((Gametime - t) * (Gametime - t) * 0.55f, 240, 0, 240, 0.75f - (Gametime - t) * (Gametime - t) * 0.35f);
                        Linerotate l1 = new(640 - (Gametime - t) * (Gametime - t) * 0.55f, 240, 0, 240, 0.75f - (Gametime - t) * (Gametime - t) * 0.35f);
                        CreateEntity(l);
                        CreateEntity(l1);
                        AddInstance(new TimeRangedEvent(0, 240, () =>
                        {
                            l.alpha -= 0.075f;
                            l1.alpha -= 0.075f;
                        }));
                    }
                }
            }
            #region 啥笔玩意儿））
            public void Easy()
            {
                //我是代码（迫真）
            }

            public void Extreme()
            {
                //这里已经趋势了
            }

            public void ExtremePlus()
            {
                //这里早就趋势了
                //别看了，游戏里面您不可能看到这个难度的（）
            }



            public void Noob()
            {
                //您看您🐎呢，这个难度怎么可能会给您开放）））
            }
            #endregion
            public void Normal()
            {
                Effect();
                if (GametimeF == 60)
                {
                    NormalBarrage.A1();
                    SetSoul(0);
                }
                if (GametimeF == 420)
                {
                    NormalBarrage.A2();
                }
                if (GametimeF > 420 && GametimeF < 755)
                    ScreenDrawing.ScreenPositionDetla = new Vector2(320f, 240f) - FightBox.instance.Centre;
                if (GametimeF > 480 && GametimeF < 600)
                {
                    SetBox(20 + (Gametime - 480) * 100 / 120, 100 + (Gametime - 480) * 100 / 120, 320, 400);
                }
                if (GametimeF > 600 && GametimeF < 755)
                {
                    SetBox(120, 200, 320 - (Gametime - 600) * 1.25f, 400 - (Gametime - 600) * 1.25f);
                }
                if (GametimeF == 800)
                {
                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 12;
                    TP();
                    SetBox(240, 180, 240);
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == 835 + i * 85)
                    {
                        NormalBarrage.A3();
                    }
                }
                if (GametimeF == 820)
                {
                    NormalBarrage.A4();
                }
                for (int i = 0; i <= 14; i++)
                {
                    if (GametimeF == 895 + i * 40)
                    {
                        NormalBarrage.A5();
                    }
                }
                if (GametimeF == 1485)
                {
                    ResetBarrage();
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (GametimeF == 1760 + i * 20)
                    {
                        CreateArrow(80, 0, 6, 0, 0);
                    }
                    if (GametimeF == 1770 + i * 20)
                    {
                        CreateArrow(80, 2, 6, 0, 0);
                    }
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == 1930 + i * 10)
                    {
                        CreateArrow(80, 0, 7, 0, 1);
                        CreateArrow(80, 1, 6, 1, 0);
                    }
                }
                if (GametimeF == 2185 + 0 * 42.5f)
                {
                    NormalBarrage.A6();
                }
                if (GametimeF == 2185 + 1 * 42.5f)
                {
                    NormalBarrage.A7();
                }
                if (GametimeF == 2185 + 2 * 42.5f)
                {
                    NormalBarrage.A6();
                }
                if (GametimeF == 2185 + 3 * 42.5f)
                {
                    NormalBarrage.A7();
                }
                if (GametimeF == 2185 + 4 * 42.5f)
                {
                    NormalBarrage.A6();
                }
                if (GametimeF == 2185 + 5 * 42.5f)
                {
                    NormalBarrage.A7();
                }
                if (GametimeF == 2185 + 6 * 42.5f)
                {
                    NormalBarrage.A6();
                }
                if (GametimeF == 2450)
                {
                    SetBox(300, 180, 180);
                }
                if (GametimeF == 2460)
                {
                    SetBox(180, 180, 180);
                }
                if (GametimeF == 2470)
                {
                    SetBox(240, 180, 180);
                    Heart.GiveForce(180, 5);
                }
                if (GametimeF == 2520 + 0 * 42.5f)
                {
                    NormalBarrage.A6();
                }
                if (GametimeF == 2520 + 1 * 42.5f)
                {
                    NormalBarrage.A7();
                }
                if (GametimeF == 2520 + 2 * 42.5f)
                {
                    NormalBarrage.A6();
                }
                if (GametimeF == 2520 + 3 * 42.5f)
                {
                    NormalBarrage.A7();
                }
                if (GametimeF == 2520 + 4 * 42.5f)
                {
                    NormalBarrage.A6();
                }
                if (GametimeF == 2520 + 5 * 42.5f)
                {
                    NormalBarrage.A7();
                }
                if (GametimeF == 2520 + 6 * 42.5f)
                {
                    NormalBarrage.A6();
                }
                if (Gametime == 2830)
                {
                    Heart.GiveForce(0, 5.5f);
                    SetSoul(2);
                }
                if (GametimeF == 2860 + 0 * 42.5f)
                {
                    NormalBarrage.A8();
                }
                if (GametimeF == 2860 + 1 * 42.5f)
                {
                    NormalBarrage.A8();
                }
                if (GametimeF == 2860 + 2 * 42.5f)
                {
                    NormalBarrage.A8();
                }
                if (GametimeF == 2860 + 3 * 42.5f)
                {
                    NormalBarrage.A8();
                }
                if (GametimeF == 2860 + 4 * 42.5f)
                {
                    NormalBarrage.A8();
                }
                if (GametimeF == 2860 + 5 * 42.5f)
                {
                    NormalBarrage.A8();
                }
                if (GametimeF == 2860 + 6 * 42.5f)
                {
                    NormalBarrage.A8();
                }
                if (GametimeF == 3185)
                {
                    Heart.GiveForce(180, 5.5f);
                }
                if (GametimeF == 3190 + 0 * 42.5f)
                {
                    NormalBarrage.A9();
                }
                if (GametimeF == 3190 + 1 * 42.5f)
                {
                    NormalBarrage.A9();
                }
                if (GametimeF == 3190 + 2 * 42.5f)
                {
                    NormalBarrage.A9();
                }
                if (GametimeF == 3190 + 3 * 42.5f)
                {
                    NormalBarrage.A9();
                }
                if (GametimeF == 3190 + 4 * 42.5f)
                {
                    NormalBarrage.A9();
                }
                if (GametimeF == 3190 + 5 * 42.5f)
                {
                    NormalBarrage.A9();
                }
                if (GametimeF == 3190 + 6 * 42.5f)
                {
                    NormalBarrage.A9();
                }
                #region 啥笔都不看的叠代码
                if (GametimeF == 3500 + 60)
                {
                    NormalBarrage.A10();
                }
                if (GametimeF == 3520 + 60)
                {
                    NormalBarrage.A11();
                }
                if (GametimeF == 3530 + 60)
                {
                    NormalBarrage.A12();
                }
                if (GametimeF == 3550 + 60)
                {
                    NormalBarrage.A13();
                }
                if (GametimeF == 3580 + 60)
                {
                    NormalBarrage.A14();
                }
                if (GametimeF == 3600 + 60)
                {
                    NormalBarrage.A15();
                }
                if (GametimeF == 3610 + 60)
                {
                    NormalBarrage.A16();
                }
                if (GametimeF == 3630 + 60)
                {
                    NormalBarrage.A17();
                    NormalBarrage.A18();
                }
                if (GametimeF == 3730)
                {
                    NormalBarrage.A014();
                }
                if (GametimeF == 3750)
                {
                    NormalBarrage.A015();
                }
                if (GametimeF == 3760)
                {
                    NormalBarrage.A016();
                }
                if (GametimeF == 3780)
                {
                    NormalBarrage.A017();
                }
                if (GametimeF == 3810)
                {
                    NormalBarrage.A010();
                }
                if (GametimeF == 3830)
                {
                    NormalBarrage.A011();
                }
                if (GametimeF == 3840)
                {
                    NormalBarrage.A012();
                }
                if (GametimeF == 3860)
                {
                    NormalBarrage.A013();
                    NormalBarrage.A18();
                }
                if (GametimeF == 3900)
                {
                    CustomBone bone = new(new(80, 240), Motions.PositionRoute.cameFromLeft, 0, 140, 400) { ColorType = 2 };
                    CreateBone(bone);
                    AddInstance(new InstantEvent(30, () =>
                    {
                        bone.ColorType = 0;
                    }));
                    SetBox(240, 560, 120);
                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 3;
                }
                if (GametimeF == 3920)
                {
                    TP(60, 240);
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == 3900 + 30 * i)
                    {
                        NormalBarrage.A19();
                    }
                }
                //二次
                if (GametimeF == 4230)
                {
                    NormalBarrage.A10();
                }
                if (GametimeF == 4250)
                {
                    NormalBarrage.A11();
                }
                if (GametimeF == 4260)
                {
                    NormalBarrage.A12();
                }
                if (GametimeF == 4280)
                {
                    NormalBarrage.A13();
                }
                if (GametimeF == 4310)
                {
                    NormalBarrage.A14();
                }
                if (GametimeF == 4330)
                {
                    NormalBarrage.A15();
                }
                if (GametimeF == 4340)
                {
                    NormalBarrage.A16();
                }
                if (GametimeF == 4360)
                {
                    NormalBarrage.A17();
                    NormalBarrage.A18();
                }
                if (GametimeF == 4420)
                {
                    NormalBarrage.A014();
                }
                if (GametimeF == 4440)
                {
                    NormalBarrage.A015();
                }
                if (GametimeF == 4450)
                {
                    NormalBarrage.A016();
                }
                if (GametimeF == 4470)
                {
                    NormalBarrage.A017();
                }
                if (GametimeF == 4500)
                {
                    NormalBarrage.A010();
                }
                if (GametimeF == 4520)
                {
                    NormalBarrage.A011();
                }
                if (GametimeF == 4530)
                {
                    HardBarrage.A012();
                }
                if (GametimeF == 4550)
                {
                    NormalBarrage.A013();
                    NormalBarrage.A18();
                }
                if (GametimeF == 4590)
                {
                    CustomBone bone = new(new(560, 240), Motions.PositionRoute.cameFromLeft, 0, 140, 400) { ColorType = 2 };
                    CreateBone(bone);
                    AddInstance(new InstantEvent(30, () =>
                    {
                        bone.ColorType = 0;
                    }));
                    SetBox(240, 560, 120);
                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 3;
                }
                if (GametimeF == 4610)
                {
                    TP(580, 240);
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == 4610 + 30 * i)
                    {
                        NormalBarrage.A20();
                    }
                }
                #endregion
                //寄来喽
                #region 寄来喽
                if (GametimeF == 4900)
                {
                    NormalBarrage.A21();
                }
                if (GametimeF == 4930)
                {
                    NormalBarrage.A22();
                }
                if (GametimeF == 4930)
                {
                    NormalBarrage.A23();
                }
                if (GametimeF == 5010)
                {
                    NormalBarrage.A24();
                }
                if (GametimeF == 5090)
                {
                    NormalBarrage.A23();
                }
                if (GametimeF == 5170)
                {
                    NormalBarrage.A24();
                }
                if (GametimeF == 5250)
                {
                    NormalBarrage.A23();
                    SetSoul(2);
                }
                if (GametimeF == 5330)
                {
                    NormalBarrage.A24();
                }
                if (GametimeF == 5410)
                {
                    NormalBarrage.A23();
                }
                if (GametimeF == 5490)
                {
                    NormalBarrage.A24();
                }

                #endregion
                //二寄来喽））
                #region 二寄来喽））
                for (int i = 0; i < 22; i++)
                {
                    if (GametimeF == 5570 + i * 32)
                    {
                        NormalBarrage.A26();
                    }
                    if (GametimeF == 5574 + i * 32)
                    {
                        NormalBarrage.A27();
                    }
                    if (GametimeF == 5578 + i * 32)
                    {
                        NormalBarrage.A28();
                    }
                    if (GametimeF == 5582 + i * 32)
                    {
                        NormalBarrage.A29();
                    }
                    if (GametimeF == 5586 + i * 32)
                    {
                        NormalBarrage.A30();
                    }
                    if (GametimeF == 5590 + i * 32)
                    {
                        NormalBarrage.A29();
                    }
                    if (GametimeF == 5594 + i * 32)
                    {
                        NormalBarrage.A28();
                    }
                    if (GametimeF == 5598 + i * 32)
                    {
                        NormalBarrage.A27();
                    }
                }
                //SP.part
                if (GametimeF > 5690 && GametimeF < 6360)
                {
                    ScreenDrawing.ScreenAngle = Sin((Gametime - 5690) * 1.25f) * 5;
                }
                for (int i = 0; i < 8; i++)
                {
                    if (GametimeF == 5690 + i * 80)//br1
                    {
                        Linerotate l1 = new(Heart.Centre.X - 30, Heart.Centre.Y, Rand(181, 360), 120, 0.65f);
                        CreateEntity(l1);
                        AddInstance(new TimeRangedEvent(10, 110, () =>
                        {
                            l1.alpha -= 0.05f;
                        }));
                        CreateEntity(new NormalGB(Heart.Centre + GetVector2(100, Rand(0, 359)), Heart.Centre, new(1, 0.45f), 50, 30));
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    if (GametimeF == 5690 + i * 140)
                    {
                        CreateBone(new CustomBone(new(80, 120), Motions.PositionRoute.linear, 60, 6400, 600)
                        {
                            ColorType = 2,
                            PositionRouteParam = new float[] { 6.55f, 4.2f }
                        });
                        CreateBone(new CustomBone(new(560, 360), Motions.PositionRoute.linear, 60, 6400, 600)
                        {
                            ColorType = 2,
                            PositionRouteParam = new float[] { -6.55f, -4.2f }
                        });
                    }
                    if (GametimeF == 5760 + i * 140)
                    {
                        CreateBone(new CustomBone(new(80, 360), Motions.PositionRoute.linear, 120, 6400, 600)
                        {
                            ColorType = 2,
                            PositionRouteParam = new float[] { 6.55f, -4.2f }
                        });
                        CreateBone(new CustomBone(new(560, 120), Motions.PositionRoute.linear, 120, 6400, 600)
                        {
                            ColorType = 2,
                            PositionRouteParam = new float[] { -6.55f, 4.2f }
                        });
                    }
                }
                if (GametimeF > 6360 && GametimeF < 6360 + 120)
                {
                    ScreenDrawing.ScreenAngle *= 0.9f;
                }
                #endregion
                //后面是尾杀我鸽了（）
                //谱师屑亿代是个大傻逼（迫真）
                var v = GetAll<Entity>();
                if (GametimeF == 6330)
                {
                    SetBox(240, 84, 84);
                    TP();
                    SetSoul(1);
                }
                if (GametimeF == 6457 - 80)
                {
                    HardBarrageArrow.arrow1();
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == (int)(6457 + bpm * 80 + i * 10 - 80))
                    {
                        CreateArrow(80, 0, 7, 0, 1);
                        CreateArrow(80, 1, 6, 1, 0);
                    }
                }
                if (GametimeF == 7050)
                {
                    SetBox(240, 168, 168);
                    TP();
                    SetSoul(2);
                }
                NormalLine line1 = new(0, 240, 640, 240, 114514, 0.4f);
                NormalLine line2 = new(0, 240, 640, 240, 114514, 0.6f);
                NormalLine line3 = new(0, 240, 640, 240, 114514, 0.8f);
                for (int a = 0; a < 16; a++)
                {
                    if (GametimeF == (int)(7077 + bpm * 16 * a))
                    {
                        RightBone bone1 = new(true, 5, 1);
                        RightBone bone2 = new(false, 5, 1);
                        DownBone bone3 = new(true, 5, 1);
                        DownBone bone4 = new(false, 5, 1);
                        LeftBone bone5 = new(true, 5, 1);
                        LeftBone bone6 = new(false, 5, 1);
                        UpBone bone7 = new(true, 5, 1);
                        UpBone bone8 = new(false, 5, 1);
                        if (Rand(0, 3) == 0)
                        {
                            Heart.GiveForce(270, 20);
                            AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(bone1);
                                CreateBone(bone2);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 8, bpm * 8, () =>
                            {
                                bone1.MissionLength += 3f;
                                bone2.MissionLength += 3f;
                            }));
                        }
                        else if (LastRand == 1)
                        {
                            Heart.GiveForce(360, 20);
                            PlaySound(Sounds.pierce);
                            AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(bone3);
                                CreateBone(bone4);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 8, bpm * 8, () =>
                            {
                                bone3.MissionLength += 3f;
                                bone4.MissionLength += 3f;
                            }));
                        }
                        else if (LastRand == 2)
                        {
                            Heart.GiveForce(90, 20);
                            AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(bone5);
                                CreateBone(bone6);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 8, bpm * 8, () =>
                            {
                                bone5.MissionLength += 3f;
                                bone6.MissionLength += 3f;
                            }));
                        }
                        else if (LastRand == 3)
                        {
                            Heart.GiveForce(180, 20);
                            AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(bone7);
                                CreateBone(bone8);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 8, bpm * 8, () =>
                            {
                                bone7.MissionLength += 3f;
                                bone8.MissionLength += 3f;
                            }));
                        }
                    }
                }
                if (GametimeF == 8488)
                {
                    SetBox(240, 84, 84);
                    TP();
                    SetSoul(1);
                    float a = 0;
                    CreateEntity(line1);
                    AddInstance(new TimeRangedEvent(bpm * 0.5f, 1, () =>
                    {
                        CreateEntity(line2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 1f, 1, () =>
                    {
                        CreateEntity(line3);
                    }));
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        line1.y1 -= a;
                        line1.y2 -= a;
                        a += 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(0.5f * 0.5f, bpm * 16, () =>
                    {
                        line2.y1 -= a;
                        line2.y2 -= a;
                        a += 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 1, bpm * 16, () =>
                    {
                        line3.y1 -= a;
                        line3.y2 -= a;
                        a += 0.1f;
                    }));
                }
                if (GametimeF == 8119)
                {
                    line1.Dispose();
                    line2.Dispose();
                    line3.Dispose();
                }
            }
            public void Hard()
            {
                /*if (GametimeF >= 0 && GametimeF <= 114514)
                {

                    CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1,
                        GametimeF.ToString(),
                        new Vector2(Heart.Centre.X - 45, Heart.Centre.Y - 40),
                        new UndyneFight_Ex.Fight.TextColorAttribute(new Color(Rand(0, 255), Rand(0, 255), Rand(0, 255)))));
                }*/
                Effect();
                if (GametimeF == 60)
                {
                    HardBarrage.A1();
                }
                if (GametimeF == 420)
                {
                    HardBarrage.A2();
                }
                if (GametimeF > 420 && GametimeF < 755)
                    ScreenDrawing.ScreenPositionDetla = new Vector2(320f, 240f) - FightBox.instance.Centre;
                if (GametimeF > 480 && GametimeF < 600)
                {
                    SetBox(20 + (Gametime - 480) * 100 / 120, 100 + (Gametime - 480) * 100 / 120, 320, 400);
                }
                if (GametimeF > 600 && GametimeF < 755)
                {
                    SetBox(120, 200, 320 - (Gametime - 600) * 1.25f, 400 - (Gametime - 600) * 1.25f);
                }
                if (GametimeF == 800)
                {
                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 12;
                    TP();
                    SetBox(240, 180, 240);
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == 835 + i * 85)
                    {
                        HardBarrage.A3();
                    }
                }
                if (GametimeF == 820)
                {
                    HardBarrage.A4();
                }
                for (int i = 0; i <= 14; i++)
                {
                    if (GametimeF == 895 + i * 40)
                    {
                        HardBarrage.A5();
                    }
                }
                if (GametimeF == 1485)
                {
                    ResetBarrage();
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                }
                for (int i = 0; i <= 6; i++)
                {
                    if (GametimeF == 1760 + i * 20)
                    {
                        CreateArrow(80, 0, 6, 0, 0);
                    }
                    if (GametimeF == 1770 + i * 20)
                    {
                        CreateArrow(80, 2, 6, 0, 0);
                    }
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == 1930 + i * 10)
                    {
                        CreateArrow(80, 0, 7, 0, 1);
                        CreateArrow(80, 1, 6, 1, 0);
                    }
                    if (GametimeF == 1935 + i * 10)
                    {
                        CreateArrow(80, 2, 7, 0, 1);
                        CreateArrow(80, 3, 6, 1, 0);
                    }
                }
                if (GametimeF == 2185 + 0 * 42.5f)
                {
                    HardBarrage.A6();
                }
                if (GametimeF == 2185 + 1 * 42.5f)
                {
                    HardBarrage.A7();
                }
                if (GametimeF == 2185 + 2 * 42.5f)
                {
                    HardBarrage.A6();
                }
                if (GametimeF == 2185 + 3 * 42.5f)
                {
                    HardBarrage.A7();
                }
                if (GametimeF == 2185 + 4 * 42.5f)
                {
                    HardBarrage.A6();
                }
                if (GametimeF == 2185 + 5 * 42.5f)
                {
                    HardBarrage.A7();
                }
                if (GametimeF == 2185 + 6 * 42.5f)
                {
                    HardBarrage.A6();
                }
                if (GametimeF == 2450)
                {
                    SetBox(300, 180, 180);
                }
                if (GametimeF == 2460)
                {
                    SetBox(180, 180, 180);
                }
                if (GametimeF == 2470)
                {
                    SetBox(240, 180, 180);
                    Heart.GiveForce(180, 5);
                }
                if (GametimeF == 2520 + 0 * 42.5f)
                {
                    HardBarrage.A6();
                }
                if (GametimeF == 2520 + 1 * 42.5f)
                {
                    HardBarrage.A7();
                }
                if (GametimeF == 2520 + 2 * 42.5f)
                {
                    HardBarrage.A6();
                }
                if (GametimeF == 2520 + 3 * 42.5f)
                {
                    HardBarrage.A7();
                }
                if (GametimeF == 2520 + 4 * 42.5f)
                {
                    HardBarrage.A6();
                }
                if (GametimeF == 2520 + 5 * 42.5f)
                {
                    HardBarrage.A7();
                }
                if (GametimeF == 2520 + 6 * 42.5f)
                {
                    HardBarrage.A6();
                }
                if (Gametime == 2830)
                {
                    Heart.GiveForce(0, 5.5f);
                    SetSoul(2);
                }
                if (GametimeF == 2860 + 0 * 42.5f)
                {
                    HardBarrage.A8();
                }
                if (GametimeF == 2860 + 1 * 42.5f)
                {
                    HardBarrage.A8();
                }
                if (GametimeF == 2860 + 2 * 42.5f)
                {
                    HardBarrage.A8();
                }
                if (GametimeF == 2860 + 3 * 42.5f)
                {
                    HardBarrage.A8();
                }
                if (GametimeF == 2860 + 4 * 42.5f)
                {
                    HardBarrage.A8();
                }
                if (GametimeF == 2860 + 5 * 42.5f)
                {
                    HardBarrage.A8();
                }
                if (GametimeF == 2860 + 6 * 42.5f)
                {
                    HardBarrage.A8();
                }
                if (GametimeF == 3185)
                {
                    Heart.GiveForce(180, 5.5f);
                }
                if (GametimeF == 3190 + 0 * 42.5f)
                {
                    HardBarrage.A9();
                }
                if (GametimeF == 3190 + 1 * 42.5f)
                {
                    HardBarrage.A9();
                }
                if (GametimeF == 3190 + 2 * 42.5f)
                {
                    HardBarrage.A9();
                }
                if (GametimeF == 3190 + 3 * 42.5f)
                {
                    HardBarrage.A9();
                }
                if (GametimeF == 3190 + 4 * 42.5f)
                {
                    HardBarrage.A9();
                }
                if (GametimeF == 3190 + 5 * 42.5f)
                {
                    HardBarrage.A9();
                }
                if (GametimeF == 3190 + 6 * 42.5f)
                {
                    HardBarrage.A9();
                }
                #region 啥笔都不看的叠代码
                if (GametimeF == 3500 + 60)
                {
                    HardBarrage.A10();
                }
                if (GametimeF == 3520 + 60)
                {
                    HardBarrage.A11();
                }
                if (GametimeF == 3530 + 60)
                {
                    HardBarrage.A12();
                }
                if (GametimeF == 3550 + 60)
                {
                    HardBarrage.A13();
                }
                if (GametimeF == 3580 + 60)
                {
                    HardBarrage.A14();
                }
                if (GametimeF == 3600 + 60)
                {
                    HardBarrage.A15();
                }
                if (GametimeF == 3610 + 60)
                {
                    HardBarrage.A16();
                }
                if (GametimeF == 3630 + 60)
                {
                    HardBarrage.A17();
                    HardBarrage.A18();
                }
                if (GametimeF == 3730)
                {
                    HardBarrage.A014();
                }
                if (GametimeF == 3750)
                {
                    HardBarrage.A015();
                }
                if (GametimeF == 3760)
                {
                    HardBarrage.A016();
                }
                if (GametimeF == 3780)
                {
                    HardBarrage.A017();
                }
                if (GametimeF == 3810)
                {
                    HardBarrage.A010();
                }
                if (GametimeF == 3830)
                {
                    HardBarrage.A011();
                }
                if (GametimeF == 3840)
                {
                    HardBarrage.A012();
                }
                if (GametimeF == 3860)
                {
                    HardBarrage.A013();
                    HardBarrage.A18();
                }
                if (GametimeF == 3900)
                {
                    CustomBone bone = new(new(80, 240), Motions.PositionRoute.cameFromLeft, 0, 140, 400) { ColorType = 2 };
                    CreateBone(bone);
                    AddInstance(new InstantEvent(30, () =>
                    {
                        bone.ColorType = 0;
                    }));
                    SetBox(240, 560, 120);
                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 3;
                }
                if (GametimeF == 3920)
                {
                    TP(60, 240);
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == 3900 + 30 * i)
                    {
                        HardBarrage.A19();
                    }
                }
                //二次
                if (GametimeF == 4230)
                {
                    HardBarrage.A10();
                }
                if (GametimeF == 4250)
                {
                    HardBarrage.A11();
                }
                if (GametimeF == 4260)
                {
                    HardBarrage.A12();
                }
                if (GametimeF == 4280)
                {
                    HardBarrage.A13();
                }
                if (GametimeF == 4310)
                {
                    HardBarrage.A14();
                }
                if (GametimeF == 4330)
                {
                    HardBarrage.A15();
                }
                if (GametimeF == 4340)
                {
                    HardBarrage.A16();
                }
                if (GametimeF == 4360)
                {
                    HardBarrage.A17();
                    HardBarrage.A18();
                }
                if (GametimeF == 4420)
                {
                    HardBarrage.A014();
                }
                if (GametimeF == 4440)
                {
                    HardBarrage.A015();
                }
                if (GametimeF == 4450)
                {
                    HardBarrage.A016();
                }
                if (GametimeF == 4470)
                {
                    HardBarrage.A017();
                }
                if (GametimeF == 4500)
                {
                    HardBarrage.A010();
                }
                if (GametimeF == 4520)
                {
                    HardBarrage.A011();
                }
                if (GametimeF == 4530)
                {
                    HardBarrage.A012();
                }
                if (GametimeF == 4550)
                {
                    HardBarrage.A013();
                    HardBarrage.A18();
                }
                if (GametimeF == 4590)
                {
                    CustomBone bone = new(new(560, 240), Motions.PositionRoute.cameFromLeft, 0, 140, 400) { ColorType = 2 };
                    CreateBone(bone);
                    AddInstance(new InstantEvent(30, () =>
                    {
                        bone.ColorType = 0;
                    }));
                    SetBox(240, 560, 120);
                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 3;
                }
                if (GametimeF == 4610)
                {
                    TP(580, 240);
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == 4610 + 30 * i)
                    {
                        HardBarrage.A20();
                    }
                }
                #endregion
                //寄来喽
                #region 寄来喽
                if (GametimeF == 4900)
                {
                    HardBarrage.A21();
                }
                if (GametimeF == 4930)
                {
                    HardBarrage.A22();
                }
                if (GametimeF == 4930)
                {
                    HardBarrage.A23();
                }
                if (GametimeF == 5010)
                {
                    HardBarrage.A24();
                }
                if (GametimeF == 5090)
                {
                    HardBarrage.A23();
                }
                if (GametimeF == 5170)
                {
                    HardBarrage.A24();
                }
                if (GametimeF == 5250)
                {
                    HardBarrage.A23();
                    SetSoul(2);
                }
                if (GametimeF == 5330)
                {
                    HardBarrage.A24();
                }
                if (GametimeF == 5410)
                {
                    HardBarrage.A23();
                }
                if (GametimeF == 5490)
                {
                    HardBarrage.A24();
                }
                for (int i = 0; i < 20; i++)
                {
                    if (GametimeF == 4930 + i * 32)
                    {
                        HardBarrage.A25();
                    }
                }

                #endregion
                //二寄来喽））
                #region 二寄来喽））
                for (int i = 0; i < 22; i++)
                {
                    if (GametimeF == 5570 + i * 32)
                    {
                        HardBarrage.A26();
                    }
                    if (GametimeF == 5574 + i * 32)
                    {
                        HardBarrage.A27();
                    }
                    if (GametimeF == 5578 + i * 32)
                    {
                        HardBarrage.A28();
                    }
                    if (GametimeF == 5582 + i * 32)
                    {
                        HardBarrage.A29();
                    }
                    if (GametimeF == 5586 + i * 32)
                    {
                        HardBarrage.A30();
                    }
                    if (GametimeF == 5590 + i * 32)
                    {
                        HardBarrage.A29();
                    }
                    if (GametimeF == 5594 + i * 32)
                    {
                        HardBarrage.A28();
                    }
                    if (GametimeF == 5598 + i * 32)
                    {
                        HardBarrage.A27();
                    }
                }
                //SP.part
                if (GametimeF > 5690 && GametimeF < 6360)
                {
                    ScreenDrawing.ScreenAngle = Sin((Gametime - 5690) * 1.25f) * 5;
                }
                for (int i = 0; i < 8; i++)
                {
                    if (GametimeF == 5690 + i * 80)//br1
                    {
                        Linerotate l1 = new(Heart.Centre.X - 30, Heart.Centre.Y, Rand(181, 360), 120, 0.65f);
                        CreateEntity(l1);
                        AddInstance(new TimeRangedEvent(10, 110, () =>
                        {
                            l1.alpha -= 0.05f;
                        }));
                        Vector2 TargetPos = GetVector2(60, LastRand + 180) + new Vector2(Heart.Centre.X, Heart.Centre.Y);
                        CreateEntity(new NormalGB(TargetPos, TargetPos, new(0.75f, 0.45f), 50, 30));
                    }
                    if (GametimeF == 5690 + i * 80)//br2
                    {
                        Linerotate l2 = new(Rand(80, 560), 240, 0, 120, 0.65f);
                        CreateEntity(l2);
                        AddInstance(new TimeRangedEvent(10, 110, () =>
                        {
                            l2.alpha -= 0.15f;
                        }));
                        CreateBone(new CustomBone(new(LastRand, -90), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { 0, 7.25f }
                        });
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    if (GametimeF == 5690 + i * 140)
                    {
                        CreateBone(new CustomBone(new(80, 120), Motions.PositionRoute.linear, 60, 6400, 600)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { 6.55f, 4.2f }
                        });
                        CreateBone(new CustomBone(new(560, 360), Motions.PositionRoute.linear, 60, 6400, 600)
                        {
                            ColorType = 2,
                            PositionRouteParam = new float[] { -6.55f, -4.2f }
                        });
                    }
                    if (GametimeF == 5760 + i * 140)
                    {
                        CreateBone(new CustomBone(new(80, 360), Motions.PositionRoute.linear, 120, 6400, 600)
                        {
                            ColorType = 2,
                            PositionRouteParam = new float[] { 6.55f, -4.2f }
                        });
                        CreateBone(new CustomBone(new(560, 120), Motions.PositionRoute.linear, 120, 6400, 600)
                        {
                            ColorType = 1,
                            PositionRouteParam = new float[] { -6.55f, 4.2f }
                        });
                    }
                }
                if (GametimeF > 6360 && GametimeF < 6360 + 120)
                {
                    ScreenDrawing.ScreenAngle *= 0.9f;
                }
                #endregion
                //后面是尾杀我鸽了（）
                //谱师屑亿代是个大傻逼（迫真）
                if (GametimeF == 6330)
                {
                    SetBox(240, 84, 84);
                    TP();
                    SetSoul(1);
                }
                if (GametimeF == 6457 - 80)
                {
                    HardBarrageArrow.arrow1();
                }
                for (int i = 0; i <= 7; i++)
                {
                    if (GametimeF == (int)(6457 + bpm * 80 + i * 10 - 80))
                    {
                        CreateArrow(80, 0, 7, 0, 1);
                        CreateArrow(80, 1, 6, 1, 0);
                    }
                    if (GametimeF == (int)(6462 + bpm * 80 + i * 10 - 80))
                    {
                        CreateArrow(80, 2, 7, 0, 1);
                        CreateArrow(80, 3, 6, 1, 0);
                    }
                }
                if (GametimeF == 7050)
                {
                    SetBox(240, 168, 168);
                    TP();
                    SetSoul(2);
                }
                RightBone bone1 = new(true, 5, 1);
                RightBone bone2 = new(false, 5, 1);
                DownBone bone3 = new(true, 5, 1);
                DownBone bone4 = new(false, 5, 1);
                LeftBone bone5 = new(true, 5, 1);
                LeftBone bone6 = new(false, 5, 1);
                UpBone bone7 = new(true, 5, 1);
                UpBone bone8 = new(false, 5, 1);
                NormalLine line1 = new(0, 240, 640, 240, 114514, 0.4f);
                NormalLine line2 = new(0, 240, 640, 240, 114514, 0.6f);
                NormalLine line3 = new(0, 240, 640, 240, 114514, 0.8f);
                for (int a = 0; a < 32; a++)
                {
                    if (GametimeF == (int)(7077 + bpm * 8 * a))
                    {
                        if (Rand(0, 3) == 0)
                        {
                            Heart.GiveForce(270, 20);
                            AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(bone1);
                                CreateBone(bone2);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                            {
                                bone1.MissionLength += 3f;
                                bone2.MissionLength += 3f;
                            }));
                        }
                        else if (LastRand == 1)
                        {
                            Heart.GiveForce(360, 20);
                            PlaySound(Sounds.pierce);
                            AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(bone3);
                                CreateBone(bone4);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                            {
                                bone3.MissionLength += 3f;
                                bone4.MissionLength += 3f;
                            }));
                        }
                        else if (LastRand == 2)
                        {
                            Heart.GiveForce(90, 20);
                            AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(bone5);
                                CreateBone(bone6);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                            {
                                bone5.MissionLength += 3f;
                                bone6.MissionLength += 3f;
                            }));
                        }
                        else if (LastRand == 3)
                        {
                            Heart.GiveForce(180, 20);
                            AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                            {
                                PlaySound(Sounds.pierce);
                                CreateBone(bone7);
                                CreateBone(bone8);
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 4, bpm * 4, () =>
                            {
                                bone7.MissionLength += 3f;
                                bone8.MissionLength += 3f;
                            }));
                        }
                    }
                }
                if (GametimeF == 8488)
                {
                    SetBox(240, 84, 84);
                    TP();
                    SetSoul(1);
                    float a = 0;
                    CreateEntity(line1);
                    AddInstance(new TimeRangedEvent(bpm * 0.5f, 1, () =>
                    {
                        CreateEntity(line2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 1f, 1, () =>
                    {
                        CreateEntity(line3);
                    }));
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        line1.y1 -= a;
                        line1.y2 -= a;
                        a += 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(0.5f * bpm, bpm * 16, () =>
                    {
                        line2.y1 -= a;
                        line2.y2 -= a;
                        a += 0.1f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 1, bpm * 16, () =>
                    {
                        line3.y1 -= a;
                        line3.y2 -= a;
                        a += 0.1f;
                    }));
                }
                if (GametimeF == 8119)
                {
                    line1.Dispose();
                    line2.Dispose();
                    line3.Dispose();
                    bone1.Dispose();
                    bone3.Dispose();
                    bone4.Dispose();
                    bone5.Dispose();
                    bone6.Dispose();
                    bone7.Dispose();
                    bone8.Dispose();
                }
            }
            public class HardBarrageArrow
            {
                public static void arrow1()
                {
                    float BeattimeB = 0;
                    string[] Bluearrowway =
                    {
                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "R","/","/","/",
                "/","/","R","/",
                "/","/","/","/",
                "R","/","/","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",

                "R","/","R","/",
                "R","/","R","/",
                "R","/","R","/",
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
            public void Start()
            {
                HeartAttribute.MaxHP = 10;
                SetBox(240, 40, 40);
                SetSoul(3);
                TP();
            }
        }
    }
}