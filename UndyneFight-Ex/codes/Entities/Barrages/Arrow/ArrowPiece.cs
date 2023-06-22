using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public partial class Arrow
    {
        public class ArrowPiece : Entity
        {
            public override void Draw()
            {
                FormalDraw(Image, Centre, new Color(1, 1, 1, 0.5f) * alp, Scale, GetRadian(Rotation), ImageCentre);
            }

            public override void Update()
            {
                Centre += speed;
                Rotation += rotateSpeed;
                alp -= fadeSpeed;
                if (alp < 0)
                    Dispose();
            }

            private Vector2 speed;
            private readonly float rotateSpeed, fadeSpeed;
            private float alp = 1f;


            public ArrowPiece(Vector2 speed, Vector2 pos, float rotation, Texture2D image, float scale)
            {
                Scale = scale;
                UpdateIn120 = true;
                Depth = 0.5f;
                fadeSpeed = Rand(0.08f, 0.14f) * 0.85f;
                Rotation = rotation;
                Image = image;
                Centre = pos;
                this.speed = speed * 1.2f;
                rotateSpeed = Rand(2.5f, 4.5f) * RandSignal();
            }
        }
    }
}