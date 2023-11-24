using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using Extends;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    internal class _1111 : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public _1111()
        {
            dif.Add("Div.4", Difficulty.Noob);
            dif.Add("Div.3", Difficulty.Easy);
            dif.Add("Div.2", Difficulty.Normal);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (160f / 60f)) { }
            public string Music => "1111";
            public string FightName => "_1111";
            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "";
                public override string BarrageAuthor => "114514";
                public override string AttributeAuthor => "1919810";
                public Information() { this.MusicOptimized = true; }
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Noob,0f),
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Noob,0f),
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Noob,0f),
                        new(Difficulty.Easy,0f),
                        new(Difficulty.Normal,0f),
                        new(Difficulty.Extreme,0f),
                    }
                    );
            }
            public void Start()
            {
                InstantSetGreenBox();
                SetSoul(1);
                InstantTP(320, 240);
                bool delay = true;
                var beat = BeatTime(delay ? 80 : 0);
                PlayOffset = BeatTime(4) + beat;
                if (delay)
                {
                    GametimeDelta += beat;
                }
                else if (beat == 0)
                {
                    DrawingUtil.BetterBlackScreen(0, 0, BeatTime(32), Color.Black);
                }
            }
            public void Easy()
            {

            }

            public void Extreme()
            {
                //Intro 1
                if (InBeat(1) || InBeat(17))
                {
                    RegisterFunctionOnce("Line1", () =>
                    {
                        Line line = new(EaseIn(BeatTime(6), new Vector2(640, 485), new(0, -5), EaseState.Sine).Easing, EaseIn(BeatTime(6), 0, 90, EaseState.Sine).Easing);
                        line.ObliqueMirror = true;
                        line.Width = 4;
                        line.Alpha = 0.6f;
                        DelayBeat(6, () => line.Dispose());
                        CreateEntity(line);
                    });
                    RegisterFunctionOnce("Line2", () =>
                    {
                        Line line = new(EaseIn(BeatTime(7), new Vector2(0, -5), new(640, 5), EaseState.Sine).Easing, EaseIn(BeatTime(7), 90, 0, EaseState.Sine).Easing);
                        line.ObliqueMirror = true;
                        line.Width = 4;
                        line.Alpha = 0.6f;
                        DelayBeat(12, () => line.Dispose());
                        CreateEntity(line);
                    });
                    CreateChart(BeatTime(2), BeatTime(1), 7f, new string[]
                    {
                        "R(Line1)", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "R", "", "",
                        "", "R", "", "",         "", "", "", "",
                        "", "", "", "",         "R(Line2)", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                    });
                }
                # region Intro 2
                if (InBeat(32))
                {
                    Arrow.UnitEasing eA = new();
                    AddInstance(eA);
                    eA.ApplyTime = BeatTime(4);
                    eA.RotationEase = LinkEase(
                        EaseOut(BeatTime(2), 0, 45, EaseState.Back),
                        EaseIn(BeatTime(2), 45, 0, EaseState.Back));
                    eA.TagApply("A");
                    RegisterFunctionOnce("Line", () =>
                    {
                        bool rnd = RandBool();
                        Vector2 start = new(rnd ? Rand(0, 100) : Rand(540, 640), Rand(0, 480)), end = new(rnd ? Rand(540, 640) : Rand(0, 100), Rand(0, 480));
                        for(int i = 0; i < 4; i++)
                        {
                            Line line = new(EaseInOut(BeatTime(4), start, end, EaseState.Linear).Easing, InfLinear(i * 45f, 4));
                            line.Alpha = 0;
                            line.AlphaIncreaseAndDecrease(BeatTime(4), 1);
                            CreateEntity(line);
                        }
                    });
                    RegisterFunctionOnce("Cam", () =>
                    {
                        ScreenDrawing.CameraEffect.Convulse(17.5f, BeatTime(3), Arguments[0] == 0);
                        ScreenDrawing.CameraEffect.SizeShrink(5, BeatTime(3));
                    });
                    CreateChart(BeatTime(2), BeatTime(1), 7.3f, new string[]
                    {
                        "R", "+01", "+10", "+01",         "+10", "+01", "+10", "+01",
                        "", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "#1.8#R(Line)(<0>Cam)", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "", "", "#1.5#R(Line)(<1>Cam)", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "R", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "(#1.5#R)(R1)(Line)", "", "+21@A", "",
                        "+21@A", "", "+21@A", "",         "", "", "+21@A", "",
                        "+21@A", "", "+21@A", "",         "", "", "+21@A", "",
                        "+21@A", "", "+21@A", "",         "", "", "+21@A", "",
                        "+21@A", "", "+21@A", "",         "", "", "+21@A", "<<2",
                        "", "", "R", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "R", "",
                        "", "", "R", "",         "", "", "R", "",
                    });
                }
                if (InBeat(34))
                {
                    AddInstance(new ScreenShaker(10, 5, BeatTime(0.1f)));
                    DrawingUtil.SetScreenScale(1.3f, BeatTime(2));
                }
                if (InBeat(34, 35) && At0thBeat(0.1f)) ScreenDrawing.ScreenAngle = ScreenDrawing.ScreenAngle >= 0 ? -1.2f : 1.2f;
                if (InBeat(35)) ScreenDrawing.ScreenAngle = 0;
                if (InBeat(36)) DrawingUtil.SetScreenScale(1, BeatTime(1));
                #endregion
                #region Intro 3
                if (InBeat(55))
                {
                    RegisterFunctionOnce("Cam", ()=> DrawingUtil.SetScreenScale(1, BeatTime(2)));
                    CreateChart(BeatTime(2), BeatTime(1), 7.3f, new string[]
                    {
                        "", "", "", "",         "", "", "D", "",
                        "D", "", "D", "",         "D", "", "D", "",
                        "D", "", "", "",         "", "", "", "",
                        "#7#+0(Cam)", "", "", "",         "D1", "", "", "",
                        "", "", "D1", "",         "", "", "", "",
                        "D1", "", "", "",         "D1", "", "", "",
                        "", "", "", "",         "D1", "", "", "",
                    });
                }
                if (InBeat(56)) DrawingUtil.SetScreenScale(1.4f, BeatTime(2));
                #endregion
                //Flute
                if(InBeat(65))
                {
                    DrawingUtil.BetterBlackScreen(BeatTime(0.5f), BeatTime(0.5f), BeatTime(0.5f), Color.Black);
                    CreateChart(BeatTime(2), BeatTime(1), 7.3f, new string[]
                    {
                        "N0", "", "", "",         "N0", "", "", "",
                        "N0", "", "", "",         "$01", "", "", "",
                        "", "", "", "",         "(N0)($01)", "", "", "",
                        "", "", "", "",         "(N0)($01)", "", "", "",
                        "N0", "", "", "",         "(N0)($01)", "", "", "",
                        "N0", "", "", "",         "$01", "", "", "",
                        "", "", "", "",         "$01", "", "", "",
                        "", "", "", "",         "$01", "", "", "",
                        "N2", "", "", "",         "(N2)($21)", "", "", "",
                        "N2", "", "", "",         "$21", "", "", "",
                        "", "", "", "",         "(N2)($21)", "", "", "",
                        "N2", "", "", "",         "(N2)$21", "", "", "",
                        "N2", "", "", "",         "(N2)($21)", "", "", "",
                        "", "", "", "",         "$21", "", "", "",
                        "", "", "", "",         "$21", "", "", "",
                        "", "", "", "",         "$21", "", "$21", "",
                        "$01", "", "", "",         "", "", "", "",
                    });
                }
                if (InBeat(66))
                {
                    ScreenDrawing.ScreenScale = 1.5f;
                    ScreenDrawing.ScreenPositionDelta += new Vector2(120, 0);
                }
                if (InBeat(73.5f)) DrawingUtil.BetterBlackScreen(BeatTime(0.5f), BeatTime(0.5f), BeatTime(0.5f), Color.Black);
                if (InBeat(74.5f)) ScreenDrawing.ScreenPositionDelta -= new Vector2(240, 0);
                if (InBeat(82.75f))
                {
                    ScreenDrawing.CameraEffect.Convulse(BeatTime(0.25f), 15, true);
                    DrawingUtil.SetScreenScale(1.2f, BeatTime(0.25f));
                    DrawingUtil.LerpScreenPos(BeatTime(0.25f), new(120, 0), 0.24f);
                }
                if (InBeat(83))
                {
                    ScreenDrawing.CameraEffect.Convulse(BeatTime(0.25f), 15, false);
                    DrawingUtil.SetScreenScale(1, BeatTime(0.25f));
                    DrawingUtil.LerpScreenPos(BeatTime(0.25f), new(0), 0.24f);
                }
                if (InBeat(82))
                {
                    CreateChart(BeatTime(2), BeatTime(1), 7.3f, new string[]
                    {
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "R", "", "", "",         "R", "", "", "",
                        "", "", "", "",         "", "", "", "",
                        "", "", "", "",         "", "", "", "",
                    });
                }
                if ((InBeat(67, 82) && At0thBeat(1))|| (InBeat(83, 999) && AtKthBeat(2, BeatTime(1))))
                {
                    var CheckBeat = InBeat(67, 82) ? At0thBeat(2) : AtKthBeat(4, BeatTime(1));
                    Line line = new(EaseOut(BeatTime(2), new Vector2(320, CheckBeat ? -5 : 485), new(320, CheckBeat ? 155 : 325), EaseState.Sine).Easing, Stable(0, 0).Easing);
                    line.Width = 4;
                    RunEase((s) => line.Alpha = s, EaseOut(BeatTime(2), 1, 0, EaseState.Linear));
                    DelayBeat(2, () => line.Dispose());
                    CreateEntity(line);
                }
            }

                public void Noob()
            {

            }

            public void Normal()
            {

            }

            #region non
            public void ExtremePlus()
            {

            }

            public void Hard()
            {

            }
            #endregion
        }
    }
}
