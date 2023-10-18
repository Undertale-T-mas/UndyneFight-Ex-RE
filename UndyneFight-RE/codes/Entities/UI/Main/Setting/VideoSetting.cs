using System;
using static UndyneFight_Ex.Settings.SettingsManager.DataLibrary;

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
                        DefaultValue = drawingQuality.ToString()
                    });
                    this.AddChild(_reduceBlue = new ScrollBar(new CollideRect(510 - 80, 180, 80 * 2, 60), "Blue reduce", 0, 100, this)
                    {
                        DefaultValue = reduceBlueAmount
                    });
                    this.AddChild(_drawFPS = new ScrollBar(new CollideRect(510 - 80, 280, 80 * 2, 60), "FPS Limit", 25, 125, this)
                    {
                        DefaultValue = DrawFPS, Digit = 0
                    });
                    this.AddChild(_samplerState = new AlternateButton(this, new(510, 410), "Sampler state", "Nearest", "3x Linear", "Anisotropic")
                    {
                        DefaultValue = SamplerState
                    });
                    //this.AddChild();
                    this.OnActivated += () =>
                    {
                        _drawingQuality.DefaultValue = drawingQuality.ToString();
                        _reduceBlue.SetValue(reduceBlueAmount);
                        _drawFPS.SetValue(MathF.Round(DrawFPS));
                        _samplerState.DefaultValue = SamplerState;
                    };

                }
                AlternateButton _drawingQuality, _samplerState;
                ScrollBar _reduceBlue, _drawFPS;
                public override void Apply()
                {
                    drawingQuality = _drawingQuality.Result switch
                    {
                        "Low" => DrawingQuality.Low,
                        "Normal" => DrawingQuality.Normal,
                        "High" => DrawingQuality.High,
                        _ => throw new Exception()
                    };
                    reduceBlueAmount = (int)MathF.Round(_reduceBlue.GetValue(), 0);
                    DrawFPS = _drawFPS.GetValue();
                    SamplerState = _samplerState.Result; 

                    GameStates.ResetRendering();
                }
            }
        }
    }
}