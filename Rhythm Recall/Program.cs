//#define OSTPublish
#if !DEBUG
#define ThrowError
#endif
using Rhythm_Recall.Waves;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UndyneFight_Ex.Achievements;
using UndyneFight_Ex.GameInterface;
using UndyneFight_Ex.SongSystem;
using static UndyneFight_Ex.IO.IOEvent;
using static Rhythm_Recall.Resources;
using UndyneFight_Ex;

namespace Rhythm_Recall
{
    public static class Program
    {
        [DllImport("nvapi64.dll", EntryPoint = "fake")]
        private static extern int LoadNvApi64();

        [DllImport("nvapi.dll", EntryPoint = "fake")]
        private static extern int LoadNvApi32();

        private static void TryForceHighPerformanceGpu()
        {
            try
            {
                if (Environment.Is64BitProcess)
                {
                    LoadNvApi64();
                }
                else
                {
                    LoadNvApi32();
                }
            }
            catch { } // this will always be triggered, so just catch it and do nothing :P
        }
        [STAThread]
        private static void Main()
        {
            TryForceHighPerformanceGpu();
            Directory.CreateDirectory("Datas\\Global");
            if (!File.Exists("Datas\\Global\\TimeCheck.Tmpf"))
            {
                WriteTmpFile("Datas\\Global\\TimeCheck", StringToByte(DateTime.UtcNow.ToFileTimeUtc().ToString() + "\n"));
            }
            else
            {
                DateTime now = DateTime.UtcNow;
                List<string> comp = ByteToString(ReadTmpfFile("Datas\\Global\\TimeCheck"));
                DateTime cmpNow = DateTime.FromFileTimeUtc(Convert.ToInt64(comp[0]));
                int res = now.CompareTo(cmpNow);
                if (res == -1)
                {
                    throw new Exception();
                }
            }
            ResetDPI(); 

            try
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            }
            catch
            {

            }

            GameStartUp.LoadingSettings.TitleTextureRoot = "FontTexture\\Title";
            GameStartUp.LoadingSettings.TitleCentrePosition = new(320, 200);
            GameStartUp.SetUpShower = typeof(SetUpDrawing);
            GameStartUp.TitleShower = typeof(SetUpDrawing);
            GameStartUp.Initialize = Initialize;

            UFEXSettings.GamejoltPrivateKey = "bcf22bf14c202efe873a06c7b9980269";
            UFEXSettings.GamejoltID = 707354;

            UFEXSettings.MainServerURL = "www.uf-ex.com";
            UFEXSettings.MainServerPort = 9982;

            ClassicalGUI.MainMenuSettings.AchievementsEnabled = true;
            ClassicalGUI.MainMenuSettings.RecordEnabled = false;

            UFEXSettings.RecordEnabled = false;

            GameStartUp.PushChampionShip(SpringCelebration2021.GetChampionShip);
            GameStartUp.PushChampionShip(RedMay2021.GetChampionShip);
            GameStartUp.PushChampionShip(MidAutumn2021.GetChampionShip);
            GameStartUp.PushChampionShip(NewYear2022.GetChampionShip);
            GameStartUp.PushChampionShip(SpringCelebration2022.GetChampionShip);
            GameStartUp.PushChampionShip(Memory2023.GetChampionShip);
#if DEBUG || RELEASE
            GameStartUp.PushChampionShip(Summer_Camp_Ⅲ.GetChampionShip);
#endif
            SetAchievement();
            SetChallenge();
#if !OSTPublish
            //GameStartUp.PushExtra(new WoodenHall());    
            //GameStartUp.PushExtra(new WoodenHallFool());
#endif
            //GameStartUp.PushExtra(new OSTUndyne());      
            //GameStartUp.PushExtra(new OSTUndyneFool());    
            //GameStartUp.PushExtra(new MikuFight());    


