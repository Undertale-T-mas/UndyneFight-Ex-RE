using Microsoft.Xna.Framework;
using static UndyneFight_Ex.FightResources.Font;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private partial class VirtualFather
        {
            private abstract class SettingChunk : SmartSelector
            {
                public abstract void Apply();
                
                public SettingChunk(string name, Vector2 centre)
                {
                    UpdateIn120 = true;
                    this.collidingBox.Size = NormalFont.SFX.MeasureString(name) * 1.05f;
                    this.Centre = centre;
                    this.OnActivated += () => _virtualFather?.Select(this);
                }
                VirtualFather _virtualFather;
                public override void Start()
                {
                    this._virtualFather = FatherObject as VirtualFather;
                    base.Start();
                }
                public override void Draw()
                {

                }
            }
        }
    }
}