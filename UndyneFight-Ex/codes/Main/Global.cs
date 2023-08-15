using System;

namespace UndyneFight_Ex
{
    //全域变量
    public class Global
    {
        public float[] floatDatas = { };
        public string[] stringDatas = { };
        public bool[] boolDatas = { };
        public string[] floatKeys = { };
        public string[] stringKeys = { };
        public string[] boolKeys = { };
        public void Add(string key, float value)
        {
            floatKeys[^1] = key;
            floatDatas[^1] = value;
        }
        public float GetValue(string key)
        {
            var i = 0;
            while (i < floatKeys.Length)
            {
                if (floatKeys[i] == key)
                {
                    return floatDatas[i];
                }
                ++i;
            }
            throw new NotImplementedException();
        }
        public void Add(string key, string value)
        {
            stringKeys[^1] = key;
            stringDatas[^1] = value;
        }
        public string GetString(string key)
        {
            var i = 0;
            while (i < stringKeys.Length)
            {
                if (stringKeys[i] == key)
                {
                    return stringDatas[i];
                }
                ++i;
            }
            throw new NotImplementedException();
        }
        public void Add(string key, bool value)
        {
            boolKeys[^1] = key;
            boolDatas[^1] = value;
        }
        public bool GetBool(string key)
        {
            var i = 0;
            while (i < boolKeys.Length)
            {
                if (boolKeys[i] == key)
                {
                    return boolDatas[i];
                }
                ++i;
            }
            throw new NotImplementedException();
        }
    }
}
