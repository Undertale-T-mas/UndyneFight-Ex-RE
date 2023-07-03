using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private partial class VirtualFather
        {
            private class AudioSetting : SettingChunk
            {
                public AudioSetting() : base("Audio", new Vector2(150, 100))
                {
                }

                public override void Apply()
                {
                    throw new System.NotImplementedException();
                }
            }
        }
    }
}