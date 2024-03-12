using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
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
            public override void Dispose()
            {
                music.Stop();
                base.Dispose();
            }
        }

        private class Charactor : AutoEntity
        {
            public Charactor() : base()
            {
                this.Image = Loader.Load<Texture2D>("Musics\\EndTime\\sprite_charactor");
            }
            public override void Update()
            {
                this.Alpha = 1f;
                this.Centre = new(320, 130);
                this.Scale = 0.5f;
            }
        }

        public class AnomalyGenerater : WaveConstructor
        {
            private class ParticleGenerater : GameObject
            {
                public float Rotation { get; set; } = 90;
                public override void Update()
                {
                    this.AddChild(new Particle(Color.White * Rand(0.4f, 0.6f), GetVector2(Rand(2f, 3f), Rand(-8f, 8f) + Rotation),
                        Rand(8f, 16f), new(Rand(-100, 740), -12), Sprites.square)
                    { DarkingSpeed = 2f, AutoRotate = true });
                }

            }
            public AnomalyGenerater() : base(TranscendenceAnomaly.SingleBeat)
            {
                AddInstance(new InstantEvent(0, this.StartEvent));
                this.UpdateIn120 = true;
                this.AddChild(new TimeRangedEvent(999999, UpdateEvent) { UpdateIn120 = true });
                GametimeDelta = -1.8f;
            }
            private void StartEvent()
            {
                Shader SinWave;
                SinWave = new Shader(Loader.Load<Effect>("Musics\\DustTrust\\shake"));

                SinWave.Parameters["frequency"].SetValue(0f);
                SinWave.Parameters["distance"].SetValue(new Vector2(0f, 0f));
                SinWave.Parameters["range"].SetValue(0f);
                SinWave.Parameters["frequency2"].SetValue(0f);
                SinWave.Parameters["range2"].SetValue(0f);
                SinWave.Parameters["time"].SetValue(0f);
                SinWave.Parameters["time2"].SetValue(0f);

                GameObject obj1;
                AddInstance(obj1 = new ParticleGenerater() { Rotation = 77 });

                AddInstance(new Charactor());

                RegisterFunctionOnce("SceneOut", () =>
                {
                    // sinwave shake
                    ScreenDrawing.Shaders.Filter filter;
                    ScreenDrawing.SceneRendering.InsertProduction(filter = new ScreenDrawing.Shaders.Filter(SinWave, 0.6755f));
                    DelayBeat(16, () =>
                    {
                        filter.Dispose();
                    });
                    float a = 0;
                    ForBeat(16, () =>
                    {
                        SinWave.Parameters["time"].SetValue(a); a += 0.26f;
                        SinWave.Parameters["time2"].SetValue(a * 1.5f);
                    });

                    ValueEasing.EaseBuilder rgbShake = new();
                    rgbShake.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(0, 1.8f, BeatTime(1)));
                    rgbShake.Insert(BeatTime(7), ValueEasing.EaseOutCubic(1.8f, 0.6f, BeatTime(7)));
                    rgbShake.Insert(BeatTime(1f), ValueEasing.EaseOutQuad(1, 8, BeatTime(1)));
                    rgbShake.Insert(BeatTime(7), ValueEasing.EaseOutCubic(8, 0, BeatTime(7)));
                    rgbShake.Run(s =>
                    {
                        SinWave.Parameters["frequency"].SetValue(s * 15f + 150f);
                        SinWave.Parameters["frequency2"].SetValue(s * 35f + 220f);
                        SinWave.Parameters["range2"].SetValue(s);
                        SinWave.Parameters["range"].SetValue(s * 1.25f);
                    });

                    //background color
                    DelayBeat(8, () =>
                    {
                        ScreenDrawing.SceneOut(Color.Black * 0.98f, BeatTime(8f - 1));
                    });
                    for (int i = 0; i < 7; i++)
                    {
                        int t = i;
                        DelayBeat(t, () =>
                        {
                            ScreenDrawing.MakeFlicker(Color.Black * (t * 0.06f + 0.4f));
                        });
                        if (i >= 4)
                        {
                            DelayBeat(t + 0.5f, () =>
                            {
                                ScreenDrawing.MakeFlicker(Color.Black * (t * 0.06f + 0.4f));
                            });
                        }
                    }

                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(8), ValueEasing.EaseInElastic(0.0f, 0.5f, BeatTime(8)));
                    builder.Insert(BeatTime(24), ValueEasing.EaseOutCubic(0.5f, 1.0f, BeatTime(24)));
                    builder.Run((s) =>
                    {
                        ScreenDrawing.BoundColor = Color.Lerp(Color.Silver, Color.Gray * 0.5f, s);
                    });
                });
                RegisterFunctionOnce("AlphaChange", () =>
                {
                    ValueEasing.EaseBuilder alphaEase = new();
                    alphaEase.Adjust = false;
                    alphaEase.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0.4f, 1f, BeatTime(4)));
                    alphaEase.Insert(BeatTime(12), ValueEasing.Stable(1f));
                    alphaEase.Insert(BeatTime(16), ValueEasing.EaseInQuart(1f, 0f, BeatTime(16)));
                    alphaEase.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0.4f, 1f, BeatTime(4)));
                    alphaEase.Insert(BeatTime(12), ValueEasing.Stable(1f));
                    alphaEase.Insert(BeatTime(16), ValueEasing.EaseInQuart(1f, 0f, BeatTime(16)));
                    alphaEase.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0.4f, 1f, BeatTime(4)));
                    for (int i = 0; i < 7; i++)
                        alphaEase.Insert(BeatTime(4), ValueEasing.EaseOutQuad(0.8f, 1f, BeatTime(4)));
                    for (int i = 0; i < 16; i++)
                        alphaEase.Insert(BeatTime(1), ValueEasing.EaseOutQuad(0.69f, 0.8f, BeatTime(1)));
                    alphaEase.Run(s => ScreenDrawing.MasterAlpha = s);
                });

                RegisterFunctionOnce("End", () =>
                {
                    IntoChart(_difficulty);
                });

                string[] rhythm = {
                        "AlphaChange", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",

                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "SceneOut", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "End",
                    };
                CreateChart(0, BeatTime(8), 0, rhythm);
            }
            private void UpdateEvent()
            {
                //if (GameStates.IsKeyPressed120f(InputIdentity.Reset)) GameStates.EndFight(); 
            }
        }

        static int _difficulty;
        public TranscendenceAnomaly(int difficulty)
        {
            _difficulty = difficulty;
        }

        public override void Start()
        {
            this.InstanceCreate(new MusicPlayer());
            this.InstanceCreate(new AnomalyGenerater());

            GametimeDelta = 0; //reset it if in need
        }
        private static void IntoChart(int difficulty)
        {
            SongFightingScene.SceneParams @params =
                new(new Transcendence.Game(), null, difficulty, "Content\\Musics\\Transcendence\\song", JudgementState.Strict, GameMode.Practice);
            GameStates.ResetScene(new SongLoadingScene(@params));

            if (PlayerManager.CurrentUser == null) return;
            SaveInfo custom = PlayerManager.CurrentUser.Custom;
            if (!custom.Nexts.ContainsKey("reTranscendence"))
            {
                custom.PushNext(new("reTranscendence{"));
                custom.Nexts["reTranscendence"].PushNext(new("info:" + difficulty));
            }
            else
            {
                int value = custom.Nexts["reTranscendence"].Nexts["info"].IntValue;
                if (difficulty > value)
                {
                    custom.Nexts["reTranscendence"].Nexts["info"][0] = difficulty.ToString();
                }
            }
            PlayerManager.Save();
        }
    }
    public class EndTimeAnomaly : Scene
    {
        private const float SingleBeat = 62.5f / (180f / 60) / 4;
        public static float BeatTime(float x) => x * SingleBeat;
        private class ParticleManager : GameObject
        {
            public bool ForeEnable { get; set; } = false;
            public ParticleManager()
            {
                UpdateIn120 = true;
            }
            int appearTime = 0;
            public override void Update()
            {
                appearTime++;
                // fore particle
                if (appearTime % 2 == 0 && ForeEnable)
                {
                    CreateEntity(new Particle(Color.White * Rand(0.6f, 0.9f) * 0.8f,
                        GetVector2(Rand(6f, 9f) * 0.9f, Rand(-12f, -18f)), Rand(12f, 18f) * 1.4f,
                        new(-12, Rand(0, 640)), Sprites.square)
                    { DarkingSpeed = 1.1f, UpdateIn120 = true, Rotation = -15, AngleMode = true });
                }
                // back particle 
                CreateEntity(new Particle(Color.AliceBlue * Rand(0.6f, 0.9f) * 0.42f,
                    GetVector2(Rand(6f, 9f) * 0.6f, Rand(-12f, -18f) * (-0.6f)), Rand(12f, 18f) * 1.0f,
                    new(-12, Rand(0 - 170, 480)), Sprites.square)
                { DarkingSpeed = 1.4f, UpdateIn120 = true, AutoRotate = true });
            }
        }
        private class ImageEntity : AutoEntity
        {
            public ImageEntity(Texture2D texture)
            {
                this.Centre = new(320, 240);
                UpdateIn120 = true;
                this.AngleMode = true;
                this.Image = texture;
                this.Scale = 2.15f;
            }
            float timer = 999990;
            public override void Update()
            {
                this.Depth = 0.2f;
                this.Alpha = 1;
            }
        }
        private class MusicPlayer : GameObject
        {
            Audio music;
            public MusicPlayer()
            {
                music = new("Musics\\EndTime\\song");
                music.Play();
                UpdateIn120 = true;
            }

            int appearTime = 0;
            public override void Update()
            {
                appearTime++;
            }
            public override void Dispose()
            {
                music.Stop();
                base.Dispose();
            }
        }

        private class DifficultySelector : Entity
        {
            public DifficultySelector()
            {
                UpdateIn120 = true;
                difficulty = 2;
            }
            public override void Draw()
            {
                Font.NormalFont.Draw("Press Z to change difficulty", new(60, 340), Color.White, 1.0f, 0.99f);
                Font.NormalFont.Draw("Current:", new(60, 390), Color.Silver, 1.0f, 0.99f);
                if (isDiv1)
                    Font.NormalFont.Draw("Hard", new(220, 390), Color.MediumPurple, 1.0f, 0.99f);
                else
                    Font.NormalFont.Draw("Noob", new(220, 390), Color.Lime, 1.0f, 0.99f);

            }
            bool isDiv1 = true;
            public override void Update()
            {
                if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                {
                    isDiv1 = !isDiv1;
                    PlaySound(Sounds.Ding);
                }
                if (isDiv1) difficulty = 3;
                else difficulty = 0;
            }
        }

        private static Difficulty Availability
        {
            get
            {
                var user = PlayerManager.CurrentUser;
                if (user == null) return Difficulty.ExtremePlus;

                SaveInfo custom = user.Custom;
                if (!custom.Nexts.ContainsKey("reTranscendence")) return Difficulty.ExtremePlus;

                return (Difficulty)custom.Nexts["reTranscendence"].Nexts["info"].IntValue;
            }
        }

        public class AnomalyGenerater : WaveConstructor
        {
            public AnomalyGenerater() : base(EndTimeAnomaly.SingleBeat)
            {
                AddInstance(new InstantEvent(0, this.StartEvent));
                this.UpdateIn120 = true;
                this.AddChild(new TimeRangedEvent(999999, () =>
                {
                    UpdateEvent();
                })
                { UpdateIn120 = true });
                AddInstance(textureEntity = new ImageEntity(anomalyTexture));
                AddInstance(particleManager = new ParticleManager());
            }
            ImageEntity textureEntity;
            ParticleManager particleManager;

            GlobalResources.Effects.GrayShader gray;
            public void StartEvent()
            {
                // finish by yourself 
                GametimeDelta = -11.5f - BeatTime(16 - 1);
                ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Glitching(0.4f) { Intensity = 2, AverageInterval = 2 });
                ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(gray = Shaders.Gray, 0.5f));
                gray.Intensity = 1f;
            }
            public void UpdateEvent()
            {
                if (InBeat(1f))
                {
                    RegisterFunctionOnce("Move", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                        for (int i = 0; i < 3; i++)
                            builder.Insert(BeatTime(32), ValueEasing.EaseOutCubic(2.15f, 2.0f, BeatTime(32)));

                        builder.Insert(BeatTime(48), ValueEasing.EaseOutCubic(2.15f, 2.0f, BeatTime(48)));

                        builder.Run(s => textureEntity.Scale = s);
                    });
                    RegisterFunctionOnce("Shine", () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White * 0.3f);
                    });
                    RegisterFunctionOnce("Out", () =>
                    {
                        ScreenDrawing.SceneOut(Color.White, BeatTime(22));
                        ScreenDrawing.SceneOutScale = 1.2f;
                        ScreenDrawing.OutFadeScale = 0.98f;
                    });
                    string[] rhythm = {
                        "Shine(Move)", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "Shine", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",

                        "Shine", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "Shine", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "Shine", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "Out", "", "", "", "", "", "", "",
                    };

                    CreateChart(0, BeatTime(8), 0.0f, rhythm);
                }
                if (InBeat(13f + 128))
                {
                    RegisterFunctionOnce("Particle", () =>
                    {
                        particleManager.ForeEnable = true;
                        gray.Intensity = 0.0f;
                    });
                    RegisterFunctionOnce("Create", () =>
                    {
                        AddInstance(new DifficultySelector());
                    });
                    string[] rhythm = {
                        "Particle(Create)", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",

                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                    };

                    CreateChart(0, BeatTime(8), 0.0f, rhythm);
                }
                if (InBeat(13f + 256))
                {
                    RegisterFunctionOnce("End", () =>
                    {
                        IntoChart();
                    });
                    string[] rhythm = {
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",

                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "End",
                    };

                    CreateChart(0, BeatTime(8), 0.0f, rhythm);
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

        private static int difficulty = 2;
        private static Texture2D anomalyTexture;
        public override void Start()
        {
            anomalyTexture = Loader.Load<Texture2D>("Musics\\EndTime\\paint");

            this.InstanceCreate(new MusicPlayer());
            this.InstanceCreate(new AnomalyGenerater());

            GametimeDelta = 0; //reset it if in need
        }
        private static void IntoChart()
        {
            if (PlayerManager.CurrentUser != null)
            {
                SaveInfo custom = PlayerManager.CurrentUser.Custom;
                if (!custom.Nexts.ContainsKey("reEndTime"))
                {
                    custom.PushNext(new("reEndTime{"));
                    custom.Nexts["reEndTime"].PushNext(new("info:" + true));
                }
            }
            SongFightingScene.SceneParams @params =
                new(new SpecialOne.Game(), null, difficulty, "Content\\Musics\\EndTime\\song", JudgementState.Lenient, GameMode.RestartDeny);
            GameStates.ResetScene(new SongLoadingScene(@params));
        }
    }
}