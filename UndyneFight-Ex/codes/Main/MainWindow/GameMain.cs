using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks; 
using Microsoft.Xna.Framework.Content; 
using static System.MathF;
using UndyneFight_Ex.Settings;
using static UndyneFight_Ex.Settings.SettingsManager;

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
        internal static float Aspect { get; set; } = 4f / 3f;
        private static GameMain instance;

        public static GameWindow CurrentWindow => instance.Window;
        public static Texture2D[] RegisterTextures = new Texture2D[3];

        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SpriteBatchEX MissionSpriteBatch { get; private set; }

        public static SpriteEffect SpriteEffect { get; private set; }
        public static EffectPass SpritePass { get; private set; }

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
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.PreferMultiSampling = true;
            Graphics.PreferredBackBufferHeight = 480;
            Graphics.PreferredBackBufferWidth = 640;
            Window.AllowUserResizing = true;
            Window.Title = "Rhythm Recall p-? (UF-Ex [V0.2.0])";
            Graphics.ApplyChanges();

            // TODO: Add your initialization logic here
            GameMain.SpriteEffect = new(Graphics.GraphicsDevice);
            GameMain.SpritePass = SpriteEffect.CurrentTechnique.Passes[0];
#if DEBUG
            debugTarget1 = new RenderTarget2D(GraphicsDevice, 96, 35, true, SurfaceFormat.Color, DepthFormat.None);
            debugTarget2 = new RenderTarget2D(GraphicsDevice, 96, 35, true, SurfaceFormat.Color, DepthFormat.None);
#endif
            finalTarget = new RenderTarget2D(GraphicsDevice, (int)(480f * GameMain.Aspect), 480, false, SurfaceFormat.Color, DepthFormat.None);
            Window.ClientSizeChanged += (s, t) =>
            {
                CilentBoundChanged();
                //  hiddenTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None);
            };
            RenderProduction.UpdateBase(new(480f * Aspect, 480));
            //base.Initialize();
            LoadContent();
        }

        private void CilentBoundChanged()
        {
            float trueX = Window.ClientBounds.Width, trueY = Window.ClientBounds.Height;
            screenSize = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);
            if (screenSize.X >= screenSize.Y * Aspect)
            {
                trueX = trueY * Aspect;
            }
            else
            {
                trueY = trueX / Aspect;
            }

            RenderProduction.UpdateBase(new(trueX, trueY));
            GameStates.WindowSizeChanged(new(trueX, trueY));
        }

        public static void ResetRendering()
        {
            DrawFPS = DataLibrary.DrawFPS;
            instance.CilentBoundChanged(); 
            Graphics.SynchronizeWithVerticalRetrace = false; 
            MissionSpriteBatch.DefaultState = DataLibrary.SamplerState switch
            {
                "Nearest" => SpriteBatchEX.NearestSample,
                "3x Linear" => Microsoft.Xna.Framework.Graphics.SamplerState.LinearClamp,
                "Anisotropic" => Microsoft.Xna.Framework.Graphics.SamplerState.AnisotropicClamp,
                _ => throw new Exception()
            };
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            MissionSpriteBatch = new SpriteBatchEX(GraphicsDevice);
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
        private static Vector2 screenSize = new(480 * Aspect, 480);
        private static float screenDistance = Sqrt(360 * 360 + 270 * 270);
        private static Matrix matrix;
        public static Matrix ResizeMatrix => matrix;

        internal static Vector2 ScreenSize => screenSize;

        internal static Scene.DrawingSettings CurrentDrawingSettings => GameStates.CurrentScene?.CurrentDrawingSettings;

        internal static bool Update120F { get; private set; }

        #endregion

        #region 反变速
        private static float speedRematcher = 1.0f;
        #endregion

        private void TryExit()
        {
            if (GameStates.CurrentScene.Pausable)
            {
                if (GameStates.Paused)
                    GameStates.RunGameResume();
                else GameStates.RunGamePause();
            }
            else this.Exit();
        }

        bool escPressed = false;

        protected override bool BeginDraw()
        {
            float frameTime = 1000f / DrawFPS;
            if(_totalElapsedMS > frameTime)
            { 
                _totalElapsedMS -= frameTime;
                return true;
            }
            return false;   
        }
        public static float DrawFPS { get; set; } = 60f;
        float _totalElapsedMS = 0;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        { 
            _totalElapsedMS += gameTime.ElapsedGameTime.Milliseconds;
            if (_totalElapsedMS > 100f) _totalElapsedMS /= 2f;
            #region Event for times
            appearTime++;
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, Math.Max(1, (int)(8f / gameSpeed * speedRematcher)));

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (!escPressed)
                    TryExit();
                escPressed = true;
            }
            else escPressed = false;
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
            GameInterface.UFEXSettings.DoUpdate();
            base.Update(gameTime);
        }

        private Vector2 lastSize;
        private bool _isFullScreen = false;

        public static bool OnFocus => instance.IsActive;

        public void ToggleFullScreen()
        {
            Graphics.ToggleFullScreen();
            if (_isFullScreen)
            {
                Graphics.PreferredBackBufferWidth = (int)(480 * Aspect);
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
                Window.AllowUserResizing = false;
            }
            _isFullScreen = !_isFullScreen;
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
