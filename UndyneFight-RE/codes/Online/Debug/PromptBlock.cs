using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using System;

namespace UndyneFight_Ex.Remake.UI.DEBUG
{
    public partial class PromptLine
    {
        private struct PromptBlock
        {
            public col color;
            public string text;

            private static readonly vec2 spaceSize;
            private static readonly GLFont font;
            static PromptBlock()
            {
                font = FightResources.Font.NormalFont;
                spaceSize = font.MeasureChar(' ');
            }

            public PromptBlock(string text)
            {
                this.Size = font.SFX.MeasureString(text);
                this.text = text;
                this.color = Analyze(text);
            }
            public void SetText(string text)
            {
                if (text == this.text) return;
                this.Size = font.SFX.MeasureString(text);
                this.text = text;
                this.color = Analyze(text);
            }

            private static col Analyze(string text)
            {
                return PromptLine.Analyze(text);
            }

            public vec2 Size { get; private set; }

            public vec2 Draw(vec2 position)
            {
                font.Draw(this.text, position, this.color);

                return position + Size + spaceSize;
            }
        }

        private static col Analyze(string text)
        {
            throw new NotImplementedException();
        }

    }
}