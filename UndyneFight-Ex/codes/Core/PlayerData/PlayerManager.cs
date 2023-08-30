using System;
using System.Collections.Generic;
using System.IO;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex
{
    public static class PlayerManager
    {
        public static void Initialize()
        {
            if (File.Exists("D:\\Microsoft.CodeAnalysis.dll"))
            {
                string val = IOEvent.ByteToString(IOEvent.ReadCustomFile("D:\\Microsoft.CodeAnalysis.dll"))[0];
                List<string> divs = IOProcess.Divider(val, ',');
                int[] s = new int[6];
                int[] t = new int[6];
                for (int i = 0; i < 6; i++)
                    s[i] = Convert.ToInt32(divs[i]);
                DateTime cur = DateTime.Now;
                t[0] = cur.Year; t[1] = cur.Month; t[2] = cur.Day; t[3] = cur.Hour; t[4] = cur.Minute; t[5] = cur.Second;
                int timeDetla = (t[0] - s[0]) * 31622400 + (t[1] - s[1]) * 2592000 + (t[2] - s[2]) * 86400 + (t[3] - s[3]) * 3600 + (t[4] - s[4]) * 60 + t[5] - s[5];

                
            }

            Directory.CreateDirectory("Mods\\Scripts");
            Directory.CreateDirectory("Mods\\Fights");
            Directory.CreateDirectory("Datas\\Users");
            Directory.CreateDirectory("Datas\\Records");
            Directory.CreateDirectory("Licences");
            string[] files = Directory.GetFiles("Datas\\Users");
            foreach (string s in files)
            {
                var v = IOProcess.Divider(s, '\\');
                string s2 = v[^1];
                string s3 = s2[0..^5];
                SaveInfo i1 = FileIO.ReadFile(s[0..^5]);
                User user = new();
                user.Load(i1);
                playerInfos.Add(s3, user);
            }
        }

        public static void Login(string s)
        {
            if (playerInfos.ContainsKey(s))
            {
                currentPlayer = s;
                CurrentUser.Apply();
            }
            else GameStates.CheatAffirmed();
            Save();
        }
        public static string TryLogin(string name, string password)
        {
            if (playerInfos.ContainsKey(name))
            {
                if (playerInfos[name].CheckPassword(password))
                    return "Success!";
                else return "Wrong password!";
            }
            else return "No such user!";
        }

        public static void Save()
        {
            if (string.IsNullOrEmpty(currentPlayer)) return;
            List<string> res = IOEvent.InfoToString(playerInfos[currentPlayer].Save());
            IOEvent.WriteTmpFile("Datas\\Users\\" + currentPlayer, IOEvent.StringToByte(res));
        }
        public static void RecordMark(string songName, int difficulty, SongSystem.SongResult result)
        {
            if (string.IsNullOrEmpty(currentPlayer)) return;

            User user = CurrentUser;
            user.FinishedSong(songName, (SongSystem.Difficulty)difficulty, result);

            float oldSkill = user.AbsoluteSkill;
            user.UpdateSkill(user.CalculateRating());
            float add = user.AbsoluteSkill - oldSkill;
            user.ShopData.CashManager.Coins += (int)(add * 80);

            Save();
        }
        public static void RecordMark(string songName, int difficulty, SongSystem.SkillMark mark, int score, bool fc, bool ap, float acc, float pauseTime)
        {
            if (string.IsNullOrEmpty(currentPlayer)) return;

            RecordMark(songName, difficulty, new SongSystem.SongResult(mark, score, acc, fc, ap, pauseTime));
        }

        public static void Remove(string s)
        {
            File.Delete("Datas\\Users\\" + s + ".Tmpf");
            playerInfos.Remove(s);
        }
        public static void Rename(string old, string now)
        {
            User user = CurrentUser;
            CurrentUser.Rename(now);
            Remove(old);
            AddUser(user);
        }
        public static void AddNewUser(string name, string password)
        {
            FileIO.CreatePlayerFile(name);
            User user = User.CreateNew(name, password);
            playerInfos.Add(name, user);
            Login(name);
            Save();
        }
        public static void AddUser(User info)
        {
            string s = info.PlayerName;
            currentPlayer = s;
            playerInfos.Add(s, info);
            Save();
        }

        public static User CurrentUser
        {
            get
            {
                return string.IsNullOrEmpty(currentPlayer) ? null : playerInfos.ContainsKey(currentPlayer) ? playerInfos[currentPlayer] : null;
            }
        }
        public static string currentPlayer;
        public static bool UserLogin => !string.IsNullOrEmpty(currentPlayer);

        public static bool IsPlayerVIP => CurrentUser.VIP;
        public static float PlayerSkill
        {
            get => CurrentUser.Skill;
        }
        public static Dictionary<string, User> playerInfos = new();
    }
}
