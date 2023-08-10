using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.Server
{
    public static class SongResultUpload
    {
        public static void PushSong(string songDataJson, Client client) {
            if(client.BindUser == null)
            {
                client.Reply("E Please login first");
                return;
            }
            SongPlayData? data = JsonSerializer.Deserialize<SongPlayData>(songDataJson);
            if(data == null)
            {
                client.Reply("E Do not send empty data");
                return;
            }
            else if (data.IsCheat())
            {
                client.Reply("E Cannot not send cheat data");
                return;
            }
            else if(data.Name == null)
            {
                client.Reply("E Song must have name");
                return;
            }
            User user = client.BindUser;
            if (user.SongRecord == null) user.SongRecord = new();
            var record = user.SongRecord;
            if (!record.ContainsKey(data.Name)) record.Add(data.Name, new());

            record[data.Name].SongName = data.Name;
            record[data.Name].Push(data);
            client.Reply("S Song message received.");
            user.Save();
        }
    }
}