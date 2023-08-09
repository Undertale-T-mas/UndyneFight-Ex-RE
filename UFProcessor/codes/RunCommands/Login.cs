using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UndyneFight_Ex.Server
{
    public class Login : Command
    {
        public Login() : base("Log") { }
        public override void Processor(string[] args, Client? client)
        { 
            if (client == null) return; 
            if (args.Length <= 0) client.Reply("F message disturbed");
            else
            {
                string arg2 = args[0];
                if (arg2 == "in") client.Reply("S login success");
            }
        }
    }
}