﻿using System.IO;
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
            if(_loaded) return; 
            FileStream stream = new("Data/Championship/data.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new(stream);
            championships = JsonSerializer.Deserialize<List<ChampionshipInfo>>(sr.ReadToEnd()) ?? throw new FileLoadException();
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
        public static string SignUp(User? user, string championshipName, string divName)
        {
            if (user == null) return "F please login first";
            ChampionshipInfo? championship = championships.Find(s => s.Name == championshipName);
            if(championship == null) { return "F championship not exist"; }
            if (!championship.Divisions.ContainsKey(divName)) return "F division not exist";
            championship.Participants.Add(user.UUID, divName);
            return "S successfully signed up";
        }
        public static void PushScore(User user, SongPlayData data)
        {
            string songName = data.Name;
            ChampionshipInfo? championShip = championships.Find(s => {
                foreach(var v in s.Divisions.Values)
                {
                    if(v.Info.ContainsKey(songName)) return true;
                }
                return false;
            });
            if (championShip == null) return;
            DivisionInformation? curDiv = null;
            foreach (var v in championShip.Divisions.Values)
                if (v.Info[songName].Item2 == data.Difficulty) { curDiv = v; break; }
            if (curDiv == null) return;
            curDiv.Scoreboard.PushScore(user, curDiv, data);
        }

        internal static string Enquire(User? bindUser, string arg)
        {
            if (bindUser == null) return "F please login first";
            ChampionshipInfo? cur = championships.Find(s => s.Name == arg);
            if (cur == null) return "F there is such championship";
            if (!cur.Available) return "F championship not available";
            if (!cur.Participants.ContainsKey(bindUser.UUID)) return "F hasn't signed up";
            string participantDivision = cur.Participants[bindUser.UUID];
            DivisionInformation division = cur.Divisions[participantDivision];
            return "S " + JsonSerializer.Serialize(division.Scoreboard);
        }
    }
}