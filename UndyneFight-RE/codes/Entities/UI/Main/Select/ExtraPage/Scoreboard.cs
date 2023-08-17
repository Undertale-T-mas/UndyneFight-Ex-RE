using System;
using UndyneFight_Ex.SongSystem;

using vec2 = Microsoft.Xna.Framework.Vector2;
using rect = UndyneFight_Ex.CollideRect;
using col = Microsoft.Xna.Framework.Color;
using VPCT = Microsoft.Xna.Framework.Graphics.VertexPositionColorTexture;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using UndyneFight_Ex.Remake.Network;
using System.Net.Sockets;
using UndyneFight_Ex.Entities;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Xna.Framework;
using System.Linq;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class Extra
        {
            private class Scoreboard : Entity
            {
                private class ListBox : Entity
                {
                    private ScoreObject _list; Entity _vfa;

                    public ListBox(Entity vfa, ScoreObject list)
                    {
                        this._vfa = vfa;
                        this._list = list;
                        this.collidingBox.Size = new(570, 340);
                    }

                    int ceilCount = 7;
                    float roll = 0;

                    string[] args = { "#rk", "Name", "Score" };
                    float[] argWeight = { 0.125f, 0.4f, 0.475f };
                    public override void Draw()
                    {
                        for (int i = 0; i <= ceilCount; i++)
                        {
                            float y = MathHelper.Lerp(this.collidingBox.Up, this.collidingBox.Down, i * 1.0f / ceilCount);
                            DrawingLab.DrawLine(new vec2(this.collidingBox.Left, y), new vec2(this.collidingBox.Right, y),
                                3, col.Silver, 0.67f);
                        }
                        GLFont font = FightResources.Font.NormalFont;
                        float cur = 0, next;
                        for(int i = 0; i <= args.Length; i++)
                        {
                            float x = MathHelper.Lerp(this.collidingBox.Left, this.collidingBox.Right, cur);
                            DrawingLab.DrawLine(new vec2(x, this.collidingBox.Up), new vec2(x, this.collidingBox.Down),
                                3, col.Silver, 0.67f);
                            if (i >= args.Length) break;
                            next = cur + argWeight[i];

                            x = MathHelper.Lerp(this.collidingBox.Left, this.collidingBox.Right, (cur + next) / 2f);
                            for(int j = 0; j < ceilCount; j++)
                            {
                                float v = j + 0.5f;
                                float y = MathHelper.Lerp(this.collidingBox.Up, this.collidingBox.Down, v * 1.0f / ceilCount);
                                if (j == 0) font.CentreDraw(args[i], new(x, y), col.White, 1.0f, 0.67f);
                                else if(j + delta <= arr.Length) font.CentreDraw(
                                    (i switch { 0 => arr[j + delta - 1].Item2.ToString(), 1 => arr[j + delta - 1].Item1, 2 => arr[j + delta - 1].Item3.ToString(), _ => throw new Exception() })
                                    , new(x, y), col.White, 1.0f, 0.67f);
                            }

                            cur = next;
                        }
                    }
                    Tuple<string, int, int> firstObj;
                    SortedDictionary<int, Tuple<string, int, int>> scores = new();
                    Tuple<string, int, int>[] arr;
                    int delta = 0;
                    public override void Update()
                    {
                        this.collidingBox.SetCentre(_vfa.Centre);
                        this.roll += MouseSystem.MouseWheelChanged * 0.3f;

                        if(this.roll < -20f && arr.Length > 6)
                        {
                            delta++;
                            if (delta >= arr.Length - 3) delta = arr.Length - 4;
                            roll += 20f;
                        }
                        if(this.roll > 20f) {
                            delta--;
                            if (delta < 0) delta = 0;
                            roll -= 20f;
                        }

                        if (!_list.Updated) return;
                        scores.Clear();
                        foreach (var v in this._list.Leaderboard) {
                            if(!scores.ContainsKey(v.Value.Item2 + 1))
                                scores.Add(v.Value.Item2 + 1, new(Abbr(v.Value.Item1), v.Value.Item2 + 1, v.Value.Item3.Score));
                        }
                        if (_list.FirstObj != null && !scores.ContainsKey(-1)) scores.Add(-1, new(Abbr(_list.FirstObj.Item1 + "(me)"), _list.FirstObj.Item2 + 1, _list.FirstObj.Item3.Score));
                        arr = scores.Values.ToArray();
                    }
                    string Abbr(string p)
                    {
                        if (p.Length < 12) return p;
                        return p[0..4] + "..." + p[^5..];
                    }
                }


                string _song;
                int _diff;
                VirtualFather _vfa;
                Entity buffer;
                Entity current;
                Extra _efa;

                int requireL, requireR;
                public Scoreboard(Extra efa ,VirtualFather vfa)
                {
                    this._efa = efa;
                    this._vfa = vfa;
                }
                private void InfoUpdated() {
                    fullList = new(1);
                    this.current = null;
                    requireL = 1; requireR = 10;
                    this._song = _vfa.SongSelected.FightName; this._diff = (int)this._vfa.CurrentDifficulty;
                    UFSocket<ScoreObject> score = new((s) => {
                        if (s.State == 'S')
                        {
                            this.fullList.Merge(s.Data);
                            if (this.current is not ListBox)
                                this.buffer = new ListBox(this, fullList);
                        }
                        else if(s.State == 'F')
                        {
                            buffer = new TextEntity("Empty Scoreboard", new(480, 470)) { Depth = 0.67f, Scale = 1.973f, Rotation = -16, AngleMode = true, BlendColor = col.Red };
                        }
                        else
                        {
                            buffer = new TextEntity("Cannot Connect!", new(480, 470)) { Depth = 0.67f, Scale = 1.973f, Rotation = -16, AngleMode = true, BlendColor = col.DarkRed };
                        }
                    });
                    score.SendRequest($"Enquire\\Scoreboard\\{_song}\\{_diff}");
                }
                ScoreObject fullList = new(1);
                class ScoreObject : IMessageResult
                {
                    private int l;
                    public ScoreObject() { this.l = 1; }
                    public ScoreObject(int l)
                    {
                        this.l = l;
                    }
                    public void Analysis(string message)
                    {
                        Updated = true;
                        List<Tuple<string, int, SongResult>> s = JsonSerializer.Deserialize<List<Tuple<string, int, SongResult>>>(message);
                        if (s[0].Item1 == PlayerManager.currentPlayer)
                        {
                            this.FirstObj = s[0];
                            for (int i = 1; i < s.Count; i++)
                            {
                                if (s[i].Item2 != i + l - 2) throw new Exception();
                                Leaderboard.Add(i + l, s[i]);
                            }
                        }
                        else
                            for (int i = 0; i < s.Count; i++)
                            {
                                if (s[i].Item2 != i + l - 1) throw new Exception();
                                Leaderboard.Add(i + l, s[i]);
                            }
                    }
                    public void Merge(ScoreObject other)
                    {
                        Updated = true;
                        if(other.FirstObj != null) this.FirstObj = other.FirstObj;
                        foreach(var obj in other.Leaderboard) {
                            if (this.Leaderboard.ContainsKey(obj.Key)) continue;
                            this.Leaderboard.Add(obj.Key, obj.Value);
                        }
                    }
                    public Tuple<string, int, SongResult> FirstObj { get; private set; }
                    public Dictionary<int, Tuple<string, int, SongResult>> Leaderboard { get; private set; } = new();
                    public bool Updated { get; internal set; } = false;
                }
                public override void Start()
                {
                    base.Start();
                }

                public override void Draw()
                {
                    current?.Draw();
                }

                public override void Update()
                {
                    this.Centre = new vec2(480, _efa.Centre.Y + 337);
                    if(buffer != null)
                    {
                        this.current = buffer;
                        buffer = null;
                    }
                    current?.Update();
                    if(_vfa.SongSelected == null) return;
                    string s = _vfa.SongSelected.FightName;
                    int t = (int)_vfa.CurrentDifficulty;
                    if(s != _song || t != _diff) { InfoUpdated(); }
                }
            }
        }
    }
}