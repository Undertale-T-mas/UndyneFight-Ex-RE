using Microsoft.Xna.Framework;
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private class Shade : Entity
        {
            public Shade(Vector2 centre, float scale, string text) {
                this.Centre = centre;
                this._scale = scale;
                this._text = text;

                UpdateIn120 = true;
            }
            float _scale2 = 1.0f, _scale;
            string _text;
            float alpha = 1.0f;
            public override void Draw()
            {
                FightResources.Font.NormalFont.CentreDraw(_text, Centre, Color.LimeGreen * alpha, Scale, 0.44f);
            }

            public override void Update()
            {
                alpha -= 0.035f;
                _scale2 += 0.04f;
                this.Scale = this._scale * this._scale2;
            }
        }
        private class Button : SelectingModule
        {
            public Button(ISelectChunk father, Vector2 centre, string text) : base(father)
            {
                fontSize = FightResources.Font.NormalFont.SFX.MeasureString(text) * 1.331f;
                this._centre = centre;
                this._text = text;
                UpdateIn120 = true;

                this.MouseOn += MouseOnEvent;
                this.LeftClick += MouseClick;
            }
            string _text;

            private Vector2 fontSize, _centre;

            public float DefaultScale { private get; set; } = 1.4f;

            public override void Draw()
            {
                if (!this._father.DrawEnabled) return;
                FightResources.Font.NormalFont.CentreDraw(_text, Centre, _drawingColor, sizeScale * DefaultScale, 0.4f);
            }
            float sizeScale = 1.0f;
            public override void Update()
            {
                base.Update();
                if (!this._father.Activated) return;
                this.collidingBox.Size = fontSize;
                this.Centre = _centre;

                if (_mouseOn) this.sizeScale = MathHelper.Lerp(sizeScale, 1.16f, 0.1f);
                else this.sizeScale = MathHelper.Lerp(sizeScale, 1.0f, 0.1f);
            }

            private void MouseClick()
            {
                Functions.PlaySound(FightResources.Sounds.select);
                GameStates.InstanceCreate(new Shade(_centre, sizeScale * this.DefaultScale, _text));
            }

            private void MouseOnEvent()
            {
                Functions.PlaySound(FightResources.Sounds.changeSelection);
            }
        }
    }
}
