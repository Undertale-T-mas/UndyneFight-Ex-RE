using Microsoft.Xna.Framework;
using System;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Settings
{
    internal static class SettingLibrary
    {
        public class MasterVolume : Setting
        {
            public static int Value { get; private set; }
            public MasterVolume(Vector2 centre) : base("Master Volume", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.masterVolume;
                showingValue = Value.ToString();
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    int value_ = Value;
                    if (IsKeyPressed120f(InputIdentity.MainLeft)) Value -= 5;
                    if (IsKeyPressed120f(InputIdentity.MainRight)) Value += 5;
                    Value = Math.Clamp(Value, 0, 100);
                    if (value_ != Value)
                    {
                        PlaySound(Ding, Value / 100f);
                        showingValue = Value.ToString();
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.masterVolume = Value;
            }
        }
        public class SpearBlockedVolume : Setting
        {
            public static int Value { get; private set; }
            public SpearBlockedVolume(Vector2 centre) : base("Spear blocking volume", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.SpearBlockingVolume;
                showingValue = Value.ToString();
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    int value_ = Value;
                    if (IsKeyPressed120f(InputIdentity.MainLeft)) Value -= 5;
                    if (IsKeyPressed120f(InputIdentity.MainRight)) Value += 5;
                    Value = Math.Clamp(Value, 0, 100);
                    if (value_ != Value)
                    {
                        PlaySound(Ding, Value / 100f);
                        showingValue = Value.ToString();
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.SpearBlockingVolume = Value;
            }
        }
        public class ReduceBlue : Setting
        {
            public static int Value { get; private set; }
            public ReduceBlue(Vector2 centre) : base("Blue Reducing", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.reduceBlueAmount;
                showingValue = Value.ToString();
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    int value_ = Value;
                    if (IsKeyPressed120f(InputIdentity.MainLeft)) Value -= 10;
                    if (IsKeyPressed120f(InputIdentity.MainRight)) Value += 10;
                    Value = Math.Clamp(Value, 0, 100);
                    if (value_ != Value)
                    {
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        showingValue = Value.ToString();
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.reduceBlueAmount = Value;
            }
        }
        public class PerciseWarning : Setting
        {
            public static bool Value { get; private set; }
            public PerciseWarning(Vector2 centre) : base("Precise Warn", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.perciseWarning;
                showingValue = Value.ToString();
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    if (IsKeyPressed120f(InputIdentity.MainLeft) || IsKeyPressed120f(InputIdentity.MainRight))
                    {
                        Value = !Value;
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        showingValue = Value.ToString();
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.perciseWarning = Value;
            }
        }
        public class DialogAvailable : Setting
        {
            public static bool Value { get; private set; }
            public DialogAvailable(Vector2 centre) : base("Dialog", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.dialogAvailable;
                showingValue = Value.ToString();
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    if (IsKeyPressed120f(InputIdentity.MainLeft) || IsKeyPressed120f(InputIdentity.MainRight))
                    {
                        Value = !Value;
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        showingValue = Value.ToString();
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.dialogAvailable = Value;
            }
        }
        public class DebugMessage : Setting
        {
            public static bool Value { get; private set; }
            public DebugMessage(Vector2 centre) : base("Debug Message", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.debugMessage;
                showingValue = Value.ToString();
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    if (IsKeyPressed120f(InputIdentity.MainLeft) || IsKeyPressed120f(InputIdentity.MainRight))
                    {
                        Value = !Value;
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        showingValue = Value.ToString();
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.debugMessage = Value;
            }
        }
        public class ArrowSpeed : Setting
        {
            public static float Value { get; private set; }
            public ArrowSpeed(Vector2 centre) : base("Arrow Speed", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.ArrowSpeed;
                showingValue = MathF.Round(Value, 2).ToString() + "x";
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    float value_ = Value;
                    if (IsKeyPressed120f(InputIdentity.MainLeft)) Value -= 0.05f;
                    if (IsKeyPressed120f(InputIdentity.MainRight)) Value += 0.05f;
                    Value = Math.Clamp(Value, 1, 2);
                    if (Value != value_)
                    {
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        showingValue = MathF.Round(Value, 2).ToString() + "x";
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.ArrowSpeed = Value;
            }
        }
        public class ArrowDelay : Setting
        {
            private class DelayHelper : Entity
            {
                public override void Draw()
                {
                    float x = 640 / 6f;
                    float s = 1 - Alpha;
                    Vector2 delta = new(0, 121 * MathF.Pow(s, 3.3f));
                    DrawingLab.DrawLine(new Vector2(x, 427) + delta, new Vector2(x, 480) + delta, 3, Color.Silver * Alpha, 0.96f);
                    DrawingLab.DrawLine(new Vector2(x * 2, 427) + delta, new Vector2(x * 2, 480) + delta, 3, Color.Silver * Alpha, 0.96f);
                    DrawingLab.DrawLine(new Vector2(x * 3, 427) + delta, new Vector2(x * 3, 480) + delta, 3, Color.Silver * Alpha, 0.96f);
                    DrawingLab.DrawLine(new Vector2(x * 4, 427) + delta, new Vector2(x * 4, 480) + delta, 3, Color.Gold * Alpha, 0.96f);
                    DrawingLab.DrawLine(new Vector2(x * 5, 427) + delta, new Vector2(x * 5, 480) + delta, 3, Color.Red * Alpha, 0.96f);
                    DrawingLab.DrawLine(new Vector2(curX, 427) + delta, new Vector2(curX, 480) + delta, 3, Color.Lime * Alpha, 0.971f);
                    DrawingLab.DrawLine(new Vector2(memX, 427) + delta, new Vector2(memX, 480) + delta, 3, Color.Goldenrod * Alpha, 0.971f);
                    Depth = 0.92f;
                    FormalDraw(FightResources.Sprites.pixUnit, new CollideRect(new Vector2(x, 427) + delta, new(x * 4, 53)).ToRectangle(), Color.Black);
                    GlobalResources.Font.FightFont.CentreDraw(this.delta.ToString("F1"), delta + new Vector2(320, 450), Color.Silver * Alpha, 1.0f, 0.0f, 0.99f);

                    if (memX == 0)
                    {
                        float newAlpha = (curX < 4 * x) ? MathF.Pow(MathF.Max(0, (curX - 3.8f * x) / x * 5f), 1.8f)
                            : Alpha * MathF.Max(0, 1 - 1.2f * (curX - 4 * x) / x);
                        GlobalResources.Font.FightFont.CentreDraw("Press Space!", delta + new Vector2(320, 420), Color.Lime * newAlpha, 1.0f, 0.0f, 0.99f);
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
                        if (appearTime == 3 * beat)
                            PlaySound(damaged, MasterVolume.Value / 100f * Alpha);
                        else if (appearTime % beat == 0)
                            PlaySound(Ding, MasterVolume.Value / 100f * Alpha);
                        bool keyPressed = IsKeyPressed120f(InputIdentity.Alternate);
                        float x = 640 / 6f;
                        curX = x + appearTime * 1f / beat * x;
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

            public static float Value { get; private set; }
            public ArrowDelay(Vector2 centre) : base("Arrow Delay", centre)
            {
                helper = new();
                Value = SettingsManager.DataLibrary.ArrowDelay;
                showingValue = MathF.Round(Value, 1).ToString() + "ms";
                AddChild(helper);
                UpdateIn120 = true;
            }

            public override void SelectionEvent() { }

            DelayHelper helper;

            public override void Update()
            {
                if (IsSelected)
                {
                    helper.Alpha += 0.05f;
                    helper.Alpha = MathF.Min(1, helper.Alpha);
                    float value_ = Value;
                    if (IsKeyPressed120f(InputIdentity.MainLeft)) value_ -= 5;
                    if (IsKeyPressed120f(InputIdentity.MainRight)) value_ += 5;
                    if (IsKeyPressed120f(InputIdentity.Confirm)) value_ = helper.Delta;
                    value_ = MathUtil.Clamp(0, value_, 120);
                    if (value_ != Value)
                    {
                        Value = value_;
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        showingValue = MathF.Round(Value, 1).ToString() + "ms";
                    }
                }
                else
                {
                    helper.Alpha = MathF.Max(0, helper.Alpha - 0.05f);
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.ArrowDelay = Value;
            }
        }
        public class ArrowScale : Setting
        {
            public static float Value { get; private set; }
            public ArrowScale(Vector2 centre) : base("Arrow Scale", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.ArrowScale;
                showingValue = MathF.Round(Value, 2).ToString() + "x";
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    float value_ = Value;
                    if (IsKeyPressed120f(InputIdentity.MainLeft)) value_ -= 0.05f;
                    if (IsKeyPressed120f(InputIdentity.MainRight)) value_ += 0.05f;
                    value_ = Math.Clamp(value_, 1, 1.25f);
                    if (value_ != Value)
                    {
                        Value = value_;
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        showingValue = MathF.Round(Value, 2).ToString() + "x";
                    }
                }
                base.Update();
            }
            public override void Save()
            {
                SettingsManager.DataLibrary.ArrowScale = Value;
            }
        }
        public class IsMirror : Setting
        {
            public static bool Value { get; private set; }
            public IsMirror(Vector2 centre) : base("Mirror", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.Mirror;
                showingValue = Value.ToString();
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    if (IsKeyPressed120f(InputIdentity.MainLeft) || IsKeyPressed120f(InputIdentity.MainRight))
                    {
                        Value = !Value;
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        showingValue = Value.ToString();
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.Mirror = Value;
            }
        }
        public class DrawingQualitySetter : Setting
        {
            public static SettingsManager.DataLibrary.DrawingQuality Value { get; private set; }
            public DrawingQualitySetter(Vector2 centre) : base("DrawingQuality", centre)
            {
                UpdateIn120 = true;
                Value = SettingsManager.DataLibrary.drawingQuality;
                showingValue = Value.ToString();
            }

            public override void SelectionEvent() { }

            public override void Update()
            {
                if (IsSelected)
                {
                    int value_ = (int)Value;
                    if (IsKeyPressed120f(InputIdentity.MainLeft)) value_--;
                    if (IsKeyPressed120f(InputIdentity.MainRight)) value_++;
                    value_ = Math.Clamp(value_, 0, 2);
                    if (value_ != (int)Value)
                    {
                        PlaySound(Ding, MasterVolume.Value / 100f);
                        Value = (SettingsManager.DataLibrary.DrawingQuality)value_;
                        showingValue = Value.ToString();
                    }
                }
                base.Update();
            }

            public override void Save()
            {
                SettingsManager.DataLibrary.drawingQuality = Value;
            }
        }
    }
}