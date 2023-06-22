using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class NeonLineShader : Shader
            {
                public NeonLineShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["maintime"].SetValue(Time); Time += Speed;
                        x.Parameters["maincolor"].SetValue(DrawingColor.ToVector4() * 0.5f);
                    };
                }

                public float Speed { private get; set; } = 1.0f;
                public float Time { private get; set; } = 1;
                public Color DrawingColor { private get; set; } = Color.White;
            }
        }
    }
}