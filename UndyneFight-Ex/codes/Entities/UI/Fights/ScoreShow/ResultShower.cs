using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.PlayerManager;

namespace UndyneFight_Ex.Entities
{
    public partial class StateShower
    {
        private bool isPaused = false;

        public float PauseTime { get; internal set; } = 0f;

        internal void PauseUsed()
        {
            isPaused = true;
        }

        public partial class ResultShower : Entity
        {
            RatingResult ratingResult;
            AnalyzeShow analyzeShow;

            SongPlayData playData;

            public ResultShower(StateShower scoreResult, Player.Analyzer analyzer)
            {
                analyzeShow = new(analyzer);
                AddChild(analyzeShow);
                judgeState = scoreResult.judgeState;
                gamePlayed = scoreResult.wave;
                topText = difficulty switch
                {
                    0 => "Noob Mode",
                    1 => "Easy Mode",
                    2 => "Normal Mode",
                    3 => "Hard Mode",
                    4 => "Extreme Mode",
                    5 => "Extreme Plus",
                    _ => "?"
                };
                topColor = difficulty switch
                {
                    0 => Color.White,
                    1 => Color.LawnGreen,
                    2 => Color.LightBlue,
                    3 => Color.MediumPurple,
                    4 => Color.Orange,
                    _ => Color.Gray
                };
                string songName = gamePlayed.FightName;
                lastMode = scoreResult.mode;
                missCount = scoreResult.miss;
                okayCount = scoreResult.okay;
                niceCount = scoreResult.nice;
                perfectCount = scoreResult.perfect;
                maxCombo = scoreResult.maxCombo;
                totalNote = missCount + okayCount + niceCount + perfectCount;
                perfectPercent = (perfectCount) / (1.0f * totalNote);
                hitPercent = (okayCount + niceCount + perfectCount) / (1.0f * totalNote);
                score = scoreResult.score;
                collidingBox = new CollideRect(96, 140, 448, 200);
                AC = scoreResult.miss == 0;
                AP = AC && scoreResult.okay == 0 && scoreResult.nice == 0 && totalNote > 0;
                GenerateMark();
                UpdateIn120 = true;

                #region 分数保存 
                SongResult result = new(mark, score, scoreResult.judgeState != JudgementState.Lenient ? GetScorePercent() : 0, AC, AP, scoreResult.PauseTime);
                var att = gamePlayed.Attributes;
                playData = gamePlayed.Attributes != null && gamePlayed.Attributes.ComplexDifficulty.ContainsKey((Difficulty)difficulty)
                    ? new SongPlayData()
                    {
                        Result = result,
                        Name = songName,
                        GameMode = scoreResult.mode,
                        CompleteThreshold = gamePlayed.Attributes.CompleteDifficulty[(Difficulty)difficulty],
                        ComplexThreshold = gamePlayed.Attributes.ComplexDifficulty[(Difficulty)difficulty],
                        APThreshold = gamePlayed.Attributes.APDifficulty[(Difficulty)difficulty],
                        Difficulty = (Difficulty)difficulty
                    }
                    : new SongPlayData()
                    {
                        Result = result,
                        Name = songName,
                        GameMode = scoreResult.mode,
                        CompleteThreshold = 0,
                        ComplexThreshold = 0,
                        APThreshold = 0,
                        Difficulty = (Difficulty)difficulty
                    };
                if (float.IsNaN(playData.CompleteThreshold))
                {
                    playData.CompleteThreshold = 0;
                }
                if (float.IsNaN(playData.ComplexThreshold))
                {
                    playData.ComplexThreshold = 0;
                }
                if (float.IsNaN(playData.APThreshold))
                {
                    playData.APThreshold = 0;
                }
                if (((int)scoreResult.mode & (int)GameMode.NoGreenSoul) != 0)
                {
                    PushModifiers("No Green Soul");
                }
                if (((int)scoreResult.mode & (int)GameMode.Practice) != 0)
                {
                    PushModifiers("Practice");
                }
                if (((int)scoreResult.mode & (int)GameMode.Autoplay) != 0)
                {
                    PushModifiers("AutoPlay");
                }
                if(Settings.SettingsManager.DataLibrary.PauseCheating && scoreResult.isPaused)
                {
                    PushModifiers("Paused");
                }
                if (ModifiersUsed) return;

                UFEXSettings.OnSongComplete?.Invoke(playData);

                ModesUsed = "None";
                if (CurrentUser == null) return;
                oldRating = CurrentUser.Skill;
                int oldCoins = CurrentUser.ShopData.CashManager.Coins;
                CurrentUser.ShopData.CashManager.Coins += GetRandom(50, 100);
                RecordMark(songName, difficulty, result);
                Save();
                coinAdded = CurrentUser.ShopData.CashManager.Coins - oldCoins;
                curRating = CurrentUser.Skill;
                ratingResult = new(oldRating, curRating);
                AddChild(ratingResult);
                if (coinAdded > 0) ratingResult.AddCoin(coinAdded);
                #endregion
            }

