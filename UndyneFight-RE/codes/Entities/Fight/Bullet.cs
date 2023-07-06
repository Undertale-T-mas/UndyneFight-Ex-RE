using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake.Entities
{
    public class SoulBullet : AutoEntity, ICustomMotion
    {
        public SoulBullet(Player.Heart heart) {
            UpdateIn120 = true;
            this._origin = heart.Centre;
            this.PositionRoute = EasingUtil.CentreEasing.Linear(MathUtil.GetVector2(10, heart.Rotation + 90));
        }

        public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
        public Func<ICustomMotion, float> RotationRoute { get; set; }
        public float[] RotationRouteParam { get; set; }
        public float[] PositionRouteParam { get; set; }

        public float AppearTime { get; set; } = 0.0f;

        public Vector2 CentrePosition => _delta;
        Vector2 _delta;
        Vector2 _origin;

        public override void Update()
        { 
            this._delta = this.PositionRoute.Invoke(this);
            this.Centre = this._delta + _origin;
        }
    }
}