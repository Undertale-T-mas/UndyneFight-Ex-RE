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

namespace UndyneFight_Ex.Remake.Texts
{ 
    public class TextSpeedEffect : TextEffect
    {
        public TextSpeedEffect(float textTime) : base(1) { _textTime = textTime; }

        private float _textTime;

        protected override void Update(TextSetting textSetting)
        {
            textSetting.TextTime = _textTime;
        }
    }
}