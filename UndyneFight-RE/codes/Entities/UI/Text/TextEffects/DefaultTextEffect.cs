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
    public class DefaultTextEffect : TextEffect
    {
        public DefaultTextEffect() : base() { }
        public DefaultTextEffect(int textCount) : base(textCount) { }


        protected override void Update(TextSetting textSetting)
        { 
        }
    }
}