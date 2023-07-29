using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class SpiralShader : Shader
            {
                public float Time { get; set; } = 0;
                public float Speed { get; set; } = 1;
                public float Intensity { get; set; } = 0;
                public SpiralShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        Time += 0.5f * Speed * TimeElapsed;
                        x.Parameters["iTime"].SetValue(Time);
                        x.Parameters["iUnit"].SetValue(Intensity);
                    };
                }
            }
        }
    }
}