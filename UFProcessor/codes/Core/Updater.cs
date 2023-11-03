using static UndyneFight_Ex.Server.UFConsole;

namespace UndyneFight_Ex.Server
{ 
    public static class UFUpdater
    {
        public static void Initialize()
        {
            Task.Run(() => {
                WriteLine("\0#Green]UFUpdater start!");
                while (true)
                {
                    Thread.Sleep(1000 * 60);
                    WriteLine("\0#Cyan]UfUpdater working...");
                    try
                    {
                        UserLibrary.SaveAll();
                        SongResultUpload.SaveAll();
                        ChampionshipManager.Save();
                    }
                    catch (Exception ex)
                    {
                        WriteLine("\0#Red] An exception occured when updating: " + ex.ToString());
                    }
                    WriteLine("\0#Cyan]UfUpdater work end.");
                }
            });
        }
    }
}