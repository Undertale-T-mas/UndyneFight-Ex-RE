using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace UndyneFight_Ex.Entities
{
    public class BoxVertex
    {
        public Vector2 CurrentPosition { get; set; }
        public Vector2 MissionPosition { get; set; }

        public float ToMissionDistance => (CurrentPosition - MissionPosition).Length();

        public int ID
        {
            get
            {
                if (_id == -1) this._id = Previous == null ? 0 : Previous.ID + 1;
                return this._id;
            }
        }
        private int _id = -1;

        public BoxVertex(Vector2 pos)
        {
            this.CurrentPosition = this.MissionPosition = pos; 
        }
        public BoxVertex() { }

        public BoxVertex Next { get; set; }
        public BoxVertex Previous { get; set; }

        public void Move(float scale)
        {
            this.CurrentPosition = CurrentPosition * (1 - scale) + MissionPosition * scale;
        }
        public void InstantMove(Vector2 position)
        {
            this.CurrentPosition = this.MissionPosition = position;
        }
        private LinkedList<BoxVertex> GetAll(LinkedList<BoxVertex> prev)
        {
            if(prev.First.Value == this) return prev;
            prev.AddLast(this);
            GetAll(prev);
            return prev;
        }
        public BoxVertex[] GetAll() => this.GetAll(new()).ToArray();
    }

    public abstract class FightBox : Entity
    {
        public BoxVertex[] Vertexs { get; set; }
        public static FightBox instance;
        public static List<FightBox> boxs = new();
        public abstract void MoveTo(object v);
        public abstract void InstanceMove(object v);

        protected readonly Player.Heart detect;
        public Player.Heart Detect => detect; 

        public float GreenSoulAlpha { get; set; } = 0.5f;
        public float InstantSetAlpha(float alpha) => GreenSoulAlpha = curAlpha = alpha;
        float curAlpha = 1.0f;

        public override void Update()
        {
            curAlpha = detect != null && detect.SoulType == 1
                ? curAlpha * 0.9f + GreenSoulAlpha * 0.1f : curAlpha * 0.9f + 1 * 0.1f;
            if (Vertexs == null) return;
            float scale = MovingScale * 0.6f;
            for (int i = 0; i < Vertexs.Length; i++)
            {
                float scaleBuff = MathF.Max(0, 1 - scale - Vertexs[i].ToMissionDistance);
                Vertexs[i].Move(scale + scaleBuff);// = Vertexs[i] * (1 - ()) + MissionVertexs[i] * (scale + scaleBuff);
            }
        }
        public override void Draw()
        {
            Vector2 gravity = Vector2.Zero;
            for (int i = 0; i < Vertexs.Length; i++)
                gravity += Vertexs[i].CurrentPosition / Vertexs.Length;
            Vector2[] positions = new Vector2[Vertexs.Length];
            for (int i = 0; i < Vertexs.Length; i++)
            {
                Vector2 delta = Vertexs[i].CurrentPosition - gravity;
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
        public FightBox(Player.Heart p) : this() {
            detect = p; 
        }

        public float MovingScale { get; set; } = 0.15f;
    }

    public class VertexBox : FightBox
    {
        public VertexBox(Player.Heart heart, RectangleBox rectangleBox) : base(heart)
        { 
            this.Vertexs = new BoxVertex[4];

            this.Vertexs[0] = new(rectangleBox.CollidingBox.TopRight);
            this.Vertexs[1] = new(rectangleBox.CollidingBox.BottomRight);
            this.Vertexs[2] = new(rectangleBox.CollidingBox.BottomLeft);
            this.Vertexs[3] = new(rectangleBox.CollidingBox.TopLeft); 
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void InstanceMove(object v)
        {
            if (v is not Vector2[]) throw new ArgumentException($"{nameof(v)} has to be an vector array");

            Vector2[] temp = v as Vector2[];
            if(temp.Length != Vertexs.Length) 
                throw new ArgumentOutOfRangeException($"{nameof(v)} must be in same length with vertex count");

            for (int i = 0; i < temp.Length; i++)
                this.Vertexs[i].InstantMove(temp[i]);
        }

        public override void MoveTo(object v)
        {
            if (v is not Vector2[]) throw new ArgumentException($"nameof(v) has to be an vector array");

            Vector2[] temp = v as Vector2[];
            if (temp.Length != Vertexs.Length)
                throw new ArgumentOutOfRangeException($"{nameof(v)} must be in same length with vertex count");

            for (int i = 0; i < temp.Length; i++)
                this.Vertexs[i].MissionPosition = temp[i];
        }

        public int Split(int originID, float scale)
        {
            if (scale < 0 || scale > 1) throw new ArgumentOutOfRangeException($"{nameof(scale)} has to be in [0, 1]");
            if (originID != this.Vertexs.Length - 1) {
                BoxVertex a = this.Vertexs[originID], b = this.Vertexs[originID + 1];
                Vector2 pos = Vector2.Lerp(a.CurrentPosition, b.CurrentPosition, scale);

                BoxVertex[] temp = new BoxVertex[Vertexs.Length + 1]; 

                temp = new BoxVertex[Vertexs.Length + 1];
                Array.Copy(Vertexs, 0, temp, 0, originID + 1);
                temp[originID + 1] = new(pos);
                Array.Copy(Vertexs, originID + 1, temp, originID + 2, Vertexs.Length - originID - 1);

                Vertexs = temp;
            }
            else
            {
                BoxVertex a = this.Vertexs[originID], b = this.Vertexs[0];
                Vector2 pos = Vector2.Lerp(a.CurrentPosition, b.CurrentPosition, scale);

                BoxVertex[] temp = new BoxVertex[Vertexs.Length + 1];  
                
                temp = new BoxVertex[Vertexs.Length + 1];
                Array.Copy(Vertexs, temp, Vertexs.Length);
                temp[originID + 1] = new(pos);

                Vertexs = temp; 
            }

            return originID + 1;
        }
        public int Split(int originID, float[] scales)
        {
            if (scales == null || scales.Length <= 0) throw new ArgumentOutOfRangeException($"{nameof(scales)} has to be in [0, 1]");
            if (originID != this.Vertexs.Length - 1) {
                BoxVertex a = this.Vertexs[originID], b = this.Vertexs[originID + 1];
                Vector2 pos = Vector2.Lerp(a.CurrentPosition, b.CurrentPosition, scale);

                BoxVertex[] temp = new BoxVertex[Vertexs.Length + 1]; 

                temp = new BoxVertex[Vertexs.Length + 1];
                Array.Copy(Vertexs, 0, temp, 0, originID + 1);
                temp[originID + 1] = new(pos);
                Array.Copy(Vertexs, originID + 1, temp, originID + 2, Vertexs.Length - originID - 1);

                Vertexs = temp;
            }
            else
            {
                BoxVertex a = this.Vertexs[originID], b = this.Vertexs[0];
                Vector2 pos = Vector2.Lerp(a.CurrentPosition, b.CurrentPosition, scale);

                BoxVertex[] temp = new BoxVertex[Vertexs.Length + 1];  
                
                temp = new BoxVertex[Vertexs.Length + 1];
                Array.Copy(Vertexs, temp, Vertexs.Length);
                temp[originID + 1] = new(pos);

                Vertexs = temp; 
            }

            return originID + 1;
        }

        public void SetPosition(int originID, Vector2 position)
        {
            this.Vertexs[originID].MissionPosition = position;
        }

        public override void Update()
        {
            base.Update();
            float x1 = Vertexs[0].CurrentPosition.X, x2 = Vertexs[0].CurrentPosition.X, 
                  y1 = Vertexs[0].CurrentPosition.Y, y2 = Vertexs[0].CurrentPosition.Y;
            for(int i =  1; i < Vertexs.Length; i++)
            {
                x1 = MathF.Min(x1, Vertexs[i].CurrentPosition.X);
                x2 = MathF.Max(x2, Vertexs[i].CurrentPosition.X);
                y1 = MathF.Min(y1, Vertexs[i].CurrentPosition.Y);
                y2 = MathF.Max(y2, Vertexs[i].CurrentPosition.Y);
            }
            this.collidingBox = new(x1, y1, x2 - x1, y2 - y1);
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
            this.Vertexs = new BoxVertex[4]; for (int i = 0; i < 4; i++) this.Vertexs[i] = new();
            this.InstanceMove(Area);
            collidingBox = Area;
        }

        public RectangleBox(Player.Heart p) : base(p)
        {
            this.Vertexs = new BoxVertex[4]; for (int i = 0; i < 4; i++) this.Vertexs[i] = new();
            collidingBox = new CollideRect(0, 0, 640, 480);
            gravityLines[0] = right;
            gravityLines[1] = down;
            gravityLines[2] = left;
            gravityLines[3] = up;
        }
        public RectangleBox(Player.Heart p, CollideRect area) : base(p)
        {
            this.Vertexs = new BoxVertex[4]; for (int i = 0; i < 4; i++) this.Vertexs[i] = new();
            collidingBox = area;
            this.InstanceMove(area);
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
            Vertexs[0].CurrentPosition = v1;
            Vertexs[1].CurrentPosition = v2;
            Vertexs[3].CurrentPosition = v3;
            Vertexs[2].CurrentPosition = v4;

            base.Update();
            collidingBox = new CollideRect(Vertexs[0].CurrentPosition, Vertexs[2].CurrentPosition - Vertexs[0].CurrentPosition);

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
            Vector2[] temp = ((CollideRect)cl).GetVertexs();
            for(int i = 0; i < temp.Length; i++)
                Vertexs[i].MissionPosition = temp[i];
        }
        public override void InstanceMove(object cl)
        {
            Vector2[] temp = ((CollideRect)cl).GetVertexs();
            for (int i = 0; i < temp.Length; i++)
                Vertexs[i].InstantMove(temp[i]);
            this.collidingBox = (CollideRect)cl;
        }

        public float Left => collidingBox.X;
        public float Up { get => collidingBox.Y; set => InstanceMove(new CollideRect(Left, value, Width, Down - value)); }
        public float Right => collidingBox.Right;
        public float Down { get => collidingBox.Down; set => InstanceMove(new CollideRect(Left, Up, Width, value - Up)); }

        public float Width => Right - Left;
        public float Height => Down - Up;
    }
}