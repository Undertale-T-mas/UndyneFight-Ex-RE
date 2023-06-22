using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.UserService
{
    public interface ISaveLoad
    {
        SaveInfo Save();
        void Load(SaveInfo info);
        List<ISaveLoad> Children { get; }
    }
    public partial class User : ISaveLoad
    {
        public static User CreateNew(string name, string password)
        {
            User user = new User();
            SaveInfo info;
            info = new SaveInfo("StartInfo->{");
            info.Nexts.Add("Password", new SaveInfo("Password:" + MathUtil.StringHash(password)));
            info.Nexts.Add("PlayerName", new SaveInfo("PlayerName:" + name));
            info.Nexts.Add("Coins", new SaveInfo("Coins:0"));
            info.Nexts.Add("Achievements", new SaveInfo("Achievements{"));
            info.Nexts.Add("ChampionShips", new SaveInfo("ChampionShips{"));
            info.Nexts.Add("NormalFights", new SaveInfo("NormalFights{"));
            info.Nexts.Add("VIP", new SaveInfo("VIP:false"));
            info.Nexts.Add("AC", new SaveInfo("AC{"));
            info.Nexts.Add("AP", new SaveInfo("AP{"));
            info.Nexts.Add("Mark", new SaveInfo("Mark{"));
            info.Nexts.Add("Skill", new SaveInfo("Skill:0"));
            info.Nexts.Add("GameJolt", new SaveInfo("GameJolt{"));
            info.Nexts.Add("Settings", new SaveInfo("Settings{"));
            info.Nexts.Add("ShopData", new SaveInfo("ShopData{"));
            info.Nexts.Add("ChallengeData", new SaveInfo("ChallengeData{"));
            user.Load(info);
            return user;
        }

        internal AchievementManager GetAchievement()
        {
            return _achievement;
        }

        public List<ISaveLoad> Children
        {
            get => null;
        }

        private readonly SongManager _songManager = new();
        private bool _isVip;
        private long _password;
        private string _name;
        public bool VIP => _isVip;
        public long Password => _password;
        public string PlayerName => _name;

        public float Skill { get; internal set; }
        public float AbsoluteSkill { get; internal set; }

        private Statistic _statistic;
        public Statistic PlayerStatistic => _statistic;

        public Settings Settings { get; private set; }
        public ShopData ShopData { get; private set; }
        public GJImformation GameJoltImformation { get; private set; }
        public ChampionshipManager ChampionshipData { get; private set; }
        public ChallengeData ChallengeData { get; private set; }
        public SaveInfo Custom => _custom;

        public AchievementManager _achievement { get; private set; }
        private SaveInfo _custom;

        public void FinishedSong(string songName, Difficulty difficulty, SongResult result)
        {
            _songManager.FinishedSong(songName, difficulty, result);
        }
        public void IntoVIP()
        {
            _isVip = true;
        }
        public void ResetPassword(long password)
        {
            _password = password;
        }

        public void Load(SaveInfo info)
        {
            _isVip = info.GetDirectory("VIP").BoolValue;

            if (!info.Nexts.ContainsKey("Skill")) info.Nexts.Add("Skill", new("value:0"));
            Skill = info.GetDirectory("Skill").FloatValue;

            _name = info.GetDirectory("PlayerName").StringValue;
            _password = Convert.ToInt64(info.GetDirectory("Password").StringValue);
            if (!info.Nexts.ContainsKey("Achievements"))
                info.Nexts.Add("Achievements", new SaveInfo("Achievements{"));
            if (!info.Nexts.ContainsKey("GameJolt"))
                info.Nexts.Add("GameJolt", new SaveInfo("GameJolt{"));
            if (!info.Nexts.ContainsKey("Settings"))
                info.Nexts.Add("Settings", new SaveInfo("Settings{"));
            if (!info.Nexts.ContainsKey("Customs"))
                info.Nexts.Add("Customs", new SaveInfo("Customs{"));
            if (!info.Nexts.ContainsKey("ChallengeData"))
                info.Nexts.Add("ChallengeData", new SaveInfo("ChallengeData{"));

            GameJoltImformation = new();
            GameJoltImformation.Load(info.Nexts["GameJolt"]);
            ChampionshipData = new();
            ChampionshipData.Load(info.Nexts["ChampionShips"]);
            Settings = new();
            Settings.Load(info.Nexts["Settings"]);

            SaveInfo fightInfo = info.Nexts["NormalFights"];
            if (!info.Nexts.ContainsKey("Statistic")) info.PushNext(new Statistic().Save());
            SaveInfo statisticInfo = info.Nexts["Statistic"];
            _statistic = new();
            _statistic.Load(statisticInfo);

            ChallengeData = new();
            ChallengeData.Load(info.Nexts["ChallengeData"]);

            if (fightInfo.Nexts != null)
                _songManager.Load(fightInfo);

            _achievement = new();
            _achievement.Load(info.Nexts["Achievements"]);

            _custom = info.Nexts["Customs"];

            UpdateSkill(CalculateRating());

            bool updated = false;
            if (!info.Nexts.ContainsKey("ShopData"))
            {
                info.Nexts.Add("ShopData", new SaveInfo("ShopData{"));
                updated = true;
            }
            ShopData = new();
            ShopData.Load(info.Nexts["ShopData"]);
            if (updated)
            {
                ShopData.CashManager.Coins = (int)(AbsoluteSkill * 80);
            }
        }

        public Vector2 CalculateRating()
        {
            RatingCalculater ratingCalculater = new(_songManager);
            return ratingCalculater.CalculateRating();
        }
        public RatingCalculater.RatingList GenerateList()
        {
            RatingCalculater ratingCalculater = new(_songManager);
            return ratingCalculater.GenerateList();
        }

        public SaveInfo Save()
        {
            SaveInfo info = new SaveInfo("StartInfo->{");

            info.PushNext(new SaveInfo("VIP:" + (_isVip ? "true" : "false")));
            info.PushNext(new SaveInfo("PlayerName:" + _name));
            info.PushNext(new SaveInfo("Password:" + _password));
            info.PushNext(new SaveInfo("Skill:" + MathUtil.FloatToString(Skill, 3)));
            info.PushNext(_custom);
            info.PushNext(GameJoltImformation.Save());
            info.PushNext(ChampionshipData.Save());
            info.PushNext(Settings.Save());
            info.PushNext(_statistic.Save());
            info.PushNext(_songManager.Save());
            info.PushNext(_achievement.Save());
            info.PushNext(ShopData.Save());
            info.PushNext(ChallengeData.Save());
            return info;
        }

        public void Apply()
        {
            Settings.Apply();
        }

        internal void SignUpChampionShip(string title, string div)
        {
            ChampionshipData.SignUp(title, div);
        }

        public void Rename(string name)
        {
            _name = name;
        }
        public void UpdateSkill(Vector2 skill)
        {
            Skill = skill.X;
            AbsoluteSkill = skill.Y;
        }
        public bool InChampionShip(string championship)
        {
            return ChampionshipData.InChampionship(championship);
        }
        public string ChampionShipDiv(string championship)
        {
            return ChampionshipData.ChampionshipDivision(championship);
        }

        public bool SongPlayed(string curFight)
        {
            return _songManager.SongPlayed(curFight);
        }
        public SongData GetSongData(string curFight)
        {
            return _songManager.Require(curFight);
        }
    }
}