using Microsoft.Xna.Framework;
using Rhythm_Recall.Engine;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    internal class Sans : Enemy
    {
        public static Sans instance;

        public Sans() : base("Sans", 1, 1.0f, 1)
        {
            Centre = new Vector2(320, 140); Depth = 0.999f;
            IsShake = false;
            instance = this;
            BodyMoveSpeed = 3;
            FollowBox = false;
            bodyTemporary = normalBody;
            bodyMovementTick = -1;
        }

        #region Resources
        private static EnemyComponment normalHead, normalBody, normalLeg, angryHead, eyesClosedHead, sprawlHandsBody;
        private static EnemyComponment handLRBody, handUDBody, handRightBody, handUpBody, handDownBody;
        private static EnemyComponment bodyTemporary;

        internal static void LoadingResources()
        {
            eyesClosedHead = new EnemyComponment("Fights\\WoodenHall\\eyesClosedHead");
            angryHead = new EnemyComponment("Fights\\WoodenHall\\angryHead");
            normalBody = new EnemyComponment("Fights\\WoodenHall\\normalBody");
            normalLeg = new EnemyComponment("Fights\\WoodenHall\\normalLeg");
            normalHead = new EnemyComponment("Fights\\WoodenHall\\normalHead");
            sprawlHandsBody = new EnemyComponment("Fights\\WoodenHall\\sprawlHandsBody");

            handLRBody = new EnemyComponment("Fights\\WoodenHall\\handLRBody", normalBody.ImageCentre);
            handUDBody = new EnemyComponment("Fights\\WoodenHall\\handUDBody", normalBody.ImageCentre + new Vector2(0, 24));
            handRightBody = new EnemyComponment("Fights\\WoodenHall\\handRightBody", normalBody.ImageCentre);
            handDownBody = new EnemyComponment("Fights\\WoodenHall\\handDownBody", normalBody.ImageCentre);
            handUpBody = new EnemyComponment("Fights\\WoodenHall\\handUpBody", normalBody.ImageCentre + new Vector2(0, 24));

            drawingHead = normalHead;
            drawingBody = normalBody;
            drawingLeg = normalLeg;
        }
        #endregion

        public enum FaceExpression
        {
            Angry = 0,
            Normal = 1,
            CloseEyes = 2
        }
        public enum BodyMovement
        {
            None = 0,
            SprawlHands = 1,
            ToLeft = 4,
            ToRight = 2,
            ToUp = 5,
            ToDown = 3
        }

        private static BodyMovement currentMovement;
        public static BodyMovement BodyTexture
        {
            set
            {
                currentMovement = value;
                switch (value)
                {
                    case BodyMovement.None:
                        bodyMovementTick = 0;
                        bodyTemporary = normalBody;
                        break;
                    case BodyMovement.SprawlHands:
                        bodyMovementTick = -1;
                        drawingBody = sprawlHandsBody;
                        break;
                    case BodyMovement.ToLeft:
                        bodyMovementTick = BodyMoveSpeed;
                        drawingBody = handLRBody;
                        bodyTemporary = normalBody;
                        break;
                    case BodyMovement.ToRight:
                        bodyMovementTick = BodyMoveSpeed;
                        drawingBody = handLRBody;
                        bodyTemporary = handRightBody;
                        break;
                    case BodyMovement.ToUp:
                        bodyMovementTick = BodyMoveSpeed;
                        drawingBody = handUDBody;
                        bodyTemporary = handUpBody;
                        break;
                    case BodyMovement.ToDown:
                        bodyMovementTick = BodyMoveSpeed;
                        drawingBody = handUDBody;
                        bodyTemporary = handDownBody;
                        break;
                    default:
                        break;
                }
            }
        }
        public static FaceExpression HeadTexture
        {
            set
            {
                switch (value)
                {
                    case FaceExpression.Angry:
                        drawingHead = angryHead;
                        break;
                    case FaceExpression.Normal:
                        drawingHead = normalHead;
                        break;
                    case FaceExpression.CloseEyes:
                        drawingHead = eyesClosedHead;
                        break;
                    default:
                        break;
                }
            }
        }

        public static bool IsShake { get; set; } = false;
        public static bool FollowBox { get; set; } = false;
        public static int BodyMoveSpeed { get; set; } = 7;

        private static EnemyComponment drawingHead, drawingBody, drawingLeg;
        private static Vector2 headPosition, bodyPosition, legPosition, gravityDetla = Vector2.Zero, gravityMission = Vector2.Zero;
        private int dodgingTime = -1, appearTime = 0;
        private static int bodyMovementTick = -1;

        Vector2 missionPosition = Vector2.Zero;
        float boxDeltaY = 0;

        protected override void Attacked()
        {
        }

        protected override void Dodge()
        {
            dodgingTime = 75;
        }

        private void SetGravityMission()
        {
            switch (currentMovement)
            {
                case BodyMovement.ToUp: gravityMission = new Vector2(0, -3); break;
                case BodyMovement.ToDown: gravityMission = new Vector2(0, 3); break;
                case BodyMovement.ToLeft: gravityMission = new Vector2(-3, 0); break;
                case BodyMovement.ToRight: gravityMission = new Vector2(3, 0); break;
                default: gravityMission = Vector2.Zero; break;
            }
        }

        public override void Update()
        {
            SetGravityMission();

            gravityDetla = gravityDetla * 0.6f + gravityMission * 0.4f;
            if (bodyMovementTick >= 0)
            {
                if (bodyMovementTick == 0) drawingBody = bodyTemporary;
                bodyMovementTick--;
            }
            appearTime++;
            Vector2 detla = new Vector2(0 + MathHelper.Min(100, AdvanceFunctions.Sin01(dodgingTime / 75f) * 125), 40);

            missionPosition = missionPosition * 0.8f + detla * 0.2f;
            if (FollowBox)
            {
                float v = (FightBox.instance as RectangleBox).Up - 225;
                boxDeltaY = boxDeltaY * 0.8f + v * 0.2f;
            }
            float angle = AdvanceFunctions.Sin01(appearTime / 60f) * 80f + 90;
            Vector2 detla2 = new Vector2(Cos(angle) * 2, Sin(angle)) * 1.5f;
            if (!IsShake) detla2 = Vector2.Zero;

            headPosition = new Vector2(-1, -88 + boxDeltaY) + missionPosition + detla2 / 1.2f + Centre + gravityDetla;
            bodyPosition = new Vector2(0, -39 + boxDeltaY) + missionPosition + detla2 + Centre + gravityDetla;
            legPosition = new Vector2(1, 8 + boxDeltaY) + missionPosition + Centre;
            dodgingTime--;
            if (dodgingTime < 0) dodgingTime = 0;
        }
        public override void Draw()
        {
            Depth = 0.06f;
            FormalDraw(drawingHead.Texture, headPosition, Color.White, 1.0f, 0, drawingHead.ImageCentre);
            Depth = 0.03f;
            FormalDraw(drawingBody.Texture, bodyPosition, Color.White, 0, drawingBody.ImageCentre);
            Depth = 0.005f;
            FormalDraw(drawingLeg.Texture, legPosition, Color.White, 0, drawingLeg.ImageCentre);
        }
    }
}