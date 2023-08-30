using Microsoft.Xna.Framework;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    internal class Bullet : Entity, ICollideAble
    {
        public Bullet(Vector2 centre, float rotation)
        {
            controlLayer = Surface.Hidden;
            Depth = 0.64f;
            missionCentre = centre;
            Rotation = rotation;

            rotation %= 360;

            CollideRect rect = FightBox.instance.CollidingBox;

            bool isDownEnabled = true, isUpEnabled = true, isLeftEnabled = true, isRightEnabled = true;

            if (rotation >= 0 && rotation <= 180) isUpEnabled = false;
            if (rotation >= 90 && rotation <= 270) isRightEnabled = false;
            if (rotation >= 180 || rotation == 0) isDownEnabled = false;
            if (rotation >= 270 || rotation <= 90) isLeftEnabled = false;

            float downCollideX = 0, upCollideX = 0, rightCollideY = 0, leftCollideY = 0;

            if (isDownEnabled) downCollideX = missionCentre.X + (rect.Down - missionCentre.Y) / Tan(rotation);
            if (isUpEnabled) upCollideX = missionCentre.X + (rect.Up - missionCentre.Y) / Tan(rotation);
            if (isRightEnabled) rightCollideY = missionCentre.Y + (rect.Right - missionCentre.X) * Tan(rotation);
            if (isLeftEnabled) leftCollideY = missionCentre.Y + (rect.Left - missionCentre.X) * Tan(rotation);

            if (isDownEnabled && (downCollideX >= rect.Left && downCollideX <= rect.Right))
                distance = (new Vector2(downCollideX, rect.Down) - missionCentre).Length();
            else if (isUpEnabled && (upCollideX >= rect.Left && upCollideX <= rect.Right))
                distance = (new Vector2(upCollideX, rect.Up) - missionCentre).Length();
            else if (isLeftEnabled && (leftCollideY >= rect.Up && leftCollideY <= rect.Down))
                distance = (new Vector2(rect.Left, leftCollideY) - missionCentre).Length();
            else if (isRightEnabled && (rightCollideY >= rect.Up && leftCollideY <= rect.Down))
                distance = (new Vector2(rect.Right, rightCollideY) - missionCentre).Length();
            distance += 20;
            //this.distance = Math.Min();
            Image = Sprites.bullet;
        }

        private Vector2 missionCentre;
        private float distance;

        private static CollideRect screen = new(-50, -50, 740, 580);

        public override void Update()
        {
            distance -= 12;
            Centre = missionCentre + GetVector2(distance, Rotation);

            if (!screen.Contain(Centre)) Dispose();
        }

        public override void Draw()
        {
            FormalDraw(Image, Centre, Color.White, 0.8f, GetRadian(Rotation), ImageCentre);
        }

        private bool hasHit = false;
        private int score = 3;

        public override void Dispose()
        {
            if (!hasHit) PushScore(score);
            base.Dispose();
        }

        public void GetCollide(Player.Heart player)
        {
            float res = (Centre - player.Centre).Length() - 11;

            if (res < 0)
            {
                if (!hasHit)
                {
                    PushScore(0);

                    GiveKR(0.6f);
                }
                LoseHP(player); hasHit = true;
            }
            else if (res <= 2)
            {
                if (score >= 2) { score = 1; Player.CreateCollideEffect(Color.LawnGreen, 3f); }
            }
            else if (res <= 6)
            {
                if (score >= 3) { score = 2; Player.CreateCollideEffect(Color.LightBlue, 6f); }
            }
            if (score != 3 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
            {
                if (!hasHit) PushScore(0); LoseHP(player); hasHit = true;
            }
        }
    }
    public class GunBullet : Entity
    {
        public GunBullet(Vector2 targetCentre, float delayTime, float rotation)
            : this(targetCentre, delayTime, new float[] { rotation }) { }
        public GunBullet(Vector2 targetCentre, float delayTime, float[] rotations)
        {
            PlaySound(Sounds.gunTargeting);
            Image = Sprites.target;
            Centre = targetCentre;
            this.delayTime = delayTime;
            distance = 190f;
            Depth = 0.41f;
            k = 0.0f;
            this.rotations = rotations;
        }

        private readonly float[] rotations;
        private readonly float distance;
        private float currentDistance;

        private int appearTime = 0;
        private readonly float delayTime;
        private float k = 0f;
        private float alpha = 0f;

        public override void Draw()
        {
            if (appearTime <= delayTime)
            {
                for (int i = 0; i < rotations.Length; i++)
                {
                    FormalDraw(Image, Centre + GetVector2(currentDistance, rotations[i]),
                        Color.White * alpha, 0.55f, 0, ImageCentre);
                }
                FormalDraw(Image, Centre, Color.White * 0.8f, 0.62f, 0, ImageCentre);
            }
            else
            {
                FormalDraw(Image, Centre, Color.White * alpha, 0.62f * k, 0, ImageCentre);
            }
        }

        public override void Update()
        {
            appearTime++;
            if (appearTime <= delayTime)
            {
                k = F(appearTime / delayTime);
                currentDistance = (1 - k) * distance;
                alpha = k * 0.7f + 0.1f;
            }
            if (appearTime == (int)delayTime)
            {
                PlaySound(Sounds.gunShot, 0.8f);
                for (int i = 0; i < rotations.Length; i++)
                    GameStates.InstanceCreate(new Bullet(Centre, rotations[i]));
            }
            if (appearTime >= delayTime)
            {
                if (alpha < 0) Dispose();
                k += 0.06f;
                alpha -= 0.05f;
            }
        }

        private float F(float x)
        {
            return -2 / (x - 2) - 1;
        }
    }
}