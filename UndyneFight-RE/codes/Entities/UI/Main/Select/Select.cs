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
using UndyneFight_Ex.Remake.Components;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Remake.Effects;

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
                GameStates.InstanceCreate(new InstantEvent(2, () => {
                    GameStates.StartSong(SongSelected, SongSelect.Illustration, dir, dif, CurrentJudgementState, ModeSelect.ModeSelected);
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

            SmartMusicPlayer smartPlayer = new();
            smartPlayer.InsertPeriod(new MusicPlayer(Resources.Musics.DreamDiver_INTRO), 2407.5f, false);
            smartPlayer.InsertPeriod(new MusicPlayer(Resources.Musics.DreamDiver_LOOP), 4808.5f, true);
            GameStates.InstanceCreate(smartPlayer); smartPlayer.Play();

            GameStates.InstanceCreate(new InstantEvent(2, () => {
                var render = GameStates.CurrentScene.BackgroundRendering;
                GameStates.CurrentScene.CurrentDrawingSettings.backGroundColor = Color.White;
                render.InsertProduction(_backGenerater = new BackGenerater(0.6f));
            }));
         /*   GameStates.InstanceCreate(new InstantEvent(2397, () => { 
                GameStates.InstanceCreate(
                    new MusicPlayer(Resources.Musics.DreamDiver_LOOP) { IsLoop = false }
                ); 

            }));*/
        }
        BackGenerater _backGenerater;

        public override void Draw()
        { 

        }

        public override void Update()
        { 
        }
    }
}
