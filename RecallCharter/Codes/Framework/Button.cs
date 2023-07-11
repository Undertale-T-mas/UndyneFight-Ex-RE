using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex;
using UndyneFight_Ex.Remake;

namespace RecallCharter
{
    internal class Button : Control
    {
        public bool ClickOffFocus { get; set; } = false;
        public event EventHandler<EventArgs> OnClick;
        public override void Update()
        {
            base.Update();
            if(this.MouseOn && MouseSystem.IsLeftClick())
            {
                this.OnClick?.Invoke(this, EventArgs.Empty);
            }
        }
        public Color MouseOnColor { get; set; }
        public override void Draw()
        {
            if (!Father.IsEnabled) return;
            if (this.MouseOn)
            {
                float depthTemp = this.Depth;
                float depth2 = MathF.Max(0, this.Depth - 0.01f);
                Texture2D temp = Image;
                this.Image = FightResources.Sprites.pixiv;
                this.Depth = depth2;
                this.FormalDraw(this.Image, this.CollidingBox.ToRectangle(), MouseOnColor * 0.5f);
                DrawingLab.DrawRectangle(this.collidingBox, MouseOnColor, 3.0f, depth2);
                this.Image = temp;
                this.Depth = depthTemp;
            }
            base.Draw();
        }
    }
}
