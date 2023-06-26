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
             
            void Selected(SelectingModule module);
        }
        enum SelectState
        {
            False = 0,
            MouseOn = 1,
            Selected = 2
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
            protected virtual void Selected() { this.State = SelectState.Selected; }
            protected virtual void Deselected() { this.State = SelectState.False; }

            protected ISelectChunk _father;

            public SelectState State { private get; set; } = SelectState.False;
            protected Color _drawingColor;

            protected event Action LeftClick;

            public override void Update()
            {
                Color mission = this.State switch
                {
                    SelectState.False => Color.White,
                    SelectState.MouseOn => Color.LightGoldenrodYellow,
                    SelectState.Selected => Color.Yellow,
                    _ => throw new ArgumentException(),
                };
                _drawingColor = Color.Lerp(_drawingColor, mission, 0.12f);
                if (!MouseSystem.Moved) return;
                if (!_father.Activated)
                {
                    State = SelectState.False;
                    return;
                }
                if(this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {
                    if (this.State == SelectState.False) this.State = SelectState.MouseOn;
                    if (MouseSystem.IsLeftClick())
                    {
                        if (this.State == SelectState.MouseOn)
                        {
                            this.State = SelectState.Selected;
                            this._father.Selected(this);
                        }
                        else
                        {
                            this.State = SelectState.MouseOn;
                        }
                        LeftClick?.Invoke();
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
                var modeselect = new ModeSelector();
                this.AddChild(modeselect);
                CurrentActivate = modeselect;
            }

            public bool Activated => true;

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
