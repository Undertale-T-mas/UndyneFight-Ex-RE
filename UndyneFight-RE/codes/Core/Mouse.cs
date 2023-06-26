using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UndyneFight_Ex.Remake
{
    public static class MouseSystem
    {
        internal static void Update()
        {
            lastState = currentState;
            currentState = Mouse.GetState();
            Moved = (lastState.Position != currentState.Position);

            Vector2 real = CurrentState.Position.ToVector2();

            Vector2 centre = new Vector2(320, 240) * GameStates.SurfaceScale;
            Vector2 delta = real - ScreenSize / 2f; 

            delta = delta / MathF.Min(
                ScreenSize.X / (640f * GameStates.SurfaceScale), ScreenSize.Y / (480f * GameStates.SurfaceScale));

            Vector2 result = centre + delta;
            TransferredPosition = result;
        }
        private static MouseState currentState, lastState;

        public static bool Moved { get; private set; }

        public static MouseState CurrentState => currentState;

        public static Vector2 TransferredPosition { get; private set; }
        public static Vector2 ScreenSize { private get; set; } = new Vector2(640, 480);

        public static bool IsLeftClick()
        {
            return (lastState.LeftButton == ButtonState.Released) && (currentState.LeftButton == ButtonState.Pressed);
        }
    }
}