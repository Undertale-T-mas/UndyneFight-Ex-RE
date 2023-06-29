using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex.Entities
{
    public interface ICustomMotion
    {
        Func<ICustomMotion, Vector2> PositionRoute { get; set; }
        Func<ICustomMotion, float> RotationRoute { get; set; }

        float[] RotationRouteParam { get; set; }
        float[] PositionRouteParam { get; set; }

        float AppearTime { get; }
        float Rotation { get; }
        Vector2 CentrePosition { get; }
    }

    public interface ICustomLength
    {
        float AppearTime { get; }
        Func<ICustomLength, float> LengthRoute { get; set; }
        float[] LengthRouteParam { get; set; }
    }

    public static class Motions
    {
        public static class RotationRoute
        {
            /// <summary>
            /// 正弦轨迹运动。其振幅波长和初相为参数0，1和2，函数均值为3
            /// </summary>
            public static Func<ICustomMotion, float> sin = (s) => { return s.RotationRouteParam[3] + (float)(s.RotationRouteParam[0] * Math.Sin((s.AppearTime + s.RotationRouteParam[2]) / s.RotationRouteParam[1] * Math.PI * 2)); };
            /// <summary>
            /// 线性旋转，旋转角速度为参数0，初始角度为参数1
            /// </summary>
            public static Func<ICustomMotion, float> linear = (s) => { return s.RotationRouteParam[0] * s.AppearTime + s.RotationRouteParam[1]; };
            public static Func<ICustomMotion, float> stableValue = (s) => { return s.RotationRouteParam[0]; };
        }
        public static class LengthRoute
        {
            /// <summary>
            /// 参数0为长度，阐释1位持续时间
            /// </summary>
            public static Func<ICustomLength, float> autoFold = (s) => { float dec = Math.Max(0, s.AppearTime - s.LengthRouteParam[1]); return s.LengthRouteParam[0] - dec * dec / 12f; };
            /// <summary>
            /// 正弦轨迹的三次方运动。其振幅波长和初相为参数0，1和2，函数均值为3
            /// </summary>
            public static Func<ICustomLength, float> sin3 = (s) => { return s.LengthRouteParam[3] + s.LengthRouteParam[0] * (float)Math.Pow(Math.Sin((s.AppearTime + s.LengthRouteParam[2]) / s.LengthRouteParam[1] * Math.PI * 2), 3); };
            public static Func<ICustomLength, float> stableValue = (s) => { return s.LengthRouteParam[0]; };
            /// <summary>
            /// 正弦轨迹运动。其振幅波长和初相为参数0，1和2，函数均值为3
            /// </summary>
            public static Func<ICustomLength, float> sin = (s) => { return s.LengthRouteParam[3] + (float)(s.LengthRouteParam[0] * Math.Sin((s.AppearTime + s.LengthRouteParam[2]) / s.LengthRouteParam[1] * Math.PI * 2)); };
        }
        public static class PositionRoute
        {
            public static Func<ICustomMotion, Vector2> stableValue = (s) => { return new Vector2(0, 0); };
            public static Func<ICustomMotion, Vector2> cameFromUp = (s) => { return new Vector2(0, 0 - (float)(Math.Pow(0.85, s.AppearTime) * 600)); };
            public static Func<ICustomMotion, Vector2> cameFromDown = (s) => { return new Vector2(0, 0 + (float)(Math.Pow(0.85, s.AppearTime) * 600)); };
            public static Func<ICustomMotion, Vector2> cameFromLeft = (s) => { return new Vector2(0 - (float)(Math.Pow(0.85, s.AppearTime) * 600), 0); };
            public static Func<ICustomMotion, Vector2> cameFromRight = (s) => { return new Vector2(0 + (float)(Math.Pow(0.85, s.AppearTime) * 600), 0); };
            /// <summary>
            /// 线性缓动，给定参数0,1分别表示在x和y方向上的速度。
            /// </summary>
            public static Func<ICustomMotion, Vector2> linear = (s) => { return new Vector2(s.PositionRouteParam[0] * s.AppearTime, s.PositionRouteParam[1] * s.AppearTime); };
            /// <summary>
            /// 沿着x轴以参数0速度匀速运动，沿着y轴按照正弦轨迹运动，其振幅波长和初相为参数1和2和3。
            /// </summary>
            public static Func<ICustomMotion, Vector2> XAxisSin = (s) => { return new Vector2(s.PositionRouteParam[0] * s.AppearTime, (float)(s.PositionRouteParam[1] * Math.Sin((s.AppearTime + s.PositionRouteParam[3]) / s.PositionRouteParam[2] * Math.PI * 2))); };
            /// <summary>
            /// 沿着y轴以参数0速度匀速运动，沿着x轴按照正弦轨迹运动，其振幅波长和初相为参数1和2和3。
            /// </summary>
            public static Func<ICustomMotion, Vector2> YAxisSin = (s) => { return new Vector2((float)(s.PositionRouteParam[1] * Math.Sin((s.AppearTime + s.PositionRouteParam[3]) / s.PositionRouteParam[2] * Math.PI * 2)), s.PositionRouteParam[0] * s.AppearTime); };
            /// <summary>
            /// 沿着y轴以参数0速度匀速运动，沿着x轴按照A * (2 * sin(wx+d) ^ 2 - 1)运动，其振幅波长和初相为参数1和2和3。
            /// </summary>
            public static Func<ICustomMotion, Vector2> YAxisSin2 = (s) =>
            {
                return new Vector2((float)(s.PositionRouteParam[1] *
                        (2 * Math.Pow(Math.Sin((s.AppearTime + s.PositionRouteParam[3]) / s.PositionRouteParam[2] * Math.PI * 1), 2) - 1)),
                        s.PositionRouteParam[0] * s.AppearTime);
            };
            /// <summary>
            /// 线性缓动，给定参数0,1,2分别表示距离.角速度,初始角度。
            /// </summary>
            public static Func<ICustomMotion, Vector2> circle = (s) =>
            {
                float alpha = s.AppearTime * s.PositionRouteParam[1] + s.PositionRouteParam[2];
                return new Vector2(s.PositionRouteParam[0] * Fight.Functions.Cos(alpha), s.PositionRouteParam[0] * Fight.Functions.Sin(alpha));
            };
            /// <summary>
            /// 沿着x轴以参数0为初速度参数1为加速度匀加速运动，沿着y轴按照正弦轨迹运动，其振幅波长和初相为参数1和2和3。
            /// </summary>
            public static Func<ICustomMotion, Vector2> XAccAxisSin = (s) => { return new Vector2(s.AppearTime * s.AppearTime / 2 * s.PositionRouteParam[1] + s.PositionRouteParam[0] * s.AppearTime, (float)(s.PositionRouteParam[2] * Math.Sin((s.AppearTime + s.PositionRouteParam[4]) / s.PositionRouteParam[3] * Math.PI * 2))); };
            /// <summary>
            /// 沿着x轴以参数0为初速度参数1为加速度匀加速运动，沿着y轴按照线性速度运动
            /// </summary>
            public static Func<ICustomMotion, Vector2> XAccYLinear = (s) => { return new Vector2(s.AppearTime * s.AppearTime / 2 * s.PositionRouteParam[1] + s.PositionRouteParam[0] * s.AppearTime, s.PositionRouteParam[2] * s.AppearTime); };

            /// <summary>
            /// 沿着y轴以参数0,1,2作为振幅波长初相运动，沿着x轴以参数3,4,5作为振幅波长初相运动
            /// </summary>
            public static Func<ICustomMotion, Vector2> XYAxisSin = (s) =>
            {
                float y = (float)(s.PositionRouteParam[0] * Math.Sin((s.AppearTime + s.PositionRouteParam[2]) / s.PositionRouteParam[1] * Math.PI * 2));
                float x = (float)(s.PositionRouteParam[3] * Math.Sin((s.AppearTime + s.PositionRouteParam[5]) / s.PositionRouteParam[4] * Math.PI * 2));
                return new Vector2(x, y);
            };

            /// <summary>
            /// 重力衰落。参数0为重力加速度，参数1为横向速度，参数2为当前纵向速度，它会被修改。
            /// </summary>
            public static Func<ICustomMotion, Vector2> GravityDown = (s) => { s.PositionRouteParam[2] += s.PositionRouteParam[0] / 60f; return new Vector2(s.CentrePosition.X + s.PositionRouteParam[1], s.CentrePosition.Y + s.PositionRouteParam[2]); };
        }
    }

    public class InstantEvent : GameObject
    {
        private readonly Action _action;
        private float _timeDelay;
        public InstantEvent(float timeDelay, Action action)
        {
            _timeDelay = (int)timeDelay;
            _action = action;
            UpdateIn120 = true;
        }
        public override void Update()
        {
            if (_timeDelay <= 0)
            {
                _action.Invoke();
                Dispose();
            }
            _timeDelay -= 0.5f;
        }
    }
    public class TimeRangedEvent : GameObject
    {
        private readonly Action _action;
        private float _timeDelay;
        private readonly float _duration;
        /// <summary>
        /// 能够持续执行一段时间的事件
        /// </summary>
        /// <param name="timeDelay">事件发生距离当时的帧数</param>
        /// <param name="duration">事件会执行几次。填写 0 不会执行！</param>
        /// <param name="action">给的事件</param>
        public TimeRangedEvent(float timeDelay, float duration, Action action)
        {
            _duration = (int)duration;
            _timeDelay = (int)timeDelay;
            _action = action;
        }
        /// <summary>
        /// 能够持续执行一段时间的事件
        /// </summary> 
        /// <param name="duration">事件会执行几次。填写 0 不会执行！</param>
        /// <param name="action">给的事件</param>
        public TimeRangedEvent(float duration, Action action)
        {
            _duration = (int)duration;
            _timeDelay = 0;
            _action = action;
        }
        public override void Update()
        {
            if (_timeDelay <= 0)
            {
                if (_timeDelay <= -_duration)
                {
                    Dispose(); return;
                }
                _action.Invoke();
            }
            if (!UpdateIn120)
                _timeDelay--;
            else _timeDelay -= 0.5f;
        }
    }

    public class BackGround : Entity
    {
        Entity camera;
        Vector2 centrePos;

        public float Alpha { get; set; }
        public BackGround(Texture2D tex, Entity camera, Vector2 centrePosition)
        {
            UpdateIn120 = true;
            Depth = 0f;
            centrePos = centrePosition;
            Image = tex;
            this.camera = camera;
        }

        public override void Draw()
        {
            FormalDraw(Image, Centre, Color.White * Alpha, Rotation, ImageCentre);
        }

        public override void Update()
        {
            Centre = centrePos + ImageCentre - camera.Centre;
            Rotation = -camera.Rotation;
            if (camera.Disposed) Dispose();
        }
    }
}
namespace UndyneFight_Ex
{
    public class ImageEntity : AutoEntity
    {
        public ImageEntity(Texture2D image)
        {
            Image = image;
        }

