using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using static Extends.Someway;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class Marisa : IChampionShip
    {
        public Marisa()
        {

            difficulties = new()
            {
                { "div.2", Difficulty.Noob },
                { "div.1", Difficulty.Extreme }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            private class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => (GameStates.difficulty == 4) ? "TK" : "T-mas (Extended by TK)";
                public override string SongAuthor => "Touhou: IN";
                public override string PaintAuthor => "axpathetic";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 0f),
                            new(Difficulty.Hard, 0f),
                            new(Difficulty.Noob, 0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 0f),
                            new(Difficulty.Hard, 0f),
                            new(Difficulty.Noob, 0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 0f),
                            new(Difficulty.Hard, 0f),
                            new(Difficulty.Noob, 0f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(5.46f) { }

            public void Start()
            {
                GametimeDelta = BeatTime(-8);
                //GametimeDelta = BeatTime(1090);
                PlayOffset = GametimeDelta + BeatTime(8);
                SetBox(290, 180, 170);
                SetSoul(2);
                if (GameStates.difficulty == 4)
                {
                    ScreenDrawing.HPBar.HPExistColor = Color.LightPink;
                    ScreenDrawing.HPBar.HPLoseColor = Color.HotPink;
                    HeartAttribute.MaxHP = 8;
                    HeartAttribute.HP = 8;
                    SetSoul(0);
                    InstantSetBox(new Vector2(320, 320), 140, 140);
                    InstantTP(320, 320);
                    //GametimeDelta = 4799;
                    //GametimeDelta = 8029;
                    GametimeDelta = 0;
                    PlayOffset = GametimeDelta;
                }
            }

            public void Noob()
            {
                #region Intro Blue
                if (InBeat(0.2f))
                {
                    CreatePlatform(new Platform(1, new Vector2(250, 290), Motions.PositionRoute.XAxisSin, 90, 45, BeatTime(128 - 8))
                    {
                        createWithScaling = true,
                        PositionRouteParam = new float[] { 0, 56, BeatTime(32), 0 },

                    });
                    CreatePlatform(new Platform(1, new Vector2(390, 290), Motions.PositionRoute.XAxisSin, 270, 45, BeatTime(128 - 8))
                    {
                        createWithScaling = true,
                        PositionRouteParam = new float[] { 0, 56, BeatTime(32), 0 },
                    });
                }
                if (InBeat(0.2f, 128f - 4) && At0thBeat(1))
                {
                    CreateBone(new LeftBone(false, 5f, 10) { MarkScore = false });
                    CreateBone(new RightBone(true, 5f, 10) { MarkScore = false });
                }
                if (InBeat(0.2f, 128f - 4) && At0thBeat(8))
                {
                    CreateBone(new LeftBone(false, 5f, 10) { MarkScore = false });
                    CreateBone(new RightBone(true, 5f, 10) { MarkScore = true });
                }
                if (InBeat(0.2f, 128f - 4) && AtKthBeat(16, 0))
                {
                    Heart.GiveForce(90, 4);
                }
                if (InBeat(0.2f, 128f - 4) && AtKthBeat(16, BeatTime(8)))
                {
                    Heart.GiveForce(270, 4);
                }
                if (InBeat(124))
                {
                    HeartAttribute.Gravity = 6.88f;
                    HeartAttribute.JumpSpeed = 5.3f;
                    Heart.GiveForce(0, 4);
                    SetBox(290, 200, 150);
                }
                if (InBeat(125, 256 - 4) && At0thBeat(8f))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(false, 4, 20));
                    CreateBone(new DownBone(true, 4, 20));
                    CreateBone(new UpBone(false, 4, 94));
                    CreateBone(new UpBone(true, 4, 94));
                }
                if (InBeat(256, 384 - 8) && At0thBeat(8f))
                {
                    CreatePlatform(new Platform(1, new Vector2(200, 294), Motions.PositionRoute.XAxisSin, Motions.LengthRoute.stableValue, Motions.RotationRoute.sin)
                    {
                        createWithScaling = true,
                        PositionRouteParam = new float[] { 2.4f, 36, BeatTime(32), Gametime * 1.5f },
                        LengthRouteParam = new float[] { 36 },
                        RotationRouteParam = new float[] { 30, BeatTime(32), Gametime, 0 }
                    });
                }

                if (InBeat(256 + 4, 384 - 4) && At0thBeat(1))
                {
                    CreateBone(new DownBone(false, 5f, 10) { MarkScore = false });
                }
                #endregion
                #region Purple
                if (InBeat(384))
                {
                    SetBox(290, 240, 150);
                    HeartAttribute.PurpleLineCount = 5;
                    SetSoul(4);
                }
                if (InBeat(384 + 7, 512 - 7) && At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new CustomBone(new Vector2(195, 290 + Rand(-2, 2) * 25), Motions.PositionRoute.linear, 0, 14)
                    {
                        PositionRouteParam = new float[] { 3, 0 }
                    });
                }
                if (InBeat(384 + 7, 512 - 7) && AtKthBeat(8, BeatTime(4)))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new CustomBone(new Vector2(445, 290 + Rand(-2, 2) * 25), Motions.PositionRoute.linear, 0, 14)
                    {
                        PositionRouteParam = new float[] { -3, 0 }
                    });
                }
                #endregion
                #region Green 1
                if (InBeat(512 - 4))
                {
                    SetSoul(1);
                    TP();
                    SetGreenBox();
                }
                if (InBeat(504))
                {
                    int way = 0, color = 0;
                    float time = BeatTime(8 + 2.73f);
                    Fortimes(12, () =>
                    {
                        way ^= 1;
                        color ^= 1;
                        Fortimes(8, () =>
                        {
                            CreateArrow(time, way, 7.0f, color, 0);
                            time += BeatTime(2.013f);
                            CreateArrow(time, way + 2, 7.0f, color, 0);
                            time += BeatTime(2.013f);
                        });
                    });
                }
                #endregion
                #region Orange
                if (InBeat(909))
                {
                    SetBox(290, 200, 160);
                    SetSoul(2);
                    Heart.IsOranged = true;
                }
                if (InBeat(910, 1100) && At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 5, 20));
                    CreateBone(new DownBone(false, 5, 20));
                }
                #endregion
                #region Green 2
                if (InBeat(1100))
                {
                    SetSoul(1);
                    TP();
                    SetGreenBox();
                }
                if (InBeat(1096))
                {
                    int way = 0, color = 0;
                    float time = BeatTime(8 + 2.73f);
                    Fortimes(11, () =>
                    {
                        way ^= 1;
                        color ^= 1;
                        Fortimes(8, () =>
                        {
                            CreateArrow(time, way, 7.0f, color, 0);
                            time += BeatTime(2.013f);
                            CreateArrow(time, way + 2, 7.0f, color, 0);
                            time += BeatTime(2.013f);
                        });
                    });
                }
                #endregion
            }

            public void Easy() { }

            public void Normal() { }

            public void Hard() { }

            #region Functions
            private static float Abs(float num)
            {
                num = num > 0 ? num : -num;
                return num;
            }
            private void Shake(float Amount, float Decrease, bool Blur = false)
            {
                ShakeIntensity = Amount;
                ShakeDecrease = Decrease;
                ShakeBlur = Blur;
            }
            private void ShakeStep()
            {
                ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Blur
                {
                    //Sigma = (HeartAttribute.MaxHP - HeartAttribute.HP) / HeartAttribute.MaxHP * 6
                    Sigma = 0
                });
                if (ShakeIntensity > 0)
                {
                    var amt = ShakeIntensity;
                    var Blur = ShakeBlur;
                    ScreenDrawing.ScreenPositionDelta = new Vector2(Rand(-amt, amt), Blur ? 0 : Rand(-amt, amt));
                    ShakeIntensity -= ShakeDecrease;
                }
            }
            #endregion
            public void Extreme()
            {

                ShakeStep();
                var time = GametimeF;
                ScreenDrawing.ThemeColor = Color.Lerp(Color.DeepPink, Color.LightPink, Abs(Sin(time * 1.5f)));
                if (time == 1)
                {
                    NormalGB blaster = new(new Vector2(320, 150), new Vector2(320, 150), new Vector2(1.0f, 1.0f), 90, 30, 90);
                    CreateGB(blaster);
                }
                if (time > 30 && time < 120)
                {
                    Shake(2, 0.1f, true);
                    if ((time % 5) == 0)
                    {
                        var dir = Rand(0, 360);
                        var spd = 5;
                        if (time < 85)
                            for (int i = 0; i < 10; ++i)
                            {
                                var ang = dir + i * 36;
                                spd *= 2;
                                if ((time % 20) == 0)
                                    CreateBone(new CustomBone(new Vector2(320, 150), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                                    {
                                        PositionRouteParam = new float[] { spd * Cos(ang), spd * Sin(ang) },
                                        IsMasked = false,
                                        RotationRouteParam = new float[] { 6f, Rand(0, 359) },
                                        LengthRouteParam = new float[] { 30f },
                                        ColorType = 1
                                    });
                                spd /= 2;
                                CreateBone(new CustomBone(new Vector2(320, 150), Motions.PositionRoute.linear, ang + 90, 30)
                                {
                                    PositionRouteParam = new float[] { spd * Cos(ang), spd * Sin(ang) },
                                    IsMasked = false,
                                });
                            }
                    }
                }
                {
                    if (time == 120)
                    {
                        var Pos = MathUtil.GetVector2(140, -90) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.6f), 90, 90, 60);
                        CreateGB(gb);
                    }
                    if (time == 140)
                    {
                        var Pos = MathUtil.GetVector2(140, -180) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.6f), 0, 70, 60);
                        CreateGB(gb);
                    }
                    if (time == 160)
                    {
                        var Pos = MathUtil.GetVector2(140, -135) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.6f), 45, 50, 60);
                        CreateGB(gb);
                    }
                    if (time == 180)
                    {
                        var Pos = MathUtil.GetVector2(140, -45) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.6f), 135, 30, 60);
                        CreateGB(gb);
                    }
                    if (time == 300)
                    {
                        var Pos = MathUtil.GetVector2(140, 112.5f - 180) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.72f), 112.5f, 90, 60);
                        CreateGB(gb);
                    }
                    if (time == 320)
                    {
                        var Pos = MathUtil.GetVector2(140, 22.5f - 180) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.72f), 22.5f, 70, 60);
                        CreateGB(gb);
                    }
                    if (time == 340)
                    {
                        var Pos = MathUtil.GetVector2(140, 67.5f - 180) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.72f), 67.5f, 50, 60);
                        CreateGB(gb);
                    }
                    if (time == 360)
                    {
                        var Pos = MathUtil.GetVector2(140, 157.5f - 180) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.72f), 157.5f, 30, 60);
                        CreateGB(gb);
                    }
                    if (time == 470)
                    {
                        var Pos = MathUtil.GetVector2(140, -90) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.6f), 90, 90, 60);
                        CreateGB(gb);
                    }
                    if (time == 490)
                    {
                        var Pos = MathUtil.GetVector2(140, -180) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.6f), 0, 70, 60);
                        CreateGB(gb);
                    }
                    if (time == 510)
                    {
                        var Pos = MathUtil.GetVector2(140, -135) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.6f), 45, 50, 60);
                        CreateGB(gb);
                    }
                    if (time == 530)
                    {
                        var Pos = MathUtil.GetVector2(140, -45) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 0.6f), 135, 30, 60);
                        CreateGB(gb);
                    }
                    if (time == 630)
                    {
                        var Pos = MathUtil.GetVector2(140, -90) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 1f), 90, 60, 20);
                        CreateGB(gb);
                    }
                    if (time == 655)
                    {
                        var Pos = MathUtil.GetVector2(140, -180) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 1f), 0, 40, 20);
                        CreateGB(gb);
                    }
                    if (time == 670)
                    {
                        var Pos = MathUtil.GetVector2(140, -135) + new Vector2(320, 320);
                        NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 1f), 45, 50, 20);
                        CreateGB(gb);
                    }
                }
                if (time == 690)
                {
                    var Pos = MathUtil.GetVector2(140, -45) + new Vector2(320, 320);
                    NormalGB gb = new(Pos, new Vector2(320, 320), new Vector2(1.0f, 1f), 135, 30, 20);
                    CreateGB(gb);
                    CreateChart(55, BeatTime(2), 6, new string[]
                    {
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",

                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",

                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",

                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",

                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",

                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R", "/", "/",
                        "/", "/", "/",
                        "/", "/", "/",
                        "R"
                    });
                    for (int i = 0; i < 16; ++i)
                    {
                        var de = i * 44f;
                        CreateArrow(50 + de, Rand(0, 3), 7, 1, 0);
                        CreateArrow(60 + de, LastRand + 1, 7, 1, 0);
                        CreateArrow(70 + de, LastRand + 2, 7, 1, 0);
                        if ((i % 4) == 3)
                        {
                            CreateArrow(75 + de, LastRand + 3, 7, 1, 0);
                            CreateArrow(80 + de, LastRand, 7, 1, 0);
                        }
                    }
                }
                if (time == 750)
                {
                    TP();
                    SetGreenBox();
                    SetSoul(1);
                }
                if (time == 1460)
                {
                    SetGreenBox();
                    SetSoul(0);
                    TP();
                }
                time += 10;
                if (time == 1470)
                {
                    UpBone bone = new(true, 284, 0, 79);
                    CreateBone(bone);
                    PlaySound(Sounds.spearAppear);
                }
                time -= 10;
                if (time == 1520)
                {
                    DownBone bone = new(true, 356, 0, 79);
                    CreateBone(bone);
                    PlaySound(Sounds.spearAppear);
                }
                if (time == 1530)
                {
                    UpBone bone = new(true, 294, 0, 79);
                    CreateBone(bone);
                    PlaySound(Sounds.spearAppear);
                }
                if (time == 1540)
                {
                    DownBone bone = new(true, 346, 0, 79);
                    CreateBone(bone);
                    PlaySound(Sounds.spearAppear);
                }
                if (time == 1585)
                {
                    PlaySound(Sounds.pierce);
                    Bone[] bones = GetAll<Bone>();
                    for (int i = 0; i < bones.Length; ++i)
                    {
                        if (bones[i].Centre.X is 294)
                        {
                            bones[i].Dispose();
                            CreateBone(new CustomBone(new Vector2(294, 240), Motions.PositionRoute.linear, 0, 79)
                            {
                                PositionRouteParam = new float[] { 2f, 0f },
                                ColorType = 1
                            });
                        }
                        if (bones[i].Centre.X is 346)
                        {
                            bones[i].Dispose();
                            var Col = bones[i].GetType();
                            CreateBone(new CustomBone(new Vector2(346, 240), Motions.PositionRoute.linear, 0, 79)
                            {
                                PositionRouteParam = new float[] { -2f, 0f },
                                ColorType = 1
                            });
                        }
                    }
                }
                if (time == 1600)
                {
                    PlaySound(Sounds.pierce);
                    Bone[] bones = GetAll<Bone>();
                    for (int i = 0; i < bones.Length; ++i)
                    {
                        if (bones[i].Centre.X is 284)
                        {
                            bones[i].Dispose();
                            CreateBone(new CustomBone(new Vector2(284, 240), Motions.PositionRoute.linear, 0, 79)
                            {
                                PositionRouteParam = new float[] { 2f, 0f },
                                ColorType = 2
                            });
                        }
                        if (bones[i].Centre.X is 356)
                        {
                            bones[i].Dispose();
                            var Col = bones[i].GetType();
                            CreateBone(new CustomBone(new Vector2(356, 240), Motions.PositionRoute.linear, 0, 79)
                            {
                                PositionRouteParam = new float[] { -2f, 0f },
                                ColorType = 2
                            });
                        }
                    }
                }
                if (time == 1610)
                {
                    SetBox(320, 140, 140);
                }
                if (time == 1620)
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 6, 110));
                    CreateBone(new DownBone(false, 6, 110));
                }
                if (time == 1635)
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new UpBone(true, 6, 30));
                    CreateBone(new UpBone(false, 6, 30));
                }
                time += 10;
                if (time is 1680 or 1700)
                {
                    CreateGB(new NormalGB(MathUtil.GetVector2(170, Rand(0, 359)) + new Vector2(320, 320), Heart.Centre, new Vector2(1.1f, 1.1f), 1730 - time, 30));
                }
                time -= 10;
                if (time == 1780)
                {
                    SetSoul(2);
                    Heart.JumpTimeLimit = 1;
                    Heart.GiveInstantForce(90, 15);
                }
                if (time == 1790)
                {
                    Heart.GiveInstantForce(270, 15);
                }
                if (time == 1800)
                {
                    Heart.GiveInstantForce(90, 15);
                }
                if (time == 1870)
                {
                    SetSoul(2);
                    Heart.GiveInstantForce(0, 15);
                }
                if (time == 1880)
                {
                    Heart.GiveInstantForce(180, 15);
                }
                if (time == 1890)
                {
                    Heart.GiveInstantForce(0, 15);
                }
                if (time > 1790 && time < 2090)
                {
                    if ((time % 10) == 0)
                    {
                        CreateBone(new LeftBone(true, 6, 20));
                        CreateBone(new RightBone(false, 6, 20));
                        if (time > 1890)
                        {
                            CreateBone(new UpBone(true, 6, 20));
                            CreateBone(new DownBone(false, 6, 20));
                        }
                    }
                }
                if (time == 2120)
                {
                    SetSoul(2);
                    Heart.RotateTo(0);
                    Heart.IsOranged = true;
                    Heart.SoftFalling = true;
                    Heart.Speed = 1;
                    Heart.Gravity = 9.81f * 1.5f;
                    HeartAttribute.HP = HeartAttribute.MaxHP;
                    PlaySound(Sounds.heal);
                }
                if (time > 2120 && time < 3380)
                {
                    if ((time % 60) == 0)
                    {
                        CreateBone(new UpBone(true, 5, 70));
                        CreateBone(new DownBone(true, 5, 20));
                        CreateBone(new UpBone(false, 5, 70));
                        CreateBone(new DownBone(false, 5, 20));
                    }
                    if ((time % 35) == 0)
                    {
                        CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 200), new Vector2(Heart.Centre.X, 0), new Vector2(1.0f, 0.5f), 20, 10));
                    }
                }
                if (time == 3380)
                {
                    SetSoul(2);
                    Heart.Gravity = 9.81f;
                    Heart.IsOranged = false;
                    Heart.SoftFalling = false;
                    Heart.Speed = 2;
                    Heart.JumpTimeLimit = 1;
                    HeartAttribute.HP = HeartAttribute.MaxHP;
                    PlaySound(Sounds.heal);
                    SetBox(320, 210, 140);
                }
                if (time > 3380 && time < 4800)
                {
                    if ((time % 60) == 0)
                    {
                        Platform platform = new(1, new Vector2(200f, 280f), Motions.PositionRoute.XAxisSin, Motions.LengthRoute.stableValue, Motions.RotationRoute.sin);
                        platform.createWithScaling = true;
                        platform.PositionRouteParam = new float[] { 2.4f, 24f, 64f, GametimeF / 1.5f };
                        platform.LengthRouteParam = new float[] { 45f };
                        platform.RotationRouteParam = new float[] { 30f, 64f, GametimeF, 0f };
                        CreatePlatform(platform);
                    }
                    InstantSetBox(280 + Cos((time - 3380) * 1.2f) * 40, 210, 140);
                    if ((time % 10) == 0 && time > 3420)
                    {
                        var state = BoxStates.Centre.Y >= 280 || BoxStates.Centre.Y < 250;
                        var go_bone = !state || ((time % 10) == 0);
                        if (go_bone)
                            CreateBone(new DownBone(true, 6, 20));
                        CreateBone(new UpBone(true, 6, 20));
                    }
                }
                if (time == 4800)
                {
                    HeartAttribute.HP = HeartAttribute.MaxHP;
                    PlaySound(Sounds.heal);
                    TP();
                    SetSoul(1);
                    SetGreenBox();
                    CreateChart(30, BeatTime(2), 7, new string[]
                    {
                        "R","/","/",
                        "/","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                        "/","/","/",
                        "R","/","/",
                    });
                    CreateChart(30, BeatTime(2), 7, new string[]
                    {
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/","/",
                        "/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                        "R1","/","/",
                        "/","/","/",
                    });
                }
                if (time == 4940)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR010",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "/",
                        "/",
                        "/",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5115)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR110",
                        "R",
                        "R",
                        "R",
                        "/",
                        "R",
                        "R",
                        "R",
                        "R",
                        "/",
                        "/",
                        "/",
                        "/",
                        "/",
                        "R",
                        "R",
                        "R",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5295)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR016",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "/",
                        "/",
                        "/",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5475)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR110",
                        "R",
                        "R",
                        "R",
                        "/",
                        "R",
                        "R",
                        "R",
                        "R",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5655)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR010",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "/",
                        "/",
                        "/",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5830)
                {
                    for (int i = 0; i < 15; ++i)
                    {
                        CreateArrow(120 + i * BeatTime(1) / 4.5f, Rand(0, 3), 8, 0, 0);
                    }
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR116",
                        "R",
                        "R",
                        "R",
                        "/",
                        "R",
                        "R",
                        "R",
                        "R",
                        "/",
                        "R",
                        "R",
                        "R",
                        "R",
                        "R",
                        "R",
                        "R",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 6005)
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR010",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "/",
                        "/",
                        "/",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                time -= 6005 + 175 - 5115;
                if (time == 5110)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR110",
                        "R",
                        "R",
                        "R",
                        "/",
                        "R",
                        "R",
                        "R",
                        "R",
                        "/",
                        "/",
                        "/",
                        "/",
                        "/",
                        "R",
                        "R",
                        "R",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5290)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR016",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "/",
                        "/",
                        "/",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5475)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR110",
                        "R",
                        "R",
                        "R",
                        "/",
                        "R",
                        "R",
                        "R",
                        "R",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5655)
                {
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR010",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                        "R1",
                        "/",
                        "/",
                        "/",
                        "/",
                        "/",
                        "R1",
                        "R1",
                        "R1",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                if (time == 5830)
                {
                    for (int i = 3; i < 15; ++i)
                    {
                        CreateArrow(120 + i * BeatTime(2), Rand(0, 3), 8, 0, 0);
                    }
                    SpecialRhythmCreate(BeatTime(2), 7, 60, new string[]
                    {
                        "WR116",
                        "R",
                        "R",
                        "R",
                        "/",
                        "R",
                        "R",
                        "R",
                        "R",
                        "/",
                        "R",
                        "R",
                        "R",
                        "R",
                        "R",
                        "R",
                        "R",
                    }, new string[]
                    {

                    }, new System.Action[]
                    {

                    });
                }
                time += 6005 + 175 - 5115;
                if (time == 6950)
                {
                    TP();
                    SetSoul(1);
                    SetGreenBox();
                    CreateGB(new GreenSoulGB(240, 0, 0, 5));
                    CreateGB(new GreenSoulGB(240, 2, 1, 5));

                    CreateGB(new GreenSoulGB(270, 3, 0, 80));
                }
                if (time == 6955)
                {
                    CreateGB(new GreenSoulGB(245, 1, 0, 5));
                    CreateGB(new GreenSoulGB(245, 3, 1, 5));
                }
                if (time == 6960)
                {
                    CreateGB(new GreenSoulGB(250, 2, 0, 5));
                    CreateGB(new GreenSoulGB(250, 0, 1, 5));

                    CreateGB(new GreenSoulGB(260, 1, 1, 80));
                }
                if (time > 7220 && time < 7230)
                {
                    Shake(2, 1);
                }
                if (time > 7230 && time < 7300)
                {
                    Shake(5, 1);
                }
                if (time == 7310)
                {
                    SetSoul(0);
                    SetGreenBox();
                    TP();
                }
                if (time > 7380 && (time - 7380) % 22 == 0 && time <= 7468)
                {
                    SetSoul(2);
                    var dir = (time - 7380) / 22 * 90;
                    Heart.GiveInstantForce(dir, 50);
                    CreateEntity(new Boneslab(dir, 70, 10, 10));
                    CreateEntity(new Boneslab(dir - 90, 65, 10, 10));
                }
                if (time == 7490)
                {
                    SetSoul(0);
                    for (int i = 0; i < 4; ++i)
                    {
                        CreateEntity(new Boneslab(i * 90, 35, 20, 50));
                    }
                }
                time -= 7560 - 7380;
                if (time > 7380 && (time - 7380) % 22 == 0 && time <= 7468)
                {
                    SetSoul(2);
                    var dir = (time - 7380) / 22 * 90 + 180;
                    Heart.GiveInstantForce(dir, 50);
                    CreateEntity(new Boneslab(dir, 70, 10, 10));
                    CreateEntity(new Boneslab(dir - 90, 65, 10, 10));
                }
                if (time == 7490)
                {
                    SetSoul(0);
                    Heart.RotateTo(0);
                    for (int i = 0; i < 4; ++i)
                    {
                        CreateEntity(new Boneslab(i * 90, 35, 20, 50));
                    }
                }
                time += 7560 - 7380;
                time -= 7740 - 7380;
                if (time > 7380 && (time - 7380) % 22 == 0 && time <= 7468)
                {
                    SetSoul(2);
                    var dir = (time - 7380) / 22 * 90 - 90;
                    Heart.GiveInstantForce(dir, 50);
                    CreateEntity(new Boneslab(dir, 70, 10, 10));
                    CreateEntity(new Boneslab(dir - 90, 65, 10, 10));
                }
                if (time == 7490)
                {
                    SetSoul(0);
                    Heart.RotateTo(0);
                    for (int i = 0; i < 4; ++i)
                    {
                        CreateEntity(new Boneslab(i * 90, 35, 20, 50));
                    }
                }
                time += 7740 - 7380;
                time -= 7920 - 7380;
                if (time > 7380 && (time - 7380) % 22 == 0 && time <= 7468)
                {
                    SetSoul(2);
                    var dir = (time - 7380) / 22 * 90 + 90;
                    Heart.GiveInstantForce(dir, 50);
                    CreateEntity(new Boneslab(dir, 70, 10, 10));
                    CreateEntity(new Boneslab(dir - 90, 65, 10, 10));
                }
                time += 7920 - 7380;
                if (time == 8030)
                {
                    SetGreenBox();
                    TP();
                    SetSoul(0);
                    Heart.InstantSetRotation(0);
                }
                if (time >= 8040)
                {
                    if ((time - 8040) % 60 == 0)
                    {
                        var col = Rand(0, 1) + 1;
                        ArrayUsage[0] = col;
                        UpBone bone = new(true, 284, 0, 79);
                        bone.ColorType = col;
                        CreateBone(bone);
                        PlaySound(Sounds.spearAppear);
                    }
                    if ((time - 8050) % 60 == 0)
                    {
                        var col = Rand(0, 1) + 1;
                        ArrayUsage[1] = col;
                        DownBone bone = new(true, 356, 0, 79);
                        bone.ColorType = col;
                        CreateBone(bone);
                        PlaySound(Sounds.spearAppear);
                    }
                    if ((time - 8070) % 60 == 0)
                    {
                        PlaySound(Sounds.pierce);
                        Bone[] bones = GetAll<Bone>();
                        for (int i = 0; i < bones.Length; ++i)
                        {
                            bones[i].Dispose();
                        }
                        CreateBone(new CustomBone(new Vector2(284, 240), Motions.PositionRoute.linear, 0, 79)
                        {
                            PositionRouteParam = new float[] { 3f, 0f },
                            ColorType = ArrayUsage[0]
                        });
                        CreateBone(new CustomBone(new Vector2(356, 240), Motions.PositionRoute.linear, 0, 79)
                        {
                            PositionRouteParam = new float[] { -3f, 0f },
                            ColorType = ArrayUsage[1]
                        });
                    }
                }
            }

            public void ExtremePlus() { }

            public string FightName => "Master Spark";

            public string Music => "Marisa";
            public float ShakeIntensity = 0;
            public float ShakeDecrease = 0;
            public bool ShakeBlur = true;
            public int[] ArrayUsage = new int[2];
            public static float bpm = 62.5f / (112 / 60f);
        }
    }
}