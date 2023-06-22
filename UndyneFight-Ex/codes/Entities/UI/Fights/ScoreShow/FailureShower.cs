using Microsoft.Xna.Framework;
using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources.Sounds;
using static UndyneFight_Ex.GameStates;
using static UndyneFight_Ex.GlobalResources.Font;

namespace UndyneFight_Ex.Entities
{
    internal partial class StateShower
    {
        internal class FailureShower : Entity
        {
            private const int previousDataCounts = 10;
            private static float[] previousTimeSurvive = new float[previousDataCounts];
            private static bool changedSong = false;
            private static int tryCount = 0;
            private static int halfedScore;

            private static bool retryAvailable = false;

            public FailureShower(StateShower result)
            {
                UpdateIn120 = true;
                int halfedScore = result.score / 2;
                int timeSurvive = result.surviveTime;
                ;

                FailureShower.halfedScore = halfedScore;
                //判定是否比赛超时
                bool res1 = FightSystem.CurrentSongs != FightSystem.MainGameSongs, res2 = false;
                if (res1)
                {
                    res2 = FightSystem.currentChampionShip.CheckTime() == ChampionShips.ChampionShip.ChampionShipStates.NotAvailable;
                }
                retryAvailable = !res2;
                if ((result.mode & GameMode.RestartDeny) == GameMode.RestartDeny) retryAvailable = false;

                PlayerManager.RecordMark(result.wave.FightName, difficulty, SkillMark.Failed, instance.score / 2, false, false, 0);
                if (changedSong)
                {
                    tryCount = 0;
                    changedSong = false;
                    previousTimeSurvive = new float[previousDataCounts];
                }
                else
                {
                    tryCount++;
                }
                for (int i = 0; i < previousDataCounts - 1; i++)
                {
                    previousTimeSurvive[i + 1] = previousTimeSurvive[i];
                }

                previousTimeSurvive[0] = timeSurvive;

                retrySelector = new RetrySelector(result);
                AddChild(retrySelector);
            }

            public Selector retrySelector;

            private class RetrySelector : Selector
            {
                private float alpha = 0;
                private int appearTime = 0;
                private bool recordSaved = false;

                public RetrySelector(StateShower s) : base(false)
                {
                    SelectChanger += () =>
                    {
                        if (IsKeyPressed120f(InputIdentity.MainUp))
                        {
                            currentSelect -= 2;
                        }
                        else if (IsKeyPressed120f(InputIdentity.MainDown))
                        {
                            currentSelect += 2;
                        }
                        else if (IsKeyPressed120f(InputIdentity.MainRight))
                        {
                            currentSelect += 1;
                        }
                        else if (IsKeyPressed120f(InputIdentity.MainLeft))
                        {
                            currentSelect -= 1;
                        }
                        if (currentSelect >= SelectionCount)
                        {
                            currentSelect = SelectionCount - 1;
                        }
                        else if (currentSelect < 0)
                        {
                            currentSelect = 0;
                        }
                    };
                    SelectChanged += () => { changeSelection.CreateInstance().Play(); };

                    if (retryAvailable)
                    {
                        PushSelection(new ReTry(s.wave));
                    }

                    PushSelection(new GiveUp(s.mode));
                }

                public override void Update()
                {
                    if (alpha < 1)
                    {
                        alpha += 0.025f;
                    }

                    base.Update();

                    if (FightSystem.CurrentSongs != FightSystem.MainGameSongs)
                    {
                        return;
                    }

                    if (IsKeyPressed120f(InputIdentity.Special) && (!recordSaved) && GameInterface.UFEXSettings.RecordEnabled)
                    {
                        recordSaved = true;
                        PlaySound(heal);
                        Recorder.Save();
                    }
                    appearTime++;
                }

                public override void Draw()
                {
                    if (tryCount == 1)
                    {
                        NormalFont.CentreDraw("You lose", new Vector2(320, 85), Color.White * alpha);
                    }
                    else
                    {
                        NormalFont.CentreDraw("You lose again", new Vector2(320, 85), Color.White * alpha);
                    }

                    NormalFont.CentreDraw("Time survived : " + MathF.Round(previousTimeSurvive[0] / 60, 2) + "s", new Vector2(320, 145), Color.White * alpha, 0.92f, 0.1f);
                    NormalFont.CentreDraw("Halfed score : " + halfedScore, new Vector2(320, 180), Color.White * alpha, 0.92f, 0.1f);

                    base.Draw();

                    if (FightSystem.CurrentSongs != FightSystem.MainGameSongs)
                    {
                        return;
                    }

                    if (!GameInterface.UFEXSettings.RecordEnabled)
                    {
                        return;
                    }

                    if (!recordSaved)
                    {
                        if (appearTime % 120 <= 60 || appearTime > 360)
                        {
                            NormalFont.CentreDraw("Press C to save record", new Vector2(320, 415), Color.Gold * alpha, 0.84f, 0.1f);
                        }
                    }
                    else
                    {
                        NormalFont.CentreDraw("Record saved", new Vector2(320, 415), Color.LawnGreen * alpha, 0.84f, 0.1f);
                    }
                }
            }

            private class ReTry : TextSelection
            {
                IWaveSet wave;
                public ReTry(IWaveSet wave) : base("Try again", new Vector2(320, 250)) { Size = 1.0f; this.wave = wave; }
                public override void SelectionEvent()
                {
                    waveSet = wave;
                    StartSong();
                    base.SelectionEvent();
                }
            }

            private class GiveUp : TextSelection
            {
                private readonly GameMode mode;
                public GiveUp(GameMode againMode) : base("Quit", new Vector2(320, 300)) { mode = againMode; Size = 1.0f; }
                public override void SelectionEvent()
                {
                    tryCount = 0;
                    DisposeInstance();
                    changedSong = true;

                    InstanceCreate(new IntroUI());

                    base.SelectionEvent();
                }
            }

            public override void Draw()
            {
            }

            public override void Update()
            {
                if (retrySelector.Disposed)
                {
                    Dispose();
                    return;
                }
            }
        }
    }
}