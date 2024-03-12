using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.Fight;
using static System.Math;
using static System.MathF;
using static UndyneFight_Ex.Fight.Functions;

namespace UndyneFight_Ex.Entities
{
    public partial class Player
    {
        public partial class Heart
        {
            private interface IShieldImage
            {
                Heart User { get; }
            }
            /// <summary>
            /// 盾牌。角度使用角度值计量
            /// </summary>
            public partial class Shield : Entity, IShieldImage
            {
                public CollisionSide CollisionChecker { get; private set; }

                private class ShieldShadow : Entity, IShieldImage
                {
                    public Heart User => user;
                    public ShieldShadow(Shield shield, float missionRotation)
                    {
                        Rotation = shield.Rotation;
                        rotateWay = (missionRotation - Rotation + 360) % 360 < (360 - missionRotation + Rotation) % 360;
                        rotateStartTime = 1;
                        user = shield.user;
                        this.missionRotation = missionRotation;
                        Image = shield.Image;
                        drawingColor = shield.drawingColor;
                        UpdateIn120 = true;
                        Depth = shield.Depth;
                        Direction = shield.way;
                    }
                    public ShieldShadow(Shield shield)
                    {
                        rotateWay = shield.rotateWay;
                        rotateStartTime = shield.rotateStartTime;
                        user = shield.user;
                        missionRotation = shield.missionRotation;
                        Rotation = shield.Rotation;
                        Image = shield.Image;
                        drawingColor = shield.drawingColor;
                        UpdateIn120 = true;
                        Depth = shield.Depth;
                        Direction = shield.way;
                    }
                    public override void Draw()
                    {
                        FormalDraw(Image, Centre, new Color(drawingColor, 0.6f) * alpha * user.Alpha, MathHelper.ToRadians(Rotation + (user.FixArrow ? 0 : user.Rotation)), ImageCentre);
                    }

                    public int Direction { get; init; }
                    private Color drawingColor;
                    private readonly float missionRotation;
                    private readonly bool rotateWay;
                    private float GetDelta()
                    {
                        return Math.Min((missionRotation - Rotation + 360) % 360, (360 - missionRotation + Rotation) % 360);
                    }

                    private float rotateStartTime;
                    private readonly Heart user;
                    private float alpha = 1.0f;
                    public override void Update()
                    {
                        if (alpha < 0) Dispose();
                        alpha -= rotateStartTime * 0.004f;
                        rotateStartTime++;
                        Centre = user.Centre;
                        if (alpha > 0)
                        {
                            float detla = GetDelta();
                            float scale = Math.Min(Pow(rotateStartTime, 1.5f) / 2.1f * 0.04f, 0.18f);
                            if (detla <= 35f)
                            {
                                scale *= 0.8f * Pow((detla + 37) / 77f, 1.5f) + 0.2f * 1;
                                scale = Math.Min(1, scale * (1 + 15f / (detla * detla + 12)));
                            }
                            if (rotateWay)
                            {
                                Rotation += detla * scale;
                            }
                            else
                            {
                                Rotation -= detla * scale;
                            }
                            Rotation = MathUtil.Posmod(Rotation, 360);
                        }
                    }
                }

                private Color drawingColor;

                public void Hold(int pos)
                {
                    if (pos == -1)
                    {
                        return;
                    }

                    pushDelta *= 0.8f;
                }

                public bool enabled = false;

                public int Way => way;
                internal int way = 0;
                internal int lastWay = 0;

                /// <summary>
                /// 旋转方向, true代表顺时针
                /// </summary>
                private bool rotateWay;
                internal int rotateStartTime = 0;
                private bool rotateStarted = false;

                public float missionRotation = 0;

                private float pushDelta = 0;
                GreenSoulGB attachedGB = null;
                public bool AttachingGB => attachedGB != null;

                public int ColorType { get; init; }
                public InputIdentity[] UpdateKeys { private get; set; } = new InputIdentity[4];

