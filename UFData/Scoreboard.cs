using System.Collections.Generic;
using System.Text.Json.Serialization;
using UFData;
using UndyneFight_Ex.SongSystem;

namespace UndyneFight_Ex.Server
{
    public class SongScoreBoard : AliveData
    {
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
        public RBTree<ScoreUnit> ScoreUnits { get; set; } = new();

        private Dictionary<long, int> _ranks { get; set; } = new();

        private bool _updated = false;
        private void Update()
        {
            return;
      /*      int pos = 0;
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
            if (!_updated) { _updated = true; Update(); }
            if (_ranks.ContainsKey(playerID))
            {   // If the player already exist in the scoreboard, remove the record is necessary
                int rank = _ranks[playerID];
                _ranks.Remove(playerID);
                ScoreUnit old = ScoreUnits[rank];
                ScoreUnits.Remove(old);
                old.Data = SongSystem.SongResult.PickBest(old.Data, playData.Result);
                this.ScoreUnits.Add(old);
                _ranks.Add(playerID, ScoreUnits.IndexOf(old));
            }
            else
            {
                // If not, simply add it to the scoreboard
                ScoreUnit d = new(playerID, playData.Result);
                this.ScoreUnits.Add(d);
                _ranks.Add(playerID, ScoreUnits.IndexOf(d));
            }
        }
    }
    public struct ScoreUnit : IComparable<ScoreUnit>, IComparable
    {
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
            int t = this.Data.Accuracy.CompareTo(other.Data.Accuracy);
            if(t != 0) return t;
            t = this.Data.Score.CompareTo(other.Data.Score);
            if (t != 0) return t;
            t = this.Data.AC.CompareTo(other.Data.AC);
            if (t != 0) return t;
            return this.PlayerID.CompareTo(other.PlayerID);
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) throw new Exception();
            return this.CompareTo((ScoreUnit)obj);
        }
    }
}