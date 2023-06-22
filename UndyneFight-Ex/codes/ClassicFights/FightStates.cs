using System.Collections.Generic;

namespace UndyneFight_Ex.Fight
{
    internal static class FightStates
    {
        /// <summary>
        /// 当前回合属于玩家还是属于敌人, false->敌人, true->玩家
        /// </summary>
        public static bool roundType = false;
        /// <summary>
        /// 是否完成选项。特别地，当它为true的时候心会自动隐藏
        /// </summary>
        public static bool finishSelecting = true;
        public static List<Enemy> enemys;
        public static List<Item> items;
        public static List<GameAction> actions;
        public static Enemy controllingEnemy;
        /// <summary>
        /// 在fight, act, item, mecry中的选择
        /// </summary>
        public static int firstDecision = 0, attackDelay = 45, actionDelay = 55;

        public static IClassicFight Fight => ClassicFightEnterance.currentFight;

        public static CollideRect UIBoxPosition { internal get; set; }

        public static TextPrinter boxMessage;
    }
}