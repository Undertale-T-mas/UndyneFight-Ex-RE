using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall
{
    internal class BlockMaker : GameObject
    {
        public BlockMaker() { if (instance != null) instance.Dispose(); instance = this; }
        static BlockMaker instance;
        private int next = 0;
        private bool type = false;
        public override void Update()
        {
            if (next <= 0)
            {
                type = !type;
                next = Rand(20, 25);
                if (type)
                {
                    AddChild(new Block(90, Rand(120, 160)));
                    AddChild(new Block(270, Rand(800, 840)));
                }
                else
                {
                    AddChild(new Block(90, Rand(40, 80)));
                    AddChild(new Block(270, Rand(880, 920)));
                }
            }
            next--;
        }
    }
    internal class Block : Entity
    {
        public Block(float direction, float y)
        {
            rotateSpeed = Rand(4.5f, 7.5f) * RandSignal() * 0.3f;
            movingSpeed = Rand(6.5f, 7f);
            this.direction = direction;
            startingPos = (this.direction / 90) switch
            {
                0 => new Vector2(-50, y),
                1 => new Vector2(y, -50),
                2 => new Vector2(960 + 50, y),
                3 => new Vector2(y, 720 + 50),
                _ => throw new Exception()
            };
            Image = Resources.BlockTexture;
            Depth = 0.005f;

            float a, b;
            switch (Rand(0, 1) * 2)
            {
                case 0:
                    positionRoute = (s) => { return new(s * 0.7f, 0); };
                    break;
                case 1:
                    positionRoute = (s) => { return new(MathF.Pow(1.5f, s * 0.01f), 0); };
                    break;
                case 2:
                    a = Rand(1.2f, 3.2f) / 2; b = Rand(12f, 18f);
                    positionRoute = (s) => { return new(s * 0.7f, Sin(s * 0.7f * a) * b); };
                    break;
                case 3:
                    a = Rand(440, 760f); b = Rand(5.4f, 9.6f);
                    positionRoute = (s) => { return new Vector2(MathF.Pow(0.995f, s % a) * b, 0) + delta; };
                    break;
            }
        }

        private readonly Func<float, Vector2> positionRoute;

        private readonly float direction, rotateSpeed, movingSpeed;
        private Vector2 delta, startingPos;
        private float appearTime = 0;

        public override void Draw()
        {
            FormalDraw(Image, Centre, Color.White * 0.3f, MathUtil.GetRadian(Rotation), ImageCentre);
        }

        private Vector2 Transform(Vector2 origin)
        {
            return (direction / 90) switch
            {
                0 => origin,
                1 => new Vector2(-origin.Y, origin.X),
                2 => -origin,
                3 => new Vector2(origin.Y, -origin.X),
                _ => throw new Exception()
            };
        }

        public override void Update()
        {
            this.
            appearTime++;
            Rotation += rotateSpeed;
            delta = positionRoute.Invoke(appearTime * movingSpeed);
            Centre = startingPos + Transform(delta);
            if (!new CollideRect(-160, -160, 1080, 1090).Contain(Centre)) Dispose();
            if (appearTime % 6 == 0) AddChild(new BlockKid(Centre, Rotation));
        }

        private class BlockKid : Entity
        {
            public BlockKid(Vector2 pos, float rot)
            {
                Rotation = rot;
                Centre = pos;
                Image = Resources.BlockTail;
            }

            private float alpha = 1.0f;
            private float realAlpha = 0.0f;
            public override void Draw()
            {
                FormalDraw(Image, Centre, Color.White * 0.3f * realAlpha, MathUtil.GetRadian(Rotation), ImageCentre);
            }

            public override void Update()
            {
                alpha -= 0.02f;
                realAlpha = realAlpha * 0.05f + alpha * 0.95f;
            }
        }
    }
}
