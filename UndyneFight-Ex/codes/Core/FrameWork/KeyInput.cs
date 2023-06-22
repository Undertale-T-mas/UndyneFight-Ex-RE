using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace UndyneFight_Ex
{
    public enum InputIdentity
    {
        None = 999999,
        Confirm = 0, // key enter and Z
        Cancel = 1, // key shift and X 
        Alternate = 2, // key Space
        Special = 3, // key C
        MainRight = 4,
        MainDown = 5,
        MainLeft = 6,
        MainUp = 7,
        SecondRight = 8,
        SecondDown = 9,
        SecondLeft = 10,
        SecondUp = 11,
        ThirdRight = 12,
        ThirdDown = 13,
        ThirdLeft = 14,
        ThirdUp = 15,
        FourthRight = 16,
        FourthDown = 17,
        FourthLeft = 18,
        FourthUp = 19,
        FullScreen = 20,
        ScreenShot = 21,
        Number1 = 22,
        Number2 = 23,
        Number3 = 24,
        Number4 = 25,
        Number5 = 26,
        Number6 = 27,
        Number7 = 28,
        Number8 = 29,
        Number9 = 30,
        Number0 = 31,
        Backspace = 32,
        Reset = 33,
        Heal = 34
    }
    public class IdentityChecker
    {
        List<Keys> checkList;
        public void ResetKeyList(List<Keys> checkList)
        {
            this.checkList = checkList;
        }
        public bool IsKeyPressed()
        {
            return !lastPressed && curPressed;
        }
        public bool IsKeyDown()
        {
            return curPressed;
        }
        bool lastPressed = false, curPressed = false;
        public void Update(KeyboardState curState)
        {
            lastPressed = curPressed;
            curPressed = false;
            foreach (Keys key in checkList)
            {
                if (curState.IsKeyDown(key))
                {
                    curPressed = true;
                    break;
                }
            }
        }
    }
}