using System;
using System.Collections.Generic;

namespace UndyneFight_Ex.Entities
{
    public partial class Player : Entity
    {
        public enum AnalyzerDataType
        {
            SoulColor = 0,
            Arrow = 1,
            Barrage = 2,
            SoulList = 3,
        }
        public abstract class AnalyzerData : IComparable
        {
            public AnalyzerData(float time)
            {
                Time = time;
            }

            public abstract AnalyzerDataType DataType { get; }
            public float Time { get; private init; }

            public int CompareTo(object obj)
            {
                return Time.CompareTo((obj as AnalyzerData).Time);
            }
        }
        public class SoulChangeData : AnalyzerData
        {
            public SoulChangeData(int soulColor, int soulID, float time) : base(time)
            {
                SoulID = soulID;
                SoulColor = soulColor;
            }

            public override AnalyzerDataType DataType => AnalyzerDataType.SoulColor;
            public int SoulColor { get; init; }
            public int SoulID { get; init; }
        }
        public class SoulListData : AnalyzerData
        {
            public SoulListData(int soulID, bool isInsert, float time) : base(time)
            {
                SoulID = soulID;
                IsInsert = isInsert;
            }

            public override AnalyzerDataType DataType => AnalyzerDataType.SoulColor;
            public bool IsInsert { get; init; }
            public int SoulID { get; init; }
        }
        public class ArrowData : AnalyzerData
        {
            public ArrowData(float deltaTime, int judgementResult, float time) : base(time)
            {
                DeltaTime = deltaTime;
                JudgementResult = judgementResult;
            }

            public override AnalyzerDataType DataType => AnalyzerDataType.Arrow;
            public float DeltaTime { get; init; }
            public int JudgementResult { get; init; }
        }
        public class Barrage : AnalyzerData
        {
            public Barrage(int judgementType, float time) : base(time)
            {
                JudgementType = judgementType;
            }

            public override AnalyzerDataType DataType => AnalyzerDataType.Barrage;
            public int JudgementType { get; init; }
        }
        public Analyzer GameAnalyzer { get; init; } = new();
        public class Analyzer : GameObject
        {
            public List<AnalyzerData> CurrentData { get; init; } = new();
            public override void Update()
            {

            }
            public void PushData(AnalyzerData analyzerData)
            {
                CurrentData.Add(analyzerData);
            }
        }
    }
}