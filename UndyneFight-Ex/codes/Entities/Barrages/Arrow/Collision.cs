using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.Settings.SettingsManager.DataLibrary;

namespace UndyneFight_Ex.Entities
{
    public partial class Arrow
    {
        public enum JudgementType
        {
            Default = 0,
            Tap = 1,
            Hold = 2
        }

        private JudgementState JudgeState
        {
            get
            {
                return GameStates.CurrentScene is SongFightingScene
                    ? (GameStates.CurrentScene as SongFightingScene).JudgeState
                    : JudgementState.Lenient;
            }
        }
        public JudgementType JudgeType { get; set; } = JudgementType.Default;
        public float BlockTime => shootShieldTime;

        float perfectNegative, perfectPositive;
        float niceNegative, nicePositive;
        float okayNegative, okayPositive;

        bool isSoundPlayed = false;

        private void PlayHitSound(float scale, bool isSettingBased = true)
        {
            if (VolumeFactor <= 0.01f) return;
            if (isSoundPlayed) return;
            isSoundPlayed = true;
            float volume = isSettingBased ? SpearBlockingVolume / 100f : 1f;

            var HitSound = SpearBlockSound switch
            {
                0 => Ding,
                1 => ArrowStuck,
                _ => throw new Exception()
            };

            PlaySound(HitSound, volume * scale * VolumeFactor);
        }
        private void Init()
        {
            switch (JudgeState)
            {
                case JudgementState.Strict:
                    perfectNegative = -2.0f; perfectPositive = 2.0f;
                    niceNegative = -3.3f; nicePositive = 3.3f;
                    okayNegative = -6.5f; okayPositive = 6.5f;
                    break;
                case JudgementState.Balanced:
                    perfectNegative = -3.3f; perfectPositive = 3.3f;
                    niceNegative = -5f; nicePositive = 5.5f;
                    okayNegative = -7.8f; okayPositive = 9f;
                    break;
                case JudgementState.Lenient:
                    perfectNegative = -4f; perfectPositive = 4.5f;
                    niceNegative = -5f; nicePositive = 7.5f;
                    okayNegative = -8.5f; okayPositive = 10f;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        private void SmartSound()
        {
            if (JudgeType == JudgementType.Default) PlayHitSound(1f);
            if (JudgeType == JudgementType.Hold) PlayHitSound(0.5f);
            if (JudgeType == JudgementType.Tap)
            {
                PlayHitSound(1f);
                PlayHitSound(1f);
            }
        }
        private void CheckCollide()
        {
            if (!mission.Shields.Exist(ArrowColor)) return;
            int curShieldWay = mission.Shields.DirectionOf(ArrowColor);
            bool attachedGB = mission.Shields.AttachedGB(ArrowColor);

            if (TimeDelta < settingDelay + 0.25f && settingDelay > 1.5f && !isSoundPlayed)
            {
                SmartSound();
            }

            //AUTOPLAY
            bool auto = false;
            foreach (Player.Heart p in Player.hearts)
            {
                if (p.Shields == null) continue;
                if (DebugState.blueShieldAuto && ArrowColor == 0)
                {
                    auto = true;
                    if (shootShieldTime - GametimeF <= 1 && curShieldWay != way)
                    {
                        p.Shields.Rotate(0, way);
                        p.Shields.ValidRotated();
                    }
                }

                if (DebugState.redShieldAuto && ArrowColor == 1)
                {
                    auto = true;
                    if (shootShieldTime - GametimeF <= 1 && curShieldWay != way)
                    {
                        p.Shields.Rotate(1, way);
                        p.Shields.ValidRotated();
                    }
                }
                if (DebugState.greenShieldAuto && ArrowColor == 2)
                {
                    auto = true;
                    if (shootShieldTime - GametimeF <= 1 && curShieldWay != way)
                    {
                        p.Shields.Rotate(2, way);
                        p.Shields.ValidRotated();
                    }
                }
                if (DebugState.purpleShieldAuto && ArrowColor == 3)
                {
                    auto = true;
                    if (shootShieldTime - GametimeF <= 1 && curShieldWay != way)
                    {
                        p.Shields.Rotate(3, way);
                        p.Shields.ValidRotated();
                    }
                }
            }

            float trueTime = rotatingType != 2
                ? mission.Shields.GetCollideChecker(ArrowColor).TimeOf(Way)
                : mission.Shields.GetCollideChecker(ArrowColor).TimeOf(Way);
            bool sameDir = mission.Shields.InSameDir(ArrowColor, way);
            if (JudgeType == JudgementType.Tap)
            {
                sameDir = false;
                trueTime = mission.Shields.GetCollideChecker(ArrowColor).TapTimeOf(Way);
            }
            else if (JudgeType == JudgementType.Hold)
            {
                sameDir = true;
                trueTime = mission.Shields.GetCollideChecker(ArrowColor).HoldTimeOf(Way);
            }

            if (auto) trueTime = 0;

            if (JudgeType == JudgementType.Tap)
            {
                float time = 0;
                if (auto && TimeDelta >= 0.5f) goto A;
                if (trueTime == 0)
                    time = TimeDelta;
                else goto A;
                float timeMax = 6.5f;
                if (JudgeState == JudgementState.Lenient) timeMax += 4.5f;
                if (JudgeState == JudgementState.Balanced) timeMax += 2.75f;
                if (time > timeMax) goto A;
                int score = GetScore(time * 1.125f);
                HitScore(score, time);
                PlayHitSound(1f);
                PlayHitSound(1f);

                Dispose();
            }
            else if (JudgeType == JudgementType.Hold)
            {
                if (TimeDelta >= 0.5f) goto A;
                if (trueTime > 5f) goto A;
                int score = GetScore(trueTime);
                HitScore(score, TimeDelta);
                PlayHitSound(0.5f);
                Dispose();
            }
            else if (TimeDelta < 0.5f || (attachedGB && TimeDelta < 12f && !auto))
            {
                if (attachedGB)
                {
                    if (sameDir)
                    {
                        if (TimeDelta >= 0.5f)
                            goto A;
                    }
                    else
                    {
                        if (trueTime != 0) goto A;
                        trueTime = TimeDelta;
                    }
                }
                float timeMax = 6;
                if (JudgeState == JudgementState.Lenient) timeMax += 3;
                if (JudgeState == JudgementState.Balanced) timeMax += 1.5f;
                if (GoldenMarkIntensity >= 1) timeMax += 2;
                if (JudgeType == JudgementType.Tap) timeMax += 2f;

                int score;
                if (trueTime > timeMax && way != curShieldWay)
                    goto A;
                if (TimeDelta < -0.5f && trueTime < timeMax) trueTime = TimeDelta;

                float time = trueTime;
                //if (lastClickTime > 6f && TimeDelta > -1.25f) goto A;
                if (sameDir) time = 0;
                score = GetScore(time);
                float del = shootShieldTime - GametimeF;

                float div = JudgeState switch
                {
                    JudgementState.Lenient => 1,
                    JudgementState.Balanced => 1.2f,
                    JudgementState.Strict => 1.5f,
                    _ => throw new ArgumentException($"{JudgeState} is not in proper form", nameof(JudgeState)),
                };
                if (score <= 1 && time > 9f / div && del >= niceNegative + 0.6f) goto A;
                if (score <= 1 && time > 15f / div && del >= okayNegative + 0.6f) goto A;

                HitScore(score, time);
                PlayHitSound(1f);
                Dispose();
                return;
            }

        A: if (distance / distanceFactor <= -34 - (hasGreenFlag ? 7 : 0))
            {
                Dispose();
                HitScore(0, -100);
                LoseHP(mission);
                GiveKR(1.2f);
                if (currentScene is SongFightingScene)
                    (currentScene as SongFightingScene).Accuracy.PushDelta(0, 0, ArrowColor, way, mission.Shields);
                return;
            }
            /*
            if ((!this.hasGreenFlag) && ((this.mission.shields[this.color].lastWay == this.way && this.mission.shields[this.color].rotateStartTime <= 6) || this.mission.shields[this.color].way == this.way) && this.distance <= 0)
            {
                this.Dispose();
                int d = Math.Max(0, Player.Shield.rotateCount - 1);
                if (this.nextHit != null && MathF.Abs(this.nextHit.shootShieldTime - this.shootShieldTime) <= 2 || DebugState.blueShieldAuto)
                    d = 0;
                if (Player.Shield.isFirstArrow) d = 0;
                Player.Shield.rotateCount = 0;
                if (this.way != this.mission.shields[this.color].way || Math.Abs(UndyneFight_Ex.Fight.Functions.Gametime - this.shootShieldTime) >= 2)
                    this.HitScore(Math.Max(1, 2 - d));
                else
                    this.HitScore(Math.Max(2, 3 - d));
                FightResources.Sounds.arrowStuck.CreateInstance().Play();
                Player.Shield.isFirstArrow = false;
            }
            else if (this.hasGreenFlag && this.distance <= 0 && this.mission.shields[this.color].rotateStartTime <= 7 &&

            (((this.mission.shields[this.color].way == this.way && this.mission.shields[this.color].lastWay == (this.way + 1) % 4) ||
            (this.mission.shields[this.color].lastWay == this.way && this.mission.shields[this.color].way == (this.way + 1) % 4)) ||

            Math.Abs(Math.Min((this.mission.shields[this.color].Rotation - this.Rotation + 360) % 360, (360 - this.mission.shields[this.color].Rotation + this.Rotation) % 360)) <= 40

            )
            )
            {
                this.Dispose();
                int d = Math.Max(0, Player.Shield.rotateCount - 4);
                if (this.nextHit != null && this.nextHit.shootShieldTime == this.shootShieldTime)
                    d = 0;
                if (Player.Shield.isFirstArrow) d = 0;
                Player.Shield.rotateCount = 0;
                if (this.distance <= -15)
                    this.HitScore(Math.Max(1, 2 - d));
                else
                    this.HitScore(Math.Max(2, 3 - d));
                FightResources.Sounds.arrowStuck.CreateInstance().Play();
                Player.Shield.isFirstArrow = false;
            }*/
        }

        private int GetScore(float time)
        {
            int score = time >= perfectNegative && time <= perfectPositive
                ? 3
                : time >= niceNegative && time <= nicePositive
                    ? time > perfectPositive ? 4 : 5
                    : time >= okayNegative && time <= okayPositive ? 2 : 1;
            return score;
        }

        private void HitScore(int score, float time)
        {
            if (!NoScore)
                Fight.AdvanceFunctions.PushScore(score);

            mission.Shields.GetCollideChecker(ArrowColor).ArrowBlock(Way);
            bool sameDir = false;
            if (GameStates.CurrentScene is SongFightingScene)
            {
                if (JudgeType != JudgementType.Tap)
                    sameDir = mission.Shields.InSameDir(ArrowColor, way);

                if (!sameDir)
                    (GameStates.CurrentScene as SongFightingScene).PlayerInstance.GameAnalyzer.PushData(new Player.ArrowData(time, score, GametimeF));

                float abs = MathF.Abs(time);
                if (abs <= 2.0f && JudgeState == JudgementState.Strict && !sameDir) Fight.AdvanceFunctions.PushBonus(5 - (abs * 2.5f));

                if (score == 3 && time > 0)
                    time /= 1.9f;
                if (sameDir && !hasGreenFlag) time = 0;

                (GameStates.CurrentScene as SongFightingScene).Accuracy.PushDelta(time, score, ArrowColor, way, mission.Shields);

                bool percise = perciseWarning;
                bool generateTip = percise ? (score != 3) : (score <= 2);
                if (JudgeType == JudgementType.Hold || ForceDisableTimeTips) { generateTip = false; }
                if (generateTip)
                {
                    Color tipscolor = Color.CornflowerBlue;
                    float xVec = 270;
                    if (ArrowColor == 0) { tipscolor = Color.CornflowerBlue; xVec = 370; }
                    if (ArrowColor == 1) { tipscolor = Color.Red; xVec = 270; }
                    if (ArrowColor == 2) { tipscolor = Color.Lime; xVec = 270; }
                    if (ArrowColor == 3) { tipscolor = Color.MediumPurple; xVec = 370; }
                    if (score >= 4) tipscolor = Color.Lerp(tipscolor, Color.Lime * 0.7f, 0.45f);
                    if (time > -1) CreateEntity(new TimeTips(new(xVec, 200), tipscolor, "early", new(0, 1)));
                    else CreateEntity(new TimeTips(new(xVec, 280), tipscolor, "late", new(0, -1)));
                }
            }
            if (score < 3 && score != 0 && ((CurrentScene as FightScene).Mode & GameMode.PerfectOnly) != 0)
            {
                Fight.AdvanceFunctions.PushScore(0); LoseHP(mission);
            }
            if (!sameDir || GoldenMarkIntensity >= 1 || JudgeState == JudgementState.Lenient)
                mission.Shields.ValidRotated();
            if (hasGreenFlag) mission.Shields.ValidRotated();
            if (score == 3) mission.Shields.Consume(0.25f);
        }

        public override void Dispose()
        {
            InstanceCreate(new BreakArrow(2, Rotation + additiveRotation, ArrowColor, rotatingType, Centre, Scale * DrawingScale));
            arrows.Remove(this);

            base.Dispose();

            if (!HasTag()) return;
            string[] strs = Tags;
            foreach (string str in strs)
            {
                taggedArrows[str].Remove(this);
            }
        }

    }
}