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
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.Remake.Network;
using System.Security.Principal;
using UndyneFight_Ex.Remake.UI.DEBUG;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Collections;

namespace UndyneFight_Ex.Remake.UI
{
    public class CommandEditor : Entity
    {
        PromptLine line;
        public CommandEditor() {
            this.AddChild(line = new PromptLine(string.Empty) { 
                Location = new(50, 626),
                MaxY = 888
            });
            UpdateIn120 = true;
        }
        public override void Draw()
        { 

        }

        string current = string.Empty;
        int index = 0;

        int usageIndex = 0;
        List<string> history = new() { "" };
        public override void Update()
        {
            char ch = GameStates.CharInput;
            bool changed = false;
            if (ch != (char)1 && ch != (char)13)
            {
                changed = true;
                current = current[..index] + ch + current[index..];
                index++;
            }
            else if (GameStates.IsKeyPressed120f(Keys.Back))
            {
                if (index == 0) return;
                changed = true;
                current = current[..(index - 1)] + current[index..];
                index--;
            }

            if (changed)
            {
                history[usageIndex] = current;
                line.SetText(current);
            }
            if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
            {
                Fight.Functions.PlaySound(FightResources.Sounds.select);
                RunCommand(current, line.CommandState);
                history[history.Count - 1] = current;
                history.Add(string.Empty);
                usageIndex = history.Count - 1;
                line.SetText(string.Empty);
                current = string.Empty;
                index = 0;
            }
            else if (GameStates.IsKeyPressed120f(Keys.Down))
            {
                if (usageIndex == history.Count - 1) return;
                history[usageIndex] = current;
                usageIndex++;
                current = history[usageIndex];
                line.SetText(current);
                index = 0;
            }
            else if (GameStates.IsKeyPressed120f(Keys.Up))
            {
                if (usageIndex == 0) return;
                history[usageIndex] = current;
                usageIndex--;
                current = history[usageIndex];
                line.SetText(current);
                index = 0;
            }
        }

        private static void RunCommand(string current, CommandState state)
        {
            if (state == CommandState.Unknown)
            {
                PromptLine.Memories.Enqueue("Warn >> No such command!");
            }
            else if(state == CommandState.Raw)
            {
                UFSocket<Empty> obj = new UFSocket<Empty>((t) => { });
                current = current.Replace(' ', '\\');
                obj.SendRequest(current);
            }
        }
    }
}