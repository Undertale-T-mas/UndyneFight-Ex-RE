namespace UndyneFight_Ex.Server
{
    public class Time : Command
    {
        public Time() : base("Time") { }

        public override void Processor(string[] args, Client? client)
        {
            if (client == null) return;
            if (args.Length == 0)
            {
                client.Reply("F format wrong, send again.");
                return;
            }
            client.Reply("S " + DateTime.UtcNow.Ticks);
        }
    }
}