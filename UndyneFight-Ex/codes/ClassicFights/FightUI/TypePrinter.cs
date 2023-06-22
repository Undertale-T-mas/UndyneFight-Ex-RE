using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace UndyneFight_Ex.Fight
{
    public abstract class TextAttribute
    {
        internal abstract void Reset(PrintingSettings textPrinter);
        internal virtual void ResetEnd(PrintingSettings textPrinter) { }
    }
    public class TextAction : TextAttribute
    {
        public TextAction(Action act)
        {
            action = act;
        }
        Action action;
        internal override void Reset(PrintingSettings textPrinter)
        {
            action.Invoke();
        }
    }
    public class TextMoveAttribute : TextAttribute
    {
        public TextMoveAttribute(Func<float, Vector2> act)
        {
            action = act;
        }
        Func<float, Vector2> action;
        internal override void Reset(PrintingSettings textPrinter)
        {
            textPrinter.charPositionDetla = action.Invoke(textPrinter.CurrentData.restTime);
        }
    }
    public class TextSizeAttribute : TextAttribute
    {
        public TextSizeAttribute(float scale)
        {
            size = scale;
        }
        float size;
        internal override void Reset(PrintingSettings textPrinter)
        {
            textPrinter.TextSize = size;
        }
    }
    public class TextSpeedAttribute : TextAttribute
    {
        private readonly float speed;
        public TextSpeedAttribute(float speed)
        {
            this.speed = speed;
        }
        internal override void Reset(PrintingSettings textPrinter)
        {
            textPrinter.PrintSpeed = speed;
        }
    }
    public class TextTimeThreshold : TextAttribute
    {
        private readonly float time;
        public TextTimeThreshold(float time)
        {
            this.time = time;
        }
        internal override void Reset(PrintingSettings textPrinter)
        {
            if (textPrinter.CurrentData.totalTime < time) textPrinter.PrintSpeed = 0;
            else textPrinter.CurrentData.restTime = textPrinter.CurrentData.totalTime - time;
        }
    }
    public class TextCharMovingAttribute : TextAttribute
    {
        private readonly Func<float, Vector2> movingFunc;
        public TextCharMovingAttribute(Func<float, Vector2> movingFunc)
        {
            this.movingFunc = movingFunc;
        }

        private Vector2 del;
        internal override void Reset(PrintingSettings textPrinter)
        {
            del = movingFunc.Invoke(textPrinter.CurrentData.restTime);
            textPrinter.charPositionDetla += del;
        }
        internal override void ResetEnd(PrintingSettings textPrinter)
        {
            textPrinter.charPositionDetla -= del;
        }
    }
    public class TextGleamAttribute : TextAttribute
    {
        private readonly bool type;
        public TextGleamAttribute(bool type)
        {
            this.type = type;
        }
        internal override void Reset(PrintingSettings textPrinter)
        {
            textPrinter.LightEnabled = type;
        }
    }
    public class TextColorAttribute : TextAttribute
    {
        private Color color;
        public TextColorAttribute(Color color)
        {
            this.color = color;
        }
        internal override void Reset(PrintingSettings textPrinter)
        {
            textPrinter.textColor = color;
        }
    }
    public class TextFadeoutAttribute : TextAttribute
    {
        float delay, duration;
        public TextFadeoutAttribute(float delay, float duration)
        {
            this.delay = delay; this.duration = duration;
        }
        internal override void Reset(PrintingSettings textPrinter)
        {
            float t = textPrinter.CurrentData.restTime;
            if (t <= delay) return;
            float val = MathF.Max(0, (delay + duration - t) / duration);
            if (val == 0) textPrinter.ShouldDispose = true;
            textPrinter.TextColorAlpha = val;
        }
    }
    /// <summary>
    /// 一种TextAttribute。包含若干子TextAttribute。它会同时执行所有子TextAttribute，避免你在文字使用中写大量$。
    /// </summary>
    public class AttributeSet : TextAttribute
    {
        private readonly TextAttribute[] textAttributes;
        public AttributeSet(params TextAttribute[] textAttributes)
        {
            this.textAttributes = textAttributes;
        }
        internal override void Reset(PrintingSettings textPrinter)
        {
            foreach (var v in textAttributes)
            {
                v.Reset(textPrinter);
            }
        }
    }

    public class PrintingSettings
    {
        public bool LightEnabled { get; set; } = false;
        public float PrintSpeed { get; set; } = 20;
        public float TextSize { get; set; } = 1.0f;
        public bool ShouldDispose { get; set; } = false;
        public float TextColorAlpha { get; set; } = 1.0f;
        public Color textColor = Color.White;
        public Vector2 charPositionDetla = Vector2.Zero;
        public GLFont renderFont = FightResources.Font.NormalFont;

        public SoundEffect printSound = FightResources.Sounds.printWord;

        public class CurrentPrintingData
        {
            public float restTime;
            public float totalTime;
        }
        public CurrentPrintingData CurrentData { get; set; } = new();
    }
    public class TextPrinter : Entity
    {
        internal void InstantEnd()
        {
            if (appearTime < ForceTime) return;
            appearTime = 0x3f3f3f3f;
            maxPosition = text.Length - 1;
            Dispose();
        }

        public bool AllShowed => maxPosition >= text.Length - 1 && (ForceTime == -1 || appearTime >= ForceTime);

        private readonly string text;
        private float appearTime;
        public bool sound = true;
        private int maxPosition = 0;

        public float LinesDistance { private get; set; } = 40;

        public float ForceTime { private set; get; } = -1;

        public Vector2 Position { get; set; }
        private readonly TextAttribute[] textAttributes;

        public TextPrinter(float forceTime, string text, Vector2 position, params TextAttribute[] textAttributes)
        {
            UpdateIn120 = true;
            Depth = 0.5f;
            ForceTime = forceTime;
            this.text = text;
            Position = position;
            this.textAttributes = textAttributes;
        }
        public TextPrinter(string text, Vector2 position, params TextAttribute[] textAttributes)
        {
            UpdateIn120 = true;
            Depth = 0.5f;
            this.text = text;
            Position = position;
            this.textAttributes = textAttributes;
        }
        public TextPrinter(string text, params TextAttribute[] textAttributes)
        {
            UpdateIn120 = true;
            Depth = 0.5f;
            this.text = text;
            this.textAttributes = textAttributes;
        }
        public TextPrinter(float forceTime, string text, params TextAttribute[] textAttributes)
        {
            UpdateIn120 = true;
            Depth = 0.5f;
            ForceTime = forceTime;
            this.text = text;
            this.textAttributes = textAttributes;
        }

        public override void Draw()
        {
            PrintingSettings printingSettings = new PrintingSettings();
            printingSettings.CurrentData.totalTime = appearTime;
            float rest = appearTime;
            int attributeCount = 0;
            bool soundPlayed = false;
            Vector2 currentPosition = Position;
            float maxSize = 0.0f;
            for (int i = 0; i < text.Length; i++)
            {
                if (rest <= 0) break;
                printingSettings.CurrentData.restTime = rest;
                if (text[i] == '$')
                {
                    textAttributes[attributeCount].Reset(printingSettings);
                    rest = printingSettings.CurrentData.restTime;
                    if (printingSettings.PrintSpeed == 0) break;
                    attributeCount++;
                    if (printingSettings.ShouldDispose)
                    {
                        Dispose();
                        return;
                    }
                    continue;
                }
                if (text[i] == '#')
                {
                    string s = "";
                    i++;
                    while (text[i] != '#')
                    {
                        s += text[i];
                        i++;
                    }
                    switch (s)
                    {
                        case "sans":
                            printingSettings.TextSize = 0.8f;
                            printingSettings.renderFont = FightResources.Font.SansFont;
                            printingSettings.textColor = Color.White;
                            printingSettings.printSound = FightResources.Sounds.sansWord;
                            break;
                        case "Japanese":
                            printingSettings.TextSize = 0.8f;
                            printingSettings.renderFont = FightResources.Font.Japanese;
                            printingSettings.textColor = Color.White;
                            break;

                        case "enemy":
                            printingSettings.TextSize = 0.65f;
                            printingSettings.textColor = Color.Black;
                            break;
                    }
                    continue;
                }
                if (text[i] == '\r' || text[i] == '\n')
                {
                    currentPosition = new Vector2(Position.X, currentPosition.Y + LinesDistance * maxSize);
                    maxSize = 0.0f;
                    continue;
                }
                if (i > maxPosition)
                {
                    if (text[i] != ' ' && (!soundPlayed))
                    {
                        soundPlayed = true;
                        if (sound)
                            Functions.PlaySound(printingSettings.printSound);
                    }
                    maxPosition = i;
                }
                maxSize = MathF.Max(maxSize, printingSettings.TextSize);

                Color col = printingSettings.textColor * printingSettings.TextColorAlpha;
                printingSettings.renderFont.Draw(text[i] + "", currentPosition + printingSettings.charPositionDetla, col, printingSettings.TextSize, Depth);
                if (printingSettings.LightEnabled)
                {
                    Vector2 textdel = printingSettings.renderFont.SFX.MeasureString(text[i].ToString());
                    textdel = MathUtil.Rotate(textdel, Rotation);

                    for (int x = 0; x <= 4; x++)
                        printingSettings.renderFont.Draw(text[i] + "", currentPosition + printingSettings.charPositionDetla - textdel * (x + 1) * 0.07f, col * (0.5f - x * 0.1f), printingSettings.TextSize * (1.14f + x * 0.14f), Depth);
                }

                Vector2 size = printingSettings.renderFont.SFX.MeasureString(text[i] + "");
                currentPosition += MathUtil.Rotate(new Vector2(size.X * printingSettings.TextSize, 0), Rotation);

                rest -= 60 / printingSettings.PrintSpeed;
                if (text[i] == '$')
                    textAttributes[attributeCount].ResetEnd(printingSettings);
            }
        }

        public override void Update()
        {
            appearTime += 0.5f;
            if (ForceTime != -1 && appearTime >= ForceTime) InstantEnd();
        }
    }

    public class DialogBox : Entity
    {
        public Action AfterDispose { private get; set; }

        private readonly TextPrinter[] textPrinters;
        private Vector2 location;

        public DialogBox(Vector2 position, TextPrinter[] textPrinters)
        {
            Image = FightResources.FightSprites.dialogBox;
            this.textPrinters = textPrinters;
            foreach (var v in this.textPrinters)
            {
                v.LinesDistance = 38;
                v.Position = position + new Vector2(36, 10);
            }
            location = position;
        }
        public DialogBox(Vector2 location, string text, params TextAttribute[] textAttributes) : this(location, new TextPrinter[] { new TextPrinter(text, textAttributes) }) { }

        private int currentProgress = 0;

        public override void Draw()
        {
            if (currentProgress >= textPrinters.Length) return;
            textPrinters[currentProgress].Draw();
            FormalDraw(Image, location, Color.White, 0, Vector2.Zero);
        }

        public override void Update()
        {
            while (!Settings.SettingsManager.DataLibrary.dialogAvailable && textPrinters[currentProgress].ForceTime != -1)
            {
                textPrinters[currentProgress].InstantEnd();
                currentProgress++;
                return;
            }
            if (currentProgress < textPrinters.Length)
                textPrinters[currentProgress].Update();
            if (GameStates.IsKeyPressed(InputIdentity.Cancel)) textPrinters[currentProgress].InstantEnd();
            if (textPrinters[currentProgress].AllShowed && (GameStates.IsKeyPressed(InputIdentity.Confirm)))
                currentProgress++;
            else if (textPrinters[currentProgress].AllShowed && textPrinters[currentProgress].ForceTime != -1) currentProgress++;
            if (currentProgress >= textPrinters.Length) Dispose();
        }

        public override void Dispose()
        {
            AfterDispose?.Invoke();
            base.Dispose();
        }

    }
    public class BoxMessage : Entity
    {
        public Action AfterDispose { private get; set; }

        private readonly TextPrinter[] textPrinters;
        public BoxMessage(TextPrinter[] textPrinters)
        {
            this.textPrinters = textPrinters;
            foreach (var v in this.textPrinters)
                if (v.Position == Vector2.Zero) v.Position = new Vector2(40, 256);
        }
        public BoxMessage(string text, params TextAttribute[] textAttributes) : this(new TextPrinter[] { new TextPrinter(text, new Vector2(40, 256), textAttributes) }) { }

        private int currentProgress = 0;

        public override void Draw()
        {
            if (currentProgress < textPrinters.Length)
                textPrinters[currentProgress].Draw();
        }

        public override void Update()
        {
            textPrinters[currentProgress].Update();
            if (GameStates.IsKeyPressed(InputIdentity.Cancel)) textPrinters[currentProgress].InstantEnd();
            if (textPrinters[currentProgress].AllShowed && (GameStates.IsKeyPressed(InputIdentity.Confirm)))
                currentProgress++;
            if (currentProgress == textPrinters.Length) Dispose();
        }

        public override void Dispose()
        {
            AfterDispose?.Invoke();
            base.Dispose();
        }
    }
}