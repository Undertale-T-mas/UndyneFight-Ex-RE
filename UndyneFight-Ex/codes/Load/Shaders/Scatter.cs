using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class ScatterShader : Shader
            {
                public ScatterShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        Time += 0.5f;
                        x.Parameters["intensity"].SetValue(Intensity);
                        x.Parameters["time"].SetValue(Time);
                        x.Parameters["ratio"].SetValue(Ratio);
                    };
                }

                public float Intensity { private get; set; } = 3.0f;
                public float Time { private get; set; } = 0;
                public float Ratio { private get; set; } = 0.14f;
            }
        }
    }
}