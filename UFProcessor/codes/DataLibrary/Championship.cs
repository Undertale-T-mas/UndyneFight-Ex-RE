using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.UserService;
using UFData;
using UndyneFight_Ex.ChampionShips;

namespace UndyneFight_Ex.Server
{
    public static class ChampionshipManager
    {
        private static List<ChampionshipInfo> championships = new();
        private static bool _loaded = false;
        private static void TryLoad()
        {
            //championships.RemoveAll(s=>s.EndTime < DateTime.UtcNow);
            if(_loaded) return; 
            _loaded = true;

            FileStream stream = new("Data/Championship/data.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new(stream);
            string str = sr.ReadToEnd();
            if (string.IsNullOrEmpty(str)) {
                sr.Dispose();
                stream.Dispose();
                return;
            }
            championships = JsonSerializer.Deserialize<List<ChampionshipInfo>>(str) ?? throw new FileLoadException();
            sr.Dispose();
            stream.Dispose();
        }
        public static void Save()
        {
            FileStream stream = new("Data/Championship/data.txt", FileMode.OpenOrCreate, FileAccess.Write);
            stream.SetLength(0);
            stream.Write(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(championships)));
            stream.Flush();
            stream.Dispose();
        }
        public static void Insert(string? info)
        {
            TryLoad();
            if(string.IsNullOrEmpty(info)) return;
            championships.Add(JsonSerializer.Deserialize<ChampionshipInfo>(info) ?? throw new ArgumentException());
        }
        public static string SignUp(User user, string championshipName, string divName)
        {
            TryLoad(); 
            ChampionshipInfo? championship = championships.Find(s => s.Name == championshipName);
            if(championship == null) { return "F championship not exist"; }
            if (!championship.Divisions.ContainsKey(divName)) return "F division not exist";
            if (championship.Participants.ContainsKey(user.UUID)) return "F already signed up-" + championship.Participants[user.UUID];
            championship.Participants.Add(user.UUID, divName);
            return "S successfully signed up";
        }
        public static bool PushScore(User user, SongPlayData data)
        {
            TryLoad();
            string songName = data.Name;
            ChampionshipInfo? championShip = championships.Find(s => {
                foreach(var v in s.Divisions.Values)
                {
                    if(v.Info.ContainsKey(songName)) return true;
                }
                return false;
            });
            if (championShip == null) return true;
            DivisionInformation? curDiv = null;
            foreach (var v in championShip.Divisions.Values)
                if (v.Info[songName].Item2 == data.Difficulty) { curDiv = v; break; }
            if (curDiv == null) return true;
            UFConsole.WriteLine($"Find championship {championShip.Name}, user {user.Name} in {curDiv} has updated score.");
            return  curDiv.Scoreboard.PushScore(user, curDiv, data);
        }
        internal static string EnquireInfo()
        {
            TryLoad();
            if (championships.Count == 0) return "F there is no championship";
            else return "S " + JsonSerializer.Serialize (championships);
        }
        internal static string Enquire(User? bindUser, string arg)
        {
            TryLoad();
            if (bindUser == null) return "F please login first";
            ChampionshipInfo? cur = championships.Find(s => s.Name == arg);
            if (cur == null) return "F there is such championship";
            if (!cur.Available) return "F championship not available";
            if (!cur.Participants.ContainsKey(bindUser.UUID)) return "F hasn't signed up";
            string participantDivision = cur.Participants[bindUser.UUID];
            DivisionInformation division = cur.Divisions[participantDivision];
            return "S " + JsonSerializer.Serialize(division.Scoreboard);
        }

        internal static bool Exist(string? info)
        {
            TryLoad();
            if (string.IsNullOrEmpty(info)) return false;
            ChampionshipInfo cinfo =  JsonSerializer.Deserialize<ChampionshipInfo>(info) ?? throw new ArgumentException();
            if(cinfo == null) return false;
            foreach(ChampionshipInfo obj in championships)
            {
                if(obj.Name == cinfo.Name) return true;
            }
            return false;   
        }

        internal static string EnquireScore(User? user, string championshipName, string divisionName)
        {
            TryLoad();
            if (user == null) return "F please login first";
            ChampionshipInfo? cinfo = championships.Find(s => s.Name == championshipName);
            if (cinfo == null) return "F there is no such championship";
            if (!cinfo.Divisions.ContainsKey(divisionName)) return "F no such division";
            return "S " + JsonSerializer.Serialize(cinfo.Divisions[divisionName].Scoreboard);
        }
    }
}