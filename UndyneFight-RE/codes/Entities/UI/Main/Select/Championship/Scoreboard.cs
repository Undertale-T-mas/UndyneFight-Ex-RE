using System.Text.Json;
using UFData;
using UndyneFight_Ex.Remake.Network;

using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class ChampionshipSelector
    {
        private class Scoreboard : Entity
        {
            private static vec2[] vertexs = {
                new(70, 80 + 340),
                new(890 - 35, 80 + 340),
                new(890, 115 + 340),
                new(890, 340 + 340),
                new(70 + 35, 340 + 340),
                new(70, 340 - 35 + 340)
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
            private void Fetch()
            {
                if (string.IsNullOrEmpty(_division)) return;
                UFSocket<ScoreObject> socket = new(
                    (t) =>
                        this.scoreboard = t.Data.Scoreboard
                    );
                socket.SendRequest($"Enquire\\Championship\\{_currentChampionshipNAME}");
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
                if (scoreboard == null)
                {
                    // show that there is no score result

                    font.CentreDraw("Empty Scoreboard", new vec2(480, 555), col.Red, 2.515f, MathUtil.GetRadian(8.5f), 0.26f);


                }
            }

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
        }
    }
}