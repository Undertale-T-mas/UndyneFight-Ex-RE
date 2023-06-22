using Microsoft.Xna.Framework.Media;
using System;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class EndlessTrail : IClassicFight
    {
        protected static bool IsFoolMode = false;

        public EndlessTrail()
        {
            isItemUsed = false;
        }

        private static Song mainMusic;

        private Action[] waveInitializes;
        private Action[] waveUpdates;
        private Action currentWaveUpdate;

        private static bool isItemUsed;

        public virtual string FightName => "Endless Challenge";
        private int wave = 0;
        private static int waveTime = 0;
        private static int totalTime = 0;

        public void RoundChanged()
        {
            waveTime = 0;
            if (wave >= waveInitializes.Length) wave = waveInitializes.Length - 1;
            waveInitializes[wave]();
            currentWaveUpdate = waveUpdates[wave];
            wave++;
        }

        public virtual void Start()
        {
            try
            {
                mainMusic = Loader.Load<Song>("Fights\\EndlessChallenge\\CHINA DANCE");
            }
            catch
            {
            }
            wave = 0;
            totalTime = 0;
#if DEBUG
            //  this.wave = 9;
#endif

            StartBattle();
            //GameStates.;
            CreateMenu();

            HeartAttribute.JumpTimeLimit = 1;
            HeartAttribute.Gravity = 7.0f;
            HeartAttribute.JumpSpeed = 5;
            HeartAttribute.KRDamage = 8;
            HeartAttribute.Speed = 2.8f;

            CreateAction(new GameAction("check", () =>
            {
                isItemUsed = true;
                string message = "* Sans - atk 1 def 1 \n* the easiest enemy.";
                CreateEntity(new BoxMessage(message) { AfterDispose = () => ChangeRound() });
            }
            ));
            InterActive.MainMessage = "The challenge continues.";

            waveInitializes = new Action[] {
                WaveLib.Wave0Initialize ,
              /* WaveLib.Wave1Initialize ,
                WaveLib.Wave2Initialize ,
                WaveLib.Wave3Initialize ,
                WaveLib.Wave4Initialize ,
                WaveLib.Wave5Initialize ,
                WaveLib.Wave6Initialize ,
                WaveLib.Wave7Initialize ,
                WaveLib.Wave8Initialize ,
                WaveLib.Wave9Initialize ,*/
            };
            waveUpdates = new Action[] {
                WaveLib.Wave0Update ,
              /*  WaveLib.Wave1Update ,
                WaveLib.Wave2Update ,
                WaveLib.Wave3Update ,
                WaveLib.Wave4Update ,
                WaveLib.Wave5Update ,
                WaveLib.Wave6Update ,
                WaveLib.Wave7Update ,
                WaveLib.Wave8Update ,
                WaveLib.Wave9Update ,*/
            };

            HeartAttribute.MaxHP = 92;
            HeartAttribute.KR = true;

            EndSelecting();
            ChangeRound();
        }

        public void Update()
        {
            totalTime++;
            if (totalTime >= 50)
            {
                waveTime++;
            }
            if (totalTime == 50) MediaPlayer.Play(mainMusic);
            if (!RoundType && totalTime >= 50)
            {
                currentWaveUpdate?.Invoke();
            }
        }

        public void GameEnd()
        {
            mainMusic.Dispose();
            MediaPlayer.Stop();
        }
    }
}