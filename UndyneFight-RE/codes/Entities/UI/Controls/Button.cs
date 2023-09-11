using Microsoft.Xna.Framework;
using UndyneFight_Ex.Fight;
using static System.Net.Mime.MediaTypeNames;

namespace UndyneFight_Ex.Remake.UI
{
    class Shade : Entity
    {
        public Shade(Vector2 centre, float scale, string text)
        {
            this.Centre = centre;
            this._scale = scale;
            this._text = text;

            UpdateIn120 = true;
        }
        float _scale2 = 1.0f, _scale;
        string _text;
        float alpha = 1.0f;

        public bool CentreDraw { get; internal set; }

        public override void Draw()
        {
            if (CentreDraw)
                FightResources.Font.NormalFont.CentreDraw(_text, Centre, Color.LimeGreen * alpha, Scale, Depth);
            else
                FightResources.Font.NormalFont.Draw(_text, Centre, Color.LimeGreen * alpha, Scale, Depth);
        }

        public override void Update()
        {
            alpha -= 0.035f;
            _scale2 += 0.04f;
            this.Scale = this._scale * this._scale2;
        }
    }
    class Button : SelectingModule
    {
        public Button(ISelectChunk father, Vector2 centre, string text) : base(father)
        {
            fontSize = Font.SFX.MeasureString(text) * DefaultScale;
            this._centre = centre;
            this._text = text;
            UpdateIn120 = true; Depth = 0.4f;

            this.MouseOn += MouseOnEvent;
            this.LeftClick += MouseClick;
        }
        public void ResetPosition(Vector2 position)
        {
            this._centre = position;
            this._realLocation = _centre + PositionDelta;
        }
        string _text;
        public override void Start()
        {
            fontSize = Font.SFX.MeasureString(_text) * DefaultScale;
            base.Start();
        }
        public void ChangeText(string text)
        {
            _text = text;
            fontSize = Font.SFX.MeasureString(text) * DefaultScale * 1.04f;
        }

        protected Vector2 fontSize, _centre;
        public Vector2 PositionDelta { get; set; } = Vector2.Zero;

        public float DefaultScale { private get; set; } = 1.4f;
        protected float SelectedScale { private get; set; } = 1.16f;

        public bool CentreDraw { get; set; } = true;

        public override void Draw()
        {
            if (!this._father.DrawEnabled) return;
            if (CentreDraw)
                Font.CentreDraw(_text, _realLocation, _drawingColor, sizeScale * DefaultScale, this.Depth);
            else
                Font.Draw(_text, _realLocation, _drawingColor, sizeScale * DefaultScale, this.Depth);
        }
        float sizeScale = 1.0f;
        protected float CurrentScaleFactor { set => sizeScale = value; }
        public GLFont Font { get; internal set; } = FightResources.Font.NormalFont;
        public string Text => this._text;

        public override void Update()
        {
            base.Update();

            if (_mouseOn) this.sizeScale = MathHelper.Lerp(sizeScale, SelectedScale, 0.1f);
            else this.sizeScale = MathHelper.Lerp(sizeScale, 1.0f, 0.1f);
             
            this.collidingBox.Size = fontSize;
            this._realLocation = _centre + PositionDelta;
            if (!CentreDraw) this._realLocation -= new Vector2(0, fontSize.Y * (sizeScale - 1) / 2f);
            this.Centre = _realLocation;
            if (!CentreDraw) this.Centre += this.collidingBox.Size / 2f;
        }
        Vector2 _realLocation;

        private void MouseClick()
        {
            Functions.PlaySound(FightResources.Sounds.select);
            GameStates.InstanceCreate(new Shade(this._realLocation, sizeScale * this.DefaultScale, _text) { CentreDraw = this.CentreDraw, Depth = this.Depth + 0.01f });
        }

        private void MouseOnEvent()
        {
            Functions.PlaySound(FightResources.Sounds.changeSelection);
        }
    }
}
