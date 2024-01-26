using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static Extends.FightUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;

namespace Rhythm_Recall.Waves
{
    public class Dusttrust : IChampionShip
    {
        public Dusttrust()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new()
            {
                { "div.2", Difficulty.Hard },
                { "div.1", Difficulty.ExtremePlus }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            private class ThisInformation : SongInformation
            {
                public override string DisplayName => "Maniac's Revenge";
                public override string BarrageAuthor => "meow?meow?meow?";
                public override string SongAuthor => "Nickolas";
                public override string AttributeAuthor => "meow?meow?meow?";
                public override string PaintAuthor => "SirDoesArt";
                //public override string Extra => "The barrages is not finished with bugs,\nand we won't fix it nowadays because the ChampionShip.";
                public override Color ExtraColor => Color.Cyan;
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 19.1f),
                            new(Difficulty.Hard, 14.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 19.0f),
                            new(Difficulty.Hard, 14.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 21.9f),
                            new(Difficulty.Hard, 19.9f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();
            public Game() : base(62.5f / (560/*bpm*/ / 60f)) { }
            public static Game instance;

            public string Music => "DustTrust";
            public string FightName => "DustTrust Phase 2";
            public static float bpm = 62.5f / (560/*bpm*/ / 60f);
            public static float bpmchange = 560;
            #region non
            public void Noob()
            {
            }
            public void Hard()
            {
                Normal();
            }
            public void Extreme()
            {
            }
            public void Easy()
            {
            }
            #endregion 
            private static Shader shader;
            private static Shader black;
            public static class MainEffects
            {
                public static void Shadershake()
                {
                    float a = 0;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, 5, () =>
                    {
                        a += 1.2f;
                        b += 300 / 5;
                        shader.Parameters["range"].SetValue(a);
                        shader.Parameters["frequency"].SetValue(b);
                    }));
                    AddInstance(new TimeRangedEvent(6, 5, () =>
                    {
                        a -= 1.2f;
                        b -= 300 / 5;
                        shader.Parameters["range"].SetValue(a);
                        shader.Parameters["frequency"].SetValue(b);
                    }));
                }
                public static void RGB()
                {
                    float a = 0;
                    AddInstance(new TimeRangedEvent(0, 2, () =>
                    {
                        shader.Parameters["distance"].SetValue(new Vector2(a, a)); a += 0.015f / 2;
                    }));
                    AddInstance(new TimeRangedEvent(3, 8, () =>
                    {
                        shader.Parameters["distance"].SetValue(new Vector2(a, a)); a -= 0.015f / 8;
                    }));
                    AddInstance(new TimeRangedEvent(12, 1, () =>
                    {
                        shader.Parameters["distance"].SetValue(new Vector2(0));
                    }));
                }
                public static void Supershake()
                {
                    float a = 1;
                    AddInstance(new TimeRangedEvent(bpm * 4, () =>
                    {
                        shader.Parameters["range2"].SetValue(a);
                        shader.Parameters["frequency2"].SetValue(240f);
                        a -= 1 / bpm / 4;
                    }));
                }
            }
            public static class Effects
            {
                public static void GB()
                {
                    NormalGB gb1 = new(new(320 - 160 - 40, 240 - 60), new(320 - 160 - 40, -45), new(1, 1), 90, 10000, 1);
                    NormalGB gb2 = new(new(320 + 160 + 40, 240 - 60), new(320 + 160 + 40, -45), new(1, 1), 90, 10000, 1);
                    NormalGB gb3 = new(new(320 - 80, 240 - 120), new(320 - 80, -45), new(1, 1), 90, 10000, 1);
                    NormalGB gb4 = new(new(320 + 80, 240 - 120), new(320 + 80, -45), new(1, 1), 90, 10000, 1);
                    PlaySound(GBSpawn);
                    CreateGB(gb1);
                    CreateGB(gb2);
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        for (int a = 0; a < 2; a++) PlaySound(GBSpawn);
                        CreateGB(gb3);
                        CreateGB(gb4);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4 * 16, 1, () =>
                    {
                        gb1.Dispose();
                        gb2.Dispose();
                        gb3.Dispose();
                        gb4.Dispose();
                    }));
                }
                public static void part0()
                {
                    float speed = 180;
                    DrawingUtil.NormalLine line1 = new(-5, 0, -5, 480, (int)(bpm * 32), 0.8f, new(189, 44, 153));
                    DrawingUtil.NormalLine line2 = new(645, 0, 645, 480, (int)(bpm * 32), 0.8f, new(189, 44, 153));
                    CreateEntity(line1);
                    CreateEntity(line2);
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        line1.x1 -= Sin(speed) * 7;
                        line1.x2 -= Sin(speed) * 7;
                        line2.x1 += Sin(speed) * 7;
                        line2.x2 += Sin(speed) * 7;
                        speed += 180 / bpm / 16;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, bpm * 16, () =>
                    {
                        line1.alpha -= 0.8f / bpm / 16;
                        line2.alpha -= 0.8f / bpm / 16;
                    }));
                }
                public static void part1()
                {
                    float b = 0;
                    for (int c = 0; c < 7; c++)
                    {
                        float i = c % 2 == 0 ? 1 : -1;
                        AddInstance(new TimeRangedEvent(c * bpm * 16 + c, bpm * 16, () =>
                        {
                            ScreenDrawing.BackGroundColor = new(b, b, b);
                            b += 0.15f / bpm / 16 * i;
                        }));
                    }
                    MainEffects.Shadershake();
                    for (int a = 1; a < 7; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * a - 2, 1, () =>
                        {
                            MainEffects.Shadershake();
                        }));
                    }
                    AddInstance(new TimeRangedEvent(bpm * 7 * 16 - 2, 1, () =>
                    {
                        MainEffects.Shadershake();
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 7 * 16 + bpm * 4 - 2, 1, () =>
                    {
                        MainEffects.Shadershake();
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 7 * 16 + bpm * 8 - 2, 1, () =>
                    {
                        MainEffects.Shadershake();
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 7 * 16 + bpm * 12 - 2, 1, () =>
                    {
                        MainEffects.Shadershake();
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8 * 16 - 2, 1, () =>
                    {
                        MainEffects.Shadershake();
                    }));
                }
                public static void part2()
                {
                    AddInstance(new TimeRangedEvent(bpm * 20, bpm * 13, () =>
                    {
                        ScreenDrawing.ScreenPositionDelta = Heart.Centre - new Vector2(320, 240);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20, 1, () =>
                    {
                        ScreenDrawing.ScreenAngle = 360 / 8 * 1;
                        ScreenDrawing.ScreenScale = 1.25f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20 + bpm * 2, 1, () =>
                    {
                        ScreenDrawing.ScreenAngle = 360 / 8 * 2;
                        ScreenDrawing.ScreenScale = 1.5f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20 + bpm * 4, 1, () =>
                    {
                        ScreenDrawing.ScreenAngle = 360 / 8 * 3;
                        ScreenDrawing.ScreenScale = 1.75f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20 + bpm * 6, 1, () =>
                    {
                        ScreenDrawing.ScreenAngle = 360 / 8 * 4;
                        ScreenDrawing.ScreenScale = 2.0f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20 + bpm * 8, 1, () =>
                    {
                        ScreenDrawing.ScreenAngle = 360 / 8 * 5;
                        ScreenDrawing.ScreenScale = 2.5f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20 + bpm * 10, 1, () =>
                    {
                        ScreenDrawing.ScreenAngle = 360 / 8 * 6;
                        ScreenDrawing.ScreenScale = 3f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20 + bpm * 12, 1, () =>
                    {
                        ScreenDrawing.ScreenAngle = 360 / 8 * 7;
                        ScreenDrawing.ScreenScale = 3.5f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20 + bpm * 14, 1, () =>
                    {
                        ScreenDrawing.ScreenAngle = 360 / 8 * 8;
                        ScreenDrawing.ScreenScale = 4f;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 20 + bpm * 14, bpm * 16, () =>
                    {
                        ScreenDrawing.ScreenPositionDelta = new(0, 0);
                        ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.95f + 1 * 0.05f;
                    }));
                }
                public static void part3()
                {

                    Heart.Gravity = 0;
                    PlaySound(switchScene);
                    SetSoul(2);
                    Heart.GiveForce(270, 0);
                    Heart.JumpTimeLimit = 0;
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                    {
                        InstantTP(320 - speed * speed * 0.5f, Heart.Centre.Y);
                        InstantSetBox(320 - 20 - speed * speed, 645, 280 - 84 * 0.75f, 280 + 84 * 0.75f);
                        speed += 0.17f;
                    }));
                    float count = 0;
                    AddInstance(new TimeRangedEvent(bpm * 16 + 1, 1, () =>
                    {
                        count = Heart.Centre.X;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + 2, 1, () =>
                    {
                        InstantTP(count, Heart.Centre.Y);
                    }));
                }
                public static void part4()
                {
                    Heart.JumpTimeLimit = 3;
                    Heart.Gravity = 9.8f;
                    SetSoul(2);
                    Heart.GiveForce(270, 1);
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                    {
                        InstantSetBox(0, BoxStates.Right - speed, 280 - 84 * 0.75f, 280 + 84 * 0.75f);
                        speed += 0.2f;
                    }));
                }
            }
            public static Func<ICustomMotion, float> special = (s) => { return 90 + s.RotationRouteParam[3] + (float)(s.RotationRouteParam[0] * Math.Sin((s.AppearTime + s.RotationRouteParam[2]) / s.RotationRouteParam[1] * Math.PI * 2)); };
            public static Func<ICustomLength, float> special1 = (s) =>
            {
                return Sin(s.AppearTime / s.LengthRouteParam[1] * MathF.PI) * s.LengthRouteParam[0];
            };
            public static class Explusfight
            {
                public static void Knife(Vector2 center, float rotate, bool trafic, float trafictime)
                {
                    DrawingUtil.Linerotatelong head = new(center.X, center.Y, rotate + 180, trafictime, 1, 34, new(255, 61, 207));
                    head.width = 7;
                    DrawingUtil.Linerotatelong end = new(center.X + Cos(rotate) * 10, center.Y + Sin(rotate) * 10, rotate, trafictime, 1, 8, new(255, 61, 207));
                    end.width = 7;
                    CreateEntity(head);
                    CreateEntity(end);
                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        head.Dispose();
                        end.Dispose();
                    }));
                    if (!trafic)
                    {
                        PlaySound(Warning);
                    }

                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        DrawingUtil.Shock(1.2f, 1.3f, 3);
                        PlaySound(largeKnife, 0.7f);
                        DrawingUtil.Linerotatelong Line = new(center.X + Cos(rotate) * 640 * 1.25f, center.Y + Sin(rotate) * 640 * 1.25f, rotate + 180, bpm * 32, 1, 640 * 2.5f, new(189, 44, 153));
                        Line.width = 0;
                        CreateEntity(Line);
                        AddInstance(new TimeRangedEvent(5, bpm * 9, () =>
                        {
                            Line.width = Line.width * 0.7f + 25 * 0.3f;
                            Line.alpha -= 1 / bpm / 4;
                        }));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateSpear(new Pike(center + new Vector2(Cos(rotate + 180) * (128 + a * 33), Sin(rotate + 180) * (128 + a * 33)), rotate, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                            CreateSpear(new Pike(center + new Vector2(Cos(rotate) * (128 + a * 33), Sin(rotate) * (128 + a * 33)), rotate + 180, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                        }
                    }));

                }
                public static void Knife(Vector2 center, float rotate, bool trafic, float trafictime, bool value)
                {

                    DrawingUtil.Linerotatelong head = new(center.X, center.Y, rotate + 180, trafictime, 1, 34, new(255, 61, 207));
                    head.width = 7;
                    DrawingUtil.Linerotatelong end = new(center.X + Cos(rotate) * 10, center.Y + Sin(rotate) * 10, rotate, trafictime, 1, 8, new(255, 61, 207));
                    end.width = 7;
                    CreateEntity(head);
                    CreateEntity(end);
                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        head.Dispose();
                        end.Dispose();
                    }));
                    if (!trafic)
                    {
                        PlaySound(Warning);
                    }
                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        if (!value) PlaySound(largeKnife, 0.7f);
                        DrawingUtil.Linerotatelong Line = new(center.X + Cos(rotate) * 640 * 1.25f, center.Y + Sin(rotate) * 640 * 1.25f, rotate + 180, bpm * 32, 1, 640 * 2.5f, new(189, 44, 153));
                        Line.width = 0;
                        CreateEntity(Line);
                        DrawingUtil.Shock(1.2f, 1.3f, 3);
                        AddInstance(new TimeRangedEvent(5, bpm * 9, () =>
                        {
                            Line.width = Line.width * 0.7f + 25 * 0.3f;
                            Line.alpha -= 1 / bpm / 4;
                        }));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateSpear(new Pike(center + new Vector2(Cos(rotate + 180) * (128 + a * 33), Sin(rotate + 180) * (128 + a * 33)), rotate, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                            CreateSpear(new Pike(center + new Vector2(Cos(rotate) * (128 + a * 33), Sin(rotate) * (128 + a * 33)), rotate + 180, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                        }
                    }));

                }
                public static void SpecialKnife(Vector2 center, float rotate, bool trafic, float trafictime, bool value)
                {
                    DrawingUtil.Linerotate head = new(center.X, center.Y, rotate + 180, trafictime, 1, Color.Red * 0.75f);
                    head.width = 1.5f;
                    CreateEntity(head);
                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        head.Dispose();
                    }));
                    float sin = 90;
                    float rot = 0;
                    float rand = Rand(-1.0f, 1.0f);
                    AddInstance(new TimeRangedEvent(0, trafictime - 2, () =>
                    {
                        sin += 90 / (trafictime - 2);
                        head.rotate = rot;
                        rot = Sin(sin) * 30 * rand + rotate;
                    }));
                    if (!trafic)
                    {
                        PlaySound(Warning);
                    }
                    AddInstance(new TimeRangedEvent(trafictime + 1, 1, () =>
                    {
                        if (!value) PlaySound(largeKnife, 0.7f);
                        DrawingUtil.Linerotate Line = new(center.X, center.Y, rot, bpm * 32, 1, Color.Red * 0.75f);
                        Line.width = 0;
                        CreateEntity(Line);
                        DrawingUtil.Shock(1.2f, 1.3f, 3);
                        AddInstance(new TimeRangedEvent(5, bpm * 9, () =>
                        {
                            Line.width = Line.width * 0.7f + 25 * 0.3f;
                            Line.alpha -= 1 / bpm / 4;
                        }));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateSpear(new Pike(center + new Vector2(Cos(rot + 180) * (128 + a * 33), Sin(rot + 180) * (128 + a * 33)), rot, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                            CreateSpear(new Pike(center + new Vector2(Cos(rot) * (128 + a * 33), Sin(rot) * (128 + a * 33)), rot + 180, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                        }
                    }));

                }
                public static void WaveParticle(float rot)
                {
                    int rand = Rand(0, 90);
                    for (int a = 0; a < 7; a++)
                    {
                        CreateBone(new CustomBone(new(320, 240), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                        {
                            PositionRouteParam = new float[] { Cos(360 / 5 * a + rot) * 6.3f, Sin(360 / 5 * a + rot) * 6.3f },
                            LengthRouteParam = new float[] { 3, 256 },
                            RotationRouteParam = new float[] { 0, 360 / 5 * a +90+rand+rot
},
                        });
                    }
                }
                public static void WaveParticle(float rot, int color, int distance)
                {
                    for (int a = 0; a < distance; a++)
                    {
                        CreateBone(new CustomBone(new(320 - Cos(360 / distance * a + rot) * 12f, 240 - Sin(360 / distance * a + rot) * 12f), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                        {
                            PositionRouteParam = new float[] { Cos(360 / distance * a + rot) * 6.4f, Sin(360 / distance * a + rot) * 6.4f },
                            LengthRouteParam = new float[] { 16, 256 },
                            RotationRouteParam = new float[] { 0, 360 / distance * a +90+rot

},
                            ColorType = color
                        });
                    }
                }
                public static void randturn()
                {
                    int[] turn = new[] { 1, 2, 3, 4, 5, 6 };
                    for (int a = 0; a < 10; a++)
                    {
                        int rand1 = Rand(0, 5);
                        int rand2 = Rand(0, 5);
                        int change = turn[rand1];
                        turn[rand1] = turn[rand2];
                        turn[rand2] = change;
                    }
                    for (int a = 0; a < 6; a++)
                    {
                        int value = a;
                        AddInstance(new TimeRangedEvent(a * bpm * 16, 1, () =>
                        {
                            TextPrinter tex1 = new((int)(bpm * 4), "$$$" + turn[value].ToString(), new Vector2(320 - 40, 240 - 80), new TextAttribute[]
                            {
                                new TextFadeoutAttribute(0,bpm*8),
                                new TextColorAttribute(Color.Lerp(new Color(255,22,36),new Color(0,0,0),0.15f)),
                                new TextSizeAttribute(5f),


                            });
                            tex1.Depth = 0.99f;
                            CreateEntity(tex1);
                            PlaySound(Ding);
                        }));
                        AddInstance(new TimeRangedEvent(a * bpm * 16 + bpm * 4, 1, () =>
                        {
                            if (turn[value] == 1) fight4();
                            if (turn[value] == 2) fight5();
                            if (turn[value] == 3) fight6();
                            if (turn[value] == 4) fight7();
                            if (turn[value] == 5) fight8();
                            if (turn[value] == 6) fight9();
                        }));
                    }
                }
                public static void fight1()
                {
                    for (int a = 0; a < 4 * 4 * 1.70f; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 4 * a, 1, () =>
                        {
                            CreateBone(new DownBone(true, 4.2f, 40));
                        }));
                    }
                    for (int a = 0; a < 16 * 4 * 7 * 1.13f * 1.5f; a++)
                    {
                        AddInstance(new TimeRangedEvent(1 * a, 1, () =>
                        {
                            DownBone bone = new(false, 13, 13) { MarkScore = false };
                            CreateBone(bone);
                        }));
                    }
                    InstantSetBox(280, 400, 150);
                    for (int a = 0; a < 26; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 4 * a + bpm * 6, 1, () =>
                        {
                            Knife(Heart.Centre, 90, false, bpm * 2);
                        }));
                    }
                    CreatePlatform(new Platform(0, new(320 - 25, 280 + 150 / 2 - 32), Motions.PositionRoute.linear, 0, 50)
                    {
                        PositionRouteParam = new float[] { (320 + 50) / 2 / bpm / 32, 0 }
                    });
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        CreatePlatform(new Platform(0, new(320 + 25 + 400 / 2, 280 + 150 / 2 - 32), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { -(320 + 50) / bpm / 32, 0 }
                        });
                        CreatePlatform(new Platform(1, new(320 + 25 + 400 / 2, 280 - 150 / 2 + 32), Motions.PositionRoute.linear, 180, 50)
                        {
                            PositionRouteParam = new float[] { -(320 + 50) / bpm / 32, 0 }
                        });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32 + 32 * bpm, 1, () =>
                    {
                        CreatePlatform(new Platform(0, new(320 - 25 - 400 / 2, 280 + 150 / 2 - 32), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { (320 + 50) / bpm / 32, 0 }
                        });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64 + 32 * bpm, 1, () =>
                    {
                        CreatePlatform(new Platform(0, new(320 + 25 + 400 / 2 + 25, 280 + 150 / 2 - 32), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { -480 / 2 / bpm / 16, 0 }
                        });
                    }));
                    //   Boneslab boneslab1 = new(0, 10, 0, (int)(bpm * 128), Motions.LengthRoute.autoFold,Motions.PositionRoute.);
                }
                public static void fight1_1()
                {
                    for (int a = 0; a < 4; a++)
                    {
                        int b = a % 2;
                        AddInstance(new TimeRangedEvent(bpm * 8 * (a + 1), 1, () =>
                        {
                            CustomBone bone1 = new(new(520, 280), Motions.PositionRoute.linear, 180, 60) { ColorType = b, PositionRouteParam = new float[] { -3, 0 } };
                            CreateBone(bone1);
                        }));
                    }
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        CreateGB(new NormalGB(new(320 + 25 + 400 / 2 + 40, 280 - 150 / 2 - 16), new(320 + 25 + 400 / 2 + 300, 280 - 150 / 2 - 16), new Vector2(1f, 1f), 180, bpm * 6, bpm * 32));
                        Heart.GiveForce(180, 6);
                    }));
                    AddInstance(new InstantEvent(bpm * 32 + bpm * 8, () =>
                    {
                        for (int a = 0; a < 20; a++)
                        {
                            float length = (90 + 45) / bpm * a;
                            UpBone bone1 = new(true, 520 + a * 3.75f * 4.5f, 4.5f, 30 + Sin(length) * 32);
                            DownBone bone2 = new(true, 520 + a * 3.75f * 4.5f, 4.5f, 150 - 30 - Sin(length) * 32 - 64);
                            CreateBone(bone1);
                            CreateBone(bone2);
                        }
                        CreateBone(new DownBone(true, 520 + 26.5f * 3.75f * 4.5f, 4.5f, 150) { ColorType = 1 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64, 1, () =>
                    {
                        Heart.GiveForce(0, 8);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64 + 5, 1, () =>
                    {
                        PlaySound(slam);
                    }));
                    for (int a = 0; a < 3; a++)
                    {
                        float b = 135;
                        AddInstance(new TimeRangedEvent(bpm * 64 + bpm * 8 + a * 16 * bpm, 1, () =>
                        {
                            LeftBone bone1 = new(true, 0, 396)
                            {
                                ColorType = 1
                            };
                            CreateBone(bone1);
                            AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                            {

                                bone1.Speed = Sin(b) * 4f;
                                b += 180 / 16 / bpm;
                            }));

                        }));
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 64 + bpm * 6 * a, 1, () =>
                        {
                            CreateBone(new UpBone(false, 4, 110 - 16 - 16));
                        }));
                    }
                    AddInstance(new TimeRangedEvent(bpm * 64 + bpm * 6 * 2 + bpm * 2, 1, () =>
                    {
                        CreateBone(new UpBone(false, 4, 75 - 32));
                        CreateBone(new DownBone(false, 4, 75 - 16));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64 + bpm * 6 * 2 + bpm * 4, 1, () =>
                    {
                        CreateBone(new UpBone(false, 4, 148) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64 + bpm * 6 * 4 + bpm * 2, 1, () =>
                    {
                        CreateBone(new UpBone(false, 4, 75 - 16));
                        CreateBone(new DownBone(false, 4, 75 - 32));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64 + bpm * 6 * 4 + bpm * 4, 1, () =>
                    {
                        CreateBone(new UpBone(false, 4.2f, 148) { ColorType = 2 });
                    }));
                }
                public static void fight2()
                {

                    CreateGB(new NormalGB(new(320 - 70, 240 - 130), new(320 - 70, 330), new(1, 0.45f), bpm * 14, bpm * 3));
                    CreateGB(new NormalGB(new(320 + 70, 240 - 130), new(320 + 70, 330), new(1, 0.45f), bpm * 14, bpm * 3));
                    AddInstance(new TimeRangedEvent(bpm * 3, 1, () =>
                    {
                        CreateGB(new NormalGB(new(320 - 70, 240 - 130), new(320 - 70, 330), new(1, 0.45f), bpm * 14 - bpm * 3, bpm * 3));
                        CreateGB(new NormalGB(new(320 + 70, 240 - 130), new(320 + 70, 330), new(1, 0.45f), bpm * 14 - bpm * 3, bpm * 3));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        CreateGB(new NormalGB(new(320 - 70, 240 - 130), new(320 - 70, 330), new(1, 0.45f), bpm * 16 - bpm * 6, bpm * 3));
                        CreateGB(new NormalGB(new(320 + 70, 240 - 130), new(320 + 70, 330), new(1, 0.45f), bpm * 16 - bpm * 6, bpm * 3));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 9, 1, () =>
                    {
                        CreateGB(new NormalGB(new(320 - 70, 240 - 130), new(320 - 70, 330), new(1, 0.45f), bpm * 16 - bpm * 9, bpm * 3));
                        CreateGB(new NormalGB(new(320 + 70, 240 - 130), new(320 + 70, 330), new(1, 0.45f), bpm * 16 - bpm * 9, bpm * 3));
                    }));
                    /*float count = 0;
                    for (int a = 0; a < 4; a++)
                    {
                        if (a % 4 == 0) count = bpm * 5;
                        if (a % 4 == 1) count = 0;
                        if (a % 4 == 2) count = bpm * 5;
                        if (a % 4 == 3) count = 0;
                        AddInstance(new TimeRangedEvent(bpm * 3 * a, 1, () =>
                        {
                            CreateGB(new NormalGB(new(320 - 70, 240-130), new(320 - 70, 330), new(1, 0.45f), bpm * 16 - bpm * 3 * a+count, bpm * 3));
                            CreateGB(new NormalGB(new(320 + 70, 240-130), new(320 + 70, 330), new(1, 0.45f), bpm * 16 - bpm * 3 * a+count, bpm * 3));
                        }));
                    }*/

                }
                public static void fight3()
                {

                    for (int a = 0; a < 8; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 8, 1, () =>
                        {
                            // float speed = 45;
                            LeftBone bone1 = new(false, 3.1f, 82 - 12);
                            CreateBone(bone1);
                            /*AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                            {
                                bone1.MissionLength = 45+Sin(speed) * 27;
                                speed += 360 / bpm / 12;
                            }));*/
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16 * (a + 1), 1, () =>
                        {
                            //  float speed = 45;
                            RightBone bone1 = new(false, 3.1f, 82 - 12);
                            CreateBone(bone1);
                            /*AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                            {
                                bone1.MissionLength = 45+Sin(speed) * 27;
                                speed += 360 / bpm / 12;
                            }));*/
                        }));
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * (a + 1) + bpm * 2, 1, () =>
                        {
                            PlaySound(spearAppear);
                            CreateSpear(new Pike(Heart.Centre + new Vector2(150, 0), 180, 6, bpm * 2) { DrawingColor = new(189, 44, 153) });
                            CreateSpear(new Pike(Heart.Centre - new Vector2(150, 0), 0, 6, bpm * 2) { DrawingColor = new(189, 44, 153) });
                            CreateSpear(new Pike(Heart.Centre + new Vector2(0, 150), 270, 6, bpm * 2) { DrawingColor = new(189, 44, 153) });
                            CreateSpear(new Pike(Heart.Centre - new Vector2(0, 150), 90, 6, bpm * 2) { DrawingColor = new(189, 44, 153) });
                        }));

                    }
                    for (int a = 0; a < 15; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 8 * (a + 1) + bpm * 8, 1, () =>
                        {
                            Knife(Heart.Centre, Rand(0, 360), false, bpm * 4);
                        }));
                    }
                    for (int a = 0; a < 6; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 24 * a, 1, () =>
                        {
                            CreatePlatform(new Platform(1, new Vector2(320, 110), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                            {
                                PositionRouteParam = new float[] { 0, 1.85f },
                                LengthRouteParam = new float[] { 60, 180 },
                                RotationRouteParam = new float[] { 30, bpm * 16, 0, 0 }
                            });
                            CreateBone(new CustomBone(new Vector2(320, 117), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, special)
                            {
                                PositionRouteParam = new float[] { 0, 1.85f },
                                LengthRouteParam = new float[] { 42, 180 },
                                RotationRouteParam = new float[] { 30, bpm * 16, 0, 0 }
                            });
                        }));

                    }
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        for (int a = 0; a < 16 * 2 * 7 + 50; a++)
                        {
                            AddInstance(new TimeRangedEvent(3 * a, 1, () =>
                            {
                                DownBone bone = new(true, 6.3f, 20) { MarkScore = false };
                                CreateBone(bone);
                            }));
                        }
                    }));
                }
                public static void fight3_3()
                {
                    for (int a = 1; a < 9; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 8, 1, () =>
                        {
                            PlaySound(pierce);
                            float count = 90;
                            DownBone bone1 = new(false, 0, 258) { ColorType = 1 };
                            CreateBone(bone1);
                            AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                            {
                                bone1.Speed = Sin(count) * 4.6f;
                                count += 180 / bpm / 8;
                            }));
                        }));
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * (a + 1), 1, () =>
                        {
                            PlaySound(pierce);
                            float count = 90;
                            DownBone bone1 = new(true, 0, 258) { ColorType = 1 };
                            CreateBone(bone1);
                            AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                            {
                                bone1.Speed = Sin(count) * 4.6f;
                                count += 180 / bpm / 8;
                            }));
                        }));
                    }
                }
                public static void fight4()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    InstantSetBox(new Vector2(410, 280), 150, 150);
                    InstantTP(410, 280);
                    SetSoul(0);
                    Knife(new(410 + 25, 280), 90, true, 0);
                    Knife(new(410 - 25, 280), 90, true, 0, true);
                    Knife(new(410 + 25, 280), 90 + 180, true, 0, true);
                    Knife(new(410 - 25, 280), 90 + 180, true, 0, true);
                    Knife(new(410, 280), 0, true, bpm * 2);
                    Knife(new(410, 280), 90, true, bpm * 2, true);
                    Knife(new(410, 280), 45, true, bpm * 4);
                    Knife(new(410, 280), 90 + 45, true, bpm * 4, true);//X
                    Knife(new(410, 280), 45 + 180, true, bpm * 4, true);
                    Knife(new(410, 280), 90 + 45 + 180, true, bpm * 4, true);//X

                    Knife(new(410 + 75, 280 - 75), 0, true, bpm * 4, true);
                    Knife(new(410 - 75, 280 + 75), 180, true, bpm * 4, true);
                    Knife(new(410 + 75, 280 + 75), 0, true, bpm * 4, true);
                    Knife(new(410 - 75, 280 - 75), 180, true, bpm * 4, true);

                    Knife(new(410 + 100, 280), 90, true, bpm * 4, true);
                    Knife(new(410 - 100, 280), 90, true, bpm * 4, true);
                    Knife(new(410 + 50, 280), 90, true, bpm * 4, true);
                    Knife(new(410 - 50, 280), 90, true, bpm * 4, true);
                    Knife(new(410, 280), 90, true, bpm * 4, true);//IIIII
                    AddInstance(new InstantEvent(bpm * 8, () =>
                    {
                        for (int a = 0; a < 2; a++) PlaySound(pierce);
                        CreateBone(new DownBone(false, 410 - 75, 8, 148) { ColorType = 1 });
                        CreateBone(new DownBone(false, 410 - 75 - 9, 8, 148) { ColorType = 1 });
                        CreateBone(new DownBone(false, 410 - 75 - 18, 8, 148) { ColorType = 1 });
                        CreateBone(new DownBone(true, 410 + 75, 8, 148) { ColorType = 2 });
                        CreateBone(new DownBone(true, 410 + 75 + 9, 8, 148) { ColorType = 2 });
                        CreateBone(new DownBone(true, 410 + 75 + 18, 8, 148) { ColorType = 2 });
                    }));
                }
                public static void fight5()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    InstantSetBox(new Vector2(240, 260), 130, 130);
                    InstantTP(240, 260);
                    SetSoul(0);
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(200, 0), Heart.Centre + new Vector2(200, 0), new Vector2(1, 1), 180, bpm * 8, bpm * 7));
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(-200, 0), Heart.Centre + new Vector2(200, 0), new Vector2(1, 1), 0, bpm * 8, bpm * 7));
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(0, 200), Heart.Centre + new Vector2(200, 0), new Vector2(1, 1), 270, bpm * 8, bpm * 7));
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(0, -200), Heart.Centre + new Vector2(200, 0), new Vector2(1, 1), 90, bpm * 8, bpm * 7));
                    for (int a = 0; a < 2; a++) PlaySound(pierce);
                    CreateBone(new DownBone(false, 5, 130 / 2 - 14));
                    CreateBone(new LeftBone(false, 5, 130 / 2 - 14));
                    CreateBone(new UpBone(true, 5, 130 / 2 - 14));
                    CreateBone(new RightBone(true, 5, 130 / 2 - 14));
                    CreateEntity(new RotBone(MathF.Sqrt(130 * 130 * 2) / 2 - 16, 7, 45, false, 0));
                    CreateEntity(new RotBone(MathF.Sqrt(130 * 130 * 2) / 2 - 16, 7, 45, false, 1));
                    CreateEntity(new RotBone(MathF.Sqrt(130 * 130 * 2) / 2 - 16, 7, 45, false, 2));
                    CreateEntity(new RotBone(MathF.Sqrt(130 * 130 * 2) / 2 - 16, 7, 45, false, 3));
                    CreateEntity(new Boneslab(0, 7, 1, bpm * 15));
                    CreateEntity(new Boneslab(90, 7, 1, bpm * 15));
                    CreateEntity(new Boneslab(180, 7, 1, bpm * 15));
                    CreateEntity(new Boneslab(270, 7, 1, bpm * 15));
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 20f, 150) { ColorType = 1 });
                        CreateBone(new DownBone(false, 20f, 150) { ColorType = 1 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 13, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 20f, 150) { ColorType = 1 });
                        CreateBone(new DownBone(false, 20f, 150) { ColorType = 1 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 5f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5f, 150) { ColorType = 2 });
                    }));
                }
                public static void fight6()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    SetSoul(2);
                    InstantSetBox(new Vector2(320, 320), 130, 130);
                    InstantTP(320, 320 + 65 - 8);

                    PlaySound(pierce);
                    CreateBone(new DownBone(true, 5f, 20));
                    CreateBone(new DownBone(false, 5f, 20));
                    CreateBone(new UpBone(true, 5f, 70));
                    CreateBone(new UpBone(false, 5f, 70));
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 5f, 75 - 30));
                        CreateBone(new DownBone(false, 5f, 75 - 30));
                        CreateBone(new UpBone(true, 5f, 75 - 30));
                        CreateBone(new UpBone(false, 5f, 75 - 30));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 5f, 70));
                        CreateBone(new DownBone(false, 5f, 70));
                        CreateBone(new UpBone(true, 5f, 20));
                        CreateBone(new UpBone(false, 5f, 20));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        SetSoul(0);
                        CreateBone(new DownBone(true, 6, 130) { ColorType = 1 });
                        CreateBone(new DownBone(false, 6, 130) { ColorType = 1 });

                        CreateBone(new DownBone(true, 5, 130 / 2 - 24) { ColorType = 0 });
                        CreateBone(new UpBone(true, 5, 130 / 2 - 24) { ColorType = 0 });
                        CreateBone(new DownBone(false, 5, 130 / 2 - 24) { ColorType = 0 });
                        CreateBone(new UpBone(false, 5, 130 / 2 - 24) { ColorType = 0 });

                        CreateBone(new DownBone(true, 4, 130 / 2 - 48) { ColorType = 2 });
                        CreateBone(new UpBone(true, 4, 130 / 2 - 48) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4, 130 / 2 - 48) { ColorType = 2 });
                        CreateBone(new UpBone(false, 4, 130 / 2 - 48) { ColorType = 2 });

                        CreateBone(new DownBone(true, 3, 130 / 2 - 24) { ColorType = 0 });
                        CreateBone(new UpBone(true, 3, 130 / 2 - 24) { ColorType = 0 });
                        CreateBone(new DownBone(false, 3, 130 / 2 - 24) { ColorType = 0 });
                        CreateBone(new UpBone(false, 3, 130 / 2 - 24) { ColorType = 0 });

                        CreateEntity(new Boneslab(0, 130 / 2 - 16, 1, bpm * 4));
                        CreateEntity(new Boneslab(90, 130 / 2 - 16, 1, bpm * 4));
                        CreateEntity(new Boneslab(180, 130 / 2 - 16, 1, bpm * 4));
                        CreateEntity(new Boneslab(270, 130 / 2 - 16, 1, bpm * 4));
                    }));
                }
                public static void fight7()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    InstantSetBox(new Vector2(430, 230), 150, 150);
                    InstantTP(430, 230);
                    SetSoul(0);

                    for (int a = 0; a < 4; a++)
                    {
                        for (int b = 0; b < 3; b++) { int rotate = Rand(180 + 45, 270 + 10); CreateSpear(new Pike(new(Heart.Centre.X + Cos(rotate) * 200, Heart.Centre.Y + Sin(rotate) * 200), rotate + 180, 12, bpm * 14 - bpm * a) { DrawingColor = new(189, 44, 153) }); }
                        AddInstance(new TimeRangedEvent(bpm * 14 - bpm * a, 1, () =>
                        {
                            PlaySound(spearShoot);
                        }));
                    }
                    for (int a = 0; a < 12; a++)
                    {
                        CreateBone(new SideCircleBone(360 / 12 * a + 13, 10, 24, bpm * 7));
                    }
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        CreateBone(new CentreCircleBone(20, 4.4f, 190, bpm * 7.5f) { IsMasked = true });
                        CreateBone(new CentreCircleBone(110, 4.4f, 190, bpm * 7.5f) { IsMasked = true });
                        CreateBone(new CentreCircleBone(20 + 4.4f, 3f, 190, bpm * 7.5f) { IsMasked = true, ColorType = 2 });
                        CreateBone(new CentreCircleBone(110 + 4.4f, 3f, 190, bpm * 7.5f) { IsMasked = true, ColorType = 2 });
                    }));

                }
                public static void fight8()
                {
                    ResetBarrage();
                    Heart.GiveForce(0, 6);
                    Player.Heart a = Heart.Split();
                    SetBoxMission(0);
                    InstantSetBox(new Vector2(405, 230), 65, 150);
                    SetBoxMission(1);
                    InstantSetBox(new Vector2(398 + 65 + 34 - 5, 230), 65, 150);
                    SetPlayerMission(0);
                    InstantTP(405, 230);
                    SetSoul(0);
                    SetPlayerMission(1);
                    InstantTP(398 + 65 + 34 - 5, 230);
                    Heart.GiveForce(180, 6);
                    SetSoul(0);
                    SetPlayerMission(0);
                    AddInstance(new InstantEvent(bpm * 16 - bpm * 2, () =>
                    {
                        Heart.Merge(a);
                        Heart.GiveForce(0, 6);
                        InstantTP(400, 240);
                        SetPlayerMission(1);
                        TP(Heart.Centre);
                    }));
                    PlaySound(pierce);
                    CreateGB(new NormalGB(new Vector2(405 + 43.5f, 230 + 100), new Vector2(405 + 43.5f, 230 - 200), new(1, 0.6f), 90, bpm * 8, bpm * 4));
                    CreateGB(new NormalGB(new Vector2(405 + 43.5f, 230 + 100), new Vector2(405 + 43.5f, 230 + 200), new(1, 0.6f), 270, bpm * 8, bpm * 4));
                    Knife(new Vector2(405 + 43.5f, 230), 90, true, 0);
                    SetBoxMission(0);
                    CreateEntity(new Boneslab(90, 65 / 2 - 4, 2, bpm * 1 - 1));
                    SetBoxMission(1);
                    CreateEntity(new Boneslab(270, 65 / 2 - 4, 2, bpm * 1 - 1));
                    AddInstance(new InstantEvent(bpm * 2, () =>
                    {
                        PlaySound(pierce);
                        Knife(new Vector2(405 + 43.5f, 230), 0, true, 0, false);
                        Knife(new Vector2(405 + 43.5f, 230), 180, true, 0, true);
                        SetBoxMission(0);
                        CreateEntity(new Boneslab(270, 65 / 2 - 4, 1, bpm * 1));
                        SetBoxMission(1);
                        CreateEntity(new Boneslab(90, 65 / 2 - 4, 1, bpm * 1));
                    }));
                    AddInstance(new InstantEvent(bpm * 4, () =>
                    {
                        PlaySound(pierce);
                        Knife(new Vector2(405 + 43.5f, 230), 90, true, 0);
                        SetBoxMission(0);
                        CreateEntity(new Boneslab(90, 65 / 2 - 4, 1, bpm * 1));
                        SetBoxMission(1);
                        CreateEntity(new Boneslab(270, 65 / 2 - 4, 1, bpm * 1));
                    }));
                    AddInstance(new InstantEvent(bpm * 8, () =>
                    {
                        Knife(new Vector2(405 + 43.5f, 230), 45, true, 0, false);
                        Knife(new Vector2(405 + 43.5f, 230), 90 + 45, true, 0, true);
                        Knife(new Vector2(405 + 43.5f, 230), 45 + 180, true, 0, false);
                        Knife(new Vector2(405 + 43.5f, 230), 45 + 270, true, 0, true);
                        SetBoxMission(0);
                        CreateEntity(new Boneslab(0, 150 - 22, bpm, bpm * 3.5f));
                        SetBoxMission(1);
                        CreateEntity(new Boneslab(180, 150 - 22, bpm, bpm * 3.5f));
                    }));
                }
                public static void fight9()
                {
                    ResetBarrage();
                    SetSoul(0);
                    for (int a = 0; a < 36; a++)
                        CreateBone(new SideCircleBone(a * 10, 5, 150 - 28, bpm * 12.5f));

                    Heart.GiveForce(0, 6);
                    InstantTP(400, 240);
                    InstantSetBox(Heart.Centre, 150, 150);
                    AddInstance(new TimeRangedEvent(1, bpm * 16 - 2, () =>
                    {
                        InstantSetBox(BoxStates.Centre - new Vector2(2.5f, 0), 150, 150);
                    }));


                }
                public static void fight10()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    Knife(Heart.Centre, 45, true, bpm * 2, false);
                    Knife(Heart.Centre, 90 + 45, true, bpm * 2, true);
                    Knife(Heart.Centre, 45 + 180, true, bpm * 2, true);
                    Knife(Heart.Centre, 90 + 45 + 180, true, bpm * 2, true);
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        Knife(Heart.Centre, 45, true, bpm * 2, false);
                        Knife(Heart.Centre, 90 + 45, true, bpm * 2, true);
                        Knife(Heart.Centre, 45 + 180, true, bpm * 2, true);
                        Knife(Heart.Centre, 90 + 45 + 180, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Knife(Heart.Centre, 45, true, bpm * 2, false);
                        Knife(Heart.Centre, 90 + 45, true, bpm * 2, true);
                        Knife(Heart.Centre, 45 + 180, true, bpm * 2, true);
                        Knife(Heart.Centre, 90 + 45 + 180, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 11, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(Heart.Centre, 45, true, bpm * 2, false);
                        Knife(Heart.Centre, 90 + 45, true, bpm * 2, true);
                        Knife(Heart.Centre, 45 + 180, true, bpm * 2, true);
                        Knife(Heart.Centre, 90 + 45 + 180, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        float rotate = 270;
                        for (int a = 0; a < 8; a++)
                        {
                            Vector2 center = Heart.Centre;
                            CreateGB(new NormalGB(center + new Vector2(Cos(rotate) * 140, Sin(rotate) * 140), center + new Vector2(Cos(rotate) * 140, Sin(rotate) * 140), new(0.75f, 0.75f), rotate + 90, bpm * 18 - a * 2 * bpm, (a + 1) * 2 * bpm));
                            rotate += 360 / 8;
                        }
                    }));
                    for (int a = 0; a < 9; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 14 + a * 2 * bpm, 1, () =>
                        {
                            Knife(Heart.Centre, 45, true, bpm * 2, false);
                            Knife(Heart.Centre, 90 + 45, true, bpm * 2, true);
                            Knife(Heart.Centre, 45 + 180, true, bpm * 2, true);
                            Knife(Heart.Centre, 90 + 45 + 180, true, bpm * 2, true);
                        }));
                    }
                }

                public static void greensoul()
                {
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(shader, 0.55f));
                    shader.Parameters["frequency"].SetValue(0f);
                    shader.Parameters["distance"].SetValue(new Vector2(0f, 0f));
                    shader.Parameters["range"].SetValue(0f);
                    shader.Parameters["frequency2"].SetValue(0f);
                    shader.Parameters["range2"].SetValue(0f);
                    shader.Parameters["time"].SetValue(0f);
                    shader.Parameters["time2"].SetValue(0f);
                    Heart.GiveForce(0, 6);
                    float beat = 0;
                    string[] arrow =
                        {
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","$2",

                "/","$2","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","$2",

                "/","$2","/","/",     "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",

                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","$2",

                "/","$2","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","$2",

                "/","$2","/","/",     "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            for (int a = 0; a < 2; a++)
                            {
                                CreateArrow(bpm * 16 + beat, arrow[i], 5.5f, 0, 0);
                            }
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void fight11()
                {
                    ResetBarrage();
                    float speed = 2.9f;
                    for (int a = 0; a < 30; a++)
                    {
                        DownBone bone1 = new(false, 0, 1);
                        UpBone bone2 = new(true, 0, 1);

                        float rand = Rand(-14.0f, 14.0f);
                        bone1 = new(false, BoxStates.Left - speed * a * 6 * bpm, speed, 160 / 2 - rand - 8);
                        bone2 = new(true, BoxStates.Right + speed * a * 6 * bpm, speed, 160 / 2 + rand - 8);
                        CreateBone(bone1);
                        CreateBone(bone2);
                        speed = 2.2f;
                    }
                    for (int a = 0; a < 6; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 18 * a + bpm * 16, 1, () =>
                        {
                            PlaySound(Ding);
                            DownBone[] bone = GetAll<DownBone>();
                            UpBone[] bone1 = GetAll<UpBone>();
                            for (int b = 0; b < bone.Length; b++)
                            {
                                int x = b;
                                AddInstance(new TimeRangedEvent(0, bpm * 3 - 1, () =>
                                {
                                    bone[x].Speed = 6f * 0.05f + 0.95f * bone[x].Speed;
                                    AddInstance(new TimeRangedEvent(bpm * 3, bpm * 11, () =>
                                    {
                                        bone[x].Speed = 2.2f * 0.15f + 0.85f * bone1[x].Speed;
                                    }));

                                }));
                            }
                            for (int b = 0; b < bone1.Length; b++)

                            {
                                int x = b;
                                AddInstance(new TimeRangedEvent(0, bpm * 3 - 1, () =>
                                {
                                    bone1[x].Speed = 6f * 0.05f + 0.95f * bone1[x].Speed;
                                    AddInstance(new TimeRangedEvent(bpm * 3, bpm * 11, () =>
                                    {
                                        bone1[x].Speed = 2.2f * 0.15f + 0.85f * bone1[x].Speed;
                                    }));

                                }));
                            }
                        }));

                    }
                    AddInstance(new TimeRangedEvent(bpm * 17 * 6 + bpm * 17, 1, () =>
                    {
                        DownBone[] bone = GetAll<DownBone>();
                        UpBone[] bone1 = GetAll<UpBone>();
                        for (int a = 0; a < bone.Length; a++)
                        {
                            int x = a;
                            AddInstance(new TimeRangedEvent(0, bpm * 64, () =>
                            {
                                bone[x].Speed -= 0.004f;
                                bone1[x].Speed -= 0.004f;

                            }));
                        }
                    }));
                }
                public static void fight12()
                {
                    ResetBarrage();
                    ScreenDrawing.BoundColor = new(243, 3, 218);
                    float bound = 0;
                    CreateEntity(new Boneslab(0, 0, 1, (int)(bpm * 16 * 10) + 8, Motions.LengthRoute.sin, new float[] { 40, bpm * 33, bpm * 16.5f, 55 }));
                    CreateEntity(new Boneslab(180, 0, 1, (int)(bpm * 16 * 10) + 8, Motions.LengthRoute.sin, new float[] { 40, bpm * 33, 0, 55 }));
                    for (int a = 0; a < 22; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 8 * a, 1, () =>
                        {
                            CreatePlatform(new Platform(0, new(BoxStates.Right, BoxStates.Down - 180 - 10), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                            {
                                PositionRouteParam = new float[] { -3, 0 },
                                LengthRouteParam = new float[] { 56, bpm * 22 },
                                RotationRouteParam = new float[] { 0, 180 },
                                Tags = new string[] { "up" }
                            }); ;
                            CreatePlatform(new Platform(0, new(BoxStates.Left - 48 + 22, BoxStates.Up + 180 + 10), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                            {
                                PositionRouteParam = new float[] { 3, 0 },
                                LengthRouteParam = new float[] { 56, bpm * 22 },
                                RotationRouteParam = new float[] { 0, 0 },
                                Tags = new string[] { "down" }
                            });
                        }));
                    }
                    for (int a = 0; a < 2; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 64 * a + bpm * 32, 1, () =>
                        {
                            PlaySound(Ding);
                            Heart.GiveForce(180, 0);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 64 * (a + 1), 1, () =>
                        {
                            PlaySound(Ding);
                            Heart.GiveForce(0, 0);
                        }));
                    }
                    for (int a = 0; a < 3; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 64 * a, 1, () =>
                   {
                       for (int a = (int)BoxStates.Left; a < BoxStates.Right; a++)
                       {
                           if (a % 80 == 0)
                           {
                               CreateGB(new NormalGB(new(a, BoxStates.Up), new(a, BoxStates.Up), new(1f, 0.5f), 90, bpm * 16, bpm * 2));
                               CreateGB(new NormalGB(new(a, BoxStates.Down), new(a, BoxStates.Down), new(1f, 0.5f), 270, bpm * 16, bpm * 2));
                           }
                       }
                   }));
                        AddInstance(new TimeRangedEvent(bpm * 64 * a + bpm * 32, 1, () =>
                        {
                            for (int a = (int)BoxStates.Left; a < BoxStates.Right; a++)
                            {
                                if ((a + 40) % 80 == 0)
                                {
                                    CreateGB(new NormalGB(new(a, BoxStates.Up), new(a, BoxStates.Up), new(1f, 0.5f), 90, bpm * 16, bpm * 2));
                                    CreateGB(new NormalGB(new(a, BoxStates.Down), new(a, BoxStates.Down), new(1f, 0.5f), 270, bpm * 16, bpm * 2));
                                }
                            }
                        }));
                    }
                }
                public static void fight13()
                {
                    ResetBarrage();
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        CreateEntity(new DownBonesea(110, 4, 74, false, 10, bpm * 64) { tags = new string[] { "a" } });
                        CreateEntity(new UpBonesea(110, 4, 16, false, 10, bpm * 64) { tags = new string[] { "a" } });
                        AddInstance(new TimeRangedEvent(1, 1, () =>
                        {
                            CreateEntity(new DownBonesea(110, 4, 74, false, 10, bpm * 64, 2) { tags = new string[] { "a" } });
                            CreateEntity(new UpBonesea(110, 4, 16, false, 10, bpm * 64, 2) { tags = new string[] { "a" } });
                        }));
                        AddInstance(new TimeRangedEvent(3, 1, () =>
                        {
                            DownBone[] down = GetAll<DownBone>("a");
                            UpBone[] up = GetAll<UpBone>("a");

                            float speed = 0;
                            AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                            {
                                for (int a = 0; a < down.Length; a++)
                                {
                                    int b = a;
                                    down[b].MissionLength = 84 * 1.5f / 3 + 6 + Sin(speed + a * 3) * 32;
                                    up[b].MissionLength = 84 * 1.5f + 6 - down[b].MissionLength - 48;
                                }
                            }));
                            AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                            {
                                speed += 360 / bpm / 12;
                            }));
                        }));
                        for (int a = 0; a < 3; a++)
                        {
                            AddInstance(new TimeRangedEvent(bpm * 24f * a, 1, () =>
                            {
                                PlaySound(pierce);
                                // DownBone blue = new(true, 0, 84 * 1.5f / 2) { ColorType = 1 };
                                UpBone orange = new(true, 0, 84 * 1.5f) { ColorType = 2 };
                                // CreateBone(blue);
                                CreateBone(orange);

                                AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                                {
                                    // blue.Speed += 0.1f;
                                    orange.Speed += 0.1f;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 24f * a + bpm * 12, 1, () =>
                            {
                                PlaySound(pierce);
                                DownBone blue = new(true, 0, 84 * 1.5f) { ColorType = 2 };
                                //UpBone orange = new(true, 0, 84 * 1.5f / 2) { ColorType = 1 };
                                CreateBone(blue);
                                // CreateBone(orange);
                                AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                                {
                                    blue.Speed += 0.1f;
                                    //orange.Speed += 0.1f;
                                }));
                            }));
                        }
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 8 * 7 + bpm * 8, 1, () =>
                    {
                        for (int a = 1; a < 6; a++)
                        {
                            AddInstance(new TimeRangedEvent(bpm * 8 * a, 1, () =>
                            {
                                Knife(Heart.Centre, 0, false, bpm * 2, false);
                                int b = a;
                                float length = 84 * 1.5f / 3 * 1 - 10 + 4;
                                DownBone bone1 = new(true, 7, length);
                                UpBone bone2 = new(true, 7, 84 * 1.5f - length - 32);
                                PlaySound(pierce);
                                CreateBone(bone1);
                                CreateBone(bone2);
                                bool p = true;
                                AddInstance(new TimeRangedEvent(bpm * 6, 20, () =>
                                {
                                    if (p)
                                    {
                                        PlaySound(Ding);
                                        bone1.ColorType = 1;
                                        bone2.ColorType = 1;
                                        AddInstance(new TimeRangedEvent(3, 1, () =>
                                        {
                                            bone1.ColorType = 0;
                                            bone2.ColorType = 0;
                                        }));
                                        p = false;
                                    }
                                    bone1.MissionLength = (84 * 1.5f / 4 * 2 + 10 + 4) * 0.1f + bone1.MissionLength * 0.9f;
                                    bone2.MissionLength = 84 * 1.5f - bone1.MissionLength - 32;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 8 * a + bpm * 4, 1, () =>
                            {
                                Knife(Heart.Centre, 0, false, bpm * 2, false);
                                int b = a;
                                float length = 84 * 1.5f / 4 * 2 + 4;
                                DownBone bone1 = new(true, 7, length);
                                UpBone bone2 = new(true, 7, 84 * 1.5f - length - 32);
                                PlaySound(pierce);
                                CreateBone(bone1);
                                CreateBone(bone2);
                                bool p = true;
                                AddInstance(new TimeRangedEvent(bpm * 6, 20, () =>
                                {
                                    if (p)
                                    {
                                        PlaySound(Ding);
                                        bone1.ColorType = 1;
                                        bone2.ColorType = 1;
                                        AddInstance(new TimeRangedEvent(3, 1, () =>
                                        {
                                            bone1.ColorType = 0;
                                            bone2.ColorType = 0;
                                        }));
                                        p = false;
                                    }
                                    bone1.MissionLength = (84 * 1.5f / 4 * 1f - 10) * 0.1f + bone1.MissionLength * 0.9f;
                                    bone2.MissionLength = 84 * 1.5f - bone1.MissionLength - 32;
                                }));
                            }));
                        }
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 8 * 7 + bpm * 8, 1, () =>
                    {
                        for (int a = 0; a < 24; a++)
                        {
                            AddInstance(new TimeRangedEvent(bpm * 4 * a, 1, () =>
                            {
                                Knife(Heart.Centre, 0, false, bpm * 2, false);
                            }));
                        }
                        Color r1 = ColorReturn(Rand(0, 1));
                        Color r2 = ColorReturn(Rand(0, 1));
                        Color r3 = ColorReturn(Rand(0, 1));
                        Color r4 = ColorReturn(Rand(0, 1));
                        Color r5 = ColorReturn(Rand(0, 1));
                        Color r6 = ColorReturn(Rand(0, 1));
                        Color r7 = ColorReturn(Rand(0, 1));
                        Color r8 = ColorReturn(Rand(0, 1));
                        AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 1, 0, 640 / 9 * 1, 480, (int)(bpm * 8 * 15), 0, r1) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 7f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 2, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 2, 0, 640 / 9 * 2, 480, (int)(bpm * 8 * 15), 0, r2) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 6.5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 3, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 3, 0, 640 / 9 * 3, 480, (int)(bpm * 8 * 15), 0, r3) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 6f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 4, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 4, 0, 640 / 9 * 4, 480, (int)(bpm * 8 * 15), 0, r4) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 5.5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 5, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 5, 0, 640 / 9 * 5, 480, (int)(bpm * 8 * 15), 0, r5) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 6, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 6, 0, 640 / 9 * 6, 480, (int)(bpm * 8 * 15), 0f, r6) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 4.5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 7, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 7, 0, 640 / 9 * 7, 480, (int)(bpm * 8 * 15), 0, r7) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 4f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 8, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 8, 0, 640 / 9 * 8, 480, (int)(bpm * 8 * 15), 0, r8) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 3.5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 8, 1, () =>
                        {
                            PlaySound(pierce);
                            InstantSetBox(BoxStates.Centre + new Vector2(5, 0), 650, 84 * 1.5f);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r1) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 8.5f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r2) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 9f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r3) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 9.5f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r4) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 10f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r5) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 10.5f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r6) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 11f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r7) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 11.5f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r8) });
                        }));
                        /*for (int a = 1; a < 9; a++)
                        {
                            int b = a;
                            Color colorrand;
                            int rand = Rand(0, 1);
                            if (rand == 0) colorrand = Color.Cyan;
                            else colorrand = Color.Orange;
                            AddInstance(new TimeRangedEvent(bpm * 8 * a, 1, () =>
                            {
                                DrawLibrary.NormalLine line;
                                line = new(640 / 9 * b, 0, 640 / 9 * b, 480, 1000, 0.9f, colorrand) { depth = 0.003f };
                                CreateEntity(line);
                                AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                                {
                                    line.alpha -= 1 / 16 / bpm;
                                }));
                            }));
                        }*/
                    }));
                }
                public static void fight14()
                {
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        CreateEntity(new DownBonesea(30, 2, 84 * 1.5f / 2 - 13, true, 15, 1000) { tags = new string[] { "w" }, markscore = true });
                        CreateEntity(new UpBonesea(30, 2, 84 * 1.5f / 2 - 13, true, 15, 1000) { tags = new string[] { "w" }, markscore = true });
                        CreateEntity(new DownBonesea(30, 2, 84 * 1.5f / 2 - 13, false, 15, 1000) { tags = new string[] { "w" }, markscore = true });
                        CreateEntity(new UpBonesea(30, 2, 84 * 1.5f / 2 - 13, false, 15, 1000) { tags = new string[] { "w" }, markscore = true });
                    }));
                }
                public static void fight15()
                {
                    ScreenDrawing.BoxBackColor = Color.Transparent;
                    PlaySound(Ding);
                    PlaySound(giga);
                    SetSoul(2);
                    SpecialKnife(Heart.Centre, Rand(0, 360), false, bpm * 8, false);
                    SpecialKnife(Heart.Centre, Rand(0, 360), true, bpm * 8, true);
                    SetBox(BoxStates.Left - 10, BoxStates.Right + 10, BoxStates.Up - 10, BoxStates.Down + 10);
                    Heart.GiveForce(Rand(0, 3) * 90, 2);
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {

                        PlaySound(Ding);
                        PlaySound(giga);
                        ScreenDrawing.ScreenScale = 2f;
                        Knife(Heart.Centre, 0, false, bpm * 8, false);
                        SetBox(BoxStates.Left - 20, BoxStates.Right + 20, BoxStates.Up - 10, BoxStates.Down + 10);
                        Heart.GiveForce(Rand(0, 3) * 90, 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {

                        PlaySound(Ding);
                        PlaySound(giga);
                        ScreenDrawing.ScreenScale = 1.5f;
                        Knife(Heart.Centre, 90, true, bpm * 8, true);
                        Knife(Heart.Centre, 60, true, bpm * 8, true);
                        Knife(Heart.Centre, 120, false, bpm * 8, false);
                        SetBox(BoxStates.Left - 20, BoxStates.Right + 20, BoxStates.Up - 10, BoxStates.Down + 10);
                        Heart.GiveForce(Rand(0, 3) * 90, 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 48, 1, () =>
                    {
                        Vector2 vec = Heart.Centre;
                        PlaySound(Ding);
                        PlaySound(giga);
                        for (int a = 0; a < 2; a++) PlaySound(giga);
                        ScreenDrawing.ScreenScale = 1.25f;
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 60, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 60 + 360 / 8, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 60 + 360 / 8 * 2, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 60 + 360 / 8 * 3, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 60 + 360 / 8 * 4, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 60 + 360 / 8 * 5, false, bpm * 8, false);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 60 + 360 / 8 * 6, false, bpm * 8, false);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 60 + 360 / 8 * 7, false, bpm * 8, false);
                        SetBox(BoxStates.Left - 20, BoxStates.Right + 20, BoxStates.Up - 10, BoxStates.Down + 10);
                        Heart.GiveForce(Rand(0, 3) * 90, 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64, 1, () =>
                    {
                        Vector2 vec = Heart.Centre;
                        PlaySound(Ding);
                        PlaySound(giga);
                        for (int a = 0; a < 2; a++) PlaySound(giga);
                        ScreenDrawing.ScreenScale = 1f;
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 60, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 60 + 360 / 8, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 60 + 360 / 8 * 2, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 60 + 360 / 8 * 3, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 60 + 360 / 8 * 4, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 60 + 360 / 8 * 5, false, bpm * 8, false);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 60 + 360 / 8 * 6, false, bpm * 8, false);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 60 + 360 / 8 * 7, false, bpm * 8, false);
                        SetBox(BoxStates.Left - 30, BoxStates.Right + 30, BoxStates.Up - 10, BoxStates.Down + 10);
                        Heart.GiveForce(Rand(0, 3) * 90, 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 80, 1, () =>
                    {
                        Vector2 vec = Heart.Centre;
                        Vector2 vec2 = new(Rand(20, 620), Rand(20, 460));
                        PlaySound(Ding);
                        PlaySound(giga);
                        for (int a = 0; a < 3; a++) PlaySound(giga);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 60, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 60 + 360 / 8, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 60 + 360 / 8 * 2, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 60 + 360 / 8 * 3, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 60 + 360 / 8 * 4, true, bpm * 8, true);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 60 + 360 / 8 * 5, false, bpm * 8, false);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 60 + 360 / 8 * 6, false, bpm * 8, false);
                        SpecialKnife(vec + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 60 + 360 / 8 * 7, false, bpm * 8, false);
                        SetBox(BoxStates.Left - 30, BoxStates.Right + 30, BoxStates.Up - 10, BoxStates.Down + 10);
                        Heart.GiveForce(Rand(0, 3) * 90, 2);
                    }));
                }
                public static void fight16()
                {
                    for (int a = 0; a < 2; a++) PlaySound(Ding);
                    SetSoul(0);
                    Heart.GiveForce(0, 0);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, false);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 11, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        ScreenDrawing.ScreenScale = 1;
                        for (int b = 0; b < 2; b++) PlaySound(Warning);
                        for (int times = 1; times < 9; times++)
                        {
                            for (int a = 0; a < 8; a++) SpecialKnife(new(320, 240), MathF.Atan2(Heart.Centre.Y, 320), true, 0.5f, true);
                            AddInstance(new TimeRangedEvent(bpm * 2 * times, 1, () =>
                            {
                                for (int b = 0; b < 1; b++) PlaySound(largeKnife);
                                ScreenDrawing.ScreenAngle = times * 360 / 9;
                            }));
                        }
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + 9 * bpm * 2, 1, () =>
                    {
                        ScreenDrawing.BoxBackColor = Color.Black;
                        for (int a = 0; a < 512; a++) CreateEntity(new Particle(Color.White,
                                                new(Rand(-30, 30) / 10f, -Rand(3f, 6f)), Rand(10, 16), new(Rand(BoxStates.Left, BoxStates.Right), Rand(BoxStates.Up, BoxStates.Down)), Sprites.square)
                        {
                            AutoRotate = true
                        });
                        InstantSetBox(240, 645, 485);
                        MainEffects.Supershake();
                        DrawingUtil.Shock(0.5f, 1.5f, 1280);
                    }));
                }
                public static void fight17()
                {
                    for (int a = 0; a < 6; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * a, 1, () =>
                        {
                            WaveParticle(45, Rand(0, 2), 11);
                            PlaySound(pierce);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 1.2f, 1, () =>
                        {
                            WaveParticle(90, Rand(0, 2), 11);
                            PlaySound(pierce);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 2.4f, 1, () =>
                        {
                            WaveParticle(135, Rand(0, 2), 11);
                            PlaySound(pierce);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 3.6f, 1, () =>
                        {
                            WaveParticle(180, Rand(0, 2), 11);
                            PlaySound(pierce);
                        }));
                    }
                    AddInstance(new TimeRangedEvent(bpm * 5 * 16, 1, () =>
                    {
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 11, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                    }));
                    for (int a = 0; a < 4; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 6 * 16 + a * bpm * 4 + bpm * 4, 1, () =>
                        {
                            PlaySound(pierce);
                            for (int a = 0; a < 120; a++)
                            {
                                CreateBone(new CustomBone(new(-24, 480 / 48 * a), Motions.PositionRoute.linear, 90, 48) { PositionRouteParam = new float[] { 15f, 0 }, ColorType = 1 });
                            }
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 6 * 16 + a * bpm * 4 + bpm * 6, 1, () =>
                        {
                            PlaySound(pierce);
                            for (int a = 0; a < 120; a++)
                            {
                                CreateBone(new CustomBone(new(664, 480 / 48 * a), Motions.PositionRoute.linear, 90, 48) { PositionRouteParam = new float[] { -15f, 0 }, ColorType = 1 });
                            }
                        }));
                    }

                }
                public static void fight18()
                {
                    InstantTP(320, 240);
                    SetSoul(2);
                    InstantSetBox(240, 150, 150);
                    BlackScreen(10);
                    ResetBarrage();

                    Heart.GiveForce(270, 6);
                    PlaySound(Ding);
                    PlaySound(pierce);
                    CreateEntity(new Boneslab(270, 150 / 2 - 15, bpm * 4, bpm * 12));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(0, 6);
                        CreateEntity(new Boneslab(0, 150 / 2 - 15, bpm * 4, bpm * 8));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(90, 6);
                        CreateEntity(new Boneslab(90, 150 / 2 - 15, bpm * 4, bpm * 4));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        Heart.GiveForce(0, 6);
                        SetSoul(0);
                        Knife(new Vector2(320, 240 - 75 + 24), 0, false, 0, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(new Vector2(320, 240 - 75 + 48), 0, false, 0, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateEntity(new RotBone(84, 6, 45, true, 0));
                        CreateEntity(new RotBone(84, 6, 45, true, 1));
                        CreateEntity(new RotBone(84, 6, 45, true, 2));
                        CreateEntity(new RotBone(84, 6, 45, true, 3));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 2, 1, () =>
                    {
                        PlaySound(pierce);
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            CreateBone(new CentreCircleBone(25, 3, 60, bpm * 5));
                            CreateBone(new CentreCircleBone(25 + 90, 3, 60, bpm * 5) { ColorType = 1 });
                            CreateBone(new CentreCircleBone(25 + 45, 3, 60, bpm * 5));
                            CreateBone(new CentreCircleBone(25 + 135, 3, 60, bpm * 5) { ColorType = 2 });
                        }));
                        for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(70 + 360 / 12 * a, 3, 24, bpm * 12));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 4, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(false, 4.5f, 75 - 8));
                        CreateBone(new UpBone(true, 4.5f, 75 - 8));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 6, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 4.5f, 75 - 8));
                        CreateBone(new UpBone(false, 4.5f, 75 - 8));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240 - 43f), new Vector2(0, 240 - 43f), new Vector2(0.8f, 0.8f), 0, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240 - 43f), new Vector2(640, 240 - 43f), new Vector2(0.8f, 0.8f), 180, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240 + 43f), new Vector2(0, 240 + 43f), new Vector2(0.8f, 0.8f), 0, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240 + 43f), new Vector2(640, 240 + 43f), new Vector2(0.8f, 0.8f), 180, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 43f, 240 - 160), new Vector2(320 - 43f, 0), new Vector2(0.8f, 0.8f), 90, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 43f, 240 - 160), new Vector2(320 - 43f, 480), new Vector2(0.8f, 0.8f), 90, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 43f, 240 + 160), new Vector2(320 + 43f, 0), new Vector2(0.8f, 0.8f), 270, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 43f, 240 + 160), new Vector2(320 + 43f, 480), new Vector2(0.8f, 0.8f), 270, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240), new Vector2(0, 240), new Vector2(1, 0.4f), 0, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240), new Vector2(640, 240), new Vector2(1, 0.4f), 180, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320, 240 - 160), new Vector2(320, 0), new Vector2(1, 0.4f), 90, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320, 240 + 160), new Vector2(320, 0), new Vector2(1, 0.4f), 270, bpm * 8, bpm * 4));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 30, 1, () =>
                    {
                        CreateEntity(new Boneslab(0, 20, bpm * 4, bpm * 2));
                        CreateEntity(new Boneslab(90, 20, bpm * 4, bpm * 2));
                        CreateEntity(new Boneslab(180, 20, bpm * 4, bpm * 2));
                        CreateEntity(new Boneslab(270, 20, bpm * 4, bpm * 2));
                    }));
                    int rand = Rand(0, 1);
                    AddInstance(new TimeRangedEvent(bpm * 24, 1, () =>
                    {
                        if (rand == 1) CreateEntity(new DrawingUtil.MaskSquare(BoxStates.Right - 20, BoxStates.Up, 24, 150, (int)(bpm * 9), Color.Lerp(Color.Cyan, new Color(0, 0, 0, 0), 0.08f), 0.8f) { Depth = 0.99f });
                        if (rand == 0) CreateEntity(new DrawingUtil.MaskSquare(BoxStates.Left - 4, BoxStates.Up, 24, 150, (int)(bpm * 9), Color.Lerp(Color.Cyan, new Color(0, 0, 0, 0), 0.08f), 0.8f) { Depth = 0.99f });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        PlaySound(Ding);
                        SetSoul(2);
                        Heart.GiveForce(rand * 180 + 90, 6);
                        PlaySound(pierce);
                    }));
                }
                public static void fight19()
                {
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                        CreateEntity(new Boneslab(90, 150 / 2 - 24, bpm * 6, bpm * 4));
                        CreateEntity(new Boneslab(270, 150 / 2 - 24, bpm * 6, bpm * 4));
                        CreateEntity(new Boneslab(0, 150 / 2 - 24, bpm * 8, bpm * 2));
                        CreateEntity(new Boneslab(180, 150 / 2 - 24, bpm * 8, bpm * 2));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 10, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(0, 6);
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            PlaySound(Ding);
                            PlaySound(pierce);
                        }));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(new(320, 240), 0, true, bpm * 2, false);
                        Knife(new(320, 240), 90, true, bpm * 2, true);
                        Knife(new(320, 240), 180, true, bpm * 2, true);
                        Knife(new(320, 240), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        Knife(new(BoxStates.Left, BoxStates.Up), 0, true, bpm * 2, false);
                        Knife(new(BoxStates.Left, BoxStates.Down), 90, true, bpm * 2, true);
                        Knife(new(BoxStates.Right, BoxStates.Down), 180, true, bpm * 2, true);
                        Knife(new(BoxStates.Right, BoxStates.Up), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 18, 1, () =>
                    {
                        Knife(new(320, 240), 0, true, bpm * 2, true);
                        Knife(new(320, 240), 90, true, bpm * 2, true);
                        Knife(new(320, 240), 180, true, bpm * 2, true);
                        Knife(new(320, 240), 270, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 22, 1, () =>
                    {
                        SetSoul(0);
                        PlaySound(Ding);
                        CreateBone(new CentreCircleBone(75, 4.3f, 150, bpm * 6));
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () => { for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(34 + 360 / 12 * a, 6.3f, 16, bpm * 6)); }));
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 3.8f, 75 - 2) { ColorType = 2 });
                        CreateBone(new RightBone(true, 3.8f, 75 - 2) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 26, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new RightBone(true, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new UpBone(true, 5.8f, 150) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 28, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new RightBone(true, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new UpBone(true, 5.8f, 150) { ColorType = 2 });
                    }));
                }
                public static void fight18_1()
                {
                    InstantTP(320, 240);
                    SetSoul(2);
                    InstantSetBox(240, 150, 150);
                    BlackScreen(10);
                    ResetBarrage();

                    Heart.GiveForce(270, 6);
                    PlaySound(Ding);
                    PlaySound(pierce);
                    CreateEntity(new Boneslab(270, 150 / 2 - 15, bpm * 4, bpm * 12));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(0, 6);
                        CreateEntity(new Boneslab(0, 150 / 2 - 15, bpm * 4, bpm * 8));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(90, 6);
                        CreateEntity(new Boneslab(90, 150 / 2 - 15, bpm * 4, bpm * 4));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        Heart.GiveForce(0, 6);
                        SetSoul(0);
                        Knife(new Vector2(320, 240 - 75 + 24), 0, false, 0, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(new Vector2(320, 240 - 75 + 48), 0, false, 0, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateEntity(new RotBone(84, 6, 45, true, 0));
                        CreateEntity(new RotBone(84, 6, 45, true, 1));
                        CreateEntity(new RotBone(84, 6, 45, true, 2));
                        CreateEntity(new RotBone(84, 6, 45, true, 3));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 2, 1, () =>
                    {
                        PlaySound(pierce);
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            CreateBone(new CentreCircleBone(25, 3, 60, bpm * 5));
                            CreateBone(new CentreCircleBone(25 + 90, 3, 60, bpm * 5) { ColorType = 1 });
                            CreateBone(new CentreCircleBone(25 + 45, 3, 60, bpm * 5));
                            CreateBone(new CentreCircleBone(25 + 135, 3, 60, bpm * 5) { ColorType = 2 });
                        }));
                        for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(70 + 360 / 12 * a, 3, 24, bpm * 12));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 4, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(false, 4.5f, 75 - 8));
                        CreateBone(new UpBone(true, 4.5f, 75 - 8));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 6, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 4.5f, 75 - 8));
                        CreateBone(new UpBone(false, 4.5f, 75 - 8));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240 - 43f), new Vector2(0, 240 - 43f), new Vector2(0.8f, 0.8f), 0, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240 - 43f), new Vector2(640, 240 - 43f), new Vector2(0.8f, 0.8f), 180, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240 + 43f), new Vector2(0, 240 + 43f), new Vector2(0.8f, 0.8f), 0, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240 + 43f), new Vector2(640, 240 + 43f), new Vector2(0.8f, 0.8f), 180, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 43f, 240 - 160), new Vector2(320 - 43f, 0), new Vector2(0.8f, 0.8f), 90, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 43f, 240 - 160), new Vector2(320 - 43f, 480), new Vector2(0.8f, 0.8f), 90, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 43f, 240 + 160), new Vector2(320 + 43f, 0), new Vector2(0.8f, 0.8f), 270, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 43f, 240 + 160), new Vector2(320 + 43f, 480), new Vector2(0.8f, 0.8f), 270, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240), new Vector2(0, 240), new Vector2(1, 0.4f), 0, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240), new Vector2(640, 240), new Vector2(1, 0.4f), 180, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320, 240 - 160), new Vector2(320, 0), new Vector2(1, 0.4f), 90, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320, 240 + 160), new Vector2(320, 0), new Vector2(1, 0.4f), 270, bpm * 8, bpm * 4));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 30, 1, () =>
                    {
                        CreateEntity(new Boneslab(0, 20, bpm * 6, bpm * 2));
                        CreateEntity(new Boneslab(90, 20, bpm * 6, bpm * 2));
                        CreateEntity(new Boneslab(180, 20, bpm * 6, bpm * 2));
                        CreateEntity(new Boneslab(270, 20, bpm * 6, bpm * 2));
                    }));
                    int rand = Rand(0, 1);
                    AddInstance(new TimeRangedEvent(bpm * 24, 1, () =>
                    {
                        if (rand == 1) CreateEntity(new DrawingUtil.MaskSquare(BoxStates.Right - 20, BoxStates.Up, 24, 150, (int)(bpm * 9), Color.Lerp(Color.Cyan, new Color(0, 0, 0, 0), 0.08f), 0.8f) { Depth = 0.99f });
                        if (rand == 0) CreateEntity(new DrawingUtil.MaskSquare(BoxStates.Left - 4, BoxStates.Up, 24, 150, (int)(bpm * 9), Color.Lerp(Color.Cyan, new Color(0, 0, 0, 0), 0.08f), 0.8f) { Depth = 0.99f });
                        DrawingUtil.MaskSquare[] mark = GetAll<DrawingUtil.MaskSquare>();
                        for (int a = 0; a < mark.Length; a++) mark[a].alpha -= 1 / bpm * 9;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        PlaySound(Ding);
                        SetSoul(2);
                        Heart.GiveForce(rand * 180 + 90, 6);
                        PlaySound(pierce);
                    }));
                }
                public static void fight19_1()
                {
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                        CreateEntity(new Boneslab(90, 150 / 2 - 24, bpm * 6, bpm * 4));
                        CreateEntity(new Boneslab(270, 150 / 2 - 24, bpm * 6, bpm * 4));
                        CreateEntity(new Boneslab(0, 150 / 2 - 24, bpm * 8, bpm * 2));
                        CreateEntity(new Boneslab(180, 150 / 2 - 24, bpm * 8, bpm * 2));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 10, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(0, 6);
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            PlaySound(Ding);
                            PlaySound(pierce);
                        }));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(new(320, 240), 0, true, bpm * 2);
                        Knife(new(320, 240), 90, true, bpm * 2, true);
                        Knife(new(320, 240), 180, true, bpm * 2, true);
                        Knife(new(320, 240), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        Knife(new(BoxStates.Left, BoxStates.Up), 0, true, bpm * 2, true);
                        Knife(new(BoxStates.Left, BoxStates.Down), 90, true, bpm * 2, true);
                        Knife(new(BoxStates.Right, BoxStates.Down), 180, true, bpm * 2, true);
                        Knife(new(BoxStates.Right, BoxStates.Up), 270, true, bpm * 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 18, 1, () =>
                    {
                        Knife(new(320, 240), 0, true, bpm * 2);
                        Knife(new(320, 240), 90, true, bpm * 2, true);
                        Knife(new(320, 240), 180, true, bpm * 2, true);
                        Knife(new(320, 240), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 22, 1, () =>
                    {
                        SetSoul(0);
                        PlaySound(Ding);
                        CreateBone(new CentreCircleBone(75, 4.3f, 150, bpm * 6));
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () => { for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(34 + 360 / 12 * a, 6.3f, 16, bpm * 6)); }));
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 3.8f, 75 - 2) { ColorType = 2 });
                        CreateBone(new RightBone(true, 3.8f, 75 - 2) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 26, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new RightBone(true, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new UpBone(true, 5.8f, 150) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 28, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new RightBone(true, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new UpBone(true, 5.8f, 150) { ColorType = 2 });
                    }));
                }
                public static void fight20()
                {
                    HeartAttribute.Speed = 3f;
                    for (int a = 0; a < 96 / 2.5f; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * a * 8, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 6, 40));
                        CreateBone(new UpBone(true, 6, 40));
                    }));
                        AddInstance(new TimeRangedEvent(bpm * a * 8 + bpm * 2, 1, () =>
                        {
                            Knife(Heart.Centre, 270, false, bpm * 4);
                            PlaySound(pierce);
                            CreateBone(new DownBone(true, 7, 200) { ColorType = 1 });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * a * 8 + bpm * 4, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new(BoxStates.Right, BoxStates.Centre.Y), Motions.PositionRoute.linear, 0, 60) { PositionRouteParam = new float[] { -6, 0 } });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * a * 8 + bpm * 6, 1, () =>
                        {
                            Knife(Heart.Centre, 270, false, bpm * 4);
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 8, 200) { ColorType = 2 });
                        }));
                    }
                }

                public static Color ColorReturn(int into)
                {
                    return into == 0 ? Color.Cyan : Color.Orange;
                }
                public static int ColorReturn(Color into)
                {
                    return into == Color.Cyan ? 1 : into == Color.Orange ? 2 : 0;
                }
            }
            public static class Hardfight
            {
                public static void Knife(Vector2 center, float rotate, bool trafic, float trafictime)
                {
                    DrawingUtil.Linerotatelong head = new(center.X, center.Y, rotate + 180, trafictime, 1, 34, new(255, 61, 207));
                    head.width = 7;
                    DrawingUtil.Linerotatelong end = new(center.X + Cos(rotate) * 10, center.Y + Sin(rotate) * 10, rotate, trafictime, 1, 8, new(255, 61, 207));
                    end.width = 7;
                    CreateEntity(head);
                    CreateEntity(end);
                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        head.Dispose();
                        end.Dispose();
                    }));
                    if (!trafic)
                    {
                        PlaySound(Warning);
                    }

                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        DrawingUtil.Shock(1.2f, 1.3f, 3);
                        PlaySound(largeKnife, 0.7f);
                        DrawingUtil.Linerotatelong Line = new(center.X + Cos(rotate) * 640 * 1.25f, center.Y + Sin(rotate) * 640 * 1.25f, rotate + 180, bpm * 32, 1, 640 * 2.5f, new(189, 44, 153));
                        Line.width = 0;
                        CreateEntity(Line);
                        AddInstance(new TimeRangedEvent(5, bpm * 9, () =>
                        {
                            Line.width = Line.width * 0.7f + 25 * 0.3f;
                            Line.alpha -= 1 / bpm / 4;
                        }));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateSpear(new Pike(center + new Vector2(Cos(rotate + 180) * (128 + a * 33), Sin(rotate + 180) * (128 + a * 33)), rotate, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                            CreateSpear(new Pike(center + new Vector2(Cos(rotate) * (128 + a * 33), Sin(rotate) * (128 + a * 33)), rotate + 180, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                        }
                    }));

                }
                public static void Knife(Vector2 center, float rotate, bool trafic, float trafictime, bool value)
                {

                    DrawingUtil.Linerotatelong head = new(center.X, center.Y, rotate + 180, trafictime, 1, 34, new(255, 61, 207));
                    head.width = 7;
                    DrawingUtil.Linerotatelong end = new(center.X + Cos(rotate) * 10, center.Y + Sin(rotate) * 10, rotate, trafictime, 1, 8, new(255, 61, 207));
                    end.width = 7;
                    CreateEntity(head);
                    CreateEntity(end);
                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        head.Dispose();
                        end.Dispose();
                    }));
                    if (!trafic)
                    {
                        PlaySound(Warning);
                    }
                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        if (!value) PlaySound(largeKnife, 0.7f);
                        DrawingUtil.Linerotatelong Line = new(center.X + Cos(rotate) * 640 * 1.25f, center.Y + Sin(rotate) * 640 * 1.25f, rotate + 180, bpm * 32, 1, 640 * 2.5f, new(189, 44, 153));
                        Line.width = 0;
                        CreateEntity(Line);
                        DrawingUtil.Shock(1.2f, 1.3f, 3);
                        AddInstance(new TimeRangedEvent(5, bpm * 9, () =>
                        {
                            Line.width = Line.width * 0.7f + 25 * 0.3f;
                            Line.alpha -= 1 / bpm / 4;
                        }));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateSpear(new Pike(center + new Vector2(Cos(rotate + 180) * (128 + a * 33), Sin(rotate + 180) * (128 + a * 33)), rotate, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                            CreateSpear(new Pike(center + new Vector2(Cos(rotate) * (128 + a * 33), Sin(rotate) * (128 + a * 33)), rotate + 180, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                        }
                    }));

                }
                public static void SpecialKnife(Vector2 center, float rotate, bool trafic, float trafictime, bool value)
                {
                    DrawingUtil.Linerotate head = new(center.X, center.Y, rotate + 180, trafictime, 1, Color.Red * 0.75f);
                    head.depth = 1f;
                    head.width = 1.5f;
                    CreateEntity(head);
                    AddInstance(new TimeRangedEvent(trafictime, 1, () =>
                    {
                        head.Dispose();
                    }));
                    float sin = 90;
                    float rot = 0;
                    float rand = Rand(-1.0f, 1.0f);
                    AddInstance(new TimeRangedEvent(0, trafictime - 2, () =>
                    {
                        sin += 90 / (trafictime - 2);
                        head.rotate = rot;
                        rot = rotate;
                    }));
                    if (!trafic)
                    {
                        PlaySound(Warning);
                    }
                    AddInstance(new TimeRangedEvent(trafictime + 1, 1, () =>
                    {
                        if (!value) PlaySound(largeKnife, 0.7f);
                        DrawingUtil.Linerotate Line = new(center.X, center.Y, rot, bpm * 32, 1, Color.Red * 0.75f);
                        Line.width = 0;
                        Line.depth = 1;
                        CreateEntity(Line);
                        DrawingUtil.Shock(1.2f, 1.3f, 3);
                        AddInstance(new TimeRangedEvent(5, bpm * 9, () =>
                        {
                            Line.width = Line.width * 0.7f + 25 * 0.3f;
                            Line.alpha -= 1 / bpm / 4;
                        }));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateSpear(new Pike(center + new Vector2(Cos(rot + 180) * (128 + a * 33), Sin(rot + 180) * (128 + a * 33)), rot, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                            CreateSpear(new Pike(center + new Vector2(Cos(rot) * (128 + a * 33), Sin(rot) * (128 + a * 33)), rot + 180, 15 + a * 1.2f, 1) { IsShootMute = true, IsSpawnMute = true, DrawingColor = new(0, 0, 0, 0) });
                        }
                    }));

                }
                public static void WaveParticle(float rot)
                {
                    int rand = Rand(0, 90);
                    for (int a = 0; a < 7; a++)
                    {
                        CreateBone(new CustomBone(new(320, 240), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                        {
                            PositionRouteParam = new float[] { Cos(360 / 5 * a + rot) * 3.9f, Sin(360 / 5 * a + rot) * 3.9f },
                            LengthRouteParam = new float[] { 3, 256 },
                            RotationRouteParam = new float[] { 0, 360 / 5 * a +90+rand+rot
},
                        });
                    }
                }
                public static void WaveParticle(float rot, int color, int distance)
                {
                    for (int a = 0; a < distance; a++)
                    {
                        CreateBone(new CustomBone(new(320 - Cos(360 / distance * a + rot) * 12f, 240 - Sin(360 / distance * a + rot) * 12f), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                        {
                            PositionRouteParam = new float[] { Cos(360 / distance * a + rot) * 4.2f, Sin(360 / distance * a + rot) * 4.2f },
                            LengthRouteParam = new float[] { 16, 256 },
                            RotationRouteParam = new float[] { 0, 360 / distance * a +90+rot

},
                            ColorType = color
                        });
                    }
                }
                public static void randturn()
                {
                    int[] turn = new[] { 1, 2, 3, 4, 5, 6 };
                    for (int a = 0; a < 10; a++)
                    {
                        int rand1 = Rand(0, 5);
                        int rand2 = Rand(0, 5);
                        int change = turn[rand1];
                        turn[rand1] = turn[rand2];
                        turn[rand2] = change;
                    }
                    for (int a = 0; a < 6; a++)
                    {
                        int value = a;
                        AddInstance(new TimeRangedEvent(a * bpm * 16, 1, () =>
                        {
                            TextPrinter tex1 = new((int)(bpm * 4), "$$$" + turn[value].ToString(), new Vector2(320 - 40, 240 - 80), new TextAttribute[]
                            {
                                new TextFadeoutAttribute(0,bpm*8),
                                new TextColorAttribute(Color.Lerp(new Color(255,22,36),new Color(0,0,0),0.15f)),
                                new TextSizeAttribute(5f),


                            });
                            tex1.Depth = 0.99f;
                            CreateEntity(tex1);
                            PlaySound(Ding);
                        }));
                        AddInstance(new TimeRangedEvent(a * bpm * 16 + bpm * 4, 1, () =>
                        {
                            if (turn[value] == 1) fight4();
                            if (turn[value] == 2) fight5();
                            if (turn[value] == 3) fight6();
                            if (turn[value] == 4) fight7();
                            if (turn[value] == 5) fight8();
                            if (turn[value] == 6) fight9();
                        }));
                    }
                }
                public static void fight1()
                {
                    for (int a = 0; a < 4 * 2 * 1.70f; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 8 * a, 1, () =>
                        {
                            CreateBone(new DownBone(true, 2.9f, 44));
                        }));
                    }
                    for (int a = 0; a < 16 * 4 * 7 * 1.13f * 1.5f; a++)
                    {
                        AddInstance(new TimeRangedEvent(1f * a, 1, () =>
                        {
                            DownBone bone = new(false, 13, 13) { MarkScore = false };
                            CreateBone(bone);
                        }));
                    }
                    InstantSetBox(280, 400, 150);
                    for (int a = 0; a < 14; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 8 * a + bpm * 6, 1, () =>
                        {
                            Explusfight.Knife(Heart.Centre, 90, false, bpm * 2);
                        }));
                    }
                    CreatePlatform(new Platform(0, new(320 - 25, 280 + 150 / 2 - 32), Motions.PositionRoute.linear, 0, 50)
                    {
                        PositionRouteParam = new float[] { (320 + 50) / 2 / bpm / 32, 0 }
                    });
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        CreatePlatform(new Platform(0, new(320 + 25 + 400 / 2, 280 + 150 / 2 - 32), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { -(320 + 50) / bpm / 32, 0 }
                        });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32 + 32 * bpm, 1, () =>
                    {
                        CreatePlatform(new Platform(0, new(320 - 25 - 400 / 2, 280 + 150 / 2 - 32), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { (320 + 50) / bpm / 32, 0 }
                        });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64 + 32 * bpm, 1, () =>
                    {
                        CreatePlatform(new Platform(0, new(320 + 25 + 400 / 2 + 25, 280 + 150 / 2 - 32), Motions.PositionRoute.linear, 0, 50)
                        {
                            PositionRouteParam = new float[] { -480 / 2 / bpm / 16, 0 }
                        });
                    }));
                    //   Boneslab boneslab1 = new(0, 10, 0, (int)(bpm * 128), Motions.LengthRoute.autoFold,Motions.PositionRoute.);
                }
                public static void fight1_1()
                {
                    AddInstance(new InstantEvent(bpm * 32 + bpm * 8, () =>
                    {
                        for (int a = 0; a < 20; a++)
                        {
                            float length = (90 + 45) / bpm * a;
                            DownBone bone2 = new(true, 520 + a * 3.75f * 4.5f, 4.5f, 150 - 60 - Sin(length) * 32 - 64);
                            CreateBone(bone2);
                        }
                        CreateBone(new DownBone(true, 520 + 26.5f * 3.75f * 4.5f, 3.5f, 80) { ColorType = 1 });
                    }));
                    for (int a = 1; a < 8; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 32 + bpm * 12 * a, 1, () =>
                        {
                            CreateBone(new UpBone(false, 4, 110 - 16 - 22));
                        }));
                    }
                }
                public static void fight2()
                {

                    CreateGB(new NormalGB(new(320 - 70, 240 - 130), new(320 - 70, 330), new(1, 0.45f), bpm * 14, bpm * 3));
                    AddInstance(new TimeRangedEvent(bpm * 3, 1, () =>
                    {
                        CreateGB(new NormalGB(new(320 + 70, 240 - 130), new(320 + 70, 330), new(1, 0.45f), bpm * 14 - bpm * 3, bpm * 3));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        CreateGB(new NormalGB(new(320 - 70, 240 - 130), new(320 - 70, 330), new(1, 0.45f), bpm * 16 - bpm * 6, bpm * 3));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 9, 1, () =>
                    {
                        CreateGB(new NormalGB(new(320 + 70, 240 - 130), new(320 + 70, 330), new(1, 0.45f), bpm * 16 - bpm * 9, bpm * 3));
                    }));
                    /*float count = 0;
                    for (int a = 0; a < 4; a++)
                    {
                        if (a % 4 == 0) count = bpm * 5;
                        if (a % 4 == 1) count = 0;
                        if (a % 4 == 2) count = bpm * 5;
                        if (a % 4 == 3) count = 0;
                        AddInstance(new TimeRangedEvent(bpm * 3 * a, 1, () =>
                        {
                            CreateGB(new NormalGB(new(320 - 70, 240-130), new(320 - 70, 330), new(1, 0.45f), bpm * 16 - bpm * 3 * a+count, bpm * 3));
                            CreateGB(new NormalGB(new(320 + 70, 240-130), new(320 + 70, 330), new(1, 0.45f), bpm * 16 - bpm * 3 * a+count, bpm * 3));
                        }));
                    }*/

                }
                public static void fight3()
                {

                    for (int a = 0; a < 8; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 8, 1, () =>
                        {
                            // float speed = 45;
                            LeftBone bone1 = new(false, 3.1f, 82 - 8);
                            CreateBone(bone1);
                            /*AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                            {
                                bone1.MissionLength = 45+Sin(speed) * 27;
                                speed += 360 / bpm / 12;
                            }));*/
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16 * (a + 1), 1, () =>
                        {
                            //  float speed = 45;
                            RightBone bone1 = new(false, 3.1f, 82 - 8);
                            CreateBone(bone1);
                            /*AddInstance(new TimeRangedEvent(0, bpm * 32, () =>
                            {
                                bone1.MissionLength = 45+Sin(speed) * 27;
                                speed += 360 / bpm / 12;
                            }));*/
                        }));
                    }
                    for (int a = 0; a < 7; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * (a + 1) + bpm * 16, 1, () =>
                        {
                            Explusfight.Knife(Heart.Centre, 0, false, bpm * 4);
                        }));
                    }
                    for (int a = 0; a < 9; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * a, 1, () =>
                        {
                            CreatePlatform(new Platform(0, new Vector2(320, 110), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                            {
                                PositionRouteParam = new float[] { 0, 1.45f },
                                LengthRouteParam = new float[] { 60, 180 },
                                RotationRouteParam = new float[] { 35, bpm * 16, 0, 0 }
                            });
                        }));

                    }
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        for (int a = 0; a < 16 * 2 * 7 + 50; a++)
                        {
                            AddInstance(new TimeRangedEvent(3 * a, 1, () =>
                            {
                                DownBone bone = new(true, 6.3f, 20) { MarkScore = false };
                                CreateBone(bone);
                            }));
                        }
                    }));
                }
                public static void fight3_3()
                {
                    for (int a = 1; a < 8; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 8, 1, () =>
                        {
                            PlaySound(pierce);
                            float count = 90;
                            DownBone bone1 = new(false, 0, 258) { ColorType = 2 };
                            CreateBone(bone1);
                            AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                            {
                                bone1.Speed = Sin(count) * 4.2f;

                            }));
                        }));
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * (a + 1), 1, () =>
                        {
                            PlaySound(pierce);
                            float count = 90;
                            DownBone bone1 = new(true, 0, 258) { ColorType = 2 };
                            CreateBone(bone1);
                            AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                            {
                                bone1.Speed = Sin(count) * 4.2f;

                            }));
                        }));
                    }
                }
                public static void fight4()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    InstantSetBox(new Vector2(410, 280), 150, 150);
                    InstantTP(410, 280);
                    SetSoul(0);
                    Knife(new(410 + 25, 280), 90, true, 0);
                    Knife(new(410 - 25, 280), 90, true, 0, true);
                    Knife(new(410 + 25, 280), 90 + 180, true, 0, true);
                    Knife(new(410 - 25, 280), 90 + 180, true, 0, true);
                    Knife(new(410, 280), 0, true, bpm * 8);
                    Knife(new(410, 280), 90, true, bpm * 8, true);
                    AddInstance(new InstantEvent(bpm * 8, () =>
                    {
                        for (int a = 0; a < 2; a++) PlaySound(pierce);
                        CreateBone(new DownBone(false, 410 - 75, 8, 148) { ColorType = 1 });
                        CreateBone(new DownBone(false, 410 - 75 - 9, 8, 148) { ColorType = 1 });
                        CreateBone(new DownBone(true, 410 + 75, 8, 148) { ColorType = 1 });
                        CreateBone(new DownBone(true, 410 + 75 + 9, 8, 148) { ColorType = 1 });
                    }));
                }
                public static void fight5()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    InstantSetBox(new Vector2(240, 260), 130, 130);
                    InstantTP(240, 260);
                    SetSoul(0);
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(200, 0), Heart.Centre + new Vector2(200, 0), new Vector2(1, 1), 180, bpm * 8, bpm * 7));
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(-200, 0), Heart.Centre + new Vector2(200, 0), new Vector2(1, 1), 0, bpm * 8, bpm * 7));
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(0, 200), Heart.Centre + new Vector2(200, 0), new Vector2(1, 1), 270, bpm * 8, bpm * 7));
                    CreateGB(new NormalGB(Heart.Centre + new Vector2(0, -200), Heart.Centre + new Vector2(200, 0), new Vector2(1, 1), 90, bpm * 8, bpm * 7));
                    for (int a = 0; a < 2; a++) PlaySound(pierce);
                    CreateBone(new DownBone(false, 5, 130 / 2 - 18));
                    CreateBone(new LeftBone(false, 5, 130 / 2 - 18));
                    CreateBone(new UpBone(true, 5, 130 / 2 - 18));
                    CreateBone(new RightBone(true, 5, 130 / 2 - 18));
                    CreateEntity(new RotBone(MathF.Sqrt(130 * 130 * 2) / 2 - 32, 7, 45, false, 0));
                    CreateEntity(new RotBone(MathF.Sqrt(130 * 130 * 2) / 2 - 32, 7, 45, false, 1));
                    CreateEntity(new RotBone(MathF.Sqrt(130 * 130 * 2) / 2 - 32, 7, 45, false, 2));
                    CreateEntity(new RotBone(MathF.Sqrt(130 * 130 * 2) / 2 - 32, 7, 45, false, 3));
                    CreateEntity(new Boneslab(0, 7, 1, bpm * 15));
                    CreateEntity(new Boneslab(90, 7, 1, bpm * 15));
                    CreateEntity(new Boneslab(180, 7, 1, bpm * 15));
                    CreateEntity(new Boneslab(270, 7, 1, bpm * 15));
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 20f, 150) { ColorType = 1 });
                        CreateBone(new DownBone(false, 20f, 150) { ColorType = 1 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 13, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 20f, 150) { ColorType = 1 });
                        CreateBone(new DownBone(false, 20f, 150) { ColorType = 1 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 5f, 150) { ColorType = 1 });
                        CreateBone(new DownBone(false, 5f, 150) { ColorType = 1 });
                    }));
                }
                public static void fight6()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    SetSoul(2);
                    InstantSetBox(new Vector2(320, 320), 130, 130);
                    InstantTP(320, 320 + 65 - 8);

                    PlaySound(pierce);
                    CreateBone(new UpBone(true, 5f, 90));
                    CreateBone(new UpBone(false, 5f, 90));
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new UpBone(true, 5f, 90));
                        CreateBone(new UpBone(false, 5f, 90));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 5f, 40));
                        CreateBone(new DownBone(false, 5f, 40));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        SetSoul(0);
                        CreateBone(new DownBone(true, 6, 24));
                        CreateBone(new DownBone(false, 6, 24));
                        CreateBone(new UpBone(true, 6, BoxStates.Height - 24) { ColorType = 2 });
                        CreateBone(new UpBone(false, 6, BoxStates.Height - 24) { ColorType = 2 });

                        CreateEntity(new Boneslab(90, 130 / 2 - 16, 1, bpm * 4));
                        CreateEntity(new Boneslab(270, 130 / 2 - 16, 1, bpm * 4));
                    }));
                }
                public static void fight7()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    InstantSetBox(new Vector2(430, 230), 150, 150);
                    InstantTP(430, 230);
                    SetSoul(0);

                    for (int a = 0; a < 4; a++)
                    {
                        for (int b = 0; b < 3; b++) { int rotate = Rand(180 + 45, 270 + 10); CreateSpear(new Pike(new(Heart.Centre.X + Cos(rotate) * 200, Heart.Centre.Y + Sin(rotate) * 200), rotate + 180, 12, bpm * 14 - bpm * a) { DrawingColor = new(189, 44, 153) }); }
                        AddInstance(new TimeRangedEvent(bpm * 14 - bpm * a, 1, () =>
                        {
                            PlaySound(spearShoot);
                        }));
                    }
                    for (int a = 0; a < 12; a++)
                    {
                        CreateBone(new SideCircleBone(360 / 12 * a + 13, 10, 24, bpm * 7));
                    }
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        CreateBone(new CentreCircleBone(60, 3.2f, 190, bpm * 5.5f) { IsMasked = true });
                    }));

                }
                public static void fight8()
                {
                    ResetBarrage();
                    Heart.GiveForce(0, 6);
                    Player.Heart a = Heart.Split();
                    SetBoxMission(0);
                    InstantSetBox(new Vector2(405, 230), 65, 150);
                    SetBoxMission(1);
                    InstantSetBox(new Vector2(398 + 65 + 34 - 5, 230), 65, 150);
                    SetPlayerMission(0);
                    InstantTP(405, 230);
                    SetSoul(0);
                    SetPlayerMission(1);
                    InstantTP(398 + 65 + 34 - 5, 230);
                    Heart.GiveForce(180, 6);
                    SetSoul(0);
                    SetPlayerMission(0);
                    AddInstance(new InstantEvent(bpm * 16 - bpm * 2, () =>
                    {
                        Heart.Merge(a);
                        Heart.GiveForce(0, 6);
                        InstantTP(400, 240);
                        SetPlayerMission(1);
                        TP(Heart.Centre);
                    }));
                    PlaySound(pierce);
                    SetBoxMission(0);
                    CreateEntity(new Boneslab(90, 65 / 2 - 4, 2, bpm * 1 - 1));
                    SetBoxMission(1);
                    CreateEntity(new Boneslab(270, 65 / 2 - 4, 2, bpm * 1 - 1));
                    AddInstance(new InstantEvent(bpm * 2, () =>
                    {
                        PlaySound(pierce);
                        Knife(new Vector2(405 + 43.5f, 230), 0, true, 0, false);
                        Knife(new Vector2(405 + 43.5f, 230), 180, true, 0, true);
                        SetBoxMission(0);
                        CreateEntity(new Boneslab(90, 65 / 2 - 4, 2, bpm * 1 - 1));
                        SetBoxMission(1);
                        CreateEntity(new Boneslab(270, 65 / 2 - 4, 2, bpm * 1 - 1));
                    }));
                    AddInstance(new InstantEvent(bpm * 4, () =>
                    {
                        PlaySound(pierce);
                        Knife(new Vector2(405 + 43.5f, 230), 90, true, 0);
                        SetBoxMission(0);
                        CreateEntity(new Boneslab(90, 65 / 2 - 4, 2, bpm * 1 - 1));
                        SetBoxMission(1);
                        CreateEntity(new Boneslab(270, 65 / 2 - 4, 2, bpm * 1 - 1));
                    }));
                    AddInstance(new InstantEvent(bpm * 8, () =>
                    {
                        SetBoxMission(0);
                        CreateEntity(new Boneslab(0, 150 - 22, bpm, bpm * 3.5f));
                        SetBoxMission(1);
                        CreateEntity(new Boneslab(180, 150 - 22, bpm, bpm * 3.5f));
                    }));
                }
                public static void fight9()
                {
                    ResetBarrage();
                    SetSoul(0);
                    for (int a = 0; a < 36; a++)
                        CreateBone(new SideCircleBone(a * 10, 5, 150 - 48, bpm * 12.5f));

                    Heart.GiveForce(0, 6);
                    InstantTP(400, 240);
                    InstantSetBox(Heart.Centre, 150, 150);
                    AddInstance(new TimeRangedEvent(1, bpm * 16 - 2, () =>
                    {
                        InstantSetBox(BoxStates.Centre - new Vector2(2.5f, 0), 150, 150);
                    }));


                }
                public static void fight10()
                {
                    Heart.GiveForce(0, 6);
                    ResetBarrage();
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 9, BoxStates.Width) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(true, 9, BoxStates.Width) { ColorType = 1 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 9, BoxStates.Width) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 11, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(true, 9, BoxStates.Width) { ColorType = 1 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 9, BoxStates.Width) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        float rotate = 270;
                        for (int a = 0; a < 8; a++)
                        {
                            Vector2 center = Heart.Centre;
                            CreateGB(new NormalGB(center + new Vector2(Cos(rotate) * 140, Sin(rotate) * 140), center + new Vector2(Cos(rotate) * 140, Sin(rotate) * 140), new(0.75f, 0.75f), rotate + 90, bpm * 18 - a * 2 * bpm, (a + 1) * 2 * bpm));
                            rotate += 360 / 8;
                        }
                    }));
                    for (int a = 0; a < 9; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 14 + a * 2 * bpm, 1, () =>
                        {
                            CreateBone(new LeftBone(false, 9, BoxStates.Width) { ColorType = a % 2 + 1 });
                            CreateBone(new LeftBone(true, 9, BoxStates.Width) { ColorType = a % 2 + 1 });
                        }));
                    }
                }

                public static void greensoul()
                {
                    Heart.GiveForce(0, 6);
                    float beat = 0;
                    string[] arrow =
                        {
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","$2",

                "/","$2","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","$2",

                "/","$2","/","/",     "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",

                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","$2",

                "/","$2","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","/",

                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "$0","/","/","/",    "/","/","/","/",
                "/","/","/","/",    "/","/","/","$2",

                "/","$2","/","/",     "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
                "$2","/","/","/",    "$0","/","/","/",
            };
                    for (int i = 0; i < arrow.Length; i++)
                    {
                        if (arrow[i] == "/") beat += bpm * 0.5f;
                        else
                        {
                            for (int a = 0; a < 2; a++)
                            {
                                CreateArrow(bpm * 16 + beat, arrow[i], 5.5f, 0, 0);
                            }
                            beat += bpm * 0.5f;
                        }
                    }
                }
                public static void fight11()
                {
                    ResetBarrage();
                    float speed = 2.9f;
                    for (int a = 0; a < 30; a++)
                    {
                        DownBone bone1 = new(false, 0, 1);
                        UpBone bone2 = new(true, 0, 1);

                        float rand = 0;
                        bone1 = new(false, BoxStates.Left - speed * a * 6 * bpm, speed, 160 / 2 - rand - 8);
                        bone2 = new(true, BoxStates.Right + speed * a * 6 * bpm, speed, 160 / 2 + rand - 8);
                        CreateBone(bone1);
                        CreateBone(bone2);
                        speed = 2.2f;
                    }
                    for (int a = 0; a < 6; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 18 * a + bpm * 16, 1, () =>
                        {
                            PlaySound(Ding);
                            DownBone[] bone = GetAll<DownBone>();
                            UpBone[] bone1 = GetAll<UpBone>();
                            for (int b = 0; b < bone.Length; b++)
                            {
                                int x = b;
                                AddInstance(new TimeRangedEvent(0, bpm * 3 - 1, () =>
                                {
                                    bone[x].Speed = 6f * 0.05f + 0.95f * bone[x].Speed;
                                    AddInstance(new TimeRangedEvent(bpm * 3, bpm * 11, () =>
                                    {
                                        bone[x].Speed = 2.2f * 0.15f + 0.85f * bone1[x].Speed;
                                    }));

                                }));
                            }
                            for (int b = 0; b < bone1.Length; b++)

                            {
                                int x = b;
                                AddInstance(new TimeRangedEvent(0, bpm * 3 - 1, () =>
                                {
                                    bone1[x].Speed = 6f * 0.05f + 0.95f * bone1[x].Speed;
                                    AddInstance(new TimeRangedEvent(bpm * 3, bpm * 11, () =>
                                    {
                                        bone1[x].Speed = 2.2f * 0.15f + 0.85f * bone1[x].Speed;
                                    }));

                                }));
                            }
                        }));

                    }
                    AddInstance(new TimeRangedEvent(bpm * 17 * 6 + bpm * 17, 1, () =>
                    {
                        DownBone[] bone = GetAll<DownBone>();
                        UpBone[] bone1 = GetAll<UpBone>();
                        for (int a = 0; a < bone.Length; a++)
                        {
                            int x = a;
                            AddInstance(new TimeRangedEvent(0, bpm * 64, () =>
                            {
                                bone[x].Speed -= 0.004f;
                                bone1[x].Speed -= 0.004f;

                            }));
                        }
                    }));
                }
                public static void fight12()
                {
                    ResetBarrage();

                    for (int a = 0; a < 22; a++)
                    {
                        float cur1 = 0, mission1 = 0;
                        float cur2 = 0, mission2 = 0;
                        AddInstance(new TimeRangedEvent(bpm * 8 * a, 1, () =>
                        {
                            Platform[] rotd = GetAll<Platform>("down");
                            Platform[] rotu = GetAll<Platform>("up");
                            CreatePlatform(new Platform(0, new(BoxStates.Right, BoxStates.Down - 180 - 40), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, (s) =>
                            {
                                cur1 = cur1 * 0.92f + mission1 * 0.08f; return cur1 + 180;
                            })
                            {
                                PositionRouteParam = new float[] { -3, 0.3f },
                                LengthRouteParam = new float[] { 56, bpm * 22 },
                                Tags = new string[] { "up" }
                            }); ;
                            CreatePlatform(new Platform(0, new(BoxStates.Left - 48 + 22, BoxStates.Up + 180 + 40), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, (s) =>
                            {
                                cur2 = cur2 * 0.92f + mission2 * 0.08f; return cur2;
                            })
                            {
                                PositionRouteParam = new float[] { 3, -0.3f },
                                LengthRouteParam = new float[] { 56, bpm * 22 },
                                Tags = new string[] { "down" }
                            });
                        }));
                    }
                    for (int a = 0; a < 3; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 48 * a + bpm * 24, 1, () =>
                        {
                            PlaySound(Ding);
                            Heart.GiveForce(180, 0);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 48 * (a + 1), 1, () =>
                        {
                            PlaySound(Ding);
                            Heart.GiveForce(0, 0);
                        }));
                    }
                    for (int a = 0; a < 512 + 32; a++)
                    {
                        AddInstance(new TimeRangedEvent(2f * a, 1, () =>
                        {
                            DownBone bone1 = new(false, 6f, 1) { MarkScore = false };
                            CreateBone(bone1);
                            AddInstance(new TimeRangedEvent(0, bpm * 12, () =>
                            {
                                bone1.MissionLength += 0.75f;
                            }));
                            UpBone bone2 = new(true, 6f, 1) { MarkScore = false };
                            CreateBone(bone2);
                            AddInstance(new TimeRangedEvent(0, bpm * 12, () =>
                            {
                                bone2.MissionLength += 0.75f;
                            }));
                        }));
                    }
                    for (int a = 0; a < 6; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 32 * a, 1, () =>
                        {
                            CreateGB(new NormalGB(new(Heart.Centre.X - 280, Heart.Centre.Y), new(0, Heart.Centre.Y), new Vector2(0.9f, 0.9f), bpm * 8, bpm * 2));
                            CreateGB(new NormalGB(new(Heart.Centre.X + 280, Heart.Centre.Y), new(640, Heart.Centre.Y), new Vector2(0.9f, 0.9f), bpm * 8, bpm * 2));
                        }));
                    }
                    for (int a = 0; a < 5; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 32 * a + bpm * 16, 1, () =>
                        {
                            CreateBone(new CustomBone(new(BoxStates.Left - 20, Heart.Centre.Y), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                            {
                                PositionRouteParam = new float[] { 0, 300, bpm * 32, 0 },
                                LengthRouteParam = new float[] { 30, bpm * 24 },
                                RotationRouteParam = new float[] { 360, bpm * 16, 0, 0 },
                                ColorType = 1
                                /// <summary>
                                /// 沿着y轴以参数0速度匀速运动，沿着x轴按照正弦轨迹运动，其振幅波长和初相为参数1和2和3。
                                /// </summary>
                            });
                            CreateBone(new CustomBone(new(BoxStates.Left - 20, Heart.Centre.Y), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, special)
                            {
                                PositionRouteParam = new float[] { 0, 300, bpm * 32, 0 },
                                LengthRouteParam = new float[] { 30, bpm * 24 },
                                RotationRouteParam = new float[] { 360, bpm * 16, 0, 0 },
                                ColorType = 1
                            });
                            CreateBone(new CustomBone(new(BoxStates.Right + 20, Heart.Centre.Y), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, Motions.RotationRoute.sin)
                            {
                                PositionRouteParam = new float[] { 0, -300, bpm * 32, 0 },
                                LengthRouteParam = new float[] { 30, bpm * 24 },
                                RotationRouteParam = new float[] { -360, bpm * 16, 00, 0 },
                                ColorType = 2
                            });
                            CreateBone(new CustomBone(new(BoxStates.Right + 20, Heart.Centre.Y), Motions.PositionRoute.YAxisSin, Motions.LengthRoute.autoFold, special)
                            {
                                PositionRouteParam = new float[] { 0, -300, bpm * 32, 0 },
                                LengthRouteParam = new float[] { 30, bpm * 24 },
                                RotationRouteParam = new float[] { -360, bpm * 16, 0, 0 },
                                ColorType = 2
                            });
                        }));
                    }
                }
                public static void fight13()
                {
                    ResetBarrage();
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        CreateEntity(new DownBonesea(110, 4, 74, false, 10, bpm * 64) { tags = new string[] { "a" } });
                        CreateEntity(new UpBonesea(110, 4, 16, false, 10, bpm * 64) { tags = new string[] { "a" } });
                        AddInstance(new TimeRangedEvent(3, 1, () =>
                        {
                            DownBone[] down = GetAll<DownBone>("a");
                            UpBone[] up = GetAll<UpBone>("a");

                            float speed = 0;
                            AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                            {
                                for (int a = 0; a < down.Length; a++)
                                {
                                    int b = a;
                                    down[b].MissionLength = 84 * 1.5f / 3 + Sin(speed + a * 3) * 32;
                                    up[b].MissionLength = 84 * 1.5f - down[b].MissionLength - 48;
                                }
                            }));
                            AddInstance(new TimeRangedEvent(0, bpm * 256, () =>
                            {
                                speed += 360 / bpm / 12;
                            }));
                        }));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 8 * 7 + bpm * 8, 1, () =>
                    {
                        for (int a = 1; a < 6; a++)
                        {
                            AddInstance(new TimeRangedEvent(bpm * 8 * a, 1, () =>
                            {
                                int b = a;
                                float length = 84 * 1.5f / 3 * 1 - 10;
                                DownBone bone1 = new(true, 7, length);
                                UpBone bone2 = new(true, 7, 84 * 1.5f - length - 36);
                                PlaySound(pierce);
                                CreateBone(bone1);
                                CreateBone(bone2);
                                bool p = true;
                                AddInstance(new TimeRangedEvent(bpm * 6, 20, () =>
                                {
                                    if (p)
                                    {
                                        PlaySound(Ding);
                                        bone1.ColorType = 1;
                                        bone2.ColorType = 1;
                                        AddInstance(new TimeRangedEvent(3, 1, () =>
                                        {
                                            bone1.ColorType = 0;
                                            bone2.ColorType = 0;
                                        }));
                                        p = false;
                                    }
                                    bone1.MissionLength = (84 * 1.5f / 4 * 2 + 10 + 4) * 0.1f + bone1.MissionLength * 0.9f;
                                    bone2.MissionLength = 84 * 1.5f - bone1.MissionLength - 36;
                                }));
                            }));
                            AddInstance(new TimeRangedEvent(bpm * 8 * a + bpm * 4, 1, () =>
                            {
                                int b = a;
                                float length = 84 * 1.5f / 4 * 2;
                                DownBone bone1 = new(true, 7, length);
                                UpBone bone2 = new(true, 7, 84 * 1.5f - length - 36);
                                PlaySound(pierce);
                                CreateBone(bone1);
                                CreateBone(bone2);
                                bool p = true;
                                AddInstance(new TimeRangedEvent(bpm * 6, 20, () =>
                                {
                                    if (p)
                                    {
                                        PlaySound(Ding);
                                        bone1.ColorType = 1;
                                        bone2.ColorType = 1;
                                        AddInstance(new TimeRangedEvent(3, 1, () =>
                                        {
                                            bone1.ColorType = 0;
                                            bone2.ColorType = 0;
                                        }));
                                        p = false;
                                    }
                                    bone1.MissionLength = (84 * 1.5f / 4 * 1f - 10) * 0.1f + bone1.MissionLength * 0.9f;
                                    bone2.MissionLength = 84 * 1.5f - bone1.MissionLength - 36;
                                }));
                            }));
                        }
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 8 * 7 + bpm * 8, 1, () =>
                    {
                        Color r1 = ColorReturn(Rand(0, 1));
                        Color r2 = ColorReturn(Rand(0, 1));
                        Color r3 = ColorReturn(Rand(0, 1));
                        Color r4 = ColorReturn(Rand(0, 1));
                        Color r5 = ColorReturn(Rand(0, 1));
                        Color r6 = ColorReturn(Rand(0, 1));
                        Color r7 = ColorReturn(Rand(0, 1));
                        Color r8 = ColorReturn(Rand(0, 1));
                        AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 1, 0, 640 / 9 * 1, 480, (int)(bpm * 8 * 15), 0, r1) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 7f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 2, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 2, 0, 640 / 9 * 2, 480, (int)(bpm * 8 * 15), 0, r2) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 6.5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 3, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 3, 0, 640 / 9 * 3, 480, (int)(bpm * 8 * 15), 0, r3) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 6f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 4, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 4, 0, 640 / 9 * 4, 480, (int)(bpm * 8 * 15), 0, r4) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 5.5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 5, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 5, 0, 640 / 9 * 5, 480, (int)(bpm * 8 * 15), 0, r5) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 6, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 6, 0, 640 / 9 * 6, 480, (int)(bpm * 8 * 15), 0f, r6) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 4.5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 7, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 7, 0, 640 / 9 * 7, 480, (int)(bpm * 8 * 15), 0, r7) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 4f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 8, 1, () =>
                        {
                            DrawingUtil.NormalLine line = new(640 / 9 * 8, 0, 640 / 9 * 8, 480, (int)(bpm * 8 * 15), 0, r8) { depth = 0.003f };
                            CreateEntity(line);
                            AddInstance(new TimeRangedEvent(0, 15, () =>
                            {
                                line.alpha += 0.9f / 15;
                            }));
                            AddInstance(new TimeRangedEvent(16, bpm * 8 * 8f, () =>
                            {
                                line.alpha -= 0.9f / bpm / 8 / 3.5f;
                            }));
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 8, 1, () =>
                        {
                            PlaySound(pierce);
                            InstantSetBox(BoxStates.Centre + new Vector2(5, 0), 650, 84 * 1.5f);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r1) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 8.5f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r2) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 9f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r3) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 9.5f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r4) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 10f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r5) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 10.5f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r6) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 11f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r7) });
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8 * 11.5f, 1, () =>
                        {
                            PlaySound(pierce);
                            CreateBone(new DownBone(false, 9, 84 * 1.5f) { ColorType = ColorReturn(r8) });
                        }));
                        /*for (int a = 1; a < 9; a++)
                        {
                            int b = a;
                            Color colorrand;
                            int rand = Rand(0, 1);
                            if (rand == 0) colorrand = Color.Cyan;
                            else colorrand = Color.Orange;
                            AddInstance(new TimeRangedEvent(bpm * 8 * a, 1, () =>
                            {
                                DrawLibrary.NormalLine line;
                                line = new(640 / 9 * b, 0, 640 / 9 * b, 480, 1000, 0.9f, colorrand) { depth = 0.003f };
                                CreateEntity(line);
                                AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                                {
                                    line.alpha -= 1 / 16 / bpm;
                                }));
                            }));
                        }*/
                    }));
                    ScreenDrawing.BoundColor = new(243, 3, 218);
                    ScreenDrawing.DownBoundDistance = 150;
                    ScreenDrawing.UpBoundDistance = 150;
                }
                public static void fight14()
                {
                    CreateEntity(new DownBonesea(10, 2, 84 * 1.5f / 2 - 20, true, 15, 1000) { tags = new string[] { "w" }, markscore = true });
                    CreateEntity(new UpBonesea(10, 2, 84 * 1.5f / 2 - 20, true, 15, 1000) { tags = new string[] { "w" }, markscore = true });
                    CreateEntity(new DownBonesea(10, 2, 84 * 1.5f / 2 - 20, false, 15, 1000) { tags = new string[] { "w" }, markscore = true });
                    CreateEntity(new UpBonesea(10, 2, 84 * 1.5f / 2 - 20, false, 15, 1000) { tags = new string[] { "w" }, markscore = true });
                }
                public static void fight15()
                {

                    PlaySound(Ding);
                    PlaySound(giga);
                    SetSoul(0);
                    Knife(Heart.Centre, 90, false, bpm * 8, false);
                    SetBox(BoxStates.Left - 10, BoxStates.Right, BoxStates.Up, BoxStates.Down);
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {

                        PlaySound(Ding);
                        PlaySound(giga);
                        ScreenDrawing.ScreenScale = 2f;
                        Knife(Heart.Centre, 90, false, bpm * 8, false);
                        SetBox(BoxStates.Left - 10, BoxStates.Right + 20, BoxStates.Up - 20, BoxStates.Down + 20);

                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {

                        PlaySound(Ding);
                        PlaySound(giga);
                        ScreenDrawing.ScreenScale = 1.5f;
                        Knife(Heart.Centre, 90, false, bpm * 8, false);
                        Knife(Heart.Centre, 0, false, bpm * 8, false);
                        SetBox(BoxStates.Left - 15, BoxStates.Right + 15, BoxStates.Up - 15, BoxStates.Down + 15);

                    }));
                    AddInstance(new TimeRangedEvent(bpm * 48, 1, () =>
                    {
                        Vector2 vec = Heart.Centre;
                        PlaySound(Ding);
                        PlaySound(giga);
                        for (int a = 0; a < 2; a++) PlaySound(giga);
                        ScreenDrawing.ScreenScale = 1.25f;
                        SpecialKnife(Heart.Centre, 90, false, bpm * 8, false);
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                        SpecialKnife(Heart.Centre, 45, true, bpm * 8, true);
                        SpecialKnife(Heart.Centre, 135, true, bpm * 8, true);
                        SetBox(BoxStates.Left - 20, BoxStates.Right + 20, BoxStates.Up - 10, BoxStates.Down + 10);

                    }));
                    AddInstance(new TimeRangedEvent(bpm * 64, 1, () =>
                    {
                        Vector2 vec = Heart.Centre;
                        PlaySound(Ding);
                        PlaySound(giga);
                        for (int a = 0; a < 2; a++) PlaySound(giga);
                        ScreenDrawing.ScreenScale = 1f;
                        SpecialKnife(Heart.Centre, 90, false, bpm * 8, false);
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                        SpecialKnife(Heart.Centre, 45, true, bpm * 8, true);
                        SpecialKnife(Heart.Centre, 135, true, bpm * 8, true);
                        SetBox(BoxStates.Left - 30, BoxStates.Right + 30, BoxStates.Up - 10, BoxStates.Down + 10);

                    }));
                    AddInstance(new TimeRangedEvent(bpm * 80, 1, () =>
                    {
                        Vector2 vec = Heart.Centre;
                        Vector2 vec2 = new(Rand(20, 620), Rand(20, 460));
                        PlaySound(Ding);
                        PlaySound(giga);
                        for (int a = 0; a < 3; a++) PlaySound(giga);
                        SpecialKnife(Heart.Centre, 90, false, bpm * 8, false);
                        SpecialKnife(Heart.Centre, 0, false, bpm * 8, false);
                        SpecialKnife(Heart.Centre, 45, true, bpm * 8, true);
                        SpecialKnife(Heart.Centre, 135, true, bpm * 8, true);
                        SetBox(BoxStates.Left - 30, BoxStates.Right + 30, BoxStates.Up - 10, BoxStates.Down + 10);

                    }));
                }
                public static void fight16()
                {
                    for (int a = 0; a < 2; a++) PlaySound(Ding);
                    SetSoul(0);

                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, false);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                    Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 11, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 1) * 240, Sin(360 / 8 * 1) * 240), 30, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 2) * 240, Sin(360 / 8 * 2) * 240), 30 + 360 / 8, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 3) * 240, Sin(360 / 8 * 3) * 240), 30 + 360 / 8 * 2, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 4) * 240, Sin(360 / 8 * 4) * 240), 30 + 360 / 8 * 3, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 5) * 240, Sin(360 / 8 * 5) * 240), 30 + 360 / 8 * 4, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 6) * 240, Sin(360 / 8 * 6) * 240), 30 + 360 / 8 * 5, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 7) * 240, Sin(360 / 8 * 7) * 240), 30 + 360 / 8 * 6, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(Cos(360 / 8 * 8) * 240, Sin(360 / 8 * 8) * 240), 30 + 360 / 8 * 7, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(-60, 0), 0, true, bpm * 2, false);
                        Knife(Heart.Centre + new Vector2(0, -60), 90, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(60, 0), 180, true, bpm * 2, true);
                        Knife(Heart.Centre + new Vector2(0, 60), 270, true, bpm * 2, true);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        ScreenDrawing.ScreenScale = 1;
                        for (int b = 0; b < 2; b++) PlaySound(Warning);
                        for (int times = 1; times < 9; times++)
                        {
                            for (int a = 0; a < 8; a++) SpecialKnife(new(320, 240), MathF.Atan2(Heart.Centre.Y, 320), true, 0.5f, true);
                            AddInstance(new TimeRangedEvent(bpm * 2 * times, 1, () =>
                            {
                                for (int b = 0; b < 1; b++) PlaySound(largeKnife);
                                ScreenDrawing.ScreenAngle = times * 360 / 9;
                            }));
                        }
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + 9 * bpm * 2, 1, () =>
                    {
                        for (int a = 0; a < 512; a++) CreateEntity(new Particle(Color.White,
                                                new(Rand(-30, 30) / 10f, -Rand(3f, 6f)), Rand(10, 16), new(Rand(BoxStates.Left, BoxStates.Right), Rand(BoxStates.Up, BoxStates.Down)), Sprites.square)
                        {
                            AutoRotate = true
                        });
                        InstantSetBox(240, 645, 485);
                        MainEffects.Supershake();
                        DrawingUtil.Shock(0.5f, 1.5f, 1280);

                    }));
                }
                public static void fight17()
                {
                    for (int a = 0; a < 6; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 * a, 1, () =>
                        {
                            WaveParticle(45, Rand(0, 2), 11);
                            PlaySound(pierce);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 1.2f, 1, () =>
                        {
                            WaveParticle(90, Rand(0, 2), 11);
                            PlaySound(pierce);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 2.4f, 1, () =>
                        {
                            WaveParticle(135, Rand(0, 2), 11);
                            PlaySound(pierce);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16 * a + bpm * 3.6f, 1, () =>
                        {
                            WaveParticle(180, Rand(0, 2), 11);
                            PlaySound(pierce);
                        }));
                    }
                    AddInstance(new TimeRangedEvent(bpm * 5 * 16, 1, () =>
                    {
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 11, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                        {
                            Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                            Knife(new Vector2(640 - Heart.Centre.X, 240), 90, true, bpm * 2, true);
                        }));
                    }));
                    for (int a = 0; a < 4; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 6 * 16 + a * bpm * 4 + bpm * 4, 1, () =>
                        {
                            PlaySound(pierce);
                            for (int a = 0; a < 120; a++)
                            {
                                CreateBone(new CustomBone(new(-24, 480 / 48 * a), Motions.PositionRoute.linear, 90, 48) { PositionRouteParam = new float[] { 15f, 0 }, ColorType = 1 });
                            }
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 6 * 16 + a * bpm * 4 + bpm * 6, 1, () =>
                        {
                            PlaySound(pierce);
                            for (int a = 0; a < 120; a++)
                            {
                                CreateBone(new CustomBone(new(664, 480 / 48 * a), Motions.PositionRoute.linear, 90, 48) { PositionRouteParam = new float[] { -15f, 0 }, ColorType = 1 });
                            }
                        }));
                    }

                }
                public static void fight18()
                {
                    InstantTP(320, 240);
                    SetSoul(2);
                    InstantSetBox(240, 150, 150);
                    BlackScreen(10);
                    ResetBarrage();

                    Heart.GiveForce(270, 6);
                    PlaySound(Ding);
                    PlaySound(pierce);
                    CreateEntity(new Boneslab(270, 150 / 2 - 15, bpm * 4, bpm * 12));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(0, 6);
                        CreateEntity(new Boneslab(0, 150 / 2 - 15, bpm * 4, bpm * 8));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(90, 6);
                        CreateEntity(new Boneslab(90, 150 / 2 - 15, bpm * 4, bpm * 4));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        Heart.GiveForce(0, 6);
                        SetSoul(0);
                        Knife(new Vector2(320, 240 - 75 + 24), 0, false, 0, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(new Vector2(320, 240 - 75 + 48), 0, false, 0, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateEntity(new RotBone(84, 6, 45, true, 0));
                        CreateEntity(new RotBone(84, 6, 45, true, 1));
                        CreateEntity(new RotBone(84, 6, 45, true, 2));
                        CreateEntity(new RotBone(84, 6, 45, true, 3));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 2, 1, () =>
                    {
                        PlaySound(pierce);
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            CreateBone(new CentreCircleBone(25, 3, 60, bpm * 5));
                            CreateBone(new CentreCircleBone(25 + 90, 3, 60, bpm * 5) { ColorType = 1 });
                            CreateBone(new CentreCircleBone(25 + 45, 3, 60, bpm * 5));
                            CreateBone(new CentreCircleBone(25 + 135, 3, 60, bpm * 5) { ColorType = 2 });
                        }));
                        for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(70 + 360 / 12 * a, 3, 24, bpm * 12));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 4, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(false, 4.5f, 75 - 8));
                        CreateBone(new UpBone(true, 4.5f, 75 - 8));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 6, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 4.5f, 75 - 8));
                        CreateBone(new UpBone(false, 4.5f, 75 - 8));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240 - 43f), new Vector2(0, 240 - 43f), new Vector2(0.8f, 0.8f), 0, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240 - 43f), new Vector2(640, 240 - 43f), new Vector2(0.8f, 0.8f), 180, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240 + 43f), new Vector2(0, 240 + 43f), new Vector2(0.8f, 0.8f), 0, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240 + 43f), new Vector2(640, 240 + 43f), new Vector2(0.8f, 0.8f), 180, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 43f, 240 - 160), new Vector2(320 - 43f, 0), new Vector2(0.8f, 0.8f), 90, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 43f, 240 - 160), new Vector2(320 - 43f, 480), new Vector2(0.8f, 0.8f), 90, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 43f, 240 + 160), new Vector2(320 + 43f, 0), new Vector2(0.8f, 0.8f), 270, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 43f, 240 + 160), new Vector2(320 + 43f, 480), new Vector2(0.8f, 0.8f), 270, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240), new Vector2(0, 240), new Vector2(1, 0.4f), 0, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240), new Vector2(640, 240), new Vector2(1, 0.4f), 180, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320, 240 - 160), new Vector2(320, 0), new Vector2(1, 0.4f), 90, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320, 240 + 160), new Vector2(320, 0), new Vector2(1, 0.4f), 270, bpm * 8, bpm * 4));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 30, 1, () =>
                    {
                        CreateEntity(new Boneslab(0, 20, bpm * 4, bpm * 2));
                        CreateEntity(new Boneslab(90, 20, bpm * 4, bpm * 2));
                        CreateEntity(new Boneslab(180, 20, bpm * 4, bpm * 2));
                        CreateEntity(new Boneslab(270, 20, bpm * 4, bpm * 2));
                    }));
                    int rand = Rand(0, 1);
                    AddInstance(new TimeRangedEvent(bpm * 24, 1, () =>
                    {
                        if (rand == 1) CreateEntity(new DrawingUtil.MaskSquare(BoxStates.Right - 20, BoxStates.Up, 24, 150, (int)(bpm * 9), Color.Lerp(Color.Cyan, new Color(0, 0, 0, 0), 0.08f), 0.8f) { Depth = 0.99f });
                        if (rand == 0) CreateEntity(new DrawingUtil.MaskSquare(BoxStates.Left - 4, BoxStates.Up, 24, 150, (int)(bpm * 9), Color.Lerp(Color.Cyan, new Color(0, 0, 0, 0), 0.08f), 0.8f) { Depth = 0.99f });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        PlaySound(Ding);
                        SetSoul(2);
                        Heart.GiveForce(rand * 180 + 90, 6);
                        PlaySound(pierce);
                    }));
                }
                public static void fight19()
                {
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                        CreateEntity(new Boneslab(90, 150 / 2 - 24, bpm * 6, bpm * 4));
                        CreateEntity(new Boneslab(270, 150 / 2 - 24, bpm * 6, bpm * 4));
                        CreateEntity(new Boneslab(0, 150 / 2 - 24, bpm * 8, bpm * 2));
                        CreateEntity(new Boneslab(180, 150 / 2 - 24, bpm * 8, bpm * 2));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 10, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(0, 6);
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            PlaySound(Ding);
                            PlaySound(pierce);
                        }));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(new(320, 240), 0, true, bpm * 2);
                        Knife(new(320, 240), 90, true, bpm * 2);
                        Knife(new(320, 240), 180, true, bpm * 2);
                        Knife(new(320, 240), 270, true, bpm * 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        Knife(new(BoxStates.Left, BoxStates.Up), 0, true, bpm * 2);
                        Knife(new(BoxStates.Left, BoxStates.Down), 90, true, bpm * 2);
                        Knife(new(BoxStates.Right, BoxStates.Down), 180, true, bpm * 2);
                        Knife(new(BoxStates.Right, BoxStates.Up), 270, true, bpm * 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 18, 1, () =>
                    {
                        Knife(new(320, 240), 0, true, bpm * 2);
                        Knife(new(320, 240), 90, true, bpm * 2);
                        Knife(new(320, 240), 180, true, bpm * 2);
                        Knife(new(320, 240), 270, true, bpm * 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 22, 1, () =>
                    {
                        SetSoul(0);
                        PlaySound(Ding);
                        CreateBone(new CentreCircleBone(75, 4.3f, 150, bpm * 6));
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () => { for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(34 + 360 / 12 * a, 6.3f, 16, bpm * 6)); }));
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 3.8f, 75 - 2) { ColorType = 2 });
                        CreateBone(new RightBone(true, 3.8f, 75 - 2) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 26, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new RightBone(true, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new UpBone(true, 5.8f, 150) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 28, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new RightBone(true, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new UpBone(true, 5.8f, 150) { ColorType = 2 });
                    }));
                }
                public static void fight18_1()
                {
                    InstantTP(320, 240);
                    SetSoul(2);
                    InstantSetBox(240, 150, 150);
                    BlackScreen(10);
                    ResetBarrage();

                    Heart.GiveForce(270, 6);
                    PlaySound(Ding);
                    PlaySound(pierce);
                    CreateEntity(new Boneslab(270, 150 / 2 - 15, bpm * 4, bpm * 12));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(0, 6);
                        CreateEntity(new Boneslab(0, 150 / 2 - 15, bpm * 4, bpm * 8));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(90, 6);
                        CreateEntity(new Boneslab(90, 150 / 2 - 15, bpm * 4, bpm * 4));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        Heart.GiveForce(0, 6);
                        SetSoul(0);
                        Knife(new Vector2(320, 240 - 75 + 24), 0, false, 0, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(new Vector2(320, 240 - 75 + 48), 0, false, 0, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateEntity(new RotBone(84, 6, 45, true, 0));
                        CreateEntity(new RotBone(84, 6, 45, true, 1));
                        CreateEntity(new RotBone(84, 6, 45, true, 2));
                        CreateEntity(new RotBone(84, 6, 45, true, 3));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 2, 1, () =>
                    {
                        PlaySound(pierce);
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            CreateBone(new CentreCircleBone(25, 3, 60, bpm * 5));
                            CreateBone(new CentreCircleBone(25 + 90, 3, 60, bpm * 5) { ColorType = 1 });
                            CreateBone(new CentreCircleBone(25 + 45, 3, 60, bpm * 5));
                            CreateBone(new CentreCircleBone(25 + 135, 3, 60, bpm * 5) { ColorType = 2 });
                        }));
                        for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(70 + 360 / 12 * a, 3, 24, bpm * 12));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 4, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(false, 4.5f, 75 - 8));
                        CreateBone(new UpBone(true, 4.5f, 75 - 8));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16 + bpm * 6, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 4.5f, 75 - 8));
                        CreateBone(new UpBone(false, 4.5f, 75 - 8));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240 - 43f), new Vector2(0, 240 - 43f), new Vector2(0.8f, 0.8f), 0, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240 - 43f), new Vector2(640, 240 - 43f), new Vector2(0.8f, 0.8f), 180, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240 + 43f), new Vector2(0, 240 + 43f), new Vector2(0.8f, 0.8f), 0, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240 + 43f), new Vector2(640, 240 + 43f), new Vector2(0.8f, 0.8f), 180, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 43f, 240 - 160), new Vector2(320 - 43f, 0), new Vector2(0.8f, 0.8f), 90, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 43f, 240 - 160), new Vector2(320 - 43f, 480), new Vector2(0.8f, 0.8f), 90, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 43f, 240 + 160), new Vector2(320 + 43f, 0), new Vector2(0.8f, 0.8f), 270, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 + 43f, 240 + 160), new Vector2(320 + 43f, 480), new Vector2(0.8f, 0.8f), 270, bpm * 6, bpm * 1));
                        CreateGB(new NormalGB(new Vector2(320 - 160, 240), new Vector2(0, 240), new Vector2(1, 0.4f), 0, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320 + 160, 240), new Vector2(640, 240), new Vector2(1, 0.4f), 180, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320, 240 - 160), new Vector2(320, 0), new Vector2(1, 0.4f), 90, bpm * 8, bpm * 4));
                        CreateGB(new NormalGB(new Vector2(320, 240 + 160), new Vector2(320, 0), new Vector2(1, 0.4f), 270, bpm * 8, bpm * 4));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 30, 1, () =>
                    {
                        CreateEntity(new Boneslab(0, 20, bpm * 6, bpm * 2));
                        CreateEntity(new Boneslab(90, 20, bpm * 6, bpm * 2));
                        CreateEntity(new Boneslab(180, 20, bpm * 6, bpm * 2));
                        CreateEntity(new Boneslab(270, 20, bpm * 6, bpm * 2));
                    }));
                    int rand = Rand(0, 1);
                    AddInstance(new TimeRangedEvent(bpm * 24, 1, () =>
                    {
                        if (rand == 1) CreateEntity(new DrawingUtil.MaskSquare(BoxStates.Right - 20, BoxStates.Up, 24, 150, (int)(bpm * 9), Color.Lerp(Color.Cyan, new Color(0, 0, 0, 0), 0.08f), 0.8f) { Depth = 0.99f });
                        if (rand == 0) CreateEntity(new DrawingUtil.MaskSquare(BoxStates.Left - 4, BoxStates.Up, 24, 150, (int)(bpm * 9), Color.Lerp(Color.Cyan, new Color(0, 0, 0, 0), 0.08f), 0.8f) { Depth = 0.99f });
                        DrawingUtil.MaskSquare[] mark = GetAll<DrawingUtil.MaskSquare>();
                        for (int a = 0; a < mark.Length; a++) mark[a].alpha -= 1 / bpm * 9;
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 32, 1, () =>
                    {
                        PlaySound(Ding);
                        SetSoul(2);
                        Heart.GiveForce(rand * 180 + 90, 6);
                        PlaySound(pierce);
                    }));
                }
                public static void fight19_1()
                {
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        Knife(new Vector2(Heart.Centre.X, 240), 90, true, bpm * 2, false);
                        CreateEntity(new Boneslab(90, 150 / 2 - 24, bpm * 6, bpm * 4));
                        CreateEntity(new Boneslab(270, 150 / 2 - 24, bpm * 6, bpm * 4));
                        CreateEntity(new Boneslab(0, 150 / 2 - 24, bpm * 8, bpm * 2));
                        CreateEntity(new Boneslab(180, 150 / 2 - 24, bpm * 8, bpm * 2));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 10, 1, () =>
                    {
                        PlaySound(Ding);
                        PlaySound(pierce);
                        Heart.GiveForce(0, 6);
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            PlaySound(Ding);
                            PlaySound(pierce);
                        }));
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                    {
                        Knife(new(320, 240), 0, true, bpm * 2);
                        Knife(new(320, 240), 90, true, bpm * 2);
                        Knife(new(320, 240), 180, true, bpm * 2);
                        Knife(new(320, 240), 270, true, bpm * 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                    {
                        Knife(new(BoxStates.Left, BoxStates.Up), 0, true, bpm * 2);
                        Knife(new(BoxStates.Left, BoxStates.Down), 90, true, bpm * 2);
                        Knife(new(BoxStates.Right, BoxStates.Down), 180, true, bpm * 2);
                        Knife(new(BoxStates.Right, BoxStates.Up), 270, true, bpm * 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 18, 1, () =>
                    {
                        Knife(new(320, 240), 0, true, bpm * 2);
                        Knife(new(320, 240), 90, true, bpm * 2);
                        Knife(new(320, 240), 180, true, bpm * 2);
                        Knife(new(320, 240), 270, true, bpm * 2);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 22, 1, () =>
                    {
                        SetSoul(0);
                        PlaySound(Ding);
                        CreateBone(new CentreCircleBone(75, 4.3f, 150, bpm * 6));
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () => { for (int a = 0; a < 12; a++) CreateBone(new SideCircleBone(34 + 360 / 12 * a, 6.3f, 16, bpm * 6)); }));
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 3.8f, 75 - 2) { ColorType = 2 });
                        CreateBone(new RightBone(true, 3.8f, 75 - 2) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 26, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new RightBone(true, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new UpBone(true, 5.8f, 150) { ColorType = 2 });
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 28, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new LeftBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new RightBone(true, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new DownBone(false, 5.8f, 150) { ColorType = 2 });
                        CreateBone(new UpBone(true, 5.8f, 150) { ColorType = 2 });
                    }));
                }
                public static void fight20()
                {
                    PlaySound(giga);
                    for (int a = 0; a < 256; a++) AddInstance(new TimeRangedEvent(bpm * a * 2, 1, () =>
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 6, 70));
                    }));
                }

                public static Color ColorReturn(int into)
                {
                    return into == 0 ? Color.Cyan : Color.Orange;
                }
                public static int ColorReturn(Color into)
                {
                    return into == Color.Cyan ? 1 : into == Color.Orange ? 2 : 0;
                }
            }
            public void ExtremePlus()
            {
                var arr = GetAll<Entity>();
                if (GametimeF == 1)
                {
                    float a = 0;
                    Effects.part0();
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(shader, 0.55f));
                    //ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(black, 0.9f));
                    AddInstance(new TimeRangedEvent(0, 114514, () =>
                    {
                        shader.Parameters["time"].SetValue(a); a += 0.25f;
                    }));

                }
                if (GametimeF == 2)
                {
                    float A = 1;
                    float count = 0;
                    float B = 0;
                    AddInstance(new TimeRangedEvent(0, 114514, () =>
                    {
                        A = Sin(count) * 0.8f + count * 0.002f;
                        count += 1.5f;
                        B -= 0.003f;
                    }));
                }
                if (InBeat(16))
                {
                    SetSoul(0);
                    Effects.part1();
                }
                if (InBeat(32)) { SetSoul(1); }
                if (InBeat(48)) { SetSoul(0); }
                if (InBeat(64)) { SetSoul(1); }
                if (InBeat(80))
                {
                    SetSoul(0);
                    Effects.part0();
                    Effects.GB();
                    DrawingUtil.Shock(1.25f, 1.5f, bpm * 24);
                    MainEffects.RGB();
                }
                if (InBeat(96)) { SetSoul(1); }
                if (InBeat(112))
                {
                    SetSoul(0);
                    Effects.part0();
                    DrawingUtil.Shock(1.25f, 1.5f, bpm * 24);
                    MainEffects.RGB();
                }
                if (InBeat(128 - 0.4f)) { SetSoul(1); MainEffects.RGB(); PlaySound(Ding); }
                if (InBeat(132 - 0.4f)) { SetSoul(0); MainEffects.RGB(); PlaySound(Ding); }
                if (InBeat(136 - 0.4f)) { SetSoul(1); MainEffects.RGB(); PlaySound(Ding); }
                if (InBeat(140 - 0.4f)) { SetSoul(2); MainEffects.RGB(); PlaySound(Ding); }
                if (InBeat(143.875f))
                {
                    ScreenDrawing.WhiteOut(bpm * 0.5f);
                }
                if (InBeat(140))
                {
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                    {
                        ScreenDrawing.ScreenPositionDelta -= new Vector2(0, 480 / bpm / 4);
                        ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 1.2f - 0.175f;
                    }));
                }
                if (InBeat(144))
                {
                    shader.Parameters["distance"].SetValue(new Vector2(0.001f, 0.001f));
                    TP();
                    ScreenDrawing.ScreenPositionDelta = new Vector2(0);
                    ScreenDrawing.ScreenScale = 1;
                    for (int b = 0; b < 4; b++)
                        PlaySound(switchScene);
                    Explusfight.fight1();
                    Explusfight.fight1_1();
                    AddInstance(new TimeRangedEvent(0, 114514, () =>
                    {
                        shader.Parameters["range"].SetValue(1.2f);
                        shader.Parameters["frequency"].SetValue(120f);
                    }));
                }
                if (InBeat(144 + 16 * 7 - 3)) SetBox(280, 128, 260);
                if (InBeat(144 + 16 * 7 - 2))
                {
                    Explusfight.fight2();
                }
                if (InBeat(260))
                { Explusfight.fight3(); Explusfight.fight3_3(); }
                if (InBeat(399)) BlackScreen(10);
                if (InBeat(400 - 4))
                {
                    Explusfight.randturn();
                }
                if (InBeat(415)) BlackScreen(10);
                if (InBeat(432)) BlackScreen(10);
                if (InBeat(447)) BlackScreen(10);
                if (InBeat(463)) BlackScreen(10);
                if (InBeat(479)) BlackScreen(10);
                if (InBeat(495)) BlackScreen(10);
                if (InBeat(496))
                {

                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 4;
                    InstantSetBox(300, 500, 84 * 1.5f);
                }
                if (InBeat(494))
                {
                    Explusfight.fight10();
                    Effects.part2();
                }
                if (InBeat(496 + 32))
                {
                    SetBox(240, 84, 84); InstantTP(320, 240); SetSoul(1);
                }
                if (InBeat(496 + 32 - 16)) { Explusfight.greensoul(); }
                if (InBeat(496 + 32 + 8 * 16 - 2))
                {
                    SetBox(270, 210, 160);
                }
                if (InBeat(496 + 32 + 8 * 16))
                {
                    Regenerate();
                    SetSoul(0);
                    Regenerate();
                    PlaySound(switchScene); ;
                    Explusfight.fight11();
                }
                if (InBeat(496 + 32 + 8 * 16 + 9 * 16))
                {
                    for (int a = 0; a < 2; a++) { PlaySound(switchScene); }
                    ScreenDrawing.WhiteOut(5);
                    SetSoul(2);
                    InstantSetBox(270, 420, 240);
                    Explusfight.fight12();
                }
                if (InBeat(963.8f)) ScreenDrawing.WhiteOut(6);
                if (InBeat(964)) { Explusfight.fight13(); Effects.part3(); }
                if (InBeat(1172 - 30)) { Explusfight.fight14(); }
                if (InBeat(1172 - 4 - 16 - 16))
                {
                    CreateEntity(new Boneslab(270, 16, 0, bpm * 46) { ColorType = 2 });
                    Effects.part4();
                }
                if (InBeat(1172 - 4 - 20))
                {
                    PlaySound(destroy);
                    DrawingUtil.BlackScreen(5, bpm * 24 - 5, bpm * 80);
                }
                if (InBeat(1172 - 4 - 18))
                {
                    ResetBarrage();
                    ScreenDrawing.ScreenScale = 3;
                    Heart.GiveForce(0, 6);
                }
                if (InBeat(1172))
                {

                    SetSoul(1);
                    InstantSetBox(240, 18, 18);
                    InstantTP(320, 240);
                    ScreenDrawing.BackGroundColor = Color.Black;

                }
                if (InBeat(1200 - 12))
                {
                    Explusfight.fight15();
                }
                if (InBeat(1200 - 12 + 96 - 2))
                {
                    Explusfight.fight16();
                }
                if (GametimeF >= (int)BeatTime(1320 + 4) && GametimeF % 2.5f == 0 && GametimeF <= (int)BeatTime(1320 + 16 - 8 + 7 * 16))
                {
                    float speed = GametimeF - (int)BeatTime(1200 - 12 + 96 - 2 + 120 - 90);
                    Explusfight.WaveParticle(speed + (speed * speed * 0.036f));
                    shader.Parameters["distance"].SetValue(new Vector2(Rand(-0.006f, 0.006f), Rand(-0.006f, 0.006f)));
                }
                if (InBeat(1320 + 16 - 9))
                {

                    Explusfight.fight17();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3))
                {
                    shader.Parameters["distance"].SetValue(new Vector2(0.001f, 0.001f));
                    Explusfight.fight18();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 32))
                {
                    Explusfight.fight19();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 64))
                {
                    Explusfight.fight18_1();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 96))
                {
                    Explusfight.fight19_1();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128))
                {
                    BlackScreen(20);
                    ResetBarrage();
                    TP();
                    SetSoul(0);
                    InstantSetBox(240, 480, 138);

                    Explusfight.fight20();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128))
                {
                    ChangeSpeedScale(1);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 8 * 16, 1320 + 16 - 8 + 7 * 16 + 3 + 128 + 20 * 16))
                {
                    ChangeSpeedScale(
                        1f
                        + 2f /
                        (12 * 16 * bpm) / (GametimeF -
                        BeatTime(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 8 * 16)
                        ));
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 20 * 16))
                {
                    ResetBarrage();
                    DrawingUtil.BetterBlackScreen(15, bpm * 4, bpm * 14, Color.White);
                    AddInstance(new TimeRangedEvent(15, 1, () => { ChangeSpeedScale(1); }));
                }
                /*if (GameStates.IsKeyPressed120f(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    Timevet.Y +=12.5f;
                    CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(11451,
                        "$"+GametimeF.ToString(),
                        Timevet,
                        new UndyneFight_Ex.Fight.TextSizeAttribute(0.5f)));
                    if (Timevet.Y == 450-25)
                    { 
                        Timevet.X += 60;
                        Timevet.Y = 100;
                    }

                }*/

            }
            Vector2 Timevet = new(24, 100);

            public void Normal()
            {
                HeartAttribute.KRDamage = 3f;
                var arr = GetAll<Entity>();
                if (GametimeF == 1)
                {
                    float a = 0;
                    Effects.part0();
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(shader, 0.55f));
                    //ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(black, 0.9f));
                    AddInstance(new TimeRangedEvent(0, 114514, () =>
                    {
                        shader.Parameters["time"].SetValue(a); a += 0.25f;
                    }));

                }
                if (GametimeF == 2)
                {
                    float A = 1;
                    float count = 0;
                    float B = 0;
                    AddInstance(new TimeRangedEvent(0, 114514, () =>
                    {
                        A = Sin(count) * 0.8f + count * 0.002f;
                        count += 1.5f;
                        B -= 0.003f;
                    }));
                }
                if (InBeat(16))
                {
                    SetSoul(0);
                    Effects.part1();
                }
                if (InBeat(32)) { SetSoul(1); }
                if (InBeat(48)) { SetSoul(0); }
                if (InBeat(64)) { SetSoul(1); }
                if (InBeat(80))
                {
                    SetSoul(0);
                    Effects.part0();
                    Effects.GB();
                    DrawingUtil.Shock(1.25f, 1.5f, bpm * 24);
                    MainEffects.RGB();
                }
                if (InBeat(96)) { SetSoul(1); }
                if (InBeat(112))
                {
                    SetSoul(0);
                    Effects.part0();
                    DrawingUtil.Shock(1.25f, 1.5f, bpm * 24);
                    MainEffects.RGB();
                }
                if (InBeat(128 - 0.4f)) { SetSoul(1); MainEffects.RGB(); PlaySound(Ding); }
                if (InBeat(132 - 0.4f)) { SetSoul(0); MainEffects.RGB(); PlaySound(Ding); }
                if (InBeat(136 - 0.4f)) { SetSoul(1); MainEffects.RGB(); PlaySound(Ding); }
                if (InBeat(140 - 0.4f)) { SetSoul(2); MainEffects.RGB(); PlaySound(Ding); }
                if (InBeat(143.875f))
                {
                    ScreenDrawing.WhiteOut(BeatTime(0.5f));
                }
                if (InBeat(140))
                {
                    AddInstance(new TimeRangedEvent(0, bpm * 4, () =>
                    {
                        ScreenDrawing.ScreenPositionDelta -= new Vector2(0, 480 / bpm / 4);
                        ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 1.2f - 0.175f;
                    }));
                }
                if (InBeat(144))
                {
                    shader.Parameters["distance"].SetValue(new Vector2(0.001f, 0.001f));
                    TP();
                    ScreenDrawing.ScreenPositionDelta = new Vector2(0);
                    ScreenDrawing.ScreenScale = 1;
                    for (int b = 0; b < 4; b++)
                        PlaySound(switchScene);
                    Hardfight.fight1();
                    Hardfight.fight1_1();
                    AddInstance(new TimeRangedEvent(0, 114514, () =>
                    {
                        shader.Parameters["range"].SetValue(1.2f);
                        shader.Parameters["frequency"].SetValue(120f);
                    }));
                }
                if (InBeat(144 + 16 * 7 - 3)) SetBox(280, 128, 260);
                if (InBeat(144 + 16 * 7 - 2))
                {
                    Hardfight.fight2();
                }
                if (InBeat(260))
                { Hardfight.fight3(); Hardfight.fight3_3(); }
                if (InBeat(399)) BlackScreen(10);
                if (InBeat(400 - 4))
                {
                    Hardfight.randturn();
                }
                if (InBeat(415)) BlackScreen(10);
                if (InBeat(432)) BlackScreen(10);
                if (InBeat(447)) BlackScreen(10);
                if (InBeat(463)) BlackScreen(10);
                if (InBeat(479)) BlackScreen(10);
                if (InBeat(495)) BlackScreen(10);
                if (InBeat(496))
                {

                    SetSoul(4);
                    HeartAttribute.PurpleLineCount = 4;
                    InstantSetBox(300, 500, 84 * 1.5f);
                }
                if (InBeat(494))
                {
                    Hardfight.fight10();
                    Effects.part2();
                }
                if (InBeat(496 + 32))
                {
                    SetBox(240, 84, 84); InstantTP(320, 240); SetSoul(1);
                }
                if (InBeat(496 + 32 - 16)) { Hardfight.greensoul(); }
                if (InBeat(496 + 32 + 8 * 16 - 2))
                {
                    SetBox(270, 210, 160);
                }
                if (InBeat(496 + 32 + 8 * 16))
                {
                    Regenerate();
                    SetSoul(0);
                    Regenerate();
                    PlaySound(switchScene); ;
                    Hardfight.fight11();
                }
                if (InBeat(496 + 32 + 8 * 16 + 9 * 16))
                {
                    for (int a = 0; a < 2; a++) { PlaySound(switchScene); }
                    ScreenDrawing.WhiteOut(5);
                    SetSoul(2);
                    InstantSetBox(270, 420, 240);
                    Hardfight.fight12();
                }
                if (InBeat(963.8f)) ScreenDrawing.WhiteOut(6);
                if (InBeat(964)) { Hardfight.fight13(); Effects.part3(); }
                if (InBeat(1172 - 30)) { Hardfight.fight14(); }
                if (InBeat(1172 - 4 - 16 - 16))
                {
                    CreateEntity(new Boneslab(270, 16, 0, bpm * 46) { ColorType = 2 });
                    Effects.part4();
                }
                if (InBeat(1172 - 4 - 20))
                {
                    PlaySound(destroy);
                    DrawingUtil.BlackScreen(5, bpm * 24 - 5, bpm * 80);
                }
                if (InBeat(1172 - 4 - 18))
                {
                    ResetBarrage();
                    ScreenDrawing.ScreenScale = 3;
                    Heart.GiveForce(0, 6);
                }
                if (InBeat(1172))
                {

                    SetSoul(1);
                    InstantSetBox(240, 18, 18);
                    InstantTP(320, 240);
                    ScreenDrawing.BackGroundColor = Color.Black;
                    shader.Parameters["frequency"].SetValue(0f);
                    shader.Parameters["distance"].SetValue(new Vector2(0f, 0f));
                    shader.Parameters["range"].SetValue(0f);
                    shader.Parameters["frequency2"].SetValue(0f);
                    shader.Parameters["range2"].SetValue(0f);
                    shader.Parameters["time"].SetValue(0f);
                    shader.Parameters["time2"].SetValue(0f);
                }
                if (InBeat(1200 - 12))
                {
                    Hardfight.fight15();
                }
                if (InBeat(1200 - 12 + 96 - 2))
                {
                    Hardfight.fight16();
                }
                if (GametimeF >= (int)BeatTime(1320 + 4) && GametimeF % 3.5f == 0 && GametimeF <= (int)BeatTime(1320 + 16 - 8 + 6 * 16 - 8))
                {
                    float speed = GametimeF - (int)BeatTime(1200 - 12 + 96 - 2 + 120 - 90);
                    Hardfight.WaveParticle(speed + (speed * speed * 0.036f));
                    shader.Parameters["distance"].SetValue(new Vector2(Rand(-0.006f, 0.006f), Rand(-0.006f, 0.006f)));
                }
                if (InBeat(1320 + 16 - 9))
                {

                    Hardfight.fight17();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3))
                {
                    shader.Parameters["distance"].SetValue(new Vector2(0.001f, 0.001f));
                    Hardfight.fight18();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 32))
                {
                    Hardfight.fight19();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 64))
                {
                    Hardfight.fight18_1();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 96))
                {
                    Hardfight.fight19_1();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128))
                {
                    BlackScreen(20);
                    ResetBarrage();
                    TP();
                    SetSoul(0);
                    InstantSetBox(240, 480, 138);

                    Hardfight.fight20();
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128))
                {
                    ChangeSpeedScale(1);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 6 * 16))
                {
                    ChangeSpeedScale(1 + 1 / 8);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 7 * 16))
                {
                    ChangeSpeedScale(1 + 2 / 8);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 8 * 16))
                {
                    ChangeSpeedScale(1 + 3 / 8);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 9 * 16))
                {
                    ChangeSpeedScale(1 + 4 / 8);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 10 * 16))
                {
                    ChangeSpeedScale(1 + 5 / 8);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 11 * 16))
                {
                    ChangeSpeedScale(1 + 6 / 8);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 12 * 16))
                {
                    ChangeSpeedScale(1 + 7 / 8);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 13 * 16))
                {
                    ChangeSpeedScale(1 + 8 / 8);
                }
                if (InBeat(1320 + 16 - 8 + 7 * 16 + 3 + 128 + 15 * 16))
                {
                    ChangeSpeedScale(1);
                }
            }

            public void Start()
            {
                Heart.SoftFalling = true;
                SetBox(240 + 80, 84, 84);
                SetSoul(1);
                TP(320, 240 + 80);
                HeartAttribute.KRDamage = 2.25f;
                HeartAttribute.MaxHP = 48;
                ScreenDrawing.HPBar.HPExistColor = Color.Yellow;
                HeartAttribute.JumpTimeLimit = 3;
                HeartAttribute.KR = true;
                shader = new Shader(Loader.Load<Effect>("Musics\\DustTrust\\shake"));
                black = new Shader(Loader.Load<Effect>("Musics\\DustTrust\\Screen"));
                shader.Parameters["frequency"].SetValue(0f);
                shader.Parameters["distance"].SetValue(new Vector2(0f, 0f));
                shader.Parameters["range"].SetValue(0f);
                shader.Parameters["frequency2"].SetValue(0f);
                shader.Parameters["range2"].SetValue(0f);
                shader.Parameters["time"].SetValue(0f);
                shader.Parameters["time2"].SetValue(0f);
            }
        }
    }
}