using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Rhythm_Recall.Waves;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall
{
    internal class Undyne : Enemy
    {
        #region Positions & Resources
        private Vector2 Head_place;
        private Vector2 Body_place;
        private Vector2 Larm_place;
        private Vector2 Rarm_place;
        private Vector2 Leggings_place;
        private Vector2 Pants_place;
        private Vector2 Hair_place;

        private Texture2D currentHead;
        private readonly Texture2D normalHead;
        private readonly Texture2D damagingHead;
        private readonly Texture2D headDied;
        private readonly Texture2D laughingHead;
        private readonly Texture2D normalBody;
        private readonly Texture2D larm;
        private readonly Texture2D rarm;
        private readonly Texture2D leggings;
        private readonly Texture2D pants;
        private readonly Texture2D hair;

        public Undyne(Texture2D hairImage)
        {
            hair = hairImage;
        }
        #endregion

        private class EyeLaser : Entity
        {
            private float scale, alpha, alphaPercent = 0;
            private readonly float f0;
            private readonly float r0;
            private readonly float speed;
            private float appearTime = 0;

            public EyeLaser()
            {
                Depth = 0.11f;
                scale = 1f; alpha = 1f;
                f0 = Rand(0, 359);
                r0 = Rand(-10, 10);
                speed = Rand(20, 40) / 10f;
                Image = OSTUndyne.Resources.eyeLaser;
            }

            public override void Draw()
            {
                FormalDraw(Image, Centre,
                    Color.White * alpha * alphaPercent * 1.32f * UndyneFight_Ex.Fight.ClassicFight.InterActive.UIAlpha,
                    new Vector2(scale, 1f), MathUtil.GetRadian(Rotation), new Vector2(0, 15));
            }

            public override void Update()
            {
                if (OSTUndyne.instance.undyne.shakingRemain > 0) Dispose();
                Centre = new(325, OSTUndyne.instance.undyne.Head_place.Y);
                alpha -= 0.018f;
                alphaPercent = alphaPercent * 0.9f + 0.1f;
                scale += 0.02f;
                appearTime++;
                Rotation = r0 + Sin(appearTime * speed + f0) * 20;
                if (alpha < 0) Dispose();
            }
        }

        private float slideDetla;
        private float alpha = 1;
        private int appearTime = 0;
        private int shakingRemain = 0;

        public Undyne()
        {
            Name = "Undyne";
            HP = 28000;
            MaxHp = 28000;
#if DEBUG
            HP = 2000;
#endif

            Centre = new Vector2(320, 135);
            normalHead = Loader.Load<Texture2D>("Fights\\OS-T Remake\\face_normal");
            headDied = Loader.Load<Texture2D>("Fights\\OS-T Remake\\face_die");
            damagingHead = Loader.Load<Texture2D>("Fights\\OS-T Remake\\face_damaging");
            laughingHead = Loader.Load<Texture2D>("Fights\\OS-T Remake\\face_laugh");
            normalBody = Loader.Load<Texture2D>("Fights\\OS-T Remake\\body");
            pants = Loader.Load<Texture2D>("Fights\\OS-T Remake\\pants");
            larm = Loader.Load<Texture2D>("Fights\\OS-T Remake\\larm");
            rarm = Loader.Load<Texture2D>("Fights\\OS-T Remake\\rarm");
            leggings = Loader.Load<Texture2D>("Fights\\OS-T Remake\\legs");
            hair = Loader.Load<Texture2D>("Fights\\OS-T Remake\\hair");
            currentHead = normalHead;
        }

        private bool isFinalType = false;
        private bool motionAvailable = true;
        private int finalTick = 0;
        private float hairRot;
        public void IntoFinal()
        {
            isFinalType = true;
        }

        public override void Update()
        {
            if (OSTUndyne.instance.wave == 19 && !UndyneFight_Ex.Fight.ClassicFight.RoundType) FinalWaveUpdate();

            alpha = UndyneFight_Ex.Fight.ClassicFight.InterActive.UIAlpha;
            appearTime += 1;

            if (!motionAvailable)
            {
                slideDetla = (float)Math.Sin(appearTime / 242f);
                hairRot = MathUtil.GetRadian((float)Math.Sin(appearTime / 52f) - 90f) * 9f;
            }
            else
            {
                slideDetla = (float)Math.Sin(appearTime / 12f);
                hairRot = MathUtil.GetRadian(slideDetla - 90f) * 9f;

                if (Rand(0, 30) == 0) CreateEntity(new EyeLaser());
            }

            Vector2 delta = Centre - new Vector2(277, 100);
            if (shakingRemain > 0)
            {
                delta += 2 * new Vector2((shakingRemain % 4) < 2 ? MathF.Sqrt(shakingRemain) : -MathF.Sqrt(shakingRemain), 0);
                shakingRemain -= 1;
                if (shakingRemain == 0)
                {
                    if (currentHead != headDied)
                        currentHead = normalHead;
                }
            }

            Head_place.X = 277;
            Head_place.Y = slideDetla * 2.5f + 16;
            Body_place.X = 257;
            Body_place.Y = Head_place.Y * 0.5f + 20;
            Larm_place.X = 178 - slideDetla * 4f / (headDied == currentHead ? 3 : 1);
            Larm_place.Y = Body_place.Y - slideDetla * 3.1f / (headDied == currentHead ? 3 : 1) + 30;
            float val2 = (float)Math.Sin(appearTime / 6.5f);
            if (!motionAvailable)
            {
                val2 = (float)Math.Sin(appearTime / 126.5f);
                val2 /= 3f;
            }
            Rarm_place.X = 315 + val2 * 2f - slideDetla * 2.2f;
            Rarm_place.Y = Body_place.Y * 1.1f * 2 + val2 * 1.9f + 31;
            Pants_place.X = 262;
            Pants_place.Y = 143 + slideDetla * 2;
            Leggings_place.X = 276;
            Leggings_place.Y = 162;

            Head_place += delta;
            Body_place += delta;
            Larm_place += delta;
            Rarm_place += delta;
            Pants_place += delta;
            Leggings_place += delta;

            hairRot += MathUtil.GetRadian(90);
            Hair_place.X = Head_place.X - 12;
            Hair_place.Y = Head_place.Y - 13;

        }

        public override void Draw()
        {
            Color dc = !isFinalType ? Color.White * alpha : new Color(10, 10, 10, 255) * alpha;
            Depth = 0.1f;
            FormalDraw(currentHead, Head_place, dc, 1, 0, new Vector2(37, 30));
            Depth -= 0.01f;
            FormalDraw(normalBody, Body_place, dc, 1, MathUtil.GetRadian(slideDetla - 0.4f) * 2.5f, new Vector2(57, 30));
            Depth -= 0.01f;
            FormalDraw(larm, Larm_place, dc, 1, 0, new Vector2(46, 76));
            Depth -= 0.01f;
            FormalDraw(rarm, Rarm_place, dc, 1, MathUtil.GetRadian(slideDetla) * 2.5f + 0.4f, new Vector2(19, 44));
            Depth += 0.03f;
            FormalDraw(pants, Pants_place, dc, 1, MathUtil.GetRadian(slideDetla) * -4.5f, new Vector2(39, 27));
            Depth -= 0.03f;
            FormalDraw(leggings, Leggings_place, dc, 1, 0, new Vector2(40, 44));
            Depth -= 0.01f;
            FormalDraw(hair, Hair_place, dc, 1, hairRot, new Vector2(70, 52));
            /* 
            if (gametime == 8660) Program.basicform.playSound("spawn.wav");
            if (gametime == 8690) Program.basicform.playSound("pierce.wav");
            if (gametime == 8725) Program.basicform.playSound("damaged.wav");
            if (gametime == 8785) Program.basicform.playSound("die_2.wav");*/
        }

        protected override void Dodge()
        {
            throw new NotImplementedException();
        }

        protected override void Attacked()
        {
            currentHead = damagingHead;
            shakingRemain = 36;

            if (HP <= 4720)
            {
                UndyneFight_Ex.Fight.ClassicFight.InterActive.MainMessage = "$Only three hits left.";
                UndyneFight_Ex.Fight.ClassicFight.InterActive.MainMessageAttributes = new TextAttribute[] {
                    new TextColorAttribute(Color.Red)
                };
            }
            if (HP <= 3160)
            {
                UndyneFight_Ex.Fight.ClassicFight.InterActive.MainMessage = "$Only two hits left.";
                UndyneFight_Ex.Fight.ClassicFight.InterActive.MainMessageAttributes = new TextAttribute[] {
                    new TextColorAttribute(Color.Red)
                };
            }
            if (HP <= 1600) //When Undyne has low HP
            {
                currentHead = headDied;
                motionAvailable = false;
                OSTUndyne.instance.wave = 18;
                OSTUndyne.musicInstance.Dispose();
                OSTUndyne.MainMusic = Loader.Load<SoundEffect>("Fights\\OS-T Remake\\final theme");
            }
        }

        private void FinalWaveUpdate()
        {
            int _time = finalTick;
            finalTick++;
            if (_time == 120 + 210) currentHead = laughingHead;
            if (_time == 120 + 200 + 210) currentHead = normalHead;
        }
    }
}