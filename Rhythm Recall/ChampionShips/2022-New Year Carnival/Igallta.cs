using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.CameraEffect;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class Igallta : IChampionShip
    {
        public Igallta()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new();
            difficulties.Add("Old", Difficulty.Noob);
            difficulties.Add("HD 12", Difficulty.Normal);
            difficulties.Add("IN 15", Difficulty.Extreme);
            difficulties.Add("AT 16", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            class Information : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {

                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {

                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {

                    }
                    );
            }
            public SongInformation Attributes => new Information();
            private class KickCounter : Entity
            {
                public override void Draw()
                {
                    Font.NormalFont.CentreDraw(MathF.Round(time, 1) + "", new Microsoft.Xna.Framework.Vector2(320, 80), Color.White, GameStates.SpriteBatch);
                    Font.NormalFont.CentreDraw(MathF.Round(lastTime, 1) + "", new Microsoft.Xna.Framework.Vector2(320, 140), Color.White, GameStates.SpriteBatch);
                    if (time > 0)
                    {
                        Font.NormalFont.CentreDraw("Time = " + (count * 1.0f / time), new Microsoft.Xna.Framework.Vector2(320, 120), Color.White, GameStates.SpriteBatch);
                        Font.NormalFont.CentreDraw("Frame = " + 60 * (count * 1.0f / time), new Microsoft.Xna.Framework.Vector2(320, 160), Color.White, GameStates.SpriteBatch);
                    }
                }

                private int count = -1;
                private float time = 0, lastTime = 0;

                public override void Update()
                {
                    if (GameStates.IsKeyPressed(InputIdentity.Alternate) && time == 0)
                    {
                        lastTime = time;
                        count = 0;
                        time += 0.0001f;
                        return;
                    }
                    if (time == 0) return;
                    time++;
                    if (GameStates.IsKeyPressed(InputIdentity.Alternate))
                    {
                        lastTime = time;
                        count++;
                        PlaySound(Sounds.pierce);
                    }
                }
            }

            public Game() : base(62.5f / (230 / 60f)) { }

            public static Game instance;

            public string Music => "igallta";
            //  public string Music => "Brain Power";

            public string FightName => "Igallta";

            #region Non-ChampionShip
            public void Easy()
            {
                throw new System.NotImplementedException();
            }
            bool oldis = true;
            public void Noob()
            {
                if (oldis) GametimeDelta = -145f;
                oldis = false;
                ExPlusBarrage.Starting0(1);
                //3.913043478260
                float del = 130.5f;

                ExPlusBarrage.Starting1A(256);
                ExPlusBarrage.Starting1B(256 + del * 1);
                ExPlusBarrage.Starting1A(256 + del * 2);
                ExPlusBarrage.Starting1C(256 + del * 3);
                ExPlusBarrage.Starting1A(256 + del * 4);
                ExPlusBarrage.Starting1B2(256 + del * 5);
                ExPlusBarrage.Starting1A(256 + del * 6);
                ExPlusBarrage.Starting1D(256 + del * 7);

                float time1 = 256 + del * 8, del2 = 113.7f;

                ExPlusBarrage.Area1A(time1 + del2 * 0);
                ExPlusBarrage.Area1B(time1 + del2 * 1);
                ExPlusBarrage.Area1C(time1 + del2 * 2);
                ExPlusBarrage.Area1D(time1 + del2 * 3);

                float time2 = time1 + del2 * 4 + 4;

                ExPlusBarrage.Breaking1(time2);
                ExPlusBarrage.Area2A(time2 + del);
                ExPlusBarrage.Area2B(time2 + del * 2);
                ExPlusBarrage.Area2C(time2 + del * 3);
                ExPlusBarrage.Area2D(time2 + del * 4);

                float time3 = time2 + del * 5 + 128;
                ExPlusBarrage.Breaking2(time3 + del * 0);
            }
            public void Hard()
            {
                throw new System.NotImplementedException();
            }
            public void Extreme()
            {
                Line[] ls = GetAll<Line>();
                for (int i = 0; i < ls.Length; i++)
                {
                    int x = i;
                    ls[x].DrawingColor = linecolor;
                }
                if (colorwhitebool)
                {
                    linecolor = Color.White;
                }
                if (!colorwhitebool && colorbluebool)
                {
                    linecolor = new(168, 231, 255);
                }
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddNiceEvent(() =>
                {
                    colorbluebool = true;
                });
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddOkayEvent(() =>
                {
                    colorbluebool = true;
                });
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddMissEvent(() =>
                {
                    colorwhitebool = true;
                });
                if (InBeat(-1)) Epart1();
                if (InBeat(4 * 4 * 5)) Epart2();
                if (InBeat(4 * 4 * 8 - 4)) Epart3();
                if (InBeat(4 * 4 * 13 - 4)) Epart4();
                if (InBeat(4 * 4 * 19 - 4)) Epart5();
                if (InBeat(4 * 4 * 22 - 8)) Epart6();
                if (InBeat(4 * 4 * 25 + 4)) Epart7();
            }
            #endregion
            Color linecolor = new(255, 251, 167);
            bool colorbluebool = false;
            bool colorwhitebool = false;
            public void Normal()
            {
                Line[] ls = GetAll<Line>();
                for (int i = 0; i < ls.Length; i++)
                {
                    int x = i;
                    ls[x].DrawingColor = linecolor;
                }
                if (colorwhitebool)
                {
                    linecolor = Color.White;
                }
                if (!colorwhitebool && colorbluebool)
                {
                    linecolor = new(168, 231, 255);
                }
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddNiceEvent(() =>
                {
                    colorbluebool = true;
                });
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddOkayEvent(() =>
                {
                    colorbluebool = true;
                });
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddMissEvent(() =>
                {
                    colorwhitebool = true;
                });
                if (InBeat(-1)) Npart1();
                if (InBeat(4 * 4 * 5)) Npart2();
                if (InBeat(4 * 4 * 8 - 4)) Npart3();
                if (InBeat(4 * 4 * 13 - 4)) Npart4();
                if (InBeat(4 * 4 * 19 - 4)) Npart5();
                if (InBeat(4 * 4 * 22 - 8)) Npart6();
                if (InBeat(4 * 4 * 25 + 4)) Npart7();
            }

            private static class ExPlusBarrage
            {
                public static void Starting0(float del)
                {
                    if (GametimeF == 0 + del)
                    {
                        float time = 106;
                        float det = 3.63f;
                        CreateGB(new GreenSoulGB(106, 3, 0, 110));

                        CreateGB(new GreenSoulGB(106 - 21, Rand(0, 3), 1, 6));
                        CreateArrow(106 - 21, "R", 7.2f, 0, 0);

                        CreateGB(new GreenSoulGB(106 - 42, Rand(0, 3), 0, 6));
                        CreateArrow(106 - 42, "R", 7.2f, 1, 0);

                        CreateGB(new GreenSoulGB(106 - 63, Rand(0, 3), 1, 6));
                        CreateArrow(106 - 63, "R", 7.2f, 0, 0);

                        Fortimes(33, (t) =>
                        {
                            t >>= 1;
                            if (t != 16)
                                CreateArrow(time, 1 + t, 6.2f * (((t + 1) % 4 == 3) ? 2 : 1), 1, 0, ((t + 1) % 4) switch
                                {
                                    0 => ArrowAttribute.RotateL,
                                    1 => ArrowAttribute.None,
                                    2 => ArrowAttribute.RotateR,
                                    3 => ArrowAttribute.SpeedUp,
                                    _ => throw new Exception()
                                });
                            else
                            {
                                CreateGB(new GreenSoulGB(time, 1, 0, 6));
                                CreateGB(new GreenSoulGB(time, 1, 1, 6));
                            }
                            time += det;
                        });
                    }
                    if (GametimeF == 130 + 106 + del)
                    {
                        SetBox(300, 200, 140);
                        SetSoul(0);
                    }
                }
                public static void Starting1A(float del)
                {
                    del = (int)del;
                    if (GametimeF == del)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 5, 64) { ColorType = 2 });
                        CreateBone(new UpBone(false, 5, 64));
                    }
                    if (GametimeF == 16 + del)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 5, 64));
                        CreateBone(new UpBone(true, 5, 64) { ColorType = 2 });
                    }

                    if (GametimeF == 34 + del)
                    {
                        CreateGB(new NormalGB(new(320 - 90, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 182, 10));
                    }
                    if (GametimeF == 40 + del)
                    {
                        CreateGB(new NormalGB(new(320 - 30, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 188, 10));
                    }
                    if (GametimeF == 46 + del)
                    {
                        CreateGB(new NormalGB(new(320 + 30, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 194, 10));
                    }
                    if (GametimeF == 52 + del)
                    {
                        CreateGB(new NormalGB(new(320 + 90, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 200, 10));
                    }
                    if (GametimeF == 87 + del)
                    {
                        PlaySound(Sounds.spearShoot);
                        PlaySound(Sounds.spearShoot);
                    }
                    if (GametimeF == 105 + del)
                    {
                        PlaySound(Sounds.spearShoot);
                        PlaySound(Sounds.spearShoot);
                    }
                    if (GametimeF == 32 + del)
                    {
                        PlaySound(Sounds.spearAppear);
                        float timedel1 = 87 - 32;
                        float timedel2 = 105 - 32;
                        float _rot = Rand(0, 359);
                        for (int i = 0; i < 6; i++)
                        {
                            float rot = i * 60 + _rot;
                            CreateEntity(new SwarmSpear(Heart.Centre, 7, 122, rot, timedel1));
                        }
                        for (int i = 0; i < 6; i++)
                        {
                            float rot = i * 60 + 45 + _rot;
                            CreateEntity(new SwarmSpear(Heart.Centre, 11, 182, rot, timedel2));
                        }
                    }
                }
                public static void Starting1B(float del)
                {
                    del = (int)del;
                    if (GametimeF == 0 + del)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 64) { ColorType = 1 });
                        CreateBone(new UpBone(true, 6, 64));
                    }
                    if (GametimeF == 16 + del)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 6, 64));
                        CreateBone(new UpBone(false, 6, 64) { ColorType = 1 });
                    }

                    if (GametimeF == 34 + del)
                    {
                        CreateGB(new NormalGB(new(320 - 60, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 182, 10));
                    }
                    if (GametimeF == 40 + del)
                    {
                        CreateGB(new NormalGB(new(320 - 0, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 188, 10));
                    }
                    if (GametimeF == 46 + del)
                    {
                        CreateGB(new NormalGB(new(320 + 60, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 194, 10));
                    }
                    if (GametimeF == 52 + del)
                    {
                        CreateGB(new NormalGB(new(320 + Rand(-220, 220), 120), Vector2.Zero, new(1, 0.5f), 216 - 200, 10));
                    }
                    if (GametimeF == 108 + del)
                    {
                        PlaySound(Sounds.boneSpawnLarge);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(220, 230), 2, Motions.PositionRoute.linear, new float[] { 5, 0 }, -5f, 166, -1);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(215, 225), 2, Motions.PositionRoute.linear, new float[] { 5, 0 }, -5f, 176, -1);
                    }
                    if (GametimeF == 118 + del)
                    {
                        PlaySound(Sounds.boneSpawnLarge);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(420, 370), 2, Motions.PositionRoute.linear, new float[] { -4, 0 }, 5f, 166, -1);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(425, 375), 2, Motions.PositionRoute.linear, new float[] { -4, 0 }, 5f, 176, -1);
                    }
                }
                public static void Starting1C(float del)
                {
                    del = (int)del;
                    if (GametimeF == 0 + del)
                    {
                        PlaySound(Sounds.boneSpawnLarge);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(220, 370), 2, Motions.PositionRoute.linear, new float[] { 5f, 0 }, -5f, 166, -1);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(215, 375), 2, Motions.PositionRoute.linear, new float[] { 5f, 0 }, -5f, 176, -1);
                    }
                    if (GametimeF == 9 + del)
                    {
                        PlaySound(Sounds.boneSpawnLarge);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(420, 230), 2, Motions.PositionRoute.linear, new float[] { -5, 0 }, 5f, 166, -1);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(425, 225), 2, Motions.PositionRoute.linear, new float[] { -5, 0 }, 5f, 176, -1);
                    }
                    if (GametimeF == 32 + del)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 5, 64) { ColorType = 2 });
                        CreateBone(new DownBone(false, 210, 5, 64) { ColorType = 1 });
                        CreateBone(new UpBone(true, 5, 64));
                    }
                    if (GametimeF == 48 + del)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 5, 64) { ColorType = 2 });
                        CreateBone(new UpBone(true, 430, 5, 64) { ColorType = 1 });
                        CreateBone(new DownBone(false, 5, 64));
                    }
                    if (GametimeF == 64 + del)
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone bone1 = new(false, 6, 198) { ColorType = 2 };
                        LeftBone bone2 = new(true, 6, 198) { ColorType = 1 };
                        CreateBone(bone1); CreateBone(bone2);
                        AddInstance(new TimeRangedEvent(0, 100, () =>
                        {
                            bone1.Speed -= 0.2f;
                            bone2.Speed -= 0.2f;
                        }));
                    }
                    if (GametimeF == 97 + del)
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone bone1 = new(false, 6, 198) { ColorType = 2 };
                        LeftBone bone2 = new(true, 6, 198) { ColorType = 1 };
                        CreateBone(bone1); CreateBone(bone2);
                        AddInstance(new TimeRangedEvent(0, 100, () =>
                        {
                            bone1.Speed -= 0.15f;
                            bone2.Speed -= 0.15f;
                        }));
                    }
                }
                public static void Starting1B2(float del)
                {
                    del = (int)del;
                    if (GametimeF == 0 + del)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 64) { ColorType = 1 });
                        CreateBone(new UpBone(true, 6, 64));
                    }
                    if (GametimeF == 9 + del)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 6, 64));
                        CreateBone(new UpBone(false, 6, 64) { ColorType = 1 });
                    }

                    if (GametimeF == 34 + del)
                    {
                        CreateGB(new NormalGB(new(320 - 60, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 182, 10));
                    }
                    if (GametimeF == 40 + del)
                    {
                        CreateGB(new NormalGB(new(320 - 0, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 188, 10));
                    }
                    if (GametimeF == 46 + del)
                    {
                        CreateGB(new NormalGB(new(320 + 60, 120), Vector2.Zero, new(1, 0.5f), 90, 216 - 194, 10));
                    }
                    if (GametimeF == 52 + del)
                    {
                        CreateGB(new NormalGB(new(320 + Rand(-220, 220), 120), Vector2.Zero, new(1, 0.5f), 216 - 200, 10));
                    }
                    if (GametimeF == 108 + del)
                    {
                        PlaySound(Sounds.boneSpawnLarge);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(220, 230), 2, Motions.PositionRoute.linear, new float[] { 5, 0 }, -5f, 166, -1);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(215, 225), 2, Motions.PositionRoute.linear, new float[] { 5, 0 }, -5f, 176, -1);
                    }
                    if (GametimeF == 118 + del)
                    {
                        PlaySound(Sounds.boneSpawnLarge);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(420, 370), 2, Motions.PositionRoute.linear, new float[] { -4, 0 }, 5f, 166, -1);
                        UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(
                            new(425, 375), 2, Motions.PositionRoute.linear, new float[] { -4, 0 }, 5f, 176, -1);
                    }
                }
                public static void Starting1D(float del)
                {
                    del = (int)del;
                    int _time = (int)(Gametime - del);
                    if (_time == 0)
                    {
                        SetSoul(4);
                        TP();
                        SetBox(300, 140, 140);
                    }
                    if (_time == 64)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(false, 6, 74));
                    }
                    if (_time == 72)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 6, 74));
                    }
                    if (_time == 80)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new RightBone(true, 6, 74));
                    }
                    if (_time == 88)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 6, 74));
                    }
                    if (_time == 97)
                    {
                        PlaySound(Sounds.boneSpawnLarge);
                        for (int i = 3; i <= 5; i++)
                        {
                            CreateBone(new DownBone(false, i + 8, 86 - i * 4) { ColorType = 2 });
                            CreateBone(new DownBone(true, i + 8, 86 - i * 4) { ColorType = 2 });
                            CreateBone(new UpBone(false, i + 8, 86 - i * 4) { ColorType = 2 });
                            CreateBone(new UpBone(true, i + 8, 86 - i * 4) { ColorType = 2 });
                            CreateBone(new LeftBone(false, i + 8, 86 - i * 4) { ColorType = 2 });
                            CreateBone(new LeftBone(true, i + 8, 86 - i * 4) { ColorType = 2 });
                            CreateBone(new RightBone(false, i + 8, 86 - i * 4) { ColorType = 2 });
                            CreateBone(new RightBone(true, i + 8, 86 - i * 4) { ColorType = 2 });
                        }
                    }
                    if (_time == 105)
                    {
                        PlaySound(Sounds.boneSpawnLarge);
                        for (int i = 0; i <= 4; i++)
                        {
                            CreateBone(new DownBone(false, i + 8, 86 - i * 4) { ColorType = 1 });
                            CreateBone(new DownBone(true, i + 8, 86 - i * 4) { ColorType = 1 });
                            CreateBone(new UpBone(false, i + 8, 86 - i * 4) { ColorType = 1 });
                            CreateBone(new UpBone(true, i + 8, 86 - i * 4) { ColorType = 1 });
                            CreateBone(new LeftBone(false, i + 8, 86 - i * 4) { ColorType = 1 });
                            CreateBone(new LeftBone(true, i + 8, 86 - i * 4) { ColorType = 1 });
                            CreateBone(new RightBone(false, i + 8, 86 - i * 4) { ColorType = 1 });
                            CreateBone(new RightBone(true, i + 8, 86 - i * 4) { ColorType = 1 });
                        }
                    }
                    if (_time == 112)
                    {
                        SetBox(-270, 25, 25);
                    }
                    if (_time == 120)
                    {
                        SetSoul(1);
                        InstantSetBox(740, 70, 70);
                        SetGreenBox();
                    }
                    if (_time >= 116 && _time < 124)
                    {
                        ScreenDrawing.ScreenAngle += 4f;
                        ScreenScale = ScreenScale * 0.8f + 0 * 0.2f;
                    }
                    if (_time > 120 && _time <= 132)
                    {
                        InstantTP(FightBox.instance.Centre);
                    }

                    if (_time >= 124 && _time <= 132)
                    {
                        ScreenScale = ScreenScale * 0.8f + 1.2f * 0.2f;
                    }
                }

                public static void Area1A(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - (int)del);
                    if (_time < 128 && _time >= 0)
                        InstantTP(FightBox.instance.Centre);
                    if (_time == 0)
                    {
                        TP();
                    }
                    if (_time == -40)
                    {
                        float curTime = 40;
                        string[] rway = {
                            "3", "/", "1", "/", "/", "/", "/", "/",
                            "3", "0", "1", "/", "/", "/", "R", "+0",
                            "R", "/", "/", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                        };
                        string[] bway = {
                            "3", "/", "1", "/", "/", "/", "/", "/",
                            "3", "2", "1", "/", "/", "/", "/", "+0",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "R", "/",
                        };
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, i == 9 ? 1 : 0);
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, i == 9 ? 1 : 0);
                            curTime += 3.47f;
                        }
                    }
                    if (_time == 126)
                    {
                        BlackScreen(4);
                        ScreenDrawing.ScreenAngle = -32;
                    }
                }
                public static void Area1B(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - (int)del);
                    if (_time < 128 && _time >= 0)
                        InstantTP(FightBox.instance.Centre);
                    if (_time == 0)
                    {
                        TP();
                    }
                    if (_time == -40)
                    {
                        float curTime = 40;
                        string[] rway = {
                            "3", "/", "1", "/", "/", "/", "/", "/",
                            "3", "0", "1", "/", "/", "/", "R", "+0",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "R", "/",
                        };
                        string[] bway = {
                            "3", "/", "1", "/", "/", "/", "/", "/",
                            "3", "2", "1", "/", "/", "/", "/", "+0",
                            "R", "/", "/", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                        };
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, i == 9 ? 1 : 0);
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, i == 9 ? 1 : 0);
                            curTime += 3.47f;
                        }
                    }
                    if (_time == 126)
                    {
                        BlackScreen(4);
                        ScreenDrawing.ScreenAngle = 180 + 32;
                    }
                }
                public static void Area1C(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - (int)del);
                    if (_time < 128 && _time >= 0)
                        InstantTP(FightBox.instance.Centre);
                    if (_time == 0)
                    {
                        TP();
                    }
                    if (_time == -40)
                    {
                        float curTime = 40;
                        string[] rway = {
                            "3", "/", "1", "/", "/", "/", "/", "/",
                            "3", "0", "1", "/", "/", "/", "R", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                        string[] bway = {
                            "3", "/", "1", "/", "/", "/", "/", "/",
                            "3", "2", "1", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, i == 9 ? 1 : 0);
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, i == 9 ? 1 : 0);
                            curTime += 3.47f;
                        }
                    }
                    if (_time == 126)
                    {
                        BlackScreen(4);
                        ScreenDrawing.ScreenAngle = 180 - 32;
                    }
                }
                public static void Area1D(float del)
                {

                    int _time = (int)(GametimeF - (int)del);
                    if (_time < 128 && _time >= 0)
                        InstantTP(FightBox.instance.Centre);
                    if (_time == 0)
                    {
                        TP();
                    }
                    if (_time == -40)
                    {
                        float curTime = 40;
                        string[] rway = {
                            "3", "/", "1", "/", "/", "/", "/", "/",
                            "3", "0", "1", "/", "/", "/", "R", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                        string[] bway = {
                            "3", "/", "1", "/", "/", "/", "/", "/",
                            "3", "2", "1", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                        };
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, i == 9 ? 1 : 0);
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, i == 9 ? 1 : 0);
                            curTime += 3.47f;
                        }
                    }
                    if (_time == 102)
                    {
                        BlackScreen(4);
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenScale = 1.3f;
                    }
                }

                public static void Breaking1(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - del);
                    if (_time >= 0 && _time < 128)
                    {
                        ScreenScale = ScreenScale * 0.96f + 1.0f * 0.04f;
                    }
                    if (_time == -40)
                    {
                        float curTime = 40 + 132;
                        for (int i = 0; i <= 1; i++)
                        {
                            Arrow arrow = MakeArrow(curTime, i * 2, 3, i, 0);
                            int v = 1 - i;
                            CreateEntity(arrow);
                            AddInstance(new InstantEvent(168, () => arrow.Delay(24.45f + 32.2f - 4f)));
                            AddInstance(new InstantEvent(168 + 24.45f, () => arrow.Delay(24.45f + 8f)));
                            AddInstance(new InstantEvent(168 + 24.45f * 2, () => arrow.Delay(24.45f)));
                            AddInstance(new InstantEvent(168 + 24.45f * 2, () => arrow.Delay(24.45f - 8f)));
                            AddInstance(new InstantEvent(168 + 115, () => arrow.ResetColor(v)));
                        }
                    }
                }

                public static void Area2A(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - del);
                    if (_time >= 0 && _time < 128)
                    {
                        ScreenScale = ScreenScale * 0.96f + 1.0f * 0.04f;
                    }
                    if (_time == -40)
                    {
                        float curTime = 40 + 128;

                        string[] rway = {
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                        string[] bway = {
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                            curTime += 4.06f;
                        }
                    }
                }
                public static void Area2B(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - del);
                    if (_time >= 0 && _time < 128)
                    {
                        ScreenScale = ScreenScale * 0.96f + 1.0f * 0.04f;
                    }
                    if (_time == -40)
                    {
                        float curTime = 40 + 128;

                        string[] rway = {
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "+0", "R", "/", "R", "/",
                            "R", "+0", "+2", "+0", "R", "/", "/", "/",
                        };
                        string[] bway = {
                            "R", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (bway[i] != "/") CreateGB(new GreenSoulGB(curTime, i % 4, 0, 5));
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                            curTime += 4.06f;
                        }
                    }
                }
                public static void Area2C(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - del);
                    if (_time >= 0 && _time < 128)
                    {
                        ScreenScale = ScreenScale * 0.96f + 1.0f * 0.04f;
                    }
                    if (_time == -40)
                    {
                        float curTime = 40 + 128;

                        string[] bway = {
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                        };
                        string[] rway = {
                            "R", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateGB(new GreenSoulGB(curTime, i % 4, 1, 5));
                            if (bway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 0, 0);
                            curTime += 4.06f;
                        }
                    }
                }
                public static void Area2D(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - del);
                    if (_time >= 0 && _time < 128)
                    {
                        ScreenScale = ScreenScale * 0.96f + 1.0f * 0.04f;
                    }
                    if (_time == -40)
                    {
                        float curTime = 40 + 128;

                        string[] bway = {
                            "R", "/", "/", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                        string[] rway = {
                            "R", "/", "/", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (i < 16)
                            {
                                if (rway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 1, 0);
                                if (bway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 0, 0);
                            }
                            else
                            {
                                if (rway[i] != "/") CreateGB(new GreenSoulGB(curTime, Rand(0, 3), 1, 11));
                                if (bway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 0, 0);
                            }
                            curTime += 4.06f;
                        }
                    }
                }

                public static void Breaking2(float del)
                {
                    del = (int)del;
                    int _time = (int)(GametimeF - del);
                    if (_time == 1)
                    {
                        SetSoul(0);
                        SetBox(320 - 45, 320 + 45, 240 - 42, 290 + 42);
                    }
                    if (_time == -1)
                    {
                        CreateGB(new NormalGB(new(150, 240 - 20), new(0, 0), new(1, 0.76f), 0, 49, 4));
                    }
                    if (_time == 7)
                    {
                        CreateGB(new NormalGB(new(150, 240 + 15), new(0, 0), new(1, 0.76f), 0, 41, 4));
                    }
                    if (_time == 15)
                    {
                        CreateGB(new NormalGB(new(150, 240 + 50), new(0, 480), new(1, 0.76f), 0, 25, 4));
                    }
                    if (_time == 23)
                    {
                        CreateGB(new NormalGB(new(150, 240 + 85), new(0, 480), new(1, 0.76f), 0, 17, 4));
                    }
                    if (_time >= 0 && _time < 128)
                    {
                        ScreenScale = ScreenScale * 0.96f + 1.0f * 0.04f;
                    }
                }
            }

            public void ExtremePlus()
            {
                Line[] ls = GetAll<Line>();
                for (int i = 0; i < ls.Length; i++)
                {
                    int x = i;
                    ls[x].DrawingColor = linecolor;
                }
                if (colorwhitebool)
                {
                    linecolor = Color.White;
                }
                if (!colorwhitebool && colorbluebool)
                {
                    linecolor = new(168, 231, 255);
                }
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddNiceEvent(() =>
                {
                    colorbluebool = true;
                });
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddOkayEvent(() =>
                {
                    colorbluebool = true;
                });
                UndyneFight_Ex.Fight.AdvanceFunctions.Interactive.AddMissEvent(() =>
                {
                    colorwhitebool = true;
                });
                if (InBeat(-1)) part1();
                if (InBeat(4 * 4 * 5)) part2();
                if (InBeat(4 * 4 * 8 - 4)) part3();
                if (InBeat(4 * 4 * 13 - 4)) part4();
                if (InBeat(4 * 4 * 19 - 4)) part5();
                if (InBeat(4 * 4 * 22 - 8)) part6();
                if (InBeat(4 * 4 * 25 + 4)) part7();
            }
            public void LineShadow(int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * 0.5f, 0.24f - 0.24f / times * t));

                }
            }
            public void LineShadow(float deep, int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * 0.5f, deep - deep / times * t));

                }
            }
            public void LineShadow(float delay, float deep, int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * delay, deep - deep / times * t));

                }
            }
            void part1()
            {
                UISettings.CreateUISurface();
                RegisterFunctionOnce("Line1", () =>
                {
                    Line l = new(CentreEasing.Stable(420, 240), ValueEasing.Stable(70));
                    Line l1 = new(CentreEasing.Stable(470, 240), ValueEasing.Stable(70));
                    CreateEntity(l);
                    CreateEntity(l1);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 0.1f, BeatTime(0.5f)));
                    ve.Insert(1, ValueEasing.Linear(0.1f, 1f, 1));
                    ve.Insert(BeatTime(2f), ValueEasing.Linear(1, 0, BeatTime(1.5f)));
                    ve.Run((s) => { l1.Alpha = s; l.Alpha = s; });
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    Line l = new(CentreEasing.Stable(320 - 100, 240), ValueEasing.Stable(110));
                    Line l1 = new(CentreEasing.Stable(320 - 50, 240), ValueEasing.Stable(110));
                    CreateEntity(l);
                    CreateEntity(l1);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 0.1f, BeatTime(0.5f)));
                    ve.Insert(1, ValueEasing.Linear(0.1f, 1f, 1));
                    ve.Insert(BeatTime(2f), ValueEasing.Linear(1, 0, BeatTime(1.5f)));
                    ve.Run((s) => { l1.Alpha = s; l.Alpha = s; });
                });
                RegisterFunctionOnce("StartLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 240));
                    ce.Insert(BeatTime(16), CentreEasing.EaseInSine(new(0, 240), new(290, 300), BeatTime(8)));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(2), ValueEasing.Stable(90));
                    ve.Insert(BeatTime(6), ValueEasing.EaseInQuad(90, 90 - 71, BeatTime(6)));
                    Line l = new(ce.GetResult(), ve.GetResult());
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(6)); });
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    LineShadow(5, 0.24f, 5, l);
                });
                RegisterFunctionOnce("LeftC", () =>
                 {
                     Convulse(4, BeatTime(1.5f), false);

                 });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(4, BeatTime(1.5f), true);

                });
                RegisterFunctionOnce("LeftC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);

                });
                RegisterFunctionOnce("RightC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), true);

                });
                RegisterFunctionOnce("LeftC3", () =>
                {
                    Convulse(6, BeatTime(0.9f), false);

                });
                RegisterFunctionOnce("RightC3", () =>
                {
                    Convulse(6, BeatTime(0.9f), true);

                });
                RegisterFunctionOnce("SizeExpend", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1, 1.2f, BeatTime(1)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 6, BeatTime(0.25f)));
                    for (int a = 0; a < 11; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(6, -6, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-6, 6, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(6, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Return", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1.2f));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1.2f, 1f, BeatTime(1f)));
                    ve.Run((s) => { ScreenScale = s; });
                    DelayBeat(1, () => { ScreenScale = 1; });
                });
                RegisterFunctionOnce("CameFromLeftLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(3.5f), CentreEasing.FromLeft(320, BeatTime(3.5f)));
                    ce.Insert(BeatTime(0.5f), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));

                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                        ce2.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                        Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                        l2.AlphaDecrease(BeatTime(2));
                        CreateEntity(l2);
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("CameFromRightLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(3.5f), CentreEasing.FromRight(320, BeatTime(3.5f)));
                    ce.Insert(BeatTime(0.5f), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                        ce2.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                        Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                        l2.AlphaDecrease(BeatTime(2));
                        CreateEntity(l2);
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });

                    CreateEntity(l);
                });
                RegisterFunctionOnce("CameFromDoubleLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4f), CentreEasing.EaseOutQuart(new(640, 0), new(320, 0), BeatTime(4f)));

                    ce.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 0));
                    ce2.Insert(BeatTime(4f), CentreEasing.EaseOutQuart(new(0, 0), new(320, 0), BeatTime(4f)));

                    ce2.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    CreateEntity(l2);
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));
                        l2.AlphaDecrease(BeatTime(2));
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("SmallShake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 1, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(1, -1, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-1, 1, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(1, 0, BeatTime(0.25f)));
                    DelayBeat(8.1f, () =>
                    {

                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("DoubleLine", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 0));
                    ce1.Insert(BeatTime(1.3f), CentreEasing.FromRight(640 - 320 + 46, BeatTime(1.3f)));
                    ce1.Insert(BeatTime(0.7f), CentreEasing.Stable(640 - 320 - 46, BeatTime(0.7f)));
                    ce1.Insert(BeatTime(1), CentreEasing.FromRight(430, BeatTime(1)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(640 + 87, 0));
                    ce2.Insert(BeatTime(1.3f), CentreEasing.FromRight(640 - 320 + 40, BeatTime(1.3f)));
                    ce2.Insert(BeatTime(0.7f), CentreEasing.Stable(640 - 320 - 40, BeatTime(0.7f)));
                    ce2.Insert(BeatTime(1), CentreEasing.FromRight(430, BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(4, () => { l1.Dispose(); l2.Dispose(); });
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(8), ValueEasing.EaseInQuart(0, 360 * 2, BeatTime(8)));
                    ve.Run((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                        Heart.InstantSetRotation(s);
                    });
                    DelayBeat(8.01f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        Heart.InstantSetRotation(0);
                    });
                });
                RegisterFunctionOnce("Change1", () =>
                {
                    UISettings.RemoveUISurface();
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(20, BeatTime(0.5f));
                });
                CreateChart(0, BeatTime(1), 6.7f, new string[]
                {
                    "StartLine","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "$3'0.75(RightC)","","","",   "","","","",
                    "($0'0.9)($21'0.9)(LeftC)","","","",   "","","","",
                    "","","","",   "(^$0'1.5)(^$01'1.5)(^$2'1.5)(^$21'1.5)(RightC)","","","",
                    "","","","",   "","","","",

                    "(#6.9#$2)(#6.9#$01)(SizeExpend)","","","",   "","","","",
                    "$3(Shake)","","$31","",   "$0","","$01","",
                    "$1","","$11","",   "$2","","$21","",
                    "$3","","$31","",   "$0","","$01","",

                    "$3","","$31","",   "$2","","$21","",
                    "$1","","$11","",   "$0","","$01","",
                    "$3","","$31","",   "$3","","$31","",
                    "(^$0'1.5)(^$01'1.5)(^$2'1.5)(^$21'1.5)","(Return)","","", "","","","",
                    //
                    "","","","",   "","","","",
                    "(R01)(+01)(LeftC)(CameFromLeftLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+1'1.2","","+1'1.2","",

                    "+1'1.2","","","",   "","","","",
                    "(R01)(+01)(RightC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",

                    "R","","","",   "","","","",
                    "(R01)(+01)(RightC)(CameFromRightLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+1'1.2","","+1'1.2","",

                    "+1'1.2","","","",   "R","","","",
                    "(R01)(+01)(LeftC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",
                    //
                    "(R)(R1)(RightC2)","","","",   "(R)(R1)(LeftC2)","","","",
                    "R(CameFromDoubleLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+1'1.2","","+1'1.2","",

                    "+1'1.2","","","",   "","","","",
                    "($3)(+11)(-21)(RightC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",

                    "R","","","",   "","","","",
                    "(R)(R1)(LeftC2)(Line2)","","","",   "(R)(R1)(RightC2)","","","",
                    "","","","",   "","","","",
                    "R","","","",   "R","","^+11'1.5","",

                    "R","","","",   "R1","","","",
                    "(R)(R1)(RightC)(DoubleLine)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(R1)(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "(R)(R1)(LeftC3)","","","",   "","","","",
                    "(R)(R1)(LeftC3)","","","",   "","","","",
                    "!!3/6","(R1)(R'1.2)(LeftC3)","","+0'1.2","","+0'1.2","",
                    "(+0'1.2)(R1)(LeftC3)","","","",   "R","","","",

                    "(R)(R1)(LeftC3)","","","",   "","","","",
                    "(R1)(LeftC3)","","","",   "R","","","",
                    "(R)(R1)(LeftC3)","","","",   "","","","",
                    "R(R1)(LeftC3)","","","",   "","","","",

                    "(R)(R1)(RightC3)(Line1)","","","",   "(R)(R1)","","","",
                    "(R1)(RightC3)","","","",   "","","","",
                    "!!3/6","(R1)(R'1.2)(RightC3)","","+0'1.2","","+0'1.2","",
                    "(+0'1.2)(R1)(RightC3)","","","",   "R","","","",

                    "(R)(R1)(RightC3)","","","",   "","","","",
                    "(R1)(RightC3)","","","",   "(R)(R1)","","","",
                    "(R1)(RightC3)","","","",   "","","","",
                    "(R)(R1)(RightC3)","","","",   "(R)(R1)","","","",
                    //
                    "$2'1.2(Rotate)","","$1'1.2","",   "$0'1.2","","$21'1.2","",
                    "$11'1.2","","$01'1.2","",   "$0'1.2","","$1'1.2","",
                    "$2'1.2","","$3'1.2","",   "$21'1.2","","$11'1.2","",
                    "$01'1.2","","$21'1.2","",   "$11'1.2","","$0'1.2","",

                    "$1'1.2","","$0'1.2","",   "$11'1.2","","$21'1.2","",
                    "$1'1.2","","$0'1.2","",   "$11'1.2","","$21'1.2","",
                    "($0'1.4)($2'1.4)","","($11'1.4)($31'1.4)","",   "($0'1.4)($2'1.4)","","($11'1.4)($31'1.4)","",
                    "($0'1.4)($2'1.4)","","($11'1.4)($31'1.4)","",   "($0'1.4)($2'1.4)","","($11'1.4)($31'1.4)","",

                    "(^$0'1.2)(^$2'1.2)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(^$11'1.2)(^$31'1.2)","","","",   "","","","",
                    "","","","",   "","","","",

                    "R","","","",   "R","","","",
                    "R","","","",   "R","","","",
                    "R","","","",   "R","","","",
                    "","","","",   "Change1","","","",
                    //
                });
            }
            void part2()
            {
                RegisterFunctionOnce("Line", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("LineN", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(100));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    Line l1 = new(CentreEasing.Stable(LastRand + 50, 240), ValueEasing.Stable(100));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("LineM", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(80));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    Line l1 = new(CentreEasing.Stable(LastRand - 50, 240), ValueEasing.Stable(80));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(0, CentreEasing.Stable(-60, 240));
                    ve.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(-60, 240), new(700, 240), BeatTime(1)));
                    Line l = new(ve.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    LineShadow(1.5f, 0.75f, 5, l);
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(0, CentreEasing.Stable(700, 240));
                    ve.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(700, 240), new(-60, 240), BeatTime(1)));
                    Line l = new(ve.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    LineShadow(1.5f, 0.75f, 5, l);
                });
                RegisterFunctionOnce("Change1", () =>
                {
                    UISettings.RemoveUISurface();
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(-20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change3", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    Heart.GiveForce(180, 0);
                    RotateTo(180 + 20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change4", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(180 - 20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change5", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.02f), ValueEasing.Linear(0, 4, BeatTime(0.02f)));
                    ve.Insert(BeatTime(8f), ValueEasing.EaseOutSine(4, 1, BeatTime(8)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(0, BeatTime(0.5f));
                    Heart.GiveForce(0, 0);
                });
                RegisterFunctionOnce("NeverGonnaGiveYouUp", () =>
                {
                    Arrow b = MakeArrow(BeatTime(8), 0, 3, 0, 0);
                    Arrow r = MakeArrow(BeatTime(8), 2, 3, 1, 0);
                    CreateEntity(b);
                    CreateEntity(r);
                    DelayBeat(3.8f + 4, () =>
                      {
                          b.Delay(BeatTime(1.7f));
                          r.Delay(BeatTime(1.7f));
                      });
                    DelayBeat(5.3f + 4, () =>
                      {
                          b.Delay(BeatTime(1.7f));
                          r.Delay(BeatTime(1.7f));
                      });
                    DelayBeat(6.8f + 4, () =>
                      {
                          b.Delay(BeatTime(1.7f));
                          r.Delay(BeatTime(1.7f));
                      });
                    DelayBeat(8.3f + 4, () =>
                    {
                        b.Delay(BeatTime(1.2f));
                        r.Delay(BeatTime(1.2f));
                    });
                    DelayBeat(9.3f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(10.8f + 4, () =>
                    {
                        b.ResetColor(1);
                        r.ResetColor(0);
                    });
                });
                RegisterFunctionOnce("ShineLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(16), CentreEasing.Alternate
                        (
                       BeatTime(0.5f),
                        new Func<ICustomMotion, Vector2>[] {

                            CentreEasing.Stable(640 / 5 * 1, 240),
                            CentreEasing.Stable(640 / 5 * 2, 240),
                            CentreEasing.Stable(640 / 5 * 3, 240),
                            CentreEasing.Stable(640 / 5 * 4, 240),
                        }
                        ));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.5f };
                    CreateEntity(l);
                    DelayBeat(10, () => { l.AlphaDecrease(BeatTime(4.5f)); });
                });
                RegisterFunctionOnce("RickLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 240));
                    ce.Insert(BeatTime(8), CentreEasing.FromLeft(278, BeatTime(8)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(278, 240), new(220, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(220, 240), new(150, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(150, 240), new(70, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseInQuart(new(70, 240), new(70 + 150, 240), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutQuart(new(70 + 150, 240), new(370, 240), BeatTime(0.75f)));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(8), ValueEasing.Stable(90));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(90, 105, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(105, 80, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(80, 90, BeatTime(1.5f)));
                    Line l = new(ce.GetResult(), ve.GetResult());
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    DelayBeat(12, () => { l.AlphaDecrease(BeatTime(2)); });
                    LineShadow(4f, 0.5f, 5, l);
                });
                CreateChart(BeatTime(3), BeatTime(1), 6.5f, new string[]
                {
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "$2","","$3","",
                    "$0","","","",    "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "R","","","",   "(Change2)","","","",

                    "","","","",   "","","","",
                    "","","","",   "$2","","$3","",
                    "$0","","","",    "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "R","","","",   "(Change3)","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "$2","","$3","",
                    "$0","","","",    "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "R","","","",   "(Change4)","","","",

                    "","","","",   "","","","",
                    "","","","",   "$2","","$3","",
                    "$0","","","",    "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "(D)(+01)","","","",
                    "(D)(+01)","","","",   "(Change5)","","","",
                    //
                    "(NeverGonnaGiveYouUp)(ShineLine)(RickLine)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    
                });
                CreateChart(BeatTime(3), BeatTime(1), 6.5f, new string[]
                {
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "R1(Line)","","","",    "","","","",
                    "","","","",   "R1(Line)","","","",
                    "","","","",   "R1(Line)","","","",
                    "","","","",   "","","","",
                    "^R1'1.35(Line2)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "R1(LineN)","","","",    "","","","",
                    "","","","",   "R1(LineN)","","","",
                    "","","","",   "R1(LineN)","","","",
                    "","","","",   "","","","",
                    "^R1'1.35(Line3)","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "R1(LineM)","","","",    "","","","",
                    "","","","",   "R1(LineM)","","","",
                    "","","","",   "R1(LineM)","","","",
                    "","","","",   "","","","",
                    "^R1'1.35(Line3)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "R1(LineN)","","","",    "","","","",
                    "","","","",   "R1(LineN)","","","",
                    "","","","",   "(LineN)","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void part3()
            {
                Player.Heart main = Heart;
                Player.Heart extra;
                extra = Heart.InstantSplit(new CollideRect(-100, 240, 84, 84));
                RegisterFunctionOnce("MoveRight", () =>
                {

                    SetPlayerBoxMission(extra);
                    SetSoul(1);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(-100, 240));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(-100, 240), new(200, 240), BeatTime(2f)));
                    ce.Insert(BeatTime(0.66f), CentreEasing.Stable(200, 240));
                    ce.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(200, 240), new(-100, 240), BeatTime(1.33f)));
                    ce.Run((s) => { SetPlayerBoxMission(extra); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(320, 240));
                    ce2.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(420, 240), BeatTime(2f)));
                    ce2.Insert(BeatTime(0.66f), CentreEasing.Stable(420, 240));
                    ce2.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(420, 240), new(320, 240), BeatTime(1.33f)));
                    ce2.Run((s) => { SetPlayerBoxMission(main); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);
                });
                RegisterFunctionOnce("MoveLeft", () =>
                {
                    SetPlayerBoxMission(extra);
                    SetSoul(1);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(740, 240));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(740, 240), new(440, 240), BeatTime(2f)));
                    ce.Insert(BeatTime(0.66f), CentreEasing.Stable(440, 240));
                    ce.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(440, 240), new(740, 240), BeatTime(1.33f)));
                    ce.Run((s) => { SetPlayerBoxMission(extra); InstantSetBox(s, 84, 84); InstantTP(s); });
                    DelayBeat(16, () => { SetPlayerBoxMission(extra); InstantSetBox(4000, 84, 84); InstantTP(4000, -11451); SetPlayerBoxMission(main); });
                    SetPlayerBoxMission(main);
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(320, 240));
                    ce2.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(220, 240), BeatTime(2f)));
                    ce2.Insert(BeatTime(0.66f), CentreEasing.Stable(220, 240));
                    ce2.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(220, 240), new(320, 240), BeatTime(1.33f)));
                    ce2.Run((s) => { SetPlayerBoxMission(main); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);

                });
                RegisterFunctionOnce("NeverGonnaGiveYouUp", () =>
                {
                    Arrow b = MakeArrow(BeatTime(4), 2, 6.5f * 1.7f, 0, 0);
                    Arrow r = MakeArrow(BeatTime(4), 0, 6.5f * 1.7f, 1, 0);
                    CreateEntity(b);
                    CreateEntity(r);
                    DelayBeat(3.8f, () =>
                    {
                        b.ResetColor(1);
                        r.ResetColor(0);

                    });
                });
                RegisterFunctionOnce("SizeExpend", () =>
                {
                    SizeExpand(8, BeatTime(2f));
                });
                RegisterFunctionOnce("StartLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(360, 240));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(360, 240), new(-4, 240), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    DelayBeat(1, () => { l.Dispose(); });
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(25, BeatTime(2f), true);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(25, BeatTime(2f), false);

                });
                RegisterFunctionOnce("SLeftC", () =>
                {
                    Convulse(8, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("SRightC", () =>
                {
                    Convulse(8, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    RotateTo(180, BeatTime(2.5f));
                    SetPlayerBoxMission(main);
                    Heart.FollowScreen(BeatTime(2.5f));
                });
                RegisterFunctionOnce("DoubleLine", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(Rand(50, 540), 90 + 15 * m);
                    Line l2 = new(LastRand + 50, 90 + 15 * m);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("DoubleLine2", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(new Vector2(320, Rand(50, 380)), 15 * m);
                    Line l2 = new(new Vector2(320, LastRand + 50), 15 * m);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("Return", () =>
                {
                    RotateTo(0, BeatTime(4.5f));
                    Heart.FollowScreen(BeatTime(4.5f));
                });
                RegisterFunctionOnce("LeftLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(5), CentreEasing.XSinWave(320 + 46, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(5, () => { l.Dispose(); });
                    LineShadow(1, 0.3f, 7, l);
                });
                RegisterFunctionOnce("RightLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(5), CentreEasing.XSinWave(320 + 46, BeatTime(8), 1));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(5, () => { l.Dispose(); });
                    LineShadow(1, 0.3f, 7, l);
                });
                RegisterFunctionOnce("|Line1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.Stable(320 + 30, 480));
                    ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(-2, 0), new(-0.34f, 0f)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(8, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                });
                RegisterFunctionOnce("|Line2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, -38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.Stable(320 - 30, 480));
                    ce.Insert(BeatTime(3), CentreEasing.Accerlating(new(2, 0), new(0.34f, 0)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(7, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                });
                RegisterFunctionOnce("|Line3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, -38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320 - 30, 0));
                    ce.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(290, 0), new(290, 130), BeatTime(2)));
                    ce.Insert(BeatTime(4), CentreEasing.EaseInSine(new(290, 130), new(290, -300), BeatTime(4)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(7, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("XLine", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(BeatTime(4), CentreEasing.Stable(320, 240));
                    ce1.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 1), new(0, 0.24f)));
                    ValueEasing.EaseBuilder ve1 = new(), ve2 = new();
                    ve1.Insert(BeatTime(3), ValueEasing.Stable(45));
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutSine(45, 0, BeatTime(2)));
                    ve2.Insert(BeatTime(3), ValueEasing.Stable(90 + 45));
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutSine(90 + 45, 90, BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ve1.GetResult()) { Alpha = 0 };
                    Line l2 = new(ce1.GetResult(), ve2.GetResult()) { Alpha = 0 };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaIncrease(BeatTime(2), 0.75f);
                    l2.AlphaIncrease(BeatTime(2), 0.75f);
                    DelayBeat(6, () =>
                     {
                         l1.AlphaDecrease(BeatTime(1.5f));
                         l2.AlphaDecrease(BeatTime(1.5f));
                     });
                });
                SetPlayerBoxMission(main);
                UISettings.CreateUISurface();
                DelayBeat(40, () => { UISettings.RemoveUISurface(); });
                DelayBeat(84, () => { UISettings.CreateUISurface(); });
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
                    "(#7#$2)(#7#$01)(SizeExpend)(StartLine)","","","",   "","","","",
                    "","","","",   "($1)($31)(SizeExpend)","","","",
                    "","","","",   "","","","",
                    "($1)($31)(SizeExpend)","","","",   "","","","",

                    "","","","",   "($1)($31)(SizeExpend)","","","",
                    "","","","",   "","","","",
                    "($1)($31)(SizeExpend)","","","",   "","","","",
                    "($1)($2)($01)($31)","","","",   "","","","",

                    "(#0.8#$21)(N2)(LeftC)(DoubleLine2)","","","",   "(N2)","","","",
                    "(N21)","","","",   "(#0.8#$21)(N2)(LeftC)(DoubleLine2)","","","",
                    "(N2)","","","",   "(N21)","","","",
                    "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","","",   "(N2)","","","",

                    "N2","+001","+001","+001",   "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","N2","",
                    "N21","","","",   "N2","","","",
                    "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","(+0)","",   "(+0)","","(+0)","",
                    "(D)","","","",   "","","","",
                    //
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",
                    "(N01)","","","",   "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",
                    "(N01)","","","",   "(N01)","","","",
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",

                    "N01","","","",   "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",
                    "N01","","","",   "N01","","","",
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",
                    "(R)(D1)","","","",   "","","","",

                    "(#0.8#R)(D1)(LeftC)","","","",   "","","","",
                    "","","","",   "D1","","","",
                    "(#0.8#R1)(D)(RightC)","","","",   "","","","",
                    "","","","",   "D1","","","",

                    "(#0.8#R)(#0.8#R1)(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(D)(D1)(SRightC)(|Line1)","","","",   "","","","",
                    "(D)(D1)(SLeftC)(|Line2)","","","",   "","","","",
                    //
                    "N0","(MoveRight)(LeftLine)","","",   "N01","","","",
                    "N0","","","",   "N01","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "N2","(MoveLeft)(RightLine)","","",   "N21","","","",
                    "N2","","","",   "N21","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "N0(|Line3)","","","",   "N0","","","",
                    "N0","","","",   "N0","","","",
                    "N0","","","",   "R","","","",
                    "R(Rotate)","","","",   "","","","",

                    "R(NeverGonnaGiveYouUp)","","","",   "","","","",
                    "","","","",   "","","","",
                    "($0'1.7)($2'1.7)(SLeftC)","","","",   "","","","",
                    "($01'1.7)($21'1.7)(SRightC)","","","",   "","","","",
                    //
                    "(#7#$0)(#7#$21)(SizeExpend)(StartLine)","","","",   "","","","",
                    "","","","",   "($11)($3)(SizeExpend)","","","",
                    "","","","",   "","","","",
                    "($11)($3)(SizeExpend)","","","",   "","","","",

                    "","","","",   "($11)($3)(SizeExpend)","","","",
                    "","","","",   "","","","",
                    "($11)($3)(SizeExpend)","","","",   "","","","",
                    "($21)($11)($0)($3)","","","",   "","","","",

                    "(#0.8#$21)(N2)(LeftC)(DoubleLine2)","","","",   "(N2)","","","",
                    "(N21)","","","",   "(#0.8#$21)(N2)(LeftC)(DoubleLine2)","","","",
                    "(N2)","","","",   "(N21)","","","",
                    "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","","",   "(N2)","","","",

                    "R","+001","+001","+001",   "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","N2","",
                    "N21","","","",   "N2","","","",
                    "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","(+0)","",   "(+0)","","(+0)","",
                    "(D)","","","",   "","","","",
                    //
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",
                    "(N01)","","","",   "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",
                    "(N01)","","","",   "(N01)","","","",
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",

                    "N01","","","",   "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",
                    "N01","","","",   "N01","","","",
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",
                    "(R)(D1)","","","",   "","","","",

                    "(R)(R1)","","","",   "","","","",
                    "(R)","","","",   "(R)(R1)(Return)(XLine)","","","",
                    "(R)","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "R","","","",   "","","","",
                    "R","","","",   "","","","",
                    "R","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
                SetPlayerBoxMission(extra);
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "$3","","","",
                    "$11","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "$3","","","",
                    "$11","","","",   "$3","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void part4()
            {
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(BoxStates.Right, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromRight(200, BeatTime(2f)));

                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(2f));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(BoxStates.Left, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromLeft(200, BeatTime(2f)));

                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(2f));
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(25, BeatTime(2f), true);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(25, BeatTime(2f), false);

                });
                RegisterFunctionOnce("SLeftC", () =>
                {
                    Convulse(6, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("SRightC", () =>
                {
                    Convulse(6, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 3, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(3, -3, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-3, 3, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(3, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Flicker", () => { MakeFlicker(Color.White * 0.4f); });
                RegisterFunctionOnce("BFlicker", () => { MakeFlicker(Color.White); });
                RegisterFunctionOnce("ScreenL", () =>
                {
                    RotateWithBack(BeatTime(4), 4);
                });
                RegisterFunctionOnce("ScreenR", () =>
                {
                    RotateWithBack(BeatTime(4), -4);
                });
                RegisterFunctionOnce("ScreenL2", () =>
                {
                    RotateWithBack(BeatTime(8), 6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 1));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenR2", () =>
                {
                    RotateWithBack(BeatTime(8), -6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 1));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenH", () =>
                {
                    RotateSymmetricBack(BeatTime(8), 6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 1));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 0));
                    ce2.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 0));
                    ce2.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 1));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l2);
                    DelayBeat(4, () => { l2.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenExpand", () =>
                {
                    SizeExpand(8, BeatTime(4));
                });
                RegisterFunctionOnce("SuddenRotateL", () =>
                {
                    Rotate(6, BeatTime(0.125f));
                    DelayBeat(0.125f, () => { Rotate(-6, BeatTime(1.375f)); });
                });
                RegisterFunctionOnce("SuddenRotateR", () =>
                {
                    Rotate(-6, BeatTime(0.125f));
                    DelayBeat(0.125f, () => { Rotate(6, BeatTime(1.375f)); });
                });
                RegisterFunctionOnce("Over", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    LerpScreenScale(BeatTime(2), 1.2f, 0.06f);
                    DelayBeat(4, () => { LerpScreenScale(BeatTime(4), 1, 0.1f); });
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    RotateTo(30, BeatTime(4));
                });
                RegisterFunction("Heal", () =>
                {
                    Regenerate(60);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
#region part
                    "R","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(R)(^$21'1.8)(Line1)(ScreenL)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(R)(^$01'1.8)(Line2)(ScreenR)","","","",   "","","","",
                    "","","","",   "(R)","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(D)(^$21'1.8)(^$01'1.8)(Line1)(Line2)(ScreenExpand)","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D1","","","",   "D1","","","",
                    "D1","","","",   "D1","","","",
                    //
                    "($11)($21)(SLeftC)","","","",   "($11)($21)(D'1.2)(SLeftC)","","","",
                    "","","","",   "($0)($1)(SRightC)","","","",
                    "($0)($1)(D1'1.2)(SRightC)","","","",   "","","","",
                    "","","","",   "D","","","",

                    "($0'1.2)($1'1.2)($2'1.2)","","","",   "(R)(D1)","","","",
                    "!!3/6","$21","$0","$21","$0","","",
                    "(R)(D1)(SLeftC)","","","",   "($0'1.2)($21'1.2)(SLeftC)","","","",
                    "($0'1.2)($21'1.2)(SRightC)","","","",   "(R)(D1)(SRightC)","","","",

                    "R'1.2(Shake)(Flicker)","","+21'1.2(Flicker)","",   "R'1.2(Flicker)","","+21'1.2(Flicker)","",
                    "R'1.2(Flicker)","","+21'1.2(Flicker)","",   "R'1.2(Flicker)","","+21'1.2(Flicker)","",
                    "R'1.2(Flicker)","","+21'1.2(Flicker)","",   "R'1.2(Flicker)","","+21'1.2(Flicker)","",
                    "R'1.2(Flicker)","","+21'1.2(Flicker)","",   "R'1.2(Flicker)","","+21'1.2(Flicker)","",

                    "($3'1.5)($31'1.5)(BFlicker)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    #endregion
#region part
                    "(#4#$1)(D1)(ScreenL2)","","","",   "","","","",
                    "(D1)","","","",   "","","","",
                    "!!3/6","N1","","+01","","N1","",
                    "+01","","","",   "","","","",

                    "(^$1'1.3)(^D1'1.3)","","","",   "","","","",
                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "(#4#$21)(D)(ScreenR2)","","","",   "","","","",
                    "(D)","","","",   "","","","",
                    "!!3/6","N21","","+0","","N11","",
                    "+0","","","",   "","","","",

                    "(^$21'1.3)(^D'1.3)","","","",   "","","","",
                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "(R)(R1)","","","",
                    //
                    "(#4#$0)(D1)(ScreenH)","","","",   "","","","",
                    "(D1)","","","",   "","","","",
                    "!!3/6","N0","","+01","","N1","",
                    "+01","","","",   "","","","",

                    "(^$0'1.3)(^D1'1.3)","","","",   "","","","",
                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "(#4#$31)(D)","","","",   "(D)","","","",
                    "","","","",   "","","","",
                    "(^N31'1.15)","","","",   "","","","",
                    "(^N31'1.15)","","","",   "","","","",

                    "(^$31'1.3)(^D'1.3)(SuddenRotateL)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    #endregion
#region part
                    "(R)(R1)","","","",   "","","","",
                    "(R)","","","",   "","","","",
                    "!!6/3","(R1)($0'1.5)(SuddenRotateR)","","($1'1.5)","(R1)","",
                    "($3'1.5)","","","",   "(R)","","",

                    "(R)(R1)","","","",   "","","","",
                    "R","","","",   "(R1)","","","",
                    "(R)","","","",   "(R)(R1)","","","",
                    "","","","",   "(R)(R1)(SuddenRotateL)","","","",

                    "","","","",   "","","","",
                    "","","","",   "(R)(D1)","","","",
                    "","","","",   "!!3/6","(R1'1.4)(+2'1.4)(SuddenRotateR)","","(+21'1.5)(+2'1.5)",
                    "","(+21'1.6)(+2'1.6)","",  "(+21'1.6)(+2'1.6)","","","","",
                    "","","","",   "(+21'1.6)(+2'1.6)","","","",
                    "","","","",   "R","","","",
                    "(R)(D1)","","","",   "","","","",
                    "","","","",   "(R)(D1)(SuddenRotateL)","","","",
                    "(R)(D1)","","","",   "(R)(+01)","","","",
                    //
                    "R","","","",   "(R)(+01)","","","",
                    "R","","","",   "(R)(+01)","","","",
                    "R","","","",   "(R)(+01)","","","",
                    "R","","","",   "$0'1.2(Shake)(Flicker)","","$2'1.2(Flicker)","",

                    "$1'1.2(Flicker)","","$31'1.2(Flicker)","",   "$2'1.2(Flicker)","","$01'1.2(Flicker)","",
                    "$3'1.2(Flicker)","","$11'1.2(Flicker)","",
                    "($0'1.35)($2'1.35)(Flicker)","","($01'1.35)($21'1.35)(Flicker)","",   "($0'1.35)($2'1.35)(Flicker)","","($01'1.35)($21'1.35)(Flicker)","",
                    "($0'1.35)($2'1.35)(Flicker)","","($01'1.35)($21'1.35)(Flicker)","",   "($0'1.35)($2'1.35)(Flicker)","","($01'1.35)($21'1.35)(Flicker)","",

                    "($0'1.35)($2'1.35)(BFlicker)(Over)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "Rotate","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
#endregion
                });
            }
            void part5()
            {


                RegisterFunctionOnce("DoubleLine", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(Rand(50, 540), 90 + 15 * m) { Alpha = 0.6f };
                    Line l2 = new(LastRand + 50, 90 + 15 * m) { Alpha = 0.6f };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    RotateTo(-6, BeatTime(2));

                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    RotateTo(6, BeatTime(2));

                });
                RegisterFunctionOnce("SRotateL", () =>
                {
                    RotateTo(-2, BeatTime(1));


                });
                RegisterFunctionOnce("SRotateR", () =>
                {
                    RotateTo(2, BeatTime(1));

                });
                RegisterFunctionOnce("BRotateL", () =>
                {
                    RotateTo(-6, BeatTime(1));


                });
                RegisterFunctionOnce("BRotateR", () =>
                {
                    RotateTo(6, BeatTime(1));

                });
                RegisterFunctionOnce("Return", () =>
                {
                    RotateTo(0, BeatTime(1));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(1, 1.3f, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    DelayBeat(1.501f, () => { ScreenScale = 1.15f; });
                    DelayBeat(2.501f, () => { ScreenScale = 1f; });
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Line", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 480));
                    ce.Insert(BeatTime(7), CentreEasing.Accerlating(new(0, -80), new(0, -0.1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.75f };
                    CreateEntity(l);
                    LineShadow(4, 0.75f, 4, l);
                });
                RegisterFunctionOnce("UpLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 600));
                    ce.Insert(BeatTime(7), CentreEasing.Accerlating(new(0, -100), new(0, -0.1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(40)) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("Camera1", () =>
                {
                    ScreenDrawing.ScreenAngle = 30;
                    ScreenScale = 0.9f;
                    ScreenPositionDelta = new(10, 20);
                });
                RegisterFunctionOnce("Camera2", () =>
                {
                    ScreenDrawing.ScreenAngle = -24;
                    ScreenScale = 0.8f;
                    ScreenPositionDelta = new(10, -20);
                });
                RegisterFunctionOnce("Camera3", () =>
                {
                    //ScreenDrawing.ScreenAngle = 0;
                    //ScreenScale = 2.5f;
                    DelayBeat(0, () => { ScreenPositionDelta = Vector2.Zero; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(1, ValueEasing.Stable(2.5f));
                    ve.Insert(BeatTime(2.5f), ValueEasing.EaseInQuad(2.5f, 1f, BeatTime(2.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    Heart.InstantSetRotation(0);
                });
                CreateChart(BeatTime(4), BeatTime(1), 7.3f, new string[]
                {
                    "(R)(+01'1.15)(DoubleLine)(RotateL)","","","",   "(R)(+01)","","","",
                    "(R)(+01'1.15)(DoubleLine)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(R1)","","","",
                    "(R)(R1)","","","",   "(R)(R1)","","","",

                    "","","","",   "(R)(R1)","","","",
                    "(R)(+01)","","","",   "(R)(R1)","","","",
                    "(R)(+01)","","","",   ">R1","",">-11","",
                    ">-11","","","",   "(R)(+01)","","","",

                    "(R)(+01'1.15)(DoubleLine)(RotateR)","","","",   "(R)(+01)","","","",
                    "(R)(+01'1.15)(DoubleLine)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(R1)","","","",
                    "(R)(R1)","","","",   "(R)(R1)","","","",

                    "","","","",   "(R)(R1)","","","",
                    "(R)(R1)","","","",   "(+0)(R1)","","","",
                    "(R)(+01)","","","",   "<R","","<+1","",
                    "<+1","","","",   "(R)(+01)","","","",
                    //
                    "(R)(+01)(DoubleLine)(RotateL)","","","",   "(R)(+01)","","","",
                    "!!3",">R'1.2",">+21'1.2",">+2'1.2",   ">+21'1.2","","","",
                    "(R)(+01)(DoubleLine)(RotateR)","","","",   "(R)(R1)","","","",
                    "(R)(R1)","","","",   "(R)(R1)","","","",

                    "(DoubleLine)(RotateL)","","","",   "(R)(+01)","","","",
                    "!!3","<R1'1.2","<+2'1.2","<+21'1.2",   "<+2'1.2","","","",
                    "(R)(+01)(DoubleLine)(RotateR)","","","",   "(R)(+01)","","","",
                    "(R)(R1)","","","",   "(R)(R1)","","","",

                    "(R)(D1)(DoubleLine)(SRotateL)","","+01","",   "(+01)(R)","","","",
                    "(R1)(D)(DoubleLine)(SRotateR)","","+0","",   "(+0)(R1)","","","",
                    "(R)(D1)(DoubleLine)(SRotateL)","","+01","",   "(+01)(R)","","","",
                    "(R1)(D)(DoubleLine)(SRotateR)","","+0","",   "(+0)","","","",

                    "($0'1.2)(#0.8#$0)($21)($11)(DoubleLine)(BRotateL)","","$21","",   "$21","","$21","",
                    "($21)(#0.8#$21)($0)($1)(DoubleLine)(BRotateR)","","$0","",   "$0","","$00","",
                    "($0'1.2)(#0.8#$0)($21)($11)(DoubleLine)(BRotateL)","","$21","",   "$21","","$21","",
                    "($21)(#0.8#$21)($0)($1)(DoubleLine)(BRotateR)","","$0","",   "$0","","$00","",
                    //
                    "(#1.3#$01)(#1.3#$2)(Return)","","","",   "","","","",
                    "","","","",   "($01)($2)","","","",
                    "","","","",   "($21)($0)","","","",
                    "","","","",   "","","","",
                    "(R'1.2)(D1'1.2)(UpLine)(Camera1)","","","",   "","","","",
                    "","","","",   "(R'1.2)(D1'1.2)(UpLine)(Camera2)","","","",
                    "","","","",   "","","","",

                    "(#4#$31)($0'1.4)($0'1.7)(>$311'1.2)(Shake)(Line)(Camera3)(Heal)","","($2'1.4)($2'1.7)(<$311'1.2)(Line)","",   "($0'1.4)($0'1.7)(>$311'1.2)(Line)","","($2'1.4)($2'1.7)(<$311'1.2)(Line)","",
                    "($0'1.4)($0'1.7)(>$311'1.2)(Line)","","($2'1.4)($2'1.7)(<$311'1.2)(Line)","",   "($0'1.4)($0'1.7)(>$311'1.2)(Line)","","($2'1.4)($2'1.7)(<$311'1.2)(Line)","",
                    "($0'1.4)($0'1.7)(>$311'1.2)(Line)","","($2'1.4)($2'1.7)(<$311'1.2)(Line)","",   "($0'1.4)($0'1.7)(>$311'1.2)(Line)","","($2'1.4)($2'1.7)(<$311'1.2)(Line)","",
                    "($0'1.4)($0'1.7)(>$311'1.2)(Line)","","($2'1.4)($2'1.7)(<$311'1.2)(Line)","",   "($0'1.4)($0'1.7)(>$311'1.2)(Line)","","($2'1.4)($2'1.7)(<$311'1.2)(Line)","",
                    "($0'1.4)($0'1.7)(>$311'1.2)(Line)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void part6()
            {
                RegisterFunctionOnce("Flicker", () => { MakeFlicker(Color.White * 0.4f); });
                RegisterFunctionOnce("BFlicker", () => { MakeFlicker(Color.White); });
                RegisterFunctionOnce("Shake1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Shake2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, -4, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(8, BeatTime(1.5f), false);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(8, BeatTime(1.5f), true);

                });
                RegisterFunctionOnce("RotateBack", () =>
                {
                    RotateTo(0f, BeatTime(2.3f));
                });
                RegisterFunctionOnce("Rotate1", () =>
                {
                    Rotate(-180, BeatTime(2.5f));
                    Heart.FollowScreen(BeatTime(2.5f));

                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    RotateTo(7, BeatTime(1.2f));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(0, 0), new(800, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    RotateTo(-7, BeatTime(1.2f));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(640, 0), new(-200, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RotateBack2", () =>
                {
                    RotateTo(0, BeatTime(1.2f));
                });
                RegisterFunctionOnce("Convulse", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);
                    DelayBeat(0.5f, () => { Convulse(4, BeatTime(0.5f), true); });
                });
                RegisterFunctionOnce("Expand", () =>
                {
                    SizeExpand(12, BeatTime(4));
                });
                RegisterFunctionOnce("HeartShine", () =>
                {
                    SetSoul(1);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.8f, new string[]
                {
                    "D","","","",   "+0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "-0","","","",
                    "D","","","",   "+0","","","",

                    "D","","","",   "-0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "+0","","","",
                    "D","","","",   "+0","","","",

                    "$3'1.4(Rotate1)","","","",   "$3'1.4","","","",
                    "$3'1.4","","","",   "$3'1.4","","$3'1.4","",
                    "$3'1.4","","","",   "$3'1.4","","","",
                    "$3'1.4","","","",   "","","","",

                    "D","","","",   "+0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "-0","","","",
                    "D","","","",   "+0","","","",
                    //
                    "D","","","",   "-0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "+0","","","",
                    "D","","","",   "-0","","","",

                    "D","","","",   "+0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "-0","","","",
                    "D","","","",   "-0","","","",

                    "$3'1.5(Rotate1)","","$3'1.5","","$3'1.5","","$3'1.5","",
                    "$3'1.5","","$3'1.5","",   "$3'1.5","","$3'1.5","",
                    "$3'1.5","","","", "","","","",
                    "","","","",   "","","","",

                    "D","","","",   "+0","","RotateL","",
                    "!!3","<$0'1.3","<$21'1.3","<$0'1.3",   "<$21'1.3","","","",
                    "D","","","",   "+0","","","",
                    "D","","","",   "+0","","","",   
                    //
                    "D","","","",   "+0","","RotateR","",
                    "!!3",">$01'1.3",">$2'1.3",">$01'1.3",   ">$2'1.3","","","",
                    "D","","","",   "+0","","","RotateBack2",
                    "D","","","",   "+0","","","",

                    "D","","","",   "+0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "+0","","","",
                    "D","","","",   "+0","","","",

                    "D","","","",   "+0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "+0","","","",
                    "D","","","",   "+0","","","",

                    "D","","","",   "+0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "+0","","","",
                    "D","","","",   "+0","","","",

                    "D","","","",   "+0","","","",
                    "$0'1.5","","+1'1.5","",   "+1'1.5","","","",
                    "D","","","",   "+0","","","",
                    "D","","","",   "+0","","","",

                });
                CreateChart(BeatTime(4), BeatTime(1), 7.7f, new string[]
                {
                    "R1","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",

                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    //
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",

                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "R1","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",

                    "R1(LeftC)","","","",   "","","","",
                    "R1(LeftC)","","","",   "","","","",
                    "($01)($21)(LeftC)","","","",   "($01)($21)","","","",
                    "($01)($21)(LeftC)","","","",   "","","","",

                    "($01)($21)(Convulse)","","","",   "($01)($21)","","","",
                    "($01)($21)(Convulse)","","","",   "($01)($21)","","","",
                    "($01)($21)(Convulse)","","","",   "($01)($21)","","","",
                    "($01)($21)(Convulse)","","","",   "($01)($21)","","","",
                    //
                    "($01)($21)(Convulse)","","","",   "($01)($21)","","","",
                    "($01)($21)(Convulse)","","","",   "($01)($21)","","","",
                    "($01)($21)(Convulse)","","","",   "($01)($21)","","","",
                    "($01)($21)(Convulse)","","","",   "($01)($21)","","","",

                    "($0'1.3)($1'1.3)(Shake1)(Flicker)","","N11(Flicker)","",   "($0'1.3)($1'1.3)(Flicker)","","N11(Flicker)","",
                    "($0'1.3)($1'1.3)(Flicker)","","N11(Flicker)","",   "($0'1.3)($1'1.3)(Flicker)","","","",
                    "($21'1.3)($11'1.3)(Shake2)(Flicker)","","N1(Flicker)","",   "($21'1.3)($11'1.3)(Flicker)","","N1(Flicker)","",
                    "($21'1.3)($11'1.3)(Flicker)","","N1(Flicker)","",   "($21'1.3)($11'1.3)(Flicker)","","","",
                    "(R)(R1)(BFlicker)(Expand)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(R1)(BFlicker)(HeartShine)","","","",   "","","","",
                    "","","","",   "","","","",
                });
                float offset = 0;
                ValueEasing.EaseBuilder ve = new();
                ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 8, BeatTime(2)));
                ve.Run((s) =>
                {
                    offset = s;
                });
                CentreEasing.EaseBuilder ce1 = new();
                ce1.Insert(0, CentreEasing.Stable(320, 240));
                ce1.Insert(BeatTime(2), CentreEasing.EaseInSine(new(320, 240), new(320, 240 + 8), BeatTime(2)));
                CentreEasing.EaseBuilder ce2 = new();
                ce2.Insert(0, CentreEasing.Stable(320, 240));
                ce2.Insert(BeatTime(2), CentreEasing.EaseInSine(new(320, 240), new(320, 240 - 8), BeatTime(2)));
                Line l1 = new(ce1.GetResult(), ValueEasing.Stable(0)) { Alpha = 0f };
                Line l2 = new(ce2.GetResult(), ValueEasing.Stable(0)) { Alpha = 0f };
                l1.AlphaIncrease(BeatTime(2), 0.2f);
                l2.AlphaIncrease(BeatTime(2), 0.2f);
                CreateEntity(l1);
                CreateEntity(l2);
                ForBeat(0, 16, () =>
                {
                    Arrow[] s = GetAll<Arrow>();
                    for (int a = 0; a < s.Length; a++)
                    {
                        int x = a;
                        if (s[x].ArrowColor == 1)
                        {
                            if (s[x].Way == 0) s[x].Offset = new(0, -offset);
                            //if (s[x].Way == 1) s[x].Offset = new(-offset, 0);
                            if (s[x].Way == 2) s[x].Offset = new(0, -offset);
                            //if (s[x].Way == 3) s[x].Offset = new(offset, 0);
                        }
                        if (s[x].ArrowColor == 0)
                        {
                            if (s[x].Way == 0) s[x].Offset = new(0, offset);
                            //if (s[x].Way == 1) s[x].Offset = new(offset, 0);
                            if (s[x].Way == 2) s[x].Offset = new(0, offset);
                            //if (s[x].Way == 3) s[x].Offset = new(-offset, 0);
                        }
                    }
                });
                DelayBeat(60, () =>
                {
                    l1.AlphaDecrease(BeatTime(2));
                    l2.AlphaDecrease(BeatTime(2));
                });
                UISettings.RemoveUISurface();

            }
            void part7()
            {
                UISettings.CreateUISurface();
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(4), CentreEasing.EaseOutQuart(new(0, 0), new(440, 0), BeatTime(4)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(4));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.EaseOutQuart(new(640, 0), new(200, 0), BeatTime(4)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(4));
                });
                RegisterFunctionOnce("RotL1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(3f), ValueEasing.EaseOutBack(0, 5, BeatTime(3)));
                    ve.Insert(BeatTime(1f), ValueEasing.Stable(5));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(5, 13, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(13, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("RotR1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(3f), ValueEasing.EaseOutBack(0, -5, BeatTime(3)));
                    ve.Insert(BeatTime(1f), ValueEasing.Stable(-5));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(-5, -13, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(-13, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("ScreenScale1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(1.25f));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseInQuart(1.25f, 1, BeatTime(2f)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("ScreenScale2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(1.45f));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseInQuart(1.45f, 1, BeatTime(2f)));
                });
                RegisterFunctionOnce("LeftC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);

                });
                RegisterFunctionOnce("RightC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), true);

                });
                RegisterFunctionOnce("ScreenScale", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1.12f));
                    ve.Insert(BeatTime(4), ValueEasing.EaseOutSine(1.12f, 1.5f, BeatTime(4)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("Over", () =>
                {
                    LerpScreenScale(BeatTime(4), 1, 0.05f);
                });
                RegisterFunctionOnce("Scale", () =>
                {
                    LerpScreenScale(BeatTime(4), 1.12f, 0.05f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 7.3f, new string[]
                {
                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "$01","","$11","",
                    "($21)(^+002'1.5)(RotL1)(Line1)","","","",   "","","","",
                    "R","","+0","",   "(R)(D1)","","","",
                    "(R)(D1)","","","",   "(R)(D1)","","","",
                    "(R)(D1)","","","",   "(R)(D1)","","","",
                    "(R1)(D)(^+002'1.3)(Line1)","","","",   "(R)(D1)","","","",
                    //
                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "$2","","$1","",
                    "($0)(^+012'1.5)(RotR1)(Line2)","","","",   "","","","",
                    "R","","+0","",   "(R)(D1)","","","",
                    "(R)(D1)","","","",   "(R)(D1)","","","",
                    "(R)(D1)","","","",   "(R)(D1)","","","",
                    "(R)(D1)(^+012'1.3)(Line2)","","","",   "(R)(D1)","","","",
                    //
                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "$21","","$11","",
                    "($01)(^+002'1.5)(RotL1)(Line1)","","","",   "","","","",
                    "R","","+0","",   "(R)(D1)","","","",
                    "(R)(D1)","","","",   "(R)(D1)","","","",
                    "(R)(D1)","","","",   "(R)(D1)","","","",
                    "(R1)(D)(^+002'1.3)(Line1)","","","",   "(R)(D1)","","","",
                    //
                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "$0","","$1","",
                    "($2)(^+012'1.5)(RotR1)(Line2)","","","",   "","","","",
                    "R","","+0","",   "(R)(D1)","","","",
                    "(R)(D1)","","","",   "(R)(D1)","","","",
                    "(R)(D1)","","","",   "(R)(D1)","","","",
                    "(R)(D1)(^+012'1.3)(Line2)","","","",   "(R)(D1)","","","",
                    //

                    "(R)(+21)","","","",   "(+0)(+21)","","","",
                    "","","","",   "(R)(+21)","","(+0)(+21)","",
                    "(+0)(+21)","","","",   "(R)(R1)","","","",
                    "(R)(R1)","","","",   "(R)(R1)","","","",
                    "(R)(R1)","","","",   "(R)(R1)","","","",
                    "(R)(R1)","","","",   "(R)(R1)","","","",
                    "(R)(R1)(LeftC2)(Scale)","","","",   "(R)(D1)(RightC2)","","","",

                    "","","","",   "(R)(D1)(RightC2)","","","",
                    "(R)(D1)(LeftC2)","","","",   "","","","",
                    "(R)(D1)(LeftC2)","","","",   "(R)(D1)(LeftC2)","","","",
                    "","","","",   "(R)(D1)(RightC2)","","","",
                    "(R)(D1)(LeftC2)","","","",   "","","","",

                    "(#6.9#$2)(#6.9#$01)(Shake)(ScreenScale)","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "(^$0'1.5)(^$31'1.5)(^$2'1.5)(^$11'1.5)(Over)","","","", "","","","",
                });
            }
            void Epart1()
            {
                UISettings.CreateUISurface();
                RegisterFunctionOnce("Line1", () =>
                {
                    Line l = new(CentreEasing.Stable(420, 240), ValueEasing.Stable(70));
                    Line l1 = new(CentreEasing.Stable(470, 240), ValueEasing.Stable(70));
                    CreateEntity(l);
                    CreateEntity(l1);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 0.1f, BeatTime(0.5f)));
                    ve.Insert(1, ValueEasing.Linear(0.1f, 1f, 1));
                    ve.Insert(BeatTime(2f), ValueEasing.Linear(1, 0, BeatTime(1.5f)));
                    ve.Run((s) => { l1.Alpha = s; l.Alpha = s; });
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    Line l = new(CentreEasing.Stable(320 - 100, 240), ValueEasing.Stable(110));
                    Line l1 = new(CentreEasing.Stable(320 - 50, 240), ValueEasing.Stable(110));
                    CreateEntity(l);
                    CreateEntity(l1);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 0.1f, BeatTime(0.5f)));
                    ve.Insert(1, ValueEasing.Linear(0.1f, 1f, 1));
                    ve.Insert(BeatTime(2f), ValueEasing.Linear(1, 0, BeatTime(1.5f)));
                    ve.Run((s) => { l1.Alpha = s; l.Alpha = s; });
                });
                RegisterFunctionOnce("StartLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 240));
                    ce.Insert(BeatTime(16), CentreEasing.EaseInSine(new(0, 240), new(290, 300), BeatTime(8)));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(2), ValueEasing.Stable(90));
                    ve.Insert(BeatTime(6), ValueEasing.EaseInQuad(90, 90 - 71, BeatTime(6)));
                    Line l = new(ce.GetResult(), ve.GetResult());
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(6)); });
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    LineShadow(5, 0.24f, 5, l);
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(4, BeatTime(1.5f), false);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(4, BeatTime(1.5f), true);

                });
                RegisterFunctionOnce("LeftC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);

                });
                RegisterFunctionOnce("RightC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), true);

                });
                RegisterFunctionOnce("LeftC3", () =>
                {
                    Convulse(6, BeatTime(0.9f), false);

                });
                RegisterFunctionOnce("RightC3", () =>
                {
                    Convulse(6, BeatTime(0.9f), true);

                });
                RegisterFunctionOnce("SizeExpend", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1, 1.2f, BeatTime(1)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 6, BeatTime(0.25f)));
                    for (int a = 0; a < 11; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(6, -6, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-6, 6, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(6, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Return", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1.2f));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1.2f, 1f, BeatTime(1f)));
                    ve.Run((s) => { ScreenScale = s; });
                    DelayBeat(1, () => { ScreenScale = 1; });
                });
                RegisterFunctionOnce("CameFromLeftLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(3.5f), CentreEasing.FromLeft(320, BeatTime(3.5f)));
                    ce.Insert(BeatTime(0.5f), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));

                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                        ce2.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                        Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                        l2.AlphaDecrease(BeatTime(2));
                        CreateEntity(l2);
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("CameFromRightLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(3.5f), CentreEasing.FromRight(320, BeatTime(3.5f)));
                    ce.Insert(BeatTime(0.5f), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                        ce2.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                        Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                        l2.AlphaDecrease(BeatTime(2));
                        CreateEntity(l2);
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });

                    CreateEntity(l);
                });
                RegisterFunctionOnce("CameFromDoubleLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4f), CentreEasing.EaseOutQuart(new(640, 0), new(320, 0), BeatTime(4f)));

                    ce.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 0));
                    ce2.Insert(BeatTime(4f), CentreEasing.EaseOutQuart(new(0, 0), new(320, 0), BeatTime(4f)));

                    ce2.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    CreateEntity(l2);
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));
                        l2.AlphaDecrease(BeatTime(2));
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("SmallShake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 1, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(1, -1, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-1, 1, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(1, 0, BeatTime(0.25f)));
                    DelayBeat(8.1f, () =>
                    {

                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("DoubleLine", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 0));
                    ce1.Insert(BeatTime(1.3f), CentreEasing.FromRight(640 - 320 + 46, BeatTime(1.3f)));
                    ce1.Insert(BeatTime(0.7f), CentreEasing.Stable(640 - 320 - 46, BeatTime(0.7f)));
                    ce1.Insert(BeatTime(1), CentreEasing.FromRight(430, BeatTime(1)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(640 + 87, 0));
                    ce2.Insert(BeatTime(1.3f), CentreEasing.FromRight(640 - 320 + 40, BeatTime(1.3f)));
                    ce2.Insert(BeatTime(0.7f), CentreEasing.Stable(640 - 320 - 40, BeatTime(0.7f)));
                    ce2.Insert(BeatTime(1), CentreEasing.FromRight(430, BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(4, () => { l1.Dispose(); l2.Dispose(); });
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(8), ValueEasing.EaseInQuart(0, 360 * 2, BeatTime(8)));
                    ve.Run((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                        Heart.InstantSetRotation(s);
                    });
                    DelayBeat(8.01f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        Heart.InstantSetRotation(0);
                    });
                });
                RegisterFunctionOnce("Change1", () =>
                {
                    UISettings.RemoveUISurface();
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(20, BeatTime(0.5f));
                });
                CreateChart(0, BeatTime(1), 6.8f, new string[]
                {
                    "StartLine","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "$3'0.75(RightC)","","","",   "","","","",
                    "($0'0.9)($2'0.9)(LeftC)","","","",   "","","","",
                    "","","","",   "(^R'1.5)(^R1'1.5)(RightC)","","","",
                    "","","","",   "","","","",

                    "(^R'1.5)(^R1'1.5)(SizeExpend)","","","",   "","","","",
                    "$0(Shake)","","$0","",   "$2","","$2","",
                    "$01","","$01","",   "$21","","$21","",
                    "$0","","$0","",   "$2","","$2","",

                    "$01","","$01","",   "$21","","$21","",
                    "$0","","$0","",   "$2","","$2","",
                    "$01","","$01","",   "$21","","$21","",
                    "(^$0'1.5)(^$21'1.5)","(Return)","","", "","","","",
                    //
                    "","","","",   "","","","",
                    "(R)(R1)(LeftC)(CameFromLeftLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+0'1.2","","+0'1.2","",

                    "+0'1.2","","","",   "","","","",
                    "(R)(R1)(RightC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",

                    "R","","","",   "","","","",
                    "(R)(R1)(RightC)(CameFromRightLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+0'1.2","","+0'1.2","",

                    "+0'1.2","","","",   "R","","","",
                    "(R)(R1)(LeftC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",
                    //
                    "(R)(RightC2)","","","",   "(R)(LeftC2)","","","",
                    "(R)(R1)(CameFromDoubleLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+0'1.2","","+0'1.2","",

                    "+0'1.2","","","",   "","","","",
                    "(R)(R1)(RightC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",

                    "R","","","",   "","","","",
                    "(R)(+21)(LeftC2)(Line2)","","","",   "(+0)(+21)(RightC2)","","","",
                    "","","","",   "","","","",
                    "R","","","",   "R","","^+0'1.5","",

                    "R","","","",   "R","","","",
                    "(R)(R1)(RightC)(DoubleLine)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(R1)(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "(R)(R1)(LeftC3)","","","",   "","","","",
                    "(R)(R1)(LeftC3)","","","",   "","","","",
                    "!!3/6","(R'1.2)(LeftC3)","","+0'1.2","","+0'1.2","",
                    "(+0'1.2)(LeftC3)","","","",   "R","","","",

                    "(R)(LeftC3)","","","",   "","","","",
                    "(R1)(LeftC3)","","","",   "R","","","",
                    "(R)(LeftC3)","","","",   "","","","",
                    "(R1)(LeftC3)","","","",   "","","","",

                    "(R)(+21)(RightC3)(Line1)","","","",   "(+0)(+21)","","","",
                    "(R)(RightC3)","","","",   "","","","",
                    "!!3/6","(R'1.2)(RightC3)","","+0'1.2","","+0'1.2","",
                    "(+0'1.2)(RightC3)","","","",   "R","","","",

                    "(R)(RightC3)","","","",   "","","","",
                    "(R)(RightC3)","","","",   "(R1)","","","",
                    "(R)(RightC3)","","","",   "","","","",
                    "(R)(RightC3)","","","",   "(R1)","","","",
                    //
                    "R'1.2(Rotate)","","+0'1.2","",   "+0'1.2","","R1'1.2","",
                    "+01'1.2","","+01'1.2","",   "R'1.2","","+0'1.2","",
                    "+0'1.2","","R1'1.2","",   "+01'1.2","","+01'1.2","",
                    "R'1.2","","+0'1.2","",   "+0'1.2","","R11'1.2","",

                    "+01'1.2","","+01'1.2","",   "R'1.2","","+0'1.2","",
                    "+0'1.2","","R1'1.2","",   "+01'1.2","","+011'1.2","",
                    "R","","+0","",   "+0","","R1","",
                    "+01","","+01","",   "R","","R1","",

                    "R","","","",   "","","","",
                    "","","","",   "","","","",
                    "(^$11'1.2)","","","",   "","","","",
                    "","","","",   "","","","",

                    "R","","","",   "R","","","",
                    "R","","","",   "R","","","",
                    "R","","","",   "R","","","",
                    "","","","",   "Change1","","","",
                    //
                });
            }
            void Epart2()
            {
                RegisterFunctionOnce("Line", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("LineN", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(100));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    Line l1 = new(CentreEasing.Stable(LastRand + 50, 240), ValueEasing.Stable(100));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("LineM", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(80));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    Line l1 = new(CentreEasing.Stable(LastRand - 50, 240), ValueEasing.Stable(80));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(0, CentreEasing.Stable(-60, 240));
                    ve.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(-60, 240), new(700, 240), BeatTime(1)));
                    Line l = new(ve.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    LineShadow(1.5f, 0.75f, 5, l);
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(0, CentreEasing.Stable(700, 240));
                    ve.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(700, 240), new(-60, 240), BeatTime(1)));
                    Line l = new(ve.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    LineShadow(1.5f, 0.75f, 5, l);
                });
                RegisterFunctionOnce("Change1", () =>
                {
                    UISettings.RemoveUISurface();
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(-20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change3", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    Heart.GiveForce(180, 0);
                    RotateTo(180 + 20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change4", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(180 - 20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change5", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.02f), ValueEasing.Linear(0, 4, BeatTime(0.02f)));
                    ve.Insert(BeatTime(8f), ValueEasing.EaseOutSine(4, 1, BeatTime(8)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(0, BeatTime(0.5f));
                    Heart.GiveForce(0, 0);
                });
                RegisterFunctionOnce("NeverGonnaGiveYouUp", () =>
                {
                    Arrow b = MakeArrow(BeatTime(8), 0, 3, 0, 0);
                    Arrow r = MakeArrow(BeatTime(8), 2, 3, 1, 0);
                    CreateEntity(b);
                    CreateEntity(r);
                    DelayBeat(3.8f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(5.3f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(6.8f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(8.3f + 4, () =>
                    {
                        b.Delay(BeatTime(1.2f));
                        r.Delay(BeatTime(1.2f));
                    });
                    DelayBeat(9.3f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(10.8f + 4, () =>
                    {
                        b.ResetColor(1);
                        r.ResetColor(0);
                    });
                });
                RegisterFunctionOnce("ShineLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(16), CentreEasing.Alternate
                        (
                       BeatTime(0.5f),
                        new Func<ICustomMotion, Vector2>[] {

                            CentreEasing.Stable(640 / 5 * 1, 240),
                            CentreEasing.Stable(640 / 5 * 2, 240),
                            CentreEasing.Stable(640 / 5 * 3, 240),
                            CentreEasing.Stable(640 / 5 * 4, 240),
                        }
                        ));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.5f };
                    CreateEntity(l);
                    DelayBeat(10, () => { l.AlphaDecrease(BeatTime(4.5f)); });
                });
                RegisterFunctionOnce("RickLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 240));
                    ce.Insert(BeatTime(8), CentreEasing.FromLeft(278, BeatTime(8)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(278, 240), new(220, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(220, 240), new(150, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(150, 240), new(70, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseInQuart(new(70, 240), new(70 + 150, 240), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutQuart(new(70 + 150, 240), new(370, 240), BeatTime(0.75f)));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(8), ValueEasing.Stable(90));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(90, 105, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(105, 80, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(80, 90, BeatTime(1.5f)));
                    Line l = new(ce.GetResult(), ve.GetResult());
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    DelayBeat(12, () => { l.AlphaDecrease(BeatTime(2)); });
                    LineShadow(4f, 0.5f, 5, l);
                });
                CreateChart(BeatTime(3), BeatTime(1), 6.5f, new string[]
                {
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "$2","","$3","",
                    "$0","","","",    "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "R","","","",   "(Change2)","","","",

                    "","","","",   "","","","",
                    "","","","",   "$2","","$3","",
                    "$0","","","",    "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "R","","","",   "(Change3)","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "$2","","$3","",
                    "$0","","","",    "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "R","","","",   "(Change4)","","","",

                    "","","","",   "","","","",
                    "","","","",   "$2","","$3","",
                    "$0","","","",    "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "(D)","","","",
                    "(D)","","","",   "(Change5)","","","",
                    //
                    "(NeverGonnaGiveYouUp)(ShineLine)(RickLine)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    
                });
                CreateChart(BeatTime(3), BeatTime(1), 6.5f, new string[]
                {
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "(Line)","","","",    "","","","",
                    "","","","",   "(Line)","","","",
                    "","","","",   "(Line)","","","",
                    "","","","",   "","","","",
                    "(Line2)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "(LineN)","","","",    "","","","",
                    "","","","",   "(LineN)","","","",
                    "","","","",   "(LineN)","","","",
                    "","","","",   "","","","",
                    "(Line3)","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "(LineM)","","","",    "","","","",
                    "","","","",   "(LineM)","","","",
                    "","","","",   "(LineM)","","","",
                    "","","","",   "","","","",
                    "(Line3)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "(LineN)","","","",    "","","","",
                    "","","","",   "(LineN)","","","",
                    "","","","",   "(LineN)","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void Epart3()
            {
                Player.Heart main = Heart;
                Player.Heart extra;
                extra = Heart.InstantSplit(new CollideRect(-100, 240, 84, 84));
                RegisterFunctionOnce("MoveRight", () =>
                {

                    SetPlayerBoxMission(extra);
                    SetSoul(1);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(-100, 240));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(-100, 240), new(200, 240), BeatTime(2f)));
                    ce.Insert(BeatTime(0.66f), CentreEasing.Stable(200, 240));
                    ce.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(200, 240), new(-100, 240), BeatTime(1.33f)));
                    ce.Run((s) => { SetPlayerBoxMission(extra); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(320, 240));
                    ce2.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(420, 240), BeatTime(2f)));
                    ce2.Insert(BeatTime(0.66f), CentreEasing.Stable(420, 240));
                    ce2.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(420, 240), new(320, 240), BeatTime(1.33f)));
                    ce2.Run((s) => { SetPlayerBoxMission(main); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);
                });
                RegisterFunctionOnce("MoveLeft", () =>
                {
                    SetPlayerBoxMission(extra);
                    SetSoul(1);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(740, 240));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(740, 240), new(440, 240), BeatTime(2f)));
                    ce.Insert(BeatTime(0.66f), CentreEasing.Stable(440, 240));
                    ce.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(440, 240), new(740, 240), BeatTime(1.33f)));
                    ce.Run((s) => { SetPlayerBoxMission(extra); InstantSetBox(s, 84, 84); InstantTP(s); });
                    DelayBeat(16, () => { SetPlayerBoxMission(extra); InstantSetBox(4000, 84, 84); InstantTP(4000, -11451); SetPlayerBoxMission(main); });
                    SetPlayerBoxMission(main);
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(320, 240));
                    ce2.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(220, 240), BeatTime(2f)));
                    ce2.Insert(BeatTime(0.66f), CentreEasing.Stable(220, 240));
                    ce2.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(220, 240), new(320, 240), BeatTime(1.33f)));
                    ce2.Run((s) => { SetPlayerBoxMission(main); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);

                });
                RegisterFunctionOnce("NeverGonnaGiveYouUp", () =>
                {
                    Arrow b = MakeArrow(BeatTime(4), 2, 6.5f * 1.7f, 0, 0);
                    Arrow r = MakeArrow(BeatTime(4), 0, 6.5f * 1.7f, 1, 0);
                    CreateEntity(b);
                    CreateEntity(r);
                    DelayBeat(3.8f, () =>
                    {
                        b.ResetColor(1);
                        r.ResetColor(0);

                    });
                });
                RegisterFunctionOnce("SizeExpend", () =>
                {
                    SizeExpand(8, BeatTime(2f));
                });
                RegisterFunctionOnce("StartLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(360, 240));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(360, 240), new(-4, 240), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    DelayBeat(1, () => { l.Dispose(); });
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(25, BeatTime(2f), true);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(25, BeatTime(2f), false);

                });
                RegisterFunctionOnce("SLeftC", () =>
                {
                    Convulse(8, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("SRightC", () =>
                {
                    Convulse(8, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    RotateTo(180, BeatTime(2.5f));
                    SetPlayerBoxMission(main);
                    Heart.FollowScreen(BeatTime(2.5f));
                });
                RegisterFunctionOnce("DoubleLine", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(Rand(50, 540), 90 + 15 * m);
                    Line l2 = new(LastRand + 50, 90 + 15 * m);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("DoubleLine2", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(new Vector2(320, Rand(50, 380)), 15 * m);
                    Line l2 = new(new Vector2(320, LastRand + 50), 15 * m);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("Return", () =>
                {
                    RotateTo(0, BeatTime(4.5f));
                    Heart.FollowScreen(BeatTime(4.5f));
                });
                RegisterFunctionOnce("LeftLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(5), CentreEasing.XSinWave(320 + 46, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(5, () => { l.Dispose(); });
                    LineShadow(1, 0.3f, 7, l);
                });
                RegisterFunctionOnce("RightLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(5), CentreEasing.XSinWave(320 + 46, BeatTime(8), 1));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(5, () => { l.Dispose(); });
                    LineShadow(1, 0.3f, 7, l);
                });
                RegisterFunctionOnce("|Line1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.Stable(320 + 30, 480));
                    ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(-2, 0), new(-0.34f, 0f)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(8, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                });
                RegisterFunctionOnce("|Line2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, -38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.Stable(320 - 30, 480));
                    ce.Insert(BeatTime(3), CentreEasing.Accerlating(new(2, 0), new(0.34f, 0)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(7, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                });
                RegisterFunctionOnce("|Line3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, -38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320 - 30, 0));
                    ce.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(290, 0), new(290, 130), BeatTime(2)));
                    ce.Insert(BeatTime(4), CentreEasing.EaseInSine(new(290, 130), new(290, -300), BeatTime(4)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(7, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("XLine", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(BeatTime(4), CentreEasing.Stable(320, 240));
                    ce1.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 1), new(0, 0.24f)));
                    ValueEasing.EaseBuilder ve1 = new(), ve2 = new();
                    ve1.Insert(BeatTime(3), ValueEasing.Stable(45));
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutSine(45, 0, BeatTime(2)));
                    ve2.Insert(BeatTime(3), ValueEasing.Stable(90 + 45));
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutSine(90 + 45, 90, BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ve1.GetResult()) { Alpha = 0 };
                    Line l2 = new(ce1.GetResult(), ve2.GetResult()) { Alpha = 0 };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaIncrease(BeatTime(2), 0.75f);
                    l2.AlphaIncrease(BeatTime(2), 0.75f);
                    DelayBeat(6, () =>
                    {
                        l1.AlphaDecrease(BeatTime(1.5f));
                        l2.AlphaDecrease(BeatTime(1.5f));
                    });
                });
                SetPlayerBoxMission(main);
                UISettings.CreateUISurface();
                DelayBeat(40, () => { UISettings.RemoveUISurface(); });
                DelayBeat(84, () => { UISettings.CreateUISurface(); });
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
                    "(SizeExpend)(StartLine)","","","",   "","","","",
                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "(#0.8#$21)(N2)(LeftC)(DoubleLine2)","","","",   "(N2)","","","",
                    "(N2)","","","",   "(#0.8#$21)(N2)(LeftC)(DoubleLine2)","","","",
                    "(N2)","","","",   "(N2)","","","",
                    "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","","",   "(N2)","","","",

                    "N2","","","",   "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","","",
                    "N2","","","",   "N2","","","",
                    "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","","",   "","","","",
                    "(N2)","","","",   "","","","",
                    //
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",
                    "(N01)","","","",   "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",
                    "(N01)","","","",   "(N01)","","","",
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",

                    "N01","","","",   "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",
                    "N01","","","",   "N01","","","",
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",
                    "(R)(D1)","","","",   "","","","",

                    "(#0.8#R)(LeftC)","","","",   "","","","",
                    "","","","",   "D1","","","",
                    "(#0.8#R)(RightC)","","","",   "","","","",
                    "","","","",   "D1","","","",

                    "(#0.8#R)(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(+01)(SRightC)(|Line1)","","","",   "","","","",
                    "(R)(+01)(SLeftC)(|Line2)","","","",   "","","","",
                    //
                    "N0","(MoveRight)(LeftLine)","","",   "N0","","","",
                    "N0","","","",   "N0","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "N2","(MoveLeft)(RightLine)","","",   "N2","","","",
                    "N2","","","",   "N2","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "N0(|Line3)","","","",   "N0","","","",
                    "N0","","","",   "N0","","","",
                    "N0","","","",   "R","","","",
                    "R(Rotate)","","","",   "","","","",

                    "R(NeverGonnaGiveYouUp)","","","",   "","","","",
                    "","","","",   "","","","",
                    "($0'1.4)($21'1.4)(SLeftC)","","","",   "","","","",
                    "($01'1.4)($2'1.4)(SRightC)","","","",   "","","","",
                    //
                    "(SizeExpend)(StartLine)","","","",   "","","","",
                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "(#0.8#$21)(N2)(LeftC)(DoubleLine2)","","","",   "(N2)","","","",
                    "(N2)","","","",   "(#0.8#$21)(N2)(LeftC)(DoubleLine2)","","","",
                    "(N2)","","","",   "(N2)","","","",
                    "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","","",   "(N2)","","","",

                    "N2","","","",   "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","","",
                    "N2","","","",   "N2","","","",
                    "(#0.35#$21)(N2)(LeftC)(DoubleLine2)","","","",   "","","","",
                    "(N2)","","","",   "","","","",
                    //
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",
                    "(N01)","","","",   "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",
                    "(N01)","","","",   "(N01)","","","",
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",

                    "N01","","","",   "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",
                    "N01","","","",   "N01","","","",
                    "(#0.8#$0)(N01)(RightC)(DoubleLine)","","","",   "(N01)","","","",
                    "(R)(D1)","","","",   "","","","",

                    "(R)(R1)","","","",   "","","","",
                    "(R)","","","",   "(R)(R1)(Return)(XLine)","","","",
                    "(R)","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "R","","","",   "","","","",
                    "R","","","",   "","","","",
                    "R","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
                SetPlayerBoxMission(extra);
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "$3","","","",
                    "$3","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "$1","","","",
                    "$1","","","",   "$1","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void Epart4()
            {
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(BoxStates.Right, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromRight(200, BeatTime(2f)));

                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(2f));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(BoxStates.Left, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromLeft(200, BeatTime(2f)));

                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(2f));
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(25, BeatTime(2f), true);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(25, BeatTime(2f), false);

                });
                RegisterFunctionOnce("SLeftC", () =>
                {
                    Convulse(6, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("SRightC", () =>
                {
                    Convulse(6, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 3, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(3, -3, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-3, 3, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(3, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Flicker", () => { MakeFlicker(Color.White * 0.4f); });
                RegisterFunctionOnce("BFlicker", () => { MakeFlicker(Color.White); });
                RegisterFunctionOnce("ScreenL", () =>
                {
                    RotateWithBack(BeatTime(4), 4);
                });
                RegisterFunctionOnce("ScreenR", () =>
                {
                    RotateWithBack(BeatTime(4), -4);
                });
                RegisterFunctionOnce("ScreenL2", () =>
                {
                    RotateWithBack(BeatTime(8), 6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 1));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenR2", () =>
                {
                    RotateWithBack(BeatTime(8), -6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 1));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenH", () =>
                {
                    RotateSymmetricBack(BeatTime(8), 6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 1));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 0));
                    ce2.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 0));
                    ce2.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 1));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l2);
                    DelayBeat(4, () => { l2.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenExpand", () =>
                {
                    SizeExpand(8, BeatTime(4));
                });
                RegisterFunctionOnce("SuddenRotateL", () =>
                {
                    Rotate(6, BeatTime(0.125f));
                    DelayBeat(0.125f, () => { Rotate(-6, BeatTime(1.375f)); });
                });
                RegisterFunctionOnce("SuddenRotateR", () =>
                {
                    Rotate(-6, BeatTime(0.125f));
                    DelayBeat(0.125f, () => { Rotate(6, BeatTime(1.375f)); });
                });
                RegisterFunctionOnce("Over", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    LerpScreenScale(BeatTime(2), 1.2f, 0.06f);
                    DelayBeat(4, () => { LerpScreenScale(BeatTime(4), 1, 0.1f); });
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    RotateTo(30, BeatTime(4));
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.5f, new string[]
                {
#region part
                    "R","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(R)(^$21'1.8)(Line1)(ScreenL)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(R)(^$01'1.8)(Line2)(ScreenR)","","","",   "","","","",
                    "","","","",   "(R)","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "($1)(^$21'1.8)(^$01'1.8)(Line1)(Line2)(ScreenExpand)","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    //
                    "($01)($21)(SLeftC)","","","",   "($01)($21)(SLeftC)","","","",
                    "","","","",   "($0)($2)(SRightC)","","","",
                    "($0)($2)(SRightC)","","","",   "","","","",
                    "","","","",   "R","","","",

                    "(R)(R1)","","","",   "(R)","","","",
                    "!!3/6","$21","$0","$21","$0","","",
                    "($0'1.2)($21'1.2)","","","",   "($01'1.2)($2'1.2)(SLeftC)","","","",
                    "($0'1.2)($21'1.2)(SRightC)","","","",   "($01'1.2)($2'1.2)(SRightC)","","","",

                    "R'1.2(Shake)(Flicker)","","+0'1.2(Flicker)","",   "R1'1.2(Flicker)","","+01'1.2(Flicker)","",
                    "R'1.2(Flicker)","","+0'1.2(Flicker)","",   "R1'1.2(Flicker)","","+01'1.2(Flicker)","",
                    "R'1.2(Flicker)","","+0'1.2(Flicker)","",   "R1'1.2(Flicker)","","+01'1.2(Flicker)","",
                    "R'1.2(Flicker)","","+0'1.2(Flicker)","",   "R1'1.2(Flicker)","","+01'1.2(Flicker)","",

                    "($3'1.5)($31'1.5)(BFlicker)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    #endregion
#region part
                    "(#4#$1)(D1)(ScreenL2)","","","",   "","","","",
                    "(D1)","","","",   "","","","",
                    "!!3/6","N1","","+01","","N1","",
                    "+01","","","",   "","","","",

                    "(^$1'1.3)(^D1'1.3)","","","",   "","","","",
                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "(#4#$21)(D)(ScreenR2)","","","",   "","","","",
                    "(D)","","","",   "","","","",
                    "!!3/6","N21","","+0","","N11","",
                    "+0","","","",   "","","","",

                    "(^$21'1.3)(^D'1.3)","","","",   "","","","",
                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "(R)","","","",
                    //
                    "(#4#$0)(D1)(ScreenH)","","","",   "","","","",
                    "(D1)","","","",   "","","","",
                    "!!3/6","N0","","+01","","N1","",
                    "+01","","","",   "","","","",

                    "(^$0'1.3)(^D1'1.3)","","","",   "","","","",
                    "","","","",   "(R)(R1)","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",

                    "(#4#$31)(D)","","","",   "(D)","","","",
                    "","","","",   "","","","",
                    "(^N31'1.15)","","","",   "","","","",
                    "(^N31'1.15)","","","",   "","","","",

                    "(^$31'1.3)(^D'1.3)(SuddenRotateL)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(R1)","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    #endregion
#region part
                    "(R)(R1)","","","",   "","","","",
                    "(R)","","","",   "","","","",
                    "!!6/3","(R1)($0'1.5)(SuddenRotateR)","","($0'1.5)","(R1)","",
                    "($0'1.5)","","","",   "(R)","","","",

                    "(R)(+01)","","","",   "","","","",
                    "R","","","",   "(R)","","","",
                    "(R)","","","",   "(R)(+01)","","","",
                    "","","","",   "(R)(+01)(SuddenRotateL)","","","",

                    "","","","",   "","","","",
                    "","","","",   "(R)(D1)","","","",
                    "","","","",   "!!3/6","(R1'1.4)(+2'1.4)(SuddenRotateR)","","(+21'1.5)(+2'1.5)",
                    "","(+21'1.6)(+2'1.6)","",  "(+21'1.6)(+2'1.6)","","","",
                    "","","","",   "(+21'1.6)(+2'1.6)","","","",
                    "","","","",   "R","","","",
                    "(R)(+01)","","","",   "","","","",
                    "","","","",   "(R)(+01)(SuddenRotateL)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    //
                    "R","","","",   "(R1)","","","",
                    "R","","","",   "(R1)","","","",
                    "R","","","",   "(R1)","","","",
                    "R","","","",   "$0'1.2(Shake)(Flicker)","","$2'1.2(Flicker)","",

                    "$0'1.2(Flicker)","","$2'1.2(Flicker)","",   "$0'1.2(Flicker)","","$2'1.2(Flicker)","",
                    "$0'1.2(Flicker)","","$2'1.2(Flicker)","",
                    "($0'1.35)($01'1.35)(Flicker)","","($2'1.35)($21'1.35)(Flicker)","",   "($0'1.35)($01'1.35)(Flicker)","","($2'1.35)($21'1.35)(Flicker)","",
                    "($0'1.35)($01'1.35)(Flicker)","","($2'1.35)($21'1.35)(Flicker)","",   "($0'1.35)($01'1.35)(Flicker)","","($2'1.35)($21'1.35)(Flicker)","",

                    "($3'1.35)($31'1.35)(BFlicker)(Over)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "Rotate","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
#endregion
                });
            }
            void Epart5()
            {
                DelayBeat(4, () => { UISettings.CreateUISurface(); });

                RegisterFunctionOnce("DoubleLine", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(Rand(50, 540), 90 + 15 * m) { Alpha = 0.6f };
                    Line l2 = new(LastRand + 50, 90 + 15 * m) { Alpha = 0.6f };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    RotateTo(-6, BeatTime(2));

                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    RotateTo(6, BeatTime(2));

                });
                RegisterFunctionOnce("SRotateL", () =>
                {
                    RotateTo(-2, BeatTime(1));


                });
                RegisterFunctionOnce("SRotateR", () =>
                {
                    RotateTo(2, BeatTime(1));

                });
                RegisterFunctionOnce("BRotateL", () =>
                {
                    RotateTo(-6, BeatTime(1));


                });
                RegisterFunctionOnce("BRotateR", () =>
                {
                    RotateTo(6, BeatTime(1));

                });
                RegisterFunctionOnce("Return", () =>
                {
                    RotateTo(0, BeatTime(1));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(1, 1.3f, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    DelayBeat(1.501f, () => { ScreenScale = 1.15f; });
                    DelayBeat(2.501f, () => { ScreenScale = 1f; });
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Line", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 480));
                    ce.Insert(BeatTime(7), CentreEasing.Accerlating(new(0, -80), new(0, -0.1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.75f };
                    CreateEntity(l);
                    LineShadow(4, 0.75f, 4, l);
                });
                RegisterFunctionOnce("UpLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 600));
                    ce.Insert(BeatTime(7), CentreEasing.Accerlating(new(0, -100), new(0, -0.1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(40)) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("Camera1", () =>
                {
                    ScreenDrawing.ScreenAngle = 30;
                    ScreenScale = 0.9f;
                    ScreenPositionDelta = new(10, 20);
                });
                RegisterFunctionOnce("Camera2", () =>
                {
                    ScreenDrawing.ScreenAngle = -24;
                    ScreenScale = 0.8f;
                    ScreenPositionDelta = new(10, -20);
                });
                RegisterFunctionOnce("Camera3", () =>
                {
                    //ScreenDrawing.ScreenAngle = 0;
                    //ScreenScale = 2.5f;
                    DelayBeat(0, () => { ScreenPositionDelta = Vector2.Zero; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(1, ValueEasing.Stable(2.5f));
                    ve.Insert(BeatTime(2.5f), ValueEasing.EaseInQuad(2.5f, 1f, BeatTime(2.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    Heart.InstantSetRotation(0);
                });
                CreateChart(BeatTime(4), BeatTime(1), 7.3f, new string[]
                {
                    "(R)(+01'1.15)(DoubleLine)(RotateL)","","","",   "(R)(+01)","","","",
                    "(R)(+01'1.15)(DoubleLine)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",

                    "","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   ">R1","",">-01","",
                    ">-01","","","",   "(R)(+01)","","","",

                    "(R)(+01'1.15)(DoubleLine)(RotateR)","","","",   "(R)(+01)","","","",
                    "(R)(+01'1.15)(DoubleLine)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",

                    "","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "<R","","<+0","",
                    "<+0","","","",   "(R)(+01)","","","",
                    //
                    "(R)(+01)(DoubleLine)(RotateL)","","","",   "(R)(+01)","","","",
                    "!!3","(R)(+01)","","",   "(R)(+01)","","","",
                    "(R)(+01)(DoubleLine)(RotateR)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",

                    "(DoubleLine)(RotateL)","","","",   "(R)(+01)","","","",
                    "!!3","(R)(+01)","","",   "(R)(+01)","","","",
                    "(R)(+01)(DoubleLine)(RotateR)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",

                    "(R)(+01)(DoubleLine)(SRotateL)","","","",   "(+01)","","","",
                    "(R)(+01)(DoubleLine)(SRotateR)","","","",   "(+0)","","","",
                    "(R)(+01)(DoubleLine)(SRotateL)","","","",   "(+01)","","","",
                    "(R)(+01)(DoubleLine)(SRotateR)","","","",   "(+0)","","","",

                    "(#0.8#R)(+01)(DoubleLine)(BRotateL)","","","",   "","","","",
                    "(#0.8#R)(+01)(DoubleLine)(BRotateR)","","","",   "","","","",
                    "(#0.8#R)(+01)(DoubleLine)(BRotateL)","","","",   "","","","",
                    "(#0.8#R)(+01)(DoubleLine)(BRotateR)","","","",   "","","","",
                    //
                    "(#1.3#$01)(#1.3#$2)(Return)","","","",   "","","","",
                    "","","","",   "($01)($2)","","","",
                    "","","","",   "($21)($0)","","","",
                    "","","","",   "","","","",
                    "(R'1.2)(D1'1.2)(UpLine)(Camera1)","","","",   "","","","",
                    "","","","",   "(R'1.2)(D1'1.2)(UpLine)(Camera2)","","","",
                    "","","","",   "","","","",

                    "(#4#$31)(>$311'1.2)(Shake)(Line)(Camera3)","","(<$311'1.2)(Line)","",   "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",
                    "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",   "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",
                    "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",   "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",
                    "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",   "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",
                    "(>$311'1.2)(Line)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void Epart6()
            {
                RegisterFunctionOnce("Flicker", () => { MakeFlicker(Color.White * 0.4f); });
                RegisterFunctionOnce("BFlicker", () => { MakeFlicker(Color.White); });
                RegisterFunctionOnce("Shake1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Shake2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, -4, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(8, BeatTime(1.5f), false);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(8, BeatTime(1.5f), true);

                });
                RegisterFunctionOnce("RotateBack", () =>
                {
                    RotateTo(0f, BeatTime(2.3f));
                });
                RegisterFunctionOnce("Rotate1", () =>
                {
                    Rotate(-180, BeatTime(2.5f));
                    Heart.FollowScreen(BeatTime(2.5f));

                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    RotateTo(7, BeatTime(1.2f));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(0, 0), new(800, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    RotateTo(-7, BeatTime(1.2f));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(640, 0), new(-200, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RotateBack2", () =>
                {
                    RotateTo(0, BeatTime(1.2f));
                });
                RegisterFunctionOnce("Convulse", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);
                    DelayBeat(0.5f, () => { Convulse(4, BeatTime(0.5f), true); });
                });
                RegisterFunctionOnce("Expand", () =>
                {
                    SizeExpand(12, BeatTime(4));
                });
                RegisterFunctionOnce("HeartShine", () =>
                {
                    SetSoul(1);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.8f, new string[]
                {
                    "D","","","",   "D","","","",
                    "$01'1.5","","+11'1.5","",   "+11'1.5","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "$01'1.5","","+11'1.5","",   "+11'1.5","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "$3'1.4(Rotate1)","","","",   "$3'1.4","","","",
                    "$3'1.4","","","",   "$3'1.4","","$3'1.4","",
                    "$3'1.4","","","",   "$3'1.4","","","",
                    "$3'1.4","","","",   "","","","",

                    "D","","","",   "D","","","",
                    "$01'1.5","","+11'1.5","",   "+11'1.5","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    //
                    "D","","","",   "D","","","",
                    "$01'1.5","","+11'1.5","",   "+11'1.5","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "$01'1.5","","+11'1.5","",   "+11'1.5","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "$3'1.5(Rotate1)","","$3'1.5","","$3'1.5","","$3'1.5","",
                    "$3'1.5","","$3'1.5","",   "$3'1.5","","$3'1.5","",
                    "$3'1.5","","","", "","","","",
                    "","","","",   "","","","",

                    "D","","","",   "D","","RotateL","",
                    "!!3","<$0'1.3","<$21'1.3","<$0'1.3",   "<$21'1.3","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",   
                    //
                    "D","","","",   "D","","RotateR","",
                    "!!3",">$01'1.3",">$2'1.3",">$01'1.3",   ">$2'1.3","","","",
                    "D","","","",   "D","","","RotateBack2",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "$01'1.5","","+11'1.5","",   "+11'1.5","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "$01'1.5","","+11'1.5","",   "+11'1.5","","","",


                });
                CreateChart(BeatTime(4), BeatTime(1), 7.7f, new string[]
                {
                    "","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    //
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "($01)($21)(LeftC)","","","",   "($0)($2)","","","",
                    "($01)($21)(LeftC)","","","",   "","","","",

                    "($01)($21)(Convulse)","","","",   "($0)($2)","","","",
                    "($01)($21)(Convulse)","","","",   "($0)($2)","","","",
                    "($01)($21)(Convulse)","","","",   "($0)($2)","","","",
                    "($01)($21)(Convulse)","","","",   "($0)($2)","","","",
                    //
                    "($01)($21)(Convulse)","","","",   "($0)($2)","","","",
                    "($01)($21)(Convulse)","","","",   "($0)($2)","","","",
                    "($01)($21)(Convulse)","","","",   "($0)($2)","","","",
                    "($01)($21)(Convulse)","","","",   "($0)($2)","","","",

                    "($0'1.3)(Shake1)(Flicker)","","N11(Flicker)","",   "($0'1.3)(Flicker)","","N11(Flicker)","",
                    "($0'1.3)(Flicker)","","N11(Flicker)","",   "($0'1.3)(Flicker)","","","",
                    "($21'1.3)(Shake2)(Flicker)","","N1(Flicker)","",   "($21'1.3)(Flicker)","","N1(Flicker)","",
                    "($21'1.3)(Flicker)","","N1(Flicker)","",   "($21'1.3)(Flicker)","","","",
                    "(R)(R1)(BFlicker)(Expand)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(R1)(BFlicker)(HeartShine)","","","",   "","","","",
                    "","","","",   "","","","",
                });
                float offset = 0;
                ValueEasing.EaseBuilder ve = new();
                ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 8, BeatTime(2)));
                ve.Run((s) =>
                {
                    offset = s;
                });
                CentreEasing.EaseBuilder ce1 = new();
                ce1.Insert(0, CentreEasing.Stable(320, 240));
                ce1.Insert(BeatTime(2), CentreEasing.EaseInSine(new(320, 240), new(320, 240 + 8), BeatTime(2)));
                CentreEasing.EaseBuilder ce2 = new();
                ce2.Insert(0, CentreEasing.Stable(320, 240));
                ce2.Insert(BeatTime(2), CentreEasing.EaseInSine(new(320, 240), new(320, 240 - 8), BeatTime(2)));
                Line l1 = new(ce1.GetResult(), ValueEasing.Stable(0)) { Alpha = 0f };
                Line l2 = new(ce2.GetResult(), ValueEasing.Stable(0)) { Alpha = 0f };
                l1.AlphaIncrease(BeatTime(2), 0.2f);
                l2.AlphaIncrease(BeatTime(2), 0.2f);
                CreateEntity(l1);
                CreateEntity(l2);
                ForBeat(0, 16, () =>
                {
                    Arrow[] s = GetAll<Arrow>();
                    for (int a = 0; a < s.Length; a++)
                    {
                        int x = a;
                        if (s[x].ArrowColor == 1)
                        {
                            if (s[x].Way == 0) s[x].Offset = new(0, -offset);
                            //if (s[x].Way == 1) s[x].Offset = new(-offset, 0);
                            if (s[x].Way == 2) s[x].Offset = new(0, -offset);
                            //if (s[x].Way == 3) s[x].Offset = new(offset, 0);
                        }
                        if (s[x].ArrowColor == 0)
                        {
                            if (s[x].Way == 0) s[x].Offset = new(0, offset);
                            //if (s[x].Way == 1) s[x].Offset = new(offset, 0);
                            if (s[x].Way == 2) s[x].Offset = new(0, offset);
                            //if (s[x].Way == 3) s[x].Offset = new(-offset, 0);
                        }
                    }
                });
                DelayBeat(60, () =>
                {
                    l1.AlphaDecrease(BeatTime(2));
                    l2.AlphaDecrease(BeatTime(2));
                });
                UISettings.RemoveUISurface();

            }
            void Epart7()
            {
                UISettings.CreateUISurface();
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(4), CentreEasing.EaseOutQuart(new(0, 0), new(440, 0), BeatTime(4)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(4));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.EaseOutQuart(new(640, 0), new(200, 0), BeatTime(4)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(4));
                });
                RegisterFunctionOnce("RotL1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(3f), ValueEasing.EaseOutBack(0, 5, BeatTime(3)));
                    ve.Insert(BeatTime(1f), ValueEasing.Stable(5));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(5, 13, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(13, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("RotR1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(3f), ValueEasing.EaseOutBack(0, -5, BeatTime(3)));
                    ve.Insert(BeatTime(1f), ValueEasing.Stable(-5));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(-5, -13, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(-13, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("ScreenScale1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(1.25f));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseInQuart(1.25f, 1, BeatTime(2f)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("ScreenScale2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(1.45f));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseInQuart(1.45f, 1, BeatTime(2f)));
                });
                RegisterFunctionOnce("LeftC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);

                });
                RegisterFunctionOnce("RightC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), true);

                });
                RegisterFunctionOnce("ScreenScale", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1.12f));
                    ve.Insert(BeatTime(4), ValueEasing.EaseOutSine(1.12f, 1.5f, BeatTime(4)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("Over", () =>
                {
                    LerpScreenScale(BeatTime(4), 1, 0.05f);
                });
                RegisterFunctionOnce("Scale", () =>
                {
                    LerpScreenScale(BeatTime(4), 1.12f, 0.05f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 7.3f, new string[]
                {
                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "$01","","$01","",
                    "($01)(^+002'1.5)(RotL1)(Line1)","","","",   "","","","",
                    "R","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)(^+002'1.3)(Line1)","","","",   "(R)(+01)","","","",
                    //
                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "$2","","$2","",
                    "($2)(^+012'1.5)(RotR1)(Line2)","","","",   "","","","",
                    "R","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)(^+012'1.3)(Line2)","","","",   "(R)(+01)","","","",
                    //
                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "$01","","$01","",
                    "($01)(^+002'1.5)(RotL1)(Line1)","","","",   "","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)(^+002'1.3)(Line1)","","","",   "(R)(+01)","","","",
                    //
                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "$2","","$2","",
                    "($2)(^+012'1.5)(RotR1)(Line2)","","","",   "","","","",
                    "R","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)(^+012'1.3)(Line2)","","","",   "(R)(+01)","","","",
                    //

                    "(R)(+21)","","","",   "(+2)(+21)","","","",
                    "","","","",   "(R)(+21)","","(+2)(+21)","",
                    "(+2)(+21)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)(LeftC2)(Scale)","","","",   "(R)(+01)(RightC2)","","","",

                    "","","","",   "(R)(+01)(RightC2)","","","",
                    "(R)(+01)(LeftC2)","","","",   "","","","",
                    "(R)(+01)(LeftC2)","","","",   "(R)(+01)(LeftC2)","","","",
                    "","","","",   "(R)(+01)(RightC2)","","","",
                    "(R)(+01)(LeftC2)","","","",   "","","","",

                    "(Shake)(ScreenScale)","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "^$3'1.3","","$31'1.5","",   "^$3'1.3","","$31'1.5","",
                    "(^$0'1.5)(^$31'1.5)(^$2'1.5)(^$11'1.5)(Over)","","","", "","","","",
                });
            }
            void Npart1()
            {
                UISettings.CreateUISurface();
                RegisterFunctionOnce("Line1", () =>
                {
                    Line l = new(CentreEasing.Stable(420, 240), ValueEasing.Stable(70));
                    Line l1 = new(CentreEasing.Stable(470, 240), ValueEasing.Stable(70));
                    CreateEntity(l);
                    CreateEntity(l1);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 0.1f, BeatTime(0.5f)));
                    ve.Insert(1, ValueEasing.Linear(0.1f, 1f, 1));
                    ve.Insert(BeatTime(2f), ValueEasing.Linear(1, 0, BeatTime(1.5f)));
                    ve.Run((s) => { l1.Alpha = s; l.Alpha = s; });
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    Line l = new(CentreEasing.Stable(320 - 100, 240), ValueEasing.Stable(110));
                    Line l1 = new(CentreEasing.Stable(320 - 50, 240), ValueEasing.Stable(110));
                    CreateEntity(l);
                    CreateEntity(l1);
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutQuad(1, 0.1f, BeatTime(0.5f)));
                    ve.Insert(1, ValueEasing.Linear(0.1f, 1f, 1));
                    ve.Insert(BeatTime(2f), ValueEasing.Linear(1, 0, BeatTime(1.5f)));
                    ve.Run((s) => { l1.Alpha = s; l.Alpha = s; });
                });
                RegisterFunctionOnce("StartLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 240));
                    ce.Insert(BeatTime(16), CentreEasing.EaseInSine(new(0, 240), new(290, 300), BeatTime(8)));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(2), ValueEasing.Stable(90));
                    ve.Insert(BeatTime(6), ValueEasing.EaseInQuad(90, 90 - 71, BeatTime(6)));
                    Line l = new(ce.GetResult(), ve.GetResult());
                    DelayBeat(2, () => { l.AlphaDecrease(BeatTime(6)); });
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    LineShadow(5, 0.24f, 5, l);
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(4, BeatTime(1.5f), false);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(4, BeatTime(1.5f), true);

                });
                RegisterFunctionOnce("LeftC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);

                });
                RegisterFunctionOnce("RightC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), true);

                });
                RegisterFunctionOnce("LeftC3", () =>
                {
                    Convulse(6, BeatTime(0.9f), false);

                });
                RegisterFunctionOnce("RightC3", () =>
                {
                    Convulse(6, BeatTime(0.9f), true);

                });
                RegisterFunctionOnce("SizeExpend", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1, 1.2f, BeatTime(1)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 6, BeatTime(0.25f)));
                    for (int a = 0; a < 11; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(6, -6, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-6, 6, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(6, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Return", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1.2f));
                    ve.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1.2f, 1f, BeatTime(1f)));
                    ve.Run((s) => { ScreenScale = s; });
                    DelayBeat(1, () => { ScreenScale = 1; });
                });
                RegisterFunctionOnce("CameFromLeftLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(3.5f), CentreEasing.FromLeft(320, BeatTime(3.5f)));
                    ce.Insert(BeatTime(0.5f), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));

                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                        ce2.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                        Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                        l2.AlphaDecrease(BeatTime(2));
                        CreateEntity(l2);
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("CameFromRightLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(3.5f), CentreEasing.FromRight(320, BeatTime(3.5f)));
                    ce.Insert(BeatTime(0.5f), CentreEasing.Stable(320, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(0), CentreEasing.Stable(320, 0));
                        ce2.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                        Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                        l2.AlphaDecrease(BeatTime(2));
                        CreateEntity(l2);
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });

                    CreateEntity(l);
                });
                RegisterFunctionOnce("CameFromDoubleLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4f), CentreEasing.EaseOutQuart(new(640, 0), new(320, 0), BeatTime(4f)));

                    ce.Insert(BeatTime(2), CentreEasing.FromRight(80, BeatTime(2)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 0));
                    ce2.Insert(BeatTime(4f), CentreEasing.EaseOutQuart(new(0, 0), new(320, 0), BeatTime(4f)));

                    ce2.Insert(BeatTime(2), CentreEasing.FromLeft(80, BeatTime(2)));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.8f };
                    CreateEntity(l2);
                    DelayBeat(4, () =>
                    {
                        l.AlphaDecrease(BeatTime(2));
                        l2.AlphaDecrease(BeatTime(2));
                        LineShadow(4, 0.3f, 3, l);
                        LineShadow(4, 0.3f, 3, l2);
                    });
                    CreateEntity(l);
                });
                RegisterFunctionOnce("SmallShake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 1, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(1, -1, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-1, 1, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(1, 0, BeatTime(0.25f)));
                    DelayBeat(8.1f, () =>
                    {

                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("DoubleLine", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(0, CentreEasing.Stable(640, 0));
                    ce1.Insert(BeatTime(1.3f), CentreEasing.FromRight(640 - 320 + 46, BeatTime(1.3f)));
                    ce1.Insert(BeatTime(0.7f), CentreEasing.Stable(640 - 320 - 46, BeatTime(0.7f)));
                    ce1.Insert(BeatTime(1), CentreEasing.FromRight(430, BeatTime(1)));
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(640 + 87, 0));
                    ce2.Insert(BeatTime(1.3f), CentreEasing.FromRight(640 - 320 + 40, BeatTime(1.3f)));
                    ce2.Insert(BeatTime(0.7f), CentreEasing.Stable(640 - 320 - 40, BeatTime(0.7f)));
                    ce2.Insert(BeatTime(1), CentreEasing.FromRight(430, BeatTime(1)));
                    Line l1 = new(ce1.GetResult(), ValueEasing.Stable(90));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l1);
                    CreateEntity(l2);
                    DelayBeat(4, () => { l1.Dispose(); l2.Dispose(); });
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(8), ValueEasing.EaseInQuart(0, 360 * 2, BeatTime(8)));
                    ve.Run((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                        Heart.InstantSetRotation(s);
                    });
                    DelayBeat(8.01f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        Heart.InstantSetRotation(0);
                    });
                });
                RegisterFunctionOnce("Change1", () =>
                {
                    UISettings.RemoveUISurface();
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(20, BeatTime(0.5f));
                });
                CreateChart(0, BeatTime(1), 6f, new string[]
                {
                    "StartLine","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "(R'0.75)(+01'0.75)(RightC)","","","",   "","","","",
                    "(R'0.9)(+01'0.9)(LeftC)","","","",   "","","","",
                    "","","","",   "(^R'1.2)(^+01'1.2)(RightC)","","","",
                    "","","","",   "","","","",

                    "(#6.9#$2)(#6.9#$01)(SizeExpend)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "(^$3'1.5)(^+01'1.5)","(Return)","","", "","","","",
                    //
                    "","","","",   "","","","",
                    "(R)(LeftC)(CameFromLeftLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+0'1.2","","+0'1.2","",

                    "+0'1.2","","","",   "","","","",
                    "(R)(RightC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",

                    "R","","","",   "","","","",
                    "(R)(RightC)(CameFromRightLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+0'1.2","","+0'1.2","",

                    "+0'1.2","","","",   "R","","","",
                    "(R)(LeftC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",
                    //
                    "(R)(RightC2)","","","",   "(+0)(LeftC2)","","","",
                    "R(CameFromDoubleLine)","","","",   "","","","",
                    "R","","","",   "","","","",
                    "!!3/6","R'1.2","","+0'1.2","","+0'1.2","",

                    "+0'1.2","","","",   "","","","",
                    "(R)(RightC)","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",

                    "R","","","",   "","","","",
                    "(R)(LeftC2)(Line2)","","","",   "(+0)(RightC2)","","","",
                    "","","","",   "","","","",
                    "R","","","",   "R","","^+0'1.5","",

                    "+0","","","",   "R","","","",
                    "(R)(RightC)(DoubleLine)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "(R)(LeftC3)","","","",   "","","","",
                    "(R)(LeftC3)","","","",   "","","","",
                    "!!3/6","(R'1.2)(LeftC3)","","+0'1.2","","+0'1.2","",
                    "(+0'1.2)(LeftC3)","","","",   "R","","","",

                    "(R)(LeftC3)","","","",   "","","","",
                    "(R)(LeftC3)","","","",   "R","","","",
                    "(R)(LeftC3)","","","",   "","","","",
                    "(R)(LeftC3)","","","",   "","","","",

                    "(R)(RightC3)(Line1)","","","",   "(+0)","","","",
                    "(R)(RightC3)","","","",   "","","","",
                    "!!3/6","(R'1.2)(RightC3)","","+0'1.2","","+0'1.2","",
                    "(+0'1.2)(RightC3)","","","",   "R","","","",

                    "(R)(RightC3)","","","",   "","","","",
                    "(R)(RightC3)","","","",   "(R)","","","",
                    "(R)(RightC3)","","","",   "","","","",
                    "(R)(RightC3)","","","",   "(+0)","","","",
                    //
                    "(R)(LeftC3)","","","",   "","","","",
                    "(R)(LeftC3)","","","",   "","","","",
                    "!!3/6","(R'1.2)(LeftC3)","","+0'1.2","","+0'1.2","",
                    "(+0'1.2)(LeftC3)","","","",   "","","","",

                    "(R)(LeftC3)","","","",   "","","","",
                    "(R)(LeftC3)","","","",   "R","","","",
                    "(R)(LeftC3)","","","",   "","","","",
                    "(R)(LeftC3)","","","",   "","","","",

                    "(^$1'1.2)(^$11'1.2)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "R","","","",   "R","","","",
                    "R","","","",   "R","","","",
                    "R","","","",   "R","","","",
                    "","","","",   "Change1","","","",
                    //
                });
            }
            void Npart2()
            {
                RegisterFunctionOnce("Line", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("LineN", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(100));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    Line l1 = new(CentreEasing.Stable(LastRand + 50, 240), ValueEasing.Stable(100));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("LineM", () =>
                {
                    Line l = new(CentreEasing.Stable(Rand(-20, 660), 240), ValueEasing.Stable(80));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    Line l1 = new(CentreEasing.Stable(LastRand - 50, 240), ValueEasing.Stable(80));
                    CreateEntity(l1);
                    l1.AlphaDecrease(BeatTime(1));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(0, CentreEasing.Stable(-60, 240));
                    ve.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(-60, 240), new(700, 240), BeatTime(1)));
                    Line l = new(ve.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    LineShadow(1.5f, 0.75f, 5, l);
                });
                RegisterFunctionOnce("Line3", () =>
                {
                    CentreEasing.EaseBuilder ve = new();
                    ve.Insert(0, CentreEasing.Stable(700, 240));
                    ve.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(700, 240), new(-60, 240), BeatTime(1)));
                    Line l = new(ve.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(1));
                    LineShadow(1.5f, 0.75f, 5, l);
                });
                RegisterFunctionOnce("Change1", () =>
                {
                    UISettings.RemoveUISurface();
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(-20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change3", () =>
                {

                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    Heart.GiveForce(180, 0);
                    RotateTo(180 + 20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change4", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(1, 0, BeatTime(0.5f)));
                    ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutBack(0, 1.05f, BeatTime(0.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(180 - 20, BeatTime(0.5f));
                });
                RegisterFunctionOnce("Change5", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(0));
                    ve.Insert(BeatTime(0.02f), ValueEasing.Linear(0, 4, BeatTime(0.02f)));
                    ve.Insert(BeatTime(8f), ValueEasing.EaseOutSine(4, 1, BeatTime(8)));
                    ve.Run((s) => { ScreenScale = s; });
                    RotateTo(0, BeatTime(0.5f));
                    Heart.GiveForce(0, 0);
                });
                RegisterFunctionOnce("NeverGonnaGiveYouUp", () =>
                {
                    Arrow b = MakeArrow(BeatTime(8), 1, 3, 0, 0);
                    Arrow r = MakeArrow(BeatTime(8), 1, 3, 1, 0);
                    CreateEntity(b);
                    CreateEntity(r);
                    DelayBeat(3.8f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(5.3f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(6.8f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(8.3f + 4, () =>
                    {
                        b.Delay(BeatTime(1.2f));
                        r.Delay(BeatTime(1.2f));
                    });
                    DelayBeat(9.3f + 4, () =>
                    {
                        b.Delay(BeatTime(1.7f));
                        r.Delay(BeatTime(1.7f));
                    });
                    DelayBeat(10.8f + 4, () =>
                    {
                        b.ResetColor(1);
                        r.ResetColor(0);
                    });
                });
                RegisterFunctionOnce("ShineLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(16), CentreEasing.Alternate
                        (
                       BeatTime(0.5f),
                        new Func<ICustomMotion, Vector2>[] {

                            CentreEasing.Stable(640 / 5 * 1, 240),
                            CentreEasing.Stable(640 / 5 * 2, 240),
                            CentreEasing.Stable(640 / 5 * 3, 240),
                            CentreEasing.Stable(640 / 5 * 4, 240),
                        }
                        ));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.5f };
                    CreateEntity(l);
                    DelayBeat(10, () => { l.AlphaDecrease(BeatTime(4.5f)); });
                });
                RegisterFunctionOnce("RickLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 240));
                    ce.Insert(BeatTime(8), CentreEasing.FromLeft(278, BeatTime(8)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(278, 240), new(220, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(220, 240), new(150, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuad(new(150, 240), new(70, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseInQuart(new(70, 240), new(70 + 150, 240), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutQuart(new(70 + 150, 240), new(370, 240), BeatTime(0.75f)));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(8), ValueEasing.Stable(90));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(90, 105, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(105, 80, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutCubic(80, 90, BeatTime(1.5f)));
                    Line l = new(ce.GetResult(), ve.GetResult());
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    DelayBeat(12, () => { l.AlphaDecrease(BeatTime(2)); });
                    LineShadow(4f, 0.5f, 5, l);
                });
                CreateChart(BeatTime(3), BeatTime(1), 6.2f, new string[]
                {
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "(Change2)","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "(Change3)","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "(Change4)","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "(Change5)","","","",
                    //
                    "(NeverGonnaGiveYouUp)(ShineLine)(RickLine)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    
                });
                CreateChart(BeatTime(3), BeatTime(1), 6.2f, new string[]
                {
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "R1(Line)","","","",    "","","","",
                    "","","","",   "R1(Line)","","","",
                    "","","","",   "R1(Line)","","","",
                    "","","","",   "","","","",
                    "^R1'1.35(Line2)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "R1(LineN)","","","",    "","","","",
                    "","","","",   "R1(LineN)","","","",
                    "","","","",   "R1(LineN)","","","",
                    "","","","",   "","","","",
                    "^R1'1.35(Line3)","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "R1(LineM)","","","",    "","","","",
                    "","","","",   "R1(LineM)","","","",
                    "","","","",   "R1(LineM)","","","",
                    "","","","",   "","","","",
                    "^R1'1.35(Line3)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "R1(LineN)","","","",    "","","","",
                    "","","","",   "R1(LineN)","","","",
                    "","","","",   "R1(LineN)","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void Npart3()
            {
                Player.Heart main = Heart;
                Player.Heart extra;
                extra = Heart.InstantSplit(new CollideRect(-100, 240, 84, 84));
                RegisterFunctionOnce("MoveRight", () =>
                {

                    SetPlayerBoxMission(extra);
                    SetSoul(1);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(-100, 240));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(-100, 240), new(200, 240), BeatTime(2f)));
                    ce.Insert(BeatTime(0.66f), CentreEasing.Stable(200, 240));
                    ce.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(200, 240), new(-100, 240), BeatTime(1.33f)));
                    ce.Run((s) => { SetPlayerBoxMission(extra); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(320, 240));
                    ce2.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(420, 240), BeatTime(2f)));
                    ce2.Insert(BeatTime(0.66f), CentreEasing.Stable(420, 240));
                    ce2.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(420, 240), new(320, 240), BeatTime(1.33f)));
                    ce2.Run((s) => { SetPlayerBoxMission(main); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);
                });
                RegisterFunctionOnce("MoveLeft", () =>
                {
                    SetPlayerBoxMission(extra);
                    SetSoul(1);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(740, 240));
                    ce.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(740, 240), new(440, 240), BeatTime(2f)));
                    ce.Insert(BeatTime(0.66f), CentreEasing.Stable(440, 240));
                    ce.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(440, 240), new(740, 240), BeatTime(1.33f)));
                    ce.Run((s) => { SetPlayerBoxMission(extra); InstantSetBox(s, 84, 84); InstantTP(s); });
                    DelayBeat(16, () => { SetPlayerBoxMission(extra); InstantSetBox(4000, 84, 84); InstantTP(4000, -11451); SetPlayerBoxMission(main); });
                    SetPlayerBoxMission(main);
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(320, 240));
                    ce2.Insert(BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(220, 240), BeatTime(2f)));
                    ce2.Insert(BeatTime(0.66f), CentreEasing.Stable(220, 240));
                    ce2.Insert(BeatTime(1.33f), CentreEasing.EaseOutQuad(new(220, 240), new(320, 240), BeatTime(1.33f)));
                    ce2.Run((s) => { SetPlayerBoxMission(main); InstantSetBox(s, 84, 84); InstantTP(s); });
                    SetPlayerBoxMission(main);

                });
                RegisterFunctionOnce("NeverGonnaGiveYouUp", () =>
                {
                    Arrow b = MakeArrow(BeatTime(4), 1, 6.5f * 1.7f, 0, 0);
                    Arrow r = MakeArrow(BeatTime(4), 1, 6.5f * 1.7f, 1, 0);
                    CreateEntity(b);
                    CreateEntity(r);
                    DelayBeat(3.8f, () =>
                    {
                        b.ResetColor(1);
                        r.ResetColor(0);

                    });
                });
                RegisterFunctionOnce("SizeExpend", () =>
                {
                    SizeExpand(8, BeatTime(2f));
                });
                RegisterFunctionOnce("StartLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(360, 240));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(360, 240), new(-4, 240), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.TransverseMirror = true;
                    DelayBeat(1, () => { l.Dispose(); });
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(25, BeatTime(2f), true);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(25, BeatTime(2f), false);

                });
                RegisterFunctionOnce("SLeftC", () =>
                {
                    Convulse(8, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("SRightC", () =>
                {
                    Convulse(8, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    RotateTo(180, BeatTime(2.5f));
                    SetPlayerBoxMission(main);
                    Heart.FollowScreen(BeatTime(2.5f));
                });
                RegisterFunctionOnce("DoubleLine", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(Rand(50, 540), 90 + 15 * m);
                    Line l2 = new(LastRand + 50, 90 + 15 * m);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("DoubleLine2", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(new Vector2(320, Rand(50, 380)), 15 * m);
                    Line l2 = new(new Vector2(320, LastRand + 50), 15 * m);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("Return", () =>
                {
                    RotateTo(0, BeatTime(4.5f));
                    Heart.FollowScreen(BeatTime(4.5f));
                });
                RegisterFunctionOnce("LeftLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(5), CentreEasing.XSinWave(320 + 46, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(5, () => { l.Dispose(); });
                    LineShadow(1, 0.3f, 7, l);
                });
                RegisterFunctionOnce("RightLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(5), CentreEasing.XSinWave(320 + 46, BeatTime(8), 1));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(5, () => { l.Dispose(); });
                    LineShadow(1, 0.3f, 7, l);
                });
                RegisterFunctionOnce("|Line1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, 38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(2), CentreEasing.Stable(320 + 30, 480));
                    ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(-2, 0), new(-0.34f, 0f)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(8, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                });
                RegisterFunctionOnce("|Line2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, -38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.Stable(320 - 30, 480));
                    ce.Insert(BeatTime(3), CentreEasing.Accerlating(new(2, 0), new(0.34f, 0)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(7, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                });
                RegisterFunctionOnce("|Line3", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(1), ValueEasing.EaseOutSine(0, -38, BeatTime(1)));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(0), CentreEasing.Stable(320 - 30, 0));
                    ce.Insert(BeatTime(2), CentreEasing.EaseOutSine(new(290, 0), new(290, 130), BeatTime(2)));
                    ce.Insert(BeatTime(4), CentreEasing.EaseInSine(new(290, 130), new(290, -300), BeatTime(4)));
                    Line l = new(ce.GetResult(), ve.GetResult()); ;
                    CreateEntity(l);
                    DelayBeat(7, () => { l.Dispose(); });
                    LineShadow(3, 0.3f, 6, l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("XLine", () =>
                {
                    CentreEasing.EaseBuilder ce1 = new();
                    ce1.Insert(BeatTime(4), CentreEasing.Stable(320, 240));
                    ce1.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 1), new(0, 0.24f)));
                    ValueEasing.EaseBuilder ve1 = new(), ve2 = new();
                    ve1.Insert(BeatTime(3), ValueEasing.Stable(45));
                    ve1.Insert(BeatTime(2), ValueEasing.EaseOutSine(45, 0, BeatTime(2)));
                    ve2.Insert(BeatTime(3), ValueEasing.Stable(90 + 45));
                    ve2.Insert(BeatTime(2), ValueEasing.EaseOutSine(90 + 45, 90, BeatTime(2)));
                    Line l1 = new(ce1.GetResult(), ve1.GetResult()) { Alpha = 0 };
                    Line l2 = new(ce1.GetResult(), ve2.GetResult()) { Alpha = 0 };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaIncrease(BeatTime(2), 0.75f);
                    l2.AlphaIncrease(BeatTime(2), 0.75f);
                    DelayBeat(6, () =>
                    {
                        l1.AlphaDecrease(BeatTime(1.5f));
                        l2.AlphaDecrease(BeatTime(1.5f));
                    });
                });
                SetPlayerBoxMission(main);
                UISettings.CreateUISurface();
                DelayBeat(40, () => { UISettings.RemoveUISurface(); });
                DelayBeat(84, () => { UISettings.CreateUISurface(); });
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
                    "(SizeExpend)(StartLine)","","","",   "","","","",
                    "","","","",   "(R)(+01)","","","",
                    "","","","",   "","","","",
                    "(R)(+01)","","","",   "","","","",

                    "","","","",   "(R)(+01)","","","",
                    "","","","",   "","","","",
                    "(R)(+01)","","","",   "","","","",
                    "(R)(+01)","","","",   "","","","",

                    "(#0.75#R)(LeftC)(DoubleLine2)","","","",   "(+0)","","","",
                    "(+0)","","","",   "(#0.75#R)(LeftC)(DoubleLine2)","","","",
                    "(+0)","","","",   "(+0)","","","",
                    "(#0.75#R)(LeftC)(DoubleLine2)","","","",   "(+0)","","","",

                    "(+0)","","","",   "(#0.75#R)(LeftC)(DoubleLine2)","","","",
                    "(+0)","","","",   "(+0)","","","",
                    "(#0.75#R)(LeftC)(DoubleLine2)","","","",   "(+0)","","","",
                    "(+0)","","","",   "","","","",
                    //
                    "(#0.75#R1)(RightC)(DoubleLine)","","","",   "(+01)","","","",
                    "(+01)","","","",   "(#0.75#$R1)(RightC)(DoubleLine)","","","",
                    "(+01)","","","",   "(+01)","","","",
                    "(#0.75#R1)(RightC)(DoubleLine)","","","",   "(+01)","","","",

                    "+01","","","",   "(#0.75#R1)(RightC)(DoubleLine)","","","",
                    "+01","","","",   "+01","","","",
                    "(#0.75#R1)(RightC)(DoubleLine)","","","",   "(+01)","","","",
                    "(+01)","","","",   "","","","",

                    "(#0.75#R)(LeftC)","","","",   "","","","",
                    "","","","",   "D1","","","",
                    "(#0.75#+01)(RightC)","","","",   "","","","",
                    "","","","",   "D","","","",

                    "(#0.8#+0)(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(D)(SRightC)(|Line1)","","","",   "","","","",
                    "(D)(SLeftC)(|Line2)","","","",   "","","","",
                    //
                    "N0","(MoveRight)(LeftLine)","","",   "+0","","","",
                    "+0","","","",   "+0","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "N2","(MoveLeft)(RightLine)","","",   "+0","","","",
                    "+0","","","",   "+0","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "N0(|Line3)","","","",   "+0","","","",
                    "+0","","","",   "N0","","","",
                    "+0","","","",   "+0","","","",
                    "R(Rotate)","","","",   "","","","",

                    "R(NeverGonnaGiveYouUp)","","","",   "","","","",
                    "","","","",   "","","","",
                    "($0'1.7)($01'1.7)(SLeftC)","","","",   "","","","",
                    "($21'1.7)($21'1.7)(SRightC)","","","",   "","","","",
                    //
                    "(SizeExpend)(StartLine)","","","",   "","","","",
                    "","","","",   "(R)(+01)","","","",
                    "","","","",   "","","","",
                    "(R)(+01)","","","",   "","","","",

                    "","","","",   "(R)(+01)","","","",
                    "","","","",   "","","","",
                    "(R)(+01)","","","",   "","","","",
                    "(R)(+01)","","","",   "","","","",

                    "(#0.75#R)(LeftC)(DoubleLine2)","","","",   "(+0)","","","",
                    "(+0)","","","",   "(#0.75#R)(LeftC)(DoubleLine2)","","","",
                    "(+0)","","","",   "(+0)","","","",
                    "(#0.75#R)(LeftC)(DoubleLine2)","","","",   "(+0)","","","",

                    "(+0)","","","",   "(#0.75#R)(LeftC)(DoubleLine2)","","","",
                    "(+0)","","","",   "(+0)","","","",
                    "(#0.75#R)(LeftC)(DoubleLine2)","","","",   "(+0)","","","",
                    "(+0)","","","",   "","","","",
                    //
                    "(#0.75#R1)(RightC)(DoubleLine)","","","",   "(+01)","","","",
                    "(+01)","","","",   "(#0.75#$R1)(RightC)(DoubleLine)","","","",
                    "(+01)","","","",   "(+01)","","","",
                    "(#0.75#R1)(RightC)(DoubleLine)","","","",   "(+01)","","","",

                    "+01","","","",   "(#0.75#R1)(RightC)(DoubleLine)","","","",
                    "+01","","","",   "+01","","","",
                    "(#0.75#R1)(RightC)(DoubleLine)","","","",   "(+01)","","","",
                    "(+01)","","","",   "","","","",

                    "(R)","","","",   "","","","",
                    "(R)","","","",   "(R)(Return)(XLine)","","","",
                    "(R)","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "R","","","",   "","","","",
                    "R","","","",   "","","","",
                    "R","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
                SetPlayerBoxMission(extra);
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "$3","","","",
                    "$3","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "$1","","","",
                    "$1","","","",   "$1","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void Npart4()
            {
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(BoxStates.Right, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromRight(200, BeatTime(2f)));

                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(2f));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(BoxStates.Left, 0));
                    ce.Insert(BeatTime(2), CentreEasing.FromLeft(200, BeatTime(2f)));

                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(2f));
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(25, BeatTime(2f), true);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(25, BeatTime(2f), false);

                });
                RegisterFunctionOnce("SLeftC", () =>
                {
                    Convulse(6, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("SRightC", () =>
                {
                    Convulse(6, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 3, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(3, -3, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-3, 3, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(3, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Flicker", () => { MakeFlicker(Color.White * 0.4f); });
                RegisterFunctionOnce("BFlicker", () => { MakeFlicker(Color.White); });
                RegisterFunctionOnce("ScreenL", () =>
                {
                    RotateWithBack(BeatTime(4), 4);
                });
                RegisterFunctionOnce("ScreenR", () =>
                {
                    RotateWithBack(BeatTime(4), -4);
                });
                RegisterFunctionOnce("ScreenL2", () =>
                {
                    RotateWithBack(BeatTime(8), 6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 1));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenR2", () =>
                {
                    RotateWithBack(BeatTime(8), -6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 1));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenH", () =>
                {
                    RotateSymmetricBack(BeatTime(8), 6);
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 1));
                    ce.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 0));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    DelayBeat(4, () => { l.AlphaDecrease(BeatTime(6)); });
                    CentreEasing.EaseBuilder ce2 = new();
                    ce2.Insert(0, CentreEasing.Stable(0, 0));
                    ce2.Insert(BeatTime(4), CentreEasing.XSinWave(400, BeatTime(8), 0));
                    ce2.Insert(BeatTime(4), CentreEasing.XSinWave(600, BeatTime(8), 1));
                    Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l2);
                    DelayBeat(4, () => { l2.AlphaDecrease(BeatTime(6)); });
                });
                RegisterFunctionOnce("ScreenExpand", () =>
                {
                    SizeExpand(8, BeatTime(4));
                });
                RegisterFunctionOnce("SuddenRotateL", () =>
                {
                    Rotate(6, BeatTime(0.125f));
                    DelayBeat(0.125f, () => { Rotate(-6, BeatTime(1.375f)); });
                });
                RegisterFunctionOnce("SuddenRotateR", () =>
                {
                    Rotate(-6, BeatTime(0.125f));
                    DelayBeat(0.125f, () => { Rotate(6, BeatTime(1.375f)); });
                });
                RegisterFunctionOnce("Over", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    LerpScreenScale(BeatTime(2), 1.2f, 0.06f);
                    DelayBeat(4, () => { LerpScreenScale(BeatTime(4), 1, 0.1f); });
                });
                RegisterFunctionOnce("Rotate", () =>
                {
                    RotateTo(30, BeatTime(4));
                });
                CreateChart(BeatTime(4), BeatTime(1), 6f, new string[]
                {
#region part
                    "R","","","",   "","","","",
                    "","","","",   "R","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(R)(Line1)(ScreenL)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(R)(Line2)(ScreenR)","","","",   "","","","",
                    "","","","",   "(R)","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(D)(Line1)(Line2)(ScreenExpand)","","","",   "+0","","","",
                    "+0","","","",   "+0","","","",
                    "D","","","",   "+1","","","",
                    "+1","","","",   "+1","","","",
                    //
                    "($2)($2)(SLeftC)","","","",   "($0)($0)(SLeftC)","","","",
                    "","","","",   "($1)($1)(SRightC)","","","",
                    "($3)($3)(SRightC)","","","",   "","","","",
                    "","","","",   "R","","","",

                    "(R)","","","",   "(R)","","","",
                    "!!3/6","$21","$0","$21","$0","","",
                    "(R)(+01)(SLeftC)","","","",   "(R)(+01)(SLeftC)","","","",
                    "(R)(+01)(SRightC)","","","",   "(R)(+01)(SRightC)","","","",

                    "R'1.2(Shake)(Flicker)","","+01'1.2(Flicker)","",   "+0'1.2(Flicker)","","+01'1.2(Flicker)","",
                    "+0'1.2(Flicker)","","+01'1.2(Flicker)","",   "+0'1.2(Flicker)","","+01'1.2(Flicker)","",
                    "+0'1.2(Flicker)","","+01'1.2(Flicker)","",   "+0'1.2(Flicker)","","+01'1.2(Flicker)","",
                    "+0'1.2(Flicker)","","+01'1.2(Flicker)","",   "+0'1.2(Flicker)","","+01'1.2(Flicker)","",

                    "($3'1.5)($31'1.5)(BFlicker)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    #endregion
#region part
                    "(D)(ScreenL2)","","","",   "","","","",
                    "(D)","","","",   "","","","",
                    "!!3/6","N1","","+0","","+0","",
                    "+0","","","",   "","","","",

                    "(^R'1.3)","","","",   "","","","",
                    "","","","",   "(R)","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(D)(ScreenR2)","","","",   "","","","",
                    "(D)","","","",   "","","","",
                    "!!3/6","N1","","+0","","+0","",
                    "+0","","","",   "","","","",

                    "(^R'1.3)","","","",   "","","","",
                    "","","","",   "(R)","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",
                    //
                    "(D)(ScreenH)","","","",   "","","","",
                    "(D)","","","",   "","","","",
                    "!!3/6","N1","","+0","","+0","",
                    "+0","","","",   "","","","",

                    "(^R'1.3)","","","",   "","","","",
                    "","","","",   "(R)","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",

                    "(D)","","","",   "(D)","","","",
                    "","","","",   "","","","",
                    "(^N3'1.15)","","","",   "","","","",
                    "(^N3'1.15)","","","",   "","","","",

                    "(^$3'1.3)(SuddenRotateL)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    #endregion
#region part
                    "(R)","","","",   "","","","",
                    "(R)","","","",   "","","","",
                    "!!6/3","(R)(SuddenRotateR)","","","(R)","",
                    "($3)","","","",   "(R)","","","",

                    "(R)","","","",   "","","","",
                    "R","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "","","","",   "(R)(SuddenRotateL)","","","",

                    "","","","",   "","","","",
                    "","","","",   "(R)","","","",
                    "","","","",   "!!3/6","(R1'1.1)(+2'1.1)(SuddenRotateR)","","(+21'1.1)(+2'1.1)",
                    "","(+21'1.1)(+2'1.1)","",  "(+21'1.1)(+2'1.1)","","","",
                    "","","","",   "(+21'1.6)(+2'1.6)","","","",
                    "","","","",   "R","","","",
                    "(R)","","","",   "","","","",
                    "","","","",   "(R)(SuddenRotateL)","","","",
                    "(R)","","","",   "(R)","","","",
                    //
                    "R","","","",   "(R)","","","",
                    "R","","","",   "(R)","","","",
                    "R","","","",   "(R)","","","",
                    "R","","","",   "$0'1.2(Shake)(Flicker)","","$0'1.2(Flicker)","",

                    "$0'1.2(Flicker)","","$01'1.2(Flicker)","",   "$0'1.2(Flicker)","","$01'1.2(Flicker)","",
                    "$0'1.2(Flicker)","","$01'1.2(Flicker)","",
                    "($0'1.35)($0'1.35)(Flicker)","","($01'1.35)($01'1.35)(Flicker)","",   "($0'1.35)($0'1.35)(Flicker)","","($01'1.35)($01'1.35)(Flicker)","",
                    "($0'1.35)($0'1.35)(Flicker)","","($01'1.35)($01'1.35)(Flicker)","",   "($0'1.35)($0'1.35)(Flicker)","","($01'1.35)($01'1.35)(Flicker)","",

                    "($0'1.35)($0'1.35)(BFlicker)(Over)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "Rotate","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
#endregion
                });
            }
            void Npart5()
            {
                DelayBeat(4, () => { UISettings.CreateUISurface(); });

                RegisterFunctionOnce("DoubleLine", () =>
                {
                    int m = Rand(0, 1) == 0 ? 1 : -1;
                    Line l1 = new(Rand(50, 540), 90 + 15 * m) { Alpha = 0.6f };
                    Line l2 = new(LastRand + 50, 90 + 15 * m) { Alpha = 0.6f };
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l1.AlphaDecrease(BeatTime(1.5f));
                    l2.AlphaDecrease(BeatTime(1.5f));
                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    RotateTo(-6, BeatTime(2));

                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    RotateTo(6, BeatTime(2));

                });
                RegisterFunctionOnce("SRotateL", () =>
                {
                    RotateTo(-2, BeatTime(1));


                });
                RegisterFunctionOnce("SRotateR", () =>
                {
                    RotateTo(2, BeatTime(1));

                });
                RegisterFunctionOnce("BRotateL", () =>
                {
                    RotateTo(-6, BeatTime(1));


                });
                RegisterFunctionOnce("BRotateR", () =>
                {
                    RotateTo(6, BeatTime(1));

                });
                RegisterFunctionOnce("Return", () =>
                {
                    RotateTo(0, BeatTime(1));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(1, 1.3f, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    DelayBeat(1.501f, () => { ScreenScale = 1.15f; });
                    DelayBeat(2.501f, () => { ScreenScale = 1f; });
                });
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Line", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 480));
                    ce.Insert(BeatTime(7), CentreEasing.Accerlating(new(0, -80), new(0, -0.1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(0)) { Alpha = 0.75f };
                    CreateEntity(l);
                    LineShadow(4, 0.75f, 4, l);
                });
                RegisterFunctionOnce("UpLine", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 600));
                    ce.Insert(BeatTime(7), CentreEasing.Accerlating(new(0, -100), new(0, -0.1f)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(40)) { Alpha = 0.75f };
                    CreateEntity(l);
                    l.TransverseMirror = true;
                });
                RegisterFunctionOnce("Camera1", () =>
                {
                    ScreenDrawing.ScreenAngle = 30;
                    ScreenScale = 0.9f;
                    ScreenPositionDelta = new(10, 20);
                });
                RegisterFunctionOnce("Camera2", () =>
                {
                    ScreenDrawing.ScreenAngle = -24;
                    ScreenScale = 0.8f;
                    ScreenPositionDelta = new(10, -20);
                });
                RegisterFunctionOnce("Camera3", () =>
                {
                    //ScreenDrawing.ScreenAngle = 0;
                    //ScreenScale = 2.5f;
                    DelayBeat(0, () => { ScreenPositionDelta = Vector2.Zero; });
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(1, ValueEasing.Stable(2.5f));
                    ve.Insert(BeatTime(2.5f), ValueEasing.EaseInQuad(2.5f, 1f, BeatTime(2.5f)));
                    ve.Run((s) => { ScreenScale = s; });
                    Heart.InstantSetRotation(0);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.4f, new string[]
                {
                    "(R)(DoubleLine)(RotateL)","","","",   "(R1)","","","",
                    "(R)(DoubleLine)","","","",   "(R1)","","","",
                    "(R)","","","",   "(R1)","","","",
                    "(R)","","","",   "(R1)","","","",

                    "","","","",   "(R1)","","","",
                    "(R)","","","",   "(R1)","","","",
                    "(R)","","","",   "R1","","","",
                    "R","","","",   "(R1)","","","",

                    "(R)(DoubleLine)(RotateR)","","","",   "(R1)","","","",
                    "(R)(DoubleLine)","","","",   "(R1)","","","",
                    "(R)","","","",   "(R1)","","","",
                    "(R)","","","",   "(R1)","","","",

                    "","","","",   "(R1)","","","",
                    "(R)","","","",   "(R1)","","","",
                    "(R)","","","",   "R1","","","",
                    "(R)","","","",   "(R1)","","","",
                    //
                    "(R)(DoubleLine)(RotateL)","","","",   "(R)","","","",
                    "!!3",">R'1.2",">+21'1.2",">+2'1.2",   ">+21'1.2","","","",
                    "(R)(DoubleLine)(RotateR)","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",

                    "(DoubleLine)(RotateL)","","","",   "(R)","","","",
                    "!!3","<R1'1.2","<+2'1.2","<+21'1.2",   "<+2'1.2","","","",
                    "(R)(DoubleLine)(RotateR)","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",

                    "(R)(+0)(DoubleLine)(SRotateL)","","","",   "(R1)(+01)","","","",
                    "(R)(+0)(DoubleLine)(SRotateR)","","","",   "(R1)(+01)","","","",
                    "(R)(+0)(DoubleLine)(SRotateL)","","","",   "(R1)(+01)","","","",
                    "(R)(+0)(DoubleLine)(SRotateR)","","","",   "(R1)(+01)","","","",

                    "(R)(+0)(DoubleLine)(BRotateL)","","","",   "(R1)(+01)","","","",
                    "(R)(+0)(DoubleLine)(BRotateR)","","","",   "(R1)(+01)","","","",
                    "(R)(+0)(DoubleLine)(BRotateL)","","","",   "(R1)(+01)","","","",
                    "(R)(+0)(DoubleLine)(BRotateR)","","","",   "(R1)(+01)","","","",
                    //
                    "(R)(+0)(Return)","","","",   "","","","",
                    "","","","",   "($01)($2)","","","",
                    "","","","",   "($21)($0)","","","",
                    "","","","",   "","","","",
                    "(UpLine)(Camera1)","","","",   "","","","",
                    "","","","",   "(UpLine)(Camera2)","","","",
                    "","","","",   "","","","",

                    "(#4#$31)(>$311'1.2)(Shake)(Line)(Camera3)","","(<$311'1.2)(Line)","",   "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",
                    "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",   "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",
                    "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",   "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",
                    "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",   "(>$311'1.2)(Line)","","(<$311'1.2)(Line)","",
                    "(>$311'1.2)(Line)","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                });
            }
            void Npart6()
            {
                RegisterFunctionOnce("Flicker", () => { MakeFlicker(Color.White * 0.4f); });
                RegisterFunctionOnce("BFlicker", () => { MakeFlicker(Color.White); });
                RegisterFunctionOnce("Shake1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Shake2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, -4, BeatTime(0.25f)));
                    for (int a = 0; a < 2; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("LeftC", () =>
                {
                    Convulse(8, BeatTime(1.5f), false);

                });
                RegisterFunctionOnce("RightC", () =>
                {
                    Convulse(8, BeatTime(1.5f), true);

                });
                RegisterFunctionOnce("RotateBack", () =>
                {
                    RotateTo(0f, BeatTime(2.3f));
                });
                RegisterFunctionOnce("Rotate1", () =>
                {
                    Rotate(-180, BeatTime(2.5f));
                    Heart.FollowScreen(BeatTime(2.5f));

                });
                RegisterFunctionOnce("RotateL", () =>
                {
                    RotateTo(7, BeatTime(1.2f));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(0, 0), new(800, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RotateR", () =>
                {
                    RotateTo(-7, BeatTime(1.2f));
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuad(new(640, 0), new(-200, 0), BeatTime(1)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                });
                RegisterFunctionOnce("RotateBack2", () =>
                {
                    RotateTo(0, BeatTime(1.2f));
                });
                RegisterFunctionOnce("Convulse", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);
                    DelayBeat(0.5f, () => { Convulse(4, BeatTime(0.5f), true); });
                });
                RegisterFunctionOnce("Expand", () =>
                {
                    SizeExpand(12, BeatTime(4));
                });
                RegisterFunctionOnce("HeartShine", () =>
                {
                    SetSoul(1);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.2f, new string[]
                {
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "D(Rotate1)","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    //
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "$3'1.5(Rotate1)","","$3'1.5","","$3'1.5","","$3'1.5","",
                    "$3'1.5","","$3'1.5","",   "$3'1.5","","$3'1.5","",
                    "$3'1.5","","","", "","","","",
                    "","","","",   "","","","",

                    "D","","","",   "D","","RotateL","",
                    "!!3","<$0'1.3","<$21'1.3","<$0'1.3",   "<$21'1.3","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",   
                    //
                    "D","","","",   "D","","RotateR","",
                    "!!3",">$01'1.3",">$2'1.3",">$01'1.3",   ">$2'1.3","","","",
                    "D","","","",   "D","","","RotateBack2",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",

                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",
                    "D","","","",   "D","","","",


                });
                CreateChart(BeatTime(4), BeatTime(1), 6.4f, new string[]
                {
                    "","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    //
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    //
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",

                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",
                    "(LeftC)","","","",   "","","","",

                    "($01)(Convulse)","","","",   "($21)","","","",
                    "($01)(Convulse)","","","",   "($21)","","","",
                    "($01)(Convulse)","","","",   "($21)","","","",
                    "($01)(Convulse)","","","",   "($21)","","","",
                    //
                    "($0)($0)(Convulse)","","","",   "($2)($2)","","","",
                    "($0)($0)(Convulse)","","","",   "($2)($2)","","","",
                    "($0)($0)(Convulse)","","","",   "($2)($2)","","","",
                    "($0)($0)(Convulse)","","","",   "($2)($2)","","","",

                    "(R'1.3)(Shake1)(Flicker)","","+0(Flicker)","",   "(+0'1.3)(Flicker)","","+0(Flicker)","",
                    "(+0'1.3)(Flicker)","","+0(Flicker)","",   "(+0'1.3)(Flicker)","","","",
                    "(R1'1.3)(Shake2)(Flicker)","","+01(Flicker)","",   "(+01'1.3)(Flicker)","","+01(Flicker)","",
                    "(+01'1.3)(Flicker)","","+01(Flicker)","",   "(+01'1.3)(Flicker)","","","",
                    "(R)(+01)(BFlicker)(Expand)","","","",   "","","","",
                    "","","","",   "","","","",
                    "(R)(+01)(BFlicker)(HeartShine)","","","",   "","","","",
                    "","","","",   "","","","",
                });
                float offset = 0;
                ValueEasing.EaseBuilder ve = new();
                ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 8, BeatTime(2)));
                ve.Run((s) =>
                {
                    offset = s;
                });
                CentreEasing.EaseBuilder ce1 = new();
                ce1.Insert(0, CentreEasing.Stable(320, 240));
                ce1.Insert(BeatTime(2), CentreEasing.EaseInSine(new(320, 240), new(320, 240 + 8), BeatTime(2)));
                CentreEasing.EaseBuilder ce2 = new();
                ce2.Insert(0, CentreEasing.Stable(320, 240));
                ce2.Insert(BeatTime(2), CentreEasing.EaseInSine(new(320, 240), new(320, 240 - 8), BeatTime(2)));
                Line l1 = new(ce1.GetResult(), ValueEasing.Stable(0)) { Alpha = 0f };
                Line l2 = new(ce2.GetResult(), ValueEasing.Stable(0)) { Alpha = 0f };
                l1.AlphaIncrease(BeatTime(2), 0.2f);
                l2.AlphaIncrease(BeatTime(2), 0.2f);
                CreateEntity(l1);
                CreateEntity(l2);
                ForBeat(0, 16, () =>
                {
                    Arrow[] s = GetAll<Arrow>();
                    for (int a = 0; a < s.Length; a++)
                    {
                        int x = a;
                        if (s[x].ArrowColor == 1)
                        {
                            if (s[x].Way == 0) s[x].Offset = new(0, -offset);
                            //if (s[x].Way == 1) s[x].Offset = new(-offset, 0);
                            if (s[x].Way == 2) s[x].Offset = new(0, -offset);
                            //if (s[x].Way == 3) s[x].Offset = new(offset, 0);
                        }
                        if (s[x].ArrowColor == 0)
                        {
                            if (s[x].Way == 0) s[x].Offset = new(0, offset);
                            //if (s[x].Way == 1) s[x].Offset = new(offset, 0);
                            if (s[x].Way == 2) s[x].Offset = new(0, offset);
                            //if (s[x].Way == 3) s[x].Offset = new(-offset, 0);
                        }
                    }
                });
                DelayBeat(60, () =>
                {
                    l1.AlphaDecrease(BeatTime(2));
                    l2.AlphaDecrease(BeatTime(2));
                });
                UISettings.RemoveUISurface();

            }
            void Npart7()
            {
                UISettings.CreateUISurface();
                RegisterFunctionOnce("Shake", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 4, BeatTime(0.25f)));
                    for (int a = 0; a < 7; a++)
                    {
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, -4, BeatTime(0.25f)));
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-4, 4, BeatTime(0.25f)));
                    }
                    ve.Insert(BeatTime(0.25f), ValueEasing.Linear(4, 0, BeatTime(0.25f)));
                    DelayBeat(6.1f, () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                    });
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("Line1", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(0, 0));
                    ce.Insert(BeatTime(4), CentreEasing.EaseOutQuart(new(0, 0), new(440, 0), BeatTime(4)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(4));
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(0, CentreEasing.Stable(640, 0));
                    ce.Insert(BeatTime(4), CentreEasing.EaseOutQuart(new(640, 0), new(200, 0), BeatTime(4)));
                    Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                    CreateEntity(l);
                    l.AlphaDecrease(BeatTime(4));
                });
                RegisterFunctionOnce("RotL1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(3f), ValueEasing.EaseOutBack(0, 5, BeatTime(3)));
                    ve.Insert(BeatTime(1f), ValueEasing.Stable(5));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(5, 13, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(13, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("RotR1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(3f), ValueEasing.EaseOutBack(0, -5, BeatTime(3)));
                    ve.Insert(BeatTime(1f), ValueEasing.Stable(-5));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuad(-5, -13, BeatTime(1.5f)));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseOutSine(-13, 0, BeatTime(1.5f)));
                    ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                });
                RegisterFunctionOnce("ScreenScale1", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(1.25f));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseInQuart(1.25f, 1, BeatTime(2f)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("ScreenScale2", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(BeatTime(0), ValueEasing.Stable(1.45f));
                    ve.Insert(BeatTime(1.5f), ValueEasing.EaseInQuart(1.45f, 1, BeatTime(2f)));
                });
                RegisterFunctionOnce("LeftC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), false);

                });
                RegisterFunctionOnce("RightC2", () =>
                {
                    Convulse(4, BeatTime(0.5f), true);

                });
                RegisterFunctionOnce("ScreenScale", () =>
                {
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(1.12f));
                    ve.Insert(BeatTime(4), ValueEasing.EaseOutSine(1.12f, 1.5f, BeatTime(4)));
                    ve.Run((s) => { ScreenScale = s; });
                });
                RegisterFunctionOnce("Over", () =>
                {
                    LerpScreenScale(BeatTime(4), 1, 0.05f);
                });
                RegisterFunctionOnce("Scale", () =>
                {
                    LerpScreenScale(BeatTime(4), 1.12f, 0.05f);
                });
                CreateChart(BeatTime(4), BeatTime(1), 6.35f, new string[]
                {
                    "(R)(+01)","","","",   "(+2)(+01)","","","",
                    "","","","",   "$0","","$0","",
                    "$0(RotL1)(Line1)","","","",   "","","","",
                    "R","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "(R)(Line1)","","","",   "(R)","","","",
                    //
                    "(R)(+01)","","","",   "(+2)(+01)","","","",
                    "","","","",   "$2","","$2","",
                    "$2(RotR1)(Line2)","","","",   "","","","",
                    "R","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "(R)(Line2)","","","",   "(R)","","","",
                    //
                    "(R)(+01)","","","",   "(+2)(+01)","","","",
                    "","","","",   "$2","","$2","",
                    "$2(RotL1)(Line1)","","","",   "","","","",
                    "R","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "(R)(Line1)","","","",   "(R)","","","",
                    //
                    "(R)(+01)","","","",   "(+2)(+01)","","","",
                    "","","","",   "$0","","$0","",
                    "$0(RotR1)(Line2)","","","",   "","","","",
                    "R","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "(R)","","","",   "(R)","","","",
                    "(R)(Line2)","","","",   "(R)","","","",
                    //

                    "(R)(+01)","","","",   "(+2)(+01)","","","",
                    "","","","",   "(R)(+01)","","","",
                    "(+2)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)","","","",   "(R)(+01)","","","",
                    "(R)(+01)(LeftC2)(Scale)","","","",   "(R)(+01)(RightC2)","","","",

                    "","","","",   "(R)(+01)(RightC2)","","","",
                    "(R)(+01)(LeftC2)","","","",   "","","","",
                    "(R)(+01)(LeftC2)","","","",   "(R)(+01)(LeftC2)","","","",
                    "","","","",   "(R)(+01)(RightC2)","","","",
                    "(R)(+01)(LeftC2)","","","",   "","","","",

                    "^$3'1.3(Shake)(ScreenScale)","","$3'1.5","",   "^$3'1.3","","$3'1.5","",
                    "^$3'1.3","","$3'1.5","",   "^$3'1.3","","$3'1.5","",
                    "^$3'1.3","","$3'1.5","",   "^$3'1.3","","$3'1.5","",
                    "^$3'1.3","","$3'1.5","",   "^$3'1.3","","$3'1.5","",
                    "^$3'1.3","","$3'1.5","",   "^$3'1.3","","$3'1.5","",
                    "^$3'1.3","","$3'1.5","",   "^$3'1.3","","$3'1.5","",
                    "^$3'1.3","","$3'1.5","",   "^$3'1.3","","$3'1.5","",
                    "(^$3'1.5)(Over)","","","", "","","","",
                });
            }
            public void Start()
            {
                //Effect3D = FightResources.Shaders.Camera;
                //Effect3D.TextureSize = new(640, 1900 * (1 + ScreenDrawing.UpExtending));
                //Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y);
                //Effect3D.ProjectPoint = new(0, 0, 200);
                //Effect3D.CameraRotation = new(0, 0, 0);
                //Effect3D.CameraPosition = new(0, -3000, -300);
                //Effect3D.TextureSize = new(640, 960);
                //Effect3D.ProjectAxisY = new(0, Cos(30), -Sin(30));
                //Effect3D.ProjectAxisZ = new(0, Sin(30), Cos(30));
                //ScreenScale = 1.5f;
                //Effect3D.ProjectPoint = new(360, 0, 0);
                //RenderProduction production4 = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                //ScreenDrawing.SceneRendering.InsertProduction(production4);
                HeartAttribute.KR = true;
                HeartAttribute.KRDamage = 2;
                HeartAttribute.MaxHP = 100;
                HeartAttribute.Speed = 2.86f;
                SetGreenBox();
                TP();
                SetSoul(1);
                GametimeDelta = -75f;
                linecolor = new Color(255, 251, 167);
                colorbluebool = false;
                colorwhitebool = false;
                //GametimeDelta = 4600;
                //PlayOffset = 4600 + 77;
                //  GametimeDetla = this.BeatTime(1532);
                // SetSoul(0); 
            }
            //GlobalResources.Effects.CameraShader Effect3D;
        }
    }
}