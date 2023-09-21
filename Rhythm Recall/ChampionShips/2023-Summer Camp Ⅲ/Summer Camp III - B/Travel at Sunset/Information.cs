using System.Collections.Generic;
using UndyneFight_Ex;
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
                private static int Accessibility()
                {
                    if (PlayerManager.CurrentUser == null) return 0;
                    var customData = PlayerManager.CurrentUser.Custom;
                    if (!customData.Nexts.ContainsKey("TaSAnomaly"))
                        customData.PushNext(new("TasAnomaly:value=0"));
                    int t = customData.Nexts["TaSAnomaly"].IntValue;
                    return t;
                }

                public Information() { this.MusicOptimized = true; }
                public override string SongAuthor => "SK_kent";
                public override string BarrageAuthor => "M.T.T";
                public override string AttributeAuthor => "T-mas ";
                public override string PaintAuthor => "Unknown";
                public override string Extra => "t-Mas Tlott Tk";

                /*      public override bool Hidden => Accessibility() == 0;
                      public override HashSet<Difficulty> UnlockedDifficulties
                      {
                          get
                          {
                              HashSet<Difficulty> result = new HashSet<Difficulty>();
                              int t = Accessibility();
                              if (t >= 1) result.Add(Difficulty.Normal);
                              if (t >= 2) result.Add(Difficulty.ExtremePlus);

                              return result;
                          }
                      }*/

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