﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class SpaceDrift : IChampionShip
    {
        public SpaceDrift()
        {
            Game.instance = new Game();

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Noob);
            difficulties.Add("div.1", Difficulty.Normal);
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

            public Game() : base(7.503f)
            { }

            public static Game instance;

            public string Music => "SpaceDrift";

            public string FightName => "SpaceDrift";

            class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => "T-mas";
                public override string SongAuthor => "Appler";

                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.0f),
                            new(Difficulty.Normal, 12.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 2.5f),
                            new(Difficulty.Normal, 10.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 7.0f),
                            new(Difficulty.Normal, 15.5f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            #region Non-ChampionShip
            public void Easy()
            {
                throw new System.NotImplementedException();
            }
            public void Extreme()
            {
                throw new System.NotImplementedException();
            }
            public void Hard()
            {
                throw new System.NotImplementedException();
            }
            public void ExtremePlus()
            {
                throw new System.NotImplementedException();
            }
            #endregion

            private void CirssCrossGB(Vector2 centre, int count, float rotation, float waitingBeat)
            {
                float rot = rotation;
                for (int i = 0; i < count; i++)
                {
                    Vector2 pos = centre + GetVector2(165, rotation + i * 360f / count);
                    CreateGB(new NormalGB(pos, pos + GetVector2(50, Rand(0, 359)), new(1, 0.5f), BeatTime(waitingBeat), BeatTime(1)));
                }
            }

            private Vector2 screenMission = Vector2.Zero;

            public void Noob()
            {
                if (InBeat(0.00001f, 64 - 4) && At0thBeat(16))
                {
                    CreateEntity(new Boneslab(Rand(0, 3) * 90, 90, (int)BeatTime(8), 24)
                    {
                        BoneProtruded = () =>
                        {
                            PlaySound(Sounds.pierce);
                        }
                    });
                }
                if (InBeat(63, 128 - 4) && At0thBeat(32))
                {
                    Vector2 v;
                    CreateGB(new NormalGB(
                        v = Heart.Centre + GetVector2(156, Rand(0, 359)),
                        v + GetVector2(84, Rand(0, 359)),
                        new(1, 0.5f),
                        BeatTime(8),
                        14
                    ));
                }
                if (InBeat(63, 128 - 4) && At0thBeat(16))
                {
                    CreateEntity(new Boneslab(Rand(0, 3) * 90, 90, (int)BeatTime(8), 24)
                    {
                        BoneProtruded = () =>
                        {
                            PlaySound(Sounds.pierce);
                        }
                    });
                }

                if (InBeat(128 - 2))
                {
                    SetBox(290, 320, 140);
                    SetSoul(2);
                    Heart.GiveForce(0, 10);
                }
                if (InBeat(128, 128 + 52) & At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(false, 2.2f, 15));
                    CreateBone(new DownBone(true, 2.2f, 15));
                    CreateBone(new UpBone(true, 2.2f, 87));
                    CreateBone(new UpBone(false, 2.2f, 87));
                }

                if (InBeat(192 - 2))
                {
                    SetBox(290, 320, 140);
                    SetSoul(2);
                    Heart.GiveForce(180, 10);
                }
                if (InBeat(192, 256 - 2) & At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 2.5f, 50));
                    CreateBone(new UpBone(false, 2.5f, 50));
                }
                if (InBeat(256, 256 + 52) & At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(false, 2.5f, 50));
                    CreateBone(new UpBone(true, 2.5f, 50));
                }

                if (InBeat(320))
                {
                    Heart.RotateTo(0);
                    SetSoul(0);
                    SetBox(300, 180, 150);
                }
                if (InBeat(320, 320 + 64 - 4) && At0thBeat(8))
                {
                    float len1 = Rand(20, 70), len2 = 90 - len1;
                    CreateBone(new DownBone(false, 234, 0, len1)
                    {
                        Tags = new string[] { "A" },
                        Extras = 0
                    });
                    CreateBone(new UpBone(false, 234, 0, len2)
                    {
                        Tags = new string[] { "A" },
                        Extras = 0
                    });
                }
                if (InBeat(320 + 64, 320 + 126 - 4) && At0thBeat(8))
                {
                    float len1 = Rand(30, 105), len2 = 135 - len1;
                    CreateBone(new DownBone(true, 406, 0, len1)
                    {
                        Tags = new string[] { "A" },
                        Extras = 0,
                        ColorType = Rand(1, 2)
                    });
                    CreateBone(new UpBone(true, 406, 0, len2)
                    {
                        Tags = new string[] { "A" },
                        Extras = 0,
                        ColorType = Rand(1, 2)
                    });
                }
                if (InBeat(320, 320 + 128))
                {
                    dynamic all = GetAll<DownBone>("A");
                    foreach (var v in all)
                    {
                        if ((int)v.Extras >= 10)
                            v.Speed = v.Speed * 0.92f + 3f * 0.08f;
                        else v.Extras = (int)v.Extras + 1;
                    }
                    all = GetAll<UpBone>("A");
                    foreach (var v in all)
                    {
                        if ((int)v.Extras >= 10)
                            v.Speed = v.Speed * 0.92f + 3f * 0.08f;
                        else v.Extras = (int)v.Extras + 1;
                    }
                }

                if (InBeat(320 + 128))
                {
                    SetBox(290, 320, 160);
                    SetSoul(2);
                    Heart.GiveForce(0, 10);
                }
                if (InBeat(320 + 128, 320 + 128 + 62))
                {
                    if (At0thBeat(8))
                    {
                        CreateBone(new CustomBone(
                            new Vector2(500, 290 - 80),
                            Motions.PositionRoute.linear,
                            (s) => { return Sin01(s.AppearTime / BeatTime(8)) * 90 + 101; },
                            (s) => { return -30; }
                        )
                        { PositionRouteParam = new float[] { -3, 0 } });
                        CreateBone(new CustomBone(
                            new Vector2(500 + 93, 290 + 80),
                            Motions.PositionRoute.linear,
                            (s) => { return Sin01(s.AppearTime / BeatTime(8)) * 90 + 101; },
                            (s) => { return -30; }
                        )
                        { PositionRouteParam = new float[] { -3, 0 } });
                    }
                }
                if (InBeat(320 + 128 + 64, 320 + 192 + 52))
                {
                    if (At0thBeat(8))
                    {
                        CreateBone(new CustomBone(
                            new Vector2(160, 290 - 80),
                            Motions.PositionRoute.linear,
                            (s) => { return Sin01(s.AppearTime / BeatTime(8)) * 90 + 101; },
                            (s) => { return 30; }
                        )
                        { PositionRouteParam = new float[] { 3, 0 } });
                        CreateBone(new CustomBone(
                            new Vector2(160 - 93, 290 + 80),
                            Motions.PositionRoute.linear,
                            (s) => { return Sin01(s.AppearTime / BeatTime(8)) * 90 + 101; },
                            (s) => { return 30; }
                        )
                        { PositionRouteParam = new float[] { 3, 0 } });
                    }
                }

                if (InBeat(576))
                {
                    SetSoul(0);
                    SetBox(300, 240, 240);
                }
                if (InBeat(576, 576 + 64 - 16) && At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                    if (RandSignal() > 0)
                    {
                        int signal;
                        CreateBone(new CustomBone(
                            new(320 + Rand(-110, 110), 300 + (signal = RandSignal()) * 126),
                            Motions.PositionRoute.linear,
                            Motions.LengthRoute.stableValue,
                            Motions.RotationRoute.linear
                        )
                        {
                            PositionRouteParam = new[] { 0f, -2 * signal },
                            RotationRouteParam = new[] { 3f * RandSignal(), Rand(0, 359) },
                            LengthRouteParam = new[] { 24f }
                        });
                    }
                    else
                    {
                        int signal;
                        CreateBone(new CustomBone(
                            new(320 + (signal = RandSignal()) * 126, 300 + Rand(-110, 110)),
                            Motions.PositionRoute.linear,
                            Motions.LengthRoute.stableValue,
                            Motions.RotationRoute.linear
                        )
                        {
                            PositionRouteParam = new[] { -2f * signal, 0 },
                            RotationRouteParam = new[] { 3f * RandSignal(), Rand(0, 359) },
                            LengthRouteParam = new[] { 24f }
                        });
                    }
                }
                if (InBeat(576 + 64))
                {
                    SetSoul(2);
                    Heart.GiveForce(90, 10);
                    SetBox(300, 260, 140);
                    CreateEntity(new Boneslab(90, 42, (int)BeatTime(8), (int)BeatTime(54)));
                    CreateEntity(new Platform(0, new Vector2(240, 300 - 32), Motions.PositionRoute.cameFromLeft, 90, 34, BeatTime(63)));
                    CreateEntity(new Platform(0, new Vector2(240, 300 + 32), Motions.PositionRoute.cameFromLeft, 90, 34, BeatTime(63)));
                    UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(new(480, 300), 12, 2f, 300, BeatTime(60), 0, true);
                }
                if (InBeat(576 + 64, 576 + 128 - 24) && At0thBeat(16))
                {
                    CreateGB(new NormalGB(new(450, 300 + RandSignal() * 32), Heart.Centre, new(1, 1), 180, BeatTime(16), BeatTime(4)));
                }

                if (InBeat(704))
                {
                    Heart.RotateTo(0);
                    SetSoul(0);
                    SetBox(300, 240, 240);
                }
                if (InBeat(704, 704 + 64 - 16) && At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                    if (RandSignal() > 0)
                    {
                        int signal;
                        CreateBone(new CustomBone(
                            new(320 + Rand(-110, 110), 300 + (signal = RandSignal()) * 126),
                            Motions.PositionRoute.linear,
                            Motions.LengthRoute.stableValue,
                            Motions.RotationRoute.linear
                        )
                        {
                            PositionRouteParam = new[] { 0f, -2 * signal },
                            RotationRouteParam = new[] { 3f * RandSignal(), Rand(0, 359) },
                            LengthRouteParam = new[] { 24f }
                        });
                    }
                    else
                    {
                        int signal;
                        CreateBone(new CustomBone(
                            new(320 + (signal = RandSignal()) * 126, 300 + Rand(-110, 110)),
                            Motions.PositionRoute.linear,
                            Motions.LengthRoute.stableValue,
                            Motions.RotationRoute.linear
                        )
                        {
                            PositionRouteParam = new[] { -2f * signal, 0 },
                            RotationRouteParam = new[] { 3f * RandSignal(), Rand(0, 359) },
                            LengthRouteParam = new[] { 24f }
                        });
                    }
                }
                if (InBeat(704 + 64))
                {
                    SetSoul(2);
                    Heart.GiveForce(270, 10);
                    SetBox(300, 260, 140);
                    CreateEntity(new Boneslab(270, 42, (int)BeatTime(8), (int)BeatTime(54)));
                    CreateEntity(new Platform(0, new Vector2(400, 300 - 32), Motions.PositionRoute.cameFromRight, 270, 34, BeatTime(63)));
                    CreateEntity(new Platform(0, new Vector2(400, 300 + 32), Motions.PositionRoute.cameFromRight, 270, 34, BeatTime(63)));
                    UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(new(160, 300), 12, 2f, 300, BeatTime(60), 0, true);
                }
                if (InBeat(704 + 64, 704 + 128 - 24) && At0thBeat(16))
                {
                    CreateGB(new NormalGB(new(190, 300 + RandSignal() * 32), Heart.Centre, new(1, 1), 0, BeatTime(16), BeatTime(4)));
                }
                if (InBeat(832))
                {
                    SetSoul(0);
                    SetBox(300, 160, 160);
                    Heart.RotateTo(0);
                }
                if (InBeat(832, 832 + 128 - 16) && At0thBeat(8))
                {
                    Vector2 v;
                    CreateGB(new NormalGB(v = Heart.Centre + GetVector2(144, Rand(0, 359)), v + GetVector2(108, Rand(0, 359)),
                        new(1, 0.5f),
                        BeatTime(8),
                        10
                    ));
                }
                if (InBeat(960))
                {
                    SetSoul(2);
                    SetBox(300, 260, 160);
                    CreateEntity(new Platform(0, new(320, 340), Motions.PositionRoute.YAxisSin, 0, 40, BeatTime(125))
                    {
                        PositionRouteParam = new[] { 0, 100, BeatTime(64), 0 },
                        Tags = new[] { "A" }
                    });
                }
                if (InBeat(960 + 64))
                {
                    var v = GetAll<Platform>("A")[0];
                    v.ChangeType();
                    PlaySound(Sounds.Ding);
                    UndyneFight_Ex.Entities.Advanced.BarrageExtend.CreateShinyEffect(v);
                }
                if (InBeat(960 + 2, 960 + 128 + 124) && Gametime % 4 == 0)
                {
                    CreateBone(new DownBone(false, 4f, 16) { MarkScore = false });
                }
                if (InBeat(960 + 4, 960 + 124) && At0thBeat(16))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 2f, 64) { });
                }
                if (InBeat(960 + 124))
                {
                    CreatePlatform(new Platform(1, new(320, 340), Motions.PositionRoute.cameFromDown, 0, 45, BeatTime(125)));
                    CreatePlatform(new Platform(1, new(320, 280), Motions.PositionRoute.cameFromUp, 0, 45, BeatTime(125)));
                }
                if (InBeat(1218))
                {
                    SetSoul(0);
                    SetBox(300, 160, 160);
                    TP(320, 300);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new SideCircleBone(i * 90, 2, 62, BeatTime(124)) { Tags = new[] { "A" } });
                }
                if (InBeat(1220))
                {
                    // CreateBone(new CentreCircleBone(0, 2.4f, 82, BeatTime(124)) { ColorType = 1 });
                }
                if (InBeat(1218, 1218 + 125))
                {
                    var v = GetAll<SideCircleBone>("A");
                    if (AtKthBeat(32, BeatTime(8)))
                    {
                        CreateGB(new NormalGB(new(100, 300), Heart.Centre, new(1.0f, 1.0f), 0, BeatTime(8), 10));
                        CreateGB(new NormalGB(new(540, 300), Heart.Centre, new(1.0f, 1.0f), 180, BeatTime(8), 10));
                    }
                    if (AtKthBeat(32, BeatTime(24)))
                    {
                        CreateGB(new NormalGB(new(320, 100), Heart.Centre, new(1.0f, 1.0f), 90, BeatTime(8), 10));
                        CreateGB(new NormalGB(new(320, 500), Heart.Centre, new(1.0f, 1.0f), 270, BeatTime(8), 10));
                    }
                    if (At0thBeat(8))
                    {
                        foreach (var bone in v)
                        {
                            bone.RotateSpeed = 2.7f;
                        }
                    }
                    foreach (var bone in v)
                    {
                        bone.RotateSpeed = bone.RotateSpeed * 0.94f + 0.5f * 0.06f;
                    }
                }

                if (InBeat(1346))
                {
                    SetSoul(0);
                    SetBox(300, 160, 160);
                    Heart.RotateTo(0);
                }
                if (InBeat(1346, 1346 + 128 - 16) && At0thBeat(16))
                {
                    Vector2 v;
                    for (int i = 0; i < 2; i++)
                        CreateGB(new NormalGB(v = Heart.Centre + GetVector2(144, Rand(0, 359)), v + GetVector2(108, Rand(0, 359)),
                            new(1, 0.5f),
                            BeatTime(8),
                            20
                        ));
                }

                if (InBeat(1472 - 2))
                {
                    SetSoul(2);
                    SetBox(300, 260, 160);
                }
                if (InBeat(1472, 1472 + 128 - 4) && At0thBeat(16))
                {
                    CreateBone(new DownBone(false, 5, 20));
                    CreateBone(new DownBone(true, 5, 20));
                }
                if (InBeat(1472, 1472 + 128 - 4) && AtKthBeat(16, BeatTime(8)))
                {
                    CreateBone(new UpBone(false, 5, 20));
                    CreateBone(new UpBone(true, 5, 20));
                }
                if (InBeat(1472 - 2, 1472 + 128 - 4) && AtKthBeat(16, BeatTime(15)))
                { Heart.GiveForce(0, 12); }
                if (InBeat(1472, 1472 + 128 - 4) && AtKthBeat(16, BeatTime(7)))
                { Heart.GiveForce(180, 12); }
                if (InBeat(1600))
                {
                    Heart.RotateTo(0);
                    SetSoul(0);
                    SetBox(300, 160, 160);
                    TP(320, 300);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new SideCircleBone(i * 90, 2, 62, BeatTime(124)) { Tags = new[] { "A" } });
                }
                if (InBeat(1602))
                {
                    // CreateBone(new CentreCircleBone(0, 2.4f, 82, BeatTime(124)) { ColorType = 1 });
                }
                if (InBeat(1600, 1600 + 125))
                {
                    var v = GetAll<SideCircleBone>("A");
                    if (AtKthBeat(32, BeatTime(8)))
                    {
                        CreateGB(new NormalGB(new(100, 300), Heart.Centre, new(1.0f, 1.0f), 0, BeatTime(8), 10));
                        CreateGB(new NormalGB(new(540, 300), Heart.Centre, new(1.0f, 1.0f), 180, BeatTime(8), 10));
                    }
                    if (AtKthBeat(32, BeatTime(24)))
                    {
                        CreateGB(new NormalGB(new(320, 100), Heart.Centre, new(1.0f, 1.0f), 90, BeatTime(8), 10));
                        CreateGB(new NormalGB(new(320, 500), Heart.Centre, new(1.0f, 1.0f), 270, BeatTime(8), 10));
                    }
                    if (At0thBeat(8))
                    {
                        foreach (var bone in v)
                        {
                            bone.RotateSpeed = 2.7f;
                        }
                    }
                    foreach (var bone in v)
                    {
                        bone.RotateSpeed = bone.RotateSpeed * 0.94f + 0.5f * 0.06f;
                    }
                }

            }

            public void Normal()
            {
                if (InBeat(0.00001f, 64 - 4) && At0thBeat(16))
                {
                    CreateEntity(new Boneslab(Rand(0, 3) * 90, 90, (int)BeatTime(8), 24)
                    {
                        BoneProtruded = () =>
                        {
                            PlaySound(Sounds.pierce);
                        }
                    });
                    CreateEntity(new Boneslab(((LastRand + 1) % 4) * 90, 90, (int)BeatTime(8), 24)
                    {
                        BoneProtruded = () =>
                        {
                            PlaySound(Sounds.pierce);
                        }
                    });
                }
                if (InBeat(63, 128 - 4) && At0thBeat(16))
                {
                    Vector2 v;
                    CreateGB(new NormalGB(
                        v = Heart.Centre + GetVector2(156, Rand(0, 359)),
                        v + GetVector2(84, Rand(0, 359)),
                        new(1, 0.5f),
                        BeatTime(8),
                        14
                    ));
                }
                if (InBeat(63, 128 - 4) && At0thBeat(16))
                {
                    CreateEntity(new Boneslab(Rand(0, 3) * 90, 90, (int)BeatTime(8), 24)
                    {
                        BoneProtruded = () =>
                        {
                            PlaySound(Sounds.pierce);
                        }
                    });
                    CreateEntity(new Boneslab(((LastRand + 1) % 4) * 90, 90, (int)BeatTime(8), 24)
                    {
                        BoneProtruded = () =>
                        {
                            PlaySound(Sounds.pierce);
                        }
                    });
                }

                if (InBeat(128 - 2))
                {
                    SetBox(290, 320, 140);
                    SetSoul(2);
                    Heart.GiveForce(0, 10);
                }
                if (InBeat(128, 128 + 52) & At0thBeat(8))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(false, 2.2f, 16));
                    CreateBone(new DownBone(true, 2.2f, 16));
                    CreateBone(new UpBone(true, 2.2f, 95));
                    CreateBone(new UpBone(false, 2.2f, 95));
                }

                if (InBeat(192 - 2))
                {
                    SetBox(290, 250, 140);
                    SetSoul(2);
                    Heart.GiveForce(180, 10);
                }
                if (InBeat(192, 256 - 12) & At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 2.6f, 60));
                    CreateBone(new UpBone(false, 2.6f, 60));
                }
                if (InBeat(256, 256 + 52) & At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(false, 2.6f, 60));
                    CreateBone(new UpBone(true, 2.6f, 60));
                }

                if (InBeat(320))
                {
                    Heart.RotateTo(0);
                    SetSoul(0);
                    SetBox(300, 180, 150);
                }
                if (InBeat(320, 320 + 64 - 4) && At0thBeat(8))
                {
                    float len1 = Rand(20, 80), len2 = 100 - len1;
                    CreateBone(new DownBone(false, 234, 0, len1)
                    {
                        Tags = new string[] { "A" },
                        Extras = 0
                    });
                    CreateBone(new UpBone(false, 234, 0, len2)
                    {
                        Tags = new string[] { "A" },
                        Extras = 0
                    });
                }
                if (InBeat(320 + 64, 320 + 126 - 8) && At0thBeat(4))
                {
                    float len1 = Rand(30, 105), len2 = 135 - len1;
                    CreateBone(new DownBone(true, 406, 0, len1)
                    {
                        Tags = new string[] { "A" },
                        Extras = 0,
                        ColorType = Rand(1, 2)
                    });
                    CreateBone(new UpBone(true, 406, 0, len2)
                    {
                        Tags = new string[] { "A" },
                        Extras = 0,
                        ColorType = Rand(1, 2)
                    });
                }
                if (InBeat(320, 320 + 128))
                {
                    dynamic all = GetAll<DownBone>("A");
                    foreach (var v in all)
                    {
                        if ((int)v.Extras >= 10)
                            v.Speed = v.Speed * 0.92f + 3f * 0.08f;
                        else v.Extras = (int)v.Extras + 1;
                    }
                    all = GetAll<UpBone>("A");
                    foreach (var v in all)
                    {
                        if ((int)v.Extras >= 10)
                            v.Speed = v.Speed * 0.92f + 3f * 0.08f;
                        else v.Extras = (int)v.Extras + 1;
                    }
                }

                if (InBeat(320 + 128))
                {
                    SetBox(290, 320, 160);
                    SetSoul(2);
                    Heart.GiveForce(0, 10);
                }
                if (InBeat(320 + 128, 320 + 128 + 62))
                {
                    if (At0thBeat(8))
                    {
                        int p = Rand(0, (int)BeatTime(8));
                        CreateBone(new CustomBone(
                            new Vector2(500, 290 - 80),
                            Motions.PositionRoute.linear,
                            (s) => { return Sin01((s.AppearTime + p) / BeatTime(4)) * 60 + 131; },
                            (s) => { return -30; }
                        )
                        { PositionRouteParam = new float[] { -3, 0 } });
                        CreateBone(new CustomBone(
                            new Vector2(500 + 93, 290 + 80),
                            Motions.PositionRoute.linear,
                            (s) => { return Sin01((s.AppearTime + p) / BeatTime(4)) * 60 + 131; },
                            (s) => { return -30; }
                        )
                        { PositionRouteParam = new float[] { -3, 0 } });
                    }
                }
                if (InBeat(320 + 128 + 64, 320 + 192 + 52))
                {
                    if (At0thBeat(8))
                    {
                        int p = Rand(0, (int)BeatTime(8));
                        CreateBone(new CustomBone(
                            new Vector2(160, 290 - 80),
                            Motions.PositionRoute.linear,
                            (s) => { return Sin01((s.AppearTime + p) / BeatTime(4)) * 60 + 131; },
                            (s) => { return 30; }
                        )
                        { PositionRouteParam = new float[] { 3, 0 } });
                        CreateBone(new CustomBone(
                            new Vector2(160 - 93, 290 + 80),
                            Motions.PositionRoute.linear,
                            (s) => { return Sin01((s.AppearTime + p) / BeatTime(4)) * 60 + 131; },
                            (s) => { return 30; }
                        )
                        { PositionRouteParam = new float[] { 3, 0 } });
                    }
                }

                if (InBeat(576))
                {
                    SetSoul(0);
                    SetBox(300, 240, 240);
                }
                if (InBeat(576, 576 + 64 - 16) && At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                    for (int i = 0; i < 3; i++)
                        if (RandSignal() > 0)
                        {
                            int signal;
                            CreateBone(new CustomBone(
                                new(320 + Rand(-110, 110), 300 + (signal = RandSignal()) * 126),
                                Motions.PositionRoute.linear,
                                Motions.LengthRoute.stableValue,
                                Motions.RotationRoute.linear
                            )
                            {
                                PositionRouteParam = new[] { 0f, -2 * signal },
                                RotationRouteParam = new[] { 3f * RandSignal(), Rand(0, 359) },
                                LengthRouteParam = new[] { 24f }
                            });
                        }
                        else
                        {
                            int signal;
                            CreateBone(new CustomBone(
                                new(320 + (signal = RandSignal()) * 126, 300 + Rand(-110, 110)),
                                Motions.PositionRoute.linear,
                                Motions.LengthRoute.stableValue,
                                Motions.RotationRoute.linear
                            )
                            {
                                PositionRouteParam = new[] { -2f * signal, 0 },
                                RotationRouteParam = new[] { 3f * RandSignal(), Rand(0, 359) },
                                LengthRouteParam = new[] { 24f }
                            });
                        }
                }
                if (InBeat(576 + 64))
                {
                    SetSoul(2);
                    Heart.GiveForce(90, 10);
                    SetBox(300, 260, 140);
                    CreateEntity(new Boneslab(90, 42, (int)BeatTime(8), (int)BeatTime(54)));
                    CreateEntity(new Platform(0, new Vector2(240, 300 - 32), Motions.PositionRoute.cameFromLeft, 90, 34, BeatTime(63)));
                    CreateEntity(new Platform(0, new Vector2(240, 300 + 32), Motions.PositionRoute.cameFromLeft, 90, 34, BeatTime(63)));
                    UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(new(480, 300), 12, 2f, 300, BeatTime(60), 0, true);
                }
                if (InBeat(576 + 64, 576 + 128 - 24) && At0thBeat(8))
                {
                    CreateGB(new NormalGB(new(450, 300 + RandSignal() * 32), Heart.Centre, new(1, 1), 180, BeatTime(16), BeatTime(2)));
                }

                if (InBeat(704))
                {
                    Heart.RotateTo(0);
                    SetSoul(0);
                    SetBox(300, 240, 240);
                }
                if (InBeat(704, 704 + 64 - 16) && At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                    for (int i = 0; i < 3; i++)
                        if (RandSignal() > 0)
                        {
                            int signal;
                            CreateBone(new CustomBone(
                                new(320 + Rand(-110, 110), 300 + (signal = RandSignal()) * 126),
                                Motions.PositionRoute.linear,
                                Motions.LengthRoute.stableValue,
                                Motions.RotationRoute.linear
                            )
                            {
                                PositionRouteParam = new[] { 0f, -2 * signal },
                                RotationRouteParam = new[] { 3f * RandSignal(), Rand(0, 359) },
                                LengthRouteParam = new[] { 24f }
                            });
                        }
                        else
                        {
                            int signal;
                            CreateBone(new CustomBone(
                                new(320 + (signal = RandSignal()) * 126, 300 + Rand(-110, 110)),
                                Motions.PositionRoute.linear,
                                Motions.LengthRoute.stableValue,
                                Motions.RotationRoute.linear
                            )
                            {
                                PositionRouteParam = new[] { -2f * signal, 0 },
                                RotationRouteParam = new[] { 3f * RandSignal(), Rand(0, 359) },
                                LengthRouteParam = new[] { 24f }
                            });
                        }
                }
                if (InBeat(704 + 64))
                {
                    SetSoul(2);
                    Heart.GiveForce(270, 10);
                    SetBox(300, 260, 140);
                    CreateEntity(new Boneslab(270, 42, (int)BeatTime(8), (int)BeatTime(54)));
                    CreateEntity(new Platform(0, new Vector2(400, 300 - 32), Motions.PositionRoute.cameFromRight, 270, 34, BeatTime(63)));
                    CreateEntity(new Platform(0, new Vector2(400, 300 + 32), Motions.PositionRoute.cameFromRight, 270, 34, BeatTime(63)));
                    UndyneFight_Ex.Entities.Advanced.BarrageStruct.BoneWindfall(new(160, 300), 12, 2f, 300, BeatTime(60), 0, true);
                }
                if (InBeat(704 + 64, 704 + 128 - 24) && At0thBeat(8))
                { 
                    CreateGB(new NormalGB(new(190, 300 + RandSignal() * 32), Heart.Centre, new(1, 1), 0, BeatTime(16), BeatTime(2)));
                }
                if (InBeat(832))
                {
                    SetSoul(0);
                    SetBox(300, 160, 160);
                    Heart.RotateTo(0);
                }
                if (InBeat(832, 832 + 128 - 16) && At0thBeat(8))
                {
                    Vector2 v;
                    CreateGB(new NormalGB(v = Heart.Centre + GetVector2(144, Rand(0, 359)), v + GetVector2(108, Rand(0, 359)),
                        new(1, 1f),
                        BeatTime(8),
                        10
                    ));
                }
                if (InBeat(960))
                {
                    SetSoul(2);
                    SetBox(300, 260, 160);
                    CreateEntity(new Platform(0, new(320, 340), Motions.PositionRoute.YAxisSin, 0, 40, BeatTime(125))
                    {
                        PositionRouteParam = new[] { 0, 100, BeatTime(64), 0 },
                        Tags = new[] { "A" }
                    });
                }
                if (InBeat(960 + 64))
                {
                    var v = GetAll<Platform>("A")[0];
                    v.ChangeType();
                    PlaySound(Sounds.Ding);
                    UndyneFight_Ex.Entities.Advanced.BarrageExtend.CreateShinyEffect(v);
                }
                if (InBeat(960 + 2, 960 + 128 + 124) && Gametime % 4 == 0)
                {
                    CreateBone(new DownBone(false, 4f, 16) { MarkScore = false });
                }
                if (InBeat(960 + 4, 960 + 124) && At0thBeat(16))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(true, 2f, 64) { });
                }
                if (InBeat(960 + 124))
                {
                    CreatePlatform(new Platform(1, new(320, 340), Motions.PositionRoute.cameFromDown, 0, 45, BeatTime(125)));
                    CreatePlatform(new Platform(1, new(320, 280), Motions.PositionRoute.cameFromUp, 0, 45, BeatTime(125)));
                }
                if (InBeat(960 + 128, 960 + 250) && At0thBeat(16))
                {
                    if (RandSignal() > 0)
                        CreateGB(new NormalGB(new(100, 280 + RandSignal() * 30), new(100, 100), Vector2.One, 0, BeatTime(8), 10));
                    else
                        CreateGB(new NormalGB(new(540, 280 + RandSignal() * 30), new(540, 100), Vector2.One, 180, BeatTime(8), 10));
                }
                if (InBeat(1218))
                {
                    SetSoul(0);
                    SetBox(300, 160, 160);
                    TP(320, 300);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new SideCircleBone(i * 90, 2, 62, BeatTime(124)) { Tags = new[] { "A" } });
                }
                if (InBeat(1220))
                {
                    CreateBone(new CentreCircleBone(0, 2.4f, 82, BeatTime(124)) { ColorType = 1 });
                }
                if (InBeat(1218, 1218 + 125))
                {
                    var v = GetAll<SideCircleBone>("A");
                    if (AtKthBeat(32, BeatTime(8)))
                    {
                        CreateGB(new NormalGB(new(100, 300), Heart.Centre, new(1.0f, 1.0f), 0, BeatTime(8), 10));
                        CreateGB(new NormalGB(new(540, 300), Heart.Centre, new(1.0f, 1.0f), 180, BeatTime(8), 10));
                    }
                    if (AtKthBeat(32, BeatTime(24)))
                    {
                        CreateGB(new NormalGB(new(320, 100), Heart.Centre, new(1.0f, 1.0f), 90, BeatTime(8), 10));
                        CreateGB(new NormalGB(new(320, 500), Heart.Centre, new(1.0f, 1.0f), 270, BeatTime(8), 10));
                    }
                    if (At0thBeat(8))
                    {
                        foreach (var bone in v)
                        {
                            bone.RotateSpeed = 2.7f;
                        }
                    }
                    foreach (var bone in v)
                    {
                        bone.RotateSpeed = bone.RotateSpeed * 0.94f + 0.5f * 0.06f;
                    }
                }

                if (InBeat(1346))
                {
                    SetSoul(0);
                    SetBox(300, 160, 160);
                    Heart.RotateTo(0);
                }
                if (InBeat(1346, 1346 + 128 - 16) && At0thBeat(16))
                {
                    Vector2 v;
                    for (int i = 0; i < 2; i++)
                        CreateGB(new NormalGB(v = Heart.Centre + GetVector2(144, Rand(0, 359)), v + GetVector2(108, Rand(0, 359)),
                            new(1, 0.6f),
                            BeatTime(8),
                            20
                        ));
                }

                if (InBeat(1472 - 2))
                {
                    SetSoul(2);
                    SetBox(300, 260, 160);
                }
                if (InBeat(1472, 1472 + 128 - 4) && At0thBeat(16))
                {
                    CreateBone(new DownBone(false, 5, 50));
                    CreateBone(new DownBone(true, 5, 50));
                }
                if (InBeat(1472, 1472 + 128 - 4) && AtKthBeat(16, BeatTime(8)))
                {
                    CreateBone(new UpBone(false, 5, 50));
                    CreateBone(new UpBone(true, 5, 50));
                }
                if (InBeat(1472 - 2, 1472 + 128 - 4) && AtKthBeat(16, BeatTime(15)))
                { Heart.GiveForce(0, 12); }
                if (InBeat(1472, 1472 + 128 - 4) && AtKthBeat(16, BeatTime(7)))
                { Heart.GiveForce(180, 12); }
                if (InBeat(1600))
                {
                    Heart.RotateTo(0);
                    SetSoul(0);
                    SetBox(300, 160, 160);
                    TP(320, 300);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new SideCircleBone(i * 90, 2, 62, BeatTime(124)) { Tags = new[] { "A" } });
                }
                if (InBeat(1602))
                {
                    CreateBone(new CentreCircleBone(0, 2.4f, 82, BeatTime(124)) { ColorType = 1 });
                }
                if (InBeat(1600, 1600 + 125))
                {
                    var v = GetAll<SideCircleBone>("A");
                    if (AtKthBeat(32, BeatTime(8)))
                    {
                        CreateGB(new NormalGB(new(100, 300), Heart.Centre, new(1.0f, 1.0f), 0, BeatTime(8), 10));
                        CreateGB(new NormalGB(new(540, 300), Heart.Centre, new(1.0f, 1.0f), 180, BeatTime(8), 10));
                    }
                    if (AtKthBeat(32, BeatTime(24)))
                    {
                        CreateGB(new NormalGB(new(320, 100), Heart.Centre, new(1.0f, 1.0f), 90, BeatTime(8), 10));
                        CreateGB(new NormalGB(new(320, 500), Heart.Centre, new(1.0f, 1.0f), 270, BeatTime(8), 10));
                    }
                    if (At0thBeat(8))
                    {
                        foreach (var bone in v)
                        {
                            bone.RotateSpeed = 2.7f;
                        }
                    }
                    foreach (var bone in v)
                    {
                        bone.RotateSpeed = bone.RotateSpeed * 0.94f + 0.5f * 0.06f;
                    }
                }
            }

            public void Start()
            {
                ScreenDrawing.UIColor = Color.Snow;

                HeartAttribute.Speed = 2.86f;
                HeartAttribute.SoftFalling = true;
                HeartAttribute.Gravity = 5.4f;
                HeartAttribute.JumpSpeed = 4f;

                GametimeDelta = -61 + BeatTime(8);
                //  GametimeDetla = this.BeatTime(1471 - 3);
                SetSoul(0);
                SetBox(300, 160, 160);

                HeartAttribute.MaxHP = 6;
            }
        }
    }
}