using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Net.Sockets;
using UndyneFight_Ex.SongSystem;
using System.ComponentModel.Design.Serialization;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI : Entity
    {
        private interface ISelectChunk
        {
            void Activate();
            void Deactivate();

            bool Activated { get; }
            bool DrawEnabled { get; }
             
            void Selected(SelectingModule module);
            void FocusOn(SelectingModule module);
        }
        enum SelectState
        {
            False = 0,
            MouseOn = 1,
            Selected = 2,
            Disabled = 3
        }
        /// <summary>
        /// A unit which is in a select chunk
        /// </summary>
        private abstract class SelectingModule : Entity
        {
            public SelectingModule(ISelectChunk father)
            {
                this._father = father;
            }

            public int ID { get; protected set; }
            public virtual void ConfirmKeyDown() {
                if (this.State == SelectState.MouseOn || this.State == SelectState.False)
                {
                    if (!NeverEnable)
                        this.State = SelectState.Selected;
                    _father.Selected(this);
                }
                else if(this.State == SelectState.Selected)
                {
                    this.State = SelectState.MouseOn;
                }
                LeftClick?.Invoke();
            } 
            public virtual void OnFocus() { 
                if (this.State == SelectState.False) {
                    this.State = SelectState.MouseOn;
                }
                MouseOn?.Invoke();
                _mouseOn = true;
                _father.FocusOn(this);
            }
            public virtual void OffFocus()
            {
                if(this.State == SelectState.MouseOn)
                {
                    this.State = SelectState.False;
                }
                _mouseOn = false;
            }

            protected ISelectChunk _father;

            public SelectState State { protected get => _state;
                set {
                    if(_state == SelectState.Selected && value == SelectState.False) {
                        ; }
                    _state = value;
                } }
            private SelectState _state =
                SelectState.False;
            protected Color _drawingColor;
            protected bool _mouseOn;

            public bool IsMouseOn => _mouseOn;

            public event Action LeftClick;
            public event Action MouseOn;

            public bool ModuleEnabled => this.State != SelectState.Disabled;
            public bool ModuleSelected => this.State == SelectState.Selected;

            public bool NeverEnable { private get; set; } = false;

            public Color ColorNormal { private get; set; } = Color.White;
            public Color ColorMouseOn { private get; set; } = Color.LightGoldenrodYellow;
            public Color ColorSelected { private get; set; } = Color.Yellow;
            public Color ColorDisabled { private get; set; } = Color.Red;

            public override void Update()
            {
                Color mission = this.State switch
                {
                    SelectState.False => ColorNormal,
                    SelectState.MouseOn => ColorMouseOn,
                    SelectState.Selected => ColorSelected,
                    SelectState.Disabled => ColorDisabled,
                    _ => throw new ArgumentException(),
                };
                _drawingColor = Color.Lerp(_drawingColor, mission, 0.12f);
                if (!_father.Activated && this.ModuleEnabled)
                {
                    //State = SelectState.False;
                    return;
                }
                if (this.State == SelectState.Disabled)
                    return;
                bool isLeftClick = MouseSystem.IsLeftClick();
                if (!(MouseSystem.Moved || isLeftClick)) 
                    return;
                if(_mouseOn = this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {
                    if (this.State == SelectState.False)
                    { 
                        this.State = SelectState.MouseOn; 
                        this.MouseOn?.Invoke(); 
                        _father.FocusOn(this); 
                    }
                    if (isLeftClick)
                    {
                        if (this.State == SelectState.MouseOn)
                        {
                            if (!NeverEnable)
                                this.State = SelectState.Selected;
                            this._father.Selected(this);
                        }
                        else
                        {
                            this.State = SelectState.MouseOn;
                        }
                        LeftClick?.Invoke();
                        _father.FocusOn(this);
                    }
                }
                else
                {
                    if (this.State == SelectState.MouseOn) this.State = SelectState.False;
                }
            }
        }
        private class VirtualFather : GameObject 
        {
            public VirtualFather()
            {
                ModeSelect = new ModeSelector();
                SongSelect = new SongSelector();
                DiffSelect = new DifficultyUI(this);
                this.AddChild(ModeSelect);
                this.AddChild(SongSelect);
                this.AddChild(DiffSelect);
                CurrentActivate = ModeSelect;
            }

            public bool Activated => true;

            public ModeSelector ModeSelect { get; init; }
            public SongSelector SongSelect { get; init; }
            public DifficultyUI DiffSelect { get; init; }

            public ISelectChunk CurrentActivate { get; set; }

            public IWaveSet SongSelected { get; private set; }
            public HashSet<Difficulty> DifficultyPanel { get; private set; }

            public void SelectSong(object result)
            {
                if(result == null)
                {
                    DifficultyPanel = null;
                    SongSelected = null;
                    return;
                }
                if (result is IWaveSet)
                {
                    DifficultyPanel = new();
                    for (int i = 0; i <= 4; i++) DifficultyPanel.Add((Difficulty)i);
                    this.SongSelected = result as IWaveSet;
                }
                else if (result is IChampionShip)
                {
                    DifficultyPanel = new();
                    IChampionShip championShip;
                    this.SongSelected = (championShip = result as IChampionShip).GameContent;
                    foreach(Difficulty difficulty in championShip.DifficultyPanel.Values) { 
                        DifficultyPanel.Add(difficulty); 
                    }
                }
            }
            public void Select(ISelectChunk module)
            {
                if (CurrentActivate == module) return;
                CurrentActivate.Deactivate();
                CurrentActivate = module;
            }

            public override void Update()
            { 
            }

            public Difficulty CurrentDifficulty { get; private set; } = Difficulty.NotSelected;

            internal void SelectDiff(Difficulty difficulty)
            {
                CurrentDifficulty = difficulty;
            }
        }

        public SelectUI()
        {
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;

            this.AddChild(new MouseCursor());
            this.AddChild(new LineDistributer());
            this.AddChild(new VirtualFather());
        }

        public override void Draw()
        { 

        }

        public override void Update()
        { 
        }
    }
}
