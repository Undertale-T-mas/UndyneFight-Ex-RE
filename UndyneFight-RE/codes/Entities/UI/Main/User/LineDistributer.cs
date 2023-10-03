using Microsoft.Xna.Framework;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class UserUI
    {
        private class LineDistributer : Entity
        {
            private void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
            {
                DrawingLab.DrawLine(start, end, size, color, 0.5f);
            }
            public override void Draw()
            {
                float l1 = 60, r1 = 325;
                this.DrawLine(new Vector2(l1, 65), new Vector2(l1 + 20, 45), Color.White);
                this.DrawLine(new Vector2(l1, 65), new Vector2(l1, 180), Color.White);

                this.DrawLine(new(l1, 180), new(l1 + 30, 210), Color.White);
                this.DrawLine(new(l1 + 30, 210), new(r1, 210), Color.White);
                
                this.DrawLine(new(l1, 95), new(l1 + 30, 125), Color.White);
                this.DrawLine(new(l1 + 30, 125), new(r1, 125), Color.White);

                float l2 = 390, r2 = 900;
                this.DrawLine(new(l2, 50), new(l2, 340), Color.White);

                this.DrawLine(new(l2, 340), new(l2 + 30, 370), Color.White);
                this.DrawLine(new(l2 + 30, 370), new(r2, 370), Color.White);
            }

            public override void Update()
            { 

            }
        }
    }
}
