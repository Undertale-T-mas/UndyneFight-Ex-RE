using Microsoft.Xna.Framework; 
using System.Collections.Generic;
using UndyneFight_Ex.Remake.UI;

namespace RecallCharter
{
    public class MasterControl : Control
    {
        public static Control FocusControl { get; set; }
        public MasterControl() { 
            this.AddChild(new MouseCursor());
            CurrentScene.CurrentDrawingSettings.defaultWidth = 1080;
            CurrentScene.CurrentDrawingSettings.backGroundColor = new(31, 31, 31, 255);
            this.AddChild(new FileButton());
#if DEBUG
            CurrentScene.Pausable = true;
#endif
        }
        public override void Update()
        {
            this.IsEnabled = true;
        }
    }
}