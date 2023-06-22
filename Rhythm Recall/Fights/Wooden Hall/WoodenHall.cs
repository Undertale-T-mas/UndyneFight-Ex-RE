using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using UndyneFight_Ex;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class WoodenHall : IClassicFight
    {
        protected static bool IsFoolMode = false;

        public WoodenHall()
        {
            isItemUsed = false;
        }

        public static void PlayMusic()
        {
            isPlayMusic = true;
        }
        public static void StopMusic()
        {
            isPlayMusic = false;
        }

        private static SoundEffect mainMusic;
        private static bool isPlayMusic = false;
        private static SoundEffectInstance musicInstance;

        private Action[] waveInitializes;
        private Action[] waveUpdates;
        private Action[] itemWaveInitializes;
        private Action[] itemWaveUpdates;
        private Action currentWaveUpdate;

        private string[] fightTexts = new string[] { "",
            "I had literally thought \nit's a liberate for all\nof us.",
            "until I saw the barrier \nbroken for another \ntime.",
            "I wasn't even shocked \nwhen I saw the \nwooden corridor.",
            "cause I've already \nknown he must do \nthat.",
            "for putting everything \nto an end.",
            "Then I saw more \nwooden things. The\nhall, chairs could never\nbe forgot.",
            "Now it's my duty to\nfinish his great \nplans",
            "With extreme ease I\ncan free everyone's\nsoul.",
            "Soon you will realize \nhow it works."
        };

        private static bool isItemUsed;

        public virtual string FightName => "Wooden Hall";
        private int wave = 0;
        private static int waveTime = 0;

        public void RoundChanged()
        {
            waveTime = 0;
            if (isItemUsed)
            {
                currentWaveUpdate = itemWaveUpdates[Rand(0, 0)];
                itemWaveInitializes[LastRand]();
                isItemUsed = false;
            }
            else
            {
                if (wave >= waveInitializes.Length) wave = waveInitializes.Length - 1;
                waveInitializes[wave]();
                currentWaveUpdate = waveUpdates[wave];
                wave++;
            }
        }

        public virtual void Start()
        {
            Sans.LoadingResources();
            IsFoolMode = false;
            try
            {
                mainMusic = Loader.Load<SoundEffect>("Fights\\WoodenHall\\MEGALOVANIA");
            }
            catch
            {
            }
            wave = 0;
#if DEBUG
            //  this.wave = 9;
#endif

            StartBattle();
            //GameStates.;
            CreateMenu();

            HeartAttribute.JumpTimeLimit = 1;
            HeartAttribute.Gravity = 7.0f;
            HeartAttribute.JumpSpeed = 5;
            HeartAttribute.KRDamage = 5;
            HeartAttribute.Speed = 2.8f;

            CreateAction(new GameAction("check", () =>
            {
                isItemUsed = true;
                string message = "* Sans - atk 1 def 1 \n* the easiest enemy.";
                CreateEntity(new BoxMessage(message) { AfterDispose = () => ChangeRound() });
            }
            ));
            CreateItem(new Item("Pie", () =>
            {
                isItemUsed = true; Regenerate(99);
                PlaySound(FightResources.Sounds.heal);
                string message = "* You ate pie.\n";
                if (HeartAttribute.IsFullHP)
                    message += "* Your HP maxed out.";
                else message += "* You recovered 100 HP.";
                CreateEntity(new BoxMessage(message) { AfterDispose = () => ChangeRound() });
            }
            ));
            CreateItem(new Item("Instant noodles", () =>
            {
                isItemUsed = true; Regenerate(90);
                PlaySound(FightResources.Sounds.heal);
                string message = "* You ate the instant noodles.\n";
                if (HeartAttribute.IsFullHP)
                    message += "* Your HP maxed out.";
                else message += "* You recovered 90 HP.";
                CreateEntity(new BoxMessage(message) { AfterDispose = () => ChangeRound() });
            }
            ));
            CreateItem(new Item("Steak", () =>
            {
                isItemUsed = true; Regenerate(60);
                PlaySound(FightResources.Sounds.heal);
                string message = "* You ate the steak.\n";
                if (HeartAttribute.IsFullHP)
                    message += "* Your HP maxed out.";
                else message += "* You recovered 60 HP.";
                CreateEntity(new BoxMessage(message) { AfterDispose = () => ChangeRound() });
            }
            ));
            CreateEnemy(new Sans());
            InterActive.AttackAnimation = typeof(Slice);
            InterActive.MainMessage = "A strange feeling ...";
            InterActive.EventAfterAction = () => CreateEntity(new DialogBox(new Vector2(380, 90), "#sans#" + fightTexts[wave])
            {
                AfterDispose = () => { ChangeRound(); }
            });

            waveInitializes = new Action[] {
                WaveLib.Wave0Initialize ,
                WaveLib.Wave1Initialize ,
                WaveLib.Wave2Initialize ,
                WaveLib.Wave3Initialize ,
                WaveLib.Wave4Initialize ,
                WaveLib.Wave5Initialize ,
                WaveLib.Wave6Initialize ,
                WaveLib.Wave7Initialize ,
                WaveLib.Wave8Initialize ,
                WaveLib.Wave9Initialize ,
            };
            waveUpdates = new Action[] {
                WaveLib.Wave0Update ,
                WaveLib.Wave1Update ,
                WaveLib.Wave2Update ,
                WaveLib.Wave3Update ,
                WaveLib.Wave4Update ,
                WaveLib.Wave5Update ,
                WaveLib.Wave6Update ,
                WaveLib.Wave7Update ,
                WaveLib.Wave8Update ,
                WaveLib.Wave9Update ,
            };
            itemWaveInitializes = new Action[] {
                WaveLib.ItemWave0Initialize ,
            };
            itemWaveUpdates = new Action[] {
                WaveLib.ItemWave0Update ,
            };

            HeartAttribute.MaxHP = 92;
            HeartAttribute.KR = true;

            EndSelecting();
            ChangeRound();
        }

        public void Update()
        {
            waveTime++;
            if (!RoundType)
                currentWaveUpdate?.Invoke();

            if (isPlayMusic)
            {
                if (musicInstance == null)
                {
                    if (mainMusic != null)
                        musicInstance = mainMusic.CreateInstance();
                }
                else musicInstance.Play();
            }
            else if (musicInstance != null && musicInstance.State == SoundState.Playing)
                musicInstance.Pause();
        }

        public void GameEnd()
        {
            if (musicInstance != null)
            {
                musicInstance.Dispose();
                musicInstance = null;
            }
            isPlayMusic = false;
            mainMusic.Dispose();
        }
    }
}