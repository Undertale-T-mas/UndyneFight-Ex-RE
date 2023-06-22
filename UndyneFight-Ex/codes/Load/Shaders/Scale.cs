using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class ScaleShader : Shader
            {
                public ScaleShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iPosX"].SetValue(320f);
                        x.Parameters["iPosY"].SetValue(240f);
                        x.Parameters["iValue"].SetValue(Intensity);
                    };
                }

                public float Intensity { private get; set; } = 1.0f;
            }
        }
    }
}