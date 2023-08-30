using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public partial class BrainPower : IChampionShip
    {
        public BrainPower()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new()
            {
                { "div1", Difficulty.ExtremePlus }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();

        public class Game : IWaveSet
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
            class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 20.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 20.6f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 26.9f),
                        }
                    );
                public override bool Hidden
                {
                    get
                    {
                        /*var songname = "";
                        if (PlayerManager.CurrentUser == null) return true;
                        if (!PlayerManager.CurrentUser.SongPlayed(songname)) return true;
                        var data = PlayerManager.CurrentUser.GetSongData(songname);
                        if (data.CurrentSongStates.ContainsKey(Difficulty.Extreme))
                        {
                            return data.CurrentSongStates[Difficulty.Extreme].Mark == SkillMark.Failed;
                        }
                        return true;*/
                        return false;
                    }
                }
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() { }

            public static Game instance;

            public string Music => "ad,!U)~ZRr]";
            //  public string Music => "Brain Power";

            public string FightName => "B$a%N_p+>E&";

            private float missionRotation = 0;
            private Vector2 missionPosition = Vector2.Zero;

            #region Non-ChampionShip
            public void Normal()
            {
                throw new NotImplementedException();
            }
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

            public void ExtremePlus()
            {
                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.88f + missionRotation * 0.12f;
                if (MathF.Abs(ScreenDrawing.ScreenAngle - missionRotation) < 0.5f)
                    ScreenDrawing.ScreenAngle = missionRotation;
                ScreenDrawing.ScreenPositionDetla = ScreenDrawing.ScreenPositionDetla * 0.88f + missionPosition * 0.12f;
                if ((ScreenDrawing.ScreenPositionDetla - missionPosition).Length() < 0.5f)
                    ScreenDrawing.ScreenPositionDetla = missionPosition;
                #region Are you ready
                if (GametimeF == 0)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(64, (t) =>
                    {
                        if (t == 59)
                        {
                            lastR = Rand(0, 3);
                            CreateGB(new GreenSoulGB(time, lastR, 1, delta));
                        }
                        if (t == 61)
                        {
                            CreateArrow(time, lastR + 2, 8.2f, 1, 1);
                            CreateArrow(time + delta * 1.33f, lastR + 2, 8.2f, 1, 1);
                            CreateArrow(time + delta * 2.66f, lastR + 2, 8.2f, 1, 1);
                        }
                        if (t % 4 == 0)
                            CreateArrow(time, lastB = Rand(0, 3), 8.2f, 0, 0);
                        if (t % 4 == 1)
                        {
                            if (t <= 56)
                            {
                                lastR = lastB + RandSignal();
                                CreateArrow(time, lastR, 8.2f, 1, 0);
                            }
                        }
                        if (t % 4 == 2)
                            CreateArrow(time, lastB + 2, 8.2f, 0, 0);
                        if (t % 4 == 3)
                        {
                            if (t <= 56)
                            {
                                CreateArrow(time, lastR + 2, 8.2f, 1, 1);
                            }
                        }
                        time += delta * 2;
                    });
                }
                if (GametimeF >= 94 && GametimeF <= 2906)
                {
                    if (((GametimeF - 94) % 22) == 1)
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, ((GametimeF - 94) % 44) == 12);
                        ScreenDrawing.CameraEffect.SizeExpand(2, 5);
                    }
                }
                if (GametimeF == 705)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(32, (t) =>
                    {
                        if (t % 4 == 0)
                            CreateArrow(time, lastB = Rand(0, 3), 8.2f, 0, 0);
                        if (t % 4 == 1)
                        {
                            lastR = lastB + RandSignal();
                            CreateArrow(time, lastR, 8.2f, 1, 0);
                        }
                        if (t % 4 == 2)
                            CreateArrow(time, lastB + 2, 8.2f, 0, 0);
                        if (t % 4 == 3)
                        {
                            CreateArrow(time, lastR + 2, 8.2f, 1, 1);
                        }
                        time += delta * 2;
                    });
                }
                if (GametimeF == 1058)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(16, (t) =>
                    {
                        if (t % 8 == 0)
                            CreateArrow(time, lastB = 0, 8.2f, 0, 0);
                        if (t % 8 == 4)
                            CreateArrow(time, lastB + 2, 8.2f, 0, 1);
                        CreateArrow(time, lastR, 16f, 1, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1146)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(16, (t) =>
                    {
                        if (t % 8 == 0)
                            CreateArrow(time, lastB = 0, 8.2f, 1, 0);
                        if (t % 8 == 4)
                            CreateArrow(time, lastB + 2, 8.2f, 1, 1);
                        CreateArrow(time, lastR, 16f, 0, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1234)
                {
                    float delta = 5.51f;
                    int lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(4, (t) =>
                    {
                        if (t == 0)
                            CreateArrow(time, 0, 8.2f, 0, 0);
                        CreateArrow(time, lastR, 16f, 1, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1256)
                {
                    float delta = 5.51f;
                    int lastR = 2;
                    float time = 5.51f * 16;
                    Fortimes(4, (t) =>
                    {
                        if (t == 0)
                            CreateArrow(time, 2, 8.2f, 1, 0);
                        CreateArrow(time, lastR, 16f, 0, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1278)
                {
                    float delta = 5.51f;
                    int lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(4, (t) =>
                    {
                        if (t == 0)
                            CreateArrow(time, 0, 8.2f, 0, 0);
                        CreateArrow(time, lastR, 16f, 1, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1300)
                {
                    float delta = 5.51f;
                    int lastR = 2;
                    float time = 5.51f * 16;
                    Fortimes(4, (t) =>
                    {
                        if (t == 0)
                            CreateArrow(time, 2, 8.2f, 1, 0);
                        CreateArrow(time, lastR, 16f, 0, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1321)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(6, (t) =>
                    {
                        if (t % 2 == 0)
                        {
                            CreateGB(new GreenSoulGB(time, lastR = Rand(0, 3), 1, 5));
                        }
                        if (t % 2 == 1)
                        {
                            CreateGB(new GreenSoulGB(time, lastB = Rand(0, 3), 0, 5));
                        }
                        time += delta * 2.65f;
                    });
                }
                if (GametimeF == 0 + 1411)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(64, (t) =>
                    {
                        if (t == 59)
                        {
                            lastR = Rand(0, 3);
                            CreateGB(new GreenSoulGB(time, lastR, 1, delta));
                        }
                        if (t == 61)
                        {
                            CreateArrow(time, lastR + 2, 8.2f, 1, 1);
                            CreateArrow(time + delta * 1.33f, lastR + 2, 8.2f, 1, 1);
                            CreateArrow(time + delta * 2.66f, lastR + 2, 8.2f, 1, 1);
                        }
                        if (t % 4 == 0)
                            CreateArrow(time, lastB = Rand(0, 3), 8.2f, 0, 0);
                        if (t % 4 == 1)
                        {
                            if (t <= 56)
                            {
                                lastR = lastB + RandSignal();
                                CreateArrow(time, lastR, 8.2f, 1, 0);
                            }
                        }
                        if (t % 4 == 2)
                            CreateArrow(time, lastB + 2, 8.2f, 0, 0);
                        if (t % 4 == 3)
                        {
                            if (t <= 56)
                            {
                                CreateArrow(time, lastR + 2, 8.2f, 1, 1);
                            }
                        }
                        time += delta * 2;
                    });
                }
                if (GametimeF == 705 + 1411)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(32, (t) =>
                    {
                        if (t % 4 == 0)
                            CreateArrow(time, lastB = Rand(0, 3), 8.2f, 0, 0);
                        if (t % 4 == 1)
                        {
                            lastR = lastB + RandSignal();
                            CreateArrow(time, lastR, 8.2f, 1, 0);
                        }
                        if (t % 4 == 2)
                            CreateArrow(time, lastB + 2, 8.2f, 0, 0);
                        if (t % 4 == 3)
                        {
                            CreateArrow(time, lastR + 2, 8.2f, 1, 1);
                        }
                        time += delta * 2;
                    });
                }
                if (GametimeF == 1058 + 1411)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(16, (t) =>
                    {
                        if (t % 8 == 0)
                            CreateArrow(time, lastB = 0, 8.2f, 0, 0);
                        if (t % 8 == 4)
                            CreateArrow(time, lastB + 2, 8.2f, 0, 1);
                        CreateArrow(time, lastR, 16f, 1, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1146 + 1411)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(16, (t) =>
                    {
                        if (t % 8 == 0)
                            CreateArrow(time, lastB = 0, 8.2f, 1, 0);
                        if (t % 8 == 4)
                            CreateArrow(time, lastB + 2, 8.2f, 1, 1);
                        CreateArrow(time, lastR, 16f, 0, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1234 + 1411)
                {
                    float delta = 5.51f;
                    int lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(4, (t) =>
                    {
                        if (t == 0)
                            CreateArrow(time, 0, 8.2f, 0, 0);
                        CreateArrow(time, lastR, 16f, 1, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1256 + 1411)
                {
                    float delta = 5.51f;
                    int lastR = 2;
                    float time = 5.51f * 16;
                    Fortimes(4, (t) =>
                    {
                        if (t == 0)
                            CreateArrow(time, 2, 8.2f, 1, 0);
                        CreateArrow(time, lastR, 16f, 0, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1278 + 1411)
                {
                    float delta = 5.51f;
                    int lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(4, (t) =>
                    {
                        if (t == 0)
                            CreateArrow(time, 0, 8.2f, 0, 0);
                        CreateArrow(time, lastR, 16f, 1, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1300 + 1411)
                {
                    float delta = 5.51f;
                    int lastR = 2;
                    float time = 5.51f * 16;
                    Fortimes(4, (t) =>
                    {
                        if (t == 0)
                            CreateArrow(time, 2, 8.2f, 1, 0);
                        CreateArrow(time, lastR, 16f, 0, 0);
                        if (t % 2 == 1)
                            lastR++;
                        time += delta;
                    });
                }
                if (GametimeF == 1321 + 1411)
                {
                    float delta = 5.51f;
                    int lastB = 0, lastR = 0;
                    float time = 5.51f * 16;
                    Fortimes(6, (t) =>
                    {
                        if (t % 2 == 0)
                        {
                            CreateGB(new GreenSoulGB(time, lastR = Rand(0, 3), 1, 5));
                        }
                        if (t % 2 == 1)
                        {
                            CreateGB(new GreenSoulGB(time, lastB = Rand(0, 3), 0, 5));
                        }
                        time += delta * 2.65f;
                    });
                }
                #endregion
                #region Vocal 1
                if (GametimeF == 2906)
                {
                    SetBox(300, 240, 160);
                    SetSoul(0);
                }
                if (GametimeF >= 2910 && GametimeF <= 2910 + 5.51f * 240)
                {
                    if (GametimeF % (5.51f * 8) < 0.5f)
                    {
                        CreateGB(new NormalGB(Heart.Centre + GetVector2(100, Rand(-135, -45)), Heart.Centre, new(1, 0.5f), 5.51f * 8, 10));
                    }
                    if (GametimeF % 5 < 0.5f)
                    {
                        float val = Sin(GametimeF / 5.51f * 4.5f) * 36f + Sin(GametimeF / 5.51f * 20f) * 16f;
                        if (GametimeF <= 2910 + 5.51f * 56)
                        {
                            CreateBone(new UpBone(false, 1.6f, 60 + val) { Tags = new string[] { "A" } });
                            CreateBone(new DownBone(false, 1.6f, 60 - val) { Tags = new string[] { "A" } });
                        }
                        else if (GametimeF <= 2910 + 5.51f * 120)
                        {
                            CreateBone(new UpBone(true, 1.6f, 60 + val) { Tags = new string[] { "A" } });
                            CreateBone(new DownBone(true, 1.6f, 60 - val) { Tags = new string[] { "A" } });
                        }
                    }
                    if (GametimeF <= 2910 + 5.51f * 142)
                    {
                        var v = GetAll<Bone>("A");
                        foreach (var x in v)
                        {
                            if (x is UpBone)
                                (x as UpBone).Speed += 0.03f;
                            if (x is DownBone)
                                (x as DownBone).Speed += 0.03f;
                        }
                    }
                    if (GametimeF >= 2910 + 5.51f * 120)
                    {
                        if ((GametimeF + 5.51f * 8) % (5.51f * 16) < 1)
                        {
                            CreateGB(new NormalGB(Heart.Centre + GetVector2(180, 0), Heart.Centre, new(1, 0.5f), 5.51f * 8, 10));
                            CreateGB(new NormalGB(Heart.Centre - GetVector2(180, 0), Heart.Centre, new(1, 0.5f), 5.51f * 8, 10));
                        }
                        if (GametimeF % (5.51f * 8) < 0.5f)
                        {
                            PlaySound(Sounds.pierce);
                            CreateBone(new CustomBone(new(200, 390),
                                (s) => { return s.CentrePosition + new Vector2(1.5f, 0); },
                                (s) => { return Sin(GametimeF / 5.51f * 9) * 90 + 180 - 29; },
                                (s) => { return 0; }));
                            CreateBone(new CustomBone(new(200, 210),
                                (s) => { return s.CentrePosition + new Vector2(1.5f, 0); },
                                (s) => { return 180 - Sin(GametimeF / 5.51f * 9) * 90 - 29; },
                                (s) => { return 0; }));
                            CreateBone(new CustomBone(new(440, 390),
                                (s) => { return s.CentrePosition + new Vector2(-1.5f, 0); },
                                (s) => { return Sin(GametimeF / 5.51f * 9) * 90 + 180 - 29; },
                                (s) => { return 0; }));
                            CreateBone(new CustomBone(new(440, 210),
                                (s) => { return s.CentrePosition + new Vector2(-1.5f, 0); },
                                (s) => { return 180 - Sin(GametimeF / 5.51f * 9) * 90 - 29; },
                                (s) => { return 0; }));
                        }
                    }
                }
                if (GametimeF == 4305)
                {
                    SetSoul(0);
                    Heart.Split();
                    SetBoxMission(0);
                    SetBox(320, 480, 250, 410);
                    SetBoxMission(1);
                    SetBox(160, 320, 250, 410);
                }
                if (GametimeF == 4306)
                {
                    SetPlayerMission(0);
                    TP(400, 330);
                    SetPlayerMission(1);
                    TP(240, 330);
                }
                if (GametimeF >= 4306 && GametimeF <= 4671)
                {
                    Player.Heart heart;
                    if (Player.hearts.Count >= 1)
                        heart = Player.hearts[Rand(0, 1)];
                    else return;

                    if (GametimeF == 4323 + 0 * 22)
                    {
                        Vector2 pos = heart.Centre + GetVector2(150, Rand(0, 359));
                        CreateGB(new NormalGB(pos, pos + GetVector2(100, Rand(0, 359)), new(1, 0.5f), 6 * 22 + 4, 10));
                    }
                    if (GametimeF == 4323 + 1 * 22)
                    {
                        Vector2 pos = heart.Centre + GetVector2(150, Rand(0, 359));
                        CreateGB(new NormalGB(pos, pos + GetVector2(100, Rand(0, 359)), new(1, 0.5f), 5 * 22 + 4, 10));
                    }
                    if (GametimeF == 4323 + 2 * 22)
                    {
                        Vector2 pos = heart.Centre + GetVector2(150, Rand(0, 359));
                        CreateGB(new NormalGB(pos, pos + GetVector2(100, Rand(0, 359)), new(1, 0.5f), 5.3f * 22 + 4, 10));
                    }
                    if (GametimeF == 4323 + 3 * 22)
                    {
                        Vector2 pos = heart.Centre + GetVector2(150, Rand(0, 359));
                        CreateGB(new NormalGB(pos, pos + GetVector2(100, Rand(0, 359)), new(1, 0.5f), 4.3f * 22 + 4, 10));
                    }
                    if (GametimeF == 4323 + 4 * 22)
                    {
                        Vector2 pos = heart.Centre + GetVector2(150, Rand(0, 359));
                        CreateGB(new NormalGB(pos, pos + GetVector2(100, Rand(0, 359)), new(1, 0.5f), 3.8f * 22 + 4, 10));
                        CreateGB(new NormalGB(pos, pos + GetVector2(100, Rand(0, 359)), new(1, 0.5f), 3.8f * 22 + 4, 10));
                    }

                    if (GametimeF == 4495)
                    {
                        SetBoxMission(0);
                        CreateEntity(new Boneslab(0, 9, 1, 8 * 22));
                        CreateEntity(new Boneslab(180, 9, 1, 8 * 22));
                        SetBoxMission(1);
                        CreateEntity(new Boneslab(0, 9, 1, 8 * 22));
                        CreateEntity(new Boneslab(180, 9, 1, 8 * 22));
                    }
                    if (GametimeF == 4495 + 2 * 22)
                    {
                        foreach (var box in FightBox.boxs)
                        {
                            for (int i = 0; i < 4; i++)
                                box.Vertexs[i].MissionPosition -= new Vector2(0, 140); 
                        }
                    }
                    if (GametimeF == 4495 + 2 * 22 + 11)
                    {
                        foreach (var box in FightBox.boxs)
                        {
                            for (int i = 0; i < 4; i++)
                                box.Vertexs[i].MissionPosition += new Vector2(0, 140);
                        }
                    }
                    if (GametimeF == 4495 + 3 * 22)
                    {
                        foreach (var box in FightBox.boxs)
                        {
                            for (int i = 0; i < 4; i++)
                                box.Vertexs[i].MissionPosition -= new Vector2(0, 140);
                        }
                    }

                    if (GametimeF == 4495 + 4 * 22)
                    {
                        foreach (var box in FightBox.boxs)
                        {
                            for (int i = 0; i < 4; i++)
                                box.Vertexs[i].MissionPosition += new Vector2(0, 140);
                        }
                        float time = 2 * 22 + 5;
                        Fortimes(8, (t) => { CreateArrow(time, 2 * t, 15, 1, 0); time += 5.54f; });
                    }
                    if (GametimeF == 4495 + 5 * 22)
                    {
                        foreach (var box in FightBox.boxs)
                        {
                            for (int i = 0; i < 4; i++)
                                box.Vertexs[i].MissionPosition -= new Vector2(0, 140);
                        }
                    }
                    if (GametimeF == 4495 + 5 * 22 + 11)
                    {
                        foreach (var p in Player.hearts)
                        {
                            p.EnabledRedShield = true;
                        }
                        foreach (var box in FightBox.boxs)
                        {
                            for (int i = 0; i < 4; i++)
                                box.Vertexs[i].MissionPosition += new Vector2(0, 140);
                        }
                    }

                    if (GametimeF == 4495 + 130)
                    {
                        Vector2 c;
                        for (int i = 0; i < 4; i++)
                            CreateGB(new NormalGB(c = new Vector2(400, 330) + GetVector2(120, i * 90 + 45), c +
                                GetVector2(60, Rand(0, 359)), new(1, 1), i * 90 - 135, 1.5f * 22, 12));
                        for (int i = 0; i < 4; i++)
                            CreateGB(new NormalGB(c = new Vector2(240, 330) + GetVector2(120, i * 90 + 45), c +
                                GetVector2(60, Rand(0, 359)), new(1, 1), i * 90 - 135, 1.5f * 22, 12));
                        CreateGB(new NormalGB(c = new(160, 90), c + GetVector2(100, Rand(0, 359)), new(1, 0.5f), 90, 1.5f * 22, 12));
                        CreateGB(new NormalGB(c = new(480, 90), c + GetVector2(100, Rand(0, 359)), new(1, 0.5f), 90, 1.5f * 22, 12));
                    }

                    if (GametimeF == 4495 + 8 * 22)
                    {
                        foreach (var p in Player.hearts)
                        {
                            p.EnabledRedShield = false;
                        }
                    }
                }

                if (GametimeF == 4673)
                {
                    SetBoxMission(0);
                    SetBox(320, 160, 160);
                    Player.hearts[0].Teleport(new(320, 330));
                    Player.hearts[1].Merge(Player.hearts[0]);
                    SetPlayerMission(0);
                }
                if (GametimeF >= 4671 && GametimeF <= 5000)
                {
                    if (GametimeF == 4697)
                    {
                        PlaySound(Sounds.boneSlabSpawn);
                        CreateEntity(new Boneslab(0, 56, 22, 5));
                        CreateEntity(new Boneslab(180, 56, 22, 5));
                        SetSoul(2);
                        Heart.GiveForce(0, 10);
                    }
                    if (GametimeF == 4719)
                    {
                        PlaySound(Sounds.pierce);
                    }
                    if (GametimeF == 4741)
                    {
                        SetSoul(0);
                        CreateBone(new DownBone(false, 5.45f, 156) { Tags = new[] { "A" } });
                        PlaySound(Sounds.pierce);
                    }
                    if (GametimeF == 4763)
                    {
                        CreateBone(new DownBone(true, 5.45f, 156) { Tags = new[] { "A" } });
                        PlaySound(Sounds.pierce);
                    }
                    if (GametimeF == 4785)
                    {
                        CreateBone(new DownBone(false, 5.45f, 156) { Tags = new[] { "A" } });
                        PlaySound(Sounds.pierce);
                    }
                    if (GametimeF == 4806)
                    {
                        CreateBone(new DownBone(false, 9.45f, 126) { ColorType = 1 });
                        CreateBone(new UpBone(true, 9.45f, 126) { ColorType = 1 });
                        CreateBone(new LeftBone(false, 9.45f, 126) { ColorType = 1 });
                        CreateBone(new RightBone(true, 9.45f, 126) { ColorType = 1 });
                        CreateBone(new DownBone(true, 9.45f, 1226) { ColorType = 1 });
                        CreateBone(new UpBone(false, 9.45f, 126) { ColorType = 1 });
                        CreateBone(new LeftBone(true, 9.45f, 126) { ColorType = 1 });
                        CreateBone(new RightBone(false, 9.45f, 126) { ColorType = 1 });

                        CreateBone(new DownBone(false, 7.45f, 126) { ColorType = 1 });
                        CreateBone(new UpBone(true, 7.45f, 126) { ColorType = 1 });
                        CreateBone(new LeftBone(false, 7.45f, 126) { ColorType = 1 });
                        CreateBone(new RightBone(true, 7.45f, 126) { ColorType = 1 });
                        CreateBone(new DownBone(true, 7.45f, 1226) { ColorType = 1 });
                        CreateBone(new UpBone(false, 7.45f, 126) { ColorType = 1 });
                        CreateBone(new LeftBone(true, 7.45f, 126) { ColorType = 1 });
                        CreateBone(new RightBone(false, 7.45f, 126) { ColorType = 1 });
                        PlaySound(Sounds.pierce);
                    }
                    if (GametimeF == 4848)
                    {
                        SetSoul(3);

                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 5.45f, 66) { ColorType = 1 });
                        CreateBone(new UpBone(true, 5.45f, 66) { ColorType = 1 });
                        CreateBone(new LeftBone(false, 5.45f, 66) { ColorType = 1 });
                        CreateBone(new RightBone(true, 5.45f, 66) { ColorType = 1 });
                        CreateBone(new DownBone(true, 5.45f, 66) { ColorType = 1 });
                        CreateBone(new UpBone(false, 5.45f, 66) { ColorType = 1 });
                        CreateBone(new LeftBone(true, 5.45f, 66) { ColorType = 1 });
                        CreateBone(new RightBone(false, 5.45f, 66) { ColorType = 1 });
                    }
                    if (GametimeF == 4891 - 22)
                    {
                        SetSoul(3);
                        SetBox(320, 200, 200);
                        for (int i = 0; i <= 2; i++)
                            CreateGB(new NormalGB(Heart.Centre + GetVector2(100, Rand(0, 359)), Heart.Centre, new(1, 1), 44, 20));
                    }
                    if (GametimeF == 4891 + 22)
                    {
                        SetSoul(3);

                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 5.45f, 26) { ColorType = 1 });
                        CreateBone(new UpBone(true, 5.45f, 26) { ColorType = 1 });
                        CreateBone(new LeftBone(false, 5.45f, 26) { ColorType = 1 });
                        CreateBone(new RightBone(true, 5.45f, 26) { ColorType = 1 });
                        CreateBone(new DownBone(true, 5.45f, 26) { ColorType = 1 });
                        CreateBone(new UpBone(false, 5.45f, 26) { ColorType = 1 });
                        CreateBone(new LeftBone(true, 5.45f, 26) { ColorType = 1 });
                        CreateBone(new RightBone(false, 5.45f, 26) { ColorType = 1 });
                    }
                    var x = GetAll<DownBone>("A");
                    foreach (var v in x)
                    {
                        v.Speed -= 0.1f;
                    }
                }
                if (GametimeF == 5000)
                {
                    HeartAttribute.PurpleLineCount = 6;
                    SetBox(300, 280, 175);
                    SetSoul(4);
                }
                if (GametimeF >= 5000 && GametimeF <= 5880)
                {
                    if (GametimeF % 176 < 88)
                    {
                        if (GametimeF % 44 == 0)
                        {
                            CreateBone(new DownBone(false, 3.8f, 83));
                        }
                        if (GametimeF % 44 == 22)
                        {
                            CreateBone(new UpBone(false, 3.8f, 83));
                        }
                    }
                    else
                    {
                        if (GametimeF % 44 == 0)
                        {
                            CreateBone(new DownBone(true, 3.8f, 83));
                        }
                        if (GametimeF % 44 == 22)
                        {
                            CreateBone(new UpBone(true, 3.8f, 83));
                        }
                    }
                    if (GametimeF % 88 == 0)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(false, 0.2f, 84) { Tags = new[] { "A" }, ColorType = 1 });
                        CreateBone(new UpBone(true, 0.2f, 84) { Tags = new[] { "A" }, ColorType = 2 });
                    }
                    if (GametimeF % 88 == 44)
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new DownBone(true, 0.2f, 84) { Tags = new[] { "A" }, ColorType = 2 });
                        CreateBone(new UpBone(false, 0.2f, 84) { Tags = new[] { "A" }, ColorType = 1 });
                    }
                    if (GametimeF == 5000 + 300)
                    {
                        CreateGB(new NormalGB(new(100, 300 + 17.5f), Vector2.Zero, new(1, 0.5f), 0, 52, 5));
                        CreateGB(new NormalGB(new(100, 300 - 17.5f), Vector2.Zero, new(1, 0.5f), 0, 52, 5));
                    }

                    var all = GetAll<Bone>("A");
                    foreach (var bone in all)
                    {
                        if (bone is DownBone)
                        {
                            var s = bone as DownBone;
                            s.Speed = s.Speed * 0.95f + 10f * 0.05f;
                        }
                        if (bone is UpBone)
                        {
                            var s = bone as UpBone;
                            s.Speed = s.Speed * 0.95f + 10f * 0.05f;
                        }
                    }

                    float _time = GametimeF - 5000;
                    float v = MathF.Pow(Sin(_time / 44f * 360), 2);
                    if (_time % 44 < 22)
                    {
                        ScreenDrawing.ScreenPositionDetla = GetVector2(v * 32, 45);
                    }
                    if (_time % 44 >= 22)
                    {
                        ScreenDrawing.ScreenPositionDetla = GetVector2(v * 32, 135);
                    }
                }
                #endregion
                #region A-e-aa-aa
                if (GametimeF == 5866)
                {
                    SetSoul(1);
                    TP();
                    SetGreenBox();
                }
                for (int i = 0; i < 3; i++)
                {
                    if (GametimeF == 5861 + 173 * 0 + i * 2 + 173 * 4 * i)
                    {
                        float delta = 5.42f * 1;
                        float time = 5.51f * 8;
                        int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 1, 1, 1, 1, 1, 1, 1,
                        0, 0, 0, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                        int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 1, 1, 1, 1, 1, 1, 1,
                        1, 5, 1, 5, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                        Fortimes(rhythm1.Length, (t) =>
                        {
                            int _dir = t % 4 < 2 ? 1 : 0;
                            if (t % 2 == 0 && t >= 4 && t < 16)
                                AddInstance(new InstantEvent(time, () =>
                                {
                                    if (_dir == 1) ScreenDrawing.ScreenAngle = 14;
                                    if (_dir == 0) ScreenDrawing.ScreenAngle = -14;
                                }));
                            NoteSummon(t, time, rhythm1, rhythm2);
                            time += delta;
                        });
                    }
                    if (GametimeF == 5861 + 173 * 1 + i * 2 + 173 * 4 * i)
                    {
                        float delta = 5.42f * 1;
                        float time = 5.51f * 8;
                        int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 1, 1, 1, 1, 1, 1, 1,
                        0, 0, 0, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                        int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 1, 1, 1, 1, 1, 1, 1,
                        1, 5, 1, 5, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                        Fortimes(rhythm1.Length, (t) =>
                        {
                            int _dir = t % 4 < 2 ? 1 : 0;
                            if (t % 2 == 0 && t >= 4 && t < 16)
                                AddInstance(new InstantEvent(time, () =>
                                {
                                    if (_dir == 1) ScreenDrawing.ScreenAngle = -14;
                                    if (_dir == 0) ScreenDrawing.ScreenAngle = 14;
                                }));
                            NoteSummon(t, time, rhythm1, rhythm2);
                            time += delta;
                        });
                    }
                    if (GametimeF == 5861 + 173 * 2 + i * 2 + 173 * 4 * i)
                    {
                        float delta = 5.42f * 1;
                        float time = 5.51f * 8;
                        int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 1,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        0, 0, 0, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                        int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        1, 0, 5, 5, 0, 1, 1, 1,
                        1, 5, 1, 5, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                        Fortimes(rhythm1.Length, (t) =>
                        {
                            int _dir = t % 4 < 2 ? 1 : 0;
                            if (t % 2 == 0 && t >= 4 && t < 16 && rhythm1[t] != 0)
                                AddInstance(new InstantEvent(time, () =>
                                {
                                    if (_dir == 1)
                                    {
                                        ScreenDrawing.ScreenAngle = -8;
                                        ScreenDrawing.ScreenPositionDetla = new(20, 0);
                                    }
                                    if (_dir == 0)
                                    {
                                        ScreenDrawing.ScreenAngle = 8;
                                        ScreenDrawing.ScreenPositionDetla = new(-20, 0);
                                    }
                                }));
                            NoteSummon(t, time, rhythm1, rhythm2);
                            time += delta;
                        });
                    }
                    if (GametimeF == 5861 + 173 * 3 + i * 2 + 173 * 4 * i)
                    {
                        float delta = 5.42f * 1;
                        float time = 5.51f * 8;
                        int[] rhythm1 = {
                        2, 0, 0, 0, 1, 1, 1, 0,
                        5, 5, 0, 1, 1, 0, 5, 5,
                        0, 0, 0, 0, 4, 0, 3, 0,
                        4, 0, 3, 0, 3, 0, 3, 0,
                    };
                        int[] rhythm2 = {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        5, 5, 0, 1, 1, 0, 5, 5,
                        1, 5, 1, 5, 3, 0, 3, 0,
                        3, 0, 3, 0, 3, 0, 3, 0,
                    };
                        Fortimes(rhythm1.Length, (t) =>
                        {
                            int _dir = t % 4 < 2 ? 1 : 0;
                            if (t % 2 == 0 && t >= 4 && t < 16 && rhythm1[t] != 0)
                                AddInstance(new InstantEvent(time, () =>
                                {
                                    if (_dir == 1)
                                    {
                                        ScreenDrawing.ScreenPositionDetla = new(0, -15);
                                        ScreenDrawing.ScreenAngle = 8;
                                    }
                                    if (_dir == 0)
                                    {
                                        ScreenDrawing.ScreenPositionDetla = new(0, 15);
                                        ScreenDrawing.ScreenAngle = -8;
                                    }
                                }));
                            NoteSummon(t, time, rhythm1, rhythm2);
                            time += delta;
                        });
                    }
                    #endregion
                }
            }

            private static void NoteSummon(int t, float time, int[] rhythm1, int[] rhythm2)
            {
                if (rhythm1[t] == 2)
                {
                    CreateGB(new GreenSoulGB(time, 3, 0, 1));
                }
                else if (rhythm1[t] == 1)
                {
                    CreateArrow(time, (t % 2) * 2, 14, 0, 0);
                }
                else if (rhythm1[t] == 5)
                {
                    CreateArrow(time, ((t + 1) % 2) * 2, 14, 0, 0);
                }
                else if (rhythm1[t] == 4)
                {
                    CreateArrow(time, "R", 7, 0, 0);
                    CreateArrow(time, "+0", 7, 0, 1);
                }
                else if (rhythm1[t] == 3)
                {
                    CreateArrow(time, "R", 7, 0, 0);
                }

                if (rhythm2[t] == 2)
                {
                    CreateGB(new GreenSoulGB(time, 3, 1, 1));
                }
                else if (rhythm2[t] == 1)
                {
                    CreateArrow(time, (t % 2) * 2 + 1, 14, 1, 0);
                }
                else if (rhythm2[t] == 5)
                {
                    CreateArrow(time, ((t + 1) % 2) * 2 + 1, 14, 1, 0);
                }
                else if (rhythm2[t] == 4)
                {
                    CreateArrow(time, "D", 7, 1, 0);
                    CreateArrow(time, "+0", 7, 1, 1);
                }
                else if (rhythm2[t] == 3)
                {
                    CreateArrow(time, "D", 7, 1, 0);
                }
            }

            public void Start()
            {
                HeartAttribute.KR = true;
                HeartAttribute.KRDamage = 2;
                HeartAttribute.MaxHP = 32;
                HeartAttribute.Speed = 2.86f;
                SetGreenBox();
                TP();
                SetSoul(1);
                GametimeDelta = -39;
                //GametimeDelta = 2905;
                //GametimeDetla = this.BeatTime(1532);
                PlayOffset = GametimeDelta + 39;
                //SetSoul(0); 
            }
        }
    }
}