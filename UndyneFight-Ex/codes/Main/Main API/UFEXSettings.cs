using System;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.GameInterface
{
    public static class UFEXSettings
    {
        public static bool RecordEnabled { get; set; }
        public static string GamejoltPrivateKey { get; set; }
        public static int GamejoltID { get; set; }

        public static string MainServerURL { get; set; }
        public static int MainServerPort { get; set; }
        public static Action<SongPlayData> OnSongComplete;

        public static event Action Update;

        internal static void DoUpdate()
        {
            Update?.Invoke();
        }

        internal static Func<StateShower, Player.Analyzer, GameObject> SongCompleteCreate { get; set; } = (s, t) => new StateShower.ResultShower(s, t);
    }
}
