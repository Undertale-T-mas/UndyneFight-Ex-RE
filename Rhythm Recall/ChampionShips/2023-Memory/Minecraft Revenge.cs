using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.FightResources;
using Color = Microsoft.Xna.Framework.Color;

namespace Rhythm_Recall.Waves
{
    public class MinecraftRevenge : IChampionShip
    {
        public MinecraftRevenge()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Noob);
            difficulties.Add("div.1", Difficulty.Hard);
            difficulties.Add("DIV.0", Difficulty.Extreme);
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
                public override string BarrageAuthor => "Tlottgodinf";
                public override string SongAuthor => "CaptainSparklez";
                public override string PaintAuthor => "CaptainSparklez";
                public override string AttributeAuthor => "Tlottgodinf";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 16.5f),
                            new(Difficulty.Hard, 13f),
                            new(Difficulty.Noob, 4f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 17.0f),
                            new(Difficulty.Hard, 13f),
                            new(Difficulty.Noob, 4f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 21.0f),
                            new(Difficulty.Hard, 19f),
                            new(Difficulty.Noob, 2.4f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();
            public string Music => "Minecraft Revenge";
            public string FightName => "Minecraft Revenge";
            public Game() : base(62.5f / (120/*bpm*/ / 60f)) { }
            public static Game instance;
            public static float bpm = 62.5f / (120/*bpm*/ / 60f);
            public class CreeperBoom : Entity
            {
                public CreeperBoom() { }
                public override void Draw()
                {

                }

                public override void Update()
                {
                    throw new System.NotImplementedException();
                }
            }

            public void Normal()
            {

            }
            /*RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
            SetBox(240, 240, 240);
            DelayBeat(1, () =>
                        {
                for (int a = 0; a < 18; a++) CreateBone(new SideCircleBone(a * 20, 2.5f, 50, BeatTime(30)));
            });
                        CentreEasing.EaseBuilder ce = new();
            ce.Insert(0, CentreEasing.Stable(0,90));
                        ce.Insert(BeatTime(16), CentreEasing.YSinWave(20, BeatTime(4), 0));
                        ce.Insert(BeatTime(2), CentreEasing.EaseOutCirc(new (0, 90),new (0,-90),BeatTime(2)));
                        ce.Insert(BeatTime(14), CentreEasing.YSinWave(20, BeatTime(4), 0.5f));
                        Platform platform = new(0, new(320, 240), ce.GetResult(), 0, 40);
            CreateEntity(platform);
            Platform platform2 = new(0, new(320, 240), ce.GetResult(), 180, 40);
            CreateEntity(platform2);
            DelayBeat(64, () => { platform.Dispose(); platform2.Dispose(); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {

            CreateBone(new DownBone(true, 6, 60));
        });
                    RegisterFunctionOnce("Bone2", () =>
                    {

            CreateBone(new UpBone(false, 6, 60));
        });
                    RegisterFunctionOnce("Bone3", () =>
                    {
            PlaySound(Sounds.pierce);
            CreateBone(new UpBone(true, 6, 60));
        });
                    RegisterFunctionOnce("Bone4", () =>
                    {
            PlaySound(Sounds.pierce);
            CreateBone(new DownBone(false, 6, 60));
        });
        */
            public void Extreme()
            {
                if (InBeat(0))
                {
                    RegisterFunctionOnce("Line", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(640, 0));
                        ce.Insert(BeatTime(16), CentreEasing.Linear(-2));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { DrawingColor = Microsoft.Xna.Framework.Color.DarkGreen };
                        CreateEntity(l);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(16), ValueEasing.Combine(ValueEasing.Stable(0.5f), ValueEasing.SinWave(1, BeatTime(2), -0.5f)));
                        ve.Run((s) => { l.Alpha = s; });
                        for (int a = 1; a < 16; a++) l.InsertRetention(new Line.RetentionEffect(BeatTime(2 * a)));

                    });
                    RegisterFunctionOnce("LineAnother", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(640 + BeatTime(1) * 2, 0));
                        ce.Insert(BeatTime(16), CentreEasing.Linear(-2));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { DrawingColor = Microsoft.Xna.Framework.Color.DarkGreen };
                        CreateEntity(l);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(16), ValueEasing.Combine(ValueEasing.Stable(0.2f), ValueEasing.SinWave(0.4f, BeatTime(2), -0.5f)));
                        ve.Run((s) => { l.Alpha = s; });
                        for (int a = 1; a < 8; a++) l.InsertRetention(new Line.RetentionEffect(BeatTime(2 * a)));

                    });
                    BarrageCreate(0, BeatTime(1), 4, new string[]
                    {
                        "($0)($01)(Line)(LineAnother)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",
                        //
                        
                    });
                    BarrageCreate(BeatTime(4) * 4, BeatTime(1), 6, new string[]
                    {
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                    BarrageCreate(BeatTime(4) * 4, BeatTime(1), 6, new string[]
                    {
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        //
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        //
                    });
                }
                if (InBeat(48))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(290, 240, 140);
                        DelayBeat(0.125f, () =>
                        {
                            for (int a = 0; a < 32 * 2 * 1.8f; a++)
                                CreateBone(new DownBone(true, 400 + 140 + 5 * a * BeatTime(0.125f), 5, 32) { MarkScore = false });
                        });
                    });
                    RegisterFunctionOnce("Platform1", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 + 155, 320));
                        ce.Insert(BeatTime(8), CentreEasing.Linear(-3));
                        CreateEntity(new Platform(1, new(0, 0), ce.GetResult(), 0, 40));
                    });
                    RegisterFunctionOnce("Platform2", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 + 164, 320 - 40));
                        ce.Insert(BeatTime(8), CentreEasing.Linear(-3));
                        CreateEntity(new Platform(1, new(0, 0), ce.GetResult(), 0, 40));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateEntity(new DownBone(true, 320 + 150 + a * 10, 3, 70) { MissionLength = 70 });
                        }
                    });
                    RegisterFunctionOnce("DownBone1", () =>
                    {
                        CreateBone(new DownBone(true, 3, 80));
                    });
                    RegisterFunctionOnce("UpBone1", () =>
                    {
                        CreateBone(new UpBone(true, 3, 55));
                    });
                    RegisterFunctionOnce("BlueBone", () =>
                    {
                        CreateBone(new DownBone(true, 5, 140) { ColorType = 1 });
                    });
                    RegisterFunctionOnce("gs", () =>
                    {
                        TP();
                        SetGreenBox();
                        SetSoul(1);
                    });
                    RegisterFunctionOnce("WhiteOut", () => { WhiteOut(BeatTime(2f)); });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "BlueSoul(Platform1)","","","",   "","","","",
                        "","","BlueBone","",   "","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "UpBone1","","","",

                        "(Platform1)(DownBone1)","","","",   "","","","",
                        "","","","",   "(BlueBone)","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "(DownBone1)","","","",

                        "(Platform1)UpBone1","","","",   "","","","",
                        "","","BlueBone","",   "","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "UpBone1","","","",

                        "(Platform1)(DownBone1)","","","",   "","","","",
                        "","","","",   "(BlueBone)","","","",
                        "","","","",   "","","","",
                        "","","","",   "gs","","","",
                        //
                        "(R)(+01)","","","",   "(R)","","","",
                        "(R)(+01)","","","",   "(R)","","","",
                        "(R)(+01)","","","",   "(R)","","","",
                        "(R)(+01)","","","",   "","","","",

                        "(R)(R1)","","","",   "(R)","","","",
                        "(R)(R1)","","","",   "(R)","","","",
                        "(R)(R1)","","","",   "(R)","","","",
                        "(R)(R1)","","","",   "","","","",

                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "","","","",   "(R)(R1)","","","",

                        "","","","",   "(R)(R1)","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(WhiteOut)","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(80))
                {
                    RegisterFunctionOnce("Box", () =>
                    {
                        HeartAttribute.JumpSpeed = 6.5f;
                        SetSoul(2);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 240 - 100));
                        ce.Insert(BeatTime(31) - 1, CentreEasing.Circle(new Vector2(0, 0), 100, BeatTime(8), 270));
                        ce.Run((s) => { InstantSetBox(s, 220, 140); InstantTP(s.X, Heart.Centre.Y); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 42));
                        CreateBone(new DownBone(false, 6, 42));
                        CreateBone(new DownBone(true, 4.5f, 140) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4.5f, 140) { ColorType = 2 });
                    });
                    RegisterFunctionOnce("ChangeWay", () =>
                    {
                        Heart.GiveForce(180, 3);
                        PlaySound(Sounds.Ding);
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 6, 42));
                        CreateBone(new UpBone(false, 6, 42));
                        CreateBone(new DownBone(true, 4.5f, 140) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4.5f, 140) { ColorType = 2 });
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        Heart.GiveForce(0, 0);
                        SetBox(240, 84 * 2f, 84 * 1.75f);
                        TP();
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        int a = Rand(0, 1);
                        if (a == 0)
                        {
                            UpBone b1 = new(true, 0, 84 * 0.8f) { ColorType = 0 };
                            DownBone b2 = new(true, 0, 84 * 0.8f) { ColorType = Rand(1, 2) };
                            CreateBone(b2);
                            CreateBone(b1);
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 5, BeatTime(2)));
                            ve.Run((s) => { b1.Speed = s; b2.Speed = s; });
                        }
                        if (a == 1)
                        {
                            DownBone b1 = new(true, 0, 84 * 0.8f) { ColorType = 0 };
                            UpBone b2 = new(true, 0, 84 * 0.8f) { ColorType = Rand(1, 2) };
                            CreateBone(b2);
                            CreateBone(b1);
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 5, BeatTime(2)));
                            ve.Run((s) => { b1.Speed = s; b2.Speed = s; });
                        }
                    });
                    RegisterFunctionOnce("Move", () =>
                    {
                        SetSoul(2);
                        SetBox(new Vector2(320, 140), 220, 140);
                        TP(320, 80);
                    });
                    RegisterFunctionOnce("Box2", () =>
                    {
                        HeartAttribute.JumpSpeed = 6.5f;
                        SetSoul(2);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 240 - 100));
                        ce.Insert(BeatTime(15) - 1, CentreEasing.Circle(new Vector2(0, 0), 100, BeatTime(8), 270));
                        ce.Run((s) => { InstantSetBox(s, 220, 140); InstantTP(s.X, Heart.Centre.Y); });
                    });
                    RegisterFunctionOnce("Back", () =>
                    {
                        SetSoul(1);
                        SetGreenBox();
                        TP();

                    });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "(Box)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "ChangeWay","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Change","","","",   "","","","",
                        //
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "Move","","","",   "","","","",
                        //
                        "(Box2)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "Back","","","",   "","","","",

                    });
                }
                if (InBeat(140))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(240, 240, 240);
                        DelayBeat(1, () =>
                        {
                            for (int a = 0; a < 36; a++) CreateBone(new SideCircleBone(a * 10, 4, 50, BeatTime(30)));
                        });
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(16), ValueEasing.SinWave(30, BeatTime(4), 0));
                        ve.Insert(BeatTime(2), ValueEasing.EaseOutSine(0, 180, BeatTime(2)));
                        ve.Insert(BeatTime(14), ValueEasing.SinWave(30, BeatTime(4), 1f));
                        CentreEasing.EaseBuilder ce = new();
                        for (int a = 0; a < 4; a++)
                        {
                            ce.Insert(BeatTime(1), CentreEasing.EaseOutCirc(new(0, 90), MathUtil.GetVector2(90, 30 + 90), BeatTime(1)));
                            ce.Insert(BeatTime(1), CentreEasing.EaseInCirc(MathUtil.GetVector2(90, 30 + 90), new(0, 90), BeatTime(1)));
                            ce.Insert(BeatTime(1), CentreEasing.EaseOutCirc(new(0, 90), MathUtil.GetVector2(90, -30 + 90), BeatTime(1)));
                            ce.Insert(BeatTime(1), CentreEasing.EaseInCirc(MathUtil.GetVector2(90, -30 + 90), new(0, 90), BeatTime(1)));
                        }
                        ce.Insert(BeatTime(2), CentreEasing.EaseOutCirc(new(0, 90), new(0, -90), BeatTime(2)));
                        ce.Insert(BeatTime(1), CentreEasing.EaseOutCirc(new(0, -90), MathUtil.GetVector2(90, -30 + 90 + 180), BeatTime(1)));
                        ce.Insert(BeatTime(1), CentreEasing.EaseInCirc(MathUtil.GetVector2(90, -30 + 90 + 180), new(0, -90), BeatTime(1)));
                        for (int a = 0; a < 3; a++)
                        {
                            ce.Insert(BeatTime(1), CentreEasing.EaseOutCirc(new(0, -90), MathUtil.GetVector2(90, 30 + 90 + 180), BeatTime(1)));
                            ce.Insert(BeatTime(1), CentreEasing.EaseInCirc(MathUtil.GetVector2(90, 30 + 90 + 180), new(0, -90), BeatTime(1)));
                            ce.Insert(BeatTime(1), CentreEasing.EaseOutCirc(new(0, -90), MathUtil.GetVector2(90, -30 + 90 + 180), BeatTime(1)));
                            ce.Insert(BeatTime(1), CentreEasing.EaseInCirc(MathUtil.GetVector2(90, -30 + 90 + 180), new(0, -90), BeatTime(1)));
                        }
                        Platform platform = new(1, new(320, 240), ce.GetResult(), Motions.LengthRoute.stableValue, ve.GetResult()) { LengthRouteParam = new float[] { 40, BeatTime(32) } };
                        CreateEntity(platform);
                        DelayBeat(64, () => { platform.Dispose(); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {

                        CreateBone(new DownBone(true, 6, 115));
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {

                        CreateBone(new UpBone(false, 6, 115));
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 6, 115));
                    });
                    RegisterFunctionOnce("Bone4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 6, 115));
                    });
                    RegisterFunctionOnce("ChangeWay", () =>
                    {
                        Heart.GiveForce(180, 0);
                    });
                    RegisterFunctionOnce("GreenSoul", () =>
                    {
                        SetSoul(1);
                        TP();
                        SetGreenBox();
                        Heart.GiveForce(0, 0);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "","","","",   "R","","","",

                        "","","","",   "R","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "BlueSoul","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "ChangeWay","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(GreenSoul)","","","",   "","","","",
                        //
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "","","","",
                        "R1","","","",   "R1","","","",

                    });
                }
                if (InBeat(204))
                {
                    HeartAttribute.ArrowFixed = true;
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "(#1.8#R1)","","","(Tap)",

                        "D02","","","",   "+002","","+002","",
                        "+002","","","",   "+002","","+002","",
                        "+002","","","",   "","","+002","",
                        "","","","",   "+002","","","",

                        "D02","","","",   "+002","","+002","",
                        "+002","","","",   "+002","","+002","",
                        "+002","","","",   "","","+002","",
                        "","","","",   "+002","","","",

                        "D12","","","",   "+012","","+012","",
                        "+012","","","",   "+012","","+012","",
                        "+012","","","",   "","","+012","",
                        "","","","",   "+012","","","",

                        "D12","","","",   "+012","","+012","",
                        "+012","","","",   "+012","","+012","",
                        "+012","","","",   "","","+012","",
                        "","","","",   "+012","","","",
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
                if (InBeat(220))
                {
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        Heart.GiveForce(0, 0);
                        SetBox(240, 84 * 2f, 84 * 1.75f);
                        TP();
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        int a = Rand(0, 1);
                        if (a == 0)
                        {
                            UpBone b1 = new(true, 0, 84 * 0.8f) { ColorType = 0 };
                            DownBone b2 = new(true, 0, 84 * 0.8f) { ColorType = Rand(1, 2) };
                            CreateBone(b2);
                            CreateBone(b1);
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 5, BeatTime(2)));
                            ve.Run((s) => { b1.Speed = s; b2.Speed = s; });
                        }
                        if (a == 1)
                        {
                            DownBone b1 = new(true, 0, 84 * 0.8f) { ColorType = 0 };
                            UpBone b2 = new(true, 0, 84 * 0.8f) { ColorType = Rand(1, 2) };
                            CreateBone(b2);
                            CreateBone(b1);
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 5, BeatTime(2)));
                            ve.Run((s) => { b1.Speed = s; b2.Speed = s; });
                        }
                    });
                    RegisterFunctionOnce("Move", () =>
                    {
                        SetSoul(1);
                        SetBox(new Vector2(320, 240), 84, 84);
                        TP(320, 240);
                        HeartAttribute.Gravity = 4f;
                    });
                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "(#1.8#R)","","","(Tap)",

                        "D12","","","",   "+012","","+012","",
                        "+012","","","",   "+012","","+012","",
                        "+012","","","",   "","","+012","",
                        "","","","",   "+012","","","",

                        "D12","","","",   "+012","","+012","",
                        "+012","","","",   "+012","","+012","",
                        "+012","","","",   "","","+012","",
                        "","","","",   "+012","","","",

                        "D02","","","",   "+002","","+002","",
                        "+002","","","",   "+002","","+002","",
                        "+002","","","",   "","","+002","",
                        "","","","",   "+002","","","",

                        "D02","","","",   "+002","","+002","",
                        "+002","","","",   "+002","","+002","",
                        "+002","","","",   "","","+002","",
                        "","","","",   "+002","","","",
                        //                      
                        "Change","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "","","","",   "","","","",
                        "Move","","","",   "","","","",
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
                if (InBeat(252))
                {
                    HeartAttribute.ArrowFixed = true;
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });

                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "(#1.8#R1)","","","(Tap)",

                        "D02","","","",   "+002","","+002","",
                        "+002","","","",   "+002","","+002","",
                        "+002","","","",   "","","+002","",
                        "","","","",   "+002","","","",

                        "D02","","","",   "+002","","+002","",
                        "+002","","","",   "+002","","+002","",
                        "+002","","","",   "","","+002","",
                        "","","","",   "+002","","","",

                        "D12","","","",   "+012","","+012","",
                        "+012","","","",   "+012","","+012","",
                        "+012","","","",   "","","+012","",
                        "","","","",   "+012","","","",

                        "D12","","","",   "+012","","+012","",
                        "+012","","","",   "+012","","+012","",
                        "+012","","","",   "","","+012","",
                        "","","","",   "+012","","","",
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
                if (InBeat(271))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(240, 130, 640);
                        HeartAttribute.Gravity = 0f;
                        HeartAttribute.JumpTimeLimit = 0;
                        TP(320, 0);
                        DelayBeat(0.5f, () => { HeartAttribute.Gravity = 9.8f; });
                    });
                    RegisterFunctionOnce("Bones1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(true, 10, 25));
                        CreateBone(new RightBone(true, 10, 25));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 10, 35));
                            CreateBone(new RightBone(true, 10, 35));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 10, 45));
                            CreateBone(new RightBone(true, 10, 45));
                        });
                    });
                    RegisterFunctionOnce("ChanegWay1", () =>
                    {
                        Heart.GiveForce(270, 4);
                        Heart.JumpTimeLimit = 1;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("LeftBone1", () =>
                    {
                        CreateBone(new LeftBone(false, 10, 90));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("RightBone1", () =>
                    {
                        CreateBone(new RightBone(true, 6.5f, 80));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("ChanegWay2", () =>
                    {
                        Heart.GiveForce(90, 1);
                        Heart.JumpTimeLimit = 1;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("LeftBone2", () =>
                    {
                        CreateBone(new LeftBone(false, 12f, 75));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("RightBone2", () =>
                    {
                        CreateBone(new RightBone(false, 10f, 80));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("ChanegWay3", () =>
                    {
                        Heart.GiveForce(180, 0);
                        Heart.JumpTimeLimit = 0;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("ChanegWay4", () =>
                    {
                        Heart.GiveForce(0, 0);
                        Heart.JumpTimeLimit = 0;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("Bones2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(false, 10, 25));
                        CreateBone(new RightBone(false, 10, 25));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(false, 10, 35));
                            CreateBone(new RightBone(false, 10, 35));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(false, 10, 45));
                            CreateBone(new RightBone(false, 10, 45));
                        });
                    });
                    RegisterFunctionOnce("Bones3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(true, 640, 10, 20));
                        CreateBone(new RightBone(true, 640, 10, 70));
                        CreateBone(new LeftBone(true, 650, 10, 25));
                        CreateBone(new RightBone(true, 650, 10, 65));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 640, 10, 30));
                            CreateBone(new RightBone(true, 640, 10, 60));
                            CreateBone(new LeftBone(true, 650, 10, 35));
                            CreateBone(new RightBone(true, 650, 10, 55));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 640, 10, 50));
                            CreateBone(new RightBone(true, 640, 10, 40));
                            CreateBone(new LeftBone(true, 650, 10, 55));
                            CreateBone(new RightBone(true, 650, 10, 35));
                        });
                    });
                    RegisterFunctionOnce("SineBone", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(false, 7, 40);
                        RightBone b2 = new(false, 7, 40);
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("SineBone2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(true, 7, 40) { ColorType = 2 };
                        RightBone b2 = new(true, 7, 40) { ColorType = 2 };
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("SineBone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(true, 7, 40) { };
                        RightBone b2 = new(true, 7, 40) { };
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("BoxBack", () =>
                    {
                        InstantSetBox(240, 128, 490);
                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        PlaySound(Sounds.Ding);
                        SetSoul(1);
                        SetBox(240, 84, 84);
                        TP();
                        Heart.GiveForce(0, 0);
                    });

                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "","","","",   "BlueSoul","","","",

                        "Bones1","","","",   "","","","",
                        "","","","",   "","","","",
                        "ChanegWay1(LeftBone1)","","","",   "(LeftBone1)","","","",
                        "RightBone1","","","",   "","","","",

                        "RightBone1(ChanegWay2)","","","",   "RightBone1","","","",
                        "LeftBone2","","","",   "","","","",
                        "ChanegWay3(Bones2)","","","",   "","","","",
                        "","","(Bones3)","",   "","","","",

                        "ChanegWay4","","","",   "","","(SineBone)","",
                        "(SineBone)","","","",   "(SineBone)","","","",
                        "ChanegWay3(SineBone)","","(SineBone)","",   "","","(SineBone3)","",
                        "BoxBack","","(SineBone3)","",   "","","(SineBone2)","",

                        "ChanegWay4(SineBone2)","","","",   "(SineBone2)","","(SineBone2)","",
                        "(SineBone2)","","","",   "(SineBone3)","","(SineBone3)","",
                        "ChanegWay3","","(SineBone2)","",   "(SineBone2)","","","",
                        "(SineBone3)","","","",   "(SineBone3)","","","",
                        //
                        "Return"
                    });
                }
                if (InBeat(284))
                {
                    RegisterFunctionOnce("Void", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].VoidMode = true;
                            ars[x].ResetColor(ars[x].ArrowColor);
                        }
                    });
                    RegisterFunctionOnce("Clear", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].VoidMode = false;
                            ars[x].ResetColor(ars[x].ArrowColor);
                        }
                    });
                    RegisterFunctionOnce("ScaleIn", () =>
                    {
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(BeatTime(4), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(4)));
                        e1.Insert(BeatTime(4), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(4)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {
                            ScreenDrawing.ScreenScale += s * 0.020f;
                        });
                    });
                    RegisterFunctionOnce("ScaleBack", () =>
                    {
                        Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1, 0.05f);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "R","","","",   "R","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "R","","","",   "R","","","",

                        "R","","","",   "R","","","",
                        "R(ScaleIn)","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "R","","","",   "","","","",
                        //
                        "Void(R)","","","",   "R'0.9","","","",
                        "R'0.8","","","",   "R'0.7","","","",
                        "","","","",   "R'0.6","","","",
                        "","","","",   "R'0.5","","","",

                        "","","R'0.5","",   "","","","",
                        "","","","",   "R'0.5","","","",
                        "","","","",   "","","","",
                        "R'0.5","","","",   "","","","",

                        "","","R'0.4","",   "","","","",
                        "","","","",   "R'0.4","","","",
                        "","","","",   "","","","",
                        "R'0.3","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(ScaleBack)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","(R)(R1)","",
                        "","","","",   "(R)(R1)","","","",

                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","(R)(R1)","",
                        "","","","",   "(R)(R1)","","","",

                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","(R)(R1)","",
                        "","","","",   "(R)(R1)","","","",

                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","(R)(R1)","",
                        "","","","",   "(R)(R1)","","","",
                        //
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","(R)(R1)","",
                        "","","","",   "(R)(R1)","","","",

                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","(R)(R1)","",
                        "","","","",   "(R)(R1)","","","",

                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","","",
                        "(R)(R1)","","","",   "","","(R)(R1)","",
                        "","","","",   "(R)(R1)","","","Clear",

                        "(R'1.03)(R1'1.03)","","","",   "","","","",
                        "(R'1.08)(R1'1.08)","","","",   "","","","",
                        "(R'1.15)(R1'1.15)","","","",   "","","(R'1.25)(R1'1.25)","",
                        "","","","",   "","","","",
                        //
                        
                    });
                }
                if (InBeat(348))
                {
                    Player.Heart main = Heart;
                    Player.Heart extra = CreateHeart(240, 84, 84);
                    RegisterFunctionOnce("Split", () =>
                    {

                        SetPlayerBoxMission(main);
                        TP(320 + 50, 240 + 50);
                        SetBox(new Vector2(320 + 50, 240 + 50), 84, 84);
                        main.Shields.RemoveShield(main.Shields.RShield);
                        SetPlayerBoxMission(extra);
                        SetSoul(1);
                        TP(320 - 50, 240 - 50);
                        SetBox(new Vector2(320 - 50, 240 - 50), 84, 84);
                        extra.Shields.RemoveShield(extra.Shields.BShield);
                        ForBeat120(64, () => { if (MathUtil.GetDistance(main.Centre, extra.Centre) <= 42) { main.controlingBox.GreenSoulAlpha = 0.25f; extra.controlingBox.GreenSoulAlpha = 0.25f; } else { main.controlingBox.GreenSoulAlpha = 0.5f; extra.controlingBox.GreenSoulAlpha = 0.5f; } });
                    });
                    RegisterFunctionOnce("Move1", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 + 50, 240 + 50));
                        ce.Insert(BeatTime(1f), CentreEasing.EaseOutElastic(new(320 + 50, 240 + 50), new(320 + 50, 240 - 50), BeatTime(2f)));
                        ce.Run((s) => { SetPlayerBoxMission(main); InstantTP(s); InstantSetBox(s, 84, 84); });
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(0, CentreEasing.Stable(320 - 50, 240 - 50));
                        ce2.Insert(BeatTime(1f), CentreEasing.EaseOutElastic(new(320 - 50, 240 - 50), new(320 - 50, 240 + 50), BeatTime(2f)));
                        ce2.Run((s) => { SetPlayerBoxMission(extra); InstantTP(s); InstantSetBox(s, 84, 84); });
                    });
                    RegisterFunctionOnce("Move2", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 + 50, 240 - 50));
                        ce.Insert(BeatTime(1f), CentreEasing.EaseOutElastic(new(320 + 50, 240 - 50), new(320 - 50, 240 - 50), BeatTime(2f)));
                        ce.Run((s) => { SetPlayerBoxMission(main); InstantTP(s); InstantSetBox(s, 84, 84); });
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(0, CentreEasing.Stable(320 - 50, 240 + 50));
                        ce2.Insert(BeatTime(1f), CentreEasing.EaseOutElastic(new(320 - 50, 240 + 50), new(320 + 50, 240 + 50), BeatTime(2f)));
                        ce2.Run((s) => { SetPlayerBoxMission(extra); InstantTP(s); InstantSetBox(s, 84, 84); });
                    });
                    RegisterFunctionOnce("Move3", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 - 50, 240 - 50));
                        ce.Insert(BeatTime(1f), CentreEasing.EaseOutElastic(new(320 - 50, 240 - 50), new(320 - 50, 240 + 50), BeatTime(2f)));
                        ce.Run((s) => { SetPlayerBoxMission(main); InstantTP(s); InstantSetBox(s, 84, 84); });
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(0, CentreEasing.Stable(320 + 50, 240 + 50));
                        ce2.Insert(BeatTime(1f), CentreEasing.EaseOutElastic(new(320 + 50, 240 + 50), new(320 + 50, 240 - 50), BeatTime(2f)));
                        ce2.Run((s) => { SetPlayerBoxMission(extra); InstantTP(s); InstantSetBox(s, 84, 84); });
                    });
                    RegisterFunctionOnce("Move4", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 - 50, 240 + 50));
                        ce.Insert(BeatTime(2f), CentreEasing.EaseOutElastic(new(320 - 50, 240 + 50), new(320 + 50, 240 + 50), BeatTime(2f)));
                        ce.Run((s) => { SetPlayerBoxMission(main); InstantTP(s); InstantSetBox(s, 84, 84); });
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(0, CentreEasing.Stable(320 + 50, 240 - 50));
                        ce2.Insert(BeatTime(2f), CentreEasing.EaseOutElastic(new(320 + 50, 240 - 50), new(320 - 50, 240 - 50), BeatTime(2f)));
                        ce2.Run((s) => { SetPlayerBoxMission(extra); InstantTP(s); InstantSetBox(s, 84, 84); });

                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        SetPlayerBoxMission(main);
                        SetBox(new Vector2(320, 140), 220, 140);
                        TP(320, 80);
                        SetSoul(2);
                        Heart.JumpSpeed = 6.5f;
                        Heart.JumpTimeLimit = 2;
                        SetPlayerBoxMission(extra);
                        SetBox(new Vector2(320, 340), 220, 140);
                        TP(320, 80);
                        SetSoul(2);
                        Heart.JumpSpeed = 6.5f;
                        Heart.GiveForce(180, 0);
                        Heart.JumpTimeLimit = 2;
                        ;
                    });

                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            if (ars[x].RotateType == 2) ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    RegisterFunctionOnce("Box", () =>
                    {
                        Platform[] ps = GetAll<Platform>();
                        for (int a = 0; a < ps.Length; a++)
                        {
                            int x = a;
                            ps[x].Dispose();
                        }

                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 240 - 100));
                        ce.Insert(BeatTime(15) - 1, CentreEasing.Circle(new Vector2(0, 0), 110, BeatTime(8), 270));
                        ce.Run((s) => { SetPlayerBoxMission(main); InstantSetBox(s, 220, 140); InstantTP(s.X, Heart.Centre.Y); });
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(0, CentreEasing.Stable(320, 240 + 100));
                        ce2.Insert(BeatTime(15) - 1, CentreEasing.Circle(new Vector2(0, 0), 110, BeatTime(8), 270 + 180));
                        ce2.Run((s) => { SetPlayerBoxMission(extra); InstantSetBox(s, 220, 140); InstantTP(s.X, Heart.Centre.Y); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {
                        SetPlayerBoxMission(main);
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 42));
                        CreateBone(new DownBone(false, 6, 42));
                        CreateBone(new DownBone(true, 4.5f, 140) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4.5f, 140) { ColorType = 2 });
                        SetPlayerBoxMission(extra);
                        CreateBone(new UpBone(true, 6, 42));
                        CreateBone(new UpBone(false, 6, 42));
                        CreateBone(new DownBone(true, 4.5f, 140) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4.5f, 140) { ColorType = 2 });
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {
                        SetPlayerBoxMission(extra);
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 42));
                        CreateBone(new DownBone(false, 6, 42));
                        CreateBone(new DownBone(true, 4.5f, 140) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4.5f, 140) { ColorType = 2 });
                        SetPlayerBoxMission(main);
                        CreateBone(new UpBone(true, 6, 42));
                        CreateBone(new UpBone(false, 6, 42));
                        CreateBone(new DownBone(true, 4.5f, 140) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4.5f, 140) { ColorType = 2 });
                    });
                    RegisterFunctionOnce("ABone2", () =>
                    {
                        SetPlayerBoxMission(extra);
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 42) { ColorType = 2 });
                        CreateBone(new DownBone(false, 6, 42) { ColorType = 2 });
                        CreateBone(new DownBone(true, 4.5f, 140) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4.5f, 140) { ColorType = 2 });
                        SetPlayerBoxMission(main);
                        CreateBone(new UpBone(true, 6, 42) { ColorType = 2 });
                        CreateBone(new UpBone(false, 6, 42) { ColorType = 2 });
                        CreateBone(new DownBone(true, 4.5f, 140) { ColorType = 2 });
                        CreateBone(new DownBone(false, 4.5f, 140) { ColorType = 2 });
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        BoxStates.BoxMovingScale = 0.3f;

                        SetPlayerBoxMission(main);
                        Heart.GiveForce(0, 0);
                        SetBox(new Vector2(320, 240 + 84 * 1.75f / 2), 170f, 84 * 1.75f);
                        TP(new Vector2(320, 240 + 84 * 1.75f / 2));
                        SetSoul(0);
                        SetPlayerBoxMission(extra);
                        Heart.GiveForce(180, 0);
                        SetBox(new Vector2(320, 240 - 84 * 1.75f / 2), 170f, 84 * 1.75f);
                        TP(new Vector2(320, 240 - 84 * 1.75f / 2));
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        int a = Rand(0, 1);
                        if (a == 0)
                        {
                            int colortype = Rand(1, 2);
                            SetPlayerBoxMission(main);
                            UpBone b1 = new(true, 0, 84 * 0.8f) { ColorType = 0 };
                            DownBone b2 = new(true, 0, 84 * 0.8f) { ColorType = colortype };
                            CreateBone(b2);
                            CreateBone(b1);
                            SetPlayerBoxMission(extra);
                            DownBone b3 = new(false, 0, 84 * 0.8f) { ColorType = 0 };
                            UpBone b4 = new(false, 0, 84 * 0.8f) { ColorType = colortype };
                            CreateBone(b3);
                            CreateBone(b4);
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 5, BeatTime(2)));
                            ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = s; });
                        }
                        if (a == 1)
                        {
                            int colortype = Rand(1, 2);
                            SetPlayerBoxMission(main);
                            DownBone b1 = new(true, 0, 84 * 0.8f) { ColorType = 0 };
                            UpBone b2 = new(true, 0, 84 * 0.8f) { ColorType = colortype };
                            CreateBone(b2);
                            CreateBone(b1);
                            SetPlayerBoxMission(extra);
                            UpBone b3 = new(false, 0, 84 * 0.8f) { ColorType = 0 };
                            DownBone b4 = new(false, 0, 84 * 0.8f) { ColorType = colortype };
                            CreateBone(b3);
                            CreateBone(b4);
                            ValueEasing.EaseBuilder ve = new();
                            ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 5, BeatTime(2)));
                            ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = s; });
                        }
                    });

                    RegisterFunctionOnce("Back", () =>
                    {
                        SetSoul(1);
                        SetGreenBox();
                        TP();

                    });
                    RegisterFunctionOnce("Over", () =>
                    {
                        ValueEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(2), ValueEasing.EaseInQuad(0, 360, BeatTime(2)));
                        ce.Run((s) =>
                        {
                            SetPlayerBoxMission(main);
                            InstantSetBox(240 + 84 * 1.75f / 2 + s, 170, 84 * 1.75f);
                            SetPlayerBoxMission(extra);
                            InstantSetBox(240 - 84 * 1.75f / 2 - s, 170, 84 * 1.75f);
                        });
                        DelayBeat(2, () =>
                        {
                            extra.Dispose();
                        });
                    });
                    RegisterFunctionOnce("HeartUp", () =>
                    {
                        SetPlayerBoxMission(main);
                        Heart.Shields.AddShield(Heart.Shields.RShield);
                        SetSoul(1);
                        ValueEasing.EaseBuilder ce = new();
                        ce.Insert(0, ValueEasing.Stable(600));
                        ce.Insert(BeatTime(2.5f), ValueEasing.EaseOutCubic(600, 240, BeatTime(2.5f)));
                        ce.Run((s) => { InstantSetBox(new Vector2(320, s), 84, 84); InstantTP(320, s); });
                    });
                    SetPlayerBoxMission(main);
                    BarrageCreate(BeatTime(3), BeatTime(1), 5, new string[]
                    {
                        "","","","",   "","Tap(Split)","","",

                        "$302","","","",   "$302","","","",
                        "$302","","","",   "$302","","","",
                        "$302","","","",   "","","$302","",
                        "","","","",   "$302(Move1)","","","",

                        "$002","","","",   "$002","","","",
                        "$002","","","",   "$002","","","",
                        "$002","","","",   "","","$002","",
                        "","","","",   "$002(Move2)","","","",

                        "$102","","","",   "$102","","","",
                        "$102","","","",   "$102","","","",
                        "$102","","","",   "","","$102","",
                        "","","","",   "$102(Move3)","","","",

                        "$202","","","",   "$202","","","",
                        "$202","","","",   "$202","","","",
                        "$202","","","",   "","","$202","",
                        "","","","",   "$202(Move4)","","","",
                        //
                        "(R)","","","",   "(+0)","","(+001)","",
                        "(+0)","","","",   "(+0)","","(+001)","",
                        "(+0)","","","",   "","","(+0)","",
                        "","","","",   "(+0)(Move1)","","","",

                        "(R)","","","",   "(+0)","","(+001)","",
                        "(+0)","","","",   "(+0)","","(+001)","",
                        "(+0)","","","",   "","","(+0)","",
                        "","","","",   "(+0)(Move2)","","","",

                        "(R)","","","",   "(+0)","","(+001)","",
                        "(+0)","","","",   "(+0)","","(+001)","",
                        "(+0)","","","",   "","","(+0)","",
                        "","","","",   "(+0)(Move3)","","","",

                        "(R)","","","",   "(+0)","","(+001)","",
                        "(+0)","","","",   "(+0)","","(+001)","",
                        "(+0)","","","",   "","","(+0)","",
                        "","","","",   "(+0)(Return)","","","",
                        //
                        "Box","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "ABone2","","","",   "","","","",
                        "Change","","","",   "","","","",
                        //
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Over)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "HeartUp"
                    });
                    SetPlayerBoxMission(extra);

                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","$112","",
                        "","","","",   "","","$112","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","$212","",
                        "","","","",   "","","$212","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","$312","",
                        "","","","",   "","","$312","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","$012","",
                        "","","","",   "","","$012","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
