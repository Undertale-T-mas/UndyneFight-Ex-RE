using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex.IO
{
    internal static class IOProcess
    {
        public static List<string> Divider(string str, char DivideChar)
        {
            List<string> returns = new();
            string ls = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != DivideChar) ls += str[i];
                else
                {
                    returns.Add(ls);
                    ls = "";
                }
            }
            returns.Add(ls);
            return returns;
        }
    }

    /// <summary>
    /// 下面是SaveInfo的存储分数规则:
    /// <para>[]为定量，{}为Values数组, ->{}为Nexts数组 ',' 为分割左右两组数据</para>
    /// <para>(ChampionShips)->{div=[string],score=[int],position=[int]}</para>
    /// <para>PlayerName:[string],VIP:[bool]</para>
    /// <para>NormalFight->{(songName):noob=[int],easy=[int],...extreme=[int]}</para>
    /// <para>Achievements->{(achievementName):type=[bool],progress=[int]}</para>
    /// <para>其他待添加</para>
    /// </summary>
    public class SaveInfo
    {
        public string this[string index]
        {
            get { return values[keysForIndexs[index]];/* return the specified index here */ }
            set { values[keysForIndexs[index]] = value; }
        }
        public string this[int index]
        {
            get { return values[index];/* return the specified index here */ }
            set { values[index] = value; /* set the specified index to value here */ }
        }
        public bool HasDeepInfo;
        public readonly string Title;
        public string fullValue;
        public List<string> values;
        public Dictionary<string, int> keysForIndexs;
        public Dictionary<int, string> indexForKeys;
        public Dictionary<string, SaveInfo> Nexts;

        public void SetNext(string mission, string info)
        {
            if (Nexts.ContainsKey(mission)) Nexts[mission] = new SaveInfo(info);
            else Nexts.Add(mission, new SaveInfo(info));
        }

        private bool CheckBool(int v)
        {
            return values[v] == "true" || values[v] == "True";
        }
        public float FloatValue
        {
            get
            {
                return MathUtil.FloatFromString(values[0]);
            }
        }
        public int IntValue
        {
            get
            {
                return Convert.ToInt32(values[0]);
            }
        }
        public Vector2 VectorValue
        {
            get
            {
                return new Vector2(MathUtil.FloatFromString(values[0]), MathUtil.FloatFromString(values[1]));
            }
        }
        public bool BoolValue
        {
            get
            {
                return CheckBool(0);
            }
        }
        public string StringValue
        {
            get
            {
                return values[0];
            }
        }

        public SaveInfo(string val)
        {
            List<string> u1 = IOProcess.Divider(val, ':');
            Title = u1[0];
            if (u1.Count > 1)
            {
                fullValue = u1[1];
                values = new List<string>();
                keysForIndexs = new Dictionary<string, int>();
                indexForKeys = new Dictionary<int, string>();
                List<string> units = IOProcess.Divider(u1[1], ',');
                List<string> parts;
                units.ForEach(s =>
                {
                    parts = IOProcess.Divider(s, '=');
                    if (parts.Count == 1) values.Add(parts[0]);
                    else
                    {
                        values.Add(parts[1]);
                        keysForIndexs.Add(parts[0], values.Count - 1);
                        indexForKeys.Add(values.Count - 1, parts[0]);
                    }
                });
            }
            else if (u1.Count == 1 && u1[0][^1] == '{')
            {
                Title = Title[0..^1];
                HasDeepInfo = true;
                Nexts = new Dictionary<string, SaveInfo>();
            }
            else
            {

            }
        }
        public void PushNext(SaveInfo info)
        {
            Nexts ??= new Dictionary<string, SaveInfo>();
            HasDeepInfo = true;
            Nexts.Add(info.Title, info);
        }
        public SaveInfo GetDirectory(string path)
        {
            SaveInfo current = this;
            string[] all = path.Split("\\");
            for (int i = 0; i < all.Length; i++)
            {
                string s = all[i];
                current = current.Nexts[s];
            }
            return current;
        }
        public bool TryDirectory(string path)
        {
            SaveInfo current = this;
            string[] all = path.Split("\\");
            for (int i = 0; i < all.Length; i++)
            {
                string s = all[i];
                if (!current.Nexts.ContainsKey(s))
                    return false;
                current = current.Nexts[s];
            }
            return true;
        }
    }

    public static class IOEvent
    {
        private static List<byte> Decoder(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)((256 - (bytes[i] + i)) % 256);
            }
            return new List<byte>(bytes);
        }
        private static byte[] Encoder(List<byte> bytes)
        {
            byte[] b = bytes.ToArray();
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = (byte)((256 * 8192 - (bytes[i] + i)) % 256);
            }
            return b;
        }

        /// <summary>
        /// 生成自定义文件（需要写后缀名）
        /// </summary>
        /// <param name="Location">文件路径</param>
        /// <param name="bytes">文件数据</param>
        public static void WriteCustomFile(string Location, List<byte> bytes)
        {
            if (bytes.Count % 2 != 1) bytes.Add(0);

            System.IO.FileStream stream = new(Location, System.IO.FileMode.OpenOrCreate);

            stream.Write(Encoder(bytes), 0, bytes.Count);

            stream.Flush();
            stream.Close();

        }
        public static void WriteTmpFile(string Location, List<byte> bytes)
        {
            if (bytes.Count % 2 != 1) bytes.Add(0);

            System.IO.FileStream stream = new(Location + ".Tmpf", System.IO.FileMode.OpenOrCreate);
            stream.SetLength(bytes.Count);
            stream.Write(Encoder(bytes), 0, bytes.Count);

            stream.Flush();
            stream.Close();

        }

        /// <summary>
        /// 生成一个全新的tmpf文件
        /// </summary>
        /// <param name="Location">文件地址</param>
        public static void CreateTmpfFile(string Location)
        {
            List<string> formals = new() { "PlayerName:255", "VIP:false", "NormalFight{", "}" };

            WriteTmpFile(Location, StringToByte(formals));
        }

        /// <summary>
        /// 读取自定义图片(不带后缀名)上的像素块的颜色值并得到一串字符列表
        /// </summary>
        /// <returns>通过记忆图片得到的字符列表</returns>
        public static List<byte> ReadCustomFile(string Path)
        {
            System.IO.FileStream stream = new(Path, System.IO.FileMode.OpenOrCreate);

            byte[] res = new byte[stream.Length];
            stream.Read(res, 0, res.Length);

            stream.Dispose();

            return Decoder(res);
        }

        /// <summary>
        /// 读取Tmp图片上的像素块的颜色值并得到一串字符列表
        /// </summary>
        /// <returns>通过记忆图片得到的字符列表</returns>
        public static List<byte> ReadTmpfFile(string Path)
        {
            System.IO.FileStream stream = new(Path + ".Tmpf", System.IO.FileMode.OpenOrCreate);

            byte[] res = new byte[stream.Length];
            stream.Read(res);
            stream.Dispose();

            return Decoder(res);
        }
        public static List<byte> StringToByte(string strings)
        {
            return StringToByte(new List<string>() { strings });
        }
        public static List<byte> StringToByte(List<string> strings)
        {
            List<byte> bytes = new();
            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    bytes.Add((byte)(strings[i][j]));
                }
                bytes.Add(1);
            }
            return bytes;
        }
        public static List<string> ByteToString(List<byte> bytes)
        {
            List<string> strs = new();
            string temp = "";
            for (int i = 0; i < bytes.Count; i++)
            {
                char ch = (char)bytes[i];
                if (bytes[i] != 1) { if (ch != 0) temp += ch; }
                else { strs.Add(temp); temp = ""; }
            }
            return strs;
        }
        public static SaveInfo ToInfos(List<string> strs)
        {
            SaveInfo Last;
            Stack<SaveInfo> buffer = new();
            if (strs[0] == "StartInfo->" || strs[0] == "StartInfo->{")
            {
                strs.RemoveAt(0);
                strs.RemoveAt(strs.Count - 1);
            }
            buffer.Push(new SaveInfo("StartInfo->"));
            Last = buffer.Peek();
            for (int i = 0; i < strs.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(strs[i])) continue;
                if (strs[i] == "{") { buffer.Push(Last); continue; }
                if (strs[i] == "End}" || strs[i] == "}")
                {
                    if (buffer.Count > 1) buffer.Pop(); continue;
                }
                SaveInfo info = new(strs[i]);
                Last = info;
                buffer.Peek().PushNext(info);
                if (info.HasDeepInfo)
                {
                    buffer.Push(info);
                }
            }
            return buffer.Peek();
        }

        public static List<string> InfoToString(SaveInfo info)
        {
            List<string> res = new();
            if (info.values != null)
            {
                string s = info.Title + ":";
                for (int i = 0; i < info.values.Count; i++)
                {
                    if (info.keysForIndexs.ContainsValue(i))
                    {
                        s += info.indexForKeys[i] + "=";
                    }
                    s += info.values[i];
                    if (i + 1 != info.values.Count)
                        s += ",";
                }
                res.Add(s);
            }
            else
            {
                string s = info.Title + "{";
                res.Add(s);
                foreach (SaveInfo next in info.Nexts.Values)
                {
                    res.AddRange(InfoToString(next));
                }
                res.Add("}");
            }
            return res;
        }
    }

    public static class FileIO
    {
        private static List<byte> Decoder(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)((256 - (bytes[i] + i)) % 256);
            }
            return new List<byte>(bytes);
        }
        /// <summary>
        /// 读取Tmp图片上的像素块的颜色值并得到一串字符列表
        /// </summary>
        /// <returns>通过记忆图片得到的字符列表</returns>
        public static SaveInfo ReadFile(string Path)
        {
            System.IO.FileStream stream = new(Path + ".Tmpf", System.IO.FileMode.OpenOrCreate);

            byte[] res1 = new byte[stream.Length];
            stream.Read(res1, 0, res1.Length);

            List<byte> returns = Decoder(res1);
            List<string> res = IOEvent.ByteToString(returns);

            if (res == null || res.Count == 0) return null;

            SaveInfo res2 = IOEvent.ToInfos(res);

            stream.Flush();
            stream.Close();

            return res2;
        }

        /// <summary>
        /// 生成一个全新的tmpf文件
        /// </summary>
        /// <param name="playerName">文件对应的玩家名称</param>
        internal static void CreatePlayerFile(string playerName)
        {
            List<string> formals = new() { "PlayerName:" + playerName, "VIP:" + "false", "NormalFight{", "}" };

            IOEvent.WriteTmpFile("Datas\\Users\\" + playerName, IOEvent.StringToByte(formals));
        }
        /// <summary>
        /// 生成一个全新的tmpf文件
        /// </summary>
        /// <param name="playerName">文件对应的玩家名称</param>
        public static void CreatePlayerFile(SaveInfo info)
        {
            IOEvent.WriteTmpFile("Datas\\Users\\" + info.Nexts["PlayerName"].StringValue, IOEvent.StringToByte(IOEvent.InfoToString(info)));
        }
    }
}