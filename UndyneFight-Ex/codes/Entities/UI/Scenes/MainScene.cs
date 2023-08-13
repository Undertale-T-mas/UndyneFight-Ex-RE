using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Entities
{
    public class GameMenuScene : Scene
    {
        private bool initialized = false;

        public GameMenuScene() : base() { }

        internal GameMenuScene(Selector selector) : base(selector)
        {
            initialized = true;
        }

        public override void Update()
        {
            if (!initialized)
            {
                initialized = true;
                GameStartUp.MainSceneIntro.Invoke();
            }

            base.Update();
        }
    }

    internal class TryAgainScene : Scene
    {
        private int appearTime = 0;
        private readonly GameObject obj;
        public TryAgainScene(StateShower shower) : this()
        { obj = new StateShower.FailureShower(shower); }
        public TryAgainScene(Fight.IClassicFight fight, GameMode mode) : this()
        { obj = new Fight.FailureShower(fight, mode); }
        public TryAgainScene(GameObject obj) : this()
        { this.obj = obj; }
        TryAgainScene()
        {
            PlayerManager.CurrentUser?.PlayerStatistic.AddDeath();
        }
        public override void Update()
        {
            appearTime++;
            if (appearTime == 100) InstanceCreate(obj);
            base.Update();
        }
    }

    internal class WinScene : Scene
    {
        public WinScene(StateShower ss, Player.Analyzer analyzer) : base(UFEXSettings.SongCompleteCreate(ss, analyzer)) { }
    }
}