            public override void Start()
            {
                Achievements.AchievementManager.CheckSongAchievements(playData);
                Achievements.AchievementManager.CheckUserAchievements();
                //Auto sync achievements
                /*
                var user = CurrentUser;
                Task a = Task.Run(() => {
                    var ach = user._achievement;
                    foreach (var v in ach.AchievementObjects.Values)
                    {
                        var achUnit = v.TargetAchievement;
                        if (achUnit.Achieved && !achUnit.OnlineAchieved)
                        {
                            achUnit.OnlineAsync();
                        }
                    }
                });*/
            }

            private float GetScorePercent()
            {
                return ((totalNote == 0) ? 0 : MathF.Min(1, score * 1.0f / (totalNote * 100)));
            }

            private void GenerateMark()
            {
                bool buffed = ((lastMode & GameMode.Buffed) == GameMode.Buffed);
                float scorePercent = GetScorePercent();
                if (AP && scorePercent >= 0.997f)
                {
                    mark = SkillMark.Impeccable;
                }
                else if ((AC && okayCount == 0 && scorePercent >= 0.99f) || (buffed && scorePercent >= 0.995f))
                {
                    mark = SkillMark.Eminent;
                    if (buffed && scorePercent >= 0.995f && AC && okayCount == 0) plus = true;
                }
                else if ((AC && scorePercent >= 0.98f) || (buffed && scorePercent >= 0.99f))
                {
                    mark = SkillMark.Excellent;
                    if ((buffed && AC) || (scorePercent >= 0.99f && AC)) plus = true;
                }
                else if (scorePercent >= 0.96f)
                {
                    mark = SkillMark.Respectable;
                    if (scorePercent >= 0.97f) plus = true;
                }
                else if (scorePercent >= 0.9f)
                {
                    mark = SkillMark.Acceptable;
                    if (scorePercent >= 0.93f) plus = true;
                }
                else if (scorePercent >= 0.75f)
                {
                    mark = SkillMark.Ordinary;
                    if (scorePercent >= 0.85f) plus = true;
                }
                else
                {
                    mark = SkillMark.Failed;
                }
            }
            private void PushModifiers(string Name)
            {
                ModesUsed += ((ModesUsedAmt > 0) ? " + " : "") + Name;
                ModesUsedAmt++;
                ModifiersUsed = true;
            }

            private float oldRating, curRating;
            private float alpha = 0;
            private int coinAdded = 0;
            private readonly int totalNote;
            private readonly IWaveSet gamePlayed;
            private float appearTime = 0;

            private readonly bool AC, AP;
            private SkillMark mark;
            private bool plus;
            private JudgementState judgeState;

            private readonly int missCount, okayCount, niceCount, perfectCount, score, maxCombo;
            private readonly float perfectPercent, hitPercent;

            private readonly string topText;
            public string difficultyText;
            private readonly GameMode lastMode;
            private readonly Color topColor;
            private string ModesUsed = "";
            private int ModesUsedAmt = 0;
            private bool ModifiersUsed = false;
            bool encouraged = false;

