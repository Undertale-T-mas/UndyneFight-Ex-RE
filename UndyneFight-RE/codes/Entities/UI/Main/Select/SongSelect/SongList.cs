using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector
        {
            private abstract class SongList : Entity, ISelectChunk
            {
                protected class LeafSelection : Button
                {
                    public LeafSelection(RootSelection rootSelection, Vector2 centre, string text) : base(rootSelection.Father, centre, text)
                    {
                        rootSelection.LinkLeaf(this);
                        this.CentreDraw = false;
                        this.SelectedScale = 1.1f; 
                        this.ColorDisabled = Color.Transparent;
                        this.State = SelectState.Disabled;
                        this.Root = rootSelection;

                        this.LeftClick += () => {
                            if (this.State == SelectState.Selected)
                            {
                                this.Root.State = SelectState.Disabled;
                            }
                            else this.Root.State = SelectState.Selected;
                        };
                    }

                    public RootSelection Root { get; init; }
                    public bool SongAvailable { get; init; } = true;

                    public override void Start()
                    {
                        base.Start();
                        this.State = SelectState.Disabled;
                        if (!SongAvailable) this.ColorSelected = Color.Red;
                    }
                    public override void Update()
                    {
                        base.Update();

                        this.PositionDelta = Root.PositionDelta;
                    }
                }
                protected class RootSelection : Button
                {
                    public RootSelection(SongList father, Vector2 centre, string text) : base(father, centre, text)
                    {
                        this._father = father;
                        this.SelectedScale = 1.1f;
                        this.CentreDraw = false;
                        this.LeftClick += () => lerpRate = 0f;
                        UpdateIn120 = true;
                    }

                    private new SongList _father;
                    public SongList Father => _father;

                    public RootSelection Last { private get; set; }

                    private float lerpRate = 0.0f;
                    private float maxHeight = 0f;
                    private float currentHeight = 0f;

                    private float sumLast = 0f;
                    public float SumHeight => sumLast + this.currentHeight + 65;
                     
                    public override void Update()
                    {
                        if(Last != null) { this.sumLast = Last.sumLast + Last.currentHeight + 65; }
                        this.PositionDelta = _father._positionDelta + new Vector2(0, sumLast);
                        base.Update();

                        if (State == SelectState.Selected || State == SelectState.Disabled)
                            currentHeight = MathHelper.Lerp(currentHeight, maxHeight, lerpRate);
                        else
                            currentHeight = MathHelper.Lerp(currentHeight, 0f, lerpRate);
                        lerpRate = MathHelper.Lerp(lerpRate, 0.19f, 0.06f);
                        if(currentHeight > 2 && currentHeight < maxHeight - 2)
                        {
                            int id = 0;
                            foreach (LeafSelection button in this._leave)
                            {
                                float y = id * 55f + (this.State == SelectState.Selected ? -25f : 75f);
                                if (!button.ModuleSelected)
                                    button.State = y < currentHeight ? SelectState.False : SelectState.Disabled;
                                id++;
                            }
                        }
                    }

                    private List<LeafSelection> _leave = new List<LeafSelection>();
                    public void LinkLeaf(LeafSelection leafSelection)
                    {
                        _leave.Add(leafSelection);
                        maxHeight += 55f;
                    }

                    public override void Draw()
                    {
                        base.Draw();
                        if(!_father.DrawEnabled) { return; }
                        Vector2 pos1 = _centre + this.PositionDelta;
                        pos1.Y += 50;
                        //276, 60
                        DrawingLab.DrawLine(new Vector2(272, pos1.Y), new(246, pos1.Y - 26), 3f, Color.Silver, 0.4f);
                        DrawingLab.DrawLine(new Vector2(272, pos1.Y), new(611, pos1.Y), 3f, Color.Silver, 0.4f);
                        DrawingLab.DrawLine(new Vector2(272, pos1.Y), new(272, pos1.Y + currentHeight), 3f, Color.Silver, 0.4f);
                    }

                    public override void Start()
                    { 
                    }
                }
                public SongList(SongSelector father)
                {
                    this._father = father;
                    this.UpdateIn120 = true;
                }

                public override void Start()
                {
                    this.all = new SelectingModule[this.ChildObjects.Count];
                    int i = 0;
                    foreach (var obj in this.ChildObjects) { 
                        this.all[i] = (obj as SelectingModule); i++; 
                    }
                    this.all[0].OnFocus();
                    base.Start();
                }
                protected RootSelection Head { set; private get; }
                private SongSelector _father;
                private Vector2 _positionDelta = Vector2.Zero;

                public bool Activated => _activated && _father.Activated; 
                public bool DrawEnabled => Activated;

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
                    currentFocus = module;
                    focusID = -1;
                }

                SelectingModule _lastSelected;
                public void Selected(SelectingModule module)
                {
                    if (module == _lastSelected) return;
                    if (module is LeafSelection)
                    {
                        LeafSelection leafLast = _lastSelected as LeafSelection;
                        LeafSelection leafCur = module as LeafSelection;
                        if (_lastSelected != null)
                        {
                            _lastSelected.State = (leafLast.Root.ModuleSelected || (!leafLast.Root.ModuleEnabled)) ? SelectState.False : SelectState.Disabled;
                            if (!leafLast.Root.ModuleEnabled && leafLast.Root != leafCur.Root) leafLast.Root.State = SelectState.Selected;
                        }
                        
                        _lastSelected = module;
                        this._father.Selected(module);
                    } 
                } 

                SelectingModule[] all;

                SelectingModule currentFocus;
                int focusID = -1;
                int FocusID
                {
                    get
                    {
                        if (focusID == -1)
                        {
                            for (int i = 0; i < all.Length; i++)
                            {
                                if (all[i] == currentFocus) return focusID = i;
                            }
                            throw new Exception();
                        }
                        else return focusID;
                    }
                }

                public sealed override void Update()
                {
                    if (!this.Activated) return;
                    if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                    {
                        int id = FocusID;
                        for (int i = id + 1; i < all.Length; i++)
                        {
                            if (all[i].ModuleEnabled)
                            {
                                currentFocus.OffFocus();
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
                                currentFocus.OffFocus();
                                all[i].OnFocus();
                                break;
                            }
                        }
                    }
                    if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                    {
                        currentFocus.ConfirmKeyDown();
                    }

                    float height = currentFocus.Centre.Y;
                    if (currentFocus.IsMouseOn)
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
