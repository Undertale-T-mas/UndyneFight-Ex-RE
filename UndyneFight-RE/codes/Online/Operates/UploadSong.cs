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
            UFSocket<Empty> sender = new((t) => {
                ;
            });
            sender.SendRequest("SongUpdate\\" + JsonSerializer.Serialize(data));
        }
    }
}