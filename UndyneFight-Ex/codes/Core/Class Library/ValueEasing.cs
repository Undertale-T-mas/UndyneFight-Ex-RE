using static System.MathF;

namespace UndyneFight_Ex
{
    //因为不会用缓动库所以就自己弄了个（
    public static class TKValueEasing
    {
        #region Normal Easing Functions
        //Linear Easing
        public static float EaseLinear(float _time, float _start, float _change, float _duration)
        {
            return (_change * _time / _duration) + _start;
        }
        public static float EaseLinear(int _time, int _start, int _change, int _duration)
        {
            return (_change * _time / _duration) + _start;
        }

        ///Quadratic Easing
        public static float EaseInQuad(float _time, float _start, float _change, float _duration)
        {
            return (-_change * Pow(_time / _duration, 2)) + _start;
        }
        public static float EaseOutQuad(float _time, float _start, float _change, float _duration)
        {
            return (_change * _time / _duration * ((_time / _duration) - 2)) + _start;
        }
        public static float EaseInOutQuad(float _time, float _start, float _change, float _duration)
        {
            _time = 2 * _time / _duration;
            _change *= 0.5f;
            return _time < 1 ? (_change * Pow(_time, 2)) + _start
                             : (-_change * (((_time - 1) * (_time - 3)) - 1)) + _start;
        }

        //Cubic Easing
        public static float EaseInCubic(float _time, float _start, float _change, float _duration)
        {
            return (_change * Pow(_time / _duration, 3)) + _start;
        }
        public static float EaseOutCubic(float _time, float _start, float _change, float _duration)
        {
            return (_change * (Pow((_time / _duration) - 1, 3) + 1)) + _start;
        }
        public static float EaseInOutCubic(float _time, float _start, float _change, float _duration)
        {
            _time = 2 * _time / _duration;
            _change *= 0.5f;
            return _time < 1 ? (_change * Pow(_time, 3)) + _start
                             : (_change * (Pow(_time - 2, 3) + 2)) + _start;
        }

        //Quart Easing
        public static float EaseInQuart(float _time, float _start, float _change, float _duration)
        {
            return (_change * Pow(_time / _duration, 4)) + _start;
        }
        public static float EaseOutQuart(float _time, float _start, float _change, float _duration)
        {
            return (_change * -(Pow((_time / _duration) - 1, 4) - 1)) + _start;
        }
        public static float EaseInOutQuart(float _time, float _start, float _change, float _duration)
        {
            _time = 2 * _time / _duration;
            _change *= 0.5f;
            return _time < 1 ? (_change * Pow(_time, 4)) + _start
                             : (-_change * (Pow(_time - 2, 4) - 2)) + _start;
        }

        //Quint Easing
        public static float EaseInQuint(float _time, float _start, float _change, float _duration)
        {
            return (_change * Pow(_time / _duration, 5)) + _start;
        }
        public static float EaseOutQuint(float _time, float _start, float _change, float _duration)
        {
            return (_change * -(Pow((_time / _duration) - 1, 5) - 1)) + _start;
        }
        public static float EaseInOutQuint(float _time, float _start, float _change, float _duration)
        {
            _time = 2 * _time / _duration;
            _change *= 0.5f;
            return _time < 1 ? (_change * Pow(_time, 5)) + _start
                             : (_change * (Pow(_time - 2, 5) + 2)) + _start;
        }

        //Sine
        public static float EaseInSine(float _time, float _start, float _change, float _duration)
        {
            return (_change * (1 - Cos(_time / _duration * (PI / 2f)))) + _start;
        }
        public static float EaseOutSine(float _time, float _start, float _change, float _duration)
        {
            return (_change * Sin(_time / _duration * (PI / 2f))) + _start;
        }
        public static float EaseInOutSine(float _time, float _start, float _change, float _duration)
        {
            return (_change * 0.5f * (1 - Cos(PI * _time / _duration))) + _start;
        }

        //Circ
        public static float EaseInCirc(float _time, float _start, float _change, float _duration)
        {
            return (_change * (1 - Sqrt(1 - (_time / _duration * _time / _duration)))) + _start;
        }
        public static float EaseOutCirc(float _time, float _start, float _change, float _duration)
        {
            _time = (_time / _duration) - 1;
            return (_change * Sqrt(Abs(1 - Pow(_time, 2)))) + _start;
        }
        public static float EaseInOutCirc(float _time, float _start, float _change, float _duration)
        {
            _time = 2 * _time / _duration;
            _change *= 0.5f;
            return _time < 1 ? (_change * (1 - Sqrt(Abs(1 - Pow(_time, 2))))) + _start
                             : (_change * (Sqrt(Abs(1 - ((_time - 2) * (_time - 2)))) + 1)) + _start;
        }

        //Expotential
        public static float EaseInExpo(float _time, float _start, float _change, float _duration)
        {
            return (_change * Pow(2, 10 * ((_time / _duration) - 1))) + _start;
        }
        public static float EaseOutExpo(float _time, float _start, float _change, float _duration)
        {
            return (_change * (-Pow(2, -10 * _time / _duration) + 1)) + _start;
        }
        public static float EaseInOutExpo(float _time, float _start, float _change, float _duration)
        {
            _time = 2 * _time / _duration;
            _change *= 0.5f;
            return _time < 1 ? (_change * Pow(2, 10 * (_time - 1))) + _start
                             : (_change * (-Pow(2, -10 * (_time - 1)) + 2)) + _start;
        }

