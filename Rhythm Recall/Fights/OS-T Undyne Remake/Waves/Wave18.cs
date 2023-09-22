using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne
    {
        private static partial class WaveLib
        {
            private static bool isInRound = false;
            private static float lightDistance = 0;
            private static float curSplitWidth = 0;
            private static float splitWidth = 0;
            private static void ResetShader()
            {
                curSplitWidth = 0;
                splitWidth = 20f;
                lightDistance = 0;
                waveTime = 0;
                ScreenDrawing.BackGroundRendering.InsertProduction(light = new LightEffect());
            }
            public static void Wave18Initialize()
            {
                SetSoul(1);
                SetGreenBox();
                TP();

                //effect settings
                isInRound = false;
                CreateEntity(new DialogBox(new Vector2(380, 50), new TextPrinter[] {
                    new(120, "#enemy#Nghhhh!"),
                    new(210, "#enemy#Is that You've got?"),
                    new(200, "#enemy#You'd better try a \nlittle harder than \nthat!"
                    )})
                {
                    AfterDispose = () =>
                    {
                        ScreenDrawing.MakeFlicker(Color.White);
                        AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(9, 9.0f, 2f));
                        PlaySound(FightResources.Sounds.switchScene);
                        PlaySound(FightResources.Sounds.switchScene);
                        PlaySound(FightResources.Sounds.switchScene);
                        ResetShader();
                        isInRound = true;
                        waveTime = 0;
                    }
                });
            }
            public static void Wave18Update()
            {
                if (!isInRound) return;

                lightDistance = MathF.Min(0.72f, MathF.Sqrt(waveTime) * 0.06f) + 0.02f;
                if (waveTime == 52)
                {
                    ScreenDrawing.WhiteOut(60);
                }
                if (waveTime == 100)
                {
                    Regenerate();
                    PlaySound(FightResources.Sounds.heal);
                    Menu.HPShowerPosition += new Vector2(0, 50);
                    Menu.NameShowerPosition += new Vector2(0, 50);
                    ScreenDrawing.UIColor = Color.Red;
                    Menu.Fight.Dispose();
                    Menu.Act.Dispose();
                    Menu.Item.Dispose();
                    Menu.Mercy.Dispose();
                    instance.undyne.IntoFinal();
                }
                if (waveTime == 108)
                {
                    SetSoul(0);
                    PlaySound(FightResources.Sounds.explode);
                    AddInstance(new UndyneFight_Ex.Entities.Advanced.ScreenShaker(15, 15.0f, 2f));
                    InstantSetBox(340, 320, 210);
                }
                FinalAttacking();
            }


            #region Attack BarrageSets 
            internal class MagicForm : Entity
            {
                private readonly float rotateSpeed;
                private float distance = 90f;
                private float alpha = 0f;
                private int appearTime = 0;
                private readonly Vector2[] positions = new Vector2[6];

                public Action<Vector2> ReleasingAction { set; private get; }

                public MagicForm(Vector2 vec)
                {
                    Image = eyeSpritesRepeat[0];
                    CreateEntity(new ParticleGather(vec, 12, 42, 1f, Color.Magenta) { Image_ = FightResources.Sprites.square, Depth = 0.5f });
                    rotateSpeed = Rand(8, 12) * (Rand(0, 1) * 2 - 1) / 5f;
                    Centre = vec;
                }

                public override void Draw()
                {
                    Depth = 0.7f;
                    if (appearTime <= 37)
                    {
                        int index = Math.Max(0, Math.Min(6, (appearTime - 10) / 4));
                        FormalDraw(eyeSpritesStart[index], Centre, Color.Aqua * alpha, 1.6f, 0, ImageCentre);
                    }
                    else
                    {
                        int index = appearTime / 3 % 3;
                        FormalDraw(eyeSpritesRepeat[index], Centre, Color.Aqua * alpha, 1.6f, 0, ImageCentre);
                    }
                    for (int i = 0; i <= 6; i++)
                        DrawingLab.DrawLine(positions[i % 6], positions[(i + 2) % 6], 3, Color.Magenta * alpha, 0.4f);
                }
                public override void Update()
                {
                    appearTime++;

                    if (appearTime == 39) ReleasingAction?.Invoke(Centre);
                    if (appearTime <= 42)
                    {
                        alpha = alpha * 0.89f + 1 * 0.11f;
                        distance = distance * 0.9f + 49f * 0.1f;
                    }
                    else
                    {
                        alpha -= 0.08f;
                        distance += 4f;
                    }
                    if (alpha < 0) Dispose();
                    Rotation += rotateSpeed;
                    for (int i = 0; i <= 5; i++) positions[i] = Centre + GetVector2(distance, Rotation + i * 60);
                }

                private static readonly Texture2D[] eyeSpritesStart = new Texture2D[7];
                private static readonly Texture2D[] eyeSpritesRepeat = new Texture2D[3];

                internal static void LoadResources()
                {
                    for (int i = 0; i < 7; i++) eyeSpritesStart[i] = Loader.Load<Texture2D>("Fights\\OS-T Remake\\EyeEffect\\main body.0" + (i + 1));
                    for (int i = 0; i < 3; i++) eyeSpritesRepeat[i] = Loader.Load<Texture2D>("Fights\\OS-T Remake\\EyeEffect\\main body.08-0" + (i + 1));
                }

                public static void MakeSpears(Vector2 Centre)
                {
                    PlaySound(FightResources.Sounds.largeKnife, 0.67f);
                    for (int i = 0; i < (IsFoolMode ? 7 : 10); i++)
                        CreateSpear(new Pike(Centre, 1) { IsSpawnMute = true, Speed = Rand(-16, 30) / 3.5f + 5.6f, Acceleration = 0.03f });
                    for (int i = 0; i < (IsFoolMode ? 3 : 5); i++)
                        CreateSpear(new Pike(Centre, 1) { IsSpawnMute = true, Speed = Rand(2, 25) / 2.5f + 6.6f, Acceleration = 0.03f });
                }
            }
            internal class FlyingArrow : Entity
            {
                private float speed;
                private int appearTime = 0;
                private readonly int colorType;
                private readonly float finalSpeed;
                private float delta;
                public FlyingArrow(Vector2 pos, int colorType)
                {
                    finalSpeed = Rand(56, 82) / 10f;
                    speed = Rand(14, 36) / 10f;
                    this.colorType = colorType;
                    Depth = 0.67f;
                    Image = FightResources.Sprites.arrow[colorType, 0, 0];
                    Centre = pos;
                    Rotation = Direction(Centre, Heart.Centre);
                    Rotation += Rand(-24, 24);
                    delta = LastRand;
                    if (Rotation <= 0) Rotation += 360;
                    else if (Rotation >= 360) Rotation -= 360;
                }

                public override void Draw()
                {
                    FormalDraw(Image, Centre, Color.White, GetRadian(Rotation + 180), ImageCentre);
                }

                public override void Update()
                {
                    appearTime++;
                    speed = speed * 0.91f + finalSpeed * 0.09f;
                    Centre += GetVector2(speed, Rotation);

                    float perc = MathF.Min(appearTime * 0.016f, 0.2f);
                    float v = delta * perc;
                    Rotation -= v;
                    delta -= v;

                    int curWay = Rotation >= 45 && Rotation <= 135 ? 3 : Rotation > 135 & Rotation <= 225 ? 0 : Rotation > 225 && Rotation <= 315 ? 1 : 2;
                    float dist = GetDistance(Heart.Centre, Centre);
                    if (dist <= 55 && (RotationDist(Rotation + 180, Heart.Shields.RotationOf(colorType)) <= 52 || curWay == Heart.Shields.DirectionOf(colorType)))
                    {
                        if (Rand(0, 7) == 7)
                            PlaySound(FightResources.Sounds.Ding, 0.8f);
                        Dispose();
                    }
                    if (dist <= 8.2f)
                    {
                        LoseHP(Heart);
                    }
                }
            }
            internal class VacanniSpear : Spear
            {
                private Vector2 centrePos;
                private readonly float missionSpeed;
                private float distance;
                public float timeWaiting;
                private float currentSpeed;
                private readonly float rotateTotal;
                public int appearTime = 0;
                public VacanniSpear(Vector2 centrePos, float speed, float distance, float rotation, float timeWaiting, bool way)
                {
                    rotateTotal = (Gametime % 30 + 40) * (way ? 1 : -1);
                    this.centrePos = centrePos;
                    missionSpeed = speed;
                    this.distance = distance;
                    Rotation = rotation;
                    this.timeWaiting = timeWaiting;
                    currentSpeed = -missionSpeed;
                    autoDispose = false;
                }
                public override void Update()
                {
                    appearTime++;
                    if (currentSpeed <= missionSpeed) currentSpeed += missionSpeed * 2 / timeWaiting;
                    if (appearTime < timeWaiting)
                    {
                        if (alpha < 1) alpha += 0.07f;
                        float perc = Sigmoid01((appearTime + 1) / timeWaiting) - Sigmoid01(appearTime / timeWaiting);
                        Rotation += rotateTotal * perc;
                    }
                    Centre = GetVector2(distance, Rotation + 180) + centrePos;
                    if (appearTime < timeWaiting)
                        distance -= currentSpeed / 2;
                    else distance -= currentSpeed;
                    if (distance <= 20)
                    {
                        alpha -= 0.07f;
                        if (alpha < 0) Dispose();
                    }
                    base.Update();
                }
            }
            #endregion

            private static float missionRotation = 45.2f;
            private static RenderProduction light;

            private static void FinalAttacking()
            {
                foreach (var v in GetAll<Spear>())
                {
                    if (Rand(0, 25) >= 6) continue;
                    if (v is VacanniSpear)
                    {
                        var obj = v as VacanniSpear;
                        if (obj.appearTime <= obj.timeWaiting) continue;
                    }
                    var obj_ = v;
                    if (obj_.Alpha < 0.9f) continue;

                    Vector2 c = v.Centre;
                    float p = v.Rotation;

                    CreateEntity(new Particle(new Color(DrawingLab.HsvToRgb(Rand(290, 330), 255, 255, 255)), GetVector2(Rand(20, 30) / 24f, p + Rand(-20, 20) + 180), 3f, c)
                    {
                        DarkingSpeed = 12
                    });
                }

                int _time = waveTime - 108;
                if (_time <= 0) return;

                if (_time == 2440)
                {
                    ScreenDrawing.WhiteOut(150f);
                    PlaySound(FightResources.Sounds.spearAppear);
                }
                if (_time == 2470)
                {
                    PlaySound(FightResources.Sounds.spearShoot);
                    PlaySound(FightResources.Sounds.spearShoot);
                }
                if (_time == 2500)
                {
                    light.Dispose();
                    PlaySound(FightResources.Sounds.damaged);
                    instance.undyne.Dispose();
                    InstantSetBox(300, 160, 140);
                }
                if (_time == 2700)
                {
                    QuitFight();
                }
                if (_time == 2320)
                {
                    ScreenDrawing.WhiteOut(150f);
                }
                if (_time >= 2060 && _time <= 2150 && (_time - 2060) % 4 == 0)
                {
                    float posX = (_time - 2060f) / (2150 - 2060) * 640f;
                    Vector2 pos1 = new(posX, 10), pos2 = new(640 - posX, 470);
                    for (int i = 0; i <= 4; i++)
                    {
                        CreateSpear(new Pike(pos1, 90, 2250 - _time) { Speed = 5f, Acceleration = 0f, DrawingColor = Color.Silver, IsShootMute = true, IsSpawnMute = true });
                        CreateSpear(new Pike(pos2, 270, 2250 - _time) { Speed = 5f, Acceleration = 0f, DrawingColor = Color.Silver, IsShootMute = true, IsSpawnMute = true });
                        pos1.Y -= 140;
                        pos2.Y += 140;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        CreateEntity(new Particle(Color.Gold, GetVector2(11.2f / Rand(5, 10), Rand(0, 359)), 3f, pos1));
                        CreateEntity(new Particle(Color.Gold, GetVector2(11.2f / Rand(5, 10), Rand(0, 359)), 3f, pos2));
                    }
                }
                if (_time >= 2090 && _time <= 2180 && (_time - 2090) % 4 == 0)
                {
                    float posX = (_time - 2090f) / (2180 - 2090) * 640f;
                    Vector2 pos1 = new(posX, 30 + 120), pos2 = new(640 - posX, 450 - 120);
                    CreateSpear(new Pike(pos1, 90, 2250 - _time) { Speed = 5f, Acceleration = 0f, DrawingColor = Color.Silver, IsShootMute = true, IsSpawnMute = false });
                    CreateSpear(new Pike(pos2, 270, 2250 - _time) { Speed = 5f, Acceleration = 0f, DrawingColor = Color.Silver, IsShootMute = true, IsSpawnMute = true });
                }
                if (_time == 2056)
                {
                    SetBox(240, 645, 180);
                }
                if (_time >= 1800 && _time <= 2040)
                {
                    if (_time % 24 == 0)
                    {
                        PlaySound(FightResources.Sounds.spearAppear);
                        PlaySound(FightResources.Sounds.spearAppear);
                        float rot = Rand(0, 44);
                        bool way = Rand(0, 1) == 1;
                        for (int i = 0; i < 8; i++)
                        {
                            CreateSpear(new VacanniSpear(Heart.Centre, 5.9f, 154, rot += 45, 77, way));
                        }
                    }
                }
                if (_time >= 1730 && _time <= 1760)
                {
                    ScreenDrawing.ScreenAngle *= 0.85f;
                    splitWidth *= 0.85f;
                }
                if (_time == 1761)
                {
                    CreateEntity(new MagicForm(Heart.Centre));
                    splitWidth = -0.001f;
                    ScreenDrawing.ScreenAngle = 0;
                }
                if (_time == 1800)
                {
                    PlaySound(FightResources.Sounds.switchScene);
                    PlaySound(FightResources.Sounds.switchScene);
                    SetSoul(2);
                    SetSoul(3);
                    SetSoul(1);
                    SetSoul(0);
                    SetBox(307, 540, 270);
                }
                if (_time >= 1200 && _time <= 1700)
                {
                    if (_time % 24 == 0)
                    {
                        Vector2 pos = Heart.Centre;
                        int ctype = Rand(0, 1);
                        float rot = Rand(0, 3) * 90;
                        for (int i = -1; i <= 1; i++)
                            CreateEntity(new MagicForm(pos + GetVector2(320, rot + i * 20) * new Vector2(1.0f, 0.75f))
                            {
                                ReleasingAction = (vec) =>
                                {
                                    PlaySound(FightResources.Sounds.pierce, 0.62f);
                                    PlaySound(FightResources.Sounds.switchScene, 0.49f);
                                    for (int i = 0; i <= 10; i++)
                                        CreateEntity(new FlyingArrow(vec, ctype));
                                }
                            });
                    }
                    if ((_time - 1200) % (24 * 6 * 2) == 0)
                    {
                        missionRotation *= -1;
                    }
                    waitingTime--;
                    ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle * 0.91f + missionRotation * 0.09f;
                }
                if (_time == 1200)
                {
                    SetSoul(1);
                    SetGreenBox();
                    TP();
                    PlaySound(FightResources.Sounds.largeKnife, 0.8f);
                    splitWidth = 20f;
                    ScreenDrawing.SceneRendering.InsertProduction(new SplitPartsPruduction());
                }

                if (_time == 1076)
                {
                    float rot = 0;
                    for (int t = 0; t < 15; t++)
                    {
                        rot += 10;
                        for (int i = 0; i < 6; i++)
                        {
                            CreateSpear(new SwarmSpear(Heart.Centre, 7.6f, 155, rot += 60, 52 + t * 4));
                            CreateSpear(new SwarmSpear(Heart.Centre, 27.6f, 565, rot, 49 + t * 4));
                            CreateSpear(new SwarmSpear(Heart.Centre, 47.6f, 965, rot, 46 + t * 4));
                        }
                    }
                }
                if (_time >= 750 && _time <= 1060 && _time % 27 == 0)
                {
                    PlaySound(FightResources.Sounds.spearAppear);
                    float rot = Rand(0, 44);
                    for (int i = 0; i < 8; i++)
                    {
                        CreateSpear(new SwarmSpear(Heart.Centre, 7.6f, 155, rot += 45, 52));
                        CreateSpear(new SwarmSpear(Heart.Centre, 27.6f, 565, rot, 50));
                        CreateSpear(new SwarmSpear(Heart.Centre, 47.6f, 965, rot, 48));
                    }
                }
                if (_time == 520)
                {
                    for (int x = -20; x <= 620; x += 40)
                        CreateSpear(new Pike(new Vector2(x, 50), 72, 140) { DrawingColor = new Color(214, 194, 214), Speed = 5.0f, Acceleration = 0.24f });
                }
                if (_time >= 530 && _time <= 570 && _time % 5 == 0)
                {
                    float x = (_time - 530) * 640 / (570 - 530);
                    CreateSplitSpears2(new Vector2(x, 100), 15, 660 - _time);
                }
                if (_time == 580)
                {
                    for (int x = 20; x <= 660; x += 40)
                        CreateSpear(new Pike(new Vector2(x, 150), 108, 80) { DrawingColor = new Color(214, 194, 214), Speed = 5.0f, Acceleration = 0.24f });
                }
                if (_time == 501)
                {
                    SetBox(300, 400, 260);
                }
                if (_time == 260)
                {
                    SetBox(307, 600, 270);
                }
                if (_time >= 280 && _time <= 500)
                {
                    if (_time % 24 == 0)
                    {
                        PlaySound(FightResources.Sounds.spearAppear);
                        PlaySound(FightResources.Sounds.spearAppear);
                        float rot = Rand(0, 44);
                        bool way = Rand(0, 1) == 1;
                        for (int i = 0; i < 8; i++)
                        {
                            CreateSpear(new VacanniSpear(Heart.Centre, 5.9f, 154, rot += 45, 77, way));
                        }
                    }
                }

                if (_time <= 240)
                {
                    if (_time % 17 == 0 && _time >= 170 && _time <= 240)
                    {
                        CreateEntity(new MagicForm(new Vector2(_time * 3 - 310, 114)) { ReleasingAction = MagicForm.MakeSpears });
                    }
                    if (_time % 48 == 0)
                        for (int i = -3; i <= 3; i++)
                        {
                            CreateSpear(new Pike(new Vector2(320 + i * 48 - 6, (FightBox.instance as RectangleBox).Down + 24), 270, 48) { IsSpawnMute = true, IsHidden = true });
                            CreateSpear(new Pike(new Vector2(320 + i * 48 - 18, (FightBox.instance as RectangleBox).Down + 24), 270, 48) { IsSpawnMute = true, IsHidden = true, IsShootMute = true });
                        }
                    if (_time % 48 == 24)
                    {
                        for (int i = -3; i <= 3; i++)
                        {
                            CreateSpear(new Pike(new Vector2(320 + i * 48 + 6, (FightBox.instance as RectangleBox).Up - 24), 90, 48) { IsSpawnMute = true, IsHidden = true });
                            CreateSpear(new Pike(new Vector2(320 + i * 48 + 18, (FightBox.instance as RectangleBox).Up - 24), 90, 48) { IsSpawnMute = true, IsHidden = true, IsShootMute = true });
                        }
                    }
                }
            }
        }
    }
}