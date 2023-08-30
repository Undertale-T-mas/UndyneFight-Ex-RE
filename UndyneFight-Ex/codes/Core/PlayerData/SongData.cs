using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UndyneFight_Ex.IO;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.UserService
{
    public class SongData : ISaveLoad
    {
        public class SongState
        {
            private int score;
            private bool isAC, isAP;
            private readonly Difficulty difficulty;
            private SkillMark mark;

            public int Score => score;
            public SkillMark Mark => mark;
            public bool AC => isAC;
            public bool AP => isAP;

            public float Accuracy { get; set; }
            public float PauseTime { get; set; }

            public void UpdateNew(SongResult result)
            {
                int newScore = result.Score;
                if(result.Accuracy > this.Accuracy)
                {
                    this.PauseTime = result.PauseTime;
                }
                score = Math.Max(newScore, score);
                isAC |= result.AC;
                isAP |= result.AP;
                mark = (SkillMark)Math.Min((int)mark, (int)result.CurrentMark);
                Accuracy = MathF.Max(Accuracy, result.Accuracy);
            }

            public SongState(SaveInfo info)
            {
                difficulty = ToDif(info.Title);
                isAC = info["AC"] == "true";
                isAP = info["AP"] == "true";
                Accuracy = MathUtil.FloatFromString(info["Accuracy"]);
                score = Convert.ToInt32(info["score"]);
                mark = ToMark(info["mark"]);
                PauseTime = info.keysForIndexs.ContainsKey("pause") ? MathUtil.FloatFromString(info["pause"]) : 0;
            }
            public SongState(Difficulty dif, SongResult result)
            {
                difficulty = dif;
                int newScore = result.CurrentMark == SkillMark.Failed ? result.Score / 2 : result.Score;
                score = newScore;
                isAC = result.AC == true;
                isAP = result.CurrentMark == SkillMark.Impeccable;
                mark = result.CurrentMark;
            }
            public SaveInfo ToInfo()
            {
                SaveInfo info = new(difficulty.ToString() + ":score=" + score +
                    ",AC=" + (isAC ? "true" : "false") +
                    ",AP=" + (isAP ? "true" : "false") +
                    ",Accuracy=" + MathUtil.FloatToString(Accuracy, 5) +
                    ",pause=" + MathUtil.FloatToString(PauseTime, 2) +
                    ",mark=" + mark.ToString());
                return info;
            }
        }
        private readonly string songName;

        public string SongName => songName;

        public List<ISaveLoad> Children => null;

        private readonly Dictionary<Difficulty, SongState> songStates = new();
        public Dictionary<Difficulty, SongState> CurrentSongStates => songStates;

        private static SkillMark ToMark(string s)
        {
            return s switch
            {
                "Impeccable" => SkillMark.Impeccable,
                "Eminent" => SkillMark.Eminent,
                "Excellent" => SkillMark.Excellent,
                "Respectable" => SkillMark.Respectable,
                "Acceptable" => SkillMark.Acceptable,
                "Ordinary" => SkillMark.Ordinary,
                "Failed" => SkillMark.Failed,
                _ => throw new NotImplementedException()
            };
        }
        private static Difficulty ToDif(string s)
        {
            return s switch
            {
                "Noob" => Difficulty.Noob,
                "Easy" => Difficulty.Easy,
                "Normal" => Difficulty.Normal,
                "Hard" => Difficulty.Hard,
                "Extreme" => Difficulty.Extreme,
                "ExtremePlus" => Difficulty.ExtremePlus,
                _ => throw new NotImplementedException()
            };
        }

        public void Load(SaveInfo info)
        {
            foreach (var v in info.Nexts)
                CurrentSongStates.Add(ToDif(v.Key), new SongState(v.Value));
        }

        private SaveInfo GetInformation(Difficulty difficulty)
        {
            return CurrentSongStates[difficulty].ToInfo();
        }

        public SaveInfo Save()
        {
            SaveInfo info = new(songName + "{");
            foreach (Difficulty dif in CurrentSongStates.Keys)
                info.PushNext(GetInformation(dif));
            return info;
        }
        public void UpdateNew(Difficulty dif, SongResult result)
        {
            if (!songStates.ContainsKey(dif)) songStates.Add(dif, new SongState(dif, result));
            songStates[dif].UpdateNew(result);
        }
        public SongData(string name)
        {
            songName = name;
        }
    }
    public class SongManager : ISaveLoad
    {
        public List<ISaveLoad> Children => null;

        public IEnumerable<SongData> AllDatas
        {
            get
            {
                return songData.Values;
            }
        }
        Dictionary<string, SongData> songData = new();

        public SongData Require(string name)
        {
            return songData[name];
        }
        internal bool SongPlayed(string curFight)
        {
            return songData.ContainsKey(curFight);
        }
        internal void FinishedSong(string songName, Difficulty difficulty, SongResult result)
        {
            if (!songData.ContainsKey(songName))
                songData.Add(songName, new SongData(songName));
            songData[songName].UpdateNew(difficulty, result);
        }

        public void Load(SaveInfo info)
        {
            foreach (var v in info.Nexts)
            {
                songData.Add(v.Key, new SongData(v.Key));
                songData[v.Key].Load(v.Value);
            }
        }

        public SaveInfo Save()
        {
            SaveInfo info = new("NormalFights{");
            foreach (var v in songData)
                info.PushNext(v.Value.Save());
            return info;
        }
    }
    public class RatingCalculater
    {
        public class RatingList
        {
            public struct SingleSong : IComparable
            {
                public Difficulty difficulty;
                public float accuracy;
                public float threshold;
                public float transferAccuracy;
                public float scoreResult;
                public string name;

                public SingleSong(string name, Difficulty difficulty, float accuracy, float threshold, float transferAccuracy, float scoreScale)
                {
                    this.name = name;
                    this.difficulty = difficulty;
                    this.accuracy = accuracy;
                    this.threshold = threshold;
                    this.transferAccuracy = transferAccuracy;
                    scoreResult = threshold * transferAccuracy * scoreScale;
                }

                public int CompareTo(object obj)
                {
                    if (obj is not SingleSong) return 0;
                    SingleSong song = (SingleSong)obj;
                    int v = scoreResult.CompareTo(song.scoreResult);
                    return v != 0 ? v : name.CompareTo(song.name);
                }
            }
            public SortedSet<SingleSong> StrictDonors { get; private set; } = new();
            SingleSong completeDonor, fcDonor, apDonor;
            public SingleSong CompleteDonor => completeDonor;
            public SingleSong FCDonor => fcDonor;
            public SingleSong APDonor => apDonor;

            public void Submit(IEnumerable<SingleSong> strictDonors, SingleSong completeDonor, SingleSong fcDonor, SingleSong apDonor)
            {
                this.completeDonor = completeDonor;
                this.fcDonor = fcDonor;
                this.apDonor = apDonor;
                foreach (SingleSong song in strictDonors) StrictDonors.Add(song);
            }
        }

        public RatingCalculater(SongManager songManager)
        {
            _songManager = songManager;
        }

        readonly SongManager _songManager;

        private Tuple<float, float, float> GetDifficulty(IWaveSet waveSet, Difficulty difficulty)
        {
            SongInformation Information = waveSet.Attributes;

            float dif1 = 0, dif2 = 0, dif3 = 0;

            if (Information != null)
            {
                if (Information.CompleteDifficulty.ContainsKey(difficulty)) dif1 = Information.CompleteDifficulty[difficulty];
                if (Information.ComplexDifficulty.ContainsKey(difficulty)) dif2 = Information.ComplexDifficulty[difficulty];
                if (Information.APDifficulty.ContainsKey(difficulty)) dif3 = Information.APDifficulty[difficulty];
            }

            return new Tuple<float, float, float>(dif1, dif2, dif3);
        }

        public RatingList GenerateList()
        {
            RatingList.SingleSong ap1 = new("NULL", Difficulty.Noob, 0, 0, 0, 0);
            RatingList.SingleSong comp1 = new("NULL", Difficulty.Noob, 0, 0, 0, 0);
            RatingList.SingleSong fc1 = new("NULL", Difficulty.Noob, 0, 0, 0, 0);

            float apMax = 0, fcMax = 0, completeMax = 0;
            SortedSet<float> alls = new();
            Dictionary<string, IWaveSet> songType = new();
            foreach (var i in FightSystem.AllSongs.Values)
            {
                object o = Activator.CreateInstance(i);
                IWaveSet waveSet = o is IWaveSet ? o as IWaveSet : (o as IChampionShip).GameContent;
                songType.Add(waveSet.FightName, waveSet);
                for (int j = 0; j <= 5; j += 1)
                {
                    var v = GetDifficulty(waveSet, (Difficulty)j);
                    completeMax = MathF.Max(completeMax, v.Item1);
                    fcMax = MathF.Max(fcMax, v.Item3);
                    apMax = MathF.Max(apMax, v.Item3);
                    alls.Add(v.Item2);
                }
            }
            for (int i = 0; alls.Count < 7; i++) alls.Add(0 - i * 0.0001f);
            float ideal = 0.001f;
            for (int i = 0; i < 7; i++)
            {
                float g = MathF.Max(0, alls.Max); alls.Remove(g);
                ideal += g;
            }

            SortedSet<RatingList.SingleSong> best7 = new();
            Func<RatingList.SingleSong, RatingList.SingleSong, RatingList.SingleSong> SelectLarge = (x, y) =>
            {
                return x.scoreResult > y.scoreResult ? x : y;
            };
            foreach (var i in _songManager.AllDatas)
            {
                var song = i;
                if (!songType.ContainsKey(song.SongName)) continue;
                foreach (var j in song.CurrentSongStates)
                {
                    var cur = j.Value;
                    var dif = GetDifficulty(songType[song.SongName], j.Key);

                    best7.Add(new(song.SongName, j.Key, cur.Accuracy, dif.Item2, ReRate(cur.Accuracy), 85 / ideal));
                    if (best7.Count >= 8)
                        best7.Remove(best7.Min);
                    if (cur.Mark != SkillMark.Failed) comp1 = SelectLarge(comp1, new(song.SongName, j.Key, cur.Accuracy, dif.Item1, 1.0f, 5 / completeMax));
                    if (cur.AP) ap1 = SelectLarge(ap1, new(song.SongName, j.Key, cur.Accuracy, dif.Item3, 1.0f, 5 / apMax));
                    if (cur.AC) fc1 = SelectLarge(fc1, new(song.SongName, j.Key, cur.Accuracy, dif.Item3, 1.0f, 5 / fcMax));
                }
            }
            RatingList result = new();
            while (best7.Count >= 8) best7.Remove(best7.Min);
            result.Submit(best7, comp1, fc1, ap1);
            return result;
        }
        public Vector2 CalculateRating()
        {
            SortedSet<float> best7 = new();
            float ap1 = 0;
            float comp1 = 0;
            float fc1 = 0;

            float apMax = 0, fcMax = 0, completeMax = 0;
            SortedSet<float> alls = new();
            Dictionary<string, IWaveSet> songType = new();
            foreach (var i in FightSystem.AllSongs.Values)
            {
                object o = Activator.CreateInstance(i);
                IWaveSet waveSet = o is IWaveSet ? o as IWaveSet : (o as IChampionShip).GameContent;
                songType.Add(waveSet.FightName, waveSet);
                for (int j = 0; j <= 5; j += 1)
                {
                    var v = GetDifficulty(waveSet, (Difficulty)j);
                    completeMax = MathF.Max(completeMax, v.Item1);
                    fcMax = MathF.Max(fcMax, v.Item3);
                    apMax = MathF.Max(apMax, v.Item3);
                    alls.Add(v.Item2);
                }
            }
            foreach (var i in _songManager.AllDatas)
            {
                var song = i;
                foreach (var j in song.CurrentSongStates)
                {
                    var cur = j.Value;
                    if (!songType.ContainsKey(song.SongName)) continue;
                    var dif = GetDifficulty(songType[song.SongName], j.Key);

                    best7.Add(dif.Item2 * ReRate(cur.Accuracy) + MathUtil.GetRandom(-0.00001f, 0.00001f));
                    if (cur.Mark != SkillMark.Failed) comp1 = MathF.Max(comp1, dif.Item1);
                    if (cur.AP) ap1 = MathF.Max(ap1, dif.Item3);
                    if (cur.AC) fc1 = MathF.Max(fc1, dif.Item3);
                }
            }
            for (int i = 0; best7.Count < 7; i++) best7.Add(0 - i * 0.00001f);
            for (int i = 0; alls.Count < 7; i++) alls.Add(0 - i * 0.00001f);
            float sum = 0.001f, ideal = 0.001f;
            for (int i = 0; i < 7; i++)
            {
                float f = MathF.Max(0, best7.Max), g = MathF.Max(0, alls.Max);
                best7.Remove(f); alls.Remove(g);
                ideal += g; sum += f;
            }
            float rating0 = sum / ideal * 85f;
            float rating1 = fc1 / fcMax * 5f;
            float rating2 = ap1 / apMax * 5f;
            float rating3 = comp1 / completeMax * 5f;
            return new(rating0 + rating1 + rating2 + rating3, sum + fc1 + ap1 + comp1);
        }

        private float ReRate(float accuracy)
        {
            if (accuracy > 1) return 1;
            float del = 1 - accuracy;
            float lim = MathF.Pow(del * 3, 0.7f) / 2.4f + del * 2.0f;
            return MathF.Max(0, 1 - lim);
        }
    }
}