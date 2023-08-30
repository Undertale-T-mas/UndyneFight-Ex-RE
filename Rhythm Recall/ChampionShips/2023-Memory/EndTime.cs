using Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public class SpecialOne : IChampionShip
    {
        public SpecialOne()
        {
            Game.instance = new Game();
            divisionInformation = new SaveInfo("imf{");
            divisionInformation.PushNext(new SaveInfo("dif:4"));
            difficulties = new();
            difficulties.Add("div.2", Difficulty.Noob);
            difficulties.Add("div.1", Difficulty.Hard);
            difficulties.Add("div.0", Difficulty.ExtremePlus);
            //   this.difficulties.Add("Anomaly Test", Difficulty.Extreme);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public SaveInfo DivisionInformation => divisionInformation;
        public SaveInfo divisionInformation;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 5.0f),
                            new(Difficulty.Hard, 15.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 5.0f),
                            new(Difficulty.Hard, 15.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 8.0f),
                            new(Difficulty.Hard, 19.0f),
                        }
                    );
                public override string BarrageAuthor => "2Kr0NO";
                public override string AttributeAuthor => "ParaDOXXX";
                public override string PaintAuthor => "Cres";
                public override string SongAuthor => "Cres";

                public override bool Hidden
                {
                    get
                    {
#if DEBUG
                        return false;
#endif
                        return PlayerManager.CurrentUser == null || !PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("reEndTime");
                    }
                }
            }
            public SongInformation Attributes => new ThisInformation();

            public Game() : base(62.5f / (180f / 60f)) { }

            public static Game game;

            public static Game instance;
            public void LineShadow(int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * 0.5f, 0.24f - 0.24f / times * t));

                }
            }
            public void LineShadow(float deep, int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * 0.5f, deep - deep / times * t));

                }
            }
            public void LineShadow(float delay, float deep, int times, Line line)
            {
                for (int i = 0; i < times; i++)
                {
                    int t = i;
                    line.InsertRetention(new(t * delay, deep - deep / times * t));

                }
            }
            private bool notRegistered = true;
            public string Music => "EndTime";
            public string FightName => "End Time";

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
                    game.ForBeat(3, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(Color.Black, new(95, 137, 154, 60), 0.2f) * alp;
                        alp += 0.0075f;
                    });
                    game.ForBeat(3, 3, () =>
                    {
                        ScreenDrawing.BackGroundColor = Color.Lerp(Color.Black, new(95, 137, 154, 60), 0.2f) * alp;
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
                public static void SoftSetBox(Vector2 center, float width, float height, float duration, int type)
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
                public static void SoftTP(Vector2 center, float duration, int type)
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

                            InstantTP(new Vector2(startx - delx * f, starty - dely * f));
                            t++;
                        }));
                    }
                }
                public static void Rain(float time, int DensityPerFrame)
                {
                    AddInstance(new TimeRangedEvent(time, () =>
                    {
                        CentreEasing.EaseBuilder e = new();
                        e.Insert(game.BeatTime(24), CentreEasing.EaseInSine(new(Rand(0, 680), -50), new(LastRand - 50, 560), game.BeatTime(24)));
                        Line a = new(e.GetResult(), (s) => { return -87; }, (s) => { return Rand(8, 48); }) { Alpha = 0.4f, Width = 2.3f, DrawingColor = new(0, 0, 125) };
                        Line b = new(e.GetResult(), (s) => { return -87; }, (s) => { return Rand(8, 48); }) { Alpha = 0.1f, Width = 2.3f, DrawingColor = new(0, 0, 55) };
                        Line[] line = { a, b };
                        foreach (Line lines in line)
                        {
                            for (int i = 0; i < DensityPerFrame; i++)
                            {
                                CreateEntity(lines);
                                game.ForBeat(6, () =>
                                {
                                    if (lines.Centre.Y >= 480) lines.Dispose();
                                });//4k确实能打（（（（
                            }
                        }
                    }));
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
                        ValueEasing.EaseBuilder rot = new();
                        rot.Insert(game.BeatTime(4), ValueEasing.EaseInBack(0, 120, game.BeatTime(4)));
                        rot.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
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
                            ScreenDrawing.ScreenAngle = 0;
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        float alp = 0.75f;
                        DrawingUtil.NormalLine a = new(0, Rand(150, 195), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine b = new(0, Rand(195, 240), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine c = new(0, Rand(240, 285), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine d = new(0, Rand(285, 330), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine e = new(0, Rand(105, 150), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine f = new(0, Rand(330, 375), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        CreateEntity(a);
                        CreateEntity(b);
                        CreateEntity(c);
                        CreateEntity(d);
                        CreateEntity(e);
                        CreateEntity(f);
                        for (int i = 0; i < game.BeatTime(24); i++)
                        {
                            AddInstance(new InstantEvent(i * 4, () =>
                            {
                                a.alpha = alp;
                                b.alpha = alp;
                                c.alpha = alp;
                                d.alpha = alp;
                                e.alpha = alp;
                                f.alpha = alp;
                                alp -= 0.0075f;
                            }));
                            AddInstance(new InstantEvent(i * 4 + 2, () =>
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
                    game.DelayBeat(8, () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuad(new(320, 240), new(160, 240), game.BeatTime(4)), (s) => { return 45; }) { Alpha = 0.85f };
                        a.AlphaDecrease(game.BeatTime(4), 0.85f);
                        a.InsertRetention(new(8, 0.75f));
                        Line b = new(CentreEasing.EaseOutQuad(new(320, 240), new(480, 240), game.BeatTime(4)), (s) => { return 45; }) { Alpha = 0.85f };
                        b.AlphaDecrease(game.BeatTime(4), 0.85f);
                        b.InsertRetention(new(8, 0.75f));
                        CreateEntity(a);
                        CreateEntity(b);
                    });
                    game.DelayBeat(16, () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuad(new(320, 240), new(160, 240), game.BeatTime(4)), (s) => { return -45; }) { Alpha = 0.85f };
                        a.AlphaDecrease(game.BeatTime(4), 0.85f);
                        a.InsertRetention(new(8, 0.75f));
                        Line b = new(CentreEasing.EaseOutQuad(new(320, 240), new(480, 240), game.BeatTime(4)), (s) => { return -45; }) { Alpha = 0.85f };
                        b.AlphaDecrease(game.BeatTime(4), 0.85f);
                        b.InsertRetention(new(8, 0.75f));
                        CreateEntity(a);
                        CreateEntity(b);
                    });
                    game.DelayBeat(20, () =>
                    {
                        CentreEasing.EaseBuilder ease1 = new();
                        CentreEasing.EaseBuilder ease2 = new();
                        CentreEasing.EaseBuilder ease3 = new();
                        CentreEasing.EaseBuilder ease4 = new();
                        ValueEasing.EaseBuilder rota = new();
                        ValueEasing.EaseBuilder rotb = new();
                        ease1.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(160, 240), game.BeatTime(2)));
                        ease2.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(480, 240), game.BeatTime(2)));
                        ease3.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(2)));
                        ease4.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(2)));
                        rota.Insert(game.BeatTime(4), ValueEasing.Stable(0));
                        rotb.Insert(game.BeatTime(4), ValueEasing.Stable(0));
                        ease1.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(160, 240), new(320, 240), game.BeatTime(2)));
                        ease2.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(480, 240), new(320, 240), game.BeatTime(2)));
                        ease3.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(240, 240), new(320, 240), game.BeatTime(2)));
                        ease4.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(400, 240), new(320, 240), game.BeatTime(2)));
                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(4)));
                        ease3.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(4)));
                        ease4.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(4)));
                        rota.Insert(game.BeatTime(4), ValueEasing.EaseOutQuart(0, 45, game.BeatTime(4)));
                        rotb.Insert(game.BeatTime(4), ValueEasing.EaseOutQuart(0, -45, game.BeatTime(4)));
                        Line a = new(ease1.GetResult(), ValueEasing.Combine(rota.GetResult(), 90)) { Alpha = 0.85f };
                        Line b = new(ease2.GetResult(), ValueEasing.Combine(rota.GetResult(), 90)) { Alpha = 0.85f };
                        Line c = new(ease3.GetResult(), ValueEasing.Combine(rotb.GetResult(), 90)) { Alpha = 0.85f };
                        Line d = new(ease4.GetResult(), ValueEasing.Combine(rotb.GetResult(), 90)) { Alpha = 0.85f };
                        a.InsertRetention(new(4, 0.55f));
                        b.InsertRetention(new(4, 0.55f));
                        c.InsertRetention(new(4, 0.55f));
                        d.InsertRetention(new(4, 0.55f));
                        Line[] lines = { a, b, c, d };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line line in lines)
                            {
                                CreateEntity(line);
                                line.AlphaDecrease(game.BeatTime(2), 0.75f);
                            }
                        });
                        game.DelayBeat(2, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaIncrease(game.BeatTime(2), 0.75f);
                            }
                        });
                        game.DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(game.BeatTime(4), 0.85f);
                            }
                        });
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
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease1 = new();
                        CentreEasing.EaseBuilder ease2 = new();
                        ValueEasing.EaseBuilder rot1 = new();
                        ValueEasing.EaseBuilder rot2 = new();
                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(640, 0), new(120, 360), game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(30));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(0, 240)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(90));

                        ease1.Insert(game.BeatTime(4), CentreEasing.Stable(new(120, 360)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(30, -135, game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(0, 240), new(480, 240), game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(90));

                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(120, 360), new(440, 180), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(480, 240)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(90, -45, game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(-135));

                        ease1.Insert(game.BeatTime(4), CentreEasing.Stable(new(440, 180)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(-135, 45, game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(480, 240), new(200, 180), game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(-45));

                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(440, 180), new(320, 240), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(200, 180)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(-45, 135, game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(45));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(135));

                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(200, 180), new(320, 240), game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(45, -135, game.BeatTime(4)));
                        ease1.Insert(game.BeatTime(12), CentreEasing.Stable(new(320, 240)));
                        ease2.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));

                        rot1.Insert(game.BeatTime(6), ValueEasing.EaseOutBack(-135, -360, game.BeatTime(6)));
                        rot2.Insert(game.BeatTime(6), ValueEasing.EaseOutBack(135, -90, game.BeatTime(6)));
                        rot1.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-360, -315, game.BeatTime(0.75f)));
                        rot2.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-90, -45, game.BeatTime(0.75f)));
                        rot1.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-315, -270, game.BeatTime(0.75f)));
                        rot2.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-45, 0, game.BeatTime(0.75f)));
                        rot1.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutExpo(-270, -225, game.BeatTime(0.5f)));
                        rot2.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutExpo(0, 45, game.BeatTime(0.5f)));
                        Line l1 = new(ease1.GetResult(), rot1.GetResult()) { Alpha = 0.55f };
                        Line l2 = new(ease2.GetResult(), rot2.GetResult()) { Alpha = 0.55f };
                        Line[] l = { l1, l2 };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line line in l)
                            {
                                CreateEntity(line);
                                line.InsertRetention(new(2, 0.8f));
                                line.InsertRetention(new(4, 0.7f));
                                line.InsertRetention(new(6, 0.6f));
                                line.InsertRetention(new(8, 0.5f));
                            }
                        });
                        game.DelayBeat(24, () =>
                        {
                            foreach (Line line in l)
                            {
                                line.AlphaDecrease(game.BeatTime(6), 0.4f);
                                game.DelayBeat(6, () =>
                                {
                                    line.Alpha = 0.55f;
                                    game.DelayBeat(0.125f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(0.25f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(0.375f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(0.75f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(0.875f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.125f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1.5f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.625f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1.75f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.875f, () =>
                                    {
                                        line.Alpha = 0;
                                    });
                                });
                            }
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        ValueEasing.EaseBuilder h = new();
                        h.Insert(0, ValueEasing.Stable(45));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(45, 95, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(95, 135, game.BeatTime(4)));
                        h.Insert(game.BeatTime(3), ValueEasing.EaseOutCubic(135, 0, game.BeatTime(3)));
                        h.Insert(1, ValueEasing.Stable(0));
                        h.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.Gray, 1.2f);
                            ScreenDrawing.DownBoundDistance = s;
                            ScreenDrawing.UpBoundDistance = s;
                        });
                        game.DelayBeat(63, () =>
                        {
                            ScreenDrawing.DownBoundDistance = 0;
                            ScreenDrawing.UpBoundDistance = 0;
                        });
                    });
                }
                public static void rhythm02()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "$0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "D","/","/","/",    "D","/","/","/",    "D","/","/","/",
                        //
                        "R","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "D","/","/","/",    "D","/","/","/",    "D","/","/","/",
                        //
                        "$01","/","/","/",    "+01","/","/","/",    "+01","/","/","/",    "+01","/","/","/",
                        "D1","/","/","/",    "D1","/","/","/",    "D1","/","/","/",    "D1","/","/","/",
                        //
                        "R1","/","/","/",    "+01","/","/","/",    "+01","/","/","/",    "+01","/","/","/",
                        "D1","/","/","/",    "D1","/","/","/",    "D1","/","/","/",    "D1","/","/","/",
                        
                        ////
                        "(R)(+01)","/","/","/",    "+01","/","/","/",    "(R)(+01)","/","/","/",    "+01","/","/","/",
                        "(R)(+01)","/","/","/",    "+01","/","/","/",    "(R)(+01)","/","/","/",    "+01","/","/","/",
                        //
                        "(R)(+01)","/","/","/",    "+01","/","/","/",    "(R)(+01)","/","/","/",    "+01","/","/","/",
                        "(R)(+01)","/","/","/",    "+01","/","/","/",    "(R)(+01)","/","/","/",    "+01(+0)","/","/","/",
                        //
                        "(R)(+01)","/","/","/",    "+01","/","/","/",    "(R)(+01)","/","/","/",    "+01","/","/","/",
                        "(R)(+01)","/","/","/",    "+01","/","/","/",    "(R)(+01)","/","/","/",    "+01","/","/","/",
                        //
                        "(R1)","/","/","/",    "R1","/","/","/",    "(R1)","/","/","/",    "R1","/","/","/",
                        "D","/","+0","/",    "/","/","D","/",    "+0","/","/","/",    "D","/","+0","/",
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
                            instance.CreateArrows(t, 5.5f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect03()
                {
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease = new();
                        ValueEasing.EaseBuilder rot = new();
                        ease.Insert(game.BeatTime(0), CentreEasing.Stable(320, 240));
                        ease.Insert(game.BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(320, 120), game.BeatTime(2)));
                        ease.Insert(game.BeatTime(3.5f), CentreEasing.EaseInQuad(new(320, 120), new(320, 840), game.BeatTime(2.5f)));
                        rot.Insert(game.BeatTime(6), ValueEasing.EaseInCirc(45, -540, game.BeatTime(6)));
                        Line a = new(ease.GetResult(), rot.GetResult()) { Alpha = 0.55f };
                        Line b = new(ease.GetResult(), ValueEasing.Combine(rot.GetResult(), 90)) { Alpha = 0.55f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                lines.AlphaDecrease(game.BeatTime(6), 0.45f);
                                for (int i = 0; i < 24; i++)
                                {
                                    int t = i;
                                    lines.InsertRetention(new(t * 0.5f, 0.24f - t * 0.01f));
                                }
                            }
                            game.DelayBeat(6, () =>
                            {
                                foreach (Line lines in line)
                                {
                                    lines.Dispose();
                                }
                            });
                        });
                    });
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
                    game.RegisterFunctionOnce("TurnR", () =>
                    {
                        DrawingUtil.PlusRotate(-5, game.BeatTime(4));
                    });
                    game.RegisterFunction("convL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7, game.BeatTime(0.6f), false);
                    });
                    game.RegisterFunction("convR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7, game.BeatTime(0.6f), true);
                    });
                    game.RegisterFunctionOnce("TurnL", () =>
                    {
                        DrawingUtil.PlusRotate(5, game.BeatTime(4));
                    });
                    game.RegisterFunctionOnce("linel", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(0, 240), new(640, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("liner", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(640, 240), new(0, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.DelayBeat(30, () =>
                    {
                        for (int i = 0; i < game.BeatTime(3); i++)
                        {
                            DrawingUtil.Linerotate lL = new(0, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lR = new(640, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lU = new(320, -80, 0, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lD = new(320, 480 + 80, 0, game.BeatTime(3), 0.85f);
                            float a = 0;
                            AddInstance(new InstantEvent(i * 1.2f, () =>
                            {
                                CreateEntity(lL);
                                CreateEntity(lR);
                                CreateEntity(lU);
                                CreateEntity(lD);
                                AddInstance(new TimeRangedEvent(30, () =>
                                {
                                    lL.xCenter += 8;
                                    lR.xCenter -= 8;
                                    lU.yCenter += 8;
                                    lD.yCenter -= 8;
                                    lL.alpha -= 0.05f;
                                    lR.alpha -= 0.05f;
                                    lU.alpha -= 0.05f;
                                    lD.alpha -= 0.05f;
                                    lL.rotate -= 5.5f;
                                    lR.rotate -= 5.5f;
                                    lU.rotate -= 5.5f;
                                    lD.rotate -= 5.5f;
                                    a++;
                                }));
                            }));
                        }
                    });
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "(R)(TurnR)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "D","/","/","/",
                        "D","/","/","/",    "D","/","/","/",

                        "(R)(TurnL)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "D","/","/","/",
                        "D","/","/","/",    "D","/","/","/",

                        "(R1)(TurnR)","/","/","/",    "+01","/","/","/",
                        "+01","/","/","/",    "+01","/","/","/",
                        "D1","/","/","/",    "D1","/","/","/",
                        "D1","/","/","/",    "D1","/","/","/",

                        "(R1)(TurnL)","/","/","/",    "+01","/","/","/",
                        "+01","/","/","/",    "+01","/","/","/",
                        "D1","/","/","/",    "D1","/","/","/",
                        "D1","/","/","/",    "D1","/","/","/",
                        ////
                        "($0)(liner)(convR)","/","/","/",    "($2)(liner)(convR)","/","/","/",
                        "($0)(liner)(convR)","/","/","/",    "($2)(liner)(convR)","/","/","/",
                        "($0)(liner)(convR)","/","/","/",    "($2)(liner)(convR)","/","/","/",
                        "($0)(liner)(convR)","/","/","/",    "($2)(liner)(convR)","/","/","/",

                        "($01)(linel)(convL)","/","/","/",    "($21)(linel)(convL)","/","/","/",
                        "($01)(linel)(convL)","/","/","/",    "($21)(linel)(convL)","/","/","/",
                        "($01)(linel)(convL)","/","/","/",    "($21)(linel)(convL)","/","/","/",
                        "($01)(linel)(convL)","/","/","/",    "($21)(linel)(convL)","/","/","/",

                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",

                        "liner","/","linel","/",    "liner","/","linel","/",
                        "liner","/","linel","/",    "liner","/","linel","/",
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
                            game.NormalizedChart(t, 5.5f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                    //vibro&双押
                    float t1 = game.BeatTime(26);
                    string[] rhythm1 =
                    {
                        "$0","/","$0","/","$0","/","$0","/",    "$1","/","$1","/","$1","/","$1","/",
                        "$3","/","$3","/","$3","/","$3","/",    "$2","/","$2","/","$2","/","$2","/",
                        //
                        "/","/","/","/","/","/","/","/",    "/","/","/","/","/","/","/","/",
                         "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",    "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",
                        ////
                    };
                    for (int i = 0; i < rhythm1.Length; i++)
                    {
                        if (rhythm1[i] == "/")
                        {
                            t1 += game.BeatTime(1 / 8f);
                        }
                        else if (rhythm1[i] != "/")
                        {
                            instance.CreateArrows(t1, 5.5f, rhythm1[i]);
                            t1 += game.BeatTime(1 / 8f);
                        }
                    }
                    float t2 = game.BeatTime(26);
                    string[] rhythm2 =
                    {
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        //
                        "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm2.Length; i++)
                    {
                        if (rhythm2[i] == "/")
                        {
                            t2 += game.BeatTime(1 / 6f);
                        }
                        else if (rhythm2[i] != "/")
                        {
                            instance.CreateArrows(t2, 5.5f, rhythm2[i]);
                            t2 += game.BeatTime(1 / 6f);
                        }
                    }
                }
                public static void rhythm04()
                {
                    game.RegisterFunctionOnce("SoulShining", () =>
                    {
                        SetSoul(1);
                    });
                    game.RegisterFunctionOnce("VoidArrow", () =>
                    {
                        Arrow[] a = GetAll<Arrow>();
                        for (int m = 0; m < a.Length; m++)
                        {
                            int x = m;
                            a[x].VoidMode = true;
                        }
                    });
                    game.RegisterFunctionOnce("lA", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(80, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lB", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(160, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lC", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(240, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lD", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(320, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lE", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(400, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lF", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(480, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lG", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(560, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    float t = game.BeatTime(6);
                    string[] rhythm =
                    {//这里就是、、、、、、
                        "VoidArrow","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($0)(lE)","/","/","/",    "($0)(lF)","/","/","/",
                        //
                        "($0)(SoulShining)(lD)(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($1)(lD)","/","/","/",    "($1)(lE)","/","/","/",
                        //
                        "($1)(SoulShining)(lC)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($2)(lC)","/","/","/",    "($2)(lD)","/","/","/",
                        //
                        "($2)(lB)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "($3)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "($0)($0)(SoulShining)(lD)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($1)(lC)(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "($2)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($3)(+0)(SoulShining)(lE)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(+0)(lB)(lF)(lA)(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////这里是吧//对，我开修了、、我写特效（
                        "$1(SoulShining)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$2(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "$3(SoulShining)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lD)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($1)(+0)(SoulShining)(lE)(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($2)(+0)(SoulShining)(lD)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$3(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "$0(SoulShining)(lB)(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "$1(lA)","/","/","/",    "$2(lB)","/","/","/",
                        //
                        "$3(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($1)(+0)(SoulShining)(lB)(lC)(lE)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
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
                        MainEffect.Rain(game.BeatTime(64), 6);
                        float alp = 0.75f;
                        Line a = new(CentreEasing.Stable(new(310, 240)), ValueEasing.Stable(90)) { Alpha = 0.75f };
                        Line b = new(CentreEasing.Stable(new(330, 240)), ValueEasing.Stable(90)) { Alpha = 0.75f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                for (int q = 0; q < game.BeatTime(4); q++)
                                {
                                    AddInstance(new InstantEvent(q * 4 + 2, () =>
                                    {
                                        lines.Alpha = 0.01f;
                                    }));
                                    AddInstance(new InstantEvent(q * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp -= 0.00375f;
                                    }));
                                }
                            }
                        });
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(0, 84, game.BeatTime(4)));
                        builder.Insert(game.BeatTime(14 * 4), ValueEasing.Stable(84));
                        builder.Insert(game.BeatTime(4), ValueEasing.EaseInCubic(84, 0, game.BeatTime(4)));
                        ScreenDrawing.BoundColor = new(95, 137, 154, 60);
                        builder.Run(s => ScreenDrawing.UpBoundDistance = s);
                    });
                    game.DelayBeat(28, () =>
                    {
                        float alp = 0.1f;
                        Line a = new(CentreEasing.Stable(new(320, 225)), ValueEasing.Stable(-45)) { Alpha = 0.75f };
                        Line b = new(CentreEasing.Stable(new(320, 255)), ValueEasing.Stable(-45)) { Alpha = 0.75f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                for (int q = 0; q < 75; q++)
                                {
                                    AddInstance(new InstantEvent(q * 4 + 2, () =>
                                    {
                                        lines.Alpha = 0.01f;
                                    }));
                                }
                                for (int i = 0; i < 37; i++)
                                {
                                    AddInstance(new InstantEvent(i * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp += 0.00375f;
                                    }));
                                    AddInstance(new InstantEvent(148 + i * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp -= 0.00375f;
                                    }));
                                }
                            }
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease = new();
                        ValueEasing.EaseBuilder rot1 = new();
                        ValueEasing.EaseBuilder rot2 = new();
                        ValueEasing.EaseBuilder alp = new();
                        rot1.Insert(0, ValueEasing.Stable(-90 - 720));
                        rot2.Insert(0, ValueEasing.Stable(-90 - 720));
                        ease.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.55f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutQuad(-90 - 720, 45 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutQuart(-90 - 720, 0, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.EaseOutSine(0.55f, 0.25f, game.BeatTime(8)));

                        rot1.Insert(0, ValueEasing.Stable(45 + 45));
                        rot2.Insert(0, ValueEasing.Stable(0));
                        ease.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(45 + 45, 165 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(0, 120, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 + 45, 165 + 65 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120, 120 + 65, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 + 65 + 45, 165 - 84 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 + 65, 120 - 84, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 - 84 + 45, 165 - 84 - 58 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 - 84, 120 - 84 - 58, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 - 84 - 58 + 45, 165 - 84 - 58 + 45 - 72, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 - 84 - 58, 120 - 84 - 58 - 72, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(-49 + 45, -49 + 45 - 66, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(-94, -94 - 66, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseInCirc(-49 + 45 - 66, -90, game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseInCirc(-94 - 66, -90, game.BeatTime(4)));
                        ease.Insert(game.BeatTime(4), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.EaseOutQuad(0.25f, 0, game.BeatTime(8)));
                        Line a = new(ease.GetResult(), rot1.GetResult());
                        Line b = new(ease.GetResult(), rot2.GetResult());
                        Line[] l = { a, b };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            lines.Alpha = 0.55f;
                            alp.Run((s) => { lines.Alpha = s; });
                            game.DelayBeat(8, () =>
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    lines.InsertRetention(new(i * 3 + 3, 0.32f - i * 0.03f));
                                }
                            });
                            game.DelayBeat(64, () =>
                            {
                                lines.Dispose();
                            });
                        }
                    });
                    game.DelayBeat(60, () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        CentreEasing.EaseBuilder ce2 = new();
                        CentreEasing.EaseBuilder ce3 = new();
                        CentreEasing.EaseBuilder ce4 = new();
                        ValueEasing.EaseBuilder alp = new();
                        ce1.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce2.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce3.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce4.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.1f));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 - 126, 240), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 - 42, 240), game.BeatTime(2)));
                        ce3.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 + 42, 240), game.BeatTime(2)));
                        ce4.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 + 126, 240), game.BeatTime(2)));
                        alp.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(0.1f, 0.45f, game.BeatTime(2)));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 - 126, 240), new(320, 240), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 - 42, 240), new(320, 240), game.BeatTime(2)));
                        ce3.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 + 42, 240), new(320, 240), game.BeatTime(2)));
                        ce4.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 + 126, 240), new(320, 240), game.BeatTime(2)));
                        alp.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(0.45f, 0, game.BeatTime(2)));
                        Line a = new(ce1.GetResult(), (s) => { return 90; });
                        Line b = new(ce2.GetResult(), (s) => { return 90; });
                        Line c = new(ce3.GetResult(), (s) => { return 90; });
                        Line d = new(ce4.GetResult(), (s) => { return 90; });
                        Line[] l = { a, b, c, d };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            alp.Run(s => lines.Alpha = s);
                            game.DelayBeat(4, () => { lines.Dispose(); });
                        }
                    });
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
                        Heart.InstantSetRotation(180);
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
                        Heart.InstantSetRotation(-180);
                        TP(new Vector2(320 + 126, 480));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$S", new Vector2(316 - 126, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$D", new Vector2(316 - 42, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$Left", new Vector2(300 + 42, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$Down", new Vector2(308 + 126, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
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
                    game.DelayBeat(62, () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        CentreEasing.EaseBuilder ce2 = new();
                        ce1.Insert(0, CentreEasing.Stable(new(0, 0)));
                        ce2.Insert(0, CentreEasing.Stable(new(640, 0)));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(0, 0), new(640, 640), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(640, 0), new(0, 640), game.BeatTime(2)));
                        Line a = new(ce1.GetResult(), (s) => { return -45; }) { Alpha = 0.45f };
                        Line b = new(ce2.GetResult(), (s) => { return 45; }) { Alpha = 0.45f };
                        Line[] l = { a, b };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            for (int i = 0; i < 8; i++)
                            {
                                lines.InsertRetention(new(i * 4 + 4, 0.55f - i * 0.03f));
                            }
                            game.DelayBeat(2, () => { lines.Dispose(); });
                        }
                    });
                }
                public static void rhythm05()
                {
                    game.RegisterFunctionOnce("LD", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                    });
                    game.RegisterFunctionOnce("LU", () =>
                    {

                    });
                    game.RegisterFunctionOnce("RD", () =>
                    {

                    });
                    game.RegisterFunctionOnce("RU", () =>
                    {

                    });
                    game.RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UIColor = new Color(20, 20, 20);
                        ScreenDrawing.UISettings.CreateUISurface();
                        float camRotation = 43;
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(0, 0), new(0, 50), game.BeatTime(2)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

                        RenderProduction production4 = new ScreenDrawing.Shaders.Filter(game.Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(production4);

                        ValueEasing.EaseBuilder camera = new();
                        game.Effect3D.TextureSize = new(640, 1280 * (1 + ScreenDrawing.UpExtending));
                        game.Effect3D.ProjectPointOffect = new(320, game.Effect3D.TextureSize.Y);
                        game.Effect3D.ProjectPoint = new(0, 0, 200);
                        camera.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(0, camRotation, game.BeatTime(2)));
                        camera.Run(rotation =>
                        {
                            game.Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            game.Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            game.Effect3D.CameraPosition = new(0, -900 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    game.RegisterFunctionOnce("StepStart", () =>
                    {
                        game.StepSample.CentreX = 320f;
                        game.StepSample.CentreY = 240f;
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;

                        });
                    });
                    game.RegisterFunctionOnce("Step", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.1f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0.1f, 0.3f, game.BeatTime(1)));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.3f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                            game.splitter.Intensity = s * 30f - 0.1f * 30f;
                        });
                    });
                    game.RegisterFunctionOnce("StepOver", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.1f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.1f, 0f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                        });
                    });
                    float t = game.BeatTime(4);
                    string[] rhythm =
                    {
                        "($3)(^+01'1.4)(StepStart)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(R)(^+01'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(R)(^+01'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "R","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(R)(^+01'1.4)(Step)","/","/","/",    "R","/","+0","/",
                        "","/","R","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "/","/","/","/",
                        ////
                        "(^R'1.4)(+01)(Step)","/","/","/",    "R1","/","/","/",
                        "/","/","/","/",    "R1","/","/","/",
                        "R1","/","","/",    "/","/","/","/",
                        "R1","/","/","/",    "R1","/","/","/",
                        //                     
                        "(^R'1.4)(+01)(Step)","/","/","/",    "R1","/","/","/",
                        "/","/","/","/",    "R1","/","/","/",
                        "R1","/","","/",    "","/","/","/",
                        "(R1)","/","/","/",    "(R1)","/","/","/",
                        //
                        "(^R'1.4)(+01)(Step)","/","/","/",    "R1","/","/","/",
                        "R1","/","+01","/",    "/","/","R1","/",
                        "+01","/","+01","/",    "/","/","","/",
                        "R1","/","","/",    "R1","/","/","/",
                        //
                        "(^R'1.4)(+01)(Step)","/","/","/",    "R1","/","+01","/",
                        "","/","R1","/",    "+01","/","/","/",
                        "R1","/","+01","/",    "+01","/","/","/",
                        "R1","/","/","/",    "+01","/","/","/",
                        ////
                        "(N21)(#2#$2)(Step)","/","/","/",    "N21","/","","/",
                        "/","/","/","/",    "N21","/","/","/",
                        "(N21)(^$2'1.2)","/","","/",    "/","/","/","/",
                        "N21","/","/","/",    "N21","/","/","/",
                        //
                        "(N01)(#2#$0)(Step)","/","/","/",    "N01","/","","/",
                        "/","/","/","/",    "N01","/","/","/",
                        "(N01)(^$0'1.2)","/","","/",    "/","/","/","/",
                        "N01","/","/","/",    "N01","/","/","/",
                        //
                        "(#1#$21)(+0)(Step)","/","/","/",    "","/","","/",
                        "(^$21'1.2)(^$2'1.2)","/","/","/",    "","/","/","/",
                        "(#1#$0)(+01)(Step)","/","/","/",    "","/","","/",
                        "(^$0'1.2)(^$01'1.2)","/","/","/",    "","/","/","/",
                        //
                        "(R)(+01)(Step)","/","/","/",    "R","/","+0","/",
                        "/","/","R","/",    "+0","/","/","/",
                        "(R)","/","+0","/",    "+0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        ////
                        "(N21)(#2#$2)(Step)","/","/","/",    "N21","/","","/",
                        "/","/","/","/",    "N21","/","/","/",
                        "(N21)(^$2'1.2)","/","","/",    "/","/","/","/",
                        "N21","/","/","/",    "N21","/","/","/",
                        //
                        "(N01)(#2#$0)(Step)","/","/","/",    "N01","/","","/",
                        "/","/","/","/",    "N01","/","/","/",
                        "(N01)(^$0'1.2)(Step)","/","","/",    "/","/","/","/",
                        "N01","/","/","/",    "N01","/","/","/",
                        //
                        "(#1#$21)(+0)(Step)","/","/","/",    "$21","/","$2","/",
                        "","/","$21","/",    "$2","/","/","/",
                        "(#1#$0)(+01)(Step)","/","","/",    "$01","/","$0","/",
                        "","/","$01","/",    "$0","/","$01","/",
                        //
                        "($1)(+01)(StepOver)","/","/","/",    "R1","/","+01","/",
                        "/","/","R1","/",    "+01","/","/","/",
                        "(R1)","/","+01","/",    "+01","/","/","/",
                        "(<$3'0.2)(>$31'0.2)","/","/","/",    "","/","/","/",
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

                    });
                    game.DelayBeat(0, () =>
                    {

                    });
                }
                public static void rhythm06A()
                {
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
                    game.StepSample.CentreY = -800f;
                    game.DelayBeat(2, () =>
                    {


                        #region Split
                        Heart.Split();
                        SetBoxMission(0);
                        MainEffect.SoftSetBox(new(320 - 126, 480 - 84), 84, 84, game.BeatTime(1), 0, 1);
                        SetPlayerMission(0);
                        Heart.Alpha = 0.2f;

                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(0); Heart.Shields.RemoveShield(Heart.Shields.BShield); });
                        Heart.InstantSetRotation(180);
                        MainEffect.SoftTP(new(320 - 126, 480 - 84), game.BeatTime(1), 0, 1);

                        Heart.Split();
                        SetBoxMission(1);
                        MainEffect.SoftSetBox(new(320 - 42, 480 - 84), 84, 84, game.BeatTime(1), 1, 1);
                        SetPlayerMission(2);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(2); Heart.Shields.RemoveShield(Heart.Shields.RShield); });
                        Heart.InstantSetRotation(90);
                        MainEffect.SoftTP(new(320 + 42, 480 - 84), game.BeatTime(1), 2, 1);
                        SetBoxMission(2);
                        MainEffect.SoftSetBox(new(320 + 42, 480 - 84), 84, 84, game.BeatTime(1), 2, 1);
                        SetPlayerMission(1);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(1); Heart.Shields.RemoveShield(Heart.Shields.BShield); });
                        Heart.InstantSetRotation(-90);
                        MainEffect.SoftTP(new(320 - 42, 480 - 84), game.BeatTime(1), 1, 1);

                        Heart.Split();
                        SetBoxMission(3);
                        MainEffect.SoftSetBox(new(320 + 126, 480 - 84), 84, 84, game.BeatTime(1), 3, 1);
                        SetPlayerMission(3);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(3); Heart.Shields.RemoveShield(Heart.Shields.RShield); });
                        Heart.InstantSetRotation(-180);
                        MainEffect.SoftTP(new(320 + 126, 480 - 84), game.BeatTime(1), 3, 1);
                        #endregion
                        #region Line
                        for (int a = 0; a < 5; a++)
                        {
                            Line l1 = new(new Vector2(320 - 126 - 42 + a * 84, 480), new Vector2(320 - 126 - 42 + a * 84, -800)) { Alpha = 0 };
                            CreateEntity(l1);
                            l1.AlphaIncrease(game.BeatTime(2), 0.5f);
                        }
                        Line l2 = new(new Vector2(320 - 126 - 42, 480 - 84 - 42), new Vector2(320 - 126 - 42 + 4 * 84, 480 - 84 - 42)) { Alpha = 0 };
                        CreateEntity(l2);
                        l2.AlphaIncrease(game.BeatTime(2), 1f);
                        #endregion
                    });
                    #region 
                    game.RegisterFunctionOnce("k0", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 24f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k1", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 24f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k2", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 24f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k3", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 24f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunction("Lk0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 1, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 0, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Rk0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 1, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 0, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Step", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.2f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0.1f, 0.3f, game.BeatTime(1)));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.3f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.8f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                        });
                    });
                    game.RegisterFunction("Polor", () =>
                    {
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.8f;

                        });
                        ValueEasing.EaseBuilder e2 = new();
                        e2.Insert(0, ValueEasing.Stable(0f));
                        e2.Insert(game.BeatTime(96), ValueEasing.SinWave(0.1f, game.BeatTime(8), 0));
                        e2.Insert(1, ValueEasing.Stable(0));
                        e2.Run((s) =>
                        {

                            game.Polar.Intensity = s * 0.07f;
                            game.Polar.IType = false;
                        });
                    });
                    #endregion
                    #region UI
                    Color uic = ScreenDrawing.UIColor;
                    ValueEasing.EaseBuilder scale = new();
                    scale.Insert(0, ValueEasing.Stable(1));
                    scale.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(1, 0.1f, game.BeatTime(4)));
                    scale.Run(scale => { ScreenDrawing.UIColor = uic * scale; ScreenDrawing.HPBar.HPExistColor = new Color(90, 112, 125) * scale; ScreenDrawing.HPBar.HPLoseColor = Color.Gray * scale; });
                    #endregion
                    ValueEasing.EaseBuilder len = new();
                    len.Insert(game.BeatTime(64), ValueEasing.Stable((4 * 84)));
                    len.Insert(game.BeatTime(1), ValueEasing.EaseInSine(4 * 84, 6 * 84, game.BeatTime(1)));
                    Line l = new(CentreEasing.Stable(320, 480 - 84 - 42), ValueEasing.Stable(0), len.GetResult()) { Depth = 0.99f, Alpha = 0 };
                    CreateEntity(l);
                    l.AlphaIncrease(game.BeatTime(8), 1);
                    game.BarrageCreate(0, game.BeatTime(1), 7.5f, new string[]
                    {
                        "(Polor)","","","",    "","","","",
                        "","","","",    "","","","",
                        "k2","","k0","",    "k1","","k2","",
                        "k3(Step)","","k1","",    "k2","","k0","",

                        "(k2)(k3)","","k0","",    "k1","","k2","",
                        "k3","","k1","",    "k2","","k0","",
                        "k3","","k2","",    "k1","","k0","",
                        "k1(Step)","","k3","",    "k2","","k1","",

                        "(k2)(k3)","","k1","",   "k2","","k0","",
                        "k1","","k2","",   "k3","","k2","",
                        "k0","","k1","",   "k2","","k3","",
                        "k2(Step)","","k0","",   "k1","","k2","",

                        "!!3/6","k0","k1","k2","k3","k2","k1",
                        "!!3/6","k0","k1","k2","k3","k2","k1",
                        "!!3/6","k0","k1","k2","k3","k2","k1",
                        "!!3/6","k0(Step)","k1","k2","k3","k0","k1",

                        "(k2)(k3)","","k0","",   "k1","","k2","",
                        "k3","","k0","",   "k1","","k3","",
                        "k2","","k1","",   "k0","","k1","",
                        "k3(Step)","","k2","",   "k0","","k1","",

                        "(k2)(k3)","","k0","",   "k1","","k2","",
                        "k3","","k2","",   "k0","","k1","",
                        "k2","","k3","",   "k0","","k1","",
                        "k2(Step)","","k1","",   "k3","","k2","",

                        "!!3/6","k0","k1","k2","k3","k0","k1",
                        "!!3/6","k2","k3","k0","k1","k2","k3",
                        "!!3/6","k2","k1","k0","k3","k2","k1",
                        "!!3/6","k0(Step)","k3","k2","k1","k0","k1",

                        "!!3/6","k3","k2","k0","k1","k3","k2",
                        "!!3/6","k0","k1","k3","k2","k0","k1",
                        "!!3/6","k2","k3","k1","k0","k2","k3",
                        "!!3/6","k1(Step)","k0","k2","k3","k2","k1",

                        "(k2)(k3)","","k0","",   "k1","","k2","",
                        "k3","","k0","",   "k1","","k2","",
                        "k3","","k2","",   "k0","","k1","",
                        "k2(Step)","","k1","",   "k3","","k2","",

                        "(k0)(k1)","","k3","",   "k2","","k1","",
                        "k2","","k0","",   "k1","","k2","",
                        "k3","","k0","",   "k3","","k2","",
                        "k1(Step)","","k0","",   "k1","","k0","",

                        "(k2)(k3)","","k0","",   "k1","","k2","",
                        "k3","","k2","",   "k0","","k1","",
                        "k2","","k3","",   "k2","","k3","",
                        "k2(Step)","","k3","",   "k2","","k1","",

                        "k2","","k3","",   "k0","","k1","",
                        "k2","","k3","",   "k0","","k1","",
                        "k2","","k1","",   "k3","","k2","",
                        "!!3/6","k0(Step)","k1","k2","k3","k0","k1",

                        "(k3)(k2)","","k1","",   "k0","","k1","",
                        "k0","","k1","",   "k0","","k1","",
                        "(k0)(k3)","","k2","",   "(k1)(k3)","","k2","",
                        "(k0)(k3)(Step)","","k2","",   "(k1)(k3)","","k2","",

                        "k0","","k1","",   "(k0)(k3)","","k1","",
                        "(k0)(k2)","","k1","",   "(k0)(k3)","","k1","",
                        "k3","","k2","",   "k1","","k0","",
                        "k1(Step)","","k3","",   "k2","","k0","",

                        "(k2)(k3)","","k0","",   "k1","","k2","",
                        "k3","","k2","",   "k0","","k1","",
                        "(k0)(k2)","","k3","",   "(k1)(k2)","","k3","",
                        "(k0)(k2)(Step)","","k3","",   "(k1)(k2)","","k3","",

                        "k0","","k3","",   "k1","","k2","",
                        "k0","","k3","",   "k1","","k2","",
                        "k3","","k0","",   "k2","","k1","",
                        "k3(Step)","","k0","",   "k2","","k1","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });

                    game.DelayBeat(60, () => { l.AlphaDecrease(game.BeatTime(4)); });

                }
                public static void _6k()
                {
                    game.To6KIn3D();
                    Player.Heart ExHeart1 = CreateHeart(new CollideRect(320 - 168, 480 - 84, 84, 84));
                    Player.Heart ExHeart2 = CreateHeart(new CollideRect(320 + 168, 480 - 84, 84, 84));
                    game.RegisterFunctionOnce("k0", () =>
                    {
                        SetPlayerMission(ExHeart1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 24f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k1", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 24f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k2", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 24f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k3", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 24f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k4", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 24f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k5", () =>
                    {
                        SetPlayerMission(ExHeart2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 24f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    SetPlayerBoxMission(ExHeart1);
                    MainEffect.SoftTP(new(320 - 126 - 84, 480 - 84), game.BeatTime(3), 4, 1);
                    MainEffect.SoftSetBox(new(320 - 126 - 84, 480 - 84), 84, 84, game.BeatTime(3), 4, 1);
                    ExHeart1.InstantSetRotation(90);
                    SetSoul(1);
                    game.DelayBeat(0, () => { ExHeart1.Shields.RemoveShield(ExHeart1.Shields.BShield); });
                    SetPlayerBoxMission(ExHeart2);
                    MainEffect.SoftTP(new(320 + 126 + 84, 480 - 84), game.BeatTime(3), 5, 1);
                    MainEffect.SoftSetBox(new(320 + 126 + 84, 480 - 84), 84, 84, game.BeatTime(3), 5, 1);
                    ExHeart2.InstantSetRotation(-90);
                    SetSoul(1);
                    game.DelayBeat(0, () => { ExHeart2.Shields.RemoveShield(ExHeart2.Shields.RShield); });
                    Line l = new(new Vector2(320 - 3 * 84, 480), new Vector2(320 - 3 * 84, -800)) { DrawingColor = ScreenDrawing.ThemeColor, Alpha = 0 };
                    CreateEntity(l);
                    l.AlphaIncrease(game.BeatTime(1), 0.5f);
                    Line l2 = new(new Vector2(320 + 3 * 84, 480), new Vector2(320 + 3 * 84, -800)) { DrawingColor = ScreenDrawing.ThemeColor, Alpha = 0 };
                    CreateEntity(l2);
                    l2.AlphaIncrease(game.BeatTime(1), 0.5f);
                    game.BarrageCreate(0, game.BeatTime(1), 8, new string[]
                    {
                        "(k3)(k4)","","k2","",   "k1","","k2","",
                        "(k3)(k4)","","k2","",   "k1","","k0","",
                        "(k1)(k2)","","k3","",   "k4","","k3","",
                        "(k1)(k2)","","k3","",   "k2","","k1","",

                        "(k2)(k3)","","k4","",   "k3","","k2","",
                        "(k0)(k1)","","k2","",   "k3","","k4","",
                        "(k2)(k3)","","k1","",   "k2","","k3","",
                        "(k4)(k5)","","k0","",   "k1","","k2","",

                        "(k0)(k1)","","k2","",   "k3","","k4","",
                        "(k2)(k3)","","k1","",   "k2","","k3","",
                        "(k4)(k5)","","k3","",   "k4","","k2","",
                        "(k0)(k1)","","k2","",   "k3","","k4","",

                        "(k2)(k3)","","k1","",   "k0","","k1","",
                        "!!3/6","k2","k1","k0","k1","k2","k3",
                        "!!3/6","k4","k5","k4","k3","k2","k1",
                        "k2","","k3","",   "k4","","k3","",
                        //
                        "(k0)(k1)","","k3","",   "k2","","k1","",
                        "(k2)(k3)","","k1","",   "k2","","k3","",
                        "(k1)(k2)","","k3","",   "k4","","k3","",
                        "(k0)(k1)","","k2","",   "k3","","k2","",

                        "(k3)(k4)","","k5","",   "k4","","k5","",
                        "(k3)(k4)","","k1","",   "k2","","k1","",
                        "(k2)(k3)","","k4","",   "k3","","k2","",
                        "(k0)(k1)","","k2","",   "k1","","k3","",

                        "(k4)(k5)","","k1","",   "k2","","k3","",
                        "(k4)(k5)","","k3","",   "k2","","k1","",
                        "(k2)(k3)","","k4","",   "k3","","k2","",
                        "(k0)(k1)","","k3","",   "k2","","k1","",

                        "(k2)(k3)","","k0","",   "k1","","k2","",
                        "!!3/12","(k0)","k1","k2","k3","k4","k5",
                        "k0","k1","k2","k3","k4","k5",
                        "k0",
                        //
                    });


                    //test
                    game.DelayBeat(34, game.Apply2D);
                }
                public static void over()
                {
                    SetPlayerMission(0);
                    game.RegisterFunctionOnce("Rot", () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(360, game.BeatTime(10));
                    });
                    game.RegisterFunctionOnce("linel", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(0, 240), new(640, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("liner", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(640, 240), new(0, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("Scale", () =>
                    {
                        for (int i = 0; i < game.BeatTime(1.5f); i++)
                        {
                            DrawingUtil.Linerotate lL = new(0, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lR = new(640, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lU = new(320, -80, 0, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lD = new(320, 480 + 80, 0, game.BeatTime(3), 0.85f);
                            float a = 0;
                            AddInstance(new InstantEvent(i * 2.4f, () =>
                            {
                                CreateEntity(lL);
                                CreateEntity(lR);
                                CreateEntity(lU);
                                CreateEntity(lD);
                                AddInstance(new TimeRangedEvent(30, () =>
                                {
                                    lL.xCenter += 8;
                                    lR.xCenter -= 8;
                                    lU.yCenter += 8;
                                    lD.yCenter -= 8;
                                    lL.alpha -= 0.05f;
                                    lR.alpha -= 0.05f;
                                    lU.alpha -= 0.05f;
                                    lD.alpha -= 0.05f;
                                    lL.rotate -= 5.5f;
                                    lR.rotate -= 5.5f;
                                    lU.rotate -= 5.5f;
                                    lD.rotate -= 5.5f;
                                    a++;
                                }));
                            }));
                        }
                        float p = 0;
                        game.ForBeat(4, () =>
                        {
                            Heart.InstantSetRotation(p * p * 0.08f);
                            ScreenDrawing.ScreenScale = p * 0.02f + 1;
                            p++;
                        });
                    });
                    game.RegisterFunctionOnce("ScaleBack", () =>
                    {
                        DrawingUtil.LerpScreenScale(game.BeatTime(16), 1, 0.03f);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Heart.Rotation % 360));
                        ve.Insert(game.BeatTime(32), ValueEasing.EaseOutSine(Heart.Rotation % 360, 360 * 2, game.BeatTime(32)));
                        ve.Run((s) => { Heart.InstantSetRotation(s); });
                    });
                    game.RegisterFunctionOnce("End", () =>
                    {
                        game.End();
                    });
                    game.BarrageCreate(0, game.BeatTime(1), 6.5f, new string[]
                    {
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "$01","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",

                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",

                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        //
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",

                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",

                        "($0)($2)(linel)","","$31'1.3","",   "($0)($2)(liner)","","$31'1.3","",
                        "($0)($2)(linel)","","$11'1.3","",   "($0)($2)(liner)","","$11'1.3","",
                        "($0)($2)(linel)","","$31'1.3","",   "($0)($2)(liner)","","$11'1.3","",
                        "($0)($2)(linel)","","$31'1.3","",   "($0)($2)(liner)","","$11'1.3","",

                        "($0)($2)(linel)(Scale)","","(<$01'1.3)(liner)","",   "($0)($2)(linel)","","(<$21'1.5)(liner)","",
                        "($0)($2)(linel)","","(<$01'1.3)(liner)","",   "($0)($2)(linel)","","(<$21'1.5)(liner)","",
                        "!!3/6","(<$0'1.3)(<$2'1.3)","(>$01)(>$21)","(<$0'1.3)(<$2'1.3)","(>$01)(>$21)","(<$0'1.3)(<$2'1.3)","(>$01)(>$21)",
                        "!!3/6","(<$0'1.3)(<$2'1.3)","(>$01)(>$21)","(<$0'1.3)(<$2'1.3)","(>$01)(>$21)","(<$0'1.3)(<$2'1.3)","(>$01)(>$21)",

                        "(<$0'1.3)(<$2'1.3)(ScaleBack)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "End","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                    game.BarrageCreate(0, game.BeatTime(1), 9, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(convL)","","","",   "(convL)","","","",

                        "","","","",   "","","","",
                        "","","","",   "(convR)","","","",
                        "(convR)","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "$0(convL)","","+2","",   "+2(convL)","","+2","",

                        "+2","","","",   "","","","",
                        "","","","",   "$2(convR)","","+2","",
                        "+2(convR)","","+2","",   "+2","","","",
                        "","","","",   "","","","",
                        //
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "$0(convL)","","+2","",   "+2(convL)","","+2","",

                        "+2","","","",   "","","","",
                        "","","","",   "$2(convR)","","+2(convR)","",
                        "+2","","+2","",   "+2","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                }
            }
            private static class NorBarrage
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
                        ValueEasing.EaseBuilder rot = new();
                        rot.Insert(game.BeatTime(4), ValueEasing.EaseInBack(0, 120, game.BeatTime(4)));
                        rot.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
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
                            ScreenDrawing.ScreenAngle = 0;
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        float alp = 0.75f;
                        DrawingUtil.NormalLine a = new(0, Rand(150, 195), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine b = new(0, Rand(195, 240), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine c = new(0, Rand(240, 285), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine d = new(0, Rand(285, 330), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine e = new(0, Rand(105, 150), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine f = new(0, Rand(330, 375), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        CreateEntity(a);
                        CreateEntity(b);
                        CreateEntity(c);
                        CreateEntity(d);
                        CreateEntity(e);
                        CreateEntity(f);
                        for (int i = 0; i < game.BeatTime(24); i++)
                        {
                            AddInstance(new InstantEvent(i * 4, () =>
                            {
                                a.alpha = alp;
                                b.alpha = alp;
                                c.alpha = alp;
                                d.alpha = alp;
                                e.alpha = alp;
                                f.alpha = alp;
                                alp -= 0.0075f;
                            }));
                            AddInstance(new InstantEvent(i * 4 + 2, () =>
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
                    game.DelayBeat(8, () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuad(new(320, 240), new(160, 240), game.BeatTime(4)), (s) => { return 45; }) { Alpha = 0.85f };
                        a.AlphaDecrease(game.BeatTime(4), 0.85f);
                        a.InsertRetention(new(8, 0.75f));
                        Line b = new(CentreEasing.EaseOutQuad(new(320, 240), new(480, 240), game.BeatTime(4)), (s) => { return 45; }) { Alpha = 0.85f };
                        b.AlphaDecrease(game.BeatTime(4), 0.85f);
                        b.InsertRetention(new(8, 0.75f));
                        CreateEntity(a);
                        CreateEntity(b);
                    });
                    game.DelayBeat(16, () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuad(new(320, 240), new(160, 240), game.BeatTime(4)), (s) => { return -45; }) { Alpha = 0.85f };
                        a.AlphaDecrease(game.BeatTime(4), 0.85f);
                        a.InsertRetention(new(8, 0.75f));
                        Line b = new(CentreEasing.EaseOutQuad(new(320, 240), new(480, 240), game.BeatTime(4)), (s) => { return -45; }) { Alpha = 0.85f };
                        b.AlphaDecrease(game.BeatTime(4), 0.85f);
                        b.InsertRetention(new(8, 0.75f));
                        CreateEntity(a);
                        CreateEntity(b);
                    });
                    game.DelayBeat(20, () =>
                    {
                        CentreEasing.EaseBuilder ease1 = new();
                        CentreEasing.EaseBuilder ease2 = new();
                        CentreEasing.EaseBuilder ease3 = new();
                        CentreEasing.EaseBuilder ease4 = new();
                        ValueEasing.EaseBuilder rota = new();
                        ValueEasing.EaseBuilder rotb = new();
                        ease1.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(160, 240), game.BeatTime(2)));
                        ease2.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(480, 240), game.BeatTime(2)));
                        ease3.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(2)));
                        ease4.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(2)));
                        rota.Insert(game.BeatTime(4), ValueEasing.Stable(0));
                        rotb.Insert(game.BeatTime(4), ValueEasing.Stable(0));
                        ease1.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(160, 240), new(320, 240), game.BeatTime(2)));
                        ease2.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(480, 240), new(320, 240), game.BeatTime(2)));
                        ease3.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(240, 240), new(320, 240), game.BeatTime(2)));
                        ease4.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(400, 240), new(320, 240), game.BeatTime(2)));
                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(4)));
                        ease3.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(4)));
                        ease4.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(4)));
                        rota.Insert(game.BeatTime(4), ValueEasing.EaseOutQuart(0, 45, game.BeatTime(4)));
                        rotb.Insert(game.BeatTime(4), ValueEasing.EaseOutQuart(0, -45, game.BeatTime(4)));
                        Line a = new(ease1.GetResult(), ValueEasing.Combine(rota.GetResult(), 90)) { Alpha = 0.85f };
                        Line b = new(ease2.GetResult(), ValueEasing.Combine(rota.GetResult(), 90)) { Alpha = 0.85f };
                        Line c = new(ease3.GetResult(), ValueEasing.Combine(rotb.GetResult(), 90)) { Alpha = 0.85f };
                        Line d = new(ease4.GetResult(), ValueEasing.Combine(rotb.GetResult(), 90)) { Alpha = 0.85f };
                        a.InsertRetention(new(4, 0.55f));
                        b.InsertRetention(new(4, 0.55f));
                        c.InsertRetention(new(4, 0.55f));
                        d.InsertRetention(new(4, 0.55f));
                        Line[] lines = { a, b, c, d };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line line in lines)
                            {
                                CreateEntity(line);
                                line.AlphaDecrease(game.BeatTime(2), 0.75f);
                            }
                        });
                        game.DelayBeat(2, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaIncrease(game.BeatTime(2), 0.75f);
                            }
                        });
                        game.DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(game.BeatTime(4), 0.85f);
                            }
                        });
                    });
                }
                public static void rhythm01()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "(R)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        //
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        //
                        "(R)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        //
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "R","/","/","/",    "/","/","/","/",
                        ////
                        "(R)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "(R)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
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
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease1 = new();
                        CentreEasing.EaseBuilder ease2 = new();
                        ValueEasing.EaseBuilder rot1 = new();
                        ValueEasing.EaseBuilder rot2 = new();
                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(640, 0), new(120, 360), game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(30));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(0, 240)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(90));

                        ease1.Insert(game.BeatTime(4), CentreEasing.Stable(new(120, 360)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(30, -135, game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(0, 240), new(480, 240), game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(90));

                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(120, 360), new(440, 180), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(480, 240)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(90, -45, game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(-135));

                        ease1.Insert(game.BeatTime(4), CentreEasing.Stable(new(440, 180)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(-135, 45, game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(480, 240), new(200, 180), game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(-45));

                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(440, 180), new(320, 240), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(200, 180)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(-45, 135, game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(45));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(135));

                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(200, 180), new(320, 240), game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(45, -135, game.BeatTime(4)));
                        ease1.Insert(game.BeatTime(12), CentreEasing.Stable(new(320, 240)));
                        ease2.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));

                        rot1.Insert(game.BeatTime(6), ValueEasing.EaseOutBack(-135, -360, game.BeatTime(6)));
                        rot2.Insert(game.BeatTime(6), ValueEasing.EaseOutBack(135, -90, game.BeatTime(6)));
                        rot1.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-360, -315, game.BeatTime(0.75f)));
                        rot2.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-90, -45, game.BeatTime(0.75f)));
                        rot1.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-315, -270, game.BeatTime(0.75f)));
                        rot2.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-45, 0, game.BeatTime(0.75f)));
                        rot1.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutExpo(-270, -225, game.BeatTime(0.5f)));
                        rot2.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutExpo(0, 45, game.BeatTime(0.5f)));
                        Line l1 = new(ease1.GetResult(), rot1.GetResult()) { Alpha = 0.55f };
                        Line l2 = new(ease2.GetResult(), rot2.GetResult()) { Alpha = 0.55f };
                        Line[] l = { l1, l2 };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line line in l)
                            {
                                CreateEntity(line);
                                line.InsertRetention(new(2, 0.8f));
                                line.InsertRetention(new(4, 0.7f));
                                line.InsertRetention(new(6, 0.6f));
                                line.InsertRetention(new(8, 0.5f));
                            }
                        });
                        game.DelayBeat(24, () =>
                        {
                            foreach (Line line in l)
                            {
                                line.AlphaDecrease(game.BeatTime(6), 0.4f);
                                game.DelayBeat(6, () =>
                                {
                                    line.Alpha = 0.55f;
                                    game.DelayBeat(0.125f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(0.25f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(0.375f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(0.75f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(0.875f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.125f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1.5f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.625f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1.75f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.875f, () =>
                                    {
                                        line.Alpha = 0;
                                    });
                                });
                            }
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        ValueEasing.EaseBuilder h = new();
                        h.Insert(0, ValueEasing.Stable(45));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(45, 95, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(95, 135, game.BeatTime(4)));
                        h.Insert(game.BeatTime(3), ValueEasing.EaseOutCubic(135, 0, game.BeatTime(3)));
                        h.Insert(1, ValueEasing.Stable(0));
                        h.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.Gray, 1.2f);
                            ScreenDrawing.DownBoundDistance = s;
                            ScreenDrawing.UpBoundDistance = s;
                        });
                        game.DelayBeat(63, () =>
                        {
                            ScreenDrawing.DownBoundDistance = 0;
                            ScreenDrawing.UpBoundDistance = 0;
                        });
                    });
                }
                public static void rhythm02()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "$0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "R","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "R","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        //
                        "R","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",    "R","/","/","/",    "R","/","/","/",
                        
                        ////
                        "$0","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "R","/","/","/",    "D","/","/","/",    "R","/","/","/",
                        //
                        "D","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "R","/","/","/",    "D","/","/","/",    "D","/","/","/",
                        //
                        "D","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "R","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        //
                        "(D)","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        "R","/","+0","/",    "/","/","+0","/",    "+0","/","/","/",    "+0","/","+0","/",
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
                            instance.CreateArrows(t, 5.5f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect03()
                {
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease = new();
                        ValueEasing.EaseBuilder rot = new();
                        ease.Insert(game.BeatTime(0), CentreEasing.Stable(320, 240));
                        ease.Insert(game.BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(320, 120), game.BeatTime(2)));
                        ease.Insert(game.BeatTime(3.5f), CentreEasing.EaseInQuad(new(320, 120), new(320, 840), game.BeatTime(2.5f)));
                        rot.Insert(game.BeatTime(6), ValueEasing.EaseInCirc(45, -540, game.BeatTime(6)));
                        Line a = new(ease.GetResult(), rot.GetResult()) { Alpha = 0.55f };
                        Line b = new(ease.GetResult(), ValueEasing.Combine(rot.GetResult(), 90)) { Alpha = 0.55f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                lines.AlphaDecrease(game.BeatTime(6), 0.45f);
                                for (int i = 0; i < 24; i++)
                                {
                                    int t = i;
                                    lines.InsertRetention(new(t * 0.5f, 0.24f - t * 0.01f));
                                }
                            }
                            game.DelayBeat(6, () =>
                            {
                                foreach (Line lines in line)
                                {
                                    lines.Dispose();
                                }
                            });
                        });
                    });
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
                    game.RegisterFunctionOnce("TurnR", () =>
                    {
                        DrawingUtil.PlusRotate(-5, game.BeatTime(4));
                    });
                    game.RegisterFunction("convL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7, game.BeatTime(0.6f), false);
                    });
                    game.RegisterFunction("convR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7, game.BeatTime(0.6f), true);
                    });
                    game.RegisterFunctionOnce("TurnL", () =>
                    {
                        DrawingUtil.PlusRotate(5, game.BeatTime(4));
                    });
                    game.RegisterFunctionOnce("linel", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(0, 240), new(640, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("liner", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(640, 240), new(0, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.DelayBeat(30, () =>
                    {
                        for (int i = 0; i < game.BeatTime(3); i++)
                        {
                            DrawingUtil.Linerotate lL = new(0, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lR = new(640, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lU = new(320, -80, 0, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lD = new(320, 480 + 80, 0, game.BeatTime(3), 0.85f);
                            float a = 0;
                            AddInstance(new InstantEvent(i * 1.2f, () =>
                            {
                                CreateEntity(lL);
                                CreateEntity(lR);
                                CreateEntity(lU);
                                CreateEntity(lD);
                                AddInstance(new TimeRangedEvent(30, () =>
                                {
                                    lL.xCenter += 8;
                                    lR.xCenter -= 8;
                                    lU.yCenter += 8;
                                    lD.yCenter -= 8;
                                    lL.alpha -= 0.05f;
                                    lR.alpha -= 0.05f;
                                    lU.alpha -= 0.05f;
                                    lD.alpha -= 0.05f;
                                    lL.rotate -= 5.5f;
                                    lR.rotate -= 5.5f;
                                    lU.rotate -= 5.5f;
                                    lD.rotate -= 5.5f;
                                    a++;
                                }));
                            }));
                        }
                    });
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "(R)(TurnR)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "D","/","/","/",
                        "D","/","/","/",    "D","/","/","/",

                        "(R)(TurnL)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "D","/","/","/",
                        "D","/","/","/",    "D","/","/","/",

                        "(R)(TurnR)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "D","/","/","/",
                        "D","/","/","/",    "D","/","/","/",

                        "(R)(TurnL)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "D","/","/","/",
                        "D","/","/","/",    "D","/","/","/",
                        ////
                        "($0)(liner)(convR)","/","/","/",    "($0)(liner)(convR)","/","/","/",
                        "($0)(liner)(convR)","/","/","/",    "($0)(liner)(convR)","/","/","/",
                        "($2)(liner)(convR)","/","/","/",    "($2)(liner)(convR)","/","/","/",
                        "($2)(liner)(convR)","/","/","/",    "($2)(liner)(convR)","/","/","/",

                        "($0)(linel)(convL)","/","/","/",    "($0)(linel)(convL)","/","/","/",
                        "($2)(linel)(convL)","/","/","/",    "($2)(linel)(convL)","/","/","/",
                        "($0)(linel)(convL)","/","/","/",    "($0)(linel)(convL)","/","/","/",
                        "($2)(linel)(convL)","/","/","/",    "($2)(linel)(convL)","/","/","/",

                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",

                        "liner","/","linel","/",    "liner","/","linel","/",
                        "liner","/","linel","/",    "liner","/","linel","/",
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
                            game.NormalizedChart(t, 5.5f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                    //vibro&双押
                    float t1 = game.BeatTime(26);
                    string[] rhythm1 =
                    {
                        "$0","/","$0","/","$0","/","$0","/",    "$1","/","$1","/","$1","/","$1","/",
                        "$2","/","$2","/","$2","/","$2","/",    "$3","/","$3","/","$3","/","$3","/",
                        //
                        "/","/","/","/","/","/","/","/",    "/","/","/","/","/","/","/","/",
                         "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",    "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",
                        ////
                    };
                    for (int i = 0; i < rhythm1.Length; i++)
                    {
                        if (rhythm1[i] == "/")
                        {
                            t1 += game.BeatTime(1 / 8f);
                        }
                        else if (rhythm1[i] != "/")
                        {
                            instance.CreateArrows(t1, 5.5f, rhythm1[i]);
                            t1 += game.BeatTime(1 / 8f);
                        }
                    }
                    float t2 = game.BeatTime(26);
                    string[] rhythm2 =
                    {
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        //
                        "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm2.Length; i++)
                    {
                        if (rhythm2[i] == "/")
                        {
                            t2 += game.BeatTime(1 / 6f);
                        }
                        else if (rhythm2[i] != "/")
                        {
                            instance.CreateArrows(t2, 5.5f, rhythm2[i]);
                            t2 += game.BeatTime(1 / 6f);
                        }
                    }
                }
                public static void rhythm04()
                {
                    game.RegisterFunctionOnce("SoulShining", () =>
                    {
                        SetSoul(1);
                    });
                    game.RegisterFunctionOnce("VoidArrow", () =>
                    {
                        Arrow[] a = GetAll<Arrow>();
                        for (int m = 0; m < a.Length; m++)
                        {
                            int x = m;
                            a[x].VoidMode = true;
                        }
                    });
                    game.RegisterFunctionOnce("lA", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(80, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lB", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(160, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lC", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(240, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lD", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(320, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lE", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(400, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lF", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(480, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lG", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(560, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    float t = game.BeatTime(6);
                    string[] rhythm =
                    {//这里就是、、、、、、
                        "VoidArrow","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($0)(lE)","/","/","/",    "($0)(lF)","/","/","/",
                        //
                        "($0)(SoulShining)(lD)(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($0)(lD)","/","/","/",    "($0)(lE)","/","/","/",
                        //
                        "($0)(SoulShining)(lC)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($0)(lC)","/","/","/",    "($0)(lD)","/","/","/",
                        //
                        "($0)(lB)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "($0)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "($0)(SoulShining)(lD)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(lC)(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "($0)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(SoulShining)(lE)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(lB)(lF)(lA)(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////这里是吧//对，我开修了、、我写特效（
                        "$0(SoulShining)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "$0(SoulShining)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lD)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(SoulShining)(lE)(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(SoulShining)(lD)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "$0(SoulShining)(lB)(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "$0(lA)","/","/","/",    "$0(lB)","/","/","/",
                        //
                        "$0(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(SoulShining)(lB)(lC)(lE)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
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
                        MainEffect.Rain(game.BeatTime(64), 6);
                        float alp = 0.75f;
                        Line a = new(CentreEasing.Stable(new(310, 240)), ValueEasing.Stable(90)) { Alpha = 0.75f };
                        Line b = new(CentreEasing.Stable(new(330, 240)), ValueEasing.Stable(90)) { Alpha = 0.75f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                for (int q = 0; q < game.BeatTime(4); q++)
                                {
                                    AddInstance(new InstantEvent(q * 4 + 2, () =>
                                    {
                                        lines.Alpha = 0.01f;
                                    }));
                                    AddInstance(new InstantEvent(q * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp -= 0.00375f;
                                    }));
                                }
                            }
                        });
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(0, 84, game.BeatTime(4)));
                        builder.Insert(game.BeatTime(14 * 4), ValueEasing.Stable(84));
                        builder.Insert(game.BeatTime(4), ValueEasing.EaseInCubic(84, 0, game.BeatTime(4)));
                        ScreenDrawing.BoundColor = new(95, 137, 154, 60);
                        builder.Run(s => ScreenDrawing.UpBoundDistance = s);
                    });
                    game.DelayBeat(28, () =>
                    {
                        float alp = 0.1f;
                        Line a = new(CentreEasing.Stable(new(320, 225)), ValueEasing.Stable(-45)) { Alpha = 0.75f };
                        Line b = new(CentreEasing.Stable(new(320, 255)), ValueEasing.Stable(-45)) { Alpha = 0.75f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                for (int q = 0; q < 75; q++)
                                {
                                    AddInstance(new InstantEvent(q * 4 + 2, () =>
                                    {
                                        lines.Alpha = 0.01f;
                                    }));
                                }
                                for (int i = 0; i < 37; i++)
                                {
                                    AddInstance(new InstantEvent(i * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp += 0.00375f;
                                    }));
                                    AddInstance(new InstantEvent(148 + i * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp -= 0.00375f;
                                    }));
                                }
                            }
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease = new();
                        ValueEasing.EaseBuilder rot1 = new();
                        ValueEasing.EaseBuilder rot2 = new();
                        ValueEasing.EaseBuilder alp = new();
                        rot1.Insert(0, ValueEasing.Stable(-90 - 720));
                        rot2.Insert(0, ValueEasing.Stable(-90 - 720));
                        ease.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.55f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutQuad(-90 - 720, 45 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutQuart(-90 - 720, 0, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.EaseOutSine(0.55f, 0.25f, game.BeatTime(8)));

                        rot1.Insert(0, ValueEasing.Stable(45 + 45));
                        rot2.Insert(0, ValueEasing.Stable(0));
                        ease.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(45 + 45, 165 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(0, 120, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 + 45, 165 + 65 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120, 120 + 65, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 + 65 + 45, 165 - 84 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 + 65, 120 - 84, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 - 84 + 45, 165 - 84 - 58 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 - 84, 120 - 84 - 58, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 - 84 - 58 + 45, 165 - 84 - 58 + 45 - 72, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 - 84 - 58, 120 - 84 - 58 - 72, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(-49 + 45, -49 + 45 - 66, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(-94, -94 - 66, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseInCirc(-49 + 45 - 66, -90, game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseInCirc(-94 - 66, -90, game.BeatTime(4)));
                        ease.Insert(game.BeatTime(4), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.EaseOutQuad(0.25f, 0, game.BeatTime(8)));
                        Line a = new(ease.GetResult(), rot1.GetResult());
                        Line b = new(ease.GetResult(), rot2.GetResult());
                        Line[] l = { a, b };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            lines.Alpha = 0.55f;
                            alp.Run((s) => { lines.Alpha = s; });
                            game.DelayBeat(8, () =>
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    lines.InsertRetention(new(i * 3 + 3, 0.32f - i * 0.03f));
                                }
                            });
                            game.DelayBeat(64, () =>
                            {
                                lines.Dispose();
                            });
                        }
                    });
                    game.DelayBeat(60, () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        CentreEasing.EaseBuilder ce2 = new();
                        CentreEasing.EaseBuilder ce3 = new();
                        CentreEasing.EaseBuilder ce4 = new();
                        ValueEasing.EaseBuilder alp = new();
                        ce1.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce2.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce3.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce4.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.1f));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 - 126, 240), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 - 42, 240), game.BeatTime(2)));
                        ce3.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 + 42, 240), game.BeatTime(2)));
                        ce4.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 + 126, 240), game.BeatTime(2)));
                        alp.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(0.1f, 0.45f, game.BeatTime(2)));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 - 126, 240), new(320, 240), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 - 42, 240), new(320, 240), game.BeatTime(2)));
                        ce3.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 + 42, 240), new(320, 240), game.BeatTime(2)));
                        ce4.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 + 126, 240), new(320, 240), game.BeatTime(2)));
                        alp.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(0.45f, 0, game.BeatTime(2)));
                        Line a = new(ce1.GetResult(), (s) => { return 90; });
                        Line b = new(ce2.GetResult(), (s) => { return 90; });
                        Line c = new(ce3.GetResult(), (s) => { return 90; });
                        Line d = new(ce4.GetResult(), (s) => { return 90; });
                        Line[] l = { a, b, c, d };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            alp.Run(s => lines.Alpha = s);
                            game.DelayBeat(4, () => { lines.Dispose(); });
                        }
                    });
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
                        Heart.InstantSetRotation(180);
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
                        Heart.InstantSetRotation(-180);
                        TP(new Vector2(320 + 126, 480));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$S", new Vector2(316 - 126, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$D", new Vector2(316 - 42, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$Left", new Vector2(300 + 42, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$Down", new Vector2(308 + 126, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
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
                    game.DelayBeat(62, () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        CentreEasing.EaseBuilder ce2 = new();
                        ce1.Insert(0, CentreEasing.Stable(new(0, 0)));
                        ce2.Insert(0, CentreEasing.Stable(new(640, 0)));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(0, 0), new(640, 640), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(640, 0), new(0, 640), game.BeatTime(2)));
                        Line a = new(ce1.GetResult(), (s) => { return -45; }) { Alpha = 0.45f };
                        Line b = new(ce2.GetResult(), (s) => { return 45; }) { Alpha = 0.45f };
                        Line[] l = { a, b };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            for (int i = 0; i < 8; i++)
                            {
                                lines.InsertRetention(new(i * 4 + 4, 0.55f - i * 0.03f));
                            }
                            game.DelayBeat(2, () => { lines.Dispose(); });
                        }
                    });
                }
                public static void rhythm05()
                {
                    game.RegisterFunctionOnce("LD", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                    });
                    game.RegisterFunctionOnce("LU", () =>
                    {

                    });
                    game.RegisterFunctionOnce("RD", () =>
                    {

                    });
                    game.RegisterFunctionOnce("RU", () =>
                    {

                    });
                    game.RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UIColor = new Color(20, 20, 20);
                        ScreenDrawing.UISettings.CreateUISurface();
                        float camRotation = 43;
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(0, 0), new(0, 50), game.BeatTime(2)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

                        RenderProduction production4 = new ScreenDrawing.Shaders.Filter(game.Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(production4);

                        ValueEasing.EaseBuilder camera = new();
                        game.Effect3D.TextureSize = new(640, 1280 * (1 + ScreenDrawing.UpExtending));
                        game.Effect3D.ProjectPointOffect = new(320, game.Effect3D.TextureSize.Y);
                        game.Effect3D.ProjectPoint = new(0, 0, 200);
                        camera.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(0, camRotation, game.BeatTime(2)));
                        camera.Run(rotation =>
                        {
                            game.Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            game.Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            game.Effect3D.CameraPosition = new(0, -900 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    game.RegisterFunctionOnce("StepStart", () =>
                    {
                        game.StepSample.CentreX = 320f;
                        game.StepSample.CentreY = 240f;
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;

                        });
                    });
                    game.RegisterFunctionOnce("Step", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.1f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0.1f, 0.3f, game.BeatTime(1)));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.3f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                            game.splitter.Intensity = s * 30f - 0.1f * 30f;
                        });
                    });
                    game.RegisterFunctionOnce("StepOver", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.1f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.1f, 0f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                        });
                    });
                    float t = game.BeatTime(4);
                    string[] rhythm =
                    {
                        "(^$3'1.4)(StepStart)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "R","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "R","/","+0","/",
                        "","/","R","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "/","/","/","/",
                        ////
                        "(^R'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //                     
                        "(^R'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "","/","/","/",
                        "(R)","/","/","/",    "(R)","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "R","/","+0","/",    "/","/","R","/",
                        "+0","/","+0","/",    "/","/","","/",
                        "R","/","","/",    "R","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "R","/","+0","/",
                        "","/","R","/",    "+0","/","/","/",
                        "R","/","+0","/",    "+0","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        ////
                        "(R)(Step)","/","/","/",    "R","/","","/",
                        "/","/","/","/",    "R","/","/","/",
                        "(^$2'1.2)","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(R)(Step)","/","/","/",    "R","/","","/",
                        "/","/","/","/",    "R","/","/","/",
                        "(^$0'1.2)","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(R)(Step)","/","/","/",    "","/","","/",
                        "(^$2'1.2)","/","/","/",    "","/","/","/",
                        "(R)(Step)","/","/","/",    "","/","","/",
                        "(^$0'1.2)","/","/","/",    "","/","/","/",
                        //
                        "(R)(Step)","/","/","/",    "R","/","+0","/",
                        "/","/","R","/",    "+0","/","/","/",
                        "(^$2'1.2)","/","+0","/",    "+0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        ////
                        "(R)(Step)","/","/","/",    "R","/","","/",
                        "/","/","/","/",    "R","/","/","/",
                        "(^$0'1.2)","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "(R)(Step)","/","/","/",    "R","/","","/",
                        "/","/","/","/",    "R","/","/","/",
                        "(R)(Step)","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        //
                        "($0)(Step)","/","/","/",    "$2","/","$2","/",
                        "","/","$2","/",    "$2","/","/","/",
                        "($2)(Step)","/","","/",    "$0","/","$0","/",
                        "","/","$0","/",    "$0","/","$0","/",
                        //
                        "(R)(StepOver)","/","/","/",    "R","/","+0","/",
                        "/","/","R","/",    "+0","/","/","/",
                        "($3)","/","+0","/",    "+0","/","/","/",
                        "($3'0.2)","/","/","/",    "","/","/","/",
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

                    });
                    game.DelayBeat(0, () =>
                    {

                    });
                }
                public static void rhythm06A()
                {
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
                    game.StepSample.CentreY = -800f;
                    game.DelayBeat(2, () =>
                    {
                        #region Split
                        Heart.Split();
                        SetBoxMission(0);
                        MainEffect.SoftSetBox(new(320 - 126, 480 - 84), 84, 84, game.BeatTime(1), 0, 1);
                        SetPlayerMission(0);
                        Heart.Alpha = 0.2f;

                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(0); Heart.Shields.RemoveShield(Heart.Shields.BShield); });
                        Heart.InstantSetRotation(180);
                        MainEffect.SoftTP(new(320 - 126, 480 - 84), game.BeatTime(1), 0, 1);

                        Heart.Split();
                        SetBoxMission(1);
                        MainEffect.SoftSetBox(new(320 - 42, 480 - 84), 84, 84, game.BeatTime(1), 1, 1);
                        SetPlayerMission(2);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(2); Heart.Shields.RemoveShield(Heart.Shields.RShield); });
                        Heart.InstantSetRotation(90);
                        MainEffect.SoftTP(new(320 + 42, 480 - 84), game.BeatTime(1), 2, 1);
                        SetBoxMission(2);
                        MainEffect.SoftSetBox(new(320 + 42, 480 - 84), 84, 84, game.BeatTime(1), 2, 1);
                        SetPlayerMission(1);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(1); Heart.Shields.RemoveShield(Heart.Shields.BShield); });
                        Heart.InstantSetRotation(-90);
                        MainEffect.SoftTP(new(320 - 42, 480 - 84), game.BeatTime(1), 1, 1);

                        Heart.Split();
                        SetBoxMission(3);
                        MainEffect.SoftSetBox(new(320 + 126, 480 - 84), 84, 84, game.BeatTime(1), 3, 1);
                        SetPlayerMission(3);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(3); Heart.Shields.RemoveShield(Heart.Shields.RShield); });
                        Heart.InstantSetRotation(-180);
                        MainEffect.SoftTP(new(320 + 126, 480 - 84), game.BeatTime(1), 3, 1);
                        #endregion
                        #region Line
                        for (int a = 0; a < 5; a++)
                        {
                            Line l1 = new(new Vector2(320 - 126 - 42 + a * 84, 480), new Vector2(320 - 126 - 42 + a * 84, -800)) { Alpha = 0 };
                            CreateEntity(l1);
                            l1.AlphaIncrease(game.BeatTime(2), 0.5f);
                        }
                        Line l2 = new(new Vector2(320 - 126 - 42, 480 - 84 - 42), new Vector2(320 - 126 - 42 + 4 * 84, 480 - 84 - 42)) { Alpha = 0 };
                        CreateEntity(l2);
                        l2.AlphaIncrease(game.BeatTime(2), 1f);
                        #endregion
                    });
                    #region 
                    game.RegisterFunctionOnce("k0", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k1", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k2", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k3", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h0", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h1", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h2", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h3", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunction("Lk0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 1, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 0, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Rk0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 1, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 0, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Step", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.2f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0.1f, 0.3f, game.BeatTime(1)));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.3f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.8f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                        });
                    });
                    game.RegisterFunction("Polor", () =>
                    {
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.8f;

                        });
                        ValueEasing.EaseBuilder e2 = new();
                        e2.Insert(0, ValueEasing.Stable(0f));
                        e2.Insert(game.BeatTime(96), ValueEasing.SinWave(0.1f, game.BeatTime(8), 0));
                        e2.Insert(1, ValueEasing.Stable(0));
                        e2.Run((s) =>
                        {

                            game.Polar.Intensity = s * 0.07f;
                            game.Polar.IType = false;
                        });
                    });
                    #endregion
                    #region UI
                    Color uic = ScreenDrawing.UIColor;
                    ValueEasing.EaseBuilder scale = new();
                    scale.Insert(0, ValueEasing.Stable(1));
                    scale.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(1, 0.1f, game.BeatTime(4)));
                    scale.Run(scale => { ScreenDrawing.UIColor = uic * scale; ScreenDrawing.HPBar.HPExistColor = new Color(90, 112, 125) * scale; ScreenDrawing.HPBar.HPLoseColor = Color.Gray * scale; });
                    #endregion
                    ValueEasing.EaseBuilder len = new();
                    len.Insert(game.BeatTime(64), ValueEasing.Stable((4 * 84)));
                    len.Insert(game.BeatTime(1), ValueEasing.EaseInSine(4 * 84, 6 * 84, game.BeatTime(1)));
                    Line l = new(CentreEasing.Stable(320, 480 - 84 - 42), ValueEasing.Stable(0), len.GetResult()) { Depth = 0.99f, Alpha = 0 };
                    CreateEntity(l);
                    l.AlphaIncrease(game.BeatTime(8), 1);
                    game.BarrageCreate(0, game.BeatTime(1), 7.5f, new string[]
                    {
                        "(Polor)","","","",    "","","","",
                        "","","","",    "","","","",
                        "(k2)","","k0","",    "k2","","k0","",
                        "k2(Step)","","k0","",    "k2","","k0","",

                        "(h2)(h1)","","h0","",    "h1","","h2","",
                        "h3","","h1","",    "h2","","h0","",
                        "(k1)","","k3","",   "k1","","k3","",
                        "k1(Step)","","k3","",   "k1","","k3","",

                        "(h1)(h0)","","h2","",    "h1","","h0","",
                        "h1","","h3","",    "h2","","h1","",
                        "(k2)","","k0","",    "k2","","k0","",
                        "(k2)(Step)","","k0","",    "k2","","k0","",

                        "!!3/6","h2","h1","h2","h3","h2","h1",
                        "!!3/6","h0","h1","h2","h3","h2","h1",
                        "!!3/6","h0","h1","h2","h3","h2","h1",
                        "!!3/6","h0(Step)","h1","h2","h3","h0","h1",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h0","",   "h1","","h2","",
                        "k3","","k2","",   "k1","","k0","",
                        "k3(Step)","","k2","",   "k1","","k0","",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h2","",   "h0","","h1","",
                        "k0","","k1","",   "k2","","k3","",
                        "k0(Step)","","k1","",   "k2","","k3","",

                        "!!3/6","h0","h1","h2","h3","h0","h1",
                        "!!3/6","h2","h3","h0","h1","h2","h3",
                        "!!3/6","h2","h1","h0","h3","h2","h1",
                        "!!3/6","h0(Step)","h3","h2","h1","h0","h1",

                        "!!3/6","h3","h2","h0","h1","h3","h2",
                        "!!3/6","h0","h1","h3","h2","h0","h1",
                        "!!3/6","h2","h3","h1","h0","h2","h3",
                        "!!3/6","h1(Step)","h0","h2","h3","h2","h1",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h0","",   "h1","","h2","",
                        "k3","","k0","",   "k1","","k2","",
                        "k3(Step)","","k0","",   "k1","","k2","",

                        "(h0)(h1)","","h3","",   "h2","","h1","",
                        "h2","","h0","",   "h1","","h2","",
                        "k3","","k2","",   "k3","","k2","",
                        "k1(Step)","","k0","",   "k1","","k0","",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h2","",   "h0","","h1","",
                        "k0","","k1","",   "k0","","k1","",
                        "k2(Step)","","k3","",   "k2","","k3","",

                        "h2","","h3","",   "h0","","h1","",
                        "h2","","h3","",   "h0","","h1","",
                        "h0","","h1","",   "h2","","h3","",
                        "!!3/6","k0(Step)","k1","k2","k3","k0","k1",

                        "(h3)(h2)","","h1","",   "h0","","h1","",
                        "h0","","h1","",   "h0","","h1","",
                        "(k0)(h3)","","h2","",   "(k1)(h3)","","h2","",
                        "(k0)(h3)(Step)","","h2","",   "(k1)(h3)","","h2","",

                        "h0","","h1","",   "(h0)(k3)","","h1","",
                        "(h0)(k2)","","h1","",   "(h0)(k3)","","h1","",
                        "h3","","h2","",   "h1","","h0","",
                        "h1(Step)","","h3","",   "h2","","h0","",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h2","",   "h0","","h1","",
                        "(k0)(h2)","","h3","",   "(k1)(h2)","","h3","",
                        "(k0)(h2)(Step)","","h3","",   "(k1)(h2)","","h3","",

                        "k0","","k2","",   "k0","","k2","",
                        "k0","","k2","",   "k0","","k2","",
                        "k1","","k3","",   "k1","","k3","",
                        "k1(Step)","","k3","",   "k1","","k3","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });

                    game.DelayBeat(60, () => { l.AlphaDecrease(game.BeatTime(4)); });

                }
                public static void _6k()
                {
                    game.To6KIn3D();
                    Player.Heart ExHeart1 = CreateHeart(new CollideRect(320 - 168, 480 - 84, 84, 84));
                    Player.Heart ExHeart2 = CreateHeart(new CollideRect(320 + 168, 480 - 84, 84, 84));
                    game.RegisterFunctionOnce("h0", () =>
                    {
                        SetPlayerMission(ExHeart1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h1", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h2", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h3", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h4", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h5", () =>
                    {
                        SetPlayerMission(ExHeart2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k0", () =>
                    {
                        SetPlayerMission(ExHeart1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k1", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k2", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k3", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k4", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k5", () =>
                    {
                        SetPlayerMission(ExHeart2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    SetPlayerBoxMission(ExHeart1);
                    MainEffect.SoftTP(new(320 - 126 - 84, 480 - 84), game.BeatTime(3), 4, 1);
                    MainEffect.SoftSetBox(new(320 - 126 - 84, 480 - 84), 84, 84, game.BeatTime(3), 4, 1);
                    ExHeart1.InstantSetRotation(90);
                    SetSoul(1);
                    game.DelayBeat(0, () => { ExHeart1.Shields.RemoveShield(ExHeart1.Shields.BShield); });
                    SetPlayerBoxMission(ExHeart2);
                    MainEffect.SoftTP(new(320 + 126 + 84, 480 - 84), game.BeatTime(3), 5, 1);
                    MainEffect.SoftSetBox(new(320 + 126 + 84, 480 - 84), 84, 84, game.BeatTime(3), 5, 1);
                    ExHeart2.InstantSetRotation(-90);
                    SetSoul(1);
                    game.DelayBeat(0, () => { ExHeart2.Shields.RemoveShield(ExHeart2.Shields.RShield); });
                    Line l = new(new Vector2(320 - 3 * 84, 480), new Vector2(320 - 3 * 84, -800)) { DrawingColor = ScreenDrawing.ThemeColor, Alpha = 0 };
                    CreateEntity(l);
                    l.AlphaIncrease(game.BeatTime(1), 0.5f);
                    Line l2 = new(new Vector2(320 + 3 * 84, 480), new Vector2(320 + 3 * 84, -800)) { DrawingColor = ScreenDrawing.ThemeColor, Alpha = 0 };
                    CreateEntity(l2);
                    l2.AlphaIncrease(game.BeatTime(1), 0.5f);
                    game.BarrageCreate(0, game.BeatTime(1), 8, new string[]
                    {
                        "(k0)","","k3","",   "k1","","k4","",
                        "(k2)","","k5","",   "k1","","k4","",
                        "(k0)","","k3","",   "k1","","k4","",
                        "(k2)","","k5","",   "k1","","k4","",

                        "(k2)","","k3","",   "k2","","k3","",
                        "(k2)","","k3","",   "k2","","k3","",
                        "(k1)","","k4","",   "k1","","k4","",
                        "(k1)","","k4","",   "k1","","k4","",

                        "(k0)","","k3","",   "k2","","k5","",
                        "(k0)","","k3","",   "k2","","k5","",
                        "(k1)","","k4","",   "k2","","k3","",
                        "(k1)","","k4","",   "k2","","k3","",

                        "(k0)","","k3","",   "k2","","k5","",
                        "!!3/6","h2","h1","h0","h1","h2","h3",
                        "!!3/6","h4","h5","h4","h3","h2","h1",
                        "(k0)","","k3","",   "k2","","k5","",
                        //
                        "(k3)","","k0","",   "k4","","k1","",
                        "(k5)","","k2","",   "k4","","k1","",
                        "(k3)","","k0","",   "k4","","k1","",
                        "(k5)","","k2","",   "k4","","k1","",

                        "(k3)","","k2","",   "k3","","k2","",
                        "(k3)","","k2","",   "k3","","k2","",
                        "(k4)","","k1","",   "k4","","k1","",
                        "(k4)","","k1","",   "k4","","k1","",

                        "(k3)","","k0","",   "k5","","k2","",
                        "(k3)","","k0","",   "k5","","k2","",
                        "(k4)","","k1","",   "k3","","k2","",
                        "(k4)","","k1","",   "k3","","k2","",

                        "(k3)","","k2","",   "k3","","k2","",
                        "!!3/12","(h0)","h1","h2","h3","h4","h5",
                        "h0","h1","h2","h3","h4","h5",
                        "h0",
                        //
                    });


                    //test
                    game.DelayBeat(34, game.Apply2D);
                }
                public static void over()
                {
                    SetPlayerMission(0);
                    game.RegisterFunctionOnce("Rot", () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(360, game.BeatTime(10));
                    });
                    game.RegisterFunctionOnce("linel", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(0, 240), new(640, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("liner", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(640, 240), new(0, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("Scale", () =>
                    {
                        for (int i = 0; i < game.BeatTime(1.5f); i++)
                        {
                            DrawingUtil.Linerotate lL = new(0, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lR = new(640, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lU = new(320, -80, 0, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lD = new(320, 480 + 80, 0, game.BeatTime(3), 0.85f);
                            float a = 0;
                            AddInstance(new InstantEvent(i * 2.4f, () =>
                            {
                                CreateEntity(lL);
                                CreateEntity(lR);
                                CreateEntity(lU);
                                CreateEntity(lD);
                                AddInstance(new TimeRangedEvent(30, () =>
                                {
                                    lL.xCenter += 8;
                                    lR.xCenter -= 8;
                                    lU.yCenter += 8;
                                    lD.yCenter -= 8;
                                    lL.alpha -= 0.05f;
                                    lR.alpha -= 0.05f;
                                    lU.alpha -= 0.05f;
                                    lD.alpha -= 0.05f;
                                    lL.rotate -= 5.5f;
                                    lR.rotate -= 5.5f;
                                    lU.rotate -= 5.5f;
                                    lD.rotate -= 5.5f;
                                    a++;
                                }));
                            }));
                        }
                        float p = 0;
                        game.ForBeat(4, () =>
                        {
                            Heart.InstantSetRotation(p * p * 0.08f);
                            ScreenDrawing.ScreenScale = p * 0.02f + 1;
                            p++;
                        });
                    });
                    game.RegisterFunctionOnce("ScaleBack", () =>
                    {
                        DrawingUtil.LerpScreenScale(game.BeatTime(16), 1, 0.03f);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Heart.Rotation % 360));
                        ve.Insert(game.BeatTime(32), ValueEasing.EaseOutSine(Heart.Rotation % 360, 360 * 2, game.BeatTime(32)));
                        ve.Run((s) => { Heart.InstantSetRotation(s); });
                    });
                    game.RegisterFunctionOnce("End", () =>
                    {
                        game.End();
                    });
                    game.BarrageCreate(0, game.BeatTime(1), 6.5f, new string[]
                    {
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "$01","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",

                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",

                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        //
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",

                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",
                        "+11","","","",   "+11","","","",

                        "($0)(linel)","","$31'1.3","",   "($0)(liner)","","$11'1.3","",
                        "($0)(linel)","","$31'1.3","",   "($0)(liner)","","$11'1.3","",
                        "($2)(linel)","","$31'1.3","",   "($2)(liner)","","$11'1.3","",
                        "($2)(linel)","","$31'1.3","",   "($2)(liner)","","$11'1.3","",

                        "($0)(linel)(Scale)","","(<$11'1.3)(liner)","",   "($0)(linel)","","(<$11'1.5)(liner)","",
                        "($0)(linel)","","(<$11'1.3)(liner)","",   "($0)(linel)","","(<$11'1.5)(liner)","",
                        "!!3/6","(<$0'1.3)","(>$11)","(>$0'1.3)","(<$11)","(<$0'1.3)","(>$11)",
                        "!!3/6","(<$0'1.3)","(<$11)","(>$0'1.3)","(>$11)","(<$0'1.3)","(<$11)",

                        "(<$0'1.3)(ScaleBack)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "End","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                    game.BarrageCreate(0, game.BeatTime(1), 7.5f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(convL)","","","",   "(convL)","","","",

                        "","","","",   "","","","",
                        "","","","",   "(convR)","","","",
                        "(convR)","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "$1(convL)","","+0","",   "+0(convL)","","+0","",

                        "+0","","","",   "","","","",
                        "","","","",   "$3(convR)","","+0","",
                        "+0(convR)","","+0","",   "+0","","","",
                        "","","","",   "","","","",
                        //
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "$1(convL)","","+0","",   "+0(convL)","","+0","",

                        "+0","","","",   "","","","",
                        "","","","",   "$3(convR)","","+0(convR)","",
                        "+0","","+0","",   "+0","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                }
            }
            private static class NoobBarrage
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
                        ValueEasing.EaseBuilder rot = new();
                        rot.Insert(game.BeatTime(4), ValueEasing.EaseInBack(0, 120, game.BeatTime(4)));
                        rot.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });
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
                            ScreenDrawing.ScreenAngle = 0;
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        float alp = 0.75f;
                        DrawingUtil.NormalLine a = new(0, Rand(150, 195), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine b = new(0, Rand(195, 240), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine c = new(0, Rand(240, 285), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine d = new(0, Rand(285, 330), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine e = new(0, Rand(105, 150), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        DrawingUtil.NormalLine f = new(0, Rand(330, 375), 640, LastRand, game.BeatTime(24), alp) { depth = 0.01f };
                        CreateEntity(a);
                        CreateEntity(b);
                        CreateEntity(c);
                        CreateEntity(d);
                        CreateEntity(e);
                        CreateEntity(f);
                        for (int i = 0; i < game.BeatTime(24); i++)
                        {
                            AddInstance(new InstantEvent(i * 4, () =>
                            {
                                a.alpha = alp;
                                b.alpha = alp;
                                c.alpha = alp;
                                d.alpha = alp;
                                e.alpha = alp;
                                f.alpha = alp;
                                alp -= 0.0075f;
                            }));
                            AddInstance(new InstantEvent(i * 4 + 2, () =>
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
                    game.DelayBeat(8, () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuad(new(320, 240), new(160, 240), game.BeatTime(4)), (s) => { return 45; }) { Alpha = 0.85f };
                        a.AlphaDecrease(game.BeatTime(4), 0.85f);
                        a.InsertRetention(new(8, 0.75f));
                        Line b = new(CentreEasing.EaseOutQuad(new(320, 240), new(480, 240), game.BeatTime(4)), (s) => { return 45; }) { Alpha = 0.85f };
                        b.AlphaDecrease(game.BeatTime(4), 0.85f);
                        b.InsertRetention(new(8, 0.75f));
                        CreateEntity(a);
                        CreateEntity(b);
                    });
                    game.DelayBeat(16, () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuad(new(320, 240), new(160, 240), game.BeatTime(4)), (s) => { return -45; }) { Alpha = 0.85f };
                        a.AlphaDecrease(game.BeatTime(4), 0.85f);
                        a.InsertRetention(new(8, 0.75f));
                        Line b = new(CentreEasing.EaseOutQuad(new(320, 240), new(480, 240), game.BeatTime(4)), (s) => { return -45; }) { Alpha = 0.85f };
                        b.AlphaDecrease(game.BeatTime(4), 0.85f);
                        b.InsertRetention(new(8, 0.75f));
                        CreateEntity(a);
                        CreateEntity(b);
                    });
                    game.DelayBeat(20, () =>
                    {
                        CentreEasing.EaseBuilder ease1 = new();
                        CentreEasing.EaseBuilder ease2 = new();
                        CentreEasing.EaseBuilder ease3 = new();
                        CentreEasing.EaseBuilder ease4 = new();
                        ValueEasing.EaseBuilder rota = new();
                        ValueEasing.EaseBuilder rotb = new();
                        ease1.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(160, 240), game.BeatTime(2)));
                        ease2.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(480, 240), game.BeatTime(2)));
                        ease3.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(2)));
                        ease4.Insert(game.BeatTime(2), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(2)));
                        rota.Insert(game.BeatTime(4), ValueEasing.Stable(0));
                        rotb.Insert(game.BeatTime(4), ValueEasing.Stable(0));
                        ease1.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(160, 240), new(320, 240), game.BeatTime(2)));
                        ease2.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(480, 240), new(320, 240), game.BeatTime(2)));
                        ease3.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(240, 240), new(320, 240), game.BeatTime(2)));
                        ease4.Insert(game.BeatTime(2), CentreEasing.EaseInCubic(new(400, 240), new(320, 240), game.BeatTime(2)));
                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(4)));
                        ease3.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(240, 240), game.BeatTime(4)));
                        ease4.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(320, 240), new(400, 240), game.BeatTime(4)));
                        rota.Insert(game.BeatTime(4), ValueEasing.EaseOutQuart(0, 45, game.BeatTime(4)));
                        rotb.Insert(game.BeatTime(4), ValueEasing.EaseOutQuart(0, -45, game.BeatTime(4)));
                        Line a = new(ease1.GetResult(), ValueEasing.Combine(rota.GetResult(), 90)) { Alpha = 0.85f };
                        Line b = new(ease2.GetResult(), ValueEasing.Combine(rota.GetResult(), 90)) { Alpha = 0.85f };
                        Line c = new(ease3.GetResult(), ValueEasing.Combine(rotb.GetResult(), 90)) { Alpha = 0.85f };
                        Line d = new(ease4.GetResult(), ValueEasing.Combine(rotb.GetResult(), 90)) { Alpha = 0.85f };
                        a.InsertRetention(new(4, 0.55f));
                        b.InsertRetention(new(4, 0.55f));
                        c.InsertRetention(new(4, 0.55f));
                        d.InsertRetention(new(4, 0.55f));
                        Line[] lines = { a, b, c, d };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line line in lines)
                            {
                                CreateEntity(line);
                                line.AlphaDecrease(game.BeatTime(2), 0.75f);
                            }
                        });
                        game.DelayBeat(2, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaIncrease(game.BeatTime(2), 0.75f);
                            }
                        });
                        game.DelayBeat(4, () =>
                        {
                            foreach (Line line in lines)
                            {
                                line.AlphaDecrease(game.BeatTime(4), 0.85f);
                            }
                        });
                    });
                }
                public static void rhythm01()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "(R)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "+0","/","/","/",    "/","/","/","/",
                        //
                        "R","/","/","/",    "/","/","/","/",    "+0","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "+0","/","/","/",    "/","/","/","/",
                        //
                        "(R)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "+0","/","/","/",    "/","/","/","/",
                        //
                        "R","/","/","/",    "/","/","/","/",    "+0","/","/","/",    "/","/","/","/",
                        "R","/","/","/",    "/","/","/","/",    "+0","/","/","/",    "/","/","/","/",
                        ////
                        "(R)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "(R)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
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
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease1 = new();
                        CentreEasing.EaseBuilder ease2 = new();
                        ValueEasing.EaseBuilder rot1 = new();
                        ValueEasing.EaseBuilder rot2 = new();
                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(640, 0), new(120, 360), game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(30));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(0, 240)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(90));

                        ease1.Insert(game.BeatTime(4), CentreEasing.Stable(new(120, 360)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(30, -135, game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(0, 240), new(480, 240), game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(90));

                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(120, 360), new(440, 180), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(480, 240)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(90, -45, game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(-135));

                        ease1.Insert(game.BeatTime(4), CentreEasing.Stable(new(440, 180)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(-135, 45, game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(480, 240), new(200, 180), game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(-45));

                        ease1.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(440, 180), new(320, 240), game.BeatTime(4)));
                        ease2.Insert(game.BeatTime(4), CentreEasing.Stable(new(200, 180)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(-45, 135, game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.Stable(45));
                        rot2.Insert(game.BeatTime(4), ValueEasing.Stable(135));

                        ease2.Insert(game.BeatTime(4), CentreEasing.EaseOutCubic(new(200, 180), new(320, 240), game.BeatTime(4)));
                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(45, -135, game.BeatTime(4)));
                        ease1.Insert(game.BeatTime(12), CentreEasing.Stable(new(320, 240)));
                        ease2.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));

                        rot1.Insert(game.BeatTime(6), ValueEasing.EaseOutBack(-135, -360, game.BeatTime(6)));
                        rot2.Insert(game.BeatTime(6), ValueEasing.EaseOutBack(135, -90, game.BeatTime(6)));
                        rot1.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-360, -315, game.BeatTime(0.75f)));
                        rot2.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-90, -45, game.BeatTime(0.75f)));
                        rot1.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-315, -270, game.BeatTime(0.75f)));
                        rot2.Insert(game.BeatTime(0.75f), ValueEasing.EaseOutExpo(-45, 0, game.BeatTime(0.75f)));
                        rot1.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutExpo(-270, -225, game.BeatTime(0.5f)));
                        rot2.Insert(game.BeatTime(0.5f), ValueEasing.EaseOutExpo(0, 45, game.BeatTime(0.5f)));
                        Line l1 = new(ease1.GetResult(), rot1.GetResult()) { Alpha = 0.55f };
                        Line l2 = new(ease2.GetResult(), rot2.GetResult()) { Alpha = 0.55f };
                        Line[] l = { l1, l2 };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line line in l)
                            {
                                CreateEntity(line);
                                line.InsertRetention(new(2, 0.8f));
                                line.InsertRetention(new(4, 0.7f));
                                line.InsertRetention(new(6, 0.6f));
                                line.InsertRetention(new(8, 0.5f));
                            }
                        });
                        game.DelayBeat(24, () =>
                        {
                            foreach (Line line in l)
                            {
                                line.AlphaDecrease(game.BeatTime(6), 0.4f);
                                game.DelayBeat(6, () =>
                                {
                                    line.Alpha = 0.55f;
                                    game.DelayBeat(0.125f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(0.25f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(0.375f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(0.75f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(0.875f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.125f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1.5f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.625f, () =>
                                    {
                                        line.Alpha = 0.25f;
                                    });
                                    game.DelayBeat(1.75f, () =>
                                    {
                                        line.Alpha = 0.55f;
                                    });
                                    game.DelayBeat(1.875f, () =>
                                    {
                                        line.Alpha = 0;
                                    });
                                });
                            }
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        ValueEasing.EaseBuilder h = new();
                        h.Insert(0, ValueEasing.Stable(45));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutSine(45, 85, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseInSine(85, 45, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(45, 95, game.BeatTime(4)));
                        h.Insert(game.BeatTime(4), ValueEasing.EaseOutBack(95, 135, game.BeatTime(4)));
                        h.Insert(game.BeatTime(3), ValueEasing.EaseOutCubic(135, 0, game.BeatTime(3)));
                        h.Insert(1, ValueEasing.Stable(0));
                        h.Run((s) =>
                        {
                            ScreenDrawing.BoundColor = Color.Lerp(Color.White, Color.Gray, 1.2f);
                            ScreenDrawing.DownBoundDistance = s;
                            ScreenDrawing.UpBoundDistance = s;
                        });
                        game.DelayBeat(63, () =>
                        {
                            ScreenDrawing.DownBoundDistance = 0;
                            ScreenDrawing.UpBoundDistance = 0;
                        });
                    });
                }
                public static void rhythm02()
                {
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "$0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",    "R","/","/","/",    "+0","/","/","/",
                        //
                        "R","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",    "R","/","/","/",    "+0","/","/","/",
                        //
                        "R","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",    "R","/","/","/",    "+0","/","/","/",
                        //
                        "R","/","/","/",    "+0","/","/","/",    "+0","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",    "R","/","/","/",    "+0","/","/","/",
                        
                        ////
                        "$0","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        //
                        "D","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        //
                        "D","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        //
                        "(D)","/","/","/",    "+0","/","/","/",    "D","/","/","/",    "+0","/","/","/",
                        "R","/","+0","/",    "/","/","+0","/",    "+0","/","/","/",    "+0","/","+0","/",
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
                            instance.CreateArrows(t, 5.5f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                }
                public static void effect03()
                {
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease = new();
                        ValueEasing.EaseBuilder rot = new();
                        ease.Insert(game.BeatTime(0), CentreEasing.Stable(320, 240));
                        ease.Insert(game.BeatTime(2f), CentreEasing.EaseOutQuad(new(320, 240), new(320, 120), game.BeatTime(2)));
                        ease.Insert(game.BeatTime(3.5f), CentreEasing.EaseInQuad(new(320, 120), new(320, 840), game.BeatTime(2.5f)));
                        rot.Insert(game.BeatTime(6), ValueEasing.EaseInCirc(45, -540, game.BeatTime(6)));
                        Line a = new(ease.GetResult(), rot.GetResult()) { Alpha = 0.55f };
                        Line b = new(ease.GetResult(), ValueEasing.Combine(rot.GetResult(), 90)) { Alpha = 0.55f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                lines.AlphaDecrease(game.BeatTime(6), 0.45f);
                                for (int i = 0; i < 24; i++)
                                {
                                    int t = i;
                                    lines.InsertRetention(new(t * 0.5f, 0.24f - t * 0.01f));
                                }
                            }
                            game.DelayBeat(6, () =>
                            {
                                foreach (Line lines in line)
                                {
                                    lines.Dispose();
                                }
                            });
                        });
                    });
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
                    game.RegisterFunctionOnce("TurnR", () =>
                    {
                        DrawingUtil.PlusRotate(-5, game.BeatTime(4));
                    });
                    game.RegisterFunction("convL", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7, game.BeatTime(0.6f), false);
                    });
                    game.RegisterFunction("convR", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(7, game.BeatTime(0.6f), true);
                    });
                    game.RegisterFunctionOnce("TurnL", () =>
                    {
                        DrawingUtil.PlusRotate(5, game.BeatTime(4));
                    });
                    game.RegisterFunctionOnce("linel", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(0, 240), new(640, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("liner", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(640, 240), new(0, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.DelayBeat(30, () =>
                    {
                        for (int i = 0; i < game.BeatTime(3); i++)
                        {
                            DrawingUtil.Linerotate lL = new(0, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lR = new(640, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lU = new(320, -80, 0, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lD = new(320, 480 + 80, 0, game.BeatTime(3), 0.85f);
                            float a = 0;
                            AddInstance(new InstantEvent(i * 1.2f, () =>
                            {
                                CreateEntity(lL);
                                CreateEntity(lR);
                                CreateEntity(lU);
                                CreateEntity(lD);
                                AddInstance(new TimeRangedEvent(30, () =>
                                {
                                    lL.xCenter += 8;
                                    lR.xCenter -= 8;
                                    lU.yCenter += 8;
                                    lD.yCenter -= 8;
                                    lL.alpha -= 0.05f;
                                    lR.alpha -= 0.05f;
                                    lU.alpha -= 0.05f;
                                    lD.alpha -= 0.05f;
                                    lL.rotate -= 5.5f;
                                    lR.rotate -= 5.5f;
                                    lU.rotate -= 5.5f;
                                    lD.rotate -= 5.5f;
                                    a++;
                                }));
                            }));
                        }
                    });
                    float t = game.BeatTime(2);
                    string[] rhythm =
                    {
                        "(R)(TurnR)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",

                        "(R)(TurnL)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",

                        "(R)(TurnR)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",

                        "(R)(TurnL)","/","/","/",    "+0","/","/","/",
                        "+0","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",
                        "D","/","/","/",    "+0","/","/","/",
                        ////
                        "($0)(liner)(convR)","/","/","/",    "($0)(liner)(convR)","/","/","/",
                        "($0)(liner)(convR)","/","/","/",    "($0)(liner)(convR)","/","/","/",
                        "($2)(liner)(convR)","/","/","/",    "($2)(liner)(convR)","/","/","/",
                        "($2)(liner)(convR)","/","/","/",    "($2)(liner)(convR)","/","/","/",

                        "($0)(linel)(convL)","/","/","/",    "($0)(linel)(convL)","/","/","/",
                        "($2)(linel)(convL)","/","/","/",    "($2)(linel)(convL)","/","/","/",
                        "($0)(linel)(convL)","/","/","/",    "($0)(linel)(convL)","/","/","/",
                        "($2)(linel)(convL)","/","/","/",    "($2)(linel)(convL)","/","/","/",

                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",
                        "liner","/","/","/",    "linel","/","/","/",

                        "liner","/","linel","/",    "liner","/","linel","/",
                        "liner","/","linel","/",    "liner","/","linel","/",
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
                            game.NormalizedChart(t, 5.5f, rhythm[i]);
                            t += game.BeatTime(0.125f);
                        }
                    }
                    //vibro&双押
                    float t1 = game.BeatTime(26);
                    string[] rhythm1 =
                    {
                        "$1","/","$1","/","$1","/","$1","/",    "$0","/","$0","/","$0","/","$0","/",
                        "$31","/","$31","/","$31","/","$31","/",    "$21","/","$21","/","$21","/","$21","/",
                        //
                        "/","/","/","/","/","/","/","/",    "/","/","/","/","/","/","/","/",
                         "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",    "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",
                        ////
                    };
                    for (int i = 0; i < rhythm1.Length; i++)
                    {
                        if (rhythm1[i] == "/")
                        {
                            t1 += game.BeatTime(1 / 8f);
                        }
                        else if (rhythm1[i] != "/")
                        {
                            instance.CreateArrows(t1, 5.5f, rhythm1[i]);
                            t1 += game.BeatTime(1 / 8f);
                        }
                    }
                    float t2 = game.BeatTime(26);
                    string[] rhythm2 =
                    {
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        //
                        "($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)","($0)","($21)",
                        "/","/","/","/","/","/",    "/","/","/","/","/","/",
                        ////
                    };
                    for (int i = 0; i < rhythm2.Length; i++)
                    {
                        if (rhythm2[i] == "/")
                        {
                            t2 += game.BeatTime(1 / 6f);
                        }
                        else if (rhythm2[i] != "/")
                        {
                            instance.CreateArrows(t2, 5.5f, rhythm2[i]);
                            t2 += game.BeatTime(1 / 6f);
                        }
                    }
                }
                public static void rhythm04()
                {
                    game.RegisterFunctionOnce("SoulShining", () =>
                    {
                        SetSoul(1);
                    });
                    game.RegisterFunctionOnce("VoidArrow", () =>
                    {
                        Arrow[] a = GetAll<Arrow>();
                        for (int m = 0; m < a.Length; m++)
                        {
                            int x = m;
                            a[x].VoidMode = true;
                        }
                    });
                    game.RegisterFunctionOnce("lA", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(80, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lB", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(160, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lC", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(240, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lD", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(320, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lE", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(400, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lF", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(480, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    game.RegisterFunctionOnce("lG", () =>
                    {
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.55f));
                        alp.Insert(game.BeatTime(4), ValueEasing.EaseInSine(0.55f, 0, game.BeatTime(4)));
                        Line line = new((s) => { return new(560, 240); }, (a) => { return 90; });
                        CreateEntity(line);
                        alp.Run(s => line.Alpha = s);
                        game.DelayBeat(4, () => { line.Dispose(); });
                    });
                    float t = game.BeatTime(6);
                    string[] rhythm =
                    {//这里就是、、、、、、
                        "VoidArrow","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($0)(lE)","/","/","/",    "($0)(lF)","/","/","/",
                        //
                        "($0)(SoulShining)(lD)(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($0)(lD)","/","/","/",    "($0)(lE)","/","/","/",
                        //
                        "($0)(SoulShining)(lC)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "($0)(lC)","/","/","/",    "($0)(lD)","/","/","/",
                        //
                        "($0)(lB)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "($0)(lA)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "($0)(SoulShining)(lD)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(lC)(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "($0)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(SoulShining)(lE)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(lB)(lF)(lA)(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////这里是吧//对，我开修了、、我写特效（
                        "$0(SoulShining)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "$0(SoulShining)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lD)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(SoulShining)(lE)(lG)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(SoulShining)(lD)(lE)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        ////
                        "$0(SoulShining)(lB)(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "/","/","/","/",    "/","/","/","/",    "$0(lA)","/","/","/",    "$0(lB)","/","/","/",
                        //
                        "$0(lC)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        "$0(lB)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
                        //
                        "($0)(SoulShining)(lB)(lC)(lE)(lF)","/","/","/",    "/","/","/","/",    "/","/","/","/",    "/","/","/","/",
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
                        MainEffect.Rain(game.BeatTime(64), 6);
                        float alp = 0.75f;
                        Line a = new(CentreEasing.Stable(new(310, 240)), ValueEasing.Stable(90)) { Alpha = 0.75f };
                        Line b = new(CentreEasing.Stable(new(330, 240)), ValueEasing.Stable(90)) { Alpha = 0.75f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                for (int q = 0; q < game.BeatTime(4); q++)
                                {
                                    AddInstance(new InstantEvent(q * 4 + 2, () =>
                                    {
                                        lines.Alpha = 0.01f;
                                    }));
                                    AddInstance(new InstantEvent(q * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp -= 0.00375f;
                                    }));
                                }
                            }
                        });
                        ValueEasing.EaseBuilder builder = new();
                        builder.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(0, 84, game.BeatTime(4)));
                        builder.Insert(game.BeatTime(14 * 4), ValueEasing.Stable(84));
                        builder.Insert(game.BeatTime(4), ValueEasing.EaseInCubic(84, 0, game.BeatTime(4)));
                        ScreenDrawing.BoundColor = new(95, 137, 154, 60);
                        builder.Run(s => ScreenDrawing.UpBoundDistance = s);
                    });
                    game.DelayBeat(28, () =>
                    {
                        float alp = 0.1f;
                        Line a = new(CentreEasing.Stable(new(320, 225)), ValueEasing.Stable(-45)) { Alpha = 0.75f };
                        Line b = new(CentreEasing.Stable(new(320, 255)), ValueEasing.Stable(-45)) { Alpha = 0.75f };
                        Line[] line = { a, b };
                        game.DelayBeat(0, () =>
                        {
                            foreach (Line lines in line)
                            {
                                CreateEntity(lines);
                                for (int q = 0; q < 75; q++)
                                {
                                    AddInstance(new InstantEvent(q * 4 + 2, () =>
                                    {
                                        lines.Alpha = 0.01f;
                                    }));
                                }
                                for (int i = 0; i < 37; i++)
                                {
                                    AddInstance(new InstantEvent(i * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp += 0.00375f;
                                    }));
                                    AddInstance(new InstantEvent(148 + i * 4, () =>
                                    {
                                        lines.Alpha = alp;
                                        alp -= 0.00375f;
                                    }));
                                }
                            }
                        });
                    });
                    game.DelayBeat(0, () =>
                    {
                        CentreEasing.EaseBuilder ease = new();
                        ValueEasing.EaseBuilder rot1 = new();
                        ValueEasing.EaseBuilder rot2 = new();
                        ValueEasing.EaseBuilder alp = new();
                        rot1.Insert(0, ValueEasing.Stable(-90 - 720));
                        rot2.Insert(0, ValueEasing.Stable(-90 - 720));
                        ease.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.55f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutQuad(-90 - 720, 45 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutQuart(-90 - 720, 0, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.EaseOutSine(0.55f, 0.25f, game.BeatTime(8)));

                        rot1.Insert(0, ValueEasing.Stable(45 + 45));
                        rot2.Insert(0, ValueEasing.Stable(0));
                        ease.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(45 + 45, 165 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(0, 120, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 + 45, 165 + 65 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120, 120 + 65, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 + 65 + 45, 165 - 84 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 + 65, 120 - 84, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 - 84 + 45, 165 - 84 - 58 + 45, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 - 84, 120 - 84 - 58, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(165 - 84 - 58 + 45, 165 - 84 - 58 + 45 - 72, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(120 - 84 - 58, 120 - 84 - 58 - 72, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(-49 + 45, -49 + 45 - 66, game.BeatTime(8)));
                        rot2.Insert(game.BeatTime(8), ValueEasing.EaseOutBack(-94, -94 - 66, game.BeatTime(8)));
                        ease.Insert(game.BeatTime(8), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.Stable(0.25f));

                        rot1.Insert(game.BeatTime(4), ValueEasing.EaseInCirc(-49 + 45 - 66, -90, game.BeatTime(4)));
                        rot2.Insert(game.BeatTime(4), ValueEasing.EaseInCirc(-94 - 66, -90, game.BeatTime(4)));
                        ease.Insert(game.BeatTime(4), CentreEasing.Stable(new(320, 240)));
                        alp.Insert(game.BeatTime(8), ValueEasing.EaseOutQuad(0.25f, 0, game.BeatTime(8)));
                        Line a = new(ease.GetResult(), rot1.GetResult());
                        Line b = new(ease.GetResult(), rot2.GetResult());
                        Line[] l = { a, b };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            lines.Alpha = 0.55f;
                            alp.Run((s) => { lines.Alpha = s; });
                            game.DelayBeat(8, () =>
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    lines.InsertRetention(new(i * 3 + 3, 0.32f - i * 0.03f));
                                }
                            });
                            game.DelayBeat(64, () =>
                            {
                                lines.Dispose();
                            });
                        }
                    });
                    game.DelayBeat(60, () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        CentreEasing.EaseBuilder ce2 = new();
                        CentreEasing.EaseBuilder ce3 = new();
                        CentreEasing.EaseBuilder ce4 = new();
                        ValueEasing.EaseBuilder alp = new();
                        ce1.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce2.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce3.Insert(0, CentreEasing.Stable(new(320, 240)));
                        ce4.Insert(0, CentreEasing.Stable(new(320, 240)));
                        alp.Insert(0, ValueEasing.Stable(0.1f));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 - 126, 240), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 - 42, 240), game.BeatTime(2)));
                        ce3.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 + 42, 240), game.BeatTime(2)));
                        ce4.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(320, 240), new(320 + 126, 240), game.BeatTime(2)));
                        alp.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(0.1f, 0.45f, game.BeatTime(2)));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 - 126, 240), new(320, 240), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 - 42, 240), new(320, 240), game.BeatTime(2)));
                        ce3.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 + 42, 240), new(320, 240), game.BeatTime(2)));
                        ce4.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(320 + 126, 240), new(320, 240), game.BeatTime(2)));
                        alp.Insert(game.BeatTime(2), ValueEasing.EaseOutQuad(0.45f, 0, game.BeatTime(2)));
                        Line a = new(ce1.GetResult(), (s) => { return 90; });
                        Line b = new(ce2.GetResult(), (s) => { return 90; });
                        Line c = new(ce3.GetResult(), (s) => { return 90; });
                        Line d = new(ce4.GetResult(), (s) => { return 90; });
                        Line[] l = { a, b, c, d };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            alp.Run(s => lines.Alpha = s);
                            game.DelayBeat(4, () => { lines.Dispose(); });
                        }
                    });
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
                        Heart.InstantSetRotation(180);
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
                        Heart.InstantSetRotation(-180);
                        TP(new Vector2(320 + 126, 480));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$S", new Vector2(316 - 126, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$D", new Vector2(316 - 42, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$Left", new Vector2(300 + 42, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
                        CreateEntity(new UndyneFight_Ex.Fight.TextPrinter(game.BeatTime(2), "$Down", new Vector2(308 + 126, 380), new UndyneFight_Ex.Fight.TextColorAttribute(Color.Gray)));
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
                    game.DelayBeat(62, () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        CentreEasing.EaseBuilder ce2 = new();
                        ce1.Insert(0, CentreEasing.Stable(new(0, 0)));
                        ce2.Insert(0, CentreEasing.Stable(new(640, 0)));

                        ce1.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(0, 0), new(640, 640), game.BeatTime(2)));
                        ce2.Insert(game.BeatTime(2), CentreEasing.EaseInCirc(new(640, 0), new(0, 640), game.BeatTime(2)));
                        Line a = new(ce1.GetResult(), (s) => { return -45; }) { Alpha = 0.45f };
                        Line b = new(ce2.GetResult(), (s) => { return 45; }) { Alpha = 0.45f };
                        Line[] l = { a, b };
                        foreach (Line lines in l)
                        {
                            CreateEntity(lines);
                            for (int i = 0; i < 8; i++)
                            {
                                lines.InsertRetention(new(i * 4 + 4, 0.55f - i * 0.03f));
                            }
                            game.DelayBeat(2, () => { lines.Dispose(); });
                        }
                    });
                }
                public static void rhythm05()
                {
                    game.RegisterFunctionOnce("LD", () =>
                    {
                        ValueEasing.EaseBuilder builder = new();
                    });
                    game.RegisterFunctionOnce("LU", () =>
                    {

                    });
                    game.RegisterFunctionOnce("RD", () =>
                    {

                    });
                    game.RegisterFunctionOnce("RU", () =>
                    {

                    });
                    game.RegisterFunctionOnce("Move", () =>
                    {
                        ScreenDrawing.UIColor = new Color(20, 20, 20);
                        ScreenDrawing.UISettings.CreateUISurface();
                        float camRotation = 43;
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(game.BeatTime(2), CentreEasing.EaseOutQuad(new(0, 0), new(0, 50), game.BeatTime(2)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

                        RenderProduction production4 = new ScreenDrawing.Shaders.Filter(game.Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(production4);

                        ValueEasing.EaseBuilder camera = new();
                        game.Effect3D.TextureSize = new(640, 1280 * (1 + ScreenDrawing.UpExtending));
                        game.Effect3D.ProjectPointOffect = new(320, game.Effect3D.TextureSize.Y);
                        game.Effect3D.ProjectPoint = new(0, 0, 200);
                        camera.Insert(game.BeatTime(2), ValueEasing.EaseOutSine(0, camRotation, game.BeatTime(2)));
                        camera.Run(rotation =>
                        {
                            game.Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            game.Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            game.Effect3D.CameraPosition = new(0, -900 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    game.RegisterFunctionOnce("StepStart", () =>
                    {
                        game.StepSample.CentreX = 320f;
                        game.StepSample.CentreY = 240f;
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;

                        });
                    });
                    game.RegisterFunctionOnce("Step", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.1f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0.1f, 0.3f, game.BeatTime(1)));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.3f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                            game.splitter.Intensity = s * 30f - 0.1f * 30f;
                        });
                    });
                    game.RegisterFunctionOnce("StepOver", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.1f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.1f, 0f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.5f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                        });
                    });
                    float t = game.BeatTime(4);
                    string[] rhythm =
                    {
                        "(^$3'1.4)(StepStart)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "+0","/","/","/",
                        "R","/","","/",    "+0","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "R","/","+0","/",
                        "","/","+0","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        "R","/","/","/",    "/","/","/","/",
                        ////
                        "(^R'1.4)(Step)","/","/","/",    "R","/","/","/",
                        "/","/","/","/",    "+0","/","/","/",
                        "R","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        //                     
                        "(^R'1.4)(Step)","/","/","/",    "+0","/","/","/",
                        "/","/","/","/",    "R","/","/","/",
                        "R","/","","/",    "","/","/","/",
                        "(R)","/","/","/",    "(+0)","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "+0","/","/","/",
                        "R","/","+0","/",    "/","/","+0","/",
                        "+0","/","+0","/",    "/","/","","/",
                        "R","/","","/",    "+0","/","/","/",
                        //
                        "(^R'1.4)(Step)","/","/","/",    "+0","/","+0","/",
                        "","/","R","/",    "+0","/","/","/",
                        "R","/","+0","/",    "+0","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        ////
                        "(R)(Step)","/","/","/",    "+0","/","","/",
                        "/","/","/","/",    "R","/","/","/",
                        "(^$2'1.2)","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        //
                        "(R)(Step)","/","/","/",    "+0","/","","/",
                        "/","/","/","/",    "R","/","/","/",
                        "(^$0'1.2)","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        //
                        "(R)(Step)","/","/","/",    "","/","","/",
                        "(^$2'1.2)","/","/","/",    "","/","/","/",
                        "(R)(Step)","/","/","/",    "","/","","/",
                        "(^$0'1.2)","/","/","/",    "","/","/","/",
                        //
                        "(R)(Step)","/","/","/",    "+0","/","+0","/",
                        "/","/","R","/",    "+0","/","/","/",
                        "(^$2'1.2)","/","+0","/",    "+0","/","/","/",
                        "R","/","/","/",    "R","/","/","/",
                        ////
                        "(R)(Step)","/","/","/",    "+0","/","","/",
                        "/","/","/","/",    "R","/","/","/",
                        "(^$0'1.2)","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        //
                        "(R)(Step)","/","/","/",    "+0","/","","/",
                        "/","/","/","/",    "R","/","/","/",
                        "(R)(Step)","/","","/",    "/","/","/","/",
                        "R","/","/","/",    "+0","/","/","/",
                        //
                        "($0)(Step)","/","/","/",    "$0","/","$0","/",
                        "","/","$2","/",    "$2","/","/","/",
                        "($2)(Step)","/","","/",    "$2","/","$2","/",
                        "","/","$0","/",    "$0","/","$0","/",
                        //
                        "(R)(StepOver)","/","/","/",    "+0","/","+0","/",
                        "/","/","R","/",    "+0","/","/","/",
                        "($3)","/","+0","/",    "+0","/","/","/",
                        "($3'0.2)","/","/","/",    "","/","/","/",
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

                    });
                    game.DelayBeat(0, () =>
                    {

                    });
                }
                public static void rhythm06A()
                {
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
                    game.StepSample.CentreY = -800f;
                    game.DelayBeat(2, () =>
                    {
                        #region Split
                        Heart.Split();
                        SetBoxMission(0);
                        MainEffect.SoftSetBox(new(320 - 126, 480 - 84), 84, 84, game.BeatTime(1), 0, 1);
                        SetPlayerMission(0);
                        Heart.Alpha = 0.2f;

                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(0); Heart.Shields.RemoveShield(Heart.Shields.BShield); });
                        Heart.InstantSetRotation(180);
                        MainEffect.SoftTP(new(320 - 126, 480 - 84), game.BeatTime(1), 0, 1);

                        Heart.Split();
                        SetBoxMission(1);
                        MainEffect.SoftSetBox(new(320 - 42, 480 - 84), 84, 84, game.BeatTime(1), 1, 1);
                        SetPlayerMission(2);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(2); Heart.Shields.RemoveShield(Heart.Shields.RShield); });
                        Heart.InstantSetRotation(90);
                        MainEffect.SoftTP(new(320 + 42, 480 - 84), game.BeatTime(1), 2, 1);
                        SetBoxMission(2);
                        MainEffect.SoftSetBox(new(320 + 42, 480 - 84), 84, 84, game.BeatTime(1), 2, 1);
                        SetPlayerMission(1);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(1); Heart.Shields.RemoveShield(Heart.Shields.BShield); });
                        Heart.InstantSetRotation(-90);
                        MainEffect.SoftTP(new(320 - 42, 480 - 84), game.BeatTime(1), 1, 1);

                        Heart.Split();
                        SetBoxMission(3);
                        MainEffect.SoftSetBox(new(320 + 126, 480 - 84), 84, 84, game.BeatTime(1), 3, 1);
                        SetPlayerMission(3);
                        Heart.Alpha = 0.2f;
                        Heart.controlingBox.GreenSoulAlpha = 0f;
                        game.DelayBeat(0, () => { SetPlayerMission(3); Heart.Shields.RemoveShield(Heart.Shields.RShield); });
                        Heart.InstantSetRotation(-180);
                        MainEffect.SoftTP(new(320 + 126, 480 - 84), game.BeatTime(1), 3, 1);
                        #endregion
                        #region Line
                        for (int a = 0; a < 5; a++)
                        {
                            Line l1 = new(new Vector2(320 - 126 - 42 + a * 84, 480), new Vector2(320 - 126 - 42 + a * 84, -800)) { Alpha = 0 };
                            CreateEntity(l1);
                            l1.AlphaIncrease(game.BeatTime(2), 0.5f);
                        }
                        Line l2 = new(new Vector2(320 - 126 - 42, 480 - 84 - 42), new Vector2(320 - 126 - 42 + 4 * 84, 480 - 84 - 42)) { Alpha = 0 };
                        CreateEntity(l2);
                        l2.AlphaIncrease(game.BeatTime(2), 1f);
                        #endregion
                    });
                    #region 
                    game.RegisterFunctionOnce("k0", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k1", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k2", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k3", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h0", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h1", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h2", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h3", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.LateWaitingScale = 0.16f;
                            ar.Scale = 2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            ar.VoidMode = true;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunction("Lk0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 1, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Lk3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 0, 0, ArrowAttribute.RotateL);
                    });
                    game.RegisterFunction("Rk0n", () =>
                    {
                        SetPlayerMission(0);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 1, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk1n", () =>
                    {
                        SetPlayerMission(1);
                        CreateArrow(game.BeatTime(3), 0, 6.25f, 1, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk2n", () =>
                    {
                        SetPlayerMission(2);
                        CreateArrow(game.BeatTime(3), 2, 6.25f, 0, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Rk3n", () =>
                    {
                        SetPlayerMission(3);
                        CreateArrow(game.BeatTime(3), 1, 6.25f, 0, 0, ArrowAttribute.RotateR);
                    });
                    game.RegisterFunction("Step", () =>
                    {

                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0.2f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0.1f, 0.3f, game.BeatTime(1)));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseInSine(0.3f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.8f;
                            game.Blur.Sigma = s * 10f - 0.1f * 10f;
                        });
                    });
                    game.RegisterFunction("Polor", () =>
                    {
                        ValueEasing.EaseBuilder e1 = new();
                        e1.Insert(0, ValueEasing.Stable(0f));
                        e1.Insert(game.BeatTime(1), ValueEasing.EaseOutQuint(0f, 0.1f, game.BeatTime(1)));
                        e1.Insert(1, ValueEasing.Stable(0));
                        e1.Run((s) =>
                        {

                            game.StepSample.Intensity = s * 0.8f;

                        });
                        ValueEasing.EaseBuilder e2 = new();
                        e2.Insert(0, ValueEasing.Stable(0f));
                        e2.Insert(game.BeatTime(96), ValueEasing.SinWave(0.1f, game.BeatTime(8), 0));
                        e2.Insert(1, ValueEasing.Stable(0));
                        e2.Run((s) =>
                        {

                            game.Polar.Intensity = s * 0.07f;
                            game.Polar.IType = false;
                        });
                    });
                    #endregion
                    #region UI
                    Color uic = ScreenDrawing.UIColor;
                    ValueEasing.EaseBuilder scale = new();
                    scale.Insert(0, ValueEasing.Stable(1));
                    scale.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(1, 0.1f, game.BeatTime(4)));
                    scale.Run(scale => { ScreenDrawing.UIColor = uic * scale; ScreenDrawing.HPBar.HPExistColor = new Color(90, 112, 125) * scale; ScreenDrawing.HPBar.HPLoseColor = Color.Gray * scale; });
                    #endregion
                    ValueEasing.EaseBuilder len = new();
                    len.Insert(game.BeatTime(64), ValueEasing.Stable((4 * 84)));
                    len.Insert(game.BeatTime(1), ValueEasing.EaseInSine(4 * 84, 6 * 84, game.BeatTime(1)));
                    Line l = new(CentreEasing.Stable(320, 480 - 84 - 42), ValueEasing.Stable(0), len.GetResult()) { Depth = 0.99f, Alpha = 0 };
                    CreateEntity(l);
                    l.AlphaIncrease(game.BeatTime(8), 1);
                    game.BarrageCreate(0, game.BeatTime(1), 7.5f, new string[]
                    {
                        "(Polor)","","","",    "","","","",
                        "","","","",    "","","","",
                        "(k2)","","h2","",    "k2","","h2","",
                        "k0(Step)","","h0","",    "k0","","h0","",

                        "(h2)(h1)","","h0","",    "h1","","h2","",
                        "h3","","h1","",    "h2","","h0","",
                        "(k1)","","h1","",   "k1","","h1","",
                        "k3(Step)","","h3","",   "k3","","h3","",

                        "(h1)(h0)","","h2","",    "h1","","h0","",
                        "h1","","h3","",    "h2","","h1","",
                        "(k2)","","h2","",    "k2","","h2","",
                        "(k0)(Step)","","h0","",    "k0","","h0","",

                        "!!3/6","h2","h1","h2","h3","h2","h1",
                        "!!3/6","h0","h1","h2","h3","h2","h1",
                        "!!3/6","h0","h1","h2","h3","h2","h1",
                        "!!3/6","h0(Step)","h1","h2","h3","h0","h1",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h0","",   "h1","","h2","",
                        "k3","","h3","",   "k1","","h1","",
                        "k2(Step)","","h2","",   "k0","","h0","",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h2","",   "h0","","h1","",
                        "k0","","h0","",   "k2","","h2","",
                        "k0(Step)","","h0","",   "k2","","h2","",

                        "!!3/6","h0","h1","h2","h3","h0","h1",
                        "!!3/6","h2","h3","h0","h1","h2","h3",
                        "!!3/6","h2","h1","h0","h3","h2","h1",
                        "!!3/6","h0(Step)","h3","h2","h1","h0","h1",

                        "!!3/6","h3","h2","h0","h1","h3","h2",
                        "!!3/6","h0","h1","h3","h2","h0","h1",
                        "!!3/6","h2","h3","h1","h0","h2","h3",
                        "!!3/6","h1(Step)","h0","h2","h3","h2","h1",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h0","",   "h1","","h2","",
                        "k3","","h3","",   "k1","","h1","",
                        "k2(Step)","","h2","",   "k0","","h0","",

                        "(h0)(h1)","","h3","",   "h2","","h1","",
                        "h2","","h0","",   "h1","","h2","",
                        "k3","","h3","",   "k2","","h2","",
                        "k1(Step)","","h1","",   "k0","","h0","",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h2","",   "h0","","h1","",
                        "k3","","h3","",   "k2","","h2","",
                        "k1(Step)","","h1","",   "k0","","h0","",

                        "h2","","h3","",   "h0","","h1","",
                        "h2","","h3","",   "h0","","h1","",
                        "h0","","h1","",   "h2","","h3","",
                        "!!3/6","k0(Step)","h0","h0","k3","h3","h3",

                        "(h3)(h2)","","h1","",   "h0","","h1","",
                        "h0","","h1","",   "h0","","h1","",
                        "(k0)(h3)","","h2","",   "(k1)(h3)","","h2","",
                        "(k0)(h3)(Step)","","h2","",   "(k1)(h3)","","h2","",

                        "h0","","h1","",   "(h0)(k3)","","h1","",
                        "(h0)(k2)","","h1","",   "(h0)(k3)","","h1","",
                        "h3","","h2","",   "h1","","h0","",
                        "h1(Step)","","h3","",   "h2","","h0","",

                        "(h2)(h3)","","h0","",   "h1","","h2","",
                        "h3","","h2","",   "h0","","h1","",
                        "(k0)(h2)","","h3","",   "(k1)(h2)","","h3","",
                        "(k0)(h2)(Step)","","h3","",   "(k1)(h2)","","h3","",

                        "k0","","h0","",   "k2","","h2","",
                        "k0","","h0","",   "k2","","h2","",
                        "k1","","h1","",   "k1","","h1","",
                        "k3(Step)","","h3","",   "k1","","h1","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                    });

                    game.DelayBeat(60, () => { l.AlphaDecrease(game.BeatTime(4)); });

                }
                public static void _6k()
                {
                    game.To6KIn3D();
                    Player.Heart ExHeart1 = CreateHeart(new CollideRect(320 - 168, 480 - 84, 84, 84));
                    Player.Heart ExHeart2 = CreateHeart(new CollideRect(320 + 168, 480 - 84, 84, 84));
                    game.RegisterFunctionOnce("h0", () =>
                    {
                        SetPlayerMission(ExHeart1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f;
                            ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h1", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h2", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h3", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h4", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("h5", () =>
                    {
                        SetPlayerMission(ExHeart2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.VoidMode = true;
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Hold;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k0", () =>
                    {
                        SetPlayerMission(ExHeart1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f;
                            ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k1", () =>
                    {
                        SetPlayerMission(0);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k2", () =>
                    {
                        SetPlayerMission(1);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 1, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k3", () =>
                    {
                        SetPlayerMission(2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 2, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k4", () =>
                    {
                        SetPlayerMission(3);
                        Arrow ar = MakeArrow(game.BeatTime(3), 1, 18, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    game.RegisterFunctionOnce("k5", () =>
                    {
                        SetPlayerMission(ExHeart2);
                        Arrow ar = MakeArrow(game.BeatTime(3), 0, 18f, 0, 0);
                        game.DelayBeat(0, () =>
                        {
                            ar.Scale = 2.25f;
                            ar.LateWaitingScale = 0.2f; ar.JudgeType = Arrow.JudgementType.Tap;
                            CreateEntity(ar);
                        });
                    });
                    SetPlayerBoxMission(ExHeart1);
                    MainEffect.SoftTP(new(320 - 126 - 84, 480 - 84), game.BeatTime(3), 4, 1);
                    MainEffect.SoftSetBox(new(320 - 126 - 84, 480 - 84), 84, 84, game.BeatTime(3), 4, 1);
                    ExHeart1.InstantSetRotation(90);
                    SetSoul(1);
                    game.DelayBeat(0, () => { ExHeart1.Shields.RemoveShield(ExHeart1.Shields.BShield); });
                    SetPlayerBoxMission(ExHeart2);
                    MainEffect.SoftTP(new(320 + 126 + 84, 480 - 84), game.BeatTime(3), 5, 1);
                    MainEffect.SoftSetBox(new(320 + 126 + 84, 480 - 84), 84, 84, game.BeatTime(3), 5, 1);
                    ExHeart2.InstantSetRotation(-90);
                    SetSoul(1);
                    game.DelayBeat(0, () => { ExHeart2.Shields.RemoveShield(ExHeart2.Shields.RShield); });
                    Line l = new(new Vector2(320 - 3 * 84, 480), new Vector2(320 - 3 * 84, -800)) { DrawingColor = ScreenDrawing.ThemeColor, Alpha = 0 };
                    CreateEntity(l);
                    l.AlphaIncrease(game.BeatTime(1), 0.5f);
                    Line l2 = new(new Vector2(320 + 3 * 84, 480), new Vector2(320 + 3 * 84, -800)) { DrawingColor = ScreenDrawing.ThemeColor, Alpha = 0 };
                    CreateEntity(l2);
                    l2.AlphaIncrease(game.BeatTime(1), 0.5f);
                    game.BarrageCreate(0, game.BeatTime(1), 8, new string[]
                    {
                        "(k0)","","h0","",   "k1","","h1","",
                        "(k2)","","h2","",   "k3","","h3","",
                        "(k4)","","h4","",   "k5","","h5","",
                        "(k4)","","h4","",   "k3","","h3","",

                        "(k2)","","h2","",   "k1","","h1","",
                        "(k0)","","h0","",   "k1","","h1","",
                        "(k2)","","h2","",   "k3","","h3","",
                        "(k4)","","h4","",   "k5","","h5","",

                        "(k4)","","h4","",   "k3","","h3","",
                        "(k2)","","h2","",   "k1","","h1","",
                        "(k0)","","h0","",   "k1","","h1","",
                        "(k2)","","h2","",   "k3","","h3","",

                        "(k0)","","h0","",   "k2","","h2","",
                        "!!3/6","h2","h1","h0","h1","h2","h3",
                        "!!3/6","h4","h5","h4","h3","h2","h1",
                        "(k0)","","h0","",   "k2","","h2","",
                        //
                        "(k3)","","h3","",   "k4","","h4","",
                        "(k5)","","h5","",   "k1","","h1","",
                        "(k2)","","h2","",   "k3","","h3","",
                        "(k5)","","h5","",   "k1","","h1","",

                        "(k2)","","h2","",   "k3","","h3","",
                        "(k5)","","h5","",   "k1","","h1","",
                        "(k2)","","h2","",   "k3","","h3","",
                        "(k4)","","h4","",   "k1","","h1","",

                        "(k2)","","h2","",   "k3","","h3","",
                        "(k3)","","h3","",   "k2","","h2","",
                        "(k0)","","h0","",   "k5","","h5","",
                        "(k5)","","h5","",   "k0","","h0","",

                        "(k0)","","h0","",   "k2","","h2","",
                        "!!3/12","(h0)","h1","h2","h3","h4","h5",
                        "h0","h1","h2","h3","h4","h5",
                        "h0",
                        //
                    });


                    //test
                    game.DelayBeat(34, game.Apply2D);
                }
                public static void over()
                {
                    SetPlayerMission(0);
                    game.RegisterFunctionOnce("Rot", () =>
                    {
                        ScreenDrawing.CameraEffect.Rotate(360, game.BeatTime(10));
                    });
                    game.RegisterFunctionOnce("linel", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(0, 240), new(640, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("liner", () =>
                    {
                        Line a = new(CentreEasing.EaseOutQuart(new(640, 240), new(0, 240), game.BeatTime(0.5f)), (s) => { return 90; }) { Alpha = 0.55f };
                        CreateEntity(a);
                        game.DelayBeat(1, () =>
                        {
                            a.Dispose();
                        });
                    });
                    game.RegisterFunctionOnce("Scale", () =>
                    {
                        for (int i = 0; i < game.BeatTime(1.5f); i++)
                        {
                            DrawingUtil.Linerotate lL = new(0, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lR = new(640, 240, 90, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lU = new(320, -80, 0, game.BeatTime(3), 0.85f);
                            DrawingUtil.Linerotate lD = new(320, 480 + 80, 0, game.BeatTime(3), 0.85f);
                            float a = 0;
                            AddInstance(new InstantEvent(i * 2.4f, () =>
                            {
                                CreateEntity(lL);
                                CreateEntity(lR);
                                CreateEntity(lU);
                                CreateEntity(lD);
                                AddInstance(new TimeRangedEvent(30, () =>
                                {
                                    lL.xCenter += 8;
                                    lR.xCenter -= 8;
                                    lU.yCenter += 8;
                                    lD.yCenter -= 8;
                                    lL.alpha -= 0.05f;
                                    lR.alpha -= 0.05f;
                                    lU.alpha -= 0.05f;
                                    lD.alpha -= 0.05f;
                                    lL.rotate -= 5.5f;
                                    lR.rotate -= 5.5f;
                                    lU.rotate -= 5.5f;
                                    lD.rotate -= 5.5f;
                                    a++;
                                }));
                            }));
                        }
                        float p = 0;
                        game.ForBeat(4, () =>
                        {
                            Heart.InstantSetRotation(p * p * 0.08f);
                            ScreenDrawing.ScreenScale = p * 0.02f + 1;
                            p++;
                        });
                    });
                    game.RegisterFunctionOnce("ScaleBack", () =>
                    {
                        DrawingUtil.LerpScreenScale(game.BeatTime(16), 1, 0.03f);
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(Heart.Rotation % 360));
                        ve.Insert(game.BeatTime(32), ValueEasing.EaseOutSine(Heart.Rotation % 360, 360 * 2, game.BeatTime(32)));
                        ve.Run((s) => { Heart.InstantSetRotation(s); });
                    });
                    game.RegisterFunctionOnce("End", () =>
                    {
                        game.End();
                    });
                    game.BarrageCreate(0, game.BeatTime(1), 5.5f, new string[]
                    {
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "$01","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",

                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",

                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        //
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",

                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",
                        "+11","","","",   "+01","","","",

                        "($2)(linel)","","$31'1.3","",   "($2)(liner)","","$31'1.3","",
                        "($2)(linel)","","$31'1.3","",   "($2)(liner)","","$31'1.3","",
                        "($0)(linel)","","$11'1.3","",   "($0)(liner)","","$11'1.3","",
                        "($0)(linel)","","$11'1.3","",   "($0)(liner)","","$11'1.3","",

                        "($0)(linel)(Scale)","","(<$11'1.3)(liner)","",   "($0)(linel)","","(<$11'1.5)(liner)","",
                        "($0)(linel)","","(<$11'1.3)(liner)","",   "($0)(linel)","","(<$11'1.5)(liner)","",
                        "!!3/6","(<$0'1.3)","(>$11)","(>$0'1.3)","(<$11)","(<$0'1.3)","(>$11)",
                        "!!3/6","(<$0'1.3)","(<$11)","(>$0'1.3)","(>$11)","(<$0'1.3)","(<$11)",

                        "(<$0'1.3)(ScaleBack)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "End","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                    game.BarrageCreate(0, game.BeatTime(1), 6.5f, new string[]
                    {
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "(convL)","","","",   "(convL)","","","",

                        "","","","",   "","","","",
                        "","","","",   "(convR)","","","",
                        "(convR)","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "$1(convL)","","+0","",   "+0(convL)","","+0","",

                        "+0","","","",   "","","","",
                        "","","","",   "$1(convR)","","+0","",
                        "+0(convR)","","+0","",   "+0","","","",
                        "","","","",   "","","","",
                        //
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "$1(convL)","","+0","",   "+0(convL)","","+0","",

                        "+0","","","",   "","","","",
                        "","","","",   "$1(convR)","","+0(convR)","",
                        "+0","","+0","",   "+0","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });
                }
            }
            private static RenderProduction cameraProduction;
            private void Apply3D()
            {
                float camRotation = 48;
                DelayBeat(0, () =>
                {
                    ScreenDrawing.UpExtending = 1.75f;
                    ScreenDrawing.DownExtending = 0.25f;

                    CentreEasing.EaseBuilder screen = new();
                    screen.Insert(BeatTime(2), CentreEasing.EaseOutQuint(new(0, 0), new(0, -10), BeatTime(2)));
                    screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

                    cameraProduction = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                    ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);

                    ValueEasing.EaseBuilder camera = new();
                    Effect3D.TextureSize = new(640, 480 * (1 + ScreenDrawing.UpExtending + ScreenDrawing.DownExtending));
                    Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y * (1 + ScreenDrawing.UpExtending) / (1 + ScreenDrawing.UpExtending + ScreenDrawing.DownExtending));
                    Effect3D.ProjectPoint = new(0, 0, 200);

                    ValueEasing.EaseBuilder posMove = new();

                    int beatCount = 2;

                    posMove.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(200, 60, BeatTime(beatCount)));
                    posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                    camera.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(0, camRotation, BeatTime(beatCount)));
                    camera.Run(rotation =>
                    {
                        Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                        Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                        Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                    });
                });
            }
            private void To6KIn3D()
            {
                float camRotation = 48;
                DelayBeat(0, () =>
                {
                    ValueEasing.EaseBuilder camera = new();

                    int beatCount = 4;

                    camera.Insert(BeatTime(beatCount), ValueEasing.EaseInSine(0, 17, BeatTime(beatCount)));
                    camera.Run(position =>
                    {
                        Effect3D.CameraPosition = new(0, -240 + Sin(camRotation) * -2 * position, -position * 2);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                    });
                });
            }
            private void Apply2D()
            {
                Color uic = Color.White;
                ValueEasing.EaseBuilder Scale = new();
                Scale.Insert(0, ValueEasing.Stable(0.1f));
                Scale.Insert(game.BeatTime(4), ValueEasing.EaseOutQuad(0.1f, 1f, game.BeatTime(4)));
                Scale.Run(Scale => { ScreenDrawing.UIColor = uic * Scale; ScreenDrawing.HPBar.HPExistColor = new Color(90, 112, 125) * Scale; ScreenDrawing.HPBar.HPLoseColor = Color.Gray * Scale; });
                Line[] ls = GetAll<Line>();
                for (int a = 0; a < ls.Length; a++)
                {
                    int x = a;
                    ls[x].AlphaDecrease(BeatTime(1));
                }
                ValueEasing.EaseBuilder camera = new();
                ValueEasing.EaseBuilder posMove = new();
                float camRotation = 48;

                float beatCount = 1.75f;

                posMove.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(170, 200, BeatTime(beatCount)));
                posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                camera.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(camRotation, 0, BeatTime(beatCount)));
                camera.Run(rotation =>
                {
                    Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                    Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                    Effect3D.CameraPosition = new(0, -240 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                });
                DelayBeat(beatCount, () =>
                {
                    ScreenDrawing.DownExtending = ScreenDrawing.UpExtending = 0;
                    cameraProduction.Dispose();
                });
                ValueEasing.EaseBuilder scale = new();
                scale.Insert(BeatTime(beatCount), ValueEasing.EaseOutSine(ScreenDrawing.ScreenScale, 1, BeatTime(beatCount)));
                scale.Run(s => ScreenDrawing.ScreenScale = s);
                CentreEasing.EaseBuilder screen = new();
                screen.Insert(BeatTime(beatCount), CentreEasing.EaseOutQuad(new(0, 60), new(0, 0), BeatTime(beatCount)));
                screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);
                DelayBeat(beatCount, () =>
                {

                    SetBoxMission(0);
                    SetBox(new Vector2(320, 240), 84, 84);
                    SetBoxMission(1);
                    SetBox(new Vector2(320, 240), 84, 84);
                    SetBoxMission(2);
                    SetBox(new Vector2(320, 240), 84, 84);
                    SetBoxMission(3);
                    SetBox(new Vector2(320, 240), 84, 84);
                    SetBoxMission(4);
                    SetBox(new Vector2(320, 240), 84, 84);
                    SetBoxMission(5);
                    SetBox(new Vector2(320, 240), 84, 84);
                    SetPlayerMission(0);
                    Heart.GiveForce(0, 0);
                    Heart.Alpha = 1;
                    Heart.controlingBox.GreenSoulAlpha = 0.5f;
                    SetPlayerMission(1);
                    Heart.GiveForce(0, 0);
                    Heart.Alpha = 1;
                    Heart.controlingBox.GreenSoulAlpha = 0.5f;
                    SetPlayerMission(2);
                    Heart.GiveForce(0, 0);
                    Heart.Alpha = 1;
                    Heart.controlingBox.GreenSoulAlpha = 0.5f;
                    SetPlayerMission(3);
                    Heart.GiveForce(0, 0);
                    Heart.Alpha = 1;
                    Heart.controlingBox.GreenSoulAlpha = 0.5f;
                    SetPlayerMission(4);
                    Heart.GiveForce(0, 0);
                    Heart.Alpha = 1;
                    Heart.controlingBox.GreenSoulAlpha = 0.5f;
                    SetPlayerMission(5);
                    Heart.GiveForce(0, 0);
                    Heart.Alpha = 1;
                    Heart.controlingBox.GreenSoulAlpha = 0.5f;
                    Player.hearts[0].Teleport(new(320, 240));
                    Player.hearts[1].Teleport(new(320, 240));
                    Player.hearts[2].Teleport(new(320, 240));
                    Player.hearts[3].Teleport(new(320, 240));
                    Player.hearts[4].Teleport(new(320, 240));
                    Player.hearts[5].Teleport(new(320, 240));

                    DelayBeat(1, () =>
                    {
                        Player.hearts[1].Merge(Player.hearts[0]);
                        Player.hearts[3].Merge(Player.hearts[2]);
                        Player.hearts[5].Merge(Player.hearts[4]);
                        Player.hearts[4].Merge(Player.hearts[2]);
                        Player.hearts[2].Merge(Player.hearts[0]);
                        SetPlayerMission(0);
                        DelayBeat(0, () =>
                        {
                            SetPlayerMission(0);
                            Heart.Shields.AddShield(Heart.Shields.BShield);
                        });
                        SetSoul(1);
                    });
                });
                DelayBeat(0, () =>
                {
                    ValueEasing.EaseBuilder e1 = new();
                    e1.Insert(0, ValueEasing.Stable(0.7f));
                    e1.Insert(BeatTime(6), ValueEasing.EaseOutQuint(0.7f, 0, BeatTime(6)));
                    e1.Insert(1, ValueEasing.Stable(0));
                    e1.Run((s) =>
                    {

                        Blur.Sigma = s;
                        StepSample.Intensity = s;
                        splitter.Intensity = 60f * s;
                    });
                });
            }
            private void End()
            {
                int cur = 0;

                if (PlayerManager.CurrentUser != null)
                {
                    SaveInfo custom = PlayerManager.CurrentUser.Custom;
                    if (!custom.Nexts.ContainsKey("reEndTime"))
                        EndSong();

                    cur = (int)CurrentDifficulty;
                    if (cur == 0) cur = 2;
                    if (cur == 3) cur = 4;

                    if (custom.Nexts.ContainsKey("reTranscendence"))
                    {
                        int cmp = custom.Nexts["reTranscendence"].Nexts["info"].IntValue;
                        if (cur <= cmp) EndSong();
                    }
                }
                else EndSong();
                RegisterFunctionOnce("Flicker", () =>
                {
                    ScreenDrawing.MakeFlicker(Color.Black * 0.6f);
                });
                RegisterFunctionOnce("Fade", () =>
                {
                    ScreenDrawing.SceneOut(Color.Black, BeatTime(6));
                });
                RegisterFunctionOnce("Reset", () =>
                {
                    GameStates.ResetScene(new TranscendenceAnomaly(cur));
                });
                string[] rhythm = {
                    "Flicker", "", "Flicker", "", "Flicker", "", "Flicker", "",
                    "Fade", "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "",
                    "", "", "", "", "Reset", "", "", "",
                };
                BarrageCreate(0, BeatTime(1), 0, rhythm);
            }
            #region

            public void Hard()
            {
                Normal();
            }
            public void Easy()
            {
                if (Gametime < 0) return;
            }

            public void Extreme()
            {
                if (Gametime < 0) return;
                if (Gametime == 0)
                {
                    GameStates.ResetScene(new EndTimeAnomaly());
                }
            }
            #endregion
            public void Normal()
            {
                if (InBeat(-2.24f)) { NorBarrage.effect00(); ScreenDrawing.UISettings.CreateUISurface(); }
                if (InBeat(2)) NorBarrage.effect01();
                if (InBeat(2 - 2)) NorBarrage.rhythm01();
                if (InBeat(2 + 4 * 9)) NorBarrage.effect02();
                if (InBeat(2 + 4 * 9 - 2)) NorBarrage.rhythm02();
                if (InBeat(2 + 4 * 17)) NorBarrage.effect03();
                if (InBeat(2 + 4 * 17 - 2)) NorBarrage.rhythm03();
                if (InBeat(2 + 4 * 25 - 6)) NorBarrage.rhythm04();
                if (InBeat(2 + 4 * 25)) NorBarrage.effect04();
                if (InBeat(2 + 4 * 41 - 4)) NorBarrage.rhythm05();
                if (InBeat(2 + 4 * 41)) NorBarrage.effect05();
                if (InBeat(2 + 4 * 57 - 2)) Apply3D();
                if (InBeat(2 + 4 * 57 - 3)) NorBarrage.rhythm06A();
                if (InBeat(2 + 4 * 57 - 3)) NorBarrage.rhythm06B();
                if (InBeat(2 + 4 * (57 + 16) - 3)) NorBarrage._6k();
                if (InBeat(2 + 4 * (57 + 16) - 3 + 34)) NorBarrage.over();
            }
            public void Noob()
            {
                if (InBeat(-2.24f)) { NoobBarrage.effect00(); ScreenDrawing.UISettings.CreateUISurface(); }
                if (InBeat(2)) NoobBarrage.effect01();
                if (InBeat(2 - 2)) NoobBarrage.rhythm01();
                if (InBeat(2 + 4 * 9)) NoobBarrage.effect02();
                if (InBeat(2 + 4 * 9 - 2)) NoobBarrage.rhythm02();
                if (InBeat(2 + 4 * 17)) NoobBarrage.effect03();
                if (InBeat(2 + 4 * 17 - 2)) NoobBarrage.rhythm03();
                if (InBeat(2 + 4 * 25 - 6)) NoobBarrage.rhythm04();
                if (InBeat(2 + 4 * 25)) NoobBarrage.effect04();
                if (InBeat(2 + 4 * 41 - 4)) NoobBarrage.rhythm05();
                if (InBeat(2 + 4 * 41)) NoobBarrage.effect05();
                if (InBeat(2 + 4 * 57 - 2)) Apply3D();
                if (InBeat(2 + 4 * 57 - 3)) NoobBarrage.rhythm06A();
                if (InBeat(2 + 4 * 57 - 3)) NoobBarrage.rhythm06B();
                if (InBeat(2 + 4 * (57 + 16) - 3)) NoobBarrage._6k();
                if (InBeat(2 + 4 * (57 + 16) - 3 + 34)) NoobBarrage.over();
            }
            public void ExtremePlus()//?
            {
                if (InBeat(-2.24f)) { Barrage.effect00(); ScreenDrawing.UISettings.CreateUISurface(); }
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
                if (InBeat(2 + 4 * 57 - 2)) Apply3D();
                if (InBeat(2 + 4 * 57 - 3)) Barrage.rhythm06A();
                if (InBeat(2 + 4 * 57 - 3)) Barrage.rhythm06B();
                if (InBeat(2 + 4 * (57 + 16) - 3)) Barrage._6k();
                if (InBeat(2 + 4 * (57 + 16) - 3 + 34)) Barrage.over();
            }
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production1;
            RenderProduction production2;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;
            GlobalResources.Effects.CameraShader Effect3D;
            public void Start()
            {
                AutoEnd = false;

                StepSample = Shaders.StepSample;
                production1 = new ScreenDrawing.Shaders.Filter(Shaders.StepSample, 0.99f);
                production2 = new ScreenDrawing.Shaders.Filter(Shaders.Polar, 0.8f);
                RenderProduction production3 = Blur = new ScreenDrawing.Shaders.Blur(0.5f);
                splitter = new ScreenDrawing.Shaders.RGBSplitting(0.9f) { Disturbance = false };
                Polar = Shaders.Polar;
                ScreenDrawing.SceneRendering.InsertProduction(splitter);
                ScreenDrawing.SceneRendering.InsertProduction(production1);
                ScreenDrawing.SceneRendering.InsertProduction(production2);
                Effect3D = Shaders.Camera;

                game = this;
                GametimeDelta = BeatTime(-2.35f);
                //   GametimeDelta = BeatTime(2 + 4 * 57 - 2) - 52.5f;
                //   PlayOffset = BeatTime(2 + 4 * 57 - 2);

                game.StepSample.CentreX = 320f;
                game.StepSample.CentreY = 240f;
                Polar.Intensity = 0.0f;
                splitter.Intensity = 0.0f;
                Blur.Sigma = 0.0f;
                StepSample.Intensity = 0f;
                Effect3D.CameraRotation = new(0, 0, 0);
                HeartAttribute.BuffedLevel = 3;
                HeartAttribute.MaxHP = 100;
                HeartAttribute.Speed = 3.0f;
                HeartAttribute.SoftFalling = true;
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(new(320, 240));
                SetSoul(1);
                ScreenDrawing.HPBar.Vertical = true;
                ScreenDrawing.UIColor = Color.White;
                ScreenDrawing.HPBar.HPExistColor = new(90, 112, 125);
                ScreenDrawing.HPBar.HPLoseColor = Color.Gray;
                ScreenDrawing.HPBar.AreaOccupied = new CollideRect(20, 140, 20, 220);
                ScreenDrawing.ThemeColor = new(90, 112, 125);
            }
        }
    }
}