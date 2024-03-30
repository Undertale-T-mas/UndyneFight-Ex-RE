using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.DrawingLab;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal class RatingShowing : Entity
    {
        Texture2D starMedal = GlobalResources.Sprites.starMedal;
        public RatingShowing()
        {
            Image = GlobalResources.Sprites.medal;
            collidingBox = new(new Vector2(2, 420), new(170, 58));
        }
        public void SetArea(CollideRect collideRect)
        {
            collidingBox = collideRect;
        }

        private float skill = 0;
        private string name = "";
        private bool enabled = false;
        private Color special = Color.Green;
        public string SkillString { get; set; }
        public string CoinString { get; set; }
        public Color SkillColor { get; set; } = Color.White;
        public Color CoinColor { get; set; } = Color.Gold;
        public override void Draw()
        {
            if (BSet.problem) return;
            if (!enabled) return;
            Depth = 0.99f;
            var BoxMiddle = collidingBox.Height / 2;
            DrawLine(new(collidingBox.Left, collidingBox.Y + BoxMiddle), new(collidingBox.Right, collidingBox.Y + BoxMiddle), BoxMiddle * 2, Color.Black * 0.5f, 0.1f);
            DrawRectangle(collidingBox, Color.White, 3, 0.4f);
            DrawLine(new Vector2(0, 4) + collidingBox.TopLeft, new Vector2(0, 4) + collidingBox.TopRight,
                1, special, 0.2f);
            DrawLine(new Vector2(0, 2) + collidingBox.TopLeft, new Vector2(0, 2) + collidingBox.TopRight,
                1, special, 0.2f);
            NormalFont.Draw(name, new Vector2(5, 0) + collidingBox.TopLeft,
                Color.White, 0.8f, 0.4f);
            NormalFont.Draw(SkillString, new Vector2(5, 28) + collidingBox.TopLeft,
                SkillColor, 0.8f, 0.4f);
            NormalFont.Draw($"Coins:{CoinString}G", new Vector2(8, -22) + collidingBox.TopLeft,
                CoinColor, 0.72f, 0.4f);

            Vector2 centre = ImageCentre; centre.Ceiling();
            Depth = 0.98f;
            if (skill >= 60)
                FormalDraw(skill > 90 ? starMedal : Image, new Vector2(-12, 30) + collidingBox.TopRight, Color.White, 0, centre);
            if (skill >= 70)
                FormalDraw(skill > 92.5f ? starMedal : Image, new Vector2(-37, 30) + collidingBox.TopRight, Color.White, 0, centre);
            if (skill >= 80)
                FormalDraw(skill > 95f ? starMedal : Image, new Vector2(-62, 30) + collidingBox.TopRight, Color.White, 0, centre);
        }

        public override void Update()
        {
            enabled = PlayerManager.CurrentUser != null;
            if (!enabled) return;
            name = PlayerManager.CurrentUser.PlayerName;
            skill = PlayerManager.PlayerSkill;
            Color[] SkillColors = { Color.Lime, Color.LawnGreen, Color.Blue,
                Color.MediumPurple, Color.Red, Color.OrangeRed, Color.Orange, Color.Gold};
            if (skill >= 20)
                for (int i = 2; i < 9; i++)
                {
                    if (skill >= i * 10)
                        special = SkillColors[i - 2];
                }
            if (string.IsNullOrEmpty(SkillString))
                SkillString = MathUtil.FloatToString(skill, 2);
            if (string.IsNullOrEmpty(CoinString))
                CoinString = PlayerManager.CurrentUser.ShopData.CashManager.Coins.ToString();
        }
    }
}