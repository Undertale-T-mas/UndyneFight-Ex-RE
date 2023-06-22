using System;
using System.Collections.Generic;
using UndyneFight_Ex.UserService;

namespace UndyneFight_Ex.Achievements
{
    public enum CheckerType
    {
        User = 1,
        Song = 2
    }
    public interface IAchievementCheck
    {
        public CheckerType CheckType { get; }
        public int ProgressCheck(object input);
    }
    public class UserDataChecker : IAchievementCheck
    {
        private Func<User, int> checker;
        public UserDataChecker(Func<User, int> checker)
        {
            this.checker = checker;
        }

        public CheckerType CheckType => CheckerType.User;

        public int ProgressCheck(object input)
        {
            return checker(input as User);
        }
    }
    public class SongDataChecker : IAchievementCheck
    {
        private Func<SongSystem.SongPlayData, int> checker;
        public SongDataChecker(Func<SongSystem.SongPlayData, int> checker)
        {
            this.checker = checker;
        }
        public CheckerType CheckType => CheckerType.Song;
        public int ProgressCheck(object input)
        {
            return checker(input as SongSystem.SongPlayData);
        }
    }
    public class Achievement : IComparable<Achievement>
    {
        internal AchievementBlock GetBlock(int x, int y)
        {
            return new AchievementBlock(this, x, y) { };
        }

        public Achievement(string title, string introduction, int totalProgress, IAchievementCheck progressChecker)
        {
            Title = title;
            AchievementIntroduction = introduction;
            FullProgress = totalProgress;
            ProgressChecker = progressChecker;
        }

        public bool CheckProgress(object checkObj)
        {
            bool last = CurrentProgress >= FullProgress && !Locked;
            CurrentProgress = Math.Max(CurrentProgress, ProgressChecker.ProgressCheck(checkObj));
            bool cur = CurrentProgress >= FullProgress && !Locked;
            Achieved = cur;
            bool res = cur && !last;
            if (res) OnAchieve?.Invoke(this);
            return res;
        }
        public bool SetProgress(int progress)
        {
            bool last = CurrentProgress >= FullProgress && !Locked;
            CurrentProgress = progress;
            bool cur = CurrentProgress >= FullProgress && !Locked;
            Achieved = cur;
            bool res = cur && !last;
            if (res) OnAchieve?.Invoke(this);
            return res;
        }
        public void LoadProgress(int progress)
        {
            CurrentProgress = progress;
            Achieved = CurrentProgress >= FullProgress;
        }

        internal static event Action<Achievement> OnAchieve;

        public string AchievementIntroduction { get; set; }
        public IAchievementCheck ProgressChecker { private get; set; }
        public CheckerType CheckType => ProgressChecker.CheckType;
        public int FullProgress { get; init; } = 1;
        public string Title { get; set; }
        public int CurrentProgress { get; private set; } = 0;
        public int GameJoltID { get; set; }
        public bool Achieved { get; set; }
        public bool OnlineAchieved { get; set; } = false;
        public bool Loaded { get; set; } = false;

        public bool Hidden { get; init; } = false;
        public bool Locked { get; set; } = false;

        public int ID { private get; set; } = 0;

        internal void OnlineAsync()
        {
            if (!Achieved) return;
            if (OnlineAchieved) return;
            User user = PlayerManager.CurrentUser;
            var info = user.GameJoltImformation;
            if (!string.IsNullOrEmpty(info.GameJoltID) && GameJoltID != 0 && info.Authed)
            {
                GameJolt.GameJoltApi api = GameStates.GameJolt;
                api.Trophies.SetAchieved(user.GameJoltImformation.Credential, GameJoltID, (s) =>
                {
                    if (s.Success)
                    {
                        OnlineAchieved = true;
                    }
                    else
                    {

                    }
                });
            }
        }

        public int CompareTo(Achievement other)
        {
            return other.ID == ID ? other.Title.CompareTo(Title) : other.ID.CompareTo(ID);
        }
    }
    internal static class AchievementManager
    {
        public static Dictionary<string, Achievement> achievements = new();

        public static void Initialize()
        {
            AchievementShower.Initialze();
        }

        public static void CheckUserAchievements()
        {
            if (PlayerManager.CurrentUser == null) return;
            //UserService.AchievementManager p = PlayerManager.CurrentUser.GetAchievement();
            foreach (var s in achievements.Values)
            {
                if (s.CheckType == CheckerType.User)
                    if (s.CheckProgress(PlayerManager.CurrentUser))
                    {
                        ShowAchieved(s);
                    }
            }
        }
        public static void CheckSongAchievements(SongSystem.SongPlayData data)
        {
            if (PlayerManager.CurrentUser == null) return;
            //UserService.AchievementManager p = PlayerManager.CurrentUser.GetAchievement();
            foreach (var s in achievements.Values)
            {
                if (s.CheckType == CheckerType.Song)
                    if (s.CheckProgress(data))
                    {
                        ShowAchieved(s);
                    }
            }
        }
        public static void ShowAchieved(Achievement achievement)
        {
            GameStates.InstanceCreate(new AchievementResult(achievement));
        }

        public static void PushAchievement(Achievement achievement)
        {
            achievements.Add(achievement.Title, achievement);
        }
    }
}