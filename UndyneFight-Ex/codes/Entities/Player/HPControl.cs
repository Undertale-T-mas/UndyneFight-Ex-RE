using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static System.Math;
using static System.MathF;
using static UndyneFight_Ex.Entities.Particle;
using static UndyneFight_Ex.FightResources.Sounds;

namespace UndyneFight_Ex.Entities
{
    public partial class Player
    {
        public class HPControl : GameObject
        {
            private Protected<float> hp;

            private bool Buffed => ((CurrentScene as FightScene).Mode & GameMode.Buffed) != 0;
            private float missionLostSpeed = 0.005f;
            private float curLost = 0.01f;
            public float LostSpeed => Pow(curLost * 100, 0.7f);
            public float Under1HPScale => MathF.Max(0, 1 - (hp / maxHP * 5));

            public int DamageTaken { internal get; set; } = 1;

            internal float maxHP = 5;
            internal float HP { get => hp.Value; set => hp.Value = value; }

            private bool kr = false;

            public int protectTime = 0;

            public bool KR
            {
                get
                {
                    return kr;
                }
                set
                {
                    kr = value;
                }
            }

            public bool InvincibleToPhysic { get; set; } = false;

            public float KRDamage { get; set; } = 4;
            public bool KRHPExist { get => krHP > 0.0005f; }
            public void GiveKR(float intensity)
            {
                krHP += KRDamage * intensity;
            }

            private float krHP = 0;
            public float KRHP => krHP;
            private bool NoHIT => ((CurrentScene as FightScene).Mode & GameMode.NoHit) != 0;

            public float BuffDifficulty { get; set; } = 4;
            public float BuffedLevel { get; set; } = 0;

            public bool OverFlowAvailable { get; set; } = false;
            public bool ScoreProtected { get; set; } = false;

            public void GetMark(int mark)
            {
                if (mark <= 1) missionLostSpeed = (missionLostSpeed * 0.8f) + (0.2f * 0.2f);
                if (mark == 2) missionLostSpeed = (missionLostSpeed * 0.85f) + (0.12f * 0.15f);
                if (mark == 3) missionLostSpeed = (missionLostSpeed * 0.965f) + (0.0f * 0.035f);
                if (mark >= 4) missionLostSpeed = missionLostSpeed < 0.05f ?
                        ((missionLostSpeed * 0.95f) + (0.05f * 0.1f)) :
                        missionLostSpeed;
            }

            private void DoHPLose()
            {
                if (krHP > hp)
                    krHP = hp;

                float del = krHP * 0.004f;
                float krLose = 0;
                krLose += del;
                krHP -= del;
                float lose2 = Math.Min(0.004f, krHP);
                krLose += lose2;
                krHP -= lose2;
                hp.Value -= krLose;
            }
            public void GiveProtectTime(int val, bool ProtectScore = false)
            {
                protectTime = val * 2;
                ScoreProtected = ProtectScore;
            }
            public void LoseHP(Heart heart)
            {
                if (BSet.timestop) return;
                if (protectTime > 0)
                    return;

                protectTime = !kr ? 110 : 5;

                if (((CurrentScene as FightScene).Mode & GameMode.NoGreenSoul) == 0 || heart.SoulType != 1)
                {
                    if (!InvincibleToPhysic)
                        hp.Value -= DamageTaken;
                    if (kr)
                        krHP += DamageTaken;
                }
                playerHurt.CreateInstance().Play();
                CreateParticles(new Color(140, 0, 0, 100), 2f, 8f, heart.Centre, kr ? 4 : 16, 4);

                CreateParticles(Color.Lerp(heart.CurrentMoveState.StateColor * 0.4f, new Color(100, 0, 0, 100), 0.5f), 2f, 8f, heart.Centre, 6, 4);
            }

            public void ResetKR()
            {
                krHP = 0;
            }
            public void ResetHP()
            {
                ResetMaxHP(5);
            }
            public void ResetHP(int hpCnt)
            {
                hp.Value = hpCnt;
            }
            public void ResetMaxHP(float hpCnt)
            {
                maxHP = hpCnt;
                hp.Value = NoHIT ? (maxHP = 1) : maxHP;
                krHP = 0;
            }

            public void Regenerate()
            {
                krHP = 0;
                hp.Value = maxHP;

                missionLostSpeed = MathF.Min(missionLostSpeed, 0.005f);
            }
            public void Regenerate(int hp_)
            {
                hp.Value = OverFlowAvailable ? hp + hp_ : Math.Min(hp + hp_, maxHP);
                krHP = MathF.Max(0, MathF.Min(krHP, maxHP - hp));

                missionLostSpeed = MathF.Min(missionLostSpeed, 0.005f);
            }

            public override void Update()
            {
                if (protectTime > 0)
                    protectTime--;
                if (protectTime == 0) ScoreProtected = false;
                curLost = MathHelper.Lerp(curLost, missionLostSpeed, 0.05f);
                missionLostSpeed *= 0.9995f;
                if (Buffed || BuffedLevel != 0)
                {
                    float scale = 1;
                    if (!NoHIT)
                        scale = MathF.Min(1, (hp / maxHP * 5 * 0.8f) + 0.2f);
                    float scale2 = hp.Value / maxHP;
                    float recovery = MathUtil.Clamp(0, 0.03f - (scale2 * 0.03f), 0.01f);
                    float dif = BuffedLevel + (Buffed ? BuffDifficulty : 0);
                    hp.Value -= maxHP * (missionLostSpeed - (recovery * 6.5f / dif)) * 0.0014f * dif * scale;
                }
                if (kr && krHP > 0)
                    DoHPLose();

                if (hp.Hacked)
                    GameStates.CheatAffirmed();

                bool PracticeDisbled = ((CurrentScene as FightScene).Mode & GameMode.Practice) == 0;
                if (hp <= 0&&BSet.again)
                {
                    if (PracticeDisbled)
                    {
                        (CurrentScene as FightScene).PlayDeath();
                        return;
                    }
                    else
                    {
                        //想写个如果开了Practice可是没死就是没作弊可是想不到）））
                    }
                }
            }

            public HPControl()
            {
                hp.Value = 5;
                UpdateIn120 = true;
            }
        }
    }
}