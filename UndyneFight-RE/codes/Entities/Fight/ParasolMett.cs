using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;
using Microsoft.Xna.Framework.Graphics;
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Remake.Entities
{
    public class ParasolMett : BulletShotable
    {
        public float WaveTime { get; set; } = 50.0f;
        private class MettBullet : Barrage
        {
            public MettBullet(Vector2 pos, float waveTime) {
                this.Image = Resources.FightSprites.MettBullet;
                this.Centre = pos;
                this.AngelMode = true;
                this.Depth = 0.5f;
                this.HitRadius = 6.5f;

                this.PositionRoute = EasingUtil.CentreEasing.Combine(EasingUtil.CentreEasing.Linear(
                    MathUtil.GetVector2(4.0f, MathUtil.Direction(this.Centre, PlayerMission.Centre))
                ), this.Centre);
                this.RotationRoute = EasingUtil.ValueEasing.SinWave(10f, waveTime * 2, 0.0f);
            }
            public override void Update()
            {
                base.Update();
                if (Alpha < 1.0f) Alpha += 0.05f;
            }
            protected override float GetDistance(Player.Heart heart)
            {
                return Vector2.Distance(heart.Centre, this.Centre);
            }
        }
        public ParasolMett() {
            images = Resources.FightSprites.ParasolMett;
            this.Image = images[0];
            shootQueue = new();
            this.Alpha = 1.0f;
            ObjectWidth = 9.0f;
            HitRadius = 7.5f;
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

        protected override float GetDistance(Player.Heart heart)
        {
            return Vector2.Distance(heart.Centre, this.Centre);
        }

        protected override void OnShot(SoulBullet bullet)
        { 
            bullet.Dispose();
            this.Dispose();
            Functions.PlaySound(Resources.Sounds.TargetBurst);
        }

        private void MakeShoot()
        {
            index = 0;
            this.AddChild(new InstantEvent(7, () => { GameStates.InstanceCreate(new MettBullet(this.Centre, this.WaveTime)); }));
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
            if(index != -1)
            {
                this.Image = images[index];
            }
        }
    }
}