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
    public class The_World_Revolving : IChampionShip
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
                            "$0($01)", "", "$0", "",     "$0($01)", "", "$0", "",
                            "$0($01)", "", "$0", "",     "", "", "d(d1)", "",
                            "d", "", "d", "",     "d(d1)", "", "d", "",
                            "d(d1)", "", "d", "",     "", "", "n0(n01)", "",

                            //2
                            "#1.5#$0", "", "", "",       "", "", "#1.5#$01", "",       
                            "", "", "", "",       "#1#$2", "", "", "",
                            "#1#$21", "", "", "",       "#1#$0", "", "", "",
                            "#1#$01", "", "", "",       "#0.5#$2", "", "", "",      

                            //3
                            "n20(n01)", "", "d0", "",     "d0(d1)", "", "d0", "",
                            "d0(d1)", "", "d0", "",     "", "", "d(d1)", "",
                            "d", "", "d", "",     "d(d1)", "", "d", "",
                            "d(d1)", "", "d", "",     "", "", "n0(n01)", "",
                            
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
                            "$0(d1)", "", "$1", "",       "$2(d1)", "", "$3", "",
                            "$0(d1)", "", "$1", "",       "$2", "", "$3(d1)", "",
                            "$0(d1)", "", "$1", "",       "$2(d1)", "", "$3", "",
                            "$0(d1)", "", "$1", "",       "$2", "", "$3(n11)", "",
                        //2  
                            "#1.5#$1", "", "", "",       "", "", "#1.5#$11", "",
                            "", "", "", "",       "#1#$3", "", "", "",
                            "#1#$31", "", "", "",       "#1#$1", "", "", "",
                            "#1#$11", "", "", "",       "#0.5#$3", "", "", "",      
                        //3 
                            "$0(d1)", "", "$1", "",       "$2(d1)", "", "$3", "",
                            "$0(d1)", "", "$1", "",       "$2", "", "$3(d1)", "",
                            "$0(d1)", "", "$1", "",       "$2(d1)", "", "$3", "",
                            "$0(d1)", "", "$1", "",       "$2", "", "$3(n21)", "",
                        //4.1
                            "$0", "~_!$0@RL", "~_!$0@RR", "~_!$0@RL",       "~_!$0@RR", "~_!$0@RL", "~_!$0@RR", "~_!$0@RL",
                            "~_!$0@RR", "~_!$0@RL", "~_!$0@RR", "~_!$0@RL",       "~_!$0@RR", "~_!$0@RL", "~_!$0@RR", "~_$0@RL",
                            "$2", "~_!$2@RL", "~_!$2@RR", "~_!$2@RL",       "~_!$2@RR", "~_!$2@RL", "~_!$2@RR", "~_!$2@RL",
                            "~_!$2@RR", "~_!$2@RL", "~_!$2@RR", "~_!$2@RL",     "", "", "", "",

                            "<<8",

                        //4.2
                            "$21", "~_!$21@RL", "~_!$21@RR", "~_!$21@RL",       "~_!$21@RR", "~_!$21@RL", "$31", "~_!$31@RL",
                            "~_!$31@RR", "~_!$31@RL", "~_!$31@RR", "~_!$31@RL",       "$11", "~_!$11@RL", "~_!$11@RR", "~_!$11@RL",
                            "$01", "~_!$01@RL", "~_!$01@RR", "~_!$01@RL",       "~_!$01@RR", "~_!$01@RL",  "$21", "$11",
                            "$01", "", "", "",       "", "", "", "",
                    });
                }
            }
            public void Easy()
            {

            }
            public void Start()
            { 
                HeartAttribute.MaxHP = 120;
                SetBox(new Vector2(320, 240), 84f, 84f);
                SetSoul(1);
                TP();
                GametimeDelta = -311 / 1000f * 62.5f + BeatTime(1);
                bool jump = true;
                if (jump)
                { 
                    int beat = 28;
                    GametimeDelta = -5.4f + BeatTime(beat);
                    PlayOffset = BeatTime(beat);
                    ScreenDrawing.MasterAlpha = 1f;
                    ScreenDrawing.ScreenScale = 1f;
                }
            }
        }
    }
}