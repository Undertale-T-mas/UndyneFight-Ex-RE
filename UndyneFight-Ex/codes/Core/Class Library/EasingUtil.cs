using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;

namespace UndyneFight_Ex.Entities
{
    public static class SimplifiedEasing
    {
        public class VEaseBuilder
        {
            List<EaseUnit<float>> eases = new();
            public bool Adjust { private get; set; } = true;
            public void Insert(EaseUnit<float> unit)
            {
                eases.Add(unit);
            }
            public EaseUnit<float> GetResult() => LinkEase(Adjust, eases.ToArray());
            public void Run(Action<float> func)
            {
                VirtualEasingObject easingObject = new();
                var ease = GetResult();
                AddInstance(new TimeRangedEvent(ease.Time, () =>
                {
                    easingObject.AppearTime += 0.5f;
                    func.Invoke(ease.Easing.Invoke(easingObject));
                })
                { UpdateIn120 = true });
                easingObject.AppearTime += 0.5f;
                func.Invoke(ease.Easing.Invoke(easingObject));
            }
        }
        public class CEaseBuilder
        {
            List<EaseUnit<Vector2>> eases = new();
            public bool Adjust { private get; set; } = true;
            public void Insert(EaseUnit<Vector2> unit)
            {
                eases.Add(unit);
            }
            public EaseUnit<Vector2> GetResult() => LinkEase(Adjust, eases.ToArray());
            public void Run(Action<Vector2> func)
            {
                VirtualEasingObject easingObject = new();
                var ease = GetResult();
                AddInstance(new TimeRangedEvent(ease.Time, () =>
                {
                    easingObject.AppearTime += 0.5f;
                    func.Invoke(ease.Easing.Invoke(easingObject));
                })
                { UpdateIn120 = true });
                easingObject.AppearTime += 0.5f;
                func.Invoke(ease.Easing.Invoke(easingObject));
            }
        }

        /// <summary>
        /// 模拟缓动物件，模拟值从CentrePosition提取
        /// </summary>
        class VirtualEasingObject : GameObject, ICustomMotion
        {
            public VirtualEasingObject(ICustomMotion copy)
            {
                PositionRoute = copy.PositionRoute;
                RotationRoute = copy.RotationRoute;
                AppearTime = copy.AppearTime;
                CentrePosition = copy.CentrePosition;
                PositionRouteParam = copy.PositionRouteParam;
                RotationRouteParam = copy.RotationRouteParam;
                UpdateIn120 = true;
            }
            public VirtualEasingObject()
            {
                UpdateIn120 = true;
                AppearTime = 0;
            }
            public static VirtualEasingObject ZeroTimeObj { get; }
            static VirtualEasingObject()
            {
                ZeroTimeObj = new VirtualEasingObject();
            }
            public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
            public Func<ICustomMotion, float> RotationRoute { get; set; }
            public float[] RotationRouteParam { get; set; }
            public float[] PositionRouteParam { get; set; }

            public float AppearTime { get; set; }

            public Vector2 CentrePosition { get; set; }
            public float Rotation { get; set; } = 0;

            public override void Update()
            {
                AppearTime += 0.5f;
                if (PositionRoute != null)
                    CentrePosition = PositionRoute.Invoke(this);
                if (RotationRoute != null)
                    Rotation = RotationRoute.Invoke(this);
            }
        }
        
        public static void RunEase(Action<Vector2> action, bool isAdjust, params EaseUnit<Vector2>[] funcs)
        {
            CEaseBuilder builder = new();
            builder.Adjust = isAdjust;
            for (int i = 0; i < funcs.Length; i++)
                builder.Insert(funcs[i]);
            builder.Run(action);
        }
        /// <summary>
        /// 执行<see langword="action"/>中的事件并使用其中的<see cref="Vector2"/>变量。变量等于<see langword="funcs"/>中的缓动变量
        /// </summary>
        /// <param name="action"></param>
        /// <param name="funcs"></param>
        public static void RunEase(Action<Vector2> action, params EaseUnit<Vector2>[] funcs)
        {
            
            RunEase(action, true, funcs);
        }
        public static void RunEase(Action<float> action, bool isAdjust, params EaseUnit<float>[] funcs)
        {
            EasingUtil.ValueEasing.EaseBuilder builder = new();
            builder.Adjust = isAdjust;
            for (int i = 0; i < funcs.Length; i++)
                builder.Insert(funcs[i].Time, funcs[i].Easing);
            builder.Run(action);
        }
        /// <summary>
        /// 执行<see langword="action"/>中的事件并使用其中的<see cref="float"/>变量。变量等于<see langword="funcs"/>中的缓动变量
        /// </summary>
        /// <param name="action"></param>
        /// <param name="funcs"></param>
        public static void RunEase(Action<float> action, params EaseUnit<float>[] funcs)
        {
            RunEase(action, true, funcs);
        }
        public static EaseUnit<float> LinkEase(bool isAdjust, params EaseUnit<float>[] funcs)
        {
            float time = funcs[0].Time;
            VirtualEasingObject easingObject = new();

            float[] startTimes = new float[funcs.Length];
            startTimes[0] = 0;
            for (int i = 1; i < funcs.Length; i++)
            {
                startTimes[i] = startTimes[i - 1] + funcs[i - 1].Time;
                time += funcs[i].Time;
            }
            float[] basis = new float[funcs.Length + 1];
            basis[0] = funcs[0].Start;
            if (isAdjust)
            {
                for (int i = 1; i <= funcs.Length; i++)
                {
                    basis[i] = basis[i - 1] + funcs[i - 1].End - funcs[i - 1].Start;
                }
            }
            else basis[^1] = funcs[^1].End;
            int curProgress = 0;
            float baseTime = 0;
            Func<ICustomMotion, float> easeResult = (s) =>
            {
                easingObject.AppearTime += s.AppearTime - baseTime;
                baseTime = s.AppearTime;
                while (curProgress < funcs.Length && easingObject.AppearTime >= funcs[curProgress].Time)
                {
                    easingObject.AppearTime -= funcs[curProgress].Time;
                    curProgress++;
                }
                while (curProgress > 0 && easingObject.AppearTime < 0)
                {
                    curProgress--;
                    easingObject.AppearTime += funcs[curProgress].Time;
                }
                return curProgress >= funcs.Length
                    ? basis[^1]
                    : !isAdjust
                    ? funcs[curProgress].Easing.Invoke(easingObject)
                    : funcs[curProgress].Easing.Invoke(easingObject) - funcs[curProgress].Start + basis[curProgress];
            };
            return new EaseUnit<float>(funcs[0].Start, basis[^1], time, easeResult);
        }
        /// <summary>
        /// 返回一个连接2个及以上的<see langword="缓动"/>
        /// </summary>
        /// <param name="funcs">每2个缓动间只需要逗号隔开</param>
        /// <returns></returns>
        public static EaseUnit<float> LinkEase(params EaseUnit<float>[] funcs)
        {
            return LinkEase(true, funcs);
        }
        public static EaseUnit<Vector2> LinkEase(bool isAdjust = true, params EaseUnit<Vector2>[] funcs)
        {
            float time = funcs[0].Time;
            VirtualEasingObject easingObject = new();

            float[] startTimes = new float[funcs.Length];
            startTimes[0] = 0;
            for (int i = 1; i < funcs.Length; i++)
            {
                startTimes[i] = startTimes[i - 1] + funcs[i - 1].Time;
                time += funcs[i].Time;
            }
            Vector2[] basis = new Vector2[funcs.Length + 1];
            basis[0] = funcs[0].Start;
            if (isAdjust)
            {
                for (int i = 1; i <= funcs.Length; i++)
                {
                    basis[i] = basis[i - 1] + funcs[i - 1].End - funcs[i - 1].Start;
                }
            }
            else basis[^1] = funcs[^1].End;
            int curProgress = 0;
            float baseTime = 0;
            Func<ICustomMotion, Vector2> easeResult = (s) =>
            {
                easingObject.AppearTime += s.AppearTime - baseTime;
                baseTime = s.AppearTime;
                while (curProgress < funcs.Length && easingObject.AppearTime >= funcs[curProgress].Time)
                {
                    easingObject.AppearTime -= funcs[curProgress].Time;
                    curProgress++;
                }
                while (curProgress > 0 && easingObject.AppearTime < 0)
                {
                    curProgress--;
                    easingObject.AppearTime += funcs[curProgress].Time;
                }
                return curProgress >= funcs.Length
                    ? basis[^1]
                    : !isAdjust
                    ? funcs[curProgress].Easing.Invoke(easingObject)
                    : funcs[curProgress].Easing.Invoke(easingObject) - funcs[curProgress].Start + basis[curProgress];
            };
            return new EaseUnit<Vector2>(funcs[0].Start, basis[^1], time, easeResult);
        }
        /// <summary>
        /// 返回一个连接2个及以上的<see langword="缓动"/>
        /// </summary>
        /// <param name="funcs">每2个缓动间只需要逗号隔开</param>
        /// <returns></returns>
        public static EaseUnit<Vector2> LinkEase(params EaseUnit<Vector2>[] funcs)
        {
            return LinkEase(true, funcs);
        }

