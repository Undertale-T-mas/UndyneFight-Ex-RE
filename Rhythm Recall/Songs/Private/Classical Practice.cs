using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class ClassicalPractice : IWaveSet
    {
        public SongInformation Attributes => null;
        public string FightName => "GreenSoul Practice";

        public string Music => "undyne theme";

        public void Easy()
        {
            if (GametimeF == 2)
            {
                float time = 98;
                Fortimes(88, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 12.5f;
                    CreateArrow(time - 8, LastRand, 5.0f, 0, 0);
                });
            }
            if (GametimeF == 1102)
            {
                float time = 98;
                Fortimes(128, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 12.5f / 2;
                    CreateArrow(time, LastRand + 2, 5.0f, 1, 0);
                    time += 12.5f / 2;
                });
            }
            if (GametimeF == 2702)
            {
                float time = 98;
                Fortimes(128, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.2f, 0, Rand(0, 1));
                    time += 12.5f;
                });
            }
            if (GametimeF == 4302)
            {
                float time = 98;
                Fortimes(64 * 2, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    CreateArrow(time + 6.25f, Rand(0, 3), 5.0f, 1, 0);
                    time += 25f / 2;
                });
            }
            if (GametimeF == 5902)
            {
                float time = 98;
                Fortimes(48 * 2, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25f / 2;
                });
            }
            if (GametimeF == 7102)
            {
                float time = 98;
                Fortimes(96, () =>
                {
                    CreateArrow(time, Rand(0, 3), 6.0f + Rand(0, 4), 0, 0);
                    time += 12.5f;
                });
            }
            if (GametimeF == 8302)
            {
                float time = 98;
                Fortimes(96, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5, Rand(0, 1), 2);
                    time += 12.5f;
                });
            }
        }

        public void Extreme()
        {
            for (int i = 0; i < 300; i++)
            {
                if (GametimeF == 10 + 173 * 0 + i * 2 + 173 * 4 * i)
                {
                    float delta = 5.42f * 1;
                    float time = 5.51f * 8;
                    int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                    int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                    Fortimes(rhythm1.Length, (t) =>
                    {
                        NoteSummon(t, time, rhythm1, rhythm2);
                        time += delta;
                    });
                }
                if (GametimeF == 10 + 173 * 1 + i * 2 + 173 * 4 * i)
                {
                    float delta = 5.42f * 1;
                    float time = 5.51f * 8;
                    int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                    int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                    Fortimes(rhythm1.Length, (t) =>
                    {
                        NoteSummon(t, time, rhythm1, rhythm2);
                        time += delta;
                    });
                }
                if (GametimeF == 10 + 173 * 2 + i * 2 + 173 * 4 * i)
                {
                    float delta = 5.42f * 1;
                    float time = 5.51f * 8;
                    int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                    int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                    Fortimes(rhythm1.Length, (t) =>
                    {
                        NoteSummon(t, time, rhythm1, rhythm2);
                        time += delta;
                    });
                }
                if (GametimeF == 10 + 173 * 3 + i * 2 + 173 * 4 * i)
                {
                    float delta = 5.42f * 1;
                    float time = 5.51f * 8;
                    int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                    int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                    Fortimes(rhythm1.Length, (t) =>
                    {
                        NoteSummon(t, time, rhythm1, rhythm2);
                        time += delta;
                    });
                }
            }
        }

        public void Normal()
        {
            if (GametimeF == 0)
            {
                HeartAttribute.MaxHP = 198;
                HeartAttribute.KR = true;
            }
            if (GametimeF == 10)
            {
                SetSoul(0);
                SetBox(290, 100, 100);
            }
            if (GametimeF == 30)
            {
                CreateBone(new UpBone(false, 5.0f, 50));

                PlaySound(Sounds.pierce);
            }

            if (GametimeF == 90)
            {
                CreateGB(new NormalGB(new Vector2(320, 50), new Vector2(0), new Vector2(1, 1), 30, 30));
            }
            if (GametimeF == 150)
            {

                CreateGB(new NormalGB(new Vector2(320, 50), new Vector2(0), new Vector2(1, 1), 30, 30));
            }
            if (GametimeF == 210)
            {
                SetBox(0, 100, 100);
                CreateGB(new NormalGB(new Vector2(320, 50), new Vector2(0), new Vector2(1, 1), 30, 30));
            }
            if (GametimeF == 270)
            {
                SetBox(150, 100, 100);
                CreateGB(new NormalGB(new Vector2(320, 50), new Vector2(0), new Vector2(1, 1), 30, 30));
            }
            if (GametimeF == 330)
            {
                SetBox(320, 240, 240);
                CreateGB(new NormalGB(new Vector2(320, 50), new Vector2(0), new Vector2(1, 1), 30, 30));
            }
            if (GametimeF == 390)
            {

                CreateGB(new NormalGB(new Vector2(300, 80), new Vector2(0), new Vector2(3, 3), 100, 100));
            }

            if (GametimeF == 540)
            {
                SetSoul(1);
                SetBox(320, 240, 240);
                TP(320, 320);
            }
            if (GametimeF == 590)
            {
                CreateArrow(120, 0, 1, 0, 0);
                CreateArrow(180, 1, 1, 0, 0);
                CreateArrow(220, 0, 1, 0, 0);
                CreateArrow(300, 3, 1, 0, 0);
                CreateArrow(100, 3, 1, 0, 0);
                CreateArrow(180, 3, 1, 0, 0);
                CreateArrow(120, 3, 1, 0, 0);
                CreateArrow(360, 2, 2, 0, 0);
                CreateArrow(240, 2, 2, 0, 0);
                CreateArrow(480, 1, 2, 0, 0);
                CreateArrow(360, 1, 2, 0, 0);
                CreateArrow(300, 3, 3, 0, 0);
                CreateArrow(400, 2, 2, 0, 0);
                CreateArrow(360, 2, 2, 0, 0);
                CreateArrow(240, 3, 2, 0, 0);
                CreateArrow(60, 1, 3, 0, 0);
                CreateArrow(240, 0, 1, 0, 0);
                CreateArrow(240, 2, 1, 0, 0);
                CreateArrow(240, 0, 2, 0, 0);
                CreateArrow(240, 2, 3, 0, 0);
                CreateArrow(240, 0, 2, 0, 0);
                CreateArrow(360, 2, 2, 0, 0);
                CreateArrow(100, 0, 1, 0, 0);
                CreateArrow(240, 2, 2, 0, 0);
                CreateArrow(240, 0, 2, 0, 0);
                CreateArrow(240, 2, 2, 0, 0);
                CreateArrow(360, 0, 1, 0, 0);
                CreateArrow(240, 2, 1, 0, 0);
                CreateArrow(300, 0, 3, 0, 0);

            }
            if (GametimeF == 1100)
            {
                SetSoul(4);
                HeartAttribute.JumpTimeLimit = 1;
                CreateBone(new UpBone(true, 2.0f, 100));
                CreateBone(new UpBone(false, 3.0f, 100));
                CreateBone(new UpBone(false, 3.0f, 100));
                CreateBone(new UpBone(false, 3.0f, 100));
                CreateBone(new DownBone(false, 1.0f, 100));
                CreateBone(new DownBone(false, 3.0f, 100));
                CreateBone(new DownBone(false, 2.0f, 100));
                CreateBone(new DownBone(false, 3.0f, 100));
            }
            if (GametimeF == 1200)
            {
                CreateGB(new NormalGB(new Vector2(400, 80), new Vector2(400, 80), new Vector2(1, 1), 30, 30));

            }
            if (GametimeF == 1300)
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(400, 0), new Vector2(400, 80), new Vector2(1, 1), 30, 30));


            }

            if (GametimeF == 1460)
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(200, 90), new Vector2(400, 80), new Vector2(1, 1), 30, 30));
            }
            if (GametimeF >= 1560 && GametimeF % 3 == 0 && GametimeF <= 1700)
            {

                CreateGB(new NormalGB(GetVector2(160 + 180, GametimeF * 3) + new Vector2(320, 290), new Vector2(400, 0), new Vector2(1, 0.5f), 30, 30));

            }
            if (GametimeF == 1720)
            {
                SetSoul(3);


                CreateGB(new NormalGB(Heart.Centre + GetVector2(400, 0), new Vector2(400, 80), new Vector2(1, 1), 60, 30));
                CreateGB(new NormalGB(Heart.Centre + GetVector2(400, 45), new Vector2(400, 80), new Vector2(1, 1), 120, 30));
                CreateGB(new NormalGB(Heart.Centre + GetVector2(400, 90), new Vector2(400, 80), new Vector2(1, 1), 150, 30));
                CreateGB(new NormalGB(Heart.Centre + GetVector2(400, 135), new Vector2(400, 80), new Vector2(1, 1), 210, 30));

            }
            if (GametimeF == 1800)
            {
                CreateBone(new UpBone(true, 1.0f, 100));
                CreateBone(new DownBone(true, 1.0f, 100));
                CreateBone(new LeftBone(true, 1.0f, 100));
            }

            if (GametimeF == 1900)
            {
                CreateEntity(new Boneslab(180, 180, 10, 10));
            }
            if (GametimeF == 1960)
            {
                CreateEntity(new Boneslab(360, 180, 100, 100));
            }
            if (GametimeF == 2200)
            {
                SetSoul(1);
                SetBox(320, 240, 240);
                TP(320, 320);
            }
            if (GametimeF == 2400)
            {
                CreateArrow(120, 0, 1, 0, 0);
                CreateArrow(180, 1, 1, 0, 0);
                CreateArrow(220, 0, 1, 0, 0);
                CreateArrow(300, 3, 1, 0, 0);
                CreateArrow(100, 3, 1, 0, 0);
                CreateArrow(180, 3, 1, 0, 0);
                CreateArrow(120, 3, 1, 0, 0);
                CreateArrow(360, 2, 2, 0, 0);
                CreateArrow(240, 2, 2, 0, 0);
                CreateArrow(480, 1, 2, 0, 0);
                CreateArrow(360, 1, 2, 0, 0);
                CreateArrow(300, 3, 3, 0, 0);
                CreateArrow(400, 2, 2, 0, 0);
                CreateArrow(360, 2, 2, 0, 0);
                CreateArrow(240, 3, 2, 0, 0);
                CreateArrow(60, 1, 3, 0, 0);
                CreateArrow(240, 0, 1, 0, 0);
                CreateArrow(240, 2, 1, 0, 0);
                CreateArrow(240, 0, 2, 0, 0);
                CreateArrow(240, 2, 3, 0, 0);
                CreateArrow(240, 0, 2, 0, 0);
                CreateArrow(360, 2, 2, 0, 0);
                CreateArrow(100, 0, 1, 0, 0);
                CreateArrow(240, 2, 2, 0, 0);
                CreateArrow(240, 0, 2, 0, 0);
                CreateArrow(240, 2, 2, 0, 0);
                CreateArrow(360, 0, 1, 0, 0);
                CreateArrow(240, 2, 1, 0, 0);
                CreateArrow(300, 0, 3, 0, 0);
                CreateArrow(300, 0, 2, 1, 1);
            }
            if (GametimeF == 3000)
            {
                SetSoul(3);
                CreateEntity(new Boneslab(360, 180, 50, 50));
                CreateEntity(new Boneslab(90, 18, 50, 50));
                CreateEntity(new Boneslab(180, 18, 50, 50));
            }
            if (GametimeF == 3120)

            {
                CreateEntity(new Boneslab(90, 180, 50, 50));
                CreateEntity(new Boneslab(90, 18, 50, 50));
                CreateEntity(new Boneslab(180, 18, 50, 50));
            }
            if (GametimeF == 3300)
            {
                CreateEntity(new Boneslab(180, 180, 50, 50));
                CreateEntity(new Boneslab(90, 18, 50, 50));
                CreateEntity(new Boneslab(360, 18, 50, 50));
                CreateEntity(new Boneslab(270, 18, 50, 50));
            }
            if (GametimeF == 3420)
            {
                SetSoul(2);
            }
            if (GametimeF == 3480)
            {
                CreateBone(new DownBone(true, 1.0f, 50));
                CreateBone(new DownBone(true, 1.0f, 50));
                CreateBone(new UpBone(true, 0.5f, 160));
                CreateBone(new DownBone(true, 1.0f, 50));
                CreateBone(new UpBone(true, 1.0f, 160));
                CreateBone(new DownBone(true, 1.0f, 50));
                CreateBone(new UpBone(true, 1.0f, 160));
                CreateBone(new DownBone(true, 1.0f, 50));
                CreateBone(new UpBone(true, 1.0f, 160));
                CreateBone(new DownBone(true, 1.0f, 50));
                CreateBone(new UpBone(true, 1.0f, 160));
                CreateBone(new DownBone(true, 1.0f, 50));
            }
            if (GametimeF == 3600)
            {
                CreateEntity(new Boneslab(180, 120, 50, 50));
                CreateEntity(new Boneslab(90, 18, 50, 50));
                CreateEntity(new Boneslab(360, 10, 10, 50));
                CreateEntity(new Boneslab(270, 18, 50, 50));
            }
            if (GametimeF == 3700)
            {
                CreateEntity(new Boneslab(180, 120, 50, 50));
                CreateEntity(new Boneslab(90, 18, 50, 50));
                CreateEntity(new Boneslab(360, 10, 10, 50));
                CreateEntity(new Boneslab(270, 18, 50, 50));
            }
            if (GametimeF == 3800)
            {
                CreateEntity(new Boneslab(180, 120, 50, 50));
                CreateEntity(new Boneslab(90, 18, 50, 50));
                CreateEntity(new Boneslab(360, 10, 10, 50));
                CreateEntity(new Boneslab(270, 18, 50, 50));
            }
            if (GametimeF == 3900)
            {
                CreateEntity(new Boneslab(180, 120, 50, 50));
                CreateEntity(new Boneslab(90, 18, 30, 30));
                CreateEntity(new Boneslab(360, 10, 10, 30));
                CreateEntity(new Boneslab(270, 18, 50, 30));
            }
            if (GametimeF == 4020)
            {
                CreateEntity(new Boneslab(360, 40, 60, 30));
            }
            if (GametimeF == 4080)
            {
                CreateEntity(new Boneslab(360, 40, 60, 30));
            }
            if (GametimeF == 4140)
            {
                CreateEntity(new Boneslab(360, 40, 60, 30));
            }
            if (GametimeF == 4200)
            {
                CreateEntity(new Boneslab(360, 40, 60, 30));
            }
            if (GametimeF == 4300)
            {
                CreateBone(new DownBone(true, 1f, 50) { ColorType = 1 });
                HeartAttribute.Gravity = 11;
                CreateBone(new DownBone(false, 1f, 50) { ColorType = 2 });
                CreateBone(new DownBone(true, 2f, 50) { ColorType = 1 });
                CreateBone(new DownBone(false, 2f, 50) { ColorType = 2 });
            }
            if (GametimeF == 4360)
            {
                CreateBone(new DownBone(true, 1f, 50) { ColorType = 1 });
                HeartAttribute.Gravity = 11;
                CreateBone(new DownBone(false, 1f, 50) { ColorType = 2 });
                CreateBone(new DownBone(true, 2f, 50) { ColorType = 1 });
                CreateBone(new DownBone(false, 2f, 50) { ColorType = 2 });
            }
        }

        public void Noob()
        {
            if (GametimeF == 2)
            {
                float time = 98;
                Fortimes(88, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 12.5f;
                });
            }
            if (GametimeF == 1102)
            {
                float time = 98;
                Fortimes(128, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, Rand(0, 1), 0);
                    time += 12.5f;
                });
            }
            if (GametimeF == 2702)
            {
                float time = 98;
                Fortimes(128, () =>
                {
                    CreateArrow(time, Rand(0, 3), 4.2f, 0, Rand(0, 1));
                    time += 12.5f;
                });
            }
            if (GametimeF == 4302)
            {
                float time = 98;
                Fortimes(64, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                    time += 25f;
                });
            }
            if (GametimeF == 5902)
            {
                float time = 98;
                Fortimes(48, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                    time += 25f;
                });
            }
            if (GametimeF == 7102)
            {
                float time = 98;
                Fortimes(96, () =>
                {
                    CreateArrow(time, Rand(0, 3), 7.0f + Rand(0, 2), 0, 0);
                    time += 12.5f;
                });
            }
            if (GametimeF == 8302)
            {
                float time = 98;
                Fortimes(96, () =>
                {
                    CreateArrow(time, Rand(0, 3), 5, 0, 2);
                    time += 12.5f;
                });
            }
        }

        public void Hard()
        {
            for (int i = 0; i < 300; i++)
            {
                if (GametimeF == 10 + 173 * 0 + i * 2 + 173 * 4 * i)
                {
                    float delta = 5.42f * 1;
                    float time = 5.51f * 8;
                    int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 1, 1, 1, 1, 1, 1, 1,
                        0, 0, 3, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                    int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 1, 1, 1, 1, 1, 1, 1,
                        0, 0, 3, 0, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                    Fortimes(rhythm1.Length, (t) =>
                    {
                        NoteSummon(t, time, rhythm1, rhythm2);
                        time += delta;
                    });
                }
                if (GametimeF == 10 + 173 * 1 + i * 2 + 173 * 4 * i)
                {
                    float delta = 5.42f * 1;
                    float time = 5.51f * 8;
                    int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 1, 1, 1, 1, 1, 1, 1,
                        0, 0, 3, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                    int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 1, 1, 1, 1, 1, 1, 1,
                        0, 0, 3, 0, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                    Fortimes(rhythm1.Length, (t) =>
                    {
                        NoteSummon(t, time, rhythm1, rhythm2);
                        time += delta;
                    });
                }
                if (GametimeF == 10 + 173 * 2 + i * 2 + 173 * 4 * i)
                {
                    float delta = 5.42f * 1;
                    float time = 5.51f * 8;
                    int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                    int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 3, 0, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                    Fortimes(rhythm1.Length, (t) =>
                    {
                        NoteSummon(t, time, rhythm1, rhythm2);
                        time += delta;
                    });
                }
                if (GametimeF == 10 + 173 * 3 + i * 2 + 173 * 4 * i)
                {
                    float delta = 5.42f * 1;
                    float time = 5.51f * 8;
                    int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 0,
                        5, 5, 0, 1, 1, 0, 5, 5,
                        0, 0, 3, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                    int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        5, 5, 0, 1, 1, 0, 5, 5,
                        0, 0, 3, 0, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                    Fortimes(rhythm1.Length, (t) =>
                    {
                        NoteSummon(t, time, rhythm1, rhythm2);
                        time += delta;
                    });
                }
            }
        }

        private static void NoteSummon(int t, float time, int[] rhythm1, int[] rhythm2)
        {
            if (rhythm1[t] == 2)
            {
                CreateGB(new GreenSoulGB(time, 3, 0, 1));
            }
            else if (rhythm1[t] == 1)
            {
                CreateArrow(time, t % 2 * 2, 14, 0, 0);
            }
            else if (rhythm1[t] == 5)
            {
                CreateArrow(time, (t + 1) % 2 * 2, 14, 0, 0);
            }
            else if (rhythm1[t] == 4)
            {
                CreateArrow(time, "R", 7, 0, 0);
                CreateArrow(time, "+0", 7, 0, 1);
            }
            else if (rhythm1[t] == 3)
            {
                CreateArrow(time, "R", 7, 0, 0);
            }

            if (rhythm2[t] == 2)
            {
                CreateGB(new GreenSoulGB(time, 3, 1, 1));
            }
            else if (rhythm2[t] == 1)
            {
                CreateArrow(time, t % 2 * 2 + 1, 14, 1, 0);
            }
            else if (rhythm2[t] == 5)
            {
                CreateArrow(time, (t + 1) % 2 * 2 + 1, 14, 1, 0);
            }
            else if (rhythm2[t] == 4)
            {
                CreateArrow(time, "R", 7, 1, 0);
                CreateArrow(time, "+0", 7, 1, 1);
            }
            else if (rhythm2[t] == 3)
            {
                CreateArrow(time, "R", 7, 1, 0);
            }
        }

        public void Start()
        {
            SetGreenBox();
            TP();
            HeartAttribute.MaxHP = 7;
            SetSoul(1);
        }

        public void ExtremePlus()
        {
            throw new System.NotImplementedException();
        }
    }
}