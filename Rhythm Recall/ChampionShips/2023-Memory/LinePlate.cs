using Microsoft.Xna.Framework;
using UndyneFight_Ex;
using static UndyneFight_Ex.FightResources;
using static UndyneFight_Ex.MathUtil;


namespace Rhythm_Recall.Waves
{
    internal partial class Transcendence
    {
        class LinePlate : Entity
        {
            class LineObject : Entity
            {
                Vector2 stablePosition;
                Vector2 mutablePosition;
                LinePlate father;
                int id;
                public LineObject(LinePlate father, int id)
                {
                    UpdateIn120 = true;
                    this.id = id;
                    this.father = father;
                    int missionId = this.id * father.Factor % father.Count;
                    float missionRotation = missionId * 1f / father.Count * 360f;
                    curRotation = missionRotation;
                }

                public override void Draw()
                {
                    DrawingLab.DrawLine(stablePosition, mutablePosition, 2f, father.DrawingColor * father.Alpha, 0.1f);
                }

                float curRotation;
                int appearTime = 0;
                public override void Update()
                {
                    appearTime++;
                    int missionId = id * father.Factor % father.Count;
                    float missionRotation = missionId * 1f / father.Count * 360f;
                    curRotation += MinRotate(curRotation, missionRotation) * 0.05f;
                    float stableRotation = id * 1f / father.Count * 360f;
                    stablePosition = father.Centre + GetVector2(father.Radius, stableRotation + father.RotationDelta);
                    mutablePosition = father.Centre + GetVector2(father.Radius, curRotation + father.RotationDelta);
                }
            }

            public int Factor { get; set; } = 1;
            public int Count { get; init; }
            public float Radius { get; set; } = 240;
            public float Alpha { get; set; } = 0.5f;
            public Color DrawingColor { get; set; } = Color.White;
            public Vector2 Centre = new(320, 240);
            public float RotationDelta { get; set; } = 90;
            public LinePlate(int count)
            {
                Count = count;
            }
            public override void Start()
            {
                for (int i = 0; i < Count; i++) AddChild(new LineObject(this, i));
                base.Start();
            }
            public override void Update()
            {

            }

            public override void Draw()
            {
#if DEBUG
                Font.FightFont.CentreDraw($"{Factor}/{Count}", new(320, 50), DrawingColor, 1, 0, 0.9f);
#endif
            }
        }
    }
}