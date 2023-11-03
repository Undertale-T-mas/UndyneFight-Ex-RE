using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex.Remake
{
    public class Text : Entity
    {
        private class DefaultTextRender : TextRenderer
        {
            public override void Render(Vector2 position)
            {
                Vector2 pos = position + Allocated.Delta;

                var glyph = Allocated.CurrentGlyph;
                Texture2D tex = Allocated.FontTexture;

                VertexPositionColorTexture[] vertexs = null;

                if (!Allocated.VertexEnabled)
                {
                    float l, r, t, b;

                    float w = tex.Width, h = tex.Height;
                    l = glyph.BoundsInTexture.Left / w;
                    r = glyph.BoundsInTexture.Right / w;
                    t = glyph.BoundsInTexture.Top / h;
                    b = glyph.BoundsInTexture.Bottom / h;

                    w = Allocated.CurrentGlyph.BoundsInTexture.Width * Allocated.Scale.X;
                    h = Allocated.CurrentGlyph.BoundsInTexture.Height * Allocated.Scale.Y;

                    pos += Allocated.CurrentGlyph.Cropping.Location.ToVector2();
                    vertexs = new VertexPositionColorTexture[4];
                    vertexs[0] = new(new Vector3(pos, Allocated.Depth), Allocated.BlendColor, new(l, t));
                    vertexs[1] = new(new Vector3(pos + new Vector2(w, 0), Allocated.Depth), Allocated.BlendColor, new(r, t));
                    vertexs[2] = new(new Vector3(pos + new Vector2(w, h), Allocated.Depth), Allocated.BlendColor, new(r, b));
                    vertexs[3] = new(new Vector3(pos + new Vector2(0, h), Allocated.Depth), Allocated.BlendColor, new(l, b));
                }
                GameStates.SpriteBatch.DrawVertex(tex, Allocated.Depth, vertexs);
            }
        }

        string _text;
        Vector2 _location;
        public Text(string text, Vector2 location, params TextEffect[] effects) { 
            this._text = text;
            this._location = location;
            Settings = new();
            UpdateIn120 = true;
            _textEffects = effects;
        }
        public Text(string text, Vector2 location, IEnumerable<TextEffect> effects) { 
            this._text = text;
            this._location = location;
            UpdateIn120 = true;
            _textEffects = effects.ToArray();
        }

        private TextEffect[] _textEffects = null;
        public TextSetting Settings { get; private set; }

        public bool PlaySound { get; init; } = true;

        private float _appearTime = 0.0f;
        private int _lastLength = -1;

        public override void Draw()
        {
            Settings = new();
            Settings.Depth = this.Depth;

            List<TextEffect> runningEffects = new();
            DefaultTextRender render;
            runningEffects.Add(render = new DefaultTextRender());
            render.GlobalReset();
            int i = 0, length = 0, effIndex = 0;

            while (_text[i] == '$') // process the pre-Effects
            {
                runningEffects.Add(_textEffects[i]);
                _textEffects[i].GlobalReset();
                effIndex++; i++;
            }
            float time = _appearTime;
            Vector2 position = _location;
            for(; i < _text.Length; i++)
            {
                if (_text[i] == '$') // add an effect
                {
                    runningEffects.Add(_textEffects[effIndex]);
                    _textEffects[effIndex].GlobalReset();
                    effIndex++;
                    continue;
                }
                if (i != 0) // calculate the time. To print the first char do not cost time!
                {
                    time -= Settings.TextTime;
                    if (time < 0.0f) break;
                }
                Settings.TimeRemain = time;
                
                // draw a char
                length++;

                runningEffects.RemoveAll(s => s.GlobalRun(this));

                var font = Settings.Font.GetFont(_text[i]).SFX;
                Settings.CurrentGlyph = font.GetGlyphs()[_text[i]];
                Settings.FontTexture = font.Texture;
                position.X += Settings.CurrentGlyph.LeftSideBearing * Settings.Scale.X;

                runningEffects.ForEach(s => {
                    (s as TextRenderer)?.Render(position);
                });
                 
                position.X += (Settings.CurrentGlyph.RightSideBearing + Settings.CurrentGlyph.Width)* Settings.Scale.X;
            }

            if(length > _lastLength && PlaySound)
            {
                //play a sound of text
                Fight.Functions.PlaySound(Settings.TextSound);
            }
            _lastLength = length;
        }

        public override void Update()
        {
            _appearTime += 0.5f;
            foreach(var effect in _textEffects)
            {
                effect.GlobalReset();
            }
        }
    }
}