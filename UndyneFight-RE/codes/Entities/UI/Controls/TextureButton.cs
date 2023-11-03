using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.Fight.Functions;

namespace UndyneFight_Ex.Remake.UI
{
    class ShadeTexture : Entity
    {
        public ShadeTexture(Vector2 centre, float scale, Texture2D image)
        {
            this.Centre = centre;
            this._scale = scale;
            this.Image = image;

            UpdateIn120 = true;
        }
        float _scale2 = 1.0f, _scale;
        string _text;
        float alpha = 1.0f;

        public bool CentreDraw { get; internal set; }

        public override void Draw()
        {
            FormalDraw(Image, Centre, Color.LimeGreen * alpha, Scale, 0, CentreDraw ? ImageCentre : new(0, 0));
        }

        public override void Update()
        {
            alpha -= 0.035f;
            _scale2 += 0.04f;
            this.Scale = this._scale * this._scale2;
        }
    }
    class TextureButton : SelectingModule
    {
        public TextureButton(ISelectChunk father, Vector2 centre, Texture2D tex) : base(father)
        {
            if (tex != null)
                textureSize = tex.Bounds.Size.ToVector2() * DefaultScale;
            this._centre = centre;
            this.Image = tex;
            UpdateIn120 = true;

            this.MouseOn += MouseOnEvent;
            this.LeftClick += MouseClick;
        } 
        public override void Start()
        {
            if (Image != null)
                textureSize = this.Image.Bounds.Size.ToVector2() * DefaultScale;
            base.Start();
        } 

        protected Vector2 textureSize, _centre;
        public Vector2 PositionDelta { get; protected set; } = Vector2.Zero;

        public float DefaultScale { private get; set; } = 1.0f;
        protected float SelectedScale { private get; set; } = 1.16f;

        public bool CentreDraw { get; set; } = true;

        public override void Draw()
        {
            if (!this._father.DrawEnabled) return;
            this.Depth = 0.4f;
            if (CentreDraw)
                this.FormalDraw(Image, _realLocation, _drawingColor, sizeScale * DefaultScale, 0, ImageCentre);
            else
                this.FormalDraw(Image, _realLocation, _drawingColor, sizeScale * DefaultScale, 0, Vector2.Zero);
        }
        float sizeScale = 1.0f;
        protected float CurrentScaleFactor { set => sizeScale = value; }
        public override void Update()
        {
            base.Update();

            sizeScale = MathHelper.Lerp(sizeScale, _mouseOn ? SelectedScale : 1, 0.1f);

            if (!this._father.Activated) return;
            this.collidingBox.Size = textureSize;
            this._realLocation = _centre + PositionDelta;
            if (!CentreDraw) this._realLocation -= new Vector2(0, textureSize.Y * (sizeScale - 1) / 2f);
            this.Centre = _realLocation;
            if (!CentreDraw) this.Centre += this.collidingBox.Size / 2f;
        }
        Vector2 _realLocation;

        private void MouseClick()
        {
            PlaySound(FightResources.Sounds.select);
            GameStates.InstanceCreate(new ShadeTexture(this._realLocation, sizeScale * this.DefaultScale, this.Image) { CentreDraw = this.CentreDraw });
        }

        private void MouseOnEvent()
        {
            PlaySound(FightResources.Sounds.changeSelection);
        }
    }
}
