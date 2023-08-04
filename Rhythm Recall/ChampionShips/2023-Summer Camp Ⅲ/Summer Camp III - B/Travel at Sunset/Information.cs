using System.Collections.Generic;
using UndyneFight_Ex.SongSystem;

namespace Rhythm_Recall.Waves
{
    public partial class Traveler_at_Sunset
    {
        public partial class Project
        {
            class Information : SongInformation
            {
                public Information() { this.MusicOptimized = true; }
                public override string SongAuthor => "SK_kent";
                public override string BarrageAuthor => "zKronO vs Tlottgodinf vs T-mas ft. TK";
                public override string AttributeAuthor => "Woem feat. ParaDOXXX and TK";
                public override string PaintAuthor => "Unknown";
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                new KeyValuePair<Difficulty, float>[]
                {
                    new(Difficulty.Normal,12.0f),
                    new(Difficulty.ExtremePlus,19.4f)
                }
                );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Normal,12.0f),
                        new(Difficulty.ExtremePlus,19.4f)
                    }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                    new KeyValuePair<Difficulty, float>[]
                    {
                        new(Difficulty.Normal,16.0f),
                        new(Difficulty.ExtremePlus,20.9f)
                    }
                    );
            }
        }
    }
}