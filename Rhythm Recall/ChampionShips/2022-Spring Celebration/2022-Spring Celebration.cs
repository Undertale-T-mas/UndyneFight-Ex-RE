using System;
using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;

namespace Rhythm_Recall.Waves
{
    public static class SpringCelebration2022
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("Spring Event II");
                fightSet.Push(typeof(Gooddrill));
                fightSet.Push(typeof(Goodtek));
                fightSet.Push(typeof(Goodrage));
                //     fightSet.Push(typeof(Igallta)); 

                int year = 2022;
                int month = 4;
                int day = 5;

                int startHour = 13, startMinute = 30, endHour = 18, endMinute = 00;

                return new ChampionShip(fightSet)
                {
                    Title = "Spring Celebration 2022",
                    SubTitle = "Apr.5 13:30-18:00",
                    EditorName = "T-mas",
                    Introduce = "A GOOD YEAR!",
                    IconPath = "ChampionShips\\TCS",

                    CheckTime = () =>
                    {
                        if (BSet.problem) { return ChampionShip.ChampionShipStates.NotStart; }
                        else
                        {
                            DateTime s = DateTime.UtcNow;
                            int dayType;
                            s = s.AddHours(8);
                            dayType = s.Year < year || (s.Year == year && s.Month < month)
                                ? -1
                                : s.Month == month && s.Year == year && s.Day < day ? -1 : s.Month == month && s.Year == year && s.Day == day ? 0 : 1;
                            if (dayType == -1) return ChampionShip.ChampionShipStates.NotStart;
                            if (dayType == 1) return ChampionShip.ChampionShipStates.End;
                            TimeSpan t = s.TimeOfDay;
                            bool res = (t.Hours > startHour || t.Hours == startHour && t.Minutes >= startMinute) &&
                                       (t.Hours < endHour || (t.Hours == endHour && t.Minutes < endMinute));
                            return res ? ChampionShip.ChampionShipStates.Starting : ChampionShip.ChampionShipStates.NotAvailable;
                        }
                    }

                };
            }
        }
    }
}