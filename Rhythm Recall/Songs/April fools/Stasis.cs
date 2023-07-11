using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;

namespace AprilExtends
{
    public class Stasis : WaveConstructor, IWaveSet
    {

        public Stasis() : base(62.5f / (180 / 60)) { }
        public string Music => "Stasis";

        public string FightName => "Stasis";

        public SongInformation Attributes => null;

        public void Easy()
        {
            if (InBeat(1))
            {
                ScreenDrawing.ScreenScale = 0.5f;
                CreateRotArrowB(0);
                CreateRotArrowB(9);
                CreateRotArrowB(18);
                CreateRotArrowB(27);
                CreateRotArrowB(36);
                CreateRotArrowB(45);
                CreateRotArrowA(45);
                CreateRotArrowA(45);
                CreateRotArrowB(45);
                CreateRotArrowB(36);
                CreateRotArrowB(27);
                CreateRotArrowB(18);
                CreateRotArrowB(9);
                CreateRotArrowB(0);
                CreateRotArrowA(0);
                CreateRotArrowA(0);
                CreateRotArrowB(0);
                CreateRotArrowB(9);
                CreateRotArrowB(18);
                CreateRotArrowB(27);
                CreateRotArrowB(36);
                CreateRotArrowB(45);
                CreateRotArrowA(45);
                CreateRotArrowA(45);
                CreateRotArrowB(45);
                CreateRotArrowB(36);
                CreateRotArrowB(27);
                CreateRotArrowB(18);
                CreateRotArrowB(9);
                CreateRotArrowB(0);
                CreateRotArrowA(0);
                CreateRotArrowA(0);
                CreateRotArrowB(0);
                CreateRotArrowB(9);
                CreateRotArrowB(18);
                CreateRotArrowB(27);
                CreateRotArrowB(36);
                CreateRotArrowB(45);
                CreateRotArrowA(45);
                CreateRotArrowA(45);
                CreateRotArrowB(45);
                CreateRotArrowB(36);
                CreateRotArrowB(27);
                CreateRotArrowB(18);
                CreateRotArrowB(9);
                CreateRotArrowB(0);
                CreateRotArrowA(0);
                CreateRotArrowA(0);
                CreateRotArrowB(0);
                CreateRotArrowB(9);
                CreateRotArrowB(18);
                CreateRotArrowB(27);
                CreateRotArrowB(36);
                CreateRotArrowB(45);
                CreateRotArrowA(45);
                CreateRotArrowA(45);
                CreateRotArrowB(45);
                CreateRotArrowB(36);
                CreateRotArrowB(27);
                CreateRotArrowB(18);
                CreateRotArrowB(9);
                CreateRotArrowB(0);
                CreateRotArrowA(0);
                CreateRotArrowA(0);
                CreateRotArrowB(0);
                CreateRotArrowB(9);
                CreateRotArrowB(18);
                CreateRotArrowB(27);
                CreateRotArrowB(36);
                CreateRotArrowB(45);
                CreateRotArrowA(45);
                CreateRotArrowA(45);
                CreateRotArrowB(45);
                CreateRotArrowB(36);
                CreateRotArrowB(27);
                CreateRotArrowB(18);
                CreateRotArrowB(9);
                CreateRotArrowB(0);
                CreateRotArrowA(0);
                CreateRotArrowA(0);
                CreateRotArrowB(0);
                CreateRotArrowB(9);
                CreateRotArrowB(18);
                CreateRotArrowB(27);
                CreateRotArrowB(36);
                CreateRotArrowB(45);
                CreateRotArrowA(45);
                CreateRotArrowA(45);
                CreateRotArrowB(45);
                CreateRotArrowB(36);
                CreateRotArrowB(27);
                CreateRotArrowB(18);
                CreateRotArrowB(9);
                CreateRotArrowB(0);
                CreateRotArrowA(0);
                CreateRotArrowA(0);
                CreateRotArrowB(0);
                CreateRotArrowB(9);
                CreateRotArrowB(18);
                CreateRotArrowB(27);
                CreateRotArrowB(36);
                CreateRotArrowB(45);
                CreateRotArrowA(45);
                CreateRotArrowA(45);
                CreateRotArrowB(45);
                CreateRotArrowB(36);
                CreateRotArrowB(27);
                CreateRotArrowB(18);
                CreateRotArrowB(9);
                CreateRotArrowB(0);
                CreateRotArrowA(0);
                CreateRotArrowA(0);
                CreateRotArrowB(0);
                CreateRotArrowB(9);
                CreateRotArrowB(18);
                CreateRotArrowB(27);
                CreateRotArrowB(36);
                CreateRotArrowB(45);
                CreateRotArrowA(45);
                CreateRotArrowA(45);
                CreateRotArrowB(45);
                CreateRotArrowB(36);
                CreateRotArrowB(27);
                CreateRotArrowB(18);
                CreateRotArrowB(9);
                CreateRotArrowB(0);
                CreateRotArrowA(0);
                CreateRotArrowA(0);
            }
        }

