using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.ChampionShips;

namespace UndyneFight_Ex.GameInterface
{
    public static class GameStartUp
    {
        public static Action MainSceneIntro { internal get; set; } = () => { GameStates.InstanceCreate(new Entities.IntroUI()); };

        private static List<Type> MainGameFights;

        public static void SetMainSongs(List<Type> fights)
        {

            MainGameFights = fights;
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

        public static Type SetUpShower
        {
            set => Entities.StartingShower.TitleSetUp = value;
        }
        public static Type TitleShower
        {
            set => Entities.StartingShower.TitleShower = value;
        }
        public static class LoadingSettings
        {
            public static Vector2 TitleCentrePosition { set; internal get; } = new(320, 220);
            public static string TitleTextureRoot { set; internal get; }
        }

        public static Action<ContentManager> Initialize { get; set; }
    }
}
