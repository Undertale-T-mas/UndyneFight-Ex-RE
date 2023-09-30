using Microsoft.Xna.Framework;
using System;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public partial class Player
    {
        public partial class Heart
        {
            private static class Move
            {
                public static Heart mission;
                private static bool Slow => IsKeyDown(InputIdentity.Cancel);

                private static InputIdentity[] Keys_ => mission.movingKey;// { Keys.Right, Keys.Down, Keys.Left, Keys.Up };

                public static int last = -1;

                public static bool blueLastWay = false;

                public static int currentLine = 0;

                public static void MoveAsPurple()
                {
                    RectangleBox _curBox = mission.controlingBox as RectangleBox;

                    if (mission.lastChangeTime >= 3)
                    {
                        mission.purpleLineLength = _curBox.CollidingBox.Width;

                        mission.Centre += _curBox.Centre - mission.lastBoxCentre;
                    }

                    Vector2 moving = Vector2.Zero;

                    if (IsKeyDown(Keys_[2]))
                        moving = new Vector2(-1, 0);

                    if (IsKeyDown(Keys_[0]))
                        moving = new Vector2(1, 0);

                    if (IsKeyPressed120f(Keys_[3]) && currentLine > 1)
                        currentLine--;
                    if (IsKeyPressed120f(Keys_[1]) && currentLine < mission.purpleLineCount)
                        currentLine++;

                    currentLine = Math.Clamp(currentLine, 1, mission.purpleLineCount);

                    int count = mission.purpleLineCount + 1;
                    float detla = _curBox.CollidingBox.Height / count;

                    Vector2 detla2 = new(0, detla * currentLine + _curBox.Up - mission.Centre.Y);
                    mission.positionRest.Y = detla2.Y;

                    if (moving != Vector2.Zero)
                    {
                        moving.Normalize();
                        if (Slow)
                            moving *= 0.5f;

                        moving *= mission.speed;
                    }

                    mission.collidingBox.Offset(moving * 0.5f);

                    if (mission.collidingBox.X < _curBox.Left)
                        mission.collidingBox.X = _curBox.Left;
                    else if (mission.collidingBox.Right > _curBox.Right)
                        mission.collidingBox.X = _curBox.Right - 16;
                    mission.lastBoxCentre = _curBox.Centre;
                }

                public static void MoveAsOrange()
                {
                    Vector2 moving = Vector2.Zero;

                    if (IsKeyDown(Keys_[0]) && IsKeyDown(Keys_[1]))
                        last = 4;
                    else if (IsKeyDown(Keys_[2]) && IsKeyDown(Keys_[1]))
                        last = 5;
                    else if (IsKeyDown(Keys_[2]) && IsKeyDown(Keys_[3]))
                        last = 6;
                    else if (IsKeyDown(Keys_[0]) && IsKeyDown(Keys_[3]))
                        last = 7;
                    else if (IsKeyDown(Keys_[2]))
                        last = 2;
                    else if (IsKeyDown(Keys_[0]))
                        last = 0;
                    else if (IsKeyDown(Keys_[3]))
                        last = 3;
                    else if (IsKeyDown(Keys_[1]))
                        last = 1;

                    switch (last)
                    {
                        case 0:
                            moving = GetVector2(1, 0);
                            break;
                        case 1:
                            moving = GetVector2(1, 90);
                            break;
                        case 2:
                            moving = GetVector2(1, 180);
                            break;
                        case 3:
                            moving = GetVector2(1, 270);
                            break;
                        case 4:
                            moving = GetVector2(1, 45);
                            break;
                        case 5:
                            moving = GetVector2(1, 135);
                            break;
                        case 6:
                            moving = GetVector2(1, 225);
                            break;
                        case 7:
                            moving = GetVector2(1, 315);
                            break;

                        case -1:
                            moving = Vector2.Zero;
                            break;
                    }

                    if (moving != Vector2.Zero)
                    {
                        moving.Normalize();
                        if (Slow)
                            moving *= 0.5f;

                        moving *= mission.speed;
                    }

                    mission.collidingBox.Offset(moving * 0.5f);
                    RectangleBox _curBox = mission.controlingBox as RectangleBox;
                    if (mission.collidingBox.X < _curBox.Left)
                        mission.collidingBox.X = _curBox.Left;
                    else if (mission.collidingBox.Right > _curBox.Right)
                        mission.collidingBox.X = _curBox.Right - 16;
                    if (mission.collidingBox.Y < _curBox.Up)
                        mission.collidingBox.Y = _curBox.Up;
                    else if (mission.collidingBox.Down > _curBox.Down)
                        mission.collidingBox.Y = _curBox.Down - 16;
                }

                public static void MoveAsRed()
                {
                    RectangleBox _curBox = mission.controlingBox as RectangleBox;
                    Vector2 moving = Vector2.Zero;

                    if (IsKeyDown(Keys_[2]))
                        moving.X--;

                    if (IsKeyDown(Keys_[0]))
                        moving.X++;

                    if (IsKeyDown(Keys_[3]))
                        moving.Y--;

                    if (IsKeyDown(Keys_[1]))
                        moving.Y++;

                    if (moving != Vector2.Zero)
                    {
                        moving.Normalize();
                        if (Slow)
                            moving *= 0.5f;

                        moving *= mission.speed;
                    }

                    Vector2 finalMoving = GetVector2(moving.Length(), (float)Math.Atan2(moving.Y, moving.X) / PI * 180 + mission.missionRotation);

                    mission.collidingBox.Offset(finalMoving * 0.5f);
                    if (mission.collidingBox.X < _curBox.Left)
                        mission.collidingBox.X = _curBox.Left;
                    else if (mission.collidingBox.Right > _curBox.Right)
                        mission.collidingBox.X = _curBox.Right - 16;
                    if (mission.collidingBox.Y < _curBox.Up)
                        mission.collidingBox.Y = _curBox.Up;
                    else if (mission.collidingBox.Down > _curBox.Down)
                        mission.collidingBox.Y = _curBox.Down - 16;
                }

                public static void MoveAsBlue()
                {
                    RectangleBox _curBox = mission.controlingBox as RectangleBox;
                    float trueRot = (mission.missionRotation + 90) % 360;

                    int jumpKey = 2;
                    if (trueRot > 45 && trueRot < 135)
                    {
                        jumpKey = 3;
                    }
                    else if (trueRot > 135 && trueRot < 225)
                    {
                        jumpKey = 0;
                    }
                    else if (trueRot > 225 && trueRot < 315)
                    {
                        jumpKey = 1;
                    }

                    int XWay = 0;
                    if (IsKeyDown(Keys_[(jumpKey + 1) % 4]))
                        XWay++;

                    if (IsKeyDown(Keys_[(jumpKey + 3) % 4]))
                        XWay--;

                    float moving = 1.0f;
                    if (Slow)
                        moving *= 0.5f;

                    Vector2 oldCentre = mission.Centre;

                    float xMoved = mission.speed * XWay * moving;

                    mission.Centre += GetVector2(xMoved * 0.5f, mission.missionRotation);

                    bool res = false;
                    if (mission.collidingBox.X < _curBox.Left)
                    {
                        res = mission.YFacing == 2 && mission.collidingBox.X < _curBox.Left - 0.029f;
                        mission.collidingBox.X = _curBox.Left;
                        oldCentre.X = mission.Centre.X;
                    }
                    else if (mission.collidingBox.Right > _curBox.Right)
                    {
                        res = mission.YFacing == 0 && mission.collidingBox.Right > _curBox.CollidingBox.Right + 0.029f;
                        mission.collidingBox.X = _curBox.Right - 16;
                        oldCentre.X = mission.Centre.X;
                    }
                    if (mission.collidingBox.Y < _curBox.Up)
                    {
                        res = mission.YFacing == 3 && mission.collidingBox.Up < _curBox.CollidingBox.Up - 0.029f;
                        mission.collidingBox.Y = _curBox.Up;
                        oldCentre.Y = mission.Centre.Y;
                    }
                    else if (mission.collidingBox.Down > _curBox.Down)
                    {
                        res = mission.YFacing == 1 && mission.collidingBox.Down > _curBox.CollidingBox.Down + 0.029f;
                        mission.collidingBox.Y = _curBox.Down - 16;
                        oldCentre.Y = mission.Centre.Y;
                    }
                    if (res && mission.isForced)
                    {
                        float v = mission.forcedSpeed;
                        if (v >= 3)
                        {
                            Fight.Functions.PlaySound(FightResources.Sounds.slam, Math.Min(1, MathF.Sqrt(v - 1) / 3f));
                            InstanceCreate(new Advanced.ScreenShaker((int)Math.Ceiling(Math.Sqrt(v - 2) * 1.33f), 4 + MathF.Sqrt(v * 1.33f + 1) * 1.56f, 3, trueRot));
                        }
                        mission.isForced = false;
                    }

                    float final = 0;
                    Vector2 adapt = Vector2.Zero;

                    foreach (var v in GravityLine.GravityLines)
                    {
                        if (v.IsCollideWith(mission) && mission.gravitySpeed >= 0f)
                        {
                            float rot;
                            rot = v.Rotation / PI * 180 % 180.01f;

                            final = rot;
                            res = true;

                            adapt = v.CorrectPosition(mission);

                            break;
                        }
                    }
                    if (res)
                    {
                        mission.isForced = false;
                        if (mission.gravitySpeed >= 0)
                        {
                            mission.gravitySpeed = 0f;
                            float playerXaxisDir = mission.XFacing * 90;
                            float best = (final + 360) % 360, bestAngle = RotationDist(best, playerXaxisDir);
                            for (int i = 1; i < 4; i++)
                            {
                                float cur = final + i * 90;
                                float curAngle = RotationDist(cur, playerXaxisDir);
                                if (curAngle < bestAngle)
                                {
                                    best = cur;
                                    bestAngle = curAngle;
                                }
                            }
                            mission.Centre = oldCentre + GetVector2(xMoved * 0.5f, final) + adapt;
                        }
                        mission.jumpTimeLeft = mission.JumpTimeLimit;
                    }
                    else
                    {
                        if (mission.jumpTimeLeft == mission.jumpTimeLimit && mission.gravitySpeed >= 1f)
                            mission.jumpTimeLeft = mission.jumpTimeLimit - 1;

                        if (mission.SoftFalling)
                        {
                            mission.gravitySpeed += (mission.gravitySpeed >= 0 && mission.gravitySpeed <= mission.gravity / 5f)
                                ? MathHelper.Lerp(0.5f, 1.0f, mission.gravitySpeed / (mission.gravity / 5f)) * mission.gravity * 0.01f
                            : mission.gravity / 100;
                        }
                        else
                        {
                            mission.gravitySpeed += mission.gravity / 100;
                        }
                    }
                    Vector2 ori = mission.Centre;
                    mission.Centre += GetVector2(mission.gravitySpeed * 0.5f, trueRot);

                    bool jumpKeyDown = IsKeyDown(Keys_[jumpKey]) &&
                        ((!IsKeyDown(Keys_[(jumpKey + 2) % 4])) || hearts.Count >= 2);

                    if (jumpKeyDown)
                    {
                        if ((res || IsKeyPressed120f(Keys_[jumpKey])) && mission.jumpTimeLeft > 0)
                        {
                            mission.jumpTimeLeft--;
                            mission.gravitySpeed = -mission.jumpSpeed;
                        }
                    }
                    else if (mission.gravitySpeed < 0)
                    {
                        if (!mission.SoftFalling)
                        {
                            if (mission.gravitySpeed < -1.0f)
                            {
                                float next = mission.gravity / 1.6f;
                                if (next > -1.0f)
                                    next = -0.6f;

                                mission.gravitySpeed = next;
                            }
                        }
                        else
                        {
                            if (mission.gravitySpeed < -0.4f)
                            {
                                float next = mission.gravity / 2.4f;
                                if (next > -0.4f)
                                    next = -0.35f;

                                mission.gravitySpeed = next;
                            }
                            mission.Centre = ori + GetVector2(mission.gravitySpeed * 0.5f, trueRot);
                        }
                    }
                    if (mission.gravitySpeed > 0)
                        if (mission.umbrellaAvailable && IsKeyDown(InputIdentity.Alternate) && (!mission.isForced))
                            mission.gravitySpeed = mission.gravitySpeed * 0.85f + 0.667f * 0.15f;
                }

                public static void MoveAsGray()
                {
                    RectangleBox _curBox = mission.controlingBox as RectangleBox;
                    float trueRot = (mission.missionRotation + 90) % 360;
                    float strength = 1.8f;

                    if (IsKeyPressed120f(Keys_[0]))
                    {
                        mission.GiveForce(-90, strength);
                    }
                    if (IsKeyPressed120f(Keys_[1]))
                    {
                        mission.GiveForce(0, strength);
                    }
                    if (IsKeyPressed120f(Keys_[2]))
                    {
                        mission.GiveForce(90, strength);
                    }
                    if (IsKeyPressed120f(Keys_[3]))
                    {
                        mission.GiveForce(180, strength);
                    }

                    bool res = false;
                    if (mission.collidingBox.X < _curBox.Left)
                    {
                        res = mission.YFacing == 2 && mission.collidingBox.X < _curBox.Left - 0.9f;

                        mission.collidingBox.X = _curBox.Left;
                    }
                    else if (mission.collidingBox.Right > _curBox.Right)
                    {
                        res = mission.YFacing == 0 && mission.collidingBox.Right > _curBox.CollidingBox.Right + 0.9f;

                        mission.collidingBox.X = _curBox.Right - 16;
                    }
                    if (mission.collidingBox.Y < _curBox.Up)
                    {
                        res = mission.YFacing == 3 && mission.collidingBox.Up < _curBox.CollidingBox.Up - 0.9f;

                        mission.collidingBox.Y = _curBox.Up;
                    }
                    else if (mission.collidingBox.Down > _curBox.Down)
                    {
                        res = mission.YFacing == 1 && mission.collidingBox.Down > _curBox.CollidingBox.Down + 0.9f;

                        mission.collidingBox.Y = _curBox.Down - 16;
                    }

                    foreach (var v in GravityLine.GravityLines)
                    {
                        if (v.IsCollideWith(mission) && mission.gravitySpeed >= 0f)
                        {
                            res = true;
                            break;
                        }
                    }

                    if (res)
                    {
                        mission.gravitySpeed = 0f;
                        mission.jumpTimeLeft = mission.JumpTimeLimit;
                    }
                    else
                    {
                        if (mission.jumpTimeLeft == mission.jumpTimeLimit)
                            mission.jumpTimeLeft = mission.jumpTimeLimit - 1;

                        mission.gravitySpeed += mission.gravity / 50f;
                    }
                    mission.Centre += GetVector2(mission.gravitySpeed, trueRot) * 0.5f;

                    if (mission.gravitySpeed > 0)
                        if (mission.umbrellaAvailable && IsKeyDown(InputIdentity.Alternate))
                            mission.gravitySpeed = mission.gravitySpeed * 0.8f + 1.0f * 0.2f;
                }

                public static void MoveAsBlueOrange()
                {
                    RectangleBox _curBox = mission.controlingBox as RectangleBox;
                    float trueRot = (mission.missionRotation + 90) % 360;

                    int jumpKey = 2;
                    if (trueRot > 45 && trueRot < 135)
                        jumpKey = 3;
                    else if (trueRot > 135 && trueRot < 225)
                        jumpKey = 0;
                    else if (trueRot > 225 && trueRot < 315)
                        jumpKey = 1;

                    int XWay = 0;
                    if (IsKeyDown(Keys_[(jumpKey + 1) % 4]))
                        blueLastWay = false;

                    if (IsKeyDown(Keys_[(jumpKey + 3) % 4]))
                        blueLastWay = true;

                    XWay -= blueLastWay ? 1 : -1;

                    float moving = 1.0f;
                    if (Slow)
                        moving *= 0.5f;

                    float xFacing = mission.XFacing * 90;

                    Vector2 oldCentre = mission.Centre;

                    float xMoved = mission.speed * XWay * moving;

                    mission.Centre += GetVector2(xMoved, mission.missionRotation);

                    bool res = false;
                    if (mission.collidingBox.X < _curBox.Left)
                    {
                        res = mission.YFacing == 2 && mission.collidingBox.X < _curBox.Left - 0.009f;
                        mission.collidingBox.X = _curBox.Left;
                        oldCentre.X = mission.Centre.X;
                    }
                    else if (mission.collidingBox.Right > _curBox.Right)
                    {
                        res = mission.YFacing == 0 && mission.collidingBox.Right > _curBox.CollidingBox.Right + 0.009f;
                        mission.collidingBox.X = _curBox.Right - 16;
                        oldCentre.X = mission.Centre.X;
                    }
                    if (mission.collidingBox.Y < _curBox.Up)
                    {
                        res = mission.YFacing == 3 && mission.collidingBox.Up < _curBox.CollidingBox.Up - 0.009f;
                        mission.collidingBox.Y = _curBox.Up;
                        oldCentre.Y = mission.Centre.Y;
                    }
                    else if (mission.collidingBox.Down > _curBox.Down)
                    {
                        res = mission.YFacing == 1 && mission.collidingBox.Down > _curBox.CollidingBox.Down + 0.009f;
                        mission.collidingBox.Y = _curBox.Down - 16;
                        oldCentre.Y = mission.Centre.Y;
                    }
                    if (res && mission.isForced)
                    {
                        InstanceCreate(new Advanced.ScreenShaker(2, 5, 3));
                        mission.isForced = false;
                    }

                    float final = 0;

                    foreach (var v in GravityLine.GravityLines)
                    {
                        if (v.IsCollideWith(mission) && mission.gravitySpeed >= 0f)
                        {
                            float rot = (v.Rotation / PI * 180 - xFacing + 180) % 180;
                            if (rot < -90)
                                rot += 180;

                            if (rot > 90)
                                rot -= 180;

                            final = rot;
                            res = true;
                            break;
                        }
                    }

                    if (res)
                    {
                        if (mission.gravitySpeed >= 0)
                        {
                            mission.gravitySpeed = 0f;
                            mission.Centre = oldCentre + GetVector2(xMoved, mission.missionRotation + final);
                        }
                        mission.jumpTimeLeft = mission.JumpTimeLimit;
                    }
                    else
                    {
                        if (mission.jumpTimeLeft == mission.jumpTimeLimit)
                            mission.jumpTimeLeft = mission.jumpTimeLimit - 1;

                        mission.gravitySpeed += mission.gravity / 50f;
                    }
                    mission.Centre += GetVector2(mission.gravitySpeed, trueRot);

                    bool jumpKeyDown = !IsKeyPressed120f(Keys_[(jumpKey + 2) % 4]);

                    if (jumpKeyDown)
                    {
                        if ((res || jumpKeyDown) && mission.jumpTimeLeft > 0)
                        {
                            mission.jumpTimeLeft--;
                            mission.gravitySpeed = -mission.jumpSpeed;
                        }
                    }
                    else if (mission.gravitySpeed < 0)
                        mission.gravitySpeed /= 3f;

                    if (mission.gravitySpeed > 0)
                        if (mission.umbrellaAvailable && IsKeyDown(InputIdentity.Alternate))
                            mission.gravitySpeed = mission.gravitySpeed * 0.8f + 1.0f * 0.2f;
                }
            }
        }
    }
}