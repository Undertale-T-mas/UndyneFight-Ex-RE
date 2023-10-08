using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake
{
    public abstract class TextRenderer : TextEffect
    {
        public TextRenderer() { }

        protected TextSetting Allocated { get; private set; }

        public abstract void Render(Vector2 position);

        protected override void Update(TextSetting textSetting)
        {
            this.Allocated = textSetting;
        }
    }
}