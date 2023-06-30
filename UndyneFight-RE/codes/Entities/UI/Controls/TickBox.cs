using Microsoft.Xna.Framework;
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Remake.UI
{ 
    class TickBox : SelectingModule
    {
        public TickBox(ISelectChunk father, Vector2 centre, string text) : base(father)
        {
            fontSize = FightResources.Font.NormalFont.SFX.MeasureString(text) * new Vector2(1.331f, 2.4f);
            this._centre = centre;
            this._text = text;
            UpdateIn120 = true;
            NeverEnable = true;

            this.MouseOn += MouseOnEvent;
            this.LeftClick += MouseClick;
        }
        string _text;
        protected void ChangeText(string text)
        {
            _text = text;
            fontSize = FightResources.Font.NormalFont.SFX.MeasureString(text) * 1.331f;
        }

        protected Vector2 fontSize, _centre;
        public Vector2 PositionDelta { get; protected set; } = Vector2.Zero;

        public float DefaultScale { private get; set; } = 1.4f;
        protected float SelectedScale { private get; set; } = 1.1f;

        public bool CentreDraw { get; set; } = true;

        private bool _enabled = false;
        public bool Enabled => _enabled;

        public override void Draw()
        {
            if (!this._father.DrawEnabled) return;
            if (CentreDraw)
                FightResources.Font.NormalFont.CentreDraw(_text, _realLocation, _drawingColor, sizeScale * DefaultScale, 0.4f);
            else
                FightResources.Font.NormalFont.Draw(_text, _realLocation, _drawingColor, sizeScale * DefaultScale, 0.4f);
        }
        float sizeScale = 1.0f;
        public override void Update()
        {
            base.Update();

            if (_mouseOn) this.sizeScale = MathHelper.Lerp(sizeScale, SelectedScale, 0.1f);
            else this.sizeScale = MathHelper.Lerp(sizeScale, 1.0f, 0.1f);

            if (!this._father.Activated) return;
            this.collidingBox.Size = fontSize;
            this._realLocation = _centre + PositionDelta;
            if (!CentreDraw) this._realLocation -= new Vector2(0, fontSize.Y * (sizeScale - 1) * DefaultScale / 2f);
            this.Centre = _realLocation;
            if (!CentreDraw) this.Centre += this.collidingBox.Size / 2f;
        }
        Vector2 _realLocation;

        private void MouseClick()
        {
            Functions.PlaySound(FightResources.Sounds.select);
            GameStates.InstanceCreate(new Shade(this._realLocation, sizeScale * this.DefaultScale, _text) { CentreDraw = this.CentreDraw });
        }

        private void MouseOnEvent()
        {
            Functions.PlaySound(FightResources.Sounds.changeSelection);
        }
    }
}
