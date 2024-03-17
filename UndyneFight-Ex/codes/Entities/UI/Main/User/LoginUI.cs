using Microsoft.Xna.Framework;
using System;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal class LoginUI : Selector
    {
        internal class PasswordInputUI : OKCancelSelector
        {
            string playerName;
            TextInputer inputer1;
            public PasswordInputUI(string playerName)
            {
                this.playerName = playerName;
                SelectChanger += () =>
                {
                    if (WordsChanged) return;
                    if (IsKeyPressed120f(InputIdentity.MainUp))
                        currentSelect--;
                    else if (IsKeyPressed120f(InputIdentity.MainDown))
                        currentSelect++;
                    if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                    else if (currentSelect < 0) currentSelect = 0;
                };
                Selected += () =>
                {
                    if (currentSelect != 0)
                        PlaySound(select);
                };
                ResetSelect();
                SelectChanged += () =>
                {
                    PlaySound(changeSelection, 0.9f);
                };
                OKAction += () =>
                {
                    string text = inputer1.Result;
                    string origin = PlayerManager.playerInfos[playerName].Password.ToString();
                    if (origin == MathUtil.StringHash(text).ToString())
                    {
                        PlayerManager.Login(playerName);
                        ResetScene(new GameMenuScene());
                        return true;
                    }
                    return false;
                };
                PushSelection(inputer1 = new TextInputer(new CollideRect(270, 230, 260, 38)));
            }

            public override void Draw()
            {
                NormalFont.CentreDraw("Input password", new Vector2(320, 50), Color.White);
                NormalFont.CentreDraw($"Account:[{playerName}]", new Vector2(320, 180), Color.White);
                NormalFont.Draw("Password:", new Vector2(120, 230), Color.White);
                base.Draw();
            }
        }
        private float alpha = 0;

        public LoginUI() : base(true)
        {
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp))
                    currentSelect -= 2;
                else if (IsKeyPressed120f(InputIdentity.MainDown))
                    currentSelect += 2;
                if (IsKeyPressed120f(InputIdentity.MainRight))
                    currentSelect++;
                else if (IsKeyPressed120f(InputIdentity.MainLeft))
                    currentSelect--;
                if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                else if (currentSelect < 0) currentSelect = 0;
            };
            ResetSelect();
            PlaySound(select, 0.9f);
            Selected += () => { PlaySound(select); };
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };

            int y = 0, x = 0;
            foreach (var info in PlayerManager.playerInfos.Values)
            {
                TextSelection selection = new(info.PlayerName, new Vector2(170 + (300 * x), 100 + (50 * y)))
                {
                    TextColor = info.VIP ? Color.Gold : Color.White,
                    SetSelectionAction = () =>
                    {
                        InstanceCreate(new PasswordInputUI(info.PlayerName));
                    }
                };

                x++;
                if (x == 2)
                {
                    x = 0;
                    y++;
                }

                PushSelection(selection);
            }

            PushSelection(new TextSelection("Register", new Vector2(320, 170 + (50 * y)))
            {
                Size = 1.0f,
                TextColor = Color.MediumPurple,
                SetSelectionAction = () => { InstanceCreate(new RegisterUI()); }
            });
        }

        public override void Update()
        {
            if (IsKeyPressed(InputIdentity.Cancel))
            {
                Back();
                Dispose();
            }
            if (alpha < 1)
                alpha += 0.025f;
            base.Update();
        }

        public override void Draw()
        {
            NormalFont.CentreDraw("Select your account", new Vector2(320, 50), Color.White);
            base.Draw(); ;
        }
    }

    internal class TextInputer : Entity, ISelectAble
    {
        private bool isSelected = false;

        public void Selected()
        {
            isSelected = true;
        }
        public void DeSelected()
        {
            isSelected = false;
        }

        public TextInputer(CollideRect area)
        {
            collidingBox = area;
            UpdateIn120 = true;
        }

        public string Result => currentString;

        public override void Draw()
        {
            if (isSelected && appearTime % 66 <= 32)
            {
                FormalDraw(GlobalResources.Sprites.cursor, new Vector2(collidingBox.TopLeft.X + 1 +
                    NormalFont.SFX.MeasureString(currentString[..cursorPlace]).X, collidingBox.TopLeft.Y + 4), Color.White, 1.2f, 0.0f, Vector2.Zero);
            }
            if (currentString != null)
                NormalFont.Draw(currentString, collidingBox.TopLeft + new Vector2(3, 2), Color.LightCoral);
            DrawingLab.DrawRectangle(collidingBox, Color.White, 4f, 0.6f);
        }

        private int cursorPlace = 0, appearTime = 0;
        private string currentString = "";

        public void InputChar(char input)
        {
            if (input == (char)13)
            {
                return;
            }
            appearTime = 0;

            string appro = currentString + input;
            float v = NormalFont.SFX.MeasureString(appro).X + 10;
            if (v > CollidingBox.Size.X) return;

            currentString = currentString.Length == 0
                ? input.ToString()
                : currentString[..cursorPlace] + input +
                (currentString.Length > cursorPlace ?
                currentString[cursorPlace..] : "");

            cursorPlace++;
        }

        public override void Update()
        {
            appearTime++;
            if (isSelected)
            {
                if (WordsChanged && appearTime > 1)
                {
                    InputChar(CharInput);
                }
                if (WordsChanged) return;
                if (IsKeyPressed120f(InputIdentity.Backspace))
                {
                    appearTime = 0;
                    if (cursorPlace != 0)
                    {
                        currentString = string.Concat(currentString.AsSpan(0, cursorPlace - 1),
                                        cursorPlace <= currentString.Length ? currentString[cursorPlace..] : "");
                        cursorPlace--;
                    }
                }
                if (IsKeyPressed120f(InputIdentity.MainLeft))
                {
                    if (cursorPlace > 0)
                        cursorPlace--;
                    appearTime = 0;
                }
                if (IsKeyPressed120f(InputIdentity.MainRight))
                {
                    if (cursorPlace < currentString.Length)
                        cursorPlace++;
                    appearTime = 0;
                }
            }
        }

        public void SelectionEvent()
        {
            RegisterUI.ChangeSelection();
        }

        public void SetString(string mission)
        {
            cursorPlace = mission.Length;
            currentString = mission;
        }
    }

    internal class RegisterUI : Entity
    {
        private static RegisterUI instance;
        private static bool needGetDown = false;
        public static void ChangeSelection()
        {
            needGetDown = true;
        }

        public RegisterUI()
        {
            instance = this;
            controls[0] = new TextInputer(new CollideRect(320, 150, 260, 38));
            controls[0].Selected();
            controls[1] = new TextInputer(new CollideRect(320, 200, 260, 38));
            controls[2] = new TextInputer(new CollideRect(320, 250, 260, 38));
            controls[3] = new OKButton();
            controls[4] = new CancelButton();
            UpdateIn120 = true;
        }

        private static readonly ISelectAble[] controls = new ISelectAble[5];

        public override void Draw()
        {
            foreach (var v in controls)
            {
                (v as Entity).Draw();
            }
            NormalFont.CentreDraw("Register", new Vector2(320, 70), Color.White);
            NormalFont.Draw("Your Name", new Vector2(80, 150), Color.White);

            NormalFont.Draw("Your password", new Vector2(80, 200), Color.White);
            NormalFont.Draw("Password again", new Vector2(80, 250), Color.White);
        }

        private int currentSelect;

        public override void Update()
        {
            int last = currentSelect;

            if (WordsChanged) goto A;
            if (IsKeyPressed120f(InputIdentity.MainDown) || needGetDown)
            {
                needGetDown = false;
                currentSelect++;
            }
            else if (IsKeyPressed120f(InputIdentity.MainUp))
            {
                currentSelect--;
            }
            if (currentSelect >= controls.Length) currentSelect = controls.Length - 1;
            else if (currentSelect < 0) currentSelect = 0;

            A: if (last != currentSelect)
            {
                controls[last].DeSelected();
                controls[currentSelect].Selected();
                PlaySound(changeSelection, 0.9f);
            }

            foreach (var v in controls)
            {
                (v as Entity).TreeUpdate();
            }

            if (currentSelect > 2 && IsKeyPressed(InputIdentity.Confirm))
            {
                controls[currentSelect].SelectionEvent();
            }
        }

        public class OKButton : TextSelection
        {
            public OKButton() : base("Confirm", new Vector2(320, 329))
            {
                Size = 1.0f;
            }
            public override void SelectionEvent()
            {
                string name = (controls[0] as TextInputer).Result;
                string EmptyPasswordText = "Input Password";
                string EmptyPasswordText2 = "Password again!";
                string IncorrectPasswordText = "Incorrect";

                if (name == "")
                {
                    return;
                }

                string key1 = (controls[1] as TextInputer).Result;
                string key2 = (controls[1] as TextInputer).Result;

                if (string.IsNullOrEmpty(key1))
                {
                    (controls[1] as TextInputer).SetString(EmptyPasswordText);
                }
                else if (string.IsNullOrEmpty(key2))
                {
                    (controls[2] as TextInputer).SetString(EmptyPasswordText2);
                }
                else
                {
                    if (key1 == key2 && key1 != EmptyPasswordText
                        && key2 != IncorrectPasswordText && key2 != EmptyPasswordText2)
                    {
                        Register(name, key1);
                    }
                    else
                    {
                        (controls[2] as TextInputer).SetString(IncorrectPasswordText);
                    }
                }
                base.SelectionEvent();
            }

            private static void Register(string str, string password)
            {
                instance.Dispose();
                PlayerManager.AddNewUser(str, password);
                ResetScene(new GameMenuScene());
            }
        }

        private class CancelButton : TextSelection
        {
            public CancelButton() : base("Cancel", new Vector2(320, 369))
            {
                Size = 1.0f;
            }
            public override void SelectionEvent()
            {
                instance.Dispose();
                InstanceCreate(new LoginUI());
                base.SelectionEvent();
            }
        }
    }
}