using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    { 
        partial class DifficultyUI : Entity, ISelectChunk
        {
            private class SeperateLine : Entity
            {
                private float _pos;
                public SeperateLine(float pos)
                {
                    this._pos = pos;
                }
                public override void Draw()
                {
                    DrawingLab.DrawLine(new(_pos, 620), new(_pos, 660), 3f, Color.Silver, 0.3f);
                }

                public override void Update()
                { 
                }
            }

            private void ChangeJudge(JudgementState judgeState)
            {
                this._virtualFather.ChangeJudge(judgeState);
            }

            IWaveSet _currentSelection;

            public SelectingModule Focus => null;

            private void UpdateChild()
            {
                if (_currentSelection != null && _currentSelection == this._virtualFather.SongSelected && this.ChildObjects.Count != 0) return;
                this.ChildObjects.Clear();
                _currentSelection = this._virtualFather.SongSelected;
                HashSet<Difficulty> diffPanel = this._virtualFather.DifficultyPanel;
                int count = diffPanel.Count;
                // L = 644, R = 930
                const float L = 676, R = 924;
                float delta = count switch
                {
                    1 => 0,
                    2 => 60,
                    3 => 40,
                    4 => 25,
                    5 => 15,
                    _ => throw new NotImplementedException()
                };
                float l = L + delta, r = R - delta;

                var attributes = _currentSelection.Attributes;
                if (attributes != null)
                    diffPanel.RemoveWhere(s => !attributes.UnlockedDifficulties.Contains(s));
                Difficulty[] difficulties = diffPanel.ToArray();
                Array.Sort(difficulties);

                for(int i = 0; i < count; i++)
                {
                    float pos = MathHelper.Lerp(l, r, count == 1 ? 0.5f : i * 1.0f / (count - 1));
                    CollideRect area = new();
                    area.Size = new Vector2(40, 60);
                    area.SetCentre(new Vector2(pos, 640));
                    
                    this.AddChild(new DifficultySelection(this, difficulties[i], area));
                }
                for(float i = 0.5f; i < count - 1; i += 1.0f)
                {
                    float pos = MathHelper.Lerp(l, r, i * 1.0f / (count - 1));
                    this.AddChild(new SeperateLine(pos));
                }

                _virtualFather.SelectDiff(Difficulty.NotSelected);

                this.all = new DifficultySelection[count];
                int j = 0;
                foreach(GameObject module in this.ChildObjects)
                {
                    if (module is DifficultySelection)
                    {
                        this.all[j] = module as DifficultySelection;
                        j++;
                    }
                }
                if (_lastDifficulty == Difficulty.NotSelected)
                    this.all[0].OnFocus();
                else
                {
                    int id = 0;
                    for(j = 0; j < all.Length; j++)
                    {
                        if ((int)all[j].Difficulty <= (int)_lastDifficulty) id = j;
                    }
                    this.all[id].OnFocus();
                }

                this.AddChild(new JudgementSelection(this));
            }

            VirtualFather _virtualFather;
            public DifficultyUI(VirtualFather virtualFather)
            {
                this._virtualFather = virtualFather;
                UpdateIn120 = true;
            }

            public bool Activated => _activated;
            public bool DrawEnabled => true;

            private bool _activated;

            public void Activate()
            {
                this._activated = true;
                this._state = SelectState.Selected;
                _virtualFather.Select(this);
                this.UpdateChild();
            }

            public void Deactivate()
            {
                this._state = SelectState.False;
                this._activated = false;
            }
            DifficultySelection[] all;

            SelectingModule _currentFocus;
            public SelectingModule CurrentFocus => _currentFocus;
            int focusID = -1;
            public int FocusID
            {
                get
                {
                    if (focusID == -1)
                    {
                        for (int i = 0; i < all.Length; i++)
                        {
                            if (all[i] == _currentFocus) return focusID = i;
                        }
                        return -1;
                    }
                    else return focusID;
                }
            }


            private SelectState _state = SelectState.False;
            private Color _drawingColor;
            public override void Draw()
            {
                Color color = _drawingColor;
                DrawingLab.DrawLine(new(48, 127 + 190), new(208, 127 + 190), 3.0f, color, 0.5f);

                FightResources.Font.NormalFont.CentreDraw("Diff", new(128, 60 + 190), color, 1.3f, 0.4f);
                FightResources.Font.NormalFont.CentreDraw("Select", new(128, 98 + 190), color, 1.3f, 0.4f);

                if (!Activated) return;
            }

            private int _timer = 0;
            public override void Update()
            {
                if (!this.Activated) { _timer = 0; this.TryActivate(); }
                this.collidingBox = new(48, 60 + 190, 208 - 48, 127 - 60);

                if (_virtualFather.DifficultyPanel == null) {
                    this._state = SelectState.Disabled;
                    if(this.ChildObjects.Count > 0) { this.ChildObjects.Clear(); }
                }
                else
                {
                    if (this.collidingBox.Contain(MouseSystem.TransferredPosition))
                    {
                        if (_state == SelectState.False) _state = SelectState.MouseOn;
                        if (_state == SelectState.MouseOn && MouseSystem.IsLeftClick())
                        {
                            this.Activate();
                        }
                    }
                    else if (_state == SelectState.MouseOn || _state == SelectState.Disabled) { _state = SelectState.False; }
                }
                Color mission = _state switch
                {
                    SelectState.False => Color.White,
                    SelectState.MouseOn => Color.PaleGoldenrod,
                    SelectState.Selected => Color.Gold,
                    SelectState.Disabled => Color.Red,
                    _ => throw new ArgumentException()
                };
                _drawingColor = Color.Lerp(_drawingColor, mission, 0.12f);

                if (!Activated) return;

                _timer++;
                if (_timer < 3) return;

                if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
                {
                    this._virtualFather.SongSelect.Activate();
                    this.Deactivate();
                }


                if (GameStates.IsKeyPressed120f(InputIdentity.MainDown) || GameStates.IsKeyPressed120f(InputIdentity.MainRight))
                {
                    int id = FocusID;
                    for (int i = id + 1; i < all.Length; i++)
                    {
                        if (all[i].ModuleEnabled)
                        {
                            _currentFocus.OffFocus();
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
                            _currentFocus.OffFocus();
                            all[i].OnFocus();
                            break;
                        }
                    }
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                {
                    _currentFocus.ConfirmKeyDown();
                }
            }

            private void TryActivate()
            {
                if (!MouseSystem.Moved) return;
                if (_virtualFather.DifficultyPanel == null) return;
                float x = MouseSystem.TransferredPosition.X;
                if (x > 644) this.Activate();
            }

            public void FocusOn(SelectingModule module)
            {
                _currentFocus = module;
                focusID = -1;
                if(module is DifficultySelection)
                {
                    this.FocusDifficulty = (module as DifficultySelection).Difficulty;
                }
            }

            SelectingModule _lastSelected;
            Difficulty _lastDifficulty = Difficulty.NotSelected;

            public Difficulty FocusDifficulty { get; private set; } = Difficulty.NotSelected;

            public void Selected(SelectingModule module)
            {
                if (module is not DifficultySelection) return;
                this._virtualFather.SelectDiff(_lastDifficulty = (module as DifficultySelection).Difficulty);
                if(_lastSelected != null && _lastSelected != module)
                {
                    _lastSelected.State = SelectState.False;
                }

                _lastSelected = module;
            }
        }
    }
}