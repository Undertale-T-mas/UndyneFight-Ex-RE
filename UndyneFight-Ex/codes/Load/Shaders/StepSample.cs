using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class StepSampleShader : Shader
            {
                public StepSampleShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iLightPosX"].SetValue(CentreX);
                        x.Parameters["iLightPosY"].SetValue(CentreY);
                        x.Parameters["iDistance"].SetValue(4 * Intensity);
                        x.Parameters["iSampling"].SetValue(1f);
                    };
                }

                public float Intensity { private get; set; } = 1.0f;
                public float CentreX { private get; set; } = 320f;
                public float CentreY { private get; set; } = 240f;
            }
        }
    }
}