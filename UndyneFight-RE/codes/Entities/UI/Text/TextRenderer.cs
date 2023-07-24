using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Globalization;
using UndyneFight_Ex.Remake.UI;
using System.Threading;

namespace UndyneFight_Ex.Remake
{ 
    public abstract class TextRenderer : TextEffect
    {
        public TextRenderer() { }

        protected TextSetting Allocated { get; private set; }

        public abstract void Render(Vector2 position);

        protected override void Update(TextSetting textSetting)
        {
            this.Allocated = textSetting;
        }
    }
}