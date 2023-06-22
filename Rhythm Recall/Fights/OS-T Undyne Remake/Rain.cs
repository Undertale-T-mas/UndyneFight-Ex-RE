using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    internal class Rain : Entity
    {
        internal static bool way;
        private Vector2 speed;
        private static Texture2D[] images = new Texture2D[3];
        internal static void LoadResources()
        {
            for (int i = 1; i <= 3; i++)
                images[i - 1] = Loader.Load<Texture2D>("Fights\\OS-T Remake\\Rain" + i);
        }
        public Rain()
        {
            Depth = 0.001f;
            Image = images[Rand(0, 2)];
            speed = new Vector2(Rand(-10, -5) / 10f * (way ? 1 : -1), 5.6f);
            Centre = new Vector2(Rand(-75, 715), -26);
        }
        public override void Draw()
        {
            FormalDraw(Image, Centre, Color.White * UndyneFight_Ex.Fight.ClassicFight.InterActive.UIAlpha, MathUtil.GetRadian(MathF.Atan2(speed.Y, speed.X)), ImageCentre);
        }

        public override void Update()
        {
            speed.Y += 2.5f / 60f;
            Centre += speed;
            if (Centre.Y >= 720) Dispose();
        }
    }
}