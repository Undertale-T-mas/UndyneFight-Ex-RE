using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class WoodenHall
    {
        private static partial class WaveLib
        {
            public static void Wave1Initialize()
            {
                SetSoul(2);
                SetBox(310, 320, 140);
                TP(280, 320);
            }
            public static void Wave1Update()
            {
                if (waveTime == 2)
                {
                    HeartAttribute.Gravity = 6.4f;
                    HeartAttribute.JumpSpeed = 4.5f;
                }
                if (waveTime % 60 == 0)
                {
                    float height = Rand(32, 86);
                    float downLen = height - 20;
                    float upLen = 120 - height;
                    float speed = 2.2f;
                    CreateBone(new DownBone(false, speed, downLen));
                    CreateBone(new DownBone(false, speed, downLen));
                    CreateBone(new UpBone(false, speed, upLen));
                    CreateBone(new UpBone(false, speed, upLen));
                    CreateBone(new CustomBone(new Vector2((FightBox.instance as RectangleBox).Left - 2, 380 - height), Motions.PositionRoute.XAxisSin, 0, 20)
                    {
                        PositionRouteParam = new float[] { speed, 32, 100, Rand(0, 99) }
                    });
                    CreateBone(new CustomBone(new Vector2((FightBox.instance as RectangleBox).Left - 2, 380 - height), Motions.PositionRoute.XAxisSin, 0, 20)
                    {
                        PositionRouteParam = new float[] { speed, 32, 100, LastRand }
                    });
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