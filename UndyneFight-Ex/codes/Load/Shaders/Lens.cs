using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class LensShader : Shader
            {
                public LensShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iCenter"].SetValue(Centre);
                        x.Parameters["iRadius"].SetValue(Radius);
                        x.Parameters["iCongergence"].SetValue(Congergence);
                    };
                }

                public float Congergence { private get; set; } = 1.0f;
                public float Radius { private get; set; } = 0.0f;
                public Vector2 Centre { private get; set; } = new Vector2(320, 240);
            }
        }
    }
}