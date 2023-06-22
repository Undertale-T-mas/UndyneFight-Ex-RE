using System;

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

        public string Title { internal get => title; set => title = value; }
        public string SubTitle { internal get => subTitle; set => subTitle = value; }
        public string EditorName { internal get => editorName; set => editorName = value; }
        public string Introduce { internal get => introduce; set => introduce = value; }
        public string IconPath { internal get => iconPath; set => iconPath = value; }

        private string title, subTitle, editorName, introduce, iconPath;

        public enum ChampionShipStates
        {
            NotStart = 1,
            Starting = 2,
            NotAvailable = 3,
            End = 4
        }

        public Func<ChampionShipStates> CheckTime { internal get => checkTime; set => checkTime = value; }
        private Func<ChampionShipStates> checkTime;
    }
}