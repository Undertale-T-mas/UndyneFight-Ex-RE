using System;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.ChampionShips;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;
using Microsoft.Xna.Framework;
using UndyneFight_Ex.Fight;

namespace Rhythm_Recall.Waves
{
    internal static class BadAppleTemp
    {
        public static ChampionShip GetChampionShip
        {
            get
            {
                SongSet fightSet = new("??????");

                fightSet.Push(typeof(BadAppleanomaly));

                return new ChampionShip(fightSet)
                {
                    Title = "",
                    SubTitle = "",
                    EditorName = "",
                    Introduce = "The deepest memory",
                    IconPath = "ChampionShips\\TCS",

                    CheckTime = () =>
                    {
                        return UndyneFight_Ex.BSet.problem ? ChampionShip.ChampionShipStates.End : ChampionShip.ChampionShipStates.NotStart;
                    }

                };
            }
        }
    }
    internal class BadAppleanomaly : IChampionShip
    {
        public BadAppleanomaly()
        {
            difficulties = new();
            difficulties.Add("XXXXX", Difficulty.ExtremePlus);
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {

            private class ThisImformation : SongInformation
            {

                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                        }
                    );
            }

            public string Music => "igallta";
            public Game() : base(0) { }
            public string FightName => "????????";
            public SongInformation Attributes => new ThisImformation();

            public void Easy()
            { }

            public void Extreme()
            { }

            public void ExtremePlus()
            { }

            public void Hard()
            {
                throw new NotImplementedException();
            }

            public void Noob()
            {
                throw new NotImplementedException();
            }

            public void Normal()
            {
                throw new NotImplementedException();
            }
            public override void Start()
            {
                BSet.windowcolor = Color.White;
                BSet.problem = false;
                Functions.InstantSetBox(new Vector2(-3000, -3000), 2, 2);
                Functions.ScreenDrawing.UIColor = Color.Transparent;
                Functions.ScreenDrawing.ThemeColor = Color.Transparent;
                Functions.ScreenDrawing.MasterAlpha = 0;
                Functions.ScreenDrawing.ScreenPositionDelta = new Vector2(9000);
                Functions.AddInstance(new InstantEvent(1, () => 
                {
                    BadApple_RE.IntoUnlockScene();
                }));
                
            }
        }
    }
}