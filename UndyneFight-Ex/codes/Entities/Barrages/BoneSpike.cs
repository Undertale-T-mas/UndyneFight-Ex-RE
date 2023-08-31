using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.AdvanceFunctions;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public class BoneSpike : Entity
    {
        private const float Length = 0;
        private readonly Player.Heart mission;
        private int appearTime = 0;
        private float rotationDelta;

        public void RotateTo(float rotation)
        {
            rotationDelta = MinRotate(Rotation + rotationDelta, rotation);
        }

        public float Speed { get; set; }
        public bool IsMasked { get; set; } = true;

        private Color drawingColor = Color.White;
        private int colorType = 0;
        private bool hasHit = false;
        private int score = 3;
        private float alpha = 0;
        float currentSpeed = 0;
        float waitingTime;

        public int ColorType
        {
            set
            {
                switch (value)
                {
                    case 0: drawingColor = Color.White; colorType = 0; break;
                    case 1: drawingColor = new Color(110, 203, 255, 255); colorType = 1; break;
                    case 2: drawingColor = Color.Orange; colorType = 2; break;
                }
            }
        }

        public BoneSpike(float waitingTime, float distance, float rotation, float speed)
        {
            Depth = 0.5f;
            this.waitingTime = waitingTime;
            mission = Heart;
            Speed = speed;
            Centre = GetVector2(distance, rotation + 180) + mission.Centre;
            rotationDelta = MathF.Min(MathF.Max(0, waitingTime - 5), 45) * 6;
            Rotation = -rotationDelta + rotation;
            Image = FightResources.Sprites.spike;
        }
        public override void Draw()
        {
            FormalDraw(Image, Centre, drawingColor * alpha, GetRadian(Rotation - 90), ImageCentre);
        }

        public override void Update()
        {
            if (alpha < 1 && appearTime <= 12) alpha += 0.1f;
            if (appearTime >= waitingTime)
            {
                currentSpeed = currentSpeed * 0.9f + Speed * 0.1f;
                Centre += GetVector2(currentSpeed, Rotation);
            }
            appearTime++;
            float del = rotationDelta * 0.1f;
            Rotation += del;
            rotationDelta *= 0.9f;
        }
        public void GetCollide(Player.Heart Heart)
        {
            Random rd = new();
            int d = rd.Next(0, 5);
            if (alpha <= 0.9f) return;
            float A, B, C, dist;
            if (Rotation == 0)
                dist = Centre.X - Heart.Centre.X;
            else
            {
                float k = (float)Math.Tan(GetRadian(Rotation + 90));
                A = k;
                B = -1;
                C = -A * Centre.X - B * Centre.Y;
                dist = (float)((A * Heart.Centre.X + B * Heart.Centre.Y + C) / Math.Sqrt(A * A + B * B));
            }

            float res = Math.Max(Math.Abs(dist) - 6f, GetDistance(Heart.Centre, Centre) - Length / 2f - 3.5f);

            if ((colorType == 1 && Heart.IsStable) || (colorType == 2 && Heart.IsMoved)) return;
            if (res < 0)
            {
                if (!hasHit)
                    PushScore(0);
                LoseHP(Heart);
                hasHit = true;
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
                if (!hasHit) PushScore(0);
                LoseHP(Heart);
                hasHit = true;
            }
        }

    }
}