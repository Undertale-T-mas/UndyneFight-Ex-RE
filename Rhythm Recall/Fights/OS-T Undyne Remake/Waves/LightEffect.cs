using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            internal class LightEffect : RenderProduction
            {
                private readonly Shader shader;
                public LightEffect() : base(null, SpriteSortMode.Immediate, BlendState.Opaque, 0)
                {
                    shader = new Shader(Loader.Load<Effect>("Fights\\OS-T Remake\\FinalShader"))
                    {
                        StableEvents = (s) =>
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                float angle;
                                Vector2 speed = GetVector2(Rand(40, 100) / 10f, angle = Rand(0, 359));
                                CreateEntity(new Particle(Color.Black * (Rand(7, 10) / 10f), speed, Rand(8, 16), GetVector2(500, angle + 180) + new Vector2(320, 240), FightResources.Sprites.square)
                                {
                                    DarkingSpeed = 4.5f
                                });
                            }
                            s.Parameters["range"].SetValue(lightDistance);
                            s.Parameters["time"].SetValue(Gametime / 15f);
                            s.Parameters["sinMul0"].SetValue(Cos(Gametime / 1.2f + 25) * 0.34f - Cos(Gametime * 0.67f + 14) * 0.05f);
                            s.Parameters["sinMul1"].SetValue(Sin(Gametime / 1.2f + 32) * 0.34f + Cos(Gametime * 0.57f + 4) * 0.09f);
                        }
                    };
                }

                public override RenderTarget2D Draw(RenderTarget2D obj)
                {
                    Shader = shader;
                    MissionTarget = obj;
                    DrawTexture(obj, new Vector2(0));
                    return obj;
                }
            }
        }
    }
}
