using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.Entities.EasingUtil;

namespace AprilExtends
{
    public class Stasis : WaveConstructor, IWaveSet
    {

        public Stasis() : base(62.5f / (180 / 60)) { }
        public string Music => "Stasis";

        public string FightName => "Stasis";

        public SongInformation Attributes => new ThisInformation();

        public class ThisInformation : SongInformation
        {
            public override string SongAuthor => "Maozon";
            public override string PaintAuthor => "From Lanota";
            public override string BarrageAuthor => "Uranusqwq";
            public override string AttributeAuthor => "Uranusqwq";
            public override string Extra => "The song is unauthorized, and \nif that infringe a copyright, \ncall T-mas by email:480642510@qq.com, \nwe will delete this song immediately";
        }

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
            for(int a=0;a<voids.Length;a++)
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
                    vea.Insert(BeatTime(16), ValueEasing.Stable(1));
                    vea.Insert(BeatTime(2), ValueEasing.Linear(1, 0, BeatTime(2)));
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
                RegisterFunctionOnce("LineEvent00", () =>
                {
                    
                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2), CentreEasing.EaseInQuad(new(320, 240), new(-100, 240), BeatTime(2)));
                    ValueEasing.EaseBuilder alpha = new();
                    ValueEasing.EaseBuilder rot = new();
                    rot.Insert(BeatTime(1), ValueEasing.Stable(90));
                    alpha.Insert(BeatTime(2), ValueEasing.EaseOutQuart(0, 1, BeatTime(2)));
                    alpha.Insert(BeatTime(1), ValueEasing.Linear(1, -0.01f, BeatTime(1)));
                    Line l1 = new(c.GetResult(), rot.GetResult()) { Alpha = 0 };
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                    });
                    CreateEntity(l1);
                });
                RegisterFunctionOnce("LineEvent01", () =>
                {

                    CentreEasing.EaseBuilder c = new();
                    c.Insert(BeatTime(2), CentreEasing.EaseInQuad(new(320, 240), new(740, 240), BeatTime(2)));
                    ValueEasing.EaseBuilder alpha = new();
                    ValueEasing.EaseBuilder rot = new();
                    rot.Insert(BeatTime(1), ValueEasing.Stable(90));
                    alpha.Insert(BeatTime(2), ValueEasing.EaseOutQuart(0, 1, BeatTime(2)));
                    alpha.Insert(BeatTime(1), ValueEasing.Linear(1, -0.01f, BeatTime(1)));
                    Line l1 = new(c.GetResult(), rot.GetResult()) { Alpha = 0 };
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                    });
                    CreateEntity(l1);
                });
                RegisterFunctionOnce("LineEvent02", () =>
                {

                    CentreEasing.EaseBuilder c1 = new();
                    CentreEasing.EaseBuilder c2 = new();
                    c1.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new(0, 240), new(700, 240), BeatTime(2)));
                    c2.Insert(BeatTime(2), CentreEasing.EaseOutQuad(new(640, 240), new(-100, 240), BeatTime(2)));
                    ValueEasing.EaseBuilder alpha = new();
                    ValueEasing.EaseBuilder rot = new();
                    rot.Insert(BeatTime(1), ValueEasing.Stable(90));
                    alpha.Insert(BeatTime(2), ValueEasing.EaseOutQuad(1, -0.01f, BeatTime(2)));
                    Line l1 = new(c1.GetResult(), rot.GetResult()) { Alpha = 1 };
                    Line l2 = new(c2.GetResult(), rot.GetResult()) { Alpha = 1 };
                    alpha.Run((m) =>
                    {
                        l1.Alpha = m;
                        l2.Alpha = m;
                    });
                    CreateEntity(l1);
                    CreateEntity(l2);
                });
                BarrageCreate(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "(#0.5#$20)(ScreenRR8F)(LineEvent00)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)(LineEvent01)","","","",    "$30","","","",
                    "(#0.5#$20)(ScreenRR8F)(LineEvent00)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)(LineEvent01)","","","",    "$30","","","",//4

                    "(#0.5#$20)(ScreenRR8F)(LineEvent00)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)(LineEvent01)","","","",    "$30","","","",
                    "(#0.5#$20)(ScreenRR8F)(LineEvent00)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)(LineEvent01)","","","",    "$30","","","",//8

                    "(#0.5#$20)(ScreenRR8F)(LineEvent00)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)(LineEvent01)","","","",    "$30","","","",
                    "(#0.5#$20)(ScreenRR8F)(LineEvent00)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)(LineEvent01)","","","",    "$30","","","",//12

                    "(#0.5#$20)(ScreenRR8F)(LineEvent00)","","","",    "$31","","","",
                    "(#0.5#$01)(ScreenRL8F)(LineEvent01)","","","",    "$30","","","",
                    "(#0.5#$20)(ScreenRR8F)(LineEvent00)","","","",    "$31","","","",
                    "($20)($01)(ScreenS5.5F)(LineEvent02)","","","",    "","","","",//16
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
                    ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale + 0.05f;
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

                    "($2)(R1)(ScreenS)","+01'1.5{Hold}","+01'1.5{Hold}","+01'1.5{Hold}",    "$2","","","",
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
                    Arrow arrow = MakeArrow(BeatTime(112 + 13), "R", 6, Rand(0, 1), 0);
                    float s = 0;
                    ValueEasing.EaseBuilder S = new();
                    S.Insert(BeatTime(4), ValueEasing.EaseInQuart(0, 10, BeatTime(8)));
                    S.Run((m) =>
                    {
                        s = m;
                        arrow.Delay(m);
                    });
                    CreateEntity(arrow);        
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

                    "($0)(R1)(ScreenS)","+01'1.5{Hold}","+01'1.5{Hold}","+01'1.5{Hold}",    "$0","","","",
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
                BarrageCreate(BeatTime(4), BeatTime(1), 5, new string[]
                {
                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "(^R12'2.5)(^+012'2.5)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "(^R12'2.5)(^+012'2.5)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "(^R12'2.5)(^+012'2.5)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "(^R12'2.5)(^+012'2.5)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                });
            }
            if (InBeat(144))
            {
                BarrageCreate(BeatTime(4), BeatTime(1), 5, new string[]
                {
                    "(R02'1.1)(R1)","","","",    "R1","","","",
                    "R0{B}","","","",    "R0{B}","","","",
                    "(^R02'2.5)(^+002'2.5)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",

                    "(R02'1.1)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",
                    "(^R02'2.5)(^+002'2.5)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",

                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "(^R12'2.5)(^+012'2.5)(R1{A})","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",

                    "(R12'1.1)(R1{A})","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "(^R12'2.5)(^+012'2.5)(R1{A})","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                });
                DelayBeat(4, () =>
                {
                    Arrow[] tagB = GetAll<Arrow>("B");
                    foreach (Arrow b in tagB)
                    {
                        b.ResetColor(1);
                    }
                });
                DelayBeat(12, () =>
                {
                    Arrow[] tagA = GetAll<Arrow>("A");
                    foreach (Arrow a in tagA)
                    {
                        a.ResetColor(0);
                    }
                });
            }
            if (InBeat(160))
            {
                BarrageCreate(BeatTime(4), BeatTime(1), 5, new string[]
                {
                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "(^R12'2.5)(^+012'2.5)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "(^R12'2.5)(^+012'2.5)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "(^R12'2.5)(^+012'2.5)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(R12'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "(^R12'2.5)(^+012'2.5)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                });
            }
            if (InBeat(176))
            {
                BarrageCreate(BeatTime(4), BeatTime(1), 5, new string[]
                {
                    "(R02'1.1)(R1)","","","",    "R1","","","",
                    "R0{B}","","","",    "R0{B}","","","",
                    "(^R02'2.5)(^+002'2.5)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",

                    "(R02'1.1)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",
                    "(^R02'2.5)(^+002'2.5)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",

                    "R0","","","",    "","","","",
                    "R0","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",

                    "","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "(R1{A})","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "","","","",

                    "($00)($21)","","","",    "","","","",
                });
                DelayBeat(4, () =>
                {
                    Arrow[] tagB = GetAll<Arrow>("B");
                    foreach (Arrow b in tagB)
                    {
                        b.ResetColor(1);
                    }
                });
                DelayBeat(12, () =>
                {
                    Arrow[] tagA = GetAll<Arrow>("A");
                    foreach (Arrow a in tagA)
                    {
                        a.ResetColor(0);
                    }
                });
            }
            if (InBeat(192))
            {
                RegisterFunctionOnce("ArrowShake", () =>
                {
                    float x = 0;
                    //SimplifiedEasing.RunEase(s => x = s, ValueEasing.EaseOutElastic(100,0,BeatTime(16)));
                    Arrow b = MakeArrow(BeatTime(5), 3, 5, 1, 0);
                    CreateEntity(b);
                    DelayBeat(4, () =>
                    {
                        b.Stop(BeatTime(14));
                    });
                    ForBeat120(16, () =>
                    {
                        b.Offset = new(x, 0);
                    });
                });
                RegisterFunctionOnce("BoxShake", () =>
                {
                    SimplifiedEasing.RunEase((s) =>
                    {
                        BoxStates.Centre = new Vector2(320 + s, 240);
                        Heart.Centre = BoxStates.Centre;
                    },
                        SimplifiedEasing.Scale(
                        SimplifiedEasing.LinkEase(
                        SimplifiedEasing.EaseIn(BeatTime(12f), 700, SimplifiedEasing.EaseState.Quart),
                        SimplifiedEasing.EaseOut(BeatTime(4f), -700, SimplifiedEasing.EaseState.Expo))
                        ,
                        SimplifiedEasing.Alternate(2f, SimplifiedEasing.Stable(1f, 1f), SimplifiedEasing.Stable(1, -1)))

                    );
                });
                RegisterFunctionOnce("ScreenEffect", () =>
                {
                    SimplifiedEasing.RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    },
                        SimplifiedEasing.EaseOut(BeatTime(12), -720, SimplifiedEasing.EaseState.Expo)
                    );
                    SimplifiedEasing.RunEase((b) =>
                    {
                        ScreenDrawing.ScreenPositionDetla = new(0 + b, 0);
                    },
                        SimplifiedEasing.Scale(
                        SimplifiedEasing.LinkEase(
                        SimplifiedEasing.EaseIn(BeatTime(12f), 200, SimplifiedEasing.EaseState.Quart),
                        SimplifiedEasing.EaseOut(BeatTime(4f), -200, SimplifiedEasing.EaseState.Expo))
                        ,
                        SimplifiedEasing.Alternate(2f, SimplifiedEasing.Stable(1f, 1f), SimplifiedEasing.Stable(1, -1)))

                    );
                });
                RegisterFunctionOnce("ScreenScale", () =>
                {
                    SimplifiedEasing.RunEase((nb) =>
                    {
                        ScreenDrawing.ScreenScale = 1 + nb;
                    },
                        SimplifiedEasing.EaseIn(BeatTime(1), 1, SimplifiedEasing.EaseState.Circ),
                        SimplifiedEasing.EaseOut(BeatTime(12), -1, SimplifiedEasing.EaseState.Expo)
                    );
                });
                BarrageCreate(BeatTime(0), BeatTime(1), 0, new string[]
                {
                    "ArrowShake","","","",  "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "ScreenScale","","","",    "","","","",
                    "(BoxShake)(ScreenEffect)","","","",    "","","",""
                });
            }
            if (InBeat(206))
            {
                
                RegisterFunctionOnce("ArrowEffect", () =>
                {
                    Arrow[] tagA = GetAll<Arrow>("A");
                    foreach (Arrow b in tagA)
                    {
                        b.Offset = new(0, -400);
                    }
                    float o = 0;
                    SimplifiedEasing.RunEase((e) =>
                    {
                        foreach (Arrow b in tagA)
                        {
                            b.Offset = new(0, 0 + o);
                        }
                        o = e;
                    },
                        SimplifiedEasing.Stable(BeatTime(2),-400),
                        SimplifiedEasing.EaseOut(BeatTime(4), 400, SimplifiedEasing.EaseState.Elastic)
                    );
                    Arrow[] tagB = GetAll<Arrow>("B");
                    foreach (Arrow a in tagB)
                    {
                        a.Offset = new(0, 400);
                    }
                    float c = 0;
                    SimplifiedEasing.RunEase((w) =>
                    {
                        foreach (Arrow a in tagB)
                        {
                            a.Offset = new(0, 0 + c);
                        }
                        c = w;
                    },
                        SimplifiedEasing.Stable(BeatTime(2), 400),
                        SimplifiedEasing.EaseOut(BeatTime(4), -400, SimplifiedEasing.EaseState.Elastic)
                    );
                });
                BarrageCreate(BeatTime(2), BeatTime(1), 5, new string[]
                {
                    "ArrowEffect","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",

                    "$0{A}","","$2{A}","",    "$0{B}","","$2{B}","",
                    "$0{A}","","$2{A}","",    "$0{B}","","$2{B}","",
                    "$0{A}","","$2{A}","",    "$0{B}","","$2{B}","",
                    "$0{A}","","$2{A}","",    "$0{B}","","$2{B}","",

                    "$0'1.1","","$2'1.1","",    "$0'1.2","","$2'1.2","",
                    "$0'1.3","","$2'1.3","",    "$0'1.4","","$2'1.4","",
                    "$0'1.5","","$1'1.5","",    "$2'1.6","","$3'1.6","",
                    "$0'1.7","","$1'1.7","",    "$2'1.8","","$3'1.8","",

                    "$0'1.9","","$1'1.9","",    "$2'2","","$3'2","",
                    "$0'2.1","","$1'2.1","",    "$2'2.2","","$3'2.2","",
                    "$0'2.3","","$1'2.3","",    "$2'2.4","","$3'2.4","",
                    "$0'2.5","","$1'2.5","",    "$2'2.5","","$3'2.5","",

                    "$0'3","","$1'3.5","",    "$2'4","","$3'4.5","",
                    "$0'5","","$1'5.5","",    "$2'6","","$3'6.5","",
                    "$0'7","","$1'7.5","",    "$2'8","","$3'8.5","",
                    "$0'9","","$1'9.5","",    "$2'10","","$3'10","",
                    //
                    "<$00'10","","<$10'9.5","",    "<$20'9","","<$30'8.5","",
                    "<$0'8","","<$1'7.5","",    "<$2'7","","<$3'6.5","",
                    "<$0'6","","<$1'5.6","",    "<$2'5.3","","<$3'5","",
                    "<$0'4.6","","<$1'4.3","",    "<$2'4","","<$3'3.6","",

                    "<$00'3.3","","<$10'3","",    "<$20'2.6","","<$30'2.5","",
                    "<$0'2.3","","<$1'2.2","",    "<$2'2.1","","<$3'2","",
                    "<$0'1.9","","<$1'1.8","",    "<$2'1.7","","<$3'1.6","",
                    "<$0'1.5","","<$1'1.4","",    "<$2'1.3","","<$3'1.2","",

                    "<$00'1.1","","<$10'1","",    "<$20","","<$30","",
                    "<$0","","<$1","",    "<$2","","<$3","",
                    "<$0","","<$1","",    "<$2","","<$3","",
                    "<$0","","<$1","",    "<$2","","<$3","",

                    "<$0","","<$1","",    "<$2","","<$3","",
                    "<$0","","<$1","",    "<$2","","<$3","",
                    "<$0","","<$1","",    "<$2","","<$3","",
                    "<$0","","<$1","",    "<$2","","(<$3)(<$11)","",
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
            offset = offset+4.9f;
            CreateEntity(r1);
            r1.CentreRotationOffset = rot;
        }
        void CreateRotArrowB(float rot)
        {
            Arrow r1 = MakeArrow(offset, 0, 10, 0, 0);
            CreateEntity(r1);
            r1.CentreRotationOffset = rot;
        }
        void CreateRotArrow2(float offset,float rot)
        {
            Arrow r1 = MakeArrow(offset, 0, 5, 0, 0);
            CreateEntity(r1);
            r1.CentreRotationOffset = rot;
        }

        public void Hard()
        {
            //if(InBeat(1))
            //{
            //    for(int a=0;a<60;a++)
            //    CreateRotArrow2(40, a*6);
                
            //}
            //if (GameStates.IsKeyPressed(Keys.Space))
            //{
            //    Arrow[] aa = GetAll<Arrow>();
            //    DelayBeat(0.25f, () =>
            //    {
            //        for (int a = 0; a < aa.Length; a++)
            //        {
            //            int i = a;
            //            aa[i].Delay(BeatTime(4));
            //        }
            //    });
            //}
            //if (GameStates.IsKeyPressed(Keys.B))
            //{
            //    Arrow[] aa = GetAll<Arrow>();
            //    for (int a = 0; a < aa.Length; a++)
            //    {
            //        int i = a;
            //        ValueEasing.EaseBuilder ve = new();
            //        ve.Insert(BeatTime(8), ValueEasing.EaseOutElastic(aa[i].CentreRotationOffset, 0, BeatTime(8)));
            //        ve.Run((s) => { aa[i].CentreRotationOffset = s; });
            //    }
                
            //}
            //if (GameStates.IsKeyPressed(Keys.B))
            //{
            //    Arrow[] aa = GetAll<Arrow>();
            //    for (int a = 0; a < aa.Length; a++)
            //    {
            //        int i = a;
            //        ValueEasing.EaseBuilder ve = new();
            //        ve.Insert(BeatTime(8), ValueEasing.EaseOutElastic(0, i*6, BeatTime(8)));
            //        ve.Run((s) => { aa[i].CentreRotationOffset = s; });
            //    }

            //}
        }

        public void Noob()
        {
            throw new NotImplementedException();
        }

        public void Normal()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            GametimeDelta = -0.4f;
            SetGreenBox();
            HeartAttribute.MaxHP = 50;
            HeartAttribute.DamageTaken = 1;
            TP();
            SetSoul(1);
            bool jump = false;
            if (jump)
            {
                int beat = 192;
                //   int beat = 326;
                //    int beat = 198 + 64;
                GametimeDelta = -3.5f + BeatTime(beat);

                PlayOffset = BeatTime(beat);
            }
        }
    }
}