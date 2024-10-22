﻿using System;
using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;

namespace Rhythm_Recall.Waves
{
    internal static class Summer_Camp_Ⅲ_A
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("Summer Camp - A");

                fightSet.Push(typeof(Death_By_Glamour));
                fightSet.Push(typeof(Asgore));
                fightSet.Push(typeof(The_World_Revolving));

                DateTime s, e;
                s = new(2023, 10, 1, 9 - 8, 0, 0, 0);
                e = new(2023, 10, 1, 12 - 8, 0, 0);
                return new ChampionShip(fightSet)
                {
                    DivisionExist = new() { "div.1", "div.2", "Div.1", "Div.2" },
                    Title = "Summer Camp - A",
                    SubTitle = "2023 Oct.1, A:9:00~12:00",
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
    internal static class Summer_Camp_Ⅲ_B
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("Summer Camp - B");

                fightSet.Push(typeof(BIG_SHOT));
                fightSet.Push(typeof(Spider_Dance));
                fightSet.Push(typeof(Traveler_at_Sunset));

                DateTime s, e;
                s = new(2023, 10, 1, 13 - 8, 0, 0, 0);
                e = new(2023, 10, 1, 17 - 8, 0, 0);
                return new ChampionShip(fightSet)
                {
                    DivisionExist = new() { "div.1", "div.2", "Div.1", "Div.2" },
                    Title = "Summer Camp - B",
                    SubTitle = "2023 Oct.1, B:13:30~17:00",
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
