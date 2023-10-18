using System.Text;

namespace UndyneFight_Ex.Server
{
    public abstract class Command
    {
        public Command(string title)
        {
            Title = title;
        }
        public bool Log { get; init; } = true;
        public string Title { get; init; }
        public abstract void Processor(string[] args, Client? client);

        private static Dictionary<string, Command> commands = new();
        public static void AddCommand(Command command)
        {
            commands.Add(command.Title, command);
        }

        internal static Command? GetCommand(string str)
        {
            if (commands.ContainsKey(str)) return commands[str];
            return null;
        }

        internal static string[] Split(string? str)
        {
            if (string.IsNullOrEmpty(str)) return new string[] { };
            List<string> results = new();
            StringBuilder last = new();
            int bracketDepth = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == '\\' && bracketDepth == 0)
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
    }
}