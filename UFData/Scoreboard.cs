using System.Text.Json.Serialization;
using UFData;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Server
{
    public class SongScoreBoard : AliveData
    {
        public SongScoreBoard() { this.SongName = string.Empty; }
        public SongScoreBoard(string name) => this.SongName = name;
        public string SongName { get; set; } 
        public Dictionary<Difficulty, UnitScoreBoard> DifficultyResults { get; set; } = new(); 
        
        public void InsertData(long playerID, SongPlayData playData)
        {
            this.Refresh();
            if (!DifficultyResults.ContainsKey(playData.Difficulty)) DifficultyResults.Add(playData.Difficulty, new());
            DifficultyResults[playData.Difficulty].InsertData(playerID, playData);
        }
    }
    public class UnitScoreBoard
    {
        public RBTree<ScoreUnit>? ScoreUnits { get; set; } = null;

        private Dictionary<long, ScoreUnit> _temp { get; set; } = new();

        public int RankOf(long uuid)
        {
            ScoreUnits ??= new();
            if (!_updated) { _updated = true; Update(); }
            if (!_temp.ContainsKey(uuid)) return -1;
            return ScoreUnits.IndexOf(_temp[uuid]);
        }

        private bool _updated = false;
        private void Update()
        {
            ScoreUnits ??= new();
            int index = -1;
            foreach (ScoreUnit scoreUnit in ScoreUnits)
            {
                index++;
                _temp.Add(scoreUnit.PlayerID, scoreUnit);
            }
            return;
            /*int pos = 0;
            var enumerator = ScoreUnits.GetEnumerator();

            List<ScoreUnit> errors = new();
            
            while (enumerator.MoveNext())
            {
                ScoreUnit scoreUnit = enumerator.Current;
                if(_ranks.ContainsKey(scoreUnit.PlayerID)) { errors.Add(scoreUnit); continue; }
                _ranks.Add(scoreUnit.PlayerID, pos);
                pos++;
            }
            errors.ForEach(s => ScoreUnits.Remove(s));*/
        }

        internal void InsertData(long playerID, SongPlayData playData)
        {
            ScoreUnits ??= new();
            if (!_updated) { _updated = true; Update(); }
            if (_temp.ContainsKey(playerID))
            {   // If the player already exist in the scoreboard, remove the record is necessary
               
                ScoreUnit old = _temp[playerID];
                _temp.Remove(playerID);
                ScoreUnits.Remove(old);
                old.Data = SongSystem.SongResult.PickBest(old.Data, playData.Result);
                this.ScoreUnits.Add(old);
                _temp.Add(playerID, old);
            }
            else
            {
                // If not, simply add it to the scoreboard
                ScoreUnit d = new(playerID, playData.Result);
                this.ScoreUnits.Add(d);
                _temp.Add(playerID, d);
            }
        }

        public SongSystem.SongResult ResultOf(long uuID)
        {
            return _temp[uuID].Data;
        } 
    }
    public struct ScoreUnit : IComparable<ScoreUnit>, IComparable
    {
        public ScoreUnit()
        {
            this.PlayerID = 0;
            this.Data = new();
        }
        public ScoreUnit(long uuID, SongSystem.SongResult data)
        {
            this.PlayerID = uuID;
            this.Data = data;
        }
        [JsonInclude]
        public long PlayerID;
        [JsonInclude]
        public SongSystem.SongResult Data;

        public int CompareTo(ScoreUnit other)
        { 
            int t = other.Data.Accuracy.CompareTo(this.Data.Accuracy);
            if(t != 0) return t;
            t = other.Data.Score.CompareTo(this.Data.Score);
            if (t != 0) return t;
            t = other.Data.AC.CompareTo(this.Data.AC);
            if (t != 0) return t;
            return other.PlayerID.CompareTo(this.PlayerID);
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) throw new Exception();
            return this.CompareTo((ScoreUnit)obj);
        }
    }
}