        public override void Update()
        {
        }
    }
    public abstract class AutoEntity : Entity
    {
        public Color BlendColor { set; private get; } = Color.White;
        public float Alpha { get; set; }

        public sealed override void Draw()
        {
            if (Alpha <= 0) return;
            FormalDraw(Image, Centre, BlendColor * Alpha, Scale, Rotation, ImageCentre);
        }
    }
    public abstract class Entity : GameObject
    {
        public bool AngelMode { set; private get; } = false;
        public static float depthDetla = 0.00f;
        private float DrawingRotation(float rotation) => AngelMode ? MathUtil.GetRadian(rotation) : rotation;

        public Entity()
        {
            controlLayer = Surface.Normal;
        }

        public Surface controlLayer;

        public void FormalDraw(Texture2D tex, Vector2 centre, Color color, float rotation, Vector2 rotateCentre)
        {
            rotation = DrawingRotation(rotation);
            if (NotInScene(tex, centre, new(1, 1), rotation, rotateCentre)) return;
            GameMain.MissionSpriteBatch.Draw(tex, centre, null, color * controlLayer.drawingAlpha, rotation, rotateCentre, 1.0f, SpriteEffects.None, Depth + depthDetla);
            depthDetla += 0.00001f;
        }
        public void FormalDraw(Texture2D tex, Vector2 centre, Rectangle? texArea, Color color, float rotation, Vector2 rotateCentre)
        {
            rotation = DrawingRotation(rotation);
            if (NotInScene(tex, centre, new(1, 1), rotation, rotateCentre)) return;
            GameMain.MissionSpriteBatch.Draw(tex, centre, texArea, color * controlLayer.drawingAlpha, rotation, rotateCentre, 1.0f, SpriteEffects.None, Depth + depthDetla);
            depthDetla += 0.00001f;
        }
        public void FormalDraw(Texture2D tex, Vector2 centre, Rectangle? texArea, Color color, float drawingScale, float rotation, Vector2 rotateCentre)
        {
            rotation = DrawingRotation(rotation);
            if (NotInScene(tex, centre, new(drawingScale, drawingScale), rotation, rotateCentre)) return;
            GameMain.MissionSpriteBatch.Draw(tex, centre, texArea, color * controlLayer.drawingAlpha, rotation, rotateCentre, drawingScale, SpriteEffects.None, Depth + depthDetla);
            depthDetla += 0.00001f;
        }
        public void FormalDraw(Texture2D tex, Vector2 centre, Rectangle? texArea, Color color, Vector2 drawingScale, float rotation, Vector2 rotateCentre, SpriteEffects spriteEffects)
        {
            rotation = DrawingRotation(rotation);
            if (NotInScene(tex, centre, drawingScale, rotation, rotateCentre)) return;
            GameMain.MissionSpriteBatch.Draw(tex, centre, texArea, color * controlLayer.drawingAlpha, rotation, rotateCentre, drawingScale, spriteEffects, Depth + depthDetla);
            depthDetla += 0.00001f;
        }
        public void FormalDraw(Texture2D tex, Rectangle area, Color color)
        {
            GameMain.MissionSpriteBatch.Draw(tex, area, null, color * controlLayer.drawingAlpha, 0, Vector2.Zero, SpriteEffects.None, Depth + depthDetla);
            depthDetla += 0.00001f;
        }
        public void FormalDraw(Texture2D tex, Rectangle area, Rectangle restrict, Color color)
        {
            GameMain.MissionSpriteBatch.Draw(tex, area, restrict, color * controlLayer.drawingAlpha, 0, Vector2.Zero, SpriteEffects.None, Depth + depthDetla);
            depthDetla += 0.00001f;
        }
        public void FormalDraw(Texture2D tex, Vector2 centre, Color color, float drawingScale, float rotation, Vector2 rotateCentre)
        {
            rotation = DrawingRotation(rotation);
            if (NotInScene(tex, centre, new Vector2(drawingScale, drawingScale), rotation, rotateCentre)) return;
            GameMain.MissionSpriteBatch.Draw(tex, centre, null, color * controlLayer.drawingAlpha, rotation, rotateCentre, drawingScale, SpriteEffects.None, Depth + depthDetla);
            depthDetla += 0.00001f;
        }

