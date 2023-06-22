using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class PolarShader : Shader
            {
                public PolarShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["itype"].SetValue(IType ? 1f : 0f);
                        x.Parameters["idegree"].SetValue(Intensity);
                    };
                }

                public bool IType { get; set; } = false;
                public float Intensity { get; set; } = 0;
            }
        }
    }
}