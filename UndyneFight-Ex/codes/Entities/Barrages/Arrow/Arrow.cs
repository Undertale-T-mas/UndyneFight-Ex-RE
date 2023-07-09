using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.Settings.SettingsManager.DataLibrary;

namespace UndyneFight_Ex.Entities
{
    public partial class Arrow : Entity, IComparable
    {
        private const float speedUpPlace = 104;
        float DrawingScale => GoldenMarkIntensity * 0.1f + 1;
        public int RotateType => rotatingType;
        public bool VoidMode { get; set; } = false;

        public float VolumeFactor { get; internal set; } = 1.0f;

        public bool NoScore { get; set; } = false;
        private float settingDelay;
        /// <summary>
        /// 生成一个箭头
        /// </summary>
        /// <param name="shootShieldTime">射中盾牌的时间</param>
        /// <param name="way">射击方向，0右1下2左3上</param>
        /// <param name="speed">移动速度</param>
        /// <param name="color">颜色，0蓝1红2绿3紫</param>
        /// <param name="rotatingType">旋转模式，0正常，1黄，2绿</param>
        public Arrow(Player.Heart mission, float shootShieldTime, int way, float speed, int color, int rotatingType)
        {
            if (Mirror) color ^= 1;
            basicScale = ArrowScale;

            Init();
            Centre = new(-50000, -50000);
            UpdateIn120 = true;
            arrows.Add(this);

            this.shootShieldTime = shootShieldTime + (settingDelay = ArrowDelay / 16);
            Speed = speed * ArrowSpeed;
            this.way = way;
            ArrowColor = color;
            this.rotatingType = rotatingType % 2;
            backColor = rotatingType;
            hasGreenFlag = (rotatingType / 2) == 1;

            if (color > 3)
            {
                ;
            }

            missionRotation = this.way * 90f;
            this.mission = mission;
        }
        private readonly Player.Heart mission;

        public override void Start()
        {
            base.Start();
            if (!HasTag()) return;
            string[] strs = Tags;
            foreach (string str in strs)
            {
                if (!taggedArrows.ContainsKey(str)) taggedArrows.Add(str, new());
                taggedArrows[str].Add(this);
            }
        }

        /// <summary>
        /// 距离玩家灵魂的距离
        /// </summary>
        private float distance;
        private readonly bool hasGreenFlag;
        private readonly float basicScale;
        public int ArrowColor { get; private set; }
        private int backColor;
        private readonly int rotatingType, way;
        public int Way => way;
        private float shootShieldTime;
        public float CentreRotationOffset { get; set; } = 0;
        public float SelfRotationOffset { get; set; } = 0;
        public float Speed { private get; set; }
        public float Alpha { private get; set; } = 1;
        private readonly float missionRotation;
        private bool isSpeedUp = false, isRotate = false;
        private float TimeDelta => shootShieldTime - GametimeF;

        internal bool IsSpeedup { set => isSpeedUp = value; }
        internal bool IsRotate { set => isRotate = value; }
        internal float RotateScale { get; set; } = 1.0f;
        internal int GoldenMarkIntensity { private get; set; }

        public override void Draw()
        {
            //Tap -> Green outline, else no outline
            if ((JudgeType != JudgementType.Tap && !taggedArrows.ContainsKey("Tap")) && backColor == 2)
                backColor = 0;
            //if (!VoidMode) Image = Sprites.arrow[ArrowColor, backColor, 0];
            //if (VoidMode) Image = Sprites.voidarrow[ArrowColor];
            Image = VoidMode ? Sprites.voidarrow[ArrowColor] : Sprites.arrow[ArrowColor, backColor, 0];
            Depth = 0.5f - ArrowColor / 200f;
            FormalDraw(Image, Centre, new Color(0.98f, 0.98f, 0.98f, ArrowColor == 1 ? 0.75f : 0.25f) * Alpha, DrawingScale * Scale, GetRadian(Rotation + additiveRotation + SelfRotationOffset), ImageCentre);

            if (GoldenMarkIntensity > 0)
            {
                Depth += 0.02f;
                Texture2D tex = Sprites.goldenBrim;
                Vector2 pos = Centre;
                FormalDraw(tex, pos, Color.White * 0.5f * Alpha, DrawingScale * Scale, GetRadian(Rotation + additiveRotation + SelfRotationOffset), new Vector2(tex.Width / 2f, tex.Height / 2f));
            }
        }
        public override void Update()
        {
            Vector4 extend = CurrentScene.CurrentDrawingSettings.Extending;
            float max = MathF.Max(MathF.Max(extend.X, extend.Y), MathF.Max(extend.Z, extend.W));
            if ((Speed) * (shootShieldTime - GametimeF) > 640 * (max + 1.1f)) return;
            PositionCalculate();
            AppearTime += 0.5f;
            if (shootShieldTime - GametimeF < 20)
                CheckCollide();
        }

        public void ResetColor(int color)
        {
            ArrowColor = color;
            Image = Sprites.arrow[color, rotatingType, 0];
            var v = CreateShinyEffect(Color.White);
            v.DarkerSpeed = 6.4f;
        }

        public int CompareTo(object obj)
        {
            int res = shootShieldTime.CompareTo((obj as Arrow).shootShieldTime);
            if (MathF.Abs((obj as Arrow).shootShieldTime - shootShieldTime) < 0.35f) res = 0;
            return res != 0 ? res : way.CompareTo((obj as Arrow).way);
        }

        private static List<Arrow> arrows => (GameStates.CurrentScene as SongFightingScene).Accuracy.AllArrows;
        private static Dictionary<string, List<Arrow>> taggedArrows => (GameStates.CurrentScene as SongFightingScene).Accuracy.TaggedArrows;

        public float AppearTime { get; private set; } = 0;
    }
}