using System;

namespace UndyneFight_Ex.GameInterface
{
    public static class UFEXSettings
    {
        public static bool RecordEnabled { get; set; }
        public static string GamejoltPrivateKey { get; set; }
        public static int GamejoltID { get; set; }

        public static event Action Update;

        internal static void DoUpdate()
        {
            Update?.Invoke();
        }
    }
}
