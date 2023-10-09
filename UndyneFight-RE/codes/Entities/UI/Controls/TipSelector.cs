using Microsoft.Xna.Framework;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using System;

namespace UndyneFight_Ex.Remake.UI
{
    public class TipUI : SmartSelector
    { 
        private string _title;
        private string[] _message;
        public TipUI(string title, params string[] message)
        {
            this._title = title;
            this._message = message;
            this.KeyEvent = KeyEventFull;
            this.AddChild(confirm = new(this, new(350, -30), "Confirm"));
            this.AddChild(cancel = new(this, new(960 - 350, -30), "Cancel"));
            confirm.Depth = 0.6977f;
            cancel.Depth = 0.6977f;
            confirm.DefaultScale = 1.55f;
            cancel.DefaultScale = 1.55f;

            confirm.LeftClick += () => this.OnConfirm?.Invoke();
            cancel.LeftClick += Back;
            this.Activate();
        }
        public event Action OnConfirm;

        Button confirm, cancel;

        static col[] colors = {
            col.Purple, col.Lerp(col.Purple, col.MediumPurple, 0.4f),
            col.Lerp(col.Purple, col.MediumPurple, 0.5f), col.Lerp(col.Purple, col.MediumPurple, 1f),
            col.Lerp(col.Purple, col.MediumPurple, 0.5f), col.Lerp(col.Purple, col.MediumPurple, 0.4f)

        };
        vec2[] points = {
            new(230, -150), new(730 - 30, -150), new(730, -150 + 30),
            new(730, 170), new(230 + 30, 170), new(230, 170 - 30)
        };

        float y = -130;
        float targetY = 360;
        bool available = true;

        protected void Back()
        {
            targetY = -230;
            this.UpdateChildren = false;
            available = false;
        }

        public override void Draw()
        {
            vec2 del = new(0, y);
            VPC[] all = new VPC[6];
            for (int i = 0; i < all.Length; i++)
            {
                all[i] = new VPC(new(points[i] + del, 0), colors[i] * 0.95f);
            }
            SpriteBatch.DrawVertex(0.6663f, all);
            for (int i = 0; i < 5; i++)
            {
                DrawingLab.DrawLine(points[i] + del, points[i + 1] + del, 4f, col.White, 0.6666f);
            }
            DrawingLab.DrawLine(points[0] + del, points[5] + del, 4f, col.White, 0.6666f);

            DrawingLab.DrawLine(new vec2(270, -72) + del, new vec2(690, -72) + del, 3f, col.Silver, 0.668f);

            GLFont font = FightResources.Font.NormalFont;
            font.CentreDraw(_title, new vec2(480, -105) + del, col.Gold, 1.6f, 0.69f);
            for(int i = 0; i < _message.Length; i++)
                font.Draw(_message[i], new vec2(251, -60 + i * 40) + del, col.White, 1.123f, 0.69f);

        }
        public override void Update()
        {
            y = MathHelper.Lerp(y, targetY, 0.1f);
            if (available)
                base.Update();
            confirm.ResetPosition(new(375, y + 133));
            cancel.ResetPosition(new(960 - 340, y + 133));
            if (!available && y < -120) this.Dispose();
        }
    }
}