using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.Remake;
using UndyneFight_Ex.SongSystem;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;
using vec2 = Microsoft.Xna.Framework.Vector2;
using col = Microsoft.Xna.Framework.Color;

namespace Rhythm_Recall.Waves
{
    class LunaticGaze : WaveConstructor, IWaveSet
    {
        public LunaticGaze() : base(62.5f / (200f / 60f)) { }
        public string Music => "Lunatic Gaze";
        public string FightName => "Lunatic Gaze";
        public SongInformation Attributes => new Information();

        public RenderProduction[] productionSlots = new RenderProduction[25];

        class Information : SongInformation
        {
            public override string SongAuthor => "solar";
            public override string BarrageAuthor => "T-mas";
            public override string AttributeAuthor => "T-mas";
            public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                        new(Difficulty.Extreme, 18.1f),
                        new(Difficulty.Hard, 14f),
                        new(Difficulty.Normal, 14f),
                        new(Difficulty.Easy, 6f),
                        new(Difficulty.Noob, 6f),
                }
            );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                        new(Difficulty.Extreme, 18.4f),
                        new(Difficulty.Hard, 14.5f),
                        new(Difficulty.Normal, 12f),
                        new(Difficulty.Easy, 7f),
                        new(Difficulty.Noob, 5f),
                }
            );
            public override Dictionary<Difficulty, float> APDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                        new(Difficulty.Extreme, 20.1f),
                        new(Difficulty.Hard, 16.5f),
                        new(Difficulty.Normal, 14f),
                        new(Difficulty.Easy, 10f),
                        new(Difficulty.Noob, 6f),
                }
            );
        }
        public void Start() {
            GametimeDelta = -BeatTime(2) + 0.95f;
            SetGreenBox();
            TP();
            SetSoul(1);
            ScreenDrawing.ScreenAngle = 0.0f;

            bool jump = false;
            if (jump)
            {
                float beat; 
                beat = 61 + 64; 
                GametimeDelta += BeatTime(beat);

                PlayOffset = BeatTime(beat);
                ScreenDrawing.MasterAlpha = 1f;
                ScreenDrawing.ScreenScale = 1f;
                Settings.GreenTap = true;
            }
        }

        public void Easy() { }

        public void Extreme()
        {
            if (GametimeF < -0.5f) return;

            if (InBeat(0))
            {
                EXIntro0();
            }
            if (InBeat(32 - 2))
            {
                EXIntro1();
            }
            if (InBeat(64 - 2))
            {
                EXIntro2();
            }
            if (InBeat(96 - 2))
            {
                EXIntro3();
            }
            if (InBeat(128 - 2))
            {
                EXIntro4();
            }
        }

        private void EXIntro2()
        {
            this.CreateChart(BeatTime(0), BeatTime(2), 6.2f, new string[]
            {
                //pre
                    "", "", "", "",      "", "", "", "",

                    "(#1.5#d1)d", "", "d", "",      "d", "", "d", "",
                    "d1(d)", "", "(#0.5#d1)d", "",      "d", "", "d(#1.5#d1)", "",
                    "d", "", "d", "",      "d", "", "d", "",
                    "d", "", "d", "",      "d", "", "d", "",

                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d1(#1.1#d)", "", "d1", "",      "d1", "", "d1", "",
                    "d1(#1.1#d)", "", "d1", "",      "d1", "", "d1", "",

                    "d(#1.5#d1)", "", "d", "",      "d", "", "d", "",
                    "d", "", "d", "",      "d", "", "d", "",
                    "d(#1.5#d1)", "", "d", "",      "d", "", "d", "",
                    "d", "", "d", "",      "d", "", "d", "",

                    "d1(#1.5#d)", "", "d1", "",      "(d1)", "", "(d1)", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d1(#1.1#d)", "", "d1", "",      "d1", "", "d1", "",
                    "d1(#0.15#d)", "", "d1", "",      "d1(+20)", "", "d1(+20)", "",
            });

        }
        private void EXIntro1()
        {
            this.CreateChart(BeatTime(0), BeatTime(2), 6.2f, new string[]
            {
                //pre
                    "", "", "", "",      "", "", "", "",

                    "(#1.5#d1)d", "", "d", "",      "d", "", "d", "",
                    "d1(d)", "", "(#0.5#d1)d", "",      "d", "", "d(#1.5#d1)", "",
                    "d", "", "d", "",      "d", "", "d", "",
                    "d", "", "d", "",      "d", "", "d", "",

                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d1(#1.1#d)", "", "d1", "",      "d1", "", "d1", "",
                    "d1(#1.1#d)", "", "d1", "",      "d1", "", "d1", "",

                    "d(#1.5#d1)", "", "d", "",      "d", "", "d", "",
                    "d(d1)", "", "d(#0.5#d1)", "",      "d", "", "d(#1.5#d1)", "",
                    "d", "", "d", "",      "d", "", "d", "",
                    "d", "", "d", "",      "d", "", "d", "",

                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d1(#1.5#d)", "", "d1", "",      "d1", "", "d1", "",
                    "d1(#1.5#d)", "", "d1", "",      "d1", "", "d1", "",
                });

        }
        private void EXIntro3()
        {
            this.CreateChart(BeatTime(0), BeatTime(2), 6.2f, new string[]
            {
                //pre
                    "", "", "", "",      "", "", "", "",

                    "(d)(+0)", "", "(+1)", "(+1)",      "(+1)(+0)", "", "(+11)(+01)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",

                    "d", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",

                    "(d)(+0)", "", "(+1)", "(+1)",      "(+1)(+0)", "", "(+11)(+01)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",

                    "(d)(+0)", "", "(+1)(+0)", "(+1)(+0)",      "(+1)(+0)", "", "(+11)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)(+0)", "", "(+11)(+01)", "(+11)",
                });

        }
        private void EXIntro4()
        {
            this.CreateChart(BeatTime(0), BeatTime(2), 6.2f, new string[]
            {
                //pre
                    "", "", "", "",      "", "", "", "",

                    "(d)(+0)", "", "(+1)", "(+1)",      "(+1)(+0)", "", "(+11)(+01)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",

                    "d", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",

                    "(d)(+0)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",

                    "(d)(+0)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                    "(+11)(+01)", "", "(+1)", "(+1)",      "(+1)", "", "(+11)", "(+11)",
                });

        }

        private void EXIntro0()
        {
            this.CreateChart(BeatTime(0), BeatTime(2), 6.2f, new string[]
            {
                //pre

                    "$0", "", "+0", "",      "+0", "", "+0", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d", "", "d", "",      "d", "", "d", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",

                    "d", "", "d", "",      "d", "", "d", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d", "", "d", "",      "d", "", "d", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",

                    "d", "", "d", "",      "d", "", "d", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d", "", "d", "",      "d", "", "d", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",

                    "d", "", "d", "",      "d", "", "d", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",
                    "d", "", "d", "",      "d", "", "d", "",
                    "d1", "", "d1", "",      "d1", "", "d1", "",

                });
        }

        public void ExtremePlus()
        {
            throw new NotImplementedException();
        }

        public void Hard() { }

        public void Noob()
        {
            return;
        }

        public void Normal()
        {
            return;
        }

    }
}
