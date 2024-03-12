using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static System.MathF;
using static UndyneFight_Ex.DrawingLab;
using static UndyneFight_Ex.Entities.Player;
using static UndyneFight_Ex.GlobalResources.Font;
using static UndyneFight_Ex.MathUtil;

namespace UndyneFight_Ex.Entities
{
    public partial class StateShower
    {
        public partial class ResultShower
        {
            private class AnalyzeShow : Entity
            {
                public float Alpha { private get; set; }
                public bool Enabled { get; internal set; }

                private Analyzer analyzer;

                public AnalyzeShow(Analyzer analyzer)
                {
                    this.analyzer = analyzer;
                    Analyze();
                }
                private void Analyze()
                {
                    AnalyzerData[] data = analyzer.CurrentData.ToArray();
                    Array.Sort(data);

                    leftTime = data[0].Time;
                    rightTime = data[^1].Time;
                    totalTime = rightTime - leftTime;

                    int maximumSoulCount = 0;

                    List<SoulChangeData> soulChangeData = new();
                    List<ArrowData> arrowData = new();
                    for (int i = 0; i < data.Length; i++)
                    {
                        AnalyzerData datum = data[i];
                        if (datum is SoulChangeData)
                        {
                            soulChangeData.Add(datum as SoulChangeData);
                        }
                        else if (datum is SoulListData)
                        {
                            SoulListData sld = datum as SoulListData;
                            if (!sld.IsInsert)
                                soulChangeData.Add(new SoulChangeData(-1, sld.SoulID, sld.Time));
                            maximumSoulCount = Math.Max(maximumSoulCount, sld.SoulID + 1);
                        }
                        else if (datum is ArrowData)
                        {
                            arrowData.Add(datum as ArrowData);
                        }
                    }
                    colorAlternates = new Dictionary<float, int>[maximumSoulCount];
                    for (int i = 0; i < maximumSoulCount; i++)
                    {
                        colorAlternates[i] = new Dictionary<float, int>();
                    }

                    foreach (SoulChangeData datum in soulChangeData)
                    {
                        if (colorAlternates[datum.SoulID].ContainsKey(datum.Time)) continue;
                        colorAlternates[datum.SoulID].Add(datum.Time, datum.SoulColor);
                    }

                    int pos = 0;
                    foreach (ArrowData datum in arrowData)
                    {
                        pos = (int)PosLerp(0, SplitCount, leftTime, rightTime + 1, datum.Time);
                        remarkCount[pos, datum.JudgementResult]++;
                    }
                    for (int i = 0; i < SplitCount; i++)
                    {
                        float v = 0;
                        for (int j = 0; j < 6; j++)
                        {
                            v += remarkCount[i, j];
                        }
                        remarkHeightMax = Max(remarkHeightMax, v);
                        remarkTotal[i] = v;
                    }

                    foreach (ArrowData datum in arrowData)
                    {
                        pos = (int)PosLerp(0, SplitCount, leftTime, rightTime + 1, datum.Time);
                        averageDelta[pos] += datum.DeltaTime / remarkTotal[pos];
                        if (datum.DeltaTime > 0.01f)
                            averagePositiveDelta[pos] += datum.DeltaTime / remarkTotal[pos];
                        else if (datum.DeltaTime < 0.01f)
                            averageNegativeDelta[pos] += datum.DeltaTime / remarkTotal[pos];
                    }
                }

                const int SplitCount = 120;

                private static readonly Color[] remarkColor = new Color[] { Color.DarkRed, Color.Lime, Color.LightBlue, Color.Gold, Color.Orange, Color.OrangeRed };
                private static readonly int[] remarkOrder = new int[] { 3, 4, 5, 2, 1, 0 };

                float totalTime, leftTime, rightTime;
                Dictionary<float, int>[] colorAlternates;
                float[,] remarkCount = new float[SplitCount, 6];
                float[] remarkTotal = new float[SplitCount];

                float[] averageDelta = new float[SplitCount];
                float[] averagePositiveDelta = new float[SplitCount];
                float[] averageNegativeDelta = new float[SplitCount];

