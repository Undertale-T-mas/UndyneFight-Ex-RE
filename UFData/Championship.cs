using System.Text.Json.Serialization;
using UndyneFight_Ex.Server;
using UndyneFight_Ex.SongSystem;

namespace UFData
{ 
    public class ChampionshipScoreboard
    {
        public SortedSet<ChampionshipParticipant> Members { get; set; } = new();

        public void PushScore(User user, DivisionInformation div, SongPlayData data)
        {
            ChampionshipParticipant? p = null;
            foreach (ChampionshipParticipant participant in Members)
            {
                if(participant.UUID == user.UUID)
                {
                    p = participant;
                    break;
                }
            }
            if (p == null) Members.Add(new(user.UUID, div));
            p.Update(div.Info[data.Name].Item1, data.Result.Accuracy);
        }
    }
    public class ChampionshipInfo
    {
        public ChampionshipInfo(string name, DateTime start, DateTime end, Dictionary<string, DivisionInformation> divisions)
        {
            this.Name = name; 
            this.StartTime = start;
            this.EndTime = end;
            this.Divisions = divisions;
        }
        public ChampionshipInfo() { }
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        [JsonIgnore]
        public bool Available => DateTime.UtcNow < this.EndTime && DateTime.UtcNow > this.StartTime;

        public Dictionary<string, DivisionInformation> Divisions { get; set; } = new();
        public Dictionary<long, string> Participants { get; set; } = new();
    }
    public record class DivisionInformation(string DivisionName, Dictionary<string, Tuple<int,  Difficulty>> Info, ChampionshipScoreboard Scoreboard);
    public class ChampionshipParticipant : IComparable<ChampionshipParticipant>
    {
        public ChampionshipParticipant(long UUID, DivisionInformation curDivision){
            this.Division = curDivision.DivisionName;
            this.UUID = UUID;
            this.AccuracyList = new float[curDivision.Info.Count];
        }
        public ChampionshipParticipant() { }

        public void Update(int index, float acc)
        {
            this.AccuracyList[index] = MathF.Max(acc, this.AccuracyList[index]);
            this._count = false;
        }

        public string Division { get; set; }
        public long UUID { get; set; }
        [JsonInclude]
        public float[] AccuracyList { get; set; }

        private static float ItemTransfer(float acc)
        { 
            if (acc > 1) return 1;
            float del = 1 - acc;
            float lim = MathF.Pow(del * 3, 0.7f) / 2.4f + del * 2.0f;
            return MathF.Max(0, 1 - lim);
        }
        float _total;
        bool _count;

        [JsonIgnore]
        public float Total
        {
            get
            {
                if(_count) return _total;
                _count = true;
                float s = 0;
                for (int i = 0; i < this.AccuracyList.Length; i++) s += ItemTransfer(this.AccuracyList[i]);
                return s;
            }
        }

        public int CompareTo(ChampionshipParticipant? other)
        {
            if (other == null) return 1;
            return -this.Total.CompareTo(other.Total);
        } 
    }
}