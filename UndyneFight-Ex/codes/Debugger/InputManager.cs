using Microsoft.Xna.Framework.Input;

namespace UndyneFight_Ex.Debugging
{
    internal partial class DebugWindow
    {
        internal static class KeyInputManager
        {
            public static bool ShiftPressed => shiftPressed;
            private static bool shiftPressed = false;

            internal static KeyboardState currentKeyState;
            internal static KeyboardState lastKeyState;

            private static char GetChar(Keys key)
            {
                int index = (int)key;
                if (IsKeyPressed(key))
                {
                    if (index < 91 && index > 64)
                        return (char)((shiftPressed ? 0 : 32) + index);
                    else if (index > 47 && index < 58)
                    {
#pragma warning disable CS8509 
                        if (shiftPressed) return (index - 48) switch
#pragma warning restore CS8509 
                        {
                            0 => ')',
                            1 => '!',
                            2 => '@',
                            3 => '#',
                            4 => '$',
                            5 => '%',
                            6 => '^',
                            7 => '&',
                            8 => '*',
                            9 => '('
                        };
                        return (char)index;
                    }
                    else switch (index)
                        {
                            case 32:
                                return ' ';
                            case 186:
                                if (shiftPressed) return ':';
                                return ';';
                            case 187:
                                if (shiftPressed) return '+';
                                return '=';
                            case 188:
                                if (shiftPressed) return '<';
                                return ',';
                            case 189:
                                if (shiftPressed) return '_';
                                return '-';
                            case 190:
                                if (shiftPressed) return '>';
                                return '.';
                            case 191:
                                if (shiftPressed) return '?';
                                return '/';
                            case 192:
                                if (shiftPressed) return '~';
                                return '`';

                        }
                }
                return (char)0;
            }
            internal static void PrepareStates()
            {
                lastKeyState = currentKeyState;
                currentKeyState = Keyboard.GetState();
                shiftPressed = IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.RightShift);
            }
            public static bool IsKeyDown(Keys key)
            {
                return currentKeyState.IsKeyDown(key);
            }
            internal static bool IsKeyPressed(Keys key)
            {
                return currentKeyState.IsKeyDown(key) && lastKeyState.IsKeyUp(key);
            }
            internal static string GetKeyInput()
            {
                string p = "";
                var v = currentKeyState.GetPressedKeys();
                if (v.Length > 0)
                    instance.lastEnterTime = 0;
                for (int i = 0; i < v.Length; i++)
                {
                    char tmp = GetChar(v[i]);
                    if (tmp != (char)0) p += tmp.ToString();
                }
                return p;
            }
        }
    }
}