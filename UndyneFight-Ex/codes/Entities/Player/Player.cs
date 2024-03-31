using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static UndyneFight_Ex.Fight.Functions;

namespace UndyneFight_Ex.Entities
{
    public partial class Player : Entity
    {
        #region 组件
        public class CollideEffect : Entity
        {
            private readonly float size;
            private readonly Color color;
            private float light = 1.0f;

            public CollideEffect(Color color, float size)
            {
                controlLayer = Surface.Hidden;
                this.size = (size + 16) / 16f;
                this.color = color;
                Image = FightResources.Sprites.soulCollide;
                UpdateIn120 = true;
                Depth = 0.4f;
            }

            public override void Draw()
            {
                FormalDraw(Image, Centre, color * light, size, MathUtil.GetRadian((FatherObject as Heart).Rotation), ImageCentre);
            }

            public override void Update()
            {
                Centre = (FatherObject as Heart).Centre;
                light -= 0.05f;
                if (light < 0)
                {
                    Dispose();
                }
            }
        }
        #endregion

        public static Heart heartInstance;
        public static List<Heart> hearts = new();

        public Player()
        {
            hpControl = new();
            hearts = new();

            heartInstance = new();
            AddChild(hpControl);
            AddChild(heartInstance);
        }

        public partial class Heart : Entity
        {
            public class PurpleFiller : Entity
            {
                private readonly Heart user;
                public PurpleFiller(int lineCount, Heart player)
                {
                    controlLayer = Surface.Hidden;
                    user = player;
                    this.lineCount = lineCount;
                    appearTime = 0;
                }

                private int appearTime = 0;
                private readonly int halfTime = 14;
                private float percent = 0;
                private readonly int lineCount = 0;

                public override void Draw()
                {
                    if (appearTime < halfTime)
                    {
                        var v = user.controlingBox.CollidingBox;
                        FormalDraw(FightResources.Sprites.pixUnit, new CollideRect(v.X, v.Y, v.Width, v.Height * percent).ToRectangle(), Color.MediumPurple);
                    }
                    else
                    {
                        var v = user.controlingBox.CollidingBox;
                        FormalDraw(FightResources.Sprites.pixUnit, new CollideRect(v.X, v.Down - (v.Height * percent), v.Width, v.Height * percent).ToRectangle(), Color.MediumPurple);
                    }
                }

                public override void Update()
                {
                    appearTime++;
                    if (appearTime < halfTime)
                    {
                        percent = (percent * 0.85f) + 0.15f;
                    }
                    else if (appearTime == halfTime)
                    {
                        Change();
                    }
                    else
                    {
                        percent *= 0.86f;
                    }
                    if (appearTime == halfTime * 3)
                    {
                        Dispose();
                    }
                }

                private void Change()
                {
                    user.lastChangeTime = 0;

                    if (lineCount == -1)
                    {
                        return;
                    }

                    int last = user.purpleLineCount;
                    user.purpleLineCount = lineCount;
                    int detla = (Move.currentLine - last) / 2;
                    Move.currentLine += detla;

                    user.purpleLineCount = lineCount;
                }
            }

            #region 基本性质

            public FightBox controlingBox;

            /// <summary>
            /// 灵魂状态。0表示红，1表示绿，2表示蓝，3表示橙，4表示紫，5表示灰
            /// </summary>
            public int SoulType { get; private set; }

            private Vector2 lastCentre;

            private bool isMoved, isStable;
            private int lastChangeTime = 0;

            #endregion

            #region 各项属性

            public int ID { get; init; }
            public Vector2 LastCentre => lastCentre;

            public bool SoftFalling { get; set; }
            public bool FixArrow { get; set; } = false;

            public float Alpha { get; set; } = 1.0f;

            private float speed = 2.5f;
            public float Speed
            {
                set
                {
                    speed = value;
                }
                get
                {
                    return speed;
                }
            }

            private int jumpTimeLimit = 2;
            public int JumpTimeLimit
            {
                set
                {
                    jumpTimeLeft = jumpTimeLimit = value;
                }
                private get
                {
                    return jumpTimeLimit;
                }
            }

            private float jumpSpeed = 6f;
            public float JumpSpeed
            {
                private get => jumpSpeed; set
                {
                    jumpSpeed = value;
                }
            }

            public float Gravity
            {
                private get
                {
                    return gravity;
                }
                set
                {
                    gravity = value;
                }
            }

