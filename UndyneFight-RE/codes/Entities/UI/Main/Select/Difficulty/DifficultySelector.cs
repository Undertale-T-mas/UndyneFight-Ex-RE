using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex.Fight;
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
            private class DifficultySelection : SelectingModule
            {
                private Difficulty _difficulty;
                private new DifficultyUI _father;
                public DifficultySelection(DifficultyUI father, Difficulty difficulty, CollideRect area) : base(father)
                {
                    _difficulty = difficulty;
                    _father = father;
                    this.collidingBox = area;

                    UpdateIn120 = true;

                    LeftClick += () => {
                        if (!this.ModuleSelected) _father._virtualFather.SelectDiff(Difficulty.NotSelected);
                        Functions.PlaySound(FightResources.Sounds.select);
                    };

                    var map = father._virtualFather.SongSelected.Attributes.ComplexDifficulty;
                    if (map.ContainsKey(difficulty)) this._difText = ((int)map[difficulty]).ToString();
                    else this._difText = "?";

                    switch(difficulty)
                    {
                        case Difficulty.Noob:
                            _color = Color.White;
                            _text = "nb";
                            break;
                        case Difficulty.Easy:
                            _color = Color.LimeGreen;
                            _text = "ez";
                            break;
                        case Difficulty.Normal:
                            _color = Color.LightSkyBlue;
                            _text = "nr";
                            break;
                        case Difficulty.Hard:
                            _color = Color.MediumPurple;
                            _text = "hd";
                            break;
                        case Difficulty.Extreme:
                            _color = Color.Orange;
                            _text = "ex";
                            break;
                        case Difficulty.ExtremePlus:
                            _color = Color.Gray;
                            _text = "ex+";
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
                Color _color;
                string _difText;
                string _text;

                float _scale = 1.0f;

                public Difficulty Difficulty => _difficulty;

                public override void Draw()
                {
                    var normalFont = FightResources.Font.NormalFont;
                    var fightFont = FightResources.Font.FightFont;
                    fightFont.CentreDraw(_text, Centre - new Vector2(0, 12 + _scale * 2f), Color.Lerp(_color, _drawingColor, _scale));
                    normalFont.CentreDraw(_difText, Centre + new Vector2(0, 12 - _scale * 2f), Color.Lerp(_color, _drawingColor, _scale), 1.1f, 0.1f);
                }
                public override void Update()
                {
                    if (_mouseOn || ModuleSelected)
                    {
                        this._scale = MathHelper.Lerp(_scale, 1, 0.1f);
                    }
                    else this._scale = MathHelper.Lerp(_scale, 0, 0.1f);

                    base.Update();
                }
            }
            private void UpdateChild()
            {
                this.ChildObjects.Clear();
                HashSet<Difficulty> diffPanel = this._virtualFather.DifficultyPanel;
                int count = diffPanel.Count;
                // L = 644, R = 930
                const float L = 682, R = 924;
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

            public override void Update()
            {
                if (!this.Activated) { this.TryActivate(); }
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
                if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
                {
                    this._virtualFather.SongSelect.Activate();
                    this.Deactivate();
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

            }

            SelectingModule _lastSelected;

            public void Selected(SelectingModule module)
            {
                this._virtualFather.SelectDiff((module as DifficultySelection).Difficulty);
                if(_lastSelected != null && _lastSelected != module)
                {
                    _lastSelected.State = SelectState.False;
                }

                _lastSelected = module;
            }
        }
    }
}