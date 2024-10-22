﻿using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Remake
{
    public static class BoxUtils
    {
        public static VertexBox VertexBoxInstance => FightBox.instance as VertexBox;
        public static bool IsVertexEnabled => BoxStates.CurrentBox is VertexBox;

        public static void Vertexify()
        {
            Player.Heart curHeart = BoxStates.CurrentBox.Detect;
            curHeart.controlingBox.Dispose();
            FightBox.boxs.Remove(curHeart.controlingBox);
            VertexBox box = new(curHeart, BoxStates.CurrentBox as RectangleBox);
            curHeart.controlingBox = box;
            InstanceCreate(box);
        }
        public static void Vertexify(Player.Heart heart)
        {
            Player.Heart curHeart = heart;
            curHeart.controlingBox.Dispose();
            FightBox.boxs.Remove(curHeart.controlingBox);
            VertexBox box = new(curHeart, BoxStates.CurrentBox as RectangleBox);
            curHeart.controlingBox = box;
            InstanceCreate(box);
        }
        public static void DeVertexify(CollideRect area)
        {
            Player.Heart curHeart = BoxStates.CurrentBox.Detect;
            curHeart.controlingBox.Dispose();
            FightBox.boxs.Remove(curHeart.controlingBox);
            RectangleBox box = new(curHeart, area);
            curHeart.controlingBox = box;
            InstanceCreate(box);
        }

        public static void Move(int index, Vector2 value)
        {
            VertexBoxInstance.SetPosition(index, value);
        }
        public static void Move(Vector2 value)
        {
            FightBox cur = VertexBoxInstance;
            for(int i = 0; i < cur.Vertexs.Length; i++)
            {
                cur.Vertexs[i].MissionPosition += value;
            }
        }
        public static void Rotate(Vector2 centre, float rotation)
        {
            FightBox cur = VertexBoxInstance;
            for(int i = 0; i < cur.Vertexs.Length; i++)
            {
                cur.Vertexs[i].MissionPosition = centre + MathUtil.Rotate(cur.Vertexs[i].MissionPosition - centre, rotation);
            }
        }
    }
}