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
            public static void Wave5Initialize()
            {
                SetSoul(2);
                SetBox(300, 265, 168);
                TP(280, 320);
            }
            public static void Wave5Update()
            {
                if (waveTime == 36)
                {
                    for (int i = 0; i < 4; i++)
                        CreateEntity(new Boneslab(i * 90, 7, 24, 650));
                }
                if (waveTime % 70 == 5)
                {
                    float speed = 1.6f;
                    float det = 50;
                    float length = 45f;
                    CreatePlatform(new Platform(Rand(0, 1), new Vector2(110, 320 - det), Motions.PositionRoute.linear, 0, length)
                    {
                        PositionRouteParam = new float[] { speed, 0 }
                    });
                    CreatePlatform(new Platform(Rand(0, 1), new Vector2(530, 320), Motions.PositionRoute.linear, 0, length)
                    {
                        PositionRouteParam = new float[] { -speed, 0 }
                    });
                    CreatePlatform(new Platform(Rand(0, 1), new Vector2(110, 320 + det), Motions.PositionRoute.linear, 0, length)
                    {
                        PositionRouteParam = new float[] { speed, 0 }
                    });
                }
                if (waveTime % 60 == 50 && waveTime <= 700)
                {
                    if (Rand(0, 1) == 0)
                        CreateGB(new NormalGB(new Vector2(100, 300 + 50 * Rand(-1, 1)), new Vector2(100, 0), new Vector2(1.0f, 0.5f), 0, 70, 25) { IsShake = true });
                    else
                        CreateGB(new NormalGB(new Vector2(640, 300 + 50 * Rand(-1, 1)), new Vector2(540, 0), new Vector2(1.0f, 0.5f), 180, 70, 25) { IsShake = true });
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