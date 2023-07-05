using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static System.Math;
using static System.MathF;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Fight
{
    public static class AdvanceFunctions
    {
        public static class Interactive
        {
            public static void AddMissEvent(Action action)
            {
                StateShower.instance.MissAction += action;
            }
            public static void AddOkayEvent(Action action)
            {
                StateShower.instance.OkayAction += action;
            }
            public static void AddNiceEvent(Action action)
            {
                StateShower.instance.NiceAction += action;
            }
            public static void AddPerfectEvent(Action action)
            {
                StateShower.instance.PerfectAction += action;
            }
            public static void AddEndEvent(Action action)
            {
                StateShower.instance.EndAction += action;
            }
        }
        /// <summary>
        /// cos01(x)等于cos(x * PI)。也就是说，cos(0) = cos(1) = 0, cos(0.5) = 1
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Cos01(float v)
        {
            return MathF.Cos(v * MathF.PI);
        }
        /// <summary>
        /// sin01(x)等于sin(x * PI)。也就是说，sin(0) = sin(1) = 0, sin(0.5) = 1
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Sin01(float v)
        {
            return Sin(v * MathF.PI);
        }

        public static void PushScore(int score)
        {
            if (StateShower.instance == null)
            {
                if (score == 0 && Functions.PlayerInstance.hpControl.KR)
                {
                    Functions.PlayerInstance.hpControl.GiveKR(1);
                }
                return;
            }
            int actual = score;
            Player.Heart p = null;
            foreach (var v in Player.hearts) { if (v.Shields == null) continue; if (v.Shields.OverRotate) { p = v; break; } }
            if (p != null && actual >= 3)
            {
                actual = 2;
                p.Shields.Consume();
            }
            StateShower.instance.PushType(actual);
        }
        internal static void PushBonus(float bonus)
        {
            if (StateShower.instance == null)
            {
                return;
            }
            StateShower.instance.PushBonus((int)bonus);
        }
    }
    public static partial class Functions
    {
        /// <summary>
        /// 使用此静态类设定或读取灵魂的一些属性
        /// </summary>
        public static class HeartAttribute
        {
            /// <summary>
            /// 蓝色灵魂是否采用软降落模式（更丝滑但时间会待得更长）
            /// </summary>
            public static bool SoftFalling
            {
                set => Heart.SoftFalling = value; get => Heart.SoftFalling;
            }
            /// <summary>
            /// 是否箭头不会因为绿色灵魂的角度而跟着旋转
            /// </summary>
            public static bool ArrowFixed
            {
                set => Heart.FixArrow = value; get => Heart.FixArrow;
            }
            /// <summary>
            /// 读取以判断玩家当前是否满血
            /// </summary>
            public static bool IsFullHP => PlayerInstance.hpControl.HP == PlayerInstance.hpControl.maxHP;

            /// <summary>
            /// 玩家每次被攻击受到的伤害
            /// </summary>
            public static int DamageTaken
            {
                set => PlayerInstance.hpControl.DamageTaken = value; get => PlayerInstance.hpControl.DamageTaken;
            }
            /// <summary>
            /// 玩家凋零强度
            /// </summary>
            public static float BuffedLevel
            {
                set => PlayerInstance.hpControl.BuffedLevel = value;
                get => PlayerInstance.hpControl.BuffedLevel;
            }
            /// <summary>
            /// 紫魂模式中紫色线的数量
            /// </summary>
            public static int PurpleLineCount
            {
                set => Heart.PurpleLineCount = value;
            }
            /// <summary>
            /// 重力加速度(默认值9.8)
            /// </summary>
            public static float Gravity
            {
                set => Heart.Gravity = value;
            }
            /// <summary>
            /// 跳跃时获得的初速度(默认值6)
            /// </summary>
            public static float JumpSpeed
            {
                set => Heart.JumpSpeed = value;
            }
            /// <summary>
            /// 玩家跳跃次数限制(默认值2)
            /// </summary>
            public static int JumpTimeLimit
            {
                set => Heart.JumpTimeLimit = value;
            }
            /// <summary>
            /// 是否打开KR（中毒带紫血帧伤模式）(默认值false)
            /// </summary>
            public static bool KR
            {
                set => PlayerInstance.hpControl.KR = value;
                get => PlayerInstance.hpControl.KR;
            }
            /// <summary>
            /// KR受到的中毒伤害(默认值4)
            /// </summary>
            public static float KRDamage
            {
                set => PlayerInstance.hpControl.KRDamage = value;
            }
            /// <summary>
            /// 玩家运动速度(默认值2.5)
            /// </summary>
            public static float Speed
            {
                set => Heart.Speed = value;
            }
            /// <summary>
            /// 可同时设置玩家的血量大小和血量上限。
            /// </summary>
            public static float MaxHP
            {
                set => PlayerInstance.hpControl.ResetMaxHP(value);
                get => PlayerInstance.hpControl.maxHP;
            }
            /// <summary>
            /// 玩家是否免疫物理伤害
            /// </summary>
            public static bool InvincibleToPhysics
            {
                set => PlayerInstance.hpControl.InvincibleToPhysic = value;
                get => PlayerInstance.hpControl.InvincibleToPhysic;
            }
            /// <summary>
            /// 可设置玩家此时的血量大小
            /// </summary>
            public static float HP
            {
                set => PlayerInstance.hpControl.HP = value;
                get => PlayerInstance.hpControl.HP;
            }
            /// <summary>
            /// 玩家是否开启滑翔伞（按空格缓降，默认不开启）
            /// </summary>
            public static bool UmbrellaAvailable
            {
                set => Heart.UmbrellaAvailable = value;
            }
            /// <summary>
            /// 玩家滑翔伞打开后降落速度(默认值1.0)
            /// </summary>
            public static float UmbrellaSpeed
            {
                set => Heart.UmbrellaSpeed = value;
            }
        }

        public static class BoxStates
        {
            public static FightBox CurrentBox => FightBox.instance;
            public static float Left => (FightBox.instance as RectangleBox).Left;
            public static float Right => (FightBox.instance as RectangleBox).Right;
            public static float Up { get => (FightBox.instance as RectangleBox).Up; set => (FightBox.instance as RectangleBox).Up = value; }
            public static float Down { get => (FightBox.instance as RectangleBox).Down; set => (FightBox.instance as RectangleBox).Down = value; }
            public static Vector2 Centre
            {
                get => (FightBox.instance as RectangleBox).Centre; set
                {
                    CollideRect area = (FightBox.instance as RectangleBox).CollidingBox;
                    area.SetCentre(value);
                    (FightBox.instance as RectangleBox).InstanceMove(area);
                }
            }
            public static Vector2 Location => (FightBox.instance as RectangleBox).CollidingBox.BottomLeft;
            public static float Width => (FightBox.instance as RectangleBox).CollidingBox.Width;
            public static float Height => (FightBox.instance as RectangleBox).CollidingBox.Height;
            /// <summary>
            /// 每次移动取的比例，一个 0-1 的浮点数。越大表示移动速度越快。0为静止不动，1为瞬间完成。默认0.15
            /// </summary>
            public static float BoxMovingScale { get => FightBox.instance.MovingScale; set => FightBox.instance.MovingScale = value; }
        }

        public static ContentManager Loader => Scene.Loader;

        public static Texture2D SongIllustration => (CurrentScene as SongFightingScene).SongIllustration;

        /// <summary>
        /// 歌曲播放结束是否自动切换到结算画面
        /// </summary>
        public static bool AutoEnd { set => (CurrentScene as SongFightingScene).AutoEnd = value; get => (CurrentScene as SongFightingScene).AutoEnd; }
        /// <summary>
        /// 歌曲播放起点时间
        /// </summary>
        public static float PlayOffset { set => (CurrentScene as SongFightingScene).PlayOffset = value; }
        /// <summary>
        /// 游戏时间位移
        /// </summary>
        public static float GametimeDelta = 0;
        /// <summary>
        /// 游戏时间
        /// </summary>
        public static int Gametime => (int)(GameMain.gameTime + GametimeDelta);
        /// <summary>
        /// 游戏时间(float)，可以看到半帧
        /// </summary>
        public static float GametimeF => GameMain.gameTime + GametimeDelta;

        public static Difficulty CurrentDifficulty => (CurrentScene as SongFightingScene).CurrentDifficulty;
        /// <summary>
        /// 你当前程序控制的玩家
        /// </summary>
        public static Player.Heart Heart => Player.heartInstance;
        /// <summary>
        /// 你当前程序控制的玩家
        /// </summary>
        public static Player PlayerInstance => (CurrentScene as FightScene).PlayerInstance;
        /// <summary>
        /// 设置你当前控制的玩家是几号玩家
        /// </summary>
        /// <param name="val"></param>
        public static void SetPlayerMission(int val)
        {
            if (Player.hearts.Count <= val) return;

            Player.heartInstance = Player.hearts[val];
        }
        /// <summary>
        /// 设置你当前控制的玩家
        /// </summary>
        /// <param name="val"></param>
        public static void SetPlayerMission(Player.Heart p)
        {
            Player.heartInstance = p;
        }
        /// <summary>
        /// 设置你当前控制的玩家和捕获框
        /// </summary>
        /// <param name="val"></param>
        public static void SetPlayerBoxMission(Player.Heart p)
        {
            Player.heartInstance = p;
            SetBoxMission(p.controlingBox);
        }
        /// <summary>
        /// 设置你当前控制的玩家和捕获框
        /// </summary>
        /// <param name="val"></param>
        public static void SetPlayerBoxMission(int val)
        {
            SetBoxMission(val);
            SetPlayerMission(val);
        }
        /// <summary>
        /// 设置你当前控制的框是几号框
        /// </summary>
        /// <param name="val">框的编号</param>
        public static void SetBoxMission(int val)
        {
            if (FightBox.boxs.Count <= val) return;

            FightBox.instance = FightBox.boxs[val];
        }
        /// <summary>
        /// 设置你当前控制的框 
        /// </summary>
        /// <param name="val">框的编号</param>
        public static void SetBoxMission(FightBox box)
        {
            FightBox.instance = box;
        }
        /// <summary>
        /// 把玩家移动到指定位置
        /// </summary>
        /// <param name="vect">指定的位置</param>
        public static void TP(Vector2 vect)
        {
            Player.heartInstance.Teleport(vect);
        }
        /// <summary>
        /// 把玩家移动到指定位置
        /// </summary>
        /// <param name="x">x位置</param>
        /// <param name="y">y位置</param>
        public static void TP(float x, float y)
        {
            Player.heartInstance.Teleport(new Vector2(x, y));
        }
        /// <summary>
        /// 把玩家瞬间传送到指定位置
        /// </summary>
        /// <param name="x">x位置</param>
        /// <param name="y">y位置</param>
        public static void InstantTP(Vector2 vec)
        {
            Player.heartInstance.InstantTP(vec);
        }
        /// <summary>
        /// 把玩家瞬间传送到指定位置
        /// </summary>
        /// <param name="x">x位置</param>
        /// <param name="y">y位置</param>
        public static void InstantTP(float x, float y)
        {
            Player.heartInstance.InstantTP(new Vector2(x, y));
        }
        /// <summary>
        /// 把玩家移动到屏幕中心
        /// </summary>
        public static void TP()
        {
            Player.heartInstance.Teleport(new Vector2(320, 240));
        }

        /// <summary>
        /// 恢复血量
        /// </summary>
        /// <param name="HP"></param>
        /// <returns></returns>
        public static void Regenerate(int HP)
        {
            PlayerInstance.hpControl.Regenerate(HP);
        }
        /// <summary>
        /// 恢复血量到满
        /// </summary>
        /// <returns></returns>
        public static void Regenerate()
        {
            PlayerInstance.hpControl.Regenerate();
        }
        public static void LoseHP(Player.Heart heart)
        {
            PlayerInstance.hpControl.LoseHP(heart);
        }
        public static void GiveKR(float scale)
        {
            PlayerInstance.hpControl.GiveKR(scale);
        }

        public static GameObject[] GetAll(string tag)
        {
            return (from x in Objects where x.ContainTag(tag) select x).ToArray();
        }
        public static T[] GetAll<T>(string tag) where T : GameObject
        {
            return (from x in Objects where x.ContainTag(tag) select x).OfType<T>().ToArray();
        }
        public static T[] GetAll<T>() where T : GameObject
        {
            return Objects.OfType<T>().ToArray();
        }

        /// <summary>
        /// 改变玩家灵魂状态。
        /// </summary>
        /// <param name="type">灵魂状态。0表示红，1表示绿，2表示蓝，3表示橙，4表示紫，5表示灰</param>
        public static void SetSoul(int type)
        {
            Player.heartInstance.ChangeColor(type);
        }
        /// <summary>
        /// 改变玩家灵魂状态。
        /// </summary> 
        public static void SetSoul(Player.MoveState state)
        {
            Player.heartInstance.ChangeState(state);
        }

        private static Arrow last;

        /// <summary>
        /// 箭头的属性标签
        /// </summary>
        [Flags]
        public enum ArrowAttribute
        {
            /// <summary>
            /// 无效果
            /// </summary>
            None = 0,
            /// <summary>
            /// 移动半程后加速
            /// </summary>
            SpeedUp = 1,
            /// <summary>
            /// 移动过程中顺时针旋转
            /// </summary>
            RotateR = 2,
            /// <summary>
            /// 移动过程中逆时针旋转
            /// </summary>
            RotateL = 4,
            /// <summary>
            /// 识别为Hold判定类型
            /// </summary>
            Hold = 8,
            /// <summary>
            /// 识别为Tap判定类型
            /// </summary>
            Tap = 16,
            /// <summary>
            /// 绘制为空心箭头
            /// </summary>
            Void = 32,
            /// <summary>
            /// 无得分
            /// </summary>
            NoScore = 64,
        }

        private static void GiveAttribute(Arrow arr, ArrowAttribute attribute)
        {
            if ((attribute & ArrowAttribute.SpeedUp) == ArrowAttribute.SpeedUp)
                arr.IsSpeedup = true;

            if ((attribute & ArrowAttribute.RotateR) == ArrowAttribute.RotateR)
                arr.IsRotate = true;

            if ((attribute & ArrowAttribute.RotateL) == ArrowAttribute.RotateL)
            {
                arr.IsRotate = true;
                arr.RotateScale = -1f;
            }

            if ((attribute & ArrowAttribute.Tap) == ArrowAttribute.Tap)
                arr.JudgeType = Arrow.JudgementType.Tap;
            if ((attribute & ArrowAttribute.Tap) == ArrowAttribute.Hold)
                arr.JudgeType = Arrow.JudgementType.Hold;
            if ((attribute & ArrowAttribute.Void) == ArrowAttribute.Void)
                arr.VoidMode = true;

            if ((attribute & ArrowAttribute.NoScore) == ArrowAttribute.NoScore)
                arr.NoScore = true;
        }

        /// <summary>
        /// 生成一个箭头
        /// </summary>
        /// <param name="shootShieldTime">射中盾牌的时间</param>
        /// <param name="way">射击方向，0右1下2左3上</param>
        /// <param name="speed">移动速度</param>
        /// <param name="color">颜色，0蓝1红</param>
        /// <param name="rotatingType">旋转模式，0正常，1黄，2绿</param>
        /// <param name="attribute">箭头的属性标签</param>
        public static void CreateArrow(float shootShieldTime, int way, float speed, int color, int rotatingType, ArrowAttribute attribute)
        {
            Arrow arr = new Arrow(Heart, shootShieldTime + GametimeF, way % 4, speed, color, rotatingType);
            last = arr;

            GiveAttribute(arr, attribute);

            GameStates.InstanceCreate(arr);
        }
        /// <summary>
        /// 获取一个箭头实例但不生成它
        /// </summary>
        /// <param name="shootShieldTime">射中盾牌的时间</param>
        /// <param name="way">射击方向，0右1下2左3上</param>
        /// <param name="speed">移动速度</param>
        /// <param name="color">颜色，0蓝1红</param>
        /// <param name="rotatingType">旋转模式，0正常，1黄，2绿</param>
        public static Arrow MakeArrow(float shootShieldTime, int way, float speed, int color, int rotatingType)
        {
            Arrow arr = new(Heart, shootShieldTime + GametimeF, (way + 4) % 4, speed, color, rotatingType);
            last = arr;
            return arr;
        }
        public static Arrow MakeArrow(float shootShieldTime, int way, float speed, int color, int rotatingType, ArrowAttribute attribute)
        {
            Arrow arr = new(Heart, shootShieldTime + GametimeF, (way + 4) % 4, speed, color, rotatingType);
            if ((attribute & ArrowAttribute.SpeedUp) == ArrowAttribute.SpeedUp)
                arr.IsSpeedup = true;

            GiveAttribute(arr, attribute);

            last = arr;
            return arr;
        }
        /// <summary>
        /// 生成一个箭头
        /// </summary>
        /// <param name="shootShieldTime">射中盾牌的时间</param>
        /// <param name="way">射击方向，0右1下2左3上</param>
        /// <param name="speed">移动速度</param>
        /// <param name="color">颜色，0蓝1红</param>
        /// <param name="rotatingType">旋转模式，0正常，1黄，2绿</param>
        public static void CreateArrow(float shootShieldTime, int way, float speed, int color, int rotatingType)
        {
            Arrow arr = new(Heart, shootShieldTime + GametimeF, (way + 64) % 4, speed, color, rotatingType);
            last = arr;
            InstanceCreate(arr);
        }
        private static int lastArrow;
        private static int[] colorLastArrow = new int[10];

        public static HashSet<char> OneElementArrows = new HashSet<char>();

        public static int GetWayFromTag(string wayTag)
        {
            int cur;
            int color = 0;
            if (wayTag.Length > 1)
            {
                if (OneElementArrows.Contains(wayTag[0]))
                {
                    if (wayTag.Length >= 2)
                    {
                        color = wayTag[1] == ' ' && wayTag.Length >= 3 ? MathUtil.Clamp(0, wayTag[2] - '0', 9) : MathUtil.Clamp(0, wayTag[1] - '0', 9);
                    }
                }
                else
                {
                    if (wayTag.Length >= 3)
                        color = MathUtil.Clamp(0, wayTag[2] - '0', 9);
                }
            }
            switch (wayTag[0])
            {
                case 'R':
                    lastArrow = Rand(0, 3);
                    return colorLastArrow[color] = lastArrow;
                case 'D':
                    cur = Rand(0, 3);
                    while (lastArrow == cur)
                        cur = Rand(0, 3);

                    lastArrow = cur;
                    return colorLastArrow[color] = lastArrow;
                case 'd':
                    cur = Rand(0, 3);
                    while (colorLastArrow[color] == cur)
                        cur = Rand(0, 3);

                    return colorLastArrow[color] = lastArrow = cur;
                case 'N':
                    int none = wayTag[1] - '0';
                    cur = Rand(0, 3);
                    while (cur == none)
                        cur = Rand(0, 3);
                    return colorLastArrow[color] = lastArrow = cur;
                case 'n':
                    int none2 = wayTag[1] - '0';
                    cur = Rand(0, 3);
                    while (cur == none2 || cur == colorLastArrow[color])
                        cur = Rand(0, 3);

                    return colorLastArrow[color] = lastArrow = cur;
                case '+':
                    lastArrow += (wayTag[1] - '0');
                    return colorLastArrow[color] = lastArrow;
                case '-':
                    lastArrow -= (wayTag[1] - '0');
                    return colorLastArrow[color] = lastArrow;
                case '$':
                    lastArrow = wayTag[1] - '0';
                    return colorLastArrow[color] = lastArrow;
                default:
                    lastArrow = wayTag[0] - '0';
                    return colorLastArrow[color] = lastArrow;
            }
        }
        /// <summary>
        /// 生成一个箭头，使用字符串方向标记。
        /// </summary>
        /// <param name="shootShieldTime">射中盾牌的时间</param>
        /// <param name="wayTag">射击方向的字符串表示</param>
        /// <param name="speed">移动速度</param>
        /// <param name="color">颜色，0蓝1红</param>
        /// <param name="rotatingType">旋转模式，0正常，1黄，2绿</param> 
        public static void CreateArrow(float shootShieldTime, string wayTag, float speed, int color, int rotatingType)
        {
            CreateArrow(shootShieldTime, GetWayFromTag(wayTag), speed, color, rotatingType);
        }
        /// <summary>
        /// 获得一个箭头实例但不生成它。使用字符串方向标记。
        /// </summary>
        /// <param name="shootShieldTime">射中盾牌的时间</param>
        /// <param name="wayTag">射击方向的字符串表示</param>
        /// <param name="speed">移动速度</param>
        /// <param name="color">颜色，0蓝1红</param>
        /// <param name="rotatingType">旋转模式，0正常，1黄，2绿</param> 
        public static Arrow MakeArrow(float shootShieldTime, string wayTag, float speed, int color, int rotatingType)
        {
            return MakeArrow(shootShieldTime, GetWayFromTag(wayTag), speed, color, rotatingType);
        }
        public static Arrow MakeArrow(float shootShieldTime, string wayTag, float speed, int color, int rotatingType, ArrowAttribute arrowattribute)
        {
            return MakeArrow(shootShieldTime, GetWayFromTag(wayTag), speed, color, rotatingType, arrowattribute);
        }
        /// <summary>
        /// 生成一个箭头，使用字符串方向标记。
        /// </summary>
        /// <param name="shootShieldTime">射中盾牌的时间</param>
        /// <param name="wayTag">射击方向的字符串表示</param>
        /// <param name="speed">移动速度</param>
        /// <param name="color">颜色，0蓝1红</param>
        /// <param name="rotatingType">旋转模式，0正常，1黄，2绿</param>
        /// <param name="attribute">箭头的属性标签</param>
        public static void CreateArrow(float shootShieldTime, string wayTag, float speed, int color, int rotatingType, ArrowAttribute attribute)
        {
            CreateArrow(shootShieldTime, GetWayFromTag(wayTag), speed, color, rotatingType, attribute);
        }
        /// <summary>
        /// 生成一个箭头
        /// </summary>
        /// <param name="shootShieldTime">射中盾牌的时间</param>
        /// <param name="way">射击方向，0右1下2左3上</param>
        /// <param name="speed">移动速度</param>
        /// <param name="color">颜色，0蓝1红</param>
        /// <param name="rotatingType">旋转模式，0正常，1黄，2绿</param>
        public static void CreateBoneArrow(float shootShieldTime, int way, float speed, int color, int rotatingType)
        {
            Arrow arr = new(Heart, shootShieldTime + Gametime, way % 4, speed, color, rotatingType);
            last = arr;
            InstanceCreate(arr);
        }

        /// <summary>
        /// 生成一支矛
        /// </summary>
        /// <param name="spear">那个矛</param>
        public static void CreateSpear(Spear spear)
        {
            InstanceCreate(spear);
        }
        /// <summary>
        /// 生成一个骨头
        /// </summary>
        /// <param name="bone">那个骨头</param>
        public static void CreateBone(Bone bone)
        {
            InstanceCreate(bone);
        }
        /// <summary>
        /// 生成GB炮
        /// </summary>
        /// <param name="gb">那个GB炮</param>
        public static void CreateGB(GasterBlaster gb)
        {
            InstanceCreate(gb);
        }
        /// <summary>
        /// 生成平台
        /// </summary>
        /// <param name="plt">那个平台</param>
        public static void CreatePlatform(Platform plt)
        {
            InstanceCreate(plt);
        }

        /// <summary>
        /// 生成任意实体
        /// </summary>
        /// <param name="et">那个实体</param>
        public static void CreateEntity(Entity et)
        {
            InstanceCreate(et);
        }
        public static void CreateEntity(params Entity[] et)
        {
            for (int a = 0; a < et.Length; a++)
            {
                int x = a;
                InstanceCreate(et[x]);
            }
        }
        /// <summary>
        /// 创建任意实例
        /// </summary>
        /// <param name="go"></param>
        public static void AddInstance(GameObject go)
        {
            InstanceCreate(go);
        }

        /// <summary>
        /// 将框平滑地移动到一个位置
        /// </summary>
        /// <param name="x1">框的左边</param>
        /// <param name="x2">框的右边</param>
        /// <param name="y1">框的上边</param>
        /// <param name="y2">框的下边</param>
        public static void SetBox(float x1, float x2, float y1, float y2)
        {
            if (FightBox.instance is RectangleBox)
                (FightBox.instance as RectangleBox).MoveTo(new CollideRect(x1, y1, x2 - x1, y2 - y1));
            else throw new NotImplementedException();
        }
        /// <summary>
        /// 将框平滑地移动到一个位置
        /// </summary> 
        public static void SetBox(Vector2 centre, float width, float height)
        {
            if (FightBox.instance is RectangleBox)
                (FightBox.instance as RectangleBox).MoveTo(new CollideRect(centre - new Vector2(width, height) / 2, new(width, height)));
            else throw new NotImplementedException();
        }
        /// <summary>
        /// 将框平滑地移动到一个位置
        /// </summary>
        /// <param name="YCentre"><那个位置的Y轴中心/param>
        /// <param name="width">框的宽度</param>
        /// <param name="height">宽的高度</param>
        public static void SetBox(float YCentre, float width, float height)
        {
            SetBox(320 - width / 2, 320 + width / 2, YCentre - height / 2, YCentre + height / 2);
        }

        /// <summary>
        /// 将框瞬间移动到一个位置
        /// </summary>
        /// <param name="x1">框的左边</param>
        /// <param name="x2">框的右边</param>
        /// <param name="y1">框的上边</param>
        /// <param name="y2">框的下边</param>
        public static void InstantSetBox(Vector2 centre, float width, float height)
        {
            if (FightBox.instance is RectangleBox)
                (FightBox.instance as RectangleBox).InstanceMove(new CollideRect(centre - new Vector2(width, height) / 2, new(width, height)));
            else throw new NotImplementedException();
        }
        /// <summary>
        /// 将框瞬间移动到一个位置
        /// </summary>
        /// <param name="x1">框的左边</param>
        /// <param name="x2">框的右边</param>
        /// <param name="y1">框的上边</param>
        /// <param name="y2">框的下边</param>
        public static void InstantSetBox(float x1, float x2, float y1, float y2)
        {
            if (FightBox.instance is RectangleBox)
                (FightBox.instance as RectangleBox).InstanceMove(new CollideRect(x1, y1, x2 - x1, y2 - y1));
            else throw new NotImplementedException();
        }
        /// <summary>
        /// 将框瞬间移动到一个位置
        /// </summary>
        /// <param name="YCentre"><那个位置的Y轴中心/param>
        /// <param name="width">框的宽度</param>
        /// <param name="height">宽的高度</param>
        public static void InstantSetBox(float YCentre, float width, float height)
        {
            InstantSetBox(320 - width / 2, 320 + width / 2, YCentre - height / 2, YCentre + height / 2);
        }

        /// <summary>
        /// 将框移动到绿魂模式中对应的位置
        /// </summary>
        public static void SetGreenBox()
        {
            SetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
        }
        /// <summary>
        /// 将框移动到绿魂模式中对应的位置
        /// </summary>
        public static void InstantSetGreenBox()
        {
            InstantSetBox(320 - 42, 320 + 42, 240 - 42, 240 + 42);
        }

        /// <summary>
        /// 执行times次操作
        /// </summary>
        /// <param name="times">操作次数</param>
        /// <param name="action">操作对应的程序</param>
        public static void Fortimes(int times, Action action)
        {
            for (int i = 1; i <= times; i++)
                action.Invoke();
        }

        /// <summary>
        /// 执行times次操作
        /// </summary>
        /// <param name="times">操作次数</param>
        /// <param name="action">操作对应的程序</param>
        public static void Fortimes(int times, Action<int> action)
        {
            for (int i = 0; i < times; i++)
                action.Invoke(i);
        }

        /// <summary>
        /// 生成一个随机数
        /// </summary>
        /// <param name="s">随机数的最小值</param>
        /// <param name="e">随机数的最大值</param>
        /// <returns>结果</returns>
        public static int Rand(int s, int e)
        {
            return LastRand = GetRandom(s, e);
        }
        /// <summary>
        /// 生成一个随机小数，
        /// </summary>
        /// <param name="s">随机数的最小值</param>
        /// <param name="e">随机数的最大值</param>
        /// <returns>结果</returns>
        public static float Rand(float s, float e)
        {
            return LastRandFloat = GetRandom(s, e);
        }
        public static int RandSignal()
        {
            return LastRand = (GetRandom(0, 1) == 0 ? 1 : -1);
        }
        public static bool RandBool()
        {
            LastRand = GetRandom(0, 1);
            return LastRand == 0;
        }
        public static int BoolToInt(bool b)
        {
            return b ? 1 : 0;
        }

        /// <summary>
        /// 给定角度求其正切值
        /// </summary>
        /// <param name="rot">角度（不是弧度！）</param>
        /// <returns></returns>
        public static float Tan(float rot)
        {
            return MathF.Tan(GetRadian(rot));
        }
        /// <summary>
        /// 给定角度求其余弦值
        /// </summary>
        /// <param name="rot">角度（不是弧度！）</param>
        /// <returns></returns>
        public static float Cos(float rot)
        {
            return MathF.Cos(GetRadian(rot));
        }
        /// <summary>
        /// 给定角度求其正弦值
        /// </summary>
        /// <param name="rot">角度（不是弧度！）</param>
        /// <returns></returns>
        public static float Sin(float rot)
        {
            return MathF.Sin(GetRadian(rot));
        }

        /// <summary>
        /// 上一次你用Rand生成的随机数
        /// </summary>
        public static int LastRand { get; set; }
        /// <summary>
        /// 上一次你用Rand生成的随机小数
        /// </summary>
        public static float LastRandFloat { get; set; }

        /// <summary>
        /// 播放一个声音
        /// </summary>
        /// <param name="effect">那个音效</param>
        /// <param name="soundVolume">声音大小，应该在0-1之间</param>
        public static void PlaySound(Microsoft.Xna.Framework.Audio.SoundEffect effect, float soundVolume)
        {
            float trueVal = soundVolume * soundVolume;
            var v = effect.CreateInstance();
            v.Volume = trueVal;
            v.Play();
        }
        /// <summary>
        /// 播放一个声音
        /// </summary>
        /// <param name="effect">那个音效</param>
        public static void PlaySound(Microsoft.Xna.Framework.Audio.SoundEffect effect)
        {
            effect.CreateInstance().Play();
        }

        /// <summary>
        /// 制造黑屏，持续一段时间用以切换场景
        /// </summary>
        /// <param name="time">黑屏时间</param>
        public static void BlackScreen(float time)
        {
            PlaySound(FightResources.Sounds.change);
            (CurrentScene as FightScene).stopTime += time;
        }

        /// <summary>
        /// 清除场面所有弹幕
        /// </summary>
        public static void ResetBarrage()
        {
            Objects.ForEach(s =>
            {
                if (s is Bone || s is Platform || s is Spear || s is NormalGB) { s.Kill(); }
            });
        }
        /// <summary>
        /// 立刻结束当前游戏
        /// </summary>
        public static void EndSong()
        {
            (CurrentScene as SongFightingScene).ForceEnd();
        }
        public static Player.Heart CreateHeart(CollideRect startingBoxPos)
        {
            return Heart.InstantSplit(startingBoxPos);
        }
        public static Player.Heart CreateHeart(float yCentre, float width, float height)
        {
            CollideRect rect = new(new Vector2(320 - width / 2, yCentre - height / 2), new(width, height));
            return Heart.InstantSplit(rect); 
        }
    }
}