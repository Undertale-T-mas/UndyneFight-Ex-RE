using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Fight;
using UndyneFight_Ex.SongSystem;

using vec2 = Microsoft.Xna.Framework.Vector2;
using rect = UndyneFight_Ex.CollideRect;
using col = Microsoft.Xna.Framework.Color;
using VPCT = Microsoft.Xna.Framework.Graphics.VertexPositionColorTexture;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private partial class Extra : SmartSelector
        { 
            VirtualFather _vfa;
            public Extra(VirtualFather virtualFather)
            {
                _vfa = virtualFather;
                this.UpdateIn120 = true;
                this.OnActivated += Extra_OnActivated;
                vec2 v = new(-111, 2);
                this.AddChild(score = new(this, v, "Score") { Depth = 0.62f});
                this.AddChild(basicInfo = new(this, v, "Info") { Depth = 0.62f });
                this.AddChild(friend = new(this, v, "Friend") { Depth = 0.62f });
                this.OneSelectionOnly = true;
                this.OnSelected += Extra_OnSelected;
                this.KeyEvent = KeyEventFull;
            }
            Info info;
            Button score, basicInfo, friend;
            public override void Start()
            {
                base.Start();
                this.AddChild(info = new Info());
                this.Selected(score);
            }

            private void Extra_OnActivated()
            {
                coolTime = 3f;
                Functions.PlaySound(FightResources.Sounds.select);
            }

            Entity current;

            const float sizeX = 190;
            float coolTime = 0.0f;
            public override void Update()
            {
                this.collidingBox.Size = new vec2(sizeX, 41);
                this.collidingBox.X = 440 - sizeX / 2f;
                this.collidingBox.Y = pos.Y;
                if (coolTime > 0)
                    coolTime -= 0.5f;
                base.Update();
                if (this.MouseOn && this.State == SelectState.Selected && coolTime <= 0)
                {
                    if (MouseSystem.IsLeftClick())
                    {
                        this.Deactivate();
                        this.State = SelectState.MouseOn;
                        Functions.PlaySound(FightResources.Sounds.select);
                    }
                }
                this.alpha = MathHelper.Lerp(alpha, (this.State == SelectState.Selected) ? 1.0f : (MouseOn ? 0.5f : 0.35f), 0.12f);
                float v2 = this.State == SelectState.Selected ? 0.8f : 0.5f;
                c1 = col.Lerp(c1, col.Lerp(col.Yellow, col.Gray, MouseOn ? v2 : 1), 0.1f);
                c2 = col.Lerp(c1, col.Lerp(col.Gold, col.Silver, MouseOn ? v2 : 1), 0.1f);

                pos.Y = MathHelper.Lerp(pos.Y, State == SelectState.Selected ? 162 : (_vfa.CurrentDifficulty == Difficulty.NotSelected ? 741 : 681), 0.1f);
                pos.X = 440;
                info.Centre = pos;

                if (this.Activated && current != null)
                {
                    if (CurrentSelected.ModuleEnabled)
                        current.Update();
                }
            }
             
            private void Extra_OnSelected()
            {
                if(CurrentSelected == score) current = new Scoreboard(this, this._vfa);
            }

            col c1, c2;
            float alpha = 0.5f;
            vec2 pos = new(440, 686);
            public override void Draw()
            {
                if (this.Activated && current != null)
                {
                    if (CurrentSelected.ModuleEnabled)
                        current.Draw();
                }
                vec2[] points = this.collidingBox.GetVertexs();
                points[2].X += 20; points[3].X -= 20;
                float alpha2 = MathF.Pow(alpha, 1.2f);
                if (points[2].Y < 720)
                {
                    vec2[] adds = {
                        new(960 - 150, points[2].Y),
                        new(960 - 150, 965),
                        new(150, 965),
                        new(150, points[2].Y),
                    };
                    SpriteBatch.DrawSortedVertex(
                        0.58f,
                        new VPC[] {
                            new(new(points[0], 0.58f), c2 * 0.95f * alpha2),
                            new(new(points[1], 0.58f), c1 * 0.95f * alpha2),
                            new(new(points[2], 0.58f), c1 * 0.95f * alpha2),
                            new(new(points[3], 0.58f), c2 * 0.95f * alpha2)
                        }
                    );
                    col c3 = col.Lerp(c1, col.Gray, 0.5f);
                    col c4 = col.Lerp(c2, col.Gray, 0.5f);
                    SpriteBatch.DrawSortedVertex(
                        0.58f,
                        new VPC[] {  
                            new(new(points[2], 0.58f), c3 * 0.985f * alpha2),
                            new(new(adds[0], 0.58f), c3 * 0.985f * alpha2),
                            new(new(adds[1], 0.58f), new col(44, 41, 44) * 0.985f * alpha2),
                            new(new(adds[2], 0.58f), new col(44, 41, 44) * 0.985f * alpha2),
                            new(new(adds[3], 0.58f), c4 * 0.985f * alpha2),
                            new(new(points[3], 0.58f), c4 * 0.985f * alpha2)
                        }
                    );
                    DrawingLab.DrawLine(points[2], adds[0], 3f, col.White * 1f * alpha, 0.581f);
                    DrawingLab.DrawLine(adds[1], adds[0], 3f, col.White * 1f * alpha, 0.581f);
                    DrawingLab.DrawLine(points[3], adds[3], 3f, col.White * 1f * alpha, 0.581f);
                    DrawingLab.DrawLine(adds[2], adds[3], 3f, col.White * 1f * alpha, 0.581f);
                }
                else
                {
                    SpriteBatch.DrawVertex(
                        0.58f, 
                        new VPC[] {
                            new(new(points[0], 0.58f), c2 * 0.9f * alpha2),
                            new(new(points[1], 0.58f), c1 * 0.9f * alpha2),
                            new(new(points[2], 0.58f), c1 * 0.9f * alpha2),
                            new(new(points[3], 0.58f), c2 * 0.9f * alpha2)
                        }
                    );
                }
                DrawingLab.DrawLine(points[0], points[3], 3f, col.White * 1f * alpha, 0.581f);
                DrawingLab.DrawLine(points[2], points[3], 3f, col.White * 0.251f * alpha, 0.581f);
                for (int i = 0; i < 2; i++) {
                    DrawingLab.DrawLine(points[i], points[i + 1], 3f, col.White * 1f * alpha, 0.581f);
                }
                GLFont font = FightResources.Font.NormalFont;
                font.CentreDraw("Extras", this.Centre, col.White * alpha, 1.2f, 0.69f);
            }
        }
    }
}