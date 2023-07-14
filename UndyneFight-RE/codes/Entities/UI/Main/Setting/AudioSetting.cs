using Microsoft.Xna.Framework;
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
            private class AudioSetting : SettingChunk
            {
                public AudioSetting() : base("Audio", 135)
                {
                    this.AddChild(_masterVolume = new ScrollBar(new CollideRect(510 - 80, 80, 80 * 2, 60), "Master Volume", 0, 100, this)
                    { DefaultValue = DataLibrary.masterVolume });
                    this.AddChild(_spearVolume = new ScrollBar(new CollideRect(510 - 80, 180, 80 * 2, 60), "Spear Volume", 0, 100, this)
                    { DefaultValue = DataLibrary.SpearBlockingVolume });
                    _masterVolume.OnChange += () => { 
                        _masterVolume.Volume = _masterVolume.GetValue() / 100f;
                        _spearVolume.Volume = _masterVolume.GetValue() / 100f * _spearVolume.GetValue() / 100f;
                    };
                    _spearVolume.OnChange += () => {
                        _spearVolume.Volume = _masterVolume.GetValue() / 100f * _spearVolume.GetValue() / 100f;
                    };
                    _spearVolume.ChangeSound = FightResources.Sounds.ArrowStuck;

                    this.OnActivated += () => {
                        _masterVolume.SetValue(DataLibrary.masterVolume);
                        _spearVolume.SetValue(DataLibrary.SpearBlockingVolume);
                    };
                }
                ScrollBar _masterVolume, _spearVolume;

                public override void Apply()
                {
                    DataLibrary.masterVolume = (int)MathF.Round(_masterVolume.GetValue(), 0);
                    DataLibrary.SpearBlockingVolume = (int)MathF.Round(_spearVolume.GetValue(), 0);

                    MediaPlayer.Volume = SoundEffect.MasterVolume = MathF.Pow(DataLibrary.masterVolume / 100f, 2);
                }
                public override void Update()
                { 
                    base.Update();
                }
            }
        }
    }
}