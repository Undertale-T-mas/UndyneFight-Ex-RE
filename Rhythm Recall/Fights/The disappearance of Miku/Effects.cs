using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class MikuFight
    {
        public static class EffectLibrary
        {
            public class BackGround : RenderProduction
            {
                Shader s0, s1;
                public BackGround() : base(null, SpriteSortMode.Immediate, BlendState.Opaque, 0.0f)
                {
                    s0 = new Shader(Loader.Load<Effect>("Fights\\Miku\\Shader0"));
                    s1 = new Shader(Loader.Load<Effect>("Fights\\Miku\\BackShader1"));
                }
                float time = 0;
                public override RenderTarget2D Draw(RenderTarget2D obj)
                {
                    time += 1.25f;

                    Shader = s0;
                    Shader.Parameters["del"].SetValue(Sin(time) * 0.1f + 0.1f);
                    MissionTarget = HelperTarget;
                    DrawTexture(obj, obj.Bounds);

                    Shader = s1;
                    MissionTarget = obj;
                    Shader.Parameters["r1"].SetValue(Sin(time * 0.2f + 0.5f) * 0.2f + 262.2f);
                    Shader.Parameters["r2"].SetValue(new Vector2(
                        Cos(time * 0.24f) * 0.1f + 521.1f,
                        Cos(time * 0.267f) * 0.3f + 351.2f));
                    DrawTexture(HelperTarget, obj.Bounds);

                    return MissionTarget;
                }
            }
        }
    }
}