using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private class ModeSelector : Entity, ISelectChunk
        {
            private class StartButton : Button
            {
                public StartButton(ISelectChunk father, Vector2 centre) : base(father, centre, "< Start! >")
                {
                    UpdateIn120 = true;
                }

                int cur = 0;
                public int Type => cur;

                //Don't uncomment until Challenges are avalible
                //string[] modes = { "< Start! >", "< Championship >", "< Challenges >" };
                string[] modes = { "< Start! >", "< Championship >"};
                float[] scales = { 1.7f, 1.56f, 1.56f };

                public override void Update()
                {
                    base.Update();
                    if (this._father.Focus != this) return;
                    int last = cur;
                    if (GameStates.IsKeyPressed120f(InputIdentity.MainLeft)) cur--;
                    else if (GameStates.IsKeyPressed120f(InputIdentity.MainRight)) cur++;
                    if(cur < 0) cur = modes.Length - 1;
                    else if(cur >= modes.Length) cur = 0;
                    if(last != cur)
                    {
                        Functions.PlaySound(FightResources.Sounds.changeSelection);
                        this.ChangeText(modes[cur]);
                        this.DefaultScale = scales[cur];
                        this.ColorNormal = cur switch
                        {
                            0 => Color.White,
                            1 => PlayerManager.CurrentUser != null ? Color.White : Color.DarkRed,
                            2 => Color.DarkRed,
                            _ => throw new NotImplementedException()
                        };
                    }
                }
            }

            public bool Activated { get; set; } = true;
            public bool DrawEnabled { get; set; } = true;

            private VirtualFather _virtualFather;

            public void Activate()
            {
                this.Activated = true;
                this.DrawEnabled = true;
                this._state = SelectState.Selected;
                _virtualFather.Select(this);
                _virtualFather.SongSelect.DrawEnabled = false;
            }

            public void Deactivate()
            {
                this.Activated = false;
                this.DrawEnabled = false;
                this._state = SelectState.False;
            }

            public override void Draw()
            {
                int k = -5;
                Color color = _drawingColor;
                DrawingLab.DrawLine(new(48, 127 + k), new(208, 127 + k), 3.0f, color, 0.5f);

                FightResources.Font.NormalFont.CentreDraw("Mode", new(128, 60 + k), color, 1.3f, 0.4f);
                FightResources.Font.NormalFont.CentreDraw("Select", new(128, 98 + k), color, 1.3f, 0.4f);

                if (!Activated) return;
            }

            public void Selected(SelectingModule module)
            {
            }

            private void TryActivate()
            {
                if (!MouseSystem.Moved) return;
                float x = MouseSystem.TransferredPosition.X;
                if (x > 231 && x < 644) this.Activate();
            }

            private SelectState _state = SelectState.Selected;
            Color _drawingColor { get; set; }
            public override void Update()
            {
                if (this.DrawEnabled && !this.Activated)
                {
                    this.TryActivate();
                }
                this.collidingBox = new(48, 60 - 5, 208 - 48, 127 - 60);
                if (this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {
                    if (_state == SelectState.False) _state = SelectState.MouseOn;
                    if (_state == SelectState.MouseOn && MouseSystem.IsLeftClick())
                    {
                        this.Activate();
                    }
                }
                else if (_state == SelectState.MouseOn) { _state = SelectState.False; }

                Color mission = _state switch
                {
                    SelectState.False => Color.White,
                    SelectState.MouseOn => Color.PaleGoldenrod,
                    SelectState.Selected => Color.Gold,
                    _ => throw new ArgumentException()
                };
                _drawingColor = Color.Lerp(_drawingColor, mission, 0.12f);

                if(!this.Activated) return;

                if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                {
                    int id = FocusID;
                    for(int i = id + 1; i < all.Length; i++)
                    {
                        if (all[i].ModuleEnabled)
                        {
                            currentFocus.OffFocus();
                            all[i].OnFocus(); 
                            break;
                        }
                    }
                }
                else if (GameStates.IsKeyPressed120f(InputIdentity.MainUp))
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
                    currentFocus.ConfirmKeyDown();
                } 
            }

            SelectingModule[] all;

            SelectingModule currentFocus;
            int focusID = -1;
            int FocusID { 
                get 
                {
                    if (focusID == -1) {
                        for(int i = 0; i < all.Length; i++)
                        {
                            if (all[i] == currentFocus) return focusID = i;
                        }
                        throw new Exception();
                    }
                    else return focusID;
                } 
            }

            public override void Start()
            {
                all = new SelectingModule[this.ChildObjects.Count];
                int i = 0;
                foreach(SelectingModule item in this.ChildObjects)
                {
                    all[i] = item;
                    i++;
                }
                this._virtualFather = this.FatherObject as VirtualFather;
                base.Start();
            }

            public void FocusOn(SelectingModule module)
            {
                currentFocus = module;
                focusID = -1;
            }

            Button noHitButton, apButton, buffButton, practiceButton, ngsButton, autoButton;
            StartButton startButton;

            public GameMode ModeSelected =>
                (noHitButton.ModuleSelected ? GameMode.NoHit : GameMode.None) |
                (apButton.ModuleSelected ? GameMode.PerfectOnly : GameMode.None) |
                (buffButton.ModuleSelected ? GameMode.Buffed : GameMode.None) |
                (practiceButton.ModuleSelected ? GameMode.Practice : GameMode.None) |
                (ngsButton.ModuleSelected ? GameMode.NoGreenSoul : GameMode.None) |
                (autoButton.ModuleSelected ? GameMode.Autoplay : GameMode.None);

            public SelectingModule Focus => this.currentFocus;

            public ModeSelector()
            {
                UpdateIn120 = true;

                this.AddChild(currentFocus = startButton = new StartButton(this, new(440, 100)) { DefaultScale = 1.7f, NeverEnable = true });
                currentFocus.OnFocus();
                this.AddChild(noHitButton = new Button(this, new(440, 210), "No Hit") ); 
                this.AddChild(apButton = new Button(this, new(440, 285), "Perfect Only") ); 
                this.AddChild(buffButton = new Button(this, new(440, 360), "Buffed") ); 
                this.AddChild(practiceButton = new Button(this, new(440, 435), "Practice") ); 
                this.AddChild(ngsButton = new Button(this, new(440, 510), "No Greensoul") );
                this.AddChild(autoButton = new Button(this, new(440, 585), "Autoplay") );

                startButton.LeftClick += () => {
                    if (startButton.Type == 0)
                        this._virtualFather.SongSelect.Activate();
                    else if(startButton.Type == 1)
                    {
                        if (PlayerManager.CurrentUser == null) return;
                        this.Dispose();
                        this._virtualFather.Dispose();
                        this._virtualFather.FatherObject?.Dispose();
                        GameStates.InstanceCreate(new ChampionshipSelector());
                    }
                };
                noHitButton.LeftClick += () => practiceButton.State = noHitButton.ModuleSelected ? SelectState.Disabled : SelectState.False;
                practiceButton.LeftClick += () => noHitButton.State = practiceButton.ModuleSelected ? SelectState.Disabled : SelectState.False;
            }
        }
    }
}
