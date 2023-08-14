using Microsoft.Xna.Framework;
using vec2 = Microsoft.Xna.Framework.Vector2;
using rect = UndyneFight_Ex.CollideRect;
using col = Microsoft.Xna.Framework.Color;
using VPCT = Microsoft.Xna.Framework.Graphics.VertexPositionColorTexture;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using System.Security.Principal;
using UndyneFight_Ex.Remake.Network;

namespace UndyneFight_Ex.Remake.UI.PageTips
{
    internal class OnlineRegisterUI : TipUI
    {
        string _name, _password;
        public OnlineRegisterUI(string name, string password) : base("Online Register", 
            "Your account is not saved",
            "online, you can press the",
            "confirm key to upload your",
            "account to cloud server"
            )
        {
            this._name = name;
            this._password = password;
            this.OnConfirm += OnlineRegisterUI_OnConfirm;
        }

        private void OnlineRegisterUI_OnConfirm()
        {
            UFSocket<Empty> login = new((s) =>
            {
                if (s.Info == "user not exist")
                {
                }
            });
            login.SendRequest($"Log\\reg\\{_name}\\{_password}");
        }
    } 
    internal class NameConflictUI : TipUI
    {
        string _name, _password;
        public NameConflictUI(string name, string password) : base("Name Conflict", 
            "Your name is used by other",
            "people. You need to change",
            "your name to successfully",
            "register in cloud server"
            )
        {
            this._name = name;
            this._password = password;
            this.OnConfirm += OnlineRegisterUI_OnConfirm;
        }

        private void OnlineRegisterUI_OnConfirm()
        {
            UFSocket<Empty> login = new((s) =>
            {
                if (s.Info == "user not exist")
                {
                }
            });
            login.SendRequest($"Log\\reg\\{_name}\\{_password}");
        }
    } 
}