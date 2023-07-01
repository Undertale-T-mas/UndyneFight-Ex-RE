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
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI : Entity
    {
        private class VirtualFather : GameObject
        {
            public class StartButton : SelectingModule
            {
                public StartButton() : base(null)
                {
                    this.Image = Resources.UI.Start;
                    this.collidingBox.Size = Image.Bounds.Size.ToVector2() * 2;
                    this.Centre = new Vector2(126, 659);

                    this.LeftClick += StartGame;
                    this.AlwaysActivate = true;
                }

                private void StartGame()
                {
                    if (this._available)
                        _virtualFather.StartGame();
                }

                public override void Draw()
                { 
                    this.FormalDraw(Image, this.Centre, null, _drawingColor, Vector2.One * 1.93f, 0, ImageCentre, SpriteEffects.None);
                    this.FormalDraw(Image, this.Centre, null, _drawingColor, Vector2.One * 1.93f, 0, ImageCentre, SpriteEffects.FlipHorizontally);
                }
                bool _available;
                public override void Update()
                {
                    Color notAvailable = Color.Purple;
                    bool available = _virtualFather.SongSelected != null && _virtualFather.CurrentDifficulty != Difficulty.NotSelected;
                    this._available = available;
                    ColorMouseOn = available ? Color.PaleGoldenrod : notAvailable;
                    ColorNormal = available ? Color.White : notAvailable;
                    this.NeverEnable = !available;

                    if (!available && this.ModuleSelected) this.State = SelectState.False;
                    base.Update();
                }
                VirtualFather _virtualFather;
                public override void Start()
                {
                    this._virtualFather = FatherObject as VirtualFather;
                    base.Start();
                }
            }

            public void StartGame()
            {
                //find directory

                string dir;
                if (System.IO.Directory.Exists(dir = "Content\\Musics\\" + SongSelected.Music))
                    dir += "\\song";

                GameStates.StartSong(SongSelected, SongSelect.Illustration, dir, (int)CurrentDifficulty, CurrentJudgementState, ModeSelect.ModeSelected);
            }

            public VirtualFather()
            {
                ModeSelect = new ModeSelector();
                SongSelect = new SongSelector();
                DiffSelect = new DifficultyUI(this);

                if(PlayerManager.CurrentUser != null)
                {
                    this.AddChild(new DataShow(PlayerManager.CurrentUser));
                }

                this.AddChild(ModeSelect);
                this.AddChild(SongSelect);
                this.AddChild(DiffSelect);

                this.AddChild(ButtonStart = new StartButton());
                CurrentActivate = ModeSelect;
            }

            public bool Activated => true;

            public StartButton ButtonStart { get; init; }
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
            public JudgementState CurrentJudgementState { get; private set; } = JudgementState.Balanced;

            internal void SelectDiff(Difficulty difficulty)
            {
                CurrentDifficulty = difficulty;
            }

            internal void ChangeJudge(JudgementState judgeState)
            {
                CurrentJudgementState = judgeState;
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