            public override void Draw()
            {
                collidingBox = new CollideRect(200, 77, 428, 298);
                DrawingLab.DrawRectangle(CollidingBox, Color.White * alpha, 3f, 0.5f);

                if (curSelection == 0)
                    SummaryDraw();

                if (curSelection == 2)
                    RatingDraw();

                GlobalResources.Font.NormalFont.CentreDraw($"Result of {gamePlayed.FightName}:", new Vector2(320, 35), Color.White * alpha, 1.1f, 0.5f);

                // modifier used:
                float centre = ratingResult == null ? 320 : 400;
                GlobalResources.Font.NormalFont.CentreDraw("Modifiers: " + ModesUsed, new Vector2(centre, 391), Color.White * alpha, 0.8f, 0.5f);

                //speed
                GlobalResources.Font.NormalFont.CentreDraw($"Arrow speed: {Math.Round(Settings.SettingsManager.DataLibrary.ArrowSpeed, 2)}x", new Vector2(centre, 414), Color.White * alpha, 0.8f, 0.5f);

                // selection
                GlobalResources.Font.NormalFont.CentreDraw("Z: Leave\nR: Restart", new Vector2(centre, 447), Color.White * alpha, 0.8f, 0.5f);

                DrawingLab.DrawRectangle(new CollideRect(new Vector2(12, 78), new Vector2(177, 70)), Color.White * alpha, 3f, 0.5f);
                GlobalResources.Font.NormalFont.Draw("Difficulty:", new Vector2(22, 87), Color.White * alpha, 0.8f, 0);
                GlobalResources.Font.NormalFont.Draw(topText, new Vector2(20, 113), topColor * alpha, 0.8f, 0);
                // MarkDraw();
                string[] texts = { "Play\nsummary", "Graph\nanalyze", "Resources\ngained" };
                for (int i = 0; i < 3; i++)
                {
                    Color color = Color.White;
                    if (i == curSelection) color = Color.Gold;
                    GlobalResources.Font.NormalFont.Draw(texts[i], new Vector2(25, 167 + 69 * i), color * alpha, 0.8f, 0.2f);
                    DrawingLab.DrawLine(new Vector2(19, 225 + 69 * i), new(177, 225 + 69 * i), 2f, color * alpha, 0.2f);
                }
                DrawingLab.DrawRectangle(new CollideRect(new Vector2(12, 158), new Vector2(177, 215)), Color.White * alpha, 3, 0.5f);
            }

            private void MarkDraw()
            {
                string text = mark.ToString();
                if (plus) text += "+";
                float height = 325;
                switch (mark)
                {
                    case SkillMark.Impeccable:
                        GlobalResources.Font.NormalFont.CentreDraw(
                            text, new(414, height + Fight.Functions.Sin(appearTime * 1.6f) * 18),
                            Color.Goldenrod * alpha, 2.0f, GetRadian(Fight.Functions.Sin(appearTime * 2.5f) * 7), 0.9f);
                        break;
                    case SkillMark.Eminent:
                        GlobalResources.Font.NormalFont.CentreDraw(
                            text, new(414, height + Fight.Functions.Sin(appearTime * 1.6f) * 18),
                            Color.OrangeRed * alpha, 2.0f, GetRadian(Fight.Functions.Sin(appearTime * 1f) * 4), 0.9f);
                        break;
                    case SkillMark.Excellent:
                        GlobalResources.Font.NormalFont.CentreDraw(
                            text, new(414, height + Fight.Functions.Sin(appearTime * 1.6f) * 9),
                            Color.MediumPurple * alpha, 2.0f, GetRadian(7), 0.9f);
                        break;
                    case SkillMark.Respectable:
                        GlobalResources.Font.NormalFont.CentreDraw(
                            text, new(414, height),
                            Color.LightSkyBlue * alpha, 2.0f, 0, 0.9f);
                        break;
                    case SkillMark.Acceptable:
                        GlobalResources.Font.NormalFont.CentreDraw(
                            text, new(414, height),
                            Color.SpringGreen * alpha, 2.0f, 0, 0.9f);
                        break;
                    case SkillMark.Ordinary:
                        GlobalResources.Font.NormalFont.CentreDraw(
                            text, new(414, height),
                            Color.Green * alpha, 2.0f, 0, 0.9f);
                        break;
                    case SkillMark.Failed:
                        GlobalResources.Font.NormalFont.CentreDraw(
                            text, new(414, height),
                            Color.DarkRed * alpha, 2.5f, 0, 0.9f);
                        break;
                }
            }

