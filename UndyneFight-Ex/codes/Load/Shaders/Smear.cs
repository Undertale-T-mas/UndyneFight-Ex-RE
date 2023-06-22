using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class Smear : Shader
            {
                public Smear(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iReach"].SetValue(Reach);
                        x.Parameters["iRadius"].SetValue(Radius);
                        x.Parameters["iVectorStart"].SetValue(Start);
                        x.Parameters["iVectorEnd"].SetValue(End);
                    };
                }

                public float Reach { get; set; } = 11;
                public float Radius { get; set; } = 22;
                public Vector2 Start { get; set; } = Vector2.Zero;
                public Vector2 End { get; set; } = new Vector2(640, 480);
            }
        }
    }
}