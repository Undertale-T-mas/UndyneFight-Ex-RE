using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static Extends.DrawingUtil;
using static UndyneFight_Ex.DrawingLab;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class BadApple : WaveConstructor, IWaveSet
    {
        static BadApple game;
        public BadApple() : base(6.7945f) { game = this; }
        private class FakeBox : Entity
        {
            public FakeBox(CollideRect area, int duration)
            {
                collidingBox = area;
                this.duration = duration;
            }

            private readonly int duration;
            private int appearTime = 0;
            private float alpha = 0;

            public float Alpha
            {
                set
                {
                    alpha = value;
                }
            }

            public override void Draw()
            {
                DrawRectangle(collidingBox, Color.Gold * (alpha + Sin(Gametime / game.BeatTime(16) * 360) * 0.5f * MathHelper.Min(1, appearTime / 100f)), 4, 0.2f);
            }

            public override void Update()
            {
                if (appearTime >= duration) Dispose();
                appearTime++;
                if (alpha < 1f)
                    alpha += 0.005f;
            }
        }

        private class FireWorkerT : Entity
        {
            public int ColorType
            {
                set => colorType = value;
            }

            private int colorType = 0;
            private int appearTime = 0;
            public FireWorkerT(Vector2 centre, int count, float speed, float duration, Color color)
            {
                Centre = centre;
                this.count = count;
                this.speed = speed;
                this.duration = duration;

                rotations = new float[count];
                sizes = new float[count];
                speeds = new float[count];
                for (int i = 0; i < count; i++)
                {
                    rotations[i] = Rand(0, 359);
                    speeds[i] = Rand(20, 50) / 10f;
                    sizes[i] = Rand(4, 9) / 10f;
                }

                drawingColor = color;
            }

            private Color drawingColor;
            private readonly float[] rotations;
            private readonly float[] speeds;
            private readonly float[] sizes;
            private readonly int count;
            private readonly float speed;
            private readonly float duration;
            private float timeLeft;

            public override void Update()
            {
                appearTime++;
                timeLeft = duration - appearTime;
                if (timeLeft < 0) Dispose();
            }

            public override void Dispose()
            {
                float add = Rand(0, 359);
                float per = 360f / count;
                for (int i = 0; i < count; i++)
                {
                    CreateBone(new FireWorkBone(Centre, speed, add + per * i) { ColorType = colorType });
                }
                PlaySound(Sounds.pierce);
                base.Dispose();
            }

            public override void Draw()
            {
                for (int i = 0; i < count; i++)
                {
                    FormalDraw(Sprites.lightBall, Centre + GetVector2(speeds[i]
                        * timeLeft, rotations[i]), drawingColor * MathHelper.Min(0.7f, appearTime / (duration / 1.3f)), sizes[i], 0, new Vector2(10, 10));
                }
            }

            private class FireWorkBone : Bone
            {
                private Vector2 speed;
                public FireWorkBone(Vector2 centre, float speed, float rotation)
                {
                    IsMasked = false;
                    Length = 30;
                    Centre = centre;
                    this.speed = GetVector2(speed, rotation);
                    Rotation = rotation + 90;
                }

                public override void Update()
                {
                    Centre += speed / 2;
                    if (alpha < 1) alpha += 0.1f;
                    base.Update();
                }

                public override void Draw()
                {
                    base.Draw();
                }
            }
        }
        private class FireWorker : Entity
        {
            public int ColorType
            {
                set => colorType = value;
            }

            private int colorType = 0;
            private int appearTime = 0;
            public FireWorker(Vector2 centre, int count, float speed, float duration, Color color)
            {
                Centre = centre;
                this.count = count;
                this.speed = speed;
                this.duration = duration;

                rotations = new float[count];
                sizes = new float[count];
                speeds = new float[count];
                for (int i = 0; i < count; i++)
                {
                    rotations[i] = Rand(0, 359);
                    speeds[i] = Rand(20, 50) / 10f;
                    sizes[i] = Rand(4, 9) / 10f;
                }

                drawingColor = color;
            }

            private Color drawingColor;
            private readonly float[] rotations;
            private readonly float[] speeds;
            private readonly float[] sizes;
            private readonly int count;
            private readonly float speed;
            private readonly float duration;
            private float timeLeft;

            public override void Update()
            {
                appearTime++;
                timeLeft = duration - appearTime;
                if (timeLeft < 0) Dispose();
            }

            public override void Dispose()
            {
                float add = Rand(0, 359);
                float per = 360f / count;
                for (int i = 0; i < count; i++)
                {
                    CreateBone(new FireWorkBone(Centre, speed, add + per * i + Rand(-3, 3)) { ColorType = colorType });
                }
                PlaySound(Sounds.pierce);
                base.Dispose();
            }

            public override void Draw()
            {
                for (int i = 0; i < count; i++)
                {
                    FormalDraw(Sprites.lightBall, Centre + GetVector2(speeds[i]
                        * timeLeft, rotations[i]), drawingColor * MathHelper.Min(0.7f, appearTime / (duration / 1.3f)), sizes[i], 0, new Vector2(10, 10));
                }
            }

            private class FireWorkBone : Bone
            {
                private Vector2 speed;
                public FireWorkBone(Vector2 centre, float speed, float rotation)
                {
                    IsMasked = false;
                    Length = 30;
                    Centre = centre;
                    this.speed = GetVector2(speed, rotation);
                    Rotation = rotation + 90;
                }

                public override void Update()
                {
                    Centre += speed / 2;
                    if (alpha < 1) alpha += 0.1f;
                    base.Update();
                }

                public override void Draw()
                {
                    base.Draw();
                }
            }
        }

        private class BoxPiece : Entity
        {
            public BoxPiece(Vector2 centre)
            {
                w = Rand(-10, 10) / 100f;
                Centre = centre;
                Rotation = 0;
                Image = Sprites.boxPiece;
                speed = new Vector2(Rand(-20, 20) / 10f, Rand(-30, 20) / 10f);
            }

            private Vector2 speed;
            private readonly float w;

            public override void Draw()
            {
                FormalDraw(Image, Centre, Color.White, 1, Rotation, ImageCentre);
            }

            public override void Update()
            {
                Rotation += w;
                speed.Y += 0.16f;
                Centre += speed;
            }
        }

        public string FightName => "Bad apple";

        public string Music => "BadApple";

        class ThisInformation : SongInformation
        {
            public override string BarrageAuthor => "T-mas";
            public override string SongAuthor => "Alstroemeria Records";
            public override string PaintAuthor => "OtokP";

            public override string AttributeAuthor => GameStates.difficulty == 4 ? "T-mas + TK" : "T-mas";

            public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.0f),
                            new(Difficulty.Easy, 6.5f),
                            new(Difficulty.Normal, 10.0f),
                            new(Difficulty.Hard, 16.0f),
                            new(Difficulty.Extreme, 17.4f),
                    }
                );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 3.0f),
                            new(Difficulty.Easy, 6.5f),
                            new(Difficulty.Normal, 10.0f),
                            new(Difficulty.Hard, 16.0f),
                            new(Difficulty.Extreme, 16.7f),
                    }
                );
            public override Dictionary<Difficulty, float> APDifficulty => new(
                      new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 7.0f),
                            new(Difficulty.Easy, 10.0f),
                            new(Difficulty.Normal, 16.0f),
                            new(Difficulty.Hard, 19.0f),
                            new(Difficulty.Extreme, 21.9f),
                    }
                );
        }
        public SongInformation Attributes => new ThisInformation();

        public void Normal()
        {
            if (InBeat(0.1f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 0, 1, 0, 1, 0, 2, 2 };

                Fortimes(8, (y) =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1 || (rhythms[x] == 2 && y % 2 == 1))
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        else if (rhythms[x] == 2 && y % 2 == 0)
                        {
                            CreateArrow(time + BeatTime(1), Rand(0, 3), 5.0f, 0, 0);
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(128f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 2, 1, 2 };

                Fortimes(4, (y) =>
                {
                    Fortimes(16, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        else if (rhythms[x] == 2)
                        {
                            CreateArrow(time + BeatTime(1), Rand(0, 3), 5.0f, 0, 0);
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(266))
            {
                SetSoul(3);
                SetBox(290, 140, 140);
            }
            if (InBeat(266 + 4, 394 - 16) && At0thBeat(6))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new CustomBone(new Vector2(320, 180), Motions.PositionRoute.linear, 0, 20)
                {
                    PositionRouteParam = new float[] { 0, 1.8f }
                });
            }
            if (InBeat(266 + 4, 394 - 20) && AtKthBeat(8, 3))
            {
                if (Rand(0, 1) == 0)
                {
                    CreateGB(new NormalGB(new Vector2(350, 430), Heart.Centre, Vector2.One, 270, BeatTime(16), BeatTime(2)));
                }
                else
                {
                    CreateGB(new NormalGB(new Vector2(290, 430), Heart.Centre, Vector2.One, 270, BeatTime(16), BeatTime(2)));
                }
            }
            if (InBeat(394))
            {
                SetBox(290, 160, 160);
                CreateEntity(new Boneslab(0, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(90, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(180, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(270, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateBone(new CentreCircleBone(0, 4.8f, 150, BeatTime(126)));
            }
            if (InBeat(522))
            {
                CreateEntity(new Boneslab(0, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(90, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(180, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(270, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
            }
            if (InBeat(522 + 4, 650 - 4) && AtKthBeat(16, BeatTime(2)))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new DownBone(false, 3.0f, 30));
                CreateBone(new UpBone(false, 3.0f, 80));
            }
            if (InBeat(522 + 4, 650 - 4) && AtKthBeat(16, BeatTime(10)))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(false, 3.0f, 30));
                CreateBone(new DownBone(false, 3.0f, 80));
            }
            if (InBeat(650))
            {
                CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(0), BeatTime(124)) { ColorType = 2 });
                CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(8), BeatTime(124)) { ColorType = 2 });
                CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(4), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(12), BeatTime(124)) { ColorType = 1 });
            }
            if (InBeat(772))
            {
                SetSoul(0);
            }
            if (InBeat(772 + 4, 906 - 14) && AtKthBeat(16, BeatTime(10.5f)))
            {
                CreateEntity(new FireWorker(new Vector2(170, 130), 14, 4, BeatTime(8), Color.Yellow));
                CreateEntity(new FireWorker(new Vector2(470, 130), 14, 4, BeatTime(8), Color.Yellow));
                CreateEntity(new FireWorker(new Vector2(170, 130), 6, 4, BeatTime(8), Color.Blue) { ColorType = 1 });
                CreateEntity(new FireWorker(new Vector2(470, 130), 6, 4, BeatTime(8), Color.Blue) { ColorType = 1 });
            }
            if (InBeat(772 + 4, 906 - 14) && AtKthBeat(16, BeatTime(2.5f)))
            {
                CreateEntity(new FireWorker(new Vector2(80, 350), 14, 4, BeatTime(8), Color.Red));
                CreateEntity(new FireWorker(new Vector2(80, 350), 6, 4, BeatTime(8), Color.Blue) { ColorType = 1 });
                CreateEntity(new FireWorker(new Vector2(560, 350), 6, 4, BeatTime(8), Color.Blue) { ColorType = 1 });
                CreateEntity(new FireWorker(new Vector2(560, 350), 14, 4, BeatTime(8), Color.Red));
            }
            if (InBeat(896f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 0, 0, 0 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                            CreateArrow(time, LastRand, 5.0f, 1, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(900f))
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (InBeat(1024f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 2, 1, 2 };

                Fortimes(4, (y) =>
                {
                    Fortimes(16, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                        }
                        else if (rhythms[x] == 2)
                        {
                            CreateArrow(time + BeatTime(1), Rand(0, 3), 6.0f, 0, 0);
                            CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(1162))
            {
                Heart.PurpleLineCount = 7;
                SetBox(290, 150, 150);
                SetSoul(4);
            }
            if (InBeat(1162 + 4, 1290 - 12) && At0thBeat(2))
            {
                CreateGB(new NormalGB(GetVector2(150, Gametime * 2.3f) + FightBox.instance.Centre, FightBox.instance.Centre, new Vector2(1.0f, 0.5f), Gametime * 2.3f + 180, 40, 20));
            }
            if (InBeat(1290))
            {
                SetBox(290, 300, 160);
                Heart.PurpleLineCount = 5;
            }
            if (InBeat(1290 + 4, 1418 - 12) && At0thBeat(4))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new CustomBone(new Vector2(170, 290), Motions.PositionRoute.XAxisSin, 0, 26)
                {
                    PositionRouteParam = new float[] { 4, 65, BeatTime(16), Rand(0, (int)BeatTime(16)) }
                });
            }
            if (InBeat(1418))
            {
                SetBox(290, 240, 160);
                Heart.PurpleLineCount = 3;
            }
            if (InBeat(1418 + 4, 1546 - 4) && At0thBeat(6))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(false, 3f, 80));
                CreateBone(new DownBone(true, 3f, 80));
            }
            if (InBeat(1546 + 4, 1674 - 14) && At0thBeat(8))
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
                CreateGB(new NormalGB(Heart.Centre + GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
            }
            if (InBeat(1669))
            {
                SetSoul(1);
                SetGreenBox();
                TP();
            }
            if (InBeat(1664f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 0, 0, 0 };

                Fortimes(16, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                            CreateArrow(time, LastRand, 5.0f, 1, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
        }

        private static float posX = 140;
        private static float veloX = 20;
        private static float accX = -0.5f;

        private static List<Bone> disposes;

        public void Extreme()
        {
            if (InBeat(0.1f))
            {
                float time = BeatTime(10);
                Fortimes(27, (i) =>
                {
                    CreateArrow(time, 2 * (i % 2), 5.0f, 1, 1);
                    time += BeatTime(4);
                });
            }
            if (InBeat(14))
            {
                CreateBone(new CustomBone(Vector2.Zero, (s) =>
                {
                    float v = Sin(360f * s.AppearTime / BeatTime(32)) * 0.5f + 0.5f;
                    float xPos = v * FightBox.instance.CollidingBox.Width + (FightBox.instance as RectangleBox).Left;
                    return new Vector2(xPos, FightBox.instance.Centre.Y);
                }, 0, 160, BeatTime(88))
                { ColorType = 2, AlphaIncrease = true });
                CreateBone(new CustomBone(Vector2.Zero, (s) =>
                {
                    float v = Sin(360f * s.AppearTime / BeatTime(32)) * -0.5f + 0.5f;
                    float xPos = v * FightBox.instance.CollidingBox.Width + (FightBox.instance as RectangleBox).Left;
                    return new Vector2(xPos, FightBox.instance.Centre.Y);
                }, 0, 160, BeatTime(88))
                { ColorType = 2, AlphaIncrease = true });
            }
            if (InBeat(10, 138 - 32))
            {
                float xc = 320 + Sin(Gametime / BeatTime(16) * 360) * 50;
                SetBox(xc - 90, xc + 90, 180, 360);
            }
            if (InBeat(138 - 31))
            {
                Heart.Speed = 5f;
                CreateEntity(new Boneslab(90, 30, (int)BeatTime(3), 5));
                SetSoul(2);
                Heart.GiveForce(90, 5);
                SetBox(110, 210, 240, 340);
                posX = 20;
                veloX = 20;
                accX = 0.22f;
            }
            if (InBeat(138 - 24))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new DownBone(true, 5, 50));
            }
            if (InBeat(138 - 20))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(true, 5, 50));
            }
            if (InBeat(138 - 32, 138))
            {
                if (InBeat(138 - 16))
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        CreateBone(new DownBone(true, i * 160 + 20, 0, 44));
                        CreateBone(new UpBone(true, i * 160 + 100, 0, 44));
                    }
                }
                if (InBeat(138 - 16, 134.3f))
                {
                    var YTop = veloX > 0 ? 240 : 190;
                    SetBox(posX, posX + 100, YTop, YTop + 100);
                    posX += veloX;
                    veloX -= accX;
                }
                ScreenDrawing.ScreenAngle = 90 - (float)Math.Pow(0.85f, Gametime - BeatTime(138 - 32)) * 90;
            }
            if (InBeat(10, 138 - 40) && At0thBeat(8))
            {
                for (int i = 0; i < 18; i++)
                {
                    CreateBone(new SideCircleBone(i * 20, -3, 80, BeatTime(7)));
                }
            }
            if (InBeat(10, 138 - 40) && At0thBeat(4))
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(100, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.4f), BeatTime(8), 6));
            }
            if (InBeat(134, 166))
            {
                ScreenDrawing.ScreenAngle = (float)Math.Pow(0.85f, Gametime - BeatTime(134)) * 90;
            }
            if (InBeat(134))
            {
                ScreenDrawing.BoxBackColor = Color.White;
                ScreenDrawing.BackGroundColor = Color.White;
                ScreenDrawing.ThemeColor = Color.Black;
                ScreenDrawing.UIColor = Color.Black;
            }
            if (InBeat(138))
            {
                Heart.Speed = 3;
                Heart.EnabledRedShield = false;
                Heart.RotateTo(0);
                SetSoul(4);
                Heart.PurpleLineCount = 5;
                InstantSetBox(-1000, 200, 200);

                ScreenDrawing.BoxBackColor = Color.Black;
                ScreenDrawing.BackGroundColor = Color.Black;
                ScreenDrawing.ThemeColor = Color.White;
                ScreenDrawing.UIColor = Color.White;
            }
            if (InBeat(138.4f, 138 + 128 - 10))
            {
                float posX = (Gametime - BeatTime(138.4f)) * 0.2f;
                float posY = Sin(Gametime / BeatTime(16) * 360) * 15;
                SetBox(400 - posX, 640 - posX, 150 + posY, 270 + posY);
                CreateEntity(new Particle(Color.White * Rand(0.3f, 0.9f), new Vector2(0), 3, new Vector2(Rand(0, 640), Rand(0, 480))) { Depth = 2 });
            }
            if (InBeat(138.4f, 138 + 128 - 16) && At0thBeat(8))
            {
                PlaySound(Sounds.pierce);
                float height = Rand(1, 5) * 20;
                CreateBone(new UpBone(false, 2.5f, height - 32));
                CreateBone(new DownBone(false, 2.5f, 120 - height - 32));
            }
            if (InBeat(138 + 64))
            {
                Vector2 c1 = new(160, 200);
                for (int i = 0; i < 6; i++)
                {
                    int step = i;
                    CreateEntity(new FireWorker(c1, 6, 7.0f, BeatTime(64 - 8f + step * 1.2f), Color.Gold * 0.5f));
                    CreateEntity(new FireWorker(c1, 4, 7.0f, BeatTime(64 - 8f + step * 1.2f), Color.Gold * 0.5f) { ColorType = 1 });
                }
                CreateEntity(new FakeBox(new CollideRect(100, 300, 220, 120), (int)BeatTime(63)));
            }
            if (InBeat(138.4f, 138 + 128 - 16) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(Sounds.pierce);
                float height = Rand(1, 5) * 20;
                CreateBone(new UpBone(true, 2.5f, height - 32));
                CreateBone(new DownBone(true, 2.5f, 120 - height - 32));
            }
            if (InBeat(138 + 128 - 7.7f))
            {
                ScreenDrawing.ScreenPositionDelta = new Vector2(110, -120) * 1.0f;
                ScreenDrawing.ScreenScale = 1.7f;
                SetSoul(0);
                SetBox(100, 320, 300, 420);
            }
            if (InBeat(266))
            {
                SetSoul(3);
                SetBox(290, 160, 160);
                ScreenDrawing.ScreenScale = 1.0f;
                ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
            }
            if (InBeat(266 + 4, 394 - 4))
            {
                ScreenDrawing.ScreenPositionDelta = new Vector2(Sin((Gametime - BeatTime(14)) / BeatTime(16) * 360) * 60, 0);
                float posY = Sin(Gametime / BeatTime(16) * 360 + 270) * 20;
                SetBox(240, 400, 230 + posY, 370);
            }
            if (InBeat(266 + 4, 394 - 4) && At0thBeat(4))
            {
                CreateBone(new UpBone(false, 3f, 60));
                CreateBone(new DownBone(true, 2f, 55) { ColorType = 1 });
                CreateBone(new DownBone(false, 4f, 55) { ColorType = 2 });
            }
            if (InBeat(266, 394 - 8) && At0thBeat(4f))
            {
                PlaySound(Sounds.pierce);
                ScreenDrawing.CameraEffect.Convulse(10, BeatTime(2.9f), At0thBeat(7.6f));
                /*AddInstance(new TimeRangedEvent(1, () =>
                {
                    Linerotate line = new(Rand(0, 640), 240, 270, BeatTime(16), 1, Color.White);
                    CreateEntity(line);
                    AlphaLerp(line, BeatTime(16), 0, 0.1f);
                }));*/
            }
            if (InBeat(394 - 8, 394 - 1) && At0thBeat(1))
            {
                ScreenDrawing.ScreenAngle = Rand(-60, 60);
                ScreenDrawing.ScreenScale = (Gametime - BeatTime(394 - 10)) / BeatTime(6) / 4 + 1;
            }
            if (InBeat(394, 522 - 4))
            {
                ScreenDrawing.ScreenScale = Sin((Gametime - BeatTime(10)) / BeatTime(8) * 360) * 0.2f + 1.2f;
                ScreenDrawing.ScreenAngle = Sin((Gametime - BeatTime(10)) / BeatTime(16) * 360) * 35f;
                SetBox(290, 160, 160);
                if (At0thBeat(8))
                {
                    for (int i = 0; i < 18; i++)
                    {
                        CreateBone(new SideCircleBone(i * 20, -4, 50, BeatTime(2)));
                    }

                }
            }
            if (InBeat(522 - 4, 522))
            {
                ScreenDrawing.ScreenScale = ScreenDrawing.ScreenScale * 0.8f + 0.2f;
                ScreenDrawing.ScreenAngle *= 0.8f;
            }
            if (InBeat(396))
            {
                SetSoul(0);
                Heart.Speed = 3f;
                CreateBone(new CentreCircleBone(0, -7, 150, BeatTime(122)) { ColorType = 2 });
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                        CreateBone(new CustomBone(new Vector2(320, 290), Motions.PositionRoute.circle, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                        {
                            PositionRouteParam = new float[] { 56, 1.2f, i * 120 },
                            LengthRouteParam = new float[] { 100, BeatTime(118) },
                            RotationRouteParam = new float[] { -1.5f, j * 90 },
                            AlphaIncrease = true
                        });
            }
            if (InBeat(522))
            {
                SetBox(290, 320, 140);
                ScreenDrawing.ScreenScale = 1.0f;
                ScreenDrawing.ScreenAngle = 0f;
            }
            if (InBeat(524f))
            {
                SetSoul(4);
                Heart.PurpleLineCount = 7;
            }
            if (InBeat(522, 650))
            {
                float d = Sin((Gametime - BeatTime(10)) / BeatTime(8) * 360) * 35f;
                float d2 = Sin((Gametime - BeatTime(10)) / BeatTime(12) * 360) * 35f;
                ScreenDrawing.ScreenPositionDelta = new Vector2(d, d2);
                if (InBeat(526))
                {
                    CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(0), BeatTime(120)) { ColorType = 2 });
                    CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(8), BeatTime(120)) { ColorType = 2 });
                    CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(4), BeatTime(120)) { ColorType = 1 });
                    CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(12), BeatTime(120)) { ColorType = 1 });
                }
                if (At0thBeat(8) && InBeat(526, 640))
                {
                    CentreEasing.EaseBuilder builder2 = new();
                    builder2.Insert(BeatTime(4), CentreEasing.EaseInQuad(new(0, 320), new(640, 320), BeatTime(4)));
                    ValueEasing.EaseBuilder builder3 = new();
                    builder3.Insert(BeatTime(4), ValueEasing.Stable(90));
                    Line line = new(builder2.GetResult(), builder3.GetResult());
                    ValueEasing.EaseBuilder builder = new();
                    builder.Insert(BeatTime(4), ValueEasing.EaseInSine(1.0f, 0.0f, BeatTime(4)));
                    builder.Adjust = false;
                    line.TransverseMirror = true;
                    builder.Run(s => line.Alpha = s);
                    CreateEntity(line);
                    DelayBeat(6, () =>
                    {
                        line.Dispose();
                    });
                    PlaySound(Sounds.pierce);
                    if (InBeat(526, 624))
                    {
                        float rand = Rand(0, (int)BeatTime(32));
                        for (int i = -2; i <= 2; i++)
                            CreateBone(new CustomBone(new Vector2(160, 290 + i * 70), Motions.PositionRoute.XAccAxisSin, 0, 26)
                            {
                                PositionRouteParam = new float[] { 0.3f, 0.04f, 85, BeatTime(24), rand }
                            });
                    }
                }
            }
            if (InBeat(644))
            {
                PlaySound(Sounds.heal);
                HeartAttribute.MaxHP = 92;
                SetBox(-200, 100, 100);
                SetSoul(2);
            }
            if (InBeat(650))
            {
                InstantSetBox(1000, 100, 100);
                SetSoul(2);
            }
            if (InBeat(646, 648))
            {
                Surface.Normal.drawingAlpha = Surface.Normal.drawingAlpha * 0.9f + 0.04f;
            }
            if (InBeat(652, 660))
            {
                Surface.Normal.drawingAlpha = Surface.Normal.drawingAlpha * 0.8f + 0.2f;
            }

            if (InBeat(648, 652))
            {
                int rd = (int)Rand(-BeatTime(2), BeatTime(6));
                float time = Gametime - BeatTime(648);
                ScreenDrawing.BackGroundColor = rd < time ? Color.White : Color.Gray;
            }
            if (InBeat(652f))
            {
                ScreenDrawing.BackGroundColor = Color.White;
                ScreenDrawing.UIColor = Color.Black;
                SetBox(290, 160, 160);
            }
            if (InBeat(654))
            {
                for (int i = 0; i < 6; i++)
                {
                    CreateBone(new SideCircleBone(i * 60, 1, 48, BeatTime(120)));
                }
            }
            if (InBeat(650.4f, 777f))
            {
                //ShaderParam = 0.0f;
                float val = (Gametime - BeatTime(10)) / BeatTime(8) * 45;
                ScreenDrawing.ScreenAngle = val;
                Heart.InstantSetRotation(val);
                if (InBeat(650.4f, 768))
                {
                    if (AtKthBeat(16, BeatTime(4)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 5, 154) { ColorType = 1 });
                    }
                    if (AtKthBeat(16, BeatTime(12)))
                    {
                        PlaySound(Sounds.pierce);
                        CreateBone(new UpBone(false, 5, 154) { ColorType = 2 });
                    }
                }
            }
            if (InBeat(777.2f))
            {
                SetSoul(0);
                Heart.Rotation = ScreenDrawing.ScreenAngle = 0;
                ScreenDrawing.ScreenPositionDelta = Vector2.Zero;
                Heart.EnabledRedShield = false;
                ScreenDrawing.BackGroundColor = Color.Black;
                ScreenDrawing.UIColor = Color.White;
            }
            if (InBeat(778.2f))
            {
                Heart.RotateTo(0);
                Heart.Rotation = ScreenDrawing.ScreenAngle = 0;
                SetBox(80, 300, 290, 410);
                TP(190, 350);
                Heart.Split();
                SetBox(340, 560, 290, 410);
                TP(450, 350);
            }
            if (InBeat(779f, 906 - 24))
            {
                if (AtKthBeat(8, 0))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new CustomBone(new Vector2(80, 350), Motions.PositionRoute.XAxisSin, 0, 14)
                    {
                        PositionRouteParam = new float[] { 1.5f, 53, BeatTime(32), Rand(0, (int)BeatTime(32)) }
                    });
                }
                if (AtKthBeat(8, BeatTime(4)))
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new CustomBone(new Vector2(560, 350), Motions.PositionRoute.XAxisSin, 0, 14)
                    {
                        PositionRouteParam = new float[] { -1.5f, 53, BeatTime(32), Rand(0, (int)BeatTime(32)) }
                    });
                }
                if (AtKthBeat(32, BeatTime(0)))
                {
                    Player.heartInstance = Player.hearts[0];
                    SetSoul(4);
                    Player.heartInstance = Player.hearts[1];
                    SetSoul(0);
                    ScreenDrawing.Shaders.RGBSplitting production1 = new(0.9f)
                    {
                        Disturbance = false,
                        Intensity = 8.0f,
                        RandomDisturb = 2
                    };
                    ScreenDrawing.SceneRendering.InsertProduction(production1);
                    AddInstance(new TimeRangedEvent(BeatTime(1), 1, () =>
                    {
                        production1.Dispose();
                    }));
                }
                if (AtKthBeat(32, BeatTime(16)))
                {
                    Player.heartInstance = Player.hearts[1];
                    SetSoul(4);
                    Player.heartInstance = Player.hearts[0];
                    SetSoul(0);
                    ScreenDrawing.Shaders.RGBSplitting production1 = new(0.9f)
                    {
                        Disturbance = false,
                        Intensity = 8.0f,
                        RandomDisturb = 2
                    };
                    ScreenDrawing.SceneRendering.InsertProduction(production1);
                    AddInstance(new TimeRangedEvent(BeatTime(1), 1, () =>
                    {
                        production1.Dispose();
                    }));
                }
            }
            if (InBeat(906.1f))
            {
                Player.hearts[0].RotateTo(180);
                Player.hearts[0].ChangeColor(2);
                Player.hearts[1].ChangeColor(2);

                FightBox.instance = FightBox.boxs[0];
                SetBox(200, 120, 120);
                TP(320, 200);

                FightBox.instance = FightBox.boxs[1];
                SetBox(330, 120, 120);
                TP(320, 330);
            }
            if (InBeat(906.1f))
            {
                for (int i = -1; i <= 1; i++)
                {
                    CreateBone(new CustomBone(new Vector2(i * 100 + 295, 140), Motions.PositionRoute.cameFromUp, 0, 150, BeatTime(120)));
                    CreateBone(new CustomBone(new Vector2(i * 100 + 345, 390), Motions.PositionRoute.cameFromDown, 0, 150, BeatTime(120)));
                }
                Player.heartInstance = Player.hearts[0];
                TP(320, 200);
                Player.heartInstance = Player.hearts[1];
                TP(320, 330);

                FightBox.instance = FightBox.boxs[0];
                CreateEntity(new Boneslab(180, 40, (int)BeatTime(14), (int)BeatTime(110)));
                FightBox.instance = FightBox.boxs[1];
                CreateEntity(new Boneslab(0, 40, (int)BeatTime(14), (int)BeatTime(110)));
            }
            if (InBeat(906.1f, 906 + 128))
            {
                float d = Sin((Gametime - BeatTime(10)) / BeatTime(128) * 360) * 162f;

                FightBox.instance = FightBox.boxs[0];
                SetBox(260 - d, 380 - d, 140, 260);

                FightBox.instance = FightBox.boxs[1];
                SetBox(260 - d, 380 - d, 270, 390);
            }
            if (InBeat(1034.1f, 1034 + 128 - 12))
            {
                float d = Sin((Gametime - BeatTime(10)) / BeatTime(64) * 360) * 20f;
                float e = Cos((Gametime - BeatTime(10)) / BeatTime(64) * 360) * 70f;

                FightBox.instance = FightBox.boxs[0];
                SetBox(250 - e, 390 - e, 195 - 80 - d, 345 - 80 - d);

                FightBox.instance = FightBox.boxs[1];
                SetBox(250 + e, 390 + e, 195 + 80 + d, 345 + 80 + d);
            }
            if (InBeat(1034))
            {
                PlaySound(Sounds.heal);
                HeartAttribute.MaxHP = 92;
                Player.heartInstance = Player.hearts[0];
                SetSoul(0);
                TP(230, 265);
                Player.heartInstance = Player.hearts[1];
                SetSoul(0);
                TP(410, 265);
            }
            if (InBeat(1034 + 4, 1034 + 128 - 16) && AtKthBeat(8, 0))
            {
                PlaySound(Sounds.pierce);
                FightBox.instance = FightBox.boxs[0];
                CreateBone(new DownBone(false, 3, 66));
                CreateBone(new UpBone(false, 3, 32));
            }
            if (InBeat(1034 + 4, 1034 + 128 - 16) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(Sounds.pierce);
                FightBox.instance = FightBox.boxs[1];
                CreateBone(new DownBone(true, 3, 66));
                CreateBone(new UpBone(true, 3, 32));
            }
            if (InBeat(889.1f, 906 + 128 - 24) && At0thBeat(8))
            {
                CreatePlatform(new Platform(1, new Vector2(50, 190), Motions.PositionRoute.linear, 0, 41)
                {
                    PositionRouteParam = new float[] { 1.7f, 0 }
                });
                CreatePlatform(new Platform(1, new Vector2(590, 340), Motions.PositionRoute.linear, 180, 41)
                {
                    PositionRouteParam = new float[] { -1.7f, 0 }
                });
            }
            if (InBeat(1162 - 8))
            {
                SetPlayerMission(0);
                Heart.Split();
                SetPlayerMission(1);
                Heart.Split();
                SetBoxMission(0);
                SetBox(80, 220, 80, 200);
                SetBoxMission(1);
                SetBox(420, 560, 80, 200);
                SetBoxMission(2);
                SetBox(80, 220, 280, 400);
                SetBoxMission(3);
                SetBox(420, 560, 280, 400);
            }
            if (InBeat(1162 - 4))
            {
                SetBoxMission(0);
                SetBox(80 - 550, 200 - 550, 80, 200);
                SetBoxMission(1);
                SetBox(440 + 550, 560 + 550, 80, 200);
                SetBoxMission(2);
                SetBox(80 - 550, 200 - 550, 280, 400);
                SetBoxMission(3);
                SetBox(440 + 550, 560 + 550, 280, 400);
            }
            if (InBeat(1162))
            {
                for (int i = 0; i < 4; i++)
                {
                    SetBoxMission(i);
                    InstantSetBox(240, 400, 600, 740);
                }
                for (int i = 1; i < 4; i++)
                {
                    Player.hearts[i].Merge(Player.hearts[0]);
                }
            }
            if (InBeat(1162 + 1f))
            {
                SetSoul(0);
                SetBoxMission(0);
                SetPlayerMission(0);
                SetBox(290, 160, 160);
                Heart.RotateTo(0);
            }
            if (InBeat(1162 + 8f))
            {
                disposes = new List<Bone>();
                for (int i = 0; i < 2; i++)
                {
                    CreateBone(new CentreCircleBone(i * 90, -2, 85, BeatTime(120 - 4)) { ColorType = i + 1 });
                    CreateBone(new SideCircleBone(i * 180 + 90, 2, 70, BeatTime(120 - 4)));
                    CreateBone(new SideCircleBone(i * 180, 2, 70, BeatTime(120 - 4)));
                }
                for (int i = -10; i < 26; i++)
                {
                    float length = 30 - MathF.Abs(8 - i);
                    Bone left = new CustomBone(new Vector2(i * 10 + 245f, 375), Motions.PositionRoute.cameFromDown, Motions.LengthRoute.sin3, Motions.RotationRoute.stableValue)
                    {
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { length, BeatTime(16), i / 16f * BeatTime(16), 40 }
                    };
                    Bone right = new CustomBone(new Vector2(i * 10 + 245f, 215), Motions.PositionRoute.cameFromUp, Motions.LengthRoute.sin3, Motions.RotationRoute.stableValue)
                    {
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { length, BeatTime(16), i / 16f * BeatTime(16), 40 }
                    };
                    disposes.Add(left);
                    disposes.Add(right);
                    CreateBone(left);
                    CreateBone(right);
                }
            }
            if (InBeat(1162 + 4, 1290 - 4))
            {
                float e = Cos((Gametime - BeatTime(10)) / BeatTime(64) * 360) * 80f;

                FightBox.instance = FightBox.boxs[0];
                SetBox(240 - e, 400 - e, 210, 370);
            }
            if (InBeat(1290 - 4))
            {
                SetBox(-320, -240, 210, 370);
            }
            if (InBeat(1290))
            {
                SetSoul(2);
                Heart.UmbrellaAvailable = true;
                SetBox(140, 480, 200, 380);
                disposes?.ForEach(s => s.Dispose());
            }
            if (InBeat(1290 + 4, 1290 + 128 - 16 - 4) && AtKthBeat(16, 0))
            {
                int timetot = 28;
                for (int i = 0; i < 6; i += 2)
                {
                    CreateBone(new CustomBone(new Vector2(510 + 100 + 10.5f * i, 200), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.stableValue)
                    {
                        PositionRouteParam = new float[] { -2.4f + i * 0.015f, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 86 + i * 9, BeatTime(timetot - i * 0.2f) }
                    });
                    CreateBone(new CustomBone(new Vector2(510 + 100 + 10.5f * i, 380), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.stableValue)
                    {
                        PositionRouteParam = new float[] { -2.4f + i * 0.015f, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 206 - i * 9, BeatTime(timetot - i * 0.2f) }
                    });
                }
                for (int i = 0; i < 6; i++)
                {
                    CreateBone(new CustomBone(new Vector2(510 + 2f + 10.3f * i, 200), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.stableValue)
                    {
                        PositionRouteParam = new float[] { -2.4f + i * 0.015f, 0 },
                        RotationRouteParam = new float[] { 0 },
                        LengthRouteParam = new float[] { 150, BeatTime(11) }
                    });
                }
                CreateBone(new CustomBone(new Vector2(700, 380), Motions.PositionRoute.linear, Motions.LengthRoute.autoFold, Motions.RotationRoute.stableValue)
                {
                    PositionRouteParam = new float[] { -2.3f, 0 },
                    RotationRouteParam = new float[] { 0 },
                    LengthRouteParam = new float[] { 250, BeatTime(timetot + 2) },
                });
                CreateBone(new CustomBone(new Vector2(510 + 30, 280), Motions.PositionRoute.linear, Motions.LengthRoute.sin, Motions.RotationRoute.stableValue)
                {
                    PositionRouteParam = new float[] { -2.4f, 0 },
                    RotationRouteParam = new float[] { 0 },
                    LengthRouteParam = new float[] { 30, BeatTime(8), Rand(0, (int)BeatTime(8)), 40 }
                });
                CreatePlatform(new Platform(1, new Vector2(510 + 30, 280), Motions.PositionRoute.linear, 0, 60)
                {
                    PositionRouteParam = new float[] { -2.4f, 0 }
                });
                CreatePlatform(new Platform(0, new Vector2(510 + 90, 340), Motions.PositionRoute.linear, 0, 40)
                {
                    PositionRouteParam = new float[] { -3.6f, 0 }
                });
            }
            if (InBeat(1290 + 4, 1290 + 128 - 4) && Gametime % 5 == 0)
            {
                CreateBone(new DownBone(true, 3, 10));
                CreateBone(new UpBone(false, 3, 10));
            }
            if (InBeat(1418 - 2))
            {
                InstantSetBox(-1000, 240, 160);
                BlackScreen((int)BeatTime(1));
                ResetBarrage();
                SetBox(290, 320, 160);
                SetSoul(4);
                Heart.PurpleLineCount = 8;
            }
            if (InBeat(1418))
            {
                PlaySound(Sounds.heal);
                HeartAttribute.MaxHP = 92;
                for (int i = 0; i < 8; i++)
                {
                    float detla = Rand(-25, 25);
                    float startDelta = Rand(-100, 100);
                    float x = Rand(24, 30);

                    float detla1 = BeatTime(i * 4);
                    float detla2 = BeatTime(i * 4 - 8);

                    for (int j = -3; j <= 3; j++)
                    {
                        CreateBone(new CustomBone(new Vector2(320, detla + 290 + j * 70), Motions.PositionRoute.XYAxisSin, 0, 24)
                        {
                            PositionRouteParam = new float[] { 50, BeatTime(x), startDelta, 160, BeatTime(32), detla2 },
                            AlphaRoute = (s) =>
                            {
                                float val = (Sin(360f * (s.AppearTime + detla1) / BeatTime(32)) * 0.7f + 1.2f) * Math.Min(1, s.AppearTime / 30f);
                                s.Depth = val < 0.9f ? 0.02f : 0.5f;
                                return val;
                            },
                            LengthRoute = (s) =>
                            {
                                float val = Math.Min(24, Sin(360f * (s.AppearTime + detla1) / BeatTime(32)) * 7.7f + 24f);
                                float timeDelta = BeatTime(124) - s.AppearTime;
                                if (timeDelta < 0) return -10;
                                float val2 = timeDelta * timeDelta / 10f;
                                return MathHelper.Min(val, val2);
                            }
                        });
                    }
                }
            }
            if (InBeat(1546))
            {
                SetSoul(3);
                SetBox(290, 140, 140);
            }
            if (InBeat(1546.1f))
            {
                for (int i = 0; i < 4; i++)
                    CreateBone(new SideCircleBone(90 * i, 3, 24, BeatTime(126)) { ColorType = 1 });
                CreateBone(new CentreCircleBone(0, -4, 100, BeatTime(126)) { ColorType = 2 });
                CreateBone(new CentreCircleBone(90, -4, 100, BeatTime(126)) { ColorType = 2 });
            }
            if (InBeat(1546 + 4, 1674 - 4))
            {
                if (AtKthBeat(16, 0))
                {
                    CreateGB(new NormalGB(new Vector2(260, 100), Heart.Centre, new Vector2(1.0f, 0.5f), 90, BeatTime(8), BeatTime(1)));
                    for (int i = 0; i < 5; i++)
                    {
                        CreateBone(new CustomBone(new Vector2(250, 220), Motions.PositionRoute.XAccAxisSin, 0, 40 + i * 24)
                        {
                            PositionRouteParam = new float[] { 0, i * 0.04f + 0.04f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(250, 360), Motions.PositionRoute.XAccAxisSin, 0, 140 - i * 24)
                        {
                            PositionRouteParam = new float[] { 0, i * 0.04f + 0.04f, 0, 100, 0 }
                        });
                    }
                }
                if (At0thBeat(4))
                {
                    ScreenDrawing.CameraEffect.SizeExpand(5, BeatTime(2));
                }
                if (AtKthBeat(16, BeatTime(8)))
                {
                    CreateGB(new NormalGB(new Vector2(380, 380), Heart.Centre, new Vector2(1.0f, 0.5f), 270, BeatTime(8), BeatTime(1)));
                    for (int i = 0; i < 5; i++)
                    {
                        CreateBone(new CustomBone(new Vector2(390, 220), Motions.PositionRoute.XAccAxisSin, 0, 40 + i * 24)
                        {
                            PositionRouteParam = new float[] { 0, -i * 0.04f - 0.02f, 0, 100, 0 }
                        });
                        CreateBone(new CustomBone(new Vector2(390, 360), Motions.PositionRoute.XAccAxisSin, 0, 140 - i * 24)
                        {
                            PositionRouteParam = new float[] { 0, -i * 0.04f - 0.02f, 0, 100, 0 }
                        });
                    }
                }
                if (AtKthBeat(16, BeatTime(4)))
                {
                    CreateGB(new NormalGB(new Vector2(260 + 70, 100), Heart.Centre, new Vector2(1.0f, 0.5f), 90, BeatTime(8), BeatTime(1)));
                }
                if (AtKthBeat(16, BeatTime(12)))
                {
                    CreateGB(new NormalGB(new Vector2(390 - 70, 380), Heart.Centre, new Vector2(1.0f, 0.5f), 270, BeatTime(8), BeatTime(1)));
                }
            }

            if (InBeat(1674 - 4))
            {
                for (int i = 0; i < 6; i++)
                {
                    CreateBone(new CustomBone(new Vector2(-200, 290), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        PositionRouteParam = new float[] { 15, 0 },
                        LengthRouteParam = new float[] { 28 },
                        RotationRouteParam = new float[] { 3, i * 60 }
                    });
                    CreateBone(new CustomBone(new Vector2(840, 290), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        PositionRouteParam = new float[] { -15, 0 },
                        LengthRouteParam = new float[] { 28 },
                        RotationRouteParam = new float[] { -3, i * 60 }
                    });
                }
                CreateGB(new NormalGB(new Vector2(120, 220), Heart.Centre, new Vector2(1.3f, 1.3f), 0, BeatTime(3), 24));
                CreateGB(new NormalGB(new Vector2(520, 360), Heart.Centre, new Vector2(1.3f, 1.3f), 180, BeatTime(3), 24));
            }
            if (InBeat(1674))
            {
                Platform[] plat = GetAll<Platform>();
                for (int i = 0; i < plat.Length; i++)
                    plat[i].Dispose();
                ScreenDrawing.UISettings.CreateUISurface();
                SetSoul(2);
                Heart.JumpTimeLimit = 1000000;
                ScreenDrawing.BackGroundColor = Color.White;
                //EffectName = "TranColorDrawing";
                InstantSetBox(320, 1000, 1000);

                PlaySound(Sounds.die1);
                PlaySound(Sounds.die1);
                PlaySound(Sounds.die2);
                PlaySound(Sounds.die2);
                CreateEntity(new BoxPiece(new Vector2(250, 290)));
                CreateEntity(new BoxPiece(new Vector2(390, 290)));
            }
            if (InBeat(1674.2f, 1680.2f))
            {
                //ScreenDrawing.BackGroundColor *= 0.8f;
                ScreenDrawing.UIColor *= 0.995f;
                ScreenDrawing.HPBar.HPExistColor = Color.Lerp(new(0, 255, 0, 255), Color.Transparent, (GametimeF - BeatTime(1674.2f)) * 0.015f);
                ScreenDrawing.HPBar.HPLoseColor = Color.Lerp(new(255, 0, 0, 255), Color.Transparent, (GametimeF - BeatTime(1674.2f)) * 0.015f);
                NameShower.nameAlpha *= 0.995f;
            }
            if (InBeat(1674.2f, 1674 + 256) && !new CollideRect(-50, -50, 740, 580).Contain(Heart.Centre))
            {
                LoseHP(Heart);
            }

            if (InBeat(1674.2f, 1674 + 64) && AtKthBeat(8, 0))
            {
                CreateEntity(new FireWorkerT(new Vector2(160, 160), 16, 2, BeatTime(8), Color.White));
                CreateEntity(new FireWorkerT(new Vector2(160, 160), 12, 4, BeatTime(8), Color.Blue) { ColorType = 1 });
            }
            if (InBeat(1674.2f, 1674 + 64) && AtKthBeat(8, BeatTime(4)))
            {
                CreateEntity(new FireWorkerT(new Vector2(480, 160), 16, 2, BeatTime(8), Color.White));
                CreateEntity(new FireWorkerT(new Vector2(480, 160), 12, 4, BeatTime(8), Color.Blue) { ColorType = 1 });
            }
            if (InBeat(1674.2f + 64, 1674 + 128) && AtKthBeat(1, 0))
            {
                float e = Cos((Gametime - BeatTime(10)) / BeatTime(32) * 360) * 240f;
                Vector2 plc1 = new Vector2(e, 0) + GetVector2(100, Gametime * 5) + new Vector2(320, 100);
                CreateEntity(new FireWorkerT(plc1, 10, 3, BeatTime(8), Color.Yellow));
            }
            if (InBeat(1674.2f + 128, 1674 + 256) && AtKthBeat(4, 0))
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 plc1 = GetVector2(230, Gametime * 5 + i * 120) + new Vector2(320, 240);
                    CreateEntity(new FireWorkerT(plc1, 10, 3, BeatTime(8), Color.Yellow));
                }
            }
        }

        public void Noob()
        {
            if (InBeat(0.1f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 0, 1, 0, 1, 0, 1, 1 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(128f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 1, 1, 1 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(266))
            {
                SetSoul(0);
                SetBox(290, 160, 160);
            }
            if (InBeat(266 + 4, 394 - 16) && At0thBeat(6))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new CustomBone(new Vector2(320, 180), Motions.PositionRoute.linear, 0, 20)
                {
                    PositionRouteParam = new float[] { 0, 2.1f }
                });
            }
            if (InBeat(266 + 4, 394 - 20) && At0thBeat(16))
            {
                if (Rand(0, 1) == 0)
                {
                    CreateGB(new NormalGB(new Vector2(365, 430), Heart.Centre, new Vector2(1.2f, 1.2f), 270, BeatTime(16), BeatTime(2)));
                }
                else
                {
                    CreateGB(new NormalGB(new Vector2(275, 430), Heart.Centre, new Vector2(1.2f, 1.2f), 270, BeatTime(16), BeatTime(2)));
                }
            }
            if (InBeat(394))
            {
                CreateEntity(new Boneslab(0, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(90, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(180, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(270, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateBone(new CentreCircleBone(0, 3f, 150, BeatTime(126)));
            }
            if (InBeat(522))
            {
                CreateEntity(new Boneslab(0, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(90, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(180, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(270, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
            }
            if (InBeat(522 + 4, 650 - 4) && AtKthBeat(16, BeatTime(2)))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new DownBone(false, 3.0f, 30));
                CreateBone(new UpBone(false, 3.0f, 70));
            }
            if (InBeat(522 + 4, 650 - 4) && AtKthBeat(16, BeatTime(10)))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(false, 3.0f, 30));
                CreateBone(new DownBone(false, 3.0f, 70));
            }
            if (InBeat(650))
            {
                CreateBone(new SwarmBone(72, BeatTime(32), BeatTime(0), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(32), BeatTime(8), BeatTime(124)) { ColorType = 2 });
                CreateBone(new SwarmBone(72, BeatTime(32), BeatTime(16), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(32), BeatTime(24), BeatTime(124)) { ColorType = 2 });
            }
            if (InBeat(772))
            {
                SetSoul(0);
            }
            if (InBeat(772 + 4, 906 - 14) && AtKthBeat(16, BeatTime(10f)))
            {
                CreateEntity(new FireWorker(new Vector2(170, 130), 8, 4, BeatTime(8), Color.Yellow));
                CreateEntity(new FireWorker(new Vector2(470, 130), 8, 4, BeatTime(8), Color.Yellow));
            }
            if (InBeat(772 + 4, 906 - 14) && AtKthBeat(16, BeatTime(2f)))
            {
                CreateEntity(new FireWorker(new Vector2(80, 350), 8, 4, BeatTime(8), Color.Orange));
                CreateEntity(new FireWorker(new Vector2(560, 350), 8, 4, BeatTime(8), Color.Orange));
            }
            if (InBeat(896f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 0, 0, 0 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(900f))
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (InBeat(1024f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 1, 1, 1 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(1162))
            {
                SetBox(290, 150, 150);
                SetSoul(0);
            }
            if (InBeat(1162 + 4, 1290 - 12) && At0thBeat(2))
            {
                CreateGB(new NormalGB(GetVector2(150, Gametime * 2) + FightBox.instance.Centre, FightBox.instance.Centre, new Vector2(1.0f, 0.5f), Gametime * 2 + 180, 40, 20));
            }
            if (InBeat(1290))
            {
                SetBox(290, 300, 160);
            }
            if (InBeat(1290 + 4, 1418 - 12) && At0thBeat(4))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new CustomBone(new Vector2(170, 290), Motions.PositionRoute.XAxisSin, 0, 20)
                {
                    PositionRouteParam = new float[] { 4, 65, BeatTime(32), Rand(0, (int)BeatTime(32)) }
                });
            }
            if (InBeat(1418))
            {
                SetBox(290, 240, 160);
            }
            if (InBeat(1418 + 4, 1546 - 4) && At0thBeat(8))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(false, 2.5f, 77));
                CreateBone(new DownBone(true, 2.5f, 77));
            }
            if (InBeat(1546 + 4, 1674 - 14) && At0thBeat(8))
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), 58, 16));
            }
            if (InBeat(1669))
            {
                SetSoul(1);
                SetGreenBox();
                TP();
            }
            if (InBeat(1664f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 0, 0, 0 };

                Fortimes(16, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
        }

        public void Easy()
        {
            if (InBeat(0.1f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 0, 1, 0, 1, 0, 1, 1 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(128f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 1, 1, 1 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(266))
            {
                SetSoul(3);
                SetBox(290, 160, 160);
            }
            if (InBeat(266 + 4, 394 - 16) && At0thBeat(6))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new CustomBone(new Vector2(320, 180), Motions.PositionRoute.linear, 0, 20)
                {
                    PositionRouteParam = new float[] { 0, 2.1f }
                });
            }
            if (InBeat(266 + 4, 394 - 20) && At0thBeat(16))
            {
                if (Rand(0, 1) == 0)
                {
                    CreateGB(new NormalGB(new Vector2(365, 430), Heart.Centre, new Vector2(1.2f, 1.2f), 270, BeatTime(16), BeatTime(2)));
                }
                else
                {
                    CreateGB(new NormalGB(new Vector2(275, 430), Heart.Centre, new Vector2(1.2f, 1.2f), 270, BeatTime(16), BeatTime(2)));
                }
            }
            if (InBeat(394))
            {
                CreateEntity(new Boneslab(0, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(90, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(180, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(270, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateBone(new CentreCircleBone(0, 3f, 150, BeatTime(126)));
            }
            if (InBeat(522))
            {
                CreateEntity(new Boneslab(0, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(90, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(180, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(270, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
            }
            if (InBeat(522 + 4, 650 - 4) && AtKthBeat(16, BeatTime(2)))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new DownBone(false, 3.0f, 30));
                CreateBone(new UpBone(false, 3.0f, 70));
            }
            if (InBeat(522 + 4, 650 - 4) && AtKthBeat(16, BeatTime(10)))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(false, 3.0f, 30));
                CreateBone(new DownBone(false, 3.0f, 70));
            }
            if (InBeat(650))
            {
                CreateBone(new SwarmBone(72, BeatTime(32), BeatTime(0), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(32), BeatTime(8), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(32), BeatTime(16), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(32), BeatTime(24), BeatTime(124)) { ColorType = 1 });
            }
            if (InBeat(772))
            {
                SetSoul(0);
            }
            if (InBeat(772 + 4, 906 - 14) && AtKthBeat(16, BeatTime(10.5f)))
            {
                CreateEntity(new FireWorker(new Vector2(170, 130), 12, 4, BeatTime(8), Color.Yellow));
                CreateEntity(new FireWorker(new Vector2(470, 130), 12, 4, BeatTime(8), Color.Yellow));
            }
            if (InBeat(772 + 4, 906 - 14) && AtKthBeat(16, BeatTime(2.5f)))
            {
                CreateEntity(new FireWorker(new Vector2(80, 350), 12, 4, BeatTime(8), Color.Orange));
                CreateEntity(new FireWorker(new Vector2(560, 350), 12, 4, BeatTime(8), Color.Orange));
            }
            if (InBeat(896f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 0, 0, 0 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(900f))
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (InBeat(1024f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 1, 1, 1 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(1162))
            {
                Heart.PurpleLineCount = 5;
                SetBox(290, 150, 150);
                SetSoul(4);
            }
            if (InBeat(1162 + 4, 1290 - 12) && At0thBeat(2))
            {
                CreateGB(new NormalGB(GetVector2(150, Gametime * 2) + FightBox.instance.Centre, FightBox.instance.Centre, new Vector2(1.0f, 0.5f), Gametime * 2 + 180, 40, 20));
            }
            if (InBeat(1290))
            {
                SetBox(290, 300, 160);
                Heart.PurpleLineCount = 5;
            }
            if (InBeat(1290 + 4, 1418 - 12) && At0thBeat(4))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new CustomBone(new Vector2(170, 290), Motions.PositionRoute.XAxisSin, 0, 20)
                {
                    PositionRouteParam = new float[] { 4, 65, BeatTime(32), Rand(0, (int)BeatTime(32)) }
                });
            }
            if (InBeat(1418))
            {
                SetBox(290, 240, 160);
                Heart.PurpleLineCount = 3;
            }
            if (InBeat(1418 + 4, 1546 - 4) && At0thBeat(8))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(false, 2.5f, 80));
                CreateBone(new DownBone(true, 2.5f, 80));
            }
            if (InBeat(1546 + 4, 1674 - 14) && At0thBeat(8))
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 1.0f), 58, 16));
            }
            if (InBeat(1669))
            {
                SetSoul(1);
                SetGreenBox();
                TP();
            }
            if (InBeat(1664f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 0, 0, 0 };

                Fortimes(16, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
        }

        public void Hard()
        {
            if (InBeat(0.1f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 0, 1, 0, 1, 0, 2, 2 };

                Fortimes(8, (y) =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1 || (rhythms[x] == 2 && y % 2 == 1))
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        else if (rhythms[x] == 2 && y % 2 == 0)
                        {
                            CreateArrow(time + BeatTime(1), Rand(0, 3), 5.0f, 0, 0);
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        if (x % 2 == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(128f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 2, 1, 2 };

                Fortimes(4, (y) =>
                {
                    Fortimes(16, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        else if (rhythms[x] == 2)
                        {
                            CreateArrow(time + BeatTime(1), Rand(0, 3), 5.0f, 0, 0);
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        if (x % 4 == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(266))
            {
                SetSoul(3);
                SetBox(290, 140, 140);
            }
            if (InBeat(266 + 4, 394 - 16) && At0thBeat(4))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new CustomBone(new Vector2(320, 180), Motions.PositionRoute.linear, 0, 16)
                {
                    PositionRouteParam = new float[] { 0, 2f }
                });
            }
            if (InBeat(266 + 4, 394 - 20) && AtKthBeat(8, 3))
            {
                if (Rand(0, 1) == 0)
                {
                    CreateGB(new NormalGB(new Vector2(350, 430), Heart.Centre, Vector2.One, 270, BeatTime(16), BeatTime(2)));
                }
                else
                {
                    CreateGB(new NormalGB(new Vector2(290, 430), Heart.Centre, Vector2.One, 270, BeatTime(16), BeatTime(2)));
                }
            }
            if (InBeat(394))
            {
                SetBox(290, 160, 160);
                CreateEntity(new Boneslab(0, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(90, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(180, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateEntity(new Boneslab(270, 20, (int)BeatTime(4), (int)BeatTime(125)));
                CreateBone(new CentreCircleBone(0, 360f / BeatTime(8), 150, BeatTime(126)));
            }
            if (InBeat(522))
            {
                CreateEntity(new Boneslab(0, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(90, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(180, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
                CreateEntity(new Boneslab(270, 10, (int)BeatTime(4), (int)BeatTime(122 + 128)));
            }
            if (InBeat(522 + 4, 650 - 4) && AtKthBeat(16, BeatTime(2)))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new DownBone(false, 3.0f, 30));
                CreateBone(new UpBone(false, 3.0f, 86));
            }
            if (InBeat(522 + 4, 650 - 4) && AtKthBeat(16, BeatTime(10)))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(false, 3.0f, 30));
                CreateBone(new DownBone(false, 3.0f, 86));
            }
            if (InBeat(650))
            {
                CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(0), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(8), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(4), BeatTime(124)) { ColorType = 1 });
                CreateBone(new SwarmBone(72, BeatTime(16), BeatTime(12), BeatTime(124)) { ColorType = 1 });
            }
            if (InBeat(772))
            {
                SetSoul(0);
            }
            if (InBeat(772 + 4, 906 - 14) && AtKthBeat(16, BeatTime(10.5f)))
            {
                CreateEntity(new FireWorker(new Vector2(170, 130), 20, 4, BeatTime(8), Color.Yellow));
                CreateEntity(new FireWorker(new Vector2(470, 130), 20, 4, BeatTime(8), Color.Yellow));
                CreateEntity(new FireWorker(new Vector2(170, 130), 12, 4, BeatTime(12), Color.Blue) { ColorType = 1 });
                CreateEntity(new FireWorker(new Vector2(470, 130), 12, 4, BeatTime(12), Color.Blue) { ColorType = 1 });
            }
            if (InBeat(772 + 4, 906 - 14) && AtKthBeat(16, BeatTime(2.5f)))
            {
                CreateEntity(new FireWorker(new Vector2(80, 350), 20, 4, BeatTime(8), Color.Red));
                CreateEntity(new FireWorker(new Vector2(80, 350), 12, 4, BeatTime(12), Color.Blue) { ColorType = 1 });
                CreateEntity(new FireWorker(new Vector2(560, 350), 12, 4, BeatTime(12), Color.Blue) { ColorType = 1 });
                CreateEntity(new FireWorker(new Vector2(560, 350), 20, 4, BeatTime(8), Color.Red));
            }
            if (InBeat(896f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 0, 0, 0 };

                Fortimes(8, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        if (x % 2 == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(900f))
            {
                TP();
                SetGreenBox();
                SetSoul(1);
            }
            if (InBeat(1024f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 2, 1, 2 };

                Fortimes(4, (y) =>
                {
                    Fortimes(16, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                        }
                        else if (rhythms[x] == 2)
                        {
                            CreateArrow(time + BeatTime(1), Rand(0, 3), 6.0f, 0, 0);
                            CreateArrow(time, Rand(0, 3), 6.0f, 0, 0);
                        }
                        if (x % 4 == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 6.0f, 1, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
            if (InBeat(1162))
            {
                PlaySound(Sounds.heal);
                Regenerate(2);
                Heart.PurpleLineCount = 7;
                SetBox(290, 150, 150);
                SetSoul(4);
            }
            if (InBeat(1162 + 4, 1290 - 12) && At0thBeat(2))
            {
                CreateGB(new NormalGB(GetVector2(150, Gametime * 2.8f) + FightBox.instance.Centre, FightBox.instance.Centre, new Vector2(1.0f, 1.0f), Gametime * 2.8f + 180, 40, 20));
            }
            if (InBeat(1290))
            {
                SetBox(290, 300, 160);
                Heart.PurpleLineCount = 5;
            }
            if (InBeat(1290 + 4, 1418 - 12) && At0thBeat(4))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new CustomBone(new Vector2(170, 290), Motions.PositionRoute.XAxisSin, 0, 26)
                {
                    PositionRouteParam = new float[] { 3, 65, BeatTime(32), Rand(0, (int)BeatTime(32)) }
                });
                CreateBone(new CustomBone(new Vector2(470, 290), Motions.PositionRoute.XAxisSin, 0, 26)
                {
                    PositionRouteParam = new float[] { -3, 65, BeatTime(32), Rand(0, (int)BeatTime(32)) }
                });
            }
            if (InBeat(1418))
            {
                SetBox(290, 240, 160);
                Heart.PurpleLineCount = 3;
            }
            if (InBeat(1418 + 4, 1546 - 4) && At0thBeat(4))
            {
                PlaySound(Sounds.pierce);
                CreateBone(new UpBone(false, 3f, 80));
                CreateBone(new DownBone(true, 3f, 80));
            }
            if (InBeat(1546 + 4, 1674 - 14) && At0thBeat(8))
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
                CreateGB(new NormalGB(Heart.Centre + GetVector2(160, Rand(0, 359)), Heart.Centre, new Vector2(1.0f, 0.5f), 58, 16));
            }
            if (InBeat(1669))
            {
                SetSoul(1);
                SetGreenBox();
                TP();
            }
            if (InBeat(1664f))
            {
                float time = BeatTime(10f);

                int[] rhythms = { 1, 1, 1, 1, 1, 0, 0, 0 };

                Fortimes(16, () =>
                {
                    Fortimes(8, (x) =>
                    {
                        if (rhythms[x] == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 0, 0);
                        }
                        if (x % 2 == 1)
                        {
                            CreateArrow(time, Rand(0, 3), 5.0f, 1, 0);
                        }
                        time += BeatTime(2);
                    });
                });
            }
        }

        public void Start()
        {
            //ScreenDrawing.UISettings.CreateUISurface();
            GametimeDelta = BeatTime(-2.5f) + 2.5f;
            //GametimeDelta = BeatTime(778f);
            //PlayOffset = GametimeDelta;
            SetGreenBox();
            TP();
            Heart.Speed = 3.0f;
            HeartAttribute.MaxHP = 7;
            SetSoul(1);
            //  SetSoul(3);
            //  Heart.UmbrellaAvailable = true;
            //  SetBox(290, 140, 140);
            if (GameStates.difficulty == 4)
            {
                SetSoul(0);
                HeartAttribute.MaxHP = 92;
                HeartAttribute.KR = true;
                Heart.EnabledRedShield = true;
            }
        }

        public void ExtremePlus()
        {
            throw new NotImplementedException();
        }
    }
}