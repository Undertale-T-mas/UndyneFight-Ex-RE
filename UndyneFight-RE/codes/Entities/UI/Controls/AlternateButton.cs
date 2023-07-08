using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndyneFight_Ex.Remake.UI
{
    internal class AlternateButton : Button
    {
        public AlternateButton(ISelectChunk father, Vector2 centre, string tip, params string[] texts) : base(father, centre, texts[0])
        {
            UpdateIn120 = true;
            _index = 0;
            _texts = texts;
            NeverEnable = true;
            this.LeftClick += AlternateSelection;
            this._tip = tip;
            Result = texts[0];
        }
        string _tip;
        public string Result { get; private set; }

        public string DefaultValue { set { 
                for(int i = 0; i < _texts.Length; i++)
                {
                    if (_texts[i] == value)
                    {
                        _index = i;
                        this.ChangeText(Result = _texts[i]);
                        return;
                    }
                }
            } }

        private void AlternateSelection()
        {
            _index++;
            if(_index >= _texts.Length) { _index = 0; }
            this.ChangeText(Result = _texts[_index]);
        }

        public override void Draw()
        {
            if(!_father.DrawEnabled) { return; }
            FightResources.Font.NormalFont.CentreDraw(_tip, this.Centre - new Vector2(0, 39), Color.White);
            base.Draw();
        }

        string[] _texts;
        int _index;
    }
}