        //Back
        public static float EaseInBack(float _time, float _start, float _change, float _duration)
        {
            _time /= _duration;
            _duration = 1.70158f; // Change '_duration' into Robert Penner's "s" value
            return (_change * Pow(_time, 2) * (((_duration + 1) * _time) - _duration)) + _start;
        }
        public static float EaseOutBack(float _time, float _start, float _change, float _duration)
        {
            _time = (_time / _duration) - 1;
            _duration = 1.70158f; // "s"
            return (_change * ((Pow(_time, 2) * (((_duration + 1) * _time) + _duration)) + 1)) + _start;
        }
        public static float EaseInOutBack(float _time, float _start, float _change, float _duration)
        {
            _time = _time / _duration * 2;
            _duration = 1.70158f; // "s"

            if (_time < 1)
            {
                _duration *= 1.525f;
                return (_change * 0.5f * ((((_duration + 1) * _time) - _duration) * Pow(_time, 2))) + _start;
            }

            _time -= 2;
            _duration *= 1.525f;

            return (_change * 0.5f * (((((_duration + 1) * _time) + _duration) * Pow(_time, 2)) + 2)) + _start;
        }

        //Bounce
        public static float EaseInBounce(float _time, float _start, float _change, float _duration)
        {
            return _change - EaseOutBounce(_duration - _time, 0, _change, _duration) + _start;
        }
        public static float EaseOutBounce(float _time, float _start, float _change, float _duration)
        {
            _time /= _duration;
            var c = 7.5625f;

            if (_time < 1 / 2.75f)
            {
                return (_change * c * _time * _time) + _start;
            }
            else
            if (_time < 2 / 2.75f)
            {
                _time -= 1.5f / 2.75f;
                return (_change * ((c * _time * _time) + 0.75f)) + _start;
            }
            else
            if (_time < 2.5f / 2.75f)
            {
                _time -= 2.25f / 2.75f;
                return (_change * ((c * _time * _time) + 0.9375f)) + _start;
            }
            else
            {
                _time -= 2.625f / 2.75f;
                return (_change * ((c * _time * _time) + 0.984375f)) + _start;
            }
        }
        public static float EaseInOutBounce(float _time, float _start, float _change, float _duration)
        {
            return _time < _duration * 0.5f ? (EaseInBounce(_time * 2, 0, _change, _duration) * 0.5f) + _start
                             : (EaseOutBounce((_time * 2) - _duration, 0, _change, _duration) * 0.5f) + (_change * 0.5f) + _start;
        }

        //Elastic
        public static float EaseInElastic(float _time, float _start, float _change, float _duration)
        {
            var _s = 1.70158f;
            var _p = _duration * 0.3f;
            var _a = _change;
            var _val = _time;

            if (_val == 0 || _a == 0) { return _start; }

            _val /= _duration;

            if (_val == 1) { return _start + _change; }

            if (_a < Abs(_change))
            {
                _a = _change;
                _s = _p * 0.25f;
            }
            else
            {
                _s = _p / (2 * PI) * Asin(_change / _a);
            }

            return -(_a * Pow(2, 10 * (_val - 1)) * Sin((((_val - 1) * _duration) - _s) * (2 * PI) / _p)) + _start;
        }
        public static float EaseOutElastic(float _time, float _start, float _change, float _duration)
        {
            var _s = 1.70158f;
            var _p = _duration * 0.3f;
            var _a = _change;
            var _val = _time;

            if (_val == 0 || _a == 0) { return _start; }

            _val /= _duration;

            if (_val == 1) { return _start + _change; }

            if (_a < Abs(_change))
            {
                _a = _change; // lol, wut?
                _s = _p * 0.25f;
            }
            else
            {
                _s = _p / (2 * PI) * Asin(_change / _a);
            }

            return (_a * Pow(2, -10 * _val) * Sin(((_val * _duration) - _s) * (2 * PI) / _p)) + _change + _start;
        }
        public static float EaseInOutElastic(float _time, float _start, float _change, float _duration)
        {
            var _s = 1.70158f;
            var _p = _duration * (0.3f * 1.5f);
            var _a = _change;
            var _val = _time;

            if (_val == 0 || _a == 0) { return _start; }

            _val /= _duration * 0.5f;

            if (_val == 2) { return _start + _change; }

            if (_a < Abs(_change))
            {
                _a = _change;
                _s = _p * 0.25f;
            }
            else
            {
                _s = _p / (2 * PI) * Asin(_change / _a);
            }

            return _val < 1
                ? (-0.5f * (_a * Pow(2, 10 * (_val - 1)) * Sin((((_val - 1) * _duration) - _s) * (2 * PI) / _p))) + _start
                : (_a * Pow(2, -10 * (_val - 1)) * Sin((((_val - 1) * _duration) - _s) * (2 * PI) / _p) * 0.5f) + _change + _start;
        }
        #endregion

    }
}