            SongSet test = new("Test");
            test.Push(typeof(AprilExtends.Stasis));
            test.Push(typeof(Clb1e86f2));
            test.Push(typeof(TheFuneral));
            test.Push(typeof(Flan));
            test.Push(typeof(Galileo));
            test.Push(typeof(LoveAndHate));
            test.Push(typeof(Marisa));
            test.Push(typeof(Igallta));
            test.Push(typeof(AprilExtends.GrievousLady));
            test.Push(typeof(AprilExtends.LostMemory));
            test.Push(typeof(AprilExtends.EtherStrike));
            test.Push(typeof(AprilExtends.GrievousLady));
            test.Push(typeof(AprilExtends.Rrharil));
            test.Push(typeof(AprilExtends.HorizonBlue));
            test.Push(typeof(AprilExtends.Pentiment));
            test.Push(typeof(AprilExtends.Seraphim));
            test.Push(typeof(AprilExtends.Stasis));
            test.Push(typeof(MEGALOVANIA));
            test.Push(typeof(RIP));
            test.Push(typeof(BrokenAltair));
            test.Push(typeof(mu));

#if DEBUG
            GameStartUp.PushSongset(test);
#endif
            GameStartUp.SetMainSongs(new List<Type>() {
                //typeof(AprilExtends.Stasis),
                //typeof(AprilExtends.GrievousLady),
                //typeof(AprilExtends.Rrharil),
                //typeof(AprilExtends.HorizonBlue),
                //typeof(BrokenAltair),
                //typeof(Flan),
                //typeof(Galileo),
                //typeof(LoveAndHate),
                //typeof(Marisa),
                //typeof(Igallta),
                //typeof(Determination),
                //typeof(AprilExtends.Pentiment),
                //typeof(AprilExtends.BocchiTheRock),
                //typeof(AprilExtends.Seraphim),
                //typeof(Clb1e86f2),
                //typeof(BrainPower),
                //typeof(Weekender), 
                typeof(DreadNaught),
                //typeof(TheFuneral),
                //typeof(PapyEn), 
                typeof(Conflict),
                typeof(Resistance),

                typeof(BadApple),
                //typeof(ClassicalPractice),
                typeof(ClassicFight),
                typeof(EternalSpringDream), 
                typeof(ULBFight),
                typeof(UniversalCollapse),
                typeof(Dusttrust),
                typeof(Resurrection) ,

                typeof(Letsgonow),
                typeof(GOODWORLD),
                //typeof(MistemperedMalignance),
                //typeof(Rainshower),
                //typeof(EtherStrike.Game)
                });
#if OSTPublish
            GameStartUp.MainSceneIntro = () => { GameStates.SelectMode(0); ClassicalGUI.CreateFightSelector(); };
#endif
#if ThrowError
            try
            {
#endif
            //UndyneFight_Ex.Remake.Initialize.MainInitialize();
            GameStartUp.StartGame();
#if ThrowError
            }
            catch (Exception e)
            { 
                FileStream stream = new("bug data.txt", FileMode.OpenOrCreate);
                TextWriter textWriter = new StreamWriter(stream);
                textWriter.Write(e);
                textWriter.Flush();
                stream.Close(); 
            }
#endif

            //UndyneFight_Ex.ChampionShips.LicenseMaker.GetScore(MidAutumn2021.GetChampionShip);
        }

