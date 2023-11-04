using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using static UndyneFight_Ex.Fight.Functions;

namespace UndyneFight_Ex.SongSystem
{
    /// <summary>
    /// 一个战斗曲目
    /// </summary>
    public interface IWaveSet
    {
        /// <summary>
        /// 歌曲开始瞬间执行的任务
        /// </summary>
        void Start();

        /// <summary>
        /// noob难度每一帧执行的任务
        /// </summary>
        void Noob();
        /// <summary>
        /// easy难度每一帧执行的任务
        /// </summary>
        void Easy();
        /// <summary>
        /// normal难度每一帧执行的任务
        /// </summary>
        void Normal();
        /// <summary>
        /// hard难度每一帧执行的任务
        /// </summary>
        void Hard();
        /// <summary>
        /// extreme难度每一帧执行的任务
        /// </summary>
        void Extreme();
        /// <summary>
        /// 隐藏极限难度每一帧执行的任务
        /// </summary>
        void ExtremePlus();

        /// <summary>
        /// 歌曲音频文件名
        /// </summary>
        string Music { get; }

        /// <summary>
        /// 对歌曲在选择战斗页面中的名称的设置（影响存档）
        /// </summary>
        string FightName { get; }
        SongInformation Attributes { get; }
    }

    public interface IChampionShip
    {
        IWaveSet GameContent { get; }
        Dictionary<string, Difficulty> DifficultyPanel { get; }
    } 

    /// <summary>
    /// 曲目模板
    /// </summary>
    public class WaveConstructor : GameObject
    {
        private static float _singleBeat;
        /// <summary>
        /// 初始化一个战斗记帧器
        /// </summary>
        /// <param name="beatTime">一个节拍占据的帧数</param>
        public WaveConstructor(float beatTime)
        {
            _singleBeat = beatTime;
            SingleBeat = beatTime;
            DelayEnabled = true; 
        }
        /// <summary>
        /// 一个节拍占据的帧数
        /// </summary>
        public float SingleBeat;
        /// <summary>
        /// 获取x个节拍占据的帧数
        /// </summary>
        /// <param name="x">给出的x</param>
        /// <returns></returns>
        public float BeatTime(float x) => x * SingleBeat;
        /// <summary>
        /// 判断当前是否在第beat拍
        /// </summary>
        /// <param name="beat">给定的beat值</param>
        /// <returns></returns>
        public bool InBeat(float beat) => GametimeF >= BeatTime(beat) && GametimeF < BeatTime(beat) + 0.5f;
        /// <summary>
        /// 以beatCount拍为一个时长单位，判定当前帧数是否是在这个时长单位的第一帧(通常用来实现每隔几拍执行一次任务)
        /// </summary>
        /// <param name="beatCount">给定的beatCount</param>
        /// <returns></returns>
        public bool At0thBeat(float beatCount) => (int)(GametimeF % BeatTime(beatCount) * 2) == 0;
        /// <summary>
        /// 以beatCount拍为一个时长单位，判定当前帧数是否是在这个时长单位的第K+1帧(通常用来实现每隔几拍执行一次任务)
        /// </summary>
        /// <param name="beatCount">给定的beatCount</param>
        /// <param name="K">给定的K</param>
        /// <returns></returns>
        public bool AtKthBeat(float beatCount, float K) => (int)(GametimeF % BeatTime(beatCount) * 2) == (int)K * 2;
        /// <summary>
        /// 判断当前是否在第left拍到第right拍之间的时间
        /// </summary>
        /// <param name="leftBeat">给定的left</param>
        /// <param name="rightBeat">给定的right</param>
        /// <returns></returns>
        public bool InBeat(float leftBeat, float rightBeat) => GametimeF >= BeatTime(leftBeat) && GametimeF <= BeatTime(rightBeat) + 0.5f;

