using Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public class Flan : IChampionShip
    {
        public Flan()
        {

            difficulties = new()
            {
                { "Extra", Difficulty.ExtremePlus }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (170 / 60f)) { }
            public string Music => "Flan";

            public string FightName => "Flandre";
            private class ThisInformation : SongInformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 0f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 0f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 0f),
                        }
                    );
                public override string BarrageAuthor => "TK";
                public override string PaintAuthor => "";
                public override string SongAuthor => "";
            }
            public SongInformation Attributes => new ThisInformation();
            private bool notRegistered = true;
            public static Game game;

            public void Hard() { }
            public void Noob() { }
            public void Easy() { }
            public void Normal() { }
            public void Extreme() { }
            public void ExtremePlus()
            {
                if (InBeat(29.5f))
                {
                    CreateChart(BeatTime(4), BeatTime(1), 5, new string[]
                    {
                        "R", "", "", "", "", "", "", "",
                        "", "", "R", "", "", "", "R", "",
                        "", "", "", "", "", "", "", "",
                        "R", "", "", "", "R", "", "", "",
                        "", "R", "", "", "", "R", "", "",
                        "", "", "R", "", "", "", "R", "",
                        "", "", "", "R", "", "", "", "R",
                        "", "", "", "*R12", "", "", "", "*+012",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "R", "", "", "",
                        "R", "", "", "", "R", "", "", "",
                        "R", "", "", "", "R", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "R", "", "", "", "R", "",
                        "", "", "R", "", "", "", "R", "",
                        "", "", "R", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "",
                    });
                }
            }
            public void Start()
            {
                SetSoul(1);
                TP();
                SetGreenBox();
                GametimeDelta = BeatTime(29);
                //GametimeDelta += BeatTime(17);
                //GametimeDelta = 2120;
                //GametimeDelta = 4190;
                PlayOffset = GametimeDelta + BeatTime(0.1f);
                HeartAttribute.MaxHP = 3;
                HeartAttribute.HP = 3;
            }

            #region Functions
            private static void FourSoulArrow(float BeatCount, int Direction, int Speed = 8, int Color = 0, int Mode = 0)
            {
                int target = 0;
                Direction %= 4;
                switch (Direction)
                {
                    case 0:
                        target = RandBool() ? 2 : 3;
                        break;
                    case 1:
                        target = RandBool() ? 1 : 3;
                        break;
                    case 2:
                        target = RandBool() ? 0 : 1;
                        break;
                    case 3:
                        target = RandBool() ? 0 : 2;
                        break;
                }
                SetPlayerBoxMission(target);
                CreateArrow(60f + 62.5f / (170f / 60f) * BeatCount / 8f, Direction, Speed, Color, Mode);
            }
            #endregion

            #region Entities
            private class Hand : Entity
            {
                private float alpha = 0;
                private int time = 0, Y = 480;
                public Hand() { }

                public override void Draw()
                {
                    FormalDraw(Resources.TKSpecial.AprilFools2023.FlanHand, new(220, Y), Color.White * alpha, 7, 0, new(0, 0));
                }
                public override void Update()
                {
                    if (Y > 140) Y -= 10;
                    time++;
                    alpha += 0.05f;
                    if (time == 30) Dispose();
                }
            }
            #endregion
        }
    }
}