using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class SeismicShader : Shader
            {
                public SeismicShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iCenter"].SetValue(Centre);
                        x.Parameters["iRadius"].SetValue(Radius);
                        x.Parameters["iProgress"].SetValue(Progress);
                    };
                }

                public float Progress { private get; set; } = 0.0f;
                public float Radius { private get; set; } = 100;
                public Vector2 Centre { private get; set; } = new(320, 240);
            }
        }
    }
}