        public void DelayBeat(float delayBeat, Action action)
        {
            AddInstance(new InstantEvent(delayBeat * SingleBeat, action));
        }
        public void Delay(float delay, Action action)
        {
            AddInstance(new InstantEvent(delay, action));
        }
        /// <summary>
        /// trigger an action after beats you have given
        /// </summary>
        /// <param name="delayBeat">the time measured in beats</param>
        /// <param name="action">the action you want to trigger</param>
        public static void Trigger(float delayBeat, Action action)
        {
            AddInstance(new InstantEvent(delayBeat * _singleBeat, action));
        }
        public void ForBeat(float durationBeat, Action action)
        {
            AddInstance(new TimeRangedEvent(durationBeat * SingleBeat, action));
        }
        public void ForBeat120(float durationBeat, Action action)
        {
            AddInstance(new TimeRangedEvent(durationBeat * SingleBeat, action) { UpdateIn120 = true });
        }
        public void ForBeat(float delayBeat, float durationBeat, Action action)
        {
            AddInstance(new TimeRangedEvent(delayBeat * SingleBeat, durationBeat * SingleBeat, action));
        }
        public void ForBeat120(float delayBeat, float durationBeat, Action action)
        {
            AddInstance(new TimeRangedEvent(delayBeat * SingleBeat, durationBeat * SingleBeat, action) { UpdateIn120 = true });
        }
        public Action<Arrow> ArrowProcesser { private get; set; } = null;

        private class BracketTreeNode
        {
            public BracketTreeNode(string s)
            {
                string cur = "", self = "";
                BracketTreeNode curNode = null;
                int cnt = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '(')
                    {
                        if (cnt > 0)
                        {
                            cur += s[i];
                        }

                        cnt++;
                    }
                    else if (s[i] == ')')
                    {
                        cnt--;
                        if (cnt == 0)
                        {
                            sons.Add(curNode = new(cur));
                            cur = "";
                        }
                        else
                        {
                            cur += s[i];
                        }
                    }
                    else if (cnt == 0 && s[i] == '[')
                    {
                        i++;
                        string mul = "";
                        for (; s[i] != ']'; i++)
                        {
                            mul += s[i];
                        }
                        CalculateTimes(curNode, mul);
                    }
                    else
                    {
                        if (cnt == 0)
                        {
                            self += s[i];
                        }
                        else
                        {
                            cur += s[i];
                        }
                    }
                }
                info = self;
            }

            private static void CalculateTimes(BracketTreeNode curNode, string mul)
            {
                if (mul.Contains(':'))
                {
                    string[] splits = mul.Split(':');
                    curNode.enumer = splits[0];
                    mul = splits[1];
                }
                if (mul.Contains(".."))
                {
                    string[] splits = mul.Split("..");
                    curNode.boundL = TryPraseInt(curNode, splits[0]);
                    mul = splits[1];
                    curNode.boundR = 1;
                }
                int mulInt = TryPraseInt(curNode, mul);
                curNode.boundR += mulInt - 1;
            }

            private static int TryPraseInt(BracketTreeNode curNode, string mul)
            {
                int mulInt;
                if (int.TryParse(mul, out mulInt))
                {
                    if (curNode == null)
                    {
                        throw new ArgumentException(string.Format("[] must be placed after )"));
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format("{0} isn't a number in the []", mul));
                }

                return mulInt;
            }

