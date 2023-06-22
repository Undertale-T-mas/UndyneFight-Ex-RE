using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class GrayShader : Shader
            {
                public float Intensity { get; set; } = 0;
                public GrayShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["intensity"].SetValue(Intensity);
                    };
                }
            }
        }
    }
}