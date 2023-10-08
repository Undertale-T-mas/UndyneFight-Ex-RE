using static UndyneFight_Ex.FightResources.Font;
using Microsoft.Xna.Framework;
using System.Linq;
using UndyneFight_Ex.Remake.Data;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.Remake.FileData;
using UndyneFight_Ex.Remake.Network;
using static UndyneFight_Ex.PlayerManager;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class UserUI
    {
        public static void AutoAuthentic(string rememberedUser, string pswd)
        {
            Login(rememberedUser);
            string password = pswd;
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
                        login.SendRequest("Log\\in\\" + rememberedUser + "\\" + newPassword);
                        keyPeriod = false;
                    }
                }
                else
                {
                    if (s.Info == "user not exist")
                    {
                        var v = new PageTips.OnlineRegisterUI(rememberedUser, newPassword);
                        DEBUG.IntroUI.PendingTip(v);
                    }
                    else if (s.Info == "success login")
                    {
                        KeepAliver.TryCreate();
                        CurrentUser.OnlineAsync = true;
                        CurrentUser.PasswordMemory = pswd;
                    }
                }
            });
            login.SendRequest($"Log\\key\\none");
        }
        internal class LoginUI : SmartSelector
        {
            GlobalDataRoot.UserMemory GlobalMemory = GlobalData.Memory;
            private void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
            {
                DrawingLab.DrawLine(start, end, size, color, 0.5f);
            }
            public override void Start()
            {
                this._virtualFather = FatherObject as VirtualFather;
                base.Start();
            }
            public LoginUI()
            {
                //L = 65, R = 325
                NameInitialize();
                this.collidingBox.Size = new Vector2(135, 75);
                this.SetChild();
                this.OneSelectionOnly = true;

                this.KeyEvent = LoginKeyChange;

                this.OnSelected += Selected;
                this.OnActivated += () => _virtualFather?.Select(this);
            }
            private VirtualFather _virtualFather;
            private void Selected()
            {
                SelectingModule selected = CurrentSelected;
                if (selected == _confirm)
                {
                    DoConfirm();
                }
                else if (selected == _cancel)
                {
                    DoBack();
                }
            }

            private void DoBack()
            {
                this._virtualFather.FatherObject.Dispose();
                InstanceCreate(new DEBUG.IntroUI());
            }

            private void DoConfirm()
            {
                string result = TryLogin(_account.Result, _password.Result);
                if (result == "Success!")
                {
                    GlobalMemory.AutoAuthentic.Value = _autoAuthentic.Ticked;
                    if (_autoAuthentic.Ticked || _remember.Ticked)
                    {
                        GlobalMemory.RememberUser.Value = _account.Result;
                    }
                    if (_autoAuthentic.Ticked)
                    {
                        GlobalMemory.PasswordMem.Value = _password.Result;
                    }
                    SaveGlobal();
                    this.FatherObject?.FatherObject?.Dispose();
                    Login(_account.Result);
                    DEBUG.IntroUI introUI;
                    InstanceCreate(introUI = new DEBUG.IntroUI());
                    SendLoginRequest();
                }
                else
                {
                    InstanceCreate(new InfoText(result, new Vector2(672, 400)) { DrawingColor = Color.Red });
                }
            }
            private void SendLoginRequest()
            {
                string password = _password.Result;
                string newPassword = "";
                bool keyPeriod = true;
                UFSocket<Empty> login = null;
                login = new((s) =>
                {
                    if (keyPeriod) {
                        if (s.Info[0..4] == "<RSA")
                        {
                            newPassword = MathUtil.Encrypt(password, s.Info);
                            login.SendRequest("Log\\in\\" + _account.Result + "\\" + newPassword);
                            keyPeriod = false;
                        } 
                    }
                    else
                    {
                        if (s.Info == "user not exist")
                        {
                            var v = new PageTips.OnlineRegisterUI(_account.Result, newPassword);
                            DEBUG.IntroUI.PendingTip(v); 
                        }
                        else if(s.Info == "success login")
                        {
                            KeepAliver.TryCreate();
                            CurrentUser.OnlineAsync = true;
                        }
                    }
                });
                login.SendRequest($"Log\\key\\none");
           //     login.SendRequest($"Log\\in\\{_account.Result}\\{_password.Result}");
            }

            private void NameInitialize()
            {
                allNames = playerInfos.Keys.ToArray();
            }

            // 0
            // 1
            //2 3
            //4 5
            int[] _downNext = { 1, 2, 4, 5, 4, 5 };
            int[] _upNext = { 0, 0, 1, 1, 2, 3 };
            private void LoginKeyChange()
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

            private string[] allNames;
            Button _confirm, _cancel;
            TextInputer _account, _password;
            TickBox _remember, _autoAuthentic;
            private void SetChild()
            {
                ChildObjects.Clear();
                ChildObjects.Add(_account = new SmartInputer(allNames, this, new CollideRect(new Vector2(571, 66), new Vector2(330, 50))) { FontScale = 1.2f });
                ChildObjects.Add(_password = new PasswordInputer(this, new CollideRect(new Vector2(571, 156), new Vector2(330, 50))) { FontScale = 1.2f });
                ChildObjects.Add(_remember = new TickBox(this, new Vector2(543, 255), "Remember me") { DefaultScale = 1.1f });
                ChildObjects.Add(_autoAuthentic = new TickBox(this, new Vector2(800, 255), "Auto login") { DefaultScale = 1.1f });

                ChildObjects.Add(_confirm = new Button(this, new Vector2(543, 315), "Confirm") { NeverEnable = true });
                ChildObjects.Add(_cancel = new Button(this, new Vector2(800, 315), "Cancel") { NeverEnable = true });
                 
                if (CurrentUser != null)
                {
                    InstanceCreate(new InstantEvent(2, () => {
                        this.FatherObject?.FatherObject?.Dispose();
                    }));
                    InstanceCreate(new SelectUI());
                }
                else if (GlobalMemory.RememberUser != "null")
                {
                    _account.SetString(GlobalMemory.RememberUser);
                    _remember.Tick();
                }
            }

            public override void Draw()
            {
                NormalFont.CentreDraw("Login", this.Centre, DrawingColor, 1.8f * _secondaryScale, 0.1f);

                if (!Activated) return;
                NormalFont.CentreDraw("Account", new Vector2(480, 65), Color.White, 1.3f, 0.1f);
                NormalFont.CentreDraw("Name", new Vector2(480, 100), Color.White, 1.3f, 0.1f);

                //float l2 = 390, r2 = 900;
                DrawLine(new(390, 105), new(410, 125), Color.White);
                DrawLine(new(410, 125), new(550, 125), Color.White);

                NormalFont.CentreDraw("Pass", new Vector2(480, 155), Color.White, 1.3f, 0.1f);
                NormalFont.CentreDraw("code", new Vector2(480, 190), Color.White, 1.3f, 0.1f);

                //float l2 = 390, r2 = 900;
                DrawLine(new(390, 195), new(410, 215), Color.White);
                DrawLine(new(410, 215), new(550, 215), Color.White);
            }
            float _secondaryScale = 1.0f;

            public override void Update()
            {
                this.Centre = new Vector2(204, 84);
                base.Update();
                if (this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {
                    this._secondaryScale = MathHelper.Lerp(_secondaryScale, 1.1f, 0.1f);
                }
                else _secondaryScale = MathHelper.Lerp(_secondaryScale, 1.0f, 0.1f);
            }
        }
    }
}