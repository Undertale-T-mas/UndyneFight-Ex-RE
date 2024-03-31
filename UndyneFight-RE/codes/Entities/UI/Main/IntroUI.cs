using System;
using System.Collections.Generic;

using UndyneFight_Ex.Entities;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Remake.Components;
using UndyneFight_Ex.Remake.Effects;
using UndyneFight_Ex.UserService;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using Microsoft.Xna.Framework.Input;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.Remake.Resources;
using static UndyneFight_Ex.Remake.Resources.UI;
using static UndyneFight_Ex.FightResources.Sprites;
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Remake.UI.DEBUG
{
    public class IntroUI : SmartSelector
    {
        TextureButton mail, start, setting, account;
        Button contributor, showPage;

        MouseCursor cursor;

        class ExhibitButton : SelectingModule
        {
            public ExhibitButton(ISelectChunk father) : base(father)
            {
                this.Image = StoreData.AllItems["2023MEMTroBrz"].Image;
            }
            public override void Start()
            {
                this.LeftClick += ExhibitButton_LeftClick;
            }

            private void ExhibitButton_LeftClick()
            {

            }

            public override void Draw()
            {
                if (this.Image != null)
                {
                    this.Depth = 0.1f;
                    this.FormalDraw(this.Image, this.collidingBox, col.White * 0.8f);
                    this.FormalDraw(StoreData.AllItems["2023MEMTroSil"].Image, this.collidingBox + new vec2(140, 0), col.White * 0.8f);
                    /*        
                    CollideRect rect = this.collidingBox;
                    rect.X -= 20; rect.Y -= 20; rect.Height += 40; rect.Width += 40;
                    DrawingLab.DrawRectangle(rect, col.White, 3.0f, 0.1f);
                    this.Depth = 0.01f;
                    this.FormalDraw(FightResources.Sprites.pixiv, rect, col.Black * 0.38f);
                    */
                }
                else
                {
                    this.FormalDraw(pixUnit, this.collidingBox, col.White * 0.8f);
                }

            }
            float scale = 1.0f;
            public override void Update()
            {
                scale = MathHelper.Lerp(scale, _mouseOn ? 1.1f : 1.0f, 0.15f);
                this.collidingBox.Size = new vec2(125, 125) * scale;
                this.Centre = new(110, 616);
                base.Update();
            }
        }
        ExhibitButton exhibit;

        static SmartMusicPlayer music;
        BackGenerator _backGenerater;

        private static TipUI _tipUI = null;
        public static void PendingTip(TipUI tipUI)
        {
            _tipUI = tipUI;
        }
        public IntroUI()
        {
            if (!BSet.problem)
            {
                if (music == null || !music.Onplay)
                {
                    SmartMusicPlayer smartPlayer = new();
                    smartPlayer.InsertPeriod(new MusicPlayer(Musics.DreamDiver_INTRO), 2407.5f, false);
                    smartPlayer.InsertPeriod(new MusicPlayer(Musics.DreamDiver_LOOP), 4808.5f, true);
                    InstanceCreate(smartPlayer); smartPlayer.Play();
                    music = smartPlayer;
                }
            }
            InstanceCreate(new InstantEvent(2, () =>
            {
                var render = GameStates.CurrentScene.BackgroundRendering;
                GameStates.CurrentScene.CurrentDrawingSettings.backGroundColor = Color.White;
                render.InsertProduction(_backGenerater = new BackGenerator(0.6f));
                if (BSet.problem)
                {
                    Functions.ScreenDrawing.ActivateShader(FightResources.Shaders.Gray);
                    FightResources.Shaders.Gray.Intensity = 1;
                    Functions.ScreenDrawing.Shaders.Glitching g = new(0.963f);
                    GameStates.CurrentScene.SceneRendering.InsertProduction(g);
                    g.Duration = new vec2(3);
                    g.AverageInterval = 0;
                    g.AverageDelta = 1.2f;
                    g.BlockScale = 1f;
                    g.Intensity = 50;
                }

            }));

            this.Activate();
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;

            float x = 490;

            mail = new(this, new(20, 20), Mail);
            start = new(this, new(x, 430), IntroStart);
            setting = new(this, new(x, 500), IntroSetting);
            account = new(this, new(x, 570), IntroAccount);
            showPage = new(this, new(960 - 115, 640), "show page") { DefaultScale = 1.15f };
            contributor = new(this, new(960 - 115, 690), "Contributors") { DefaultScale = 1.15f };
            start.DefaultScale = 1.5f;
            setting.DefaultScale = 1.5f;
            account.DefaultScale = 1.5f;
            SelectUI.Initialize();
            if (BSet.problem)
            {
                mail = new(this, new(-200, -200), Mail);
                AddChild(mail);
                start = new(this, new(x, 430), specialStart);
                AddChild(start);
            }
            else
            {
                AddChild(mail);
                this.AddChild(start);
            }
            if (!BSet.problem)
            {
                this.AddChild(setting);
                this.AddChild(account);
                //this.AddChild(showPage);
                this.AddChild(contributor);
            }


            DefaultFocus = 1;

            this.OnSelected += IntroUI_OnSelected;
            this.KeyEvent = () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainDown))
                {
                    int id = FocusID;
                    if (id + 1 < all.Length)
                    {
                        currentFocus.OffFocus();
                        all[id + 1].OnFocus();
                    }
                }
                else if (IsKeyPressed120f(InputIdentity.MainUp))
                {
                    int id = FocusID;
                    if (id > 1)
                    {
                        currentFocus.OffFocus();
                        all[id - 1].OnFocus();
                    }
                }
                if (IsKeyPressed120f(InputIdentity.Confirm))
                {
                    currentFocus?.ConfirmKeyDown();
                }
            };
        }

        private void IntroUI_OnSelected()
        {
            var selection = CurrentSelected;
            if (selection == start)
            {
                // Do start
                this.Dispose();
                InstanceCreate(new SelectUI());
            }
            else if (selection == setting)
            {
                // into setting
                this.Dispose();
                InstanceCreate(new SettingUI());
            }
            else if (selection == account)
            {
                //into account
                this.Dispose();
                InstanceCreate(PlayerManager.CurrentUser == null ? new UserUI() : new AccountManager());
            }
            else if (selection == contributor)
            {
                this.Dispose();
                InstanceCreate(new ContributerUI());
            }
            else if (selection == showPage)
            {
                this.Dispose();
                InstanceCreate(new ItemShowUI());
            }
        }

        public override void Start()
        {
            base.Start();
            cursor = new MouseCursor();
            if (StartingShower.TitleShower != null)
            {
                titleShower = Activator.CreateInstance(StartingShower.TitleShower) as Entity;
            }
        }
        Entity titleShower;

        public override void Draw()
        {
            if (topUI != null)
            {
                IEnumerable<Entity> all = topUI.GetDrawableTree();
                foreach (Entity entity in all) { entity.Draw(); }
            }
            if (titleShower != null)
            {
                IEnumerable<Entity> all = titleShower.GetDrawableTree();
                foreach (Entity entity in all) { entity.Draw(); }
            }
            cursor.Draw();
            if(!BSet.problem)
            FightResources.Font.NormalFont.Draw("V. 2024 April Fools", new vec2(10, 680), col.White, 1, 1);
        }

        public override void Update()
        {
            if (_tipUI != null)
            {
                if (_tipUI.Disposed) _tipUI = null;
                if (topUI == null)
                {
                    this.topUI = _tipUI;
                }
            }
            if (topUI != null)
            {
                this.UpdateChildren = false;
                topUI.TreeUpdate();
                if (topUI.Disposed) topUI = null;
            }
            if (topUI == null)
            {
                this.UpdateChildren = true;
                base.Update();
            }
            titleShower?.TreeUpdate();
            cursor.Update();

#if DEBUG
            if (IsKeyDown(Keys.LeftControl) && IsKeyPressed120f(Keys.D))
            {
                // DEBUG INTRO
                this.Dispose();
                InstanceCreate(new DebugWindow());
            }
#endif
        }
        Entity topUI;
    }
}