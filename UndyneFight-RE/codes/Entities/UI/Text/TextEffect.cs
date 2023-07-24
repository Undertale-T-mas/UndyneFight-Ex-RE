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
    public abstract class TextEffect 
    {
        public TextEffect(int textRange) {
            this.TextRange = textRange;   
        }
        public TextEffect() {
            this.TextRange = 0x3f3f3f3f;   
        }
        public int TextRange { get; private set; }
        protected int TextIndex { get; private set; } = 0;

        protected abstract void Update(TextSetting textSetting); 

        internal bool GlobalRun(Text text)
        {
            if(_count == 0) { return true; }
            _count--;
            this.Update(text.Settings); TextIndex++;
            return false;
        }
        private int _count = 0;
        internal void GlobalReset()
        {
            _count = TextRange;
            TextIndex = 0;
        }
    }
}