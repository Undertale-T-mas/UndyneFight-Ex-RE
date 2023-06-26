using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex.ChampionShips;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.ChampionShips.ChampionShip;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal class ChampionShipCard : Entity, ISelectAble
    {
        public readonly ChampionShip championShip;
        private bool isSelected = false;
        private Vector2 startPosition;
        private readonly ChampionShipSelector selector;
        public ChampionShipCard(ChampionShip championShip, Vector2 Centre, ChampionShipSelector selector)
        {
            this.selector = selector;
            collidingBox = new CollideRect(0, 0, 560, 119);
            startPosition = Centre;
            this.championShip = championShip;
            try
            {
                Image = Scene.Loader.Load<Texture2D>(championShip.IconPath);
            }
            catch
            {
                Image = Sprites.championShip;
            }
        }

        private string state = "";
        private float size = 1.0f;
        private Color stateDrawingColor = Color.White;

        private float moveDelta = 0;

        public override void Update()
        {
            Centre = startPosition - new Vector2(ChampionShipSelector.XPositionDetla - moveDelta, 0);
            switch (championShip.CheckTime())
            {
                case ChampionShipStates.End:
                    state = "";
                    break;
                case ChampionShipStates.NotAvailable:
                    state = "Not Available";
                    size = 1.1f;
                    stateDrawingColor = Color.Red;
                    break;
                case ChampionShipStates.Starting:
                    state = "Ongoing!";
                    size = 1.3f;
                    stateDrawingColor = Color.Gold;
                    break;
            }
            moveDelta = MathF.Round(MathHelper.Lerp(moveDelta, isSelected ? 16 : 0, 0.18f), 1);
        }

        public override void Draw()
        {
            GameMain.MissionSpriteBatch.Draw(Image, new Rectangle((int)collidingBox.X + 5,
                (int)collidingBox.Y + 5, 100, 100), null, Color.White,
                0.0f, Vector2.Zero, SpriteEffects.None, 0.92f);
            collidingBox.Height -= 10;
            var BoxMiddle = collidingBox.Height / 2;
            DrawingLab.DrawLine(new(collidingBox.Left, collidingBox.Y + BoxMiddle),
                new(collidingBox.Right, collidingBox.Y + BoxMiddle), BoxMiddle * 2, Color.Black, 0.1f);
            DrawingLab.DrawRectangle(collidingBox, isSelected ? Color.Gold : Color.White, 4, 0.5f);
            collidingBox.Height += 10;

            NormalFont.CentreDraw(championShip.Title, Centre + new Vector2(30, -36), Color.White, 1f, 0.53f);
            NormalFont.CentreDraw(championShip.SubTitle, Centre + new Vector2(30, -4), Color.White, 0.8f, 0.53f);
            NormalFont.CentreDraw(championShip.Introduce, Centre + new Vector2(30, +24), Color.White, 0.65f, 0.53f);
            NormalFont.CentreDraw(state, Centre, stateDrawingColor, size, 0.54f);
        }

        public void DeSelected()
        {
            isSelected = false;
        }

        public void Selected()
        {
            isSelected = true;
        }

        public void SelectionEvent()
        {
#if !DEBUG
            if (this.championShip.CheckTime() != ChampionShip.ChampionShipStates.End)
            {
                if (string.IsNullOrEmpty(PlayerManager.currentPlayer))
                {
                    this.selector.Dispose();
                    GameStates.InstanceCreate(new LoginUI());
                    return;
                }
                else if (!PlayerManager.CurrentUser.InChampionShip(this.championShip.Title))
                {
                    this.selector.Dispose();
                    GameStates.InstanceCreate(new SignUpUI(this));
                    return;
                }
            }

            if (this.championShip.CheckTime() == ChampionShip.ChampionShipStates.NotAvailable)
            {
                return;
            }
#endif
            FightSystem.SelectSongSet(championShip);
            FightSystem.CurrentChampionShip = championShip;
            InstanceCreate(new CModeSelector());
            selector.Dispose();
        }
    }
    internal class ChampionShipSelector : Selector
    {
        private int pages = 0;
        private int curPage = 0;
        public static float XPositionDetla = 0;
        public static float XPositionDetlaMission = 0;

        public ChampionShipSelector() : base(true)
        {
            UpdateIn120 = true;
            AutoDispose = false;
            XPositionDetla = XPositionDetlaMission = 0;
            Vector2 Position0 = new Vector2(317, 127);
            int yCnt = 0;
            foreach (ChampionShip s in FightSystem.ChampionShips)
            {
#if !DEBUG
                if (s.CheckTime() == ChampionShip.ChampionShipStates.NotStart) continue;
#endif
                PushSelection(new ChampionShipCard(s, Position0, this));
                Position0.Y += 123;
                yCnt++;
                if (yCnt == 3)
                {
                    pages++;
                    yCnt = 0;
                    Position0.Y = 127;
                    Position0.X += 640;
                }
            }
            if (yCnt == 0) pages--;
            ResetSelect();
            PlaySound(select, 0.9f);
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainDown) && currentSelect != SelectionCount - 1)
                {
                    currentSelect++;
                    if (currentSelect % 3 == 0) currentSelect--;
                }
                if (IsKeyPressed120f(InputIdentity.MainUp) && currentSelect != 0)
                {
                    currentSelect--;
                    if (currentSelect % 3 == 2) currentSelect++;
                }
                if (IsKeyPressed120f(InputIdentity.MainLeft) && curPage != 0)
                {
                    curPage--;
                    currentSelect -= 3;
                    XPositionDetlaMission -= 640;
                }
                if (IsKeyPressed120f(InputIdentity.MainRight) && curPage != pages)
                {
                    curPage++;
                    currentSelect += 3;
                    if (currentSelect >= SelectionCount) currentSelect = SelectionCount - 1;
                    XPositionDetlaMission += 640;
                }
