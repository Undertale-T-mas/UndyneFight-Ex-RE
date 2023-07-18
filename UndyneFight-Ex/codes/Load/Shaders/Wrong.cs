using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class WrongShader : Shader
            {
                public float Time { get; set; } = 0;
                public float Intensity { get; set; } = 0;
                public WrongShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        Time += 0.01f;
                        x.Parameters["iTime"].SetValue(Time);
                        x.Parameters["iValue"].SetValue(Intensity);
                    };
                }
            }
        }
    }
}