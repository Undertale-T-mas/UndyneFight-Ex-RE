using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace UndyneFight_Ex.Remake.Components
{
    /// <summary>
    /// Receivable events: MusicFadeOut 
    /// </summary>
    public class SmartMusicPlayer : GameObject
    {
        public SmartMusicPlayer() { UpdateIn120 = true; CrossScene = true; }
        private struct PeriodData
        {
            public PeriodData(MusicPlayer player, float changeTime, bool isLoop)
            {
                Player = player;
                ChangeTime = changeTime;
                IsLoop = isLoop;
            }

            public MusicPlayer Player { get; set; }
            public float ChangeTime { get; set; }
            public bool IsLoop { get; set; }
        }
        public void InsertPeriod(MusicPlayer player, float changeTime, bool isLoop = false)
        {
            periods.Add(new(player, changeTime, isLoop));
            player.IsLoop = false;
        }
        public void Play()
        {
            currentIndex = 0;
            currentPeriod = periods[0];
            currentPeriod.Player.Play();
        }
        List<PeriodData> periods = new List<PeriodData>();
        PeriodData currentPeriod;
        int currentIndex = -1;

        bool _inFadeOut = false;
        public override void Update()
        {
            var list = GameStates.DetectEvent("MusicFadeOut");
            if(list != null && list.Count > 0)
            {
                _inFadeOut = true;
                list[0].Dispose();
                currentIndex = -1;
                currentPeriod.Player.FadeOut();
            }
            if (currentPeriod.Player == null) return;
            currentPeriod.Player.Update();
            if (_inFadeOut && currentPeriod.Player.Disposed) this.Dispose();
            if (currentIndex == -1) return;
            if(currentPeriod.Player.PlayTime >= currentPeriod.ChangeTime)
            {
                if (!currentPeriod.IsLoop)
                    currentIndex++;
                currentPeriod = periods[currentIndex];
                currentPeriod.Player.Play();
            }
        }
        public void Stop()
        {
            currentIndex = -1;
            currentPeriod.Player.Stop();
        }
        public void FadeOut()
        {
            currentIndex = -1;
            currentPeriod.Player.FadeOut();
        }
    }
    /// <summary>
    /// Receivable events: MusicFadeOut 
    /// </summary>
    public class MusicPlayer : GameObject
    {
        Audio _audio;
        public MusicPlayer(Audio audio)
        {
            this._audio = audio;
            UpdateIn120 = true;
            CrossScene = true;
        }
        public void Play()
        {
            PlayTime = 0;
            _audio.Play();
        }
        public float TruePlayTime { get; set; } = -1;
        public override void Update()
        {
            if(this._audio.IsEnd) { return; }
            var list = GameStates.DetectEvent("MusicFadeOut");
            if (list != null && list.Count > 0) { 
                this.FadeOut(); list[0].Dispose(); }
            if (isFadingOut) {
                this.fadeOutScale -= 0.02f;
                _audio.Volume = this.fadeOutScale;
                if(this.fadeOutScale <= 0) { this.Stop(); this.Dispose(); }
            }
            if (IsLoop) {
                if (_audio.IsEnd)
                    _audio.Play();
            }
            if (_audio.OnPlay)
                PlayTime += 0.5f;

            bool timeAccessible;
            if (!_audio.IsEnd)
            {
                float trueTime = _audio.TryGetPosition(out timeAccessible);
                if (timeAccessible)
                {
                    TruePlayTime = trueTime;
                }
            }
            if(FadeIn && PlayTime < IntroTime)
            {
                this._audio.Volume = MathHelper.Lerp(IntroVolume, 1, PlayTime / IntroTime);
            }
            else if(PlayTime >= IntroTime && PlayTime < IntroTime + 0.5f) _audio.Volume = 1;
        }
        public float PlayTime { get; private set; }
        private bool isFadingOut = false;
        float fadeOutScale = 1.0f;
        public bool IsLoop { get; set; } = true;
        public bool FadeIn { get; set; } = false;
        public float IntroVolume { get; set; } = 0.0f;
        public float IntroTime { get; set; } = 30.0f;

     public   void FadeOut()
        {
            this.IsLoop = false;
            this.isFadingOut= true;
            this._audio.Volume = fadeOutScale;
        }
      public  void Stop()
        {
            this.IsLoop = false;
            this._audio.Stop();
            this.Dispose();
        }
        
    }
}