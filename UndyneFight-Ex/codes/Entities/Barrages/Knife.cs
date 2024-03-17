using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace UndyneFight_Ex.Entities
{
    public class Knife : Entity, ICollideAble, ICustomMotion
    {
        private JudgementState JudgeState
        {
            get
            {
                return GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Lenient;
            }
        }

        float delay;
        Func<ICustomMotion, float> rotease;
        Func<ICustomMotion, Vector2> vecease;
        public Knife(float delay, Func<ICustomMotion, Vector2> vecease, Func<ICustomMotion, float> rotease)
        {
            Image = Sprites.knife;
            this.delay = delay;
            this.vecease = vecease;
            this.rotease = rotease;
            AngleMode = true;

            UpdateIn120 = true;
        }
        public Knife(float delay, Vector2 centre, float rot) : this(delay, CentreEasing.Stable(centre), ValueEasing.Stable(rot)) { }

        bool build = true;
        float appearTime = 0;
        int score = 3;
        bool hasHit;
        public bool MarkScore { get; set; } = true;

        public Func<ICustomMotion, Vector2> PositionRoute { get => vecease; set => vecease = value; }
        public Func<ICustomMotion, float> RotationRoute { get => rotease; set => rotease = value; }
        public float[] RotationRouteParam
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public float[] PositionRouteParam
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public float AppearTime => appearTime;

        public Vector2 CentrePosition => Centre;
        public Color DrawColor { get; set; } = Color.Purple;

        public override void Draw()
        {
            Depth = 0.99f;
            float alpha = 1 - (AppearTime / delay * 2f);
            if (AppearTime > delay)
                FormalDraw(Image, Centre, DrawColor * rayAlpha, new Vector2(scale * 0.5f, 2), Rotation - 90, ImageCentre);
            else if (alpha > 0)
                FormalDraw(Sprites.KnifeWarn, Centre, DrawColor * alpha, Rotation - 90, Sprites.KnifeWarn.Bounds.Size.ToVector2() / 2);
        }
        public void GetCollide(Player.Heart player)
        {
            if (appearTime < delay) return;
            float A, B, C, dist;
            bool needAP = ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0;
            if (Rotation % 90 < 0.1f || Rotation % 90 > 89.9f)
                dist = Centre.X - Heart.Centre.X;
            else
            {
                float k = (float)Math.Tan(MathUtil.GetRadian(Rotation));
                A = k; B = -1;
                C = (-A * Centre.X) - (B * Centre.Y);
                dist = (float)(((A * Heart.Centre.X) + (B * Heart.Centre.Y) + C) / Math.Sqrt((A * A) + (B * B)));
            }

            float res = Math.Abs(dist) - 2 - (8.5f * scale);

            int offset = 3 - (int)JudgeState;

            if (res < 0)
            {
                if (!hasHit) PushScore(0);
                LoseHP(Heart);
                hasHit = true;
            }
            else if (res <= 1.6f - (offset * 0.4f))
            {
                if (score >= 2)
                {
                    score = 1;
                    Player.CreateCollideEffect(Color.LawnGreen, 3f);
                }
            }
            else if (res <= 4.2f - (offset * 1.2f))
            {
                if (score >= 3)
                {
                    score = 2;
                    Player.CreateCollideEffect(Color.LightBlue, 6f);
                }
            }
            if (score != 3 && needAP && MarkScore)
            {
                if (!hasHit)
                {
                    PushScore(0);
                    LoseHP(Heart);
                    hasHit = true;
                }
            }
        }

        float scale = 0;
        float rayAlpha = 1;
        public override void Update()
        {
            appearTime += 0.5f;
            Centre = vecease.Invoke(this);
            Rotation = rotease.Invoke(this);

            if (build)
            {
                PlaySound(Sounds.Warning);

                Line l = new((s) => Centre, (s) => Rotation);
                CreateEntity(l);
                build = false;
                AddInstance(new InstantEvent(delay, () =>
                {
                    PlaySound(Sounds.largeKnife, 0.7f);
                    l.Dispose();
                }));
            }
            if (AppearTime > delay)
            {
                scale = (scale * 0.9f) + (1 * 0.1f);
                rayAlpha -= 0.015f;
            }
            if (rayAlpha < 0) Dispose();
        }
        public override void Dispose()
        {
            if (!hasHit && MarkScore) PushScore(score);
            base.Dispose();
        }
    }
}