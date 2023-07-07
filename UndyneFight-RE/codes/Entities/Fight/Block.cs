using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;

namespace UndyneFight_Ex.Remake.Entities
{ 
    public class ShotableBlock : BulletShotable
    {
        public ShotableBlock() {
            this.Image = Resources.FightSprites.MettBlockA;
            this.Alpha = 1.0f;
            this.Depth = 0.45f;
        }

        protected override float GetDistance(Player.Heart heart)
        {
             return Vector2.Distance(heart.Centre, this.Centre);
        }

        protected override void OnShot(SoulBullet bullet)
        {
            bullet.Dispose();
            Functions.PlaySound(Resources.Sounds.TargetBurst);
            GameStates.InstanceCreate(new Shattered(this));
            this.Dispose();
        }
    }


    public class ToughBlock : BulletShotable
    {
        public ToughBlock()
        {
            this.Image = Resources.FightSprites.MettBlockB;
            this.Alpha = 1.0f;
            this.Depth = 0.45f;
        }

        protected override float GetDistance(Player.Heart heart)
        {
            return Vector2.Distance(heart.Centre, this.Centre);
        }

        protected override void OnShot(SoulBullet bullet)
        {
            bullet.Dispose();
            Functions.PlaySound(FightResources.Sounds.Ding); 
        }
    }
}