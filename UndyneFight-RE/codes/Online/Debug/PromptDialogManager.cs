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
        private bool _updated = true;
        float delta = 0.0f;
        float deltaBuffer = 0.0f;

        public PromptDialog()
        {
            UpdateIn120 = true;
        }
        public override void Update()
        {
            if (MouseSystem.MouseWheelChanged != 0)
            {
                _updated = false;
                deltaBuffer += MouseSystem.MouseWheelChanged;
            }
            delta += deltaBuffer * 0.2f;
            deltaBuffer *= 0.8f;
            if (delta > 0) delta *= 0.8f;
            if (PromptLine.Memories.Count == 0) return;
            string str = PromptLine.Memories.Dequeue();
            promptLines.Add(new PromptLine(str));
            _updated = true;
        }
         
        public override void Draw()
        {
            Vector2 pos = new(50, 35 + delta);
             
            for (int i = 0; i < promptLines.Count; i++)
            {
                float nextY = 38 * promptLines[i].LineCount + pos.Y; 
                if ((nextY >= 34 && pos.Y < 610) || promptLines[i].LineCount == 0)
                {
                    promptLines[i].Location = pos;
                    promptLines[i].Draw();
                }
                pos.Y = nextY;
                if (pos.Y > 1610)
                {
                    pos.Y = 1610;
                    break;
                }
            }
            if (pos.Y > 600 && _updated) delta -= 2f + (pos.Y - 600) * 0.2f;
            if (pos.Y < 124 && delta < -10) delta += (124 - pos.Y) * (pos.Y < 67 ? 0.4f : 0.2f);
        }
    }
}