            public int YFacing
            {
                get
                {
                    int jumpKey = 1;
                    missionRotation += 360;
                    missionRotation %= 360;
                    if (missionRotation >= 45 && missionRotation < 135)
                    {
                        jumpKey = 2;
                    }
                    else if (missionRotation >= 135 && missionRotation < 225)
                    {
                        jumpKey = 3;
                    }
                    else if (missionRotation >= 225 && missionRotation < 315)
                    {
                        jumpKey = 0;
                    }

                    return jumpKey;
                }
            }
            public int XFacing
            {
                get
                {
                    int jumpKey = 0;
                    missionRotation += 360;
                    missionRotation %= 360;
                    if (missionRotation >= 45 && missionRotation < 135)
                    {
                        jumpKey = 1;
                    }
                    else if (missionRotation >= 135 && missionRotation < 225)
                    {
                        jumpKey = 2;
                    }
                    else if (missionRotation >= 225 && missionRotation < 315)
                    {
                        jumpKey = 3;
                    }

                    return jumpKey;
                }
            }

            public bool IsMoved => isMoved;
            public bool IsStable => isStable;

            public float UmbrellaSpeed
            {
                set => umbrellaSpeed = value;
            }
            private float umbrellaSpeed = 1f;

            private int purpleLineCount = 3;
            public int PurpleLineCount
            {
                set
                {
                    GameStates.InstanceCreate(new PurpleFiller(value, this));
                }
                get
                {
                    return purpleLineCount;
                }
            }

            private bool enabledRedShield = false;
            public bool EnabledRedShield
            {
                set => enabledRedShield = value;
            }

            public bool IsSoulSplit
            {
                set
                {
                    if (value)
                    {
                        _ = new Player();
                        soulSplitTime = 0;
                        isSoulSplit = true;
                    }
                    else
                    {
                        heartInstance = hearts[0];
                        hearts.Clear();
                        hearts.Add(heartInstance);
                    }
                }
                get
                {
                    return isSoulSplit;
                }
            }

            public bool UmbrellaAvailable
            {
                set
                {
                    umbrellaAvailable = value;
                }
            }

            public InputIdentity[] movingKey = new InputIdentity[4] { InputIdentity.MainRight, InputIdentity.MainDown, InputIdentity.MainLeft, InputIdentity.MainUp };

            private bool isOranged = false;
            public bool IsOranged
            {
                set
                {
                    isOranged = value;
                    if (value)
                    {
                        ResetOrange();
                    }
                }
            }
            #endregion

            #region 被属性控制的私字段

            private float gravity = 9.8f, gravitySpeed = 0.0f;

            public int soulSplitTime = 0;
            private bool isSoulSplit = false, umbrellaAvailable = false;

            /// <summary>
            /// 是否被重力摔了
            /// </summary>
            private bool isForced = false;
            private float forcedSpeed = 2f, purpleLineLength = 0;
            private int jumpTimeLeft = 2;

            #endregion 

            public Heart()
            {
                _currentMoveState = _red;
                controlLayer = Surface.Hidden;
                heartInstance = this;
                hearts.Add(this);

                ID = hearts.Count - 1;

                controlingBox = new RectangleBox(this);
                Image = FightResources.Sprites.player;
                collidingBox.Size = new Vector2(16, 16);
                Centre = new Vector2(300, 300);
                Depth = 0.3f;
                UpdateIn120 = true;
            }

            public override void Start()
            {
                AddChild(Shields = new ShieldManager());
                GameStates.InstanceCreate(controlingBox);

                Player manager = FatherObject as Player;
                manager.GameAnalyzer.PushData(new SoulListData(ID, true, GametimeF));

                manager.GameAnalyzer.PushData(new SoulChangeData(SoulType, ID, GametimeF));
            }
            public override void Dispose()
            {
                Player manager = FatherObject as Player;
                manager.GameAnalyzer.PushData(new SoulListData(ID, false, GametimeF));
                controlingBox.Dispose();
                base.Dispose();
            }

            /// <summary>
            /// 将此玩家灵魂和另外一个合并，此灵魂消失
            /// </summary>
            /// <param name="another">另外一个</param>
            public void Merge(Heart another)
            {
                mergeTime = 0;
                mergeMission = another;
                (FatherObject as Player).hpControl.GiveProtectTime(32);
            }
            /// <summary>
            /// 将所有玩家灵魂合并
            /// </summary>
            public void MergeAll()
            {
                for (int i = 1; i < hearts.Count; i++)
                    hearts[i].Merge(hearts[i - 1]);
            }

            private int mergeTime;
            private int appearTime = 0;
            private Heart mergeMission;

