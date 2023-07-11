using Microsoft.Xna.Framework;
using UndyneFight_Ex.Network;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal partial class AccountManager
    {
        private partial class GamejoltManager : TextSelection
        {
            public GamejoltManager() : base("Gamejolt", new(320, instance.SelectionCount * 50 + 110))
            {
            }
            public override void SelectionEvent()
            {
                instance.Dispose(); InstanceCreate(PlayerManager.CurrentUser.GameJoltInformation.Authed ?
                    new GamejoltModifyUI() : new GamejoltLoginUI());
                base.SelectionEvent();
            }
            private class GamejoltLoginUI : OKCancelSelector
            {
                private string currentAccount = "";
                private string name;
                private string token;

                GamejoltLogin loginner = new();

                private class InfoText : Entity
                {
                    static InfoText last;
                    public InfoText(string text, Color color)
                    {
                        last?.Dispose();
                        last = this;
                        this.text = text;
                        this.color = color;
                    }
                    string text; Color color;

                    public override void Draw()
                    {
                        NormalFont.CentreDraw(text, new(320, 288), color * alpha, 0.7f, 0.5f);
                    }
                    float alpha = 1; float time = 0;

                    public override void Update()
                    {
                        time += 0.5f;
                        if (time > 30f) alpha -= 0.02f;
                        if (alpha < 0) Dispose();
                    }
                }

                public GamejoltLoginUI()
                {
                    name = PlayerManager.CurrentUser.GameJoltInformation.GameJoltID;
                    token = PlayerManager.CurrentUser.GameJoltInformation.Token;

                    OKAction += GamejoltLogin;

                    UpdateIn120 = true;
                    AutoDispose = false;
                    IsCancelAvailable = false;

                    bool a = false;
                    SelectChanged += () =>
                    {
                        if (a)
                            PlaySound(changeSelection, 0.9f);
                    };
                    a = true;
                    Selected += () =>
                    {
                        if (currentSelect > 2)
                            PlaySound(select);
                    };
                    SelectChanger += () =>
                    {
                        if (CharInput != 1) return;
                        if (IsKeyPressed120f(InputIdentity.MainUp))
                        {
                            currentSelect--;
                        }
                        else if (IsKeyPressed120f(InputIdentity.MainDown))
                        {
                            currentSelect++;
                        }
                        if (currentSelect >= SelectionCount) currentSelect -= SelectionCount;
                        else if (currentSelect < 0) currentSelect = SelectionCount - 1;
                    };

                    PushSelection(nameInputer = new TextInputer(new CollideRect(290, 150, 290, 38)));
                    PushSelection(passwordInputer = new TextInputer(new CollideRect(290, 200, 290, 38)));

                    nameInputer.SetString(name);
                    passwordInputer.SetString(token);

                    loginner.LoginFailure += LoginFailure;
                    loginner.LoginSuccess += LoginSuccess;

                    AddChild(loginner);
                }

                private void LoginSuccess(GameJolt.Response<GameJolt.Objects.Credentials> obj)
                {
                    GameJolt.Objects.Credentials credentials = obj.Data;
                    name = credentials.Name;
                    token = credentials.Token;

                    var info = PlayerManager.CurrentUser.GameJoltInformation;
                    info.GameJoltID = name;
                    info.Token = token;
                    info.Authed = true;
                    info.Credential = credentials;

                    PlayerManager.Save();

                    AddChild(new InfoText("Success", Color.Lime));
                }

                TextInputer nameInputer, passwordInputer;

                private void LoginFailure(GameJolt.Response<GameJolt.Objects.Credentials> obj)
                {
                    AddChild(new InfoText(obj.Message, Color.Red));
                }

                private bool GamejoltLogin()
                {
                    if (loginner.Working)
                    {
                        AddChild(new InfoText("Please wait", Color.Red));
                        return false;
                    }
                    var info = PlayerManager.CurrentUser.GameJoltInformation;
                    if (info.Authed && nameInputer.Result == info.GameJoltID)
                    {
                        AddChild(new InfoText("Already authed!", Color.Red));
                        return false;
                    }
                    loginner.Login(nameInputer.Result, passwordInputer.Result);
                    AddChild(new InfoText("Please wait", Color.White));
                    return false;
                }

                public override void Draw()
                {
                    NormalFont.CentreDraw("Gamejolt", new Vector2(320, 45), Color.White);
                    NormalFont.CentreDraw("Current account ID:" + currentAccount, new Vector2(320, 87), Color.White, 0.8f, 0.1f);
                    NormalFont.Draw("Gamejolt Name", new Vector2(70, 150), Color.White);
                    NormalFont.Draw("Your Token", new Vector2(70, 200), Color.White);

                    base.Draw();
                }


            }
        }
    }
}