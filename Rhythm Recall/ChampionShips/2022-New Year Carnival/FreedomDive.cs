
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

namespace Rhythm_Recall.Waves
{
    public class FreedomDive : IChampionShip
    {
        public FreedomDive()
        {
            Game.instance = new Game();
            divisionImformation = new SaveInfo("imf{");
            divisionImformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new();
            difficulties.Add("div.3", Difficulty.Noob);
            difficulties.Add("div.2", Difficulty.Hard);
            difficulties.Add("div.1", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionImformation => divisionImformation;
        public SaveInfo divisionImformation;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            private class KickCounter : Entity
            {
                public override void Draw()
                {
                    FightResources.Font.NormalFont.CentreDraw((count + 1) + "", new Microsoft.Xna.Framework.Vector2(320, 80), Color.White, GameStates.SpriteBatch);
                    if (time > 0)
                    {
                        FightResources.Font.NormalFont.CentreDraw("Time = " + (count * 1.0f / time), new Microsoft.Xna.Framework.Vector2(320, 120), Color.White, GameStates.SpriteBatch);
                        FightResources.Font.NormalFont.CentreDraw("Frame = " + 60 * (count * 1.0f / time), new Microsoft.Xna.Framework.Vector2(320, 160), Color.White, GameStates.SpriteBatch);
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

            public Game() : base(16.8725f / 4f) { }

            private class ThisImformation : SongImformation
            {
                public override string BarrageAuthor => "T-mas";
                public override string AttributeAuthor => "T-mas";
                public override string SongAuthor => "xi";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 5.0f),
                            new(Difficulty.Hard, 16.0f),
                            new(Difficulty.ExtremePlus, 20.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 6.0f),
                            new(Difficulty.Hard, 17.0f),
                            new(Difficulty.ExtremePlus, 20.7f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 13.5f),
                            new(Difficulty.Hard, 19.5f),
                            new(Difficulty.ExtremePlus, 21.4f),
                        }
                    );
            }
            public SongImformation Attributes => new ThisImformation();

            public static Game instance;

            public string Music => "Freedom Dive";

            public string FightName => "Freedom Dive";

            #region Non-ChampionShip
            public void Easy()
            {
                throw new System.NotImplementedException();
            }
            public void Normal()
            {
                throw new System.NotImplementedException();
            }
            public void Extreme()
            {
                throw new System.NotImplementedException();
            }
            #endregion

            private class SpecialThanks : Entity
            {
                private class SkipPress : Entity
                {
                    public SkipPress() { UpdateIn120 = true; }

                    private int appearTime = 0;
                    public override void Draw()
                    {
                        if (appearTime % 120 < 60 || appearTime > 300) Font.NormalFont.CentreDraw("Press SpaceBar to skip.", new(320, 435), Color.Gold, 0.6f, 0.5f);
                    }

                    public override void Update()
                    {
                        appearTime++;
                        if (GameStates.IsKeyPressed120f(InputIdentity.Alternate)) EndSong();
                    }
                }
                public SpecialThanks()
                {
                    AddChild(new SkipPress());

                    ScreenDrawing.WhiteOut(50);
                    AddInstance(new InstantEvent(50, () =>
                    {
                        ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                        ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                        ScreenDrawing.UIColor = Color.Transparent;

                        ScreenDrawing.BackGroundColor = Color.DarkBlue;
                        ScreenDrawing.BoundColor = Color.Silver;

                        AddInstance(new TimeRangedEvent(0, 10000, () =>
                        {
                            ScreenDrawing.DownBoundDistance = ScreenDrawing.UpBoundDistance = ScreenDrawing.LeftBoundDistance = ScreenDrawing.RightBoundDistance = 0.02f + 0.01f * Sin(GametimeF);
                        }));
                        ScreenDrawing.Shaders.Filter p1, p2;
                        ScreenDrawing.SceneRendering.InsertProduction(p1 = new ScreenDrawing.Shaders.Filter(Shaders.NeonLine, 0.55f));
                        (p1.CurrentShader as GlobalResources.Effects.NeonLineShader).DrawingColor = Color.White * 0.2f;
                        (p1.CurrentShader as GlobalResources.Effects.NeonLineShader).Speed = 0.4f;
                        ScreenDrawing.SceneRendering.InsertProduction(p2 = new ScreenDrawing.Shaders.Filter(Shaders.Cos1Ball, 0.6f));
                        var v = (p2.CurrentShader as GlobalResources.Effects.BallShapingShader);
                        v.Intensity = 0.5f;
                        v.ScreenScale = 1.1f;

                        InstantSetBox(-100, 24, 24);

                        ScreenDrawing.ScreenPositionDetla = new(0, -4);
                    }));
                    AddInstance(new InstantEvent(1200, () =>
                    {
                        for (int i = 0; i < givenTexts.Length; i++) AddInstance(givenTexts[i]);
                    }));
                    for (int i = 0; i < authorPresent.Length; i++) AddInstance(authorPresent[i]);
                }

                public override void Draw()
                { }

                private readonly InstantEvent[] authorPresent = {
                    new InstantEvent(100, ()=>{
                        CreateEntity(new TextPrinter("$$Undertale Rhythm Recall", new Vector2(40, 50), new TextFadeoutAttribute(600, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(150, ()=>{
                        CreateEntity(new TextPrinter("$$Engine by T-mas", new Vector2(40, 110), new TextFadeoutAttribute(550, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(200, ()=>{
                        CreateEntity(new TextPrinter("$$Arts by OtokP", new Vector2(40, 170), new TextFadeoutAttribute(500, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(260, ()=>{
                        CreateEntity(new TextPrinter("$$Barrage by T-mas", new Vector2(40, 230), new TextFadeoutAttribute(450, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(300, ()=>{
                        CreateEntity(new TextPrinter("$$Graphics Rendering by T-mas & Walar", new Vector2(40, 290), new TextFadeoutAttribute(400, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(350, ()=>{
                        CreateEntity(new TextPrinter("$$Tests by Taster & OtokP", new Vector2(40, 350), new TextFadeoutAttribute(350, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(900, ()=>{
                        CreateEntity(new TextPrinter("$$Now, We are going to present\nthe Editor's words:", new Vector2(40, 240), new TextFadeoutAttribute(300, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(5500, ()=>{
                        CreateEntity(new TextPrinter("$$Undertale - Toby Fox copyright[2015]", new Vector2(40, 220), new TextFadeoutAttribute(700, 50), new TextSpeedAttribute(30)));
                        CreateEntity(new TextPrinter("$$$Rhythm Recall RPG is coming in 2023", new Vector2(40, 320), new TextFadeoutAttribute(1400, 150), new TextSpeedAttribute(30), new TextMoveAttribute(
                            (x) => {
                                if(x <= 800) return Vector2.Zero;
                                else if(x <= 900) return Vector2.Lerp(Vector2.Zero, new(0, -100), (x - 800) / 100);
                                return new(0, -100);
                            })));
                    }),
                };
                private readonly InstantEvent[] givenTexts = {
                    new InstantEvent(100, ()=>{
                        CreateEntity(new TextPrinter("$$I'm so glad that you can play\nRhythm Recall for so long.", new Vector2(40, 50), new TextFadeoutAttribute(600, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(200, ()=>{
                        CreateEntity(new TextPrinter("$$You are so determined that\nyou have beated the hardest level.", new Vector2(40, 150), new TextFadeoutAttribute(500, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(350, ()=>{
                        CreateEntity(new TextPrinter("$$But now rest for a while, and try to\nrecall your deepest memory.", new Vector2(40, 250), new TextFadeoutAttribute(350, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(800, ()=>{
                        CreateEntity(new TextPrinter("$$The memory of the original intention\nto play undertale fan games.", new Vector2(40, 50), new TextFadeoutAttribute(1000, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(1000, ()=>{
                        CreateEntity(new TextPrinter("$$The memory of days and nights\nwhen you tried to beat so much \ndifficult games.", new Vector2(40, 150), new TextFadeoutAttribute(800, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(1200, ()=>{
                        CreateEntity(new TextPrinter("$$The memory of excitement from your \nheart at the time you achieve \nanother success.", new Vector2(40, 290), new TextFadeoutAttribute(600, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(2000, ()=>{
                        CreateEntity(new TextPrinter("$$Looking back on the past bit by bit\nthey had added a lot of luster\nto your journey of life", new Vector2(40, 50), new TextFadeoutAttribute(1000, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(2200, ()=>{
                        CreateEntity(new TextPrinter("$$Every time you win, it's just \nzero and one in the computer.", new Vector2(40, 190), new TextFadeoutAttribute(800, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(2400, ()=>{
                        CreateEntity(new TextPrinter("$$And yet you play the game.\nsince this isn't zero and one \nin your heart", new Vector2(40, 290), new TextFadeoutAttribute(600, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(3000, ()=>{
                        CreateEntity(new TextPrinter("$$Once an impeccable rating\nthe essence of your soul \nis given", new Vector2(40, 50), new TextFadeoutAttribute(800, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(3200, ()=>{
                        CreateEntity(new TextPrinter("$$You enjoy the boring things\nin the eyes of others", new Vector2(40, 190), new TextFadeoutAttribute(600, 50), new TextSpeedAttribute(30)));
                    }),
                    new InstantEvent(3400, ()=>{
                        CreateEntity(new TextPrinter("$$Because they are unique to you.\nWithout them, you may lose \nyour $determination", new Vector2(40, 290), new TextFadeoutAttribute(400, 50), new TextSpeedAttribute(30), new TextColorAttribute(Color.Red)));
                    }),
                    new InstantEvent(3840, ()=>{
                        CreateEntity(new TextPrinter("$$Stay determined!\nThe future is boundless\nand always remain enthusiastic\nabout more challenges", new Vector2(40, 50), new TextFadeoutAttribute(400, 50), new TextSpeedAttribute(30)));
                    }),
                };

                public override void Update()
                {

                }
            }

            private static class NoobBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "+0", "/",
                            "/", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "+0", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(8);
                    string[] bway = {
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(8);
                    string[] bway = {
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro3()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro4()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro5()
                {
                    float curTime = game.BeatTime(8);
                    string[] bway = {
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                            "R", "/", "+0", "/", "+0", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void IntroRotate0()
                {
                }

                public static void Turning1A()
                {
                    float speedY = -3.4f, speedX = Rand(-20, 20) / 26f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(0), game.BeatTime(32), () =>
                    {
                        speedY += 0.12f + speedY / 100f;
                        InstantSetBox(BoxStates.Left + speedX, BoxStates.Right + speedX, BoxStates.Up + speedY, BoxStates.Down + speedY);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(0), game.BeatTime(56), () =>
                    {
                        InstantTP(BoxStates.Centre);
                    }));

                    AddInstance(new InstantEvent(game.BeatTime(32 + 0.1f), () =>
                    {
                        BoxStates.BoxMovingScale = 0.07f;
                        speedY = 0f;
                        InstantSetBox(-500, 75, 75);
                        SetGreenBox();
                    }));

                    ScreenDrawing.BoundColor = Color.DarkBlue * 0.9f;
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(118), () =>
                    {
                        if (Gametime % 15 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(0, 240), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(400, 640), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                    }
                    ));
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(128), () =>
                    {
                        ScreenDrawing.DownBoundDistance = ScreenDrawing.DownBoundDistance * 0.992f + 1800f * 0.008f;
                        ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, Color.LightBlue, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(64), game.BeatTime(64), () =>
                    {
                        if (Gametime % 5 == 0)
                            ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.LightBlue * 0.38f, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(61), game.BeatTime(2560), () =>
                    {
                        float cycleTick = Gametime / game.BeatTime(16);
                        speedY = speedY * 0.99f + 16 * 0.01f;
                        Func<float, float> sin01 = UndyneFight_Ex.Fight.AdvanceFunctions.Sin01;
                        InstantSetBox(new Vector2(320, 240) + new Vector2(sin01(cycleTick / 2.72f) * 15, sin01(cycleTick) * speedY), 84, 84);
                        InstantTP(BoxStates.Centre);
                        CreateEntity(new Particle(Color.Lerp(Color.Aqua, Color.White, MathF.Pow(Rand(0, 90) / 100f, 2)) * MathF.Pow((Rand(2, 14) / 20f), 1.5f),
                                     new(Rand(-10, 10) / 10f, -7.4f + Rand(-15, 15) / 10f), Rand(10, 16), new(Rand(-100, 740), 495), Sprites.square)
                        {
                            DarkingSpeed = 1.7f,
                            AutoRotate = true
                        });
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () => ScreenDrawing.SceneOut(new(0, 0, 128), game.BeatTime(4))));
                    AddInstance(new InstantEvent(game.BeatTime(64), () => ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.RGBSplitting())));
                }

                public static void Turning1B()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1C()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1D()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1E()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(16), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(true);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1F()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                    }));
                    float curTime = game.BeatTime(8);
                    string[] bway = {
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Area1A()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < bway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], i % 8 == 4 ? 8.0f : 4.5f, 0, 0);
                            curTime += game.BeatTime(1);
                        }
                }
                public static void Area1B()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                }
                public static void Area1B2()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$2", "/", "+0", "/", "+1", "/", "+0", "/",
                            "+1", "/", "+0", "/", "+1", "/", "+0", "/",
                            "+1", "/", "+0", "/", "+1", "/", "+0", "/",
                            "+1", "/", "+0", "/", "+1", "/", "+0", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 9.0f, 0, 0, ((i / 2) % 4) switch
                        {
                            0 => ArrowAttribute.None,
                            1 => ArrowAttribute.RotateL,
                            2 => ArrowAttribute.RotateR,
                            3 => ArrowAttribute.SpeedUp,
                            _ => throw new Exception()
                        });
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2A()
                {
                    float curTime = game.BeatTime(32);
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 7.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 7.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 7.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "$0", "/", "+1", "/", "+1", "/", "+1", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "R", "/", "R", "R", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "R", "/", "/", "R", "/", "R", "/",
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                            "+0", "/", "/", "/", "+0", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Turning2A()
                {
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(118), () =>
                    {
                        if (Gametime % 24 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(0, 240), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(400, 640), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                        if (Gametime % 48 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(-5.0f + Rand(-18, 18) / 10f, Rand(-10, 10) / 50f), 24f, new(655, Rand(0, 120)), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(5.0f + Rand(-18, 18) / 10f, Rand(-10, 10) / 50f), 24f, new(-15, Rand(400, 480)), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                    }
                    ));

                    float curTime = game.BeatTime(64);
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < bway.Length; i++)
                        {
                            if (bway[i] == "T")
                            {
                                CreateArrow(curTime, 2, 10.0f, 0, 0);
                                CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                                CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                            }
                            else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                            curTime += game.BeatTime(1);
                        }

                    Action lrot = () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    };
                    Action rrot = () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(true);
                    };

                    AddInstance(new InstantEvent(game.BeatTime(128), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 32), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 64), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 80), rrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 88), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 96), rrot));
                }
                public static void Turning2B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",

                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning2C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "/", "/", "/", "/", "$2", "+0", "+0", "+0",
                            "+2", "+0", "+0", "+0", "+2", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Final3A()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway0 = {
                            "$2", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                            "+2", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                            "+2", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                            "+2", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                            "+2", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                            "+2", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                            "+2", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                            "+2", "+0", "+0", "+0", "+2", "+0", "+0", "+0",
                        };
                    for (int i = 0; i < bway0.Length; i++)
                    {
                        if (bway0[i] != "/") CreateArrow(curTime, bway0[i], 8, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
            }
            private static class HardBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro3()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro4()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro5()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "+0", "/", "/", "/",
                            "/", "/", "/", "/", "+0", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void IntroRotate0()
                {
                    float curTime = game.BeatTime(8);
                    string[] bway = {
                            "R", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Turning1A()
                {
                    float speedY = -3.4f, speedX = Rand(-20, 20) / 26f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(0), game.BeatTime(32), () =>
                    {
                        speedY += 0.12f + speedY / 100f;
                        InstantSetBox(BoxStates.Left + speedX, BoxStates.Right + speedX, BoxStates.Up + speedY, BoxStates.Down + speedY);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(0), game.BeatTime(56), () =>
                    {
                        InstantTP(BoxStates.Centre);
                    }));

                    AddInstance(new InstantEvent(game.BeatTime(32 + 0.1f), () =>
                    {
                        BoxStates.BoxMovingScale = 0.07f;
                        speedY = 0f;
                        InstantSetBox(-500, 75, 75);
                        SetGreenBox();
                    }));

                    ScreenDrawing.BoundColor = Color.DarkBlue * 0.9f;
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(118), () =>
                    {
                        if (Gametime % 15 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(0, 240), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(400, 640), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                    }
                    ));
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(128), () =>
                    {
                        ScreenDrawing.DownBoundDistance = ScreenDrawing.DownBoundDistance * 0.992f + 1800f * 0.008f;
                        ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, Color.LightBlue, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(64), game.BeatTime(64), () =>
                    {
                        if (Gametime % 5 == 0)
                            ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.LightBlue * 0.38f, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(61), game.BeatTime(2560), () =>
                    {
                        float cycleTick = Gametime / game.BeatTime(16);
                        speedY = speedY * 0.99f + 16 * 0.01f;
                        Func<float, float> sin01 = UndyneFight_Ex.Fight.AdvanceFunctions.Sin01;
                        InstantSetBox(new Vector2(320, 240) + new Vector2(sin01(cycleTick / 2.72f) * 15, sin01(cycleTick) * speedY), 84, 84);
                        InstantTP(BoxStates.Centre);
                        CreateEntity(new Particle(Color.Lerp(Color.Aqua, Color.White, MathF.Pow(Rand(0, 90) / 100f, 2)) * MathF.Pow((Rand(2, 14) / 20f), 1.5f),
                                     new(Rand(-10, 10) / 10f, -7.4f + Rand(-15, 15) / 10f), Rand(10, 16), new(Rand(-100, 740), 495), Sprites.square)
                        {
                            DarkingSpeed = 1.7f,
                            AutoRotate = true
                        });
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () => ScreenDrawing.SceneOut(new(0, 0, 128), game.BeatTime(4))));
                    AddInstance(new InstantEvent(game.BeatTime(64), () => ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.RGBSplitting())));
                }

                public static void Turning1B()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1C()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1D()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "R", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1E()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));

                    AddInstance(new InstantEvent(game.BeatTime(16), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(true);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1F()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(8);
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Area1A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 8.0f, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                            "R", "/", "+0", "/", "+0", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < bway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 5.0f, 0, 0);
                            curTime += game.BeatTime(1);
                        }
                }
                public static void Area1B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] rway = {
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(32);
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                            curTime += game.BeatTime(1);
                        }
                }
                public static void Area1B2()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                        };
                    string[] rway = {
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$2", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 9.0f, 1, 0, ((i / 2) % 4) switch
                        {
                            0 => ArrowAttribute.None,
                            1 => ArrowAttribute.RotateL,
                            2 => ArrowAttribute.RotateR,
                            3 => ArrowAttribute.SpeedUp,
                            _ => throw new Exception()
                        });
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "T", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 7.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 7.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 7.0f, 1, 0);
                        }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "T", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "T", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 7.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 7.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 7.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2B()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 1, 0);
                        }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2C()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "/", "$2", "/", "+0", "/", "+0", "/", "+0",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 1, 0);
                        }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "$0", "/", "+0", "/", "+0", "/", "+0", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "R", "/", "R", "R", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "R", "/", "/", "R", "/", "R", "/",
                            "R", "/", "/", "/", "+0", "/", "/", "/",
                            "+0", "/", "/", "/", "+0", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Turning2A()
                {
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(118), () =>
                    {
                        if (Gametime % 24 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(0, 240), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(400, 640), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                        if (Gametime % 48 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(-5.0f + Rand(-18, 18) / 10f, Rand(-10, 10) / 50f), 24f, new(655, Rand(0, 120)), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(5.0f + Rand(-18, 18) / 10f, Rand(-10, 10) / 50f), 24f, new(-15, Rand(400, 480)), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                    }
                    ));

                    float curTime = game.BeatTime(64);
                    string[] rway = {
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] == "T")
                            {
                                CreateArrow(curTime, 2, 10.0f, 1, 0);
                                CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 1, 0);
                                CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 1, 0);
                            }
                            else if (rway[i] != "/") CreateArrow(curTime, rway[i], 6, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(64);
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < bway.Length; i++)
                        {
                            if (bway[i] == "T")
                            {
                                CreateArrow(curTime, 2, 10.0f, 0, 0);
                                CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                                CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                            }
                            else if (bway[i] != "/") CreateArrow(curTime, bway[i], 6, 0, 0);
                            curTime += game.BeatTime(1);
                        }

                    Action lrot = () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    };
                    Action rrot = () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(true);
                    };

                    AddInstance(new InstantEvent(game.BeatTime(128), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 32), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 64), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 80), rrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 88), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 96), rrot));
                }
                public static void Turning2B()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",

                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",
                            "R", "/", "/", "/", "/", "/", "+0", "/",

                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 6.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 6.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning2C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$0", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, i % 2);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Final3A()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway0 = {
                            "$2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                        };
                    for (int i = 0; i < bway0.Length; i++)
                    {
                        if (bway0[i] != "/") CreateArrow(curTime, bway0[i], 8, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
            }
            public void Noob()
            {
                if (InBeat(0 - 8)) NoobBarrage.Intro0();
                if (InBeat(32 - 8)) NoobBarrage.Intro0();
                if (InBeat(64 - 8)) NoobBarrage.Intro0();
                if (InBeat(96 - 8)) NoobBarrage.Intro1();
                if (InBeat(128 - 8)) NoobBarrage.Intro0();
                if (InBeat(160 - 8)) NoobBarrage.Intro0();
                if (InBeat(192 - 8)) NoobBarrage.Intro0();
                if (InBeat(224 - 8)) NoobBarrage.Intro2();
                for (int i = 0; i < 3; i++) if (InBeat(i * 32 + 256 - 8)) NoobBarrage.IntroRotate0();
                if (InBeat(256 + 32 - 8)) NoobBarrage.Intro3();
                if (InBeat(256 + 64 - 8)) NoobBarrage.Intro4();
                if (InBeat(256 + 96 - 8)) NoobBarrage.Intro5();
                if (InBeat(256 + 128)) NoobBarrage.Turning1A();

                if (InBeat(512 - 8)) NoobBarrage.Turning1B();
                if (InBeat(512 + 32 - 8)) NoobBarrage.Turning1C();
                if (InBeat(512 + 64 - 8)) NoobBarrage.Turning1D();
                if (InBeat(512 + 96 - 8)) NoobBarrage.Turning1E();
                if (InBeat(512 + 128 - 8)) NoobBarrage.Turning1F();

                if (InBeat(640 + 0)) NoobBarrage.Area1A();
                if (InBeat(640 + 64)) NoobBarrage.Area1B();
                if (InBeat(640 + 128)) NoobBarrage.Area1A();
                if (InBeat(640 + 192)) NoobBarrage.Area1B2();
                if (InBeat(640 + 224)) NoobBarrage.Area1C();

                if (InBeat(896)) NoobBarrage.Area2A();
                if (InBeat(896 + 64)) NoobBarrage.Area2B();
                if (InBeat(896 + 128)) NoobBarrage.Area2C();

                if (InBeat(1184)) NoobBarrage.Turning2A();
                if (InBeat(1184 + 192 - 32)) NoobBarrage.Turning2B();
                if (InBeat(1518)) NoobBarrage.Turning2C();

                if (InBeat(1536)) NoobBarrage.Area2A();
                if (InBeat(1536 + 64)) NoobBarrage.Area2B();
                if (InBeat(1536 + 128)) NoobBarrage.Area2C();
                if (InBeat(1792)) NoobBarrage.Area2A();
                if (InBeat(1792 + 64)) NoobBarrage.Area2B();
                if (InBeat(1792 + 128)) NoobBarrage.Area2C();
                if (InBeat(1792 + 256)) NoobBarrage.Area1C();
                if (InBeat(1792 + 256 + 32)) NoobBarrage.Area1C();
                if (InBeat(2112)) NoobBarrage.Final3A();
            }
            public void Hard()
            {
                if (InBeat(0 - 8)) HardBarrage.Intro0();
                if (InBeat(32 - 8)) HardBarrage.Intro0();
                if (InBeat(64 - 8)) HardBarrage.Intro0();
                if (InBeat(96 - 8)) HardBarrage.Intro1();
                if (InBeat(128 - 8)) HardBarrage.Intro0();
                if (InBeat(160 - 8)) HardBarrage.Intro0();
                if (InBeat(192 - 8)) HardBarrage.Intro0();
                if (InBeat(224 - 8)) HardBarrage.Intro2();
                for (int i = 0; i < 3; i++) if (InBeat(i * 32 + 256 - 8)) HardBarrage.IntroRotate0();
                if (InBeat(256 + 32 - 8)) HardBarrage.Intro3();
                if (InBeat(256 + 64 - 8)) HardBarrage.Intro4();
                if (InBeat(256 + 96 - 8)) HardBarrage.Intro5();
                if (InBeat(256 + 128)) HardBarrage.Turning1A();

                if (InBeat(512 - 8)) HardBarrage.Turning1B();
                if (InBeat(512 + 32 - 8)) HardBarrage.Turning1C();
                if (InBeat(512 + 64 - 8)) HardBarrage.Turning1D();
                if (InBeat(512 + 96 - 8)) HardBarrage.Turning1E();
                if (InBeat(512 + 128 - 8)) HardBarrage.Turning1F();

                if (InBeat(640 + 0)) HardBarrage.Area1A();
                if (InBeat(640 + 64)) HardBarrage.Area1B();
                if (InBeat(640 + 128)) HardBarrage.Area1A();
                if (InBeat(640 + 192)) HardBarrage.Area1B2();
                if (InBeat(640 + 224)) HardBarrage.Area1C();

                if (InBeat(896)) HardBarrage.Area2A();
                if (InBeat(896 + 64)) HardBarrage.Area2B();
                if (InBeat(896 + 128)) HardBarrage.Area2C();

                if (InBeat(1184)) HardBarrage.Turning2A();
                if (InBeat(1184 + 192 - 32)) HardBarrage.Turning2B();
                if (InBeat(1518)) HardBarrage.Turning2C();

                if (InBeat(1536)) HardBarrage.Area2A();
                if (InBeat(1536 + 64)) HardBarrage.Area2B();
                if (InBeat(1536 + 128)) HardBarrage.Area2C();
                if (InBeat(1792)) HardBarrage.Area2A();
                if (InBeat(1792 + 64)) HardBarrage.Area2B();
                if (InBeat(1792 + 128)) HardBarrage.Area2C();
                if (InBeat(1792 + 256)) HardBarrage.Area1C();
                if (InBeat(1792 + 256 + 32)) HardBarrage.Area1C();
                if (InBeat(2112)) HardBarrage.Final3A();
            }

            private static class ExBarrage
            {
                public static Game game;
                public static void Intro0()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro1()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "/", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "R", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro2()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro3()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro4()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Intro5()
                {
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void IntroRotate0()
                {
                    float curTime = game.BeatTime(8);
                    string[] bway = {
                            "R", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                            "+1", "/", "/", "/", "+1", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, i == 9 ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Turning1A()
                {
                    float speedY = -3.4f, speedX = Rand(-20, 20) / 26f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(0), game.BeatTime(32), () =>
                    {
                        speedY += 0.12f + speedY / 100f;
                        InstantSetBox(BoxStates.Left + speedX, BoxStates.Right + speedX, BoxStates.Up + speedY, BoxStates.Down + speedY);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(0), game.BeatTime(56), () =>
                    {
                        InstantTP(BoxStates.Centre);
                    }));

                    AddInstance(new InstantEvent(game.BeatTime(32 + 0.1f), () =>
                    {
                        BoxStates.BoxMovingScale = 0.07f;
                        speedY = 0f;
                        InstantSetBox(-500, 75, 75);
                        SetGreenBox();
                    }));

                    ScreenDrawing.BoundColor = Color.DarkBlue * 0.9f;
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(118), () =>
                    {
                        if (Gametime % 15 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(0, 240), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(400, 640), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                    }
                    ));
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(128), () =>
                    {
                        ScreenDrawing.DownBoundDistance = ScreenDrawing.DownBoundDistance * 0.992f + 1800f * 0.008f;
                        ScreenDrawing.UIColor = Color.Lerp(ScreenDrawing.UIColor, Color.LightBlue, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(64), game.BeatTime(64), () =>
                    {
                        if (Gametime % 5 == 0)
                            ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.LightBlue * 0.38f, 0.02f);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(61), game.BeatTime(2112 - 384), () =>
                    {
                        float cycleTick = Gametime / game.BeatTime(16);
                        speedY = speedY * 0.99f + 16 * 0.01f;
                        Func<float, float> sin01 = UndyneFight_Ex.Fight.AdvanceFunctions.Sin01;
                        InstantSetBox(new Vector2(320, 240) + new Vector2(sin01(cycleTick / 2.72f) * 15, sin01(cycleTick) * speedY), 84, 84);
                    }));
                    AddInstance(new TimeRangedEvent(game.BeatTime(61), game.BeatTime(2560), () =>
                    {
                        InstantTP(BoxStates.Centre);
                        CreateEntity(new Particle(Color.Lerp(Color.Aqua, Color.White, MathF.Pow(Rand(0, 90) / 100f, 2)) * MathF.Pow((Rand(2, 14) / 20f), 1.5f),
                                     new(Rand(-10, 10) / 10f, -7.4f + Rand(-15, 15) / 10f), Rand(10, 16), new(Rand(-100, 740), 495), Sprites.square)
                        {
                            DarkingSpeed = 1.7f,
                            AutoRotate = true
                        });
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(60), () => ScreenDrawing.SceneOut(new(0, 0, 128), game.BeatTime(4))));
                    AddInstance(new InstantEvent(game.BeatTime(64), () => ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.RGBSplitting())));
                }

                public static void Turning1B()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "R", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1C()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1D()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(24), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "R", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1E()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(16), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(true);
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning1F()
                {
                    AddInstance(new InstantEvent(game.BeatTime(8), () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                    }));
                    float curTime = game.BeatTime(8);
                    string[] rway = {
                            "R", "/", "/", "/", "R", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "R", "/",
                        };
                    string[] bway = {
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                            "R", "/", "R", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Area1A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                            "/", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 10.0f, 1, 0, ArrowAttribute.SpeedUp);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "+0", "/", "+2", "/", "+0", "/",
                            "R", "/", "+0", "/", "+2", "/", "+0", "/",
                            "R", "/", "+0", "/", "+2", "/", "+0", "/",
                            "R", "/", "+0", "/", "+2", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < bway.Length; i++)
                        {
                            bool isYellow = i % 8 >= 4;
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, isYellow ? 1 : 0);
                            curTime += game.BeatTime(1);
                        }
                }
                public static void Area1B()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    string[] rway = {
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, i % 4 == 2 ? 1 : 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(32);
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                            curTime += game.BeatTime(1);
                        }
                }
                public static void Area1B2()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    string[] rway = {
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                            "R", "/", "+0", "/", "R", "/", "+0", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, i % 4 == 2 ? 1 : 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area1C()
                {
                    float curTime = game.BeatTime(32);
                    string[] bway = {
                            "$2", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                        };
                    string[] rway = {
                            "$0", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                            "+1", "/", "+1", "/", "+1", "/", "+1", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 9.0f, 1, 0, ((i / 2) % 4) switch
                        {
                            0 => ArrowAttribute.None,
                            1 => ArrowAttribute.RotateL,
                            2 => ArrowAttribute.RotateR,
                            3 => ArrowAttribute.SpeedUp,
                            _ => throw new Exception()
                        });
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i] + 2, 9.0f, 0, 1);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2A()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "T", "/", "/", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 1, 0);
                        }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 7, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "T", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "T", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "R", "/", "R", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 7, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2B()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "R", "/", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "R", "/",
                            "R", "/", "R", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 1, 0);
                        }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 7, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 7, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Area2C()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "/", "R", "/", "+1", "/", "+1", "/", "+1",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "/", "/", "R", "/", "/", "/", "R", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "/", "/", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 1, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 1, 0);
                        }
                        else if (rway[i] != "/") CreateArrow(curTime, rway[i], 7, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "+1", "/", "+1", "/", "+1", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "R", "/", "R", "R", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "R", "/", "/", "R", "/", "/", "R", "/",
                            "/", "R", "/", "/", "R", "/", "R", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < bway.Length; i++)
                    {
                        if (bway[i] == "T")
                        {
                            CreateArrow(curTime, 2, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                            CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                        }
                        else if (bway[i] != "/") CreateArrow(curTime, bway[i], 7, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Turning2A()
                {
                    AddInstance(new TimeRangedEvent(0, game.BeatTime(118), () =>
                    {
                        if (Gametime % 24 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(0, 240), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(Rand(-10, 10) / 50f, -5.0f + Rand(-18, 18) / 10f), 24f, new(Rand(400, 640), 495), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                        if (Gametime % 48 == 0)
                        {
                            CreateEntity(new Particle(Color.White, new(-5.0f + Rand(-18, 18) / 10f, Rand(-10, 10) / 50f), 24f, new(655, Rand(0, 120)), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                            CreateEntity(new Particle(Color.White, new(5.0f + Rand(-18, 18) / 10f, Rand(-10, 10) / 50f), 24f, new(-15, Rand(400, 480)), Sprites.arrow[Rand(0, 1), Rand(0, 3), 0])
                            {
                                DarkingSpeed = 1.0f,
                                AutoRotate = true
                            });
                        }
                    }
                    ));

                    float curTime = game.BeatTime(64);
                    string[] rway = {
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                            "R", "/", "+0", "/", "/", "/", "/", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < rway.Length; i++)
                        {
                            if (rway[i] == "T")
                            {
                                CreateArrow(curTime, 2, 10.0f, 1, 0);
                                CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 1, 0);
                                CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 1, 0);
                            }
                            else if (rway[i] != "/") CreateArrow(curTime, rway[i], 7, 1, 0);
                            curTime += game.BeatTime(1);
                        }
                    curTime = game.BeatTime(64);
                    string[] bway = {
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                            "/", "/", "/", "/", "R", "/", "+0", "/",
                        };
                    for (int T = 0; T < 2; T++)
                        for (int i = 0; i < bway.Length; i++)
                        {
                            if (bway[i] == "T")
                            {
                                CreateArrow(curTime, 2, 10.0f, 0, 0);
                                CreateArrow(curTime + game.BeatTime(0.5f), 1, 10.0f, 0, 0);
                                CreateArrow(curTime + game.BeatTime(1f), 0, 10.0f, 0, 0);
                            }
                            else if (bway[i] != "/") CreateArrow(curTime, bway[i], 7, 0, 0);
                            curTime += game.BeatTime(1);
                        }

                    Action lrot = () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(false);
                    };
                    Action rrot = () =>
                    {
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(6, 12f, 2.5f));
                        ScreenDrawing.CameraEffect.Convulse(true);
                    };

                    AddInstance(new InstantEvent(game.BeatTime(128), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 32), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 64), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 80), rrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 88), lrot));
                    AddInstance(new InstantEvent(game.BeatTime(128 + 96), rrot));
                }
                public static void Turning2B()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",
                            "R", "/", "+0", "/", "R", "/", "/", "/",

                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0",
                        };
                    string[] bway = {
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "+0", "/",
                            "R", "/", "/", "/", "R", "/", "+0", "/",

                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "+0", "+0", "+0", "+0", "+0", "+0", "+0",
                            "R", "/", "/", "/", "R", "/", "/", "/",
                            "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, 0);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, 0);
                        curTime += game.BeatTime(1);
                    }
                }
                public static void Turning2C()
                {
                    float curTime = game.BeatTime(32);
                    string[] rway = {
                            "$2", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "/", "R", "/", "/", "/",
                        };
                    string[] bway = {
                            "$0", "+2", "+2", "+2", "+2", "+2", "+2", "+2",
                            "+2", "+2", "+2", "+2", "+2", "/", "R", "/", "/", "/",
                        };
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (bway[i] != "/") CreateArrow(curTime, bway[i], 7.0f, 0, i % 2);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < rway.Length; i++)
                    {
                        if (rway[i] != "/") CreateArrow(curTime, rway[i], 7.0f, 1, i % 2);
                        curTime += game.BeatTime(1);
                    }
                }

                public static void Final3A()
                {
                    float curTime = game.BeatTime(32);
                    CreateGB(new GreenSoulGB(curTime, 3, 1, game.BeatTime(64 - 1)));
                    string[] bway0 = {
                            "$2", "", "$2", "", "$2", "", "$2", "",
                            "$2", "", "$2", "", "$2", "", "$2", "",
                            "$2", "", "$2", "", "$2", "", "$2", "",
                            "$2", "", "$2", "", "$2", "", "$2", "",
                            "$2", "", "$2", "", "$2", "", "$2", "",
                            "$2", "", "$2", "", "$2", "", "$2", "",
                            "$2", "", "$2", "", "$2", "", "$2", "",
                            "$2", "", "$2", "", "$2", "", "$2", "",
                        };
                    string[] bway1 = {
                            "", "$0", "", "$0", "", "$0", "", "$0", "",
                            "$0", "", "$0", "", "$0", "", "$0", "",
                            "$0", "", "$0", "", "$0", "", "$0", "",
                            "$0", "", "$0", "", "$0", "", "$0", "",
                            "$0", "", "$0", "", "$0", "", "$0", "",
                            "$0", "", "$0", "", "$0", "", "$0", "",
                            "$0", "", "$0", "", "$0", "", "$0", "",
                            "$0", "", "$0", "", "$0", "", "$0",
                        };
                    for (int i = 0; i < bway0.Length; i++)
                    {
                        int col = (i / 2 + 0) % 2;
                        if (bway0[i] != "") CreateArrow(curTime, bway0[i], 16.0f, 0, col, col == 0 ? ArrowAttribute.SpeedUp : ArrowAttribute.None);
                        curTime += game.BeatTime(1);
                    }
                    curTime = game.BeatTime(32);
                    for (int i = 0; i < bway1.Length; i++)
                    {
                        int col = (i / 2 + 0) % 2;
                        if (bway1[i] != "") CreateArrow(curTime, bway1[i], 16.0f, 0, col, col == 0 ? ArrowAttribute.SpeedUp : ArrowAttribute.None);
                        curTime += game.BeatTime(1);
                    }

                    CreateEntity(new TextPrinter("$Welcome to Rhythm Recall", new Vector2(130, 100), new TextFadeoutAttribute(500, 100)) { Depth = 0.95f });
                    AddInstance(new InstantEvent(700, () =>
                    {
                        CreateEntity(new SpecialThanks());
                    }));
                    for (int i = 0; i < 64; i++)
                    {
                        bool dir = i % 2 == 0;
                        AddInstance(new InstantEvent(game.BeatTime(32 + i), () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(16, game.BeatTime(2), dir);
                        }));
                    }
                }
            }

            public void ExtremePlus()
            {
                if (InBeat(0 - 8))
                {
                    for (int i = 0; i < 8; i++) AddInstance(new InstantEvent(BeatTime(8 + i * 8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(1));
                    }));
                    ExBarrage.Intro0();
                }
                if (InBeat(32 - 8)) ExBarrage.Intro0();
                if (InBeat(64 - 8))
                {
                    for (int i = 0; i < 8; i++) AddInstance(new InstantEvent(BeatTime(8 + i * 8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(3, BeatTime(1));
                    })); ExBarrage.Intro0();
                }
                if (InBeat(96 - 8)) ExBarrage.Intro1();
                if (InBeat(128 - 8))
                {
                    for (int i = 0; i < 8; i++) AddInstance(new InstantEvent(BeatTime(8 + i * 8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(3, BeatTime(1));
                    }));
                    ExBarrage.Intro0();
                }
                if (InBeat(160 - 8)) ExBarrage.Intro0();
                if (InBeat(192 - 8))
                {
                    for (int i = 0; i < 8; i++) AddInstance(new InstantEvent(BeatTime(8 + i * 8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(3, BeatTime(1));
                    })); ExBarrage.Intro0();
                }
                if (InBeat(224 - 8)) ExBarrage.Intro2();
                for (int i = 0; i < 3; i++) if (InBeat(i * 32 + 256 - 8)) ExBarrage.IntroRotate0();
                if (InBeat(256 + 32 - 8)) ExBarrage.Intro3();
                if (InBeat(256 + 64 - 8)) ExBarrage.Intro4();
                if (InBeat(256 + 96 - 8))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        float alp = (i % 2 == 0 ? -1 : 1) * 10;
                        AddInstance(new InstantEvent(BeatTime(8 + i * 8), () =>
                        {
                            ScreenDrawing.CameraEffect.RotateTo(alp, BeatTime(8));
                        }));
                    }
                    AddInstance(new TimeRangedEvent(BeatTime(8), BeatTime(50), () =>
                    {
                        Heart.Rotation = ScreenDrawing.ScreenAngle * 0.3f;
                    }));
                    ExBarrage.Intro5();
                }
                if (InBeat(256 + 128))
                {
                    ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(8));
                    ScreenDrawing.CameraEffect.SizeExpand(6, BeatTime(4));
                    ExBarrage.Turning1A();
                }
                if (InBeat(512 - 8)) ExBarrage.Turning1B();
                if (InBeat(512 + 32 - 8)) ExBarrage.Turning1C();
                if (InBeat(512 + 64 - 8)) ExBarrage.Turning1D();
                if (InBeat(512 + 96 - 8)) ExBarrage.Turning1E();
                if (InBeat(512 + 128 - 8)) ExBarrage.Turning1F();

                if (InBeat(640 + 0))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        float alp = (i % 2 == 0 ? -1 : 1) * 12;
                        AddInstance(new InstantEvent(BeatTime(32 + i * 32), () =>
                        {
                            ScreenDrawing.CameraEffect.RotateTo(alp, BeatTime(32));
                        }));
                    }
                    ExBarrage.Area1A();
                }
                if (InBeat(640 + 64))
                {
                    for (int i = 0; i < 8; i++) AddInstance(new InstantEvent(BeatTime(32 + i * 8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(4, BeatTime(2));
                    })); ExBarrage.Area1B();
                }
                if (InBeat(640 + 128))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        float alp = (i % 2 == 0 ? -1 : 1) * 12;
                        AddInstance(new InstantEvent(BeatTime(8 + i * 32), () =>
                        {
                            ScreenDrawing.CameraEffect.RotateTo(alp, BeatTime(32));
                        }));
                    }
                    ExBarrage.Area1A();
                }
                if (InBeat(640 + 192))
                {
                    for (int i = 0; i < 4; i++) AddInstance(new InstantEvent(BeatTime(32 + i * 8), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeExpand(4, BeatTime(2));
                    }));
                    ExBarrage.Area1B2();
                }
                if (InBeat(640 + 224))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        bool shake = i % 2 == 0;
                        int type = i % 4;
                        AddInstance(new InstantEvent(BeatTime(32 + i * 4), () =>
                        {
                            if (shake) AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(4, 8f, 2.4f));
                            switch (type)
                            {
                                case 0:
                                    ScreenDrawing.CameraEffect.SizeExpand(5, BeatTime(2));
                                    break;
                                case 1:
                                    ScreenDrawing.CameraEffect.Convulse(false);
                                    break;
                                case 2:
                                    ScreenDrawing.CameraEffect.SizeShrink(5, BeatTime(2));
                                    break;
                                case 3:
                                    ScreenDrawing.CameraEffect.Convulse(true);
                                    break;
                            }
                        }));
                    }
                    ExBarrage.Area1C();
                }
                if (InBeat(896))
                {
                    ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(4));
                    for (int i = 0; i < 8; i++)
                    {
                        bool shake = i % 2 == 1;
                        int type = i % 4;
                        AddInstance(new InstantEvent(BeatTime(32 + i * 8 - (i % 2 == 0 ? 1 : 0)), () =>
                        {
                            if (shake) AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(4, 8f, 2.4f));
                            switch (type)
                            {
                                case 0:
                                    ScreenDrawing.CameraEffect.RotateTo(12, BeatTime(8));
                                    break;
                                case 1:
                                    ScreenDrawing.CameraEffect.Convulse(40, false);
                                    break;
                                case 2:
                                    ScreenDrawing.CameraEffect.RotateTo(-12, BeatTime(8));
                                    break;
                                case 3:
                                    ScreenDrawing.CameraEffect.Convulse(40, true);
                                    break;
                            }
                        }));
                    }
                    ExBarrage.Area2A();
                }
                if (InBeat(896 + 64))
                {
                    AddInstance(new InstantEvent(BeatTime(34), () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(6));
                    }));
                    ExBarrage.Area2B();
                }
                if (InBeat(896 + 128)) ExBarrage.Area2C();

                if (InBeat(1184)) ExBarrage.Turning2A();
                if (InBeat(1184 + 192 - 32)) ExBarrage.Turning2B();
                if (InBeat(1518)) ExBarrage.Turning2C();

                if (InBeat(1536))
                {
                    ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(4));
                    for (int i = 0; i < 8; i++)
                    {
                        bool shake = i % 2 == 1;
                        int type = i % 4;
                        AddInstance(new InstantEvent(BeatTime(32 + i * 8 - (i % 2 == 0 ? 1 : 0)), () =>
                        {
                            if (shake) AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(4, 8f, 2.4f));
                            switch (type)
                            {
                                case 0:
                                    ScreenDrawing.CameraEffect.RotateTo(12, BeatTime(8));
                                    break;
                                case 1:
                                    ScreenDrawing.CameraEffect.Convulse(40, false);
                                    break;
                                case 2:
                                    ScreenDrawing.CameraEffect.RotateTo(-12, BeatTime(8));
                                    break;
                                case 3:
                                    ScreenDrawing.CameraEffect.Convulse(40, true);
                                    break;
                            }
                        }));
                    }
                    ExBarrage.Area2A();
                }
                if (InBeat(1536 + 64))
                {
                    AddInstance(new InstantEvent(BeatTime(34), () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(6));
                    }));
                    ExBarrage.Area2B();
                }
                if (InBeat(1536 + 128)) ExBarrage.Area2C();
                if (InBeat(1792))
                {
                    ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(4));
                    for (int i = 0; i < 8; i++)
                    {
                        bool shake = i % 2 == 1;
                        int type = i % 4;
                        AddInstance(new InstantEvent(BeatTime(32 + i * 8 - (i % 2 == 0 ? 1 : 0)), () =>
                        {
                            if (shake) AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(4, 8f, 2.4f));
                            switch (type)
                            {
                                case 0:
                                    ScreenDrawing.CameraEffect.RotateTo(12, BeatTime(8));
                                    break;
                                case 1:
                                    ScreenDrawing.CameraEffect.Convulse(40, false);
                                    break;
                                case 2:
                                    ScreenDrawing.CameraEffect.RotateTo(-12, BeatTime(8));
                                    break;
                                case 3:
                                    ScreenDrawing.CameraEffect.Convulse(40, true);
                                    break;
                            }
                        }));
                    }
                    ExBarrage.Area2A();
                }
                if (InBeat(1792 + 64))
                {
                    AddInstance(new InstantEvent(BeatTime(34), () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(6));
                    }));
                    ExBarrage.Area2B();
                }
                if (InBeat(1792 + 128)) ExBarrage.Area2C();
                if (InBeat(1792 + 256) || InBeat(1792 + 256 + 32))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        bool shake = i % 2 == 0;
                        int type = i % 4;
                        AddInstance(new InstantEvent(BeatTime(32 + i * 4), () =>
                        {
                            if (shake) AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(4, 8f, 2.4f));
                            switch (type)
                            {
                                case 0:
                                    ScreenDrawing.CameraEffect.SizeExpand(5, BeatTime(2));
                                    break;
                                case 1:
                                    ScreenDrawing.CameraEffect.Convulse(false);
                                    break;
                                case 2:
                                    ScreenDrawing.CameraEffect.SizeShrink(5, BeatTime(2));
                                    break;
                                case 3:
                                    ScreenDrawing.CameraEffect.Convulse(true);
                                    break;
                            }
                        }));
                    }
                    ExBarrage.Area1C();
                }
                if (InBeat(2112)) ExBarrage.Final3A();
                /* if (this.At0thBeat(4))
                 {
                     PlaySound(Sounds.arrowStuck);
                 }
                 if (this.At0thBeat(8))
                 {
                     PlaySound(Sounds.pierce);
                 }*/
            }

            public void Start()
            {
                ScreenDrawing.UISettings.CreateUISurface();
                NoobBarrage.game = this;
                HardBarrage.game = this;
                ExBarrage.game = this;
                HeartAttribute.MaxHP = 10;
                HeartAttribute.Speed = 2.86f;
                SetGreenBox();
                TP();
                SetSoul(1);
                GametimeDelta = -16.806f / 4f * 30.7f - 0.5f;
                // GametimeDelta = this.BeatTime(2110);

                //  GametimeDetla = this.BeatTime(1532);
                // SetSoul(0); 
            }
        }
    }
}