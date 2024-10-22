﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            if (this.MouseOn && MouseSystem.IsLeftClick())
            {
                this.OnClick?.Invoke(this, EventArgs.Empty);
            }
        }
        public Color MouseOnColor { get; set; }
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public override void Draw()
        {
            if (!Father.IsEnabled) return;
            Color _backColor = this.MouseOn ? MouseOnColor : BackgroundColor;
            float depthTemp = this.Depth;
            float depth2 = MathF.Max(0, this.Depth - 0.01f);
            Texture2D temp = Image;
            this.Image = FightResources.Sprites.pixUnit;
            this.Depth = depth2;
            this.FormalDraw(this.Image, this.CollidingBox.ToRectangle(), _backColor * 0.5f);
            if (this.MouseOn) DrawingLab.DrawRectangle(this.collidingBox, MouseOnColor, 3.0f, depth2);
            this.Image = temp;
            this.Depth = depthTemp;
            if (this.Image != null) this.FormalDraw(this.Image, this.Centre, Color.White, 0, ImageCentre);
            base.Draw();
        }
    }
}
