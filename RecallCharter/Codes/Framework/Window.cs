using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex;
using UndyneFight_Ex.Remake;

namespace RecallCharter
{
    internal class Window : Control
    {
        public static List<Window> Windows = new List<Window>();
        public Window() { 
            Windows.Add(this); 
        }
        public Color BackgroundColor { get; set; } = Color.DimGray * 0.5f;
        public Color BorderColor { get; set; } = Color.Gray * 0.9f;

        public string Title { get;set; }

        public override void Start()
        {
            Vector2 tr = new Vector2(this.collidingBox.Width, 0);
            tr += new Vector2(-20, 4);

            CollideRect area = new(tr, new Vector2(18, 18)); 

            this.closeButton = new Button() { CollidingBox = area, MouseOnColor = Color.Silver,
                 Image = Resources.XImage, Depth = this.Depth + 0.01f
            };
            this.AddChild(closeButton);

            base.Start();
        }

        Button closeButton;

        CollideRect topBar;
        
        public override void Draw()
        {
            if (!Father.IsEnabled)
                return;
            this.Image = FightResources.Sprites.pixiv;
            float dep = this.Depth;
            this.FormalDraw(Image, this.CollidingBox.ToRectangle(), this.BackgroundColor);
            this.Depth = dep + 0.005f;
            this.FormalDraw(Image, this.topBar.ToRectangle(), this.BorderColor);
            this.Depth = dep;
            DrawingLab.DrawRectangle(this.CollidingBox, this.BorderColor, 2.0f, this.Depth + 0.01f);

            UndyneFight_Ex.Remake.Resources.Font.Normal.Draw(this.Title, this.CollidingBox.TopLeft + new Vector2(5, 1), Color.White, 0.8f, this.Depth + 0.015f);

            if (!this.IsEnabled) this.IsEnabled = true;
        }
        public override void Update()
        {
            base.Update();
            topBar = this.CollidingBox;
            topBar.Height = 27;
        }

        public override void Dispose()
        {
            Windows.Remove(this);
            base.Dispose();
        }
    }
}