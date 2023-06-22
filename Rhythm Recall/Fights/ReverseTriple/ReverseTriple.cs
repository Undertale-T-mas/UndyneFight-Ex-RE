using System;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class ReverseTriple : IClassicFight
    {
        private Action[] waveInitializes;
        private Action[] waveUpdates;
        private Action currentWaveUpdate;

        public string FightName => "Reverse Triple";
        private int wave = 0;
        private static int waveTime = 0;

        public void RoundChanged()
        {
            waveTime = 0;
            waveInitializes[wave]();
            currentWaveUpdate = waveUpdates[wave];
            wave++;
        }

        public void Start()
        {
            wave = 0;

            StartBattle();
            //GameStates.;
            CreateMenu();
            InterActive.AttackAnimation = typeof(Slice);

            waveInitializes = new Action[] {
                WaveLib.Wave0Initialize
            };
            waveUpdates = new Action[] {
                WaveLib.Wave0Update
            };

            HeartAttribute.MaxHP = 92;
            HeartAttribute.KR = true;

            EndSelecting();
            ChangeRound();

            HeartAttribute.KRDamage = 6;
        }

        public void Update()
        {
            waveTime++;
            currentWaveUpdate?.Invoke();
        }

        public void GameEnd()
        {
        }
    }
}