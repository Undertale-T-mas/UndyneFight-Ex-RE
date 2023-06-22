using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UndyneFight_Ex.Debugging
{
    internal partial class DebugWindow : Game
    {
        private SpriteBatch _spriteBatch;
        private static DebugWindow instance;
        private readonly GraphicsDeviceManager graphics;
        private int appearTime = 0, lastEnterTime = 0;

        private static GLFont debugFont;
        private static bool contentLoaded = false;

        private static bool DrawCursor => instance.lastEnterTime % 48 < 24;
        private static Vector2 ScreenSize => instance.Window.ClientBounds.Size.ToVector2();

        public DebugWindow()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            Window.Title = "UF-Ex Debugger [V0.1.0]";
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            TypeMatchingLibrary.Initialize();
            CommandManager.Initialize();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            if (contentLoaded) return;
            contentLoaded = true;
            debugFont = new GLFont("Content\\Sprites\\font\\normal", Content);
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (KeyInputManager.IsKeyDown(Keys.Escape)) Exit();

            CommandManager.Update();

            appearTime++;
            lastEnterTime++;

            KeyInputManager.PrepareStates();
            CommandManager.Insert(KeyInputManager.GetKeyInput());
            if (KeyInputManager.IsKeyPressed(Keys.Back)) CommandManager.DeleteBack();
            if (KeyInputManager.IsKeyPressed(Keys.Left)) CommandManager.CursorLeft();
            if (KeyInputManager.IsKeyPressed(Keys.Right)) CommandManager.CursorRight();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new(0, 0, 32, 255));

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, null);
            debugFont.Draw("Enter your command:", new Vector2(10, 10), Color.LightBlue, _spriteBatch);
            if (lastEnterTime % 48 < 24 || CommandManager.HasCommand)
                debugFont.Draw(">", new Vector2(10, 42), Color.LightBlue, _spriteBatch);
            CommandManager.RenderCommand(new Vector2(25, 42), CommandManager.MainCommand);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}