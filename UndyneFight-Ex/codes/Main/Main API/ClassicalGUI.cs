using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.GameInterface
{
    public static class ClassicalGUI
    {
        public static void CreateFightSelector()
        {
            GameStates.InstanceCreate(new Entities.FightSelector(GameMode.None));
        }
        public static class MainMenuSettings
        {
            public static bool RecordEnabled { set; internal get; } = true;
            public static bool AchievementsEnabled { set; internal get; } = true;
        }
    }
}
