using Microsoft.Xna.Framework;
using UndyneFight_Ex;

namespace RecallCharter
{
    internal class Panel : Control
    { 
        public Panel() { }
        public Color BackgroundColor { protected get; set; }
        public Color BorderColor { protected get; set; }
        public override void Draw()
        {
            if (!Father.IsEnabled) return;
            this.Image = FightResources.Sprites.pixUnit;
            this.FormalDraw(Image, this.CollidingBox.ToRectangle(), this.BackgroundColor);
            DrawingLab.DrawRectangle(this.CollidingBox, this.BorderColor, 2.0f, this.Depth + 0.01f);
            if (!this.IsEnabled) this.IsEnabled = true;
        }
    }
}