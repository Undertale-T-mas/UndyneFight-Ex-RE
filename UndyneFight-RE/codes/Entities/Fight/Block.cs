using System;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.Remake.Resources;

namespace UndyneFight_Ex.Remake.Entities
{
    public class ShotableBlock : BulletShotable
    {
        public ShotableBlock(EaseUnit<Vector2> centreEasing) : this() {
            this.PositionRoute = centreEasing.Easing;
        }
        public ShotableBlock(Func<ICustomMotion, Vector2> centreEasing) : this() {
            this.PositionRoute = centreEasing;
        }
        public ShotableBlock(EaseUnit<Vector2> centreEasing, EaseUnit<float> rotationEasing) : this() {
            this.PositionRoute = centreEasing.Easing;
            this.RotationRoute = rotationEasing.Easing;
        }
        public ShotableBlock(Func<ICustomMotion, Vector2> centreEasing, Func<ICustomMotion, float> rotationEasing) : this() {
            this.PositionRoute = centreEasing;
            this.RotationRoute = rotationEasing;
        }
        public ShotableBlock() {
            this.AngleMode = true;
            this.Image = FightSprites.MettBlockA;
            this.Alpha = 1.0f;
            this.Depth = 0.45f;
            this.HitRadius = 7f;
            this.ObjectWidth = 10;
        }

        protected override float GetDistance(Player.Heart heart)
        {
             return Vector2.Distance(heart.Centre, this.Centre);
        }

        protected override void OnShot(SoulBullet bullet)
        {
            bullet.Dispose();
            PlaySound(Sounds.TargetBurst);
            InstanceCreate(new Shattered(this));
            this.Dispose();
        }
    }
     
    public class ToughBlock : BulletShotable
    {
        public ToughBlock(EaseUnit<Vector2> centreEasing) : this()
        {
            this.PositionRoute = centreEasing.Easing;
        }
        public ToughBlock(Func<ICustomMotion, Vector2> centreEasing) : this()
        {
            this.PositionRoute = centreEasing;
        }
        public ToughBlock(EaseUnit<Vector2> centreEasing, EaseUnit<float> rotationEasing) : this()
        {
            this.PositionRoute = centreEasing.Easing;
            this.RotationRoute = rotationEasing.Easing;
        }
        public ToughBlock(Func<ICustomMotion, Vector2> centreEasing, Func<ICustomMotion, float> rotationEasing) : this()
        {
            this.PositionRoute = centreEasing;
            this.RotationRoute = rotationEasing;
        }
        public ToughBlock()
        {
            this.AngleMode = true;
            this.Image = FightSprites.MettBlockB;
            this.Alpha = 1.0f;
            this.Depth = 0.45f;
            this.HitRadius = 6.5f;
            this.ObjectWidth = 10;
        }

        protected override float GetDistance(Player.Heart heart)
        {
            return Vector2.Distance(heart.Centre, this.Centre);
        }

        protected override void OnShot(SoulBullet bullet)
        {
            bullet.Dispose();
            PlaySound(FightResources.Sounds.Ding); 
        }
    }
}