        public void FormalDraw(Texture2D tex, Vector2 centre, Color color, Vector2 drawingScale, float rotation, Vector2 rotateCentre)
        {
            rotation = DrawingRotation(rotation);
            if (NotInScene(tex, centre, drawingScale, rotation, rotateCentre)) return;
            GameMain.MissionSpriteBatch.Draw(tex, centre, null, color * controlLayer.drawingAlpha, rotation, rotateCentre, drawingScale, SpriteEffects.None, Depth + depthDetla);
            depthDetla += 0.00001f;
        }

        private bool NotInScene(Texture2D tex, Vector2 centre, Vector2 drawingScale, float rotation, Vector2 rotateCentre)
        {
            float scale = (1 / MathF.Abs(CurrentScene.CurrentDrawingSettings.screenScale)) * (MathF.Abs(MathF.Sin(CurrentScene.CurrentDrawingSettings.screenAngle * 2)) * 0.414f + 1) * 1.05f;
            Vector4 extend = CurrentScene.CurrentDrawingSettings.Extending;
            CollideRect cur = new CollideRect(0, -480 * extend.W, 640 * scale * GameStates.SurfaceScale, 480 * (scale + extend.W) * GameStates.SurfaceScale);
            cur.SetCentre(new Vector2(320, 240 - 240 * extend.W) * GameStates.SurfaceScale);
            cur.Offset(-CurrentScene.CurrentDrawingSettings.screenDetla / CurrentScene.CurrentDrawingSettings.screenScale);

            if (cur.Contain(centre)) return false;

            Vector2 size = tex.Bounds.Size.ToVector2() * drawingScale;
            rotateCentre *= drawingScale;
            Vector2[] points = { Vector2.Zero, new(size.X, 0), new(0, size.Y), size };
            for (int i = 0; i < points.Length; i++) points[i] = points[i] - rotateCentre;
            Vector2[] reals = new Vector2[4];
            for (int i = 0; i < reals.Length; i++) reals[i] = MathUtil.Rotate(points[i], MathUtil.GetAngle(rotation)) + centre;
            float[] dirs = new float[4];
            dirs[0] = dirs[2] = 99999;
            dirs[1] = dirs[3] = -99999;
            for (int i = 0; i < dirs.Length; i++)
            {
                dirs[0] = MathF.Min(reals[i].X, dirs[0]);
                dirs[1] = MathF.Max(reals[i].X, dirs[1]);
                dirs[2] = MathF.Min(reals[i].Y, dirs[2]);
                dirs[3] = MathF.Max(reals[i].Y, dirs[3]);
            }
            CollideRect bounding = new(dirs[0], dirs[2], dirs[1] - dirs[0], dirs[3] - dirs[2]);

            bool b = !bounding.Intersects(cur);
            return b;
        }

