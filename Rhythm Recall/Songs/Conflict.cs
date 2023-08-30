using Extends;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class Conflict : IChampionShip
    {
        public Conflict()
        {

            difficulties = new();
            difficulties.Add("div.3", Difficulty.Noob);
            difficulties.Add("div.2", Difficulty.Normal);
            difficulties.Add("div.1", Difficulty.Extreme);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public void ExtremePlus()
            {
                throw new System.NotImplementedException();
            }

            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                        new(Difficulty.Noob, 3.0f),
                            new(Difficulty.Normal, 10.0f),
                            new(Difficulty.Extreme, 17.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                        new(Difficulty.Noob, 3.0f),
                            new(Difficulty.Normal, 10.5f),
                            new(Difficulty.Extreme, 17.9f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                        new(Difficulty.Noob, 9.0f),
                            new(Difficulty.Normal, 15.0f),
                            new(Difficulty.Extreme, 20.9f),
                        }
                    );
                public override string BarrageAuthor => "T-mas & zKronO & Tlottgodinf";
                public override string AttributeAuthor => "IceAgeDOT & Walar";
                public override string SongAuthor => "siromaru + cranky";
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(5.859f) { }

            public static Game instance;

            public string Music => "Conflict";
            //  public string Music => "Brain Power";
            public string FightName => "Conflict";

            private static Game game;
            private static void MakeLine(int t, bool dir)
            {
                DrawingUtil.NormalLine line = dir
                    ? new(240 - t * 40, 0, 240 - t * 40, 640, (int)game.BeatTime(10.5f + t * 2), 0)
                    : new(400 + t * 40, 0, 400 + t * 40, 640, (int)game.BeatTime(10.5f + t * 2), 0);
                Color col = line.color = Color.Lerp(Color.White, Color.Magenta, 0.4f);
                AddInstance(new TimeRangedEvent(game.BeatTime(8f + t * 2), game.BeatTime(2.5f), () =>
                {
                    line.color = Color.Lerp(line.color, col, 0.14f);
                    line.alpha -= 1 / game.BeatTime(2.5f);
                }));
                AddInstance(new TimeRangedEvent(game.BeatTime(2), () =>
                {
                    line.alpha += 1 / game.BeatTime(2);
                }));
                AddInstance(new InstantEvent(game.BeatTime(8 + t * 2), () =>
                {
                    line.color = Color.White;

                    for (int i = -1; i <= 1; i++)
                    {
                        if (i == 0) continue;
                        float speed = i * 1.7f;
                        float time = game.BeatTime(2.5f - MathF.Abs(i) * 0.3f);
                        float alpha = 0.6f - MathF.Abs(i) * 0.1f;
                        Extends.DrawingUtil.NormalLine line1 = new(line.x1, line.y1, line.x2, line.y2, (int)time, alpha);
                        line1.color = Color.Lerp(Color.White, Color.Magenta, 0.3f);
                        AddInstance(new TimeRangedEvent(time, () =>
                        {
                            line1.x1 += speed; line1.x2 += speed;
                            line1.alpha -= alpha / time;
                        }));
                        CreateEntity(line1);
                    }
                }));
                CreateEntity(line);
            }
            public static void UpForwardLine()
            {
                Extends.DrawingUtil.Linerotatelong UFL1 = new(Rand(320 - 120, 320 + 120), 525f, 90f, 600, Rand(0.15f, 0.85f), Rand(25, 75), Color.White);
                UFL1.width = 2f;
                if (Rand(1, 3) == 1)
                {
                    CreateEntity(UFL1);
                }
                else
                {
                    for (int a2 = 0; a2 < 2; a2++)
                    {
                        CreateEntity(UFL1);
                    }
                }
                AddInstance(new TimeRangedEvent(0f, 600, delegate ()
                {
                    UFL1.yCenter -= Rand(6.0f, 12.0f);
                }));
            }
            private static void ShinyLine(Vector2 centre, float rotation, Color col)
            {
                DrawingUtil.Linerotate linerotate = new(centre.X, centre.Y, rotation, game.BeatTime(3), 0.0f);
                game.ForBeat(0.6f, () => { linerotate.alpha += 0.2f; });
                game.ForBeat(0.6f, 2.4f, () => { linerotate.alpha -= 0.05f; });
                linerotate.color = col;
                CreateEntity(linerotate);
            }
            public class MaskSquare : Entity
            {
                public float duration = 0;
                public float LeftUpX = 0;
                public float LeftUpY = 0;
                public float width = 0;
                public float height = 0;
                public Color color = Color.White;
                public MaskSquare(float LeftUpX, float LeftUpY, float width, float height, float duration, Color color, float alpha)
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
            public static void DIY_Up_DownBoneWall(float LeftUpX, float LeftUpY, float width, float height, float WarningTime, float duration)
            {
                if (LeftUpY == 0)
                {
                    MaskSquare m = new(LeftUpX, LeftUpY, width, height, WarningTime, Color.DarkRed, 0);
                    CreateEntity(m);
                    AddInstance(new TimeRangedEvent(WarningTime / 2, () =>
                    {
                        m.alpha += 1 / WarningTime;
                    }));
                    AddInstance(new TimeRangedEvent(WarningTime / 2, WarningTime / 2, () =>
                    {
                        m.alpha -= 1 / WarningTime;
                    }));
                    AddInstance(new InstantEvent(WarningTime + 1, () =>
                    {
                        PlaySound(Sounds.slam);
                        for (int i = 0; i < 1 + (width / 10); i++)
                        {
                            CustomBone b = new(new(LeftUpX + i * 10, LeftUpY), EasingUtil.CentreEasing.FromUp(640, game.BeatTime(1)), 0, height * 2, duration) { PositionRouteParam = new float[] { 0, 0 }, LengthRouteParam = new float[] { height * 2, 10 } };
                            CreateBone(b);
                        }
                    }));
                }
                else if (LeftUpY == 480)
                {
                    MaskSquare m = new(LeftUpX, LeftUpY - 480, width, height, WarningTime, Color.DarkRed, 0);
                    CreateEntity(m);
                    AddInstance(new TimeRangedEvent(WarningTime / 2, () =>
                    {
                        m.alpha += 1 / WarningTime;
                    }));
                    AddInstance(new TimeRangedEvent(WarningTime / 2, WarningTime / 2, () =>
                    {
                        m.alpha -= 1 / WarningTime;
                    }));
                    AddInstance(new InstantEvent(WarningTime + 1, () =>
                    {
                        PlaySound(Sounds.slam);
                        for (int i = 0; i < 1 + (width / 10); i++)
                        {
                            CustomBone b = new(new(LeftUpX + i * 10, LeftUpY), EasingUtil.CentreEasing.FromDown(640, game.BeatTime(1)), 0, height * 2, duration) { PositionRouteParam = new float[] { 0, 0 }, LengthRouteParam = new float[] { height * 2, 10 } };
                            CreateBone(b);
                        }
                    }));
                }
            }
            public class Linerotate : Entity
            {
                public float duration = 0;
                public float xCenter = 0;
                public float yCenter = 0;
                public float rotate = 0;
                public float depth = 0;
                public float width = 4;
                public Color color = Color.White;
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
                public Linerotate(float xCenter, float yCenter, float rotate, float duration, float alpha, float depth) : this(xCenter, yCenter, rotate, duration, alpha, Color.White, depth) { }
                public float alpha = 1;
                public int time = 0;
                public float speed = 1;
                public override void Draw()
                {
                    if (rotate % 180 != 0)
                        DrawingLab.DrawLine(new(xCenter - (1f / Tan(rotate)) * yCenter, 0), new(xCenter + (1f / Tan(rotate)) * (480 - yCenter), 480), width, color * alpha, depth);
                    else
                        DrawingLab.DrawLine(new(0, yCenter), new(640, yCenter), width, color * alpha, depth);
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
            private static class NormalBarrage { }

            public void Noob()
            {
                if (Gametime < 0) return;
                if (InBeat(0)) EasyBarrage.Intro0();
                if (InBeat(64)) EasyBarrage.Intro1();

                if (InBeat(128)) EasyBarrage.Main0A();
                if (InBeat(192)) EasyBarrage.Main0B();
                if (InBeat(256)) EasyBarrage.Main1A();
                if (InBeat(320)) EasyBarrage.Main1B();

                if (InBeat(384)) EasyBarrage.Hall0A0();
                if (InBeat(384)) EasyBarrage.Hall0A3();
                if (InBeat(384 + 48)) EasyBarrage.Hall0A4();
                if (InBeat(384 + 16)) EasyBarrage.Hall0A2();
                for (int i = 0; i < 110; i++)
                {
                    if (InBeat(384 + 13 + i)) EasyBarrage.Hall0A1();
                }
                for (int i = 0; i < 6; i++)
                {
                    if (InBeat(384 + 84 + i * 4)) EasyBarrage.Hall0A5();
                }
                if (InBeat(384 + 127.5f)) EasyBarrage.Hall0A6();

                if (InBeat(384 + 128)) EasyBarrage.Main2A();
                if (InBeat(384 + 192)) EasyBarrage.Main2B();
                if (InBeat(640)) EasyBarrage.Rest0A();
                if (InBeat(640 + 64)) EasyBarrage.Rest0B();
                if (InBeat(640 + 128)) EasyBarrage.Rest1A();
                if (InBeat(640 + 192)) EasyBarrage.Rest1B();
                if (InBeat(640 + 192)) EasyBarrage.Rest1B();
                if (InBeat(640 + 192 + 48)) NoobPartF1();
                if (InBeat(640 + 192 + 56 * 4 + 48)) NoobPartF2();

                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 - 4)) EasyBarrage.Main0A();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 - 4)) EasyBarrage.Main0B();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 + 64 - 4)) EasyBarrage.Main1A();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 + 64 + 64 - 4)) NoobPartF3();
            }
            public void Hard()
            {
                if (Gametime < 0) return;
            }
            public void Easy()
            {
                if (Gametime < 0) return;
            }

            public void Normal()
            {
                if (Gametime < 0) return;
                if (InBeat(0)) NorBarrage.Intro0();
                if (InBeat(64)) NorBarrage.Intro1();

                if (InBeat(128)) NorBarrage.Main0A();
                if (InBeat(192)) NorBarrage.Main0B();
                if (InBeat(256)) NorBarrage.Main1A();
                if (InBeat(320)) NorBarrage.Main1B();

                if (InBeat(384)) NorBarrage.Hall0A0();
                if (InBeat(384)) NorBarrage.Hall0A3();
                if (InBeat(384 + 48)) NorBarrage.Hall0A4();
                if (InBeat(384 + 16)) NorBarrage.Hall0A2();
                for (int i = 0; i < 110; i++)
                {
                    if (InBeat(384 + 13 + i)) NorBarrage.Hall0A1();
                }
                for (int i = 0; i < 6; i++)
                {
                    if (InBeat(384 + 84 + i * 4)) NorBarrage.Hall0A5();
                }
                if (InBeat(384 + 127.5f)) NorBarrage.Hall0A6();

                if (InBeat(384 + 128)) NorBarrage.Main2A();
                if (InBeat(384 + 192)) NorBarrage.Main2B();
                if (InBeat(640)) NorBarrage.Rest0A();
                if (InBeat(640 + 64)) NorBarrage.Rest0B();
                if (InBeat(640 + 128)) NorBarrage.Rest1A();
                if (InBeat(640 + 192)) NorBarrage.Rest1B();
                if (InBeat(640 + 192)) NorBarrage.Rest1B();
                if (InBeat(640 + 192 + 48)) NorPartF1();
                if (InBeat(640 + 192 + 56 * 4 + 48)) NorPartF2();

                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 - 4)) NorBarrage.Main0A();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 - 4)) NorBarrage.Main0B();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 + 64 - 4)) NorBarrage.Main1A();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 + 64 + 64 - 4)) NorPartF3();
            }

            private static class ExBarrage
            {
                public static void Intro0()
                {
                    BoxStates.BoxMovingScale = 0.07f;
                    SetBox(320, 250, 160);
                    for (int i = 0; i < 4; i++)
                    {
                        float cur = game.BeatTime(i * 16 + 8);
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new DownBone(false, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 1;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new UpBone(true, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 2;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                    }
                }
                public static void Intro1()
                {
                    for (int i = 0; i < 3; i++)
                    {
                        float cur = game.BeatTime(i * 16 + 8);
                        int t = i;
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new DownBone(false, 5.3f, 77));
                            if (t <= 1)
                            {
                                AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                                {
                                    bone.Speed -= 10.6f / game.BeatTime(3);
                                    bone.MissionLength += 78 / game.BeatTime(3);
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(8), () =>
                                {
                                    bone.ColorType = 1;
                                    PlaySound(Sounds.Ding, 0.8f);
                                }));
                            }
                        }));
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new UpBone(true, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 2;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            CreateGB(new NormalGB(
                                new(Heart.Centre.X, 100),
                                Heart.Centre.X < 320 ? Vector2.Zero : new(640, 480), new(1, 1),
                                game.BeatTime(8), game.BeatTime(1))
                            );
                        }));
                    }
                    float cur2 = game.BeatTime(56 - 4);
                    SideBone a = new UpBone(true, 320, 0, 60), b = new DownBone(true, 320, 0, 60);
                    AddInstance(new InstantEvent(cur2, () =>
                    {
                        CreateBone(a);
                        CreateBone(b);
                        SetBox(320, 80, 160);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(3.5f), () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(4.3f), () =>
                    {
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(5.3f), () =>
                    {
                        Heart.GiveForce(90, 20);
                        a.MissionLength = 30;
                        b.MissionLength = 90;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(6.1f), () =>
                    {
                        SetSoul(0);
                        Heart.RotateTo(0);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(7.4f), () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(8.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(9.4f), () =>
                    {
                        Heart.GiveForce(270, 20);
                        a.MissionLength = 60;
                        b.MissionLength = 60;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(11.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(12.4f), () =>
                    {
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(13.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                        a.MissionLength = 20;
                        b.MissionLength = 80;
                        SetBox(310, 80, 140);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(15.1f), () =>
                    {
                        Heart.GiveForce(180, 20);
                        a.Speed = b.Speed = -5;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(16.0f), () =>
                    {
                        a.Speed = b.Speed = 5;
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(18.1f), () =>
                    {
                        SetSoul(1);
                        TP();
                        PlaySound(Sounds.switchScene);
                        SetGreenBox();
                        Heart.RotateTo(0);
                        BoxStates.BoxMovingScale = 0.15f;
                    }));
                }
                public static void Main0A()
                {
                    BoxStates.BoxMovingScale = 0.15f;
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "R", "+11", "/", "/", "/",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6.5f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6.5f, "+0");
                        time += game.BeatTime(1);
                    }
                    bool[] seq = { RandBool(), RandBool(), RandBool(), RandBool() };

                    float del = 30;
                    DrawingUtil.NormalLine line1 = new(0, 130, 640, 130 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line2 = new(0, 142, 640, 142 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line3 = new(0, 130 + del, 640, 130, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line4 = new(0, 142 + del, 640, 139, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine[] lines = { line1, line2, line3, line4 };
                    foreach (DrawingUtil.NormalLine line in lines)
                    {
                        line.alpha = 0;
                        line.width = 4f;
                    }
                    game.DelayBeat(36, () => { foreach (DrawingUtil.NormalLine line in lines) CreateEntity(line); });
                    game.ForBeat(40 - 4, 4, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha += 0.03f; });
                    game.ForBeat(54, 8, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha -= 0.05f; });

                    AddInstance(new InstantEvent(game.BeatTime(40 - 0.5f), () => { SetSoul(0); }));
                    for (int i = 0; i < 4; i++)
                    {
                        int t = i;
                        bool dir = seq[t];
                        AddInstance(new InstantEvent(game.BeatTime(32 + i * 2), () =>
                        {
                            MakeLine(t, dir);
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(40 + i * 4 - 0.5f), () =>
                        {
                            SetBox(300, 80, 160);
                            SideBone bone;
                            if (seq[t]) CreateBone(bone = new RightBone(false, 300, 0, 36));
                            else CreateBone(bone = new LeftBone(false, 300, 0, 36));

                            game.DelayBeat(1, () =>
                            {
                                if (t % 2 == 0)
                                {
                                    float speed = 7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                                else
                                {
                                    float speed = -7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                            });

                            AddInstance(new InstantEvent(game.BeatTime(4), () => { bone.Dispose(); }));
                            AddInstance(new InstantEvent(game.BeatTime(2), () =>
                            {
                                if (t % 2 == 0)
                                {
                                    SetBox(370, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Up), 0, Color.Magenta * 0.6f); });
                                }
                                else
                                {
                                    SetBox(230, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Down), 0, Color.Magenta * 0.6f); });
                                }
                            }));
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(56), () => { SetSoul(1); TP(); SetGreenBox(); }));
                }
                public static void Main0B()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+21", "/", "/", "R1",
                    "R(R1)", "/", "/", "/", "R(R1)", "/", "/", "/",
                    "R(R1)", "/", "/", "/", "R(R1)", "/", "/", "/",
                    "R(R1)", "/", "/", "/", "R(R1)", "/", "/", "/",
                    "R(R1)", "/", "/", "/", "R(R1)", "/", "/", "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6.5f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6.5f, "+0");
                        time += game.BeatTime(1);
                    }
                }
                public static void Main1A()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$01", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "R1", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "R1", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "R1", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "R1", "+1", "/", "/", "/",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6.5f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6.5f, "+01");
                        time += game.BeatTime(1);
                    }

                    bool[] seq = { RandBool(), RandBool(), RandBool(), RandBool() };

                    float del = 30;
                    DrawingUtil.NormalLine line1 = new(0, 130, 640, 130 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line2 = new(0, 142, 640, 142 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line3 = new(0, 130 + del, 640, 130, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line4 = new(0, 142 + del, 640, 139, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine[] lines = { line1, line2, line3, line4 };
                    foreach (DrawingUtil.NormalLine line in lines)
                    {
                        line.alpha = 0;
                        line.width = 4f;
                    }
                    game.DelayBeat(36, () => { foreach (DrawingUtil.NormalLine line in lines) CreateEntity(line); });
                    game.ForBeat(40 - 4, 4, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha += 0.03f; });
                    game.ForBeat(54, 8, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha -= 0.05f; });

                    AddInstance(new InstantEvent(game.BeatTime(40 - 0.5f), () => { SetSoul(0); }));
                    for (int i = 0; i < 4; i++)
                    {
                        int t = i;
                        bool dir = seq[t];
                        AddInstance(new InstantEvent(game.BeatTime(32 + i * 2), () =>
                        {
                            MakeLine(t, dir);
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(40 + i * 4 - 0.5f), () =>
                        {
                            SetBox(300, 80, 160);
                            SideBone bone;
                            if (seq[t]) CreateBone(bone = new RightBone(false, 300, 0, 36));
                            else CreateBone(bone = new LeftBone(false, 300, 0, 36));

                            game.DelayBeat(1, () =>
                            {
                                if (t % 2 == 0)
                                {
                                    float speed = 7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                                else
                                {
                                    float speed = -7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                            });

                            AddInstance(new InstantEvent(game.BeatTime(4), () => { bone.Dispose(); }));
                            AddInstance(new InstantEvent(game.BeatTime(2), () =>
                            {
                                if (t % 2 == 0)
                                {
                                    SetBox(370, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Up), 0, Color.Magenta * 0.6f); });
                                }
                                else
                                {
                                    SetBox(230, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Down), 0, Color.Magenta * 0.6f); });
                                }
                            }));
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(56), () => { SetSoul(1); TP(); SetGreenBox(); }));
                }
                public static void Main1B()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$01", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "R1", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "R1", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "R1", "+21", "+21", "+21", "+2", "/", "/", "R",
                    "R(R1)", "/", "/", "/", "R(R1)", "/", "/", "R",
                    "R(R1)", "/", "/", "/", "R(R1)", "/", "/", "R",
                    "R(R1)", "/", "/", "R", "R1", "/", "/", "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6.5f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6.5f, "+01");
                        if (i == 52) CreateGB(new GreenSoulGB(time, "R", 0, game.BeatTime(8)));
                        time += game.BeatTime(1);
                    }
                }
                #region The Hall
                public static void Hall0A0()
                {
                    int h = 0;
                    AddInstance(new InstantEvent(game.BeatTime(7.5f), () =>
                    {
                        SetBox(320 - 82, 320 + 82, 240 - 42, 490);
                        SetSoul(2);
                        HeartAttribute.Gravity = 0;
                        Heart.GiveForce(0, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(8), 95, () =>
                    {
                        TP(320, 240 - h * h * 0.0105f);
                        SetBox(320 - 82, 320 + 82, 240 - 42 - h * h * 0.45f, 490);
                        h++;
                    }));
                    float speed = 9.8f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(6.5f), game.BeatTime(6.5f), () =>
                    {
                        speed *= 0.934f;
                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied + new Vector2(speed, 0);
                        rect.Width -= 0.23f;
                        ScreenDrawing.HPBar.AreaOccupied = rect;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(128 - 8), () =>
                    {
                        SetBox(320 - 82, 320 + 82, -20, 240 + 120);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(128 - 4), () =>
                    {
                        Heart.GiveForce(0, 10);
                    }));
                }
                public static void Hall0A1()
                {
                    int a = 0;
                    LeftBone b1 = new(true, 4, 90) { MarkScore = false };
                    RightBone b2 = new(true, 4, 90);
                    CreateBone(b1);
                    CreateBone(b2);
                    AddInstance(new TimeRangedEvent(20, () =>
                    {
                        b1.MissionLength -= 4;
                        b2.MissionLength -= 4;
                    }));
                    AddInstance(new TimeRangedEvent(105, 15, () =>
                    {
                        b1.MissionLength += a * 1.5f;
                        b2.MissionLength += a * 1.5f;
                        a++;
                    }));
                }
                public static void Hall0A2()
                {
                    float sin = 0;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, 40, () =>
                    {
                        sin = Sin(b * 2.5f) * 60;
                        b += 2;
                    }));
                    float sin1 = 0;
                    float b1 = 0;
                    AddInstance(new TimeRangedEvent(40, 40, () =>
                    {
                        sin1 = Sin(b1 * 2.5f) * 60;
                        b1 += 2;
                    }));
                    float sin2 = 0;
                    float b2 = 0;
                    AddInstance(new TimeRangedEvent(80, 40, () =>
                    {
                        sin2 = Sin(b2 * 2.5f) * 60;
                        b2 += 2;
                    }));
                    float sin3 = 0;
                    float b3 = 0;
                    AddInstance(new TimeRangedEvent(120, 40, () =>
                    {
                        sin3 = Sin(b3 * 2.5f) * 60;
                        b3 += 2;
                    }));
                    for (int a = 0; a < 20; a++)
                    {
                        AddInstance(new TimeRangedEvent(a * 2, 1, () =>
                        {
                            CreateBone(new LeftBone(true, 5.25f, 20 + sin) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40, 1, () =>
                        {
                            CreateBone(new RightBone(true, 5.25f, 20 + sin1) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40 * 2, 1, () =>
                        {
                            CreateBone(new LeftBone(true, 5.25f, 20 + sin2) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40 * 3, 1, () =>
                        {
                            CreateBone(new RightBone(true, 5.25f, 20 + sin3) { MarkScore = a % 2 == 0 });
                        }));
                    }
                }
                public static void Hall0A3()
                {
                    int k = 0;
                    AddInstance(new TimeRangedEvent(game.BeatTime(24), game.BeatTime(128 - 48), () =>
                    {
                        k++;
                        if (k % 8 == 0)
                        {
                            UpForwardLine();
                        }
                    }));
                }
                public static void Hall0A4()
                {
                    DIY_Up_DownBoneWall(320 - 30, 0, 60, 480, 60, 40);
                    AddInstance(new TimeRangedEvent(120, 1, () =>
                    {
                        DIY_Up_DownBoneWall(320 - 90, 480, 60, 480, 60, 40);
                        DIY_Up_DownBoneWall(320 + 30, 480, 60, 480, 60, 40);
                    }));
                }
                public static void Hall0A5()
                {
                    CreateBone(new LeftBone(true, 4.55f, Rand(60 - 40, 60 + 40)));
                    CreateBone(new RightBone(true, 4.55f, 120 - LastRand));
                }
                public static void Hall0A6()
                {
                    CreateEntity(new Boneslab(0, 20, 10, 20));
                    CreateBone(new DownBone(true, 5, 120) { ColorType = 2 });
                    CreateBone(new DownBone(false, 5, 120) { ColorType = 2 });
                }
                public static void Hall0A7()
                {

                }
                public static void Hall0A8()
                {

                }
                #endregion

                public static void Main2A()
                {
                    float speed1 = 9.8f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(6.5f), game.BeatTime(6.5f), () =>
                    {
                        speed1 *= 0.934f;
                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied - new Vector2(speed1, 0);
                        rect.Width += 0.23f;
                        ScreenDrawing.HPBar.AreaOccupied = rect;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(5), () =>
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    }));
                    float time = game.BeatTime(8) + 1;
                    string[] rhythm = {
                    "R", "/", "R", "+0", "+0", "/", "R1", "/",
                    "R", "+0", "+0", "/", "R", "/", "R1", "+01",
                    "+01", "/", "R", "/", "R", "+0", "+0", "/",
                    "$0($2)", "$0($2)", "$0($2)", "/", "R", "/", "R", "/",
                    "R", "/", "R", "+0", "+0", "/", "R1", "/",
                    "R", "+0", "+0", "/", "R", "/", "R1", "+01",
                    "+01", "/", "R", "/", "R", "+0", "+0", "/",
                    "$0($2)", "$0($2)", "$0($2)", "/", "R", "/", "R", "/",
                };
                    Action A = () =>
                    {
                        Line line1 = new(new(80, 0), BoxStates.CurrentBox.CollidingBox.TopLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new(560, 0), BoxStates.CurrentBox.CollidingBox.TopRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new(80, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new(560, 480), BoxStates.CurrentBox.CollidingBox.BottomRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action B = () =>
                    {
                        Line line1 = new(new Vector2(120, 0), new Vector2(120, 480)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new Vector2(520, 0), new Vector2(520, 480)) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action C = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(370 + i * 25, 0), new Vector2(170 + i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action D = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(270 - i * 25, 0), new Vector2(470 - i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 6.5f;
                        if (i % 32 >= 24 && i % 32 <= 26) speed += 3;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);

                        if (i % 32 < 27 && i % 32 > 23)
                            game.DelayBeat(8 + i - 0.2f, C);
                        else if (i % 32 < 31 && i % 32 > 27)
                            game.DelayBeat(8 + i - 0.2f, D);
                        else if (i % 8 == 4)
                        {
                            game.DelayBeat(8 + i - 0.2f, A);
                        }
                        else if (i % 4 == 0)
                        {
                            game.DelayBeat(8 + i - 0.2f, B);
                        }
                    }
                }
                public static void Main2B()
                {
                    float time = game.BeatTime(8) + 1;
                    string[] rhythm = {
                    "R1", "/", "R1", "+01", "+01", "/", "R", "/",
                    "R1", "+01", "+01", "/", "R1", "/", "R", "+0",
                    "+0", "/", "R1", "/", "R1", "+01", "+01", "/",
                    "$0($2)", "$0($2)", "$0($2)", "/", "R1", "/", "R1", "/",
                    "R1", "/", "R1", "+01", "+01", "/", "R", "/",
                    "R1", "+01", "+01", "/", "R1", "/", "R", "+0",
                    "R(R1)", "/", "/", "R(R1)", "/", "/", "R(R1)", "/",
                    "/", "R(R1)", "/", "/", "R", "/", "R1", "/",
                };
                    Action A = () =>
                    {
                        Line line1 = new(new(80, 0), BoxStates.CurrentBox.CollidingBox.TopLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new(560, 0), BoxStates.CurrentBox.CollidingBox.TopRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new(80, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new(560, 480), BoxStates.CurrentBox.CollidingBox.BottomRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action B = () =>
                    {
                        Line line1 = new(new Vector2(0, 50), new Vector2(640, 50)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new Vector2(0, 70), new Vector2(640, 70)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new Vector2(0, 410), new Vector2(640, 410)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new Vector2(0, 430), new Vector2(640, 430)) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action C = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(370 + i * 25, 0), new Vector2(170 + i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action D = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(270 - i * 25, 0), new Vector2(470 - i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action E = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 2; i++)
                            lines.Add(new(new Vector2(220 - i * 45, 0), new Vector2(520 - i * 45, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action F = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 2; i++)
                            lines.Add(new(new Vector2(420 + i * 45, 0), new Vector2(140 + i * 45, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };

                    game.RegisterFunction("E", E);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 6.5f;
                        if (i >= 24 && i <= 26) speed += 3;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);

                        if (i < 27 && i > 23)
                            game.DelayBeat(8 + i - 0.2f, D);
                        else if (i < 31 && i > 27)
                            game.DelayBeat(8 + i - 0.2f, C);
                        else if (i % 8 == 4 && i < 48)
                        {
                            game.DelayBeat(8 + i - 0.2f, A);
                        }
                        if (i % 4 == 0 && i < 48)
                        {
                            game.DelayBeat(8 + i - 0.2f, B);
                        }
                    }
                }

                public static void Rest0A()
                {
                    float speed = 0.0f;
                    for (int i = -1; i <= 1; i++)
                    {
                        float delta = 320;
                        DrawingUtil.NormalLine line = new(320 + i * 12, -150, 320 + i * 12, BoxStates.Up - 320, (int)game.BeatTime(8), 1.0f)
                        {
                            color = Color.Silver * 0.7f,
                        };
                        CreateEntity(line);
                        game.ForBeat(8, () =>
                        {
                            delta *= 0.9f;
                            if (delta > 5) delta -= 4.5f;
                            line.y2 = BoxStates.Up - delta - speed - 2;
                        });
                    }
                    game.ForBeat(2.5f, 6.5f, () =>
                    {
                        speed = speed * 0.9f + 12 * 0.1f;
                        InstantSetBox(BoxStates.Centre.Y - speed, BoxStates.Width, BoxStates.Height);
                        InstantTP(BoxStates.Centre + new Vector2(0, 10));
                    });
                    game.DelayBeat(9.1f, () =>
                    {
                        InstantSetBox(540, 100, 130);
                        SetBox(510, 100, 130);
                        BoxStates.BoxMovingScale = 0.07f;
                        SetSoul(0);

                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied;
                        rect.TopLeft += new Vector2(120, 0);
                        ScreenDrawing.HPBar.AreaOccupied = rect;

                        //creating bone:
                        bool direction = false;
                        int count = 0;
                        for (int y = 12; y <= 440; y += Rand(42, 54))
                        {
                            if (Rand(0, 1.0f) < 0.7f) direction = !direction;
                            else y -= Rand(9, 12);
                            float length = Rand(52, 75) + 20;
                            float distance = (direction ? -1 : 1) * (length / 2 + Rand(-6, 4));
                            float curY = y;
                            bool tag = false;
                            float speed = direction ? -1 : 1;
                            CreateBone(new CustomBone(new(320 + distance, y),
                                (t) =>
                                {
                                    if (Heart.Centre.Y <= curY - 19) tag = true;
                                    if (tag)
                                    {
                                        speed *= 1.1f;
                                        return t.CentrePosition + new Vector2(speed, 0);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, length
                            ));
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                        for (int y = 12; y <= 440; y += Rand(68, 108))
                        {
                            float curY = y;
                            bool tag = false;
                            float speed = 1.0f;
                            CreateBone(new CustomBone(new(320, y),
                                (t) =>
                                {
                                    if (t.CentrePosition.Y < -1000) (t as Entity).Dispose();
                                    if (Heart.Centre.Y <= curY - 3) tag = true;
                                    if (tag)
                                    {
                                        speed += 0.1f;
                                        return t.CentrePosition + new Vector2(0, speed);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, 125
                            )
                            { ColorType = 2 });
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                        while (count < 25)
                        {
                            direction = RandBool();
                            float y = Rand(50, 440);
                            float length = Rand(52, 75) + 20;
                            float distance = (direction ? -1 : 1) * (length / 2 + Rand(26, 54));
                            float curY = y;
                            bool tag = false;
                            float speed = direction ? -1 : 1;
                            CreateBone(new CustomBone(new(320 + distance, y),
                                (t) =>
                                {
                                    if (Heart.Centre.Y <= curY - 19) tag = true;
                                    if (tag)
                                    {
                                        speed *= 1.1f;
                                        return t.CentrePosition + new Vector2(speed, 0);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, length
                            ));
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                    });
                    for (int i = 1; i < 7; i++)
                    {
                        int t = i;
                        game.DelayBeat(i * 8 + 8, () =>
                        {
                            SetBox(new Vector2(320 + (t % 2 == 0 ? 9 : -9), 510 - t * 70), 100, 130);
                        });
                    }
                    game.DelayBeat(63.8f, () =>
                    {
                        game.ForBeat(8, () =>
                        {
                            CollideRect rect = ScreenDrawing.HPBar.AreaOccupied;
                            rect.TopLeft = rect.TopLeft * 0.9f + new Vector2(320, 455 - 12) * 0.1f;
                            ScreenDrawing.HPBar.AreaOccupied = rect;
                        });
                        SetBox(new Vector2(320, 240), 180, 180);
                    });
                    game.DelayBeat(5, () =>
                    {
                        DrawingUtil.BlackScreen(game.BeatTime(4), 1, game.BeatTime(2));
                    });
                }
                public static void Rest0B()
                {
                    Action makeGB = () =>
                    {
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(150, Rand(0, 359)), Heart.Centre, new(1, 0.5f), game.BeatTime(8.3f), game.BeatTime(5)));
                    };
                    for (int i = 0; i < 2; i++)
                        game.DelayBeat(8 + i * 16, makeGB);
                    game.DelayBeat(8 + 5 * 8, makeGB);
                    game.DelayBeat(8 + 4 * 8, () =>
                    {
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(150, Rand(0, 359)), Heart.Centre, new(1, 0.5f), game.BeatTime(16.3f), game.BeatTime(5)));
                    });
                    Action makeBone = () =>
                    {
                        bool dir = RandBool();
                        int signal = dir ? 1 : -1;
                        Vector2 centre = new(320 + signal * Rand(0, 120), BoxStates.Down + 20);
                        float length = 18;
                        Vector2 speed = new Vector2(-signal * Rand(1.5f, 4.0f), Rand(-7f, -4f)) * 0.17f;
                        CreateBone(new CustomBone(centre, (s) =>
                        {
                            speed.Y += 0.003f;
                            return s.CentrePosition + speed;
                        }, (s) => length, (s) => MathUtil.Direction(speed) + 90));
                    };
                    for (int i = 0; i < 12; i++)
                        game.DelayBeat(8 + i * 4, makeBone);
                    game.DelayBeat(64, () =>
                    {
                        TP();
                        SetGreenBox();
                        SetSoul(1);
                    });
                }
                public static void Rest1A()
                {
                    float time = game.BeatTime(8) + 0.25f;
                    string[] rhythm = {
                    "D", "", "", "", "D1", "", "", "",
                    "D1(D)", "", "", "", "", "", "", "",
                    "D", "", "", "", "D1", "", "", "",
                    "D1(D)", "", "", "", "", "", "", "",
                    "D1(D)", "", "", "", "", "", "", "",
                    "D1(D)", "", "", "", "", "", "", "",
                    "D", "", "", "", "D1", "", "", "",
                    "D1(D)", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 5.4f;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                public static void Rest1B()
                {
                    bool soundPlayed = false;
                    Func<float, float, Action> createBone = (t, len) =>
                    {
                        return () =>
                        {
                            float start = 1 + MathF.Sqrt(t);
                            DownBone bone1 = new(false, start, len);
                            DownBone bone2 = new(true, start, len);
                            UpBone bone3 = new(false, start, len);
                            UpBone bone4 = new(true, start, len);
                            SideBone[] bones = { bone1, bone2, bone3, bone4 };

                            foreach (SideBone bone in bones) CreateBone(bone);

                            if (!soundPlayed)
                                PlaySound(Sounds.spearAppear);
                            soundPlayed = true;

                            game.ForBeat(8 - 0.2f, () =>
                            {
                                foreach (SideBone bone in bones)
                                {
                                    bone.Speed *= (0.7f + t * 0.029f);
                                }
                            });
                            game.DelayBeat(8, () =>
                            {
                                if (!soundPlayed)
                                    PlaySound(Sounds.pierce);
                                soundPlayed = true;

                                foreach (SideBone bone in bones)
                                {
                                    bone.Speed = t;
                                }
                            });
                        };
                    };
                    game.ForBeat(72, () => soundPlayed = false);
                    game.DelayBeat(8 - 1, () =>
                    {
                        SetSoul(2);
                        HeartAttribute.Gravity = 5.2f;
                        HeartAttribute.JumpTimeLimit = 2;
                        Heart.GiveForce(0, 5);
                        SetBox(300, 84, 84);

                        game.DelayBeat(0, createBone(4, 20));
                    });
                    game.DelayBeat(8 + 16 - 1, () =>
                    {
                        Heart.GiveForce(180, 6);
                        SetBox(220, 100, 100);
                        game.DelayBeat(0, createBone(5, 22));
                        game.DelayBeat(0, createBone(4, 30));
                    });
                    game.DelayBeat(8 + 32 - 1, () =>
                    {
                        Heart.GiveForce(0, 7);
                        SetBox(310, 120, 120);
                        game.DelayBeat(0, createBone(6, 22));
                        game.DelayBeat(0, createBone(5, 30));
                        game.DelayBeat(0, createBone(4, 38));
                    });
                    game.DelayBeat(8 + 48 - 1, () =>
                    {
                        SetBox(310, 180, 120);
                        CreateGB(new NormalGB(new(100, 285), new(320, 0), new(1, 1), 0, game.BeatTime(8), game.BeatTime(8)));
                        CreateGB(new NormalGB(new(540, 285), new(320, 0), new(1, 1), 180, game.BeatTime(8), game.BeatTime(8)));
                        CreatePlatform(new Platform(0, new(Heart.Centre.X, 310), EasingUtil.CentreEasing.FromDown(64, game.BeatTime(4)), 0, 40, game.BeatTime(12)));
                        game.DelayBeat(8, () =>
                        {
                            for (int i = 0; i < 6; i++) game.DelayBeat(i * 0.667f, () => ScreenDrawing.CameraEffect.Convulse(11 - i, 5, i % 2 == 0));
                            // game.DelayBeat(5, () => ScreenDrawing.CameraEffect.Rotate180(game.BeatTime(16)));
                            ScreenDrawing.CameraEffect.SizeExpand(11, game.BeatTime(4));
                        });
                    });
                    game.DelayBeat(8 + 63.5f, () =>
                    {
                        SetSoul(1);
                        game.ForBeat(0.5f + 128, () => TP(BoxStates.Centre));
                        BoxStates.BoxMovingScale = 0.04f;
                        SetGreenBox();
                    });
                }
            }
            private static class NorBarrage
            {
                public static void Intro0()
                {
                    BoxStates.BoxMovingScale = 0.07f;
                    SetBox(320, 250, 160);
                    for (int i = 0; i < 4; i++)
                    {
                        float cur = game.BeatTime(i * 16 + 8);
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new DownBone(false, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 1;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new UpBone(true, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 2;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                    }
                }
                public static void Intro1()
                {
                    for (int i = 0; i < 3; i++)
                    {
                        float cur = game.BeatTime(i * 16 + 8);
                        int t = i;
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new DownBone(false, 5.3f, 77));
                            if (t <= 1)
                            {
                                AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                                {
                                    bone.Speed -= 10.6f / game.BeatTime(3);
                                    bone.MissionLength += 78 / game.BeatTime(3);
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(8), () =>
                                {
                                    bone.ColorType = 1;
                                    PlaySound(Sounds.Ding, 0.8f);
                                }));
                            }
                        }));
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new UpBone(true, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 2;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                    }
                    float cur2 = game.BeatTime(56 - 4);
                    SideBone a = new UpBone(true, 320, 0, 60), b = new DownBone(true, 320, 0, 60);
                    AddInstance(new InstantEvent(cur2, () =>
                    {
                        SetBox(320, 80, 160);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(3.5f), () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(4.3f), () =>
                    {
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(5.3f), () =>
                    {
                        Heart.GiveForce(90, 20);
                        a.MissionLength = 30;
                        b.MissionLength = 90;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(6.1f), () =>
                    {
                        SetSoul(0);
                        Heart.RotateTo(0);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(7.4f), () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(8.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(9.4f), () =>
                    {
                        Heart.GiveForce(270, 20);
                        a.MissionLength = 60;
                        b.MissionLength = 60;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(11.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(12.4f), () =>
                    {
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(13.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                        a.MissionLength = 20;
                        b.MissionLength = 80;
                        SetBox(310, 80, 140);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(15.1f), () =>
                    {
                        Heart.GiveForce(180, 20);
                        a.Speed = b.Speed = -5;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(16.0f), () =>
                    {
                        a.Speed = b.Speed = 5;
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(18.1f), () =>
                    {
                        SetSoul(1);
                        TP();
                        PlaySound(Sounds.switchScene);
                        SetGreenBox();
                        Heart.RotateTo(0);
                        BoxStates.BoxMovingScale = 0.15f;
                    }));
                }
                public static void Main0A()
                {
                    BoxStates.BoxMovingScale = 0.15f;
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "R", "+0", "/", "/", "/",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6.5f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6.5f, "+0");
                        time += game.BeatTime(1);
                    }
                    bool[] seq = { RandBool(), RandBool(), RandBool(), RandBool() };

                    float del = 30;
                    DrawingUtil.NormalLine line1 = new(0, 130, 640, 130 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line2 = new(0, 142, 640, 142 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line3 = new(0, 130 + del, 640, 130, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line4 = new(0, 142 + del, 640, 139, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine[] lines = { line1, line2, line3, line4 };
                    foreach (DrawingUtil.NormalLine line in lines)
                    {
                        line.alpha = 0;
                        line.width = 4f;
                    }
                    game.DelayBeat(36, () => { foreach (DrawingUtil.NormalLine line in lines) CreateEntity(line); });
                    game.ForBeat(40 - 4, 4, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha += 0.03f; });
                    game.ForBeat(54, 8, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha -= 0.05f; });

                    AddInstance(new InstantEvent(game.BeatTime(40 - 0.5f), () => { SetSoul(0); }));
                    for (int i = 0; i < 2; i++)
                    {
                        int t = i;
                        bool dir = seq[t];
                        AddInstance(new InstantEvent(game.BeatTime(32 + i * 4), () =>
                        {
                            MakeLine(t, dir);
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(40 + i * 8 - 0.5f), () =>
                        {
                            SetBox(300, 80, 160);
                            SideBone bone;
                            if (seq[t]) CreateBone(bone = new RightBone(false, 300, 0, 36));
                            else CreateBone(bone = new LeftBone(false, 300, 0, 36));

                            game.DelayBeat(1, () =>
                            {
                                if (t % 2 == 0)
                                {
                                    float speed = 7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                                else
                                {
                                    float speed = -7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                            });

                            AddInstance(new InstantEvent(game.BeatTime(4), () => { bone.Dispose(); }));
                            AddInstance(new InstantEvent(game.BeatTime(2), () =>
                            {
                                if (t % 2 == 0)
                                {
                                    SetBox(370, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Up), 0, Color.Magenta * 0.6f); });
                                }
                                else
                                {
                                    SetBox(230, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Down), 0, Color.Magenta * 0.6f); });
                                }
                            }));
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(56), () => { SetSoul(1); TP(); SetGreenBox(); }));
                }
                public static void Main0B()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "/", "/", "R",
                    "(+0)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "/",
                    "(R)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "/",
                    "(R)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "/",
                    "(R)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6.5f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6.5f, "+0");
                        time += game.BeatTime(1);
                    }
                }
                public static void Main1A()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+21", "+2", "+21", "+2", "+21", "/", "/",
                    "R", "+21", "+2", "+21", "+2", "+21", "/", "/",
                    "R", "+21", "+2", "+21", "+2", "+21", "/", "/",
                    "R", "+21", "+2", "+21", "+2", "+21", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "R", "+1", "/", "/", "/",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6.5f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6.5f, "+0");
                        time += game.BeatTime(1);
                    }

                    bool[] seq = { RandBool(), RandBool(), RandBool(), RandBool() };

                    float del = 30;
                    DrawingUtil.NormalLine line1 = new(0, 130, 640, 130 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line2 = new(0, 142, 640, 142 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line3 = new(0, 130 + del, 640, 130, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line4 = new(0, 142 + del, 640, 139, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine[] lines = { line1, line2, line3, line4 };
                    foreach (DrawingUtil.NormalLine line in lines)
                    {
                        line.alpha = 0;
                        line.width = 4f;
                    }
                    game.DelayBeat(36, () => { foreach (DrawingUtil.NormalLine line in lines) CreateEntity(line); });
                    game.ForBeat(40 - 4, 4, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha += 0.03f; });
                    game.ForBeat(54, 8, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha -= 0.05f; });

                    AddInstance(new InstantEvent(game.BeatTime(40 - 0.5f), () => { SetSoul(0); }));
                    for (int i = 0; i < 2; i++)
                    {
                        int t = i;
                        bool dir = seq[t];
                        AddInstance(new InstantEvent(game.BeatTime(32 + i * 4), () =>
                        {
                            MakeLine(t, dir);
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(40 + i * 8 - 0.5f), () =>
                        {
                            SetBox(300, 80, 160);
                            SideBone bone;
                            if (seq[t]) CreateBone(bone = new RightBone(false, 300, 0, 36));
                            else CreateBone(bone = new LeftBone(false, 300, 0, 36));

                            game.DelayBeat(1, () =>
                            {
                                if (t % 2 == 0)
                                {
                                    float speed = 7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                                else
                                {
                                    float speed = -7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                            });

                            AddInstance(new InstantEvent(game.BeatTime(4), () => { bone.Dispose(); }));
                            AddInstance(new InstantEvent(game.BeatTime(2), () =>
                            {
                                if (t % 2 == 0)
                                {
                                    SetBox(370, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Up), 0, Color.Magenta * 0.6f); });
                                }
                                else
                                {
                                    SetBox(230, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Down), 0, Color.Magenta * 0.6f); });
                                }
                            }));
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(56), () => { SetSoul(1); TP(); SetGreenBox(); }));
                }
                public static void Main1B()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "R", "+2", "+2", "+2", "+2", "/", "/", "R",
                    "(+0)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "R",
                    "(+0)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "R",
                    "(+0)(+01)", "/", "/", "R1", "", "/", "/", "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6.5f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6.5f, "+0");
                        if (i == 52) CreateGB(new GreenSoulGB(time, "R", 0, game.BeatTime(8)));
                        time += game.BeatTime(1);
                    }
                }
                #region The Hall
                public static void Hall0A0()
                {
                    int h = 0;
                    AddInstance(new InstantEvent(game.BeatTime(7.5f), () =>
                    {
                        SetBox(320 - 82, 320 + 82, 240 - 42, 490);
                        SetSoul(2);
                        HeartAttribute.Gravity = 0;
                        Heart.GiveForce(0, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(8), 95, () =>
                    {
                        TP(320, 240 - h * h * 0.0105f);
                        SetBox(320 - 82, 320 + 82, 240 - 42 - h * h * 0.45f, 490);
                        h++;
                    }));
                    float speed = 9.8f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(6.5f), game.BeatTime(6.5f), () =>
                    {
                        speed *= 0.934f;
                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied + new Vector2(speed, 0);
                        rect.Width -= 0.23f;
                        ScreenDrawing.HPBar.AreaOccupied = rect;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(128 - 8), () =>
                    {
                        SetBox(320 - 82, 320 + 82, -20, 240 + 120);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(128 - 4), () =>
                    {
                        Heart.GiveForce(0, 10);
                    }));
                }
                public static void Hall0A1()
                {
                    int a = 0;
                    LeftBone b1 = new(true, 4, 90) { MarkScore = false };
                    RightBone b2 = new(true, 4, 90);
                    CreateBone(b1);
                    CreateBone(b2);
                    AddInstance(new TimeRangedEvent(20, () =>
                    {
                        b1.MissionLength -= 4;
                        b2.MissionLength -= 4;
                    }));
                    AddInstance(new TimeRangedEvent(105, 15, () =>
                    {
                        b1.MissionLength += a * 1.5f;
                        b2.MissionLength += a * 1.5f;
                        a++;
                    }));
                }
                public static void Hall0A2()
                {
                    float sin = 0;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, 40, () =>
                    {
                        sin = Sin(b * 2.5f) * 85;
                        b += 2;
                    }));
                    float sin1 = 0;
                    float b1 = 0;
                    AddInstance(new TimeRangedEvent(40, 40, () =>
                    {
                        sin1 = Sin(b1 * 2.5f) * 85;
                        b1 += 2;
                    }));
                    float sin2 = 0;
                    float b2 = 0;
                    AddInstance(new TimeRangedEvent(80, 40, () =>
                    {
                        sin2 = Sin(b2 * 2.5f) * 85;
                        b2 += 2;
                    }));
                    float sin3 = 0;
                    float b3 = 0;
                    AddInstance(new TimeRangedEvent(120, 40, () =>
                    {
                        sin3 = Sin(b3 * 2.5f) * 85;
                        b3 += 2;
                    }));
                    for (int a = 0; a < 20; a++)
                    {
                        AddInstance(new TimeRangedEvent(a * 2, 1, () =>
                        {
                            CreateBone(new LeftBone(true, 5.25f, 20 + sin) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40, 1, () =>
                        {
                            CreateBone(new RightBone(true, 5.25f, 20 + sin1) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40 * 2, 1, () =>
                        {
                            CreateBone(new LeftBone(true, 5.25f, 20 + sin2) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40 * 3, 1, () =>
                        {
                            CreateBone(new RightBone(true, 5.25f, 20 + sin3) { MarkScore = a % 2 == 0 });
                        }));
                    }
                }
                public static void Hall0A3()
                {
                    int k = 0;
                    AddInstance(new TimeRangedEvent(game.BeatTime(24), game.BeatTime(128 - 48), () =>
                    {
                        k++;
                        if (k % 8 == 0)
                        {
                            UpForwardLine();
                        }
                    }));
                }
                public static void Hall0A4()
                {
                    DIY_Up_DownBoneWall(320 - 30, 0, 60, 480, 60, 40);
                    AddInstance(new TimeRangedEvent(120, 1, () =>
                    {
                        DIY_Up_DownBoneWall(320 - 90, 480, 60, 480, 60, 40);
                        DIY_Up_DownBoneWall(320 + 30, 480, 60, 480, 60, 40);
                    }));
                }
                public static void Hall0A5()
                {
                    CreateBone(new LeftBone(true, 4.55f, Rand(60 - 40, 60 + 40)));
                    CreateBone(new RightBone(true, 4.55f, 120 - LastRand));
                }
                public static void Hall0A6()
                {
                    CreateEntity(new Boneslab(0, 20, 10, 20));
                    CreateBone(new DownBone(true, 5, 120) { ColorType = 2 });
                    CreateBone(new DownBone(false, 5, 120) { ColorType = 2 });
                }
                public static void Hall0A7()
                {

                }
                public static void Hall0A8()
                {

                }
                #endregion

                public static void Main2A()
                {
                    float speed1 = 9.8f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(6.5f), game.BeatTime(6.5f), () =>
                    {
                        speed1 *= 0.934f;
                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied - new Vector2(speed1, 0);
                        rect.Width += 0.23f;
                        ScreenDrawing.HPBar.AreaOccupied = rect;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(5), () =>
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    }));
                    float time = game.BeatTime(8) + 1;
                    string[] rhythm = {
                    "R", "/", "R", "+0", "+0", "/", "R", "/",
                    "R", "+0", "+0", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "/", "R", "+0", "+0", "/",
                    "$0", "$0", "$0", "/", "R", "/", "R", "/",
                    "R", "/", "R", "+0", "+0", "/", "R", "/",
                    "R", "+0", "+0", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "/", "R", "+0", "+0", "/",
                    "$2", "($2)", "$2", "/", "R", "/", "R", "/",
                };
                    Action A = () =>
                    {
                        Line line1 = new(new(80, 0), BoxStates.CurrentBox.CollidingBox.TopLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new(560, 0), BoxStates.CurrentBox.CollidingBox.TopRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new(80, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new(560, 480), BoxStates.CurrentBox.CollidingBox.BottomRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action B = () =>
                    {
                        Line line1 = new(new Vector2(120, 0), new Vector2(120, 480)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new Vector2(520, 0), new Vector2(520, 480)) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action C = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(370 + i * 25, 0), new Vector2(170 + i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action D = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(270 - i * 25, 0), new Vector2(470 - i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 6.5f;
                        if (i % 32 >= 24 && i % 32 <= 26) speed += 3;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);

                        if (i % 32 < 27 && i % 32 > 23)
                            game.DelayBeat(8 + i - 0.2f, C);
                        else if (i % 32 < 31 && i % 32 > 27)
                            game.DelayBeat(8 + i - 0.2f, D);
                        else if (i % 8 == 4)
                        {
                            game.DelayBeat(8 + i - 0.2f, A);
                        }
                        else if (i % 4 == 0)
                        {
                            game.DelayBeat(8 + i - 0.2f, B);
                        }
                    }
                }
                public static void Main2B()
                {
                    float time = game.BeatTime(8) + 1;
                    string[] rhythm = {
                    "R", "/", "R", "+0", "+0", "/", "R", "/",
                    "R", "+0", "+0", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "/", "R", "+0", "+0", "/",
                    "$0", "$0", "$0", "/", "R", "/", "R", "/",
                    "R", "/", "R", "+0", "+0", "/", "R", "/",
                    "R", "+0", "+0", "/", "R", "/", "R", "+0",
                    "(R)(+01)", "/", "/", "(R)(+01)", "/", "/", "(R)(+01)", "/",
                    "/", "(R)(+01)", "/", "/", "R", "/", "R1", "/",
                };
                    Action A = () =>
                    {
                        Line line1 = new(new(80, 0), BoxStates.CurrentBox.CollidingBox.TopLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new(560, 0), BoxStates.CurrentBox.CollidingBox.TopRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new(80, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new(560, 480), BoxStates.CurrentBox.CollidingBox.BottomRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action B = () =>
                    {
                        Line line1 = new(new Vector2(0, 50), new Vector2(640, 50)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new Vector2(0, 70), new Vector2(640, 70)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new Vector2(0, 410), new Vector2(640, 410)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new Vector2(0, 430), new Vector2(640, 430)) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action C = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(370 + i * 25, 0), new Vector2(170 + i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action D = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(270 - i * 25, 0), new Vector2(470 - i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action E = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 2; i++)
                            lines.Add(new(new Vector2(220 - i * 45, 0), new Vector2(520 - i * 45, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action F = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 2; i++)
                            lines.Add(new(new Vector2(420 + i * 45, 0), new Vector2(140 + i * 45, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };

                    game.RegisterFunction("E", E);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 6.5f;
                        if (i >= 24 && i <= 26) speed += 3;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);

                        if (i < 27 && i > 23)
                            game.DelayBeat(8 + i - 0.2f, D);
                        else if (i < 31 && i > 27)
                            game.DelayBeat(8 + i - 0.2f, C);
                        else if (i % 8 == 4 && i < 48)
                        {
                            game.DelayBeat(8 + i - 0.2f, A);
                        }
                        if (i % 4 == 0 && i < 48)
                        {
                            game.DelayBeat(8 + i - 0.2f, B);
                        }
                    }
                }

                public static void Rest0A()
                {
                    float speed = 0.0f;
                    for (int i = -1; i <= 1; i++)
                    {
                        float delta = 320;
                        DrawingUtil.NormalLine line = new(320 + i * 12, -150, 320 + i * 12, BoxStates.Up - 320, (int)game.BeatTime(8), 1.0f)
                        {
                            color = Color.Silver * 0.7f,
                        };
                        CreateEntity(line);
                        game.ForBeat(8, () =>
                        {
                            delta *= 0.9f;
                            if (delta > 5) delta -= 4.5f;
                            line.y2 = BoxStates.Up - delta - speed - 2;
                        });
                    }
                    game.ForBeat(2.5f, 6.5f, () =>
                    {
                        speed = speed * 0.9f + 12 * 0.1f;
                        InstantSetBox(BoxStates.Centre.Y - speed, BoxStates.Width, BoxStates.Height);
                        InstantTP(BoxStates.Centre + new Vector2(0, 10));
                    });
                    game.DelayBeat(9.1f, () =>
                    {
                        InstantSetBox(540, 100, 130);
                        SetBox(510, 100, 130);
                        BoxStates.BoxMovingScale = 0.07f;
                        SetSoul(0);

                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied;
                        rect.TopLeft += new Vector2(120, 0);
                        ScreenDrawing.HPBar.AreaOccupied = rect;

                        //creating bone:
                        bool direction = false;
                        int count = 0;
                        for (int y = 12; y <= 440; y += Rand(42, 54))
                        {
                            if (Rand(0, 1.0f) < 0.7f) direction = !direction;
                            else y -= Rand(9, 12);
                            float length = Rand(52, 75) + 20;
                            float distance = (direction ? -1 : 1) * (length / 2 + Rand(-6, 4));
                            float curY = y;
                            bool tag = false;
                            float speed = direction ? -1 : 1;
                            CreateBone(new CustomBone(new(320 + distance, y),
                                (t) =>
                                {
                                    if (Heart.Centre.Y <= curY - 19) tag = true;
                                    if (tag)
                                    {
                                        speed *= 1.1f;
                                        return t.CentrePosition + new Vector2(speed, 0);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, length
                            ));
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                        for (int y = 12; y <= 440; y += Rand(68, 108))
                        {
                            float curY = y;
                            bool tag = false;
                            float speed = 1.0f;
                            CreateBone(new CustomBone(new(320, y),
                                (t) =>
                                {
                                    if (t.CentrePosition.Y < -1000) (t as Entity).Dispose();
                                    if (Heart.Centre.Y <= curY - 3) tag = true;
                                    if (tag)
                                    {
                                        speed += 0.1f;
                                        return t.CentrePosition + new Vector2(0, speed);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, 125
                            )
                            { ColorType = 2 });
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                        while (count < 25)
                        {
                            direction = RandBool();
                            float y = Rand(50, 440);
                            float length = Rand(52, 75) + 20;
                            float distance = (direction ? -1 : 1) * (length / 2 + Rand(26, 54));
                            float curY = y;
                            bool tag = false;
                            float speed = direction ? -1 : 1;
                            CreateBone(new CustomBone(new(320 + distance, y),
                                (t) =>
                                {
                                    if (Heart.Centre.Y <= curY - 19) tag = true;
                                    if (tag)
                                    {
                                        speed *= 1.1f;
                                        return t.CentrePosition + new Vector2(speed, 0);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, length
                            ));
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                    });
                    for (int i = 1; i < 7; i++)
                    {
                        int t = i;
                        game.DelayBeat(i * 8 + 8, () =>
                        {
                            SetBox(new Vector2(320 + (t % 2 == 0 ? 9 : -9), 510 - t * 70), 100, 130);
                        });
                    }
                    game.DelayBeat(63.8f, () =>
                    {
                        game.ForBeat(8, () =>
                        {
                            CollideRect rect = ScreenDrawing.HPBar.AreaOccupied;
                            rect.TopLeft = rect.TopLeft * 0.9f + new Vector2(320, 455 - 12) * 0.1f;
                            ScreenDrawing.HPBar.AreaOccupied = rect;
                        });
                        SetBox(new Vector2(320, 240), 180, 180);
                    });
                    game.DelayBeat(5, () =>
                    {
                        DrawingUtil.BlackScreen(game.BeatTime(4), 1, game.BeatTime(2));
                    });
                }
                public static void Rest0B()
                {
                    Action makeGB = () =>
                    {
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(150, Rand(0, 359)), Heart.Centre, new(1, 0.5f), game.BeatTime(8.3f), game.BeatTime(5)));
                    };
                    for (int i = 0; i < 2; i++)
                        game.DelayBeat(8 + i * 16, makeGB);
                    game.DelayBeat(8 + 5 * 8, makeGB);
                    game.DelayBeat(8 + 4 * 8, () =>
                    {
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(150, Rand(0, 359)), Heart.Centre, new(1, 0.5f), game.BeatTime(16.3f), game.BeatTime(5)));
                    });
                    Action makeBone = () =>
                    {
                        bool dir = RandBool();
                        int signal = dir ? 1 : -1;
                        Vector2 centre = new(320 + signal * Rand(0, 120), BoxStates.Down + 20);
                        float length = 18;
                        Vector2 speed = new Vector2(-signal * Rand(1.5f, 4.0f), Rand(-7f, -4f)) * 0.17f;
                        CreateBone(new CustomBone(centre, (s) =>
                        {
                            speed.Y += 0.003f;
                            return s.CentrePosition + speed;
                        }, (s) => length, (s) => MathUtil.Direction(speed) + 90));
                    };
                    for (int i = 0; i < 12; i++)
                        game.DelayBeat(8 + i * 4, makeBone);
                    game.DelayBeat(64, () =>
                    {
                        TP();
                        SetGreenBox();
                        SetSoul(1);
                    });
                }
                public static void Rest1A()
                {
                    float time = game.BeatTime(8) + 0.25f;
                    string[] rhythm = {
                    "D", "", "", "", "D1", "", "", "",
                    "(D)(+01)", "", "", "", "", "", "", "",
                    "D", "", "", "", "D1", "", "", "",
                    "(D)(+01)", "", "", "", "", "", "", "",
                    "(D)(+01)", "", "", "", "", "", "", "",
                    "(D)(+01)", "", "", "", "", "", "", "",
                    "D", "", "", "", "D1", "", "", "",
                    "(D)(+01)", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 5.4f;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                public static void Rest1B()
                {
                    bool soundPlayed = false;
                    Func<float, float, Action> createBone = (t, len) =>
                    {
                        return () =>
                        {
                            float start = 1 + MathF.Sqrt(t);
                            DownBone bone1 = new(false, start, len);
                            DownBone bone2 = new(true, start, len);
                            UpBone bone3 = new(false, start, len);
                            UpBone bone4 = new(true, start, len);
                            SideBone[] bones = { bone1, bone2, bone3, bone4 };

                            foreach (SideBone bone in bones) CreateBone(bone);

                            if (!soundPlayed)
                                PlaySound(Sounds.spearAppear);
                            soundPlayed = true;

                            game.ForBeat(8 - 0.2f, () =>
                            {
                                foreach (SideBone bone in bones)
                                {
                                    bone.Speed *= (0.7f + t * 0.029f);
                                }
                            });
                            game.DelayBeat(8, () =>
                            {
                                if (!soundPlayed)
                                    PlaySound(Sounds.pierce);
                                soundPlayed = true;

                                foreach (SideBone bone in bones)
                                {
                                    bone.Speed = t;
                                }
                            });
                        };
                    };
                    game.ForBeat(72, () => soundPlayed = false);
                    game.DelayBeat(8 - 1, () =>
                    {
                        SetSoul(2);
                        HeartAttribute.Gravity = 5.2f;
                        HeartAttribute.JumpTimeLimit = 2;
                        Heart.GiveForce(0, 5);
                        SetBox(300, 84, 84);

                        game.DelayBeat(0, createBone(4, 20));
                    });
                    game.DelayBeat(8 + 16 - 1, () =>
                    {
                        Heart.GiveForce(180, 6);
                        SetBox(220, 100, 100);
                        game.DelayBeat(0, createBone(5, 22));
                        game.DelayBeat(0, createBone(4, 30));
                    });
                    game.DelayBeat(8 + 32 - 1, () =>
                    {
                        Heart.GiveForce(0, 7);
                        SetBox(310, 120, 120);
                        game.DelayBeat(0, createBone(6, 22));
                        game.DelayBeat(0, createBone(5, 30));
                        game.DelayBeat(0, createBone(4, 38));
                    });
                    game.DelayBeat(8 + 48 - 1, () =>
                    {
                        SetBox(310, 180, 120);
                        CreateGB(new NormalGB(new(100, 285), new(320, 0), new(1, 1), 0, game.BeatTime(8), game.BeatTime(8)));
                        CreateGB(new NormalGB(new(540, 285), new(320, 0), new(1, 1), 180, game.BeatTime(8), game.BeatTime(8)));
                        CreatePlatform(new Platform(0, new(Heart.Centre.X, 310), EasingUtil.CentreEasing.FromDown(64, game.BeatTime(4)), 0, 40, game.BeatTime(12)));
                        game.DelayBeat(8, () =>
                        {
                            for (int i = 0; i < 6; i++) game.DelayBeat(i * 0.667f, () => ScreenDrawing.CameraEffect.Convulse(11 - i, 5, i % 2 == 0));
                            // game.DelayBeat(5, () => ScreenDrawing.CameraEffect.Rotate180(game.BeatTime(16)));
                            ScreenDrawing.CameraEffect.SizeExpand(11, game.BeatTime(4));
                        });
                    });
                    game.DelayBeat(8 + 63.5f, () =>
                    {
                        SetSoul(1);
                        game.ForBeat(0.5f + 128, () => TP(BoxStates.Centre));
                        BoxStates.BoxMovingScale = 0.04f;
                        SetGreenBox();
                    });
                }
            }
            private static class EasyBarrage
            {
                public static void Intro0()
                {
                    BoxStates.BoxMovingScale = 0.07f;
                    SetBox(320, 250, 160);
                    for (int i = 0; i < 2; i++)
                    {
                        float cur = game.BeatTime(i * 32 + 8);
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new DownBone(false, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 1;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new UpBone(true, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 2;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                    }
                }
                public static void Intro1()
                {
                    for (int i = 0; i < 3; i++)
                    {
                        float cur = game.BeatTime(i * 16 + 8);
                        int t = i;
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new DownBone(false, 5.3f, 77));
                            if (t <= 1)
                            {
                                AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                                {
                                    bone.Speed -= 10.6f / game.BeatTime(3);
                                    bone.MissionLength += 78 / game.BeatTime(3);
                                }));
                                AddInstance(new InstantEvent(game.BeatTime(8), () =>
                                {
                                    bone.ColorType = 1;
                                    PlaySound(Sounds.Ding, 0.8f);
                                }));
                            }
                        }));
                        AddInstance(new InstantEvent(cur, () =>
                        {
                            SideBone bone;
                            PlaySound(Sounds.pierce, 0.8f);
                            CreateBone(bone = new UpBone(true, 5.3f, 77));
                            AddInstance(new TimeRangedEvent(game.BeatTime(7), game.BeatTime(3), () =>
                            {
                                bone.Speed -= 10.6f / game.BeatTime(3);
                                bone.MissionLength += 78 / game.BeatTime(3);
                            }));
                            AddInstance(new InstantEvent(game.BeatTime(8), () =>
                            {
                                bone.ColorType = 2;
                                PlaySound(Sounds.Ding, 0.8f);
                            }));
                        }));
                    }
                    float cur2 = game.BeatTime(56 - 4);
                    SideBone a = new UpBone(true, 320, 0, 60), b = new DownBone(true, 320, 0, 60);
                    AddInstance(new InstantEvent(cur2, () =>
                    {
                        SetBox(320, 80, 160);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(3.5f), () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(4.3f), () =>
                    {
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(5.3f), () =>
                    {
                        Heart.GiveForce(90, 20);
                        a.MissionLength = 30;
                        b.MissionLength = 90;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(6.1f), () =>
                    {
                        SetSoul(0);
                        Heart.RotateTo(0);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(7.4f), () =>
                    {
                        SetSoul(2);
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(8.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(9.4f), () =>
                    {
                        Heart.GiveForce(270, 20);
                        a.MissionLength = 60;
                        b.MissionLength = 60;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(11.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(12.4f), () =>
                    {
                        Heart.GiveForce(270, 20);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(13.4f), () =>
                    {
                        Heart.GiveForce(90, 20);
                        a.MissionLength = 20;
                        b.MissionLength = 80;
                        SetBox(310, 80, 140);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(15.1f), () =>
                    {
                        Heart.GiveForce(180, 20);
                        a.Speed = b.Speed = -5;
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(16.0f), () =>
                    {
                        a.Speed = b.Speed = 5;
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new InstantEvent(cur2 + game.BeatTime(18.1f), () =>
                    {
                        SetSoul(1);
                        TP();
                        PlaySound(Sounds.switchScene);
                        SetGreenBox();
                        Heart.RotateTo(0);
                        BoxStates.BoxMovingScale = 0.15f;
                    }));
                }
                public static void Main0A()
                {
                    BoxStates.BoxMovingScale = 0.15f;
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "R", "+0", "/", "/", "/",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6f, "+0");
                        time += game.BeatTime(1);
                    }
                    bool[] seq = { RandBool(), RandBool(), RandBool(), RandBool() };

                    float del = 30;
                    DrawingUtil.NormalLine line1 = new(0, 130, 640, 130 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line2 = new(0, 142, 640, 142 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line3 = new(0, 130 + del, 640, 130, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line4 = new(0, 142 + del, 640, 139, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine[] lines = { line1, line2, line3, line4 };
                    foreach (DrawingUtil.NormalLine line in lines)
                    {
                        line.alpha = 0;
                        line.width = 4f;
                    }
                    game.DelayBeat(36, () => { foreach (DrawingUtil.NormalLine line in lines) CreateEntity(line); });
                    game.ForBeat(40 - 4, 4, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha += 0.03f; });
                    game.ForBeat(54, 8, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha -= 0.05f; });

                    AddInstance(new InstantEvent(game.BeatTime(40 - 0.5f), () => { SetSoul(0); }));
                    for (int i = 0; i < 1; i++)
                    {
                        int t = i;
                        bool dir = seq[t];
                        AddInstance(new InstantEvent(game.BeatTime(32 + i * 8), () =>
                        {
                            MakeLine(t, dir);
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(40 + i * 16 - 0.5f), () =>
                        {
                            SetBox(300, 80, 160);
                            SideBone bone;
                            if (seq[t]) CreateBone(bone = new RightBone(false, 300, 0, 36));
                            else CreateBone(bone = new LeftBone(false, 300, 0, 36));

                            game.DelayBeat(1, () =>
                            {
                                if (t % 2 == 0)
                                {
                                    float speed = 7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                                else
                                {
                                    float speed = -7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                            });

                            AddInstance(new InstantEvent(game.BeatTime(4), () => { bone.Dispose(); }));
                            AddInstance(new InstantEvent(game.BeatTime(2), () =>
                            {
                                if (t % 2 == 0)
                                {
                                    SetBox(370, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Up), 0, Color.Magenta * 0.6f); });
                                }
                                else
                                {
                                    SetBox(230, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Down), 0, Color.Magenta * 0.6f); });
                                }
                            }));
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(56), () => { SetSoul(1); TP(); SetGreenBox(); }));
                }
                public static void Main0B()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "/", "/", "",
                    "(+0)", "/", "/", "/", "(R)", "/", "/", "/",
                    "(+0)", "/", "/", "/", "(R)", "/", "/", "/",
                    "(+0)", "/", "/", "/", "(R)", "/", "/", "/",
                    "(+0)", "/", "/", "/", "(R)", "/", "/", "/",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6f, "+0");
                        time += game.BeatTime(1);
                    }
                }
                public static void Main1A()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "/", "/", "/", "/", "/",
                    "/", "/", "/", "R", "+0", "/", "/", "/",
                };

                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6f, "+0");
                        time += game.BeatTime(1);
                    }

                    bool[] seq = { RandBool(), RandBool(), RandBool(), RandBool() };

                    float del = 30;
                    DrawingUtil.NormalLine line1 = new(0, 130, 640, 130 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line2 = new(0, 142, 640, 142 + del, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line3 = new(0, 130 + del, 640, 130, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine line4 = new(0, 142 + del, 640, 139, (int)game.BeatTime(58), 0.5f) { color = Color.AliceBlue * 0.89f };
                    DrawingUtil.NormalLine[] lines = { line1, line2, line3, line4 };
                    foreach (DrawingUtil.NormalLine line in lines)
                    {
                        line.alpha = 0;
                        line.width = 4f;
                    }
                    game.DelayBeat(36, () => { foreach (DrawingUtil.NormalLine line in lines) CreateEntity(line); });
                    game.ForBeat(40 - 4, 4, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha += 0.03f; });
                    game.ForBeat(54, 8, () => { foreach (DrawingUtil.NormalLine line in lines) line.alpha -= 0.05f; });

                    AddInstance(new InstantEvent(game.BeatTime(40 - 0.5f), () => { SetSoul(0); }));
                    for (int i = 0; i < 1; i++)
                    {
                        int t = i;
                        bool dir = seq[t];
                        AddInstance(new InstantEvent(game.BeatTime(32 + i * 8), () =>
                        {
                            MakeLine(t, dir);
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(40 + i * 16 - 0.5f), () =>
                        {
                            SetBox(300, 80, 160);
                            SideBone bone;
                            if (seq[t]) CreateBone(bone = new RightBone(false, 300, 0, 36));
                            else CreateBone(bone = new LeftBone(false, 300, 0, 36));

                            game.DelayBeat(1, () =>
                            {
                                if (t % 2 == 0)
                                {
                                    float speed = 7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                                else
                                {
                                    float speed = -7.0f;
                                    game.ForBeat120(3, () =>
                                    {
                                        for (int i = 0; i < lines.Length; i++)
                                        {
                                            DrawingUtil.NormalLine line = lines[i];
                                            if (i < 2)
                                            {
                                                line.y1 += speed; line.y2 += speed * 0.12f;
                                            }
                                            else
                                            {
                                                line.y1 += speed * 0.12f; line.y2 += speed;
                                            }
                                        }
                                        speed *= 0.91f;
                                    });
                                }
                            });

                            AddInstance(new InstantEvent(game.BeatTime(4), () => { bone.Dispose(); }));
                            AddInstance(new InstantEvent(game.BeatTime(2), () =>
                            {
                                if (t % 2 == 0)
                                {
                                    SetBox(370, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Up), 0, Color.Magenta * 0.6f); });
                                }
                                else
                                {
                                    SetBox(230, 80, 20); game.ForBeat(0.8f, () => { ShinyLine(new(320, BoxStates.Down), 0, Color.Magenta * 0.6f); });
                                }
                            }));
                        }));
                    }

                    AddInstance(new InstantEvent(game.BeatTime(56), () => { SetSoul(1); TP(); SetGreenBox(); }));
                }
                public static void Main1B()
                {
                    float time = game.BeatTime(8) + 0.5f;
                    string[] rhythm = {
                    "$0", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "R", "+0", "+0", "+0", "+0", "/", "/", "R",
                    "(+0)", "/", "/", "/", "(R)", "/", "/", "R",
                    "(+0)", "/", "/", "/", "(R)", "/", "/", "R",
                    "(+0)", "/", "/", "/", "", "/", "/", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, 6f, rhythm[i]);
                        if (i % 8 == 0 && i < 32)
                            game.CreateArrows(time, 6f, "+0");
                        if (i == 52) CreateGB(new GreenSoulGB(time, "R", 0, game.BeatTime(8)));
                        time += game.BeatTime(1);
                    }
                }
                #region The Hall
                public static void Hall0A0()
                {
                    int h = 0;
                    AddInstance(new InstantEvent(game.BeatTime(7.5f), () =>
                    {
                        SetBox(320 - 82, 320 + 82, 240 - 42, 490);
                        SetSoul(2);
                        HeartAttribute.Gravity = 0;
                        Heart.GiveForce(0, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(8), 95, () =>
                    {
                        TP(320, 240 - h * h * 0.0105f);
                        SetBox(320 - 82, 320 + 82, 240 - 42 - h * h * 0.45f, 490);
                        h++;
                    }));
                    float speed = 9.8f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(6.5f), game.BeatTime(6.5f), () =>
                    {
                        speed *= 0.934f;
                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied + new Vector2(speed, 0);
                        rect.Width -= 0.23f;
                        ScreenDrawing.HPBar.AreaOccupied = rect;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(128 - 8), () =>
                    {
                        SetBox(320 - 82, 320 + 82, -20, 240 + 120);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(128 - 4), () =>
                    {
                        Heart.GiveForce(0, 10);
                    }));
                }
                public static void Hall0A1()
                {
                    int a = 0;
                    LeftBone b1 = new(true, 4, 90) { MarkScore = false };
                    RightBone b2 = new(true, 4, 90);
                    CreateBone(b1);
                    CreateBone(b2);
                    AddInstance(new TimeRangedEvent(20, () =>
                    {
                        b1.MissionLength -= 4;
                        b2.MissionLength -= 4;
                    }));
                    AddInstance(new TimeRangedEvent(105, 15, () =>
                    {
                        b1.MissionLength += a * 1.5f;
                        b2.MissionLength += a * 1.5f;
                        a++;
                    }));
                }
                public static void Hall0A2()
                {
                    float sin = 0;
                    float b = 0;
                    AddInstance(new TimeRangedEvent(0, 40, () =>
                    {
                        sin = Sin(b * 2.5f) * 85;
                        b += 2;
                    }));
                    float sin1 = 0;
                    float b1 = 0;
                    AddInstance(new TimeRangedEvent(40, 40, () =>
                    {
                        sin1 = Sin(b1 * 2.5f) * 85;
                        b1 += 2;
                    }));
                    float sin2 = 0;
                    float b2 = 0;
                    AddInstance(new TimeRangedEvent(80, 40, () =>
                    {
                        sin2 = Sin(b2 * 2.5f) * 85;
                        b2 += 2;
                    }));
                    float sin3 = 0;
                    float b3 = 0;
                    AddInstance(new TimeRangedEvent(120, 40, () =>
                    {
                        sin3 = Sin(b3 * 2.5f) * 85;
                        b3 += 2;
                    }));
                    for (int a = 0; a < 20; a++)
                    {
                        AddInstance(new TimeRangedEvent(a * 2, 1, () =>
                        {
                            CreateBone(new LeftBone(true, 5.25f, 20 + sin) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40, 1, () =>
                        {
                            CreateBone(new RightBone(true, 5.25f, 20 + sin1) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40 * 2, 1, () =>
                        {
                            CreateBone(new LeftBone(true, 5.25f, 20 + sin2) { MarkScore = a % 2 == 0 });
                        }));
                        AddInstance(new TimeRangedEvent(a * 2 + 40 * 3, 1, () =>
                        {
                            CreateBone(new RightBone(true, 5.25f, 20 + sin3) { MarkScore = a % 2 == 0 });
                        }));
                    }
                }
                public static void Hall0A3()
                {
                    int k = 0;
                    AddInstance(new TimeRangedEvent(game.BeatTime(24), game.BeatTime(128 - 48), () =>
                    {
                        k++;
                        if (k % 8 == 0)
                        {
                            UpForwardLine();
                        }
                    }));
                }
                public static void Hall0A4()
                {
                    DIY_Up_DownBoneWall(320 - 30, 0, 60, 480, 60, 40);
                    AddInstance(new TimeRangedEvent(120, 1, () =>
                    {
                        DIY_Up_DownBoneWall(320 - 90, 480, 60, 480, 60, 40);
                        DIY_Up_DownBoneWall(320 + 30, 480, 60, 480, 60, 40);
                    }));
                }
                public static void Hall0A5()
                {

                }
                public static void Hall0A6()
                {
                    CreateEntity(new Boneslab(0, 20, 10, 20));
                    CreateBone(new DownBone(true, 5, 120) { ColorType = 2 });
                    CreateBone(new DownBone(false, 5, 120) { ColorType = 2 });
                }
                public static void Hall0A7()
                {

                }
                public static void Hall0A8()
                {

                }
                #endregion

                public static void Main2A()
                {
                    float speed1 = 9.8f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(6.5f), game.BeatTime(6.5f), () =>
                    {
                        speed1 *= 0.934f;
                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied - new Vector2(speed1, 0);
                        rect.Width += 0.23f;
                        ScreenDrawing.HPBar.AreaOccupied = rect;
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(5), () =>
                    {
                        SetGreenBox();
                        TP();
                        SetSoul(1);
                    }));
                    float time = game.BeatTime(8) + 1;
                    string[] rhythm = {
                    "$0", "/", "+0", "+0", "+0", "/", "+0", "/",
                    "+0", "+0", "+0", "/", "+0", "/", "+0", "+0",
                    "+0", "/", "+0", "/", "+0", "+0", "+0", "/",
                    "$0", "$0", "$0", "/", "$2", "/", "+0", "/",
                    "+0", "/", "+0", "+0", "+0", "/", "+0", "/",
                    "+0", "+0", "+0", "/", "+0", "/", "+0", "+0",
                    "+0", "/", "+0", "/", "+0", "+0", "+0", "/",
                    "$2", "($2)", "$2", "/", "+0", "/", "+0", "/",
                };
                    Action A = () =>
                    {
                        Line line1 = new(new(80, 0), BoxStates.CurrentBox.CollidingBox.TopLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new(560, 0), BoxStates.CurrentBox.CollidingBox.TopRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new(80, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new(560, 480), BoxStates.CurrentBox.CollidingBox.BottomRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action B = () =>
                    {
                        Line line1 = new(new Vector2(120, 0), new Vector2(120, 480)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new Vector2(520, 0), new Vector2(520, 480)) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action C = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(370 + i * 25, 0), new Vector2(170 + i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action D = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(270 - i * 25, 0), new Vector2(470 - i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 6f;
                        if (i % 32 >= 24 && i % 32 <= 26) speed += 3;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);

                        if (i % 32 < 27 && i % 32 > 23)
                            game.DelayBeat(8 + i - 0.2f, C);
                        else if (i % 32 < 31 && i % 32 > 27)
                            game.DelayBeat(8 + i - 0.2f, D);
                        else if (i % 8 == 4)
                        {
                            game.DelayBeat(8 + i - 0.2f, A);
                        }
                        else if (i % 4 == 0)
                        {
                            game.DelayBeat(8 + i - 0.2f, B);
                        }
                    }
                }
                public static void Main2B()
                {
                    float time = game.BeatTime(8) + 1;
                    string[] rhythm = {
                    "R", "/", "+0", "+0", "+0", "/", "+0", "/",
                    "+0", "+0", "+0", "/", "+0", "/", "+0", "+0",
                    "+0", "/", "+0", "/", "+0", "+0", "+0", "/",
                    "+0", "+0", "+0", "/", "+2", "/", "+0", "/",
                    "+0", "/", "+0", "+0", "+0", "/", "+0", "/",
                    "+0", "+0", "+0", "/", "+0", "/", "+0", "+0",
                    "(R)", "/", "/", "(R)", "/", "/", "(R)", "/",
                    "/", "(R)", "/", "/", "R", "/", "R", "/",
                };
                    Action A = () =>
                    {
                        Line line1 = new(new(80, 0), BoxStates.CurrentBox.CollidingBox.TopLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new(560, 0), BoxStates.CurrentBox.CollidingBox.TopRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new(80, 480), BoxStates.CurrentBox.CollidingBox.BottomLeft) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new(560, 480), BoxStates.CurrentBox.CollidingBox.BottomRight) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action B = () =>
                    {
                        Line line1 = new(new Vector2(0, 50), new Vector2(640, 50)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line3 = new(new Vector2(0, 70), new Vector2(640, 70)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line2 = new(new Vector2(0, 410), new Vector2(640, 410)) { DrawingColor = Color.Magenta * 0.4f };
                        Line line4 = new(new Vector2(0, 430), new Vector2(640, 430)) { DrawingColor = Color.Magenta * 0.4f };
                        Line[] lines = { line1, line2, line3, line4 };
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(3), () => line.Alpha -= 1 / game.BeatTime(3));
                            game.DelayBeat(game.BeatTime(3), () => line.Dispose());
                        }
                    };
                    Action C = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(370 + i * 25, 0), new Vector2(170 + i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action D = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 3; i++)
                            lines.Add(new(new Vector2(270 - i * 25, 0), new Vector2(470 - i * 25, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action E = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 2; i++)
                            lines.Add(new(new Vector2(220 - i * 45, 0), new Vector2(520 - i * 45, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };
                    Action F = () =>
                    {
                        List<Line> lines = new();
                        for (int i = 0; i < 2; i++)
                            lines.Add(new(new Vector2(420 + i * 45, 0), new Vector2(140 + i * 45, 480)) { DrawingColor = Color.Magenta * 0.4f });
                        foreach (Line line in lines)
                        {
                            CreateEntity(line);
                            game.ForBeat(game.BeatTime(2), () => line.Alpha -= 1 / game.BeatTime(2));
                            game.DelayBeat(game.BeatTime(2), () => line.Dispose());
                        }
                    };

                    game.RegisterFunction("E", E);
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 6f;
                        if (i >= 24 && i <= 26) speed += 3;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);

                        if (i < 27 && i > 23)
                            game.DelayBeat(8 + i - 0.2f, D);
                        else if (i < 31 && i > 27)
                            game.DelayBeat(8 + i - 0.2f, C);
                        else if (i % 8 == 4 && i < 48)
                        {
                            game.DelayBeat(8 + i - 0.2f, A);
                        }
                        if (i % 4 == 0 && i < 48)
                        {
                            game.DelayBeat(8 + i - 0.2f, B);
                        }
                    }
                }

                public static void Rest0A()
                {
                    float speed = 0.0f;
                    for (int i = -1; i <= 1; i++)
                    {
                        float delta = 320;
                        DrawingUtil.NormalLine line = new(320 + i * 12, -150, 320 + i * 12, BoxStates.Up - 320, (int)game.BeatTime(8), 1.0f)
                        {
                            color = Color.Silver * 0.7f,
                        };
                        CreateEntity(line);
                        game.ForBeat(8, () =>
                        {
                            delta *= 0.9f;
                            if (delta > 5) delta -= 4.5f;
                            line.y2 = BoxStates.Up - delta - speed - 2;
                        });
                    }
                    game.ForBeat(2.5f, 6.5f, () =>
                    {
                        speed = speed * 0.9f + 12 * 0.1f;
                        InstantSetBox(BoxStates.Centre.Y - speed, BoxStates.Width, BoxStates.Height);
                        InstantTP(BoxStates.Centre + new Vector2(0, 10));
                    });
                    game.DelayBeat(9.1f, () =>
                    {
                        InstantSetBox(540, 100, 130);
                        SetBox(510, 100, 130);
                        BoxStates.BoxMovingScale = 0.07f;
                        SetSoul(0);

                        CollideRect rect = ScreenDrawing.HPBar.AreaOccupied;
                        rect.TopLeft += new Vector2(120, 0);
                        ScreenDrawing.HPBar.AreaOccupied = rect;

                        //creating bone:
                        bool direction = false;
                        int count = 0;
                        for (int y = 12; y <= 440; y += Rand(54, 74))
                        {
                            if (Rand(0, 1.0f) < 0.7f) direction = !direction;
                            else y -= Rand(9, 12);
                            float length = Rand(52, 75) + 20;
                            float distance = (direction ? -1 : 1) * (length / 2 + Rand(-6, 4));
                            float curY = y;
                            bool tag = false;
                            float speed = direction ? -1 : 1;
                            CreateBone(new CustomBone(new(320 + distance, y),
                                (t) =>
                                {
                                    if (Heart.Centre.Y <= curY - 19) tag = true;
                                    if (tag)
                                    {
                                        speed *= 1.1f;
                                        return t.CentrePosition + new Vector2(speed, 0);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, length
                            ));
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                        for (int y = 12; y <= 440; y += Rand(108, 168))
                        {
                            float curY = y;
                            bool tag = false;
                            float speed = 1.0f;
                            CreateBone(new CustomBone(new(320, y),
                                (t) =>
                                {
                                    if (t.CentrePosition.Y < -1000) (t as Entity).Dispose();
                                    if (Heart.Centre.Y <= curY - 3) tag = true;
                                    if (tag)
                                    {
                                        speed += 0.1f;
                                        return t.CentrePosition + new Vector2(0, speed);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, 125
                            )
                            { ColorType = 2 });
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                        while (count < 25)
                        {
                            direction = RandBool();
                            float y = Rand(50, 440);
                            float length = Rand(52, 75) + 20;
                            float distance = (direction ? -1 : 1) * (length / 2 + Rand(26, 54));
                            float curY = y;
                            bool tag = false;
                            float speed = direction ? -1 : 1;
                            CreateBone(new CustomBone(new(320 + distance, y),
                                (t) =>
                                {
                                    if (Heart.Centre.Y <= curY - 19) tag = true;
                                    if (tag)
                                    {
                                        speed *= 1.1f;
                                        return t.CentrePosition + new Vector2(speed, 0);
                                    }
                                    return new(0, 0);
                                },
                                Rand(-12, 12) + 90, length
                            ));
                            game.DelayBeat(52, () => tag = true);
                            count++;
                        }
                    });
                    for (int i = 1; i < 7; i++)
                    {
                        int t = i;
                        game.DelayBeat(i * 8 + 8, () =>
                        {
                            SetBox(new Vector2(320 + (t % 2 == 0 ? 9 : -9), 510 - t * 70), 100, 130);
                        });
                    }
                    game.DelayBeat(63.8f, () =>
                    {
                        game.ForBeat(8, () =>
                        {
                            CollideRect rect = ScreenDrawing.HPBar.AreaOccupied;
                            rect.TopLeft = rect.TopLeft * 0.9f + new Vector2(320, 455 - 12) * 0.1f;
                            ScreenDrawing.HPBar.AreaOccupied = rect;
                        });
                        SetBox(new Vector2(320, 240), 180, 180);
                    });
                    game.DelayBeat(5, () =>
                    {
                        DrawingUtil.BlackScreen(game.BeatTime(4), 1, game.BeatTime(2));
                    });
                }
                public static void Rest0B()
                {
                    Action makeGB = () =>
                    {
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(150, Rand(0, 359)), Heart.Centre, new(1, 0.5f), game.BeatTime(8.3f), game.BeatTime(5)));
                    };
                    for (int i = 0; i < 2; i++)
                        game.DelayBeat(8 + i * 16, makeGB);
                    game.DelayBeat(8 + 5 * 8, makeGB);
                    game.DelayBeat(8 + 4 * 8, () =>
                    {
                        CreateGB(new NormalGB(Heart.Centre + MathUtil.GetVector2(150, Rand(0, 359)), Heart.Centre, new(1, 0.5f), game.BeatTime(16.3f), game.BeatTime(5)));
                    });
                    Action makeBone = () =>
                    {
                        bool dir = RandBool();
                        int signal = dir ? 1 : -1;
                        Vector2 centre = new(320 + signal * Rand(0, 120), BoxStates.Down + 20);
                        Vector2 speed = new Vector2(-signal * Rand(1.5f, 4.0f), Rand(-7f, -4f)) * 0.17f;
                    };
                    for (int i = 0; i < 12; i++)
                        game.DelayBeat(8 + i * 4, makeBone);
                    game.DelayBeat(64, () =>
                    {
                        TP();
                        SetGreenBox();
                        SetSoul(1);
                    });
                }
                public static void Rest1A()
                {
                    float time = game.BeatTime(8) + 0.25f;
                    string[] rhythm = {
                    "D", "", "", "", "D", "", "", "",
                    "(D)", "", "", "", "", "", "", "",
                    "D", "", "", "", "D", "", "", "",
                    "(D)", "", "", "", "", "", "", "",
                    "(D)", "", "", "", "", "", "", "",
                    "(D)", "", "", "", "", "", "", "",
                    "D", "", "", "", "D", "", "", "",
                    "(D)", "", "", "", "", "", "", "",
                };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        float speed = 5f;
                        if (rhythm[i] != "/")
                            game.CreateArrows(time, speed, rhythm[i]);
                        time += game.BeatTime(1);
                    }
                }
                public static void Rest1B()
                {
                    bool soundPlayed = false;
                    Func<float, float, Action> createBone = (t, len) =>
                    {
                        return () =>
                        {
                            float start = 1 + MathF.Sqrt(t);
                            DownBone bone1 = new(false, start, len);
                            DownBone bone2 = new(true, start, len);
                            UpBone bone3 = new(false, start, len);
                            UpBone bone4 = new(true, start, len);
                            SideBone[] bones = { bone1, bone2, bone3, bone4 };

                            foreach (SideBone bone in bones) CreateBone(bone);

                            if (!soundPlayed)
                                PlaySound(Sounds.spearAppear);
                            soundPlayed = true;

                            game.ForBeat(8 - 0.2f, () =>
                            {
                                foreach (SideBone bone in bones)
                                {
                                    bone.Speed *= (0.7f + t * 0.029f);
                                }
                            });
                            game.DelayBeat(8, () =>
                            {
                                if (!soundPlayed)
                                    PlaySound(Sounds.pierce);
                                soundPlayed = true;

                                foreach (SideBone bone in bones)
                                {
                                    bone.Speed = t;
                                }
                            });
                        };
                    };
                    game.ForBeat(72, () => soundPlayed = false);
                    game.DelayBeat(8 - 1, () =>
                    {
                        SetSoul(2);
                        HeartAttribute.Gravity = 5.2f;
                        HeartAttribute.JumpTimeLimit = 2;
                        Heart.GiveForce(0, 5);
                        SetBox(300, 84, 84);

                        game.DelayBeat(0, createBone(4, 20));
                    });
                    game.DelayBeat(8 + 16 - 1, () =>
                    {
                        Heart.GiveForce(180, 6);
                        SetBox(220, 100, 100);
                        game.DelayBeat(0, createBone(5, 22));
                        game.DelayBeat(0, createBone(4, 30));
                    });
                    game.DelayBeat(8 + 32 - 1, () =>
                    {
                        Heart.GiveForce(0, 7);
                        SetBox(310, 120, 120);
                        game.DelayBeat(0, createBone(6, 22));
                        game.DelayBeat(0, createBone(5, 30));
                        game.DelayBeat(0, createBone(4, 38));
                    });
                    game.DelayBeat(8 + 48 - 1, () =>
                    {
                        SetBox(310, 180, 120);
                        CreateGB(new NormalGB(new(100, 285), new(320, 0), new(1, 1), 0, game.BeatTime(8), game.BeatTime(8)));
                        CreateGB(new NormalGB(new(540, 285), new(320, 0), new(1, 1), 180, game.BeatTime(8), game.BeatTime(8)));

                        game.DelayBeat(8, () =>
                        {
                            for (int i = 0; i < 6; i++) game.DelayBeat(i * 0.667f, () => ScreenDrawing.CameraEffect.Convulse(11 - i, 5, i % 2 == 0));
                            // game.DelayBeat(5, () => ScreenDrawing.CameraEffect.Rotate180(game.BeatTime(16)));
                            ScreenDrawing.CameraEffect.SizeExpand(11, game.BeatTime(4));
                        });
                    });
                    game.DelayBeat(8 + 63.5f, () =>
                    {
                        SetSoul(1);
                        game.ForBeat(0.5f + 128, () => TP(BoxStates.Centre));
                        BoxStates.BoxMovingScale = 0.04f;
                        SetGreenBox();
                    });
                }
            }
            void ExPartF1()
            {
                RegisterFunctionOnce("qwq", () =>
                {

                });
                BarrageCreate(BeatTime(24) + 3.5f, BeatTime(8), 6.5f, new string[]
                {
                    "$0", "/", "R", "+0", "+0", "/", "R1", "/",
                    "R", "+0", "+0", "/", "R", "/", "R1", "+01",
                    "+01", "/", "R", "/", "R", "+0", "+0", "/",
                    "(R)(R1)", "", "(R)(R1)", "/", "R", "/", "R", "+0",
                    "+0", "/", "R1", "", "R", "+0", "+0", "/",
                    "R", "", "R1", "+01", "+01", "/", "R", "",
                    "R", "+0", "+0", "/", "(R)(R1)", "", "(R)(R1)", "/",

                    "R", "/", "R", "+0", "+0", "/", "R1", "/",
                    "R", "+0", "+0", "/", "R", "/", "R1", "+01",
                    "+01", "/", "R", "/", "R", "+0", "+0", "/",
                    "(R)(R1)", "", "(R)(R1)", "/", "R", "/", "R", "+0",
                    "($0'1.5)($2'1.5)", "/", "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)", "", "(R1)", "/",
                    "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)", "", "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)", "($01'1.5)($21'1.5)", "",
                    "($01'1.5)($21'1.5)", "", "", "", "", "", "", "",
                //
                "R", "/", "R", "+0", "+0", "/", "R1", "/",
                    "R", "+0", "+0", "/", "R", "/", "R1", "+01",
                    "+01", "/", "R", "/", "R", "+0", "+0", "/",
                    "(R)(R1)", "", "(R)(R1)", "/", "R", "/", "R", "+0",
                    "+0", "/", "R1", "", "R", "+0", "+0", "/",
                    "R", "", "R1", "+01", "+01", "/", "R", "",
                    "R", "+0", "+0", "/", "(R)(R1)", "", "(R)(R1)", "/",

                "R", "/", "R", "+0", "+0", "/", "R1", "/",
                    "R", "+0", "+0", "/", "R", "/", "R1", "+01",
                    "+01", "/", "R", "/", "R", "+0", "+0", "/",
                    "(R)(R1)", "", "(R)(R1)", "/", "R", "/", "+0", "",
                    "+0", "/", "+0", "", "+0", "", "+0", "/",
                    "+0", "", "+0", "", "+0", "", "+0", "",
                    "(D)(+01)", "", "", "",   "(D)(+01)", "", "", "",
                    "($0'1.5)($2'1.5)","($0'1.5)($2'1.5)","($0'1.5)($2'1.5)",
                });

            }
            void ExPartF2()
            {
                BarrageCreate(BeatTime(24) + 8, BeatTime(8), 6.5f, new string[]
                {
                "", "","","", "", "(D)(+01)", "", "",
                "(D)(+01)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                "", "(D)(+01)", "","", "(D)(+01)", "", "", "($0'1.5)($2'1.5)",
                "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)", "",  "(D)(+01)", "", "", "(D)(+01)",
                "", "", "(D)(+01)","", "", "(D)(+01)", "", "",
                "(D)(+01)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                "", "(D)(+01)", "","", "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)","",

                "(D)(+01)","", "", "(D)(+01)", "", "",
                "(D)(+01)", "", "", "(D)(+01)", "", "", "(D)(+01)", "",
                "", "(D)(+01)", "","", "(D)(+01)", "", "", "(D)(+01)",
                "", "", "(D)(+01)", "",  "($0'1.5)($2'1.5)", "($0'1.5)($2'1.5)","",
                "!!8/16","$3'1.5","+0'1.5","+0'1.5","+0'1.5",   "+0'1.5","+0'1.5","+0'1.5","+0'1.5",
                "+0'1.5","+0'1.5","+0'1.5","+0'1.5",   "+0'1.5","+0'1.5","+0'1.5","+0'1.5",
                "!!6/6","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","","","",

                });
            }
            void ExPartF3()
            {

                BarrageCreate(BeatTime(8) + 5.5f, BeatTime(8), 6.5f, new string[]
                {
                    "($01)(+01)", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "(R1)(+01)", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "(R1)(+01)", "+21", "+21", "+21", "+21", "+21", "/", "/",
                    "(R1)(+01)", "+21", "+21", "+21", "+2", "/", "/", "R",
                    "R(R1)", "/", "/", "/", "R(R1)", "/", "/", "R",
                    "R(R1)", "/", "/", "/", "R(R1)", "/", "/", "R",
                    "R(R1)", "/", "/", "", "R(R1)", "/", "/", "R",
                    "R(R1)", "/", "/", "", "R(R1)", "/", "/", "R",
                    "R(R1)","/", "/", "R","(#44#R)(#44#D1)"
                    });


            }
            void NorPartF1()
            {
                RegisterFunctionOnce("qwq", () =>
                {

                });
                BarrageCreate(BeatTime(24) + 3.5f, BeatTime(8), 6.5f, new string[]
                {
                    "$0", "/", "R", "+0", "+0", "/", "R", "/",
                    "R", "+0", "+0", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "/", "R", "+0", "+0", "/",
                    "(R)(+01)", "", "(R)(+01)", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "", "R", "+0", "+0", "/",
                    "R", "", "R", "+0", "+0", "/", "R", "",
                    "R", "+0", "+0", "/", "(R)(+01)", "", "(R)(+01)", "/",

                    "R", "/", "R", "+0", "+0", "/", "R", "/",
                    "R", "+0", "+0", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "/", "R", "+0", "+0", "/",
                    "(R)(+01)", "", "(R)(+01)", "/", "R", "/", "R", "+0",
                    "($0'1.5)", "/", "(+0'1.5)(+0'1.5)", "(+20'1.5)(+0'1.5)", "(+20'1.5)(+0'1.5)", "", "($2'1.5)($2'1.5)", "/",
                    "(+0'1.5)(+0'1.5)", "(+2'1.5)(+0'1.5)", "(+2'1.5)(+0'1.5)", "", "(+20'1.5)(+0'1.5)", "(+20'1.5)(+0'1.5)", "(+20'1.5)(+0'1.5)", "",
                    "(+20'1.5)(+0'1.5)", "", "", "", "", "", "", "",
                //
                "R", "/", "R", "+0", "+0", "/", "R", "/",
                    "R", "+0", "+0", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "/", "R", "+0", "+0", "/",
                    "(R)(+01)", "", "(R)(+01)", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "", "R", "+0", "+0", "/",
                    "R", "", "R", "+0", "+0", "/", "R", "",
                    "R", "+0", "+0", "/", "(R)(+01)", "", "(R)(+01)", "/",

                "R", "/", "R", "+0", "+0", "/", "R", "/",
                    "R", "+0", "+0", "/", "R", "/", "R", "+0",
                    "+0", "/", "R", "/", "R", "+0", "+0", "/",
                    "(R)(+01)", "", "(R)(+01)", "/", "R", "/", "+0", "",
                    "+0", "/", "+0", "", "+0", "", "+0", "/",
                    "+0", "", "+0", "", "+0", "", "+0", "",
                    "(D)(+01)", "", "", "",   "(D)(+01)", "", "", "",
                    "($0'1.5)($2'1.5)","(+21'1.5)($21'1.5)","(+20'1.5)($2'1.5)",
                });

            }
            void NorPartF2()
            {
                BarrageCreate(BeatTime(24) + 8, BeatTime(8), 6.5f, new string[]
                {
                "", "","","", "", "(R)(+01)", "", "",
                "(R)(+01)", "", "", "(R)(+01)", "", "", "(R)(+01)", "",
                "", "(R)(+01)", "","", "(R)(+01)", "", "", "($0'1.5)($0'1.5)",
                "($0'1.5)($0'1.5)", "($0'1.5)($0'1.5)", "",  "(R)(+01)", "", "", "(R)(+01)",
                "", "", "(R)(+01)","", "", "(R)(+01)", "", "",
                "(R)(+01)", "", "", "(R)(+01)", "", "", "(R)(+01)", "",
                "", "(R)(+01)", "","", "($2'1.5)($2'1.5)", "($2'1.5)($2'1.5)", "($2'1.5)($2'1.5)","",

                "(R)(+01)","", "", "(R)(+01)", "", "",
                "(R)(+01)", "", "", "(R)(+01)", "", "", "(R)(+01)", "",
                "", "(R)(+01)", "","", "(R)(+01)", "", "", "(R)(+01)",
                "", "", "(R)(+01)", "",  "($0'1.5)($0'1.5)", "($0'1.5)($0'1.5)","",
                "!!8/16","$3'1.5","+0'1.5","+0'1.5","+0'1.5",   "+0'1.5","+0'1.5","+0'1.5","+0'1.5",
                "+0'1.5","+0'1.5","+0'1.5","+0'1.5",   "+0'1.5","+0'1.5","+0'1.5","+0'1.5",
                "!!6/6","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","","","",

                });
            }
            void NorPartF3()
            {

                BarrageCreate(BeatTime(8) + 5.5f, BeatTime(8), 6.5f, new string[]
                {
                    "($0)(+0)", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "(R)(+0)", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "(R)(+0)", "+2", "+2", "+2", "+2", "+2", "/", "/",
                    "(R)(+0)", "+2", "+2", "+2", "+2", "/", "/", "R",
                    "(+0)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "R",
                    "(+0)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "R",
                    "(+0)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "R",
                    "(+0)(+01)", "/", "/", "/", "(R)(+01)", "/", "/", "R",
                    "(+0)(+01)","/", "/", "R1","(#44#R)"
                    });


            }
            void NoobPartF1()
            {
                RegisterFunctionOnce("qwq", () =>
                {

                });
                BarrageCreate(BeatTime(24) + 3.5f, BeatTime(8), 6f, new string[]
                {
                    "$0", "/", "+0", "+0", "+0", "/", "+0", "/",
                    "+0", "+0", "+0", "/", "+0", "/", "+0", "+0",
                    "+0", "/", "+0", "/", "+0", "+0", "+0", "/",
                    "(D)", "", "(D)", "/", "D", "/", "+0", "+0",
                    "+0", "/", "+0", "", "+0", "+0", "+0", "/",
                    "+0", "", "+0", "+0", "+0", "/", "+0", "",
                    "+0", "+0", "+0", "/", "(D)", "", "(D)", "/",

                    "D", "/", "+0", "+0", "+0", "/", "+0", "/",
                    "+0", "+0", "+0", "/", "+0", "/", "+0", "+0",
                    "+0", "/", "+0", "/", "+0", "+0", "+0", "/",
                    "(D)", "", "(D)", "/", "(D)", "/", "(D)", "+0",
                    "($0'1.5)", "/", "($0'1.5)", "($0'1.5)", "($0'1.5)", "", "($0'1.5)", "/",
                    "($0'1.5)", "($0'1.5)", "($0'1.5)", "", "($0'1.5)", "($0'1.5)", "($0'1.5)", "",
                    "($0'1.5)", "", "", "", "", "", "", "",
                //
                "R", "/", "+0", "+0", "+0", "/", "+0", "/",
                    "+0", "+0", "+0", "/", "+0", "/", "+0", "+0",
                    "+0", "/", "+0", "/", "+0", "+0", "+0", "/",
                    "(D)", "", "(D)", "/", "D", "/", "D", "+0",
                    "+0", "/", "+0", "", "+0", "+0", "+0", "/",
                    "+0", "", "+0", "+0", "+0", "/", "+0", "",
                    "+0", "+0", "+0", "/", "(D)", "", "(D)", "/",

                "D", "/", "D", "+0", "+0", "/", "+0", "/",
                    "+0", "+0", "+0", "/", "+0", "/", "+0", "+0",
                    "+0", "/", "+0", "/", "+0", "+0", "+0", "/",
                    "(D)", "", "(D)", "/", "+0", "/", "+0", "",
                    "+0", "/", "+0", "", "+0", "", "+0", "/",
                    "+0", "", "+0", "", "+0", "", "+0", "",
                    "(D)", "", "", "",   "(D)", "", "", "",
                    "($0'1.5)","($0'1.5)","($0'1.5)",
                });

            }
            void NoobPartF2()
            {
                BarrageCreate(BeatTime(24) + 8, BeatTime(8), 6.5f, new string[]
                {
                "", "","","", "", "(R)", "", "",
                "(R)", "", "", "(R)", "", "", "(R)", "",
                "", "(R)", "","", "(R)", "", "", "($0'1.5)",
                "($0'1.5)", "($0'1.5)", "",  "(R)", "", "", "(R)",
                "", "", "(R)","", "", "(R)", "", "",
                "(R)", "", "", "(R)", "", "", "(R)", "",
                "", "(R)", "","", "($0'1.5)", "($0'1.5)", "($0'1.5)","",

                "(R)","", "", "(R)", "", "",
                "(R)", "", "", "(R)", "", "", "(R)", "",
                "", "(R)", "","", "(R)", "", "", "(R)",
                "", "", "(R)", "",  "($0'1.5)", "($0'1.5)","",
                "!!8/16","$3'1.5","+0'1.5","+0'1.5","+0'1.5",   "+0'1.5","+0'1.5","+0'1.5","+0'1.5",
                "+0'1.5","+0'1.5","+0'1.5","+0'1.5",   "+0'1.5","+0'1.5","+0'1.5","+0'1.5",
                "!!6/6","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","+0'1.5","","","",

                });
            }
            void NoobPartF3()
            {

                BarrageCreate(BeatTime(8) + 5.5f, BeatTime(8), 6.5f, new string[]
                {
                    "($0)", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "(R)", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "(R)", "+0", "+0", "+0", "+0", "+0", "/", "/",
                    "(R)", "+0", "+0", "+0", "+0", "/", "/", "",
                    "R", "/", "/", "/", "R", "/", "/", "",
                    "R", "/", "/", "/", "R", "/", "/", "",
                    "R", "/", "/", "", "R", "/", "/", "",
                    "R", "/", "/", "", "R", "/", "/", "",
                    "R","/", "/", "","(#44#R)"
                    });


            }
            public void Extreme()
            {
                if (Gametime < 0) return;
                if (InBeat(0)) ExBarrage.Intro0();
                if (InBeat(64)) ExBarrage.Intro1();

                if (InBeat(128)) ExBarrage.Main0A();
                if (InBeat(192)) ExBarrage.Main0B();
                if (InBeat(256)) ExBarrage.Main1A();
                if (InBeat(320)) ExBarrage.Main1B();

                if (InBeat(384)) ExBarrage.Hall0A0();
                if (InBeat(384)) ExBarrage.Hall0A3();
                if (InBeat(384 + 48)) ExBarrage.Hall0A4();
                if (InBeat(384 + 16)) ExBarrage.Hall0A2();
                for (int i = 0; i < 110; i++)
                {
                    if (InBeat(384 + 13 + i)) ExBarrage.Hall0A1();
                }
                for (int i = 0; i < 6; i++)
                {
                    if (InBeat(384 + 84 + i * 4)) ExBarrage.Hall0A5();
                }
                if (InBeat(384 + 127.5f)) ExBarrage.Hall0A6();

                if (InBeat(384 + 128)) ExBarrage.Main2A();
                if (InBeat(384 + 192)) ExBarrage.Main2B();
                if (InBeat(640)) ExBarrage.Rest0A();
                if (InBeat(640 + 64)) ExBarrage.Rest0B();
                if (InBeat(640 + 128)) ExBarrage.Rest1A();
                if (InBeat(640 + 192)) ExBarrage.Rest1B();
                if (InBeat(640 + 192)) ExBarrage.Rest1B();
                if (InBeat(640 + 192 + 48)) ExPartF1();
                if (InBeat(640 + 192 + 56 * 4 + 48)) ExPartF2();

                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 - 4)) ExBarrage.Main0A();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 - 4)) ExBarrage.Main0B();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 + 64 - 4)) ExBarrage.Main1A();
                if (InBeat(640 + 192 + 56 * 4 + 48 + 8 * 17 + 64 + 64 + 64 - 4)) ExPartF3();
            }

            public void Start()
            {

                game = this;
                HeartAttribute.MaxHP = 6;
                HeartAttribute.Speed = 3.26f;
                HeartAttribute.SoftFalling = true;
                TP();
                //SetSoul(1);
                InstantSetBox(240, 85, 85);
                GametimeDelta = BeatTime(10) * -1 - 0.6f;
                //GametimeDelta = BeatTime(640 + 192 + 56 * 4 + 48) + BeatTime(10) * -1 - 4.6f;
                //PlayOffset = BeatTime(640 + 192 + 56 * 4 + 48);
                //GlobalResources.Effects.StepSampleShader step = FightResources.Shaders.StepSample;
                //ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(step, 0.98f));
                //step.Intensity = 0.5f;
                AddInstance(new TimeRangedEvent(100000, () =>
                {
                    ScreenDrawing.ThemeColor = ScreenDrawing.UIColor = Color.Lerp(Color.White, Color.Magenta, MathF.Max(0, AdvanceFunctions.Sin01(0.08f + GametimeF / BeatTime(2)) * 0.1f));
                }));
                HeartAttribute.ArrowFixed = true;
                //   GametimeDetla = 4300;

                //   GametimeDelta = BeatTime(1599);
                // SetSoul(0); 
            }
        }
    }
}