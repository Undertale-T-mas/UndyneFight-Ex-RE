using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.Remake.Network;
using UndyneFight_Ex.Remake.UI.DEBUG;
using static UndyneFight_Ex.GameStates;

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
            char ch = CharInput;
            bool changed = false;
            if (ch != (char)1 && ch != (char)13)
            {
                changed = true;
                current = current[..index] + ch + current[index..];
                index++;
            }
            else if (IsKeyPressed120f(Keys.Back))
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
            if (IsKeyPressed120f(InputIdentity.Confirm))
            {
                Fight.Functions.PlaySound(FightResources.Sounds.select);
                RunCommand(current, line.CommandState);
                history[^1] = current;
                history.Add(string.Empty);
                usageIndex = history.Count - 1;
                line.SetText(string.Empty);
                current = string.Empty;
                index = 0;
            }
            else if (IsKeyPressed120f(Keys.Down))
            {
                if (usageIndex == history.Count - 1) return;
                history[usageIndex] = current;
                usageIndex++;
                current = history[usageIndex];
                line.SetText(current);
                index = 0;
            }
            else if (IsKeyPressed120f(Keys.Up))
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
                UFSocket<Empty> obj = new((t) => { });
                current = current.Replace(' ', '\\');
                obj.SendRequest(current);
            }
        }
    }
}