            private readonly string info;
            private int boundR = 0;
            private int boundL = 0;
            private string enumer = "";
            private readonly List<BracketTreeNode> sons = new();
            public List<string> GetAll(Dictionary<string, int> enums)
            {
                List<string> res = new();
                // in the foreach, we recursively get the items in the subtrees.
                foreach (var son in sons)
                {
                    bool existEnumer;
                    if (existEnumer = !string.IsNullOrEmpty(son.enumer))
                    {
                        enums.Add(son.enumer, 0);
                    }
                    for (int i = son.boundL; i <= son.boundR; i++)
                    {
                        if (existEnumer)
                        {
                            enums[son.enumer] = i;
                        }

                        var single = son.GetAll(enums);
                        res.AddRange(single);
                    }
                    if (existEnumer)
                    {
                        enums.Remove(son.enumer);
                    }
                }

                //if info exists, then we push the info in current node into the results list.
                if (!string.IsNullOrEmpty(info))
                {
                    string cur = info;
                    foreach (var pair in enums)
                    {
                        cur = cur.Replace("*" + pair.Key, pair.Value.ToString());
                    }
                    res.Add(cur);
                }

                return res;
            }
        }
        public static string[] SplitBracket(string origin)
        {
            int cnt1 = origin.Count(s => s == '(');
            int cnt2 = origin.Count(s => s == ')');
            if (cnt1 != cnt2)
            {
                throw new ArgumentException($"{origin} isn't a legal bracket sequence");
            }

            BracketTreeNode root = new(origin);
            return root.GetAll(new()).ToArray();
        }
        private static string GenerateBracket(string[] arr)
        {
            if (arr.Length == 1)
            {
                return arr[0];
            }

            string s = "";
            for (int i = 0; i < arr.Length; i++)
            {
                s += "(" + arr[i] + ")";
            }
            return s;
        }

        private string[] ProduceTag(ref string origin)
        {
            if (origin[^1] != '}') return null;
            int tag = -1;
            for (int i = 0; i < origin.Length; i++) if (origin[i] == '{') tag = i;
            if (tag == -1) throw new ArgumentException($"{nameof(origin)} has no character '{{'", origin);

            string result = origin[(tag + 1)..^1];
            origin = origin[0..tag];

            return result.Split(',');
        }
        private IEnumerable<GameObject> MakeChartObject(float shootShieldTime, string origin, float speed, ArrowAttribute arrowAttribute, bool normalized)
        {
            if (origin == "/")
            {
                return null;
            }
            if(string.IsNullOrWhiteSpace(origin)) { return null; }
            string originCopy = origin;
            string[] entityTags = ProduceTag(ref origin);
            bool isFunction = false;
            GameObject[] results = TryGetObjects(origin, shootShieldTime, ref isFunction);
            if (isFunction) return results;

            int speedPos = -1;
            int tagPos = -1;
            bool multiTag = false;
            for (int i = origin.Length - 1; i >= 0; i--)
            {
                if (origin[i] == ',') multiTag = true;
                if (origin[i] == '\'')
                {
                    speedPos = i;
                }
                if (origin[i] == '@')
                {
                    tagPos = i;
                }
            }
            string tag = null;
            float speedMul = 1f;
            bool isvoid = false;
            if (speedPos != -1)
            {
                speedMul = tagPos > speedPos
                    ? MathUtil.FloatFromString(origin[(speedPos + 1)..tagPos])
                    : MathUtil.FloatFromString(origin[(speedPos + 1)..]);
            }
            if (tagPos != -1)
            {
                tag = speedPos > tagPos ? origin[(tagPos + 1)..speedPos] : origin[(tagPos + 1)..];
            }

            int cut1 = speedPos;
            if (cut1 == -1) cut1 = tagPos;
            else if (tagPos != -1) cut1 = Math.Min(cut1, tagPos);
            if (cut1 != -1)
                origin = origin[0..cut1];

            int curSpecialI = 0;
            if (origin[curSpecialI] == '~')
            {
                arrowAttribute |= ArrowAttribute.Void;
                curSpecialI++;
                isvoid = true;
            }
            if (origin[curSpecialI] == '*')
            {
                arrowAttribute |= ArrowAttribute.Tap;
                curSpecialI++;
                if (this.Settings.GreenTap)
                {
                    arrowAttribute |= ArrowAttribute.ForceGreen;
                }
            }
            else if (origin[curSpecialI] == '_')
            {
                arrowAttribute |= ArrowAttribute.Hold;
                curSpecialI++;
            }
            if (origin[curSpecialI] == '<')
            {
                arrowAttribute |= ArrowAttribute.RotateL;
                curSpecialI++;
            }
            else if (origin[curSpecialI] == '>')
            {
                arrowAttribute |= ArrowAttribute.RotateR;
                curSpecialI++;
            }
            if (origin[curSpecialI] == '^')
            {
                arrowAttribute |= ArrowAttribute.SpeedUp;
                curSpecialI++;
            }
            if (origin[curSpecialI] == '!')
            {
                arrowAttribute |= ArrowAttribute.NoScore;
                curSpecialI++;
            }
            origin = origin[curSpecialI..];
            bool GB = origin[0] == '#';
            string cut = "";
            if (GB)
            {
                int pos;
                cut = origin[(origin.IndexOf('#') + 1)..(pos = origin.LastIndexOf('#'))];
                origin = origin[(pos + 1)..];
            }
            if ((origin.Length == 1 || origin[1] != ' ') && (origin[0] == 'R' || origin[0] == 'D' || origin[0] == 'd'))
            {
                origin = origin[0] + " " + origin[1..];
            }
            if (origin.Length == 2)
            {
                origin += "00";
            }
            if (origin.Length == 3)
            {
                origin += "0";
            }
            Arrow arr = null;
            List<Entity> result = new();
            if (GB)
            {
                string way = origin[0..2];
                if (normalized) result.Add(arr = MakeArrow(shootShieldTime, way, speed * speedMul, origin[2] - '0', 0, arrowAttribute));
                result.Add(new GreenSoulGB(shootShieldTime, "+0", origin[2] - '0', BeatTime(MathUtil.FloatFromString(cut))) { AppearVolume = Settings.GBAppearVolume, ShootVolume = Settings.GBShootVolume, Follow = Settings.GBFollowing });
            }
            else result.Add(arr = MakeArrow(shootShieldTime, origin, speed * speedMul, origin[2] - '0', origin[3] - '0', arrowAttribute));

            if (!string.IsNullOrEmpty(tag))
            {
                if (!multiTag)
                    result.ForEach(s => s.Tags = new string[] { tag });
                else
                {
                    string[] tags = tag.Split(',');
                    result.ForEach(s => s.Tags = tags);
                }
            }
            if (arr != null)
            {
                if (entityTags != null)
                    arr.Tags = entityTags;
                if (arr.RotateType == -1)
                    ;
                if (isvoid) arr.VolumeFactor *= this.Settings.VoidArrowVolume;
                LastArrow = arr;

                if (ArrowProcesser != null) ArrowProcesser(arr);
            }
            return result;
        }

        private GameObject[] TryGetObjects(string origin, float delay, ref bool isFunction)
        {
            string args = "";
            bool delayMode = DelayEnabled;
            if (origin[0] == '<')
            {
                if (origin.Contains('>'))
                {
                    int i = 1;
                    if (origin[1] == '!')
                    {
                        i = 3;
                        delayMode = false;
                    }
                    if (origin[2] == '>' && origin[1] == '!') i = 2;
                    else
                        for (; origin[i] != '>'; i++)
                        {
                            args += origin[i];
                        }
                    origin = origin[(i + 1)..];
                }
                else
                {
                    isFunction = false;
                    return null;
                }
            }
            if (chartingActions.ContainsKey(origin))
            {
                isFunction = true;
                if (args != "")
                { 
                    string[] argStrings = args.Split(',');
                    float[] argsFloat = new float[argStrings.Length];
                    for(int i = 0; i < argsFloat.Length; i++) argsFloat[i] = MathUtil.FloatFromString(argStrings[i]);

                    if (delayMode)
                    {
                        Action action = chartingActions[origin];
                        GameObject[] list = { new InstantEvent(delay, () => {
                            Arguments = argsFloat;
                            action.Invoke(); 
                        }) };
                        return list;
                    }
                    else
                    {
                        Arguments = argsFloat;
                        chartingActions[origin].Invoke();
                        return null;
                    }
                }
                if (delayMode)
                {
                    GameObject[] list = { new InstantEvent(delay, chartingActions[origin]) };
                    return list;
                }
                else
                {
                    chartingActions[origin].Invoke();
                    return null;
                }
            }
            return null;
        }

        public class ChartSettings
        {
            public float GBAppearVolume = 0.5f;
            public float GBShootVolume = 0.75f;
            public float VoidArrowVolume = 0.5f;
            public bool GreenTap = false;

            public bool GBFollowing { get; set; } = false;
        }

        protected ChartSettings Settings { get; private set; } = new();

        public GameObject[] MakeArrows(float shootShieldTime, float speed, string allArrowTag, bool normalized = false)
        {
            string[] arrowTags = SplitBracket(allArrowTag);
            List<GameObject> arrows = new();

            for (int i = 0; i < arrowTags.Length; i++)
            {
                IEnumerable<GameObject> t = MakeChartObject(shootShieldTime, arrowTags[i], speed, ArrowAttribute.None, normalized);
                if (t != null)
                    arrows.AddRange(t);
            }
            return arrows.ToArray();
        }
        public GameObject[] MakeArrows(float shootShieldTime, float speed, string allArrowTag, ArrowAttribute arrowattribute, bool normalized = false)
        {
            string[] arrowTags = SplitBracket(allArrowTag);
            List<GameObject> arrows = new();

            for (int i = 0; i < arrowTags.Length; i++)
            {
                IEnumerable<GameObject> t = MakeChartObject(shootShieldTime, arrowTags[i], speed, arrowattribute, normalized);
                if (t != null)
                    arrows.AddRange(t);
            }
            return arrows.ToArray();
        }
        public void CreateArrows(float shootShieldTime, float speed, string allArrowTag)
        {
            GameObject[] arrows = MakeArrows(shootShieldTime, speed, allArrowTag);
            for (int i = 0; i < arrows.Length; i++)
            {
                if (arrows[i] != null)
                    AddInstance(arrows[i]);
            }
        }
        public void CreateArrows(float shootShieldTime, float speed, string allArrowTag, ArrowAttribute arrowAttribute)
        {
            GameObject[] arrows = MakeArrows(shootShieldTime, speed, allArrowTag, arrowAttribute);
            for (int i = 0; i < arrows.Length; i++)
            {
                if (arrows[i] != null)
                    AddInstance(arrows[i]);
            }
        }
        public void NormalizedChart(float shootShieldTime, float speed, string allArrowTag)
        {
            GameObject[] arrows = MakeArrows(shootShieldTime, speed, allArrowTag, true);
            for (int i = 0; i < arrows.Length; i++)
            {
                if (arrows[i] != null)
                    AddInstance(arrows[i]);
            }
        }
        public GameObject[] NormalizedObjects(float shootShieldTime, float speed, string allArrowTag)
        {
            GameObject[] arrows = MakeArrows(shootShieldTime, speed, allArrowTag, true);
            return arrows;
        }

        private Dictionary<string, Action> chartingActions = new();

        public void RegisterFunction(string name, Action action)
        {
            if (chartingActions.ContainsKey(name)) chartingActions[name] = action;
            else
                chartingActions.Add(name, action);
        }
        private List<string> removingActions = new();
        public void RegisterFunctionOnce(string name, Action action)
        {
            if (chartingActions.ContainsKey(name)) return;
            chartingActions.Add(name, action);
            removingActions.Add(name);
        }

        public void ArrowAllocate(int slot, int direction)
        {
            DirectionAllocate[slot] = direction;
        }

        protected static Arrow LastArrow { get; private set; }
        public static float CurrentTime { get; private set; } = 0;
        public static bool DelayEnabled { private get; set; } = true;

        public static float[] Temps { get; private set; } = new float[100];
        public static float[] Arguments { get; private set; } 
        /// <summary>
        /// 便携的谱面创建，"" 或者 "/" 是空拍，用法如下（神他妈复杂）（打*为可有可无）<br/>
        /// 箭头：<br/>
        /// *0.特性    "!"无分，   "^"加速，     ">"右旋转，     "*"Tap，   "~"Void，      "_"Hold<br/>
        /// 特性顺序：~*_>^!<br/>
        /// 1.方向    "R"随机，     "D"跟上次不一样，     "+/-x"上一个方向的 +/-x方向，   "$x"固定在x方向    "Nx"不为x的方向<br/>
        /// *2.颜色    "0"蓝，       "1"红<br/>
        /// *3.旋转    "0"无，       "1"黄矛，      "2"斜矛<br/>
        /// 组合：(R)(+0)才是两个叠在一起，R(+0)会无效<br/>
        /// GB：#xx#yz<br/>
        /// #xx#表示停留节拍，y表示方向（用法和箭头相同），z表示颜色<br/>
        /// 使用 'x 重置arrowspeed; 使用 << >> 调节 节拍时间</x><br/>
        /// 事件用RegisterFunction()或RegisterFunctionOnce()然后放进字符里面<br/>
        /// 比如RegisterFunctionOnce("func", ()=> {});<br/>
        /// "(func)(R)"，即会发动事件和做一根随机蓝矛<br/>
        /// "!!X*/Y"，即会在接下来的Y拍切成8 * X 分音符<br/>
        /// 事件还可以在前面加上"<参数,参数...>"的形式自定义参数，在写谱的时候可以加入</x><br/>
        /// 在事件里使用Arguments[x]参数，就同等于自定义参数，顺序为0123..如果使用了Arguments而不填会报错
        /// </summary>
        /// <param name="Delay">延迟时间，一般用来让箭头不闪现入场</param>
        /// <param name="Beat">拍号，如果写BeatTime(1)即为每个字符串占一个32分的长度</param>
        /// <param name="arrowspeed">普遍的箭头速度</param>
        /// <param name="Barrage">谱面内容，即字符串数组</param>
        public void CreateChart(float Delay, float Beat, float arrowspeed, string[] Barrage)
        {
            float t = Delay;
            int effectLast = 0;
            int currentCount = 4;
            for (int i = 0; i < Barrage.Length; i++)
            {
                string cur = Barrage[i];
                if (cur.Length > 2)
                {
                    //改变间隔
                    if (cur[0..2] == "!!")
                    {
                        string str = Barrage[i][2..];
                        int pos = -1;
                        for (int j = 0; j < str.Length; j++)
                        {
                            if (str[j] == '/') pos = j;
                        }
                        if (pos == -1)
                        {
                            int count = Convert.ToInt32(str);
                            currentCount = count;
                            effectLast = count;
                        }
                        else
                        {
                            int count = Convert.ToInt32(str[0..pos]);
                            currentCount = count;
                            effectLast = Convert.ToInt32(str[(pos + 1)..]);
                        }
                        continue;
                    }
                    else if (cur[0..2] == "''")
                    {
                        arrowspeed = MathUtil.FloatFromString(cur[2..]);
                        continue;
                    }
                    else if (cur[0..2] == "<<")
                    {
                        float p = MathUtil.FloatFromString(cur[2..]);
                        t -= BeatTime(p);
                        continue;
                    }
                    else if (cur[0..2] == ">>")
                    {
                        float p = MathUtil.FloatFromString(cur[2..]);
                        t += BeatTime(p);
                        continue;
                    }
                }
                CurrentTime = t;
                if (!string.IsNullOrWhiteSpace(cur))
                    NormalizedChart(t, arrowspeed, cur);
                t += Beat / (currentCount * 2f);
                if (effectLast > 0)
                    effectLast--;
                if (effectLast == 0)
                    currentCount = 4;
            }
        }
        public sealed override void Update()
        {
            removingActions.ForEach(s => chartingActions.Remove(s));
            removingActions.Clear();
        }
    }

    public abstract class SongInformation
    {
        public bool MusicOptimized { get; protected set; } = false;
        public virtual string DisplayName => "";
        public virtual string SongAuthor => "Unknown";
        public virtual string BarrageAuthor => "Unknown";
        public virtual string AttributeAuthor => "Unknown";
        public virtual string PaintAuthor => "Unknown";
        public virtual string Extra => "";
        public virtual Vector2 ExtraPosition => new(20, 50);
        public virtual Color ExtraColor => Color.White;

        public virtual bool Hidden => false;

        public virtual Dictionary<Difficulty, float> CompleteDifficulty => new();
        public virtual Dictionary<Difficulty, float> APDifficulty => new();
        public virtual Dictionary<Difficulty, float> ComplexDifficulty => new();

        public virtual HashSet<Difficulty> UnlockedDifficulties => new()
        { Difficulty.Noob, Difficulty.Easy, Difficulty.Normal, Difficulty.Hard, Difficulty.Extreme, Difficulty.ExtremePlus };
    }
}