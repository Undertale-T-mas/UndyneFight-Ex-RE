using UndyneFight_Ex.Remake.Data;
using UndyneFight_Ex.IO;
using System.Diagnostics;

namespace UndyneFight_Ex.Remake
{
    public static class FileData
    {
        public static void Initialize()
        {
            GlobalData = new();
            GlobalData.Load(FileIO.ReadFile("Datas\\Global\\RE_ENGINE_GLOBAL"));
        }
        public static void SaveGlobal()
        {
            IOEvent.WriteTmpFile("Datas\\Global\\RE_ENGINE_GLOBAL", IOEvent.InfoToByte(GlobalData.Save()));
        }
        public static GlobalDataRoot GlobalData { get; private set; }
    }
}