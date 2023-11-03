using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public partial class WoodenHall
    {
        private static partial class WaveLib
        {
            public static void ItemWave0Initialize()
            {
                SetSoul(0);
                SetBox(300, 160, 160);
                TP(280, 320);
            }
            public static void ItemWave0Update()
            {
                if (waveTime % 5 == 0 && waveTime <= 440)
                {
                    CreateGB(new NormalGB(FightBox.instance.Centre + GetVector2(140, waveTime * 4), Heart.Centre, new Vector2(1.0f, 0.5f), waveTime * 4 + 180, 35, 14)
                    {
                        IsShake = true
                    });
                }
                if (waveTime >= 500)
                {
                    ChangeRound();
                }
            }
        }
    }
}