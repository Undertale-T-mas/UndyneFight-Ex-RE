using Rhythm_Recall.Engine;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;

namespace Rhythm_Recall.Waves
{
    public static class RedMay2021
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("Red May");

                if (AprilSettings.IsAprilFool)
                    fightSet.Push(typeof(AprilExtends.Seraphim));
                else
                    fightSet.Push(typeof(UnderFell));
                fightSet.Push(typeof(SuddenChange));
                //fightSet.Push(typeof(UnderFellPractice)); 
                return new ChampionShip(fightSet)
                {
                    Title = "Red May 2021",
                    SubTitle = "May.3 13:45-17:30",
                    EditorName = "T-mas",
                    Introduce = "It's a \"red\" championship",
                    //IconPath = "ChampionShips\\TCS",
                    IconPath = "ChampionShips\\red_cup",
                    CheckTime = () =>
                    {
                        DateTime s = DateTime.Now;
                        int dayType = s.Year < 2021 || (s.Year == 2021 && s.Month < 5)
                            ? -1
                            : s.Month == 5 && s.Year == 2021 && s.Day <= 2 ? -1 : s.Month == 5 && s.Year == 2021 && s.Day == 3 ? 0 : 1;
                        if (dayType == -1) return ChampionShip.ChampionShipStates.NotStart;
                        if (dayType == 1) return ChampionShip.ChampionShipStates.End;
                        TimeSpan t = s.TimeOfDay;
                        bool res = (t.Hours > 13 || t.Hours == 13 && t.Minutes >= 45) && (t.Hours < 15 || (t.Hours == 15 && t.Minutes < 43));
                        return res ? ChampionShip.ChampionShipStates.Starting : ChampionShip.ChampionShipStates.NotAvailable;
                    }

                };
            }
        }
    }
}