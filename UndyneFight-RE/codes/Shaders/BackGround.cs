using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace UndyneFight_Ex.Remake.Effects
{
    public class BackgroundShader : Shader
    {
        public Vector2 DeltaPosition { get; set; } = new Vector2(0.5f, 0.5f);
        public float Time { get; set; } = 0;
        public BackgroundShader(Effect eff) : base(eff)
        {
            StableEvents = (x) =>
            {
                time = Time * 0.02f;
                x.Parameters["iTime"].SetValue(time);
                x.Parameters["iDelta"].SetValue(DeltaPosition);
            };
        }
        float time = 0.0f;
        public void ResetTime()
        {
            this.time = 0;
        }
    }
}