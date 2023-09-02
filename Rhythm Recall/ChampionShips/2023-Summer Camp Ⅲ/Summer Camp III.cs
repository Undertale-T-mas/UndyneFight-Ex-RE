using System;
using System.Runtime.CompilerServices;
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

                DateTime s = new(2023, 10, 1, 9, 0, 0, 0), e = new(2023, 10, 1, 12, 0, 0);
                return new ChampionShip(fightSet)
                {
                    Title = "Summer Camp Ⅲ",
                    SubTitle = "2023 Aug.20, A:9:30~12:00 B:14:00~17:30",
                    EditorName = "T-mas",
                    Introduce = "The happiest summer we have",
                    IconPath = "ChampionShips\\TCS",
                    Start = s,
                    End = e,

                    CheckTime = () =>
                    {
                        DateTime cur = DateTime.UtcNow;
                        if (cur < s) return ChampionShip.ChampionShipStates.NotStart;
                        else if (cur < e) return ChampionShip.ChampionShipStates.Starting;
                        else if (cur.Day == e.Day && cur.Month == e.Month && cur.Year == e.Year)
                            return ChampionShip.ChampionShipStates.NotAvailable;
                        else return ChampionShip.ChampionShipStates.End;
                    }

                };
            }
        }
    }
}
