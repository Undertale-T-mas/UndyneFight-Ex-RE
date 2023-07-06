using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.FightResources;
using UndyneFight_Ex.Fight;
using Microsoft.Xna.Framework.Audio;

namespace UndyneFight_Ex.Remake.UI
{
    internal class ScrollBar : SelectingModule
    {
        public ScrollBar(CollideRect area, string text, float areaL, float areaR, ISelectChunk father) : base(father)
        {
            this.collidingBox = area;
            UpdateIn120 = true;
            this._text = text;
            this._areaL = areaL; this._areaR = areaR;
            this.NeverEnable = true;
            this.MouseOn += () => {
                Functions.PlaySound(Sounds.changeSelection, Volume);
            };
        }
        public float DefaultValue { init { this.SetValue(value); } }
        string _text;
        float _areaL, _areaR;

        public float FontScale { private get; set; } = 1.2f;
        private static void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
        {
            DrawingLab.DrawLine(start, end, size, color, 0.5f);
        }
        float _selectingScale = 0.5f;
        float _currentValue = 0;
        public int Digit { private get; set; } = 1;

        private int _parentTimer = 0;

        public void SetValue(float value)
        {
            this._selectingScale = this._focusScale = (value - this._areaL) / (this._areaR -  this._areaL);
        }
        public float GetValue()
        {
            return MathHelper.Lerp(this._areaL, _areaR, this._selectingScale);
        }
        public override void Draw()
        {
            if(!_father.DrawEnabled) return;
            NormalFont.CentreDraw(this._text, this.Centre - new Vector2(0, 28), _drawingColor);
            Vector2 l = this.CollidingBox.BottomLeft - new Vector2(0, 4), r = this.CollidingBox.BottomRight - new Vector2(0, 4);
            DrawLine(l, r, Color.White);
            DrawLine(l, l - new Vector2(0, 6), Color.White);
            DrawLine(r, r - new Vector2(0, 6), Color.White);
            FightFont.CentreDraw(this._areaL.ToString("f1"), l + new Vector2(-7, -16), _drawingColor);
            FightFont.CentreDraw(this._areaR.ToString("f1"), r + new Vector2(7, -16), _drawingColor);
            FightFont.CentreDraw(this._currentValue.ToString("f" + Digit), this.Centre + new Vector2(0, -2), _secondaryColor * _valueAlpha);
            this.Image = Resources.UI.ScrollArrow;
            this.FormalDraw(Image, new Vector2(MathHelper.Lerp(l.X, r.X, _selectingScale), this.collidingBox.Down - ImageCentre.Y - 5),
                Color.White, 0, ImageCentre);

            if(IsMouseOn && !MouseSystem.IsLeftDown())
            {
                this.FormalDraw(Image, new Vector2(MathHelper.Lerp(l.X, r.X, _focusScale), this.collidingBox.Down - ImageCentre.Y - 5),
                    Color.Silver * 0.7f * _focusAlpha, 0, ImageCentre);
            }
        }
        Color _secondaryColor = Color.White;
        float _focusScale = 0.5f;
        float _valueAlpha = 1.0f;
        float _focusAlpha = 1.0f;
        private int lastOnTimer = 10;

        public float Volume { private get; set; } = 1.0f;
        public float KeyScale { private get; set; } = 0.1f;
        public SoundEffect ChangeSound { private get; set; } = Sounds.changeSelection;

        public event Action OnChange;

        public override void Update()
        {
            base.Update();
            if (!_father.Activated) { _parentTimer = -1; lastOnTimer = 10; return; }
            else if (MouseSystem.PositionMoved) _parentTimer++;

            float posL = this.collidingBox.Left, posR = this.collidingBox.Right;

            if (_father.Focus == this)
            {
                if (GameStates.IsKeyPressed120f(InputIdentity.MainLeft) && _selectingScale > 0)
                {
                    _selectingScale -= KeyScale;
                    _selectingScale = MathF.Max(0, _selectingScale);
                    _focusScale = _selectingScale;
                    OnChange?.Invoke();
                    Functions.PlaySound(ChangeSound, Volume);
                    this._secondaryColor = Color.MediumPurple;
                }
                else if (GameStates.IsKeyPressed120f(InputIdentity.MainRight) && _selectingScale < 1)
                {
                    _selectingScale += KeyScale;
                    _selectingScale = MathF.Min(1, _selectingScale);
                    _focusScale = _selectingScale;
                    OnChange?.Invoke();
                    Functions.PlaySound(ChangeSound, Volume);
                    this._secondaryColor = Color.MediumPurple;
                }
            }
            _currentValue = MathHelper.Lerp(_areaL, _areaR, _selectingScale);
            if (!IsMouseOn && lastOnTimer > 4) {
                _secondaryColor = Color.Lerp(_secondaryColor, Color.White, 0.1f);
                this._focusScale = this._selectingScale; this._valueAlpha = 1f; 
                return;
            }
            if (MouseSystem.Moved && _parentTimer > 15)
            {
                this._focusScale = (MouseSystem.TransferredPosition.X - posL) / (posR - posL);
                _focusScale = MathUtil.Clamp(0, _focusScale, 1);
            }
            if (IsMouseOn) lastOnTimer = 0;
            else lastOnTimer++;
            if (MouseSystem.IsLeftDown() && _parentTimer > 15)
            {
                this._selectingScale = _focusScale;
                this._secondaryColor = Color.Lerp(_secondaryColor, Color.Gold, 0.12f);
            }
            else this._secondaryColor = Color.Lerp(_secondaryColor, Color.White, 0.1f);
            if (MouseSystem.IsLeftReleaseing() && _parentTimer > 15)
            {
                OnChange?.Invoke();
                Functions.PlaySound(ChangeSound, Volume);
            }
            this._focusAlpha = MathF.Min(1.0f, MathF.Abs(_focusScale - _selectingScale) * 20);

            _currentValue = MathHelper.Lerp(_areaL, _areaR, _focusScale);
            this._valueAlpha = MathF.Max(0.6f, 1 - MathF.Abs(_focusScale - _selectingScale) * 5);
        }
    }
}
