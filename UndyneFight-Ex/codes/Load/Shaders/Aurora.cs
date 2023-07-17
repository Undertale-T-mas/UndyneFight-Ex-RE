using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class AuroraShader : Shader
            {
                public AuroraShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        Time += 0.05f;

                        x.Parameters["iTime"].SetValue(Time);
                        x.Parameters["iRGB"].SetValue(ThemeColor.ToVector3());
                        x.Parameters["hashMap"].SetValue(Sprites.hashtex);
                    };
                }

                public float Time { get; set; } = 0.0f;
                public Color ThemeColor { get; set; } = Color.White;
            }
        }
    }
}