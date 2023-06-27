using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private class ModeSelector : Entity, ISelectChunk
        { 
            public bool Activated { get; set; } = true;
            public bool DrawEnabled { get; set; } = true;

            private VirtualFather _virtualFather;

            public void Activate()
            {
                this.Activated = true;
                this.DrawEnabled = true;
                this._state = SelectState.Selected;
                _virtualFather.Select(this);
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

            private SelectState _state = SelectState.Selected;
            Color _drawingColor { get; set; }
            public override void Update()
            {
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

            Button startButton, noHitButton, apButton, buffButton, practiceButton, ngsButton;

            public ModeSelector()
            {
                UpdateIn120 = true;

                this.AddChild(currentFocus = startButton = new Button(this, new(440, 100), "Start!") { DefaultScale = 1.7f, NeverEnable = true });
                currentFocus.OnFocus();
                this.AddChild(noHitButton = new Button(this, new(440, 210), "No Hit") ); 
                this.AddChild(apButton = new Button(this, new(440, 290), "Perfect Only") ); 
                this.AddChild(buffButton = new Button(this, new(440, 370), "Buffed") ); 
                this.AddChild(practiceButton = new Button(this, new(440, 450), "Practice") ); 
                this.AddChild(ngsButton = new Button(this, new(440, 530), "No Greensoul") );

                startButton.LeftClick += () => {  
                    this._virtualFather.SongSelect.Activate();
                };
                noHitButton.LeftClick += () => practiceButton.State = noHitButton.ModuleSelected ? SelectState.Disabled : SelectState.False;
                practiceButton.LeftClick += () => noHitButton.State = practiceButton.ModuleSelected ? SelectState.Disabled : SelectState.False;
            }
        }
    }
}
