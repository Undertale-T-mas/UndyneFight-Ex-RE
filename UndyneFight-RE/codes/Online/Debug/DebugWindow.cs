using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UndyneFight_Ex.Entities;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Remake.Components;
using UndyneFight_Ex.Remake.Effects;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.UserService;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.Remake.Network;
using System.Security.Principal;
using UndyneFight_Ex.Remake.UI.DEBUG;

namespace UndyneFight_Ex.Remake.UI
{
    internal class DebugWindow : SmartSelector
    {
        Button connect, back, login;
        private bool alive = false;
        private bool checkingAlive = false;
        private void Update(bool alive)
        {
            checkingAlive = false;
            this.alive = alive;
            if (alive) {
                connect.ChangeText("Connected");
                connect.State = SelectState.Disabled;
            }
            else
            {
                connect.NeverEnable = true;
                connect.State = SelectState.False;
            }
        }
        public DebugWindow() {
            this.KeyEvent = KeyEventFull;
            this.AddChild(back = new Button(this, new(100, 680), "<<Back") { State = SelectState.False });
            this.AddChild(connect = new Button(this, new(350, 680), "Connected") { State = SelectState.Disabled });
            this.AddChild(login = new Button(this, new(600, 680), "Login") { State = SelectState.False });

            back.LeftClick += Back_LeftClick;
            connect.LeftClick += Connect_LeftClick;
            login.LeftClick += Login_LeftClick;

            this.Activate();

            KeepAliver.CheckAlive(null, Update);
        }

        private void Login_LeftClick()
        {
            bool keyPeriod = false;
            string password = "DEBUG123abc";
            string newPassword = string.Empty;
            UFSocket<Empty> login = null;
            login = new((s) =>
            {
                if (keyPeriod)
                {
                    if (s.Info[0..4] == "<RSA")
                    {
                        newPassword = MathUtil.Encrypt(password, s.Info);
                        login.SendRequest("Log\\in\\" + "DEBUG" + "\\" + newPassword);
                        keyPeriod = false;
                    }
                }
                else
                {
                    if (s.Info == "user not exist")
                    {
                        var v = new PageTips.OnlineRegisterUI("DEBUG", newPassword);
                        DEBUG.IntroUI.PendingTip(v);
                    }
                    else if (s.Info == "success login")
                    {
                        if (!KeepAliver.IsAlive)
                            GameStates.InstanceCreate(new KeepAliver());
                    }
                }
            });
            login.SendRequest($"Log\\key\\none");
        }

        private void Back_LeftClick()
        {
            this.Dispose();
            GameStates.InstanceCreate(new DEBUG.IntroUI());
        }

        private void Connect_LeftClick()
        {
            if (checkingAlive) return;
            checkingAlive = true;
            KeepAliver.CheckAlive(null, Update);
        }

        public override void Start()
        {
            base.Start();
            this.AddChild(new MouseCursor());
            this.AddChild(new PromptDialog());
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        { 
        }
    }
}