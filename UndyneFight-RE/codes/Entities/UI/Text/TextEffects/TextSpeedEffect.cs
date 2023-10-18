namespace UndyneFight_Ex.Remake.Texts
{
    public class TextSpeedEffect : TextEffect
    {
        public TextSpeedEffect(float textTime) : base(1) { _textTime = textTime; }

        private float _textTime;

        protected override void Update(TextSetting textSetting)
        {
            textSetting.TextTime = _textTime;
        }
    }
}