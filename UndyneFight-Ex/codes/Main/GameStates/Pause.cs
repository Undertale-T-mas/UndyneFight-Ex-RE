using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex
{
    public static partial class GameStates
    {
        public static bool Paused { get; private set; } = false;
        public static void RunGamePause()
        {
            Paused = true;
            CurrentScene.AlternatePause();
        }        
        public static void RunGameResume()
        {
            Paused = false;
            CurrentScene.AlternatePause();
        }
    }
}