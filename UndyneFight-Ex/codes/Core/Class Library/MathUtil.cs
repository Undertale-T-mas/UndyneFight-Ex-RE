using Microsoft.Xna.Framework;
using System;
using static System.Math;
using static System.MathF;

namespace UndyneFight_Ex
{
    public static class MathUtil
    {
        internal static Random rander = new Random();
        public static float PI = 3.141592f;

        public static string FloatToString(float val, int digits) => FloatToString(val, digits, false);
        public static string FloatToString(float val, int digits, bool isCheck)
        {
            string res = "";
            if (val < 0)
            {
                val = -val;
                res += "-";
            }
            int upper = (int)val;
            res += upper.ToString();
            val -= upper;
            if (digits > 0) res += ".";
            for (int i = 0; i < digits; i++)
            {
                val *= 10;
                upper = (int)val;
                res += upper.ToString();
                val -= upper;
            }
            return res;
        }
        /// <summary>
        /// 获取字符串表示的小数值
        /// </summary>
        /// <param name="str">该字符串</param>
        /// <returns>小数数值</returns>
        public static float FloatFromString(string str) => FloatFromString(str, false);
        /// <summary>
        /// 获取字符串表示的小数值
        /// </summary>
        /// <param name="str">该字符串</param>
        /// <returns>小数数值</returns>
        public static float FloatFromString(string str, bool isCheck)
        {
            char[] ch = str.ToCharArray();
            float intValue = 0, fractionValue = 0;
            float basis = 1.0f;
            bool intPart = true;
            int sign = 1;
            for (int i = 0; i < ch.Length; i++)
            {
                if (i == 0 && ch[i] == '-')
                {
                    sign = -1;
                    continue;
                }
                if (ch[i] == '.')
                {
                    if (!intPart)
                        return isCheck ? throw new ArgumentException($"The argument {str} has too many decimal points!", nameof(str)) : 0;
                    intPart = false;
                    continue;
                }
                if (ch[i] < '0' || ch[i] > '9')
                    return isCheck ? throw new ArgumentException($"{str} has some character which is not digit", nameof(str)) : 0;
                if (intPart)
                    intValue = intValue * 10 + (ch[i] - '0');
                else
                {
                    basis *= 10f;
                    fractionValue += (ch[i] - '0') / basis;
                }
            }
            return sign * (intValue + fractionValue);
        }
        /// <summary>
        /// 输入两个方向角，获取它们之间最小夹角。角度取值为[0, 360]
        /// </summary>
        /// <param name="rot1">第一个方向</param>
        /// <param name="rot2">第二个方向</param>
        /// <returns>两者夹角。不超过180度。</returns>
        public static float RotationDist(float rot1, float rot2)
        {
            return MathF.Min((rot1 + 360 - rot2) % 360, (rot2 + 360 - rot1) % 360);
        }
        public static Vector2 Transfer(Matrix matrix, Vector2 origin)
        {
            return new(origin.X * matrix.M11 + origin.Y * matrix.M21 + matrix.M41, origin.X * matrix.M12 + origin.Y * matrix.M22 + matrix.M42);
        }
        public static Vector2 Rotate(Vector2 origin, float rot)
        {
            float len = origin.Length(); float angle = origin.Direction();
            return GetVector2(len, angle + rot);
        }
        /// <summary>
        /// 输入两个夹角小于360°的角，返回一个绝对值小于180°角，使得第一个方向角加它为第二个方向角等效的方向。
        /// </summary>
        /// <param name="rot1">第一个方向</param>
        /// <param name="rot2">第二个方向</param>
        /// <returns>角度旋转。绝对值不超过180度。</returns>
        public static float MinRotate(float rot1, float rot2)
        {
            if (MathF.Abs(rot1 - rot2) <= 0.001f) return 0.0f;
            float a1 = (rot2 + 360 - rot1) % 360;
            return a1 <= 180 ? a1 : -((rot1 + 360 - rot2) % 360);
        }

        public static float SignedPow(float timeDel, float v)
        {
            return timeDel >= 0 ? Pow(timeDel, v) : -Pow(-timeDel, v);
        }