        private static void SetAchievement()
        {
            GameStartUp.PushAchievement(new Achievement
                ("Getting Start", "Complete any\nlevel", 1, new SongDataChecker((s) =>
                {
                    return !SongPlayData.IsCheat(s.GameMode) ? 1 : 0;
                }))
            {
                ID = 1,
                GameJoltID = 161249,
            }
            );
            GameStartUp.PushAchievement(new Achievement
                ("Better Accuracy", "Get a \n\"Respectable\"\nrating in any\nlevel", 1, new SongDataChecker((s) =>
                {
                    return SongPlayData.IsCheat(s.GameMode) ? 0 : (int)s.Result.CurrentMark <= 4 ? 1 : 0;
                }))
            {
                ID = 2,
                GameJoltID = 161250,
            }
            );
            GameStartUp.PushAchievement(new Achievement
                ("No hitter", "Get a \"NO HIT\" in any level", 1, new SongDataChecker((s) =>
                {
                    return SongPlayData.IsCheat(s.GameMode) ? 0 : s.Result.AC || s.Result.AP ? 1 : 0;
                }))
            {
                ID = 4,
                GameJoltID = 161252,
            }
            );
            GameStartUp.PushAchievement(new Achievement
                ("Delicate NO-HIT", "Get \"Excellent\"\nrating using\nStrict in a\nlevel with\ndifficulty\nhigher than 9", 1, new SongDataChecker((s) =>
                {
                    return SongPlayData.IsCheat(s.GameMode) ? 0 : s.Result.AC && s.CompleteThreshold >= 9f && (int)s.Result.CurrentMark <= 3 ? 1 : 0;
                }))
            {
                ID = 9,
                GameJoltID = 161257,
            }
            );
            GameStartUp.PushAchievement(new Achievement
                ("Impeccable", "Get an \n\"Impeccable\"\nrating in any\nlevel", 1, new SongDataChecker((s) =>
                {
                    return s.Result.CurrentMark == SkillMark.Impeccable ? 1 : 0;
                }))
            {
                ID = 7,
                GameJoltID = 161255,
            }
            );

            GameStartUp.PushAchievement(new Achievement
                ("Extreme gameplay", "Complete a\nlevel with the\ndifficulty\nhigher than 16", 1, new SongDataChecker((s) =>
                {
                    return SongPlayData.IsCheat(s.GameMode) ? 0 : s.CompleteThreshold >= 16f && (int)s.Result.CurrentMark <= 6 ? 1 : 0;
                }))
            {
                ID = 11,
                GameJoltID = 161259,
            }
            );

            GameStartUp.PushAchievement(new Achievement
                ("18+", "Complete a\nlevel which has\nthe difficulty\nhigher than 18", 1, new SongDataChecker((s) =>
                {
                    return SongPlayData.IsCheat(s.GameMode) ? 0 : s.CompleteThreshold >= 18f && (int)s.Result.CurrentMark <= 6 ? 1 : 0;
                }))
            {
                ID = 12,
                GameJoltID = 161260,
            }
            );
            GameStartUp.PushAchievement(new Achievement
                ("Beginner", "Get no less\nthan 20 rating", 20, new UserDataChecker((s) =>
                {
                    return (int)s.Skill;
                }))
            {
                ID = 3,
                GameJoltID = 161251,
            }
            );

            GameStartUp.PushAchievement(new Achievement
                ("Junior", "Get no less\nthan 30 rating", 30, new UserDataChecker((s) =>
                {
                    return (int)s.Skill;
                }))
            {
                ID = 5,
                GameJoltID = 161253,
            }
            );

            GameStartUp.PushAchievement(new Achievement
                ("Average Skill", "Get no less\nthan 40 rating", 40, new UserDataChecker((s) =>
                {
                    return (int)s.Skill;
                }))
            {
                ID = 6,
                GameJoltID = 161254,
            }
            );

            GameStartUp.PushAchievement(new Achievement
                ("Let's get higher", "Get no less\nthan 50 rating", 50, new UserDataChecker((s) =>
                {
                    return (int)s.Skill;
                }))
            {
                ID = 8,
                GameJoltID = 161256,
            }
            );

            GameStartUp.PushAchievement(new Achievement
                ("Senior", "Get no less\nthan 60 rating,\nYou aquired a\nmedal OwO", 60, new UserDataChecker((s) =>
                {
                    return (int)s.Skill;
                }))
            {
                ID = 10,
                GameJoltID = 161258,
            }
            );
        }

