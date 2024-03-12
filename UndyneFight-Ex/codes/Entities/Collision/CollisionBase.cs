using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.DrawingLab;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex
{
    public interface CollidingComponent
    {
        public bool CollideWith(CollidingComponent _component);
    }
    internal static class CheckCollision
    {
        internal static Dictionary<Type, Dictionary<Type, Func<CollidingComponent, CollidingComponent, bool>>> collisionCheck =
            new();
        internal static void Initialize()
        {

        }
        internal static bool TakeCheck(CollidingComponent s1, CollidingComponent s2)
        {
            return collisionCheck[s1.GetType()].ContainsKey(s2.GetType())
                ? collisionCheck[s1.GetType()][s2.GetType()].Invoke(s1, s2)
                : collisionCheck[s2.GetType()].ContainsKey(s1.GetType())
                ? collisionCheck[s2.GetType()][s1.GetType()].Invoke(s2, s1)
                : throw new NotImplementedException();
        }
        internal static bool CircleWithCircle(CollidingCircle s1, CollidingCircle s2)
        {
            float dist = GetDistance(s1.position, s2.position);
            return dist <= s1.radius + s2.radius && dist >= MathF.Abs(s1.radius - s2.radius);
        }
        internal static bool CircleWithSegment(CollidingCircle s1, CollidingSegment s2)
        {
            Vector2 circlePos = s1.position, lineVec = s2.v2 - s2.v1, circleTop = circlePos - s2.v1;
            float redist = Vector2.Dot(circleTop, lineVec / lineVec.Length());
            float dist = MathF.Sqrt(Vector2.Dot(circlePos, circlePos) - (redist * redist));
            return dist <= s1.radius && GetDistance((s2.v2 + s2.v1) / 2, s1.position) <= lineVec.Length() / 2;
        }
    }
    public struct CollidingSegment : CollidingComponent
    {
        internal Vector2 v1, v2;
        public CollidingSegment(Vector2 v1, Vector2 v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
        public CollidingSegment(Vector2 centre, float length, float rotation)
        {
            v1 = centre + GetVector2(length / 2, rotation);
            v2 = centre - GetVector2(length / 2, rotation);
        }

        public bool CollideWith(CollidingComponent _component)
        {
            return CheckCollision.TakeCheck(this, _component);
        }
    }
    public struct CollidingCircle : CollidingComponent
    {
        internal Vector2 position;
        internal float radius;
        public CollidingCircle(Vector2 position, float radius)
        {
            this.position = position;
            this.radius = radius;
        }
        public bool CollideWith(CollidingComponent _component)
        {
            return CheckCollision.TakeCheck(this, _component);
        }
    }

    public class GravityLine
    {
        public static void Reload()
        {
            reloadTime = 4;
        }
        public static void Recover()
        {
            reloadTime--;
        }

        private static int reloadTime = 0;
        public bool enabled = true;

        private bool IsEnable => reloadTime <= 0 && enabled;

#if DEBUG
        private Vector2 v1, v2;
#endif
        public static HashSet<GravityLine> GravityLines = new();

        public void Dispose()
        {
            GravityLines.Remove(this);
        }

        public void SetPosition(Vector2 v1, Vector2 v2)
        {
#if DEBUG
            this.v1 = v1;
            this.v2 = v2;
#endif
            Vector2 v = (v1 + v2) / 2, old = Centre;
            Centre = v;
            float old_ = rotation;
            rotation = MathF.Atan2(v1.Y - v2.Y, v1.X - v2.X);
            float detla = rotation - old_;
            if (v1.X == v2.X)
            {
                A = 1;
                B = 0;
                C = -Centre.X;
            }
            else
            {
                float k = (v1.Y - v2.Y) / (v1.X - v2.X);
                A = k;
                B = -1;
                C = (-A * Centre.X) - (B * Centre.Y);
            }
            if (isCollide && Heart.SoulType == 2)
            {
                collidePlayers.ForEach(s =>
                {
                    Vector2 oldPos = s.Centre;
                    float dx = s.Centre.X - Centre.X, dy = s.Centre.Y - Centre.Y;
                    Vector2 detla_ = v - old;
                    if (Math.Abs(detla) > 1e-5f)
                    {
                        float ori = MathF.Atan2(dy, dx);
                        float length = GetDistance(Centre, s.Centre);
                        detla_ += Centre + GetVector2(length, (ori + detla) / PI * 180) - s.Centre;
                    }
                    if (sticky)
                        s.Centre += detla_;
                    else
                    {
                        Vector2 v_ = new(MathF.Cos(NormalRotation), MathF.Sin(NormalRotation));
                        if (detla_.Length() > 0.001f)
                            s.Centre += v_ * Cos(v_, detla_) * detla_.Length();
                    }
                });
            }
            collidePlayers.Clear();
        }

        public void SetLength(float length)
        {
            this.length = length;
        }
        public void SetWidth(float width)
        {
            this.width = width;
        }

        public GravityLine(Vector2 v1, Vector2 v2)
        {
#if DEBUG
            this.v1 = v1;
            this.v2 = v2;
#endif
            GravityLines.Add(this);
            Centre = (v1 + v2) / 2;
            rotation = MathF.Atan2(v1.Y - v2.Y, v1.X - v2.X);
            if (v1.Y == v2.Y)
            {
                A = 1;
                B = 0;
                C = -Centre.X;
            }
            else
            {
                float k = (v1.Y - v2.Y) / (v1.X - v2.X);
                A = k;
                B = -1;
                C = (-A * Centre.X) - (B * Centre.Y);
            }
        }

        private float A, B, C, length = 1000, width = 0, rotation = 0;
        private Vector2 Centre;

        public float Rotation => rotation;
        public float NormalRotation => rotation - (PI / 2f);

        private float Distance(Player.Heart heart) => (float)(((A * heart.Centre.X) + (B * heart.Centre.Y) + C) / Math.Sqrt((A * A) + (B * B)));

        private bool isCollide;
        public bool sticky = true;
        private readonly List<Player.Heart> collidePlayers = new();

        public bool IsCollideWith(Player.Heart player)
        {
            if (!IsEnable) return false;
            if (isCollide = Math.Abs(Distance(player)) <= 8.01f + width && GetDistance(player.Centre, Centre) <= ((length / 2) + 6))
            {
                float dx = player.Centre.X - Centre.X, dy = player.Centre.Y - Centre.Y;
                Vector2 v1 = new(dx, dy);
                Vector2 v2 = GetVector2(1, player.Rotation - 90);
                if (Vector2.Dot(v1, v2) < 0)
                {
                    isCollide = false;
                    goto A;
                }
                collidePlayers.Add(player);
                return true;
            }
        A: return false;
        }
        public Vector2 CorrectPosition(Player.Heart player)
        {
            Vector2 v_ = new(MathF.Cos(NormalRotation), MathF.Sin(NormalRotation));
            float dx = player.Centre.X - Centre.X, dy = player.Centre.Y - Centre.Y;
            Vector2 v1 = new(dx, dy);
            float dist = Cos(v1, v_) * v1.Length();
            float detla = 8 + width - dist;
            Vector2 change = v_ * detla * 0.25f;
            return change;
        }
        public void Draw()
        {
#if DEBUG
            if (!ModeLab.showCollide) return;
            if (!IsEnable)
                DrawLine(v1, v2, 2, Color.Red, 0.999f);
            else if (!isCollide)
                DrawLine(v1, v2, 2, Color.Green, 0.999f);
            else
                DrawLine(v1, v2, 2, Color.Gold, 0.999f);
            DrawVector(Centre, NormalRotation);
#endif
        }
    }

    public struct CollideRect : CollidingComponent
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2[] GetVertexs()
        {
            return new Vector2[] { new(Left, Up), new(Right, Up), new(Right, Down), new(Left, Down) };
        }
        public bool CollideWith(CollidingComponent _component)
        {
            return CheckCollision.TakeCheck(this, _component);
        }
        public CollideRect(float X, float Y, float Width, float Height)
        {
            this.Width = Width;
            this.Height = Height;
            this.X = X;
            this.Y = Y;
        }
        public CollideRect(Vector2 pos, Vector2 size) : this(pos.X, pos.Y, size.X, size.Y) { }
        public CollideRect(Rectangle rec) : this(rec.X, rec.Y, rec.Width, rec.Height) { }

        public void Offset(Vector2 vect) { X += vect.X; Y += vect.Y; }

        public Vector2 GetCentre()
        {
            return new Vector2(X + (Width / 2), Y + (Height / 2));
        }

        public void SetCentre(Vector2 Centre)
        {
            Vector2 detla = Centre - GetCentre();
            Offset(detla);
        }
        public void SetCentre(float X, float Y)
        {
            Vector2 detla = new Vector2(X, Y) - GetCentre();
            Offset(detla);
        }

        public bool Intersects(CollideRect collideRectAno)
        {
            Vector2 C1 = GetCentre(), C2 = collideRectAno.GetCentre();
            float X_Max = (Width / 2) + (collideRectAno.Width / 2);
            float Y_Max = (Height / 2) + (collideRectAno.Height / 2);
            return Math.Abs((C1 - C2).X) <= X_Max && Math.Abs((C1 - C2).Y) <= Y_Max;
        }

        public bool Contain(Vector2 vect)
        {
            Vector2 anyz = GetCentre() - vect;
            return Math.Abs(anyz.X) <= Width / 2 && Math.Abs(anyz.Y) <= Height / 2;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }

        public Vector2 BottomLeft
        {
            get
            {
                return new Vector2(X, Y + Size.Y);
            }
            set
            {
                X = value.X; Y = value.Y - Size.Y;
            }
        }
        public Vector2 BottomRight
        {
            get
            {
                return new Vector2(X + Size.X, Y + Size.Y);
            }
            set
            {
                X = value.X - Size.X; Y = value.Y - Size.Y;
            }
        }
        public Vector2 TopLeft
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X; Y = value.Y;
            }
        }
        public Vector2 TopRight
        {
            get
            {
                return new Vector2(X + Size.X, Y);
            }
            set
            {
                X = value.X - Size.X; Y = value.Y;
            }
        }
        /// <summary>
        /// 尺寸设置和读取。读取时得到一个x=width,y=height的向量，设置时左上角xy位置不变，width和height分别改成所给向量的xy
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(Width, Height);
            }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }
        public static implicit operator Rectangle(CollideRect rect) => rect.ToRectangle();
        public static implicit operator CollideRect(Rectangle rect) => new(rect);

        public float Up => Y;
        public float Down => Y + Height;
        public float Right => X + Width;
        public float Left => X;

        public static CollideRect operator +(CollideRect left, Vector2 right)
        {
            left.Offset(right);
            return left;
        }
        public static CollideRect operator -(CollideRect left, Vector2 right)
        {
            left.Offset(-right);
            return left;
        }
        public static CollideRect operator +(Vector2 left, CollideRect right)
        {
            right.Offset(left);
            return right;
        }

        /// <summary>
        /// 形容一个矩形和一个标量相乘，得到一个新的矩形，它的中心点和原矩形不变，而长和宽都是那个标量倍。
        /// </summary>
        /// <param name="left">矩形</param>
        /// <param name="right">标量</param>
        public static CollideRect operator *(CollideRect left, float right)
        {
            Vector2 vect = left.GetCentre();
            return new CollideRect(vect.X - (left.Width * right / 2), vect.Y - (left.Height * right / 2), left.Width * right, left.Height * right);
        }
    }
}