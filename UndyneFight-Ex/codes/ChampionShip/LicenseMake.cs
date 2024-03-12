using System;
using System.Collections.Generic;
using System.IO;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.ChampionShips
{
    public class LicenseMaker
    {
        private static float ReRate(float accuracy)
        {
            if (accuracy > 1) return 1;
            float del = 1 - accuracy;
            float lim = (MathF.Pow(del * 3, 0.7f) / 2.4f) + (del * 2.0f);
            return MathF.Max(0, 1 - lim);
        }
        public static void GetScore(ChampionShip championShip)
        {
            StreamWriter writer = new("Content\\result.txt");
            Dictionary<string, SaveInfo> players = GetPlayers();
            SortedList<Tuple<int, float>, string> values = new();

            SongSystem.IChampionShip[] songs;
            List<SongSystem.IChampionShip> tempSongs = new();
            foreach (Type type in championShip.Fights.Values)
            {
                var v = Activator.CreateInstance(type) as SongSystem.IChampionShip;
                if (v.GameContent.Attributes.Hidden == true) continue;
                tempSongs.Add(v);
            }
            songs = tempSongs.ToArray();

            Dictionary<string, float[]> maxScores = new();
            foreach (SaveInfo s in players.Values)
            {
                if (!s.Nexts["ChampionShips"].Nexts.ContainsKey(championShip.Title)) continue;
                string div = s.Nexts["ChampionShips"].Nexts[championShip.Title].StringValue;
                if (!maxScores.ContainsKey(div)) maxScores.Add(div, new float[songs.Length]);
                SaveInfo t = s.Nexts["NormalFights"];
                for (int i = 0; i < songs.Length; i++)
                {
                    if (!songs[i].DifficultyPanel.ContainsKey("div." + div)) continue;
                    float v = GetResult(t, songs[i].GameContent.FightName, (int)songs[i].DifficultyPanel["div." + div]);
                    maxScores[div][i] = MathF.Max(maxScores[div][i], v);
                }
            }
            foreach (var pair in maxScores)
            {
                string str = $"(div.{pair.Key})";
                for (int i = 0; i < songs.Length; i++) str += " " + pair.Value[i];
                values.Add(new Tuple<int, float>(Convert.ToInt32(pair.Key), -4000), "full mark: " + str);
            }
            foreach (SaveInfo s in players.Values)
            {
                if (!s.Nexts["ChampionShips"].Nexts.ContainsKey(championShip.Title)) continue;
                string res = s.Nexts["ChampionShips"].Nexts[championShip.Title].StringValue;
                string div = res;
                res += ("->" + s.Nexts["PlayerName"].StringValue + ":").PadRight(22);
                SaveInfo t = s.Nexts["NormalFights"];
                float sum = 0;
                float cur = 0;
                char curID = 'A';
                for (int i = 0; i < songs.Length; i++)
                {
                    if (!songs[i].DifficultyPanel.ContainsKey("div." + div)) continue;
                    cur = GetResult(t, songs[i].GameContent.FightName, (int)songs[i].DifficultyPanel["div." + div]);
                    cur /= maxScores[div][i];
                    float reCur = ReRate(cur);
                    res += $" {curID}:{reCur:F3}({cur:F3})".PadRight(21);
                    curID++;
                    sum += reCur;
                }
                res += " Total:" + sum;
                values.Add(new Tuple<int, float>(Convert.ToInt32(div[0]), -sum), res);
            }
            foreach (string s in values.Values)
            {
                writer.WriteLine(s);
            }
            writer.Flush();
            writer.Dispose();
        }

        private static int GetResult(SaveInfo s, string songName, int dif)
        {
            return s.Nexts.ContainsKey(songName) ? Convert.ToInt32(s.Nexts[songName].Nexts[((SongSystem.Difficulty)dif).ToString()]["score"]) : 0;
        }

        public static Dictionary<string, SaveInfo> GetPlayers()
        {
            Dictionary<string, SaveInfo> info = new();
            string res = "Datas\\Users";
            string[] files = Directory.GetFiles(res);
            foreach (string v in files)
            {
                string resName = IOProcess.Divider(v, '.')[0];

                SaveInfo s = FileIO.ReadFile(resName);

                if (s == null) continue;
                try
                {
                    info.Add(s.Nexts["PlayerName"].StringValue, s);
                }
                catch
                {
                    continue;
                }
            }
            return info;
        }

        public static void MakeLicence()
        {
            string res = "Licences";
            string[] files = Directory.GetFiles(res + "\\Input");
            string newpos;
            Directory.CreateDirectory(newpos = res + "\\Result");
            foreach (string v in files)
            {
                string resName = IOProcess.Divider(IOProcess.Divider(v, '\\')[^1], '.')[0];

                List<string> texts = new();
                StreamReader sr = new(v);

                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    texts.Add(str + "\n");
                }

                sr.Close();

                try
                {
                    string div = IOProcess.Divider(texts[0], '=')[1];
                    div = div.Trim();
                    IOEvent.WriteCustomFile(newpos + "\\" + resName + ".Tmpf", IOEvent.StringToByte("div:" + div));
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}