using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.MathF;

namespace UndyneFight_Ex
{
    internal static class ModeLab
    {
        #region debug Area
        public static bool showCollide = true;
        public static bool shaderReduce = false;

        #endregion
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    internal partial class GameMain : Game
    {
        private static GameMain instance;

        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SpriteBatch MissionSpriteBatch { get; private set; }

        private static bool isDebugWindowExists = false;

        public static float gameTime = 0;

        public static float shaderParam = 0f;

        public static float GameSpeed
        {
            set
            {
                gameSpeed = value;
            }
            get
            {
                return gameSpeed;
            }
        }
        public static float gameSpeed = 1 / 1f;

        public static void ExitGame()
        {
            instance.Exit();
        }

        private int appearTime = 0;

        internal GameMain()
        {
            instance = this;
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 8 * 1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Graphics.PreferredBackBufferHeight = 480;
            Graphics.PreferredBackBufferWidth = 640;
            Window.AllowUserResizing = true;
            Window.Title = "UF-Ex [V0.1.7]";
            Graphics.ApplyChanges();

            // TODO: Add your initialization logic here
#if DEBUG
            debugTarget1 = new RenderTarget2D(GraphicsDevice, 96, 35, true, SurfaceFormat.Color, DepthFormat.None);
            debugTarget2 = new RenderTarget2D(GraphicsDevice, 96, 35, true, SurfaceFormat.Color, DepthFormat.None);
#endif
            finalTarget = new RenderTarget2D(GraphicsDevice, 640, 480, false, SurfaceFormat.Color, DepthFormat.None);
            Window.ClientSizeChanged += (s, t) =>
            {
                CilentBoundChanged();
                //  hiddenTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None);
            };
            RenderProduction.UpdateBase(new(640, 480));
            //base.Initialize();
            LoadContent();
        }

        private void CilentBoundChanged()
        {
            float trueX = Window.ClientBounds.Width, trueY = Window.ClientBounds.Height;
            if (screenSize.X >= screenSize.Y * 4f / 3f)
            {
                trueX = trueY * 4f / 3f;
            }
            else
            {
                trueY = trueX * 0.75f;
            }

            RenderProduction.UpdateBase(new(trueX, trueY));
            GameStates.WindowSizeChanged(new(trueX, trueY));
        }

        public static void ResetRendering()
        {
            instance.CilentBoundChanged();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            MissionSpriteBatch = new SpriteBatch(GraphicsDevice);
            LoadObject();

#if !DEBUG && REPELL
            try
            {
#endif
            GlobalResources.Initialize(Content);
            GameStates.ResetScene(new ResourcesLoadingScene(Content));
#if !DEBUG && REPELL
            }
            catch (Exception e)
            {  
                throw new Exception("The ResourcePack has something wrong!", e);
            }
#endif

            InitializeRendering();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Fight.Functions.Loader.Dispose();

            base.UnloadContent();

            GC.Collect();
            // TODO: Unload any non ContentManager content here
        }

        #region RenderTargets
#if DEBUG
        private RenderTarget2D debugTarget1;
        private RenderTarget2D debugTarget2;
#endif 

        private RenderTarget2D finalTarget;

        #endregion

        #region fields 

        private static float basicAngle = Atan2(-320, -240);
        private const float quarterAngle = 0.5f * PI;
        private static Vector2 screenSize = new(640, 480);
        private static float screenDistance = Sqrt(360 * 360 + 270 * 270);
        private static Matrix matrix;

        internal static Vector2 ScreenSize => screenSize;

        internal static Scene.DrawingSettings CurrentDrawingSettings => GameStates.CurrentScene?.CurrentDrawingSettings;

        internal static bool Update120F { get; private set; }

        #endregion

        #region 反变速
        private static float speedRematcher = 1.0f;
        #endregion

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            #region Event for times
            appearTime++;
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, Math.Max(1, (int)(8f / gameSpeed * speedRematcher)));

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GameStates.IsKeyPressed120f(InputIdentity.FullScreen)) ToggleFullScreen();
            #endregion

            Update120F = GameMain.gameTime == (int)GameMain.gameTime;
            GameStates.StateUpdate();

            ResetDrawingSettings();

            /*     if (GameStates.IsKeyDown(Keys.LeftControl) && GameStates.IsKeyDown(Keys.D) && !isDebugWindowExists)
                 {
                     isDebugWindowExists = true;
                     CreateDebugWindow();
                     // Thread thread = new Thread(new ThreadStart(() => {
                     //  GraphicsDevice gd = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters());
                     var v = new Debugging.DebugWindow();
                     v.Run();
                     //  }));

                     //thread.Start();
                 } */
            if (GameStates.IsKeyPressed120f(InputIdentity.ScreenShot))
            {
                string FileDirectory = "Datas\\Screenshot";
                if (!Directory.Exists(FileDirectory))
                    Directory.CreateDirectory(FileDirectory);
                var Time = DateTime.Now;
                string time = $"{Time.Year}_{Time.Month}_{Time.Day}-{Time.Hour}_{Time.Minute}_{Time.Second}";
                Stream stream = new FileStream($"{FileDirectory}\\{time}.png", FileMode.OpenOrCreate);
                Task task = new(() =>
                {
                    finalTarget.SaveAsJpeg(stream, finalTarget.Width, finalTarget.Height);
                    stream.Flush();
                    stream.Dispose();
                });
                task.RunSynchronously();
            }

            base.Update(gameTime);
        }

        private Vector2 lastSize;
        private bool isFullScreen = false;

        public void ToggleFullScreen()
        {
            isFullScreen = !isFullScreen;
            Graphics.ToggleFullScreen();
            if (isFullScreen)
            {
                Graphics.PreferredBackBufferWidth = 640;
                Graphics.PreferredBackBufferHeight = 480;
                Window.AllowUserResizing = true;
                CilentBoundChanged();
            }
            else
            {
                GraphicsAdapter adapter = Graphics.GraphicsDevice.Adapter;
                lastSize = screenSize;
                Graphics.PreferredBackBufferWidth = adapter.CurrentDisplayMode.Width;
                Graphics.PreferredBackBufferHeight = adapter.CurrentDisplayMode.Height;
                Window.AllowUserResizing = true;
            }
            Graphics.ApplyChanges();
        }

        private static void CreateDebugWindow()
        {
            //throw new NotImplementedException();
            Task.Run(() =>
            {
                Debugging.DebugWindow window = new();
                window.Run();
            });
        }
    }
}
