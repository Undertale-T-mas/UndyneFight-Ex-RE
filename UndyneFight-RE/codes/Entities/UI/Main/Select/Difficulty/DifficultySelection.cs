using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    { 
        partial class DifficultyUI
        {
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
                    MouseOn += () =>
                    {
                        Functions.PlaySound(FightResources.Sounds.changeSelection);
                    };

                    var attribute = father._virtualFather.SongSelected.Attributes;
                    if (attribute != null)
                    {
                        var map = attribute.ComplexDifficulty;
                        if (map.ContainsKey(difficulty)) this._difText = ((int)map[difficulty]).ToString();
                        else this._difText = "?";
                    }
                    else {
                        this._difText = "?";
                    }
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
                float _move = 0.0f;

                public Difficulty Difficulty => _difficulty;

                public override void Draw()
                {
                    var normalFont = FightResources.Font.NormalFont;
                    var fightFont = FightResources.Font.FightFont;
                    fightFont.CentreDraw(_text, Centre - new Vector2(-1, 12 + _move), Color.Lerp(_color, _drawingColor, _scale));
                    normalFont.CentreDraw(_difText, Centre + new Vector2(1, 12 - _move), Color.Lerp(_color, _drawingColor, _scale), 1.1f, 0.1f);

                    if(this._move > 0.1f)
                    {
                        DrawingLab.DrawLine(Centre + new Vector2(-10, 26), Centre + new Vector2(10, 26), 3.0f, _color * (_move / 2.5f), 0.2f);
                    }
                }
                private int _lastClickTimer = 0;
                public override void Update()
                { 
                    if (GameStates.IsKeyPressed120f(InputIdentity.Confirm) || (_mouseOn && MouseSystem.IsLeftClick()))
                    { // double Click;
                        if (_lastClickTimer < 50 && _lastClickTimer > 2)
                        {
                            _father._virtualFather.SelectDiff(this._difficulty);
                            _father._virtualFather.StartGame();
                        }
                    }

                    if (ModuleSelected)
                    {
                        _lastClickTimer++;
                        this._scale = MathHelper.Lerp(_scale, 1, 0.1f);
                    }
                    else
                    {
                        this._scale = MathHelper.Lerp(_scale, 0, 0.1f);
                        _lastClickTimer = 0;
                    }
                    if (_mouseOn)
                    {
                        this._move = MathHelper.Lerp(_move, 2.5f, 0.1f);
                    }
                    else this._move = MathHelper.Lerp(_move, 0, 0.1f);

                    base.Update();
                }
            }
        }
    }
}