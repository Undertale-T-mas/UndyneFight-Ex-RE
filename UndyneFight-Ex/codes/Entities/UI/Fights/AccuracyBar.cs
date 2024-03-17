using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static UndyneFight_Ex.FightResources;

namespace UndyneFight_Ex.Entities
{
    public class AccuracyBar : Entity
    {
        internal class MultiTag : Entity
        {
            Arrow a1, a2;
            internal MultiTag(Arrow a1, Arrow a2)
            {
                UpdateIn120 = true;
                this.a1 = a1;
                this.a2 = a2;
            }

            public override void Draw()
            {
                /* DrawingLab.DrawLine(a1.Centre + MathUtil.GetVector2(4, a1.Rotation + 180), a2.Centre + MathUtil.GetVector2(4, a2.Rotation + 180),
                     3, Color.Gold * 0.25f, 0);*/
            }

            public override void Update()
            {
                Dispose();
            }
        }
        internal class AccuracyPointer : Entity
        {
            Color drawingColor;
            float timeDel;
            float alpha = 4;
            int areaBelong;
            public AccuracyPointer(float timedel, int remark)
            {
                UpdateIn120 = true;
                if (remark == 0) areaBelong = -1;
                drawingColor = remark switch
                {
                    0 => Color.Silver,
                    1 => Color.LawnGreen,
                    2 => Color.CadetBlue,
                    3 => Color.Gold,
                    4 => Color.Orange,
                    5 => Color.Orange,
                    _ => throw new ArgumentOutOfRangeException(nameof(remark))
                };
                timeDel = timedel;
            }
            public override void Start()
            {
                controlLayer = (FatherObject as Entity).controlLayer;
                if (areaBelong == 0)
                    Centre = (FatherObject as Entity).Centre + new Vector2(MathUtil.Clamp(-70, MathUtil.SignedPow(timeDel, 1.3f) * 6f, 70), 0);
                else if (areaBelong == -1)
                    Centre = (FatherObject as Entity).Centre + new Vector2(-75, 0);
            }

            public override void Draw()
            {
                float ra = MathF.Min(alpha, 1);
                Depth = 0.2f;
                FormalDraw(Sprites.accuracyPointers[1], Centre, drawingColor * ra, 0, Sprites.accuracyPointers[1].Bounds.Size.ToVector2() / 2);
                Depth = 0.01f;
                FormalDraw(Sprites.accuracyPointers[0], Centre - new Vector2(3, 0), Color.White * ra, 0, Sprites.accuracyPointers[0].Bounds.Size.ToVector2() / 2);
                FormalDraw(Sprites.accuracyPointers[2], Centre + new Vector2(3, 0), Color.White * ra, 0, Sprites.accuracyPointers[2].Bounds.Size.ToVector2() / 2);
            }

            public override void Update()
            {
                alpha -= 0.009f;
                if (alpha < 0)
                {
                    Dispose();
                    return;
                }
            }
        }
        public AccuracyBar()
        {
            Depth = 0.15f;
            Image = Sprites.accuracyBar;
            Centre = new(320, 582);
            UpdateIn120 = true;
        }

        public override void Draw()
        {
            Depth = 0.15f;
            FormalDraw(Image, Centre, Color.White, 0, ImageCentre);
        }

        int appearTime = 0;
        public override void Update()
        {
            appearTime++;
            Centre = (Centre * 0.9f) + (new Vector2(320, 482) * 0.1f);

            if (appearTime % 4 == 0 && EnabledGolden)
            {
                Arrow[] arrows = AllArrows.ToArray();
                Array.Sort(arrows);
                for (int i = 0; i < arrows.Length; i++) arrows[i].GoldenMarkIntensity = 0;
                List<Arrow> timeSame = new();
                Action add = () =>
                {
                    if (timeSame.Count < 2)
                    {
                        timeSame.Clear();
                        return;
                    }
                    int[] counts = { 0, 0, 0, 0 };
                    timeSame.ForEach(s => { s.GoldenMarkIntensity = 1; counts[s.Way]++; });
                    for (int i = 0; i < 4; i++)
                    {
                        if (counts[i] <= 1) continue;
                        timeSame.ForEach(s => { if (s.Way == i) s.GoldenMarkIntensity = 2; });
                    }
                    if (EnabledLine)
                    {
                        Arrow[] cur = timeSame.ToArray();
                        for (int i = 1; i < cur.Length; i++)
                        {
                            if ((cur[i].Way - cur[i - 1].Way + 40) % 4 == 2) continue;
                            AddChild(new MultiTag(cur[i], cur[i - 1]));
                        }
                    }
                    timeSame.Clear();
                };
                for (int i = 0; i < arrows.Length; i++)
                {
                    if (arrows[i].BlockTime - Fight.Functions.GametimeF > MaxTime) break;
                    if (i != 0 && arrows[i].BlockTime - arrows[i - 1].BlockTime > SpecifyTime)
                    {
                        add();
                    }
                    timeSame.Add(arrows[i]);
                }
                add();
            }
        }
        public float SpecifyTime { get; set; } = 0.6f;
        public bool EnabledGolden { get; set; } = true;
        public bool EnabledLine { get; set; } = true;
        public float MaxTime { get; set; } = 150f;

        public List<Arrow> AllArrows { get; init; } = new();
        public Dictionary<string, List<Arrow>> TaggedArrows { get; init; } = new();

        public void PushDelta(float time, int remark, int col, int way, Player.Heart.ShieldManager shieldManager)
        {
            AddChild(new AccuracyPointer(time, remark));
            shieldManager.ShieldShine(way, col, remark);
        }
    }
}