            private void SummaryDraw()
            {
                MarkDraw();
                GlobalResources.Font.NormalFont.Draw("Your score:", new Vector2(214, 89 + 7), Color.White * alpha, 1.0f, 0.5f);
                GlobalResources.Font.NormalFont.Draw(score.ToString(), new Vector2(392, 85 + 7), Color.White * alpha, 1.2f, 0.5f);
                GlobalResources.Font.NormalFont.Draw($"({MathF.Round(accuracy * 100, 1)}%)", new Vector2(516, 99), Color.Silver * alpha, 0.93f, 0.5f);

                DrawingLab.DrawLine(new Vector2(212, 145), new Vector2(616, 145), 2, Color.Silver, 0.4f);

                if (accuracy > 0)
                {
                    if (judgeState == JudgementState.Strict) GlobalResources.Font.NormalFont.Draw("(S)", new(555, 208), Color.Red * alpha, 1.0f, 0.5f);
                    else if (judgeState == JudgementState.Balanced) GlobalResources.Font.NormalFont.Draw("(B)", new(555, 208), Color.Yellow * alpha, 1.0f, 0.5f);
                    else GlobalResources.Font.NormalFont.Draw("(L)", new(555, 208), Color.Lime * alpha, 1.0f, 0.5f);
                }

                if (AP)
                {
                    FormalDraw(FightResources.Sprites.allPerfectText, new(300, 166), Color.White * alpha, 0, Vector2.Zero);
                    /*
                    GlobalResources.Font.NormalFont.Draw(" ALL PERFECT ", new Vector2(100 + 8, 195 + detla), Color.Gold * 0.8f * alpha);
                    for (int i = 0; i < 4; i++)
                    {
                        GlobalResources.Font.NormalFont.Draw(" ALL PERFECT ", new Vector2(100 + 8, 195 + detla) + MathLab.GetVector2(12, appearTime * 1.0f + i * 90), new Color(DrawingLab.HsvToRgb(appearTime * 1.3f + i * 60, 255, 255, 255)) * 0.75f * alpha);
                    }*/
                }
                else
                {
                    if (accuracy > 0)
                    {
                        if (AC)
                        {
                            GlobalResources.Font.NormalFont.Draw("NO HIT", new Vector2(214, 166), Color.Orange * alpha);
                        }
                        else
                        {
                            GlobalResources.Font.NormalFont.Draw("miss", new Vector2(214, 166), Color.Red * alpha);
                            GlobalResources.Font.NormalFont.Draw(missCount.ToString(), new Vector2(214 + 79, 166), Color.LightGray * alpha);
                        }

                        GlobalResources.Font.NormalFont.Draw("okay", new Vector2(346, 166), Color.Green * alpha);
                        GlobalResources.Font.NormalFont.Draw(okayCount.ToString(), new Vector2(346 + 79, 166), Color.LightGray * alpha);
                        GlobalResources.Font.NormalFont.Draw("nice", new Vector2(478, 166), Color.LightBlue * alpha);
                        GlobalResources.Font.NormalFont.Draw(niceCount.ToString(), new Vector2(478 + 79, 166), Color.LightGray * alpha);
                    }
                    else GlobalResources.Font.NormalFont.CentreDraw("!NO BARRAGE!", new Vector2(400, 216), Color.Red * alpha);
                }

                if (accuracy > 0)
                {
                    GlobalResources.Font.NormalFont.Draw("perfect", new Vector2(214, 208), Color.Yellow * alpha);
                    GlobalResources.Font.NormalFont.Draw($"{perfectCount} = {((int)(perfectPercent * 100))}." +
                        $"{((int)(perfectPercent * 10000) - ((int)(perfectPercent * 100)) * 100)}%", new Vector2(214 + 125, 208), Color.LightGray * alpha);
                    GlobalResources.Font.NormalFont.Draw("Max Combo:" + maxCombo, new Vector2(214, 251), Color.Silver * alpha);
                }
            }

