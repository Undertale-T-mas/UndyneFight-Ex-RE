using System;
using System.Collections.Generic;
using System.Linq;

namespace UndyneFight_Ex
{
    public class FightSet
    {
        public FightSet(string fightSetName)
        {
            this.fightSetName = fightSetName;
        }
        private string fightSetName;
        private Dictionary<string, Type> fightDictionary = new Dictionary<string, Type>();

        public void Push(Type type)
        {
            fightDictionary.Add(type.Name, type);
        }

        public Type[] Values => fightDictionary.Values.ToArray();

        public Type this[string index]
        {
            get { return fightDictionary[index]; }
        }

        public string FightSetName => fightSetName;
    }
    public class SongSet
    {
        public SongSet(string songSetName)
        {
            this.songSetName = songSetName;
        }
        private string songSetName;
        private Dictionary<string, Type> fightDictionary = new Dictionary<string, Type>();

        public void Push(Type type)
        {
            if (fightDictionary.ContainsKey(type.FullName)) return;
            fightDictionary.Add(type.FullName, type);
        }

        public Type[] Values => fightDictionary.Values.ToArray();

        public Type this[string index]
        {
            get { return fightDictionary[index]; }
        }

        public string SongSetName => songSetName;
    }
}