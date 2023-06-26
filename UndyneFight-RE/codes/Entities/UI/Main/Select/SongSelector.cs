using System;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private class SongSelector : Entity, ISelectChunk
        {
            public bool Activated { get; set; } = false;
            public bool DrawEnabled { get; set; } = false;

            public void Activate()
            {

            }

            public void Deactivate()
            {

            }

            public override void Draw()
            { 
            }

            public void FocusOn(SelectingModule module)
            {

            }

            public void Selected(SelectingModule module)
            {

            }

            public override void Update()
            {  
            }
        }
    }
}
