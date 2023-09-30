using System.Text.Json;
using UFData;
using UndyneFight_Ex.Remake.Network;

using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class ChampionshipSelector
    {
        private class Scoreboard : Entity
        {
            private static vec2[] vertexs = {
                new(70, 80 + 345),
                new(890 - 35, 80 + 345),
                new(890, 115 + 345),
                new(890, 340 + 345),
                new(70 + 35, 340 + 345),
                new(70, 340 - 35 + 345)
            };
            string? _division;
            public Scoreboard( string? division ) {  
                this._division = division;
                Fetch();
            }
            class ScoreObject : IMessageResult
            {
                ChampionshipScoreboard result;
                public ChampionshipScoreboard Scoreboard => result;
                public void Analysis(string message)
                { 
                    this.result = JsonSerializer.Deserialize<ChampionshipScoreboard>(message);
                }
            }

            ChampionshipScoreboard scoreboard;
            ChampionshipParticipant[] participants;
            private void Fetch()
            {
                if (string.IsNullOrEmpty(_division)) return;
                UFSocket<ScoreObject> socket = new(
                    (t) => {
                        if (t.Info == "please login first")
                        {
                            //login
                            KeepAliver.CheckAlive();
                            return;
                        }
                        this.scoreboard = t.Data.Scoreboard;
                        if (this.scoreboard == null) return;
                        this.participants = scoreboard.Members.ToArray();
                    }
                    ); ;
                socket.SendRequest($"Championship\\Score\\{_currentChampionshipNAME}\\{_division}");
            }

            public override void Draw()
            {
                this.SpriteBatch.DrawSortedVertex(0.15f,
                    new VPC[] {
                        new(new(vertexs[0]  , 0), col.Lerp(col.Transparent, col.Magenta, 0.31f)),
                        new(new(vertexs[1]  , 0), col.Lerp(col.Transparent, col.Magenta, 0.3f)),
                        new(new(vertexs[2]  , 0), col.Lerp(col.Transparent, col.Magenta, 0.35f)),
                        new(new(vertexs[3]  , 0), col.Lerp(col.Transparent, col.Magenta, 0.543f)),
                        new(new(vertexs[4]  , 0), col.Lerp(col.Transparent, col.Magenta, 0.543f)),
                        new(new(vertexs[5]  , 0), col.Lerp(col.Transparent, col.Magenta, 0.35f)),
                    }
                    );
                for (int i = 0; i < vertexs.Length; i++)
                {
                    int next = i + 1;
                    next %= vertexs.Length;
                    DrawingLab.DrawLine(vertexs[i]  , vertexs[next]  , 3.0f, col.White, 0.22f);
                }
                GLFont font = FightResources.Font.NormalFont;
                if (scoreboard == null || participants == null || participants.Length <= 0)
                {
                    // show that there is no score result

                    font.CentreDraw("Empty Scoreboard", new vec2(480, 555), col.Red, 2.515f, MathUtil.GetRadian(8.5f), 0.26f);


                }

                else
                {

                    DrawInfo();

                }
            }

            private void DrawInfo()
            {
                void DrawLine(vec2 a, vec2 b)
                {
                    DrawingLab.DrawLine(a, b, 3.0f, col.Silver, 0.5f);
                }
                for(int i = 0; i < 4; i++)
                {
                    float y = i * 52 + 475;
                    DrawLine(new(120, y), new(960 - 120, y));
                }
                GLFont font = FightResources.Font.NormalFont;
                font.CentreDraw("#rk", new(154, 456), col.Wheat, 1.1f, 0.0f, 0.5f) ;
                font.CentreDraw("Total", new(793, 456), col.Wheat, 1.1f, 0.0f, 0.5f) ;

                float[] positions = new float[participants[0].AccuracyList.Length];
                float delWord;
                const float L = 370, R = 730;

                float LMID = (L + 190) / 2f;
                font.CentreDraw("Name", new(LMID, 456), col.White, 1.15f, 0.5f);
                for(int i = 0; i < positions.Length ; i++)
                { 
                    float k = i * 1.0f / positions.Length ;
                    positions[i] = MathHelper.Lerp(L, R, k);
                }
                delWord = (R - L) * 0.5f / positions.Length;
                for(int i = 0; i < positions.Length; i++)
                {
                    DrawLine(new(positions[i], 440), new(positions[i], 672));
                    font.CentreDraw(((char)('A' + i)).ToString(), new(positions[i] + delWord, 456), col.White, 1.1f, 0.0f, 0.5f);
                }

                for(int i = indexStart, j = 0; i < participants.Length && j < 4 ; i++, j++)
                {
                    float y = j * 52 + 505;
                    float[] list = participants[i].AccuracyList;
                    for (int x = 0; x < positions.Length; x++)
                        font.CentreDraw(list[x].ToString("F2"), new(positions[x] + delWord, y), col.White, 1.06f, 0.5f);
                    font.CentreDraw((i + 1).ToString(), new(154, y), col.Wheat, 1.06f, 0.5f);
                    font.CentreDraw((participants[i].Total * 100).ToString("F2"), new(793, y), col.Wheat, 1.06f, 0.5f);

                    string name = participants[i].Name;
                    if (name.Length > 8) name = name[..3] + ".." + name[^4..];
                    font.CentreDraw(name, new(LMID, y), col.White, 1.06f, 0.5f);
                }

                DrawLine(new(190, 440), new(190, 672));
                DrawLine(new(R, 440), new(R, 672));
            }

            int indexStart = 0;

            float appearTime = 0;
            public override void Update()
            {
                appearTime += 1f;
                if(appearTime > 650)
                {
                    appearTime = 0;
                    Fetch();
                }
            }

            internal void PageLeft()
            {
                if (indexStart >= 4) indexStart -= 4;
            }

            internal void PageRight()
            {
                if (participants == null || participants.Length == 0) return;
                if(indexStart + 4 < participants.Length) indexStart += 4;
            }
        }
    }
}