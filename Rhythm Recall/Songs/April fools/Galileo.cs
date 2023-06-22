using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.CameraEffect;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class Galileo : IChampionShip
    {
        public Galileo()
        {

            difficulties = new()
            {
                { "div.2", Difficulty.Extreme },
                { "div.1", Difficulty.ExtremePlus }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (172f / 60f))
            {

            }
            public string Music => "Galileo";

            public string FightName => "Galileo";
            private class ThisImformation : SongImformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 18f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 18f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 21f),
                        }
                    );
                public override string BarrageAuthor => "TK";
                public override string PaintAuthor => "Masaharu Fukuyama";
                public override string SongAuthor => "Masaharu Fukuyama + Akira Inoue";
            }
            public SongImformation Attributes => new ThisImformation();
            private bool notRegistered = true;
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production1;
            RenderProduction production2;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;
            public float TempVar;
            #region Not Yet
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
            }
            #endregion
            public void ExtremePlus()
            {
                #region Intro
                //Guitar
                if (InBeat(0.1f))
                {
                    var dir = Rand(0, 3);
                    for (int i = 0; i < 3; ++i)
                        CreateArrow(BeatTime(2.1f + i * 0.25f), dir + i, 6, 0, 0);
                }
                if (InBeat(2.2f) || InBeat(2.45f) || InBeat(2.7f))
                {
                    //Line
                    var X = GametimeF * 15 - 475;
                    Line a = new(new Vector2(X, 0), new Vector2(X, 480));
                    a.AlphaDecrease(BeatTime(1));
                    CreateEntity(a);
                    DelayBeat(1, () =>
                    {
                        a.Dispose();
                    });
                }
                if (InBeat(2.85f) || InBeat(9.75f) || InBeat(16.65f) || InBeat(24.25f)
                     || InBeat(31.15f) || InBeat(38.75f) || InBeat(45.65f) || InBeat(52.55f))
                {
                    CreateArrow(BeatTime(2), Rand(0, 3), 6, 0, 0);
                    CreateArrow(BeatTime(3), Rand(0, 3), 6, 0, 0);
                    CreateGB(new GreenSoulGB(BeatTime(4), Rand(0, 3), 0, BeatTime(3)));
                    DelayBeat(4, () =>
                    {
                        SizeShrink(9f, BeatTime(4));
                        Convulse(45, BeatTime(4), RandBool());
                    });
                    if (InBeat(24, 46))
                    {
                        var v1 = FightResources.Shaders.Blur;
                        RenderProduction production1 = new Filter(v1, 0.99f);
                        v1.Factor = new(0.5f, 0.5f);
                        DelayBeat(4, () =>
                        {
                            SceneRendering.InsertProduction(production1);
                        });
                        DelayBeat(8, () =>
                        {
                            production1.Dispose();
                        });
                    }
                }
                if (InBeat(58))
                {
                    CreateGB(new GreenSoulGB(BeatTime(4), Rand(0, 3), 0, BeatTime(7)));
                }
                if (InBeat(62))
                {
                    CreateGB(new GreenSoulGB(BeatTime(4), Rand(0, 3), 1, BeatTime(7)));
                    LerpScreenScale(BeatTime(3), 1.1f, 0.12f);
                }
                if (InBeat(66))
                {
                    TempVar = Rand(0, 3);
                    CreateGB(new GreenSoulGB(BeatTime(4), (int)TempVar, 0, BeatTime(7)));
                    LerpScreenScale(BeatTime(3), 1.2f, 0.12f);
                }
                if (InBeat(70))
                    LerpScreenScale(BeatTime(3), 1.3f, 0.12f);
                if (InBeat(73))
                {
                    for (int i = 0; i < 16; ++i)
                    {
                        float delay = (i < 5) ? BeatTime(1f + i) : BeatTime(4.4f + (i - 5) * 0.35f);
                        CreateArrow(delay, (int)TempVar, 7, 0, 0);
                        DelayBeat(delay / SingleBeat, () =>
                        {
                            ScreenDrawing.ScreenAngle = Rand(-180, 180);
                            var dir = Rand(0, 360);
                            Vector2 pos = MathUtil.GetVector2(1000, dir);
                            Vector2 center = new(320, 240);
                            Line a = new(center + pos, center - pos);
                            a.AlphaDecrease(BeatTime(1));
                            CreateEntity(a);
                            DelayBeat(1, () =>
                            {
                                a.Dispose();
                            });
                        });
                    }
                }
                //Background Piano
                if (InBeat(13.5f))
                {
                    CreateArrow(BeatTime(2), Rand(0, 3), 7, 1, 0);
                    CreateArrow(BeatTime(3.7f), Rand(0, 3), 7, 1, 0);
                    DelayBeat(2, () =>
                    {
                        Line a = new(new Vector2(0, -80), new Vector2(640, 570));
                        a.AlphaDecrease(BeatTime(1));
                        CreateEntity(a);
                        DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    DelayBeat(3.7f, () =>
                    {
                        Line a = new(new Vector2(560, 0), new Vector2(80, 480));
                        a.AlphaDecrease(BeatTime(1));
                        CreateEntity(a);
                        DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                }
                if (InBeat(21.5f))
                {
                    CreateArrow(BeatTime(2), Rand(0, 3), 7, 1, 0);
                    CreateArrow(BeatTime(2.5f), Rand(0, 3), 7, 1, 0);
                    CreateArrow(BeatTime(4f), Rand(0, 3), 7, 1, 0);
                    CreateArrow(BeatTime(5f), Rand(0, 3), 7, 1, 0);
                    DelayBeat(2, () =>
                    {
                        Line a = new(new Vector2(320, 240), new Vector2(640, -80));
                        a.AlphaDecrease(BeatTime(1));
                        CreateEntity(a);
                        DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    DelayBeat(2.5f, () =>
                    {
                        Line a = new(new Vector2(320, 240), new Vector2(0, -80));
                        a.AlphaDecrease(BeatTime(1));
                        CreateEntity(a);
                        DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    DelayBeat(4, () =>
                    {
                        Line a = new(new Vector2(320, 240), new Vector2(0, 560));
                        a.AlphaDecrease(BeatTime(1));
                        CreateEntity(a);
                        DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    DelayBeat(5, () =>
                    {
                        Line a = new(new Vector2(320, 240), new Vector2(640, 560));
                        a.AlphaDecrease(BeatTime(1));
                        CreateEntity(a);
                        DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                }
                #endregion
                #region Bass Guitar
                if (InBeat(82))
                {
                    RotateTo(0, BeatTime(0.5f));
                    LerpScreenScale(BeatTime(2), 1, 0.16f);
                }
                if (InBeat(79.5f, 110f))
                {
                    if (InBeat(79.5f, 108f) && At0thBeat(1.5f))
                    {
                        Convulse(30, BeatTime(1.5f), RandBool());
                        var dir = Rand(0, 3);
                        for (int i = 0; i < 3; ++i)
                        {
                            CreateArrow(BeatTime(2f + i * 0.5f), dir, 8, 0, 0);
                            CreateArrow(BeatTime(2f + i * 0.5f), dir + 2, 8, 0, 0);
                        }
                    }
                    if (At0thBeat(2.8f))
                    {
                        var dir = Rand(0, 360);
                        var dist = Rand(50, 170);
                        for (int i = 0; i < 4; ++i)
                        {
                            Vector2 pos = new Vector2(320, 240) + MathUtil.GetVector2(dist, dir + i * 90);
                            CreateGB(new NormalGB(pos, pos, new(1, 1), dir + (i + 1) * 90, BeatTime(1.5f), 10));
                        }
                    }
                }
                if (InBeat(94))
                {
                    var dir = Rand(0, 3);
                    for (int i = 0; i < 10; ++i)
                    {
                        CreateArrow(BeatTime(2f + i * 0.2f), dir + i * 2, 6, 1, 0);
                    }
                }
                if (InBeat(110) || InBeat(125.5f))
                {
                    RotateTo(0, BeatTime(0.5f));
                    for (int i = 0; i < 4; ++i)
                    {
                        if (i < 3)
                            CreateArrow(BeatTime(2f + i * 0.5f), Rand(0, 3), 7, 0, 0);
                        else
                            CreateGB(new GreenSoulGB(BeatTime(2f + i * 0.5f), Rand(0, 3), 0, BeatTime(3)));

                    }
                }
                if (InBeat(117.5f))
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        if (i < 4)
                            CreateArrow(BeatTime(2f + i * 0.5f), Rand(0, 3), 7, 0, 0);
                        else
                            CreateGB(new GreenSoulGB(BeatTime(2f + i * 0.5f), Rand(0, 3), 0, BeatTime(3)));
                    }
                }
                if (InBeat(131))
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        CreateArrow(BeatTime(2f + i * 0.5f), Rand(0, 3), 7, 0, 0);
                    }
                }
                if (InBeat(134))
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        CreateArrow(BeatTime(2f + i * 0.5f), Rand(0, 3), 7, 0, 0);
                    }
                }
                if (InBeat(112) || InBeat(120) || InBeat(128))
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        CreateArrow(BeatTime(2f + i * 1.6f), Rand(0, 3), 7, 1, 0);
                        CreateArrow(BeatTime(2f + i * 1.6f), LastRand, 7, 1, 0);
                        DelayBeat(2f + i * 1.6f, () =>
                        {
                            ScreenShaker a = new(12, 20, 1);
                            AddInstance(a);
                            DelayBeat(2, () =>
                            {
                                a.Dispose();
                            });
                        });
                    }
                    CreateArrow(BeatTime(9.2f), Rand(0, 3), 7, 1, 0);
                    CreateArrow(BeatTime(9.2f), LastRand, 7, 1, 0);
                }
                if (InBeat(137))
                {
                    for (int i = 0; i < 10; ++i)
                    {
                        CreateArrow(BeatTime(2f + i * 0.5f), Rand(0, 3), 7, 0, 0);
                    }
                }
                if (InBeat(144))
                {
                    LerpScreenScale(BeatTime(1), 2, 0.18f);
                    Rotate(360, BeatTime(2));
                }
                if (InBeat(145))
                {
                    for (int i = 0; i < 10; ++i)
                    {
                        CreateArrow(BeatTime(2f + i * 0.5f), Rand(0, 3), 4, 0, 0);
                    }
                }
                if (InBeat(152))
                {
                    WhiteOut(BeatTime(3));
                }
                if (InBeat(153.5f))
                {
                    ScreenScale = 1;
                    SetSoul(2);
                    HeartAttribute.SoftFalling = true;
                    InstantSetBox(310, 340, 160);
                    InstantTP(170, 380);
                    TempVar = 0;
                }
                #endregion
                #region Pursue
                if (InBeat(155, 232))
                {
                    if (At0thBeat(0.15f))
                        CreateBone(new DownBone(true, 4, 20) { MarkScore = false });
                    if (At0thBeat(3.75f))
                    {
                        TempVar++;
                        CreatePlatform(new Platform(0, new(520, 350), Motions.PositionRoute.linear, 0, 60)
                        {
                            PositionRouteParam = new float[] { -4, 0 }
                        });
                        if (TempVar == 0)
                        {
                            UpBone bone = new(true, 520, 4, 200);
                            bone.Length = 200;
                            bone.MissionLength = 0;
                            bone.LengthLerpScale = 0.02f;
                            DelayBeat(1, () =>
                            {
                                bone.MissionLength = 80;
                            });
                            CreateBone(bone);
                        }
                    }
                    if (InBeat(157f) || InBeat(158f))
                    {
                        PlaySound(pierce);
                        CreateBone(new UpBone(true, 6, 60));
                        CreateBone(new DownBone(true, 6, 60));
                        Convulse(20, BeatTime(3f), InBeat(157));
                        SizeShrink(9, BeatTime(3f));
                    }
                    if (InBeat(159f))
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 6.7f, 80));
                    }
                    if (InBeat(162))
                    {
                        Rotate(360, BeatTime(1.8f));
                    }
                    if ((InBeat(162, 163)) && At0thBeat(0.1f))
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 7, 60) { ColorType = 1 });
                    }
                    if (InBeat(165.6f))
                    {
                        PlaySound(pierce);
                        CreateBone(new UpBone(true, 7, 80));
                    }
                    if (InBeat(166.4f) || InBeat(166.8f) || InBeat(167.4f))
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 7, 80));
                    }
                    if (InBeat(168f))
                    {
                        PlaySound(pierce);
                        CreateBone(new UpBone(true, 7, 70));
                    }
                    if (InBeat(170) || InBeat(170.7f) || InBeat(171.4f))
                    {
                        ThrowBones();
                    }
                    if (InBeat(173.3f) || InBeat(174.7f))
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 8, 60));
                        CreateBone(new DownBone(false, 8, 60));
                    }
                    if (InBeat(174.0f))
                    {
                        PlaySound(pierce);
                        CreateBone(new UpBone(true, 8, 90));
                        CreateBone(new UpBone(false, 8, 90));
                    }
                    if (InBeat(178))
                    {
                        SizeExpand(25f, BeatTime(3f));
                        Convulse(95, BeatTime(3f), false);
                    }
                    if ((InBeat(178, 179)) && At0thBeat(0.1f))
                    {
                        PlaySound(pierce);
                        CreateBone(new UpBone(true, 7, 60 - Sin(GametimeF * 5) * 40) { ColorType = 2 });
                        CreateBone(new DownBone(true, 7, 85 + Sin(GametimeF * 5) * 40));
                    }
                    if (InBeat(181.3f))
                    {
                        PlaySound(pierce);
                        CreateBone(new UpBone(true, 7, 80));
                    }
                    if (InBeat(181.9f) || InBeat(182.3f) || InBeat(182.9f))
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 7, 80));
                    }
                    if (InBeat(183.3f))
                    {
                        PlaySound(pierce);
                        CreateBone(new UpBone(true, 7, 70));
                    }
                    if (InBeat(185f) || InBeat(185.8f) || InBeat(186.6f) ||
                        InBeat(189f) || InBeat(189.8f) || InBeat(190.6f) ||
                        InBeat(193.2f) || InBeat(194.0f) || InBeat(194.8f))
                    {
                        PlaySound(pierce);
                        var bol = InBeat(185.8f) || InBeat(189) || InBeat(190.6f) || InBeat(194) || InBeat(194.8f);
                        CreateBone(bol ? new DownBone(true, 7, 70) : new UpBone(true, 7, 70));
                    }
                    if (InBeat(196.7f) || InBeat(197.7f))
                    {
                        PlaySound(pierce);
                        CreateBone(new DownBone(true, 7, 80));
                    }
                    if (InBeat(199))
                    {
                        Convulse(true);
                        LerpScreenScale(BeatTime(1), ScreenScale + 0.3f, 0.24f);
                        ScreenPositionDetla -= new Vector2(0, 20);
                        CreateEntity(new Boneslab(0, 80, 1, 10));
                    }
                    if (InBeat(200f))
                    {
                        Convulse(false);
                        LerpScreenScale(BeatTime(1), ScreenScale + 0.3f, 0.24f);
                        ScreenPositionDetla -= new Vector2(0, 20);
                        CreateEntity(new Boneslab(90, 170, 1, 10));
                    }
                    if (InBeat(201f))
                    {
                        Convulse(true);
                        LerpScreenScale(BeatTime(1), ScreenScale + 0.3f, 0.24f);
                        ScreenPositionDetla -= new Vector2(0, 20);
                        CreateEntity(new Boneslab(180, 80, 1, 10));
                    }
                    if (InBeat(202f))
                    {
                        Convulse(false);
                        LerpScreenScale(BeatTime(1), ScreenScale + 0.3f, 0.24f);
                        ScreenPositionDetla -= new Vector2(0, 20);
                        CreateEntity(new Boneslab(270, 170, 1, 10));
                    }
                    if (InBeat(203))
                    {
                        LerpScreenScale(BeatTime(1), 1, 0.24f);
                        LerpScreenPos(BeatTime(1), Vector2.Zero, 0.24f);
                    }
                    if (InBeat(203.2f))
                    {
                        PlaySound(pierce);
                        for (int i = 0; i < 6; ++i)
                        {
                            CreateBone(new CustomBone(new(560, 310), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                            {
                                PositionRouteParam = new float[] { -4, 0 },
                                RotationRouteParam = new float[] { -6f, i * 30 },
                                LengthRouteParam = new float[] { 110f },
                            });
                        }
                    }
                    if (InBeat(205) || InBeat(206))
                    {
                        PlaySound(pierce);
                        CreateBone(new CustomBone(new(560, Heart.Centre.Y), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                        {
                            PositionRouteParam = new float[] { -8, 0 },
                            RotationRouteParam = new float[] { 0, 90 },
                            LengthRouteParam = new float[] { 40f },
                        });
                    }
                    if (InBeat(207))
                    {
                        PlaySound(pierce);
                        CreateBone(new CustomBone(new(Heart.Centre.X, 200), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                        {
                            PositionRouteParam = new float[] { 0, 8 },
                            RotationRouteParam = new float[] { 0, 0 },
                            LengthRouteParam = new float[] { 40f },
                        });
                    }
                    if (InBeat(209))
                    {
                        PlaySound(pierce);
                        for (int i = 0; i < 4; ++i)
                        {
                            var dir = i * 90 + 45;
                            CreateBone(new CustomBone(GetVector2(200, dir) + Heart.Centre, Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                            {
                                PositionRouteParam = new float[] { -8 * Cos(dir), -8 * Sin(dir) },
                                RotationRouteParam = new float[] { 0, (i + 1) * 90 + 45 },
                                LengthRouteParam = new float[] { 40f },
                            });
                        }
                    }
                    if (InBeat(210))
                    {
                        PlaySound(pierce);
                        for (int i = 0; i < 4; ++i)
                        {
                            var dir = i * 90;
                            CreateBone(new CustomBone(GetVector2(200, dir) + Heart.Centre, Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                            {
                                PositionRouteParam = new float[] { -8 * Cos(dir), -8 * Sin(dir) },
                                RotationRouteParam = new float[] { 0, (i + 1) * 90 },
                                LengthRouteParam = new float[] { 40f },
                            });
                        }
                    }
                    if (InBeat(211.3f))
                    {
                        PlaySound(pierce);
                        CreateEntity(new Boneslab(90, 153, 1, 20));
                        CreateEntity(new Boneslab(270, 153, 1, 20));
                    }
                    if (InBeat(215.5f, 231))
                    {
                        if (At0thBeat(0.5f))
                        {
                            PlaySound(pierce);
                            var spd = 8;
                            var dir = GametimeF;
                            for (int i = 0; i < 15; ++i)
                            {
                                var ang = dir + i * 24;
                                Vector2 pos = new Vector2(320, 70) + GetVector2(100, dir);
                                CreateBone(new CustomBone(pos, Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                                {
                                    PositionRouteParam = new float[] { spd * Cos(ang), spd * Sin(ang) },
                                    IsMasked = false,
                                    RotationRouteParam = new float[] { 0f, ang + 90 },
                                    LengthRouteParam = new float[] { 30f },
                                });
                            }
                        }
                    }
                }
                #endregion
                #region Break
                if (InBeat(232))
                {
                    SetBox(240, 140, 140);
                    BarrageCreate(60, BeatTime(1), 7, new string[]
                    {
                        "R", "", "", "", "", "", "",
                        "R", "", "", "", "", "", "",
                        "", "", "", "", "", "", "",
                        "R", "", "", "", "", "", "",
                        "", "", "", "", "R", "", "",
                        "","","", "R", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "R", "", "", "", "", "", "",
                        "", "", "", "", "", "", "",
                        "R", "", "", "", "", "", "",
                        "R", "", "", "", "", "", "",
                        "", "", "", "", "", "", "",
                        "R", "", "", "", "", "", "",
                    });
                }
                if (InBeat(233))
                {
                    SetSoul(1);
                    TP();
                    SetGreenBox();
                }
                #endregion
            }
            public void Start()
            {
                SetSoul(1);
                TP();
                SetGreenBox();
                //GametimeDelta = 0;
                //GametimeDelta = BeatTime(80);
                //GametimeDelta = BeatTime(144);
                GametimeDelta = BeatTime(231);
                PlayOffset = GametimeDelta;
            }
            #region Functions
            private static void ThrowBones()
            {
                PlaySound(pierce);
                var dir = Rand(0, 360);
                var spd = 5;
                for (int i = 0; i < 10; ++i)
                {
                    var ang = dir + i * 36;
                    spd *= 2;
                    CreateBone(new CustomBone(new(320, 50), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        PositionRouteParam = new float[] { spd * Cos(ang), spd * Sin(ang) },
                        IsMasked = false,
                        RotationRouteParam = new float[] { 6f, Rand(0, 359) },
                        LengthRouteParam = new float[] { 30f },
                        ColorType = 1
                    });
                    spd /= 2;
                    CreateBone(new CustomBone(new(320, 50), Motions.PositionRoute.linear, ang + 90, 30)
                    {
                        PositionRouteParam = new float[] { spd * Cos(ang), spd * Sin(ang) },
                        IsMasked = false,
                    });
                }
            }
            #endregion
        }
    }
}