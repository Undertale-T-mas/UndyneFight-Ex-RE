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

            public bool RotateOffset { get; set; } = false;
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

            private bool _lastRotateOffsetState = false;
            public override void Update()
            {
                arrows.RemoveAll(s => s.Disposed);
                arrows.ForEach(s => SetArrowPos(s));
                if (RotateOffset ^ _lastRotateOffsetState)
                {
                    arrows.ForEach(s => s.RotateOffset = this.RotateOffset);
                    _lastRotateOffsetState = RotateOffset;
                }
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
                if (alphaEaseEnabled) _easingTimeMax = MathF.Max(_easingTimeMax, alphaEase.Time);

                maxIndex = ToArrayIndex(_easingTimeMax) + 1;
                if (maxIndex > 0)
                {
                    if (positionEaseEnabled)
                        if (positionBuffer == null || maxIndex > positionBuffer.Length)
                            positionBuffer = new Vector2[maxIndex];
                    if (rotationEaseEnabled)
                        if (rotationBuffer == null || maxIndex > rotationBuffer.Length)
                            rotationBuffer = new float[maxIndex];
                    if (distanceEaseEnabled)
                        if (distanceBuffer == null || maxIndex > distanceBuffer.Length)
                            distanceBuffer = new float[maxIndex];
                    if (alphaEaseEnabled)
                        if (alphaBuffer == null || maxIndex > alphaBuffer.Length)
                            alphaBuffer = new float[maxIndex];
                }
            }
            public float ApplyTime { get; set; } = 60;
            private float _easingTimeMax = 0;

            private EaseUnit<Vector2> positionEase;
            private EaseUnit<float> rotationEase;
            private EaseUnit<float> distanceEase;
            private EaseUnit<float> alphaEase;

            private bool positionEaseEnabled = false;
            private bool rotationEaseEnabled = false;
            private bool distanceEaseEnabled = false;
            private bool alphaEaseEnabled = false;

            public EaseUnit<Vector2> PositionEase { set { positionEase = value; positionEaseEnabled = true; arrayIndex = -1; Reset(); } }
            public EaseUnit<float> RotationEase { set { rotationEase = value; rotationEaseEnabled = true; arrayIndex = -1; Reset(); } }
            public EaseUnit<float> DistanceEase { set { distanceEase = value; distanceEaseEnabled = true; arrayIndex = -1; Reset(); } }
            public EaseUnit<float> AlphaEase { set { alphaEase = value; alphaEaseEnabled = true; arrayIndex = -1; Reset(); } }

            private Vector2[] positionBuffer;
            private float[] rotationBuffer;
            private float[] distanceBuffer;
            private float[] alphaBuffer;

            public Func<ICustomMotion, Vector2> PositionRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Func<ICustomMotion, float> RotationRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public float[] RotationRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public float[] PositionRouteParam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public float AppearTime { get; set; } = 0f;

            public float Rotation { get; set; } = 0f;
            public float TempAlpha { get; set; } = 0f;
            public float Distance { get; set; } = 0f;
            public Vector2 CentrePosition { get; set; }
            public float SelfRotation { get; set; } = 0f;
            public bool AutoDispose { get; internal set; }

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

                Vector2 realPos = Vector2.Zero;
                float realRot = 0, realDis = 0, realAlp = 0.0f;
                if (l == r)
                {
                    if (positionEaseEnabled)
                        realPos = positionBuffer[l] * Intensity;
                    if (rotationEaseEnabled)
                        realRot = rotationBuffer[l] * Intensity;
                    if (distanceEaseEnabled)
                        realDis = distanceBuffer[l] * Intensity;
                    if (alphaEaseEnabled)
                        realAlp = alphaBuffer[l] * Intensity;
                }
                else
                {
                    if (positionEaseEnabled)
                        realPos = Vector2.Lerp(positionBuffer[l], positionBuffer[r], add) * Intensity;
                    if (rotationEaseEnabled)
                        realRot = MathHelper.Lerp(rotationBuffer[l], rotationBuffer[r], add) * Intensity;
                    if (distanceEaseEnabled)
                        realDis = MathHelper.Lerp(distanceBuffer[l], distanceBuffer[r], add) * Intensity;
                    if (alphaEaseEnabled)
                        realAlp = MathHelper.Lerp(alphaBuffer[l], alphaBuffer[r], add) * 1.0f;
                }

                if (positionEaseEnabled)
                    arr.Offset = realPos;
                if (rotationEaseEnabled)
                    arr.CentreRotationOffset = realRot;
                if (distanceEaseEnabled)
                    arr.additiveDistance = realDis;
                if (alphaEaseEnabled)
                    arr.Alpha = realAlp;
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
                if (alphaEaseEnabled)
                    alphaBuffer[arrayIndex] = TempAlpha = alphaEase.Easing.Invoke(this);

                if(AppearTime > 20 && AutoDispose)
                {
                    if (arrows.Count == 0) this.Dispose();
                }
            }
        }

        public class ClassicApplier : ArrowEasing
        {
            public void ApplyDelay(float delay)
            {
                tuples.Add(new(delay, DelayControl.DelayType.Pull));
            }
            public void ApplyStop(float stopTime)
            {
                tuples.Add(new(stopTime, DelayControl.DelayType.Stop));
            }
            List<Tuple<float, DelayControl.DelayType>> tuples = new();
            
            public override void SetArrowPos(Arrow arr)
            {
                foreach(var pair in tuples)
                {
                    if (pair.Item2 == DelayControl.DelayType.Pull) arr.Delay(pair.Item1);
                    else arr.Stop(pair.Item1);
                }
            }
            public override void Update()
            {
                base.Update();
                tuples.Clear();
            }
        }
    }
}