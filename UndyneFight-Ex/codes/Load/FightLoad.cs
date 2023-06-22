using System;
using System.Collections.Generic;
using UndyneFight_Ex.ChampionShips;

namespace UndyneFight_Ex
{
    internal static class FightSystem
    {
        public static void Initialize(List<Type> loadItems)
        {
            if (loadItems == null) throw new Exception("There is no levels in your game!");
            foreach (var v in loadItems)
            {
                mainSongs.Add(v);
            }
            mainSongs.ForEach(s => MainGameSongs.Push(s));
            mainSongs.ForEach(s => AllSongs.Push(s));

            /* string[] files = Directory.GetFiles("Content\\Fights");
             foreach(string s in files)
             {
                 PushFight(s);
             }*/
        }
        /*
        private static void PushFight(string codeFile)
        {
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");

            CompilerResults cr = provider1.CompileAssemblyFromFile(cplist, codeFile);

            if (cr.Errors.HasErrors)
            {
                foreach (CompilerError err in cr.Errors)
                {
                    MessageBox.Show(err.ErrorText);
                }
            }
            else
            {
                // 通过反射，执行代码
                Assembly objAssembly = cr.CompiledAssembly;
                object obj = objAssembly.CreateInstance("CodeTest.Test");
                MethodInfo objMI = obj.GetType().GetMethod("ShowMessage");
                objMI.Invoke(obj, new object[] { "This is CodeTest!" });
            }
        }*/

        private static List<Type> mainSongs = new();

        public static SongSet CurrentSongs { get; private set; }
        public static SongSet AllSongs { get; private set; } = new SongSet("All");
        public static SongSet MainGameSongs { get; private set; } = new SongSet("MainGameSong");
        public static FightSet MainGameFights { get; private set; } = new FightSet("MainGameFight");

        public static List<ChampionShip> championShips = new();
        public static ChampionShip currentChampionShip;

        public static List<Challenge> challenges = new();
        public static Dictionary<string, Challenge> challengeDictionary = new();

        public static void PushChampionShip(ChampionShip championShip)
        {
            championShips.Add(championShip);
            foreach (var s in championShip.Fights.Values)
            {
                AllSongs.Push(s);
            }
        }
        public static void PushChallenge(Challenge challenge)
        {
            challenges.Add(challenge);
            challengeDictionary.Add(challenge.Title, challenge);
        }

        public static void PushExtra(Fight.IExtraOption classicFight)
        {
            MainGameFights.Push(classicFight.GetType());
        }
        internal static void SelectSongSet(ChampionShip championShip)
        {
            CurrentSongs = championShip.Fights;
        }
        internal static void SelectMainSet()
        {
            currentChampionShip = null;
            CurrentSongs = MainGameSongs;
        }
    }
}