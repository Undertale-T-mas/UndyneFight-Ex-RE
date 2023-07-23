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
    public class TextColorEffect : TextEffect
    {
        public TextColorEffect(Color color) : base(1) { cur = color; }
        private int _totalCount = -1;
        public TextColorEffect(Color color, int textCount) : base(textCount + 1) { 
            _totalCount = textCount;
            cur = color;
        }

        Color def, cur;
        protected override void Update(TextSetting textSetting)
        { 
            if(_totalCount > 0)
            {
                if (TextIndex == 0) def = textSetting.BlendColor;
                if (_totalCount == TextIndex) textSetting.BlendColor = def;
                else textSetting.BlendColor = cur;
            }
            else
            {
                textSetting.BlendColor = cur;
            }
        }
    }
}