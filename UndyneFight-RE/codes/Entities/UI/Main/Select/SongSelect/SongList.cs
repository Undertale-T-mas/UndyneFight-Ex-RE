using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            public int SelectedID { get; private set; } = -1;

            private abstract partial class SongList : Entity, ISelectChunk
            {
                public SongList(SongSelector father)
                {
                    this._father = father;
                    this.UpdateIn120 = true;
                }

                public override void Start()
                {
                    this.all = new SelectingModule[this.ChildObjects.Count];
                    int i = 0;
                    _images = new Texture2D[this.ChildObjects.Count];
                    foreach (var obj in this.ChildObjects) { 
                        this.all[i] = (obj as SelectingModule); 
                        if (all[i] is LeafSelection)
                        {
                            LeafSelection leaf = (LeafSelection)all[i];
                            _images[i] = leaf.Illustration;
                            leaf.LeftClick += () =>
                            {
                                if (!leaf.ModuleSelected) this.DeSelect();
                            };
                        }
                        i++;
                    }
                    this.all[0].OnFocus();
                    base.Start();
                }

                protected RootSelection Head { set; private get; }
                private SongSelector _father;
                private Vector2 _positionDelta = Vector2.Zero;

                private Texture2D[] _images;
                public Texture2D[] Images => _images;

                public bool Activated => _activated && _father.Activated; 
                public bool DrawEnabled => _father.DrawEnabled && this._activated;

                private bool _activated = false;

                public void Activate()
                {
                    this._activated = true;
                }

                public void Deactivate()
                { 
                    this._activated = false;
                }

                public void FocusOn(SelectingModule module)
                {
                    _currentFocus = module;
                    focusID = -1;
                }

                SelectingModule _lastSelected;
                private void DeSelect()
                {
                    this._father.DeSelectSong();
                }
                public void Selected(SelectingModule module)
                {
                    if (module is LeafSelection)
                    {
                        LeafSelection leafCur = module as LeafSelection;
                        if (module == _lastSelected) { goto A; }
                        LeafSelection leafLast = _lastSelected as LeafSelection;
                        if (_lastSelected != null)
                        {
                            _lastSelected.State = (leafLast.Root.ModuleSelected || (!leafLast.Root.ModuleEnabled)) ? SelectState.False : SelectState.Disabled;
                            if (!leafLast.Root.ModuleEnabled && leafLast.Root != leafCur.Root)
                            {
                                leafLast.Root.State = SelectState.Selected;
                            }
                        }
                        
                        _lastSelected = module;
                        A: module.Extras = leafCur.FightObject;
                        this._father.Selected(module);
                        this._father.SelectedID = FocusID;
                    } 
                } 

                SelectingModule[] all;

                SelectingModule _currentFocus;
                public SelectingModule CurrentFocus => _currentFocus;
                int focusID = -1;
                public int FocusID
                {
                    get
                    {
                        if (focusID == -1)
                        {
                            for (int i = 0; i < all.Length; i++)
                            {
                                if (all[i] == _currentFocus) return focusID = i;
                            }
                            throw new Exception();
                        }
                        else return focusID;
                    }
                }

                protected int SelectedID { get; private set; } = -1;

                public sealed override void Update()
                {
                    if (!this.Activated) return;

                    if (_lastSelected != null && !_lastSelected.ModuleSelected) this._father.SelectedID = -1;

                    if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                    {
                        int id = FocusID;
                        for (int i = id + 1; i < all.Length; i++)
                        {
                            if (all[i].ModuleEnabled)
                            {
                                _currentFocus.OffFocus();
                                all[i].OnFocus();
                                break;
                            }
                        }
                    }
                    else if (GameStates.IsKeyPressed120f(InputIdentity.MainUp))
                    {
                        int id = FocusID;
                        for (int i = id - 1; i >= 0; i--)
                        {
                            if (all[i].ModuleEnabled)
                            {
                                _currentFocus.OffFocus();
                                all[i].OnFocus();
                                break;
                            }
                        }
                    }
                    if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                    {
                        _currentFocus.ConfirmKeyDown();
                    }

                    float height = _currentFocus.Centre.Y;
                    if (_currentFocus.IsMouseOn)
                    {
                        if (height > 660) wheelRemain += MathF.Min(20, (height - 660) * 0.2f);
                        if (height < 80) wheelRemain -= MathF.Min(20, (80 - height) * 0.2f);
                    }
                    //mouse

                    float wheel = MouseSystem.CurrentState.ScrollWheelValue;
                    
                    wheelRemain -= (wheel - wheelLast); 
                    wheelLast = wheel;

                    if (MathF.Abs( wheelRemain ) > 0.001f)
                    {
                        float wheelNext = wheelRemain * 0.8f;
                        float delta = wheelNext - wheelRemain;
                        wheelRemain += delta;
                        this._positionDelta.Y += delta * 0.35f;
                    }
                    if (this._positionDelta.Y > 0)
                        this._positionDelta.Y = MathHelper.Lerp(this._positionDelta.Y, 0, 0.15f);
                    float totalY = Head.SumHeight + 150;
                    if (totalY > 720 && this._positionDelta.Y < 720 - totalY)
                        this._positionDelta.Y = MathHelper.Lerp(this._positionDelta.Y, 720 - totalY, 0.15f);
                    else if(totalY <= 720 && this._positionDelta.Y < 0) 
                        this._positionDelta.Y = MathHelper.Lerp(this._positionDelta.Y, 0, 0.15f);
                }
                float wheelRemain = 0f, wheelLast = 0;
            }
        }
    }
}
