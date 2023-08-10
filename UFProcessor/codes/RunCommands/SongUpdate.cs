using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UndyneFight_Ex.Server
{
    public class SongUpdate : Command
    {
        public SongUpdate( ) : base("SongUpdate")
        {
        }

        public override void Processor(string[] args, Client? client)
        {
            if (client == null)
            {
                return;
            }
            if (args.Length <= 0) return;
            SongResultUpload.PushSong(args[0], client);
        }
    }
}