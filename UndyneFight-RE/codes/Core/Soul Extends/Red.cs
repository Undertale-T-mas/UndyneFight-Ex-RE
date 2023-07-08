using System;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UndyneFight_Ex.Entities;

namespace UndyneFight_Ex.Remake
{
    public partial class Souls
    {
        public static Player.MoveState RedSoul { get; private set; } = new(Color.Red, (s) =>
        {
            SoulMove(s);
        });

        private static void SoulMove(Player.Heart s)
        {
            CollideRect curPos = s.CollidingBox;

            Vector2 curCentre = curPos.GetCentre();

            Vector2 delta = Vector2.Zero;
            for (int i = 0; i < 4; i++)
            {
                if (GameStates.IsKeyDown(s.movingKey[i])) delta += MathUtil.GetVector2(s.Speed * 0.5f, i * 90);
            }

            Vector2 nexCentre = curCentre + delta;

            FightBox box = s.controlingBox;
            Vector2[] vertexs = box.Vertexs;


            float Project(Vector2 origin, Vector2 vector)
            {
                return Vector2.Dot(origin, vector) / origin.Length();
            }

            // calculate all vertexs' normal vector

            Vector2[] normals = new Vector2[vertexs.Length];
            for (int i = 0; i < vertexs.Length; i++)
            {
                Vector2 a = vertexs[i], b = vertexs[(i + 1) % vertexs.Length];
                Vector2 normal = MathUtil.Rotate(b - a, 90);
                Vector2 centre = (a + b) / 2;
                Vector2 along = (b - a) / 2;

                Vector2 del1 = curCentre - centre;
                Vector2 del2 = nexCentre - centre;

                float distance = along.Length();
                normal.Normalize();
                along.Normalize();

                //project the vector to the along vector to make sure the heart can be control by the segment
                float dirDelta1 = Project(along, del1), dirDelta2 = Project(along, del2);
                if (MathF.Abs(dirDelta1) > distance + 0.2f && MathF.Abs(dirDelta2) > distance + 0.2f) { continue; }

                //project the vector to the normal vector and get the distance of heart and line
                float dis1 = Project(normal, del1), dis2 = Project(normal, del2);
                if (dis1 < 0) { dis1 = -dis1; dis2 = -dis2; normal = -normal; }

                if (dis2 < 8)
                {
                    dis2 = 8;
                    // linear combination
                    nexCentre = centre + along * dirDelta2 + dis2 * normal;
                }
            }

            s.Centre = nexCentre;
        }
    }
}