using System;
using System.Collections.Generic;
using col = Microsoft.Xna.Framework.Color;

namespace UndyneFight_Ex.Remake.UI.DEBUG
{
    internal class ServerInfo : Command
    {
        private Dictionary<string, col> colorMap = new();
        private Dictionary<string, col> colorMap2 = new();

        public ServerInfo() : base("Server")
        { 
            colorMap.Add("S", col.Lime);
            colorMap.Add("F", col.MonoGameOrange);
            colorMap.Add("E", col.Red);
            colorMap.Add("D", col.LightGray);
            colorMap2.Add("S", col.Lerp(col.Lime, col.White, 0.7f));
            colorMap2.Add("F", col.Lerp(col.MonoGameOrange, col.White, 0.7f));
            colorMap2.Add("E", col.Red);
            colorMap2.Add("D", col.LightGray);
        }

        internal override col Analyze(Semantics semantics)
        {
            if (semantics.CurrentText == "Server" && semantics.Extra == null)
            {
                return col.Magenta;
            }
            if (semantics.CurrentText == ">>") return col.Silver;
            if (semantics.Extra == null)
            {
                string s = semantics.CurrentText;
                if (colorMap.ContainsKey(s)) { semantics.Extra = colorMap2[s]; return colorMap[s]; }
                else throw new Exception(); 
            }
            return (col)semantics.Extra;
        }
    }
    internal class WarnInfo : Command
    {
        public WarnInfo() : base("Warn")
        {
            CommandState = CommandState.Reply;
        }

        internal override col Analyze(Semantics semantics)
        {
            if (semantics.CurrentText == ">>") return col.Silver;
            return col.Red;
        }
    }
    internal abstract class Command
    {
        protected string Title { private get; init; }
        public CommandState CommandState { get; protected set; } = CommandState.Unknown;

        static Dictionary<string, Command> _commands = new();
        internal static Command TryAnalyze(string text)
        {
            if(_commands.ContainsKey(text)) return _commands[text];
            return null;
        }
        static void PushCommand(Command command)
        {
            _commands.Add(command.Title, command);
        }
        static Command()
        {
            PushCommand(new ServerInfo());
            PushCommand(new WarnInfo());
            PushCommand(new LogCommand());
            PushCommand(new KeepAliveCommand());
            PushCommand(new EnquireCommand());
            PushCommand(new ChampionshipCommand());
            PushCommand(new TimeCommand());
        }

        protected Command(string title)
        {
            Title = title;
        }

        internal abstract col Analyze(Semantics semantics);
    }
}