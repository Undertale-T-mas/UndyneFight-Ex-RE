using Microsoft.Xna.Framework;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Font;
using static Microsoft.Xna.Framework.MathHelper;

namespace UndyneFight_Ex.Remake.UI
{ 
    class TickBox : SelectingModule
    {
        public TickBox(ISelectChunk father, Vector2 centre, string text) : base(father)
        {
            this._centre = centre;
            this._text = text;
            UpdateIn120 = true;
            NeverEnable = true;

            this.MouseOn += MouseOnEvent;
            this.LeftClick += MouseClick;
        }
        public override void Start()
        {
            fontSize = NormalFont.SFX.MeasureString(_text) * DefaultScale + new Vector2(22, 0);
            base.Start();
        }
        string _text;
        protected void ChangeText(string text)
        {
            _text = text;
            fontSize = NormalFont.SFX.MeasureString(_text) * DefaultScale + new Vector2(22, 0);
        }
         
        protected Vector2 fontSize, _centre;
        public Vector2 PositionDelta { get; protected set; } = Vector2.Zero;

        public float DefaultScale { private get; set; } = 1.4f;
        protected float SelectedScale { private get; set; } = 1.1f;  
        public bool Ticked => _ticked;
        public bool DefaultValue { set { this._ticked = value; } }

        public override void Draw()
        {
            if (!this._father.DrawEnabled) return;
            NormalFont.CentreDraw(_text, _realLocation - new Vector2(35, 0), _drawingColor, DefaultScale, 0.4f);
            Vector2 pos2 = _realLocation + new Vector2(fontSize.X * 0.5f - 18, 0);
            CollideRect rect2 = new();
            rect2.Size = new(26, 26);
            rect2.SetCentre(pos2);

            DrawingLab.DrawRectangle(rect2, Color.Lerp(Color.White, Color.Gold, _colorScale), 3f, 0.1f);

            this.Image = Resources.UI.Tick;
            this.Depth = 0.2f;
            this.FormalDraw(this.Image, rect2.GetCentre() - new Vector2(0, 4), Color.White * _alphaScale, 0.0f, ImageCentre);
        }
        float _colorScale = 0.0f;
        float _alphaScale = 0.0f;
        public override void Update()
        {
            base.Update();

            _colorScale = Lerp(_colorScale, _mouseOn ? 1 : 0, 0.1f);

            if (!this._father.Activated) return;
            this.collidingBox.Size = fontSize;
            this._realLocation = _centre + PositionDelta; 
            this.Centre = _realLocation;

            _alphaScale = Lerp(_alphaScale, _ticked ? 1 : 0, 0.14f);
        }
        Vector2 _realLocation;

        private bool _ticked = false;

        private void MouseClick()
        {
            PlaySound(FightResources.Sounds.select); 
            this._ticked = !this._ticked;
        }

        private void MouseOnEvent()
        {
            PlaySound(FightResources.Sounds.changeSelection);
        }

        internal void Tick()
        {
            this._ticked = !_ticked;
        }
    }
}