#if DEBUG
                if (IsKeyPressed120f(InputIdentity.Special))
                    LicenseMaker.GetScore((Selections[currentSelect] as ChampionShipCard).championShip);
#endif
            };
        }

        public override void Update()
        {
            XPositionDetla = MathHelper.Lerp(XPositionDetla, XPositionDetlaMission, 0.2f);
            base.Update();
        }

        public override void Draw()
        {
            System.DateTime time = System.DateTime.UtcNow;
            time = time.AddHours(8);
            NormalFont.CentreDraw("Championships", new Vector2(120, 34), Color.White);
            NormalFont.CentreDraw("UTC+8: " + time.ToShortTimeString(), new Vector2(520, 34), Color.White, 0.95f, 0.5f);
            NormalFont.CentreDraw($"Page: {curPage + 1} / {pages + 1}", new Vector2(320, 462), Color.White, 0.95f, 0.5f);
            base.Draw();
        }
    }

    internal class CModeSelector : Selector
    {
        private float alpha = 0;

        public CModeSelector()
        {
            selectedMode = last;
            ResetSelect();
            PlaySound(select, 0.9f);
            isRecord = true;
            isReplay = false;
            SelectChanger += () =>
            {
                if (IsKeyPressed120f(InputIdentity.MainUp) || IsKeyPressed120f(InputIdentity.MainLeft))
                    currentSelect--;
                else if (IsKeyPressed120f(InputIdentity.MainDown) || IsKeyPressed120f(InputIdentity.MainRight))
                    currentSelect++;
                if (currentSelect >= SelectionCount) currentSelect -= SelectionCount;
                else if (currentSelect < 0) currentSelect = SelectionCount - 1;
            };
            SelectChanged += () =>
            {
                PlaySound(changeSelection, 0.9f);
            };
            Selected += () =>
            {
                PlaySound(select, 0.9f);
            };
            PushSelection(new SelectEnd(this));
            PushSelection(new Buffed(this));
            PushSelection(new NoHitter(this));
            PushSelection(new APer(this));


            bool InChampionShip = FightSystem.CurrentChampionShip != null
                && FightSystem.CurrentChampionShip.CheckTime() == ChampionShipStates.Starting;
            if (!InChampionShip)
            {
                PushSelection(new Practice(this));
                PushSelection(new NoGreenSoul(this));
                PushSelection(new Autoplay(this));
            }
            AutoDispose = false;
        }

        public override void Dispose()
        {
            last = selectedMode;
            base.Dispose();
        }

        private static GameMode last;
        private GameMode selectedMode;

        public class SelectEnd : TextSelection
        {
            private readonly CModeSelector selector;
            public SelectEnd(CModeSelector selector) : base("Okay!", new Vector2(320, 160 - 12 - 25))
            { this.selector = selector; Size = 1.02f; }
            public override void SelectionEvent()
            {
                InstanceCreate(new SongSelector(selector.selectedMode));
                selector.Dispose();
                base.SelectionEvent();
            }
        }
        public class Buffed : TextSelection
        {
            private readonly CModeSelector selector;
            public Buffed(CModeSelector selector) : base("Buffed", new Vector2(320, 230 - 12))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.Buffed) != 0 ? Color.Red : Color.Gray;
            }
            public override void SelectionEvent()
            {
                selector.selectedMode ^= GameMode.Buffed;
                TextColor = (selector.selectedMode & GameMode.Buffed) != 0 ? Color.Red : Color.Gray;
                base.SelectionEvent();
            }
        }
        public class NoHitter : TextSelection
        {
            private readonly CModeSelector selector;
            public NoHitter(CModeSelector selector) : base("No Hit", new Vector2(320, 275 - 12))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.NoHit) != 0 ? Color.Yellow : Color.Gray;
            }
            bool Interrupted => (selector.selectedMode & GameMode.Practice) != 0;
            public override void SelectionEvent()
            {
                if (!Interrupted)
                {
                    selector.selectedMode ^= GameMode.NoHit;
                    TextColor = (selector.selectedMode & GameMode.NoHit) != 0 ? Color.Yellow : Color.Gray;
                }
                base.SelectionEvent();
            }
            public override void Update()
            {
                if (Interrupted) TextColor = Color.DarkRed;
                if (!Interrupted && TextColor == Color.DarkRed) TextColor = Color.Gray;
                base.Update();
            }
        }
        public class APer : TextSelection
        {
            private readonly CModeSelector selector;
            public APer(CModeSelector selector) : base("Perfect Only", new Vector2(320, 320 - 12))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.PerfectOnly) != 0
                    ? Color.Yellow : Color.Gray;
            }
            public override void SelectionEvent()
            {
                selector.selectedMode ^= GameMode.PerfectOnly;
                TextColor = (selector.selectedMode & GameMode.PerfectOnly) != 0 ? Color.Yellow : Color.Gray;
                base.SelectionEvent();
            }
        }
        public class Practice : TextSelection
        {
            private readonly CModeSelector selector;
            public Practice(CModeSelector selector) : base("Practice", new Vector2(320, 365 - 12))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.Practice) != 0 ? Color.Lime : Color.Gray;
            }
            bool Interrupted => (selector.selectedMode & GameMode.NoHit) != 0;
            public override void SelectionEvent()
            {
                if (!Interrupted)
                {
                    selector.selectedMode ^= GameMode.Practice;
                    TextColor = (selector.selectedMode & GameMode.Practice) != 0 ? Color.Lime : Color.Gray;
                }
                base.SelectionEvent();
            }
            public override void Update()
            {
                if (Interrupted) TextColor = Color.DarkRed;
                if (!Interrupted && TextColor == Color.DarkRed) TextColor = Color.Gray;
                base.Update();
            }
        }
        public class NoGreenSoul : TextSelection
        {
            private readonly CModeSelector selector;
            public NoGreenSoul(CModeSelector selector) : base("No green soul", new Vector2(320, 410 - 12))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.NoGreenSoul) != 0 ? Color.Beige : Color.Gray;
            }
            public override void SelectionEvent()
            {
                selector.selectedMode ^= GameMode.NoGreenSoul;
                TextColor = (selector.selectedMode & GameMode.NoGreenSoul) != 0 ? Color.Beige : Color.Gray;
                base.SelectionEvent();
            }
        }
        public class Autoplay : TextSelection
        {
            private readonly CModeSelector selector;
            public Autoplay(CModeSelector selector) : base("Autoplay", new Vector2(320, 455 - 12))
            {
                this.selector = selector;
                TextColor = (this.selector.selectedMode & GameMode.Autoplay) != 0 ? Color.Gold : Color.Gray;
            }
            public override void SelectionEvent()
            {
                selector.selectedMode ^= GameMode.Autoplay;
                TextColor = (selector.selectedMode & GameMode.Autoplay) != 0 ? Color.Gold : Color.Gray;
                base.SelectionEvent();
            }
        }

        public override void Update()
        {
            if (alpha < 1)
                alpha += 0.025f;
            base.Update();
        }

        public override void Draw()
        {
            NormalFont.CentreDraw("Select your gamemode", new Vector2(320, 45), Color.White * alpha);
            //sGlobalResources.Font.NormalFont.CentreDraw("Extra Settings", new Vector2(320, 175), Color.White * alpha);
            base.Draw();
        }
    }
}