namespace UndyneFight_Ex.Server
{
    public class SongUpdate : Command
    {
        public SongUpdate( ) : base("SongUpdate") { }

        public override void Processor(string[] args, Client? client)
        {
            if (client == null || args.Length <= 0) return;
            SongResultUpload.PushSong(args[0], client);
        }
    }
}