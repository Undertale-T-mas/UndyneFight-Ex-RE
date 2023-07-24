using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using static UndyneFight_Ex.Settings.SettingsManager;

namespace UndyneFight_Ex.Settings
{
    public static class SettingsResetInterface
    {
        internal static void SaveSettings(IEnumerable<Setting> e)
        {
            foreach (var sel in e)
            {
                sel.Save();
            }
            PlayerManager.Save();
            ApplySettings();
        }
        internal static void ApplySettings()
        {
            MediaPlayer.Volume = SoundEffect.MasterVolume = MathF.Pow(DataLibrary.masterVolume / 100f, 2);

            GameMain.ResetRendering();
        }
        internal static void EnterSettings()
        {
            SoundEffect.MasterVolume = 1.0f;
        }
    }
}