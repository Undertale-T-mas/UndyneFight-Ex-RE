using System.Text.Json.Serialization;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Server
{
    public class SongResult
    {
        public string? SongName { get; set; }
        public Dictionary<Difficulty, SongPlayData> DifficultyResults { get; set; } = new();

        public void Push(SongPlayData data)
        {
            if (DifficultyResults.ContainsKey(data.Difficulty)) DifficultyResults[data.Difficulty].Update(data);
            else DifficultyResults.Add(data.Difficulty, data);
        }
    }
}
namespace UndyneFight_Ex.SongSystem
{
    /// <summary>  
    ///Impeccable    -> All Perfect <br></br>
    ///Eminent       -> No Hit + 99% score<br></br>
    ///Excellent     -> No Hit + 98% socre<br></br>
    ///Respectable   -> 96% score<br></br>
    ///Acceptable    -> 92% score<br></br>
    ///Ordinary      -> 75% score<br></br>
    /// </summary>
    public enum SkillMark
    {
        Impeccable = 1,
        Eminent = 2,
        Excellent = 3,
        Respectable = 4,
        Acceptable = 5,
        Ordinary = 6,
        Failed = 7
    }
    public enum JudgementState
    {
        Lenient = 1,
        Balanced = 2,
        Strict = 3
    }
    public class SongPlayData
    {
        public SongResult Result { get; set; } = SongResult.Empty;
        public GameMode GameMode { get; set; } = GameMode.None;
        public float PauseTime { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public float CompleteThreshold { get; set; } = 0;
        public float ComplexThreshold { get; set; } = 0;
        public float APThreshold { get; set; } = 0;
        public Difficulty Difficulty { get; set; } = Difficulty.NotSelected;

        public static bool IsCheat(GameMode mode)
        {
            return ((int)mode & (int)GameMode.NoGreenSoul) != 0
|| ((int)mode & (int)GameMode.Practice) != 0 || ((int)mode & (int)GameMode.Autoplay) != 0;
        }

        public bool IsCheat()
        {
            return IsCheat(GameMode);
        }

        internal void Update(SongPlayData data)
        {
            if (data.Difficulty != this.Difficulty) throw new ArgumentException("Cannot update because the difficulty of the origin is different from the target");
            if (data.IsCheat()) throw new ArgumentException("Cannot update with a cheated data");
            this.CompleteThreshold = data.CompleteThreshold;
            this.ComplexThreshold = data.ComplexThreshold;
            this.APThreshold = data.APThreshold;
            this.PauseTime = data.PauseTime;
            this.Result = SongResult.PickBest(this.Result, data.Result);
        }
    }
    public struct SongResult
    {
        public readonly static SongResult Empty = new();
        public SongResult(SkillMark currentMark = SkillMark.Failed, int score = 0, float acc = 0, bool ac = false, bool ap = false, float pauseTime = 0)
        {
            CurrentMark = currentMark;
            Score = score;
            Accuracy = acc;
            AC = ac;
            AP = ap;
            this.PauseTime = pauseTime;
        }
        [JsonInclude]
        public SkillMark CurrentMark;
        [JsonInclude]
        public int Score;
        [JsonInclude]
        public bool AC;
        [JsonInclude]
        public bool AP;
        [JsonInclude]
        public float Accuracy;
        [JsonInclude]
        public float PauseTime;

        public static SongResult PickBest(SongResult result1, SongResult result2)
        {
            SongResult result = new SongResult(
                (SkillMark)Math.Min((int)result1.CurrentMark, (int)result2.CurrentMark),
                Math.Max(result1.Score, result2.Score),
                MathF.Max(result1.Accuracy, result2.Accuracy),
                result1.AC || result2.AC,
                result2.AP || result1.AP,
                result2.PauseTime
                );
            return result;
        }
    }
    public enum Difficulty
    {
        Noob = 0,
        Easy = 1,
        Normal = 2,
        Hard = 3,
        Extreme = 4,
        ExtremePlus = 5,
        NotSelected = 6
    }

    [Flags]
    public enum GameMode
    {
        None = 0,
        NoHit = 1,
        PerfectOnly = 2,
        AllPerfect = 3,
        NoGreenSoul = 4,
        Practice = 8,
        Buffed = 16,
        Autoplay = 32,
        RestartDeny = 64,
        PauseDeny = 256
    }
}