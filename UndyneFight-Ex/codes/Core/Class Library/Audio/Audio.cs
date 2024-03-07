using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media; 
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

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
            bool OnPlay { get; }
            float Volume { set; }

            void Resume();
            void Pause();
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

            public void Resume()
            {
                effect.Resume();
            }

            public void Pause()
            {
                effect.Pause();
            }

            public bool IsEnd => effect.State == SoundState.Stopped;
            public bool OnPlay => effect.State == SoundState.Playing;

            public float Volume { set => effect.Volume = value; }
        }
        private class DynamicSongPlayer : IAudioSource
        {
            public DynamicSongPlayer(string path)
            {
                _dynamicSong = new(path);
            }
            DynamicSong _dynamicSong;
            List<DynamicSongInstance> allInstances = new();

            private void Update() { 
                this.allInstances.RemoveAll(s => s.State == SoundState.Stopped); 
            }

            public bool IsEnd { get { this.Update(); return allInstances.Count == 0; } }

            public bool OnPlay { get { this.Update(); return allInstances.Count > 0; } }

            public float Volume { set { this.allInstances[0].Volume = value; } }

            public TimeSpan GetDuration()
            {
                return new TimeSpan(0, 0, 0, 0, 0);
            }

            public void Start()
            {
                DynamicSongInstance currentInstance;
                allInstances.Add(currentInstance = _dynamicSong.CreateInstance());
                currentInstance.Play();
            }

            public void Stop()
            {
                this.allInstances.ForEach(s => { s.Stop(); });
                this.allInstances.Clear();
            }

            float lastPosition = 0.0f;
            internal float GetPosition()
            {
                if (this.allInstances.Count > 0)
                    lastPosition = this.allInstances[^1].GetPosition();
                return lastPosition;
            }

            internal void SetPosition(float position)
            {
                if (allInstances.Count == 0) return;
                else
                this.allInstances[0].SetPosition(position / 62.5f * 1000f);
            }

            public void Resume()
            {
                this.allInstances[0].Pause();
            }

            public void Pause()
            {
                this.allInstances[0].Resume();
            }
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
                float x = Settings.SettingsManager.DataLibrary.masterVolume / 100f;
                MediaPlayer.Volume = x * x;
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

            public void Resume()
            {
                MediaPlayer.Resume();
            }

            public void Pause()
            {
                MediaPlayer.Pause();
            }

            public bool IsEnd => MediaPlayer.State == MediaState.Stopped;
            public bool OnPlay => MediaPlayer.State == MediaState.Playing;

            public float Volume { set => MediaPlayer.Volume = value * Settings.SettingsManager.DataLibrary.masterVolume / 100f; }
        }
        IAudioSource source;
        public float Volume { set => source.Volume = value; }
        public Audio(string path) : this(path, Fight.Functions.Loader) {  }
        public Audio(string path, ContentManager loader)
        {
            if (path.EndsWith(".ogg"))
            {
                source = new DynamicSongPlayer(string.IsNullOrEmpty(loader.RootDirectory) ? path : loader.RootDirectory + "\\" + path);
                return;
            }
            object result = loader.Load<object>(path);
            if (result is SoundEffect) source = new EffectPlayer(result as SoundEffect);
            else if (result is Song) source = new SongPlayer(result as Song);
            source.Volume = 1f;
        }
        public Audio(SoundEffect effect)
        {
            source = new EffectPlayer(effect); source.Volume = 1f;
        }
        public Audio(Song song)
        {
            source = new SongPlayer(song); source.Volume = 1f;
        } 
        public bool OnPlay => source.OnPlay;
        public float PlayPosition { private get; set; }
        public TimeSpan SongDuration => source.GetDuration();
        public void Play()
        {
            if (MathF.Abs(PlayPosition) > 0.01f) {
                if (source is SongPlayer)
                    (source as SongPlayer).SetPosition(PlayPosition); 
            }
            source.Start();
            if (MathF.Abs(PlayPosition) > 0.01f) { 
                if (source is DynamicSongPlayer) 
                    (source as DynamicSongPlayer).SetPosition(PlayPosition); 
            }
        }
        public void Stop()
        {
            source.Stop();
        }
        public bool IsEnd => source.IsEnd;

        public bool TrySetPosition(float position)
        {
            if(source is DynamicSongPlayer)
            {
                (source as DynamicSongPlayer).SetPosition(position);
                return true;
            }
            return false;
        }
        public float TryGetPosition(out bool result)
        {
            if (source is SongPlayer)
            {
                result = true;
                return (float)(MediaPlayer.PlayPosition.TotalMilliseconds * 62.5 / 1000);
            }
            else if(source is DynamicSongPlayer)
            {
                result = true;
                return (source as DynamicSongPlayer).GetPosition() / 1000f * 62.5f;
            }
            else result = false;
            return -1;
        }

        internal void Resume()
        { 
            source.Resume();
        }

        internal void Pause()
        { 
            source.Pause();
        }
    }
}