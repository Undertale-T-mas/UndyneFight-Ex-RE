using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
namespace AprilExtends
{
    public class Pentiment : IChampionShip
    {
        public Pentiment()
        {

            difficulties = new();
            difficulties.Add("DIV.0 Lv.???", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (222f / 60f))
            {

            }
            public string Music => "Pentiment";

            public string FightName => "Pentiment";
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, -1f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, -1f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, -1f),
                        }
                    );
                public override string BarrageAuthor => "Woem";
                public override string AttributeAuthor => "Woem";
                public override string SongAuthor => "Nothing But Requiem with Museo";
            }
            public SongInformation Attributes => new ThisInformation();
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
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;

            public void ExtremePlus()
            {
                TagsArrow();
                if (InBeat(0))
                {
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) CreateChart(BeatTime(8), BeatTime(1), 7, new string[]
                        {

                        });
                }
                if (InBeat(60))
                {
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) CreateChart(BeatTime(8), BeatTime(1), 5, new string[]
                        {
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(^D'2)(^D1'2)","","","",   "","","","",
                            "(^D'2)(^D1'2)","","","",   "","","","",
                            "(^D'2)(^D1'2)","","","",   "","","","",
                            "(^D'2)(^D1'2)","","","",   "","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(^D'2)(^D1'2)","","","",   "","","(^D'2)(^D1'2)","",
                            "","","","",   "(^D'2)(^D1'2)","","","",
                            "(^D'2)(^D1'2)","","","",   "","","","",
                            "","","","",   "","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",

                            "(^D'2)(^D1'2)","","","",   "","","","",
                            "(^D'2)(^D1'2)","","","",   "","","","",
                            "(^D'2)(^D1'2)","","","",   "","","","",
                            "(^D'2)(^D1'2)","","","",   "","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "","","","",

                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D)(D1)","","","",   "(D)(D1)","","","",
                            "(D'2)","","D1'2","",   "(D'2)","","D1'2","",
                            "(D'2)","","D1'2","",   "(D'2)","","D1'2","",

                            "(^$0'3)(^$21'3)","","","",
                        });
                }
                if (InBeat(172))
                {
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) CreateChart(BeatTime(8), BeatTime(1), 6.5f, new string[]
                        {
                            "($1)(#0.25#$1)(N21)","","","",   "($2)(#0.25#$2)(N11)","","","",
                            "(*$1)(*$2)(R1)","","","",   "(*$1)(*$2)(R1)","","","",
                            "(*$1)(*$2)(R1)","","","",   "(*$1)(*$2)(R1)","","","",
                            "(*$1)(*$2)(R1)","","","",   "(*$1)(*$2)(R1)","","","",

                            "($01)(#0.25#$01)(N01)","","","",   "($11)(#0.25#$1)(N11)","","","",
                            "(*$01)(*$11)(R)","","","",   "(*$01)(*$11)(R)","","","",
                            "(*$01)(*$11)(R)","","","",   "(*$01)(*$11)(R)","","","",
                            "(*$01)(*$11)(R)","","","",   "(*$01)(*$11)(R)","","","",

                            "(*$1)(*$2)(*$01)(*$11)","","","",   "(*$1)(*$2)(*$01)(*$11)","","","",
                            "(*$1)(*$2)(*$01)(*$11)","","","",   "(*$1)(*$2)(*$01)(*$11)","","","",
                            "(*$1)(*$2)(*$01)(*$11)","","","",   "(*$1)(*$2)(*$01)(*$11)","","","",
                            "(*$1)(*$2)(*$01)(*$11)","","","",   "(*$1)(*$2)(*$01)(*$11)","","","",

                            "(*$1)(*$2)(*$01)(*$11)","","","",   "(*$1)(*$0)(*$21)(*$11)","","","",
                            "","","","",   "(*$1)(*$2)(*$01)(*$11)","","","",
                            "(*$1)(*$0)(*$21)(*$11)","","","",   "","","","",
                            "($1'0.7)","","","",   "","","","",
                            "($1'0.5)","","","",   "","","","",
                        });
                }
                if (InBeat(196))
                {
                    RegisterFunctionOnce("Void", () =>
                    {
                        Arrow[] ars = GetAll<Arrow>();
                        for (int a = 0; a < ars.Length; a++)
                        {
                            int x = a;
                            ars[x].VoidMode = true;
                        }
                    });
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) CreateChart(0, BeatTime(2), 6.5f / 2, new string[]
                        {
                            "Void","","","",   "","","","",
                            "R","","","",   "","","","",
                            "R","","","",   "R","","","",
                            "R","","","",   "R","","","",

                            "R","","","",   "","","","",
                            "R","","","",   "","","","",
                            "R","","","",   "R","","","",
                            "R","","","",   "R","","","",

                            "R","","","",   "R","","","",
                            "R","","","",   "R","","","",
                            "R","","","",   "R","","","",
                            "R","","","",   "R","","","",

                            "R","","","",   "","","","",
                            "","","","",   "","","","",
                            "R","","","",  "","","","",
                            "","","","",   "","","","",
                        });
                }
                if (InBeat(226))
                {
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) CreateChart(0, BeatTime(1), 7.2f, new string[]
                        {
                            "!!3/9","($3'0.1)($31'0.1)","($3'0.1)($31'0.1)","($3'0.1)($31'0.1)","($3'0.1)($31'0.1)",   "($3'0.1)($31'0.1)","($3'0.1)($31'0.1)",
                            "($3'0.1)($31'0.1)","($3'0.1)($31'0.1)","($3'0.1)($31'0.1)","($3'0.1)($31'0.1)","","","",

                            "(#3.8#R)(R1)","","","",   "(R1)","","","",
                            "(R)(R1)","","","",   "(R1)","","","",
                            "(R)(R1)","","","",   "(R1)","","","",
                            "(R)(R1)","","","",   "(R1)","","","",
                            "!!3","($0)(+21)","","(+0)(+21)","","(+0)(+21)","",
                            "(#2#+0)(#2#+21)","","","",   "","","","",
                            "","","","",   "","","","",
                            "","","","",   "","","","",

                            "(#3.8#R1)(R)","","","",   "(R)","","","",
                            "(R1)(R)","","","",   "(R)","","","",
                            "(R1)(R)","","","",   "(R)","","","",
                            "(R1)(R)","","","",   "(R)","","","",
                            "!!3","($2)(+21)","","(+0)(+21)","","(+0)(+21)","",
                            "(#2#+0)(#2#+21)","","","",   "","","","",
                            "","","","",   "","","","",
                            "","","","",   "","","","",
                        });
                }
                if (InBeat(252))
                {
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) CreateChart(BeatTime(8), BeatTime(1), 7.2f, new string[]
                        {
                            "(#7.8#R)","","","",   "","","","",
                            "(+2)","","","",   "","","","",
                            "(+0)","","","",   "","","","",
                            "(+0)","","","",   "","","","",
                            "(+0)","","","",   "","","","",
                            "(+0)","","","",   "","","","",
                            "(+0)","","","",   "","","","",
                            "(+0)","","","",   "(+0)","","","",

                            "(#5.8#R1)","","","",   "","","","",
                            "(+21)","","","",   "","","","",
                            "(+01)","","","",   "","","","",
                            "(+01)","","","",   "","","","",
                            "(+01)","","","",   "","","","",
                            "(+01)","","","",   "(+01)","","","",
                            "$0","","+2","",   "$11","","$21","",
                            "$0","","","",   "R1","","","",

                            "(R)","","","",   "(+0)","","","",
                            "(+0)","","","",   "(+0)","","","",
                            "(R1)","","","",   "(+01)","","","",
                            "(+01)","","","",   "(+01)","","","",
                            "(R1)(R)","","","",   "(+0)","","","",
                            "(+0)","","","",   "(+0)","","","",
                            "(R)(R1)","","","",   "(+01)","","","",
                            "(+01)","","","",   "(+01)","","","",

                            "(R1)(R)","","","",   "(+0)","","","",
                            "(R1)(R)","","","",   "(+0)","","","",
                            "(R)(R1)","","","",   "(+01)","","","",
                            "(R)(R1)","","","",   "(+01)","","","",
                            "(R)(+01)","","","",   "(R)(+01)","","","",
                            "(R)(+01)","","","",   "(R)(+01)","","","",
                            "(R'1.1)","","+01'1.1","",   "D'1.1","","+01'1.1","",
                            "D'1.1","","+01'1.1","",   "D'1.1","","+01'1.1","",
                            "+0'1.1"
                        });
                }
                if (InBeat(284))
                {
                    ScreenDrawing.UISettings.CreateUISurface();
                    Player.Heart key1;
                    Player.Heart key2;
                    Player.Heart key3;
                    Player.Heart key4;
                    RegisterFunctionOnce("Split", () =>
                    {
                        key1 = Heart;
                        key2 = CreateHeart(240, 84, 84);
                        key3 = CreateHeart(240, 84, 84);
                        key4 = CreateHeart(240, 84, 84);
                        AddInstance(new InstantEvent(1, () =>
                        {
                            CentreEasing.EaseBuilder k1 = new();
                            k1.Stable(0, 320, 240);
                            k1.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 43 - 86, 435), BeatTime(8)));
                            k1.Run((s) => { SetPlayerBoxMission(key1); InstantSetBox(s, 84, 84); InstantTP(s); });
                            key1.RotateTo(180);
                            key1.Shields.RemoveShield(key1.Shields.BShield);
                            CentreEasing.EaseBuilder k2 = new();
                            k2.Stable(0, 320, 240);
                            k2.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 43, 435), BeatTime(8)));
                            k2.Run((s) => { SetPlayerBoxMission(key2); InstantSetBox(s, 84, 84); InstantTP(s); });
                            key2.RotateTo(270);
                            key2.Shields.RemoveShield(key2.Shields.BShield);
                            CentreEasing.EaseBuilder k3 = new();
                            k3.Stable(0, 320, 240);
                            k3.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 43, 435), BeatTime(8)));
                            k3.Run((s) => { SetPlayerBoxMission(key3); InstantSetBox(s, 84, 84); InstantTP(s); });
                            key3.RotateTo(90);
                            key3.Shields.RemoveShield(key3.Shields.RShield);
                            CentreEasing.EaseBuilder k4 = new();
                            k4.Stable(0, 320, 240);
                            k4.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 43 + 86, 435), BeatTime(8)));
                            k4.Run((s) => { SetPlayerBoxMission(key4); InstantSetBox(s, 84, 84); InstantTP(s); });
                            key4.RotateTo(180);
                            key4.Shields.RemoveShield(key4.Shields.RShield);
                        }));
                    });
                    RegisterFunctionOnce("Line", () =>
                    {
                        CentreEasing.EaseBuilder ce1 = new();
                        ce1.Stable(BeatTime(64), 320, 240);
                        ValueEasing.EaseBuilder ve1 = new();
                        ve1.Stable(BeatTime(64), 90);
                        Line l = new(ce1.GetResult(), ve1.GetResult());
                        CreateEntity(l);
                        CentreEasing.EaseBuilder ce2 = new();
                        ce2.Stable(0, 320, 240);
                        ce2.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 86, 240), BeatTime(8)));
                        ValueEasing.EaseBuilder ve2 = new();
                        ve2.Stable(BeatTime(64), 90);
                        Line l2 = new(ce2.GetResult(), ve2.GetResult());
                        CreateEntity(l2);
                        CentreEasing.EaseBuilder ce3 = new();
                        ce3.Stable(0, 320, 240);
                        ce3.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 86, 240), BeatTime(8)));
                        ValueEasing.EaseBuilder ve3 = new();
                        ve3.Stable(BeatTime(64), 90);
                        Line l3 = new(ce3.GetResult(), ve3.GetResult());
                        CreateEntity(l3);
                        CentreEasing.EaseBuilder ce4 = new();
                        ce4.Stable(0, 320, 240);
                        ce4.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320, 240), new(320 - 86 - 86, 240), BeatTime(8)));
                        ValueEasing.EaseBuilder ve4 = new();
                        ve4.Stable(BeatTime(64), 90);
                        Line l4 = new(ce4.GetResult(), ve4.GetResult());
                        CreateEntity(l4);
                        CentreEasing.EaseBuilder ce5 = new();
                        ce5.Stable(0, 320, 240);
                        ce5.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320, 240), new(320 + 86 + 86, 240), BeatTime(8)));
                        ValueEasing.EaseBuilder ve5 = new();
                        ve5.Stable(BeatTime(64), 90);
                        Line l5 = new(ce5.GetResult(), ve5.GetResult());
                        CreateEntity(l5);
                        CentreEasing.EaseBuilder ce6 = new();
                        ce6.Stable(0, 320 + 43, 240 + 43);
                        ce6.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320 + 43, 240 + 43), new(320 + 86 + 86, 480 - 2), BeatTime(8)));
                        CentreEasing.EaseBuilder ce7 = new();
                        ce7.Stable(0, 320 - 43, 240 + 43);
                        ce7.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320 - 43, 240 + 43), new(320 - 86 - 86, 480 - 2), BeatTime(8)));
                        Line l6 = new(ce6.GetResult(), ce7.GetResult());
                        CreateEntity(l6);
                        CentreEasing.EaseBuilder ce8 = new();
                        ce8.Stable(0, 320, 240 - 43);
                        ce8.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320 + 43, 240 - 43), new(320 + 86 + 86 + 43, 480 - 86 - 2), BeatTime(8)));
                        CentreEasing.EaseBuilder ce9 = new();
                        ce9.Stable(0, 320, 240 - 43);
                        ce9.Insert(BeatTime(8), CentreEasing.EaseOutQuint(new(320 - 43, 240 - 43), new(320 - 86 - 86 - 43, 480 - 86 - 2), BeatTime(8)));
                        Line l7 = new(ce8.GetResult(), ce9.GetResult());
                        CreateEntity(l7);
                        ValueEasing.EaseBuilder Alpha = new();
                        Alpha.Stable(0, 0);
                        Alpha.Insert(BeatTime(4), ValueEasing.Linear(0, 1, BeatTime(4)));
                        Alpha.Run((s) => { l.Alpha = l2.Alpha = l3.Alpha = l4.Alpha = l5.Alpha = l6.Alpha = l7.Alpha = s; });
                        l.Width = l2.Width = l3.Width = l4.Width = l5.Width = l7.Width = l6.Width = 6;
                        l.Depth = l2.Depth = l3.Depth = l4.Depth = l5.Depth = l7.Depth = l6.Depth = 0.99f;
                        l.DrawingColor = l2.DrawingColor = l3.DrawingColor = l4.DrawingColor = l5.DrawingColor = l6.DrawingColor = l7.DrawingColor = new(147, 57, 132);
                    });
                    float speed = 26.5f;
                    float scale = 2.5f;
                    float lateWaitingScale = 0.1f;
                    float camRotation = 40;
                    RegisterFunctionOnce("Move", () =>
                    {
                        Line l = new(CentreEasing.Stable(-3000, 240), CentreEasing.Stable(640 + 3000, 240)) { Alpha = 0, DrawingColor = Color.White, Width = 8, Depth = 0.999f };
                        l.AlphaIncrease(BeatTime(2), 1);
                        CreateEntity(l);
                        ScreenDrawing.UpExtending = 1.45f;

                        CentreEasing.EaseBuilder screen = new();
                        screen.Insert(BeatTime(2), CentreEasing.EaseOutQuart(new(0, 0), new(0, 0), BeatTime(2)));
                        screen.Run(s => ScreenDrawing.ScreenPositionDetla = s);

                        cameraProduction = new ScreenDrawing.Shaders.Filter(Effect3D, 0.2f);
                        ScreenDrawing.SceneRendering.InsertProduction(cameraProduction);

                        ValueEasing.EaseBuilder camera = new();
                        Effect3D.TextureSize = new(640, 480 * (1f + ScreenDrawing.UpExtending));
                        Effect3D.ProjectPointOffect = new(320, Effect3D.TextureSize.Y);
                        Effect3D.ProjectPoint = new(0, 0, 240);

                        ValueEasing.EaseBuilder posMove = new();
                        posMove.Insert(BeatTime(2), ValueEasing.EaseOutCubic(200, 100, BeatTime(2)));
                        posMove.Run(s => Effect3D.ProjectPoint = new(0, 0, s));

                        camera.Insert(BeatTime(2), ValueEasing.EaseOutCubic(0, camRotation, BeatTime(2)));
                        camera.Run(rotation =>
                        {
                            Effect3D.ProjectAxisY = new(0, Cos(rotation), -Sin(rotation));
                            Effect3D.ProjectAxisZ = new(0, Sin(rotation), Cos(rotation));
                            Effect3D.CameraPosition = new(0, -200 + Sin(rotation) * 1, 0);// -Effect3D.ProjectPoint.Z * (1 - Cos(rotation))); 
                        });
                    });
                    RegisterFunctionOnce("T0V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(0);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 1, 0);
                            arr.VoidMode = true;
                            arr.Scale = scale;
                            arr.JudgeType = Arrow.JudgementType.Hold;
                            arr.LateWaitingScale = lateWaitingScale;
                            CreateEntity(arr);
                        }
                    });
                    RegisterFunctionOnce("T1V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(1);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 0, speed, 1, 0);
                            arr.VoidMode = true;
                            arr.Scale = scale;
                            arr.JudgeType = Arrow.JudgementType.Hold;
                            arr.LateWaitingScale = lateWaitingScale;
                            CreateEntity(arr);
                        }
                    });
                    RegisterFunctionOnce("T2V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(2);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 2, speed, 0, 0);
                            arr.VoidMode = true;
                            arr.Scale = scale;
                            arr.JudgeType = Arrow.JudgementType.Hold;
                            arr.LateWaitingScale = lateWaitingScale;
                            CreateEntity(arr);
                        }
                    });
                    RegisterFunctionOnce("T3V", () =>
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            SetPlayerBoxMission(3);
                            Arrow arr = MakeArrow(CurrentTime - BeatTime(i * 0.5f), 1, speed, 0, 0);
                            arr.VoidMode = true;
                            arr.Scale = scale;
                            arr.JudgeType = Arrow.JudgementType.Hold;
                            arr.LateWaitingScale = lateWaitingScale;
                            CreateEntity(arr);
                        }
                    });
                    RegisterFunctionOnce("T0", () =>
                    {
                        SetPlayerBoxMission(0);
                        Arrow arr = MakeArrow(CurrentTime, 1, speed, 1, 0);
                        arr.JudgeType = Arrow.JudgementType.Tap;
                        arr.Scale = scale;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    });
                    RegisterFunctionOnce("T1", () =>
                    {
                        SetPlayerBoxMission(1);
                        Arrow arr = MakeArrow(CurrentTime, 0, speed, 1, 0);
                        arr.JudgeType = Arrow.JudgementType.Tap;
                        arr.Scale = scale;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    });
                    RegisterFunctionOnce("T2", () =>
                    {
                        SetPlayerBoxMission(2);
                        Arrow arr = MakeArrow(CurrentTime, 2, speed, 0, 0);
                        arr.JudgeType = Arrow.JudgementType.Tap;
                        arr.Scale = scale;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    });
                    RegisterFunctionOnce("T3", () =>
                    {
                        SetPlayerBoxMission(3);
                        Arrow arr = MakeArrow(CurrentTime, 1, speed, 0, 0);
                        arr.JudgeType = Arrow.JudgementType.Tap;
                        arr.Scale = scale;
                        arr.LateWaitingScale = lateWaitingScale;
                        CreateEntity(arr);
                    });
                    int playednumber = 5;
                    if (playednumber == 0 || playednumber == 5) CreateChart(BeatTime(8), BeatTime(1), 7.2f, new string[]
                        {
                            "Split(Line)","","","",   "","","","",
                            "","","","",   "","","","",
                            "","","","",   "","","","",
                            "","","","",   "","","","",

                            "","","","",   "","","","",
                            "","","","",   "","","","",
                            "Move","","","",
                        });
                }
            }
            RenderProduction cameraProduction;
            GlobalResources.Effects.CameraShader Effect3D;
            void TagsArrow()
            {
                Arrow[] ars1 = GetAll<Arrow>("Up");
                for (int i = 0; i < ars1.Length; i++)
                {
                    int x = i;
                    SetOffset(ars1[x], -10);
                    if (ars1[x].Way == 0) ars1[x].CentreRotationOffset = -15;
                    if (ars1[x].Way == 2) ars1[x].CentreRotationOffset = 15;
                }
                Arrow[] ars2 = GetAll<Arrow>("Down");
                for (int i = 0; i < ars2.Length; i++)
                {
                    int x = i;
                    SetOffset(ars2[x], 10);
                    if (ars2[x].Way == 0) ars2[x].CentreRotationOffset = 15;
                    if (ars2[x].Way == 2) ars2[x].CentreRotationOffset = -15;
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
            public void Start()
            {
                Effect3D = Shaders.Camera;
                Effect3D.CameraRotation = new(0, 0, 0);

                Knife k1 = new(40, CentreEasing.Stable(320, 240), ValueEasing.Stable(30));
                //CreateEntity(k1);
                game = this;
                GametimeDelta = -0.5f;
                SetSoul(1);
                InstantSetBox(new Vector2(320, 240), 84, 84);
                InstantTP(new Vector2(320, 240));

                bool jump_1 = true;
                if (jump_1)
                {
                    InstantSetBox(new Vector2(320, 240), 84, 84);
                    InstantTP(new Vector2(320, 240));
                    GametimeDelta = -7f + BeatTime(284);
                    PlayOffset = BeatTime(284);
                }
                ScreenDrawing.HPBar.AreaOccupied = new(26, 130, 18, 200);
                ScreenDrawing.HPBar.Vertical = true;
                HeartAttribute.MaxHP = 10000;
                ScreenDrawing.HPBar.HPExistColor = new(0, 161, 255);
                ScreenDrawing.HPBar.HPLoseColor = new(Color.Gray, 0.5f);
            }
        }
    }
}