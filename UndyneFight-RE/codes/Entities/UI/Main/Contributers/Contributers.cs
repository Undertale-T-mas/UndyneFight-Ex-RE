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
using Microsoft.Win32;
using System.Diagnostics;

namespace UndyneFight_Ex.Remake.UI
{
    public class ContributerUI : SmartSelector
    {
        Button back;
        public ContributerUI()
        {
            this.Activate();

            this.AddChild(new Button(this, new(335, 141), "OTokP") { NeverEnable = true, Extras = 26162719 });
            this.AddChild(new Button(this, new(495, 141), "ZF_75_") { NeverEnable = true, Extras = 2103619364 });
            this.AddChild(new Button(this, new(405, 221), "IceWind") { NeverEnable = true, Extras = 502576976 });
            this.AddChild(new Button(this, new(585, 221), "Sshan") { NeverEnable = true, Extras = 34215247 });
            this.AddChild(new Button(this, new(455, 301), "Tlottgodinf_") { NeverEnable = true, Extras = 383883725 });
            this.AddChild(new Button(this, new(715, 301), "zKronO") { NeverEnable = true, Extras = 631626325 });
            this.AddChild(new Button(this, new(392, 381), "Walar") { NeverEnable = true, Extras = 32046609 });
            this.AddChild(new Button(this, new(500, 460), "T-mas") { NeverEnable = true, Extras = 398385691 });
            this.AddChild(back = new Button(this, new(100, 686), "<< Back") { NeverEnable = true, DefaultScale = 1.3f });

            back.LeftClick += Back;

            this.KeyEvent = () => {
                if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                {
                    int id = FocusID;
                    if (id + 1 < all.Length)
                    {
                        currentFocus.OffFocus();
                        all[id + 1].OnFocus();
                    }
                }
                else if (GameStates.IsKeyPressed120f(InputIdentity.MainUp))
                {
                    int id = FocusID;
                    if (id > 1)
                    {
                        currentFocus.OffFocus();
                        all[id - 1].OnFocus();
                    }
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                {
                    currentFocus?.ConfirmKeyDown();
                }
            };

            this.OnSelected += ContributerUI_OnSelected;
        }

        private void ContributerUI_OnSelected()
        {
            var v = CurrentSelected.Extras;
            if (v != null) StartPage(v.ToString());
        }

        private void StartPage(string id)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string s = key.GetValue("").ToString();

            string target = "https://space.bilibili.com/" + id; 
         //   System.Diagnostics.Process.Start(s.Substring(0, s.Length - 8), target);
            System.Diagnostics.Process.Start("explorer.exe", target);
        }

        public override void Start()
        {
            base.Start();
            this.AddChild(new MouseCursor());
        }

        public override void Update()
        {
            base.Update();

            if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
            {
                Back();
            }
        }

        private void Back()
        {
            this.Dispose();
            GameStates.InstanceCreate(new DEBUG.IntroUI());
        }

        public override void Draw()
        {
            var font = FightResources.Font.NormalFont;
            font.CentreDraw("Rhythm Recall Arcade Team", new(480, 56), Color.White, 1.5f, 0.1f);
            for (int i = 0; i < TextList.Length; i++)
                font.Draw(TextList[i], new(80, 116 + 80 * i), Color.White, 1.4f, 0.1f);
        }
        string[] TextList = { "Art by:", "Music by:", "Chart by:", "Effect by:", "UF-Ex Engine by:"};
    }
}