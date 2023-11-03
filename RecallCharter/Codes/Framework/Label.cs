using Microsoft.Xna.Framework;
using static UndyneFight_Ex.Remake.Resources.Font;

namespace RecallCharter
{
    internal class Label : Control
    {
        public string Text { get; set; }
        public Color FontColor { get; set; }

        public override void Draw()
        {
            if (!Father.IsEnabled) return;
            Normal.CentreDraw(Text, this.Centre, FontColor, Scale, Depth);
            base.Draw();
        }
        public override void Update()
        {
            this.collidingBox.Size = Normal.SFX.MeasureString(Text);
            base.Update();
        }
    }
}
