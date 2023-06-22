using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public partial class WoodenHall
    {
        private static partial class WaveLib
        {
            public static void SansThrowingMovement(int dir)
            {
                switch (dir)
                {
                    case 0:
                        Sans.BodyTexture = Sans.BodyMovement.ToDown;
                        break;
                    case 1:
                        Sans.BodyTexture = Sans.BodyMovement.ToLeft;
                        break;
                    case 2:
                        Sans.BodyTexture = Sans.BodyMovement.ToUp;
                        break;
                    case 3:
                        Sans.BodyTexture = Sans.BodyMovement.ToRight;
                        break;
                }
            }
            public static void Wave7Initialize()
            {
                HeartAttribute.Gravity = 6.8f;
                HeartAttribute.JumpSpeed = 4.1f;
                SetSoul(2);
                SetBox(322, 124, 124);
                TP(320, 320);
            }
            public static void Wave7Update()
            {
                if (waveTime == 549)
                {
                    Heart.RotateTo(0); ChangeRound();
                    Sans.BodyTexture = Sans.BodyMovement.None;
                }
                if (waveTime % 52 == 0)
                {
                    Sans.BodyTexture = Sans.BodyMovement.None;
                }
                if (waveTime % 52 == 30)
                {
                    int dir = Rand(0, 3);
                    float rot = dir * 90;
                    SansThrowingMovement(dir);
                    Heart.GiveForce(rot, 11);
                    PlaySound(Sounds.boneSlabSpawn);
                    if (Rand(0, 1) == 0)
                    {
                        CreateEntity(new Boneslab(rot, 30, 32, 9));
                    }
                    else
                    {
                        CreateEntity(new Boneslab(rot, 90, 32, 9) { ColorType = 1 });
                    }
                }
            }
        }
    }
}