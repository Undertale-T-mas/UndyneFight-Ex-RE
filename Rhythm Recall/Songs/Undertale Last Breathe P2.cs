using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class ULBFight : IWaveSet
    {
        public string FightName => "ULB sans Phase 2";

        public string Music => "ulb theme";

        class ThisInformation : SongInformation
        {
            public override string BarrageAuthor => "T-mas";
            public override string SongAuthor => "Saster";
            public override string PaintAuthor => "NitrodeZ";
            public override string DisplayName => "The Slaughter Continues";

            public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.5f),
                            new(Difficulty.Easy, 7.0f),
                            new(Difficulty.Normal, 12.6f),
                            new(Difficulty.Hard, 15.2f),
                            new(Difficulty.Extreme, 18.0f),
                    }
                );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.6f),
                            new(Difficulty.Easy, 7.1f),
                            new(Difficulty.Normal, 12.6f),
                            new(Difficulty.Hard, 15.2f),
                            new(Difficulty.Extreme, 18.0f),
                    }
                );
            public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 6.0f),
                            new(Difficulty.Easy, 12.0f),
                            new(Difficulty.Normal, 16.0f),
                            new(Difficulty.Hard, 19.0f),
                            new(Difficulty.Extreme, 21.0f),
                    }
                );
        }
        public SongInformation Attributes => new ThisInformation();

        public void Easy()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                Heart.Gravity = 8.0f;
                Heart.ChangeColor(2);
            }
            if (Gametime == 40)
            {
                for (int i = 1; i <= 24; i++)
                    CreateBone(new SideCircleBone(i * 15, 2, 55f, 80f));
            }
            if (Gametime == 147)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.6f, 30));
                CreateBone(new DownBone(false, 3.6f, 30));
            }
            if (Gametime >= 190 && Gametime <= 350 && Gametime % 25 == 19)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 3.2f, 50));
                CreateBone(new DownBone(true, 2.7f, 24));
            }
            if (Gametime == 390)
            {
                Platform p = new(0, new Vector2(140, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { 1.9f, 0 }
                };
                CreatePlatform(p); Platform q = new(0, new Vector2(500, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { -1.9f, 0 }
                };
                CreatePlatform(q);
            }
            if (Gametime == 454)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 3.6f, 55));
                CreateBone(new DownBone(true, 3.6f, 55));
                CreateBone(new DownBone(false, 320, 3.6f, 55));
                CreateBone(new DownBone(true, 320, 3.6f, 55));
            }
            if (Gametime == 539)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 4.0f, 132));
                CreateBone(new UpBone(true, 4.0f, 132));
            }
            if (Gametime == 584)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 560 - 23)
            {
                float time = 110;
                Fortimes(5, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    time += 25f;
                });
            }
            if (Gametime == 782)
            {
                Heart.JumpSpeed = 4f;
                SetSoul(2);
                SetBox(290f, 220f, 170f);
            }
            if (Gametime >= 800 && Gametime <= 1200)
            {
                SetBox(290f + Sin((Gametime - 800) / 100f * 360f) * 50, 220f, 170f);
            }
            if (Gametime >= 785 && Gametime <= 1195 && Gametime % 100 == 85)
            {
                Heart.GiveForce(0, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 35)
            {
                Heart.GiveForce(180, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.9f, 20));
                CreateBone(new DownBone(false, 3.9f, 20));
                CreateBone(new UpBone(true, 3.9f, 105));
                CreateBone(new UpBone(false, 3.9f, 105));
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0 + 50)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.9f, 105));
                CreateBone(new DownBone(false, 3.9f, 105));
                CreateBone(new UpBone(true, 3.9f, 20));
                CreateBone(new UpBone(false, 3.9f, 20));
            }
            if (Gametime == 1200)
            {
                SetSoul(0);
                SetBox(290, 400, 240);
            }
            if (Gametime >= 1200 && Gametime <= 1480 && Gametime % 25 == 0)
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 75, 10));
            }
            if (Gametime == 1530)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 1490)
            {
                float time = 111;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    time += 25f;
                });
                time += 24f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, 0, 0);
                    time += 12.5f;
                });

                time += 24f;

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    time += 12.5f;
                });

                time += 35.5f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, 0, 0);
                    time += 12.5f;
                });
            }
            if (Gametime == 1993)
            {
                Heart.JumpSpeed = 4.5f;
                Heart.Gravity = 5.5f;
                SetSoul(2);
                SetBox(310f, 280f, 160f);
            }
            if (Gametime >= 1995 && Gametime <= 2700 && Gametime % 100 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                float pos1 = Rand(0, 149);
                float pos2 = Rand(-60, 60);
                for (int i = -2; i < 3; i++)
                    CreateBone(new CustomBone(new Vector2(465, 310 + i * 83 + pos2), Motions.PositionRoute.XAxisSin, 0, 40)
                    {
                        PositionRouteParam = new float[] { -2.6f, 42, 150, pos1 }
                    });
            }
            if (Gametime >= 2800 && Gametime <= 3400 && Gametime % 50 == 0)
            {
                CreateBone(new CustomBone(new Vector2(495, 225), Motions.PositionRoute.XAxisSin, 0, 75)
                {
                    PositionRouteParam = new float[] { -2.4f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(495, 380), Motions.PositionRoute.XAxisSin, 0, 130)
                {
                    PositionRouteParam = new float[] { -2.4f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreatePlatform(new Platform(0, new Vector2(495, 304), Motions.PositionRoute.XAxisSin, 0, 40)
                {
                    PositionRouteParam = new float[] { -2.4f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreateGB(new NormalGB(new Vector2(70, 390), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 0, 35f, 75f));
            }
            if (Gametime == 3540)
            {
                SetBox(310f, 310f, 160f);
                Heart.JumpSpeed = 5.0f;
                Heart.Gravity = 6.2f;
            }
            if (Gametime >= 3600 && Gametime <= 4300 && Gametime % 50 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                bool way = Rand(0, 1) == 0;
                int height = Rand(20, 60);
                CreateBone(new UpBone(way, 3.4f, 120 - height));
                CreateBone(new DownBone(way, 3.4f, height));
            }
            if (Gametime == 4367)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4300)
            {
                float time = 100;
                Fortimes(16, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 1), 0, 0);
                    time += 12.5f;
                });
                Fortimes(3, () =>
                {
                    Fortimes(16, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 1), 1, 0);
                        time += 12.5f;
                    });
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 37.5f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                        CreateArrow(time, LastRand, 5.0f, 0, 0);
                        time += 25;
                    });
                    time += 37.5f;
                });

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 2);
                    time += 25;
                });
            }
            if (Gametime == 6005)
            {
                SetSoul(2);
                SetBox(310f, 240f, 160f);
                Heart.JumpSpeed = 5.8f;
                Heart.Gravity = 9.2f;
                CreatePlatform(new Platform(0, new Vector2(260, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
                CreatePlatform(new Platform(0, new Vector2(380, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
            }
            if (Gametime >= 6025 && Gametime <= 6700 && (int)(Gametime % 8f) == 0)
            {
                CreateBone(new DownBone(false, 2.5f, 20) { MarkScore = false });
                CreateBone(new UpBone(true, 2.5f, 20) { MarkScore = false });
            }
            if (Gametime >= 6855 && Gametime <= 7580 && (int)(Gametime % 6f) == 0)
            {
                CreateBone(new DownBone(false, 3.5f, 20) { MarkScore = false });
                CreateBone(new UpBone(true, 3.5f, 20) { MarkScore = false });
            }
            if (Gametime >= 6050 && Gametime <= 6780 && Gametime % 100 == 0)
            {
                CreateGB(new NormalGB(new Vector2(120, 350), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 0f, 60f, 10f));
                CreateGB(new NormalGB(new Vector2(520, 350), new Vector2(640, 480), new Vector2(1.0f, 0.5f), 180f, 60f, 10f));
                if (Rand(0, 1) == 1)
                    CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, Vector2.One, 90f, 60f, 30f));
                else
                    CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, new Vector2(1.0f, 0.5f), 60f, 30f));
            }
            if (Gametime == 6820)
            {
                CreatePlatform(new Platform(1, new Vector2(320, 350), Motions.PositionRoute.YAxisSin, 0, 64, 810)
                {
                    PositionRouteParam = new float[] { 0, 86, 100, 0 },
                    createWithScaling = true
                });
            }
            if (Gametime == 7680)
            {
                SetSoul(0);
            }
            if (Gametime == 7695)
            {
                PlaySound(FightResources.Sounds.pierce);
                SetBox(310, 180, 180);
                for (int i = 0; i < 4; i++)
                    CreateBone(new SideCircleBone(i * 90, 2f, 75, 750));
            }
            if (Gametime == 7705)
            {
                CreateBone(new CentreCircleBone(0, -2f, 75, 740));
                CreateBone(new CentreCircleBone(0, -1f, 280, 740));
                CreateBone(new CentreCircleBone(90, -2f, 75, 740));
            }
            if (Gametime == 8475)
            {
                TP();
                SetSoul(1);
                SetGreenBox();
            }
            if (Gametime == 8400)
            {
                float time = 100;
                Fortimes(31, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, Rand(0, 2));
                    time += 25f;
                });
            }
            if (Gametime == 9275)
            {
                SetBox(310, 280, 180);
                SetSoul(2);
            }
            if (Gametime >= 9300 && Gametime <= 10070 && Gametime % 25 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 2.7f, 50));
            }
            if (Gametime >= 9300 && Gametime <= 10000 && Gametime % 200 == 0)
            {
                CreateGB(new NormalGB(new Vector2(580, 400), new Vector2(640, 480), new Vector2(1.0f, 1.0f), 180, 50, 10));
            }
            if (Gametime == 10075)
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (Gametime == 10001)
            {
                float time = 100;
                int[] hasArrow = { 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1,
                0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0};

                Fortimes(2, () =>
                {
                    int t = 0;
                    Fortimes(32, () =>
                    {
                        if (hasArrow[t] == 1)
                            CreateArrow(time, Rand(0, 3), 5.0f + Rand(-1, 1) * 1.5f, 0, 0);
                        time += 12.5f;
                        t++;
                    });
                });
            }
        }

        public void Extreme()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                Heart.Gravity = 8.0f;
                Heart.ChangeColor(2);
            }
            if (Gametime == 40)
            {
                for (int i = 1; i <= 24; i++)
                    CreateBone(new SideCircleBone(i * 15, 2, 32f, 90f));
            }
            if (Gametime == 75)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 5.0f, 95));
                CreateBone(new DownBone(true, 5.0f, 95));
            }
            if (Gametime == 100)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(true, 5.0f, 95));
                CreateBone(new DownBone(false, 5.0f, 95));
            }
            if (Gametime == 56)
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(140, Rand(0, 364)), Heart.Centre, new Vector2(1.0f, 0.5f), 94 - 25, 5));
                CreateGB(new NormalGB(Heart.Centre + GetVector2(140, Rand(0, 364)), Heart.Centre, new Vector2(1.0f, 0.5f), 119 - 25, 5));
            }
            if (Gametime == 175)
            {
                CreateBone(new DownBone(false, 4.6f, 40));
                CreateBone(new UpBone(true, 4.6f, 160));
                PlaySound(FightResources.Sounds.pierce);
            }
            if (Gametime >= 190 && Gametime <= 350 && Gametime % 25 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 3.2f, 54));
                CreateBone(new DownBone(true, 3.2f, 34));
                if (Gametime % 25 == 0)
                    CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 100), Heart.Centre, new Vector2(1.0F, 0.5F), 25, 6));
            }
            if (Gametime == 400)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 4.0f, 92));
                CreateBone(new UpBone(true, 4.0f, 92));

                Platform p = new(1, new Vector2(140, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { 2.4f, 0 }
                };
                CreatePlatform(p);
                Platform q = new(1, new Vector2(500, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { -2.4f, 0 }
                };
                CreatePlatform(q);
            }
            if (Gametime == 425)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 3.6f, 15));
                CreateBone(new UpBone(true, 3.6f, 15));
                CreateBone(new UpBone(false, 320, 3.6f, 15));
                CreateBone(new UpBone(true, 320, 3.6f, 15));
            }
            if (Gametime == 450)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 3.6f, 55));
                CreateBone(new DownBone(true, 3.6f, 55));
                CreateBone(new DownBone(false, 320, 3.6f, 55));
                CreateBone(new DownBone(true, 320, 3.6f, 55));
            }
            if (Gametime == 500)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 4.0f, 87));
                CreateBone(new DownBone(true, 4.0f, 87));
                CreateBone(new UpBone(false, 4.0f, 72));
                CreateBone(new UpBone(true, 4.0f, 72));
            }
            if (Gametime == 550)
            {
                CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 100), Heart.Centre, new Vector2(1.0F, 0.5F), 25, 6));
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 5.0f, 122));
                CreateBone(new UpBone(true, 5.0f, 122));
                CreateBone(new DownBone(false, 5.0f, 22));
                CreateBone(new DownBone(true, 5.0f, 22));
            }
            if (Gametime == 575)
            {
                CreateGB(new NormalGB(new Vector2(100, Heart.Centre.Y), Heart.Centre, new Vector2(1.0F, 0.5F), 25, 6));
                PlaySound(FightResources.Sounds.pierce);
            }
            if (Gametime == 620)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 560 - 23)
            {
                float time = 110;
                Fortimes(5, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.0f, 0, 2);
                    CreateArrow(time, Rand(0, 3), 6.0f, 1, 2);
                    time += 25f;
                });
            }
            if (Gametime == 782)
            {
                Heart.JumpSpeed = 4f;
                SetSoul(2);
                SetBox(290f, 220f, 170f);
            }
            if (Gametime >= 800 && Gametime <= 1200)
            {
                ScreenDrawing.ScreenAngle = Sin((Gametime - 800) / 100f * 360f) * 10;
                float dt1 = Sin((Gametime - 800) / 100f * 360f) * 50;
                float dt2 = Sin((Gametime - 800) / 100f * 360f) * 78;
                SetBox(210 + dt2, 430 + dt2, 205f + dt1, 375f + dt1);
            }
            if (Gametime >= 785 && Gametime <= 1195 && Gametime % 100 == 85)
            {
                Heart.GiveForce(0, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 35)
            {
                Heart.GiveForce(180, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 430, 3.9f, 20));
                CreateBone(new DownBone(false, 210, 3.9f, 20));
                CreateBone(new UpBone(true, 430, 3.9f, 108));
                CreateBone(new UpBone(false, 210, 3.9f, 108));
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0 + 50)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 430, 3.9f, 108));
                CreateBone(new DownBone(false, 210, 3.9f, 108));
                CreateBone(new UpBone(true, 430, 3.9f, 20));
                CreateBone(new UpBone(false, 210, 3.9f, 20));
            }
            if (Gametime == 1200)
            {
                SetSoul(0);
                SetBox(290, 400, 240);
            }
            if (Gametime >= 1200 && Gametime <= 1500 && Gametime % 25 == 0)
            {
                if (Gametime % 50 == 0)
                {
                    Vector2 centre = new(GetRandom(100, 540), GetRandom(100, 380));
                    float angle = Rand(0, 359);
                    for (int i = 0; i < 4; i++)
                    {
                        angle += 90;
                        CreateGB(new NormalGB(centre + GetVector2(50, angle), centre, new Vector2(1.0f, 0.5f), angle + 180, 75, 10));
                    }
                }
                CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), 50, 6));
            }
            if (Gametime == 1530)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 1490)
            {
                float time = 111;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    CreateArrow(time, Rand(0, 3), 3.0f, 1, 0);
                    time += 25f;
                });
                time += 24f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, Rand(0, 1), Rand(0, 1));
                    time += 12.5f;
                });

                time += 25f;

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    CreateArrow(time, Rand(0, 3), 3.0f, 1, 0);
                    time += 12.5f;
                });

                time += 37.5f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, Rand(0, 1), Rand(0, 1));
                    time += 12.5f;
                });
            }
            if (Gametime == 1993)
            {
                Heart.JumpSpeed = 4.5f;
                Heart.Gravity = 5.5f;
                SetSoul(2);
                SetBox(310f, 280f, 160f);
            }
            if (Gametime >= 1995 && Gametime <= 2700 && Gametime % 50 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                float pos1 = Rand(0, 149);
                float pos2 = Rand(-60, 60);
                for (int i = -2; i < 3; i++)
                    CreateBone(new CustomBone(new Vector2(465, 310 + i * 83 + pos2), Motions.PositionRoute.XAxisSin, 0, 40)
                    {
                        PositionRouteParam = new float[] { -2.6f, 39, 178, pos1 }
                    });
            }
            if (Gametime >= 2800 && Gametime <= 3400)
            {
                ScreenDrawing.ScreenScale = Sin((Gametime - 2800) / 150f * 360) * 0.2f + 1;
            }
            if (Gametime >= 2800 && Gametime <= 3400 && Gametime % 50 == 0)
            {
                CreateBone(new CustomBone(new Vector2(495, 225), Motions.PositionRoute.XAxisSin, 0, 75)
                {
                    PositionRouteParam = new float[] { -2.1f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(495, 380), Motions.PositionRoute.XAxisSin, 0, 168)
                {
                    PositionRouteParam = new float[] { -2.1f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreatePlatform(new Platform(0, new Vector2(495, 304), Motions.PositionRoute.XAxisSin, 0, 48)
                {
                    PositionRouteParam = new float[] { -2.1f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                if (Gametime % 200 == 0)
                    CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 110), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 90f, 35f, 4f));
                CreateGB(new NormalGB(new Vector2(70, 390), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 0, 35f, 75f));
            }
            if (Gametime == 3540)
            {
                SetBox(310f, 320f, 160f);
                Heart.JumpSpeed = 5.0f;
                Heart.Gravity = 6.2f;
            }
            if (Gametime >= 3600 && Gametime <= 4349)
            {
                ScreenDrawing.ScreenAngle = Gametime <= 4279
                    ? (float)(Math.Pow(0.9, Gametime - 3600) * 180 + 180)
                    : Gametime <= 4348 ? (float)(Math.Pow(0.9, Gametime - 4279) * 180) : 0;
                if (Gametime % 50 == 0)
                {
                    PlaySound(FightResources.Sounds.pierce);
                    bool way = Rand(0, 1) == 0;
                    float spd = way ? -3.4f : 3.4f;
                    float plc = way ? 475 : 165;
                    int height = Rand(20, 60);
                    CreateBone(new CustomBone(new Vector2(plc, 390), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                    {
                        ColorType = 2,
                        PositionRouteParam = new float[] { spd, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 39, 40, Rand(0, 120), height * 2 }
                    });
                    if (!way)
                    {
                        CreateBone(new UpBone(way, 160, 3.4f, 120 - height));
                        CreateBone(new DownBone(way, 160, 3.4f, height));
                        CreateBone(new UpBone(way, 170, 3.4f, 120 - height));
                        CreateBone(new DownBone(way, 170, 3.4f, height));
                    }
                    else
                    {
                        CreateBone(new UpBone(way, 480, 3.4f, 120 - height));
                        CreateBone(new DownBone(way, 480, 3.4f, height));
                        CreateBone(new UpBone(way, 470, 3.4f, 120 - height));
                        CreateBone(new DownBone(way, 470, 3.4f, height));
                    }
                }
            }
            if (Gametime == 4367)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4300)
            {
                float time = 100;
                Fortimes(16, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 1), 0, 0);
                    time += 12.5f;
                });
                Fortimes(3, () =>
                {
                    int cnt = 0;
                    Fortimes(16, () =>
                    {
                        cnt++; CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 0), 0, 0);
                        if (cnt % 2 == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 0), 1, Rand(0, 1));
                        }
                        time += 12.5f;
                    });
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 37.5f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        time += 25;
                    });
                    time += 37.5f;
                });

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 2);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25;
                });
            }
            if (Gametime == 6035)
            {
                CreateBone(new CustomBone(new Vector2(320, 230), Motions.PositionRoute.cameFromUp, 0, 70, 738f));
                CreateBone(new CustomBone(new Vector2(320, 390), Motions.PositionRoute.cameFromDown, 0, 140, 738f));
            }
            if (Gametime == 6005)
            {
                SetSoul(2);
                SetBox(310f, 240f, 160f);
                Heart.JumpSpeed = 5.8f;
                Heart.Gravity = 9.2f;
                CreatePlatform(new Platform(0, new Vector2(260, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
                CreatePlatform(new Platform(0, new Vector2(380, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
            }
            if (Gametime >= 6025 && Gametime <= 6700)
            {
                if (Gametime % 8 == 0)
                {
                    CreateBone(new DownBone(false, 2.5f, 20) { MarkScore = false });
                    CreateBone(new UpBone(true, 2.5f, 20) { MarkScore = false });
                }
            }
            if (Gametime >= 6855 && Gametime <= 7580 && (int)(Gametime % 3f) == 0)
            {
                if (Gametime % 6 == 0)
                {
                    CreateBone(new DownBone(false, 3.5f, 20) { MarkScore = false });
                    CreateBone(new CustomBone(new Vector2(440, 230), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                    {
                        PositionRouteParam = new float[] { -3.7f, 0.0f },
                        LengthRouteParam = new float[] { 20, 48, Gametime * 2, 25 },
                        RotationRouteParam = new float[] { 0 }
                    });
                }
                else
                    CreateBone(new DownBone(false, 3.5f, 50) { MarkScore = false, ColorType = 2 });
            }
            if (Gametime >= 6050 && Gametime <= 6780 && Gametime % 100 == 0)
            {
                CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, new Vector2(1.0f, 1.5f), 90f, 60f, 30f));
                CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, new Vector2(1.0f, 0.5f), 60f, 30f));
            }
            if (Gametime == 6820)
            {
                CreatePlatform(new Platform(1, new Vector2(320, 350), Motions.PositionRoute.YAxisSin, 0, 40, 810)
                {
                    PositionRouteParam = new float[] { 0, 96, 100, 0 },
                    createWithScaling = true
                });
                CreateBone(new CustomBone(new Vector2(200, 310), Motions.PositionRoute.YAxisSin, 0, 156, 810)
                {
                    PositionRouteParam = new float[] { 0, 140, 200, 0 }
                });
                CreateBone(new CustomBone(new Vector2(440, 310), Motions.PositionRoute.YAxisSin, 0, 156, 810)
                {
                    PositionRouteParam = new float[] { 0, 140, 200, 0 }
                });
            }
            if (Gametime == 7680)
            {
                SetSoul(0);
            }
            if (Gametime == 7695)
            {
                PlaySound(FightResources.Sounds.pierce);
                SetBox(310, 180, 180);
                for (int i = 0; i < 4; i++)
                    CreateBone(new SideCircleBone(i * 90, 2f, 75, 750));
            }
            if (Gametime == 7705)
            {
                CreateBone(new CentreCircleBone(0, -2f, 75, 740));
                CreateBone(new CentreCircleBone(0, -1f, 280, 740) { ColorType = 1 });
                CreateBone(new CentreCircleBone(0, 1f, 280, 740) { ColorType = 2 });
                CreateBone(new CentreCircleBone(90, -2f, 75, 740));
            }
            if (Gametime == 8475)
            {
                TP();
                SetSoul(1);
                SetGreenBox();
            }
            if (Gametime == 8400)
            {
                float time = 100;
                Fortimes(31, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, 0, Rand(0, 2));
                    CreateArrow(time, Rand(0, 3), 4.0f, 1, Rand(0, 2));
                    time += 25f;
                });
            }
            if (Gametime == 9275)
            {
                Heart.Speed = 3.2f;
                SetBox(310, 280, 180);
                SetSoul(2);
            }
            if (Gametime >= 9300 && Gametime <= 10070 && (int)(Gametime % 25f) == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 3.5f, 32));
            }
            if (Gametime >= 9300 && Gametime <= 10070 && (int)(Gametime % 25f) == 12)
            {
                CreateBone(new CustomBone(new Vector2(180, 220), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                {
                    PositionRouteParam = new float[] { 3.5f, 0.0f },
                    LengthRouteParam = new float[] { 135, 60, Gametime, 136 },
                    RotationRouteParam = new float[] { 0 }
                });
            }
            if (Gametime >= 9300 && Gametime <= 10000 && Gametime % 200 == 0)
            {
                CreateGB(new NormalGB(new Vector2(580, 400), new Vector2(640, 480), new Vector2(1.0f, 1.0f), 180, 50, 10));
            }
            if (Gametime == 10075)
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (Gametime == 10000)
            {
                float time = 100;
                int[] hasArrow = { 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1,
                0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0};

                Fortimes(2, () =>
                {
                    int t = 0;
                    Fortimes(32, () =>
                    {
                        if (hasArrow[t] == 1)
                            CreateArrow(time, Rand(0, 3), 5.0f + Rand(-1, 1) * 1.5f, 0, 0);
                        time += 12.5f;
                        t++;
                    });
                });
            }
        }

        public void ExtremePlus()
        {
            throw new NotImplementedException();
        }

        public void Hard()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                Heart.Gravity = 8.0f;
                Heart.ChangeColor(2);
            }
            if (Gametime == 40)
            {
                for (int i = 1; i <= 24; i++)
                    CreateBone(new SideCircleBone(i * 15, 2, 32f, 90f));
            }
            if (Gametime == 56)
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(140, Rand(0, 364)), Heart.Centre, new Vector2(1.0f, 0.5f), 44, 15));
                CreateGB(new NormalGB(Heart.Centre + GetVector2(140, Rand(0, 364)), Heart.Centre, new Vector2(1.0f, 0.5f), 44, 15));
            }
            if (Gametime == 150)
            {
                CreateBone(new DownBone(true, 3.6f, 40));
                CreateBone(new DownBone(false, 3.6f, 40));
                PlaySound(FightResources.Sounds.pierce);
            }
            if (Gametime >= 190 && Gametime <= 350 && Gametime % 25 == 19)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 3.2f, 54));
                CreateBone(new DownBone(true, 3.2f, 34));
                if (Gametime % 50 == 19)
                    CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 100), Heart.Centre, new Vector2(1.0F, 0.5F), 25, 10));
            }
            if (Gametime == 400)
            {
                Platform p = new(1, new Vector2(140, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { 1.9f, 0 }
                };
                CreatePlatform(p); Platform q = new(1, new Vector2(500, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { -1.9f, 0 }
                };
                CreatePlatform(q);
            }
            if (Gametime == 454)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 3.6f, 55));
                CreateBone(new DownBone(true, 3.6f, 55));
                CreateBone(new DownBone(false, 320, 3.6f, 55));
                CreateBone(new DownBone(true, 320, 3.6f, 55));
            }
            if (Gametime == 500)
            {
                CreateBone(new DownBone(false, 4.0f, 87));
                CreateBone(new DownBone(true, 4.0f, 87));
                CreateBone(new UpBone(false, 4.0f, 72));
                CreateBone(new UpBone(true, 4.0f, 72));
            }
            if (Gametime == 550)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 4.0f, 122));
                CreateBone(new UpBone(true, 4.0f, 122));
            }
            if (Gametime == 584)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 560 - 23)
            {
                float time = 110;
                Fortimes(5, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    CreateArrow(time, Rand(0, 3), 3.0f, 1, 0);
                    time += 25f;
                });
            }
            if (Gametime == 782)
            {
                Heart.JumpSpeed = 4f;
                SetSoul(2);
                SetBox(290f, 220f, 170f);
            }
            if (Gametime >= 800 && Gametime <= 1200)
            {
                float dt1 = Sin((Gametime - 800) / 100f * 360f) * 50;
                float dt2 = Cos((Gametime - 800) / 100f * 360f) * 50;
                SetBox(210 + dt2, 430 + dt2, 205f + dt1, 375f + dt1);
            }
            if (Gametime >= 785 && Gametime <= 1195 && Gametime % 100 == 85)
            {
                Heart.GiveForce(0, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 35)
            {
                Heart.GiveForce(180, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.9f, 20));
                CreateBone(new DownBone(false, 3.9f, 20));
                CreateBone(new UpBone(true, 3.9f, 111));
                CreateBone(new UpBone(false, 3.9f, 111));
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0 + 50)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.9f, 111));
                CreateBone(new DownBone(false, 3.9f, 111));
                CreateBone(new UpBone(true, 3.9f, 20));
                CreateBone(new UpBone(false, 3.9f, 20));
            }
            if (Gametime == 1200)
            {
                SetSoul(0);
                SetBox(290, 400, 240);
            }
            if (Gametime >= 1200 && Gametime <= 1480 && Gametime % 25 == 0)
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), 75, 10));
            }
            if (Gametime == 1530)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 1490)
            {
                float time = 111;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    CreateArrow(time, Rand(0, 3), 3.0f, 1, 0);
                    time += 25f;
                });
                time += 24f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, Rand(0, 1), Rand(0, 1));
                    time += 12.5f;
                });

                time += 25f;

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    CreateArrow(time, Rand(0, 3), 3.0f, 1, 0);
                    time += 12.5f;
                });

                time += 37.5f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, Rand(0, 1), Rand(0, 1));
                    time += 12.5f;
                });
            }
            if (Gametime == 1993)
            {
                Heart.JumpSpeed = 4.5f;
                Heart.Gravity = 5.5f;
                SetSoul(2);
                SetBox(310f, 280f, 160f);
            }
            if (Gametime >= 1995 && Gametime <= 2700 && Gametime % 50 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                float pos1 = Rand(0, 149);
                float pos2 = Rand(-60, 60);
                for (int i = -2; i < 3; i++)
                    CreateBone(new CustomBone(new Vector2(465, 310 + i * 83 + pos2), Motions.PositionRoute.XAxisSin, 0, 40)
                    {
                        PositionRouteParam = new float[] { -2.6f, 39, 178, pos1 }
                    });
            }
            if (Gametime >= 2800 && Gametime <= 3400 && Gametime % 50 == 0)
            {
                CreateBone(new CustomBone(new Vector2(495, 225), Motions.PositionRoute.XAxisSin, 0, 75)
                {
                    PositionRouteParam = new float[] { -2.1f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(495, 380), Motions.PositionRoute.XAxisSin, 0, 168)
                {
                    PositionRouteParam = new float[] { -2.1f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreatePlatform(new Platform(0, new Vector2(495, 304), Motions.PositionRoute.XAxisSin, 0, 48)
                {
                    PositionRouteParam = new float[] { -2.1f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                if (Gametime % 200 == 0)
                    CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 110), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 90f, 35f, 4f));
                CreateGB(new NormalGB(new Vector2(70, 390), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 0, 35f, 75f));
            }
            if (Gametime == 3540)
            {
                SetBox(310f, 320f, 160f);
                Heart.JumpSpeed = 5.0f;
                Heart.Gravity = 6.2f;
            }
            if (Gametime >= 3600 && Gametime <= 4300 && Gametime % 50 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                bool way = Rand(0, 1) == 0;
                float spd = way ? -3.4f : 3.4f;
                float plc = way ? 475 : 165;
                int height = Rand(20, 60);
                CreateBone(new CustomBone(new Vector2(plc, 390), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                {
                    ColorType = 2,
                    PositionRouteParam = new float[] { spd, 0 },
                    RotationRouteParam = new float[] { 0 },
                    LengthRouteParam = new float[] { 39, 40, Rand(0, 120), height * 2 }
                });
                if (!way)
                {
                    CreateBone(new UpBone(way, 160, 3.4f, 120 - height));
                    CreateBone(new DownBone(way, 160, 3.4f, height));
                    CreateBone(new UpBone(way, 170, 3.4f, 120 - height));
                    CreateBone(new DownBone(way, 170, 3.4f, height));
                }
                else
                {
                    CreateBone(new UpBone(way, 480, 3.4f, 120 - height));
                    CreateBone(new DownBone(way, 480, 3.4f, height));
                    CreateBone(new UpBone(way, 470, 3.4f, 120 - height));
                    CreateBone(new DownBone(way, 470, 3.4f, height));
                }
            }
            if (Gametime == 4367)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4300)
            {
                float time = 100;
                Fortimes(16, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 1), 0, 0);
                    time += 12.5f;
                });
                Fortimes(3, () =>
                {
                    Fortimes(16, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 0), 1, Rand(0, 1));
                        time += 12.5f;
                    });
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 37.5f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        time += 25;
                    });
                    time += 37.5f;
                });

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 2);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25;
                });
            }
            if (Gametime == 6035)
            {
                CreateBone(new CustomBone(new Vector2(320, 230), Motions.PositionRoute.cameFromUp, 0, 70, 738f));
                CreateBone(new CustomBone(new Vector2(320, 390), Motions.PositionRoute.cameFromDown, 0, 140, 738f));
            }
            if (Gametime == 6005)
            {
                SetSoul(2);
                SetBox(310f, 240f, 160f);
                Heart.JumpSpeed = 5.8f;
                Heart.Gravity = 9.2f;
                CreatePlatform(new Platform(0, new Vector2(260, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
                CreatePlatform(new Platform(0, new Vector2(380, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
            }
            if (Gametime >= 6025 && Gametime <= 6700 && (int)(Gametime % 8f) == 0)
            {
                CreateBone(new DownBone(false, 2.5f, 20) { MarkScore = false });
                CreateBone(new UpBone(true, 2.5f, 20) { MarkScore = false });
            }
            if (Gametime >= 6855 && Gametime <= 7580 && (int)(Gametime % 6f) == 0)
            {
                CreateBone(new DownBone(false, 3.5f, 20) { MarkScore = false });
                CreateBone(new CustomBone(new Vector2(440, 230), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                {
                    PositionRouteParam = new float[] { -3.7f, 0.0f },
                    LengthRouteParam = new float[] { 20, 48, Gametime * 2, 25 },
                    RotationRouteParam = new float[] { 0 }
                });
            }
            if (Gametime >= 6050 && Gametime <= 6780 && Gametime % 100 == 0)
            {
                CreateGB(new NormalGB(new Vector2(120, 350), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 0f, 60f, 10f));
                CreateGB(new NormalGB(new Vector2(520, 350), new Vector2(640, 480), new Vector2(1.0f, 0.5f), 180f, 60f, 10f));

                CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, Vector2.One, 90f, 60f, 30f));
                CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, new Vector2(1.0f, 0.5f), 60f, 30f));
            }
            if (Gametime == 6820)
            {
                CreatePlatform(new Platform(1, new Vector2(320, 350), Motions.PositionRoute.YAxisSin, 0, 44, 810)
                {
                    PositionRouteParam = new float[] { 0, 96, 100, 0 },
                    createWithScaling = true
                });
                CreateBone(new CustomBone(new Vector2(200, 310), Motions.PositionRoute.YAxisSin, 0, 156, 810)
                {
                    PositionRouteParam = new float[] { 0, 140, 200, 0 }
                });
                CreateBone(new CustomBone(new Vector2(440, 310), Motions.PositionRoute.YAxisSin, 0, 156, 810)
                {
                    PositionRouteParam = new float[] { 0, 140, 200, 0 }
                });
            }
            if (Gametime == 7680)
            {
                SetSoul(0);
            }
            if (Gametime == 7695)
            {
                PlaySound(FightResources.Sounds.pierce);
                SetBox(310, 180, 180);
                for (int i = 0; i < 4; i++)
                    CreateBone(new SideCircleBone(i * 90, 2f, 75, 750));
            }
            if (Gametime == 7705)
            {
                CreateBone(new CentreCircleBone(0, -2f, 75, 740));
                CreateBone(new CentreCircleBone(0, -1f, 280, 740) { ColorType = 1 });
                CreateBone(new CentreCircleBone(0, 1f, 280, 740) { ColorType = 2 });
                CreateBone(new CentreCircleBone(90, -2f, 75, 740));
            }
            if (Gametime == 8475)
            {
                TP();
                SetSoul(1);
                SetGreenBox();
            }
            if (Gametime == 8400)
            {
                float time = 100;
                Fortimes(31, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, 0, Rand(0, 1) * 2);
                    CreateArrow(time, Rand(0, 3), 4.0f, 1, 0);
                    time += 25f;
                });
            }
            if (Gametime == 9275)
            {
                Heart.Speed = 3.2f;
                SetBox(310, 280, 180);
                SetSoul(2);
            }
            if (Gametime >= 9300 && Gametime <= 10070 && (int)(Gametime % 25f) == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 3.5f, 32));
            }
            if (Gametime >= 9300 && Gametime <= 10070 && (int)(Gametime % 25f) == 12)
            {
                CreateBone(new CustomBone(new Vector2(180, 220), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                {
                    PositionRouteParam = new float[] { 3.5f, 0.0f },
                    LengthRouteParam = new float[] { 135, 60, Gametime, 136 },
                    RotationRouteParam = new float[] { 0 }
                });
            }
            if (Gametime >= 9300 && Gametime <= 10000 && Gametime % 200 == 0)
            {
                CreateGB(new NormalGB(new Vector2(580, 400), new Vector2(640, 480), new Vector2(1.0f, 1.0f), 180, 50, 10));
            }
            if (Gametime == 10075)
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (Gametime == 10000)
            {
                float time = 100;
                int[] hasArrow = { 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1,
                0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0};

                Fortimes(2, () =>
                {
                    int t = 0;
                    Fortimes(32, () =>
                    {
                        if (hasArrow[t] == 1)
                            CreateArrow(time, Rand(0, 3), 5.0f + Rand(-1, 1) * 1.5f, 0, 0);
                        time += 12.5f;
                        t++;
                    });
                });
            }
        }

        public void Noob()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                Heart.Gravity = 8.0f;
                Heart.ChangeColor(2);
            }
            if (Gametime == 40)
            {
                for (int i = 1; i <= 24; i++)
                    CreateBone(new SideCircleBone(i * 15, 2, 55f, 80f));
            }
            if (Gametime == 147)
            {
                CreateBone(new DownBone(true, 3.6f, 30));
                CreateBone(new DownBone(false, 3.6f, 30));
                PlaySound(FightResources.Sounds.pierce);
            }
            if (Gametime >= 190 && Gametime <= 350 && Gametime % 25 == 19)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 3.2f, 50));
                CreateBone(new DownBone(true, 2.7f, 24));
            }
            if (Gametime == 390)
            {
                Platform p = new(0, new Vector2(140, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { 1.9f, 0 }
                };
                CreatePlatform(p); Platform q = new(0, new Vector2(500, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { -1.9f, 0 }
                };
                CreatePlatform(q);
            }
            if (Gametime == 454)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 3.6f, 55));
                CreateBone(new DownBone(true, 3.6f, 55));
                CreateBone(new DownBone(false, 320, 3.6f, 55));
                CreateBone(new DownBone(true, 320, 3.6f, 55));
            }
            if (Gametime == 539)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 4.0f, 132));
                CreateBone(new UpBone(true, 4.0f, 132));
            }
            if (Gametime == 584)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 560 - 23)
            {
                float time = 110;
                Fortimes(5, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    time += 25f;
                });
            }
            if (Gametime == 782)
            {
                Heart.JumpSpeed = 4f;
                SetSoul(2);
                SetBox(290f, 220f, 170f);
            }
            if (Gametime >= 800 && Gametime <= 1200)
            {
                SetBox(290f + Sin((Gametime - 800) / 100f * 360f) * 40, 220f, 170f);
            }
            if (Gametime >= 785 && Gametime <= 1195 && Gametime % 100 == 85)
            {
                Heart.GiveForce(0, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 35)
            {
                Heart.GiveForce(180, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.9f, 20));
                CreateBone(new DownBone(false, 3.9f, 20));
                CreateBone(new UpBone(true, 3.9f, 100));
                CreateBone(new UpBone(false, 3.9f, 100));
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0 + 50)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.9f, 100));
                CreateBone(new DownBone(false, 3.9f, 100));
                CreateBone(new UpBone(true, 3.9f, 20));
                CreateBone(new UpBone(false, 3.9f, 20));
            }
            if (Gametime == 1200)
            {
                SetSoul(0);
                SetBox(290, 400, 240);
            }
            if (Gametime >= 1200 && Gametime <= 1480 && Gametime % 50 == 0)
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), 75, 30));
            }
            if (Gametime == 1530)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 1490)
            {
                float time = 111;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    time += 25f;
                });
                time += 24f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, 0, 0);
                    time += 12.5f;
                });

                time += 24f;

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    time += 12.5f;
                });

                time += 35.5f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, 0, 0);
                    time += 12.5f;
                });
            }
            if (Gametime == 1993)
            {
                Heart.JumpSpeed = 4.5f;
                Heart.Gravity = 5.5f;
                SetSoul(2);
                SetBox(310f, 280f, 160f);
            }
            if (Gametime >= 1995 && Gametime <= 2700 && Gametime % 100 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                float pos1 = Rand(0, 149);
                for (int i = -1; i < 2; i++)
                    CreateBone(new CustomBone(new Vector2(465, 310 + i * 83), Motions.PositionRoute.XAxisSin, 0, 40)
                    {
                        PositionRouteParam = new float[] { -2.6f, 37, 150, pos1 }
                    });
            }
            if (Gametime >= 2800 && Gametime <= 3400 && Gametime % 50 == 0)
            {
                CreateBone(new CustomBone(new Vector2(495, 225), Motions.PositionRoute.XAxisSin, 0, 75)
                {
                    PositionRouteParam = new float[] { -2.4f, 37, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreatePlatform(new Platform(0, new Vector2(495, 304), Motions.PositionRoute.XAxisSin, 0, 40)
                {
                    PositionRouteParam = new float[] { -2.4f, 37, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreateGB(new NormalGB(new Vector2(70, 390), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 0, 35f, 75f));
            }
            if (Gametime == 3540)
            {
                SetBox(310f, 310f, 160f);
                Heart.JumpSpeed = 5.0f;
                Heart.Gravity = 6.2f;
            }
            if (Gametime >= 3600 && Gametime <= 4300 && Gametime % 50 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                bool way = Rand(0, 1) == 0;
                int height = Rand(15, 60);
                CreateBone(new UpBone(way, 3.4f, 116 - height));
                CreateBone(new DownBone(way, 3.4f, height));
            }
            if (Gametime == 4367)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4300)
            {
                float time = 100;
                Fortimes(16, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, 0, 0);
                    time += 12.5f;
                });
                Fortimes(3, () =>
                {
                    Fortimes(16, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 4.0f, 1, 0);
                        time += 12.5f;
                    });
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 37.5f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                        CreateArrow(time, LastRand, 5.0f, 0, 0);
                        time += 25;
                    });
                    time += 37.5f;
                });

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 2);
                    time += 25;
                });
            }
            if (Gametime == 6005)
            {
                SetSoul(2);
                SetBox(310f, 240f, 160f);
                Heart.JumpSpeed = 5.4f;
                Heart.Gravity = 8.2f;
                CreatePlatform(new Platform(0, new Vector2(260, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
                CreatePlatform(new Platform(0, new Vector2(380, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
            }
            if (Gametime >= 6025 && Gametime <= 6700 && (int)(Gametime % 8f) == 0)
            {
                CreateBone(new DownBone(false, 2.5f, 20) { MarkScore = false });
                CreateBone(new UpBone(true, 2.5f, 20) { MarkScore = false });
            }
            if (Gametime >= 6855 && Gametime <= 7580 && (int)(Gametime % 6f) == 0)
            {
                CreateBone(new DownBone(false, 3.5f, 20) { MarkScore = false });
                CreateBone(new UpBone(true, 3.5f, 20) { MarkScore = false });
            }
            if (Gametime >= 6050 && Gametime <= 6780 && Gametime % 100 == 0)
            {
                CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, Vector2.One, 90f, 60f, 30f));
            }
            if (Gametime == 6820)
            {
                CreatePlatform(new Platform(1, new Vector2(320, 350), Motions.PositionRoute.YAxisSin, 0, 44, 810)
                {
                    PositionRouteParam = new float[] { 0, 96, 200, 0 },
                    createWithScaling = true
                });
            }
            if (Gametime == 7680)
            {
                SetSoul(0);
            }
            if (Gametime == 7695)
            {
                PlaySound(FightResources.Sounds.pierce);
                SetBox(310, 180, 180);
                for (int i = 0; i < 4; i++)
                    CreateBone(new SideCircleBone(i * 90, 2f, 75, 750));
            }
            if (Gametime == 7705)
            {
                CreateBone(new CentreCircleBone(0, -2f, 75, 740));
                CreateBone(new CentreCircleBone(90, -2f, 75, 740));
            }
            if (Gametime == 8475)
            {
                TP();
                SetSoul(1);
                SetGreenBox();
            }
            if (Gametime == 8400)
            {
                float time = 100;
                Fortimes(31, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, 0, Rand(0, 2));
                    time += 25f;
                });
            }
            if (Gametime == 9275)
            {
                SetBox(310, 280, 180);
                SetSoul(2);
            }
            if (Gametime >= 9300 && Gametime <= 10070 && Gametime % 25 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 2.7f, 30));
            }
            if (Gametime >= 9300 && Gametime <= 10000 && Gametime % 200 == 0)
            {
                CreateGB(new NormalGB(new Vector2(580, 400), new Vector2(640, 480), new Vector2(1.0f, 0.5f), 180, 50, 10));
            }
            if (Gametime == 10075)
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (Gametime == 10001)
            {
                float time = 100;
                int[] hasArrow = { 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1,
                0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0};

                Fortimes(2, () =>
                {
                    int t = 0;
                    Fortimes(32, () =>
                    {
                        if (hasArrow[t] == 1)
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        time += 12.5f;
                        t++;
                    });
                });
            }
        }

        public void Normal()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                Heart.Gravity = 8.0f;
                Heart.ChangeColor(2);
            }
            if (Gametime == 40)
            {
                for (int i = 1; i <= 24; i++)
                    CreateBone(new SideCircleBone(i * 15, 2, 55f, 100f));
            }
            if (Gametime == 150)
            {
                CreateBone(new DownBone(true, 3.6f, 40));
                CreateBone(new DownBone(false, 3.6f, 40));
                PlaySound(FightResources.Sounds.pierce);
            }
            if (Gametime >= 190 && Gametime <= 350 && Gametime % 25 == 19)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 3.2f, 54));
                CreateBone(new DownBone(true, 3.2f, 34));
            }
            if (Gametime == 400)
            {
                Platform p = new(1, new Vector2(140, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { 1.9f, 0 }
                };
                CreatePlatform(p); Platform q = new(1, new Vector2(500, 290), Motions.PositionRoute.linear, 0.0f, 60)
                {
                    PositionRouteParam = new float[] { -1.9f, 0 }
                };
                CreatePlatform(q);
            }
            if (Gametime == 454)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 3.6f, 55));
                CreateBone(new DownBone(true, 3.6f, 55));
                CreateBone(new DownBone(false, 320, 3.6f, 55));
                CreateBone(new DownBone(true, 320, 3.6f, 55));
            }
            if (Gametime == 500)
            {
                CreateBone(new DownBone(false, 4.0f, 87));
                CreateBone(new DownBone(true, 4.0f, 87));
                CreateBone(new UpBone(false, 4.0f, 72));
                CreateBone(new UpBone(true, 4.0f, 72));
            }
            if (Gametime == 550)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new UpBone(false, 4.0f, 122));
                CreateBone(new UpBone(true, 4.0f, 122));
            }
            if (Gametime == 584)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 560 - 23)
            {
                float time = 110;
                Fortimes(5, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, 0, 2);
                    time += 25f;
                });
            }
            if (Gametime == 782)
            {
                Heart.JumpSpeed = 4f;
                SetSoul(2);
                SetBox(290f, 220f, 170f);
            }
            if (Gametime >= 800 && Gametime <= 1200)
            {
                SetBox(290f + Sin((Gametime - 800) / 100f * 360f) * 50, 220f, 170f);
            }
            if (Gametime >= 785 && Gametime <= 1195 && Gametime % 100 == 85)
            {
                Heart.GiveForce(0, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 35)
            {
                Heart.GiveForce(180, 9);
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.9f, 20));
                CreateBone(new DownBone(false, 3.9f, 20));
                CreateBone(new UpBone(true, 3.9f, 108));
                CreateBone(new UpBone(false, 3.9f, 108));
            }
            if (Gametime >= 795 && Gametime <= 1195 && Gametime % 100 == 0 + 50)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(true, 3.9f, 108));
                CreateBone(new DownBone(false, 3.9f, 108));
                CreateBone(new UpBone(true, 3.9f, 20));
                CreateBone(new UpBone(false, 3.9f, 20));
            }
            if (Gametime == 1200)
            {
                SetSoul(0);
                SetBox(290, 400, 240);
            }
            if (Gametime >= 1200 && Gametime <= 1480 && Gametime % 25 == 0)
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), 75, 10));
            }
            if (Gametime == 1530)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 1490)
            {
                float time = 111;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, Rand(0, 1), 2);
                    time += 25f;
                });
                time += 24f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, Rand(0, 1), 0);
                    time += 12.5f;
                });

                time += 25f;

                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.0f, Rand(0, 1), 2);
                    time += 12.5f;
                });

                time += 37.5f;

                Fortimes(8, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f, Rand(0, 1), 0);
                    time += 12.5f;
                });
            }
            if (Gametime == 1993)
            {
                Heart.JumpSpeed = 4.5f;
                Heart.Gravity = 5.5f;
                SetSoul(2);
                SetBox(310f, 280f, 160f);
            }
            if (Gametime >= 1995 && Gametime <= 2700 && Gametime % 50 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                float pos1 = Rand(0, 149);
                float pos2 = Rand(-60, 60);
                for (int i = -2; i < 3; i++)
                    CreateBone(new CustomBone(new Vector2(465, 310 + i * 83 + pos2), Motions.PositionRoute.XAxisSin, 0, 40)
                    {
                        PositionRouteParam = new float[] { -2.6f, 39, 233, pos1 }
                    });
            }
            if (Gametime >= 2800 && Gametime <= 3400 && Gametime % 50 == 0)
            {
                CreateBone(new CustomBone(new Vector2(495, 225), Motions.PositionRoute.XAxisSin, 0, 75)
                {
                    PositionRouteParam = new float[] { -2.4f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(495, 380), Motions.PositionRoute.XAxisSin, 0, 130)
                {
                    PositionRouteParam = new float[] { -2.4f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                CreatePlatform(new Platform(0, new Vector2(495, 304), Motions.PositionRoute.XAxisSin, 0, 40)
                {
                    PositionRouteParam = new float[] { -2.4f, 25, 200 - (Gametime - 1750) / 20f, 0 }
                });
                if (Gametime % 100 == 0)
                    CreateGB(new NormalGB(new Vector2(70, 390), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 35f, 15f));
                CreateGB(new NormalGB(new Vector2(70, 390), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 0, 35f, 75f));
            }
            if (Gametime == 3540)
            {
                SetBox(310f, 310f, 160f);
                Heart.JumpSpeed = 5.0f;
                Heart.Gravity = 6.2f;
            }
            if (Gametime >= 3600 && Gametime <= 4300 && Gametime % 50 == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                bool way = Rand(0, 1) == 0;
                int height = Rand(20, 60);
                CreateBone(new UpBone(way, 3.4f, 120 - height));
                CreateBone(new DownBone(way, 3.4f, height));
            }
            if (Gametime == 4367)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4300)
            {
                float time = 100;
                Fortimes(16, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 1), 0, 0);
                    time += 12.5f;
                });
                Fortimes(3, () =>
                {
                    Fortimes(16, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 4.0f + Rand(-1, 0), 1, Rand(0, 1) == 1 ? Rand(0, 1) : 0);
                        time += 12.5f;
                    });
                    int d = Rand(0, 1);
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, d);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, d);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25f;
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, d);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 37.5f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 5.0f, 1, d);
                        CreateArrow(time, LastRand, 5.0f, 0, 0);
                        time += 25;
                    });
                    time += 37.5f;
                });

                Fortimes(3, () =>
                {
                    int d = Rand(0, 1);
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, d);
                    CreateArrow(time, LastRand, 5.0f, 0, 0);
                    time += 25;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 2);
                    time += 25;
                });
            }
            if (Gametime == 6035)
            {
                CreateBone(new CustomBone(new Vector2(320, 230), Motions.PositionRoute.cameFromUp, 0, 70, 738f));
                CreateBone(new CustomBone(new Vector2(320, 390), Motions.PositionRoute.cameFromDown, 0, 140, 738f));
            }
            if (Gametime == 6005)
            {
                SetSoul(2);
                SetBox(310f, 240f, 160f);
                Heart.JumpSpeed = 5.8f;
                Heart.Gravity = 9.2f;
                CreatePlatform(new Platform(0, new Vector2(260, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
                CreatePlatform(new Platform(0, new Vector2(380, 350), Motions.PositionRoute.cameFromDown, 0.0f, 40f, 788f));
            }
            if (Gametime >= 6025 && Gametime <= 6700 && (int)(Gametime % 8f) == 0)
            {
                CreateBone(new DownBone(false, 2.5f, 20) { MarkScore = false });
                CreateBone(new UpBone(true, 2.5f, 20) { MarkScore = false });
            }
            if (Gametime >= 6855 && Gametime <= 7580 && (int)(Gametime % 6f) == 0)
            {
                CreateBone(new DownBone(false, 3.5f, 20) { MarkScore = false });
                CreateBone(new UpBone(true, 3.5f, 20) { MarkScore = false });
            }
            if (Gametime >= 6050 && Gametime <= 6780 && Gametime % 100 == 0)
            {
                CreateGB(new NormalGB(new Vector2(120, 350), new Vector2(0, 480), new Vector2(1.0f, 0.5f), 0f, 60f, 10f));
                CreateGB(new NormalGB(new Vector2(520, 350), new Vector2(640, 480), new Vector2(1.0f, 0.5f), 180f, 60f, 10f));
                if (Rand(0, 1) == 1)
                    CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, Vector2.One, 90f, 60f, 30f));
                else
                    CreateGB(new NormalGB(new Vector2(260 + Rand(0, 1) * 120, 150), Heart.Centre, new Vector2(1.0f, 0.5f), 60f, 30f));
            }
            if (Gametime == 6820)
            {
                CreatePlatform(new Platform(1, new Vector2(320, 350), Motions.PositionRoute.YAxisSin, 0, 44, 810)
                {
                    PositionRouteParam = new float[] { 0, 96, 100, 0 },
                    createWithScaling = true
                });
            }
            if (Gametime == 7680)
            {
                SetSoul(0);
            }
            if (Gametime == 7695)
            {
                PlaySound(FightResources.Sounds.pierce);
                SetBox(310, 180, 180);
                for (int i = 0; i < 4; i++)
                    CreateBone(new SideCircleBone(i * 90, 2f, 75, 750));
            }
            if (Gametime == 7705)
            {
                CreateBone(new CentreCircleBone(0, -2f, 75, 740));
                CreateBone(new CentreCircleBone(0, -1f, 280, 740));
                CreateBone(new CentreCircleBone(90, -2f, 75, 740));
            }
            if (Gametime == 8475)
            {
                TP();
                SetSoul(1);
                SetGreenBox();
            }
            if (Gametime == 8400)
            {
                float time = 100;
                Fortimes(31, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f + Rand(-2, 1), 0, Rand(0, 2));
                    time += 25f;
                });
            }
            if (Gametime == 9275)
            {
                Heart.Speed = 3.0f;
                SetBox(310, 280, 180);
                SetSoul(2);
            }
            if (Gametime >= 9300 && Gametime <= 10070 && (int)(Gametime % 12.5f) == 0)
            {
                PlaySound(FightResources.Sounds.pierce);
                CreateBone(new DownBone(false, 4.5f, 25));
            }
            if (Gametime >= 9300 && Gametime <= 10000 && Gametime % 200 == 0)
            {
                CreateGB(new NormalGB(new Vector2(580, 400), new Vector2(640, 480), new Vector2(1.0f, 1.0f), 180, 50, 10));
            }
            if (Gametime == 10075)
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (Gametime == 10001)
            {
                float time = 100;
                int[] hasArrow = { 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1,
                0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0};

                Fortimes(2, () =>
                {
                    int t = 0;
                    Fortimes(32, () =>
                    {
                        if (hasArrow[t] == 1)
                            CreateArrow(time, Rand(0, 3), 5.0f + Rand(-1, 1) * 1.5f, 0, 0);
                        time += 12.5f;
                        t++;
                    });
                });
            }
        }

        public void Start()
        {
            //ScreenDrawing.UISettings.CreateUISurface();
            HeartAttribute.MaxHP = 7;
            TP(320, 360);
            InstantSetBox(270f, 200f, 200f);
            HeartAttribute.Gravity = 6;
            HeartAttribute.JumpSpeed = 5.8f;
        }
    }
}