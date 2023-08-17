using System.Text.Json;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.Remake.Network
{
    public static class SongUpload
    { 
        public static void UploadSong(SongPlayData data)
        {
            if (PlayerManager.CurrentUser == null) return;
            KeepAliver.CheckAlive(null, (t) => {
                UFSocket<Empty> sender = new((t) => {
                    if (t.Info == "Please login first") { 
                        
                    }
                });
                sender.SendRequest("SongUpdate\\" + JsonSerializer.Serialize(data));
            });
        }
    }
}