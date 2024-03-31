using System;
using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;

namespace Rhythm_Recall.Waves
{
    public static class MidAutumn2021
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("Mid Autumn I");
                fightSet.Push(typeof(SpaceDrift));
                fightSet.Push(typeof(SustenanceOfMoon));
                fightSet.Push(typeof(SpiningSetsugekka));

                int year = 2021;
                int month = 9;
                int day = 21;

                int startHour = 13, startMinute = 30, endHour = 17, endMinute = 0;

                return new ChampionShip(fightSet)
                {
                    Title = "Mid Autumn Feast 2021",
                    SubTitle = "2021 Mid Autumn, 1:30-6:00",
                    EditorName = "T-mas",
                    Introduce = "Have fun in mid autumn!",
                    //IconPath = "ChampionShips\\TCS",
                    IconPath = "ChampionShips\\mid aut fes",

                    CheckTime = () =>
                    {
                        DateTime s = DateTime.Now;
                        int dayType = s.Year < year || (s.Year == year && s.Month < month)
                            ? -1
                            : s.Month == month && s.Year == year && s.Day < day ? -1 : s.Month == month && s.Year == year && s.Day == day ? 0 : 1;
                        if (dayType == -1) return ChampionShip.ChampionShipStates.NotStart;
                        if (dayType == 1) return ChampionShip.ChampionShipStates.End;
                        TimeSpan t = s.TimeOfDay;
                        bool res = (t.Hours > startHour || t.Hours == startHour && t.Minutes >= startMinute) &&
                                   (t.Hours < endHour || (t.Hours == endHour && t.Minutes < endMinute));
                        return res ? ChampionShip.ChampionShipStates.Starting : ChampionShip.ChampionShipStates.NotAvailable;
                    }

                };
            }
        }
    }
}