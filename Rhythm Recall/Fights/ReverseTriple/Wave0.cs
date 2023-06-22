using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public partial class ReverseTriple
    {
        private static partial class WaveLib
        {
            public static void Wave0Initialize()
            {
                SetBox(300, 160, 160);
                TP(280, 320);
            }
            public static void Wave0Update()
            {
                if (waveTime == 41)
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(2);
                    Heart.GiveForce(0, 10);
                    CreateEntity(new Boneslab(0, 24, 30, 113));
                    CreateEntity(new Boneslab(180, 24, 70, 20));
                }
                if (waveTime == 81)
                {
                    Heart.GiveForce(180, 10);
                }
                if (waveTime == 120)
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(0);
                }
                for (int i = 0; i <= 5; i++)
                    if (waveTime == 110 + i * 5)
                    {
                        CreateGB(new NormalGB(new Vector2(140 + i * 100, 140), Heart.Centre, Vector2.One, 120, 40 - i * 5, 25));
                    }
                if (waveTime == 165)
                {
                    Heart.RotateTo(0);
                }
                if (waveTime == 175)
                {
                    SetBox(310, 140, 140);
                    SetSoul(2);
                    Heart.GiveForce(0, 6);
                }
                if (waveTime == 194)
                {
                    SetSoul(0);
                }
                if (waveTime == 200)
                {
                    for (int i = 0; i < 4; i++)
                        CreateBone(new SwarmBone(70, 90, 10 + i * 22.5f, 96) { ColorType = (i % 2) + 1 });
                }
                if (waveTime >= 208 && waveTime <= 296 && waveTime % 4 == 0)
                {
                    float upLength = Sin(waveTime * 6) * 26 + 57;
                    float downLength = 105 - upLength;
                    CreateBone(new DownBone(true, 6.0f, downLength));
                    CreateBone(new UpBone(true, 6.0f, upLength));
                }
                if (waveTime == 305)
                {
                    CreateGB(new NormalGB(new Vector2(320 - 100, 310 - 100), Heart.Centre, Vector2.One, 45, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320 + 100, 310 - 100), Heart.Centre, Vector2.One, 135, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320 + 100, 310 + 100), Heart.Centre, Vector2.One, 225, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320 - 100, 310 + 100), Heart.Centre, Vector2.One, 315, 35, 20));
                }
                if (waveTime == 340)
                {
                    CreateGB(new NormalGB(new Vector2(320 - 100, 310 - 50), Heart.Centre, Vector2.One, 0, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320 + 100, 310 + 50), Heart.Centre, Vector2.One, 180, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320 - 50, 310 - 100), Heart.Centre, Vector2.One, 90, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320 + 50, 310 + 100), Heart.Centre, Vector2.One, 270, 35, 20));
                }
                if (waveTime == 375)
                {
                    CreateGB(new NormalGB(new Vector2(320 - 100, 310), Heart.Centre, Vector2.One, 0, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320 + 100, 310), Heart.Centre, Vector2.One, 180, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320, 310 - 100), Heart.Centre, Vector2.One, 90, 35, 20));
                    CreateGB(new NormalGB(new Vector2(320, 310 + 100), Heart.Centre, Vector2.One, 270, 35, 20));
                }
                if (waveTime == 430)
                {
                    SetSoul(2);
                    Heart.GiveForce(180, 6);
                }
                if (waveTime == 440)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        CreateBone(new UpBone(false, i * 2 + 3, 20));
                        CreateBone(new UpBone(true, i * 2 + 3, 20));
                    }
                    PlaySound(Sounds.pierce);
                }
                if (waveTime == 500)
                {
                    ChangeRound();
                }
            }
        }
    }
}