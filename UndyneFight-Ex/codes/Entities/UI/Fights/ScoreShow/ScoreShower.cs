using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    internal partial class StateShower : Entity
    {
        internal void PushBonus(int bonus)
        {
            score.Value = Score + bonus;
        }

        internal SongResult GenerateResult()
        {
            SongResult result = new(GenerateCurrentMark(), score, CurrentScorePercent(), AC, AP);
            return result;
        }

        #region 主要行为

        /// <summary>
        /// 增加一个得分
        /// </summary>
        /// <param name="type">得分种类。0:miss 1:okay 2:nice 3:perfect</param>
        public void PushType(int type)
        {
            if ((CurrentScene as FightScene).PlayerInstance.hpControl == null) return;
            (CurrentScene as FightScene).PlayerInstance.hpControl.GetMark(type);
            switch (type)
            {
                case 0: MissAction?.Invoke(); break;
                case 1: OkayAction?.Invoke(); break;
                case 2: NiceAction?.Invoke(); break;
                default: PerfectAction?.Invoke(); break;
            }
            if (type == 0)
            {
                if (PlayerInstance.hpControl.KR)
                    PlayerInstance.hpControl.GiveKR(1);

                combo = 0;
            }
            else
            {
                combo++;
            }

            maxCombo = Math.Max(maxCombo, combo);

            var v = new ScoreText(type == 0 ? "miss" : (type == 1 ? "okay" : (type == 2 ? "nice" : (type == 3 ? "perfect" : (type == 4 ? "perfectE" : "perfectL")))),
                type == 0 ? Color.Red : (type == 1 ? Color.Green : (type == 2 ? Color.LightBlue : (type == 3 ? Color.Gold : Color.Orange))), combo);
            current?.GetOut();
            current = v;

            totalCount++;
            if (type == 0)
            {
                miss++;
            }

            if (type == 1)
            {
                okay++;
            }

            if (type == 2)
            {
                nice++;
            }

            if (type >= 3)
            {
                perfect++;
            }

            int perfectScore = judgeState switch
            {
                JudgementState.Strict => 100,
                JudgementState.Balanced => 98,
                JudgementState.Lenient => 96,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (type != 0)
            {
                score.Value = score + (type switch
                {
                    1 => 0,
                    2 => 40,
                    3 => perfectScore,
                    4 => 80,
                    5 => 80,
                    _ => throw new NotImplementedException()
                });
            }
        }

        private int miss, okay, nice, perfect, maxCombo;
        private int totalCount = 0;
        private readonly JudgementState judgeState;
        internal int surviveTime = 0;

        private static int difficulty;

        private int combo;
        private Protected<int> score;

        public static int Score => instance.score;

        internal ScoreText current;

        internal class ScoreText : Entity
        {
            private float alpha;
            private int appearTime = 0;
            private readonly int combo;
            private readonly string text;
            private Color color;
            private float outingSpeed = 0.4f;
            private bool isOuting = false;

            public void GetOut()
            {
                if (alpha < 0.98f)
                {
                    outingSpeed = alpha / 3f;
                }
                alpha = alpha * 0.5f + 0.5f;
                if (instance != null)
                    controlLayer = instance.controlLayer;
                InstanceCreate(this);
                isOuting = true;
            }

            public ScoreText(string text, Color cl, int combo)
            {
                this.combo = combo;
                color = cl;
                color *= CurrentScene.CurrentDrawingSettings.UIColor.A / 255f;
                Centre = new Vector2(540, 80);
                this.text = text;
            }

            public override void Draw()
            {
                if (combo != 0)
                {
                    FightResources.Font.NormalFont.CentreDraw("x" + combo, Centre + new Vector2(0, 24), color * alpha, Math.Min(10, appearTime) / 10f, 0.45f);
                }

                FightResources.Font.NormalFont.CentreDraw(text, Centre, color * alpha, Math.Min(10, appearTime) / 10f, 0.45f);
            }

            public override void Update()
            {
                appearTime++;
                if (appearTime == 60)
                {
                    GetOut();
                }

                if (!isOuting)
                {
                    if (alpha <= 1f)
                    {
                        collidingBox.Y -= 1.6f * (1f - alpha);
                        alpha = alpha * 0.8f + 1.1f * 0.2f;
                    }
                }
                else
                {
                    collidingBox.Y -= 3f * outingSpeed;
                    outingSpeed += 0.06f;
                    alpha -= 0.06f;
                }
                if (alpha <= 0)
                {
                    Dispose();
                }
            }
        }

        public float CurrentScorePercent()
        {
            return MathF.Min(1, score * 1.0f / (totalCount * 100));
        }
        public static bool AC => instance.miss == 0;
        public static bool AP => (instance.miss + instance.nice + instance.okay) == 0;
        public SkillMark GenerateCurrentMark()
        {
            SkillMark mark;
            bool buffed = ((mode & GameMode.Buffed) == GameMode.Buffed);
            float scorePercent = MathF.Min(1, score * 1.0f / (totalCount * 100));
            bool AC = miss == 0;
            bool AP = (miss + okay + nice) == 0;
            mark = AP && scorePercent >= 0.997f
                ? SkillMark.Impeccable
                : (AC && okay == 0 && scorePercent >= 0.99f) || (buffed && scorePercent >= 0.995f)
                    ? SkillMark.Eminent
                    : (AC && scorePercent >= 0.98f) || (buffed && scorePercent >= 0.99f)
                                    ? SkillMark.Excellent
                                    : scorePercent >= 0.96f
                                                    ? SkillMark.Respectable
                                                    : scorePercent >= 0.9f ? SkillMark.Acceptable :
                                                    scorePercent >= 0.75f ? SkillMark.Ordinary : SkillMark.Failed;
            return mark;
        }
        internal static StateShower instance;

        private IWaveSet wave;
        private readonly GameMode mode;
        private float songDuration;

        public Action MissAction { get; set; }
        public Action OkayAction { get; set; }
        public Action NiceAction { get; set; }
        public Action PerfectAction { get; set; }
        public Action EndAction { get; set; }

        internal StateShower(IWaveSet waveSet, int difficulty, JudgementState judgeState, GameMode mode, float duration)
        {
            songDuration = duration;
            this.mode = mode;
            this.judgeState = judgeState;
            StateShower.difficulty = difficulty;
            instance = this;
            wave = waveSet;
        }

        public override void Draw()
        {
            FightResources.Font.NormalFont.CentreDraw(score.Value.ToString(), new Vector2(540, 20), GameMain.CurrentDrawingSettings.UIColor);
            if (totalCount != 0)
            {
                FightResources.Font.NormalFont.CentreDraw("p/a:" + MathF.Round((float)(perfect * 100.0 / totalCount), 1) + "%", new Vector2(100, 50), GameMain.CurrentDrawingSettings.UIColor);
                FightResources.Font.NormalFont.CentreDraw("m/a:" + MathF.Round((float)((okay + nice + perfect) * 100.0 / totalCount), 1) + "%", new Vector2(100, 80), GameMain.CurrentDrawingSettings.UIColor);
            }
            current?.Draw();
        }

        public override void Update()
        {
            surviveTime++;
            current?.Update();
            if (score.Hacked) CheatAffirmed();
        }

        public static void DisposeInstance()
        {
            instance = null;
        }
        #endregion
    }
}