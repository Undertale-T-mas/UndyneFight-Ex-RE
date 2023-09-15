using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    internal class Death_By_Glamour : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Death_By_Glamour()
        {
            dif.Add("Div.2", Difficulty.Noob);
            dif.Add("Div.1", Difficulty.Normal);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (148f / 60f)) { }
            public string Music => "Death By Glamour";

            public string FightName => "Death By Glamour";

            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "Unknown";
                public override string BarrageAuthor => "Unknown";
                public override string AttributeAuthor => "Unknown";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Noob,1.0f),
                    new(Difficulty.Normal,9.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Noob,1.0f),
                    new(Difficulty.Normal,9.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Noob,5.0f),
                    new(Difficulty.Normal,14.0f)
                    }
                    );
            }
            #region disused
            public void ExtremePlus()
            {
                throw new NotImplementedException();
            }

            public void Hard()
            {
                throw new NotImplementedException();
            }
            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }
            #endregion
            public void Normal()
            {
                if (InBeat(0))
                {

                    BarrageCreate(BeatTime(0), BeatTime(2), 5.6f, new string[]
                    {                        
                        //1
                        "$0", "", "", "",     "d", "", "", "",    
                        "d", "", "", "d",     "", "", "d", "",    
                        "", "", "d", "",     "d", "", "", "",    
                        "d", "", "", "d",     "", "", "", "",    

                        //2
                        "d1", "", "", "",     "d1", "", "", "",
                        "d1", "", "", "d1",     "", "", "d1", "",
                        "", "", "d1", "",     "d1", "", "", "",
                        "n21", "", "", "$21",     "", "", "", "",

                        //3.1  bass
                        "$21", "", "", "$21",     "", "", "$21", "",
                        "$21", "", "$21", "",     "$21", "", "$21", "",
                        "$21", "", "$21", "",     "", "", "$21", "",
                        "$21", "", "$21", "",     "", "", "", "",

                        "<<8",

                        //3.2
                        "d", "", "", "",     "d", "", "", "",
                        "d", "", "", "d",     "", "", "d", "",
                        "", "", "d", "",     "d", "", "", "",
                        "n2", "", "", "$2",     "", "", "", "",

                        //4.1  bass
                        "$2", "", "", "$2",     "", "", "$2", "",
                        "$2", "", "$2", "",     "$2", "", "$2", "",
                        "$2", "", "$2", "",     "", "", "$2", "",
                        "$2", "", "$2", "",     "", "", "", "",

                        "<<8",

                        //4.2
                        "d1", "", "", "",     "d1", "", "", "",
                        "d1", "", "", "d1",     "", "", "d1", "",
                        "", "", "d1", "",     "d1", "", "", "",
                        "n21", "", "", "$21",     "", "", "", "",
                    });
                }
                if (InBeat(30))
                {
                    BarrageCreate(BeatTime(2), BeatTime(2), 5.6f, new string[]
                    {                        
                        //1
                        "#5#$00", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "",
                        "#9#$00", "", "", "",     "+2", "", "", "", 
                        //2
                        "+0", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "",
                        "#1.5#$21", "", "", "",     "+21", "", "", "", 
                        //3
                        "#6#+21($3)", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "$31", "$31", "$11", "$11", 
                        //4
                        "#7#$31", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "", 
                        "", "", "", "",     "", "", "", "",


                        "<<16",
                        
                        //3 drums
                        "$2(!$2)", "", "($2)", "",     "($2)(!$2)", "", "($2)", "",
                        "($2)", "($2)", "($2)", "($2)",     "($2)(!$2)", "", "($2)", "",
                        "$2(!$2)", "", "($2)", "",     "($2)(!$2)", "", "($2)", "",
                        "($2)", "($2)", "($2)", "($2)",     "($2)(!$2)", "", "($2)", "",
                        //4 drums
                        "$2(!$2)", "", "($2)", "",     "($2)(!$2)", "", "($2)", "",
                        "($2)", "($2)", "($2)", "($2)",     "($2)(!$2)", "", "($2)", "",
                        "$2(!$2)", "", "($2)", "",     "($2)(!$2)", "", "($2)", "",
                        "($2)", "($2)", "($2)", "($2)",     "(<$2)(>!$2)", "", "(>$2)(<!$2)", "",
                    });
                }
            }
            public void Noob()
            {

            }
            public void Start()
            {
                HeartAttribute.MaxHP = 12;
                SetBox(new Vector2(320, 240), 84f, 84f);
                SetSoul(1);
                TP();
                GametimeDelta = -2.5f;
            }
        }
    }
}