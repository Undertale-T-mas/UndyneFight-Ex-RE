using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using static Rhythm_Recall.Resources;

namespace Rhythm_Recall
{
    internal class SetUpDrawing : Entity
    {
        class Camera : Entity
        {
            public override void Draw()
            { }

            public override void Update()
            {
                Centre = new Vector2(320, 240);
            }
        }
        public SetUpDrawing()
        {
            AddChild(new ImageEntity(Resources.BackGround) { Scale = 1.5f, Centre = new(480, 360), Alpha = 0.37f });
        }

        private Vector2 detla = new Vector2(320, 240) * 6;
        private float RhythmX = 0;
        private float RecallX = 0;
        private int appearTime;
        private float scale;

        public void DrawString(string str, Vector2 centre, float scale)
        {
            float width = 0;
            for (int i = 0; i < str.Length; i++)
            {
                width += charTextures[str[i]].Width + 5;
            }
            float curX = centre.X - width / 2f * scale;
            this.Depth = 0.5f;
            for (int i = 0; i < str.Length; i++)
            {
                FormalDraw(charTextures[str[i]], new Vector2(curX, centre.Y), Color.White, scale, 0, new Vector2(0, charTextures[str[i]].Height / 2f));
                curX += (charTextures[str[i]].Width + 5) * scale;
            }
        }

        public override void Draw()
        {
            DrawString("RHYTHM", (new Vector2(190 + RhythmX, 70) - detla) * 1.5f, scale * 1.5f);
            DrawString("RECALL", (new Vector2(450 + RecallX, 144) + detla) * 1.5f, scale * 1.5f);
        }

        public override void Update()
        {
            if (appearTime == 0)
            {
                this.AddChild(new BlockMaker());
            }
            appearTime++;
            if (appearTime < 10) return;
            scale = Math.Min((appearTime - 10) * 0.02f, 1f);
            if (appearTime > 60)
            {
                var time = appearTime - 200;
                RhythmX = MathF.Sin(time / 50f) * 10;
                RecallX = MathF.Sin(time / 50f) * -10;
            }
            else
                detla *= 0.88f;
        }
    }
}