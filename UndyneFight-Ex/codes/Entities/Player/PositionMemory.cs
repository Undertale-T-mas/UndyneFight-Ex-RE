using Microsoft.Xna.Framework;
using System;

namespace UndyneFight_Ex.Entities
{
    public partial class Player
    {
        public partial class Heart
        {
            public PositionMemory CreatePositioner(int maxLength)
            {
                return new PositionMemory(this, maxLength);
            }
            public class PositionMemory : GameObject
            {
                Vector2[] memories;
                Heart mission;
                public PositionMemory(Heart heart, int maxLength)
                {
                    memories = new Vector2[maxLength];
                    mission = heart;
                }
                public override void Update()
                {
                    for (int i = 0; i < memories.Length - 1; i++)
                    {
                        memories[i] = memories[i + 1];
                    }
                    memories[^1] = mission.Centre;
                }
                public Vector2 GetAverageSpeed(int frameTime)
                {
                    return frameTime + 1 >= memories.Length
                        ? throw new ArgumentOutOfRangeException($"{nameof(frameTime)} can not greater than the length of the array")
                        : (memories[memories.Length - frameTime - 1] - memories[^1]) / frameTime;
                }
            }
        }
    }
}