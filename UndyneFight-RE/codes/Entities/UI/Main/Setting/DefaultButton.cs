using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private partial class VirtualFather
        { 
            private partial class SettingChunk
            {
                private class ApplyButton : Button
                {
                    new SettingChunk _father;
                    public ApplyButton(SettingChunk father) : base(father, new Vector2(505, 490), "Apply")
                    {
                        this._father = father;
                        NeverEnable = true;
                        LeftClick += DoApply;
                    }

                    private void DoApply()
                    {
                        _father.Apply();
                        _father._virtualFather._keyEnabled = true;
                        _father.Deactivate();
                    }
                    public override void Update()
                    {
                        base.Update();
                        if (!_father.Activated) CurrentScaleFactor = 1.0f;
                    }
                }
                private class CancelButton : Button
                {
                    new SettingChunk _father;
                    public CancelButton(SettingChunk father) : base(father, new Vector2(510 + 150, 490), "Cancel")
                    {
                        this._father = father;
                        NeverEnable = true;
                        LeftClick += DoCancel;
                    }

                    private void DoCancel()
                    { 
                        _father._virtualFather._keyEnabled = true;
                        _father.Deactivate();
                    }
                    public override void Update()
                    {
                        base.Update();
                        if (!_father.Activated) CurrentScaleFactor = 1.0f;
                    }
                }
                private class ResetButton : Button
                {
                    new SettingChunk _father;
                    public ResetButton(SettingChunk father) : base(father, new Vector2(515 + 300, 490), "Reset")
                    {
                        this._father = father;
                        NeverEnable = true;
                        LeftClick += DoReset;
                        ColorNormal = Color.Orange;
                        ColorMouseOn = Color.Red;
                    }

                    private void DoReset()
                    {
                        _father.Reset();
                    }
                    public override void Update()
                    {
                        base.Update();
                        if (!_father.Activated) CurrentScaleFactor = 1.0f;
                    }
                }
            }
        }
    }
}