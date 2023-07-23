using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Globalization;
using UndyneFight_Ex.Remake.UI;
using System.Threading;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.Entities.EasingUtil;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake.Texts
{ 
    public class TextMotionEffect : TextEffect, ICustomMotion
    { 
        public TextMotionEffect(EaseUnit<Vector2> easing) : base() { this.PositionRoute = easing.Easing; }
        public TextMotionEffect(EaseUnit<Vector2> easing, int textCount) : base(textCount + 1) { 
            this.PositionRoute = easing.Easing;
            this._totalCount = textCount;
        }
        int _totalCount = -1;

        public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
        public Func<ICustomMotion, float> RotationRoute { get; set; }
        public float[] RotationRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float[] PositionRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float AppearTime { get; private set; }

        public float Rotation { get; private set; }

        public Vector2 CentrePosition { get; private set; } 

        protected override void Update(TextSetting textSetting)
        {
            this.AppearTime = textSetting.TimeRemain;
            if (_totalCount != -1) {
                if (this.TextIndex == _totalCount) textSetting.Delta = Vector2.Zero;
                else textSetting.Delta = PositionRoute.Invoke(this);
            }
            else
            { 
                textSetting.Delta = PositionRoute.Invoke(this);
            }
        }
    }
}