using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI : Entity
    {
        private interface ISelectChunk
        {
            void Activate();
            void Deactivate();

            bool Activated { get; }

            void Register(SelectingModule module);
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
            public int ID { get; protected set; }
            protected virtual void Selected() { this.State = SelectState.Selected; }
            protected virtual void Deselected() { this.State = SelectState.False; }

            private ISelectChunk _father;

            public SelectState State { private get; set; } = SelectState.False;
            Color drawingColor;

            public override void Update()
            {
                Color mission = this.State switch
                {
                    SelectState.False => Color.White,
                    SelectState.MouseOn => Color.LightGoldenrodYellow,
                    SelectState.Selected => Color.Yellow,
                    _ => throw new ArgumentException(),
                };
                drawingColor = Color.Lerp(drawingColor, mission, 0.12f);
                if (!_father.Activated)
                {
                    State = SelectState.False;
                    return;
                }
                if(this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {

                }
            }
        }
        private class ModeSelector : Entity
        {
            public override void Draw()
            { 
            }

            public override void Update()
            { 
            }
        }

        public SelectUI()
        {
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;

            this.AddChild(new LineDistributer());
        }

        public override void Draw()
        { 

        }

        public override void Update()
        { 
        }
    }
}
