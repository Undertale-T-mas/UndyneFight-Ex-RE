using Microsoft.Xna.Framework;
using System;
using static Microsoft.Xna.Framework.MathHelper;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public abstract class SideBone : Bone
    {
        protected float speed;
        public float Speed { set => speed = value; get => speed; }
        protected float missionLength;
        public float MissionLength { get => missionLength; set => missionLength = value; }
        public float LengthLerpScale { get; set; } = 0.1f;
    }
    public class DownBone : SideBone
    {
        /// <summary>
        /// 制造出沿着屏幕下方移动的骨头
        /// </summary>
        /// <param name="way">从左边出现还是从右边出现，若true则右边，false则左边</param>
        /// <param name="length">骨头长度</param>
        /// <param name="speed">骨头速度</param>
        public DownBone(bool way, float speed, float length)
        {
            alpha = 1.0f;
            RectangleBox controlingBox = this.controlingBox as RectangleBox;
            collidingBox.X = way ? controlingBox.Right + 2 : controlingBox.Left - 2;

            Length = missionLength = length;
            this.speed = speed;
            movingWay = way;
        }

        /// <summary>
        /// 制造出沿着屏幕下方移动的骨头
        /// </summary>
        /// <param name="way">从左边出现还是从右边出现，若true则右边，false则左边</param>
        /// <param name="length">骨头长度</param>
        /// <param name="speed">骨头速度</param>
        /// <param name="position">骨头初始X轴位置</param>
        public DownBone(bool way, float position, float speed, float length)
        {
            alpha = 1.0f;
            collidingBox.X = position;

            missionLength = length;
            this.speed = speed;
            movingWay = way;
        }

        private readonly bool movingWay;

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            Length = Lerp(Length, missionLength, LengthLerpScale);
            collidingBox.X += speed * 0.5f * (movingWay ? -1 : 1);
            RectangleBox controlingBox = this.controlingBox as RectangleBox;
            collidingBox.Y = controlingBox.Down - Length / 2 - 3;
            base.Update();
        }
    }
    public class UpBone : SideBone
    {
        /// <summary>
        /// 制造出沿着屏幕上方移动的骨头
        /// </summary>
        /// <param name="way">从左边出现还是从右边出现，若true则右边，false则左边</param>
        /// <param name="length">骨头长度</param>
        /// <param name="speed">骨头速度</param>
        public UpBone(bool way, float speed, float length)
        {
            alpha = 1.0f;
            RectangleBox controlingBox = this.controlingBox as RectangleBox;
            collidingBox.X = way ? controlingBox.Right + 2 : controlingBox.Left - 2;

            Length = missionLength = length;
            this.speed = speed;
            movingWay = way;
        }

        /// <summary>
        /// 制造出沿着屏幕上方移动的骨头
        /// </summary>
        /// <param name="way">从左边出现还是从右边出现，若true则右边，false则左边</param>
        /// <param name="length">骨头长度</param>
        /// <param name="speed">骨头速度</param>
        /// <param name="position">骨头初始X轴位置</param>
        public UpBone(bool way, float position, float speed, float length)
        {
            alpha = 1.0f;
            collidingBox.X = position;

            missionLength = length;
            this.speed = speed;
            movingWay = way;
        }

        private readonly bool movingWay;

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            Length = Lerp(Length, missionLength, LengthLerpScale);
            collidingBox.X += speed * 0.5f * (movingWay ? -1 : 1);
            RectangleBox controlingBox = this.controlingBox as RectangleBox;
            collidingBox.Y = controlingBox.Up + Length / 2 + 3;
            base.Update();
        }
    }
    public class LeftBone : SideBone
    {
        /// <summary>
        /// 制造出沿着屏幕左方移动的骨头
        /// </summary>
        /// <param name="way">从上边出现还是从下边出现，若true则下边，false则上边</param>
        /// <param name="length">骨头长度</param>
        /// <param name="speed">骨头速度</param>
        public LeftBone(bool way, float speed, float length)
        {
            Rotation = 90f;
            alpha = 1.0f;
            RectangleBox controlingBox = this.controlingBox as RectangleBox;
            collidingBox.Y = way ? controlingBox.Down + 2 : controlingBox.Up - 2;

            Length = missionLength = length;
            this.speed = speed;
            movingWay = way;
        }

        /// <summary>
        /// 制造出沿着屏幕左方移动的骨头
        /// </summary>
        /// <param name="way">从上边出现还是从下边出现，若true则下边，false则上边</param>
        /// <param name="length">骨头长度</param>
        /// <param name="speed">骨头速度</param>
        /// <param name="position">骨头初始Y轴位置</param>
        public LeftBone(bool way, float position, float speed, float length)
        {
            Rotation = 90f;
            alpha = 1.0f;
            collidingBox.Y = position;

            missionLength = length;
            this.speed = speed;
            movingWay = way;
        }

        private readonly bool movingWay;

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            Length = Lerp(Length, missionLength, LengthLerpScale);
            collidingBox.Y += speed * 0.5f * (movingWay ? -1 : 1);
            RectangleBox controlingBox = this.controlingBox as RectangleBox;
            collidingBox.X = controlingBox.Left + Length / 2 + 3;
            base.Update();
        }
    }
    public class RightBone : SideBone
    {
        /// <summary>
        /// 制造出沿着屏幕右方移动的骨头
        /// </summary>
        /// <param name="way">从上边出现还是从下边出现，若true则下边，false则上边</param>
        /// <param name="length">骨头长度</param>
        /// <param name="speed">骨头速度</param>
        public RightBone(bool way, float speed, float length)
        {
            Rotation = 90f;
            alpha = 1.0f;
            RectangleBox controlingBox = this.controlingBox as RectangleBox;
            collidingBox.Y = way ? controlingBox.Down + 2 : controlingBox.Up - 2;

            Length = missionLength = length;
            this.speed = speed;
            movingWay = way;
        }

        /// <summary>
        /// 制造出沿着屏幕右方移动的骨头
        /// </summary>
        /// <param name="way">从上边出现还是从下边出现，若true则下边，false则上边</param>
        /// <param name="length">骨头长度</param>
        /// <param name="speed">骨头速度</param>
        /// <param name="position">骨头初始Y轴位置</param>
        public RightBone(bool way, float position, float speed, float length)
        {
            Rotation = 90f;
            alpha = 1.0f;
            collidingBox.Y = position;

            missionLength = length;
            this.speed = speed;
            movingWay = way;
        }

        private readonly bool movingWay;

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            Length = Lerp(Length, missionLength, LengthLerpScale);
            collidingBox.Y += speed * 0.5f * (movingWay ? -1 : 1);
            RectangleBox controlingBox = this.controlingBox as RectangleBox;
            collidingBox.X = controlingBox.Right - Length / 2 - 3;
            base.Update();
        }
    }

    public class CentreCircleBone : Bone
    {
        private float rotateSpeed;
        public float RotateSpeed { set => rotateSpeed = value; }
        public float MissionLength { set => missionLength = value; }
        private float duration, missionLength;
        private float appearTime = 0;

        public CentreCircleBone(float startRotation, float rotateSpeed, float length, float duration)
        {
            Centre = controlingBox.Centre;
            alpha = 1.0f;
            IsMasked = false;
            missionLength = length;
            this.duration = duration;
            this.rotateSpeed = rotateSpeed;
            Rotation = startRotation;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            Centre = controlingBox.Centre;
            appearTime += 0.5f;
            Length = appearTime >= duration ? Length * 0.96f - 0.25f : Length * 0.96f + missionLength * 0.04f;
            Rotation += rotateSpeed * 0.5f;
            base.Update();
            if (Length <= 1f)
                Dispose();
        }
    }

    public class SideCircleBone : Bone
    {
        /// <summary>
        /// 制造出一根沿着捕获框外围旋转的骨头(捕获框此时必须是正方形，否则会失效)
        /// </summary>
        /// <param name="startRotation">旋转起始位置</param>
        /// <param name="rotateSpeed">旋转速度</param>
        /// <param name="length">骨头长度</param>
        /// <param name="duration">骨头持续时间</param>
        public SideCircleBone(float startRotation, float rotateSpeed, float length, float duration)
        {
            autoDispose = false;
            IsMasked = false;
            Rotation = startRotation + 360;
            alpha = 1;
            this.rotateSpeed = rotateSpeed;
            missionLength = length;
            length1 = -controlingBox.CollidingBox.Width * 0.25f;
            this.duration = duration;
        }

        private float rotateSpeed, length1, appearTime = 0;
        private readonly float duration, missionLength;
        public float RotateSpeed { set => rotateSpeed = value; get => rotateSpeed; }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            if (appearTime <= duration)
                length1 = length1 * 0.93f + missionLength * 0.07f;
            appearTime += 0.5f;
            float r = controlingBox.CollidingBox.Width / 2,
                  alpha = (Rotation + 270) % 90, cosV;
            cosV = (float)Math.Cos(GetRadian(alpha <= 45f ? alpha : 90 - alpha));

            float dist1 = r - length1 / 2,
                  dist2 = r / cosV - 3;

            if (appearTime > duration)
                dist1 += (float)Math.Pow((appearTime - duration), 2) / 3f;
            float dist = (dist1 + dist2) / 2;

            Length = (dist2 - dist1);
            if (Length < -50)
                Dispose();

            Centre = controlingBox.Centre + GetVector2(dist, Rotation + 90);

            Rotation += rotateSpeed * 0.5f;
            if (Rotation < 360) Rotation += 360;

            base.Update();
        }
    }

    public class SwarmBone : Bone
    {
        public SwarmBone(float length, float roundTime, float startTime, float duration)
        {
            missionLength = length;
            this.duration = duration;
            this.startTime = startTime;
            this.roundTime = roundTime;
            alpha = 1f;
        }

        private readonly float missionLength, duration, roundTime, startTime;
        private float appearTime = 0;

        public override void Update()
        {
            if (appearTime >= duration)
            {
                Length -= 0.5f;
                Length *= 0.82f;
                if (Length < 0) Dispose();
            }
            else Length = Length * 0.86f + missionLength * 0.14f;

            float trueVal = ((appearTime + startTime) / roundTime * 360) % 360;
            float X = Cos(trueVal) * (controlingBox.CollidingBox.Width - 2) / 2;
            float heightDetla = controlingBox.CollidingBox.Height - Length - 2;
            float res1 = Math.Abs(Sin(trueVal));
            float Y = trueVal > 180 ? -(float)Math.Sqrt(res1) : (float)Math.Sqrt(res1);
            //Y = Sin(trueVal) * heightDetla / 2;

            Centre = new Vector2(X, Y * heightDetla / 2) + controlingBox.Centre;

            appearTime += 0.5f;

            base.Update();
        }
    }

    public class CustomBone : Bone, ICustomMotion, ICustomLength
    {
        public bool AlphaIncrease { get; set; } = false;

        public CollideRect screenC = new CollideRect(-50, -50, 740, 580);

        private readonly Vector2 startPos;

        public Func<ICustomMotion, Vector2> PositionRoute { get; set; }

        public Func<ICustomLength, float> LengthRoute { get; set; }
        public Func<ICustomMotion, float> RotationRoute { get; set; }

        public Vector2 CentrePosition => detla;

        public float[] PositionRouteParam { get; set; }
        public float[] LengthRouteParam { get; set; }
        public float[] RotationRouteParam { get; set; }

        public Func<CustomBone, float> AlphaRoute { private get; set; }

        public float AppearTime => appearTime;
        public float RotationDetla { set => rotationDetla = value; }

        private float rotationDetla;
        private float appearTime = 0;
        private Vector2 detla;

        public CustomBone(EaseUnit<Vector2> positionRoute, EaseUnit<float> rotationRoute, float length) : this(Vector2.Zero, positionRoute.Easing, (s) => length, rotationRoute.Easing) { }

        public CustomBone(Vector2 startPos, Func<ICustomMotion, Vector2> positionRoute, float rot, float len, float duration) : this(startPos, positionRoute, Motions.LengthRoute.autoFold, Motions.RotationRoute.stableValue)
        {
            RotationRouteParam = new float[] { rot };
            LengthRouteParam = new float[] { len, duration };
        }

        public CustomBone(Vector2 startPos, Func<ICustomMotion, Vector2> positionRoute, float rot, float len) : this(startPos, positionRoute, Motions.LengthRoute.stableValue, Motions.RotationRoute.stableValue)
        {
            RotationRouteParam = new float[] { rot };
            LengthRouteParam = new float[] { len };
        }

        public CustomBone(Vector2 startPos, Func<ICustomMotion, Vector2> positionRoute, Func<ICustomLength, float> lengthRoute, Func<ICustomMotion, float> rotationRoute)
        {
            alpha = 1.0f;

            Centre = startPos;
            this.startPos = startPos;
            PositionRoute = positionRoute;
            LengthRoute = lengthRoute;
            RotationRoute = rotationRoute;
        }
        public CustomBone(Vector2 startPos, Func<ICustomMotion, Vector2> positionRoute, Func<ICustomMotion, float> rotationRoute, float length)
        {
            alpha = 1.0f;

            LengthRouteParam = new float[] { length };
            Centre = startPos;
            this.startPos = startPos;
            PositionRoute = positionRoute;
            LengthRoute = Motions.LengthRoute.stableValue;
            RotationRoute = rotationRoute;
        }
        public CustomBone(Func<ICustomMotion, Vector2> positionRoute1, Func<ICustomMotion, Vector2> positionRoute2)
        {
            alpha = 1.0f;
            PositionRoute = positionRoute1;

        }
        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            if (AlphaRoute != null)
                alpha = AlphaRoute(this);
            else if (AlphaIncrease)
                alpha = Math.Min(1, appearTime / 20f);
            appearTime += 0.5f;
            Vector2 v = PositionRoute(this);
            Centre = startPos + v;
            detla = v;
            Rotation = RotationRoute(this) + rotationDetla;
            Length = LengthRoute(this);

            if (Length < -1) Dispose();

            base.Update();
        }
    }
}