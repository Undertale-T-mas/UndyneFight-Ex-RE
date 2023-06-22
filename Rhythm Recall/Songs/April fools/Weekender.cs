using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public class Weekender : IChampionShip
    {
        public Weekender()
        {

            difficulties = new()
            {
                { "div.2", Difficulty.Extreme },
                { "div.1", Difficulty.ExtremePlus }
            };
        }

        private readonly Dictionary<string, Difficulty> difficulties = new();
        public Dictionary<string, Difficulty> DifficultyPanel => difficulties;

        public IWaveSet GameContent => new Game();
        public class Game : WaveConstructor, IWaveSet
        {
            public Game() : base(62.5f / (125f / 60f))
            {

            }
            public string Music => "Weekender";

            public string FightName => "Weekender";
            private class ThisImformation : SongImformation
            {
                public override Dictionary<Difficulty, float> CompleteDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 18f),
                        }
                    );
                public override Dictionary<Difficulty, float> ComplexDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 18f),
                        }
                    );
                public override Dictionary<Difficulty, float> APDifficulty => new Dictionary<Difficulty, float>(
                        new KeyValuePair<Difficulty, float>[] {
                            new(Difficulty.ExtremePlus, 21f),
                        }
                    );
                public override string BarrageAuthor => "TK";
                public override string PaintAuthor => "";
                public override string SongAuthor => "Hey! Say! Jump";
            }
            public SongImformation Attributes => new ThisImformation();
            private bool notRegistered = true;
            public static Game game;
            GlobalResources.Effects.StepSampleShader StepSample;
            ScreenDrawing.Shaders.Blur Blur;
            RenderProduction production1;
            RenderProduction production2;
            ScreenDrawing.Shaders.RGBSplitting splitter = new();
            GlobalResources.Effects.PolarShader Polar;
            public float TempVar;

            public void Hard()
            {
                if (Gametime < 0) return;
            }

            public void Noob()
            {
                if (Gametime < 0) return;
            }
            public void Easy()
            {
                if (Gametime < 0) return;
            }
            public void Normal()
            {
                if (Gametime < 0) return;
            }
            public void Extreme()
            {
                if (Gametime < 0) return;
            }
            public void ExtremePlus()
            {

            }
            public void Start()
            {
                SetSoul(1);
                TP();
                SetGreenBox();
                GametimeDelta = 240;
                PlayOffset = GametimeDelta;
            }
        }
    }
}