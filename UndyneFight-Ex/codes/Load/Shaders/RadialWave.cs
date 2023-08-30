using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class RadialWaveShader : Shader
            {
                public RadialWaveShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iCenter"].SetValue(Centre);
                        x.Parameters["iRadius"].SetValue(Radius);
                        x.Parameters["iProgress"].SetValue(Progress);
                    };
                }

                public float Progress { private get; set; } = 0.0f;
                public float Radius { private get; set; } = 490.0f;
                public Vector2 Centre { private get; set; } = new Vector2(320, 240);
            }
        }
    }
}