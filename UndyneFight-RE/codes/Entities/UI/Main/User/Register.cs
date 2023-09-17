using static UndyneFight_Ex.FightResources.Font;
using Microsoft.Xna.Framework;
using System.Xml;
using System;
using System.Linq;
using UndyneFight_Ex.Remake.Data;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Remake.Network;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class UserUI
    {
        private class RegisterUI : SmartSelector
        {
            public RegisterUI()
            {
                UpdateIn120 = true;
                this.collidingBox.Size = new Vector2(135, 75);
                this.OnActivated += () => _virtualFather.Select(this);
                this.SetChild();
                this.OneSelectionOnly = true;
                this.KeyEvent = RegKeyChange;
                this.OnSelected += Selected;
            }

            private void Selected()
            { 
                if(this.CurrentSelected == _confirm)
                {
                    //confirm:
                    if (string.IsNullOrEmpty(this._account.Result))
                    {
                        InstanceCreate(new InfoText("Input user name!", new Vector2(672, 400)) { DrawingColor = Color.Red });
                    }
                    else if (string.IsNullOrEmpty(this._password.Result))
                    {
                        InstanceCreate(new InfoText("Input password!", new Vector2(672, 400)) { DrawingColor = Color.Red });
                    }
                    else if (string.IsNullOrEmpty(this._password2.Result))
                    {
                        InstanceCreate(new InfoText("Input password twice!", new Vector2(672, 400)) { DrawingColor = Color.Red });
                    }
                    else if (this._password2.Result != this._password.Result)
                    {
                        InstanceCreate(new InfoText("Your two passwords are not same", new Vector2(672, 400)) { DrawingColor = Color.Red });
                    }
                    else if (PlayerManager.playerInfos.ContainsKey(this._account.Result))
                    {
                        InstanceCreate(new InfoText("User already exists!", new Vector2(672, 400)) { DrawingColor = Color.Red });
                    }
                    else
                    {
                        PlayerManager.AddNewUser(_account.Result, this._password2.Result);
                        this._virtualFather.FatherObject.Dispose();
                        SendRegRequest();
                        InstanceCreate(new DEBUG.IntroUI());
                    }
                }
                else if (CurrentSelected == _cancel)
                {
                    DoBack();
                }
            }
            private void SendRegRequest()
            {
                string password = _password.Result;
                string newPassword = "";
                bool keyPeriod = true;
                UFSocket<Empty> login = null;
                login = new((s) =>
                {
                    if (keyPeriod)
                    {
                        if (s.Info[0..4] == "<RSA")
                        {
                            newPassword = MathUtil.Encrypt(password, s.Info);
                            login.SendRequest("Log\\reg\\" + _account.Result + "\\" + newPassword);
                            keyPeriod = false;
                        }
                    }
                    else
                    { 
                        if (s.Info == "success login")
                        {
                            PlayerManager.CurrentUser.OnlineAsync = true;
                            KeepAliver.TryCreate();
                        }
                        else if(s.Info == "the name already exists")
                        {
                            var v = new PageTips.NameConflictUI(_account.Result, newPassword);
                            DEBUG.IntroUI.PendingTip(v); 
                        }
                    }
                });
                login.SendRequest($"Log\\key\\none");
                //     login.SendRequest($"Log\\in\\{_account.Result}\\{_password.Result}");
            }

            private void RegKeyChange()
            {
                if (IsKeyPressed120f(InputIdentity.MainRight) && currentFocus is not TextInputer)
                {
                    int id = FocusID;
                    for (int i = id + 1; i < all.Length; i++)
                    {
                        if (all[i].ModuleEnabled)
                        {
                            currentFocus.OffFocus();
                            all[i].OnFocus();
                            break;
                        }
                    }
                }
                else if (IsKeyPressed120f(InputIdentity.MainLeft) && currentFocus is not TextInputer)
                {
                    int id = FocusID;
                    for (int i = id - 1; i >= 0; i--)
                    {
                        if (all[i].ModuleEnabled)
                        {
                            currentFocus.OffFocus();
                            all[i].OnFocus();
                            break;
                        }
                    }
                }

                if (IsKeyPressed120f(InputIdentity.MainDown))
                {
                    int id = _downNext[FocusID];
                    currentFocus.OffFocus();
                    all[id].OnFocus();
                }
                if (IsKeyPressed120f(InputIdentity.MainUp))
                {
                    int id = _upNext[FocusID];
                    currentFocus.OffFocus();
                    all[id].OnFocus();
                }
                if (IsKeyPressed120f(InputIdentity.Confirm))
                {
                    currentFocus?.ConfirmKeyDown();
                }
            }

            private void SetChild()
            {
                ChildObjects.Clear();
                ChildObjects.Add(_account = new TextInputer(this, new CollideRect(new Vector2(571, 66), new Vector2(330, 50))) { FontScale = 1.2f });
                ChildObjects.Add(_password = new PasswordInputer(this, new CollideRect(new Vector2(571, 153), new Vector2(330, 50))) { FontScale = 1.2f });
                ChildObjects.Add(_password2 = new PasswordInputer(this, new CollideRect(new Vector2(571, 240), new Vector2(330, 50))) { FontScale = 1.2f });


                ChildObjects.Add(_confirm = new Button(this, new Vector2(543, 330), "Confirm") { NeverEnable = true });
                ChildObjects.Add(_cancel = new Button(this, new Vector2(800, 330), "Cancel") { NeverEnable = true });
            }

            VirtualFather _virtualFather;
            public override void Start()
            {
                _virtualFather = FatherObject as VirtualFather;
                base.Start();
            }
            private void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
            {
                DrawingLab.DrawLine(start, end, size, color, 0.5f);
            }
            public override void Draw()
            {
                NormalFont.CentreDraw("Register", this.Centre, DrawingColor, 1.8f * _secondaryScale, 0.1f);

                if (!Activated) return;
                NormalFont.CentreDraw("Account", new Vector2(480, 65), Color.White, 1.3f, 0.1f);
                NormalFont.CentreDraw("Name", new Vector2(480, 100), Color.White, 1.3f, 0.1f);

                //float l2 = 390, r2 = 900;
                DrawLine(new(390, 105), new(410, 125), Color.White);
                DrawLine(new(410, 125), new(550, 125), Color.White);

                NormalFont.CentreDraw("Pass", new Vector2(480, 152), Color.White, 1.3f, 0.1f);
                NormalFont.CentreDraw("code", new Vector2(480, 187), Color.White, 1.3f, 0.1f);

                //float l2 = 390, r2 = 900;
                DrawLine(new(390, 192), new(410, 212), Color.White);
                DrawLine(new(410, 212), new(550, 212), Color.White);

                NormalFont.CentreDraw("Passcode", new Vector2(480, 239), Color.White, 1.3f, 0.1f);
                NormalFont.CentreDraw("again", new Vector2(480, 274), Color.White, 1.3f, 0.1f);

                //float l2 = 390, r2 = 900;
                DrawLine(new(390, 279), new(410, 299), Color.White);
                DrawLine(new(410, 299), new(550, 299), Color.White);
            }
            float _secondaryScale = 1.0f;
            private TextInputer _account;
            private PasswordInputer _password, _password2;
            private Button _confirm, _cancel;
            /*
             0
             1
             2
            3 4
             */
            private int[] _downNext = { 1, 2, 3, 3, 4};
            private int[] _upNext = { 0, 0, 1, 2, 2};

            public override void Update()
            {
                this.Centre = new Vector2(204, 169);
                base.Update();
                if (this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {
                    this._secondaryScale = MathHelper.Lerp(_secondaryScale, 1.1f, 0.1f);
                }
                else _secondaryScale = MathHelper.Lerp(_secondaryScale, 1.0f, 0.1f);
            }
            private void DoBack()
            {
                this._virtualFather.FatherObject.Dispose();
                InstanceCreate(new DEBUG.IntroUI());
            }
        }
    }
}