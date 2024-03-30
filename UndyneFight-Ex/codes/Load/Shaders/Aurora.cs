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
                        x.Parameters["iRGB1"].SetValue(ThemeColorA.ToVector3());
                        x.Parameters["iRGB2"].SetValue(ThemeColorB.ToVector3());
                        x.Parameters["iSlope"].SetValue(Slope);
                        x.Parameters["type"].SetValue(Type);
                        float u = YCentre / 640f;
                        // y * slope = addition
                        // fx : float y1 = 1.0 - abs(uv.y * iSlope - iAddition);
                        x.Parameters["iAddition"].SetValue(u * Slope);
                    };
                }

                public float Time { get; set; } = 0.0f;
                public float YCentre { get; set; } = 320.0f;
                public float Slope { get; set; } = 2.0f;
                public float Type { get; set; } = 0f;
                public Color ThemeColorA { get; set; } = Color.White;
                public Color ThemeColorB { get; set; } = Color.White;
            }
        }
    }
}