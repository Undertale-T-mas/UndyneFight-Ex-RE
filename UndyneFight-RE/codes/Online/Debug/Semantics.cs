using col = Microsoft.Xna.Framework.Color;

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
            if (_command == null)
            {
                _command = Command.TryAnalyze(text);
                if (_command != null) this.state = _command.CommandState;
            }
            if (_command != null)
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