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
    internal class DreamTravel : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public DreamTravel()
        {
            dif.Add("Div.3", Difficulty.Easy);
            dif.Add("Div.2", Difficulty.Hard);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor, IWaveSet
        {
            public Project() : base(62.5f / (200f / 60f)) { }
            public string Music => "Dream Travel";
            public string FightName => "Dream Travel";
            public SongInformation Attributes => new Information();
            class Information : SongInformation
            {
                public override string SongAuthor => "";
                public override string BarrageAuthor => "Dream.T";
                public override string AttributeAuthor => "Dream.T";
                public Information() { this.MusicOptimized = true; }
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
 
                    new(Difficulty.Easy,0f),
                    new(Difficulty.Hard,0f),
                    new(Difficulty.Extreme,0f),

                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Easy,0f),
                    new(Difficulty.Hard,0f),
                    new(Difficulty.Extreme,0f),

                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Easy,0f),
                    new(Difficulty.Hard,0f),
                    new(Difficulty.Extreme,0f),

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
