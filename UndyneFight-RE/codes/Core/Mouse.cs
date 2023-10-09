using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake
{
    public enum CursorState
    {
        Normal = 0,
        Drag = 1,
        Sizing = 2
    }
    public static class MouseSystem
    {
        public static CursorState CursorState { internal get; set; }
        internal static void Initialize()
        {
            ScreenSize = CurrentWindow.ClientBounds.Size.ToVector2();
        }

        internal static void Update()
        {
            lastState = currentState;
            currentState = Mouse.GetState();

            MouseWheelChanged = currentState.ScrollWheelValue - lastState.ScrollWheelValue;
            Moved = (PositionMoved = lastState.Position != currentState.Position) || MathF.Abs(MouseWheelChanged) > 0.1f || IsLeftClick();

            Vector2 real = CurrentState.Position.ToVector2();

            Vector2 centre = new Vector2(240 * Aspect, 240) * SurfaceScale;
            Vector2 delta = real - ScreenSize / 2f; 

            delta /= MathF.Min(
                ScreenSize.X / (480f * Aspect * SurfaceScale), ScreenSize.Y / (480f * SurfaceScale));

            Vector2 result = centre + delta;
            if (GameOnFocus)
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
            if (!GameOnFocus) return false;
            return (lastState.LeftButton == ButtonState.Released) && (currentState.LeftButton == ButtonState.Pressed);
        }
        public static bool IsLeftDown()
        {
            if (!GameOnFocus) return false;
            return currentState.LeftButton == ButtonState.Pressed;
        }
        public static bool IsLeftReleaseing()
        {
            if (!GameOnFocus) return false;
            return (currentState.LeftButton == ButtonState.Released) && (lastState.LeftButton == ButtonState.Pressed);
        }
    }
}