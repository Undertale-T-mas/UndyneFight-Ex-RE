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
    internal class Shadow : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Shadow()
        {
            dif.Add("Div.3", Difficulty.Easy);
            dif.Add("Div.2", Difficulty.Normal);
            dif.Add("Div.1", Difficulty.ExtremePlus);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (220f / 60f)) { }
            public string Music => "Shadow";
            public string FightName => "Shadow";
            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "";
                public override string BarrageAuthor => "Tlott";
                public override string AttributeAuthor => "Tlott";
                public Information() { this.MusicOptimized = true; }
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Easy,0f),
                    new(Difficulty.Normal,0f),
                    new(Difficulty.ExtremePlus,0f),


                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Easy,0f),
                    new(Difficulty.Normal,0f),
                    new(Difficulty.ExtremePlus,0f),
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Easy,0f),
                    new(Difficulty.Normal,0f),
                    new(Difficulty.ExtremePlus,0f),
                    }
                    );
            }
            public void Start()
            {

            }
            public void Easy()
            {
                
            }

            public void Extreme()
            {
                
            }

            public void ExtremePlus()
            {
                
            }

            public void Hard()
            {
                
            }

            public void Noob()
            {
                
            }

            public void Normal()
            {
                
            }
        }
    }
}
