using Microsoft.Xna.Framework;
using System;
using System.Net.Sockets;
using System.Text.Json.Serialization;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing;

using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using System.Collections.Generic;
using UndyneFight_Ex.ChampionShips;
using UndyneFight_Ex.SongSystem;
using Microsoft.Xna.Framework.Graphics;
using System.Buffers;
using UndyneFight_Ex.Remake.Network;
using System.Text.Json;

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
                    this.AddChild(new Particle(Color.White, new(0, -speed * 1.15f * 4f), Rand(16f, 24f) / MathF.Pow(speed, 1.42f) * 1.315f, new(Rand(0, 960), 730), FightResources.Sprites.square)
                    {
                        DarkingSpeed = 4,
                        AutoRotate = true
                    });
                }
            }
        }
        private void KeyAction()
        { 
            if (GameStates.IsKeyPressed120f(InputIdentity.MainDown) || GameStates.IsKeyPressed120f(InputIdentity.MainRight))
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
            else if (GameStates.IsKeyPressed120f(InputIdentity.MainUp) || GameStates.IsKeyPressed120f(InputIdentity.MainLeft))
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
            if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
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
                    GameStates.InstanceCreate(new InstantEvent(0, () => {
                        var extra = CurrentSelected.Extras as SongFightingScene.SceneParams;
                        GameStates.StartSong(extra);
                        GameStates.difficulty = extra.difficulty;
                    }));
                }
                else
                {
                    // get back

                    this.Dispose();
                    GameStates.InstanceCreate(new SelectUI());
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
                    GameStates.InstanceCreate(new SelectUI());
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
            this.AddChild(scoreboard= new Scoreboard(PlayerManager.CurrentUser?.ChampionshipData?.ChampionshipDivision(_currentChampionshipNAME)));
            this.AddChild(new MouseCursor());
            DivisionSelector ds = new DivisionSelector(cis.Info);
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
                    GameStates.InstanceCreate(new InfoText("Check the connection!", new(480, 400)) { DrawingColor = col.Red }); ;
                    GameStates.InstanceCreate(new SelectUI());
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
                    GameStates.InstanceCreate(new SelectUI());

                    GameStates.InstanceCreate(new InfoText("Time delta too large!", new(480, 400)) { DrawingColor = col.Red }); ;
                }
            });
            OnlineCheck.SendRequest("Time\\none");
        }

        private void MakeSongSelection(ChampionshipInfoShower cis, ChampionShip c)
        {
            if (PlayerManager.CurrentUser != null)
            {
                if (PlayerManager.CurrentUser.ChampionshipData.InChampionship(c.Title))
                {
                    string divName = PlayerManager.CurrentUser.ChampionshipData.ChampionshipDivision(c.Title);
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
                        buttons[i].Extras = new SongFightingScene.SceneParams(waveset, illustration, (int)song.DifficultyPanel[divName], "Content\\Musics\\" + waveset.Music + "\\song", JudgementState.Strict);

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
            List<Button> buttons = new List<Button>();
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
                    if (PlayerManager.CurrentUser == null) return;

                    UFSocket<Empty> socket = null;
                    socket = new(t => {
                        if (!t.Success) {

                            if (t.Info == "please login first")
                            {
                                KeepAliver.CheckAlive(afterCheck: (t) =>
                                {
                                    if (!t)
                                    {
                                        PlaySound(FightResources.Sounds.die1);
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
                                PlaySound(FightResources.Sounds.die1);
                            }

                            return;
                        }
                        A:
                        PlaySound(FightResources.Sounds.Ding);
                        PlayerManager.CurrentUser.ChampionshipData.SignUp(c.Title, text);
                        PlayerManager.Save();
                        buttons.ForEach(s => s.State = SelectState.Disabled);
                        bt.ColorDisabled = col.Orange;
                        all[0].OnFocus();
                    });

                    socket.SendRequest($"Championship\\SignUp\\{c.Title}\\{text}");
                };
            }
            if (PlayerManager.CurrentUser != null)
            {
                if (PlayerManager.CurrentUser.ChampionshipData != null)
                {
                    if (PlayerManager.CurrentUser.ChampionshipData.InChampionship(c.Title))
                    {
                        string divName = PlayerManager.CurrentUser.ChampionShipDiv(c.Title);
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
        Color bound = Color.Transparent;
        float height = 0;
        public override void Update()
        {
            bound = Color.Lerp(bound, Color.Aqua, 0.035f);
            height = MathHelper.Lerp(height, 200, 0.05f);

            DownBoundDistance = height;
            LeftBoundDistance = 0;
            RightBoundDistance = 0;
            UpBoundDistance = 0;
            BoundColor = bound;

            if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
            {
                this.Dispose();
                GameStates.InstanceCreate(new SelectUI());
            }

            if (GameStates.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) && GameStates.IsKeyPressed120f(InputIdentity.Special)) {
                // insert championship

                PlaySound(FightResources.Sounds.select);

                UFSocket<Empty> inserter = new((t) => {
                    if (t.Success) PlaySound(FightResources.Sounds.Ding);
                    else PlaySound(FightResources.Sounds.die1);
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