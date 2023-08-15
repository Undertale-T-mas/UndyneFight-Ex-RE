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
            string arg0 = args[0]; 
            client.Reply("S alive message received.");
        }
    }
}