using Microsoft.Xna.Framework;
using System;
using static System.Math;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public class NormalSpear : Spear
    {
        private readonly float missionRotation = 0;
        public float Speed { set; private get; } = 1.7f;
        public float Acceleration { set; private get; } = 0.12f;
        private int appearTime = 0;

        public bool IsMute { set; private get; }
        public bool DelayTargeting { set; private get; }

        public float WaitingTime { get; set; } = 59;
        public bool Rebound { get; set; } = false;
        public int ReboundCount { get; set; } = 3;
        public Vector2[] ReboundVertexs { get; set; } = { new(0, 0), new(640, 0), new(640, 480), new(0, 480)};
        public float Duration { private get; set; } = 200;

        public NormalSpear(Vector2 centre) : this(centre, (float)(Atan2(Heart.Centre.Y - centre.Y, Heart.Centre.X - centre.X) * 180 / Math.PI)) { }
        public NormalSpear(Vector2 centre, float rotation)
        {
            Rotation = Rand(0, 359);
            Centre = centre;
            missionRotation = rotation;
        }
        public NormalSpear(Vector2 centre, float rotation, float speed) : this(centre, rotation)
        {
            Speed = speed;
        }

        public override void Update()
        {
            if (appearTime == 0)
            {
                if (!IsMute)
                {
                    FightResources.Sounds.spearAppear.CreateInstance().Play();
                }
            }
            appearTime++;
            if (appearTime < WaitingTime)
            {
                Rotation += (float)Pow(WaitingTime + 8 - appearTime, 1.5) / 31f * (59 / WaitingTime);
            }
            else if (appearTime == (int)WaitingTime + 1)
            {
                Rotation = (DelayTargeting ? (float)(Atan2(Heart.Centre.Y - Centre.Y, Heart.Centre.X - Centre.X) * 180 / Math.PI)
                    : missionRotation);

                if (!IsMute)
                {
                    FightResources.Sounds.spearShoot.CreateInstance().Play();
                }
            }
            else
            {
                Speed += Acceleration;
                Centre += GetVector2(Speed, Rotation);
            }
            if (appearTime >= Duration)
            {
                alpha -= 0.16f;
                if (alpha <= 0) Dispose();
            }
            else alpha = Min(appearTime, 25) * 0.04f;

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }

    public class Pike : Spear
    {
        private float speed = 9.7f;
        private readonly float waitingTime;
        private int appearTime = 0;

        private bool isSpawnMute = false, isShootMute = false;
        public static bool shootSoundPlayed = false, spawnSoundPlayed = false;

        public float Speed { set => speed = value; }
        public float Acceleration { private get; set; } = 0.41f;
        public bool IsSpawnMute { set => isSpawnMute = value; }
        public bool IsShootMute { set => isShootMute = value; }

        private readonly float alphaChangeTime = 20;

        public Pike(Vector2 centre, float waitingTime) : this(centre, (float)(Atan2(Heart.Centre.Y - centre.Y, Heart.Centre.X - centre.X) * 180 / Math.PI), waitingTime) { }
        public Pike(Vector2 centre, float rotation, float waitingTime)
        {
            alphaChangeTime = 20 - 12 / waitingTime;
            Centre = centre;
            Rotation = rotation;
            this.waitingTime = waitingTime;
        }
        public Pike(Vector2 centre, float rotation, float speed, float waitingTime) : this(centre, rotation, waitingTime)
        {
            this.speed = speed;
        }

        public override void Update()
        {
            if (appearTime == 0 && (!isSpawnMute) && (!spawnSoundPlayed))
            {
                spawnSoundPlayed = true;
                FightResources.Sounds.spearAppear.CreateInstance().Play();
            }
            appearTime++;
            alpha = Max(alpha, Min(appearTime, alphaChangeTime) / alphaChangeTime);
            if (appearTime < waitingTime && appearTime <= 72)
            {
                Centre += GetVector2((float)Math.Cos(appearTime / 25f), Rotation);
            }
            else if (appearTime >= waitingTime)
            {
                if (appearTime == waitingTime && (!shootSoundPlayed) && (!isShootMute))
                {
                    shootSoundPlayed = true;
                    FightResources.Sounds.spearShoot.CreateInstance().Play();
                }
                speed += Acceleration;
                Centre += GetVector2(speed, Rotation);
            }
            if (appearTime >= waitingTime + 240)
            {
                Dispose();
            }

            base.Update();
        }
    }

    public class SwarmSpear : Spear
    {
        public Vector2 missionCentre;
        private int appearTime = 0;
        private readonly float acclen = 0.15f;
        private float linearSpeed;
        private float distance;
        private readonly float waitingTime;
        private readonly float missionRotation;
        private float appearRotation = 320f;

        public SwarmSpear(Vector2 rotateCentre, float linearSpeed, float distance, float rotation, float waitingTime)
        {
            this.waitingTime = waitingTime;
            missionCentre = rotateCentre;
            this.linearSpeed = linearSpeed;
            this.distance = distance;
            missionRotation = rotation;
        }

        public override void Update()
        {
            appearTime++;
            if (appearTime <= 22f)
            {
                alpha = appearTime / 22f;
            }

            if (appearTime > waitingTime)
            {
                linearSpeed += acclen;
                distance -= linearSpeed;
            }
            else
            {
                if (waitingTime < 34)
                {
                    appearRotation *= (1 - 1.3f / waitingTime);
                    appearRotation -= 5f / waitingTime;
                }
                appearRotation -= 0.8f;
                appearRotation = Max(appearRotation * 0.9f, 0);
            }

            Rotation = missionRotation;

            Centre = missionCentre + GetVector2(distance, Rotation + 180);

            Rotation += appearRotation;

            if (distance < 10)
            {
                alpha -= 0.08f;
            }

            if (alpha < 0.12f && distance < 10f)
            {
                Dispose();
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }

    public class CircleSpear : Spear
    {
        public Vector2 rotateCentre;
        private int appearTime = 0;
        private float rotateSpeed;
        private readonly float linearSpeed;
        private float distance;
        private readonly float rotateFriction = 0.01f;

        public CircleSpear(Vector2 rotateCentre, float rotateSpeed, float linearSpeed, float distance, float rotation, float rotateFriction) : this(rotateCentre, rotateSpeed, linearSpeed, distance, rotation)
        {
            this.rotateFriction = rotateFriction;
        }

        public CircleSpear(Vector2 rotateCentre, float rotateSpeed, float linearSpeed, float distance, float rotation)
        {
            autoDispose = false;
            this.rotateSpeed = rotateSpeed;
            this.linearSpeed = linearSpeed;
            this.distance = distance;
            Rotation = rotation;
            this.rotateCentre = rotateCentre;
        }

        public override void Update()
        {
            appearTime++;
            if (appearTime <= 15f)
            {
                alpha = appearTime / 15f;
            }

            Rotation += rotateSpeed;
            rotateSpeed *= (1 - rotateFriction);

            distance -= linearSpeed;

            Centre = rotateCentre + GetVector2(distance, Rotation + 180);

            if (distance < 10)
            {
                alpha -= 0.08f;
            }

            if (alpha < 0.12f && distance < 10f)
            {
                Dispose();
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }

    public class CustomSpear : Spear, ICustomMotion
    {
        public Vector2 CentrePosition => delta;

        public float[] PositionRouteParam { get; set; }
        public float[] LengthRouteParam { get; set; }
        public float[] RotationRouteParam { get; set; }
        public float AppearTime => appearTime;

        public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
        public Func<ICustomMotion, float> RotationRoute { get; set; }

        private Vector2 delta, startPos;
        float appearTime = 0;

        public CustomSpear(Vector2 startPos, Func<ICustomMotion, Vector2> positionRoute, Func<ICustomMotion, float> rotationRoute)
        {
            UpdateIn120 = true;
            alpha = 0.0f;
            Centre = startPos;
            this.startPos = startPos;
            PositionRoute = positionRoute;
            RotationRoute = rotationRoute;
        }
        public override void Update()
        {
            appearTime += 0.5f;
            Vector2 v = PositionRoute(this);
            Centre = startPos + v;
            delta = v;
            Rotation = RotationRoute(this);
            base.Update();
        }
        public void AlphaIncrease(float time)
        {
            float del = (1 - alpha) / (time * 2f);
            AddInstance(new TimeRangedEvent(time, () =>
            {
                alpha = Min(1, alpha + del);
            })
            { UpdateIn120 = true });
            AddInstance(new InstantEvent(time + 0.5f, () => { alpha = 1; }));
        }
        public void AlphaDecrease(float time)
        {
            float del = (alpha) / (time * 2f);
            AddInstance(new TimeRangedEvent(time, () =>
            {
                alpha = Max(0, alpha - del);
            })
            { UpdateIn120 = true });
            AddInstance(new InstantEvent(time + 0.5f, () => { alpha = 0; Dispose(); }));
        }
    }
}