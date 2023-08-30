using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    /// <summary>
    /// 矛使用角度制计量角度
    /// </summary>
    public class Spear : Entity, ICollideAble
    {
        public bool IsHidden { set; private get; }
        private int score = 3;
        protected float alpha = 0;

        public float Alpha { protected set => alpha = value; get => alpha; }

        private bool hasHit = false;
        private bool hasBeenInside = false;
        protected bool autoDispose = true;

        protected Color drawingColor = Color.White;
        public Color DrawingColor { set => drawingColor = value; }
        public bool MarkScore { set; private get; } = true;

        public Spear()
        {
            Depth = 0.5f;
            Image = FightResources.Sprites.spear;
        }

        public override void Draw()
        {
            FormalDraw(Image, Centre, drawingColor * alpha, GetRadian(Rotation), ImageCentre);
        }

        public override void Dispose()
        {
            if (!hasHit && MarkScore) PushScore(score);
            base.Dispose();
        }

        private static CollideRect screen = new(-50, -50, 740, 580);

        public override void Update()
        {
            controlLayer = (IsHidden ? Surface.Hidden : Surface.Normal);
            if (autoDispose)
            {
                bool ins = screen.Contain(Centre);
                if (ins && (!hasBeenInside)) hasBeenInside = true;
                if (hasBeenInside && (!ins))
                {
                    if (this is not NormalSpear)
                        Dispose();
                    else
                    {
                        var _ = this as NormalSpear;
                        if (_.Rebound && _.ReboundCount > -1)
                        {
                            /*改天看一下为啥这跑不了
                            var ReboundNum = _.ReboundVertexs.Length;
                            for (int i = 0; i < ReboundNum; i++)
                            {
                                var ThisVertex = _.ReboundVertexs[i];
                                var NextVertex = _.ReboundVertexs[Posmod(i + 1, ReboundNum)];
                                var Normal = Direction(ThisVertex, NextVertex);
                                if (GetDistance(Centre, ClosestPointOnEdge(_.Centre, ThisVertex, NextVertex)) < 6)
                                {
                                    Rotation = 2 * Normal - Rotation;
                                    _.ReboundCount--;
                                    return;
                                }
                            }*/
                            
                            var Normal = 0;
                            //Left
                            if (Centre.X <= 30)
                            {
                                Normal = 270;
                            }
                            //Left
                            if (Centre.X >= 610)
                            {
                                Normal = 90;
                            }
                            //Top
                            if (Centre.Y <= 30)
                            {
                                Normal = 0;
                            }
                            //Down
                            if (Centre.Y >= 450)
                            {
                                Normal = 180;
                            }

                            Rotation = 2 * Normal - Rotation;
                            _.ReboundCount--;
                        }
                        else Dispose();
                    }
                }
            }
        }

        private JudgementState JudgeState
        {
            get
            {
                return GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Lenient;
            }
        }
        public void GetCollide(Player.Heart heart)
        {
            if (alpha <= 0.9f) return;
            float A, B = -1, C, dist;
            if (MathF.Abs((Rotation + 90 + 180) % 180) < 0.01f)
            {
                dist = Centre.X - heart.Centre.X;
            }
            else
            {
                float k = (float)Math.Tan(GetRadian(Rotation));
                A = k;
                C = -A * Centre.X - B * Centre.Y;
                dist = (float)((A * heart.Centre.X + B * heart.Centre.Y + C) / Math.Sqrt(A * A + B * B));
            }

            float res = Math.Max(Math.Abs(dist) - 6.5f, GetDistance(heart.Centre, Centre + GetVector2(12, Rotation)) - 31 + 12);

            if (!hasHit)
            {
                int offset = 3 - (int)JudgeState;
                if (res < 0)
                {
                    //Miss
                    if (!hasHit)
                        PushScore(0);
                    LoseHP(heart);
                    hasHit = true;
                }
                else if (res <= 1.6f - offset * 0.4f)
                {
                    //Okay
                    if (score >= 2) { score = 1; heart.CreateCollideEffect2(Color.LawnGreen, 3f); }
                }
                else if (res <= 3.9f - offset * 1.1f)
                {
                    //Nice
                    if (score >= 3) { score = 2; heart.CreateCollideEffect2(Color.LightBlue, 6f); }
                }
                if (score != 3 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
                {
                    //Perfect Only
                    if (!hasHit) PushScore(0); LoseHP(heart); ; hasHit = true;
                }
            }
        }
    }
}