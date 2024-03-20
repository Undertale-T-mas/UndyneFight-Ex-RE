using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class SwirlShader : Shader
            {
                public SwirlShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iDense"].SetValue(Dense);
                        x.Parameters["iDistort"].SetValue(1 * Intensity);
                        x.Parameters["iRadius"].SetValue(new Vector2(Radius, RadiusOut));
                    };
                }

                public float Intensity { private get; set; } = 1.0f;
                public float Radius { private get; set; } = 100f;
                public float RadiusOut { private get; set; } = 100f;
                public float Dense { private get; set; } = 10f;
            }
        }
    }
}