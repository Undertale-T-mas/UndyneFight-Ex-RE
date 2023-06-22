using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class WoodenHall
    {
        private static partial class WaveLib
        {
            public static void Wave2Initialize()
            {
                SetSoul(2);
                SetBox(310, 320, 140);
                TP(280, 320);
            }
            public static void Wave2Update()
            {
                if (waveTime == 2)
                {
                    HeartAttribute.Gravity = 6.3f;
                    HeartAttribute.JumpSpeed = 4.6f;
                }
                float speed = 2.4f, downLen = 38f, upLen = 68f;
                if (waveTime % 60 == 0)
                {
                    CreateBone(new DownBone(true, speed, downLen));
                    CreateBone(new DownBone(true, speed, downLen));
                    CreateBone(new UpBone(true, speed, upLen));
                    CreateBone(new UpBone(true, speed, upLen));
                }
                if (waveTime % 110 == 30)
                {
                    CreateBone(new DownBone(true, speed * 2f, downLen * 2f) { ColorType = 1 });
                }
                if (waveTime % 120 == 90)
                {
                    CreateEntity(new Boneslab(0, 20, 30, 50) { ColorType = 2 });
                }
                if (waveTime == 750)
                {
                    ResetBarrage();
                    ChangeRound();
                }
            }
        }
    }
}