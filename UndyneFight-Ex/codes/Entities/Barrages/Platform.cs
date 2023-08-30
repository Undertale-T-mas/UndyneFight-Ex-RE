using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static UndyneFight_Ex.FightResources.Sprites;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public class Platform : Entity, ICustomMotion, ICustomLength
    {
        protected bool autoDispose = true;
        private bool hasBeenInside = false;
        private static CollideRect screen = new(-550, -550, 1740, 1580);

        private float length;
        private Vector2 startPos;
        public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
        public Func<ICustomLength, float> LengthRoute { get; set; }
        public Func<ICustomMotion, float> RotationRoute { get; set; }
        public Vector2 CentrePosition => detla;

        public float[] PositionRouteParam { get; set; }
        public float[] LengthRouteParam { get; set; }
        public float[] RotationRouteParam { get; set; }

        public void ResetStartPosition(Vector2 vector2)
        {
            startPos = vector2;
        }

        public float AppearTime => appearTime;
        protected int platformType;
        protected float appearTime = 0;
        public bool isMasked = true, createWithScaling = false; public float scale = 0.0f;
        private Vector2 detla;

        public Platform(int platformType, Vector2 startPos, Func<ICustomMotion, Vector2> positionRoute, float rot, float len) : this(platformType, startPos, positionRoute, Motions.LengthRoute.stableValue, Motions.RotationRoute.stableValue)
        {
            RotationRouteParam = new float[] { rot };
            LengthRouteParam = new float[] { len };
        }

        public Platform(int platformType, Vector2 startPos, Func<ICustomMotion, Vector2> positionRoute, float rot, float len, float duration) : this(platformType, startPos, positionRoute, Motions.LengthRoute.autoFold, Motions.RotationRoute.stableValue)
        {
            RotationRouteParam = new float[] { rot };
            LengthRouteParam = new float[] { len, duration };
        }

        public Platform(int platformType, Vector2 startPos, Func<ICustomMotion, Vector2> positionRoute, Func<ICustomLength, float> lengthRoute, Func<ICustomMotion, float> rotationRoute)
        {
            gravityLine = new GravityLine(Vector2.Zero, Vector2.Zero);
            this.platformType = platformType;
            Centre = startPos;
            this.startPos = startPos;
            PositionRoute = positionRoute;
            LengthRoute = lengthRoute;
            RotationRoute = rotationRoute;
            Image = platform[platformType];
            gravityLine.SetWidth(4);
            gravityLine.sticky = platformType == 0;
            UpdateIn120 = true;
        }

        public override void Draw()
        {
            Vector2 delta = GetVector2(length / 2, Rotation);
            var side = platformSide[platformType];
            GameMain.MissionSpriteBatch.Draw(Image, Centre, new Rectangle(0, 0, (int)length, 12), Color.White, GetRadian(Rotation), new Vector2(length / 2, 6), 1.0f, SpriteEffects.None, 0.35f);
            Depth = 0.35F;
            FormalDraw(side, Centre - delta, Color.White, GetRadian(Rotation), new Vector2(0, 6));
            FormalDraw(side, Centre + delta, Color.White, GetRadian(Rotation), new Vector2(0, 6));
        }

        private readonly GravityLine gravityLine;

        public override void Update()
        {
            controlLayer = isMasked ? Surface.Hidden : Surface.Normal;
            if (autoDispose)
            {
                if (length < 0) Dispose();
                bool ins = screen.Contain(Centre);
                if (ins && (!hasBeenInside)) hasBeenInside = true;
                if (hasBeenInside && (!ins)) Dispose();
            }
            scale = createWithScaling ? scale * 0.85f + 0.15f : 1.0f;
            appearTime += 0.5f;
            Vector2 v = PositionRoute(this);
            this.detla = v;
            Centre = startPos + v;
            Rotation = RotationRoute(this);
            length = LengthRoute(this) * scale;

            Vector2 detla = GetVector2(length / 2, Rotation);

            gravityLine.SetPosition(Centre + detla, Centre - detla);
            gravityLine.SetLength(length);
        }

        public override void Dispose()
        {
            gravityLine?.Dispose();
            base.Dispose();
        }

        /// <summary>
        /// 获取一组有关此平台的数据。数据值分别为: length, platformType
        /// </summary>
        /// <returns></returns>
        internal Vector2 GetData()
        {
            return new Vector2(length, platformType);
        }

        public void ChangeType()
        {
            platformType = 1 - platformType;
            Image = platform[platformType];
            gravityLine.sticky = !gravityLine.sticky;
        }
        public void ResetTime()
        {
            appearTime = 0;
        }
    }
}