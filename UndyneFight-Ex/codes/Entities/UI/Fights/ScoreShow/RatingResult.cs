using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.Fight;
using static System.MathF;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    public partial class StateShower
    {
        public partial class ResultShower
        {
            private class RatingResult : Entity
            {
                private static readonly float[] thresholds = { -10, 20, 30, 40, 50, 60, 70, 75, 80, 85, 90, float.PositiveInfinity };
                public RatingResult(float old, float cur)
                {
                    int l = 0, r = thresholds.Length - 1;
                    while (l < r)
                    {
                        int mid = (l + r + 1) >> 1;
                        if (cur < thresholds[mid]) r = mid - 1;
                        else l = mid;
                    }
                    ProgressMade = old < thresholds[l];
                    ratingShowing = new();
                    AddChild(ratingShowing);
                    oldRating = old;
                    curRating = cur;
                }
                float oldRating, curRating;
                RatingShowing ratingShowing;

                private class LineDrawer : Entity
                {
                    RatingShowing rshow;
                    public LineDrawer(RatingShowing ratingShowing)
                    {
                        rshow = ratingShowing;
                    }
                    int appearTime = 0;

                    public override void Draw()
                    {
                        float alpha = MathF.Min(1.0f, appearTime / 60f);
                        Color color = Color.White * alpha;

                        float height = 30;

                        DrawingLab.DrawLine(
                            new(0, rshow.CollidingBox.TopLeft.Y - 5), new Vector2(0, 20) + rshow.CollidingBox.TopLeft,
                            3.5f, color, 0.5f);
                        DrawingLab.DrawLine(
                            new(0, rshow.CollidingBox.TopLeft.Y - 5 + height), new Vector2(0, 20 + height) + rshow.CollidingBox.TopLeft,
                            3.5f, color, 0.5f);
                        DrawingLab.DrawLine(
                            new(640, rshow.CollidingBox.TopRight.Y - 5), new Vector2(0, 20) + rshow.CollidingBox.TopRight,
                            3.5f, color, 0.5f);
                        DrawingLab.DrawLine(
                            new(640, rshow.CollidingBox.TopRight.Y - 5 + height), new Vector2(0, 20 + height) + rshow.CollidingBox.TopRight,
                            3.5f, color, 0.5f);
                    }

                    public override void Update()
                    {
                        appearTime++;
                    }
                }
                public void IntoCentre()
                {
                    InstanceCreate(new TimeRangedEvent(100, () =>
                    {
                        var v = ratingShowing.CollidingBox;
                        v.SetCentre(Vector2.Lerp(ratingShowing.CollidingBox.GetCentre(), new(320, 180), 0.2f));
                        ratingShowing.SetArea(v);
                    }));
                    if (curRating < 85f)
                    {
                        InstanceCreate(new InstantEvent(120, () =>
                        {
                            InstanceCreate(new TextPrinter("Congratulations!", new Vector2(200, 280)));
                        }));
                        InstanceCreate(new InstantEvent(210, () =>
                        {
                            InstanceCreate(new TextPrinter("$$Don't be affected by the brilliant achievements of others!", new Vector2(50, 335), new Fight.TextSizeAttribute(0.62f), new Fight.TextSpeedAttribute(15)));
                            InstanceCreate(new TextPrinter("$$As long as you do yourself well, you are respectable.", new Vector2(70, 370), new Fight.TextSizeAttribute(0.62f), new Fight.TextSpeedAttribute(15)));
                        }));
                    }
                    else
                    {
                        InstanceCreate(new InstantEvent(120, () =>
                        {
                            InstanceCreate(new TextPrinter("Congratulations!", new Vector2(200, 280)));
                        }));
                        InstanceCreate(new InstantEvent(210, () =>
                        {
                            InstanceCreate(new TextPrinter("$$Don't be arrogant for brillant achievements of yourself!", new Vector2(50, 335), new Fight.TextSizeAttribute(0.62f), new Fight.TextSpeedAttribute(15)));
                            InstanceCreate(new TextPrinter("$$Only when you have a calm mind will you move forward.", new Vector2(70, 370), new Fight.TextSizeAttribute(0.62f), new Fight.TextSpeedAttribute(15)));
                        }));
                    }
                    ChangeState(RatingShowState.Encourage);
                }
                public bool ProgressMade { get; private init; }
                public override void Draw()
                {
                }

                public void AddCoin(int coins)
                {
                    ratingShowing.CoinString = "+" + coins;
                    AddChild(new InstantEvent(120, () =>
                    {
                        ratingShowing.CoinColor = Color.Gold;
                        ratingShowing.CoinString = PlayerManager.CurrentUser.ShopData.CashManager.Coins.ToString();
                    }));
                    float alpha = 1;
                    AddChild(new TimeRangedEvent(60, 59, () =>
                    {
                        alpha -= 1 / (59f * 2f);
                        ratingShowing.CoinColor = Color.Gold * alpha;
                    })
                    { UpdateIn120 = true });
                }

                public override void Update()
                {
                    if (curState == 0)
                    {
                        if (curRating - oldRating > 0.01f) ChangeState(RatingShowState.AddingRating);
                        else ChangeState(RatingShowState.KeepRating);
                    }
                    switch (curState)
                    {
                        case RatingShowState.AddingRating:
                            ratingShowing.SkillColor = Color.Lerp(Color.Transparent, Color.Lime, Min(1, appearTime / 30f));
                            ratingShowing.SkillString = "+" + Round(curRating - oldRating, 1);
                            if (appearTime == 90) ChangeState(RatingShowState.ShowRating);
                            break;
                        case RatingShowState.KeepRating:
                            ratingShowing.SkillColor = Color.Lerp(Color.Transparent, Color.Silver, Min(1, appearTime / 30f));
                            ratingShowing.SkillString = "no progress";
                            if (appearTime == 90) ChangeState(RatingShowState.ShowRating);
                            break;
                        case RatingShowState.ShowRating:
                            if (appearTime <= 30)
                            {
                                ratingShowing.SkillColor = Color.Lerp(ratingShowing.SkillColor, Color.Transparent, appearTime / 30f);
                            }
                            else if (appearTime >= 40)
                            {
                                ratingShowing.SkillColor = Color.Lerp(Color.Transparent, Color.White, Min(1, (appearTime - 40) / 30f));
                                ratingShowing.SkillString = Round(curRating, 2).ToString();
                            }
                            break;
                        case RatingShowState.Encourage:
                            if (appearTime <= 30)
                            {
                                ratingShowing.SkillColor = Color.Lerp(ratingShowing.SkillColor, Color.Transparent, appearTime / 30f);
                            }
                            else if (appearTime >= 40)
                            {
                                ratingShowing.SkillColor = Color.Lerp(Color.Transparent, Color.Gold, Min(1, (appearTime - 40) / 30f));
                                ratingShowing.SkillString = string.Format("{0:F1}->{1:F1}", oldRating, curRating);
                            }
                            break;
                    }
                    appearTime++;
                }
                private void ChangeState(RatingShowState state)
                {
                    curState = state;
                    appearTime = 0;
                }
                RatingShowState curState;
                int appearTime;
                enum RatingShowState
                {
                    AddingRating = 1,
                    KeepRating = 2,
                    ShowRating = 3,
                    Encourage = 4
                }
            }
        }
    }
}