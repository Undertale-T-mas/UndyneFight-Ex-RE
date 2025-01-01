using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.UserService;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.FightResources.Sprites;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public class EternalSpringDream : WaveConstructor, IWaveSet
    {
        public static EternalSpringDream instance;
        private class ExplodeTrigger2 : Entity, ICollideAble
        {
            private class Explode : Entity, ICollideAble
            {
                private int score = 3;
                private bool hasHit = false;
                private float alpha = 1;
                private int appearTime = 0;
                public Explode(Vector2 centre)
                {
                    Depth = 0.5f;
                    Centre = centre;
                    appearTime = 0;
                    Image = explodes[0];
                    alpha = 1;
                }

                private static readonly int[] textureSort = { 1, 2, 3, 4, 3, 2, 2, 2, 2, 2 };
                private static readonly float[] alphaSort = { 1f, 1f, 1f, 1f, 0.9f, 0.75f, 0.6f, 0.45f, 0.3f, 0.15f };

                public override void Draw()
                {
                    FormalDraw(Image, Centre, Color.White * alpha, 0, ImageCentre);
                }

                public void GetCollide(Player.Heart player)
                {
                    float range = Image.Width / 2f;
                    float dist = GetDistance(player.Centre, Centre);
                    float res = dist - range - 5;

                    if (res < 0) { if (!hasHit) PushScore(0); LoseHP(player); hasHit = true; }
                    else if (res <= 2)
                    {
                        if (score >= 2) { score = 1; player.CreateCollideEffect2(Color.LawnGreen, 3f); }
                    }
                    else if (res <= 5)
                    {
                        if (score >= 3) { score = 2; player.CreateCollideEffect2(Color.LightBlue, 6f); }
                    }
                    if (score != 3 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
                    {
                        if (!hasHit) PushScore(0); LoseHP(player); hasHit = true;
                    }
                }

                public override void Update()
                {
                    Image = explodes[textureSort[appearTime / 3] - 1];
                    alpha = alphaSort[appearTime / 3];
                    appearTime++;
                    if (appearTime == 30) Dispose();
                }
            }

            private class CirclingBone : Bone
            {
                private readonly ExplodeTrigger2 detect;
                private const float rotateSpeed = 2;
                public CirclingBone(ExplodeTrigger2 detect)
                {
                    alpha = 1;
                    this.detect = detect;
                    IsMasked = false;
                }
                public override void Update()
                {
                    Rotation += rotateSpeed;
                    Length = 24;
                    Centre = detect.Centre;
                    if (detect == null || detect.Disposed)
                    {
                        foreach (var v in this.Split())
                        {
                            CreateBone(v);
                        }
                    }
                    base.Update();
                }
            }

            private int score = 3;
            private bool hasHit = false;
            private static bool soundPlayed = true;

            private float waitingTime = 0;
            private Vector2 speed;

            private readonly Bone attractBone;

            public ExplodeTrigger2(Vector2 start, Vector2 speed, float waitingTime)
            {
                Depth = 0.5f;
                attractBone = new CirclingBone(this);
                CreateBone(attractBone);
                soundPlayed = false;
                this.speed = speed;
                Centre = start;
                Rotation = MathF.Atan2(speed.Y, speed.X);
                this.waitingTime = waitingTime;
                Image = explodeTrigger;
            }

            public override void Draw()
            {
                FormalDraw(Image, Centre, Color.White, 0, ImageCentre);
                FormalDraw(arrow[0, 3, 0], Centre, Color.White * 0.2f, Rotation + PI, new Vector2(10.5f, 6.5f));
            }

            public override void Update()
            {
                Centre += speed;
                waitingTime--;

                if (waitingTime <= 0)
                {
                    if (!soundPlayed) PlaySound(explode, 0.4f);
                    InstanceCreate(new Explode(Centre));
                    Dispose();
                }
            }

            public void GetCollide(Player.Heart player)
            {
                float dist = GetDistance(player.Centre, Centre);

                float res = dist - 7;

                if (res < 0) { if (!hasHit) PushScore(0); LoseHP(player); hasHit = true; }
                else if (res <= 2)
                {
                    if (score >= 2) { score = 1; player.CreateCollideEffect2(Color.LawnGreen, 3f); }
                }
                else if (res <= 5)
                {
                    if (score >= 3) { score = 2; player.CreateCollideEffect2(Color.LightBlue, 6f); }
                }
                if (score != 3 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
                {
                    if (!hasHit) PushScore(0); LoseHP(player); hasHit = true;
                }
            }
        }
        private class ExplodeTrigger : Entity, ICollideAble
        {
            private class Explode : Entity, ICollideAble
            {
                private int score = 3;
                private bool hasHit = false;
                private float alpha = 1;
                private int appearTime = 0;
                public Explode(Vector2 centre)
                {
                    Depth = 0.95f;
                    Centre = centre;
                    appearTime = 0;
                    Image = explodes[0];
                    alpha = 1;
                }

                private static readonly int[] textureSort = { 1, 2, 3, 4, 3, 2, 2, 2, 2, 2 };
                private static readonly float[] alphaSort = { 1f, 1f, 1f, 1f, 0.9f, 0.75f, 0.6f, 0.45f, 0.3f, 0.15f };

                public override void Draw()
                {
                    FormalDraw(Image, Centre, Color.White * alpha, 0, ImageCentre);
                }

                public void GetCollide(Player.Heart player)
                {
                    float range = Image.Width / 2f;
                    float dist = GetDistance(player.Centre, Centre);
                    float res = dist - range - 5;

                    if (res < 0) { if (!hasHit) PushScore(0); LoseHP(player); hasHit = true; }
                    else if (res <= 2)
                    {
                        if (score >= 2) { score = 1; player.CreateCollideEffect2(Color.LawnGreen, 3f); }
                    }
                    else if (res <= 5)
                    {
                        if (score >= 3) { score = 2; player.CreateCollideEffect2(Color.LightBlue, 6f); }
                    }
                    if (score != 3 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
                    {
                        if (!hasHit) PushScore(0); LoseHP(player); hasHit = true;
                    }
                }
                public override void Update()
                {
                    Image = explodes[textureSort[appearTime / 3] - 1];
                    alpha = alphaSort[appearTime / 3];
                    appearTime++;
                    if (appearTime == 30) Dispose();
                }
            }

            private int score = 3;
            private bool hasHit = false;
            private static bool soundPlayed = true;

            private float waitingTime = 0;
            private Vector2 speed;

            public ExplodeTrigger(Vector2 start, Vector2 speed, float waitingTime)
            {
                Depth = 0.5f;
                soundPlayed = false;
                this.speed = speed;
                Centre = start;
                Rotation = MathF.Atan2(speed.Y, speed.X);
                this.waitingTime = waitingTime;
                Image = explodeTrigger;
            }

            public override void Draw()
            {
                FormalDraw(Image, Centre, Color.White, 0, ImageCentre);
                FormalDraw(arrow[0, 3, 0], Centre, Color.White * 0.2f, Rotation + PI, new Vector2(10.5f, 6.5f));
            }
            public override void Update()
            {
                Centre += speed;
                waitingTime--;

                if (waitingTime <= 0)
                {
                    if (!soundPlayed) PlaySound(explode, 0.4f);
                    InstanceCreate(new Explode(Centre));
                    Dispose();
                }
            }

            public void GetCollide(Player.Heart player)
            {
                float dist = GetDistance(player.Centre, Centre);

                float res = dist - 7;

                if (res < 0) { if (!hasHit) PushScore(0); LoseHP(player); hasHit = true; }
                else if (res <= 2)
                {
                    if (score >= 2) { score = 1; player.CreateCollideEffect2(Color.LawnGreen, 3f); }
                }
                else if (res <= 5)
                {
                    if (score >= 3) { score = 2; player.CreateCollideEffect2(Color.LightBlue, 6f); }
                }
                if (score != 3 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
                {
                    if (!hasHit) PushScore(0); LoseHP(player); hasHit = true;
                }
            }
        }

        public EternalSpringDream() : base(5.237f)
        {
            instance = this;
        }

        class ThisInformation : SongInformation
        {
            public override string BarrageAuthor => "T-mas";
            public override string SongAuthor => "Touhou (ZUN)";
            public override string PaintAuthor => "Touhou (ZUN)";

            public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 4.0f),
                            new(Difficulty.Easy, 6.5f),
                            new(Difficulty.Normal, 9.0f),
                            new(Difficulty.Hard, 14.8f),
                            new(Difficulty.Extreme, 17.4f),
                    }
                );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 4.0f),
                            new(Difficulty.Easy, 6.5f),
                            new(Difficulty.Normal, 9.0f),
                            new(Difficulty.Hard, 14.8f),
                            new(Difficulty.Extreme, 16.7f),
                    }
                );
            public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.Noob, 7.0f),
                            new(Difficulty.Easy, 11.5f),
                            new(Difficulty.Normal, 15.0f),
                            new(Difficulty.Hard, 19.9f),
                            new(Difficulty.Extreme, 21.9f),
                    }
                );
        }
        public SongInformation Attributes => new ThisInformation();
        int MaxHP;
        public void Start()
        {
            SetBox(290, 140, 140);
            SetSoul(0);
            if (difficulty >= 3)
            {
                HeartAttribute.MaxHP = 92;
                HeartAttribute.KR = true;
                HeartAttribute.KRDamage = 7f;
                Heart.Speed = 3;
                if (difficulty == 4 && PlayerManager.CurrentUser != null)
                {
                    var songs = PlayerManager.CurrentUser.SongManager;
                    Dictionary<string, SongData> dic = new();
                    foreach (var v in songs.AllDatas)
                    {
                        dic.Add(v.SongName, v);
                    }
                    if (dic.ContainsKey("Bad apple"))
                    {
                        if (
                            dic["Bad apple"].CurrentSongStates.ContainsKey(Difficulty.Extreme)&&
                            dic["Bad apple"].CurrentSongStates[Difficulty.Extreme].Accuracy != 0)
                        {
                            if (!PlayerManager.CurrentUser.Custom.Nexts.ContainsKey("BadApple%"))
                            {
                                MaxHP = 80;
                                HeartAttribute.MaxHP = 80;
                            }
                        }
                    }

                }
            }
        }

        public void Noob()
        {
            if (InBeat(3.2f))
            {
                CreateBone(new CentreCircleBone(0, 1, 170, BeatTime(124)));
            }
            if (InBeat(0.2f, 128 - 12f) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(Rand(100, 540), Rand(100, 380)), Heart.Centre, new Vector2(1.0f, 0.5f), BeatTime(12), 10));
            }
            if (InBeat(128))
            {
                for (int i = 0; i < 4; i++)
                    CreateEntity(new Boneslab(i * 90, 10, 30, (int)BeatTime(128) - 90));
                SetSoul(3);
            }
            if (InBeat(128.2f, 256 - 12f) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(Rand(100, 540), Rand(100, 380)), Heart.Centre, new Vector2(1.0f, 0.4f), BeatTime(12), 10));
            }
            if (InBeat(245))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(230))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 4, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 8, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }
            if (InBeat(514))
            {
                SetSoul(0);
                SetBox(290, 140, 140);
            }
            if (InBeat(512.2f + 4, 768 - 4) && At0thBeat(8))
            {
                PlaySound(pierce);
                float height = Rand(40, 100);
                CreateBone(new CustomBone(new Vector2(250, 220), Motions.PositionRoute.XAccAxisSin, 0, (height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.05f, 0, 100, 0 }
                });
                CreateBone(new CustomBone(new Vector2(250, 360), Motions.PositionRoute.XAccAxisSin, 0, (140 - height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.05f, 0, 100, 0 }
                });
            }
            if (InBeat(757))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(742))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 4, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 8, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(1024f))
            {
                SetBox(290, 140, 140);
                SetSoul(2);
            }
            if (InBeat(1026.2f))
            {
                CreateBone(new SideCircleBone(270, 3, 50, BeatTime(128 - 8)));
                CreateBone(new SideCircleBone(90, 3, 140, BeatTime(128 - 8)));
            }

            if (InBeat(1152))
            {
                SetSoul(3);
                SetBox(290, 320, 160);
            }

            if (InBeat(1152.2f + 4, 1280 - 4f) && AtKthBeat(8, 0))
            {
                PlaySound(pierce);
                CreateBone(new UpBone(false, 2, 67));
            }
            if (InBeat(1152.2f + 4, 1280 - 8f) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(pierce);
                CreateBone(new DownBone(false, 4, 67));
            }
            if (InBeat(1268))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(1254))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 4, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 8, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });

                rhythm = new int[,]{
                    { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 4, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 8, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(1792))
            {
                SetBox(290, 320, 160);
                SetSoul(3);
            }
            if (InBeat(1792 + 4, 1920f - 4) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(pierce);
                CreateBone(new DownBone(false, 4, 68));
            }
            if (InBeat(1792 + 4, 1920f - 4) && AtKthBeat(8, BeatTime(0)))
            {
                PlaySound(pierce);
                CreateBone(new UpBone(true, 3, 68));
            }
            if (InBeat(1920)) SetBox(290, 190, 190);
            if (InBeat(1920.2f, 1920 + 128 - 4) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(Rand(50, 590), Rand(50, 430)), Heart.Centre, new Vector2(1.0f, 1.0f), BeatTime(16), 4));
            }
        }

        public void Easy()
        {
            if (InBeat(3.2f))
            {
                SetSoul(3);
                CreateBone(new CentreCircleBone(0, 1, 170, BeatTime(124 + 128)));
            }
            if (InBeat(0.2f, 128 - 12f) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(Rand(100, 540), Rand(100, 380)), Heart.Centre, new Vector2(1.0f, 0.5f), BeatTime(12), 10));
            }
            if (InBeat(128))
            {
                for (int i = 0; i < 4; i++)
                    CreateEntity(new Boneslab(i * 90, 10, 30, (int)BeatTime(128) - 90));
                SetSoul(3);
            }
            if (InBeat(128.2f, 256 - 12f) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(Rand(100, 540), Rand(100, 380)), Heart.Centre, new Vector2(1.0f, 0.4f), BeatTime(12), 10));
            }
            if (InBeat(245))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(230))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 10, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }
            if (InBeat(514))
            {
                SetSoul(0);
                SetBox(290, 140, 140);
            }
            if (InBeat(512.2f + 4, 768 - 4) && At0thBeat(8))
            {
                PlaySound(pierce);
                float height = Rand(40, 100);
                CreateBone(new CustomBone(new Vector2(250, 220), Motions.PositionRoute.XAccAxisSin, 0, (height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.05f, 0, 100, 0 }
                });
                CreateBone(new CustomBone(new Vector2(250, 360), Motions.PositionRoute.XAccAxisSin, 0, (140 - height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.05f, 0, 100, 0 }
                });
            }
            if (InBeat(757))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(742))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 10, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(1024f))
            {
                SetBox(290, 140, 140);
                SetSoul(2);
            }
            if (InBeat(1026.2f))
            {
                CreateBone(new SideCircleBone(270, 3, 50, BeatTime(128 - 8)));
                CreateBone(new SideCircleBone(90, 3, 140, BeatTime(128 - 8)));
            }

            if (InBeat(1152))
            {
                SetSoul(3);
                SetBox(290, 320, 160);
            }

            if (InBeat(1152.2f + 4, 1280 - 4f) && AtKthBeat(8, 0))
            {
                PlaySound(pierce);
                CreateBone(new UpBone(false, 2, 67));
            }
            if (InBeat(1152.2f + 4, 1280 - 8f) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(pierce);
                CreateBone(new DownBone(false, 4, 67));
            }
            if (InBeat(1268))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(1254))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 10, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });

                rhythm = new int[,]{
                    { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 4, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 8, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(1792))
            {
                SetBox(290, 320, 160);
                SetSoul(3);
            }
            if (InBeat(1792 + 4, 1920f - 4) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(pierce);
                CreateBone(new DownBone(false, 4, 68));
            }
            if (InBeat(1792 + 4, 1920f - 4) && AtKthBeat(8, BeatTime(0)))
            {
                PlaySound(pierce);
                CreateBone(new UpBone(true, 3, 68));
            }
            if (InBeat(1920)) SetBox(290, 190, 190);
            if (InBeat(1920.2f, 1920 + 128 - 4) && At0thBeat(6))
            {
                CreateGB(new NormalGB(new Vector2(Rand(50, 590), Rand(50, 430)), Heart.Centre, new Vector2(1.0f, 1.0f), BeatTime(16), 8));
            }
        }

        public void Normal()
        {
            if (InBeat(3.2f))
            {
                for (int i = 0; i < 4; i++)
                    CreateEntity(new Boneslab(i * 90, 10, 30, (int)BeatTime(256) - 90));
                SetSoul(3);
                CreateBone(new CentreCircleBone(0, 1, 170, BeatTime(124 + 128)));
            }
            if (InBeat(0.2f, 128 - 12f) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(Rand(100, 540), Rand(100, 380)), Heart.Centre, new Vector2(1.0f, 0.5f), BeatTime(12), 10));
            }
            if (InBeat(128))
            {
                SetSoul(3);
            }
            if (InBeat(128.2f, 256 - 12f) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(Rand(100, 540), Rand(100, 380)), Heart.Centre, new Vector2(1.0f, 0.4f), BeatTime(12), 10));
            }
            if (InBeat(245))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(230))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 10, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }
            if (InBeat(514))
            {
                SetSoul(0);
                SetBox(290, 140, 140);
            }
            if (InBeat(512.2f + 4, 768 - 4) && At0thBeat(8))
            {
                PlaySound(pierce);
                float height = Rand(40, 100);
                CreateBone(new CustomBone(new Vector2(250, 220), Motions.PositionRoute.XAccAxisSin, 0, (height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.05f, 0, 100, 0 }
                });
                CreateBone(new CustomBone(new Vector2(250, 360), Motions.PositionRoute.XAccAxisSin, 0, (140 - height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.05f, 0, 100, 0 }
                });
            }
            if (InBeat(757))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(742))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 10, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(1024f))
            {
                SetBox(290, 140, 140);
                SetSoul(2);
            }
            if (InBeat(1026.2f))
            {
                CreateBone(new SideCircleBone(270, 3, 50, BeatTime(128 - 8)));
                CreateBone(new SideCircleBone(90, 3, 140, BeatTime(128 - 8)));
            }

            if (InBeat(1152))
            {
                SetSoul(3);
                SetBox(290, 320, 160);
            }

            if (InBeat(1152.2f + 4, 1280 - 4f) && AtKthBeat(8, 0))
            {
                PlaySound(pierce);
                CreateBone(new UpBone(false, 2, 67));
            }
            if (InBeat(1152.2f + 4, 1280 - 8f) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(pierce);
                CreateBone(new DownBone(false, 4, 67));
            }
            if (InBeat(1268))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(1254))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 10, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });

                rhythm = new int[,]{
                    { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 4, 0, 0, ArrowAttribute.None);
                            if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 8, 0, 0, ArrowAttribute.SpeedUp);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(1792))
            {
                SetBox(290, 320, 160);
                SetSoul(3);
            }
            if (InBeat(1792 + 4, 1920f - 4) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(pierce);
                CreateBone(new DownBone(false, 4, 68));
            }
            if (InBeat(1792 + 4, 1920f - 4) && AtKthBeat(8, BeatTime(0)))
            {
                PlaySound(pierce);
                CreateBone(new UpBone(true, 3, 68));
            }
            if (InBeat(1920)) SetBox(290, 190, 190);
            if (InBeat(1920.2f, 1920 + 128 - 4) && At0thBeat(4))
            {
                CreateGB(new NormalGB(new Vector2(Rand(50, 590), Rand(50, 430)), Heart.Centre, new Vector2(1.0f, 1.0f), BeatTime(16), 8));
            }
        }

        public void Hard()
        {
            if (InBeat(3.2f))
            {
                for (int i = 0; i < 4; i++)
                    CreateEntity(new Boneslab(i * 90, 10, 30, (int)BeatTime(256) - 90));
                SetSoul(3);
                SetBox(290, 140, 140);
                CreateBone(new CentreCircleBone(0, 1, 170, BeatTime(124)));
            }
            if (InBeat(1, 256 - 18) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(Rand(100, 540), Rand(100, 380)), Heart.Centre, new Vector2(1.0f, 0.4f), BeatTime(12), 8));
                CreateGB(new NormalGB(new Vector2(Rand(100, 540), Rand(100, 380)), Heart.Centre, new Vector2(1.0f, 0.4f), BeatTime(12), 8));
            }
            if (InBeat(129, 256 - 18) && At0thBeat(2))
            {
                float alp = Gametime * 2;
                CreateGB(new NormalGB(GetVector2(160, alp) + FightBox.instance.Centre, Heart.Centre, new Vector2(1.0f, 0.4f), alp + 180, BeatTime(12), 8));
            }
            if (InBeat(245))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(230))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            else if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            else if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (x % 2 == 1)
                                CreateArrow(time, Rand(0, 3), 5, 1, 0, ArrowAttribute.None);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(513))
            {
                Heart.Speed = 3.1f;
                SetSoul(0);
                SetBox(290, 140, 140);
            }
            if (InBeat(517))
            {
                CreateBone(new CentreCircleBone(0, -4, 77, BeatTime(250)) { ColorType = 2 });
                CreateBone(new CentreCircleBone(90, -4, 77, BeatTime(250)) { ColorType = 2 });
                for (int i = 0; i < 4; i++)
                    CreateBone(new SideCircleBone(i * 90, 3, 55, BeatTime(250)) { ColorType = 2 });
            }
            if (InBeat(512.2f + 4, 768 - 4) && AtKthBeat(8, BeatTime(0)))
            {
                PlaySound(pierce);
                float height = Rand(40, 100);
                CreateBone(new CustomBone(new Vector2(250, 220), Motions.PositionRoute.XAccAxisSin, 0, (height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.07f, 0, 100, 0 }
                });
                CreateBone(new CustomBone(new Vector2(250, 360), Motions.PositionRoute.XAccAxisSin, 0, (140 - height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.07f, 0, 100, 0 }
                });
            }
            if (InBeat(512.2f + 4, 768 - 4) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(pierce);
                float height = Rand(40, 100);
                CreateBone(new CustomBone(new Vector2(390, 220), Motions.PositionRoute.XAccAxisSin, 0, (height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, -0.07f, 0, 100, 0 }
                });
                CreateBone(new CustomBone(new Vector2(390, 360), Motions.PositionRoute.XAccAxisSin, 0, (140 - height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, -0.07f, 0, 100, 0 }
                });
            }

            if (InBeat(757))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(742))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            else if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 10, 0, 0, ArrowAttribute.SpeedUp);
                            else if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (x % 2 == 1)
                                CreateArrow(time, Rand(0, 3), 5, 1, 0, ArrowAttribute.None);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(1024f))
            {
                PlaySound(heal);
                HeartAttribute.MaxHP = 92;
                SetBox(290, 160, 140);
                SetSoul(2);
                Heart.JumpSpeed = 6.7f;
                Heart.UmbrellaAvailable = true;
            }
            if (InBeat(1024 + 4, 1240 - 4) && AtKthBeat(32, BeatTime(0)))
            {
                CreateEntity(new Boneslab(0, 20, 20, (int)BeatTime(24)));
            }
            if (InBeat(1024 + 4, 1240 - 4) && AtKthBeat(32, BeatTime(4)))
            {
                for (int i = 0; i < 6; i++)
                {
                    CreateBone(new CustomBone(new Vector2(240 - i * 17, 220), Motions.PositionRoute.linear, 0, 2 * (i * 5 + 22))
                    {
                        PositionRouteParam = new float[] { 3, 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(240 - i * 17, 360), Motions.PositionRoute.linear, 0, 2 * (i * -5 + 82))
                    {
                        PositionRouteParam = new float[] { 3, 0 }
                    });
                }
            }
            if (InBeat(1024 + 4, 1240 - 4) && AtKthBeat(32, BeatTime(20)))
            {
                for (int i = 0; i < 4; i++)
                {
                    CreateBone(new CustomBone(new Vector2(400 + i * 28, 220), Motions.PositionRoute.linear, 0, 2 * (i * i * 5.5f + 12))
                    {
                        PositionRouteParam = new float[] { -3, 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(400 + i * 28, 360), Motions.PositionRoute.linear, 0, 2 * (i * i * -5.5f + 85))
                    {
                        PositionRouteParam = new float[] { -3, 0 }
                    });
                }
            }

            if (InBeat(1268))
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            if (InBeat(1254))
            {
                float time = BeatTime(26 - 7.7f);
                Fortimes(4, () => { CreateArrow(time, Rand(0, 3), 4, 0, 0); time += BeatTime(2); });
                int[,] rhythm = { { 3, 0, 1, 0, 3, 0, 1, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 1, 0, 3, 0, 1, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 3, 0, 1, 0, 3, 0, 1, 0, 3, 0, 1, 0, 3, 0, 1, 1 },
                { 3, 0, 1, 0, 3, 0, 1, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 3)
                            {
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 1, BeatTime(3)));
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            }
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            time += BeatTime(2);
                        });
                    });
                });

                rhythm = new int[,]{ { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 1 },
                { 2, 0, 2, 0, 2, 0, 2, 0, 3, 0, 0, 0, 0, 1, 1, 1 },};
                Fortimes(2, () =>
                {
                    Fortimes(4, (y) =>
                    {
                        Fortimes(16, (x) =>
                        {
                            if (rhythm[y, x] == 1)
                                CreateArrow(time, Rand(0, 3), 5, 0, 0, ArrowAttribute.None);
                            else if (rhythm[y, x] == 2)
                                CreateArrow(time, Rand(0, 3), 10, 0, 0, ArrowAttribute.SpeedUp);
                            else if (rhythm[y, x] == 3)
                                CreateGB(new GreenSoulGB(time, Rand(0, 3), 0, BeatTime(3)));
                            if (x % 2 == 1)
                                CreateArrow(time, Rand(0, 3), 5, 1, 0, ArrowAttribute.None);
                            time += BeatTime(2);
                        });
                    });
                });
            }

            if (InBeat(1792))
            {
                SetSoul(0);
                InstantSetBox(240, 10000, 10000);
                ScreenDrawing.BackGroundColor = Color.White;
            }
            if (InBeat(1792.3f))
            {
                ScreenDrawing.BackGroundColor = Color.Black;
            }
            if (InBeat(1674.2f, 1674 + 256) && !new CollideRect(-50, -50, 740, 580).Contain(Heart.Centre))
            {
                LoseHP(Heart);
            }
            if (InBeat(1792.3f, 1920f + 120) && AtKthBeat(3.5f, 0))
            {
                float v = Sin(360f * Gametime / BeatTime(128)) * 320 + 320;
                for (int i = 1; i <= 8; i++)
                {
                    CreateEntity(new ExplodeTrigger(new Vector2(v, 0), GetVector2(3.2f, i * 20), BeatTime(Rand(20, 50))));
                    CreateEntity(new ExplodeTrigger(new Vector2(640 - v, 0), GetVector2(3.2f, i * 20), BeatTime(Rand(20, 50))));
                }
            }
        }

        private class TMotion1 : ICustomMotion
        {
            public Func<ICustomMotion, Vector2> PositionRoute { get => Motions.PositionRoute.linear; set => throw new NotImplementedException(); }
            public Func<ICustomMotion, float> RotationRoute { get => Motions.RotationRoute.linear; set => throw new NotImplementedException(); }
            public float[] RotationRouteParam { get => rRP; set => throw new NotImplementedException(); }
            public float[] PositionRouteParam { get => pRP; set => throw new NotImplementedException(); }
            public Vector2 CentrePosition => new(0, 0);
            public float Rotation => 0;

            private readonly float[] pRP;
            private readonly float[] rRP;

            public float AppearTime => Gametime - timeDelta;

            private readonly int timeDelta;
            public TMotion1(float[] pRP, float[] rRP)
            {
                this.pRP = pRP;
                this.rRP = rRP;
                timeDelta = Gametime;
            }
        }

        private class CrossBoneRunner : Entity
        {
            private class CrosserBone : Bone
            {
                public CrosserBone(int type)
                {
                    Length = 20;
                    ColorType = type;
                    alpha = 1f;
                }
            }
            public CrossBoneRunner(Vector2 start, Vector2 speed, int boneCount)
            {
                controlLayer = Surface.Hidden;
                boneCount *= 2;
                rotateSpeed = 2 * (Rand(0, 1) * 2 - 1);
                Rotation = Rand(0, 359);
                Centre = start;
                this.speed = speed;
                bones = new Bone[boneCount];
                for (int i = 0; i < boneCount; i++)
                {
                    bones[i] = new CrosserBone(2 * (i % 2));
                }
            }

            private Vector2 speed;
            private readonly Bone[] bones;
            private float appearTime = 0;
            private float distSin = 0;

            public override void Draw()
            {
                foreach (var v in bones)
                {
                    v.Draw();
                }
            }

            private readonly float rotateSpeed = 2;

            public override void Update()
            {
                Rotation += rotateSpeed;
                Centre += speed;
                appearTime++;
                distSin = Sin(appearTime / instance.BeatTime(24) * 360) * 100;

                float del = 360 / bones.Length;
                for (int i = 0; i < bones.Length; i++)
                {
                    var v = bones[i];
                    v.Centre = Centre + GetVector2(distSin, Rotation + del * i);
                    v.Rotation = Rotation * 1.5f + del * i;
                    v.GetCollide(Heart);
                }
            }
        }

        public void Extreme()
        {
            if (InBeat(3.2f))
            {
                for (int i = 0; i < 4; i++) CreateBone(new SwarmBone(74, BeatTime(16), i * BeatTime(4), BeatTime(126)) { ColorType = 2 });
                SetSoul(2);
                SetBox(290, 160, 160);
            }
            if (InBeat(1.2f, 124))
            {
                float xc = 320 + Sin((Gametime - BeatTime(1.1f)) / BeatTime(64) * 360) * 140;
                SetBox(xc - 80, xc + 80, 210, 370);
            }
            for (int i = 0; i < 4; i++)
                if (InBeat(2, 122) && AtKthBeat(16, BeatTime(i * 2)))
                {
                    CreateGB(new NormalGB(new Vector2(180 + i * 95, 100), Heart.Centre, new Vector2(1, 0.5f), 90, BeatTime(4), 10));
                }
            for (int i = 0; i < 4; i++)
                if (InBeat(2, 122) && AtKthBeat(16, BeatTime(i * 2 + 8)))
                {
                    CreateGB(new NormalGB(new Vector2(460 - i * 95, 380), Heart.Centre, new Vector2(1, 0.5f), 270, BeatTime(4), 10));
                }
            if (InBeat(2, 118) && At0thBeat(8))
            {
                PlaySound(pierce);
                CreateBone(new CustomBone(new Vector2(250 - 140, 370), Motions.PositionRoute.linear, 135, 80)
                {
                    PositionRouteParam = new float[] { 4.5f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(110 - 140, 230), Motions.PositionRoute.linear, 135, 200)
                {
                    PositionRouteParam = new float[] { 4.5f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(390 + 140, 370), Motions.PositionRoute.linear, 45, 80)
                {
                    PositionRouteParam = new float[] { -4.5f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(530 + 140, 230), Motions.PositionRoute.linear, 45, 200)
                {
                    PositionRouteParam = new float[] { -4.5f, 0 }
                });
            }
            if (InBeat(124, 138))
            {
                ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.85f + 0.15f * 180;
            }
            if (InBeat(128.1f))
            {
                CreateBone(new CentreCircleBone(0, 3, 100, BeatTime(122)) { ColorType = 1 });
                CreateBone(new CentreCircleBone(0, -6, 200, BeatTime(122)) { ColorType = 2, IsMasked = true });
                CreateBone(new SideCircleBone(90, 3, 100, BeatTime(122)));
                CreateBone(new SideCircleBone(270, 3, 100, BeatTime(122)));
                SetBox(290, 160, 160);
                SetSoul(3);
            }
            if (InBeat(124, 240) && At0thBeat(4))
            {
                CreateGB(new NormalGB(Heart.Centre + GetVector2(Rand(120, 160), Rand(0, 359)), Heart.Centre, new Vector2(1.0F, 0.45F), BeatTime(8), BeatTime(2)));
            }
            if (InBeat(248, 362)) ScreenDrawing.ScreenAngle *= 0.88f;
            if (InBeat(258, 500) && GametimeF % 4 == 1)
            {
                float xc = 16 + Sin((GametimeF - BeatTime(2f)) / BeatTime(6) * 360) * 10;
                CreateBone(new DownBone(false, 4, xc) { MarkScore = false });
                CreateBone(new UpBone(true, 4, xc) { MarkScore = false });
            }
            if (InBeat(258, 500) && AtKthBeat(64, BeatTime(32)))
            {
                Heart.GiveForce(180, 4);
            }
            if (InBeat(258, 500) && AtKthBeat(64, 0))
            {
                Heart.GiveForce(0, 4);
            }

            if (InBeat(384, 512 - 8) && At0thBeat(8))
            {
                CreateGB(new NormalGB(new Vector2(80, Heart.Centre.Y), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(8), 10));
                CreateGB(new NormalGB(new Vector2(560, Heart.Centre.Y), Heart.Centre, new Vector2(1.0f, 0.5f), 180, BeatTime(8), 10));
            }
            if (InBeat(512))
            {
                PlaySound(heal);
                HeartAttribute.MaxHP = MaxHP;
            }
            if (InBeat(258, 500) && At0thBeat(8))
            {
                PlaySound(pierce);
                for (int i = 0; i < 2; i++)
                {
                    CreateBone(new CustomBone(new Vector2(150, 310), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        LengthRouteParam = new float[] { 24 },
                        PositionRouteParam = new float[] { 4, 0 },
                        RotationRouteParam = new float[] { 6, 0 },
                        RotationDelta = i * 90
                    });
                    CreateBone(new CustomBone(new Vector2(490, 310), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        LengthRouteParam = new float[] { 24 },
                        PositionRouteParam = new float[] { -4, 0 },
                        RotationRouteParam = new float[] { 6, 0 },
                        RotationDelta = i * 90
                    });
                }

                CreateBone(new CustomBone(new Vector2(250 - 140, 410), Motions.PositionRoute.linear, 135, 220)
                {
                    ColorType = 2,
                    PositionRouteParam = new float[] { 4.5f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(110 - 140, 210), Motions.PositionRoute.linear, 135, 220)
                {
                    ColorType = 2,
                    PositionRouteParam = new float[] { 4.5f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(390 + 140, 410), Motions.PositionRoute.linear, 45, 220)
                {
                    ColorType = 2,
                    PositionRouteParam = new float[] { -4.5f, 0 }
                });
                CreateBone(new CustomBone(new Vector2(530 + 140, 210), Motions.PositionRoute.linear, 45, 220)
                {
                    ColorType = 2,
                    PositionRouteParam = new float[] { -4.5f, 0 }
                });

            }
            if (InBeat(256))
            {
                SetSoul(2);
                PlaySound(heal);
                HeartAttribute.MaxHP = MaxHP;
                SetBox(310f, 320f, 210f);
                CreatePlatform(new Platform(1, new Vector2(320, 380), Motions.PositionRoute.YAxisSin, 0, 49, BeatTime(252))
                {
                    PositionRouteParam = new float[] { 0, 106, BeatTime(32), 0 },
                    createWithScaling = true
                });
                CreatePlatform(new Platform(1, new Vector2(320, 240), Motions.PositionRoute.YAxisSin, 180, 49, BeatTime(252))
                {
                    PositionRouteParam = new float[] { 0, 106, BeatTime(32), BeatTime(16) },
                    createWithScaling = true
                });
                CreatePlatform(new Platform(1, new Vector2(320, 380), Motions.PositionRoute.YAxisSin, 0, 49, BeatTime(252))
                {
                    PositionRouteParam = new float[] { 0, 106, BeatTime(32), BeatTime(16) },
                    createWithScaling = true
                });
                CreatePlatform(new Platform(1, new Vector2(320, 240), Motions.PositionRoute.YAxisSin, 180, 49, BeatTime(252))
                {
                    PositionRouteParam = new float[] { 0, 106, BeatTime(32), 0 },
                    createWithScaling = true
                });
                CreateBone(new CustomBone(new Vector2(160, 310), Motions.PositionRoute.YAxisSin, 0, 196, BeatTime(122))
                {
                    PositionRouteParam = new float[] { 0, 164, BeatTime(48), 0 }
                });
                CreateBone(new CustomBone(new Vector2(480, 310), Motions.PositionRoute.YAxisSin, 0, 196, BeatTime(122))
                {
                    PositionRouteParam = new float[] { 0, 164, BeatTime(48), 0 }
                });
                CreateBone(new CustomBone(new Vector2(150, 310), Motions.PositionRoute.YAxisSin, 0, 196, BeatTime(122))
                {
                    PositionRouteParam = new float[] { 0, 164, BeatTime(48), 0 }
                });
                CreateBone(new CustomBone(new Vector2(490, 310), Motions.PositionRoute.YAxisSin, 0, 196, BeatTime(122))
                {
                    PositionRouteParam = new float[] { 0, 164, BeatTime(48), 0 }
                });
            }

            if (InBeat(513))
            {
                Heart.Speed = 3.1f;
                SetSoul(0);
                SetBox(290, 140, 140);
                Heart.RotateTo(0);
            }
            if (InBeat(517))
            {
                CreateBone(new CentreCircleBone(0, -4, 77, BeatTime(245)) { ColorType = 2 });
                CreateBone(new CentreCircleBone(90, -4, 77, BeatTime(245)) { ColorType = 2 });
                for (int i = 0; i < 4; i++)
                    CreateBone(new SideCircleBone(i * 90, 3, 55, BeatTime(245)) { ColorType = 2 });
            }
            if (InBeat(512.2f + 4, 768 - 14) && AtKthBeat(16, BeatTime(0)))
            {
                CreateBone(new CustomBone(new Vector2(250, 220), Motions.PositionRoute.linear, 45, 180)
                {
                    ColorType = 0,
                    PositionRouteParam = new float[] { 0, 3 }
                });
            }
            if (InBeat(512.2f + 4, 768 - 14) && AtKthBeat(16, BeatTime(8)))
            {
                CreateBone(new CustomBone(new Vector2(390, 360), Motions.PositionRoute.linear, 45, 180)
                {
                    ColorType = 0,
                    PositionRouteParam = new float[] { 0, -3 }
                });
            }
            if (InBeat(512.2f + 4, 768 - 14) && AtKthBeat(8, BeatTime(0)))
            {
                PlaySound(pierce);
                float height = Rand(40, 100);
                CreateBone(new CustomBone(new Vector2(250, 220), Motions.PositionRoute.XAccAxisSin, 0, (height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.07f, 0, 100, 0 }
                });
                CreateBone(new CustomBone(new Vector2(250, 360), Motions.PositionRoute.XAccAxisSin, 0, (140 - height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, 0.07f, 0, 100, 0 }
                });
            }
            if (InBeat(512.2f + 4, 768 - 14) && AtKthBeat(8, BeatTime(4)))
            {
                PlaySound(pierce);
                float height = Rand(40, 100);
                CreateBone(new CustomBone(new Vector2(390, 220), Motions.PositionRoute.XAccAxisSin, 0, (height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, -0.07f, 0, 100, 0 }
                });
                CreateBone(new CustomBone(new Vector2(390, 360), Motions.PositionRoute.XAccAxisSin, 0, (140 - height - 19) * 2)
                {
                    PositionRouteParam = new float[] { 0, -0.07f, 0, 100, 0 }
                });
            }
            if (InBeat(764))
            {
                SetBox(300, 320, 200);
            }
            if (InBeat(768))
            {
                PlaySound(heal);
                HeartAttribute.MaxHP = MaxHP;
                Heart.Speed = 3.3f;
                SetSoul(2);
                CreatePlatform(new Platform(1, new Vector2(320, 345), Motions.PositionRoute.YAxisSin, 0, 48, BeatTime(252))
                {
                    PositionRouteParam = new float[] { 0, 128, BeatTime(48), 0 },
                    createWithScaling = true
                });
                CreatePlatform(new Platform(1, new Vector2(320, 270), Motions.PositionRoute.YAxisSin, 0, 48, BeatTime(252))
                {
                    PositionRouteParam = new float[] { 0, 128, BeatTime(48), BeatTime(24) },
                    createWithScaling = true
                });
                CreateBone(new SwarmBone(92, BeatTime(64), BeatTime(43), BeatTime(122)));
                for (int i = 0; i < 4; i++)
                    CreateBone(new SwarmBone(92, BeatTime(32), BeatTime(i * 8), BeatTime(122)) { ColorType = (i % 2) + 1 });
            }
            if (InBeat(768, 1020 - 24) && GametimeF % 5 == 0)
            {
                CreateBone(new DownBone(false, 4, 10) { MarkScore = false });
                CreateBone(new UpBone(true, 4, 10) { MarkScore = false });
                CreateBone(new LeftBone(false, 4, 10) { MarkScore = false });
                CreateBone(new RightBone(true, 4, 10) { MarkScore = false });
            }
            if (InBeat(896, 1024 - 42) && At0thBeat(16))
            {
                CreateEntity(new CrossBoneRunner(new Vector2(80, 300), new Vector2(2.2f, 0), 2));
            }
            if (InBeat(768 + 8, 1020 - 42) && At0thBeat(16))
            {
                BarrageStruct.BoneWindfall(new Vector2(40, 370), 2, new TMotion1(new float[] { 2.4f, 0 }, new float[] { -2.4f, 0 }), 130, 500);
                BarrageStruct.BoneWindfall(new Vector2(600, 230), 2, new TMotion1(new float[] { -2.4f, 0 }, new float[] { -2.4f, 0 }), 130, 500);
            }

            if (InBeat(1024))
            {
                PlaySound(heal);
                HeartAttribute.MaxHP = MaxHP;
                SetSoul(2);
                SetBox(290, 160, 180);
                Heart.GiveForce(180, 4);
            }
            if (InBeat(1024 + 12, 1152 - 12) && At0thBeat(8))
            {
                PlaySound(pierce);
                CreateBone(new DownBone(true, 3, 38));
            }
            if (InBeat(1024, 1152 - 12) && At0thBeat(4))
            {
                CreatePlatform(new Platform(1, new Vector2(118, 260), Motions.PositionRoute.linear, 180, 35)
                {
                    PositionRouteParam = new float[] { 3, 0 }
                });
                CreateBone(new UpBone(false, 150, 3, 90));
                CreateBone(new UpBone(false, 140, 3, 45) { MarkScore = false });
                CreateBone(new UpBone(false, 129, 3, 45) { MarkScore = false });
                CreateBone(new UpBone(false, 118, 3, 45) { MarkScore = false });
                CreateBone(new UpBone(false, 107, 3, 45) { MarkScore = false });
                CreateBone(new UpBone(false, 97, 3, 45) { MarkScore = false });
            }

            if (InBeat(1152 - 4))
            {
                Heart.RotateTo(0);
                SetSoul(0);
                SetBox(180, 300, 100, 220);
                Heart.movingKey = new InputIdentity[] { InputIdentity.MainRight, InputIdentity.None, InputIdentity.MainLeft, InputIdentity.None };
                TP(320, 240);
                Heart.Split();
                SetBox(340, 460, 260, 380);
                Heart.movingKey = new InputIdentity[] { InputIdentity.SecondRight, InputIdentity.None, InputIdentity.SecondLeft, InputIdentity.None };
                TP(320, 240);
                Heart.Split();
                SetBox(340, 460, 100, 220);
                Heart.movingKey = new InputIdentity[] { InputIdentity.None, InputIdentity.MainDown, InputIdentity.None, InputIdentity.MainUp };
                TP(320, 240);
                Heart.Split();
                SetBox(180, 300, 260, 380);
                Heart.movingKey = new InputIdentity[] { InputIdentity.None, InputIdentity.SecondDown, InputIdentity.None, InputIdentity.SecondUp };
                TP(320, 240);
            }
            if (InBeat(1152))
            {
                CreateBone(new CustomBone(new Vector2(320, 240), Motions.PositionRoute.stableValue, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    LengthRouteParam = new float[] { 300, BeatTime(120 - 4) },
                    RotationRouteParam = new float[] { 1.5f, 0 },
                    ColorType = 1,
                    Tags = new string[] { "a" }
                });
                CreateBone(new CustomBone(new Vector2(320, 240), Motions.PositionRoute.stableValue, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                {
                    LengthRouteParam = new float[] { 300, BeatTime(120 - 4) },
                    RotationRouteParam = new float[] { 1.5f, 0 },
                    ColorType = 2,
                    RotationDelta = 90,
                    Tags = new string[] { "a" }
                });
            }
            if (InBeat(1152 + 2, 1280 - 10 - 16) && AtKthBeat(32, BeatTime(8)))
            {
                PlaySound(pierce);
                for (int i = -1; i <= 1; i += 2)
                    for (int j = -1; j <= 1; j += 2)
                    {
                        float rotS = j * (i + 1) / 2 * -90 + (j + 1) / 2 * 270;
                        Vector2 loc = new(320 + i * 140, 240 + j * 140);
                        for (int t = 0; t <= 90; t += 15)
                        {
                            float rot = rotS + t;
                            CreateBone(new CustomBone(loc - GetVector2(10, rot), Motions.PositionRoute.linear, rot + 90, 20)
                            {
                                PositionRouteParam = new float[] { Cos(rot) * 1.5f, Sin(rot) * 1.5f }
                            });
                        }
                    }
            }
            if (InBeat(1280 - 6, 1280))
            {
                foreach (Bone b in GetAll<CustomBone>("a"))
                {
                    b.Alpha -= 0.01f;
                }
            }
            if (InBeat(1280 - 4))
            {
                for (int i = 1; i <= 3; i++)
                    Player.hearts[i].Merge(Player.hearts[0]);
                SetPlayerMission(0);
                SetBoxMission(0);
                SetBox(290, 140, 140);
            }
            if (InBeat(1280 - 2))
            {
                HeartAttribute.KRDamage = 5;
                HeartAttribute.Speed = 3.0f;
                HeartAttribute.UmbrellaAvailable = true;
                HeartAttribute.UmbrellaSpeed = 0.8f;
                SetBox(290, 380, 240);
                SetSoul(2);
                PlaySound(heal);
                HeartAttribute.MaxHP = MaxHP;
                Heart.movingKey = new InputIdentity[] { InputIdentity.MainRight, InputIdentity.MainDown, InputIdentity.MainLeft, InputIdentity.MainUp };
            }
            if (InBeat(1280 + 12, 1536 - 24) && GametimeF % 4 == 1)
            {
                CreateBone(new DownBone(false, 4, 10) { MarkScore = false });
                CreateBone(new UpBone(true, 4, 10) { MarkScore = false });
                CreateBone(new LeftBone(false, 4, 10) { MarkScore = false });
                CreateBone(new RightBone(true, 4, 10) { MarkScore = false });
            }

            if (InBeat(1280 + 128, 1536 - 16) && AtKthBeat(16, BeatTime(8)))
            {
                CreateGB(new NormalGB(new Vector2(570, Heart.Centre.Y), Heart.Centre, new Vector2(1.0f, 0.5f), 180, BeatTime(8), 8));
                CreateGB(new NormalGB(new Vector2(70, Heart.Centre.Y), Heart.Centre, new Vector2(1.0f, 0.5f), 0, BeatTime(8), 8));
            }
            if (InBeat(1280 + 128, 1536 - 16) && AtKthBeat(16, 0))
            {
                CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 70), Heart.Centre, new Vector2(1.0f, 0.5f), 90, BeatTime(8), 8));
                CreateGB(new NormalGB(new Vector2(Heart.Centre.X, 410), Heart.Centre, new Vector2(1.0f, 0.5f), 270, BeatTime(8), 8));
            }
            if (InBeat(1280 + 6, 1536 - 16) && AtKthBeat(16, 0))
            {
                float py = Rand(60, 100) - 200;
                for (int i = 0; i < 8; i++)
                {
                    CreateBone(new CustomBone(new Vector2(130 - 4, py), Motions.PositionRoute.XAccAxisSin, 0, 26)
                    {
                        PositionRouteParam = new float[] { 0, 0.04f, 200, BeatTime(128), 0 }
                    });
                    py += 70;
                }
                PlaySound(pierce);
            }
            if (InBeat(1280 + 6, 1536 - 16) && AtKthBeat(16, BeatTime(8)))
            {
                float py = Rand(60, 100) + 70;
                for (int i = 0; i < 8; i++)
                {
                    CreateBone(new CustomBone(new Vector2(510, py), Motions.PositionRoute.XAccAxisSin, 0, 26)
                    {
                        PositionRouteParam = new float[] { 0, -0.04f, -200, BeatTime(128), 0 }
                    });
                    py += 70;
                }
                PlaySound(pierce);
            }
            if (InBeat(1280 + 6, 1536 - 16) && At0thBeat(8))
            {
                CreatePlatform(new Platform(0, new Vector2(80, 295), Motions.PositionRoute.XAxisSin, 0, 44)
                {
                    PositionRouteParam = new float[] { 2.7f, 66, BeatTime(32), -Gametime + BeatTime(4) }
                });
                PlaySound(pierce);
                for (int i = 0; i < 2; i++)
                {
                    CreateBone(new CustomBone(new Vector2(110, 245), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        LengthRouteParam = new float[] { 24 },
                        PositionRouteParam = new float[] { 4, 0 },
                        RotationRouteParam = new float[] { 6, 0 },
                        RotationDelta = i * 90
                    });
                    CreateBone(new CustomBone(new Vector2(530, 335), Motions.PositionRoute.linear, Motions.LengthRoute.stableValue, Motions.RotationRoute.linear)
                    {
                        LengthRouteParam = new float[] { 24 },
                        PositionRouteParam = new float[] { -4, 0 },
                        RotationRouteParam = new float[] { 6, 0 },
                        RotationDelta = i * 90
                    });
                }
            }

            if (InBeat(1536))
            {
                PlaySound(heal);
                HeartAttribute.MaxHP = MaxHP;
                for (int i = 0; i < 4; i++)
                    CreateEntity(new Boneslab(i * 90, 10, 40, (int)BeatTime(120 + 128)));
                Heart.Gravity = 3.0f;
                Heart.UmbrellaAvailable = false;
                SetSoul(5);
                SetBox(290, 160, 160);
                for (int i = 0; i < 10; i++)
                    for (int j = -2; j < 7; j++)
                    {
                        CreateBone(new CustomBone(new Vector2(120 + i * 50, 170 + j * 50), Motions.PositionRoute.stableValue, Motions.LengthRoute.autoFold, Motions.RotationRoute.linear)
                        {
                            LengthRouteParam = new float[] { 8, BeatTime(128 + 120) },
                            PositionRouteParam = new float[] { },
                            RotationRouteParam = new float[] { 2, 3 },
                            AlphaIncrease = true
                        });
                    }
            }
            if (InBeat(1536, 1792))
            {
                float yc = 270 + Sin((Gametime - BeatTime(1.1f)) / BeatTime(72) * 360) * 110;
                float xc = 95;
                if (Gametime >= BeatTime(1536 + 128))
                    xc = 240 - Cos((Gametime - BeatTime(1536 + 128)) / BeatTime(100) * 360) * (240 - 95);
                SetBox(xc, xc + 160, yc - 65, yc + 65);
                if (At0thBeat(5.7f) && Gametime <= BeatTime(1784))
                {
                    CreateBone(new CustomBone(new Vector2(60, 270), Motions.PositionRoute.linear, 90, 18)
                    {
                        PositionRouteParam = new float[] { 2f, 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(580, 220), Motions.PositionRoute.linear, 90, 18)
                    {
                        PositionRouteParam = new float[] { -2f, 0 }
                    });
                    CreateBone(new CustomBone(new Vector2(580, 320), Motions.PositionRoute.linear, 90, 18)
                    {
                        PositionRouteParam = new float[] { -2f, 0 }
                    });
                }
            }
            if (InBeat(1792))
            {
                PlaySound(heal);
                HeartAttribute.MaxHP = MaxHP;
                HeartAttribute.KRDamage = 5;
                InstantSetBox(240, 1644, 1484);
                ResetBarrage();
            }
            if (InBeat(1792.3f))
            {
                ScreenDrawing.SceneRendering.InsertProduction(new ScreenDrawing.Shaders.RGBSplitting() { Disturbance = false });
            }
            if (InBeat(1793, 1793 + 255))
            {
                float v = Sin(360f * Gametime / BeatTime(128)) * 320 + 320;
                if (At0thBeat(8))
                    for (int i = 1; i < 8; i++)
                    {
                        CreateEntity(new ExplodeTrigger2(new Vector2(v, 0), GetVector2(3, i * 20), Rand(80, 200)));
                        CreateEntity(new ExplodeTrigger2(new Vector2(640 - v, 0), GetVector2(3, i * 20), Rand(80, 200)));
                    }

            }
        }

        public void ExtremePlus()
        {
            throw new NotImplementedException();
        }

        public string FightName => "Eternal Spring Dream";

        public string Music => "Eternal Spring Dream";
    }
}