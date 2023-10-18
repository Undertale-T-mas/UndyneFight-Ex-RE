using Microsoft.Xna.Framework;
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.GameStates;
using System.Collections.Generic;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private partial class VirtualFather
        {
            private abstract partial class SettingChunk : SmartSelector
            {
                public abstract void Apply();
                public virtual void Reset() { }

                public SettingChunk(string name, float centreY)
                {
                    UpdateIn120 = true;
                    this.collidingBox.Size = NormalFont.SFX.MeasureString(name) * 1.05f;
                    this.Centre = new Vector2(210, centreY);
                    this.OnActivated += () => { _virtualFather?.Select(this);
                        PlaySound(select);
                        if (all != null)
                        {
                            if (currentFocus != all[0] && currentFocus.IsMouseOn)
                            {
                                currentFocus.CheckMouse();
                                currentFocus.State = SelectState.False;
                            }
                            this.currentFocus = all[0];
                            currentFocus.OnFocus();
                        }
                        this.focusID = 0; 
                    };
                    this._name = name;
                    this.KeyEvent = () => {
                        if (_virtualFather._keyEnabled) return;
                        if (IsKeyPressed120f(InputIdentity.MainLeft))
                        {
                            if (bottomButton.ContainsKey(currentFocus))
                            {
                                int value = bottomButton[currentFocus];
                                value--;
                                currentFocus.OffFocus();
                                all[value].OnFocus();
                            }
                        }
                        if (IsKeyPressed120f(InputIdentity.MainRight))
                        {
                            if (bottomButton.ContainsKey(currentFocus))
                            {
                                int value = bottomButton[currentFocus];
                                value++;
                                if (all.Length <= value) return;
                                currentFocus.OffFocus();
                                all[value].OnFocus();
                            }
                        }
                        if (IsKeyPressed120f(InputIdentity.MainDown))
                        {
                            int id = FocusID;
                            float x = all[id].Centre.X;
                            bool isLeft = x < 640f;
                            for (int i = id + 1; i < all.Length; i++)
                            {
                                float x2 = all[i].Centre.X;
                                bool isLeft2 = x2 < 640f;

                                if (isLeft == isLeft2)
                                {
                                    currentFocus.OffFocus();
                                    all[i].OnFocus();
                                    break;
                                }
                            }
                        }
                        else if (IsKeyPressed120f(InputIdentity.MainUp))
                        {
                            int id = FocusID;
                            float x = all[id].Centre.X;
                            bool isLeft = x < 640f;
                            for (int i = id - 1; i >= 0; i--)
                            {
                                if (i >= all.Length - 2) continue;
                                float x2 = all[i].Centre.X;
                                bool isLeft2 = x2 < 640f;

                                if (isLeft == isLeft2)
                                {
                                    currentFocus.OffFocus();
                                    all[i].OnFocus();
                                    break;
                                }
                            }
                        }
                        if (IsKeyPressed120f(InputIdentity.Confirm))
                        {
                            currentFocus?.ConfirmKeyDown();
                        }
                    };
                }
                string _name;
                VirtualFather _virtualFather;
                ApplyButton _applyButton;
                CancelButton _cancelButton;
                ResetButton _resetButton;
                SelectingModule[] bottomList;
                Dictionary<SelectingModule, int> bottomButton;
                public override void Start()
                {
                    this._virtualFather = FatherObject as VirtualFather;
                    this.AddChild(_applyButton = new ApplyButton(this));
                    this.AddChild(_cancelButton = new CancelButton(this));
                    this.AddChild(_resetButton = new ResetButton(this));
                    bottomList = new Button[] { _applyButton, _cancelButton, _resetButton };
                    bottomButton = new Dictionary<SelectingModule, int>(new KeyValuePair<SelectingModule, int>[] { 
                        new(_applyButton, ChildObjects.Count - 3),
                        new(_cancelButton, ChildObjects.Count - 2),
                        new(_resetButton, ChildObjects.Count - 1)
                    });
                    base.Start();
                }
                public override void Draw()
                { 
                    float l1 = 50, r1 = 320;

                    DrawLine(new(l1 + 40, Centre.Y + 9), new(l1 + 60, Centre.Y + 29), Color.White);
                    DrawLine(new(l1 + 60, Centre.Y + 29), new(r1, Centre.Y + 29), Color.White);

                    NormalFont.CentreDraw(this._name, this.Centre, DrawingColor, 1.48f * _secondaryScale, 0.1f);
                }
                float _secondaryScale = 1.0f;
                public float SecondaryScale { private get; set; } = 1.15f;
                bool _mouseOn = false;
                public void KeyOn()
                {
                    this._mouseOn = true;
                    if (this.State == SelectState.False) this.State = SelectState.MouseOn;
                    this._virtualFather.OnFocus(this);
                }
                public void KeyOff()
                {
                    if (State == SelectState.MouseOn) this.State = SelectState.False;
                    this._mouseOn = false;
                }
                public override void Update()
                {
                    base.Update();
                    _secondaryScale = MathHelper.Lerp(_secondaryScale, _mouseOn ? SecondaryScale : 1, 0.1f);
                    if (!MouseSystem.Moved) { return; }
                    if (this.collidingBox.Contain(MouseSystem.TransferredPosition))
                    {
                        if (!_mouseOn)
                        {
                            PlaySound(changeSelection, 1.0f);
                            this._virtualFather.OnFocus(this);
                        }
                        _mouseOn = true;
                    }
                    else
                    {
                        _mouseOn = false;
                    }
                }
            }
        }
    }
}