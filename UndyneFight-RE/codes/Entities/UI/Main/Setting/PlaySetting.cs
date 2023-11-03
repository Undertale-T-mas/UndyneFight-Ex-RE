using static UndyneFight_Ex.Settings.SettingsManager.DataLibrary;

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
                    this.AddChild(_arrowScale = new(new CollideRect(510 - 80, 80, 80 * 2, 60), "Arrow Size", 1.0f, 1.25f, this) { 
                        DefaultValue = ArrowScale,
                        Digit = 2
                    });
                    this.AddChild(_arrowSpeed = new(new CollideRect(510 - 80, 180, 80 * 2, 60), "Arrow Speed", 1.0f, 1.5f, this)
                    {
                        DefaultValue = ArrowSpeed,
                        Digit = 2
                    });
                    this.AddChild(_mirror = new(this, new(520, 280), "Mirror")
                    {
                        DefaultScale = 1.3f,
                        DefaultValue = Mirror
                    });
                    this.AddChild(_perciseWarn = new(this, new(516, 350), "Precise\n Warns") { 
                        DefaultScale = 1.2f,
                        DefaultValue = perciseWarning
                    });
                    this.AddChild(_pauseCheat = new(this, new(764, 100), "Pausing is\n cheating") { 
                        DefaultScale = 1.2f,
                        DefaultValue = PauseCheating
                    });
                    this.OnActivated += () => {
                        _arrowScale.SetValue(ArrowScale);
                        _arrowSpeed.SetValue(ArrowSpeed);
                        _mirror.DefaultValue = Mirror;
                        _perciseWarn.DefaultValue = perciseWarning;
                        _pauseCheat.DefaultValue = PauseCheating;
                    };

                }
                ScrollBar _arrowScale, _arrowSpeed;
                TickBox _mirror, _perciseWarn, _pauseCheat;

                public override void Apply()
                {
                    ArrowScale = _arrowScale.GetValue();
                    ArrowSpeed = _arrowSpeed.GetValue();
                    Mirror = _mirror.Ticked;
                    perciseWarning = _perciseWarn.Ticked;
                    PauseCheating = _pauseCheat.Ticked;
                }
            }
        }
    }
}