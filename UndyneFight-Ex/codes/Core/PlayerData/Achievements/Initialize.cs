using System.Collections.Generic;
using UndyneFight_Ex.Achievements;
using UndyneFight_Ex.IO;

namespace UndyneFight_Ex.UserService
{
    public partial class AchievementManager : ISaveLoad
    {
        public List<ISaveLoad> Children => new();

        public Dictionary<string, AchievementObject> AchievementObjects = new();

        public void Load(SaveInfo info)
        {
            foreach (var pair in info.Nexts)
            {
                Insert(Achievements.AchievementManager.achievements[pair.Key]);
                AchievementObjects[pair.Key].Load(pair.Value);
            }
            foreach (var achieve in Achievements.AchievementManager.achievements)
            {
                if (!AchievementObjects.ContainsKey(achieve.Key))
                    Insert(achieve.Value);
            }
        }
        public void Insert(Achievement achievement)
        {
            AchievementObject obj = new(achievement);
            AchievementObjects.Add(achievement.Title, obj);
        }

        public SaveInfo Save()
        {
            SaveInfo info = new("Achievements{");
            foreach (var v in AchievementObjects)
            {
                info.PushNext(v.Value.Save());
            }
            return info;
        }
    }

}