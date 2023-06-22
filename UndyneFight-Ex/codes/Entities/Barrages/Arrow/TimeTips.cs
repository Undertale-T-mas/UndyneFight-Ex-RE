using Microsoft.Xna.Framework;
using System;

namespace UndyneFight_Ex.Entities
{
    public partial class Arrow
    {
        private class TimeTips : Entity
        {
            string text;
            Vector2 pos, fadeSpeed;
            Color color;
            public TimeTips(Vector2 pos, Color color, string text, Vector2 fadeSpeed)
            {
                this.fadeSpeed = fadeSpeed;
                this.color = color;
                this.pos = pos;
                this.text = text;
                UpdateIn120 = true;
            }
            float appearTime = 0;
            float alpha;
            public override void Update()
            {
                appearTime += 0.5f;
                Centre = pos + MathF.Pow(appearTime / 2, 1.7f) * fadeSpeed;
                alpha = appearTime > 20f ? (40 - appearTime) * 0.05f : 1;
                if (alpha <= 0) Dispose();
            }
            public override void Draw()
            {
                GlobalResources.Font.NormalFont.CentreDraw(text, pos, color * alpha, 0.5f, 0.5f);
            }
        }
    }
}