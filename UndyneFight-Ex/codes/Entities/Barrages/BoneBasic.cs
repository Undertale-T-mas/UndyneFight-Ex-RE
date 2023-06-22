using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public class Bone : Entity, ICollideAble
    {
        protected RectangleBox controlingBox;
        public RectangleBox ControlingBox => controlingBox;

        private int score = 3;
        protected float alpha = 0;
        private bool hasHit = false;
        public bool MarkScore { get; set; } = true;

        protected bool autoDispose = true;
        private bool hasBeenInside = false;
        public static CollideRect screen = new CollideRect(-50, -50, 740, 580);

        public float Length { get; set; }
        public bool IsMasked { get; set; } = true;

        protected Color drawingColor;

        public float Alpha { get => alpha; set => alpha = value; }

        private int colorType = 0;
        /// <summary>
        /// 设置骨头的颜色属性并且自动重置颜色。0->白，1->蓝，2->绿
        /// </summary>
        public float ColorType
        {
            set
            {
                switch (value)
                {
                    case 0: drawingColor = Color.White; colorType = 0; break;
                    case 1: drawingColor = new Color(110, 203, 255, 255); colorType = 1; break;
                    case 2: drawingColor = Color.Orange; colorType = 2; break;
                    default: throw new ArgumentOutOfRangeException("rvalue", value, "The rvalue can only be 0, 1 or 2");
                }
            }
        }

        public void ResetColor(Color color)
        {
            drawingColor = color;
        }

        public override void Draw()
        {
            if (Length < 0) return;
            Depth = 0.5f - colorType * 0.02f;

            var spb = GameMain.MissionSpriteBatch;
            CollideRect cl1 = new CollideRect(0, 0, 6, Length - 2);
            Vector2 detla = GetVector2(Length / 2, Rotation + 90);
            spb.Draw(Sprites.boneBody, Centre, cl1.ToRectangle(), drawingColor * alpha, GetRadian(Rotation), new Vector2(3, Length / 2), 1.0f, SpriteEffects.None, 0.399f);
            spb.Draw(Sprites.boneHead, Centre - detla, null, drawingColor * alpha, GetRadian(Rotation), new Vector2(5, 3), 1.0f, SpriteEffects.None, 0.399f);
            spb.Draw(Sprites.boneTail, Centre + detla, null, drawingColor * alpha, GetRadian(Rotation), new Vector2(5, 3), 1.0f, SpriteEffects.None, 0.399f);
        }

        public override void Update()
        {
            controlLayer = IsMasked ? Surface.Hidden : Surface.Normal;
            if (autoDispose)
            {
                bool ins = GetType() != typeof(CustomBone) ? screen.Contain(Centre) : (this as CustomBone).screenC.Contain(Centre);
                if (ins && (!hasBeenInside)) hasBeenInside = true;
                if (hasBeenInside && (!ins)) Dispose();
            }
        }

        private JudgementState JudgeState
        {
            get
            {
                return GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Lenient;
            }
        }

        public void GetCollide(Player.Heart Heart)
        {
            Random rd = new Random();
            int d = rd.Next(0, 5);
            if (alpha <= 0.9f) return;
            float A, B, C, dist;
            bool needAP = ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0;
            if (Rotation == 0)
                dist = Centre.X - Heart.Centre.X;
            else
            {
                float k = (float)Math.Tan(GetRadian(Rotation + 90));
                A = k;
                B = -1;
                C = -A * Centre.X - B * Centre.Y;
                dist = (float)((A * Heart.Centre.X + B * Heart.Centre.Y + C) / Math.Sqrt(A * A + B * B));
            }

            float res = Math.Max(Math.Abs(dist) - 4.5f, GetDistance(Heart.Centre, Centre) - Length / 2f - 3.5f);

            int offset = 3 - (int)JudgeState;

            if (colorType == 1 && Heart.IsStable) return;
            if (colorType == 2 && Heart.IsMoved) return;
            if (res < 0)
            {
                if (!hasHit)
                    PushScore(0);
                LoseHP(Heart);
                hasHit = true;
            }
            else if (res <= 1.6f - offset * 0.4f)
            {
                if (score >= 2) { score = 1; Player.CreateCollideEffect(Color.LawnGreen, 3f); }
            }
            else if (res <= 4.2f - offset * 1.2f)
            {
                if (score >= 3) { score = 2; Player.CreateCollideEffect(Color.LightBlue, 6f); }
            }
            if (score != 3 && needAP && MarkScore)
            {
                if (!hasHit)
                {
                    PushScore(0);
                    LoseHP(Heart);
                    hasHit = true;
                }
            }
        }

        public override void Dispose()
        {
            if (!hasHit && MarkScore) PushScore(score);
            base.Dispose();
        }

        public Bone()
        {
            drawingColor = GameMain.CurrentDrawingSettings.themeColor;
            Depth = 0.5f;
            UpdateIn120 = true;
            controlingBox = FightBox.instance as RectangleBox;
            if (controlingBox == null) throw new NotImplementedException();
        }
    }
}