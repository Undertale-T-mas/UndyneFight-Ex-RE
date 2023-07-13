using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class BlockTileShader : Shader
            {
                public BlockTileShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iOffset"].SetValue(Offset);
                        x.Parameters["iWidth"].SetValue(Width);
                        x.Parameters["iScatter"].SetValue(Scatter);
                        x.Parameters["iSize"].SetValue(Size);
                    };
                }

                public Vector2 Offset { private get; set; } = new Vector2(320, 240);
                public Vector2 Width { private get; set; } = new Vector2(320, 240);
                public Vector2 Scatter { private get; set; } = new Vector2(320, 240);
                public Vector2 Size { private get; set; } = new Vector2(320, 240);
            }
        }
    }
}