﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.ChampionShips;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.GameInterface
{
    public static class GameStartUp
    {
        public static bool CheckLevelExist { set => FightSystem.CheckLevelExist = value; }

        public static Action MainSceneIntro { get; set; } = () => { GameStates.InstanceCreate(new Entities.IntroUI()); };

        private static List<Type> MainGameFights;

        public static void SetMainSongs(List<Type> fights)
        {
            MainGameFights = fights;
        }
        public static void PushStoreItem(StoreItem item)
        {
            StoreData.AddToItemList(item);
        }
        public static void PushChampionShip(ChampionShip system)
        {
            FightSystem.PushChampionShip(system);
        }
        public static void PushChallenge(Challenge challenge)
        {
            FightSystem.PushChallenge(challenge);
        }
        public static void PushAchievement(Achievements.Achievement achievement)
        {
            Achievements.AchievementManager.PushAchievement(achievement);
        }
        public static void PushExtra(Fight.IExtraOption classicFight)
        {
            FightSystem.PushExtra(classicFight);
        }

        public static void StartGame()
        {
            GameMain.fights = MainGameFights;
            using var game = new GameMain();
            game.Run();
        }

        public static void PushSongset(SongSet songset)
        {
            FightSystem.PushSongSet(songset);
        }

        public static Type SetUpShower
        {
            set => StartingShower.TitleSetUp = value;
        }
        public static Type TitleShower
        {
            set => StartingShower.TitleShower = value;
        }
        public static class LoadingSettings
        {
            public static Vector2 TitleCentrePosition { set; internal get; } = new(320, 220);
            public static string TitleTextureRoot { set; internal get; }
        }

        public static Action<ContentManager> Initialize { get; set; }
    }
}
