using System.Net;
using System.Net.Sockets;
using System.Text;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Server
{
    public class Enquire : Command
    {
        public Enquire( ) : base("Enquire")
        {
        }

        public override void Processor(string[] args, Client? client)
        {
            if (client == null)
            {
                return;
            }
            if (args.Length < 1) goto A; 
            string arg0 = args[0]; 
            switch (arg0)
            {
                case "Scoreboard":
                    if (args.Length < 3)
                        goto A;
                    string arg1 = args[1];
                    bool success = int.TryParse(args[2], out int argInt);
                    if(!success) goto A;
                    client.Reply(SongResultUpload.Enquire(client.BindUser, arg1, (Difficulty)argInt));
                    return;
                case "Championship":
                    client.Reply(ChampionshipManager.Enquire(client.BindUser, args[1]));
                    return;
                case "Self":
                    if(client.BindUser == null)
                    {
                        client.Reply("F Please login first!");
                        return;
                    }
                    string? res = UserLibrary.Check(client.BindUser.UUID);
                    if (string.IsNullOrEmpty(res))
                    {
                        client.Reply("E Unexpected exception");
                        return;
                    }
                    client.Reply("S " + res);

                    return;

                default:
                    client.Reply("E cannot enquire inexistent object");
                    break;
            }

        A:
            client.Reply("E argument wrong");
        }
    }
}