using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using System.Net.NetworkInformation;
using System;
using static UndyneFight_Ex.Settings.SettingsManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.Runtime.InteropServices;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private partial class VirtualFather
        {
            private class InputSetting : SettingChunk
            {
                private class DelayHelper : Entity
                {
                    public override void Draw()
                    {
                        float x = 960 / 6f;
                        float s = 1 - Alpha;
                        Vector2 delta = new(0, 141 * MathF.Pow(s, 3.3f) + 240);
                        DrawingLab.DrawLine(new Vector2(x, 427) + delta, new Vector2(x, 480) + delta, 3, Color.Silver * Alpha, 0.96f);
                        DrawingLab.DrawLine(new Vector2(x * 2, 427) + delta, new Vector2(x * 2, 480) + delta, 3, Color.Silver * Alpha, 0.96f);
                        DrawingLab.DrawLine(new Vector2(x * 3, 427) + delta, new Vector2(x * 3, 480) + delta, 3, Color.Silver * Alpha, 0.96f);
                        DrawingLab.DrawLine(new Vector2(x * 4, 427) + delta, new Vector2(x * 4, 480) + delta, 3, Color.Gold * Alpha, 0.96f);
                        DrawingLab.DrawLine(new Vector2(x * 5, 427) + delta, new Vector2(x * 5, 480) + delta, 3, Color.Red * Alpha, 0.96f);
                        DrawingLab.DrawLine(new Vector2(curX, 427) + delta, new Vector2(curX, 480) + delta, 3, Color.Lime * Alpha, 0.971f);
                        DrawingLab.DrawLine(new Vector2(memX, 427) + delta, new Vector2(memX, 480) + delta, 3, Color.Goldenrod * Alpha, 0.971f);
                        Depth = 0.92f;
                        FormalDraw(FightResources.Sprites.pixUnit, new CollideRect(new Vector2(x, 427) + delta, new(x * 4, 53)).ToRectangle(), Color.Black);
                        FightResources.Font.FightFont.CentreDraw(this.delta.ToString("F1"), delta + new Vector2(480, 450), Color.Silver * Alpha, 1.0f, 0.0f, 0.99f);

                        if (memX == 0)
                        {
                            float newAlpha = ((curX < 4 * x) ? MathF.Pow(MathF.Max(0, (curX - 3.8f * x) / x * 5f), 1.8f)
                                : Alpha * MathF.Max(0, 1 - 1.2f * (curX - 4 * x) / x));
                            FightResources.Font.FightFont.CentreDraw("Press Space!", delta + new Vector2(480, 420), Color.Lime * newAlpha, 1.0f, 0.0f, 0.99f);
                        }
                    }
                    int appearTime = 0;
                    float delta = 0, curX = 0, memX = 0;
                    public float Delta => delta;
                    public override void Update()
                    {
                        int beat = 51;
                        appearTime++;

                        if (appearTime == 4 * beat) appearTime = 0;

                        if (Alpha > 0.2f)
                        {
                            Alpha = MathF.Min(Alpha, 1);
                            if (appearTime == 3 * beat)
                                PlaySound(damaged, Alpha);
                            else if (appearTime % beat == 0)
                                PlaySound(Ding, Alpha);
                            bool keyPressed = GameStates.IsKeyPressed120f(InputIdentity.Alternate);
                            float x = 960 / 6f;
                            curX = x + (appearTime * 1f / beat) * x;
                            if (keyPressed)
                            {
                                memX = curX;
                                delta = (appearTime - beat * 3) * 8f;
                            }
                        }
                    }
                    public DelayHelper()
                    {
                        UpdateIn120 = true;
                    }
                    public float Alpha { get; set; } = 0.0f;
                }

                public InputSetting() : base("Input", 345)
                {
                    this.AddChild(_delay = new(new CollideRect(510 - 80, 80, 80 * 2, 60), "Arrow Delay", -50, 250, this)
                    {
                        KeyScale = 0.04f,
                        Digit = 0,
                        DefaultValue = DataLibrary.ArrowDelay
                    });
                    OnActivated += () => { _delay.SetValue(DataLibrary.ArrowDelay); };
                }
                ScrollBar _delay;
                
                DelayHelper _helper;

                public override void Apply()
                {
                    DataLibrary.ArrowDelay = _delay.GetValue();
                }
                public override void Start()
                {
                    base.Start();
                    _helper = new DelayHelper();
                    this.AddChild(_helper);
                }
                public override void Update()
                {
                    if(this.Focus == _delay && this.Activated)
                    {
                        if (_helper.Alpha < 1.0f)
                            _helper.Alpha += 0.02f;
                    }
                    else
                    {
                        if (_helper.Alpha > 0) _helper.Alpha -= 0.02f;
                    }
                    base.Update();
                }
            }
        }
    }
}