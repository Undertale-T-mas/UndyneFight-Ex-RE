using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
namespace AprilExtends
{
    public class Seraphim : IChampionShip
    {
        public Seraphim()
        {

            difficulties = new();
            difficulties.Add("DIV.0 Lv.20.0", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (186f / 60f))
            {

            }
            public string Music => "Seraphim (Long Ver.)";

            public string FightName => "Seraphim (Long Ver.)";
            private class ThisImformation : SongImformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 20.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 20.0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Extreme, 21.5f),
                        }
                    );
                public override string BarrageAuthor => "Woem";
                public override string AttributeAuthor => "Woem";
                public override string SongAuthor => "Yoohsic Roomz";
            }
            public SongImformation Attributes => new ThisImformation();
            private bool notRegistered = true;
            public static Game game;
            public void SetOffset(Arrow arrow, float offset)
            {
                if (arrow.Way == 0) arrow.Offset = new(0, offset);
                if (arrow.Way == 1) arrow.Offset = new(offset, 0);
                if (arrow.Way == 2) arrow.Offset = new(0, offset);
                if (arrow.Way == 3) arrow.Offset = new(-offset, 0);
            }
            GlobalResources.Effects.StepSampleShader StepSample;

            ScreenDrawing.Shaders.Blur Blur;

            GlobalResources.Effects.PolarShader Polar;
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
            public void ExtremePlus()
            {
                TagsArrow();
                if (InBeat(0))
                {
                    RegisterFunctionOnce("BoxUp", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(6), CentreEasing.EaseOutQuart(new(320, 600), new(320, 240), BeatTime(6)));
                        ce.Run((s) => { InstantSetBox(s, 84, 84); InstantTP(s); });
                    });
                    RegisterFunctionOnce("StartLine", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 0));
                        ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0), new(0.15f, 0)));
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(0, CentreEasing.Stable(320, 0));
                        ce2.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0), new(-0.15f, 0)));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                        Line l2 = new(ce2.GetResult(), ValueEasing.Stable(90));
                        CreateEntity(l);
                        CreateEntity(l2);
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0));
                        alp.Insert(BeatTime(2), ValueEasing.Linear(0, 1, BeatTime(2)));
                        alp.Run((s) => { l.Alpha = s; l2.Alpha = s; });
                    });
                    RegisterFunctionOnce("RandomLine", () =>
                    {
                        Line l = new(CentreEasing.Stable(new(Rand(0, 640), Rand(0, 480))), ValueEasing.Stable(Rand(0, 359)));
                        CreateEntity(l);
                        ValueEasing.EaseBuilder alp = new();
                        alp.Insert(0, ValueEasing.Stable(0.5f));
                        alp.Insert(BeatTime(4), ValueEasing.EaseInSine(0.5f, 0, BeatTime(4)));
                        alp.Run((s) => { l.Alpha = s; });
                    });
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 5, new string[]
                        {
                        "R(StartLine)","","","",   "","","","",
                        "+0(StartLine)","","","",   "","","","",
                        "D(RandomLine)","","","",   "","","","",
                        "+1(RandomLine)","","","",   "","","","",
                        //
                        "+1(RandomLine)","","","",   "","","","",
                        "+1","","","",   "","","","",
                        "+2(RandomLine)","","","",   "","","","",
                        "+1(RandomLine)","","","",   "","","","",

                        "+1(RandomLine)","","","",   "","","","",
                        "","","","",   "","","","",
                        "D(RandomLine)","","","",   "","","","",
                        "+1(RandomLine)","","","",   "","","","",

                        "+1(RandomLine)","","","",   "","","","",
                        "+1","","","",   "","","","",
                        "+1(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "R","","+21","",

                        "+2(RandomLine)","","","",   "","","","",
                        "","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        //
                        "R(RandomLine)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "","","","",

                        "R(RandomLine)","","","",   "","","","",
                        "","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "","","","",

                        "R(RandomLine)","","","",   "","","","",
                        "R","","","",   "+0","","+21","",
                        "+2(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "","","","",

                        "R(RandomLine)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        //
                        "R(RandomLine)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",

                        "R(RandomLine)","","","",   "","","","",
                        "","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",

                        "R(RandomLine)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",

                        "R(RandomLine)","","","",   "","","","",
                        "","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        "R(RandomLine)","","","",   "","","","",
                        //
                        "R(RandomLine)","","","",   "","","","",
                        "R","","","",   "","","","",
                        "(#18#$0)(#18#$21)(RandomLine)","","","",   "","","","",
                        "","","","",   "","","","",

                        "$1(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "","","","",
                        "$3(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "","","","",

                        "+2(RandomLine)","","","",   "","","","",
                        "$1(RandomLine)","","","",   "+0","","+21","",
                        "+2(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "","","","",

                        "+2(RandomLine)","","","",   "","","","",
                        "+2(RandomLine)","","","",   "","","","",
                        "+2(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "","","","",

                        "(RandomLine)","","","",   "","","","",
                        "(RandomLine)","","","",   "","","","",
                        "$1(RandomLine)","","","",   "","","","",
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 5, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });

                }
                if (InBeat(72))
                {
                    RegisterFunctionOnce("BoxUp", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(BeatTime(6), CentreEasing.EaseOutQuart(new(320, 600), new(320, 240), BeatTime(6)));
                        ce.Run((s) => { InstantSetBox(s, 84, 84); InstantTP(s); });
                    });
                    RegisterFunctionOnce("ConR", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ConL", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ConBack", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("StepSampleIn", () =>
                    {
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseInSine(0, 0.05f, BeatTime(0.25f)));
                        ve.Insert(BeatTime(1f), ValueEasing.EaseInQuad(0.05f, 0, BeatTime(1f)));
                        ve.Run((s) => { StepSample.Intensity = s; });
                    });
                    RegisterFunctionOnce("LeftLine", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(-30, 0));
                        ce.Insert(BeatTime(2.3f), CentreEasing.EaseOutCubic(new(-30, 0), new(670, 0), BeatTime(2.3f)));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.7f };
                        CreateEntity(l);
                    });
                    RegisterFunctionOnce("RightLine", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(670, 0));
                        ce.Insert(BeatTime(2.3f), CentreEasing.EaseOutCubic(new(670, 0), new(-30, 0), BeatTime(2.3f)));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90)) { Alpha = 0.7f };
                        CreateEntity(l);
                    });

                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "R","","+2","",   "+2","","","",
                        "R","","+2","",   "+2","","","",

                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "R","","+2","",   "+2","","","",
                        "R","","+2","",   "+2","","","",

                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "R1","","+21","",   "+21","","","",
                        "R1","","+21","",   "+21","","","",

                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "R1","","+21","",   "+21","","","",
                        "R1","","+21","",   "+21","","","",
                        //
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "R","","+21","",   "+2","","","",
                        "R","","+21","",   "+2","","","",

                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "R","","+21","",   "+2","","","",
                        "R","","+21","",   "+2","","","",

                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "R1","","+2","",   "+21","","","",
                        "R1","","+2","",   "+21","","","",

                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "(R)(D1)","","+01{_}","",   "+01{_}","","","",
                        "R1","","+2","",   "+21","","","",
                        "R1","","+2","",   "+21","","($0)($2)","",
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(3), BeatTime(1), 7, new string[]
                    {

                        "","","","",   "(ConR)","","StepSampleIn","",

                        "","","","",   "(ConL)","","StepSampleIn","",
                        "","","","",   "","","","",
                        "LeftLine","","LeftLine","",   "LeftLine","","","",
                        "LeftLine","","(LeftLine)","",   "LeftLine(ConR)","","StepSampleIn","",

                        "","","","",   "(ConL)","","StepSampleIn","",
                        "","","","",   "","","","",
                        "(LeftLine)","","(LeftLine)","",   "(LeftLine)","","","",
                        "(LeftLine)","","(LeftLine)","",   "(LeftLine)(ConR)","","StepSampleIn","",

                        "","","","",   "(ConL)","","StepSampleIn","",
                        "","","","",   "","","","",
                        "(LeftLine)","","(LeftLine)","",   "(LeftLine)","","","",
                        "(LeftLine)","","(LeftLine)","",   "(LeftLine)(ConR)","","StepSampleIn","",

                        "","","","",   "(ConBack)","","StepSampleIn","",
                        "","","","",   "","","","",
                        "(LeftLine)","","(LeftLine)","",   "(LeftLine)","","","",
                        "(LeftLine)","","(LeftLine)","",   "(LeftLine)(ConL)","","StepSampleIn","",
                        //
                        "","","","",   "(ConR)","","StepSampleIn","",
                        "","","","",   "","","","",
                        "(RightLine)","","(RightLine)","",   "(RightLine)","","","",
                        "(RightLine)","","(RightLine)","",   "(RightLine)(ConL)","","StepSampleIn","",

                        "","","","",   "(ConR)","","StepSampleIn","",
                        "","","","",   "","","","",
                        "(RightLine)","","(RightLine)","",   "(RightLine)","","","",
                        "(RightLine)","","(RightLine)","",   "(RightLine)(ConL)","","StepSampleIn","",

                        "","","","",   "(ConR)","","StepSampleIn","",
                        "","","","",   "","","","",
                        "(RightLine)","","(RightLine)","",   "(RightLine)","","","",
                        "(RightLine)","","(RightLine)","",   "(RightLine)(ConL)","","StepSampleIn","",

                        "","","","",   "(ConBack)","","StepSampleIn","",
                        "","","","",   "","","","",
                        "(RightLine)","","(RightLine)","",   "(RightLine)","","","",
                        "(RightLine)","","(RightLine)","",   "(RightLine)","","","",

                    });

                }
                if (InBeat(104))
                {
                    RegisterFunctionOnce("Fallen", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, 240));
                        ce.Insert(BeatTime(6), CentreEasing.Accerlating(new(0.8f, -4.5f), new(0.02f, 0.215f)));
                        ce.Run((s) => { InstantSetBox(s, 84, 84); InstantTP(s); });
                        ScreenDrawing.BoundColor = Color.DarkBlue * 0.9f;
                        AddInstance(new TimeRangedEvent(0, game.BeatTime(7), () =>
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
                        AddInstance(new TimeRangedEvent(game.BeatTime(12), game.BeatTime(64), () =>
                        {
                            if (Gametime % 5 == 0)
                                ScreenDrawing.BoundColor = Color.Lerp(ScreenDrawing.BoundColor, Color.LightBlue * 0.38f, 0.02f);
                        }));
                        AddInstance(new TimeRangedEvent(game.BeatTime(12), game.BeatTime(2560), () =>
                        {
                            InstantTP(BoxStates.Centre);
                            CreateEntity(new Particle(Color.Lerp(Color.Aqua, Color.White, MathF.Pow(Rand(0, 90) / 100f, 2)) * MathF.Pow((Rand(2, 14) / 20f), 1.5f),
                                         new(Rand(-10, 10) / 10f, -7.4f + Rand(-15, 15) / 10f), Rand(10, 16), new(Rand(-100, 740), 495), Sprites.square)
                            {
                                DarkingSpeed = 1.7f,
                                AutoRotate = true
                            });
                        }));
                        AddInstance(new InstantEvent(game.BeatTime(11), () => ScreenDrawing.SceneOut(new(0, 0, 128), game.BeatTime(1))));
                        AddInstance(new InstantEvent(game.BeatTime(11), () => ScreenDrawing.SceneRendering.InsertProduction(RGBSplitting)));
                    });
                    RegisterFunctionOnce("BoxDown", () =>
                    {
                        InstantSetBox(new Vector2(320, -100), 84, 84);
                        InstantTP(320, -100);
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(320, -100));
                        ce.Insert(BeatTime(4), CentreEasing.EaseOutQuart(new(320, -100), new(320, 240), BeatTime(4)));
                        ce.Run((s) => { InstantSetBox(s, 84, 84); InstantTP(s); });
                    });
                    RegisterFunctionOnce("ConR", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ConL", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ConBack", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(1.5f));
                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    });
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "($0'1.4)(^$21'1.4)","","","",   "","","($0)($2)","",
                        "($01'1.4)(^$2'1.4)","","","",   "","","($0)($2)","",
                        "($0'1.4)(^$21'1.4)","","","",   "","","($0)($2)","",
                        "($01'1.4)(^$2'1.4)","","","",   "","","($0)($2)","",

                        "($0'1.4)(^$21'1.4)","","","",   "","","($0)($2)","",
                        "($01'1.4)(^$2'1.4)","","","",   "","","($0)($2)","",
                        "($0'1.4)(^$21'1.4)","","","",   "","","($0)($2)","",
                        "($01'1.4)(^$2'1.4)","","","",   "","","($0)($2)","",

                        "($0'1.4)(^$21'1.4)","","","",   "","","($01)($21)","",
                        "($01'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",
                        "($0'1.4)(^$21'1.4)","","","",   "","","($01)($21)","",
                        "($01'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",

                        "($0'1.4)(^$21'1.4)","","","",   "","","($01)($21)","",
                        "($01'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",
                        "($0'1.4)(^$21'1.4)","","","",   "","","($01)($21)","",
                        "($01'1.4)(^$2'1.4)","","","",   "$3'1.2","$31'1.2","$3'1.2","$31'1.2",
                        //
                        "($0'1.4)(^$2'1.4)Clean","","","","","","($01)($21)","",
                        "($0'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",
                        "($0'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",
                        "($0'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",

                        "($0'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",
                        "($0'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",
                        "($0'1.4)(^$2'1.4)","","","",   "","","($01)($21)","",
                        "($0'1.4)(^$2'1.4)","","","",   "","","($0)($2)","",

                        "($01'1.4)(^$21'1.4)","","","",   "","","($0)($2)","",
                        "($01'1.4)(^$21'1.4)","","","",   "","","($0)($2)","",
                        "($01'1.4)(^$21'1.4)","","","",   "","","($0)($2)","",
                        "($01'1.4)(^$21'1.4)","","","",   "","","","",

                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        //
                        "(<$31)(>$30)(Fallen)","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "BoxDown","","","",
                        "","","","",   "","","","",
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "","","","",   "ConR","","","",
                        "ConBack","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack","","","",   "","","","",
                        //
                        "","","","",   "ConR","","","",
                        "ConBack","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack","","","",   "","","","",

                        "!!3/24",
                        "RandomRotate","","","","RandomRotate","",
                        "","","RandomRotate","","","",
                        "RandomRotate","","","","RandomRotate","",
                        "","","RandomRotate","","","",
                        //
                        "Return","","","",   "","","","",
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
                        //
                    });

                }
                if (InBeat(152))
                {

                    RegisterFunctionOnce("Rotate1", () =>
                    {
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(0, 3, BeatTime(2)));
                        ve.Insert(BeatTime(2), ValueEasing.EaseOutSine(3, 7, BeatTime(2)));
                        ve.Insert(BeatTime(2), ValueEasing.EaseInSine(7, -3, BeatTime(2)));
                        ve.Insert(BeatTime(1f), ValueEasing.EaseOutSine(-3, -10, BeatTime(1f)));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseInQuad(-10, 12, BeatTime(0.5f)));
                        ve.Insert(BeatTime(2.5f), ValueEasing.EaseInSine(12, 4, BeatTime(2.5f)));
                        ve.Insert(BeatTime(2f), ValueEasing.EaseOutSine(4, -4, BeatTime(2f)));
                        ve.Insert(BeatTime(2f), ValueEasing.EaseOutSine(-4, 0, BeatTime(2f)));
                        ve.Run((S) => { ScreenDrawing.ScreenAngle = S; });
                    });
                    RegisterFunctionOnce("Shake", () => { Extends.DrawingUtil.Shock(3f, 2, 2, 2); });
                    RegisterFunctionOnce("Return", () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    });
                    RegisterFunctionOnce("LeftLine", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(0, 0));
                        ce.Insert(BeatTime(1f), CentreEasing.EaseOutCubic(new(0, 0), new(60, 0), BeatTime(1f)));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(1));
                    });
                    RegisterFunctionOnce("RightLine", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(640, 0));
                        ce.Insert(BeatTime(1f), CentreEasing.EaseOutCubic(new(640, 0), new(580, 0), BeatTime(1f)));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(1));
                    });
                    RegisterFunctionOnce("LongShake", () =>
                    {
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 2, BeatTime(0.25f)));
                        for (int a = 0; a < 7; a++)
                        {
                            ve.Insert(BeatTime(0.25f), ValueEasing.Linear(2, -2, BeatTime(0.25f)));
                            ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-2, 2, BeatTime(0.25f)));
                        }
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(2, 0, BeatTime(0.25f)));
                        ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                    });
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",
                        "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",

                         "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "(R'1.3)(D1'1.3)","","","",   "(R'1.3)(D1'1.3)","","","",
                        "(R'1.3)(D1'1.3)","","","",   "$0(Shake)","$11","$2","$31",

                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",
                        "+2","","+2","",   "($01)($21)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "($0)($2)","","","",   "R","","+2","",

                         "(<^R'1.5)(>^D1'1.5)(LeftLine)","","","",   "","","","",
                        "(<^R'1.5)(>^D1'1.5)(RightLine)","","","",   "","","","",
                        "(<^R'1.5)(>^D1'1.5)(LeftLine)(RightLine)","","","",   "","","","",
                        "(<^R'1.5)(>^D1'1.5)(LeftLine)(RightLine)","","","",   "","","(<^$01'1.4)(>^$21'1.4)","",
                        //
                        "(>^$0'1.4)(<^$2'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(<^$0'1.4)(>^$2'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(>^$0'1.4)(<^$2'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(<^$0'1.4)(>^$2'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",

                        "(>^$0'1.4)(<^$2'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(<^$0'1.4)(>^$2'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(>^$0'1.4)(<^$2'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(<^$01'1.4)(>^$21'1.4)(Shake)(LeftLine)(RightLine)","","(>^$0'1.4)(<^$2'1.4)","",   "(<^$01'1.4)(>^$21'1.4)","","(>^$0'1.4)(<^$2'1.4)","",

                        "(<^$01'1.4)(>^$21'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(>^$01'1.4)(<^$21'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(<^$01'1.4)(>^$21'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",
                        "(>^$01'1.4)(<^$21'1.4)(Shake)(LeftLine)(RightLine)","","","",   "","","","",

                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","","","","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","","","","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","","","",
                        //
                        "($0'1.3)($2'1.3)(Return)(LeftLine)","","","",   "R","","+2","",
                        "+2(RightLine)","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1(LeftLine)","","+21","",   "+21","","+21","",
                        "($0'1.3)($2'1.3)(RightLine)","","","",   "R","","+2","",

                        "+2(LeftLine)","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1(RightLine)","","+21","",   "+21","","+21","",
                        "($0'1.3)($2'1.3)(LeftLine)","","","",   "R","","+2","",
                        "+2(RightLine)","","+2","",   "($01'1.3)($21'1.3)","","","",
                            "$21(LongShake)","","$31","",

                        "$01","","$11","",   "$21","",
                        "$0","","$3","",   "$2","","$1","",
                        "$0","","$21","","$31","",
                        "$01","","$11","",   "$21","","$31","",

                        "!!3/6","($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","","($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","",   "($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","",
                        "!!3/6","($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","","($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","",   "($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","",
                        "!!3/6","($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","","($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","",   "($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","",
                        "($0)($2)(R1{Down})(Return)","","","",   "^$3'1.5","^$31'1.5","^$3'1.5","^$31'1.5",
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });
                }
                if (InBeat(200))
                {

                    RegisterFunctionOnce("ConR", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ConL", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(0.5f));
                    });
                    RegisterFunctionOnce("ConBack", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(1.5f));
                    });
                    RegisterFunctionOnce("Line1", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(0, 0));
                        ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutSine(new(0, 0), new(240, 0), BeatTime(0.5f)));
                        ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutSine(new(240, 0), new(120, 0), BeatTime(0.5f)));
                        ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutSine(new(120, 0), new(360, 0), BeatTime(0.5f)));
                        ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(0, 0), new(0, -0.1f)));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(90));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(90, 70, BeatTime(0.5f)));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(70, 110, BeatTime(0.5f)));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(110, 70, BeatTime(0.5f)));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(70, 0, BeatTime(0.5f)));
                        Line l = new(ce.GetResult(), ve.GetResult()) { TransverseMirror = true };
                        CreateEntity(l);
                        LineShadow(0.5f, 0.2f, 10, l);
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(0, 480));
                        ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutSine(new(0, 480), new(240, 480), BeatTime(0.5f)));
                        ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutSine(new(240, 480), new(120, 480), BeatTime(0.5f)));
                        ce.Insert(BeatTime(0.5f), CentreEasing.EaseOutSine(new(120, 480), new(360, 480), BeatTime(0.5f)));
                        ce.Insert(BeatTime(4), CentreEasing.Accerlating(new(0, 0), new(0, 0.1f)));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(90));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(90, 110, BeatTime(0.5f)));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(110, 70, BeatTime(0.5f)));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(70, 110, BeatTime(0.5f)));
                        ve.Insert(BeatTime(0.5f), ValueEasing.EaseOutSine(110, 180, BeatTime(0.5f)));
                        Line l = new(ce.GetResult(), ve.GetResult()) { TransverseMirror = true };
                        CreateEntity(l);
                        LineShadow(0.5f, 0.2f, 10, l);
                    });
                    RegisterFunctionOnce("DrumLine", () =>
                    {
                        Line l = new(40, 90) { TransverseMirror = true };
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(2));
                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    });
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "($1'1.5)($2'1.5)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",

                         "(R)(D1)","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "(R)(D1)","","","",
                        "($0'1.5)($2'1.5)(Line1)","","","",   "($0'1.5)($2'1.5)","","","",
                        "($01'1.5)($21'1.5)","","","",   "($01'1.5)($21'1.5)","","","",

                        "($0'1.5)($2'1.5)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$0'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$0'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$0'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",

                         "(R)(D1)","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "(R)(D1)","","","",
                        "($01'1.5)($21'1.5)(Line2)","","","",   "($01'1.5)($21'1.5)","","","",
                        "($0'1.5)($2'1.5)","","","",   "($0'1.5)($2'1.5)","","","",
                        //
                        "(>^$0'1.4)(<^$2'1.4)","","","",   "(^$01'2)(^$21'2)","","(^$01'2)(^$21'2)","",
                        "(<^$0'1.4)(>^$2'1.4)","","","",   "(^$01'2)(^$21'2)","","(^$01'2)(^$21'2)","",
                        "(>^$0'1.4)(<^$2'1.4)","","","",   "(^$01'2)(^$21'2)","","(^$01'2)(^$21'2)","",
                        "(<^$0'1.4)(>^$2'1.4)","","","",   "(^$01'2)(^$21'2)","","(^$01'2)(^$21'2)","",

                        "(R)(D1)","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "(R)(D1)","","","",

                        "(#3.4#R)(#3.4#D1)","","","",   "","","","",
                        "","","","",   "(+0)(D1)","","","",
                        "(+0)(D1)","","","",   "","","","",
                        "","","","",   "(+0)(D1)","","","",

                        "(+0)(D1)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)($01)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$0'1.5)($21)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)($01)","","","", "(^$1'1.5)","","(^$1'1.5)","",
                        //
                        "($0'1.5)($2'1.5)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",

                         "(R)(D1)","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "(R)(D1)","","","",
                        "($0'1.5)($2'1.5)(Line1)","","","",   "($0'1.5)($2'1.5)","","","",
                        "($01'1.5)($21'1.5)","","","",   "($01'1.5)($21'1.5)","","","",

                        "($0'1.5)($2'1.5)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$0'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$0'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$0'1.5)(R1'1.2)","","","",   "(^$1'1.5)","","(^$1'1.5)","",

                         "(R)(D1)","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "(R)(D1)","","","",
                        "($01'1.5)($21'1.5)(Line2)","","","",   "($01'1.5)($21'1.5)","","","",
                        "($0'1.5)($2'1.5)","","","",   "($0'1.5)($2'1.5)","","","",
                        //
                        "(#3.4#R)(#3.4#D1)","","","",   "","","","",
                        "","","","",   "(+0)(D1)","","","",
                        "(+0)(D1)","","","",   "","","","",
                        "","","","",   "(+0)(D1)","","","",

                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(#3.4#R)(#3.4#D1)","","","",   "","","","",
                        "","","","",   "(+0)(D1)","","","",
                        "(+0)(D1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "Return",
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                    {
                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        //
                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        //
                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        //
                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "","","","",   "ConR","","","",
                        "ConBack(DrumLine)","","","",   "","","","",
                        "","","","",   "ConL","","","",
                        "ConBack(DrumLine)","","","",   "","","","",

                        "!!3/24",
                        "RandomRotate","","","","RandomRotate","",
                        "","","RandomRotate","","","",
                        "RandomRotate","","","","RandomRotate","",
                        "","","RandomRotate","","","",
                    });
                }
                if (InBeat(264))
                {

                    RegisterFunctionOnce("ConR", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(1.5f));
                    });
                    RegisterFunctionOnce("ConL", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(1.5f));
                    });
                    RegisterFunctionOnce("ConBack", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(1f));
                    });
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "($3'1.2)($0'1.2)(ConR)","","","",   "$1","","+1","",
                        "-1","","","",   "($11'1.2)($21'1.2)(ConL)","","","",
                        "$31","","+11","",   "-11","","","",
                        "($3'1.2)($0'1.2)(ConR)","","","",   "$1","","+1","",
                        "-1","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",

                        "($0'1.2)($1'1.2)(ConL)","","","",   "$2","","+1","",
                        "-1","","","",   "($21'1.2)($31'1.2)(ConR)","","","",
                        "$01","","+11","",   "-11","","","",
                        "($0'1.2)($1'1.2)(ConL)","","","",   "$2","","+1","",
                        "-1","","","",   "(R)(D1)(ConBack)","","","",
                        "","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",
                        "$2","","$11","",   "$3","","$31","",
                        //
                        "$0","","$21","",   "$1","","$11","",
                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "R","","R1","",

                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "R","","R1","",
                        "$1","","$11","",   "$0","","$21","",
                        "$3","","$31","",   "$2","","$11","",

                        "(#3.4#$0)(#3.4#$21)","","","",   "","","","",
                        "","","","",   "(+0)(D1)","","","",
                        "(+0)(D1)","","","",   "","","","",
                        "","","","",   "(+0)(D1)(ConL)","","","",

                        "(+0)(D1)","","","",   "(^$1'1.5)(ConR)","","(^$1'1.5)","",
                        "(^$2'1.5)($01)","","","",   "(^$1'1.5)(ConL)","","(^$1'1.5)","",
                        "(^$0'1.5)($21)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)($01)(ConBack)","","","", "(^$1'1.5)","","(^$1'1.5)","",
                        
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });
                }
                if (InBeat(296))
                {
                    RegisterFunctionOnce("ConR", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(5, BeatTime(1f));
                    });
                    RegisterFunctionOnce("ConL", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(-5, BeatTime(1f));
                    });
                    RegisterFunctionOnce("ConBack", () =>
                    {
                        ScreenDrawing.CameraEffect.RotateTo(0, BeatTime(1f));
                    });

                    RegisterFunctionOnce("StepSampleIn", () =>
                    {
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(0));
                        ve.Insert(BeatTime(0.25f), ValueEasing.EaseInSine(0, 0.1f, BeatTime(0.25f)));
                        ve.Insert(BeatTime(1.25f), ValueEasing.EaseInQuad(0.1f, 0, BeatTime(1.25f)));
                        ve.Run((s) => { StepSample.Intensity = s; });
                    });
                    RegisterFunctionOnce("Return", () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    });
                    RegisterFunctionOnce("ConR2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1f), true);
                    });
                    RegisterFunctionOnce("ConL2", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(5, BeatTime(1f), false);
                    });
                    RegisterFunctionOnce("LongShake", () =>
                    {
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 2, BeatTime(0.25f)));
                        for (int a = 0; a < 7; a++)
                        {
                            ve.Insert(BeatTime(0.25f), ValueEasing.Linear(2, -2, BeatTime(0.25f)));
                            ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-2, 2, BeatTime(0.25f)));
                        }
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(2, 0, BeatTime(0.25f)));
                        ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                    });
                    RegisterFunctionOnce("Over", () =>
                    {
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(0, ValueEasing.Stable(1));
                        ve.Insert(BeatTime(4), ValueEasing.EaseOutSine(1, 1.2f, BeatTime(4)));
                        ve.Insert(BeatTime(4), ValueEasing.EaseInSine(1.2f, 1, BeatTime(4)));
                        ve.Run((s) => { ScreenDrawing.ScreenScale = s; });
                        ValueEasing.EaseBuilder rot = new();
                        rot.Insert(0, ValueEasing.Stable(0));
                        rot.Insert(BeatTime(3), ValueEasing.EaseInSine(0, 180, BeatTime(3)));
                        rot.Insert(BeatTime(3), ValueEasing.EaseOutSine(180, 360, BeatTime(3)));
                        rot.Run((s) =>
                        {
                            ScreenDrawing.ScreenAngle = s;
                        });

                    });
                    RegisterFunctionOnce("Lines1", () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        ce1.Insert(BeatTime(4), CentreEasing.Stable(320, 120));
                        ce1.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(320, 120), new(320, 360), BeatTime(4)));
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(BeatTime(4), ValueEasing.Stable(45));
                        ve1.Insert(BeatTime(4), ValueEasing.EaseInQuad(45, 120, BeatTime(4)));
                        Line l1 = new(ce1.GetResult(), ve1.GetResult()) { Alpha = 1 };
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1f), 0.4f);
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(4), CentreEasing.Stable(320, 100));
                        ce2.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(320, 100), new(320, 360), BeatTime(4)));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(BeatTime(4), ValueEasing.Stable(45));
                        ve2.Insert(BeatTime(4), ValueEasing.EaseInQuad(45, 120, BeatTime(4)));
                        Line l2 = new(ce2.GetResult(), ve2.GetResult()) { Alpha = 1 };
                        CreateEntity(l2);
                        l2.AlphaDecrease(BeatTime(1f), 0.4f);
                        DelayBeat(4, () => { l1.AlphaDecrease(BeatTime(4)); l2.AlphaDecrease(BeatTime(4)); });
                    });
                    RegisterFunctionOnce("Lines2", () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        ce1.Insert(BeatTime(3), CentreEasing.Stable(440, 240));
                        ce1.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(440, 240), new(200, 240), BeatTime(4)));
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(BeatTime(3), ValueEasing.Stable(135));
                        ve1.Insert(BeatTime(4), ValueEasing.EaseInQuad(135, 210, BeatTime(4)));
                        Line l1 = new(ce1.GetResult(), ve1.GetResult()) { Alpha = 1 };
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1f), 0.4f);
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(3), CentreEasing.Stable(460, 240));
                        ce2.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(460, 240), new(200, 240), BeatTime(4)));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(BeatTime(3), ValueEasing.Stable(135));
                        ve2.Insert(BeatTime(4), ValueEasing.EaseInQuad(135, 210, BeatTime(4)));
                        Line l2 = new(ce2.GetResult(), ve2.GetResult()) { Alpha = 1 };
                        CreateEntity(l2);
                        l2.AlphaDecrease(BeatTime(1f), 0.4f);
                        DelayBeat(3, () => { l1.AlphaDecrease(BeatTime(4)); l2.AlphaDecrease(BeatTime(4)); });
                    });
                    RegisterFunctionOnce("Lines3", () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        ce1.Insert(BeatTime(2), CentreEasing.Stable(320, 360));
                        ce1.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(320, 360), new(320, 120), BeatTime(4)));
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(BeatTime(2), ValueEasing.Stable(45 + 180));
                        ve1.Insert(BeatTime(4), ValueEasing.EaseInQuad(45 + 180, 120 + 180, BeatTime(4)));
                        Line l1 = new(ce1.GetResult(), ve1.GetResult()) { Alpha = 1 };
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1f), 0.4f);
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(2), CentreEasing.Stable(320, 380));
                        ce2.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(320, 380), new(320, 120), BeatTime(4)));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(BeatTime(2), ValueEasing.Stable(45 + 180));
                        ve2.Insert(BeatTime(4), ValueEasing.EaseInQuad(45 + 180, 120 + 180, BeatTime(4)));
                        Line l2 = new(ce2.GetResult(), ve2.GetResult()) { Alpha = 1 };
                        CreateEntity(l2);
                        l2.AlphaDecrease(BeatTime(1f), 0.4f);
                        DelayBeat(2, () => { l1.AlphaDecrease(BeatTime(4)); l2.AlphaDecrease(BeatTime(4)); });
                    });
                    RegisterFunctionOnce("Lines4", () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        ce1.Insert(BeatTime(1), CentreEasing.Stable(200, 240));
                        ce1.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(200, 240), new(440, 240), BeatTime(4)));
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Insert(BeatTime(1), ValueEasing.Stable(135 + 180));
                        ve1.Insert(BeatTime(4), ValueEasing.EaseInQuad(135 + 180, 210 + 180, BeatTime(4)));
                        Line l1 = new(ce1.GetResult(), ve1.GetResult()) { Alpha = 1 };
                        CreateEntity(l1);
                        l1.AlphaDecrease(BeatTime(1f), 0.4f);
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Insert(BeatTime(1), CentreEasing.Stable(180, 240));
                        ce2.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(180, 240), new(440, 240), BeatTime(4)));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Insert(BeatTime(1), ValueEasing.Stable(135 + 180));
                        ve2.Insert(BeatTime(4), ValueEasing.EaseInQuad(135 + 180, 210 + 180, BeatTime(4)));
                        Line l2 = new(ce2.GetResult(), ve2.GetResult()) { Alpha = 1 };
                        CreateEntity(l2);
                        l2.AlphaDecrease(BeatTime(1f), 0.4f);
                        DelayBeat(1, () => { l1.AlphaDecrease(BeatTime(4)); l2.AlphaDecrease(BeatTime(4)); });
                    });
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "(^R'1.5)(^D1'1.5)ConL","","","",   "!!3","$0'1.3(StepSampleIn)","$01'1.3","$0'1.3",
                        "($01'1.3)(^$0'1.3)","","","",   "(^R'1.5)(^D1'1.5)ConR","","","",
                         "!!3","$2'1.3(StepSampleIn)","$21'1.3","$2'1.3",   "($21'1.3)(^$2'1.3)","","","",
                        "(^R'1.5)(^D1'1.5)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                        "(^R'1.5)(^D1'1.5)ConL","","","",   "!!3","$0'1.3(StepSampleIn)","$01'1.3","$0'1.3",
                        "($01'1.3)(^$0'1.3)","","","",   "(^R'1.5)(^D1'1.5)ConR","","","",
                         "!!3","$2'1.3(StepSampleIn)","$21'1.3","$2'1.3",   "($21'1.3)(^$2'1.3)","","","",
                        "(^R'1.5)(^D1'1.5)","","","",   "(^R'1.5)(^D1'1.5)","","","",

                        "(^R'1.5)(^D1'1.5)ConL","","","",   "!!3","$0'1.3(StepSampleIn)","$01'1.3","$0'1.3",
                        "($01'1.3)(^$0'1.3)","","","",   "(^R'1.5)(^D1'1.5)ConR","","","",
                         "!!3","$2'1.3(StepSampleIn)","$21'1.3","$2'1.3",   "($21'1.3)(^$2'1.3)","","","",
                        "(^R'1.5)(^D1'1.5)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                        "(^R'1.5)(^D1'1.5)ConL","","","",   "!!3","$0'1.3(StepSampleIn)","$01'1.3","$0'1.3",
                        "($01'1.3)(^$0'1.3)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                         "!!3/6","($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","","($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","",   "($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","",
                        "!!3/6","($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","","($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","",   "($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","",

                        "($0)($2)(R1{Down})(Return)","","","",   "($21)($11)(R)(ConR2)","","","",
                        "($21)($11)(R)(ConR2)","","","",   "($21)($11)(R)(ConR2)","","","",
                        "($0)($1)(R1)(ConL2)","","","",   "($0)($1)(R1)(ConL2)","","","",
                        "($0)($1)(R1)(ConL2)","","","","($0)($1)(R1)","","","",

                        "$0(LongShake)","","$21","",   "$1","","$11","",
                        "$2","","$01","",   "$3","","$31","",
                        "$2","","$01","",   "$2","","$01","",
                        "$1","","$11'1.2","","$1'1.4","$11'1.6","$1'1.8","$11'1.6","($3'3.0)($31'3.0)",
                        "Over(Lines1)","","","",   "","","","",
                        "(Lines2)","","","",   "","","","",
                        "(Lines3)","","","",   "","","","",
                        "(Lines4)","","","",   "","","","",
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });
                }
                if (InBeat(328))
                {

                    RegisterFunctionOnce("Shake", () => { Extends.DrawingUtil.Shock(3f, 2, 2, 2); });
                    RegisterFunctionOnce("Return", () =>
                    {
                        ScreenDrawing.ScreenAngle = 0;
                        ScreenDrawing.ScreenScale = 1;
                    });
                    RegisterFunctionOnce("LeftLine", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(0, 0));
                        ce.Insert(BeatTime(1f), CentreEasing.EaseOutCubic(new(0, 0), new(60, 0), BeatTime(1f)));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(1));
                    });
                    RegisterFunctionOnce("RightLine", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(640, 0));
                        ce.Insert(BeatTime(1f), CentreEasing.EaseOutCubic(new(640, 0), new(580, 0), BeatTime(1f)));
                        Line l = new(ce.GetResult(), ValueEasing.Stable(90));
                        CreateEntity(l);
                        l.AlphaDecrease(BeatTime(1));
                    });
                    RegisterFunctionOnce("LongShake", () =>
                    {
                        ValueEasing.EaseBuilder ve = new();
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(0, 2, BeatTime(0.25f)));
                        for (int a = 0; a < 7; a++)
                        {
                            ve.Insert(BeatTime(0.25f), ValueEasing.Linear(2, -2, BeatTime(0.25f)));
                            ve.Insert(BeatTime(0.25f), ValueEasing.Linear(-2, 2, BeatTime(0.25f)));
                        }
                        ve.Insert(BeatTime(0.25f), ValueEasing.Linear(2, 0, BeatTime(0.25f)));
                        ve.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                    });
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",
                        "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",

                         "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "(R'1.3)(D1'1.3)","","","",   "(R'1.3)(D1'1.3)","","","",
                        "(R'1.3)(D1'1.3)","","","",   "$0(Shake)","$11","$2","$31",

                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",
                        "+2","","+2","",   "($01)($21)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "($0)($2)","","","",   "(R)(D1)","","","",

                        "(R)(D1)(LeftLine)","","","",   "","","","",
                        "(R)(D1)(RightLine)","","","",   "","","","",
                        "(R)(D1)(LeftLine)","","","",   "","","","",
                        "(R)(D1)","","","",   "(R)(D1)","","","",
                        //
                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",
                        "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",

                         "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "(R'1.3)(D1'1.3)","","","",   "(R'1.3)(D1'1.3)","","","",
                        "(R'1.3)(D1'1.3)","","","",   "$0(Shake)","$11","$2","$31",

                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",
                        "+2","","+2","",   "($01)($21)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "($0)($2)","","","",   "R","","+2","",

                         "(<^R'1.5)(>^D1'1.5)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(<^R'1.5)(>^D1'1.5)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(<^R'1.5)(>^D1'1.5)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(<^R'1.5)(>^D1'1.5)(LeftLine)(RightLine)(Shake)","","","",   "","","(<^$01'1.4)(>^$21'1.4)","",
                        //
                        "(>^$0'1.4)(<^$2'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(<^$0'1.4)(>^$2'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(>^$0'1.4)(<^$2'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(<^$0'1.4)(>^$2'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",

                        "(>^$0'1.4)(<^$2'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(<^$0'1.4)(>^$2'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(>^$0'1.4)(<^$2'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(<^$01'1.4)(>^$21'1.4)(LeftLine)(RightLine)(Shake)","","(>^$0'1.4)(<^$2'1.4)","",   "(<^$01'1.4)(>^$21'1.4)","","(>^$0'1.4)(<^$2'1.4)","",

                        "(<^$01'1.4)(>^$21'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(>^$01'1.4)(<^$21'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(<^$01'1.4)(>^$21'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",
                        "(>^$01'1.4)(<^$21'1.4)(LeftLine)(RightLine)(Shake)","","","",   "","","","",

                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","","","","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","","","","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)(RandomRotate)","","","",
                        //
                        "($0'1.3)($2'1.3)(Return)","","","",   "R","","+2","",
                        "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",

                        "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                        "R1","","+21","",   "+21","","+21","",
                        "($0'1.3)($2'1.3)","","","",   "R","","+2","",
                        "+2","","+2","",   "($01'1.3)($21'1.3)","","","",
                            "$21(LongShake)","","$31","",

                        "$01","","$11","",   "$21","",
                        "$0","","$3","",   "$2","","$1","",
                        "$0","","$21","","$31","",
                        "$01","","$11","",   "$21","","$31","",

                        "!!3/6","($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","","($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","",   "($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","",
                        "!!3/6","($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","","($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","",   "($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","",
                        "!!3/6","($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","","($0'2)($2'1.5)(R1{Down})(SmallRandomRotate)","",   "($0'1.5)($2'2)(R1{Down})(SmallRandomRotate)","",
                        "($0)($2)(R1{Down})(SmallRandomRotate)","","","",   "^$3'1.5","^$31'1.5","^$3'1.5","^$31'1.5",
                        "Return"
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });

                }
                if (InBeat(392))
                {
                    RegisterFunctionOnce("StartLine", () =>
                    {
                        CentreEasing.EaseBuilder ce = new();
                        ce.Stable(0, 0, 0);
                        ce.Insert(BeatTime(4), CentreEasing.EaseOutQuad(new(0, 0), new(480, 160), BeatTime(4)));
                        ValueEasing.EaseBuilder ve = new();
                        ve.Stable(0, 30);

                        Line l = new(ce.GetResult(), ve.GetResult()) { VerticalLine = true };
                        CreateEntity(l);

                    });
                    RegisterFunctionOnce("Dress", () =>
                    {
                        ValueEasing.EaseBuilder rot = new();
                        rot.Insert(BeatTime(6), ValueEasing.EaseOutQuad(0, -10, BeatTime(6)));
                        rot.Stable(BeatTime(2), -10);
                        rot.Insert(BeatTime(6), ValueEasing.EaseOutQuad(-10, 10, BeatTime(6)));
                        rot.Stable(BeatTime(2), 10);
                        rot.Insert(BeatTime(6), ValueEasing.EaseOutQuad(10, -10, BeatTime(6)));
                        rot.Stable(BeatTime(2), -10);
                        rot.Insert(BeatTime(4), ValueEasing.EaseOutQuad(-10, 5, BeatTime(4)));
                        rot.Insert(BeatTime(2), ValueEasing.EaseOutQuad(5, -5, BeatTime(2)));
                        rot.Insert(BeatTime(2), ValueEasing.EaseOutQuad(-5, 5, BeatTime(2)));
                        rot.Insert(BeatTime(2), ValueEasing.EaseOutQuad(5, 0, BeatTime(2)));
                        rot.Run((s) => { ScreenDrawing.ScreenAngle = s; });
                        CentreEasing.EaseBuilder ce = new();
                        ce.Insert(0, CentreEasing.Stable(0, 0));
                        ce.Insert(BeatTime(6), CentreEasing.EaseOutCubic(new(0, 0), new(30, -30), BeatTime(6)));
                        ce.Stable(BeatTime(2), new Vector2(30, -30));
                        ce.Insert(BeatTime(6), CentreEasing.EaseOutCubic(new(30, -30), new(-30, -30), BeatTime(6)));
                        ce.Stable(BeatTime(2), new Vector2(-30, -30));
                        ce.Insert(BeatTime(6), CentreEasing.EaseOutCubic(new(-30, -30), new(30, 30), BeatTime(6)));
                        ce.Stable(BeatTime(2), new Vector2(30, 30));
                        ce.Insert(BeatTime(4), CentreEasing.EaseOutCubic(new(30, 30), new(-10, 0), BeatTime(4)));
                        ce.Insert(BeatTime(2), CentreEasing.EaseOutCubic(new(-10, 0), new(10, 0), BeatTime(2)));
                        ce.Insert(BeatTime(2), CentreEasing.EaseOutCubic(new(10, 0), new(-10, 0), BeatTime(2)));
                        ce.Insert(BeatTime(2), CentreEasing.EaseOutCubic(new(-10, 0), new(0, 0), BeatTime(2)));
                        ce.Run((s) => { ScreenDrawing.ScreenPositionDetla = s; });
                    });

                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 5.7f, new string[]
                        {
                        "(#8#$3)(Dress)(StartLine)","","","",   "","","","",
                        "","","","",   "$0","","","",
                        "$3","","","",   "$0","","","",
                        "$3","","","",   "$2","","","",

                         "$3","","","",   "$1","","","",
                        "$3","","","",   "$0","","","",
                        "($3)($1)","","","",   "$2","","","",
                        "($3)($3)","","","",   "$0","","","",

                        "(#8#$11)","","","",   "","","","",
                        "","","","",   "$31","","","",
                        "$11","","","",   "$21","","","",
                        "$11","","","",   "$01","","","",

                         "$11","","","",   "$31","","","",
                        "$11","","","",   "$21","","","",
                        "($11)($31)","","","",   "$01","","","",
                        "($11)($11)","","","",   "$21","","","",
                        //
                        "(#2#$2)","","","",   "","","","",
                        "","","","",   "$0'1.2","","$1'1.2","",
                        "(#2#$01)$2'1.2","","","",   "","","","",
                        "","","","",   "$21'1.2","","$11'1.2","",

                         "(#3.8#$2)$01'1.2","","","",   "","","","",
                        "D","","","",   "","","","",
                        "D","","","",   "$0","","$1","",
                        "$2","","","",   "D","","","",

                        "(#2#$01)D","","","",   "","","","",
                        "","","","",   "$21","","$11","",
                        "$01(#1.8#$2)","","","",   "","","","",
                        "","","","",   "","","","",

                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        //
                        "(#8#$3)","","","",   "","","","",
                        "","","","",   "$0","","","",
                        "$3","","","",   "$0","","","",
                        "$3","","","",   "$2","","","",

                         "$3","","","",   "$1","","","",
                        "$3","","","",   "$0","","","",
                        "($3)($1)","","","",   "$2","","","",
                        "($3)($3)","","","",   "$0","","","",

                        "(#8#$11)","","","",   "","","","",
                        "","","","",   "$31","","","",
                        "$11","","","",   "$21","","","",
                        "$11","","","",   "$01","","","",

                         "$11","","","",   "$31","","","",
                        "$11","","","",   "$21","","","",
                        "($11)($31)","","","",   "$01","","","",
                        "($11)($11)","","","",   "$21","","","",
                        //
                        "(#2#$2)","","","",   "","","","",
                        "","","","",   "$0'1.2","","$1'1.2","",
                        "(#2#$01)$2'1.2","","","",   "","","","",
                        "","","","",   "$21'1.2","","$11'1.2","",

                         "(#3.8#$2)$01'1.2","","","",   "","","","",
                        "D","","","",   "","","","",
                        "D","","","",   "$0","","$1","",
                        "$2","","","",   "D","","","",

                        "(#2#$01)D","","","",   "","","","",
                        "","","","",   "$21","","$11","",
                        "$01(#1.8#$2)","","","",   "","","","",
                        "","","","",   "","","","",

                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });

                }
                if (InBeat(456))
                {

                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 5.7f, new string[]
                        {
                        "(#8#R)","","","",   "","","","",
                        "+2","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","","",

                         "+0","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","","",
                        "+0","","","",   "","","","",

                        "(#8#R1)","","","",   "","","","",
                        "+21","","","",   "","","","",
                        "+01","","","",   "","","","",
                        "+01","","","",   "","","","",

                         "+01","","","",   "","","","",
                        "+01","","","",   "","","","",
                        "+01","","","",   "","","","",
                        "+01","","","",   "","","","",
                        //
                        "(#8#R)","","","",   "","","","",
                        "(+2)(+2)","","","",   "","","","",
                        "(+2)(+2)","","","",   "","","","",
                        "(+2)(+2)","","","",   "","","","",

                         "(+2)(+2)","","","",   "","","","",
                        "(+2)(+2)","","","",   "","","","",
                        "(+2)(+2)","","","",   "","","","",
                        "(+2)(+2)","","","",   "","","","",

                        "(#8#R1)","","","",   "","","","",
                        "(+21)(+21)","","","",   "","","","",
                        "(+21)(+21)","","","",   "","","","",
                        "(+21)(+21)","","","",   "","","","",

                         "(+21)(+21)","","","",   "","","","",
                        "(+21)(+21)","","","",   "","","","",
                        "(+21)(+21)","","","",   "","","","",
                        "(+21)(+21)","","","",   "","","","",
                        //
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                    {

                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "(R1)","","","",

                        "(R1)","","","",   "(R1)","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "(R)","","","",

                        "(R)","","","",   "(R)","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "(R1)","","","",

                        "(R1)","","","",   "(R1)","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "(R)","","","",

                        "(R)","","","",   "(R)","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });

                }
                if (InBeat(488))
                {

                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7f, new string[]
                        {
                        "($0)($2)","","","",   "","","","",
                        "($0)($2)","","","",   "","","","",
                        "($0)($2)","","","",   "","","","",
                        "($0)($2)","","","",   "","","","",

                         "($0)($2)","","","",   "","","","",
                        "($0)($2)","","","",   "","","","",
                        "($0)($2)","","","",   "","","","",
                        "($0)($2)","","","",   "($01)($21)","","($0)($2)","",

                        //
                        "($01)($21)","","","",   "","","","",
                        "($01)($21)","","","",   "","","","",
                        "($01)($21)","","","",   "","","","",
                        "($01)($21)","","","",   "","","","",

                        "($01)($21)","","","",   "","","","",
                        "($01)($21)","","","",   "","","","",
                        "($01)($21)","","","",   "","","","",
                        "($01)($21)","","($0)($2)","",   "($01)($21)","","($0)($2)","",

                        "($01)($21)","","","",   "($0)($2)","","","",
                        "($0)($2)","","($01)($21)","",   "($0)($2)","","($01)($21)","",
                        "($0)($2)","","","",   "($01)($21)","","","",
                        "($01)($21)","","($0)($2)","",   "($01)($21)","","($0)($2)","",

                        "($01)($21)","","","",   "($0)($2)","","","",
                        "($0)($2)","","($01)($21)","",   "($0)($2)","","($01)($21)","",
                        "($0)($2)","","","",   "($01)($21)","","","",
                        "($01)($21)","","($0)($2)","",   "($01)($21)","","($0)($2)","",
                        //
                        "($01)($21)","","","",   "","","","",
                        "","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                    {

                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "(R1)","","","",

                        "(R1)","","","",   "(R1)","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "(R)","","","",

                        "(R)","","","",   "(R)","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "(R1)","","","",

                        "(R1)","","","",   "(R1)","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "(R)","","","",

                        "(R)","","","",   "(R)","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });

                }
                if (InBeat(520))
                {

                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7f, new string[]
                        {
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",

                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)(R)","","","",   "($01)($21)(R)","","","",
                        "($01)($21)(R)","","","",   "($01)($21)(R)","","","",


                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",

                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)(R1)","","","",   "($0)($2)(R1)","","","",
                        "($0)($2)(R1)","","","",   "($0)($2)(R1)","","","",
                        //
                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",

                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        "($01)($21)(R02)","","","",   "($01)($21)(R02)","","","",
                        "($01)($21)(R02)","","","",   "($01)($21)(R02)","","","",


                        "($01)($21)(R02)","","","",   "","","","",
                        "","","","",   "($01)($21)(R02)","","","",
                        "($0)($2)(R12)","","","",   "","","","",
                        "","","","",   "($0)($2)(R12)","","","",

                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        "($01)($21)","","","",   "(<R'1.3)(+1'1.3)","","(+0'1.3)(-1'1.3)","",
                        //
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",

                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)","","","",   "<R'1.3","","<+1'1.3","",
                        "($01)($21)(R)","","","",   "($01)($21)(R)","","","",
                        "($01)($21)(R)","","","",   "($01)($21)(R)","","","",


                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",

                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)","","","",   ">R1'1.3","",">+11'1.3","",
                        "($0)($2)(R1)","","","",   "($0)($2)(R1)","","","",
                        "($0)($2)(R1)","","","",   "($0)($2)(R1)","","","",
                        //
                        "($0)($2)(R02)","","","",   "","","","",
                        "","","","",   "($0)($2)(R02)","","","",
                        "($01)($21)(R02)","","","",   "","","","",
                        "","","","",   "($01)($21)(R02)","","","",

                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",
                        "(R)(D1)","","","",   "","","","",

                        "(R)(D1)","","","",   "","","","",
                        "","","","",   "(R)(D1)","","","",
                        "(R)(D1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                    {

                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "(R1)","","","",

                        "(R1)","","","",   "(R1)","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "(R)","","","",

                        "(R)","","","",   "(R)","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "(R1)","","","",

                        "(R1)","","","",   "(R1)","","","",
                        "","","","",   "","","","",
                        "(R1)","","","",   "","","","",
                        "","","","",   "","","","",

                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "(R)","","","",

                        "(R)","","","",   "(R)","","","",
                        "","","","",   "","","","",
                        "(R)","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                    });

                }
                if (InBeat(584))
                {
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "($3'1.2)($0'1.2)","","","",   "$1","","+1","",
                        "-1","","","",   "($11'1.2)($21'1.2)","","","",
                        "$31","","+11","",   "-11","","","",
                        "($3'1.2)($0'1.2)","","","",   "$1","","+1","",
                        "-1","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",

                        "($0'1.2)($1'1.2)","","","",   "$2","","+1","",
                        "-1","","","",   "($21'1.2)($31'1.2)","","","",
                        "$01","","+11","",   "-11","","","",
                        "($0'1.2)($1'1.2)","","","",   "$2","","+1","",
                        "-1","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",
                        "","","","",   "(R)(D1)","","","",
                        "$2","","$11","",   "$3","","$31","",
                        //
                        "$0","","$21","",   "$1","","$11","",
                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "R","","R1","",

                        "R","","R1","",   "R","","R1","",
                        "R","","R1","",   "R","","R1","",
                        "$1","","$11","",   "$0","","$21","",
                        "$3","","$31","",   "$2","","$11","",

                        "(#3.4#$0)(#3.4#$21)","","","",   "","","","",
                        "","","","",   "(+0)(D1)","","","",
                        "(+0)(D1)","","","",   "","","","",
                        "","","","",   "(+0)(D1)","","","",

                        "(+0)(D1)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)($01)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$0'1.5)($21)","","","",   "(^$1'1.5)","","(^$1'1.5)","",
                        "(^$2'1.5)($01)","","","", "(^$1'1.5)","","(^$1'1.5)","",
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });
                }
                if (InBeat(616))
                {
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "(^R'1.5)(^D1'1.5)","","","",   "!!3","$0'1.3","$01'1.3","$0'1.3",
                        "($01'1.3)(^$0'1.3)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                         "!!3","$2'1.3","$21'1.3","$2'1.3",   "($21'1.3)(^$2'1.3)","","","",
                        "(^R'1.5)(^D1'1.5)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                        "(^R'1.5)(^D1'1.5)","","","",   "!!3","$0'1.3","$01'1.3","$0'1.3",
                        "($01'1.3)(^$0'1.3)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                         "!!3","$2'1.3","$21'1.3","$2'1.3",   "($21'1.3)(^$2'1.3)","","","",
                        "(^R'1.5)(^D1'1.5)","","","",   "(^R'1.5)(^D1'1.5)","","","",

                        "(^R'1.5)(^D1'1.5)","","","",   "!!3","$0'1.3","$01'1.3","$0'1.3",
                        "($01'1.3)(^$0'1.3)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                         "!!3","$2'1.3","$21'1.3","$2'1.3",   "($21'1.3)(^$2'1.3)","","","",
                        "(^R'1.5)(^D1'1.5)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                        "(^R'1.5)(^D1'1.5)","","","",   "!!3","$0'1.3","$01'1.3","$0'1.3",
                        "($01'1.3)(^$0'1.3)","","","",   "(^R'1.5)(^D1'1.5)","","","",
                         "!!3/6","($0'2)($2'1.5)(R1)","","($0'1.5)($2'2)(R1)","",   "($0'2)($2'1.5)(R1)","",
                        "!!3/6","($0'1.5)($2'2)(R1)","","($0'2)($2'1.5)(R1)","",   "($0'1.5)($2'2)(R1)","",

                        "($0)($2)(R1)","","","",   "($21)($11)(R)","","","",
                        "($21)($11)(R)","","","",   "($21)($11)(R)","","","",
                        "($0)($1)(R1)","","","",   "($0)($1)(R1)","","","",
                        "($0)($1)(R1)","","","","($0)($1)(R1)","","","",

                        "$0","","$21","",   "$1","","$11","",
                        "$2","","$01","",   "$3","","$31","",
                        "$2","","$01","",   "$2","","$01","",
                        "$1","","$11'1.2","","$1'1.4","$11'1.6","$1'1.8","$11'1.6","($3'3.0)($31'3.0)",

                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });
                }
                if (InBeat(648))
                {
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "(#3.7#$0)(^$01)(^N01)","","<$0{_}","",   ">$0{_}","","<$0{_}","",
                        ">$0{_}(^$01)(^N01)","","<$0{_}","",   ">$0{_}","","<$0{_}","",
                        ">$0{_}","","<$0{_}","",   ">$0{_}(^$01)(^N01)","","<$0{_}","",
                        ">$0{_}","","<$0{_}","",   ">$0{_}(^$01)(^N01)","","<$0{_}","",
                        "(#3.7#$1)(^$11)(^N11)","","<$1{_}","",   ">$1{_}","","<$1{_}","",
                        ">$1{_}(^$11)(^N11)","","<$1{_}","",   ">$1{_}","","<$1{_}","",
                        ">$1{_}","","<$1{_}","",   ">$1{_}(^$11)(^N11)","","<$1{_}","",
                        ">$1{_}","","<$1{_}","",   ">$1{_}(^$11)(^N11)","","<$1{_}","",

                        "(#3.7#$2)(^$21)(^N21)","","<$2{_}","",   ">$2{_}","","<$2{_}","",
                        ">$2{_}(^$21)(^N21)","","<$2{_}","",   ">$2{_}","","<$2{_}","",
                        ">$2{_}","","<$2{_}","",   ">$2{_}(^$21)(^N21)","","<$2{_}","",
                        ">$2{_}","","<$2{_}","",   ">$2{_}(^$21)(^N21)","","<$2{_}","",
                        "(#3.7#$3)(^$31)(^N31)","","<$3{_}","",   ">$3{_}","","<$3{_}","",
                        ">$3{_}(^$31)(^N31)","","<$3{_}","",   ">$3{_}","","<$3{_}","",
                        ">$3{_}(^$31)(^N31)","","<$3{_}","",   ">$3{_}(^$31)(^N31)","","<$3{_}","",
                        ">$3{_}","","<$3{_}","",   ">$3{_}(^$31)(^N31)","","<$3{_}","",

                        "(#3.7#$01)(^$0)(^N0)","","<$01{_}","",   ">$01{_}","","<$01{_}","",
                        ">$01{_}(^$0)(^N0)","","<$01{_}","",   ">$01{_}","","<$01{_}","",
                        ">$01{_}","","<$01{_}","",   ">$01{_}(^$0)(^N0)","","<$01{_}","",
                        ">$01{_}","","<$01{_}","",   ">$01{_}(^$0)(^N0)","","<$01{_}","",
                        "(#3.7#$11)(^$1)(^N1)","","<$11{_}","",   ">$11{_}","","<$11{_}","",
                        ">$11{_}(^$1)(^N1)","","<$11{_}","",   ">$11{_}","","<$11{_}","",
                        ">$11{_}","","<$11{_}","",   ">$11{_}(^$1)(^N1)","","<$11{_}","",
                        ">$11{_}","","<$11{_}","",   ">$11{_}(^$1)(^N1)","","<$11{_}","",

                        "(#3.7#$21)(^$2)(^N2)","","<$21{_}","",   ">$21{_}","","<$21{_}","",
                        ">$21{_}(^$2)(^N2)","","<$21{_}","",   ">$21{_}","","<$21{_}","",
                        ">$21{_}","","<$21{_}","",   ">$21{_}(^$2)(^N2)","","<$21{_}","",
                        ">$21{_}","","<$21{_}","",   ">$21{_}(^$2)(^N2)","","<$21{_}","",
                        "(#2#$31)(^$3)(^N3)","","<$31{_}","",   ">$31{_}","","<$31{_}","",
                        ">$31{_}(^$3)(^N3)","","<$31{_}","",   ">$31{_}","","<$31{_}","",
                        ">$31{_}(^$3)(^N3)","","<$31{_}","",   "","","","", "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'1.3)($2'1.3)($01'0.8)($21'0.8)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'1.3)($2'1.3)($01'0.8)($21'0.8)","",
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });
                }
                if (InBeat(680))
                {
                    int playednumber = 0;
                    if (playednumber == 0 || playednumber == 5) BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                        {
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",

                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",   "","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",

                        "!!3/6","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "!!3/6","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "!!3/6","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "!!3/6","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "!!3/6","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "!!3/6","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "!!3/6","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",
                        "!!3/6","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","",


                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",  "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)",
                         "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",  "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)",
                        "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",  "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)",
                         "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","","","",  "($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)","($0'0.8)($2'0.8)($01'1.3)($21'1.3)",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "!!3/6","(R'1.2)(+2'1.2)(D12'1.2)","","","","(R'1.2)(+2'1.2)(D12'1.2)","",
                        "!!3/6","","","(R'1.2)(+2'1.2)(D12'1.2)","","","",
                        "","","","",   "","","","",
                        "","","","",   "$1","","$31","",
                        "$1",
                        //
                        });
                    if (playednumber == 1 || playednumber == 5) BarrageCreate(BeatTime(2), BeatTime(1), 7, new string[]
                    {

                        "BoxUp","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",

                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        "","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",
                        //
                        "R3","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",

                        "","","","",   "R3","","","",
                        "R3","","","",   "R3","","","",
                        "","","","",   "R3","","","",
                        "R3","","","",   "","","","",

                        "#6#$33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "","","","",
                        "N33","","","",   "N33","","","",

                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "N33","","","",
                        "","","","",   "","","","",

                        "D3","","","",   "","","D3","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",

                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "","","","",   "D3","","","",
                        "D3","","","",   "","","","",
                    });
                }
            }
            #region non
            public void Hard()
            {
                if (Gametime < 0) return;
            }

            public void Noob()
            {
                if (Gametime < 0) return;
            }

            public void Easy()
            {
                if (Gametime < 0) return;
            }
            public void Normal()
            {
                if (Gametime < 0) return;
            }
            public void Extreme()
            {
                if (Gametime < 0) return;
            }
            #endregion
            void TagsArrow()
            {
                Arrow[] ars1 = GetAll<Arrow>();
                for (int i = 0; i < ars1.Length; i++)
                {
                    int x = i;
                    if (ars1[x].VoidMode == false) ars1[x].JudgeType = Arrow.JudgementType.Tap;
                    if (ars1[x].VoidMode == true) ars1[x].JudgeType = Arrow.JudgementType.Hold;
                }
                Arrow[] v = GetAll<Arrow>("_");
                for (int i = 0; i < v.Length; i++)
                {
                    int x = i;
                    v[x].VoidMode = true;
                }
                Arrow[] Down = GetAll<Arrow>("Down");
                for (int i = 0; i < Down.Length; i++)
                {
                    int x = i;
                    if (Down[x].Way == 0) Down[x].Offset = new(0, 24);
                    if (Down[x].Way == 2) Down[x].Offset = new(0, 24);
                }
                Arrow[] rr45 = GetAll<Arrow>("Down");
                for (int i = 0; i < rr45.Length; i++)
                {
                    int x = i;
                    rr45[x].CentreRotationOffset = 45;
                }
            }
            void RotateArrow(string Tag, Func<ICustomMotion, float> ease, float duration)
            {
                Arrow[] ars = GetAll<Arrow>(Tag);
                ValueEasing.EaseBuilder ve = new();
                ve.Insert(duration, ease);
                ve.Run((s) => { for (int i = 0; i < ars.Length; i++) { int x = i; ars[x].CentreRotationOffset = s; } });
            }
            void Shake(float duration, Vector2 intensity, float timedel)
            {
                CentreEasing.EaseBuilder ce = new();
                ce.Insert(0, CentreEasing.Stable(0, 0));
                ce.Insert(timedel, CentreEasing.Linear(intensity / timedel));
                ce.Insert(duration - timedel * 2, CentreEasing.Alternate(timedel, new Func<ICustomMotion, Vector2>[]
                {
                    CentreEasing.Linear(-intensity*2/timedel),
                    CentreEasing.Linear(intensity*2/timedel),
                }));
                ce.Insert(timedel, CentreEasing.Linear(intensity / timedel));
                ce.Run((s) => { ScreenDrawing.ScreenPositionDetla = s / 10; });
            }
            ScreenDrawing.Shaders.RGBSplitting RGBSplitting = new();
            public void Start()
            {
                StepSample = Shaders.StepSample;
                ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.Filter(StepSample, 0.99f));

                StepSample.Intensity = 0f;
                game = this;
                GametimeDelta = -1.5f;
                SetSoul(1);
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(new Vector2(320, 240));
                bool jump_1 = true;
                if (jump_1)
                {
                    InstantSetBox(new Vector2(320, 240), 84, 84);
                    InstantTP(new Vector2(320, 240));
                    GametimeDelta = -5f + BeatTime(392);
                    PlayOffset = BeatTime(392);
                }
                HeartAttribute.MaxHP = 20;
                RegisterFunction("RandomRotate", () =>
                {
                    ScreenDrawing.ScreenAngle = Rand(-25, 25);
                    ScreenDrawing.ScreenScale = Rand(0.900f, 1.100f);
                });
                RegisterFunction("SmallRandomRotate", () =>
                {
                    ScreenDrawing.ScreenAngle = Rand(-10, 10);
                    ScreenDrawing.ScreenScale = Rand(0.950f, 1.050f);
                });
                RegisterFunction("Clean", () =>
                {
                    Line[] ars = GetAll<Line>();
                    for (int a = 0; a < ars.Length; a++)
                    {
                        int x = a;
                        ars[x].Dispose();
                    }
                });
            }
        }
    }
}