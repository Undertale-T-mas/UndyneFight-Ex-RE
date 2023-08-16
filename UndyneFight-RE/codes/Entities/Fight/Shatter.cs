using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using System.Linq.Expressions;
using System.Collections.Specialized;

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
            rest1 = new CollideRect(ImageCentre, ImageCentre);
            rest2 = new CollideRect(new Vector2(0, ImageCentre.Y), ImageCentre);
            rest3 = new CollideRect(Vector2.Zero, ImageCentre);
            rest4 = new CollideRect(new Vector2(ImageCentre.X, 0), ImageCentre);
            this.distance = ImageCentre / 2f;
        }

        public override void Draw()
        {
            this.FormalDraw(this.Image, pos1.ToRectangle(), rest1.ToRectangle(), Color.White * alpha);
            this.FormalDraw(this.Image, pos2.ToRectangle(), rest2.ToRectangle(), Color.White * alpha);
            this.FormalDraw(this.Image, pos3.ToRectangle(), rest3.ToRectangle(), Color.White * alpha);
            this.FormalDraw(this.Image, pos4.ToRectangle(), rest4.ToRectangle(), Color.White * alpha);
        }
        Vector2 distance = Vector2.Zero;
        float alpha = 1.0f;

        CollideRect pos1, pos2, pos3, pos4;
        CollideRect rest1, rest2, rest3, rest4;

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