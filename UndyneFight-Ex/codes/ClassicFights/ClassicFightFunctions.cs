using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight.InterActive;
using static UndyneFight_Ex.Fight.FightSelectionCollection;
using static UndyneFight_Ex.Fight.FightStates;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Fight
{
    public static class ClassicFight
    {
        /// <summary>
        /// 当前回合属于玩家还是属于敌人, false->敌人, true->玩家
        /// </summary>
        public static bool RoundType => FightStates.roundType;
        public static string PlayerName { set => NameShower.name = value; }
        public static int PlayerLevel { set => NameShower.level = value; }
        public static class PlayerImformation
        {
            public static string PlayerName { set => NameShower.name = value; }
            public static float AttackDamage { internal get; set; } = 50;
        }
        public static void StartBattle()
        {
            isInBattle = true;
        }
        public static void QuitFight()
        {
            ResetScene(new GameMenuScene());
            ResetFightState(true);
        }
        public static void CreateMenu()
        {
            _ = new Player();
            InstantSetBox(312, 589, 140);
            PlayerInstance.hpControl.ResetHP();
            PlayerInstance.hpControl.ResetKR();
            InstanceCreate(new HPShower() { Centre = new Vector2(350, 408) });
            InstanceCreate(new NameShower() { Centre = new Vector2(20, 409) });
            InstanceCreate
            (
                new FightSelectionCollection(
                    new FightSelection[]
                    {
                        new MenuButtons.FightButton(), new MenuButtons.ActButton(),
                        new MenuButtons.ItemButton(), new MenuButtons.MercyButton()
                    }
                )
                { IsCirclulateSelection = true }
            );
        }
        public static void ChangeRound()
        {
            PlayerInstance.hpControl.GiveProtectTime(15);
            if (roundType)
            {
                FightStates.Fight.RoundChanged();
                boxMessage?.Dispose();
            }
            else
            {
                (FightBox.instance as RectangleBox).MoveTo(UIBoxPosition);
            }

            finishSelecting = true;
            roundType = !roundType;

            if (!isInBattle)
            {
                var v = decisionTree.Peek();
                v.Reverse();
                if (roundType)
                {
                    v.Enabled = true;
                    v.Teleport();
                }
                v.UpdateEnabled = true;
            }
        }
        public static void CreateItem(Item i)
        {
            items.Add(i);
        }
        public static void CreateAction(GameAction i)
        {
            actions.Add(i);
        }
        public static void CreateEnemy(Enemy e)
        {
            InstanceCreate(e);
            enemys.Add(e);
        }
        public static void EndSelecting()
        {
            while (decisionTree.Count > 1)
            {
                decisionTree.Peek().Dispose();
                decisionTree.Pop();
            }
            var v = decisionTree.Peek();
            v.Enabled = false;
            v.UpdateEnabled = false;
            finishSelecting = false;
            //GameStates.InstanceCreate(v);
        }
        internal static void DoingAction()
        {
            if (EventAfterAction != null)
                EventAfterAction.Invoke();
            else ChangeRound();
        }

        internal static void ResetMessage()
        {
            boxMessage = !string.IsNullOrEmpty(MainMessage) && MainMessage[0] == '$'
                ? new TextPrinter("$* " + MainMessage[1..],
                    new Vector2(50, 260), MainMessageAttributes)
                : new TextPrinter("* " + MainMessage,
                    new Vector2(50, 260), MainMessageAttributes);
            InstanceCreate(boxMessage);
        }

        public static class InterActive
        {
            private static string mainMessage;

            internal static void Reset()
            {
                UIAlpha = 1.0f;
                AttackAnimation = null;
                EventAfterAction = null;
                MainMessage = "";
                NoDamageMessage = "MISS";
                DamageMessageColor = Color.White;
            }
            /// <summary>
            /// 没有对敌人造成伤害产生的信息
            /// </summary>
            public static string NoDamageMessage { internal get; set; } = "miss";

            /// <summary>
            /// 主信息的效果增加
            /// </summary>
            public static TextAttribute[] MainMessageAttributes { get; set; }
            /// <summary>
            /// 当玩家在菜单栏选择的时候框内的信息。请最好先填写MainMessageAttributes项，否则有一定可能会出现错误。
            /// </summary>
            public static string MainMessage
            {
                internal get => mainMessage;
                set
                {
                    mainMessage = value;
                    if (decisionTree == null || decisionTree.Peek() == MainMenu)
                    {
                        if (roundType == false) return;
                        boxMessage?.Dispose();
                        if (decisionTree.Peek() != MainMenu) return;
                        if (!finishSelecting) return;
                        ResetMessage();
                    }
                }
            }
            /// <summary>
            /// 攻击动效。传递一个类型反射
            /// </summary>
            public static Type AttackAnimation { internal get; set; }
            /// <summary>
            /// 完成所有选项选择和选项选择后事件 即将进入敌人回合时 所发生的事件
            /// </summary>
            public static Action EventAfterAction { internal get; set; }
            /// <summary>
            /// 敌人被击中时，显示伤害的颜色
            /// </summary>
            public static Color DamageMessageColor { internal get; set; } = Color.White;
            /// <summary>
            /// UI界面的alpha值。血条不考虑在内。
            /// </summary>
            public static float UIAlpha { get; set; } = 1.0f;
        }
        public static class Menu
        {
            public static MenuButtons.FightButton Fight => MenuButtons.FightButton.instance;
            public static MenuButtons.ActButton Act => MenuButtons.ActButton.instance;
            public static MenuButtons.ItemButton Item => MenuButtons.ItemButton.instance;
            public static MenuButtons.MercyButton Mercy => MenuButtons.MercyButton.instance;

            public static Vector2 NameShowerPosition { get => NameShower.instance.Centre; set => NameShower.instance.Centre = value; }
            public static Vector2 HPShowerPosition { get => HPShower.instance.Centre; set => HPShower.instance.Centre = value; }
        }
    }
}