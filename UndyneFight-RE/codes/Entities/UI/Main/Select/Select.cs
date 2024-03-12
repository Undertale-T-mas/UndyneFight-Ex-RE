using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.SongSystem;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.GameStates;

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

                    UpdateIn120 = true;

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
                    this.FormalDraw(Image, this.Centre, null, _drawingColor * alpha1, Vector2.One * 1.93f, 0, ImageCentre, SpriteEffects.None);
                    this.FormalDraw(Image, this.Centre, null, _drawingColor * (1.6f - alpha1), Vector2.One * 1.93f, 0, ImageCentre, SpriteEffects.FlipHorizontally);

                    Resources.Font.Normal.CentreDraw("开始", this.Centre - new Vector2(0, 20), _drawingColor, 1.2f, 0.2f);
                }
                bool _available;
                float alpha1 = 0.7f;
                float time = 0.0f;
                public override void Update()
                {
                    alpha1 = MathF.Sin(time) * 0.2f + 0.8f;
                    time += 0.01f;
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

                GameStates.Broadcast(new(null, "MusicFadeOut"));
                int dif = (int)CurrentDifficulty;
                InstanceCreate(new InstantEvent(2, () => {
                    StartSong(SongSelected, SongSelect.Illustration, dir, dif, CurrentJudgementState, ModeSelect.ModeSelected);
                }));
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
                    foreach (Difficulty difficulty in championShip.DifficultyPanel.Values) {
                        var Attributes = championShip.GameContent.Attributes;
                        if (Attributes == null) break;
                        if (Attributes.UnlockedDifficulties.Contains(difficulty))
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
                if (CurrentDifficulty == difficulty) return;
                CurrentDifficulty = difficulty;
                if (difficulty != Difficulty.NotSelected)
                    InstanceCreate(new InstantEvent(1, () =>
                    {
                        this.SongSelect.DifficultyChanged(difficulty);
                    }));
            }

            internal void ChangeJudge(JudgementState judgeState)
            {
                CurrentJudgementState = judgeState;
            }
        }
        VirtualFather _vFather;
        SmartSelector _extra;
        public SelectUI()
        {
            CurrentScene.CurrentDrawingSettings.defaultWidth = 960f;

            this.UpdateIn120 = true;
            this.AddChild(new MouseCursor());
            this.AddChild(new LineDistributer());
            this.AddChild(_vFather = new VirtualFather());
            this.AddChild(_extra = new Extra(_vFather));
         /*   GameStates.InstanceCreate(new InstantEvent(2397, () => { 
                GameStates.InstanceCreate(
                    new MusicPlayer(Resources.Musics.DreamDiver_LOOP) { IsLoop = false }
                ); 

            }));*/
        }

        public override void Draw()
        { 

        }

        public override void Update()
        {
            if (this._vFather.CurrentActivate == _vFather.ModeSelect && IsKeyPressed120f(InputIdentity.Cancel))
            {
                this.Dispose();
                InstanceCreate(new DEBUG.IntroUI());
            }
            _vFather.UpdateEnabled = !_extra.Activated;
        }
    }
}
