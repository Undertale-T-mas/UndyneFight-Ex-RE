using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex
{
    public abstract class RenderProduction : IComparable<RenderProduction>
    {
        private static bool HighQuality => Settings.SettingsManager.DataLibrary.drawingQuality == Settings.SettingsManager.DataLibrary.DrawingQuality.High;
        protected static float AdaptingScale => HighQuality ? MathF.Min(ScreenSize.X / (480f * GameMain.Aspect * GameStates.SurfaceScale), ScreenSize.Y / (480f)) : 1;
        protected static Vector2 ScreenSize => HighQuality ? GameMain.ScreenSize : new Vector2(480f * GameMain.Aspect, 480) * GameStates.SurfaceScale;

        protected static GraphicsDevice WindowDevice => GameMain.Graphics.GraphicsDevice;
        protected static SpriteBatch spriteBatch => GameMain.MissionSpriteBatch;

        private static readonly HashSet<Type> updatedTypes = new();

        internal bool disposed = false;
        public virtual void Dispose()
        {
            disposed = true;
        }
        public virtual void Update() { }
        private static Vector2 Adapt(Vector2 origin)
        {
            if (!HighQuality) return new Vector2(480f * GameMain.Aspect, 480) * GameStates.SurfaceScale;

            float trueX, trueY;
            if (origin.X >= origin.Y * GameMain.Aspect) { trueX = origin.Y * GameMain.Aspect; trueY = origin.Y; }
            else { trueY = origin.X / GameMain.Aspect; trueX = origin.X; }

            return new(trueX, trueY);

        }
        protected static Vector2 AdaptedSize
        {
            get
            {
                return Adapt(ScreenSize);
            }
        }
        protected RenderProduction(Shader shader, SpriteSortMode sortMode, BlendState blendState, float depth)
        {
            Type type = GetType();
            if (!updatedTypes.Contains(type))
            {
                updatedTypes.Add(type);
                WindowSizeChanged(AdaptedSize);
            }
            this.shader = shader;
            SpriteSortMode = sortMode;
            BlendState = blendState;
            this.depth = depth;
            if (depth > 1 || depth < 0)
                throw new ArgumentException(String.Format("the value {0} have to be in 0~1", nameof(depth)), nameof(depth));
        }
        protected RenderTarget2D MissionTarget { get; set; }

        private Shader shader;
        public SpriteSortMode SpriteSortMode { private get; set; }
        public BlendState BlendState { private get; set; }

        protected bool AutoChangeSize { private get; set; } = true;

        private bool enabledMatrix = false;
        private Matrix matrix;
        protected Matrix TransForm { set { matrix = value; enabledMatrix = true; } }
        private readonly float depth;
        protected Shader Shader { set => shader = value; get => shader; }

        protected SamplerState SamplerState { get; set; } = null;

        public int CompareTo(RenderProduction r)
        {
            RenderProduction obj = r;
            return obj == null ? throw new NotImplementedException() : depth < obj.depth ? -1 : depth == obj.depth ? 0 : 1;
        }
        protected void ResetTargetColor(Color color)
        {
            TrySetTarget();
            GameDevice.Clear(color);
        }
        protected void DrawEntities(Entity[] entities)
        {
            TrySetTarget();
            if (shader == null)
            {
                GameMain.MissionSpriteBatch.Begin(SpriteSortMode, BlendState, null, null, null, null, enabledMatrix ? matrix : null);
                foreach (var v in entities) v.Draw();
                GameMain.MissionSpriteBatch.End();
                return;
            }
            shader.Update();
            GameMain.MissionSpriteBatch.Begin(SpriteSortMode, BlendState, null, null, null, shader, enabledMatrix ? matrix : null);
            foreach (var v in entities) v.Draw();
            GameMain.MissionSpriteBatch.End();
        }
        protected void DrawTexture(Texture2D s, Rectangle pos, Rectangle? from, Color color)
        {
            if (s == MissionTarget)
            {
                CopyRenderTarget(screenSizedTarget, s);
                s = screenSizedTarget;
            }
            TrySetTarget();
            if (shader != null)
            {
                shader.Update();
                GameMain.MissionSpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, null, null, shader, enabledMatrix ? matrix : null);
            }
            else
            {
                GameMain.MissionSpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, null, null, null, enabledMatrix ? matrix : null);
            }
            GameMain.MissionSpriteBatch.Draw(s, pos, from, color);
            GameMain.MissionSpriteBatch.End();
        }
        protected void DrawTextures(Texture2D[] tex, Rectangle pos, Rectangle? from, Color[] colors)
        {
            for (int i = 0; i < tex.Length; i++)
                if (tex[i] == MissionTarget)
                {
                    CopyRenderTarget(screenSizedTarget, tex[i]);
                    tex[i] = screenSizedTarget;
                }
            TrySetTarget();
            if (shader != null)
            {
                shader.Update();
                GameMain.MissionSpriteBatch.Begin(SpriteSortMode, BlendState, null, null, null, shader, enabledMatrix ? matrix : null);
            }
            else
            {
                GameMain.MissionSpriteBatch.Begin(SpriteSortMode, BlendState, null, null, null, null, enabledMatrix ? matrix : null);
            }
            for(int i = 0; i < tex.Length; ++i)
                GameMain.MissionSpriteBatch.Draw(tex[i], pos, from, colors[i]);
            GameMain.MissionSpriteBatch.End();
        }
        protected void DrawTextures(Texture2D[] tex, Rectangle pos, Rectangle? from, Color color)
        {
            Color[] colors = new Color[tex.Length];
            for (int i = 0; i < tex.Length; i++) colors[i] = color;
            this.DrawTextures(tex, pos, from, colors);
        }
        protected void DrawTextures(Texture2D[] s, Rectangle bound)
        {
            DrawTextures(s, bound, null, Color.White);
        }
        protected void DrawTexture(Texture2D s, Vector2 pos, Color color)
        {
            DrawTexture(s, new Rectangle(pos.ToPoint(), s.Bounds.Size), null, color);
        }
        protected void DrawTexture(Texture2D s, Vector2 pos, Color color, float size)
        {
            DrawTexture(s, new Rectangle(pos.ToPoint(), (s.Bounds.Size.ToVector2() * size).ToPoint()), null, color);
        }
        protected void DrawTexture(Texture2D s, Vector2 pos, Color color, Vector2 size)
        {
            DrawTexture(s, new Rectangle(pos.ToPoint(), (s.Bounds.Size.ToVector2() * size).ToPoint()), null, color);
        }
        protected void DrawTexture(Texture2D s, Rectangle bound)
        {
            DrawTexture(s, bound, null, Color.White);
        }
        protected void DrawTexture(Texture2D s, Rectangle bound, Color color)
        {
            DrawTexture(s, bound, null, color);
        }
        protected void DrawTexture(Texture2D s, Vector2 pos)
        {
            DrawTexture(s, pos, Color.White);
        }
        public virtual void WindowSizeChanged(Vector2 vec)
        {
            updatedTypes.Add(GetType());
        }
        internal static void UpdateBase(Vector2 vec)
        {
            vec = Adapt(vec);
            updatedTypes.Clear();
            if (vec == Vector2.Zero) return;
            if (screenSizedTarget == null || screenSizedTarget.Width != (int)vec.X || screenSizedTarget.Height != (int)vec.Y)
                screenSizedTarget = new RenderTarget2D(WindowDevice, (int)vec.X, (int)vec.Y, false, SurfaceFormat.Color, DepthFormat.None);
            if (HelperTarget == null || HelperTarget.Width != (int)vec.X || HelperTarget.Height != (int)vec.Y)
            {
                HelperTarget = new RenderTarget2D(WindowDevice, (int)vec.X, (int)vec.Y, false, SurfaceFormat.Color, DepthFormat.None);
                HelperTarget2 = new RenderTarget2D(WindowDevice, (int)vec.X, (int)vec.Y, false, SurfaceFormat.Color, DepthFormat.None);
                HelperTarget3 = new RenderTarget2D(WindowDevice, (int)vec.X, (int)vec.Y, false, SurfaceFormat.Color, DepthFormat.None);
            }
        }
        public abstract RenderTarget2D Draw(RenderTarget2D obj);

        private void TrySetTarget()
        {
            if (currentTarget == MissionTarget) return;
            currentTarget = MissionTarget;
            GameDevice.SetRenderTarget(MissionTarget);
        }
        private void TrySetTarget(RenderTarget2D mission)
        {
            if (currentTarget == mission) return;
            currentTarget = mission;
            GameDevice.SetRenderTarget(mission);
        }
        protected void CopyRenderTarget(RenderTarget2D distin, Texture2D source)
        {
            TrySetTarget(distin);
            //ResetTargetColor(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Immediate);
            spriteBatch.Draw(source, distin.Bounds, Color.White);
            spriteBatch.End();
        }

        private static GraphicsDevice GameDevice => GameMain.Graphics.GraphicsDevice;

        private static RenderTarget2D currentTarget;
        private static RenderTarget2D screenSizedTarget;
        protected static RenderTarget2D HelperTarget { private set; get; }
        protected static RenderTarget2D HelperTarget2 { private set; get; }
        protected static RenderTarget2D HelperTarget3 { private set; get; }

        public bool Enabled { get; set; } = true;
    }

    public class Surface : RenderProduction
    {
        public float drawingAlpha { get; set; } = 1;

        internal static void Initialize()
        {
            Normal = new("normal") { BlendState = BlendState.AlphaBlend, SpriteSortMode = SpriteSortMode.FrontToBack, Transfer = TransferUse.ForceNormal };
            Hidden = new("hidden", true) { BlendState = BlendState.AlphaBlend, SpriteSortMode = SpriteSortMode.FrontToBack, BackGroundColor = Color.Black };
        }
        public Surface(string name) : base(null, SpriteSortMode.Deferred, null, 0.0f)
        {
            Name = name;
            RenderPaint = new RenderTarget2D(WindowDevice, (int)ScreenSize.X, (int)ScreenSize.Y);
        }
        public Surface(string name, bool lockSize) : base(null, SpriteSortMode.Deferred, null, 0.0f)
        {
            Name = name;
            SizeLock = lockSize;
            RenderPaint = SizeLock ? new RenderTarget2D(WindowDevice, (int)ScreenSize.X, (int)ScreenSize.Y) : new RenderTarget2D(WindowDevice, (int)(480 * GameMain.Aspect * GameStates.SurfaceScale), (int)(480 * GameStates.SurfaceScale));
        }
        public override void Dispose()
        {
            RenderPaint.Dispose();
            base.Dispose();
        }
        private class BoxPartDrawer : Entity
        {
            private CollideRect rect;
            public BoxPartDrawer(RenderTarget2D target, CollideRect pos)
            {
                Image = target;
                rect = pos;
                Depth = 0.39f;
            }
            public override void Draw()
            {
                spriteBatch.Draw(Image, rect.ToRectangle(), rect.ToRectangle(), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.39f);
            }

            public override void Update()
            {
                throw new NotImplementedException();
            }
        }

        public RenderTarget2D RenderPaint { get; private set; }
        public static Surface Normal { get; private set; }
        public static Surface Hidden { get; private set; } 
         
        public Color BackGroundColor { get; set; } = Color.Transparent;
        public bool DisableExpand { get; set; } = false;
        public event Action DoUpdate;

        public override void Update()
        {
            DoUpdate?.Invoke();
            Vector4 extending = DisableExpand ? Vector4.Zero : GameStates.CurrentScene.CurrentDrawingSettings.Extending;
            Vector2 size = !SizeLock ? AdaptedSize : new Vector2(480 * GameMain.Aspect, 480) * GameStates.SurfaceScale;
            int missionX = (int)size.X, missionY = (int)(size.Y * (1 + extending.W));
            if (RenderPaint.Bounds.Size != new Point(missionX, missionY))
            {
                RenderPaint.Dispose();
                RenderPaint = new RenderTarget2D(WindowDevice, missionX, missionY);
            }
        }
        public enum TransferUse
        {
            ForceDefault = 0,
            ForceNormal = 1,
            Custom = 2
        }
        public bool SizeLock { get; private set; } = false;
        public TransferUse Transfer { private get; set; } = TransferUse.Custom;
        public static Matrix NormalTransfer { get; private set; }
        public Matrix CustomMatrix { get; private set; } = Matrix.Identity;
        public void Draw(Entity[] entities, Matrix transfer)
        {
            Entity.depthDetla = 0;
            if (Transfer == TransferUse.ForceDefault)
            {
                transfer = Matrix.CreateScale(AdaptingScale / GameStates.SurfaceScale); transfer.M33 = 1;
            }
            else if (Transfer == TransferUse.Custom) transfer = CustomMatrix;
            MissionTarget = RenderPaint;
            ResetTargetColor(BackGroundColor);
            TransForm = transfer;
            Entity.depthDetla = 0;
            DrawEntities(entities);/*
            GameMain.Graphics.GraphicsDevice.SetRenderTarget(RenderPaint);
           // GameMain.Graphics.GraphicsDevice.SetRenderTarget(null);
            GameMain.Graphics.GraphicsDevice.Clear(BackGroundColor);
            GameMain.MissionSpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, DepthStencilState.Default, RasterizerState.CullNone, null, transfer);
            foreach(Entity entity in entities)
            {
                entity.TreeDraw();
            }
            GameMain.MissionSpriteBatch.End();*/
        }

        internal static void DistributeEntity(Entity[] entities, Matrix transfer)
        {
            NormalTransfer = transfer;
            Dictionary<string, Surface> surfaces = GameStates.CurrentScene.CurrentDrawingSettings.surfaces;
            foreach (KeyValuePair<string, Surface> kvp in surfaces)
            {
                kvp.Value.Update();
            }
            Dictionary<Surface, List<Entity>> distributer = new();
            foreach (Entity entity in entities)
            {
                if (!distributer.ContainsKey(entity.controlLayer))
                {
                    distributer.Add(entity.controlLayer, new List<Entity>());
                }
                distributer[entity.controlLayer].Add(entity);
            }
            if (!distributer.ContainsKey(Hidden)) distributer.Add(Hidden, new());
            Hidden.Draw(distributer[Hidden].ToArray(), transfer);
            distributer.Remove(Hidden);
            foreach (FightBox box in FightBox.boxs)
            {
                CollideRect collideRect = box.CollidingBox;
                distributer[Normal].Add(new BoxPartDrawer(Hidden.RenderPaint, collideRect));
            }
            foreach (KeyValuePair<Surface, List<Entity>> kvp in distributer)
            {
                kvp.Key.Draw(kvp.Value.ToArray(), transfer);
            }
        }

        public override RenderTarget2D Draw(RenderTarget2D obj)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
    }
    public class RenderingManager
    {
        private readonly SortedSet<RenderProduction> surfaces = new();
        public bool ExistProduction => surfaces.Count >= 1;

        public RenderTarget2D Draw(RenderTarget2D startTarget)
        {
            surfaces.RemoveWhere((s) => s.disposed);
            RenderTarget2D cur = startTarget;

            foreach (var itor in surfaces)
            {
                Entity.depthDetla = 0;
                if (itor.Enabled)
                    cur = itor.Draw(cur);
            }
            return cur;
        }
        public void WindowSizeChanged(Vector2 vec)
        {
            foreach (var v in surfaces)
            {
                v.WindowSizeChanged(vec);
            }
        }
        public void InsertProduction(RenderProduction production)
        {
            production.disposed = false;
            surfaces.Add(production);
        }

        internal void UpdateAll()
        {
            foreach (var v in this.surfaces) v.Update();
        }
    }
}