using Microsoft.Xna.Framework;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities.Advanced
{
    public class ScreenShaker : GameObject
    {
        internal static Vector2 ScreenShakeDetla { get => CurrentSetting.shakings; set => CurrentSetting.shakings = value; }
        Vector2[] shakeVector;
        public ScreenShaker(int shakeCount, float shakeIndensity, float shakeDelay)
        {
            float curAngle = GetRandom(0, 359);
            shakeVector = new Vector2[shakeCount + 1];
            for (int i = 0; i < shakeCount; i++)
            {
                shakeVector[i] = GetVector2(shakeIndensity, curAngle += GetRandom(120, 240));
                shakeIndensity *= 0.85f;
            }
            shakeVector[shakeCount] = Vector2.Zero;
            this.shakeDelay = shakeDelay;
            UpdateIn120 = true;
        }
        /// <summary>
        /// 创建一个屏幕振荡器
        /// </summary>
        /// <param name="shakeCount">震动次数</param>
        /// <param name="shakeIndensity">震动强度</param>
        /// <param name="shakeDelay">震动之间间隔</param>
        /// <param name="startAngle">初始震动方向角度（非弧度！）</param>
        public ScreenShaker(int shakeCount, float shakeIndensity, float shakeDelay, float startAngle)
        {
            UpdateIn120 = true;
            float curAngle = startAngle;
            shakeVector = new Vector2[shakeCount + 1];
            for (int i = 0; i < shakeCount; i++)
            {
                shakeVector[i] = GetVector2(shakeIndensity, curAngle);
                curAngle += GetRandom(120, 240);
                shakeIndensity *= 0.85f;
            }
            shakeVector[shakeCount] = Vector2.Zero;
            this.shakeDelay = shakeDelay;
        }
        /// <summary>
        /// 创建一个屏幕振荡器
        /// </summary>
        /// <param name="shakeCount">震动次数</param>
        /// <param name="shakeIndensity">震动强度</param>
        /// <param name="shakeDelay">震动之间间隔</param>
        /// <param name="startAngle">初始震动方向角度（非弧度！）</param>
        public ScreenShaker(int shakeCount, float shakeIndensity, float shakeDelay, float startAngle, float angleDelta, float shakeFriction = 0.85f)
        {
            UpdateIn120 = true;
            float curAngle = startAngle;
            shakeVector = new Vector2[shakeCount + 1];
            for (int i = 0; i < shakeCount; i++)
            {
                shakeVector[i] = GetVector2(shakeIndensity, curAngle);
                curAngle += angleDelta;
                shakeIndensity *= shakeFriction;
            }
            shakeVector[shakeCount] = Vector2.Zero;
            this.shakeDelay = shakeDelay;
        }
        float appearTime = 0, shakeDelay;
        int _index = 0;
        public override void Update()
        {
            if (appearTime >= shakeDelay)
            {
                appearTime -= shakeDelay;
                _index++;
                if (_index >= shakeVector.Length)
                {
                    Dispose();
                    return;
                }
            }
            float movePercent = (1f / shakeDelay) * 0.4f + 1f * 0.6f;
            movePercent *= 0.7f;
            ScreenShakeDetla = ScreenShakeDetla * (1 - movePercent) + shakeVector[_index] * movePercent;
            appearTime += 0.5f;
        }
        public override void Dispose()
        {
            ScreenShakeDetla = Vector2.Zero;
            base.Dispose();
        }
    }
}
