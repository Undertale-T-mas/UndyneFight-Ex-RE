using System;
using static UndyneFight_Ex.Settings.SettingsManager;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private partial class VirtualFather
        {
            private class VideoSetting : SettingChunk
            {
                public VideoSetting() : base("Video", 205)
                {
                    this.AddChild(_drawingQuality = new AlternateButton(this, new(510, 130), "Draw quality", "Low", "Normal", "High")
                    {
                        DefaultValue = DataLibrary.drawingQuality.ToString()
                    });
                    this.AddChild(_reduceBlue = new ScrollBar(new CollideRect(510 - 80, 180, 80 * 2, 60), "Blue reduce", 0, 100, this)
                    {
                        DefaultValue = DataLibrary.reduceBlueAmount
                    });
                    //this.AddChild();
                    this.OnActivated += () =>
                    {
                        _drawingQuality.DefaultValue = DataLibrary.drawingQuality.ToString();
                        _reduceBlue.SetValue(DataLibrary.reduceBlueAmount);
                    };

                }
                AlternateButton _drawingQuality;
                ScrollBar _reduceBlue;
                public override void Apply()
                {
                    DataLibrary.drawingQuality = _drawingQuality.Result switch
                    {
                        "Low" => DataLibrary.DrawingQuality.Low,
                        "Normal" => DataLibrary.DrawingQuality.Normal,
                        "High" => DataLibrary.DrawingQuality.High,
                        _ => throw new Exception()
                    };
                    DataLibrary.reduceBlueAmount = (int)MathF.Round(_reduceBlue.GetValue(), 0);
                    GameStates.ResetRendering();
                }
            }
        }
    }
}