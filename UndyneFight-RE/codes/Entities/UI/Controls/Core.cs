using System;
using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    public enum SelectState
    {
        False = 0,
        MouseOn = 1,
        Selected = 2,
        Disabled = 3
    }
    /// <summary>
    /// A unit which is in a select chunk
    /// </summary>
    abstract public class SelectingModule : Entity
    {
        public SelectingModule(ISelectChunk father)
        {
            this._father = father;
        }

        public int ID { get; protected set; }
        public bool AlwaysActivate { get; protected set; }
        public bool MouseDisable { private get; set; } = true;

        public bool KeyLocked { get; protected set; } = false;

        public virtual void ConfirmKeyDown()
        {
            if (this.State == SelectState.MouseOn || this.State == SelectState.False)
            {
                if (!NeverEnable)
                    this.State = SelectState.Selected;
                _father?.Selected(this);
            }
            else if (this.State == SelectState.Selected)
            {
                this.State = SelectState.MouseOn;
            }
            LeftClick?.Invoke();
        }
        public virtual void OnFocus()
        {
            if (this.State == SelectState.False)
            {
                this.State = SelectState.MouseOn;
            }
            MouseOn?.Invoke();
            _mouseOn = true;
            _father?.FocusOn(this);
        }
        public virtual void OffFocus()
        {
            if (this.State == SelectState.MouseOn)
            {
                this.State = SelectState.False;
            }
            _mouseOn = false;
        }

        protected ISelectChunk _father;

        public SelectState State
        {
            protected get => _state;
            set
            {
                if (_state == SelectState.Selected && value == SelectState.False)
                {
                    ;
                }
                _state = value;
            }
        }
        private SelectState _state =
            SelectState.False;
        protected Color _drawingColor;
        protected bool _mouseOn;

        public bool IsMouseOn => _mouseOn;

        public event Action LeftClick;
        public event Action MouseOn;

        public bool ModuleEnabled => this.State != SelectState.Disabled;
        public bool ModuleSelected => this.State == SelectState.Selected;

        public bool NeverEnable { private get; set; } = false;

        public Color ColorNormal { private get; set; } = Color.White;
        public Color ColorMouseOn { private get; set; } = Color.LightGoldenrodYellow;
        public Color ColorSelected { private get; set; } = Color.Yellow;
        public Color ColorDisabled { private get; set; } = Color.Red;

        private int _timer = 0;

        public override void Update()
        {
            Color mission = this.State switch
            {
                SelectState.False => _mouseOn ? ColorMouseOn : ColorNormal,
                SelectState.MouseOn => ColorMouseOn,
                SelectState.Selected => ColorSelected,
                SelectState.Disabled => ColorDisabled,
                _ => throw new ArgumentException(),
            };
            _drawingColor = Color.Lerp(_drawingColor, mission, 0.12f);
            if (!AlwaysActivate)
            {
                if (_father != null && !_father.Activated && this.ModuleEnabled)
                {
                    _timer = 0;
                    //State = SelectState.False;
                    return;
                }
                _timer++;
                if (_timer < 3) return;
                if (this.State == SelectState.Disabled)
                    return;
            }
            bool isLeftClick = MouseSystem.IsLeftClick();
            if (!(MouseSystem.Moved || isLeftClick))
                return;
            if (_mouseOn = this.collidingBox.Contain(MouseSystem.TransferredPosition))
            {
                if (this.State == SelectState.False)
                {
                    this.State = SelectState.MouseOn;
                    this.MouseOn?.Invoke();
                    _father?.FocusOn(this);
                }
                if (isLeftClick)
                {
                    if (this.State == SelectState.MouseOn)
                    {
                        if (!NeverEnable)
                            this.State = SelectState.Selected;
                        this._father?.Selected(this);
                    }
                    else if(MouseDisable)
                    {
                        this.State = SelectState.MouseOn;
                    }
                    LeftClick?.Invoke();
                    _father?.FocusOn(this);
                }
            }
            else
            {
                if (this.State == SelectState.MouseOn) this.State = SelectState.False;
            }
        }

        internal void CheckMouse()
        {
            this._mouseOn = this.collidingBox.Contain(MouseSystem.TransferredPosition);
        }
    }
    public interface ISelectChunk
    {
        void Activate();
        void Deactivate();

        bool Activated { get; }
        bool DrawEnabled { get; }

        void Selected(SelectingModule module);
        void FocusOn(SelectingModule module);

        SelectingModule Focus { get; }
    }
    abstract public class SmartSelector : Entity, ISelectChunk
    {
        protected int DefaultFocus { private get;  set; } = 0;

        private int _activateTime = 0;

        public bool Activated => _activateTime > 2; 
        public bool DrawEnabled => _activated;

        public SelectingModule Focus => this.currentFocus;

        private bool _activated = false;

        protected event Action OnActivated;

        public void Activate()
        {
            this._state = SelectState.Selected;
            this._activated = true;
            this.OnActivated?.Invoke();
        }

        public void Deactivate()
        {
            this._state = SelectState.False;
            this._activated = false;
        }

        public void FocusOn(SelectingModule module)
        {
            currentFocus = module;
            focusID = -1;
        }

        public void Selected(SelectingModule module) {
            if (OneSelectionOnly)
            {
                if (CurrentSelected != null && CurrentSelected != module)
                    CurrentSelected.State = SelectState.False;
            }
            CurrentSelected = module;
            this.OnSelected?.Invoke();
        }
         protected SelectingModule CurrentSelected { get; private set; }
        protected event Action OnSelected;
        
        protected bool OneSelectionOnly { private get; set; }
        protected SelectingModule[] all;

        protected SelectingModule currentFocus;
        protected int focusID = -1;
        protected int FocusID
        {
            get
            {
                if (focusID == -1)
                {
                    for (int i = 0; i < all.Length; i++)
                    {
                        if (all[i] == currentFocus) return focusID = i;
                    }
                    throw new Exception();
                }
                else return focusID;
            }
        }

        protected Action KeyEvent { get; set; }
        protected Color DrawingColor { get; private set; }
        public bool ZKeyConfirm { get; internal set; } = true;

        private SelectState _state = SelectState.False;
        protected SelectState State { get => _state ; set => _state = value; }

        protected void KeyEventFull()
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
        protected void KeyEventNormal()
        {
            if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
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
                currentFocus?.ConfirmKeyDown();
            }
        }

        public override void Update()
        {
            if (_activated) _activateTime++;
            else _activateTime = 0;
            if (this.collidingBox.Contain(MouseSystem.TransferredPosition))
            {
                if (_state == SelectState.False) _state = SelectState.MouseOn;
                if (_state == SelectState.MouseOn && MouseSystem.IsLeftClick())
                {
                    this.Activate();
                }
            }
            else if (_state == SelectState.MouseOn && MouseSystem.Moved) { _state = SelectState.False; }

            Color mission = _state switch
            {
                SelectState.False => Color.White,
                SelectState.MouseOn => Color.PaleGoldenrod,
                SelectState.Selected => Color.Gold,
                _ => throw new ArgumentException()
            };
            DrawingColor = Color.Lerp(DrawingColor, mission, 0.12f);

            if (_activateTime < 2) return;
            if (this.ChildObjects.Count == 0) { return; }

            if (this.CurrentSelected != null && this.CurrentSelected.KeyLocked) return;
            if (currentFocus == null) return;
            if (ZKeyConfirm)
            {
                this.KeyEvent.Invoke();
            }
            else if(GameStates.CharInput == (char)1 || GameStates.CharInput == (char)13)
            {
                this.KeyEvent.Invoke();
            }
        }

        public SmartSelector()
        {
            this.KeyEvent = KeyEventNormal;
            UpdateIn120 = true;
        }

        public override void Start()
        {
            all = new SelectingModule[this.ChildObjects.Count];
            int i = 0;
            foreach (SelectingModule item in this.ChildObjects)
            {
                all[i] = item;
                i++;
            }
            if (all.Length > 0 && this.currentFocus == null)
            {
                this.currentFocus = all[DefaultFocus];
                this.currentFocus.OnFocus();
            }
            base.Start();
        }
    }
}