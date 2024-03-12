using System;
using Microsoft.Xna.Framework.Audio;

namespace UndyneFight_Ex
{
    public class DynamicSongInstance
    {
        // Properties

        // Public

        public float Volume
        {
            get => dynamicSound.Volume;
            set => dynamicSound.Volume = Math.Min(Math.Max(0, value), 1);
        }

        public float Pitch
        {
            get => dynamicSound.Pitch;
            set => dynamicSound.Pitch = Math.Min(Math.Max(-1, value), 1);
        }

        // Private

        private float originalVolume;
        private DynamicSoundEffectInstance dynamicSound;
        private byte[] byteArray;
        private int position;
        private int count;
        private int loopLengthBytes;
        private int loopEndBytes;
        private float bytesOverMilliseconds;

        // Methods

        // Public

        public DynamicSongInstance(DynamicSoundEffectInstance dynamicSound, byte[] byteArray, int count, int loopLengthBytes, int loopEndBytes, float bytesOverMilliseconds)
        {
            this.dynamicSound = dynamicSound;
            this.byteArray = byteArray;
            this.count = count;
            this.loopLengthBytes = loopLengthBytes;
            this.loopEndBytes = loopEndBytes;
            this.bytesOverMilliseconds = bytesOverMilliseconds;

            this.dynamicSound.BufferNeeded += new EventHandler<EventArgs>(UpdateBuffer);
        }

        public SoundState State => this.dynamicSound.IsDisposed ? SoundState.Stopped : this.dynamicSound.State;

        public void Play()
        {
            dynamicSound.Pitch = 0; 
            dynamicSound.Play();
        }

        public void Pause()
        {
            if (dynamicSound != null)
                dynamicSound.Stop();
        }

        public void Stop()
        {
            if (dynamicSound != null)
                dynamicSound.Stop();
            dynamicSound = null;
        }

        public void SetPosition(float milliseconds)
        {
            position = (int)Math.Floor(milliseconds * bytesOverMilliseconds);
            while (position % 8 != 0) position -= 1;
        }

        public float GetPosition()
        {
            return position / bytesOverMilliseconds;
        }

        // Private

        private void UpdateBuffer(object sender, EventArgs e)
        {
            if (!_enabled)
            {
                dynamicSound.Stop();
                dynamicSound.Dispose();
                return;
            }
            dynamicSound.SubmitBuffer(byteArray, position, count / 2);
            dynamicSound.SubmitBuffer(byteArray, position + count / 2, count / 2);
            position += count;

            if ((loopEndBytes > 0) && (loopLengthBytes > 0) && (position + count >= loopEndBytes))
            {
                position -= loopLengthBytes;
            }

            if (position + count > byteArray.Length)
            {
                if (IsLoop)
                    position = 0;
                else
                    this._enabled = false;
            }
        }

        internal void Resume()
        {
            this.dynamicSound.Resume();
        }

        private bool _enabled = true;
        public bool IsLoop { get; set; } = false;
    }
}