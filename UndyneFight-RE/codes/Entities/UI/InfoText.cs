using Microsoft.Xna.Framework;
using static UndyneFight_Ex.FightResources.Font;

namespace UndyneFight_Ex.Remake.UI
{
    internal class InfoText : Entity
    {
        private string _text;
        private Vector2 _originPosition;

        public Vector2 PositionDelta { private get; set; } = Vector2.Zero;

        public InfoText(string text, Vector2 position)
        {
            UpdateIn120 = true;
            this._text = text;
            this._originPosition = position;
        }

        private float _appearTime = 0.0f;
        private float _alpha = 1.0f;

        public float ExistTime { private get; set; } = 60f;

        public float FadeSpeed { private get; set; } = 1.0f;
        public Color DrawingColor { private get; set; } = Color.White;

        public override void Draw()
        {
            NormalFont.CentreDraw(_text, Centre, DrawingColor * _alpha, Scale, 0.999f);
        }

        public override void Update()
        {
            if (_appearTime > ExistTime)
            {
                this._alpha -= FadeSpeed * 0.01f;
                if (_alpha < 0.0f) this.Dispose();
            }

            this.Centre = _originPosition + PositionDelta;

            this._appearTime += 0.5f;
        }
    }
}