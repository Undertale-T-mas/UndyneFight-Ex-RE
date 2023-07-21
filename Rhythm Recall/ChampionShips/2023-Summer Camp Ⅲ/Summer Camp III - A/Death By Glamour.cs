using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public class Death_By_Glamour : IChampionShip
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
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Noob,1.0f),
                    new(Difficulty.Normal,9.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Noob,1.0f),
                    new(Difficulty.Normal,9.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
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
                    BarrageCreate(BeatTime(0), BeatTime(2), 6.6f, new string[]
                    {                        
                         "($00)($20)","","","", 
                        "($00)($20)","","","", "($00)($20)","","","$20", 
                        "$10","","$20","", "$20","","$10","", 
                         "($00)($20)","","","", "($00)($20)","","","$11",
                           "$31","","","", "($01)(+21)","","","",

                         "($0)(+2)","","","",  "($31)(+21)","","","$1",
                        "$21","","$0","",   "+3","","+21","",
                        "($10)($3)","","","",   "($0)($01)","","","$01",
                        "$2","","","",   "($0)(+2)","","","R",

                        "R","","","",   "($0)(+2)","","","-3",
                        "+2","","+1","",   "($0)($01)","","+1","+1",
                        "+1","","$21","",   "$1","","($0)($2)","",
                        "($0)($2)","","$11","",   "($1)($3)","","$11","$11",

                        "$11","","$11","",  "($0)(+2)","","","+1",
                        "-2","","+2","",  "($01)($11)","","$11","$11",
                        "$11","","$11","",  "$2","","($0)($11)","",
                        "($0)($11)","","$11","",  "($2)($21)","","$1","$1",

                        "-1","","+2","",  "($01)(#6#30)","","","",
                        "$1","","","",  "$1","","","$1",
                        "","","+1","",  "","","-21","",
                        "+2","","","",  "$0(#1#31)","","","$21",

                        "#1#30","","","",  "(#7#31)($2)","","","",
                        "$0","","","",  "$2","","","$1",
                        "","","$2","",  "","","$2","",
                        "$1","","","",  "($21)(#1#30)","","","$31",

                        "#1#30","","","",  "(#7#30)($3)","","$3","",
                        "$1","","+1","",  "$1","+1","$1","",
                        "$1","","-1","",  "-1","","-1","",
                        "+1","","+1","",  "-1","$1","$1","",

                        "+1","$2","$2","$2",  "$2","$2","($2)(#8#20)","",
                        "$2","","$2","",  "$2","","$2","$01",
                        "$2","","$2","",  "$01","","$01","",
                        "$2","","$2","",  "$2","","$2","",

                        "$2","","$01","$01",  "$01","","!!3/6","$2",
                        "!!3/6","","$2","","$2",   "!!3/6","$2","","$2","",
                        "$2","","($2)($21)","",  "($2)($21)","","($2)($21)","",
                        "","","","",  "","","","",

                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",

                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",

                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",


                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",

                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",

                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",

                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",

                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                        "","","","",  "","","","",
                    });
                }
            }
            public void Noob()
            {

            }
            public void Start()
            {
               
                if (InBeat(0))
                {
                    HeartAttribute.MaxHP = 12;
                    SetBox(new Vector2(320, 240), 84f, 84f);
                    SetSoul(1);
                    TP();
                }
                GametimeDelta = -BeatTime(1);
            }
        }
    }
}