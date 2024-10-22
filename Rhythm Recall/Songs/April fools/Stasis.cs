﻿using Extends;
using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;

namespace AprilExtends
{
    public class Stasis : WaveConstructor, IWaveSet
    {
        public class Hexagon : RectangleBox
        {
            static float r = 0;
            float d;
            public Hexagon(Player.Heart Position, float Rotation, float Duration) : base(Position)
            {
                r = Rotation;
                d = Duration;
            }

            public override void Draw()
            {
                for (int a = 0; a < 6; a++)
                    DrawingLab.DrawLine(Functions.Heart.Centre + MathUtil.GetVector2(MathF.Sqrt(42 * 42 * 2), 30 + r + a * 60), Functions.Heart.Centre + MathUtil.GetVector2(MathF.Sqrt(42 * 42 * 2), 30 + r + 60 + a * 60), 4.2f, Color.White * 0.5f, 0.99f);
            }
            int time = 0;
            public override void Update()
            {
                time++;
                if (time == d)
                {
                    this.Dispose();
                }
            }
            //六边形
        }
        public class RegularPolygon : RectangleBox
        {
            //可实时修改边数的多边形（有缝）
            static float r { get; set; } = 0;
            float d;
            float side { get; set; } = 3;
            public RegularPolygon(Player.Heart Position, float Side, float Rotation, float Duration) : base(Position)
            {
                r = Rotation;
                d = Duration;
                side = Side;
            }

            public override void Draw()
            {
                if (side.GetType() == typeof(float))
                {

                }
                for (int a = 0; a < side; a++)
                    DrawingLab.DrawLine(Functions.Heart.Centre + MathUtil.GetVector2(MathF.Sqrt(42 * 42 * 2), 180 / side + r + a * 360 / side), Functions.Heart.Centre + MathUtil.GetVector2(MathF.Sqrt(42 * 42 * 2), 180 / side + r + 360 / side + a * 360 / side), 4.2f, Color.White * 0.5f, 0.99f);
            }
            int time = 0;
            public override void Update()
            {
                time++;
                if (time == d)
                {
                    this.Dispose();
                }
            }
        }
        private void WhiteScreen(float duration, float alpha)
        {
            DrawingUtil.MaskSquare square = new(0, 0, 640, 480, duration, Color.White, alpha);
            CreateEntity(square);
            AddInstance(new TimeRangedEvent(duration + 1, () =>
            {
                if (alpha >= 0)
                {
                    alpha -= 1 / duration;
                }
                square.alpha = alpha;
                if (square.alpha <= 0)
                {
                    square.Dispose();
                }
            }));
        }

        public Stasis() : base(62.5f / (180 / 60)) { }
        public string Music => "Stasis";
#if DEBUG
        public string FightName => "Stasis";
#else
            public string FightName => "Maware setsugetsuka"; 
#endif 

        public SongInformation Attributes => new ThisInformation();

