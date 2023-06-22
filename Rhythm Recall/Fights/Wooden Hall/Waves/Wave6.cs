using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class WoodenHall
    {
        private static partial class WaveLib
        {
            private static CentreCircleBone boneA, boneB;
            public static void Wave6Initialize()
            {
                SetSoul(0);
                SetBox(302, 164, 164);
                TP(280, 320);
            }
            public static void Wave6Update()
            {
                if (waveTime == 10)
                {
                    for (int i = -3; i <= 3; i++)
                    {
                        if ((i + 4) % 2 == 0)
                            BarrageStruct.BoneWindfall(new Vector2(320, 304 + i * 23), 2, Motions.PositionRoute.YAxisSin2,
                                new float[] { 0, 74, 160, 0 }, 3, 18, 700);
                        else
                            BarrageStruct.BoneWindfall(new Vector2(320, 304 + i * 23), 2, Motions.PositionRoute.YAxisSin2,
                                new float[] { 0, 74, 160, 80 }, 3, 18, 700);
                    }
                    CreateBone(boneA = new CentreCircleBone(0, 0.15f, 160 * 1.4f, 700));
                    CreateBone(boneB = new CentreCircleBone(90, 0.15f, 160 * 1.4f, 700));
                }
                if (waveTime >= 21 && waveTime <= 120)
                {
                    boneA.RotateSpeed = waveTime / 120f;
                    boneB.RotateSpeed = waveTime / 120f;
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