        public enum EaseState
        {
            Linear = 0,
            Quad = 1,
            Cubic = 2,
            Quart = 3,
            Quint = 4,
            Circ = 5,
            Sine = 6,
            Elastic = 7,
            Expo = 8,
            Back = 9,
            Bounce = 10
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由慢到快的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseIn(float time, Vector2 start, Vector2 end, EaseState state)
        {
            return state switch
            {
                EaseState.Linear => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.Linear(start, end, time)),
                EaseState.Quad => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInQuad(start, end, time)),
                EaseState.Cubic => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInCubic(start, end, time)),
                EaseState.Quart => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInQuart(start, end, time)),
                EaseState.Quint => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInQuint(start, end, time)),
                EaseState.Circ => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInCirc(start, end, time)),
                EaseState.Sine => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInSine(start, end, time)),
                EaseState.Elastic => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInElastic(start, end, time)),
                EaseState.Expo => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInExpo(start, end, time)),
                EaseState.Back => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInBack(start, end, time)),
                EaseState.Bounce => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseInBounce(start, end, time)),
                _ => throw new ArgumentOutOfRangeException($"{state.ToString()} is not a proper easing state", nameof(state))
            };
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由快到慢的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseOut(float time, Vector2 start, Vector2 end, EaseState state)
        {
            return state switch
            {
                EaseState.Linear => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.Linear(start, end, time)),
                EaseState.Quad => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutQuad(start, end, time)),
                EaseState.Cubic => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutCubic(start, end, time)),
                EaseState.Quart => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutQuart(start, end, time)),
                EaseState.Quint => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutQuint(start, end, time)),
                EaseState.Circ => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutCirc(start, end, time)),
                EaseState.Sine => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutSine(start, end, time)),
                EaseState.Elastic => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutElastic(start, end, time)),
                EaseState.Expo => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutExpo(start, end, time)),
                EaseState.Back => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutBack(start, end, time)),
                EaseState.Bounce => new EaseUnit<Vector2>(start, end, time, EasingUtil.CentreEasing.EaseOutBounce(start, end, time)),
                _ => throw new ArgumentOutOfRangeException($"{state.ToString()} is not a proper easing state", nameof(state))
            };
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由慢到快的缓动"/><br/>
        /// Tips:该函数被拼接时，起点为0，可以理解为“在上一个函数的基础上增加<see langword="end"></see>”
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseIn(float time, Vector2 end, EaseState state)
        {
            return EaseIn(time, Vector2.Zero, end, state);
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由快到慢的缓动"/><br/>
        /// Tips:该函数被拼接时，起点为0，可以理解为“在上一个函数的基础上增加<see langword="end"></see>”
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseOut(float time, Vector2 end, EaseState state)
        {
            return EaseOut(time, Vector2.Zero, end, state);
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由慢到快再到慢的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="amount">起点缓动与中点缓动时间的距离占比<br/>如：0.4f 表示起点缓动移动的距离占40% 中点缓动移动的距离占60%<br/>但缓动时间之比仍然是1：1</param>
        /// <param name="Astate">起点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <param name="Bstate">中点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseInOut(float time, Vector2 start, Vector2 end, float amount, EaseState Astate, EaseState Bstate)
        {
            float midx = MathHelper.Lerp(start.X, end.X, amount);
            float midy = MathHelper.Lerp(start.Y, end.Y, amount);
            Vector2 mid = new(midx, midy);
            EaseUnit<Vector2> A;
            A = Astate switch
            {
                EaseState.Linear => EaseIn(time / 2, start, mid, EaseState.Linear),
                EaseState.Quad => EaseIn(time / 2, start, mid, EaseState.Quad),
                EaseState.Cubic => EaseIn(time / 2, start, mid, EaseState.Cubic),
                EaseState.Quart => EaseIn(time / 2, start, mid, EaseState.Quart),
                EaseState.Quint => EaseIn(time / 2, start, mid, EaseState.Quint),
                EaseState.Circ => EaseIn(time / 2, start, mid, EaseState.Circ),
                EaseState.Sine => EaseIn(time / 2, start, mid, EaseState.Sine),
                EaseState.Elastic => EaseIn(time / 2, start, mid, EaseState.Elastic),
                EaseState.Expo => EaseIn(time / 2, start, mid, EaseState.Expo),
                EaseState.Back => EaseIn(time / 2, start, mid, EaseState.Back),
                EaseState.Bounce => EaseIn(time / 2, start, mid, EaseState.Bounce),
                _ => throw new ArgumentOutOfRangeException($"{Astate.ToString()} is not a proper easing state", nameof(Astate))
            };
            EaseUnit<Vector2> B;
            B = Bstate switch
            {
                EaseState.Linear => EaseOut(time / 2, mid, end, EaseState.Linear),
                EaseState.Quad => EaseOut(time / 2, mid, end, EaseState.Quad),
                EaseState.Cubic => EaseOut(time / 2, mid, end, EaseState.Cubic),
                EaseState.Quart => EaseOut(time / 2, mid, end, EaseState.Quart),
                EaseState.Quint => EaseOut(time / 2, mid, end, EaseState.Quint),
                EaseState.Circ => EaseOut(time / 2, mid, end, EaseState.Circ),
                EaseState.Sine => EaseOut(time / 2, mid, end, EaseState.Sine),
                EaseState.Elastic => EaseOut(time / 2, mid, end, EaseState.Elastic),
                EaseState.Expo => EaseOut(time / 2, mid, end, EaseState.Expo),
                EaseState.Back => EaseOut(time / 2, mid, end, EaseState.Back),
                EaseState.Bounce => EaseOut(time / 2, mid, end, EaseState.Bounce),
                _ => throw new ArgumentOutOfRangeException($"{Bstate.ToString()} is not a proper easing state", nameof(Bstate))
            };
            return LinkEase(A, B);
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由快到慢再到快的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="amount">起点缓动与中点缓动时间的距离占比<br/>如：0.4f 表示起点缓动移动的距离占40% 中点缓动移动的距离占60%<br/>但缓动时间之比仍然是1：1</param>
        /// <param name="Astate">起点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <param name="Bstate">中点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseOutIn(float time, Vector2 start, Vector2 end, float amount, EaseState Astate, EaseState Bstate)
        {
            float midx = MathHelper.Lerp(start.X, end.X, amount);
            float midy = MathHelper.Lerp(start.Y, end.Y, amount);
            Vector2 mid = new(midx, midy);
            EaseUnit<Vector2> B;
            B = Astate switch
            {
                EaseState.Linear => EaseIn(time / 2, mid, end, EaseState.Linear),
                EaseState.Quad => EaseIn(time / 2, mid, end, EaseState.Quad),
                EaseState.Cubic => EaseIn(time / 2, mid, end, EaseState.Cubic),
                EaseState.Quart => EaseIn(time / 2, mid, end, EaseState.Quart),
                EaseState.Quint => EaseIn(time / 2, mid, end, EaseState.Quint),
                EaseState.Circ => EaseIn(time / 2, mid, end, EaseState.Circ),
                EaseState.Sine => EaseIn(time / 2, mid, end, EaseState.Sine),
                EaseState.Elastic => EaseIn(time / 2, mid, end, EaseState.Elastic),
                EaseState.Expo => EaseIn(time / 2, mid, end, EaseState.Expo),
                EaseState.Back => EaseIn(time / 2, mid, end, EaseState.Back),
                EaseState.Bounce => EaseIn(time / 2, mid, end, EaseState.Bounce),
                _ => throw new ArgumentOutOfRangeException($"{Bstate.ToString()} is not a proper easing state", nameof(Bstate))
            };
            EaseUnit<Vector2> A;
            A = Bstate switch
            {
                EaseState.Linear => EaseOut(time / 2, start, mid, EaseState.Linear),
                EaseState.Quad => EaseOut(time / 2, start, mid, EaseState.Quad),
                EaseState.Cubic => EaseOut(time / 2, start, mid, EaseState.Cubic),
                EaseState.Quart => EaseOut(time / 2, start, mid, EaseState.Quart),
                EaseState.Quint => EaseOut(time / 2, start, mid, EaseState.Quint),
                EaseState.Circ => EaseOut(time / 2, start, mid, EaseState.Circ),
                EaseState.Sine => EaseOut(time / 2, start, mid, EaseState.Sine),
                EaseState.Elastic => EaseOut(time / 2, start, mid, EaseState.Elastic),
                EaseState.Expo => EaseOut(time / 2, start, mid, EaseState.Expo),
                EaseState.Back => EaseOut(time / 2, start, mid, EaseState.Back),
                EaseState.Bounce => EaseOut(time / 2, start, mid, EaseState.Bounce),
                _ => throw new ArgumentOutOfRangeException($"{Astate.ToString()} is not a proper easing state", nameof(Astate))
            };
            return LinkEase(A, B);
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由慢到快再到慢的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="amount">起点缓动与中点缓动时间的距离占比<br/>如：0.4f 表示起点缓动移动的距离占40% 中点缓动移动的距离占60%<br/>但缓动时间之比仍然是1：1</param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseInOut(float time, Vector2 start, Vector2 end, float amount, EaseState state)
        {
            return EaseInOut(time, start, end, amount, state, state);
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由快到慢再到快的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="amount">起点缓动与中点缓动时间的距离占比<br/>如：0.4f 表示起点缓动移动的距离占40% 中点缓动移动的距离占60%<br/>但缓动时间之比仍然是1：1</param>
        /// <param name="state">起点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseOutIn(float time, Vector2 start, Vector2 end, float amount, EaseState state)
        {
            return EaseOutIn(time, start, end, amount, state, state);
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由慢到快再到慢的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseInOut(float time, Vector2 start, Vector2 end, EaseState state)
        {
            return EaseInOut(time, start, end, 0.5f, state, state);
        }
        /// <summary>
        /// 返回一个<see cref="Vector2"/>的<see langword="由快到慢再到快的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="state">起点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.CentreEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<Vector2> EaseOutIn(float time, Vector2 start, Vector2 end, EaseState state)
        {
            return EaseOutIn(time, start, end, 0.5f, state, state);
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由慢到快的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseIn(float time, float start, float end, EaseState state)
        {
            return state switch
            {
                EaseState.Linear => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.Linear(start, end, time)),
                EaseState.Quad => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInQuad(start, end, time)),
                EaseState.Cubic => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInCubic(start, end, time)),
                EaseState.Quart => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInQuart(start, end, time)),
                EaseState.Quint => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInQuint(start, end, time)),
                EaseState.Circ => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInCirc(start, end, time)),
                EaseState.Sine => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInSine(start, end, time)),
                EaseState.Elastic => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInElastic(start, end, time)),
                EaseState.Expo => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInExpo(start, end, time)),
                EaseState.Back => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInBack(start, end, time)),
                EaseState.Bounce => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseInBounce(start, end, time)),
                _ => throw new ArgumentOutOfRangeException($"{state.ToString()} is not a proper easing state", nameof(state))
            };
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由快到慢的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseOut(float time, float start, float end, EaseState state)
        {
            return state switch
            {
                EaseState.Linear => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.Linear(start, end, time)),
                EaseState.Quad => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutQuad(start, end, time)),
                EaseState.Cubic => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutCubic(start, end, time)),
                EaseState.Quart => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutQuart(start, end, time)),
                EaseState.Quint => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutQuint(start, end, time)),
                EaseState.Circ => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutCirc(start, end, time)),
                EaseState.Sine => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutSine(start, end, time)),
                EaseState.Elastic => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutElastic(start, end, time)),
                EaseState.Expo => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutExpo(start, end, time)),
                EaseState.Back => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutBack(start, end, time)),
                EaseState.Bounce => new EaseUnit<float>(start, end, time, EasingUtil.ValueEasing.EaseOutBounce(start, end, time)),
                _ => throw new ArgumentOutOfRangeException($"{state.ToString()} is not a proper easing state", nameof(state))
            };
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由慢到快的缓动"/><br/>
        /// Tips:该函数被拼接时，起点为0，可以理解为“在上一个函数的基础上增加<see langword="end"></see>”
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseIn(float time, float end, EaseState state)
        {
            return EaseIn(time, 0, end, state);
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由快到慢的缓动"/><br/>
        /// Tips:该函数被拼接时，起点为0，可以理解为“在上一个函数的基础上增加<see langword="end"></see>”
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseOut(float time, float end, EaseState state)
        {
            return EaseOut(time, 0, end, state);
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由慢到快再到慢的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="amount">起点缓动与中点缓动时间的距离占比<br/>如：0.4f 表示起点缓动移动的距离占40% 中点缓动移动的距离占60%<br/>但缓动时间之比仍然是1：1</param>
        /// <param name="Astate">起点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <param name="Bstate">中点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseInOut(float time, float start, float end, float amount, EaseState Astate, EaseState Bstate)
        {
            float mid = MathHelper.Lerp(start, end, amount);
            EaseUnit<float> A;
            A = Astate switch
            {
                EaseState.Linear => EaseIn(time / 2, start, mid, EaseState.Linear),
                EaseState.Quad => EaseIn(time / 2, start, mid, EaseState.Quad),
                EaseState.Cubic => EaseIn(time / 2, start, mid, EaseState.Cubic),
                EaseState.Quart => EaseIn(time / 2, start, mid, EaseState.Quart),
                EaseState.Quint => EaseIn(time / 2, start, mid, EaseState.Quint),
                EaseState.Circ => EaseIn(time / 2, start, mid, EaseState.Circ),
                EaseState.Sine => EaseIn(time / 2, start, mid, EaseState.Sine),
                EaseState.Elastic => EaseIn(time / 2, start, mid, EaseState.Elastic),
                EaseState.Expo => EaseIn(time / 2, start, mid, EaseState.Expo),
                EaseState.Back => EaseIn(time / 2, start, mid, EaseState.Back),
                EaseState.Bounce => EaseIn(time / 2, start, mid, EaseState.Bounce),
                _ => throw new ArgumentOutOfRangeException($"{Astate.ToString()} is not a proper easing state", nameof(Astate))
            };
            EaseUnit<float> B;
            B = Bstate switch
            {
                EaseState.Linear => EaseOut(time / 2, mid, end, EaseState.Linear),
                EaseState.Quad => EaseOut(time / 2, mid, end, EaseState.Quad),
                EaseState.Cubic => EaseOut(time / 2, mid, end, EaseState.Cubic),
                EaseState.Quart => EaseOut(time / 2, mid, end, EaseState.Quart),
                EaseState.Quint => EaseOut(time / 2, mid, end, EaseState.Quint),
                EaseState.Circ => EaseOut(time / 2, mid, end, EaseState.Circ),
                EaseState.Sine => EaseOut(time / 2, mid, end, EaseState.Sine),
                EaseState.Elastic => EaseOut(time / 2, mid, end, EaseState.Elastic),
                EaseState.Expo => EaseOut(time / 2, mid, end, EaseState.Expo),
                EaseState.Back => EaseOut(time / 2, mid, end, EaseState.Back),
                EaseState.Bounce => EaseOut(time / 2, mid, end, EaseState.Bounce),
                _ => throw new ArgumentOutOfRangeException($"{Bstate.ToString()} is not a proper easing state", nameof(Bstate))
            };
            return LinkEase(A, B);
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由快到慢再到快的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="amount">起点缓动与中点缓动时间的距离占比<br/>如：0.4f 表示起点缓动移动的距离占40% 中点缓动移动的距离占60%<br/>但缓动时间之比仍然是1：1</param>
        /// <param name="Astate">起点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <param name="Bstate">中点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseOutIn(float time, float start, float end, float amount, EaseState Astate, EaseState Bstate)
        {
            float mid = MathHelper.Lerp(start, end, amount);
            EaseUnit<float> B;
            B = Astate switch
            {
                EaseState.Linear => EaseIn(time / 2, mid, end, EaseState.Linear),
                EaseState.Quad => EaseIn(time / 2, mid, end, EaseState.Quad),
                EaseState.Cubic => EaseIn(time / 2, mid, end, EaseState.Cubic),
                EaseState.Quart => EaseIn(time / 2, mid, end, EaseState.Quart),
                EaseState.Quint => EaseIn(time / 2, mid, end, EaseState.Quint),
                EaseState.Circ => EaseIn(time / 2, mid, end, EaseState.Circ),
                EaseState.Sine => EaseIn(time / 2, mid, end, EaseState.Sine),
                EaseState.Elastic => EaseIn(time / 2, mid, end, EaseState.Elastic),
                EaseState.Expo => EaseIn(time / 2, mid, end, EaseState.Expo),
                EaseState.Back => EaseIn(time / 2, mid, end, EaseState.Back),
                EaseState.Bounce => EaseIn(time / 2, mid, end, EaseState.Bounce),
                _ => throw new ArgumentOutOfRangeException($"{Bstate.ToString()} is not a proper easing state", nameof(Bstate))
            };
            EaseUnit<float> A;
            A = Bstate switch
            {
                EaseState.Linear => EaseOut(time / 2, start, mid, EaseState.Linear),
                EaseState.Quad => EaseOut(time / 2, start, mid, EaseState.Quad),
                EaseState.Cubic => EaseOut(time / 2, start, mid, EaseState.Cubic),
                EaseState.Quart => EaseOut(time / 2, start, mid, EaseState.Quart),
                EaseState.Quint => EaseOut(time / 2, start, mid, EaseState.Quint),
                EaseState.Circ => EaseOut(time / 2, start, mid, EaseState.Circ),
                EaseState.Sine => EaseOut(time / 2, start, mid, EaseState.Sine),
                EaseState.Elastic => EaseOut(time / 2, start, mid, EaseState.Elastic),
                EaseState.Expo => EaseOut(time / 2, start, mid, EaseState.Expo),
                EaseState.Back => EaseOut(time / 2, start, mid, EaseState.Back),
                EaseState.Bounce => EaseOut(time / 2, start, mid, EaseState.Bounce),
                _ => throw new ArgumentOutOfRangeException($"{Astate.ToString()} is not a proper easing state", nameof(Astate))
            };
            return LinkEase(A, B);
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由慢到快再到慢的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="amount">起点缓动与中点缓动时间的距离占比<br/>如：0.4f 表示起点缓动移动的距离占40% 中点缓动移动的距离占60%<br/>但缓动时间之比仍然是1：1</param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseInOut(float time, float start, float end, float amount, EaseState state)
        {
            return EaseInOut(time, start, end, amount, state, state);
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由快到慢再到快的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="amount">起点缓动与中点缓动时间的距离占比<br/>如：0.4f 表示起点缓动移动的距离占40% 中点缓动移动的距离占60%<br/>但缓动时间之比仍然是1：1</param>
        /// <param name="state">起点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseOutIn(float time, float start, float end, float amount, EaseState state)
        {
            return EaseOutIn(time, start, end, amount, state, state);
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由慢到快再到慢的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="state">缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseInOut(float time, float start, float end,EaseState state)
        {
            return EaseInOut(time, start, end, 0.5f, state, state);
        }
        /// <summary>
        /// 返回一个<see cref="float"/>的<see langword="由快到慢再到快的缓动"/>
        /// </summary>
        /// <param name="time">缓动持续时间</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="state">起点缓动类型，类型均在<see cref="EaseState"/>中</param>
        /// <returns>注意返回类型并不是<see cref="EasingUtil.ValueEasing"/>，如需转换请在后面加上 .Easing</returns>
        public static EaseUnit<float> EaseOutIn(float time, float start, float end,EaseState state)
        {
            return EaseOutIn(time, start, end, 0.5f, state, state);
        }
        public static EaseUnit<Vector2> Stable(float time, Vector2 value)
        {
            return new(value, value, time, (s) => value);
        }
        public static EaseUnit<float> Stable(float time, float value)
        {
            return new(value, value, time, (s) => value);
        }
        public static EaseUnit<float> Stable(float time)
        {
            return new(0, 0, time, (s) => 0);
        }
        public static EaseUnit<Vector2> Copy(EaseUnit<Vector2> ease, int times)
        {
            EaseUnit<Vector2>[] easeUnits = new EaseUnit<Vector2>[times];
            for (int i = 0; i < times; i++) easeUnits[i] = new(ease.Start, ease.End, ease.Time, ease.Easing);
            return LinkEase(false, easeUnits);
        }
        public static EaseUnit<float> Copy(EaseUnit<float> ease, int times)
        {
            EaseUnit<float>[] easeUnits = new EaseUnit<float>[times];
            for (int i = 0; i < times; i++) easeUnits[i] = new(ease.Start, ease.End, ease.Time, ease.Easing);
            return LinkEase(false, easeUnits);
        }
        public static EaseUnit<Vector2> Alternate(float interval, EaseUnit<Vector2> main, params EaseUnit<Vector2>[] addons)
        {
            float curTime = 0;
            int curProgress = -1;
            return new EaseUnit<Vector2>(main.Start, main.End, main.Time, (s) =>
            {
                curTime += 0.5f;
                if (curTime > interval)
                {
                    curTime -= interval;
                    curProgress++;
                }
                if (curProgress == addons.Length) curProgress = -1;
                return curProgress == -1 ? main.Easing.Invoke(s) : addons[curProgress].Easing.Invoke(s);
            });
        }
        public static EaseUnit<float> Alternate(float interval, EaseUnit<float> main, params EaseUnit<float>[] addons)
        {
            float curTime = 0;
            int curProgress = -1;
            return new EaseUnit<float>(main.Start, main.End, main.Time, (s) =>
            {
                curTime += 0.5f;
                if (curTime > interval)
                {
                    curTime -= interval;
                    curProgress++;
                }
                if (curProgress == addons.Length) curProgress = -1;
                return curProgress == -1 ? main.Easing.Invoke(s) : addons[curProgress].Easing.Invoke(s);
            });
        }
        public static EaseUnit<Vector2> Add(EaseUnit<Vector2> main, EaseUnit<Vector2> addon)
        {
            return new EaseUnit<Vector2>(main.Start + addon.Start, main.End + addon.End,
                main.Time, (s) => { return main.Easing.Invoke(s) + addon.Easing.Invoke(s); });
        }
        public static EaseUnit<Vector2> Add(EaseUnit<Vector2> main, Vector2 addon)
        {
            return new EaseUnit<Vector2>(main.Start + addon, main.End + addon,
                main.Time, (s) => { return main.Easing.Invoke(s) + addon; });
        }
        public static EaseUnit<Vector2> Scale(EaseUnit<Vector2> origin, EaseUnit<float> scaler)
        {
            return new EaseUnit<Vector2>(origin.Start * scaler.Start, origin.End * scaler.End, origin.Time,
                (s) => { return origin.Easing.Invoke(s) * scaler.Easing.Invoke(s); });
        }
        public static EaseUnit<Vector2> Scale(EaseUnit<Vector2> origin, float scaler)
        {
            return new EaseUnit<Vector2>(origin.Start * scaler, origin.End * scaler, origin.Time,
                (s) => { return origin.Easing.Invoke(s) * scaler; });
        }
        public static EaseUnit<float> Scale(EaseUnit<float> origin, EaseUnit<float> scaler)
        {
            return new EaseUnit<float>(origin.Start * scaler.Start, origin.End * scaler.End, origin.Time,
                (s) => { return origin.Easing.Invoke(s) * scaler.Easing.Invoke(s); });
        }
        public static EaseUnit<float> Scale(EaseUnit<float> origin, float scaler)
        {
            return new EaseUnit<float>(origin.Start * scaler, origin.End * scaler, origin.Time,
                (s) => { return origin.Easing.Invoke(s) * scaler; });
        }
        public static EaseUnit<Vector2> Polar(EaseUnit<Vector2> main, EaseUnit<float> rotate)
        {
            return new EaseUnit<Vector2>(MathUtil.Rotate(main.Start, rotate.Start), MathUtil.Rotate(main.End, rotate.End),
                main.Time, (s) => { return MathUtil.Rotate(main.Start, rotate.Easing.Invoke(s)); });
        }
        public static EaseUnit<Vector2> Polar(EaseUnit<float> main, EaseUnit<float> rotate)
        {
            return new EaseUnit<Vector2>(MathUtil.GetVector2(main.Start, rotate.Start), MathUtil.GetVector2(main.End, rotate.End),
                main.Time, (s) => { return MathUtil.GetVector2(main.Start, rotate.Easing.Invoke(s)); });
        }
        public static EaseUnit<Vector2> Polar(EaseUnit<Vector2> main, float rotate)
        {
            return new EaseUnit<Vector2>(MathUtil.Rotate(main.Start, rotate), MathUtil.Rotate(main.End, rotate),
                main.Time, (s) => { return MathUtil.Rotate(main.Start, rotate); });
        }
        public static EaseUnit<float> Add(EaseUnit<float> main, EaseUnit<float> addon)
        {
            return new EaseUnit<float>(main.Start + addon.Start, main.End + addon.End,
                main.Time, (s) => { return main.Easing.Invoke(s) + addon.Easing.Invoke(s); });
        }
        public static EaseUnit<float> Add(EaseUnit<float> main, float addon)
        {
            return new EaseUnit<float>(main.Start + addon, main.End + addon,
                main.Time, (s) => { return main.Easing.Invoke(s) + addon; });
        }
    }
    public struct EaseUnit<T>
    {
        public float Time;
        public T Start { get; init; }
        public T End { get; init; }
        public Func<ICustomMotion, T> Easing;
        public EaseUnit(T start, T end, float time, Func<ICustomMotion, T> easing)
        {
            Start = start;
            End = end;
            Time = time;
            Easing = easing;
        }

    }
    public static class EasingUtil
    {
        /// <summary>
        /// 缓动库。注意缓动实体均为125帧制。
        /// </summary>
        public static class CentreEasing
        {
            public static Func<ICustomMotion, Vector2> Stable(Vector2 position) => (s) => { return position; };
            public static Func<ICustomMotion, Vector2> Stable(float x, float y) => (s) => { return new(x, y); };
            public static Func<ICustomMotion, Vector2> Linear(Vector2 speed) =>
                (s) => { return s.AppearTime * speed; };
            public static Func<ICustomMotion, Vector2> Linear(float Xspeed) =>
                (s) => { return s.AppearTime * new Vector2(Xspeed, 0); };
            public static Func<ICustomMotion, Vector2> FromDown(float distance, float time) =>
                (s) =>
                {
                    float cur = Math.Max(0, time - s.AppearTime) / time;
                    return new Vector2(0, cur * cur) * distance;
                };
            public static Func<ICustomMotion, Vector2> FromUp(float distance, float time) =>
                (s) =>
                {
                    float cur = Math.Max(0, time - s.AppearTime) / time;
                    return new Vector2(0, -cur * cur) * distance;
                };
            public static Func<ICustomMotion, Vector2> FromRight(float distance, float time) =>
                (s) =>
                {
                    float cur = Math.Max(0, time - s.AppearTime) / time;
                    return new Vector2(cur * cur, 0) * distance;
                };
            public static Func<ICustomMotion, Vector2> FromLeft(float distance, float time) =>
                (s) =>
                {
                    float cur = Math.Max(0, time - s.AppearTime) / time;
                    return new Vector2(-cur * cur, 0) * distance;
                };
            public static Func<ICustomMotion, Vector2> Circle(Vector2 centre, float radius, float roundTime, float startingRotation) =>
                (s) =>
                {
                    return centre + MathUtil.GetVector2(radius, s.AppearTime / roundTime * 360f + startingRotation);
                };
            public static Func<ICustomMotion, Vector2> Convert(Func<float, Vector2> timeParamEase) =>
                (s) =>
                {
                    return timeParamEase.Invoke(s.AppearTime);
                };
            public static Func<ICustomMotion, Vector2> Circle(Func<ICustomMotion, Vector2> easing, float radius, float roundTime, float startingRotation) =>
                (s) =>
                {
                    return easing.Invoke(s) + MathUtil.GetVector2(radius, s.AppearTime / roundTime * 360f + startingRotation);
                };
            public static Func<ICustomMotion, Vector2> Circle(Func<ICustomMotion, Vector2> easing, Func<ICustomMotion, float> radius, float roundTime, float startingRotation) =>
                (s) =>
                {
                    return easing.Invoke(s) + MathUtil.GetVector2(radius.Invoke(s), s.AppearTime / roundTime * 360f + startingRotation);
                };

            /// <summary>
            /// 构建一个摆动的正弦波的缓动
            /// </summary>
            /// <param name="intensity">振幅</param>
            /// <param name="cycleTime">每个波占的时间，即周期</param>
            /// <param name="startPhase">初始位置在第一个半波里面的比例位置。例如写0.5即从第一个半波的一半位置开始。</param>
            /// <param name="rotation">摆动方向</param>
            /// <returns></returns>
            public static Func<ICustomMotion, Vector2> SinWave(float intensity, float cycleTime, float startPhase, float rotation) =>
                (s) =>
                {
                    return MathUtil.GetVector2(Sin01(s.AppearTime * 2 / cycleTime + startPhase) * intensity, rotation);
                };
            /// <summary>
            /// 构建一个上下摆动的正弦波的缓动
            /// </summary>
            /// <param name="intensity">振幅</param>
            /// <param name="cycleTime">每个波占的时间，即周期</param>
            /// <param name="startPhase">初始位置在第一个半波里面的比例位置。例如写0.5即从第一个半波的一半位置开始。</param>
            /// <returns></returns>
            public static Func<ICustomMotion, Vector2> XSinWave(float intensity, float cycleTime, float startPhase) =>
                (s) =>
                {
                    Vector2 vec = new Vector2(Sin01(s.AppearTime * 2 / cycleTime + startPhase) * intensity, 0);
                    return vec;
                };
            /// <summary>
            /// 构建一个左右摆动的正弦波的缓动
            /// </summary>
            /// <param name="intensity">振幅</param>
            /// <param name="cycleTime">每个波占的时间，即周期</param>
            /// <param name="startPhase">初始位置在第一个半波里面的比例位置。例如写0.5即从第一个半波的一半位置开始。</param>
            /// <returns></returns>
            public static Func<ICustomMotion, Vector2> YSinWave(float intensity, float cycleTime, float startPhase) =>
                (s) =>
                {
                    Vector2 vec = new Vector2(0, Sin01(s.AppearTime * 2 / cycleTime + startPhase) * intensity);
                    return vec;
                };
            public static Func<ICustomMotion, Vector2> Accerlating(Vector2 speed, Vector2 accerlation) =>
                (s) =>
                {
                    return speed * s.AppearTime + accerlation * (0.5f * s.AppearTime * s.AppearTime);
                };

            public static Func<ICustomMotion, Vector2> Linear(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    return Vector2.Lerp(v1, v2, s.AppearTime / time);
                };
            public static Func<ICustomMotion, Vector2> EaseInSine(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, 1 - MathF.Cos((scale * MathF.PI) / 2));
                };
            public static Func<ICustomMotion, Vector2> EaseOutSine(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, MathF.Sin((scale * MathF.PI) / 2));
                };
            public static Func<ICustomMotion, Vector2> EaseInQuad(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, scale * scale);
                };
            public static Func<ICustomMotion, Vector2> EaseOutQuad(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, 1 - (1 - scale) * (1 - scale));
                };
            public static Func<ICustomMotion, Vector2> EaseInCubic(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, scale * scale * scale);
                };
            public static Func<ICustomMotion, Vector2> EaseOutCubic(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, 1 - (1 - scale) * (1 - scale) * (1 - scale));
                };
            public static Func<ICustomMotion, Vector2> EaseInQuart(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, scale * scale * scale * scale);
                };
            public static Func<ICustomMotion, Vector2> EaseInOutQuart(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;

                    Vector2 V1 = v2 * 0.5f;
                    return scale <= 0.49f ? EaseInQuart(v1, V1, time * 0.5f).Invoke(s) : EaseOutQuart(V1, v2, time * 0.5f).Invoke(s);
                };
            public static Func<ICustomMotion, Vector2> EaseInOutQuad(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;

                    Vector2 V1 = v2 * 0.5f;
                    return scale <= 0.49f ? EaseInQuad(v1, V1, time * 0.5f).Invoke(s) : EaseOutQuad(V1, v2, time * 0.5f).Invoke(s);
                };
            public static Func<ICustomMotion, Vector2> EaseOutQuart(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, 1 - (1 - scale) * (1 - scale) * (1 - scale) * (1 - scale));
                };
            public static Func<ICustomMotion, Vector2> EaseInQuint(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, scale * scale * scale * scale * scale);
                };
            public static Func<ICustomMotion, Vector2> EaseOutQuint(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, 1 - (1 - scale) * (1 - scale) * (1 - scale) * (1 - scale) * (1 - scale));
                };
            public static Func<ICustomMotion, Vector2> EaseInExpo(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, x == 0 ? 0 : MathF.Pow(2, 10 * x - 10));
                };
            public static Func<ICustomMotion, Vector2> EaseOutExpo(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, x == 1 ? 1 : 1 - MathF.Pow(2, -10 * x));
                };
            public static Func<ICustomMotion, Vector2> EaseInCirc(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, 1 - MathF.Sqrt(1 - x * x));
                };
            public static Func<ICustomMotion, Vector2> EaseOutCirc(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    return Vector2.Lerp(v1, v2, MathF.Sqrt(1 - (1 - x) * (1 - x)));
                };
            public static Func<ICustomMotion, Vector2> EaseInBack(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    float c1 = 1.70158f;
                    float c3 = c1 + 1;
                    float value = c3 * x * x * x - c1 * x * x;
                    return Vector2.Lerp(v1, v2, value);
                };
            public static Func<ICustomMotion, Vector2> EaseOutBack(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time - 1;
                    float c1 = 1.70158f;
                    float c3 = c1 + 1;
                    float value = 1 + c3 * x * x * x + c1 * x * x;
                    return Vector2.Lerp(v1, v2, value);
                };
            public static Func<ICustomMotion, Vector2> EaseInElastic(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;

                    float c4 = (2 * MathF.PI) / 3;

                    float value = x == 0 ? 0 : (x == 1 ? 1 :
                        -MathF.Pow(2, 10 * x - 10) * MathF.Sin((x * 10 - 10.75f) * c4)
                    );

                    return Vector2.Lerp(v1, v2, value);
                };
            public static Func<ICustomMotion, Vector2> EaseOutElastic(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = 1 - s.AppearTime / time;

                    float c4 = (2 * MathF.PI) / 3;

                    float value = x == 0 ? 0 : (x == 1 ? 1 :
                        -MathF.Pow(2, 10 * x - 10) * MathF.Sin((x * 10 - 10.75f) * c4)
                    );

                    return Vector2.Lerp(v1, v2, 1 - value);
                };
            public static Func<ICustomMotion, Vector2> EaseInBounce(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = 1 - s.AppearTime / time;
                    float n1 = 7.5625f;
                    float d1 = 2.75f;

                    float value = x < 1 / d1
                        ? n1 * x * x
                        : x < 2 / d1
                            ? n1 * (x - 1.5f / d1) * x + 0.75f
                            : x < 2.5 / d1 ? n1 * (x - 2.25f / d1) * x + 0.9375f : n1 * (x - 2.625f / d1) * x + 0.984375f;
                    return Vector2.Lerp(v1, v2, 1 - value);
                };
            public static Func<ICustomMotion, Vector2> EaseOutBounce(Vector2 v1, Vector2 v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    float n1 = 7.5625f;
                    float d1 = 2.75f;

                    float value = x < 1 / d1
                        ? n1 * x * x
                        : x < 2 / d1
                            ? n1 * (x - 1.5f / d1) * x + 0.75f
                            : x < 2.5 / d1 ? n1 * (x - 2.25f / d1) * x + 0.9375f : n1 * (x - 2.625f / d1) * x + 0.984375f;
                    return Vector2.Lerp(v1, v2, value);
                };

            public static Func<ICustomMotion, Vector2> LerpTo(Vector2 start, float scale, Func<ICustomMotion, Vector2> origin)
            {
                Vector2 curPos = start;
                return (s) =>
                {
                    curPos = Vector2.Lerp(curPos, origin.Invoke(s), scale);
                    return curPos;
                };
            }
            public static Func<ICustomMotion, Vector2> LerpTo(Vector2 start, float scale, Vector2 origin)
            {
                Vector2 curPos = start;
                return (s) =>
                {
                    curPos = Vector2.Lerp(curPos, origin, scale);
                    return curPos;
                };
            }
            public static Func<ICustomMotion, Vector2> Alternate(float time, params Func<ICustomMotion, Vector2>[] easings)
            {
                int curPhase = 0;
                float timer = 0;
                time *= 2;
                return (s) =>
                {
                    timer++;
                    if (timer >= time)
                    {
                        timer -= time;
                        curPhase++;
                    }
                    if (curPhase >= easings.Length)
                        curPhase = 0;
                    return easings[curPhase].Invoke(s);
                };
            }
            public static Func<ICustomMotion, Vector2> Alternate(params Func<ICustomMotion, Vector2>[] easings)
            {
                return Alternate(1, easings);
            }
            public static Func<ICustomMotion, Vector2> Intensify(Func<ICustomMotion, Vector2> easing, Func<ICustomMotion, float> scale)
            {
                return (s) =>
                    easing.Invoke(s) * scale(s);
            }

            public static Func<ICustomMotion, Vector2> Combine(Func<ICustomMotion, Vector2> ease1, Func<ICustomMotion, Vector2> ease2) =>
                (s) =>
                {
                    return ease1.Invoke(s) + ease2.Invoke(s);
                };
            public static Func<ICustomMotion, Vector2> Combine(Func<ICustomMotion, Vector2> ease1, Vector2 centre) =>
                (s) =>
                {
                    return ease1.Invoke(s) + centre;
                };
            public static Func<ICustomMotion, Vector2> Combine(Vector2 centre, Func<ICustomMotion, Vector2> ease1) =>
                (s) =>
                {
                    return ease1.Invoke(s) + centre;
                };
            public static Func<ICustomMotion, Vector2> Combine(Func<ICustomMotion, float> xEase, Func<ICustomMotion, float> yEase) =>
                (s) =>
                {
                    return new(xEase.Invoke(s), yEase.Invoke(s));
                };
            public static Func<ICustomMotion, Vector2> PolarCombine(Func<ICustomMotion, Vector2> centreEase, Func<ICustomMotion, float> rotationEase) =>
                (s) =>
                {
                    return MathUtil.Rotate(centreEase.Invoke(s), rotationEase.Invoke(s));
                };
            public static Func<ICustomMotion, Vector2> PolarCombine(Func<ICustomMotion, float> lengthEase, Func<ICustomMotion, float> rotationEase) =>
                (s) =>
                {
                    return MathUtil.GetVector2(lengthEase.Invoke(s), rotationEase.Invoke(s));
                };

            /// <summary>
            /// 模拟缓动物件，模拟值从CentrePosition提取
            /// </summary>
            class VirtualEasingObject : GameObject, ICustomMotion
            {
                public VirtualEasingObject(ICustomMotion copy)
                {
                    PositionRoute = copy.PositionRoute;
                    RotationRoute = copy.RotationRoute;
                    AppearTime = copy.AppearTime;
                    CentrePosition = copy.CentrePosition;
                    PositionRouteParam = copy.PositionRouteParam;
                    RotationRouteParam = copy.RotationRouteParam;
                    UpdateIn120 = true;
                }
                public VirtualEasingObject()
                {
                    UpdateIn120 = true;
                    AppearTime = 0;
                }
                public static VirtualEasingObject ZeroTimeObj { get; }
                static VirtualEasingObject()
                {
                    ZeroTimeObj = new VirtualEasingObject();
                }
                public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
                public Func<ICustomMotion, float> RotationRoute { get; set; }
                public float[] RotationRouteParam { get; set; }
                public float[] PositionRouteParam { get; set; }

                public float AppearTime { get; set; }

                public Vector2 CentrePosition { get; set; }
                public float Rotation { get; set; } = 0;

                public override void Update()
                {
                    AppearTime += 0.5f;
                    CentrePosition = PositionRoute.Invoke(this);
                }
            }
            /* Cycle
            /// <summary>
            /// 传入若干二元组，分别表示 每一个缓动的持续时长 和 缓动的函数，将它们按照时间串接在一起。
            /// </summary>
            /// <param name="motionPairs">缓动的二元组</param>
            /// <returns></returns>
            public static Func<ICustomMotion, Vector2> Cycle(params Tuple<float, Func<ICustomMotion, Vector2>>[] motionPairs)
            {
                float totalTime = 0;
                int curPhase = 0;
                float[] timeZone = new float[motionPairs.Length];
                for (int i = 0; i < motionPairs.Length; i++)
                {
                    totalTime += motionPairs[i].Item1;
                    timeZone[i] = totalTime;
                }
                Vector2 lastPhasePos = Vector2.Zero;
                int times = 0;
                return (s) =>
                {
                    float time = s.AppearTime - times * totalTime;
                    while(time >= timeZone[curPhase])
                    {
                        lastPhasePos = s.CentrePosition;
                        curPhase++;
                        if(curPhase == motionPairs.Length)
                        {
                            curPhase = 0;
                            times++;
                            time -= totalTime;
                        }
                    }
                    VirtualEasingObject obj = new(s);
                    if (curPhase > 0)
                        obj.AppearTime -= timeZone[curPhase - 1];
                    return lastPhasePos + motionPairs[curPhase].Item2.Invoke(obj);
                };
            }
            */
            public class EaseBuilder
            {
                public static implicit operator Func<ICustomMotion, Vector2>(EaseBuilder val)
                {
                    return val.GetResult();
                }
                public Vector2 OffsetPosition { get; set; }
                public bool Adjust { get; set; } = true;
                List<Tuple<float, Func<ICustomMotion, Vector2>>> motionPairs = new();
                public void Insert(float time, Func<ICustomMotion, Vector2> function)
                {
                    motionPairs.Add(new(time, function));
                }
                public void Run(Action<Vector2> action)
                {
                    VirtualEasingObject easer = new VirtualEasingObject();
                    AddInstance(easer);
                    easer.PositionRoute = GetResult();
                    AddInstance(new TimeRangedEvent(_totalTime, () =>
                    {
                        action.Invoke(easer.CentrePosition);
                    })
                    { UpdateIn120 = true }); ;
                    AddInstance(new InstantEvent(_totalTime, () => easer.Dispose()));
                }
                private float _totalTime;
                public Func<ICustomMotion, Vector2> GetResult()
                {
                    VirtualEasingObject obj = new();
                    VirtualEasingObject objEnd = new();

                    Tuple<float, Func<ICustomMotion, Vector2>>[] pairs = motionPairs.ToArray();
                    float[] timeZone = new float[pairs.Length];
                    float totalTime = 0;
                    Vector2[] startings = new Vector2[pairs.Length];
                    Vector2[] endings = new Vector2[pairs.Length];
                    for (int i = 0; i < pairs.Length; i++)
                    {
                        totalTime += pairs[i].Item1;
                        timeZone[i] = totalTime;
                        startings[i] = pairs[i].Item2.Invoke(obj);
                        objEnd.AppearTime = pairs[i].Item1;
                        endings[i] = pairs[i].Item2.Invoke(objEnd);
                    }
                    int curPhase = 0;
                    Vector2 basis = Vector2.Zero;
                    startings[0] = Vector2.Zero;
                    _totalTime = totalTime;

                    return !Adjust
                        ? ((s) =>
                        {
                            obj.AppearTime += 0.5f;
                            if (curPhase >= pairs.Length) return s.CentrePosition;
                            while (s.AppearTime >= timeZone[curPhase])
                            {
                                curPhase++;
                                obj.AppearTime = 0.5f;
                                if (curPhase >= pairs.Length) return s.CentrePosition;
                            }
                            return pairs[curPhase].Item2.Invoke(obj) + OffsetPosition;
                        })
                        : ((s) =>
                    {
                        obj.AppearTime += 0.5f;
                        if (curPhase >= pairs.Length) return s.CentrePosition;
                        while (s.AppearTime >= timeZone[curPhase])
                        {
                            basis = endings[curPhase] + basis - startings[curPhase];
                            curPhase++;
                            obj.AppearTime = 0.5f;
                            if (curPhase >= pairs.Length) return s.CentrePosition;
                        }
                        Vector2 result = pairs[curPhase].Item2.Invoke(obj) + basis - startings[curPhase] + OffsetPosition;
                        return result;
                    });
                }
                public void Stable(float time, Vector2 val)
                {
                    Insert(time, CentreEasing.Stable(val));
                }
                public void Stable(float time, float val1, float val2)
                {
                    Insert(time, CentreEasing.Stable(val1, val2));
                }
            }

        }

        /// <summary>
        /// 缓动库。注意缓动实体均为125帧制
        /// </summary>
        public static class ValueEasing
        {
            public static Func<ICustomMotion, float> Convert(Func<float, float> timeParamEase) =>
                (s) =>
                {
                    return timeParamEase.Invoke(s.AppearTime);
                };

            public static Func<ICustomMotion, float> Stable(float position) => (s) => { return position; };

            /// <summary>
            /// 构建一个正弦波的缓动
            /// </summary>
            /// <param name="intensity">振幅</param>
            /// <param name="cycleTime">每个波占的时间，即周期</param>
            /// <param name="startPhase">初始位置在第一个半波里面的比例位置。例如写0.5即从第一个半波的一半位置开始。</param>
            /// <returns></returns>
            public static Func<ICustomMotion, float> SinWave(float intensity, float cycleTime, float startPhase) =>
                (s) => Sin01(s.AppearTime * 2 / cycleTime + startPhase) * intensity;
            public static Func<ICustomMotion, float> Accerlating(float speed, float accerlation) =>
                (s) =>
                {
                    return speed * s.AppearTime + accerlation * (0.5f * s.AppearTime * s.AppearTime);
                };

            public static Func<ICustomMotion, float> Linear(float v1, float v2, float time) =>
                (s) =>
                {
                    return MathHelper.Lerp(v1, v2, s.AppearTime / time);
                };
            public static Func<ICustomMotion, float> Linear(float Xspeed) =>
                (s) => { return s.AppearTime * Xspeed; };
            public static Func<ICustomMotion, float> EaseInSine(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, 1 - MathF.Cos((scale * MathF.PI) / 2));
                };
            public static Func<ICustomMotion, float> EaseOutSine(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, MathF.Sin((scale * MathF.PI) / 2));
                };
            public static Func<ICustomMotion, float> EaseInQuad(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, scale * scale);
                };
            public static Func<ICustomMotion, float> EaseOutQuad(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, 1 - (1 - scale) * (1 - scale));
                };
            public static Func<ICustomMotion, float> EaseInCubic(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, scale * scale * scale);
                };
            public static Func<ICustomMotion, float> EaseOutCubic(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, 1 - (1 - scale) * (1 - scale) * (1 - scale));
                };
            public static Func<ICustomMotion, float> EaseInQuart(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, scale * scale * scale * scale);
                };
            public static Func<ICustomMotion, float> EaseOutQuart(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, 1 - (1 - scale) * (1 - scale) * (1 - scale) * (1 - scale));
                };
            public static Func<ICustomMotion, float> EaseInQuint(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, scale * scale * scale * scale * scale);
                };
            public static Func<ICustomMotion, float> EaseOutQuint(float v1, float v2, float time) =>
                (s) =>
                {
                    float scale = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, 1 - (1 - scale) * (1 - scale) * (1 - scale) * (1 - scale) * (1 - scale));
                };
            public static Func<ICustomMotion, float> EaseInExpo(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, x == 0 ? 0 : MathF.Pow(2, 10 * x - 10));
                };
            public static Func<ICustomMotion, float> EaseOutExpo(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, x == 1 ? 1 : 1 - MathF.Pow(2, -10 * x));
                };
            public static Func<ICustomMotion, float> EaseInCirc(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, 1 - MathF.Sqrt(1 - x * x));
                };
            public static Func<ICustomMotion, float> EaseOutCirc(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    return MathHelper.Lerp(v1, v2, MathF.Sqrt(1 - (1 - x) * (1 - x)));
                };
            public static Func<ICustomMotion, float> EaseInBack(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    float c1 = 1.70158f;
                    float c3 = c1 + 1;
                    float value = c3 * x * x * x - c1 * x * x;
                    return MathHelper.Lerp(v1, v2, value);
                };
            public static Func<ICustomMotion, float> EaseOutBack(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time - 1;
                    float c1 = 1.70158f;
                    float c3 = c1 + 1;
                    float value = 1 + c3 * x * x * x + c1 * x * x;
                    return MathHelper.Lerp(v1, v2, value);
                };
            public static Func<ICustomMotion, float> EaseInElastic(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;

                    float c4 = (2 * MathF.PI) / 3;

                    float value = x == 0 ? 0 : (x == 1 ? 1 :
                        -MathF.Pow(2, 10 * x - 10) * MathF.Sin((x * 10 - 10.75f) * c4)
                    );

                    return MathHelper.Lerp(v1, v2, value);
                };
            public static Func<ICustomMotion, float> EaseOutElastic(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = 1 - s.AppearTime / time;

                    float c4 = (2 * MathF.PI) / 3;

                    float value = x == 0 ? 0 : (x == 1 ? 1 :
                        -MathF.Pow(2, 10 * x - 10) * MathF.Sin((x * 10 - 10.75f) * c4)
                    );

                    return MathHelper.Lerp(v1, v2, 1 - value);
                };
            public static Func<ICustomMotion, float> EaseInBounce(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = 1 - s.AppearTime / time;
                    float n1 = 7.5625f;
                    float d1 = 2.75f;
                    float value = x < 1 / d1
                        ? n1 * x * x
                        : x < 2 / d1
                            ? n1 * (x - 1.5f / d1) * x + 0.75f
                            : x < 2.5 / d1 ? n1 * (x - 2.25f / d1) * x + 0.9375f : n1 * (x - 2.625f / d1) * x + 0.984375f;
                    return MathHelper.Lerp(v1, v2, 1 - value);
                };
            public static Func<ICustomMotion, float> EaseOutBounce(float v1, float v2, float time) =>
                (s) =>
                {
                    float x = s.AppearTime / time;
                    float n1 = 7.5625f;
                    float d1 = 2.75f;
                    float value = x < 1 / d1
                        ? n1 * x * x
                        : x < 2 / d1
                            ? n1 * (x - 1.5f / d1) * x + 0.75f
                            : x < 2.5 / d1 ? n1 * (x - 2.25f / d1) * x + 0.9375f : n1 * (x - 2.625f / d1) * x + 0.984375f;
                    return MathHelper.Lerp(v1, v2, value);
                };

            public static Func<ICustomMotion, float> LerpTo(float start, float scale, Func<ICustomMotion, float> origin)
            {
                float curPos = start;
                return (s) =>
                {
                    curPos = MathHelper.Lerp(curPos, origin.Invoke(s), scale);
                    return curPos;
                };
            }
            public static Func<ICustomMotion, float> LerpTo(float start, float origin, float scale)
            {
                float curPos = start;
                return (s) =>
                {
                    curPos = MathHelper.Lerp(curPos, origin, scale);
                    return curPos;
                };
            }
            public static Func<ICustomMotion, float> Alternate(float time, params Func<ICustomMotion, float>[] easings)
            {
                int curPhase = 0;
                float timer = 0;
                time *= 2;
                return (s) =>
                {
                    timer++;
                    if (timer >= time)
                    {
                        timer -= time;
                        curPhase++;
                    }
                    if (curPhase >= easings.Length)
                        curPhase = 0;
                    return easings[curPhase].Invoke(s);
                };
            }
            public static Func<ICustomMotion, float> Alternate(params Func<ICustomMotion, float>[] easings)
            {
                return Alternate(1, easings);
            }

            public static Func<ICustomMotion, float> Combine(Func<ICustomMotion, float> ease1, Func<ICustomMotion, float> ease2) =>
                (s) =>
                {
                    return ease1.Invoke(s) + ease2.Invoke(s);
                };
            public static Func<ICustomMotion, float> Combine(Func<ICustomMotion, float> ease1, float basis) =>
                (s) =>
                {
                    return ease1.Invoke(s) + basis;
                };

            /// <summary>
            /// 模拟缓动物件，模拟值从Rotation提取
            /// </summary>
            class VirtualEasingObject : GameObject, ICustomMotion
            {
                public VirtualEasingObject(ICustomMotion copy)
                {
                    PositionRoute = copy.PositionRoute;
                    RotationRoute = copy.RotationRoute;
                    AppearTime = copy.AppearTime;
                    CentrePosition = copy.CentrePosition;
                    PositionRouteParam = copy.PositionRouteParam;
                    RotationRouteParam = copy.RotationRouteParam;
                    UpdateIn120 = true;
                }
                public VirtualEasingObject()
                {
                    UpdateIn120 = true;
                    AppearTime = 0;
                }
                public static VirtualEasingObject ZeroTimeObj { get; }
                static VirtualEasingObject()
                {
                    ZeroTimeObj = new VirtualEasingObject();
                }
                public Func<ICustomMotion, Vector2> PositionRoute { get; set; }
                public Func<ICustomMotion, float> RotationRoute { get; set; }
                public float[] RotationRouteParam { get; set; }
                public float[] PositionRouteParam { get; set; }

                public float AppearTime { get; set; }

                public Vector2 CentrePosition { get; set; }
                public float Rotation { get; set; }

                public override void Update()
                {
                    AppearTime += 0.5f;
                    Rotation = RotationRoute.Invoke(this);
                }
            }
            public class EaseBuilder
            {
                public float OffsetPosition { get; set; }
                public bool Adjust { get; set; } = true;
                List<Tuple<float, Func<ICustomMotion, float>>> motionPairs = new();
                public void Insert(float time, Func<ICustomMotion, float> function)
                {
                    motionPairs.Add(new(time, function));
                }
                public static implicit operator Func<ICustomMotion, float>(EaseBuilder val)
                {
                    return val.GetResult();
                }
                public void Run(Action<float> action)
                {
                    VirtualEasingObject easer = new VirtualEasingObject();
                    AddInstance(easer);
                    easer.RotationRoute = GetResult();
                    AddInstance(new TimeRangedEvent(_totalTime, () =>
                    {
                        action.Invoke(easer.Rotation);
                    })
                    { UpdateIn120 = true }); ;
                    AddInstance(new InstantEvent(_totalTime, () => easer.Dispose()));
                }
                private float _totalTime;
                public Func<ICustomMotion, float> GetResult()
                {
                    VirtualEasingObject obj = new();
                    VirtualEasingObject objEnd = new();

                    Tuple<float, Func<ICustomMotion, float>>[] pairs = motionPairs.ToArray();
                    float[] timeZone = new float[pairs.Length];
                    float totalTime = 0;
                    float[] startings = new float[pairs.Length];
                    float[] endings = new float[pairs.Length];
                    for (int i = 0; i < pairs.Length; i++)
                    {
                        totalTime += pairs[i].Item1;
                        timeZone[i] = totalTime;
                        if (Adjust)
                            startings[i] = pairs[i].Item2.Invoke(obj);
                        objEnd.AppearTime = pairs[i].Item1;
                        endings[i] = pairs[i].Item2.Invoke(objEnd);
                    }
                    int curPhase = 0;
                    float basis = 0;
                    startings[0] = 0;
                    _totalTime = totalTime;

                    return !Adjust
                        ? ((s) =>
                        {
                            obj.AppearTime += 0.5f;
                            if (curPhase >= pairs.Length) return s.Rotation;
                            while (s.AppearTime >= timeZone[curPhase])
                            {
                                curPhase++;
                                obj.AppearTime = 0.5f;
                                if (curPhase >= pairs.Length) return s.Rotation;
                            }
                            return pairs[curPhase].Item2.Invoke(obj) + OffsetPosition;
                        })
                        : ((s) =>
                    {
                        obj.AppearTime += 0.5f;
                        if (s.AppearTime <= 0.5f)
                        {
                            basis = 0;
                            curPhase = 0;
                        }
                        if (curPhase >= pairs.Length) return basis;
                        while (s.AppearTime >= timeZone[curPhase])
                        {
                            basis = endings[curPhase] + basis - startings[curPhase];
                            curPhase++;
                            obj.AppearTime = 0.5f;
                            if (curPhase >= pairs.Length) return basis;
                        }
                        return pairs[curPhase].Item2.Invoke(obj) + basis - startings[curPhase] + OffsetPosition;
                    });
                }
                public void Stable(float time, float val)
                {
                    Insert(time, ValueEasing.Stable(val));
                }
            }
        }
    }
}