namespace UndyneFight_Ex.Server
{
    public class KeepAlive : Command
    {
        public KeepAlive() : base("keepAlive")
        {
        }

        public override void Processor(string[] args, Client? client)
        {
            if (client == null) return;
            if(args.Length == 0)
            {
                client.Reply("F format wrong, send again.");
                return;
            }
            string arg0 = args[0];
            if (client.BindUser == null)
                client.Reply("S none");
            else
                client.Reply("S alive message received.");
        }
    }
}