using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex.Entities
{
    public abstract class FightBox : Entity
    {
        public virtual Vector2[] Vertexs { get; set; }
        public static FightBox instance;
        public static List<FightBox> boxs = new();
        public abstract void MoveTo(object v);
        public abstract void InstanceMove(object v);

        protected readonly Player.Heart detect;
        public Player.Heart Detect => detect;

        public Vector2[] MissionVertexs { get; protected set; }

        public float GreenSoulAlpha { get; set; } = 0.5f;
        public float InstantSetAlpha(float alpha) => GreenSoulAlpha = curAlpha = alpha;
        float curAlpha = 1.0f;

        public override void Update()
        {
            if (MissionVertexs == null) return;
            float scale = MovingScale * 0.6f;
            for (int i = 0; i < Vertexs.Length; i++)
                Vertexs[i] = Vertexs[i] * (1 - scale) + MissionVertexs[i] * scale;

            curAlpha = detect != null && detect.SoulType == 1
                ? curAlpha * 0.9f + GreenSoulAlpha * 0.1f : curAlpha * 0.9f + 1 * 0.1f;
        }
        public override void Draw()
        {
            Vector2 gravity = Vector2.Zero;
            for (int i = 0; i < Vertexs.Length; i++)
                gravity += Vertexs[i] / Vertexs.Length;
            Vector2[] positions = new Vector2[Vertexs.Length];
            for (int i = 0; i < Vertexs.Length; i++)
            {
                Vector2 delta = Vertexs[i] - gravity;
                delta = MathUtil.GetVector2(delta.Length() + 2f, MathF.Atan2(delta.Y, delta.X) * 180 / MathF.PI);
                positions[i] = delta + gravity;
            }
            for (int i = 0; i < Vertexs.Length - 1; i++)
            {
                DrawingLab.DrawLine(positions[i], positions[i + 1], 4.2f, GameMain.CurrentDrawingSettings.themeColor * curAlpha, 0.4f);
            }
            DrawingLab.DrawLine(positions[0], positions[Vertexs.Length - 1], 4.2f, GameMain.CurrentDrawingSettings.themeColor * curAlpha, 0.4f);
        }

        public FightBox()
        {
            UpdateIn120 = true;
            boxs.Add(this);
            instance = this;
        }
        public FightBox(Player.Heart p) : this() { detect = p; }

        public float MovingScale { get; set; } = 0.15f;
    }
    public class VertexBox : FightBox
    {
        public override void Draw()
        {
        }

        public override void InstanceMove(object v)
        {
            throw new NotImplementedException();
        }

        public override void MoveTo(object v)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            base.Update();
        }
    }
    public class RectangleBox : FightBox
    {
        public override void Dispose()
        {
            foreach (var v in gravityLines)
            {
                v?.Dispose();
            }
            boxs.Remove(this);
            base.Dispose();
        }

        public RectangleBox(CollideRect Area)
        {
            collidingBox = Area;
        }

        public RectangleBox(Player.Heart p) : base(p)
        {
            collidingBox = new CollideRect(0, 0, 640, 480);
            gravityLines[0] = right;
            gravityLines[1] = down;
            gravityLines[2] = left;
            gravityLines[3] = up;
        }

        public override void Draw()
        {
            base.Draw();
            return;
            /*
            CollideRect real = this.collidingBox;
            real.X -= 1; real.Y -= 1;
            real.Width += 2; real.Height += 2;
            DrawingLab.DrawRectangle(real, GameMain.themeColor, 4.2f, 0.4f);
            */
        }

        public override Vector2[] Vertexs { get; set; } = new Vector2[4];

        public override void Update()
        {
            Vector2 v1 = collidingBox.TopLeft,
                    v2 = new(collidingBox.Right, collidingBox.Up),
                    v3 = new(collidingBox.Left, collidingBox.Down),
                    v4 = new(collidingBox.Right, collidingBox.Down);
            up.SetPosition(v1, v2);
            right.SetPosition(v2, v4);
            left.SetPosition(v3, v1);
            down.SetPosition(v4, v3);
            Vertexs[0] = v1;
            Vertexs[1] = v2;
            Vertexs[3] = v3;
            Vertexs[2] = v4;

            base.Update();
            collidingBox = new CollideRect(Vertexs[0], Vertexs[2] - Vertexs[0]);

            if (detect == null) return;
            bool[] enabled = { false, false, false, false };
            if (detect.SoulType == 2 || detect.SoulType == 5)
            {
                enabled[detect.YFacing] = true;
            }
            else for (int i = 0; i < 4; i++)
                    enabled[i] = true;
            for (int i = 0; i < 4; i++)
                gravityLines[i].enabled = enabled[i];
        }

        private readonly GravityLine[] gravityLines = new GravityLine[4];
        private readonly GravityLine up = new(Vector2.Zero, Vector2.Zero);
        private readonly GravityLine down = new(Vector2.Zero, Vector2.Zero);
        private readonly GravityLine left = new(Vector2.Zero, Vector2.Zero);
        private readonly GravityLine right = new(Vector2.Zero, Vector2.Zero);

        public override void MoveTo(object cl)
        {
            MissionVertexs = ((CollideRect)cl).GetVertexs();
        }
        public override void InstanceMove(object cl)
        {
            collidingBox = (CollideRect)(cl);
            Vertexs = MissionVertexs = collidingBox.GetVertexs();
        }

        public float Left => collidingBox.X;
        public float Up { get => collidingBox.Y; set => InstanceMove(new CollideRect(Left, value, Width, Down - value)); }
        public float Right => collidingBox.Right;
        public float Down { get => collidingBox.Down; set => InstanceMove(new CollideRect(Left, Up, Width, value - Up)); }

        public float Width => Right - Left;
        public float Height => Down - Up;
    }
}