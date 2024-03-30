using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.Network
{
    internal class WarningShower : Entity
    {
        private string info;

        public WarningShower(string v, GLFont font = null)
        {
            this.info = v;
            this.UpdateIn120 = true;
            this.font = font ?? FightResources.Font.NormalFont;
            this.size = this.font.SFX.MeasureString(v) * 1.12f + new Vector2(0, 50);
        }
        GLFont font;
        Vector2 size;
        Vector2 location = new(-400, 600);

        public float appearTime = 0.0f;
        public override void Update()
        {
            appearTime += 0.5f;
            if (appearTime < 60) location = Vector2.Lerp(location, new(50, 600), 0.12f);
            else if (appearTime > 300) location.X -= 6f;
            else if (appearTime > 600) this.Dispose();
        }
        public override void Draw()
        {
            if (BSet.problem) return;
            CollideRect rect = new(location, size);
            Vector2 titlePos = rect.GetCentre() - new Vector2(0, 21);
            Vector2 infoPos = rect.GetCentre() + new Vector2(0, 21);
            this.font.CentreDraw("Warning!", titlePos, Color.Red, 1.34f, 0.1f);
            this.font.CentreDraw(info, infoPos, Color.Yellow, 1.05f, 0.1f);
            this.Image = FightResources.Sprites.pixUnit;
            this.Depth = 0.02f;
            this.FormalDraw(this.Image, rect, Color.Gray);
            DrawingLab.DrawRectangle(rect, Color.White, 3f, 0.1f);
        }
    }
}