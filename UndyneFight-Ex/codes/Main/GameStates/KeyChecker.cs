using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Entities.Player;

namespace UndyneFight_Ex
{
    public static partial class GameStates
    {
        #region keys
        public class KeyChecker
        {
            public KeyChecker()
            {
                allCheckers.Add(this);
                foreach (var kvp in defaultInput)
                {
                    IdentityChecker singleChecker = new();
                    singleChecker.ResetKeyList(kvp.Value);
                    _identityCheckers.Add(kvp.Key, singleChecker);
                }
            }
            public void Update(KeyboardState keyboardState)
            {
                foreach (KeyValuePair<InputIdentity, IdentityChecker> singleChecker in _identityCheckers)
                {
                    singleChecker.Value.Update(keyboardState);
                }
            }
            public bool IsKeyDown(InputIdentity identity)
            {
                return identity != InputIdentity.None && _identityCheckers[identity].IsKeyDown();
            }
            public bool IsKeyPressed(InputIdentity identity)
            {
                return identity != InputIdentity.None && _identityCheckers[identity].IsKeyPressed();
            }
            Dictionary<InputIdentity, IdentityChecker> _identityCheckers = new();

            public static Keys[] StringToKey(string text)
            {
                Keys[] keys = new Keys[1];
                if (string.IsNullOrEmpty(text))
                {
                    keys[0] = Keys.None;
                    return keys;
                }
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Keys));
                string[] keyValueTemp = text?.Split(',');
                var Length = keyValueTemp.Length;
                for (int i = 0; i < Length; i++)
                {
                    keys[i] = (Keys)converter.ConvertFromString(keyValueTemp[i]);
                }
                
                return keys;
            }

            static KeyChecker()
            {
                //if (File.Exists("Keybinds.txt"))
                if (false)
                {
                    //Load keybinds
                    string[] texts = File.ReadAllLines("Keybinds.txt");

                    for (int i = 0; i < 36; i++)
                    {
                        string cur = texts[i];
                        cur = cur[(cur.IndexOf(": ") + 1)..];
                        defaultInput.Add((InputIdentity)i, new(StringToKey(cur)));
                    }
                }
                else
                {
                    defaultInput.Add(InputIdentity.Confirm, new() { Keys.Enter, Keys.Z });
                    defaultInput.Add(InputIdentity.Cancel, new() { Keys.LeftShift, Keys.X });
                    defaultInput.Add(InputIdentity.Alternate, new() { Keys.Space });
                    defaultInput.Add(InputIdentity.Special, new() { Keys.C });
                    defaultInput.Add(InputIdentity.MainRight, new() { Keys.Right, Keys.OemSemicolon });
                    defaultInput.Add(InputIdentity.MainDown, new() { Keys.Down, Keys.L });
                    defaultInput.Add(InputIdentity.MainLeft, new() { Keys.Left, Keys.K });
                    defaultInput.Add(InputIdentity.MainUp, new() { Keys.Up, Keys.O });
                    defaultInput.Add(InputIdentity.SecondRight, new() { Keys.D });
                    defaultInput.Add(InputIdentity.SecondDown, new() { Keys.S });
                    defaultInput.Add(InputIdentity.SecondLeft, new() { Keys.A });
                    defaultInput.Add(InputIdentity.SecondUp, new() { Keys.W });
                    defaultInput.Add(InputIdentity.ThirdRight, new() { Keys.B });
                    defaultInput.Add(InputIdentity.ThirdDown, new() { Keys.V });
                    defaultInput.Add(InputIdentity.ThirdLeft, new() { Keys.C });
                    defaultInput.Add(InputIdentity.ThirdUp, new() { Keys.F });
                    defaultInput.Add(InputIdentity.FourthRight, new() { Keys.OemComma });
                    defaultInput.Add(InputIdentity.FourthDown, new() { Keys.M });
                    defaultInput.Add(InputIdentity.FourthLeft, new() { Keys.N });
                    defaultInput.Add(InputIdentity.FourthUp, new() { Keys.J });
                    defaultInput.Add(InputIdentity.FullScreen, new() { Keys.F4 });
                    defaultInput.Add(InputIdentity.ScreenShot, new() { Keys.F2 });
                    defaultInput.Add(InputIdentity.Number1, new() { Keys.D1 });
                    defaultInput.Add(InputIdentity.Number2, new() { Keys.D2 });
                    defaultInput.Add(InputIdentity.Number3, new() { Keys.D3 });
                    defaultInput.Add(InputIdentity.Number4, new() { Keys.D4 });
                    defaultInput.Add(InputIdentity.Number5, new() { Keys.D5 });
                    defaultInput.Add(InputIdentity.Number6, new() { Keys.D6 });
                    defaultInput.Add(InputIdentity.Number7, new() { Keys.D7 });
                    defaultInput.Add(InputIdentity.Number8, new() { Keys.D8 });
                    defaultInput.Add(InputIdentity.Number9, new() { Keys.D9 });
                    defaultInput.Add(InputIdentity.Number0, new() { Keys.D0 });
                    defaultInput.Add(InputIdentity.Backspace, new() { Keys.Back });
                    defaultInput.Add(InputIdentity.Reset, new() { Keys.R });
                    defaultInput.Add(InputIdentity.Heal, new() { Keys.H });
                    defaultInput.Add(InputIdentity.Tab, new() { Keys.Tab });
                }
            }
            private static List<KeyChecker> allCheckers = new();
            private static Dictionary<InputIdentity, List<Keys>> defaultInput = new();
            public static List<Keys> GetIdentityKeys(InputIdentity identity)
            {
                return defaultInput[identity];
            }
            public static void SetIdentityKeys(InputIdentity identity, List<Keys> mission)
            {
                defaultInput[identity] = mission;
                allCheckers.ForEach(s =>
                {
                    s._identityCheckers[identity].ResetKeyList(mission);
                });
            }
        }
        public static bool WordsChanged { get; private set; } = false;
        public static char CharInput { get; private set; }
