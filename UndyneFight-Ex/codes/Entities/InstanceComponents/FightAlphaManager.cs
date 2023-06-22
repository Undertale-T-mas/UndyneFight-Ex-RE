using System;
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Entities.Advanced
{
    /// <summary>
    /// 自动控制 ClassicFight.InterActive.UIAlpha的工具。
    /// </summary>
    public class FightAlphaManager : GameObject
    {
        private float mission;

        /// <summary>
        /// 期望的Alpha值。alpha值朝着期望丝滑过渡。
        /// </summary>
        /// <param name="mission"></param>
        public void SetMission(float mission)
        {
            this.mission = mission;
        }
        public override void Update()
        {
            float speed = 0.02f;
            float mission = FightStates.roundType ? 1 : this.mission;
            float alpha = ClassicFight.InterActive.UIAlpha;
            if (alpha < mission)
                alpha = MathF.Min(alpha + speed, mission);
            if (alpha > mission)
                alpha = MathF.Max(alpha - speed, mission);
            ClassicFight.InterActive.UIAlpha = alpha;
        }
    }
}
