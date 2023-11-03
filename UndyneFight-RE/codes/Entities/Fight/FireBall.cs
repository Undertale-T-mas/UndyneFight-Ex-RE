using System;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake.Entities
{
    public class FireBall : Barrage
    {
        public FireBall(EaseUnit<Vector2> _positionRoute) : this() {
            this.PositionRoute = _positionRoute.Easing;
        }
        public FireBall(Func<ICustomMotion, Vector2> _positionRoute) : this() {
            this.PositionRoute = _positionRoute;
        }
        public bool AutoDispose { set;  get; } = true;
        FireBall()
        {
            this.AddChild(new ImageMotion(Resources.FightSprites.Fireball, 5));
            this.Depth = 0.5f;
            this._autoDispose = true;
            this.Alpha = 1.0f;
            this.UpdateIn120 = true;
            this.HitRadius = 4f;
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
    }
}