            public Heart Split()
            {
                Heart v = new()
                {
                    speed = speed,
                    jumpTimeLimit = jumpTimeLimit,
                    SoulType = SoulType,
                    purpleLineCount = purpleLineCount,
                    gravity = gravity,
                    jumpSpeed = jumpSpeed,
                    umbrellaAvailable = umbrellaAvailable,
                    umbrellaSpeed = umbrellaSpeed,
                    Centre = Centre,
                    Alpha = Alpha,
                    _currentMoveState = _currentMoveState,
                };
                v.controlingBox.InstanceMove(controlingBox.CollidingBox);

                soulSplitTime = 0;
                isSoulSplit = true;

                (FatherObject as Player).AddChild(v);

                return v;
            }
            public Heart InstantSplit(CollideRect area)
            {
                Heart v = new()
                {
                    speed = speed,
                    jumpTimeLimit = jumpTimeLimit,
                    SoulType = SoulType,
                    purpleLineCount = purpleLineCount,
                    gravity = gravity,
                    jumpSpeed = jumpSpeed,
                    umbrellaAvailable = umbrellaAvailable,
                    umbrellaSpeed = umbrellaSpeed,
                    Centre = area.GetCentre(),
                    Alpha = Alpha,
                    _currentMoveState = _currentMoveState
                };
                v.controlingBox.InstanceMove(area);

                (FatherObject as Player).AddChild(v);

                return v;
            }

            internal void DrawHeart()
            {
                if (!Fight.FightStates.finishSelecting)
                {
                    return;
                }

                Depth = 0.3f;
                int protectTime = (FatherObject as Player).hpControl.protectTime;
                Color drawingColor = isOranged ? Color.Orange : _currentMoveState.StateColor;
                if (protectTime > 0)
                {
                    FormalDraw(Image, Centre, drawingColor * ((protectTime % 30) > 8 ? 0.6f : 1.0f) * Alpha, MathUtil.GetRadian(Rotation), ImageCentre);
                }
                else
                {
                    FormalDraw(Image, Centre, drawingColor * Alpha, MathUtil.GetRadian(Rotation), ImageCentre);
                }

                if (SoulType == 4)
                {
                    int count = PurpleLineCount + 1;
                    float detla = controlingBox.CollidingBox.Height / count;
                    for (int i = 1; i < count; i++)
                    {
                        RectangleBox box = controlingBox as RectangleBox;
                        DrawingLab.DrawLine(new Vector2(box.Centre.X, (i * detla) + box.Up),
                            0, purpleLineLength, 3, Color.MediumPurple, 0.1f);
                    }
                }
            }

            public override void Update()
            {
                GravityLine.Recover();

                if (mergeMission != null)
                {
                    DoMerge();
                }

                lastCentre = Centre;
                lastChangeTime++;
                appearTime++;
                if (isSoulSplit)
                {
                    soulSplitTime++;
                }

                if (SoulType != 4)
                {
                    purpleLineLength *= 0.84f;
                    Centre += positionRest * 0.25f;
                    positionRest *= 0.75f;
                }
                else
                {
                    Centre += positionRest * 0.3f;
                    positionRest *= 0.7f;
                }

                float rotateDelta = GetRotateDelta();

                Rotation += rotateDelta * 0.3f * (rotateWay ? 1 : -1);

                if (!Fight.FightStates.roundType)
                {
                    _currentMoveState.MoveFunction.Invoke(this);
                }

                if (isOranged)
                {
                    if (Gametime % 2 == 1)
                    {
                        GameStates.InstanceCreate(new RetentionEffect(this, 15, Color.Orange)
                        { AngleMode = true });
                    }
                }

                isMoved = (lastCentre - Centre).Length() >= 0.005f;
                isStable = (lastCentre - Centre).Length() <= 0.5f;
            }

            private void ResetOrange()
            {
                if (SoulType == 2)
                {
                    jumpTimeLeft = 0;
                    gravitySpeed = 0;
                }
                Move.blueLastWay = false;
                Move.last = -1;
            }

            /// <summary>
            /// 改变颜色。
            /// </summary>
            /// <param name="type">灵魂状态。0表示红，1表示绿，2表示蓝，3表示橙，4表示紫，5表示灰</param>
            public void ChangeColor(int type)
            {
                isOranged = type == 3;
                lastChangeTime = 0;
                switch (type)
                {
                    case 2:
                        jumpTimeLeft = JumpTimeLimit;
                        break;
                    case 3:
                        ResetOrange();
                        type = 0;
                        break;
                    case 4:
                        GameStates.InstanceCreate(new PurpleFiller(-1, this));
                        break;
                }
                _currentMoveState = type switch
                {
                    0 => _red,
                    1 => _green,
                    2 => _blue,
                    3 => _red,
                    4 => _purple,
                    5 => _gray,
                    _ => throw new ArgumentOutOfRangeException(nameof(type)),
                };
                SoulType = type;
                CreateShinyEffect(_currentMoveState.StateColor);
                Player manager = FatherObject as Player;
                manager.GameAnalyzer.PushData(new SoulChangeData(SoulType, ID, GametimeF));
            }

