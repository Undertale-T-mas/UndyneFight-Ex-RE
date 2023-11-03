using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Remake.Texts;

namespace UndyneFight_Ex.Remake
{
    public static class TextUtils
    {
        public static Text DrawText(float speed, string text, Vector2 location, bool mute = false, params TextEffect[] effects)
        {
            List<TextEffect> effects1 = new()
            {
                new TextSpeedEffect(speed)
            };
            effects1.AddRange(effects);

            Text tp = new("$" + text, location, effects1)
            {
                PlaySound = !mute
            };
            return tp;
        }
        public static Text DrawText(string text, Vector2 location, params TextEffect[] effects)
        {
            return DrawText(0.0f, text, location, true, effects);
        }
    }
}
