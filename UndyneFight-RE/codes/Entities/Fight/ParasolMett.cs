using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Entities.EasingUtil;
using static UndyneFight_Ex.Remake.Resources;

namespace UndyneFight_Ex.Remake.Entities
{
    public class ParasolMett : BulletShotable
    {
        public float WaveTime { get; set; } = 50.0f;
        public float BulletDepth { get; set; } = 0.5f;
        public bool damage { get; set; } = true;
        private class MettBullet : Barrage
        {
            public bool damage { get; set; } = true;
            public MettBullet(Vector2 pos, float waveTime)
            {
                this.Image = FightSprites.MettBullet;
                this.Centre = pos;
                this.AngleMode = true;
                this.HitRadius = 6.5f;

                this.PositionRoute = CentreEasing.Combine(CentreEasing.Linear(
                    MathUtil.GetVector2(4.0f, MathUtil.Direction(this.Centre, PlayerMission.Centre))
                ), this.Centre);
                this.RotationRoute = ValueEasing.SinWave(10f, waveTime * 2, 0.0f);
            }
            public override void Update()
            {
                base.Update();
                if (Alpha < 1.0f) Alpha += 0.05f;
            }
            protected override float GetDistance(Player.Heart heart)
            {
                return damage ? Vector2.Distance(heart.Centre, this.Centre) : Vector2.Distance(new Vector2(-9000), this.Centre);
            }
        }
        ParasolMett()
        {
            images = FightSprites.ParasolMett;
            this.Image = images[0];
            shootQueue = new();
            this.Alpha = 1.0f;
            ObjectWidth = 9.0f;
            HitRadius = 7.5f;
        }
        public ParasolMett(EaseUnit<Vector2> centreEasing) : this()
        {
            this.PositionRoute = centreEasing.Easing;
        }
        public ParasolMett(Func<ICustomMotion, Vector2> centreEasing) : this()
        {
            this.PositionRoute = centreEasing;
        }
        public ParasolMett(EaseUnit<Vector2> centreEasing, EaseUnit<float> rotationEasing) : this()
        {
            this.PositionRoute = centreEasing.Easing;
            this.RotationRoute = rotationEasing.Easing;
        }
        public ParasolMett(Func<ICustomMotion, Vector2> centreEasing, Func<ICustomMotion, float> rotationEasing) : this()
        {
            this.PositionRoute = centreEasing;
            this.RotationRoute = rotationEasing;
        }
        Texture2D[] images;

        Queue<float> shootQueue;

        /// <summary>
        /// insert an time to shoot. If insert more than one times, you have to insert in an increasing order
        /// </summary>
        /// <param name="time">The time delayed to shoot</param>
        public void InsertShot(float time)
        {
            shootQueue.Enqueue(time);
        }
        public void InsertShot(params float[] times)
        {
            for (int a = 0; a < times.Length; a++)
            {
                InsertShot(times[a]);
            }
        }
        public void InsertShot(int val, float time)
        {
            for (int a = 0; a < val; a++)
                shootQueue.Enqueue(time * a);
        }
        protected override float GetDistance(Player.Heart heart)
        {
            return damage ? Vector2.Distance(heart.Centre, this.Centre) : Vector2.Distance(new Vector2(-9000), this.Centre);
        }

        protected override void OnShot(SoulBullet bullet)
        {
            bullet.Dispose();
            this.Dispose();
            Functions.PlaySound(Sounds.TargetBurst);
        }

        private void MakeShoot()
        {
            index = 0;
            this.AddChild(new InstantEvent(7, () => { GameStates.InstanceCreate(new MettBullet(this.Centre, this.WaveTime) { Depth = this.BulletDepth, damage = this.damage }); }));
        }
        int index = -1;

        float appearTime = 0;
        public override void Update()
        {
            base.Update();
            appearTime += 0.5f;
            while (shootQueue.Count > 0 && shootQueue.Peek() <= appearTime)
            {
                shootQueue.Dequeue();
                this.MakeShoot();
            }
            if (index != -1 && appearTime % 1f < 0.5f)
            {
                index++;
                if (index >= images.Length)
                {
                    index = -1;
                }
            }
            if (index != -1)
            {
                this.Image = images[index];
            }
        }
    }
}