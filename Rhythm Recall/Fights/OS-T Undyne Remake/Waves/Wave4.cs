using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            public static void Wave4Initialize()
            {
                SetBox(295, 150, 80);
                TP();
                SetSoul(0);
            }
            public static void Wave4Update()
            {
                if (waveTime <= 550)
                {
                    if (waveTime % 46 == 0)
                    {
                        CreateSpear(new Pike(new Vector2(220, 295 + Rand(-2, 2) * 16), 0, 55) { IsHidden = true });
                        CreateSpear(new Pike(new Vector2(420, 295 + Rand(-2, 2) * 16), 180, 55) { IsHidden = true });
                    }
                    if (!IsFoolMode)
                        if (waveTime % 120 >= 90 && waveTime % 5 == 0)
                        {
                            Vector2 pos = new(Rand(160, 480), Rand(77, 140));
                            CreateSpear(new NormalSpear(pos) { Speed = 8f });
                        }
                }
                if (waveTime == 570)
                {
                    SetSoul(1);
                }
                if (waveTime == 580)
                    ChangeRound();
            }
        }
    }
}