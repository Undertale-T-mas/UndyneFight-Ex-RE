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
            Camera cam = new Camera();
            AddChild(cam);
            AddChild(new BackGround(Resources.BackGround, cam, new(320, 240)) { Alpha = 0.4f });
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
                width += (charTextures[str[i]].Width + 5);
            }
            float curX = centre.X - width / 2f * scale;
            for (int i = 0; i < str.Length; i++)
            {
                FormalDraw(charTextures[str[i]], new Vector2(curX, centre.Y), Microsoft.Xna.Framework.Color.White, scale, 0, new Vector2(0, Resources.charTextures[str[i]].Height / 2f));
                curX += (charTextures[str[i]].Width + 5) * scale;
            }
        }

        public override void Draw()
        {
            DrawString("RHYTHM", new Vector2(190 + RhythmX, 90) - detla, scale);
            DrawString("RECALL", new Vector2(450 + RecallX, 170) + detla, scale);
        }

        public override void Update()
        {
            if (appearTime == 0)
            {
                GameStates.InstanceCreate(new BlockMaker());
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