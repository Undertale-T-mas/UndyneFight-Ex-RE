using Microsoft.Xna.Framework;

namespace UndyneFight_Ex.Remake.UI
{
    internal partial class SelectUI
    {
        private class LineDistributer : Entity
        {
            private void DrawLine(Vector2 start, Vector2 end, Color color, float size = 3f)
            {
                DrawingLab.DrawLine(start, end, size, color, 0.5f);
            }
            public override void Draw()
            {
                //960, 720

                //left side
                float r1 = 231;
                DrawLine(new(r1, 65), new(r1, 585), Color.White);
                DrawLine(new(r1, 65), new(r1 - 30, 35), Color.White);

                DrawLine(new(r1, 360), new(55, 360), Color.White);
                DrawLine(new(25, 330), new(55, 360), Color.White); 

                DrawLine(new(r1, 585), new(55, 585), Color.White);
                DrawLine(new(25, 585 - 30), new(55, 585), Color.White);


                //right side
                float r2 = 930;
                float l2 = 930 - 280 - 6;
                DrawLine(new(r2, 65), new(r2, 465), Color.White);
                DrawLine(new(r2, 65), new(r2 - 30, 35), Color.White);

                DrawLine(new(l2, 65), new(l2, 650), Color.White);
                DrawLine(new(l2, 65), new(l2 + 30, 35), Color.White);

                DrawLine(new(l2, 175), new(l2 + 30, 145), Color.White);
                DrawLine(new(l2 + 30, 145), new(r2, 145), Color.White);

                DrawLine(new(l2, 115 + 212), new(l2 + 30, 145 + 212), Color.White);
                DrawLine(new(l2 + 30, 145 + 212), new(r2, 145 + 212), Color.White);
                 
                DrawLine(new(l2, 465), new(r2, 465), Color.White);

                DrawLine(new(l2 + 30, 605), new(l2, 575), Color.White);
                DrawLine(new(l2 + 30, 605), new(r2, 605), Color.White);

                DrawLine(new(l2 + 25, 675), new(l2, 650), Color.White);
                DrawLine(new(l2 + 25, 675), new(r2, 675), Color.White);
            }

            public override void Update()
            { 

            }
        }
    }
}
