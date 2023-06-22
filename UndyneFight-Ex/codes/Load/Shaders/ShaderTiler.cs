using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class ShaderTiler : Shader
            {
                public ShaderTiler(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iCenter"].SetValue(Centre);
                        x.Parameters["iScale"].SetValue(Scale);
                    };
                }

                public float Scale { private get; set; } = 1f;
                public Vector2 Centre { private get; set; } = new(320, 240);
            }
        }
    }
}