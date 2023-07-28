using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UndyneFight_Ex.ChampionShips;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        public static void Initialize()
        {
            SongSelector.Initialize();
        }
        private partial class SongSelector : Entity, ISelectChunk
        { 
            public bool Activated { get; set; } = false;
            public bool DrawEnabled { get; set; } = false;
            public Texture2D Illustration => _currentSongList.Illustration;

            public SelectingModule Focus => null;

            private VirtualFather _virtualFather;

            public void Activate()
            {
                this.Activated = true;
                this.DrawEnabled = true;
                this._state = SelectState.Selected;
                _virtualFather.Select(this);
            }

            public void Deactivate()
            {
                this.Activated = false;
                this._state = SelectState.False;
            }

            public override void Draw()
            {
                Color color = _drawingColor;
                DrawingLab.DrawLine(new(48, 127 + 95), new(208, 127 + 95), 3.0f, color, 0.5f);

                FightResources.Font.NormalFont.CentreDraw("Song", new(128, 60 + 95), color, 1.3f, 0.4f);
                FightResources.Font.NormalFont.CentreDraw("Select", new(128, 98 + 95), color, 1.3f, 0.4f);

                if (!Activated) return;
            }

            public void FocusOn(SelectingModule module)
            {

            }
            public void DifficultyChanged(Difficulty dif)
            {
                if (this.Activated) return;
                if(this._currentSongList is DiffMode)
                {
                    (this._currentSongList as DiffMode).ReSort(dif);
                }
            }

            private void OrderChanged()
            {
                if (!this.Activated) return;

                bool order = this._sortOrder.IsReverse;

                if (this._currentSongList is DiffMode)
                {
                    (this._currentSongList as DiffMode).ReSort(order);
                }
            }

            public void Selected(SelectingModule module)
            {
                if (module.Extras == null) return;
                this._virtualFather.SelectSong(module.Extras);
                this._virtualFather.DiffSelect.Activate(); 
                this.Deactivate();
            }
            public void DeSelectSong()
            {
                this._virtualFather.SelectSong(null);
            }

            private SelectState _state = SelectState.False;
            private Color _drawingColor;

            private int _timer = 0;

            public override void Update()
            {
                if(this.DrawEnabled && !this.Activated) { this.TryActivate(); }
                this.collidingBox = new(48, 60 + 95, 208 - 48, 127 - 60); 
                if (this.collidingBox.Contain(MouseSystem.TransferredPosition))
                {
                    if (_state == SelectState.False) _state = SelectState.MouseOn;
                    if (_state == SelectState.MouseOn && MouseSystem.IsLeftClick())
                    {
                        this.Activate();
                    }
                }
                else if (_state == SelectState.MouseOn) { _state = SelectState.False; }

                Color mission = _state switch
                {
                    SelectState.False => Color.White,
                    SelectState.MouseOn => Color.PaleGoldenrod,
                    SelectState.Selected => Color.Gold,
                    _ => throw new ArgumentException()
                };
                _drawingColor = Color.Lerp(_drawingColor, mission, 0.12f);

                if (!Activated) { _timer = 0; return; }

                _timer++;
                if (_timer < 3) return;

                if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
                {
                    this._virtualFather.ModeSelect.Activate();
                    this.Deactivate();
                }
            }

            private void TryActivate()
            {
                if (!MouseSystem.Moved) return;
                if (_virtualFather.CurrentDifficulty != Difficulty.NotSelected)
                {
                    if (!MouseSystem.IsLeftClick()) return;
                }
                float x = MouseSystem.TransferredPosition.X;
                if (x > 231 && x < 644) this.Activate();
            }

            private class SongPack
            {
                public string Title { get; init; }
                public IWaveSet[] AllSongs { get; init; }

                public Dictionary<string, Texture2D> Images { get; init; } = new();
                public HashSet<string> Availables { get; init; } = new();
                public Dictionary<IWaveSet, IChampionShip> ChampionshipMap { get; init; } = new();
                
                public SongPack(SongSet songSet) : this(songSet, songSet.SongSetName)
                { 
                }
                public SongPack(SongSet songSet, string name)
                {
                    this.Title = name;
                    this.AllSongs = new IWaveSet[songSet.Values.Length];
                    for(int i = 0;  i < this.AllSongs.Length; i++)
                    {
                        object tmp = Activator.CreateInstance(songSet.Values[i]);
                        if (tmp is IWaveSet) this.AllSongs[i] = (IWaveSet)tmp;
                        else if (tmp is IChampionShip)
                        {
                            IChampionShip championShip;
                            this.AllSongs[i] = (championShip = (IChampionShip)tmp).GameContent;
                            ChampionshipMap.Add(this.AllSongs[i], championShip);
                        }
                        string dir = "";
                        Resources.MainLoader.RootDirectory = "";
                        if (Directory.Exists(dir = "Content\\Musics\\" + this.AllSongs[i].Music))
                        {
                            if (File.Exists(dir + "\\song.xnb"))
                                Availables.Add(this.AllSongs[i].Music);
                            if (File.Exists(dir + "\\paint.xnb"))
                                Images.Add(this.AllSongs[i].Music + this.AllSongs[i].FightName, Resources.MainLoader.Load<Texture2D>(dir + "\\paint"));
                        }
                        else if (File.Exists(dir)) Availables.Add(this.AllSongs[i].Music);
                    }
                }
            }
            private static bool loaded = false;
            private static bool loading = false;
            private static SongPack[] FetchSongPack()
            {
                loading = true;
                SongSet main = FightSystem.MainGameSongs;
                SongPack mainPack = new(main, "Main Game");

                List<SongSet> extras = FightSystem.ExtraSongSets;
                SongPack[] extraPacks = new SongPack[extras.Count];
                int i = 0;
                foreach(SongSet songSet in extras)
                {
                    extraPacks[i] = new SongPack(songSet);
                    i++;
                }

                i = 0;
                SongPack[] championshipPack = new SongPack[FightSystem.ChampionShips.Count];
                foreach(ChampionShip championShip in FightSystem.ChampionShips)
                {
                    championshipPack[i] = new SongPack(championShip.Fights);
                    i++;
                }
                List<SongPack> result = new();
                result.Add(mainPack);
                result.AddRange(extraPacks);
                result.AddRange(championshipPack);

                loaded = true;
                loading = false;
                return result.ToArray();
            }
            private static SongPack[] songPacks;

            private SongList _currentSongList;

            public static void Initialize()
            {
                songPacks = FetchSongPack();
            }
            private SortOrder _sortOrder;
            public override void Start()
            {
                this._virtualFather = this.FatherObject as VirtualFather;

                this.AddChild(new ImageDrawer());
                this.AddChild(new SortInterface(this));
                this.AddChild(this._packMode = new PackMode(this));
                this.AddChild(this._diffClearMode = new DiffClearMode(this));
                this.AddChild(this._diffComplexMode = new DiffComplexMode(this));
                this.AddChild(this._letterMode = new LetterMode(this));
                
                this.AddChild(this._sortOrder = new(this));
                
                this._packMode.Activate();
                this._currentSongList = _packMode;
            }
            PackMode _packMode;
            DiffClearMode _diffClearMode;
            DiffComplexMode _diffComplexMode;
            LetterMode _letterMode;
            public SongSelector()
            {
                this.UpdateIn120 = true;
            }
        }
    }
}
