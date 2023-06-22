using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class BlurShader : Shader
            {
                public BlurShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iFactorX"].SetValue(Factor.X);
                        x.Parameters["iFactorY"].SetValue(Factor.Y);
                        x.Parameters["iSigma2"].SetValue(Sigma * Sigma);
                    };
                }

                public Vector2 Factor { private get; set; } = Vector2.Zero;
                public float Sigma { get; set; } = 0;
            }
            public class FastBlurShader : Shader
            {
                public FastBlurShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iFactorX"].SetValue(Factor.X);
                        x.Parameters["iFactorY"].SetValue(Factor.Y);
                        x.Parameters["iSigma2"].SetValue(Sigma * Sigma);
                    };
                }

                public Vector2 Factor { private get; set; } = Vector2.Zero;
                public float Sigma { get; set; } = 0;
            }
        }
    }
}