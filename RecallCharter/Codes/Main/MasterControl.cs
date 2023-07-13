using Microsoft.Xna.Framework; 
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UndyneFight_Ex.Remake.UI;

namespace RecallCharter
{
    public class MasterControl : Control
    {
        private static MasterControl _master;
        public static Control FocusControl { get; set; }
        public MasterControl() {
            _master = this;
            this.AddChild(new MouseCursor());
            CurrentScene.CurrentDrawingSettings.defaultWidth = 1080;
            CurrentScene.CurrentDrawingSettings.backGroundColor = new(31, 31, 31, 255);
            this.AddChild(new FileButton());
#if DEBUG
            CurrentScene.Pausable = true;
#endif
            this.ChildrenUpdateFirst = false;
        }
        public override void Start()
        {
            this.ChildrenUpdateFirst = false;
            base.Start();
        }
        private static List<Control> _childBuffer = new List<Control>();
        public static void AddControl(Control control)
        {
            _childBuffer.Add(control);
        }
        public override void Update()
        {
            _childBuffer.ForEach(s => this.AddChild(s));
            _childBuffer.Clear();
            this.IsEnabled = true;
        }
    }
}