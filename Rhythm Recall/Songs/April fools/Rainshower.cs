using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public class Rainshower : IChampionShip
    {
        public Rainshower()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));

            difficulties = new();
            difficulties.Add("It's going to rain", Difficulty.ExtremePlus);
            difficulties.Add("TEST", Difficulty.Extreme);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public static Game instance;
            private static Shader shader;
            public Game() : base(62.5f / (174 / 60f)) { }
            public static float bpm = 62.5f / (174 / 60f) / 4;

            public string Music => "Rainshower";

            public string FightName => "Rainshower";

            public SongInformation Attributes => new ThisInformation();
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 1)
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 1)
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 1),
                        }
                    );
                public override string BarrageAuthor => "zKronO";
                public override string AttributeAuthor => "zKronO";
                public override string PaintAuthor => "Shun Yamaguchi";
                public override string SongAuthor => "Silentroom";
            }
            public static void Shadershake(float Wave, float Radian, float duration)
            {
                ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(shader));
                float a = 0;
                float b = 0;
                AddInstance(new TimeRangedEvent(duration / 2, () =>
                {
                    a += Wave;
                    b += Radian;
                    shader.Parameters["range"].SetValue(a);
                    shader.Parameters["frequency"].SetValue(b);
                }));
                AddInstance(new TimeRangedEvent(duration / 2 + 1, duration / 2, () =>
                {
                    a -= Wave;
                    b -= Radian;
                    shader.Parameters["range"].SetValue(a);
                    shader.Parameters["frequency"].SetValue(b);
                }));
            }
            public static Game game;
            private static class ExBarrage
            {
                public static void effect1()
                {
                    game.DelayBeat(288.25f, () =>
                    {
                        ScreenDrawing.UIColor = Color.White;
                        ScreenDrawing.HPBar.HPExistColor = Color.Blue;
                        ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                        ScreenDrawing.HPBar.AreaOccupied = new CollideRect(20, 140, 20, 220);
                    });
                }
                public static void rhythm01()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "$3","/","/","/",    "$3","/","$3","/",    "$3","/","/","/",    "/","/","/","/",
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
                            instance.CreateArrows(t, 7.5f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void act00()
                {
                    game.DelayBeat(0.25f, () =>
                    {
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(4), "$Slowly and gleaming fell the shower", new UndyneFight_Ex.Fight.TextSpeedAttribute(10000)) { Position = new(40, 240) });
                    });
                    game.DelayBeat(0.25f + 4, () =>
                    {
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(4), "$Through which the evening sun shone", new UndyneFight_Ex.Fight.TextSpeedAttribute(10000)) { Position = new(40, 240) });
                    });
                    game.DelayBeat(0.25f + 4 * 2, () =>
                    {
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(4), "$The traveller walked down narrow paths", new UndyneFight_Ex.Fight.TextSpeedAttribute(10000)) { Position = new(15, 240) });
                    });
                    game.DelayBeat(0.25f + 4 * 3, () =>
                    {
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(4), "$with a sombre soul", new UndyneFight_Ex.Fight.TextSpeedAttribute(10000)) { Position = new(180, 240) });
                    });
                    game.DelayBeat(0.25f + 4 * 4, () =>
                    {
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(4), "$RAINSHOWER", new UndyneFight_Ex.Fight.TextSpeedAttribute(10000)) { Position = new(240, 240) });
                    });
                    game.DelayBeat(0.5f + 4 * 4, () =>
                    {
                        Shadershake(30, 5, game.BeatTime(1f));
                    });
                }
                public static void act01()
                {
                    float a = (int)(bpm * 5);
                    Extends.DrawingUtil.Linerotate l1 = new(0, 240, 90, game.BeatTime(2), 0.75f, Color.Blue);
                    Extends.DrawingUtil.Linerotate l2 = new(0, 240, 90, game.BeatTime(2), 0.75f, Color.Blue);
                    Extends.DrawingUtil.Linerotate l3 = new(0, 240, 90, game.BeatTime(2), 0.75f, Color.Blue);
                    Extends.DrawingUtil.Linerotate l4 = new(0, 240, 90, game.BeatTime(2), 0.75f, Color.Blue);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    CreateEntity(l3);
                    CreateEntity(l4);
                    game.ForBeat(1.25f, () =>
                    {
                        a--;
                        l1.xCenter += a * 0.75f;
                        l2.xCenter += a * 0.5f;
                        l3.xCenter += a * 0.25f;
                        l4.xCenter += a * 0.125f;
                    });
                    game.ForBeat(0, 1, () =>
                    {
                        l1.alpha -= a * 0.00125f;
                        l2.alpha -= a * 0.00125f;
                        l3.alpha -= a * 0.00125f;
                        l4.alpha -= a * 0.00125f;
                    });
                    game.DelayBeat(1, () =>
                    {
                        l1.alpha = 0.75f;
                        l2.alpha = 0.75f;
                        l3.alpha = 0.75f;
                        l4.alpha = 0.75f;
                    });
                    game.ForBeat(1, 1, () =>
                    {
                        l1.alpha -= 0.0375f;
                        l2.alpha -= 0.0375f;
                        l3.alpha -= 0.0375f;
                        l4.alpha -= 0.0375f;
                    });
                }
                public static void act02()
                {
                    float a = (int)(bpm * 5);
                    Extends.DrawingUtil.Linerotate l1 = new(640, 240, 90, game.BeatTime(2), 0.75f, Color.Blue);
                    Extends.DrawingUtil.Linerotate l2 = new(640, 240, 90, game.BeatTime(2), 0.75f, Color.Blue);
                    Extends.DrawingUtil.Linerotate l3 = new(640, 240, 90, game.BeatTime(2), 0.75f, Color.Blue);
                    Extends.DrawingUtil.Linerotate l4 = new(640, 240, 90, game.BeatTime(2), 0.75f, Color.Blue);
                    CreateEntity(l1);
                    CreateEntity(l2);
                    CreateEntity(l3);
                    CreateEntity(l4);
                    game.ForBeat(1.25f, () =>
                    {
                        a--;
                        l1.xCenter -= a * 0.75f;
                        l2.xCenter -= a * 0.5f;
                        l3.xCenter -= a * 0.25f;
                        l4.xCenter -= a * 0.125f;
                    });
                    game.ForBeat(0, 1, () =>
                    {
                        l1.alpha -= a * 0.00125f;
                        l2.alpha -= a * 0.00125f;
                        l3.alpha -= a * 0.00125f;
                        l4.alpha -= a * 0.00125f;
                    });
                    game.DelayBeat(1, () =>
                    {
                        l1.alpha = 0.75f;
                        l2.alpha = 0.75f;
                        l3.alpha = 0.75f;
                        l4.alpha = 0.75f;
                    });
                    game.ForBeat(1, 1, () =>
                    {
                        l1.alpha -= 0.0375f;
                        l2.alpha -= 0.0375f;
                        l3.alpha -= 0.0375f;
                        l4.alpha -= 0.0375f;
                    });
                }

            }
            public void ExtremePlus()
            {
                if (InBeat(0)) ExBarrage.effect1();
                if (InBeat(32.5f)) ExBarrage.act00();
                for (int i = 0; i < 16; i++)
                {
                    if (InBeat(i * 4)) ExBarrage.rhythm01();
                }
                for (int i = 0; i < 7; i++)
                {
                    if (InBeat(i * 8 + 2)) ExBarrage.act01();
                    if (InBeat(i * 8 + 6)) ExBarrage.act02();
                }
            }

            public void Extreme()
            {
                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Entities:" + "$" + (GetAll<Entity>().Length - 9).ToString(), new(0, 240), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { PlaySound = false });

                CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(1, "$$Stars:" + "$" + GetAll<Extends.Star>().Length.ToString(), new(0, 240 - 20), new UndyneFight_Ex.Fight.TextAttribute[] { new UndyneFight_Ex.Fight.TextSpeedAttribute(1145), new UndyneFight_Ex.Fight.TextSizeAttribute(0.7f), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Cyan) }) { PlaySound = false });
                if (GametimeF == 1)

                    /*if(GametimeF==1) 
                    {
                        CreateArrow(40, 1, 5, 2, 1);
                        CreateArrow(40, 3, 5, 1, 0);
                        CreateGB(new GreenSoulGB(40, 3, 3, 40));
                    }*/
                    if (GametimeF == 100)
                    {
                        SetSoul(0);

                        Extends.FakeArrow arrow = new(0, 1, 2, new(320, 200), 1145, 1.5f, 270);
                        CreateEntity(arrow);
                        //   AddInstance(new TimeRangedEvent(150, () =>
                        //CreateEntity(arrow);
                        /* AddInstance(new TimeRangedEvent(150, () =>

                         {
                             arrow.Centre = arrow.Centre * 0.95f + new Vector2(320, 40) * 0.05f;
                         }));
                         AddInstance(new TimeRangedEvent(151, 1, () =>
                         {
                             arrow.ChangeType(0, 0, 0);
                         }));*/
                    }
            }
            #region Non-
            public void Easy()
            {
                //
            }

            public void Hard()
            {
                //
            }

            public void Noob()
            {
                //
            }

            public void Normal()
            {
                //
            }
            #endregion
            public void Start()
            {
                game = this;
                GametimeDelta = BeatTime(-2f);
                //GametimeDelta = BeatTime(32);
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(320, 240);
                SetSoul(1);
                HeartAttribute.MaxHP = 1000;
                ScreenDrawing.HPBar.Vertical = true;
                //HeartAttribute.KR = true;
                // HeartAttribute.KRDamage = 2;
                ScreenDrawing.HPBar.HPExistColor = Color.Transparent;
                ScreenDrawing.HPBar.HPLoseColor = Color.Transparent;
                //ScreenDrawing.UIColor = Color.Transparent;
                shader = new Shader(Loader.Load<Effect>("Musics\\Rainshower\\Shake"));
            }
        }

    }
}