                float remarkHeightMax = 0;
                float PosLerp(float lPos, float rPos, float lTime, float rTime, float curTime)
                {
                    return ((rPos - lPos) * ((curTime - lTime) / (rTime - lTime))) + lPos;
                }
                public override void Draw()
                {
                    if (!Enabled) return;
                    Vector2 graph1Loc = new(220, 450);

                    float lastX;
                    int y = 194;
                    float graphL = 212, graphR = 614;
                    for (int i = 0; i < colorAlternates.Length; i++)
                    {
                        y += 2;

                        lastX = graphL; Color color = Color.Transparent;
                        foreach (KeyValuePair<float, int> kvp in colorAlternates[i])
                        {
                            float x = PosLerp(graphL, graphR, leftTime, rightTime, kvp.Key);
                            DrawLine(new(lastX, y), new(x - 1, y), 1, color, 0.5f);

                            color = kvp.Value switch
                            {
                                -1 => Color.Transparent,
                                0 => Color.Red,
                                1 => Color.Lime,
                                2 => Color.Blue,
                                3 => Color.Orange,
                                4 => Color.MediumPurple,
                                _ => Color.Gray,
                            };
                            lastX = x;
                        }
                        DrawLine(new(lastX, y), new(graphR - 1, y), 1, color, 0.5f);
                    }

                    lastX = graphL;
                    graphL -= 3; graphR += 3;
                    for (int i = 1; i < SplitCount; i++)
                    {
                        float x = PosLerp(graphL, graphR, 0, SplitCount, i);

                        y = 191;
                        for (int j = 0; j < 6; j++)
                        {
                            int remark = remarkOrder[j];
                            if (remarkCount[i, remark] < 0.5f) continue;
                            int height = (int)(remarkCount[i, remark] * 100 / remarkHeightMax);
                            FormalDraw(FightResources.Sprites.pixUnit,
                            new Rectangle((int)lastX, y - height, (int)x - (int)lastX, height), remarkColor[remark]);
                            y -= height;
                        }
                        lastX = x;
                    }
                    graphL = 212; graphR = 614;

                    float centreY = 282;
                    DrawLine(new Vector2(graphL - 4, centreY), new Vector2(graphR, centreY), 2, Color.Silver, 0.99f);

                    lastX = graphL;

                    FightFont.Draw("early", new Vector2(graphL + 2, centreY + 5), Color.Orange, MathF.PI / 2, 0.6f, 0.99f);
                    FightFont.Draw("late", new Vector2(graphL + 2, centreY - 42), Color.Violet, MathF.PI / 2, 0.6f, 0.99f);

                    float lastY = averageDelta[0], lastYP = averagePositiveDelta[0], lastYN = averageNegativeDelta[0];
                    for (int i = 1; i < SplitCount; i++)
                    {
                        float x = PosLerp(graphL, graphR, 0, SplitCount - 1, i);
                        float y1 = averageDelta[i], yp = averagePositiveDelta[i], yn = averageNegativeDelta[i];
                        y1 = Clamp(-5, y1, 5);
                        yp = Clamp(-5, yp, 5);
                        yn = Clamp(-5, yn, 5);

                        DrawLine(new Vector2(lastX, (lastY * 10) + centreY), new Vector2(x, (y1 * 10) + centreY),
                            1, Color.White, 0.5f);

                        if (Abs(yp) > 0.01f || Abs(lastYP) > 0.01f)
                            DrawLine(new Vector2(lastX, (lastYP * 10) + centreY), new Vector2(x, (yp * 10) + centreY),
                                1, Color.Orange, 0.55f);

                        if (Abs(yn) > 0.01f || Abs(lastYN) > 0.01f)
                            DrawLine(new Vector2(lastX, (lastYN * 10) + centreY), new Vector2(x, (yn * 10) + centreY),
                                1, Color.Violet, 0.55f);

                        lastY = y1; lastYP = yp; lastYN = yn;

                        lastX = x;
                    }
                }

                public override void Update()
                {
                }
            }
        }
    }
}