using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RecallCharter
{
    internal abstract class CharterObject
    {
        private static GameMain Instance => GameMain.instance;
        protected Vector2 ToUVPosition(Vector2 origin)
        {
            return (origin - Instance.RenderPosition) / Instance.TrueSize;
        }
    }
    internal class MouseData : CharterObject, IUpdate
    {
        public void Update()
        {
            MouseState state = Mouse.GetState();
            GlobalPosition = ToUVPosition(state.Position.ToVector2());
        }
        /// <summary>
        /// A vector2 indicates the position of the mouse. From (0, 0) for TopLeft to (1, 1) for BottomRight
        /// </summary>
        public Vector2 GlobalPosition { get; private set; }
    }
    internal abstract class Control : CharterObject, IUpdate
    {
        public List<Control> ChildrenControl = new();
        protected Control Father { get; private set; }
        public Vector2 RelativePosition { get; private set; }

        public void Update()
        {
        }
        private bool isStarted = false;
        protected abstract void Start();
        protected void TreeUpdate()
        {
            Update();
            ChildrenControl.ForEach(s =>
            {
                if (!s.isStarted) { s.isStarted = true; s.Start(); }
                s.TreeUpdate();
            });
        }
    }
    interface IUpdate
    {
        void Update();
    }
    interface IMouseClick
    {
        void Click(MouseData data);
        void MouseOn();
    }
}