        public class ThisInformation : SongInformation
        {
            public override string SongAuthor => "Maozon";
            public override string PaintAuthor => "From Lanota";
            public override string BarrageAuthor => "Me";
            public override string AttributeAuthor => "Me";
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
        private float T(float beat) => BeatTime(beat);
        static Arrow.UnitEasing easeX, easeY;
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
                });
                RegisterFunctionOnce("ScreenE5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenE7.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(7.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenRR8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), true);
                });
                RegisterFunctionOnce("ScreenRL8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), false);
                });
                RegisterFunctionOnce("ScreenS5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenRR8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("ScreenRL8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("ScreenE5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(1));
                });
                RegisterFunctionOnce("ScreenS5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(1));
                });
                CreateChart(BeatTime(4), BeatTime(1), 5.5f, new string[]
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
                RegisterFunctionOnce("ScreenE5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenE7.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(7.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenRR8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), true);
                });
                RegisterFunctionOnce("ScreenRL8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), false);
                });
                RegisterFunctionOnce("ScreenS5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenRR8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("ScreenRL8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("ScreenE5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(1));
                });
                RegisterFunctionOnce("ScreenS5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(1));
                });
                CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "(#0.75#$1)(#0.75#$31)(ScreenE5.5)","","","",    "","","","",
                    "","","","",    "(#0.75#$2)(#0.75#$01)(ScreenE7.5)","","","",
                    "","","","",    "","","","",
                    "$1(LineEvent01)","","","",    "$3(LineEvent02)","","$1(LineEvent03)","",

                    "","","","",    "(#0.3#$1)(#0.3#$21)(ScreenRR8)","","","",
                    "","","","",    "(#0.3#$3)(#0.3#$01)(ScreenRL8)","","","",
                    "","","","",    "#0.75#$1(ScreenS5.5)","","","",
                    "","","","",    "$3","","","",

                    "(#0.75#$1)(#0.75#$01)(ScreenE5.5)","","","",    "","","","",
                    "","","","",    "(#0.75#$2)(#0.75#$31)(ScreenE7.5)","","","",
                    "","","","",    "","","","",
                    "$2(LineEvent04)","","","",    "$0(LineEvent05)","","$2(LineEvent06)","",

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
                RegisterFunctionOnce("ScreenE5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenE7.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(7.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenRR8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), true);
                });
                RegisterFunctionOnce("ScreenRL8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), false);
                });
                RegisterFunctionOnce("ScreenS5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenRR8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("ScreenRL8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("ScreenE5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(1));
                });
                RegisterFunctionOnce("ScreenS5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(1));
                });
                CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
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
                        Functions.Heart.InstantSetRotation(m);
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
                RegisterFunctionOnce("ScreenE5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenE7.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(7.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenRR8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), true);
                });
                RegisterFunctionOnce("ScreenRL8", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(1.5f), false);
                });
                RegisterFunctionOnce("ScreenS5.5", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(2));
                });
                RegisterFunctionOnce("ScreenRR8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), true);
                });
                RegisterFunctionOnce("ScreenRL8F", () =>
                {
                    ScreenDrawing.CameraEffect.Convulse(8, BeatTime(0.5f), false);
                });
                RegisterFunctionOnce("ScreenE5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5.5f, BeatTime(1));
                });
                RegisterFunctionOnce("ScreenS5.5F", () =>
                {
                    ScreenDrawing.CameraEffect.SizeShrink(5.5f, BeatTime(1));
                });
                CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
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
                CreateChart(BeatTime(4), BeatTime(1), 4, new string[]
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
                CreateChart(BeatTime(4), BeatTime(1), 4, new string[]
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
                Arrow.UnitEasing eA = new();
                AddInstance(eA);
                eA.ApplyTime = BeatTime(4);
                eA.RotationEase = LinkEase(
                    Stable(T(2), 45),
                    EaseOut(BeatTime(2), 45, 0, EaseState.Back));
                eA.TagApply("A");
                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
                    "$2(R1@A)(ScreenS)","","","",    "$2","","","",
                    "$3(R1@A)","","","",    "$3","","","",
                    "$2(ScreenRL)","","","",    "$2","","","",
                    "$3(R1@A)","","","",    "$3","","","",

                    "$2(R1@A)(ScreenS)","","","",    "$2","","","",
                    "$3","","","",    "$3(R1@A)","","","",
                    "$2(R1@A)(ScreenRR)","","","",    "$2(R1@A)","","","",
                    "$3(R1@A)","","","",    "$3","","","",

                    "($2)(R1)(ScreenS)","+01'1.5{Hold}","+01'1.5{Hold}","+01'1.5{Hold}",    "$2","","","",
                    "$3(R1@A)","","","",    "$3","","","",
                    "$2(ScreenRL)","","","",    "$2","","","",
                    "$3(R1@A)","","","",    "$3","","","",

                    "$2(R1@A)(ScreenS)","","","",    "$2","","","",
                    "$3","","","",    "$3(R1@A)","","","",
                    "R(R1@A)(ScreenSAdd)","","","",    "","","R(R1)(ScreenSAdd)","",
                    "","","","",    "R(R1@A)(ScreenSAdd)","","","",
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
                Arrow.UnitEasing eA = new();
                AddInstance(eA);
                eA.ApplyTime = BeatTime(4);
                eA.RotationEase = LinkEase(
                    Stable(T(2), -45),
                    EaseOut(BeatTime(2), -45, 0, EaseState.Back));
                eA.TagApply("A");
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

                CreateChart(BeatTime(4), BeatTime(1), 6, new string[]
                {
                    "$0(R1@A)(ScreenSBack)","","","",    "$0","","","",
                    "$1(R1@A)","","","",    "$1","","","",
                    "$0(ScreenRL)","","","",    "$0","","","",
                    "$1(R1@A)","","","",    "$1","","","",

                    "$0(R1@A)(ScreenS)","","","",    "$0","","","",
                    "$1","","","",    "$1(R1@A)","","","",
                    "$0(R1@A)(ScreenRR)","","","",    "$0(R1@A)","","","",
                    "$1(R1@A)","","","",    "$1","","","",

                    "($0)(R1)(ScreenS)","+01'1.5{Hold}","+01'1.5{Hold}","+01'1.5{Hold}",    "$0","","","",
                    "$1(R1@A)","","","",    "$1","","","",
                    "$0(ScreenRL)","","","",    "$0","","","",
                    "$1(R1@A)","","","",    "$1","","","",

                    "R","","","",    "R","","+01'1.2","",
                    "R","","","",    "R","","","",
                    "R","","+01'1.2","",   "R","","","",
                    "(R)(+01)","","","",    "","","","",
                });
            }
            if (InBeat(128))
            {
                Arrow.UnitEasing eA = new();
                AddInstance(eA);
                eA.ApplyTime = BeatTime(16);
                eA.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eA.TagApply("A");
                eA.PositionEase = LinkEase(
                    Stable(T(14), 250, 200),
                    Combine(EaseOut(T(2), -250, EaseState.Sine), EaseIn(T(2), -200, EaseState.Sine))
                    );
                eA.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eB = new();
                AddInstance(eB);
                eB.ApplyTime = BeatTime(16);
                eB.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eB.TagApply("B");
                eB.PositionEase = LinkEase(
                    Stable(T(14), -250, -200),
                    Combine(EaseOut(T(2), 250, EaseState.Sine), EaseIn(T(2), 200, EaseState.Sine))
                    );
                eB.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eC = new();
                AddInstance(eC);
                eC.ApplyTime = BeatTime(16);
                eC.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eC.TagApply("C");
                eC.PositionEase = LinkEase(
                    Stable(T(14), -250, 200),
                    Combine(EaseOut(T(2), 250, EaseState.Sine), EaseIn(T(2), -200, EaseState.Sine))
                    );
                eC.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eD = new();
                AddInstance(eD);
                eD.ApplyTime = BeatTime(16);
                eD.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eD.TagApply("D");
                eD.PositionEase = LinkEase(
                    Stable(T(14), 250, -200),
                    Combine(EaseOut(T(2), -250, EaseState.Sine), EaseIn(T(2), 200, EaseState.Sine))
                    );
                eD.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                {
                    "(^$012'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "($21'0@A)($01'0@A)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(^$212'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "($21'0@B)($01'0@B)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(^$112'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "($21'0@C)($01'0@C)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(^$312'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "($21'0@D)($01'0@D)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R1($002'1.1)","","","",
                });
            }
            if (InBeat(144))
            {
                Arrow.UnitEasing eA = new();
                AddInstance(eA);
                eA.ApplyTime = BeatTime(16);
                eA.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eA.TagApply("Aa");
                eA.PositionEase = LinkEase(
                    Stable(T(14), 250, 200),
                    Combine(EaseOut(T(2), -250, EaseState.Sine), EaseIn(T(2), -200, EaseState.Sine))
                    );
                eA.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eB = new();
                AddInstance(eB);
                eB.ApplyTime = BeatTime(16);
                eB.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eB.TagApply("Bb");
                eB.PositionEase = LinkEase(
                    Stable(T(14), -250, -200),
                    Combine(EaseOut(T(2), 250, EaseState.Sine), EaseIn(T(2), 200, EaseState.Sine))
                    );
                eB.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eC = new();
                AddInstance(eC);
                eC.ApplyTime = BeatTime(16);
                eC.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eC.TagApply("Cc");
                eC.PositionEase = LinkEase(
                    Stable(T(14), -250, 200),
                    Combine(EaseOut(T(2), 250, EaseState.Sine), EaseIn(T(2), -200, EaseState.Sine))
                    );
                eC.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eD = new();
                AddInstance(eD);
                eD.ApplyTime = BeatTime(16);
                eD.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eD.TagApply("Dd");
                eD.PositionEase = LinkEase(
                    Stable(T(14), 250, -200),
                    Combine(EaseOut(T(2), -250, EaseState.Sine), EaseIn(T(2), 200, EaseState.Sine))
                    );
                eD.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                {
                    "(R1)","","","",    "R1","","","",
                    "R0{B}","","","",    "R0{B}","","","",
                    "($00'0@Aa)($20'0@Aa)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",

                    "($202'1.1)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",
                    "($00'0@Bb)($20'0@Bb)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0($112'1.1)","","","",

                    "(R0)","","","",    "R0","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "($01'0@Cc)($21'0@Cc)(R1{A})","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",

                    "($112'1.1)(R1{A})","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "($01'0@Dd)($21'0@Dd)(R1{A})","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                });
                DelayBeat(3.5f, () =>
                {
                    Arrow[] tagB = GetAll<Arrow>("B");
                    foreach (Arrow b in tagB)
                    {
                        b.ResetColor(1);
                    }
                });
                DelayBeat(11.5f, () =>
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
                Arrow.UnitEasing eA = new();
                AddInstance(eA);
                eA.ApplyTime = BeatTime(16);
                eA.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eA.TagApply("Aa");
                eA.PositionEase = LinkEase(
                    Stable(T(14), 250, 200),
                    Combine(EaseOut(T(2), -250, EaseState.Sine), EaseIn(T(2), -200, EaseState.Sine))
                    );
                eA.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eB = new();
                AddInstance(eB);
                eB.ApplyTime = BeatTime(16);
                eB.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eB.TagApply("Bb");
                eB.PositionEase = LinkEase(
                    Stable(T(14), -250, -200),
                    Combine(EaseOut(T(2), 250, EaseState.Sine), EaseIn(T(2), 200, EaseState.Sine))
                    );
                eB.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eC = new();
                AddInstance(eC);
                eC.ApplyTime = BeatTime(16);
                eC.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eC.TagApply("Cc");
                eC.PositionEase = LinkEase(
                    Stable(T(14), -250, 200),
                    Combine(EaseOut(T(2), 250, EaseState.Sine), EaseIn(T(2), -200, EaseState.Sine))
                    );
                eC.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eD = new();
                AddInstance(eD);
                eD.ApplyTime = BeatTime(16);
                eD.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eD.TagApply("Dd");
                eD.PositionEase = LinkEase(
                    Stable(T(14), 250, -200),
                    Combine(EaseOut(T(2), -250, EaseState.Sine), EaseIn(T(2), 200, EaseState.Sine))
                    );
                eD.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                {
                    "(^$012'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "($21'0@Aa)($01'0@Aa)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(^$212'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "($21'0@Bb)($01'0@Bb)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(^$112'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "($21'0@Cc)($01'0@Cc)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",

                    "(^$312'1.1)(R0)","","","",    "R0","","","",
                    "R0","","","",    "R0","","","",
                    "($21'0@Dd)($01'0@Dd)(R0)","","","",    "R0","","","",
                    "R0","","","",    "(^$002'1.1)R1","","","",
                });
            }
            if (InBeat(176))
            {
                Arrow.UnitEasing eA = new();
                AddInstance(eA);
                eA.ApplyTime = BeatTime(16);
                eA.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eA.TagApply("Aa");
                eA.PositionEase = LinkEase(
                    Stable(T(14), 250, 200),
                    Combine(EaseOut(T(2), -250, EaseState.Sine), EaseIn(T(2), -200, EaseState.Sine))
                    );
                eA.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing eB = new();
                AddInstance(eB);
                eB.ApplyTime = BeatTime(16);
                eB.RotationEase = LinkEase(
                    Stable(T(14), -180),
                    EaseOut(BeatTime(2), -180, 0, EaseState.Back));
                eB.TagApply("Bb");
                eB.PositionEase = LinkEase(
                    Stable(T(14), -250, -200),
                    Combine(EaseOut(T(2), 250, EaseState.Sine), EaseIn(T(2), 200, EaseState.Sine))
                    );
                eB.AlphaEase = LinkEase(false,
                    Stable(T(14), 0),
                    Stable(T(2), 1)
                    );
                Arrow.UnitEasing f1 = new();
                AddInstance(f1);
                f1.ApplyTime = BeatTime(16);
                f1.TagApply("f1");
                f1.PositionEase = LinkEase(
                    Stable(T(12), -200, -300),
                    Combine(EaseIn(T(4), 200, EaseState.Sine), EaseOut(T(4), 300, EaseState.Cubic))
                    );
                f1.AlphaEase = LinkEase(false,
                    Stable(T(12), 0),
                    Stable(T(4), 1)
                    );
                Arrow.UnitEasing f2 = new();
                AddInstance(f2);
                f2.ApplyTime = BeatTime(16);
                f2.TagApply("f2");
                f2.PositionEase = LinkEase(
                    Stable(T(12), 200, 300),
                    Combine(EaseIn(T(4), -200, EaseState.Sine), EaseOut(T(4), -300, EaseState.Cubic))
                    );
                f2.AlphaEase = LinkEase(false,
                    Stable(T(12), 0),
                    Stable(T(4), 1)
                    );
                CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                {
                    "(R1)","","","",    "R1","","","",
                    "R0{B}","","","",    "R0{B}","","","",
                    "($0'0@Aa)(+2'0@Aa)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",

                    "(^$202'1.1)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0{B}","","","",
                    "($0'0@Bb)(+2'0@Bb)(R0{B})","","","",    "R0{B}","","","",
                    "R0{B}","","","",    "R0(^$112'1.1)","","","",

                    "","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",

                    "","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "R1{A}","","","",
                    "(R1{A})","","","",    "R1{A}","","","",
                    "R1{A}","","","",    "","","","",

                    "($00'0@f1)($21'0@f2)","","","",    "","","","",
                });
                DelayBeat(3.5f, () =>
                {
                    Arrow[] tagB = GetAll<Arrow>("B");
                    foreach (Arrow b in tagB)
                    {
                        b.ResetColor(1);
                    }
                });
                DelayBeat(11.5f, () =>
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
                    RunEase((s) =>
                    {
                        BoxStates.Centre = new Vector2(320 + s, 240);
                        Functions.Heart.Centre = BoxStates.Centre;
                    },
                        Scale(
                        LinkEase(
                        EaseIn(BeatTime(12f), 700, EaseState.Quart),
                        EaseOut(BeatTime(4f), -700, EaseState.Expo)),
                        Alternate(2f, Stable(1f, 1f), Stable(1, -1)))

                    );
                });
                RegisterFunctionOnce("ScreenEffect", () =>
                {
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenAngle = s;
                    },
                        EaseOut(BeatTime(12), -720, EaseState.Expo)
                    );
                    RunEase((b) =>
                    {
                        ScreenDrawing.ScreenPositionDelta = new(0 + b, 0);
                    },
                        Scale(
                        LinkEase(
                        EaseIn(BeatTime(12f), 200, EaseState.Quart),
                        EaseOut(BeatTime(4f), -200, EaseState.Expo))
                        ,
                        Alternate(2f, Stable(1f, 1f), Stable(1, -1)))

                    );
                });
                RegisterFunctionOnce("ScreenScale", () =>
                {
                    RunEase((nb) =>
                    {
                        ScreenDrawing.ScreenScale = 1 + nb;
                    },
                        EaseIn(BeatTime(1), 1, EaseState.Circ),
                        EaseOut(BeatTime(12), -1, EaseState.Expo)
                    );
                });
                CreateChart(BeatTime(0), BeatTime(1), 0, new string[]
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
                    RunEase((e) =>
                    {
                        foreach (Arrow b in tagA)
                        {
                            b.Offset = new(0, 0 + o);
                        }
                        o = e;
                    },
                        Stable(BeatTime(2), -400),
                        EaseOut(BeatTime(4), 400, EaseState.Elastic)
                    );
                    Arrow[] tagB = GetAll<Arrow>("B");
                    foreach (Arrow a in tagB)
                    {
                        a.Offset = new(0, 400);
                    }
                    float c = 0;
                    RunEase((w) =>
                    {
                        foreach (Arrow a in tagB)
                        {
                            a.Offset = new(0, 0 + c);
                        }
                        c = w;
                    },
                        Stable(BeatTime(2), 400),
                        EaseOut(BeatTime(4), -400, EaseState.Elastic)
                    );
                });
                RegisterFunctionOnce("ScreenEffect", () =>
                {
                    RunEase((s) =>
                    {
                        ScreenDrawing.ScreenScale = 1 + s;
                    },
                        EaseIn(BeatTime(8), 0.8f, EaseState.Expo),
                        EaseOut(BeatTime(4), -0.9f, EaseState.Elastic),
                        EaseInOut(BeatTime(4), 0, 0.1f, EaseState.Cubic)
                    );
                    RunEase((r) =>
                    {
                        ScreenDrawing.ScreenAngle = 0 + r;
                    },
                        EaseInOut(BeatTime(24), 0, 720, EaseState.Expo)
                    );
                });
                easeX.TagApply("X");
                CreateChart(BeatTime(2), BeatTime(1), 7, new string[]
                {
                    "ArrowEffect","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",

                    "$0{A}","","$2{A}","",    "$0{B}","","$2{B}","",
                    "$0{A}","","$2{A}","",    "$0{B}","","$2{B}","",
                    "$0{A}","","$2{A}","",    "$0{B}","","$2{B}","",
                    "$0{A}","","$2{A}","",    "$0{B}","","$2{B}","",

                    "$0","","$2","",    "$0","","$2","",
                    "$0","","$2","",    "$0","","$2","",
                    "$0","","$2","",    "$0","","$2","",
                    "$0","","$2","",    "$0","","$2","",

                    "($0)(ScreenEffect)","","+1","",    "+1","","+1","",
                    "+1","","+1","",    "+1","","+1","",
                    "$0","","+1","",    "+1","","+1","",
                    "+1","","+1","",    "+1","","+1","",

                    "$0","","+1","",    "+1","","+1","",
                    "+1","","+1","",    "+1","","+1","",
                    "$0","","+1","",    "+1","","+1","",
                    "+1","","+1","",    "+1","","+1","",
                    //
                    "$0@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",

                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",

                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",

                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "+1@X","","+1@X","",    "+1@X","","+1@X","",
                    "(<$3)(<$11)","","","",    "","","","",
                });
            }
            if (InBeat(243))
            {

                RegisterFunctionOnce("Box", () =>
                {
                    SetBox(new Vector2(320, 240), 250, 250);
                    RunEase(r =>
                        CreateEntity(new DrawingUtil.SpecialBox(1, r, Functions.Heart) { alpha = 0.5f }),
                        EaseIn(BeatTime(4), 0, 720, EaseState.Cubic),
                        EaseOut(BeatTime(2), 0, 10, EaseState.Elastic),
                        EaseOut(BeatTime(2), 0, -20, EaseState.Back),
                        EaseOut(BeatTime(16), 0, 730, EaseState.Cubic),
                        Stable(BeatTime(1)),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(1), 8, 10, EaseState.Elastic),
                        EaseInOut(BeatTime(3), 0, 240, EaseState.Cubic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseIn(BeatTime(0.5f), 0, 8, EaseState.Cubic),
                        EaseOut(BeatTime(0.5f), 8, 10, EaseState.Elastic),
                        EaseOut(BeatTime(3), 0, 120, EaseState.Expo),
                        EaseIn(BeatTime(9), 0, 1800, EaseState.Cubic)
                    );

                    Functions.Heart.controlingBox.GreenSoulAlpha = 0f;
                });
                CreateChart(BeatTime(1), BeatTime(1), 7, new string[]
                {
                    "($0)($21)(<$21)","","$0","",    "($0)($21)(>$21)","","$0","",
                    "($0)($21)(<$21)","","$0","",    "($0)($21)(>$21)","","$0","",
                    "($1)($31)(<$31)","","$1","",    "($1)($31)(>$31)","","$1","",
                    "($1)($31)(<$31)","","$1","",    "($1)($31)(>$31)","","$1","",

                    "($2)($01)(<$01)","","$2","",    "($2)($01)(>$01)","","$2","",
                    "($2)($01)(<$01)","","$2","",    "($2)($01)(>$01)","","$2","",
                    "($3)($11)(<$11)","","$3","",    "($3)($11)(>$11)","","$3","",
                    "($3)($11)(<$11)","","$3","",    "($3)($11)(>$11)","","$3","",

                    "($0)($21)(Box)","","($2)($01)","",    "($0)($21)","","($2)($01)","",
                    "($0)($21)","","($2)($01)","",    "($0)($21)","","($2)($01)","",
                    "($0)($21)","","($0)($21)","",    "($0)($21)","","($0)($21)","",
                    "($0)($21)","","($0)($21)","",    "($0)($21)","","($0)($21)","",

                    "($0)($1)($21)($31)","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                });
            }
            if (InBeat(256))
            {
                RegisterFunctionOnce("Pos", () =>
                {
                    WhiteScreen(BeatTime(8), 0.8f);
                    RunEase(p =>
                    {
                        InstantTP(p);
                    },
                    EaseOut(BeatTime(8), new Vector2(320, 240), new Vector2(320, 140), EaseState.Quint)
                    );
                    DelayBeat(16, () =>
                    {
                        RunEase(p =>
                        {
                            InstantTP(p);
                        },
                    EaseOut(BeatTime(4), new Vector2(320, 140), new Vector2(320, 240), EaseState.Expo)
                    );
                    });
                    DelayBeat(28, () =>
                    {
                        RunEase(p =>
                        {
                            ScreenDrawing.ScreenPositionDelta = new(0 - 0.5f * p, 0);
                            InstantTP(320 + p, 240);
                        },
                        Scale(
                            EaseOut(BeatTime(4), 40, 0, EaseState.Cubic),
                            Alternate(2, Stable(1, 1), Stable(1, -1)))
                    );
                    });
                });
                CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "(#0.5#$1)(Pos)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($01)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($21)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($01)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",

                    "($1{Hold})($21)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($01)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($21)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($01)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",

                    "($1{Hold})($21)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($01)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($21)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($01)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",

                    "($1{Hold})($21)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($01)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($21)","","$1{Hold}","",    "$1{Hold}","","$1{Hold}","",
                    "($1{Hold})($01)","","$1{Hold}","",    "($1{Hold})($21)","","$1{Hold}","",

                    "($01)($21)","","","",    "","","","",
                });
            }
            if (InBeat(272 + 1))
            {
                CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "$3","","+1","",    "+1","","+1","",
                    "$31","","-11","",    "-11","","-11","",
                    "$3","","+1","",    "+1","","+1","",
                    "$31","","-11","",    "-11","","-11","",

                    "$3","","+1","",    "+1","","+1","",
                    "$31","","-11","",    "-11","","-11","",
                    "$0","","+1","",    "+1","","+0{Hold}","",
                    "$21","","-11","",    "-11","","-0{Hold}","",

                    "$0","","+1","",    "+1","","+0{Hold}","",
                    "$21","","-11","",    "-11","","-0{Hold}","",
                    "$0","","+1","",    "+1","","+0{Hold}","",
                    "($0)($21)","","","",    "","","","",
                });
            }
            if (InBeat(272 + 16))
            {
                CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "(^$0'2)(^$21'2)","","","",    "","","","",
                    "$0","","+1","",    "+1","","-1","",
                    "-1","","+1","",    "+1","","-1","",
                    "-1","","+1","",    "+1","","-1","",

                    "-1","","+1","",    "+1","","-1","",
                    "-1","","+1","",    "+1","","-1","",
                    "-1","","+1","",    "+1","","-1","",
                    "-1","","+1","",    "+1","","-1","",

                    "($1)($01)","","$1","",    "($1)($21)","","$1","",
                    "($1)($01)","","$1","",    "($1)($21)","","$1","",
                    "($1)($01)","","$1","",    "($1)($21)","","$1","",
                    "($1)($01)","","$1","",    "($1)($21)","","$1","",

                    "($1)($01)","","$1","",    "($1)($21)","","$1","",
                    "($1)($01)","","$1","",    "($1)($21)","","$1","",
                    "($1)($01)","","$1","",    "($1)($21)","","$1","",
                    "($1)($01)","","$1","",    "($1)($21)","","$1","",
                });
            }
            if (InBeat(272 + 32))
            {
                CreateChart(BeatTime(4), BeatTime(1), 7, new string[]
                {
                    "($0)($2)","","","",    "","","","",
                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",
                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",
                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",

                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",
                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",
                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",
                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",

                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",
                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",
                    "($0)($21)","","($01)($2)","",    "($0)($21)","","($01)($2)","",
                    "","","","",    "","","","",
                });
            }
            if (InBeat(272 + 32 + 16))
            {
                ScreenDrawing.UISettings.CreateUISurface();
                InstantSetGreenBox();
                Functions.Heart.controlingBox.GreenSoulAlpha = 0.5f;
                BoxUtils.Vertexify();
                var box = BoxUtils.VertexBoxInstance;

                box.SetPosition(0, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90));
                box.SetPosition(1, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72));
                box.SetPosition(2, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72 * 2));
                box.SetPosition(3, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72 * 3));
                box.SetPosition(box.Split(3, 1), new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72 * 4));
                Delay(T(1.5f), () =>
                {
                    box.SetPosition(0, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 60 * 2));
                    box.SetPosition(1, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 60 * 3));
                    box.SetPosition(2, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 60 * 4));
                    box.SetPosition(3, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 60 * 5));
                    box.SetPosition(4, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 60 * 6));
                    box.SetPosition(box.Split(4, 1), new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 60 * 7));
                });
                Delay(T(3f), () =>
                {
                    box.SetPosition(0, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90));
                    box.SetPosition(1, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72));
                    box.SetPosition(2, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72 * 2));
                    box.SetPosition(3, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72 * 3));
                    box.SetPosition(4, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72 * 4));
                    box.SetPosition(5, new Vector2(320, 240) + MathUtil.GetVector2(MathF.Sqrt(2) * 42, -90 + 72 * 4));
                });
                Delay(T(4f), () =>
                {
                    box.SetPosition(0, new Vector2(320 + 42, 240 - 42));
                    box.SetPosition(1, new Vector2(320 + 42, 240 + 42));
                    box.SetPosition(2, new Vector2(320 - 42, 240 + 42));
                    box.SetPosition(3, new Vector2(320 - 42, 240 - 42));
                    box.SetPosition(4, new Vector2(320 - 42, 240 - 42));
                    box.SetPosition(5, new Vector2(320 - 42, 240 - 42));
                    Delay(T(1f), () => BoxUtils.DeVertexify(new(320 - 42, 240 - 42, 84, 84)));
                });
                RegisterFunctionOnce("PushLeft", () =>
                {
                    var eas = LinkEase(Stable(0, 0, 0), EaseOut(T(2.5f), new Vector2(50, 0), EaseState.Quad), EaseIn(T(4f), new Vector2(-15, 0), EaseState.Sine));
                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, eas);
                    var rot = LinkEase(
                        Stable(0, 0),
                        EaseOut(T(0.5f), -10, EaseState.Quad),
                        EaseIn(T(0.5f), 10, EaseState.Quad),
                        EaseOut(T(0.5f), 10, EaseState.Quad),
                        EaseIn(T(0.5f), -15, EaseState.Quad),
                        EaseOut(T(0.5f), 5, EaseState.Quad));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, rot);
                });
                RegisterFunctionOnce("PushRight", () =>
                {
                    var eas = LinkEase(Stable(0, 0, 0), EaseOut(T(2.5f), new Vector2(-50, 0), EaseState.Quad), EaseIn(T(4f), new Vector2(15, 0), EaseState.Sine));
                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, eas);
                    var rot = LinkEase(
                        Stable(0, 0),
                        EaseOut(T(0.5f), 10, EaseState.Quad),
                        EaseIn(T(0.5f), -10, EaseState.Quad),
                        EaseOut(T(0.5f), -10, EaseState.Quad),
                        EaseIn(T(0.5f), 15, EaseState.Quad),
                        EaseOut(T(0.5f), -5, EaseState.Quad));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, rot);
                });
                RegisterFunctionOnce("InstantBack", () =>
                {
                    var eas = LinkEase(false,
                        Stable(2, -15, 0),
                        Stable(2, 15, 0),
                        Stable(2, -14, 0),
                        Stable(2, 14, 0),
                        Stable(2, -12, 0),
                        Stable(2, 12, 0),
                        Stable(2, -9, 0),
                        Stable(2, 9, 0),
                        Stable(2, -6, 0),
                        Stable(2, 6, 0),
                        Stable(2, -4, 0),
                        Stable(2, 4, 0),
                        Stable(2, -3, 0),
                        Stable(2, 3, 0),
                        Stable(2, 0, 0)
                        );
                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, eas);
                });
                CreateChart(0, BeatTime(1), 7f, new string[]
                {
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "$2","","$3","",    "$0","","$1","",
                    //
                    "(#1#$2)(D1)(PushLeft)","","","",    "(D1)","","","",
                    "($2)(D1)","","","",    "(D1)","","+2","",
                    "+31","","+2","",    "(D)(D1)","","","",
                    "(D1)","","+2","",    "(+11)","","+2","",

                    "(D)(D1)","","","",    "D1","","","",
                    "(D)(D1)","","","",    "(D1)","","+2","",
                    "+31","","+2","",    "(D)(D1)","","","",
                    "(D1)","","+2","",    "(+11)","","+2","",

                    "(#0.4#+11)(#0.4#+2)(InstantBack)","","","",    "","","","",
                    "($1)(D1)","","","",    "(D1)","","+2","",
                    "+31","","+2","",    "(D)(D1)","","","",
                    "(D1)","","+2","",    "(+11)","","+2","",

                    "(D)(D1)","","","",    "D1","","","",
                    "(D)(D1)","","","",    "(D1)","","+2","",
                    "+31","","+2","",    "(D)(D1)","","","",
                    "(D1)","","+2","",    "(+11)","","+2","",
                    //
                    "(#0.4#$01)(PushRight)","","","",    "D","","","",
                    "(R)(D1)","","","",    "(D1)","","+2","",
                    "+31","","+2","",    "(D)(D1)","","","",
                    "(D1)","","+2","",    "(+11)","","+2","",

                    "(D)(D1)","","","",    "D1","","","",
                    "(D)(D1)","","","",    "(D1)","","+2","",
                    "+31","","+2","",    "(D)(D1)","","","",
                    "(D1)","","+2","",    "(+11)","","+2","",

                    "(#0.4#+11)(#0.4#+2)(InstantBack)","","","",    "D","","","",
                    "(D)(D1)","","","",    "(D1)","","+2","",
                    "+21","","+2","",    "(D)(D1)","","","",
                    "(D1)","","+2","",    "(+21)","","+2","",

                    "$0","","$21","",    "$11","","$01","",
                    "$1","","$21","",    "$11","","$01","",
                    "$2","","$21","",    "$11","","$01","",
                    "$3","","$21","",    "$11","","$01","",
                    //
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
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                });
            }
            if (InBeat(272 + 32 + 32 + 16))
            {
                RegisterFunctionOnce("PushLeft", () =>
                {
                    var eas = LinkEase(Stable(0, 0, 0), EaseOut(T(2.5f), new Vector2(40, 0), EaseState.Quad), EaseIn(T(4f), new Vector2(-15, 0), EaseState.Sine));
                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, eas);
                    var rot = LinkEase(
                        Stable(0, 0),
                        EaseOut(T(0.5f), -5, EaseState.Quad),
                        EaseIn(T(0.5f), 5, EaseState.Quad),
                        EaseOut(T(0.5f), 5, EaseState.Quad),
                        EaseIn(T(0.5f), -8, EaseState.Quad),
                        EaseOut(T(0.5f), 3, EaseState.Quad));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, rot);
                });
                RegisterFunctionOnce("PushRight", () =>
                {
                    var eas = LinkEase(Stable(0, 0, 0), EaseOut(T(2.5f), new Vector2(-40, 0), EaseState.Quad), EaseIn(T(4f), new Vector2(15, 0), EaseState.Sine));
                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, eas);
                    var rot = LinkEase(
                        Stable(0, 0),
                        EaseOut(T(0.5f), 5, EaseState.Quad),
                        EaseIn(T(0.5f), -5, EaseState.Quad),
                        EaseOut(T(0.5f), -5, EaseState.Quad),
                        EaseIn(T(0.5f), 8, EaseState.Quad),
                        EaseOut(T(0.5f), -3, EaseState.Quad));
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, rot);
                });
                RegisterFunctionOnce("InstantBack", () =>
                {
                    var eas = LinkEase(false,
                        Stable(2, -15, 0),
                        Stable(2, 15, 0),
                        Stable(2, -14, 0),
                        Stable(2, 14, 0),
                        Stable(2, -12, 0),
                        Stable(2, 12, 0),
                        Stable(2, -9, 0),
                        Stable(2, 9, 0),
                        Stable(2, -6, 0),
                        Stable(2, 6, 0),
                        Stable(2, -4, 0),
                        Stable(2, 4, 0),
                        Stable(2, -3, 0),
                        Stable(2, 3, 0),
                        Stable(2, 0, 0)
                        );
                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, eas);
                });
                RegisterFunctionOnce("InstantBack2", () =>
                {
                    var eas = LinkEase(false,
                        Stable(2, -15, 0),
                        Stable(2, 15, 0),
                        Stable(2, -14, 0),
                        Stable(2, 14, 0),
                        Stable(2, -12, 0),
                        Stable(2, 12, 0),
                        Stable(2, -9, 0),
                        Stable(2, 9, 0),
                        Stable(2, -10, 0),
                        Stable(2, 10, 0),
                        Stable(2, -11, 0),
                        Stable(2, 11, 0),
                        Stable(2, -10, 0),
                        Stable(2, 10, 0),
                        Stable(2, -9, 0),
                        Stable(2, 9, 0),
                        Stable(2, -6, 0),
                        Stable(2, 6, 0),
                        Stable(2, -4, 0),
                        Stable(2, 4, 0),
                        Stable(2, -3, 0),
                        Stable(2, 3, 0),
                        Stable(2, 0, 0)
                        );
                    RunEase((s) => { ScreenDrawing.ScreenPositionDelta = s; }, eas);
                });

                CreateChart(T(4), BeatTime(1), 7f, new string[]
                {
                    "($01)($2)(PushLeft)","","","",    "(D1)","","","",
                    "(D)(D1)","","","",    "($21)($0)","","$11","",
                    "$1","","$11","",    "($01)($2)","","","",
                    "($21)($0)","","$11","",    "$1","","$11","",

                    "($01)($2)","","","",    "D1","","","",
                    "(D)(D1)","","","",    "($21)($0)","","$11","",
                    "$1","","$11","",    "($01)($2)","","","",
                    "($21)($0)","","$11","",    "$1","","$11","",

                    "($01)($2)(InstantBack)","","","",    "D","","","",
                    "(D)(D1)","","","",    "($21)($0)","","$11","",
                    "$1","","$11","",    "($01)($2)","","","",
                    "($21)($0)","","$11","",    "$1","","$11","",

                    "($21)($0)","","","",    "D","","","",
                    "(D)(D1)","","","",    "($21)($0)","","($11)","",
                    "(^$0'1.2)(^$2'1.2)","","","",    "","","(^$0'1.2)(^$2'1.2)","",
                    "","","","",    "(^$0'1.2)(^$2'1.2)","","","",

                    "($21)($0)(PushRight)","","","",    "(D1)","","","",
                    "(D)(D1)","","","",    "($01)($2)","","$11","",
                    "$1","","$11","",    "($21)($0)","","","",
                    "($01)($2)","","$11","",    "$1","","$11","",

                    "($21)($0)","","","",    "D1","","","",
                    "(D)(D1)","","","",    "($01)($2)","","$11","",
                    "$1","","$11","",    "($21)($0)","","","",
                    "($01)($2)","","$11","",    "$1","","$11","",

                    "(#0.5#$21)(#0.5#$0)(InstantBack2)","($21'3)($0'3)","($21'3)($0'3)","($21'3)($0'3)",    "(#5.5#$21)(#5.5#$0)","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",

                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                    "","","","",    "","","","",
                });
            }
            if (InBeat(272 + 32 + 32 + 32 + 16))
            {
                RegisterFunctionOnce("RotIn", () =>
                {
                    var eas = LinkEase(Stable(0, 0),
                        EaseIn(T(2f), 2, EaseState.Sine),
                        EaseOut(T(2f), 2, EaseState.Sine),
                        //
                        EaseIn(T(2f), -1.5f, EaseState.Sine),
                        EaseOut(T(2f), -1.5f, EaseState.Sine),
                        EaseIn(T(2f), 1.5f, EaseState.Sine),
                        EaseOut(T(2f), 1.5f, EaseState.Sine),

                        EaseIn(T(2f), -1.5f, EaseState.Sine),
                        EaseOut(T(2f), -1.5f, EaseState.Sine),
                        EaseIn(T(2f), 1.5f, EaseState.Sine),
                        EaseOut(T(2f), 1.5f, EaseState.Sine),

                        EaseIn(T(2f), -1.5f, EaseState.Sine),
                        EaseOut(T(2f), -1.5f, EaseState.Sine),
                        EaseIn(T(2f), 1.5f, EaseState.Sine),
                        EaseOut(T(2f), 1.5f, EaseState.Sine),

                        EaseIn(T(2f), -1.5f, EaseState.Sine),
                        EaseOut(T(2f), -1.5f, EaseState.Sine),
                        EaseIn(T(2f), 1.5f, EaseState.Sine),
                        EaseOut(T(2f), 1.5f, EaseState.Sine),
                        //
                        EaseIn(T(2f), -4, EaseState.Sine),
                        EaseOut(T(2f), -4, EaseState.Sine),

                        EaseIn(T(2f), 1.5f, EaseState.Sine),
                        EaseOut(T(2f), 1.5f, EaseState.Sine),
                        EaseIn(T(2f), -1.5f, EaseState.Sine),
                        EaseOut(T(2f), -1.5f, EaseState.Sine),

                        EaseIn(T(2f), 1.5f, EaseState.Sine),
                        EaseOut(T(2f), 1.5f, EaseState.Sine),
                        EaseIn(T(2f), -1.5f, EaseState.Sine),
                        EaseOut(T(2f), -1.5f, EaseState.Sine),

                        EaseIn(T(2f), 1.5f, EaseState.Sine),
                        EaseOut(T(2f), 1.5f, EaseState.Sine),
                        EaseIn(T(2f), -1.5f, EaseState.Sine),
                        EaseOut(T(2f), -1.5f, EaseState.Sine),
                        //
                        EaseIn(T(2f), 2f, EaseState.Sine),
                        EaseOut(T(2f), 2f, EaseState.Sine)
                        );
                    RunEase((s) => { ScreenDrawing.ScreenAngle = s; }, eas);
                });
                RegisterFunctionOnce("Box", () =>
                {
                    var eas = LinkEase(Stable(0, 320, 240),
                        EaseIn(T(2),new Vector2(0,11),EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 11), EaseState.Sine),
                        //
                        EaseIn(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseIn(T(2), new Vector2(0, 22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 22), EaseState.Sine),

                        EaseIn(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseIn(T(2), new Vector2(0, 22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 22), EaseState.Sine),

                        EaseIn(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseIn(T(2), new Vector2(0, 22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 22), EaseState.Sine),

                        EaseIn(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseIn(T(2), new Vector2(0, 22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 22), EaseState.Sine),
                        //
                        EaseIn(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseIn(T(2), new Vector2(0, 22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 22), EaseState.Sine),

                        EaseIn(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseIn(T(2), new Vector2(0, 22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 22), EaseState.Sine),

                        EaseIn(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseIn(T(2), new Vector2(0, 22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 22), EaseState.Sine),

                        EaseIn(T(2), new Vector2(0, -22), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, -22), EaseState.Sine),

                        EaseIn(T(2), new Vector2(0, 11), EaseState.Sine),
                        EaseOut(T(2), new Vector2(0, 11), EaseState.Sine)
                        );
                    RunEase((s) => { InstantSetBox(s, 84, 84); InstantTP(s); }, eas);
                });
                RegisterFunctionOnce("ScaleOut", () =>
                {
                    var eas = LinkEase(Stable(0, 1), EaseOut(T(8), 0.2f, EaseState.Sine));
                    RunEase((s) => { ScreenDrawing.ScreenScale = s; }, eas);
                });
                CreateChart(0, BeatTime(1), 7f, new string[]
                {
                    "RotIn(Box)","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",
                    "","","","",   "","","","",

                    "R","","","",    "R","","","",
                    "R","","","",    "R","","+0","",
                    "+0","","+0","",    "+0","","","",
                    "R","","+0","",    "+0","","+0","",

                    "+0","","","",    "R","","","",
                    "R","","","",    "R","","+0","",
                    "+0","","+0","",    "+0","","","",
                    "R","","+0","",    "+0","","+0","",

                    "+0","","","",    "R","","","",
                    "R","","","",    "R","","+0","",
                    "+2","","+2","",    "+0","","","",
                    "R","","+0","",    "+2","","+2","",

                    "+0","","","",    "R","","+0","",
                    "+2","","+2","",    "+0","","","",
                    "R","","+0","",    "+2","","+2","",
                    "+0","","","",    "","","","",
                    //
                    "R1","","","",    "R1","","","",
                    "R1","","","",    "R1","","+01","",
                    "+01","","+01","",    "+01","","","",
                    "R1","","+01","",    "+01","","+01","",

                    "+01","","","",    "R1","","","",
                    "R1","","","",    "R1","","+01","",
                    "+01","","+01","",    "+01","","","",
                    "R1","","+01","",    "+01","","+01","",

                    "+01","","","",    "R1","","","",
                    "R1","","","",    "R1","","+01","",
                    "+21","","+21","",    "+01","","","",
                    "R1","","+21","",    "+21","","","",

                    "R1","","","",    "R1","","+01","",
                    "+21","","+21","",    "+01","","","",
                    "R1","","+01","",    "+21","","+21","",
                    "+01","","","",    "","","","",
                    //
                    "R","","","",    "R1","","","",
                    "R1","","","",    "R","","+01","",
                    "+2","","+01","",    "+2","","","",
                    "R","","+01","",    "+2","","+01","",

                    "+2","","","",    "R1","","","",
                    "R1","","","",    "R","","+01","",
                    "+2","","+01","",    "+2","","","",
                    "R","","+01","",    "+2","","+01","",

                    "+0","","","",    "R1","","","",
                    "R1","","","",    "R","","+01","",
                    "+2","","+01","",    "+2","","","",
                    "R","","+01","",    "+2","","+01","",

                    "+2","","","",    "R1","","","",
                    "R1","","","",    "R","","+01","",
                    "+2","","+01","",    "+2","","","",
                    "R","","+01","",    "+2","","+01","",
                    //
                    "+2","","","",    "R1","","","",
                    "R1","","","",    "R","","+21","",
                    "+0","","+21","",    "+0","","","",
                    "R","","+21","",    "+0","","+21","",

                    "+0","","","",    "R1","","","",
                    "R1","","","",    "R","","+21","",
                    "+0","","+21","",    "+0","","","",
                    "R","","+21","",    "+0","","+21","",

                    "+0","","","",    "R1","","","",
                    "R1","","","",    "R","","+21","",
                    "+0","","+21","",    "+0","","","",
                    "R","","+21","",    "+0","","+21","",

                    "+0(ScaleOut)","","","",    "R","","","",
                    "R","","","",    "R","","","",
                    "","","","",    "R","","","",
                    "R","","","",    "R","","","",

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
            offset = offset + 4.9f;
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

        public void Hard() { }
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
            easeX = new();
            AddInstance(easeX = new Arrow.UnitEasing()
            {
                ApplyTime = BeatTime(8),
                RotationEase = LinkEase(EaseIn(BeatTime(4), 0, 90, EaseState.Quart), EaseOut(BeatTime(8), -180, 0, EaseState.Linear))
            });
            GametimeDelta = -1.4f;
            ScreenDrawing.HPBar.HPLoseColor = Color.Black;
            //HeartAttribute.BuffedLevel = 1;
            SetGreenBox();
            HeartAttribute.MaxHP = 5;
            HeartAttribute.DamageTaken = 1;
            TP();
            SetSoul(1);
            bool jump = false;
            if (jump)
            {
                int beat = 272 + 32 +32+32+ 16;
                //   int beat = 326;
                //    int beat = 198 + 64;
                GametimeDelta = -5.5f + BeatTime(beat);

                PlayOffset = BeatTime(beat);
            }
        }
    }
}