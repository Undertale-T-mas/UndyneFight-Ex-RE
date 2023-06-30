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
    public class BIG_SHOT : IChampionShip
    {
        Dictionary<string, Difficulty> dif = new();
        public BIG_SHOT()
        {
            dif.Add("Div.2", Difficulty.Easy);
            dif.Add("Div.1", Difficulty.Extreme);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;
        class Project : IWaveSet
        {
            public string Music => "BIG SHOT";

            public string FightName => "BIG SHOT";

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
                    new(Difficulty.Easy,4.0f),
                    new(Difficulty.Extreme,16.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,4.0f),
                    new(Difficulty.Extreme,16.0f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                    new(Difficulty.Easy,8.0f),
                    new(Difficulty.Extreme,20.0f)
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
            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Hard()
            {
                throw new NotImplementedException();
            }
            #endregion
            public void Extreme()
            {

            }
            public void Easy()
            {

            }
            public void Start()
            {

            }
        }
    }
}