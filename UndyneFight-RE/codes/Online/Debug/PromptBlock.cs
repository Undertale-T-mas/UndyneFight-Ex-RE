using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using System.Collections.Generic;

namespace UndyneFight_Ex.Remake.UI.DEBUG
{
    public partial class PromptLine
    {
        private class PromptBlock
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

            public PromptBlock(Semantics semantics, string text)
            {
                this.Size = font.SFX.MeasureString(text);
                this.text = text;
                this.color = Analyze(semantics, text);
            }
            public void TextUpdate(Semantics semantics)
            {
                this.Size = font.SFX.MeasureString(text); 
                this.color = Analyze(semantics, text);
            }
            public void Analyze(Semantics semantics)
            {
                this.color = Analyze(semantics, text);
            }

            private static col Analyze(Semantics semantics, string text)
            {
                return semantics.Analyze(text);
            }

            public vec2 Size { get; private set; }

            public vec2 Draw(vec2 position, out int lineChanged)
            {
                lineChanged = 0;
                float lastX = position.X;
                float nextX = position.X + Size.X; 

                string cur = this.text;
                if (nextX > 910)
                {
                    while (cur != null)
                    {
                        string show;
                        bool changeLine;
                        GetSuitableString(lastX, ref cur, out show, out changeLine);
                        font.Draw(show, position, this.color);
                        if (!changeLine)
                        {
                            position.X += font.SFX.MeasureString(show).X;
                        }
                        else
                        {
                            lastX = 50;
                            lineChanged++;
                            position.Y += 38;
                            position.X = 50;
                        }
                        if (position.Y > 610) return position; 
                    }
                }
                else
                {
                    font.Draw(cur, position, this.color);
                    position.X += this.Size.X;
                    cur = null;
                }
                if (position.X > 910)
                {
                    lineChanged++;
                    position.X = 50;
                    position.Y += 38;
                }
                position.X += spaceSize.X;

                return position;
            }

            private void GetSuitableString(float lastX, ref string cur, out string show, out bool changeline)
            {
                float unitLength = spaceSize.X;
                changeline = true;
                float total = 910 - lastX;
                int count = (int)(total / unitLength);
                if(count > cur.Length)
                {
                    show = cur;
                    cur = null;
                    changeline = false; ;
                    return;
                }
                show = cur[0..count];
                cur = cur[count..];
            }
        }

        public static col Analyze(string text)
        {
            if (!colInited)
            {
                ColorInit();
            }
            if(colorMap.ContainsKey(text)) return colorMap[text];
            return col.White;
        }
        private static Dictionary<string, col> colorMap;
        private static bool colInited = false;

        static void ColorInit()
        {
            col cur;
            colorMap = new Dictionary<string, col>
            {
                // 1. Local 
                { "Local", col.Wheat }, 
            };

            // 2. Punctuations
            cur = col.Silver;
            colorMap.Add(">>", cur);
            colorMap.Add(">", cur);
            colorMap.Add("<", cur);
            colorMap.Add(",", cur);
            colorMap.Add("~", cur);
            colorMap.Add(".", cur);
            colorMap.Add("/", cur);
            colorMap.Add("&", cur); 
        }
    }
}