using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.UserService;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal partial class AccountManager : Selector
    {
        private static AccountManager instance;

        private void GainMessage()
        {
            name = PlayerManager.currentPlayer;
            info = PlayerManager.playerInfos[name];
            isVIP = info.VIP;
        }

        public AccountManager() : base(true)
        {
            AutoDispose = false;
            instance = this;

            GainMessage();

            ResetSelect();
            PlaySound(select, 0.9f);
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };
            SelectChanger += () =>
            {
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
            PushSelection(new Rename());
            PushSelection(isVIP ? new ChangeColor() : new BecomeVIP());
            PushSelection(new RatingList());
            PushSelection(new ShopMall());
            PushSelection(new Statistic());
            PushSelection(new GamejoltManager());
            PushSelection(new ChangePassword());

            Selected += () =>
            {
                PlaySound(select, 0.9f);
            };
        }

        #region Player's Information
        private string name;
        private User info;
        private bool isVIP;
        #endregion

        public override void Draw()
        {
            NormalFont.CentreDraw(name, new Vector2(320, 50), isVIP ? Color.Gold : Color.White);
            base.Draw();
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Reverse()
        {
            GainMessage();
            base.Reverse();
        }

        private class BecomeVIP : TextSelection
        {
            public BecomeVIP() : base("Input VIP key", new Vector2(320, instance.SelectionCount * 50 + 110))
            { }
            public override void SelectionEvent()
            {
                instance.Dispose();
                InstanceCreate(new VIPManager());
                base.SelectionEvent();
            }
            private class VIPManager : OKCancelSelector
            {
                private readonly TextInputer textInputer;
                public VIPManager()
                {
                    textInputer = new TextInputer(new CollideRect(270, 160, 260, 38));
                    PushSelection(textInputer);

                    SelectChanger += () =>
                    {
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
                    ResetSelect();
                    SelectChanged += () =>
                    {
                        PlaySound(changeSelection, 0.9f);
                    };

                    OKAction += () =>
                    {
                        string key = textInputer.Result;

                        if (string.IsNullOrEmpty(key))
                        {
                            textInputer.SetString("Wrong!");
                            return false;
                        }
                        else
                        {
                            if (GetValue(key) == MathUtil.StringHash(PlayerManager.currentPlayer))
                            {
                                PlayerManager.CurrentUser.IntoVIP();

                                PlayerManager.Save();

                                var last = Last;
                                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                                object tem = asm.CreateInstance(last.GetType().FullName);

                                InstanceCreate(tem as Entity);

                                Dispose();
                                ResetScene(new GameMenuScene());
                            }
                            else
                            {
                                textInputer.SetString("Wrong!");
                                return false;
                            }
                        }

                        return true;
                    };

                }
                private static ulong GetValue(string s)
                {
                    ulong res = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        res = res * 10 + Convert.ToUInt64(Math.Abs(s[i] - '0'));
                    }
                    return res;
                }

                public override void Draw()
                {
                    NormalFont.CentreDraw("Input VIP key", new Vector2(320, 80), Color.White);
                    NormalFont.Draw("VIP Key", new Vector2(80, 160), Color.White);

                    base.Draw();
                }
            }
        }

        private class Rename : TextSelection
        {
            public Rename() : base("Rename", new Vector2(320, instance.SelectionCount * 50 + 110))
            { }
            public override void SelectionEvent()
            {
                instance.Dispose();
                InstanceCreate(new RenameManager());
                base.SelectionEvent();
            }
            internal class RenameManager : OKCancelSelector
            {
                private readonly TextInputer textInputer;
                public RenameManager()
                {
                    textInputer = new TextInputer(new CollideRect(270, 160, 260, 38));
                    PushSelection(textInputer);

                    SelectChanger += () =>
                    {
                        if (WordsChanged) return;
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
                    ResetSelect();
                    SelectChanged += () =>
                    {
                        PlaySound(changeSelection, 0.9f);
                    };

                    OKAction += () =>
                    {
                        string res = textInputer.Result;
                        if (res == PlayerManager.currentPlayer)
                        {
                            return false;
                        }

                        PlayerManager.Rename(PlayerManager.currentPlayer, res);

                        Back();
                        return true;
                    };
                }

                public override void Draw()
                {
                    NormalFont.CentreDraw("Rename", new Vector2(320, 80), Color.White);
                    NormalFont.Draw("New Name: ", new Vector2(100, 160), Color.White);

                    base.Draw();
                }
            }
        }

        private class ChangeColor : TextSelection
        {
            private void Change()
            {
                switch (GameRule.nameColor)
                {
                    case "White":
                        GameRule.nameColor = "Blue";
                        break;
                    case "Blue":
                        GameRule.nameColor = (PlayerManager.PlayerSkill >= 1) ?
                        "Orange" : "White";
                        break;
                    case "Orange":
                        GameRule.nameColor = (PlayerManager.PlayerSkill >= 2) ?
                        "Colorful" : "White";
                        break;
                    case "Colorful":
                        GameRule.nameColor = "White";
                        break;
                }
            }
            public ChangeColor() : base("Current name color: " + GameRule.nameColor, new Vector2(320, instance.SelectionCount * 50 + 110))
            {
                MaxSize = 1.2f;
            }
            public override void SelectionEvent()
            {
                Change();
                texts = "Current name color: " + GameRule.nameColor;
                base.SelectionEvent();
            }
        }

        private class Statistic : TextSelection
        {
            public Statistic() : base("Statistic", new(320, instance.SelectionCount * 50 + 110)) { }
            public override void SelectionEvent()
            {
                instance.Dispose();
                InstanceCreate(new StatisticText());
                base.SelectionEvent();
            }
            private class StatisticText : Selector
            {
                public StatisticText()
                {
                    UpdateIn120 = true;
                    UserService.Statistic statistic = PlayerManager.CurrentUser.PlayerStatistic;
                    deathCount = statistic.DeathCount;
                    playtime = statistic.PlayedTime;
                    name = PlayerManager.CurrentUser.PlayerName;
                }
                float alpha = 0;
                int deathCount, appearTime = 0, playtime;
                string name;
                public override void Update()
                {
                    if (alpha < 1)
                        alpha += 0.025f;
                    base.Update();
                    appearTime++;
                    if (appearTime % 125 == 0) playtime++;
                }
                public override void Draw()
                {
                    var f = FightResources.Font.NormalFont;
                    f.CentreDraw("Statistic", new(320, 60), Color.White, 1.2f, 0);
                    DrawingLab.DrawRectangle(new CollideRect(100, 144, 440, 166), Color.White, 3, 1);
                    f.Draw("Username: " + name, new(115, 155), Color.White, 1.0f, 1);
                    f.Draw("Death Count: " + deathCount, new(115, 185), Color.White, 1.0f, 1);
                    string time = "Play Time: " + TimeSpan.FromSeconds(playtime).ToString();
                    f.Draw(time, new(115, 215), Color.White, 1.0f, 1);
                    base.Draw();
                }
            }
        }
        private class RatingList : TextSelection
        {
            public RatingList() : base("Rating list", new(320, instance.SelectionCount * 50 + 110))
            {
            }
            public override void SelectionEvent()
            {
                instance.Dispose();
                InstanceCreate(new ListText());
                base.SelectionEvent();
            }
            private class ListText : Selector
            {
                private class DataBox : Entity
                {
                    Color color;
                    CollideRect area;
                    RatingCalculater.RatingList.SingleSong data;

                    ListText father;

                    public DataBox(Color color, CollideRect area, RatingCalculater.RatingList.SingleSong data)
                    {
                        controlLayer = Surface.Hidden;
                        this.color = color;
                        this.area = area;
                        this.data = data;
                    }
                    public override void Start()
                    {
                        father = FatherObject as ListText;
                        base.Start();
                    }

                    public override void Draw()
                    {
                        CollideRect trueArea = area + (father == null ? Vector2.Zero : -father.delta);
                        DrawingLab.DrawRectangle(trueArea, color, 3.5f, 0.5f);
                        var f = FightResources.Font.NormalFont;
                        var f2 = FightResources.Font.FightFont;
                        string text = data.name.Length > 16 ? data.name[0..7] + "..." + data.name[(data.name.Length - 7)..] : data.name;
                        f.Draw(text, trueArea.TopLeft + new Vector2(9, 4), Color.White, 0.9f, 0.5f);
                        f.Draw(data.scoreResult.ToString("F2"), trueArea.TopLeft + new Vector2(200, 70), Color.LightBlue, 1.07f, 0.5f);
                        f.Draw(data.threshold.ToString("F1"), trueArea.TopLeft + new Vector2(9, 37), Color.Aqua, 0.9f, 0.5f);
                        f.Draw("* " + (data.transferAccuracy * 100).ToString("F2") + "%", trueArea.TopLeft + new Vector2(82, 39), Color.Wheat, 0.84f, 0.5f);
                        f.Draw("ACC = " + (data.accuracy * 100).ToString("F2") + "%", trueArea.TopLeft + new Vector2(9, 67), Color.White, 0.9f, 0.5f);

                        //Draw the difficulty Tag
                        switch (data.difficulty)
                        {
                            case Difficulty.Noob:
                                f2.Draw("NB", trueArea.TopRight + new Vector2(-28, 4), Color.White, 0.75f, 0.5f);
                                break;
                            case Difficulty.Easy:
                                f2.Draw("EZ", trueArea.TopRight + new Vector2(-28, 4), Color.Lime, 0.75f, 0.5f);
                                break;
                            case Difficulty.Normal:
                                f2.Draw("NM", trueArea.TopRight + new Vector2(-28, 4), Color.CornflowerBlue, 0.75f, 0.5f);
                                break;
                            case Difficulty.Hard:
                                f2.Draw("HD", trueArea.TopRight + new Vector2(-28, 4), Color.MediumPurple, 0.75f, 0.5f);
                                break;
                            case Difficulty.Extreme:
                                f2.Draw("EX", trueArea.TopRight + new Vector2(-28, 4), Color.Orange, 0.75f, 0.5f);
                                break;
                            case Difficulty.ExtremePlus:
                                f2.Draw("EX+", trueArea.TopRight + new Vector2(-38, 4), Color.Silver, 0.75f, 0.5f);
                                f2.Draw("EX+", trueArea.TopRight + new Vector2(-40, 4), Color.Red * 0.7f, 0.75f, 0.4f);
                                f2.Draw("EX+", trueArea.TopRight + new Vector2(-36, 4), Color.Lime * 0.7f, 0.75f, 0.4f);
                                f2.Draw("EX+", trueArea.TopRight + new Vector2(-38, 2), Color.MediumPurple * 0.7f, 0.75f, 0.4f);
                                f2.Draw("EX+", trueArea.TopRight + new Vector2(-38, 6), Color.LightSkyBlue * 0.7f, 0.75f, 0.4f);
                                break;
                        }
                    }

                    public override void Update()
                    {
                    }
                }

                RectangleBox box;
                Vector2 delta = Vector2.Zero;

                public ListText()
                {
                    box = new(new CollideRect(14, 70, 640 - 28, 480 - 150));
                    InstanceCreate(box);

                    UpdateIn120 = true;
                    list = PlayerManager.CurrentUser.GenerateList();

                    CollideRect[] positions = new CollideRect[10];
                    for (int i = 0; i < 10; i++)
                    {
                        int x = i % 2;
                        int y = i / 2;
                        positions[i].Width = 290;
                        positions[i].Height = 106;
                        positions[i].X = x * 320 + 15 - (x - 0.5f) * 16;
                        positions[i].Y = y * 120 + 77;
                    }
                    List<DataBox> dataBoxs = new()
                    {
                        new DataBox(Color.Lime, positions[0], list.CompleteDonor),
                        new DataBox(Color.Orange, positions[1], list.FCDonor),
                        new DataBox(Color.Gold, positions[2], list.APDonor)
                    };

                    for (int i = 0; i < 7; i++)
                    {
                        if (list.StrictDonors.Count == 0)
                            list.StrictDonors.Add(new("NULL", Difficulty.Noob, 0, 0, 0, 0));
                        dataBoxs.Add(new DataBox(Color.White, positions[i + 3], list.StrictDonors.Max));
                        list.StrictDonors.Remove(list.StrictDonors.Max);
                    }
                    dataBoxs.ForEach(s => { AddChild(s); });
                }
                RatingCalculater.RatingList list;
                float alpha = 0;
                int appearTime = 0;

                float speed = 0;
                public override void Update()
                {
                    if (alpha < 1)
                        alpha += 0.025f;
                    base.Update();
                    appearTime++;

                    bool downKeyPressed = IsKeyDown(InputIdentity.MainDown) || IsKeyDown(InputIdentity.SecondDown);
                    bool upKeyPressed = IsKeyDown(InputIdentity.MainUp) || IsKeyDown(InputIdentity.SecondUp);

                    if ((upKeyPressed && downKeyPressed) || (!upKeyPressed && !downKeyPressed))
                    {
                        if (speed > 0)
                            speed -= MathF.Min(speed, 0.3f);
                        else speed += MathF.Min(-speed, 0.3f);
                        speed *= 0.95f;
                    }
                    else if (upKeyPressed)
                    {
                        speed -= 0.4f;
                        speed = MathF.Max(-6f, speed);
                    }
                    else if (downKeyPressed)
                    {
                        speed += 0.4f;
                        speed = MathF.Min(6f, speed);
                    }
                    delta.Y += speed;
                    if (delta.Y < 0) { delta.Y = 0; speed = 0; }
                    float v = 270;
                    if (delta.Y > v) { delta.Y = v; speed = 0; }
                }
                public override void Draw()
                {
                    var f = FightResources.Font.NormalFont;
                    f.CentreDraw("Rating Data", new(320, 32), Color.White, 1.0f, 0);
                    base.Draw();
                }
                public override void Dispose()
                {
                    box.Dispose();
                    base.Dispose();
                }
            }
        }

        private class ShopMall : TextSelection
        {
            public ShopMall() : base("Shop", new Vector2(320, instance.SelectionCount * 50 + 110))
            { }
            public override void SelectionEvent()
            {
                instance.Dispose();
                InstanceCreate(new StoreFront());
                base.SelectionEvent();
            }
        }

        private class ChangePassword : TextSelection
        {
            public ChangePassword() : base("Change password", new Vector2(320, instance.SelectionCount * 50 + 110))
            { }
            public override void SelectionEvent()
            {
                instance.Dispose();
                InstanceCreate(new PassWordManager());
                base.SelectionEvent();
            }
            internal class PassWordManager : OKCancelSelector
            {
                private readonly TextInputer originPassword;
                private readonly TextInputer newPassword;
                private readonly TextInputer passwordAgain;
                public PassWordManager()
                {
                    originPassword = new TextInputer(new CollideRect(270, 160, 260, 38));
                    newPassword = new TextInputer(new CollideRect(270, 210, 260, 38));
                    passwordAgain = new TextInputer(new CollideRect(270, 260, 260, 38));
                    PushSelection(originPassword);
                    PushSelection(newPassword);
                    PushSelection(passwordAgain);

                    SelectChanger += () =>
                    {
                        if (WordsChanged) return;
                        if (IsKeyPressed120f(InputIdentity.MainUp))
                        {
                            currentSelect--;
                        }
                        else if (IsKeyPressed120f(InputIdentity.MainDown))
                        {
                            currentSelect++;
                        }
                        if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                        else if (currentSelect < 0) currentSelect = 0;
                    };
                    ResetSelect();
                    SelectChanged += () =>
                    {
                        PlaySound(changeSelection, 0.9f);
                    };

                    OKAction += () =>
                    {
                        string res = originPassword.Result;
                        if (MathUtil.StringHash(res) != PlayerManager.CurrentUser.Password || newPassword.Result != passwordAgain.Result || string.IsNullOrEmpty(newPassword.Result))
                        {
                            return false;
                        }

                        PlayerManager.CurrentUser.ResetPassword(MathUtil.StringHash(newPassword.Result));
                        PlayerManager.Save();

                        Back();
                        return true;
                    };
                }

                public override void Draw()
                {
                    NormalFont.CentreDraw("Change password", new Vector2(320, 80), Color.White);
                    NormalFont.Draw("Old password", new Vector2(50, 160), Color.White);
                    NormalFont.Draw("New password", new Vector2(50, 210), Color.White);
                    NormalFont.Draw("Input again", new Vector2(50, 260), Color.White);

                    base.Draw();
                }
            }
        }
    }
}