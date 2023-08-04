using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.Fight.Functions;
using System.Resources;
using Microsoft.Xna.Framework.Graphics;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using System.Runtime.CompilerServices;

namespace UndyneFight_Ex.Remake.Entities
{
    public class Bomb : BulletShotable
    {
        private class BombBlast : AutoEntity
        {
            Texture2D[] allStates;
            public BombBlast(Vector2 position, Texture2D[] mode) {
                this.allStates = mode;
                this.Centre = position;
                this.Image = allStates[0];
                this.Alpha = 1.0f;
                this.Depth = 0.53f;
            }
            int index = 0;
            int count = 0;
            
            public override void Update()
            {
                count++;
                if (count > 4)
                {
                    count -= 2;
                    index++;
                    if(index >= allStates.Length)
                    {
                        this.Dispose();
                        return;
                    }
                    this.Image = this.allStates[index];
                }
            }
        }
        float _explodeDelay;
        public Bomb(float explodeDelay, Func<ICustomMotion, Vector2> ease)
        {
            this._explodeDelay = explodeDelay;
            this.Image = Resources.FightSprites.MettBomb[0];
            this.Alpha = 1.0f;
            this.HitRadius = 6.6f;
            this.PositionRoute = ease;
            this.Depth = 0.501f;
        }
        List<Entity> canDestroy = new();
        public void ConnectEntity(Entity entity)
        {
            canDestroy.Add(entity);
        }
        protected override float GetDistance(Player.Heart heart)
        {
            if(count / 2f >= _explodeDelay - 1.1f)
            {
                Vector2 c1 = heart.Centre, c2 = this.Centre;
                return MathF.Min(MathF.Abs(c1.X - c2.X), MathF.Abs(c1.Y - c2.Y)) + 3;
            }
            return Vector2.Distance(heart.Centre, this.Centre);
        }
        private bool isShot = false;
        protected override void OnShot(SoulBullet bullet)
        {
            bullet.Dispose();
            Trigger();
        }

        private void Trigger()
        {
            PlaySound(FightResources.Sounds.Warning);
            this.isShot = true;
        }

        private int count = 0;
        public bool AbleLink { get; set; }= true;
        public bool Destructive { get; set; }= true;
        public override void Update()
        {
            base.Update();
            if(isShot) {
                count++;
                if(count % 8 < 4)
                {
                    this.Image = Resources.FightSprites.MettBomb[1];
                }
                else
                    this.Image = Resources.FightSprites.MettBomb[0];
                if(count / 2f >= _explodeDelay)
                {
                    PlaySound(Resources.Sounds.Bomb);
                    Explode();
                }
            }
            
        }

        public void Explode()
        {
            if (AbleLink) this.Broadcast("Explode");
            this.GenerateBlast();
            this.Dispose();
        }

        private void GenerateBlast()
        {
            canDestroy.ForEach(c => c.Dispose());
            Vector2 centre = this.Centre;
            GameStates.InstanceCreate(new BombBlast(centre, Resources.FightSprites.MettBombCoreBlast));
            for(int i = 0; i < 4; i++)
            {
                float rotation = i * 90;
                centre = this.Centre;
                while(centre.X < 640 && centre.X > 0 && centre.Y < 480 && centre.Y > 0)
                {
                    centre += MathUtil.GetVector2(20, rotation);
                    GameStates.InstanceCreate(new BombBlast(centre, i % 2 == 0 ?
                            Resources.FightSprites.MettBombHorBlast :
                            Resources.FightSprites.MettBombVerBlast
                        ));
                }
            }
        }
    }
}