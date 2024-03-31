using System;
using System.Collections.Generic;
using System.Linq;
using UndyneFight_Ex.ChampionShips;

namespace UndyneFight_Ex
{
    public static class FightSystem
    {
        internal static bool CheckLevelExist { get; set; } = true;
        public static void Initialize(List<Type> loadItems)
        {
            if (loadItems == null)
            {
                if (CheckLevelExist)
                    throw new Exception("There is no levels in your game!");
                else return;
            }
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

        public static List<ChampionShip> ChampionShips { get; private set; } = new();
        public static ChampionShip CurrentChampionShip { get; internal set; }

        public static List<Challenge> Challenges { get; internal set; } = new();
        public static Dictionary<string, Challenge> ChallengeDictionary { get; internal set; } = new();

        public static List<SongSet> ExtraSongSets { get; internal set; } = new();

        public static void PushSongSet(SongSet songSet)
        {
            ExtraSongSets.Add(songSet);
        }
        public static void PushChampionShip(ChampionShip championShip)
        {
            ChampionShips.Add(championShip);
            foreach (var s in championShip.Fights.Values)
            {
                AllSongs.Push(s);
            }
        }
        public static void PushChallenge(Challenge challenge)
        {
            Challenges.Add(challenge);
            ChallengeDictionary.Add(challenge.Title, challenge);
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
            CurrentChampionShip = null;
            CurrentSongs = MainGameSongs;
        }

        public static List<Type> GetAllAvailables()
        {
            List<Type> result = new();
            result.AddRange(from v in MainGameSongs.Values select v);
            foreach (SongSet s in ExtraSongSets) result.AddRange(from v in s.Values select v);
            foreach (ChampionShip c in ChampionShips) if (c.CheckTime.Invoke() == ChampionShip.ChampionShipStates.End)
                    result.AddRange(from v in c.Fights.Values select v);
            return result;
        }
    }
}