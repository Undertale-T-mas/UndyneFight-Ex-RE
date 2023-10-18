using System;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Remake.Network;
using UndyneFight_Ex.Remake.UI;
using static UndyneFight_Ex.GameStates;

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

            ResetFightState(true);

            CurrentWindow.ClientSizeChanged += WindowSizeChanged;

            Data.GlobalDataRoot.UserMemory m = FileData.GlobalData.Memory;
            if (m.AutoAuthentic)
                UserUI.AutoAuthentic(m.RememberUser, m.PasswordMem);
        }
        public static void MainInitialize()
        { 
            FileData.Initialize();  
            
            GameStartUp.MainSceneIntro = () => {
                InstanceCreate(new UI.DEBUG.IntroUI()); LateInitialize(); 
            };
            // GameStartUp.MainSceneIntro = () => { GameStates.InstanceCreate(new UI.UserUI()); LateInitialize(); };
            // GameStartUp.MainSceneIntro = () => { GameStates.InstanceCreate(new UI.SettingUI()); LateInitialize(); };
            GameStartUp.Initialize += (loader) => { LateInitialize(); };

            UFEXSettings.OnSongComplete += SongUpload.UploadSong;
            UFEXSettings.Update += MouseSystem.Update;
            GameStartUp.Initialize += Resources.Initialize; 
        }

        private static void WindowSizeChanged(object sender, EventArgs e)
        {
            Vector2 currentSize = CurrentWindow.ClientBounds.Size.ToVector2();
            MouseSystem.ScreenSize = currentSize;
        }
    }
}
