using System.Text.Json;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.Network
{
    public static class SongUpload
    { 
        public static void UploadSong(SongPlayData data)
        {
            if (PlayerManager.CurrentUser == null) return;
            int count = 0;
            KeepAliver.CheckAlive(null, (t) => {
                UFSocket<Empty> sender = null;
                sender = new((t) => {
                    if (t.Info == "Please login first") { 
                        
                    }
                    else if (!t.Success)
                    {
                        count++;
                        if(count < 3)
                        {
                            sender.SendRequest("SongUpdate\\" + JsonSerializer.Serialize(data));
                        }
                    }
                });
                sender.SendRequest("SongUpdate\\" + JsonSerializer.Serialize(data));
            });
        }
    }
}