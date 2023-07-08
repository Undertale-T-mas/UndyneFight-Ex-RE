
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake.UI
{ 
    internal class TextInputer : SelectingModule
    {  
        public TextInputer(ISelectChunk father, CollideRect area) : base(father)
        { 
            collidingBox = area;
            UpdateIn120 = true;
            ColorMouseOn = Color.LightGoldenrodYellow;
            MouseDisable = false;
            if(father is SmartSelector)
            {
                (father as SmartSelector).ZKeyConfirm = false;
            }
            this.ResultChanged += () => {
                this.RightPosition = collidingBox.Left + 5 * FontScale + NormalFont.SFX.MeasureString(Result).X * FontScale;
            };
        }

        public string Result => currentString;

        public override void Draw()
        {
            if (!_father.DrawEnabled) return;
            if (ModuleSelected && appearTime % 66 <= 32)
            {
                FormalDraw(GlobalResources.Sprites.cursor, new Vector2(collidingBox.TopLeft.X + 1 +
                    NormalFont.SFX.MeasureString(drawingString[..cursorPlace]).X * FontScale, collidingBox.TopLeft.Y + 4), Color.White * 1.0f, 1.2f * FontScale, 0.0f, Vector2.Zero);
            }
            if (IsMouseOn && _missionCursorPlace != cursorPlace || this.State == SelectState.MouseOn)
            {
                FormalDraw(GlobalResources.Sprites.cursor, new Vector2(collidingBox.TopLeft.X + 1 +
                    NormalFont.SFX.MeasureString(drawingString[.._missionCursorPlace]).X * FontScale, collidingBox.TopLeft.Y + 4), Color.Gold * 0.7f, 1.2f * FontScale, 0.0f, Vector2.Zero);
            }
            if (drawingString != null)
                NormalFont.Draw(drawingString, collidingBox.TopLeft + new Vector2(5 * FontScale, 2), Color.LightCoral, FontScale, this.Depth);
            DrawingLab.DrawRectangle(collidingBox, this._drawingColor, 3f, 0.6f);
        }

        private int cursorPlace = 0, appearTime = 0;
        private string currentString = "";
        protected string drawingString = "";

        public float FontScale { protected get; set; } = 1.0f;

        public void InputChar(char input)
        {
            if (input == (char)13)
            {
                return;
            }
            appearTime = 0;

            string appro = currentString + input;
            float v = NormalFont.SFX.MeasureString(appro).X * FontScale + 10;
            if (v > CollidingBox.Size.X) return;

            currentString = currentString.Length == 0
                ? input.ToString()
                : currentString[..cursorPlace] + input +
                (currentString.Length > cursorPlace ?
                currentString[cursorPlace..] : "");

            cursorPlace++;
            this._missionCursorPlace = cursorPlace;
        }

        private int _missionCursorPlace = 0;
        protected event Action ResultChanged;

        protected float RightPosition { get; private set; }

        public override void Update()
        {
            if (!_father.Activated)
            {
                base.Update();
                return;
            }
            if((_mouseOn || State == SelectState.MouseOn) && ((CharInput != (char)1 && CharInput != (char)13) || IsKeyPressed120f(InputIdentity.Backspace)))
            {
                if (this.State != SelectState.Selected)
                {
                    this.State = SelectState.Selected;
                    this._father?.Selected(this);
                }
                this.cursorPlace = _missionCursorPlace;
            }
            base.Update();
            appearTime++;
            if (ModuleSelected)
            {
                if (WordsChanged && appearTime > 1 && (_father.Focus == this || !_father.Focus.IsMouseOn))
                {
                    InputChar(CharInput);
                    drawingString = currentString;
                    ResultChanged?.Invoke();
                }
                if (WordsChanged) return;
                if (IsKeyPressed120f(InputIdentity.Backspace) && (_father.Focus == this || !_father.Focus.IsMouseOn))
                {
                    appearTime = 0;
                    DoDelete();
                }

                if (_mouseOn && MouseSystem.IsLeftClick())
                {
                    int pos = GetCursorPlace();
                    this.cursorPlace = pos;
                } 
            }
            if((_father.Focus == this || (_father.Focus != null && !_father.Focus.IsMouseOn)))
            {
                if (IsKeyPressed120f(InputIdentity.MainLeft))
                {
                    if (cursorPlace > 0)
                        cursorPlace--;
                    appearTime = 0;
                    this._missionCursorPlace = cursorPlace;
                }
                if (IsKeyPressed120f(InputIdentity.MainRight))
                {
                    if (cursorPlace < currentString.Length)
                        cursorPlace++;
                    appearTime = 0;
                    this._missionCursorPlace = cursorPlace;
                }
            }

            if (IsMouseOn && MouseSystem.Moved)
            {
                this._missionCursorPlace = GetCursorPlace();
            }
        }

        private void DoDelete()
        {
            if (cursorPlace != 0)
            {
                currentString = string.Concat(currentString.AsSpan(0, cursorPlace - 1),
                                cursorPlace <= currentString.Length ? currentString[cursorPlace..] : "");
                cursorPlace--;
                drawingString = currentString;
                ResultChanged?.Invoke();
            }
            this._missionCursorPlace = cursorPlace;
        }

        private int GetCursorPlace()
        {
            int L = 0, R = currentString.Length;

            // Get the index of next char which mouse left click
            while (L < R)
            {
                int mid = (L + R) >> 1;
                if (MouseOnRight(mid))
                {
                    L = mid + 1;
                }
                else
                {
                    R = mid;
                }
            }
            int pos = L;
            if (pos > 0 && pos < drawingString.Length)
            {
                float strX = collidingBox.Left + 5 * FontScale +
                    (NormalFont.SFX.MeasureString(drawingString[..(pos - 1)]).X +
                    NormalFont.SFX.MeasureString(drawingString[pos].ToString()).X * 0.5f) * FontScale;
                if (MouseSystem.TransferredPosition.X < strX - 3) pos -= 1;
            }

            return pos;
        }

        private bool MouseOnRight(int charLength)
        {
            //NormalFont.Draw(currentString, collidingBox.TopLeft + new Vector2(5 * FontScale, 2), Color.LightCoral, FontScale, this.Depth);
            float mouseX = MouseSystem.TransferredPosition.X;
            float strX = collidingBox.Left + 5 * FontScale + NormalFont.SFX.MeasureString(drawingString[..charLength]).X * FontScale;
            return mouseX > strX;
        }
        public void SetString(string mission)
        {
            _missionCursorPlace = cursorPlace = mission.Length;
            currentString = mission;
            drawingString = mission;
            this.ResultChanged?.Invoke();
        }
    }
}