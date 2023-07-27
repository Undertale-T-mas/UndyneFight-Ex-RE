using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;
using System.Runtime.ExceptionServices;
using System.Net.Http;
using System.Diagnostics;
using static UndyneFight_Ex.Entities.SimplifiedEasing;

namespace UndyneFight_Ex.Remake.Entities
{
    public class Spider : Barrage
    {
        public Spider(EaseUnit<Vector2> _positionRoute) : this() {
            this.PositionRoute = _positionRoute.Easing;
        }
        public Spider(Func<ICustomMotion, Vector2> _positionRoute) : this() {
            this.PositionRoute = _positionRoute;
        }
        public Spider(EaseUnit<Vector2>_positionRoute,EaseUnit<float> _rotationRoute) : this()
        {
            this.PositionRoute=_positionRoute.Easing;
            this.RotationRoute = _rotationRoute.Easing;
        }
        public Spider(EaseUnit<Vector2> _positionRoute, float Rotation) : this()
        {
            this.PositionRoute = _positionRoute.Easing;
            this.Rotation = Rotation;
        }
        public Spider(Func<ICustomMotion, Vector2> _positionRoute, Func<ICustomMotion, float> _rotationRoute) : this()
        {
            this.PositionRoute = _positionRoute;
            this.RotationRoute = _rotationRoute;
        }
        public bool AutoDispose { set;  get; } = true;
        Spider()
        {
            this.Image = Resources.FightSprites.Spider;
            this.Depth = 0.5f;
            this._autoDispose = true;
            this.Alpha = 1.0f;
            this.UpdateIn120 = true;
            this.HitRadius = 4f;
            this.AngelMode = true;
            this.controlLayer = Surface.Hidden;
        }
        protected override float GetDistance(Player.Heart heart)
        {
            return Vector2.Distance(heart.Centre, this.Centre);
        }
        public override void Start()
        {
            _autoDispose = AutoDispose;
            this.HitRadius = 4f*Scale;
        }
        public class LineSpider : Spider
        {
            protected float speed;
            public float Speed { set => speed = value; get => speed; }
            public LineSpider(int CountLine, bool IsLeftOrRight, float Speed)
            {
                /*float SpiderX1 = BoxStates.Centre.X - BoxStates.Width / 2f;
                float SpiderX2 = BoxStates.Centre.X + BoxStates.Width / 2f;
                float SpiderY = BoxStates.Height / (Heart.PurpleLineCount + 1f) * CountLine + BoxStates.Centre.Y - BoxStates.Height / 2f;
                if (IsLeftOrRight)
                {
                    this.PositionRoute = EaseOut(BoxStates.Width / Speed, new Vector2(SpiderX1, SpiderY), new Vector2(SpiderX2, SpiderY), EaseState.Linear).Easing;
                }
                else
                {
                    this.PositionRoute = EaseOut(BoxStates.Width / Speed, new Vector2(SpiderX2, SpiderY), new Vector2(SpiderX1, SpiderY), EaseState.Linear).Easing;
                }*/
                collidingBox.X = IsLeftOrRight ? BoxStates.Centre.X - BoxStates.Width / 2f - 10f : BoxStates.Centre.X + BoxStates.Width / 2f + 10f;
                this.speed = Speed;
                movingWay = IsLeftOrRight;
            }
            private readonly bool movingWay;
            public override void Draw()
            {
                base.Draw();
            }
            public override void Update()
            {
                collidingBox.X += speed * 0.5f * (movingWay ? 1 : -1);
                base.Update();
            }
        }
    }
}