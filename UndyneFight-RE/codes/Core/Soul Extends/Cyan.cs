using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Remake
{
    public partial class Souls
    {
        public class CyanMoveState : Player.MoveState
        {
            public CyanMoveState() : base(Color.Cyan, null)
            {
                this.MoveFunction = SoulMove;
            }

            public float PlungeSpeed { get; set; } = 15f;
            public float PlungeDecay { get; set; } = 0.1f;
            private Vector2 _plungeSpeed = Vector2.Zero;


            private const float COOLDOWN = 62.5f;
            private float _curTime = 0.0f;

            public void SoulMove(Player.Heart s)
            {
                CollideRect curPos = s.CollidingBox;

                Vector2 curCentre = curPos.GetCentre();

                float speed = s.Speed;
                if (IsKeyDown(InputIdentity.Cancel)) { speed *= 0.5f; }
                Vector2 delta = Vector2.Zero;
                bool flag = false;
                Vector2 plungeBuffer = Vector2.Zero;
                for (int i = 0; i < 4; i++)
                {
                    if (IsKeyDown(s.movingKey[i]))
                    {
                        delta += GetVector2(speed * 0.5f, i * 90);
                        
                        if(_curTime > COOLDOWN && IsKeyPressed120f(InputIdentity.Alternate))
                        {
                            flag = true;
                            plungeBuffer += GetVector2(1f, i * 90);
                        }
                    }
                }
                if (flag)
                {
                    _curTime = 0;
                    plungeBuffer.Normalize();
                    plungeBuffer *= 0.5f * PlungeSpeed;
                }
                this._plungeSpeed += plungeBuffer;
                if (_curTime <= COOLDOWN) _curTime += 0.5f;
                this._plungeSpeed *= 1 - PlungeDecay;
                delta += _plungeSpeed;

                Vector2 nexCentre = curCentre + delta;

                FightBox box = s.controlingBox;
                BoxVertex[] vertexs = box.Vertexs;

                // calculate all vertexs' normal vector

                nexCentre = DoBoxRestriction(curCentre, nexCentre, vertexs);

                s.Centre = nexCentre; 
            }
        }

        public static Player.MoveState CyanSoul => new CyanMoveState();
    }
}