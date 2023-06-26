using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private class ModeSelector : Entity, ISelectChunk
        {
            private class Button : SelectingModule
            {
                public Button(ISelectChunk father, Vector2 centre, string text) : base(father)
                {
                    this.collidingBox.Size = FightResources.Font.NormalFont.SFX.MeasureString(text) * 1.1f;
                    this.Centre = centre;
                    this._text = text;
                }
                string _text;

                public override void Draw()
                {
                    if (!this._father.Activated) return;
                    FightResources.Font.NormalFont.CentreDraw(_text, Centre, _drawingColor);
                }
                public override void Update()
                {
                    base.Update(); 
                }
            }

            public bool Activated { get; set; } = true;

            private VirtualFather _virtualFather;

            public void Activate()
            {
                this.Activated = true;
            }

            public void Deactivate()
            {
                this.Activated = false;
            }

            public override void Draw()
            { 
                if(!Activated) return;
            }

            public void Selected(SelectingModule module)
            { 
            }

            public override void Update()
            { 
            }

            SelectingModule[] all;
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
        }
    }
}
