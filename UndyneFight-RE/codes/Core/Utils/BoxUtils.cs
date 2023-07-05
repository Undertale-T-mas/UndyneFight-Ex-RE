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
            VertexBox box = new(Functions.BoxStates.CurrentBox as RectangleBox);
            curHeart.controlingBox = box; 
            GameStates.InstanceCreate(box);
        }
    }
}