using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UndyneFight_Ex.ChampionShips;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class SongSelector : Entity, ISelectChunk
        { 
            public bool Activated { get; set; } = false;
            public bool DrawEnabled { get; set; } = false;

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
                DrawingLab.DrawLine(new(48, 127 + 90), new(208, 127 + 90), 3.0f, color, 0.5f);

                FightResources.Font.NormalFont.CentreDraw("Song", new(128, 60 + 90), color, 1.3f, 0.4f);
                FightResources.Font.NormalFont.CentreDraw("Select", new(128, 98 + 90), color, 1.3f, 0.4f);

                if (!Activated) return;
            }

            public void FocusOn(SelectingModule module)
            {

            }

            public void Selected(SelectingModule module)
            {

            }

            private SelectState _state = SelectState.False;
            private Color _drawingColor;

            public override void Update()
            {
                this.collidingBox = new(48, 60 + 90, 208 - 48, 127 - 60); 
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

                if (GameStates.IsKeyPressed120f(InputIdentity.Cancel))
                {
                    this._virtualFather.ModeSelect.Activate();
                    this.Deactivate();
                }
            } 

            private class SongPack
            {
                public string Title { get; init; }
                public IWaveSet[] AllSongs { get; init; }

                public Dictionary<string, Texture2D> Images { get; init; } = new();
                public HashSet<string> Availables { get; init; } = new();
                
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
                        else if (tmp is IChampionShip) this.AllSongs[i] = ((IChampionShip)tmp).GameContent;

                        string dir = "";
                        Resources.MainLoader.RootDirectory = "";
                        if (System.IO.Directory.Exists(dir = "Content\\Musics\\" + this.AllSongs[i].Music))
                        {
                            if (System.IO.File.Exists(dir + "\\song.xnb"))
                                Availables.Add(this.AllSongs[i].Music);
                            if (System.IO.File.Exists(dir + "\\paint.xnb"))
                                Images.Add(this.AllSongs[i].Music, Resources.MainLoader.Load<Texture2D>(dir + "\\paint"));
                        }
                        else if (System.IO.File.Exists(dir)) Availables.Add(this.AllSongs[i].Music);
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

            public override void Start()
            {
                this._virtualFather = this.FatherObject as VirtualFather;
                Task task = Task.Run(() =>
                {
                    songPacks = FetchSongPack();
                    this.AddChild(new ImageDrawer());
                    this.AddChild(this._packMode = new PackMode(this));
                    this._packMode.Activate();
                    this._currentSongList = _packMode;
                });
            }
            PackMode _packMode;
            public SongSelector()
            {
                this.UpdateIn120 = true;
            }
        }
    }
}