                public void Push(GreenSoulGB gb)
                {
                    int color = gb.DrawingColor;
                    bool Auto = (DebugState.blueShieldAuto && color == 0) || (DebugState.redShieldAuto && color == 1) || (DebugState.greenShieldAuto && color == 2) || (DebugState.purpleShieldAuto && color == 3) || (DebugState.otherAuto && color >= 2);

                    if (Auto || GameStates.IsKeyDown(UpdateKeys[gb.Way]))
                        attachedGB = gb;
                    if (Auto) return;
                    if (pushDelta > user.controlingBox.CollidingBox.Width - 12)
                    {
                        return;
                    }

                    pushDelta += 0.1f;
                    pushDelta *= 1.2f;
                    pushDelta = Math.Min(user.controlingBox.CollidingBox.Width - 12, pushDelta);
                }

                internal Heart user;
                public Heart User => user;

                public float PushDelta => pushDelta;

                private float GetDelta()
                {
                    return Math.Min((missionRotation - Rotation + 360) % 360, (360 - missionRotation + Rotation) % 360);
                }

                public Shield(int type, Heart user)
                {
                    UpdateIn120 = true;
                    this.user = user;
                    Depth = 0.43f - type * 0.005f;
                    Image = FightResources.Sprites.shield;
                    Rotation = 0;

                    drawingColor = type switch
                    {
                        0 => new(0, 128, 255),
                        1 => Color.Red,
                        2 => Color.Yellow,
                        3 => new(255, 128, 255),
                        _ => throw new ArgumentException(),
                    };
                    ColorType = type;
                }

                public override void Start()
                {
                    CollisionChecker = new();
                    AddChild(CollisionChecker);
                    base.Start();
                }

                public override void Draw()
                {
                    if (!enabled)
                    {
                        return;
                    }

#if DEBUG
                    for(int i = 0; i < 4; i++) 
                        if (GameStates.IsKeyDown(UpdateKeys[i]))
                        {
                            Vector2 position = user.Centre + MathUtil.GetVector2(50, i * 90f);
                            Vector2 delta = MathUtil.GetVector2(30, i * 90 + 90);
                            DrawingLab.DrawLine(position + delta, position - delta, 3f, this.drawingColor * 0.4f, 0.4f);
                        }
#endif

                    FormalDraw(Image, Centre + MathUtil.GetVector2(pushDelta, Rotation + 180 + (user.FixArrow ? 0 : (user.FixArrow ? 0 : user.Rotation))), new Color(drawingColor, 0.6f) * user.Alpha, MathHelper.ToRadians(Rotation + user.Rotation), ImageCentre);
                }

                public override void Update()
                {
                    if (attachedGB != null && (attachedGB.Ending || attachedGB.Disposed || attachedGB.Way != way ||
                        (!GameStates.IsKeyDown(UpdateKeys[attachedGB.Way]) && !attachedGB.Auto))) attachedGB = null;
                    while (resetTime > 0)
                    {
                        resetTime--;
                        pushDelta *= 0.6f;
                    }
                    rotateStartTime++;
                    Centre = user.Centre;
                    if (rotateStarted)
                    {
                        float detla = GetDelta();
                        float scale = Math.Min(Pow(rotateStartTime, 1.5f) / 2.1f * 0.04f, 0.18f);
                        if (detla <= 35f)
                        {
                            scale *= 0.8f * Pow((detla + 37) / 77f, 1.5f) + 0.2f * 1;
                            scale = Math.Min(1, scale * (1 + 15f / (detla * detla + 12)));
                        }
                        if (rotateWay)
                        {
                            Rotation += detla * scale;
                        }
                        else
                        {
                            Rotation -= detla * scale;
                        }
                        Rotation = MathUtil.Posmod(Rotation, 360);
                    }
                }

                private int resetTime = 0;
                public void ResetPushDelta()
                {
                    resetTime = 20;
                }

                public void Rotate(int missionWay)
                {
                    if (attachedGB != null && pushDelta < 4)
                    {
                        AddChild(new ShieldShadow(this, missionWay * 90));
                        return;
                    }
                    if (way != missionWay)
                    {
                        if (hearts.Count == 1)
                            user.Shields.ShieldRotated();
                        ResetPushDelta();
                        if (rotateStartTime < 8f)
                            AddChild(new ShieldShadow(this));
                    }
                    lastWay = way;
                    way = missionWay;
                    missionRotation = missionWay * 90;
                    rotateWay = (missionRotation - Rotation + 360) % 360 < (360 - missionRotation + Rotation) % 360;

                    rotateStartTime = rotateStartTime >= 9f ? 0 : (int)(rotateStartTime / 3f + 1);

                    rotateStarted = true;
                }

