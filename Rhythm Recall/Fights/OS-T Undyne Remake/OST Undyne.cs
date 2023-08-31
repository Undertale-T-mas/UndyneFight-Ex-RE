using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using UndyneFight_Ex.Entities.Advanced;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.ClassicFight;
using static UndyneFight_Ex.Fight.Functions;
using static UndyneFight_Ex.FightResources;

namespace Rhythm_Recall.Waves
{
    public partial class OSTUndyne : IClassicFight, IExtraOption
    {
        protected static bool IsFoolMode = false;

        internal static SoundEffect MainMusic
        {
            set => musicInstance = value.CreateInstance();
        }
        internal static SoundEffectInstance musicInstance;

        public static OSTUndyne instance;

        private FightAlphaManager alphaManager;
        internal Undyne undyne;

        private Action[] waveInitializes;
        private Action[] waveUpdates;
        private Action currentWaveUpdate;

        public virtual string FightName => "OS-T Undyne REMAKE";

        public string OptionName => "ost undyne";

        public Type IntroScene => throw new NotImplementedException();

        internal int wave = 0;
        private static int waveTime = 0;
        private static int totalTime = 0;
        private bool isTeased = false;
        protected int slowDownTime = 2;

        public void RoundChanged()
        {
            waveTime = 0;
            if (undyne.HP > 1600 && wave >= 18) wave = Rand(14, 17);
            waveInitializes[wave]();
            currentWaveUpdate = waveUpdates[wave];
            wave++;
            if (wave == 19)
                alphaManager.SetMission(1);
            else alphaManager.SetMission(Heart.SoulType == 1 ? 0.45f : 0.7f);
        }

        public virtual void Start()
        {
            slowDownTime = 3;
            instance = this;
            alphaManager = new FightAlphaManager();
            AddInstance(alphaManager);
            Rain.LoadResources();
            WaveLib.MagicForm.LoadResources();
            ScreenDrawing.BoundColor = Color.White;
#if !DEBUG
            try
            {
#endif
            MainMusic = Loader.Load<SoundEffect>("Fights\\OS-T Remake\\undyne theme");
#if !DEBUG
            }
            catch
            { 

            }
#endif
            wave = 0;

            totalTime = 0;
#if DEBUG
            //  this.wave = 9;
#endif

            StartBattle();
            //GameStates.;
            CreateMenu();

            PlayerInformation.AttackDamage = 1600;

            HeartAttribute.DamageTaken = 12;
            HeartAttribute.JumpTimeLimit = 1;
            HeartAttribute.Gravity = 7.0f;
            HeartAttribute.JumpSpeed = 5;
            HeartAttribute.KRDamage = 8;
            HeartAttribute.Speed = 2.8f;

            CreateAction(new GameAction("check", () =>
            {
                string message = "* Undyne - atk 800 def 666 \n* The true heroine.";
                CreateEntity(new BoxMessage(message) { AfterDispose = () => ChangeRound() });
            }
            ));
            CreateAction(new GameAction("tease", () =>
            {
                if (isTeased)
                {
                    CreateEntity(new BoxMessage(new TextPrinter[] {
                    new ("* You teased undyne. \n* . . .") ,
                    new ("* Nothing happened.")
                })
                    {
                        AfterDispose = () => { ChangeRound(); }
                    });
                    return;
                }
                isTeased = true;
                CreateEntity(new BoxMessage(new TextPrinter[] {
                    new ("* You teased undyne. \n* She became really angry.") ,
                    new ("* Undyne's attack increased. \n* Undyne's defence dropped. ")
                })
                {
                    AfterDispose = () => { ChangeRound(); }
                });
                HeartAttribute.DamageTaken += 2;
                PlayerInformation.AttackDamage = 1800;
            }
            ));
            for (int i = 0; i < 4; i++)
                CreateItem(new Item("Snow", () =>
                {
                    Regenerate(50);
                    PlaySound(Sounds.heal);
                    string message = "* You ate the snow piece.\n";
                    if (HeartAttribute.IsFullHP)
                        message += "* Your HP maxed out.";
                    else message += "* You recovered 50 HP.";
                    CreateEntity(new BoxMessage(message) { AfterDispose = () => ChangeRound() });
                }
                ));
            CreateItem(new Item("Pie", () =>
            {
                Regenerate(100);
                PlaySound(Sounds.heal);
                string message = "* You ate the pie.\n";
                if (HeartAttribute.IsFullHP)
                    message += "* Your HP maxed out.";
                else message += "* You recovered 100 HP.";
                CreateEntity(new BoxMessage(message) { AfterDispose = () => ChangeRound() });
            }
            ));
            CreateEnemy(undyne = new Undyne());

            InterActive.AttackAnimation = typeof(Slice);
            InterActive.MainMessage = "The wind is howling.";
            InterActive.DamageMessageColor = Color.Red;

            Resources.Initialize();

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
                WaveLib.Wave10Initialize ,
                WaveLib.Wave11Initialize ,
                WaveLib.Wave12Initialize ,
                WaveLib.Wave13Initialize ,
                WaveLib.Wave14Initialize ,
                WaveLib.Wave15Initialize ,
                WaveLib.Wave16Initialize ,
                WaveLib.Wave17Initialize ,
                WaveLib.Wave18Initialize ,
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
                WaveLib.Wave10Update ,
                WaveLib.Wave11Update ,
                WaveLib.Wave12Update ,
                WaveLib.Wave13Update ,
                WaveLib.Wave14Update ,
                WaveLib.Wave15Update ,
                WaveLib.Wave16Update ,
                WaveLib.Wave17Update ,
                WaveLib.Wave18Update ,
            };

            PlayerLevel = 13;
            HeartAttribute.MaxHP = 68;
            HeartAttribute.KR = false;

            IsFoolMode = false;
        }

        public void Update()
        {
            CreateEntity(new Rain());
            if (Rand(0, 400 + (RoundType ? -125 : 0)) == 0)
                ScreenDrawing.MakeFlicker(new Color(255, 255, 255, (int)(128 * (RoundType ? 1.5f : 1))));
            totalTime++;
            if (totalTime >= 50)
            {
                if (musicInstance.State == SoundState.Stopped)
                {
                    musicInstance.Play();
                }
                waveTime++;
            }

            if (!RoundType && totalTime >= 50)
            {
                currentWaveUpdate?.Invoke();
            }

            if (UndyneFight_Ex.GameStates.IsKeyPressed(UndyneFight_Ex.InputIdentity.Alternate) && slowDownTime >= 0 && wave != 18)
            {
                slowDownTime--;
                UndyneFight_Ex.Entities.Advanced.ZaWarudo v;
                MediaPlayer.Volume *= 0.5f;
                AddInstance(v = new UndyneFight_Ex.Entities.Advanced.ZaWarudo(70, 0.2f, Color.LightBlue) { });
                v.OnDispose += () => { MediaPlayer.Volume *= 2f; };
            }
            if (RoundType && Heart.SoulType == 0 && Gametime % 24 == 0 && undyne.HP >= 3200)
            {
                Vector2 pos = new(Rand(140, 500), Rand(60, 120));
                CreateSpear(new UndyneFight_Ex.Entities.NormalSpear(pos) { Speed = 7f });
                pos = new Vector2(Rand(140, 500), Rand(60, 120));
                CreateSpear(new UndyneFight_Ex.Entities.NormalSpear(pos) { Speed = 11f, IsMute = true });
            }
        }

        public void GameEnd()
        {
            musicInstance.Dispose();
            MediaPlayer.Stop();
            totalTime = 0;
        }
    }
}