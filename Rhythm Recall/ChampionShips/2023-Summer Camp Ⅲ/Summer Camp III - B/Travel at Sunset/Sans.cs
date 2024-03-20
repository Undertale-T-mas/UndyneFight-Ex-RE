using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    internal partial class Traveler_at_Sunset
    {
        public partial class Project
        {
            private class Sans : Entity
            {
                private Texture2D head;
                private Texture2D body, bodyPreR, bodyR, bodyPreB, bodyB, bodyPreU, bodyU;
                private Texture2D leg;

                public Sans(ContentManager loader)
                {
                    loader.RootDirectory = "Content\\Musics\\Traveler at Sunset\\Sans";
                    head = loader.Load<Texture2D>("head");
                    body = loader.Load<Texture2D>("body");
                    bodyPreR = loader.Load<Texture2D>("bodyPreR");
                    bodyPreB = loader.Load<Texture2D>("bodyPreB");
                    bodyPreU = loader.Load<Texture2D>("bodyPreU");
                    bodyR = loader.Load<Texture2D>("bodyR");
                    bodyB = loader.Load<Texture2D>("bodyB");
                    bodyU = loader.Load<Texture2D>("bodyU");
                    leg = loader.Load<Texture2D>("leg");
                    GeneratePart();
                    loader.RootDirectory = "Content";
                }

                public class Component : AutoEntity
                {
                    float appearTime = 0.0f;
                    public float AppearTime => appearTime;
                    public Component(Texture2D image)
                    {
                        this.Image = image;
                        UpdateIn120 = true;
                        this.Alpha = 1.0f;
                        this.Scale = 2.0f;
                    }
                    public override void Update()
                    {
                        if (_time > 0) { StateChangedTime += 0.5f; _time -= 0.5f; }
                        else this.OnUpdate?.Invoke(this);
                        appearTime += 0.5f;
                        this.Visible = Alpha >= 0.0f && (FatherObject as Entity).Visible;
                    }
                    public override void Draw()
                    {
                        if (Vertex)
                            this.SpriteBatch.DrawVertex(this.Image, this.Depth, Vertexs);
                        else
                            base.Draw();
                    }
                    public VertexPositionColorTexture[] Vertexs = new VertexPositionColorTexture[4];
                    public bool Vertex { get; set; } = false;
                    public event Action<Component> OnUpdate;

                    float _time = 0.0f;
                    public float StateChangedTime { get; private set; } = 0.0f;
                    public void ChangeState(float time, Action<Component> action)
                    {
                        StateChangedTime = 0.0f;
                        _time = time;
                        action.Invoke(this);
                        GameStates.InstanceCreate(new TimeRangedEvent(time - 0.5f,
                            () => action.Invoke(this))
                        { UpdateIn120 = true });
                    }
                }

                Component compHead, compBody, compLeg;
                public Component CompHead => compHead;
                public Component CompBody => compBody;
                public Component CompLeg => compLeg;
                Vector2 anchHead, anchBody, anchLeg;
                // up pre Height += 26;

                public float Alpha { get; set; } = 0.6f;
                public Vector2 Offset { get; set; } = new(0, 0);

                public void MoveHand(int dir)
                {
                    Vector2 curPos = compBody.Centre, curPosHead = CompHead.Centre;
                    var ease = EaseOut(8, GetVector2(28, dir * 90f) * new Vector2(1.0f, 0.5f), Vector2.Zero, EaseState.Cubic);
                    Vector2 delta = Vector2.Zero;
                    RunEase(s => delta = s, ease);
                    switch (dir)
                    {
                        case 0:
                            compBody.Image = bodyPreR;

                            compBody.ChangeState(9, (s) =>
                            {
                                s.Centre = curPos + delta;
                                if (s.StateChangedTime >= 8) s.Image = body;
                                else if (s.StateChangedTime >= 4f) s.Image = bodyR;
                            });
                            break;
                        case 1:
                            compBody.Image = bodyPreB; compBody.Anchor = anchBody + new Vector2(0, 26);

                            compBody.ChangeState(9, (s) =>
                            {
                                s.Centre = curPos + delta;
                                if (s.StateChangedTime >= 8) s.Image = body;
                                else if (s.StateChangedTime >= 4f)
                                {
                                    s.Anchor = anchBody;
                                    s.Image = bodyB;
                                }
                            });
                            break;
                        case 2:
                            compBody.Image = bodyPreR;

                            compBody.ChangeState(9, (s) =>
                            {
                                s.Centre = curPos + delta;
                                if (s.StateChangedTime >= 7) s.Image = body;
                            });
                            break;
                        case 3:
                            compBody.Image = bodyPreU; compBody.Anchor = anchBody + new Vector2(0, 26);

                            compBody.ChangeState(9, (s) =>
                            {
                                s.Centre = curPos + delta;
                                if (s.StateChangedTime >= 8)
                                {
                                    s.Anchor = anchBody; s.Image = body;
                                }
                                else if (s.StateChangedTime >= 4f)
                                {
                                    s.Image = bodyU;
                                }
                            });
                            break;
                    }

                    CompHead.ChangeState(7, (s) => { s.Centre = curPosHead + delta; });
                }

                private void GeneratePart()
                {
                    compHead = new(head) { Depth = 0.1f };
                    compBody = new(body) { Depth = 0.08f };
                    compLeg = new(leg) { Depth = 0.05f, Vertex = true };
                    this.AddChild(compHead);
                    this.AddChild(compBody);
                    this.AddChild(compLeg);
                    anchHead = compHead.Anchor;
                    anchBody = compBody.Anchor;
                    anchLeg = compLeg.Anchor;
                    compHead.Anchor = anchHead;
                    compBody.Anchor = anchBody;
                    compLeg.Anchor = anchLeg;

                    compHead.OnUpdate += CompHead_OnUpdate;
                    compBody.OnUpdate += CompBody_OnUpdate;
                    compLeg.OnUpdate += CompLeg_OnUpdate;
                }

                private void CompLeg_OnUpdate(Component obj)
                {
                    obj.Alpha = Alpha;
                    Vector2 size = obj.Image.Bounds.Size.ToVector2() * 2;
                    obj.Centre = new(320 + Offset.X, 145 + Offset.Y);
                    Vector2 pos2 = obj.Centre + size * new Vector2(0.5f, 0.5f);
                    Vector2 pos3 = obj.Centre + size * new Vector2(-0.5f, 0.5f);
                    Vector2 pos0 = obj.Centre + size * new Vector2(-0.5f, -0.5f);
                    Vector2 pos1 = obj.Centre + size * new Vector2(0.5f, -0.5f);

                    float delX = compBody.Centre.X - 320 - Offset.X;
                    pos0.X += delX; pos1.X += delX;
                    obj.Vertexs[0] = new(new(pos0, 0.1f), Color.White * Alpha, new(0, 0));
                    obj.Vertexs[1] = new(new(pos1, 0.1f), Color.White * Alpha, new(1, 0));
                    obj.Vertexs[2] = new(new(pos2, 0.1f), Color.White * Alpha, new(1, 1));
                    obj.Vertexs[3] = new(new(pos3, 0.1f), Color.White * Alpha, new(0, 1));
                }

                private void CompBody_OnUpdate(Component obj)
                {
                    obj.Alpha = Alpha;
                    obj.Centre = compHead.Centre + new Vector2(0, 49);
                }

                private void CompHead_OnUpdate(Component component)
                {
                    component.Alpha = Alpha;
                    component.Centre = Offset + new Vector2(320, 40) + new Vector2(1, 1.2f) * GetVector2(1.5f, 90 + Sin(component.AppearTime * 2.5f) * 60f);
                }

                public override void Draw()
                {
                }

                public override void Update()
                {
                }
            }
        }
    }
}