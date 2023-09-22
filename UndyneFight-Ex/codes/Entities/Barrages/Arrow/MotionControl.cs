using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Settings;
using static UndyneFight_Ex.Fight.Functions;

namespace UndyneFight_Ex.Entities
{
    public partial class Arrow : ICustomMotion
    {
        private void PositionCalculate()
        { 
            additiveRotation = RotationRoute != null ? RotationRoute.Invoke(this) : 0;
            additiveRotation += CentreRotationOffset;

            if (PositionRoute == null)
            {
                if (rotatingType == 1)
                {
                    distance = (shootShieldTime - GametimeF) * Speed;
                    if (distance >= 186)
                        distance -= 75 * 1.5f;
                    else if (distance >= 36)
                        distance -= (distance - 36) * 0.75f;

                    float d = MathUtil.Clamp(0, distance - 36, 37.5f) / 37.5f;
                    Rotation = missionRotation + d * (float)Math.Sqrt(d) * 180 * RotateScale;
                }
                else
                {
                    distance = (shootShieldTime - GametimeF) * Speed;
                    Rotation = missionRotation + (hasGreenFlag ? 45 : 0);
                }
                if (isRotate)
                    Rotation += (shootShieldTime - GametimeF) * 2.5f * RotateScale;

                float trueSPP = speedUpPlace * ((way % 2 == 0) ? 1.35f : 1.0f);
                if (isSpeedUp && distance >= trueSPP)
                    distance -= (distance - trueSPP) / 1.8f;
                else if (Rand(0, 1) == 0 && isSpeedUp)
                {
                    Vector2 speed = MathUtil.GetVector2(Rand(20, 30) / 10f, Rotation + Rand(-25, 25));
                    Color color = Rand(0, 1) == 0 ? Color.Red : Color.Orange;
                    GameStates.InstanceCreate(new Particle(color, speed, 20 + Rand(0, 12), Centre, FightResources.Sprites.firePartical));
                }
                if (!mission.FixArrow)
                    Rotation += mission.Rotation;
                if (distance < 0)
                {
                    if (LateWaitingScale > 0.4f)
                        distance *= 0.8f * MathF.Max(LateWaitingScale, 1 + distance / 200f);
                    else
                    {
                        distanceFactor = 0.4f / LateWaitingScale;
                        distance *= 0.8f * MathF.Max(0.4f, 1 + distance / 200f);
                    }
                    distance *= 0.9f;
                }

                float extraDist = (Scale - 1) * 10f + additiveDistance;
                Vector2 offset = Offset;
                if (RotateOffset) offset = MathUtil.Rotate(offset, this.Rotation + this.CentreRotationOffset);
                Centre = MissionCentre + MathUtil.GetVector2(distance + 44 + 5 + extraDist + (hasGreenFlag ? 5 : 0), Rotation + CentreRotationOffset) + offset;
            }
            else
            {
                Vector2 res1 = PositionRoute.Invoke(this);
                float dist = res1.Length(), rotation = res1.Direction();
                rotation += Rotation;
                dist += 44 + 5 + (hasGreenFlag ? 5 : 0);
                float extraDist = (Scale - 1) * 10f + additiveDistance;
                Centre = MissionCentre + MathUtil.GetVector2(dist + extraDist, rotation + CentreRotationOffset) * SettingsManager.DataLibrary.ArrowSpeed;
            }

            if (lastScale != Scale)
            {
                Scale *= basicScale;
                lastScale = Scale;
            }
        }

        float lastScale = -0.5f;

        private float distanceFactor = 1.0f;

        private float additiveRotation = 0;
        private float additiveDistance = 0;

        public Func<ICustomMotion, Vector2> PositionRoute { get; set; } = null;
        public Func<ICustomMotion, float> RotationRoute { get; set; } = null;
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

        public Vector2 MissionCentre { set {
                _anchorOnHeart = false;
                _missionCentre = value;
            }
            get => _anchorOnHeart ? mission.Centre : _missionCentre; 
        }
        private Vector2 _missionCentre;
        private bool _anchorOnHeart = true;

        public Vector2 CentrePosition => Centre - mission.Centre;

        public float LateWaitingScale { get; set; } = 0.5f;
        public Vector2 Offset { get; set; }

        public bool RotateOffset { get; set; } = false;
    }
}