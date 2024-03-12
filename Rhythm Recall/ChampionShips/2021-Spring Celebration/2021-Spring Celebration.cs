﻿using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;

namespace Rhythm_Recall.Waves
{
    public static class SpringCelebration2021
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("Spring Event I");
                fightSet.Push(typeof(Astronomia));
                fightSet.Push(typeof(HellTaker));
                //2024 April
                fightSet.Push(typeof(Goyang));
                //fightSet.Push(typeof(IndihomePaketPhoenix));
                fightSet.Push(typeof(NeverGonnaGiveYouUp));
                return new ChampionShip(fightSet)
                {
                    Title = "Spring Celebration 2021",
                    SubTitle = "Feb.9 9:30-12:00 + 14:00-17:00",
                    EditorName = "T-mas",
                    Introduce = "The first championship in Rhythm Recall!",
                    //IconPath = "ChampionShips\\TIcon",
                    //IconPath = "ChampionShips\\TCS",
                    IconPath = "ChampionShips\\cup_highres",
                    CheckTime = () => { return ChampionShip.ChampionShipStates.End; }
                };
            }
        }
    }
}