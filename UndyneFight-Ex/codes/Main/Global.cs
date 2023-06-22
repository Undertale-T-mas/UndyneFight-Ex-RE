namespace UndyneFight_Ex
{
    //全域变量
    public class Global
    {
        public float[] floatDatas = { };
        public string[] stringDatas = { };
        public string[] keys = { };
        public void Add(string key, float value)
        {
            keys[^1] = key;
            floatDatas[^1] = value;
        }
        public float GetValue(string key)
        {
            var i = 0;
            while (i < keys.Length)
            {
                if (keys[i] == key)
                {
                    return floatDatas[i];
                }
                ++i;
            }
            return -1;
        }
        public void Add(string key, string value)
        {
            keys[^1] = key;
            stringDatas[^1] = value;
        }
        public string GetString(string key)
        {
            var i = 0;
            while (i < keys.Length)
            {
                if (keys[i] == key)
                {
                    return stringDatas[i];
                }
                ++i;
            }
            return "";
        }
    }
}
