using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public partial class WoodenHall
    {
        private static partial class WaveLib
        {
            public static void Wave3Initialize()
            {
                SetBox(310, 200, 140);
                TP(280, 320);
            }
            public static Platform p;
            public static void Wave3Update()
            {
                if (waveTime == 2)
                {
                    CreateEntity(new Boneslab(180, 20, 15, 9));
                    SetSoul(2);
                    Sans.BodyTexture = Sans.BodyMovement.ToUp;
                    Heart.GiveForce(180, 6);
                }
                if (waveTime == 14)
                {
                    p = new Platform(0, new Vector2(320, 330), Motions.PositionRoute.cameFromDown, 0, 30);
                    CreatePlatform(p);
                }
                if (waveTime == 27)
                {
                    Sans.BodyTexture = Sans.BodyMovement.ToDown;
                    CreateEntity(new Boneslab(0, 20, 5, 390));
                    Heart.GiveForce(0, 6);
                }
                if (waveTime == 40)
                {
                    PlaySound(Sounds.pierce);
                    BarrageStruct.BoneWindfall(new(210, 310), 2, Motions.PositionRoute.linear, new float[] { 7, 0 }, 8, 18, -1);
                    BarrageStruct.BoneWindfall(new(430, 310), 2, Motions.PositionRoute.linear, new float[] { -7, 0 }, 8, 18, -1);
                }
                if (waveTime == 48)
                {
                    Sans.BodyTexture = Sans.BodyMovement.None;
                    CreateGB(new NormalGB(new Vector2(320, 100), new Vector2(320, 0), new Vector2(1.0f, 0.46f), 90, 30, 25) { IsShake = true });
                }
                if (waveTime == 58)
                {
                    p.ChangeType();
                    p.CreateShinyEffect();
                }
                if (waveTime % 5 == 0 && waveTime >= 300 && waveTime <= 390)
                {
                    CreateGB(new NormalGB(new Vector2(Rand(100, 560), 100), Heart.Centre, new Vector2(1.0f, 0.5f), 60, 10) { IsShake = true });
                }
                if (waveTime % 32 == 0)
                {
                    float speed = 2.6f;
                    CreateBone(new DownBone(true, 620, speed, 45));
                    CreateBone(new UpBone(false, 32, speed, 66));
                }
                if (waveTime >= 100 && waveTime <= 280)
                {
                    float speed = 0.95f;
                    p.ResetStartPosition(new(p.Centre.X + speed, p.Centre.Y));
                    var v = FightBox.instance.CollidingBox;
                    v.X += speed;
                    (FightBox.instance as RectangleBox).InstanceMove(v);
                }
                if (waveTime == 298)
                {
                    p.ChangeType();
                    Heart.RotateTo(30);
                    p.CreateShinyEffect();
                }
                if (waveTime >= 300 && waveTime <= 470)
                {
                    float t = 0.85f;
                    float speed = -1.55f;
                    p.ResetStartPosition(new(p.Centre.X + speed, p.Centre.Y * t + 339 * (1 - t)));
                    var v = FightBox.instance.CollidingBox;
                    v.X += speed;
                    (FightBox.instance as RectangleBox).InstanceMove(v);

                    p.LengthRouteParam[0] = p.LengthRouteParam[0] * t + 42 * (1 - t);
                    p.RotationRouteParam[0] = p.RotationRouteParam[0] * t + 30 * (1 - t);
                }
                if (waveTime >= 58 && waveTime <= 78)
                {
                    float v = 0.92f;
                    p.ResetStartPosition(new Vector2(p.Centre.X, p.Centre.Y * v + 346 * (1 - v)));
                }
                if (waveTime >= 74 && waveTime <= 98)
                {
                    float v = 0.85f;
                    p.LengthRouteParam[0] = p.LengthRouteParam[0] * v + 48 * (1 - v);
                }
                if (waveTime == 49)
                {
                    SetBox(315, 160, 124);
                }
                if (waveTime == 475)
                {
                    Heart.RotateTo(0);
                    ResetBarrage();
                    ChangeRound();
                }
            }
        }
    }
}