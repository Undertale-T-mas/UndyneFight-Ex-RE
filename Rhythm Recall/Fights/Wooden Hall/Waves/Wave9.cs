using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public partial class WoodenHall
    {
        private static partial class WaveLib
        {
            public static void Wave9Initialize()
            {
                isPlayMusic = true;
                HeartAttribute.UmbrellaAvailable = true;
                HeartAttribute.Gravity = 6.7f;
                HeartAttribute.JumpSpeed = 4.3f;
                wave9_sansMoveSpeed = 0;
                SetSoul(2);
                SetBox(312, 144, 144);
                TP(320, 320);
            }

            private static float wave9_rot;
            private static float wave9_acc;
            private static float wave9_sansMoveSpeed;
            public static void Wave9Update()
            {
                if (waveTime == 35)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        CreateBone(new CentreCircleBone(i * 90, 2.1f, 31, 62 * 6 - 46));
                        CreateBone(new SideCircleBone(i * 90, 2.1f, 26, 62 * 6 - 46));
                    }
                }
                if (waveTime == 62 * 6 + 16) SetBox(100, 392, 312 - 72, 312 + 72);
                if (waveTime == 62 * 6 + 20) { Heart.GiveForce(90, 12.0f); Heart.JumpSpeed = 0.000001f; SansThrowingMovement(1); }
                if (waveTime == 62 * 6 + 53)
                {
                    SansThrowingMovement(3);
                    HeartAttribute.Gravity = 0;
                    Heart.RotateTo(270);
                    SetBox(312, 660, 144);
                }
                if (waveTime == 1177)
                {
                    Sans.HeadTexture = Sans.FaceExpression.Normal;
                    Sans.BodyTexture = Sans.BodyMovement.None;
                    if (IsFoolMode) Regenerate(45);
                    isPlayMusic = false;
                    InstantTP(540, Heart.Centre.Y);
                    InstantSetBox(-10, 600, 312 - 62, 312 + 62);
                    BlackScreen(10); Sans.BodyTexture = Sans.BodyMovement.None;
                    ResetBarrage();
                    CreateEntity(new Boneslab(270, 17, 21, 10));
                }
                if (waveTime == 1236)
                {
                    foreach (var v in (FightBox.instance as RectangleBox).Vertexs)
                    {
                        BarrageStruct.BoneWindfall(v.CurrentPosition, 2, 6, 200, 11, 1, false);
                        BarrageStruct.BoneWindfall(v.CurrentPosition, 2, 10, 200, 11, 1, false);
                        BarrageStruct.BoneWindfall(v.CurrentPosition, 2, 14, 200, 11, 1, false);
                    }
                }
                if (waveTime == 1275)
                {
                    CreateEntity(new Boneslab(180, 120, 76, 14));
                    Heart.GiveForce(0, 8);
                    isPlayMusic = true;
                }
                if (waveTime == 1284)
                    for (int i = 0; i < 20; i++) CreateBone(new SideCircleBone(i * 12 - 56, 7.95f, 90, 95.5f - i * 2.25f));
                if (waveTime == 1260)
                {
                    HeartAttribute.Gravity = 6.7f;
                    HeartAttribute.JumpSpeed = 4.3f;
                    InstantTP(320, 300);
                    isPlayMusic = false;
                    BlackScreen(16);
                    InstantSetBox(300, 140, 140);
                    ScreenDrawing.ScreenAngle = 180f;
                }
                if (waveTime == 1204)
                {
                    SetBox(600 - 124, 600, 312 - 62, 312 + 62);
                }
                if (waveTime == 1213)
                {
                    PlaySound(Sounds.pierce);
                    for (float i = 4.5f; i <= 15.5f; i += 2.4f)
                    {
                        CreateBone(new RightBone(false, i, -i * 4.7f + 94) { ColorType = 2 });
                        CreateBone(new RightBone(true, i, -i * 4.7f + 94) { ColorType = 2 });
                    }
                }
                if (waveTime == 1186)
                {
                    isPlayMusic = true;
                    HeartAttribute.Gravity = 6.7f;
                    HeartAttribute.JumpSpeed = 4.5f;
                    Heart.GiveForce(270, 6);
                }
                if (waveTime == 931 || waveTime == 1150)
                {
                    PlaySound(Sounds.pierce);
                    CreateBone(new CustomBone(new Vector2(-10, 312), Motions.PositionRoute.linear, 0, 40) { PositionRouteParam = new float[] { 8, 0 } });
                }
                if (waveTime == 941)
                {
                    SetBox(312, 660, 124);
                }
                if (waveTime >= 935 && waveTime <= 1100)
                {
                    if (waveTime % 50 == 25) CreateGB(new NormalGB(new Vector2(550, 312 - 30), new Vector2(550, 100), Vector2.One, 180, 20, 28));
                    if (waveTime % 50 == 0) CreateGB(new NormalGB(new Vector2(550, 312 + 30), new Vector2(550, 380), Vector2.One, 180, 20, 28));
                }
                if (waveTime >= 425 && waveTime <= 1175)
                {
                    wave9_sansMoveSpeed = wave9_sansMoveSpeed * 0.9f + 16f * 0.1f;
                    Sans.instance.Centre -= new Vector2(wave9_sansMoveSpeed, 0);
                    if (Sans.instance.Centre.X < -100)
                    {
                        int v = Rand(0, 1);
                        if (v == 0)
                        {
                            Sans.HeadTexture = (Sans.FaceExpression)Rand(0, 2);
                            Sans.BodyTexture = (Sans.BodyMovement)v;
                        }
                        else
                        {
                            Sans.HeadTexture = (Sans.FaceExpression)Rand(1, 2);
                            Sans.BodyTexture = (Sans.BodyMovement)v;
                        }
                        Sans.instance.Centre = new Vector2(800, Sans.instance.Centre.Y);
                    }
                }
                if (waveTime >= 425 && waveTime <= 845)
                {
                    if (waveTime % 6 == 0)
                    {
                        float sk = 0;
                        float[] sinVal = { 0.8f, -0.65f, 0.76f };
                        for (int i = 0; i < sinVal.Length; i++)
                        {
                            sk += Sin((i + 1) * waveTime * 1.5f) * sinVal[i];
                        }
                        float upLength = 72 - sk * 26 - 19, downLength = 72 + sk * 26 - 19;
                        CreateBone(new DownBone(true, 6.0f, downLength));
                        CreateBone(new UpBone(true, 6.0f, upLength));
                    }
                    if (waveTime % 60 == 0)
                    {
                        CreateBone(new CustomBone(new Vector2(645, 312), Motions.PositionRoute.XAccYLinear, 0, 140)
                        {
                            PositionRouteParam = new float[] { -16, 0.22f, 0 },
                            ColorType = Rand(1, 2)
                        });
                    }
                }

                if (waveTime <= 62 * 6)
                {
                    if (waveTime % 62 == 1)
                    {
                        Sans.BodyTexture = Sans.BodyMovement.None;
                        wave9_rot = Rand(0, 3) * 90;
                        CreateEntity(new Boneslab(wave9_rot, 24, 30 + 30, 14));
                    }
                    if (waveTime % 62 == 30)
                    {
                        SansThrowingMovement((int)(wave9_rot / 90));
                        Heart.GiveForce(wave9_rot, 11);
                        PlaySound(Sounds.boneSlabSpawn);
                    }
                }

                if (waveTime == 1380 - 10)
                {
                    ResetBarrage();
                    SetSoul(0);
                    isPlayMusic = false;
                    BlackScreen(16);
                    InstantSetBox(420 - 70, 420 + 70, 300 - 70, 300 + 70);
                    ScreenDrawing.ScreenAngle = 0f;
                }
                if (waveTime == 1400)
                {
                    PlaySound(Sounds.pierce);
                    for (int i = 0; i < 3; i++)
                    {
                        CreateBone(new DownBone(false, 12 + i * 2, 134) { ColorType = 1 });
                        CreateBone(new DownBone(true, 12 + i * 2, 134) { ColorType = 1 });
                    }
                }
                if (waveTime == 1424)
                {
                    PlaySound(Sounds.pierce);
                    for (int i = 0; i < 3; i++)
                    {
                        CreateBone(new UpBone(false, 12 + i * 2, 134) { ColorType = 1 });
                        CreateBone(new UpBone(true, 12 + i * 2, 134) { ColorType = 1 });
                    }
                }
                if (waveTime == 1399 - 10)
                    for (int i = 0; i < 4; i++)
                    {
                        if (i < 2)
                            CreateBone(new CentreCircleBone(i * 90, -5, 64, 78));
                        CreateBone(new SideCircleBone(i * 90, 5, 65, 78));
                    }
                if (waveTime == 1395 - 10)
                {
                    GasterBlaster.IsGBMute = true;
                    CreateGB(new NormalGB(new Vector2(100, 300 - 35), new Vector2(0, 0), Vector2.One, 0, 1448 - 1385, 20) { IsShake = true });
                    CreateGB(new NormalGB(new Vector2(540, 300 - 35), new Vector2(640, 0), Vector2.One, 180, 1448 - 1385, 20) { IsShake = true });
                    InstantTP(420, 300 - 40);
                    isPlayMusic = true;
                }
                if (waveTime == 1470)
                {
                    ResetBarrage();
                    SetSoul(0);
                    isPlayMusic = false;
                    BlackScreen(16);
                    InstantSetBox(220 - 70, 220 + 70, 300 - 70, 300 + 70);
                }
                if (waveTime >= 1487 && waveTime <= 1557 && waveTime % 4 == 0)
                {
                    float dx = Cos((waveTime - 1487) * 4) * 40, dy = Sin((waveTime - 1487) * 4) * 40;
                    CreateBone(new LeftBone(false, 8, 50 + dx));
                    CreateBone(new RightBone(false, 8, 50 - dx));
                    CreateBone(new UpBone(false, 8, 50 + dy));
                    CreateBone(new DownBone(false, 8, 50 - dy));
                }
                if (waveTime == 1485)
                {
                    isPlayMusic = true;
                    InstantTP(220 + 40, 300);
                }
                if (waveTime == 1565)
                {
                    wave9_rot = 0;
                    wave9_acc = 1.4f;
                    ResetBarrage();
                    SetSoul(0);
                    isPlayMusic = false;
                    BlackScreen(16);
                    InstantSetBox(283, 201, 201);
                }
                if (waveTime >= 1580 && waveTime <= 2080)
                {
                    wave9_rot += wave9_acc;
                    wave9_acc += 0.002f;
                    if (waveTime % 6 == 0)
                        CreateGB(new NormalGB(RectangleBox.instance.Centre + GetVector2(140, wave9_rot), Heart.Centre, new Vector2(1.0f, 0.5f), wave9_rot + 180, 35, 14)
                        {
                            IsShake = true
                        });
                }
                if (waveTime == 1580)
                {
                    for (int i = -4; i <= 4; i++)
                    {
                        if ((i + 4) % 2 == 0)
                            BarrageStruct.BoneWindfall(new Vector2(320, 283 + i * 22), 2, Motions.PositionRoute.YAxisSin2,
                                new float[] { 0, 88, 140, 0 }, 3, 17, 500);
                        else
                            BarrageStruct.BoneWindfall(new Vector2(320, 283 + i * 22), 2, Motions.PositionRoute.YAxisSin2,
                                new float[] { 0, 88, 140, 70 }, 3, 17, 500);
                    }
                    isPlayMusic = true;
                    InstantTP(320, 283);
                }
                if (waveTime == 2120) { SetSoul(2); SetBox(312, 144, 144); }
                if (waveTime >= 2130 && waveTime % 15 == 0 && waveTime <= 2380)
                {
                    int dir = Rand(0, 3);
                    Heart.GiveForce(dir * 90, 15);
                    SansThrowingMovement(dir);
                }
            }
        }
    }
}