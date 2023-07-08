using System;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.GameStates;

namespace UndyneFight_Ex.Entities
{
    public abstract class FightScene : Scene
    {
        internal Player PlayerInstance { get; set; }
        internal HPShower HPBar { get; set; }
        internal NameShower NameShow { get; set; }

        private bool playerAlive = true;
        private CheatDetector Detector = new();

        public abstract GameMode Mode { get; }
        public override void Dispose()
        {
            ResetFightState(!playerAlive);
            base.Dispose();
        }
        public FightScene()
        {
            UpdateIn120 = true;
            InstanceCreate(NameShow = new NameShower());
            InstanceCreate(new CheatDetector());
        }

        public void PlayDeath()
        {
            playerAlive = false;
            PlayerDied();

            Achievements.AchievementManager.CheckUserAchievements();
            GameStates.InstanceCreate(new Player.BrokenHeart());
        }
        protected abstract void PlayerDied();

        public override void Start()
        {
        }

        public override void Update()
        {
            if (stopTime <= 0.01f)
            {
                GasterBlaster.shootSoundPlayed = GasterBlaster.spawnSoundPlayed =
                Pike.shootSoundPlayed = Pike.spawnSoundPlayed = false;

                foreach (Player.Heart heart in Player.hearts)
                {
                    if (heart.SoulType == 1) continue;
                    foreach (GameObject entity in Objects)
                    {
                        if (entity is ICollideAble collideAble) collideAble.GetCollide(heart);
                    }
                }
            }

            base.Update();
        }
    }
    internal class NormalFightingScene : FightScene
    {
        private int appearTime = 0, restartTimer = 0;
        private readonly Fight.IClassicFight current;
        private readonly GameMode mode;
        public override GameMode Mode => mode;

        public NormalFightingScene(Fight.IClassicFight obj, GameMode mode)
        {
            this.mode = mode;
            Type type = obj.GetType();
            current = (Fight.IClassicFight)Activator.CreateInstance(type);
        }
        protected override void PlayerDied()
        {
            ResetScene(new TryAgainScene(current, mode));
        }
        public override void Update()
        {
            if (appearTime == 0)
                Fight.ClassicFightEnterance.CreateClassicFight(current);
            appearTime++;
            base.Update();

            restartTimer = (IsKeyDown(InputIdentity.Reset)) ? restartTimer + 1 : 0;
            if (restartTimer >= 45)
            {
                ResetFightState(true);
                GameStates.InstanceCreate(new Player.BrokenHeart());
                ResetScene(new TryAgainScene(Fight.ClassicFightEnterance.currentFight, mode));
            }
        }
    }
}