using System.Collections.Generic;
using System;

namespace UndyneFight_Ex.Remake.Texts
{ 
    public class SmartFont
    {
        private SortedSet<Tuple<float, GLFont>> _gLFonts = new();

        private Dictionary<char, GLFont> _buffer = new();

        public void Insert(GLFont font, float priority)
        {
            _gLFonts.Add(new(-priority, font));
        }
        public GLFont GetFont(char ch) { 
            if(_buffer.ContainsKey(ch)) return _buffer[ch];
            var enumerator = _gLFonts.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var font = enumerator.Current.Item2;
                if (font.SFX.GetGlyphs().ContainsKey(ch)) return font;
            }
            throw new ArgumentOutOfRangeException($"Can not find charactor {ch} and its belonging in the stored glyphs");
        }
    }
}