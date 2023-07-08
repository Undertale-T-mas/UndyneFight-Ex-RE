using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UndyneFight_Ex.Fight;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Globalization;
using UndyneFight_Ex.Remake.UI;
using System.Threading;

namespace UndyneFight_Ex.Remake
{
    public static class TextUtils
    {
        public static TextPrinter DrawText(float speed, string text, Vector2 location, bool mute = false, params TextAttribute[] textAttributes)
        {
            List<TextAttribute> textAttributes1 = new List<TextAttribute>
            {
                new TextSpeedAttribute(speed)
            };
            textAttributes1.AddRange(textAttributes);
            TextPrinter printer;
            GameStates.InstanceCreate(printer = new TextPrinter("$" + text, location, textAttributes1.ToArray()));
            if (mute) printer.PlaySound = false;
            return printer;
        }
        public static TextPrinter DrawText(string text, Vector2 location, params TextAttribute[] textAttributes)
        {
            return DrawText(10000f, text, location, true, textAttributes);
        }
    }
}
