using System;
using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;

namespace Rhythm_Recall.Waves
{
    public static class NewYear2022
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new SongSet("new year");

                fightSet.Push(typeof(NightofKnights));
                fightSet.Push(typeof(DreamBattle));
                fightSet.Push(typeof(FreedomDive));
                //    if (System.IO.File.Exists("Datas\\Global\\Unlock1.Tmpf"))
                //       fightSet.Push(typeof(BrainPower));
                // fightSet.Push(typeof(Igallta)); 

                int year = 2022;
                int month = 1;
                int day = 1;

                int startHour = 13, startMinute = 0, endHour = 17, endMinute = 30;

                return new ChampionShip(fightSet)
                {
                    Title = "New Year 2022",
                    SubTitle = "2022 Jan. 1st, 1:00-5:30",
                    EditorName = "T-mas",
                    Introduce = "A new start!",
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
                        bool res = (t.Hours > startHour || t.Hours == startHour && t.Minutes >= startMinute) &&
                                   (t.Hours < endHour || (t.Hours == endHour && t.Minutes < endMinute));
                        return res ? ChampionShip.ChampionShipStates.Starting : ChampionShip.ChampionShipStates.NotAvailable;
                    }

                };
            }
        }
    }
}