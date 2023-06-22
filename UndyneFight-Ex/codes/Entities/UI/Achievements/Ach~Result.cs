using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex.Achievements
{
    internal class AchievementResult : Entity
    {
        public AchievementResult(Achievement achievement)
        {
            // actions
            SetID(currentList.Count + 1);
            currentY = targetY;

            collidingBox = new(Vector2.Zero, new(156, 76));

            Depth = 0.80f;
            CrossScene = true;

            currentList.Enqueue(this);
        }

        private static Queue<AchievementResult> currentList = new();
        int appearTime = 0;
        float bufferX = 340;

        int id = 0;

        float currentY = 0, targetY = 0;

        private void SetID(int index)
        {
            targetY = 522 - index * 85;
        }

        const int totalTime = 300;

        public override void Draw()
        {
            DrawingLab.DrawRectangle(collidingBox, Color.White, 2f, Depth + 0.01f);
            FormalDraw(FightResources.Sprites.pixiv, collidingBox.ToRectangle(), Color.Black);
        }

        public override void Update()
        {
            appearTime++;
            if (appearTime <= totalTime) bufferX = MathF.Round(bufferX * 0.8f, 1);
            else bufferX += 10;

            currentY = MathHelper.Lerp(currentY, targetY, 0.2f);
            Centre = new(540 - 6 + bufferX, currentY);

            if (appearTime > totalTime + 100) Dispose();
        }

        public override void Dispose()
        {
            if (currentList.Peek() != this) throw new SystemException("Impossible Case!");
            currentList.Dequeue();
            foreach (var v in currentList)
            {
                v.SetID(v.id - 1);
            }
            base.Dispose();
        }
    }
}