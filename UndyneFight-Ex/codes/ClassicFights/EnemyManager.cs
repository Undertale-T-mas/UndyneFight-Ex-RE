using Microsoft.Xna.Framework;
using System;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Fight
{
    public abstract class Enemy : Entity
    {
        private string name;
        private float hp, maxHp, missChance, defence, hpBarLength = 240;

        public string Name { internal get => name; set => name = value; }
        public float HP { get => hp; set => hp = value; }
        public float MissChance { internal get => missChance; set => missChance = value; }
        public float Defence { internal get => defence; set => defence = value; }
        public float HPBarLength { internal get => hpBarLength; set => hpBarLength = value; }
        public float MaxHp { get => maxHp; set => maxHp = value; }

        public Enemy() { }
        public Enemy(string name, int HP)
        {
            this.name = name;
            maxHp = hp = HP;
        }
        public Enemy(string name, int HP, float missChance, float defence)
        {
            this.name = name;
            maxHp = hp = HP;
            this.missChance = missChance;
            this.defence = defence;
        }

        protected abstract void Dodge();
        protected abstract void Attacked();

        internal void AttackInterface(float attackDamage)
        {
            if (MathUtil.GetRandom(0, 10000) < missChance * 10000 || attackDamage <= 0)
            {
                InstanceCreate(new DamageShower(ClassicFight.InterActive.NoDamageMessage, Centre - new Vector2(0, 45)));
                Dodge();
            }
            else
            {
                InstanceCreate(new DamageShower(((int)attackDamage).ToString(), Centre - new Vector2(0, 45)));
                InstanceCreate(new HPController(this, attackDamage));
            }
        }

        private class DamageShower : Entity
        {
            private readonly string result;
            private readonly float normalHeight;
            private Vector2 speed;
            private int appearTime = 0;
            public DamageShower(string showResult, Vector2 position)
            {
                Depth = 0.512f;
                appearTime = -FightStates.attackDelay;
                speed = new Vector2(Rand(-10, 10) / 10f, Rand(-10, 2) / 10f);
                normalHeight = position.Y + 60;
                Centre = position;
                result = showResult;
            }

            public override void Draw()
            {
                if (appearTime < 0) return;
                GlobalResources.Font.DamageFont.CentreDraw(result, Centre, ClassicFight.InterActive.DamageMessageColor, 1.0f, 0.512f);
            }

            public override void Update()
            {
                appearTime++;
                if (appearTime < 0) return;
                speed.Y += 9.0f / 60f;
                Centre += speed;
                if (Centre.Y >= normalHeight) { speed.Y = -speed.Y * 0.4f; speed.X *= 0.3f; Centre = new Vector2(Centre.X, normalHeight); }
                if (appearTime >= FightStates.actionDelay) Dispose();
            }
        }

        private class HPController : Entity
        {
            private readonly Enemy enemy;
            private readonly float remainHP;
            private int appearTime = 0;

            private CollideRect currentHPBar;

            public HPController(Enemy enemy, float attackDamage)
            {
                appearTime = -FightStates.attackDelay;
                Vector2 pos = enemy.Centre;
                CollideRect bound = new(0, 0, enemy.HPBarLength, 16);
                bound.SetCentre(pos + new Vector2(0, 40));
                currentHPBar = bound;
                collidingBox = bound;

                this.enemy = enemy;
                remainHP = enemy.hp - attackDamage;
            }

            public override void Draw()
            {
                if (appearTime < 0) return;
                Depth = 0.5f;
                FormalDraw(Sprites.pixUnit, collidingBox.ToRectangle(), Color.Red);
                Depth = 0.501f;
                FormalDraw(Sprites.pixUnit, currentHPBar.ToRectangle(), Color.Lime);
            }

            public override void Update()
            {
                appearTime++;
                if (appearTime == 0)
                {
                    float tmp = enemy.hp;
                    enemy.hp = remainHP;
                    PlaySound(Sounds.damaged);
                    enemy.Attacked();
                    enemy.hp = tmp;
                }
                if (appearTime >= 0)
                {
                    float scale = Math.Min(0.11f, appearTime * 0.015f + 0.015f);
                    enemy.hp = remainHP * scale + enemy.hp * (1 - scale);
                    currentHPBar.Width = enemy.hp / enemy.maxHp * enemy.HPBarLength;
                }
                if (appearTime >= FightStates.actionDelay)
                {
                    enemy.hp = remainHP;
                    Dispose();
                }
            }
        }
    }
}