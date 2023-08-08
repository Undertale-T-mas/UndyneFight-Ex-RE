using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UndyneFight_Ex.Server
{
    public abstract class Command
    {
        public Command(string title)        {
            Title = title; 
        } 
        public string Title { get; init; } 
        public abstract void Processor(string[] args, Client? client);

        private static Dictionary<string, Command> commands = new();
        public static void AddCommand(Command command) {
            commands.Add(command.Title, command);
        }

        internal static Command GetCommand(string str)
        {
            return commands[str];
        }
    }
}