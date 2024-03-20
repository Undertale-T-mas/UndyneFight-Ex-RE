using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.GlobalResources.Effects;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    internal partial class Transcendence
    {
        public Transcendence()
        {
            Game.game = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));
            difficulties = new()
            {
                { "div2", Difficulty.Normal },
                { "div1", Difficulty.Extreme },
                { "div0", Difficulty.ExtremePlus },
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;
        public partial class Game : WaveConstructor, IWaveSet
        {
            #region Entities
            private class Rainer : Entity
            {
                public float Intensity { set; private get; } = 0;
                public float Speed { set; private get; } = 3f;

                public Rainer()
                {
                    Rotation = 10f;
                    UpdateIn120 = true;
                }

                private class WindParticle : Entity
                {
                    private readonly float randVal = 0;
                    private readonly Rainer rainer;
                    private readonly float alpha = 0;
                    private readonly float speed = 0;

                    public WindParticle(Rainer rainer)
                    {
                        UpdateIn120 = true;
                        speed = Rand(0f, 1f) + rainer.Speed;
                        Rotation = Rand(-2f, 2f);
                        alpha = Rand(0.3f, 0.5f);
                        this.rainer = rainer;
                        Centre = new(Rand(-50, 690), -15);
                        randVal = Rand(0f, 1f);
                    }

                    public override void Draw()
                    {
                        if (rainer.Intensity <= randVal) return;
                    }

                    public override void Update()
                    {
                        Vector2 del = GetVector2(speed / 1.5f, rainer.Rotation + Rotation + 90);
                        Centre += del;
                        if (Centre.Y > 499) Dispose();
                    }
                }
                private class RainDrop : Entity
                {
                    private readonly float length;
                    private readonly float randVal = 0;
                    private readonly Rainer rainer;
                    private readonly float alpha = 0;
                    private readonly float speed = 0;

                    public RainDrop(Rainer rainer)
                    {
                        controlLayer = rainer.controlLayer;
                        UpdateIn120 = true;
                        speed = Rand(0f, 1f) + rainer.Speed;
                        Rotation = Rand(-2f, 2f);
                        alpha = Rand(0.3f, 0.5f);
                        this.rainer = rainer;
                        Centre = new(Rand(-50, 690), -15);
                        length = Rand(6, 11) + (rainer.Speed * 1f);
                        randVal = Rand(0f, 1f);
                    }

                    public override void Draw()
                    {
                        if (rainer.Intensity <= randVal) return;
                        Vector2 del = GetVector2(length / 2, rainer.Rotation + Rotation + 90);
                        DrawingLab.DrawLine(Centre + del, Centre - del, 2, Color.LightBlue * alpha, 0.0f);
                    }

                    public override void Update()
                    {
                        Vector2 del = GetVector2(speed / 1.5f, rainer.Rotation + Rotation + 90);
                        Centre += del;
                        if (Centre.Y > 499) Dispose();
                    }
                }

                public override void Draw()
                {
                }

                public override void Update()
                {
                    var drop = new RainDrop(this);
                    AddChild(drop);
                }
            }

            private Rainer rainer;
            private RGBSplitting splitter = new();

            class FakeArrow : AutoEntity
            {
                public FakeArrow(int color, int rotateType)
                {
                    Image = Sprites.arrow[color, rotateType, 0];
                }
                public override void Update()
                {

                }
            }
            #endregion
            StepSampleShader StepSample;
            ScaleShader ShadersScale;
            CameraShader Effect3D;
            GrayShader Gray;
            Blur Blur;
            Shader SinWave;

            RenderProduction cameraProduction, production1, production2, production3, grayProduction;
            Lighting lightProduction;
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 14.0f),
                            new(Difficulty.Extreme, 19.7f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 14.0f),
                            new(Difficulty.Extreme, 19.9f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Normal, 16.5f),
                            new(Difficulty.Extreme, 21.7f),
                        }
                    );
                public override string BarrageAuthor => GameStates.difficulty == 5 ? "TK" : "T-mas";
                public override string AttributeAuthor => "T-mas & Walar & IceAgeDOT" + (GameStates.difficulty == 5 ? " & TK" : "");
                public override string PaintAuthor => "Normist & OtokP";
                public override string SongAuthor => "SK_kent";
                public override string Extra => GameStates.difficulty == 5 ? "This chart is only for display and not for completion" : "";
                public override bool Hidden
                {
                    get
                    {
#if DEBUG
                        return false;
#endif
                        return PlayerManager.CurrentUser == null || !PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("reTranscendence");
                    }
                }
                public override HashSet<Difficulty> UnlockedDifficulties
                {
                    get
                    {
#if DEBUG
                        return base.UnlockedDifficulties;
#endif
                        HashSet<Difficulty> difficulties = new();
                        int v = PlayerManager.CurrentUser.Custom.Nexts["reTranscendence"].Nexts["info"].IntValue;
                        var check = false;
                        check = PlayerManager.currentPlayer != null && PlayerManager.PlayerSkill >= 90;
#if DEBUG
                        check = true;
#endif
                        for (int i = 0; i <= v; i++) difficulties.Add((Difficulty)i);
                        return difficulties;
                    }
                }
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(62.5f / (190f / 60) / 4) { }

            public static Game game;

            public string Music => "Transcendence";
            public string FightName => "Transcendence";
            public void Start()
            {
                #region Shaders
                StepSample = Shaders.StepSample;
                ShadersScale = Shaders.Scale;
                Effect3D = Shaders.Camera;
                Gray = Shaders.Gray;
                Loader.RootDirectory = "Content";

                SinWave = new Shader(Loader.Load<Effect>("Musics\\DustTrust\\shake"));

                SinWave.Parameters["frequency"].SetValue(0f);
                SinWave.Parameters["distance"].SetValue(new Vector2(0f, 0f));
                SinWave.Parameters["range"].SetValue(0f);
                SinWave.Parameters["frequency2"].SetValue(0f);
                SinWave.Parameters["range2"].SetValue(0f);
                SinWave.Parameters["time"].SetValue(0f);
                SinWave.Parameters["time2"].SetValue(0f);

                StepSample.Intensity = 0.01f;
                ShadersScale.Intensity = 0;

                production1 = new Filter(Shaders.StepSample, 0.51f);
                production2 = new Filter(Shaders.Scale, 0.501f);
                production3 = Blur = new Blur(0.505f);
                grayProduction = new Filter(Shaders.Gray, 0.998f);

                Blur.Sigma = 0.0f;

                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(production2);
                ScreenDrawing.SceneRendering.InsertProduction(production3);

                Effect3D.CameraRotation = new(0, 0, 0);

                splitter = new RGBSplitting(0.9f)
                {
                    Disturbance = false,
                    Intensity = 1.0f,
                    RandomDisturb = 0
                };
                ScreenDrawing.SceneRendering.InsertProduction(splitter);
                #endregion

                CreateEntity(rainer = new Rainer());
                rainer.Intensity = 0.18f;
                rainer.Speed = 3f;

                game = this;

                HeartAttribute.BuffedLevel = 3;
                HeartAttribute.MaxHP = 10;
                HeartAttribute.Speed = 3.26f;
                HeartAttribute.SoftFalling = true;

                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.HPExistColor = Color.Silver;

                InstantTP(-200, -200);
                InstantSetBox(new Vector2(180, 560), 84, 84);
                SetSoul(1);
                HeartAttribute.ArrowFixed = true;

                if (GameStates.difficulty == 5)
                {
                    HeartAttribute.BuffedLevel = 0;
                    #region Functions
                    int rand1 = Rand(-1, 1);
                    int rand2 = Rand(1, 3);
                    void Allocate()
                    {
                        ArrowAllocate(1, rand1);
                        ArrowAllocate(2, rand2);
                    }
                    while (rand2 == rand1) { rand2 = Rand(1, 3); }
                    RegisterFunction("Rerand", () =>
                    {
                        rand1 = Rand(-1, 1);
                        rand2 = Rand(1, 3);
                        while (rand2 == rand1)
                        {
                            rand2 = Rand(1, 3);
                        }
                        Allocate();
                    });
                    #endregion
                }

                GametimeDelta = -1.1f;
                bool delayEnable = false;
                if (delayEnable)
                {
                    float delay = BeatTime(1912);
                    PlayOffset = delay;
                    GametimeDelta += delay;
                    InstantSetBox(new Vector2(320, 240), 84, 84);
                    InstantTP(new Vector2(320, 240));

                    ScreenDrawing.BoundColor = Color.Silver;
                    ScreenDrawing.UpBoundDistance = 140;
                    ScreenDrawing.DownBoundDistance = 140;
                }
                else
                {
                    if (GameStates.difficulty == 5)
                    {
                        PlayOffset = 0;
                        ScreenDrawing.UIColor = Color.Transparent;
                        ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                        ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                        NameShower.nameAlpha = 0;
                        RunEase((s) =>
                        {
                            ScreenDrawing.UIColor = Color.Lerp(Color.Transparent, Color.White, s);
                            ScreenDrawing.HPBar.HPLoseColor = Color.Lerp(Color.Transparent, Color.Gray, s);
                            ScreenDrawing.HPBar.HPExistColor = Color.Lerp(Color.Transparent, Color.Silver, s);
                            NameShower.nameAlpha = s;
                        },
                        LinkEase(Stable(BeatTime(116), 0.1f), EaseOut(BeatTime(4), 0, 1, EaseState.Linear)));

                        Lighting lighting;

                        DelayBeat(238, () =>
                        {
                            lighting = new(0.74f);
                            ScreenDrawing.SceneRendering.InsertProduction(lighting);

                            lighting.LightingMode = Lighting.LightMode.Additive;
                            RunEase((s) =>
                            {
                                lighting.AmbientColor = Color.Lerp(Color.Transparent, Color.White, s);
                            },
                            EaseIn(BeatTime(6), 0.7f, EaseState.Quad),
                            EaseOut(BeatTime(3), 0.7f, 1.0f, EaseState.Quad),
                            EaseOut(BeatTime(3), 1.0f, 0.0f, EaseState.Linear));

                            Lighting.Light lightA = new() { position = new(320, 240), color = Color.Transparent, size = 80 };
                            lighting.Lights.Add(lightA);

                            lightA.scale = new(1.15f, 1.0f);
                            RunEase((s) =>
                            {
                                lightA.size = MathHelper.Lerp(100, 670, s);
                                lightA.color = Color.Lerp(Color.Transparent, Color.White, s);
                            },
                            EaseIn(BeatTime(6), 0.7f, EaseState.Quad),
                            EaseOut(BeatTime(3), 0.7f, 1.0f, EaseState.Quad),
                            EaseOut(BeatTime(2), 1.0f, 0.0f, EaseState.Linear));
                            DelayBeat(12, () =>
                            {
                                lighting.Dispose();
                            });
                        });
                        #region Inital Line
                        Line line1 = new(new Vector2(320, -100), new Vector2(320, 580))
                        {
                            Width = 10
                        };
                        DelayBeat(248, () => { line1.Dispose(); });
                        RunEase((s) => { line1.Alpha = s; },
                        LinkEase(Stable(1, BeatTime(248)), EaseOut(BeatTime(2), 1, 0, EaseState.Linear)));
                        CreateEntity(line1);
                        #endregion
                        #region Inital Souls
                        Player.Heart Heart1 = Heart, Heart2 = CreateHeart(-40, 84, 84);
                        //Right soul
                        SetPlayerBoxMission(Heart1);
                        SetSoul(1);
                        DelayBeat(1, () =>
                        {
                            Heart1.Shields.RemoveShield(Heart1.Shields.RShield);
                        });
                        RunEase((s) =>
                        {
                            //R
                            SetPlayerBoxMission(Heart1);
                            InstantTP(s, 240);
                            InstantSetBox(new Vector2(s, 240), 84, 84);
                        },
                        LinkEase(EaseOut(BeatTime(24), 686, 520, EaseState.Quad)));
                        //Left soul
                        SetPlayerBoxMission(Heart2);
                        SetSoul(1);
                        DelayBeat(1, () =>
                        {
                            Heart2.Shields.RemoveShield(Heart2.Shields.BShield);
                        });
                        RunEase((s) =>
                        {
                            //L
                            SetPlayerBoxMission(Heart2);
                            InstantTP(s, 240);
                            InstantSetBox(new Vector2(s, 240), 84, 84);
                        },
                        LinkEase(
                            Stable(BeatTime(30), -46),
                            EaseOut(BeatTime(24), -46, 120, EaseState.Quad)));
                        DelayBeat(56, () =>
                            RunEase((s) =>
                            {
                                //R
                                SetPlayerBoxMission(Heart1);
                                InstantTP(s, 240);
                                InstantSetBox(new Vector2(s, 240), 84, 84);
                                //L
                                SetPlayerBoxMission(Heart2);
                                InstantTP(640 - s, 240);
                                InstantSetBox(new Vector2(640 - s, 240), 84, 84);
                            },
                            LinkEase(
                                EaseOut(BeatTime(24), 520, 400, EaseState.Quad),
                                Stable(BeatTime(14), 40),
                                EaseOut(BeatTime(22), 400, 520, EaseState.Quad),
                                Stable(BeatTime(4), 520),
                                EaseOut(BeatTime(125), 520, 370, EaseState.Quad)
                            ))
                        );
                        DelayBeat(245.1f, () =>
                        {
                            Heart2.Merge(Heart1);
                            SetPlayerBoxMission(Heart1);
                            Heart1.Shields.AddShield(Heart1.Shields.RShield);
                            SetSoul(1);
                            InstantSetBox(new Vector2(320, 240), 84, 84);
                            InstantTP(320, 240);
                        });
                        #endregion
                        DelayBeat(120, () =>
                            RunEase((s) =>
                            {
                                splitter.Intensity = s;
                                SinWave.Parameters["frequency"].SetValue(s * 3);
                                SinWave.Parameters["range"].SetValue(s * 3);
                                SinWave.Parameters["distance"].SetValue(s * 10);
                            }, EaseOut(BeatTime(131), 1, 5, EaseState.Quad)));
                    }
                }


                /* useful line
                LinePlate p = new(128)
                {
                    Radius = 555,
                    Centre = new(320, 130)
                };
                AddInstance(p);
                p.Factor = 44;
                p.Alpha = 0.2f;
                for (int i = 2; i < 127; i++)
                {
                    int t = i;
                    DelayBeat(i * 16, () => p.Factor = t);
                }
                */
            }
        }
    }
}