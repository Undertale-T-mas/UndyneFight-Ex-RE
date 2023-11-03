using System;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Remake.Resources;

namespace UndyneFight_Ex.Remake.Entities
{
    public class SoulBullet : AutoEntity, ICustomMotion
    {
        public SoulBullet(Player.Heart heart) {
            UpdateIn120 = true;
            this._origin = heart.Centre + MathUtil.GetVector2(14, heart.Rotation + 90);
            this.PositionRoute = EasingUtil.CentreEasing.Linear(MathUtil.GetVector2(22, heart.Rotation + 90));
            this.Image = FightSprites.SoulShoot;
            PlaySound(Sounds.YellowShoot);
            this.Rotation = heart.Rotation + 180;
            this.AngleMode = true;
            this.Broadcast("Bullet");
        }

        public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
        public Func<ICustomMotion, float> RotationRoute { get; set; }
        public float[] RotationRouteParam { get; set; }
        public float[] PositionRouteParam { get; set; }

        public float AppearTime { get; set; } = 0.0f;

        public Vector2 CentrePosition => _delta;

        public bool Hitting { get; internal set; }

        Vector2 _delta;
        Vector2 _origin;

        public override void Update()
        { 
            this._delta = this.PositionRoute.Invoke(this);
            this.Centre = this._delta + _origin;
            this.AppearTime += 0.5f;
            this.Depth = 0.5f;
            this.Alpha = 1.0f;

            if (AppearTime >= 35f) this.Dispose();
        }

        internal void HitTarget()
        {
            this.Hitting = true;
        }
    }
}