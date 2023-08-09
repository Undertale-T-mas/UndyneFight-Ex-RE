using Microsoft.Xna.Framework;
using vec2 = Microsoft.Xna.Framework.Vector2;
using rect = UndyneFight_Ex.CollideRect;
using col = Microsoft.Xna.Framework.Color;
using VPCT = Microsoft.Xna.Framework.Graphics.VertexPositionColorTexture;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using System.Security.Principal;
using UndyneFight_Ex.Remake.Network;

namespace UndyneFight_Ex.Remake.UI
{
    internal class OnlineRegisterUI : SmartSelector
    {
        public string PasswordTosend { private get; set; }
        public OnlineRegisterUI() {
            this.KeyEvent = KeyEventFull;
            this.AddChild(confirm = new(this, new(350, -30), "confirm"));
            this.AddChild(cancel = new(this, new(960 - 350, -30), "cancel"));
            confirm.Depth = 0.6977f;
            cancel.Depth = 0.6977f;
            confirm.DefaultScale = 1.55f;
            cancel.DefaultScale = 1.55f;

            confirm.LeftClick += Back;
            confirm.LeftClick += Confirm_LeftClick;
            cancel.LeftClick += Back;
            this.Activate();
        }

        private void Confirm_LeftClick()
        {
            UFSocket<Empty> login = new((s) => {
                if (s.Info == "user not exist")
                { 
                }
            });
            login.SendRequest($"Log\\reg\\{PlayerManager.CurrentUser.PlayerName}\\{PasswordTosend}"); 
        }

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

        private void Back()
        {
            targetY = -230;
            this.UpdateChildren = false;
            available = false;
        }

        public override void Draw()
        {
            vec2 del = new(0, y);
            VPC[] all = new VPC[6];
            for(int i = 0; i < all.Length; i++)
            {
                all[i] = new VPC(new(points[i] + del, 0), colors[i] * 0.95f);
            }
            SpriteBatch.DrawVertex(0.6663f, all);
            for(int i = 0; i < 5; i++)
            {
                DrawingLab.DrawLine(points[i] + del, points[i + 1] + del, 4f, col.White, 0.6666f);
            }
            DrawingLab.DrawLine(points[0] + del, points[5] + del, 4f, col.White, 0.6666f);

            DrawingLab.DrawLine(new vec2(270, -72) + del, new vec2(690, -72) + del, 3f, col.Silver, 0.668f);

            GLFont font = FightResources.Font.NormalFont;
            font.CentreDraw("Online Register", new vec2(480, -105) + del, col.Gold, 1.6f, 0.69f);
            font.Draw("Your account is not saved", new vec2(251, -60) + del, col.White, 1.123f, 0.69f);
            font.Draw("online, you can press the", new vec2(251, -20) + del, col.White, 1.123f, 0.69f);
            font.Draw("confirm key to upload your", new vec2(251, 20) + del, col.White, 1.123f, 0.69f);
            font.Draw("account to cloud server", new vec2(251, 60) + del, col.White, 1.123f, 0.69f);

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