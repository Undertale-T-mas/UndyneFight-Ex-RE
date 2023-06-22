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
                        x.Parameters["iDense"].SetValue(13f);
                        x.Parameters["iDistort"].SetValue(1 * Intensity);
                        x.Parameters["iRadius"].SetValue(Radius);
                        x.Parameters["iRadiusOut"].SetValue(RadiusOut);
                        x.Parameters["iAngle"].SetValue(Rotation);
                    };
                }

                public float Intensity { private get; set; } = 1.0f;
                public float Radius { private get; set; } = 1.0f;
                public float RadiusOut { private get; set; } = 1.0f;
                public float Rotation { private get; set; } = 0.0f;
            }
        }
    }
}