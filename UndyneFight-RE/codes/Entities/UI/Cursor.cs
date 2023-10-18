using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    public class MouseCursor : Entity
    {
        public MouseCursor() { 
            Image = Resources.UI.Cursor;
            UpdateIn120 = true; 
            ImageCentre = Vector2.Zero; 
        }
        public override void Draw()
        {
            this.FormalDraw(Image, temp, Color.White, 0.0f, ImageCentre);
        }
        Vector2 temp;
        public override void Update()
        {
            temp = MouseSystem.TransferredPosition;
            this.Depth = 0.7f;
        }
    }
}