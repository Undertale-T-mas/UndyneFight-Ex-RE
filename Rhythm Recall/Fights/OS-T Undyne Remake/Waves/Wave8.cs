using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave8Initialize()
            {
                SetGreenBox();
                TP();
                SetSoul(1);
            }
            public static void Wave8Update()
            {
                if (waveTime == 5)
                {
                    float time = 40;
                    Fortimes(4, () =>
                    {
                        CreateArrow(time, "$2", 14, 0, 0);
                        if (!IsFoolMode) CreateArrow(time, "+2", 14, 1, 0);
                        time += 6f;
                        CreateArrow(time, "$3", 14, 0, 0);
                        if (!IsFoolMode) CreateArrow(time, "+2", 14, 1, 0);
                        time += 6f;
                    });
                    Fortimes(3, () =>
                    {
                        CreateArrow(time, "$0", 9, 0, 1);
                        if (!IsFoolMode) CreateArrow(time, "+2", 9, 1, 0);
                        time += 8f;
                        CreateArrow(time, "$1", 9, 0, 1);
                        if (!IsFoolMode) CreateArrow(time, "+2", 9, 1, 0);
                        time += 8f;
                    });
                }
                if (waveTime == 146)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (!IsFoolMode) CreateSpear(new CircleSpear(new Vector2(320, 240), 3.2f, 1.9f, 155, i * 90, 0));
                        CreateSpear(new Pike(new Vector2(320, 240) + GetVector2(100, i * 90 + 45), i * 90 + 225, 35));
                    }
                    SetSoul(0);
                }
                if (waveTime >= 185 && waveTime <= 237 && waveTime % 6 == 0)
                {
                    CreateSpear(new Pike(new Vector2(Rand(100, 540), Rand(60, 100)), 270 - waveTime));
                }
                if (waveTime >= 309) ChangeRound();
            }
        }
    }
}