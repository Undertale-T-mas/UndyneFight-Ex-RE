using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UndyneFight_Ex.Entities;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Remake.Components;
using UndyneFight_Ex.Remake.Effects;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.UserService;
using col = Microsoft.Xna.Framework.Color;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.Remake.Network;
using System.Security.Principal;

namespace UndyneFight_Ex.Remake.UI.DEBUG
{
    public class PromptDialog : Entity
    { 
        public static List<PromptLine> promptLines = new List<PromptLine>();
        public override void Update()
        {
            if (PromptLine.Memories.Count == 0) return;
            string str = PromptLine.Memories.Dequeue();
            promptLines.Add(new PromptLine(str));
        }

        public override void Draw()
        {
            int start = Math.Max(0, promptLines.Count - 16);
            Vector2 pos = new(50, 35);
            for(int i = start; i < promptLines.Count; i++)
            {
                promptLines[i].Location = pos;
                pos.Y += 40;
                promptLines[i].Draw();
            }
        }
    }
}