using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class SuddenChange : IChampionShip
    {
        public SuddenChange()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("time:13.75,15.50"));
            divisionInformation.PushNext(new SaveInfo("date:5,2"));
            divisionInformation.PushNext(new SaveInfo("dif:2,4"));

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Normal);
            difficulties.Add("div.1", Difficulty.Extreme);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            private class KickCounter : Entity
            {
                public override void Draw()
                {
                    Font.NormalFont.CentreDraw((count + 1) + "", new Microsoft.Xna.Framework.Vector2(320, 80), Color.White, GameStates.SpriteBatch);
                    if (time > 0)
                    {
                        Font.NormalFont.CentreDraw("Time = " + (count * 1.0f / time), new Microsoft.Xna.Framework.Vector2(320, 120), Color.White, GameStates.SpriteBatch);
                        Font.NormalFont.CentreDraw("Frame = " + 60 * (count * 1.0f / time), new Microsoft.Xna.Framework.Vector2(320, 160), Color.White, GameStates.SpriteBatch);
                    }
                }

                private int count = -1;
                private float time = 0;

                public override void Update()
                {
                    if (GameStates.IsKeyPressed(InputIdentity.Alternate) && time == 0)
                    {
                        count = 0;
                        time += 0.001f;
                        return;
                    }
                    if (time == 0) return;
                    time++;
                    if (GameStates.IsKeyPressed(InputIdentity.Alternate))
                    {
                        count++;
                        PlaySound(Sounds.pierce);
                    }
                }
            }

            public Game() : base(6.7075f)
            { }

            public static Game instance;

            public string Music => "Sudden Changes";

            public string FightName => "Sudden Change";

            private class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => "T-mas";
                public override string PaintAuthor => "Kaeny";
                public override string SongAuthor => "Xylium";

                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 11.5f),
                            new(Difficulty.Extreme, 17.8f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 12.5f),
                            new(Difficulty.Extreme, 18.9f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 19.0f),
                            new(Difficulty.Extreme, 21.6f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            #region Non-ChampionShip
            public void Easy()
            {
                throw new System.NotImplementedException();
            }
            public void Noob()
            {
                throw new System.NotImplementedException();
            }
            public void Hard()
            {
                throw new System.NotImplementedException();
            }
            public void ExtremePlus()
            {
                throw new NotImplementedException();
            }
            #endregion

            private static int stableID = 1;
            private static float curHeight = 0;
            private static bool overPos = false;
            private static float gravity = 0;

            public void Normal()
            {
                if (InBeat(0, 128 - 1) && At0thBeat(16))
                {
                    for (int i = 0; i < 20; i++) CreateBone(new SideCircleBone(i * 18, 3, 60, BeatTime(6)));
                }
                if (InBeat(0, 128 - 1) && AtKthBeat(16, BeatTime(4)))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 0, 180 }));
                }
                if (InBeat(0, 128 - 1) && AtKthBeat(16, BeatTime(7.2f)))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 0, 180 }));
                }
                if (InBeat(0, 128 - 1) && AtKthBeat(16, BeatTime(10f)))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 0, 180 }));
                }

                if (InBeat(128, 256 - 12) && At0thBeat(8))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(8),
                        new float[] { Rand(0, 359), Rand(0, 359) }));
                }

                if (InBeat(256))
                {
                    HeartAttribute.Gravity = 6.7f;
                    HeartAttribute.JumpSpeed = 5.1f;
                    SetBox(295, 200, 85);
                    SetSoul(2);
                }
                if (InBeat(256 + 2, 256 + 127) && GametimeF % 6 == 0)
                {
                    float upPos = Sin(GametimeF * 3) * 35f - 15f;
                    float downPos = Sin(GametimeF * 3 + 180) * 35f - 15f;
                    if (upPos > 0) CreateBone(new UpBone(false, 2.0f, upPos));
                    if (downPos > 0) CreateBone(new DownBone(false, 2.0f, downPos));
                }
                if (InBeat(256, 384 + 127) && AtKthBeat(16, BeatTime(6)))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(2), new float[] { 0, 180 }));
                }
                if (InBeat(384))
                {
                    CreatePlatform(new Platform(1, new Vector2(320, 310), Motions.PositionRoute.YAxisSin, 0, 40, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 80, BeatTime(32), BeatTime(8) }
                    });
                    CreatePlatform(new Platform(0, new Vector2(320, 310), Motions.PositionRoute.YAxisSin, 0, 40, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 80, BeatTime(32), BeatTime(24) }
                    });
                    CreateEntity(new Boneslab(0, 20, (int)BeatTime(4), (int)BeatTime(120)));
                    SetBox(290, 210, 130);
                }
                if (InBeat(512))
                {
                    SetSoul(0);
                    SetBox(290, 200, 200);
                }
                if (InBeat(512, 512 + 64 - 12) && At0thBeat(8))
                {
                    CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), BeatTime(8), 10));
                }
                if (InBeat(576))
                {
                    SetBox(280, 220, 160);
                    CreateEntity(new Boneslab(90, 20, (int)BeatTime(2), (int)BeatTime(124), Motions.LengthRoute.sin,
                        new float[] { 40, BeatTime(16), 0, 70 }));
                    CreateEntity(new Boneslab(270, 20, (int)BeatTime(2), (int)BeatTime(124), Motions.LengthRoute.sin,
                        new float[] { 40, BeatTime(16), BeatTime(8), 70 }));
                }
                if (InBeat(576 + 128))
                {
                    CreateEntity(new Boneslab(90, 20, (int)BeatTime(2), (int)BeatTime(124), Motions.LengthRoute.sin,
                        new float[] { 40, BeatTime(16), 0, 60 }));
                    CreateEntity(new Boneslab(270, 20, (int)BeatTime(2), (int)BeatTime(124), Motions.LengthRoute.sin,
                        new float[] { 40, BeatTime(16), BeatTime(8), 60 }));
                    SetSoul(3);
                }
                if (InBeat(576, 576 + 256 - 4) && At0thBeat(8))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 0, 90, 180, 270 }));
                }
                if (InBeat(832))
                {
                    HeartAttribute.JumpSpeed = 4.5f;
                    SetBox(290, 200, 160);
                    SetSoul(2);
                }
                if (InBeat(832 + 4, 832 + 96 - 4) && At0thBeat(8))
                {
                    CreateBone(new DownBone(false, 5, 16));
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 5, 16));
                    CreateBone(new UpBone(true, 5, 111));
                    CreateBone(new UpBone(false, 5, 111));
                }
                if (InBeat(832 + 96))
                {
                    SetBox(290, 140, 140);
                    PlaySound(Sounds.Ding);
                    SetSoul(0);
                }
                if (InBeat(832 + 95.0f, 832 + 109.0f))
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 180 * 0.1f;
                }
                if (InBeat(928 + 1, 928 + 128 - 4) && At0thBeat(1))
                {
                    CreateEntity(new GunBullet(new Vector2(320, 290), BeatTime(4), GametimeF * 2));
                }
                if (InBeat(928 + 4, 928 + 128 - 12) && At0thBeat(8))
                {
                    CreateGB(new NormalGB(Heart.Centre + GetVector2(176, Rand(0, 359)), Heart.Centre, new Vector2(1.0F, 0.5F), BeatTime(8), BeatTime(3)));
                }
                if (InBeat(928 + 127.0f, 928 + 149.0f))
                {
                    ScreenDrawing.ScreenAngle *= 0.9f;
                }

                if (InBeat(1056, 1056 + 128))
                {
                    if (overPos)
                    {
                        overPos = false;
                        gravity *= -0.35f;
                    }
                    gravity += 0.11f;
                    if (At0thBeat(16))
                    {
                        gravity = -4.8f;
                        foreach (var v in GetAll<Platform>())
                        {
                            v.Centre = new Vector2(v.Centre.X, 318);
                        }
                    }
                    if (AtKthBeat(16, BeatTime(14)))
                    {
                        stableID = Rand(1, 3);
                    }
                    float mod = (GametimeF / BeatTime(1)) % 16f;
                    curHeight = mod >= 14 ? AdvanceFunctions.Sin01((mod - 14) / 2f) * -20f : mod <= 6 ? AdvanceFunctions.Sin01(mod / 6f) * 60f : 0;
                    ;
                }
                if (InBeat(1056))
                {
                    SetSoul(2);
                    SetBox(290, 220, 135);

                    Func<ICustomLength, float> lengthControl = (s) =>
                    {
                        CustomBone b = s as CustomBone;
                        return GametimeF > BeatTime(1056 + 124)
                            ? -MathF.Pow(GametimeF - BeatTime(1056 + 124), 1.7f) + 54
                            : b.ContainTag(stableID.ToString()) || s.AppearTime <= BeatTime(16) ? 54 : 54 + curHeight * 2;
                    };
                    Func<ICustomMotion, Vector2> action = (s) =>
                    {
                        Platform p = s as Platform;
                        if (p.ContainTag(stableID.ToString()) || s.AppearTime <= BeatTime(16)) return Vector2.Zero;
                        float oldPosY = p.Centre.Y;
                        float newPosY = oldPosY + gravity * 0.5f;
                        if (Math.Abs(gravity) < 0.2f) newPosY = oldPosY;
                        if (newPosY > 318)
                        {
                            overPos = true;
                            newPosY = 318;
                        }
                        if (newPosY > 323 - curHeight)
                        {
                            newPosY = 323 - curHeight;
                        }
                        return new Vector2(0, newPosY - 318);
                    };

                    CreatePlatform(new Platform(0, new Vector2(250, 318), action, 0, 39, BeatTime(125))
                    { Tags = new string[] { "1" } });
                    CreatePlatform(new Platform(0, new Vector2(320, 318), action, 0, 39, BeatTime(125))
                    { Tags = new string[] { "2" } });
                    CreatePlatform(new Platform(0, new Vector2(390, 318), action, 0, 39, BeatTime(125))
                    { Tags = new string[] { "3" } });

                    for (int i = 0; i < 3; i++)
                    {
                        CreateBone(new CustomBone(new Vector2(i * 10 + 270 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "0" },
                            LengthRoute = lengthControl
                        });
                        CreateBone(new CustomBone(new Vector2(i * 10 + 340 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "0" },
                            LengthRoute = lengthControl
                        });
                        CreateBone(new CustomBone(new Vector2(i * 10 + 200 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "0" },
                            LengthRoute = lengthControl
                        });
                        CreateBone(new CustomBone(new Vector2(i * 10 + 410 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "0" },
                            LengthRoute = lengthControl
                        });
                    }
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new Vector2(i * 10 + 230 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "1" },
                            LengthRoute = lengthControl
                        });
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new Vector2(i * 10 + 300 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "2" },
                            LengthRoute = lengthControl
                        });
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new Vector2(i * 10 + 370 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "3" },
                            LengthRoute = lengthControl
                        });
                }
                if (InBeat(1056, 1056 + 128 - 16) && AtKthBeat(16, BeatTime(7)))
                {
                    CreateEntity(new GunBullet(new Vector2(320, 240), BeatTime(8), new float[] { 0, 180 }));
                    CreateEntity(new GunBullet(new Vector2(320, 285), BeatTime(8), new float[] { 0, 180 }));
                    CreateEntity(new GunBullet(new Vector2(320, 262), BeatTime(6), new float[] { 0, 180 }));
                }
                if (InBeat(1184))
                {
                    SetBox(290, 200, 140);
                }
                if (InBeat(1184 + 64))
                {
                    Heart.GiveForce(180, 9);
                }
                if (InBeat(1184, 1184 + 64 - 8) && At0thBeat(8))
                {
                    float height = Rand(15, 60);
                    CreateBone(new DownBone(false, 3.0f, height));
                    CreateBone(new UpBone(false, 3.0f, 103 - height));
                }
                if (InBeat(1184 + 64 - 1, 1184 + 128 - 16) && At0thBeat(8))
                {
                    float height = Rand(15, 60);
                    CreateBone(new UpBone(false, 3.0f, height));
                    CreateBone(new DownBone(false, 3.0f, 103 - height));
                }
            }

            public void Extreme()
            {
                if (InBeat(0, 128 - 1) && At0thBeat(16))
                {
                    for (int i = 0; i < 20; i++) CreateBone(new SideCircleBone(i * 18, 3, 95, BeatTime(7.3f)));
                }
                if (InBeat(0, 128 - 1) && AtKthBeat(16, BeatTime(3.9f)))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 0, 90, 270, 180 }));
                }
                if (InBeat(0, 128 - 1) && AtKthBeat(16, BeatTime(7.2f)))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 45, 135, 225, 315 }));
                }
                if (InBeat(0, 128 - 1) && AtKthBeat(16, BeatTime(10f)))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 0, 90, 180, 270 }));
                }

                if (InBeat(128, 256 - 12) && At0thBeat(4))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(8),
                        new float[] { Rand(0, 359), Rand(0, 359) }));
                }

                if (InBeat(256))
                {
                    HeartAttribute.Gravity = 6.7f;
                    HeartAttribute.JumpSpeed = 5.1f;
                    SetBox(295, 200, 85);
                    SetSoul(2);
                }
                if (InBeat(256 + 2, 256 + 127) && GametimeF % 6 == 0)
                {
                    float upPos = Sin(GametimeF * 3) * 39f - 11f;
                    float downPos = Sin(GametimeF * 3 + 180) * 39f - 11f;
                    if (upPos > 0) CreateBone(new UpBone(false, 2.2f, upPos));
                    if (downPos > 0) CreateBone(new DownBone(false, 2.2f, downPos));
                }
                if (InBeat(256, 384 + 127) && AtKthBeat(16, BeatTime(6)))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(2), new float[] { 0, 45, 90, 135, 180, 225, 270, 315 }));
                }
                if (InBeat(256, 384) && AtKthBeat(16, BeatTime(6)))
                {
                    CreateEntity(new GunBullet(new Vector2(260, 295), BeatTime(6), new float[] { 0, 45, 135, 180, 225, 315 }));
                    CreateEntity(new GunBullet(new Vector2(380, 295), BeatTime(6), new float[] { 0, 45, 135, 180, 225, 315 }));
                }
                if (InBeat(256, 384 + 127) && AtKthBeat(16, 0))
                {
                    CreateEntity(new GunBullet(new Vector2(320, 295), BeatTime(4), new float[] { 0, 180 }));
                }
                if (InBeat(256, 384) && AtKthBeat(16, BeatTime(12)))
                {
                    CreateEntity(new GunBullet(new Vector2(320, 295), BeatTime(4), new float[] { 0, 180 }));
                }
                if (InBeat(384))
                {
                    CreatePlatform(new Platform(1, new Vector2(320, 310), Motions.PositionRoute.YAxisSin, 0, 40, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 80, BeatTime(32), BeatTime(8) }
                    });
                    CreatePlatform(new Platform(0, new Vector2(320, 310), Motions.PositionRoute.YAxisSin, 0, 40, BeatTime(124))
                    {
                        PositionRouteParam = new float[] { 0, 80, BeatTime(32), BeatTime(24) }
                    });
                    CreateEntity(new Boneslab(0, 20, (int)BeatTime(4), (int)BeatTime(120)));
                    SetBox(290, 210, 130);
                }
                if (InBeat(512))
                {
                    SetSoul(0);
                    SetBox(290, 200, 200);
                }
                if (InBeat(512, 512 + 64 - 12) && At0thBeat(8))
                {
                    CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), BeatTime(8), 10));
                }
                if (InBeat(512 + 32 - 8, 512 + 64 - 12) && At0thBeat(8))
                {
                    CreateGB(new NormalGB(Heart.Centre + GetVector2(150, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), BeatTime(8), 10));
                }
                if (InBeat(256))
                {
                    PlaySound(Sounds.heal);
                    HeartAttribute.MaxHP = 92;
                }

                if (InBeat(576))
                {
                    SetBox(280, 220, 160);
                    CreateEntity(new Boneslab(90, 20, (int)BeatTime(2), (int)BeatTime(124), Motions.LengthRoute.sin,
                        new float[] { 40, BeatTime(16), 0, 70 }));
                    CreateEntity(new Boneslab(270, 20, (int)BeatTime(2), (int)BeatTime(124), Motions.LengthRoute.sin,
                        new float[] { 40, BeatTime(16), BeatTime(8), 70 }));
                }
                if (InBeat(576 + 128 - 4))
                {
                    CreateEntity(new Boneslab(90, 20, (int)BeatTime(2), (int)BeatTime(124), Motions.LengthRoute.sin,
                        new float[] { 40, BeatTime(16), 0, 60 }));
                    CreateEntity(new Boneslab(270, 20, (int)BeatTime(2), (int)BeatTime(124), Motions.LengthRoute.sin,
                        new float[] { 40, BeatTime(16), BeatTime(8), 60 }));
                    SetSoul(3);
                }

                if (InBeat(576))
                {
                    PlaySound(Sounds.heal);
                    HeartAttribute.MaxHP = 92;
                }

                if (InBeat(576, 576 + 256 - 4) && At0thBeat(6))
                {
                    CreateBone(new CustomBone(new Vector2(180, Rand(210, 350)), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        PositionRouteParam = new float[] { 2.0f, 0.0f },
                        LengthRouteParam = new float[] { 20.0f },
                        RotationRouteParam = new float[] { 2.0f, Rand(0, 359) }
                    });
                    CreateBone(new CustomBone(new Vector2(640 - 180, Rand(210, 350)), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        PositionRouteParam = new float[] { -2.0f, 0.0f },
                        LengthRouteParam = new float[] { 20.0f },
                        RotationRouteParam = new float[] { -2.0f, Rand(0, 359) }
                    });
                }
                if (InBeat(576, 576 + 256 - 4) && At0thBeat(8))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 0, 90, 180, 270 }));
                }
                if (InBeat(832))
                {
                    HeartAttribute.JumpSpeed = 4.5f;
                    SetBox(290, 200, 160);
                    SetSoul(2);
                }
                if (InBeat(832 + 4, 832 + 96 - 4) && At0thBeat(8))
                {
                    CreateBone(new DownBone(false, 5, 16));
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 5, 16));
                    CreateBone(new UpBone(true, 5, 111));
                    CreateBone(new UpBone(false, 5, 111));
                }
                if (InBeat(832 + 96))
                {
                    SetBox(290, 140, 140);
                    PlaySound(Sounds.Ding);
                    SetSoul(0);
                }
                if (InBeat(832 + 95.0f, 832 + 109.0f))
                {
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.9f + 180 * 0.1f;
                }

                if (InBeat(928))
                {
                    PlaySound(Sounds.heal);
                    HeartAttribute.MaxHP = 92;
                }

                if (InBeat(928 + 1, 928 + 128 - 4) && At0thBeat(1))
                {
                    CreateEntity(new GunBullet(new Vector2(320, 290), BeatTime(4), GametimeF * 2));
                }
                if (InBeat(928 + 4, 928 + 128 - 12) && At0thBeat(8))
                {
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(4), new float[] { 0, 45, 90, 135, 180, 225, 270, 315 }));
                    CreateGB(new NormalGB(Heart.Centre + GetVector2(176, Rand(0, 359)), Heart.Centre, new Vector2(1.0F, 0.5F), BeatTime(8), BeatTime(3)));
                }
                if (InBeat(928 + 127.0f, 928 + 149.0f))
                {
                    ScreenDrawing.ScreenAngle *= 0.9f;
                }

                if (InBeat(1056, 1056 + 128))
                {
                    if (overPos)
                    {
                        overPos = false;
                        gravity *= -0.35f;
                    }
                    gravity += 0.11f;
                    if (At0thBeat(16))
                    {
                        gravity = -4.8f;
                        foreach (var v in GetAll<Platform>())
                        {
                            v.Centre = new Vector2(v.Centre.X, 318);
                        }
                    }
                    if (AtKthBeat(16, BeatTime(14)))
                    {
                        stableID = Rand(1, 3);
                    }
                    float mod = (GametimeF / BeatTime(1)) % 16f;
                    curHeight = mod >= 14 ? AdvanceFunctions.Sin01((mod - 14) / 2f) * -20f : mod <= 6 ? AdvanceFunctions.Sin01(mod / 6f) * 60f : 0;
                }
                if (InBeat(1056))
                {
                    SetSoul(2);
                    SetBox(290, 220, 135);

                    Func<ICustomLength, float> lengthControl = (s) =>
                    {
                        CustomBone b = s as CustomBone;
                        return GametimeF > BeatTime(1056 + 124)
                            ? -MathF.Pow(GametimeF - BeatTime(1056 + 124), 1.7f) + 54
                            : b.ContainTag(stableID.ToString()) || s.AppearTime <= BeatTime(16) ? 54 : 54 + curHeight * 2;
                    };
                    Func<ICustomMotion, Vector2> action = (s) =>
                    {
                        Platform p = s as Platform;
                        if (p.ContainTag(stableID.ToString()) || s.AppearTime <= BeatTime(16)) return Vector2.Zero;
                        float oldPosY = p.Centre.Y;
                        float newPosY = oldPosY + gravity * 0.5f;
                        if (Math.Abs(gravity) < 0.2f) newPosY = oldPosY;
                        if (newPosY > 318)
                        {
                            overPos = true;
                            newPosY = 318;
                        }
                        if (newPosY > 323 - curHeight)
                        {
                            newPosY = 323 - curHeight;
                        }
                        return new Vector2(0, newPosY - 318);
                    };

                    CreatePlatform(new Platform(0, new Vector2(250, 318), action, 0, 39, BeatTime(125))
                    { Tags = new string[] { "1" } });
                    CreatePlatform(new Platform(0, new Vector2(320, 318), action, 0, 39, BeatTime(125))
                    { Tags = new string[] { "2" } });
                    CreatePlatform(new Platform(0, new Vector2(390, 318), action, 0, 39, BeatTime(125))
                    { Tags = new string[] { "3" } });

                    for (int i = 0; i < 3; i++)
                    {
                        CreateBone(new CustomBone(new Vector2(i * 10 + 270 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "0" },
                            LengthRoute = lengthControl
                        });
                        CreateBone(new CustomBone(new Vector2(i * 10 + 340 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "0" },
                            LengthRoute = lengthControl
                        });
                        CreateBone(new CustomBone(new Vector2(i * 10 + 200 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "0" },
                            LengthRoute = lengthControl
                        });
                        CreateBone(new CustomBone(new Vector2(i * 10 + 410 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "0" },
                            LengthRoute = lengthControl
                        });
                    }
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new Vector2(i * 10 + 230 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "1" },
                            LengthRoute = lengthControl
                        });
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new Vector2(i * 10 + 300 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "2" },
                            LengthRoute = lengthControl
                        });
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new Vector2(i * 10 + 370 + 5, 354), Motions.PositionRoute.stableValue, 0, 59)
                        {
                            Tags = new string[] { "3" },
                            LengthRoute = lengthControl
                        });
                }
                if (InBeat(1056, 1056 + 128 - 16) && AtKthBeat(16, BeatTime(7)))
                {
                    CreateEntity(new GunBullet(new Vector2(320, 240), BeatTime(8), new float[] { 0, 180 }));
                    CreateEntity(new GunBullet(new Vector2(320, 285), BeatTime(8), new float[] { 0, 180 }));
                    CreateEntity(new GunBullet(new Vector2(320, 262), BeatTime(6), new float[] { 0, 180 }));
                }
                if (InBeat(1056, 1056 + 128 - 16) && AtKthBeat(16, 0))
                {
                    CreateEntity(new GunBullet(new Vector2(320, 285), BeatTime(8), 90));
                    CreateEntity(new GunBullet(new Vector2(250, 285), BeatTime(8), 90));
                    CreateEntity(new GunBullet(new Vector2(390, 285), BeatTime(8), 90));
                    CreateEntity(new GunBullet(Heart.Centre, BeatTime(12), new float[] { 0, 90, 180, 270 }));
                }
                if (InBeat(1184))
                {
                    SetBox(290, 200, 140);
                }
                if (InBeat(1184 + 64))
                {
                    Heart.GiveForce(180, 9);
                }
                if (InBeat(1184, 1184 + 64 - 8) && At0thBeat(8))
                {
                    float height = Rand(15, 60);
                    CreateBone(new DownBone(false, 3.0f, height));
                    CreateBone(new UpBone(false, 3.0f, 103 - height));
                }
                if (InBeat(1184 + 64 - 1, 1184 + 128 - 16) && At0thBeat(8))
                {
                    float height = Rand(15, 60);
                    CreateBone(new UpBone(false, 3.0f, height));
                    CreateBone(new DownBone(false, 3.0f, 103 - height));
                }
            }

            public void Start()
            {
                ScreenDrawing.ThemeColor = Color.Red;
                HeartAttribute.Speed = 3.0f;
                SetBox(290, 130, 130);
                TP(320, 290);
                SetSoul(0);
                // SetSoul(0); 
                HeartAttribute.MaxHP = 92;
                HeartAttribute.KR = true;
                gravity = 0;
                HeartAttribute.KRDamage = 5;
                HeartAttribute.Gravity = 5.6f;
                HeartAttribute.JumpSpeed = 4.6f;
            }
        }
    }
}