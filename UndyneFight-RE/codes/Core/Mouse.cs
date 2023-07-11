using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UndyneFight_Ex.Remake
{
    public static class MouseSystem
    {
        internal static void Initialize()
        {
            ScreenSize = GameStates.CurrentWindow.ClientBounds.Size.ToVector2();
        }

        internal static void Update()
        {
            lastState = currentState;
            currentState = Mouse.GetState();

            MouseWheelChanged = currentState.ScrollWheelValue - lastState.ScrollWheelValue;
            Moved = (PositionMoved = lastState.Position != currentState.Position) || MathF.Abs(MouseWheelChanged) > 0.1f || IsLeftClick();

            Vector2 real = CurrentState.Position.ToVector2();

            Vector2 centre = new Vector2(240 * GameStates.Aspect, 240) * GameStates.SurfaceScale;
            Vector2 delta = real - ScreenSize / 2f; 

            delta /= MathF.Min(
                ScreenSize.X / (480f * GameStates.Aspect * GameStates.SurfaceScale), ScreenSize.Y / (480f * GameStates.SurfaceScale));

            Vector2 result = centre + delta;
            if (GameStates.GameOnFocus)
                TransferredPosition = result;
        }
        private static MouseState currentState, lastState;

        public static bool Moved { get; private set; }
        public static bool PositionMoved { get; private set; }

        public static MouseState CurrentState => currentState;

        public static Vector2 TransferredPosition { get; private set; }
        public static Vector2 ScreenSize { private get; set; } = new Vector2(640, 480);

        public static float MouseWheelChanged { get; private set; } = 0f;

        public static bool IsLeftClick()
        {
            if (!GameStates.GameOnFocus) return false;
            return (lastState.LeftButton == ButtonState.Released) && (currentState.LeftButton == ButtonState.Pressed);
        }
        public static bool IsLeftDown()
        {
            if (!GameStates.GameOnFocus) return false;
            return currentState.LeftButton == ButtonState.Pressed;
        }
        public static bool IsLeftReleaseing()
        {
            if (!GameStates.GameOnFocus) return false;
            return (currentState.LeftButton == ButtonState.Released) && (lastState.LeftButton == ButtonState.Pressed);
        }
    }
}