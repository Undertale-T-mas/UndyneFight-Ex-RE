using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Entities.SimplifiedEasing;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.Fight.Functions.ScreenDrawing.Shaders;
using static UndyneFight_Ex.FightResources;
namespace Rhythm_Recall.Waves
{
    internal class The_World_Revolving : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public The_World_Revolving()
        {
            dif.Add("Div.2", Difficulty.Easy);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor,IWaveSet
        {
            public Project() : base(62.5f / (190f / 60f)) { }
            public string Music => "The World Revolving";

            public string FightName => "The World Revolving";

            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "Unknown";
                public override string BarrageAuthor => "Unknown";
                public override string AttributeAuthor => "Unknown";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Easy,5.0f),
                    new(Difficulty.Extreme,18.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,5.0f),
                    new(Difficulty.Extreme,18.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,9.0f),
                    new(Difficulty.Extreme,20.4f)
                    }
                    );
            }
            #region disused
            public void ExtremePlus()
            {
                throw new NotImplementedException();
            }

            public void Normal()
            {
                throw new NotImplementedException();
            }
            public void Hard()
            {
                throw new NotImplementedException();
            }

            public void Noob()
            {
                throw new NotImplementedException();
            }
            #endregion
            public void Extreme()
             {
                if (InBeat(0.06f))
                { 
                    BarrageCreate(BeatTime(0), BeatTime(2), 5.6f, new string[]
                    {
                            //1
                            "($0'0.7)($01'0.7)", "", "$0'0.7", "",     "($0'0.7)($01'0.7)", "", "$0'0.7", "",
                            "(d'1.2)(+01'1.2)", "", "(d'1.2)(+01'1.2)", "",     "(d'1.2)(+01'1.2)", "", "(d'1.2)(+01'1.2)", "",
                            "(D'0.7)(+01'0.7)", "", "R'0.7", "",     "(D'0.7)(+01'0.7)", "", "R'0.7", "",
                            "(d'1.2)(+01'1.2)", "", "(d'1.2)(+01'1.2)", "",     "(d'1.2)(+01'1.2)", "", "(n0'1.2)(+01'1.2)", "",

                            //2
                            "#1.5#$0", "", "", "",       "", "", "#1.5#$01", "",       
                            "", "", "", "",       "#1#$2", "", "", "",
                            "#1#$21", "", "", "",       "#1#$0", "", "", "",
                            "#1#$01", "", "", "",       "#0.5#$2", "", "", "",      

                            //3
                            "(n2'0.7)(+01'0.7)", "", "R'0.7", "",     "(d0'0.7)(+01'0.7)", "", "R'0.7", "",
                            "(d'1.2)(+01'1.2)", "", "(d'1.2)(+01'1.2)", "",     "(d'1.2)(+01'1.2)", "", "(d'1.2)(+01'1.2)", "",
                            "(D'0.7)(+01'0.7)", "", "R'0.7", "",     "(D'0.7)(+01'0.7)", "", "R'0.7", "",
                            "(d'1.2)(+01'1.2)", "", "(d'1.2)(+01'1.2)", "",     "(d'1.2)(+01'1.2)", "", "(n0'1.2)(+01'1.2)", "",
                            
                            //4 
                            "#1.5#$0", "", "", "",       "", "", "#1.5#$01", "",
                            "", "", "", "",       "#1#$2", "", "", "",
                            "#1.5#$31", "", "", "",       "", "", "$0", "$1",
                            "#1.5#$2", "", "", "",       "", "", "", "",       
                    });
                }
                if (InBeat(32 - 4))
                {
                    ArrowEase.UnitRotation("RL", EaseOut(BeatTime(4), 80, 0, EaseState.Sine));
                    ArrowEase.UnitRotation("RR", EaseOut(BeatTime(4), -80, 0, EaseState.Sine));
                    BarrageCreate(BeatTime(0), BeatTime(2), 5.6f, new string[]
                    {
                        //pre   
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                        //1   
                            "($0'1.2)($21)", "", "$1'1.2", "",       "($2'1.2)(d1)", "", "$3'1.2", "",
                            "($0'1.2)(d1)", "", "$1'1.2", "",       "($2'1.2)(d1)", "", "$3'1.2", "",
                            "($0'1.2)(d1)", "", "$1'1.2", "",       "($2'1.2)(d1)", "", "$3'1.2", "",
                            "($0'1.2)(d1)", "", "$1'1.2", "",       "($2'1.2)(d1)", "", "n11'1.2", "",
                        //2  
                            "#1.5#$1", "", "", "",       "", "", "#1.5#$11", "",
                            "", "", "", "",       "#1#$3", "", "", "",
                            "#1#$31", "", "", "",       "#1#$1", "", "", "",
                            "#1#$11", "", "", "",       "#0.5#$3", "", "", "",      
                        //3 
                            "($0'1.2)($21)", "", "$1'1.2", "",       "($2'1.2)(d1)", "", "$3'1.2", "",
                            "($0'1.2)(d1)", "", "$1'1.2", "",       "($2'1.2)(d1)", "", "$3'1.2", "",
                            "($0'1.2)(d1)", "", "$1'1.2", "",       "($2'1.2)(d1)", "", "$3'1.2", "",
                            "($0'1.2)(d1)", "", "$1'1.2", "",       "($2'1.2)(d1)", "", "n11'1.2", "",
                        //4.1
                            "$0", "~_!$0@RL", "~_!$0@RR", "~_!$0@RL",       "~_!$0@RR", "~_!$0@RL", "~_!$0@RR", "~_!$0@RL",
                            "~_!$0@RR", "~_!$0@RL", "~_!$0@RR", "~_!$0@RL",       "~_!$0@RR", "~_!$0@RL", "~_!$0@RR", "~_$0@RL",
                            "$2", "~_!$2@RL", "~_!$2@RR", "~_!$2@RL",       "~_!$2@RR", "~_!$2@RL", "~_!$2@RR", "~_!$2@RL",
                            "~_!$2@RR", "~_!$2@RL", "~_!$2@RR", "~_!$2@RL",     "", "", "", "",

                            "<<8",

                        //4.2
                            "$21", "~_!$21@RL", "~_!$21@RR", "~_!$21@RL",       "~_!$21@RR", "~_!$21@RL", "$31", "~_!$31@RL",
                            "~_!$31@RR", "~_!$31@RL", "~_!$31@RR", "~_!$31@RL",       "$11", "~_!$11@RL", "~_!$11@RR", "~_!$11@RL",
                            "$01", "~_!$01@RL", "~_!$01@RR", "~_!$01@RL",       "~_!$01@RR", "~_!$01@RL",  "$11'1.4", "$01'1.4",
                            "$11'1.4", "", "", "",       "", "", "", "",
                    });
                }
                if (InBeat(64-4))
                {
                    BarrageCreate(BeatTime(0), BeatTime(2), 5.6f, new string[]
                    {
                        //pre   
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                        //1   
                            "(R)(+0)", "", "", "",       "+1", "", "", "",
                            "+1", "", "", "",       "+1", "", "", "",
                            "+1", "", "", "",       "+1", "", "", "",
                            "+1", "", "", "",       "+1", "", "", "",

                            "*R", "*+21", "*+2", "*+21",       "*+2", "", "*+0", "",
                            "*R", "*+21", "*+2", "*+21",       "*+2", "", "*+0", "",
                            "*R1", "*+2", "*+21", "*+2",       "*+21", "", "*+01", "",
                            "*R1", "*+2", "*+21", "*+2",       "*+21", "", "*+01", "",

                            "(R)(+0)", "", "", "",       "+1", "", "", "",
                            "+1", "", "", "",       "+1", "", "", "",
                            "+1", "", "", "",       "+1", "", "", "",
                            "+1", "", "", "",       "+1", "", "", "",

                            "*R", "*+21", "*+2", "*+21",       "*+2", "", "*+0", "",
                            "*R", "*+21", "*+2", "*+21",       "*+2", "", "*+0", "",
                            "*R1", "*+2", "*+21", "*+2",       "*+21", "", "*+01", "",
                            "*R1", "*+2", "*+21", "*+2",       "*+21", "", "*+01", "",

                            "(R)(+01)", "", "", "",       "(+1)(+01)", "", "", "",
                            "(+1)(+01)", "", "", "",       "(+1)(+01)", "", "", "",
                            "(+1)(+01)", "", "", "",       "(+1)(+01)", "", "", "",
                            "(+1)(+01)", "", "", "",       "(+1)(+01)", "", "", "",

                            "*R", "*+21", "*+2", "*+21",       "*+2", "", "*+0", "",
                            "*R", "*+21", "*+2", "*+21",       "*+2", "", "*+0", "",
                            "*R1", "*+2", "*+21", "*+2",       "*+21", "", "*+01", "",
                            "*R1", "*+2", "*+21", "*+2",       "*+21", "", "*+01", "",

                            "(R)(+01)", "", "", "",       "(+1)(+01)", "", "", "",
                            "(+1)(+01)", "", "", "",       "(+1)(+01)", "", "", "",
                            "(+1)(+01)", "", "", "",       "(+1)(+01)", "", "", "",
                            "(+1)(+01)", "", "", "",       "(+1)(+01)", "", "", "",

                            "*R", "*+21", "*+2", "*+21",       "*+2", "", "*+0", "",
                            "*R", "*+21", "*+2", "*+21",       "*+2", "", "*+0", "",
                            "*R1", "*+2", "*+21", "*+2",       "*+21", "", "*+01", "",
                            "*R1", "*+2", "*+21", "*+2",       "*+21", "", "*+01", "",

                    });
                }
                if (InBeat(128 - 4))
                {
                    int val1 = Rand(0, 3);
                    int val2 = Rand(0, 3);
                    BarrageCreate(BeatTime(0), BeatTime(2), 5.6f, new string[]
                    {
                        //pre   
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                        //1
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",

                            "(R)(D1)", "","!!6/3",$"${val1}", $"${val2}1",  $"${val1}",$"${val2}1", "","R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",

                            "(R)(D1)", "","!!6/3",$"${val1}", $"${val2}1",  $"${val1}",$"${val2}1", "","R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "", "",       "", "", "", "",

                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "", "",       "(R)(D1)", "", "", "",
                            "(R)(D1)", "", "", "",       "", "", "R1", "+3",
                            "+11", "", "", "",       "", "", "", "",
                    });
                }
                if (InBeat(160 - 4))
                {
                    int val1 = Rand(0, 3);
                    int val2 = Rand(0, 3);
                    BarrageCreate(BeatTime(0), BeatTime(2), 5.6f, new string[]
                    {
                        //pre   
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                        //1
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",

                            "(R)(D1)", "","!!6/3",$"${val1}", $"${val2}1",  $"${val1}",$"${val2}1", "","R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",

                            "(R)(D1)", "","!!6/3",$"${val1}", $"${val2}1",  $"${val1}",$"${val2}1", "","R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "", "",       "", "", "", "",

                            "(R)(D1)", "", "R", "",       "R1", "", "R", "",
                            "(R)(D1)", "", "", "",       "(R)(D1)", "", "", "",
                            "(R)(D1)", "", "", "",       "", "", "R1", "+3",
                            "+11", "", "", "",       "", "", "", "",
                    });
                }
                if (InBeat(192 - 4))
                {
                    BarrageCreate(BeatTime(0), BeatTime(2), 5.6f, new string[]
                    {
                        //pre   
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "(D)(+0)", "",
                        //1
                            "+0", "+0", "+0", "+1",       "+1", "", "(D1)(+01)", "",
                            "+01", "+01", "+01", "+11",       "+11", "", "(D)(+0)", "",
                            "+0", "+0", "+0", "+1",       "+1", "", "(D1)(+01)", "",
                            "+01", "+01", "+01", "+11",       "+11", "", "(D)(+0)", "",

                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",

                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",

                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                    });
                }
                if (InBeat(222 - 4))
                {
                    BarrageCreate(BeatTime(0), BeatTime(2), 5.6f, new string[]
                    {
                        //pre   
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                        //1
                            "D", "+0", "+0", "+1",       "+1", "", "D1", "",
                            "+01", "+01", "+01", "+11",       "+11", "", "D", "",
                            "+0", "+0", "+0", "+1",       "+1", "", "D1", "",
                            "+01", "+01", "+01", "+11",       "+11", "", "D", "",

                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",

                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",

                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                            "", "", "", "",       "", "", "", "",
                    });
                }
            }
            public void Easy()
            {

            }
            public void Start()
            {
                Settings.GreenTap = true;
                HeartAttribute.MaxHP = 188;
                SetBox(new Vector2(320, 240), 84f, 84f);
                SetSoul(1);
                TP();
                GametimeDelta = -311 / 1000f * 62.5f + BeatTime(1);
                bool jump = true;
                if (jump)
                { 
                    int beat = 156;
                    GametimeDelta = -4.4f + BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}