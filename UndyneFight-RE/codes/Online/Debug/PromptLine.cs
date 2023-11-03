using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI.DEBUG
{
    public partial class PromptLine : Entity
    {
        private string _str;
        public PromptLine(string str) { this.Build(_str = str); } 

        private List<PromptBlock> promptBlocks = new();

        private static string[] Split(string? str)
        {
            if (string.IsNullOrEmpty(str)) return new string[] { };
            List<string> results = new();
            StringBuilder last = new();
            int bracketDepth = 0;
            bool rawCommand = false;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == '\\' && bracketDepth == 0)
                {
                    rawCommand = true;
                    results.Add(last.ToString());
                    last.Clear();
                    continue;
                }
                else if(!rawCommand && c == ' ')
                {
                    results.Add(last.ToString());
                    last.Clear();
                    continue; 
                }
                if (c == '{') bracketDepth++;
                else if (c == '}') bracketDepth--;
                last.Append(c);
            }
            if (last.Length > 0) results.Add(last.ToString());
            return results.ToArray();
        }
        internal CommandState CommandState { get; private set; } = CommandState.Unknown;
        public void Build(string str)
        {
            if (string.IsNullOrEmpty(str)) return;
            string[] list = Split(str);
            while(promptBlocks.Count > list.Length)
            {
                promptBlocks.RemoveAt(promptBlocks.Count - 1);
            }
            int i = 0;
            Semantics semantic = new();
            for (; i < promptBlocks.Count; i++)
            {
                string curs = list[i];
                if (promptBlocks[i].text == curs)
                {
                    promptBlocks[i].Analyze(semantic);
                    continue;
                }
                promptBlocks[i].text = curs;
                promptBlocks[i].TextUpdate(semantic);
            }
            for (; i < list.Length; i++)
            {
                promptBlocks.Add(new(semantic, list[i]));
            }
            CommandState = semantic.CurrentState;
        }
        public void SetText(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                this._str = text;
                this.promptBlocks.Clear();
                return;
            }
            if (_str == text) return;
            _str = text;
            Build(text);
        }

        public Vector2 Location { get; set; }

        public int LineCount { get; set; } = 0;

        public float MaxY { get; set; } = 610;

        public override void Draw()
        {
            Vector2 pos = Location;
            int lcount, ltotal = 1;
            foreach(var promptBlock in promptBlocks)
            {
                if (pos.Y > MaxY) return;
                pos = promptBlock.Draw(pos, out lcount);
                ltotal += lcount;
            }
            LineCount = ltotal;
        }

        public override void Update()
        { 
        }

        public static Queue<string> Memories = new();
    }
}