            #region 缓动

            private Vector2 positionRest;
            private Vector2 lastBoxCentre;
            private float missionRotation;

            private void DoMerge()
            {
                float v = 0.78f;
                float v2 = 1 - v;
                Centre = (Centre * v) + (mergeMission.Centre * v2);
                var cl = controlingBox.CollidingBox;
                for (int i = 0; i < controlingBox.Vertexs.Length; i++)
                {
                    controlingBox.Vertexs[i].CurrentPosition = (controlingBox.Vertexs[i].CurrentPosition * v) + (mergeMission.controlingBox.Vertexs[i].MissionPosition * v2);
                }

                controlingBox.InstanceMove(new CollideRect(controlingBox.Vertexs[0].CurrentPosition, controlingBox.Vertexs[2].CurrentPosition - controlingBox.Vertexs[0].CurrentPosition));
                mergeTime++;
                if (mergeTime == 25)
                {
                    Dispose();
                }
            }

            /// <summary>
            /// 旋转方向，true则代表顺时针
            /// </summary>
            private bool rotateWay;

            private float GetRotateDelta()
            {
                float trueRot = (Rotation + 90) % 360;
                float trueMission = (missionRotation + 90) % 360;
                return Math.Min((trueMission - trueRot + 360) % 360, (360 - trueMission + trueRot) % 360);
            }

            public void InstantSetRotation(float rot)
            {
                Rotation = missionRotation = rot;
            }

            public void RotateTo(float rot)
            {
                float trueRot = (Rotation + 90) % 360;
                missionRotation = rot;
                float trueMission = (missionRotation + 90) % 360;
                rotateWay = (trueMission - trueRot + 360) % 360 < (360 - trueMission + trueRot) % 360;
            }
            public void GiveForce(float rotation, float speed)
            {
                forcedSpeed = speed;
                isForced = true;
                jumpTimeLeft = 0;
                RotateTo(rotation);
                gravitySpeed = speed;
                GravityLine.Reload();
                heartInstance.Centre += MathUtil.GetVector2(heartInstance.gravitySpeed, missionRotation + 90);
            }
            public void GiveInstantForce(float rotation, float speed)
            {
                forcedSpeed = speed;
                isForced = true;
                jumpTimeLeft = 0;
                InstantSetRotation(rotation);
                gravitySpeed = speed;
                GravityLine.Reload();
                heartInstance.Centre += MathUtil.GetVector2(heartInstance.gravitySpeed, missionRotation + 90);
            }

            public void Teleport(Vector2 mission)
            {
                positionRest = mission - Centre;
            }
            public void InstantTP(Vector2 mission)
            {
                Centre = mission;
                if (Shields != null)
                {
                    Shields.Circle.Centre = mission;
                    Shields.RShield.Centre = mission;
                    Shields.BShield.Centre = mission;
                }
            }

            public static void ResetMove()
            {
                Move.last = -1;
                Move.currentLine = 0;
                Move.blueLastWay = false;
            }
            public void FollowScreen(float duration)
            {
                AddInstance(new TimeRangedEvent(duration, () =>
                {
                    InstantSetRotation(ScreenDrawing.ScreenAngle);
                })
                { UpdateIn120 = true });
            }
            #endregion

            public override void Draw()
            {
                DrawHeart();
#if DEBUG
                GlobalResources.Font.NormalFont.CentreDraw(jumpTimeLeft + "/" + jumpTimeLimit, new Vector2(320, 150), Color.Gray);
                DrawingLab.DrawVector(Centre, MathUtil.GetRadian(Rotation + 90));
#endif 
            }
            public void CreateCollideEffect2(Color color, float size)
            {
                AddChild(new CollideEffect(color, size * 1.5f));
            }
        }

        public override void Update()
        {
            hearts.RemoveAll(s => s.Disposed);
            if (hearts.Count == 0) heartInstance = null;
            else if (heartInstance == null || heartInstance.Disposed) heartInstance = hearts[0];
        }
        public override void Draw()
        {
        }
        public override void Dispose()
        {
            base.Dispose();
        }

        #region 血量控制 

        public HPControl hpControl { get; private set; }

        #endregion

        public static void CreateCollideEffect(Color color, float size)
        {
            heartInstance.AddChild(new CollideEffect(color, size * 1.5f));
        }
    }
}