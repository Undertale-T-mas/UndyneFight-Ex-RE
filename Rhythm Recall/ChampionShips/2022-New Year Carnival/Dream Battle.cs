using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.FightResources.Sounds;
using Extends;
using static Extends.DrawingUtil;
using System;

namespace Rhythm_Recall.Waves
{
    public class DreamBattle : IChampionShip
    {
        public DreamBattle()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new()
            {
                { "div.3", Difficulty.Easy },
                { "div.2", Difficulty.Normal },
                { "div.1", Difficulty.Extreme },
                { "div.0", Difficulty.ExtremePlus }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            private class Unlocker : GameObject
            {
                private int state = 0;
                private readonly InputIdentity[] keys = {
                    InputIdentity.Number2 ,
                    InputIdentity.Number0,
                    InputIdentity.Number2 ,
                    InputIdentity.Number0 ,
                    InputIdentity.Number1 ,
                    InputIdentity.Number0 ,
                    InputIdentity.Number1 ,
                    InputIdentity.Number8,
                };
                private readonly string[] keynum =
                {
                    "2","0","2","0","1","0","1","8"
                };
                private int time = 0;
                public Unlocker()
                {
                    UpdateIn120 = true;
                }
                public override void Update()
                {
                    if (state < keys.Length)
                    {
                        if (GameStates.IsKeyPressed120f(keys[state]))
                        {
                            TextPrinter tex1 = new(5.8623f * 2f, "$$$" + keynum[state], new Vector2(Rand(100, 540), Rand(100, 380)), new TextAttribute[]
                            {
                                new TextFadeoutAttribute(0, 5.8623f * 2f),
                                new TextColorAttribute(Color.Lerp(Color.White, Color.Red, (float)state / keys.Length)),
                                new TextSizeAttribute(3f),
                            })
                            {
                                Depth = 0.99f
                            };
                            CreateEntity(tex1);
                            PlaySound(Ding);
                            state++;
                        }
                    }
                    if (state == keys.Length)
                    {
                        if (time == 0)
                        {
                            ScreenDrawing.SceneOut(Color.Black, 60);
                        }
                        time++;
                        if (time == 60) state++;
                    }
                    if (state == keys.Length + 1)
                    {
                        Dispose();
                        BrainPower.IntoUnlockScene();
                    }
                }
            }

            public Game() : base(5.8623f) { }

            class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => GameStates.difficulty == 5 ? "T-mas, modded by TK" : "T-mas";
                public override string SongAuthor => "Touhou";
                public override string PaintAuthor => "";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 4.5f),
                            new(Difficulty.Normal, 12.5f),
                            new(Difficulty.Extreme, 18.2f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 5.5f),
                            new(Difficulty.Normal, 13.5f),
                            new(Difficulty.Extreme, 18.7f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Easy, 10.5f),
                            new(Difficulty.Normal, 16.9f),
                            new(Difficulty.Extreme, 20.5f),
                        }
                    );

                public override HashSet<Difficulty> UnlockedDifficulties
                {
                    get
                    {
                        HashSet<Difficulty> difficulties = new();
                        for (int i = 0; i <= 4; i++) difficulties.Add((Difficulty)i);
                        return difficulties;
                    }
                }
            }
            public SongInformation Attributes => new ThisInformation();

            public static Game instance;

            public string Music => "Dream Battle";

            public string FightName => "Dream Battle";

            private static void BallCreate(int cnt, float range)
            {
                Vector3[] xy = new Vector3[cnt];
                Vector3[] xz = new Vector3[cnt];
                Vector3[] yz = new Vector3[cnt];
                VertexHull[] hull = new VertexHull[3];
                for (int i = 0; i < cnt; i++)
                {
                    float rot = i * 360f / cnt;
                    xy[i] = new Vector3(Cos(rot), Sin(rot), 0);
                    yz[i] = new Vector3(0, Cos(rot), Sin(rot));
                    xz[i] = new Vector3(Cos(rot), 0, Sin(rot));
                }
                Vector3 rotatingSpeed = new(0.6f, 0, 0);
                hull[0] = new VertexHull(xy, rotatingSpeed);
                hull[1] = new VertexHull(yz, rotatingSpeed);
                hull[2] = new VertexHull(xz, rotatingSpeed);
                for (int i = 0; i <= 2; i++) AddInstance(hull[i]);
                AddInstance(new TimeRangedEvent(0, 1500, () =>
                {
                    float del = Sin01(Gametime / ExBarrage.game.BeatTime(64)) * 0.2f;
                    for (int i = 0; i <= 2; i++) hull[i].Axises[2] = new Vector2(-Sin01(del), Cos01(del)) * 0.9f;
                }));
                Vector2 stalker = -Heart.Centre;
                TimeRangedEvent e;
                AddInstance(e = new TimeRangedEvent(0, 1500, () =>
                {
                    stalker = stalker * 0.99f + Heart.Centre * 0.01f;
                }));
                for (int i = 0; i < 3; i++)
                {
                    hull[i].Axises[0] = new(1.25f, 0);
                    for (int j = 0; j < cnt; j++)
                    {
                        int x = i, y = j;
                        CreateBone(new CustomBone(new(0, 0), (s) => stalker + hull[x].Translated[y] * range, 3, 5) { IsMasked = false, AlphaIncrease = true });
                    }
                }
                AddInstance(new InstantEvent(ExBarrage.game.BeatTime(252), () =>
                {
                    stalker = new(320, -500);
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                }));
            }

            private static class EasyBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 5, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 5, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 5, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 5, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 5, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 5 + i % 2 / 3f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Area1A()
                {
                    if (game.InBeat(392))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -1; i <= 1; i++)
                            if (i != 0)
                                CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(394))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(0, 12);
                        CreateEntity(new Boneslab(0, 20, (int)game.BeatTime(6), (int)game.BeatTime(122)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 345), Motions.PositionRoute.cameFromDown, 0, 42, game.BeatTime(122)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(128), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 56 * 0.1f;
                        }));
                    }
                    if (game.InBeat(401))
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (i == 0) continue;
                            CreateBone(new DownBone(false, 320 + i * 60, 0, 40) { Tags = new string[] { "A" } });
                        }
                    }
                    if (game.InBeat(506))
                    {
                        for (int i = -1; i <= 1; i++)
                            if (i != 0)
                                CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(514.5f))
                    {
                        var v = GetAll<Bone>("A");
                        foreach (Bone bone in v)
                        {
                            bone.Dispose();
                        }
                    }
                }
                public static void Area1C()
                {
                    if (game.InBeat(520))
                    {
                        SetSoul(0);
                        SetBox(290, 150, 150);
                    }
                    if (game.InBeat(524, 580) && game.At0thBeat(8))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                    }
                    if (game.InBeat(588, 634) && game.At0thBeat(8))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(395, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                    }
                }
                public static void Area2A()
                {
                    if (game.InBeat(648))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -1; i <= 1; i += 2)
                            CreateGB(new NormalGB(new(320 + i * 80, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(650))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(180, 12);
                        CreateEntity(new Boneslab(180, 20, (int)game.BeatTime(6), (int)game.BeatTime(122)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 235), Motions.PositionRoute.cameFromUp, 180, 36, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(128), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 50 * 0.1f;
                        }));
                    }
                    if (game.InBeat(657))
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            int dir = -i;
                            UpBone y;
                            CreateBone(y = new UpBone(false, 320 + i * 80, 0, 60) { Tags = new string[] { "A" } });

                            float time = game.BeatTime(712) - Gametime;
                            AddInstance(new TimeRangedEvent(time, 36, () =>
                            {
                                y.Speed += dir * 0.1f;
                            }));
                            AddInstance(new InstantEvent(time + 4, () =>
                            {
                                y.ColorType = 0;
                            }));
                            AddInstance(new InstantEvent(time + 2, () =>
                            {
                                y.ColorType = 1;
                            }));
                            AddInstance(new InstantEvent(time, () =>
                            {
                                y.ColorType = 2;
                                y.Speed = dir * 0.62f;
                                PlaySound(Ding);
                            }));
                            AddInstance(new InstantEvent(time * 2, () =>
                            {
                                y.Dispose();
                            }));
                        }
                    }
                }
                public static void Area2C()
                {
                    if (game.InBeat(776))
                    {
                        Heart.RotateTo(0);
                        SetSoul(0);
                        SetBox(290, 150, 150);

                        CentreCircleBone bone;
                        CreateBone(bone = new CentreCircleBone(0, 2.5f, 360, game.BeatTime(123)));
                        bone.ColorType = 2;
                        bone.IsMasked = true;
                        AddInstance(new InstantEvent(game.BeatTime(56), () =>
                        {
                            bone.MissionLength = 12f;
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(64), () =>
                        {
                            bone.ColorType = 1; bone.MissionLength = 72f;
                            bone.RotateSpeed = -2.2f;
                        }));
                    }
                    if (game.InBeat(780, 836))
                    {
                        if (game.At0thBeat(16))
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, 74 * 2)
                            {
                                PositionRouteParam = new float[] { 0.15f, 0.03f, 0, 100, 0 },
                                ColorType = 1
                            });
                        }
                        if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, 74 * 2)
                            {
                                PositionRouteParam = new float[] { -0.15f, -0.03f, 0, 100, 0 },
                                ColorType = 1
                            });
                        }
                    }
                    if (game.InBeat(840))
                    {
                        PlaySound(pierce);
                        for (int i = 0; i < 3; i++)
                            CreateBone(new SideCircleBone(i * 120, 2.2f, 65, game.BeatTime(60)));
                    }
                }

                public static void Area3A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(29), () =>
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    }));

                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "R", "+0", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 5, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area3B() { Intro1(); }

                public static void Area4A()
                {
                    if (game.InBeat(1164))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -1; i <= 1; i++)
                            if (i != 0)
                                CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(1166))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(0, 12);
                        CreateEntity(new Boneslab(0, 20, (int)game.BeatTime(6), (int)game.BeatTime(122)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 345), Motions.PositionRoute.cameFromDown, 0, 36, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(128), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 50 * 0.1f;
                        }));
                    }
                    if (game.InBeat(1173))
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (i == 0) continue;
                            CreateBone(new DownBone(false, 320 + i * 60, 0, 40) { Tags = new string[] { "A" } });
                            CreateBone(new UpBone(false, 320 + i * 60, 0, 90) { Tags = new string[] { "A" } });
                        }
                    }
                    if (game.InBeat(1280))
                    {
                        for (int i = -1; i <= 1; i++)
                            if (i != 0)
                                CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(1288.5f))
                    {
                        var v = GetAll<Bone>("A");
                        foreach (Bone bone in v)
                        {
                            bone.Dispose();
                        }
                    }
                }
                public static void Area4C()
                {
                    if (game.InBeat(1292))
                    {
                        SetSoul(0);
                        SetBox(290, 150, 150);
                    }
                    if (game.InBeat(1296, 1352) && game.At0thBeat(8))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                    }
                    if (game.InBeat(1292 + 64 + 4, 1292 + 128 - 14) && game.At0thBeat(8))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(395, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                    }
                }
            }
            private static class NormalBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(32);
                    for (int i = 0; i < 4; i++)
                        CreateGB(new GreenSoulGB(curTime + game.BeatTime(16 * i), Rand(0, 3), 1, game.BeatTime(12)));
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "+0", "R", "+0", "R", "+0", "R", "+0",
                            "R", "+0", "R", "+0", "R", "+0", "R", "+0",
                            "R", "+0", "R", "+0", "R", "+0", "R", "+0",
                            "R", "+0", "R", "+0", "R", "+0", "R", "+0",
                            "R", "+0", "R", "+0", "R", "+0", "R", "+0",
                            "R", "+0", "R", "+0", "R", "+0", "R", "+0",
                            "R", "+0", "R", "+0", "R", "+0", "R", "+0",
                            "R", "+0", "R", "+0", "R", "+0", "R", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, i % 2);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Area1A()
                {
                    if (game.InBeat(392))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(394))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(0, 12);
                        CreateEntity(new Boneslab(0, 20, (int)game.BeatTime(6), (int)game.BeatTime(120)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 345), Motions.PositionRoute.cameFromDown, 0, 36, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 50 * 0.1f;
                        }));
                    }
                    if (game.InBeat(401))
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            CreateBone(new DownBone(false, 320 + i * 60, 0, 40) { Tags = new string[] { "A" } });
                            CreateBone(new UpBone(false, 320 + i * 60, 0, 90) { Tags = new string[] { "A" } });
                        }
                    }
                    if (game.InBeat(448))
                    {
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(456.5f))
                    {
                        var v = GetAll<Bone>("A");
                        foreach (Bone bone in v)
                        {
                            bone.Dispose();
                        }
                    }
                }
                public static void Area1B()
                {
                    if (game.At0thBeat(8))
                    {
                        CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 0.5f), 0, game.BeatTime(8), game.BeatTime(2)));
                        CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 0.5f), 180, game.BeatTime(8), game.BeatTime(1)));
                    }
                }
                public static void Area1C()
                {
                    if (game.InBeat(520))
                    {
                        SetSoul(0);
                        SetBox(290, 150, 150);
                    }
                    if (game.InBeat(524, 580) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                    }
                    if (game.InBeat(588, 634) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(395, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                    }
                }
                public static void Area2A()
                {
                    if (game.InBeat(648))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -1; i <= 1; i += 2)
                            CreateGB(new NormalGB(new(320 + i * 80, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(650))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(180, 12);
                        CreateEntity(new Boneslab(180, 20, (int)game.BeatTime(6), (int)game.BeatTime(120)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 235), Motions.PositionRoute.cameFromUp, 180, 36, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 50 * 0.1f;
                        }));
                    }
                    if (game.InBeat(401 + 256))
                    {
                        for (int i = -1; i <= 1; i += 2)
                        {
                            int dir = -i;
                            DownBone x; UpBone y;
                            CreateBone(x = new DownBone(false, 320 + i * 80, 0, 70) { Tags = new string[] { "A" } });
                            CreateBone(y = new UpBone(false, 320 + i * 80, 0, 60) { Tags = new string[] { "A" } });

                            float time = game.BeatTime(392 + 64 + 256) - Gametime;
                            AddInstance(new TimeRangedEvent(time, 36, () =>
                            {
                                x.Speed += dir * 0.1f;
                                y.Speed += dir * 0.1f;
                            }));
                            AddInstance(new InstantEvent(time + 4, () =>
                            {
                                x.ColorType = 0;
                                y.ColorType = 0;
                            }));
                            AddInstance(new InstantEvent(time + 2, () =>
                            {
                                x.ColorType = 1;
                                y.ColorType = 1;
                            }));
                            AddInstance(new InstantEvent(time, () =>
                            {
                                x.ColorType = 2;
                                y.ColorType = 2;
                                x.Speed = dir * 0.62f;
                                y.Speed = dir * 0.62f;
                                PlaySound(Ding);
                            }));
                            AddInstance(new InstantEvent(time * 2, () =>
                            {
                                x.Dispose(); y.Dispose();
                            }));
                        }
                    }
                }
                public static void Area2B()
                {
                    if (game.At0thBeat(8))
                    {
                        CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 0.5f), 0, game.BeatTime(8), game.BeatTime(2)));
                        CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 0.5f), 180, game.BeatTime(8), game.BeatTime(1)));
                    }
                }
                public static void Area2C()
                {
                    if (game.InBeat(776))
                    {
                        Heart.RotateTo(0);
                        SetSoul(0);
                        SetBox(290, 150, 150);
                        for (int i = 0; i < 4; i++)
                        {
                            CentreCircleBone bone;
                            CreateBone(bone = new CentreCircleBone(i * 45, 3.5f, 360, game.BeatTime(123)));
                            bone.ColorType = 2;
                            bone.IsMasked = true;
                            bone.ResetColor(Color.Orange * 0.8f);
                            if (i == 0)
                            {
                                AddInstance(new InstantEvent(game.BeatTime(56), () =>
                                {
                                    bone.MissionLength = 12f;
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(64), () =>
                                {
                                    bone.ColorType = 1; bone.MissionLength = 72f;
                                    bone.RotateSpeed = -2.2f;
                                }));
                            }
                        }
                    }
                    if (game.InBeat(780, 828))
                    {
                        if (game.At0thBeat(8))
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, 74 * 2)
                            {
                                PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 },
                                ColorType = 1
                            });
                        }
                        if (game.AtKthBeat(8, game.BeatTime(4)))
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, 74 * 2)
                            {
                                PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 },
                                ColorType = 1
                            });
                        }
                    }
                    if (game.InBeat(840))
                    {
                        PlaySound(pierce);
                        for (int i = 0; i < 4; i++)
                            CreateBone(new SideCircleBone(i * 90, 2.2f, 65, game.BeatTime(60)));
                    }
                }

                public static void Area3A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(29), () =>
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    }));

                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "+0", "/", "+0",
                            "/", "+0", "/", "+0", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, i % 2);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area3B() { Intro1(); }

                public static void Area4A()
                {
                    if (game.InBeat(1164))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(1166))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(0, 12);
                        CreateEntity(new Boneslab(0, 20, (int)game.BeatTime(6), (int)game.BeatTime(120)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 345), Motions.PositionRoute.cameFromDown, 0, 36, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 50 * 0.1f;
                        }));
                    }
                    if (game.InBeat(1173))
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            CreateBone(new DownBone(false, 320 + i * 60, 0, 40) { Tags = new string[] { "A" } });
                            CreateBone(new UpBone(false, 320 + i * 60, 0, 90) { Tags = new string[] { "A" } });
                        }
                    }
                    if (game.InBeat(1220))
                    {
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(1228.5f))
                    {
                        var v = GetAll<Bone>("A");
                        foreach (Bone bone in v)
                        {
                            bone.Dispose();
                        }
                    }
                }
                public static void Area4B()
                {
                    if (game.At0thBeat(8))
                    {
                        CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 0.5f), 0, game.BeatTime(8), game.BeatTime(2)));
                        CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 0.5f), 180, game.BeatTime(8), game.BeatTime(1)));
                    }
                }
                public static void Area4C()
                {
                    if (game.InBeat(1292))
                    {
                        SetSoul(0);
                        SetBox(290, 150, 150);
                    }
                    if (game.InBeat(1296, 1352) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                    }
                    if (game.InBeat(1360, 1406) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(395, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                    }
                }
                public static void Area4D()
                {
                    if (game.InBeat(1420))
                    {
                        SetSoul(0);
                        SetBox(290, 170, 160); BallCreate(12, 86);
                    }
                    if (game.InBeat(1548, 1675) && game.At0thBeat(16))
                    {
                        CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 120), new(320, 0), new(1, 0.5f), game.BeatTime(8), 5));
                    }
                    if (game.InBeat(1548, 1675) && game.At0thBeat(16))
                    {
                        CreateGB(new NormalGB(new Vector2(120, Heart.Centre.Y), new(320, 0), new(1, 0.5f), game.BeatTime(8), 5));
                        CreateGB(new NormalGB(new Vector2(520, Heart.Centre.Y), new(320, 0), new(1, 0.5f), game.BeatTime(8), 5));
                    }
                }
            }
            private static class ExBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "R",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(32);
                    for (int i = 0; i < 4; i++)
                        CreateGB(new GreenSoulGB(curTime + game.BeatTime(16 * i), Rand(0, 3), 1, game.BeatTime(2f)));
                    string[] rway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "R", "/", "R", "/", "R", "/", "R",
                            "/", "R", "/", "R", "/", "R", "/", "R",
                            "/", "R", "/", "R", "/", "R", "/", "R",
                            "/", "R", "/", "R", "/", "R", "/", "R",
                        };
                    string[] bway = {
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, i % 2);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Area1A()
                {
                    if (game.InBeat(392))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(394))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(0, 12);
                        CreateEntity(new Boneslab(0, 20, (int)game.BeatTime(6), (int)game.BeatTime(120)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 345), Motions.PositionRoute.cameFromDown, 0, 24, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 36 * 0.1f;
                        }));
                    }
                    if (game.InBeat(401))
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            CreateBone(new DownBone(false, 320 + i * 60, 0, 40) { Tags = new string[] { "A" } });
                            CreateBone(new UpBone(false, 320 + i * 60, 0, 90) { Tags = new string[] { "A" } });
                        }
                    }
                    if (game.InBeat(448))
                    {
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(456.5f))
                    {
                        var v = GetAll<Bone>("A");
                        foreach (Bone bone in v)
                        {
                            bone.Dispose();
                        }
                    }
                }
                public static void Area1B()
                {
                    if (game.At0thBeat(8))
                    {
                        CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 1f), 0, game.BeatTime(8), game.BeatTime(2)));
                        CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 1f), 180, game.BeatTime(8), game.BeatTime(1)));
                    }
                }
                public static void Area1C()
                {
                    if (game.InBeat(520))
                    {
                        SetSoul(0);
                        SetBox(290, 150, 150);
                        for (int i = 0; i < 4; i++)
                        {
                            CentreCircleBone bone;
                            CreateBone(bone = new CentreCircleBone(i * 45, 3.5f, 360, game.BeatTime(123)));
                            bone.ColorType = 2;
                            bone.IsMasked = true;
                            bone.ResetColor(Color.Orange * 0.8f);
                        }
                    }
                    if (game.InBeat(524, 580) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                    }
                    if (game.InBeat(588, 634) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(395, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                    }
                }
                public static void Area2A()
                {
                    if (game.InBeat(648))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -1; i <= 1; i += 2)
                            CreateGB(new NormalGB(new(320 + i * 80, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(650))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(180, 12);
                        CreateEntity(new Boneslab(180, 20, (int)game.BeatTime(6), (int)game.BeatTime(120)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 235), Motions.PositionRoute.cameFromUp, 180, 24, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 36 * 0.1f;
                        }));
                    }
                    if (game.InBeat(657))
                    {
                        for (int t = -1; t <= 1; t += 2)
                            for (int i = -1; i <= 1; i += 2)
                            {
                                int dir = -i;
                                DownBone x; UpBone y;
                                CreateBone(x = new DownBone(false, 320 + i * 80 + 4 * t, 0, 70) { Tags = new string[] { "A" } });
                                CreateBone(y = new UpBone(false, 320 + i * 80 + 4 * t, 0, 60) { Tags = new string[] { "A" } });

                                float time = game.BeatTime(392 + 64 + 256) - Gametime;
                                AddInstance(new TimeRangedEvent(time, 36, () =>
                                {
                                    x.Speed += dir * 0.1f;
                                    y.Speed += dir * 0.1f;
                                }));
                                AddInstance(new InstantEvent(time + 4, () =>
                                {
                                    x.ColorType = 0;
                                    y.ColorType = 0;
                                }));
                                AddInstance(new InstantEvent(time + 2, () =>
                                {
                                    x.ColorType = 1;
                                    y.ColorType = 1;
                                }));
                                AddInstance(new InstantEvent(time, () =>
                                {
                                    x.ColorType = 2;
                                    y.ColorType = 2;
                                    x.Speed = dir * 0.62f;
                                    y.Speed = dir * 0.62f;
                                    PlaySound(Ding);
                                }));
                                AddInstance(new InstantEvent(time * 2, () =>
                                {
                                    x.Dispose(); y.Dispose();
                                }));
                            }
                    }
                }
                public static void Area2B()
                {
                    if (game.At0thBeat(8))
                    {
                        CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 1f), 0, game.BeatTime(8), game.BeatTime(2)));
                        CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 1f), 180, game.BeatTime(8), game.BeatTime(1)));
                    }
                }
                public static void Area2C()
                {
                    if (game.InBeat(776))
                    {
                        Heart.RotateTo(0);
                        SetSoul(0);
                        SetBox(290, 150, 150);
                        for (int i = 0; i < 4; i++)
                        {
                            CentreCircleBone bone;
                            CreateBone(bone = new CentreCircleBone(i * 45, 3.5f, 360, game.BeatTime(123)));
                            bone.ColorType = 2;
                            bone.IsMasked = true;
                            bone.ResetColor(Color.Orange * 0.8f);
                            if (i == 0)
                            {
                                AddInstance(new InstantEvent(game.BeatTime(56), () =>
                                {
                                    bone.MissionLength = 12f;
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(64), () =>
                                {
                                    bone.ColorType = 1; bone.MissionLength = 72f;
                                    bone.RotateSpeed = -3.6f;
                                }));
                            }
                        }
                    }
                    if (game.InBeat(780, 828))
                    {
                        if (game.At0thBeat(8))
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, 78 * 2)
                            {
                                PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 },
                                ColorType = 1
                            });
                        }
                        if (game.AtKthBeat(8, game.BeatTime(4)))
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, 78 * 2)
                            {
                                PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 },
                                ColorType = 1
                            });
                        }
                    }
                    if (game.InBeat(840))
                    {
                        PlaySound(pierce);
                        for (int i = 0; i < 4; i++)
                            CreateBone(new SideCircleBone(i * 90, 4.2f, 65, game.BeatTime(60)));
                    }
                }

                public static void Area3A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(29), () =>
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    }));

                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "R", "/", "R", "/", "R", "/", "R",
                            "/", "R", "/", "R", "/", "R", "/", "R",
                            "/", "R", "/", "R", "/", "R", "/", "R",
                            "/", "R", "/", "R", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, i % 2);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area3B() { Intro1(); }

                public static void Area4A()
                {
                    if (game.InBeat(1164))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.56f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(1166))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(0, 14);
                        CreateEntity(new Boneslab(0, 22, (int)game.BeatTime(3), (int)game.BeatTime(123)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 345), Motions.PositionRoute.cameFromDown, 0, 24, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 32 * 0.1f;
                        }));
                    }
                    if (game.InBeat(1173))
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            CreateBone(new DownBone(false, 320 + i * 60, 0, 42) { Tags = new string[] { "A" } });
                            CreateBone(new UpBone(false, 320 + i * 60, 0, 95) { Tags = new string[] { "A" } });
                        }
                    }
                    if (game.InBeat(1220))
                    {
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(1228.5f))
                    {
                        var v = GetAll<Bone>("A");
                        foreach (Bone bone in v)
                        {
                            bone.Dispose();
                        }
                    }
                }
                public static void Area4B()
                {
                    if (game.At0thBeat(8))
                    {
                        CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 1f), 0, game.BeatTime(8), game.BeatTime(2)));
                        CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 1f), 180, game.BeatTime(8), game.BeatTime(1)));
                    }
                }
                public static void Area4C()
                {
                    if (game.InBeat(1292))
                    {
                        SetSoul(0);
                        SetBox(290, 150, 150);
                        for (int i = 0; i < 6; i++)
                        {
                            CentreCircleBone bone;
                            CreateBone(bone = new CentreCircleBone(i * 30, 3.5f, 360, game.BeatTime(123)));
                            bone.ColorType = 2;
                            bone.IsMasked = true;
                            bone.ResetColor(Color.Orange * 0.8f);
                        }
                    }
                    if (game.InBeat(1296, 1352) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                    }
                    if (game.InBeat(1360, 1408) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(395, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                        });
                    }
                }
                public static void Area4D()
                {
                    if (game.InBeat(1420))
                    {
                        SetSoul(0);
                        SetBox(290, 170, 160); BallCreate(16, 84);
                    }
                    if (game.InBeat(1548, 1675))
                    {
                        if (game.At0thBeat(16))
                        {
                            CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 120), new(320, 0), new(1, 0.5f), game.BeatTime(8), 5));
                        }
                        else if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            CreateGB(new NormalGB(new Vector2(120, Heart.Centre.Y), new(320, 0), new(1, 0.5f), game.BeatTime(8), 5));
                            CreateGB(new NormalGB(new Vector2(520, Heart.Centre.Y), new(320, 0), new(1, 0.5f), game.BeatTime(8), 5));
                        }
                    }
                }
            }

            #region Non-ChampionShip
            public void Noob()
            {
                throw new System.NotImplementedException();
            }
            public void Hard()
            {
                throw new System.NotImplementedException();
            }
            #endregion
            #region TKSP
            private static class ExPlusBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "R",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(32);
                    for (int i = 0; i < 4; i++)
                        CreateGB(new GreenSoulGB(curTime + game.BeatTime(16 * i), Rand(0, 3), 0, game.BeatTime(2f)));
                    string[] rway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                        };
                    string[] bway = {
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0, ArrowAttribute.RotateR);
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, i % 2, ArrowAttribute.RotateL);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Area1A()
                {
                    if (game.InBeat(392))
                    {
                        SetSoul(2);
                        Heart.Speed = 3;
                        SetBox(290, 320, 175);
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(394))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(0, 12);
                        CreateEntity(new Boneslab(0, 20, (int)game.BeatTime(6), (int)game.BeatTime(120)));
                        Platform a = new(0, new(320, 345), Motions.PositionRoute.cameFromDown, 0, 24, game.BeatTime(120));
                        CreatePlatform(a);
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 36 * 0.1f;
                        }));
                    }
                    if (game.InBeat(401))
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            CreateBone(new DownBone(false, 320 + i * 60, 0, 110) { Tags = new string[] { "A" } });
                            CreateBone(new UpBone(false, 320 + i * 60, 0, 20) { Tags = new string[] { "A" } });
                        }
                    }
                    if (game.InBeat(448))
                    {
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(456.5f))
                    {
                        var v = GetAll<Bone>("A");
                        foreach (Bone bone in v)
                        {
                            bone.Dispose();
                        }
                        CreateEntity(new Boneslab(0, 50, (int)game.BeatTime(0), (int)game.BeatTime(57.5f)) { ColorType = 2 });
                        CreateEntity(new Boneslab(270, 40, (int)game.BeatTime(5), (int)game.BeatTime(57.5f)) { ColorType = 1 });
                        CreateEntity(new Boneslab(90, 40, (int)game.BeatTime(5), (int)game.BeatTime(57.5f)) { ColorType = 1 });
                        CreateEntity(new Boneslab(180, 30, (int)game.BeatTime(0), (int)game.BeatTime(57.5f)));
                    }
                }
                public static void Area1B()
                {
                    if (game.At0thBeat(8))
                    {
                        CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 1f), 0, game.BeatTime(8), game.BeatTime(2)));
                        CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 1f), 180, game.BeatTime(8), game.BeatTime(1)));
                    }
                }
                public static void Area1C()
                {
                    if (game.InBeat(520))
                    {
                        SetSoul(0);
                        Heart.Speed = 2.5f;
                        SetBox(290, 150, 150);
                        for (int i = 0; i < 4; i++)
                        {
                            CentreCircleBone bone;
                            CreateBone(bone = new CentreCircleBone(i * 45, (i % 2) == 0 ? 3.5f : -3.5f, 360, game.BeatTime(123)));
                            bone.ColorType = 2;
                            bone.IsMasked = true;
                            bone.ResetColor(Color.Orange * 0.8f);
                        }
                    }
                    /*if (game.InBeat(524, 580) && game.At0thBeat(4))
                    {
                        PlaySound(pierce);
                        float height = Rand(40, 110);
                        CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                        {
                            PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                        });
                    }*/
                    if (game.InBeat(524, 580) && game.At0thBeat(1))
                    {
                        string[] str = new string[]
                        {
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "B",     "/", "B", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                        };
                        int curBeat = (int)((GametimeF / game.BeatTime(1)) - 524);
                        if (curBeat < str.Length)
                        {
                            if (str[curBeat] == "B")
                            {
                                PlaySound(pierce);
                                var pre = LastRand;
                                float height = pre + Rand(-40, 40);
                                if (height < 50)
                                {
                                    height = 50;
                                    height += Rand(0, 40);
                                }
                                if (height > 110)
                                {
                                    height = 110;
                                    height -= Rand(0, 40);
                                }
                                var temp = Rand(height, height);
                                CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                                {
                                    PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                                });
                                CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                                {
                                    PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                                });
                            }
                        }
                    }
                    if (game.InBeat(588, 634) && game.At0thBeat(1))
                    {
                        string[] str = new string[]
                        {
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "B",     "/", "B", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                        };
                        int curBeat = (int)((GametimeF / game.BeatTime(1)) - 588);
                        if (curBeat < str.Length)
                        {
                            if (str[curBeat] == "B")
                            {
                                PlaySound(pierce);
                                var pre = LastRand;
                                float height = pre + Rand(-40, 40);
                                if (height < 50)
                                {
                                    height = 50;
                                    height += Rand(0, 40);
                                }
                                if (height > 110)
                                {
                                    height = 110;
                                    height -= Rand(0, 40);
                                }
                                var temp = Rand(height, height);
                                CreateBone(new CustomBone(new Vector2(395, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                                {
                                    PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                                });
                                CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, (150 - height - 21) * 2)
                                {
                                    PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                                });
                            }
                        }
                    }
                }
                public static void Area2A()
                {
                    if (game.InBeat(648))
                    {
                        SetSoul(2);
                        Heart.Speed = 3;
                        SetBox(290, 320, 175);
                        for (int i = -1; i <= 1; i += 2)
                        {
                            CreateGB(new NormalGB(new(320 + i * 80, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8) - 10, 8));
                            CreateGB(new NormalGB(new(320 + i * 120, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8) + 20, 8));
                            CreateGB(new NormalGB(new(320 + i * 140, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8) + 30, 8));
                            CreateGB(new NormalGB(new(320 + i * 10, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8) + 80, 8));
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < 2)
                                CreateGB(new NormalGB(new(480 - i * 160, 125), new(320, 0), new(1.0f, 1.0f), 90, game.BeatTime(8) + 200 + i * 10, 6));
                            CreateGB(new NormalGB(new(140 + i * 250 / 7, 125), new(320, 0), new(1.0f, 0.75f), 90, game.BeatTime(8) + 110 + i * 7, 6));
                        }
                        for (int i = 4; i < 7; i++)
                        {
                            CreateGB(new NormalGB(new(140 + i * 250 / 7, 125), new(320, 0), new(1.0f, 0.75f), 90, game.BeatTime(8) + 110 + i * 10, 6));
                        }
                    }
                    if (game.InBeat(650))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(180, 12);
                        CreateEntity(new Boneslab(180, 20, (int)game.BeatTime(6), (int)game.BeatTime(120)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 235), Motions.PositionRoute.cameFromUp, 180, 24, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 36 * 0.1f;
                        }));
                    }
                    if (game.InBeat(657))
                    {
                        for (int t = -1; t <= 1; t += 2)
                            for (int i = -1; i <= 1; i += 2)
                            {
                                int dir = -i;
                                DownBone x; UpBone y;
                                CreateBone(x = new DownBone(false, 320 + i * 80 + 4 * t, 0, 70) { Tags = new string[] { "A" } });
                                CreateBone(y = new UpBone(false, 320 + i * 80 + 4 * t, 0, 60) { Tags = new string[] { "A" } });

                                float time = game.BeatTime(392 + 64 + 256) - Gametime;
                                AddInstance(new TimeRangedEvent(time, 36, () =>
                                {
                                    x.Speed += dir * 0.1f;
                                    y.Speed += dir * 0.1f;
                                }));
                                AddInstance(new InstantEvent(time + 4, () =>
                                {
                                    x.ColorType = 0;
                                    y.ColorType = 0;
                                }));
                                AddInstance(new InstantEvent(time + 2, () =>
                                {
                                    x.ColorType = 1;
                                    y.ColorType = 1;
                                }));
                                AddInstance(new InstantEvent(time, () =>
                                {
                                    x.ColorType = 2;
                                    y.ColorType = 2;
                                    x.Speed = dir * 0.62f;
                                    y.Speed = dir * 0.62f;
                                    PlaySound(Ding);
                                }));
                                AddInstance(new InstantEvent(time * 2, () =>
                                {
                                    x.Dispose(); y.Dispose();
                                }));
                            }
                    }
                    if (game.InBeat(693))
                    {
                        CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 1f), 0, game.BeatTime(8), game.BeatTime(6)));
                        CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 1f), 180, game.BeatTime(8), game.BeatTime(5)));
                    }
                }
                public static void Area2B()
                {
                    if (game.At0thBeat(1))
                    {
                        string[] str = new string[]
                        {
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "B",     "/", "B", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "/", "/", "/",     "/", "/", "/", "/",
                        };
                        int curBeat = (int)((GametimeF / game.BeatTime(1)) - 708);
                        if (curBeat > 0 && curBeat < str.Length)
                        {
                            if (str[curBeat] == "B")
                            {
                                CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 1f), 0, game.BeatTime(8), game.BeatTime(1)));
                                CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 1f), 180, game.BeatTime(8), game.BeatTime(0.5f)));
                            }
                        }
                    }
                }
                public static void Area2C()
                {
                    if (game.InBeat(776))
                    {
                        Heart.RotateTo(0);
                        SetSoul(0);
                        SetBox(290, 150, 150);
                        for (int i = 0; i < 4; i++)
                        {
                            CentreCircleBone bone;
                            CreateBone(bone = new CentreCircleBone(i * 45, 3.5f, 360, game.BeatTime(123)));
                            bone.ColorType = 2;
                            bone.IsMasked = true;
                            bone.ResetColor(Color.Orange * 0.8f);
                            if (i == 0 || i == 2)
                            {
                                AddInstance(new InstantEvent(game.BeatTime(56), () =>
                                {
                                    bone.MissionLength = 12f;
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(64), () =>
                                {
                                    bone.ColorType = 0;
                                    bone.MissionLength = 72f;
                                    bone.RotateSpeed = -4.8f;
                                }));
                            }
                        }
                    }
                    if (game.InBeat(780, 828))
                    {
                        if (game.At0thBeat(8))
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, 78 * 2)
                            {
                                PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 },
                                ColorType = 1
                            });
                        }
                        if (game.AtKthBeat(8, game.BeatTime(4)))
                        {
                            PlaySound(pierce);
                            CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, 78 * 2)
                            {
                                PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 },
                                ColorType = 1
                            });
                        }
                    }
                    if (game.InBeat(840))
                    {
                        PlaySound(pierce);
                        for (int i = 0; i < 4; i++)
                            CreateBone(new SideCircleBone(i * 90, 4.2f, 65, game.BeatTime(60)));
                    }
                }

                public static void Area3A()
                {
                    AddInstance(new InstantEvent(game.BeatTime(29), () =>
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    }));

                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+2", "/", "+2",
                            "/", "+2", "/", "+2", "/", "+0", "/", "+0",
                        };
                    string[] bway = {
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0, ArrowAttribute.RotateR);
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, i % 2, ArrowAttribute.RotateL);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area3B() { Intro1(); }

                public static void Area4A()
                {
                    if (game.InBeat(1164))
                    {
                        SetSoul(2);
                        SetBox(290, 320, 175);
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.56f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(1166))
                    {
                        PlaySound(boneSlabSpawn);
                        Heart.GiveForce(0, 14);
                        CreateEntity(new Boneslab(0, 22, (int)game.BeatTime(3), (int)game.BeatTime(123)));
                        Platform a;
                        CreatePlatform(a = new Platform(0, new(320, 345), Motions.PositionRoute.cameFromDown, 0, 24, game.BeatTime(120)));
                        AddInstance(new InstantEvent(game.BeatTime(8), () =>
                        {
                            PlaySound(Ding);
                            a.PositionRoute = Motions.PositionRoute.YAxisSin;
                            a.PositionRouteParam = new float[] { 0, 150, game.BeatTime(64), -a.AppearTime };
                            a.ChangeType();
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(8), game.BeatTime(18), () =>
                        {
                            a.LengthRouteParam[0] = a.LengthRouteParam[0] * 0.9f + 32 * 0.1f;
                        }));
                    }
                    if (game.InBeat(1173))
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            CreateBone(new DownBone(false, 320 + i * 60, 0, 95) { Tags = new string[] { "A" } });
                            CreateBone(new UpBone(false, 320 + i * 60, 0, 42) { Tags = new string[] { "A" } });
                        }
                    }
                    if (game.InBeat(1220))
                    {
                        for (int i = -2; i <= 2; i++)
                            CreateGB(new NormalGB(new(320 + i * 60, 125), new(320, 0), new(1.0f, 0.5f), 90, game.BeatTime(8), 8));
                    }
                    if (game.InBeat(1228.5f))
                    {
                        var v = GetAll<Bone>("A");
                        foreach (Bone bone in v)
                        {
                            bone.Dispose();
                        }
                    }
                }
                public static void Area4B()
                {
                    if (game.At0thBeat(1))
                    {
                        string[] str = new string[]
                        {
                        "/", "B", "/", "/",     "/", "/", "/", "B",
                        "/", "B", "/", "/",     "/", "/", "/", "/",
                        "/", "B", "/", "/",     "/", "/", "/", "B",
                        "/", "B", "/", "B",     "/", "B", "/", "B",
                        "/", "B", "/", "/",     "/", "/", "/", "B",
                        "/", "B", "/", "/",     "/", "/", "/", "/",
                        "/", "/", "/", "/",     "/", "/", "/", "/",
                        };
                        int curBeat = (int)((GametimeF / game.BeatTime(1)) - 1220);
                        if (curBeat > 0 && curBeat < str.Length)
                        {
                            if (str[curBeat] == "B")
                            {
                                CreateGB(new NormalGB(new(50, Heart.Centre.Y), new(0, 480), new(1, 1f), 0, game.BeatTime(8), game.BeatTime(1)));
                                CreateGB(new NormalGB(new(590, Heart.Centre.Y), new(0, 480), new(1, 1f), 180, game.BeatTime(8), game.BeatTime(0.5f)));
                            }
                        }
                    }

                }
                public static void Area4C()
                {
                    if (game.InBeat(1292))
                    {
                        SetSoul(0);
                        SetBox(290, 150, 150);
                        for (int i = 0; i < 6; i++)
                        {
                            CentreCircleBone bone;
                            CreateBone(bone = new CentreCircleBone(i * 30, 3.5f, 360, game.BeatTime(123)));
                            bone.ColorType = 2;
                            bone.IsMasked = true;
                            bone.ResetColor(Color.Orange * 0.8f);
                        }
                    }
                    if (game.InBeat(1292, 1352) && game.At0thBeat(1))
                    {
                        string[] str = new string[]
                        {
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "B",     "/", "B", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                        };
                        int curBeat = (int)((GametimeF / game.BeatTime(1)) - 1291);
                        if (curBeat < str.Length)
                        {
                            if (str[curBeat] == "B")
                            {
                                PlaySound(pierce);
                                var pre = LastRand;
                                float height = pre + Rand(-40, 40);
                                if (height < 50)
                                {
                                    height = 50;
                                    height += Rand(0, 40);
                                }
                                if (height > 110)
                                {
                                    height = 110;
                                    height -= Rand(0, 40);
                                }
                                var temp = Rand(height, height);
                                CreateBone(new CustomBone(new Vector2(245, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                                {
                                    PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                                });
                                CreateBone(new CustomBone(new Vector2(245, 365), Motions.PositionRoute.XAccAxisSin, 0, (160 - height - 21) * 2)
                                {
                                    PositionRouteParam = new float[] { 0.2f, 0.05f, 0, 100, 0 }
                                });
                            }
                        }
                    }
                    if (game.InBeat(1356, 1408) && game.At0thBeat(1))
                    {
                        string[] str = new string[]
                        {
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "B",     "/", "B", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "B",
                            "/", "B", "/", "/",     "/", "/", "/", "/",
                            "/", "/", "/", "/",     "/", "/", "/", "/",
                        };
                        int curBeat = (int)((GametimeF / game.BeatTime(1)) - 1355);
                        if (curBeat < str.Length)
                        {
                            if (str[curBeat] == "B")
                            {
                                PlaySound(pierce);
                                var pre = LastRand;
                                float height = pre + Rand(-40, 40);
                                if (height < 50)
                                {
                                    height = 50;
                                    height += Rand(0, 40);
                                }
                                if (height > 110)
                                {
                                    height = 110;
                                    height -= Rand(0, 40);
                                }
                                var temp = Rand(height, height);
                                CreateBone(new CustomBone(new Vector2(395, 215), Motions.PositionRoute.XAccAxisSin, 0, (height - 21) * 2)
                                {
                                    PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                                });
                                CreateBone(new CustomBone(new Vector2(395, 365), Motions.PositionRoute.XAccAxisSin, 0, (160 - height - 21) * 2)
                                {
                                    PositionRouteParam = new float[] { -0.2f, -0.05f, 0, 100, 0 }
                                });
                            }
                        }
                    }
                }
                public static void Area4D()
                {
                    if (game.InBeat(1420))
                    {
                        SetSoul(0);
                        SetBox(290, 170, 160);
                        BallCreate(24, 124);
                        CreateBone(new CentreCircleBone(0, 3, 190, game.BeatTime(240)) { ColorType = 2 });
                        CreateBone(new CentreCircleBone(0, -5, 190, game.BeatTime(240)) { ColorType = 2 });
                    }
                    if (game.InBeat(1548, 1675))
                    {
                        if (game.At0thBeat(16))
                        {
                            CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 120), new(320, 0), new(1, 1f), game.BeatTime(8), 5));
                        }
                        else if (game.AtKthBeat(16, game.BeatTime(8)))
                        {
                            CreateGB(new NormalGB(new Vector2(120, Heart.Centre.Y), new(320, 0), new(1, 1f), game.BeatTime(8), 5));
                            CreateGB(new NormalGB(new Vector2(520, Heart.Centre.Y), new(320, 0), new(1, 1f), game.BeatTime(8), 5));
                        }
                    }
                }
            }
            public void ExtremePlus()
            {
                if (Gametime < 0) return;
                if (GametimeF == 0)
                {
                    CreateGB(new GreenSoulGB(BeatTime(8), 3, 0, BeatTime(56)));
                    CreateGB(new GreenSoulGB(BeatTime(8), 1, 1, BeatTime(56)));
                    CreateArrow(BeatTime(68), "R", 6, 0, 0);
                    CreateArrow(BeatTime(68), "R", 6, 1, 0);
                }
                if (InBeat(8)) { LerpScreenScale(BeatTime(56), 1, 7 / BeatTime(56)); }
                if (InBeat(40)) ExPlusBarrage.Intro0();
                if (InBeat(68))
                {
                    ScreenDrawing.CameraEffect.Convulse(36, BeatTime(3), true);
                    ScreenDrawing.CameraEffect.SizeExpand(5, BeatTime(3));
                }
                if (InBeat(72, 328) && AtKthBeat(16, BeatTime(12)))
                {
                    ScreenDrawing.CameraEffect.Convulse(36, BeatTime(3), RandBool());
                    ScreenDrawing.CameraEffect.SizeExpand(5, BeatTime(3));
                }
                if ((InBeat(188, 313) || InBeat(1024, 1149)) && AtKthBeat(1, 0))
                {
                    var dir = (GametimeF - (int)BeatTime(188)) / BeatTime(0.5f);
                    CreateArrow(60, (int)MathUtil.Posmod(dir, 4), 6, 1, 0);
                }
                if (InBeat(104)) ExPlusBarrage.Intro0();
                if (InBeat(168)) ExPlusBarrage.Intro1();
                if (InBeat(232)) ExPlusBarrage.Intro1();
                if (InBeat(296)) ExPlusBarrage.Intro2();
                if (InBeat(392, 457)) ExPlusBarrage.Area1A();
                if (InBeat(456, 511)) ExPlusBarrage.Area1B();
                if (InBeat(520, 648)) ExPlusBarrage.Area1C();
                if (InBeat(648, 713)) ExPlusBarrage.Area2A();
                if (InBeat(712, 767)) ExPlusBarrage.Area2B();
                if (InBeat(776, 894)) ExPlusBarrage.Area2C();
                if (InBeat(876)) ExPlusBarrage.Area3A();
                if (InBeat(940)) ExPlusBarrage.Area3A();
                if (InBeat(1004)) ExPlusBarrage.Area3B();
                if (InBeat(1068)) ExPlusBarrage.Area3B();
                if (InBeat(1164, 1229)) ExPlusBarrage.Area4A();
                if (InBeat(1228, 1283)) ExPlusBarrage.Area4B();
                if (InBeat(1292, 1420)) ExPlusBarrage.Area4C();
                if (InBeat(1420, 1676)) ExPlusBarrage.Area4D();
            }
            #endregion


            public void Easy()
            {
                if (Gametime < 0) return;
                if (GametimeF == 0)
                {
                    HeartAttribute.KRDamage = 1;
                    CreateGB(new GreenSoulGB(BeatTime(8), 3, 0, BeatTime(4 * 11)));
                    CreateArrow(BeatTime(4 * 17), "R", 6, 0, 0);
                }
                if (InBeat(72 - 32)) EasyBarrage.Intro0();
                if (InBeat(136 - 32)) EasyBarrage.Intro0();
                if (InBeat(200 - 32)) EasyBarrage.Intro1();
                if (InBeat(264 - 32)) EasyBarrage.Intro1();
                if (InBeat(328 - 32)) EasyBarrage.Intro2();
                if (InBeat(392, 392 + 129)) EasyBarrage.Area1A();
                if (InBeat(520, 520 + 128)) EasyBarrage.Area1C();
                if (InBeat(392 + 256, 392 + 256 + 129)) EasyBarrage.Area2A();
                if (InBeat(520 + 256, 520 + 256 + 128)) EasyBarrage.Area2C();
                if (InBeat(908 - 32)) EasyBarrage.Area3A();
                if (InBeat(972 - 32)) EasyBarrage.Area3A();
                if (InBeat(1036 - 32)) EasyBarrage.Area3B();
                if (InBeat(1100 - 32)) EasyBarrage.Area3B();
                if (InBeat(1164, 1164 + 129)) EasyBarrage.Area4A();
                if (InBeat(1292, 1292 + 128)) EasyBarrage.Area4C();
            }

            public void Normal()
            {
                if (Gametime < 0) return;
                if (GametimeF == 0)
                {
                    CreateGB(new GreenSoulGB(BeatTime(8), 3, 0, BeatTime(4 * 14)));
                    CreateArrow(BeatTime(4 * 17), "R", 6, 0, 0);
                }
                if (InBeat(72 - 32)) NormalBarrage.Intro0();
                if (InBeat(136 - 32)) NormalBarrage.Intro0();
                if (InBeat(200 - 32)) NormalBarrage.Intro1();
                if (InBeat(264 - 32)) NormalBarrage.Intro1();
                if (InBeat(328 - 32)) NormalBarrage.Intro2();
                if (InBeat(392, 392 + 65)) NormalBarrage.Area1A();
                if (InBeat(456, 456 + 55)) NormalBarrage.Area1B();
                if (InBeat(520, 520 + 128)) NormalBarrage.Area1C();
                if (InBeat(392 + 256, 392 + 256 + 65)) NormalBarrage.Area2A();
                if (InBeat(456 + 256, 456 + 256 + 55)) NormalBarrage.Area2B();
                if (InBeat(520 + 256, 520 + 256 + 128)) NormalBarrage.Area2C();
                if (InBeat(908 - 32)) NormalBarrage.Area3A();
                if (InBeat(972 - 32)) NormalBarrage.Area3A();
                if (InBeat(1036 - 32)) NormalBarrage.Area3B();
                if (InBeat(1100 - 32)) NormalBarrage.Area3B();
                if (InBeat(1164, 1164 + 65)) NormalBarrage.Area4A();
                if (InBeat(1228, 1228 + 55)) NormalBarrage.Area4B();
                if (InBeat(1292, 1292 + 128)) NormalBarrage.Area4C();
                if (InBeat(1420, 1420 + 128)) NormalBarrage.Area4D();
            }

            public void Extreme()
            {
                if (Gametime < 0) return;
                if (GametimeF == 0)
                {
                    AddInstance(new Unlocker());
                    CreateGB(new GreenSoulGB(BeatTime(8), 3, 0, BeatTime(4 * 14)));
                    CreateGB(new GreenSoulGB(BeatTime(8), 1, 1, BeatTime(4 * 14)));
                    CreateArrow(BeatTime(4 * 17), "R", 6, 0, 0);
                    CreateArrow(BeatTime(4 * 17), "R", 6, 1, 0);
                }
                if (InBeat(72 - 32)) ExBarrage.Intro0();
                if (InBeat(136 - 32)) ExBarrage.Intro0();
                if (InBeat(200 - 32)) ExBarrage.Intro1();
                if (InBeat(264 - 32)) ExBarrage.Intro1();
                if (InBeat(328 - 32)) ExBarrage.Intro2();
                if (InBeat(392, 392 + 65)) ExBarrage.Area1A();
                if (InBeat(456, 456 + 55)) ExBarrage.Area1B();
                if (InBeat(520, 520 + 128)) ExBarrage.Area1C();
                if (InBeat(392 + 256, 392 + 256 + 65)) ExBarrage.Area2A();
                if (InBeat(456 + 256, 456 + 256 + 55)) ExBarrage.Area2B();
                if (InBeat(520 + 256, 520 + 256 + 128)) ExBarrage.Area2C();
                if (InBeat(908 - 32)) ExBarrage.Area3A();
                if (InBeat(972 - 32)) ExBarrage.Area3A();
                if (InBeat(1036 - 32)) ExBarrage.Area3B();
                if (InBeat(1100 - 32)) ExBarrage.Area3B();
                if (InBeat(1164, 1164 + 65)) ExBarrage.Area4A();
                if (InBeat(1228, 1228 + 55)) ExBarrage.Area4B();
                if (InBeat(1292, 1292 + 128)) ExBarrage.Area4C();
                if (InBeat(1420, 1420 + 256)) ExBarrage.Area4D();
            }

            public void Start()
            {
                EasyBarrage.game = this;
                NormalBarrage.game = this;
                ExBarrage.game = this;
                ExPlusBarrage.game = this;
                HeartAttribute.KR = true;
                HeartAttribute.KRDamage = 1.5f;
                HeartAttribute.MaxHP = 32;
                HeartAttribute.Speed = 2.86f;
                HeartAttribute.SoftFalling = true;
                SetGreenBox();
                TP();
                SetSoul(1);
                GametimeDelta = -17;
                //GametimeDelta = 4300;
                //GametimeDelta = this.BeatTime(1532);
                //SetSoul(0); 
                var displace = BeatTime(0);
                GametimeDelta += displace;
                PlayOffset = displace;
                if (GameStates.difficulty == 5 && GametimeDelta == -17)
                {
                    BlackScreen(0, BeatTime(8), 240);
                    ScreenDrawing.ScreenAngle = 90;
                    ScreenAngle(0, BeatTime(64));
                    ScreenDrawing.ScreenScale = 4;
                }
            }
        }
    }
}