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
        private class ScoreboardManager
        {
            public ScoreboardManager() {  
            }
            private SongScoreBoard Ready(string name)
            {
                if (_songs.ContainsKey(name)) return _songs[name];
                if(!File.Exists("Data/Scoreboard/" + name))
                {
                    SongScoreBoard board = new(name);
                    _songs.Add(name, board);
                    return board;
                }
                FileStream stream = new("Data/Scoreboard/" + name, FileMode.Open, FileAccess.Read);
                StreamReader sr = new(stream);

                SongScoreBoard scoreBoard = JsonSerializer.Deserialize<SongScoreBoard>(sr.ReadToEnd()) ?? throw new FileLoadException();
                _songs.Add(name, scoreBoard);
                return scoreBoard;
            }
            private Dictionary<string, SongScoreBoard> _songs = new();
            public void Insert(User user, SongPlayData data)
            {
                var board = Ready(data.Name);
                board.InsertData(user.UUID, data);
            }
            public string Enquire( string songName, Difficulty difficulty)
            {
                var board = Ready(songName);
                if (!board.DifficultyResults.ContainsKey(difficulty)) return "F Empty scoreboard.";
                UnitScoreBoard unitScoreBoard = board.DifficultyResults[difficulty];
                List<Tuple<string, int, SongSystem.SongResult>> answer = new();

                var rbt = unitScoreBoard.ScoreUnits;
                int len = rbt.Count;
                if (len <= 0) return "F Empty scoreboard."; 
                len = Math.Min(10, len);
                for(int i = 0; i < len; i++)
                {
                    var res = rbt[i];
                    answer.Add(new(UserLibrary.NameOf(res.PlayerID), i, res.Data));
                }

                return "S " + JsonSerializer.Serialize(answer);
            }

            private void Save(SongScoreBoard scoreBoard)
            {
                FileStream stream = new("Data/Scoreboard/" + scoreBoard.SongName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.SetLength(0);
                stream.Write(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(scoreBoard)));
                stream.Flush();
                stream.Dispose();
            }
            public void Save()
            {
                foreach(var pair in _songs.Values)
                {
                    Save(pair);
                    if (pair.IsDead()) _songs.Remove(pair.SongName);
                }
            } 
        }
        private static ScoreboardManager scoreboardManager = new();
        public static void SaveAll()
        {
            scoreboardManager.Save();
        }
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

            scoreboardManager.Insert(user, data);
            if (ChampionshipManager.PushScore(user, data))
                client.Reply("S Song message received.");
            else client.Reply("F Error occured!");

            user.Save();
        }

        internal static string Enquire(User? user, string arg1, Difficulty arg2)
        {
            return scoreboardManager.Enquire( arg1, arg2) ;
        }
    }
}