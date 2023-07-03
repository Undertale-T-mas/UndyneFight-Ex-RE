using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SettingUI
    {
        private class LineDistributer : Entity
        {
            private void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
            {
                DrawingLab.DrawLine(start, end, size, color, 0.5f);
            }
            public override void Draw()
            {
                float l1 = 50, r1 = 330;
                DrawLine(new(l1, 50), new(l1, 430), Color.White);
                DrawLine(new(l1 + 30, 460), new(r1, 460), Color.White);
                DrawLine(new(l1 + 30, 460), new(l1, 430), Color.White);

                DrawLine(new(l1 + 80, 70), new(l1 + 110, 100), Color.White);
                DrawLine(new(l1 + 110, 100), new(r1, 100), Color.White);
            }

            public override void Update()
            { 

            }
        }
    }
}
