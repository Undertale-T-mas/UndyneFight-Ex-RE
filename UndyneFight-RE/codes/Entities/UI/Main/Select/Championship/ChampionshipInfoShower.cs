using System;
using static UndyneFight_Ex.Fight.Functions;

using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;
using UFData;
using UndyneFight_Ex.ChampionShips;
using Microsoft.Xna.Framework.Graphics;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class ChampionshipSelector
    { 
        private static string _currentChampionshipNAME;
        private class ChampionshipInfoShower : Entity
        {
            public ChampionshipInfoShower() {
                var list = FightSystem.ChampionShips;
                foreach(var obj in list)
                {
                    var state = obj.CheckTime.Invoke();
                    if (state != ChampionShip.ChampionShipStates.End)
                    {
                        // championship that can be shown

                        this.championship = obj;
                        this.info = obj.ToInfo();
                        this.Image = Loader.Load<Texture2D>(obj.IconPath);

                        _currentChampionshipNAME = obj.Title.Replace("Ⅲ", "III");

                        if (state != ChampionShip.ChampionShipStates.NotAvailable)
                            break;
                    }
                }

            }
            private static vec2[] vertexs = { 
                new(100, 80),
                new(860 - 35, 80),
                new(860, 115),
                new(860, 320),
                new(100 + 35, 320),
                new(100, 320 - 35)
            };
            vec2 delta;
            public vec2 Delta => delta;
            public override void Draw()
            {
                this.SpriteBatch.DrawSortedVertex(0.15f,
                    new VPC[] {
                        new(new(vertexs[0] + delta, 0), col.Lerp(col.Transparent, col.Magenta, 0.1f)),
                        new(new(vertexs[1] + delta, 0), col.Lerp(col.Transparent, col.Magenta, 0.1f)),
                        new(new(vertexs[2] + delta, 0), col.Lerp(col.Transparent, col.Magenta, 0.15f)),
                        new(new(vertexs[3] + delta, 0), col.Lerp(col.Transparent, col.Magenta, 0.23f)),
                        new(new(vertexs[4] + delta, 0), col.Lerp(col.Transparent, col.Magenta, 0.23f)),
                        new(new(vertexs[5] + delta, 0), col.Lerp(col.Transparent, col.Magenta, 0.15f)),
                    }
                    );
                for(int i = 0; i < vertexs.Length; i++) {
                    int next = i + 1;
                    next %= vertexs.Length;
                    DrawingLab.DrawLine(vertexs[i] +delta, vertexs[next] + delta, 3.0f, col.White, 0.22f);
                }

                GLFont font = FightResources.Font.NormalFont;
                if(info == null)
                {
                    // show the empty championship string.
                    font.CentreDraw("There is no ongoing championship", new vec2(480, 215) + delta, col.Red, 1.5f, 5f/MathF.PI*4, 0.26f);
                }
                else
                {
                    // there is a championship ongoing! show the info of it
                    font.Draw(championship.Title.Replace("Ⅲ", "III"), new vec2(132, 100) + delta, col.White, 1.35f, 0.25f);

                    string s = state.ToString();
                    font.Draw(s, new vec2(845, 100) + delta,
                        state switch { 
                         ChampionShip.ChampionShipStates.NotStart => col.Red,
                         ChampionShip.ChampionShipStates.NotAvailable => col.OrangeRed,
                         ChampionShip.ChampionShipStates.Starting => col.Gold,
                         ChampionShip.ChampionShipStates.End => col.Red,
                         _ => throw new NotSupportedException(s),
                        }
                        , 0, 1.35f,new vec2(font.SFX.MeasureString(s).X * 1.35f, 0), 0.25f);

                    this.FormalDraw(Image, new CollideRect(132 + delta.X, 161 + delta.Y, 135, 135), col.White);
                }
            }
            ChampionShip.ChampionShipStates? state;
            ChampionShip championship;
            ChampionshipInfo info;
            public ChampionshipInfo Info => info;

            public ChampionShip Championship => championship;

            float appearTime = 0f;
            public override void Update()
            {
                state = championship?.CheckTime.Invoke();
                appearTime += 0.5f;
                delta = new(Sin(appearTime * 2.5f) * 3.2f, Cos(appearTime * 3.6f) * 1.7f - 25);
            }
        }
    }
}