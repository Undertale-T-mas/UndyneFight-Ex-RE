using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;


namespace UndyneFight_Ex.Remake.Entities
{
    public abstract class BulletShotable : Barrage
    {
        protected abstract void OnShot(SoulBullet bullet);

        List<SoulBullet> detects = new();
        protected float ObjectWidth { get; set; } = 6f;
        public override void Update()
        {
            base.Update();
            var pair = this.TryDetect("Bullet");
            if (pair.Item1)
            {
                var args = pair.Item2;
                SoulBullet newBullet = args.Source as SoulBullet;
                this.detects.Add(newBullet);
                pair.Item2.Dispose();
            }
            this.detects.RemoveAll(s => s.Disposed);
            this.detects.ForEach(bullet => {
                if (!bullet.Hitting && bullet.BeingUpdated)
                {
                    Vector2 distance = this.Centre - bullet.Centre;

                    float rotation = bullet.Rotation - 90;

                    Vector2 unit1 = MathUtil.GetVector2(1, rotation);
                    Vector2 unit2 = MathUtil.GetVector2(1, rotation + 90);
                    float project1 = Vector2.Dot(unit1, distance);
                    float project2 = Vector2.Dot(unit2, distance);
                    if(project1 < 30f && project1 > -ObjectWidth && MathF.Abs(project2) < ObjectWidth)
                    {
                        this.OnShot(bullet);
                    }
                }
            });
            var pair2 = this.TryDetect("Explode");
            if (pair2.Item1)
            {
                var args = pair2.Item2;
                Bomb et = args.Source as Bomb;
                float dis = 6;
                args.Dispose();
                
                if (MathF.Abs(et.Centre.X - this.Centre.X) <= dis || MathF.Abs(et.Centre.Y - this.Centre.Y)<=dis)
                { 
                    if (this is not Bomb) 
                    {
                        if (et.Destructive)
                        { this.Dispose();}
                    }
                    else
                    {
                        Bomb b = this as Bomb;
                        if (b.AbleLink) 
                        {
                            b.Explode();
                    }
                }
                }
            }
        }
    }
}