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
    public class Asgore : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public Asgore()
        {
            dif.Add("Div.2", Difficulty.Noob);
            dif.Add("Div.1", Difficulty.Hard);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : WaveConstructor,IWaveSet
        {
            public Project() : base(62.5f / (226f / 60f)) { }
            public string Music => "Asgore";

            public string FightName => "Asgore";

            public SongImformation Attributes => new Information();
            class Information : SongImformation
            {
                public override string SongAuthor => "Unknown";
                public override string BarrageAuthor => "Unknown";
                public override string AttributeAuthor => "Unknown";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Noob,3.0f),
                    new(Difficulty.Hard,14.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Noob,3.0f),
                    new(Difficulty.Hard,14.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Noob,7.0f),
                    new(Difficulty.Hard,18.0f)
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
            public void Easy()
            {
                throw new NotImplementedException();
            }

            public void Extreme()
            {
                throw new NotImplementedException();
            }
            #endregion
            public void Hard()
            {

            }
            public void Noob()
            {

            }
            public void Start()
            {

            }
        }
    }
}