using System;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Remake
{
    public partial class Souls
    {
        private static float Project(Vector2 origin, Vector2 vector)
        {
            return Vector2.Dot(origin, vector) / origin.Length();
        }
        public static Player.MoveState RedSoul { get; private set; } = new(Color.Red, (s) =>
        {
            SoulMove(s);
        });

        private static void SoulMove(Player.Heart s)
        {
            CollideRect curPos = s.CollidingBox;

            Vector2 curCentre = curPos.GetCentre();

            float speed = s.Speed;
            if (IsKeyDown(InputIdentity.Cancel)) { speed *= 0.5f; }
            Vector2 delta = Vector2.Zero;
            for (int i = 0; i < 4; i++)
            {
                if (IsKeyDown(s.movingKey[i])) delta += GetVector2(speed * 0.5f, i * 90);
            }

            Vector2 nexCentre = curCentre + delta;

            FightBox box = s.controlingBox;
            BoxVertex[] vertexs = box.Vertexs;

            // calculate all vertexs' normal vector

            nexCentre = DoBoxRestriction(curCentre, nexCentre, vertexs);

            s.Centre = nexCentre;
        }

        private static Vector2 DoBoxRestriction(Vector2 curCentre, Vector2 nexCentre, BoxVertex[] vertexs)
        {
            Vector2[] normals = new Vector2[vertexs.Length];
            for (int i = 0; i < vertexs.Length; i++)
            {
                Vector2 a = vertexs[i].CurrentPosition, b = vertexs[(i + 1) % vertexs.Length].CurrentPosition,
                normal = Rotate(b - a, 90), centre = (a + b) / 2, along = -centre,
                del1 = curCentre - centre,
                del2 = nexCentre - centre;

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
            return nexCentre;
        }
    }
}