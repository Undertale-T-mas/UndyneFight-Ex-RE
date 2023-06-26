using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Net.Sockets;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI : Entity
    {
        private interface ISelectChunk
        {
            void Activate();
            void Deactivate();

            bool Activated { get; }
            bool DrawEnabled { get; }
             
            void Selected(SelectingModule module);
            void FocusOn(SelectingModule module);
        }
        enum SelectState
        {
            False = 0,
            MouseOn = 1,
            Selected = 2,
            Disabled = 3
        }
        /// <summary>
        /// A unit which is in a select chunk
        /// </summary>
        private abstract class SelectingModule : Entity
        {
            public SelectingModule(ISelectChunk father)
            {
                this._father = father;
            }

            public int ID { get; protected set; }
            public virtual void ConfirmKeyDown() {
                if (this.State == SelectState.MouseOn)
                {
                    if (!NeverEnable)
                        this.State = SelectState.Selected;
                }
                else if(this.State == SelectState.Selected)
                {
                    this.State = SelectState.MouseOn;
                }
                LeftClick?.Invoke();
            }
            public virtual void OnFocus() { 
                if (this.State == SelectState.False) {
                    this.State = SelectState.MouseOn;
                }
                MouseOn?.Invoke();
                _mouseOn = true;
                _father.FocusOn(this);
            }
            public virtual void OffFocus()
            {
                if(this.State == SelectState.MouseOn)
                {
                    this.State = SelectState.False;
                }
                _mouseOn = false;
            }

            protected ISelectChunk _father;

            public SelectState State { protected get; set; } = SelectState.False;
            protected Color _drawingColor;
            protected bool _mouseOn;

            public event Action LeftClick;
            public event Action MouseOn;

            public bool ModuleEnabled => this.State != SelectState.Disabled;
            public bool ModuleSelected => this.State == SelectState.Selected;

            public bool NeverEnable { private get; set; } = false;

            public Color ColorNormal { private get; set; } = Color.White;
            public Color ColorMouseOn { private get; set; } = Color.LightGoldenrodYellow;
            public Color ColorSelected { private get; set; } = Color.Yellow;
            public Color ColorDisabled { private get; set; } = Color.Red;

            public override void Update()
            {
                if (!_father.Activated)
                {
                    State = SelectState.False;
                    return;
                }
                Color mission = this.State switch
                {
                    SelectState.False => ColorNormal,
                    SelectState.MouseOn => ColorMouseOn,
                    SelectState.Selected => ColorSelected,
                    SelectState.Disabled => ColorDisabled,
                    _ => throw new ArgumentException(),
                };
                _drawingColor = Color.Lerp(_drawingColor, mission, 0.12f);
                bool isLeftClick = MouseSystem.IsLeftClick();
                if (!(MouseSystem.Moved || isLeftClick) || (this.State == SelectState.Disabled)) return;
                if(_mouseOn = this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {
                    if (this.State == SelectState.False)
                    { 
                        this.State = SelectState.MouseOn; 
                        this.MouseOn?.Invoke(); 
                        _father.FocusOn(this); 
                    }
                    if (isLeftClick)
                    {
                        if (this.State == SelectState.MouseOn)
                        {
                            if (!NeverEnable)
                                this.State = SelectState.Selected;
                            this._father.Selected(this);
                        }
                        else
                        {
                            this.State = SelectState.MouseOn;
                        }
                        LeftClick?.Invoke();
                        _father.FocusOn(this);
                    }
                }
                else
                {
                    if (this.State == SelectState.MouseOn) this.State = SelectState.False;
                }
            }
        }
        private class VirtualFather : GameObject 
        {
            public VirtualFather()
            {
                modeSelect = new ModeSelector();
                this.AddChild(modeSelect);
                CurrentActivate = modeSelect;
            }

            public bool Activated => true;

            ModeSelector modeSelect;
            SongSelector songSelect;

            public ISelectChunk CurrentActivate; 

            public void Selected(ISelectChunk module)
            {
                CurrentActivate.Deactivate();
                CurrentActivate = module;
            }

            public override void Update()
            { 
            }
        }

        public SelectUI()
        {
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;

            this.AddChild(new MouseCursor());
            this.AddChild(new LineDistributer());
            this.AddChild(new VirtualFather());
        }

        public override void Draw()
        { 

        }

        public override void Update()
        { 
        }
    }
}
