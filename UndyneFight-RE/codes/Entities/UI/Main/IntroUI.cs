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

namespace UndyneFight_Ex.Remake.UI
{
    public class IntroUI : SmartSelector
    {
        TextureButton mail, start, setting, account;
        static SmartMusicPlayer music;
        BackGenerater _backGenerater;

        public IntroUI()
        {
            if (music == null || !music.Onplay)
            {
                SmartMusicPlayer smartPlayer = new();
                smartPlayer.InsertPeriod(new MusicPlayer(Resources.Musics.DreamDiver_INTRO), 2407.5f, false);
                smartPlayer.InsertPeriod(new MusicPlayer(Resources.Musics.DreamDiver_LOOP), 4808.5f, true);
                GameStates.InstanceCreate(smartPlayer); smartPlayer.Play();
                music = smartPlayer;
            }

            GameStates.InstanceCreate(new InstantEvent(2, () => {
                var render = GameStates.CurrentScene.BackgroundRendering;
                GameStates.CurrentScene.CurrentDrawingSettings.backGroundColor = Color.White;
                render.InsertProduction(_backGenerater = new BackGenerater(0.6f));
            }));

            this.Activate();
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;

            mail = new(this, new(20, 20), Resources.UI.Mail);
            start = new(this, new(480, 430), Resources.UI.IntroStart); 
            setting = new(this, new(480, 500), Resources.UI.IntroSetting);
            account = new(this, new(480, 570), Resources.UI.IntroAccount);

            this.AddChild(mail);
            this.AddChild(start);
            this.AddChild(setting);
            this.AddChild(account);

            DefaultFocus = 1;

            this.OnSelected += IntroUI_OnSelected;
            this.KeyEvent = () => {  
                if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                {
                    int id = FocusID; 
                    if(id + 1 < all.Length)
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
        }

        private void IntroUI_OnSelected()
        {
            var selection = CurrentSelected;
            if(selection == start)
            {
                // Do start
                this.Dispose();
                GameStates.InstanceCreate(new SelectUI());
            }
            else if(selection == setting)
            {
                // into setting
                this.Dispose();
                GameStates.InstanceCreate(new SettingUI());
            }
            else if(selection == account)
            {
                //into account
                this.Dispose();
                if (PlayerManager.CurrentUser != null)
                    GameStates.InstanceCreate(new AccountManager());
                else
                    GameStates.InstanceCreate(new UserUI());
            }
        }

        public override void Start()
        {
            base.Start();

            this.AddChild(new MouseCursor());
            if (StartingShower.TitleShower != null)
            {
                Entity entity = Activator.CreateInstance(StartingShower.TitleShower) as Entity;
                this.AddChild(entity);
            }
        }

        public override void Draw()
        { 
        }

        public override void Update()
        { 
            base.Update();
        }
    }
}