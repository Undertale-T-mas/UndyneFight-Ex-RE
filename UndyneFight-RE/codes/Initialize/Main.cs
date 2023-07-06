using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake
{
    public static partial class Initialize
    {
        private static bool isLateInitialized = false;
        private static void LateInitialize()
        {
            if (isLateInitialized)
            {
                return;
            }
            isLateInitialized = true;

            GameStates.CurrentWindow.ClientSizeChanged += WindowSizeChanged;
        }
        public static void MainInitialize()
        { 
            FileData.Initialize(); 
          //  GameStartUp.MainSceneIntro = () => { GameStates.InstanceCreate(new UI.SelectUI()); LateInitialize(); };
            GameStartUp.MainSceneIntro = () => { GameStates.InstanceCreate(new UI.UserUI()); LateInitialize(); };
         //   GameStartUp.MainSceneIntro = () => { GameStates.InstanceCreate(new UI.SettingUI()); LateInitialize(); };
            

            UFEXSettings.Update += MouseSystem.Update;
            GameStartUp.Initialize += Resources.Initialize; 
        }

        private static void WindowSizeChanged(object sender, EventArgs e)
        {
            Vector2 currentSize = GameStates.CurrentWindow.ClientBounds.Size.ToVector2();
            MouseSystem.ScreenSize = currentSize;
        }
    }
}
