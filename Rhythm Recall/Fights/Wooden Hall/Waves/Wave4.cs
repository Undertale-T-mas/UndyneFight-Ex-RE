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
            public static void Wave4Initialize()
            {
                SetSoul(0);
                SetBox(310, 140, 140);
                TP(280, 320);
            }
            private static Boneslab down, up;
            private static float upPos, downPos;
            public static void Wave4Update()
            {
                if (waveTime == 570)
                {
                    ResetBarrage();
                    ChangeRound();
                    return;
                }
                if (waveTime == 20)
                {
                    CreateEntity(down = new Boneslab(0, 30, 20, 500
                        , Motions.LengthRoute.sin, new float[] { 23, 120, 0, 36 }));
                    CreateEntity(up = new Boneslab(180, 30, 20, 500
                        , Motions.LengthRoute.sin, new float[] { 23, 120, 60, 36 }));
                }
                if (waveTime % 150 == 75)
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(false, 5, 133) { ColorType = 1 });
                }
                if (waveTime % 150 == 0)
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new DownBone(false, 5, 133) { ColorType = 2 });
                }
                if (waveTime >= 22)
                {
                    downPos = down.LengthRoute(down);
                    upPos = up.LengthRoute(up);
                    float speed = 2.0f;
                    if (waveTime % 60 == 0)
                    {
                        CreateBone(new UpBone(false, speed, 30) { Tags = new string[] { "v" } });
                    }
                    if (waveTime % 60 == 30)
                    {
                        CreateBone(new DownBone(false, speed, 30) { Tags = new string[] { "t" } });
                    }
                    var v = GetAll<UpBone>("v");
                    foreach (var bone in v)
                    {
                        bone.Length = upPos + 36;
                    }
                    var t = GetAll<DownBone>("t");
                    foreach (var bone in t)
                    {
                        bone.Length = downPos + 36;
                    }
                }
                if (waveTime == 3)
                {
                    PlaySound(Sounds.Ding);
                    SetSoul(0);
                }
            }
        }
    }
}