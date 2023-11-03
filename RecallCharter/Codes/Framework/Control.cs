using Microsoft.Xna.Framework;
using UndyneFight_Ex;
using UndyneFight_Ex.Remake;

namespace RecallCharter
{
    public class Control : AutoEntity
    {
        public Control() { ChildrenUpdateFirst = true; UpdateIn120 = true; }

        protected bool EnableWhenFocused { private get; set; } = true;

        public bool IsEnabled { get; protected set; }
        protected bool IsFocused { get; private set; }
        public bool ForceFocus { private get; init; } = false;

        protected Control FocusedKid { get; private set; }

        public bool MouseOn { get; private set; }
        public bool ClickToFocus { get; protected set; } = false;

        protected Control Father { get; private set; }

        public override void Start()
        {
            this.Father = FatherObject as Control;
            base.Start();
        }

        Vector2 _relatedPosition = Vector2.Zero;
        public bool _posUpdated = false;

        private bool _deattaching = false;
        public void Deattach()
        {
            _deattaching = true;
        }
        public void Attach()
        {
            this.IsFocused = this.IsEnabled = true;
        }

        public override void Update()
        {
            if (_deattaching)
            {
                Father.Deattach();
                this.IsEnabled = false;
                this.IsFocused = false;
                this.MouseOn = false;
                _deattaching = false;
                return;
            }
            if (!Father.IsEnabled)
            {
                return;
            }
            if (!_posUpdated)
            {
                _posUpdated = true;
                this._relatedPosition = this.collidingBox.TopLeft;
            }
            this.collidingBox.TopLeft = this.Father.collidingBox.TopLeft + _relatedPosition;
            this.MouseOn = this.CollidingBox.Contain(MouseSystem.TransferredPosition);
            if (this.FocusedKid != null && (!FocusedKid.IsFocused))
            {
                this.FocusedKid = null;
                foreach (Control ctr in this.ChildObjects)
                {
                    if (ctr.IsFocused)
                    {
                        FocusedKid = ctr;
                        break;
                    }
                };
            }
            if (this.FocusedKid != null || this.ForceFocus)
            {
                this.IsFocused = true;
            }
            else if (this.MouseOn && (!ClickToFocus || MouseSystem.IsLeftClick()))
            {
                this.IsFocused = true;
                if (this.FocusedKid == null)
                    MasterControl.FocusControl = this;
            }
            else if (!ClickToFocus || MouseSystem.IsLeftClick()) this.IsFocused = false;
            if (this.IsFocused) this.Father.FocusedKid = this;
            if (this.EnableWhenFocused) { this.IsEnabled = this.IsFocused; }
        }
    }
}