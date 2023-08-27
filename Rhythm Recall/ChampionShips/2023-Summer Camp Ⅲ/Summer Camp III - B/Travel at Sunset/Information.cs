using System.Collections.Generic;
using UndyneFight_Ex.SongSystem;

namespace Rhythm_Recall.Waves
{
    internal partial class Traveler_at_Sunset
    {
        Dictionary<string, Difficulty> dif = new();
        public Traveler_at_Sunset()
        {
            dif.Add("Anomoly", Difficulty.Noob);
            dif.Add("Div.2", Difficulty.Normal);
            dif.Add("Div.1", Difficulty.ExtremePlus);
        }
        public IWaveSet GameContent => new Project();
        public Dictionary<string, Difficulty> DifficultyPanel => dif;

        public partial class Project
        {
            public string Music => "Traveler at Sunset";

            public string FightName => "Traveler at Sunset";

            class Information : SongInformation
            {
                public Information() { this.MusicOptimized = true; }
                public override string SongAuthor => "SK_kent";
                public override string BarrageAuthor => "M.T.T";
                public override string AttributeAuthor => "T-mas ";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Normal, 12.0f),
                    new(Difficulty.ExtremePlus, 20.0f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Normal, 12.0f),
                        new(Difficulty.ExtremePlus, 20.6f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Normal, 16.0f),
                        new(Difficulty.ExtremePlus, 21.9f)
                    }
                    );
            }
        }
    }
}