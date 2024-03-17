using Microsoft.Xna.Framework;
using static UndyneFight_Ex.FightResources.Font;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    public class NameShower : Entity
    {
        public NameShower() { Centre = new Vector2(20, 457); instance = this; }

        public float detla = 50;
        public static string level = "";
        public static string name;
        public static NameShower instance;
        public static float nameAlpha = 1;
        public static string OverrideName = "";

        public override void Draw()
        {
            Vector2 namePos = new(Centre.X, Centre.Y - (FightFont.SFX.MeasureString("HP").Y / 2f));
            string showing = OverrideName == "" ? (string.IsNullOrEmpty(name) ? (string.IsNullOrEmpty(PlayerManager.currentPlayer)
                ? "guest" : PlayerManager.currentPlayer) : name) : OverrideName;
            Vector2 lvPos = new(FightFont.SFX.MeasureString(showing).X + 22 + Centre.X + (GameRule.nameColor == "Colorful" ? 20 : 0), Centre.Y - (FightFont.SFX.MeasureString("HP").Y / 2f));

            switch (GameRule.nameColor)
            {
                case "White":
                    FightFont.Draw(showing,
                        namePos, Color.White * nameAlpha);
                    break;
                case "Blue":
                    FightFont.Draw(showing,
                        namePos, Color.LightBlue * nameAlpha);
                    break;
                case "Orange":
                    FightFont.Draw(showing,
                        namePos, Color.Orange * nameAlpha);
                    break;
                default:
                    FightFont.CentreDraw(OverrideName == "" ? (string.IsNullOrEmpty(PlayerManager.currentPlayer)
                        ? "guest" : PlayerManager.currentPlayer) : OverrideName,
                        new Vector2(100, 462), new Color(DrawingLab.HsvToRgb(GameMain.gameTime, 160, 160, 255)));
                    for (int i = 0; i < 3; i++)
                    {
                        FightFont.CentreDraw(string.IsNullOrEmpty(PlayerManager.currentPlayer)
                            ? "guest" : PlayerManager.currentPlayer,
                            new Vector2(100, 462) + (MathUtil.GetVector2(10, (GameMain.gameTime / 1.5f) + (i * 120)) * new Vector2(1.0f, 0.8f)),
                            new Color(DrawingLab.HsvToRgb((GameMain.gameTime / 1.3f) + (i * 100) + 16, 255, 255, 255)),
                            1.0f, (i / 100f) + 0.01f);
                    }
                    break;
            }

            string trueLV = (level != "") ? level : difficulty.ToString();
            FightFont.Draw("lv " + trueLV,
                lvPos, GameMain.CurrentDrawingSettings.UIColor * nameAlpha);
        }

        public override void Update() { }
    }
}