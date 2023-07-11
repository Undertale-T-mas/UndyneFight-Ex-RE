using Microsoft.Xna.Framework;

using static UndyneFight_Ex.FightResources.Font;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private static void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
        {
            DrawingLab.DrawLine(start, end, size, color, 0.5f);
        }
        private class LineDistributer : Entity
        {
            public override void Draw()
            {
                float l1 = 50, r1 = 320;
                DrawLine(new(l1, 50), new(l1, 430), Color.White);
                DrawLine(new(l1 + 30, 460), new(r1 - 45, 460), Color.White);
                DrawLine(new(l1 + 30, 460), new(l1, 430), Color.White);

                DrawLine(new(l1 + 90, 80), new(l1 + 110, 100), Color.White);
                DrawLine(new(l1 + 110, 100), new(r1, 100), Color.White);
                DrawLine(new(r1, 100), new(r1, 415), Color.White);

                NormalFont.Draw("Settings", new Vector2(161, 49), Color.White, 1.45f, 0.1f);

                float l2 = 355 + 25, r2 = 899;
                DrawLine(new(l2 - 25, 50), new(l2, 75), Color.White);
                DrawLine(new(l2, 430), new(l2, 75), Color.White);
                DrawLine(new(l2 + 30, 460), new(l2, 430), Color.White);
                DrawLine(new(l2 + 30, 460), new(r2, 460), Color.White);

                DrawLine(new(640, 460), new(640, 110), Color.White );
                DrawLine(new(r2, 75), new(r2, 520), Color.White );

                DrawLine(new(l2 + 30, 520), new(r2, 520), Color.White );
                DrawLine(new(l2 + 30, 520), new(l2 + 10, 500), Color.White );

                DrawLine(new(r2 - 25, 50), new(r2, 75), Color.White );

                this.Image = Resources.UI.Gear;
                FormalDraw(this.Image, new Vector2(l1 + 45, 84), Color.White, 2, 0, ImageCentre);
            }

            public override void Update()
            { 

            }
        }
    }
}
