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
    public class Boneslab : Entity, ICustomLength, ICollideAble
    {
        private const int boneslabOuttime = 8;
        private const float quarterAngle = (float)(Math.PI / 2);

        internal static Texture2D BoneSlabTexture => Sprites.boneSlab;
        internal static Texture2D WarningLine => Sprites.warningLine;
        private int appearTime, score = 3, colorType = 0;
        private bool hasHit;
        private Vector2 renderPlace;
        private Vector2 _warningLine;
        private float currentHeight, missionHeight;
        private readonly int appearDelay, totalTime;
        private readonly float trueRotation;
        private Color drawingColor;

        public Action BoneProtruded { get; set; }

        public override void Dispose()
        {
            if (!hasHit && MarkScore) PushScore(score);
            base.Dispose();
        }

        public int ColorType
        {
            set
            {
                switch (value)
                {
                    case 0: drawingColor = Color.White; colorType = 0; break;
                    case 1: drawingColor = new Color(110, 203, 255, 255); colorType = 1; break;
                    case 2: drawingColor = Color.Orange; colorType = 2; break;
                }
            }
        }
        public bool MarkScore { get; set; } = true;
        public float AppearTime => appearTime - appearDelay;

        private FightBox controlingBox;
        public FightBox ControlingBox
        {
            set => controlingBox = value;
        }

        public Func<ICustomLength, float> LengthRoute { get; set; }
        public float[] LengthRouteParam { get => lengthRouteParam; set => lengthRouteParam = value; }
        private float[] lengthRouteParam;

        /// <summary>
        /// 生成一个骨墙
        /// </summary>
        /// <param name="rotation">骨墙旋转角(只能是0,90,180,270,否则会有bug)</param>
        /// <param name="height">骨墙高度</param>
        /// <param name="appearDelay">骨墙出现前的警告时间</param>
        /// <param name="totalTime">骨墙持续时间</param>
        /// <param name="lengthRoute">骨墙高度随时间变化的函数</param>
        /// <param name="lengthRouteParam">骨墙高度随时间变化函数的参数</param>
        public Boneslab(float rotation, float height, int appearDelay, int totalTime, Func<ICustomLength, float> lengthRoute, float[] lengthRouteParam)
        {
            drawingColor = GameMain.CurrentDrawingSettings.themeColor;
            controlingBox = FightBox.instance;
            LengthRoute = lengthRoute;
            this.lengthRouteParam = lengthRouteParam;
            rotation %= 360;
            trueRotation = rotation;
            Rotation = rotation;
            this.totalTime = totalTime;
            this.appearDelay = appearDelay;
        }
        /// <summary>
        /// 生成一个骨墙
        /// </summary>
        /// <param name="rotation">骨墙旋转角(只能是0,90,180,270,否则会有bug)</param>
        /// <param name="height">骨墙高度</param>
        /// <param name="appearDelay">骨墙出现前的警告时间</param>
        /// <param name="totalTime">骨墙持续时间</param>
        public Boneslab(float rotation, float height, float appearDelay, float totalTime)
        {
            controlLayer = Surface.Hidden;
            drawingColor = GameMain.CurrentDrawingSettings.themeColor;
            controlingBox = FightBox.instance;
            rotation %= 360;
            trueRotation = rotation;
            Rotation = rotation;
            missionHeight = height;
            this.totalTime = (int)totalTime;
            this.appearDelay = (int)appearDelay;
        }

        public override void Draw()
        {
            if (trueRotation == 90 || trueRotation == 270)
                GameMain.MissionSpriteBatch.Draw(BoneSlabTexture, renderPlace, new Rectangle(0, 320 - (int)currentHeight, (int)(controlingBox as RectangleBox).Height, (int)currentHeight),
                    drawingColor, GetRadian(Rotation) + (float)Math.PI, new Vector2((controlingBox as RectangleBox).Height / 2, 0), 1.0f, SpriteEffects.None, 0.499f);
            if (trueRotation == 0 || trueRotation == 180)
                GameMain.MissionSpriteBatch.Draw(BoneSlabTexture, renderPlace, new Rectangle(0, 320 - (int)currentHeight, (int)(controlingBox as RectangleBox).Width, (int)currentHeight),
                    drawingColor, GetRadian(Rotation) + (float)Math.PI, new Vector2((controlingBox as RectangleBox).Width / 2, 0), 1.0f, SpriteEffects.None, 0.499f);
            if (appearTime < appearDelay)
            {
                if (trueRotation == 90 || trueRotation == 270)
                    GameMain.MissionSpriteBatch.Draw(WarningLine, _warningLine,
                    new Rectangle(0, 0, (int)(controlingBox as RectangleBox).Height, 2),
                    appearTime % 6 < 3 ? Color.Red : Color.Yellow,
                    GetRadian(Rotation) + (float)Math.PI, new Vector2((int)(controlingBox as RectangleBox).Height / 2, 0),
                    1.0f, SpriteEffects.None, 0.3f);

                else
                    GameMain.MissionSpriteBatch.Draw(WarningLine, _warningLine,
                    new Rectangle(0, 0, (int)(controlingBox as RectangleBox).Width, 2),
                    appearTime % 6 < 3 ? Color.Red : Color.Yellow, GetRadian(Rotation) + (float)Math.PI, new Vector2((int)(controlingBox as RectangleBox).Width / 2, 0), 1.0f, SpriteEffects.None, 0.3f);
            }
        }

        public override void Update()
        {
            FightBox box = controlingBox;
            appearTime += 1;
            if (appearTime >= appearDelay)
            {
                if (appearTime == appearDelay + 1)
                    BoneProtruded?.Invoke();
                if (LengthRoute != null && lengthRouteParam != null)
                {
                    if (appearTime <= appearDelay + boneslabOuttime * 2)
                    {
                        float d = (appearTime - appearDelay * 1.0f) / (boneslabOuttime * 2);
                        float e = d * d * 0.85f + 0.15f;
                        missionHeight = LengthRoute(this);
                        currentHeight = missionHeight * e + currentHeight * (1 - e);
                    }
                    else if (appearTime <= appearDelay + totalTime)
                        currentHeight = LengthRoute(this);
                    else
                        currentHeight -= ((appearTime - appearDelay - totalTime) / 1.2f + 0.5f) * (float)Math.Sqrt(missionHeight) / 7 * (7f / boneslabOuttime);
                    goto A;
                }
                if (appearTime <= appearDelay + boneslabOuttime)
                {
                    currentHeight += missionHeight / 20f;
                    currentHeight = missionHeight * 0.22f + currentHeight * 0.78f;
                    currentHeight = Math.Min(currentHeight, missionHeight);
                }
                else if (appearTime >= appearDelay + totalTime)
                    currentHeight -= ((appearTime - appearDelay - totalTime) / 1.2f + 0.5f) * (float)Math.Sqrt(missionHeight) / 7 * (7f / boneslabOuttime);
                else currentHeight = missionHeight;

                A: if (currentHeight < -4) Dispose();
            }
            renderPlace.X = (float)Math.Cos(quarterAngle + GetRadian(Rotation)) * box.CollidingBox.Width / 2 + box.Centre.X;
            renderPlace.Y = (float)Math.Sin(quarterAngle + GetRadian(Rotation)) * box.CollidingBox.Height / 2 + box.Centre.Y;
            _warningLine.X = -(float)Math.Cos(quarterAngle + GetRadian(Rotation)) * missionHeight + renderPlace.X;
            _warningLine.Y = -(float)Math.Sin(quarterAngle + GetRadian(Rotation)) * missionHeight + renderPlace.Y;
        }

        public void GetCollide(Player.Heart heart)
        {
            RectangleBox box = controlingBox as RectangleBox;
            if (!box.CollidingBox.Contain(heart.Centre)) return;
            if (currentHeight <= 1) return;
            if (colorType == 1 && heart.IsStable) return;
            if (colorType == 2 && heart.IsMoved) return;

            float res = 0x3f3f3f3f;
            if (trueRotation == 0) res = box.Down - currentHeight - heart.Centre.Y;
            else if (trueRotation == 270) res = box.Right - currentHeight - heart.Centre.X;
            else if (trueRotation == 180) res = -(box.Up + currentHeight - heart.Centre.Y);
            else if (trueRotation == 90) res = -(box.Left + currentHeight - heart.Centre.X);

            if (appearTime <= appearDelay) return;

            if (res < 0.7f)
            {
                if (!hasHit) PushScore(0);
                LoseHP(heart); hasHit = true;
            }
            else if (res <= 2.1f)
            {
                if (score >= 2) { score = 1; heart.CreateCollideEffect2(Color.LawnGreen, 3f); }
            }
            else if (res <= 4.5f)
            {
                if (score >= 3) { score = 2; heart.CreateCollideEffect2(Color.LightBlue, 6f); }
            }

            if (score != 3 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
            {
                if (!hasHit)
                {
                    PushScore(0);
                    LoseHP(heart);
                    hasHit = true;
                }
            }
        }
    }
}