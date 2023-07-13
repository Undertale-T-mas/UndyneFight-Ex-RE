using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Remake
{
    public static class BoxUtils
    {
        public static VertexBox VertexBoxInstance => FightBox.instance as VertexBox;
        public static bool IsVertexEnabled => Functions.BoxStates.CurrentBox is VertexBox;

        public static void Vertexify()
        {
            Player.Heart curHeart = Functions.BoxStates.CurrentBox.Detect;
            curHeart.controlingBox.Dispose();
            FightBox.boxs.Remove(curHeart.controlingBox);
            VertexBox box = new(curHeart, Functions.BoxStates.CurrentBox as RectangleBox);
            curHeart.controlingBox = box; 
            GameStates.InstanceCreate(box);
        }
        public static void DeVertexify(CollideRect area)
        {
            Player.Heart curHeart = Functions.BoxStates.CurrentBox.Detect;
            curHeart.controlingBox.Dispose();
            FightBox.boxs.Remove(curHeart.controlingBox);
            RectangleBox box = new(curHeart, area);
            curHeart.controlingBox = box; 
            GameStates.InstanceCreate(box);
        }

        public static void Move(Vector2 value)
        {
            VertexBox cur = VertexBoxInstance;
            for(int i = 0; i < cur.MissionVertexs.Length; i++)
            {
                cur.MissionVertexs[i] += value;
            }
        }
        public static void Rotate(Vector2 centre, float rotation)
        {
            VertexBox cur = VertexBoxInstance;
            for(int i = 0; i < cur.MissionVertexs.Length; i++)
            {
                cur.MissionVertexs[i] = centre + MathUtil.Rotate(cur.MissionVertexs[i] - centre, rotation);
            }
        }
    }
}