using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.UserService
{
    public class ChampionshipManager : ISaveLoad
    {
        Dictionary<string, string> championshipData = new();

        public List<ISaveLoad> Children => throw new NotImplementedException();

        public void Load(SaveInfo info)
        {
            foreach (var v in info.Nexts)
                championshipData.Add(v.Key, v.Value.StringValue);
        }

        public SaveInfo Save()
        {
            SaveInfo info = new("ChampionShips{");
            foreach (var v in championshipData)
                info.PushNext(new(v.Key + ":" + v.Value));
            return info;
        }

        public void SignUp(string title, string div)
        {
            championshipData.Add(title, div);
        }

        public bool InChampionship(string championship)
        {
            return championshipData.ContainsKey(championship);
        }

        public string ChampionshipDivision(string championship)
        {
            if (championshipData.ContainsKey(championship))
                return championshipData[championship];
            else return null;
        }
    }

}