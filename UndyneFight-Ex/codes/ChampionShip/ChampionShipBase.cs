using System;
using System.Collections.Generic;
using UFData;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.ChampionShips
{
    public class ChampionShip
    {
        public ChampionShip(SongSet fightSet)
        {
            this.fightSet = fightSet;
        }

        private readonly SongSet fightSet;
        public SongSet Fights => fightSet;

        public string Title { get => title; set => title = value; }
        public string SubTitle { get => subTitle; set => subTitle = value; }
        public string EditorName { get => editorName; set => editorName = value; }
        public string Introduce { get => introduce; set => introduce = value; }
        public string IconPath { get => iconPath; set => iconPath = value; }

        public HashSet<string> DivisionExist { get; set; }

        private string title, subTitle, editorName, introduce, iconPath;

        public enum ChampionShipStates
        {
            NotStart = 1,
            Starting = 2,
            NotAvailable = 3,
            End = 4
        }

        public Func<ChampionShipStates> CheckTime { get => checkTime; set => checkTime = value; }
        private Func<ChampionShipStates> checkTime;

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        /*public ChampionshipInfo ToInfo()
        {
            return new ChampionshipInfo(this.title, Start, End, GetDiv());
        }

        private Dictionary<string, DivisionInformation> GetDiv()
        {
            Type[] types = this.fightSet.Values;
            IChampionShip[] songs = new IChampionShip[types.Length];
            for(int i = 0; i < types.Length; i++)
            {
                songs[i] = Activator.CreateInstance(types[i]) as IChampionShip;
            }
            Dictionary<string, DivisionInformation> result = new();
            for(int i = 0; i<types.Length;  i++)
            {
                foreach(var v in songs[i].DifficultyPanel)
                {
                    if (!result.ContainsKey(v.Key)) result.Add(v.Key, new(v.Key, new(), new()));

                    var map = result[v.Key].Info;
                    map.Add(songs[i].GameContent.FightName, new(i, v.Value));
                }
            }
            return result;
        }*/
    }
}