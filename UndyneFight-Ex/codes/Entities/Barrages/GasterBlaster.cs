using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public class GasterBlaster : Entity
    {
        public float AppearVolume { get; set; } = 0.85f;
        public float ShootVolume { get; set; } = 0.8f;
        public static bool IsGBMute { set => spawnSoundPlayed = value; }

        public bool IsShake { get; set; } = false;

        public GasterBlaster()
        {
            Image = Sprites.GBStart[0];
        }

        protected float depth_ = 0.6f;
        protected Color drawingColor = Color.White;
        protected static CollideRect screen = new(-150, -50, 940, 580);
        public static bool spawnSoundPlayed = false, shootSoundPlayed = false;

        protected float GetDetla()
        {
            return Math.Min((missionRotation - Rotation + 360) % 360, (360 - missionRotation + Rotation) % 360);
        }

        protected float missionRotation, waitingTime, appearTime = 0, recoilSpeed = 0, laserAffectTime = 1, duration;
        protected Vector2 missionPlace, size, laserPlace, laserSize;
        protected bool rotateWay, laserIncreasing = true;

        protected int score = 3;
        protected float alpha = 0, beamAlpha = 1f, movingScale = 0.9f;
        protected bool hasHit = false;

        public override void Draw()
        {
            Depth = depth_;
            FormalDraw(Image, Centre, drawingColor * alpha, size, GetRadian(Rotation), ImageCentre);
            Depth = depth_ - 0.001f;
            if (appearTime >= waitingTime && laserSize.Y > 0)
                FormalDraw(Sprites.GBLaser, laserPlace, drawingColor * beamAlpha, laserSize * size, GetRadian(missionRotation), new Vector2(0, 35));
        }

        public override void Update()
        {
            appearTime++;
            if (screen.Contain(laserPlace))
                laserPlace = GetVector2(27 * size.Y, missionRotation) + Centre;

            if ((int)(appearTime - waitingTime) == -12)
                Image = Sprites.GBStart[1];
            else if ((int)(appearTime - waitingTime) == -9)
                Image = Sprites.GBStart[2];
            else if ((int)(appearTime - waitingTime) == -6)
                Image = Sprites.GBStart[3];
            else if ((int)(appearTime - waitingTime) == -3)
            {
                Image = Sprites.GBStart[4];
                if (IsShake)
                    GameStates.InstanceCreate(new Advanced.ScreenShaker(3, 9 * MathF.Min(size.X, size.Y), 3));
            }
            else if ((int)(appearTime - waitingTime) == -1 && !shootSoundPlayed)
            {
                shootSoundPlayed = true;
                PlaySound(Sounds.GBShoot, ShootVolume);
            }
        }

        public void MoveToMission()
        {
            if (alpha <= 1f)
                alpha += 0.06f * (1 / movingScale);

            if (appearTime < waitingTime)
                Centre = Centre * movingScale + missionPlace * (1 - movingScale);

            Rotation += GetDetla() * (0.98f - movingScale) * (rotateWay ? 1 : -1);
        }

        public void PushDown()
        {
            recoilSpeed += 0.4f;
            Centre += GetVector2(recoilSpeed, Rotation + 180);
            Image = Sprites.GBShooting[BoolToInt(appearTime % 6 <= 3)];
            if (laserIncreasing)
            {
                beamAlpha = beamAlpha * 0.8f + 0.2f;
                laserSize.Y = laserSize.Y * 0.8f + 0.21f;
                if (laserSize.Y >= 0.88f)
                    laserIncreasing = false;
            }
            else
            {
                beamAlpha = beamAlpha * 0.8f + 0.2f;
                laserSize.Y = 0.9f + Sin(laserAffectTime * 15) * 0.18f;
                laserAffectTime++;
            }
        }
        public void BeamDisappear()
        {
            recoilSpeed += 0.45f;
            Centre += GetVector2(recoilSpeed, Rotation + 180);

            if (recoilSpeed >= 5f)
                beamAlpha *= 0.9f;

            float detla = appearTime - waitingTime - duration;
            laserSize.Y -= (float)Math.Sqrt(detla) / 36f;
            if (laserSize.Y <= 0 && (!screen.Contain(Centre)))
                Dispose();
        }
        private class DelayControl : GameObject
        {
            internal enum DelayType
            {
                Pull = 0,
                Stop = 1
            }
            private float delay = 0;
            private readonly DelayType type;
            public DelayControl(float delay, DelayType delayType)
            {
                UpdateIn120 = true;
                type = delayType;
                this.delay = delay;
            }
            public override void Update()
            {
                GasterBlaster control = FatherObject as GasterBlaster;
                float del = type == DelayType.Pull
                    ? Math.Max(0.5f, MathF.Min(3, delay * 0.1f))
                    : Math.Max(0.4f, MathF.Min(1, (delay > 10 ? 10 : MathF.Sqrt(delay * 2)) * 0.3f));
                if (delay < del)
                    del = delay;
                del /= 2;
                control.waitingTime += del;
                delay -= del;
                if (delay <= 0f) Dispose();
            }
            public override void Dispose()
            {
                base.Dispose();
            }
        }
        public void Delay(float delay)
        {
            AddChild(new DelayControl(delay, DelayControl.DelayType.Pull));
        }
        public void Stop(float delay)
        {
            AddChild(new DelayControl(delay, DelayControl.DelayType.Stop));
        }
    }

    public class GreenSoulGB : GasterBlaster
    {
        private static Texture2D Stuck1 => Sprites.stuck1;

        private static Texture2D Stuck2 => Sprites.stuck2;

        private Texture2D StuckTexture => (appearTime % 6 > 2) ? Stuck1 : Stuck2;

        private readonly Player.Heart missionPlayer;
        private readonly int way;
        public int Way => way;
        private readonly int color;
        private Vector2 Position; //以玩家为中心建立平面直角坐标系该炮的位置

        private bool stucked = false;
        private float pushDetla;

        float timeDelta;

        public GreenSoulGB(float shootShieldTime, string way, int color, float duration) : this(shootShieldTime, GetWayFromTag(way), color, duration)
        { }
        public GreenSoulGB(float shootShieldTime, int way, int color, float duration)
        {
            if (Settings.SettingsManager.DataLibrary.Mirror) color ^= 1;
            if (way >= 4) way %= 4;
            timeDelta = Settings.SettingsManager.DataLibrary.ArrowDelay / 16f;
            depth_ = 0.466f;
            shootShieldTime += Gametime;
            laserSize.X = 1.0f;
            size = new Vector2(1.0f, 0.7f);
            missionPlayer = Player.heartInstance;
            waitingTime = shootShieldTime - Gametime;
            this.duration = duration;
            this.way = way;
            this.color = color;
            drawingColor = color switch
            {
                0 => Color.LightBlue,
                1 => Color.LightCoral,
                3 => new Color(255, 128, 255),
                2 => Color.Yellow,
                _ => throw new ArgumentException(),
            };
            basicRotation = Rotation = (way * 90 + 180) % 360;
            switch (way)
            {
                case 0:
                    Position = new Vector2(270, 0);
                    break;
                case 1:
                    Position = new Vector2(0, 190);
                    break;
                case 2:
                    Position = new Vector2(-270, 0);
                    break;
                case 3:
                    Position = new Vector2(0, -190);
                    break;
            }
        }
        private float basicRotation;

        public bool Follow { private get; set; } = false;
        public bool Ending { get; private set; } = false;
        public int DrawingColor => color;
        private int ShieldDirection => missionPlayer.Shields.DirectionOf(color);

        public bool Auto => (DebugState.blueShieldAuto && color == 0) || (DebugState.redShieldAuto && color == 1) || (DebugState.greenShieldAuto && color == 2) || (DebugState.purpleShieldAuto && color == 3) || (DebugState.otherAuto && color >= 2);

        private Vector2 _lastPlayerPos;
        public override void Update()
        {
            if (!missionPlayer.FixArrow)
            {
                float resultRotation = basicRotation + missionPlayer.Rotation;
                if (missionRotation != resultRotation)
                {
                    Rotation += resultRotation - missionRotation;
                    missionRotation = basicRotation + missionPlayer.Rotation;
                }
            }
            else missionRotation = basicRotation;
            int dir = ShieldDirection;
            base.Update();

            if ((int)(appearTime - waitingTime) >= 0)
            {
                if (appearTime <= waitingTime + duration + timeDelta)
                {
                    if (Auto && dir != way)
                        foreach (var p in Player.hearts)
                        {
                            p.Shields.Rotate(color, way);
                            p.Shields.ValidRotated();
                        }
                    if (appearTime - waitingTime >= timeDelta)
                    {
                        //check collision
                        CalcPush(dir);
                        PushDown();
                        if(Follow && (missionPlayer.Centre - _lastPlayerPos).LengthSquared() > 0.10f)
                        {
                            this.ArrangePos();
                        }
                        GetCollide();
                    }
                    Rotation = missionRotation * 0.12f + Rotation * 0.88f;
                    if (appearTime <= waitingTime + 10 + timeDelta)
                        Centre = Centre * movingScale + missionPlace * (1 - movingScale);
                }
            }
            else
            {
                missionPlace = Rotate(Position, missionPlayer.Rotation) + missionPlayer.Centre;
                if (waitingTime - appearTime <= 54)
                {
                    if (appearTime < waitingTime)
                    {
                        Centre = Centre * movingScale + missionPlace * (1 - movingScale);
                    }

                    Rotation = missionRotation * 0.12f + Rotation * 0.88f;
                    if (alpha < 1)
                        alpha += 0.1f;
                }
                if ((int)(waitingTime - appearTime) == 55)
                {
                    GameStates.InstanceCreate(new ParticleGather(missionPlace, 21, 55, drawingColor));
                    Centre = GetVector2(120, Rotation) + missionPlace;
                    Rotation += Rand(-40, 40);
                    missionPlayer.Shields.Consume();
                    if (!spawnSoundPlayed)
                    {
                        PlaySound(Sounds.GBSpawn, AppearVolume);
                        spawnSoundPlayed = true;
                    }
                }
            }

            if (appearTime >= waitingTime + duration + timeDelta)
            {
                if (!Ending)
                {
                    missionPlayer.Shields.ShieldShine(Way, color, score);
                    missionPlayer.Shields.GetCollideChecker(color).ArrowBlock(Way);
                }
                Ending = true;
                BeamDisappear();
            }
            _lastPlayerPos = missionPlayer.Centre;
        }

        private void ArrangePos()
        {
            float rotation = this.missionRotation;
            float dirVertical = rotation + 90;
            Vector2 unitU = GetVector2(1, rotation);
            float distance = Vector2.Dot(unitU, this.Centre - Heart.Centre);
            this.Centre = Heart.Centre + unitU * distance;
        }

        private void CalcPush(int dir)
        {
            if (dir == way)
            {
                stucked = true;
                laserPlace = GetVector2(1091 + 38 - pushDetla, missionRotation + 180) + missionPlayer.Centre;

                missionPlayer.Shields.Push(this, color);
            }
            else
            {
                stucked = false;
                laserPlace = GetVector2(1091 + 38 - 76, missionRotation + 180) + missionPlayer.Centre;
            }
            pushDetla = missionPlayer.Shields.PushDelta(color);
        }

        public override void Draw()
        {
            base.Draw();
            if (appearTime >= waitingTime && laserSize.Y > 0)
                FormalDraw(StuckTexture, laserPlace + GetVector2(1091 + 2, missionRotation),
                    drawingColor * alpha, 1.33f * laserSize.Y, GetRadian(missionRotation + 180), new Vector2(0, 35));
        }

        private void GetCollide()
        {
            bool alw = Auto;
            if (!stucked || pushDetla > 22)
            {
                if (alw || alpha < 0.5f)
                    return;

                if (appearTime - waitingTime - timeDelta < 4.5f)
                {
                    if (appearTime - waitingTime - timeDelta < 2.5f) return;
                    score = Math.Min(2, score);
                    return;
                }
                if (alpha < 0.97f)
                {
                    if (alpha > 0.6f)
                    {
                        score = Math.Min(2, score);
                        return;
                    }
                    score = 1;
                }
                LoseHP(missionPlayer);
                if (!hasHit)
                {
                    PushScore(0);
                    score = 0;
                    hasHit = true;
                }
            }
            else if (pushDetla > 14 && stucked)
            {
                score = Math.Min(2, score);
            }
            else if (pushDetla > 6 && stucked)
            {
                score = 1;
            }
        }

        public override void Dispose()
        {
            if (!hasHit)
            {
                PushScore(score);
                missionPlayer.Shields.ValidRotated();
            }

            base.Dispose();
        }
    }

    public class NormalGB : GasterBlaster, ICollideAble
    {
        /// <summary>
        /// 生成一个普通GB炮
        /// </summary>
        /// <param name="missionPlace">射击时的位置</param>
        /// <param name="spawnPlace">初始位置</param>
        /// <param name="size">大小</param>
        /// <param name="waitingTime">射击的等待时间</param>
        /// <param name="duration">射击持续时间</param>
        public NormalGB(Vector2 missionPlace, Vector2 spawnPlace, Vector2 size, float waitingTime, float duration) : this(missionPlace, spawnPlace, size,
            (float)(Math.Atan2(Heart.Centre.Y - missionPlace.Y, Heart.Centre.X - missionPlace.X) * 180 / Math.PI), waitingTime, duration)
        { }

        /// <summary>
        /// 生成一个普通GB炮
        /// </summary>
        /// <param name="missionPlace">射击时的位置</param>
        /// <param name="spawnPlace">初始位置</param>
        /// <param name="size">大小</param>
        /// <param name="waitingTime">射击的等待时间</param>
        /// <param name="duration">射击持续时间</param>
        /// <param name="rotation">射击方向</param>
        public NormalGB(Vector2 missionPlace, Vector2 spawnPlace, Vector2 size, float rotation, float waitingTime, float duration)
        {
            movingScale = waitingTime < 30 ? 0.5f + waitingTime / 90f : 0.93334f - 3f / waitingTime;

            if (!spawnSoundPlayed)
            {
                PlaySound(Sounds.GBSpawn, 0.85f);
                spawnSoundPlayed = true;
            }
            Centre = spawnPlace;
            missionRotation = rotation;
            Rotation = GetRandom(0, 359);
            this.missionPlace = missionPlace;
            this.size = size;
            laserSize.X = 1.0f;
            Depth = 0.6f;
            this.waitingTime = waitingTime;
            this.duration = duration;

            rotateWay = (missionRotation - Rotation + 360) % 360 < (360 - missionRotation + Rotation) % 360;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            #region 缓动
            MoveToMission();

            base.Update();

            if ((int)(appearTime - waitingTime) >= 0 && appearTime <= waitingTime + duration)
                PushDown();

            if (appearTime >= waitingTime + duration)
                BeamDisappear();
            #endregion
        }

        public void GetCollide(Player.Heart heart)
        {
            if (appearTime <= waitingTime + duration + 2)
            {
                if ((appearTime < waitingTime - 2) || (heart.SoulType == 1 || alpha <= 0.8f))
                    return;


                float A, B, C, dist;
                if (Rotation == 0)
                    dist = Centre.X - heart.Centre.X;
                else
                {
                    float k = (float)Math.Tan(GetRadian(Rotation));
                    A = k;
                    B = -1;
                    C = -A * Centre.X - B * Centre.Y;
                    dist = (float)((A * heart.Centre.X + B * heart.Centre.Y + C) / Math.Sqrt(A * A + B * B));
                }
                float res = Math.Abs(dist) - (32 * laserSize.Y * size.Y - 2);

                if (res < 0)
                {
                    if (!hasHit)
                    {
                        PushScore(0);
                    }
                    LoseHP(heart);
                    hasHit = true;
                }
                if (!hasHit)
                {
                    if (res <= 2)
                    {
                        if (score >= 2) { score = 1; Player.CreateCollideEffect(Color.LawnGreen, 3f); }
                    }
                    else if (res <= 5.4f)
                    {
                        if (score >= 3) { score = 2; Player.CreateCollideEffect(Color.LightBlue, 6f); }
                    }
                    if (score != 3 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
                    {
                        if (!hasHit) PushScore(0);

                        LoseHP(heart); hasHit = true;
                    }
                }
            }
        }

        public override void Dispose()
        {
            if (!hasHit) PushScore(score);

            base.Dispose();
        }
    }
}