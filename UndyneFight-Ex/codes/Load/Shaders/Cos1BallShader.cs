using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class BallShapingShader : Shader
            {
                public BallShapingShader(Effect eff) : base(eff)
                {
                    StableEvents = (X) =>
                    {
                        Parameters["fSizeMult"].SetValue(Intensity);
                        Parameters["scale2"].SetValue(ScreenScale);
                    };
                }

                private void Calc()
                {
                    Vector2 Turnpolar(Vector2 pos)
                    {
                        return new Vector2(MathF.Atan2(pos.Y, pos.X), pos.Length());
                    }

                    Vector2 Turnposition(Vector2 polar)
                    {
                        return new Vector2(MathF.Cos(polar.X) * polar.Y, MathF.Sin(polar.X) * polar.Y);
                    }

                    Vector2 MainPS(Vector2 textCoordinates)
                    {
                        float fBallArgument = 3.1415926f / 2.0f;
                        Vector2 center = new(320, 240);
                        Vector2 position = new(textCoordinates.X, 240 * textCoordinates.Y);
                        Vector2 usingPolar = Turnpolar(position - center);
                        Vector2 usingPosition = (Turnposition(new Vector2(usingPolar.X, MathF.Asin(usingPolar.Y / (center.Length() * Intensity)) / fBallArgument * (center.Length() * Intensity))) * 1.57f)
                        + center;
                        Vector2 usingTextureCoord = new(usingPosition.X / 320, usingPosition.Y / 240);
                        return usingTextureCoord;
                    }

                    Vector2 t2 = MainPS(new(0.5f, 1));
                }

                public float Intensity { private get => intensity; set => intensity = 1 / value; }
                public float ScreenScale { get; set; } = 1;

                private float intensity;
            }
        }
    }
}