            private void RatingDraw()
            {
                if (string.IsNullOrEmpty(difficultyText)) return;
                GlobalResources.Font.NormalFont.Draw("rating gained:", new Vector2(211, 88), Color.White, 0.95f, 0.1f);
                if (curRating > oldRating + 0.001f)
                {
                    GlobalResources.Font.NormalFont.Draw("+" + FloatToString(curRating - oldRating, 3), new Vector2(431, 88), Color.Lime);
                }
                else
                {
                    GlobalResources.Font.NormalFont.Draw("No progress", new Vector2(431, 88), Color.Silver);
                }
                GlobalResources.Font.NormalFont.Draw("->", new Vector2(211, 126), Color.Silver, 0.9f, 0.1f);
                GlobalResources.Font.NormalFont.Draw(difficultyText, new Vector2(261, 126), topColor, 0.9f, 0.1f);
                GlobalResources.Font.NormalFont.Draw("*", new Vector2(330, 126), Color.White, 0.9f, 0.1f);
                GlobalResources.Font.NormalFont.Draw($"{FloatToString(rerate * 100, 1)}%({FloatToString(accuracy * 100, 1)}%)", new Vector2(354, 126), Color.White, 0.9f, 0.1f);
            }
            float ReRate(float accuracy)
            {
                if (accuracy > 1) return 1;
                float del = 1 - accuracy;
                float lim = MathF.Pow(del * 3, 0.7f) / 2.4f + del * 2.0f;
                return MathF.Max(0, 1 - lim);
            }
            float accuracy = 0, rerate = 0;
            int curSelection = 0;
            public override void Update()
            {
                appearTime += 0.5f;
                if (alpha < 1f)
                {
                    if (!encouraged)
                        alpha += 0.01f;
                }
                if (encouraged && alpha > 0.1f) alpha -= 0.01f;
                analyzeShow.Alpha = alpha;
                if (gamePlayed.Attributes != null)
                    if (gamePlayed.Attributes.ComplexDifficulty.ContainsKey((Difficulty)difficulty))
                        difficultyText = "" + gamePlayed.Attributes.ComplexDifficulty[(Difficulty)difficulty];

                accuracy = GetScorePercent();
                rerate = ReRate(accuracy);

                int lastSelection = curSelection;
                if (GameStates.IsKeyPressed120f(InputIdentity.MainDown))
                {
                    curSelection++;
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.MainUp))
                {
                    curSelection--;
                }
                curSelection = Posmod(curSelection, 3);
                if (lastSelection != curSelection) Fight.Functions.PlaySound(FightResources.Sounds.changeSelection);

                analyzeShow.Enabled = (curSelection == 1);

                if (GameStates.IsKeyPressed120f(InputIdentity.Confirm))
                {
                    if (ratingResult == null || !ratingResult.ProgressMade || encouraged)
                        CreateNextUI();
                    else if (!encouraged)
                    {
                        encouraged = true;
                        ratingResult.IntoCentre();
                    }
                }
                if (GameStates.IsKeyPressed120f(InputIdentity.Reset))
                {
                    GameStates.StartSong();
                }
            }

            private void CreateNextUI()
            {
                Dispose();
                GameStates.ResetScene(GameStates.isRecord && GameInterface.UFEXSettings.RecordEnabled ?
                    new GameMenuScene(new RecordSelector())
                    : new GameMenuScene());
            }
        }
    }
}