using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            internal class SplitPartsPruduction : RenderProduction
            {
                Shader shader;
                public SplitPartsPruduction() : base(null, SpriteSortMode.Immediate, BlendState.Opaque, 0)
                {
                    shader = new Shader(Loader.Load<Effect>("Fights\\OS-T Remake\\PositionSplit"))
                    {
                        StableEvents = (s) =>
                        {
                            curSplitWidth = curSplitWidth * 0.82f + splitWidth * 0.18f;
                            s.Parameters["splitWidth"].SetValue(curSplitWidth);
                        }
                    };
                }

                public override RenderTarget2D Draw(RenderTarget2D obj)
                {
                    Shader = shader;
                    MissionTarget = obj;
                    DrawTexture(obj, new Vector2(0, 0));
                    return obj;
                }
            }
        }
    }
}