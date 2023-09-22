using Extends;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace AprilExtends
{
    public class EtherStrike : IChampionShip
    {
        public EtherStrike()
        {
            Game.instance = new Game();

            difficulties = new();
            difficulties.Add("Ether strike", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();

        public class Game : WaveConstructor, IWaveSet
        {
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 21.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 21.5f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 26.5f),
                        }
                    );

            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(62.5f / (156 / 60f)) { }

            public static Game instance;

            public string Music => "Ether Strike";

            public string FightName => "Ether Strike";
            public static float bpm = 6.0096153f;
            private float curHP = 10000;

            public static class ExBarrage
            {
                public static Game game;

                public static void Intro()
                {
                    float[] beats1 =
                    {
                        game.BeatTime(2)
                    };
                    for (int i = 0; i < beats1.Length; i++)
                    {
                        CreateGB(new GreenSoulGB(beats1[i], 3, 0, game.BeatTime(5.5f)));
                    }
                    float[] beats2 =
                    {
                        game.BeatTime(2),
                        game.BeatTime(3),
                        game.BeatTime(4),
                        game.BeatTime(5),
                        game.BeatTime(6),
                        game.BeatTime(7),
                        game.BeatTime(8)
                    };
                    for (int i = 0; i < beats2.Length; i++)
                    {
                        AddInstance(new InstantEvent(beats2[i], () => { ScreenDrawing.CameraEffect.Convulse(false); }));
                    }
                    float[] beats3 =
                    {
                        game.BeatTime(9),
                        game.BeatTime(9.75f)
                    };
                    for (int i = 0; i < beats3.Length; i++)
                    {
                        CreateArrow(beats3[i], i, 8.5f, 0, 0);
                        CreateArrow(beats3[i], i + 2, 8.5f, 1, 0);
                        AddInstance(new TimeRangedEvent(beats3[i], game.BeatTime(0.5f), () => { ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.95f + 1.5f * 0.05f; }));
                    }
                    float[] beats6 =
                    {
                        game.BeatTime(24.75f),
                        game.BeatTime(25.25f),
                        game.BeatTime(26)
                    };
                    for (int i = 0; i < beats6.Length; i++)
                    {
                        int dir = Rand(0, 3);
                        Arrow red = MakeArrow(beats6[i] - game.BeatTime(8), dir, 6, 0, 0);
                        Arrow blue = MakeArrow(beats6[i] - game.BeatTime(8), (dir + 2) % 4, 6, 1, 0);
                        AddInstance(new InstantEvent(game.BeatTime(16.5f + i * 0.2f), () =>
                        {
                            red.Stop(game.BeatTime(8));
                            blue.Stop(game.BeatTime(8));
                        }));
                        CreateEntity(red);
                        CreateEntity(blue);
                    }
                    AddInstance(new InstantEvent(game.BeatTime(10), () =>
                    {
                        int total = (int)game.BeatTime(7);
                        float start = ScreenDrawing.ScreenScale;
                        float end = 0.8f;
                        float del = start - end;
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, total, () =>
                        {
                            ScreenDrawing.ScreenScale = start - del * (t / (total - 1)) * (t / (total - 1));
                            t++;
                        }));
                        float start2 = ScreenDrawing.ScreenAngle;
                        float end2 = 540;
                        float del2 = start2 - end2;
                        float t2 = 0;
                        AddInstance(new TimeRangedEvent(0, total, () =>
                        {
                            ScreenDrawing.ScreenAngle = start2 - del2 * (t2 / (total - 1)) * (t2 / (total - 1));
                            t2++;
                        }));
                    }));
                    CreateGB(new GreenSoulGB(game.BeatTime(18), 0, 1, game.BeatTime(5.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(18), () =>
                    {
                        int total = (int)game.BeatTime(7);
                        float start = ScreenDrawing.ScreenScale;
                        float end = 1;
                        float del = start - end;
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, total, () =>
                        {
                            ScreenDrawing.ScreenScale = start - del * (t / (total - 1)) * (t / (total - 1));
                            t++;
                        }));
                    }));
                    float[] beats4 =
                    {
                        game.BeatTime(18),
                        game.BeatTime(19),
                        game.BeatTime(20),
                        game.BeatTime(21),
                        game.BeatTime(22),
                        game.BeatTime(23),
                        game.BeatTime(24)
                    };
                    for (int i = 0; i < beats4.Length; i++)
                        AddInstance(new InstantEvent(beats4[i], () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(false);
                        }));
                    AddInstance(new InstantEvent(game.BeatTime(26), () =>
                    {
                        int total = (int)game.BeatTime(4);
                        float start = ScreenDrawing.ScreenAngle;
                        float end = ScreenDrawing.ScreenAngle + 180;
                        float del = start - end;
                        float t = 0;
                        AddInstance(new TimeRangedEvent(0, total, () =>
                        {
                            ScreenDrawing.ScreenAngle = start - del * (t / (total - 1)) * (t / (total - 1));
                            t++;
                        }));
                    }));
                    float[] beats5 =
                    {
                        game.BeatTime(30.5f),
                        game.BeatTime(31.25f),
                        game.BeatTime(32f),
                        game.BeatTime(32.75f),
                        game.BeatTime(33.5f),
                    };
                    for (int i = 0; i < beats5.Length; i++)
                    {
                        CreateArrow(beats5[i], "D", 8.5f, 0, 0);
                        CreateArrow(beats5[i], "D", 8.5f, 1, 0);
                    }
                    float[] beats7 =
                    {
                        game.BeatTime(34),
                        game.BeatTime(35),
                        game.BeatTime(36),
                        game.BeatTime(37),
                    };
                    for (int i = 0; i < beats7.Length; i++)
                    {
                        if (i == 0)
                            CreateGB(new GreenSoulGB(beats7[i], Rand(0, 3), 1, game.BeatTime(3.5f)));
                        CreateArrow(beats7[i], "D", 8.5f, 0, 0);
                    }
                    float[] beats8 =
                    {
                        game.BeatTime(38),
                        game.BeatTime(39),
                        game.BeatTime(40),
                        game.BeatTime(41),
                        game.BeatTime(41.25f),
                        game.BeatTime(41.5f),
                        game.BeatTime(41.75f),
                    };
                    for (int i = 0; i < beats8.Length; i++)
                    {
                        int dir = Rand(0, 3);
                        if (i == 0)
                            CreateGB(new GreenSoulGB(beats8[i], Rand(0, 3), 0, game.BeatTime(2.5f)));
                        if (i < 3)
                            CreateArrow(beats8[i], "D", 8.5f, 1, 0);
                        else
                            CreateArrow(beats8[i], i == 3 ? "D" : "+0", 8.5f, 1, 2);
                    }
                    float[] beats9 =
                    {
                        game.BeatTime(42),
                        game.BeatTime(43),
                        game.BeatTime(44),
                        game.BeatTime(45),
                    };
                    for (int i = 0; i < beats9.Length; i++)
                    {
                        if (i == 0)
                            CreateGB(new GreenSoulGB(beats9[i], Rand(0, 3), 1, game.BeatTime(3.5f)));
                        CreateArrow(beats9[i], "D", 8.5f, 0, 0);
                    }
                    float[] beats10 =
                    {
                        game.BeatTime(46),
                        game.BeatTime(47),
                        game.BeatTime(48),
                        game.BeatTime(48.5f),
                    };
                    for (int i = 0; i < beats10.Length; i++)
                    {
                        if (i == 0)
                            CreateGB(new GreenSoulGB(beats10[i], Rand(0, 3), 0, game.BeatTime(3.5f)));
                        CreateArrow(beats10[i], "D", 8.5f, 1, 0);
                    }
                    float[] beats11 =
                    {
                        game.BeatTime(50),
                        game.BeatTime(50.75f),
                        game.BeatTime(51.5f),
                        game.BeatTime(52.25f),
                        game.BeatTime(53),
                        game.BeatTime(53.75f),
                        game.BeatTime(54.5f),
                        game.BeatTime(55.25f),
                        game.BeatTime(56),
                        game.BeatTime(56.75f),
                        game.BeatTime(57.5f),
                    };
                    for (int i = 0; i < beats11.Length; i++)
                    {
                        int type = Rand(0, 1);
                        CreateArrow(beats11[i], "D", 7, 0, type == 0 ? 0 : 2);
                        CreateArrow(beats11[i], "D", 7, 1, type == 0 ? 2 : 0);
                        AddInstance(new InstantEvent(beats11[i], () =>
                        {
                            ScreenDrawing.ScreenAngle = Rand(-30f, 30f);
                            ScreenDrawing.ScreenScale = Rand(1f, 1.15f);
                        }));
                    }
                    AddInstance(new InstantEvent(game.BeatTime(58), () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    }));
                    float beats12 = game.BeatTime(58);
                    CreateGB(new GreenSoulGB(beats12, 3, 1, game.BeatTime(3.5f)));
                    for (int i = 0; i < 16; i++)
                    {
                        CreateArrow(beats12 + game.BeatTime(0.25f * i), 0, 10, 0, 1);
                        CreateArrow(beats12 + game.BeatTime(0.25f * i), 2, 10, 0, 1);
                    }
                    float beats13 = game.BeatTime(62);
                    for (int i = 0; i < 16; i++)
                    {
                        CreateArrow(beats13 + game.BeatTime(0.125f * i), 0, 10, 0, 1);
                        CreateArrow(beats13 + game.BeatTime(0.125f * i), 2, 10, 0, 1);
                        AddInstance(new InstantEvent(beats13 + game.BeatTime(0.125f * i), () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(game.BeatTime(0.125f), RandBool());
                        }));
                    }
                }
                public static void BuildUp()
                {
                    float beats1 = game.BeatTime(2);
                    for (int i = 0; i < 11; i++)
                    {
                        CreateArrow(beats1 + game.BeatTime(0.75f * i), "D", 8.5f, 0, 0);
                        CreateArrow(beats1 + game.BeatTime(0.75f * i), "D", 8.5f, 1, 0);
                        AddInstance(new InstantEvent(beats1 + game.BeatTime(0.75f * i), () =>
                        {
                            ScreenDrawing.CameraEffect.SizeExpand(1.2f, game.BeatTime(0.75f));
                        }));
                    }
                    float beats2 = game.BeatTime(10);
                    CreateGB(new GreenSoulGB(beats2, 0, 0, game.BeatTime(4)));
                    float speed1 = 4;
                    for (int i = 0; i < 16; i++)
                    {
                        CreateArrow(beats2 + game.BeatTime(0.25f * i), 1, speed1, 1, 1);
                        CreateArrow(beats2 + game.BeatTime(0.25f * i), 3, speed1, 1, 1);
                        AddInstance(new InstantEvent(beats2 + game.BeatTime(0.25f * i), () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(speed1, game.BeatTime(0.25f), RandBool());
                        }));
                        speed1 += 12f / 32f;
                    }
                    float beats3 = game.BeatTime(14);
                    for (int i = 0; i < 16; i++)
                    {
                        CreateArrow(beats3 + game.BeatTime(0.125f * i), 1, speed1, 1, 1);
                        CreateArrow(beats3 + game.BeatTime(0.125f * i), 3, speed1, 1, 1);
                        AddInstance(new InstantEvent(beats3 + game.BeatTime(0.125f * i), () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(speed1, game.BeatTime(0.125f), RandBool());
                        }));
                        speed1 += 12f / 32f;
                    }
                    float beats4 = game.BeatTime(18);
                    float delay1 = game.BeatTime(7.5f);
                    CreateGB(new GreenSoulGB(beats4, 3, 0, delay1));
                    AddInstance(new InstantEvent(beats4, () =>
                    {
                        float t = 0;
                        float total = game.BeatTime(3);
                        float start = 0;
                        float end = 200;
                        float del = start - end;
                        AddInstance(new TimeRangedEvent(0, total, () =>
                        {
                            float x = t / (total - 1) * t / (total - 1);
                            float g = MathF.Sqrt(2 * x - x * x);
                            ScreenDrawing.ScreenPositionDetla = new(0, start - del * g);
                            t++;
                        }));
                    }));
                    float[] beats5 =
                    {
                        game.BeatTime(25),
                        game.BeatTime(25.75f),
                    };
                    for (int i = 0; i < beats5.Length; i++)
                    {
                        Arrow arrow = MakeArrow(beats5[i] - game.BeatTime(8), 1, 8.5f, 1, 0);
                        CreateEntity(arrow);
                        AddInstance(new InstantEvent(beats5[i] - game.BeatTime(8.2f), () =>
                        {
                            arrow.Stop(game.BeatTime(8));
                        }));
                        AddInstance(new InstantEvent(beats5[i], () =>
                        {
                            float t = 0;
                            float total = game.BeatTime(0.75f);
                            float start = ScreenDrawing.ScreenPositionDetla.Y;
                            float end = ScreenDrawing.ScreenPositionDetla.Y - 100;
                            float del = start - end;
                            AddInstance(new TimeRangedEvent(0, total, () =>
                            {
                                float x = t / (total - 1) * t / (total - 1);
                                float g = MathF.Sqrt(2 * x - x * x);
                                ScreenDrawing.ScreenPositionDetla = new(0, start - del * g);
                                t++;
                            }));
                        }));
                    }
                    float beats6 = game.BeatTime(34);
                    float delay2 = game.BeatTime(6.5f);
                    CreateGB(new GreenSoulGB(beats6, 1, 0, delay2));
                    CreateGB(new GreenSoulGB(beats6, 3, 1, delay2));
                    float intensity1 = 30f;
                    for (int i = 0; i < 32; i++)
                    {
                        AddInstance(new InstantEvent(beats6 + game.BeatTime(1f / 8f * i), () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(intensity1, game.BeatTime(1f / 8f), i % 2 == 0);
                            intensity1 -= 30f / 32f;
                        }));
                    }
                    float[] beats7 =
                    {
                        game.BeatTime(40.5f),
                        game.BeatTime(41.25f),
                    };
                    for (int i = 0; i < beats7.Length; i++)
                    {
                        CreateArrow(beats7[i], 0, 8.5f, i % 2, 1);
                        AddInstance(new InstantEvent(beats7[i], () =>
                        {
                            float t = 0;
                            float start = ScreenDrawing.ScreenPositionDetla.X;
                            float end = ScreenDrawing.ScreenPositionDetla.X - 150;
                            float del = start - end;
                            float total = game.BeatTime(0.75f);
                            AddInstance(new TimeRangedEvent(0, total, () =>
                            {
                                float x = t / (total - 1) * t / (total - 1);
                                float g = MathF.Sqrt(2 * x - x * x);
                                ScreenDrawing.ScreenPositionDetla = new(start - del * g, 0);
                                t++;
                            }));
                        }));
                    }
                    float beats8 = game.BeatTime(42);
                    float delay3 = game.BeatTime(3.5f);
                    CreateGB(new GreenSoulGB(beats8, 2, 1, delay3));
                    AddInstance(new InstantEvent(beats8, () =>
                    {
                        float t = 0;
                        float start = ScreenDrawing.ScreenPositionDetla.X;
                        float end = ScreenDrawing.ScreenPositionDetla.X + 300;
                        float del = start - end;
                        float total = delay3;
                        AddInstance(new TimeRangedEvent(0, total, () =>
                        {
                            float x = t / (total - 1) * t / (total - 1);
                            float g = MathF.Sqrt(2 * x - x * x);
                            ScreenDrawing.ScreenPositionDetla = new(start - del * g, 0);
                            t++;
                        }));
                    }));
                    float[] beats9 =
                    {
                        game.BeatTime(46),
                        game.BeatTime(46.75f),
                        game.BeatTime(47.5f),
                        game.BeatTime(48.25f),
                        game.BeatTime(49),
                        game.BeatTime(49.5f),
                    };
                    for (int i = 0; i < beats9.Length; i++)
                    {
                        CreateArrow(beats9[i], i % 2 == 0 ? 1 : 3, 8.5f, Rand(0, 1), 0);
                        AddInstance(new InstantEvent(beats9[i], () =>
                        {
                            ScreenDrawing.CameraEffect.Rotate180(i < 5 ? beats9[i + 1] - beats9[i] : game.BeatTime(0.5f));
                            ScreenDrawing.CameraEffect.SizeExpand(1.2f, i < 5 ? beats9[i + 1] - beats9[i] : game.BeatTime(0.5f));
                        }));
                    }
                    float beats10 = game.BeatTime(50);
                    float delay4 = game.BeatTime(13.5f);
                    CreateGB(new GreenSoulGB(beats10, 0, 0, delay4));
                    CreateGB(new GreenSoulGB(beats10, 2, 1, delay4));
                    AddInstance(new InstantEvent(beats10, () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(720, game.BeatTime(8));

                        Heart.Shields.AddShield(new Player.Heart.Shield(2, Heart)
                        {
                            UpdateKeys = new InputIdentity[]
                            {
                            InputIdentity.ThirdRight, InputIdentity.ThirdDown, InputIdentity.ThirdLeft, InputIdentity.ThirdUp
                            }
                        });
                        Heart.Shields.AddShield(new Player.Heart.Shield(3, Heart)
                        {
                            UpdateKeys = new InputIdentity[]
                            {
                            InputIdentity.FourthRight, InputIdentity.FourthDown, InputIdentity.FourthLeft, InputIdentity.FourthUp
                            }
                        });
                    }));
                    float beats11 = game.BeatTime(58);
                    float delay5 = game.BeatTime(5.5f);
                    AddInstance(new InstantEvent(beats11 - game.BeatTime(2), () =>
                    {
                        CreateGB(new GreenSoulGB(game.BeatTime(2), 1, 2, delay5));
                        CreateGB(new GreenSoulGB(game.BeatTime(2), 3, 3, delay5));
                    }));
                    float intensity2 = 30f;
                    for (int i = 0; i < 32; i++)
                    {
                        AddInstance(new InstantEvent(beats11 + game.BeatTime(1f / 8f * i), () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(intensity2, game.BeatTime(1f / 8f), i % 2 == 0);
                            intensity2 -= 30f / 32f;
                        }));
                    }
                    float[] beats12 =
                    {
                        game.BeatTime(65.5f),
                        game.BeatTime(66.25f),
                        game.BeatTime(67),
                        game.BeatTime(67.75f),
                        game.BeatTime(68.5f),
                        game.BeatTime(69.25f),
                        game.BeatTime(70),
                        game.BeatTime(70.75f),
                        game.BeatTime(71.5f),
                        game.BeatTime(72.25f)
                    };
                    for (int i = 0; i < beats12.Length; i++)
                    {
                        Arrow red = MakeArrow(beats12[i], "D", 100, 0, 0);
                        Arrow blue = MakeArrow(beats12[i], "D", 100, 1, 0);
                        CreateEntity(red);
                        CreateEntity(blue);
                        AddInstance(new InstantEvent(beats12[i] - 1, () =>
                        {
                            red.Stop(game.BeatTime(0.75f));
                            blue.Stop(game.BeatTime(0.75f));
                        }));
                    }
                    float beats13 = game.BeatTime(73.5f);
                    CreateGB(new GreenSoulGB(beats13, 0, 0, game.BeatTime(3.5f)));
                    float speed3 = 4;
                    for (int i = 0; i < 16; i++)
                    {
                        CreateArrow(beats13 + game.BeatTime(0.25f * i), 1, speed3, 1, 1);
                        CreateArrow(beats13 + game.BeatTime(0.25f * i), 3, speed3, 1, 1);
                        AddInstance(new InstantEvent(beats13 + game.BeatTime(0.25f * i), () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(speed3, game.BeatTime(0.25f), RandBool());
                        }));
                        speed3 += 12f / 32f;
                    }
                    float beats14 = game.BeatTime(77.5f);
                    for (int i = 0; i < 16; i++)
                    {
                        CreateArrow(beats14 + game.BeatTime(0.125f * i), 0, speed3, 0, 1);
                        CreateArrow(beats14 + game.BeatTime(0.125f * i), 2, speed3, 0, 1);
                        AddInstance(new InstantEvent(beats14 + game.BeatTime(0.125f * i), () =>
                        {
                            ScreenDrawing.CameraEffect.Convulse(speed3, game.BeatTime(0.125f), RandBool());
                        }));
                        speed3 += 12f / 32f;
                    }
                }
                public static void Rotate(float intensity, float time)
                {
                    float start = ScreenDrawing.ScreenAngle;
                    float end = start + intensity;
                    float del = start - end;
                    float t = 0;
                    AddInstance(new TimeRangedEvent(0, time / 2f, () =>
                    {
                        float x = t / (time / 2f - 1);
                        float f = 2 * x - x * x;
                        ScreenDrawing.ScreenAngle = start - del * f;
                        t++;
                    }));
                    float t2 = 0;
                    float start2 = start + intensity;
                    float end2 = start;
                    float del2 = start2 - end2;
                    AddInstance(new TimeRangedEvent(time / 2f + 1, time / 2f, () =>
                    {
                        float x = t2 / (time / 2f - 1);
                        float f = x * x;
                        ScreenDrawing.ScreenAngle = start2 - del2 * f;
                        t2++;
                    }));
                }
                public static void Drop()
                {
                    Regenerate((int)(game.curHP < 5000f ? 5000f - game.curHP : 0f));
                    game.curHP = MathF.Max(game.curHP, 5000);
                    ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(new Shader(Loader.Load<Effect>("Musics\\Ether Strike\\foggy"))
                    {
                        StableEvents = (x) =>
                        {
                            x.Parameters["hp"].SetValue(game.curHP / 10000f * game.curHP / 10000f);
                        }
                    }));
                    float t = 0;
                    float k = 0.005f;
                    AddInstance(new TimeRangedEvent(game.BeatTime(0), game.BeatTime(1000), () =>
                    {
                        float start = game.curHP / 10000f * game.curHP / 10000f;
                        float f = 1 - start + start * MathF.Abs(2 * k * t % 2 - 1);
                        ScreenDrawing.HPBar.HPExistColor = new(255, 0, 0, f);
                        t++;
                    }));
                    CreateGB(new GreenSoulGB(game.BeatTime(2), 0, 0, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(2), 1, 1, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(2), 2, 2, game.BeatTime(3.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(2), 3, 3, game.BeatTime(1.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(2), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(2));
                    }));
                    CreateArrow(game.BeatTime(4), 1, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(4), 3, 8.5f, 1, 0);
                    CreateGB(new GreenSoulGB(game.BeatTime(5), 3, 0, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(5), 0, 1, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(5), 1, 3, game.BeatTime(10.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(5), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(15, game.BeatTime(3));
                        Rotate(15, game.BeatTime(3));
                    }));
                    CreateArrow(game.BeatTime(8), 2, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(8), 3, 8.5f, 1, 0);
                    CreateGB(new GreenSoulGB(game.BeatTime(10), 3, 0, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(10), 0, 1, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(10), 2, 2, game.BeatTime(1.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(10), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(2));
                    }));
                    CreateArrow(game.BeatTime(12.5f), 1, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(12), 2, 8.5f, 1, 0);
                    CreateGB(new GreenSoulGB(game.BeatTime(13), 2, 0, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(13), 3, 1, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(13), 0, 2, game.BeatTime(6.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(13), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(3));
                        Rotate(-15, game.BeatTime(3));
                    }));
                    CreateArrow(game.BeatTime(16), 1, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(16), 2, 8.5f, 1, 0);
                    AddInstance(new InstantEvent(game.BeatTime(16), () =>
                    {
                        ScreenDrawing.ScreenScale = Rand(1, 1.2f);
                        ScreenDrawing.ScreenAngle = Rand(-15f, 15f);
                    }));
                    CreateArrow(game.BeatTime(16.75f), 2, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(16.75f), 3, 8.5f, 1, 0);
                    AddInstance(new InstantEvent(game.BeatTime(16.75f), () =>
                    {
                        ScreenDrawing.ScreenScale = Rand(1, 1.2f);
                        ScreenDrawing.ScreenAngle = Rand(-15f, 15f);
                    }));
                    CreateGB(new GreenSoulGB(game.BeatTime(17.5f), 3, 0, game.BeatTime(2)));
                    CreateGB(new GreenSoulGB(game.BeatTime(17.5f), 1, 1, game.BeatTime(2)));
                    AddInstance(new InstantEvent(game.BeatTime(17.5f), () =>
                    {
                        ScreenDrawing.ScreenScale = 1;
                        ScreenDrawing.ScreenAngle = 0;
                    }));
                    CreateGB(new GreenSoulGB(game.BeatTime(18), 2, 3, game.BeatTime(1.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(18), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(2));
                    }));
                    CreateArrow(game.BeatTime(20), 0, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(20), 2, 8.5f, 1, 0);
                    CreateArrow(game.BeatTime(20.5f), 3, 8.5f, 1, 0);
                    CreateGB(new GreenSoulGB(game.BeatTime(21), 1, 0, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(21), 0, 1, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(21), 2, 2, game.BeatTime(6.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(21), 3, 3, game.BeatTime(2.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(21), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(15, game.BeatTime(3));
                        ScreenDrawing.CameraEffect.Rotate(180, game.BeatTime(3));
                    }));
                    CreateArrow(game.BeatTime(24), 2, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(24), 1, 8.5f, 1, 0);
                    CreateGB(new GreenSoulGB(game.BeatTime(26), 0, 0, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(26), 3, 1, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(26), 1, 3, game.BeatTime(1.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(26), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(2));
                    }));
                    CreateArrow(game.BeatTime(28), 1, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(28), 2, 8.5f, 1, 0);
                    CreateArrow(game.BeatTime(28.5f), 3, 8.5f, 0, 0);
                    CreateGB(new GreenSoulGB(game.BeatTime(29), 2, 0, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(29), 0, 1, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(29), 1, 2, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(29), 3, 3, game.BeatTime(3.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(29), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(4));
                        ScreenDrawing.CameraEffect.Rotate(180, game.BeatTime(4));
                    }));
                    CreateArrow(game.BeatTime(31), 0, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(31), 2, 8.5f, 1, 0);
                    CreateArrow(game.BeatTime(32), 1, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(32), 0, 8.5f, 1, 0);
                    CreateArrow(game.BeatTime(33), 3, 8.5f, 0, 0);
                    CreateArrow(game.BeatTime(33), 2, 8.5f, 1, 0);
                    CreateGB(new GreenSoulGB(game.BeatTime(33), 0, 0, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(33), 1, 1, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(34), 3, 2, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(34), 2, 3, game.BeatTime(1.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(34), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(2));
                    }));
                    CreateArrow(game.BeatTime(36), 3, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(36), 2, 8.5f, 1, 1);
                    CreateGB(new GreenSoulGB(game.BeatTime(36.5f), 0, 3, game.BeatTime(3)));
                    CreateArrow(game.BeatTime(37), 1, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(37), 3, 8.5f, 1, 1);
                    CreateGB(new GreenSoulGB(game.BeatTime(37), 2, 2, game.BeatTime(2.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(37), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(15, game.BeatTime(3));
                        Rotate(-30, game.BeatTime(3));
                    }));
                    CreateArrow(game.BeatTime(40), 0, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(40), 1, 8.5f, 1, 1);
                    CreateGB(new GreenSoulGB(game.BeatTime(42), 1, 0, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(42), 3, 1, game.BeatTime(1.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(42), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(2));
                    }));
                    CreateGB(new GreenSoulGB(game.BeatTime(42), 0, 2, game.BeatTime(2)));
                    CreateGB(new GreenSoulGB(game.BeatTime(42), 2, 3, game.BeatTime(2)));
                    CreateArrow(game.BeatTime(44), 2, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(44), 1, 8.5f, 1, 1);
                    CreateArrow(game.BeatTime(44.5f), 0, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(44.5f), 2, 8.5f, 1, 1);
                    CreateGB(new GreenSoulGB(game.BeatTime(45), 3, 0, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(45), 1, 1, game.BeatTime(2.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(45), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(15, game.BeatTime(3));
                        Rotate(30, game.BeatTime(3));
                    }));
                    CreateGB(new GreenSoulGB(game.BeatTime(45), 2, 2, game.BeatTime(3)));
                    CreateGB(new GreenSoulGB(game.BeatTime(45), 0, 3, game.BeatTime(2.5f)));
                    CreateArrow(game.BeatTime(48), 0, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(48), 3, 8.5f, 1, 1);
                    AddInstance(new InstantEvent(game.BeatTime(48), () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(30, game.BeatTime(0.75f), false);
                    }));
                    CreateArrow(game.BeatTime(48.75f), 2, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(48.75f), 1, 8.5f, 1, 1);
                    AddInstance(new InstantEvent(game.BeatTime(48.75f), () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(30, game.BeatTime(0.75f), true);
                    }));
                    CreateGB(new GreenSoulGB(game.BeatTime(48.75f), 3, 3, game.BeatTime(0.75f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(49.5f), 0, 0, game.BeatTime(2f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(49.5f), 2, 1, game.BeatTime(2f)));
                    AddInstance(new InstantEvent(game.BeatTime(49.5f), () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(20, game.BeatTime(0.5f), false);
                    }));
                    CreateGB(new GreenSoulGB(game.BeatTime(50), 1, 2, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(50), 3, 3, game.BeatTime(1.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(50), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(2));
                    }));
                    CreateArrow(game.BeatTime(52), 3, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(52), 1, 8.5f, 1, 1);
                    CreateGB(new GreenSoulGB(game.BeatTime(52.5f), 2, 3, game.BeatTime(1.5f)));
                    CreateArrow(game.BeatTime(53), 0, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(53), 3, 8.5f, 1, 1);
                    CreateGB(new GreenSoulGB(game.BeatTime(53), 1, 2, game.BeatTime(2.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(53), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(15, game.BeatTime(3));
                        ScreenDrawing.CameraEffect.Rotate(360, game.BeatTime(3));
                    }));
                    CreateArrow(game.BeatTime(56), 1, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(56), 2, 8.5f, 1, 1);
                    CreateGB(new GreenSoulGB(game.BeatTime(58), 0, 0, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(58), 1, 1, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(58), 2, 2, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(58), 3, 3, game.BeatTime(2.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(58), () =>
                    {
                        ScreenDrawing.CameraEffect.SizeShrink(10, game.BeatTime(2));
                    }));
                    CreateArrow(game.BeatTime(60), 1, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(60), 0, 8.5f, 1, 1);
                    CreateArrow(game.BeatTime(60.5f), 2, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(60.5f), 3, 8.5f, 1, 1);
                    CreateArrow(game.BeatTime(61), 3, 8.5f, 0, 1);
                    CreateArrow(game.BeatTime(61), 2, 8.5f, 1, 1);
                    CreateGB(new GreenSoulGB(game.BeatTime(61), 0, 2, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(61), 1, 3, game.BeatTime(1.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(62), 0, 0, game.BeatTime(2.5f)));
                    CreateGB(new GreenSoulGB(game.BeatTime(62), 1, 1, game.BeatTime(2.5f)));
                    AddInstance(new InstantEvent(game.BeatTime(61), () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(90, game.BeatTime(1), false);
                    }));
                    AddInstance(new InstantEvent(game.BeatTime(62), () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(180, game.BeatTime(2), true);
                    }));
                }
                public static void arrowattack_1()
                {
                    float HeartX = Heart.Centre.X;
                    float HeartY = Heart.Centre.Y;
                    DrawingUtil.NormalLine line1 = new(HeartX - 400, HeartY, HeartX + 400, HeartY, 120, 1, Color.White);
                    DrawingUtil.NormalLine line2 = new(HeartX, HeartY - 400, HeartX, HeartY + 400, 120, 1, Color.White);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    AddInstance(new TimeRangedEvent(0, 40, () =>
                    {
                        line1.alpha -= 0.1f;
                        line2.alpha -= 0.1f;
                    }));
                    for (int i = 0; i < 1; i++)
                    {

                        float a = 90;
                        CreateSpear(new Pike(new Vector2(
                            HeartX + Cos(a * i + 180) * 320,
                            HeartY + Sin(a * i + 180) * 320),
                            a * i,
                            15 - i * 2f,
                            1)
                        { IsShootMute = true });

                        CreateSpear(new Pike(new Vector2(
                            HeartX + Cos(a * i + a + 180) * 320,
                            HeartY + Sin(a * i + a + 180) * 320),
                            a * i + a,
                            15 - i * 2f,
                            1)
                        { IsShootMute = true });
                        CreateSpear(new Pike(new Vector2(
                            HeartX + Cos(a * i + a * 2 + 180) * 320,
                            HeartY + Sin(a * i + a * 2 + 180) * 320),
                            a * i + a * 2,
                            15 - i * 2f,
                            1)
                        { IsShootMute = true });
                        CreateSpear(new Pike(new Vector2(
                            HeartX + Cos(a * i + a * 3 + 180) * 320,
                            HeartY + Sin(a * i + a * 3 + 180) * 320),
                            a * i + a * 3,
                            15 - i * 2f,
                            1)
                        { IsShootMute = true });
                    }
                }
                public static void arrowattack_2()
                {
                    float HeartX = Heart.Centre.X;
                    float HeartY = Heart.Centre.Y;
                    DrawingUtil.NormalLine line1 = new(HeartX - 400, HeartY + 400, HeartX + 400, HeartY - 400, 120, 1, Color.White);
                    DrawingUtil.NormalLine line2 = new(HeartX - 400, HeartY - 400, HeartX + 400, HeartY + 400, 120, 1, Color.White);
                    CreateEntity(line1);
                    CreateEntity(line2);
                    AddInstance(new TimeRangedEvent(0, 40, () =>
                    {
                        line1.alpha -= 0.1f;
                        line2.alpha -= 0.1f;
                    }));

                    for (int i = 0; i < 1; i++)
                    {

                        float a = 90;
                        CreateSpear(new Pike(new Vector2(
                            HeartX + Cos(a * i + 180 + 45) * 320,
                            HeartY + Sin(a * i + 180 + 45) * 320),
                            a * i + 45,
                            15 - i * 3f,
                            1)
                        { IsShootMute = true });

                        CreateSpear(new Pike(new Vector2(
                            HeartX + Cos(a * i + a + 180 + 45) * 320,
                            HeartY + Sin(a * i + a + 180 + 45) * 320),
                            a * i + a + 45,
                            15 - i * 3f,
                            1)
                        { IsShootMute = true });
                        CreateSpear(new Pike(new Vector2(
                            HeartX + Cos(a * i + a * 2 + 180 + 45) * 320,
                            HeartY + Sin(a * i + a * 2 + 180 + 45) * 320),
                            a * i + a * 2 + 45,
                            15 - i * 3f,
                            1)
                        { IsShootMute = true });
                        CreateSpear(new Pike(new Vector2(
                            HeartX + Cos(a * i + a * 3 + 180 + 45) * 320,
                            HeartY + Sin(a * i + a * 3 + 180 + 45) * 320),
                            a * i + a * 3 + 45,
                            15 - i * 3f,
                            1)
                        { IsShootMute = true });
                    }
                }
                public static void Dt5attack()
                {


                    CentreCircleBone rotatebone1 = new(0 + 45, 3.1f, 186, bpm * 16 * 16) { IsMasked = true };
                    CentreCircleBone rotatebone2 = new(90 + 45, 3.1f, 186, bpm * 16 * 16) { IsMasked = true };
                    Boneslab boneslab1 = new(0, 10, 0, bpm * 16 * 16);
                    Boneslab boneslab2 = new(90, 10, 0, bpm * 16 * 16);
                    Boneslab boneslab3 = new(180, 10, 0, bpm * 16 * 16);
                    Boneslab boneslab4 = new(270, 10, 0, bpm * 16 * 16);
                    SetBox(240, 168, 168);
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        PlaySound(Sounds.switchScene);
                        SetSoul(0);
                        TP(320 - 42, 240 - 42);
                        CreateEntity(boneslab1);
                        CreateEntity(boneslab2);
                        CreateEntity(boneslab3);
                        CreateEntity(boneslab4);
                        CreateEntity(rotatebone1);
                        CreateEntity(rotatebone2);
                    }));



                }
                public static void Goandbackbone()
                {
                    DrawingUtil.NormalLine Blueline1 = new(320 - 84 - 24, 240 - 84, 320 - 84 - 24, 240 + 84, (int)(bpm * 16), 1, Color.Cyan);
                    DrawingUtil.NormalLine Blueline2 = new(320 + 84 + 24, 240 - 84, 320 + 84 + 24, 240 + 84, (int)(bpm * 16), 1, Color.Cyan);
                    DrawingUtil.NormalLine Orangeline1 = new(320 + 84 + 24, 240 - 84, 320 + 84 + 24, 240 + 84, (int)(bpm * 16), 1, Color.Orange);
                    DrawingUtil.NormalLine Orangeline2 = new(320 - 84 - 24, 240 - 84, 320 - 84 - 24, 240 + 84, (int)(bpm * 16), 1, Color.Orange);
                    int rand = Rand(1, 2);
                    int realrand = 0;
                    if (rand == 1)
                    {
                        realrand = 2;
                        CreateEntity(Blueline2);
                        CreateEntity(Orangeline2);
                        AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                        {
                            Blueline2.alpha -= 1 / bpm / 8;
                            Orangeline2.alpha -= 1 / bpm / 8;
                        }));
                    }
                    if (rand == 2)
                    {
                        realrand = 1;
                        CreateEntity(Blueline1);
                        CreateEntity(Orangeline1);
                        AddInstance(new TimeRangedEvent(0, bpm * 8, () =>
                        {
                            Blueline1.alpha -= 1 / bpm / 8;
                            Orangeline1.alpha -= 1 / bpm / 8;
                        }));
                    }
                    UpBone bone1 = new(true, 0, 168) { ColorType = rand };
                    UpBone bone2 = new(false, 0, 168) { ColorType = realrand };
                    float speed = 0;
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                        {
                            CreateBone(bone1);
                            CreateBone(bone2);
                        }));
                    AddInstance(new TimeRangedEvent(bpm * 8, bpm * 16, () =>
                    {
                        bone1.Speed = Sin(45 + speed) * 5f;
                        bone2.Speed = Sin(45 + speed) * 5f;
                        speed += (360 - 45) / 6 / 16;
                    }));

                }
                public static void BoneSlabAttack()
                {
                    AddInstance(new TimeRangedEvent(0, 1, () =>
                    {
                        Boneslab slab1 = new(0, 84, 1, bpm * 0.5f);
                        CreateEntity(slab1);
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                    {
                        Boneslab slab2 = new(90, 84, 1, bpm * 0.5f);
                        CreateEntity(slab2);
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                    {
                        Boneslab slab3 = new(180, 84, 1, bpm * 0.5f);
                        CreateEntity(slab3);
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 6, 1, () =>
                    {
                        Boneslab slab4 = new(270, 84, 1, bpm * 0.5f);
                        CreateEntity(slab4);
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                    {
                        Boneslab slab1 = new(0, 84, 1, bpm * 0.5f);
                        CreateEntity(slab1);
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 10, 1, () =>
                    {
                        Boneslab slab2 = new(90, 84, 1, bpm * 0.5f);
                        CreateEntity(slab2);
                        PlaySound(Sounds.pierce);
                    }));
                    AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                    {
                        Boneslab slab3 = new(180, 84, 1, bpm * 0.5f);
                        CreateEntity(slab3);
                        PlaySound(Sounds.pierce);
                    }));
                }//我们开始写我们的第一个攻击模块,也就是第一个小节
                public static void ComeandBackBone()
                {
                    if (Rand(0, 1) == 0)
                    {
                        float a = 0;
                        DownBone Bone1 = new(true, 0, Rand(84 - 24, 84));
                        UpBone Bone2 = new(true, 0, 168 - LastRand - 48);
                        CreateBone(Bone1);
                        CreateBone(Bone2);
                        PlaySound(Sounds.pierce);
                        AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                        {
                            Bone1.Speed += a * a;
                            Bone2.Speed += a * a;
                            a += 0.009f;
                        }));
                    }
                    else
                    {
                        float a = 0;
                        DownBone Bone1 = new(false, 0, Rand(84 - 24, 84));
                        UpBone Bone2 = new(false, 0, 168 - LastRand - 48);
                        CreateBone(Bone1);
                        CreateBone(Bone2);
                        PlaySound(Sounds.pierce);
                        AddInstance(new TimeRangedEvent(0, bpm * 16, () =>
                        {
                            Bone1.Speed += a * a;
                            Bone2.Speed += a * a;
                            a += 0.009f;
                        }));
                    }
                }
                public static class Firstred
                {
                    public static void part1()
                    {//骨墙攻击和箭头攻击，箭头攻击我有写过，不用担心
                     //上面那个是一拍的骨墙
                     //第一个小节：//// XXXX XXXX XXXX,懂？//好可怕
                     //一拍之后，是bpm*4
                        SetBox(240, 168, 168);
                        SetSoul(0);
                        PlaySound(Sounds.switchScene);
                        AddInstance(new InstantEvent(bpm * 4 - 2, () =>
                        {
                            BoneSlabAttack();
                        }));
                    }

                    //next one!
                    public static void part2()
                    {
                        arrowattack_1();
                        AddInstance(new InstantEvent(bpm * 4 - 2, () =>
                        {
                            BoneSlabAttack();
                        }));

                    }
                    public static void part3()
                    {
                        AddInstance(new InstantEvent(bpm * 1 - 2, () =>
                        { //方法体
                            arrowattack_2();
                        }));

                        AddInstance(new InstantEvent(bpm * 4 - 2, () =>
                        { //方法体
                            BoneSlabAttack();
                        }));

                    }
                    public static void part5()
                    {
                        arrowattack_2();
                        CentreCircleBone bone1 = new(30, 3, 186, bpm * 16 * 12)
                        {
                            ColorType = 2,
                            IsMasked = true
                        };

                        CreateBone(bone1);

                        for (int a = 0; a < 6; a++)
                        {
                            AddInstance(new TimeRangedEvent(bpm * 4 + bpm * 2 * a, 1, () =>
                            {
                                ComeandBackBone();
                            }));
                        }
                    }
                    public static void part6()
                    {
                        arrowattack_2();
                        for (int a = 0; a < 6; a++)
                        {
                            AddInstance(new TimeRangedEvent(bpm * 4 + bpm * 2 * a, 1, () =>
                            {
                                ComeandBackBone();
                            }));
                        }
                    }
                    public static void part7_8()
                    {
                        AddInstance(new TimeRangedEvent(bpm, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 16, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 20, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 23, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 26, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                    }
                    public static void part9()
                    {
                        arrowattack_1();
                        AddInstance(new TimeRangedEvent(bpm * 4, 1, () =>
                        {
                            ComeandBackBone();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 4 + bpm * 4 * 0.5f, 1, () =>
                        {
                            ComeandBackBone();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 8, 1, () =>
                        {
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 12, 1, () =>
                        {
                            arrowattack_1();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                        {
                            arrowattack_2();
                        }));
                    }
                    public static void part10()
                    {
                        arrowattack_1();
                        AddInstance(new TimeRangedEvent(bpm * 2, 1, () =>
                        {
                            arrowattack_2();
                        }));
                        for (int a = 0; a < 6; a++)
                        {
                            AddInstance(new TimeRangedEvent(bpm * 4 + bpm * 2 * a, 1, () =>
                            {
                                ComeandBackBone();
                            }));
                        }
                    }
                    public static void part11()
                    {

                        AddInstance(new TimeRangedEvent(bpm * 1, 1, () =>
                        {
                            arrowattack_1();
                        }));
                        for (int a = 0; a < 6; a++)
                        {
                            AddInstance(new TimeRangedEvent(bpm * 4 + bpm * 2 * a, 1, () =>
                            {
                                ComeandBackBone();
                            }));
                        }
                    }
                    public static void part12()
                    {
                        AddInstance(new TimeRangedEvent(bpm * 16 - bpm * 16, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 20 - bpm * 16, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 23 - bpm * 16, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 26 - bpm * 16, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                        AddInstance(new TimeRangedEvent(bpm * 14, 1, () =>
                        {
                            arrowattack_1();
                            arrowattack_2();
                        }));
                    }
                    public static void part13()
                    {
                        float sin = 0;
                        float speed = 0;
                        AddInstance(new TimeRangedEvent(0, bpm * 64, () =>
                        {
                            sin = Sin(speed) * 24;
                            speed += 4f;
                        }));
                        for (int a = 0; a < (int)(8 * bpm); a++)
                        {
                            AddInstance(new TimeRangedEvent(a * 6, 1, () =>
                            {
                                CreateBone(new LeftBone(true, 3.5f, 84 - 12 + sin));
                                CreateBone(new RightBone(true, 3.5f, 84 - 24 - sin));
                            }));
                        }
                    }
                }
            }
            public void BarrageRedSoul()
            {
            }



            public void Noob()
            {

            }
            public void Hard()
            {
            }
            public void Extreme()
            {
                BarrageRedSoul();
                // if (At0thBeat(2)) PlaySound(Sounds.switchScene);
                // if (At0thBeat(1)) PlaySound(Sounds.arrowStuck);
                // if (this.InBeat(0)) ExBarrage.Intro0();
                // if (this.InBeat(8)) ExBarrage.Intro1();
                if (InBeat(0))
                {
                    ScreenDrawing.HPBar.HPExistColor = new(0, 161, 255);
                    ScreenDrawing.HPBar.HPLoseColor = new(Color.Gray, 0.5f);
                    ScreenDrawing.HPBar.AreaOccupied = new(26, 150, 18, 200);
                    ScreenDrawing.HPBar.Vertical = true;
                    ExBarrage.Intro();
                    // Regenerate(-9999);
                    curHP = 10000;
                }
                if (InBeat(66)) ExBarrage.Firstred.part1();
                if (InBeat(70)) ExBarrage.Firstred.part2();
                if (InBeat(74)) ExBarrage.Firstred.part3();
                if (InBeat(78)) ExBarrage.Firstred.part2();
                if (InBeat(82)) ExBarrage.Firstred.part5();
                if (InBeat(86)) ExBarrage.Firstred.part6();
                if (InBeat(90)) ExBarrage.Firstred.part7_8();
                if (InBeat(96)) ExBarrage.Firstred.part6();
                if (InBeat(102)) ExBarrage.Firstred.part9();
                if (InBeat(106)) ExBarrage.Firstred.part10();
                if (InBeat(110)) ExBarrage.Firstred.part9();
                if (InBeat(114)) ExBarrage.Firstred.part6();
                if (InBeat(118)) ExBarrage.Firstred.part6();
                if (InBeat(122)) ExBarrage.Firstred.part11();
                if (InBeat(126)) ExBarrage.Firstred.part12();
                if (InBeat(130)) ExBarrage.Firstred.part13();
                if (InBeat(144))
                {
                    SetSoul(1);
                    SetBox(240, 84, 84);
                    TP();
                }
                if (InBeat(144))
                    ExBarrage.BuildUp();
                if (InBeat(224))
                {
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    PlaySound(Sounds.switchScene);
                    ExBarrage.Drop();
                }
                if (curHP > 10000)
                    curHP = 10000;
                if (Gametime >= BeatTime(224) && curHP > 1)
                {
                    if (Gametime % 2 == 1)
                    {
                        curHP--;
                        Regenerate(-1);
                    }
                }
                if (InBeat(288))
                {
                    ExBarrage.Dt5attack();
                    for (int a = 0; a < 11; a++)
                    {
                        AddInstance(new TimeRangedEvent(bpm * 24 * a, 1, () =>
                        {
                            ExBarrage.Goandbackbone();
                        }));
                    }
                }
                if (InBeat(288 + 6 * 11))
                {
                    CreateEntity(new Boneslab(0, 64, bpm * 16, bpm * 32));
                    CreateEntity(new Boneslab(90, 64, bpm * 16, bpm * 32));
                    CreateEntity(new Boneslab(180, 64, bpm * 16, bpm * 32));
                    CreateEntity(new Boneslab(270, 64, bpm * 16, bpm * 32));
                }
            }
            public void Easy()
            {
            }
            public void ExtremePlus()
            {
                Extreme();
            }
            public void Normal()
            {
                Boneslab boneslab1 = new(0, 84, 1, bpm);
                Boneslab boneslab2 = new(0, 84, 1, bpm);
                Boneslab boneslab3 = new(0, 84, 1, bpm);
                Boneslab boneslab4 = new(0, 84, 1, bpm);
                if (GametimeF == 1)
                {
                    SetBox(240, 168, 168);
                }
                if (GametimeF == 120)
                {
                    //  CreateBone(new CustomBone(new(Heart.Centre.X, 240 + 120), Motions.PositionRoute., 0, 30)
                    // {
                    //     PositionRouteParam = new float[] { 1, 1 }
                    // });
                    //  CustomBone bone1 = new()
                }

            }

            private const int ObjectCount = 1169;

            public void Start()
            {
                ExBarrage.game = this;
                HeartAttribute.MaxHP = 10000;
                HeartAttribute.DamageTaken = 0;
                int k = (int)(100 * (96f / ObjectCount + 0.08f));
                AdvanceFunctions.Interactive.AddMissEvent(() => { Regenerate(-200); curHP -= 200; });
                AdvanceFunctions.Interactive.AddOkayEvent(() => { Regenerate(k / 4); curHP += k / 4; });
                AdvanceFunctions.Interactive.AddNiceEvent(() => { Regenerate(k / 2); curHP += k / 2; });
                AdvanceFunctions.Interactive.AddPerfectEvent(() => { Regenerate(k); curHP += k; });
                HeartAttribute.Speed = 3.46f;
                HeartAttribute.SoftFalling = true;
                SetGreenBox();
                TP();
                SetSoul(1);
                // GametimeDelta = -10 + this.BeatTime(224);
                GametimeDelta = -10;
                //   GametimeDetla = 4300;

                //  GametimeDetla = this.BeatTime(1532);
                // SetSoul(0); 

            }
        }
    }
}