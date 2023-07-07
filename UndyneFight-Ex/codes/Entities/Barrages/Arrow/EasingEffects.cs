using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;

namespace UndyneFight_Ex.Entities
{
    public partial class Arrow : Entity
    {
        public abstract class ArrowEasing : GameObject
        {
            private List<Arrow> arrows = new();

            public ArrowEasing() { UpdateIn120 = true; }

            public void TagApply(string tagName)
            {
                if (CurrentScene is SongFightingScene)
                    AddInstance(new InstantEvent(1.2f, () =>
                    {
                        SetArrowSet((CurrentScene as SongFightingScene).Accuracy.TaggedArrows[tagName]);
                    }));
            }

            public void SetArrowSet(List<Arrow> arrows)
            {
                this.arrows.AddRange(arrows);
            }

            public abstract void SetArrowPos(Arrow arr);
            public override void Update()
            {
                arrows.RemoveAll(s => s.Disposed);
                arrows.ForEach(s => SetArrowPos(s));
            }

            public float Intensity { get; set; } = 1.0f;
        }
        public class EnsembleEasing : ArrowEasing
        {
            private Vector2 _deltaEasing = Vector2.Zero;
            private float _revolutionEasing = 0;
            private float _rotationEasing = 0;
            private float _distanceEasing = 0;

            public EnsembleEasing() { }

            public void DeltaEase(params EaseUnit<Vector2>[] deltaEases)
            {
                RunEase((s) =>
                {
                    _deltaEasing = s;
                }, false,
                    deltaEases);
            }
            public void RevolutionEase(params EaseUnit<float>[] rotationEases)
            {
                RunEase((s) => { _revolutionEasing = s; }, rotationEases);
            }
            public void SelfRotationEase(params EaseUnit<float>[] rotationEases)
            {
                RunEase((s) => { _rotationEasing = s; }, rotationEases);
            }
            public void DistanceEase(params EaseUnit<float>[] distanceEases)
            {
                RunEase((s) => { _distanceEasing = s; }, distanceEases);
            }

            public override void SetArrowPos(Arrow arr)
            {
                arr.Offset = _deltaEasing * Intensity;
                arr.CentreRotationOffset = _revolutionEasing * Intensity;
                arr.SelfRotationOffset = _rotationEasing * Intensity;
                arr.additiveDistance = _distanceEasing * Intensity;
            }
        }
        public class UnitEasing : ArrowEasing, ICustomMotion
        {
            public UnitEasing()
            {

            }

            public override void Start()
            {
                Reset();
                base.Start();
            }

            private void Reset()
            {
                _easingTimeMax = 0;
                if (positionEaseEnabled) _easingTimeMax = MathF.Max(_easingTimeMax, positionEase.Time);
                if (rotationEaseEnabled) _easingTimeMax = MathF.Max(_easingTimeMax, rotationEase.Time);
                if (distanceEaseEnabled) _easingTimeMax = MathF.Max(_easingTimeMax, distanceEase.Time);

                maxIndex = ToArrayIndex(_easingTimeMax) + 1;
                if (maxIndex > 0)
                {
                    positionBuffer = new Vector2[maxIndex];
                    rotationBuffer = new float[maxIndex];
                    distanceBuffer = new float[maxIndex];
                }
            }
            public float ApplyTime { get; set; } = 60;
            private float _easingTimeMax = 0;

            private EaseUnit<Vector2> positionEase;
            private EaseUnit<float> rotationEase;
            private EaseUnit<float> distanceEase;

            private bool positionEaseEnabled = false;
            private bool rotationEaseEnabled = false;
            private bool distanceEaseEnabled = false;

            public EaseUnit<Vector2> PositionEase { set { positionEase = value; positionEaseEnabled = true; arrayIndex = -1; Reset(); } }
            public EaseUnit<float> RotationEase { set { rotationEase = value; rotationEaseEnabled = true; arrayIndex = -1; Reset(); } }
            public EaseUnit<float> DistanceEase { set { distanceEase = value; distanceEaseEnabled = true; arrayIndex = -1; Reset(); } }

            private Vector2[] positionBuffer;
            private float[] rotationBuffer;
            private float[] distanceBuffer;

            public Func<ICustomMotion, Vector2> PositionRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Func<ICustomMotion, float> RotationRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public float[] RotationRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public float[] PositionRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public float AppearTime { get; set; } = 0f;

            public float Rotation { get; set; } = 0f;
            public float Distance { get; set; } = 0f;
            public Vector2 CentrePosition { get; set; }
            public float SelfRotation { get; set; } = 0f;

            int maxIndex = 0;
            int arrayIndex = -1;

            private int ToArrayIndex(float x) => (int)((x - 0.5f) * 2f);

            public override void SetArrowPos(Arrow arr)
            {
                float time = ApplyTime - arr.TimeDelta;
                if (time < 0.5f) return;
                if (MathF.Abs(SelfRotation) > 1) { arr.SelfRotationOffset = SelfRotation; }
                int l = ToArrayIndex(time), r = l + 1;

                if (l >= maxIndex + 2) return;

                if (l >= maxIndex) l = maxIndex - 1;
                if (r >= maxIndex) r = maxIndex - 1;

                while (r > arrayIndex) UpdateEase();

                float add = (time - 0.5f) * 2f - l;

                Vector2 realPos;
                float realRot, realDis;
                if (l == r)
                {
                    realPos = positionBuffer[l] * Intensity;
                    realRot = rotationBuffer[l] * Intensity;
                    realDis = distanceBuffer[l] * Intensity;
                }
                else
                {
                    realPos = Vector2.Lerp(positionBuffer[l], positionBuffer[r], add) * Intensity;
                    realRot = MathHelper.Lerp(rotationBuffer[l], rotationBuffer[r], add) * Intensity;
                    realDis = MathHelper.Lerp(distanceBuffer[l], distanceBuffer[r], add) * Intensity;
                }

                arr.Offset = realPos;
                arr.CentreRotationOffset = realRot;
                arr.additiveDistance = realDis;
            }

            private void UpdateEase()
            {
                AppearTime += 0.5f;

                arrayIndex++;

                if (positionEaseEnabled)
                    positionBuffer[arrayIndex] = CentrePosition = positionEase.Easing.Invoke(this);
                if (rotationEaseEnabled)
                    rotationBuffer[arrayIndex] = Rotation = rotationEase.Easing.Invoke(this);
                if (distanceEaseEnabled)
                    distanceBuffer[arrayIndex] = Distance = distanceEase.Easing.Invoke(this);
            }
        }
    }
}