using System.Drawing;

namespace UndyneFight_Ex.Server
{ 
    public static class UFUpdater
    {
        public static void Initialize()
        {
            Task.Run(() => {
                UFConsole.WriteLine("\0#Green]UFUpdater start!");
                while (true)
                {
                    Thread.Sleep(1000 * 60);
                    UFConsole.WriteLine("\0#Cyan]UfUpdater working...");
                    try
                    {
                        UserLibrary.SaveAll();
                        SongResultUpload.SaveAll();
                    }
                    catch (Exception ex)
                    {
                        UFConsole.WriteLine("\0#Red] An exception occured when updating: " + ex.ToString());
                    }
                    UFConsole.WriteLine("\0#Cyan]UfUpdater work end.");
                }
            });
        }
    }
}