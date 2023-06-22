using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public partial class Arrow
    {
        private class BreakArrow : Entity
        {
            private static readonly Vector2[] shardPosition = new Vector2[6] {
                new(11, 2),
                new(3, 5),
                new(8, 5),
                new(0, 8),
                new(0, 0),
                new(2, 3)
            };
            public BreakArrow(float speed, float missionRot, int color, int rotatingType, Vector2 position, float scale)
            {
                Scale = scale;
                UpdateIn120 = true;
                Centre = position;
                Rotation = missionRot;
                this.speed = GetVector2(speed / 3, missionRot + 180);
                this.color = color;
                this.rotatingType = rotatingType;
                appearTime = 0;
                Depth = 0.5f - color / 200f;
            }

            private Vector2 speed;
            private readonly int color, rotatingType;
            private int appearTime = 0;

            public override void Update()
            {
                appearTime++;
                Centre += speed;
                if (appearTime >= 4)
                {
                    Dispose();
                    return;
                }
                Image = FightResources.Sprites.arrow[color, rotatingType, appearTime];
            }

            public override void Draw()
            {
                FormalDraw(Image, Centre, new Color(1.0f, 1.0f, 1.0f, 0.5f), Scale, GetRadian(Rotation), ImageCentre);
            }
            public override void Dispose()
            {
                for (int i = 0; i < 6; i++)
                {
                    Texture2D image = FightResources.Sprites.arrowShards[color, rotatingType, i];
                    Vector2 pos = Centre + Rotate(shardPosition[i] - ImageCentre + image.Bounds.Size.ToVector2() / 2, Rotation) * Scale;
                    if (shardPosition[i].Y > ImageCentre.Y / 2)
                    {
                        GameStates.InstanceCreate(new ArrowPiece(
                            GetVector2(Rand(1.1f, 2.6f) / 1.5f, Rand(-28, 28) + Rotation + 90) * Scale,
                            pos,
                            Rotation, image, Scale
                        ));
                    }
                    else
                    {
                        GameStates.InstanceCreate(new ArrowPiece(
                            GetVector2(Rand(1.1f, 2.6f) / 1.5f, Rand(-28, 28) + Rotation - 90) * Scale,
                            pos,
                            Rotation, image, Scale
                        ));
                    }
                }
                base.Dispose();
            }
        }
    }
}