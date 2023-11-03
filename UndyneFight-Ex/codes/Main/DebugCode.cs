using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using UFData;
using UndyneFight_Ex.Server;

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
        private string cur = "[{\"Name\":\"Summer Camp - A\",\"StartTime\":\"2023-10-01T01:00:00\",\"EndTime\":\"2023-10-01T04:00:00\",\"Divisions\":{\"Div.2\":{\"DivisionName\":\"Div.2\",\"Info\":{\"Death By Glamour\":{\"Item1\":0,\"Item2\":0},\"Asgore\":{\"Item1\":1,\"Item2\":0},\"The World Revolving\":{\"Item1\":2,\"Item2\":2}},\"Scoreboard\":{\"Members\":[{\"Division\":\"Div.2\",\"UUID\":19,\"Name\":\"Hary\",\"AccuracyList\":[0.99408346,0.9936848,0.96039605]},{\"Division\":\"Div.2\",\"UUID\":7,\"Name\":\"KURO\",\"AccuracyList\":[0.99450094,0.9907952,0.95256186]},{\"Division\":\"Div.2\",\"UUID\":71,\"Name\":\"KB\",\"AccuracyList\":[0.976824,0.99313486,0.9372401]},{\"Division\":\"Div.2\",\"UUID\":46,\"Name\":\"NaHCO3\",\"AccuracyList\":[0.9868421,0.9896498,0.92382425]},{\"Division\":\"Div.2\",\"UUID\":27,\"Name\":\"ypcbt\",\"AccuracyList\":[0.975118,0.99021685,0.8798886]},{\"Division\":\"Div.2\",\"UUID\":51,\"Name\":\"flag\",\"AccuracyList\":[0.9773684,0.96770155,0.8257921]},{\"Division\":\"Div.2\",\"UUID\":44,\"Name\":\"huaji\",\"AccuracyList\":[0.97117966,0.984017,0.7297896]},{\"Division\":\"Div.2\",\"UUID\":52,\"Name\":\"S\",\"AccuracyList\":[0.9637024,0.9824125,0]},{\"Division\":\"Div.2\",\"UUID\":69,\"Name\":\"cloudplayer\",\"AccuracyList\":[0,0,0.9248515]}]}},\"Div.1\":{\"DivisionName\":\"Div.1\",\"Info\":{\"Death By Glamour\":{\"Item1\":0,\"Item2\":2},\"Asgore\":{\"Item1\":1,\"Item2\":3},\"The World Revolving\":{\"Item1\":2,\"Item2\":4}},\"Scoreboard\":{\"Members\":[{\"Division\":\"Div.1\",\"UUID\":6,\"Name\":\"ta27\",\"AccuracyList\":[1,0.99238837,0.97890496]},{\"Division\":\"Div.1\",\"UUID\":39,\"Name\":\"bomei sama\",\"AccuracyList\":[0.99905896,0.9847594,0.96030086]},{\"Division\":\"Div.1\",\"UUID\":33,\"Name\":\"Sedwix\",\"AccuracyList\":[0.98490167,0.9770573,0.93598074]},{\"Division\":\"Div.1\",\"UUID\":54,\"Name\":\"qijiqiji\",\"AccuracyList\":[0.9608989,0.9823129,0.91311675]},{\"Division\":\"Div.1\",\"UUID\":70,\"Name\":\"a169a\",\"AccuracyList\":[0.9501966,0.9650337,0.9124549]},{\"Division\":\"Div.1\",\"UUID\":49,\"Name\":\"DJwww\",\"AccuracyList\":[0.9621489,0.9705112,0.90910953]},{\"Division\":\"Div.1\",\"UUID\":62,\"Name\":\"mentototo\",\"AccuracyList\":[0,0.98210114,0.9073077]},{\"Division\":\"Div.1\",\"UUID\":5,\"Name\":\"misaka sama\",\"AccuracyList\":[0.9257584,0,0.8342479]},{\"Division\":\"Div.1\",\"UUID\":37,\"Name\":\"T-mas222\",\"AccuracyList\":[0,0.9847224,0]},{\"Division\":\"Div.1\",\"UUID\":75,\"Name\":\"xamid_vlll\",\"AccuracyList\":[0.9561236,0,0]},{\"Division\":\"Div.1\",\"UUID\":47,\"Name\":\"parchim27\",\"AccuracyList\":[0.9361517,0,0]}]}},\"Div.3\":{\"DivisionName\":\"Div.3\",\"Info\":{\"The World Revolving\":{\"Item1\":2,\"Item2\":1}},\"Scoreboard\":{\"Members\":[]}}},\"Participants\":{\"40\":\"Div.2\",\"42\":\"Div.2\",\"19\":\"Div.2\",\"13\":\"Div.2\",\"27\":\"Div.2\",\"39\":\"Div.1\",\"47\":\"Div.1\",\"44\":\"Div.2\",\"49\":\"Div.1\",\"46\":\"Div.2\",\"51\":\"Div.2\",\"6\":\"Div.1\",\"52\":\"Div.2\",\"54\":\"Div.1\",\"5\":\"Div.1\",\"55\":\"Div.2\",\"7\":\"Div.2\",\"57\":\"Div.2\",\"33\":\"Div.1\",\"60\":\"Div.2\",\"69\":\"Div.2\",\"70\":\"Div.1\",\"37\":\"Div.1\",\"71\":\"Div.2\",\"72\":\"Div.1\",\"73\":\"Div.2\",\"75\":\"Div.1\",\"11\":\"Div.1\",\"78\":\"Div.1\"}},{\"Name\":\"Summer Camp - B\",\"StartTime\":\"2023-10-01T05:00:00\",\"EndTime\":\"2023-10-01T09:00:00\",\"Divisions\":{\"Div.2\":{\"DivisionName\":\"Div.2\",\"Info\":{\"BIG SHOT\":{\"Item1\":0,\"Item2\":1},\"Spider Dance\":{\"Item1\":1,\"Item2\":1},\"Traveler at Sunset\":{\"Item1\":2,\"Item2\":2}},\"Scoreboard\":{\"Members\":[{\"Division\":\"Div.2\",\"UUID\":19,\"Name\":\"Hary\",\"AccuracyList\":[0.9875725,0.989966,0]},{\"Division\":\"Div.2\",\"UUID\":66,\"Name\":\"NIHEMe\",\"AccuracyList\":[0.9724638,0.96400225,0]},{\"Division\":\"Div.2\",\"UUID\":7,\"Name\":\"KURO\",\"AccuracyList\":[0.97631645,0.9770068,0.7918958]},{\"Division\":\"Div.2\",\"UUID\":59,\"Name\":\"m\",\"AccuracyList\":[0.882971,0.9143537,0]},{\"Division\":\"Div.1\",\"UUID\":59,\"Name\":\"DJwww\",\"AccuracyList\":[0.946, 0.924,0]},{\"Division\":\"Div.2\",\"UUID\":46,\"Name\":\"NaHCO3\",\"AccuracyList\":[0.99163043,0.99499434,0]},{\"Division\":\"Div.2\",\"UUID\":27,\"Name\":\"ypcbt\",\"AccuracyList\":[0.97161835,0.9695578,0]},{\"Division\":\"Div.2\",\"UUID\":51,\"Name\":\"flag\",\"AccuracyList\":[0.9405314,0,0]},{\"Division\":\"Div.2\",\"UUID\":81,\"Name\":\"wingggggg\",\"AccuracyList\":[0.8884058,0,0]},{\"Division\":\"Div.2\",\"UUID\":47,\"Name\":\"parchim27\",\"AccuracyList\":[0.992657,0.9856009,0]},{\"Division\":\"Div.2\",\"UUID\":38,\"Name\":\"jch\",\"AccuracyList\":[0,0,0.9417573]},{\"Division\":\"Div.2\",\"UUID\":57,\"Name\":\"Tanatana\",\"AccuracyList\":[0.9875604,0.984563,0]},{\"Division\":\"Div.2\",\"UUID\":13,\"Name\":\"169\",\"AccuracyList\":[0.95716184,0.9612925,0]},{\"Division\":\"Div.2\",\"UUID\":89,\"Name\":\"1mt\",\"AccuracyList\":[0.9822222,0.95520407,0]},{\"Division\":\"Div.2\",\"UUID\":94,\"Name\":\"MoRoZ_549\",\"AccuracyList\":[0.96582127,0.9515873,0]}]}},\"Div.1\":{\"DivisionName\":\"Div.1\",\"Info\":{\"BIG SHOT\":{\"Item1\":0,\"Item2\":4},\"Spider Dance\":{\"Item1\":1,\"Item2\":4},\"Traveler at Sunset\":{\"Item1\":2,\"Item2\":5}},\"Scoreboard\":{\"Members\":[{\"Division\":\"Div.1\",\"UUID\":24,\"Name\":\"1\",\"AccuracyList\":[0,0,0]},{\"Division\":\"Div.1\",\"UUID\":40,\"Name\":\"Tlottgodinf\",\"AccuracyList\":[0.9127716,0.923905,0]},{\"Division\":\"Div.1\",\"UUID\":80,\"Name\":\"mentosu\",\"AccuracyList\":[0,0.9787899,0]},{\"Division\":\"Div.1\",\"UUID\":75,\"Name\":\"xamid_vlll\",\"AccuracyList\":[0.97109693,0.9664085,0]},{\"Division\":\"Div.1\",\"UUID\":6,\"Name\":\"ta27\",\"AccuracyList\":[0.96850425,0.9682381,0]},{\"Division\":\"Div.1\",\"UUID\":71,\"Name\":\"KB\",\"AccuracyList\":[0.77951366,0.73465115,0]},{\"Division\":\"Div.1\",\"UUID\":54,\"Name\":\"qijiqiji\",\"AccuracyList\":[0.910017,0.89890397,0]},{\"Division\":\"Div.1\",\"UUID\":33,\"Name\":\"Sedwix\",\"AccuracyList\":[0.9238856,0.91297483,0]},{\"Division\":\"Div.1\",\"UUID\":39,\"Name\":\"bomei sama\",\"AccuracyList\":[0.94567406,0.9294966,0]},{\"Division\":\"Div.1\",\"UUID\":90,\"Name\":\"mentotototototo\",\"AccuracyList\":[0.94049406,0.9811907,0]},{\"Division\":\"Div.1\",\"UUID\":96,\"Name\":\"tk\",\"AccuracyList\":[0.9093419,0.953727,0]}]}},\"Anomoly\":{\"DivisionName\":\"Anomoly\",\"Info\":{\"Traveler at Sunset\":{\"Item1\":2,\"Item2\":0}},\"Scoreboard\":{\"Members\":[]}}},\"Participants\":{\"27\":\"Div.2\",\"54\":\"Div.1\",\"6\":\"Div.1\",\"33\":\"Div.1\",\"46\":\"Div.2\",\"19\":\"Div.2\",\"66\":\"Div.2\",\"7\":\"Div.2\",\"70\":\"Div.1\",\"49\":\"Div.1\",\"71\":\"Div.1\",\"75\":\"Div.1\",\"11\":\"Div.1\",\"51\":\"Div.2\",\"59\":\"Div.2\",\"40\":\"Div.1\",\"80\":\"Div.1\",\"81\":\"Div.2\",\"47\":\"Div.2\",\"5\":\"Div.1\",\"85\":\"Div.2\",\"39\":\"Div.1\",\"57\":\"Div.2\",\"13\":\"Div.2\",\"88\":\"Div.2\",\"89\":\"Div.2\",\"68\":\"Div.2\",\"90\":\"Div.1\",\"92\":\"Div.1\",\"93\":\"Div.1\",\"94\":\"Div.2\",\"95\":\"Div.1\"}}]";
        private void InstanceCode()
        {
            /*
            List<ChampionshipInfo> infos = JsonSerializer.Deserialize<List<ChampionshipInfo>>(cur);
            ;
            Dictionary<string, Dictionary<string, ChampionshipParticipant>> existDivs = new();
            foreach (var i in infos)
                foreach (var j in i.Participants)
                    if (!existDivs.ContainsKey(j.Value))
                        existDivs.Add(j.Value, new());

            foreach(var i in infos)
                foreach(var j in i.Divisions.Values)
                {
                    if (!existDivs.ContainsKey(j.DivisionName)) continue;
                    ChampionshipScoreboard scoreboard = j.Scoreboard;
                    float[] accMax = new float[scoreboard.Members.First().AccuracyList.Length];
                    foreach(var obj in scoreboard.Members)
                    {
                        for(int k = 0; k < accMax.Length; k++)
                            accMax[k] = MathF.Max(accMax[k], obj.AccuracyList[k]);
                    }
                    foreach (var obj in scoreboard.Members)
                    {
                        ChampionshipParticipant p = new(obj.UUID, obj.Name, j);
                        p.AccuracyList = obj.AccuracyList;
                        for (int k = 0; k < accMax.Length; k++)
                        {
                            if (accMax[k] < 0.0001f) p.AccuracyList[k] = 0.998f;
                            else p.AccuracyList[k] = p.AccuracyList[k] / accMax[k];
                        }
                        if (!existDivs[j.DivisionName].ContainsKey(p.Name))
                            existDivs[j.DivisionName].Add(p.Name, p);
                        float v = existDivs[j.DivisionName][p.Name].Total;
                        if (p.Total > v)
                        {
                            existDivs[j.DivisionName][p.Name].AccuracyList = p.AccuracyList;
                            existDivs[j.DivisionName][p.Name].Update();
                        }
                    }
                }
            
            foreach(var dic in existDivs)
            {
                FileStream stream = new("Datas\\" + dic.Key + ".txt", FileMode.OpenOrCreate);
                StreamWriter textWriter = new StreamWriter(stream);
                
                foreach (var v in dic.Value) { 
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(v.Key.ToString() + " : ");
                    foreach (var c in v.Value.AccuracyList)
                    {
                        stringBuilder.Append(c + " ");
                    }
                    stringBuilder.Append(", Total = " + v.Value.Total);
                    textWriter.WriteLine(stringBuilder);
                }
                textWriter.Flush();
                textWriter.Dispose();
                stream.Dispose();
            }
            ;*/
            /* System.IO.FileStream stream = new System.IO.FileStream("Taster.txt", System.IO.FileMode.Open);

             List<byte> bytes = new List<byte>();
             while (stream.Position != stream.Length)
             {
                 bytes.Add((byte)stream.ReadByte()); 
             } byte[] res = bytes.ToArray();

             IO.IOEvent.WriteTmpFile("Taster.Tmpf", bytes);*/
            //var s = TEngine.Network.InformationLibrary.GetIP();
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