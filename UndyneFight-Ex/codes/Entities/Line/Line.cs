using Microsoft.Xna.Framework;
using System;
using static UndyneFight_Ex.DrawingLab;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    public partial class Line : Entity
    {
        private class LinePoint : GameObject, ICustomMotion
        {
            public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
            public Func<ICustomMotion, float> RotationRoute { get; set; }
            public float[] RotationRouteParam { get; set; }
            public float[] PositionRouteParam { get; set; }

            public float AppearTime { get; private set; }

            public Vector2 CentrePosition { get; private set; }
            public float Rotation => 0;

            public override void Update()
            {
                AppearTime += 0.5f;
                CentrePosition = PositionRoute.Invoke(this);
            }
            public LinePoint(Func<ICustomMotion, Vector2> positionRoute)
            {
                PositionRoute = positionRoute;
                UpdateIn120 = true;
            }
        }

        LinePoint vec1, vec2;
        public float Alpha { get; set; } = 1.0f;
        public float Width { private get; set; } = 3.0f;
        public Color DrawingColor { get; set; } = Color.White;
        /// <summary>
        /// 竖直的镜像开关
        /// </summary>
        public bool VerticalMirror { private get; set; } = false;
        /// <summary>
        /// 横直的镜像开关
        /// </summary>
        public bool TransverseMirror { private get; set; } = false;
        /// <summary>
        /// 竖直+横直的镜像开关
        /// </summary>
        public bool ObliqueMirror { private get; set; } = false;
        /// <summary>
        /// 垂线的开关（90°线）
        /// </summary>
        public bool VerticalLine { private get; set; } = false;
        public Line(Vector2 vec1, Vector2 vec2) : this(EasingUtil.CentreEasing.Stable(vec1), EasingUtil.CentreEasing.Stable(vec2))
        {
        }
        public Line(Vector2 centre, float rotation) : this(centre, EasingUtil.ValueEasing.Stable(rotation))
        {
        }
        public Line(float Xcentre, float rotation) : this(EasingUtil.CentreEasing.Stable(Xcentre, 240), EasingUtil.ValueEasing.Stable(rotation))
        {
        }
        public Line(Func<ICustomMotion, Vector2> easing1, Func<ICustomMotion, Vector2> easing2)
        {
            UpdateIn120 = true;
            vec1 = new(easing1);
            vec2 = new(easing2);
            AddChild(vec1);
            AddChild(vec2);
        }
        public Line(Vector2 centre, Func<ICustomMotion, float> rotationEasing) : this(EasingUtil.CentreEasing.Stable(centre), rotationEasing)
        {
        }
        public Line(Func<ICustomMotion, Vector2> centreEasing, Func<ICustomMotion, float> rotationEasing)
        {
            UpdateIn120 = true;

            bool xCalc = false;
            float rotation = 0;
            Vector2 centre = new();
            Func<ICustomMotion, Vector2> easing1 = (s) =>
            {
                rotation = rotationEasing.Invoke(s);
                centre = centreEasing.Invoke(s);
                float jr = rotation;
                if (jr < 0) jr += 3600;
                jr %= 180f;
                Vector2 result;
                xCalc = jr < 45 || jr > 135f;
                if (xCalc)
                {
                    float dist = centre.X + 640;
                    result = new(-640, centre.Y - Tan(rotation) * dist);
                }
                else
                {
                    float dist = centre.Y + 480;
                    result = new(centre.X - dist / Tan(rotation), -480);
                }
                return result;
            };
            Func<ICustomMotion, Vector2> easing2 = (s) =>
            {
                Vector2 result;
                if (xCalc)
                {
                    float dist = 1280 - centre.X;
                    result = new(1280, centre.Y + Tan(rotation) * dist);
                }
                else
                {
                    float dist = 960 - centre.Y;
                    result = new(centre.X + dist / Tan(rotation), 960);
                }
                return result;
            };
            vec1 = new(easing1);
            vec2 = new(easing2);
            AddChild(vec1);
            AddChild(vec2);
        }
        public Line(Func<ICustomMotion, Vector2> centreEasing, Func<ICustomMotion, float> rotationEasing, Func<ICustomMotion, float> lengthEasing)
        {
            UpdateIn120 = true;
            float rotation = 0;

            LinePoint centre = new(centreEasing);
            AddChild(centre);

            Func<ICustomMotion, Vector2> easing1 = (s) =>
            {
                rotation = rotationEasing.Invoke(s);
                return centre.CentrePosition + MathUtil.GetVector2(lengthEasing.Invoke(s) / 2, rotation);
            };
            Func<ICustomMotion, Vector2> easing2 = (s) =>
            {
                return centre.CentrePosition - MathUtil.GetVector2(lengthEasing.Invoke(s) / 2, rotation);
            };
            vec1 = new(easing1);
            vec2 = new(easing2);
            AddChild(vec1);
            AddChild(vec2);
        }

        public override void Draw()
        {
            if (Alpha < 0) return;
            DrawLine(vec1.CentrePosition, vec2.CentrePosition, Width, DrawingColor * Alpha, Depth, Image);
            if (VerticalMirror)
                DrawLine(new Vector2(vec1.CentrePosition.X, 480 - vec1.CentrePosition.Y), new Vector2(vec2.CentrePosition.X, 480 - vec2.CentrePosition.Y), Width, DrawingColor * Alpha, Depth, Image);
            if (TransverseMirror)
                DrawLine(new Vector2(640 - vec1.CentrePosition.X, vec1.CentrePosition.Y), new Vector2(640 - vec2.CentrePosition.X, vec2.CentrePosition.Y), Width, DrawingColor * Alpha, Depth, Image);
            if (ObliqueMirror)
                DrawLine(new Vector2(640 - vec1.CentrePosition.X, 480 - vec1.CentrePosition.Y), new Vector2(640 - vec2.CentrePosition.X, 480 - vec2.CentrePosition.Y), Width, DrawingColor * Alpha, Depth, Image);
            if (VerticalLine)
            {
                DrawLine(new Vector2(-vec1.CentrePosition.Y - 80, vec1.CentrePosition.X - 80), new Vector2(-vec2.CentrePosition.Y - 80, vec2.CentrePosition.X - 80), Width, DrawingColor * Alpha, Depth, Image);
                if (TransverseMirror)
                    DrawLine(new Vector2(640 - (560 - vec1.CentrePosition.Y), vec1.CentrePosition.X - 80), new Vector2(640 - (560 - vec2.CentrePosition.Y), vec2.CentrePosition.X - 80), Width, DrawingColor * Alpha, Depth, Image);
                if (VerticalMirror)
                    DrawLine(new Vector2(560 - vec1.CentrePosition.Y, 480 - (vec1.CentrePosition.X - 80)), new Vector2(560 - vec2.CentrePosition.Y, 480 - (vec2.CentrePosition.X - 80)), Width, DrawingColor * Alpha, Depth, Image);
                if (ObliqueMirror)
                    DrawLine(new Vector2(640 - (560 - vec1.CentrePosition.Y), 480 - (vec1.CentrePosition.X - 80)), new Vector2(640 - (560 - vec2.CentrePosition.Y), 480 - (vec2.CentrePosition.X - 80)), Width, DrawingColor * Alpha, Depth, Image);
            }
        }

        public float AppearTime => vec1.AppearTime;
        public new Vector2 Centre => (vec1.CentrePosition + vec2.CentrePosition) / 2;
        public new float Rotation => MathUtil.Direction(vec1.CentrePosition, vec2.CentrePosition);

        public override void Update()
        {
        }

        public void AlphaDecrease(float time)
        {
            float total = Alpha;
            float once = total / time;
            InstanceCreate(new TimeRangedEvent(time + 5, () => { Alpha -= once; }));
            InstanceCreate(new InstantEvent(time + 5, () => { Dispose(); }));
        }
        public void DelayAlphaDecrease(float delay,float time)
        {
            InstanceCreate(new InstantEvent(delay, () =>
            {
                float total = Alpha;
                float once = total / time;
                InstanceCreate(new TimeRangedEvent(time + 5, () => { Alpha -= once; }));
                InstanceCreate(new InstantEvent(time + 5, () => { Dispose(); }));
            }));
        }
        public void AlphaIncrease(float time, float val)
        {
            float total = val;
            float once = total / time;
            InstanceCreate(new TimeRangedEvent(time, () => { Alpha += once; }));
        }
        public void AlphaDecrease(float time, float val)
        {
            float total = val;
            float once = total / time;
            InstanceCreate(new TimeRangedEvent(time, () => { Alpha -= once; }));
            InstanceCreate(new InstantEvent(time, () => { if (Alpha < 0) Dispose(); }));
        }
        public void AlphaDecreaseAndIncrease(float time, float val)
        {
            float total = val;
            float once = total / time;
            InstanceCreate(new TimeRangedEvent(time / 4, () => { Alpha -= once * 4; }));
            InstanceCreate(new TimeRangedEvent(time / 4, time / 4 * 3, () => { Alpha += once * 4 / 3; }));
            InstanceCreate(new InstantEvent(time, () => { if (Alpha < 0) Dispose(); }));
        }
        public void AlphaIncreaseAndDecrease(float time, float val)
        {
            float total = val;
            float once = total / time;
            InstanceCreate(new TimeRangedEvent(time / 4, () => { Alpha += once * 4; }));
            InstanceCreate(new TimeRangedEvent(time / 4, time / 4 * 3, () => { Alpha -= once * 4 / 3; }));
            InstanceCreate(new InstantEvent(time, () => { if (Alpha < 0) Dispose(); }));
        }
        public Line Split(bool clear)
        {
            return clear
                ? this
                : new Line(vec1.CentrePosition, vec2.CentrePosition) { Alpha = Alpha, Depth = Depth, DrawingColor = DrawingColor, };
        }

        private struct LineState
        {
            public Vector2 p1, p2;
            public float alpha;
            public float time;
            public Color color;
            public bool transverseMirror, verticalMirror, obliqueMirror, verticalline;
        }
    }
}