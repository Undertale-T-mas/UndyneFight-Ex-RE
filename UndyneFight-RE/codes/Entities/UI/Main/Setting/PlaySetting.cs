using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using UndyneFight_Ex.Settings;
using static UndyneFight_Ex.Settings.SettingsManager;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private partial class VirtualFather
        {
            private class PlaySetting : SettingChunk
            {
                public PlaySetting() : base("Gameplay", 275)
                {
                    SecondaryScale = 1.07f;
                    this.AddChild(_arrowScale = new(new CollideRect(510 - 80, 80, 80 * 2, 60), "Arrow Size", 1.0f, 2.0f, this) { 
                        DefaultValue = Settings.SettingsManager.DataLibrary.ArrowScale,
                        Digit = 2
                    });
                    this.AddChild(_arrowSpeed = new(new CollideRect(510 - 80, 180, 80 * 2, 60), "Arrow Speed", 1.0f, 2.0f, this)
                    {
                        DefaultValue = Settings.SettingsManager.DataLibrary.ArrowSpeed,
                        Digit = 2
                    });
                    this.AddChild(_mirror = new(this, new(520, 280), "Mirror")
                    {
                        DefaultScale = 1.3f,
                        DefaultValue = Settings.SettingsManager.DataLibrary.Mirror
                    });
                    this.AddChild(_perciseWarn = new(this, new(516, 350), "Percise\n Warns") { 
                        DefaultScale = 1.2f,
                        DefaultValue = Settings.SettingsManager.DataLibrary.perciseWarning
                    });
                    this.OnActivated += () => {
                        _arrowScale.SetValue(DataLibrary.ArrowScale);
                        _arrowSpeed.SetValue(DataLibrary.ArrowSpeed);
                        _mirror.DefaultValue = DataLibrary.Mirror;
                        _perciseWarn.DefaultValue = DataLibrary.perciseWarning;
                    };

                }
                ScrollBar _arrowScale, _arrowSpeed;
                TickBox _mirror, _perciseWarn;

                public override void Apply()
                {
                    DataLibrary.ArrowScale = _arrowScale.GetValue();
                    DataLibrary.ArrowSpeed = _arrowSpeed.GetValue();
                    DataLibrary.Mirror = _mirror.Ticked;
                    DataLibrary.perciseWarning = _perciseWarn.Ticked;
                }
            }
        }
    }
}