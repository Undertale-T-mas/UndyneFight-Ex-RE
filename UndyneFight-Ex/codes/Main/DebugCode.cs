using Microsoft.Xna.Framework;

namespace UndyneFight_Ex
{
    internal static class DebugState
    {
        public static bool redShieldAuto = false;
        public static bool blueShieldAuto = false;
        public static bool greenShieldAuto = false;
        public static bool purpleShieldAuto = false;
        public static bool otherAuto = false;
    }
    internal partial class GameMain : Game
    {
        private void InstanceCode()
        {
            /* System.IO.FileStream stream = new System.IO.FileStream("Taster.txt", System.IO.FileMode.Open);

             List<byte> bytes = new List<byte>();
             while (stream.Position != stream.Length)
             {
                 bytes.Add((byte)stream.ReadByte()); 
             } byte[] res = bytes.ToArray();

             IO.IOEvent.WriteTmpFile("Taster.Tmpf", bytes);*/
            //var s = TEngine.Network.ImformationLibrary.GetIP();
            //byte[] bytes = { 1, 2, 3, 4, 5, 6, 7 };
            //byte[] res = IO.IOEvent.Decoder(IO.IOEvent.Encoder(new List<byte>(bytes))).ToArray();
            //    var v= MathUtil.StringHash("Evelyne");
            //;
            // ChampionShips.LicenseMaker.MakeLicence(); 

            //  List<string> stringsOld = IO.IOEvent.ByteToString(IO.IOEvent.ReadTmpfFile("Datas\\tk"));
            //   stringsOld[240] = "Hard:score=0,AC=true,AP=true,Accuracy=0,mark=Failed";
            // List<string> stringsNew = IO.IOEvent.ByteToString(IO.IOEvent.ReadTmpfFile("Datas\\DJwwwNew"));
            //
            //strings.RemoveRange(100, 10);
            //   IO.IOEvent.WriteTmpFile("Datas\\tk", IO.IOEvent.StringToByte(stringsOld));
            ;
        }
    }
}