                public void CreateShinyEffect(int type, int direction)
                {
                    if (type < 1 || type > 4)
                    {
                        return;
                    }

                    Color cl = Color.Orange;
                    switch (type)
                    {
                        case 1:
                            cl = Color.Green;
                            break;
                        case 2:
                            cl = Color.LightBlue;
                            break;
                        case 3:
                            cl = Color.Gold;
                            break;
                    }

                    ShieldShinyEffect effect = null;
                    if (way == direction)
                        effect = new ShieldShinyEffect(this, cl);
                    else
                    { 
                        foreach (GameObject obj in this.ChildObjects)
                        { 
                            if (obj is ShieldShadow)
                            {
                                ShieldShadow shadow = obj as ShieldShadow; ;
                                if (shadow.Direction == direction) { effect = new ShieldShinyEffect(shadow, cl); break; }
                            }
                        }
                        effect = new ShieldShinyEffect(this, cl);
                    }
                    GameStates.InstanceCreate(effect);

                    MakeParticle(type);
                }

                private void MakeParticle(int type)
                {
                    Vector2 createCentre = Centre + MathUtil.GetVector2(33, missionRotation + Functions.Heart.Rotation);
                    if (type != 0)
                    {
                        int times = type switch
                        {
                            1 => 12,
                            2 => 8,
                            4 => 5,
                            5 => 3,
                            3 => 3,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        for (int i = 0; i < times; i++)
                        {
                            float rotation1 = missionRotation + 90 + Rand(0, 1) * 180;
                            float rdelta = Rand(0, 1f) * Rand(0, 1f) * Rand(0, 1f) * RandSignal();
                            rotation1 += rdelta * 90;
                            GameStates.InstanceCreate(new Particle((type switch
                            {
                                1 => Color.Lime,
                                2 => Color.LightBlue,
                                4 => Color.Orange,
                                5 => Color.Orange,
                                3 => Color.Gold,
                                _ => throw new ArgumentOutOfRangeException()
                            }) * Rand(0.67f, 0.85f), MathUtil.GetVector2(Rand(4f, 8f), rotation1), Rand(6, 10), createCentre, FightResources.Sprites.square)
                            { DarkingSpeed = Rand(10f, 14.6f), SlowLerp = 0.25f });
                        }
                    }
                }

                protected class ShieldShinyEffect : Entity
                {
                    private readonly Entity attracter;
                    public ShieldShinyEffect(Entity att, Color cl)
                    {
                        attracter = att;
                        drawingColor = cl;
                        Image = FightResources.Sprites.shinyShield;
                    }

                    private float drawingScale = 1.0f;
                    private float darkerSpeed = 7.9f;

                    public float DarkerSpeed { set => darkerSpeed = value; }
                    public Vector2 MissionSize { set => missionSize = value; }

                    private Color drawingColor;
                    private Vector2 missionSize = new(2.6f, 1.5f);

                    public override void Update()
                    {
                        if (attracter != null)
                        {
                            Centre = attracter.Centre + MathUtil.GetVector2(33, attracter.Rotation + (attracter as IShieldImage).User.Rotation);
                            Rotation = attracter.Rotation;
                            Depth = attracter.Depth + 0.0101f;
                        }
                        drawingScale += darkerSpeed / 100f;
                        if (drawingScale >= 2f)
                        {
                            Dispose();
                        }
                    }

                    public override void Draw()
                    {
                        FormalDraw(Image, Centre, drawingColor * MathF.Min(1, (float)Math.Pow(2.1f - drawingScale, 2.1f)), Vector2.Lerp(Vector2.One, missionSize, drawingScale - 1), MathUtil.GetRadian(Rotation + (attracter as IShieldImage).User.Rotation), ImageCentre);
                    }
                }
                public void CheckKey()
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (GameStates.IsKeyPressed120f(UpdateKeys[i]))
                        {
                            Rotate(i);
                        }
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        if (GameStates.IsKeyDown(UpdateKeys[i]))
                        {
                            Hold(i);
                        }
                    }
                }
            }