#if DEBUG
        public static float KeyCheckTime1 { get; private set; }
        public static float KeyCheckTime2 { get; private set; }
#endif
        internal static KeyboardState currentKeyState2;
        internal static KeyboardState lastKeyState2;

        private static KeyChecker checker120f = new(), checker = new();

        /// <summary>
        /// 一个键盘操作录制器(播放)或者是一个键盘操作控制器(回放)
        /// </summary>
        private static Entity keyEventBuffer;

        internal static char KeysUpdate()
        {
            KeyboardState currentKeyState;
            currentKeyState = Keyboard.GetState();

            bool shift_pressed = currentKeyState.IsKeyDown(Keys.LeftShift) || currentKeyState.IsKeyDown(Keys.RightShift);
            //   if (MapDrawSwitch.get_type()) return (char)1;
            for (int i = 0; i < 256; i++)
            {
                Keys t = (Keys)i;
                if (IsKeyPressed120f(t))
                {
                    WordsChanged = true;
                    if (i < 91 && i > 64)
                    {
                        return (char)((shift_pressed ? 0 : 32) + i);
                    }
                    else if (i > 47 && i < 58) return (char)i;
                    else switch (i)
                        {
                            case 188:
                                if (shift_pressed) return '<';
                                return ',';
                            case 189:
                                if (shift_pressed) return '_';
                                return '-';
                            case 190:
                                if (shift_pressed) return '>';
                                return '.';
                            case 187:
                                if (shift_pressed) return '+';
                                return '=';
                            case 191:
                                if (shift_pressed) return '?';
                                return '/';
                            case 186:
                                if (shift_pressed) return ':';
                                return ';';
                            case 0x20:
                                return (char)0x20;
                            case 13:
                                return (char)13;
                        }
                }
            }
            WordsChanged = false;
            return (char)1;
        }
        internal static void KeysUpdate2()
        {
#if DEBUG
            Stopwatch stopwatch = new();
            stopwatch.Start();
#endif
            lastKeyState2 = currentKeyState2;
            currentKeyState2 = Keyboard.GetState();

#if DEBUG
            if (IsKeyDown(Keys.LeftControl))
            {
                if (IsKeyPressed120f(Keys.D1)) GameMain.GameSpeed = 0.02f;
                if (IsKeyPressed120f(Keys.D2)) GameMain.GameSpeed = 0.05f;
                if (IsKeyPressed120f(Keys.D3)) GameMain.GameSpeed = 0.1f;
                if (IsKeyPressed120f(Keys.D4)) GameMain.GameSpeed = 0.25f;
                if (IsKeyPressed120f(Keys.D5)) GameMain.GameSpeed = 0.5f;
                if (IsKeyPressed120f(Keys.D6)) GameMain.GameSpeed = 0.7f;
                if (IsKeyPressed120f(Keys.Y)) GameMain.GameSpeed = 0.85f;
                if (IsKeyPressed120f(Keys.D7)) GameMain.GameSpeed = 1f;
                if (IsKeyPressed120f(Keys.D8)) GameMain.GameSpeed = 1.5f;
                if (IsKeyPressed120f(Keys.D9)) GameMain.GameSpeed = 2f;
            }
            if (IsKeyPressed120f(Keys.H) && CurrentScene is FightScene) (CurrentScene as FightScene).PlayerInstance.hpControl.Regenerate();
#endif  
            if (IsKeyPressed120f(Keys.F2) && isInBattle)
            {
                EndFight();
            }
            if (isInBattle)
            {
                keyEventBuffer?.Update();
            }

            if (GameMain.Update120F)
            {
                checker.Update(currentKeyState2);
            }
            checker120f.Update(currentKeyState2);

#if DEBUG

            KeyCheckTime2 = (float)stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Stop(); 
#endif
        }
        public static bool IsKeyPressed(InputIdentity key)
        {
            return checker.IsKeyPressed(key);
        }
        public static bool IsKeyPressed120f(InputIdentity key)
        {
            return checker120f.IsKeyPressed(key);
        }
        public static bool IsKeyDown(InputIdentity key)
        {
            return checker120f.IsKeyDown(key);
        }
        public static bool IsKeyPressed120f(Keys key)
        {
            return currentKeyState2.IsKeyDown(key) && lastKeyState2.IsKeyUp(key);
        }
        public static bool IsKeyDown(Keys key)
        {
            return currentKeyState2.IsKeyDown(key);
        }
        #endregion
    }
}