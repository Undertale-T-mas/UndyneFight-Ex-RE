using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex
{
    public static partial class GlobalResources
    {
        public static partial class Effects
        {
            public class TyndallShader : Shader
            {
                public Vector2 LightPos { get; set; } = Vector2.Zero;
                public float Time { get; set; } = 0;
                public float Distance { get; set; } = 5;
                public float Sampling { get; set; } = 1;
                public TyndallShader(Effect eff) : base(eff)
                {
                    StableEvents = (x) =>
                    {
                        x.Parameters["iLightPosY"].SetValue(LightPos.Y);
                        x.Parameters["iLightPosX"].SetValue(LightPos.X);
                        x.Parameters["iDistance"].SetValue(Distance);
                        x.Parameters["iSampling"].SetValue(Sampling);
                    };
                }
            }
        }
    }
}