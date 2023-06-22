using System;
using System.Collections.Generic;
using UndyneFight_Ex.Achievements;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.UserService
{
    public class AchievementObject : ISaveLoad
    {
        public List<ISaveLoad> Children => throw new NotImplementedException();

        public AchievementObject(Achievement target)
        {
            TargetAchievement = target;
        }

        public Achievement TargetAchievement { get; init; }

        public void Load(SaveInfo info)
        {
            TargetAchievement.LoadProgress(info.IntValue);

            if (!info.keysForIndexs.ContainsKey("async")) info = Save();
            TargetAchievement.OnlineAchieved = info["async"] == "1";
        }

        public SaveInfo Save()
        {
            return new SaveInfo(
                $"{TargetAchievement.Title}:value={TargetAchievement.CurrentProgress},async={(TargetAchievement.OnlineAchieved ? 1 : 0)}");
        }
    }
}