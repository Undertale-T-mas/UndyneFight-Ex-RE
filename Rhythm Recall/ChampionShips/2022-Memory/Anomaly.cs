using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class TranscendenceAnomaly : Scene
    {
        private const float SingleBeat = 62.5f / (190f / 60) / 4;
        public static float BeatTime(float x) => x * SingleBeat;
        private class MusicPlayer : GameObject
        {
            SoundEffectInstance music;
            public MusicPlayer()
            {
                music = Loader.Load<SoundEffect>("Musics\\Transcendence\\anomaly").CreateInstance();
                music.Play();
                UpdateIn120 = true;
            }

            int appearTime = 0;
            public override void Update()
            {
                appearTime++;
            }
        }

        public class AnomalyGenerater : WaveConstructor
        {
            public AnomalyGenerater() : base(TranscendenceAnomaly.SingleBeat)
            {
                AddInstance(new InstantEvent(0, StartEvent));
                UpdateIn120 = true;
                AddChild(new TimeRangedEvent(999999, UpdateEvent) { UpdateIn120 = true });
                GametimeDelta = -1.8f;
            }
            private void StartEvent()
            {

            }
            private void UpdateEvent()
            {
                if (At0thBeat(4)) PlaySound(Sounds.pierce);
                if (GameStates.IsKeyPressed120f(Keys.R)) GameStates.EndFight();
            }
        }

        int _difficulty;
        public TranscendenceAnomaly(int difficulty)
        {
            _difficulty = difficulty;
        }

        public override void Start()
        {
            InstanceCreate(new MusicPlayer());
            InstanceCreate(new AnomalyGenerater());

            GametimeDelta = 0; //reset it if in need
        }
        private static void IntoChart(int difficulty)
        {
            SongFightingScene.SceneParams @params =
                new(new SpecialOne.Game(), null, difficulty, "Content\\Musics\\Transcendence\\song", JudgementState.Strict, GameMode.Practice);
            GameStates.ResetScene(new SongLoadingScene(@params));
            SaveInfo custom = PlayerManager.CurrentUser.Custom;
            if (!custom.Nexts.ContainsKey("Transcendence"))
            {
                custom.PushNext(new("Transcendence{"));
                custom.PushNext(new("info:" + difficulty));
            }
        }
    }
    public class EndTimeAnomaly : Scene
    {
        private const float SingleBeat = 62.5f / (180f / 60) / 4;
        public static float BeatTime(float x) => x * SingleBeat;
        private class ParticleManager : GameObject
        {
            public ParticleManager()
            {
                UpdateIn120 = true;
            }
            int appearTime = 0;
            public override void Update()
            {
                appearTime++;
                // fore particle
                if (appearTime % 2 == 0)
                {
                    CreateEntity(new Particle(Color.White * Rand(0.6f, 0.9f) * 0.8f,
                        GetVector2(Rand(6f, 9f) * 0.9f, Rand(-12f, -18f)), Rand(12f, 18f) * 1.4f,
                        new(-12, Rand(0, 640)), FightResources.Sprites.square)
                    { DarkingSpeed = 1.1f, UpdateIn120 = true, Rotation = -15, AngelMode = true });
                }
                // back particle 
                CreateEntity(new Particle(Color.AliceBlue * Rand(0.6f, 0.9f) * 0.42f,
                    GetVector2(Rand(6f, 9f) * 0.6f, Rand(-12f, -18f) * (-0.6f)), Rand(12f, 18f) * 1.0f,
                    new(-12, Rand(0 - 170, 480)), FightResources.Sprites.square)
                { DarkingSpeed = 1.4f, UpdateIn120 = true, AutoRotate = true });
            }
        }
        private class ImageEntity : AutoEntity
        {
            CentreEasing.EaseBuilder Circuit;
            ValueEasing.EaseBuilder Rotate;
            public ImageEntity(Texture2D texture)
            {
                Centre = new(320, 240);
                UpdateIn120 = true;
                AngelMode = true;
                Image = texture;
                Circuit = new();
                Circuit.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(16, 16), BeatTime(16)));
                Circuit.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(16, 16), new(0, 0), BeatTime(16)));
                Circuit.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(-16, 16), BeatTime(16)));
                Circuit.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(-16, 16), new(0, 0), BeatTime(16)));
                Circuit.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(-16, -16), BeatTime(16)));
                Circuit.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(-16, -16), new(0, 0), BeatTime(16)));
                Circuit.Insert(BeatTime(16), CentreEasing.EaseOutBack(new(0, 0), new(16, -16), BeatTime(16)));
                Circuit.Insert(BeatTime(16), CentreEasing.EaseOutSine(new(16, -16), new(0, 0), BeatTime(16)));

                Rotate = new();
                Rotate.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 1.8f, BeatTime(16)));
                Rotate.Insert(BeatTime(16), ValueEasing.EaseInQuad(1.8f, 0, BeatTime(16)));
                Rotate.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, -1.8f, BeatTime(16)));
                Rotate.Insert(BeatTime(16), ValueEasing.EaseInQuad(-1.8f, 0, BeatTime(16)));
                Rotate.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, 1.8f, BeatTime(16)));
                Rotate.Insert(BeatTime(16), ValueEasing.EaseInQuad(1.8f, 0, BeatTime(16)));
                Rotate.Insert(BeatTime(16), ValueEasing.EaseOutQuad(0, -1.8f, BeatTime(16)));
                Rotate.Insert(BeatTime(16), ValueEasing.EaseInQuad(-1.8f, 0, BeatTime(16)));
                Scale = 1.15f;
            }
            float timer = 999990;
            public override void Update()
            {
                timer += 0.5f;
                if (timer > BeatTime(128))
                {
                    timer = 0;
                    Circuit.Run((s) =>
                    {
                        Centre = Centre * 0.995f + (s + new Vector2(320, 240)) * 0.005f;
                    });
                    Rotate.Run((s) =>
                    {
                        Rotation = s * 0.6f;
                    });
                }

                Alpha = 1;
            }
        }
        private class MusicPlayer : GameObject
        {
            SoundEffectInstance music;
            public MusicPlayer()
            {
                music = Loader.Load<SoundEffect>("Musics\\EndTime\\song").CreateInstance();
                music.Play();
                UpdateIn120 = true;
            }

            int appearTime = 0;
            public override void Update()
            {
                appearTime++;
            }
        }

        private static Difficulty Availability
        {
            get
            {
                var user = PlayerManager.CurrentUser;
                if (user == null) return Difficulty.ExtremePlus;

                SaveInfo custom = user.Custom;
                return !custom.Nexts.ContainsKey("Transcendence")
                    ? Difficulty.ExtremePlus
                    : (Difficulty)(custom.Nexts["Transcendence"].Nexts["info"]).IntValue;
            }
        }

        public class AnomalyGenerater : WaveConstructor
        {
            public AnomalyGenerater() : base(EndTimeAnomaly.SingleBeat)
            {
                AddInstance(new InstantEvent(0, StartEvent));
                UpdateIn120 = true;
                AddChild(new TimeRangedEvent(999999, UpdateEvent) { UpdateIn120 = true });
            }
            public static AnomalyGenerater game;
            public static AnomalyGenerater instance;
            private static class MainEffect
            {
                public static void PlusGreenSoulRotate(float rot, float duration)
                {
                    float start = Heart.Rotation;
                    float end = rot;
                    float del = start - end;
                    float t = 0;
                    AddInstance(new TimeRangedEvent(0, duration, () =>
                    {
                        float x = t / (duration - 1);
                        float f = 2 * x - x * x;
                        Heart.InstantSetRotation(start - del * f);
                        t++;
                    }));
                }
                public static void BGshining()
                {
                    float alp = 0;
                    game.ForBeat(2, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Gray * alp;
                        alp += 0.0075f;
                    });
                    game.ForBeat(2, 2, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Gray * alp;
                        alp -= 0.0075f;
                    });
                }
                public static void SoftSetBox(Vector2 center, float width, float height, float duration, int SetMission, int type)
                {
                    float t = 0;
                    float startx = BoxStates.Centre.X;
                    float starty = BoxStates.Centre.Y;
                    float endx = center.X;
                    float endy = center.Y;
                    float delx = startx - endx;
                    float dely = starty - endy;
                    if (type == 1)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = 2 * x - x * x;
                            SetBoxMission(SetMission);
                            InstantSetBox(new Vector2(startx - delx * f, starty - dely * f), width, height);
                            t++;
                        }));
                    }
                    if (type == 2)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = x * x;
                            SetBoxMission(SetMission);
                            InstantSetBox(new Vector2(startx - delx * f, starty - dely * f), width, height);
                            t++;
                        }));
                    }
                }
                public static void SoftTP(Vector2 center, float duration, int SetMission, int type)
                {
                    float t = 0;
                    float startx = Heart.Centre.X;
                    float starty = Heart.Centre.Y;
                    float endx = center.X;
                    float endy = center.Y;
                    float delx = startx - endx;
                    float dely = starty - endy;
                    if (type == 1)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = 2 * x - x * x;
                            SetPlayerMission(SetMission);
                            InstantTP(new Vector2(startx - delx * f, starty - dely * f));
                            t++;
                        }));
                    }
                    if (type == 2)
                    {
                        AddInstance(new TimeRangedEvent(0, duration, () =>
                        {
                            float x = t / (duration - 1);
                            float f = x * x;
                            SetPlayerMission(SetMission);
                            InstantTP(new Vector2(startx - delx * f, starty - dely * f));
                            t++;
                        }));
                    }
                }
            }
            private static class Barrage
            {
                public static void effect00()
                {
                    ScreenDrawing.ScreenScale = 1.6f;
                    ScreenDrawing.BoundColor = new(95, 137, 154, 60);
                    DrawingUtil.MaskSquare m = new(0, 0, 640, 480, game.BeatTime(38), Color.Black, 1);
                    CreateEntity(m);
                    float t = 0;
                    game.ForBeat(4, () =>
                    {
                        m.alpha -= 0.0075f;
                    });
                    game.ForBeat(37, () =>
                    {
                        ScreenDrawing.LeftBoundDistance = 320 - 90 + Sin(t * 1.5f) * 10;
                        ScreenDrawing.RightBoundDistance = 320 - 90 + Sin(t * 1.5f) * 10;
                        t++;
                    });
                    game.DelayBeat(39.5f, () =>
                    {
                        m.Dispose();
                        ScreenDrawing.LeftBoundDistance = 0;
                        ScreenDrawing.RightBoundDistance = 0;
                    });
                }
                public static void effect01()
                {
                    game.DelayBeat(32, () =>
                    {
                        DrawingUtil.MinusScreenScale(0.2f, game.BeatTime(2));
                        game.ForBeat(2, 2, () =>
                        {
                            ScreenDrawing.ScreenScale += 0.075f;
                        });
                        game.DelayBeat(2, () =>
                        {
                            ScreenDrawing.WhiteOut(game.BeatTime(2));
                        });
                        game.DelayBeat(4, () =>
                        {
                            ScreenDrawing.ScreenScale = 1;
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        float alp = 0.8f;
                        DrawingUtil.NormalLine a = new(0, Rand(150, 195), 640, LastRand, game.BeatTime(12), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine b = new(0, Rand(195, 240), 640, LastRand, game.BeatTime(12), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine c = new(0, Rand(240, 285), 640, LastRand, game.BeatTime(12), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine d = new(0, Rand(285, 330), 640, LastRand, game.BeatTime(12), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine e = new(0, Rand(105, 150), 640, LastRand, game.BeatTime(12), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine f = new(0, Rand(330, 375), 640, LastRand, game.BeatTime(12), alp) { depth = 0.01f };
                        CreateEntity(a);
                        CreateEntity(b);
                        CreateEntity(c);
                        CreateEntity(d);
                        CreateEntity(e);
                        CreateEntity(f);
                        for (int i = 0; i < game.BeatTime(6); i++)
                        {
                            AddInstance(new InstantEvent(i * 2, () =>
                            {
                                a.alpha = alp;
                                b.alpha = alp;
                                c.alpha = alp;
                                d.alpha = alp;
                                e.alpha = alp;
                                f.alpha = alp;
                                alp -= 0.005f;
                            }));
                            AddInstance(new InstantEvent(i * 2 + 1, () =>
                            {
                                a.alpha = 0;
                                b.alpha = 0;
                                c.alpha = 0;
                                d.alpha = 0;
                                e.alpha = 0;
                                f.alpha = 0;
                            }));
                        }
                    });
                }
                public static void rhythm01()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "(R)(D1)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        //
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        //
                        "(R1)(D)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R1","/","/","/",    "/","/","/","/",
                        //
                        "R1","/","/","/",    "/","/","/","/",    "R1","/","/","/",    "/","/","/","/",
                        "R1","/","/","/",    "/","/","/","/",    "R1","/","/","/",    "/","/","/","/",
                        ////
                        "(D)(+21)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(+21)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "(R1)(+21)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        /* - End Start - */
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(t, 3.15f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect02()
                {

                }
                public static void rhythm02()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "($0)($0)","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "(D)(+201)","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        "D","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "(R)(D1)","/","/","/",    "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        "D1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //
                        "(D)(+211)","/","/","/",    "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        "D1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        ////
                        "(R)(D1)","/","/","/",    "R","/","/","/",    "(R)(D1)","/","/","/",    "R","/","/","/",
                        "(D)(D1)","/","/","/",    "R","/","/","/",    "(R)(D1)","/","/","/",    "R","/","/","/",
                        //
                        "(R)(D1)","/","/","/",    "R","/","/","/",    "(R)(D1)","/","/","/",    "R","/","/","/",
                        "(D)(D1)","/","/","/",    "R","/","/","/",    "(R)(D1)","/","/","/",    "R","/","/","/",
                        //
                        "(R1)(D)","/","/","/",    "R1","/","/","/",    "(R1)(D)","/","/","/",    "R1","/","/","/",
                        "(D1)(D)","/","/","/",    "R1","/","/","/",    "(R1)(D)","/","/","/",    "R1","/","/","/",
                        //
                        "(R1)(D)","/","/","/",    "R1","/","/","/",    "(R1)(D)","/","/","/",    "R1","/","/","/",
                        "D","/","+0","/",    "/","/","D1","/",    "+01","/","/","/",    "D","/","+0","/",
                        ////
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(t, 6.25f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect03()
                {
                    game.DelayBeat(16, () =>
                    {
                        MainEffect.PlusGreenSoulRotate(5, game.BeatTime(4));
                    });
                    game.DelayBeat(20, () =>
                    {
                        MainEffect.PlusGreenSoulRotate(-5, game.BeatTime(4));
                    });
                    game.DelayBeat(24, () =>
                    {
                        MainEffect.PlusGreenSoulRotate(0, game.BeatTime(4));
                    });
                    game.DelayBeat(28, () =>
                    {
                        float p = 0;
                        game.ForBeat(4, () =>
                        {
                            Heart.InstantSetRotation(p * p * 0.075f);
                            ScreenDrawing.ScreenScale = p * 0.02f + 1;
                            p++;
                        });
                    });
                }
                public static void rhythm03()
                {
                    game.DelayBeat(0, () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(2), Rand(0, 3), 1, game.BeatTime(1)));
                        CreateEntity(new GreenSoulGB(game.BeatTime(3.5f), LastRand, 1, game.BeatTime(2.5f)));
                    });
                    game.DelayBeat(4, () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(2), Rand(0, 3), 1, game.BeatTime(1)));
                        CreateEntity(new GreenSoulGB(game.BeatTime(3.5f), LastRand, 1, game.BeatTime(2.5f)));
                    });
                    game.DelayBeat(8, () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(2), Rand(0, 3), 0, game.BeatTime(1)));
                        CreateEntity(new GreenSoulGB(game.BeatTime(3.5f), LastRand, 0, game.BeatTime(2.5f)));
                    });
                    game.DelayBeat(12, () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(2), Rand(0, 3), 0, game.BeatTime(1)));
                        CreateEntity(new GreenSoulGB(game.BeatTime(3.5f), LastRand, 0, game.BeatTime(2.5f)));
                    });
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "R","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        "D","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "R","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        "D","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        "D1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //
                        "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        "D1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        ////
                        "($0)($2)","/","/","/",    "($0)($2)","/","/","/",    "($0)($2)","/","/","/",    "($0)($2)","/","/","/",
                        "($0)($2)","/","/","/",    "($0)($2)","/","/","/",    "($0)($2)","/","/","/",    "($0)($2)","/","/","/",
                        //
                        "($01)($21)","/","/","/",    "($01)($21)","/","/","/",    "($01)($21)","/","/","/",    "($01)($21)","/","/","/",
                        "($01)($21)","/","/","/",    "($01)($21)","/","/","/",    "($01)($21)","/","/","/",    "($01)($21)","/","/","/",
                        //
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            instance.CreateArrows(t, 6.25f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                    float t1 = game.BeatTime(26);
                    string[] rhythm1 =
                    {
                        /*
                        "$0","$1","$2","$1","$0","$1",    "$2","$1","$0","$1","$2","$1",
                        "$0","$1","$2","$1","$0","$1",    "$2","$1","$0","$1","$2","$1",
                        //
                        "$0","$1","$2","$1","$0","$1",    "$2","$1","$0","$1","$2","$1",
                        "$0","$1","$2","$1","$0","$1",    "$2","$1","$0","$1","$2","$1",
                        ////
                        */
                        "$0","$2","$0","$2","$0","$2",    "$0","$2","$0","$2","$0","$2",
                        "$0","$2","$0","$2","$0","$2",    "$0","$2","$0","$2","$0","$2",
                        //
                        "($0)($31)","($2)($11)","($0)($31)","($2)($11)","($0)($31)","($2)($11)",    "($0)($31)","($2)($11)","($0)($31)","($2)($11)","($0)($31)","($2)($11)",
                        "($0)($31)","($2)($11)","($0)($31)","($2)($11)","($0)($31)","($2)($11)",    "($0)($31)","($2)($11)","($0)($31)","($2)($11)","($0)($31)","($2)($11)",
                        ////
                    };
                    for (int i = 0; i < rhythm1.Length; i++)
                    {
                        if (rhythm1[i] == "/")
                        {
                            t1 += game.BeatTime(1 / 6f);
                        }
                        else if (rhythm1[i] != "/")
                        {
                            instance.CreateArrows(t1, 6.25f, rhythm1[i]);
                            t1 += game.BeatTime(1 / 6f);
                        }
                    }
                }
                public static void rhythm04()
                {
                    game.RegisterFunctionOnce("SoulShining", () =>
                    {
                        SetSoul(1);
                    });
                    float t = game.BeatTime(6);
                    string[] rhythm =
                    {
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "$1","/","/","/",    "$2","/","/","/",
                        //
                        "($1)(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "$31","/","/","/",    "$01","/","/","/",
                        //
                        "($31)(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "$0","/","/","/",    "$1","/","/","/",
                        //
                        "$0","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$311","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "($101)(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "$202","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$012","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($3)(+01)(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($2)(+2)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "$1(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$011","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "$201(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$31","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($2)(+01)(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($3)(+01)(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$101","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "$21(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "$1","/","/","/",    "$2","/","/","/",
                        //
                        "$31","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(+01)(SoulShining)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            game.NormalizedChart(t, 2.65f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect04()
                {
                    game.DelayBeat(0, () =>
                    {
                        DrawingUtil.SetScreenScale(1, game.BeatTime(16));
                        float rot1 = Heart.Rotation;
                        game.ForBeat(60, () =>
                        {
                            Heart.InstantSetRotation(rot1);
                            rot1 += 0.75f;
                        });
                    });
                    for (int i = 0; i < 7; i++)
                    {
                        game.DelayBeat(i * 8, () =>
                        {
                            MainEffect.BGshining();
                        });
                    }
                    game.DelayBeat(60, () =>
                    {
                        Heart.Split();
                        SetBoxMission(0);
                        SetBox(new Vector2(320 - 126, 480), 84, 84);
                        SetPlayerMission(0);
                        Heart.InstantSetRotation(90);
                        TP(new Vector2(320 - 126, 480));
                        Heart.Split();
                        SetBoxMission(1);
                        SetBox(new Vector2(320 - 42, 480), 84, 84);
                        SetPlayerMission(2);
                        Heart.InstantSetRotation(-90);
                        TP(new Vector2(320 - 42, 480));
                        SetBoxMission(2);
                        SetBox(new Vector2(320 + 42, 480), 84, 84);
                        SetPlayerMission(1);
                        Heart.InstantSetRotation(90);
                        TP(new Vector2(320 + 42, 480));
                        Heart.Split();
                        SetBoxMission(3);
                        SetBox(new Vector2(320 + 126, 480), 84, 84);
                        SetPlayerMission(3);
                        Heart.InstantSetRotation(-90);
                        TP(new Vector2(320 + 126, 480));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$A", new Vector2(316 - 126, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$D", new Vector2(316 - 42, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$Left", new Vector2(300 + 42, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$Right", new Vector2(308 + 126, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                    });
                    game.DelayBeat(62, () =>
                    {
                        SetBoxMission(0);
                        MainEffect.SoftSetBox(new(320, 240), 84, 84, game.BeatTime(2), 0, 2);
                        SetPlayerMission(0);
                        MainEffect.SoftTP(new(320, 240), game.BeatTime(2), 0, 2);
                        MainEffect.PlusGreenSoulRotate(0, game.BeatTime(2));
                        SetBoxMission(1);
                        MainEffect.SoftSetBox(new(320, 240), 84, 84, game.BeatTime(2), 1, 2);
                        SetPlayerMission(1);
                        MainEffect.SoftTP(new(320, 240), game.BeatTime(2), 1, 2);
                        MainEffect.PlusGreenSoulRotate(0, game.BeatTime(2));
                        SetBoxMission(2);
                        MainEffect.SoftSetBox(new(320, 240), 84, 84, game.BeatTime(2), 2, 2);
                        SetPlayerMission(2);
                        MainEffect.SoftTP(new(320, 240), game.BeatTime(2), 2, 2);
                        MainEffect.PlusGreenSoulRotate(0, game.BeatTime(2));
                        SetBoxMission(3);
                        MainEffect.SoftSetBox(new(320, 240), 84, 84, game.BeatTime(2), 3, 2);
                        SetPlayerMission(3);
                        MainEffect.SoftTP(new(320, 240), game.BeatTime(2), 3, 2);
                        MainEffect.PlusGreenSoulRotate(0, game.BeatTime(2));
                    });
                    game.DelayBeat(64, () =>
                    {
                        SetBoxMission(0);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        SetBoxMission(1);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        SetBoxMission(2);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        SetBoxMission(3);
                        InstantSetBox(new Vector2(320, 240), 84, 84);
                        SetPlayerMission(0);
                        Player.hearts[1].Teleport(new(320, 240));
                        Player.hearts[1].Merge(Player.hearts[0]);
                        SetPlayerMission(0);
                        InstantTP(new(320, 240));
                        SetPlayerMission(2);
                        Player.hearts[3].Teleport(new(320, 240));
                        Player.hearts[3].Merge(Player.hearts[2]);
                        SetPlayerMission(2);
                        InstantTP(new(320, 240));
                        SetPlayerMission(0);
                        Player.hearts[2].Teleport(new(320, 240));
                        Player.hearts[2].Merge(Player.hearts[0]);
                        SetPlayerMission(0);
                        InstantTP(new(320, 240));
                        SetSoul(1);
                    });
                }
                public static void rhythm05()
                {
                    game.RegisterFunctionOnce("bGBl", () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(4), "R", 0, game.BeatTime(3)));
                    });
                    game.RegisterFunctionOnce("bGBs", () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(4), "D", 0, game.BeatTime(0.85f)));
                    });
                    game.RegisterFunctionOnce("rGBl", () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(4), "R", 1, game.BeatTime(3)));
                    });
                    game.RegisterFunctionOnce("rGBs", () =>
                    {
                        CreateEntity(new GreenSoulGB(game.BeatTime(4), "D", 1, game.BeatTime(0.85f)));
                    });
                    float t = game.BeatTime(4);
                    string[] rhythm =
                    {
                        "$3","/","/","/",    "R","/","+0","/",    "/","/","/","/",    "(R)(+01)","/","/","/",
                        "D","/","+0","/",    "/","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "D","/","/","/",    "R","/","+0","/",    "/","/","/","/",    "(R)(+01)","/","/","/",
                        "D","/","+0","/",    "/","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "D","/","/","/",    "R","/","+0","/",    "/","/","/","/",    "(R)(+01)","/","/","/",
                        "D","/","+011","/",    "+0","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "D","/","/","/",    "R","/","+0","/",    "/","/","R","/",    "+0","/","/","/",
                        "D","/","+01","/",    "+0","/","/","/",    "(D)(+2)","/","/","/",    "/","/","/","/",
                        ////
                        "$31","/","/","/",    "R1","/","+01","/",    "/","/","/","/",    "(R1)(+011)","/","/","/",
                        "D1","/","+01","/",    "/","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //
                        "D1","/","/","/",    "R1","/","+01","/",    "/","/","/","/",    "(R1)(+011)","/","/","/",
                        "D1","/","+01","/",    "/","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //
                        "D1","/","/","/",    "R1","/","+01","/",    "/","/","R1","/",    "+01","/","/","/",
                        "D1","/","+001","/",    "/","/","+01","/",    "R1","/","+01","/",    "R1","/","/","/",
                        //
                        "D1","/","/","/",    "R1","/","+01","/",    "/","/","R1","/",    "+0","/","+01","/",
                        "D11","/","/","/",    "+111","/","/","/",    "+111","/","/","/",    "+111","/","/","/",
                        ////
                        "($31)(#3#R)","/","/","/",    "R1","/","+01","/",    "/","/","/","/",    "R1","/","/","/",
                        "(D1)(R)","/","+01","/",    "/","/","/","/",    "R1","/","/","/",    "R1","/","/","/",
                        //
                        "(D1)(#3#R)","/","/","/",    "R1","/","+01","/",    "/","/","/","/",    "R1","/","/","/",
                        "(D1)(R)","/","+01","/",    "+01","/","/","/",    "(R)(+01)","/","/","/",    "(R)(+01)","/","/","/",
                        //
                        "(D1)(#2#D)","/","/","/",    "R","/","+01","/",    "(R)(#2#D1)","/","/","/",    "R1","/","+0","/",
                        "(D1)(#2#D)","/","+011","/",    "/","/","+0","/",    "(R)(#2#D1)","/","+0","/",    "+01","/","/","/",
                        //
                        "(D1)(R)","/","/","/",    "R1","/","+01","/",    "(+01)(R)","/","/","/",    "R1","/","/","/",
                        "(D1)(R)","/","/","/",    "R1","/","+01","/",    "(+01)(R)","/","/","/",    "R1","/","/","/",
                        ////
                        "($3)(#3#R1)","/","/","/",    "R","/","+0","/",    "R1","/","/","/",    "R1","/","/","/",
                        "(D)(R1)","/","+0","/",    "/","/","/","/",    "(R)(R1)","/","/","/",    "R1","/","/","/",
                        //
                        "(D)(#3#R1)","/","/","/",    "R","/","+0","/",    "(R)(R1)","/","/","/",    "R","/","/","/",
                        "(D)(R1)","/","+0","/",    "+0","/","/","/",    "(R)(+01)","/","+011","/",    "+0","/","/","/",
                        //
                        "(#3#R)(R1)","/","/","/",    "R","/","+0","/",    "R1","/","R","/",    "+0","/","/","/",
                        "(D)(R1)","/","+001","/",    "+01","/","/","/",    "(R)(R1)","/","+001","/",    "+01","/","/","/",
                        //
                        "(D)(#3#R1)","/","/","/",    "R1","/","+01","/",    "R","/","R1","/",    "+01","/","/","/",
                        "D11","/","-111","/",    "-111","/","-111","/",    "(<^$3)(>^$31)","/","/","/",    "/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            game.NormalizedChart(t, 6, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect05()
                {
                    game.DelayBeat(63, () =>
                    {
                        Heart.Split();
                        SetBoxMission(0);
                        MainEffect.SoftSetBox(new(320 - 126, 480), 84, 84, game.BeatTime(3), 0, 1);
                        SetPlayerMission(0);
                        Heart.InstantSetRotation(90);
                        MainEffect.SoftTP(new(320 - 126, 480), game.BeatTime(3), 0, 1);
                        Heart.Split();
                        SetBoxMission(1);
                        MainEffect.SoftSetBox(new(320 - 42, 480), 84, 84, game.BeatTime(3), 1, 1);
                        SetPlayerMission(2);
                        Heart.InstantSetRotation(90);
                        MainEffect.SoftTP(new(320 + 42, 480), game.BeatTime(3), 2, 1);
                        SetBoxMission(2);
                        MainEffect.SoftSetBox(new(320 + 42, 480), 84, 84, game.BeatTime(3), 2, 1);
                        SetPlayerMission(1);
                        Heart.InstantSetRotation(-90);
                        MainEffect.SoftTP(new(320 - 42, 480), game.BeatTime(3), 1, 1);
                        Heart.Split();
                        SetBoxMission(3);
                        MainEffect.SoftSetBox(new(320 + 126, 480), 84, 84, game.BeatTime(3), 3, 1);
                        SetPlayerMission(3);
                        Heart.InstantSetRotation(-90);
                        MainEffect.SoftTP(new(320 + 126, 480), game.BeatTime(3), 3, 1);
                    });
                }
                public static void rhythm06A()
                {
                    game.RegisterFunction("k0", () =>
                    {
                        SetBoxMission(0);
                    });
                    game.RegisterFunction("k1", () =>
                    {
                        SetBoxMission(1);
                    });
                    game.RegisterFunction("k2", () =>
                    {
                        SetBoxMission(2);
                    });
                    game.RegisterFunction("k3", () =>
                    {
                        SetBoxMission(3);
                    });
                    float t = game.BeatTime(3);
                    string[] rhythm =
                    {
                        //effected arrow
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            game.NormalizedChart(t, 6.35f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void rhythm06B()
                {
                    game.RegisterFunctionOnce("k0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 1, 0);
                    });
                    game.RegisterFunctionOnce("k1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0);
                    });
                    game.RegisterFunctionOnce("k2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0);
                    });
                    game.RegisterFunctionOnce("k3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 0, 0);
                    });
                    game.RegisterFunctionOnce("Lk0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 1, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunctionOnce("Lk1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunctionOnce("Lk2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunctionOnce("Lk3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 0, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunctionOnce("Rk0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 1, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunctionOnce("Rk1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunctionOnce("Rk2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunctionOnce("Rk3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 0, 0, ArrowAttribute.RotateR);
                    });
                    float t = game.BeatTime(0);
                    string[] rhythm =
                    {
                        //common arrow
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "k0n","/","Lk0n","/",    "k1n","/","Lk1n","/",    "k2n","/","Lk2n","/",    "k3n","/","Lk3n","/",
                        //
                        "k0n","/","Lk0n","/",    "k1n","/","Lk1n","/",    "k3n","/","Rk3n","/",    "k2n","/","Rk2n","/",
                        "k1n","/","Rk1n","/",    "k0n","/","Rk0n","/",    "k3n","/","Rk3n","/",    "k2n","/","Rk2n","/",
                        //

                        //
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        if (rhythm[i] == "/")
                        {
                            t += game.BeatTime(0.125f);
                        }
                        else if (rhythm[i] != "/")
                        {
                            game.NormalizedChart(t, 6.25f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect06()
                {

                }
            }
            public void StartEvent()
            {
                // finish by yourself 

                GametimeDelta = -6.5f - BeatTime(16);
            }
            public void UpdateEvent()
            {
                // finish by yourself  
                if (GametimeF < 0) return;
                if (InBeat(0))
                {
                    RegisterFunctionOnce("Make", () => { PlaySound(Sounds.pierce); });
                    float time = BeatTime(0);
                    string[] rhythm = {
                        "Make", "", "", "", "", "", "", "",
                        "Make", "", "", "", "", "", "", "",
                        "Make", "", "", "", "", "", "", "",
                        "Make", "", "", "", "", "", "", "",
                        "Make", "", "", "", "", "", "", "",
                        "Make", "", "", "", "", "", "", "",
                        "Make", "", "", "", "", "", "", "",
                        "Make", "", "", "", "", "", "", "",
                    };
                    for (int i = 0; i < rhythm.Length; i++)
                    {
                        NormalizedChart(time, 6.0f, rhythm[i]);
                        time += BeatTime(1);
                    }
                }
            }
            /*   public void UpdateEvent()
               {
                   // finish by yourself

                   if (InBeat(-2.24f)) Barrage.effect00();
                   if (InBeat(2)) Barrage.effect01();
                   if (InBeat(2 - 2)) Barrage.rhythm01();
                   if (InBeat(2 + 4 * 9)) Barrage.effect02();
                   if (InBeat(2 + 4 * 9 - 2)) Barrage.rhythm02();
                   if (InBeat(2 + 4 * 17)) Barrage.effect03();
                   if (InBeat(2 + 4 * 17 - 2)) Barrage.rhythm03();
                   if (InBeat(2 + 4 * 25 - 6)) Barrage.rhythm04();
                   if (InBeat(2 + 4 * 25)) Barrage.effect04();
                   if (InBeat(2 + 4 * 41 - 4)) Barrage.rhythm05();
                   if (InBeat(2 + 4 * 41)) Barrage.effect05();
                   if (InBeat(2 + 4 * 57 - 3)) Barrage.rhythm06A();
                   if (InBeat(2 + 4 * 57 - 3)) Barrage.rhythm06B();
                   if (InBeat(2 + 4 * 57)) Barrage.effect06(); 
               }*/
        }

        public override void Start()
        {
            Texture2D texture = Loader.Load<Texture2D>("Musics\\EndTime\\paint");

            InstanceCreate(new ImageEntity(texture));
            InstanceCreate(new ParticleManager());
            InstanceCreate(new MusicPlayer());
            InstanceCreate(new AnomalyGenerater());

            GametimeDelta = 0; //reset it if in need
        }
        private static void IntoChart()
        {
            SongFightingScene.SceneParams @params =
                new(new SpecialOne.Game(), null, 5, "Content\\Musics\\EndTime\\song", JudgementState.Strict, GameMode.None);
            GameStates.ResetScene(new SongLoadingScene(@params));
        }
    }
}