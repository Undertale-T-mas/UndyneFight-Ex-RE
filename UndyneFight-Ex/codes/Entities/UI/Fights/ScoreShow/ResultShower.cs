using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.MathUtil;
using static UndyneFight_Ex.PlayerManager;
using static UndyneFight_Ex.GlobalResources.Font;
using System.Collections.Generic;

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
                perfectECount = scoreResult.perfectE;
                perfectLCount = scoreResult.perfectL;
                maxCombo = scoreResult.maxCombo;
                totalNote = missCount + okayCount + niceCount + perfectCount;
                perfectPercent = perfectCount / (1.0f * totalNote);
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
                return (totalNote == 0) ? 0 : MathF.Min(1, score * 1.0f / (totalNote * 100));
            }

            private void GenerateMark()
            {
                bool buffed = (lastMode & GameMode.Buffed) == GameMode.Buffed;
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
            private float[] dif = new float[3];
            private readonly bool AC, AP;
            private SkillMark mark;
            private bool plus;
            private JudgementState judgeState;

            private readonly int missCount, okayCount, niceCount, perfectCount, perfectECount, perfectLCount, score, maxCombo;
            private readonly float perfectPercent, hitPercent;

            private readonly string topText;
            public string[] difficultyText=new string[3];
            private readonly GameMode lastMode;
            private readonly Color topColor;
            private string ModesUsed = "";
            private int ModesUsedAmt = 0;
            private bool ModifiersUsed = false;
            bool encouraged = false;
            public static bool record;
            public override void Draw()
            {
                collidingBox = new CollideRect(200, 77, 428, 298);
                DrawingLab.DrawRectangle(CollidingBox, Color.White * alpha, 3f, 0.5f);

                if (curSelection == 0)
                    SummaryDraw();

                if (curSelection == 2)
                    RatingDraw();

                NormalFont.CentreDraw($"Result of {(!string.IsNullOrEmpty(gamePlayed.Attributes.DisplayName) ? gamePlayed.Attributes.DisplayName : gamePlayed.FightName)}:", new Vector2(320, 35), Color.White * alpha, 1.1f, 0.5f);

                // modifier used:
                float centre = ratingResult == null ? 320 : 400;
                NormalFont.CentreDraw("Modifiers: " + ModesUsed, new Vector2(centre, 391), Color.White * alpha, 0.8f, 0.5f);

                //speed
                NormalFont.CentreDraw($"Arrow speed: {Math.Round(Settings.SettingsManager.DataLibrary.ArrowSpeed, 2)}x", new Vector2(centre, 414), Color.White * alpha, 0.8f, 0.5f);

                // selection
                NormalFont.CentreDraw("Z: Leave\nR: Restart", new Vector2(centre, 447), Color.White * alpha, 0.8f, 0.5f);

                DrawingLab.DrawRectangle(new CollideRect(new Vector2(12, 78), new Vector2(177, 70)), Color.White * alpha, 3f, 0.5f);
                NormalFont.Draw("Difficulty:", new Vector2(22, 87), Color.White * alpha, 0.8f, 0);
                NormalFont.Draw(topText, new Vector2(20, 113), topColor * alpha, 0.8f, 0);
                // MarkDraw();
                string[] texts = { "Play\nsummary", "Graph\nanalyze", "Resources\ngained" };
                for (int i = 0; i < 3; i++)
                {
                    Color color = Color.White;
                    if (i == curSelection) color = Color.Gold;
                    NormalFont.Draw(texts[i], new Vector2(25, 167 + 69 * i), color * alpha, 0.8f, 0.2f);
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
                        NormalFont.CentreDraw(
                            text, new(414, height + Fight.Functions.Sin(appearTime * 1.6f) * 18),
                            Color.Goldenrod * alpha, 2.0f, GetRadian(Fight.Functions.Sin(appearTime * 2.5f) * 7), 0.9f);
                        break;
                    case SkillMark.Eminent:
                        NormalFont.CentreDraw(
                            text, new(414, height + Fight.Functions.Sin(appearTime * 1.6f) * 18),
                            Color.OrangeRed * alpha, 2.0f, GetRadian(Fight.Functions.Sin(appearTime * 1f) * 4), 0.9f);
                        break;
                    case SkillMark.Excellent:
                        NormalFont.CentreDraw(
                            text, new(414, height + Fight.Functions.Sin(appearTime * 1.6f) * 9),
                            Color.MediumPurple * alpha, 2.0f, GetRadian(7), 0.9f);
                        break;
                    case SkillMark.Respectable:
                        NormalFont.CentreDraw(
                            text, new(414, height),
                            Color.LightSkyBlue * alpha, 2.0f, 0, 0.9f);
                        break;
                    case SkillMark.Acceptable:
                        NormalFont.CentreDraw(
                            text, new(414, height),
                            Color.SpringGreen * alpha, 2.0f, 0, 0.9f);
                        break;
                    case SkillMark.Ordinary:
                        NormalFont.CentreDraw(
                            text, new(414, height),
                            Color.Green * alpha, 2.0f, 0, 0.9f);
                        break;
                    case SkillMark.Failed:
                        NormalFont.CentreDraw(
                            text, new(414, height),
                            Color.DarkRed * alpha, 2.5f, 0, 0.9f);
                        break;
                }
            }

            private void SummaryDraw()
            {
                MarkDraw();
                NormalFont.Draw("Your score:", new Vector2(214, 89 + 7), Color.White * alpha, 1.0f, 0.5f);
                NormalFont.Draw(score.ToString(), new Vector2(392, 85 + 7), Color.White * alpha, 1.2f, 0.5f);
                NormalFont.Draw($"({MathF.Round(accuracy * 100, 1)}%)", new Vector2(516, 99), Color.Silver * alpha, 0.93f, 0.5f);
                if (record)
                    NormalFont.Draw("New Record!", new Vector2(442, 251), Color.Gold * alpha * (0.5f + Fight.Functions.Sin(appearTime * 4) / 2f), 1, 0.5f);
                DrawingLab.DrawLine(new Vector2(212, 145), new Vector2(616, 145), 2, Color.Silver, 0.4f);

                if (accuracy > 0)
                {
                    if (judgeState == JudgementState.Strict) NormalFont.Draw("(S)", new(555, 208), Color.Red * alpha, 1.0f, 0.5f);
                    else if (judgeState == JudgementState.Balanced) NormalFont.Draw("(B)", new(555, 208), Color.Yellow * alpha, 1.0f, 0.5f);
                    else NormalFont.Draw("(L)", new(555, 208), Color.Lime * alpha, 1.0f, 0.5f);
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
                            NormalFont.Draw("NO HIT", new Vector2(214, 166), Color.Orange * alpha);
                        }
                        else
                        {
                            NormalFont.Draw("miss", new Vector2(214, 166), Color.Red * alpha);
                            NormalFont.Draw(missCount.ToString(), new Vector2(214 + 79, 166), Color.LightGray * alpha);
                        }

                        NormalFont.Draw("okay", new Vector2(346, 166), Color.Green * alpha);
                        NormalFont.Draw(okayCount.ToString(), new Vector2(346 + 79, 166), Color.LightGray * alpha);
                        NormalFont.Draw("nice", new Vector2(478, 166), Color.LightBlue * alpha);
                        NormalFont.Draw(niceCount.ToString(), new Vector2(478 + 79, 166), Color.LightGray * alpha);
                    }
                    else NormalFont.CentreDraw("!NO BARRAGE!", new Vector2(400, 216), Color.Red * alpha);
                }

                if (accuracy > 0)
                {
                    NormalFont.Draw("perfect", new Vector2(214, 208), Color.Yellow * alpha);
                    NormalFont.Draw($"{perfectCount} = {(int)(perfectPercent * 100)}." +
                        $"{(int)(perfectPercent * 10000) - ((int)(perfectPercent * 100)) * 100}%", new Vector2(214 + 125, 208), Color.LightGray * alpha);
                    NormalFont.Draw($"Early:{perfectECount} Late:{perfectLCount}", new(214, 238), Color.Orange * alpha, 0.7f, 0);
                    NormalFont.Draw("Max Combo:" + maxCombo, new Vector2(214, 251), Color.Silver * alpha);
                }
            }

            private void RatingDraw()
            {
                if (string.IsNullOrEmpty(difficultyText[0])) return;
                NormalFont.Draw("rating gained:", new Vector2(211, 88), Color.White, 0.95f, 0.1f);
                if (curRating > oldRating + 0.001f)
                {
                    NormalFont.Draw("+" + FloatToString(curRating - oldRating, 3), new Vector2(431, 88), Color.Lime);
                }
                else
                {
                    NormalFont.Draw("No progress", new Vector2(431, 88), Color.Silver);
                }
                Vector3 Rating = SingleCalculateRating(new Vector3(dif[0], dif[1], dif[2]), rerate);
                DrawingLab.DrawLine(new Vector2(210, 237.5f), new Vector2(618, 237.5f), 2, Color.White, 0.5f);
                NormalFont.Draw("->", new Vector2(211, 156), Color.Silver, 0.9f, 0.1f);
                NormalFont.Draw("*", new Vector2(330, 156), Color.White, 0.9f, 0.1f);
                NormalFont.Draw($"{FloatToString(rerate * 100, 1)}%({FloatToString(accuracy * 100, 1)}%)", new Vector2(354, 156), Color.White, 0.9f, 0.1f);
                if (RatingSelection == 0 && !AP || AP)
                {
                    NormalFont.Draw("->", new Vector2(211, 286), Color.Silver, 0.9f, 0.1f);
                    NormalFont.Draw("*", new Vector2(330, 286), Color.White, 0.9f, 0.1f);
                    NormalFont.Draw($"{FloatToString(rerate * 100, 1)}%({FloatToString(accuracy * 100, 1)}%)", new Vector2(354, 286), Color.White, 0.9f, 0.1f);
                }
                if (RatingSelection == 0)
                {
                    NormalFont.Draw("Complete:", new Vector2(211, 126), new(0, 255, 0), 0.95f, 0.1f);
                    NormalFont.Draw(difficultyText[0], new Vector2(261, 156), topColor, 0.9f, 0.1f);
                    NormalFont.Draw("Complete Rating:", new Vector2(211, 189), Color.White);
                    NormalFont.Draw($"{Rating.Z:F2}", new Vector2(520, 186), Color.PowderBlue, 1.2f, 0.1f);
                    NormalFont.Draw("Complex:", new Vector2(211, 256), Color.White, 0.95f, 0.1f);
                    NormalFont.Draw(difficultyText[1], new Vector2(261, 286), topColor, 0.9f, 0.1f);
                    NormalFont.Draw("Complex Rating:", new Vector2(211, 319), Color.White);
                    NormalFont.Draw($"{Rating.X:F2}", new Vector2(520, 316), Color.PowderBlue, 1.2f, 0.1f);
                }
                else
                {
                    if (AC)
                    {
                        NormalFont.Draw("FullCombo:", new Vector2(211, 126), Color.Gold, 0.95f, 0.1f);
                        NormalFont.Draw(difficultyText[2], new Vector2(261, 156), topColor, 0.9f, 0.1f);
                        NormalFont.Draw("FullCombo Rating:", new Vector2(211, 189), Color.White);
                        NormalFont.Draw($"{Rating.Y:F2}", new Vector2(520, 186), Color.Gold, 1.2f, 0.1f);
                    }
                    if (AP)
                    {
                        NormalFont.Draw("AllPerfect:", new Vector2(211, 256), Color.Yellow, 0.95f, 0.1f);
                        NormalFont.Draw(difficultyText[2], new Vector2(261, 286), topColor, 0.9f, 0.1f);
                        NormalFont.Draw("AllPerfect Rating:", new Vector2(211, 319), Color.White);
                        NormalFont.Draw($"{Rating.Y:F2}", new Vector2(520, 316), Color.Yellow, 1.2f, 0.1f);
                    }
                }
            }
            private Vector3 SingleCalculateRating(Vector3 Dif, float acc)
            {
                Tuple<float, float, float> GetDifficulty(IWaveSet waveSet, Difficulty difficulty)
                {
                    SongInformation Information = waveSet.Attributes;

                    float dif1 = 0, dif2 = 0, dif3 = 0;

                    if (Information != null)
                    {
                        if (Information.CompleteDifficulty.ContainsKey(difficulty)) dif1 = Information.CompleteDifficulty[difficulty];
                        if (Information.ComplexDifficulty.ContainsKey(difficulty)) dif2 = Information.ComplexDifficulty[difficulty];
                        if (Information.APDifficulty.ContainsKey(difficulty)) dif3 = Information.APDifficulty[difficulty];
                    }

                    return new Tuple<float, float, float>(dif1, dif2, dif3);
                }
                float apMax = 0, fcMax = 0, completeMax = 0;
                SortedSet<float> alls = new();
                Dictionary<string, IWaveSet> songType = new();
                foreach (var i in FightSystem.AllSongs.Values)
                {
                    object o = Activator.CreateInstance(i);
                    IWaveSet waveSet = o is IWaveSet ? o as IWaveSet : (o as IChampionShip).GameContent;
                    songType.Add(waveSet.FightName, waveSet);
                    for (int j = 0; j <= 5; j += 1)
                    {
                        var v = GetDifficulty(waveSet, (Difficulty)j);
                        completeMax = MathF.Max(completeMax, v.Item1);
                        fcMax = MathF.Max(fcMax, v.Item3);
                        apMax = MathF.Max(apMax, v.Item3);
                        alls.Add(v.Item2);
                    }
                }

                for (int i = 0; alls.Count < 7; i++) alls.Add(0 - i * 0.00001f);
                float sum = 0.001f, ideal = 0.001f;
                for (int i = 0; i < 7; i++)
                {
                    float g = MathF.Max(0, alls.Max);
                    alls.Remove(g);
                    ideal += g;
                }
                sum += Dif.Y * acc;
                float rating0 = sum / ideal * 85f;
                float rating1 = Dif.Z / apMax * 5f;
                float rating2 = Dif.X / completeMax * 5f;
                return new(rating0, rating1, rating2);
            }
            float ReRate(float accuracy)
            {
                if (accuracy > 1) return 1;
                float del = 1 - accuracy;
                float lim = MathF.Pow(del * 3, 0.7f) / 2.4f + del * 2.0f;
                return MathF.Max(0, 1 - lim);
            }
            float accuracy = 0, rerate = 0;
            int curSelection = 0,RatingSelection=0;
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
                {
                    if (gamePlayed.Attributes.CompleteDifficulty.ContainsKey((Difficulty)difficulty))
                    {
                        dif[0] = gamePlayed.Attributes.CompleteDifficulty[(Difficulty)difficulty];
                        difficultyText[0] = "" + dif[0];
                    }
                    if (gamePlayed.Attributes.ComplexDifficulty.ContainsKey((Difficulty)difficulty))
                    {
                        dif[1] = gamePlayed.Attributes.ComplexDifficulty[(Difficulty)difficulty];
                        difficultyText[1] = "" + dif[1];
                    }
                    if (gamePlayed.Attributes.APDifficulty.ContainsKey((Difficulty)difficulty))
                    {
                        dif[2] = gamePlayed.Attributes.APDifficulty[(Difficulty)difficulty];
                        difficultyText[2] = "" + dif[2];
                    }
                }
                accuracy = GetScorePercent();
                rerate = ReRate(accuracy);

                int lastSelection = curSelection,lastSelection2=RatingSelection;
                if (IsKeyPressed120f(InputIdentity.MainDown))
                {
                    curSelection++;
                }
                if (IsKeyPressed120f(InputIdentity.MainUp))
                {
                    curSelection--;
                }
                if (curSelection == 2 && AC)
                {
                    if (IsKeyPressed120f(InputIdentity.MainLeft))
                        RatingSelection = 0;
                    if (IsKeyPressed120f(InputIdentity.MainRight))
                        RatingSelection = 1;
                }
                curSelection = Posmod(curSelection, 3);
                if (lastSelection != curSelection || lastSelection2 != RatingSelection) Fight.Functions.PlaySound(FightResources.Sounds.changeSelection);

                analyzeShow.Enabled = curSelection == 1;

                if (IsKeyPressed120f(InputIdentity.Confirm))
                {
                    if (ratingResult == null || !ratingResult.ProgressMade || encouraged)
                        CreateNextUI();
                    else if (!encouraged)
                    {
                        encouraged = true;
                        ratingResult.IntoCentre();
                    }
                }
                if (IsKeyPressed120f(InputIdentity.Reset))
                {
                    StartSong();
                }
            }

            private void CreateNextUI()
            {
                Dispose();
                ResetScene(isRecord && UFEXSettings.RecordEnabled ?
                    new GameMenuScene(new RecordSelector())
                    : new GameMenuScene());
            }
        }
    }
}