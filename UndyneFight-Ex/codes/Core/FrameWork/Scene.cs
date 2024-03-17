using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex
{
    /// <summary>
    /// 场景，所有游戏实体的承载体。每个场景强制为125帧更新速度
    /// </summary>
    public abstract class Scene : Entity
    {
        public class DrawingSettings
        {
            public Color backGroundColor = Color.Black;
            public Color themeColor = Color.White;
            public Color UIColor = Color.White;
            public float screenScale = 1f;
            public float sceneOutScale = 3.0f;
            public float outFadeScale = 0.86f;
            public float masterAlpha = 1.0f;
            public float defaultWidth = 640f;
            public Vector2 screenDelta;
            public Vector2 shakings = new(0, 0);
            public float screenAngle = 0.0f;
            public Dictionary<string, Surface> surfaces = new();
            public Vector4 Extending
            {
                get => extending;
                set
                {
                    extending = value;
                    GameMain.ResetRendering();
                }
            }
            private Vector4 extending;

            public float SurfaceScale => defaultWidth / 640f;

            public DrawingSettings()
            {
                surfaces.Add("normal", Surface.Normal);
                surfaces.Add("hidden", Surface.Hidden);
            }
        }
        public void PushSurface(Surface surface)
        {
            CurrentDrawingSettings.surfaces.Add(surface.Name, surface);
        }

        internal static void PrepareLoader(IServiceProvider serviceProvider)
        {
            Loader = new ContentManager(serviceProvider)
            {
                RootDirectory = "Content"
            };
        }

        public DrawingSettings CurrentDrawingSettings { get; set; } = new();

        public RenderingManager BackgroundRendering { get; internal set; } = new();
        public RenderingManager SceneRendering { get; internal set; } = new();

        private readonly List<GameObject> buffer = new();
        private readonly List<GameObject> objects = new();
        public List<GameObject> Objects => objects;

        public static ContentManager Loader { get; private set; }
        internal Selector BaseSelector { get; set; }

        public bool Pausable
        {
            get;
#if DEBUG
            set;
#else
            protected set;
#endif
        } = false;

        internal float stopTime = 0;

        public void InstanceCreate(GameObject t)
        {
            buffer.Add(t);
        }
        public override void Draw()
        {
            if (stopTime > 0.01f) return;
        }
        private Dictionary<string, List<GameEventArgs>> GameEvents { get; set; } = new Dictionary<string, List<GameEventArgs>>();
        public void Broadcast(GameEventArgs gameEventArgs)
        {
            if (!GameEvents.ContainsKey(gameEventArgs.ActionName))
                GameEvents.Add(gameEventArgs.ActionName, new());
            GameEvents[gameEventArgs.ActionName].Add(gameEventArgs);
        }
        public List<GameEventArgs> DetectEvent(string ActionName)
        {
            return GameEvents.ContainsKey(ActionName) ? GameEvents[ActionName] : null;
        }

        public List<GameObject> GlobalObjects() => objects.FindAll(s => s.CrossScene);

        public override void Update()
        {
            if (!BeingUpdated)
            {
                BeingUpdated = true;
                Fight.Functions.ScreenDrawing.Reset();
                Start();
            }
            buffer.ForEach(s => objects.Add(s));
            buffer.Clear();
            objects.RemoveAll(s => s.Disposed);
            objects.ForEach(s =>
            {
                s.TreeUpdate();
            });
            foreach (var v in GameEvents)
            {
                v.Value.RemoveAll(s => s.Disposed);
            }

            if (stopTime >= 0.4f)
                stopTime -= 0.50001f;
            if (stopTime < 0f)
            {
                stopTime = 0;
                Fight.Functions.PlaySound(FightResources.Sounds.change);
            }
        }
        public void SceneUpdate()
        {
            Update();
        }

        protected Scene(GameObject startObj)
        {
            objects.Add(startObj);
        }
        protected Scene() { }

        public override void Dispose()
        {
            objects.ForEach(s => { if (!s.CrossScene) s.Dispose(); });
            buffer.Clear();
            base.Dispose();
        }
        public RenderTarget2D DrawAll(RenderTarget2D mission)
        {
            return SceneRendering.Draw(mission);
        }

        public virtual void AlternatePause()
        {

        }
        public virtual void WhenPaused()
        {

        }

        internal void UpdateRendering()
        {
            SceneRendering.UpdateAll();
            BackgroundRendering.UpdateAll();
        }
    }
}