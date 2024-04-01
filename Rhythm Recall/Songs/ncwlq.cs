using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    class NCWLQ : WaveConstructor, IWaveSet
    {
        public NCWLQ() : base(62.5f / (150f / 60f)) { }
        public string Music => "ncwlq";
        public string FightName => "You haven't ever left (Euphoric Hardstyle Remix)";
        public SongInformation Attributes => new Information();

        public RenderProduction[] productionSlots = new RenderProduction[25];

        class Information : SongInformation
        {
            public override string SongAuthor => "PixEl";
            public override string BarrageAuthor => "T-mas";
            public override string AttributeAuthor => "T-mas";
            public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                        new(Difficulty.Extreme, 18.5f),
                        new(Difficulty.Hard, 14f),
                        new(Difficulty.Normal, 14f),
                        new(Difficulty.Easy, 6f),
                        new(Difficulty.Noob, 6f),
                }
            );
            public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                        new(Difficulty.Extreme, 18.9f),
                        new(Difficulty.Hard, 14.5f),
                        new(Difficulty.Normal, 12f),
                        new(Difficulty.Easy, 7f),
                        new(Difficulty.Noob, 5f),
                }
            );
            public override Dictionary<Difficulty, float> APDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                        new(Difficulty.Extreme, 20.5f),
                        new(Difficulty.Hard, 16.5f),
                        new(Difficulty.Normal, 14f),
                        new(Difficulty.Easy, 10f),
                        new(Difficulty.Noob, 6f),
                }
            );
        }
        public void Start()
        {
            GametimeDelta = -BeatTime(4 - 0.25f) + 5.35f;
            SetGreenBox();
            TP();
            SetSoul(1);
            ScreenDrawing.ScreenAngle = 0.0f;

            bool jump = false;
            if (jump)
            {
                float beat;
                beat = 63;
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
            if (InBeat(32))
            {
                EXIntro1();
            }
            if (InBeat(64))
            {
                EXIntro2();
            }
            if (InBeat(96))
            {
                EXIntro3();
            }
        }

        private void EXIntro2()
        {
            RegisterFunctionOnce("setR", () =>
            {
                SetSoul(0);
                SetBox(300, 660, 140);
                TP(50, 300);
                DelayBeat(0.25f, () => TP(50, 300));
                DelayBeat(0.5f, () => TP(50, 300));
                DelayBeat(0.75f, () =>
                {
                    SetSoul(2);
                    HeartAttribute.Gravity = 0.0f;
                    Heart.GiveForce(270, 0.6f);
                });
            });
            this.CreateChart(BeatTime(1.5f), BeatTime(2), 6.2f, new string[]
            {  
                    // pre
                    "", "", "", "",    "setR", "", "", "",

                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",

                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",

                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",

                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
                    "", "", "", "",    "", "", "", "",
            });
        }
        private void EXIntro1()
        {
            this.CreateChart(BeatTime(2), BeatTime(2), 6.2f, new string[]
            { 
                    // pre
                    "", "", "", "",    "", "", "", "",

                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",

                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",

                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",

                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",

            });
        }
        private void EXIntro3()
        {
            RegisterFunctionOnce("setG", () =>
            {
                TP();
                SetGreenBox();
                TP();
                Heart.RotateTo(0);
                SetSoul(1);
            });
            this.CreateChart(BeatTime(1), BeatTime(2), 6.2f, new string[]
            {  
                    // pre
                    "", "", "setG", "",    "", "", "", "",

                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",

                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",

                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",
                    "d", "", "d", "",    "d", "", "d", "",

                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "", "d1", "",
                    "d1", "", "d1", "",    "d1", "",
            });
        }

        private void EXIntro0()
        {
            this.CreateChart(BeatTime(4.35f), BeatTime(2), 6.2f, new string[] {
                    ">>0.25",
                    "", "", "", "",    "R", "", "", "",
                    "R", "", "", "",   ">>0.025",  "R", "", "R", "", ">>0.225",
                    "", "", "R", "",    "", "", "", "",  "<<0.5",
                    "", "", "", "",    "", "", "", "",

                    ">>0.05",
                    "", "", "", "",    "R", "", "R", "",
                    "R", "", "", "",    "R", "", "", "", "<<0.4525",
                    "R", "", "", "",  ">>0.3025",  "R", "", "", "", ">>0.10",
                    "", "", "", "",    "", "", "", "",

                    ">>0.115",
                    "", "", "", "",    "R", "", "", "",
                    "R", "", "", "",    "R", "", "R", "",
                    "", "", "", "R",    "", "", "", "R",
                    "", "", "", "R",    "", "", "", "",  "<<0.075",

                    "", "", "", "",    "R", "", "R", "",
                    "R", "", "", "",    "R", "", "R", "",
                    "", "", "", "R",    "", "", "", "",
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
