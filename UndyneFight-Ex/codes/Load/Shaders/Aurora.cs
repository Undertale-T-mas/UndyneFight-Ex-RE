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
                        Time += 0.01f;
                        RegisterTexture(Sprites.hashtex, 1);

                        x.Parameters["iTime"].SetValue(Time);
                        x.Parameters["iRGB"].SetValue(ThemeColor.ToVector3()); 
                    };
                }

                public float Time { get; set; } = 0.0f;
                public Color ThemeColor { get; set; } = Color.White;
            }
        }
    }
}