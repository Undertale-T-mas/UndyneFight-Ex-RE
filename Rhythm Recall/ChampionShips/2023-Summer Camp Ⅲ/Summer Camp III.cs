using System;
using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;

namespace Rhythm_Recall.Waves
{
    internal static class Summer_Camp_Ⅲ
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("Summer Camp III");

                fightSet.Push(typeof(Death_By_Glamour));
                fightSet.Push(typeof(Asgore));
                fightSet.Push(typeof(The_World_Revolving));
                fightSet.Push(typeof(BIG_SHOT));
                fightSet.Push(typeof(Spider_Dance));
                fightSet.Push(typeof(Traveler_at_Sunset));

                int year = 2023;
                int month = 8;
                int day = 20;

                int startHourA = 9, startMinuteA = 30, endHourA = 12, endMinuteA = 0;
                int startHourB = 9, startMinuteB = 30, endHourB = 12, endMinuteB = 0;
                return new ChampionShip(fightSet)
                {
                    Title = "Summer Camp Ⅲ",
                    SubTitle = "2023 August.20th, \\Part A:9:30~12:00\\Part B:14:00~17:30",
                    EditorName = "T-mas",
                    Introduce = "The happiest summer we have",
                    IconPath = "ChampionShips\\TCS",

                    CheckTime = () =>
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
                        bool res = (t.Hours > startHourA || t.Hours == startHourA && t.Minutes >= startMinuteA) &&
                                   (t.Hours < endHourA || (t.Hours == endHourA && t.Minutes < endMinuteA))&& 
                                   (t.Hours > startHourB || t.Hours == startHourB && t.Minutes >= startMinuteB)&&
                                   (t.Hours < endHourB || (t.Hours == endHourB && t.Minutes < endMinuteB));
                        return res ? ChampionShip.ChampionShipStates.Starting : ChampionShip.ChampionShipStates.NotAvailable;
                    }

                };
            }
        }
    }
}