        public void Extreme()
        {
            Arrow[] voids = GetAll<Arrow>("Hold");
            for (int a = 0; a < voids.Length; a++)
            {
                int x = a;
                voids[x].JudgeType = Arrow.JudgementType.Hold;
                voids[x].VoidMode = true;
            }
            if (InBeat(0))
            {
                ScreenDrawing.CameraEffect.SizeExpand(40.5f, BeatTime(20));
            }
            if (InBeat(0))
            {
                RegisterFunctionOnce("LineEvent", () =>
                {
                    CentreEasing.EaseBuilder ce = new();
                    ce.Insert(BeatTime(8), CentreEasing.EaseInQuart(new(0, 240), new(320, 240), BeatTime(8)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutQuad(new(320, 240), new(240, 240), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseInQuad(new(240, 240), new(320, 240), BeatTime(0.75f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuart(new(320, 240), new(120, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(0.25f), CentreEasing.EaseOutQuart(new(120, 240), new(160, 240), BeatTime(0.25f)));
                    ce.Insert(BeatTime(0.25f), CentreEasing.EaseOutQuart(new(160, 240), new(200, 240), BeatTime(0.25f)));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuart(new(200, 240), new(240, 240), BeatTime(1)));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuart(new(240, 240), new(300, 240), BeatTime(1)));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuart(new(300, 240), new(240, 240), BeatTime(1)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseOutQuad(new(240, 240), new(180, 240), BeatTime(0.75f)));
                    ce.Insert(BeatTime(0.75f), CentreEasing.EaseInQuad(new(180, 240), new(320, 240), BeatTime(0.75f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuart(new(320, 240), new(240, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(1.5f), CentreEasing.EaseOutQuart(new(240, 240), new(100, 240), BeatTime(1.5f)));
                    ce.Insert(BeatTime(0.25f), CentreEasing.EaseOutQuart(new(100, 240), new(140, 240), BeatTime(0.25f)));
                    ce.Insert(BeatTime(0.25f), CentreEasing.EaseOutQuart(new(140, 240), new(180, 240), BeatTime(0.25f)));
                    ce.Insert(BeatTime(1), CentreEasing.EaseOutQuart(new(180, 240), new(240, 240), BeatTime(1)));
                    ce.Insert(BeatTime(4), CentreEasing.EaseInQuart(new(240, 240), new(-320, 240), BeatTime(4)));
                    ValueEasing.EaseBuilder ve = new();
                    ve.Insert(0, ValueEasing.Stable(90));
                    ValueEasing.EaseBuilder vea = new();
                    Line l1 = new(ce.GetResult(), ve.GetResult()) { Alpha = 0 };
                    Line l2 = new(ce.GetResult(), ve.GetResult()) { Alpha = 0 };
                    vea.Insert(BeatTime(8), ValueEasing.EaseInQuart(0, 1, BeatTime(8)));
                    vea.Run((m) =>
                    {
                        l1.Alpha = m;
                        l2.Alpha = m;
                    });

                    CreateEntity(l1);
                    CreateEntity(l2);
                    l2.ObliqueMirror = true;
                    //l1.ObliqueMirror = true;
                    //l2.ObliqueMirror = true;
                });
                RegisterFunction("ScreenE5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(2));
                });
                RegisterFunction("ScreenE7.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(7.5f, BeatTime(2));
                });
                RegisterFunction("ScreenRR8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), true);
                });
                RegisterFunction("ScreenRL8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), false);
                });
                RegisterFunction("ScreenS5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(2));
                });
                RegisterFunction("ScreenRR8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), true);
                });
                RegisterFunction("ScreenRL8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), false);
                });
                RegisterFunction("ScreenE5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(1));
                });
                RegisterFunction("ScreenS5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(1));
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 5.5f, new string[]
                {
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",

                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",

                    "R(LineEvent)","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",

                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                });
            }
            if (InBeat(16))
            {
                RegisterFunctionOnce("RotScreen", () =>
                {
                    ValueEasing.EaseBuilder Rot = new();
                    Rot.Insert(BeatTime(4), ValueEasing.EaseOutQuart(0, -360, BeatTime(4)));
                    Rot.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                RegisterFunctionOnce("LineEvent01", () =>
                {
                    Line l1 = new(new Vector2(120, 0), new Vector2(120, 480)) { Alpha = 0 };
                    Line l2 = new(new Vector2(120, 0), new Vector2(120, 480)) { Alpha = 0 };
                    ValueEasing.EaseBuilder alpha = new();
                    alpha.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1, -0.01f, BeatTime(1)));
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                        l2.Alpha = m;
                    });
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l2.ObliqueMirror = true;
                });
                RegisterFunctionOnce("LineEvent02", () =>
                {
                    Line l1 = new(new Vector2(160, 0), new Vector2(160, 480)) { Alpha = 0 };
                    Line l2 = new(new Vector2(160, 0), new Vector2(160, 480)) { Alpha = 0 };
                    ValueEasing.EaseBuilder alpha = new();
                    alpha.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1, -0.01f, BeatTime(1)));
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                        l2.Alpha = m;
                    });
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l2.ObliqueMirror = true;
                });
                RegisterFunctionOnce("LineEvent03", () =>
                {
                    Line l1 = new(new Vector2(200, 0), new Vector2(200, 480)) { Alpha = 0 };
                    Line l2 = new(new Vector2(200, 0), new Vector2(200, 480)) { Alpha = 0 };
                    ValueEasing.EaseBuilder alpha = new();
                    alpha.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1, -0.01f, BeatTime(1)));
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                        l2.Alpha = m;
                    });
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l2.ObliqueMirror = true;
                });
                RegisterFunctionOnce("LineEvent04", () =>
                {
                    Line l1 = new(new Vector2(100, 0), new Vector2(100, 480)) { Alpha = 0 };
                    Line l2 = new(new Vector2(100, 0), new Vector2(100, 480)) { Alpha = 0 };
                    ValueEasing.EaseBuilder alpha = new();
                    alpha.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1, -0.01f, BeatTime(1)));
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                        l2.Alpha = m;
                    });
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l2.ObliqueMirror = true;
                });
                RegisterFunctionOnce("LineEvent05", () =>
                {
                    Line l1 = new(new Vector2(140, 0), new Vector2(140, 480)) { Alpha = 0 };
                    Line l2 = new(new Vector2(140, 0), new Vector2(140, 480)) { Alpha = 0 };
                    ValueEasing.EaseBuilder alpha = new();
                    alpha.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1, -0.01f, BeatTime(1)));
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                        l2.Alpha = m;
                    });
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l2.ObliqueMirror = true;
                });
                RegisterFunctionOnce("LineEvent06", () =>
                {
                    Line l1 = new(new Vector2(180, 0), new Vector2(180, 480)) { Alpha = 0 };
                    Line l2 = new(new Vector2(180, 0), new Vector2(180, 480)) { Alpha = 0 };
                    ValueEasing.EaseBuilder alpha = new();
                    alpha.Insert(BeatTime(1), ValueEasing.EaseOutQuart(1, -0.01f, BeatTime(1)));
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                        l2.Alpha = m;
                    });
                    CreateEntity(l1);
                    CreateEntity(l2);
                    l2.ObliqueMirror = true;
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "(#0.75#$1)(#0.75#$31)(ScreenE5.5)","","","",    "","","","",
                    "","","","",    "(#0.75#$2)(#0.75#$01)(ScreenE7.5)","","","",
                    "","","","",    "","","","",
                    "$1(LineEvent01)","","$3(LineEvent02)","",    "$1(LineEvent03)","","","",

                    "","","","",    "(#0.3#$1)(#0.3#$21)(ScreenRR8)","","","",
                    "","","","",    "(#0.3#$3)(#0.3#$01)(ScreenRL8)","","","",
                    "","","","",    "#0.75#$1(ScreenS5.5)","","","",
                    "","","","",    "","","","",

                    "(#0.75#$1)(#0.75#$01)(ScreenE5.5)","","","",    "","","","",
                    "","","","",    "(#0.75#$2)(#0.75#$31)(ScreenE7.5)","","","",
                    "","","","",    "","","","",
                    "$2(LineEvent04)","","$0(LineEvent05)","",    "$2(LineEvent06)","","","",

                    "(#2.5#$2)(#2.5#$01)(RotScreen)","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                });
            }
            if (InBeat(32))
            {
                BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "(#0.5#$20)(ScreenRR8F)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)","","","",    "$30","","","",
                    "(#0.5#$20)(ScreenRR8F)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)","","","",    "$30","","","",

                    "(#0.5#$20)(ScreenRR8F)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)","","","",    "$30","","","",
                    "(#0.5#$20)(ScreenRR8F)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)","","","",    "$30","","","",

                    "(#0.5#$20)(ScreenRR8F)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)","","","",    "$30","","","",
                    "(#0.5#$20)(ScreenRR8F)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)","","","",    "$30","","","",

                    "(#0.5#$20)(ScreenRR8F)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)","","","",    "$30","","","",
                    "(#0.5#$20)(ScreenRR8F)","","","",    "$31","","","",
                    "($20)($01)(ScreenS5.5F)","","","",    "","","","",
                });
            }
            if (InBeat(48))
            {
                RegisterFunctionOnce("RotBarrage", () =>
                {
                    ValueEasing.EaseBuilder Rot = new();
                    Rot.Insert(BeatTime(4), ValueEasing.EaseOutQuart(0, 720, BeatTime(4)));
                    Rot.Run((m) =>
                    {
                        Heart.InstantSetRotation(m);
                    });
                });
                RegisterFunctionOnce("ScreenS", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseInQuart(1, 2, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenScale = m;
                    });
                });
                RegisterFunctionOnce("ScreenSBack", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutBack(2, 1, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenScale = m;
                    });
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "$00(ScreenE5.5F)","","$01","",    "$2(ScreenE5.5F)","","$21","",
                    "$00(ScreenE5.5F)","","$01","",    "$2(ScreenE5.5F)","","$21","",
                    "$00(ScreenE5.5F)","","$01","",    "$2(ScreenE5.5F)","","$21","",
                    "$00(ScreenE5.5F)","","$01","",    "$2(ScreenE5.5F)","","$21","",

                    "$00(ScreenE5.5F)","","$01","",    "$2(ScreenE5.5F)","","$21","",
                    "$00(ScreenE5.5F)","","$01","",    "$2(ScreenE5.5F)","","$21","",
                    "$00(ScreenE5.5F)","","$01","",    "$2(ScreenE5.5F)","","$21","",
                    "$00(ScreenE5.5F)","","$01","",    "$2(ScreenE5.5F)","","$21","",

                    "($00)($20)(ScreenS)(ScreenE5.5F)","","($01)($21)(ScreenE5.5F)","",    "($00)($20)(ScreenE5.5F)","","($01)($21)(ScreenE5.5F)","",
                    "($00)($20)(ScreenE5.5F)","","($01)($21)(ScreenE5.5F)","",    "($00)($20)(ScreenE5.5F)","","($01)($21)(ScreenE5.5F)","",
                    "($00)($20)(ScreenE5.5F)","","($01)($21)(ScreenE5.5F)","",    "($00)($20)(ScreenE5.5F)","","($01)($21)(ScreenE5.5F)","",
                    "$0(ScreenE5.5F)","","$1(ScreenE5.5F)","",    "$2(ScreenE5.5F)","","$3(ScreenE5.5F)","",

                    "(RotBarrage)(ScreenSBack)","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                });
            }
            if (InBeat(64))
            {

                //RegisterFunctionOnce("Line1", () =>
                //{
                //    CentreEasing.EaseBuilder ce = new();
                //    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(0, 120, BeatTime(4)), ValueEasing.EaseOutSine(0, 180, BeatTime(4))));
                //    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(120, 280, BeatTime(4)), ValueEasing.EaseOutQuad(180, 160, BeatTime(4))));
                //    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(280, 260, BeatTime(4)), ValueEasing.EaseOutSine(160, 300, BeatTime(4))));
                //    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(260, 320, BeatTime(4)), ValueEasing.EaseOutQuad(300, 360, BeatTime(4))));
                //    ce.Insert(BeatTime(4), CentreEasing.Combine(ValueEasing.EaseOutQuart(320, 420, BeatTime(4)), ValueEasing.EaseOutQuad(360, 160, BeatTime(4))));
                //    ce.Insert(BeatTime(8), CentreEasing.Accerlating(new(0, 0.5f), new(0, 0.15f)));
                //    ValueEasing.EaseBuilder ve1 = new();
                //    ve1.Insert(0, ValueEasing.Stable(90));
                //    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(90, 90 + 60, BeatTime(4)));
                //    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(90 + 60, 40, (BeatTime(4))));
                //    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(40, 130, BeatTime(4)));
                //    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(130, 160, (BeatTime(4))));
                //    ve1.Insert(BeatTime(4), ValueEasing.EaseOutBack(160, 180 + 45, (BeatTime(4))));
                //    ValueEasing.EaseBuilder ve2 = new();
                //    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(0, 60, BeatTime(4)));
                //    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(60, 40 - 90, (BeatTime(4))));
                //    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(40 - 90, 130 - 90, BeatTime(4)));
                //    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(130 - 90, 160 - 90, (BeatTime(4))));
                //    ve2.Insert(BeatTime(4), ValueEasing.EaseOutBack(160 - 90, 90 + 45, (BeatTime(4))));
                //    Line l1 = new(ce.GetResult(), ve1.GetResult()) { Alpha = 0.65f };
                //    Line l2 = new(ce.GetResult(), ve2.GetResult()) { Alpha = 0.65f };
                //    CreateEntity(l1);
                //    CreateEntity(l2);

                //    //l1.ObliqueMirror = true;
                //    //l2.ObliqueMirror = true;
                //});
                RegisterFunctionOnce("ScreenS", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(2), ValueEasing.EaseOutQuart(1.05f, 1, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenScale = m;
                    });
                });
                RegisterFunctionOnce("ScreenRL", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutQuart(2, 0, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                RegisterFunctionOnce("ScreenRR", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutQuart(-2, 0, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 4, new string[]
                {
                    "R(ScreenS)","","","",    "","","","",
                    "R","","","",    "","","","",
                    "R1'1.5(ScreenRL)","","","",    "","","","",
                    "R","","","",    "","","","",

                    "R(ScreenS)","","","",    "","","","",
                    "","","","",    "R","","","",
                    "R(R1'1.5)(ScreenRL)","","","",    "R","","","",
                    "R","","","",    "","","","",

                    "R'2(ScreenS)","+0'2.5{Hold}","+0'2.5{Hold}","+0'2.5{Hold}",    "","","","",
                    "R","","","",    "","","","",
                    "R1'1.5(ScreenRR)","","","",    "","","","",
                    "R","","","",    "","","","",

                    "R(ScreenS)","","","",    "","","","",
                    "","","","",    "R","","","",
                    "R(R1'1.5)(ScreenRR)","","","",    "R","","","",
                    "R","","","",    "","","","",
                });
            }
            if (InBeat(80))
            {

                RegisterFunctionOnce("ScreenS", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(2), ValueEasing.EaseOutQuart(1.05f, 1, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenScale = m;
                    });
                });
                RegisterFunctionOnce("ScreenRL", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutQuart(2, 0, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                RegisterFunctionOnce("ScreenRR", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutQuart(-2, 0, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 4, new string[]
                {
                    "R(ScreenS)","","","",    "","","","",
                    "R","","","",    "","","","",
                    "R1'1.5(ScreenRL)","","","",    "","","","",
                    "R","","","",    "","","","",

                    "R(ScreenS)","","","",    "","","","",
                    "","","","",    "R","","","",
                    "R(R1'1.5)(ScreenRL)","","","",    "R","","","",
                    "R","","","",    "","","","",

                    "R'2(ScreenS)","+0'2.5{Hold}","+0'2.5{Hold}","+0'2.5{Hold}",    "","","","",
                    "R","","","",    "","","","",
                    "R1'1.5(ScreenRR)","","","",    "","","","",
                    "R","","","",    "","","","",

                    "$2'1.5","","$3'1.5","",    "$2'1.5","","$3'1.5","",
                    "$2'1.5","","$3'1.5","",    "$2'1.5","","$3'1.5","",
                    "$2'1.5","","$0'1.5","",    "$2'1.5","","$0'1.5","",
                    "$2'1.5","","$0'1.5","",    "$2'1.5","","$0'1.5","",
                });
            }
            if (InBeat(96))
            {
                RegisterFunctionOnce("ScreenS", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(1.5f), ValueEasing.EaseOutQuart(1.05f, 1, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenScale = m;
                    });
                });
                RegisterFunctionOnce("ScreenSAdd", () =>
                {
                    ScreenDrawing.ScreenScale += 0.05f;
                });
                RegisterFunctionOnce("ScreenRL", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutQuart(2, 0, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                RegisterFunctionOnce("ScreenRR", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutQuart(-2, 0, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                {
                    "$2(R1)(ScreenS)","","","",    "$2","","","",
                    "$3(R1)","","","",    "$3","","","",
                    "$2(ScreenRL)","","","",    "$2","","","",
                    "$3(R1)","","","",    "$3","","","",

                    "$2(R1)(ScreenS)","","","",    "$2","","","",
                    "$3","","","",    "$3(R1)","","","",
                    "$2(R1)(ScreenRR)","","","",    "$2(R1)","","","",
                    "$3(R1)","","","",    "$3","","","",

                    "($2)(R1'2)(ScreenS)","+01'2.5{Hold}","+01'2.5{Hold}","+01'2.5{Hold}",    "$2","","","",
                    "$3(R1)","","","",    "$3","","","",
                    "$2(ScreenRL)","","","",    "$2","","","",
                    "$3(R1)","","","",    "$3","","","",

                    "$2(R1)(ScreenS)","","","",    "$2","","","",
                    "$3","","","",    "$3(R1)","","","",
                    "R(R1)(ScreenSAdd)","","","",    "","","R(R1)(ScreenSAdd)","",
                    "","","","",    "R(R1)(ScreenSAdd)","","","",
                });
            }
            if (InBeat(112))
            {
                RegisterFunctionOnce("Arrow", () =>
                {
                    float s = 0;
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseInQuart(0, 10, BeatTime(8)));
                    S.Run((m) =>
                    {
                        s = m;
                    });
                    Arrow arrow = MakeArrow(BeatTime(112 + 13), "R", 6, Rand(0, 1), 0);
                    CreateEntity(arrow);
                    AddInstance(new TimeRangedEvent(BeatTime(4), () =>
                    {
                        arrow.Delay(s);
                    }));
                });
            }
            if (InBeat(112))
            {
                RegisterFunctionOnce("ScreenSBack", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(2), ValueEasing.EaseOutQuart(1.15f, 1, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenScale = m;
                    });
                });
                RegisterFunctionOnce("ScreenRL", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutQuart(2, 0, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                RegisterFunctionOnce("ScreenRR", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseOutQuart(-2, 0, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenAngle = m;
                    });
                });
                RegisterFunctionOnce("ScreenS", () =>
                {
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(2), ValueEasing.EaseOutQuart(1.05f, 1, BeatTime(4)));
                    S.Run((m) =>
                    {
                        ScreenDrawing.ScreenScale = m;
                    });
                });

                BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                {
                    "$0(R1)(ScreenSBack)","","","",    "$0","","","",
                    "$1(R1)","","","",    "$1","","","",
                    "$0(ScreenRL)","","","",    "$0","","","",
                    "$1(R1)","","","",    "$1","","","",

                    "$0(R1)(ScreenS)","","","",    "$0","","","",
                    "$1","","","",    "$1(R1)","","","",
                    "$0(R1)(ScreenRR)","","","",    "$0(R1)","","","",
                    "$1(R1)","","","",    "$1","","","",

                    "($0)(R1'2)(ScreenS)","+01'2.5{Hold}","+01'2.5{Hold}","+01'2.5{Hold}",    "$0","","","",
                    "$1(R1)","","","",    "$1","","","",
                    "$0(ScreenRL)","","","",    "$0","","","",
                    "$1(R1)","","","",    "$1","","","",

                    "R","","","",    "R","","+01'1.2","",
                    "R","","","",    "R","","","",
                    "R","","+01'1.2","",   "R","","","",
                    "(R)(+01)","","","",    "","","","",
                });
            }
            if (InBeat(128))
            {
                BarrageCreate(BeatTime(4), BeatTime(1), 6, new string[]
                {
                    "R","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "R","","","",   "R","","","",
                    "","","","",    "","","","",

                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",

                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",

                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                });
            }
        }

        public void ExtremePlus()
        {
            throw new NotImplementedException();
        }
        float offset = 40;
        void CreateRotArrowA(float rot)
        {
            Arrow r1 = MakeArrow(offset, 0, 10, 0, 0);
            offset += 4.9f;
            CreateEntity(r1);
            r1.CentreRotationOffset = rot;
        }
        void CreateRotArrowB(float rot)
        {
            Arrow r1 = MakeArrow(offset, 0, 10, 0, 0);
            CreateEntity(r1);
            r1.CentreRotationOffset = rot;
        }
        void CreateRotArrow2(float offset, float rot)
        {
            Arrow r1 = MakeArrow(offset, 0, 5, 0, 0);
            CreateEntity(r1);
            r1.CentreRotationOffset = rot;
        }

        public void Hard()
        {
            if (InBeat(1))
            {
                for (int a = 0; a < 60; a++)
                    CreateRotArrow2(40, a * 6);

            }
            if (GameStates.IsKeyPressed(InputIdentity.Alternate))
            {
                Arrow[] aa = GetAll<Arrow>();
                DelayBeat(0.25f, () =>
                {
                    for (int a = 0; a < aa.Length; a++)
                    {
                        int i = a;
                        aa[i].Delay(BeatTime(4));
                    }
                });
            }
            /*   if (GameStates.IsKeyPressed(Keys.V))
               {
                   Arrow[] aa = GetAll<Arrow>();
                   for (int a = 0; a < aa.Length; a++)
                   {
                       int i = a;
                       ValueEasing.EaseBuilder ve = new();
                       ve.Insert(BeatTime(8), ValueEasing.EaseOutElastic(aa[i].CentreRotationOffset, 0, BeatTime(8)));
                       ve.Run((s) => { aa[i].CentreRotationOffset = s; });
                   }

               }
               if (GameStates.IsKeyPressed(Keys.B))
               {
                   Arrow[] aa = GetAll<Arrow>();
                   for (int a = 0; a < aa.Length; a++)
                   {
                       int i = a;
                       ValueEasing.EaseBuilder ve = new();
                       ve.Insert(BeatTime(8), ValueEasing.EaseOutElastic(0, i*6, BeatTime(8)));
                       ve.Run((s) => { aa[i].CentreRotationOffset = s; });
                   }

               }*/
        }

        public void Noob()
        {
            throw new NotImplementedException();
        }

        public void Normal()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            GametimeDelta = -2.5f;
            SetGreenBox();
            HeartAttribute.MaxHP = 50;
            HeartAttribute.DamageTaken = 1;
            TP();
            SetSoul(1);
        }
    }
}