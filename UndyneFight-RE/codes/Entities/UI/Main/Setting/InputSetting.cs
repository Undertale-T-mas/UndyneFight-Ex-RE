
using static UndyneFight_Ex.Settings.SettingsManager;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private partial class VirtualFather
        {
            private class InputSetting : SettingChunk
            {
                public InputSetting() : base("Input", 345)
                {
                    this.AddChild(_delay = new(new CollideRect(510 - 80, 80, 80 * 2, 60), "Arrow Delay", -50, 150, this)
                    {
                        KeyScale = 0.04f,
                        Digit = 0,
                        DefaultValue = DataLibrary.ArrowDelay
                    });
                    OnActivated += () => { _delay.SetValue(DataLibrary.ArrowDelay); };
                }
                ScrollBar _delay;

                public override void Apply()
                {
                    DataLibrary.ArrowDelay = _delay.GetValue();
                }
            }
        }
    }
}