        /// <summary>
        /// 获取从start到end向量的角度(不是弧度)
        /// </summary>
        /// <param name="start">开始位置</param>
        /// <param name="end">终止位置</param>
        /// <returns>角度结果</returns>
        public static float Direction(Vector2 start, Vector2 end)
        {
            return Atan2((end - start).Y, (end - start).X) / MathF.PI * 180;
        }
        /// <summary>
        /// 获取一个向量在平面直角坐标系中的角度（不是弧度）
        /// </summary>
        /// <param name="vec">这个向量</param>
        /// <returns>所得角度</returns>
        public static float Direction(this Vector2 vec)
        {
            return Direction(Vector2.Zero, vec);
        }
        /// <summary>
        /// 即调整后的Tanh函数。用于丝滑过渡。val定义域为[0, 1]时，函数值域为[0, 1]
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float Sigmoid01(float val)
        {
            return (Tanh(val * 6 - 3) / 0.99505f + 1) / 2f;
        }
        /// <summary>
        /// 两向量的叉积 
        /// </summary>
        /// <param name="vec">第一个向量</param>
        /// <param name="vec2">第二个向量</param>
        /// <returns>叉积</returns>
        public static float Cross(this Vector2 vec, Vector2 vec2)
        {
            return vec.X * vec2.Y - vec.Y * vec2.X;
        }
        public static int Clamp(int min, int val, int max)
        {
            return val > max ? max : (val < min ? min : val);
        }
        public static float Clamp(float min, float val, float max)
        {
            return val > max ? max : (val < min ? min : val);
        }
        public static float GetRadian(float angle)
        {
            return (float)(angle / 180 * Math.PI);
        }
        public static float GetAngle(float angle)
        {
            return (float)(angle / Math.PI * 180);
        }
        /// <summary>
        /// 由角度和长度计算出向量
        /// </summary>
        /// <param name="length">给定的长度</param>
        /// <param name="angle">给定的角度(非弧度制)</param>
        /// <returns></returns>
        public static Vector2 GetVector2(float length, float angle)
        {
            return new Vector2((float)Math.Cos(GetRadian(angle)) * length, (float)Math.Sin(GetRadian(angle)) * length);
        }
        public static float GetDistance(Vector2 P1, Vector2 P2)
        {
            float dx = P1.X - P2.X;
            float dy = P1.Y - P2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
        public static int GetRandom(int x, int y)
        {
            return rander.Next(x, y + 1);
        }
        public static float GetRandom(float x, float y)
        {
            return MathHelper.LerpPrecise(x, y, (float)rander.NextDouble());
        }
        public static float Sqrt(float v)
        {
            float l = 0.001f, r = v, mid = -1;
            while (r - l > 1e-5)
            {
                mid = (l + r) / 2;
                if (mid * mid > v) r = mid;
                else l = mid;
            }
            return mid;
        }
        public static T RandIn<T>(params T[] inputs)
        {
            return inputs[GetRandom(0, inputs.Length - 1)];
        }
        /// <summary>
        /// 获取两向量夹角
        /// </summary>
        /// <param name="a">第一个向量</param>
        /// <param name="b">第二个向量</param>
        /// <returns>夹角</returns>
        internal static float Cos(Vector2 a, Vector2 b)
        {
            float v = Vector2.Dot(a, b) / (a.Length() * b.Length());
            return v;
        }

        public static uint StringHash(string s)
        {
            ulong partA = GetHashCode(s);
            ulong hashResult1 = QuickPow(97, partA);
            uint hashResult2 = Convert.ToUInt32(hashResult1 % (1u << 31));
            return hashResult2;
        }
        private static ulong GetHashCode(string s)
        {
            ulong res = 0;
            for (int i = 0; i < s.Length; i++) { res = res * 131 + Convert.ToUInt64((int)s[i]); }
            return res;
        }
        public static ulong QuickPow(ulong a, ulong b)
        {
            ulong ret = 1, pow = a;

            while (b != 0)
            {
                if ((b & 1) == 1) ret *= pow;
                pow *= pow;
                b /= 2;
            }
            return ret;
        }
        public static ulong QuickPow(ulong a, ulong b, ulong mod)
        {
            ulong ret = 1, pow = a;

            while (b != 0)
            {
                if ((b & 1) == 1) ret = pow * ret % mod;
                pow = pow * pow % mod;
                b /= 2;
            }
            return ret;
        }
        public static float Posmod(float a, float b)
        {
            var value = a % b;
            while ((value < 0 && b > 0) || (value > 0 && b < 0))
                value += b;
            return value;
        }
        public static int Posmod(int a, int b)
        {
            var value = a % b;
            while ((value < 0 && b > 0) || (value > 0 && b < 0))
                value += b;
            return value;
        }
    }
}