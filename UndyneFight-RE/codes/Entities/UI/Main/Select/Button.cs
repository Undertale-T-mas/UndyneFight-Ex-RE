﻿using Microsoft.Xna.Framework;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.FightResources.Sounds;

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

            public bool CentreDraw { get; internal set; }

            public override void Draw()
            {
                if (CentreDraw)
                    NormalFont.CentreDraw(_text, Centre, Color.LimeGreen * alpha, Scale, 0.44f);
                else
                    NormalFont.Draw(_text, Centre, Color.LimeGreen * alpha, Scale, 0.44f);
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
                fontSize = NormalFont.SFX.MeasureString(text) * 1.331f;
                this._centre = centre;
                this._text = text;
                UpdateIn120 = true;

                this.MouseOn += MouseOnEvent;
                this.LeftClick += MouseClick;
            }
            string _text;

            protected Vector2 fontSize, _centre;
            public Vector2 PositionDelta { get; protected set; } = Vector2.Zero;

            public float DefaultScale { private get; set; } = 1.4f;
            protected float SelectedScale { private get; set; } = 1.16f;

            public bool CentreDraw { get; set; } = true;

            public override void Draw()
            {
                if (!this._father.DrawEnabled) return;
                if (CentreDraw)
                    NormalFont.CentreDraw(_text, _realLocation, _drawingColor, sizeScale * DefaultScale, 0.4f);
                else
                    NormalFont.Draw(_text, _realLocation, _drawingColor, sizeScale * DefaultScale, 0.4f);
            }
            float sizeScale = 1.0f;
            public override void Update()
            {
                base.Update();
                if (!this._father.Activated) return;
                this.collidingBox.Size = fontSize;
                this._realLocation = _centre + PositionDelta;
                if (!CentreDraw) this._realLocation -= new Vector2(0, fontSize.Y * (sizeScale - 1) * DefaultScale / 2f);
                this.Centre = _realLocation;
                if (!CentreDraw) this.Centre += this.collidingBox.Size / 2f;

                if (_mouseOn) this.sizeScale = MathHelper.Lerp(sizeScale, SelectedScale, 0.1f);
                else this.sizeScale = MathHelper.Lerp(sizeScale, 1.0f, 0.1f);
            }
            Vector2 _realLocation;

            private void MouseClick()
            {
                PlaySound(select);
                GameStates.InstanceCreate(new Shade(this._realLocation, sizeScale * this.DefaultScale, _text) { CentreDraw = this.CentreDraw});
            }

            private void MouseOnEvent()
            {
                PlaySound(changeSelection);
            }
        }
    }
}