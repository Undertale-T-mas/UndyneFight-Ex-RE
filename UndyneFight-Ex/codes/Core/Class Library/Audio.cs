using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;

namespace UndyneFight_Ex
{
    public class Audio
    {
        private interface IAudioSource
        {
            void Start();
            void Stop();
            TimeSpan GetDuration();
            bool IsEnd { get; }
        }
        private class EffectPlayer : IAudioSource
        {
            TimeSpan duration;
            SoundEffectInstance effect;
            public EffectPlayer(SoundEffect effect)
            {
                duration = effect.Duration;
                this.effect = effect.CreateInstance();
            }

            public TimeSpan GetDuration()
            {
                return duration;
            }

            public void Start()
            {
                effect.Play();
            }

            public void Stop()
            {
                effect.Stop();
            }
            public bool IsEnd => effect.State == SoundState.Stopped;
        }
        private class SongPlayer : IAudioSource
        {
            Song song;
            public SongPlayer(Song song)
            {
                this.song = song;
            }
            public void Start()
            {
                if (position != TimeSpan.Zero)
                    MediaPlayer.Play(song, position);
                else MediaPlayer.Play(song);
            }

            public void Stop()
            {
                MediaPlayer.Stop();
            }
            TimeSpan position = TimeSpan.Zero;
            public void SetPosition(float position)
            {
                position /= 62.5f;
                int sec = (int)position;
                int mil = (int)((position - sec) * 1000);
                this.position = new TimeSpan(0, 0, 0, sec, mil);
            }

            public TimeSpan GetDuration()
            {
                return song.Duration;
            }
            public bool IsEnd => MediaPlayer.State == MediaState.Stopped;
        }
        IAudioSource source;
        public Audio(string path) : this(path, Fight.Functions.Loader) { }
        public Audio(string path, ContentManager loader)
        {
            object result = loader.Load<object>(path);
            if (result is SoundEffect) source = new EffectPlayer(result as SoundEffect);
            else if (result is Song) source = new SongPlayer(result as Song);
        }
        public Audio(SoundEffect effect)
        {
            source = new EffectPlayer(effect);
        }
        public Audio(Song song)
        {
            source = new SongPlayer(song);
        }
        public float PlayPosition { private get; set; }
        public TimeSpan SongDuration => source.GetDuration();
        public void Play()
        {
            if (MathF.Abs(PlayPosition) > 0.01f)
                (source as SongPlayer)?.SetPosition(PlayPosition);
            source.Start();
        }
        public void Stop()
        {
            source.Stop();
        }
        public bool IsEnd => source.IsEnd;

        public float TryGetPosition(out bool result)
        {
            if (source is SongPlayer)
            {
                result = true;
                return (float)(MediaPlayer.PlayPosition.TotalMilliseconds * 62.5 / 1000);
            }
            else result = false;
            return -1;
        }
    }
}