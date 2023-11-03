using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.Entities
{ 
    public class Shattered : Entity
    {
        public Shattered(Entity origin) {
            this.AngleMode = origin.AngleMode;
            this.Image = origin.Image;
            this.Centre = origin.Centre;
            this.Rotation = origin.Rotation;
            this.Scale = origin.Scale;
            this.UpdateIn120 = true;
            rect1 = new CollideRect(ImageCentre, ImageCentre);
            rect2 = new CollideRect(new Vector2(0, ImageCentre.Y), ImageCentre);
            rect3 = new CollideRect(Vector2.Zero, ImageCentre);
            rect4 = new CollideRect(new Vector2(ImageCentre.X, 0), ImageCentre);
            this.distance = ImageCentre / 2f;
        }

        public override void Draw()
        {
            this.FormalDraw(this.Image, pos1.ToRectangle(), rect1.ToRectangle(), Color.White * alpha);
            this.FormalDraw(this.Image, pos2.ToRectangle(), rect2.ToRectangle(), Color.White * alpha);
            this.FormalDraw(this.Image, pos3.ToRectangle(), rect3.ToRectangle(), Color.White * alpha);
            this.FormalDraw(this.Image, pos4.ToRectangle(), rect4.ToRectangle(), Color.White * alpha);
        }
        Vector2 distance = Vector2.Zero;
        float alpha = 1.0f;

        CollideRect pos1, pos2, pos3, pos4;
        CollideRect rect1, rect2, rect3, rect4;

        public override void Update()
        {
            this.alpha -= 0.035f;
            if(this.alpha < 0.0f) {  this.Dispose(); }
            this.distance += this.ImageCentre * this.Scale * 0.035f;
            CollideRect centre = new();
            centre.Size = this.ImageCentre;
            centre.SetCentre(this.Centre);
            pos1.Size = this.ImageCentre; pos2.Size = this.ImageCentre; 
            pos3.Size = this.ImageCentre; pos4.Size = this.ImageCentre;
            pos1.SetCentre(this.Centre + distance);
            pos2.SetCentre(this.Centre + distance * new Vector2(-1, 1));
            pos3.SetCentre(this.Centre + distance * new Vector2(-1, -1));
            pos4.SetCentre(this.Centre + distance * new Vector2(1, -1));

        }
    }
}