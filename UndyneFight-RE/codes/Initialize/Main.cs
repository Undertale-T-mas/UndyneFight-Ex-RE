using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Remake.Network;

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

            GameStates.ResetFightState(true);

            GameStates.CurrentWindow.ClientSizeChanged += WindowSizeChanged;

            Data.GlobalDataRoot.UserMemory m = FileData.GlobalData.Memory;
            if (m.AutoAuthentic)
                UI.UserUI.AutoAuthentic(m.RememberUser, m.PasswordMem);
        }
        public static void MainInitialize()
        { 
            FileData.Initialize();  
            
            GameStartUp.MainSceneIntro = () => { 
                GameStates.InstanceCreate(new UI.DEBUG.IntroUI()); LateInitialize(); 
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
            Vector2 currentSize = GameStates.CurrentWindow.ClientBounds.Size.ToVector2();
            MouseSystem.ScreenSize = currentSize;
        }
    }
}