        public float Depth { get; set; }

        private Texture2D image;
        protected Vector2 ImageCentre { get; set; }
        protected Texture2D Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                ImageCentre = new Vector2(value.Width, value.Height) / 2.0f;
            }
        }

        protected CollideRect collidingBox;

        public float Rotation { get; set; }
        public float Scale { get; set; } = 1.0f;

        public CollideRect CollidingBox
        {
            get
            {
                return collidingBox;
            }
        }

        public Vector2 Centre
        {
            get
            {
                return collidingBox.GetCentre();
            }
            set
            {
                collidingBox.SetCentre(value);
            }
        }

        public abstract void Draw();
        protected ShinyEffect CreateShinyEffect()
        {
            ShinyEffect effect = new ShinyEffect(this);
            GameStates.InstanceCreate(effect);
            return effect;
        }
        protected ShinyEffect CreateShinyEffect(Color color)
        {
            ShinyEffect effect = new ShinyEffect(this, color);
            GameStates.InstanceCreate(effect);
            return effect;
        }
        protected ShinyEffect CreateShinyEffect(Color color, Texture2D image)
        {
            ShinyEffect effect = new ShinyEffect(this, color, image);
            GameStates.InstanceCreate(effect);
            return effect;
        }

        protected void CreateRetentionEffect(float time)
        {
            RetentionEffect effect = new RetentionEffect(this, time);
            GameStates.InstanceCreate(effect);
        }
        protected void CreateRetentionEffect(Color color, float time)
        {
            RetentionEffect effect = new RetentionEffect(this, time, color);
            GameStates.InstanceCreate(effect);
        }

        protected class ShinyEffect : Entity
        {
            private readonly Entity attracter;

            public ShinyEffect(Entity original)
            {
                controlLayer = original.controlLayer;
                attracter = original;
                Image = original.image;
            }

            public ShinyEffect(Entity original, Color color)
            {
                controlLayer = original.controlLayer;
                attracter = original;
                Image = original.image;
                drawingColor = color;
            }

            public ShinyEffect(Entity original, Color color, Texture2D shinyImage)
            {
                controlLayer = original.controlLayer;
                attracter = original;
                Image = shinyImage;
                drawingColor = color;
            }

            public ShinyEffect(Color color, Texture2D tex, Vector2 centre, float rotation)
            {
                Rotation = rotation;
                drawingColor = color;
                Image = tex;
                Centre = centre;
            }

            private float baseScale = 1.0f;
            private float drawingScale = 1.0f;
            private float darkerSpeed = 3.5f;

            public float DarkerSpeed { set => darkerSpeed = value; }
            public Vector2 MissionSize { set => missionSize = value; }

            private Color drawingColor;
            private Vector2 missionSize = new Vector2(3.0f, 3.0f);

            public override void Update()
            {
                if (attracter != null)
                {
                    Centre = attracter.Centre;
                    Rotation = attracter.Rotation;
                    Depth = attracter.Depth + 0.001f;
                    baseScale = attracter.Scale;
                }
                drawingScale += darkerSpeed / 100f;
                if (drawingScale >= 2f) Dispose();
            }

            public override void Draw()
            {
                FormalDraw(image, Centre, drawingColor * (2 - drawingScale), Vector2.Lerp(Vector2.One, missionSize * baseScale, drawingScale - 1), MathUtil.GetRadian(Rotation), ImageCentre);
            }
        }

        protected class RetentionEffect : Entity
        {
            private readonly float totalTime = 30;
            private Color color;

            public RetentionEffect(Entity original, float time) : this(original, time, Color.White) { }
            public RetentionEffect(Entity original, float time, Color color)
            {
                controlLayer = original.controlLayer;
                time += 5f;
                Depth = original.Depth;
                Depth -= 0.01f;
                this.color = color;
                totalTime = time;
                Rotation = original.Rotation;
                Centre = original.Centre;
                Image = original.image;
            }
            public override void Draw()
            {
                FormalDraw(image, Centre, color * alpha, Rotation, ImageCentre);
            }

            private float alpha = 1f;
            public override void Update()
            {
                alpha -= 1 / totalTime;
                alpha *= 0.95f;
                Depth -= 0.0005f;
                if (alpha < 0) Dispose();
            }
        }
    }

    public abstract class GameObject
    {
        protected Scene CurrentScene => GameStates.CurrentScene;
        public bool UpdateIn120 { get; init; } = false;
        public bool BeingUpdated { get; protected set; } = false;
        public bool UpdateEnabled { get; set; } = true;

        public bool CrossScene { get; protected set; } = false;

        public bool ContainTag(string tagName)
        {
            if (tags == null) return false;
            foreach (var v in tags)
            {
                if (v.tagName == tagName) return true;
            }
            return false;
        }
        public string[] Tags
        {
            set
            {
                tags = new Tag[value.Length];
                for (int i = 0; i < value.Length; i++) tags[i] = new Tag(value[i]);
            }
            get
            {
                string[] str = new string[tags.Length];
                for (int i = 0; i < str.Length; i++) str[i] = tags[i].tagName;
                return str;
            }
        }
        protected bool HasTag()
        {
            return tags != null;
        }
        private Tag[] tags;

        public object Extras { get; set; }
        protected internal List<GameObject> ChildObjects { get; private set; } = new List<GameObject>();

        public abstract void Update();
        public virtual void Start() { }

        internal void TreeUpdate()
        {
            if (disposed) return;
            if (Update120F || UpdateIn120)
            {
                if (!BeingUpdated) Start();
                Update();
                BeingUpdated = true;
            }
            ChildObjects.RemoveAll(
                s =>
                {
                    return s.disposed;
                    // test
                    bool result = s.disposed;

                    if (result)
                    {
                        ;
                    }
                    return result;
                }
            );
            ChildObjects.ForEach(s => s.TreeUpdate());
        }

        private bool disposed = false;
        private bool Update120F => GameMain.Update120F;

        public bool Disposed => disposed;

        public virtual void Dispose()
        {
            OnDispose?.Invoke();
            disposed = true;
            ChildObjects.ForEach(s => s.Dispose());
        }
        public void Kill()
        {
            disposed = true;
        }
        public event Action OnDispose;
        public GameObject FatherObject { get; private set; }
        public void AddChild(GameObject obj)
        {
            ChildObjects.Add(obj);
            obj.FatherObject = this;
        }
        public virtual void Reverse()
        {
            ChildObjects.ForEach(s => s.Reverse());
            disposed = false;
        }

        public List<Entity> GetDrawableTree()
        {
            List<Entity> list = new List<Entity>();
            if (BeingUpdated && this is Entity) list.Add(this as Entity);
            foreach (GameObject child in ChildObjects)
            {
                list.AddRange(child.GetDrawableTree());
            }
            return list;
        }
    }

    public class Tag
    {
        public Tag() { }
        public Tag(string name) { tagName = name; }
        public string tagName;
    }
}