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
        public bool AlphaBlend { private get; set; } = false;
        public Line(Vector2 vec1, Vector2 vec2) : this(EasingUtil.CentreEasing.Stable(vec1), EasingUtil.CentreEasing.Stable(vec2)) { }
        public Line(Vector2 centre, float rotation) : this(centre, EasingUtil.ValueEasing.Stable(rotation)) { }
        public Line(float Xcentre, float rotation) : this(EasingUtil.CentreEasing.Stable(Xcentre, 240), EasingUtil.ValueEasing.Stable(rotation)) { }
        public Line(Func<ICustomMotion, Vector2> easing1, Func<ICustomMotion, Vector2> easing2)
        {
            UpdateIn120 = true;
            vec1 = new(easing1);
            vec2 = new(easing2);
            AddChild(vec1);
            AddChild(vec2);
        }
        public Line(Vector2 centre, Func<ICustomMotion, float> rotationEasing) : this(EasingUtil.CentreEasing.Stable(centre), rotationEasing) { }
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
                jr = MathUtil.Posmod(jr, 180);
                Vector2 result;
                xCalc = jr < 45 || jr > 135f;
                if (xCalc)
                {
                    float dist = centre.X + 640;
                    result = new(-640, centre.Y - (Tan(rotation) * dist));
                }
                else
                {
                    float dist = centre.Y + 480;
                    result = new(centre.X - (dist / Tan(rotation)), -480);
                }
                return result;
            };
            Func<ICustomMotion, Vector2> easing2 = (s) =>
            {
                Vector2 result;
                if (xCalc)
                {
                    float dist = 1280 - centre.X;
                    result = new(1280, centre.Y + (Tan(rotation) * dist));
                }
                else
                {
                    float dist = 960 - centre.Y;
                    result = new(centre.X + (dist / Tan(rotation)), 960);
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
        void DrawTargetLine(Vector2 Start, Vector2 End)
        {
            DrawLine(Start, End, Width, AlphaBlend ? Color.Lerp(DrawingColor, ScreenDrawing.BackGroundColor, Alpha) : (DrawingColor * Alpha), Depth, Image);
        }
        public override void Draw()
        {
            if (Alpha <= 0) return;
            DrawTargetLine(vec1.CentrePosition, vec2.CentrePosition);
            if (VerticalMirror)
                DrawTargetLine(new Vector2(vec1.CentrePosition.X, 480 - vec1.CentrePosition.Y), new Vector2(vec2.CentrePosition.X, 480 - vec2.CentrePosition.Y));
            if (TransverseMirror)
                DrawTargetLine(new Vector2(640 - vec1.CentrePosition.X, vec1.CentrePosition.Y), new Vector2(640 - vec2.CentrePosition.X, vec2.CentrePosition.Y));
            if (ObliqueMirror)
                DrawTargetLine(new Vector2(640 - vec1.CentrePosition.X, 480 - vec1.CentrePosition.Y), new Vector2(640 - vec2.CentrePosition.X, 480 - vec2.CentrePosition.Y));
            if (VerticalLine)
            {
                DrawTargetLine(new Vector2(-vec1.CentrePosition.Y - 80, vec1.CentrePosition.X - 80), new Vector2(-vec2.CentrePosition.Y - 80, vec2.CentrePosition.X - 80));
                if (TransverseMirror)
                    DrawTargetLine(new Vector2(640 - (560 - vec1.CentrePosition.Y), vec1.CentrePosition.X - 80), new Vector2(640 - (560 - vec2.CentrePosition.Y), vec2.CentrePosition.X - 80));
                if (VerticalMirror)
                    DrawTargetLine(new Vector2(560 - vec1.CentrePosition.Y, 480 - (vec1.CentrePosition.X - 80)), new Vector2(560 - vec2.CentrePosition.Y, 480 - (vec2.CentrePosition.X - 80)));
                if (ObliqueMirror)
                    DrawTargetLine(new Vector2(640 - (560 - vec1.CentrePosition.Y), 480 - (vec1.CentrePosition.X - 80)), new Vector2(640 - (560 - vec2.CentrePosition.Y), 480 - (vec2.CentrePosition.X - 80)));
            }
        }

        public float AppearTime => vec1.AppearTime;
        public new Vector2 Centre => (vec1.CentrePosition + vec2.CentrePosition) / 2;
        public new float Rotation => MathUtil.Direction(vec1.CentrePosition, vec2.CentrePosition);

        public override void Update() { }

        public void AlphaDecrease(float time)
        {
            float total = Alpha, once = total / time;
            InstanceCreate(new TimeRangedEvent(time + 5, () => Alpha -= once));
            InstanceCreate(new InstantEvent(time + 5, () => Dispose()));
        }
        public void DelayAlphaDecrease(float delay, float time)
        {
            InstanceCreate(new InstantEvent(delay, () =>
            {
                float total = Alpha, once = total / time;
                InstanceCreate(new TimeRangedEvent(time + 5, () => Alpha -= once));
                InstanceCreate(new InstantEvent(time + 5, () => Dispose()));
            }));
        }
        public void DelayAlphaDecrease(float delay, float time, float val)
        {
            InstanceCreate(new InstantEvent(delay, () =>
            {
                float total = val, once = total / time;
                InstanceCreate(new TimeRangedEvent(time + 5, () => Alpha -= once));

            }));
        }
        public void AlphaIncrease(float time, float val)
        {
            float total = val, once = total / time;
            InstanceCreate(new TimeRangedEvent(time, () => Alpha += once));
        }
        public void DelayAlphaIncrease(float delay, float time, float val)
        {
            InstanceCreate(new InstantEvent(delay, () =>
            {
                float total = val, once = total / time;
                InstanceCreate(new TimeRangedEvent(time, () => Alpha += once));
            }));
        }
        public void AlphaDecrease(float time, float val)
        {
            float total = val, once = total / time;
            InstanceCreate(new TimeRangedEvent(time, () => Alpha -= once));
            InstanceCreate(new InstantEvent(time, () => { if (Alpha < 0) Dispose(); }));
        }
        public void AlphaDecreaseAndIncrease(float time, float val)
        {
            float total = val, once = total / time;
            InstanceCreate(new TimeRangedEvent(time / 4, () => Alpha -= once * 4));
            InstanceCreate(new TimeRangedEvent(time / 4, time / 4 * 3, () => Alpha += once * 4 / 3));
            InstanceCreate(new InstantEvent(time, () => { if (Alpha < 0) Dispose(); }));
        }
        public void AlphaIncreaseAndDecrease(float time, float val)
        {
            float total = val, once = total / time;
            InstanceCreate(new TimeRangedEvent(time / 4, () => Alpha += once * 4));
            InstanceCreate(new TimeRangedEvent(time / 4, time / 4 * 3, () => Alpha -= once * 4 / 3));
            InstanceCreate(new InstantEvent(time, () => { if (Alpha < 0) Dispose(); }));
        }
        public Line Split(bool clear)
        {
            return clear
                ? this
                : new Line(vec1.CentrePosition, vec2.CentrePosition) { Alpha = Alpha, Depth = Depth, DrawingColor = DrawingColor, };
        }
        public void AddShadow(float timeLag, float alphaFactor)
        {
            InsertRetention(new RetentionEffect(timeLag, alphaFactor));
        }
        public void AddShadow(RetentionEffect r)
        {
            InsertRetention(r);
        }
        public void AddShadow(params RetentionEffect[] r)
        {
            for (int a = 0; a < r.Length; a++)
            {
                int x = a;
                InsertRetention(r[x]);
            }
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