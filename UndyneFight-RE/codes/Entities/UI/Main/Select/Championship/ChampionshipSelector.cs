using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Entities;
using col = Microsoft.Xna.Framework.Color;
using System.Collections.Generic;
using UndyneFight_Ex.ChampionShips;
using UndyneFight_Ex.SongSystem;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Remake.Network;
using System.Text.Json;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.PlayerManager;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class ChampionshipSelector : SmartSelector
    {
        private class ParticleGenerater : GameObject
        {
            public override void Update()
            {
                if(Rand(0, 1f) < 0.765f)
                {
                    float speed = Rand(1.0f, 1.6f);
                    this.AddChild(new Particle(col.White, new(0, -speed * 1.15f * 4f), Rand(16f, 24f) / MathF.Pow(speed, 1.42f) * 1.315f, new(Rand(0, 960), 730), Sprites.square)
                    {
                        DarkingSpeed = 4,
                        AutoRotate = true
                    });
                }
            }
        }
        private void KeyAction()
        { 
            if (IsKeyPressed120f(InputIdentity.MainDown) || IsKeyPressed120f(InputIdentity.MainRight))
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
            else if (IsKeyPressed120f(InputIdentity.MainUp) || IsKeyPressed120f(InputIdentity.MainLeft))
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
            if (IsKeyPressed120f(InputIdentity.Confirm))
            {
                currentFocus?.ConfirmKeyDown();
            }
        }
        Button[] _songButtons;
        public ChampionshipSelector() {
            this.KeyEvent = KeyAction;
            UpdateIn120 = true;
            this.Activate();
            this.OnSelected += ChampionshipSelector_OnSelected;
        }

        private void ChampionshipSelector_OnSelected()
        {
            this.FocusOn(CurrentSelected);
            if(this.FocusID <= 2 && _songButtons != null)
            {
                // clicking a song button
                this.Dispose();
                bool state = false;
#if DEBUG
                state = true;
#endif
                if (currentChampionship.CheckTime() == ChampionShip.ChampionShipStates.Starting || state) {
                    // get into the song

                    this.Broadcast("MusicFadeOut");
                    InstanceCreate(new InstantEvent(0, () => {
                        var extra = CurrentSelected.Extras as SongFightingScene.SceneParams;
                        StartSong(extra);
                        difficulty = extra.difficulty;
                    }));
                }
                else
                {
                    // get back
                    this.Dispose();
                    InstanceCreate(new SelectUI());
                }
            }
            else
            {
                if (CurrentSelected.Extras is int && (int)CurrentSelected.Extras == -1)
                {
                    // clicking a scoreboard page button
                }
                else
                {
                    // clicking a division button
                    this.Dispose();
                    InstanceCreate(new SelectUI());
                }
            }
        }

        ChampionShip currentChampionship;

        public override void Start()
        {
            CheckTime();
            ChampionshipInfoShower cis;
            ChampionShip c;
            this.AddChild(cis = new ChampionshipInfoShower());
            currentChampionship = c = cis.Championship;
            this.AddChild(scoreboard= new Scoreboard(CurrentUser?.ChampionshipData?.ChampionshipDivision(_currentChampionshipNAME)));
            this.AddChild(new MouseCursor());
            DivisionSelector ds = new(cis.Info);
            this.AddChild(ds);

            if (c == null) goto A;

            // push song selections
            MakeSongSelection(cis, c);

            // push division selections
            MakeDivisionButton(c, ds);

            // push scoreboard page selections
            MakePageButton();

        A:
            this.AddChild(new ParticleGenerater());
            base.Start();
        }

        Scoreboard scoreboard;

        private void MakePageButton()
        {
            Button left, right;
            this.AddChild(left = new Button(this, new(96, 555), "<"));
            this.AddChild(right = new Button(this, new(967 - 96, 555), ">"));
            left.LeftClick += () => scoreboard.PageLeft();
            right.LeftClick += () => scoreboard.PageRight();
            left.Extras = -1;
            right.Extras = -1;
            left.NeverEnable = true; right.NeverEnable = true;
        }

        bool timeChecked = false;
        private void CheckTime()
        {
            long start = DateTime.UtcNow.Ticks;
            UFSocket<Empty> OnlineCheck = new((t) => {
                timeChecked = true;
                if (!t.Success)
                {
                    this.Dispose();
                    InstanceCreate(new InfoText("Check the connection!", new(480, 400)) { DrawingColor = col.Red });
                    InstanceCreate(new SelectUI());
                    return;
                }
                long result = Convert.ToInt64(t.Info) - t.DelayTick;
                long delta = start - result;
                if (Math.Abs(delta) < 5000000000l)
                {
                    // acceptable time delta (500s)
                }
                else {
                    // unacceptable time delta

                    this.Dispose();
                    InstanceCreate(new SelectUI());

                    InstanceCreate(new InfoText("Time delta too large!", new(480, 400)) { DrawingColor = col.Red }); ;
                }
            });
            OnlineCheck.SendRequest("Time\\none");
        }

        private void MakeSongSelection(ChampionshipInfoShower cis, ChampionShip c)
        {
            if (CurrentUser != null)
            {
                if (CurrentUser.ChampionshipData.InChampionship(c.Title))
                {
                    string divName = CurrentUser.ChampionshipData.ChampionshipDivision(c.Title);
                    int length = c.Fights.Values.Length;
                    int pages = (length - 1) / 3 + 1;
                    Button[] buttons = new Button[length];
                    for (int i = 0; i < length; i++)
                    {
                        IChampionShip song = Activator.CreateInstance(c.Fights.Values[i]) as IChampionShip;
                        IWaveSet waveset = song.GameContent;
                        string text = waveset.FightName;
                        bool hidden = waveset.Attributes.Hidden;
                        if (hidden) text = "? ? ? ? ?";

                        int yi = i % 3;

                        buttons[i] = new Button(this, new(325, yi * 50.5f + 153.5f), text);
                        buttons[i].CentreDraw = false;
                        buttons[i].DefaultScale = 1.263f;

                        string path = "Musics\\" + waveset.Music + "\\paint";
                        Texture2D illustration = null;
                        if (System.IO.File.Exists("Content\\" + path + ".xnb"))
                            illustration = Loader.Load<Texture2D>(path);
                        buttons[i].Extras = new SongFightingScene.SceneParams(waveset, illustration, (int)song.DifficultyPanel[divName], $"Content\\Musics\\{waveset.Music}\\song", JudgementState.Strict);

                        if (hidden) buttons[i].State = SelectState.Disabled;
                    }
                    for (int i = 0; i < Math.Min(length, 3); i++)
                    {
                        this.AddChild(buttons[i]);
                        int t = i;
                        this.AddChild(new TimeRangedEvent(99999f, () =>
                        {
                            buttons[t].PositionDelta = cis.Delta;
                        }));
                    }
                    _songButtons = buttons;
                }

            }
        }

        private void MakeDivisionButton(ChampionShip c, DivisionSelector ds)
        {
            string[] divisions = ds.GetDivisions(c);
            List<Button> buttons = new();
            for (int i = 0; i < divisions.Length; i++)
            {
                // get the lerp position
                float lerpPos = i * 1.0f / (divisions.Length - 1);
                float posX = MathHelper.Lerp(550f, 770f, lerpPos);

                Button bt;
                this.AddChild(bt = new Button(this, new(posX, 361), divisions[i]));
                buttons.Add(bt);
                string text = divisions[i];
                bt.LeftClick += () =>
                {
                    if (CurrentUser == null) return;

                    UFSocket<Empty> socket = null;
                    socket = new(t => {
                        if (!t.Success) {

                            if (t.Info == "please login first")
                            {
                                KeepAliver.CheckAlive(afterCheck: (t) =>
                                {
                                    if (!t)
                                    {
                                        PlaySound(Sounds.die1);
                                        return;
                                    }
                                    else
                                    {
                                        socket.SendRequest($"Championship\\SignUp\\{c.Title}\\{text}");
                                    }
                                });
                            }
                            else if (t.Info[0..7] == "already") {
                                string k = t.Info.Split('-')[1];
                                text = k;
                                goto A;
                            }
                            else
                            {
                                PlaySound(Sounds.die1);
                            }

                            return;
                        }
                        A:
                        PlaySound(Sounds.Ding);
                        CurrentUser.ChampionshipData.SignUp(c.Title, text);
                        Save();
                        buttons.ForEach(s => s.State = SelectState.Disabled);
                        bt.ColorDisabled = col.Orange;
                        all[0].OnFocus();
                    });

                    socket.SendRequest($"Championship\\SignUp\\{c.Title}\\{text}");
                };
            }
            if (CurrentUser != null)
            {
                if (CurrentUser.ChampionshipData != null)
                {
                    if (CurrentUser.ChampionshipData.InChampionship(c.Title))
                    {
                        string divName = CurrentUser.ChampionShipDiv(c.Title);
                        buttons.ForEach(s =>
                        {
                            s.State = SelectState.Disabled;
                            if (s.Text == divName) s.ColorDisabled = col.Orange;
                        });
                    }
                }
            }
        }

        public override void Draw()
        {

        }
        col bound = col.Transparent;
        float height = 0;
        public override void Update()
        {
            bound = col.Lerp(bound, col.Aqua, 0.035f);
            height = MathHelper.Lerp(height, 200, 0.05f);

            DownBoundDistance = height;
            LeftBoundDistance = 0;
            RightBoundDistance = 0;
            UpBoundDistance = 0;
            BoundColor = bound;

            if (IsKeyPressed120f(InputIdentity.Cancel))
            {
                this.Dispose();
                InstanceCreate(new SelectUI());
            }

            if (IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) && IsKeyPressed120f(InputIdentity.Special)) {
                // insert championship

                PlaySound(Sounds.select);

                UFSocket<Empty> inserter = new((t) => {
                    if (t.Success) PlaySound(Sounds.Ding);
                    else PlaySound(Sounds.die1);
                });
                inserter.SendRequest("Championship\\Insert\\" + JsonSerializer.Serialize(currentChampionship.ToInfo()));
            }

            base.Update();
        }
        public override void Dispose()
        {
            MasterAlpha = 1f;
            DownBoundDistance = 0f;
            base.Dispose();
        }
    }
}