        private static void SetChallenge()
        {
            GameStartUp.PushChallenge(new("Sprites\\Challenges\\Classic Challenge", "Classic Challenge", new[] {
                new Tuple<Type, Difficulty>(typeof(ClassicFightRemake.Game), Difficulty.Normal),
                new Tuple<Type, Difficulty>(typeof(ULBFight), Difficulty.Normal),
                new Tuple<Type, Difficulty>(typeof(BadApple), Difficulty.Normal),
            }));

            GameStartUp.PushChallenge(new("Sprites\\Challenges\\Touhou Set", "Touhou Set", new[] {
                new Tuple<Type, Difficulty>(typeof(BadApple), Difficulty.Normal),
                new Tuple<Type, Difficulty>(typeof(DreamBattle.Game), Difficulty.Normal),
                new Tuple<Type, Difficulty>(typeof(EternalSpringDream), Difficulty.Hard),
            }));

            GameStartUp.PushChallenge(new("Sprites\\Challenges\\Megalo Style", "Megalo Style", new[] {
                new Tuple<Type, Difficulty>(typeof(Resurrection), Difficulty.Normal),
                new Tuple<Type, Difficulty>(typeof(ULBFight), Difficulty.Hard),
                new Tuple<Type, Difficulty>(typeof(Dusttrust.Game), Difficulty.Hard),
            }));

            GameStartUp.PushChallenge(new("Sprites\\Challenges\\Strive Forward", "Strive forward", new[] {
                new Tuple<Type, Difficulty>(typeof(Resistance), Difficulty.Hard),
                new Tuple<Type, Difficulty>(typeof(HopesAndDreams.Game), Difficulty.Normal),
                new Tuple<Type, Difficulty>(typeof(UniversalCollapse), Difficulty.Hard),
            }));

            GameStartUp.PushChallenge(new("Sprites\\Challenges\\Meme Fight", "Meme Fight", new[] {
                new Tuple<Type, Difficulty>(typeof(MinecraftRevenge.Game), Difficulty.Hard),
                new Tuple<Type, Difficulty>(typeof(HellTaker.Game), Difficulty.Hard),
                new Tuple<Type, Difficulty>(typeof(NeverGonnaGiveYouUp.Game), Difficulty.Extreme),
            }));

            GameStartUp.PushChallenge(new("Sprites\\Challenges\\MISHMASH_1", "Mishmash #1", new[] {
                new Tuple<Type, Difficulty>(typeof(NightofKnights.Game), Difficulty.Hard),
                new Tuple<Type, Difficulty>(typeof(Letsgonow.Game), Difficulty.Extreme),
                new Tuple<Type, Difficulty>(typeof(DreadNaught.Game), Difficulty.Extreme),
            }));

            GameStartUp.PushChallenge(new("Sprites\\Challenges\\Non-Greesoul Fight", "Non-Greensoul Fight", new[] {
                new Tuple<Type, Difficulty>(typeof(Gooddrill.GOODDRILL), Difficulty.Hard),
                new Tuple<Type, Difficulty>(typeof(SuddenChange.Game), Difficulty.Extreme),
                new Tuple<Type, Difficulty>(typeof(Dusttrust.Game), Difficulty.ExtremePlus),
            }));

            GameStartUp.PushChallenge(new("Sprites\\Challenges\\Eoxtic Culture", "Exotic Culture", new[] {
                new Tuple<Type, Difficulty>(typeof(SustenanceOfMoon.Game), Difficulty.Extreme),
                new Tuple<Type, Difficulty>(typeof(IndihomePaketPhoenix.Game), Difficulty.ExtremePlus),
                new Tuple<Type, Difficulty>(typeof(SpiningSetsugekka.Game), Difficulty.ExtremePlus),
            }));

            GameStartUp.PushChallenge(new("Sprites\\Challenges\\MISHMASH_2", "MishMash #2", new[] {
                new Tuple<Type, Difficulty>(typeof(UnderFell.Game), Difficulty.ExtremePlus),
                new Tuple<Type, Difficulty>(typeof(Goodrage.Game), Difficulty.ExtremePlus),
                new Tuple<Type, Difficulty>(typeof(FreedomDive.Game), Difficulty.ExtremePlus),
            }));
        }
        private static void ResetDPI()
        { }
    }
}

