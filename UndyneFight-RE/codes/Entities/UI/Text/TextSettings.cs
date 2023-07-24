using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Globalization;
using UndyneFight_Ex.Remake.UI;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using static Microsoft.Xna.Framework.Graphics.SpriteFont;
using UndyneFight_Ex.Remake.Texts;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex.Remake
{ 
    public struct TextVertex
    {
        public Vector2 TextureCoordinate { get; set; }
        public Color VertexColor { get; set; }
    }
    public class TextSetting
    {
        public TextSetting() { }

        public Vector2 Delta { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public Color BlendColor { get; set; } = Color.White;

        public bool VertexEnabled { get; set; } = false;
        public TextVertex[] Vertexs { get; set; }

        public Glyph CurrentGlyph { get; set; } 
        public float TextTime { get; set; }

        public SmartFont Font { get; set; } = Resources.FontPatched.Default;
        public SoundEffect TextSound { get; set; } = FightResources.Sounds.printWord;

        public float TimeRemain { get; set; }

        public float Depth { get; set; }
        public Texture2D FontTexture { get; set; }
    }
}