            public ShieldManager Shields { get; internal set; }
            public class ShieldManager : Entity
            {
                internal void Rotate(int col, int dir)
                {
                    if (shields.ContainsKey(col))
                        shields[col].Rotate(dir);
                }
                /// <summary>
                /// 两个盾牌。0蓝1红
                /// </summary>
                private readonly Dictionary<int, Shield> shields = new();

                int[] oldDirections = new int[4];
                public ShieldManager()
                {
                    oldDirections[0] = oldDirections[1] = oldDirections[2] = oldDirections[3] = 0;
                    UpdateIn120 = true;
                }
                public Shield RShield { get; private set; }
                public Shield BShield { get; private set; }
                public Shield GShield { get; private set; }
                public Shield PShield { get; private set; }
                public ShieldCircle Circle { get; private set; }
                public override void Start()
                {
                    Heart mission = FatherObject as Heart;
                    Shield bshield = new(0, mission) { UpdateKeys = new InputIdentity[] { InputIdentity.MainRight, InputIdentity.MainDown, InputIdentity.MainLeft, InputIdentity.MainUp } };
                    Shield rshield = new(1, mission) { UpdateKeys = new InputIdentity[] { InputIdentity.SecondRight, InputIdentity.SecondDown, InputIdentity.SecondLeft, InputIdentity.SecondUp } };
                    Shield pshield = new(2, mission) { UpdateKeys = new InputIdentity[] { InputIdentity.FourthRight, InputIdentity.FourthDown, InputIdentity.FourthLeft, InputIdentity.FourthUp } };
                    Shield gshield = new(3, mission) { UpdateKeys = new InputIdentity[] { InputIdentity.ThirdRight, InputIdentity.ThirdDown, InputIdentity.ThirdLeft, InputIdentity.ThirdUp } };
                    shields.Add(0, BShield = bshield);
                    shields.Add(1, RShield = rshield);
                    //shields.Add(2,pshield);
                    //shields.Add(3,gshield);
                    AddChild(bshield);
                    AddChild(rshield);
                    //AddChild(pshield);
                    //AddChild(gshield);
                    Circle = new();
                    AddChild(Circle);

                    //this.AddChildObject(pshield);
                    //this.AddChildObject(gshield); 
                }

                public void Push(GreenSoulGB greenSoulGB, int shieldID)
                {
                    shields[shieldID].Push(greenSoulGB);
                }
                public int DirectionOf(int shieldID)
                {
                    return shields[shieldID].Way;
                }
                public bool Exist(int color)
                {
                    return shields.ContainsKey(color);
                }
                public bool InSameDir(int color, int dir)
                {
                    return shields[color].AttachingGB ? shields[color].Way == dir : oldDirections[color] == dir;
                }
                public bool AttachedGB(int color)
                {
                    return shields[color].AttachingGB;
                }
                public int LastDirectionOf(int shieldID)
                {
                    return shields[shieldID].lastWay;
                }
                public override void Update()
                {
                    Heart mission = FatherObject as Heart;
                    this.Circle.controlLayer = this.controlLayer;
                    foreach (var v in shields) v.Value.controlLayer = this.controlLayer;
                    if (mission.enabledRedShield && mission.SoulType != 1)
                    {
                        shields[1].enabled = true; shields[0].enabled = false;
                        shields[1].CheckKey();
                    }
                    else if (mission.SoulType == 1)
                    {
                        foreach (var v in shields)
                        {
                            Shield s = v.Value;
                            s.enabled = true;
                            s.CheckKey();
                        }
                    }
                    else
                    {
                        foreach (var v in shields)
                        {
                            Shield s = v.Value;
                            s.enabled = false;
                        }
                    }
                }

                public float RotationOf(int colorType)
                {
                    return shields[colorType].Rotation;
                }

                public float PushDelta(int color)
                {
                    return shields[color].PushDelta;
                }

                public void ShieldShine(int direction, int color, int score)
                {
                    if (score != 0)
                        shields[color].CreateShinyEffect(score, direction);
                    Circle.CheckScore(score);
                    oldDirections[color] = direction;
                }

