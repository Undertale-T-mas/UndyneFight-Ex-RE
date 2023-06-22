using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex
{
    internal partial class GameMain : Game
    {
        internal static List<Type> fights = new List<Type>();
        public void LoadObject()
        {
            InstanceCode();

            if (!string.IsNullOrEmpty(GameInterface.UFEXSettings.GamejoltPrivateKey))
            {
                GameStates.GameJolt = new(GameInterface.UFEXSettings.GamejoltID, GameInterface.UFEXSettings.GamejoltPrivateKey);
                GameStates.GameJolt.Timeout = new TimeSpan(0, 0, 20);
            }
            Scene.PrepareLoader(Content.ServiceProvider);

            UserService.StoreData.StoreItemLoad();
            FightSystem.Initialize(fights);
            Achievements.AchievementManager.Initialize();
            Settings.SettingsManager.Initialize();
            Surface.Initialize();
            Challenge.Initialize();

            PlayerManager.Initialize();

            Fight.Functions.OneElementArrows.Add('R');
            Fight.Functions.OneElementArrows.Add('D');
            Fight.Functions.OneElementArrows.Add('d');
        }
    }
}