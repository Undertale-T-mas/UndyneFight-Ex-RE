using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class SpiningSetsugekka : IChampionShip
    {
        public SpiningSetsugekka()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:1,4"));

            difficulties = new();
            difficulties.Add("div.2", Difficulty.Normal);
            difficulties.Add("div.1", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {

            public Game() : base(5.86f)
            { }

            public static Game instance;

            public string Music => "Spin! Setsugekka";

            public string FightName => "Spin! setsugekka";

            private class ThisInformation : SongInformation
            {
                public override string BarrageAuthor => "T-mas";
                public override string PaintAuthor => "OtokP";
                public override string SongAuthor => "Hige Driver";
                public override string DisplayName => "Maware setsugetsuka";

                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 9.5f),
                            new(Difficulty.ExtremePlus, 19.3f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 10.0f),
                            new(Difficulty.ExtremePlus, 19.4f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 17.5f),
                            new(Difficulty.ExtremePlus, 21.6f),
                        }
                    );
            }
            public SongInformation Attributes => new ThisInformation();

            private static Bone obj1, obj2, obj3;
            private static GameObject hull1, hull2;

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
            public void Extreme()
            {
                throw new System.NotImplementedException();
            }
            #endregion

            public void Normal()
            {
                if (GametimeF == -26)
                {
                    float time = 28;
                    CreateArrow(time, "R", 7.6f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 7.6f, 0, 0);
                    CreateArrow(time += BeatTime(4f), "R", 7.6f, 0, 0);
                    CreateArrow(time += BeatTime(4f / 3), "R", 7.6f, 0, 0);
                    CreateArrow(time += BeatTime(4f / 3), "+0", 7.6f, 0, 0);
                    CreateArrow(time += BeatTime(4f / 3), "+0", 7.6f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 7.6f, 0, 0);
                }
                if (GametimeF < 0) return;

                if (InBeat(16))
                {
                    SetSoul(0);
                    SetBox(290, 160, 160);
                    InstantTP(330, 290);
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    CreateBone(obj1 = new DownBone(false, 320, 0, 0) { MarkScore = false });
                    CreateBone(obj2 = new UpBone(false, 320, 0, 0) { MarkScore = false });
                }

                if (InBeat(0 + 16, 64 + 16 - 1))
                {
                    obj1.Length = Sin01(GametimeF / BeatTime(8)) * 40 + 72;
                    obj2.Length = 160 - obj1.Length;
                    obj1.Length -= 30;
                    obj2.Length -= 30;
                    if (At0thBeat(8))
                    {
                        CreateEntity(new Boneslab(90, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        CreateEntity(new Boneslab(270, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                }
                if (InBeat(83))
                {
                    obj1.Dispose();
                    obj2.Dispose();
                }
                if (InBeat(64 + 16))
                {
                    SetBox(290, 240, 150);
                }
                if (InBeat(64 + 16, 128 + 16 - 16))
                {
                    if (At0thBeat(8))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 3, 30));
                        CreateBone(new DownBone(false, 3, 70));
                    }
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 3, 70));
                        CreateBone(new DownBone(true, 3, 30));
                    }
                }
                if (InBeat(128 + 16 - 16))
                {
                    PlaySound(Sounds.boneSlabSpawn);
                    SetBox(290, 150, 150);
                    CreateEntity(new Boneslab(90, 76, (int)BeatTime(8 + 2), (int)BeatTime(0.9f)) { MarkScore = false });
                    CreateEntity(new Boneslab(270, 76, (int)BeatTime(8 + 4), (int)BeatTime(0.9f)) { MarkScore = false });
                    CreateEntity(new Boneslab(90, 76, (int)BeatTime(8 + 6), (int)BeatTime(0.9f)) { MarkScore = false });
                }
                if (InBeat(128 + 16 - 16 + 1, 128 + 16 - 8 - 1))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.93f + 2f * 0.07f;
                }
                if (InBeat(128 + 16 - 6) || InBeat(128 + 16 - 2))
                {
                    ScreenDrawing.ScreenAngle = 30;
                }
                if (InBeat(128 + 16 - 4))
                {
                    ScreenDrawing.ScreenAngle = -30;
                }
                if (InBeat(128 + 16 - 8 + 1, 128 + 16 - 1) && At0thBeat(2))
                {
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(128 + 16, 128 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1f * 0.2f;
                }
                if (InBeat(128 + 16))
                {
                    ScreenDrawing.ScreenAngle = 0;
                    SetGreenBox();
                    TP();
                    SetSoul(1); Heart.RotateTo(0); Heart.RotateTo(0);

                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }
                if (InBeat(128 + 16 - 8))
                {
                    float time = BeatTime(8 + 4.4f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2.4f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(3.0f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(2.7f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                }
                if (InBeat(192 + 16 - 8))
                {
                    float time = BeatTime(8 + 2f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);

                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4.4f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4.2f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);
                    CreateArrow(time += BeatTime(4), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);

                    CreateArrow(time += BeatTime(4f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(3.6f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                }

                if (InBeat(256 + 32))
                {
                    SetSoul(0);
                    SetBox(290, 240, 150);
                }
                if (InBeat(256 + 32 + 2, 320 + 32 - 16))
                {
                    if (At0thBeat(8))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 3, 70));
                        CreateBone(new DownBone(false, 3, 30));
                    }
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 3, 30));
                        CreateBone(new DownBone(true, 3, 70));
                    }
                }
                if (InBeat(320 + 32 - 16))
                {
                    PlaySound(Sounds.boneSlabSpawn);
                    SetBox(300, 150, 150);
                    CreateEntity(new Boneslab(180, 118, (int)BeatTime(8 + 4), (int)BeatTime(0.9f)));
                }
                if (InBeat(320 + 32 - 16 + 1, 320 + 32 - 8 - 1))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.93f + 2f * 0.07f;
                }
                if (InBeat(320 + 32 - 4))
                {
                    ScreenDrawing.ScreenPositionDelta = new Vector2(0, -90);
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(320 + 32, 320 + 80))
                {
                    ScreenDrawing.ScreenPositionDelta *= 0.8f;
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1f * 0.2f;
                }
                if (InBeat(320 + 32))
                {
                    ScreenDrawing.ScreenAngle = 0;
                    SetGreenBox();
                    TP();
                    SetSoul(1); Heart.RotateTo(0);

                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }

                if (InBeat(320 + 32 - 8))
                {
                    float time = BeatTime(8 + 4.4f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4.1f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2.3f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(3.0f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(2.7f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                }
                if (InBeat(384 + 32 - 8))
                {
                    float time = BeatTime(8 + 2f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);

                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);
                    CreateArrow(time += BeatTime(4), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);

                    CreateArrow(time += BeatTime(4.3f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(3.9f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2.1f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                }

                if (InBeat(496))
                {
                    SetSoul(0);
                    SetBox(290, 150, 150);
                }
                if (InBeat(497))
                {
                    CreateBone(obj1 = new CentreCircleBone(0, 180 / BeatTime(16), 320, BeatTime(112)) { IsMasked = true });
                }
                if (InBeat(496, 496 + 128 - 20) && At0thBeat(8))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 0.5f), BeatTime(8.5f), 8));
                }
                if (InBeat(496 + 64))
                {
                    PlaySound(Sounds.Ding);
                    (obj1 as CentreCircleBone).RotateSpeed = -180 / BeatTime(16);
                }

                if (InBeat(496 + 128 - 8))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(496 + 128 - 10))
                {
                    float time = BeatTime(10.15f);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenAngle = (s * 2 - 1) * 36f;
                        };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(496 + 128 - 8, 496 + 128 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(496 + 128 - 2, 496 + 128))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(496 + 128 + 31))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(496 + 128 + 32, 496 + 128 + 64 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(560 + 128 - 8))
                {
                    Heart.RotateTo(0);
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(560 + 128 - 10))
                {
                    float time = BeatTime(10);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenAngle = (s * 2 - 1) * 36f;
                        };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(560 + 128 - 8, 560 + 128 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(560 + 128 - 2, 560 + 128))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(560 + 128 + 31))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(560 + 128 + 32, 560 + 128 + 64 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(752 - 16))
                {
                    float time = BeatTime(8);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 3, 3, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (type == 2) CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 0);
                    });
                }
                if (InBeat(752 - 11))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    TP();
                    Heart.RotateTo(0);
                    SetGreenBox();
                }
                if (InBeat(752 - 18 + 64))
                {
                    float time = BeatTime(10.01f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 1, 0, 0,
                        1, 0, 0, 0, 1, 0, 0, 0,
                        1, 0, 0, 0, 0, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.998f);
                        if (type == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (type == 2) CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 0);
                    });
                }

                if (InBeat(880))
                {
                    SetSoul(0);
                    SetBox(290, 160, 160);
                    InstantTP(330, 290);
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    CreateBone(obj1 = new DownBone(false, 320, 0, 0));
                    CreateBone(obj2 = new UpBone(false, 320, 0, 0));

                    CreateBone(obj3 = new CustomBone(new(320, 290), Motions.PositionRoute.stableValue,
                        (s) => { return 35 - Sin01(GametimeF / BeatTime(8)) * 24; }, (s) => 0));
                }

                if (InBeat(880, 880 + 64 - 1))
                {
                    obj1.Length = Sin01(GametimeF / BeatTime(8)) * 15 + 24;
                    obj2.Length = obj1.Length;
                    if (At0thBeat(16))
                    {
                        CreateEntity(new Boneslab(90, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                    if (AtKthBeat(16, BeatTime(4)))
                    {
                        CreateEntity(new Boneslab(180, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                    if (AtKthBeat(16, BeatTime(8)))
                    {
                        CreateEntity(new Boneslab(270, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                    if (AtKthBeat(16, BeatTime(12)))
                    {
                        CreateEntity(new Boneslab(0, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                }
                if (InBeat(944))
                {
                    (obj3 as CustomBone).LengthRoute = (s) => { return MathF.Max(-2, (s as CustomBone).Length - 3); };
                }
                if (InBeat(944 + 2.4f))
                {
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                }
                if (InBeat(944))
                {
                    SetBox(290, 240, 150);
                }
                if (InBeat(944, 1008 - 16))
                {
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 3, 54));
                        CreateBone(new DownBone(false, 3, 54));
                    }
                    if (At0thBeat(8))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new CustomBone(new Vector2(440, 290), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -4, 0 }
                        });
                    }
                }

                if (InBeat(1008 - 16))
                {
                    PlaySound(Sounds.boneSlabSpawn);
                    SetBox(290, 150, 150);
                    CreateEntity(new Boneslab(270, 76, (int)BeatTime(8 + 2), (int)BeatTime(0.9f)) { MarkScore = false });
                    CreateEntity(new Boneslab(90, 76, (int)BeatTime(8 + 4), (int)BeatTime(0.9f)) { MarkScore = false });
                    CreateEntity(new Boneslab(270, 76, (int)BeatTime(8 + 6), (int)BeatTime(0.9f)) { MarkScore = false });
                }
                if (InBeat(1008 - 16 + 1, 128 + 16 - 8 - 1))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.93f + 2f * 0.07f;
                }
                if (InBeat(1008 - 6) || InBeat(1008 - 2))
                {
                    ScreenDrawing.ScreenAngle = 30;
                }
                if (InBeat(1008 - 4))
                {
                    ScreenDrawing.ScreenAngle = -30;
                }
                if (InBeat(1008 - 8 + 1, 1008 - 1) && At0thBeat(2))
                {
                    PlaySound(Sounds.pierce);
                }

                if (InBeat(1008, 1008 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1f * 0.2f;
                }
                if (InBeat(1008))
                {
                    ScreenDrawing.ScreenAngle = 0;
                    SetGreenBox();
                    TP();
                    SetSoul(1); Heart.RotateTo(0);

                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }
                if (InBeat(1008 - 8))
                {
                    float time = BeatTime(8 + 4.4f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2.4f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(3.0f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(2.7f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                }
                if (InBeat(1072 - 8))
                {
                    float time = BeatTime(8 + 2f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);

                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4.4f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4.2f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);
                    CreateArrow(time += BeatTime(4), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);

                    CreateArrow(time += BeatTime(4f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(3.6f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                }

                if (InBeat(1150))
                {
                    SetSoul(0);
                    SetBox(290, 150, 150);
                }
                if (InBeat(1153))
                {
                    CreateBone(obj1 = new CentreCircleBone(0, 120 / BeatTime(16), 320, BeatTime(112)) { IsMasked = true, ColorType = 1 });
                    CreateBone(obj2 = new CentreCircleBone(90, 120 / BeatTime(16), 320, BeatTime(112)) { IsMasked = true, ColorType = 1 });
                }
                if (InBeat(1152, 1144 + 128 - 20) && At0thBeat(8))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 0.5f), BeatTime(8.5f), 8));
                }
                if (InBeat(1152 + 64))
                {
                    PlaySound(Sounds.Ding);
                    (obj1 as CentreCircleBone).RotateSpeed = -180 / BeatTime(16);
                    (obj1 as CentreCircleBone).ColorType = 2;
                }

                if (InBeat(1152 + 128 - 8))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1152 + 128 - 10))
                {
                    float time = BeatTime(10.15f);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        if (v == 0)
                            a.OnDispose += () =>
                            {
                                ScreenDrawing.ScreenAngle = (x + 2) / 2 * 45;
                            };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(1152 + 128 - 8, 1152 + 128 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(1152 + 128 - 2, 1152 + 128))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(1152 + 128 + 31))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(1152 + 128 + 32, 1152 + 128 + 64 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(1280 + 64 - 8))
                {
                    Heart.RotateTo(0);
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1280 + 64 - 10))
                {
                    float time = BeatTime(10);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        if (v == 0)
                            a.OnDispose += () =>
                            {
                                ScreenDrawing.ScreenAngle = (x + 2) / 2 * 45;
                            };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(1280 + 64 - 8, 1280 + 64 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(1280 + 64 - 2, 1280 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(1280 + 95))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(1280 + 96, 1280 + 128 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(1408 - 16))
                {
                    float time = BeatTime(8);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 3, 3, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (type == 2) CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 0);
                    });
                }
                if (InBeat(1408 - 11))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    TP();
                    Heart.RotateTo(0);
                    SetGreenBox();
                }
                if (InBeat(1408 - 18 + 64))
                {
                    float time = BeatTime(10.01f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 1, 0, 0,
                        1, 0, 0, 0, 1, 0, 0, 0,
                        1, 0, 0, 0, 0, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.998f);
                        if (type == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (type == 2) CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 0);
                    });
                }

                if (InBeat(1536))
                {
                    SetSoul(0);
                    SetBox(310, 240, 120);
                }
                if (InBeat(1537.2f))
                {
                    CreateBone(obj1 = new CustomBone(new Vector2(260, 310), Motions.PositionRoute.stableValue,
                        (s) =>
                        {
                            float alpha = (s as CustomBone).Rotation;
                            float res = 1 / Cos(((alpha + 45) % 90) - 45) * 110f;
                            return res;
                        },
                        (s) =>
                        {
                            return s.AppearTime / BeatTime(8) * 120;
                        }
                    )
                    { AlphaIncrease = true });
                    CreateBone(obj2 = new CustomBone(new Vector2(380, 310), Motions.PositionRoute.stableValue,
                        (s) =>
                        {
                            float alpha = (s as CustomBone).Rotation;
                            float res = 1 / Cos(((alpha + 45) % 90) - 45) * 110f;
                            return res;
                        },
                        (s) =>
                        {
                            return s.AppearTime / BeatTime(8) * 120;
                        }
                    )
                    { AlphaIncrease = true });

                    AddInstance(hull1 = new VertexHull(
                        new Vector3[] {
                            new(20, 20, 20),
                            new(20, -20, 20),
                            new(-20, 20, 20),
                            new(-20, -20, 20),
                            new(20, 20, -20),
                            new(20, -20, -20),
                            new(-20, 20, -20),
                            new(-20, -20, -20),
                        },
                        new Vector3(0.5f, -0.5f, -3)
                    ));

                    AddInstance(hull2 = new VertexHull(
                        new Vector3[] {
                            new(20, 20, 20),
                            new(20, -20, 20),
                            new(-20, 20, 20),
                            new(-20, -20, 20),
                            new(20, 20, -20),
                            new(20, -20, -20),
                            new(-20, 20, -20),
                            new(-20, -20, -20),
                        },
                        new Vector3(-0.5f, 0.5f, 3)
                    ));

                    Point[] points = new Point[] {
                        new(0, 1),
                        new(1, 3),
                        new(2, 3),
                        new(2, 0),
                        new(4, 5),
                        new(5, 7),
                        new(6, 7),
                        new(6, 4),
                        new(0, 4),
                        new(1, 5),
                        new(2, 6),
                        new(3, 7),
                    };

                    for (int i = 0; i < points.Length; i++)
                    {
                        CreateBone(new CustomBone(new Vector2(260, 310),
                            (s) =>
                            {
                                Point cur = (Point)(s as GameObject).Extras;
                                Vector2 vec1 = (hull1 as VertexHull).Translated[cur.X];
                                Vector2 vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                return vec1 + (vec2 - vec1) / 2;
                            },
                            (s) =>
                            {
                                Point cur = (Point)(s as GameObject).Extras;
                                Vector2 vec1 = (hull1 as VertexHull).Translated[cur.X];
                                Vector2 vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                return MathF.Max(0, (vec2 - vec1).Length() - 4);
                            },
                            (s) =>
                            {
                                Point cur = (Point)(s as GameObject).Extras;
                                float v;
                                Vector2 vec1 = (hull1 as VertexHull).Translated[cur.X];
                                Vector2 vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                return v = (vec2 - vec1).Direction() + 90;
                            }
                        )
                        { Tags = new string[] { "" }, Extras = points[i], AlphaIncrease = true });
                    }
                    for (int i = 0; i < points.Length; i++)
                    {
                        CreateBone(new CustomBone(new Vector2(380, 310),
                            (s) =>
                            {
                                Point cur = (Point)(s as GameObject).Extras;
                                Vector2 vec1 = (hull2 as VertexHull).Translated[cur.X];
                                Vector2 vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                return vec1 + (vec2 - vec1) / 2;
                            },
                            (s) =>
                            {
                                Point cur = (Point)(s as GameObject).Extras;
                                Vector2 vec1 = (hull2 as VertexHull).Translated[cur.X];
                                Vector2 vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                return MathF.Max(0, (vec2 - vec1).Length() - 4);
                            },
                            (s) =>
                            {
                                Point cur = (Point)(s as GameObject).Extras;
                                float v;
                                Vector2 vec1 = (hull2 as VertexHull).Translated[cur.X];
                                Vector2 vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                return v = (vec2 - vec1).Direction() + 90;
                            }
                        )
                        { Tags = new string[] { "" }, Extras = points[i], AlphaIncrease = true });
                    }
                }

                if (InBeat(1536 + 64))
                {
                    SetBox(200, 320, 250, 370);
                }
                if (InBeat(1536 + 64 + 24, 1536 + 64 + 32))
                {
                    float x = MathHelper.Lerp(200, 320, (GametimeF - BeatTime(1536 + 64 + 24)) / BeatTime(8));
                    SetBox(x, 440, 250, 370);
                }
                if (InBeat(1536 + 64 + 48))
                {
                    SetBox(310, 120, 120);
                }
                if (InBeat(1536 + 64 + 56))
                {
                    SetBox(310, 80, 120);
                }
                if (InBeat(1536 + 64 + 60))
                {
                    SetBox(310, 60, 120);
                }

                if (InBeat(1662.6f))
                {
                    ResetBarrage();
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1664 - 12))
                {
                    int[] arr = { 1, 0, 2, 0, 1, 0, 2, 0, 1, 0, 2, 0, 1, 3, 3, 3 };

                    float time = BeatTime(12.7f);

                    Fortimes(3, (i) =>
                    {
                        Fortimes(arr.Length, (x) =>
                        {
                            int way = arr[x] switch
                            {
                                1 => 0,
                                2 => 2,
                                3 => 3,
                                _ => -1
                            };
                            if (arr[x] != 0)
                            {
                                var v = MakeArrow(time, way, 7, arr[x] == 3 ? 1 : 0, 0);
                                CreateEntity(v);
                                if (i == 1 && arr[x] == 3) v.OnDispose += () => { ScreenDrawing.ScreenScale = 1.25f; ScreenDrawing.ScreenAngle = (x % 2 * 2 - 1) * 40; };
                            }
                            time += BeatTime(2);
                        });
                    });
                }
                if (InBeat(1664 + 96 - 12))
                {
                    int[] arr = { 1, 0, 2, 0, 1, 0, 2, 0, 1, 0, 2, 0, 1 };

                    float time = BeatTime(12.5f);

                    Fortimes(arr.Length, (x) =>
                    {
                        int way = arr[x] switch
                        {
                            1 => 0,
                            2 => 2,
                            3 => 3,
                            _ => -1
                        };
                        if (arr[x] != 0)
                        {
                            var v = MakeArrow(time, way, 7, arr[x] == 3 ? 1 : 0, 0);
                            CreateEntity(v);
                        }
                        time += BeatTime(2);
                    });
                }
                if (InBeat(1664 + 64, 1664 + 65))
                {
                    ScreenDrawing.ScreenAngle = 0;
                }
                if (InBeat(1664 + 64, 1664 + 96))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.88f + 1f * 0.12f;
                }
                if (InBeat(1664 + 128 - 8, 1664 + 128))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.88f + 1.32f * 0.12f;
                }

                if (InBeat(1792))
                {
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    SetBox(290, 180, 180);
                    SetSoul(0);
                    ScreenDrawing.ScreenScale = 1f;
                }
                if (InBeat(1792, 1792 + 31) && At0thBeat(16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }
                if (InBeat(1792 + 31, 1792 + 45) && At0thBeat(4))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 0.5f), BeatTime(16.5f), 18));
                }
                if (InBeat(1792 + 64, 1792 + 95) && At0thBeat(16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }
                if (InBeat(1792 + 95, 1792 + 107) && At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(1792 + 95 - 6, 1792 + 107 - 6) && AtKthBeat(4, BeatTime(2)))
                {
                    int ctype = Rand(1, 2);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new(220 - i * 9, 290), (s) =>
                        {
                            return s.AppearTime < BeatTime(6)
                                ? s.CentrePosition + new Vector2((BeatTime(6) - s.AppearTime) * 0.05f, 0)
                                : s.CentrePosition + new Vector2(9, 0);
                        }, 0, 175)
                        { ColorType = ctype });
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new(420 + i * 9, 290), (s) =>
                        {
                            return s.AppearTime < BeatTime(6)
                                ? s.CentrePosition - new Vector2((BeatTime(6) - s.AppearTime) * 0.05f, 0)
                                : s.CentrePosition - new Vector2(9, 0);
                        }, 0, 175)
                        { ColorType = ctype });
                }

                if (InBeat(1792 + 96 + 16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }

                if (InBeat(1920))
                {
                    SetBox(290, 180, 180);
                    SetSoul(0);
                    ScreenDrawing.ScreenScale = 1f;
                }
                if (InBeat(1920, 1920 + 31) && At0thBeat(16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }
                if (InBeat(1920 + 31, 1920 + 45) && At0thBeat(4))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 0.5f), BeatTime(16.5f), 18));
                }
                if (InBeat(1920 + 64, 1920 + 95) && At0thBeat(16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }
                if (InBeat(1920 + 95, 1920 + 107) && At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(1920 + 95 - 6, 1920 + 107 - 6) && AtKthBeat(4, BeatTime(2)))
                {
                    int ctype = Rand(1, 2);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new(220 - i * 9, 290), (s) =>
                        {
                            return s.AppearTime < BeatTime(6)
                                ? s.CentrePosition + new Vector2((BeatTime(6) - s.AppearTime) * 0.05f, 0)
                                : s.CentrePosition + new Vector2(9, 0);
                        }, 0, 175)
                        { ColorType = ctype });
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new(420 + i * 9, 290), (s) =>
                        {
                            return s.AppearTime < BeatTime(6)
                                ? s.CentrePosition - new Vector2((BeatTime(6) - s.AppearTime) * 0.05f, 0)
                                : s.CentrePosition - new Vector2(9, 0);
                        }, 0, 175)
                        { ColorType = ctype });
                }


                if (InBeat(2080 - 8))
                {
                    Heart.RotateTo(0);
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(2080 - 10))
                {
                    float time = BeatTime(10);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        if (v == 0)
                            a.OnDispose += () =>
                            {
                                ScreenDrawing.ScreenAngle = (x + 2) / 2 * 45;
                            };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(2080 - 14, 2080 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(2080 - 2, 2080))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(2080 + 32 - 1))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(2080 + 32, 2080 + 64 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(2080 + 64 - 8))
                {
                    Heart.RotateTo(0);
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(2080 + 64 - 10))
                {
                    float time = BeatTime(10);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        if (v == 0)
                            a.OnDispose += () =>
                            {
                                ScreenDrawing.ScreenAngle = (x + 2) / 2 * 45;
                            };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(2080 + 64 - 8, 1280 + 64 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(2080 + 64 - 2, 1280 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(2080 + 95))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(2080 + 96, 2080 + 128 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(2208 - 16))
                {
                    float time = BeatTime(8);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 3, 3, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (type == 2) CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 0);
                    });
                }
                if (InBeat(2208 - 11))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    TP();
                    Heart.RotateTo(0);
                    SetGreenBox();
                }
                if (InBeat(2208 - 18 + 64))
                {
                    float time = BeatTime(10.01f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        2, 0, 0, 0, 0, 0, 1, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 1, 0, 0,
                        1, 0, 0, 0, 1, 0, 0, 0,
                        1, 0, 0, 0, 0, 0, 0, 0,
                        1, 0, 0, 0, 1, 0, 1, 0,
                        1, 0, 0, 0, 0, 0, 0, 0,
                        1, 0, 0, 0, 1, 0, 1, 0,
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 0, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.998f);
                        if (type == 1) CreateArrow(time, "R", 6.5f, 0, 0);
                        if (type == 2) CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 0);
                    });
                }
                if (InBeat(2384 - 4))
                {
                    SetSoul(0);
                    SetBox(300, 180, 150);
                }

                if (InBeat(2384, 2384 + 48 - 1))
                {
                    if (At0thBeat(8))
                    {
                        PlaySound(Sounds.pierce);
                    }
                    if (AtKthBeat(16, BeatTime(8)))
                    {
                        CreateBone(new DownBone(false, 3f, 146) { ColorType = Rand(1, 2) });
                    }
                    if (AtKthBeat(16, BeatTime(0)))
                    {
                        CreateBone(new DownBone(true, 3f, 146) { ColorType = Rand(1, 2) });
                    }
                }
                if (InBeat(2384 + 48, 2384 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.3f * 0.1f;
                }
                if (InBeat(2384 + 48))
                {
                    for (int i = 0; i < 4; i++)
                        CreateEntity(new Boneslab(i * 90, 50, (int)BeatTime(12), 10));
                }
                if (InBeat(2348 + 64))
                {
                    PlaySound(Sounds.pierce);
                    ScreenDrawing.ScreenScale = 1;
                }
            }

            public void ExtremePlus()
            {
                if (GametimeF == -26)
                {
                    float time = 28;
                    CreateArrow(time, "R", 7.6f, 0, 0);
                    CreateArrow(time, "R", 7.6f, 1, 0);
                    CreateArrow(time += BeatTime(4), "R", 7.6f, 0, 0);
                    CreateArrow(time, "R", 7.6f, 1, 0);
                    CreateArrow(time += BeatTime(4f), "R", 7.6f, 0, 0);
                    CreateArrow(time, "+0", 7.6f, 1, 0);
                    CreateArrow(time += BeatTime(4f / 3), "+0", 7.6f, 0, 0);
                    CreateArrow(time, "+2", 7.6f, 1, 1);
                    CreateArrow(time += BeatTime(4f / 3), "+2", 7.6f, 0, 0);
                    CreateArrow(time, "+2", 7.6f, 1, 1);
                    CreateArrow(time += BeatTime(4f / 3), "R", 7.6f, 0, 0);
                    CreateArrow(time, "R", 7.6f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 7.6f, 0, 0);
                    CreateArrow(time, "R", 7.6f, 1, 0);
                }
                if (GametimeF < 0) return;

                if (InBeat(16))
                {
                    SetSoul(0);
                    SetBox(290, 160, 160);
                    InstantTP(330, 290);
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    CreateBone(obj1 = new DownBone(false, 320, 0, 0) { MarkScore = false });
                    CreateBone(obj2 = new UpBone(false, 320, 0, 0) { MarkScore = false });
                }

                if (InBeat(0 + 16, 64 + 16 - 1))
                {
                    obj1.Length = Sin01(GametimeF / BeatTime(16)) * 46 + 72;
                    obj2.Length = 160 - obj1.Length;
                    obj1.Length -= 12;
                    obj2.Length -= 12;
                    if (At0thBeat(8))
                    {
                        CreateEntity(new Boneslab(90, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        CreateEntity(new Boneslab(270, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                }
                if (InBeat(83))
                {
                    obj1.Dispose();
                    obj2.Dispose();
                }
                if (InBeat(64 + 16))
                {
                    SetBox(290, 240, 150);
                }
                if (InBeat(64 + 16, 128 + 16 - 16))
                {
                    if (At0thBeat(8))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 1.8f, 30));
                        CreateBone(new DownBone(true, 3.8f, 145) { ColorType = 1 });
                        CreateBone(new DownBone(false, 2.5f, 70));
                    }
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 2.5f, 70));
                        CreateBone(new DownBone(false, 0.6f + GametimeF / 180f, 145) { ColorType = 2 });
                        CreateBone(new DownBone(true, 1.8f, 30));
                    }
                }
                if (InBeat(128 + 16 - 16))
                {
                    PlaySound(Sounds.boneSlabSpawn);
                    SetBox(290, 150, 150);
                    CreateEntity(new Boneslab(90, 81, (int)BeatTime(8 + 2), (int)BeatTime(0.9f)) { MarkScore = false });
                    CreateEntity(new Boneslab(270, 81, (int)BeatTime(8 + 4), (int)BeatTime(0.9f)) { MarkScore = false });
                    CreateEntity(new Boneslab(90, 81, (int)BeatTime(8 + 6), (int)BeatTime(0.9f)) { MarkScore = false });
                }
                if (InBeat(128 + 16 - 16 + 1, 128 + 16 - 8 - 1))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.93f + 2f * 0.07f;
                }
                if (InBeat(128 + 16 - 6) || InBeat(128 + 16 - 2))
                {
                    ScreenDrawing.ScreenAngle = 30;
                }
                if (InBeat(128 + 16 - 4))
                {
                    ScreenDrawing.ScreenAngle = -30;
                }
                if (InBeat(128 + 16 - 8 + 1, 128 + 16 - 1) && At0thBeat(2))
                {
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(128 + 16, 128 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1f * 0.2f;
                }
                if (InBeat(128 + 16))
                {
                    ScreenDrawing.ScreenAngle = 0;
                    SetGreenBox();
                    TP();
                    SetSoul(1); Heart.RotateTo(0);

                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }
                if (InBeat(128 + 16 - 8))
                {
                    float time = BeatTime(8 + 4.4f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2.4f), "0", 7.4f, 0, 0);
                    CreateArrow(time, "+1", 9.4f, 1, 1);
                    CreateArrow(time += BeatTime(3.0f), "0", 7.4f, 0, 0);
                    CreateArrow(time, "+2", 9.4f, 1, 1);
                    CreateArrow(time += BeatTime(2.7f), "0", 7.4f, 0, 0);
                    CreateArrow(time, "+3", 9.4f, 1, 1);
                }
                if (InBeat(192 + 16 - 8))
                {
                    float time = BeatTime(8 + 2f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);

                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time, "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(4.4f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(4.2f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);
                    CreateArrow(time += BeatTime(4), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);

                    CreateArrow(time += BeatTime(4f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(3.6f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                }

                if (InBeat(256 + 32))
                {
                    SetSoul(0);
                    SetBox(290, 240, 150);
                }
                if (InBeat(256 + 32 + 2, 320 + 32 - 16))
                {
                    if (At0thBeat(8))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 1.8f, 30));
                        CreateBone(new DownBone(false, 3.8f, 145) { ColorType = 1 });
                        CreateBone(new DownBone(true, 2.5f, 70));
                    }
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 2.5f, 70));
                        CreateBone(new DownBone(true, -6.6f + GametimeF / 180f, 145) { ColorType = 2 });
                        CreateBone(new DownBone(false, 1.8f, 30));
                    }
                }
                if (InBeat(320 + 32 - 16))
                {
                    PlaySound(Sounds.boneSlabSpawn);
                    SetBox(300, 150, 150);
                    CreateEntity(new Boneslab(180, 118, (int)BeatTime(8 + 4), (int)BeatTime(0.9f)));
                }
                if (InBeat(320 + 32 - 16 + 1, 320 + 32 - 8 - 1))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.93f + 2f * 0.07f;
                }
                if (InBeat(320 + 32 - 4))
                {
                    ScreenDrawing.ScreenPositionDelta = new Vector2(0, -90);
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(320 + 32, 320 + 80))
                {
                    ScreenDrawing.ScreenPositionDelta *= 0.8f;
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1f * 0.2f;
                }
                if (InBeat(320 + 32))
                {
                    ScreenDrawing.ScreenAngle = 0;
                    SetGreenBox();
                    TP();
                    SetSoul(1); Heart.RotateTo(0);

                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }

                if (InBeat(320 + 32 - 8))
                {
                    float time = BeatTime(8 + 4.4f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2f), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2.1f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2.3f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(3.0f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(2.7f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 0);
                }

                if (InBeat(384 + 32 - 8))
                {
                    float time = BeatTime(8 + 4f);
                    Fortimes(12, () =>
                    {
                        CreateArrow(time, "R", 9.6f, 1, 0, ArrowAttribute.SpeedUp);
                        time += BeatTime(4f);
                    });

                    time = BeatTime(8 + 2f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);

                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4f), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(4f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);
                    CreateArrow(time += BeatTime(4), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);

                    CreateArrow(time += BeatTime(4.3f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 5.6f, 1, 0);
                    CreateArrow(time += BeatTime(3.9f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 5.6f, 1, 0);
                    CreateArrow(time += BeatTime(2.1f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 5.6f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 5.6f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 5.6f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 5.6f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 5.6f, 1, 0);
                }

                if (InBeat(496))
                {
                    SetSoul(0);
                    SetBox(290, 150, 150);
                }
                if (InBeat(497))
                {
                    CreateBone(obj1 = new CentreCircleBone(0, 120 / BeatTime(16), 320, BeatTime(112)) { IsMasked = true });
                    CreateBone(obj2 = new CentreCircleBone(0, -180 / BeatTime(16), 320, BeatTime(112)) { IsMasked = true, ColorType = 1 });
                }
                if (InBeat(496, 496 + 128 - 20) && At0thBeat(8))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 0.5f), BeatTime(8.5f), 8));
                }
                if (InBeat(496 + 64))
                {
                    PlaySound(Sounds.Ding);
                    (obj1 as CentreCircleBone).RotateSpeed = -180 / BeatTime(16);
                }

                if (InBeat(496 + 128 - 8))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(496 + 128 - 10))
                {
                    float time = BeatTime(10.15f);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 9.2f, 0, 1);
                        int s = x % 4 / 2;
                        if (v == 0) CreateGB(new GreenSoulGB(time, s * 2 + 1, 1, BeatTime(1f)));
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenAngle = (s * 2 - 1) * 36f;
                        };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(496 + 128 - 8, 496 + 128 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(496 + 128 - 2, 496 + 128))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(496 + 128 + 31))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(496 + 128 + 32, 496 + 128 + 64 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(560 + 128 - 8))
                {
                    Heart.RotateTo(0);
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(560 + 128 - 10))
                {
                    float time = BeatTime(10);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 1);
                        int s = x % 4 / 2;
                        if (v == 0) CreateGB(new GreenSoulGB(time, s * 2 + 1, 1, BeatTime(1f)));
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenAngle = (s * 2 - 1) * 36f;
                        };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(560 + 128 - 8, 560 + 128 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(560 + 128 - 2, 560 + 128))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(560 + 128 + 31))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(560 + 128 + 32, 560 + 128 + 64 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(752 - 16))
                {
                    float time = BeatTime(7.95f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 3, 3, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1)
                        {
                            CreateArrow(time, "R", 6.5f, 0, 0);
                            CreateArrow(time, "R", 6.5f, 1, 0);
                        }
                        if (type == 2)
                        {
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(1)));
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(4)));
                        }
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 1);
                        if (type == 4)
                            CreateArrow(time, "R", 6.5f, 0, 0);
                    });
                }
                if (InBeat(752 - 11))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    TP();
                    Heart.RotateTo(0);
                    SetGreenBox();
                }
                if (InBeat(752 - 18 + 64))
                {
                    float time = BeatTime(9.95f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 1, 0, 0,
                        1, 0, 0, 0, 1, 0, 0, 0,
                        1, 0, 0, 0, 0, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1)
                        {
                            CreateArrow(time, "R", 6.5f, 0, 0);
                            CreateArrow(time, "R", 6.5f, 1, 0);
                        }
                        if (type == 2)
                        {
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(1)));
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(4)));
                        }
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 1);
                        if (type == 4)
                            CreateArrow(time, "R", 6.5f, 0, 0);
                    });
                }

                if (InBeat(880))
                {
                    SetSoul(0);
                    SetBox(290, 160, 160);
                    InstantTP(330, 290);
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    CreateBone(obj1 = new DownBone(false, 320, 0, 0));
                    CreateBone(obj2 = new UpBone(false, 320, 0, 0));

                    CreateBone(obj3 = new CustomBone(new(320, 290), Motions.PositionRoute.stableValue,
                        (s) => { return 39 - Sin01(GametimeF / BeatTime(8)) * 24; }, (s) => 0));
                }

                if (InBeat(880, 880 + 64 - 1))
                {
                    obj1.Length = Sin01(GametimeF / BeatTime(8)) * 15 + 26;
                    obj2.Length = obj1.Length;
                    if (At0thBeat(16))
                    {
                        CreateEntity(new Boneslab(90, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                    if (AtKthBeat(16, BeatTime(4)))
                    {
                        CreateEntity(new Boneslab(180, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                    if (AtKthBeat(16, BeatTime(8)))
                    {
                        CreateEntity(new Boneslab(270, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                    if (AtKthBeat(16, BeatTime(12)))
                    {
                        CreateEntity(new Boneslab(0, 78, (int)BeatTime(1), (int)BeatTime(1)));
                    }
                }
                if (InBeat(944))
                {
                    (obj3 as CustomBone).LengthRoute = (s) => { return MathF.Max(-2, (s as CustomBone).Length - 3); };
                }
                if (InBeat(944 + 2.4f))
                {
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                }
                if (InBeat(944))
                {
                    SetBox(290, 240, 150);
                }
                if (InBeat(944, 1008 - 16))
                {
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 3, 54));
                        CreateBone(new DownBone(false, 150, 3, 54));
                    }
                    if (At0thBeat(8))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(true, 4, 144) { ColorType = 1 });
                        CreateBone(new CustomBone(new Vector2(440, 290), Motions.PositionRoute.linear, 0, 40)
                        {
                            PositionRouteParam = new float[] { -4, 0 }
                        });
                    }
                }

                if (InBeat(1008 - 16))
                {
                    PlaySound(Sounds.boneSlabSpawn);
                    SetBox(290, 150, 150);
                    CreateEntity(new Boneslab(270, 80, (int)BeatTime(8 + 2), (int)BeatTime(0.9f)) { MarkScore = false });
                    CreateEntity(new Boneslab(90, 80, (int)BeatTime(8 + 4), (int)BeatTime(0.9f)) { MarkScore = false });
                    CreateEntity(new Boneslab(270, 80, (int)BeatTime(8 + 6), (int)BeatTime(0.9f)) { MarkScore = false });
                }
                if (InBeat(1008 - 16 + 1, 128 + 16 - 8 - 1))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.93f + 2f * 0.07f;
                }
                if (InBeat(1008 - 6) || InBeat(1008 - 2))
                {
                    ScreenDrawing.ScreenAngle = 30;
                }
                if (InBeat(1008 - 4))
                {
                    ScreenDrawing.ScreenAngle = -30;
                }
                if (InBeat(1008 - 8 + 1, 1008 - 1) && At0thBeat(2))
                {
                    PlaySound(Sounds.pierce);
                }

                if (InBeat(1008, 1008 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 1f * 0.2f;
                }
                if (InBeat(1008))
                {
                    ScreenDrawing.ScreenAngle = 0;
                    SetGreenBox();
                    TP();
                    SetSoul(1); Heart.RotateTo(0);

                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                }
                if (InBeat(1008 - 8))
                {
                    float time = BeatTime(8 + 4.4f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);

                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);

                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(0), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2.4f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+2", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(3.0f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+2", 9.4f, 1, 0);
                    CreateArrow(time += BeatTime(2.7f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+2", 9.4f, 1, 0);
                }
                if (InBeat(1072 - 8))
                {
                    float time = BeatTime(8 + 2f);

                    CreateArrow(time, "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time + BeatTime(1), "+2", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);

                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(4), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(4.4f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(4.2f), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);
                    CreateArrow(time += BeatTime(4), "R", 7.4f, 0, 0);
                    CreateArrow(time, "+0", 9.4f, 1, 1);

                    CreateArrow(time += BeatTime(4f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(3.6f), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                    CreateArrow(time += BeatTime(2), "R", 6.4f, 0, 0);
                    CreateArrow(time, "R", 6.4f, 1, 0);
                }

                if (InBeat(1152))
                {
                    SetSoul(0);
                    SetBox(290, 150, 150);
                }
                if (InBeat(1154))
                {
                    CreateBone(obj1 = new CentreCircleBone(0, 180 / BeatTime(16), 320, BeatTime(112)) { IsMasked = true, ColorType = 1 });
                    CreateBone(obj2 = new CentreCircleBone(90, 180 / BeatTime(16), 320, BeatTime(112)) { IsMasked = true, ColorType = 1 });
                }
                if (InBeat(1152, 1144 + 128 - 20) && At0thBeat(8))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 0.5f), BeatTime(8.5f), 8));
                }
                if (InBeat(1152 + 64))
                {
                    PlaySound(Sounds.Ding);
                    (obj1 as CentreCircleBone).RotateSpeed = -240 / BeatTime(16);
                    (obj1 as CentreCircleBone).ColorType = 2;
                }

                if (InBeat(1152 + 128 - 8))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1152 + 128 - 10))
                {
                    float time = BeatTime(10.15f);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        if (v == 0) CreateGB(new GreenSoulGB(time, s * 2 + 1, 1, BeatTime(1f)));
                        if (v == 0)
                            a.OnDispose += () =>
                            {
                                ScreenDrawing.ScreenAngle = (x + 2) / 2 * 45;
                            };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(1152 + 128 - 8, 1152 + 128 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(1152 + 128 - 2, 1152 + 128))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(1152 + 128 + 31))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(1152 + 128 + 32, 1152 + 128 + 64 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(1280 + 64 - 8))
                {
                    Heart.RotateTo(0);
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1280 + 64 - 10))
                {
                    float time = BeatTime(10);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        if (v == 0) CreateGB(new GreenSoulGB(time, s * 2 + 1, 1, BeatTime(1f)));
                        if (v == 0)
                            a.OnDispose += () =>
                            {
                                ScreenDrawing.ScreenAngle = (x + 2) / 2 * 45;
                            };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(1280 + 64 - 8, 1280 + 64 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(1280 + 64 - 2, 1280 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(1280 + 95))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(1280 + 96, 1280 + 128 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(1408 - 16))
                {
                    float time = BeatTime(7.96f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 3, 3, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1)
                        {
                            CreateArrow(time, "R", 6.5f, 0, 0);
                            CreateArrow(time, "R", 6.5f, 1, 0);
                        }
                        if (type == 2)
                        {
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(1)));
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(4)));
                        }
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 1);
                        if (type == 4)
                            CreateArrow(time, "R", 6.5f, 0, 0);
                    });
                }
                if (InBeat(1408 - 11))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    TP();
                    Heart.RotateTo(0);
                    SetGreenBox();
                }
                if (InBeat(1408 - 18 + 64))
                {
                    float time = BeatTime(9.95f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 1, 0, 0,
                        1, 0, 0, 0, 1, 0, 0, 0,
                        1, 0, 0, 0, 0, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1)
                        {
                            CreateArrow(time, "R", 6.5f, 0, 0);
                            CreateArrow(time, "R", 6.5f, 1, 0);
                        }
                        if (type == 2)
                        {
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(1)));
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(4)));
                        }
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 1);
                        if (type == 4)
                            CreateArrow(time, "R", 6.5f, 0, 0);
                    });
                }

                if (InBeat(1536))
                {
                    SetSoul(0);
                    SetBox(310, 280, 98);
                }
                if (InBeat(1537.2f))
                {
                    float range = 12;
                    AddInstance(hull1 = new VertexHull(
                        new Vector3[] {
                            new(range, range, range),
                            new(range, -range, range),
                            new(-range, range, range),
                            new(-range, -range, range),
                            new(range, range, -range),
                            new(range, -range, -range),
                            new(-range, range, -range),
                            new(-range, -range, -range),
                        },
                        new Vector3(0.5f, -0.5f, -3)
                    ));
                    AddInstance(hull2 = new VertexHull(
                        new Vector3[] {
                            new(range, range, range),
                            new(range, -range, range),
                            new(-range, range, range),
                            new(-range, -range, range),
                            new(range, range, -range),
                            new(range, -range, -range),
                            new(-range, range, -range),
                            new(-range, -range, -range),
                        },
                        new Vector3(-0.5f, 0.5f, 3)
                    ));

                    Point[] points = new Point[] {
                        new(0, 1),
                        new(1, 3),
                        new(2, 3),
                        new(2, 0),
                        new(4, 5),
                        new(5, 7),
                        new(6, 7),
                        new(6, 4),
                        new(0, 4),
                        new(1, 5),
                        new(2, 6),
                        new(3, 7),
                    };

                    for (int cnt = 1; cnt <= 3; cnt++)
                    {
                        CreateBone(new CustomBone(new Vector2(365 - cnt * 90, 310), Motions.PositionRoute.stableValue,
                            (s) =>
                            {
                                float alpha = (s as CustomBone).Rotation;
                                float res = 1 / Cos(((alpha + 45) % 90) - 45) * 84f;
                                return res;
                            },
                            (s) =>
                            {
                                return s.AppearTime / BeatTime(8) * 270;
                            }
                        )
                        { AlphaIncrease = true });
                        CreateBone(new CustomBone(new Vector2(275 + cnt * 90, 310), Motions.PositionRoute.stableValue,
                            (s) =>
                            {
                                float alpha = (s as CustomBone).Rotation;
                                float res = 1 / Cos(((alpha + 45) % 90) - 45) * 84f;
                                return res;
                            },
                            (s) =>
                            {
                                return s.AppearTime / BeatTime(8) * 270;
                            }
                        )
                        { AlphaIncrease = true });
                        for (int i = 0; i < points.Length; i++)
                        {
                            CreateBone(new CustomBone(new Vector2(365 - cnt * 90, 310),
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    Vector2 vec1 = (hull1 as VertexHull).Translated[cur.X];
                                    Vector2 vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                    return vec1 + (vec2 - vec1) / 2;
                                },
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    Vector2 vec1 = (hull1 as VertexHull).Translated[cur.X];
                                    Vector2 vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                    return MathF.Max(0, (vec2 - vec1).Length() - 4);
                                },
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    float v;
                                    Vector2 vec1 = (hull1 as VertexHull).Translated[cur.X];
                                    Vector2 vec2 = (hull1 as VertexHull).Translated[cur.Y];
                                    return v = (vec2 - vec1).Direction() + 90;
                                }
                            )
                            { Tags = new string[] { "" }, Extras = points[i], AlphaIncrease = true });
                        }
                        for (int i = 0; i < points.Length; i++)
                        {
                            CreateBone(new CustomBone(new Vector2(275 + cnt * 90, 310),
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    Vector2 vec1 = (hull2 as VertexHull).Translated[cur.X];
                                    Vector2 vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                    return vec1 + (vec2 - vec1) / 2;
                                },
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    Vector2 vec1 = (hull2 as VertexHull).Translated[cur.X];
                                    Vector2 vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                    return MathF.Max(0, (vec2 - vec1).Length() - 4);
                                },
                                (s) =>
                                {
                                    Point cur = (Point)(s as GameObject).Extras;
                                    float v;
                                    Vector2 vec1 = (hull2 as VertexHull).Translated[cur.X];
                                    Vector2 vec2 = (hull2 as VertexHull).Translated[cur.Y];
                                    return v = (vec2 - vec1).Direction() + 90;
                                }
                            )
                            { Tags = new string[] { "" }, Extras = points[i], AlphaIncrease = true });
                        }
                    }
                }

                if (InBeat(1536 + 64))
                {
                    SetBox(180, 270, 261, 359);
                }
                if (InBeat(1536 + 64 + 16))
                {
                    SetBox(180, 460, 261, 359);
                }
                if (InBeat(1536 + 64 + 24, 1536 + 64 + 32))
                {
                    float x = MathHelper.Lerp(200, 370, (GametimeF - BeatTime(1536 + 64 + 24)) / BeatTime(8));
                    SetBox(x, 460, 261, 359);
                }
                if (InBeat(1536 + 64 + 48))
                {
                    SetBox(310, 240, 98);
                }
                if (InBeat(1536 + 64 + 56))
                {
                    SetBox(310, 80, 94);
                }
                if (InBeat(1536 + 64 + 60))
                {
                    SetBox(310, 60, 90);
                }

                if (InBeat(1662.6f))
                {
                    ResetBarrage();
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(1664 - 12))
                {
                    int[] arr = { 1, 0, 2, 0, 1, 0, 2, 0, 1, 0, 2, 0, 1, 3, 3, 3 };

                    float time = BeatTime(12.7f);

                    Fortimes(3, (i) =>
                    {
                        Fortimes(arr.Length, (x) =>
                        {
                            int way = arr[x] switch
                            {
                                1 => 0,
                                2 => 2,
                                3 => 3,
                                _ => -1
                            };
                            if (arr[x] != 0)
                            {
                                var v = MakeArrow(time, way, 7, arr[x] == 3 ? 1 : 0, 0);
                                CreateEntity(v);
                                if (i == 1 && arr[x] == 3) v.OnDispose += () => { ScreenDrawing.ScreenScale = 1.25f; ScreenDrawing.ScreenAngle = (x % 2 * 2 - 1) * 40; };
                            }
                            time += BeatTime(2);
                        });
                    });
                }
                if (InBeat(1664 + 96 - 12))
                {
                    int[] arr = { 1, 0, 2, 0, 1, 0, 2, 0, 1, 0, 2, 0, 1 };

                    float time = BeatTime(12.5f);

                    Fortimes(arr.Length, (x) =>
                    {
                        int way = arr[x] switch
                        {
                            1 => 0,
                            2 => 2,
                            3 => 3,
                            _ => -1
                        };
                        if (arr[x] != 0)
                        {
                            var v = MakeArrow(time, way, 7, arr[x] == 3 ? 1 : 0, 0);
                            CreateEntity(v);
                        }
                        time += BeatTime(2);
                    });
                }
                if (InBeat(1664 + 64, 1664 + 65))
                {
                    ScreenDrawing.ScreenAngle = 0;
                }
                if (InBeat(1664 + 64, 1664 + 96))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.88f + 1f * 0.12f;
                }
                if (InBeat(1664 + 128 - 8, 1664 + 128))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.88f + 1.32f * 0.12f;
                }

                if (InBeat(1792))
                {
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    SetBox(290, 180, 180);
                    SetSoul(0);
                    ScreenDrawing.ScreenScale = 1f;
                }
                if (InBeat(1792, 1792 + 31) && At0thBeat(16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }
                if (InBeat(1792 + 31, 1792 + 45) && At0thBeat(4))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 0.5f), BeatTime(16.5f), 18));
                }
                if (InBeat(1792 + 64, 1792 + 95) && At0thBeat(16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }
                if (InBeat(1792 + 95, 1792 + 107) && At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(1792 + 95 - 6, 1792 + 107 - 6) && AtKthBeat(4, BeatTime(2)))
                {
                    int ctype = Rand(1, 2);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new(220 - i * 9, 290), (s) =>
                        {
                            return s.AppearTime < BeatTime(6)
                                ? s.CentrePosition + new Vector2((BeatTime(6) - s.AppearTime) * 0.05f, 0)
                                : s.CentrePosition + new Vector2(9, 0);
                        }, 0, 175)
                        { ColorType = ctype });
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new(420 + i * 9, 290), (s) =>
                        {
                            return s.AppearTime < BeatTime(6)
                                ? s.CentrePosition - new Vector2((BeatTime(6) - s.AppearTime) * 0.05f, 0)
                                : s.CentrePosition - new Vector2(9, 0);
                        }, 0, 175)
                        { ColorType = ctype });
                }

                if (InBeat(1792 + 96 + 16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }

                if (InBeat(1920))
                {
                    SetBox(290, 180, 180);
                    SetSoul(0);
                    ScreenDrawing.ScreenScale = 1f;
                }
                if (InBeat(1920, 1920 + 31) && At0thBeat(16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }
                if (InBeat(1920 + 31, 1920 + 45) && At0thBeat(4))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 0.5f), BeatTime(16.5f), 18));
                }
                if (InBeat(1920 + 64, 1920 + 95) && At0thBeat(16))
                {
                    Vector2 pos;
                    CreateGB(new NormalGB(pos = Heart.Centre + GetVector2(128, Rand(0, 359)), pos + GetVector2(100, Rand(0, 359)), new Vector2(1, 1), BeatTime(8.5f), 18));
                }
                if (InBeat(1920 + 95, 1920 + 107) && At0thBeat(4))
                {
                    PlaySound(Sounds.pierce);
                }
                if (InBeat(1920 + 95 - 6, 1920 + 107 - 6) && AtKthBeat(4, BeatTime(2)))
                {
                    int ctype = Rand(1, 2);
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new(220 - i * 9, 290), (s) =>
                        {
                            return s.AppearTime < BeatTime(6)
                                ? s.CentrePosition + new Vector2((BeatTime(6) - s.AppearTime) * 0.05f, 0)
                                : s.CentrePosition + new Vector2(9, 0);
                        }, 0, 175)
                        { ColorType = ctype });
                    for (int i = 0; i < 4; i++)
                        CreateBone(new CustomBone(new(420 + i * 9, 290), (s) =>
                        {
                            return s.AppearTime < BeatTime(6)
                                ? s.CentrePosition - new Vector2((BeatTime(6) - s.AppearTime) * 0.05f, 0)
                                : s.CentrePosition - new Vector2(9, 0);
                        }, 0, 175)
                        { ColorType = ctype });
                }


                if (InBeat(2080 - 8))
                {
                    Heart.RotateTo(0);
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(2080 - 10))
                {
                    float time = BeatTime(10.1f);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        if (v == 0) CreateGB(new GreenSoulGB(time, s * 2 + 1, 1, BeatTime(1f)));
                        if (v == 0)
                            a.OnDispose += () =>
                            {
                                ScreenDrawing.ScreenAngle = (x + 2) / 2 * 45;
                            };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(2080 - 14, 2080 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(2080 - 2, 2080))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(2080 + 32 - 1))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(2080 + 32, 2080 + 64 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(2080 + 64 - 8))
                {
                    Heart.RotateTo(0);
                    SetSoul(1); Heart.RotateTo(0);
                    SetGreenBox();
                    TP();
                }
                if (InBeat(2080 + 64 - 10))
                {
                    float time = BeatTime(10.1f);
                    Fortimes(16, (x) =>
                    {
                        int v = x % 2;
                        Arrow a = MakeArrow(time, v * 2, 8.2f, 0, 0);
                        int s = x % 4 / 2;
                        if (v == 0) CreateGB(new GreenSoulGB(time, s * 2 + 1, 1, BeatTime(1f)));
                        if (v == 0)
                            a.OnDispose += () =>
                            {
                                ScreenDrawing.ScreenAngle = (x + 2) / 2 * 45;
                            };
                        time += BeatTime(1.5f);
                        CreateEntity(a);
                    });
                    for (int i = 1; i <= 3; i++)
                    {
                        Arrow a = MakeArrow(time, 3, 9f, 0, 0);
                        int x = i;
                        a.OnDispose += () =>
                        {
                            ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                            ScreenDrawing.ScreenAngle = 0;
                            ScreenDrawing.ScreenScale = 1.3f - x * 0.1f;
                        };
                        time += BeatTime(5.4f - i * 1.6f);
                        CreateEntity(a);
                    }
                }

                if (InBeat(2080 + 64 - 8, 1280 + 64 - 3))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.92f + 0.87f * 0.08f;
                }
                if (InBeat(2080 + 64 - 2, 1280 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.85f + 1.3f * 0.15f;
                }

                if (InBeat(2080 + 95))
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    SetBox(290, 150, 150);
                }
                if (InBeat(2080 + 96, 2080 + 128 - 14) && At0thBeat(8))
                {
                    int rot;
                    PlaySound(Sounds.boneSlabSpawn);
                    Heart.GiveForce(rot = Rand(0, 3) * 90, 8);
                    CreateEntity(new Boneslab(rot, 35, (int)BeatTime(5), 10));
                }

                if (InBeat(2208 - 16))
                {
                    float time = BeatTime(7.95f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 3, 3, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 1, 0, 1, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1)
                        {
                            CreateArrow(time, "R", 6.5f, 0, 0);
                            CreateArrow(time, "R", 6.5f, 1, 0);
                        }
                        if (type == 2)
                        {
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(1)));
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(4)));
                        }
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 1);
                        if (type == 4)
                            CreateArrow(time, "R", 6.5f, 0, 0);
                    });
                }
                if (InBeat(2208 - 11))
                {
                    SetSoul(1); Heart.RotateTo(0);
                    TP();
                    Heart.RotateTo(0);
                    SetGreenBox();
                }
                if (InBeat(2208 - 18 + 64))
                {
                    float time = BeatTime(9.91f);
                    int[] arr = {
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        2, 0, 0, 0, 4, 0, 4, 3,
                        1, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 0, 1, 0,
                        0, 0, 1, 0, 1, 0, 1, 0,
                        1, 0, 0, 1, 0, 1, 0, 0,
                        1, 0, 0, 0, 1, 0, 0, 0,
                        1, 0, 0, 0, 4, 0, 0, 0,
                        1, 0, 0, 0, 1, 0, 1, 0,
                        1, 0, 0, 0, 4, 0, 0, 0,
                        1, 0, 0, 0, 1, 0, 1, 0,
                        1, 0, 0, 0, 1, 0, 0, 0,
                        2, 0, 0, 0, 4, 0, 0, 0
                    };
                    Fortimes(arr.Length, (x) =>
                    {
                        int type = arr[x];
                        time += BeatTime(0.995f);
                        if (type == 1)
                        {
                            CreateArrow(time, "R", 6.5f, 0, 0);
                            CreateArrow(time, "R", 6.5f, 1, 0);
                        }
                        if (type == 2)
                        {
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(1)));
                            CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(4)));
                        }
                        if (type == 3) CreateArrow(time, "+0", 6.5f, 0, 1);
                        if (type == 4)
                            CreateArrow(time, "R", 6.5f, 0, 0);
                    });
                }
                if (InBeat(2384 - 4))
                {
                    SetSoul(0);
                    SetBox(300, 180, 150);
                }

                if (InBeat(2384, 2384 + 48 - 1))
                {
                    if (At0thBeat(4))
                    {
                        PlaySound(Sounds.pierce);
                    }
                    if (AtKthBeat(8, BeatTime(4)))
                    {
                        CreateBone(new DownBone(false, 3f, 146) { ColorType = Rand(1, 2) });
                    }
                    if (AtKthBeat(8, BeatTime(0)))
                    {
                        CreateBone(new DownBone(true, 3f, 146) { ColorType = Rand(1, 2) });
                    }
                }
                if (InBeat(2384 + 48, 2384 + 64))
                {
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.9f + 1.3f * 0.1f;
                }
                if (InBeat(2384 + 48))
                {
                    for (int i = 0; i < 4; i++)
                        CreateEntity(new Boneslab(i * 90, 50, (int)BeatTime(12), 10));
                }
                if (InBeat(2348 + 64))
                {
                    PlaySound(Sounds.pierce);
                    ScreenDrawing.ScreenScale = 1;
                }
            }

            public void Start()
            {
                ScreenDrawing.UIColor = Color.Snow;

                HeartAttribute.Speed = 2.86f;
                SetGreenBox();
                TP();
                SetSoul(1); Heart.RotateTo(0);
                HeartAttribute.ArrowFixed = true;

                GametimeDelta = -27;
                //  GametimeFDelta = this.BeatTime(1532);
                // SetSoul(0);
                HeartAttribute.MaxHP = 10;
                ScreenDrawing.UISettings.CreateUISurface();
            }
        }
    }
}