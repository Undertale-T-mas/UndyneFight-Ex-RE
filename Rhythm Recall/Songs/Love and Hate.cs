using Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class LoveAndHate : WaveConstructor, IWaveSet
    {
        public LoveAndHate() : base(62.5f / (240f / 60f))
        {

        }
        public string Music => "Love and Hate";

        public string FightName => "Love and Hate";
        private class ThisInformation : SongInformation
        {
            public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 18.9f),
                    }
                );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 18.9f),
                    }
                );
            public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 20.9f),
                    }
                );
            public override string BarrageAuthor => "T-mas & TK";
            public override string AttributeAuthor => "T-mas & TK";
            public override string SongAuthor => "?";
            public override string PaintAuthor => "?";
        }
        public SongInformation Attributes => new ThisInformation();
        public static LoveAndHate game;

        public void ExtremePlus()
        {

        }
        public void Hard()
        {
            if (Gametime < 0) return;
        }

        public void Noob()
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
        }
        public void Extreme()
        {
            if (Gametime < 0) return;
            if (InBeat(0))
            {
                RegisterFunctionOnce("Summon", () =>
                {
                    PlaySound(Sounds.pierce);
                    float l = 320 - 128;
                    float r = 320 + 128;

                    int count = 12;
                    List<DownBone> bones2 = new();
                    for (int i = 0; i <= count; i++)
                    {
                        float x = MathHelper.Lerp(l, 320, i * 1.0f / count);
                        DelayBeat(i / 16f, () =>
                        {
                            DownBone temp;
                            CreateBone(temp = new DownBone(false, x, 0, 20));
                            temp.LengthLerpScale = 0.02f;
                            bones2.Add(temp);
                        });
                        float x2 = MathHelper.Lerp(r, 320, i * 1.0f / count);
                        DelayBeat(i / 16f, () =>
                        {
                            DownBone temp;
                            CreateBone(temp = new DownBone(false, x2, 0, 20));
                            temp.LengthLerpScale = 0.02f;
                            bones2.Add(temp);
                        });
                    }

                    DelayBeat(24, () =>
                    {
                        foreach (DownBone temp in bones2)
                        {
                            temp.MissionLength = 140f;
                            //temp.ColorType = 2;  hard才转颜色
                            temp.Alpha = 0.5f;
                            temp.LengthLerpScale = 0.02f;
                            ForBeat(12, () =>
                            {
                                temp.Speed += 0.06f;
                                if (temp.Alpha < 0.96f)
                                    temp.Alpha += 0.009f;
                            });
                        }
                    });
                    var len1 = Rand(15, 30);
                    var len2 = Rand(15, 30);
                    SideBone[] bones = new SideBone[] {
                        new UpBone(false, l - 12, 6, len1),
                        new DownBone(false, l - 12, 6, 95 - len1),
                        new UpBone(true, r + 12, 6, len2),
                        new DownBone(true, r + 12, 6, 95 - len2),
                    };
                    float speed = 0;
                    ValueEasing.EaseBuilder builder = new();
                    builder.Adjust = false;
                    builder.Insert(BeatTime(4), ValueEasing.EaseOutQuint(6, 0, BeatTime(4)));
                    builder.Insert(BeatTime(4), ValueEasing.Stable(0));
                    builder.Insert(BeatTime(1), ValueEasing.Stable(8));
                    builder.Run(s =>
                    {
                        speed = s;
                        foreach (SideBone bone in bones)
                        {
                            bone.Speed = speed;
                        }
                    });

                    foreach (SideBone bone in bones)
                    {
                        CreateEntity(bone);
                    }
                });
                RegisterFunctionOnce("Blue", () =>
                {
                    SetSoul(2);
                    Heart.GiveForce(180, 6);
                    CreateEntity(new Boneslab(180, 20, 10, 10));

                    CentreEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(8), CentreEasing.EaseOutBack(new Vector2(500, 340), new Vector2(320, 340), BeatTime(8)));
                    builder.Insert(BeatTime(2), CentreEasing.EaseOutCubic(new Vector2(320, 340), new Vector2(260, 340), BeatTime(2)));
                    builder.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new Vector2(260, 340), new Vector2(380, 340), BeatTime(2)));
                    builder.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new Vector2(380, 340), new Vector2(260, 340), BeatTime(2)));
                    builder.Insert(BeatTime(2), CentreEasing.EaseOutCubic(new Vector2(260, 340), new Vector2(320, 340), BeatTime(2)));
                    builder.Insert(BeatTime(8), CentreEasing.Combine(
                         CentreEasing.Intensify(CentreEasing.SinWave(11, 3, 0, 0), ValueEasing.EaseOutCubic(1f, 0f, BeatTime(8)))
                        , CentreEasing.Accerlating(new Vector2(RandSignal() * Rand(2.0f, 3.6f) * 0.7f, -6f), new Vector2(0, 0.25f))));

                    ValueEasing.EaseBuilder builder1 = new();
                    builder1.Insert(BeatTime(8), ValueEasing.EaseOutCubic(32, 0, BeatTime(8)));
                    builder1.Insert(BeatTime(2), ValueEasing.EaseOutCubic(0, 4, BeatTime(2)));
                    builder1.Insert(BeatTime(2), ValueEasing.EaseOutCubic(4, -4, BeatTime(2)));
                    builder1.Insert(BeatTime(2), ValueEasing.EaseOutCubic(-4, 4, BeatTime(2)));
                    builder1.Insert(BeatTime(2), ValueEasing.EaseOutCubic(4, 0, BeatTime(2)));
                    builder1.Insert(BeatTime(8), ValueEasing.Linear(4, RandSignal() * Rand(4f, 6f) * 50f, BeatTime(8)));

                    Platform platform = new(0, Vector2.Zero,
                        builder.GetResult(), Motions.LengthRoute.autoFold, builder1.GetResult()
                        )
                    { LengthRouteParam = new float[] { 45, BeatTime(24) } };
                    CreateEntity(platform);

                    PlaySound(Sounds.pierce);

                    DelayBeat(4, () =>
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 7, 60));
                        CreateBone(new UpBone(true, 7, 60));
                    });

                    Line line = new(260f, 90);
                    ValueEasing.EaseBuilder builder2 = new();
                    builder2.Insert(BeatTime(4), ValueEasing.EaseInSine(0, 0.5f, BeatTime(4)));
                    builder2.Insert(BeatTime(4), ValueEasing.Alternate(2f, ValueEasing.Stable(0), ValueEasing.Linear(0.5f, 0.0f, BeatTime(4))));
                    builder2.Insert(BeatTime(4), ValueEasing.EaseOutSine(0.5f, 0.0f, BeatTime(4)));
                    builder2.Adjust = false;
                    line.DrawingColor = Color.Aqua;
                    builder2.Run(s => line.Alpha = s);
                    CreateEntity(line);
                    DelayBeat(12, () =>
                    {
                        line.Dispose();
                    });
                });
                RegisterFunctionOnce("Trans", () =>
                {
                    Heart.GiveForce(0, 8);
                });
                RegisterFunctionOnce("Trans2", () =>
                {
                    SetSoul(0);
                    SetBox(300, 146, 146);
                    PlaySound(Sounds.Ding);
                });
                RegisterFunctionOnce("Summon2", () =>
                {
                    for (int i = 0; i < 2; i++)
                    {
                        bool state = i == 1;
                        float v = i == 0 ? 1 : -1;
                        CreateBone(new UpBone(state, 320 - v * 150, 3.2f, 40));
                        CreateBone(new UpBone(state, 320 - v * 274, 3.5f, 40));
                        CreateBone(new DownBone(state, 320 - v * 150, 3.2f, 47));
                        CreateBone(new DownBone(state, 320 - v * 274, 3.5f, 47));
                    }
                    CreateGB(new NormalGB(new(260, 100), Vector2.Zero, new(1, 1.3f), 90, BeatTime(8.2f), BeatTime(1)));
                    CreateGB(new NormalGB(new(380, 100), new(640, 0), new(1, 1.3f), 90, BeatTime(8.2f), BeatTime(1)));
                });
                RegisterFunctionOnce("Zoom", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(3.5f, BeatTime(2));
                });
                RegisterFunctionOnce("Line", () =>
                {
                    Line line = new(Rand(100, 540), 90);
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(4), ValueEasing.EaseInSine(1.0f, 0.0f, BeatTime(4)));
                    builder.Adjust = false;
                    line.DrawingColor = Color.Aqua;
                    builder.Run(s => line.Alpha = s);
                    CreateEntity(line);
                    DelayBeat(4, () =>
                    {
                        line.Dispose();
                    });
                });
                RegisterFunctionOnce("Line2", () =>
                {
                    CentreEasing.EaseBuilder builder2 = new();
                    builder2.Insert(BeatTime(2), CentreEasing.EaseInQuad(new(0, 320), new(660, 320), BeatTime(2)));
                    ValueEasing.EaseBuilder builder3 = new();
                    builder3.Insert(BeatTime(2), ValueEasing.EaseOutSine(90, 270, BeatTime(2)));
                    Line line = new(builder2.GetResult(), builder3.GetResult());
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(4), ValueEasing.EaseInSine(1.0f, 0.0f, BeatTime(4)));
                    builder.Adjust = false;
                    line.DrawingColor = Color.Aqua;
                    builder.Run(s => line.Alpha = s);
                    line.ObliqueMirror = true;
                    CreateEntity(line);
                    DelayBeat(4, () =>
                    {
                        line.Dispose();
                    });
                });
                BarrageCreate(0, BeatTime(4), 1, new string[]{
                    "Summon(Line)", "", "Line", "Line",  "", "", "Line", "",
                    "Line", "", "Line", "Line",  "", "", "Line2", "",
                    "Blue(Zoom)", "", "", "",  "", "", "", "",
                    "", "", "", "",  "", "", "", "",
                    "Trans(Summon2)(Zoom)", "", "", "",  "", "", "", "",
                    "", "", "", "",  "", "", "", "",
                    "Trans2", "", "", "",  "", "", "", "",
                });
            }
            if (InBeat(30))
            {
                RegisterFunctionOnce("ToGreen", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                RegisterFunctionOnce("Red", () =>
                {
                    SetSoul(0);
                    SetBox(320, 140, 140);
                    TP(350, 350);
                });
                BarrageCreate(0, BeatTime(2), 6, new string[]{
                    "ToGreen", "", "", "",  "", "", "", "",

                    "D(D1)", "", "", "",   "D", "", "D", "",
                    "", "", "", "",   "D(D1)", "", "", "",
                    "D(D1)", "", "", "",   "D1", "", "D1", "",
                    "", "", "", "",   "D(D1)", "", "", "",
                    "D", "", "D", "",   "D", "", "D", "",
                    "D", "", "D", "",   "D", "", "", "",
                    "D1", "", "D1", "",   "D1", "", "D1", "",
                    "D1", "", "D1", "",   "D1", "", "", "",
                    "D", "", "D", "",   "D", "", "D", "",
                    "D", "", "D", "",   "D", "", "D", "",
                    "D", "", "D", "",   "D", "", "D", "",
                    "D", "", "D", "+11",   "+1", "", "", "",
                    "(D)(+01)", "", "(D)(+01)", "",   "(D)(+01)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "",   "(D)(+01)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "",   "(D)(+01)", "", "(D)(+01)", "",
                    "(D)(+01)", "", "(D)(+01)", "",   "(D)(+01)", "", "(D)(+01)", "",
                    "Red"
                });
            }
            if (InBeat(65))
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        CustomBone SideBone = new(new Vector2(320, 320), Motions.PositionRoute.circle, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                        {
                            PositionRouteParam = new float[] { 70, 1.8f, i * 90 },
                            LengthRouteParam = new float[] { 70 + k * 30, BeatTime(48) },
                            RotationRouteParam = new float[] { 2 * (((i + k) % 2) == 0 ? 2 : -2), k * 90 },
                            AlphaIncrease = true,
                            ColorType = k * 2,
                        };
                        DelayBeat(30, () =>
                        {
                            PlaySound(Sounds.Ding);
                        });
                        CreateBone(SideBone);
                    }
                    if ((i % 2) == 0)
                    {
                        CentreCircleBone bone = new(i * 45, 2, 0, 720)
                        {
                            MissionLength = 30,
                        };
                        DelayBeat(30, () =>
                        {
                            bone.MissionLength = 140 * 1.41f;
                        });
                        CreateBone(bone);
                    }
                }
            }
            if (InBeat(65, 95) && At0thBeat(4))
            {
                CreateEntity(new Boneslab(LastRand * 90, 40, BeatTime(2), BeatTime(1)));
            }
            if (InBeat(67))
            {
                CreateGB(new NormalGB(new(140, 200), new(140, 0), new(1, 1), MathUtil.Direction(new(140, 200), new(320, 320)), BeatTime(61), 20));
                CreateGB(new NormalGB(new(500, 200), new(500, 0), new(1, 1), MathUtil.Direction(new(500, 200), new(320, 320)), BeatTime(61), 20));
            }
            if (InBeat(73))
            {
                CreateGB(new NormalGB(new(320, 170), new(320, 480), new(1, 1), 90, BeatTime(63), 20));
                CreateGB(new NormalGB(new(250, 170), new(250, 0), new(1, 1), 90, BeatTime(71), 20));
                CreateGB(new NormalGB(new(390, 170), new(390, 0), new(1, 1), 90, BeatTime(71), 20));
            }
            if (InBeat(95))
            {
                SetSoul(0);
                Heart.RotateTo(0);
            }
            if (InBeat(102))
            {
                DrawingUtil.BetterBlackScreen(BeatTime(10), BeatTime(15), BeatTime(3), Color.Black);
            }
            if (InBeat(130, 158))
            {
                if (AtKthBeat(2, 0))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new UpBone(true, 390, 5, 65));
                    CreateBone(new CustomBone(new Vector2(455, 320), Motions.PositionRoute.linear, 90, 25)
                    {
                        PositionRouteParam = new float[] { -5, 0 },
                    }); ;
                }
                if (AtKthBeat(2, 1))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 465, 5, 65));
                }
            }
            if (InBeat(160, 188))
            {
                if (AtKthBeat(2, 1))
                {
                    LeftBone bone = new(true, 315, 0, 140)
                    {
                        LengthLerpScale = 0.16f,
                    };
                    DelayBeat(2, () =>
                    {
                        bone.IsMasked = false;
                        bone.ColorType = Rand(1, 2);
                    });
                    DelayBeat(4, () =>
                    {
                        bone.Speed = 3;
                        bone.IsMasked = true;
                        PlaySound(Sounds.pierce);
                    });
                    CreateBone(bone);
                }
                if (AtKthBeat(2, 0))
                {
                    RightBone bone2 = new(false, 325, 0, 140)
                    {
                        LengthLerpScale = 0.16f,
                    };
                    DelayBeat(2, () =>
                    {
                        bone2.IsMasked = false;
                        bone2.ColorType = Rand(1, 2);
                        PlaySound(Sounds.Ding);
                    });
                    DelayBeat(4, () =>
                    {
                        bone2.Speed = 3;
                        bone2.IsMasked = true;
                        PlaySound(Sounds.pierce);
                    });
                    CreateBone(bone2);
                }
            }
            if (InBeat(190))
            {
                RegisterFunctionOnce("ToGreen", () =>
                {
                    foreach (var b in GetAll<LeftBone>())
                    {
                        b.IsMasked = true;
                    }
                    foreach (var b in GetAll<RightBone>())
                    {
                        b.IsMasked = true;
                    }
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                BarrageCreate(0, BeatTime(2), 6, new string[]{
                    "ToGreen", "", "", "",  "", "", "", "",

                    "D(D1)", "", "", "",  "", "", "", "",
                    "D(D1)", "", "", "",    "D", "", "D", "",
                    "D", "", "D", "",    "D", "", "D", "",
                    "D", "", "D", "",    "D", "", "D", "",
                    "D", "", "D", "+01",    "D", "", "D", "",
                    "D", "", "D", "+01",    "D", "", "D", "",
                    "D(D1)", "", "", "",    "(D)(D1)", "", "", "",
                    "D(D1)", "", "", "",    "(D)(D1)", "", "", "",

                    "(D)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "", "",
                    "(D)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(#0.1#D)(#0.1#+21)", "", "", "",      "(#0.1#+1)(#0.1#+21)", "", "", "",
                    "(#0.1#+1)(#0.1#+21)", "", "", "",      "(#0.1#+1)(#0.1#+21)", "", "", "",
                    "(<$0)(>$2)(#0.1#$31)", "", "", "",      "(>$00)(<$20)(#0.1#$31)", "", "", "",
                    "(<$01)(>$21)(#0.1#$10)", "", "", "",      "(>$01)(<$21)(#0.1#$10)", "", "", "",
                });
            }

            if (InBeat(222))
            {
                RegisterFunctionOnce("ToGreen", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                BarrageCreate(0, BeatTime(2), 6, new string[]{
                    "ToGreen", "", "", "",  "", "", "", "",

                    "(D)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "($0)(+0)", "+01", "$1", "+01",    "($2)(+0)", "+01", "$3", "+01",
                    "($0)(+0)", "+01", "$1", "+01",    "($2)(+0)", "+01", "$3", "+01",
                    "($0)(+0)", "+01", "$2", "+01",    "($0)(+0)", "+01", "$2", "+01",
                    "($1)(+0)", "+01", "$3", "+01",    "($0)(+0)", "+01", "$2", "+01",
                    "(+0)(+01)", "", "+11", "",      "(+1)(+01)", "", "+11", "",
                    "(+1)(+01)", "", "+11", "",      "(+1)(+01)", "", "+11", "",

                    "(#0.1#D)(#0.1#+21)", "", "", "",      "(#0.1#+1)(#0.1#+21)", "", "", "",
                    "(#0.1#+1)(#0.1#+21)", "", "", "",      "(#0.1#+1)(#0.1#+21)", "", "", "",
                    "(#0.1#-1)(#0.1#+21)", "", "", "",      "(#0.1#-1)(#0.1#+21)", "", "", "",
                    "(#0.1#-1)(#0.1#+21)", "", "", "",      "(#0.1#-1)(#0.1#+21)", "", "", "",
                    "(D)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "($1)(+0)", "+01", "$3", "+01",    "($0)(+0)", "+01", "$2", "+01",
                    "($1)(+0)", "+01", "$3", "+01",    "($0)(+0)", "+01", "$2", "+01",
                });
            }

            if (InBeat(254))
            {
                RegisterFunctionOnce("ToGreen", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                BarrageCreate(0, BeatTime(2), 6, new string[]{
                    "ToGreen", "", "", "",  "", "", "", "",

                    "(D)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(#1.4#$0)(#1.4#+21)", "", "", "",      "(>+201'0.7)(<+211'0.7)", "", "", "",
                    "(#1.4#+1)(#1.4#+21)", "", "", "",      "(<+201'0.7)(>+211'0.7)", "", "", "",
                    "(#1.4#+1)(#1.4#+21)", "", "", "",      "(>+201'0.7)(>+211'0.7)", "", "", "",
                    "(#1.4#+1)(#1.4#+21)", "", "", "",      "(<+201'0.7)(>+211'0.7)", "", "", "",

                    "D(D1)", "", "", "",  "(D)(D1)", "", "", "",
                    "D(D1)", "", "", "",  "(D)(D1)", "", "", "",
                    "(D)(+01)", "", "+1", "",  "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "+01",  "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",  "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "+01",  "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",  "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",  "(+1)(+01)", "", "+1", "",
                });
            }
            if (InBeat(286))
            {
                RegisterFunctionOnce("ToGreen", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                BarrageCreate(0, BeatTime(2), 6, new string[]{
                    "ToGreen", "", "", "",  "", "", "", "",

                    "(D)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "($1)(+0)", "+01", "$3", "+01",    "($0)(+0)", "+01", "$2", "+01",
                    "($1)(+0)", "+01", "$3", "+01",    "($0)(+0)", "+01", "$2", "+01",
                    "($3)(+0)", "+01", "$1", "+01",    "($2)(+0)", "+01", "$0", "+01",
                    "($3)(+0)", "+01", "$1", "+01",    "($2)(+0)", "+01", "$0", "+01",

                    "(D)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "", "+1", "",    "(+1)(+01)", "", "+1", "",
                    "(+1)(+01)", "+10", "+01", "",    "(+1)(+01)", "", "+11", "",
                    "($0)(+0)", "+01", "$2", "+01",    "($0)(+0)", "+01", "$2", "+01",
                    "($3)(+0)", "+01", "$1", "+01",    "($2)(+0)", "", "", "",
                });
            }
            if (InBeat(318))
            {
                RegisterFunctionOnce("ToGreen", () =>
                {
                    SetGreenBox();
                    SetSoul(1);
                    TP();
                });
                BarrageCreate(0, BeatTime(2), 6, new string[]{
                    "ToGreen", "", "", "",  "", "", "", "",

                });
            }
        }
        public void Start()
        {
            game = this;
            GametimeDelta = -0.6f;
            SetSoul(0);
            SetBox(new Vector2(320, 310), 256, 136);
            TP(new Vector2(320, 240));
            HeartAttribute.Gravity = 7.0f;
            HeartAttribute.JumpSpeed = 5.2f;
            HeartAttribute.SoftFalling = true;
            HeartAttribute.MaxHP = 8;
            bool delayEnable = false;
            if (delayEnable)
            {
                float delay = BeatTime(256 - 3);
                delay = BeatTime(10 + 366);
                PlayOffset = delay + 3.9f;
                GametimeDelta += delay;
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(new Vector2(320, 240));
            }
            else
            {
                PlayOffset = 0;
            }
            //GametimeDelta = -0.6f + BeatTime(159);
            GametimeDelta = -0.6f;
            //PlayOffset = BeatTime(159);
            //SetBox(320, 140, 140);
        }
    }

}