//
                        "(R1)","","","",   "(+01)","","(+011)","",
                        "(+01)","","","",   "(+01)","","(+011)","",
                        "(+01)","","","",   "","","(+01)","",
                        "","","","",   "(+01)","","","",

                        "(R1)","","","",   "(+01)","","(+011)","",
                        "(+01)","","","",   "(+01)","","(+011)","",
                        "(+01)","","","",   "","","(+01)","",
                        "","","","",   "(+01)","","","",

                        "(R1)","","","",   "(+01)","","(+011)","",
                        "(+01)","","","",   "(+01)","","(+011)","",
                        "(+01)","","","",   "","","(+01)","",
                        "","","","",   "(+01)","","","",

                        "(R1)","","","",   "(+01)","","(+011)","",
                        "(+01)","","","",   "(+01)","","(+011)","",
                        "(+01)","","","",   "","","(+01)","",
                        "","","","",   "(+01)","","","",
                        //
                    });
                }
                if (InBeat(412))
                {
                    SetPlayerBoxMission(0);
                    RegisterFunctionOnce("VoidHold", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            if (ars[a].ArrowColor == 0)
                            {

                                ars[x].VoidMode = true;
                                ars[x].JudgeType = Arrow.JudgementType.Hold;

                            }
                            ars[x].ResetColor(ars[x].ArrowColor);
                        }
                    });
                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "VoidHold","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "$0($21)","","","",   "$0","","$0","",
                        "$0($21)($21)","","","",   "$0","","$0","",
                        "$0($21)","","","",   "","","$0","",
                        "($21)($21)","","","",   "$0","","","",

                        "$0($21)","","","",   "$0","","$0","",
                        "$0($21)($21)","","","",   "$0","","$0","",
                        "$0($21)","","","",   "","","$0","",
                        "($21)($21)","","","",   "$0","","","",

                        "$0($21)","","","",   "$0","","$0","",
                        "$0($21)($21)","","","",   "$0","","$0","",
                        "$0($21)","","","",   "","","$0","",
                        "($21)","","","",   "$0","","","",
                        //
                        "$31","","","",   "","","","",
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
            }
            public void Noob()
            {
                if (InBeat(0))
                {
                    RegisterFunctionOnce("Line", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(640, 0));
                        ce.Insert(BeatTime(16), CentreEasing.Linear(-2));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { DrawingColor = Microsoft.Xna.Framework.Color.DarkGreen };
                        CreateEntity(l);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(16), ValueEasing.Combine(ValueEasing.Stable(0.5f), ValueEasing.SinWave(1, BeatTime(2), -0.5f)));
                        ve.Run((s) => { l.Alpha = s; });
                        for (int a = 1; a < 16; a++) l.InsertRetention(new Line.RetentionEffect(BeatTime(2 * a)));

                    });
                    RegisterFunctionOnce("LineAnother", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(640 + BeatTime(1) * 2, 0));
                        ce.Insert(BeatTime(16), CentreEasing.Linear(-2));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { DrawingColor = Microsoft.Xna.Framework.Color.DarkGreen };
                        CreateEntity(l);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(16), ValueEasing.Combine(ValueEasing.Stable(0.2f), ValueEasing.SinWave(0.4f, BeatTime(2), -0.5f)));
                        ve.Run((s) => { l.Alpha = s; });
                        for (int a = 1; a < 8; a++) l.InsertRetention(new Line.RetentionEffect(BeatTime(2 * a)));

                    });
                    BarrageCreate(0, BeatTime(1), 5, new string[]
                    {
                        "($0)(Line)(LineAnother)","","","",   "","","","",
                        "(+0)","","","",   "","","","",
                        "(+0)","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(R)","","","",   "","","","",
                        "(+0)","","","",   "","","","",
                        "(+0)","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(R)","","","",   "","","","",
                        "(+0)","","","",   "","","","",
                        "(+0)","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(R)","","","",   "","","","",
                        "(+0)","","","",   "","","","",
                        "(+0)","","","",   "","","+0","",
                        "","","","",   "+0","","","",
                        //

                    });
                    BarrageCreate(BeatTime(4) * 4, BeatTime(1), 5, new string[]
                    {
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",

                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",

                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",

                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        //
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",

                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",

                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",

                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "+0","","","",   "","","","",
                        //
                    });

                }
                if (InBeat(48))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(290, 240, 140);
                        DelayBeat(0.125f, () =>
                        {
                            for (int a = 0; a < 32 * 2 * 1.8f; a++)
                                CreateBone(new DownBone(true, 400 + 140 + 3 * a * BeatTime(0.125f), 3, 32) { MarkScore = false });
                        });
                    });
                    RegisterFunctionOnce("Platform1", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 + 155, 320));
                        ce.Insert(BeatTime(8), CentreEasing.Linear(-2.3f));
                        CreateEntity(new Platform(0, new(0, 0), ce.GetResult(), 0, 40));
                    });
                    RegisterFunctionOnce("Platform2", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 + 164, 320 - 40));
                        ce.Insert(BeatTime(8), CentreEasing.Linear(-2.3f));
                        CreateEntity(new Platform(0, new(0, 0), ce.GetResult(), 0, 40));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateEntity(new DownBone(true, 320 + 150 + a * 10, 2.3f, 70) { MissionLength = 70 });
                        }
                    });
                    RegisterFunctionOnce("DownBone1", () =>
                    {
                        CreateBone(new DownBone(true, 3, 80));
                    });
                    RegisterFunctionOnce("UpBone1", () =>
                    {
                        CreateBone(new UpBone(true, 3, 55));
                    });
                    RegisterFunctionOnce("BlueBone", () =>
                    {
                        CreateBone(new DownBone(true, 5, 140) { ColorType = 1 });
                    });
                    RegisterFunctionOnce("gs", () =>
                    {
                        TP();
                        SetGreenBox();
                        SetSoul(1);
                    });
                    RegisterFunctionOnce("WhiteOut", () => { WhiteOut(BeatTime(2f)); });
                    BarrageCreate(0, BeatTime(1), 5, new string[]
                    {
                        "BlueSoul(Platform1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Platform1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Platform1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "","","","",

                        "(Platform1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "gs","","","",
                        //
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(WhiteOut)","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(80))
                {
                    RegisterFunctionOnce("Box", () =>
                    {
                        HeartAttribute.JumpSpeed = 6.5f;
                        SetSoul(2);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(1, CentreEasing.Stable(320, 240));

                        ce.Run((s) => { InstantSetBox(s, 220, 140); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 20));
                        CreateBone(new DownBone(false, 6, 20));
                    });
                    RegisterFunctionOnce("ChangeWay", () =>
                    {
                        Heart.GiveForce(180, 3);
                        PlaySound(Sounds.Ding);
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 6, 20));
                        CreateBone(new UpBone(false, 6, 20));
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        Heart.GiveForce(0, 0);
                        SetBox(240, 84 * 2.2f, 84 * 1.75f);

                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 2 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 2 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Bone4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 1 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 1 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Move", () =>
                        {
                            SetSoul(2);
                            SetBox(new Vector2(320, 240), 220, 140);
                        });
                    RegisterFunctionOnce("Box2", () =>
                    {
                        HeartAttribute.JumpSpeed = 6.5f;
                        SetSoul(2);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 240));

                        SetBox(new Vector2(320, 240), 220, 140);
                    });
                    RegisterFunctionOnce("Back", () =>
                    {
                        SetSoul(1);
                        SetGreenBox();
                        TP();

                    });
                    BarrageCreate(0, BeatTime(1), 5, new string[]
                    {
                        "(Box)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "ChangeWay","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Change","","","",   "","","","",
                        //
                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone4","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "Move","","","",   "","","","",
                        //
                        "(Box2)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "Back","","","",   "","","","",

                    });
                }
                if (InBeat(140))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(240, 240, 240);
                        DelayBeat(1, () =>
                        {
                            for (int a = 0; a < 18; a++) CreateBone(new SideCircleBone(a * 20, 2.5f, 50, BeatTime(30)));
                        });
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(0, 70));
                        ce.Insert(BeatTime(16), CentreEasing.YSinWave(10, BeatTime(4), 0));
                        ce.Insert(BeatTime(2), CentreEasing.EaseOutCirc(new(0, 70), new(0, -70), BeatTime(2)));
                        ce.Insert(BeatTime(14), CentreEasing.YSinWave(10, BeatTime(4), 0.5f));
                        Platform platform = new(0, new(320, 240), ce.GetResult(), 0, 40);
                        CreateEntity(platform);
                        Platform platform2 = new(0, new(320 + 2, 240 + 5), ce.GetResult(), 180, 40);
                        CreateEntity(platform2);
                        DelayBeat(64, () => { platform.Dispose(); platform2.Dispose(); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {

                        CreateBone(new DownBone(true, 6, 60));
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {

                        CreateBone(new UpBone(false, 6, 60));
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 6, 60));
                    });
                    RegisterFunctionOnce("Bone4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 6, 60));
                    });
                    RegisterFunctionOnce("ChangeWay", () =>
                        {
                            Heart.GiveForce(180, 0);
                        });
                    RegisterFunctionOnce("GreenSoul", () =>
                    {
                        SetSoul(1);
                        TP();
                        SetGreenBox();
                        Heart.GiveForce(0, 0);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 5, new string[]
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
                        "BlueSoul","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "ChangeWay","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(GreenSoul)","","","",   "","","","",
                        //
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 5, new string[]
                    {
                        "R","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                    });
                }
                if (InBeat(204))
                {
                    HeartAttribute.ArrowFixed = true;
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    BarrageCreate(BeatTime(3), BeatTime(1), 5, new string[]
                    {
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","","",   "+0","","","",

                        "","","","",   "R","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "+0","","","",
                        "","","","",   "R","","","",

                        "","","","",   "+0","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //

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
                if (InBeat(220))
                {
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        Heart.GiveForce(0, 0);
                        SetBox(240, 84 * 2f, 84 * 1.75f);
                        TP();
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 1 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 1 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Move", () =>
                        {
                            SetSoul(1);
                            SetBox(new Vector2(320, 240), 84, 84);
                            TP(320, 240);
                            HeartAttribute.Gravity = 4f;
                        });
                    BarrageCreate(BeatTime(3), BeatTime(1), 5, new string[]
                    {
                        "R","","","",   "+0","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","","",   "+0","","","",

                        "","","","",   "R","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "+0","","","",
                        "+0","","","",   "+0","","","",

                        "","","","",   "+0","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "R","","","",

                        "","","","",   "+0","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "","","","",   "","","","",
                        //                      
                        "Change","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "","","","",   "","","","",
                        "Move","","","",   "","","","",
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
                if (InBeat(252))
                {
                    HeartAttribute.ArrowFixed = true;
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });

                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "R","","","",   "+0","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "R","","","",   "+0","","","",

                        "","","","",   "R","","","",
                        "+0","","","",   "","","","",
                        "R","","","",   "+0","","","",
                        "+0","","","",   "+0","","","",

                        "","","","",   "+0","","","",
                        "+0","","","",   "","","","",
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
                if (InBeat(271))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(240, 130, 640);
                        HeartAttribute.Gravity = 0f;
                        HeartAttribute.JumpTimeLimit = 0;
                        TP(320, 0);
                        DelayBeat(0.5f, () => { HeartAttribute.Gravity = 9.8f; });
                    });
                    RegisterFunctionOnce("Bones1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(true, 10, 35));
                        CreateBone(new RightBone(true, 10, 35));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 10, 35));
                            CreateBone(new RightBone(true, 10, 35));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 10, 35));
                            CreateBone(new RightBone(true, 10, 35));
                        });
                    });
                    RegisterFunctionOnce("ChanegWay1", () =>
                    {
                        Heart.GiveForce(270, 4);
                        Heart.JumpTimeLimit = 1;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("LeftBone1", () =>
                    {
                        CreateBone(new LeftBone(false, 10, 50));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("RightBone1", () =>
                    {
                        CreateBone(new RightBone(true, 6.5f, 50));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("ChanegWay2", () =>
                    {
                        Heart.GiveForce(90, 4);
                        Heart.JumpTimeLimit = 1;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("LeftBone2", () =>
                    {
                        CreateBone(new LeftBone(false, 12f, 50));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("RightBone2", () =>
                    {
                        CreateBone(new RightBone(false, 10f, 50));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("ChanegWay3", () =>
                    {
                        Heart.GiveForce(180, 0);
                        Heart.JumpTimeLimit = 0;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("ChanegWay4", () =>
                    {
                        Heart.GiveForce(0, 0);
                        Heart.JumpTimeLimit = 0;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("Bones2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(false, 10, 35));
                        CreateBone(new RightBone(false, 10, 35));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(false, 10, 35));
                            CreateBone(new RightBone(false, 10, 35));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(false, 10, 35));
                            CreateBone(new RightBone(false, 10, 35));
                        });
                    });
                    RegisterFunctionOnce("Bones3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(true, 640, 10, 35));
                        CreateBone(new RightBone(true, 640, 10, 34));
                        CreateBone(new LeftBone(true, 660, 10, 35));
                        CreateBone(new RightBone(true, 660, 10, 35));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 640, 10, 35));
                            CreateBone(new RightBone(true, 640, 10, 35));
                            CreateBone(new LeftBone(true, 660, 10, 35));
                            CreateBone(new RightBone(true, 660, 10, 35));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 640, 10, 35));
                            CreateBone(new RightBone(true, 640, 10, 35));
                            CreateBone(new LeftBone(true, 660, 10, 35));
                            CreateBone(new RightBone(true, 660, 10, 35));
                        });
                    });
                    RegisterFunctionOnce("SineBone", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(false, 7, 40);
                        RightBone b2 = new(false, 7, 40);
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("SineBone2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(true, 7, 40) { ColorType = 2 };
                        RightBone b2 = new(true, 7, 40) { ColorType = 2 };
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("SineBone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(true, 7, 40) { };
                        RightBone b2 = new(true, 7, 40) { };
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("BoxBack", () =>
                    {
                        InstantSetBox(240, 128, 490);
                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        PlaySound(Sounds.Ding);
                        SetSoul(1);
                        SetBox(240, 84, 84);
                        TP();
                        Heart.GiveForce(0, 0);
                    });

                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "","","","",   "BlueSoul","","","",

                        "Bones1","","","",   "","","","",
                        "","","","",   "","","","",
                        "ChanegWay1(LeftBone1)","","","",   "(LeftBone1)","","","",
                        "LeftBone1","","","",   "","","","",

                        "RightBone1(ChanegWay2)","","","",   "RightBone1","","","",
                        "RightBone1","","","",   "","","","",
                        "ChanegWay3(Bones2)","","","",   "","","","",
                        "","","Bones3","",   "","","","",

                        "ChanegWay4","","","",   "","","(SineBone2)","",
                        "(SineBone2)","","","",   "(SineBone2)","","","",
                        "ChanegWay3(SineBone2)","","(SineBone2)","",   "","","(SineBone2)","",
                        "BoxBack","","(SineBone2)","",   "","","(SineBone2)","",

                        "ChanegWay4(SineBone2)","","","",   "(SineBone2)","","(SineBone2)","",
                        "(SineBone2)","","","",   "(SineBone2)","","(SineBone2)","",
                        "ChanegWay3","","(SineBone2)","",   "(SineBone2)","","","",
                        "(SineBone2)","","","",   "(SineBone2)","","","",
                        //
                        "Return"
                    });
                }
                if (InBeat(284))
                {
                    RegisterFunctionOnce("Void", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].VoidMode = true;
                            ars[x].ResetColor(ars[x].ArrowColor);
                        }
                    });
                    RegisterFunctionOnce("ScaleIn", () =>
                    {
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(BeatTime(4), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(4)));
                        e1.Insert(BeatTime(4), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(4)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {
                            ScreenDrawing.ScreenScale += s * 0.020f;
                        });
                    });
                    RegisterFunctionOnce("ScaleBack", () =>
                    {
                        Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1, 0.05f);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",
                        "R","","","",   "+0","","","",
                        "","","","",   "R","","","",
                        "+0","","","",   "+0","","","",

                        "+0","","","",   "+0","","","",
                        "+0","","","",   "+0","","","",
                        "","","","",   "R","","","",
                        "+0","","","",   "","","","",

                        "R","","","",   "+0","","","",
                        "+0","","","",   "+0","","","",
                        "","","","",   "R","","","",
                        "+0","","","",   "+0","","","",

                        "+0","","","",   "+0","","","",
                        "+0(ScaleIn)","","","",   "+0","","","",
                        "","","","",   "R","","","",
                        "+0","","","",   "","","","",
                        //
                        "Void(R)","","","",   "R'0.9","","","",
                        "+0'0.8","","","",   "+0'0.7","","","",
                        "","","","",   "+0'0.6","","","",
                        "","","","",   "+0'0.5","","","",

                        "","","+0'0.5","",   "","","","",
                        "","","","",   "+0'0.5","","","",
                        "","","","",   "","","","",
                        "+0'0.5","","","",   "","","","",

                        "","","+0'0.4","",   "","","","",
                        "","","","",   "+0'0.4","","","",
                        "","","","",   "","","","",
                        "+0'0.3","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(ScaleBack)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(+0)","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(+0)","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(+0)","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(+0)","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",
                        //
                        "(+0)","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(+0)","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(+0)","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "(+0)","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "","","","",
                        //

                    });
                }
                if (InBeat(348))
                {

                    RegisterFunctionOnce("Split", () =>
                    {


                        TP(320, 240);
                        SetBox(new Vector2(320, 240), 84, 84);


                    });
                    RegisterFunctionOnce("Return", () =>
                    {

                        SetBox(new Vector2(320, 240), 220, 140);
                        TP(320, 80);
                        SetSoul(2);
                        Heart.JumpSpeed = 6.5f;
                        Heart.JumpTimeLimit = 2;
                        ;
                    });

                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            if (ars[x].RotateType == 2) ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    RegisterFunctionOnce("Box", () =>
                    {
                        Platform[] ps = GetAll<Platform>();
                        for (int a = 0; a < ps.Length; a++)
                        {
                            int x = a;
                            ps[x].Dispose();
                        }

                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(1, CentreEasing.Stable(320, 240));
                        ce.Run((s) => { SetBox(s, 220, 140); TP(320, 240); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 20));
                        CreateBone(new DownBone(false, 6, 20));
                    });



                    RegisterFunctionOnce("Change", () =>
                    {
                        BoxStates.BoxMovingScale = 0.3f;
                        Heart.GiveForce(0, 0);
                        SetBox(new Vector2(320, 240), 170f, 84 * 1.75f);
                        TP(new Vector2(320, 240));
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 1 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 1 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });

                    RegisterFunctionOnce("Back", () =>
                        {
                            SetSoul(1);
                            SetGreenBox();
                            TP();

                        });
                    RegisterFunctionOnce("Over", () =>
                    {
                        ValueEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(2), ValueEasing.EaseInQuad(0, 360, BeatTime(2)));
                        ce.Run((s) =>
                        {
                            InstantSetBox(240 + s, 170, 84 * 1.75f);
                        });

                    });
                    RegisterFunctionOnce("HeartUp", () =>
                    {
                        SetSoul(1);
                        ValueEasing.EaseBuilder ce = new();
                        ce.Insert(0, ValueEasing.Stable(600));
                        ce.Insert(BeatTime(2.5f), ValueEasing.EaseOutCubic(600, 240, BeatTime(2.5f)));
                        ce.Run((s) => { InstantSetBox(new Vector2(320, s), 84, 84); InstantTP(320, s); });
                    });

                    BarrageCreate(BeatTime(3), BeatTime(1), 5, new string[]
                    {
                        "","","","",   "","","","",

                        "R","","","",   "+0","","+0","",
                        "+0","","","",   "+0","","+0","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "+0","","+0","",
                        "+0","","","",   "+0","","+0","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "+0","","+0","",
                        "+0","","","",   "+0","","+0","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "+0","","+0","",
                        "+0","","","",   "+0","","+0","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",
                        //
                         "R","","","",   "+0","","+0","",
                        "+0","","","",   "+0","","+0","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "+0","","+0","",
                        "+0","","","",   "+0","","+0","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "+0","","+0","",
                        "+0","","","",   "+0","","+0","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0","","","",

                        "R","","","",   "+0","","+0","",
                        "+0","","","",   "+0","","+0","",
                        "+0","","","",   "","","+0","",
                        "","","","",   "+0(Return)","","","",
                        //
                        "Box","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "Change","","","",   "","","","",
                        //
                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone2","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Over)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "HeartUp"
                    });


                }
                if (InBeat(412))
                {
                    SetPlayerBoxMission(0);
                    RegisterFunctionOnce("VoidHold", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            if (ars[a].ArrowColor == 0)
                            {

                                ars[x].VoidMode = true;
                                ars[x].JudgeType = Arrow.JudgementType.Hold;

                            }
                            ars[x].ResetColor(ars[x].ArrowColor);
                        }
                    });
                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "","","$0","",
                        "","","","",   "$0","","","",

                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "","","$0","",
                        "","","","",   "$0","","","",

                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "","","$0","",
                        "","","","",   "$0","","","",
                        //
                        "$0","","","",   "","","","",
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
            }


            public void ExtremePlus()
            {

            }
            public void Easy()
            {

            }
            public void Hard()
            {

                if (InBeat(0))
                {
                    RegisterFunctionOnce("Line", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(640, 0));
                        ce.Insert(BeatTime(16), CentreEasing.Linear(-2));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { DrawingColor = Microsoft.Xna.Framework.Color.DarkGreen };
                        CreateEntity(l);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(16), ValueEasing.Combine(ValueEasing.Stable(0.5f), ValueEasing.SinWave(1, BeatTime(2), -0.5f)));
                        ve.Run((s) => { l.Alpha = s; });
                        for (int a = 1; a < 16; a++) l.InsertRetention(new Line.RetentionEffect(BeatTime(2 * a)));

                    });
                    RegisterFunctionOnce("LineAnother", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(640 + BeatTime(1) * 2, 0));
                        ce.Insert(BeatTime(16), CentreEasing.Linear(-2));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { DrawingColor = Microsoft.Xna.Framework.Color.DarkGreen };
                        CreateEntity(l);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(16), ValueEasing.Combine(ValueEasing.Stable(0.2f), ValueEasing.SinWave(0.4f, BeatTime(2), -0.5f)));
                        ve.Run((s) => { l.Alpha = s; });
                        for (int a = 1; a < 8; a++) l.InsertRetention(new Line.RetentionEffect(BeatTime(2 * a)));

                    });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "$0(Line)(LineAnother)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R","","","",   "","","R","",
                        "","","","",   "R","","","",

                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","+0","",
                        "","","","",   "R","","","",

                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","+0","",
                        "","","","",   "R","","","",

                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "(R)","","","",   "","","+0","",
                        "","","","",   "R","","","",
                        //

                    });
                    BarrageCreate(BeatTime(4) * 4, BeatTime(1), 6, new string[]
                    {
                        "($0)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",
                        //
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",
                        //
                    });

                }
                if (InBeat(48))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(290, 240, 140);
                        DelayBeat(0.125f, () =>
                        {
                            for (int a = 0; a < 32 * 2 * 1.8f; a++)
                                CreateBone(new DownBone(true, 400 + 140 + 4 * a * BeatTime(0.125f), 4, 32) { MarkScore = false });
                        });
                    });
                    RegisterFunctionOnce("Platform1", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 + 155, 320));
                        ce.Insert(BeatTime(8), CentreEasing.Linear(-3f));
                        CreateEntity(new Platform(1, new(0, 0), ce.GetResult(), 0, 40));
                    });
                    RegisterFunctionOnce("Platform2", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320 + 164, 320 - 40));
                        ce.Insert(BeatTime(8), CentreEasing.Linear(-3f));
                        CreateEntity(new Platform(1, new(0, 0), ce.GetResult(), 0, 40));
                        for (int a = 0; a < 4; a++)
                        {
                            CreateEntity(new DownBone(true, 320 + 150 + a * 10, 3f, 70) { MissionLength = 70 });
                        }
                    });
                    RegisterFunctionOnce("DownBone1", () =>
                    {
                        CreateBone(new DownBone(true, 3, 80));
                    });
                    RegisterFunctionOnce("UpBone1", () =>
                    {
                        CreateBone(new UpBone(true, 3, 55));
                    });
                    RegisterFunctionOnce("BlueBone", () =>
                    {
                        CreateBone(new DownBone(true, 5, 140) { ColorType = 1 });
                    });
                    RegisterFunctionOnce("gs", () =>
                    {
                        TP();
                        SetGreenBox();
                        SetSoul(1);
                    });
                    RegisterFunctionOnce("WhiteOut", () => { WhiteOut(BeatTime(2f)); });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "BlueSoul(Platform1)","","","",   "","","","",
                        "","","","",   "DownBone1","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "UpBone1","","","",

                        "(Platform1)","","","",   "","","","",
                        "","","","",   "DownBone1","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "UpBone1","","","",

                        "(Platform1)","","","",   "","","","",
                        "","","","",   "DownBone1","","","",
                        "Platform2","","","",   "","","","",
                        "","","","",   "UpBone1","","","",

                        "(Platform1)","","","",   "","","","",
                        "","","","",   "DownBone1","","","",
                        "","","","",   "","","","",
                        "","","","",   "gs","","","",
                        //
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(WhiteOut)","","","",   "","","","",
                        "","","","",   "","","","",
                    });
                }
                if (InBeat(80))
                {
                    RegisterFunctionOnce("Box", () =>
                    {
                        HeartAttribute.JumpSpeed = 6.5f;
                        SetSoul(2);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 240));
                        ce.Insert(BeatTime(32) - 2, CentreEasing.YSinWave(30, BeatTime(8), 0));
                        ce.Run((s) => { InstantSetBox(s, 220, 140); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 20));
                        CreateBone(new DownBone(false, 6, 20));
                        CreateBone(new UpBone(true, 6, 70));
                        CreateBone(new UpBone(false, 6, 70));
                    });
                    RegisterFunctionOnce("ChangeWay", () =>
                    {
                        Heart.GiveForce(180, 3);
                        PlaySound(Sounds.Ding);
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 6, 20));
                        CreateBone(new UpBone(false, 6, 20));
                        CreateBone(new DownBone(true, 6, 70));
                        CreateBone(new DownBone(false, 6, 70));
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        Heart.GiveForce(0, 0);
                        SetBox(240, 84 * 2.2f, 84 * 1.75f);

                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 2 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 2 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Bone4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 1 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 1 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Move", () =>
                    {
                        SetSoul(2);
                        SetBox(new Vector2(320, 240), 220, 140);
                    });
                    RegisterFunctionOnce("Box2", () =>
                    {
                        HeartAttribute.JumpSpeed = 6.5f;
                        SetSoul(2);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 240));
                        ce.Insert(BeatTime(15) - 2, CentreEasing.YSinWave(30, BeatTime(8), 0));
                        ce.Run((s) => { SetBox(s, 220, 140); });
                    });
                    RegisterFunctionOnce("Back", () =>
                    {
                        SetSoul(1);
                        SetGreenBox();
                        TP();

                    });
                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "(Box)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "ChangeWay","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone2","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Change","","","",   "","","","",
                        //
                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "Move","","","",   "","","","",
                        //
                        "(Box2)","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "Back","","","",   "","","","",

                    });
                }
                if (InBeat(140))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(240, 240, 240);
                        DelayBeat(1, () =>
                        {
                            for (int a = 0; a < 18; a++) CreateBone(new SideCircleBone(a * 20, 2.5f, 50, BeatTime(30)));
                        });
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(0, 0));
                        ce.Insert(BeatTime(32), CentreEasing.YSinWave(30, BeatTime(4), 0));
                        ce.Insert(BeatTime(4), CentreEasing.EaseInSine(new(0, 0), new(-160, 0), BeatTime(4)));
                        Platform platform = new(0, new(320, 240), ce.GetResult(), 0, 40);
                        CreateEntity(platform);
                        Platform platform2 = new(0, new(320 + 2, 240 + 4), ce.GetResult(), 180, 40);
                        CreateEntity(platform2);
                        DelayBeat(64, () => { platform.Dispose(); platform2.Dispose(); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {

                        CreateBone(new DownBone(true, 6, 90));
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {

                        CreateBone(new UpBone(false, 6, 90));
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 6, 90));
                    });
                    RegisterFunctionOnce("Bone4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 6, 90));
                    });
                    RegisterFunctionOnce("ChangeWay", () =>
                    {
                        Heart.GiveForce(180, 0);
                    });
                    RegisterFunctionOnce("GreenSoul", () =>
                    {
                        SetSoul(1);
                        TP();
                        SetGreenBox();
                        Heart.GiveForce(0, 0);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
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
                        "BlueSoul","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",

                        "(Bone3)","","","",   "","","","",
                        "(Bone1)","","","",   "","","","",
                        "(Bone3)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "ChangeWay","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",

                        "(Bone4)","","","",   "","","","",
                        "(Bone2)","","","",   "","","","",
                        "(Bone4)","","","",   "","","","",
                        "(GreenSoul)","","","",   "","","","",
                        //
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                    });
                }
                if (InBeat(204))
                {
                    HeartAttribute.ArrowFixed = true;
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",
                        //

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
                if (InBeat(220))
                {
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    RegisterFunctionOnce("Change", () =>
                    {
                        Heart.GiveForce(0, 0);
                        SetBox(240, 84 * 2f, 84 * 1.75f);
                        TP();
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 1 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 1 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Bone4", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 2 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 2 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Move", () =>
                    {
                        SetSoul(1);
                        SetBox(new Vector2(320, 240), 84, 84);
                        TP(320, 240);
                        HeartAttribute.Gravity = 4f;
                    });
                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",
                        //                      
                        "Change","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone4","","","",   "","","","",
                        "Bone3","","","",   "","","","",

                        "Bone4","","","",   "","","","",
                        "","","","",   "","","","",
                        "Move","","","",   "","","","",
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
                if (InBeat(252))
                {
                    HeartAttribute.ArrowFixed = true;
                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });

                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",

                        "(R)(+01)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "+21","","+01","",
                        "(+01)(+0)","","","",   "","","(R)","",
                        "","","","",   "(R)","","","",
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
                if (InBeat(271))
                {
                    RegisterFunctionOnce("BlueSoul", () =>
                    {
                        SetSoul(2);
                        SetBox(240, 130, 640);
                        HeartAttribute.Gravity = 0f;
                        HeartAttribute.JumpTimeLimit = 0;
                        TP(320, 0);
                        DelayBeat(0.5f, () => { HeartAttribute.Gravity = 9.8f; });
                    });
                    RegisterFunctionOnce("Bones1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(true, 10, 40));
                        CreateBone(new RightBone(true, 10, 40));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 10, 40));
                            CreateBone(new RightBone(true, 10, 40));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 10, 40));
                            CreateBone(new RightBone(true, 10, 40));
                        });
                    });
                    RegisterFunctionOnce("ChanegWay1", () =>
                    {
                        Heart.GiveForce(270, 4);
                        Heart.JumpTimeLimit = 1;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("LeftBone1", () =>
                    {
                        CreateBone(new LeftBone(false, 11, 80));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("RightBone1", () =>
                    {
                        CreateBone(new RightBone(true, 6.5f, 80));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("ChanegWay2", () =>
                    {
                        Heart.GiveForce(90, 4);
                        Heart.JumpTimeLimit = 1;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("LeftBone2", () =>
                    {
                        CreateBone(new LeftBone(false, 12f, 80));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("RightBone2", () =>
                    {
                        CreateBone(new RightBone(false, 10f, 80));
                        PlaySound(Sounds.pierce);
                    });
                    RegisterFunctionOnce("ChanegWay3", () =>
                    {
                        Heart.GiveForce(180, 0);
                        Heart.JumpTimeLimit = 0;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("ChanegWay4", () =>
                    {
                        Heart.GiveForce(0, 0);
                        Heart.JumpTimeLimit = 0;
                        PlaySound(Sounds.Ding);

                    });
                    RegisterFunctionOnce("Bones2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(false, 10, 40));
                        CreateBone(new RightBone(false, 10, 40));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(false, 10, 40));
                            CreateBone(new RightBone(false, 10, 40));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(false, 10, 40));
                            CreateBone(new RightBone(false, 10, 40));
                        });
                    });
                    RegisterFunctionOnce("Bones3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new LeftBone(true, 640, 10, 35));
                        CreateBone(new RightBone(true, 640, 10, 35));
                        CreateBone(new LeftBone(true, 660, 10, 40));
                        CreateBone(new RightBone(true, 660, 10, 40));
                        DelayBeat(0.25f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 640, 10, 35));
                            CreateBone(new RightBone(true, 640, 10, 35));
                            CreateBone(new LeftBone(true, 660, 10, 40));
                            CreateBone(new RightBone(true, 660, 10, 40));
                        });
                        DelayBeat(0.75f, () =>
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new LeftBone(true, 640, 10, 35));
                            CreateBone(new RightBone(true, 640, 10, 35));
                            CreateBone(new LeftBone(true, 660, 10, 40));
                            CreateBone(new RightBone(true, 660, 10, 40));
                        });
                    });
                    RegisterFunctionOnce("SineBone", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(false, 7, 40);
                        RightBone b2 = new(false, 7, 40);
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("SineBone2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(true, 7, 40) { ColorType = 2 };
                        RightBone b2 = new(true, 7, 40) { ColorType = 2 };
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("SineBone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        LeftBone b1 = new(true, 7, 40) { };
                        RightBone b2 = new(true, 7, 40) { };
                        float a = 0;
                        ForBeat120(16, () =>
                        {
                            a += BeatTime(0.125f);
                            b1.MissionLength = 40 + Sin(a) * 40;
                            b2.MissionLength = 80 - b1.MissionLength;
                        });
                        CreateBone(b1);
                        CreateBone(b2);
                    });
                    RegisterFunctionOnce("BoxBack", () =>
                    {
                        InstantSetBox(240, 128, 490);
                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        PlaySound(Sounds.Ding);
                        SetSoul(1);
                        SetBox(240, 84, 84);
                        TP();
                        Heart.GiveForce(0, 0);
                    });

                    BarrageCreate(0, BeatTime(1), 6, new string[]
                    {
                        "","","","",   "BlueSoul","","","",

                        "Bones1","","","",   "","","","",
                        "","","","",   "","","","",
                        "ChanegWay1(LeftBone1)","","","",   "(LeftBone1)","","","",
                        "LeftBone1","","","",   "","","","",

                        "RightBone1(ChanegWay2)","","","",   "RightBone1","","","",
                        "RightBone1","","","",   "","","","",
                        "ChanegWay3(Bones2)","","","",   "","","","",
                        "","","Bones3","",   "","","","",

                        "ChanegWay4","","","",   "","","(SineBone2)","",
                        "(SineBone2)","","","",   "(SineBone2)","","","",
                        "ChanegWay3(SineBone2)","","(SineBone2)","",   "","","(SineBone2)","",
                        "BoxBack","","(SineBone2)","",   "","","(SineBone2)","",

                        "ChanegWay4(SineBone2)","","","",   "(SineBone2)","","(SineBone2)","",
                        "(SineBone2)","","","",   "(SineBone2)","","(SineBone2)","",
                        "ChanegWay3","","(SineBone2)","",   "(SineBone2)","","","",
                        "(SineBone2)","","","",   "(SineBone2)","","","",
                        //
                        "Return"
                    });
                }
                if (InBeat(284))
                {
                    RegisterFunctionOnce("Void", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].VoidMode = true;
                            ars[x].ResetColor(ars[x].ArrowColor);
                        }
                    });
                    RegisterFunctionOnce("ScaleIn", () =>
                    {
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(BeatTime(4), ValueEasing.EaseInQuint(0, 0.3f, BeatTime(4)));
                        e1.Insert(BeatTime(4), ValueEasing.EaseOutQuint(0.3f, 0, BeatTime(4)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {
                            ScreenDrawing.ScreenScale += s * 0.020f;
                        });
                    });
                    RegisterFunctionOnce("ScaleBack", () =>
                    {
                        Extends.DrawingUtil.LerpScreenScale(BeatTime(4), 1, 0.05f);
                    });
                    BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "R","","","",   "R","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "R","","","",   "","","","",

                        "R","","","",   "R","","","",
                        "R","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "R","","","",   "R","","","",

                        "R","","","",   "R","","","",
                        "R(ScaleIn)","","","",   "R","","","",
                        "","","","",   "R","","","",
                        "R","","","",   "","","","",
                        //
                        "Void($0)","","","",   "$0'0.9","","","",
                        "+0'0.8","","","",   "+0'0.7","","","",
                        "","","","",   "+0'0.6","","","",
                        "","","","",   "+0'0.5","","","",

                        "","","+0'0.5","",   "","","","",
                        "","","","",   "+0'0.5","","","",
                        "","","","",   "","","","",
                        "+0'0.5","","","",   "","","","",

                        "","","+0'0.4","",   "","","","",
                        "","","","",   "+0'0.4","","","",
                        "","","","",   "","","","",
                        "+0'0.3","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(ScaleBack)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                         "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",
                        //
                         "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                         "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","(R)(+01)","",
                        "","","","",   "(R)(+01)","","","",
                        //

                    });
                }
                if (InBeat(348))
                {

                    RegisterFunctionOnce("Split", () =>
                    {


                        TP(320, 240);
                        SetBox(new Vector2(320, 240), 84, 84);


                    });
                    RegisterFunctionOnce("Return", () =>
                    {

                        SetBox(new Vector2(320, 240), 220, 140);
                        TP(320, 80);
                        SetSoul(2);
                        Heart.JumpSpeed = 6.5f;
                        Heart.JumpTimeLimit = 2;
                        ;
                    });

                    RegisterFunctionOnce("Tap", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            if (ars[x].RotateType == 2) ars[x].JudgeType = Arrow.JudgementType.Tap;
                        }
                    });
                    RegisterFunctionOnce("Box", () =>
                    {
                        HeartAttribute.JumpSpeed = 6.5f;
                        SetSoul(2);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 240));
                        ce.Insert(BeatTime(32) - 2, CentreEasing.YSinWave(30, BeatTime(8), 0));
                        ce.Run((s) => { InstantSetBox(s, 220, 140); });
                    });
                    RegisterFunctionOnce("Bone1", () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 6, 20));
                        CreateBone(new DownBone(false, 6, 20));
                        CreateBone(new UpBone(true, 6, 70));
                        CreateBone(new UpBone(false, 6, 70));
                    });



                    RegisterFunctionOnce("Change", () =>
                    {
                        BoxStates.BoxMovingScale = 0.3f;
                        Heart.GiveForce(0, 0);
                        SetBox(new Vector2(320, 240), 170f, 84 * 1.75f);
                        TP(new Vector2(320, 240));
                        SetSoul(0);
                    });
                    RegisterFunctionOnce("Bone2", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 1 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 1 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Bone3", () =>
                    {
                        PlaySound(Sounds.pierce);
                        UpBone b1 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b2 = new(true, 320 + 84 * 1.1f, 0, 84 * 0.4f) { ColorType = 0 };
                        UpBone b3 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b4 = new(true, 320 + 84 * 1.1f + 30, 0, 84 * 0.4f) { ColorType = 0 };
                        DownBone b5 = new(true, 320 + 84 * 1.1f + 10, 0, 84 * 2.2f) { ColorType = 2 };
                        DownBone b6 = new(true, 320 + 84 * 1.1f + 20, 0, 84 * 2.2f) { ColorType = 2 };
                        CreateBone(b2);
                        CreateBone(b1);
                        CreateBone(b3);
                        CreateBone(b4);
                        CreateBone(b5);
                        CreateBone(b6);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 4, BeatTime(2)));
                        ve.Run((s) => { b1.Speed = b2.Speed = b3.Speed = b4.Speed = b5.Speed = b6.Speed = s; });


                    });
                    RegisterFunctionOnce("Back", () =>
                    {
                        SetSoul(1);
                        SetGreenBox();
                        TP();

                    });
                    RegisterFunctionOnce("Over", () =>
                    {
                        ValueEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(2), ValueEasing.EaseInQuad(0, 360, BeatTime(2)));
                        ce.Run((s) =>
                        {
                            InstantSetBox(240 + s, 170, 84 * 1.75f);
                        });

                    });
                    RegisterFunctionOnce("HeartUp", () =>
                    {
                        SetSoul(1);
                        ValueEasing.EaseBuilder ce = new();
                        ce.Insert(0, ValueEasing.Stable(600));
                        ce.Insert(BeatTime(2.5f), ValueEasing.EaseOutCubic(600, 240, BeatTime(2.5f)));
                        ce.Run((s) => { InstantSetBox(new Vector2(320, s), 84, 84); InstantTP(320, s); });
                    });

                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",
                        //
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R","","","",

                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","","",
                        "(R)(+01)","","","",   "","","R","",
                        "(+01)","","","",   "R(Return)","","","",
                        //
                        "Box","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",

                        "Bone1","","","",   "","","","",
                        "","","","",   "","","","",
                        "Bone1","","","",   "","","","",
                        "Change","","","",   "","","","",
                        //
                        "Bone3","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone2","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone2","","","",   "","","","",

                        "Bone3","","","",   "","","","",
                        "Bone2","","","",   "","","","",
                        "Bone3","","","",   "","","","",
                        "Bone2","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(Over)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "HeartUp"
                    });


                }
                if (InBeat(412))
                {
                    SetPlayerBoxMission(0);
                    RegisterFunctionOnce("VoidHold", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            if (ars[a].ArrowColor == 0)
                            {

                                ars[x].VoidMode = true;
                                ars[x].JudgeType = Arrow.JudgementType.Hold;

                            }
                            ars[x].ResetColor(ars[x].ArrowColor);
                        }
                    });
                    BarrageCreate(BeatTime(3), BeatTime(1), 6, new string[]
                    {
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "","","$0","",
                        "","","","",   "$0","","","",

                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "","","$0","",
                        "","","","",   "$0","","","",

                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "+0","","+0","",
                        "$0","","","",   "","","$0","",
                        "","","","",   "$0","","","",
                        //
                        "$0","","","",   "","","","",
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

            }

            public void Start()
            {
                AdvanceFunctions.Interactive.AddEndEvent(() => { });
                SetSoul(1);
                SetBox(240, 84, 84);
                TP();
                HeartAttribute.MaxHP = 20;
                HeartAttribute.DamageTaken = 3;
                ScreenDrawing.HPBar.HPExistColor = Color.Yellow;
                GametimeDelta = -5f;
                //    GametimeDelta = BeatTime(140) - 8f;
                //   PlayOffset = BeatTime(140) ;
                if (AnomalyAvailable()) TryGenerateAnomaly();
            }

            bool AnomalyAvailable()
            {
                return PlayerManager.CurrentUser != null
&& DateTime.Now.Day == 18 && !PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("reTranscendence");
            }
            void AnomalyIntro()
            {
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Black * 0.6f);
                });
                RegisterFunctionOnce("Fade", () =>
                {
                    ScreenDrawing.SceneOut(Color.Black, BeatTime(11));
                    ResetBarrage();
                });
                RegisterFunctionOnce("Reset", () =>
                {
                    GameStates.ResetScene(new FacingCharactor());
                });
                string[] rhythm = {
                            "Flicker", "", "Flicker", "", "Flicker", "", "Flicker", "Flicker",
                            "Fade", "", "", "", "", "", "", "",
                            "Reset", "", "", "", "", "", "", "",
                        };
                BarrageCreate(0, BeatTime(1), 0, rhythm);
            }
            void TryGenerateAnomaly()
            {
                if (((GameStates.CurrentScene as SongFightingScene).Mode & GameMode.Buffed) == GameMode.Buffed)
                    DelayBeat(44, () =>
                    {
                        if (HeartAttribute.HP <= 1f)
                        {
                            AnomalyIntro();
                        }
                    });
                if (CurrentDifficulty == Difficulty.Extreme)
                {
                    AdvanceFunctions.Interactive.AddEndEvent(AnomalyIntro);
                    AutoEnd = false;
                }
            }
        }
    }
}