                public void AddShield(Shield shield)
                {
                    if (shields.ContainsKey(shield.ColorType)) return;
                    shields.Add(shield.ColorType, shield);
                    AddChild(shield);
                }
                public void RemoveShield(Shield shield)
                {
                    ChildObjects.Remove(shield);
                    shields.Remove(shield.ColorType);
                }
                public void MakeShieldParticle(Color col, float ang)
                {
                    Heart mission = FatherObject as Heart;
                    Vector2 createCentre = mission.Centre + MathUtil.GetVector2(33, ang - 180 + Functions.Heart.Rotation);
                    for (int i = 0; i < Rand(5, 20); i++)
                    {
                        float rotation1 = ang + 90 + Rand(0, 1) * 180;
                        float rdelta = Rand(0, 1f) * Rand(0, 1f) * Rand(0, 1f) * RandSignal();
                        rotation1 += rdelta * 90;
                        GameStates.InstanceCreate(new Particle(col * Rand(0.67f, 0.85f), MathUtil.GetVector2(Rand(5f, 9f), rotation1), Rand(4, 8), createCentre, FightResources.Sprites.square)
                        { DarkingSpeed = Rand(12f, 16f), SlowLerp = 0.25f });
                    }
                }

                public override void Draw()
                {
                }

                public bool OverRotate => RotateConsumption > 8.001f;
                public float RotateConsumption { get; private set; } = 0;
                private void UpdateCircle()
                {
                    Circle.Consumption = RotateConsumption / 8f;
                }
                internal void ValidRotated()
                {
                    RotateConsumption = MathF.Max(RotateConsumption - 1, 0);
                    UpdateCircle();
                }
                internal void Consume()
                {
                    Consume(1f);
                }
                internal void Consume(float v)
                {
                    RotateConsumption -= v * 0.25f;
                    UpdateCircle();
                }
                internal void ShieldRotated()
                {
                    RotateConsumption++;
                    UpdateCircle();
                }

                public Shield.CollisionSide GetCollideChecker(int arrowColor)
                {
                    return shields[arrowColor].CollisionChecker;
                }
            }

            public class ShieldCircle : Entity
            {
                public ShieldCircle()
                {
                    Image = FightResources.Sprites.ShieldCircle;
                    Depth = 0.4f;
                    UpdateIn120 = true;
                }
                private Color DrawingColor
                {
                    get =>
                       type switch
                       {
                           0 => Color.White,
                           1 => Color.Lime,
                           2 => Color.LightBlue,
                           3 => Color.Orange,
                           4 => Color.Gold,
                           _ => throw new ArgumentOutOfRangeException()
                       };
                }
                public float Consumption { get; set; } = 0.0f;

                private float drawConsumption = 0.0f;

                public override void Draw()
                {
                    if (enabled)
                    {
                        FormalDraw(Image, Centre, curColor * 0.6f * (FatherObject.FatherObject as Heart).Alpha, Rotation, ImageCentre);
                        float scale = MathF.Min(1, drawConsumption);
                        if (drawConsumption > 0.004f)
                            FormalDraw(Image,
                                Centre + new Vector2(0, Image.Height * (1 - scale)),
                                new CollideRect(0, Image.Height * (1 - scale), Image.Width,
                                Image.Height * scale).ToRectangle(),
                                Color.Red * 0.8f, Rotation, ImageCentre);
                    }
                }

                private bool enabled = true;
                private Color curColor = Color.Gold;
                public override void Update()
                {
                    enabled = (FatherObject.FatherObject as Heart).SoulType == 1;
                    Centre = (FatherObject.FatherObject as Heart).Centre;
                    curColor = Color.Lerp(curColor, DrawingColor, 0.1f);
                    drawConsumption = drawConsumption * 0.9f + Consumption * 0.1f;
                }

                private int type = 4;
                internal void CheckScore(int score)
                {
                    if (score == 3) return;
                    type = score >= 4 ? Min(type, 3) : score == 2 ? Min(type, 2) : score == 1 ? Min(type, 1) : Min(type, 0);
                }
            }
        }
    }
}