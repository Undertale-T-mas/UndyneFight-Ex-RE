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

namespace UndyneFight_Ex.Remake.UI.DEBUG
{ 
    internal enum CommandState
    {
        Unknown = -1,
        Raw = 0,
        Data = 1,
        UFShell = 2,
        Reply = 3
    }
    internal class Semantics
    {
        private CommandState state = CommandState.Unknown;
        private Command _command;
        public Semantics() { }
        public string CurrentText { get; private set; }
        public object Extra { get; set; }
        public CommandState CurrentState => state;

        public col Analyze(string text)
        {
            if(_command == null) {
                _command = Command.TryAnalyze(text);
                if (_command != null) this.state = _command.CommandState;
            }
            if(_command != null)
            {
                CurrentText = text;
                return _command.Analyze(this);
            }
            else
            {
                return PromptLine.Analyze(text);
            }
        }
    }
}