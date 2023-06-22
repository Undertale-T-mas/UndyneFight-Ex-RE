using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Entities
{
    public partial class Player
    {
        public class BrokenHeart : Entity
        {
            private static Vector2 playerPos;
            private Color showingColor;
            public BrokenHeart()
            {
                playerPos = heartInstance.Centre;
                FightResources.Sounds.die1.CreateInstance().Play();
                showingColor = heartInstance.DrawingColor;
                Image = FightResources.Sprites.brokenHeart;
                Centre = heartInstance.Centre;
            }
            public override void Draw()
            {
                FormalDraw(Image, Centre, showingColor, 0.0f, ImageCentre);
            }

            private int appearTime = 0;
            public override void Update()
            {
                appearTime++;
                if (appearTime == 50)
                {
                    FightResources.Sounds.die2.CreateInstance().Play();
                    int c = MathUtil.GetRandom(4, 6);
                    for (int i = 0; i < c; i++)
                        GameStates.InstanceCreate(new HeartPiece(playerPos, showingColor));
                    Dispose();
                }
            }
        }

        public class HeartPiece : Entity
        {
            private static CollideRect screen = new CollideRect(-50, -50, 740, 580);
            private Vector2 speed;
            private readonly float rotateSpeed;
            private Color color;
            public HeartPiece(Vector2 startPos, Color color)
            {
                Depth = 0.9f;
                this.color = color;
                rotateSpeed = MathUtil.GetRandom(-40, 40) / 300f;
                Centre = startPos;
                speed.X = MathUtil.GetRandom(-40, 40) / 8f;
                speed.Y = MathUtil.GetRandom(-50, 20) / 8f;
                Image = FightResources.Sprites.heartPieces[MathUtil.GetRandom(0, 4)];
            }
            public override void Draw()
            {
                FormalDraw(Image, Centre, color, Rotation, ImageCentre);
            }
            public override void Dispose()
            {
                base.Dispose();
            }
            public override void Update()
            {
                if (!screen.Contain(Centre)) Dispose();
                Centre += speed;
                Rotation += rotateSpeed;
                speed.Y += 0.12f;
            }
        }
    }
}