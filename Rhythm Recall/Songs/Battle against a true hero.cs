using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class ClassicFight : IWaveSet
    {
        public string FightName => "Undertale Undyne";

        class ThisInformation : SongInformation
        {
            public override string BarrageAuthor => "T-mas";
            public override string SongAuthor => "Toby Fox";
            public override string PaintAuthor => "Undertale - Toby Fox";

            public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 4.5f),
                            new(Difficulty.Easy, 7.5f),
                            new(Difficulty.Normal, 12.0f),
                            new(Difficulty.Hard, 14.0f),
                            new(Difficulty.Extreme, 18.7f),
                    }
                );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 4.5f),
                            new(Difficulty.Easy, 7.5f),
                            new(Difficulty.Normal, 12.0f),
                            new(Difficulty.Hard, 14.0f),
                            new(Difficulty.Extreme, 18.7f),
                    }
                );
            public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 8.5f),
                            new(Difficulty.Easy, 11.5f),
                            new(Difficulty.Normal, 15.5f),
                            new(Difficulty.Hard, 19.5f),
                            new(Difficulty.Extreme, 21.5f),
                    }
                );
        }
        public SongInformation Attributes => new ThisInformation();

        public string Music => "undyne theme";

        public void Easy()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 49)
            {
                CreateArrow(150, 3, 2.0f, 0, 0);
                CreateArrow(200, 3, 2.0f, 0, 0);
                CreateArrow(250, 3, 2.0f, 0, 0);

                CreateArrow(350, 3, 1.6f, 0, 0);
                CreateArrow(449, 3, 1.6f, 0, 0);
                CreateArrow(548, 3, 1.6f, 0, 0);
                CreateArrow(647, 3, 1.6f, 0, 0);
            }
            if (Gametime == 447)
            {
                CreateArrow(350, 3, 1.6f, 0, 1);
                CreateArrow(448, 3, 1.6f, 0, 1);
                CreateArrow(547, 3, 1.6f, 0, 1);
                CreateArrow(647, 3, 1.6f, 0, 1);
            }
            if (Gametime == 292)
            {
                float time = 0, time2 = 0;
                Fortimes(7, () =>
                {
                    Fortimes(4, () =>
                    {
                        time2 += 19.8f;
                        CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                    });
                    time2 += 10;
                    CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                    time += 99.7f;
                    time2 = 0;
                }); Fortimes(4, () =>
                {
                    time2 += 19.8f;
                    CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                });
                time += 99.7f;
            }

            if (Gametime == 1098)
            {
                float time = 103;
                Fortimes(63, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.9f, 0, Rand(0, 1) == 1 ? Rand(0, 1) : 0);
                    time += 12.5f;
                });
            }
            if (Gametime == 1996)
            {
                SetBox(295, 160, 160);
                SetSoul(0);
            }
            if (Gametime >= 1990 && Gametime <= 2700 && Gametime % 25 == 1)
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(150, Rand(0, 359))));
            }
            if (Gametime == 2740)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 2698)
            {
                float time = 103;
                int a = 0;
                Fortimes(63, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.5f, a ^= 1, 0);
                    time += 12.5f;
                });
            }
            if (Gametime == 3606)
            {
                SetBox(295, 60, 100);
                SetSoul(0);
            }
            if (Gametime >= 3610 && Gametime <= 4300 && Gametime % 25 == 1)
            {
                CreateSpear(new Pike(new Vector2(320 + Rand(-1, 1) * 20, 362), 270, 75.0f));
            }

            if (Gametime == 4360)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4298)
            {
                float time = 103;
                Fortimes(32, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.2f, 0, 0);
                    time += 12.5f;
                    CreateArrow(time, LastRand + 2, 4.2f, 1, 1);
                    time += 12.5f;
                });
            }

            if (Gametime == 5236)
            {
                SetBox(275, 100, 100);
                SetSoul(0);
            }
            if (Gametime >= 5230 && Gametime <= 5880 && (Gametime % 25 == 1))
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(160, Rand(0, 359))));
            }

            if (Gametime == 5940)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 5897)
            {
                float time = 103;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 6589)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }

            if (Gametime >= 6637 && Gametime <= 7120 && (Gametime % 75 == 72))
            {
                float way = Rand(0, 1) * 2 - 1;
                float rot = Rand(0, 359);
                Fortimes(8, () =>
                {
                    CreateSpear(new CircleSpear(Heart.Centre, 4.3f * way, 1.63f, 187f, rot += 45, 0.02f));
                });
            }

            if (Gametime == 7140)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 7098)
            {
                float time = 103;

                Fortimes(7, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(5, () =>
                {
                    CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                    time += 12.5f;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 37.5f;
                });

                Fortimes(7, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(5, () =>
                {
                    CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(2, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 25f;
                });
                CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                CreateArrow(time - 4, Rand(0, 3), 6.2f, 1, 0);
                CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                time += 25f;

                time += 50f;

            }
            if (Gametime == 7790)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 7790 && Gametime <= 8299 && (Gametime % 25 == 14))
            {
                float rot = Rand(0, 359);
                Fortimes(8, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 4.53f, 154f, rot += 45, 40));
                });
            }
            if (Gametime == 8320)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 8297)
            {
                float time = 103;

                Fortimes(7, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(5, () =>
                {
                    CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                    time += 12.5f;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 37.5f;
                });

                Fortimes(7, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(5, () =>
                {
                    CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(2, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 25f;
                });
                CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                CreateArrow(time - 4, Rand(0, 3), 6.2f, 1, 0);
                CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                time += 25f;

                time += 50f;
            }
            if (Gametime == 8990)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 8990 && Gametime <= 9499 && (Gametime % 25 == 14))
            {
                float rot = Rand(0, 359);
                Fortimes(8, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 4.53f, 154f, rot += 40, 40));
                    CreateSpear(new SwarmSpear(Heart.Centre, 4.53f, 154f, rot += 5, 40));
                });
            }
        }

        public void Extreme()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 49)
            {
                CreateArrow(150, 3, 2.0f, 0, 0);
                CreateArrow(150, 1, 2.0f, 1, 1);
                CreateArrow(200, 3, 2.0f, 0, 0);
                CreateArrow(200, 1, 2.0f, 1, 1);
                CreateArrow(250, 3, 2.0f, 0, 0);
                CreateArrow(250, 1, 2.0f, 1, 1);

                CreateArrow(350, 3, 1.6f, 0, 0);
                CreateArrow(350, 1, 1.6f, 1, 1);
                CreateArrow(449, 3, 1.6f, 0, 0);
                CreateArrow(449, 1, 1.6f, 1, 1);
                CreateArrow(548, 3, 1.6f, 0, 0);
                CreateArrow(548, 1, 1.6f, 1, 1);
                CreateArrow(647, 3, 1.6f, 0, 0);
                CreateArrow(647, 1, 1.6f, 1, 1);
            }
            if (Gametime == 447)
            {
                CreateArrow(350, 3, 1.6f, 0, 1);
                CreateArrow(350, 1, 1.6f, 1, 1);
                CreateArrow(448, 3, 1.6f, 0, 1);
                CreateArrow(448, 1, 1.6f, 1, 1);
                CreateArrow(548, 3, 1.6f, 0, 1);
                CreateArrow(548, 1, 1.6f, 1, 1);
                CreateArrow(648, 3, 1.6f, 0, 1);
                CreateArrow(648, 1, 1.6f, 1, 1);
            }
            if (Gametime == 292)
            {
                float time = 0, time2 = 0;
                Fortimes(7, () =>
                {
                    Fortimes(4, () =>
                    {
                        time2 += 19.9f;
                        CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                    });
                    time2 += 10;
                    CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                    CreateArrow(time + time2 + 94, Rand(0, 3), 4.9f, 1, 0);
                    CreateArrow(time + time2 + 94 - 19.8f, Rand(0, 3), 4.9f, 1, 0);
                    time += 99.7f;
                    time2 = 0;
                });
                Fortimes(4, () =>
                {
                    time2 += 19.8f;
                    CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                });
                time += 99.7f;
            }

            if (Gametime == 1097)
            {
                float time = 102.5f;
                Fortimes(64, (x) =>
                {
                    CreateArrow(time, Rand(0, 3), 4, 0, 0);
                    int l = LastRand;
                    int r = Rand(0, 3);
                    while (r == l) r = Rand(0, 3);
                    if (x % 4 != 3)
                        CreateArrow(time, r, 4, 1, 0);
                    time += 12.5f;
                });
            }
            if (Gametime == 1996)
            {
                SetBox(295, 220, 220);
                SetSoul(0);
            }
            if (Gametime >= 1990 && Gametime <= 2700 && (int)(Gametime % 12.5f) == 1)
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(160, Rand(0, 359))));
                if (Gametime % 25 == 1)
                    CreateSpear(new NormalSpear(Heart.Centre + GetVector2(160, Rand(0, 359))));
            }
            if (Gametime == 2740)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 2698)
            {
                float time = 103;
                int a = 0;
                Fortimes(63, () =>
                {
                    a = Rand(0, 1);
                    CreateArrow(time, Rand(0, 3), 5.2f, a, 0);
                    time += 6.246f;
                    CreateArrow(time, LastRand, 5.2f, a, 1);
                    time += 6.246f;
                });
            }
            if (Gametime == 3606)
            {
                SetBox(295, 72, 100);
                SetSoul(0);
            }
            if (Gametime >= 3610 && Gametime <= 4300 && Gametime % 25 == 1)
            {
                CreateSpear(new Pike(new Vector2(320 + Rand(-1, 1) * 24, 370), 270, 55.0f));
                CreateSpear(new Pike(new Vector2(320 + Rand(-1, 1) * 24, 220), 90, 55.0f));
            }
            if (Gametime >= 3610 && Gametime <= 4300 && Gametime % 50 == 1)
            {
                CreateSpear(new Pike(new Vector2(320 + 0.5f * 26, 400), 270, 55.0f) { DrawingColor = Color.Gray });
                CreateSpear(new Pike(new Vector2(320 + 0.5f * 26, 190), 90, 55.0f) { DrawingColor = Color.Gray });
                CreateSpear(new Pike(new Vector2(320 - 0.5f * 26, 400), 270, 55.0f) { DrawingColor = Color.Gray });
                CreateSpear(new Pike(new Vector2(320 - 0.5f * 26, 190), 90, 55.0f) { DrawingColor = Color.Gray });
            }

            if (Gametime == 4360)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4298)
            {
                float time = 103;
                Fortimes(64, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.2f, 0, 0);
                    CreateArrow(time, LastRand + 2, 4.2f, 1, 1);
                    time += 12.5f;
                });
            }

            if (Gametime == 5236)
            {
                SetBox(275, 170, 170);
                SetSoul(0);
            }
            if (Gametime >= 5230 && Gametime <= 5880 && ((int)(Gametime % 12.5) == 1))
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(160, Rand(0, 359))) { Acceleration = 0.06f });
                if (Gametime % 25 == 1)
                    CreateSpear(new NormalSpear(Heart.Centre + GetVector2(160, Rand(0, 359))) { Acceleration = 0.03f });
            }

            if (Gametime == 5940)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 5897)
            {
                float time = 102;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    CreateArrow(time - 8, LastRand += 1, 6.2f, 0, 0);
                    CreateArrow(time - 8, LastRand += 1, 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 6589)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }

            if (Gametime >= 6637 && Gametime <= 7120 && (Gametime % 75 == 72))
            {
                float rot = Rand(0, 359);
                Fortimes(4, () =>
                {
                    CreateSpear(new CircleSpear(Heart.Centre, 2.5f, 1.3f, 187f, rot += 90, 0.0f));
                    CreateSpear(new CircleSpear(Heart.Centre, -2.5f, 1.3f, 187f, rot, 0.0f));
                });
            }

            if (Gametime == 7140)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 7098)
            {
                float time = 102;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    CreateArrow(time - 8, LastRand += 1, 6.2f, 0, 0);
                    CreateArrow(time - 8, LastRand += 1, 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 7790)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 7790 && Gametime <= 8299 && ((int)(Gametime % 25f) == 2))
            {
                float rot = Rand(0, 359);
                Fortimes(10, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 5.93f, 176f, rot += 36, 40));
                });
            }
            if (Gametime == 8320)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 8297)
            {
                float time = 102;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    CreateArrow(time - 8, LastRand += 1, 6.2f, 0, 0);
                    CreateArrow(time - 8, LastRand += 1, 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 8990)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 8990 && Gametime <= 9499 && ((int)(Gametime % 25f) == 2))
            {
                float rot = Rand(0, 359);
                int way = Rand(0, 1) * 2 - 1;
                Fortimes(4, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 5.53f, 169f, rot += 90, 40));
                });
                Fortimes(3, () =>
                {
                    CreateSpear(new CircleSpear(Heart.Centre, 2.1f * way, 2.43f, 189f, rot += 120, 0.02f));
                });
            }
        }

        public void Normal()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 49)
            {
                CreateArrow(150, 3, 2.0f, 0, 0);
                CreateArrow(150, 1, 2.0f, 1, 1);
                CreateArrow(200, 3, 2.0f, 0, 0);
                CreateArrow(200, 1, 2.0f, 1, 1);
                CreateArrow(250, 3, 2.0f, 0, 0);
                CreateArrow(250, 1, 2.0f, 1, 1);

                CreateArrow(350, 3, 1.6f, 0, 0);
                CreateArrow(350, 1, 1.6f, 1, 1);
                CreateArrow(449, 3, 1.6f, 0, 0);
                CreateArrow(449, 1, 1.6f, 1, 1);
                CreateArrow(548, 3, 1.6f, 0, 0);
                CreateArrow(548, 1, 1.6f, 1, 1);
                CreateArrow(647, 3, 1.6f, 0, 0);
                CreateArrow(647, 1, 1.6f, 1, 1);
            }
            if (Gametime == 447)
            {
                CreateArrow(350, 3, 1.6f, 0, 1);
                CreateArrow(350, 1, 1.6f, 1, 1);
                CreateArrow(448, 3, 1.6f, 0, 1);
                CreateArrow(448, 1, 1.6f, 1, 1);
                CreateArrow(547, 3, 1.6f, 0, 1);
                CreateArrow(547, 1, 1.6f, 1, 1);
                CreateArrow(647, 3, 1.6f, 0, 1);
                CreateArrow(647, 1, 1.6f, 1, 1);
            }
            if (Gametime == 292)
            {
                float time = 0, time2 = 0;
                Fortimes(7, () =>
                {
                    Fortimes(4, () =>
                    {
                        time2 += 19.8f;
                        CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                    });
                    time2 += 10;
                    CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                    time += 99.7f;
                    time2 = 0;
                }); Fortimes(4, () =>
                {
                    time2 += 19.8f;
                    CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                });
                time += 99.7f;
            }

            if (Gametime == 1097)
            {
                float time = 103;
                Fortimes(16, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.9f, 0, 0);
                    time += 12.44f;
                    CreateArrow(time, Rand(0, 3), 3.9f, 0, 0);
                    time += 12.44f;
                    CreateArrow(time, Rand(0, 3), 3.9f, 0, 0);
                    time += 12.44f;
                    CreateArrow(time, Rand(0, 3), 3.9f, 0, 0);
                    int l = LastRand;
                    int r = Rand(0, 3);
                    while (r == l) r = Rand(0, 3);
                    CreateArrow(time, r, 3.9f, 1, 0);
                    time += 12.44f;
                });
            }
            if (Gametime == 1996)
            {
                SetBox(295, 220, 220);
                SetSoul(0);
            }
            if (Gametime >= 1990 && Gametime <= 2700 && (int)(Gametime % 12.5f) == 1)
            {
                var v = new NormalSpear(Heart.Centre + GetVector2(180, Rand(0, 359)))
                {
                    Speed = 1.0f
                };
                CreateSpear(v);
            }
            if (Gametime == 2740)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 2698)
            {
                float time = 103;
                Fortimes(63, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.6f, 0, 0);
                    time += 6.246f;
                    CreateArrow(time, LastRand, 5.6f, 0, 0);
                    time += 6.246f;
                });
            }
            if (Gametime == 3606)
            {
                SetBox(295, 60, 100);
                SetSoul(0);
            }
            if (Gametime >= 3610 && Gametime <= 4300 && Gametime % 50 == 1)
            {
                CreateSpear(new Pike(new Vector2(320 + Rand(-1, 1) * 20, 372), 270, 65.0f));
                CreateSpear(new Pike(new Vector2(320 + Rand(-1, 1) * 20, 218), 90, 65.0f));
            }

            if (Gametime == 4360)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4298)
            {
                float time = 103;
                Fortimes(64, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.2f, 0, 0);
                    CreateArrow(time, LastRand, 4.2f, 1, 0);
                    time += 12.5f;
                });
            }

            if (Gametime == 5236)
            {
                SetBox(275, 220, 220);
                SetSoul(0);
            }
            if (Gametime >= 5230 && Gametime <= 5880 && ((int)(Gametime % 12.5) == 1))
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(160, Rand(0, 359))));
            }

            if (Gametime == 5940)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 5897)
            {
                float time = 103;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 0);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 0); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 0);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 0); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 6589)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }

            if (Gametime >= 6637 && Gametime <= 7120 && (Gametime % 75 == 72))
            {
                float rot = Rand(0, 359);
                Fortimes(3, () =>
                {
                    CreateSpear(new CircleSpear(Heart.Centre, 3.0f, 1.3f, 187f, rot += 120, 0.01f));
                    CreateSpear(new CircleSpear(Heart.Centre, -3.0f, 1.3f, 187f, rot, 0.01f));
                });
            }

            if (Gametime == 7140)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 7098)
            {
                float time = 103;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 0);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 0); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 0);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 0); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 7790)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 7790 && Gametime <= 8299 && ((int)(Gametime % 37.5f) == 2))
            {
                float rot = Rand(0, 359);
                Fortimes(3, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 5.53f, 169f, rot += 60, 50));
                    CreateSpear(new SwarmSpear(Heart.Centre, 25.53f, 629f, rot += 60, 50));
                });
            }
            if (Gametime == 8320)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 8297)
            {
                float time = 103;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 0);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 0); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 0);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 0); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 8990)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 8990 && Gametime <= 9499 && ((int)(Gametime % 37.5f) == 2))
            {
                float rot = Rand(0, 359);
                Fortimes(4, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 5.53f, 169f, rot += 45, 50));
                    CreateSpear(new SwarmSpear(Heart.Centre, 25.53f, 629f, rot += 45, 50));
                });
            }

        }

        public void Noob()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 49)
            {
                CreateArrow(150, 3, 2.0f, 0, 0);
                CreateArrow(200, 3, 2.0f, 0, 0);
                CreateArrow(250, 3, 2.0f, 0, 0);

                CreateArrow(350, 3, 1.6f, 0, 0);
                CreateArrow(449, 3, 1.6f, 0, 0);
                CreateArrow(548, 3, 1.6f, 0, 0);
                CreateArrow(647, 3, 1.6f, 0, 0);
            }
            if (Gametime == 447)
            {
                CreateArrow(350, 3, 1.6f, 0, 1);
                CreateArrow(448, 3, 1.6f, 0, 1);
                CreateArrow(547, 3, 1.6f, 0, 1);
                CreateArrow(647, 3, 1.6f, 0, 1);
            }
            if (Gametime == 292)
            {
                float time = 0, time2 = 0;
                Fortimes(8, () =>
                {
                    Fortimes(4, () =>
                    {
                        time2 += 20;
                        CreateArrow(time + time2 + 104, Rand(0, 2), 5.9f, 0, 0);
                    });
                    time += 99.7f;
                    time2 = 0;
                });
            }

            if (Gametime == 1098)
            {
                float time = 103;
                Fortimes(63, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.9f, 0, 0);
                    time += 12.5f;
                });
            }
            if (Gametime == 1996)
            {
                SetBox(275, 220, 220);
                SetSoul(0);
            }
            if (Gametime >= 1990 && Gametime <= 2700 && Gametime % 25 == 1)
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(150, Rand(0, 359))));
            }
            if (Gametime == 2740)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 2698)
            {
                float time = 103;
                Fortimes(63, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.5f, 0, Rand(0, 1) == 1 ? Rand(0, 1) : 0);
                    time += 12.5f;
                });
            }
            if (Gametime == 3606)
            {
                SetBox(295, 60, 100);
                SetSoul(0);
            }
            if (Gametime >= 3610 && Gametime <= 4300 && Gametime % 25 == 1)
            {
                CreateSpear(new Pike(new Vector2(320 + Rand(-1, 1) * 20, 362), 270, 55.0f));
            }

            if (Gametime == 4360)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4298)
            {
                float time = 103;
                Fortimes(32, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.2f, 0, 0);
                    time += 12.5f;
                    CreateArrow(time, LastRand, 4.2f, 1, 1);
                    time += 12.5f;
                });
            }

            if (Gametime == 5236)
            {
                SetBox(275, 180, 180);
                SetSoul(0);
            }
            if (Gametime >= 5230 && Gametime <= 5880 && (Gametime % 25 == 1))
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(150, Rand(0, 359))));
            }

            if (Gametime == 5940)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 5897)
            {
                float time = 103;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    time += 50f;
                });
            }
            if (Gametime == 6589)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }

            if (Gametime >= 6637 && Gametime <= 7120 && (Gametime % 75 == 72))
            {
                float way = Rand(0, 1) * 2 - 1;
                float rot = Rand(0, 359);
                Fortimes(7, () =>
                {
                    CreateSpear(new CircleSpear(Heart.Centre, 3.7f * way, 1.53f, 187f, rot += 360f / 7, 0.02f));
                });
            }

            if (Gametime == 7140)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 7098)
            {
                float time = 103;

                Fortimes(7, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(5, () =>
                {
                    CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                    time += 12.5f;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 37.5f;
                });

                Fortimes(7, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(5, () =>
                {
                    CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 25f;
                });
                time += 50f;

            }
            if (Gametime == 7790)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 7790 && Gametime <= 8299 && (Gametime % 25 == 14))
            {
                float rot = Rand(0, 359);
                Fortimes(6, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 4.53f, 154f, rot += 60, 40));
                });
            }
            if (Gametime == 8320)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 8297)
            {
                float time = 103;

                Fortimes(7, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(5, () =>
                {
                    CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                    time += 12.5f;
                });
                time += 25f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 37.5f;
                });

                Fortimes(7, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(5, () =>
                {
                    CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                    time += 12.5f;
                });
                time += 12.5f;
                Fortimes(3, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                    time += 25f;
                });
                time += 50f;

            }
            if (Gametime == 8990)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 8990 && Gametime <= 9499 && (Gametime % 25 == 14))
            {
                float rot = Rand(0, 359);
                Fortimes(6, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 4.53f, 154f, rot += 55, 40));
                    CreateSpear(new SwarmSpear(Heart.Centre, 4.53f, 154f, rot += 5, 40));
                });
            }
        }

        public void Hard()
        {
            if ((int)(GametimeF * 2) != (int)GametimeF * 2) return;
            if (Gametime == 2)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 49)
            {
                CreateArrow(150, 3, 2.0f, 0, 0);
                CreateArrow(150, 1, 2.0f, 1, 1);
                CreateArrow(200, 3, 2.0f, 0, 0);
                CreateArrow(200, 1, 2.0f, 1, 1);
                CreateArrow(250, 3, 2.0f, 0, 0);
                CreateArrow(250, 1, 2.0f, 1, 1);

                CreateArrow(350, 3, 1.6f, 0, 0);
                CreateArrow(350, 1, 1.6f, 1, 1);
                CreateArrow(449, 3, 1.6f, 0, 0);
                CreateArrow(449, 1, 1.6f, 1, 1);
                CreateArrow(548, 3, 1.6f, 0, 0);
                CreateArrow(548, 1, 1.6f, 1, 1);
                CreateArrow(647, 3, 1.6f, 0, 0);
                CreateArrow(647, 1, 1.6f, 1, 1);
            }
            if (Gametime == 447)
            {
                CreateArrow(350, 3, 1.6f, 0, 1);
                CreateArrow(350, 1, 1.6f, 1, 1);
                CreateArrow(448, 3, 1.6f, 0, 1);
                CreateArrow(448, 1, 1.6f, 1, 1);
                CreateArrow(547, 3, 1.6f, 0, 1);
                CreateArrow(547, 1, 1.6f, 1, 1);
                CreateArrow(647, 3, 1.6f, 0, 1);
                CreateArrow(647, 1, 1.6f, 1, 1);
            }
            if (Gametime == 292)
            {
                float time = 0, time2 = 0;
                Fortimes(7, () =>
                {
                    Fortimes(4, () =>
                    {
                        time2 += 19.8f;
                        CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                    });
                    time2 += 10;
                    CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                    CreateArrow(time + time2 + 94, Rand(0, 3), 4.9f, 1, 0);
                    time += 99.7f;
                    time2 = 0;
                }); Fortimes(4, () =>
                {
                    time2 += 19.8f;
                    CreateArrow(time + time2 + 104, Rand(0, 2), 6.9f, 0, 0);
                });
                time += 99.7f;
            }

            if (Gametime == 1097)
            {
                float time = 103;
                Fortimes(32, () =>
                {
                    CreateArrow(time, Rand(0, 3), 3.9f, 0, 0);
                    time += 12.44f;
                    CreateArrow(time, Rand(0, 3), 3.9f, 0, 0);
                    int l = LastRand;
                    int r = Rand(0, 3);
                    while (r == l) r = Rand(0, 3);
                    CreateArrow(time, r, 3.9f, 1, 0);
                    time += 12.44f;
                });
            }
            if (Gametime == 1996)
            {
                SetBox(295, 220, 220);
                SetSoul(0);
            }
            if (Gametime >= 1990 && Gametime <= 2700 && (int)(Gametime % 12.5f) == 1)
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(150, Rand(0, 359))));
            }
            if (Gametime == 2740)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 2698)
            {
                float time = 103;
                Fortimes(63, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.2f, 0, 0);
                    time += 6.246f;
                    CreateArrow(time, LastRand, 5.2f, 0, 1);
                    time += 6.246f;
                });
            }
            if (Gametime == 3606)
            {
                SetBox(295, 60, 100);
                SetSoul(0);
            }
            if (Gametime >= 3610 && Gametime <= 4300 && Gametime % 25 == 1)
            {
                CreateSpear(new Pike(new Vector2(320 + Rand(-1, 1) * 20, 372), 270, 55.0f));
                CreateSpear(new Pike(new Vector2(320 + Rand(-1, 1) * 20, 218), 90, 55.0f));
            }

            if (Gametime == 4360)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 4298)
            {
                float time = 103;
                Fortimes(64, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.2f, 0, 0);
                    CreateArrow(time, LastRand, 4.2f, 1, 1);
                    time += 12.5f;
                });
            }

            if (Gametime == 5236)
            {
                SetBox(275, 170, 170);
                SetSoul(0);
            }
            if (Gametime >= 5230 && Gametime <= 5880 && ((int)(Gametime % 12.5) == 1))
            {
                CreateSpear(new NormalSpear(Heart.Centre + GetVector2(160, Rand(0, 359))));
            }

            if (Gametime == 5940)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 5897)
            {
                float time = 103;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 6589)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }

            if (Gametime >= 6637 && Gametime <= 7120 && (Gametime % 75 == 72))
            {
                float rot = Rand(0, 359);
                Fortimes(4, () =>
                {
                    CreateSpear(new CircleSpear(Heart.Centre, 3.0f, 1.3f, 187f, rot += 90, 0.01f));
                    CreateSpear(new CircleSpear(Heart.Centre, -3.0f, 1.3f, 187f, rot, 0.01f));
                });
            }

            if (Gametime == 7140)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 7098)
            {
                float time = 103;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 7790)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 7790 && Gametime <= 8299 && ((int)(Gametime % 25f) == 2))
            {
                float rot = Rand(0, 359);
                Fortimes(3, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 5.53f, 169f, rot += 60, 40));
                    CreateSpear(new SwarmSpear(Heart.Centre, 25.53f, 629f, rot += 60, 40));
                });
            }
            if (Gametime == 8320)
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (Gametime == 8297)
            {
                float time = 103;
                Fortimes(1, () =>
                {
                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 25f;
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        time += 37.5f;
                    });

                    Fortimes(7, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 0, 0);
                        CreateArrow(time, LastRand, 6.2f, 1, 1);
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(5, () =>
                    {
                        CreateArrow(time, LastRand += 2, 6.2f, 0, 1);
                        CreateArrow(time, LastRand += 2, 6.2f, 1, 1); LastRand += 2;
                        time += 12.5f;
                    });
                    time += 12.5f;
                    Fortimes(2, () =>
                    {
                        CreateArrow(time, Rand(0, 3), 6.2f, 1, 0);
                        time += 25f;
                    });
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 1, 0);
                    CreateArrow(time - 4, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time, LastRand += 1, 6.2f, 1, 0);
                    CreateArrow(time - 8, Rand(0, 3), 6.2f, 0, 0);
                    time += 25f;

                    time += 50f;
                });
            }
            if (Gametime == 8990)
            {
                SetBox(285, 560, 300);
                SetSoul(0);
            }
            if (Gametime >= 8990 && Gametime <= 9499 && ((int)(Gametime % 25f) == 2))
            {
                float rot = Rand(0, 359);
                Fortimes(4, () =>
                {
                    CreateSpear(new SwarmSpear(Heart.Centre, 5.53f, 169f, rot += 45, 40));
                    CreateSpear(new SwarmSpear(Heart.Centre, 25.53f, 629f, rot += 45, 40));
                });
            }

        }

        public void Start()
        {
            HeartAttribute.Speed = 2.85f;
        }

        public void ExtremePlus()
        {
            throw new System.NotImplementedException();
        }
    }
}