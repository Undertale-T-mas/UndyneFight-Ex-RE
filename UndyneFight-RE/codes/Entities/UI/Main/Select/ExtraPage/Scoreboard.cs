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
                        this.collidingBox.Size = new(500, 340);
                    }

                    int ceilCount = 7;
                    float roll = 0;

                    public override void Draw()
                    {
                        for (int i = 0; i <= ceilCount; i++)
                        {
                            float y = MathHelper.Lerp(this.collidingBox.Up, this.collidingBox.Down, i * 1.0f / ceilCount);
                            DrawingLab.DrawLine(new vec2(this.collidingBox.Left, y), new vec2(this.collidingBox.Right, y),
                                3, col.White, 0.67f);
                        }
                    }

                    public override void Update()
                    {
                        this.collidingBox.SetCentre(_vfa.Centre);
                        this.roll += MouseSystem.MouseWheelChanged;
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
                        List<Tuple<string, SongResult>> s = JsonSerializer.Deserialize<List<Tuple<string, SongResult>>>(message);
                        for (int i = 0; i < s.Count; i++) Leaderboard.Add(i + l, s[i]);
                    }
                    public void Merge(ScoreObject other)
                    {
                        foreach(var obj in other.Leaderboard) {
                            if (this.Leaderboard.ContainsKey(obj.Key)) continue;
                            this.Leaderboard.Add(obj.Key, obj.Value);
                        }
                    }
                    public Dictionary<int, Tuple<string, SongResult>> Leaderboard { get; private set; } = new();
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