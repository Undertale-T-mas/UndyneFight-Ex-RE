using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndyneFight_Ex.Entities;
using static UndyneFight_Ex.FightResources.FightSprites;

namespace UndyneFight_Ex.Fight
{
    internal class AttackTrail : Entity
    {
        public AttackTrail()
        {
            Image = movingBar;
            Depth = 0.6f;
            controlLayer = Surface.Hidden;
            Centre = new Vector2((FightBox.instance as RectangleBox).Left + MathUtil.GetRandom(0, 25), FightBox.instance.Centre.Y);
        }
        private bool moving = true;
        private int changeTime = 1;
        private const float movingSpeed = 5.0f;
        public override void Draw()
        {
            FormalDraw(Image, Centre, Color.White, 0, ImageCentre);
        }
        public override void Update()
        {
            if (moving)
                Centre = new Vector2(Centre.X + movingSpeed, Centre.Y);
            if ((GameStates.IsKeyPressed(InputIdentity.Confirm)) && moving)
            {
                if (ClassicFight.InterActive.AttackAnimation != null)
                    GameStates.InstanceCreate(Activator.CreateInstance(ClassicFight.InterActive.AttackAnimation) as AttackAnimation);
                float c = (300 - Math.Abs(Centre.X - 320)) / 300f;
                if (c >= 0)
                {
                    float val = MathF.Pow(c, 2) * ClassicFight.PlayerInformation.AttackDamage;
                    AttackTarget.Attacking(val);
                }
                else AttackTarget.Attacking(-1);
                moving = false;
            }
            else if (!moving)
            {
                changeTime--;
                if (changeTime <= 0)
                {
                    Image = Image == stopBar ? movingBar : stopBar;
                    changeTime = 5;
                }
            }
        }
    }
    internal class AttackTarget : Entity
    {
        public static AttackTrail attackTrail;
        private static bool isAttacking = false;

        private int appearTime = 0;
        private float alpha = 1.0f;

        public static void Attacking(float attackDamage)
        {
            isAttacking = true;
            FightStates.controllingEnemy.AttackInterface(attackDamage);
        }

        public AttackTarget()
        {
            isAttacking = false;
            Depth = 0.5f;
            controlLayer = Surface.Hidden;
            Centre = FightBox.instance.Centre;
            attackTrail = new AttackTrail();
            Image = aimer;
        }

        public override void Draw()
        {
            GameMain.MissionSpriteBatch.Draw(Image, Centre, null, Color.White * alpha, 0, ImageCentre, new Vector2(alpha, 1.0f), SpriteEffects.None, 0.5f);
        }

        public override void Update()
        {
            if (isAttacking) appearTime++;
            if (appearTime >= FightStates.attackDelay + FightStates.actionDelay)
            {
                alpha -= 0.04f;
                if (alpha < 0)
                {
                    Dispose();
                    return;
                }
                Centre = FightBox.instance.Centre;
            }
            attackTrail.Update();
        }

        public override void Dispose()
        {
            ClassicFight.DoingAction();
            base.Dispose();
        }
    }

    public abstract class AttackAnimation : Entity
    {
        internal static AttackAnimation instance;
        public AttackAnimation()
        {
            instance = this;
        }
        protected static Enemy AttackingEnemy => FightStates.controllingEnemy;
    }
    public class Slice : AttackAnimation
    {
        private static Texture2D[] slideFrames => slides;

        private int currentProgress = 0;
        private int tick = 0;
        public Slice()
        {
            Depth = 0.15f;
            Centre = AttackingEnemy.Centre;
            Image = slides[0];
            Functions.PlaySound(FightResources.Sounds.playerSlice);
        }
        public override void Draw()
        {
            if (currentProgress > 5)
            {
                Dispose();
                return;
            }
            FormalDraw(slideFrames[currentProgress], Centre, Color.White, 0, ImageCentre);
        }

        public override void Update()
        {
            tick++;
            if (tick == 7)
            {
                currentProgress++;
                tick = 0;
            }
        }
    }
}